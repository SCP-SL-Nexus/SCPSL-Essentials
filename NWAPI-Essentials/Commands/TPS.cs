using CommandSystem;
using System;
using LabApi.Features.Wrappers;

namespace NWAPI_Essentials.Commands
{
    internal class TPS : ICommand
    {
        public static TPS Instance { get; } = new TPS();
        public string Command { get; } = "TPS";
        public string[] Aliases { get; } = { "t" };
        public string Description { get; } = "Show you a server TPS";

        public bool Execute(ArraySegment<string> arguments, ICommandSender sender, out string response)
        {
            var config = Plugins.Singleton.Config;
            var lang = config.language;
            response = lang == "en" ? $"Server TPS: {Server.TPS}" : $"TPS Сервера: {Server.TPS}";
            return false;
        }
    }
}

