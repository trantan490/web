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
    public partial class PRD010208 : Miracom.SmartWeb.UI.Controls.udcCUSReport002
    {
        private string strSelect;
        private string strDecode;

        /// <summary>
        /// 클  래  스: PRD010208<br/>
        /// 클래스요약: 삼성 MCP Arrange 현황<br/>
        /// 작  성  자: 배수민<br/>
        /// 최초작성일: 2010-12-16<br/>
        /// 상세  설명: 삼성 MCP Arrange 현황<br/>
        /// 변경  내용: <br/>
        /// 2012-02-01-김민우 : MRASRESSTS의 공정정보 RES_STS_9에서 RES_STS_8로 변경 
        /// 2012-05-15-임종우 : MCP, DDP, QDP PKG 분리 작업

        /// </summary>
        public PRD010208()
        {
            InitializeComponent();
            SortInit();
            GridColumnInit();

            this.SetFactory(GlobalVariable.gsAssyDefaultFactory);
            cdvFactory.Text = GlobalVariable.gsAssyDefaultFactory;

            strSelect = string.Empty;
            strDecode = string.Empty;
                        
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
        
            return true;

        }

        /// <summary>
        /// 2. 헤더 생성
        /// </summary>
        private void GridColumnInit()
        {

            spdData.RPT_ColumnInit();

            spdData.RPT_AddBasicColumn("Customer", 0, 0, Visibles.False, Frozen.True, Align.Left, Merge.True, Formatter.String, 60);
            spdData.RPT_AddBasicColumn("Family", 0, 1, Visibles.False, Frozen.True, Align.Left, Merge.True, Formatter.String, 60);
            spdData.RPT_AddBasicColumn("Package", 0, 2, Visibles.False, Frozen.True, Align.Left, Merge.True, Formatter.String, 60);
            spdData.RPT_AddBasicColumn("Pin_Type", 0, 3, Visibles.True, Frozen.True, Align.Left, Merge.True, Formatter.String, 150);
            spdData.RPT_AddBasicColumn("Type1", 0, 4, Visibles.False, Frozen.True, Align.Left, Merge.True, Formatter.String, 60);
            spdData.RPT_AddBasicColumn("Ttpe2", 0, 5, Visibles.True, Frozen.True, Align.Left, Merge.True, Formatter.String, 60);
            spdData.RPT_AddBasicColumn("LD Count", 0, 6, Visibles.False, Frozen.True, Align.Left, Merge.True, Formatter.String, 60);
            spdData.RPT_AddBasicColumn("Density", 0, 7, Visibles.False, Frozen.True, Align.Left, Merge.True, Formatter.String, 60);
            spdData.RPT_AddBasicColumn("Generation", 0, 8, Visibles.False, Frozen.True, Align.Left, Merge.True, Formatter.String, 60);
            spdData.RPT_AddBasicColumn("Product", 0, 9, Visibles.False, Frozen.True, Align.Left, Merge.True, Formatter.String, 60);
            spdData.RPT_AddBasicColumn("Cust_Device", 0, 10, Visibles.False, Frozen.True, Align.Left, Merge.True, Formatter.String, 60);
            spdData.RPT_AddBasicColumn("D/A_CAPA", 0, 11, Visibles.True, Frozen.False, Align.Right, Merge.True, Formatter.String, 65);
            spdData.RPT_AddBasicColumn("W/B_CAPA", 0, 12, Visibles.True, Frozen.False, Align.Right, Merge.True, Formatter.String, 65);
            spdData.RPT_AddBasicColumn("CAPA_difference", 0, 13, Visibles.True, Frozen.False, Align.Right, Merge.True, Formatter.String, 65);
            spdData.RPT_AddBasicColumn("D/A_Equipment", 0, 14, Visibles.True, Frozen.False, Align.Right, Merge.True, Formatter.String, 65);
            spdData.RPT_AddBasicColumn("W/B Equipment", 0, 15, Visibles.True, Frozen.False, Align.Right, Merge.True, Formatter.String, 65);
            spdData.RPT_AddBasicColumn("A0401(D/A1)", 0, 16, Visibles.True, Frozen.False, Align.Right, Merge.True, Formatter.String, 95);
            spdData.RPT_AddBasicColumn("A0501(QURE1)", 0, 17, Visibles.True, Frozen.False, Align.Right, Merge.True, Formatter.String, 95);
            spdData.RPT_AddBasicColumn("A0551(PLASMA1)", 0, 18, Visibles.True, Frozen.False, Align.Right, Merge.True, Formatter.String, 95);
            spdData.RPT_AddBasicColumn("A0601(W/B1)", 0, 19, Visibles.True, Frozen.False, Align.Right, Merge.True, Formatter.String, 95);
            spdData.RPT_AddBasicColumn("A0402(D/A2)", 0, 20, Visibles.True, Frozen.False, Align.Right, Merge.True, Formatter.String, 95);
            spdData.RPT_AddBasicColumn("A0502(QURE2)", 0, 21, Visibles.True, Frozen.False, Align.Right, Merge.True, Formatter.String, 95);
            spdData.RPT_AddBasicColumn("A0552(PLASMA2)", 0, 22, Visibles.True, Frozen.False, Align.Right, Merge.True, Formatter.String, 95);
            spdData.RPT_AddBasicColumn("A0602(W/B2)", 0, 23, Visibles.True, Frozen.False, Align.Right, Merge.True, Formatter.String, 95);
            spdData.RPT_AddBasicColumn("A0403(D/A3)", 0, 24, Visibles.True, Frozen.False, Align.Right, Merge.True, Formatter.String, 95);
            spdData.RPT_AddBasicColumn("A0503(QURE3)", 0, 25, Visibles.True, Frozen.False, Align.Right, Merge.True, Formatter.String, 95);
            spdData.RPT_AddBasicColumn("A0553(PLASMA3)", 0, 26, Visibles.True, Frozen.False, Align.Right, Merge.True, Formatter.String, 95);
            spdData.RPT_AddBasicColumn("A0603(W/B3)", 0, 27, Visibles.True, Frozen.False, Align.Right, Merge.True, Formatter.String, 95);
           
            spdData.RPT_ColumnConfigFromTable(btnSort); // Group항목이 있을 경우, 반드시 선언 해줄 것.
        }

        /// <summary>
        /// 3. Group By 정의
        /// </summary>
        private void SortInit()
        {

            ((udcTableForm)(this.btnSort.BindingForm)).AddRow("Customer", "MAT_GRP_1", "MAT.MAT_GRP_1 ", "MAT.MAT_GRP_1", false);
            ((udcTableForm)(this.btnSort.BindingForm)).AddRow("Family", "MAT_GRP_2", "MAT.MAT_GRP_2 ", "MAT.MAT_GRP_2", false);
            ((udcTableForm)(this.btnSort.BindingForm)).AddRow("Package", "MAT_GRP_3", "MAT.MAT_GRP_3", "MAT.MAT_GRP_3", false);
            ((udcTableForm)(this.btnSort.BindingForm)).AddRow("Pin_type", "PIN_TYPE", "MAT.MAT_CMF_10 AS PIN_TYPE", "MAT.MAT_CMF_10", true);
            ((udcTableForm)(this.btnSort.BindingForm)).AddRow("Type1", "MAT_GRP_4", "MAT.MAT_GRP_4 ", "MAT.MAT_GRP_4", false);
            ((udcTableForm)(this.btnSort.BindingForm)).AddRow("Type2", "FLOOR", "MAT.MAT_GRP_5 AS FLOOR ", "MAT.MAT_GRP_5", true);
            ((udcTableForm)(this.btnSort.BindingForm)).AddRow("LD Count", "MAT_GRP_6", "MAT.MAT_GRP_6 ", "MAT.MAT_GRP_6", false);
            ((udcTableForm)(this.btnSort.BindingForm)).AddRow("Density", "MAT_GRP_7", "MAT.MAT_GRP_7 ", "MAT.MAT_GRP_7", false);
            ((udcTableForm)(this.btnSort.BindingForm)).AddRow("Generation", "MAT_GRP_8", "MAT.MAT_GRP_8","MAT.MAT_GRP_8", false);
            ((udcTableForm)(this.btnSort.BindingForm)).AddRow("Product", "MAT_ID", "MAT.MAT_ID", "MAT.MAT_ID", false);
            ((udcTableForm)(this.btnSort.BindingForm)).AddRow("Cust_Device", "MAT_CMF_7", "MAT.MAT_CMF_7", "MAT.MAT_CMF_7", false);
           
        }
        
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
            
            udcTableForm tableForm = (udcTableForm)btnSort.BindingForm;

            QueryCond1 = tableForm.SelectedValueToQueryContainNull;
            QueryCond2 = tableForm.SelectedValue2ToQueryContainNull;
            QueryCond3 = tableForm.SelectedValue3ToQueryContainNull;
            
            strSqlString.Append("  SELECT "+ QueryCond1 + "\n");
            strSqlString.Append("       , SUM(DA_CAPA) AS DA_CAPA " + "\n");
            strSqlString.Append("       , SUM(WB_CAPA) AS WB_CAPA " + "\n");
            strSqlString.Append("       , SUM(CAPA_차이) AS CAPA_차이 " + "\n");
            strSqlString.Append("       , SUM(CNT_DA) AS CNT_DA " + "\n");
            strSqlString.Append("       , SUM(CNT_WB) AS CNT_WB " + "\n");

            if (ckbKpcs.Checked == true)
            {
                strSqlString.Append("       , ROUND(SUM(A0401) / 1000, 1) AS A0401 " + "\n");
                strSqlString.Append("       , ROUND(SUM(A0501) / 1000, 1) AS A0501 " + "\n");
                strSqlString.Append("       , ROUND(SUM(A0551) / 1000, 1) AS A0551 " + "\n");
                strSqlString.Append("       , ROUND(SUM(A0601) / 1000, 1) AS A0601 " + "\n");
                strSqlString.Append("       , ROUND(SUM(A0402) / 1000, 1) AS A0402 " + "\n");
                strSqlString.Append("       , ROUND(SUM(A0502) / 1000, 1) AS A0502 " + "\n");
                strSqlString.Append("       , ROUND(SUM(A0552) / 1000, 1) AS A0552 " + "\n");
                strSqlString.Append("       , ROUND(SUM(A0602) / 1000, 1) AS A0602 " + "\n");
                strSqlString.Append("       , ROUND(SUM(A0403) / 1000, 1) AS A0403 " + "\n");
                strSqlString.Append("       , ROUND(SUM(A0503) / 1000, 1) AS A0503 " + "\n");
                strSqlString.Append("       , ROUND(SUM(A0553) / 1000, 1) AS A0553 " + "\n");
                strSqlString.Append("       , ROUND(SUM(A0603) / 1000, 1) AS A0603 " + "\n");
            }
            else
            {
                strSqlString.Append("       , SUM(A0401) AS A0401 " + "\n");
                strSqlString.Append("       , SUM(A0501) AS A0501 " + "\n");
                strSqlString.Append("       , SUM(A0551) AS A0551 " + "\n");
                strSqlString.Append("       , SUM(A0601) AS A0601 " + "\n");
                strSqlString.Append("       , SUM(A0402) AS A0402 " + "\n");
                strSqlString.Append("       , SUM(A0502) AS A0502 " + "\n");
                strSqlString.Append("       , SUM(A0552) AS A0552 " + "\n");
                strSqlString.Append("       , SUM(A0602) AS A0602 " + "\n");
                strSqlString.Append("       , SUM(A0403) AS A0403 " + "\n");
                strSqlString.Append("       , SUM(A0503) AS A0503 " + "\n");
                strSqlString.Append("       , SUM(A0553) AS A0553 " + "\n");
                strSqlString.Append("       , SUM(A0603) AS A0603 " + "\n");
            }
          
                strSqlString.Append("    FROM " + "\n");
                strSqlString.Append("        (SELECT " + QueryCond2+ "\n");
                strSqlString.Append("              , DECODE(RES.RES_GRP_3, 'DIE ATTACH', RES.EQP_CNT * 35, 0) AS DA_CAPA" + "\n");
                strSqlString.Append("              , DECODE(RES.RES_GRP_3, 'WIRE BOND', RES.EQP_CNT * 10, 0) AS WB_CAPA" + "\n");
                strSqlString.Append("              , DECODE(RES.RES_GRP_3, 'WIRE BOND', RES.EQP_CNT * 10, 0) - DECODE(RES.RES_GRP_3, 'DIE ATTACH', RES.EQP_CNT * 35, 0) AS CAPA_차이" + "\n");
                strSqlString.Append("              , DECODE(RES.RES_GRP_3, 'DIE ATTACH', RES.EQP_CNT, 0) AS CNT_DA " + "\n");
                strSqlString.Append("              , DECODE(RES.RES_GRP_3, 'WIRE BOND', RES.EQP_CNT, 0) AS CNT_WB " + "\n");
                strSqlString.Append("              , SUM(DECODE(LOT.OPER, 'A0401', LOT.QTY_1, 0)) AS A0401 " + "\n");
                strSqlString.Append("              , SUM(DECODE(LOT.OPER, 'A0501', LOT.QTY_1, 0)) AS A0501 " + "\n");
                strSqlString.Append("              , SUM(DECODE(LOT.OPER, 'A0551', LOT.QTY_1, 0)) AS A0551 " + "\n");
                strSqlString.Append("              , SUM(DECODE(LOT.OPER, 'A0601', LOT.QTY_1, 0)) AS A0601 " + "\n");
                strSqlString.Append("              , SUM(DECODE(LOT.OPER, 'A0402', LOT.QTY_1, 0)) AS A0402 " + "\n");
                strSqlString.Append("              , SUM(DECODE(LOT.OPER, 'A0502', LOT.QTY_1, 0)) AS A0502 " + "\n");
                strSqlString.Append("              , SUM(DECODE(LOT.OPER, 'A0552', LOT.QTY_1, 0)) AS A0552 " + "\n");
                strSqlString.Append("              , SUM(DECODE(LOT.OPER, 'A0602', LOT.QTY_1, 0)) AS A0602 " + "\n");
                strSqlString.Append("              , SUM(DECODE(LOT.OPER, 'A0403', LOT.QTY_1, 0)) AS A0403 " + "\n");
                strSqlString.Append("              , SUM(DECODE(LOT.OPER, 'A0503', LOT.QTY_1, 0)) AS A0503 " + "\n");
                strSqlString.Append("              , SUM(DECODE(LOT.OPER, 'A0553', LOT.QTY_1, 0)) AS A0553 " + "\n");
                strSqlString.Append("              , SUM(DECODE(LOT.OPER, 'A0603', LOT.QTY_1, 0)) AS A0603 " + "\n");
                strSqlString.Append("         FROM " + "\n");
                strSqlString.Append("             (SELECT MAT_ID " + "\n");
                strSqlString.Append("                   , FACTORY " + "\n");
                strSqlString.Append("                   , OPER " + "\n");
                strSqlString.Append("                   , QTY_1 " + "\n");
                strSqlString.Append("                FROM RWIPLOTSTS " + "\n");
                strSqlString.Append("               WHERE 1 =1 " + "\n");
                strSqlString.Append("                 AND OWNER_CODE = 'PROD' " + "\n");
                strSqlString.Append("                 AND LOT_DEL_FLAG = ' ' " + "\n");
                strSqlString.Append("                 AND MAT_VER = 1 " + "\n");
                strSqlString.Append("                 AND FACTORY  = '" + GlobalVariable.gsAssyDefaultFactory + "'" + "\n");
                strSqlString.Append("                 AND OPER IN ('A0401', 'A0501', 'A0551', 'A0601', 'A0402', 'A0502', 'A0552', 'A0602', 'A0403', 'A0503', 'A0553','A0603') " + "\n");
                strSqlString.Append("                 AND MAT_ID LIKE 'SE%' " + "\n");
                strSqlString.Append("             ) LOT, " + "\n");
                strSqlString.Append("             (SELECT DISTINCT RES_STS_2 AS MAT_ID " + "\n");
                strSqlString.Append("                    ,FACTORY " + "\n");
                strSqlString.Append("                    ,RES_GRP_3 " + "\n");
                strSqlString.Append("                    ,RES_STS_8 AS OPER " + "\n");
                strSqlString.Append("                    ,COUNT(RES_ID) AS EQP_CNT " + "\n");
                strSqlString.Append("                FROM MRASRESDEF " + "\n");
                strSqlString.Append("               WHERE 1 = 1 " + "\n");

                strSqlString.Append("                 AND FACTORY = '" + GlobalVariable.gsAssyDefaultFactory + "' " + "\n");
                strSqlString.Append("                 AND RES_CMF_9 = 'Y' " + "\n");
                strSqlString.Append("                 AND DELETE_FLAG = ' ' " + "\n");
                strSqlString.Append("                 AND RES_GRP_3 IN ('DIE ATTACH','WIRE BOND') " + "\n");
                strSqlString.Append("                 AND RES_CMF_20 <= TO_CHAR(SYSDATE,'YYYYMMDD')  " + "\n");
                strSqlString.Append("            GROUP BY FACTORY ,RES_STS_2, RES_GRP_3, RES_STS_8 ) RES , MWIPMATDEF MAT  " + "\n");
                strSqlString.Append("         WHERE 1 = 1  " + "\n");
                strSqlString.Append("           AND LOT.FACTORY = MAT.FACTORY " + "\n");
                strSqlString.Append("           AND LOT.FACTORY = RES.FACTORY " + "\n");
                strSqlString.Append("           AND MAT.MAT_ID = LOT.MAT_ID " + "\n");
                strSqlString.Append("           AND MAT.MAT_ID = RES.MAT_ID " + "\n");
                strSqlString.Append("           AND RES.OPER = LOT.OPER " + "\n");
                strSqlString.Append("           AND MAT.MAT_GRP_3 IN ('MCP', 'DDP', 'QDP') " + "\n");
 
            #region 상세 조회에 따른 SQL문 생성
                if (udcWIPCondition1.Text != "ALL" && udcWIPCondition1.Text != "")
                    strSqlString.AppendFormat("  AND   MAT.MAT_GRP_1 {0} " + "\n", udcWIPCondition1.SelectedValueToQueryString);

                if (udcWIPCondition2.Text != "ALL" && udcWIPCondition2.Text != "")
                    strSqlString.AppendFormat("  AND   MAT.MAT_GRP_2 {0} " + "\n", udcWIPCondition2.SelectedValueToQueryString);

                if (udcWIPCondition3.Text != "ALL" && udcWIPCondition3.Text != "")
                    strSqlString.AppendFormat("  AND   MAT.MAT_GRP_3 {0} " + "\n", udcWIPCondition3.SelectedValueToQueryString);

                if (udcWIPCondition4.Text != "ALL" && udcWIPCondition4.Text != "")
                    strSqlString.AppendFormat("  AND   MAT.MAT_GRP_4 {0} " + "\n", udcWIPCondition4.SelectedValueToQueryString);

                if (udcWIPCondition5.Text != "ALL" && udcWIPCondition5.Text != "")
                    strSqlString.AppendFormat("  AND   MAT.MAT_GRP_5 {0} " + "\n", udcWIPCondition5.SelectedValueToQueryString);

                if (udcWIPCondition6.Text != "ALL" && udcWIPCondition6.Text != "")
                    strSqlString.AppendFormat("  AND   MAT.MAT_GRP_6 {0} " + "\n", udcWIPCondition6.SelectedValueToQueryString);

                if (udcWIPCondition7.Text != "ALL" && udcWIPCondition7.Text != "")
                    strSqlString.AppendFormat("  AND   MAT.MAT_GRP_7 {0} " + "\n", udcWIPCondition7.SelectedValueToQueryString);

                if (udcWIPCondition8.Text != "ALL" && udcWIPCondition8.Text != "")
                    strSqlString.AppendFormat("  AND   MAT.MAT_GRP_8 {0} " + "\n", udcWIPCondition8.SelectedValueToQueryString);

                if (udcWIPCondition9.Text != "ALL" && udcWIPCondition9.Text != "")
                    strSqlString.AppendFormat("  AND   MAT.MAT_GRP_9 {0} " + "\n", udcWIPCondition9.SelectedValueToQueryString);
                #endregion

                strSqlString.Append("      GROUP BY " + QueryCond3 + ",RES.RES_GRP_3, RES.EQP_CNT" + "\n");
                strSqlString.Append("        ) " + "\n");
                strSqlString.Append("         WHERE 1 = 1  " + "\n");
                strSqlString.Append("      GROUP BY " + QueryCond1 + "\n");
                strSqlString.Append("      ORDER BY " + QueryCond1 + "\n");

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

        #endregion

        private void btnView_Click(object sender, EventArgs e)
        {                       
            
            DataTable dt = null;

            if (CheckField() == true)
            {
                GridColumnInit();
                spdData_Sheet1.RowCount = 0;
            }

            if (cdvFactory.Text.Equals("HMKE1") || cdvFactory.Text.Equals(GlobalVariable.gsTestDefaultFactory) || cdvFactory.Text.Equals("FGS")) 
            {
                CmnFunction.ShowMsgBox(RptMessages.GetMessage("STD052", GlobalVariable.gcLanguage));
                return;
            }

            try
            {
                LoadingPopUp.LoadIngPopUpShow(this);

                this.Refresh();

                dt = CmnFunction.oComm.GetFuncDataTable("DYNAMIC", MakeSqlString()); //데이터 저장

                if (dt.Rows.Count == 0)
                {
                    dt.Dispose(); 
                    LoadingPopUp.LoadingPopUpHidden();
                    CmnFunction.ShowMsgBox(RptMessages.GetMessage("STD010", GlobalVariable.gcLanguage));
                    return;
                }

                
                
                //1.Griid 합계 표시
                int[] rowType = spdData.RPT_DataBindingWithSubTotal(dt, 0, 0, 1, null, null, btnSort);
          
                //2. Total부분 셀머지
                spdData.RPT_FillDataSelectiveCells("Total", 0, 11, 0, 1, true, Align.Center, VerticalAlign.Center);   
                
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
            this.SetFactory(cdvFactory.txtValue);

        }

       
    }
}
