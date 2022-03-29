
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace iParking.Object
{
    public class LedCollection : CollectionBase
    {
        // Constructor
        public LedCollection()
        {

        }

        public Led this[int index]
        {
            get { return (Led)InnerList[index]; }
        }
        // Get zone by it's zoneID
        public Led GetLed(string ledID)
        {
            foreach (Led led in InnerList)
            {
                if (led.Id == ledID)
                {
                    return led;
                }
            }
            return null;
        }

        // Add
        public void Add(Led led)
        {
            InnerList.Add(led);
        }

        // Remove
        public void Remove(Led led)
        {
            InnerList.Remove(led);
        }
    }
}


