using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using DynamicExpresso;
using WebSocketSharp.Net;
using WebSocketSharp.Server;
using HttpListener = System.Net.HttpListener;

namespace pocker_backend_core.helper
{
    public static class WebHelper
    {
        private static readonly Interpreter Interpreter = new Interpreter();

        private static readonly List<string> AllowedExtensions =
            File.ReadAllLines(ConfigurationManager.AppSettings["allowedExtList"]).ToList();

        public static bool AllowedExtension(string path)
        {
            return AllowedExtensions.Any(path.EndsWith);
        }

        public static void PreProcessHtml(ref byte[] contents)
        {
            var html = Regex.Replace(
                Encoding.UTF8.GetString(contents),
                "\\$\\{(.+)\\}",
                match => Interpreter.Eval(match.Groups[1].Value).ToString());
            contents = Encoding.UTF8.GetBytes(html);
        }

        public static void StatusCode(HttpStatusCode statusCode, HttpRequestEventArgs eventArgs)
        {
            eventArgs.Response.StatusCode = (int) statusCode;
            Console.WriteLine($"{statusCode} - {eventArgs.Request.Url} - {eventArgs.Request.RemoteEndPoint}");
        }

        public static async void SimpleListenerExample(IReadOnlyCollection<string> prefixes)
        {
            if (!HttpListener.IsSupported)
                throw new ApplicationException(
                    "Windows XP SP2 or Server 2003 is required to use the HttpListener class.");

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
    }
}