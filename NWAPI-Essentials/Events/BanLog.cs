using PluginAPI.Core.Attributes;
using PluginAPI.Core.Interfaces;
using PluginAPI.Core;
using PluginAPI.Enums;
using System;
using System.Net.Http;

namespace NWAPI_Essentials.Events
{
    internal class BanLog
    {
        [PluginConfig]
        public Config Config;
        [PluginEvent(ServerEventType.PlayerBanned)]
        public void LogBan(IPlayer player, Player bannedPlayer, string reason, Int64 duration)
        {
            if (player != null)
            {
                var serverName = Config.ServerName;
                var bannerNickname = player;

                using (var httpClient = new HttpClient())
                {
                    var payload = new
                    {
                        username = bannedPlayer.Nickname,
                        content = $"{bannerNickname.Nickname}, {bannerNickname.UserId}, {bannerNickname.IpAddress}, {serverName}, {reason}, {duration}"
                    };

                    var jsonPayload = Newtonsoft.Json.JsonConvert.SerializeObject(payload);
                    var httpContent = new StringContent(jsonPayload, System.Text.Encoding.UTF8, "application/json");

                    var responseTask = httpClient.PostAsync(Config.discord_webhook_autoban_warn, httpContent);
                    responseTask.Wait();
                }
                {
                    Log.Debug("player in null");
                }
            }
        }
    }
}
