using PluginAPI.Core.Attributes;
using PluginAPI.Enums;
using PluginAPI.Core;
using System.Net.Http;
using PluginAPI.Core.Interfaces;
using System;

namespace NWAPI_Essentials.Events
{
    internal class BanLog
    {
        [PluginEvent(ServerEventType.PlayerBanned)]
        public void LogBan(IPlayer player, Player bannedPlayer, string reason, Int64 duration)
        {
            var config = Plugins.Singleton.Config;
            if (player != null)
            {
                var serverName = config.server_name;
                var bannerNickname = player;

                using (var httpClient = new HttpClient())
                {
                    var payload = new
                    {
                        username = bannedPlayer.Nickname,
                        content = config.discord_webhook_style == "text" ? $"{bannerNickname.Nickname}, {bannerNickname.UserId}, {bannerNickname.IpAddress}, {serverName}, {reason}, {duration}" : null,
                        embeds = config.discord_webhook_style == "embed" ? new[] { new { title = "BanLog", description = $"```{bannerNickname.Nickname}\n {bannerNickname.UserId}\n {bannerNickname.IpAddress}\n {serverName}\n {reason}\n {duration}```", color = 2031871 } } : null,
                    };

                    var jsonPayload = Newtonsoft.Json.JsonConvert.SerializeObject(payload);
                    var httpContent = new StringContent(jsonPayload, System.Text.Encoding.UTF8, "application/json");

                    var responseTask = httpClient.PostAsync(config.discord_webhook_autoban_warn, httpContent);
                    responseTask.Wait();
                }
                {
                    if (config.language == "en")
                    {
                        Log.Debug("player is null or Dedicated Server");
                    }
                    else
                    {
                        Log.Debug("Игрок равен нулю или Dedicated Server");
                    }
                }
            }
        }
    }
}