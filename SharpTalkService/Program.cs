using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.WebSockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace SharpTalkService
{
    class Program
    {
        static void Main(string[] args)
        {
            if (args.Length < 1 || args[0] == "listen")
            {
                Console.WriteLine("Listening");
                using (var ws = new ClientWebSocket())
                {
                    ws.ConnectAsync(new Uri("ws://127.0.0.1"), CancellationToken.None);
                    while (ws.State == WebSocketState.Open)
                    {
                        string msg = "aeiou";
                        ArraySegment<byte> bytesToSend = new ArraySegment<byte>(Encoding.UTF8.GetBytes(msg));
                         ws.SendAsync(bytesToSend, WebSocketMessageType.Text, true, CancellationToken.None).RunSynchronously();
                        ArraySegment<byte> bytesReceived = new ArraySegment<byte>(new byte[1024]);
                        WebSocketReceiveResult result = ws.ReceiveAsync(bytesReceived, CancellationToken.None).Result;
                        var backMsg = Encoding.UTF8.GetString(bytesReceived.Array, 0, result.Count);
                        Console.WriteLine(backMsg);
                        //Console.WriteLine(Encoding.UTF8.GetString(bytesReceived.Array, 0, result.Count));

                    }
                }
            }
            else
            {
                Console.WriteLine("Hosting");
                // PipeServer.Go();
                var wss = new WebSocketServer(9009);
                wss.Start();
            }
        }
    }
}
