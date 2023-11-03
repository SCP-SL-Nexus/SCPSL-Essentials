using CommandSystem;
using CustomPlayerEffects;
using InventorySystem.Items.Usables;
using PluginAPI.Core;
using RemoteAdmin;
using System;

namespace NWAPI_Essentials.Commands
{
    internal class Visible : ICommand
    {
            public static Visible Instance { get; } = new Visible();
            public string Command { get; } = "Visible";
            public string[] Aliases { get; } = Array.Empty<string>();
            public string Description { get; } = "Make a Admin to Visible";

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

            PlayerEffectsController effectsController = playerSender.ReferenceHub.playerEffectsController;
            effectsController.DisableEffect<Invisible>();
            response = "You are now Visible!";
            return true;
        }
    }
}

