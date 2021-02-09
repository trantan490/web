using System;
using System.Data;

using Miracom.Query;
using Miracom.SmartWeb.FWX;
using Miracom.SmartWeb.RO;
using System.Collections;

namespace Miracom.SmartWeb.RO
{
    public partial class StandardFunction
    {
        public DataTable STD1905(ArrayList TempList)
        {
            DataTable RetDT = null;
            DataTable ReRetDT = new DataTable();
            string DynamicQuery = null;
            string DynamicSubQuery_1 = null;
            string DynamicSubQuery_2 = null;
            string[] CondList = null;

            try
            {
                CondList = (String[])TempList.ToArray(typeof(string));
                StdGlobalVariable.DBQuery.InitQueryComponent(StdGlobalVariable.QueryPath + @"\" + StdGlobalVariable.MP_TRACEABILITY_XML_FILE);
                if (CondList[0] != null && CondList[0].TrimEnd().Length > 0)
                {
                    DynamicSubQuery_1 = "  AND HIS.FACTORY = '" + CondList[0] + "'";

                    if (CondList[1] == "1")
                    {
                        DynamicSubQuery_1 += " AND HIS.SUBLOT_ID = '" + CondList[2] + "'";
                    }
                    else if (CondList[1] == "2")
                    {
                        DynamicSubQuery_2 = " WHERE START_HIS.START_LOT_NUM = '" + CondList[2] + "'";
                    }
                    else if (CondList[1] == "3")
                    {
                        DynamicSubQuery_2 = " WHERE END_HIS.END_LOT_NUM = '" + CondList[2] + "'";
                    }
                    else if (CondList[1] == "4")
                    {
                        DynamicSubQuery_1 += " AND SUBLOT_CMF_1 = '" + CondList[2] + "'";
                    }

                    DynamicQuery = " SELECT DECODE(NVL(START_EQ_NAME, ' '), ' ', NVL(END_EQ_NAME, ' '), NVL(START_EQ_NAME, ' ')) AS EQ_NAME, ";
                    DynamicQuery += "        DECODE(NVL(START_EQ_FACILITY, ' '), ' ', NVL(END_EQ_FACILITY, ' '), NVL(START_EQ_FACILITY, ' ')) AS EQ_FACILITY, ";
                    DynamicQuery += "        NVL(START_PJ_START_DATE, ' ') AS PJ_START_DATE, ";
                    DynamicQuery += "        NVL(END_PJ_START_DATE, ' ') AS PJ_END_DATE, ";
                    DynamicQuery += "        NVL(START_LOT_NUM, ' ') AS LOT_NUM, ";
                    DynamicQuery += "        DECODE(NVL(START_OPERATION, ' '), ' ', NVL(END_OPERATION, ' '), NVL(START_OPERATION, ' ')) AS OPERATION, ";
                    DynamicQuery += "        DECODE(NVL(START_WAF_ID, ' '), ' ', NVL(END_WAF_ID, ' '), NVL(START_WAF_ID, ' ')) AS WAF_ID, ";
                    //DynamicQuery += "        DECODE(NVL(START_WAF_FLAG, ' '), ' ', NVL(END_WAF_FLAG, ' '), NVL(START_WAF_FLAG, ' ')) AS WAF_FLAG, ";
                    DynamicQuery += "        NVL(END_LOT_NUM, ' ') AS LOT_NUM_OUT, ";
                    DynamicQuery += "        NVL(START_CASS_ID_IN, ' ') AS CASS_ID_IN, ";
                    DynamicQuery += "        NVL(START_SLOT_NUM_IN, 0) AS SLOT_NUM_IN, ";
                    DynamicQuery += "        NVL(START_WAF_TYPE_IN, ' ') AS WAF_TYPE_IN, ";
                    DynamicQuery += "        NVL(END_CASS_ID_IN, ' ') AS CASS_ID_OUT, ";
                    DynamicQuery += "        NVL(END_SLOT_NUM_IN, 0) AS SLOT_NUM_OUT, ";
                    DynamicQuery += "        NVL(END_WAF_TYPE_IN, ' ') AS WAF_TYPE_OUT, ";
                    DynamicQuery += "        DECODE(NVL(START_LASERMARK, ' '), ' ', NVL(END_LASERMARK, ' '), NVL(START_LASERMARK, ' ')) AS LASERMARK, ";
                    DynamicQuery += "        DECODE(NVL(START_INGOT_SEG, ' '), ' ', NVL(END_INGOT_SEG, ' '), NVL(START_INGOT_SEG, ' ')) AS INGOT_SEG, ";
                    DynamicQuery += "        DECODE(NVL(START_INGOT_NUM, ' '), ' ', NVL(END_INGOT_NUM, ' '), NVL(START_INGOT_NUM, ' ')) AS INGOT_NUM, ";
                    DynamicQuery += "        DECODE(NVL(START_INGOT_POS_MM, ' '), ' ', NVL(END_INGOT_POS_MM, ' '), NVL(START_INGOT_POS_MM, ' ')) AS INGOT_POS_MM, ";
                    DynamicQuery += "        DECODE(NVL(START_CURR_PRODUCT, ' '), ' ', NVL(END_CURR_PRODUCT, ' '), NVL(START_CURR_PRODUCT, ' ')) AS CURR_PRODUCT ";
                    DynamicQuery += "     FROM (SELECT HIS.START_RES_ID AS START_EQ_NAME, HIS.FACTORY AS START_EQ_FACILITY, ";
                    DynamicQuery += "                  HIS.START_TIME AS START_PJ_START_DATE, HIS.LOT_ID AS START_LOT_NUM, ";
                    DynamicQuery += "                  HIS.OPER AS START_OPERATION, HIS.SUBLOT_ID AS START_WAF_ID, ";
                    DynamicQuery += "                  'A' AS START_WAF_FLAG, HIS.CRR_ID AS START_CASS_ID_IN, ";
                    DynamicQuery += "                  HIS.SLOT_NO AS START_SLOT_NUM_IN, GCM.DATA_1 AS START_WAF_TYPE_IN, HIS.SUBLOT_CMF_1 AS START_LASERMARK, ";
                    DynamicQuery += "                  HIS.SUBLOT_CMF_2 AS START_INGOT_SEG, SUBSTR(HIS.SUBLOT_CMF_2, 0, 6) AS START_INGOT_NUM, ";
                    DynamicQuery += "                  ATR.ATTR_VALUE AS START_INGOT_POS_MM, HIS.MAT_ID AS START_CURR_PRODUCT ";
                    DynamicQuery += "             FROM MWIPSLTHIS HIS, MGCMTBLDAT GCM, MATRNAMSTS ATR ";
                    DynamicQuery += "            WHERE HIS.TRAN_CODE = 'START' AND HIS.OPER <> '8820' AND HIS.HIST_DEL_FLAG = ' ' ";
                    DynamicQuery += "              AND HIS.FACTORY = GCM.FACTORY(+) AND GCM.TABLE_NAME(+) = 'SUBLOT_GRADE' AND GCM.KEY_1(+) = HIS.GRADE ";
                    DynamicQuery += "              AND HIS.FACTORY = ATR.FACTORY(+) AND ATR.ATTR_TYPE(+) = 'SUBLOT' AND ATR.ATTR_NAME(+) = 'StartPosition' AND ATR.ATTR_KEY(+) = HIS.SUBLOT_ID ";
                    DynamicQuery += DynamicSubQuery_1;
                    DynamicQuery += "          ) START_HIS ";
                    DynamicQuery += "          FULL OUTER JOIN ";
                    DynamicQuery += "          (SELECT HIS.END_RES_ID AS END_EQ_NAME, HIS.FACTORY AS END_EQ_FACILITY, ";
                    DynamicQuery += "                  HIS.END_TIME AS END_PJ_START_DATE, HIS.LOT_ID AS END_LOT_NUM, ";
                    DynamicQuery += "                  DECODE (HIS.OLD_OPER, ";
                    DynamicQuery += "                          '8820', '8810', ";
                    DynamicQuery += "                          HIS.OLD_OPER ";
                    DynamicQuery += "                         ) AS END_OPERATION, HIS.SUBLOT_ID AS END_WAF_ID, ";
                    DynamicQuery += "                  'A' AS END_WAF_FLAG, HIS.CRR_ID AS END_CASS_ID_IN, ";
                    DynamicQuery += "                  HIS.SLOT_NO AS END_SLOT_NUM_IN, GCM.DATA_1 AS END_WAF_TYPE_IN, HIS.SUBLOT_CMF_1 AS END_LASERMARK, ";
                    DynamicQuery += "                  HIS.SUBLOT_CMF_2 AS END_INGOT_SEG, SUBSTR(HIS.SUBLOT_CMF_2, 0, 6) AS END_INGOT_NUM, ";
                    DynamicQuery += "                  ATR.ATTR_VALUE AS END_INGOT_POS_MM, HIS.MAT_ID AS END_CURR_PRODUCT ";
                    DynamicQuery += "             FROM MWIPSLTHIS HIS, MGCMTBLDAT GCM, MATRNAMSTS ATR ";
                    DynamicQuery += "            WHERE HIS.TRAN_CODE = 'END' AND HIS.OLD_OPER <> '8810' AND HIS.HIST_DEL_FLAG = ' ' ";
                    DynamicQuery += "              AND HIS.FACTORY = GCM.FACTORY(+) AND GCM.TABLE_NAME(+) = 'SUBLOT_GRADE' AND GCM.KEY_1(+) = HIS.GRADE ";
                    DynamicQuery += "              AND HIS.FACTORY = ATR.FACTORY(+) AND ATR.ATTR_TYPE(+) = 'SUBLOT' AND ATR.ATTR_NAME(+) = 'StartPosition' AND ATR.ATTR_KEY(+) = HIS.SUBLOT_ID ";
                    DynamicQuery += DynamicSubQuery_1;
                    DynamicQuery += "          ) END_HIS ";
                    DynamicQuery += "          ON START_HIS.START_OPERATION = END_HIS.END_OPERATION ";
                    DynamicQuery += "        AND START_HIS.START_WAF_ID = END_HIS.END_WAF_ID ";
                    DynamicQuery += DynamicSubQuery_2;
                    DynamicQuery += " ORDER BY PJ_START_DATE, LOT_NUM, SLOT_NUM_IN ";

                    RetDT = StdGlobalVariable.DBQuery.GetFuncDataTable("STD1905", new string[] { DynamicQuery }, CondList);
                }

                return RetDT;

            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                if (RetDT != null) RetDT.Dispose();
            }

        }
    }
}
