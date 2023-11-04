using CommandSystem;
using PluginAPI.Core;
using RemoteAdmin;
using System;
using System.Security.Policy;
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

            if (!float.TryParse(arguments.At(1), out float sizeX) || !float.TryParse(arguments.At(2), out float sizeY) || !float.TryParse(arguments.At(3), out float sizeZ))
{
                response = "Invalid size values";
                return false;
            }
            if (sizeX <= 0f || sizeX > 10f || sizeY <= 0f || sizeY > 10f || sizeY <= 0f || sizeZ > 10f)
            {
                response = "Player size must be between 0.1 and 10.";
                return false;
            }
            playerObject.transform.localScale = new Vector3(sizeX, sizeY, sizeZ);

            Camera playerCamera = playerObject.GetComponentInChildren<Camera>();
            if (playerCamera != null)
            {
                playerCamera.transform.localScale = new Vector3(sizeX, sizeY, sizeZ);
            }

             response = $"Player {playerId}'s size has been changed to {sizeX}, {sizeY}, {sizeZ}.";
             return true;
        }
    }
}

