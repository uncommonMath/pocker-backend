using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;
using pocker_backend_core.lobby;
using pocker_backend_core.messaging.interaction.response;

namespace pocker_backend_core.messaging.interaction.request
{
    [Description("Создание новой комнаты.")]
    public class JoinToLobbyNew : JoinToLobbyBase
    {
        [DefaultValue(2)] [Description("Размер комнаты.")] [JsonProperty(Order = 3)] [Range(2, 4)] [Required]
        protected int LobbySize;

        public JoinToLobbyNew(string lobbyName, string username, int lobbySize) : base(username, lobbyName)
        {
            LobbySize = lobbySize;
        }

        public override void Run(LobbyService actor)
        {
            if (LobbySize < 2 || LobbySize > 4) throw new ArgumentException("LobbySize");

            if (!LobbyService.CheckLobbyName(LobbyName))
            {
                Directory.Send(new BadLobbyNameResponse(Requester));
                return;
            }
            if (!actor.CheckUsername(Username))
            {
                Directory.Send(new BadUsernameResponse(Requester));
                return;
            }

            var lobby = actor.NewLobby(LobbyName, LobbySize);
            if (lobby == null)
            {
                Directory.Send(new LobbyAlreadyExistsResponse(Requester));
                return;
            }

            if (TryEnterLobby(actor, lobby)) Directory.Send(new SuccessLobbyCreationResponse(Requester));
        }
    }
}