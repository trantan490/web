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

namespace Hana.PQC
{
    public partial class PQC030303 : Miracom.SmartWeb.UI.Controls.udcCUSReport002
    {        
        /// <summary>
        /// 클  래  스: PQC030303<br/>
        /// 클래스요약: 공정별 품질 인력 예측<br/>
        /// 작  성  자: 하나마이크론 임종우<br/>
        /// 최초작성일: 2015-01-14<br/>
        /// 상세  설명: 공정별 품질 인력 예측(최재주D)<br/>
        /// 변경  내용: <br/>         
        /// 2015-01-20-임종우 : 월, 주 산출식 변경 (최재주D 요청)
        /// </summary>
        public PQC030303()
        {
            InitializeComponent();
            cdvFromToDate.DaySelector.SelectedValue = "MONTH";
            cdvFromToDate.AutoBindingUserSetting(DateTime.Now.ToString("yyyy-MM-dd"), DateTime.Now.ToString("yyyy-MM-dd"));
            cdvFromToDate.ToYearMonth.Visible = false;
            cdvFromToDate.ToYear.Visible = false;
            cdvFromToDate.ToWeek.Visible = false;
            cdvFromToDate.FromDate.Visible = false;
            cdvFromToDate.ToDate.Visible = false;

            SortInit();
            GridColumnInit();
            this.SetFactory(GlobalVariable.gsAssyDefaultFactory);
            this.cdvFactory.sFactory = GlobalVariable.gsAssyDefaultFactory;
            cdvFactory.Text = GlobalVariable.gsAssyDefaultFactory;
        }

