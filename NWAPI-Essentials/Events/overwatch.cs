using System.IO;
using System;
using System.Collections.Generic;
using LabApi.Events.CustomHandlers;
using LabApi.Events.Arguments.PlayerEvents;
using LabApi.Features.Console;
using LabApi.Events.Arguments.ServerEvents;
using LabApi.Features.Wrappers;

namespace NWAPI_Essentials.Events
{
    internal class overwatch : CustomEventsHandler
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
                var overwatchFilePath = plugin.overwatch;
                var overwatchList = new HashSet<string>(File.ReadAllLines(overwatchFilePath));
                foreach (var player in Player.List)
                {
                    var userId = player.UserId;
                    if (player.IsOverwatchEnabled)
                    {
                        overwatchList.Add(userId);
                    }
                    else
                    {
                        overwatchList.Remove(userId);
                    }
                }
                File.WriteAllLines(overwatchFilePath, overwatchList);
                foreach (var userId in overwatchList)
                {
                    Logger.Debug($"{userId} is in overwatch.");
                }
            }
            catch (Exception e)
            {
                Logger.Debug(e.Message);
            }
            base.OnServerRoundEnded(ev);
        }
    }
}