using PluginAPI.Core.Attributes;
using PluginAPI.Enums;
using PluginAPI.Events;
using System.IO;
using PluginAPI.Core;
using System;
using System.Collections.Generic;
using static RoundSummary;

namespace NWAPI_Essentials.Events
{
    internal class overwatch
    {
        [PluginEvent(ServerEventType.PlayerJoined)]
        public void OnPlayerJoined(PlayerJoinedEvent ev)
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
                Log.Debug(e.Message);
            }
        }
        [PluginEvent(ServerEventType.RoundEnd)]
        public void OnRoundEnd(LeadingTeam leading)
        {
            try
            {
                var plugin = Plugins.Singleton;
                var overwatchFilePath = plugin.overwatch;
                var overwatchList = new HashSet<string>(File.ReadAllLines(overwatchFilePath));
                foreach (var player in Player.GetPlayers())
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
                    Log.Debug($"{userId} is in overwatch.");
                }
            }
            catch (Exception e)
            {
                Log.Debug(e.Message);
            }
        }
    }
}