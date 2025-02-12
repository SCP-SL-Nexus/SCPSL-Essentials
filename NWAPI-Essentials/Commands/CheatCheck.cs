using CommandSystem;
using LabApi.Features.Wrappers;
using PlayerRoles;
using RemoteAdmin;
using System;

namespace NWAPI_Essentials
{
    internal class CheatCheck : ICommand
    {
        public static CheatCheck Instance { get; } = new CheatCheck();
        public string Command { get; } = "CheatCheck";
        public string[] Aliases { get; } = { "cc" };
        public string Description { get; } = "Check player for cheats";

        public bool Execute(ArraySegment<string> arguments, ICommandSender sender, out string response)
        {
            var config = Plugins.Singleton.Config;
            bool isEnglish = config.language == "en";
            if (!sender.CheckPermission(PlayerPermissions.Overwatch))
            {
                response = isEnglish ? "You don't have permission to use this command! (Permission name: Overwatch)" : "У вас нет разрешения на эту команду! (Название разрешения: Overwatch)";
                return false;
            }
            if (!(sender is PlayerCommandSender playerSender))
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
            player.SetRole(RoleTypeId.Overwatch);
            player.SendBroadcast(isEnglish ? "Cheat Check Start" : "Проверка на читы началась", 5);
            response = isEnglish ? "Player role changed to Overwatch." : "Роль игрока изменена на Overwatch.";
            return true;
        }
    }
}
