using System.Diagnostics.CodeAnalysis;
using System.Threading;

namespace pocker_backend_core
{
    public class Directory
    {
        private static readonly Directory Instance = new Directory();

        public static void StartDirectory()
        {
            ThreadPool.QueueUserWorkItem(
                state => state.Start(),
                Instance,
                false);
            Instance.Wait();
        }

        private readonly ManualResetEvent _manualResetEvent = new ManualResetEvent(false);

        private Directory()
        {
        }

        [SuppressMessage("ReSharper", "FunctionNeverReturns")]
        private void Start()
        {
            while (true)
            {
            }
        }

        private void Wait()
        {
            _manualResetEvent.WaitOne();
        }
    }
}