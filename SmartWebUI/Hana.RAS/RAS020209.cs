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

namespace Hana.RAS
{
    public partial class RAS020209 : Miracom.SmartWeb.UI.Controls.udcCUSReport002
    {
        string[] selectDate = null;

        /// <summary>
        /// 클  래  스: RAS020209<br/>
        /// 클래스요약: UPH Trend<br/>
        /// 작  성  자: 하나마이크론 임종우<br/>
        /// 최초작성일: 2015-10-01<br/>
        /// 상세  설명: UPH Trend(민재훈C 요청)<br/>
        /// 변경  내용: <br/>
        /// 2015-11-04-임종우 : PRDUCT 평균 조회 추가(민재훈C 요청)
        /// </summary>
        public RAS020209()
        {
            InitializeComponent();

            cdvFromToDate.DaySelector.SelectedValue = "MONTH";
            cdvFromToDate.DaySelector.Visible = false;
            cdvFromToDate.AutoBindingUserSetting(DateTime.Now.ToString("yyyy-MM-dd"), DateTime.Now.ToString("yyyy-MM-dd"));
            
            SortInit();
            GridColumnInit();
        }
        #region " Constant Definition "

        #endregion

        #region " Function Definition "

        /// <summary 1. 유효성 검사>
        /// <remarks></remarks>
        /// </summary>
        /// <returns></returns>
        private Boolean CheckField()
        {
            if (cdvFactory.Text.TrimEnd() == "")
            {
                CmnFunction.ShowMsgBox(RptMessages.GetMessage("STD001", GlobalVariable.gcLanguage));
                return false;
            }

            if (cdvStep.Text.TrimEnd() == "" || cdvStep.Text.TrimEnd() == "ALL")
            {
                CmnFunction.ShowMsgBox(RptMessages.GetMessage("STD005", GlobalVariable.gcLanguage));
                return false;
            }

            if (cdvStep.SelectCount > 10)
            {
                CmnFunction.ShowMsgBox(RptMessages.GetMessage("STD069", GlobalVariable.gcLanguage));
                return false;
            }

            if (cdvFromToDate.SubtractBetweenFromToDate + 1 > 12)
            {
                CmnFunction.ShowMsgBox(RptMessages.GetMessage("STD070", GlobalVariable.gcLanguage));
                return false;
            }

            return true;
        }

