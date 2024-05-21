using PluginAPI.Core.Attributes;
using PluginAPI.Core;
using PluginAPI.Enums;

namespace NWAPI_Essentials.Events
{
    internal class BCreport
    {
        [PluginConfig]
        public Config Config;

        [PluginEvent(ServerEventType.PlayerReport)]
        public void Report(Player ply, Player ply2, string reason)
        {
            if (ply == null || ply2 == null)
            {
                Log.Debug("One or both players are null");
                return;
            }

            string message = Config.language == "en" ? $"<color=red>{ply.Nickname}</color> <color=yellow>reported</color> <color=red>{ply2.Nickname}</color> <color=yellow>for</color> <color=red>{reason}</color>" : $"<color=red>{ply.Nickname}</color> <color=yellow>пожаловался на</color> <color=red>{ply2.Nickname}</color> <color=yellow>за</color> <color=red>{reason}</color>";

            foreach (Player p in Player.GetPlayers())
            {
                if (p.RemoteAdminAccess)
                {
                    p.SendBroadcast(message, 9);
                }
                else
                {
                    Log.Debug("Error");
                }
            }
        }
    }
}