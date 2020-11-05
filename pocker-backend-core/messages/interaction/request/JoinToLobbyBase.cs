using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;
using pocker_backend_core.lobby;

namespace pocker_backend_core.messages.interaction.request
{
    public abstract class JoinToLobbyBase : AbstractRequest<LobbyService>
    {
        [DefaultValue("POE Hideout")] [Description("Имя комнаты.")] [JsonProperty(Order = 2)] [Required]
        protected string LobbyName;

        [DefaultValue("Go 1x1 noob")] [Description("Имя игрока.")] [JsonProperty(Order = 1)] [Required]
        protected string Username;

        protected JoinToLobbyBase(string lobbyName, string username)
        {
            LobbyName = lobbyName;
            Username = username;
        }
    }
}