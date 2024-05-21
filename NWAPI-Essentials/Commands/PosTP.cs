using CommandSystem;
using PluginAPI.Core;
using System;

namespace NWAPI_Essentials.Commands
{
    internal class PosTP : ICommand
    {
        public static PosTP Instance { get; } = new PosTP();
        public string Command { get; } = "PosTeleport";
        public string[] Aliases { get; } = { "PosTP" };
        public string Description { get; } = "Teleport you to a specified position";
        public bool Execute(ArraySegment<string> arguments, ICommandSender sender, out string response)
        {
            var config = Plugins.Singleton.Config;
            var lang = config.language;
            if (!sender.CheckPermission(PlayerPermissions.PlayersManagement))
            {
                response = lang == "en" ? "You don't have permission to use this command! (Permission name: PlayersManagement)" : "У вас нет разрешения на эту команду! (Название разрешения: PlayersManagement)";
                return false;
            }
            if (arguments.Count < 4 || !float.TryParse(arguments.At(1), out float x) || !float.TryParse(arguments.At(2), out float y) || !float.TryParse(arguments.At(3), out float z))
            {
                response = lang == "en" ? "Invalid position values" : "Неправильные значения для позиции";
                return false;
            }
            var player = Player.Get((CommandSender)sender);
            player.Position = new UnityEngine.Vector3(x, y, z);
            response = lang == "en" ? $"You teleported to {x}, {y}, {z}" : $"Вы были телепортированы на {x}, {y}, {z}";
            return true;
        }
    }
}
