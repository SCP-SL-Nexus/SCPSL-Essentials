using CommandSystem;
using CustomPlayerEffects;
using RemoteAdmin;
using System;

namespace NWAPI_Essentials.Commands
{
    internal class Invis : ICommand
    {
        public static Invis Instance { get; } = new Invis();
        public string Command { get; } = "Invis";
        public string[] Aliases { get; } = { "inv" };
        public string Description { get; } = "Make a Admin to Invis";

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
            effectsController.EnableEffect<Invisible>();
            response = "You are now invisible!";
            return true;
        }
    }
}


