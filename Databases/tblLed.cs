using iParking.Object;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace iParking.Databases
{
    public class tblLed
    {
        public static string TBL_NAME = "tbl_LED";
        public static string TBL_COL_NAME = "Name";
        public static string TBL_COL_SORT = "Sort";
        public static string TBL_COL_ID = "ID";
        public static string TBL_COL_CODE = "Code";
        public static string TBL_COL_DESCRIPTION = "Description";
        public static string TBL_COL_IP = "IP";
        public static string TBL_COL_Port = "Port";
        public static string TBL_COL_TYPE = "Type";
        public static string TBL_COL_ADDR = "Address";
        public static string TBL_COL_ARROW = "Arrow";
        public static string TBL_COL_COLOR = "Color";
        public static string TBL_COL_ZERO_COLOR = "ZeroColor";
        public static string TBL_COL_COMMUNICATION_TYPE = "CommunicationType";
        public static string TBL_COL_LED_FUNCTION = "LedFunction";

        public static string GetCMD = $@"Select {TBL_COL_ID},{TBL_COL_CODE},{TBL_COL_DESCRIPTION},{TBL_COL_NAME},
                                          {TBL_COL_IP},{TBL_COL_Port},{TBL_COL_TYPE},{TBL_COL_ADDR},
                                          {TBL_COL_ARROW},{TBL_COL_COLOR},{TBL_COL_ZERO_COLOR},
                                          {TBL_COL_COMMUNICATION_TYPE}
                                   from dbo.{TBL_NAME} order by {TBL_COL_ADDR}";
        public string  Id { get; set; }
        public int Sort { get; set; }
        public string Name { get; set; }
        public string Code { get; set; }
        public string Description { get; set; }
        public int Type { get; set; }
        public int CommunicationType { get; set; }
        public string IP { get; set; }
        public int Port { get; set; }
        public int Address { get; set; }
        public int Arrow { get; set; }
        public int Color { get; set; }
        public int ZeroColor { get; set; }

        public static LedCollection LoadDataLed(LedCollection ledCollection)
        {
            DataTable dtLed = StaticPool.mdb.FillData(GetCMD);
            ledCollection.Clear();
            if (dtLed != null)
            {
                if (dtLed.Rows.Count > 0) {
                    foreach (DataRow row in dtLed.Rows)
                    {
                        Led led = new Led();
                        led.Id = row[TBL_COL_ID].ToString();
                        led.Code = row[TBL_COL_CODE].ToString();
                        led.Description = row[TBL_COL_DESCRIPTION].ToString();
                        led.Name = row[TBL_COL_NAME].ToString();
                        led.IPAddress = row[TBL_COL_IP].ToString();
                        led.Port = Convert.ToInt32(row[TBL_COL_Port].ToString());
                        led.LedType = Convert.ToInt32(row[TBL_COL_TYPE].ToString());
                        led.CommunicationType = Convert.ToInt32(row[TBL_COL_COMMUNICATION_TYPE].ToString());
                        led.Address = Convert.ToInt32(row[TBL_COL_ADDR].ToString());

                        led.LedArrow = Convert.ToByte(row[TBL_COL_ARROW].ToString());
                        led.Color = Convert.ToByte(row[TBL_COL_COLOR].ToString());
                        led.ZeroColor = Convert.ToByte(row[TBL_COL_ZERO_COLOR].ToString());
                        ledCollection.Add(led);
                    }
                    dtLed.Dispose();
                    DataTable dtSlotCount = StaticPool.mdb.FillData($@"SELECT COUNT({tblLedDetail.TBL_COL_ZONEID}) as SlotCount,{tblLedDetail.TBL_COL_LedID}
                                                               FROM {tblLedDetail.TBL_NAME}
                                                               Group by {tblLedDetail.TBL_COL_LedID} ");
                    if (dtSlotCount != null)
                    {
                        if (dtSlotCount.Rows.Count > 0)
                        {
                            foreach (DataRow row in dtSlotCount.Rows)
                            {
                                string ledId = row[tblLedDetail.TBL_COL_LedID].ToString();
                                Led led = ledCollection.GetLed(ledId);
                                if (led != null)
                                {
                                    led.SlotCount = Convert.ToInt32(row["SlotCount"].ToString());
                                }
                            }
                        }
                    }
                    return ledCollection;
                }
                

            }
            return null;
        }
        //Add
        public static string InsertAndGetLastID(string code, string description, string outputName, string ip, int port, int type, int address, byte arrow, byte color, byte zeroColor, int communicationType)
        {
            string insertCMD = $@"DECLARE @generated_keys table([{TBL_COL_ID}] varchar(150))
                                  Insert into {TBL_NAME}({TBL_COL_CODE},{TBL_COL_DESCRIPTION},{TBL_COL_NAME},
                                                         {TBL_COL_IP},{TBL_COL_Port},{TBL_COL_TYPE},{TBL_COL_ADDR},
                                                         {TBL_COL_ARROW},{TBL_COL_COLOR},{TBL_COL_ZERO_COLOR},{TBL_COL_COMMUNICATION_TYPE})
                                  OUTPUT inserted.{TBL_COL_ID} Into @generated_keys
                                  values(N'{code}',N'{description}',N'{outputName}',
                                          '{ip}',{port},{type},{address},{Convert.ToInt32(arrow)},
                                           {Convert.ToInt32(color)},{Convert.ToInt32(zeroColor)},{communicationType})
                                  Select * from @generated_keys";
            DataTable dtbLastID = StaticPool.mdb.FillData(insertCMD);
            if ((dtbLastID == null))
            {
                dtbLastID = StaticPool.mdb.FillData(insertCMD);
                if ((dtbLastID == null))
                {
                    return "";
                }
            }
            return dtbLastID.Rows[0][TBL_COL_ID].ToString();
        }
        //Modify
        public static bool Modify(string id, string code, string description, string outputName, string ip, int port, int type, int address, byte arrow, byte color, byte zeroColor, int communicationType)
        {
            string modifyCMD = $@"update {TBL_NAME} 
                                  set {TBL_COL_CODE}=N'{code}',
                                      {TBL_COL_DESCRIPTION}=N'{description}',
                                      {TBL_COL_NAME}=N'{outputName}',
                                      {TBL_COL_IP} ='{ip}',
                                      {TBL_COL_Port} = {port},
                                      {TBL_COL_TYPE} = {type},
                                      {TBL_COL_ADDR} = {address},
                                      {TBL_COL_ARROW} = {Convert.ToInt32(arrow)},
                                      {TBL_COL_COLOR} = {Convert.ToInt32(color)},
                                      {TBL_COL_ZERO_COLOR} = {Convert.ToInt32(zeroColor)},
                                      {TBL_COL_COMMUNICATION_TYPE} = {communicationType}
                                  Where {TBL_COL_ID} = '{id}'";
            if (!StaticPool.mdb.ExecuteCommand(modifyCMD))
            {
                if (!StaticPool.mdb.ExecuteCommand(modifyCMD))
                {
                    return false;
                }
            }
            return true;
        }
        //Delete
        public static bool Delete(string ledID)
        {
            string deleteCMD = $"Delete {TBL_NAME} where {TBL_COL_ID}='{ledID}'";
            if (!StaticPool.mdb.ExecuteCommand(deleteCMD))
            {
                if (!StaticPool.mdb.ExecuteCommand(deleteCMD))
                {
                    return false;
                }
            }
            return true;
        }
    }
}
