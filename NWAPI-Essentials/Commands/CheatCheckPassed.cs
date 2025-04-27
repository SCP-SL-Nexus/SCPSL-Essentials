using CommandSystem;
using LabApi.Features.Wrappers;
using PlayerRoles;
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
            bool isEnglish = config.language == "en";
            if (!sender.CheckPermission(PlayerPermissions.Overwatch))
            {
                response = isEnglish ? "You don't have permission to use this command! (Permission name: Overwatch)" : "У вас нет разрешения на эту команду! (Название разрешения: Overwatch)";
                return false;
            }
            if (!(sender is PlayerCommandSender))
            {
                response = isEnglish ? "This command can only be used by players." : "Эта команда может использоваться только на игроках.";
                return false;
            }
            if (arguments.Count < 1)
            {
                response = isEnglish ? "You must specify a player ID to target." : "Вы должны ввести ID игрока.";
                return false;
            }
            if (!int.TryParse(arguments.At(0), out int playerId))
            {
                response = isEnglish ? "Invalid player ID provided." : "Неправильный ID игрока.";
                return false;
            }
            var player = Player.Get(playerId);
            if (player == null)
            {
                response = isEnglish ? "No player found with that ID." : "Игрок с таким ID не найден.";
                return false;
            }
            player.SetRole(RoleTypeId.Spectator);
            player.SendBroadcast(isEnglish ? "Cheat Check Pass" : "Проверка на читы пройдена", 5);
            response = isEnglish ? "Player role changed to Spectator." : "Роль игрока изменена на Спектора.";
            return true;
        }
    }
}