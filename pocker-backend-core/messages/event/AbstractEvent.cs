using System.Diagnostics.CodeAnalysis;

namespace pocker_backend_core.messages.@event
{
    public abstract class AbstractEvent<TA, TE> : AbstractMessage<TA>
    {
        protected readonly TE EventArgs;

        protected AbstractEvent(TE eventArgs)
        {
            EventArgs = eventArgs;
        }

        [SuppressMessage("ReSharper", "UnusedMember.Global")]
        public static void Static<T>() where T : AbstractEvent<TA, TE>
        {
            Directory.RegisterEvent<T, TA, TE>();
        }
    }
}