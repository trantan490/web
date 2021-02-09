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
    public partial class PRD010905 : Miracom.SmartWeb.UI.Controls.udcCUSReport002
    {
        /// <summary>
        /// 클  래  스: PRD010905<br/>
        /// 클래스요약: 삼성 MCP  KIT 율 조회<br/>
        /// 작  성  자: 하나마이크론 임종우<br/>
        /// 최초작성일: 2010-12-12<br/>
        /// 상세  설명: 삼성 MCP  KIT 율 조회 화면<br/>
        /// 변경  내용: <br/>
        /// 2011-12-21-임종우 : KIT 율 100%미만 색상 표시 (임태성 요청)
        /// 2012-05-15-임종우 : MCP, DDP, QDP PKG 분리 작업
        /// 2012-08-23-임종우 : DP, DA 기준의 KIP율 추가 및 2차, 3차 투입필요 부분 추가 (전영준 요청)
        /// </summary>
        /// 
        public PRD010905()
        {
            InitializeComponent();
            this.SetFactory(GlobalVariable.gsAssyDefaultFactory);
            cdvFactory.Text = GlobalVariable.gsAssyDefaultFactory;
            //SortInit();
            GridColumnInit();
        }

        #region 초기화 및 유효성 검사
        /// <summary 1. 유효성 검사>
        ///
        /// </summary>        
        private Boolean CheckField()
        {
            if (cdvFactory.Text.TrimEnd() == "")
            {
                CmnFunction.ShowMsgBox(RptMessages.GetMessage("STD001", GlobalVariable.gcLanguage));
                return false;
            }

            return true;
        }

        /// <summary 2. 헤더 생성> 
        ///
        /// </summary>
        private void GridColumnInit()
        {
            spdData.ActiveSheet.ColumnHeader.Rows.Count = 1;
            
            try
            {
                spdData.RPT_ColumnInit();
                spdData.RPT_AddBasicColumn("PIN TYPE", 0, 0, Visibles.True, Frozen.True, Align.Left, Merge.False, Formatter.String, 60);
                spdData.RPT_AddBasicColumn("PACK CODE", 0, 1, Visibles.True, Frozen.True, Align.Center, Merge.False, Formatter.String, 70);
                spdData.RPT_AddBasicColumn("PRODUCT", 0, 2, Visibles.True, Frozen.True, Align.Left, Merge.False, Formatter.String, 60);
                spdData.RPT_AddBasicColumn("TYPE1", 0, 3, Visibles.True, Frozen.True, Align.Center, Merge.False, Formatter.String, 60);
                spdData.RPT_AddBasicColumn("TYPE2", 0, 4, Visibles.True, Frozen.True, Align.Center, Merge.False, Formatter.String, 60);

                spdData.RPT_AddBasicColumn("KIT 율", 0, 5, Visibles.True, Frozen.False, Align.Center, Merge.False, Formatter.String, 60);
                spdData.RPT_AddBasicColumn("TOTAL standard", 1, 5, Visibles.True, Frozen.False, Align.Center, Merge.False, Formatter.Double1, 70);
                spdData.RPT_AddBasicColumn("DP standard", 1, 6, Visibles.True, Frozen.False, Align.Center, Merge.False, Formatter.Double1, 60);
                spdData.RPT_AddBasicColumn("DA standard", 1, 7, Visibles.True, Frozen.False, Align.Center, Merge.False, Formatter.Double1, 60);
                spdData.RPT_MerageHeaderColumnSpan(0, 5, 3);

                spdData.RPT_AddBasicColumn("Input required", 0, 8, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.String, 60);
                spdData.RPT_AddBasicColumn("TOTAL standard", 1, 8, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 70);
                spdData.RPT_AddBasicColumn("DP standard", 1, 9, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 60);
                spdData.RPT_AddBasicColumn("DA standard", 1, 10, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 60);
                spdData.RPT_MerageHeaderColumnSpan(0, 8, 3);

                spdData.RPT_AddBasicColumn("TOTAL", 0, spdData.ActiveSheet.ColumnCount, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 60);
                spdData.RPT_AddBasicColumn("A0000", 0, spdData.ActiveSheet.ColumnCount, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 60);
                spdData.RPT_AddBasicColumn("A0020", 0, spdData.ActiveSheet.ColumnCount, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 60);
                spdData.RPT_AddBasicColumn("A0040", 0, spdData.ActiveSheet.ColumnCount, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 60);
                spdData.RPT_AddBasicColumn("A0200", 0, spdData.ActiveSheet.ColumnCount, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 60);
                spdData.RPT_AddBasicColumn("A0250", 0, spdData.ActiveSheet.ColumnCount, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 60);
                spdData.RPT_AddBasicColumn("A0300", 0, spdData.ActiveSheet.ColumnCount, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 60);
                spdData.RPT_AddBasicColumn("A0401", 0, spdData.ActiveSheet.ColumnCount, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 60);
                spdData.RPT_AddBasicColumn("A0402", 0, spdData.ActiveSheet.ColumnCount, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 60);
                spdData.RPT_AddBasicColumn("A0403", 0, spdData.ActiveSheet.ColumnCount, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 60);
                spdData.RPT_AddBasicColumn("A0501", 0, spdData.ActiveSheet.ColumnCount, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 60);
                spdData.RPT_AddBasicColumn("A0502", 0, spdData.ActiveSheet.ColumnCount, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 60);
                spdData.RPT_AddBasicColumn("A0503", 0, spdData.ActiveSheet.ColumnCount, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 60);
                spdData.RPT_AddBasicColumn("A0551", 0, spdData.ActiveSheet.ColumnCount, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 60);
                spdData.RPT_AddBasicColumn("A0552", 0, spdData.ActiveSheet.ColumnCount, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 60);
                spdData.RPT_AddBasicColumn("A0553", 0, spdData.ActiveSheet.ColumnCount, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 60);
                spdData.RPT_AddBasicColumn("A0601", 0, spdData.ActiveSheet.ColumnCount, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 60);
                spdData.RPT_AddBasicColumn("A0602", 0, spdData.ActiveSheet.ColumnCount, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 60);

                spdData.RPT_MerageHeaderRowSpan(0, 0, 2);
                spdData.RPT_MerageHeaderRowSpan(0, 1, 2);
                spdData.RPT_MerageHeaderRowSpan(0, 2, 2);
                spdData.RPT_MerageHeaderRowSpan(0, 3, 2);
                spdData.RPT_MerageHeaderRowSpan(0, 4, 2);

                for (int i = 11; i < spdData.ActiveSheet.ColumnCount; i++)
                {
                    spdData.RPT_MerageHeaderRowSpan(0, i, 2);
                }
                
                //spdData.RPT_ColumnConfigFromTable(btnSort); //Group항목이 있을경우 반드시 선언해줄것.
            }

            catch (Exception ex)
            {
                CmnFunction.ShowMsgBox(ex.Message);
            }
        }

        /// <summary 3. SortInit>
        /// 
        /// </summary>
        private void SortInit()
        {
        }
        #endregion


        #region SQL 쿼리 Build
        /// <summary 4. SQL 쿼리 Build>
        /// 
        /// </summary>
        /// <returns> strSqlString </returns>

        private string MakeSqlString()
        {
            StringBuilder strSqlString = new StringBuilder();

            string QueryCond1;
            string QueryCond2;

            udcTableForm tableForm = (udcTableForm)btnSort.BindingForm;
            QueryCond1 = tableForm.SelectedValueToQueryContainNull;
            QueryCond2 = tableForm.SelectedValue2ToQueryContainNull;

            // Total KIT율 로직 -> A000 ~ AZ010 전체 공정에 대한 재공을 가지고 계산 한다. 1차:x     2차: (2차 TTL / 1차 TTL)     3차 : (3차 TTL / (1차 TTL + Middle))
            // DP KIT율 로직 ->  A0040 ~ A0602 공정에 대한 재공만 가지고 계산 한다.       1차:x     2차: (2차 TTL / 1차 TTL)     3차 : (3차 TTL / (1차 TTL + Middle))
            // DA KIT율 로직 ->  A0401 ~ A0602 공정에 대한 재공만 가지고 계산 한다.       1차:x     2차: (2차 TTL / 1차 TTL)     3차 : (3차 TTL / (1차 TTL + Middle))
            // Total 투입필요 로직 -> A000 ~ AZ010 전체 공정에 대한 재공을 가지고 계산 한다. 1차:x     2차: (1차 TTL - 2차 TTL)     3차 : ((1차 TTL + Middle) - 3차 TTL)
            // DP 투입필요 로직 ->  A0040 ~ A0602 공정에 대한 재공만 가지고 계산 한다.       1차:x     2차: (1차 TTL - 2차 TTL)     3차 : ((1차 TTL + Middle) - 3차 TTL)
            // DA 투입필요 로직 ->  A0401 ~ A0602 공정에 대한 재공만 가지고 계산 한다.       1차:x     2차: (1차 TTL - 2차 TTL)     3차 : ((1차 TTL + Middle) - 3차 TTL)
            strSqlString.Append("SELECT A.MAT_CMF_10, A.MAT_CMF_11, A.MAT_ID, A.MAT_GRP_4, A.MAT_GRP_5 " + "\n");
            strSqlString.Append("     , CASE WHEN A.MAT_GRP_5 = '2nd' AND NVL(ST1, 0) != 0 THEN ROUND((TOTAL / ST1) * 100, 1) " + "\n");
            strSqlString.Append("            WHEN A.MAT_GRP_5 = '3rd' AND NVL(ST1_MIDDLE_TTL, 0) != 0 THEN ROUND((TOTAL / ST1_MIDDLE_TTL) * 100, 1) " + "\n");
            strSqlString.Append("            ELSE 0" + "\n");
            strSqlString.Append("       END AS KIT_TTL " + "\n");
            strSqlString.Append("     , CASE WHEN A.MAT_GRP_5 = '2nd' AND NVL(ST1_DP, 0) != 0 THEN ROUND((DP / ST1_DP) * 100, 1) " + "\n");
            strSqlString.Append("            WHEN A.MAT_GRP_5 = '3rd' AND NVL(ST1_MIDDLE_DP, 0) != 0 THEN ROUND((DP / ST1_MIDDLE_DP) * 100, 1) " + "\n");
            strSqlString.Append("            ELSE 0" + "\n");
            strSqlString.Append("       END AS KIT_DP " + "\n");
            strSqlString.Append("     , CASE WHEN A.MAT_GRP_5 = '2nd' AND NVL(ST1_DA, 0) != 0 THEN ROUND((DA / ST1_DA) * 100, 1)  " + "\n");
            strSqlString.Append("            WHEN A.MAT_GRP_5 = '3rd' AND NVL(ST1_MIDDLE_DA, 0) != 0 THEN ROUND((DA / ST1_MIDDLE_DA) * 100, 1) " + "\n");
            strSqlString.Append("            ELSE 0" + "\n");
            strSqlString.Append("       END AS KIT_DA " + "\n");
            strSqlString.Append("     , CASE WHEN A.MAT_GRP_5 = '2nd' THEN ST1 - TOTAL " + "\n");
            strSqlString.Append("            WHEN A.MAT_GRP_5 = '3rd' THEN ST1_MIDDLE_TTL - TOTAL " + "\n");
            strSqlString.Append("            ELSE 0" + "\n");
            strSqlString.Append("       END AS IN_TTL " + "\n");
            strSqlString.Append("     , CASE WHEN A.MAT_GRP_5 = '2nd' THEN ST1_DP - DP " + "\n");
            strSqlString.Append("            WHEN A.MAT_GRP_5 = '3rd' THEN ST1_MIDDLE_DP - DP " + "\n");
            strSqlString.Append("            ELSE 0" + "\n");
            strSqlString.Append("       END AS IN_DP " + "\n");
            strSqlString.Append("     , CASE WHEN A.MAT_GRP_5 = '2nd' THEN ST1_DA - DA " + "\n");
            strSqlString.Append("            WHEN A.MAT_GRP_5 = '3rd' THEN ST1_MIDDLE_DA - DA " + "\n");
            strSqlString.Append("            ELSE 0" + "\n");
            strSqlString.Append("       END AS IN_DA " + "\n");
            strSqlString.Append("     , TOTAL, V0, V1, V2, V3, V4, V5, V6, V7, V8, V9, V10, V11, V12, V13, V14, V15, V16 " + "\n");            
            strSqlString.Append("  FROM ( " + "\n");
            strSqlString.Append("        SELECT B.MAT_CMF_10, B.MAT_CMF_11, A.MAT_ID, B.MAT_GRP_4, B.MAT_GRP_5 " + "\n");
            strSqlString.Append("             , SUM(QTY_1) AS TOTAL " + "\n");
            strSqlString.Append("             , SUM(DECODE(A.OPER, 'A0000', QTY_1, 0))V0 " + "\n");
            strSqlString.Append("             , SUM(DECODE(A.OPER, 'A0020', QTY_1, 0))V1 " + "\n");
            strSqlString.Append("             , SUM(DECODE(A.OPER, 'A0040', QTY_1, 0))V2 " + "\n");
            strSqlString.Append("             , SUM(DECODE(A.OPER, 'A0200', QTY_1, 0))V3 " + "\n");
            strSqlString.Append("             , SUM(DECODE(A.OPER, 'A0250', QTY_1, 0))V4 " + "\n");
            strSqlString.Append("             , SUM(DECODE(A.OPER, 'A0300', QTY_1, 0))V5 " + "\n");
            strSqlString.Append("             , SUM(DECODE(A.OPER, 'A0401', QTY_1, 0))V6 " + "\n");
            strSqlString.Append("             , SUM(DECODE(A.OPER, 'A0402', QTY_1, 0))V7 " + "\n");
            strSqlString.Append("             , SUM(DECODE(A.OPER, 'A0403', QTY_1, 0))V8 " + "\n");
            strSqlString.Append("             , SUM(DECODE(A.OPER, 'A0501', QTY_1, 0))V9 " + "\n");
            strSqlString.Append("             , SUM(DECODE(A.OPER, 'A0502', QTY_1, 0))V10 " + "\n");
            strSqlString.Append("             , SUM(DECODE(A.OPER, 'A0503', QTY_1, 0))V11 " + "\n");
            strSqlString.Append("             , SUM(DECODE(A.OPER, 'A0551', QTY_1, 0))V12 " + "\n");
            strSqlString.Append("             , SUM(DECODE(A.OPER, 'A0552', QTY_1, 0))V13 " + "\n");
            strSqlString.Append("             , SUM(DECODE(A.OPER, 'A0553', QTY_1, 0))V14 " + "\n");
            strSqlString.Append("             , SUM(DECODE(A.OPER, 'A0601', QTY_1, 0))V15 " + "\n");
            strSqlString.Append("             , SUM(DECODE(A.OPER, 'A0602', QTY_1, 0))V16 " + "\n");
            strSqlString.Append("             , SUM(CASE WHEN A.OPER BETWEEN 'A0040' AND 'A0602' THEN QTY_1 ELSE 0 END) AS DP " + "\n");
            strSqlString.Append("             , SUM(CASE WHEN A.OPER BETWEEN 'A0401' AND 'A0602' THEN QTY_1 ELSE 0 END) AS DA " + "\n");
            strSqlString.Append("          FROM RWIPLOTSTS A " + "\n");
            strSqlString.Append("             , MWIPMATDEF B " + "\n");
            strSqlString.Append("         WHERE 1=1 " + "\n");
            strSqlString.Append("           AND A.FACTORY = B.FACTORY " + "\n");
            strSqlString.Append("           AND A.MAT_ID = B.MAT_ID " + "\n");
            strSqlString.Append("           AND A.FACTORY = '" + cdvFactory.Text + "'" + "\n");
            strSqlString.Append("           AND A.LOT_DEL_FLAG = ' ' " + "\n");
            strSqlString.Append("           AND A.LOT_TYPE = 'W' " + "\n");
            strSqlString.Append("           AND A.MAT_ID LIKE 'SEK%' " + "\n");
            strSqlString.Append("           AND A.LOT_CMF_2 = 'SE' " + "\n");
            strSqlString.Append("           AND B.MAT_TYPE = 'FG' " + "\n");
            strSqlString.Append("           AND B.MAT_GRP_3 IN ('MCP', 'DDP', 'QDP') " + "\n");
            strSqlString.Append("           AND B.MAT_GRP_5 <> 'Merge' " + "\n");
            

            if (cbLotType.Text != "ALL")
            {
                strSqlString.Append("           AND A.LOT_CMF_5 LIKE '" + cbLotType.Text + "' " + "\n");
            }

            strSqlString.Append("         GROUP BY B.MAT_CMF_10, B.MAT_CMF_11, A.MAT_ID, B.MAT_GRP_4, B.MAT_GRP_5 " + "\n");
            strSqlString.Append("       ) A  " + "\n");
            strSqlString.Append("     , (  " + "\n");            
            strSqlString.Append("        SELECT B.MAT_CMF_11 " + "\n");
            strSqlString.Append("             , SUM(DECODE(B.MAT_GRP_5, '1st', QTY_1, 0)) AS ST1 " + "\n");
            strSqlString.Append("             , SUM(DECODE(B.MAT_GRP_5, 'Middle', QTY_1, 0)) AS MIDDLE " + "\n");
            strSqlString.Append("             , SUM(QTY_1) AS ST1_MIDDLE_TTL " + "\n");
            strSqlString.Append("             , SUM(CASE WHEN B.MAT_GRP_5 = '1st' AND A.OPER BETWEEN 'A0040' AND 'A0602' THEN QTY_1 ELSE 0 END) AS ST1_DP " + "\n");
            strSqlString.Append("             , SUM(CASE WHEN B.MAT_GRP_5 = '1st' AND A.OPER BETWEEN 'A0401' AND 'A0602' THEN QTY_1 ELSE 0 END) AS ST1_DA " + "\n");
            strSqlString.Append("             , SUM(CASE WHEN A.OPER BETWEEN 'A0040' AND 'A0602' THEN QTY_1 ELSE 0 END) AS ST1_MIDDLE_DP " + "\n");
            strSqlString.Append("             , SUM(CASE WHEN A.OPER BETWEEN 'A0401' AND 'A0602' THEN QTY_1 ELSE 0 END) AS ST1_MIDDLE_DA " + "\n");
            strSqlString.Append("          FROM RWIPLOTSTS A " + "\n");
            strSqlString.Append("             , MWIPMATDEF B " + "\n");
            strSqlString.Append("         WHERE 1=1 " + "\n");
            strSqlString.Append("           AND A.FACTORY = B.FACTORY " + "\n");
            strSqlString.Append("           AND A.MAT_ID = B.MAT_ID " + "\n");
            strSqlString.Append("           AND A.FACTORY = '" + cdvFactory.Text + "'" + "\n");
            strSqlString.Append("           AND A.LOT_DEL_FLAG = ' ' " + "\n");
            strSqlString.Append("           AND A.LOT_TYPE = 'W' " + "\n");
            strSqlString.Append("           AND A.MAT_ID LIKE 'SEK%' " + "\n");
            strSqlString.Append("           AND A.LOT_CMF_2 = 'SE' " + "\n");
            strSqlString.Append("           AND B.MAT_TYPE = 'FG' " + "\n");
            strSqlString.Append("           AND B.MAT_GRP_3 IN ('MCP', 'DDP', 'QDP') " + "\n");
            strSqlString.Append("           AND B.MAT_GRP_5 IN ('1st','Middle') " + "\n");

            if (cbLotType.Text != "ALL")
            {
                strSqlString.Append("           AND A.LOT_CMF_5 LIKE '" + cbLotType.Text + "' " + "\n");
            }

            strSqlString.Append("         GROUP BY B.MAT_CMF_11 " + "\n");
            strSqlString.Append("       ) B  " + "\n");
            strSqlString.Append(" WHERE 1=1  " + "\n");
            strSqlString.Append("   AND A.MAT_CMF_11 = B.MAT_CMF_11(+) " + "\n");
            strSqlString.Append(" ORDER BY A.MAT_CMF_11, A.MAT_GRP_5, A.MAT_CMF_10, A.MAT_ID, A.MAT_GRP_4 " + "\n");
            

            if (GlobalVariable.gsUserGroup == "ADMIN_GROUP" || GlobalVariable.gsUserGroup == "HANA_ADMIN_GROUP")
            {
                System.Windows.Forms.Clipboard.SetText(strSqlString.ToString());
            }

            return strSqlString.ToString();
        }
        #endregion


        #region EVENT 처리
        /// <summary 5. View>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnView_Click(object sender, EventArgs e)
        {
            DataTable dt = null;

            if (CheckField() == false) return;

            try
            {
                LoadingPopUp.LoadIngPopUpShow(this);
                this.Refresh();
                GridColumnInit();
                dt = CmnFunction.oComm.GetFuncDataTable("DYNAMIC", MakeSqlString());

                if (dt.Rows.Count == 0)
                {
                    dt.Dispose();
                    LoadingPopUp.LoadingPopUpHidden();
                    CmnFunction.ShowMsgBox(RptMessages.GetMessage("STD010", GlobalVariable.gcLanguage));
                    return;
                }

                spdData.DataSource = dt;

                // 2011-12-21-임종우 : KIT율 100% 미만은 색상 표시 함 (임태성 요청)
                for(int i=0; i < spdData.ActiveSheet.RowCount; i++)
                {
                    if (spdData.ActiveSheet.Cells[i, 5].Value != null && Convert.ToInt32(spdData.ActiveSheet.Cells[i, 5].Value) < 100)
                    {
                        spdData.ActiveSheet.Rows[i].BackColor = Color.Red;
                    }
                }

                //4. Column Auto Fit
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



        /// <summary 6. Excel Export>
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
        #endregion
    }
}
