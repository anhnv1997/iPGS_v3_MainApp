using iParking.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace iParking.Object
{
    public class Led
    {
        // Constructor
        public Led()
        {

        }

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
        
        private string name = "";
        public string Name
        {
            get { return name; }
            set { name = value; }
        }
        
        private string description = "";
        public string Description
        {
            get { return description; }
            set { description = value; }
        }
        
        private string ccuId = "";
        public string CCUId
        {
            get { return ccuId; }
            set { ccuId = value; }
        }
        
        private int address = 0;
        public int Address
        {
            get { return address; }
            set { address = value; }
        }
        
        private bool isConnect = false;
        public bool IsConnect
        {
            get { return isConnect; }
            set { isConnect = value; }
        }
        
        public string IPAddress { get; set; } = "";
        
        public int Port { get; set; } = 0;
        
        private int communicationType = 0;
        public int CommunicationType { get => communicationType; set => communicationType = value; }
        
        public int LedType { get; set; } = 0;
        
        #region:Arrow
        private byte ledArrow = LedArrowDirection.GetLedArrowDirection(EM_LedArrowDirection.RIGHT_SIDE_and_RIGHT_TO_LEFT);
        public byte LedArrow
        {
            get { return ledArrow; }
            set { ledArrow = value; }
        }
        #endregion

        #region:Color
        private byte color = LedColor.GetLedColor(EM_LedColor.RED);
        public byte Color
        {
            get { return color; }
            set { color = value; }
        }

        private byte zeroColor = LedColor.GetLedColor(EM_LedColor.RED);
        public byte ZeroColor
        {
            get { return zeroColor; }
            set { zeroColor = value; }
        }
        #endregion

        #region:Parking Space
        private int parkingSpaceMax = 0;
        public int ParkingSpaceMax
        {
            get { return parkingSpaceMax; }
            set { parkingSpaceMax = value; }
        }
        public int OldParkingSpace { get; set; } = -1;
        // Cho chong hien tai
        private int parkingSpace = 0;
        public int ParkingSpace
        {
            get { return parkingSpace; }
            set { parkingSpace = value; }
        }
        #endregion
       
        public int SlotCount { get; set; }
        public int LedFunction { get; set; }
    }
}

