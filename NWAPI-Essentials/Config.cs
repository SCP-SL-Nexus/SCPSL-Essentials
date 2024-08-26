using System.Collections.Generic;
using System.ComponentModel;

namespace NWAPI_Essentials
{
    internal class Config
    {
        public Config() { }

        [Description("Active Essentials?.")]
        public bool IsEnabled { get; set; } = true;

        [Description("Whether or not to show debug messages.")]
        public bool Debug { get; set; } = false;
        [Description("Active check version?")]
        public bool Check { get; set; } = true;

        [Description("Godmode for tutorial?.")]
        public bool GodmodeTutorial { get; set; } = false;

        [Description("Enable auto ff on Roundend? Not recomended for server the ff Enable?.")]
        public bool autofftogle { get; set; } = false;

        [Description("Active BCreport?.")]
        public bool bc_report { get; set; } = false;
        [Description("Active Save Overwacth?.")]
        public bool ov { get; set; } = false;
        [Description("Discord Webhook ( if URL =https://discord.com/api/webhooks/ADD_YOUR_URL, command don't work ).")]
        public string discord_webhook { get; set; } = "https://discord.com/api/webhooks/ADD_YOUR_URL";
        [Description("Discord Webhook's style ( embed and text ).")]
        public string discord_webhook_style { get; set; } = "text";

        [Description("Active log autoban?")]
        public bool log { get; set; } = false;

        [Description("URL for Log for autoban and warn")]
        public string discord_webhook_autoban_warn { get; set; } = "https://discord.com/api/webhooks/ADD_YOUR_URL";

        [Description("ServerName for autoban")]
        public string server_name { get; set; } = "";

        [Description("language ( en, ru )")]
        public string language { get; set; } = "en";
        public List<string> Ranks { get; set; } = new List<string>()
        {
            "owner",
            "admin",
            "moderator"
        };
    }
}