        /// <summary>
        /// 2. 헤더 생성
        /// </summary>
        private void GridColumnInit()
        {
            //spdData.RPT_ColumnInit();

            selectDate = cdvFromToDate.getSelectDate();

            try
            {                
                spdData.RPT_ColumnInit();
                spdData.RPT_AddBasicColumn("OPER", 0, 0, Visibles.True, Frozen.False, Align.Center, Merge.True, Formatter.String, 70);
                spdData.RPT_AddBasicColumn("OPER DESC", 0, 1, Visibles.True, Frozen.False, Align.Left, Merge.True, Formatter.String, 100);
                spdData.RPT_AddBasicColumn("PIN TYPE", 0, 2, Visibles.True, Frozen.False, Align.Left, Merge.True, Formatter.String, 150);
                spdData.RPT_AddBasicColumn("MAT ID", 0, 3, Visibles.True, Frozen.False, Align.Left, Merge.True, Formatter.String, 120);
                spdData.RPT_AddBasicColumn("MODEL", 0, 4, Visibles.True, Frozen.False, Align.Left, Merge.True, Formatter.String, 70);
                spdData.RPT_AddBasicColumn("UPH GROUP", 0, 5, Visibles.True, Frozen.False, Align.Left, Merge.True, Formatter.String, 50);

                if (ckbProduct.Checked == false)
                {
                    spdData.RPT_AddBasicColumn("PRODUCT GROUP", 0, 6, Visibles.True, Frozen.False, Align.Left, Merge.True, Formatter.String, 100);
                }
                else
                {
                    spdData.RPT_AddBasicColumn("PRODUCT GROUP", 0, 6, Visibles.False, Frozen.False, Align.Left, Merge.True, Formatter.String, 100);
                }

                if (cdvFactory.Text != "")
                {
                    if (cboType.Text == "ALL")
                    {
                        spdData.RPT_AddBasicColumn("BM", 0, 7, Visibles.True, Frozen.False, Align.Center, Merge.False, Formatter.String, 100);
                        spdData.RPT_AddBasicColumn("Cycle Time", 1, 7, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.String, 60);
                        spdData.RPT_AddBasicColumn("UPH", 1, 8, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.String, 60);
                        spdData.RPT_AddBasicColumn("장/hr", 1, 9, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.String, 60);
                        spdData.RPT_MerageHeaderColumnSpan(0, 7, 3);

                        for (int i = 0; i < selectDate.Length; i++)
                        {
                            spdData.RPT_AddBasicColumn(selectDate[i].ToString(), 0, spdData.ActiveSheet.ColumnHeader.Columns.Count, Visibles.True, Frozen.False, Align.Center, Merge.False, Formatter.String, 60);
                            spdData.RPT_AddBasicColumn("Cycle Time", 1, spdData.ActiveSheet.ColumnHeader.Columns.Count - 1, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.String, 60);
                            spdData.RPT_AddBasicColumn("UPH", 1, spdData.ActiveSheet.ColumnHeader.Columns.Count, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.String, 60);
                            spdData.RPT_AddBasicColumn("장/hr", 1, spdData.ActiveSheet.ColumnHeader.Columns.Count, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.String, 60);
                            spdData.RPT_MerageHeaderColumnSpan(0, spdData.ActiveSheet.ColumnHeader.Columns.Count - 3, 3);
                        }

                        spdData.RPT_AddBasicColumn("Change rate", 0, spdData.ActiveSheet.ColumnHeader.Columns.Count, Visibles.True, Frozen.False, Align.Center, Merge.False, Formatter.String, 60);
                        spdData.RPT_AddBasicColumn("Cycle Time", 1, spdData.ActiveSheet.ColumnHeader.Columns.Count - 1, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.String, 60);
                        spdData.RPT_AddBasicColumn("UPH", 1, spdData.ActiveSheet.ColumnHeader.Columns.Count, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.String, 60);
                        spdData.RPT_AddBasicColumn("장/hr", 1, spdData.ActiveSheet.ColumnHeader.Columns.Count, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.String, 60);
                        spdData.RPT_MerageHeaderColumnSpan(0, spdData.ActiveSheet.ColumnHeader.Columns.Count - 3, 3);
                    }
                    else
                    {
                        spdData.RPT_AddBasicColumn("BM", 0, 7, Visibles.True, Frozen.False, Align.Center, Merge.False, Formatter.String, 100);
                        spdData.RPT_AddBasicColumn(cboType.Text, 1, 7, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.String, 60);                        

                        for (int i = 0; i < selectDate.Length; i++)
                        {
                            spdData.RPT_AddBasicColumn(selectDate[i].ToString(), 0, spdData.ActiveSheet.ColumnHeader.Columns.Count, Visibles.True, Frozen.False, Align.Center, Merge.False, Formatter.String, 60);
                            spdData.RPT_AddBasicColumn(cboType.Text, 1, spdData.ActiveSheet.ColumnHeader.Columns.Count - 1, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.String, 60);                            
                        }

                        spdData.RPT_AddBasicColumn("Change rate", 0, spdData.ActiveSheet.ColumnHeader.Columns.Count, Visibles.True, Frozen.False, Align.Center, Merge.False, Formatter.String, 60);
                        spdData.RPT_AddBasicColumn(cboType.Text, 1, spdData.ActiveSheet.ColumnHeader.Columns.Count - 1, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.String, 60);                        
                    }
                }

                spdData.RPT_MerageHeaderRowSpan(0, 0, 2);
                spdData.RPT_MerageHeaderRowSpan(0, 1, 2);
                spdData.RPT_MerageHeaderRowSpan(0, 2, 2);
                spdData.RPT_MerageHeaderRowSpan(0, 3, 2);
                spdData.RPT_MerageHeaderRowSpan(0, 4, 2);
                spdData.RPT_MerageHeaderRowSpan(0, 5, 2);
                spdData.RPT_MerageHeaderRowSpan(0, 6, 2);

                spdData.RPT_ColumnConfigFromTable_New(btnSort);
                //spdData.RPT_ColumnConfigFromTable(btnSort); //Group항목이 있을경우 반드시 선언해줄것.
                
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
            //((udcTableForm)(this.btnSort.BindingForm)).Clear();
            //((udcTableForm)(this.btnSort.BindingForm)).AddRow("Model", "NVL(RES.RES_MODEL,'-') AS MODEL", "RES.RES_MODEL", true);
            //((udcTableForm)(this.btnSort.BindingForm)).AddRow("Customer", "NVL((SELECT DATA_1 FROM MGCMTBLDAT WHERE FACTORY = MAT.FACTORY AND TABLE_NAME = 'H_CUSTOMER' AND ROWNUM=1 AND KEY_1 = MAT.MAT_GRP_1), '-') AS TEAM", "MAT.MAT_GRP_1", true);
            //((udcTableForm)(this.btnSort.BindingForm)).AddRow("Family", "MAT.MAT_GRP_2 AS Family", "MAT_GRP_2", false);
            //((udcTableForm)(this.btnSort.BindingForm)).AddRow("Package", "MAT.MAT_GRP_3 AS Package", "MAT_GRP_3", false);
            //((udcTableForm)(this.btnSort.BindingForm)).AddRow("Type1", "MAT.MAT_GRP_4 AS Type1", "MAT_GRP_4", false);
            //((udcTableForm)(this.btnSort.BindingForm)).AddRow("Type2", "MAT.MAT_GRP_5 AS Type2", "MAT_GRP_5", false);
            //((udcTableForm)(this.btnSort.BindingForm)).AddRow("LD Count", "MAT.MAT_GRP_6 AS LD_Count", "MAT_GRP_6", false);
            //((udcTableForm)(this.btnSort.BindingForm)).AddRow("Density", "MAT.MAT_GRP_7 AS Density", "MAT_GRP_7", false);
            //((udcTableForm)(this.btnSort.BindingForm)).AddRow("Generation", "MAT.MAT_GRP_8 AS Generation", "MAT_GRP_8", false);
            //((udcTableForm)(this.btnSort.BindingForm)).AddRow("Pin Type", "MAT.MAT_CMF_10 AS Pin_Type", "MAT.MAT_CMF_10", true);
            //((udcTableForm)(this.btnSort.BindingForm)).AddRow("Step", "RES.OPER AS Step", "RES.OPER", true);
            //((udcTableForm)(this.btnSort.BindingForm)).AddRow("Product", "RES.RES_STS_2 AS Product", "RES.RES_STS_2", true);
            //((udcTableForm)(this.btnSort.BindingForm)).AddRow("Cust Device", "MAT.MAT_CMF_7 AS Cust_Device", "MAT.MAT_CMF_7", false);
           
        }

        /// <summary>
        /// 4. 쿼리 생성
        /// </summary>
        /// <returns></returns>
        private string MakeSqlString()
        {
            StringBuilder strSqlString = new StringBuilder();

            string QueryCond1 = null;
            string QueryCond2 = null;            
            string sMonth = null;
            string eMonth = null;
            int iendCnt = 0;

            udcTableForm tableForm = (udcTableForm)btnSort.BindingForm;

            QueryCond1 = tableForm.SelectedValueToQueryContainNull;
            QueryCond2 = tableForm.SelectedValue2ToQueryContainNull;

            sMonth = cdvFromToDate.FromYearMonth.Value.ToString("yyyyMM");
            eMonth = cdvFromToDate.ToYearMonth.Value.ToString("yyyyMM");
            iendCnt = selectDate.Length - 1;
                        
            if (ckbProduct.Checked == true)
            {
                #region 1. Product 평균 조회
                strSqlString.Append("SELECT OPER, OPER_DESC, MAT_CMF_10, MAT_ID, RES_MODEL, UPEH_GRP, ' '" + "\n");

                if (cboType.Text == "ALL")
                {
                    strSqlString.Append("     , ROUND(AVG(CYCLE_TIME),0) AS CYCLE_TIME, ROUND(AVG(UPEH),0) AS UPEH, ROUND(AVG(WPH),1) AS WPH" + "\n");                    

                    for (int i = 0; i < selectDate.Length; i++)
                    {
                        strSqlString.Append("     , ROUND(AVG(CYCLE_TIME_" + i + "),0) AS CYCLE_TIME_" + i + ", ROUND(AVG(UPEH_" + i + "),0) AS UPEH_" + i + ", ROUND(AVG(WPH_" + i + "), 1) AS WPH_" + i + "\n");
                    }

                    strSqlString.Append("     , ROUND(AVG(TREND_CYCLE),0) AS TREND_CYCLE, ROUND(AVG(TREND_UPEH),0) AS TREND_UPEH, ROUND(AVG(TREND_WPH),1) AS TREND_WPH" + "\n");
                }
                else if (cboType.Text == "Cycle Time")
                {
                    strSqlString.Append("     , ROUND(AVG(CYCLE_TIME),0) AS CYCLE_TIME" + "\n");

                    for (int i = 0; i < selectDate.Length; i++)
                    {
                        strSqlString.Append("     , ROUND(AVG(CYCLE_TIME_" + i + "),0) AS CYCLE_TIME_" + i + "\n");
                    }

                    strSqlString.Append("     , ROUND(AVG(TREND_CYCLE),0) AS TREND_CYCLE" + "\n");
                }
                else if (cboType.Text == "UPH")
                {
                    strSqlString.Append("     , ROUND(AVG(UPEH),0) AS UPEH" + "\n");                    

                    for (int i = 0; i < selectDate.Length; i++)
                    {
                        strSqlString.Append("     , ROUND(AVG(UPEH_" + i + "),0) AS UPEH_" + i + "\n");
                    }

                    strSqlString.Append("     , ROUND(AVG(TREND_UPEH),0) AS TREND_UPEH" + "\n");
                }
                else
                {
                    strSqlString.Append("     , ROUND(AVG(WPH),1) AS WPH" + "\n");                    

                    for (int i = 0; i < selectDate.Length; i++)
                    {
                        strSqlString.Append("     , ROUND(AVG(WPH_" + i + "), 1) AS WPH_" + i + "\n");
                    }

                    strSqlString.Append("     , ROUND(AVG(TREND_WPH),1) AS TREND_WPH" + "\n");
                }

                strSqlString.Append("  FROM (" + "\n");
                strSqlString.Append("        SELECT A.OPER, D.OPER_DESC, C.MAT_CMF_10" + "\n");
                strSqlString.Append("             , CASE WHEN C.MAT_GRP_1 = 'HX' THEN C.MAT_CMF_10" + "\n");
                strSqlString.Append("                    WHEN C.MAT_ID LIKE 'SEK%' THEN 'SEK_________-___' || SUBSTR(C.MAT_ID, -3)" + "\n");
                strSqlString.Append("                    ELSE C.MAT_ID" + "\n");
                strSqlString.Append("               END MAT_ID" + "\n");
                strSqlString.Append("             , A.RES_MODEL, A.UPEH_GRP" + "\n");          
                strSqlString.Append("             , B.CYCLE_TIME, B.UPEH, ROUND(B.WPH, 1) AS WPH" + "\n");

                for (int i = 0; i < selectDate.Length; i++)
                {
                    strSqlString.Append("             , A.CYCLE_TIME_" + i + ", A.UPEH_" + i + ", ROUND(A.WPH_" + i + ", 1) AS WPH_" + i + "\n");
                }

                strSqlString.Append("             , CASE WHEN B.CYCLE_TIME IN (' ', '0') THEN 0" + "\n");
                strSqlString.Append("                    ELSE ROUND(A.CYCLE_TIME_" + iendCnt + " / B.CYCLE_TIME * 100, 0) " + "\n");
                strSqlString.Append("               END AS TREND_CYCLE" + "\n");
                strSqlString.Append("             , CASE WHEN B.UPEH = 0 THEN 0" + "\n");
                strSqlString.Append("                    ELSE ROUND(A.UPEH_" + iendCnt + " / B.UPEH * 100, 0) " + "\n");
                strSqlString.Append("               END AS TREND_UPEH" + "\n");
                strSqlString.Append("             , CASE WHEN B.WPH = 0 THEN 0" + "\n");
                strSqlString.Append("                    ELSE ROUND(A.WPH_" + iendCnt + " / B.WPH * 100, 0) " + "\n");
                strSqlString.Append("               END AS TREND_WPH" + "\n");    
                strSqlString.Append("          FROM (" + "\n");
                strSqlString.Append("                SELECT OPER, MAT_ID, RES_MODEL, UPEH_GRP" + "\n");
                strSqlString.Append("                     , MAX(PRODUCT_GROUP) KEEP(DENSE_RANK FIRST ORDER BY WORK_MONTH DESC) AS PRODUCT_GROUP" + "\n");

                for (int i = 0; i < selectDate.Length; i++)
                {
                    strSqlString.Append("                     , MAX(DECODE(WORK_MONTH, '" + selectDate[i].ToString() + "', CYCLE_TIME, 0)) AS CYCLE_TIME_" + i + "\n");
                    strSqlString.Append("                     , MAX(DECODE(WORK_MONTH, '" + selectDate[i].ToString() + "', UPEH, 0)) AS UPEH_" + i + "\n");
                    strSqlString.Append("                     , MAX(DECODE(WORK_MONTH, '" + selectDate[i].ToString() + "', WPH, 0)) AS WPH_" + i + "\n");
                }

                strSqlString.Append("                  FROM CRASUPHDEF_BOH" + "\n");
                strSqlString.Append("                 WHERE 1=1" + "\n");
                strSqlString.Append("                   AND FACTORY = '" + cdvFactory.Text + "'" + "\n");
                strSqlString.Append("                   AND WORK_MONTH BETWEEN '" + sMonth + "' AND '" + eMonth + "' " + "\n");
                strSqlString.Append("                 GROUP BY OPER, MAT_ID, RES_MODEL, UPEH_GRP" + "\n");
                strSqlString.Append("               ) A" + "\n");
                strSqlString.Append("             , (" + "\n");
                strSqlString.Append("                SELECT * " + "\n");
                strSqlString.Append("                  FROM CRASUPHDEF_BOH" + "\n");
                strSqlString.Append("                 WHERE 1=1 " + "\n");
                strSqlString.Append("                   AND FACTORY = '" + cdvFactory.Text + "'" + "\n");
                strSqlString.Append("                  AND BM_TYPE = 'Y' " + "\n");
                strSqlString.Append("               ) B" + "\n");
                strSqlString.Append("             , MWIPMATDEF C" + "\n");
                strSqlString.Append("             , MWIPOPRDEF D" + "\n");
                strSqlString.Append("         WHERE 1=1" + "\n");
                strSqlString.Append("           AND A.OPER = B.OPER(+)" + "\n");
                strSqlString.Append("           AND A.RES_MODEL = B.RES_MODEL(+)" + "\n");
                strSqlString.Append("           AND A.UPEH_GRP = B.UPEH_GRP(+)" + "\n");
                strSqlString.Append("           AND A.MAT_ID = B.MAT_ID(+)" + "\n");
                strSqlString.Append("           AND A.MAT_ID = C.MAT_ID" + "\n");
                strSqlString.Append("           AND A.OPER = D.OPER(+) " + "\n");
                strSqlString.Append("           AND C.FACTORY = '" + cdvFactory.Text + "'" + "\n");
                strSqlString.Append("           AND C.DELETE_FLAG = ' ' " + "\n");
                strSqlString.Append("           AND C.MAT_TYPE = 'FG'" + "\n");
                strSqlString.Append("           AND D.FACTORY(+) = '" + cdvFactory.Text + "'" + "\n");
                strSqlString.Append("           AND A.OPER " + cdvStep.SelectedValueToQueryString + "\n");

                if (cdvModel.Text != "ALL" && cdvModel.Text.Trim() != "")
                    strSqlString.Append("           AND A.RES_MODEL " + cdvModel.SelectedValueToQueryString + "\n");

                if (txtSearchProduct.Text.Trim() != "%" && txtSearchProduct.Text.Trim() != "")
                    strSqlString.AppendFormat("           AND A.MAT_ID LIKE '{0}'" + "\n", txtSearchProduct.Text);

                #region 상세 조회에 따른 SQL문 생성

                if (udcWIPCondition1.Text != "ALL" && udcWIPCondition1.Text != "")
                    strSqlString.AppendFormat("           AND C.MAT_GRP_1 {0} " + "\n", udcWIPCondition1.SelectedValueToQueryString);

                if (udcWIPCondition2.Text != "ALL" && udcWIPCondition2.Text != "")
                    strSqlString.AppendFormat("           AND C.MAT_GRP_2 {0} " + "\n", udcWIPCondition2.SelectedValueToQueryString);

                if (udcWIPCondition3.Text != "ALL" && udcWIPCondition3.Text != "")
                    strSqlString.AppendFormat("           AND C.MAT_GRP_3 {0} " + "\n", udcWIPCondition3.SelectedValueToQueryString);

                if (udcWIPCondition4.Text != "ALL" && udcWIPCondition4.Text != "")
                    strSqlString.AppendFormat("           AND C.MAT_GRP_4 {0} " + "\n", udcWIPCondition4.SelectedValueToQueryString);

                if (udcWIPCondition5.Text != "ALL" && udcWIPCondition5.Text != "")
                    strSqlString.AppendFormat("           AND C.MAT_GRP_5 {0} " + "\n", udcWIPCondition5.SelectedValueToQueryString);

                if (udcWIPCondition6.Text != "ALL" && udcWIPCondition6.Text != "")
                    strSqlString.AppendFormat("           AND C.MAT_GRP_6 {0} " + "\n", udcWIPCondition6.SelectedValueToQueryString);

                if (udcWIPCondition7.Text != "ALL" && udcWIPCondition7.Text != "")
                    strSqlString.AppendFormat("           AND C.MAT_GRP_7 {0} " + "\n", udcWIPCondition7.SelectedValueToQueryString);

                if (udcWIPCondition8.Text != "ALL" && udcWIPCondition8.Text != "")
                    strSqlString.AppendFormat("           AND C.MAT_GRP_8 {0} " + "\n", udcWIPCondition8.SelectedValueToQueryString);

                if (udcWIPCondition9.Text != "ALL" && udcWIPCondition9.Text != "")
                    strSqlString.AppendFormat("           AND C.MAT_GRP_9 {0} " + "\n", udcWIPCondition9.SelectedValueToQueryString);

                #endregion

                strSqlString.Append("       )" + "\n");
                strSqlString.Append(" GROUP BY OPER, OPER_DESC, MAT_CMF_10, MAT_ID, RES_MODEL, UPEH_GRP " + "\n");
                strSqlString.Append(" ORDER BY OPER, OPER_DESC, MAT_CMF_10, MAT_ID, RES_MODEL, UPEH_GRP " + "\n");
                #endregion
            }            
            else
            {
                #region 2. 기존 조회
                strSqlString.Append("SELECT A.OPER, D.OPER_DESC, C.MAT_CMF_10, A.MAT_ID, A.RES_MODEL, A.UPEH_GRP, A.PRODUCT_GROUP" + "\n");

                if (cboType.Text == "ALL")
                {
                    strSqlString.Append("     , B.CYCLE_TIME, B.UPEH, ROUND(B.WPH, 1) AS WPH" + "\n");

                    for (int i = 0; i < selectDate.Length; i++)
                    {
                        strSqlString.Append("     , A.CYCLE_TIME_" + i + ", A.UPEH_" + i + ", ROUND(A.WPH_" + i + ", 1) AS WPH_" + i + "\n");
                    }

                    strSqlString.Append("     , CASE WHEN B.CYCLE_TIME IN (' ', '0') THEN 0" + "\n");
                    strSqlString.Append("            ELSE ROUND(A.CYCLE_TIME_" + iendCnt + " / B.CYCLE_TIME * 100, 0) " + "\n");
                    strSqlString.Append("       END AS TREND_CYCLE" + "\n");
                    strSqlString.Append("     , CASE WHEN B.UPEH = 0 THEN 0" + "\n");
                    strSqlString.Append("            ELSE ROUND(A.UPEH_" + iendCnt + " / B.UPEH * 100, 0) " + "\n");
                    strSqlString.Append("       END AS TREND_UPEH" + "\n");
                    strSqlString.Append("     , CASE WHEN B.WPH = 0 THEN 0" + "\n");
                    strSqlString.Append("            ELSE ROUND(A.WPH_" + iendCnt + " / B.WPH * 100, 0) " + "\n");
                    strSqlString.Append("       END AS TREND_WPH" + "\n");
                }
                else if (cboType.Text == "Cycle Time")
                {
                    strSqlString.Append("     , B.CYCLE_TIME" + "\n");

                    for (int i = 0; i < selectDate.Length; i++)
                    {
                        strSqlString.Append("     , A.CYCLE_TIME_" + i + "\n");
                    }

                    strSqlString.Append("     , CASE WHEN B.CYCLE_TIME IN (' ', '0') THEN 0" + "\n");
                    strSqlString.Append("            ELSE ROUND(A.CYCLE_TIME_" + iendCnt + " / B.CYCLE_TIME * 100, 0) " + "\n");
                    strSqlString.Append("       END AS TREND_CYCLE" + "\n");
                }
                else if (cboType.Text == "UPH")
                {
                    strSqlString.Append("     , B.UPEH" + "\n");

                    for (int i = 0; i < selectDate.Length; i++)
                    {
                        strSqlString.Append("     , A.UPEH_" + i + "\n");
                    }

                    strSqlString.Append("     , CASE WHEN B.UPEH = 0 THEN 0" + "\n");
                    strSqlString.Append("            ELSE ROUND(A.UPEH_" + iendCnt + " / B.UPEH * 100, 0) " + "\n");
                    strSqlString.Append("       END AS TREND_UPEH" + "\n");
                }
                else
                {
                    strSqlString.Append("     , ROUND(B.WPH, 1) AS WPH" + "\n");

                    for (int i = 0; i < selectDate.Length; i++)
                    {
                        strSqlString.Append("     , ROUND(A.WPH_" + i + ", 1) AS WPH_" + i + "\n");
                    }

                    strSqlString.Append("     , CASE WHEN B.WPH = 0 THEN 0" + "\n");
                    strSqlString.Append("            ELSE ROUND(A.WPH_" + iendCnt + " / B.WPH * 100, 0) " + "\n");
                    strSqlString.Append("       END AS TREND_WPH" + "\n");
                }

                strSqlString.Append("  FROM (" + "\n");
                strSqlString.Append("        SELECT OPER, MAT_ID, RES_MODEL, UPEH_GRP" + "\n");
                strSqlString.Append("             , MAX(PRODUCT_GROUP) KEEP(DENSE_RANK FIRST ORDER BY WORK_MONTH DESC) AS PRODUCT_GROUP" + "\n");

                for (int i = 0; i < selectDate.Length; i++)
                {
                    strSqlString.Append("             , MAX(DECODE(WORK_MONTH, '" + selectDate[i].ToString() + "', CYCLE_TIME, 0)) AS CYCLE_TIME_" + i + "\n");
                    strSqlString.Append("             , MAX(DECODE(WORK_MONTH, '" + selectDate[i].ToString() + "', UPEH, 0)) AS UPEH_" + i + "\n");
                    strSqlString.Append("             , MAX(DECODE(WORK_MONTH, '" + selectDate[i].ToString() + "', WPH, 0)) AS WPH_" + i + "\n");
                }

                strSqlString.Append("          FROM CRASUPHDEF_BOH" + "\n");
                strSqlString.Append("         WHERE 1=1" + "\n");
                strSqlString.Append("           AND FACTORY = '" + cdvFactory.Text + "'" + "\n");
                strSqlString.Append("           AND WORK_MONTH BETWEEN '" + sMonth + "' AND '" + eMonth + "' " + "\n");
                strSqlString.Append("         GROUP BY OPER, MAT_ID, RES_MODEL, UPEH_GRP" + "\n");
                strSqlString.Append("       ) A" + "\n");
                strSqlString.Append("     , (" + "\n");
                strSqlString.Append("        SELECT * " + "\n");
                strSqlString.Append("          FROM CRASUPHDEF_BOH" + "\n");
                strSqlString.Append("         WHERE 1=1 " + "\n");
                strSqlString.Append("           AND FACTORY = '" + cdvFactory.Text + "'" + "\n");
                strSqlString.Append("           AND BM_TYPE = 'Y' " + "\n");
                strSqlString.Append("       ) B" + "\n");
                strSqlString.Append("     , MWIPMATDEF C" + "\n");
                strSqlString.Append("     , MWIPOPRDEF D" + "\n");
                strSqlString.Append(" WHERE 1=1" + "\n");
                strSqlString.Append("   AND A.OPER = B.OPER(+)" + "\n");
                strSqlString.Append("   AND A.RES_MODEL = B.RES_MODEL(+)" + "\n");
                strSqlString.Append("   AND A.UPEH_GRP = B.UPEH_GRP(+)" + "\n");
                strSqlString.Append("   AND A.MAT_ID = B.MAT_ID(+)" + "\n");
                strSqlString.Append("   AND A.MAT_ID = C.MAT_ID" + "\n");
                strSqlString.Append("   AND A.OPER = D.OPER(+) " + "\n");
                strSqlString.Append("   AND C.FACTORY = '" + cdvFactory.Text + "'" + "\n");
                strSqlString.Append("   AND C.DELETE_FLAG = ' ' " + "\n");
                strSqlString.Append("   AND C.MAT_TYPE = 'FG'" + "\n");
                strSqlString.Append("   AND D.FACTORY(+) = '" + cdvFactory.Text + "'" + "\n");
                strSqlString.Append("   AND A.OPER " + cdvStep.SelectedValueToQueryString + "\n");

                if (cdvModel.Text != "ALL" && cdvModel.Text.Trim() != "")
                    strSqlString.Append("   AND A.RES_MODEL " + cdvModel.SelectedValueToQueryString + "\n");

                if (txtSearchProduct.Text.Trim() != "%" && txtSearchProduct.Text.Trim() != "")
                    strSqlString.AppendFormat("   AND A.MAT_ID LIKE '{0}'" + "\n", txtSearchProduct.Text);

                #region 상세 조회에 따른 SQL문 생성

                if (udcWIPCondition1.Text != "ALL" && udcWIPCondition1.Text != "")
                    strSqlString.AppendFormat("   AND C.MAT_GRP_1 {0} " + "\n", udcWIPCondition1.SelectedValueToQueryString);

                if (udcWIPCondition2.Text != "ALL" && udcWIPCondition2.Text != "")
                    strSqlString.AppendFormat("   AND C.MAT_GRP_2 {0} " + "\n", udcWIPCondition2.SelectedValueToQueryString);

                if (udcWIPCondition3.Text != "ALL" && udcWIPCondition3.Text != "")
                    strSqlString.AppendFormat("   AND C.MAT_GRP_3 {0} " + "\n", udcWIPCondition3.SelectedValueToQueryString);

                if (udcWIPCondition4.Text != "ALL" && udcWIPCondition4.Text != "")
                    strSqlString.AppendFormat("   AND C.MAT_GRP_4 {0} " + "\n", udcWIPCondition4.SelectedValueToQueryString);

                if (udcWIPCondition5.Text != "ALL" && udcWIPCondition5.Text != "")
                    strSqlString.AppendFormat("   AND C.MAT_GRP_5 {0} " + "\n", udcWIPCondition5.SelectedValueToQueryString);

                if (udcWIPCondition6.Text != "ALL" && udcWIPCondition6.Text != "")
                    strSqlString.AppendFormat("   AND C.MAT_GRP_6 {0} " + "\n", udcWIPCondition6.SelectedValueToQueryString);

                if (udcWIPCondition7.Text != "ALL" && udcWIPCondition7.Text != "")
                    strSqlString.AppendFormat("   AND C.MAT_GRP_7 {0} " + "\n", udcWIPCondition7.SelectedValueToQueryString);

                if (udcWIPCondition8.Text != "ALL" && udcWIPCondition8.Text != "")
                    strSqlString.AppendFormat("   AND C.MAT_GRP_8 {0} " + "\n", udcWIPCondition8.SelectedValueToQueryString);

                if (udcWIPCondition9.Text != "ALL" && udcWIPCondition9.Text != "")
                    strSqlString.AppendFormat("   AND C.MAT_GRP_9 {0} " + "\n", udcWIPCondition9.SelectedValueToQueryString);

                #endregion

                strSqlString.Append(" ORDER BY A.OPER, D.OPER_DESC, C.MAT_CMF_10, A.MAT_ID, A.RES_MODEL, A.UPEH_GRP, A.PRODUCT_GROUP " + "\n");
                #endregion
            }

            if (GlobalVariable.gsUserGroup == "ADMIN_GROUP" || GlobalVariable.gsUserGroup == "HANA_ADMIN_GROUP")
            {
                System.Windows.Forms.Clipboard.SetText(strSqlString.ToString());
            }
 
            return strSqlString.ToString();
        }

        #endregion

        #region EventHandler

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

                spdData.DataSource = dt;
                //int[] rowType = spdData.RPT_DataBindingWithSubTotal(dt, 0, sub+1, 13, null, null, btnSort);

                //spdData.RPT_FillDataSelectiveCells("Total", 0, 13, 0, 1, true, Align.Center, VerticalAlign.Center);
                //spdData.Sheets[0].FrozenColumnCount = 3;

                for (int i = 0; i <= 6; i++)
                {
                    spdData.ActiveSheet.Columns[i].BackColor = Color.LemonChiffon;
                }                

                spdData.RPT_AutoFit(false);

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
            // Excel 바로 보이기
            //ExcelHelper.Instance.subMakeExcel(spdData, this.lblTitle.Text, " ^ ", " ^ ", true);
            spdData.ExportExcel();            
        }

