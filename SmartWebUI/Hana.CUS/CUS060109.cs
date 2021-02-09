using System;
using System.IO;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;

using Miracom.UI;
using Miracom.SmartWeb;
using Miracom.SmartWeb.UI;
using Miracom.SmartWeb.FWX;
using Miracom.SmartWeb.UI.Controls;

namespace Hana.CUS
{
    public partial class CUS060109 : Miracom.SmartWeb.UI.Controls.udcCUSReport002
    {
        /// <summary>
        /// 클  래  스: CUS060109<br/>
        /// 클래스요약: IML 정산 Report<br/>
        /// 작  성  자: 김민우<br/>
        /// 최초작성일: 2011-01-11<br/>
        /// 상세  설명: 고객사 IML 정산 Report<br/>
        /// 변경  내용: <br/>
        /// </summary>
        /// 
        public CUS060109()
        {
            InitializeComponent();
            //SortInit();
            GridColumnInit();
            this.SetFactory(GlobalVariable.gsAssyDefaultFactory);
            cdvFactory.Text = GlobalVariable.gsAssyDefaultFactory;
            cdvFromToDate.AutoBinding(DateTime.Today.ToString("yyyy-MM") + "-01", DateTime.Today.AddDays(-1).ToString("yyyy-MM-dd"));
        }

        #region 유효성 검사 및 초기화

        /// <summary>
        /// 1. 유효성 검사
        /// </summary>
        /// <returns></returns>
        private Boolean CheckField()
        {
            if (cdvFactory.txtValue.Trim() == "")
            {
                CmnFunction.ShowMsgBox(RptMessages.GetMessage("STD001", GlobalVariable.gcLanguage));
                return false;
            }

            return true;
        }
        /// <summary>
        /// 2. 헤더 생성
        /// </summary>
        private void GridColumnInit()
        {
            spdData.RPT_ColumnInit();
            spdData.RPT_AddBasicColumn("PO NO", 0, 0, Visibles.True, Frozen.True, Align.Left, Merge.True, Formatter.String, 70);
            spdData.RPT_AddBasicColumn("Customer_ID", 0, 1, Visibles.True, Frozen.True, Align.Left, Merge.True, Formatter.String, 70);
            spdData.RPT_AddBasicColumn("RUN ID", 0, 2, Visibles.True, Frozen.True, Align.Left, Merge.True, Formatter.String, 70);
            spdData.RPT_AddBasicColumn("PIN TYPE", 0, 3, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.String, 60);
            spdData.RPT_AddBasicColumn("DA LOSS", 0, 4, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 60);            
            spdData.RPT_AddBasicColumn("ASSY WIP (starting date)", 0, 5, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 60);
            spdData.RPT_AddBasicColumn("ASSY WIP (end date)", 0, 6, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 60);
            spdData.RPT_AddBasicColumn("ASSY IN", 0, 7, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 60);
            spdData.RPT_AddBasicColumn("ASSY OUT", 0, 8, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 60);
            spdData.RPT_AddBasicColumn("ASSY LOSS", 0, 9, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 60);
            spdData.RPT_AddBasicColumn("TEST WIP (start date)", 0, 10, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 60);
            spdData.RPT_AddBasicColumn("TEST WIP (end date)", 0, 11, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 60);
            spdData.RPT_AddBasicColumn("TEST IN", 0, 12, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 60);
            spdData.RPT_AddBasicColumn("TEST OUT", 0, 13, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 60);
            spdData.RPT_AddBasicColumn("TEST LOSS", 0, 14, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 60);
            spdData.RPT_AddBasicColumn("CV", 0, 15, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 60);
            spdData.RPT_AddBasicColumn("TNR LOSS", 0, 16, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 60);
            spdData.RPT_AddBasicColumn("FGS IN", 0, 17, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 60);

            spdData.RPT_ColumnConfigFromTable(btnSort);
        }

        /// <summary>
        /// 3. Group By 정의
        /// </summary>
        private void SortInit()
        {
            //((udcTableForm)(this.btnSort.BindingForm)).AddRow("RUN ID", "SHP.LOT_CMF_4", true);
        }

        #endregion

        #region SQL 쿼리 Build

