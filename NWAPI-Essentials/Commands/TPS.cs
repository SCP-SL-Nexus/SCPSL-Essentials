using CommandSystem.Commands.RemoteAdmin.MutingAndIntercom;
using CommandSystem;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PlayerRoles;
using PluginAPI.Core;

namespace NWAPI_Essentials.Commands
{
    internal class TPS : ICommand
    {
        public static TPS Instance { get; } = new TPS();
        public string Command { get; } = "TPS";
        public string[] Aliases { get; } = Array.Empty<string>();
        public string Description { get; } = "Show you a server TPS";

        public bool Execute(ArraySegment<string> arguments, ICommandSender sender, out string response)
        {
            response = $"Server TPS: {Server.TPS}";
            return true;
        }
    }
}

