using System.ComponentModel;
using pocker_backend_core.frontEnd;

namespace pocker_backend_core.messaging.interaction.response
{
    [Description("Успешное подключение к комнате.")]
    public class SuccessLobbyJoiningResponse : AbstractResponse
    {
        public SuccessLobbyJoiningResponse(User receiver) : base(receiver)
        {
        }
    }
}