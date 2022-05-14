using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dungeons.Networking.Messages
{
    [Serializable]
    public abstract class FromClientToServer : NetMessage
    {

        public virtual bool ServerValidate()
        {
            return true;
        }

        public void Send()
        {

        }

    }
}
