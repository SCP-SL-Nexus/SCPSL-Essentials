using Essentials.pathes;
using LabApi.Features.Wrappers;
using System;
using UnityEngine;
using Logger = LabApi.Features.Console.Logger;

namespace Essentials.Events
{
    internal static class StaticCommands
    {
        public static void SetPlayerScale(Player target, Vector3 scale)
        {
            if (target.ReferenceHub.transform.localScale == scale)
                return;

            try
            {
                target.ReferenceHub.transform.localScale = scale;

                foreach (Player plr in Player.List)
                    NetworkServerInvoker.InvokeSendSpawnMessage(target.ReferenceHub.networkIdentity, plr.Connection);
            }
            catch (Exception e)
            {
                Logger.Info($"Set Scale error: {e}");
            }
        }
        public static void SetFakePlayerScale(Player target, Vector3 scale)
        {
            var original = target.ReferenceHub.transform.localScale;

            if (target.ReferenceHub.transform.localScale == scale)
                return;

            try
            {
                target.ReferenceHub.transform.localScale = scale;

                foreach (Player plr in Player.List)
                    NetworkServerInvoker.InvokeSendSpawnMessage(target.ReferenceHub.networkIdentity, plr.Connection);

                target.ReferenceHub.transform.localScale = original;
            }
            catch (Exception e)
            {
                Logger.Info($"Set Scale error: {e}");
            }
        }
    }
}