using PluginAPI.Core.Attributes;
using PluginAPI.Enums;
using PluginAPI.Events;
using System.IO;
using PluginAPI.Core;
using System;
using static RoundSummary;
using System.Collections.Generic;
using System.Linq;

namespace NWAPI_Essentials.Events
{
    internal class overwatch
    {
        [PluginEvent(ServerEventType.PlayerJoined)]
        public void Join(PlayerJoinedEvent ev)
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
                Log.Debug($"{e.Message}");
            }
        }
        [PluginEvent(ServerEventType.RoundEnd)]
        public void RoundEnd(LeadingTeam leading)
        {
            try
            {
                var plugin = Plugins.Singleton;
                List<string> overwatchRead = File.ReadAllLines(plugin.overwatch).ToList();
                foreach (Player player in Player.GetPlayers())
                {
                    string userId = player.UserId;

                    if (player.IsOverwatchEnabled && !overwatchRead.Contains(userId))
                        overwatchRead.Add(userId);
                    else if (!player.IsOverwatchEnabled && overwatchRead.Contains(userId))
                        overwatchRead.Remove(userId);
                }
                foreach (string s in overwatchRead)
                    Log.Debug($"{s} is in overwatch.");
                File.WriteAllLines(plugin.overwatch, overwatchRead);
            }
            catch (Exception e)
            {
                Log.Debug($"{e.Message}");
            }
        }
    }
}
