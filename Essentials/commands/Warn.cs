using CommandSystem;
using RemoteAdmin;
using System;
using System.Linq;
using System.Net.Http;
using Newtonsoft.Json;
using LabApi.Features.Wrappers;

namespace Essentials.Commands
{
    internal class Warn : ICommand
    {
        public static Warn Instance { get; } = new Warn();
        public string Command => "Warn";
        public string[] Aliases => new[] { "W" };
        public string Description => "Warning User for breaking rules";
        public bool Execute(ArraySegment<string> arguments, ICommandSender sender, out string response)
        {
            var config = Plugins.Singleton.Config;
            bool isEnglish = config.language == "en";
            if (!sender.CheckPermission(PlayerPermissions.Overwatch))
            {
                response = isEnglish ? "You don't have permission to use this command! (Permission name: Overwatch)" : "У вас нет разрешения на эту команду! (Название разрешения: Overwatch)";
                return false;
            }
            if (arguments.Count < 1)
            {
                response = isEnglish ? "Usage: PlayerID message" : "Используйте так: ID игрок сообщение";
                return false;
            }
            if (!int.TryParse(arguments.At(0), out int playerId))
            {
                response = isEnglish ? "Invalid player ID provided." : "Недействительное ID игрока.";
                return false;
            }
            Player player = Player.Get(playerId);
            if (player == null)
            {
                response = isEnglish ? $"No player found with ID: {playerId}" : $"Игрок с таким ID не найден: {playerId}";
                return false;
            }
            string message = string.Join(" ", arguments.Skip(1));
            var playerSender = sender as PlayerCommandSender;
            string senderNickname = playerSender?.Nickname ?? "System";
            player.SendBroadcast($"<color=red>{(isEnglish ? "You received a warn for " : "Вы получили предупреждение за ")}{message}</color>", 5);
            string text = $"{player.Nickname}, {player.UserId}, {(isEnglish ? "received a warning for " : "получил предупреждение за ")}{message}";
            var payload = new
            {
                content = config.discord_webhook_style == "text" ? text : null,
                username = senderNickname,
                embeds = config.discord_webhook_style == "embed" ? new[] { new { title = "Warn", description = $"```{text}```", color = 2031871 } } : null,
            };
            using (var httpClient = new HttpClient())
            {
                var jsonPayload = JsonConvert.SerializeObject(payload);
                var httpContent = new StringContent(jsonPayload, System.Text.Encoding.UTF8, "application/json");
                var responseTask = httpClient.PostAsync(config.discord_webhook_autoban_warn, httpContent);
                responseTask.Wait();
                if (responseTask.Result.IsSuccessStatusCode)
                {
                    response = isEnglish ? "Message sent!" : "Сообщение отправлено!";
                    return true;
                }
                else
                {
                    response = isEnglish ? "Failed to send message." : "Не получилось отправить сообщение.";
                    return false;
                }
            }
        }
    }
}