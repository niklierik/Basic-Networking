using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dungeons.Networking.Messages
{
    public class ServerStopping : Multicast
    {
        public string Message { get; set; } = "Server stopping...";

        public ServerStopping()
        {
        }
    }
}
