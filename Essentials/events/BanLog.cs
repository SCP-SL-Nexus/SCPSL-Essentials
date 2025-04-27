using System.Net.Http;
using LabApi.Events.CustomHandlers;
using LabApi.Events.Arguments.PlayerEvents;
using LabApi.Features.Console;

namespace Essentials.Events
{
    internal class BanLog : CustomEventsHandler
    {
        public override void OnPlayerBanned(PlayerBannedEventArgs ev)
        {
            if (ev.Player == null)
            {
                Logger.Debug(Plugins.Singleton.Config.language == "en" ? "Player is null or Dedicated Server" : "Игрок равен нулю или Dedicated Server");
                return;
            }

            var config = Plugins.Singleton.Config;
            var serverName = ServerConsole.ServerName;
            var bannerNickname = ev.Player;

            using (var httpClient = new HttpClient())
            {
                var payload = new
                {
                    username = ev.Issuer.Nickname,
                    content = config.discord_webhook_style == "text" ? $"{bannerNickname.Nickname}, {bannerNickname.UserId}, {bannerNickname.IpAddress}, {serverName}, {ev.Reason}, {ev.Duration}" : null,
                    embeds = config.discord_webhook_style == "embed" ? new[] { new { title = "BanLog", description = $"```{bannerNickname.Nickname}\n {bannerNickname.UserId}\n {bannerNickname.IpAddress}\n {serverName}\n {ev.Reason}\n {ev.Duration}```", color = 2031871 } } : null,
                };

                var jsonPayload = Newtonsoft.Json.JsonConvert.SerializeObject(payload);
                var httpContent = new StringContent(jsonPayload, System.Text.Encoding.UTF8, "application/json");

                var responseTask = httpClient.PostAsync(config.discord_webhook_autoban_warn, httpContent);
                responseTask.Wait();
            }
            base.OnPlayerBanned(ev);
        }
    }
}
