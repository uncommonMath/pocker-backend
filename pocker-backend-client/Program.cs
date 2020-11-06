﻿using System;
using System.Diagnostics.CodeAnalysis;
using System.Threading;
using pocker_backend_core.helper;
using pocker_backend_core.messaging.interaction.request;
using WebSocketSharp;

namespace pocker_backend_client
{
    internal static class Program
    {
        [SuppressMessage("ReSharper", "UnusedParameter.Local")]
        [SuppressMessage("ReSharper", "FunctionNeverReturns")]
        private static void Main(string[] args)
        {
            using var ws = new WebSocket($"wss://{Console.ReadLine()}/FrontEnd");
            ws.OnMessage += (sender, e) =>
                Console.WriteLine("Response: " + e.Data);
            ws.Connect();
            Console.CancelKeyPress += (s, e) =>
            {
                ws.Close();
                Console.WriteLine("finished");
            };
            while (true)
            {
                var newLobbyReq = JsonHelper.Serialize(new JoinToLobbyNew("Dungeon", "Go 1x1 noob", 4));
                ws.Send(newLobbyReq);
                Thread.Sleep(5000);
            }
        }
    }
}