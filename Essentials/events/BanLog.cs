using LabApi.Events.Arguments.PlayerEvents;
using LabApi.Events.Arguments.ServerEvents;
using LabApi.Events.CustomHandlers;
using LabApi.Features.Wrappers;
using System.Net.Http;
using System.Text.RegularExpressions;

namespace Essentials.Events
{
    internal class BanLog : CustomEventsHandler
    {
        private string CleanServerName(string rawName)
        {
            string cleanName = Regex.Replace(rawName, "<.*?>", string.Empty);
            int exiledIndex = cleanName.IndexOf("<color=#00000000>");
            if (exiledIndex >= 0)
            {
                cleanName = cleanName.Substring(0, exiledIndex);
            }
            cleanName = Regex.Replace(cleanName, "<.*?>", string.Empty);
            return cleanName.Trim();
        }
        public override void OnPlayerBanning(PlayerBanningEventArgs ev)
        {
            var config = Plugins.Singleton.Config;
            var serverName = CleanServerName(ServerConsole.ServerName);
            var bannerNickname = ev.Player;

            using (var httpClient = new HttpClient())
            {
                var payload = new
                {
                    username = ev.Issuer.Nickname,
                    content = config.discord_webhook_style == "text" ? $"{bannerNickname.Nickname}, {bannerNickname.UserId}, {bannerNickname.IpAddress}, {serverName}, {ev.Reason}, {ev.Duration}" : null,
                    embeds = config.discord_webhook_style == "embed" ? new[] { new { title = "BanLog", description = $"```{bannerNickname.Nickname}\n {bannerNickname.UserId}\n {bannerNickname.IpAddress}\n {serverName}\n {ev.Reason}\n {ev.Duration}```", color = config.color } } : null,
                };

                var jsonPayload = Newtonsoft.Json.JsonConvert.SerializeObject(payload);
                var httpContent = new StringContent(jsonPayload, System.Text.Encoding.UTF8, "application/json");

                var responseTask = httpClient.PostAsync(config.discord_webhook_autoban_warn, httpContent);
                responseTask.Wait();
            }
            base.OnPlayerBanning(ev);
        }
    }
}
