using System;
using System.Diagnostics.CodeAnalysis;
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
            ws.Send("password");
            Console.ReadKey(true);
        }
    }
}