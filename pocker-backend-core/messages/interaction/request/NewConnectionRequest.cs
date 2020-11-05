using System.ComponentModel;
using System.Diagnostics;
using pocker_backend_core.frontEnd;
using pocker_backend_core.messages.interaction.response;

namespace pocker_backend_core.messages.interaction.request
{
    [Description("Технический request, не предназначен для использования на FrontEnd.")]
    public class NewConnectionRequest : AbstractRequest<FrontEndService>
    {
        private readonly Connection _connection;

        public NewConnectionRequest(Connection connection)
        {
            _connection = connection;
        }

        public override void Run(FrontEndService actor)
        {
            Debug.Assert(_connection != null,
                "_connection != null, probably trying use NewConnectionRequest on FrontEnd, which is unacceptable");
            var user = actor.Accept(_connection);
            Directory.Send(new SuccessConnectionResponse(user));
        }
    }
}