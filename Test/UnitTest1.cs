using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Net.WebSockets;
using System.Threading.Tasks;
using SharpTalkService;
using System.Threading;
using System.Text;

namespace Test
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public async void TestMethod1()
        {
            Task.Factory.StartNew(() =>
            {
                var wss = new WebSocketServer(9009);
                wss.Start();
            });
            using (var ws = new ClientWebSocket())
            {
               await ws.ConnectAsync(new Uri("ws://127.0.0.1"),CancellationToken.None);
                while (ws.State == WebSocketState.Open)
                 {
                    string msg = "aeiou";
                    ArraySegment<byte> bytesToSend = new ArraySegment<byte>(Encoding.UTF8.GetBytes(msg));
                    await ws.SendAsync(bytesToSend, WebSocketMessageType.Text, true, CancellationToken.None);
                    ArraySegment<byte> bytesReceived = new ArraySegment<byte>(new byte[1024]);
                    WebSocketReceiveResult result = await ws.ReceiveAsync(bytesReceived, CancellationToken.None);
                    var backMsg = Encoding.UTF8.GetString(bytesReceived.Array, 0, result.Count);
                    Assert.AreEqual(msg, backMsg);
                    //Console.WriteLine(Encoding.UTF8.GetString(bytesReceived.Array, 0, result.Count));

                }
            }
        }
    }
}
