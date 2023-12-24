using PluginAPI.Core.Attributes;
using PluginAPI.Core;
using PluginAPI.Enums;

namespace NWAPI_Essentials.Events
{
    public class BCreport
    {
        [PluginConfig]
        public Config Config;

        [PluginEvent(ServerEventType.PlayerReport)]
        public void Report(Player ply, Player ply2, string reason)
        {
            if (ply != null && ply2 != null)
            {
                foreach (Player p in Player.GetPlayers())
                {
                    if (p.RemoteAdminAccess == true)
                    {
                        p.SendBroadcast($"<color=red> {ply.Nickname} <color=yellow> Reported on</color> <color=red> {ply2.Nickname} </color><color=yellow>for</color> <color=red> {reason}" , 9);
                    }
                    else
                    {
                    }
                }
            }
            else
            {
                Log.Debug("One or both players are null");
            }
        }
    }
}