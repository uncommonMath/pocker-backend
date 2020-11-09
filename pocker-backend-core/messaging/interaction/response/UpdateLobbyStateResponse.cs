using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;
using Newtonsoft.Json;
using pocker_backend_core.frontEnd;
using pocker_backend_core.helper;
using pocker_backend_core.lobby;

namespace pocker_backend_core.messaging.interaction.response
{
    [Description("Событие обновления комнаты.")]
    public class UpdateLobbyStateResponse : AbstractResponse
    {
        [DefaultValue("unknownEvent")]
        [Description("Идентификатор события.")]
        [JsonProperty(Order = 2, DefaultValueHandling = DefaultValueHandling.Populate)]
        [Required]
        protected readonly string EventType;

        [SuppressMessage("ReSharper", "NotAccessedField.Local")]
        [DefaultValue(typeof(LobbyConverter), "{}")]
        [Description("Объект, который обновился.")]
        [JsonProperty(Order = 3, DefaultValueHandling = DefaultValueHandling.Populate)]
        [Required]
        protected readonly Lobby Object;

        public UpdateLobbyStateResponse(User receiver, string eventType, Lobby @object) : base(receiver)
        {
            EventType = eventType;
            Object = @object;
        }
    }
}