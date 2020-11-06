using pocker_backend_core.frontEnd;

namespace pocker_backend_core.messaging.impl
{
    public class ConnectionLostMessage : AbstractMessage<FrontEndService>
    {
        private readonly Connection _connection;

        public ConnectionLostMessage(Connection connection)
        {
            _connection = connection;
        }

        public override void Run(FrontEndService actor)
        {
            actor.Lost(_connection);
        }
    }
}