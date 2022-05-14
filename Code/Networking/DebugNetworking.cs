using Steamworks;
using Steamworks.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TMPro;
using UnityEngine;
using UnityEngine.UI;




namespace Dungeons.Networking
{/// <summary>
/// This class is for only Debugging. It has horrible code.
/// </summary>
    public class DebugNetworking : MonoBehaviour
    {

        public TMP_InputField ip;
        public TMP_InputField port;

        public Button hostBtn;
        public Button shutdownBtn;
        public Button connectBtn;
        public Button activateDebugBtn;
        public Text activateDebugTxt;

        public static bool debugging = false;

        public static string debug
        {
            set
            {
                if (debugging)
                {
                    Debug.Log(value);
                }
            }
        }


        private void Start()
        {
            Debug.Log(SteamClient.SteamId);
            activateDebugBtn.onClick.AddListener(() =>
            {
                debugging = !debugging;
            });
        }

        public void OnConnectBtn()
        {
            try
            {
                DClient.Connect(ulong.Parse(ip.text), port.text.Equals("") ? DServer.DefaultPort : ushort.Parse(port.text));
            }
            catch (Exception e)
            {
                Debug.LogException(e);
            }
        }

        public void OnCreateServerBtn()
        {
            DServer.StartServer();
        }

        public void OnShutdownBtn()
        {
            DServer.StopServer();
            DClient.Disconnect();
        }

        public void Update()
        {
            hostBtn.enabled = DServer.IsNull && DClient.IsNull;
            connectBtn.enabled = DServer.IsNull && DClient.IsNull;
            activateDebugTxt.text = debugging ? "Disable debugging" : "Activate debugging";
        }

    }
}
