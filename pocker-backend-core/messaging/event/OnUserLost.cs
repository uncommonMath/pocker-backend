using System.Diagnostics.CodeAnalysis;
using pocker_backend_core.frontEnd;
using pocker_backend_core.lobby;

namespace pocker_backend_core.messaging.@event
{
    [SuppressMessage("ReSharper", "ClassNeverInstantiated.Global")]
    [SuppressMessage("ReSharper", "UnusedType.Global")]
    public class OnUserLost : AbstractEvent<LobbyService, UserLostEventHandlerArgs>
    {
        public OnUserLost(UserLostEventHandlerArgs eventArgs) : base(eventArgs)
        {
        }

        public override void Run(LobbyService actor)
        {
            actor.OnUserLost(EventArgs.LostUser);
        }
    }

    public class UserLostEventHandlerArgs
    {
        public UserLostEventHandlerArgs(User lostUser)
        {
            LostUser = lostUser;
        }

        public User LostUser { get; }
    }
}