using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Steamworks;
using Steamworks.Data;
using System.Linq;
using System;
using System.Runtime.InteropServices;

using Random = UnityEngine.Random;

namespace Dungeons.Networking
{

    public class DServer : SocketManager
    {
        public const ushort DefaultPort = 0;

        private static DServer _server;

        public static DServer Server
        {
            get
            {
                if (_server == null)
                {
                    throw new InvalidOperationException("Server has not been started.");
                }
                return _server;
            }
        }

        public static bool IsNull => _server is null;

        public static void Update()
        {
            var rnd = (byte) Random.Range((int) 0, (int) 128);
            Server.Receive();
            DebugNetworking.debug = rnd.ToString();
            foreach (var c in Server.Connections)
            {

                c.Value.Connection.SendMessage(new byte[] { rnd });
            }
        }

        public Dictionary<uint, DConnection> Connections
        {
            get; private set;
        } = new Dictionary<uint, DConnection>();

        public DConnection ConOf(Connection c)
        {
            return Connections[c];
        }


        public override void OnConnecting(Connection connection, ConnectionInfo info)
        {
            base.OnConnecting(connection, info);
            connection.Accept();
            Debug.Log($"{connection} is connecting...");
        }

        public override void OnConnected(Connection connection, ConnectionInfo info)
        {
            base.OnConnected(connection, info);
            Connections[connection.Id] = new DConnection(connection);
            Debug.Log($"{connection} is connected");
        }


        public override void OnConnectionChanged(Connection connection, ConnectionInfo info)
        {
            base.OnConnectionChanged(connection, info);
            Connections[connection.Id].OnConnectionChanged(info);
        }

        public override void OnDisconnected(Connection connection, ConnectionInfo info)
        {
            base.OnDisconnected(connection, info);
            Connections.Remove(connection.Id);
            Debug.Log($"{connection} is disconnected");
        }

        public override void OnMessage(Connection connection, NetIdentity identity, IntPtr data, int size, long messageNum, long recvTime, int channel)
        {
            base.OnMessage(connection, identity, data, size, messageNum, recvTime, channel);
        }

        public static void StartServer(ushort port = DefaultPort)
        {
            _server = SteamNetworkingSockets.CreateRelaySocket<DServer>(port);
            DClient.Connect(SteamClient.SteamId);
            // Debug.Log(_server.ToString());
        }

        public static void StopServer()
        {
            if (!IsNull)
            {
                _server.Stop();
            }
        }

        public void Stop()
        {
            foreach (var client in Connections)
            {

                client.Value.Connection.Close(false, 0, "Stopping server");
            }
            Close();
            _server = null;
        }
    }
}