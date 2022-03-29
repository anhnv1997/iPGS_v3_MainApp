using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace iParking.Databases
{
    public class tblMapDetail
    {
        public static string TBL_NAME = "tblMapDetail";
        public static string TBL_COL_ID = "ID";
        public static string TBL_COL_MAPID = "MapID";
        public static string TBL_COL_ZONEID = "ZoneID";
        public static string TBL_COL_POSX = "ZonePosX";
        public static string TBL_COL_POSY = "ZonePosY";
        public static string TBL_COL_WIDTH = "ZoneWidth";
        public static string TBL_COL_HEIGHT = "ZoneHeight";
        public static string TBL_COL_MAP_WIDTH = "MapWidth";
        public static string TBL_COL_MAP_HEIGHT = "MapHeight";
        public static string GetCMD = $@"Select {TBL_COL_ID},{TBL_COL_MAPID},{TBL_COL_ZONEID},{TBL_COL_POSX},{TBL_COL_POSY},{TBL_COL_WIDTH},{TBL_COL_HEIGHT} ,{TBL_COL_MAP_WIDTH},{TBL_COL_MAP_HEIGHT}
                                                               from dbo.{TBL_NAME}";
        
        public string ID { get; set; }
        public string MapID { get; set; }
        public string ZoneID { get; set; }
        public float ZonePosX { get; set; }
        public float ZonePosY { get; set; }
        public int ZoneWidth { get; set; }
        public int ZoneHeight { get; set; }
        //Get
        public static MapDetailCollection LoadMapDetailData(MapDetailCollection mapDetailCollection)
        {
            
            DataTable dtMapDetail = StaticPool.mdb.FillData(GetCMD);
            mapDetailCollection.Clear();
            if (dtMapDetail != null)
            {
                if (dtMapDetail.Rows.Count > 0)
                {
                    foreach (DataRow row in dtMapDetail.Rows)
                    {
                        MapDetail mapDetail = new MapDetail();
                        mapDetail.Id = row[TBL_COL_ID].ToString();
                        mapDetail.MapId = row[TBL_COL_MAPID].ToString();
                        mapDetail.ZONEId = row[TBL_COL_ZONEID].ToString();
                        mapDetail.PosX = Convert.ToInt32(row[TBL_COL_POSX].ToString());
                        mapDetail.PosY = Convert.ToInt32(row[TBL_COL_POSY].ToString());
                        mapDetail.zoneWidth = Convert.ToInt32(row[TBL_COL_WIDTH].ToString());
                        mapDetail.zoneHeight = Convert.ToInt32(row[TBL_COL_HEIGHT].ToString());
                        mapDetail.picMapWidth = Convert.ToInt32(row[TBL_COL_MAP_WIDTH].ToString());
                        mapDetail.picMapHeight = Convert.ToInt32(row[TBL_COL_MAP_HEIGHT].ToString());
                        mapDetailCollection.Add(mapDetail);
                    }
                    dtMapDetail.Dispose();
                    return mapDetailCollection;
                }
            }
            return null;
        }
        //Add
        public static string InsertAndGetLastID(string mapID, string zoneID, int posX, int posY, int zoneWidth, int zoneHeight,int mapWidth, int mapHeight)
        {
            string insertCMD = $@"DECLARE @generated_keys table([{TBL_COL_ID}] varchar(150))
                                  Insert into {TBL_NAME}({TBL_COL_MAPID},{TBL_COL_ZONEID},{TBL_COL_POSX},{TBL_COL_POSY},{TBL_COL_WIDTH},{TBL_COL_HEIGHT},{TBL_COL_MAP_WIDTH},{TBL_COL_MAP_HEIGHT})
                                  OUTPUT inserted.{TBL_COL_ID} 
                                  Into @generated_keys
                                  Values('{mapID}','{zoneID}',{posX},{posY},{zoneWidth},{zoneHeight},{mapWidth},{mapHeight})
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
        public static bool Modify(string mapID, string zoneID, int posX, int posY, int zoneWidth, int zoneHeight,int mapWidth, int mapHeight)
        {
            string modifyCMD = @$"update {TBL_NAME} 
                                  set {TBL_COL_MAPID} = '{mapID}',
                                      {TBL_COL_ZONEID} = '{zoneID}',
                                      {TBL_COL_POSX} = {posX},
                                      {TBL_COL_POSY} = {posY},
                                      {TBL_COL_WIDTH} = {zoneWidth},
                                      {TBL_COL_HEIGHT} = {zoneHeight},
                                      {TBL_COL_MAP_WIDTH} = {mapWidth},
                                      {TBL_COL_MAP_HEIGHT} = {mapHeight}
                                  where {TBL_COL_MAPID} = '{mapID}' AND {TBL_COL_ZONEID} = '{zoneID}'";
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
        public static bool Delete(string zoneID, string mapID)
        {
            string deleteCMD = $"Delete {TBL_NAME} where {TBL_COL_ZONEID} = '{zoneID}' AND {TBL_COL_MAPID}='{mapID}'";
            if (!StaticPool.mdb.ExecuteCommand(deleteCMD))
            {
                if (!StaticPool.mdb.ExecuteCommand(deleteCMD))
                {
                    return false;
                }
            }
            return true;
        }
        public static bool DeleteByZoneID(string zoneID)
        {
            string deleteCMD = $"delete {tblMapDetail.TBL_NAME} where {tblMapDetail.TBL_NAME}.{tblMapDetail.TBL_COL_ZONEID} = '{zoneID}'";
            if (!StaticPool.mdb.ExecuteCommand(deleteCMD))
            {
                if (!StaticPool.mdb.ExecuteCommand(deleteCMD))
                {
                    return false;
                }
            }
            return true;
        }
        public static bool DeleteByMapID(string mapID)
        {
            string deleteCMD = $"delete {tblMapDetail.TBL_NAME} where {tblMapDetail.TBL_NAME}.{tblMapDetail.TBL_COL_MAPID} = '{mapID}'";
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
