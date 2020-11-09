using System.ComponentModel;
using pocker_backend_core.frontEnd;

namespace pocker_backend_core.messaging.interaction.response
{
    [Description("Комнаты с таким названием не существует.")]
    public class FailureLobbyNotExistsResponse : AbstractResponse
    {
        public FailureLobbyNotExistsResponse(User receiver) : base(receiver)
        {
        }
    }
}