        private void cdvFactory_ValueSelectedItemChanged(object sender, MCCodeViewSelChanged_EventArgs e)
        {
            if (cdvFactory.txtValue.Equals("HMKB1"))
            {
                BaseFormType = eBaseFormType.BUMP_BASE;
                pnlBUMPDetail.Visible = false;
            }
            else
            {
                BaseFormType = eBaseFormType.WIP_BASE;
                pnlWIPDetail.Visible = false;
            }

            this.SetFactory(cdvFactory.txtValue);
            cdvModel.sFactory = cdvFactory.txtValue;
            cdvStep.sFactory = cdvFactory.txtValue;

            cdvStep.SetChangedFlag(true);
            cdvStep.Text = "";
            string strQuery = string.Empty;
            /*
            if (cdvFactory.txtValue != "")
                cdvStep.sDynamicQuery = @"SELECT DISTINCT OPER Code, OPER_DESC Data FROM MWIPOPRDEF WHERE FACTORY " + cdvFactory.SelectedValueToQueryString + " ORDER BY 1 ";
            else
                cdvStep.sDynamicQuery = "";
            */

            strQuery += "SELECT DISTINCT OPR.OPER CODE, OPR.OPER_DESC DATA" + "\n";
            strQuery += "  FROM MRASRESMFO RES, MWIPOPRDEF OPR " + "\n";
            strQuery += " WHERE RES.FACTORY " + cdvFactory.SelectedValueToQueryString + "\n";
            strQuery += "   AND REL_LEVEL='R' " + "\n";

            if (cdvFactory.txtValue.Equals(GlobalVariable.gsAssyDefaultFactory))
            {
                //2010-07-28-임종우 : A1300 공정 표시 되도록 추가함(임태성 요청), 원부자재 공정 제거함.
                strQuery += "   AND RES.OPER NOT IN ('A0100','A0150','A0250','A0320','A0330','A0340','A0350','A0370','A0380','A0390','A0500','A0501',";
                strQuery += "'A0502','A0503','A0504','A0505','A0506','A0507','A0508','A0509','A0550','A0950','A1100','A1110','A1180','A1230','A1720','A1950', 'A2050', 'A2100')" + "\n";
                strQuery += "   AND OPR.OPER LIKE 'A%'" + "\n";
            }       
            else
            {
                strQuery += "   AND RES.OPER IN ('T0100','T0650','T0900','T1040','T1080','T1200')" + "\n";
            }

            strQuery += "   AND RES.FACTORY=OPR.FACTORY " + "\n";
            strQuery += "   AND RES.OPER=OPR.OPER " + "\n";
            strQuery += "ORDER BY OPR.OPER " + "\n";

            if (cdvFactory.txtValue != "")
                cdvStep.sDynamicQuery = strQuery;
            else
                cdvStep.sDynamicQuery = "";


            SortInit();     //add. 150601
        }

