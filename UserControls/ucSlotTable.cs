using iParking.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace iParking.UserControls
{
    public partial class ucSlotTable : UserControl
    {
        public string ZoneID { get; set; }
        public string ZoneName { get; set; }
        public string FloorName { get; set; }
        public int Status { get; set; }
        public Timer delayTimer { get; set; }
        private bool isInit = false;
        private int countForWaiting = 0;
        private int currentStatus;
        public ucSlotTable(string zoneID, string zoneName, string floorName, int status)
        {
            InitializeComponent();
            CreateUcName(zoneID);
            this.ZoneID = ZoneID;
            this.ZoneName = zoneName;
            this.FloorName = floorName;
            this.Status = status;
            delayTimer = new Timer();
            delayTimer.Interval = 1000;
            delayTimer.Enabled = false;
            delayTimer.Tick += DelayTimer_Tick;
        }

        private void DelayTimer_Tick(object sender, EventArgs e)
        {
            countForWaiting++;
            if (countForWaiting >= 15)
            {
                countForWaiting = 0;
                lbZoneStatus?.Invoke(new Action(() =>
                {
                    lbZoneStatus.Text = Enum.GetName(typeof(EM_ZoneStatusType), currentStatus);
                }));
                delayTimer.Enabled = false;
            }
        }

        private void CreateUcName(string zoneID)
        {
            this.Name = $"SlotTable_Zone:{zoneID}";
        }

        private void ucSlotTable_Load(object sender, EventArgs e)
        {
            lbZoneName.Text = this.ZoneName;
            lbZoneStatus.Text = Enum.GetName(typeof(EM_ZoneStatusType), this.Status);
        }

        private void lbZoneStatus_TextChanged(object sender, EventArgs e)
        {
            if (lbZoneStatus.Text == EM_ZoneStatusType.UN_OCCUPIED.ToString())
            {
                lbZoneStatus.BackColor = Color.DarkGreen;
                lbZoneStatus.ForeColor = SystemColors.ControlLightLight;
                timerParkingTimeCount.Enabled = false;
                lbStartTime.Text = "";
                lbParkingTimeCount.Text = "";
            }
            else if (lbZoneStatus.Text == EM_ZoneStatusType.DISCONNECT.ToString())
            {
                lbZoneStatus.BackColor = Color.Gray;
                lbZoneStatus.ForeColor = lbZoneStatus.ForeColor = SystemColors.ControlLightLight;
            }
            else if (lbZoneStatus.Text == EM_ZoneStatusType.ORDER.ToString())
            {
                lbZoneStatus.ForeColor = Color.Black;
                lbZoneStatus.BackColor = Color.Yellow;
            }
            else if (lbZoneStatus.Text == ""|| lbZoneStatus.Text == "LOCK")
            {
                lbZoneStatus.Text = "LOCK";
                lbZoneStatus.BackColor = Color.Gray;
                lbZoneStatus.ForeColor = lbZoneStatus.ForeColor = SystemColors.ControlLightLight;
            }
            else if (lbZoneStatus.Text != "")
            {
                lbZoneStatus.BackColor = Color.DarkRed;
                lbZoneStatus.ForeColor = SystemColors.ControlLightLight;
                lbStartTime.Text = DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss");
                lbParkingTimeCount.Text = "00:00:00";
                timerParkingTimeCount.Enabled = true;
            }
            else
            {
                lbZoneStatus.BackColor = SystemColors.Control;
                lbStartTime.Text ="";
                lbParkingTimeCount.Text = "";
                timerParkingTimeCount.Enabled = false;
            }
        }
        public void UpdateStatus(int status)
        {
            if (!isInit)
            {
                lbZoneStatus?.Invoke(new Action(() =>
                {
                    lbZoneStatus.Text = Enum.GetName(typeof(EM_ZoneStatusType), status);
                }));
                isInit = true;
                currentStatus = status;
            }
            else
            {
                if (currentStatus != status)
                {
                    currentStatus = status;
                    countForWaiting = 0;
                    delayTimer.Enabled = true;
                }
            }

        }
        private void timerParkingTimeCount_Tick(object sender, EventArgs e)
        {
            string endTime = DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss");
            TimeSpan duration = DateTime.Parse(endTime).Subtract(DateTime.Parse(lbStartTime.Text));
            lbParkingTimeCount.Text = duration.Hours.ToString("00") + ":" + duration.Minutes.ToString("00") + ":" + duration.Seconds.ToString("00");
        }
    }
}
