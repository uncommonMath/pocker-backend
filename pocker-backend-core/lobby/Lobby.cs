using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using Newtonsoft.Json;
using NUnit.Framework;
using pocker_backend_core.frontEnd;
using pocker_backend_core.helper;
using pocker_backend_core.messaging;
using pocker_backend_core.messaging.interaction.response;

namespace pocker_backend_core.lobby
{
    [JsonObject]
    public class Lobby : AbstractStateHolder
    {
        protected readonly Dictionary<User, string> UsersMap = new Dictionary<User, string>
        {
            {User.Invalid, "Go1x1Noob"}
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

        [DefaultValue(2)]
        [System.ComponentModel.Description("Размер комнаты.")]
        [JsonProperty(Order = 2, DefaultValueHandling = DefaultValueHandling.Populate)]
        [System.ComponentModel.DataAnnotations.Range(2, 4)]
        [Required]
        public int LobbySize { get; }

        [DefaultValue(typeof(StringArrayConverter), "Go1x1noob")]
        [System.ComponentModel.Description("Участники комнаты.")]
        [JsonProperty(Order = 3, DefaultValueHandling = DefaultValueHandling.Populate)]
        [MinLength(1)]
        [MaxLength(4)]
        [Required]
        public string[] Users => UsersMap.Values.ToArray();

        [JsonIgnore] public bool IsEmpty => !Users.Any();
        public override ICollection Subscribers => UsersMap.Keys;

        public static Lobby NewLobby(string lobbyName, int lobbySize)
        {
            Assert.IsTrue(lobbySize > 1 && lobbySize < 5, "lobbySize > 1 && lobbySize < 5");
            var lobby = new Lobby(lobbyName, lobbySize);
            lobby.UsersMap.Clear();
            return lobby;
        }

        public bool ContainsUser(User user)
        {
            return UsersMap.ContainsKey(user);
        }

        [StateChanger(ResponseType = typeof(UpdateLobbyStateResponse))]
        public bool AddUser(User requester, string name)
        {
            if (UsersMap.Count >= LobbySize) return false;
            UsersMap.Add(requester, name);
            return true;
        }

        [StateChanger(ResponseType = typeof(UpdateLobbyStateResponse))]
        public void RemoveUser(User requester)
        {
            Assert.IsTrue(UsersMap.Remove(requester), "UsersMap.Remove(requester)");
        }
    }
}