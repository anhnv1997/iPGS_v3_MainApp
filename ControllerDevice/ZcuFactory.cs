using iParking.Enums;
using iParking.ZCU_Controller;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace iParking.Device
{
    public class ZcuFactory
    {
        public static IZCU GetZCU(ZCU zcu)
        {
            switch (zcu.Type)
            {
                case (int)EM_ZcuTypes.AI_TMA:
                    return new AI_TMA(zcu);
                //return new AI_TMA(zcu.IPAddress, zcu.Port, zcu.Username, zcu.Password, zcu.Id);
                case (int)EM_ZcuTypes.Camera:
                    return new Camera(zcu);
                case (int)EM_ZcuTypes.FuC02:
                    return new FuC02(zcu);
                default:
                    return null;
            }
        }
    }
}
