using iParking.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace iParking.Device
{
    public interface IZCU
    {
        event ZoneEvent zoneEvent;
        event StatusChangeEvent statusChangeEvent;
        bool Connect();
        // pooling start
        void PollingStart();
        // pooling stop
        void PollingStop();
        void GetZoneDetail(int zoneIndex, ref string plateNum, ref string imagePath);
    }
}
