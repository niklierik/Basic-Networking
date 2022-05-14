//#define SHIPPING
//#define STEAMWORKS_DEBUG

using Dungeons.Networking;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using Steamworks;

namespace Dungeons
{
    public class Game : MonoBehaviour
    {

        public const int AppID = 705210;

        public static Game Instance { get; private set; }

        public SteamEvents SteamEvents { get; private set; } = new SteamEvents();

        public void Awake()
        {
            if (Instance)
            {
                Destroy(gameObject);
                return;
            }
            Instance = this;
            DontDestroyOnLoad(Instance);
            Init();
        }

        public void Init()
        {
#if SHIPPING
            SteamClient.RestartAppIfNecessary(AppID);
#endif
            try
            {
                SteamClient.Init(AppID);
            }
            catch (Exception e)
            {
                Debug.LogException(e);
            }
#if SHIPPING
            SteamNetworkingUtils.DebugLevel = NetDebugOutput.Important;
#elif STEAMWORKS_DEBUG
            SteamNetworkingUtils.DebugLevel = NetDebugOutput.Everything;
#endif
            SteamNetworking.AllowP2PPacketRelay(true);
            SteamNetworkingUtils.InitRelayNetworkAccess();
            SteamNetworkingUtils.OnDebugOutput += (type, text) =>
            {
                switch (type)
                {
                    case NetDebugOutput.Bug:
                    case NetDebugOutput.Error:
                        Debug.LogError($"[{type}] {text}");
                        break;
                    case NetDebugOutput.Warning:
                    case NetDebugOutput.Important:
                        Debug.LogWarning($"[{type}] {text}");
                        break;
                    default:
                        Debug.Log($"[{type}] {text}");
                        break;
                }
            };

        }

        public void Update()
        {
            if (!DServer.IsNull)
            {
                DServer.Update();
            }
            if (!DClient.IsNull)
            {
                DClient.Update();
            }
        }

        private void OnDisable()
        {
            try
            {
                SteamClient.Shutdown();
            }
            catch { }
        }

        private void OnDestroy()
        {
            try
            {
                SteamClient.Shutdown();
            }
            catch { }
        }

        private void OnApplicationQuit()
        {
            try
            {
                SteamClient.Shutdown();
            }
            catch { }
        }
    }
}
