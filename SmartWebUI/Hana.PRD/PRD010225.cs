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

namespace Hana.PRD
{
    public partial class PRD010225 : Miracom.SmartWeb.UI.Controls.udcCUSReport002
    {
        /// <summary>
        /// 클  래  스: PRD010225<br/>
        /// 클래스요약: ER_CS Monitor (new)<br/>
        /// 작  성  자: 하나마이크론 임종우<br/>
        /// 최초작성일: 2017-08-24<br/>
        /// 상세  설명: ER_CS Monitor (new)<br/>
        /// 변경  내용: <br/>
        /// 2017-09-11-임종우 : DUE DATE 초기값인(19990101) LOT 제외 (임태성C 요청)
        /// 2017-09-25-임종우 : 과거 데이터 검색 기능추가 (김우정 요청)
        /// </summary>
        public PRD010225()
        {
            InitializeComponent();
            SortInit();
            cdvDate.Value = DateTime.Now;
            GridColumnInit();
            this.cdvFactory.sFactory = GlobalVariable.gsAssyDefaultFactory;
            this.cdvLotType.sFactory = GlobalVariable.gsAssyDefaultFactory;
            this.SetFactory(GlobalVariable.gsAssyDefaultFactory);
            cdvFactory.Text = GlobalVariable.gsAssyDefaultFactory;
            cdvLotType.sFactory = GlobalVariable.gsAssyDefaultFactory;
        }

        #region 초기화 및 유효성 검사
        /// <summary 1. 유효성 검사>
        /// <remarks></remarks>
        /// </summary>
        /// <returns></returns>
        private Boolean CheckField()
        {
            return true;
        }

        /// <summary>
        /// 2. 헤더 생성
        /// </summary>
        private void GridColumnInit()
        {
            spdData.RPT_ColumnInit();

            try
            {
                spdData.RPT_AddBasicColumn("Order number", 0, 0, Visibles.True, Frozen.True, Align.Left, Merge.True, Formatter.String, 100);
                spdData.RPT_AddBasicColumn("Vendor description", 0, 1, Visibles.True, Frozen.True, Align.Left, Merge.True, Formatter.String, 70);
                spdData.RPT_AddBasicColumn("Package", 0, 2, Visibles.True, Frozen.True, Align.Left, Merge.True, Formatter.String, 70);
                spdData.RPT_AddBasicColumn("Part no", 0, 3, Visibles.True, Frozen.True, Align.Left, Merge.True, Formatter.String, 200);
                spdData.RPT_AddBasicColumn("Pin type", 0, 4, Visibles.True, Frozen.True, Align.Left, Merge.True, Formatter.String, 200);

                spdData.RPT_AddBasicColumn("LOT ID", 0, 5, Visibles.True, Frozen.False, Align.Left, Merge.False, Formatter.String, 100);                                
                spdData.RPT_AddBasicColumn("QTY", 0, 6, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 70);
                spdData.RPT_AddBasicColumn("LOT TYPE", 0, 7, Visibles.True, Frozen.False, Align.Center, Merge.False, Formatter.String, 50);

                spdData.RPT_AddBasicColumn("Schedule", 0, 8, Visibles.True, Frozen.False, Align.Center, Merge.False, Formatter.String, 70);
                spdData.RPT_AddBasicColumn("Start", 1, 8, Visibles.True, Frozen.False, Align.Center, Merge.False, Formatter.String, 70);
                spdData.RPT_AddBasicColumn("End", 1, 9, Visibles.True, Frozen.False, Align.Center, Merge.False, Formatter.String, 70);
                spdData.RPT_MerageHeaderColumnSpan(0, 8, 2);

                spdData.RPT_AddBasicColumn("Step", 0, 10, Visibles.True, Frozen.False, Align.Center, Merge.False, Formatter.String, 70);
                spdData.RPT_AddBasicColumn("Code", 1, 10, Visibles.True, Frozen.False, Align.Center, Merge.False, Formatter.String, 70);
                spdData.RPT_AddBasicColumn("Operation", 1, 11, Visibles.True, Frozen.False, Align.Left, Merge.False, Formatter.String, 100);
                spdData.RPT_MerageHeaderColumnSpan(0, 10, 2);

                spdData.RPT_AddBasicColumn("Target", 0, 12, Visibles.True, Frozen.False, Align.Center, Merge.False, Formatter.String, 70);

                for (int i = 0; i <= 5; i++)
                {
                    spdData.RPT_AddBasicColumn(cdvDate.Value.AddDays(i).ToString("MM.dd"), 1, 12 + i, Visibles.True, Frozen.False, Align.Center, Merge.False, Formatter.String, 70);
                }

                spdData.RPT_MerageHeaderColumnSpan(0, 12, 6);          

                spdData.RPT_MerageHeaderRowSpan(0, 0, 2);
                spdData.RPT_MerageHeaderRowSpan(0, 1, 2);
                spdData.RPT_MerageHeaderRowSpan(0, 2, 2);
                spdData.RPT_MerageHeaderRowSpan(0, 3, 2);
                spdData.RPT_MerageHeaderRowSpan(0, 4, 2);
                spdData.RPT_MerageHeaderRowSpan(0, 5, 2);
                spdData.RPT_MerageHeaderRowSpan(0, 6, 2);
                spdData.RPT_MerageHeaderRowSpan(0, 7, 2);
                
                //spdData.RPT_ColumnConfigFromTable(btnSort);
            }
            catch (Exception ex)
            {
                CmnFunction.ShowMsgBox(ex.Message);
            }
        }

