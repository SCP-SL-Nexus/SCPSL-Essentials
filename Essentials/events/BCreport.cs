using LabApi.Events.Arguments.PlayerEvents;
using LabApi.Events.CustomHandlers;
using LabApi.Features.Console;
using LabApi.Features.Wrappers;
using System.Text.RegularExpressions;

namespace Essentials.Events
{
    internal class BCreport : CustomEventsHandler
    {
        public override void OnPlayerReportedPlayer(PlayerReportedPlayerEventArgs ev)
        {
            var config = Plugins.Singleton.Config;
            if (ev.Player == null || ev.Target == null)
            {
                Logger.Debug("One or both players are null");
                return;
            }

            string message = config.language == "en" ? $"<color=red>{ev.Player.Nickname}</color> <color=yellow>reported</color> <color=red>{ev.Target.Nickname}</color> <color=yellow>for</color> <color=red>{ev.Reason}</color>" : $"<color=red>{ev.Player.Nickname}</color> <color=yellow>пожаловался на</color> <color=red>{ev.Target.Nickname}</color> <color=yellow>за</color> <color=red>{ev.Reason}</color>";
            foreach (Player p in Player.List)
            {
                foreach (var t in config.Ranks)
                {
                    if (p.UserGroup.Name == t)
                    {
                        p.SendBroadcast(message, 9);
                    }
                }
            }
            base.OnPlayerReportedPlayer(ev);
        }

        public override void OnPlayerReportedCheater(PlayerReportedCheaterEventArgs ev)
        {
            var config = Plugins.Singleton.Config;
            if (ev.Player == null || ev.Target == null)
            {
                Logger.Debug("One or both players are null");
                return;
            }

            string message = config.language == "en" ? $"<color=red>{ev.Player.Nickname}</color> <color=yellow>reported</color> <color=red>{ev.Target.Nickname}</color> <color=yellow>for</color> <color=red>{ev.Reason}</color>" : $"<color=red>{ev.Player.Nickname}</color> <color=yellow>пожаловался на</color> <color=red>{ev.Target.Nickname}</color> <color=yellow>за</color> <color=red>{ev.Reason}</color>";
            foreach (Player p in Player.List)
            {
                foreach (var t in config.Ranks)
                {
                    if (p.UserGroup.Name == t)
                    {
                        p.SendBroadcast(message, 9);
                    }
                }
            }
            base.OnPlayerReportedCheater(ev);
        }
    }
}