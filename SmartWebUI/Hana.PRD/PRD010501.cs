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
    public partial class PRD010501 : Miracom.SmartWeb.UI.Controls.udcCUSReport002
    {
        /// <summary>
        /// 클  래  스: PRD010501<br/>
        /// 클래스요약: 설비 Arrange 제품기준<br/>
        /// 작  성  자: 미라콤 양형석<br/>
        /// 최초작성일: 2008-12-10<br/>
        /// 상세  설명: 설비 Arrange 제품기준.<br/>
        /// 변경  내용: <br/>
        /// 변  경  자: 하나마이크론 김준용<br />
        /// Excel Export 저장 기능 변경<br />
        /// </summary>
        public PRD010501()
        {
            InitializeComponent();

            SortInit();
            GridColumnInit();

            cdvDate.Value = DateTime.Today;
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

            if(cdvStep.Text.Trim() == "")
            {
                CmnFunction.ShowMsgBox(RptMessages.GetMessage("STD005", GlobalVariable.gcLanguage));
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
                spdData.RPT_AddBasicColumn("Customer", 0, 0, Visibles.True, Frozen.True, Align.Left, Merge.True, Formatter.String, 60);
                spdData.RPT_AddBasicColumn("Family", 0, 1, Visibles.False, Frozen.True, Align.Left, Merge.True, Formatter.String, 60);
                spdData.RPT_AddBasicColumn("Package", 0, 2, Visibles.False, Frozen.True, Align.Left, Merge.True, Formatter.String, 60);
                spdData.RPT_AddBasicColumn("Type1", 0, 3, Visibles.False, Frozen.True, Align.Left, Merge.True, Formatter.String, 70);
                spdData.RPT_AddBasicColumn("Type2", 0, 4, Visibles.False, Frozen.True, Align.Left, Merge.True, Formatter.String, 70);
                spdData.RPT_AddBasicColumn("LD Count", 0, 5, Visibles.False, Frozen.True, Align.Left, Merge.True, Formatter.String, 70);
                spdData.RPT_AddBasicColumn("Density", 0, 6, Visibles.False, Frozen.True, Align.Left, Merge.True, Formatter.String, 70);
                spdData.RPT_AddBasicColumn("Generation", 0, 7, Visibles.False, Frozen.True, Align.Center, Merge.True, Formatter.String, 70);
                spdData.RPT_AddBasicColumn("Product", 0, 8, Visibles.True, Frozen.True, Align.Left, Merge.True, Formatter.String, 70);
                spdData.RPT_AddBasicColumn("CAPA", 0, 9, Visibles.True, Frozen.True, Align.Center, Merge.True, Formatter.String, 70);
                spdData.RPT_AddBasicColumn("Classification", 0, 10, Visibles.True, Frozen.True, Align.Center, Merge.True, Formatter.String, 70);

                if (cdvFactory.txtValue != "")
                {
                    spdData.RPT_AddDynamicColumn(cdvStep, 0, 11, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 80);
                }

                spdData.RPT_ColumnConfigFromTable(btnSort); //Group항목이 있을경우 반드시 선언해줄것.
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
            ((udcTableForm)(this.btnSort.BindingForm)).AddRow("Customer", "DATA_1 as Customer", "DATA_1", "GCM.DATA_1", true);
            ((udcTableForm)(this.btnSort.BindingForm)).AddRow("Family", "MAT_GRP_2 as Family", "MAT_GRP_2", "MAT.MAT_GRP_2", false);
            ((udcTableForm)(this.btnSort.BindingForm)).AddRow("Package", "MAT_GRP_3 as Package", "MAT_GRP_3", "MAT.MAT_GRP_3", false);
            ((udcTableForm)(this.btnSort.BindingForm)).AddRow("Type1", "MAT_GRP_4 as Type1", "MAT_GRP_4", "MAT.MAT_GRP_4", false);
            ((udcTableForm)(this.btnSort.BindingForm)).AddRow("Type2", "MAT_GRP_5 as Type2", "MAT_GRP_5", "MAT.MAT_GRP_5", false);
            ((udcTableForm)(this.btnSort.BindingForm)).AddRow("LD Count", "MAT_GRP_6 as LD_Count", "MAT_GRP_6", "MAT.MAT_GRP_6", false);
            ((udcTableForm)(this.btnSort.BindingForm)).AddRow("Density", "MAT_GRP_7 as Density", "MAT_GRP_7", "MAT.MAT_GRP_7", false);
            ((udcTableForm)(this.btnSort.BindingForm)).AddRow("Generation", "MAT_GRP_8 as Generation", "MAT_GRP_8", "MAT.MAT_GRP_8", false);
            ((udcTableForm)(this.btnSort.BindingForm)).AddRow("Product", "MAT.MAT_ID as Product", "MAT_ID", "MAT.MAT_ID", true);
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
            string QueryCond3 = null;

            udcTableForm tableForm = (udcTableForm)btnSort.BindingForm;

            QueryCond1 = tableForm.SelectedValueToQueryContainNull;
            QueryCond2 = tableForm.SelectedValue2ToQueryContainNull;
            QueryCond3 = tableForm.SelectedValue3ToQueryContainNull;

            string strSelect = string.Empty;
            string strDecode = string.Empty;

            string strDate = cdvDate.Value.ToString("yyyyMMdd");
            string strPrevDate;
            if (DateTime.Now.ToString("yyyyMMdd").Equals(strDate)) // 오늘 날짜
            {
                strDate = DateTime.Now.ToString("yyyyMMddHHmmss");
                strPrevDate = DateTime.Now.AddDays(-1).ToString("yyyyMMdd") + "220000";

                strSelect += cdvStep.getRepeatQuery("SUM(WIP", ") AS ", "WIP").Replace(", SUM", "        , SUM");
                strSelect += cdvStep.getRepeatQuery("SUM(CNT", ") AS ", "CNT").Replace(", SUM", "        , SUM");
                strSelect += cdvStep.getRepeatQuery("TRUNC(AVG(CAPA", ")) AS  ", "CAPA").Replace(", TRUNC", "        , TRUNC");

                strDecode += cdvStep.getDecodeQuery("DECODE(RES.OPER_GRP_1 ", " RES.WIP, 0)", "WIP").Replace(", DECODE", "                   , DECODE");
                strDecode += cdvStep.getDecodeQuery("DECODE(RES.OPER_GRP_1 ", " RES.EQP_CNT, 0)", "CNT").Replace(", DECODE", "                   , DECODE");
                strDecode += cdvStep.getDecodeQuery("DECODE(RES.OPER_GRP_1 ", " TRUNC(NVL(NVL(UPH.UPEH,0) * 22.5 * 0.7 * RES.EQP_CNT, 0)), 0)", "CAPA").Replace(", DECODE", "                   , DECODE");

                strSqlString.AppendFormat("SELECT  {0}  " + "\n", QueryCond1);
                strSqlString.Append("        , ' ' AS CAPA " + "\n");
                strSqlString.Append("        , ' ' AS Gubun " + "\n");
                strSqlString.Append(strSelect);
                strSqlString.Append("FROM    (         " + "\n");
                strSqlString.Append("            SELECT RES.FACTORY " + "\n");
                strSqlString.Append("                   , RES.MAT_ID " + "\n");
                strSqlString.Append(strDecode);
                strSqlString.Append("            FROM  " + "\n");
                strSqlString.Append("               ( SELECT LTH.FACTORY, LTH.MAT_ID, OPR.OPER_GRP_1, OPR.OPER, DEF.RES_GRP_6 RES_MODEL, DEF.RES_GRP_7 UPEH_GRP, COUNT(*) EQP_CNT, SUM(LTH.QTY_1) WIP " + "\n");
                strSqlString.Append("                 FROM   MRASRESLTH LTH " + "\n");
                strSqlString.Append("                        , MRASRESDEF DEF " + "\n");
                strSqlString.Append("                        , MWIPOPRDEF OPR " + "\n");
                strSqlString.Append("                 WHERE 1 = 1  " + "\n");
                strSqlString.Append("                       AND LTH.FACTORY " + cdvFactory.SelectedValueToQueryString + "\n");
                if (cdvStep.Text != "ALL")
                    strSqlString.Append("                       AND OPR.OPER_GRP_1 " + cdvStep.SelectedValueToQueryString + "\n");
                if (txtSearchProduct.Text.Trim() != "%" && txtSearchProduct.Text.Trim() != "")
                    strSqlString.Append("                       AND LTH.MAT_ID LIKE '" + txtSearchProduct.Text + "'" + "\n");
                if (cdvFactory.Text == GlobalVariable.gsAssyDefaultFactory)
                    strSqlString.Append("                       AND LTH.OPER NOT IN ('00001','00002') " + "\n");
                strSqlString.Append("                       AND LTH.RES_HIST_DEL_FLAG = ' ' " + "\n");
                strSqlString.Append("                       AND LTH.TRAN_TIME BETWEEN '" + strPrevDate + "' AND '" + strDate + "' " + "\n");
                strSqlString.Append("                       AND LTH.FACTORY = DEF.FACTORY " + "\n");
                strSqlString.Append("                       AND LTH.FACTORY = OPR.FACTORY " + "\n");
                strSqlString.Append("                       AND LTH.OPER = OPR.OPER " + "\n");
                strSqlString.Append("                       AND LTH.RES_ID = DEF.RES_ID    " + "\n");
                strSqlString.Append("                 GROUP BY LTH.FACTORY, LTH.MAT_ID, OPR.OPER_GRP_1, OPR.OPER, DEF.RES_GRP_6, DEF.RES_GRP_7 " + "\n");
                strSqlString.Append("               ) RES " + "\n");
                strSqlString.Append("                 , CRASUPHDEF UPH " + "\n");
                strSqlString.Append("            WHERE 1 = 1 " + "\n");
                strSqlString.Append("                  AND RES.FACTORY = UPH.FACTORY(+) " + "\n");
                strSqlString.Append("                  AND RES.OPER = UPH.OPER(+) " + "\n");
                strSqlString.Append("                  AND RES.RES_MODEL = UPH.RES_MODEL(+) " + "\n");
                strSqlString.Append("                  AND RES.UPEH_GRP = UPH.UPEH_GRP(+) " + "\n");
                strSqlString.Append("                  AND RES.MAT_ID = UPH.MAT_ID(+) " + "\n");
                strSqlString.Append("        ) RES " + "\n");
                strSqlString.Append("        , MWIPMATDEF MAT " + "\n");
                strSqlString.Append("        , MGCMTBLDAT GCM " + "\n");
                strSqlString.Append("WHERE   1 = 1      " + "\n");
                strSqlString.Append("        AND GCM.TABLE_NAME = 'H_CUSTOMER'     " + "\n");
                strSqlString.Append("        AND RES.FACTORY = MAT.FACTORY     " + "\n");
                strSqlString.Append("        AND MAT.FACTORY = GCM.FACTORY     " + "\n");
                strSqlString.Append("        AND RES.MAT_ID = MAT.MAT_ID      " + "\n");
                strSqlString.Append("        AND MAT.MAT_GRP_1 = GCM.KEY_1  " + "\n");

                #region 상세 조회에 따른 SQL문 생성
                if (udcWIPCondition1.Text != "ALL" && udcWIPCondition1.Text != "")
                    strSqlString.AppendFormat("   AND MAT.MAT_GRP_1 {0} " + "\n", udcWIPCondition1.SelectedValueToQueryString);

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
                #endregion

                strSqlString.AppendFormat("GROUP BY {0} " + "\n", QueryCond3);
                strSqlString.AppendFormat("ORDER BY {0} " + "\n", QueryCond3);


            }
            else // 과거 날짜
            {
                strSelect += cdvStep.getRepeatQuery("SUM(WIP", ") AS ", "WIP").Replace(", SUM", "        , SUM");
                strSelect += cdvStep.getRepeatQuery("SUM(CNT", ") AS ", "CNT").Replace(", SUM", "        , SUM");
                strSelect += cdvStep.getRepeatQuery("TRUNC(AVG(CAPA", ")) AS  ", "CAPA").Replace(", TRUNC", "        , TRUNC");

                strDecode += cdvStep.getDecodeQuery("DECODE(OPR.OPER_GRP_1 ", " CAP.WIP, 0)", "WIP").Replace(", SUM", "                    , SUM");
                strDecode += cdvStep.getDecodeQuery("DECODE(OPR.OPER_GRP_1 ", " CAP.EQP_CNT, 0)", "CNT").Replace(", SUM", "                    , SUM");
                strDecode += cdvStep.getDecodeQuery("DECODE(OPR.OPER_GRP_1 ", " CAP.CAPA, 0)", "CAPA").Replace(", SUM", "                    , SUM");

                strSqlString.AppendFormat("SELECT  {0}  " + "\n", QueryCond1);
                strSqlString.Append("        , ' ' AS CAPA " + "\n");
                strSqlString.Append("        , ' ' AS Gubun " + "\n");
                strSqlString.AppendFormat("{0} " + "\n", strSelect);
                strSqlString.Append("FROM    (         " + "\n");
                strSqlString.Append("            SELECT  CAP.FACTORY " + "\n");
                strSqlString.Append("                    , CAP.MAT_ID " + "\n");
                strSqlString.AppendFormat("{0} " + "\n", strDecode);
                strSqlString.Append("            FROM    RSUMRESCAP CAP, " + "\n");
                strSqlString.Append("                    MWIPOPRDEF OPR " + "\n");
                strSqlString.Append("            WHERE   1=1 " + "\n");
                strSqlString.Append("                    AND CAP.FACTORY = OPR.FACTORY " + "\n");
                strSqlString.Append("                    AND CAP.OPER = OPR.OPER " + "\n");
                strSqlString.Append("                    AND CAP.CUTOFF_DT = '" + strDate + "' " + "\n");
                strSqlString.Append("                    AND CAP.FACTORY " + cdvFactory.SelectedValueToQueryString + "\n");
                if (cdvStep.Text != "ALL")
                    strSqlString.Append("                    AND OPR.OPER_GRP_1 " + cdvStep.SelectedValueToQueryString + "\n");
                if (txtSearchProduct.Text.Trim() != "%" && txtSearchProduct.Text.Trim() != "")
                    strSqlString.Append("                    AND CAP.MAT_ID LIKE '" + txtSearchProduct.Text + "'" + "\n");
                if (cdvFactory.Text == GlobalVariable.gsAssyDefaultFactory)
                    strSqlString.Append("                    AND CAP.OPER NOT IN ('00001','00002') " + "\n");
                strSqlString.Append("        ) CAP " + "\n");
                strSqlString.Append("        , MWIPMATDEF MAT " + "\n");
                strSqlString.Append("        , MGCMTBLDAT GCM " + "\n");
                strSqlString.Append("WHERE   1=1 " + "\n");
                strSqlString.Append("        AND CAP.FACTORY = MAT.FACTORY " + "\n");
                strSqlString.Append("        AND MAT.FACTORY = GCM.FACTORY " + "\n");
                strSqlString.Append("        AND CAP.MAT_ID = MAT.MAT_ID " + "\n");
                strSqlString.Append("        AND GCM.TABLE_NAME = 'H_CUSTOMER'   " + "\n");
                strSqlString.Append("        AND MAT.MAT_GRP_1 = GCM.KEY_1  " + "\n");

                #region 상세 조회에 따른 SQL문 생성
                if (udcWIPCondition1.Text != "ALL" && udcWIPCondition1.Text != "")
                    strSqlString.AppendFormat("   AND MAT.MAT_GRP_1 {0} " + "\n", udcWIPCondition1.SelectedValueToQueryString);

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
                #endregion

                strSqlString.AppendFormat("GROUP BY {0} " + "\n", QueryCond3);
                strSqlString.AppendFormat("ORDER BY {0} " + "\n", QueryCond3);
            }

            System.Windows.Forms.Clipboard.SetText(strSqlString.ToString());
            return strSqlString.ToString();
        }

        /// <summary>
        /// 5. Chart 생성
        /// </summary>
        /// <param name="DT">Chart를 생성할 데이터 테이블</param>
        private void MakeChart(DataTable DT)
        {

        }

        #endregion

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

                //by John
                //1.Griid 합계 표시
                int[] rowType = spdData.RPT_DataBindingWithSubTotalAndDivideRows(dt, 0, sub+1, 10, 11, 3, cdvStep.SelectCount, btnSort);
                //spdData.DataSource = dt;

                //2. 칼럼 고정(필요하다면..)
                //spdData.Sheets[0].FrozenColumnCount = 10;

                //3. Total부분 셀머지
                spdData.RPT_FillDataSelectiveCells("Total", 0, 10, 0, 3, true, Align.Center, VerticalAlign.Center);


                //4. Column Auto Fit
                spdData.RPT_AutoFit(false);

                //5. 값이 없는 칼럼 삭제
                spdData.RPT_RemoveZeroColumn(11, 3);

                spdData.RPT_FillColumnData(10, new string[] { "WIP", "CNT", "CAPA" });

                // Min CAPA 항목 채워넣기
                FillMinCapa();

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

        /// <summary>
        /// 스프레드를 클릭했을 때 해당 공정 그룹의 Arrange현황을 팝업창을 띄워 보여줍니다.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void spdData_CellClick(object sender, FarPoint.Win.Spread.CellClickEventArgs e)
        {
            if (e.Column < 11)
                return;

            string strStepName = spdData.ActiveSheet.Columns[e.Column].Label;

            //this._NewFormWithArgument("PRD010502", new object[] { strStepName });

            DataTable dt = CmnFunction.oComm.GetFuncDataTable("DYNAMIC", MakeSqlStringForPopup(strStepName));


            if (dt != null && dt.Rows.Count > 0)
            {
                System.Windows.Forms.Form frm = new PRD010501_P1(strStepName, dt);
                frm.ShowDialog();
            }
        }

        /// <summary>
        /// 공정그룹의 설비 Arrange현황을 구하기 위한 쿼리를 생성하는 메서드
        /// </summary>
        /// <param name="strStep">공정그룹의 이름</param>
        /// <returns>쿼리</returns>
        private string MakeSqlStringForPopup(string strStep)
        {
            if (strStep == null || strStep.Equals(string.Empty))
                throw new ArgumentException(RptMessages.GetMessage("STD100", GlobalVariable.gcLanguage));

            StringBuilder strSqlString = new StringBuilder();

            string QueryCond1 = null;
            string QueryCond2 = null;
            string QueryCond3 = null;

            udcTableForm tableForm = (udcTableForm)btnSort.BindingForm;

            QueryCond1 = tableForm.SelectedValueToQueryContainNull;
            QueryCond2 = tableForm.SelectedValue2ToQueryContainNull;
            QueryCond3 = tableForm.SelectedValue3ToQueryContainNull;

            string strSelect = string.Empty;
            string strDecode = string.Empty;

            string strDate = cdvDate.Value.ToString("yyyyMMdd");
            string strPrevDate;
            if (DateTime.Now.ToString("yyyyMMdd").Equals(strDate)) // 오늘 날짜
            {
                strDate = DateTime.Now.ToString("yyyyMMddHHmmss");
                strPrevDate = DateTime.Now.AddDays(-1).ToString("yyyyMMdd") + "220000";


                strSqlString.Append("SELECT  GCM.DATA_1, MAT.MAT_ID  " + "\n");
                strSqlString.Append("        , RES.RES_MODEL AS MODEL " + "\n");
                strSqlString.Append("        , RES.EQP_CNT AS CNT" + "\n");
                strSqlString.Append("        , TRUNC(NVL(NVL(UPH.UPEH,0) * 22.5 * 0.7 * RES.EQP_CNT, 0)) AS CAPA  " + "\n");
                strSqlString.Append("FROM    (   " + "\n");
                strSqlString.Append("             SELECT  LTH.FACTORY  " + "\n");
                strSqlString.Append("                     , LTH.MAT_ID  " + "\n");
                strSqlString.Append("                     , LTH.OPER  " + "\n");
                strSqlString.Append("                     , DEF.RES_GRP_6 AS RES_MODEL  " + "\n");
                strSqlString.Append("                     , DEF.RES_GRP_7 AS UPEH_GRP  " + "\n");
                strSqlString.Append("                     , COUNT(*) AS EQP_CNT  " + "\n");
                strSqlString.Append("             FROM    MRASRESLTH LTH,   " + "\n");
                strSqlString.Append("                     MRASRESDEF DEF  " + "\n");
                strSqlString.Append("             WHERE   1 = 1   " + "\n");
                strSqlString.Append("                     AND LTH.FACTORY " + cdvFactory.SelectedValueToQueryString + "\n");
                if (cdvFactory.Text == GlobalVariable.gsAssyDefaultFactory)
                    strSqlString.Append("                     AND LTH.OPER NOT IN ('00001','00002') " + "\n");
                if (txtSearchProduct.Text.Trim() != "%" && txtSearchProduct.Text.Trim() != "")
                    strSqlString.Append("                     AND LTH.MAT_ID LIKE '" + txtSearchProduct.Text + "'" + "\n");
                strSqlString.Append("                     AND LTH.RES_HIST_DEL_FLAG = ' '  " + "\n");
                strSqlString.Append("                     AND LTH.TRAN_TIME BETWEEN '" + strPrevDate + "' AND '" + strDate + "' " + "\n");
                strSqlString.Append("                     AND LTH.FACTORY = DEF.FACTORY  " + "\n");
                strSqlString.Append("                     AND LTH.RES_ID = DEF.RES_ID     " + "\n");
                strSqlString.Append("             GROUP BY LTH.FACTORY, LTH.MAT_ID, LTH.OPER, DEF.RES_GRP_6, DEF.RES_GRP_7  " + "\n");
                strSqlString.Append("        ) RES,  " + "\n");
                strSqlString.Append("        CRASUPHDEF UPH, " + "\n");
                strSqlString.Append("        MWIPOPRDEF OPR,  " + "\n");
                strSqlString.Append("        MWIPMATDEF MAT,  " + "\n");
                strSqlString.Append("        MGCMTBLDAT GCM  " + "\n");
                strSqlString.Append("WHERE   1 = 1  " + "\n");
                strSqlString.Append("        AND RES.FACTORY = UPH.FACTORY(+)  " + "\n");
                strSqlString.Append("        AND RES.OPER = UPH.OPER(+)  " + "\n");
                strSqlString.Append("        AND RES.RES_MODEL = UPH.RES_MODEL(+)  " + "\n");
                strSqlString.Append("        AND RES.UPEH_GRP = UPH.UPEH_GRP(+)  " + "\n");
                strSqlString.Append("        AND RES.MAT_ID = UPH.MAT_ID(+)  " + "\n");
                strSqlString.Append("        AND RES.FACTORY = OPR.FACTORY " + "\n");
                strSqlString.Append("        AND OPR.FACTORY = MAT.FACTORY     " + "\n");
                strSqlString.Append("        AND MAT.FACTORY = GCM.FACTORY " + "\n");
                strSqlString.Append("        AND RES.OPER = OPR.OPER     " + "\n");
                strSqlString.Append("        AND RES.MAT_ID = MAT.MAT_ID      " + "\n");
                strSqlString.Append("        AND MAT.MAT_GRP_1 = GCM.KEY_1  " + "\n");
                strSqlString.Append("        AND OPR.OPER_GRP_1 = '" + strStep + "' " + "\n");
                strSqlString.Append("        AND GCM.TABLE_NAME = 'H_CUSTOMER' " + "\n");

                #region 상세 조회에 따른 SQL문 생성
                if (udcWIPCondition1.Text != "ALL" && udcWIPCondition1.Text != "")
                    strSqlString.AppendFormat("        AND MAT.MAT_GRP_1 {0} " + "\n", udcWIPCondition1.SelectedValueToQueryString);

                if (udcWIPCondition2.Text != "ALL" && udcWIPCondition2.Text != "")
                    strSqlString.AppendFormat("        AND MAT.MAT_GRP_2 {0} " + "\n", udcWIPCondition2.SelectedValueToQueryString);

                if (udcWIPCondition3.Text != "ALL" && udcWIPCondition3.Text != "")
                    strSqlString.AppendFormat("        AND MAT.MAT_GRP_3 {0} " + "\n", udcWIPCondition3.SelectedValueToQueryString);

                if (udcWIPCondition4.Text != "ALL" && udcWIPCondition4.Text != "")
                    strSqlString.AppendFormat("        AND MAT.MAT_GRP_4 {0} " + "\n", udcWIPCondition4.SelectedValueToQueryString);

                if (udcWIPCondition5.Text != "ALL" && udcWIPCondition5.Text != "")
                    strSqlString.AppendFormat("        AND MAT.MAT_GRP_5 {0} " + "\n", udcWIPCondition5.SelectedValueToQueryString);

                if (udcWIPCondition6.Text != "ALL" && udcWIPCondition6.Text != "")
                    strSqlString.AppendFormat("        AND MAT.MAT_GRP_6 {0} " + "\n", udcWIPCondition6.SelectedValueToQueryString);

                if (udcWIPCondition7.Text != "ALL" && udcWIPCondition7.Text != "")
                    strSqlString.AppendFormat("        AND MAT.MAT_GRP_7 {0} " + "\n", udcWIPCondition7.SelectedValueToQueryString);

                if (udcWIPCondition8.Text != "ALL" && udcWIPCondition8.Text != "")
                    strSqlString.AppendFormat("        AND MAT.MAT_GRP_8 {0} " + "\n", udcWIPCondition8.SelectedValueToQueryString);

                if (udcWIPCondition9.Text != "ALL" && udcWIPCondition9.Text != "")
                    strSqlString.AppendFormat("        AND MAT.MAT_GRP_9 {0} " + "\n", udcWIPCondition9.SelectedValueToQueryString);
                #endregion

                strSqlString.Append("ORDER BY GCM.DATA_1, MAT.MAT_ID  " + "\n");

            }
            else // 과거 날짜
            {
                strSqlString.Append("SELECT  DATA_1 as Customer, MAT.MAT_ID as Product   " + "\n");
                strSqlString.Append("        , MODEL, SUM(CNT), AVG(CAPA)" + "\n");
                strSqlString.Append("FROM    (   " + "\n");
                strSqlString.Append("            SELECT    " + "\n");
                strSqlString.Append("                    GCM.DATA_1, MAT.MAT_ID" + "\n");
                strSqlString.Append("                    , CAP.RES_MODEL AS MODEL " + "\n");
                strSqlString.Append("                    , CAP.EQP_CNT AS CNT  " + "\n");
                strSqlString.Append("                    , CAP.CAPA AS CAPA  " + "\n");
                strSqlString.Append("            FROM    RSUMRESCAP CAP, " + "\n");
                strSqlString.Append("                    MWIPOPRDEF OPR,    " + "\n");
                strSqlString.Append("                    MWIPMATDEF MAT,    " + "\n");
                strSqlString.Append("                    MGCMTBLDAT GCM    " + "\n");
                strSqlString.Append("            WHERE   1=1    " + "\n");
                strSqlString.Append("                    AND CAP.FACTORY = OPR.FACTORY " + "\n");
                strSqlString.Append("                    AND OPR.FACTORY = MAT.FACTORY    " + "\n");
                strSqlString.Append("                    AND MAT.FACTORY = GCM.FACTORY   " + "\n");
                strSqlString.Append("                    AND CAP.OPER = OPR.OPER " + "\n");
                strSqlString.Append("                    AND CAP.MAT_ID = MAT.MAT_ID     " + "\n");
                strSqlString.Append("                    AND MAT.MAT_GRP_1 = GCM.KEY_1   " + "\n");
                strSqlString.Append("                    AND CAP.CUTOFF_DT  = '" + strDate + "' " + "\n");
                strSqlString.Append("                    AND GCM.TABLE_NAME = 'H_CUSTOMER'    " + "\n");
                strSqlString.Append("                    AND CAP.FACTORY " + cdvFactory.SelectedValueToQueryString + "\n");
                if (txtSearchProduct.Text.Trim() != "%" && txtSearchProduct.Text.Trim() != "")
                    strSqlString.Append("                    AND CAP.MAT_ID LIKE '" + txtSearchProduct.Text + "'" + "\n");
                strSqlString.Append("                    AND OPR.OPER_GRP_1 = '" + strStep + "' " + "\n");
                if (cdvFactory.Text == GlobalVariable.gsAssyDefaultFactory)
                    strSqlString.Append("                    AND CAP.OPER NOT IN ('00001','00002') " + "\n");

                #region 상세 조회에 따른 SQL문 생성
                if (udcWIPCondition1.Text != "ALL" && udcWIPCondition1.Text != "")
                    strSqlString.AppendFormat("                    AND MAT.MAT_GRP_1 {0} " + "\n", udcWIPCondition1.SelectedValueToQueryString);

                if (udcWIPCondition2.Text != "ALL" && udcWIPCondition2.Text != "")
                    strSqlString.AppendFormat("                    AND MAT.MAT_GRP_2 {0} " + "\n", udcWIPCondition2.SelectedValueToQueryString);

                if (udcWIPCondition3.Text != "ALL" && udcWIPCondition3.Text != "")
                    strSqlString.AppendFormat("                    AND MAT.MAT_GRP_3 {0} " + "\n", udcWIPCondition3.SelectedValueToQueryString);

                if (udcWIPCondition4.Text != "ALL" && udcWIPCondition4.Text != "")
                    strSqlString.AppendFormat("                    AND MAT.MAT_GRP_4 {0} " + "\n", udcWIPCondition4.SelectedValueToQueryString);

                if (udcWIPCondition5.Text != "ALL" && udcWIPCondition5.Text != "")
                    strSqlString.AppendFormat("                    AND MAT.MAT_GRP_5 {0} " + "\n", udcWIPCondition5.SelectedValueToQueryString);

                if (udcWIPCondition6.Text != "ALL" && udcWIPCondition6.Text != "")
                    strSqlString.AppendFormat("                    AND MAT.MAT_GRP_6 {0} " + "\n", udcWIPCondition6.SelectedValueToQueryString);

                if (udcWIPCondition7.Text != "ALL" && udcWIPCondition7.Text != "")
                    strSqlString.AppendFormat("                    AND MAT.MAT_GRP_7 {0} " + "\n", udcWIPCondition7.SelectedValueToQueryString);

                if (udcWIPCondition8.Text != "ALL" && udcWIPCondition8.Text != "")
                    strSqlString.AppendFormat("                    AND MAT.MAT_GRP_8 {0} " + "\n", udcWIPCondition8.SelectedValueToQueryString);

                if (udcWIPCondition9.Text != "ALL" && udcWIPCondition9.Text != "")
                    strSqlString.AppendFormat("                    AND MAT.MAT_GRP_9 {0} " + "\n", udcWIPCondition9.SelectedValueToQueryString);
                #endregion

                strSqlString.Append("        )  MAT " + "\n");
                strSqlString.AppendFormat("GROUP BY DATA_1, MAT_ID, MODEL " + "\n", QueryCond2);
                strSqlString.AppendFormat("ORDER BY DATA_1, MAT_ID, MODEL " + "\n", QueryCond2);
            }

            if (GlobalVariable.gsUserGroup == "ADMIN_GROUP" || GlobalVariable.gsUserGroup == "HANA_ADMIN_GROUP")
            {
                System.Windows.Forms.Clipboard.SetText(strSqlString.ToString());
            }

            return strSqlString.ToString();
        }

        /// <summary>
        /// Product 별로 MIN CAPA를 찾아서 스프레드에 넣어주는 메서드
        /// </summary>
        private void FillMinCapa()
        {
            if (spdData.ActiveSheet.Rows.Count < 1)
                return;

            int nStartRowPos = 0;           // 비교할 값이 시작되는 행 위치
            int nStartColPos = 11;          // 비교할 값이 시작되는 열 위치
            int nRowStep = 3;               // 피봇으로 묶이는 행의 개수
            int nCapaRowPos = 2;            // 피봇으로 묶이는 행들 중에서 CAPA행의 위치
            int nMinCapaColPos = 9;         // 비교된 후 최소값이 저장될 칼럼 위치
            int nSubTotalColPos = -1;        // 서브토탈까지 비교할 경우 -1, 아니면 서브토탈의 가장 깊은 단계의 칼럼 위치
            int minValue = int.MaxValue;
            int cellValue = 0;
            string minCapaOperGrp = string.Empty;
            bool isSubTotalRow = false;

            FarPoint.Win.Spread.CellType.TextCellType ct = new FarPoint.Win.Spread.CellType.TextCellType();
            ct.Multiline = true;

            for (int i = nStartRowPos + nCapaRowPos; i < spdData.ActiveSheet.Rows.Count; i += nRowStep)
            {
                // 서브토탈 행인지 체크
                for (int s = nSubTotalColPos; s >= 0; s--)
                {
                    if(spdData.ActiveSheet.Cells[i, s].Value.ToString().IndexOf("Sub Total") > -1)
                    {
                        isSubTotalRow = true;
                        break;
                    }
                }
                if (isSubTotalRow)
                {
                    isSubTotalRow = false;
                    continue;
                }
             
                // 최소 Capa 구하기
                minValue = int.MaxValue;
                minCapaOperGrp = string.Empty;
                for (int j = nStartColPos; j < spdData.ActiveSheet.Columns.Count; j++)
                {
                    cellValue = Convert.ToInt32(spdData.ActiveSheet.Cells[i, j].Value);
                    if (!cellValue.GetType().Equals(Type.GetType("string"))
                        && cellValue != 0
                        && cellValue < minValue)
                    {
                        minCapaOperGrp = spdData.ActiveSheet.Columns[j].Label;
                        minValue = cellValue;
                    }
                }

                if (!minValue.Equals(int.MaxValue))
                {
                    // 최소 Capa 값 넣기
                    for (int k = i - nRowStep + 1; k <= i; k++)
                    {
                        spdData.ActiveSheet.Cells[k, nMinCapaColPos].CellType = ct;
                        spdData.ActiveSheet.Cells[k, nMinCapaColPos].ForeColor = Color.Red;
                        
                        spdData.ActiveSheet.Cells[k, nMinCapaColPos].Value = string.Format("{0}\n{1:n0}", minCapaOperGrp, minValue);
                    }
                }
            }
        }

        private void cdvFactory_ValueSelectedItemChanged(object sender, MCCodeViewSelChanged_EventArgs e)
        {
            this.SetFactory(cdvFactory.txtValue);
            cdvStep.sFactory = cdvFactory.txtValue;
        }
    }
}
