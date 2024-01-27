using CommandSystem;
using System;
using System.Linq;
using System.Net.Http;
using PluginAPI.Core;

namespace NWAPI_Essentials.Commands
{
    internal class AdminLog : ICommand
    {
        public static AdminLog Instance { get; } = new AdminLog();
        public string Command { get; } = "Log";
        public string[] Aliases { get; } = { "L" };
        public string Description { get; } = "Logs for SCPSL";
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
            string message = string.Join(" ", arguments.ToArray());
            var pl = Player.Get(sender);
            string user = pl.Nickname;
            if (config != null)
            {
                using (var httpClient = new HttpClient())
                {
                    var payload = new
                    {
                        content = config.discord_webhook_style == "text" ? message : null,
                        username = user,
                        embeds = config.discord_webhook_style == "embed" ? new[] { new { title = "Log", description = $"```{message}```", color = 2031871 } } : null,
                    };
                    var jsonPayload = Newtonsoft.Json.JsonConvert.SerializeObject(payload);
                    var httpContent = new StringContent(jsonPayload, System.Text.Encoding.UTF8, "application/json");
                    var responseTask = httpClient.PostAsync(config.discord_webhook, httpContent);
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
                            response = "Failed to sent message!";
                            return true;
                        }
                        else
                        {
                            response = "Не удалось отправить сообщение!";
                            return true;
                        }
                    }
                }
            }
            else
            {
                if (config.language == "en")
                {
                    response = "Config is null!";
                    return true;
                }
                else
                {
                    response = "Config равен нулю!";
                    return true;
                }
            }
        }
    }
}
