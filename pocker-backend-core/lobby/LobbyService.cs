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

        public static bool CheckLobbyName(string userName)
        {
            return Regex.IsMatch(userName, "^[A-Za-z]{4,16}$");
        }

        public void UpdateUser(User user)
        {
            Console.WriteLine($"User lost: {user}"); //to!do exceptions work
            UserLeave(user);
        }

        public bool CheckUsername(string userName)
        {
            lock (_lobbies)
            {
                return Regex.IsMatch(userName, "^[A-Za-z]{4,16}$") && !_lobbies.SelectMany(x => x.Users)
                    .Any(x => x.Equals(userName, StringComparison.OrdinalIgnoreCase));
            }
        }

        public Lobby NewLobby(string lobbyName, int lobbySize)
        {
            lock (_lobbies)
            {
                if (this[lobbyName] != null) return null;
                var lobby = new Lobby(lobbyName, lobbySize);
                _lobbies.Add(lobby);
                return lobby;
            }
        }

        public bool UserJoin(User requester, string userName, Lobby lobby)
        {
            lock (_lobbies)
            {
                if (GetLobbyByUser(requester) != null) throw new ApplicationException("already in lobby");

                if (!CheckUsername(userName)) throw new ArgumentException("username");

                Assert.IsFalse(_lobbies.Contains(lobby), "_lobbies.Contains(lobby)");
                return lobby.AddUser(requester, userName);
            }
        }

        private void UserLeave(User user)
        {
            lock (_lobbies)
            {
                var lobby = GetLobbyByUser(user);
                lobby?.RemoveUser(user);
            }
        }

        private Lobby GetLobbyByUser(User user)
        {
            lock (_lobbies)
            {
                return _lobbies.FirstOrDefault(x => x.ContainsUser(user));
            }
        }
    }
}