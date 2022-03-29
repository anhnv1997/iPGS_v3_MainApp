using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace iParking.Object
{
    public class OutputCollection : CollectionBase
    {
        // Constructor
        public OutputCollection()
        {

        }

        public Output this[int index]
        {
            get { return (Output)InnerList[index]; }
        }

        // Add
        public void Add(Output output)
        {
            InnerList.Add(output);
        }

        // Remove
        public void Remove(Output output)
        {
            InnerList.Remove(output);
        }

        // Get zone by it's zoneID
        public Output GetOutput(string outputID)
        {
            foreach (Output output in InnerList)
            {
                if (output.ID == outputID)
                {
                    return output;
                }
            }
            return null;
        }
    }
}


