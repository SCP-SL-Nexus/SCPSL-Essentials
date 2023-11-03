using System.ComponentModel;
using System.Xml.Linq;

namespace NWAPI_Essentials
{
    public class Config
    {
        public Config() { }
        [Description("Active Essentials?.")]

        public bool IsEnabled { get; set; } = true;
        [Description("Whether or not to show debug messages.")]

        public bool Debug { get; set; } = false;
        [Description("Active god mod for Tutorial?.")]

        public bool Tutorialgodmode { get; set; } = false;
    }
}