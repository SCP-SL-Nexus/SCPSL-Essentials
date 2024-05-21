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
            GameObject go = target.ReferenceHub.gameObject;
            if (go.transform.localScale == scale)
                return;
            try
            {
                Vector3 oldScale = go.transform.localScale;
                go.transform.localScale = scale;
                float scaleFactor = scale.y / oldScale.y;
                go.transform.position = new Vector3(go.transform.position.x, go.transform.position.y * scaleFactor, go.transform.position.z);
                RpcUpdatePlayerScale(target, scale, go.transform.position);
            }
            catch (Exception e)
            {
                Log.Info($"Set Scale error: {e}");
            }
        }
        [ClientRpc]
        private static void RpcUpdatePlayerScale(Player target, Vector3 scale, Vector3 position)
        {
            if (target.GameObject != null)
            {
                target.ReferenceHub.gameObject.transform.localScale = scale;
                target.ReferenceHub.gameObject.transform.position = position;
            }
        }
    }
}
