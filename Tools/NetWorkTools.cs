using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading.Tasks;

namespace iParking.Tools
{
    public class NetWorkTools
    {
        public static bool IsPingSuccess(string ipAddress, int timeOut)
        {

            Ping pingSender = new Ping();
            PingReply reply = null;
            reply = pingSender.Send(ipAddress, 500);
            if (reply != null && reply.Status == IPStatus.Success)
                return true;
            else
                return false;
        }
    }
}
