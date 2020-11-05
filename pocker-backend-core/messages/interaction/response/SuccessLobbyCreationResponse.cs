using System.ComponentModel;
using pocker_backend_core.frontEnd;

namespace pocker_backend_core.messages.interaction.response
{
    [Description("Успешное создание комнаты.")]
    public class SuccessLobbyCreationResponse : AbstractResponse
    {
        public SuccessLobbyCreationResponse(User receiver) : base(receiver)
        {
        }
    }
}