        /// <summary>
        /// 4. 쿼리 생성
        /// </summary>
        /// <returns></returns>
        private string MakeSqlString()
        {
            StringBuilder strSqlString = new StringBuilder();

            string QueryCond1 = null;
            string QueryCond2 = null;
            string Start_Tran_Time = null;
            string End_Tran_Time = null;
            string Start_Day = null;
            string End_Day = null;

            Start_Tran_Time = cdvFromToDate.ExactFromDate;
            End_Tran_Time = cdvFromToDate.ExactToDate;

            Start_Day = Convert.ToDateTime(Start_Tran_Time.Substring(0, 4) + "-" + Start_Tran_Time.Substring(4, 2) + "-" + Start_Tran_Time.Substring(6, 2)).AddDays(1).ToString("yyyyMMdd") +"22";
            End_Day = End_Tran_Time.Substring(0, 8) + "22";

            udcTableForm tableForm = (udcTableForm)btnSort.BindingForm;
            QueryCond1 = tableForm.SelectedValueToQueryContainNull;
            QueryCond2 = tableForm.SelectedValue2ToQueryContainNull;

            strSqlString.Append("SELECT NVL(PO.PO_NO,'N/A') AS PO_NO " + "\n");
            strSqlString.Append("     , MAT_CMF_7 " + "\n");
            strSqlString.Append("     , A.RUN_ID " + "\n");
            strSqlString.Append("     , MAT_CMF_10 " + "\n");
            strSqlString.Append("     , DA_LOSS " + "\n");
            strSqlString.Append("     , BEG_ASSY_START " + "\n");
            strSqlString.Append("     , BEG_ASSY_END " + "\n");
            strSqlString.Append("     , ASSY_IN " + "\n");
            strSqlString.Append("     , ASSY_OUT " + "\n");
            strSqlString.Append("     , ASSY_LOSS " + "\n");
            strSqlString.Append("     , BEG_TEST_START " + "\n");
            strSqlString.Append("     , BEG_TEST_END " + "\n");
            strSqlString.Append("     , TEST_OUT+TEST_LOSS AS TEST_IN " + "\n");
            strSqlString.Append("     , TEST_OUT " + "\n");
            strSqlString.Append("     , TEST_LOSS " + "\n");
            strSqlString.Append("     , CV " + "\n");
            strSqlString.Append("     , TNR_LOSS " + "\n");
            strSqlString.Append("     , FGS_IN " + "\n");

            strSqlString.Append("  FROM " + "\n");
            strSqlString.Append("      ( " + "\n");
            strSqlString.Append("       SELECT RUN_ID " + "\n");
            strSqlString.Append("            , MAT_CMF_10 " + "\n");
            strSqlString.Append("            , MAT_CMF_7 " + "\n");
            strSqlString.Append("            , SUM(DA_LOSS) AS DA_LOSS " + "\n");
            strSqlString.Append("            , SUM(BEG_ASSY_START) AS BEG_ASSY_START " + "\n");
            strSqlString.Append("            , SUM(BEG_ASSY_END) AS BEG_ASSY_END " + "\n");
            strSqlString.Append("            , SUM(ASSY_IN) AS ASSY_IN " + "\n");
            strSqlString.Append("            , SUM(ASSY_OUT) AS ASSY_OUT " + "\n");
            strSqlString.Append("            , SUM(ASSY_LOSS) AS ASSY_LOSS " + "\n");
            strSqlString.Append("            , SUM(BEG_TEST_START) AS BEG_TEST_START " + "\n");
            strSqlString.Append("            , SUM(BEG_TEST_END) AS BEG_TEST_END " + "\n");
            strSqlString.Append("            , SUM(TEST_IN) AS TEST_IN " + "\n");
            strSqlString.Append("            , SUM(TEST_OUT) AS TEST_OUT " + "\n");
            strSqlString.Append("            , SUM(TEST_LOSS) AS TEST_LOSS " + "\n");
            strSqlString.Append("            , SUM(CV) AS CV " + "\n");
            strSqlString.Append("            , SUM(TNR_LOSS) AS TNR_LOSS " + "\n");
            strSqlString.Append("            , SUM(FGS_IN) AS FGS_IN " + "\n");

            strSqlString.Append("         FROM ( " + "\n");
            // D/A LOSS
            strSqlString.Append("               SELECT LOT.LOT_CMF_4 AS RUN_ID " + "\n");
            strSqlString.Append("                    , MAT_CMF_7 " + "\n");
            strSqlString.Append("                    , MAT_CMF_10 " + "\n");
            strSqlString.Append("                    , SUM(LOSS_QTY) AS DA_LOSS " + "\n");
            strSqlString.Append("                    , 0 AS BEG_ASSY_START " + "\n");
            strSqlString.Append("                    , 0 AS BEG_ASSY_END " + "\n");
            strSqlString.Append("                    , 0 AS ASSY_IN " + "\n");
            strSqlString.Append("                    , 0 AS ASSY_OUT " + "\n");
            strSqlString.Append("                    , 0 AS ASSY_LOSS " + "\n");
            strSqlString.Append("                    , 0 AS BEG_TEST_START " + "\n");
            strSqlString.Append("                    , 0 AS BEG_TEST_END " + "\n");
            strSqlString.Append("                    , 0 AS TEST_IN " + "\n");
            strSqlString.Append("                    , 0 AS TEST_OUT " + "\n");
            strSqlString.Append("                    , 0 AS TEST_LOSS " + "\n");
            strSqlString.Append("                    , 0 AS CV " + "\n");
            strSqlString.Append("                    , 0 AS TNR_LOSS " + "\n");
            strSqlString.Append("                    , 0 AS FGS_IN " + "\n");
            strSqlString.Append("                 FROM RWIPLOTLSM LSM, RWIPLOTSTS LOT, MWIPMATDEF MAT " + "\n");
            strSqlString.Append("                WHERE LSM.FACTORY ='" + GlobalVariable.gsAssyDefaultFactory + "' " + "\n");
            strSqlString.Append("                   AND LSM.MAT_ID LIKE 'IM%' " + "\n");
            strSqlString.Append("                   AND LSM.FACTORY = MAT.FACTORY " + "\n");
            strSqlString.Append("                   AND LSM.MAT_ID = MAT.MAT_ID " + "\n");
            strSqlString.Append("                   AND LSM.MAT_VER = 1 " + "\n");
            strSqlString.Append("                   AND LSM.OPER IN ('A0400','A0401','A0402','A0403') " + "\n");
            strSqlString.Append("                   AND LSM.HIST_DEL_FLAG = ' ' " + "\n");
            strSqlString.Append("                   AND LSM.TRAN_TIME BETWEEN '" + Start_Tran_Time + "' AND '" + End_Tran_Time + "'" + "\n");
            strSqlString.Append("                   AND LSM.LOT_ID = LOT.LOT_ID " + "\n");
            strSqlString.Append("                 GROUP BY MAT_CMF_7, LOT.LOT_CMF_4, MAT_CMF_10 " + "\n");
            strSqlString.Append("               UNION ALL " + "\n");
            // C/V LOSS
            strSqlString.Append("               SELECT LOT.LOT_CMF_4 AS RUN_ID " + "\n");
            strSqlString.Append("                    , MAT_CMF_7 " + "\n");
            strSqlString.Append("                    , MAT_CMF_10 " + "\n");
            strSqlString.Append("                    , 0 AS DA_LOSS " + "\n");
            strSqlString.Append("                    , 0 AS BEG_ASSY_START " + "\n");
            strSqlString.Append("                    , 0 AS BEG_ASSY_END " + "\n");
            strSqlString.Append("                    , 0 AS ASSY_IN " + "\n");
            strSqlString.Append("                    , 0 AS ASSY_OUT " + "\n");
            strSqlString.Append("                    , 0 AS ASSY_LOSS " + "\n");
            strSqlString.Append("                    , 0 AS BEG_TEST_START " + "\n");
            strSqlString.Append("                    , 0 AS BEG_TEST_END " + "\n");
            strSqlString.Append("                    , 0 AS TEST_IN " + "\n");
            strSqlString.Append("                    , 0 AS TEST_OUT " + "\n");
            strSqlString.Append("                    , 0 AS TEST_LOSS " + "\n");
            strSqlString.Append("                    , SUM(LOSS_QTY) AS CV " + "\n");
            strSqlString.Append("                    , 0 AS TNR_LOSS " + "\n");
            strSqlString.Append("                    , 0 AS FGS_IN " + "\n");
            strSqlString.Append("                 FROM RWIPLOTLSM LSM, RWIPLOTSTS LOT, MWIPMATDEF MAT " + "\n");
            strSqlString.Append("                WHERE LSM.FACTORY ='" + GlobalVariable.gsTestDefaultFactory + "' " + "\n");
            strSqlString.Append("                   AND LSM.FACTORY = MAT.FACTORY " + "\n");
            strSqlString.Append("                   AND LSM.MAT_ID = MAT.MAT_ID " + "\n");
            strSqlString.Append("                   AND LSM.OPER NOT IN ('T0100','T0400','T1200') " + "\n");
            strSqlString.Append("                   AND LSM.MAT_ID LIKE 'IM%' " + "\n");
            strSqlString.Append("                   AND LSM.MAT_VER = 1 " + "\n");
            strSqlString.Append("                   AND LSM.HIST_DEL_FLAG = ' ' " + "\n");
            strSqlString.Append("                   AND LSM.TRAN_TIME BETWEEN '" + Start_Tran_Time + "' AND '" + End_Tran_Time + "'" + "\n");
            strSqlString.Append("                   AND LSM.LOT_ID = LOT.LOT_ID " + "\n");
            strSqlString.Append("                 GROUP BY MAT_CMF_7, LOT.LOT_CMF_4, MAT_CMF_10 " + "\n");
            strSqlString.Append("               UNION ALL " + "\n");
            // ASSY IN
            strSqlString.Append("               SELECT LOT_CMF_4 AS RUN_ID " + "\n");
            strSqlString.Append("                    , MAT_CMF_7 " + "\n");
            strSqlString.Append("                    , MAT_CMF_10 " + "\n");
            strSqlString.Append("                    , 0 AS DA_LOSS " + "\n");
            strSqlString.Append("                    , 0 AS BEG_ASSY_START " + "\n");
            strSqlString.Append("                    , 0 AS BEG_ASSY_END " + "\n");
            strSqlString.Append("                    , SUM (QTY_1) AS ASSY_IN " + "\n");
            strSqlString.Append("                    , 0 AS ASSY_OUT " + "\n");
            strSqlString.Append("                    , 0 AS ASSY_LOSS " + "\n");
            strSqlString.Append("                    , 0 AS BEG_TEST_START " + "\n");
            strSqlString.Append("                    , 0 AS BEG_TEST_END " + "\n");
            strSqlString.Append("                    , 0 AS TEST_IN " + "\n");
            strSqlString.Append("                    , 0 AS TEST_OUT " + "\n");
            strSqlString.Append("                    , 0 AS TEST_LOSS " + "\n");
            strSqlString.Append("                    , 0 AS CV " + "\n");
            strSqlString.Append("                    , 0 AS TNR_LOSS " + "\n");
            strSqlString.Append("                    , 0 AS FGS_IN " + "\n");
            strSqlString.Append("                 FROM RWIPLOTHIS HIS, MWIPMATDEF MAT " + "\n");
            strSqlString.Append("                WHERE HIS.FACTORY  = '" + GlobalVariable.gsAssyDefaultFactory + "' " + "\n");
            strSqlString.Append("                   AND HIS.MAT_ID LIKE 'IM%' " + "\n");
            strSqlString.Append("                   AND HIS.MAT_ID = MAT.MAT_ID " + "\n");
            strSqlString.Append("                   AND HIS.FACTORY  = MAT.FACTORY " + "\n");
            strSqlString.Append("                   AND HIST_DEL_FLAG = ' ' " + "\n");
            strSqlString.Append("                   AND HIS.MAT_VER = 1 " + "\n");
            strSqlString.Append("                   AND OPER IN ('A0000', 'A000N') " + "\n");
            strSqlString.Append("                   AND TRAN_CODE = 'CREATE' " + "\n");
            strSqlString.Append("                   AND TRAN_TIME BETWEEN '" + Start_Tran_Time + "' AND '" + End_Tran_Time + "'" + "\n");
            strSqlString.Append("                 GROUP BY MAT_CMF_7, LOT_CMF_4, MAT_CMF_10 " + "\n");
            strSqlString.Append("               UNION ALL " + "\n");
            // ASSY OUT
            strSqlString.Append("               SELECT LOT_CMF_4 AS RUN_ID " + "\n");
            strSqlString.Append("                    , MAT_CMF_7 " + "\n");
            strSqlString.Append("                    , MAT_CMF_10 " + "\n");
            strSqlString.Append("                    , 0 AS DA_LOSS " + "\n");
            strSqlString.Append("                    , 0 AS BEG_ASSY_START " + "\n");
            strSqlString.Append("                    , 0 AS BEG_ASSY_END " + "\n");
            strSqlString.Append("                    , 0 AS ASSY_IN " + "\n");
            strSqlString.Append("                    , SUM(SHIP_QTY_1) AS ASSY_OUT " + "\n");
            strSqlString.Append("                    , 0 AS ASSY_LOSS " + "\n");
            strSqlString.Append("                    , 0 AS BEG_TEST_START " + "\n");
            strSqlString.Append("                    , 0 AS BEG_TEST_END " + "\n");
            strSqlString.Append("                    , 0 AS TEST_IN " + "\n");
            strSqlString.Append("                    , 0 AS TEST_OUT " + "\n");
            strSqlString.Append("                    , 0 AS TEST_LOSS " + "\n");
            strSqlString.Append("                    , 0 AS CV " + "\n");
            strSqlString.Append("                    , 0 AS TNR_LOSS " + "\n");
            strSqlString.Append("                    , 0 AS FGS_IN " + "\n");
            strSqlString.Append("                 FROM VWIPSHPLOT SHP, MWIPMATDEF MAT " + "\n");
            strSqlString.Append("                WHERE FROM_FACTORY = '" + GlobalVariable.gsAssyDefaultFactory + "' " + "\n");
            strSqlString.Append("                   AND SHP.MAT_ID = MAT.MAT_ID " + "\n");
            strSqlString.Append("                   AND SHP.FROM_FACTORY = MAT.FACTORY " + "\n");
            strSqlString.Append("                   AND OWNER_CODE = 'PROD'  " + "\n");
            strSqlString.Append("                   AND LOT_TYPE = 'W' " + "\n");
            strSqlString.Append("                   AND FROM_OPER IN ('AZ010') " + "\n");
            strSqlString.Append("                   AND TRAN_TIME BETWEEN '" + Start_Tran_Time + "' AND '" + End_Tran_Time + "'" + "\n");
            strSqlString.Append("                   AND SHP.MAT_ID LIKE 'IM%' " + "\n");
            strSqlString.Append("                 GROUP BY MAT_CMF_7, LOT_CMF_4, MAT_CMF_10 " + "\n");
            strSqlString.Append("               UNION ALL " + "\n");
            // ASSY LOSS
            strSqlString.Append("               SELECT LOT.LOT_CMF_4 AS RUN_ID " + "\n");
            strSqlString.Append("                    , MAT_CMF_7 " + "\n");
            strSqlString.Append("                    , MAT_CMF_10 " + "\n");
            strSqlString.Append("                    , 0 AS DA_LOSS " + "\n");
            strSqlString.Append("                    , 0 AS BEG_ASSY_START " + "\n");
            strSqlString.Append("                    , 0 AS BEG_ASSY_END " + "\n");
            strSqlString.Append("                    , 0 AS ASSY_IN " + "\n");
            strSqlString.Append("                    , 0 AS ASSY_OUT " + "\n");
            strSqlString.Append("                    , SUM(LOSS_QTY) AS ASSY_LOSS " + "\n");
            strSqlString.Append("                    , 0 AS BEG_TEST_START " + "\n");
            strSqlString.Append("                    , 0 AS BEG_TEST_END " + "\n");
            strSqlString.Append("                    , 0 AS TEST_IN " + "\n");
            strSqlString.Append("                    , 0 AS TEST_OUT " + "\n");
            strSqlString.Append("                    , 0 AS TEST_LOSS " + "\n");
            strSqlString.Append("                    , 0 AS CV " + "\n");
            strSqlString.Append("                    , 0 AS TNR_LOSS " + "\n");
            strSqlString.Append("                    , 0 AS FGS_IN " + "\n");
            strSqlString.Append("                 FROM RWIPLOTLSM LSM, RWIPLOTSTS LOT, MWIPMATDEF MAT " + "\n");
            strSqlString.Append("                WHERE LSM.FACTORY ='" + GlobalVariable.gsAssyDefaultFactory + "' " + "\n");
            strSqlString.Append("                   AND LSM.MAT_ID LIKE 'IM%' " + "\n");
            strSqlString.Append("                   AND LSM.FACTORY = MAT.FACTORY " + "\n");
            strSqlString.Append("                   AND LSM.MAT_ID = MAT.MAT_ID  " + "\n");
            strSqlString.Append("                   AND LSM.MAT_VER = 1 " + "\n");
            strSqlString.Append("                   AND LSM.HIST_DEL_FLAG = ' ' " + "\n");
            strSqlString.Append("                   AND LSM.TRAN_TIME BETWEEN '" + Start_Tran_Time + "' AND '" + End_Tran_Time + "'" + "\n");
            strSqlString.Append("                   AND LSM.LOT_ID = LOT.LOT_ID " + "\n");
            strSqlString.Append("                 GROUP BY MAT_CMF_7, LOT.LOT_CMF_4, MAT_CMF_10 " + "\n");
            strSqlString.Append("               UNION ALL " + "\n");
            // TEST IN
            strSqlString.Append("               SELECT LOT_CMF_4 AS RUN_ID " + "\n");
            strSqlString.Append("                    , MAT_CMF_7 " + "\n");
            strSqlString.Append("                    , MAT_CMF_10 " + "\n");
            strSqlString.Append("                    , 0 AS DA_LOSS " + "\n");
            strSqlString.Append("                    , 0 AS BEG_ASSY_START " + "\n");
            strSqlString.Append("                    , 0 AS BEG_ASSY_END " + "\n");
            strSqlString.Append("                    , 0 AS ASSY_IN " + "\n");
            strSqlString.Append("                    , 0 AS ASSY_OUT " + "\n");
            strSqlString.Append("                    , 0 AS ASSY_LOSS " + "\n");
            strSqlString.Append("                    , 0 AS BEG_TEST_START " + "\n");
            strSqlString.Append("                    , 0 AS BEG_TEST_END " + "\n");
            strSqlString.Append("                    , SUM (QTY_1) AS TEST_IN " + "\n");
            strSqlString.Append("                    , 0 AS TEST_OUT " + "\n");
            strSqlString.Append("                    , 0 AS TEST_LOSS " + "\n");
            strSqlString.Append("                    , 0 AS CV " + "\n");
            strSqlString.Append("                    , 0 AS TNR_LOSS " + "\n");
            strSqlString.Append("                    , 0 AS FGS_IN " + "\n");
            strSqlString.Append("                 FROM RWIPLOTHIS HIS, MWIPMATDEF MAT " + "\n");
            strSqlString.Append("                WHERE HIS.FACTORY  = '" + GlobalVariable.gsTestDefaultFactory + "' " + "\n");
            strSqlString.Append("                   AND HIS.MAT_ID LIKE 'IM%' " + "\n");
            strSqlString.Append("                   AND HIS.MAT_ID = MAT.MAT_ID " + "\n");
            strSqlString.Append("                   AND HIS.FACTORY  = MAT.FACTORY  " + "\n");
            strSqlString.Append("                   AND HIST_DEL_FLAG = ' ' " + "\n");
            strSqlString.Append("                   AND HIS.MAT_VER = 1  " + "\n");
            strSqlString.Append("                   AND OPER IN ('T0100', 'T0400') " + "\n");
            strSqlString.Append("                   AND TRAN_CODE = 'START' " + "\n");
            strSqlString.Append("                   AND TRAN_TIME BETWEEN '" + Start_Tran_Time + "' AND '" + End_Tran_Time + "'" + "\n");
            strSqlString.Append("                 GROUP BY MAT_CMF_7, LOT_CMF_4, MAT_CMF_10 " + "\n");
            strSqlString.Append("               UNION ALL " + "\n");
            // TEST OUT
            strSqlString.Append("               SELECT LOT_CMF_4 AS RUN_ID " + "\n");
            strSqlString.Append("                    , MAT_CMF_7 " + "\n");
            strSqlString.Append("                    , MAT_CMF_10 " + "\n");
            strSqlString.Append("                    , 0 AS DA_LOSS " + "\n");
            strSqlString.Append("                    , 0 AS BEG_ASSY_START " + "\n");
            strSqlString.Append("                    , 0 AS BEG_ASSY_END " + "\n");
            strSqlString.Append("                    , 0 AS ASSY_IN " + "\n");
            strSqlString.Append("                    , 0 AS ASSY_OUT " + "\n");
            strSqlString.Append("                    , 0 AS ASSY_LOSS " + "\n");
            strSqlString.Append("                    , 0 AS BEG_TEST_START " + "\n");
            strSqlString.Append("                    , 0 AS BEG_TEST_END " + "\n");
            strSqlString.Append("                    , 0 AS TEST_IN " + "\n");
            strSqlString.Append("                    , SUM (QTY_1) AS TEST_OUT " + "\n");
            strSqlString.Append("                    , 0 AS TEST_LOSS " + "\n");
            strSqlString.Append("                    , 0 AS CV " + "\n");
            strSqlString.Append("                    , 0 AS TNR_LOSS " + "\n");
            strSqlString.Append("                    , 0 AS FGS_IN " + "\n");
            strSqlString.Append("                 FROM RWIPLOTHIS HIS, MWIPMATDEF MAT " + "\n");
            strSqlString.Append("                WHERE HIS.FACTORY  = '" + GlobalVariable.gsTestDefaultFactory + "' " + "\n");
            strSqlString.Append("                   AND HIS.MAT_ID LIKE 'IM%' " + "\n");
            strSqlString.Append("                   AND HIS.MAT_ID = MAT.MAT_ID " + "\n");
            strSqlString.Append("                   AND HIS.FACTORY  = MAT.FACTORY  " + "\n");
            strSqlString.Append("                   AND HIST_DEL_FLAG = ' ' " + "\n");
            strSqlString.Append("                   AND HIS.MAT_VER = 1  " + "\n");
            strSqlString.Append("                   AND OLD_OPER IN ('T0100', 'T0400') " + "\n");
            strSqlString.Append("                   AND TRAN_CODE = 'END' " + "\n");
            strSqlString.Append("                   AND TRAN_TIME BETWEEN '" + Start_Tran_Time + "' AND '" + End_Tran_Time + "'" + "\n");
            strSqlString.Append("                 GROUP BY MAT_CMF_7, LOT_CMF_4, MAT_CMF_10 " + "\n");
            strSqlString.Append("               UNION ALL " + "\n");
            // TEST LOSS
            strSqlString.Append("               SELECT LOT.LOT_CMF_4 AS RUN_ID " + "\n");
            strSqlString.Append("                    , MAT_CMF_7 " + "\n");
            strSqlString.Append("                    , MAT_CMF_10 " + "\n");
            strSqlString.Append("                    , 0 AS DA_LOSS " + "\n");
            strSqlString.Append("                    , 0 AS BEG_ASSY_START " + "\n");
            strSqlString.Append("                    , 0 AS BEG_ASSY_END " + "\n");
            strSqlString.Append("                    , 0 AS ASSY_IN " + "\n");
            strSqlString.Append("                    , 0 AS ASSY_OUT " + "\n");
            strSqlString.Append("                    , 0 AS ASSY_LOSS " + "\n");
            strSqlString.Append("                    , 0 AS BEG_TEST_START " + "\n");
            strSqlString.Append("                    , 0 AS BEG_TEST_END " + "\n");
            strSqlString.Append("                    , 0 AS TEST_IN " + "\n");
            strSqlString.Append("                    , 0 AS TEST_OUT " + "\n");
            strSqlString.Append("                    , SUM(LOSS_QTY) AS TEST_LOSS " + "\n");
            strSqlString.Append("                    , 0 AS CV " + "\n");
            strSqlString.Append("                    , 0 AS TNR_LOSS " + "\n");
            strSqlString.Append("                    , 0 AS FGS_IN " + "\n");
            strSqlString.Append("                 FROM RWIPLOTLSM LSM, RWIPLOTSTS LOT, MWIPMATDEF MAT " + "\n");
            strSqlString.Append("                WHERE LSM.FACTORY ='" + GlobalVariable.gsTestDefaultFactory + "' " + "\n");
            strSqlString.Append("                   AND LSM.MAT_ID LIKE 'IM%' " + "\n");
            strSqlString.Append("                   AND LSM.FACTORY = MAT.FACTORY " + "\n");
            strSqlString.Append("                   AND LSM.MAT_ID = MAT.MAT_ID  " + "\n");
            strSqlString.Append("                   AND LSM.MAT_VER = 1 " + "\n");
            strSqlString.Append("                   AND LSM.OPER IN ('T0100',',T0400') " + "\n");
            strSqlString.Append("                   AND LSM.HIST_DEL_FLAG = ' ' " + "\n");
            strSqlString.Append("                   AND LSM.TRAN_TIME BETWEEN '" + Start_Tran_Time + "' AND '" + End_Tran_Time + "'" + "\n");
            strSqlString.Append("                   AND LSM.LOT_ID = LOT.LOT_ID " + "\n");
            strSqlString.Append("                 GROUP BY MAT_CMF_7, LOT.LOT_CMF_4, MAT_CMF_10 " + "\n");
            strSqlString.Append("               UNION ALL " + "\n");
            // TNR LOSS
            strSqlString.Append("               SELECT LOT.LOT_CMF_4 AS RUN_ID " + "\n");
            strSqlString.Append("                    , MAT_CMF_7 " + "\n");
            strSqlString.Append("                    , MAT_CMF_10 " + "\n");
            strSqlString.Append("                    , 0 AS DA_LOSS " + "\n");
            strSqlString.Append("                    , 0 AS BEG_ASSY_START " + "\n");
            strSqlString.Append("                    , 0 AS BEG_ASSY_END " + "\n");
            strSqlString.Append("                    , 0 AS ASSY_IN " + "\n");
            strSqlString.Append("                    , 0 AS ASSY_OUT " + "\n");
            strSqlString.Append("                    , 0 AS ASSY_LOSS " + "\n");
            strSqlString.Append("                    , 0 AS BEG_TEST_START " + "\n");
            strSqlString.Append("                    , 0 AS BEG_TEST_END " + "\n");
            strSqlString.Append("                    , 0 AS TEST_IN " + "\n");
            strSqlString.Append("                    , 0 AS TEST_OUT " + "\n");
            strSqlString.Append("                    , 0 AS TEST_LOSS " + "\n");
            strSqlString.Append("                    , 0 AS CV " + "\n");
            strSqlString.Append("                    , SUM(LOSS_QTY) AS TNR_LOSS " + "\n");
            strSqlString.Append("                    , 0 AS FGS_IN " + "\n");
            strSqlString.Append("                 FROM RWIPLOTLSM LSM, RWIPLOTSTS LOT, MWIPMATDEF MAT " + "\n");
            strSqlString.Append("                WHERE LSM.FACTORY ='" + GlobalVariable.gsTestDefaultFactory + "' " + "\n");
            strSqlString.Append("                   AND LSM.MAT_ID LIKE 'IM%' " + "\n");
            strSqlString.Append("                   AND LSM.FACTORY = MAT.FACTORY " + "\n");
            strSqlString.Append("                   AND LSM.MAT_ID = MAT.MAT_ID  " + "\n");
            strSqlString.Append("                   AND LSM.MAT_VER = 1 " + "\n");
            strSqlString.Append("                   AND LSM.OPER = 'T1200' " + "\n");
            strSqlString.Append("                   AND LSM.HIST_DEL_FLAG = ' ' " + "\n");
            strSqlString.Append("                   AND LSM.TRAN_TIME BETWEEN '" + Start_Tran_Time + "' AND '" + End_Tran_Time + "'" + "\n");
            strSqlString.Append("                   AND LSM.LOT_ID = LOT.LOT_ID " + "\n");
            strSqlString.Append("                 GROUP BY MAT_CMF_7, LOT.LOT_CMF_4, MAT_CMF_10 " + "\n");
            strSqlString.Append("               UNION ALL " + "\n");
            // FGS IN
            strSqlString.Append("               SELECT LOT_CMF_4 AS RUN_ID " + "\n");
            strSqlString.Append("                    , MAT_CMF_7 " + "\n");
            strSqlString.Append("                    , MAT_CMF_10 " + "\n");
            strSqlString.Append("                    , 0 AS DA_LOSS " + "\n");
            strSqlString.Append("                    , 0 AS BEG_ASSY_START " + "\n");
            strSqlString.Append("                    , 0 AS BEG_ASSY_END " + "\n");
            strSqlString.Append("                    , 0 AS ASSY_IN " + "\n");
            strSqlString.Append("                    , 0 AS ASSY_OUT " + "\n");
            strSqlString.Append("                    , 0 AS ASSY_LOSS " + "\n");
            strSqlString.Append("                    , 0 AS BEG_TEST_START " + "\n");
            strSqlString.Append("                    , 0 AS BEG_TEST_END " + "\n");
            strSqlString.Append("                    , 0 AS TEST_IN " + "\n");
            strSqlString.Append("                    , 0 AS TEST_OUT " + "\n");
            strSqlString.Append("                    , 0 AS TEST_LOSS " + "\n");
            strSqlString.Append("                    , 0 AS CV " + "\n");
            strSqlString.Append("                    , 0 AS TNR_LOSS " + "\n");
            strSqlString.Append("                    , SUM (QTY_1) AS FGS_IN " + "\n");
            strSqlString.Append("                 FROM RWIPLOTHIS HIS, MWIPMATDEF MAT " + "\n");
            strSqlString.Append("                WHERE HIS.FACTORY  = 'FGS' " + "\n");
            strSqlString.Append("                   AND HIS.MAT_ID LIKE 'IM%' " + "\n");
            strSqlString.Append("                   AND HIS.MAT_ID = MAT.MAT_ID " + "\n");
            strSqlString.Append("                   AND HIS.FACTORY  = MAT.FACTORY  " + "\n");
            strSqlString.Append("                   AND HIST_DEL_FLAG = ' ' " + "\n");
            strSqlString.Append("                   AND HIS.MAT_VER = 1 " + "\n");
            strSqlString.Append("                   AND OPER IN ('F0000') " + "\n");
            strSqlString.Append("                   AND TRAN_CODE = 'RECEIVE' " + "\n");
            strSqlString.Append("                   AND TRAN_TIME BETWEEN '" + Start_Tran_Time + "' AND '" + End_Tran_Time + "'" + "\n");
            strSqlString.Append("                 GROUP BY MAT_CMF_7, LOT_CMF_4, MAT_CMF_10 " + "\n");
            strSqlString.Append("               UNION ALL " + "\n");
            // BEG_ASSY_START
            strSqlString.Append("               SELECT LOT_CMF_4 AS RUN_ID " + "\n");
            strSqlString.Append("                    , MAT_CMF_7 " + "\n");
            strSqlString.Append("                    , MAT_CMF_10 " + "\n");
            strSqlString.Append("                    , 0 AS DA_LOSS " + "\n");
            strSqlString.Append("                    , SUM (QTY_1) AS BEG_ASSY_START " + "\n");
            strSqlString.Append("                    , 0 AS BEG_ASSY_END " + "\n");
            strSqlString.Append("                    , 0 AS ASSY_IN " + "\n");
            strSqlString.Append("                    , 0 AS ASSY_OUT " + "\n");
            strSqlString.Append("                    , 0 AS ASSY_LOSS " + "\n");
            strSqlString.Append("                    , 0 AS BEG_TEST_START " + "\n");
            strSqlString.Append("                    , 0 AS BEG_TEST_END " + "\n");
            strSqlString.Append("                    , 0 AS TEST_IN " + "\n");
            strSqlString.Append("                    , 0 AS TEST_OUT " + "\n");
            strSqlString.Append("                    , 0 AS TEST_LOSS " + "\n");
            strSqlString.Append("                    , 0 AS CV " + "\n");
            strSqlString.Append("                    , 0 AS TNR_LOSS " + "\n");
            strSqlString.Append("                    , 0 AS FGS_IN " + "\n");
            strSqlString.Append("                 FROM RWIPLOTSTS_BOH A, MWIPMATDEF B " + "\n");
            strSqlString.Append("                WHERE A.FACTORY = '" + GlobalVariable.gsAssyDefaultFactory + "' " + "\n");
            strSqlString.Append("                   AND B.MAT_VER = 1 " + "\n");
            strSqlString.Append("                   AND A.OWNER_CODE = 'PROD' " + "\n");
            strSqlString.Append("                   AND A.CUTOFF_DT = '" + Start_Day + "'" + "\n");
            strSqlString.Append("                   AND A.OPER BETWEEN 'A0000' AND 'AZ010' " + "\n");
            strSqlString.Append("                   AND A.FACTORY = B.FACTORY " + "\n");
            strSqlString.Append("                   AND A.MAT_ID = B.MAT_ID " + "\n");
            strSqlString.Append("                   AND A.MAT_VER = 1 " + "\n");
            strSqlString.Append("                   AND A.MAT_ID LIKE 'IM%' " + "\n");
            strSqlString.Append("                 GROUP BY MAT_CMF_7, LOT_CMF_4, MAT_CMF_10 " + "\n");
            strSqlString.Append("               UNION ALL " + "\n");
            // BEG_ASSY_END
            strSqlString.Append("               SELECT LOT_CMF_4 AS RUN_ID " + "\n");
            strSqlString.Append("                    , MAT_CMF_7 " + "\n");
            strSqlString.Append("                    , MAT_CMF_10 " + "\n");
            strSqlString.Append("                    , 0 AS DA_LOSS " + "\n");
            strSqlString.Append("                    , 0 AS BEG_ASSY_START " + "\n");
            strSqlString.Append("                    , SUM (QTY_1) AS BEG_ASSY_END " + "\n");
            strSqlString.Append("                    , 0 AS ASSY_IN " + "\n");
            strSqlString.Append("                    , 0 AS ASSY_OUT " + "\n");
            strSqlString.Append("                    , 0 AS ASSY_LOSS " + "\n");
            strSqlString.Append("                    , 0 AS BEG_TEST_START " + "\n");
            strSqlString.Append("                    , 0 AS BEG_TEST_END " + "\n");
            strSqlString.Append("                    , 0 AS TEST_IN " + "\n");
            strSqlString.Append("                    , 0 AS TEST_OUT " + "\n");
            strSqlString.Append("                    , 0 AS TEST_LOSS " + "\n");
            strSqlString.Append("                    , 0 AS CV " + "\n");
            strSqlString.Append("                    , 0 AS TNR_LOSS " + "\n");
            strSqlString.Append("                    , 0 AS FGS_IN " + "\n");
            strSqlString.Append("                 FROM RWIPLOTSTS_BOH A, MWIPMATDEF B " + "\n");
            strSqlString.Append("                WHERE A.FACTORY = '" + GlobalVariable.gsAssyDefaultFactory + "' " + "\n");
            strSqlString.Append("                   AND B.MAT_VER = 1 " + "\n");
            strSqlString.Append("                   AND A.OWNER_CODE = 'PROD' " + "\n");
            strSqlString.Append("                   AND A.CUTOFF_DT = '" + End_Day + "'" + "\n");
            strSqlString.Append("                   AND A.OPER BETWEEN 'A0000' AND 'AZ010' " + "\n");
            strSqlString.Append("                   AND A.FACTORY = B.FACTORY " + "\n");
            strSqlString.Append("                   AND A.MAT_ID = B.MAT_ID " + "\n");
            strSqlString.Append("                   AND A.MAT_VER = 1 " + "\n");
            strSqlString.Append("                   AND A.MAT_ID LIKE 'IM%' " + "\n");
            strSqlString.Append("                 GROUP BY MAT_CMF_7, LOT_CMF_4, MAT_CMF_10 " + "\n");
            strSqlString.Append("               UNION ALL " + "\n");
            // BEG_TEST_START
            strSqlString.Append("               SELECT LOT_CMF_4 AS RUN_ID " + "\n");
            strSqlString.Append("                    , MAT_CMF_7 " + "\n");
            strSqlString.Append("                    , MAT_CMF_10 " + "\n");
            strSqlString.Append("                    , 0 AS DA_LOSS " + "\n");
            strSqlString.Append("                    , 0 AS BEG_ASSY_START " + "\n");
            strSqlString.Append("                    , 0 AS BEG_ASSY_END " + "\n");
            strSqlString.Append("                    , 0 AS ASSY_IN " + "\n");
            strSqlString.Append("                    , 0 AS ASSY_OUT " + "\n");
            strSqlString.Append("                    , 0 AS ASSY_LOSS " + "\n");
            strSqlString.Append("                    , SUM (QTY_1) AS BEG_TEST_START " + "\n");
            strSqlString.Append("                    , 0 AS BEG_TEST_END " + "\n");
            strSqlString.Append("                    , 0 AS TEST_IN " + "\n");
            strSqlString.Append("                    , 0 AS TEST_OUT " + "\n");
            strSqlString.Append("                    , 0 AS TEST_LOSS " + "\n");
            strSqlString.Append("                    , 0 AS CV " + "\n");
            strSqlString.Append("                    , 0 AS TNR_LOSS " + "\n");
            strSqlString.Append("                    , 0 AS FGS_IN " + "\n");
            strSqlString.Append("                 FROM RWIPLOTSTS_BOH A, MWIPMATDEF B " + "\n");
            strSqlString.Append("                WHERE A.FACTORY = '" + GlobalVariable.gsTestDefaultFactory + "' " + "\n");
            strSqlString.Append("                   AND B.MAT_VER = 1 " + "\n");
            strSqlString.Append("                   AND A.OWNER_CODE = 'PROD' " + "\n");
            strSqlString.Append("                   AND A.CUTOFF_DT = '" + Start_Day + "'" + "\n");
            strSqlString.Append("                   AND A.OPER BETWEEN 'T0000' AND 'TZ010' " + "\n");
            strSqlString.Append("                   AND A.FACTORY = B.FACTORY " + "\n");
            strSqlString.Append("                   AND A.MAT_ID = B.MAT_ID " + "\n");
            strSqlString.Append("                   AND A.MAT_VER = 1 " + "\n");
            strSqlString.Append("                   AND A.MAT_ID LIKE 'IM%' " + "\n");
            strSqlString.Append("                 GROUP BY MAT_CMF_7, LOT_CMF_4, MAT_CMF_10 " + "\n");
            strSqlString.Append("               UNION ALL " + "\n");
            // BEG_TEST_END
            strSqlString.Append("               SELECT LOT_CMF_4 AS RUN_ID " + "\n");
            strSqlString.Append("                    , MAT_CMF_7 " + "\n");
            strSqlString.Append("                    , MAT_CMF_10 " + "\n");
            strSqlString.Append("                    , 0 AS DA_LOSS " + "\n");
            strSqlString.Append("                    , 0 AS BEG_ASSY_START " + "\n");
            strSqlString.Append("                    , 0 AS BEG_ASSY_END " + "\n");
            strSqlString.Append("                    , 0 AS ASSY_IN " + "\n");
            strSqlString.Append("                    , 0 AS ASSY_OUT " + "\n");
            strSqlString.Append("                    , 0 AS ASSY_LOSS " + "\n");
            strSqlString.Append("                    , 0 AS BEG_TEST_START " + "\n");
            strSqlString.Append("                    , SUM (QTY_1) AS BEG_TEST_END " + "\n");
            strSqlString.Append("                    , 0 AS TEST_IN " + "\n");
            strSqlString.Append("                    , 0 AS TEST_OUT " + "\n");
            strSqlString.Append("                    , 0 AS TEST_LOSS " + "\n");
            strSqlString.Append("                    , 0 AS CV " + "\n");
            strSqlString.Append("                    , 0 AS TNR_LOSS " + "\n");
            strSqlString.Append("                    , 0 AS FGS_IN " + "\n");
            strSqlString.Append("                 FROM RWIPLOTSTS_BOH A, MWIPMATDEF B " + "\n");
            strSqlString.Append("                WHERE A.FACTORY = '" + GlobalVariable.gsTestDefaultFactory + "' " + "\n");
            strSqlString.Append("                   AND B.MAT_VER = 1 " + "\n");
            strSqlString.Append("                   AND A.OWNER_CODE = 'PROD' " + "\n");
            strSqlString.Append("                   AND A.CUTOFF_DT = '" + End_Day + "'" + "\n");
            strSqlString.Append("                   AND A.OPER BETWEEN 'T0000' AND 'TZ010' " + "\n");
            strSqlString.Append("                   AND A.FACTORY = B.FACTORY " + "\n");
            strSqlString.Append("                   AND A.MAT_ID = B.MAT_ID " + "\n");
            strSqlString.Append("                   AND A.MAT_VER = 1 " + "\n");
            strSqlString.Append("                   AND A.MAT_ID LIKE 'IM%' " + "\n");
            strSqlString.Append("                 GROUP BY MAT_CMF_7, LOT_CMF_4, MAT_CMF_10 " + "\n");

            strSqlString.Append("              ) " + "\n");
            strSqlString.Append("       GROUP BY MAT_CMF_7, RUN_ID, MAT_CMF_10 " + "\n");
            strSqlString.Append("      ) A, CWIPCUSORD@RPTTOMES PO " + "\n");
            strSqlString.Append(" WHERE A.RUN_ID=PO.RUN_ID(+) " + "\n");
            strSqlString.Append(" ORDER BY PO_NO, RUN_ID " + "\n");

            if (GlobalVariable.gsUserGroup == "ADMIN_GROUP" || GlobalVariable.gsUserGroup == "HANA_ADMIN_GROUP")
            {
                System.Windows.Forms.Clipboard.SetText(strSqlString.ToString());
            }

            return strSqlString.ToString();
        }

