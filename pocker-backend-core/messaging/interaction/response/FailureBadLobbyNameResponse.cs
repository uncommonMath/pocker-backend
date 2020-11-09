using System.ComponentModel;
using pocker_backend_core.frontEnd;

namespace pocker_backend_core.messaging.interaction.response
{
    [Description("Недопустимое название комнаты(должно содержать 4-16 символов латиницы или цифры).")]
    public class FailureBadLobbyNameResponse : AbstractResponse
    {
        public FailureBadLobbyNameResponse(User requester) : base(requester)
        {
        }
    }
}