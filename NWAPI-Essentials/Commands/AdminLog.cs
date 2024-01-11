using CommandSystem;
using System;
using System.Linq;
using RemoteAdmin;
using System.Net.Http;
using PluginAPI.Core.Attributes;

namespace NWAPI_Essentials.Commands
{
    internal class AdminLog : ICommand
    {
        [PluginConfig]
        public Config Config;
        public AdminLog()
        {
            Config = new Config();
        }
        public static AdminLog Instance { get; } = new AdminLog();
        public string Command { get; } = "Log";
        public string[] Aliases { get; } = { "L" };
        public string Description { get; } = "Logs for SCPSL";

        public bool Execute(ArraySegment<string> arguments, ICommandSender sender, out string response)
        {
            if (!sender.CheckPermission(PlayerPermissions.Overwatch))
            {
                response = "You don't have permission to use this command! (Permission name: Overwatch)";
                return false;
            }

            string message = string.Join(" ", arguments.ToArray());
            var playerSender = sender as PlayerCommandSender;
            var user = playerSender.Nickname;

            using (var httpClient = new HttpClient())
            {
                var payload = new
                {
                    content = message,
                    username = user,
                };

                var jsonPayload = Newtonsoft.Json.JsonConvert.SerializeObject(payload);
                var httpContent = new StringContent(jsonPayload, System.Text.Encoding.UTF8, "application/json");

                var responseTask = httpClient.PostAsync(Config.discord_webhook, httpContent);
                responseTask.Wait();

                if (responseTask.Result.IsSuccessStatusCode)
                {
                    response = "Message sent!";
                    return true;
                }
                else
                {
                    response = "Failed to send message.";
                    return false;
                }
            }
        }
    }
}

