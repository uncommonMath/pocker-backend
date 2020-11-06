using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;
using pocker_backend_core.lobby;
using pocker_backend_core.messaging.interaction.response;

namespace pocker_backend_core.messaging.interaction.request
{
    public abstract class JoinToLobbyBase : AbstractRequest<LobbyService>
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

        protected JoinToLobbyBase(string username, string lobbyName)
        {
            Username = username;
            LobbyName = lobbyName;
        }

        protected bool TryEnterLobby(LobbyService actor, Lobby lobby)
        {
            if (!actor.CheckUsername(Username))
            {
                Directory.Send(new BadUsernameResponse(Requester));
                return false;
            }

            if (actor.UserJoin(Requester, Username, lobby)) return true;
            Directory.Send(new LobbyIsFullResponse(Requester));
            return false;
        }
    }
}