using System.ComponentModel;
using pocker_backend_core.frontEnd;

namespace pocker_backend_core.messaging.interaction.response
{
    [Description("Комната заполнена.")]
    public class FailureLobbyIsFullResponse : AbstractResponse
    {
        public FailureLobbyIsFullResponse(User receiver) : base(receiver)
        {
        }
    }
}