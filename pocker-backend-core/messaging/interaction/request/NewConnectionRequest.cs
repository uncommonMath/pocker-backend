using NUnit.Framework;
using pocker_backend_core.frontEnd;
using pocker_backend_core.messaging.interaction.response;

namespace pocker_backend_core.messaging.interaction.request
{
    [System.ComponentModel.Description("Технический request, не предназначен для использования на FrontEnd.")]
    public class NewConnectionRequest : AbstractRequest<FrontEndService>
    {
        private readonly Connection _connection;

        public NewConnectionRequest(Connection connection)
        {
            _connection = connection;
        }

        public override void Run(FrontEndService actor)
        {
            Assert.IsTrue(_connection != null,
                "_connection == null, probably trying use NewConnectionRequest on FrontEnd, which is unacceptable");
            Send<SuccessConnectionResponse>(actor.Accept(_connection));
        }
    }
}