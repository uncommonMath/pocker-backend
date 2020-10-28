using System;

namespace pocker_backend_core
{
    internal static class Program
    {
        private static async void SimpleListenerExample(string[] prefixes)
        {
            if (!System.Net.HttpListener.IsSupported)
            {
                Console.WriteLine("Windows XP SP2 or Server 2003 is required to use the HttpListener class.");
                return;
            }
            if (prefixes == null || prefixes.Length == 0)
                throw new ArgumentException("prefixes");

            var listener = new System.Net.HttpListener();
            foreach (var s in prefixes)
            {
                listener.Prefixes.Add(s);
            }
            listener.Start();
            
            while (true)
            {
                var context = await listener.GetContextAsync();
                var response = context.Response;
                var responseString =
                    await System.IO.File.ReadAllTextAsync(System.IO.Path.Combine(
                        System.Configuration.ConfigurationManager.AppSettings["DocumentRootPath"],
                        "index.html"));
                var buffer = System.Text.Encoding.UTF8.GetBytes(responseString);
                var output = response.OutputStream;
                await output.WriteAsync(buffer,0,buffer.Length);
                output.Close();
            }
        }

        // ReSharper disable once UnusedParameter.Local
        private static void Main(string[] args)
        {
            var port = ushort.Parse(args[0]);
            Console.WriteLine($"Staring up application on {port} port");
            if (args.Length < 2 || !string.Equals(args[1], "Simple", StringComparison.Ordinal))
            {
                FrontEndService.Start(port);
            }
            else
            {
                SimpleListenerExample(new[] {$"http://*:{port}/"});
            }
            Directory.StartDirectory();
        }
    }
}