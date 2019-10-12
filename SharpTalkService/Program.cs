using Fleck;
using SharpTalk;
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
                Listen().Wait();
            }
            else
            {
                Console.WriteLine("Hosting");
                using (var tts = new FonixTalkEngine())
                {
                    var server = new WebSocketServer("ws://127.0.0.1:8181");
                    server.Start(socket =>
                    {
                        socket.OnOpen = () => Console.WriteLine("Open!");
                        socket.OnClose = () => Console.WriteLine("Close!");
                        socket.OnMessage = message => tts.Speak(message);
                    });
                    while (true)
                    {
                        if (Console.ReadKey() == new ConsoleKeyInfo('q', ConsoleKey.Q, false, false, false))
                        {
                            return;
                        }
                    }
                }
            }
        }

        private static async Task Listen() {
            using (var ws = new ClientWebSocket())
            {
               await ws.ConnectAsync(new Uri("ws://127.0.0.1:8181"), CancellationToken.None);
                while (ws.State == WebSocketState.Open)
                {
                    //string msg = "aeiou";
                    var msg = Console.ReadLine();
                    ArraySegment<byte> bytesToSend = new ArraySegment<byte>(Encoding.UTF8.GetBytes(msg));
                    await ws.SendAsync(bytesToSend, WebSocketMessageType.Text, true, CancellationToken.None);
                    //ArraySegment<byte> bytesReceived = new ArraySegment<byte>(new byte[1024]);
                    //WebSocketReceiveResult result = await ws.ReceiveAsync(bytesReceived, CancellationToken.None);
                    //var backMsg = Encoding.UTF8.GetString(bytesReceived.Array, 0, result.Count);
                    //1Console.WriteLine(backMsg);
                    //Console.WriteLine(Encoding.UTF8.GetString(bytesReceived.Array, 0, result.Count));

                }
            }
        }

    }
}
