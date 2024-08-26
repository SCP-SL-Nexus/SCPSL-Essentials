using PluginAPI.Core;
using PluginAPI.Core.Attributes;
using PluginAPI.Enums;
using PluginAPI.Events;
using HarmonyLib;
using PluginAPI.Helpers;
using System.IO;
using System;
using Newtonsoft.Json.Linq;
using System.Net.Http;

namespace NWAPI_Essentials
{
    internal class Plugins
    {
        public string overwatch;
        public string Version = "v1.1.2";
        private static readonly Harmony HarmonyPatcher = new Harmony("Essentials.Github.SCPSL-Nexus");
        public static Plugins Singleton { get; private set; }

        [PluginConfig]
        public Config Config;

        [PluginPriority(LoadPriority.Medium)]
        [PluginEntryPoint("NWAPI-Essentials", "1.1.2", "Add more admin commands", "Ralsei")]
        public void LoadPlugin()
        {
            Singleton = this;
            if (!Config.IsEnabled) return;

            Log.Info($"Essentials {Version} created by SCPSL-Nexus");
            HarmonyPatcher.PatchAll();
            RegisterEvents();

            if (Config.Check) IsUpdateAvailable();
            if (Config.ov) InitializeFiles();
        }

        private void RegisterEvents()
        {
            if (Config.GodmodeTutorial) EventManager.RegisterEvents<Events.GodmodeforTutorial>(this);
            if (Config.autofftogle) EventManager.RegisterEvents<Events.autoffroggle>(this);
            if (Config.bc_report) EventManager.RegisterEvents<Events.BCreport>(this);
            if (Config.log) EventManager.RegisterEvents<Events.BanLog>(this);
            if (Config.ov) EventManager.RegisterEvents<Events.overwatch>(this);

            EventManager.RegisterEvents(this);
        }

        private void InitializeFiles()
        {
            try
            {
                string path = Path.Combine(Paths.Plugins, "Essentials-save");
                overwatch = Path.Combine(path, "overwatch.txt");

                if (!Directory.Exists(path)) Directory.CreateDirectory(path);
                if (!File.Exists(overwatch)) File.Create(overwatch).Close();
            }
            catch (Exception e)
            {
                Log.Debug(e.Message);
            }
        }

        public static bool IsUpdateAvailable()
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
                    Log.Error("Failed to fetch release information from GitHub");
                    return false;
                }
                string json = response.Content.ReadAsStringAsync().Result;
                JArray releases = JArray.Parse(json);
                foreach (JObject release in releases)
                {
                    string version = release["tag_name"].ToString();
                    if (version.CompareTo(PluginVersion) > 0)
                    {
                        Log.Info($"New version available: {version}");
                        return true;
                    }
                }
                Log.Info("Your version is up to date.");
                return false;
            }
            catch (Exception e)
            {
                Log.Error($"An error occurred while checking for updates: {e.Message}");
                return false;
            }
        }
    }
}