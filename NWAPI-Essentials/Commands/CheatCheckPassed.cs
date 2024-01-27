using CommandSystem;
using PlayerRoles;
using PluginAPI.Core;
using RemoteAdmin;
using System;

namespace NWAPI_Essentials
{
    internal class Cheatcheckpassed : ICommand
    {
        public static Cheatcheckpassed Instance { get; } = new Cheatcheckpassed();
        public string Command { get; } = "Cheatcheckpassed";
        public string[] Aliases { get; } = { "ccp" };
        public string Description { get; } = "Pass a cheatcheck";

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

            Player player = Player.Get(playerId);
            if (player != null)
            {
                player.SetRole(RoleTypeId.Spectator);
                if (config.language == "en")
                {
                    player.SendBroadcast("Cheat Check Pass", 5);
                }
                else
                {
                    player.SendBroadcast("Проверка на читы пройдена", 5);
                }
            }
            if (config.language == "en")
            {
                response = "Player role changed to Spectator.";
                return true;
            }
            else
            {
                response = "Роль игркоа изменена на Спектора.";
                return true;
            }
        }
    }
}

