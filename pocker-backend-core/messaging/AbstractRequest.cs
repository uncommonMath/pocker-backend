using Newtonsoft.Json;
using pocker_backend_core.frontEnd;

namespace pocker_backend_core.messaging
{
    [JsonObject]
    public abstract class AbstractRequest<T> : AbstractMessage<T>, IRequest
    {
        [JsonIgnore] protected readonly User Requester;
    }
}