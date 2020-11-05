using System;
using System.Diagnostics.CodeAnalysis;
using pocker_backend_core.helpers;
using pocker_backend_core.messages.interaction.request;
using WebSocketSharp;

namespace pocker_backend_client
{
    internal static class Program
    {
        [SuppressMessage("ReSharper", "UnusedParameter.Local")]
        private static void Main(string[] args)
        {
            using var ws = new WebSocket($"wss://{Console.ReadLine()}/FrontEnd");
            ws.OnMessage += (sender, e) =>
                Console.WriteLine("Response: " + e.Data);
            ws.Connect();
            /*ws.Send("wrong");
            Console.ReadKey(true);
            ws.Send("password");
            Console.ReadKey(true);*/
            Console.ReadKey(true);
            // ReSharper disable StringLiteralTypo
            var newLobbyReq = JsonHelper.Serialize(new JoinToLobbyNew("Dungeon", "Arthas", 4));
            ws.Send(newLobbyReq);
            var newLobbyReq2 = JsonHelper.Serialize(new JoinToLobbyNew("Dungeon", "Arthas", 4));
            ws.Send(newLobbyReq2);
            var newLobbyReq3 = JsonHelper.Serialize(new JoinToLobbyNew("Dungeon2", "Arthas", 4));
            // ReSharper restore StringLiteralTypo
            ws.Send(newLobbyReq3);
            Console.ReadKey(true);
            ws.Close();
        }
    }
}