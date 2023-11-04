using CommandSystem;
using CustomPlayerEffects;
using PluginAPI.Core;
using RemoteAdmin;
using System;
using UnityEngine;

namespace NWAPI_Essentials.Commands
{
    internal class Freeze : ICommand
    {
        public static Freeze Instance { get; } = new Freeze();
        public string Command { get; } = "Freeze";
        public string[] Aliases { get; } = Array.Empty<string>();
        public string Description { get; } = "Freeze a player for Breaking rules";

        public bool Execute(ArraySegment<string> arguments, ICommandSender sender, out string response)
        {
            if (!sender.CheckPermission(PlayerPermissions.Effects))
            {
                response = "You don't have permission to use this command! (Permission name: Effects)";
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
            PlayerEffectsController effectsController = playerObject.GetComponent<PlayerEffectsController>();
            if (effectsController != null)
            {
                effectsController.EnableEffect<Ensnared>();
                response = "Player is Freezed!";
                return true;
            }
            else
            {
                response = "Error: Player effects controller not found.";
                return false;
            }
        }
    }
}