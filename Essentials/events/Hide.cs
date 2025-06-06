using LabApi.Events.Arguments.PlayerEvents;
using LabApi.Events.Arguments.ServerEvents;
using LabApi.Events.CustomHandlers;
using LabApi.Features.Console;
using LabApi.Features.Wrappers;
using System;
using System.Collections.Generic;
using System.IO;

namespace Essentials.Events
{
    internal class Hide : CustomEventsHandler
    {
        public override void OnPlayerJoined(PlayerJoinedEventArgs ev)
        {
            try
            {
                var plugin = Plugins.Singleton;
                if (File.ReadAllText(plugin.overwatch).Contains(ev.Player.UserId))
                {
                    ev.Player.SetRole(PlayerRoles.RoleTypeId.Overwatch);
                }
                if (File.ReadAllText(plugin.tag).Contains(ev.Player.UserId))
                {
                    ev.Player.ReferenceHub.serverRoles.TryHideTag();
                }
            }
            catch (Exception e)
            {
                Logger.Debug(e.Message);
            }
            base.OnPlayerJoined(ev);
        }
        public override void OnServerRoundEnded(RoundEndedEventArgs ev)
        {
            try
            {
                var plugin = Plugins.Singleton;
                var hidetagFilePath = plugin.tag;
                var hidetagList = new HashSet<string>(File.ReadAllLines(hidetagFilePath));
                foreach (var player in Player.List)
                {
                    var userId = player.UserId;
                    if (player.ReferenceHub.serverRoles.HasBadgeHidden == true)
                        hidetagList.Add(userId);
                    else
                        hidetagList.Remove(userId);

                }
                File.WriteAllLines(hidetagFilePath, hidetagList);
            }
            catch (Exception e)
            {
                Logger.Debug(e.Message);
            }
            base.OnServerRoundEnded(ev);
        }
        public override void OnPlayerChangedRole(PlayerChangedRoleEventArgs ev)
        {
            try
            {
                var plugin = Plugins.Singleton;
                var overwatchFilePath = plugin.overwatch;
                var overwatchList = new HashSet<string>(File.ReadAllLines(overwatchFilePath));
                if (ev.Player.IsOverwatchEnabled)
                    overwatchList.Add(ev.Player.UserId);
                else
                    overwatchList.Remove(ev.Player.UserId);
                File.WriteAllLines(overwatchFilePath, overwatchList);
            }
            catch (Exception e)
            {
                Logger.Debug(e.Message);
            }
            base.OnPlayerChangedRole(ev);
        }
    }
}