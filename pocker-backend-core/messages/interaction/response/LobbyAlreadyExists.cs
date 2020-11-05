using System.ComponentModel;
using pocker_backend_core.frontEnd;

namespace pocker_backend_core.messages.interaction.response
{
    [Description("Комната с таким названием уже существуем.")]
    public class LobbyAlreadyExists : AbstractResponse
    {
        public LobbyAlreadyExists(User receiver) : base(receiver)
        {
        }
    }
}