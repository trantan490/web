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

namespace Hana.MAT
{
    public partial class MAT070502 : Miracom.SmartWeb.UI.Controls.udcCUSReport002
    {
        /// <summary>
        /// 클  래  스: MAT070502<br/>
        /// 클래스요약: 원부자재 투입 점검<br/>
        /// 작  성  자: 하나마이크론 임종우<br/>
        /// 최초작성일: 2013-02-06<br/>
        /// 상세  설명: 원부자재 투입 점검<br/>
        /// 변경  내용: <br/>
        /// 2013-02-18-임종우 : 투입 가능량이 "0" 이하인 경우 붉은색 음영표시(임태성 요청)
        #region " MAT070502 : Program Initial "

        public MAT070502()
        {
            InitializeComponent();            
            SortInit();            
            GridColumnInit();                       
            cdvFactory.Text = GlobalVariable.gsAssyDefaultFactory;            
        }

        #endregion
        

        #region " Function Definition "

        /// <summary 1. 유효성 검사>
        /// <remarks></remarks>
        /// </summary>
        /// <returns></returns>
        private Boolean CheckField()
        {
            if (String.IsNullOrEmpty(cdvMatType.Text))
            {
                CmnFunction.ShowMsgBox(RptMessages.GetMessage("STD003", GlobalVariable.gcLanguage));
                return false;
            }

            return true;
        }

        #endregion


        #region " GridColumnInit : Sheet Title 설정 "

