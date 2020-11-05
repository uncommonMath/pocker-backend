using pocker_backend_core.frontEnd;
using pocker_backend_core.messages.interaction;

namespace pocker_backend_core.messages.impl
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