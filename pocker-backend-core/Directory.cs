using System.Collections.Concurrent;
using System.Diagnostics.CodeAnalysis;

namespace pocker_backend_core
{
    public class Directory
    {
        private static readonly Directory Instance = new Directory();

        private readonly BlockingCollection<object> _messages = new BlockingCollection<object>();

        private Directory()
        {
        }

        public static void StartDirectory()
        {
            lock (Instance)
            {
                Instance.Start();
            }
        }

        [SuppressMessage("ReSharper", "FunctionNeverReturns")]
        private void Start()
        {
            while (!_messages.IsCompleted) _messages.Take();
        }
    }
}