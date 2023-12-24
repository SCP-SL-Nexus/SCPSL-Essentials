using CommandSystem;
using PluginAPI.Core;
using RemoteAdmin;
using System;
using UnityEngine;

namespace NWAPI_Essentials.Commands
{
    internal class Size : ICommand
    {
        public static Size Instance { get; } = new Size();
        public string Command { get; } = "Size";
        public string[] Aliases { get; } = { "s" };
        public string Description { get; } = "Size a player";

        public bool Execute(ArraySegment<string> arguments, ICommandSender sender, out string response)
        {
            if (!sender.CheckPermission(PlayerPermissions.PlayersManagement))
            {
                response = "You don't have permission to use this command! (Permission name: PlayersManagement)";
                return false;
            }

            if (!(sender is PlayerCommandSender playerSender))
            {
                response = "This command can only be used by players.";
                return false;
            }

            if (arguments.Count < 4)
            {
                response = "Use this: PlayerID x y z";
                return false;
            }

            bool parsed = int.TryParse(arguments.At(0), out int playerId);
            if (!parsed)
            {
                response = "Invalid player ID provided.";
                return false;
            }

            Player player = Player.Get(playerId);
            if (player == null)
            {
                response = $"No player found with ID: {playerId}";
                return false;
            }

            if (!float.TryParse(arguments.At(1), out float x) || !float.TryParse(arguments.At(2), out float y) || !float.TryParse(arguments.At(3), out float z))
            {
                response = "Invalid size values";
                return false;
            }

            Events.Commands.SetPlayerScale(player, new Vector3(x, y, z));
            response = $"Player {playerId}'s size has been changed to {x}, {y}, {z}.";
            return true;
        }
    }
}