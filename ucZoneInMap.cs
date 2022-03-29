using iParking.Device;
using iParking.Enums;
using iParking.Events;
using iParking.Object;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace iParking
{
    public partial class ucZoneInMap : UserControl
    {
        #region:Properties
        private string zoneName;
        private string mapID = "";
        private string zoneID = "";
        //reSize
        public bool MouseInLeftEdge, MouseInRightEdge, MouseInTopEdge, MouseInBottomEdge;
        bool LeftEdge, RightEdge, TopEdge, BottomEdge, TopLeftCorner, BottomLeftCorner, TopRighttCorner, BottomRightCorner;
        bool isResizing, isMoving;
        private static Point _cursorStartPoint;
        //Event
        public event DeleteZoneInMapHandler DeleteZoneEvent;
        public event ShowMapDetailInfor ShowMapDetailInforEvent;
        public event SetZoneStatusByHandHandler SetZoneStatusByHand;
        public event ReleaseOrderEventHandler ReleaseOrderEvent;
        //
        private bool isInit = false;
        private int countForWaiting = 0;
        private int countForReleaseOrderEvent = 0;
        private Color currentColor;
        #endregion

        #region:Getter, Setter
        public string MapID { get => mapID; set => mapID = value; }
        public string ZoneID { get => zoneID; set => zoneID = value; }
        #endregion

        #region:Contructor
        public ucZoneInMap(string _zoneName)
        {
            InitializeComponent();
            this.zoneName = _zoneName;
        }
        #endregion

        #region:Event
        public void AssignHover()
        {
            lblZoneName.MouseHover += lblZoneName_MouseHover;
        }
        public void AsignAllowDrag()
        {
            lblZoneName.MouseMove += picZoneStatus_MouseMove;
            lblZoneName.MouseDown += picZoneStatus_MouseDown;
            lblZoneName.MouseUp += picZoneStatus_MouseUp;
        }

        public void AsignDeleteZoneInMap()
        {
            lblZoneName.MouseClick += lblZoneName_MouseClick;
        }
        public void AsignSetZoneByHand()
        {
            lblZoneName.MouseClick += lblZoneName_SetZoneByHand;
        }
        public static void ScaleLabel(Label label, float stepSize = 0.5f)
        {
            if (label.Width == 0)
            {
                return;
            }
            if (label.Width * label.Height > label.Font.Size * label.Text.Length)
            {
                while (label.Width * label.Height > label.Font.Size * label.Text.Length * label.Font.Height)
                {
                    label.Font = new Font(label.Font.FontFamily, label.Font.Size + stepSize, label.Font.Style);
                }

            }
            else
            {
                while (label.Font.Size > 0.5 && label.Width * label.Height < label.Font.Size * label.Text.Length * label.Font.Height)
                {
                    label.Font = new Font(label.Font.FontFamily, label.Font.Size - stepSize, label.Font.Style);
                }
            }
            while ((label.Width) < label.Font.Size * 1.5 && label.Font.Size > 0.5)
            {
                label.Font = new Font(label.Font.FontFamily, label.Font.Size - stepSize, label.Font.Style);
            }

            int textInRow = (int)((label.Width) / label.Font.Size);

            int textLine = (label.Text.Length / textInRow) - (int)(label.Text.Length / textInRow) > 0 ? (int)(label.Text.Length / textInRow) + 1 : (int)(label.Text.Length / textInRow);
            while (label.Height < (label.Font.Height * 1.5) * (textLine + 1) && label.Font.Size > 0.5)
            {
                textInRow = (int)((label.Width) / label.Font.Size);
                if (textInRow == 0)
                {
                    return;
                }
                textLine = (label.Text.Length / textInRow) - (int)(label.Text.Length / textInRow) > 0 ? (int)(label.Text.Length / textInRow) + 1 : (int)(label.Text.Length / textInRow);
                label.Font = new Font(label.Font.FontFamily, label.Font.Size - stepSize, label.Font.Style);
            }
            //if (label.Font.Size > 2 && label.Height>1.5 * label.Height)
            //{
            //    label.Font = new Font(label.Font.FontFamily, label.Font.Size - 2, label.Font.Style);
            //}
        }

        private void lblZoneName_Paint(object sender, PaintEventArgs e)
        {
        }

        private void ucZoneInMap_SizeChanged(object sender, EventArgs e)
        {
            string data = lblZoneName.Text;
            if (lblZoneName.Text != "")
            {
                if (this.Size.Width > 0)
                    ScaleLabel(lblZoneName);
            }
        }

        private void lblZoneName_TextChanged(object sender, EventArgs e)
        {
            ScaleLabel(lblZoneName);
        }

        public void AssignBlink()
        {
            timerBlink.Enabled = true;
        }
        #endregion

        #region:Form
        private void ucZoneInMap_Load(object sender, EventArgs e)
        {
            lblZoneName.Text = this.zoneName;
            ZONE zone = StaticPool.zoneCollection.GetZONE(this.zoneID);
            if (zone != null)
            {
                switch (zone.Status)
                {
                    case (int)EM_ZoneStatusType.UN_OCCUPIED:
                        lblZoneName.BackColor = Color.DarkGreen;
                        lblZoneName.ForeColor = SystemColors.ControlLightLight;
                        break;
                    case (int)EM_ZoneStatusType.DISCONNECT:
                        lblZoneName.BackColor = Color.Gray;
                        lblZoneName.ForeColor = SystemColors.ControlLightLight;
                        break;
                    case (int)EM_ZoneStatusType.ORDER:
                        lblZoneName.BackColor = Color.Yellow;
                        lblZoneName.ForeColor = Color.Black;
                        break;
                    case (int)EM_ZoneStatusType.OCCUPIED:
                        lblZoneName.BackColor = Color.DarkRed;
                        lblZoneName.ForeColor = SystemColors.ControlLightLight;
                        break;
                    default:
                        lblZoneName.BackColor = Color.Gray;
                        lblZoneName.ForeColor = SystemColors.ControlLightLight;
                        break;
                }
            }
            lblZoneName.DoubleClick += lblZoneName_DoubleClick;
            currentColor = lblZoneName.BackColor;
            //if (lblZoneName.BackColor == Color.Yellow)
            //{
            //    timerReleaseOrderState.Enabled = true;
            //}
            //else if (timerReleaseOrderState.Enabled == true)
            //{
            //    timerReleaseOrderState.Enabled = false;
            //    countForReleaseOrderEvent = 0;
            //}

        }

        #endregion

        #region:Control In Form
        private void picZoneStatus_MouseMove(object sender, MouseEventArgs e)
        {
            try
            {
                if (!isResizing && !isMoving)
                {
                    MouseInLeftEdge = Math.Abs(e.X) <= 2;
                    MouseInRightEdge = Math.Abs(e.X - this.Width) <= 5;
                    MouseInTopEdge = Math.Abs(e.Y) <= 2;
                    MouseInBottomEdge = Math.Abs(e.Y - this.Height) <= 5;
                    LeftEdge = MouseInLeftEdge == true && MouseInTopEdge == false && MouseInBottomEdge == false;
                    RightEdge = MouseInRightEdge == true && MouseInTopEdge == false && MouseInBottomEdge == false;
                    TopEdge = MouseInLeftEdge == false && MouseInRightEdge == false && MouseInTopEdge == true;
                    BottomEdge = MouseInBottomEdge == true && MouseInLeftEdge == false && MouseInRightEdge == false;
                    TopLeftCorner = MouseInLeftEdge == true && MouseInTopEdge == true;
                    BottomLeftCorner = MouseInLeftEdge == true && MouseInBottomEdge == true;
                    TopRighttCorner = MouseInRightEdge == true && MouseInTopEdge == true;
                    BottomRightCorner = MouseInRightEdge == true && MouseInBottomEdge == true;
                    if (LeftEdge)
                    {
                        this.Cursor = Cursors.SizeWE;
                    }
                    else if (RightEdge) { this.Cursor = Cursors.SizeWE; }
                    else if (TopEdge) { this.Cursor = Cursors.SizeNS; }
                    else if (BottomEdge) { this.Cursor = Cursors.SizeNS; }
                    else if (TopLeftCorner) { this.Cursor = Cursors.SizeNWSE; }
                    else if (BottomLeftCorner) { this.Cursor = Cursors.SizeNESW; }
                    else if (TopRighttCorner) { this.Cursor = Cursors.SizeNESW; }
                    else if (BottomRightCorner) { this.Cursor = Cursors.SizeNWSE; }
                    else { this.Cursor = Cursors.Hand; }
                    _cursorStartPoint = new Point(e.X, e.Y);
                }
                else
                {
                    int X = this.Width;
                    int Y = this.Height;
                    if (isResizing)
                    {
                        if (LeftEdge)
                        {
                            this.Location = new Point(e.X + this.Location.X, this.Location.Y);
                            this.Size = new Size(this.Width - e.X, this.Height);
                        }
                        else if (RightEdge)
                        {
                            this.Size = new Size(e.X, this.Height);
                        }
                        else if (TopEdge)
                        {
                            this.Location = new Point(this.Location.X, e.Y + this.Location.Y);
                            this.Size = new Size(this.Width, this.Height - e.Y);
                        }
                        else if (BottomEdge)
                        {
                            this.Size = new Size(this.Width, e.Y);
                        }
                        else if (TopLeftCorner)
                        {
                            this.Location = new Point(e.X + this.Location.X, e.Y + this.Location.Y);
                            this.Size = new Size(this.Width - e.X, this.Height - e.Y);
                        }
                        else if (BottomLeftCorner)
                        {
                            this.Location = new Point(e.X + this.Location.X, this.Location.Y);
                            this.Size = new Size(this.Width - e.X, e.Y);
                        }
                        else if (TopRighttCorner)
                        {
                            this.Location = new Point(this.Location.X, e.Y + this.Location.Y);
                            this.Size = new Size(e.X, this.Height - e.Y);
                        }
                        else if (BottomRightCorner)
                        {
                            this.Location = new Point(this.Location.X, this.Location.Y);
                            this.Size = new Size(e.X, e.Y);
                        }
                    }
                    else if (isMoving)
                    {
                        int x = (e.X - _cursorStartPoint.X) + this.Left;
                        int y = (e.Y - _cursorStartPoint.Y) + this.Top;
                        this.Location = new Point(x, y);
                    }
                }
            }
            catch (Exception)
            {
                isMoving = false;
                isResizing = false;
            }


        }


        private void picZoneStatus_MouseUp(object sender, MouseEventArgs e)
        {
            bool isExist = false;
            isResizing = false;
            isMoving = false;
        }
        private void picZoneStatus_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                if (MouseInLeftEdge || MouseInRightEdge || MouseInTopEdge || MouseInBottomEdge)
                {
                    isResizing = true;
                    isMoving = false;
                }
                else
                {
                    isResizing = false;
                    isMoving = true;
                }
            }
        }

        private void lblZoneName_DoubleClick(object sender, EventArgs e)
        {
            ZONE zone = StaticPool.zoneCollection.GetZONE(this.zoneID);
            if (zone != null)
            {
                ZCU zcu = StaticPool.zcuCollection.GetZCUById(zone.ZCUId);
                if (zcu != null)
                {
                    IZCU iZCU = ZcuFactory.GetZCU(zcu);
                    string plateNum = "";
                    string imagePath = "";
                    iZCU.GetZoneDetail(zone.ZcuIndex, ref plateNum, ref imagePath);
                    ShowMapDetailInforEventArgs eventArgs = new ShowMapDetailInforEventArgs();
                    eventArgs.zoneID = this.zoneID;
                    eventArgs.mapID = this.mapID;
                    eventArgs.zoneStatus = zone.Status;
                    eventArgs.imagePath = imagePath;
                    eventArgs.plateNum = plateNum;
                    eventArgs.zcuID = zcu.Id;
                    ShowMapDetailInforEvent?.Invoke(this, eventArgs);
                }
            }
        }
        private void lblZoneName_MouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                ContextMenuStrip menu = new ContextMenuStrip();
                menu.Items.Add("Delete").Name = "Delete";
                menu.Show(lblZoneName, new Point(e.X, e.Y));
                menu.ItemClicked += Menu_ItemClicked_Delete;
            }
        }
        private void lblZoneName_SetZoneByHand(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                ContextMenuStrip menu = new ContextMenuStrip();
                menu.Items.Add("Set Zone Status: Occupied AND UnUseAI").Name = "Occupied_UnUseAI";
                menu.Items.Add("Set Zone Status: UnOccupied AND UnUseAI").Name = "UnOccupied_UnUseAI";
                menu.Items.Add("Set Zone Status: Order AND UnUseAI").Name = "Order_UnUseAI";
                menu.Items.Add("Set Zone Status: Occupied AND UseAI").Name = "Occupied_UseAI";
                menu.Items.Add("Set Zone Status: UnOccupied AND UseAI").Name = "UnOccupied_UseAI";
                menu.Items.Add("Set Zone Status: Order AND UseAI").Name = "Order_UseAI";
                menu.Show(lblZoneName, new Point(e.X, e.Y));
                menu.ItemClicked += Menu_ItemClicked_SetZoneByHand;
            }
        }
        private void lblZoneName_MouseHover(object sender, EventArgs e)
        {
            ZONE zone = StaticPool.zoneCollection.GetZONE(this.zoneID);
            if (zone != null)
            {
                ZoneGroup zoneGroup = StaticPool.zoneGroupCollection.GetzgById(zone.ZoneGroupId);
                if (zoneGroup != null)
                {
                    Floor floor = StaticPool.floorCollection.GetFloor(zoneGroup.FloorID);
                    string floorName = "";
                    if (floor != null)
                    {
                        floorName = floor.Name;
                        toolTip1.Show($"Floor: {floorName}\r\nGroup: {zoneGroup.Name}\r\nZone : {zone.zoneName}", lblZoneName);
                    }
                }
            }
        }
        #endregion

        #region :Timer
        private void timerBlink_Tick(object sender, EventArgs e)
        {
            if (this.Visible == true)
            {
                this.Visible = false;
                return;
            }
            this.Visible = true;
        }
        private void timerWaitForCheckUpdateStatus_Tick(object sender, EventArgs e)
        {
            countForWaiting++;
            if (countForWaiting >= Properties.Settings.Default.minRefreshStateTime)
            {
                countForWaiting = 0;
                lblZoneName.BackColor = currentColor;
                if (currentColor == Color.DarkGreen || currentColor == Color.DarkRed || currentColor == Color.Gray)
                    lblZoneName.ForeColor = SystemColors.ControlLightLight;
                else
                    lblZoneName.ForeColor = Color.Black;
                timerWaitForCheckUpdateStatus.Enabled = false;
                //if (currentColor == Color.Yellow)
                //{
                //    timerReleaseOrderState.Enabled = true;
                //}
                //else if(timerReleaseOrderState.Enabled == true)
                //{
                //    timerReleaseOrderState.Enabled = false;
                //    countForReleaseOrderEvent = 0;
                //}
            }
        }

        private void timerReleaseOrderState_Tick(object sender, EventArgs e)
        {
            //countForReleaseOrderEvent++;
            //if (countForReleaseOrderEvent >= Properties.Settings.Default.maxOrderStateKeepTime * 5)
            //{
            //    timerReleaseOrderState.Enabled = false;
            //    lblZoneName.BackColor = Color.DarkGreen;
            //    lblZoneName.ForeColor = SystemColors.ControlLightLight;
            //    countForReleaseOrderEvent = 0;

            //    ReleaseOrderEventArgs releaseOrderEventArgs = new ReleaseOrderEventArgs()
            //    {
            //        ZoneID = this.zoneID
            //    };
            //    ReleaseOrderEvent?.Invoke(this, releaseOrderEventArgs);
            //}
        }
        #endregion


        #region:Internal
        public void SetStatus(Color color)
        {
            //Lần đầu mở phần mềm, cho cập nhật trạng thái ngay lập tức
            //Từ lần t2 đợi 15s, nếu trong 15s không đổi thì mới cập nhật trạng thái.
            //if (!isInit)
            //{
            //    lblZoneName.BackColor = color;
            //    if (color == Color.DarkGreen || color == Color.DarkRed || color == Color.Gray)
            //        lblZoneName.ForeColor = SystemColors.ControlLightLight;
            //    else
            //        lblZoneName.ForeColor = Color.Black;
            //    isInit = true;
            //    currentColor = color;
            //    if (currentColor == Color.Yellow)
            //    {
            //        timerReleaseOrderState.Enabled = true;
            //    }
            //    else if (timerReleaseOrderState.Enabled == true)
            //    {
            //        timerReleaseOrderState.Enabled = false;
            //        countForReleaseOrderEvent = 0;
            //    }
            //}
            //else
            {
                if (currentColor != color)
                {
                    currentColor = color;
                    countForWaiting = 0;
                    timerWaitForCheckUpdateStatus.Enabled = true;
                }
            }
        }
        private void Menu_ItemClicked_Delete(object sender, ToolStripItemClickedEventArgs e)
        {
            switch (e.ClickedItem.Name.ToString())
            {
                case "Delete":
                    DeleteZoneInMapEventArgs deleteEventArgs = new DeleteZoneInMapEventArgs(this.zoneID, this.mapID);
                    DeleteZoneEvent?.Invoke(this, deleteEventArgs);
                    break;
            }
        }
        private void Menu_ItemClicked_SetZoneByHand(object sender, ToolStripItemClickedEventArgs e)
        {
            bool isUseAI = false;
            EM_ZoneStatusType updateStatus = 0;
            switch (e.ClickedItem.Name.ToString())
            {
                case "Occupied_UnUseAI":
                    updateStatus = EM_ZoneStatusType.OCCUPIED;
                    isUseAI = false;
                    break;
                case "UnOccupied_UnUseAI":
                    updateStatus = EM_ZoneStatusType.UN_OCCUPIED;
                    isUseAI = false;
                    break;
                case "Order_UnUseAI":
                    updateStatus = EM_ZoneStatusType.ORDER;
                    isUseAI = false;
                    break;
                case "Occupied_UseAI":
                    updateStatus = EM_ZoneStatusType.OCCUPIED;
                    isUseAI = true;
                    break;
                case "UnOccupied_UseAI":
                    updateStatus = EM_ZoneStatusType.UN_OCCUPIED;
                    isUseAI = true;
                    break;
                case "Order_UseAI":
                    updateStatus = EM_ZoneStatusType.ORDER;
                    isUseAI = true;
                    break;
            }
            SetZoneStatusByHandEventArgs setzoneEventArgs = new SetZoneStatusByHandEventArgs()
            {
                Status = updateStatus,
                ZoneID = this.zoneID,
                isUseAI = isUseAI

            };
            SetZoneStatusByHand?.Invoke(this, setzoneEventArgs);
        }

        #endregion
    }
}
