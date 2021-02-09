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


namespace Hana.TAT
{
    public partial class TAT050501 : Miracom.SmartWeb.UI.Controls.udcCUSReport002
    {
        private DataTable dtOper;

        /// <summary>
        /// 클  래  스: TAT050501<br/>
        /// 클래스요약: Lot별 TAT 조회<br/>
        /// 작  성  자: 하나마이크론 임종우<br/>
        /// 최초작성일: 2009-04-08<br/>
        /// 상세  설명: Lot별 TAT 조회<br/>
        /// 변경  내용: <br/>
        /// 변  경  자: <br />
        /// 2011-02-08-임종우 : TAT 단위 시간 단위 추가 표시 (임태성 요청)
        /// 2013-05-08-김민우 : Package2 추가
        /// 2013-10-17-김민우 : LOT TYPE ALL, P%, E% 구분으로변경
        /// 2015-01-06-임종우 : M% 공정 제외
        /// 2015-05-21-임종우 : 공정가져오는 부분 FLOW 정보에 따라 가져오도록 수정 함.
        /// 2017-12-22-임종우 : SubTotal, GrandTotal 평균값 구하기 Function 변경
        /// 2019-07-05-임종우 : 공정 순서대로 표시 되도록 수정 (박형순대리 요청)
        /// </summary>
        /// 
        public TAT050501()
        {
            InitializeComponent();
            cdvFromToDate.AutoBinding();
            SortInit();           
            GridColumnInit();            
        }
                
        #region 유효성 검사 및 초기화
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

