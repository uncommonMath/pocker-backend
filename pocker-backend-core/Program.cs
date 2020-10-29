using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Net;
using System.Text;

namespace pocker_backend_core
{
    internal static class Program
    {
        private static async void SimpleListenerExample(IReadOnlyCollection<string> prefixes)
        {
            if (!HttpListener.IsSupported)
            {
                Console.WriteLine("Windows XP SP2 or Server 2003 is required to use the HttpListener class.");
                return;
            }

            if (prefixes == null || prefixes.Count == 0)
                throw new ArgumentException("prefixes");

            var listener = new HttpListener();
            foreach (var s in prefixes) listener.Prefixes.Add(s);
            listener.Start();

            while (true)
            {
                var context = await listener.GetContextAsync();
                var response = context.Response;
                var output = response.OutputStream;
                
                var responseString =
                    await File.ReadAllTextAsync(Path.Combine(
                        ConfigurationManager.AppSettings["webRoot"],
                        "index.html"));
                var buffer = Encoding.UTF8.GetBytes(responseString);
                await output.WriteAsync(buffer, 0, buffer.Length);
                output.Close();
            }
        }

        // ReSharper disable once UnusedParameter.Local
        private static void Main(string[] args)
        {
            var port = ushort.Parse(args[0]);
            
            if (args.Length < 2 || !string.Equals(args[1], "Simple", StringComparison.Ordinal))
                FrontEndService.Start(port);
            else
                SimpleListenerExample(new[] {$"http://*:{port}/"});
            
            Console.WriteLine($"Started application at http://localhost:{port}");
            
            Directory.StartDirectory();
        }
    }
}