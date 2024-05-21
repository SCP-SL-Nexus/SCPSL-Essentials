using CommandSystem;
using MEC;
using PluginAPI.Core;
using RemoteAdmin;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace NWAPI_Essentials.Commands
{
    internal class Size : ICommand
    {
        public static Size Instance { get; } = new Size();
        public string Command { get; } = "Size";
        public string[] Aliases { get; } = { "s" };
        public string Description { get; } = "Change the size of a player";
        public bool Execute(ArraySegment<string> arguments, ICommandSender sender, out string response)
        {
            var config = Plugins.Singleton.Config;
            string lang = config.language;
            if (!sender.CheckPermission(PlayerPermissions.PlayersManagement))
            {
                response = lang == "en" ? "You don't have permission to use this command! (Permission name: PlayersManagement)" : "У вас нет разрешения на эту команду! (Название разрешения: PlayersManagement)";
                return false;
            }
            if (arguments.Count < 4)
            {
                response = lang == "en" ? "Usage: Size <PlayerID> <x> <y> <z>" : "Использование: Size <ID игрока> <x> <y> <z>";
                return false;
            }
            if (!int.TryParse(arguments.At(0), out int playerId) ||
                !float.TryParse(arguments.At(1), out float x) ||
                !float.TryParse(arguments.At(2), out float y) ||
                !float.TryParse(arguments.At(3), out float z))
            {
                response = lang == "en" ? "Invalid input values." : "Неправильные входные значения.";
                return false;
            }
            Player player = Player.Get(playerId);
            if (player == null)
            {
                response = lang == "en" ? "No player found with that ID." : "Игрок с таким ID не найден.";
                return false;
            }
            Vector3 scale = new Vector3(x, y, z);
            Events.StaticCommands.SetPlayerScale(player, scale);
            Timing.RunCoroutine(ResetScaleOnDeath(player));
            response = lang == "en" ? $"Player {playerId}'s size has been changed to {x}, {y}, {z}." : $"Игрок {playerId} изменён в размере на {x}, {y}, {z}.";
            return true;
        }
        private IEnumerator<float> ResetScaleOnDeath(Player player)
        {
            while (player.IsAlive)
            {
                yield return Timing.WaitForSeconds(3f);
            }
            Events.StaticCommands.SetPlayerScale(player, new Vector3(1f, 1f, 1f));
        }
    }
}
