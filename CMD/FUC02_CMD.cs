using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace iParking.CMD
{
    public class FUC02_CMD
    {
        public static string GetAllSensorCMD()
        {
            return "GetStateAllSensor?/";
        }
        public static string GetZoneDetail(string zcuIP, int zoneIndex)
        {
            return "";
        }
    }
}
