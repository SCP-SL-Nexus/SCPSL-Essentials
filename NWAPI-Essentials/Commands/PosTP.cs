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
        public string Description { get; } = "Teleport you to pos";

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
                    response = "У вас нет разрешения на эту команду! (Название разрешения: PlayersManagement)";
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
            var pl = Player.Get((CommandSender)sender);
            var pos = new UnityEngine.Vector3(x, y, z);
            pl.Position = pos;
            if (config.language == "en")
            {
                response = $"You teleport to {x}, {y}, {z}";
                return true;
            }
            else
            {
                response = $"Вы были телепортированы на {x}, {y}, {z}";
                return true;
            }
        }
    }
}
