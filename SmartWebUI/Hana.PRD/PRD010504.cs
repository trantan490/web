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
    public partial class PRD010504 : Miracom.SmartWeb.UI.Controls.udcCUSReport002
    {
        /// <summary>
        /// 클  래  스: PRD010504<br/>
        /// 클래스요약: 설비 Arrange Trend<br/>
        /// 작  성  자: 하나마이크론 임종우<br/>
        /// 최초작성일: 2017-05-10<br/>
        /// 상세  설명: 설비 Arrange Trend (최인남상무 요청)<br/> 
        /// 변경  내용: <br/>
        /// 2017-05-12-임종우 : 시간 기준 변경 - 전일 23시 ~ 금일 22시 (최인남상무 요청)
        /// </summary>
        public PRD010504()
        {
            InitializeComponent();

            cdvDate.Value = DateTime.Today;
            SortInit();
            GridColumnInit();
        }


        #region 유효성검사 및 초기화
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
            spdData.RPT_AddBasicColumn("Block", 0, 0, Visibles.True, Frozen.True, Align.Center, Merge.True, Formatter.String, 80);
            spdData.RPT_AddBasicColumn("Model", 0, 1, Visibles.True, Frozen.True, Align.Center, Merge.True, Formatter.String, 80);
            spdData.RPT_AddBasicColumn("Customer", 0, 2, Visibles.True, Frozen.True, Align.Left, Merge.True, Formatter.String, 60);
            spdData.RPT_AddBasicColumn("Family", 0, 3, Visibles.False, Frozen.True, Align.Left, Merge.True, Formatter.String, 60);
            spdData.RPT_AddBasicColumn("Package", 0, 4, Visibles.False, Frozen.True, Align.Left, Merge.True, Formatter.String, 60);
            spdData.RPT_AddBasicColumn("Type1", 0, 5, Visibles.False, Frozen.True, Align.Left, Merge.True, Formatter.String, 70);
            spdData.RPT_AddBasicColumn("Type2", 0, 6, Visibles.False, Frozen.True, Align.Left, Merge.True, Formatter.String, 70);
            spdData.RPT_AddBasicColumn("LD Count", 0, 7, Visibles.False, Frozen.True, Align.Left, Merge.True, Formatter.String, 70);
            spdData.RPT_AddBasicColumn("Density", 0, 8, Visibles.False, Frozen.True, Align.Left, Merge.True, Formatter.String, 70);
            spdData.RPT_AddBasicColumn("Generation", 0, 9, Visibles.False, Frozen.True, Align.Center, Merge.True, Formatter.String, 70);
            spdData.RPT_AddBasicColumn("Pin Type", 0, 10, Visibles.True, Frozen.True, Align.Center, Merge.True, Formatter.String, 70);
            spdData.RPT_AddBasicColumn("Step", 0, 11, Visibles.True, Frozen.True, Align.Center, Merge.True, Formatter.String, 70);
            spdData.RPT_AddBasicColumn("Product", 0, 12, Visibles.True, Frozen.True, Align.Left, Merge.True, Formatter.String, 80);
            spdData.RPT_AddBasicColumn("Cust Device", 0, 13, Visibles.False, Frozen.True, Align.Left, Merge.True, Formatter.String, 80);
            spdData.RPT_AddBasicColumn("구분", 0, 14, Visibles.True, Frozen.True, Align.Center, Merge.False, Formatter.String, 70);

            spdData.RPT_AddBasicColumn("23시", 0, 15, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 70);
            spdData.RPT_AddBasicColumn("00시", 0, 16, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 70);
            spdData.RPT_AddBasicColumn("01시", 0, 17, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 70);
            spdData.RPT_AddBasicColumn("02시", 0, 18, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 70);
            spdData.RPT_AddBasicColumn("03시", 0, 19, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 70);
            spdData.RPT_AddBasicColumn("04시", 0, 20, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 70);
            spdData.RPT_AddBasicColumn("05시", 0, 21, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 70);
            spdData.RPT_AddBasicColumn("06시", 0, 22, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 70);
            spdData.RPT_AddBasicColumn("07시", 0, 23, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 70);
            spdData.RPT_AddBasicColumn("08시", 0, 24, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 70);
            spdData.RPT_AddBasicColumn("09시", 0, 25, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 70);
            spdData.RPT_AddBasicColumn("10시", 0, 26, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 70);
            spdData.RPT_AddBasicColumn("11시", 0, 27, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 70);
            spdData.RPT_AddBasicColumn("12시", 0, 28, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 70);
            spdData.RPT_AddBasicColumn("13시", 0, 29, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 70);
            spdData.RPT_AddBasicColumn("14시", 0, 30, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 70);
            spdData.RPT_AddBasicColumn("15시", 0, 31, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 70);
            spdData.RPT_AddBasicColumn("16시", 0, 32, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 70);
            spdData.RPT_AddBasicColumn("17시", 0, 33, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 70);
            spdData.RPT_AddBasicColumn("18시", 0, 34, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 70);
            spdData.RPT_AddBasicColumn("19시", 0, 35, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 70);
            spdData.RPT_AddBasicColumn("20시", 0, 36, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 70);
            spdData.RPT_AddBasicColumn("21시", 0, 37, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 70);
            spdData.RPT_AddBasicColumn("22시", 0, 38, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 70);
            

            spdData.RPT_ColumnConfigFromTable(btnSort); //Group항목이 있을경우 반드시 선언해줄것.            
        }

        /// <summary>
        /// 3. Group By 정의
        /// </summary>
        private void SortInit()
        {
            ((udcTableForm)(this.btnSort.BindingForm)).Clear();
            ((udcTableForm)(this.btnSort.BindingForm)).AddRow("Block", "NVL(A.SUB_AREA_ID,'-') AS Block", "A.SUB_AREA_ID", true);
            ((udcTableForm)(this.btnSort.BindingForm)).AddRow("Model", "NVL(A.RES_MODEL,'-') AS Model", "A.RES_MODEL", true);
            ((udcTableForm)(this.btnSort.BindingForm)).AddRow("Customer", "NVL((SELECT DATA_1 FROM MGCMTBLDAT WHERE FACTORY = B.FACTORY AND TABLE_NAME = 'H_CUSTOMER' AND ROWNUM=1 AND KEY_1 = B.MAT_GRP_1), '-') AS Customer", "B.MAT_GRP_1", true);
            ((udcTableForm)(this.btnSort.BindingForm)).AddRow("Family", "B.MAT_GRP_2 AS Family", "MAT_GRP_2", false);
            ((udcTableForm)(this.btnSort.BindingForm)).AddRow("Package", "B.MAT_GRP_3 AS Package", "MAT_GRP_3", false);
            ((udcTableForm)(this.btnSort.BindingForm)).AddRow("Type1", "B.MAT_GRP_4 AS Type1", "MAT_GRP_4", false);
            ((udcTableForm)(this.btnSort.BindingForm)).AddRow("Type2", "B.MAT_GRP_5 AS Type2", "MAT_GRP_5", false);
            ((udcTableForm)(this.btnSort.BindingForm)).AddRow("LD Count", "B.MAT_GRP_6 AS LD_Count", "MAT_GRP_6", false);
            ((udcTableForm)(this.btnSort.BindingForm)).AddRow("Density", "B.MAT_GRP_7 AS Density", "MAT_GRP_7", false);
            ((udcTableForm)(this.btnSort.BindingForm)).AddRow("Generation", "B.MAT_GRP_8 AS Generation", "MAT_GRP_8", false);
            ((udcTableForm)(this.btnSort.BindingForm)).AddRow("Pin Type", "B.MAT_CMF_10 AS Pin_Type", "MAT_CMF_10", true);
            ((udcTableForm)(this.btnSort.BindingForm)).AddRow("Step", "A.OPER AS Step", "A.OPER", true);
            ((udcTableForm)(this.btnSort.BindingForm)).AddRow("Product", "A.MAT_ID AS Product", "A.MAT_ID", true);
            ((udcTableForm)(this.btnSort.BindingForm)).AddRow("Cust Device", "B.MAT_CMF_7 AS Cust_Device", "B.MAT_CMF_7", false);

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

            string strDate = cdvDate.Value.ToString("yyyyMMdd");
            string sYesterday = cdvDate.Value.AddDays(-1).ToString("yyyyMMdd");

            udcTableForm tableForm = (udcTableForm)btnSort.BindingForm;

            QueryCond1 = tableForm.SelectedValueToQueryContainNull;
            QueryCond2 = tableForm.SelectedValue2ToQueryContainNull;

            strSqlString.AppendFormat("SELECT {0}, A.LV_TYPE" + "\n", QueryCond1);
            strSqlString.Append("     , SUM(DECODE(CUTOFF_DT, '" + sYesterday + "23', QTY, 0)) AS TIME_23" + "\n"); 
            strSqlString.Append("     , SUM(DECODE(CUTOFF_DT, '" + strDate + "00', QTY, 0)) AS TIME_0" + "\n");
            strSqlString.Append("     , SUM(DECODE(CUTOFF_DT, '" + strDate + "01', QTY, 0)) AS TIME_1" + "\n");
            strSqlString.Append("     , SUM(DECODE(CUTOFF_DT, '" + strDate + "02', QTY, 0)) AS TIME_2" + "\n");
            strSqlString.Append("     , SUM(DECODE(CUTOFF_DT, '" + strDate + "03', QTY, 0)) AS TIME_3" + "\n");
            strSqlString.Append("     , SUM(DECODE(CUTOFF_DT, '" + strDate + "04', QTY, 0)) AS TIME_4" + "\n");
            strSqlString.Append("     , SUM(DECODE(CUTOFF_DT, '" + strDate + "05', QTY, 0)) AS TIME_5" + "\n");
            strSqlString.Append("     , SUM(DECODE(CUTOFF_DT, '" + strDate + "06', QTY, 0)) AS TIME_6" + "\n");
            strSqlString.Append("     , SUM(DECODE(CUTOFF_DT, '" + strDate + "07', QTY, 0)) AS TIME_7" + "\n");
            strSqlString.Append("     , SUM(DECODE(CUTOFF_DT, '" + strDate + "08', QTY, 0)) AS TIME_8" + "\n");
            strSqlString.Append("     , SUM(DECODE(CUTOFF_DT, '" + strDate + "09', QTY, 0)) AS TIME_9" + "\n");
            strSqlString.Append("     , SUM(DECODE(CUTOFF_DT, '" + strDate + "10', QTY, 0)) AS TIME_10" + "\n");
            strSqlString.Append("     , SUM(DECODE(CUTOFF_DT, '" + strDate + "11', QTY, 0)) AS TIME_11" + "\n");
            strSqlString.Append("     , SUM(DECODE(CUTOFF_DT, '" + strDate + "12', QTY, 0)) AS TIME_12" + "\n");
            strSqlString.Append("     , SUM(DECODE(CUTOFF_DT, '" + strDate + "13', QTY, 0)) AS TIME_13" + "\n");
            strSqlString.Append("     , SUM(DECODE(CUTOFF_DT, '" + strDate + "14', QTY, 0)) AS TIME_14" + "\n");
            strSqlString.Append("     , SUM(DECODE(CUTOFF_DT, '" + strDate + "15', QTY, 0)) AS TIME_15" + "\n");
            strSqlString.Append("     , SUM(DECODE(CUTOFF_DT, '" + strDate + "16', QTY, 0)) AS TIME_16" + "\n");
            strSqlString.Append("     , SUM(DECODE(CUTOFF_DT, '" + strDate + "17', QTY, 0)) AS TIME_17" + "\n");
            strSqlString.Append("     , SUM(DECODE(CUTOFF_DT, '" + strDate + "18', QTY, 0)) AS TIME_18" + "\n");
            strSqlString.Append("     , SUM(DECODE(CUTOFF_DT, '" + strDate + "19', QTY, 0)) AS TIME_19" + "\n");
            strSqlString.Append("     , SUM(DECODE(CUTOFF_DT, '" + strDate + "20', QTY, 0)) AS TIME_20" + "\n");
            strSqlString.Append("     , SUM(DECODE(CUTOFF_DT, '" + strDate + "21', QTY, 0)) AS TIME_21" + "\n");
            strSqlString.Append("     , SUM(DECODE(CUTOFF_DT, '" + strDate + "22', QTY, 0)) AS TIME_22" + "\n");
                                   
            strSqlString.Append("  FROM (" + "\n");
            strSqlString.Append("        SELECT FACTORY, SUB_AREA_ID, RES_MODEL, MAT_ID, LV_TYPE, CUTOFF_DT, OPER" + "\n");
            strSqlString.Append("             , CASE WHEN LV_TYPE = '대수' THEN FMB_CNT" + "\n");
            strSqlString.Append("                    WHEN LV_TYPE = 'RUN' THEN FMB_RUN" + "\n");
            strSqlString.Append("                    WHEN LV_TYPE = 'WAIT' THEN FMB_WAIT" + "\n");
            strSqlString.Append("               END AS QTY" + "\n");
            strSqlString.Append("          FROM RSUMARRDAT " + "\n");
            strSqlString.Append("             , (SELECT DECODE(LEVEL, 1, '대수', 2, 'RUN', 3, 'WAIT') AS LV_TYPE FROM DUAL CONNECT BY LEVEL <= 3) " + "\n");                        
            strSqlString.Append("         WHERE 1=1 " + "\n");
            strSqlString.Append("           AND WORK_DATE IN ('" + sYesterday + "','" + strDate + "') " + "\n");
            strSqlString.Append("           AND GUBUN = 'SUM' " + "\n");
            strSqlString.Append("           AND FACTORY = '" + cdvFactory.Text + "'  " + "\n");
            strSqlString.Append("           AND MAT_ID LIKE '" + txtSearchProduct.Text + "' " + "\n");

            if (cdvStep.Text != "ALL" && cdvStep.Text.Trim() != "")
            {
                strSqlString.Append("           AND OPER " + cdvStep.SelectedValueToQueryString + "\n");
            }

            if (cdvModel.Text != "ALL" && cdvModel.Text.Trim() != "")
            {
                strSqlString.Append("           AND RES_MODEL " + cdvModel.SelectedValueToQueryString + "\n");
            }

            if (ckbWB.Checked == true)
            {
                strSqlString.Append("           AND OPER LIKE 'A06%'" + "\n");
            }

            strSqlString.Append("       ) A" + "\n");
            strSqlString.Append("     , MWIPMATDEF B" + "\n");
            strSqlString.Append(" WHERE 1=1" + "\n");
            strSqlString.Append("   AND A.MAT_ID = B.MAT_ID" + "\n");
            strSqlString.Append("   AND A.FACTORY = B.FACTORY" + "\n");
            strSqlString.Append("   AND B.DELETE_FLAG = ' '" + "\n");

            #region 상세 조회에 따른 SQL문 생성
            if (udcWIPCondition1.Text != "ALL" && udcWIPCondition1.Text != "")
                strSqlString.AppendFormat("   AND B.MAT_GRP_1 {0} " + "\n", udcWIPCondition1.SelectedValueToQueryString);

            if (udcWIPCondition2.Text != "ALL" && udcWIPCondition2.Text != "")
                strSqlString.AppendFormat("   AND B.MAT_GRP_2 {0} " + "\n", udcWIPCondition2.SelectedValueToQueryString);

            if (udcWIPCondition3.Text != "ALL" && udcWIPCondition3.Text != "")
                strSqlString.AppendFormat("   AND B.MAT_GRP_3 {0} " + "\n", udcWIPCondition3.SelectedValueToQueryString);

            if (udcWIPCondition4.Text != "ALL" && udcWIPCondition4.Text != "")
                strSqlString.AppendFormat("   AND B.MAT_GRP_4 {0} " + "\n", udcWIPCondition4.SelectedValueToQueryString);

            if (udcWIPCondition5.Text != "ALL" && udcWIPCondition5.Text != "")
                strSqlString.AppendFormat("   AND B.MAT_GRP_5 {0} " + "\n", udcWIPCondition5.SelectedValueToQueryString);

            if (udcWIPCondition6.Text != "ALL" && udcWIPCondition6.Text != "")
                strSqlString.AppendFormat("   AND B.MAT_GRP_6 {0} " + "\n", udcWIPCondition6.SelectedValueToQueryString);

            if (udcWIPCondition7.Text != "ALL" && udcWIPCondition7.Text != "")
                strSqlString.AppendFormat("   AND B.MAT_GRP_7 {0} " + "\n", udcWIPCondition7.SelectedValueToQueryString);

            if (udcWIPCondition8.Text != "ALL" && udcWIPCondition8.Text != "")
                strSqlString.AppendFormat("   AND B.MAT_GRP_8 {0} " + "\n", udcWIPCondition8.SelectedValueToQueryString);

            if (udcWIPCondition9.Text != "ALL" && udcWIPCondition9.Text != "")
                strSqlString.AppendFormat("   AND B.MAT_GRP_9 {0} " + "\n", udcWIPCondition9.SelectedValueToQueryString);
            #endregion

            strSqlString.AppendFormat("GROUP BY {0}, B.FACTORY, A.LV_TYPE" + "\n", QueryCond2);
            strSqlString.AppendFormat("ORDER BY {0}, DECODE(LV_TYPE, '대수', 1, 'RUN', 2, 'WAIT', 3)" + "\n", QueryCond2);  
          

            if (GlobalVariable.gsUserGroup == "ADMIN_GROUP" || GlobalVariable.gsUserGroup == "HANA_ADMIN_GROUP")
            {
                System.Windows.Forms.Clipboard.SetText(strSqlString.ToString());
            }
            
            return strSqlString.ToString();
        }
           
        #endregion


        #region EVENT 처리
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

                spdData.DataSource = dt;
                //그루핑 순서 1순위만 SubTotal을 사용하기 위해서 첫번째 값을 가져옴
                //int sub = ((udcTableForm)(this.btnSort.BindingForm)).GetCheckedValue(2);
                
                //by John
                //1.Griid 합계 표시
                //int[] rowType = spdData.RPT_DataBindingWithSubTotal(dt, 0, sub + 1, 13, null, null, btnSort);
                //                  토탈 시작할 셀넘버, 시작 부터 서브토탈 몇개 쓸건지, 첫셀부터 몇번째 셀까지 TOTAL 적용안함

                //3. Total부분 셀머지
                //spdData.RPT_FillDataSelectiveCells("Total", 0, 13, 0, 1, true, Align.Center, VerticalAlign.Center);            
                               
                //4. Column Auto Fit
                spdData.RPT_AutoFit(false);

                for (int i = 0; i <= 13; i++)
                {
                    spdData.ActiveSheet.Columns[i].BackColor = Color.LemonChiffon;
                }

                for (int i = 0; i < spdData.ActiveSheet.RowCount; i++)
                {
                    if ("대수" == spdData.ActiveSheet.Cells[i, 14].Value.ToString())
                    {
                        spdData.ActiveSheet.Rows[i].BackColor = Color.LemonChiffon;                       
                    }
                }
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

        private void cdvFactory_ValueSelectedItemChanged(object sender, Miracom.UI.MCCodeViewSelChanged_EventArgs e)
        {
            GridColumnInit();

            this.SetFactory(cdvFactory.txtValue);
            cdvModel.sFactory = cdvFactory.txtValue;
            cdvStep.sFactory = cdvFactory.txtValue;

            cdvStep.SetChangedFlag(true);
            cdvStep.Text = "";
            string strQuery = string.Empty;

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

    }
}