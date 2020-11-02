using System;
using System.Configuration;
using System.Security.Cryptography.X509Certificates;
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

        public static string Start(ushort port, bool secure)
        {
            _httpServer = new HttpServer(port, secure)
            {
                DocumentRootPath = ConfigurationManager.AppSettings["webRoot"],
                Log =
                {
                    Level = LogLevel.Trace
                },
                Realm = "pocker-backend"
            };
            if (secure)
                _httpServer.SslConfiguration.ServerCertificate =
                    new X509Certificate2(ConfigurationManager.AppSettings["sslCertFile"]);
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
            _httpServer.AddWebSocketService<FrontEndService>("/FrontEnd");

            _httpServer.Start();

            if (!_httpServer.IsListening) throw new ApplicationException("startup");

            Console.WriteLine("Listening on port {0}, and providing WebSocket services:", _httpServer.Port);
            foreach (var path in _httpServer.WebSocketServices.Paths)
                Console.WriteLine("- {0}", path);

            return $"{(_httpServer.IsSecure ? "https" : "http")}://{_httpServer.Address}:{_httpServer.Port}";
        }
    }
}