using System;
using System.Configuration;
using System.Text;
using WebSocketSharp;
using WebSocketSharp.Net;
using WebSocketSharp.Server;

namespace pocker_backend_core
{
    public class FrontEndService : WebSocketBehavior
    {
        private static HttpServer _httpServer;

        protected override void OnMessage(MessageEventArgs e)
        {
            var msg = e.Data == "password"
                ? "response"
                : "unknown";

            Send(msg);
        }

        public static void Start(ushort port)
        {
            _httpServer = new HttpServer(port)
            {
                DocumentRootPath = ConfigurationManager.AppSettings["DocumentRootPath"]
            };
            _httpServer.OnGet += (sender, e) =>
            {
                var req = e.Request;
                var res = e.Response;

                var path = req.RawUrl;
                if (path == "/")
                    path += "index.html";

                if (!e.TryReadFile(path, out var contents) || !path.EndsWith(".html"))
                {
                    Console.WriteLine($"404 - {path}");
                    res.StatusCode = (int) HttpStatusCode.NotFound;
                    return;
                }

                res.ContentEncoding = Encoding.UTF8;
                res.ContentLength64 = contents.LongLength;
                res.ContentType = "text/html";
                res.Close(contents, true);
            };
            _httpServer.AddWebSocketService<FrontEndService>("/FrontEnd");
            _httpServer.Start();
            if (_httpServer.IsListening)
            {
                Console.WriteLine("Listening on port {0}, and providing WebSocket services:", _httpServer.Port);
                foreach (var path in _httpServer.WebSocketServices.Paths)
                    Console.WriteLine("- {0}", path);
            }
        }
    }
}