        private String MakeOperTable()
        {
            StringBuilder strSqlString = new StringBuilder();

            //strSqlString.Append("SELECT OPER FROM MWIPOPRDEF WHERE FACTORY " + cdvFactory.SelectedValueToQueryString + " AND OPER NOT IN ('00001','00002') AND OPER NOT LIKE 'V%' AND OPER NOT LIKE 'M%' ORDER BY OPER" + "\n");

            // 2015-05-21-임종우 : 공정가져오는 부분 FLOW 정보에 따라 가져오도록 수정 함.
            // 2019-07-05-임종우 : 공정 순서대로 표시 되도록 수정
            strSqlString.Append("SELECT OPER " + "\n");
            strSqlString.Append("  FROM ( " + "\n");
            strSqlString.Append("        SELECT DISTINCT OPER " + "\n");
            strSqlString.Append("             , (SELECT OPER_CMF_2 FROM MWIPOPRDEF WHERE FACTORY = A.FACTORY AND OPER = A.OPER) AS SEQ " + "\n");
            strSqlString.Append("          FROM MWIPFLWOPR@RPTTOMES A" + "\n");                                               //-- B1 공장 데이타가 없음
            strSqlString.Append("         WHERE FACTORY " + cdvFactory.SelectedValueToQueryString + "\n");
            strSqlString.Append("           AND OPER NOT IN ('00001','00002') " + "\n");
            strSqlString.Append("           AND OPER NOT LIKE 'V%' " + "\n");
            strSqlString.Append("           AND OPER NOT LIKE 'M%' " + "\n");
            strSqlString.Append("           AND FLOW IN ( " + "\n");
            strSqlString.Append("                        SELECT DISTINCT FIRST_FLOW " + "\n");
            strSqlString.Append("                          FROM MWIPMATDEF " + "\n");
            strSqlString.Append("                         WHERE FACTORY " + cdvFactory.SelectedValueToQueryString + "\n");
            strSqlString.Append("                           AND DELETE_FLAG = ' ' " + "\n");
            strSqlString.Append("                           AND MAT_TYPE = 'FG' " + "\n");
            strSqlString.Append("                           AND MAT_ID LIKE '" + txtProduct.Text + "'" + "\n");

            #region 상세 조회에 따른 SQL문 생성
            if (cdvFactory.Text.Trim() == "HMKB1")
            {
                if (udcBUMPCondition1.Text != "ALL" && udcBUMPCondition1.Text != "")
                    strSqlString.AppendFormat("                           AND MAT_GRP_1 {0} " + "\n", udcBUMPCondition1.SelectedValueToQueryString);

                if (udcBUMPCondition2.Text != "ALL" && udcBUMPCondition2.Text != "")
                    strSqlString.AppendFormat("                           AND MAT_GRP_2 {0} " + "\n", udcBUMPCondition2.SelectedValueToQueryString);

                if (udcBUMPCondition3.Text != "ALL" && udcBUMPCondition3.Text != "")
                    strSqlString.AppendFormat("                           AND MAT_GRP_3 {0} " + "\n", udcBUMPCondition3.SelectedValueToQueryString);

                if (udcBUMPCondition4.Text != "ALL" && udcBUMPCondition4.Text != "")
                    strSqlString.AppendFormat("                           AND MAT_GRP_4 {0} " + "\n", udcBUMPCondition4.SelectedValueToQueryString);

                if (udcBUMPCondition5.Text != "ALL" && udcBUMPCondition5.Text != "")
                    strSqlString.AppendFormat("                           AND MAT_GRP_5 {0} " + "\n", udcBUMPCondition5.SelectedValueToQueryString);

                if (udcBUMPCondition6.Text != "ALL" && udcBUMPCondition6.Text != "")
                    strSqlString.AppendFormat("                           AND MAT_GRP_6 {0} " + "\n", udcBUMPCondition6.SelectedValueToQueryString);

                if (udcBUMPCondition7.Text != "ALL" && udcBUMPCondition7.Text != "")
                    strSqlString.AppendFormat("                           AND MAT_GRP_7 {0} " + "\n", udcBUMPCondition7.SelectedValueToQueryString);

                if (udcBUMPCondition8.Text != "ALL" && udcBUMPCondition8.Text != "")
                    strSqlString.AppendFormat("                           AND MAT_GRP_8 {0} " + "\n", udcBUMPCondition8.SelectedValueToQueryString);

                if (udcBUMPCondition9.Text != "ALL" && udcBUMPCondition9.Text != "")
                    strSqlString.AppendFormat("                           AND MAT_CMF_14 {0} " + "\n", udcBUMPCondition9.SelectedValueToQueryString);

                if (udcBUMPCondition10.Text != "ALL" && udcBUMPCondition10.Text != "")
                    strSqlString.AppendFormat("                           AND MAT_CMF_2 {0} " + "\n", udcBUMPCondition10.SelectedValueToQueryString);

                if (udcBUMPCondition11.Text != "ALL" && udcBUMPCondition11.Text != "")
                    strSqlString.AppendFormat("                           AND MAT_CMF_3 {0} " + "\n", udcBUMPCondition11.SelectedValueToQueryString);

                if (udcBUMPCondition12.Text != "ALL" && udcBUMPCondition12.Text != "")
                    strSqlString.AppendFormat("                           AND MAT_CMF_4 {0} " + "\n", udcBUMPCondition12.SelectedValueToQueryString);
            }
            else
            {
                //상세 조회에 따른 SQL문 생성                        
                if (udcWIPCondition1.Text != "ALL" && udcWIPCondition1.Text != "")
                    strSqlString.AppendFormat("                           AND MAT_GRP_1 {0}" + "\n", udcWIPCondition1.SelectedValueToQueryString);

                if (udcWIPCondition2.Text != "ALL" && udcWIPCondition2.Text != "")
                    strSqlString.AppendFormat("                           AND MAT_GRP_2 {0}" + "\n", udcWIPCondition2.SelectedValueToQueryString);

                if (udcWIPCondition3.Text != "ALL" && udcWIPCondition3.Text != "")
                    strSqlString.AppendFormat("                           AND MAT_GRP_3 {0} " + "\n", udcWIPCondition3.SelectedValueToQueryString);

                if (udcWIPCondition4.Text != "ALL" && udcWIPCondition4.Text != "")
                    strSqlString.AppendFormat("                           AND MAT_GRP_4 {0} " + "\n", udcWIPCondition4.SelectedValueToQueryString);

                if (udcWIPCondition5.Text != "ALL" && udcWIPCondition5.Text != "")
                    strSqlString.AppendFormat("                           AND MAT_GRP_5 {0} " + "\n", udcWIPCondition5.SelectedValueToQueryString);

                if (udcWIPCondition6.Text != "ALL" && udcWIPCondition6.Text != "")
                    strSqlString.AppendFormat("                           AND MAT_GRP_6 {0} " + "\n", udcWIPCondition6.SelectedValueToQueryString);

                if (udcWIPCondition7.Text != "ALL" && udcWIPCondition7.Text != "")
                    strSqlString.AppendFormat("                           AND MAT_GRP_7 {0} " + "\n", udcWIPCondition7.SelectedValueToQueryString);

                if (udcWIPCondition8.Text != "ALL" && udcWIPCondition8.Text != "")
                    strSqlString.AppendFormat("                           AND MAT_GRP_8 {0} " + "\n", udcWIPCondition8.SelectedValueToQueryString);

                if (udcWIPCondition9.Text != "ALL" && udcWIPCondition9.Text != "")
                    strSqlString.AppendFormat("                           AND MAT_GRP_9 {0} " + "\n", udcWIPCondition9.SelectedValueToQueryString);
            }
            #endregion
            
            strSqlString.Append("                       ) " + "\n");
            strSqlString.Append("       ) " + "\n");
            strSqlString.Append(" ORDER BY DECODE(SEQ, ' ', 99999, TO_NUMBER(SEQ)), OPER" + "\n");

            return strSqlString.ToString();
        }

