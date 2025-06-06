using System.Collections.Generic;
using System.ComponentModel;

namespace Essentials
{
    internal class Config
    {
        [Description("Active check version?")]
        public bool Check { get; set; } = true;

        [Description("Godmode for tutorial?.")]
        public bool GodmodeTutorial { get; set; } = false;

        [Description("Enable auto ff on Roundend? Not recomended for server the ff Enable?.")]
        public bool autofftogle { get; set; } = false;

        [Description("Active BCreport?.")]
        public bool bc_report { get; set; } = false;
        [Description("Active Save Overwacth & Tag?")]
        public bool save { get; set; } = false;
        [Description("Active AntiSCP ( do not track 173 and 096 on Tutorial )?.")]
        public bool antiscp { get; set; } = false;
        [Description("Discord Webhook ( if URL =https://discord.com/api/webhooks/ADD_YOUR_URL, command don't work ).")]
        public string discord_webhook { get; set; } = "https://discord.com/api/webhooks/ADD_YOUR_URL";
        [Description("Discord Webhook's style ( embed and text ).")]
        public string discord_webhook_style { get; set; } = "text";

        [Description("Active log autoban?")]
        public bool log { get; set; } = false;

        [Description("URL for Log for autoban and warn")]
        public string discord_webhook_autoban_warn { get; set; } = "https://discord.com/api/webhooks/ADD_YOUR_URL";

        [Description("language ( en, ru )")]
        public string language { get; set; } = "en";
        [Description("Enter Visible name ranks for show BC report and other staff.")]
        public List<string> Ranks { get; set; } = new List<string>()
        {
            "owner",
            "admin",
            "moderator"
        };
        [Description("Custom color for embed")]
        public int color = 2031871;
    }
}