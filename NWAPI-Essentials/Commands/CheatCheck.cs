using CommandSystem;
using PlayerRoles;
using PluginAPI.Core;
using PluginAPI.Core.Attributes;
using RemoteAdmin;
using System;
using UnityEngine;

namespace NWAPI_Essentials
{
    internal class CheatCheck : ICommand
    {
        [PluginConfig]
        public Config Config;
        public static CheatCheck Instance { get; } = new CheatCheck();
        public string Command { get; } = "CheatCheck";
        public string[] Aliases { get; } = { "cc" };
        public string Description { get; } = "Check player a Cheats";

        public bool Execute(ArraySegment<string> arguments, ICommandSender sender, out string response)
        {
            var config = Plugins.Singleton.Config;
            if (!sender.CheckPermission(PlayerPermissions.Overwatch))
            {
                if (config.language == "en")
                {
                    response = "You don't have permission to use this command! (Permission name: Overwatch)";
                    return false;
                }
                else
                {
                    response = "У вас нет разрешения на эту команду! (Название разрешения: Overwatch)";
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
                    response = "Эта команда может использоваться только на игроках.";
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
                    response = "Вы должны ввести ID игрока.";
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

            Player player = Player.Get(playerId);
            if (player != null)
            {
                player.SetRole(RoleTypeId.Overwatch);
                if (config.language == "en")
                {
                    player.SendBroadcast("Cheat Check Start", 5);
                }
                else
                {
                    player.SendBroadcast("Проверка на читы началась", 5);
                }
            }
            if (config.language == "en")
            {
                response = "Player role changed to Overwatch.";
                return true;
            }
            else
            {
                response = "Роль игрока изменена на Overwatch.";
                return true;
            }
        }
    }
}
