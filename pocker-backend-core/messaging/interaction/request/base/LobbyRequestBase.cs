using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;
using pocker_backend_core.lobby;
using pocker_backend_core.messaging.interaction.response;

namespace pocker_backend_core.messaging.interaction.request.@base
{
    public abstract class LobbyRequestBase : AbstractRequest<LobbyService>
    {
        [DefaultValue("POE Hideout")]
        [Description("Имя комнаты.")]
        [JsonProperty(Order = 2, DefaultValueHandling = DefaultValueHandling.Populate)]
        [Required]
        protected string LobbyName;

        [DefaultValue("Go 1x1 noob")]
        [Description("Имя игрока.")]
        [JsonProperty(Order = 1, DefaultValueHandling = DefaultValueHandling.Populate)]
        [Required]
        protected string Username;

        protected LobbyRequestBase(string username, string lobbyName)
        {
            Username = username;
            LobbyName = lobbyName;
        }

        protected bool TryEnterLobby(LobbyService actor, Lobby lobby)
        {
            if (!actor.CheckUsername(Username))
            {
                SendResponse<FailureBadUsernameResponse>();
                return false;
            }

            if (lobby.IsFull)
            {
                SendResponse<FailureLobbyIsFullResponse>();
                return false;
            }

            actor.UserJoin(Requester, Username, lobby);
            return true;
        }
    }
}