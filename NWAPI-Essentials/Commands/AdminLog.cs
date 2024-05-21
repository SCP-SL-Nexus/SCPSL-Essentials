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
                response = config.language == "en" ? "You don't have permission to use this command! (Permission name: Overwatch)" : "У вас нет разрешения на эту команду! (Название разрешения: Overwatch)";
                return false;
            }
            string message = string.Join(" ", arguments);
            var player = Player.Get(sender);
            string user = player.Nickname;
            if (config == null)
            {
                response = config.language == "en" ? "Config is null!" : "Config равен нулю!";
                return true;
            }
            try
            {
                using (var httpClient = new HttpClient())
                {
                    var payload = new
                    {
                        content = config.discord_webhook_style == "text" ? message : null,
                        username = user,
                        embeds = config.discord_webhook_style == "embed" ? new[] { new { title = "Log", description = $"```{message}```", color = 2031871 } } : null
                    };

                    var jsonPayload = Newtonsoft.Json.JsonConvert.SerializeObject(payload);
                    var httpContent = new StringContent(jsonPayload, System.Text.Encoding.UTF8, "application/json");
                    var responseTask = httpClient.PostAsync(config.discord_webhook, httpContent);
                    responseTask.Wait();

                    response = responseTask.Result.IsSuccessStatusCode ? (config.language == "en" ? "Message sent!" : "Сообщение отправлено!") : (config.language == "en" ? "Failed to send message!" : "Не удалось отправить сообщение!");

                    return true;
                }
            }
            catch (Exception e)
            {
                Log.Debug(e.Message);
                response = config.language == "en" ? "An error occurred!" : "Произошла ошибка!";
                return true;
            }
        }
    }
}
