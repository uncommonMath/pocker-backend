using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using pocker_backend_core.frontEnd;
using pocker_backend_core.messages;

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

        [SuppressMessage("ReSharper", "InconsistentlySynchronizedField")]
        public Lobby this[string lobbyName] => _lobbies.FirstOrDefault(x => x.LobbyName == lobbyName);

        public Lobby NewLobby(string lobbyName, int lobbySize)
        {
            lock (_lobbies)
            {
                if (_lobbies.Any(x => x.LobbyName == lobbyName)) return null;
                var lobby = new Lobby(lobbyName, lobbySize);
                _lobbies.Add(lobby);
                return lobby;
            }
        }

        public void UpdateUser(User user)
        {
            Console.WriteLine($"User lost: {user}"); //todo exceptions work
            UserLeave(user);
        }

        public bool UserJoin(User requester, string userName, Lobby lobby)
        {
            lock (_lobbies)
            {
                if (_lobbies.Any(x => x.ContainsUser(requester))) throw new ApplicationException("already in lobby");

                Debug.Assert(_lobbies.Contains(lobby), "_lobbies.Contains(lobby)");
                return lobby.AddUser(requester, userName);
            }
        }

        private void UserLeave(User user)
        {
            lock (_lobbies)
            {
                var lobby = _lobbies.FirstOrDefault(x => x.ContainsUser(user));
                lobby?.RemoveUser(user);
            }
        }
    }
}