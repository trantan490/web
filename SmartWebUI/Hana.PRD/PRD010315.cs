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
    public partial class PRD010315 : Miracom.SmartWeb.UI.Controls.udcCUSReport002
    {
        /// <summary>
        /// 클  래  스: PRD010315<br/>
        /// 클래스요약: ASSY Lot 직행율<br/>
        /// 작  성  자: 임종우<br/>
        /// 최초작성일: 2017-11-23<br/>
        /// 상세  설명: ASSY Lot 직행율(이승희 요청)<br/>
        /// 변경  내용: <br/>         
        /// </summary>
        public PRD010315()
        {
            InitializeComponent();                                   
            SortInit();
            cdvFromToDate.AutoBinding();
            cdvFromToDate.DaySelector.Visible = false;
            GridColumnInit();
            this.cdvFactory.sFactory = GlobalVariable.gsAssyDefaultFactory;
            this.SetFactory(GlobalVariable.gsAssyDefaultFactory);
            cdvFactory.Text = GlobalVariable.gsAssyDefaultFactory;
        }

        #region 초기화 및 유효성 검사
        /// <summary 1. 유효성 검사>
        /// <remarks></remarks>
        /// </summary>
        /// <returns></returns>
        private Boolean CheckField()
        {
            //if (cdvFactory.Text.TrimEnd() == "")
            //{
            //    CmnFunction.ShowMsgBox(RptMessages.GetMessage("STD001", GlobalVariable.gcLanguage));
            //    return false;
            //}

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
                spdData.RPT_AddBasicColumn("CUSTOMER", 0, 0, Visibles.True, Frozen.True, Align.Left, Merge.True, Formatter.String, 60);
                spdData.RPT_AddBasicColumn("PIN_TYPE", 0, 1, Visibles.False, Frozen.True, Align.Left, Merge.True, Formatter.String, 60);
                spdData.RPT_AddBasicColumn("MAJOR", 0, 2, Visibles.False, Frozen.True, Align.Left, Merge.True, Formatter.String, 60);
                spdData.RPT_AddBasicColumn("PKG", 0, 3, Visibles.False, Frozen.True, Align.Left, Merge.True, Formatter.String, 60);
                spdData.RPT_AddBasicColumn("LD_COUNT", 0, 4, Visibles.False, Frozen.True, Align.Left, Merge.True, Formatter.String, 60);
                spdData.RPT_AddBasicColumn("PKG_CODE", 0, 5, Visibles.False, Frozen.True, Align.Left, Merge.True, Formatter.String, 60);
                spdData.RPT_AddBasicColumn("FAMILY", 0, 6, Visibles.False, Frozen.True, Align.Left, Merge.True, Formatter.String, 60);
                spdData.RPT_AddBasicColumn("PRODUCT", 0, 7, Visibles.True, Frozen.True, Align.Left, Merge.True, Formatter.String, 80);
                spdData.RPT_AddBasicColumn("TYPE_1", 0, 8, Visibles.False, Frozen.True, Align.Left, Merge.True, Formatter.String, 60);
                spdData.RPT_AddBasicColumn("TYPE_2", 0, 9, Visibles.False, Frozen.True, Align.Left, Merge.True, Formatter.String, 60);
                spdData.RPT_AddBasicColumn("DENSITY", 0, 10, Visibles.False, Frozen.True, Align.Left, Merge.True, Formatter.String, 80);
                spdData.RPT_AddBasicColumn("GENERATION", 0, 11, Visibles.False, Frozen.True, Align.Left, Merge.True, Formatter.String, 80);                

                spdData.RPT_AddBasicColumn("Invoice No", 0, 12, Visibles.True, Frozen.True, Align.Left, Merge.False, Formatter.String, 100);
                spdData.RPT_AddBasicColumn("Ship Site", 0, 13, Visibles.True, Frozen.True, Align.Left, Merge.False, Formatter.String, 70);
                spdData.RPT_AddBasicColumn("Lot Id", 0, 14, Visibles.True, Frozen.True, Align.Left, Merge.False, Formatter.String, 100);
                spdData.RPT_AddBasicColumn("SHIP", 0, 15, Visibles.True, Frozen.False, Align.Left, Merge.False, Formatter.String, 100);
                spdData.RPT_AddBasicColumn("Daily standard", 0, 16, Visibles.True, Frozen.False, Align.Center, Merge.False, Formatter.String, 70);
                spdData.RPT_AddBasicColumn("Type", 0, 17, Visibles.True, Frozen.False, Align.Center, Merge.False, Formatter.String, 50);                
                spdData.RPT_AddBasicColumn("In Qty", 0, 18, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 70);
                spdData.RPT_AddBasicColumn("Ship Qty", 0, 19, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 70);
                spdData.RPT_AddBasicColumn("Yield", 0, 20, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Double1, 70);
                spdData.RPT_AddBasicColumn("HOLD Count", 0, 21, Visibles.True, Frozen.True, Align.Right, Merge.False, Formatter.Number, 70);
                                                
                spdData.RPT_AddBasicColumn("LAMI", 0, 22, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 60);
                spdData.RPT_AddBasicColumn("Pre B/G", 0, 23, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 60);
                spdData.RPT_AddBasicColumn("Stealth", 0, 24, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 60);
                spdData.RPT_AddBasicColumn("BG", 0, 25, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 60);
                spdData.RPT_AddBasicColumn("LG", 0, 26, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 60);
                spdData.RPT_AddBasicColumn("SAW", 0, 27, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 60);
                spdData.RPT_AddBasicColumn("DDS", 0, 28, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 60);
                spdData.RPT_AddBasicColumn("CA", 0, 29, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 60);
                spdData.RPT_AddBasicColumn("DA", 0, 30, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 60);
                spdData.RPT_AddBasicColumn("WB", 0, 31, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 60);
                spdData.RPT_AddBasicColumn("MOLD", 0, 32, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 60);
                spdData.RPT_AddBasicColumn("PCB MK", 0, 33, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 60);
                spdData.RPT_AddBasicColumn("MK", 0, 34, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 60);
                spdData.RPT_AddBasicColumn("SBA", 0, 35, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 60);
                spdData.RPT_AddBasicColumn("STRIP TEST", 0, 36, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 60);
                spdData.RPT_AddBasicColumn("SST", 0, 37, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 60);
                spdData.RPT_AddBasicColumn("DC TEST", 0, 38, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 60);
                spdData.RPT_AddBasicColumn("PKG Grinding", 0, 39, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 60);
                spdData.RPT_AddBasicColumn("QC", 0, 40, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 60);                
               
                spdData.RPT_ColumnConfigFromTable(btnSort);
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
            ((udcTableForm)(this.btnSort.BindingForm)).Clear();
            ((udcTableForm)(this.btnSort.BindingForm)).AddRow("CUSTOMER", "(SELECT DATA_1 FROM MGCMTBLDAT@RPTTOMES WHERE FACTORY = '" + GlobalVariable.gsAssyDefaultFactory + "' AND TABLE_NAME = 'H_CUSTOMER' AND KEY_1 = MAT.MAT_GRP_1) AS CUSTOMER", "MAT.MAT_GRP_1", "MAT_GRP_1", true);
            ((udcTableForm)(this.btnSort.BindingForm)).AddRow("PIN_TYPE", "MAT.MAT_CMF_10 AS PIN_TYPE", "MAT.MAT_CMF_10", "MAT_CMF_10", false);
            ((udcTableForm)(this.btnSort.BindingForm)).AddRow("MAJOR", "MAT.MAT_GRP_9 AS MAJOR", "MAT.MAT_GRP_9", "MAT_GRP_9", false);
            ((udcTableForm)(this.btnSort.BindingForm)).AddRow("PKG", "MAT.MAT_GRP_10 AS PKG", "MAT.MAT_GRP_10", "MAT_GRP_10", false);
            ((udcTableForm)(this.btnSort.BindingForm)).AddRow("LD_COUNT", "MAT.MAT_GRP_6 AS LD_COUNT", "MAT.MAT_GRP_6", "MAT_GRP_6", false);
            ((udcTableForm)(this.btnSort.BindingForm)).AddRow("PKG_CODE", "MAT.MAT_CMF_11 AS PKG_CODE", "MAT.MAT_CMF_11", "MAT_CMF_11", false);
            ((udcTableForm)(this.btnSort.BindingForm)).AddRow("FAMILY", "MAT.MAT_GRP_2 AS FAMILY", "MAT.MAT_GRP_2", "MAT_GRP_2", false);
            ((udcTableForm)(this.btnSort.BindingForm)).AddRow("PRODUCT", "MAT.MAT_ID AS PRODUCT", "MAT.MAT_ID", "CONV_MAT_ID", true);
            ((udcTableForm)(this.btnSort.BindingForm)).AddRow("TYPE_1", "MAT.MAT_GRP_4 AS TYPE_1", "MAT.MAT_GRP_4", "MAT_GRP_4", false);
            ((udcTableForm)(this.btnSort.BindingForm)).AddRow("TYPE_2", "MAT.MAT_GRP_5 AS TYPE_2", "MAT.MAT_GRP_5", "MAT_GRP_5", false);
            ((udcTableForm)(this.btnSort.BindingForm)).AddRow("DENSITY", "MAT.MAT_GRP_7 AS DENSITY", "MAT.MAT_GRP_7", "MAT_GRP_7", false);
            ((udcTableForm)(this.btnSort.BindingForm)).AddRow("GENERATION", "MAT.MAT_GRP_8 AS GENERATION", "MAT.MAT_GRP_8", "MAT_GRP_8", false);    
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
            string sFromDate;
            string sEndDate;
            
            udcTableForm tableForm = (udcTableForm)btnSort.BindingForm;
            QueryCond1 = tableForm.SelectedValueToQueryContainNull;
            QueryCond2 = tableForm.SelectedValue2ToQueryContainNull;
            QueryCond3 = tableForm.SelectedValue3ToQueryContainNull;

            sFromDate = cdvFromToDate.ExactFromDate;
            sEndDate = cdvFromToDate.ExactToDate;


            strSqlString.Append("WITH DT AS" + "\n");
            strSqlString.Append("(" + "\n");
            strSqlString.Append(" SELECT MAT_ID, INVOICE_NO, LOT_CMF_11 AS SHIP_SITE, LOT_ID, TRAN_TIME, WORK_DATE, LOT_CMF_5 AS LOT_TYPE, SHIP_QTY_1" + "\n");
            strSqlString.Append("      , (SELECT SUM(LOSS_QTY) FROM RWIPLOTLSM WHERE FACTORY = A.FROM_FACTORY AND LOT_ID = A.LOT_ID AND HIST_DEL_FLAG = ' ') AS LOSS_QTY" + "\n");
            strSqlString.Append("   FROM VWIPSHPLOT A" + "\n");
            strSqlString.Append("  WHERE 1=1" + "\n");
            
            strSqlString.Append("    AND FROM_FACTORY = '" + cdvFactory.Text + "'" + "\n");
            strSqlString.Append("    AND TRAN_TIME BETWEEN '" + sFromDate + "' AND '" + sEndDate + "'" + "\n");

            if (cdvType.Text != "ALL")
            {
                strSqlString.Append("    AND LOT_CMF_5 LIKE '" + cdvType.Text + "' " + "\n");
            }

            strSqlString.Append(")" + "\n");
            strSqlString.Append("SELECT " + QueryCond1 + "\n");
            strSqlString.Append("     , SHP.INVOICE_NO" + "\n");
            strSqlString.Append("     , SHP.SHIP_SITE" + "\n");
            strSqlString.Append("     , SHP.LOT_ID" + "\n");
            strSqlString.Append("     , SHP.TRAN_TIME" + "\n");
            strSqlString.Append("     , SHP.WORK_DATE" + "\n");
            strSqlString.Append("     , SHP.LOT_TYPE" + "\n");
            strSqlString.Append("     , NVL(SHP.SHIP_QTY_1,0) + NVL(SHP.LOSS_QTY,0) AS IN_QTY" + "\n");
            strSqlString.Append("     , SHP.SHIP_QTY_1" + "\n");
            strSqlString.Append("     , ROUND(NVL(SHP.SHIP_QTY_1,0) / (NVL(SHP.SHIP_QTY_1,0) + NVL(SHP.LOSS_QTY,0)) * 100, 1) AS YIELD" + "\n");
            strSqlString.Append("     , HLD.TTL_HOLD_CNT" + "\n");
            strSqlString.Append("     , HLD.LAMI, HLD.PRI_BG, HLD.STEALTH, HLD.BG, HLD.LG, HLD.SAW, HLD.DDS" + "\n");
            strSqlString.Append("     , HLD.CA, HLD.DA, HLD.WB, HLD.MOLD, HLD.PCB_MK, HLD.MK, HLD.SBA" + "\n");
            strSqlString.Append("     , HLD.STRIP_TEST, HLD.SST, HLD.DC_TEST, HLD.PKG_GRINDING, HLD.QC" + "\n");
            strSqlString.Append("  FROM MWIPMATDEF MAT" + "\n");
            strSqlString.Append("     , DT SHP" + "\n");
            strSqlString.Append("     , (   " + "\n");
            strSqlString.Append("        SELECT LOT_ID" + "\n");
            strSqlString.Append("             , COUNT(LOT_ID) AS TTL_HOLD_CNT" + "\n");
            strSqlString.Append("             , SUM(DECODE(OPER, 'A0020', CNT, 0)) AS LAMI" + "\n");
            strSqlString.Append("             , SUM(DECODE(OPER, 'A0030', CNT, 0)) AS PRI_BG" + "\n");
            strSqlString.Append("             , SUM(DECODE(OPER, 'A0033', CNT, 0)) AS STEALTH " + "\n");
            strSqlString.Append("             , SUM(DECODE(OPER, 'A0040', CNT, 0)) AS BG" + "\n");
            strSqlString.Append("             , SUM(DECODE(OPER, 'A0170', CNT, 0)) AS LG" + "\n");
            strSqlString.Append("             , SUM(DECODE(OPER, 'A0200', CNT, 0)) AS SAW" + "\n");
            strSqlString.Append("             , SUM(DECODE(OPER, 'A0230', CNT, 0)) AS DDS" + "\n");
            strSqlString.Append("             , SUM(DECODE(OPER, 'A0333', CNT, 0)) AS CA" + "\n");
            strSqlString.Append("             , SUM(CASE WHEN OPER LIKE 'A040%' THEN CNT ELSE 0 END) AS DA" + "\n");
            strSqlString.Append("             , SUM(CASE WHEN OPER LIKE 'A060%' THEN CNT ELSE 0 END) AS WB" + "\n");
            strSqlString.Append("             , SUM(DECODE(OPER, 'A1000', CNT, 0)) AS MOLD" + "\n");
            strSqlString.Append("             , SUM(DECODE(OPER, 'A1020', CNT, 0)) AS PCB_MK" + "\n");
            strSqlString.Append("             , SUM(DECODE(OPER, 'A1150', CNT, 'A1500', CNT, 0)) AS MK" + "\n");
            strSqlString.Append("             , SUM(DECODE(OPER, 'A1300', CNT, 0)) AS SBA " + "\n");
            strSqlString.Append("             , SUM(DECODE(OPER, 'A1710', CNT, 'A1840', CNT, 0)) AS STRIP_TEST" + "\n");
            strSqlString.Append("             , SUM(DECODE(OPER, 'A1750', CNT, 'A1825', CNT, 0)) AS SST " + "\n");
            strSqlString.Append("             , SUM(DECODE(OPER, 'A1790', CNT, 'A1795', CNT, 0)) AS DC_TEST " + "\n");
            strSqlString.Append("             , SUM(DECODE(OPER, 'A1810', CNT, 0)) AS PKG_GRINDING " + "\n");
            strSqlString.Append("             , SUM(DECODE(OPER, 'A2100', CNT, 0)) AS QC " + "\n");
            strSqlString.Append("          FROM (" + "\n");
            strSqlString.Append("                SELECT LOT_ID, OPER, COUNT(*) AS CNT " + "\n");
            strSqlString.Append("                  FROM RWIPLOTHLD " + "\n");
            strSqlString.Append("                 WHERE 1=1 " + "\n");
            strSqlString.Append("                   AND FACTORY = '" + cdvFactory.Text + "'" + "\n");
            strSqlString.Append("                   AND LOT_ID IN (SELECT LOT_ID FROM DT)" + "\n");
            strSqlString.Append("                   AND REGEXP_LIKE(OPER, 'A0020|A0030|A0033|A0040|A0170|A0200|A0230|A0333|A040*|A060*|A1000|A1020|A1150|A1300|A1500|A1710|A1750|A1790|A1795|A1810|A1825|A1840|A2100')" + "\n");

            if (cdvHoldCode.Text != "ALL" && cdvHoldCode.Text != "")
            {
                strSqlString.AppendFormat("                   AND HOLD_CODE {0}" + "\n", cdvHoldCode.SelectedValueToQueryString);
            }

            strSqlString.Append("                 GROUP BY LOT_ID, OPER" + "\n");
            strSqlString.Append("               )" + "\n");
            strSqlString.Append("         GROUP BY LOT_ID" + "\n");
            strSqlString.Append("       ) HLD" + "\n");
            strSqlString.Append(" WHERE 1=1" + "\n");
            strSqlString.Append("   AND MAT.MAT_ID = SHP.MAT_ID" + "\n");
            strSqlString.Append("   AND SHP.LOT_ID = HLD.LOT_ID(+)" + "\n");
            strSqlString.Append("   AND MAT.FACTORY = '" + cdvFactory.Text + "'" + "\n");
            strSqlString.Append("   AND MAT.DELETE_FLAG = ' '" + "\n");

            if (txtProduct.Text.Trim() != "%" && txtProduct.Text.Trim() != "")
            {
                strSqlString.AppendFormat("   AND MAT.MAT_ID LIKE '{0}'" + "\n", txtProduct.Text);
            }

            #region 상세 조회에 따른 SQL문 생성
            if (udcBUMPCondition1.Text != "ALL" && udcBUMPCondition1.Text != "")
                strSqlString.AppendFormat("   AND MAT.CUSTOMER_ID {0} " + "\n", udcBUMPCondition1.SelectedValueToQueryString);

            if (udcBUMPCondition2.Text != "ALL" && udcBUMPCondition2.Text != "")
                strSqlString.AppendFormat("   AND MAT.MAT_GRP_01 {0} " + "\n", udcBUMPCondition2.SelectedValueToQueryString);

            if (udcBUMPCondition3.Text != "ALL" && udcBUMPCondition3.Text != "")
                strSqlString.AppendFormat("   AND MAT.MAT_GRP_02 {0} " + "\n", udcBUMPCondition3.SelectedValueToQueryString);

            if (udcBUMPCondition4.Text != "ALL" && udcBUMPCondition4.Text != "")
                strSqlString.AppendFormat("   AND MAT.MAT_GRP_03 {0} " + "\n", udcBUMPCondition4.SelectedValueToQueryString);

            if (udcBUMPCondition5.Text != "ALL" && udcBUMPCondition5.Text != "")
                strSqlString.AppendFormat("   AND MAT.MAT_GRP_04 {0} " + "\n", udcBUMPCondition5.SelectedValueToQueryString);

            if (udcBUMPCondition6.Text != "ALL" && udcBUMPCondition6.Text != "")
                strSqlString.AppendFormat("   AND MAT.MAT_GRP_05 {0} " + "\n", udcBUMPCondition6.SelectedValueToQueryString);

            if (udcBUMPCondition7.Text != "ALL" && udcBUMPCondition7.Text != "")
                strSqlString.AppendFormat("   AND MAT.MAT_GRP_06 {0} " + "\n", udcBUMPCondition7.SelectedValueToQueryString);

            if (udcBUMPCondition8.Text != "ALL" && udcBUMPCondition8.Text != "")
                strSqlString.AppendFormat("   AND MAT.MAT_GRP_07 {0} " + "\n", udcBUMPCondition8.SelectedValueToQueryString);

            if (udcBUMPCondition9.Text != "ALL" && udcBUMPCondition9.Text != "")
                strSqlString.AppendFormat("   AND MAT.MAT_CMF_01 {0} " + "\n", udcBUMPCondition9.SelectedValueToQueryString);

            if (udcBUMPCondition10.Text != "ALL" && udcBUMPCondition10.Text != "")
                strSqlString.AppendFormat("   AND MAT.MAT_CMF_02 {0} " + "\n", udcBUMPCondition10.SelectedValueToQueryString);

            if (udcBUMPCondition11.Text != "ALL" && udcBUMPCondition11.Text != "")
                strSqlString.AppendFormat("   AND MAT.MAT_CMF_03 {0} " + "\n", udcBUMPCondition11.SelectedValueToQueryString);

            if (udcBUMPCondition12.Text != "ALL" && udcBUMPCondition12.Text != "")
                strSqlString.AppendFormat("   AND MAT.MAT_CMF_04 {0} " + "\n", udcBUMPCondition12.SelectedValueToQueryString);

            #endregion

            strSqlString.Append(" ORDER BY " + QueryCond2 + "\n"); 
            
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

                //그루핑 순서 1순위만 SubTotal을 사용하기 위해서 첫번째 값을 가져옴
                int sub = ((udcTableForm)(this.btnSort.BindingForm)).GetCheckedValue(2);
                int nGroupCount = ((udcTableForm)(this.btnSort.BindingForm)).GetSelectedCount();

                int[] rowType = spdData.RPT_DataBindingWithSubTotal(dt, 0, sub+1, 18, null, null, btnSort);                
                //데이타테이블, 토탈 시작할 셀넘버, 시작 부터 서브토탈 몇개 쓸건지, 첫셀부터 몇번째 셀까지 TOTAL 적용안함
                //spdData.Sheets[0].FrozenColumnCount = 3;
                //spdData.RPT_AutoFit(false);
                //spdData.DataSource = dt;
                //Total부분 셀머지
                spdData.RPT_FillDataSelectiveCells("Total", 0, 18, 0, 1, true, Align.Center, VerticalAlign.Center);

                spdData.RPT_AutoFit(false);

                spdData.RPT_SetAvgSubTotalAndGrandTotal(1, 20, nGroupCount, false);

                SetPerVertical(22, 40); // 시작컬럼, 종료컬럼

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

        public void SetPerVertical(int nStartColPos, int nEndColPos)
        {         
            double divide = 0;
            double totalDivide = 0;
            double subDivide = 0;

            double[] sumArray = new double[nEndColPos - nStartColPos + 1];
            double[] subSumArray = new double[nEndColPos - nStartColPos + 1];
            double[] totalSumArray = new double[nEndColPos - nStartColPos + 1];

            int count = ((udcTableForm)(this.btnSort.BindingForm)).GetSelectedCount();

            
                        
            for (int i = 1; i < spdData.ActiveSheet.Rows.Count; i++)
            {
                int x = 0;

                Color color = spdData.ActiveSheet.Cells[1, nStartColPos].BackColor;

                if (spdData.ActiveSheet.Cells[i, nStartColPos].BackColor == color)
                {
                    for (int y = nStartColPos; y <= nEndColPos; y++)
                    {
                        if (Convert.ToDouble(spdData.ActiveSheet.Cells[i, y].Value) > 0)
                        {
                            sumArray[x] += 1;
                        }

                        x += 1;
                    }

                    divide += 1;
                }
                else
                {
                    if (divide != 0)
                    {
                        for (int y = nStartColPos; y <= nEndColPos; y++)
                        {
                            spdData.ActiveSheet.Cells[i, y].Value = (divide - sumArray[x]) / divide * 100;

                            subSumArray[x] += sumArray[x];
                            totalSumArray[x] += sumArray[x];

                            sumArray[x] = 0;

                            x += 1;
                        }

                        if (count > 2) // Group 항목에서 체크된게 2개 이상인것(서브토탈이 2 Depth 이상인것)
                        {
                            x = 0;

                            subDivide += divide;

                            if (spdData.ActiveSheet.Cells[i, nStartColPos].BackColor == spdData.ActiveSheet.Cells[i + 1, nStartColPos].BackColor)
                            {
                                for (int y = nStartColPos; y <= nEndColPos; y++)
                                {
                                    spdData.ActiveSheet.Cells[i + 1, y].Value = (subDivide - subSumArray[x]) / subDivide * 100;

                                    subSumArray[x] = 0;

                                    x += 1;
                                }

                                subDivide = 0;
                            }
                            
                        }
                    }

                    totalDivide += divide;
                    divide = 0;
                }                                 
            }

            // GrandTotal 구함.   
            int xx = 0;
            
            for (int y = nStartColPos; y <= nEndColPos; y++)
            {
                spdData.ActiveSheet.Cells[0, y].Value = (totalDivide - totalSumArray[xx]) / totalDivide * 100;

                xx += 1;
            }
            

            return;
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
            //cdvLotType.sFactory = cdvFactory.txtValue;
        }
                
        #endregion
    }       
}
