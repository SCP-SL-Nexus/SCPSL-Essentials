using CommandSystem;
using PluginAPI.Core;
using System;

namespace NWAPI_Essentials.Commands
{
    internal class Adminsonserver : ICommand
    {
        public static Adminsonserver Instance { get; } = new Adminsonserver();
        public string Command { get; } = "Adminsonserver";
        public string[] Aliases { get; } = { "Admonser" };
        public string Description { get; } = "Admin on Server";

        public bool Execute(ArraySegment<string> arguments, ICommandSender sender, out string response)
        {
            if (!sender.CheckPermission(PlayerPermissions.PlayersManagement))
            {
                response = "You don't have permission to use this command! (Permission name: PlayersManagement)";
                return false;
            }
            foreach (Player p in Player.GetPlayers())
            {
                if (p.RemoteAdminAccess == true)
                {
                    response = $"Admin on server: {string.Join(", ", p.Nickname)}";
                    return true;
                }
            }
            response = null;
            return true;
        }
    }
}
