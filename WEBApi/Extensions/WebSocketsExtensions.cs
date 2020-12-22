using Microsoft.AspNetCore.Builder;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.WebSockets;
using System.Threading;
using System.Threading.Tasks;
using WEBApi.Middleware;

namespace WEBApi.Extensions
{
    public static class WebSocketsExtensions
    {
        public static IApplicationBuilder UseWebSocketsServer(this IApplicationBuilder app)
        {
            app.UseMiddleware<WebSocketsMiddleware>();
            return app;
        }
    }
}
