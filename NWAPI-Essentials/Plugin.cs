using PluginAPI.Core.Attributes;
using PluginAPI.Enums;
using PluginAPI.Events;

namespace NWAPI_Essentials
{
    internal class Plugin
    {
        public static Plugin Singletion {  get; set; }
        [PluginConfig]
        public Config Config;

        [PluginPriority(LoadPriority.Medium)]
        [PluginEntryPoint("NWAPI-Essentials", "1.0.2", "Add more admin commands", "Jevil")]

        public void LoadPlugin()
        {
            if (!Config.IsEnabled)
                return;
                Singletion = this;
            if (!Config.GodmodeTutorial)
                return;
                EventManager.RegisterEvents<Events.GodmodeforTutorial>(this);
            if (!Config.autofftogle)
                return;
                EventManager.RegisterEvents<Events.autoffroggle>(this);
        }
    }
}
