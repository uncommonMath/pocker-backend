using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using Microsoft.CodeAnalysis.CSharp.Scripting;
using WebSocketSharp.Net;
using WebSocketSharp.Server;

namespace pocker_backend_core
{
    public static class WebHelper
    {
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
                match => CSharpScript.EvaluateAsync(match.Groups[1].Value).Result?.ToString());
            contents = Encoding.UTF8.GetBytes(html);
        }

        public static void StatusCode(HttpStatusCode statusCode, HttpRequestEventArgs eventArgs)
        {
            eventArgs.Response.StatusCode = (int)statusCode;
            Console.WriteLine($"{statusCode} - {eventArgs.Request.Url} - {eventArgs.Request.RemoteEndPoint}");
        }
    }
}