        #region 초기화 및 유효성 검사
        /// <summary 1. 유효성 검사>
        /// <remarks></remarks>
        /// </summary>
        /// <returns></returns>
        private Boolean CheckField()
        {            
            if (cdvFromToDate.DaySelector.SelectedValue.ToString() == "DAY")
            {
                CmnFunction.ShowMsgBox(RptMessages.GetMessage("STD045", GlobalVariable.gcLanguage));
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
            
            try
            {
                if (cdvFromToDate.DaySelector.SelectedValue.ToString() == "MONTH")
                {
                    spdData.RPT_AddBasicColumn("Operation", 0, 0, Visibles.True, Frozen.True, Align.Center, Merge.True, Formatter.String, 70);
                    spdData.RPT_AddBasicColumn("Production plan (month)", 0, 1, Visibles.True, Frozen.True, Align.Right, Merge.True, Formatter.Number, 100);
                    spdData.RPT_AddBasicColumn("Expected number of inspections (month)", 0, 2, Visibles.True, Frozen.True, Align.Right, Merge.True, Formatter.Number, 100);
                    spdData.RPT_AddBasicColumn("Inspection required time (month)", 0, 3, Visibles.True, Frozen.True, Align.Right, Merge.True, Formatter.Number, 100);
                    spdData.RPT_AddBasicColumn("Inspection time (week)", 0, 4, Visibles.True, Frozen.True, Align.Right, Merge.True, Formatter.Number, 100);
                    spdData.RPT_AddBasicColumn("Necessary manpower(days)", 0, 5, Visibles.True, Frozen.True, Align.Right, Merge.True, Formatter.Double2, 80);
                }
                else
                {
                    spdData.RPT_AddBasicColumn("Operation", 0, 0, Visibles.True, Frozen.True, Align.Center, Merge.True, Formatter.String, 70);
                    spdData.RPT_AddBasicColumn("Production plan (week)", 0, 1, Visibles.True, Frozen.True, Align.Right, Merge.True, Formatter.Number, 100);
                    spdData.RPT_AddBasicColumn("Estimated number of inspections", 0, 2, Visibles.True, Frozen.True, Align.Right, Merge.True, Formatter.Number, 100);
                    spdData.RPT_AddBasicColumn("Inspection required time (week)", 0, 3, Visibles.True, Frozen.True, Align.Right, Merge.True, Formatter.Number, 100);
                    spdData.RPT_AddBasicColumn("Inspection time (week)", 0, 4, Visibles.True, Frozen.True, Align.Right, Merge.True, Formatter.Number, 100);
                    spdData.RPT_AddBasicColumn("Necessary manpower(days)", 0, 5, Visibles.True, Frozen.True, Align.Right, Merge.True, Formatter.Double2, 80);
                }

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
            //((udcTableForm)(this.btnSort.BindingForm)).AddRow("TYPE", "CUST_TYPE", "CUST_TYPE", "DECODE(CUST_TYPE, 'SEC', 1, 'Hynix', 2, 'Fabless', 3, 4)", "CUST_TYPE", true);
            //((udcTableForm)(this.btnSort.BindingForm)).AddRow("CUSTOMER", "MAT_GRP_1", "NVL((SELECT DATA_1 FROM MGCMTBLDAT WHERE TABLE_NAME = 'H_CUSTOMER' AND KEY_1 = MAT_GRP_1 AND ROWNUM=1), '-') AS CUSTOMER", "MAT_GRP_1", true);
            //((udcTableForm)(this.btnSort.BindingForm)).AddRow("MAJOR CODE", "MAT_GRP_9", "MAT_GRP_9 AS MAJOR_CODE", "MAT_GRP_9", true);
            //((udcTableForm)(this.btnSort.BindingForm)).AddRow("FAMILY", "MAT_GRP_2", "MAT_GRP_2 AS FAMILY", "MAT_GRP_2", true);
            //((udcTableForm)(this.btnSort.BindingForm)).AddRow("PACKAGE", "MAT_GRP_10", "MAT_GRP_10 AS PACKAGE", "MAT_GRP_10", false);
            //((udcTableForm)(this.btnSort.BindingForm)).AddRow("TYPE1", "MAT_GRP_4", "MAT_GRP_4 AS TYPE1", "MAT_GRP_4", false);
            //((udcTableForm)(this.btnSort.BindingForm)).AddRow("TYPE2", "MAT_GRP_5", "MAT_GRP_5 AS TYPE2", "MAT_GRP_5", false);
            //((udcTableForm)(this.btnSort.BindingForm)).AddRow("LD COUNT", "MAT_GRP_6", "MAT_GRP_6 AS LD_COUNT", "MAT_GRP_6", false);
            //((udcTableForm)(this.btnSort.BindingForm)).AddRow("PKG CODE", "MAT_CMF_11", "MAT_CMF_11 AS PKG_CODE", "MAT_CMF_11", false);
            //((udcTableForm)(this.btnSort.BindingForm)).AddRow("DENSITY", "MAT_GRP_7", "MAT_GRP_7 AS DENSITY", "MAT_GRP_7", false);
            //((udcTableForm)(this.btnSort.BindingForm)).AddRow("GENERATION", "MAT_GRP_8", "MAT_GRP_8 AS GENERATION", "MAT_GRP_8", false);
            //((udcTableForm)(this.btnSort.BindingForm)).AddRow("PIN TYPE", "MAT_CMF_10", "MAT_CMF_10 AS PIN_TYPE", "MAT_CMF_10", false);
            //((udcTableForm)(this.btnSort.BindingForm)).AddRow("PRODUCT", "MAT_ID", "MAT_ID AS PRODUCT", "MAT_ID", false);
            //((udcTableForm)(this.btnSort.BindingForm)).AddRow("SAP CODE", "VENDOR_ID", "VENDOR_ID AS SAP_CODE", "VENDOR_ID", false);            
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
            
            udcTableForm tableForm = (udcTableForm)btnSort.BindingForm;
            QueryCond1 = tableForm.SelectedValueToQueryContainNull;
            QueryCond2 = tableForm.SelectedValue2ToQueryContainNull;

            if (cdvFromToDate.DaySelector.SelectedValue.ToString() == "MONTH")
            {
                strSqlString.Append("SELECT OPER_GRP" + "\n");
                strSqlString.Append("     , ROUND(PLAN_QTY, 0) AS PLAN_QTY" + "\n");
                strSqlString.Append("     , ROUND(QC_CNT, 0) AS QC_CNT" + "\n");
                strSqlString.Append("     , ROUND(AVG_TIME, 0) AS AVG_TIME" + "\n");
                strSqlString.Append("     , ROUND((QC_CNT * AVG_TIME) / 4, 0) AS QC_TIME" + "\n");
                strSqlString.Append("     , ROUND((((QC_CNT * AVG_TIME) / 4) / (27000 * 3)) / 7, 2) AS NEED_CNT" + "\n");
                strSqlString.Append("  FROM (" + "\n");
                strSqlString.Append("        SELECT A.OPER_GRP" + "\n");
                strSqlString.Append("             , B.PLAN_QTY" + "\n");
                strSqlString.Append("             , CASE WHEN A.OPER_GRP = 'DP' THEN -1286.759 + ((7.1115 * 0.00001) * B.PLAN_QTY)" + "\n");
                strSqlString.Append("                    WHEN A.OPER_GRP = '2ND GATE' THEN (11938.844 - (0.0001374 * B.PLAN_QTY)) * 7" + "\n");
                strSqlString.Append("                    WHEN A.OPER_GRP = 'DA' THEN 2154.653 + ((2.8417 * 0.000001) * B.PLAN_QTY)" + "\n");
                strSqlString.Append("                    WHEN A.OPER_GRP = 'WB' THEN 2050.841 + ((7.5847 * 0.00001) * B.PLAN_QTY)" + "\n");
                strSqlString.Append("                    WHEN A.OPER_GRP = '3RD GATE' THEN (5804.6568 + (0.0002687 * B.PLAN_QTY)) * 3" + "\n");
                strSqlString.Append("                    WHEN A.OPER_GRP = 'MD' THEN 858.91476 + ((1.168 * 0.00001) * B.PLAN_QTY)" + "\n");
                strSqlString.Append("                    WHEN A.OPER_GRP = 'PVI GATE' THEN (1428.0901 + (0.0003968 * B.PLAN_QTY)) * 0.7" + "\n");
                strSqlString.Append("                    ELSE 2512.8459 + (0.000011 * B.PLAN_QTY)" + "\n");
                strSqlString.Append("               END QC_CNT" + "\n");
                strSqlString.Append("             , C.AVG_TIME" + "\n");
                strSqlString.Append("          FROM (" + "\n");
                strSqlString.Append("                SELECT 'DP' AS OPER_GRP FROM DUAL UNION" + "\n");
                strSqlString.Append("                SELECT '2ND GATE' AS OPER_GRP FROM DUAL UNION" + "\n");
                strSqlString.Append("                SELECT 'DA' AS OPER_GRP FROM DUAL UNION" + "\n");
                strSqlString.Append("                SELECT 'WB' AS OPER_GRP FROM DUAL UNION" + "\n");
                strSqlString.Append("                SELECT '3RD GATE' AS OPER_GRP FROM DUAL UNION" + "\n");
                strSqlString.Append("                SELECT 'MD' AS OPER_GRP FROM DUAL UNION" + "\n");
                strSqlString.Append("                SELECT 'FINISH' AS OPER_GRP FROM DUAL UNION" + "\n");
                strSqlString.Append("                SELECT 'PVI GATE' AS OPER_GRP FROM DUAL" + "\n");
                strSqlString.Append("               ) A" + "\n");
                strSqlString.Append("             , (" + "\n");
                strSqlString.Append("                SELECT SUM(CASE WHEN B.MAT_GRP_3 IN ('COB', 'BGN') THEN TO_NUMBER(DECODE(RESV_FIELD1, ' ', 0, RESV_FIELD1)) / B.NET_DIE ELSE TO_NUMBER(DECODE(RESV_FIELD1, ' ', 0, RESV_FIELD1)) END) AS PLAN_QTY" + "\n");
                strSqlString.Append("                  FROM CWIPPLNMON A" + "\n");
                strSqlString.Append("                     , VWIPMATDEF B" + "\n");
                strSqlString.Append("                 WHERE 1=1" + "\n");
                strSqlString.Append("                   AND A.FACTORY = B.FACTORY" + "\n");
                strSqlString.Append("                   AND A.MAT_ID = B.MAT_ID" + "\n");
                strSqlString.Append("                   AND A.FACTORY = '" + cdvFactory.Text + "'" + "\n");
                strSqlString.Append("                   AND A.PLAN_MONTH = '" + cdvFromToDate.FromYearMonth.Value.ToString("yyyyMM") + "'" + "\n");
                strSqlString.Append("                   AND B.DELETE_FLAG = ' '" + "\n");
                strSqlString.Append("               ) B" + "\n");
                strSqlString.Append("             , (" + "\n");
                strSqlString.Append("                SELECT OPER_GRP, AVG(AVG_TIME) AS AVG_TIME" + "\n");
                strSqlString.Append("                  FROM (" + "\n");
                strSqlString.Append("                        SELECT KEY_1, KEY_3, KEY_4" + "\n");
                strSqlString.Append("                             , CASE WHEN KEY_3 = 'A0300' THEN '2ND GATE'" + "\n");
                strSqlString.Append("                                    WHEN KEY_1 = 'DP' THEN 'DP'" + "\n");
                strSqlString.Append("                                    WHEN KEY_3 LIKE 'A04%' THEN 'DA'" + "\n");
                strSqlString.Append("                                    WHEN KEY_3 LIKE 'A06%' THEN 'WB'" + "\n");
                strSqlString.Append("                                    WHEN KEY_3 LIKE 'A08%' THEN '3RD GATE'" + "\n");
                strSqlString.Append("                                    WHEN KEY_3 = 'A1000' THEN 'MD'" + "\n");
                strSqlString.Append("                                    WHEN KEY_3 IN ('A2050', 'A2100') THEN 'PVI GATE'" + "\n");
                strSqlString.Append("                                    ELSE 'FINISH'" + "\n");
                strSqlString.Append("                               END AS OPER_GRP" + "\n");
                strSqlString.Append("                             , (KEY_5+KEY_6+KEY_7+KEY_8+KEY_9+KEY_10+DATA_1+DATA_2+DATA_3+DATA_4+DATA_5+DATA_6+DATA_7) / 13 AS AVG_TIME" + "\n");
                strSqlString.Append("                          FROM MGCMTBLDAT" + "\n");
                strSqlString.Append("                         WHERE 1=1" + "\n");
                strSqlString.Append("                           AND FACTORY = '" + cdvFactory.Text + "'" + "\n");
                strSqlString.Append("                           AND TABLE_NAME = 'H_RPT_HUMAN_QC'" + "\n");
                strSqlString.Append("                       )" + "\n");
                strSqlString.Append("                 GROUP BY OPER_GRP" + "\n");
                strSqlString.Append("               ) C" + "\n");
                strSqlString.Append("         WHERE 1=1" + "\n");
                strSqlString.Append("           AND A.OPER_GRP = C.OPER_GRP(+)" + "\n");
                strSqlString.Append("       )" + "\n");
                strSqlString.Append(" ORDER BY DECODE(OPER_GRP, 'DP', 1, '2ND GATE', 2, 'DA', 3, 'WB', 4, '3RD GATE', 5, 'MD', 6, 'PVI GATE', 7, 8)" + "\n");
            }
            else
            {
                strSqlString.Append("SELECT OPER_GRP" + "\n");
                strSqlString.Append("     , ROUND(PLAN_QTY, 0) AS PLAN_QTY" + "\n");
                strSqlString.Append("     , ROUND(QC_CNT, 0) AS QC_CNT" + "\n");
                strSqlString.Append("     , ROUND(AVG_TIME, 0) AS AVG_TIME" + "\n");
                strSqlString.Append("     , ROUND(QC_CNT * AVG_TIME, 0) AS QC_TIME" + "\n");
                strSqlString.Append("     , ROUND(((QC_CNT * AVG_TIME) / (27000 * 3)) / 7, 2) AS NEED_CNT" + "\n");
                strSqlString.Append("  FROM (" + "\n");
                strSqlString.Append("        SELECT A.OPER_GRP" + "\n");
                strSqlString.Append("             , B.PLAN_QTY" + "\n");
                strSqlString.Append("             , CASE WHEN A.OPER_GRP = 'DP' THEN 646.25694 - ((2.8322 * 0.00001) * B.PLAN_QTY)" + "\n");
                strSqlString.Append("                    WHEN A.OPER_GRP = '2ND GATE' THEN (1168.7215 + ((5.2145 * 0.00001) * B.PLAN_QTY)) * 7" + "\n");
                strSqlString.Append("                    WHEN A.OPER_GRP = 'DA' THEN 358.37747 + ((1.6746 * 0.00001) * B.PLAN_QTY)" + "\n");
                strSqlString.Append("                    WHEN A.OPER_GRP = 'WB' THEN 1244.4207 - ((2.9529 * 0.000001) * B.PLAN_QTY)" + "\n");
                strSqlString.Append("                    WHEN A.OPER_GRP = '3RD GATE' THEN (2269.9014 + (0.000144 * B.PLAN_QTY)) * 3" + "\n");
                strSqlString.Append("                    WHEN A.OPER_GRP = 'MD' THEN 216.0437 + ((7.8229 * 0.000001) * B.PLAN_QTY)" + "\n");
                strSqlString.Append("                    WHEN A.OPER_GRP = 'PVI GATE' THEN (2089.3495 + (0.0002051 * B.PLAN_QTY)) * 0.7" + "\n");
                strSqlString.Append("                    ELSE 408.52379 + (0.0000287 * B.PLAN_QTY)" + "\n");
                strSqlString.Append("               END QC_CNT" + "\n");
                strSqlString.Append("             , C.AVG_TIME" + "\n");
                strSqlString.Append("          FROM (" + "\n");
                strSqlString.Append("                SELECT 'DP' AS OPER_GRP FROM DUAL UNION" + "\n");
                strSqlString.Append("                SELECT '2ND GATE' AS OPER_GRP FROM DUAL UNION" + "\n");
                strSqlString.Append("                SELECT 'DA' AS OPER_GRP FROM DUAL UNION" + "\n");
                strSqlString.Append("                SELECT 'WB' AS OPER_GRP FROM DUAL UNION" + "\n");
                strSqlString.Append("                SELECT '3RD GATE' AS OPER_GRP FROM DUAL UNION" + "\n");
                strSqlString.Append("                SELECT 'MD' AS OPER_GRP FROM DUAL UNION" + "\n");
                strSqlString.Append("                SELECT 'FINISH' AS OPER_GRP FROM DUAL UNION" + "\n");
                strSqlString.Append("                SELECT 'PVI GATE' AS OPER_GRP FROM DUAL" + "\n");
                strSqlString.Append("               ) A" + "\n");
                strSqlString.Append("             , (" + "\n");
                strSqlString.Append("                SELECT SUM(CASE WHEN B.MAT_GRP_3 IN ('COB', 'BGN') THEN WW_QTY / B.NET_DIE ELSE WW_QTY END) AS PLAN_QTY" + "\n");
                strSqlString.Append("                  FROM RWIPPLNWEK A" + "\n");
                strSqlString.Append("                     , VWIPMATDEF B" + "\n");
                strSqlString.Append("                 WHERE 1=1" + "\n");
                strSqlString.Append("                   AND A.FACTORY = B.FACTORY" + "\n");
                strSqlString.Append("                   AND A.MAT_ID = B.MAT_ID" + "\n");
                strSqlString.Append("                   AND A.FACTORY = '" + cdvFactory.Text + "'" + "\n");
                strSqlString.Append("                   AND A.PLAN_WEEK = '" + cdvFromToDate.HmFromWeek + "'" + "\n");
                strSqlString.Append("                   AND A.GUBUN = '3'" + "\n");
                strSqlString.Append("                   AND B.DELETE_FLAG = ' '" + "\n");
                strSqlString.Append("               ) B" + "\n");
                strSqlString.Append("             , (" + "\n");
                strSqlString.Append("                SELECT OPER_GRP, AVG(AVG_TIME) AS AVG_TIME" + "\n");
                strSqlString.Append("                  FROM (" + "\n");
                strSqlString.Append("                        SELECT KEY_1, KEY_3, KEY_4" + "\n");
                strSqlString.Append("                             , CASE WHEN KEY_3 = 'A0300' THEN '2ND GATE'" + "\n");
                strSqlString.Append("                                    WHEN KEY_1 = 'DP' THEN 'DP'" + "\n");
                strSqlString.Append("                                    WHEN KEY_3 LIKE 'A04%' THEN 'DA'" + "\n");
                strSqlString.Append("                                    WHEN KEY_3 LIKE 'A06%' THEN 'WB'" + "\n");
                strSqlString.Append("                                    WHEN KEY_3 LIKE 'A08%' THEN '3RD GATE'" + "\n");
                strSqlString.Append("                                    WHEN KEY_3 = 'A1000' THEN 'MD'" + "\n");
                strSqlString.Append("                                    WHEN KEY_3 IN ('A2050', 'A2100') THEN 'PVI GATE'" + "\n");
                strSqlString.Append("                                    ELSE 'FINISH'" + "\n");
                strSqlString.Append("                               END AS OPER_GRP" + "\n");
                strSqlString.Append("                             , (KEY_5+KEY_6+KEY_7+KEY_8+KEY_9+KEY_10+DATA_1+DATA_2+DATA_3+DATA_4+DATA_5+DATA_6+DATA_7) / 13 AS AVG_TIME" + "\n");
                strSqlString.Append("                          FROM MGCMTBLDAT" + "\n");
                strSqlString.Append("                         WHERE 1=1" + "\n");
                strSqlString.Append("                           AND FACTORY = '" + cdvFactory.Text + "'" + "\n");
                strSqlString.Append("                           AND TABLE_NAME = 'H_RPT_HUMAN_QC'" + "\n");
                strSqlString.Append("                       )" + "\n");
                strSqlString.Append("                 GROUP BY OPER_GRP" + "\n");
                strSqlString.Append("               ) C" + "\n");
                strSqlString.Append("         WHERE 1=1" + "\n");
                strSqlString.Append("           AND A.OPER_GRP = C.OPER_GRP(+)" + "\n");
                strSqlString.Append("       )" + "\n");
                strSqlString.Append(" ORDER BY DECODE(OPER_GRP, 'DP', 1, '2ND GATE', 2, 'DA', 3, 'WB', 4, '3RD GATE', 5, 'MD', 6, 'PVI GATE', 7, 8)" + "\n");
            }

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
                //int sub = ((udcTableForm)(this.btnSort.BindingForm)).GetCheckedValue(2);

                int[] rowType = spdData.RPT_DataBindingWithSubTotal(dt, 0, 0, 1, null, null, btnSort);
                //데이타테이블, 토탈 시작할 셀넘버, 시작 부터 서브토탈 몇개 쓸건지, 첫셀부터 몇번째 셀까지 TOTAL 적용안함

                //Total부분 셀머지
                spdData.RPT_FillDataSelectiveCells("Total", 0, 1, 0, 1, true, Align.Center, VerticalAlign.Center);

                spdData.RPT_AutoFit(false);

                //spdData.DataSource = dt;
                
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
                //ExcelHelper.Instance.subMakeExcel(spdData, this.lblTitle.Text, null, null, true);
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
            //this.SetFactory(cdvFactory.txtValue);
            //cdvLotType.sFactory = cdvFactory.txtValue;
        }

        #endregion



    }
}
