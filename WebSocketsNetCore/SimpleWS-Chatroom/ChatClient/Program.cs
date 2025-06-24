//
// Credits: https://positiwise.com/blog/websockets-in-asp-net-core
//
using System.Net.WebSockets;
using System.Text;

namespace ChatClient{

    class Program
    {
        static async Task Main(string[] args)
        {
            string name;
            Console.Write("Input name [blank or empty to terminate]: ");
            var readtext = Console.ReadLine();
            if(String.IsNullOrWhiteSpace(readtext)) return;

            name = readtext.Trim();

            Console.WriteLine("Starting WebSocket client!");

            var ws = new ClientWebSocket();
            Console.WriteLine("Connecting to server...");
            await ws.ConnectAsync(new Uri($"ws://localhost:12421/ws?name={name}"), CancellationToken.None);

            if (ws.State != WebSocketState.Open)
            {
                Console.WriteLine($"Connection error: {ws.State}");
                return;
            }
            Console.WriteLine("Connected!");

            Task sendTask = Task.Run(async () =>
            {
                while(true)
                {
                    var message = Console.ReadLine();
                    message = String.IsNullOrWhiteSpace(message) ? String.Empty : message.Trim();
                    if(String.Compare(message, "exit", StringComparison.InvariantCultureIgnoreCase) == 0) break;

                    byte[] bytes = Encoding.UTF8.GetBytes(message);
                    await ws.SendAsync(new ArraySegment<byte>(bytes), WebSocketMessageType.Text, endOfMessage: true, CancellationToken.None);
                }
            });

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

            await Task.WhenAny(sendTask, recieveTask);

            if (ws.State == WebSocketState.Closed)
            {
                await ws.CloseAsync(WebSocketCloseStatus.NormalClosure, "... closing ...", CancellationToken.None);
            }

            await Task.WhenAll(sendTask, recieveTask);
        }
    }
}