        /// <summary>
        /// 2. 헤더 생성
        /// </summary>
        private void GridColumnInit()
        {            
            try
            {
                spdData.RPT_ColumnInit();
                spdData.RPT_AddBasicColumn("CUSTOMER", 0, 0, Visibles.True, Frozen.True, Align.Center, Merge.True, Formatter.String, 80);
                spdData.RPT_AddBasicColumn("FAMILY", 0, 1, Visibles.False, Frozen.True, Align.Center, Merge.True, Formatter.String, 80);
                spdData.RPT_AddBasicColumn("PACKAGE", 0, 2, Visibles.False, Frozen.True, Align.Center, Merge.True, Formatter.String, 80);
                spdData.RPT_AddBasicColumn("TYPE1", 0, 3, Visibles.False, Frozen.True, Align.Center, Merge.True, Formatter.String, 70);
                spdData.RPT_AddBasicColumn("TYPE2", 0, 4, Visibles.False, Frozen.True, Align.Center, Merge.True, Formatter.String, 70);
                spdData.RPT_AddBasicColumn("LD COUNT", 0, 5, Visibles.False, Frozen.True, Align.Center, Merge.True, Formatter.String, 70);
                spdData.RPT_AddBasicColumn("DENSITY", 0, 6, Visibles.False, Frozen.True, Align.Center, Merge.True, Formatter.String, 70);
                spdData.RPT_AddBasicColumn("GENERATION", 0, 7, Visibles.False, Frozen.True, Align.Center, Merge.True, Formatter.String, 70);
                spdData.RPT_AddBasicColumn("MAJOR CODE", 0, 8, Visibles.False, Frozen.True, Align.Center, Merge.True, Formatter.String, 70);
                spdData.RPT_AddBasicColumn("PIN TYPE", 0, 9, Visibles.True, Frozen.True, Align.Left, Merge.True, Formatter.String, 70);
                spdData.RPT_AddBasicColumn("PRODUCT", 0, 10, Visibles.True, Frozen.False, Align.Left, Merge.True, Formatter.String, 70); ;
                spdData.RPT_AddBasicColumn("CUST DEVICE", 0, 11, Visibles.False, Frozen.True, Align.Left, Merge.True, Formatter.String, 70);

                spdData.RPT_AddBasicColumn("STOCK WIP", 0, 12, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.String, 80);
                spdData.RPT_AddBasicColumn("Inputable quantity", 0, 13, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.String, 80);

                spdData.RPT_AddBasicColumn("Raw materials reservation status", 0, 14, Visibles.True, Frozen.False, Align.Center, Merge.False, Formatter.String, 80);
                spdData.RPT_AddBasicColumn("Reservation", 1, 14, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.String, 80);
                spdData.RPT_AddBasicColumn("availability", 1, 15, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.String, 80);
                spdData.RPT_MerageHeaderColumnSpan(0, 14, 2);

                spdData.RPT_AddBasicColumn("Raw materials", 0, 16, Visibles.True, Frozen.False, Align.Center, Merge.False, Formatter.String, 70);
                spdData.RPT_AddBasicColumn("STEP", 1, 16, Visibles.True, Frozen.False, Align.Center, Merge.False, Formatter.String, 70);
                spdData.RPT_AddBasicColumn("Material type", 1, 17, Visibles.True, Frozen.False, Align.Center, Merge.False, Formatter.String, 70);
                spdData.RPT_AddBasicColumn("Code", 1, 18, Visibles.True, Frozen.False, Align.Center, Merge.False, Formatter.String, 70);
                spdData.RPT_AddBasicColumn("Item name", 1, 19, Visibles.True, Frozen.False, Align.Left, Merge.False, Formatter.String, 100);
                spdData.RPT_MerageHeaderColumnSpan(0, 16, 4);

                spdData.RPT_AddBasicColumn("Inventory status", 0, 20, Visibles.True, Frozen.False, Align.Center, Merge.False, Formatter.String, 70);
                spdData.RPT_AddBasicColumn("TOTAL", 1, 20, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.String, 70);
                spdData.RPT_AddBasicColumn("Warehouse (purchase)", 1, 21, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.String, 70);
                spdData.RPT_AddBasicColumn("On-site warehouse", 1, 22, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.String, 70);
                spdData.RPT_AddBasicColumn("LINE", 1, 23, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.String, 70);
                spdData.RPT_MerageHeaderColumnSpan(0, 20, 4);

                spdData.RPT_AddBasicColumn("Order quantity", 0, 24, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.String, 70);

                spdData.RPT_AddBasicColumn("Unavailable inventory status", 0, 25, Visibles.True, Frozen.False, Align.Center, Merge.False, Formatter.String, 70);
                spdData.RPT_AddBasicColumn("Outsourcing (2205)", 1, 25, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.String, 70);
                spdData.RPT_AddBasicColumn("Suspension (1004)", 1, 26, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.String, 70);
                spdData.RPT_AddBasicColumn("a substitute product (1005)", 1, 27, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.String, 70);                
                spdData.RPT_MerageHeaderColumnSpan(0, 25, 3);

                for (int i = 0; i <= 13; i++)
                {
                    spdData.RPT_MerageHeaderRowSpan(0, i, 2);
                }
                                
                spdData.RPT_MerageHeaderRowSpan(0, 24, 2);

                // Group항목이 있을경우 반드시 선언해줄것.
                spdData.RPT_ColumnConfigFromTable(btnSort);

            }
            catch (Exception ex)
            {
                CmnFunction.ShowMsgBox(ex.Message);
                LoadingPopUp.LoadingPopUpHidden();
            }
        }

        #endregion


        #region " SortInit : Group By 설정 "

