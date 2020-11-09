using System;
using System.Linq;

namespace pocker_backend_core.messaging
{
    public abstract class AbstractMessage<T>
    {
        public abstract void Run(T actor);

        public Type GetMessageType()
        {
            return typeof(T);
        }

        public static void Send<TM>(params object[] args)
        {
            Directory.Send(typeof(TM).GetConstructors().First().Invoke(args));
        }
    }
}