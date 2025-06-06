using HarmonyLib;
using Mirror;
using System;

namespace Essentials.pathes
{
    internal class NetworkServerInvoker
    {
        private delegate void SendSpawnMessageDelegate(NetworkIdentity identity, NetworkConnection conn);

        private static SendSpawnMessageDelegate _sendSpawnMessage;

        static NetworkServerInvoker()
        {
            var method = AccessTools.Method("Mirror.NetworkServer:SendSpawnMessage");
            if (method == null)
            {
                throw new Exception("SendSpawnMessage method not found.");
            }

            _sendSpawnMessage = (SendSpawnMessageDelegate)Delegate.CreateDelegate(typeof(SendSpawnMessageDelegate), method);
        }

        public static void InvokeSendSpawnMessage(NetworkIdentity identity, NetworkConnection conn)
        {
            _sendSpawnMessage?.Invoke(identity, conn);
        }
    }
}
