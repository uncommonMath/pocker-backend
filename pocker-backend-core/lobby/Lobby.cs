using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using pocker_backend_core.frontEnd;

namespace pocker_backend_core.lobby
{
    public class Lobby
    {
        private readonly int _lobbySize;

        private readonly Dictionary<User, string> _users = new Dictionary<User, string>();

        public Lobby(string lobbyName, int lobbySize)
        {
            LobbyName = lobbyName;
            _lobbySize = lobbySize;
        }

        [Required] public string LobbyName { get; }

        [SuppressMessage("ReSharper", "UnusedMember.Global")]
        [Required]
        public List<string> Users => _users.Values.ToList();

        public bool ContainsUser(User user)
        {
            return _users.ContainsKey(user);
        }

        public bool AddUser(User requester, string name)
        {
            if (_users.Count >= _lobbySize) return false;

            _users.Add(requester, name);
            NotifyLobbyChanged(requester);
            return true;
        }

        public void RemoveUser(User requester)
        {
            Debug.Assert(_users.Remove(requester), "_users.Remove(requester)");
            NotifyLobbyChanged(requester);
        }

        private void NotifyLobbyChanged(User cause)
        {
            _users.Where(x => x.Key != cause).ToList().ForEach(x => x.Key.UpdateLobby(this));
        }
    }
}