        #endregion


        //private void cdvStep_ValueTextChanged(object sender, EventArgs e)
        //{
        //    cdvModel.SetChangedFlag(true);
        //    cdvModel.Text = "";

        //    string strQuery = string.Empty;
        //    strQuery += "SELECT DISTINCT (SELECT RES_GRP_6 FROM MRASRESDEF WHERE FACTORY = LTH.FACTORY AND RES_ID = LTH.RES_ID AND ROWNUM = 1) CODE, ' ' DATA " + "\n";
        //    strQuery += "  FROM MRASRESLTH LTH " + "\n";
        //    strQuery += " WHERE FACTORY " + cdvFactory.SelectedValueToQueryString + "\n";
        //    strQuery += "   AND TRAN_TIME BETWEEN '" + cdvDate.Value.AddDays(-1).ToString("yyyyMMdd") + "220000" + "' AND '" + cdvDate.Value.ToString("yyyyMMdd") + "220000" + "' " + "\n";
        //    if (cdvStep.Text.Trim() != "" && cdvStep.Text.Trim() != "ALL")
        //        strQuery += "   AND OPER IN ('" + cdvStep.Text.Replace(",", "', '") + "') " + "\n";
        //    strQuery += " ORDER BY 1 " + "\n";

        //    if (cdvStep.txtValue != "")
        //        cdvModel.sDynamicQuery = strQuery;
        //    else
        //        cdvModel.sDynamicQuery = "";
        //}       
        
    }
}