        /// <summary>
        /// 3. Group By 정의
        /// </summary>
        private void SortInit()
        {
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

            string QueryCond1;
            string QueryCond2;
            string QueryCond3;            

            udcTableForm tableForm = (udcTableForm)btnSort.BindingForm;
            QueryCond1 = tableForm.SelectedValueToQueryContainNull;
            QueryCond2 = tableForm.SelectedValue2ToQueryContainNull;
            QueryCond3 = tableForm.SelectedValue3ToQueryContainNull;

            strSqlString.Append("SELECT LOT_CMF_3 AS ORDER_NO" + "\n");
            strSqlString.Append("     , (SELECT DATA_1 FROM MGCMTBLDAT WHERE TABLE_NAME = 'H_CUSTOMER' AND KEY_1 = MAT_GRP_1 AND ROWNUM=1) AS CUSTOMER " + "\n");
            strSqlString.Append("     , MAT_GRP_10 AS PKG" + "\n");
            strSqlString.Append("     , MAT_ID " + "\n");
            strSqlString.Append("     , MAT_CMF_10 AS PIN_TYPE " + "\n");
            strSqlString.Append("     , LOT_ID " + "\n");
            strSqlString.Append("     , QTY_1 " + "\n");
            strSqlString.Append("     , LOT_CMF_5 AS LOT_TYPE" + "\n");
            strSqlString.Append("     , START_DAY " + "\n");
            strSqlString.Append("     , END_DAY " + "\n");
            strSqlString.Append("     , OPER " + "\n");
            strSqlString.Append("     , OPER_DESC " + "\n");

            for (int i = 0; i <= 5; i++)
            {                
                strSqlString.Append("     , MAX(DECODE(TARGET, '" + cdvDate.Value.AddDays(i).ToString("yyyyMMdd") + "', TARGET_OPER_GRP)) AS D" + i + " " + "\n");                
            }

            strSqlString.Append("  FROM ( " + "\n");
            strSqlString.Append("        SELECT LOT_CMF_3" + "\n");
            strSqlString.Append("             , MAT_GRP_1" + "\n");
            strSqlString.Append("             , MAT_GRP_10" + "\n");
            strSqlString.Append("             , MAT_ID" + "\n");
            strSqlString.Append("             , MAT_CMF_10" + "\n");
            strSqlString.Append("             , LOT_ID " + "\n");
            strSqlString.Append("             , QTY_1 " + "\n");
            strSqlString.Append("             , LOT_CMF_5" + "\n");
            strSqlString.Append("             , START_DAY " + "\n");
            strSqlString.Append("             , END_DAY " + "\n");
            strSqlString.Append("             , OPER " + "\n");
            strSqlString.Append("             , OPER_DESC " + "\n");
            strSqlString.Append("             , LOT_PRIORITY" + "\n");
            strSqlString.Append("             , TARGET" + "\n");
            strSqlString.Append("             , WM_CONCAT(TARGET_OPER_GRP) AS TARGET_OPER_GRP " + "\n");
            strSqlString.Append("          FROM (" + "\n");
            strSqlString.Append("                SELECT B.LOT_CMF_3 " + "\n");
            strSqlString.Append("                     , A.MAT_GRP_1" + "\n");
            strSqlString.Append("                     , A.MAT_GRP_10" + "\n");
            strSqlString.Append("                     , A.MAT_ID" + "\n");
            strSqlString.Append("                     , A.MAT_CMF_10" + "\n");
            strSqlString.Append("                     , B.LOT_ID " + "\n");
            strSqlString.Append("                     , B.QTY_1 " + "\n");
            strSqlString.Append("                     , B.LOT_CMF_5" + "\n");
            strSqlString.Append("                     , SUBSTR(B.RESV_FIELD_1, 1, 8) AS START_DAY " + "\n");
            strSqlString.Append("                     , SUBSTR(B.SCH_DUE_TIME, 1, 8) AS END_DAY " + "\n");
            strSqlString.Append("                     , B.OPER " + "\n");
            strSqlString.Append("                     , (SELECT OPER_DESC FROM MWIPOPRDEF WHERE FACTORY = B.FACTORY AND OPER = B.OPER) AS OPER_DESC " + "\n");
            strSqlString.Append("                     , B.LOT_PRIORITY" + "\n");
            strSqlString.Append("                     , TO_CHAR(TO_DATE(SUBSTR(B.SCH_DUE_TIME, 1, 8), 'YYYYMMDD') - DECODE(B.LOT_PRIORITY, 1, LOT_1, LOT_5), 'YYYYMMDD') AS TARGET" + "\n");
            strSqlString.Append("                     , C.TARGET_OPER" + "\n");
            strSqlString.Append("                     , C.TARGET_OPER_GRP" + "\n");
            strSqlString.Append("                     , C.LOT_5" + "\n");
            strSqlString.Append("                     , C.LOT_1" + "\n");
            strSqlString.Append("                  FROM MWIPMATDEF A " + "\n");
            strSqlString.Append("                     , (" + "\n");
            strSqlString.Append("                        SELECT A.MAT_ID, B.FLOW, B.OPER AS TARGET_OPER" + "\n");
            strSqlString.Append("                             , CASE WHEN B.OPER = 'A2100' THEN 'FIN'" + "\n");
            strSqlString.Append("                                    WHEN B.OPER = 'A1000' THEN 'MLD'" + "\n");
            strSqlString.Append("                                    WHEN B.OPER = 'A0600' THEN 'WB'" + "\n");
            strSqlString.Append("                                    WHEN B.OPER = 'A0400' THEN 'DA'" + "\n");
            strSqlString.Append("                                    WHEN B.OPER LIKE 'A06%' THEN 'WB' || SUBSTR(B.OPER, -1)" + "\n");
            strSqlString.Append("                                    WHEN B.OPER = 'A0333' THEN 'CA'" + "\n");
            strSqlString.Append("                                    WHEN B.OPER LIKE 'A04%' THEN 'DA' || SUBSTR(B.OPER, -1)" + "\n");
            strSqlString.Append("                                    WHEN B.OPER = 'A0040' THEN 'DP'" + "\n");
            strSqlString.Append("                                END TARGET_OPER_GRP" + "\n");
            strSqlString.Append("                             , ROW_NUMBER() OVER(PARTITION BY MAT_ID ORDER BY SEQ_NUM DESC) AS LOT_5" + "\n");
            strSqlString.Append("                             , SUM(0.5) OVER(PARTITION BY MAT_ID ORDER BY SEQ_NUM DESC) AS LOT_1" + "\n");
            strSqlString.Append("                          FROM MWIPMATDEF A" + "\n");
            strSqlString.Append("                             , MWIPFLWOPR@RPTTOMES B" + "\n");
            strSqlString.Append("                         WHERE 1=1" + "\n");
            strSqlString.Append("                           AND A.FACTORY = B.FACTORY " + "\n");
            strSqlString.Append("                           AND A.FIRST_FLOW = B.FLOW " + "\n");
            strSqlString.Append("                           AND A.FACTORY = '" + cdvFactory.Text + "'  " + "\n");
            strSqlString.Append("                           AND A.MAT_TYPE = 'FG'" + "\n");
            strSqlString.Append("                           AND A.DELETE_FLAG = ' '" + "\n");
            strSqlString.Append("                           AND REGEXP_LIKE(B.OPER, 'A040*|A060*|A0040|A0333|A1000|A2100') " + "\n");
            strSqlString.Append("                       ) C" + "\n");

            if (DateTime.Now.ToString("yyyyMMdd") == cdvDate.SelectedValue())
            {
                strSqlString.Append("                     , RWIPLOTSTS B " + "\n");
                strSqlString.Append("                 WHERE 1 = 1 " + "\n");
            }
            else
            {
                strSqlString.Append("                     , RWIPLOTSTS_BOH B " + "\n");
                strSqlString.Append("                 WHERE B.CUTOFF_DT = '" + cdvDate.SelectedValue() + "22' " + "\n");                
            }                        
            
            strSqlString.Append("                   AND A.FACTORY = B.FACTORY  " + "\n");
            strSqlString.Append("                   AND A.MAT_ID = B.MAT_ID  " + "\n");
            strSqlString.Append("                   AND A.MAT_ID = C.MAT_ID(+) " + "\n");
            strSqlString.Append("                   AND B.FACTORY = '" + cdvFactory.Text + "'  " + "\n");
            strSqlString.Append("                   AND B.LOT_DEL_FLAG = ' ' " + "\n");
            strSqlString.Append("                   AND B.LOT_TYPE = 'W' " + "\n");
            strSqlString.Append("                   AND B.LOT_CMF_5 LIKE 'E%' " + "\n");
            strSqlString.Append("                   AND A.DELETE_FLAG = ' ' " + "\n");
            strSqlString.Append("                   AND B.SCH_DUE_TIME <> '19990101' " + "\n");


            if (txtProduct.Text.Trim() != "%" && txtProduct.Text.Trim() != "")
                strSqlString.AppendFormat("                   AND A.MAT_ID LIKE '" + txtProduct.Text + "' " + "\n");

            if (cdvLotType.txtValue != "ALL" || cdvLotType.txtValue != "")
            {
                strSqlString.Append("                   AND B.LOT_CMF_5 " + cdvLotType.SelectedValueToQueryString + "\n");
            }

            #region 상세 조회에 따른 SQL문 생성
            if (udcWIPCondition1.Text != "ALL" && udcWIPCondition1.Text != "")
                strSqlString.AppendFormat("                   AND A.MAT_GRP_1 {0}" + "\n", udcWIPCondition1.SelectedValueToQueryString);

            if (udcWIPCondition2.Text != "ALL" && udcWIPCondition2.Text != "")
                strSqlString.AppendFormat("                   AND A.MAT_GRP_2 {0} " + "\n", udcWIPCondition2.SelectedValueToQueryString);

            if (udcWIPCondition3.Text != "ALL" && udcWIPCondition3.Text != "")
                strSqlString.AppendFormat("                   AND A.MAT_GRP_3 {0} " + "\n", udcWIPCondition3.SelectedValueToQueryString);

            if (udcWIPCondition4.Text != "ALL" && udcWIPCondition4.Text != "")
                strSqlString.AppendFormat("                   AND A.MAT_GRP_4 {0} " + "\n", udcWIPCondition4.SelectedValueToQueryString);

            if (udcWIPCondition5.Text != "ALL" && udcWIPCondition5.Text != "")
                strSqlString.AppendFormat("                   AND A.MAT_GRP_5 {0} " + "\n", udcWIPCondition5.SelectedValueToQueryString);

            if (udcWIPCondition6.Text != "ALL" && udcWIPCondition6.Text != "")
                strSqlString.AppendFormat("                   AND A.MAT_GRP_6 {0} " + "\n", udcWIPCondition6.SelectedValueToQueryString);

            if (udcWIPCondition7.Text != "ALL" && udcWIPCondition7.Text != "")
                strSqlString.AppendFormat("                   AND A.MAT_GRP_7 {0} " + "\n", udcWIPCondition7.SelectedValueToQueryString);

            if (udcWIPCondition8.Text != "ALL" && udcWIPCondition8.Text != "")
                strSqlString.AppendFormat("                   AND A.MAT_GRP_8 {0} " + "\n", udcWIPCondition8.SelectedValueToQueryString);

            if (udcWIPCondition9.Text != "ALL" && udcWIPCondition9.Text != "")
                strSqlString.AppendFormat("                   AND A.MAT_GRP_9 {0} " + "\n", udcWIPCondition9.SelectedValueToQueryString);
            #endregion

            strSqlString.Append("               )" + "\n");
            strSqlString.Append("         GROUP BY LOT_CMF_3, MAT_GRP_1, MAT_GRP_10, MAT_ID, MAT_CMF_10, LOT_ID, QTY_1, LOT_CMF_5, START_DAY, END_DAY, OPER, OPER_DESC, LOT_PRIORITY, TARGET " + "\n");
            strSqlString.Append("       )" + "\n");
            strSqlString.Append(" GROUP BY LOT_CMF_3, MAT_GRP_1, MAT_GRP_10, MAT_ID, MAT_CMF_10, LOT_ID, QTY_1, LOT_CMF_5, START_DAY, END_DAY, OPER, OPER_DESC, LOT_PRIORITY" + "\n");
            strSqlString.Append(" ORDER BY LOT_CMF_3, MAT_GRP_1, MAT_GRP_10, MAT_ID, MAT_CMF_10, LOT_ID, OPER" + "\n");
            
            if (GlobalVariable.gsUserGroup == "ADMIN_GROUP" || GlobalVariable.gsUserGroup == "HANA_ADMIN_GROUP")
            {
                System.Windows.Forms.Clipboard.SetText(strSqlString.ToString());
            }

            return strSqlString.ToString();
        }       

