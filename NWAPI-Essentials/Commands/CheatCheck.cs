using CommandSystem;
using PlayerRoles;
using PluginAPI.Core;
using PluginAPI.Core.Attributes;
using RemoteAdmin;
using System;
using UnityEngine;

namespace NWAPI_Essentials
{
    internal class CheatCheck : ICommand
    {
        [PluginConfig]
        public Config Config;
        public static CheatCheck Instance { get; } = new CheatCheck();
        public string Command { get; } = "CheatCheck";
        public string[] Aliases { get; } = { "cc" };
        public string Description { get; } = "Check player a Cheats";

        public bool Execute(ArraySegment<string> arguments, ICommandSender sender, out string response)
        {
            if (!sender.CheckPermission(PlayerPermissions.Overwatch))
            {
                response = "You don't have permission to use this command! (Permission name: Overwatch)";
                return false;
            }

            if (!(sender is PlayerCommandSender playerSender))
            {
                response = "This command can only be used by players.";
                return false;
            }

            if (arguments.Count < 1)
            {
                response = "You must specify a player ID to target.";
                return false;
            }

            bool parsed = int.TryParse(arguments.At(0), out int playerId);
            if (!parsed)
            {
                response = "Invalid player ID provided.";
                return false;
            }

            GameObject playerObject = Player.Get(playerId)?.GameObject;
            if (playerObject == null)
            {
                response = "No player found with that ID.";
                return false;
            }

            Player player = Player.Get(playerId);
            if (player != null)
            {
                player.SetRole(RoleTypeId.Overwatch);
                player.SendBroadcast("Cheat Check Start", 5);
            }

            response = "Player role changed to Overwatch.";
            return true;
        }
    }
}
