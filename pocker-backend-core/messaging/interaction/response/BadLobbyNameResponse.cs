using System.ComponentModel;
using pocker_backend_core.frontEnd;

namespace pocker_backend_core.messaging.interaction.response
{
    [Description("Недопустимое название комнаты(должно содержать 4-16 символов латиницы или цифры).")]
    public class BadLobbyNameResponse : AbstractResponse
    {
        public BadLobbyNameResponse(User requester) : base(requester)
        {
        }
    }
}