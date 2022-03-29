using System;
using System.Collections.Generic;
using System.Text;
using System.Collections;

namespace iParking
{
    public class CCUCollection : CollectionBase
    {
        // Constructor
        public CCUCollection()
        {

        }

        public CCU this[int index]
        {
            get { return (CCU)InnerList[index]; }
        }
        // Get ccu by it's ccuID
        public CCU Getccu(string ccuId)
        {
            foreach (CCU ccu in InnerList)
            {
                if (ccu.Id == ccuId)
                {
                    return ccu;
                }
            }
            return null;
        }

        // Add
        public void Add(CCU ccu)
        {
            InnerList.Add(ccu);
        }

        // Remove
        public void Remove(CCU ccu)
        {
            InnerList.Remove(ccu);
        }


    }
}
