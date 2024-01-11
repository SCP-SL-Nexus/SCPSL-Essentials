using PluginAPI.Core.Attributes;
using static RoundSummary;
using PluginAPI.Enums;
using PluginAPI.Core;

namespace NWAPI_Essentials.Events
{
    internal class autoffroggle
    {
        [PluginConfig]
        public Config Config;

        [PluginEvent(ServerEventType.RoundEnd)]
        public void RoundEnd(LeadingTeam leading)
        {
            Server.FriendlyFire = true;
        }
        [PluginEvent(ServerEventType.WaitingForPlayers)]
        public void RoundStart()
        {
            Server.FriendlyFire = false;
        }
    }
}