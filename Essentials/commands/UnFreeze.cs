using CommandSystem;
using CustomPlayerEffects;
using LabApi.Features.Wrappers;
using System;

namespace Essentials.Commands
{
    internal class UnFreeze : ICommand
    {
        public static UnFreeze Instance { get; } = new UnFreeze();
        public string Command { get; } = "UnFreeze";
        public string[] Aliases { get; } = { "unf" };
        public string Description { get; } = "UnFreeze a player";

        public bool Execute(ArraySegment<string> arguments, ICommandSender sender, out string response)
        {
            var config = Plugins.Singleton.Config;
            string lang = config.language;
            if (!sender.CheckPermission(PlayerPermissions.Effects))
            {
                response = lang == "en" ? "You don't have permission to use this command! (Permission name: Effects)" : "У вас нет разрешения на эту команду! (Название разрешения: Effects)";
                return false;
            }
            if (arguments.Count < 1 || !int.TryParse(arguments.At(0), out int playerId))
            {
                response = lang == "en" ? "You must specify a valid player ID to target." : "Вы должны ввести действительный ID игрока.";
                return false;
            }
            var player = Player.Get(playerId);
            if (player == null)
            {
                response = lang == "en" ? "No player found with that ID." : "Игрок с таким ID не найден.";
                return false;
            }
            var effectsController = player.GameObject.GetComponent<PlayerEffectsController>();
            if (effectsController == null)
            {
                response = lang == "en" ? "Error: Player effects controller not found." : "Ошибка: Контроллер эффектов игрока не найден.";
                return false;
            }
            effectsController.DisableEffect<Ensnared>();
            response = lang == "en" ? "Player is UnFreezed!" : "Игрок разморожен!";
            return true;
        }
    }
}