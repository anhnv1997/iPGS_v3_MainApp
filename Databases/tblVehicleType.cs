using iParking.Object;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace iParking.Databases
{
    public class tblVehicleType
    {
        public static string TBL_NAME = "tblVehicleType";
        public static string TBL_COL_ID = "ID";
        public static string TBL_COL_SORT = "Sort";
        public static string TBL_COL_NAME = "Name";
        public static string TBL_COL_CODE = "Code";
        public static string TBL_COL_DESCRIPTION = "Description";

        public static string GetCMD = $"Select {TBL_COL_ID},{TBL_COL_NAME},{TBL_COL_CODE},{TBL_COL_DESCRIPTION} from dbo.{TBL_NAME} order by {TBL_COL_SORT}";

        public string ID { get; set; }
        public int Sort { get; set; }
        public string Name { get; set; }
        public string Code { get; set; }
        public string Description { get; set; }

        //Get
        public static VehicleTypeCollection LoadVehicleType(VehicleTypeCollection vehicleTypeCollection)
        {
            DataTable dtVehicleType = StaticPool.mdb.FillData(GetCMD);
            vehicleTypeCollection.Clear();
            if (dtVehicleType != null)
            {
                if (dtVehicleType.Rows.Count > 0)
                {
                    foreach (DataRow row in dtVehicleType.Rows)
                    {
                        VehicleType vehicleType = new VehicleType();
                        vehicleType.ID = row[TBL_COL_ID].ToString();
                        vehicleType.Code = row[TBL_COL_CODE].ToString();
                        vehicleType.Description = row[TBL_COL_DESCRIPTION].ToString();
                        vehicleType.Name = row[TBL_COL_NAME].ToString();
                        vehicleTypeCollection.Add(vehicleType);
                    }
                    dtVehicleType.Dispose();
                    return vehicleTypeCollection;
                }
            }
            return null;
        }
        //Add
        public static string InsertAndGetLastID(string code, string description, string name)
        {
            string insertCMD = $@"DECLARE @generated_keys table([{TBL_COL_ID}] varchar(150))
                                  Insert into {TBL_NAME}({TBL_COL_CODE},{TBL_COL_DESCRIPTION},{TBL_COL_NAME})
                                  OUTPUT inserted.{TBL_COL_ID} 
                                  Into @generated_keys
                                  values(N'{code}',N'{description}',N'{name}')
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
        public static bool Modify(string id, string code, string description, string name)
        {
            string modifyCMD = $@"update {TBL_NAME} 
                                  set {TBL_COL_CODE} = N'{code}',
                                      {TBL_COL_DESCRIPTION} = N'{description}',
                                      {TBL_COL_NAME} = N'{name}' 
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
        public static bool Delete(string typeID)
        {
            string deleteCMD = $"Delete {TBL_NAME} where {TBL_COL_ID} = '{typeID}'";
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
