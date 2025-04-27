using CommandSystem;
using CustomPlayerEffects;
using RemoteAdmin;
using System;

namespace Essentials.Commands
{
    internal class Visible : ICommand
    {
        public static Visible Instance { get; } = new Visible();
        public string Command { get; } = "Visible";
        public string[] Aliases { get; } = { "vis" };
        public string Description { get; } = "Make an admin visible";

        public bool Execute(ArraySegment<string> arguments, ICommandSender sender, out string response)
        {
            var config = Plugins.Singleton.Config;
            string lang = config.language;

            if (!sender.CheckPermission(PlayerPermissions.Effects))
            {
                response = lang == "en" ? "You don't have permission to use this command! (Permission name: Effects)" : "У вас нет разрешения на эту команду! (Название разрешения: Effects)";
                return false;
            }

            if (sender is not PlayerCommandSender playerSender)
            {
                response = lang == "en" ? "This command can only be used by players." : "Эта команда может быть использована только на игроках.";
                return false;
            }
            playerSender.ReferenceHub.playerEffectsController.DisableEffect<Invisible>();
            response = lang == "en" ? "You are now visible!" : "Теперь вы видимый!";
            return true;
        }
    }
}
