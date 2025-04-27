using System;
using System.Net;
using System.Net.WebSockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using DiscordBot_Essentials;
using LabApi.Features.Wrappers;

public class WebSocketServer
{
    private static HttpListener _listener = new HttpListener();
    private static WebSocket _botSocket;

    public static async Task StartServer()
    {
        var config = Plugins.Singleton.Config;
        _listener.Prefixes.Add($"http://localhost:{config.Port}/");
        _listener.Start();

        while (true)
        {
            var context = await _listener.GetContextAsync();
            if (context.Request.IsWebSocketRequest)
            {
                var wsContext = await context.AcceptWebSocketAsync(null);
                _botSocket = wsContext.WebSocket;
                _ = HandleClient(_botSocket);
            }
        }
    }

    private static async Task HandleClient(WebSocket socket)
    {
        byte[] buffer = new byte[1024];

        while (socket.State == WebSocketState.Open)
        {
            var result = await socket.ReceiveAsync(new ArraySegment<byte>(buffer), CancellationToken.None);
            string message = Encoding.UTF8.GetString(buffer, 0, result.Count);
            ServerConsole.EnterCommand(message);
        }
    }

    public static async Task SendPlayerCount()
    {
        if (_botSocket == null || _botSocket.State != WebSocketState.Open)
            return;

        int players = Player.List.Count;
        int maxPlayers = Server.MaxPlayers;
        string data = $"players:{players}/{maxPlayers}";

        byte[] buffer = Encoding.UTF8.GetBytes(data);
        await _botSocket.SendAsync(new ArraySegment<byte>(buffer), WebSocketMessageType.Text, true, CancellationToken.None);
    }
}