using Steamworks;
using Steamworks.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dungeons.Networking
{
    public class DConnection
    {

        public Connection Connection
        {
            get; private set;
        }

        public DConnection(Connection c)
        {
            Connection = c;
        }

        public SteamId Owner { get; private set; }

        public void OnConnectionChanged(ConnectionInfo info)
        {

        }

    }
}