        /// <summary>
        /// 3. Group By 정의
        /// </summary>
        private void SortInit()
        {
            try
            {
                ((udcTableForm)(this.btnSort.BindingForm)).AddRow("CUSTOMER", "MAT.MAT_GRP_1", "MAT.MAT_GRP_1", "(SELECT DATA_1 FROM MGCMTBLDAT WHERE TABLE_NAME = 'H_CUSTOMER' AND KEY_1 = MAT.MAT_GRP_1 AND ROWNUM=1) AS CUSTOMER", true);
                ((udcTableForm)(this.btnSort.BindingForm)).AddRow("FAMILY", "MAT.MAT_GRP_2", "MAT.MAT_GRP_2", "MAT.MAT_GRP_2", false);
                ((udcTableForm)(this.btnSort.BindingForm)).AddRow("PACKAGE", "MAT.MAT_GRP_3", "MAT.MAT_GRP_3", "MAT.MAT_GRP_3", false);
                ((udcTableForm)(this.btnSort.BindingForm)).AddRow("TYPE1", "MAT.MAT_GRP_4", "MAT.MAT_GRP_4", "MAT.MAT_GRP_4", false);
                ((udcTableForm)(this.btnSort.BindingForm)).AddRow("TYPE2", "MAT.MAT_GRP_5", "MAT.MAT_GRP_5", "MAT.MAT_GRP_5", false);
                ((udcTableForm)(this.btnSort.BindingForm)).AddRow("LD COUNT", "MAT.MAT_GRP_6", "MAT.MAT_GRP_6", "MAT.MAT_GRP_6", false);
                ((udcTableForm)(this.btnSort.BindingForm)).AddRow("DENSITY", "MAT.MAT_GRP_7", "MAT.MAT_GRP_7", "MAT.MAT_GRP_7", false);
                ((udcTableForm)(this.btnSort.BindingForm)).AddRow("GENERATION", "MAT.MAT_GRP_8", "MAT.MAT_GRP_8", "MAT.MAT_GRP_8", false);
                ((udcTableForm)(this.btnSort.BindingForm)).AddRow("MAJOR CODE", "MAT.MAT_GRP_9", "MAT.MAT_GRP_9", "MAT.MAT_GRP_9", false);
                ((udcTableForm)(this.btnSort.BindingForm)).AddRow("PIN TYPE", "MAT.MAT_CMF_10", "MAT.MAT_CMF_10", "MAT.MAT_CMF_10", true);
                ((udcTableForm)(this.btnSort.BindingForm)).AddRow("PRODUCT", "MAT.MAT_ID", "MAT.MAT_ID", "MAT.MAT_ID", true);
                ((udcTableForm)(this.btnSort.BindingForm)).AddRow("CUST DEVICE", "MAT.MAT_CMF_7", "MAT.MAT_CMF_7", "MAT.MAT_CMF_7", false);
            }
            catch (Exception ex)
            {
                CmnFunction.ShowMsgBox(ex.Message);
                LoadingPopUp.LoadingPopUpHidden();
            }
        }

        #endregion

        

        #region " MakeSqlString : Sql Query문 "

