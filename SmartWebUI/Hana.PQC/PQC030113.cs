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
    public partial class PQC030113 : Miracom.SmartWeb.UI.Controls.udcCUSReport002
    {
        private DataTable dtDftCode;
        
        /// <summary>
        /// 클  래  스: PQC030113<br/>
        /// 클래스요약: 도금수입검사현황(이력)조회<br/>
        /// 작  성  자: 미라콤 양형석<br/>
        /// 최초작성일: 2009-01-22<br/>
        /// 상세  설명: 도금수입검사현황(이력)조회<br/>
        /// 변경  내용: <br/>
        /// </summary>
        public PQC030113()
        {
            InitializeComponent();
            udcFromToDate.AutoBinding();
            udcFromToDate.DaySelector.SelectedValue = "MONTH";
            SortInit();
            GridColumnInit();
            udcChartFX1.RPT_1_ChartInit();  //차트 초기화. 

            this.udcCustomer.sFactory = GlobalVariable.gsAssyDefaultFactory;
            this.udcVendor.sFactory = GlobalVariable.gsAssyDefaultFactory;
            this.udcLotType.sFactory = GlobalVariable.gsAssyDefaultFactory;
            this.udcPkgType.sFactory = GlobalVariable.gsAssyDefaultFactory;

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
            spdData.RPT_ColumnInit();

            spdData.RPT_AddBasicColumn("Lot No", 0, 0, Visibles.True, Frozen.True, Align.Left, Merge.True, Formatter.String, 80);
            spdData.RPT_AddBasicColumn("Lot Division", 0, 1, Visibles.True, Frozen.True, Align.Center, Merge.True, Formatter.String, 80);
            spdData.RPT_AddBasicColumn("Customer", 0, 2, Visibles.True, Frozen.True, Align.Center, Merge.True, Formatter.String, 80);

            spdData.RPT_AddBasicColumn("Lead수", 0, 3, Visibles.True, Frozen.True, Align.Center, Merge.True, Formatter.String, 80);
            spdData.RPT_AddBasicColumn("PKG", 0, 4, Visibles.True, Frozen.True, Align.Center, Merge.True, Formatter.String, 80);
            spdData.RPT_AddBasicColumn("Density", 0, 5, Visibles.True, Frozen.True, Align.Center, Merge.True, Formatter.String, 80);
            spdData.RPT_AddBasicColumn("Outsourcing company", 0, 6, Visibles.True, Frozen.True, Align.Center, Merge.True, Formatter.String, 80);
            spdData.RPT_AddBasicColumn("LOT Qty", 0, 7, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 80);
            spdData.RPT_AddBasicColumn("Date of inspection", 0, 8, Visibles.True, Frozen.False, Align.Center, Merge.False, Formatter.String, 80);
            spdData.RPT_AddBasicColumn("Inspection quantity", 0, 9, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 80);
            spdData.RPT_AddBasicColumn("Inspector", 0, 10, Visibles.True, Frozen.False, Align.Center, Merge.False, Formatter.String, 80);
            spdData.RPT_AddBasicColumn("Judgment result", 0, 11, Visibles.True, Frozen.False, Align.Center, Merge.False, Formatter.String, 80);
            spdData.RPT_AddBasicColumn("Appearance defect (ppm)", 0, 12, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 80);
            spdData.RPT_AddBasicColumn("Total Defect Count", 0, 13, Visibles.True, Frozen.False, Align.Center, Merge.False, Formatter.Number, 80);

            spdData.RPT_ColumnConfigFromTable(btnSort); //Group항목이 있을경우 반드시 선언해줄것.
        }

        /// <summary>
        /// 3. Group By 정의
        /// </summary>
        private void SortInit()
        {
            ((udcTableForm)(this.btnSort.BindingForm)).AddRow("Lot No", "HIS.LOT_ID", "HIS.LOT_ID", "HIS.LOT_ID", true);
            ((udcTableForm)(this.btnSort.BindingForm)).AddRow("Lot Division", "HIS.IQC_TYPE", "HIS.IQC_TYPE", "HIS.IQC_TYPE", true);
            ((udcTableForm)(this.btnSort.BindingForm)).AddRow("Customer", "NVL((SELECT DATA_1 FROM MGCMTBLDAT WHERE FACTORY = MAT.FACTORY AND TABLE_NAME = 'H_CUSTOMER' AND KEY_1 = MAT.MAT_GRP_1), '-') CUSTOMER", "MAT.MAT_GRP_1", "MAT.MAT_GRP_1", true);
            ((udcTableForm)(this.btnSort.BindingForm)).AddRow("Lead count", "HIS.LEAD_COUNT", "HIS.LEAD_COUNT", "HIS.LEAD_COUNT", true);
            ((udcTableForm)(this.btnSort.BindingForm)).AddRow("PKG", "HIS.H_PACKAGE", "HIS.H_PACKAGE", "HIS.H_PACKAGE", true);
            ((udcTableForm)(this.btnSort.BindingForm)).AddRow("Density", "HIS.DENSITY", "HIS.DENSITY", "HIS.DENSITY", true);
            ((udcTableForm)(this.btnSort.BindingForm)).AddRow("Outsourcing company", "NVL((SELECT DATA_1 FROM MGCMTBLDAT WHERE FACTORY = MAT.FACTORY AND TABLE_NAME = 'H_VENDOR' AND KEY_1 = BAT.VENDOR), '-') VENDOR", "BAT.VENDOR", "BAT.VENDOR", true);

        }

        /// <summary>
        /// 4. 쿼리 생성
        /// </summary>
        /// <returns></returns>
        private string MakeSqlString(int nSwitch)
        {
            StringBuilder strSqlString = new StringBuilder();

            string QueryCond1 = null;
            string QueryCond2 = null;
            string QueryCond3 = null;

            string strFromDate = string.Empty;
            string strToDate = string.Empty;

            string strDecode = string.Empty;

            udcTableForm tableForm = (udcTableForm)btnSort.BindingForm;

            QueryCond1 = tableForm.SelectedValueToQueryContainNull;
            QueryCond2 = tableForm.SelectedValue2ToQuery;
            QueryCond3 = tableForm.SelectedValue3ToQuery;

            #region " udcDuration에서 정확한 조회시간을 받아오기 : strFromDate, strToDate "
            strFromDate = udcFromToDate.ExactFromDate;
            strToDate = udcFromToDate.ExactToDate;
            #endregion

            switch(nSwitch)
            {
                case 0: // 스프레드용 쿼리

                    #region " DEFECT CODE 미리 가져오는 쿼리 : dtDftCode "
                    strSqlString.Append("SELECT DISTINCT DATA_1 " + "\n");
                    strSqlString.Append("  FROM ( " + "\n");
                    strSqlString.Append("        SELECT DISTINCT FACTORY, IQC_TYPE, IQC_NO, MAT_VER, LOT_ID, MAT_ID  " + "\n");
                    strSqlString.Append("          FROM CIQCLOTHIS " + "\n");
                    strSqlString.Append("         WHERE FACTORY = '" + GlobalVariable.gsAssyDefaultFactory + "'" + "\n");
                    if (udcPkgType.txtValue.Trim() != "" && udcPkgType.txtValue.Trim() != "ALL")
                        strSqlString.Append("           AND H_PACKAGE " + udcPkgType.SelectedValueToQueryString + "\n");
                    if (udcLotType.txtValue.Trim() != "" && udcLotType.txtValue.Trim() != "ALL")
                        strSqlString.Append("           AND IQC_TYPE " + udcLotType.SelectedValueToQueryString + "\n");
                    strSqlString.Append("           AND TRAN_TIME BETWEEN '" + strFromDate + "' AND '" + strToDate + "' " + "\n");
                    strSqlString.Append("           AND HIST_DEL_FLAG = ' ' " + "\n");
                    strSqlString.Append("           AND MAT_VER = 1 " + "\n");
                    strSqlString.Append("       ) HIS " + "\n");
                    strSqlString.Append("     , CWIPSITDEF DEF " + "\n");
                    strSqlString.Append("     , CIQCLOTDFT DFT " + "\n");
                    strSqlString.Append("     , MWIPMATDEF MAT " + "\n");
                    strSqlString.Append("     , CIQCBATSTS BAT " + "\n");
                    strSqlString.Append(" WHERE 1=1 " + "\n");
                    strSqlString.Append("   AND HIS.FACTORY = BAT.FACTORY " + "\n");
                    strSqlString.Append("   AND HIS.IQC_TYPE = BAT.IQC_TYPE " + "\n");
                    strSqlString.Append("   AND HIS.IQC_NO = BAT.IQC_NO " + "\n");
                    strSqlString.Append("   AND HIS.MAT_ID = BAT.MAT_ID " + "\n");
                    strSqlString.Append("   AND HIS.MAT_VER = BAT.MAT_VER " + "\n");
                    strSqlString.Append("   AND HIS.FACTORY = DFT.FACTORY " + "\n");
                    strSqlString.Append("   AND HIS.FACTORY = MAT.FACTORY " + "\n");
                    strSqlString.Append("   AND HIS.FACTORY = DEF.FACTORY " + "\n");
                    strSqlString.Append("   AND HIS.LOT_ID = DFT.LOT_ID " + "\n");
                    strSqlString.Append("   AND HIS.MAT_ID = MAT.MAT_ID " + "\n");
                    strSqlString.Append("   AND DEF.TABLE_NAME = 'MAT_DEFECT_RELATION' " + "\n");
                    strSqlString.Append("   AND DEF.KEY_1 = MAT.MAT_TYPE " + "\n");
                    strSqlString.Append("   AND DEF.KEY_2 = DFT.DEFECT_CODE " + "\n");
                    if (udcCustomer.txtValue.Trim() != "" && udcCustomer.txtValue.Trim() != "ALL")
                        strSqlString.Append("   AND MAT.MAT_GRP_1 " + udcCustomer.SelectedValueToQueryString + "\n");
                    if (udcVendor.txtValue.Trim() != "" && udcVendor.txtValue.Trim() != "ALL")
                        strSqlString.Append("   AND BAT.VENDOR " + udcVendor.SelectedValueToQueryString + "\n");
                    dtDftCode = CmnFunction.oComm.GetFuncDataTable("DYNAMIC", strSqlString.ToString());
                    strSqlString.Remove(0, strSqlString.Length - 1);
                    #endregion

                    #region " 스프레드 칼럼추가 & DECODE 쿼리구문 : strDecode "
                    for (int i = 0; i < dtDftCode.Rows.Count; i++)
                    {
                        strDecode += "     , SUM(DECODE(DEF.DATA_1, '" + dtDftCode.Rows[i][0].ToString() + "', 1, 0)) DEF_" + i.ToString() + "\n";
                        spdData.RPT_AddBasicColumn(dtDftCode.Rows[0][0].ToString(), 0, spdData.ActiveSheet.Columns.Count, Visibles.True, Frozen.False, Align.Center, Merge.False, Formatter.Number, 80);
                    }
                    #endregion

                    #region " MAIN 쿼리 "
                    strSqlString.Append("SELECT " + QueryCond1 + " " + "\n");
                    strSqlString.Append("     , LOT.QTY_1 LOT_QTY " + "\n");
                    strSqlString.Append("     , BAT.CREATE_TIME " + "\n");
                    strSqlString.Append("     , HIS.SAMPLE_QTY " + "\n");
                    strSqlString.Append("     , BAT.QC_OPERATOR " + "\n");
                    strSqlString.Append("     , BAT.QC_PASS_FLAG " + "\n");
                    strSqlString.Append("     , TRUNC(SUM(DFT.DEFECT_QTY_1) / LOT.CREATE_QTY_1 * 1000000) PPM  " + "\n");
                    strSqlString.Append("     , SUM(DFT.DEFECT_QTY_1) TOTAL_DFT_QTY " + "\n");
                    strSqlString.Append(strDecode);
                    strSqlString.Append("  FROM ( " + "\n");
                    strSqlString.Append("        SELECT DISTINCT FACTORY, IQC_TYPE, IQC_NO, LOT_ID, MAT_ID, MAT_VER, SAMPLE_QTY, TOTAL_DEFECT_QTY_1, H_PACKAGE, LEAD_COUNT, DENSITY  " + "\n");
                    strSqlString.Append("          FROM CIQCLOTHIS " + "\n");
                    strSqlString.Append("         WHERE 1=1 " + "\n");
                    strSqlString.Append("           AND FACTORY = '" + GlobalVariable.gsAssyDefaultFactory + "'" + "\n");
                    if (udcPkgType.txtValue.Trim() != "" && udcPkgType.txtValue.Trim() != "ALL")
                        strSqlString.Append("           AND H_PACKAGE " + udcPkgType.SelectedValueToQueryString + "\n");
                    if (udcLotType.txtValue.Trim() != "" && udcLotType.txtValue.Trim() != "ALL")
                        strSqlString.Append("           AND IQC_TYPE " + udcLotType.SelectedValueToQueryString + "\n");
                    strSqlString.Append("           AND TRAN_TIME BETWEEN '" + strFromDate + "' AND '" + strToDate + "' " + "\n");
                    strSqlString.Append("           AND HIST_DEL_FLAG = ' ' " + "\n");
                    strSqlString.Append("           AND MAT_VER = 1 " + "\n");
                    strSqlString.Append("       ) HIS " + "\n");
                    strSqlString.Append("     , CIQCLOTDFT DFT " + "\n");
                    strSqlString.Append("     , CIQCBATSTS BAT " + "\n");
                    strSqlString.Append("     , CWIPSITDEF DEF " + "\n");
                    strSqlString.Append("     , MWIPMATDEF MAT " + "\n");
                    strSqlString.Append("     , MWIPLOTSTS LOT  " + "\n");
                    strSqlString.Append(" WHERE 1=1 " + "\n");
                    strSqlString.Append("   AND HIS.FACTORY = DEF.FACTORY " + "\n");
                    strSqlString.Append("   AND HIS.FACTORY = DFT.FACTORY " + "\n");
                    strSqlString.Append("   AND HIS.FACTORY = MAT.FACTORY " + "\n");
                    strSqlString.Append("   AND HIS.FACTORY = BAT.FACTORY " + "\n");
                    strSqlString.Append("   AND HIS.FACTORY = LOT.FACTORY " + "\n");
                    strSqlString.Append("   AND HIS.LOT_ID = DFT.LOT_ID " + "\n");
                    strSqlString.Append("   AND HIS.MAT_ID = MAT.MAT_ID " + "\n");
                    strSqlString.Append("   AND HIS.IQC_TYPE = BAT.IQC_TYPE " + "\n");
                    strSqlString.Append("   AND HIS.IQC_NO = BAT.IQC_NO " + "\n");
                    strSqlString.Append("   AND HIS.MAT_ID = BAT.MAT_ID " + "\n");
                    strSqlString.Append("   AND HIS.MAT_VER = BAT.MAT_VER " + "\n");
                    strSqlString.Append("   AND HIS.LOT_ID = LOT.LOT_ID  " + "\n");
                    strSqlString.Append("   AND DEF.TABLE_NAME = 'MAT_DEFECT_RELATION' " + "\n");
                    strSqlString.Append("   AND DEF.KEY_1 = MAT.MAT_TYPE " + "\n");
                    strSqlString.Append("   AND DEF.KEY_2 = DFT.DEFECT_CODE " + "\n");
                    if (udcCustomer.txtValue.Trim() != "" && udcCustomer.txtValue.Trim() != "ALL")
                        strSqlString.Append("   AND MAT.MAT_GRP_1 " + udcCustomer.SelectedValueToQueryString + "\n");
                    if (udcVendor.txtValue.Trim() != "" && udcVendor.txtValue.Trim() != "ALL")
                        strSqlString.Append("   AND BAT.VENDOR " + udcVendor.SelectedValueToQueryString + "\n");
                    strSqlString.Append("   AND MAT.MAT_VER = 1 " + "\n");
                    strSqlString.Append("   AND LOT.LOT_DEL_FLAG = ' ' " + "\n");
                    strSqlString.Append(" GROUP BY MAT.FACTORY, HIS.LOT_ID, HIS.IQC_TYPE, MAT.MAT_GRP_1, HIS.LEAD_COUNT, HIS.H_PACKAGE, HIS.DENSITY, BAT.VENDOR, LOT.QTY_1, BAT.CREATE_TIME, HIS.SAMPLE_QTY, BAT.QC_OPERATOR, BAT.QC_PASS_FLAG, LOT.CREATE_QTY_1 " + "\n");
                    strSqlString.Append(" ORDER BY MAT.FACTORY, HIS.LOT_ID, HIS.IQC_TYPE, MAT.MAT_GRP_1, HIS.LEAD_COUNT, HIS.H_PACKAGE, HIS.DENSITY, BAT.VENDOR, LOT.QTY_1, BAT.CREATE_TIME, HIS.SAMPLE_QTY, BAT.QC_OPERATOR, BAT.QC_PASS_FLAG, LOT.CREATE_QTY_1 " + "\n");

                    #endregion

                    break;
                case 1: // 1. 판정현황

                    #region
                    #endregion

                    break;
                case 2: // 2. 검사현황(업체)
                    break;
                case 3: // 3. 검사현황(PKG)
                    break;
                case 4: // 4. PKG별 판정비율
                    break;
                case 5: // 5. 업체별 불량율
                    break;
                case 6: // 6. 불량종류별 현황
                    break;
                case 7: // 7. 불량종류별 비율
                    break;
            }

            if (GlobalVariable.gsUserGroup == "ADMIN_GROUP" || GlobalVariable.gsUserGroup == "HANA_ADMIN_GROUP")
            {
                System.Windows.Forms.Clipboard.SetText(strSqlString.ToString());
            }

            return strSqlString.ToString();
        }

        /// <summary>
        /// 5. Chart 생성
        /// </summary>
        /// <param name="DT">Chart를 생성할 데이터 테이블</param>
        private void MakeChart(DataTable DT)
        {

        }

        private void ShowChart()
        {
            // 차트 설정
            udcChartFX1.RPT_1_ChartInit();
            udcChartFX1.RPT_2_ClearData();

            udcChartFX1.AxisY.Font = new System.Drawing.Font("굴림체", 8, System.Drawing.FontStyle.Regular);
            udcChartFX1.AxisY2.Font = new System.Drawing.Font("굴림체", 8, System.Drawing.FontStyle.Regular);
            udcChartFX1.AxisX.Font = new System.Drawing.Font("굴림체", 8, System.Drawing.FontStyle.Regular);
            
            switch (cboChart.SelectedIndex)
            {
                case 0:
                    {
                        #region " 1번 판정현황 "

                        DataTable dt = CmnFunction.oComm.GetFuncDataTable("DYNAMIC", MakeSqlString(1));

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
                case 1:
                    {
                        #region " 2번 검사현황(업체) "

                        DataTable dt = CmnFunction.oComm.GetFuncDataTable("DYNAMIC", MakeSqlString(2));

                        if (dt == null || dt.Rows.Count < 1)
                            return;

                        udcChartFX1.DataSource = dt;
                        udcChartFX1.Gallery = SoftwareFX.ChartFX.Gallery.Bar;
                        udcChartFX1.SerLegBox = false;
                        udcChartFX1.SerLegBoxObj.Docked = SoftwareFX.ChartFX.Docked.Top;
                        udcChartFX1.LegendBox = false;
                        udcChartFX1.PointLabels = true;

                        udcChartFX1.AxisY.Max = udcChartFX1.AxisY.Max * 1.2;

                        #endregion
                    }
                    break;
                case 2:
                    {
                        #region " 3번 검사현황(PKG) "

                        DataTable dt = CmnFunction.oComm.GetFuncDataTable("DYNAMIC", MakeSqlString(3));

                        if (dt == null || dt.Rows.Count < 1)
                            return;

                        dt = GetRotatedDataTable(ref dt);

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
                case 3:
                    {
                        #region " 4번 PKG별 판정비율 "
                        DataTable dt = CmnFunction.oComm.GetFuncDataTable("DYNAMIC", MakeSqlString(4));


                        if (dt == null || dt.Rows.Count < 1)
                            return;

                        udcChartFX1.DataSource = dt;
                        udcChartFX1.Gallery = SoftwareFX.ChartFX.Gallery.Pie;
                        udcChartFX1.SerLegBox = false;
                        udcChartFX1.SerLegBoxObj.Docked = SoftwareFX.ChartFX.Docked.Top;
                        udcChartFX1.LegendBox = true;
                        udcChartFX1.PointLabels = true;

                        udcChartFX1.AxisY.Max = udcChartFX1.AxisY.Max * 1.2;

                        #endregion
                    }
                    break;
                case 4:
                    {
                        #region " 5번 업체별 불량율 "

                        DataTable dt = CmnFunction.oComm.GetFuncDataTable("DYNAMIC", MakeSqlString(5));

                        if (dt == null || dt.Rows.Count < 1)
                            return;


                        udcChartFX1.DataSource = dt;
                        udcChartFX1.Gallery = SoftwareFX.ChartFX.Gallery.Pie;
                        udcChartFX1.SerLegBox = false;
                        udcChartFX1.SerLegBoxObj.Docked = SoftwareFX.ChartFX.Docked.Top;
                        udcChartFX1.LegendBox = true;
                        udcChartFX1.PointLabels = true;

                        udcChartFX1.AxisY.Max = udcChartFX1.AxisY.Max * 1.2;

                        #endregion
                    }
                    break;
                case 5:
                    {
                        #region " 6번 불량종류별 현황 "

                        DataTable dt = CmnFunction.oComm.GetFuncDataTable("DYNAMIC", MakeSqlString(6));

                        if (dt == null || dt.Rows.Count < 1)
                            return;

                        udcChartFX1.DataSource = dt;
                        udcChartFX1.Gallery = SoftwareFX.ChartFX.Gallery.Bar;
                        udcChartFX1.SerLegBox = false;
                        udcChartFX1.SerLegBoxObj.Docked = SoftwareFX.ChartFX.Docked.Top;
                        udcChartFX1.LegendBox = false;
                        udcChartFX1.PointLabels = true;

                        udcChartFX1.AxisY.Max = udcChartFX1.AxisY.Max * 1.2;

                        #endregion
                    }
                    break;
                case 6:
                    {
                        #region " 7번 불량종류별 비율 "

                        DataTable dt = CmnFunction.oComm.GetFuncDataTable("DYNAMIC", MakeSqlString(6));

                        if (dt == null || dt.Rows.Count < 1)
                            return;

                        udcChartFX1.DataSource = dt;
                        udcChartFX1.Gallery = SoftwareFX.ChartFX.Gallery.Bar;
                        udcChartFX1.SerLegBox = false;
                        udcChartFX1.SerLegBoxObj.Docked = SoftwareFX.ChartFX.Docked.Top;
                        udcChartFX1.LegendBox = false;
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
                //cboChart.SelectedIndex = 0;
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

        #endregion

        private void cboChart_SelectedIndexChanged(object sender, EventArgs e)
        {
            ShowChart();
        }

        public DataTable GetRotatedDataTable(ref DataTable dt)
        {
            int nColToRow = 0;  // 변환된 DataTable에서 칼럼으로 쓸 dt의 칼럼 위치 

            DataTable dtNew = new DataTable();
            Object [] dr = null;

            // Series의 Type 추출
            Type type = dt.Columns[1].DataType;

            // 칼럼 추가
            dtNew.Columns.Add("GUBUN", typeof(String));
            for(int i=0; i<dt.Rows.Count; i++)
            {
                dtNew.Columns.Add(dt.Rows[i][0].ToString(), type);
            }

            // 데이터 채워넣기
            for (int j = nColToRow + 1; j < dt.Columns.Count; j++)
            {
                dr = dtNew.NewRow().ItemArray;
                dr[0] = dt.Columns[j].Caption;
                for(int i=0; i<dt.Rows.Count; i++)
                {
                    dr[i+1] = dt.Rows[i][j];
                }
                dtNew.Rows.Add(dr);
            }
            return dtNew;
        }
    }
}