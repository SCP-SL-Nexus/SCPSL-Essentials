using CommandSystem;
using PluginAPI.Core;
using PluginAPI.Core.Attributes;
using RemoteAdmin;
using System;
using System.Linq;
using System.Net.Http;
using static System.Net.Mime.MediaTypeNames;

namespace NWAPI_Essentials.Commands
{
    internal class Warn : ICommand
    {
        public static Warn Instance { get; } = new Warn();
        public string Command => "Warn";
        public string[] Aliases => new[] { "W" };
        public string Description => " Warning User for breaking rules";

        public bool Execute(ArraySegment<string> arguments, ICommandSender sender, out string response)
        {
            var config = Plugins.Singleton.Config;
            if (!sender.CheckPermission(PlayerPermissions.Overwatch))
            {
                if (config.language == "en")
                {
                    response = "You don't have permission to use this command! (Permission name: Overwatch)";
                    return false;
                }
                else
                {
                    response = "У вас нет разрешения на эту команду! (Название разрешения: Overwatch)";
                    return false;
                }
            }

            if (arguments.Count < 1)
            {
                if (config.language == "en")
                {
                    response = "Use this: PlayerID message";
                    return false;
                }
                else
                {
                    response = "Используйте так: ID игрок сообщение";
                    return false;
                }
            }

            bool parsed = int.TryParse(arguments.At(0), out int playerId);
            if (!parsed)
            {
                if (config.language == "en")
                {
                    response = "Invalid player ID provided.";
                    return false;
                }
                else
                {
                    response = "Недействительное ID игрока.";
                    return false;
                }
            }

            Player player = Player.Get(playerId);
            if (player == null)
            {
                if (config.language == "en")
                {
                    response = $"No player found with ID: {playerId}";
                    return false;
                }
                else
                {
                    response = $"Игрок с таким ID не найден: {playerId}";
                    return false;
                }
            }
            string text;
            string message = string.Join(" ", arguments.ToArray());
            var playerSender = sender as PlayerCommandSender;
            string ply = playerSender.Nickname;
            if (config.language == "en")
            {
                player.SendBroadcast($"<color=red>You received a warn for {message}", 5);
                text = $"{player.Nickname}, {player.UserId}, received a warning for {message}";
            }
            else
            {
                player.SendBroadcast($"<color=red>Вы получили предупреждение за {message}", 5);
                text = $"{player.Nickname}, {player.UserId}, получил предупреждение за {message}";
            }
            if (config != null)
            {
                using (var httpClient = new HttpClient())
                {
                    var payload = new
                    {
                        username = ply,
                        content = text,
                    };

                    var jsonPayload = Newtonsoft.Json.JsonConvert.SerializeObject(payload);
                    var httpContent = new StringContent(jsonPayload, System.Text.Encoding.UTF8, "application/json");

                    var responseTask = httpClient.PostAsync(config.discord_webhook_autoban_warn, httpContent);
                    responseTask.Wait();

                    if (responseTask.Result.IsSuccessStatusCode)
                    {
                        if (config.language == "en")
                        {
                            response = "Message sent!";
                            return true;
                        }
                        else
                        {
                            response = "Сообщение отправлено!";
                            return true;
                        }

                    }
                    else
                    {
                        if (config.language == "en")
                        {
                            response = "Failed to send message.";
                            return false;
                        }
                        else
                        {
                            response = "Не получилось отправить сообщение.";
                            return false;
                        }
                    }
                }
            }
            else
            {
                if (config.language == "en")
                {
                    response = "Config is null.";
                    return false;
                }
                else
                {
                    response = "Config равен нулю.";
                    return false;
                }
            }
        }
    }
}