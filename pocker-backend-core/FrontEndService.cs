using System;
using System.Configuration;
using System.Text.RegularExpressions;
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
                DocumentRootPath = ConfigurationManager.AppSettings["webRoot"]
            };
            _httpServer.AddWebSocketService<FrontEndService>("/FrontEnd");
            _httpServer.OnGet += (sender, e) =>
            {
                var req = e.Request;
                var res = e.Response;

                var path = req.RawUrl;
                if (path == "/")
                    path += "index.html";
                path = Regex.Replace(path, "\\?.+", string.Empty);

                if (!e.TryReadFile(path, out var contents) || !WebHelper.AllowedExtension(path))
                {
                    WebHelper.StatusCode(HttpStatusCode.NotFound, e);
                    return;
                }

                if (path.EndsWith(".html")) WebHelper.PreProcessHtml(ref contents);
                
                WebHelper.StatusCode(HttpStatusCode.OK, e);
                res.ContentLength64 = contents.LongLength;
                res.Close(contents, true);
            };
            
            _httpServer.Start();

            if (!_httpServer.IsListening) return;
            {
                Console.WriteLine("Listening on port {0}, and providing WebSocket services:", _httpServer.Port);
                foreach (var path in _httpServer.WebSocketServices.Paths)
                    Console.WriteLine("- {0}", path);
            }
        }
    }
}