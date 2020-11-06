using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using Newtonsoft.Json;
using NUnit.Framework;
using pocker_backend_core.frontEnd;
using pocker_backend_core.helper;

namespace pocker_backend_core.lobby
{
    [TypeConverter(typeof(LobbyConverter))]
    [JsonObject]
    public class Lobby
    {
        private readonly Dictionary<User, string> _users = new Dictionary<User, string>
        {
            {User.Invalid, "Go 1x1 noob"}
        };

        public Lobby(string lobbyName, int lobbySize)
        {
            LobbyName = lobbyName;
            LobbySize = lobbySize;
        }

        [DefaultValue("POE Hideout")]
        [System.ComponentModel.Description("Имя комнаты.")]
        [JsonProperty(Order = 1, DefaultValueHandling = DefaultValueHandling.Populate)]
        [Required]
        public string LobbyName { get; }

        [SuppressMessage("ReSharper", "MemberCanBePrivate.Global")]
        [DefaultValue(2)]
        [System.ComponentModel.Description("Размер комнаты.")]
        [JsonProperty(Order = 2, DefaultValueHandling = DefaultValueHandling.Populate)]
        [System.ComponentModel.DataAnnotations.Range(2, 4)]
        [Required]
        public int LobbySize { get; }

        [DefaultValue(typeof(StringList), "Go 1x1 noob")]
        [System.ComponentModel.Description("Участники комнаты.")]
        [JsonProperty(Order = 3, DefaultValueHandling = DefaultValueHandling.Populate)]
        [MinLength(1)]
        [MaxLength(4)]
        [Required]
        public IEnumerable<string> Users => _users.Values.ToList();

        public bool ContainsUser(User user)
        {
            return _users.ContainsKey(user);
        }

        public bool AddUser(User requester, string name)
        {
            if (_users.Count >= LobbySize) return false;

            _users.Add(requester, name);
            NotifyLobbyChanged(requester);
            return true;
        }

        public void RemoveUser(User requester)
        {
            Assert.IsTrue(_users.Remove(requester), "_users.Remove(requester)");
            NotifyLobbyChanged(requester);
        }

        private void NotifyLobbyChanged(User cause)
        {
            _users.Keys.Where(x => x != cause).ToList().ForEach(x => x.UpdateLobby(this));
        }
    }
}