using iParking.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace iParking.Object
{
    public class Output
    {
        public Output(int outputType)
        {
            SlotCounts = new Dictionary<int, int>();
            this.OutputType = outputType;
            for (int i = 1; i <= 8; i++)
            {
                SlotCounts.Add(i, 0);
            }
            if (outputType == (int)EM_OutputTypes.KZ_IO1616)
            {
                for (int i = 9; i <= 16; i++)
                {
                    SlotCounts.Add(i, 0);
                }
            }
        }
        public string ID { get; set; }
        public string Code { get; set; }
        public string Description { get; set; }
        public string Name { get; set; }

        public string IPAddress { get; set; }
        public int Port { get; set; }

        public int OutputType { get; set; }
        public Dictionary<int,int> SlotCounts { get; set; }
    }
}
