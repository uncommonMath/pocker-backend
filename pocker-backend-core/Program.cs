using System;

namespace pocker_backend_core
{
    internal static class Program
    {
        private static void Main(string[] args)
        {
            var port = ushort.Parse(args[0]);

            var url = $"http://*:{port}/";

            if (args.Length < 2 || !string.Equals(args[1], "simple", StringComparison.Ordinal))
                url = FrontEndService.Start(port,
                    args.Length > 2 && string.Equals(args[2], "ssl", StringComparison.Ordinal));
            else
                WebHelper.SimpleListenerExample(new[] {url});

            Console.WriteLine($"Started application at {url}");

            Directory.StartDirectory();
        }
    }
}