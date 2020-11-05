using System.ComponentModel;
using pocker_backend_core.frontEnd;

namespace pocker_backend_core.messages.interaction.response
{
    [Description("Успешное подключение.")]
    public class SuccessConnectionResponse : AbstractResponse
    {
        public SuccessConnectionResponse(User receiver) : base(receiver)
        {
        }
    }
}