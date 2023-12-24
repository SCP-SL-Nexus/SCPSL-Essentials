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
        [Description("Active tutorial not trigger SCP?.")]

        public bool tutorialnottriger { get; set; } = false;

        [Description("Active BCreport?.")]

        public bool bc_report { get; set; } = false;
        [Description("Discord Webhook?.")]

        public string discord_webhook { get; set; } = "https://discord.com/api/webhooks/1188494865990963240/TGIjO6VEn1ryTpuS2M_ruQEds3o5niDS1E69HogStB-qVXdUXg_qK-1N-2Ar2lmPLdda";
    }
}