using System.Net;
using System.Net.WebSockets;
using System.Text;
using System.Threading.Tasks.Dataflow;
using Microsoft.AspNetCore.Authentication;

namespace ChatServer
{

    public class Program
    {
        private static List<WebSocket> connections = [];

        public static async Task Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            builder.WebHost.UseUrls("http://localhost:12421");
            var app = builder.Build();
            app.UseWebSockets();

            

            app.Map("/ws", async context => 
            {
                if (context.WebSockets.IsWebSocketRequest)
                {
                    var curname = context.Request.Query["name"];

                    using var ws = await context.WebSockets.AcceptWebSocketAsync();
                    
                    connections.Add(ws);

                    await Broadcast($"[from server] '{curname}' joined the room.");
                    await Broadcast($"[from server] '{connections.Count}' users connected.");
                    await ReceiveMessage(ws, async (results, buffer) => 
                    {
                        if (results.MessageType == WebSocketMessageType.Text)
                        {
                            string message = Encoding.UTF8.GetString(buffer, 0, results.Count);
                            await Broadcast($"{curname}: {message}");
                        }
                        else if (results.MessageType == WebSocketMessageType.Close || ws.State == WebSocketState.Aborted)
                        {
                            connections.Remove(ws);
                            await Broadcast($"[from server] {curname} left the room.");
                            await Broadcast($"[from server] {connections.Count} users connected.");
                            await ws.CloseAsync(results.CloseStatus.Value, results.CloseStatusDescription, CancellationToken.None);
                        }
                    });
                }
                else
                {
                    context.Response.StatusCode = (int) HttpStatusCode.BadRequest;
                }
            });

            await app.RunAsync();
        }

        public static async Task ReceiveMessage(WebSocket socket, Action<WebSocketReceiveResult, byte[]> handleMesssage)
        {
            byte[] buffer = new byte[1024 * 4];

            while (socket.State == WebSocketState.Open)
            {
                var result = await socket.ReceiveAsync(new ArraySegment<byte>(buffer), CancellationToken.None);
                handleMesssage(result, buffer);
            }
        }

        public static async Task Broadcast(string message)
        {
            byte[] bytes = Encoding.UTF8.GetBytes(message);

            foreach (WebSocket connection in connections)
            {
                if (connection.State == WebSocketState.Open)
                {
                    ArraySegment<byte> arraySegment = new(bytes, 0, bytes.Length);

                    await connection.SendAsync(arraySegment, WebSocketMessageType.Text, endOfMessage: true, CancellationToken.None);
                }
            }

        }
    }
}