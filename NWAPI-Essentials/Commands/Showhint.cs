using CommandSystem;
using PluginAPI.Core;
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
        public string Description { get; } = "Showhint for player";
        public bool Execute(ArraySegment<string> arguments, ICommandSender sender, out string response)
        {
            var config = Plugins.Singleton.Config;
            if (!sender.CheckPermission(PlayerPermissions.Broadcasting))
            {
                if (config.language == "en")
                {
                    response = "You don't have permission to use this command! (Permission name: Broadcasting)";
                    return false;
                }
                else
                {
                    response = "У вас нет разрешения на эту команду! (Название разрешения: Broadcasting)";
                    return false;
                }
            }
            string message = string.Join(" ", arguments.ToArray());
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
            Player pl = Player.Get(playerId);
            pl.ReceiveHint(message);
            if (config.language == "en")
            {
                response = "Hint show.";
                return false;
            }
            else
            {
                response = "Hint был показан.";
                return false;
            }
        }
    }
}
