using System;
using System.Collections.Generic;
using System.Configuration;
using System.Diagnostics.CodeAnalysis;
using System.Reflection;
using System.Security.Cryptography.X509Certificates;
using System.Text.RegularExpressions;
using pocker_backend_core.helper;
using pocker_backend_core.messaging;
using pocker_backend_core.messaging.@event;
using WebSocketSharp;
using WebSocketSharp.Net;
using WebSocketSharp.Server;

namespace pocker_backend_core.frontEnd
{
    public class FrontEndService : Actor
    {
        public static readonly FrontEndService Instance = new FrontEndService();

        private readonly Dictionary<User, Connection> _connections = new Dictionary<User, Connection>();

        private HttpServer _httpServer;

        private FrontEndService()
        {
            Console.CancelKeyPress += (s, e) =>
            {
                if (_httpServer?.IsListening ?? false) _httpServer.Stop();
                Console.WriteLine("finished");
            };
        }

        [SuppressMessage("ReSharper", "EventNeverSubscribedTo.Global")]
        public event Action<UserLostEventHandlerArgs> UserLost;

        public User Accept(Connection connection)
        {
            lock (_connections)
            {
                if (_connections.ContainsValue(connection)) throw new ApplicationException("connection already exists");

                var newUser = User.Create();
                _connections.Add(newUser, connection);
                return newUser;
            }
        }

        public void Lost(Connection connection)
        {
            lock (_connections)
            {
                if (!_connections.ContainsValue(connection))
                    throw new ApplicationException("connection does not exists");

                var user = CollectionHelper.GetByValue(_connections, connection);
                _connections.Remove(user);
                UserLost?.Invoke(new UserLostEventHandlerArgs(user));
            }
        }

        public void ReceiveRequest(Connection connection, IRequest request)
        {
            lock (_connections)
            {
                request.GetType().GetField("Requester",
                    BindingFlags.NonPublic | BindingFlags.Instance)!.SetValue(request,
                    CollectionHelper.GetByValue(_connections, connection));

                Directory.Send(request);
            }
        }

        public void SendResponse(AbstractResponse response)
        {
            lock (_connections)
            {
                var connection = _connections[response.Receiver];
                connection.SendResponse(response);
            }
        }

        public string Start(ushort port, bool secure)
        {
            lock (this)
            {
                if (_httpServer != null) throw new ApplicationException("already started");

                _httpServer = new HttpServer(port, secure)
                {
                    DocumentRootPath = ConfigurationManager.AppSettings["webRoot"],
                    Log =
                    {
                        Level = LogLevel.Info
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
                _httpServer.AddWebSocketService<Connection>("/FrontEnd");

                _httpServer.Start();

                if (!_httpServer.IsListening) throw new ApplicationException("startup");

                Console.WriteLine("Listening on port {0}, and providing WebSocket services:", _httpServer.Port);
                foreach (var path in _httpServer.WebSocketServices.Paths)
                    Console.WriteLine("- {0}", path);

                return $"{(_httpServer.IsSecure ? "https" : "http")}://{_httpServer.Address}:{_httpServer.Port}";
            }
        }
    }
}