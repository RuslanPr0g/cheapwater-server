using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.WebSockets;
using System.Threading;
using System.Threading.Tasks;

namespace WEBApi.Middleware
{
    public class WebSocketsMiddleware
    {
        private readonly RequestDelegate _next;

        public WebSocketsMiddleware(RequestDelegate next)
        {
            _next = next;
        }
        public async Task InvokeAsync(HttpContext context)
        {
            if (context.WebSockets.IsWebSocketRequest)
            {
                WebSocket webSocket = await context.WebSockets.AcceptWebSocketAsync();
                    
                await Receive(webSocket, async (result, buffer) =>
                {
                    switch (result.MessageType)
                    {
                        case WebSocketMessageType.Text:
                            //process text message
                            break;
                        case WebSocketMessageType.Binary:
                            //process binary message
                            break;
                        case WebSocketMessageType.Close:
                            //close the connection
                            break;
                    }
                });
            }
            else
            {
                await _next(context);
            }
        }
        private async Task Receive(WebSocket socket, Action<WebSocketReceiveResult, byte[]> handleMessage)
        {
            var buffer = new byte[1024 * 4];

            while (socket.State == WebSocketState.Open)
            {
                var result = await socket.ReceiveAsync(buffer: new ArraySegment<byte>(buffer),
                                                       cancellationToken: CancellationToken.None);
                handleMessage(result, buffer);
            }
        }
    }
}
