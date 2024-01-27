using PluginAPI.Core.Attributes;
using PluginAPI.Enums;
using PluginAPI.Events;

namespace NWAPI_Essentials.Events
{
    internal class Welcome
    {
        [PluginEvent(ServerEventType.PlayerJoined)]
        public void Join(PlayerJoinedEvent ev)
        {
            var config = Plugins.Singleton.Config;
            ev.Player.SendBroadcast(config.Welcome_bc_meg, (ushort)config.Welcome_bc_dur);
        }
    }
}
