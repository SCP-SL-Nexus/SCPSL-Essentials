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
        public string[] Aliases { get; } = Array.Empty<string>();
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

            GameObject playerObject = Player.Get(playerId)?.GameObject;
            if (playerObject == null)
            {
                response = $"No player found with ID: {playerId}";
                return false;
            }

            float sizeX = 1f;
            if (float.TryParse(arguments.At(1), out float parsedSizeX))
            {
                if (parsedSizeX <= 0f || parsedSizeX > 10f)
                {
                    response = "Player size must be between 0.1 and 10.";
                    return false;
                }
                sizeX = parsedSizeX;
            }

            float sizeY = 1f;
            if (float.TryParse(arguments.At(2), out float parsedSizeY))
            {
                if (parsedSizeY <= 0f || parsedSizeY > 10f)
                {
                    response = "Player size must be between 0.1 and 10.";
                    return false;
                }
                sizeY = parsedSizeY;
            }

            float sizeZ = 1f;
            if (float.TryParse(arguments.At(3), out float parsedSizeZ))
            {
                if (parsedSizeZ <= 0f || parsedSizeZ > 10f)
                {
                    response = "Player size must be between 0.1 and 10.";
                    return false;
                }
                sizeZ = parsedSizeZ;
            }

            playerObject.transform.localScale = new Vector3(sizeX, sizeY, sizeZ);

            playerObject.transform.position += Vector3.up * (sizeY - 1f);

            response = $"Player {playerId}'s size has been changed to {sizeX}, {sizeY}, {sizeZ}.";
            return true;
        }
    }
}

