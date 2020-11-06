using System;

namespace pocker_backend_core.messaging
{
    public abstract class AbstractMessage<T>
    {
        public abstract void Run(T actor);

        public Type GetMessageType()
        {
            return typeof(T);
        }
    }
}