//
// Credits: https://positiwise.com/blog/websockets-in-asp-net-core
//
using System.Net.WebSockets;
using System.Text;

namespace SimpleWS_Client
{
    internal class Client
    {
        static async Task Main(string[] args)
        {
            Console.WriteLine("Starting WebSocket client!");

            var ws = new ClientWebSocket();

            Console.WriteLine("Connecting to server!");
            await ws.ConnectAsync(new Uri("ws://localhost:6969/ws"),   CancellationToken.None);
            Console.WriteLine("Connected");

            Task recieveTask = Task.Run(async () => { 
                byte[] buffer = new byte[1024 * 4];
                while (true)
                {
                    WebSocketReceiveResult result = await ws.ReceiveAsync(new ArraySegment<byte>(buffer), CancellationToken.None);

                    if (result.MessageType == WebSocketMessageType.Close) 
                    {
                        break;
                    }

                    var message = Encoding.UTF8.GetString(buffer, 0, result.Count);
                    Console.WriteLine($"Received: {message}");
                }
            });

            await recieveTask;
        }
    }
}
