using CommandSystem;
using CustomPlayerEffects;
using PluginAPI.Core;
using RemoteAdmin;
using System;

namespace NWAPI_Essentials.Commands
{
    internal class Freeze : ICommand
    {
        public static Freeze Instance { get; } = new Freeze();
        public string Command { get; } = "Freeze";
        public string[] Aliases { get; } = { "f" };
        public string Description { get; } = "Freeze a player for breaking rules";

        public bool Execute(ArraySegment<string> arguments, ICommandSender sender, out string response)
        {
            var config = Plugins.Singleton.Config;
            if (!sender.CheckPermission(PlayerPermissions.Effects))
            {
                response = config.language == "en" ? "You don't have permission to use this command! (Permission name: Effects)" : "У вас нет разрешения на эту команду! (Название разрешения: Effects)";
                return false;
            }
            if (!(sender is PlayerCommandSender))
            {
                response = config.language == "en"
                    ? "This command can only be used by players." : "Эта команда может быть использована только на игроках.";
                return false;
            }
            if (arguments.Count < 1)
            {
                response = config.language == "en" ? "You must specify a player ID to target." : "Вы должны использовать ID игрока.";
                return false;
            }
            if (!int.TryParse(arguments.At(0), out int playerId))
            {
                response = config.language == "en" ? "Invalid player ID provided." : "Неправильный ID игрока.";
                return false;
            }
            var player = Player.Get(playerId);
            if (player == null)
            {
                response = config.language == "en" ? "No player found with that ID." : "Игрок с таким ID не найден.";
                return false;
            }
            var effectsController = player.GameObject.GetComponent<PlayerEffectsController>();
            if (effectsController == null)
            {
                response = config.language == "en" ? "Error: Player effects controller not found." : "Ошибка: Контройлер эффектов не был найден.";
                return false;
            }
            effectsController.EnableEffect<Ensnared>();
            response = config.language == "en" ? "Player is frozen!" : "Игрок заморожен!";
            return true;
        }
    }
}