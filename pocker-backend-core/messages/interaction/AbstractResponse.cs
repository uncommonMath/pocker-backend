using Newtonsoft.Json;
using pocker_backend_core.frontEnd;

namespace pocker_backend_core.messages.interaction
{
    public abstract class AbstractResponse : AbstractMessage<FrontEndService>
    {
        protected AbstractResponse(User receiver)
        {
            Receiver = receiver;
        }

        [JsonIgnore] public User Receiver { get; }

        public override void Run(FrontEndService actor)
        {
            actor.SendResponse(this);
        }
    }
}