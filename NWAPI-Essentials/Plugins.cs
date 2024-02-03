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
        public string Version = "v1.0.9";
        private static readonly Harmony HarmonyPatcher = new Harmony("Essentials.Github.Essentials-Team");
        public static Plugins Singleton { get; set; }
        [PluginConfig]
        public Config Config;

        [PluginPriority(LoadPriority.Medium)]
        [PluginEntryPoint("NWAPI-Essentials", "1.0.9", "Add more admin commands", "Frisk")]
        public void LoadPlugin()
        {
            if (Config.IsEnabled)
            {
                Log.Info($"Essentials {Version} created by Essentials-Team");
                HarmonyPatcher.PatchAll();
                Singleton = this;
                var config = Singleton.Config;
                if (config.Check == true)
                {
                    IsUpdateAvailable();
                }
                if (config.GodmodeTutorial == true)
                {
                    EventManager.RegisterEvents<Events.GodmodeforTutorial>(this);
                }
                if (config.autofftogle == true)
                {
                    EventManager.RegisterEvents<Events.autoffroggle>(this);
                }
                if (config.bc_report == true)
                {
                    EventManager.RegisterEvents<Events.BCreport>(this);
                }
                if (config.log == true)
                {
                    EventManager.RegisterEvents<Events.BanLog>(this);
                }
                if (config.ov == true)
                {
                    Files();
                    EventManager.RegisterEvents<Events.overwatch>(this);
                }
                EventManager.RegisterEvents(this);
            }
            EventManager.RegisterEvents(this);
        }
        public void Files()
        {
            try
            {
                string appData = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
                string pluginPath = Path.Combine(appData, "NWAPI-Essential");
                string path = Path.Combine(Paths.Plugins, "Essentials-save");
                string overwatchfile = Path.Combine(path, "overwatch.txt");
                if (!Directory.Exists(path))
                    Directory.CreateDirectory(path);
                if (!File.Exists(overwatchfile))
                    File.Create(overwatchfile).Close();
                overwatch = overwatchfile;
            }
            catch (Exception e)
            {
                Log.Debug($"{e.Message}");
            }
        }
        public static bool IsUpdateAvailable()
        {
            const string PluginVersion = "1.0.9";
            const string RepositoryUrl = "https://api.github.com/repos/SCP-SLEssentials-Team/SCPSL-Essentials/releases";
            try
            {
                HttpClient client = new HttpClient();
                client.DefaultRequestHeaders.Add("User-Agent", "request");
                HttpResponseMessage response = client.GetAsync(RepositoryUrl).Result;

                if (response.IsSuccessStatusCode)
                {
                    string json = response.Content.ReadAsStringAsync().Result;
                    JArray releases = JArray.Parse(json);

                    foreach (JObject release in releases)
                    {
                        string version = release["tag_name"].ToString();
                        if (version.CompareTo(PluginVersion) > 0)
                        {
                            Log.Info("New version is available: " + version);
                            return true;
                        }
                    }
                    Log.Info("Your version is up to date.");
                    return false;
                }
                else
                {
                    Log.Error("Failed to fetch release information from GitHub");
                }
            }
            catch (Exception e)
            {
                Log.Error("An error occurred while checking for updates: " + e.Message);
            }

            return false;
        }
    }
}
