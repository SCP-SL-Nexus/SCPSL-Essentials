using Mirror;
using PluginAPI.Core;
using System;
using UnityEngine;

namespace NWAPI_Essentials.Events
{
    internal static class StaticCommands
    {
        public static void SetPlayerScale(Player target, Vector3 scale)
        {
            GameObject go = target.GameObject;
            if (go.transform.localScale == scale)
                return;
            try
            {
                go.transform.localScale = scale;
                RpcUpdatePlayerScale(target, scale);
            }
            catch (Exception e)
            {
                Log.Info($"Set Scale error: {e}");
            }
        }
        [ClientRpc]
        public static void RpcUpdatePlayerScale(Player target, Vector3 scale)
        {
            target.GameObject.transform.localScale = scale;
        }
        public static void SetPlayerScale(Player target, float scale) => SetPlayerScale(target, Vector3.one * scale);
    }
}