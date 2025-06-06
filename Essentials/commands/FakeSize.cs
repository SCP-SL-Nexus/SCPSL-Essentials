using CommandSystem;
using LabApi.Features.Wrappers;
using System;
using UnityEngine;

namespace Essentials.commands
{
    internal class FakeSize : ICommand
    {
        public static FakeSize Instance { get; } = new FakeSize();
        public string Command { get; } = "FakeSize";
        public string[] Aliases { get; } = { "fs" };
        public string Description { get; } = "Change the size of player and make they not true";
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
                response = lang == "en" ? "Usage: FakeSize <PlayerID> <x> <y> <z>" : "Использование: FakeSize <ID игрока> <x> <y> <z>";
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
            Events.StaticCommands.SetFakePlayerScale(player, scale);
            response = lang == "en" ? $"Player {playerId}'s fake size has been changed to {x}, {y}, {z}." : $"Игрок {playerId} изменён в ненастоящем размере на {x}, {y}, {z}.";
            return true;
        }
    }
}