        /// <summary>
        /// 2. 헤더 생성
        /// </summary>
        private void GridColumnInit()
        {
            try
            {
                spdData.ActiveSheet.ColumnHeader.Rows.Count = 1;
                spdData.RPT_ColumnInit();

                if (cdvFactory.txtValue.Equals("HMKB1"))
                {
                    spdData.RPT_AddBasicColumn("Customer", 0, 0, Visibles.False, Frozen.True, Align.Center, Merge.True, Formatter.String, 60);
                    spdData.RPT_AddBasicColumn("Bumping Type", 0, 1, Visibles.True, Frozen.True, Align.Center, Merge.True, Formatter.String, 70);
                    spdData.RPT_AddBasicColumn("Operation Flow", 0, 2, Visibles.True, Frozen.True, Align.Left, Merge.True, Formatter.String, 70);
                    spdData.RPT_AddBasicColumn("Layer classification", 0, 3, Visibles.True, Frozen.True, Align.Center, Merge.True, Formatter.String, 70);
                    spdData.RPT_AddBasicColumn("PKG Type", 0, 4, Visibles.False, Frozen.True, Align.Center, Merge.True, Formatter.String, 50);
                    spdData.RPT_AddBasicColumn("RDL Plating", 0, 5, Visibles.False, Frozen.True, Align.Center, Merge.True, Formatter.String, 50);
                    spdData.RPT_AddBasicColumn("Final Bump", 0, 6, Visibles.False, Frozen.True, Align.Center, Merge.True, Formatter.String, 50);
                    spdData.RPT_AddBasicColumn("Sub. Material", 0, 7, Visibles.False, Frozen.True, Align.Center, Merge.True, Formatter.String, 50);
                    spdData.RPT_AddBasicColumn("Size", 0, 8, Visibles.False, Frozen.True, Align.Center, Merge.True, Formatter.String, 50);
                    spdData.RPT_AddBasicColumn("Thickness", 0, 9, Visibles.False, Frozen.True, Align.Center, Merge.True, Formatter.String, 50);
                    spdData.RPT_AddBasicColumn("Flat Type", 0, 10, Visibles.False, Frozen.True, Align.Center, Merge.True, Formatter.String, 50);
                    spdData.RPT_AddBasicColumn("Wafer Orientation", 0, 11, Visibles.True, Frozen.False, Align.Center, Merge.True, Formatter.String, 50);
                }
                else
                {
                    spdData.RPT_AddBasicColumn("Customer", 0, 0, Visibles.False, Frozen.True, Align.Center, Merge.True, Formatter.String, 60);
                    spdData.RPT_AddBasicColumn("Family", 0, 1, Visibles.False, Frozen.True, Align.Center, Merge.True, Formatter.String, 60);
                    spdData.RPT_AddBasicColumn("Package", 0, 2, Visibles.False, Frozen.True, Align.Center, Merge.True, Formatter.String, 60);
                    spdData.RPT_AddBasicColumn("Type1", 0, 3, Visibles.False, Frozen.True, Align.Center, Merge.True, Formatter.String, 60);
                    spdData.RPT_AddBasicColumn("Type2", 0, 4, Visibles.False, Frozen.True, Align.Center, Merge.True, Formatter.String, 60);
                    spdData.RPT_AddBasicColumn("LD Count", 0, 5, Visibles.False, Frozen.True, Align.Center, Merge.True, Formatter.String, 60);
                    spdData.RPT_AddBasicColumn("Density", 0, 6, Visibles.False, Frozen.True, Align.Center, Merge.True, Formatter.String, 60);
                    spdData.RPT_AddBasicColumn("Generation", 0, 7, Visibles.False, Frozen.True, Align.Center, Merge.True, Formatter.String, 70);
                    spdData.RPT_AddBasicColumn("Pin Type", 0, 8, Visibles.True, Frozen.True, Align.Left, Merge.True, Formatter.String, 70);
                    spdData.RPT_AddBasicColumn("Product", 0, 9, Visibles.True, Frozen.True, Align.Left, Merge.True, Formatter.String, 80);
                    spdData.RPT_AddBasicColumn("Cust Device", 0, 10, Visibles.False, Frozen.True, Align.Left, Merge.True, Formatter.String, 70);
                    spdData.RPT_AddBasicColumn("Package2", 0, 11, Visibles.False, Frozen.True, Align.Left, Merge.True, Formatter.String, 70);
                }

                spdData.RPT_AddBasicColumn("LOT ID", 0, 12, Visibles.True, Frozen.True, Align.Center, Merge.True, Formatter.String, 80);
                spdData.RPT_AddBasicColumn("Ship Factory", 0, 13, Visibles.True, Frozen.False, Align.Center, Merge.True, Formatter.String, 80);
                spdData.RPT_AddBasicColumn("Ship Qty", 0, 14, Visibles.True, Frozen.False, Align.Center, Merge.True, Formatter.Number, 80);
                spdData.RPT_AddBasicColumn("TAT", 0, 15, Visibles.True, Frozen.False, Align.Center, Merge.True, Formatter.Double2, 50);

                // header 의 Colnum Count
                int headerCount = 16;

                // Header 에 공정 추가하기

                if (dtOper != null)
                {
                    for (int i = 0; i < dtOper.Rows.Count; i++)
                    {
                        spdData.RPT_AddBasicColumn(dtOper.Rows[i][0].ToString(), 0, headerCount, Visibles.True, Frozen.False, Align.Center, Merge.True, Formatter.String, 80);
                        spdData.RPT_AddBasicColumn("Wait", 1, headerCount, Visibles.True, Frozen.False, Align.Center, Merge.True, Formatter.Double2, 40);
                        headerCount++;

                        spdData.RPT_AddBasicColumn("Run", 1, headerCount, Visibles.True, Frozen.False, Align.Center, Merge.True, Formatter.Double2, 40);
                        headerCount++;

                        spdData.RPT_MerageHeaderColumnSpan(0, headerCount - 2, 2);

                    }
                }

                spdData.RPT_MerageHeaderRowSpan(0, 0, 2);
                spdData.RPT_MerageHeaderRowSpan(0, 1, 2);
                spdData.RPT_MerageHeaderRowSpan(0, 2, 2);
                spdData.RPT_MerageHeaderRowSpan(0, 3, 2);
                spdData.RPT_MerageHeaderRowSpan(0, 4, 2);
                spdData.RPT_MerageHeaderRowSpan(0, 5, 2);
                spdData.RPT_MerageHeaderRowSpan(0, 6, 2);
                spdData.RPT_MerageHeaderRowSpan(0, 7, 2);
                spdData.RPT_MerageHeaderRowSpan(0, 8, 2);
                spdData.RPT_MerageHeaderRowSpan(0, 9, 2);
                spdData.RPT_MerageHeaderRowSpan(0, 10, 2);
                spdData.RPT_MerageHeaderRowSpan(0, 11, 2);
                spdData.RPT_MerageHeaderRowSpan(0, 12, 2);
                spdData.RPT_MerageHeaderRowSpan(0, 13, 2);
                spdData.RPT_MerageHeaderRowSpan(0, 14, 2);
                spdData.RPT_MerageHeaderRowSpan(0, 15, 2);

                spdData.RPT_ColumnConfigFromTable_New(btnSort); //Group항목이 있을경우 반드시 선언해줄것.
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
            ((udcTableForm)(this.btnSort.BindingForm)).Clear();

            if (cdvFactory.txtValue.Equals("HMKB1"))
            {
                ((udcTableForm)(this.btnSort.BindingForm)).AddRow("Customer", "MAT.MAT_GRP_1", "MAT.MAT_GRP_1", true);
                ((udcTableForm)(this.btnSort.BindingForm)).AddRow("Bumping Type", "MAT.MAT_GRP_2", "MAT.MAT_GRP_2", true);
                ((udcTableForm)(this.btnSort.BindingForm)).AddRow("Operation Flow", "MAT.MAT_GRP_3", "MAT.MAT_GRP_3", false);
                ((udcTableForm)(this.btnSort.BindingForm)).AddRow("Layer classification", "MAT.MAT_GRP_4", "MAT.MAT_GRP_4", true);
                ((udcTableForm)(this.btnSort.BindingForm)).AddRow("PKG Type", "MAT.MAT_GRP_5", "MAT.MAT_GRP_5", false);
                ((udcTableForm)(this.btnSort.BindingForm)).AddRow("RDL Plating", "MAT.MAT_GRP_6", "MAT.MAT_GRP_6", false);
                ((udcTableForm)(this.btnSort.BindingForm)).AddRow("Final Bump", "MAT.MAT_GRP_7", "MAT.MAT_GRP_7", false);
                ((udcTableForm)(this.btnSort.BindingForm)).AddRow("Sub. Material", "MAT.MAT_GRP_8", "MAT.MAT_GRP_8", false);
                ((udcTableForm)(this.btnSort.BindingForm)).AddRow("Size", "MAT.MAT_CMF_14", "MAT.MAT_CMF_14", false);
                ((udcTableForm)(this.btnSort.BindingForm)).AddRow("Thickness", "MAT.MAT_CMF_2", "MAT.MAT_CMF_2", false);
                ((udcTableForm)(this.btnSort.BindingForm)).AddRow("Flat Type", "MAT.MAT_CMF_3", "MAT.MAT_CMF_3", false);
                ((udcTableForm)(this.btnSort.BindingForm)).AddRow("Wafer Orientation", "MAT.MAT_CMF_4", "MAT.MAT_CMF_4", false);

            }
            else
            {
                ((udcTableForm)(this.btnSort.BindingForm)).AddRow("Customer", "MAT.MAT_GRP_1", "MAT.MAT_GRP_1", false);
                ((udcTableForm)(this.btnSort.BindingForm)).AddRow("Family", "MAT.MAT_GRP_2", "MAT.MAT_GRP_2", false);
                ((udcTableForm)(this.btnSort.BindingForm)).AddRow("Package", "MAT.MAT_GRP_3", "MAT.MAT_GRP_3", false);
                ((udcTableForm)(this.btnSort.BindingForm)).AddRow("Type1", "MAT.MAT_GRP_4", "MAT.MAT_GRP_4", false);
                ((udcTableForm)(this.btnSort.BindingForm)).AddRow("Type2", "MAT.MAT_GRP_5", "MAT.MAT_GRP_5", false);
                ((udcTableForm)(this.btnSort.BindingForm)).AddRow("LD Count", "MAT.MAT_GRP_6", "MAT.MAT_GRP_6", false);
                ((udcTableForm)(this.btnSort.BindingForm)).AddRow("Density", "MAT.MAT_GRP_7", "MAT.MAT_GRP_7", false);
                ((udcTableForm)(this.btnSort.BindingForm)).AddRow("Generation", "MAT.MAT_GRP_8", "MAT.MAT_GRP_8", false);
                ((udcTableForm)(this.btnSort.BindingForm)).AddRow("Pin Type", "MAT.MAT_CMF_10", "MAT.MAT_CMF_10", true);
                ((udcTableForm)(this.btnSort.BindingForm)).AddRow("Product", "MAT.MAT_ID", "MAT.MAT_ID", true);
                ((udcTableForm)(this.btnSort.BindingForm)).AddRow("Cust Device", "MAT.MAT_CMF_7", "MAT.MAT_CMF_7", false);
                ((udcTableForm)(this.btnSort.BindingForm)).AddRow("Package2", "MAT.MAT_GRP_10", "MAT.MAT_GRP_10", false);
            }
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

            string sStart_Tran_Time = cdvFromToDate.Start_Tran_Time.ToString();
            string sEnd_Tran_Time = cdvFromToDate.End_Tran_Time.ToString();
            
            udcTableForm tableForm = (udcTableForm)btnSort.BindingForm;

            QueryCond1 = tableForm.SelectedValueToQueryContainNull;            
            QueryCond2 = tableForm.SelectedValue2ToQueryContainNull;


            StringBuilder query1 = new StringBuilder();
            StringBuilder query2 = new StringBuilder();

            // 2011-02-08-임종우 : TAT 단위 시간 단위 추가 표시 (임태성 요청)
            if (ckdTime.Checked == true)
            {
                for (int i = 0; i < dtOper.Rows.Count; i++)
                {

                    query1.Append("     , ROUND(SUM(TAT.WAIT_" + dtOper.Rows[i][0] + ") * 24,2) AS WAIT_" + dtOper.Rows[i][0] + "\n");
                    query1.Append("     , ROUND(SUM(TAT.RUN_" + dtOper.Rows[i][0] + ") * 24,2) AS RUN_" + dtOper.Rows[i][0] + "\n");

                    query2.Append("     , DECODE(LTH.OPER,'" + dtOper.Rows[i][0] + "',LTH.WAIT_TAT_QTY/HIS.SHIP_QTY) AS WAIT_" + dtOper.Rows[i][0] + "\n");
                    query2.Append("     , DECODE(LTH.OPER,'" + dtOper.Rows[i][0] + "',LTH.PROC_TAT_QTY/HIS.SHIP_QTY) AS RUN_" + dtOper.Rows[i][0] + "\n");
                }
            }
            else
            {
                for (int i = 0; i < dtOper.Rows.Count; i++)
                {

                    query1.Append("     , ROUND(SUM(TAT.WAIT_" + dtOper.Rows[i][0] + "),2) AS WAIT_" + dtOper.Rows[i][0] + "\n");
                    query1.Append("     , ROUND(SUM(TAT.RUN_" + dtOper.Rows[i][0] + "),2) AS RUN_" + dtOper.Rows[i][0] + "\n");

                    query2.Append("     , DECODE(LTH.OPER,'" + dtOper.Rows[i][0] + "',LTH.WAIT_TAT_QTY/HIS.SHIP_QTY) AS WAIT_" + dtOper.Rows[i][0] + "\n");
                    query2.Append("     , DECODE(LTH.OPER,'" + dtOper.Rows[i][0] + "',LTH.PROC_TAT_QTY/HIS.SHIP_QTY) AS RUN_" + dtOper.Rows[i][0] + "\n");
                }
            }

            strSqlString.AppendFormat("SELECT {0}" + "\n", QueryCond2);
            strSqlString.Append("     , TAT.LOT_ID,TAT.SHIP_FACTORY,TAT.SHIP_QTY " + "\n");

            // 2011-02-08-임종우 : TAT 단위 시간 단위 추가 표시 (임태성 요청)
            if (ckdTime.Checked == true)
            {
                strSqlString.Append("     , ROUND(SUM(TAT.TAT) * 24,2) AS TAT " + "\n");
            }
            else
            {
                strSqlString.Append("     , ROUND(SUM(TAT.TAT),2) AS TAT " + "\n");
            }

            strSqlString.Append(query1.ToString());
            strSqlString.Append("  FROM (" + "\n");
            strSqlString.Append("        SELECT HIS.MAT_ID" + "\n");
            strSqlString.Append("             , LTH.LOT_ID" + "\n");
            strSqlString.Append("             , '" + cdvFactory.Text + "' AS SHIP_FACTORY" + "\n");
            strSqlString.Append("             , HIS.SHIP_QTY" + "\n");
            strSqlString.Append("             , LTH.OPER" + "\n");
            strSqlString.Append("             , LTH.TOTAL_TAT_QTY/HIS.SHIP_QTY AS TAT" + "\n");
            strSqlString.Append(query2.ToString());
            strSqlString.Append("          FROM CSUMTATLOT@RPTTOMES LTH" + "\n");
            strSqlString.Append("             , (" + "\n");
            strSqlString.Append("                SELECT TAT.FACTORY, TAT.LOT_ID, TAT.MAT_ID, TAT.SHIP_TIME AS WORK_DATE" + "\n");
            strSqlString.Append("                     , SHIP_QTY" + "\n");
            strSqlString.Append("                  FROM CSUMTATLOT@RPTTOMES TAT" + "\n");
            strSqlString.Append("                     , MWIPMATDEF MAT" + "\n");
            strSqlString.Append("                 WHERE 1=1" + "\n");
            strSqlString.Append("                   AND TAT.FACTORY = '" + cdvFactory.Text + "'" + "\n");
            strSqlString.Append("                   AND TAT.FACTORY = MAT.FACTORY" + "\n");
            strSqlString.Append("                   AND TAT.MAT_ID = MAT.MAT_ID" + "\n");
            strSqlString.Append("                   AND TAT.MAT_ID LIKE '" + txtProduct.Text + "'" + "\n");
            strSqlString.Append("                   AND MAT.DELETE_FLAG = ' '" + "\n");

            //strSqlString.Append("                   AND TAT.OPER ='AZ010'" + "\n");
            strSqlString.Append("                   AND TAT.OPER IN ('AZ010', 'BZ010') " + "\n");

            //strSqlString.Append("                   AND TAT.LOT_CMF_5 " + cdvLotType.SelectedValueToQueryString + "\n");
            if (cdvLotType.Text != "ALL")
            {
                strSqlString.Append("                   AND TAT.LOT_CMF_5 LIKE '" + cdvLotType.Text + "'" + "\n");
            }   
            strSqlString.Append("                   AND TAT.SHIP_TIME BETWEEN '" + sStart_Tran_Time + "' AND '" + sEnd_Tran_Time + "'\n");
            strSqlString.Append("               ) HIS" + "\n");
            strSqlString.Append("         WHERE 1=1" + "\n");
            strSqlString.Append("           AND LTH.FACTORY " + cdvFactory.SelectedValueToQueryString + "\n");
            strSqlString.Append("           AND LTH.MAT_ID=HIS.MAT_ID" + "\n");
            strSqlString.Append("           AND LTH.LOT_ID=HIS.LOT_ID" + "\n");
            if (cdvOper.ToText != "" && cdvOper.FromText != "")
            {
                strSqlString.Append("           AND LTH.OPER BETWEEN '" + cdvOper.FromText + "' AND '" + cdvOper.ToText+"'"+"\n");
            }
            //strSqlString.Append("           AND LTH.SHIP_TIME BETWEEN '" + sStart_Tran_Time + "' AND '" + sEnd_Tran_Time + "'\n");
            strSqlString.Append("       ) TAT" + "\n");
            strSqlString.Append("     , MWIPMATDEF MAT" + "\n");
            strSqlString.Append(" WHERE 1=1" + "\n");
            strSqlString.Append("   AND TAT.MAT_ID=MAT.MAT_ID" + "\n");
            strSqlString.Append("   AND MAT.FACTORY " + cdvFactory.SelectedValueToQueryString + "\n");

            #region 상세 조회에 따른 SQL문 생성
            if (cdvFactory.Text.Trim() == "HMKB1")
            {
                if (udcBUMPCondition1.Text != "ALL" && udcBUMPCondition1.Text != "")
                    strSqlString.AppendFormat("   AND MAT.MAT_GRP_1 {0} " + "\n", udcBUMPCondition1.SelectedValueToQueryString);

                if (udcBUMPCondition2.Text != "ALL" && udcBUMPCondition2.Text != "")
                    strSqlString.AppendFormat("   AND MAT.MAT_GRP_2 {0} " + "\n", udcBUMPCondition2.SelectedValueToQueryString);

                if (udcBUMPCondition3.Text != "ALL" && udcBUMPCondition3.Text != "")
                    strSqlString.AppendFormat("   AND MAT.MAT_GRP_3 {0} " + "\n", udcBUMPCondition3.SelectedValueToQueryString);

                if (udcBUMPCondition4.Text != "ALL" && udcBUMPCondition4.Text != "")
                    strSqlString.AppendFormat("   AND MAT.MAT_GRP_4 {0} " + "\n", udcBUMPCondition4.SelectedValueToQueryString);

                if (udcBUMPCondition5.Text != "ALL" && udcBUMPCondition5.Text != "")
                    strSqlString.AppendFormat("   AND MAT.MAT_GRP_5 {0} " + "\n", udcBUMPCondition5.SelectedValueToQueryString);

                if (udcBUMPCondition6.Text != "ALL" && udcBUMPCondition6.Text != "")
                    strSqlString.AppendFormat("   AND MAT.MAT_GRP_6 {0} " + "\n", udcBUMPCondition6.SelectedValueToQueryString);

                if (udcBUMPCondition7.Text != "ALL" && udcBUMPCondition7.Text != "")
                    strSqlString.AppendFormat("   AND MAT.MAT_GRP_7 {0} " + "\n", udcBUMPCondition7.SelectedValueToQueryString);

                if (udcBUMPCondition8.Text != "ALL" && udcBUMPCondition8.Text != "")
                    strSqlString.AppendFormat("   AND MAT.MAT_GRP_8 {0} " + "\n", udcBUMPCondition8.SelectedValueToQueryString);

                if (udcBUMPCondition9.Text != "ALL" && udcBUMPCondition9.Text != "")
                    strSqlString.AppendFormat("   AND MAT.MAT_CMF_14 {0} " + "\n", udcBUMPCondition9.SelectedValueToQueryString);

                if (udcBUMPCondition10.Text != "ALL" && udcBUMPCondition10.Text != "")
                    strSqlString.AppendFormat("   AND MAT.MAT_CMF_2 {0} " + "\n", udcBUMPCondition10.SelectedValueToQueryString);

                if (udcBUMPCondition11.Text != "ALL" && udcBUMPCondition11.Text != "")
                    strSqlString.AppendFormat("   AND MAT.MAT_CMF_3 {0} " + "\n", udcBUMPCondition11.SelectedValueToQueryString);

                if (udcBUMPCondition12.Text != "ALL" && udcBUMPCondition12.Text != "")
                    strSqlString.AppendFormat("   AND MAT.MAT_CMF_4 {0} " + "\n", udcBUMPCondition12.SelectedValueToQueryString);
            }
            else
            {
                //상세 조회에 따른 SQL문 생성                        
                if (udcWIPCondition1.Text != "ALL" && udcWIPCondition1.Text != "")
                    strSqlString.AppendFormat("   AND MAT.MAT_GRP_1 {0}" + "\n", udcWIPCondition1.SelectedValueToQueryString);

                if (udcWIPCondition2.Text != "ALL" && udcWIPCondition2.Text != "")
                    strSqlString.AppendFormat("   AND MAT.MAT_GRP_2 {0}" + "\n", udcWIPCondition2.SelectedValueToQueryString);

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
            }
            #endregion

            strSqlString.Append(" GROUP BY " + QueryCond1 + ",TAT.LOT_ID,TAT.SHIP_FACTORY,TAT.SHIP_QTY" + "\n");
            strSqlString.Append(" ORDER BY " + QueryCond1 + ",TAT.LOT_ID,TAT.SHIP_FACTORY,TAT.SHIP_QTY" + "\n");

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
            
            // 공정 코드 가져오기
            dtOper = CmnFunction.oComm.GetFuncDataTable("DYNAMIC", MakeOperTable());            

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

                //spdData.DataSource = dt;

                //그루핑 순서 1순위만 SubTotal을 사용하기 위해서 첫번째 값을 가져옴
                int sub = ((udcTableForm)(this.btnSort.BindingForm)).GetCheckedValue(2);
                int nGroupCount = ((udcTableForm)(this.btnSort.BindingForm)).GetSelectedCount();

                // 0값 표시 설정
                spdData.isShowZero = true;

                //1.Griid 합계 표시
                int[] rowType = spdData.RPT_DataBindingWithSubTotal(dt, 0, sub+1, 14, null, null, btnSort);

                //int[] rowType = spdData.RPT_DataBindingWithSubTotal(dt, 0, sub + 1, 13, null, null, btnSort, true);
                //3. Total부분 셀머지
                spdData.RPT_FillDataSelectiveCells("Total", 0, 14, 0, 1, true, Align.Center, VerticalAlign.Center);
                                
                //4. Column Auto Fit
                spdData.RPT_AutoFit(false);

                //spdData.ActiveSheet.Columns[9].AllowAutoSort = true;
                //spdData.ActiveSheet.Columns[11].AllowAutoSort = true;
                //spdData.ActiveSheet.Columns[12].AllowAutoSort = true;
                //spdData.ActiveSheet.Columns[13].AllowAutoSort = true;

                //5. 데이타가 0인 부분은 제거(Add by John. 2009.1.13)
                //spdData.RPT_RemoveZeroColumn(15, 1);                

                // Sub Total 평균값으로 구하기(TAT)
                spdData.RPT_SetAvgSubTotalAndGrandTotal(1, 15, nGroupCount, true);                

                int countOper = dtOper.Rows.Count * 2;

                for (int i = 0; i < countOper ; i++)
                {
                    spdData.RPT_SetAvgSubTotalAndGrandTotal(1, 16 + i, nGroupCount, true);                    
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
            cdvOper.sFactory = cdvFactory.txtValue;
            //cdvLotType.sFactory = cdvFactory.txtValue;

            SortInit();
         }
         #endregion
     }
}
