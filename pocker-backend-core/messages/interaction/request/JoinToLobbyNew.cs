using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;
using pocker_backend_core.lobby;
using pocker_backend_core.messages.interaction.response;

namespace pocker_backend_core.messages.interaction.request
{
    [Description("Создание новой комнаты.")]
    public class JoinToLobbyNew : JoinToLobbyBase
    {
        [DefaultValue(2)] [Description("Размер комнаты.")] [JsonProperty(Order = 3)] [RangeAttribute(2, 4)]
        private int _lobbySize;

        public JoinToLobbyNew(string lobbyName, string username, int lobbySize) : base(lobbyName, username)
        {
            _lobbySize = lobbySize;
        }

        public override void Run(LobbyService actor)
        {
            if (_lobbySize < 2 || _lobbySize > 4) throw new ArgumentException("_lobbySize");

            var lobby = actor.NewLobby(LobbyName, _lobbySize);
            if (lobby == null)
            {
                Directory.Send(new LobbyAlreadyExists(Requester));
                return;
            }

            if (!actor.UserJoin(Requester, Username, lobby)) throw new ApplicationException("unexpected");

            Directory.Send(new SuccessLobbyCreationResponse(Requester));
        }
    }
}