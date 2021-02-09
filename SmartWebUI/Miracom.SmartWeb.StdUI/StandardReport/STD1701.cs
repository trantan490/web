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

namespace Miracom.SmartWeb.UI
{
    public partial class STD1701 : Miracom.SmartWeb.UI.Controls.udcCUSReport002
    {
        private string strSelect;
        private string strDecode;
        DataTable dtOper = null;

        /// <summary>
        /// 클  래  스: STD1701<br/>
        /// 클래스요약: 제품별 FLOW 정보<br/>
        /// 작  성  자: 하나마이크론 임종우<br/>
        /// 최초작성일: 2011-08-29<br/>
        /// 상세  설명: 제품별 FLOW 정보<br/>
        /// 변경  내용: <br/>
        /// 변  경  자: <br />
        /// 2011-12-01-임종우 : 제품 기준정보 추가 함 (민재훈차장 요청)
        /// 2018-04-17-임종우 : Full Process 검색용 추가 (한승종책임 요청)
        /// </summary>
        public STD1701()
        {
            InitializeComponent();
                        
            SortInit();
            GridColumnInit();

            cdvFromToDate.AutoBinding();                              
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

            if (cdvStep.Text.TrimEnd() == "")
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
            spdData.ActiveSheet.ColumnHeader.Rows.Count = 1;
            spdData.RPT_ColumnInit();
            
            if (ckbFull.Checked == true && cdvStep.Text != "ALL" && cdvStep.Text != "")
            {
                dtOper = CmnFunction.oComm.GetFuncDataTable("DYNAMIC", MakeSqlString1());
            }

            try
            {
                spdData.RPT_AddBasicColumn("Customer", 0, 0, Visibles.True, Frozen.True, Align.Center, Merge.True, Formatter.String, 80);
                spdData.RPT_AddBasicColumn("Family", 0, 1, Visibles.False, Frozen.True, Align.Center, Merge.True, Formatter.String, 80);
                spdData.RPT_AddBasicColumn("Package", 0, 2, Visibles.True, Frozen.True, Align.Center, Merge.True, Formatter.String, 80);
                spdData.RPT_AddBasicColumn("Type1", 0, 3, Visibles.False, Frozen.True, Align.Center, Merge.True, Formatter.String, 80);
                spdData.RPT_AddBasicColumn("Type2", 0, 4, Visibles.False, Frozen.True, Align.Center, Merge.True, Formatter.String, 80);
                spdData.RPT_AddBasicColumn("Lead", 0, 5, Visibles.False, Frozen.True, Align.Center, Merge.True, Formatter.String, 80);
                spdData.RPT_AddBasicColumn("Density", 0, 6, Visibles.False, Frozen.True, Align.Center, Merge.True, Formatter.String, 80);
                spdData.RPT_AddBasicColumn("Generation", 0, 7, Visibles.False, Frozen.True, Align.Center, Merge.True, Formatter.String, 80);
                spdData.RPT_AddBasicColumn("PIN TYPE", 0, 8, Visibles.True, Frozen.True, Align.Left, Merge.True, Formatter.String, 80);
                spdData.RPT_AddBasicColumn("Product", 0, 9, Visibles.True, Frozen.True, Align.Left, Merge.True, Formatter.String, 180);
                spdData.RPT_AddBasicColumn("Flow", 0, 10, Visibles.True, Frozen.True, Align.Center, Merge.True, Formatter.String, 60);
                spdData.RPT_AddBasicColumn("Flow_Desc", 0, 11, Visibles.True, Frozen.True, Align.Left, Merge.True, Formatter.String, 120);

                // 2018-04-17-임종우 : Full Process 검색용 추가
                if (ckbFull.Checked == true && cdvStep.Text != "ALL" && cdvStep.Text != "")
                {
                    int headerCount = 12;

                    for (int i = 0; i < dtOper.Rows.Count; i++)
                    {
                        spdData.RPT_AddBasicColumn(dtOper.Rows[i][0].ToString(), 0, headerCount, Visibles.True, Frozen.False, Align.Center, Merge.False, Formatter.String, 60);
                        headerCount++;
                    }
                }
                else
                {
                    spdData.RPT_AddDynamicColumn(cdvStep, 0, 12, Visibles.True, Frozen.False, Align.Center, Merge.False, Formatter.String, 60);
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
            ((udcTableForm)(this.btnSort.BindingForm)).AddRow("Customer", "(SELECT DATA_1 FROM MGCMTBLDAT WHERE TABLE_NAME = 'H_CUSTOMER' AND KEY_1 = B.MAT_GRP_1 AND ROWNUM=1) AS MAT_GRP_1", "B.MAT_GRP_1", true);
            ((udcTableForm)(this.btnSort.BindingForm)).AddRow("Family", "B.MAT_GRP_2", "B.MAT_GRP_2", false);
            ((udcTableForm)(this.btnSort.BindingForm)).AddRow("Package", "B.MAT_GRP_3", "B.MAT_GRP_3", true);
            ((udcTableForm)(this.btnSort.BindingForm)).AddRow("Type1", "B.MAT_GRP_4", "B.MAT_GRP_4", false);
            ((udcTableForm)(this.btnSort.BindingForm)).AddRow("Type2", "B.MAT_GRP_5", "B.MAT_GRP_5", false);
            ((udcTableForm)(this.btnSort.BindingForm)).AddRow("LD Count", "B.MAT_GRP_6", "B.MAT_GRP_6", false);
            ((udcTableForm)(this.btnSort.BindingForm)).AddRow("Density", "B.MAT_GRP_7", "B.MAT_GRP_7", false);
            ((udcTableForm)(this.btnSort.BindingForm)).AddRow("Generation", "B.MAT_GRP_8", "B.MAT_GRP_8", false);
            ((udcTableForm)(this.btnSort.BindingForm)).AddRow("Pin Type", "B.MAT_CMF_10", "B.MAT_CMF_10", true);
            ((udcTableForm)(this.btnSort.BindingForm)).AddRow("Product", "B.MAT_ID", "B.MAT_ID", true);
            ((udcTableForm)(this.btnSort.BindingForm)).AddRow("Flow", "A.FLOW", "A.FLOW", true);

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
            
            udcTableForm tableForm = (udcTableForm)btnSort.BindingForm;

            QueryCond1 = tableForm.SelectedValueToQueryContainNull;            
            QueryCond2 = tableForm.SelectedValue2ToQueryContainNull;            

            strSelect = string.Empty;
            strDecode = string.Empty;
            
            strDecode = cdvStep.getDecodeQuery("MAX(DECODE(OPER", " '■', '')) AS ", "V").Replace(", MAX(DECODE(OPER,", "     , MAX(DECODE(OPER,");            
            
            strSqlString.AppendFormat("SELECT {0} " + "\n", QueryCond1);
            strSqlString.AppendFormat("     , (SELECT FLOW_DESC FROM MWIPFLWDEF WHERE FACTORY = '" + cdvFactory.Text + "' AND FLOW = A.FLOW) AS FLOW_DESC " + "\n");

            // 2018-04-17-임종우 : Full Process 검색용 추가
            if (ckbFull.Checked == true && cdvStep.Text != "ALL" && cdvStep.Text != "")
            {
                for (int i = 0; i < dtOper.Rows.Count; i++)
                {
                    strSqlString.Append("     , MAX(DECODE(OPER, '" + dtOper.Rows[i][0].ToString() + "',  '■', '')) AS  V" + i + "\n");
                }
            }
            else
            {
                strSqlString.AppendFormat("{0} ", strDecode);
            }

            strSqlString.AppendFormat("  FROM MWIPFLWOPR@RPTTOMES A " + "\n");
            strSqlString.AppendFormat("     , MWIPMATDEF B " + "\n");

            // 생산실적 기준 제품의 FLOW 정보
            if (rdbEnd.Checked)
            {
                strSqlString.AppendFormat("     , ( " + "\n");
                strSqlString.AppendFormat("        SELECT DISTINCT MAT_ID " + "\n");
                strSqlString.AppendFormat("          FROM RSUMWIPMOV " + "\n");
                strSqlString.AppendFormat("          WHERE 1=1 " + "\n");
                strSqlString.AppendFormat("            AND WORK_DATE BETWEEN '{0}' AND '{1}' " + "\n", cdvFromToDate.HmFromDay, cdvFromToDate.HmToDay);
                strSqlString.AppendFormat("            AND CM_KEY_1 = '" + cdvFactory.Text + "'" + "\n");
                strSqlString.AppendFormat("            AND LOT_TYPE = 'W' " + "\n");
                strSqlString.AppendFormat("       ) C " + "\n");
            }

            strSqlString.AppendFormat(" WHERE 1 = 1 " + "\n");
            strSqlString.AppendFormat("   AND A.FACTORY = B.FACTORY " + "\n");
            strSqlString.AppendFormat("   AND A.FLOW = B.FIRST_FLOW " + "\n");

            if (rdbEnd.Checked)
            {
                strSqlString.AppendFormat("   AND B.MAT_ID = C.MAT_ID " + "\n");
            }

            strSqlString.AppendFormat("   AND A.FACTORY = '" + cdvFactory.Text + "'"+ "\n");
            strSqlString.AppendFormat("   AND B.MAT_TYPE = 'FG'  " + "\n");
            strSqlString.AppendFormat("   AND B.DELETE_FLAG = ' ' " + "\n");

            if (txtSearchProduct.Text.Trim() != "%" && txtSearchProduct.Text.Trim() != "")
            {
                strSqlString.AppendFormat("   AND B.MAT_ID LIKE '" + txtSearchProduct.Text + "'" + "\n");
            }

            // 2018-04-17-임종우 : Full Process 검색용 추가
            if (ckbFull.Checked == true && cdvStep.Text != "ALL" && cdvStep.Text != "")
            {
                strSqlString.AppendFormat("   AND A.FLOW IN ( " + "\n");
                strSqlString.AppendFormat("                  SELECT FLOW " + "\n");
                strSqlString.AppendFormat("                    FROM MWIPFLWOPR@RPTTOMES " + "\n");
                strSqlString.AppendFormat("                   WHERE FACTORY = '" + cdvFactory.Text + "' " + "\n");
                strSqlString.AppendFormat("                     AND OPER " + cdvStep.SelectedValueToQueryString + "\n");
                strSqlString.AppendFormat("                   GROUP BY FLOW " + "\n");
                strSqlString.AppendFormat("                  HAVING COUNT(OPER) = " + cdvStep.SelectCount + "\n");
                strSqlString.AppendFormat("                 )" + "\n");
            }
            else if (cdvStep.Text != "ALL" && cdvStep.Text != "")
            {
                strSqlString.AppendFormat("   AND A.OPER " + cdvStep.SelectedValueToQueryString + "\n");
            }

            #region 상세 조회에 따른 SQL문 생성
            if (udcWIPCondition1.Text != "ALL" && udcWIPCondition1.Text != "")
                strSqlString.AppendFormat("  AND B.MAT_GRP_1 {0} " + "\n", udcWIPCondition1.SelectedValueToQueryString);

            if (udcWIPCondition2.Text != "ALL" && udcWIPCondition2.Text != "")
                strSqlString.AppendFormat("  AND B.MAT_GRP_2 {0} " + "\n", udcWIPCondition2.SelectedValueToQueryString);

            if (udcWIPCondition3.Text != "ALL" && udcWIPCondition3.Text != "")
                strSqlString.AppendFormat("  AND B.MAT_GRP_3 {0} " + "\n", udcWIPCondition3.SelectedValueToQueryString);

            if (udcWIPCondition4.Text != "ALL" && udcWIPCondition4.Text != "")
                strSqlString.AppendFormat("  AND B.MAT_GRP_4 {0} " + "\n", udcWIPCondition4.SelectedValueToQueryString);

            if (udcWIPCondition5.Text != "ALL" && udcWIPCondition5.Text != "")
                strSqlString.AppendFormat("  AND B.MAT_GRP_5 {0} " + "\n", udcWIPCondition5.SelectedValueToQueryString);

            if (udcWIPCondition6.Text != "ALL" && udcWIPCondition6.Text != "")
                strSqlString.AppendFormat("  AND B.MAT_GRP_6 {0} " + "\n", udcWIPCondition6.SelectedValueToQueryString);

            if (udcWIPCondition7.Text != "ALL" && udcWIPCondition7.Text != "")
                strSqlString.AppendFormat("  AND B.MAT_GRP_7 {0} " + "\n", udcWIPCondition7.SelectedValueToQueryString);

            if (udcWIPCondition8.Text != "ALL" && udcWIPCondition8.Text != "")
                strSqlString.AppendFormat("  AND B.MAT_GRP_8 {0} " + "\n", udcWIPCondition8.SelectedValueToQueryString);
            #endregion

            strSqlString.AppendFormat(" GROUP BY {0} " + "\n", QueryCond2);
            strSqlString.AppendFormat(" ORDER BY {0} " + "\n", QueryCond2);

            if (GlobalVariable.gsUserGroup == "ADMIN_GROUP" || GlobalVariable.gsUserGroup == "HANA_ADMIN_GROUP")
            {
                System.Windows.Forms.Clipboard.SetText(strSqlString.ToString());
            }

            return strSqlString.ToString();
        }

        private string MakeSqlString1()
        {
            StringBuilder strSqlString = new StringBuilder();
                        
            strSqlString.Append("SELECT DISTINCT OPER" + "\n");
            strSqlString.Append("  FROM MWIPFLWOPR@RPTTOMES A " + "\n");
            strSqlString.Append("     , MWIPMATDEF B " + "\n");
            strSqlString.Append(" WHERE 1=1" + "\n");
            strSqlString.Append("   AND A.FACTORY = B.FACTORY " + "\n");
            strSqlString.Append("   AND A.FLOW = B.FIRST_FLOW " + "\n");
            strSqlString.Append("   AND A.FACTORY = '" + cdvFactory.Text + "' " + "\n");
            strSqlString.Append("   AND B.MAT_TYPE = 'FG' " + "\n");
            strSqlString.Append("   AND B.DELETE_FLAG = ' ' " + "\n");
            strSqlString.Append("   AND A.FLOW IN ( " + "\n");
            strSqlString.Append("                  SELECT FLOW " + "\n");
            strSqlString.Append("                    FROM MWIPFLWOPR@RPTTOMES " + "\n");
            strSqlString.Append("                   WHERE FACTORY = '" + cdvFactory.Text + "' " + "\n");
            strSqlString.Append("                     AND OPER " + cdvStep.SelectedValueToQueryString + "\n");
            strSqlString.Append("                   GROUP BY FLOW " + "\n");
            strSqlString.Append("                  HAVING COUNT(OPER) = " + cdvStep.SelectCount + "\n");
            strSqlString.Append("                 )" + "\n");

            #region 상세 조회에 따른 SQL문 생성
            if (udcWIPCondition1.Text != "ALL" && udcWIPCondition1.Text != "")
                strSqlString.AppendFormat("  AND B.MAT_GRP_1 {0} " + "\n", udcWIPCondition1.SelectedValueToQueryString);

            if (udcWIPCondition2.Text != "ALL" && udcWIPCondition2.Text != "")
                strSqlString.AppendFormat("  AND B.MAT_GRP_2 {0} " + "\n", udcWIPCondition2.SelectedValueToQueryString);

            if (udcWIPCondition3.Text != "ALL" && udcWIPCondition3.Text != "")
                strSqlString.AppendFormat("  AND B.MAT_GRP_3 {0} " + "\n", udcWIPCondition3.SelectedValueToQueryString);

            if (udcWIPCondition4.Text != "ALL" && udcWIPCondition4.Text != "")
                strSqlString.AppendFormat("  AND B.MAT_GRP_4 {0} " + "\n", udcWIPCondition4.SelectedValueToQueryString);

            if (udcWIPCondition5.Text != "ALL" && udcWIPCondition5.Text != "")
                strSqlString.AppendFormat("  AND B.MAT_GRP_5 {0} " + "\n", udcWIPCondition5.SelectedValueToQueryString);

            if (udcWIPCondition6.Text != "ALL" && udcWIPCondition6.Text != "")
                strSqlString.AppendFormat("  AND B.MAT_GRP_6 {0} " + "\n", udcWIPCondition6.SelectedValueToQueryString);

            if (udcWIPCondition7.Text != "ALL" && udcWIPCondition7.Text != "")
                strSqlString.AppendFormat("  AND B.MAT_GRP_7 {0} " + "\n", udcWIPCondition7.SelectedValueToQueryString);

            if (udcWIPCondition8.Text != "ALL" && udcWIPCondition8.Text != "")
                strSqlString.AppendFormat("  AND B.MAT_GRP_8 {0} " + "\n", udcWIPCondition8.SelectedValueToQueryString);
            #endregion

            if (txtSearchProduct.Text.Trim() != "%" && txtSearchProduct.Text.Trim() != "")
            {
                strSqlString.AppendFormat("   AND B.MAT_ID LIKE '" + txtSearchProduct.Text + "'" + "\n");
            }

            strSqlString.Append(" ORDER BY OPER " + "\n");

            return strSqlString.ToString();
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

                spdData.DataSource = dt;

                spdData.RPT_AutoFit(false);
                
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
            this.SetFactory(cdvFactory.txtValue);
            cdvStep.sFactory = cdvFactory.txtValue;            
        }

        private void rdbMaster_CheckedChanged(object sender, EventArgs e)
        {
            if (rdbMaster.Checked)            
                cdvFromToDate.Enabled = false;            
            else
                cdvFromToDate.Enabled = true;
        }
    }
}
