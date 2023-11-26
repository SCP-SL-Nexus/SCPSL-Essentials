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
            switch (leading)
            {
                case LeadingTeam.Anomalies:
                    Server.FriendlyFire = true;
                    break;
                case LeadingTeam.ChaosInsurgency:
                    Server.FriendlyFire = true;
                    break;
                case LeadingTeam.FacilityForces:
                    Server.FriendlyFire = true;
                    break;
                case LeadingTeam.Draw:
                    Server.FriendlyFire = true;
                    break;
                default:
                    break;
            }
        }
        [PluginEvent(ServerEventType.RoundStart)]
        public void RoundStart()
        {
            if (Server.FriendlyFire)
            {
                Server.FriendlyFire = false;
            }
        }
    }
}