        #endregion

        #region EVEND 처리

        private void btnView_Click(object sender, EventArgs e)
        {
            DataTable dt = null;
            if (CheckField() == false) return;

            spdData_Sheet1.RowCount = 0;

            try
            {
                this.Refresh();
                LoadingPopUp.LoadIngPopUpShow(this);
                dt = CmnFunction.oComm.GetFuncDataTable("DYNAMIC", MakeSqlString());

                GridColumnInit();

                if (dt.Rows.Count == 0)
                {
                    dt.Dispose();
                    LoadingPopUp.LoadingPopUpHidden();
                    CmnFunction.ShowMsgBox(RptMessages.GetMessage("STD010", GlobalVariable.gcLanguage));
                    return;
                }

                int[] rowType = spdData.RPT_DataBindingWithSubTotal(dt, 0, 0, 1, null, null, btnSort);

                spdData.RPT_FillDataSelectiveCells("TOTAL", 0, 4, 0, 1, true, Align.Center, VerticalAlign.Center); //Total부분 셀 merge 

                //spdData.DataSource = dt;

                spdData.RPT_AutoFit(false);
            }
            catch (Exception ex)
            {
                LoadingPopUp.LoadingPopUpHidden();
                CmnFunction.ShowMsgBox(ex.Message);
            }
            finally
            {
                LoadingPopUp.LoadingPopUpHidden();
            }
        }

        private void btnExcelExport_Click(object sender, EventArgs e)
        {
            spdData.ExportExcel();
        }

        #endregion

    }
}