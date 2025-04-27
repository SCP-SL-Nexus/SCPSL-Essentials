using CommandSystem;
using LabApi.Features.Wrappers;
using MEC;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace Essentials.Commands
{
    internal class Disco : ICommand
    {
        public static Disco Instance { get; } = new Disco();
        public string Command { get; } = "Disco";
        public string[] Aliases { get; } = { "D" };
        public string Description { get; } = "Disco!";

        public bool Execute(ArraySegment<string> arguments, ICommandSender sender, out string response)
        {
            var config = Plugins.Singleton.Config;
            bool isEnglish = config.language == "en";
            if (!sender.CheckPermission(PlayerPermissions.Effects))
            {
                response = isEnglish ? "You don't have permission to use this command! (Permission name: Effects)" : "У вас нет разрешения на эту команду! (Название разрешения: Effects)";
                return false;
            }
            if (arguments.Count < 1)
            {
                response = isEnglish ? "Usage: et Disco on or off" : "Используйте так: et Disco on или off";
                return false;
            }
            switch (arguments.At(0).ToLower())
            {
                case "on":
                    Timing.RunCoroutine(DiscoPlay(), "Disco");
                    response = isEnglish ? "The disco turned on" : "Disco будет включен сейчас";
                    return true;
                case "off":
                    Timing.KillCoroutines("Disco");
                    response = isEnglish ? "The disco turned off" : "Disco будет выключен сейчас";
                    return true;
                default:
                    response = isEnglish ? "Invalid argument. Use 'on' or 'off'." : "Недопустимый аргумент. Используйте 'on' или 'off'.";
                    return false;
            }
        }
        private IEnumerator<float> DiscoPlay()
        {
            var colors = new Color[] { Color.red, Color.cyan, Color.black, Color.white, Color.green, Color.gray, Color.grey, Color.yellow, Color.magenta };
            while (true)
            {
                foreach (var color in colors)
                {
                    Map.SetColorOfLights(color);
                    yield return Timing.WaitForSeconds(1f);
                }
            }
        }
    }
}