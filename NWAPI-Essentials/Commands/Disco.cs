using CommandSystem;
using MEC;
using PluginAPI.Core;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace NWAPI_Essentials.Commands
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
            if (!sender.CheckPermission(PlayerPermissions.Effects))
            {
                if (config.language == "en")
                {
                    response = "You don't have permission to use this command! (Permission name: Effects)";
                    return false;
                }
                else
                {
                    response = "У вас нет разрешения на эту команду! (Название разрешения: Effects)";
                    return false;
                }
            }
            if (arguments.Count < 1)
            {
                if (config.language == "en")
                {
                    response = "Usage: et Disco on or off";
                    return false;
                }
                else
                {
                    response = "Используйте так: et Disco on или off";
                    return false;
                }

            }

            switch (arguments.At(0))
            {
                case "On":
                    if (config.language == "en")
                    {
                        response = "The disco turned on";
                        Timing.RunCoroutine(DiscoPlay(), $"Disco");
                        return false;
                    }
                    else
                    {
                        response = "Disco будет включен сейчас";
                        Timing.RunCoroutine(DiscoPlay(), $"Disco");
                        return false;
                    }
                case "off":
                    if (config.language == "en")
                    {
                        response = "The disco turned off";
                        Timing.KillCoroutines("Disco");
                        return false;
                    }
                    else
                    {
                        Timing.KillCoroutines("Disco");
                        response = "Disco будет выключен сейчас";
                        return false;
                    }
                default:
                    if (config.language == "en")
                    {
                        Timing.RunCoroutine(DiscoPlay(), "Disco");
                        response = "The disco has been turned on or will be turned on now";
                        return false;
                    }
                    else
                    {
                        Timing.RunCoroutine(DiscoPlay(), "Disco");
                        response = "Disco был включен или будет включен сейчас";
                        return false;
                    }
            }
        }
        private IEnumerator<float> DiscoPlay()
        {
            for (; ; )
            {
                yield return Timing.WaitForSeconds(1f);
                {
                    Map.ChangeColorOfAllLights(Color.red);
                    Timing.WaitForSeconds(1f);
                    Map.ChangeColorOfAllLights(Color.cyan);
                    Timing.WaitForSeconds(1f);
                    Map.ChangeColorOfAllLights(Color.black);
                    Timing.WaitForSeconds(1f);
                    Map.ChangeColorOfAllLights(Color.white);
                    Timing.WaitForSeconds(1f);
                    Map.ChangeColorOfAllLights(Color.green);
                    Timing.WaitForSeconds(1f);
                    Map.ChangeColorOfAllLights(Color.gray);
                    Timing.WaitForSeconds(1f);
                    Map.ChangeColorOfAllLights(Color.grey);
                    Timing.WaitForSeconds(1f);
                    Map.ChangeColorOfAllLights(Color.yellow);
                    Timing.WaitForSeconds(1f);
                    Map.ChangeColorOfAllLights(Color.magenta);
                }
            }
        }
    }
}
