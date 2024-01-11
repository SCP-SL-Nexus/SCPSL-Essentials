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
        [Description("Godmode for tutorial?.")]

        public bool GodmodeTutorial { get; set; } = false;
        [Description("Enable auto ff on Roundend? Not recomended for server the ff Enable?.")]

        public bool autofftogle { get; set; } = false;

        [Description("Active BCreport?.")]

        public bool bc_report { get; set; } = false;
        [Description("Discord Webhook ( if URL =https://discord.com/api/webhooks/ADD_YOUR_URL, command don't work )?.")]

        public string discord_webhook { get; set; } = "https://discord.com/api/webhooks/ADD_YOUR_URL";

        [Description("Active log autoban?")]

        public bool log = false;
        [Description("URL for Log for autoban and warn")]

        public string discord_webhook_autoban_warn { get; set; } = "https://discord.com/api/webhooks/ADD_YOUR_URL";
        [Description("ServerName for autoban")]

        public string ServerName = "";
    }
}