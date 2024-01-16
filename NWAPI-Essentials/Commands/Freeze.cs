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
        public string[] Aliases { get; } = { "f" };
        public string Description { get; } = "Freeze a player for Breaking rules";

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

            if (arguments.Count < 1)
            {
                if (config.language == "en")
                {
                    response = "You must specify a player ID to target.";
                    return false;
                }
                else
                {
                    response = "Вы должны использовать ID игрока.";
                    return false;
                }
            }
            bool parsed = int.TryParse(arguments.At(0), out int playerId);
            if (!parsed)
            {
                if (config.language == "en")
                {
                    response = "Invalid player ID provided.";
                    return false;
                }
                else
                {
                    response = "Неправильный ID игрока.";
                    return false;
                }
            }
            GameObject playerObject = Player.Get(playerId)?.GameObject;
            if (playerObject == null)
            {
                if (config.language == "en")
                {
                    response = "No player found with that ID.";
                    return false;
                }
                else
                {
                    response = "Игрок с таким ID не найден.";
                    return false;
                }
            }
            PlayerEffectsController effectsController = playerObject.GetComponent<PlayerEffectsController>();
            if (effectsController != null)
            {
                effectsController.EnableEffect<Ensnared>();
                if (config.language == "en")
                {
                    response = "Player is Freezed!";
                    return true;
                }
                else
                {
                    response = "Игрок заморожен!";
                    return true;
                }
            }
            else
            {
                if (config.language == "en")
                {
                    response = "Error: Player effects controller not found.";
                    return false;
                }
                else
                {
                    response = "Ошибка: Контройлер эффектов не был найден.";
                    return false;
                }
            }
        }
    }
}