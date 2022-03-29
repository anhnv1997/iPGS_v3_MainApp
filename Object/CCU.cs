using System;
using System.Collections.Generic;
using System.Text;

namespace iParking
{
    public class CCU
    {
        // Constructor
        public CCU()
        {

        }
        public string Name { get; set; }
        private string id = "";
        public string Id
        {
            get { return id; }
            set { id = value; }
        }

        private string code = "";
        public string Code
        {
            get { return code; }
            set { code = value; }
        }

        private string description = "";
        public string Description
        {
            get { return description; }
            set { description = value; }
        }

        private int ccuType = 0;
        public int CCUType
        {
            get { return ccuType; }
            set { ccuType = value; }
        }

        private int communicationType = 0;
        public int CommunicationType
        {
            get { return communicationType; }
            set { communicationType = value; }
        }

        private string comPort = "";
        public string ComPort
        {
            get { return comPort; }
            set { comPort = value; }
        }

        private string baudRate = "";
        public string BaudRate
        {
            get { return baudRate; }
            set { baudRate = value; }
        }
    }
}
