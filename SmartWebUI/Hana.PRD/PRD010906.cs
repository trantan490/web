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
    public partial class PRD010906 : Miracom.SmartWeb.UI.Controls.udcCUSReport002
    {
        private Color color = Color.Empty;

        /// <summary>
        /// 클  래  스: PRD010906<br/>
        /// 클래스요약: OMS 시간대 별 투입 계획<br/>
        /// 작  성  자: 하나마이크론 임종우<br/>
        /// 최초작성일: 2012-08-21<br/>
        /// 상세  설명: OMS 시간대 별 투입 계획 조회<br/>
        /// 변경  내용: <br/>
        /// 2012-08-22-임종우 : 계획이 없는 제품은 표시 하지 않는다. 재공이 존재 해도...(김문한K 요청)
        
        /// </summary>
        public PRD010906()
        {
            InitializeComponent();
            cdvDate.Value = DateTime.Now;
            SortInit();
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
                spdData.RPT_AddBasicColumn("Type1", 0, 0, Visibles.True, Frozen.True, Align.Center, Merge.True, Formatter.String, 50);
                spdData.RPT_AddBasicColumn("Package", 0, 1, Visibles.True, Frozen.True, Align.Center, Merge.True, Formatter.String, 70);
                spdData.RPT_AddBasicColumn("Product", 0, 2, Visibles.True, Frozen.True, Align.Left, Merge.True, Formatter.String, 70);                
                spdData.RPT_AddBasicColumn("SERISE", 0, 3, Visibles.True, Frozen.True, Align.Center, Merge.True, Formatter.String, 50);
                spdData.RPT_AddBasicColumn("PKG", 0, 4, Visibles.True, Frozen.True, Align.Center, Merge.True, Formatter.String, 70);                
                                
                spdData.RPT_AddBasicColumn("Hourly OMS Plan", 0, 5, Visibles.True, Frozen.False, Align.Center, Merge.False, Formatter.String, 70);
                spdData.RPT_AddBasicColumn("0시", 1, 5, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 70);
                spdData.RPT_AddBasicColumn("04시", 1, 6, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 70);
                spdData.RPT_AddBasicColumn("08시", 1, 7, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 70);
                spdData.RPT_AddBasicColumn("12시", 1, 8, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 70);
                spdData.RPT_AddBasicColumn("16시", 1, 9, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 70);
                spdData.RPT_AddBasicColumn("20시", 1, 10, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 70);
                spdData.RPT_MerageHeaderColumnSpan(0, 5, 6);

                spdData.RPT_AddBasicColumn("Final plan", 0, 11, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 70);
                spdData.RPT_AddBasicColumn("Input Performance", 0, 12, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 70);
                spdData.RPT_AddBasicColumn("residual quantity", 0, 13, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 70);
                spdData.RPT_AddBasicColumn("Achievement rate", 0, 14, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 70);
                spdData.RPT_AddBasicColumn("STOCK WIP", 0, 15, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 70);
                
                spdData.RPT_MerageHeaderRowSpan(0, 0, 2);
                spdData.RPT_MerageHeaderRowSpan(0, 1, 2);
                spdData.RPT_MerageHeaderRowSpan(0, 2, 2);
                spdData.RPT_MerageHeaderRowSpan(0, 3, 2);
                spdData.RPT_MerageHeaderRowSpan(0, 4, 2);
                spdData.RPT_MerageHeaderRowSpan(0, 11, 2);
                spdData.RPT_MerageHeaderRowSpan(0, 12, 2);
                spdData.RPT_MerageHeaderRowSpan(0, 13, 2);
                spdData.RPT_MerageHeaderRowSpan(0, 14, 2);
                spdData.RPT_MerageHeaderRowSpan(0, 15, 2);
                
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
            //((udcTableForm)(this.btnSort.BindingForm)).AddRow("CUSTOMER", "PLN.MAT_GRP_1", "NVL((SELECT DATA_1 FROM MGCMTBLDAT WHERE TABLE_NAME = 'H_CUSTOMER' AND KEY_1 = PLN.MAT_GRP_1 AND ROWNUM=1), '-') AS CUSTOMER", true);
            //((udcTableForm)(this.btnSort.BindingForm)).AddRow("MAJOR CODE", "PLN.MAT_GRP_9", "PLN.MAT_GRP_9 AS MAJOR_CODE", false);
            //((udcTableForm)(this.btnSort.BindingForm)).AddRow("FAMILY", "PLN.MAT_GRP_2", "PLN.MAT_GRP_2 AS FAMILY", true);
            //((udcTableForm)(this.btnSort.BindingForm)).AddRow("PACKAGE", "PLN.MAT_GRP_3", "PLN.MAT_GRP_3 AS PACKAGE", true);
            //((udcTableForm)(this.btnSort.BindingForm)).AddRow("TYPE1", "PLN.MAT_GRP_4", "PLN.MAT_GRP_4 AS TYPE1", false);
            //((udcTableForm)(this.btnSort.BindingForm)).AddRow("TYPE2", "PLN.MAT_GRP_5", "PLN.MAT_GRP_5 AS TYPE2", false);
            //((udcTableForm)(this.btnSort.BindingForm)).AddRow("LD COUNT", "PLN.MAT_GRP_6", "PLN.MAT_GRP_6 AS \"LD COUNT\"", false);
            //((udcTableForm)(this.btnSort.BindingForm)).AddRow("PIN TYPE", "PLN.MAT_CMF_10", "PLN.MAT_CMF_10 AS PIN_TYPE", true);
            //((udcTableForm)(this.btnSort.BindingForm)).AddRow("DENSITY", "PLN.MAT_GRP_7", "PLN.MAT_GRP_7 AS DENSITY", true);
            //((udcTableForm)(this.btnSort.BindingForm)).AddRow("GENERATION", "PLN.MAT_GRP_8", "PLN.MAT_GRP_8 AS GENERATION", false);
            //((udcTableForm)(this.btnSort.BindingForm)).AddRow("PRODUCT", "PLN.MAT_ID", "PLN.MAT_ID AS PRODUCT", true);
            //((udcTableForm)(this.btnSort.BindingForm)).AddRow("CUST DEVICE", "PLN.MAT_CMF_7", "PLN.MAT_CMF_7 AS CUST_DEVICE", false);
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

            string today;

            today = cdvDate.Value.ToString("yyyyMMdd");

            strSqlString.Append("SELECT MAT_GRP_4" + "\n");
            strSqlString.Append("     , MAT_GRP_3" + "\n");
            strSqlString.Append("     , PLN.MAT_ID" + "\n");
            strSqlString.Append("     , DECODE(MAJ_CODE, 'MEMORY', SUBSTR(PLN.MAT_ID, 8, 2), '-') AS SERISE" + "\n");
            strSqlString.Append("     , MAT_GRP_6 || MAT_GRP_3 AS PKG" + "\n");
            strSqlString.Append("     , TIME_QTY_00" + "\n");
            strSqlString.Append("     , TIME_QTY_04" + "\n");
            strSqlString.Append("     , TIME_QTY_08" + "\n");
            strSqlString.Append("     , TIME_QTY_12" + "\n");
            strSqlString.Append("     , TIME_QTY_16" + "\n");
            strSqlString.Append("     , TIME_QTY_20" + "\n");
            strSqlString.Append("     , FINAL_QTY" + "\n");
            strSqlString.Append("     , NVL(ISS.ISSUE_QTY, 0) AS ISSUE_QTY" + "\n");
            strSqlString.Append("     , FINAL_QTY - NVL(ISS.ISSUE_QTY, 0) AS DEF_QTY" + "\n");
            strSqlString.Append("     , CASE WHEN FINAL_QTY = 0 OR NVL(ISS.ISSUE_QTY, 0) = 0 THEN 0" + "\n");
            strSqlString.Append("            ELSE ROUND(ISS.ISSUE_QTY / FINAL_QTY * 100, 0) " + "\n");
            strSqlString.Append("       END AS PER" + "\n");
            strSqlString.Append("     , WIP.STOCK_QTY" + "\n");
            strSqlString.Append("  FROM RWIPPLNTIM PLN" + "\n");            
            strSqlString.Append("     , (" + "\n");
            strSqlString.Append("        SELECT MAT_ID, SUM(QTY_1) AS STOCK_QTY " + "\n");
            strSqlString.Append("          FROM ( " + "\n");
            strSqlString.Append("                SELECT CASE WHEN MAT_ID LIKE 'SEK%' THEN REPLACE(MAT_ID, SUBSTR(MAT_ID, 13, 4), '-___') " + "\n");
            strSqlString.Append("                            ELSE MAT_ID END MAT_ID, QTY_1 " + "\n");
            strSqlString.Append("                  FROM RWIPLOTSTS " + "\n");
            strSqlString.Append("                 WHERE 1=1 " + "\n");
            strSqlString.Append("                   AND FACTORY = '" + GlobalVariable.gsAssyDefaultFactory + "' " + "\n");
            strSqlString.Append("                   AND LOT_TYPE = 'W' " + "\n");
            strSqlString.Append("                   AND OPER = 'A0000' " + "\n");
            strSqlString.Append("                   AND LOT_DEL_FLAG = ' ' " + "\n");
            strSqlString.Append("                   AND MAT_ID LIKE 'SE%'" + "\n");            
            strSqlString.Append("               ) " + "\n");
            strSqlString.Append("         GROUP BY MAT_ID" + "\n");
            strSqlString.Append("       ) WIP" + "\n");
            strSqlString.Append("     , (" + "\n");
            strSqlString.Append("        SELECT MAT_ID, SUM(QTY_1) AS ISSUE_QTY " + "\n");
            strSqlString.Append("          FROM (" + "\n");
            strSqlString.Append("                SELECT CASE WHEN MAT_ID LIKE 'SEK%' THEN REPLACE(MAT_ID, SUBSTR(MAT_ID, 13, 4), '-___')" + "\n");
            strSqlString.Append("                            ELSE MAT_ID END MAT_ID, S1_END_QTY_1+S2_END_QTY_1+S3_END_QTY_1 AS QTY_1" + "\n");
            strSqlString.Append("                  FROM RSUMWIPMOV" + "\n");            
            strSqlString.Append("                 WHERE 1=1" + "\n");
            strSqlString.Append("                   AND FACTORY = '" + GlobalVariable.gsAssyDefaultFactory + "'" + "\n");
            strSqlString.Append("                   AND WORK_DATE = '" + today + "'" + "\n");
            strSqlString.Append("                   AND MAT_ID LIKE 'SE%'" + "\n");
            strSqlString.Append("                   AND OPER = 'A0000'" + "\n");
            strSqlString.Append("               ) " + "\n");
            strSqlString.Append("         WHERE QTY_1 > 0" + "\n");
            strSqlString.Append("         GROUP BY MAT_ID" + "\n");
            strSqlString.Append("       ) ISS" + "\n");
            strSqlString.Append(" WHERE 1=1" + "\n");
            strSqlString.Append("   AND PLN.MAT_ID = WIP.MAT_ID(+)" + "\n");
            strSqlString.Append("   AND PLN.MAT_ID = ISS.MAT_ID(+)" + "\n");
            strSqlString.Append("   AND PLN.FACTORY = '" + GlobalVariable.gsAssyDefaultFactory + "'" + "\n");
            strSqlString.Append("   AND PLN.PLAN_DATE = '" + today + "'" + "\n");
            strSqlString.Append("   AND PLN.MAT_ID LIKE '" + txtProduct.Text + "'" + "\n");
            strSqlString.Append("   AND (TIME_QTY_00+TIME_QTY_04+TIME_QTY_08+TIME_QTY_12+TIME_QTY_16+TIME_QTY_20) > 0" + "\n");

            //상세 조회에 따른 SQL문 생성                        
            if (udcWIPCondition1.Text != "ALL" && udcWIPCondition1.Text != "")
                strSqlString.AppendFormat("   AND PLN.MAT_GRP_1 {0}" + "\n", udcWIPCondition1.SelectedValueToQueryString);

            if (udcWIPCondition2.Text != "ALL" && udcWIPCondition2.Text != "")
                strSqlString.AppendFormat("   AND PLN.MAT_GRP_2 {0} " + "\n", udcWIPCondition2.SelectedValueToQueryString);

            if (udcWIPCondition3.Text != "ALL" && udcWIPCondition3.Text != "")
                strSqlString.AppendFormat("   AND PLN.MAT_GRP_3 {0} " + "\n", udcWIPCondition3.SelectedValueToQueryString);

            if (udcWIPCondition4.Text != "ALL" && udcWIPCondition4.Text != "")
                strSqlString.AppendFormat("   AND PLN.MAT_GRP_4 {0} " + "\n", udcWIPCondition4.SelectedValueToQueryString);

            if (udcWIPCondition5.Text != "ALL" && udcWIPCondition5.Text != "")
                strSqlString.AppendFormat("   AND PLN.MAT_GRP_5 {0} " + "\n", udcWIPCondition5.SelectedValueToQueryString);

            if (udcWIPCondition6.Text != "ALL" && udcWIPCondition6.Text != "")
                strSqlString.AppendFormat("   AND PLN.MAT_GRP_6 {0} " + "\n", udcWIPCondition6.SelectedValueToQueryString);

            if (udcWIPCondition7.Text != "ALL" && udcWIPCondition7.Text != "")
                strSqlString.AppendFormat("   AND PLN.MAT_GRP_7 {0} " + "\n", udcWIPCondition7.SelectedValueToQueryString);

            if (udcWIPCondition8.Text != "ALL" && udcWIPCondition8.Text != "")
                strSqlString.AppendFormat("   AND PLN.MAT_GRP_8 {0} " + "\n", udcWIPCondition8.SelectedValueToQueryString);

            if (udcWIPCondition9.Text != "ALL" && udcWIPCondition9.Text != "")
                strSqlString.AppendFormat("   AND PLN.MAT_GRP_9 {0} " + "\n", udcWIPCondition9.SelectedValueToQueryString);

            strSqlString.Append(" ORDER BY MAJ_CODE, MAT_GRP_4, MAT_GRP_3, MAT_ID" + "\n");

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

                int[] rowType = spdData.RPT_DataBindingWithSubTotal(dt, 0, 2, 5, null, null, btnSort);
                //데이타테이블, 토탈 시작할 셀넘버, 시작 부터 서브토탈 몇개 쓸건지, 첫셀부터 몇번째 셀까지 TOTAL 적용안함

                //Total부분 셀머지
                spdData.RPT_FillDataSelectiveCells("Total", 0, 5, 0, 1, true, Align.Center, VerticalAlign.Center);

                spdData.RPT_AutoFit(false);

                SetAvgVertical(1, 14);

                // 2012-08-22-임종우 : 달성율 100% 미만은 색상 표시 함                
                for (int i = 0; i < spdData.ActiveSheet.RowCount; i++)
                {
                    if (spdData.ActiveSheet.Cells[i, 14].BackColor == color && spdData.ActiveSheet.Cells[i, 14].Value != null && Convert.ToInt32(spdData.ActiveSheet.Cells[i, 14].Value) < 100)
                    {
                        for (int y = 2; y <= 15; y++)
                        {
                            spdData.ActiveSheet.Cells[i, y].BackColor = Color.Pink;
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

        


        /// <summary>
        /// 2012-08-22-임종우 : AVG 구하기.. SubTotal, GrandTotal 구할때 특정 컬럼 내용들로 직접 구할때..        
        /// 
        /// </summary>
        /// <param name="nSampleNormalRowPos"></param>
        /// <param name="nColPos"></param>
        public void SetAvgVertical(int nSampleNormalRowPos, int nColPos)
        {             
            double iPlan = 0;
            double iIn = 0;

            color = spdData.ActiveSheet.Cells[nSampleNormalRowPos, nColPos].BackColor;
            iPlan = Convert.ToDouble(spdData.ActiveSheet.Cells[0, 11].Value);
            iIn = Convert.ToDouble(spdData.ActiveSheet.Cells[0, 12].Value);

            // 분모값이 0이 아닐경우에만 계산한다..
            if (iIn != 0)
            {
                spdData.ActiveSheet.Cells[0, nColPos].Value = (iIn / iPlan) * 100;

                for (int i = 1; i < spdData.ActiveSheet.Rows.Count; i++)
                {
                    if (spdData.ActiveSheet.Cells[i, nColPos].BackColor != color)
                    {
                        iPlan = Convert.ToDouble(spdData.ActiveSheet.Cells[i, 11].Value);
                        iIn = Convert.ToDouble(spdData.ActiveSheet.Cells[i, 12].Value);

                        if (iPlan != 0)
                        {
                            spdData.ActiveSheet.Cells[i, nColPos].Value = iIn / iPlan * 100;
                        }
                    }
                }
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
                ExcelHelper.Instance.subMakeExcel(spdData, this.lblTitle.Text, null, null, true);
                //spdData.ExportExcel();            
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
