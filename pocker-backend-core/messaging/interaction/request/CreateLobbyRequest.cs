using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;
using pocker_backend_core.lobby;
using pocker_backend_core.messaging.interaction.request.@base;
using pocker_backend_core.messaging.interaction.response;

namespace pocker_backend_core.messaging.interaction.request
{
    [Description("Создание новой комнаты.")]
    public class CreateLobbyRequest : LobbyRequestBase
    {
        [DefaultValue(2)] [Description("Размер комнаты.")] [JsonProperty(Order = 3)] [Range(2, 4)] [Required]
        protected int LobbySize;

        public CreateLobbyRequest(string lobbyName, string username, int lobbySize) : base(username, lobbyName)
        {
            LobbySize = lobbySize;
        }

        public override void Run(LobbyService actor)
        {
            if (LobbySize < 2 || LobbySize > 4)
            {
                SendResponse<FailureBadLobbySizeResponse>();
                return;
            }

            if (!actor.CheckLobbyName(LobbyName))
            {
                SendResponse<FailureBadLobbyNameResponse>();
                return;
            }

            if (!actor.CheckUsername(Username))
            {
                SendResponse<FailureBadUsernameResponse>();
                return;
            }

            if (TryEnterLobby(actor, actor.NewLobby(LobbyName, LobbySize)))
                SendResponse<SuccessLobbyCreationResponse>();
        }
    }
}