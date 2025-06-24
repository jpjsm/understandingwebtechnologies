//
// Credits: https://positiwise.com/blog/websockets-in-asp-net-core
//


using System.Net;
using System.Security.Cryptography;
using System.Text;
using System.Net.WebSockets;

namespace SimpleWS_Server
{
    public class Server
    {
        public static async Task Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            builder.WebHost.UseUrls("http://localhost:6969");
            var app = builder.Build();
            app.UseWebSockets();
            app.MapGet("/ws", async context => {
                if (context.WebSockets.IsWebSocketRequest)
                {
                    using var ws = await context.WebSockets.AcceptWebSocketAsync();
                    while (true)
                    {
                        string? message = $"The local time is '{DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")}' @ {TimeZoneInfo.Local.DisplayName}";
                        byte[] bytes = Encoding.UTF8.GetBytes(message);
                        ArraySegment<byte> arraySegment = new ArraySegment<byte>(bytes, 0, bytes.Length);
                        if (ws.State == WebSocketState.Open)
                        {
                            await ws.SendAsync(arraySegment, WebSocketMessageType.Text, endOfMessage: true, CancellationToken.None);
                        }
                        else if (ws.State == WebSocketState.Closed || ws.State == WebSocketState.Aborted)
                        {
                            break;
                        }

                        Thread.Sleep(1000);
                    }
                }
                else
                {
                    context.Response.StatusCode = (int)HttpStatusCode.BadRequest;
                }
            });

            await app.RunAsync();
        }
    }
}
