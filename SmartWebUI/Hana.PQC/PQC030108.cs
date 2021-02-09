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
    /// <summary>
    /// 클  래  스: PQC030108<br/>
    /// 클래스요약: 수입검사 Sampling수준<br/>
    /// 작  성  자: 미라콤 양형석<br/>
    /// 최초작성일: 2009-01-20<br/>
    /// 상세  설명: 수입검사 Sampling수준<br/>
    /// 변경  내용: <br/>
    /// </summary>
    public partial class PQC030108 : Miracom.SmartWeb.UI.Controls.udcCUSReport002
    {
        public PQC030108()
        {
            InitializeComponent();
            udcFromToDate.AutoBinding();
            udcFromToDate.DaySelector.SelectedValue = "MONTH";
            SortInit();
            GridColumnInit();
            udcChartFX1.RPT_1_ChartInit();  //차트 초기화. 

            this.udcItem.sFactory = GlobalVariable.gsAssyDefaultFactory;
            this.udcPkgType.sFactory = GlobalVariable.gsAssyDefaultFactory;
            this.udcVendor.sFactory = GlobalVariable.gsAssyDefaultFactory;
            this.udcCurrentTestLevel.sFactory = GlobalVariable.gsAssyDefaultFactory;
            this.udcMatSize.sFactory = GlobalVariable.gsAssyDefaultFactory;
            this.udcLeadCount.sFactory = GlobalVariable.gsAssyDefaultFactory;

            cboChart.SelectedIndexChanged -= cboChart_SelectedIndexChanged;
            cboChart.SelectedIndex = 0;
            cboChart.SelectedIndexChanged += cboChart_SelectedIndexChanged;
        }

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

            return true;
        }

        /// <summary>
        /// 2. 헤더 생성
        /// </summary>
        private void GridColumnInit()
        {
            //spdData.ActiveSheet.ColumnHeader.Rows.Count = 1;
            //spdData.ActiveSheet.RowHeader.ColumnCount = 0;
            spdData.RPT_ColumnInit();


            spdData.RPT_AddBasicColumn("Item", 0, 0, Visibles.True, Frozen.True, Align.Center, Merge.True, Formatter.String, 80);
            spdData.RPT_AddBasicColumn("Vendor", 0, 1, Visibles.True, Frozen.True, Align.Center, Merge.True, Formatter.String, 80);
            spdData.RPT_AddBasicColumn("PKG Type", 0, 2, Visibles.True, Frozen.True, Align.Left, Merge.True, Formatter.String, 80);
            spdData.RPT_AddBasicColumn("Standard", 0, 3, Visibles.True, Frozen.True, Align.Left, Merge.True, Formatter.String, 80);
            spdData.RPT_AddBasicColumn("Progress of the current inspection level", 0, 4, Visibles.True, Frozen.False, Align.Center, Merge.True, Formatter.String, 80);
            spdData.RPT_AddBasicColumn("Skip count", 1, 4, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.String, 80);
            spdData.RPT_AddBasicColumn("Cumulative Failure", 1, 5, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.String, 80);
            spdData.RPT_AddBasicColumn("Continuous pass", 1, 6, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.String, 80);
            spdData.RPT_AddBasicColumn("Current inspection level", 0, 7, Visibles.True, Frozen.False, Align.Center, Merge.False, Formatter.String, 80);

            spdData.RPT_MerageHeaderRowSpan(0, 0, 2);
            spdData.RPT_MerageHeaderRowSpan(0, 1, 2);
            spdData.RPT_MerageHeaderRowSpan(0, 2, 2);
            spdData.RPT_MerageHeaderRowSpan(0, 3, 2);
            spdData.RPT_MerageHeaderColumnSpan(0, 4, 3);
            spdData.RPT_MerageHeaderRowSpan(0, 7, 2);

            spdData.RPT_ColumnConfigFromTable(btnSort); //Group항목이 있을경우 반드시 선언해줄것.
        }

        /// <summary>
        /// 3. Group By 정의
        /// </summary>
        private void SortInit()
        {
            ((udcTableForm)(this.btnSort.BindingForm)).AddRow("Item", "NVL((SELECT DATA_1 FROM MGCMTBLDAT WHERE FACTORY = MAT.FACTORY AND TABLE_NAME = 'MATERIAL_TYPE' AND KEY_1 = MAT.MAT_TYPE AND ROWNUM =1),'-') MATERIAL_TYPE", "MAT_TYPE", "MAT.MAT_TYPE", true);
            ((udcTableForm)(this.btnSort.BindingForm)).AddRow("Vendor", "NVL((SELECT DATA_1 FROM MGCMTBLDAT WHERE FACTORY = MAT.FACTORY AND TABLE_NAME = 'H_VENDOR' AND KEY_1 = CSM.VENDOR AND ROWNUM =1),'-')", "VENDOR", "CSM.VENDOR", true);
            ((udcTableForm)(this.btnSort.BindingForm)).AddRow("PKG Type", "NVL((SELECT DATA_1 FROM MGCMTBLDAT WHERE FACTORY = MAT.FACTORY AND TABLE_NAME = 'H_PKG_TYPE' AND KEY_1 = MAT.MAT_GRP_3 AND ROWNUM =1),'-') PKG_TYPE", "MAT_GRP_3", "MAT.MAT_GRP_3", true);
            ((udcTableForm)(this.btnSort.BindingForm)).AddRow("Standard", "MAT_SIZE", "MAT_SIZE", "CSM.MAT_SIZE", true);
        }

        /// <summary>
        /// 4. 쿼리 생성
        /// </summary>
        /// <returns></returns>
        private string MakeSqlString(int nIndex)
        {
            StringBuilder strSqlString = new StringBuilder();

            string QueryCond1 = null;
            string QueryCond2 = null;
            string QueryCond3 = null;

            string strFromDate = string.Empty;
            string strToDate = string.Empty;

            udcTableForm tableForm = (udcTableForm)btnSort.BindingForm;

            QueryCond1 = tableForm.SelectedValueToQueryContainNull;
            QueryCond2 = tableForm.SelectedValue2ToQuery;
            QueryCond3 = tableForm.SelectedValue3ToQuery;

            #region " udcDuration에서 정확한 조회시간을 받아오기 : strFromDate, strToDate "
            strFromDate = udcFromToDate.ExactFromDate;
            strToDate = udcFromToDate.ExactToDate;
            #endregion

            switch (nIndex)
            {
                case 0:
                    {
                        #region " 스프레드 쿼리 "

                        strSqlString.Append("SELECT " + QueryCond1 + " " + "\n");
                        strSqlString.Append("     , SKIP_CNT_BY_PASS " + "\n");
                        strSqlString.Append("     , TST_CNT_BY_FAIL " + "\n");
                        strSqlString.Append("     , TEST_COUNT " + "\n");
                        strSqlString.Append("     , SMP_LEVEL " + "\n");
                        strSqlString.Append("  FROM CIQCSMPSTS CSM " + "\n");
                        strSqlString.Append("     , MWIPMATDEF MAT " + "\n");
                        strSqlString.Append(" WHERE 1=1 " + "\n");
                        strSqlString.Append("   AND CSM.FACTORY = MAT.FACTORY " + "\n");
                        strSqlString.Append("   AND CSM.MAT_ID = MAT.MAT_ID " + "\n");

                        #region " 조회조건 : 품목, Vendor, Lead Count, PKG종류, 규격, 현 검사수준 "
                        if (udcItem.Text != "ALL" && udcItem.Text != "")
                            strSqlString.AppendFormat("   AND MAT.MAT_TYPE {0} " + "\n", udcItem.SelectedValueToQueryString);

                        if (udcVendor.Text != "ALL" && udcVendor.Text != "")
                            strSqlString.AppendFormat("   AND CSM.VENDOR {0} " + "\n", udcVendor.SelectedValueToQueryString);

                        if (udcPkgType.Text != "ALL" && udcPkgType.Text != "")
                            strSqlString.AppendFormat("   AND MAT.MAT_GRP_3 {0} " + "\n", udcPkgType.SelectedValueToQueryString);

                        if (udcMatSize.Text != "ALL" && udcMatSize.Text != "")
                            strSqlString.AppendFormat("   AND CSM.MAT_SIZE {0} " + "\n", udcMatSize.SelectedValueToQueryString);

                        if (udcLeadCount.Text != "ALL" && udcLeadCount.Text != "")
                            strSqlString.AppendFormat("   AND MAT.MAT_GRP_6 {0} " + "\n", udcLeadCount.SelectedValueToQueryString);

                        if (udcCurrentTestLevel.Text != "ALL" && udcCurrentTestLevel.Text != "")
                            strSqlString.AppendFormat("   AND CSM.SMP_LEVEL {0} " + "\n", udcCurrentTestLevel.SelectedValueToQueryString);
                        #endregion

                        #endregion
                    }
                    break;
                case 1:
                    {
                        #region " 1번 수준별 검사현황 "

                        strSqlString.Append("SELECT SMP_LEVEL, COUNT(*) CNT " + "\n");
                        strSqlString.Append("  FROM CIQCSMPSTS CSM " + "\n");
                        strSqlString.Append("     , MWIPMATDEF MAT " + "\n");
                        strSqlString.Append(" WHERE 1=1 " + "\n");
                        strSqlString.Append("   AND CSM.FACTORY = MAT.FACTORY " + "\n");
                        strSqlString.Append("   AND CSM.MAT_ID = MAT.MAT_ID " + "\n");

                        #region " 조회조건 : 품목, Vendor, Lead Count, PKG종류, 규격, 현 검사수준 "
                        if (udcItem.Text != "ALL" && udcItem.Text != "")
                            strSqlString.AppendFormat("   AND MAT.MAT_TYPE {0} " + "\n", udcItem.SelectedValueToQueryString);

                        if (udcVendor.Text != "ALL" && udcVendor.Text != "")
                            strSqlString.AppendFormat("   AND CSM.VENDOR {0} " + "\n", udcVendor.SelectedValueToQueryString);

                        if (udcPkgType.Text != "ALL" && udcPkgType.Text != "")
                            strSqlString.AppendFormat("   AND MAT.MAT_GRP_3 {0} " + "\n", udcPkgType.SelectedValueToQueryString);

                        if (udcMatSize.Text != "ALL" && udcMatSize.Text != "")
                            strSqlString.AppendFormat("   AND CSM.MAT_SIZE {0} " + "\n", udcMatSize.SelectedValueToQueryString);

                        if (udcLeadCount.Text != "ALL" && udcLeadCount.Text != "")
                            strSqlString.AppendFormat("   AND MAT.MAT_GRP_6 {0} " + "\n", udcLeadCount.SelectedValueToQueryString);

                        if (udcCurrentTestLevel.Text != "ALL" && udcCurrentTestLevel.Text != "")
                            strSqlString.AppendFormat("   AND CSM.SMP_LEVEL {0} " + "\n", udcCurrentTestLevel.SelectedValueToQueryString);
                        #endregion

                        strSqlString.Append(" GROUP BY SMP_LEVEL " + "\n");

                        #endregion
                    }
                    break;
                case 2:
                    {
                        #region " 2번 품목별 검사수준 "

                        strSqlString.Append("SELECT NVL((SELECT DATA_1 FROM MGCMTBLDAT WHERE FACTORY = MAT.FACTORY AND TABLE_NAME = 'MATERIAL_TYPE' AND KEY_1 = MAT.MAT_TYPE AND ROWNUM =1),'-') \"품목\" " + "\n");
                        strSqlString.Append("     , SUM(DECODE(SMP_LEVEL, '검사중지', 1, 0)) \"검사중지\" " + "\n");
                        strSqlString.Append("     , SUM(DECODE(SMP_LEVEL, '까다로운검사', 1, 0)) \"까다로운검사\" " + "\n");
                        strSqlString.Append("     , SUM(DECODE(SMP_LEVEL, '보통검사', 1, 0)) \"보통검사\" " + "\n");
                        strSqlString.Append("     , SUM(DECODE(SMP_LEVEL, '수월한검사', 1, 0)) \"수월한검사\" " + "\n");
                        strSqlString.Append("     , SUM(DECODE(SMP_LEVEL, '검사SKIP', 1, 0)) \"검사SKIP\" " + "\n");
                        strSqlString.Append("  FROM CIQCSMPSTS CSM " + "\n");
                        strSqlString.Append("     , MWIPMATDEF MAT " + "\n");
                        strSqlString.Append(" WHERE 1=1 " + "\n");
                        strSqlString.Append("   AND CSM.FACTORY = MAT.FACTORY " + "\n");
                        strSqlString.Append("   AND CSM.MAT_ID = MAT.MAT_ID " + "\n");

                        #region " 조회조건 : 품목, Vendor, Lead Count, PKG종류, 규격, 현 검사수준 "
                        if (udcItem.Text != "ALL" && udcItem.Text != "")
                            strSqlString.AppendFormat("   AND MAT.MAT_TYPE {0} " + "\n", udcItem.SelectedValueToQueryString);

                        if (udcVendor.Text != "ALL" && udcVendor.Text != "")
                            strSqlString.AppendFormat("   AND CSM.VENDOR {0} " + "\n", udcVendor.SelectedValueToQueryString);

                        if (udcPkgType.Text != "ALL" && udcPkgType.Text != "")
                            strSqlString.AppendFormat("   AND MAT.MAT_GRP_3 {0} " + "\n", udcPkgType.SelectedValueToQueryString);

                        if (udcMatSize.Text != "ALL" && udcMatSize.Text != "")
                            strSqlString.AppendFormat("   AND CSM.MAT_SIZE {0} " + "\n", udcMatSize.SelectedValueToQueryString);

                        if (udcLeadCount.Text != "ALL" && udcLeadCount.Text != "")
                            strSqlString.AppendFormat("   AND MAT.MAT_GRP_6 {0} " + "\n", udcLeadCount.SelectedValueToQueryString);

                        if (udcCurrentTestLevel.Text != "ALL" && udcCurrentTestLevel.Text != "")
                            strSqlString.AppendFormat("   AND CSM.SMP_LEVEL {0} " + "\n", udcCurrentTestLevel.SelectedValueToQueryString);
                        #endregion

                        strSqlString.Append(" GROUP BY MAT.FACTORY, MAT.MAT_TYPE " + "\n");
                        #endregion
                    }
                    break;
                case 3:
                    {
                        #region " 3번 검사수준별 비율 "

                        strSqlString.Append("SELECT SMP_LEVEL \"검사수준\", COUNT(*) CNT  " + "\n");
                        strSqlString.Append("  FROM CIQCSMPSTS CSM  " + "\n");
                        strSqlString.Append("     , MWIPMATDEF MAT  " + "\n");
                        strSqlString.Append(" WHERE 1=1  " + "\n");
                        strSqlString.Append("   AND CSM.FACTORY = MAT.FACTORY  " + "\n");
                        strSqlString.Append("   AND CSM.MAT_ID = MAT.MAT_ID  " + "\n");

                        #region " 조회조건 : 품목, Vendor, Lead Count, PKG종류, 규격, 현 검사수준 "
                        if (udcItem.Text != "ALL" && udcItem.Text != "")
                            strSqlString.AppendFormat("   AND MAT.MAT_TYPE {0} " + "\n", udcItem.SelectedValueToQueryString);

                        if (udcVendor.Text != "ALL" && udcVendor.Text != "")
                            strSqlString.AppendFormat("   AND CSM.VENDOR {0} " + "\n", udcVendor.SelectedValueToQueryString);

                        if (udcPkgType.Text != "ALL" && udcPkgType.Text != "")
                            strSqlString.AppendFormat("   AND MAT.MAT_GRP_3 {0} " + "\n", udcPkgType.SelectedValueToQueryString);

                        if (udcMatSize.Text != "ALL" && udcMatSize.Text != "")
                            strSqlString.AppendFormat("   AND CSM.MAT_SIZE {0} " + "\n", udcMatSize.SelectedValueToQueryString);

                        if (udcLeadCount.Text != "ALL" && udcLeadCount.Text != "")
                            strSqlString.AppendFormat("   AND MAT.MAT_GRP_6 {0} " + "\n", udcLeadCount.SelectedValueToQueryString);

                        if (udcCurrentTestLevel.Text != "ALL" && udcCurrentTestLevel.Text != "")
                            strSqlString.AppendFormat("   AND CSM.SMP_LEVEL {0} " + "\n", udcCurrentTestLevel.SelectedValueToQueryString);
                        #endregion

                        strSqlString.Append(" GROUP BY SMP_LEVEL " + "\n");

                        #endregion
                    }
                    break;
                case 4:
                    {
                        #region " 4번 품목별 검사수준 "

                        strSqlString.Append("SELECT \"품목\" " + "\n");
                        strSqlString.Append("     , RATIO_TO_REPORT(\"검사중지\") OVER() \"검사중지\" " + "\n");
                        strSqlString.Append("     , RATIO_TO_REPORT(\"까다로운검사\") OVER() \"까다로운검사\" " + "\n");
                        strSqlString.Append("     , RATIO_TO_REPORT(\"보통검사\") OVER() \"보통검사\" " + "\n");
                        strSqlString.Append("     , RATIO_TO_REPORT(\"수월한검사\") OVER() \"수월한검사\" " + "\n");
                        strSqlString.Append("     , RATIO_TO_REPORT(\"검사SKIP\") OVER() \"검사SKIP\" " + "\n");
                        strSqlString.Append("  FROM ( " + "\n");
                        strSqlString.Append("        SELECT NVL((SELECT DATA_1 FROM MGCMTBLDAT WHERE FACTORY = MAT.FACTORY AND TABLE_NAME = 'MATERIAL_TYPE' AND KEY_1 = MAT.MAT_TYPE AND ROWNUM =1),'-') \"품목\" " + "\n");
                        strSqlString.Append("             , SUM(DECODE(SMP_LEVEL, '검사중지', 1, 0)) \"검사중지\" " + "\n");
                        strSqlString.Append("             , SUM(DECODE(SMP_LEVEL, '까다로운검사', 1, 0)) \"까다로운검사\" " + "\n");
                        strSqlString.Append("             , SUM(DECODE(SMP_LEVEL, '보통검사', 1, 0)) \"보통검사\" " + "\n");
                        strSqlString.Append("             , SUM(DECODE(SMP_LEVEL, '수월한검사', 1, 0)) \"수월한검사\" " + "\n");
                        strSqlString.Append("             , SUM(DECODE(SMP_LEVEL, '검사SKIP', 1, 0)) \"검사SKIP\" " + "\n");
                        strSqlString.Append("          FROM CIQCSMPSTS CSM " + "\n");
                        strSqlString.Append("             , MWIPMATDEF MAT " + "\n");
                        strSqlString.Append("         WHERE 1=1 " + "\n");
                        strSqlString.Append("           AND CSM.FACTORY = MAT.FACTORY " + "\n");
                        strSqlString.Append("           AND CSM.MAT_ID = MAT.MAT_ID " + "\n");

                        #region " 조회조건 : 품목, Vendor, Lead Count, PKG종류, 규격, 현 검사수준 "
                        if (udcItem.Text != "ALL" && udcItem.Text != "")
                            strSqlString.AppendFormat("   AND MAT.MAT_TYPE {0} " + "\n", udcItem.SelectedValueToQueryString);

                        if (udcVendor.Text != "ALL" && udcVendor.Text != "")
                            strSqlString.AppendFormat("   AND CSM.VENDOR {0} " + "\n", udcVendor.SelectedValueToQueryString);

                        if (udcPkgType.Text != "ALL" && udcPkgType.Text != "")
                            strSqlString.AppendFormat("   AND MAT.MAT_GRP_3 {0} " + "\n", udcPkgType.SelectedValueToQueryString);

                        if (udcMatSize.Text != "ALL" && udcMatSize.Text != "")
                            strSqlString.AppendFormat("   AND CSM.MAT_SIZE {0} " + "\n", udcMatSize.SelectedValueToQueryString);

                        if (udcLeadCount.Text != "ALL" && udcLeadCount.Text != "")
                            strSqlString.AppendFormat("   AND MAT.MAT_GRP_6 {0} " + "\n", udcLeadCount.SelectedValueToQueryString);

                        if (udcCurrentTestLevel.Text != "ALL" && udcCurrentTestLevel.Text != "")
                            strSqlString.AppendFormat("   AND CSM.SMP_LEVEL {0} " + "\n", udcCurrentTestLevel.SelectedValueToQueryString);
                        #endregion

                        strSqlString.Append("         GROUP BY MAT.FACTORY, MAT.MAT_TYPE " + "\n");
                        strSqlString.Append("       ) " + "\n");

                        #endregion
                    }
                    break;
            }

            if (GlobalVariable.gsUserGroup == "ADMIN_GROUP" || GlobalVariable.gsUserGroup == "HANA_ADMIN_GROUP")
            {
                System.Windows.Forms.Clipboard.SetText(strSqlString.ToString());
            }

            return strSqlString.ToString();
        }        


        private void ShowChart()
        {
            // 차트 설정
            udcChartFX1.RPT_1_ChartInit();
            udcChartFX1.RPT_2_ClearData();

            udcChartFX1.AxisY.Font = new System.Drawing.Font("굴림체", 8, System.Drawing.FontStyle.Regular);
            udcChartFX1.AxisY2.Font = new System.Drawing.Font("굴림체", 8, System.Drawing.FontStyle.Regular);
            udcChartFX1.AxisX.Font = new System.Drawing.Font("굴림체", 8, System.Drawing.FontStyle.Regular);

            switch(cboChart.SelectedIndex)
            {
                case 0:
                    {
                        #region " 1번 수준별 검사현황 "

                        DataTable dt = CmnFunction.oComm.GetFuncDataTable("DYNAMIC", MakeSqlString(1));

                        if(dt == null || dt.Rows.Count < 1)
                            return;

                        udcChartFX1.DataSource = dt;
                        udcChartFX1.Gallery = SoftwareFX.ChartFX.Gallery.Bar;
                        udcChartFX1.LegendBox = false;
                        udcChartFX1.PointLabels = true;

                        udcChartFX1.AxisY.Max = udcChartFX1.AxisY.Max * 1.2;
                        #endregion
                    }
                    break;
                case 1:
                    {
                        #region " 2번 품목별 검사수준 "

                        DataTable dt = CmnFunction.oComm.GetFuncDataTable("DYNAMIC", MakeSqlString(2));
                        
                        if (dt == null || dt.Rows.Count < 1)
                            return;

                        udcChartFX1.DataSource = dt;
                        udcChartFX1.Gallery = SoftwareFX.ChartFX.Gallery.Bar;
                        udcChartFX1.SerLegBox = true;
                        udcChartFX1.SerLegBoxObj.Docked = SoftwareFX.ChartFX.Docked.Top;
                        udcChartFX1.LegendBox = false;
                        udcChartFX1.PointLabels = true;

                        udcChartFX1.AxisY.Max = udcChartFX1.AxisY.Max * 1.2;
                        #endregion
                    }
                    break;
                case 2:
                    {
                        #region " 3번 검사수준별 비율 "

                        DataTable dt = CmnFunction.oComm.GetFuncDataTable("DYNAMIC", MakeSqlString(3));

                        if (dt == null || dt.Rows.Count < 1)
                            return;

                        udcChartFX1.DataSource = dt;
                        udcChartFX1.Gallery = SoftwareFX.ChartFX.Gallery.Pie;
                        udcChartFX1.LegendBox = false;
                        udcChartFX1.PointLabels = true;

                        udcChartFX1.AxisY.Max = udcChartFX1.AxisY.Max * 1.2;
                        #endregion
                    }
                    break;
                case 3:
                    {
                        #region " 4번 품목별 검사수준 "

                        DataTable dt = CmnFunction.oComm.GetFuncDataTable("DYNAMIC", MakeSqlString(4));

                        if (dt == null || dt.Rows.Count < 1)
                            return;

                        udcChartFX1.DataSource = dt;
                        udcChartFX1.Gallery = SoftwareFX.ChartFX.Gallery.Pie;
                        udcChartFX1.LegendBox = true;
                        udcChartFX1.PointLabels = true;

                        udcChartFX1.AxisY.Max = udcChartFX1.AxisY.Max * 1.2;
                        #endregion
                    }
                    break;
            }
        }

        #endregion
        
        #region " EVENT HANDLER "

        private void btnView_Click(object sender, EventArgs e)
        {
            DataTable dt = null;

            if (CheckField() == false) return;

            GridColumnInit();
            spdData_Sheet1.RowCount = 0;

            try
            {
                LoadingPopUp.LoadIngPopUpShow(this);
                this.Refresh();

                dt = CmnFunction.oComm.GetFuncDataTable("DYNAMIC", MakeSqlString(0));

                if (dt.Rows.Count == 0)
                {
                    dt.Dispose();
                    LoadingPopUp.LoadingPopUpHidden();
                    CmnFunction.ShowMsgBox(RptMessages.GetMessage("STD010", GlobalVariable.gcLanguage));
                    return;
                }

                //by John
                //1.Griid 합계 표시

                spdData.DataSource = dt;
                spdData.RPT_ColumnConfigFromTable(btnSort);
                

                //2. 칼럼 고정(필요하다면..)
                //spdData.Sheets[0].FrozenColumnCount = 10;

                //3. Total부분 셀머지
                //spdData.RPT_FillDataSelectiveCells("Total", 0, 4, 0, 9, true, Align.Center, VerticalAlign.Center);

                //4. Column Auto Fit4
                spdData.RPT_AutoFit(false);

                // 1번 그래프 그리도록 이벤트 발생
                cboChart.SelectedIndex = 0;
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
            ExcelHelper.Instance.subMakeExcel(spdData, this.lblTitle.Text, " ^ ", " ^ ");
        }

        private void cdvFactory_ValueSelectedItemChanged(object sender, Miracom.UI.MCCodeViewSelChanged_EventArgs e)
        {
            this.SetFactory(cdvFactory.txtValue);
        }

        private void cboChart_SelectedIndexChanged(object sender, EventArgs e)
        {
            ShowChart();
        }

        #endregion
    }
}