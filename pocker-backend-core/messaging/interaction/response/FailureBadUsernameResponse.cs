using System.ComponentModel;
using pocker_backend_core.frontEnd;

namespace pocker_backend_core.messaging.interaction.response
{
    [Description(
        "Недопустимое имя игрока(должно содержать 4-16 символов латиницы или цифры и не совпадать с именами других игроков на сервере).")]
    public class FailureBadUsernameResponse : AbstractResponse
    {
        public FailureBadUsernameResponse(User requester) : base(requester)
        {
        }
    }
}