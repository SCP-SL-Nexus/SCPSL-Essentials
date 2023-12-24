using Mirror;
using PluginAPI.Core;
using System;
using UnityEngine;

namespace NWAPI_Essentials.Events
{
    internal class Commands
    {
        public static void SetPlayerScale(Player target, Vector3 scale)
        {
            GameObject go = target.GameObject;
            Camera playerCamera = go.GetComponentInChildren<Camera>();
            if (playerCamera != null)
            {
                playerCamera.transform.localPosition = new Vector3(0f, scale.y / 2f, -2f);
            }
            if (go.transform.localScale == scale)
                return;
            try
            {
                NetworkIdentity identity = target.ReferenceHub.networkIdentity;
                go.transform.localScale = scale;
            }
            catch (Exception error)
            {
                Log.Info($"Set Scale error: {error}");
            }
        }
        public static void SetPlayerScale(Player target, float scale) => SetPlayerScale(target, Vector3.one * scale);
    }
}
