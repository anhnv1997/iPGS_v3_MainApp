using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace iParking.Object
{
    public class VehicleTypeCollection : CollectionBase
    {
        // Constructor
        public VehicleTypeCollection()
        {

        }
        public VehicleType this[int index]
        {
            get { return (VehicleType)InnerList[index]; }
        }
        // Add
        public void Add(VehicleType type)
        {
            InnerList.Add(type);
        }
        // Remove
        public void Remove(VehicleType type)
        {
            InnerList.Remove(type);
        }
        // Get zg by it's zgID
        public VehicleType GetVehicleTypeByID(string typeID)
        {
            foreach (VehicleType vehicleType in InnerList)
            {
                if (vehicleType.ID == typeID)
                {
                    return vehicleType;
                }
            }
            return null;
        }
    }
}