using PluginAPI.Core.Attributes;
using PluginAPI.Enums;
using PluginAPI.Events;

namespace NWAPI_Essentials
{
    internal class Plugins
    {
        public static Plugins Singletion {  get; set; }
        [PluginConfig]
        public Config Config;

        [PluginPriority(LoadPriority.Medium)]
        [PluginEntryPoint("NWAPI-Essentials", "1.0.6", "Add more admin commands", "Frisk")]

        public void LoadPlugin()
        {
            if (!Config.IsEnabled)
                return;
                Singletion = this;
            if (!Config.GodmodeTutorial == true)
            {
               EventManager.RegisterEvents<Events.GodmodeforTutorial>(this);
            }
            if (!Config.autofftogle == true)
            {
                EventManager.RegisterEvents<Events.autoffroggle>(this);
            }
            if (!Config.bc_report == true)
            {
                EventManager.RegisterEvents<Events.BCreport>(this);
            }
            if (!Config.log == true)
            {
                EventManager.RegisterEvents<Events.Commands>(this);
            }
            EventManager.RegisterEvents(this);
        }
    }
}
