using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace AI_TMA
{
    public class ParkingLotRec
    {
        [JsonProperty("operator")]
        public string action { get; set; } = "Parkinglot";

        public string cid { get; set; }

        public DateTime? created { get; set; }

        public List<ParkingLotRec_Result> result { get; set; }
    }

    public class ParkingLotRec_Result
    {
        public List<SlotStatus> slot_status { get; set; }

        public int stream_id { get; set; }

        public string timestamp { get; set; }
    }

    public class SlotStatus
    {
        public int num_slot { get; set; }

        public bool is_empty { get; set; }
    }
}
