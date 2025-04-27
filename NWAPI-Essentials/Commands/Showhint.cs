using CommandSystem;
using LabApi.Features.Wrappers;
using RemoteAdmin;
using System;
using System.Linq;

namespace NWAPI_Essentials.Commands
{
    internal class Showhint : ICommand
    {
        public static Showhint Instance { get; } = new Showhint();
        public string Command { get; } = "Showhint";
        public string[] Aliases { get; } = { "Sh" };
        public string Description { get; } = "Show a hint to a player";
        public bool Execute(ArraySegment<string> arguments, ICommandSender sender, out string response)
        {
            var config = Plugins.Singleton.Config;
            var lang = config.language;
            if (!sender.CheckPermission(PlayerPermissions.Broadcasting))
            {
                response = lang == "en" ? "You don't have permission to use this command! (Permission name: Broadcasting)" : "У вас нет разрешения на эту команду! (Название разрешения: Broadcasting)";
                return false;
            }
            if (!(sender is PlayerCommandSender playerSender))
            {
                response = lang == "en" ? "This command can only be used by players." : "Эта команда может использоваться только на игроках.";
                return false;
            }
            if (arguments.Count < 2)
            {
                response = lang == "en" ? "Usage: Showhint <playerID> <message>" : "Использование: Showhint <playerID> <сообщение>";
                return false;
            }
            if (!int.TryParse(arguments.At(0), out int playerId))
            {
                response = lang == "en" ? "Invalid player ID provided." : "Неправильный ID игрока.";
                return false;
            }
            var player = Player.Get(playerId);
            if (player == null)
            {
                response = lang == "en" ? "No player found with that ID." : "Игрок с таким ID не найден.";
                return false;
            }
            var message = string.Join(" ", arguments.Skip(1));
            player.SendHint(message);
            response = lang == "en" ? "Hint shown." : "Hint был показан.";
            return true;
        }
    }
}
