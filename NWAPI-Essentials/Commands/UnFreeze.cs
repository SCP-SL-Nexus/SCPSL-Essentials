using CommandSystem;
using CustomPlayerEffects;
using PluginAPI.Core;
using RemoteAdmin;
using System;
using UnityEngine;

namespace NWAPI_Essentials.Commands
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
                effectsController.DisableEffect<Ensnared>();
                if (config.language == "en")
                {
                    response = "Player is UnFreezed!";
                    return true;
                }
                else
                {
                    response = "Игрок разморожен!";
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
                    response = "Ошибка: Контройлер игроков не был найден.";
                    return false;
                }
            }
        }
    }
}

