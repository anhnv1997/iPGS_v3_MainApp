using System;
using System.Collections.Generic;
using System.Text;

namespace AI_TMA
{
    public class ParkingLotTopics
    {
        public ParkingLotTopics(string deviceToken = "default_token")
        {
            _deviceToken = deviceToken;
        }

        public string _deviceToken = "default_token";

        public string ParkingLot_Rec { get => $"scp/device/{_deviceToken}/Parkinglot"; }
    }
}
