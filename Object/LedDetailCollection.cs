using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace iParking.Object
{
    public class LedDetailCollection : CollectionBase
    {
        // Constructor
        public LedDetailCollection()
        {

        }
        public LedDetail this[int index]
        {
            get { return (LedDetail)InnerList[index]; }
        }
        public LedDetail GetLedDetail(string ledID, string zoneID)
        {
            foreach (LedDetail ledDetail in InnerList)
            {
                if (ledDetail.LedID == ledID && ledDetail.ZoneID == zoneID)
                {
                    return ledDetail;
                }
            }
            return null;
        }

        // Add
        public void Add(LedDetail ledDetail)
        {
            InnerList.Add(ledDetail);
        }

        // Remove
        public void Remove(LedDetail ledDetail)
        {
            InnerList.Remove(ledDetail);
        }
        public void RemoveByZoneID(string zoneID)
        {
            List<LedDetail> deleteLedList = new List<LedDetail>();
            foreach (LedDetail ledDetail in InnerList)
            {
                if (ledDetail.ZoneID == zoneID)
                {
                    deleteLedList.Add(ledDetail);
                }
            }
            foreach (LedDetail ledDetail in deleteLedList)
            {
                InnerList.Remove(ledDetail);
            }
        }
        public void RemoveByLedID(string ledid)
        {
            List<LedDetail> deleteLedList = new List<LedDetail>();
            foreach (LedDetail ledDetail in InnerList)
            {
                if (ledDetail.LedID == ledid)
                {
                    deleteLedList.Add(ledDetail);
                }
            }
            foreach (LedDetail ledDetail in deleteLedList)
            {
                InnerList.Remove(ledDetail);
            }
        }
    }
}


