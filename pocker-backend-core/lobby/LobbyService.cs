using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text.RegularExpressions;
using NUnit.Framework;
using pocker_backend_core.frontEnd;
using pocker_backend_core.messaging;

namespace pocker_backend_core.lobby
{
    public class LobbyService : Actor
    {
        [SuppressMessage("ReSharper", "UnusedMember.Global")]
        public static readonly LobbyService Instance = new LobbyService();

        private readonly List<Lobby> _lobbies = new List<Lobby>();

        private LobbyService()
        {
        }

        public Lobby this[string lobbyName]
        {
            get
            {
                lock (_lobbies)
                {
                    return _lobbies.FirstOrDefault(x => x.LobbyName == lobbyName);
                }
            }
        }

        public void OnUserLost(User user)
        {
            Console.WriteLine($"User lost: {user}"); //to!do exceptions work
            UserLeave(user);
        }

        public bool CheckLobbyName(string lobbyName)
        {
            return Regex.IsMatch(lobbyName, "^[A-Za-z0-9]{4,16}$") &&
                   this[lobbyName] == null;
        }

        public bool CheckUsername(string userName)
        {
            lock (_lobbies)
            {
                return Regex.IsMatch(userName, "^[A-Za-z0-9]{4,16}$") &&
                       GetLobbyByUser(userName) == null;
            }
        }

        public Lobby NewLobby(string lobbyName, int lobbySize)
        {
            lock (_lobbies)
            {
                Assert.IsTrue(CheckLobbyName(lobbyName), "CheckLobbyName(lobbyName)");

                var lobby = Lobby.NewLobby(lobbyName, lobbySize);
                _lobbies.Add(lobby);
                return lobby;
            }
        }

        public void UserJoin(User requester, string userName, Lobby lobby)
        {
            lock (_lobbies)
            {
                Assert.IsNull(GetLobbyByUser(requester), "GetLobbyByUser(requester) == null");

                Assert.IsTrue(CheckUsername(userName), "CheckUsername(userName)");

                Assert.IsTrue(_lobbies.Contains(lobby), "_lobbies.Contains(lobby)");

                lobby.AddUser(requester, userName);
            }
        }

        private void UserLeave(User user)
        {
            lock (_lobbies)
            {
                var lobby = GetLobbyByUser(user);
                lobby?.RemoveUser(user);

                if (lobby?.IsEmpty ?? false) _lobbies.Remove(lobby);
            }
        }

        private Lobby GetLobbyByUser(User user)
        {
            lock (_lobbies)
            {
                return _lobbies.FirstOrDefault(x => x.ContainsUser(user));
            }
        }

        private Lobby GetLobbyByUser(string userName)
        {
            lock (_lobbies)
            {
                return _lobbies.FirstOrDefault(x => x.ContainsUser(userName));
            }
        }
    }
}