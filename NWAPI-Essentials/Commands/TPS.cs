using CommandSystem;
using System;
using PluginAPI.Core;

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
            response = $"Server TPS: {Server.TPS}";
            return true;
        }
    }
}

