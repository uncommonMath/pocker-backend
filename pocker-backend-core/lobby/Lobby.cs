using System;
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

        public override ICollection Subscribers => UsersMap.Keys;

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

        [DefaultValue(typeof(StringArrayConverter), "-1")]
        [System.ComponentModel.Description("Идентификаторы участников.")]
        [JsonProperty(Order = 4, DefaultValueHandling = DefaultValueHandling.Populate)]
        [MinLength(1)]
        [MaxLength(4)]
        [Required]
        public long[] Ids => UsersMap.Keys.Select(x => x.UserId).ToArray();

        [DefaultValue(true)]
        [System.ComponentModel.Description("Комната пуста.")]
        [JsonProperty(Order = 5, DefaultValueHandling = DefaultValueHandling.Populate)]
        [Required]
        public bool IsEmpty => !Users.Any();

        [DefaultValue(false)]
        [System.ComponentModel.Description("Комната заполнена.")]
        [JsonProperty(Order = 6, DefaultValueHandling = DefaultValueHandling.Populate)]
        [Required]
        public bool IsFull => Users.Length == LobbySize;

        public static Lobby NewLobby(string lobbyName, int lobbySize)
        {
            Assert.IsTrue(lobbySize > 1 && lobbySize < 5, "lobbySize > 1 && lobbySize < 5");

            var lobby = new Lobby(lobbyName, lobbySize);
            lobby.UsersMap.Clear();
            return lobby;
        }

        [StateChanger(ResponseType = typeof(UpdateLobbyStateResponse))]
        public void AddUser(User requester, string name)
        {
            Assert.IsFalse(IsFull, "IsFull");
            UsersMap.Add(requester, name);
        }

        [StateChanger(ResponseType = typeof(UpdateLobbyStateResponse))]
        public void RemoveUser(User requester)
        {
            Assert.IsTrue(UsersMap.Remove(requester), "UsersMap.Remove(requester)");
        }

        public bool ContainsUser(User user)
        {
            return UsersMap.ContainsKey(user);
        }

        public bool ContainsUser(string userName)
        {
            return UsersMap.Values.Any(x => x.Equals(userName, StringComparison.OrdinalIgnoreCase));
        }
    }
}