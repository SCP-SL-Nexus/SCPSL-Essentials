using CommandSystem;
using PluginAPI.Core;
using RemoteAdmin;
using System;
using UnityEngine;

namespace NWAPI_Essentials.Commands
{
    internal class Size : ICommand
    {
        public static Size Instance { get; } = new Size();
        public string Command { get; } = "Size";
        public string[] Aliases { get; } = { "s" };
        public string Description { get; } = "Size a player";

        public bool Execute(ArraySegment<string> arguments, ICommandSender sender, out string response)
        {
            var config = Plugins.Singleton.Config;
            if (!sender.CheckPermission(PlayerPermissions.PlayersManagement))
            {
                if (config.language == "en")
                {
                    response = "You don't have permission to use this command! (Permission name: PlayersManagement)";
                    return false;
                }
                else
                {
                    response = "У вас нету разрешения на эту команду! (Название разрешения: PlayersManagement)";
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

            if (arguments.Count < 4)
            {
                if (config.language == "en")
                {
                    response = "Use this: PlayerID x y z";
                    return false;
                }
                else
                {
                    response = "Используйте так: ID игрока x y z";
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

            Player player = Player.Get(playerId);
            if (player == null)
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

            if (!float.TryParse(arguments.At(1), out float x) || !float.TryParse(arguments.At(2), out float y) || !float.TryParse(arguments.At(3), out float z))
            {
                if (config.language == "en")
                {
                    response = "Invalid size values";
                    return false;
                }
                else
                {
                    response = "Не правильные значения для size";
                    return false;
                }
            }

            Events.Commands.SetPlayerScale(player, new Vector3(x, y, z));
            if (config.language == "en")
            {
                response = $"Player {playerId}'s size has been changed to {x}, {y}, {z}.";
                return true;
            }
            else
            {
                response = $"Игрок {playerId}, был изменён в размере на {x}, {y}, {z}.";
                return true;
            }
        }
    }
}