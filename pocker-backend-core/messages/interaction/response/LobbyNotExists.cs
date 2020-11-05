using System.ComponentModel;
using pocker_backend_core.frontEnd;

namespace pocker_backend_core.messages.interaction.response
{
    [Description("Комнаты с таким названием не существует.")]
    public class LobbyNotExists : AbstractResponse
    {
        public LobbyNotExists(User receiver) : base(receiver)
        {
        }
    }
}