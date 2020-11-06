using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;
using Newtonsoft.Json;
using pocker_backend_core.frontEnd;
using pocker_backend_core.lobby;

namespace pocker_backend_core.messaging.interaction.response
{
    [Description("Событие обновления комнаты.")]
    public class UpdateLobbyResponse : AbstractResponse
    {
        [SuppressMessage("ReSharper", "NotAccessedField.Local")]
        [DefaultValue(typeof(Lobby), "{}")]
        [Description("Комната.")]
        [JsonProperty(Order = 1, DefaultValueHandling = DefaultValueHandling.Populate)]
        [Required]
        protected readonly Lobby Lobby;

        public UpdateLobbyResponse(User receiver, Lobby lobby) : base(receiver)
        {
            Lobby = lobby;
        }
    }
}