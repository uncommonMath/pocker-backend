using System.ComponentModel;
using pocker_backend_core.frontEnd;

namespace pocker_backend_core.messaging.interaction.response
{
    [Description("Недопустимый размер комнаты(должен быть от 2 до 4 участников).")]
    public class FailureBadLobbySizeResponse : AbstractResponse
    {
        public FailureBadLobbySizeResponse(User requester) : base(requester)
        {
        }
    }
}