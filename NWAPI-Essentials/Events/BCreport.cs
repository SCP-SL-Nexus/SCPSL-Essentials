using PluginAPI.Core.Attributes;
using PluginAPI.Core;
using PluginAPI.Enums;

namespace NWAPI_Essentials.Events
{
    internal class BCreport
    {
        [PluginEvent(ServerEventType.PlayerReport)]
        public void ReportPlayer(Player ply, Player ply2, string reason)
        {
            var config = Plugins.Singleton.Config;
            if (ply == null || ply2 == null)
            {
                Log.Debug("One or both players are null");
                return;
            }

            string message = config.language == "en" ? $"<color=red>{ply.Nickname}</color> <color=yellow>reported</color> <color=red>{ply2.Nickname}</color> <color=yellow>for</color> <color=red>{reason}</color>" : $"<color=red>{ply.Nickname}</color> <color=yellow>пожаловался на</color> <color=red>{ply2.Nickname}</color> <color=yellow>за</color> <color=red>{reason}</color>";
            foreach (Player p in Player.GetPlayers())
            {
                foreach (var t in config.Ranks)
                {
                    if (p.ReferenceHub.serverRoles.Network_myText == t)
                    {
                        p.SendBroadcast(message, 9);
                    }
                }
            }
        }

        [PluginEvent(ServerEventType.PlayerCheaterReport)]
        public void ReportCheater(Player ply, Player ply2, string reason)
        {
            var config = Plugins.Singleton.Config;
            if (ply == null || ply2 == null)
            {
                Log.Debug("One or both players are null");
                return;
            }

            string message = config.language == "en" ? $"<color=red>{ply.Nickname}</color> <color=yellow>reported</color> <color=red>{ply2.Nickname}</color> <color=yellow>for</color> <color=red>{reason}</color>" : $"<color=red>{ply.Nickname}</color> <color=yellow>пожаловался на</color> <color=red>{ply2.Nickname}</color> <color=yellow>за</color> <color=red>{reason}</color>";
            foreach (Player p in Player.GetPlayers())
            {
                foreach (var t in config.Ranks)
                {
                    if (p.ReferenceHub.serverRoles.Network_myText == t)
                    {
                        p.SendBroadcast(message, 9);
                    }
                }
            }
        }
    }
}