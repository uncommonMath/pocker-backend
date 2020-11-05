using System.Diagnostics.CodeAnalysis;
using pocker_backend_core.lobby;

namespace pocker_backend_core.messages.@event
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
            actor.UpdateUser(EventArgs.LostUser);
        }
    }
}