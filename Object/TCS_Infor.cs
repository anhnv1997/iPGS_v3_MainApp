using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace iParking.Object
{
    public class TCS_Infor
    {
        public string EventType { get; set; } = "";
        public DateTime EventDate { get; set; } = DateTime.Now;
        public string SlotNumber { get; set; } = "";
        public string SlotName { get; set; } = "";
        public string VehicleNumber { get; set; } = "";
        public string CardNumber { get; set; } = "";
        public int VehicleType { get; set; } = -1;
        public string DriverID { get; set; } = "";
        public string DriverName { get; set; } = "";
        public DateTime RegistedDate { get; set; }
        public bool IsFree { get; set; } = false;
        public int GateIndex { get; set; } = -1;
    }
}
