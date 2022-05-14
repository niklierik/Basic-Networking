using Dungeons.Utils;
using Steamworks;
using Steamworks.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Dungeons.Networking
{
    public class DClient : ConnectionManager
    {

        private static DClient _client;

        public static DClient Client
        {

            get
            {
                if (_client == null)
                {
                    throw new InvalidOperationException("Client is not connected to a server.");
                }
                return _client;
            }
        }

        public static bool IsNull => _client is null;

        public static void Connect(SteamId address, int port = DServer.DefaultPort)
        {
            _client = SteamNetworkingSockets.ConnectRelay<DClient>(address, port);
        }

        public static void Update()
        {
            Client.Receive();
        }

        public static void Disconnect()
        {
            if (!IsNull)
            {
                Client.Close();
                _client = null;
            }
        }

        public override void OnMessage(IntPtr data, int size, long messageNum, long recvTime, int channel)
        {
            base.OnMessage(data, size, messageNum, recvTime, channel);
            byte[] array = data.ToBytes(size);
            DebugNetworking.debug = array[0].ToString();
        }

        public override void OnConnecting(ConnectionInfo info)
        {
            base.OnConnecting(info);
            Debug.Log($"Connecting to {info.Address}.");
        }

        public override void OnConnected(ConnectionInfo info)
        {
            base.OnConnected(info);
            Debug.Log($"Connected to {info.Address}.");
        }

        public override void OnDisconnected(ConnectionInfo info)
        {
            base.OnDisconnected(info);
            Debug.Log($"Disconected to {info.Address}.");
        }
    }
}
