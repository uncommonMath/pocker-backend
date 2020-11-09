using System.Linq;
using Newtonsoft.Json;
using pocker_backend_core.frontEnd;

namespace pocker_backend_core.messaging
{
    [JsonObject]
    public abstract class AbstractRequest<T> : AbstractMessage<T>, IRequest
    {
        [JsonIgnore] protected User Requester { get; private set; }

        public void SendResponse<TR>(params object[] args) where TR : AbstractResponse
        {
            Directory.Send(typeof(TR).GetConstructors().First().Invoke(new[] {Requester}.Concat(args).ToArray()));
        }
    }
}