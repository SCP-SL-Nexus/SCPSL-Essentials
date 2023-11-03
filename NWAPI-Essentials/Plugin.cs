using PluginAPI.Core;
using PluginAPI.Core.Attributes;
using PluginAPI.Enums;
using PluginAPI.Events;
using System;
using System.IO;

namespace NWAPI_Essentials
{
    internal class Plugin
    {
        public static Plugin Singletion {  get; set; }
        [PluginConfig]
        public Config Config;

        [PluginPriority(LoadPriority.Medium)]
        [PluginEntryPoint("NW-Essentials", "1.0.0", "Add more admin commands", "Jevil")]

        public void LoadPlugin()
        {
            if (!Config.IsEnabled)
                return;
                Singletion = this;
                EventManager.RegisterEvents<Events>(this);
        }
    }
}
