using System;
using System.Threading;
using System.Threading.Tasks;
using NUnit.Framework;

namespace pocker_backend_core.helper
{
    public static class TasksHelper
    {
        public static void RecurringTask(Action action, int seconds, CancellationToken token) 
        {
            Assert.NotNull(action);
            Task.Run(async () => {
                while (!token.IsCancellationRequested)
                {
                    action();
                    await Task.Delay(TimeSpan.FromSeconds(seconds), token);
                }
            }, token);
        }
    }
}