using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace iParking.Object
{
    public class VehicleZoneDetailCollection : CollectionBase
    {
        // Constructor
        public VehicleZoneDetailCollection()
        {

        }

        public VehicleZoneDetail this[int index]
        {
            get { return (VehicleZoneDetail)InnerList[index]; }
        }

        // Add
        public void Add(VehicleZoneDetail _VehicleZoneDetail)
        {
            InnerList.Add(_VehicleZoneDetail);
        }

        // Remove
        public void Remove(VehicleZoneDetail _VehicleZoneDetail)
        {
            InnerList.Remove(_VehicleZoneDetail);
        }
        // Get
        public VehicleZoneDetail GetVehicleZoneDetailByID(string vehicleZoneDetailID)
        {
            foreach (VehicleZoneDetail _VehicleZoneDetail in InnerList)
            {
                if (_VehicleZoneDetail.ID == vehicleZoneDetailID)
                {
                    return _VehicleZoneDetail;
                }
            }
            return null;
        }
        public VehicleZoneDetail GetVehicleZoneDetail(string vehicleID, string zoneID)
        {
            foreach (VehicleZoneDetail _VehicleZoneDetail in InnerList)
            {
                if (_VehicleZoneDetail.ZoneID == zoneID && _VehicleZoneDetail.VehicleTypeID ==vehicleID)
                {
                    return _VehicleZoneDetail;
                }
            }
            return null;
        }

        public VehicleZoneDetail GetVehicleZoneDetailWithStatus(string vehicleID, string zoneID, int status)
        {
            foreach (VehicleZoneDetail _VehicleZoneDetail in InnerList)
            {
                if (_VehicleZoneDetail.ZoneID == zoneID && _VehicleZoneDetail.VehicleTypeID == vehicleID)
                {
                    ZONE zone = StaticPool.zoneCollection.GetZONE(zoneID);
                    if (zone != null)
                    {
                        if (zone.Status == status)
                        {
                            return _VehicleZoneDetail;
                        }
                    }
                }
            }
            return null;
        }

        public VehicleZoneDetail GetVehicleZoneDetailWithStatus(string vehicleZoneDetailID, int status)
        {
            foreach (VehicleZoneDetail _VehicleZoneDetail in InnerList)
            {
                if (_VehicleZoneDetail.ID == vehicleZoneDetailID)
                {
                    ZONE zone = StaticPool.zoneCollection.GetZONE(_VehicleZoneDetail.ZoneID);
                    if (zone != null)
                    {
                        if (zone.Status == status)
                        {
                            return _VehicleZoneDetail;
                        }
                    }
                }
            }
            return null;
        }

    }
}
