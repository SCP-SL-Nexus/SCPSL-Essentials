using HarmonyLib;
using System.IO;
using System;
using Newtonsoft.Json.Linq;
using System.Net.Http;
using LabApi.Features.Console;
using LabApi.Loader.Features.Plugins;
using LabApi.Events.CustomHandlers;
using NWAPI_Essentials.Events;

namespace NWAPI_Essentials
{
    internal class Plugins : Plugin<Config>
    {
        public string overwatch;

        private static readonly Harmony HarmonyPatcher = new Harmony("Essentials.Github.SCPSL-Nexus");
        public static Plugins Singleton { get; private set; }

        public override string Name => "NWAPI-Essentials";

        public override string Description => "Add more admin commands";

        public override string Author => "Ralsei";

        public override Version Version => new Version(1, 1, 2);

        public override Version RequiredApiVersion => new Version(0, 4, 0, 0);

        public autoffroggle autoffroggle = new autoffroggle();
        public BanLog BanLog => new BanLog();
        public BCreport BCreport => new BCreport();
        public GodmodeforTutorial GodmodeforTutorial => new GodmodeforTutorial();
        public overwatch overwatch1 = new overwatch();
        public AntiSCPToggle ant = new AntiSCPToggle();
        public override void Enable()
        {
            Singleton = this;
            if (!Config.IsEnabled) return;

            Logger.Info($"Essentials {Version} created by SCPSL-Nexus");
            HarmonyPatcher.PatchAll();
            if (Config.GodmodeTutorial) CustomHandlersManager.RegisterEventsHandler(GodmodeforTutorial);
            if (Config.autofftogle) CustomHandlersManager.RegisterEventsHandler(autoffroggle);
            if (Config.bc_report) CustomHandlersManager.RegisterEventsHandler(BCreport);
            if (Config.log) CustomHandlersManager.RegisterEventsHandler(BanLog);
            if (Config.ov) CustomHandlersManager.RegisterEventsHandler(overwatch1);
            if (Config.antiscp) CustomHandlersManager.RegisterEventsHandler(ant);
            if (Config.Check) IsUpdateAvailable();
            if (Config.ov) InitializeFiles();
        }
        public override void Disable()
        {
            if (Config.GodmodeTutorial) CustomHandlersManager.UnregisterEventsHandler(GodmodeforTutorial);
            if (Config.autofftogle) CustomHandlersManager.UnregisterEventsHandler(autoffroggle);
            if (Config.bc_report) CustomHandlersManager.UnregisterEventsHandler(BCreport);
            if (Config.log) CustomHandlersManager.UnregisterEventsHandler(BanLog);
            if (Config.ov) CustomHandlersManager.UnregisterEventsHandler(overwatch1);
        }
        private void InitializeFiles()
        {
            try
            {
                string path = Path.Combine(LabApi.Loader.Features.Paths.PathManager.Configs.ToString(), "Essentials-save");
                overwatch = Path.Combine(path, "overwatch.txt");

                if (!Directory.Exists(path)) Directory.CreateDirectory(path);
                if (!File.Exists(overwatch)) File.Create(overwatch).Close();
            }
            catch (Exception e)
            {
                Logger.Debug(e.Message);
            }
        }

        private static bool IsUpdateAvailable()
        {
            const string PluginVersion = "1.1.2";
            const string RepositoryUrl = "https://api.github.com/repos/SCP-SLEssentials-Team/SCPSL-Essentials/releases";

            try
            {
                using HttpClient client = new HttpClient();
                client.DefaultRequestHeaders.Add("User-Agent", "request");
                HttpResponseMessage response = client.GetAsync(RepositoryUrl).Result;
                if (!response.IsSuccessStatusCode)
                {
                    Logger.Error("Failed to fetch release information from GitHub");
                    return false;
                }
                string json = response.Content.ReadAsStringAsync().Result;
                JArray releases = JArray.Parse(json);
                foreach (JObject release in releases)
                {
                    string version = release["tag_name"].ToString();
                    if (version.CompareTo(PluginVersion) > 0)
                    {
                        Logger.Info($"New version available: {version}");
                        return true;
                    }
                }
                Logger.Info("Your version is up to date.");
                return false;
            }
            catch (Exception e)
            {
                Logger.Error($"An error occurred while checking for updates: {e.Message}");
                return false;
            }
        }
    }
}