using System.Diagnostics.CodeAnalysis;

namespace pocker_backend_core.messaging
{
    public abstract class AbstractEvent<TA, TE> : AbstractMessage<TA>
    {
        protected readonly TE EventArgs;

        protected AbstractEvent(TE eventArgs)
        {
            EventArgs = eventArgs;
        }

        [SuppressMessage("ReSharper", "UnusedMember.Global")]
        public static void Static<TM>() where TM : AbstractEvent<TA, TE>
        {
            Directory.RegisterEvent<TM, TA, TE>();
        }
    }
}