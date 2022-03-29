using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace iParking.Object
{
    public class OutputDetailCollection : CollectionBase
    {
        // Constructor
        public OutputDetailCollection()
        {

        }
        public OutputDetail this[int index]
        {
            get { return (OutputDetail)InnerList[index]; }
        }
        // Add
        public void Add(OutputDetail outputDetail)
        {
            InnerList.Add(outputDetail);
        }
        // Remove
        public void Remove(OutputDetail outputDetail)
        {
            InnerList.Remove(outputDetail);
        }
        public void RemoveByOutputID(string outputID)
        {
            List<OutputDetail> deleteOutputList = new List<OutputDetail>();
            foreach (OutputDetail outputDetail in InnerList)
            {
                if (outputDetail.OutputID == outputID)
                {
                    deleteOutputList.Add(outputDetail);
                }
            }
            foreach (OutputDetail outputDetail in deleteOutputList)
            {
                InnerList.Remove(outputDetail);
            }
        }
        // Get zone by it's zoneID
        public List<OutputDetail> GetOutputDetail(string outputID, string zoneID)
        {
            List<OutputDetail> outputDetails = new List<OutputDetail>();
            foreach (OutputDetail outputDetail in InnerList)
            {
                if (outputDetail.OutputID == outputID && outputDetail.ZoneID == zoneID)
                {
                    outputDetails.Add(outputDetail);
                }
            }
            return null;
        }

        //public List<int> GetRelayIndexs(string outputID, string zoneID)
        //{
        //    List<int> relays = new List<int>();
        //    foreach (OutputDetail outputDetail in InnerList)
        //    {
        //        if (outputDetail.OutputID == outputID && outputDetail.ZoneID == zoneID)
        //        {
        //            relays.Add(outputDetail.RelayIndex);
        //        }
        //    }
        //    return relays;
        //}
    }
}


