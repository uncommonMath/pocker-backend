using pocker_backend_core.frontEnd;

namespace pocker_backend_core.messaging.impl
{
    public class ReceivedRequestMessage : AbstractMessage<FrontEndService>
    {
        private readonly Connection _connection;

        private readonly IRequest _request;

        public ReceivedRequestMessage(Connection connection, IRequest request)
        {
            _connection = connection;
            _request = request;
        }

        public override void Run(FrontEndService actor)
        {
            actor.ReceiveRequest(_connection, _request);
        }
    }
}