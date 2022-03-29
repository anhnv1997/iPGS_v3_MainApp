using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace iParking.Databases
{
    public class tblCCU
    {
        public static string TBL_NAME = "tblCCU";
        public static string TBL_COL_NAME = "Name";
        public static string TBL_COL_SORT = "Sort";
        public static string TBL_COL_ID = "ID";

        //Get
        public static CCUCollection LoadDataCCU(CCUCollection ccuCollection)
        {
            string GetCMD = $@"Select {TBL_COL_ID},{TBL_COL_NAME} 
                               from {TBL_NAME} order by {TBL_COL_SORT}";
            DataTable dtCCU = StaticPool.mdb.FillData(GetCMD);
            ccuCollection.Clear();
            if (dtCCU != null)
            {
                if (dtCCU.Rows.Count > 0)
                {
                    foreach (DataRow row in dtCCU.Rows)
                    {
                        CCU ccu = new CCU();
                        ccu.Id = row[TBL_COL_ID].ToString();
                        ccu.Name = row[TBL_COL_NAME].ToString();
                        ccuCollection.Add(ccu);
                    }
                    dtCCU.Dispose();
                    return ccuCollection;
                }
            }
            return null;
        }
    }
}
