using pocker_backend_core.frontEnd;

namespace pocker_backend_core.messages.interaction
{
    public abstract class AbstractRequest<T> : AbstractMessage<T>, IRequest
    {
        protected readonly User Requester = null;
    }
}