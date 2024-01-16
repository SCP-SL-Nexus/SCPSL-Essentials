using CommandSystem;
using CustomPlayerEffects;
using RemoteAdmin;
using System;

namespace NWAPI_Essentials.Commands
{
    internal class Visible : ICommand
    {
        public static Visible Instance { get; } = new Visible();
        public string Command { get; } = "Visible";
        public string[] Aliases { get; } = { "vis" };
        public string Description { get; } = "Make a Admin to Visible";

        public bool Execute(ArraySegment<string> arguments, ICommandSender sender, out string response)
        {
            var config = Plugins.Singleton.Config;
            if (!sender.CheckPermission(PlayerPermissions.Effects))
            {
                if (config.language == "en")
                {
                    response = "You don't have permission to use this command! (Permission name: Effects)";
                    return false;
                }
                else
                {
                    response = "У вас нет разрешения на эту команду! (Название разрешения: Effects)";
                    return false;
                }
            }

            if (!(sender is PlayerCommandSender playerSender))
            {
                if (config.language == "en")
                {
                    response = "This command can only be used by players.";
                    return false;
                }
                else
                {
                    response = "Эта команда может быть использована только на игроках.";
                    return false;
                }
            }

            PlayerEffectsController effectsController = playerSender.ReferenceHub.playerEffectsController;
            effectsController.DisableEffect<Invisible>();
            if (config.language == "en")
            {
                response = "You are now Visible!";
                return true;
            }
            else
            {
                response = "Теперь вы видимый!";
                return true;
            }
        }
    }
}

