using CommandSystem;
using LabApi.Features.Wrappers;
using RemoteAdmin;
using System;
using System.Linq;

namespace Essentials.Commands
{
    internal class CustomInfo : ICommand
    {
        public static CustomInfo Instance { get; } = new CustomInfo();
        public string Command { get; } = "CustomInfo";
        public string[] Aliases { get; } = { "CI" };
        public string Description { get; } = "Give a CustomInfo to Player";

        public bool Execute(ArraySegment<string> arguments, ICommandSender sender, out string response)
        {
            var config = Plugins.Singleton.Config;
            bool isEnglish = config.language == "en";
            if (!sender.CheckPermission(PlayerPermissions.PlayersManagement))
            {
                response = isEnglish ? "You don't have permission to use this command! (Permission name: PlayersManagement)" : "У вас нету разрешения на эту команду! (Название разрешения: PlayersManagement)";
                return false;
            }
            if (!(sender is PlayerCommandSender))
            {
                response = isEnglish ? "This command can only be used by players." : "Эта команда может быть использована только на игроках.";
                return false;
            }
            if (arguments.Count < 2)
            {
                response = isEnglish ? "You must specify a player ID and a message." : "Вы должны указать ID игрока и сообщение.";
                return false;
            }
            if (!int.TryParse(arguments.At(0), out int playerId))
            {
                response = isEnglish ? "Invalid player ID provided." : "Недействительное ID игрока.";
                return false;
            }
            var player = Player.Get(playerId);
            if (player == null)
            {
                response = isEnglish ? $"No player found with ID: {playerId}" : $"Игрок с таким ID не найден: {playerId}";
                return false;
            }
            string message = string.Join(" ", arguments.Skip(1));
            player.CustomInfo = message;
            response = isEnglish ? $"CustomInfo '{message}' given to {player.Nickname}." : $"CustomInfo '{message}' был выдан {player.Nickname}.";
            return true;
        }
    }
}