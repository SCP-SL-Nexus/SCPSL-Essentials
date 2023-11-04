using PluginAPI.Core.Attributes;
using PluginAPI.Core;
using PluginAPI.Enums;
using PlayerRoles;
using static RoundSummary;

namespace NWAPI_Essentials
{
    internal class Events
    {
        [PluginConfig]
        public Config Config;

        [PluginEvent(ServerEventType.PlayerChangeRole)]
        public void On(Player player, PlayerRoleBase oldRole, RoleTypeId newRole, RoleChangeReason reason)
        {
            if (!Config.Tutorialgodmode)
                return;
            if (newRole == RoleTypeId.Tutorial)
            {
                player.IsGodModeEnabled = true;
            }
            else
                player.IsGodModeEnabled = false;
        }
       [PluginEvent(ServerEventType.PlayerReport)]
        public void Report(Player player, Player target, string reason)
        {
            if (!Config.bcreport)
                return;
                        
            string message = $"{player.Nickname} {Config.bcmessage} {target.Nickname} {reason}";
            Server.Broadcast.BroadcastMessage(message);
        }
        [PluginEvent(ServerEventType.RoundEnd)]
        public void RoundEnd(LeadingTeam leading)
        {
            if (!Config.autofftoggle)
                return;

            if (Server.FriendlyFire)
            {
                Server.FriendlyFire = true;
            }
        }
        [PluginEvent(ServerEventType.RoundStart)]
        public void RoundStart()
        {
            if (!Config.autofftoggle)
                return;
            if (Server.FriendlyFire)
            {
                Server.FriendlyFire = false;
            }
        }
    }
}

