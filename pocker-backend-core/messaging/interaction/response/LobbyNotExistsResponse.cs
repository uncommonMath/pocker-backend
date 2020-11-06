using System.ComponentModel;
using pocker_backend_core.frontEnd;

namespace pocker_backend_core.messaging.interaction.response
{
    [Description("Комнаты с таким названием не существует.")]
    public class LobbyNotExistsResponse : AbstractResponse
    {
        public LobbyNotExistsResponse(User receiver) : base(receiver)
        {
        }
    }
}