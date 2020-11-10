using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Threading;
using Newtonsoft.Json;

namespace pocker_backend_core.frontEnd
{
    public sealed class User
    {
        private static long _incrementId;

        public static readonly User Invalid = new User(-1);

        private User(long userId)
        {
            UserId = userId;
        }

        [Description("Идентификатор пользователя.")]
        [JsonProperty(Order = 1, DefaultValueHandling = DefaultValueHandling.Populate)]
        [Required]
        public long UserId { get; }

        public static User Create()
        {
            return new User(Interlocked.Increment(ref _incrementId));
        }

        public override string ToString()
        {
            return $"User(Id={UserId})";
        }
    }
}