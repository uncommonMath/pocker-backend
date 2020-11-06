using System.ComponentModel;
using pocker_backend_core.frontEnd;

namespace pocker_backend_core.messaging.interaction.response
{
    [Description("Комната с таким названием уже существуем.")]
    public class LobbyAlreadyExistsResponse : AbstractResponse
    {
        public LobbyAlreadyExistsResponse(User receiver) : base(receiver)
        {
        }
    }
}