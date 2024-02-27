using CommandSystem;
using PluginAPI.Core;
using RemoteAdmin;
using System;
using System.Linq;

namespace NWAPI_Essentials.Commands
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
                    response = "Недействительное ID игрока.";
                    return false;
                }
            }

            Player player = Player.Get(playerId);
            if (player == null)
            {
                if (config.language == "en")
                {
                    response = $"No player found with ID: {playerId}";
                    return false;
                }
                else
                {
                    response = $"Игрок с таким ID не найден: {playerId}";
                    return false;
                }
            }
            string message = string.Join(" ", arguments.Skip(1).ToArray());
            player.CustomInfo = message;
            if (config.language == "en")
            {
                response = $"CustomInfo {message}, Give to {player}.";
                return true;
            }
            else
            {
                response = $"CustomInfo {message}, Был выдан {player}.";
                return false;
            }
        }
    }
}
