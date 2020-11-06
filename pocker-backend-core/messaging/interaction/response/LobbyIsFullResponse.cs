using System.ComponentModel;
using pocker_backend_core.frontEnd;

namespace pocker_backend_core.messaging.interaction.response
{
    [Description("Комната заполнена.")]
    public class LobbyIsFullResponse : AbstractResponse
    {
        public LobbyIsFullResponse(User receiver) : base(receiver)
        {
        }
    }
}