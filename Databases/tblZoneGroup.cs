using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace iParking.Databases
{
    public class tblZoneGroup
    {
        public static string TBL_NAME = "tblZoneGroup";
        public static string TBL_COL_ID = "ID";
        public static string TBL_COL_NAME = "Name";
        public static string TBL_COL_CODE = "Code";
        public static string TBL_COL_DESCRIPTION = "Description";
        public static string TBL_COL_FLOOR_ID = "FloorID";
        public static string TBL_COL_SORT = "Sort";
        public static string TBL_COL_ISAUTOORDER = "isAutoOrder";
        public static string GetCMD = $"Select {TBL_COL_ID},{TBL_COL_NAME},{TBL_COL_CODE},{TBL_COL_DESCRIPTION},{TBL_COL_FLOOR_ID},{TBL_COL_ISAUTOORDER} from {TBL_NAME} order by {TBL_COL_SORT}";

        public string ID { get; set; }
        public int Sort { get; set; }
        public string Name { get; set; }
        public string Code { get; set; }
        public string Description { get; set; }
        public string FloorID { get; set; }

        //Get
        public static ZoneGroupCollection LoadDataGroup(ZoneGroupCollection zgCollection)
        {
            DataTable dtZg = StaticPool.mdb.FillData(GetCMD);
            zgCollection.Clear();
            if (dtZg != null)
            {
                if (dtZg.Rows.Count > 0)
                {
                    foreach (DataRow row in dtZg.Rows)
                    {
                        ZoneGroup zoneGroup = new ZoneGroup();
                        zoneGroup.Id = row[TBL_COL_ID].ToString();
                        zoneGroup.Code = row[TBL_COL_CODE].ToString();
                        zoneGroup.Description = row[TBL_COL_DESCRIPTION].ToString();
                        zoneGroup.FloorID = row[TBL_COL_FLOOR_ID].ToString();
                        zoneGroup.Name = row[TBL_COL_NAME].ToString();
                        zoneGroup.isAutoOrder = Convert.ToBoolean(row[TBL_COL_ISAUTOORDER].ToString());
                        zgCollection.Add(zoneGroup);
                    }
                    dtZg.Dispose();
                    return zgCollection;
                }
            }
            return null;
        }
        //Add
        public static string InsertAndGetLastID(string code, string description, string floorID, string zoneGroupName, int isAutoOrder)
        {
            string insertCMD = $@"DECLARE @generated_keys table([{TBL_COL_ID}] varchar(150))
                                  Insert into {TBL_NAME}({TBL_COL_CODE},{TBL_COL_DESCRIPTION},
                                                         {TBL_COL_FLOOR_ID},{TBL_COL_NAME},{TBL_COL_ISAUTOORDER})
                                  OUTPUT inserted.{TBL_COL_ID} 
                                  Into @generated_keys
                                  values(N'{code}',N'{description}','{floorID}',N'{zoneGroupName}',{isAutoOrder})
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
        public static bool Modify(string id, string code, string description, string floorID, string zoneGroupName, int isAutoOrder)
        {
            string modifyCMD = $@"update {TBL_NAME} 
                                  set {TBL_COL_CODE} = N'{code}',
                                      {TBL_COL_DESCRIPTION} = N'{description}',
                                      {TBL_COL_FLOOR_ID} = '{floorID}',
                                      {TBL_COL_NAME} = N'{zoneGroupName}',
                                      {TBL_COL_ISAUTOORDER} = {isAutoOrder}
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
        public static bool Delete(string groupID)
        {
            string deleteCMD = $"Delete {TBL_NAME} where {TBL_COL_ID} = '{groupID}'";
            if (!StaticPool.mdb.ExecuteCommand(deleteCMD))
            {
                if (!StaticPool.mdb.ExecuteCommand(deleteCMD))
                {
                    return false;
                }
            }
            return true;
        }
        //Update IsAutoOrder

        public static bool UpdateIsAutoOrder(bool isAutoOrder,string ID)
        {
            int _isAutoOrder = isAutoOrder == true ? 1 : 0;
            string updateCmd = $@"Update {TBL_NAME} set {TBL_COL_ISAUTOORDER}={_isAutoOrder} where {TBL_COL_ID} = '{ID}'";
            if (!StaticPool.mdb.ExecuteCommand(updateCmd))
            {
                if (!StaticPool.mdb.ExecuteCommand(updateCmd))
                {
                    return false;
                }
            }
            return true;
        }

        public static void CheckZoneAutoOrder(string ID)
        {
            DataTable dtCount = StaticPool.mdb.FillData($@"select COUNT(ID) as count from tblZone where tblZone.GroupID ='{ID}' and {TBL_COL_ISAUTOORDER}=1");
            int currentZoneAutoOrder = 0;
            bool refreshStatus = false;
            if (dtCount != null)
            {
                if (dtCount.Rows.Count > 0)
                {
                    currentZoneAutoOrder = Convert.ToInt32(dtCount.Rows[0]["count"].ToString());
                    if (currentZoneAutoOrder == 0)
                    {
                        refreshStatus = false;
                        UpdateIsAutoOrder(false, ID);
                    }
                    else
                    {
                        refreshStatus = true;
                        UpdateIsAutoOrder(true, ID);
                    }
                }
            }
            else
            {
                refreshStatus = false;
                UpdateIsAutoOrder(false, ID);
            }
            foreach(ZoneGroup zg in StaticPool.zoneGroupCollection)
            {
                if(zg.Id == ID)
                {
                    zg.isAutoOrder = refreshStatus;
                    return;
                }
            }
        }
    }
}
