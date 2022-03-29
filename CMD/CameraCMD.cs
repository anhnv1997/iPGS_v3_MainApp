using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace iParking.CMD
{
    public class CameraCMD
    {
        public static string GetAllSensorCMD()
        {
            return "GetStateAllSensor?/";
        }
        public static string GetZoneDetail(string zcuIP, int zoneIndex)
        {
            string index = zoneIndex + "";
            if (index.Length < 2)
            {
                index = "0" + index;
            }
            return $"GetInforFrom{zcuIP}At{index}";
        }
    }
}
