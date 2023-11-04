using System.ComponentModel;

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
        [Description("Active report player BC?")]

        public bool bcreport { get; set; } = false;
        [Description("BC message")]

        public string bcmessage { get; set; } = "Reporting";
        [Description("EnableAutofftogle?. Not recommended for servers where FF is enabled")]

        public bool autofftoggle { get; set; } = false;
    }
}