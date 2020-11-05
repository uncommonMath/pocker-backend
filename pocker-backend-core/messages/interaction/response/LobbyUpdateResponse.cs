using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;
using Newtonsoft.Json;
using pocker_backend_core.frontEnd;
using pocker_backend_core.lobby;

namespace pocker_backend_core.messages.interaction.response
{
    [Description("Событие обновления комнаты.")]
    public class LobbyUpdateResponse : AbstractResponse
    {
        [SuppressMessage("ReSharper", "NotAccessedField.Local")] [JsonProperty] [Required]
        private readonly Lobby _lobby;

        public LobbyUpdateResponse(User receiver, Lobby lobby) : base(receiver)
        {
            _lobby = lobby;
        }
    }
}