        #endregion

        #region EVENT 처리
        /// <summary>
        /// 6. View 버튼 Action
        /// </summary>        ///         
        private void btnView_Click(object sender, EventArgs e)
        {
            DataTable dt = null;

            if (CheckField() == false) return;

            GridColumnInit();

            spdData_Sheet1.RowCount = 0;

            try
            {
                LoadingPopUp.LoadIngPopUpShow(this);

                dt = CmnFunction.oComm.GetFuncDataTable("DYNAMIC", MakeSqlString());

                if (dt.Rows.Count == 0)
                {
                    dt.Dispose();
                    LoadingPopUp.LoadingPopUpHidden();
                    CmnFunction.ShowMsgBox(RptMessages.GetMessage("STD010", GlobalVariable.gcLanguage));
                    return;
                }

                spdData.DataSource = dt;
                spdData.RPT_AutoFit(false);

                for (int i = 0; i <= 4; i++)
                {
                    spdData.ActiveSheet.Columns[i].BackColor = Color.LemonChiffon;
                }

                dt.Dispose();
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

        /// <summary>
        /// Excel Export
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnExcelExport_Click(object sender, EventArgs e)
        {
            if (spdData.ActiveSheet.Rows.Count > 0)
            {
                spdData.ExportExcel();            
            }
        }

        /// <summary>
        /// Factory 설정
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cdvFactory_ValueSelectedItemChanged(object sender, Miracom.UI.MCCodeViewSelChanged_EventArgs e)
        {
            this.SetFactory(cdvFactory.txtValue);
            cdvLotType.sFactory = cdvFactory.txtValue;// lot type 정할때 필요            
        }

        #endregion

        private void cdvLotType_ValueButtonPress(object sender, EventArgs e)
        {
            string strQuery = string.Empty;

            strQuery += "SELECT KEY_1 AS Code, '' Data" + "\n";
            strQuery += "  FROM MGCMTBLDAT " + "\n";
            strQuery += " WHERE 1=1 " + "\n";
            strQuery += "   AND FACTORY = '" + GlobalVariable.gsAssyDefaultFactory + "'" + "\n";
            strQuery += "   AND TABLE_NAME = 'H_TYPE' " + "\n";
            strQuery += "   AND KEY_1 LIKE 'E%' " + "\n";
            strQuery += " ORDER BY KEY_1 " + "\n";

            cdvLotType.sDynamicQuery = strQuery;
        }

       
    }
}