        /// <summary>
        /// 4. 쿼리 생성
        /// </summary>
        /// <returns></returns>
        private string MakeSqlString()
        {
            string QueryCond1 = null;
            string QueryCond2 = null;
            string QueryCond3 = null;

            StringBuilder strSqlString = new StringBuilder();

            udcTableForm tableForm = (udcTableForm)btnSort.BindingForm;

            QueryCond1 = tableForm.SelectedValueToQueryContainNull;
            QueryCond2 = tableForm.SelectedValue2ToQueryContainNull;
            QueryCond3 = tableForm.SelectedValue3ToQueryContainNull;

            strSqlString.AppendFormat("SELECT {0} " + "\n", QueryCond3);
            strSqlString.AppendFormat("     , WIP_STOCK" + "\n");
            strSqlString.AppendFormat("     , ROUND(DECODE(UNIT_QTY, 0, 0, (WIP_MAT_TTL - REV_QTY) * 1000 / UNIT_QTY), 1) AS IN_POS" + "\n");
            strSqlString.AppendFormat("     , ROUND(REV_QTY, 1) AS REV_QTY" + "\n");
            strSqlString.AppendFormat("     , ROUND(WIP_MAT_TTL - REV_QTY, 1) AS AVA_QTY " + "\n");
            strSqlString.AppendFormat("     , OPER" + "\n");
            strSqlString.AppendFormat("     , DAT.MAT_TYPE" + "\n");
            strSqlString.AppendFormat("     , MATCODE" + "\n");
            strSqlString.AppendFormat("     , DESCRIPT" + "\n");
            strSqlString.AppendFormat("     , ROUND(WIP_MAT_TTL, 1) AS WIP_MAT_TTL" + "\n");
            strSqlString.AppendFormat("     , ROUND(INV_QTY, 1) AS INV_QTY" + "\n");
            strSqlString.AppendFormat("     , ROUND(INV_L_QTY, 1) AS INV_L_QTY" + "\n");
            strSqlString.AppendFormat("     , ROUND(MAT_QTY, 1) AS MAT_QTY" + "\n");
            strSqlString.AppendFormat("     , ROUND(ORDER_QTY, 1) AS ORDER_QTY" + "\n");
            strSqlString.AppendFormat("     , ROUND(QTY_2205, 1) AS QTY_2205" + "\n");
            strSqlString.AppendFormat("     , ROUND(QTY_1004, 1) AS QTY_1004" + "\n");
            strSqlString.AppendFormat("     , ROUND(QTY_1005, 1) AS QTY_1005" + "\n");
            strSqlString.AppendFormat("  FROM MWIPMATDEF MAT " + "\n");
            strSqlString.AppendFormat("     , ( " + "\n");
            strSqlString.AppendFormat("        SELECT SMM.PARTNUMBER " + "\n");
            strSqlString.AppendFormat("             , SMM.UNIT_QTY " + "\n");
            strSqlString.AppendFormat("             , NVL(WIP.WIP_STOCK,0) AS WIP_STOCK  " + "\n");
            strSqlString.AppendFormat("             , CASE WHEN SMM.MAT_TYPE = 'TE' THEN WIP_TE * UNIT_QTY / 1000 " + "\n");
            strSqlString.AppendFormat("                    WHEN SMM.MAT_TYPE IN ('PB', 'LF') THEN WIP_PB * UNIT_QTY / 1000 " + "\n");
            strSqlString.AppendFormat("                    WHEN SMM.MAT_TYPE = 'MC' THEN WIP_MC * UNIT_QTY / 1000 " + "\n");
            strSqlString.AppendFormat("                    WHEN SMM.MAT_TYPE = 'SB' THEN WIP_SB * UNIT_QTY / 1000 " + "\n");
            strSqlString.AppendFormat("                    ELSE WIP_GW * UNIT_QTY / 1000 " + "\n");
            strSqlString.AppendFormat("               END AS REV_QTY " + "\n");
            strSqlString.AppendFormat("             , SMM.OPER " + "\n");
            strSqlString.AppendFormat("             , SMM.MAT_TYPE " + "\n");
            strSqlString.AppendFormat("             , SMM.MATCODE " + "\n");
            strSqlString.AppendFormat("             , SMM.DESCRIPT " + "\n");
            strSqlString.AppendFormat("             , SMM.WIP_MAT_TTL " + "\n");
            strSqlString.AppendFormat("             , SMM.INV_QTY " + "\n");
            strSqlString.AppendFormat("             , SMM.INV_L_QTY " + "\n");
            strSqlString.AppendFormat("             , SMM.MAT_QTY " + "\n");
            strSqlString.AppendFormat("             , SMM.ORDER_QTY " + "\n");
            strSqlString.AppendFormat("             , SMM.QTY_2205 " + "\n");
            strSqlString.AppendFormat("             , SMM.QTY_1004 " + "\n");
            strSqlString.AppendFormat("             , SMM.QTY_1005 " + "\n");
            strSqlString.AppendFormat("          FROM RSUMWIPMAT SMM " + "\n");
            strSqlString.AppendFormat("             , ( " + "\n");
            strSqlString.AppendFormat("                SELECT /*+ INDEX(RWIPLOTSTS RWIPLOTSTS_IDX_4) */ MAT_ID " + "\n");
            strSqlString.AppendFormat("                     , SUM(DECODE(OPER, 'A0000', QTY_1)) AS WIP_STOCK " + "\n");
            strSqlString.AppendFormat("                     , SUM(CASE WHEN OPER BETWEEN 'A0010' AND 'A0040' THEN QTY_1 ELSE 0 END) AS WIP_TE " + "\n");
            strSqlString.AppendFormat("                     , SUM(CASE WHEN OPER BETWEEN 'A0010' AND 'A0401' THEN QTY_1 ELSE 0 END) AS WIP_PB " + "\n");
            strSqlString.AppendFormat("                     , SUM(CASE WHEN OPER BETWEEN 'A0010' AND 'A0609' THEN QTY_1 ELSE 0 END) AS WIP_GW " + "\n");
            strSqlString.AppendFormat("                     , SUM(CASE WHEN OPER BETWEEN 'A0010' AND 'A1000' THEN QTY_1 ELSE 0 END) AS WIP_MC " + "\n");
            strSqlString.AppendFormat("                     , SUM(CASE WHEN OPER BETWEEN 'A0010' AND 'A1300' THEN QTY_1 ELSE 0 END) AS WIP_SB " + "\n");
            strSqlString.AppendFormat("                  FROM RWIPLOTSTS " + "\n");
            strSqlString.AppendFormat("                 WHERE 1=1 " + "\n");
            strSqlString.AppendFormat("                   AND FACTORY = '" + GlobalVariable.gsAssyDefaultFactory + "' " + "\n");
            strSqlString.AppendFormat("                   AND LOT_DEL_FLAG = ' ' " + "\n");
            strSqlString.AppendFormat("                   AND LOT_TYPE = 'W' " + "\n");
            //strSqlString.AppendFormat("                   AND OPER IN ('A0000','A0020','A0040','A0200','A0400','A0600','A0800','A0950','A1000','A1300') " + "\n");

            if (cdvLotType.Text != "ALL")
            {
                strSqlString.AppendFormat("                   AND LOT_CMF_5 LIKE '" + cdvLotType.Text + "' " + "\n");
            }
            
            strSqlString.AppendFormat("                 GROUP BY MAT_ID " + "\n");
            strSqlString.AppendFormat("               ) WIP" + "\n");
            strSqlString.AppendFormat("         WHERE SMM.PARTNUMBER = WIP.MAT_ID " + "\n");
            strSqlString.AppendFormat("       ) DAT " + "\n");
            strSqlString.AppendFormat(" WHERE 1=1 " + "\n");
            strSqlString.AppendFormat("   AND MAT.MAT_ID = DAT.PARTNUMBER " + "\n");
            strSqlString.AppendFormat("   AND MAT.FACTORY = '" + GlobalVariable.gsAssyDefaultFactory + "' " + "\n");
            strSqlString.AppendFormat("   AND MAT.DELETE_FLAG = ' '" + "\n");

            // Stock 보유 재공만 조회 가능하도록...
            if (ckbStock.Checked == true)
                strSqlString.AppendFormat("   AND DAT.WIP_STOCK > 0 " + "\n");

            if (cdvMatType.Text != "ALL" && cdvMatType.Text != "")
                strSqlString.AppendFormat("   AND DAT.MAT_TYPE " + cdvMatType.SelectedValueToQueryString + "\n");

            if (txtMatCode.Text.Trim() != "%" && txtMatCode.Text.Trim() != "")
                strSqlString.AppendFormat("   AND MAT.MAT_ID LIKE '{0}'" + "\n", txtMatCode.Text);

            //상세 조회에 따른 SQL문 생성                        
            if (udcWIPCondition1.Text != "ALL" && udcWIPCondition1.Text != "")
                strSqlString.AppendFormat("   AND MAT.MAT_GRP_1 {0}" + "\n", udcWIPCondition1.SelectedValueToQueryString);

            if (udcWIPCondition2.Text != "ALL" && udcWIPCondition2.Text != "")
                strSqlString.AppendFormat("   AND MAT.MAT_GRP_2 {0} " + "\n", udcWIPCondition2.SelectedValueToQueryString);

            if (udcWIPCondition3.Text != "ALL" && udcWIPCondition3.Text != "")
                strSqlString.AppendFormat("   AND MAT.MAT_GRP_3 {0} " + "\n", udcWIPCondition3.SelectedValueToQueryString);

            if (udcWIPCondition4.Text != "ALL" && udcWIPCondition4.Text != "")
                strSqlString.AppendFormat("   AND MAT.MAT_GRP_4 {0} " + "\n", udcWIPCondition4.SelectedValueToQueryString);

            if (udcWIPCondition5.Text != "ALL" && udcWIPCondition5.Text != "")
                strSqlString.AppendFormat("   AND MAT.MAT_GRP_5 {0} " + "\n", udcWIPCondition5.SelectedValueToQueryString);

            if (udcWIPCondition6.Text != "ALL" && udcWIPCondition6.Text != "")
                strSqlString.AppendFormat("   AND MAT.MAT_GRP_6 {0} " + "\n", udcWIPCondition6.SelectedValueToQueryString);

            if (udcWIPCondition7.Text != "ALL" && udcWIPCondition7.Text != "")
                strSqlString.AppendFormat("   AND MAT.MAT_GRP_7 {0} " + "\n", udcWIPCondition7.SelectedValueToQueryString);

            if (udcWIPCondition8.Text != "ALL" && udcWIPCondition8.Text != "")
                strSqlString.AppendFormat("   AND MAT.MAT_GRP_8 {0} " + "\n", udcWIPCondition8.SelectedValueToQueryString);

            if (udcWIPCondition9.Text != "ALL" && udcWIPCondition9.Text != "")
                strSqlString.AppendFormat("   AND MAT.MAT_GRP_9 {0} " + "\n", udcWIPCondition9.SelectedValueToQueryString);

            //strSqlString.AppendFormat(" GROUP BY {0}, WIP.OPER, WIP.MAT_TYPE, WIP.MATCODE, WIP.DESCRIPT " + "\n", QueryCond1);
            strSqlString.AppendFormat(" ORDER BY {0}, OPER, MAT_TYPE, MATCODE, DESCRIPT " + "\n", QueryCond1);            

            if (GlobalVariable.gsUserGroup == "ADMIN_GROUP" || GlobalVariable.gsUserGroup == "HANA_ADMIN_GROUP")
            {
                System.Windows.Forms.Clipboard.SetText(strSqlString.ToString());
            }

            return strSqlString.ToString();
        }

