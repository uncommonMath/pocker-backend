using System;
using System.Net;

namespace pocker_backend_core
{
    internal static class Program
    {
        private static void SimpleListenerExample(string[] prefixes)
        {
            if (!HttpListener.IsSupported)
            {
                Console.WriteLine ("Windows XP SP2 or Server 2003 is required to use the HttpListener class.");
                return;
            }
            if (prefixes == null || prefixes.Length == 0)
                throw new ArgumentException("prefixes");

            HttpListener listener = new HttpListener();
            foreach (string s in prefixes)
            {
                listener.Prefixes.Add(s);
            }
            listener.Start();
            while (true)
            {
                HttpListenerContext context = listener.GetContext();
                HttpListenerResponse response = context.Response;
                string responseString = "<HTML><BODY><center>Hello world!!!<center></BODY></HTML>";
                byte[] buffer = System.Text.Encoding.UTF8.GetBytes(responseString);
                response.ContentLength64 = buffer.Length;
                System.IO.Stream output = response.OutputStream;
                output.Write(buffer,0,buffer.Length);
                output.Close();
            }
        }
        
        // ReSharper disable once UnusedParameter.Local
        private static void Main(string[] args)
        {
            Console.WriteLine(args[0]);
            SimpleListenerExample(args);
        }
    }
}