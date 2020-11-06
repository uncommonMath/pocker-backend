using System.ComponentModel;
using pocker_backend_core.frontEnd;

namespace pocker_backend_core.messaging.interaction.response
{
    [Description(
        "Недопустимое имя игрока(должно содержать 4-16 символов латиницы и не совпадать с именами других игроков на сервере).")]
    public class BadUsernameResponse : AbstractResponse
    {
        public BadUsernameResponse(User requester) : base(requester)
        {
        }
    }
}