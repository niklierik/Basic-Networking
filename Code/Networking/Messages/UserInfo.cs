using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dungeons.Networking.Messages
{
    [Serializable]
    public class UserInfo : FromClientToServer
    {
        public ulong SteamId { get; set; }

        public string Name { get; set; }

    }
}