        #endregion

        #region " Event 처리 "


        #region " btnView_Click : View버튼을 선택했을 때 "

        private void btnView_Click(object sender, EventArgs e)
        {
            DataTable dt = null;
            DataTable dt1 = null;

            StringBuilder strSqlString = new StringBuilder();

            // 마지막 SUMMARY 시간 가져오기
            strSqlString.Append("SELECT DISTINCT TO_DATE(UPDATE_TIME,'YYYY-MM-DD HH24MISS') FROM RSUMWIPMAT");

            dt1 = CmnFunction.oComm.GetFuncDataTable("DYNAMIC", strSqlString.ToString());
            lblUpdateTime.Text = dt1.Rows[0][0].ToString();


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

                //int[] rowType = spdData.RPT_DataBindingWithSubTotal(dt, 0, 1, 12, null, null);
                
                //Total부분 셀머지
                //spdData.RPT_FillDataSelectiveCells("Total", 0, 12, 0, 1, true, Align.Center, VerticalAlign.Center);
                                
                spdData.RPT_AutoFit(false);

                // 2013-02-18-임종우 : 투입가능량이 0 이하인 경우 색상 표시 함 (임태성 요청)
                for (int i = 0; i < spdData.ActiveSheet.RowCount; i++)
                {
                    if (Convert.ToDouble(spdData.ActiveSheet.Cells[i, 13].Value) <= 0)
                    {
                        //spdData.ActiveSheet.Rows[i].BackColor = Color.GreenYellow;
                        for (int y = 12; y <= 27; y++)
                        {
                            spdData.ActiveSheet.Cells[i, y].BackColor = Color.GreenYellow;
                        }
                    }
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

        #endregion


        #region " btnExcelExport_Click : Excel로 내보내기 "

        /// <summary>
        /// Excel Export
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnExcelExport_Click(object sender, EventArgs e)
        {
            //ExcelHelper.Instance.subMakeExcel(spdData, this.lblTitle.Text, null, null, true);
            spdData.ExportExcel();
        }

        #endregion

        private void cdvMatType_ValueButtonPress(object sender, EventArgs e)
        {
            StringBuilder strSqlString = new StringBuilder();

            strSqlString.Append("SELECT KEY_1 AS Code, DATA_1 AS Data " + "\n");
            strSqlString.Append("  FROM MGCMTBLDAT " + "\n");
            strSqlString.Append(" WHERE FACTORY = '" + GlobalVariable.gsAssyDefaultFactory + "' " + "\n");
            strSqlString.Append("   AND TABLE_NAME = 'MATERIAL_TYPE' " + "\n");
            strSqlString.Append("   AND KEY_1 IN ('TE','PB','LF','GW','SW','CW','MC', 'SB') " + "\n");
            strSqlString.Append(" ORDER BY KEY_1 " + "\n");

            cdvMatType.sDynamicQuery = strSqlString.ToString();
        }

        #endregion

    }
}
