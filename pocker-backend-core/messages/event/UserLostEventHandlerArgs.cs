using pocker_backend_core.frontEnd;

namespace pocker_backend_core.messages.@event
{
    public class UserLostEventHandlerArgs
    {
        public UserLostEventHandlerArgs(User lostUser)
        {
            LostUser = lostUser;
        }

        public User LostUser { get; }
    }
}