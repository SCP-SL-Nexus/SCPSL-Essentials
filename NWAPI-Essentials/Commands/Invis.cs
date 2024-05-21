using CommandSystem;
using CustomPlayerEffects;
using RemoteAdmin;
using PluginAPI.Core;
using System;

namespace NWAPI_Essentials.Commands
{
    internal class Invis : ICommand
    {
        public static Invis Instance { get; } = new Invis();
        public string Command { get; } = "Invis";
        public string[] Aliases { get; } = { "inv" };
        public string Description { get; } = "Make an admin invisible";

        public bool Execute(ArraySegment<string> arguments, ICommandSender sender, out string response)
        {
            var config = Plugins.Singleton.Config;

            if (!sender.CheckPermission(PlayerPermissions.Effects))
            {
                response = config.language == "en" ? "You don't have permission to use this command! (Permission name: Effects)" : "У вас нет разрешения на эту команду! (Название разрешения: Effects)";
                return false;
            }
            if (sender is not PlayerCommandSender playerSender)
            {
                response = config.language == "en" ? "This command can only be used by players." : "Эта команда может быть использована только на игроках.";
                return false;
            }
            var effectsController = playerSender.ReferenceHub.playerEffectsController;
            effectsController.EnableEffect<Invisible>();
            response = config.language == "en" ? "You are now invisible!" : "Теперь вы невидимый!";
            return true;
        }
    }
}
