using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace iParking
{
    public class TMA_ServerCollection :CollectionBase
    {
        // Constructor
        public TMA_ServerCollection()
        {

        }

        public TMA_Server this[int index]
        {
            get { return (TMA_Server)InnerList[index]; }
        }

        // Add
        public void Add(TMA_Server _TMA_Server)
        {
            InnerList.Add(_TMA_Server);
        }

        // Remove
        public void Remove(TMA_Server _TMA_Server)
        {
            InnerList.Remove(_TMA_Server);
        }
        // Get
        public TMA_Server GetTMA_Server(string TMA_ServerID)
        {
            foreach (TMA_Server _TMA_Server in InnerList)
            {
                if (_TMA_Server.Id == TMA_ServerID)
                {
                    return _TMA_Server;
                }
            }
            return null;
        }
    }
}
