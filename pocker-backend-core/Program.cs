using System;
using pocker_backend_core.frontEnd;
using pocker_backend_core.helpers;
using pocker_backend_core.messages;

namespace pocker_backend_core
{
    internal static class Program
    {
        private static void Main(string[] args)
        {
            InitializationHelper.Initialize();

            if (args.Length > 3 && string.Equals(args[3], "schemas", StringComparison.Ordinal))
            {
                Support.GenerateSchemasForInteractions();
                return;
            }

            var port = ushort.Parse(args[0]);

            var url = $"http://*:{port}/";

            if (args.Length < 2 || !string.Equals(args[1], "simple", StringComparison.Ordinal))
                url = FrontEndService.Instance.Start(port,
                    args.Length > 2 && string.Equals(args[2], "ssl", StringComparison.Ordinal));
            else
                WebHelper.SimpleListenerExample(new[] {url});

            Console.WriteLine($"Started application at {url}");

            Directory.StartDirectory();
        }
    }
}