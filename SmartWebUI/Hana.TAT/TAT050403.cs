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
    public partial class TAT050403 : Miracom.SmartWeb.UI.Controls.udcCUSReport002
    {
        private DataTable dtOper;

        /// <summary>
        /// 클  래  스: TAT050403<br/>
        /// 클래스요약: 공정 TAT 관리<br/>
        /// 작  성  자: 하나마이크론 임종우<br/>
        /// 최초작성일: 2014-08-13<br/>
        /// 상세  설명: 공정 TAT 관리<br/>
        /// 변경  내용: <br/>
        /// 2014-08-20-임종우 : 누계 TAT 도 음영표시 추가 (김권수D 요청)
        /// 2014-08-22-임종우 : kpcs 검색 기능 추가, TAT 소숫점 1자리까지 (김권수D 요청)
        /// </summary>
        /// 
        public TAT050403()
        {
            InitializeComponent();            
            SortInit();
            cdvDate.Value = DateTime.Now;
            GridColumnInit();
            this.SetFactory(GlobalVariable.gsAssyDefaultFactory);
            cdvFactory.Text = GlobalVariable.gsAssyDefaultFactory;
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

            //strSqlString.Append("SELECT OPER FROM MWIPOPRDEF WHERE FACTORY " + cdvFactory.SelectedValueToQueryString + " AND OPER <> 'A000N' AND OPER LIKE 'A%' ORDER BY OPER" + "\n");
            strSqlString.Append("SELECT DISTINCT B.OPER, OPER_CMF_2" + "\n");
            strSqlString.Append("  FROM MWIPFLWOPR@RPTTOMES A" + "\n");
            strSqlString.Append("     , MWIPOPRDEF B" + "\n");
            strSqlString.Append(" WHERE 1=1 " + "\n");
            strSqlString.Append("   AND A.FACTORY = B.FACTORY " + "\n");
            strSqlString.Append("   AND A.OPER = B.OPER " + "\n");
            if (cdvFactory.Text.Trim() == "HMKB1")
            {
                strSqlString.Append("   AND B.INV_FLAG = ' ' " + "\n");
            }
            strSqlString.Append("   AND A.FACTORY = '" + cdvFactory.Text + "'" + "\n");
            strSqlString.Append("   AND A.FLOW IN (" + "\n");
            strSqlString.Append("                SELECT DISTINCT FLOW" + "\n");
            strSqlString.Append("                  FROM RWIPLOTSTS A" + "\n");
            strSqlString.Append("                     , MWIPMATDEF B" + "\n");
            strSqlString.Append("                 WHERE 1=1" + "\n");
            strSqlString.Append("                   AND A.FACTORY = B.FACTORY" + "\n");
            strSqlString.Append("                   AND A.MAT_ID = B.MAT_ID" + "\n");
            strSqlString.Append("                   AND A.FACTORY = '" + cdvFactory.Text + "'" + "\n");
            strSqlString.Append("                   AND A.LOT_DEL_FLAG = ' '" + "\n");
            strSqlString.Append("                   AND A.LOT_TYPE = 'W'" + "\n");
            strSqlString.Append("                   AND A.QTY_1 > 0" + "\n");

            if (cdvLotType.Text != "ALL")
            {
                strSqlString.Append("                   AND A.LOT_CMF_5 LIKE '" + cdvLotType.Text + "'" + "\n");
            }

            if (txtProduct.Text.Trim() != "%" && txtProduct.Text.Trim() != "")
            {
                strSqlString.AppendFormat("                   AND A.MAT_ID LIKE '{0}'" + "\n", txtProduct.Text);
            }

            #region 상세 조회에 따른 SQL문 생성생성
            if (cdvFactory.Text.Trim() == "HMKB1")
            {
                if (udcBUMPCondition1.Text != "ALL" && udcBUMPCondition1.Text != "")
                    strSqlString.AppendFormat("                   AND MAT.MAT_GRP_1 {0} " + "\n", udcBUMPCondition1.SelectedValueToQueryString);

                if (udcBUMPCondition2.Text != "ALL" && udcBUMPCondition2.Text != "")
                    strSqlString.AppendFormat("                   AND MAT.MAT_GRP_2 {0} " + "\n", udcBUMPCondition2.SelectedValueToQueryString);

                if (udcBUMPCondition3.Text != "ALL" && udcBUMPCondition3.Text != "")
                    strSqlString.AppendFormat("                   AND MAT.MAT_GRP_3 {0} " + "\n", udcBUMPCondition3.SelectedValueToQueryString);

                if (udcBUMPCondition4.Text != "ALL" && udcBUMPCondition4.Text != "")
                    strSqlString.AppendFormat("                   AND MAT.MAT_GRP_4 {0} " + "\n", udcBUMPCondition4.SelectedValueToQueryString);

                if (udcBUMPCondition5.Text != "ALL" && udcBUMPCondition5.Text != "")
                    strSqlString.AppendFormat("                   AND MAT.MAT_GRP_5 {0} " + "\n", udcBUMPCondition5.SelectedValueToQueryString);

                if (udcBUMPCondition6.Text != "ALL" && udcBUMPCondition6.Text != "")
                    strSqlString.AppendFormat("                   AND MAT.MAT_GRP_6 {0} " + "\n", udcBUMPCondition6.SelectedValueToQueryString);

                if (udcBUMPCondition7.Text != "ALL" && udcBUMPCondition7.Text != "")
                    strSqlString.AppendFormat("                   AND MAT.MAT_GRP_7 {0} " + "\n", udcBUMPCondition7.SelectedValueToQueryString);

                if (udcBUMPCondition8.Text != "ALL" && udcBUMPCondition8.Text != "")
                    strSqlString.AppendFormat("                   AND MAT.MAT_GRP_8 {0} " + "\n", udcBUMPCondition8.SelectedValueToQueryString);

                if (udcBUMPCondition9.Text != "ALL" && udcBUMPCondition9.Text != "")
                    strSqlString.AppendFormat("                   AND MAT.MAT_CMF_14 {0} " + "\n", udcBUMPCondition9.SelectedValueToQueryString);

                if (udcBUMPCondition10.Text != "ALL" && udcBUMPCondition10.Text != "")
                    strSqlString.AppendFormat("                   AND MAT.MAT_CMF_2 {0} " + "\n", udcBUMPCondition10.SelectedValueToQueryString);

                if (udcBUMPCondition11.Text != "ALL" && udcBUMPCondition11.Text != "")
                    strSqlString.AppendFormat("                   AND MAT.MAT_CMF_3 {0} " + "\n", udcBUMPCondition11.SelectedValueToQueryString);

                if (udcBUMPCondition12.Text != "ALL" && udcBUMPCondition12.Text != "")
                    strSqlString.AppendFormat("                   AND MAT.MAT_CMF_4 {0} " + "\n", udcBUMPCondition12.SelectedValueToQueryString);
            }
            else
            {
                //상세 조회에 따른 SQL문 생성                        
                if (udcWIPCondition1.Text != "ALL" && udcWIPCondition1.Text != "")
                    strSqlString.AppendFormat("                   AND B.MAT_GRP_1 {0}" + "\n", udcWIPCondition1.SelectedValueToQueryString);

                if (udcWIPCondition2.Text != "ALL" && udcWIPCondition2.Text != "")
                    strSqlString.AppendFormat("                   AND B.MAT_GRP_2 {0}" + "\n", udcWIPCondition2.SelectedValueToQueryString);

                if (udcWIPCondition3.Text != "ALL" && udcWIPCondition3.Text != "")
                    strSqlString.AppendFormat("                   AND B.MAT_GRP_3 {0} " + "\n", udcWIPCondition3.SelectedValueToQueryString);

                if (udcWIPCondition4.Text != "ALL" && udcWIPCondition4.Text != "")
                    strSqlString.AppendFormat("                   AND B.MAT_GRP_4 {0} " + "\n", udcWIPCondition4.SelectedValueToQueryString);

                if (udcWIPCondition5.Text != "ALL" && udcWIPCondition5.Text != "")
                    strSqlString.AppendFormat("                   AND B.MAT_GRP_5 {0} " + "\n", udcWIPCondition5.SelectedValueToQueryString);

                if (udcWIPCondition6.Text != "ALL" && udcWIPCondition6.Text != "")
                    strSqlString.AppendFormat("                   AND B.MAT_GRP_6 {0} " + "\n", udcWIPCondition6.SelectedValueToQueryString);

                if (udcWIPCondition7.Text != "ALL" && udcWIPCondition7.Text != "")
                    strSqlString.AppendFormat("                   AND B.MAT_GRP_7 {0} " + "\n", udcWIPCondition7.SelectedValueToQueryString);

                if (udcWIPCondition8.Text != "ALL" && udcWIPCondition8.Text != "")
                    strSqlString.AppendFormat("                   AND B.MAT_GRP_8 {0} " + "\n", udcWIPCondition8.SelectedValueToQueryString);

                if (udcWIPCondition9.Text != "ALL" && udcWIPCondition9.Text != "")
                    strSqlString.AppendFormat("                   AND B.MAT_GRP_9 {0} " + "\n", udcWIPCondition9.SelectedValueToQueryString);
            }
            #endregion

            strSqlString.Append("               )" + "\n");
            strSqlString.Append(" ORDER BY TO_NUMBER(OPER_CMF_2), OPER" + "\n");

            return strSqlString.ToString();
        }

        /// <summary>
        /// 2. 헤더 생성
        /// </summary>
        private void GridColumnInit()
        {
            int headerCount = 0;

            try
            {
                spdData.ActiveSheet.ColumnHeader.Rows.Count = 1;
                spdData.RPT_ColumnInit();

                if (cdvFactory.txtValue.Equals("HMKB1"))
                {
                    spdData.RPT_AddBasicColumn("Customer", 0, 0, Visibles.True, Frozen.True, Align.Center, Merge.True, Formatter.String, 80);
                    spdData.RPT_AddBasicColumn("Bumping Type", 0, 1, Visibles.True, Frozen.True, Align.Center, Merge.True, Formatter.String, 70);
                    spdData.RPT_AddBasicColumn("Process Flow", 0, 2, Visibles.False, Frozen.True, Align.Left, Merge.True, Formatter.String, 70);
                    spdData.RPT_AddBasicColumn("Layer classification", 0, 3, Visibles.True, Frozen.True, Align.Center, Merge.True, Formatter.String, 70);
                    spdData.RPT_AddBasicColumn("PKG Type", 0, 4, Visibles.True, Frozen.True, Align.Center, Merge.True, Formatter.String, 50);
                    spdData.RPT_AddBasicColumn("RDL Plating", 0, 5, Visibles.False, Frozen.True, Align.Center, Merge.True, Formatter.String, 50);
                    spdData.RPT_AddBasicColumn("Final Bump", 0, 6, Visibles.False, Frozen.True, Align.Center, Merge.True, Formatter.String, 50);
                    spdData.RPT_AddBasicColumn("Sub. Material", 0, 7, Visibles.False, Frozen.True, Align.Center, Merge.True, Formatter.String, 50);
                    spdData.RPT_AddBasicColumn("Size", 0, 8, Visibles.False, Frozen.True, Align.Center, Merge.True, Formatter.String, 50);
                    spdData.RPT_AddBasicColumn("Thickness", 0, 9, Visibles.False, Frozen.True, Align.Center, Merge.True, Formatter.String, 50);
                    spdData.RPT_AddBasicColumn("Flat Type", 0, 10, Visibles.False, Frozen.True, Align.Center, Merge.True, Formatter.String, 50);
                    spdData.RPT_AddBasicColumn("Wafer Orientation", 0, 11, Visibles.False, Frozen.True, Align.Center, Merge.True, Formatter.String, 50);

                    spdData.RPT_AddBasicColumn("PRODUCT", 0, 12, Visibles.True, Frozen.True, Align.Left, Merge.True, Formatter.String, 160);

                    spdData.RPT_AddBasicColumn("구분", 0, 13, Visibles.True, Frozen.True, Align.Center, Merge.True, Formatter.String, 80);

                    // header 의 Colnum Count
                    headerCount = 14;

                }
                else
                {
                    spdData.RPT_AddBasicColumn("CUSTOMER", 0, 0, Visibles.True, Frozen.True, Align.Center, Merge.True, Formatter.String, 70);
                    spdData.RPT_AddBasicColumn("MAJOR", 0, 1, Visibles.True, Frozen.True, Align.Center, Merge.True, Formatter.String, 70);
                    spdData.RPT_AddBasicColumn("FAMILY", 0, 2, Visibles.False, Frozen.True, Align.Center, Merge.True, Formatter.String, 70);
                    spdData.RPT_AddBasicColumn("PKG", 0, 3, Visibles.True, Frozen.True, Align.Center, Merge.True, Formatter.String, 70);
                    spdData.RPT_AddBasicColumn("TYPE1", 0, 4, Visibles.False, Frozen.True, Align.Center, Merge.True, Formatter.String, 70);
                    spdData.RPT_AddBasicColumn("TYPE2", 0, 5, Visibles.False, Frozen.True, Align.Center, Merge.True, Formatter.String, 70);
                    spdData.RPT_AddBasicColumn("LD COUNT", 0, 6, Visibles.True, Frozen.True, Align.Center, Merge.True, Formatter.String, 70);
                    spdData.RPT_AddBasicColumn("DENSITY", 0, 7, Visibles.False, Frozen.True, Align.Center, Merge.True, Formatter.String, 70);
                    spdData.RPT_AddBasicColumn("GENERATION", 0, 8, Visibles.False, Frozen.True, Align.Center, Merge.True, Formatter.String, 70);
                    spdData.RPT_AddBasicColumn("PIN TYPE", 0, 9, Visibles.True, Frozen.True, Align.Left, Merge.True, Formatter.String, 140);
                    spdData.RPT_AddBasicColumn("PKG CODE", 0, 10, Visibles.True, Frozen.True, Align.Center, Merge.True, Formatter.String, 70);

                    spdData.RPT_AddBasicColumn("PRODUCT", 0, 11, Visibles.True, Frozen.True, Align.Left, Merge.True, Formatter.String, 160);

                    spdData.RPT_AddBasicColumn("구분", 0, 12, Visibles.True, Frozen.True, Align.Center, Merge.True, Formatter.String, 80);

                    // header 의 Colnum Count
                    headerCount = 13;

                }

                // Header 에 공정 추가하기
                if (dtOper != null)
                {
                    for (int i = 0; i < dtOper.Rows.Count; i++)
                    {
                        spdData.RPT_AddBasicColumn(dtOper.Rows[i][0].ToString(), 0, headerCount, Visibles.True, Frozen.False, Align.Right, Merge.True, Formatter.String, 60);                        
                        headerCount++;
                    }
                }                

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
                ((udcTableForm)(this.btnSort.BindingForm)).AddRow("CUSTOMER", "MAT_GRP_1", "MAT.MAT_GRP_1", true);
                ((udcTableForm)(this.btnSort.BindingForm)).AddRow("Bumping Type", "MAT_GRP_2", "MAT.MAT_GRP_2", true);
                ((udcTableForm)(this.btnSort.BindingForm)).AddRow("Process Flow", "MAT_GRP_3", "MAT.MAT_GRP_3", false);
                ((udcTableForm)(this.btnSort.BindingForm)).AddRow("Layer classification", "MAT_GRP_4", "MAT.MAT_GRP_4", true);
                ((udcTableForm)(this.btnSort.BindingForm)).AddRow("PKG Type", "MAT_GRP_5", "MAT.MAT_GRP_5", true);
                ((udcTableForm)(this.btnSort.BindingForm)).AddRow("RDL Plating", "MAT_GRP_6", "MAT.MAT_GRP_6", false);
                ((udcTableForm)(this.btnSort.BindingForm)).AddRow("Final Bump", "MAT_GRP_7", "MAT.MAT_GRP_7", false);
                ((udcTableForm)(this.btnSort.BindingForm)).AddRow("Sub. Material", "MAT_GRP_8", "MAT.MAT_GRP_8", false);
                ((udcTableForm)(this.btnSort.BindingForm)).AddRow("Size", "MAT_CMF_14", "MAT.MAT_CMF_14", false);
                ((udcTableForm)(this.btnSort.BindingForm)).AddRow("Thickness", "MAT_CMF_2", "MAT.MAT_CMF_2", false);
                ((udcTableForm)(this.btnSort.BindingForm)).AddRow("Flat Type", "MAT_CMF_3", "MAT.MAT_CMF_3", false);
                ((udcTableForm)(this.btnSort.BindingForm)).AddRow("Wafer Orientation", "MAT_CMF_4", "MAT.MAT_CMF_4", false);
                ((udcTableForm)(this.btnSort.BindingForm)).AddRow("PRODUCT", "MAT_ID", "MAT.MAT_ID", true);
            }
            else
            {
                ((udcTableForm)(this.btnSort.BindingForm)).AddRow("CUSTOMER", "MAT_GRP_1", "MAT.MAT_GRP_1", true);
                ((udcTableForm)(this.btnSort.BindingForm)).AddRow("MAJOR", "MAT_GRP_9", "MAT.MAT_GRP_10", false);
                ((udcTableForm)(this.btnSort.BindingForm)).AddRow("FAMILY", "MAT_GRP_2", "MAT.MAT_GRP_2", false);
                ((udcTableForm)(this.btnSort.BindingForm)).AddRow("PKG", "MAT_GRP_10", "MAT.MAT_GRP_3", true);
                ((udcTableForm)(this.btnSort.BindingForm)).AddRow("TYPE1", "MAT_GRP_4", "MAT.MAT_GRP_4", false);
                ((udcTableForm)(this.btnSort.BindingForm)).AddRow("TYPE2", "MAT_GRP_5", "MAT.MAT_GRP_5", false);
                ((udcTableForm)(this.btnSort.BindingForm)).AddRow("LD COUNT", "MAT_GRP_6", "MAT.MAT_GRP_6", true);
                ((udcTableForm)(this.btnSort.BindingForm)).AddRow("DENSITY", "MAT_GRP_7", "MAT.MAT_GRP_7", false);
                ((udcTableForm)(this.btnSort.BindingForm)).AddRow("GENERATION", "MAT_GRP_8", "MAT.MAT_GRP_8", false);
                ((udcTableForm)(this.btnSort.BindingForm)).AddRow("PIN TYPE", "MAT_CMF_10", "MAT.MAT_CMF_10", false);
                ((udcTableForm)(this.btnSort.BindingForm)).AddRow("PKG CODE", "MAT_CMF_11", "MAT.MAT_CMF_7", true);
                ((udcTableForm)(this.btnSort.BindingForm)).AddRow("PRODUCT", "MAT_ID", "MAT.MAT_ID", true);
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
            StringBuilder query = new StringBuilder();

            string date = null;
            string QueryCond1 = null;
            string QueryCond2 = null;
            string sKpcsValue = null;
            
            udcTableForm tableForm = (udcTableForm)btnSort.BindingForm;

            QueryCond1 = tableForm.SelectedValueToQueryContainNull;            
            QueryCond2 = tableForm.SelectedValue2ToQueryContainNull;

            date = cdvDate.SelectedValue();

            if (ckbKpcs.Checked == true)
            {
                sKpcsValue = "1000";
            }
            else
            {
                sKpcsValue = "1";
            }

            strSqlString.Append("WITH DT AS" + "\n");
            strSqlString.Append("(" + "\n");
            strSqlString.Append("SELECT MAT_ID, OPER" + "\n");
            strSqlString.Append("     , SUM(QTY_1) AS WIP_QTY" + "\n");
            strSqlString.Append("     , SUM(TAT_QTY) AS TAT_QTY" + "\n");
            strSqlString.Append("     , SUM(SUM_TAT_QTY) AS SUM_TAT_QTY" + "\n");
            strSqlString.Append("  FROM (" + "\n");
            strSqlString.Append("        SELECT MAT_ID, OPER, LOT_ID, QTY_1" + "\n");            

            if (date == DateTime.Now.ToString("yyyyMMdd"))
            {
                strSqlString.Append("             , (SYSDATE - TO_DATE(OPER_IN_TIME, 'YYYYMMDDHH24MISS')) * QTY_1 AS TAT_QTY" + "\n");
                strSqlString.Append("             , DECODE(RESV_FIELD_1, ' ', 0, (SYSDATE - TO_DATE(RESV_FIELD_1, 'YYYYMMDDHH24MISS')) * QTY_1) AS SUM_TAT_QTY" + "\n");        //-- ?????
                strSqlString.Append("          FROM RWIPLOTSTS " + "\n");
                strSqlString.Append("         WHERE 1=1  " + "\n");
            }
            else
            {
                strSqlString.Append("             , (TO_DATE(" + date + "220000, 'YYYYMMDDHH24MISS') - TO_DATE(OPER_IN_TIME, 'YYYYMMDDHH24MISS')) * QTY_1 AS TAT_QTY" + "\n");
                strSqlString.Append("             , DECODE(RESV_FIELD_1, ' ', 0, (TO_DATE(" + date + "220000, 'YYYYMMDDHH24MISS') - TO_DATE(RESV_FIELD_1, 'YYYYMMDDHH24MISS')) * QTY_1) AS SUM_TAT_QTY" + "\n");        //-- ?????
                strSqlString.Append("          FROM RWIPLOTSTS_BOH " + "\n");
                strSqlString.Append("         WHERE 1=1  " + "\n");
                strSqlString.Append("           AND CUTOFF_DT = '" + date + "22' " + "\n");
            }

            strSqlString.Append("           AND FACTORY = '" + cdvFactory.txtValue + "'" + "\n");
            strSqlString.Append("           AND LOT_DEL_FLAG = ' '" + "\n");
            strSqlString.Append("           AND LOT_TYPE = 'W'" + "\n");
            strSqlString.Append("           AND QTY_1 > 0" + "\n");

            if (cdvLotType.Text != "ALL")
            {
                strSqlString.Append("           AND LOT_CMF_5 LIKE '" + cdvLotType.Text + "'" + "\n");
            }
                        
            strSqlString.Append("       )" + "\n");
            strSqlString.Append(" GROUP BY MAT_ID, OPER" + "\n");
            strSqlString.Append(")" + "\n");

            strSqlString.AppendFormat("SELECT {0}, GUBUN" + "\n", QueryCond1);                                    

            for (int i = 0; i < dtOper.Rows.Count; i++)
            {

                query.Append("     , ROUND(SUM(DECODE(OPER, '" + dtOper.Rows[i][0] + "', DATA)), 1) AS " + dtOper.Rows[i][0] + "\n");                    
            }

            strSqlString.Append(query.ToString());
            strSqlString.Append("  FROM (" + "\n");
            strSqlString.AppendFormat("        SELECT {0}, GUBUN, OPER" + "\n", QueryCond1);            
            strSqlString.Append("             , NVL(CASE GUBUN WHEN 'WIP' THEN WIP_QTY" + "\n");
            strSqlString.Append("                          WHEN '공정 Target' THEN TARGET_TAT" + "\n");
            strSqlString.Append("                          WHEN '공정 TAT' THEN TAT" + "\n");
            strSqlString.Append("                          WHEN '누계 Target' THEN SUM_TARGET_TAT" + "\n");
            strSqlString.Append("                          WHEN '누계 TAT' THEN SUM_TAT" + "\n");
            strSqlString.Append("                          ELSE 0" + "\n");
            strSqlString.Append("               END, 0) DATA     " + "\n");
            strSqlString.Append("          FROM (" + "\n");
            strSqlString.AppendFormat("                SELECT {0}, OPER" + "\n", QueryCond1);
            strSqlString.Append("                     , ROUND(SUM(WIP_QTY) / " + sKpcsValue + ", 0) AS WIP_QTY" + "\n");

            if (ckdTime.Checked == true)
            {
                strSqlString.Append("                     , AVG(TARGET_TAT) * 24 AS TARGET_TAT" + "\n");
                strSqlString.Append("                     , AVG(SUM_TARGET_TAT) * 24 AS SUM_TARGET_TAT" + "\n");                
                strSqlString.Append("                     , SUM(TAT_QTY) / SUM(WIP_QTY) * 24 AS TAT" + "\n");
                strSqlString.Append("                     , SUM(SUM_TAT_QTY) / SUM(WIP_QTY) * 24 AS SUM_TAT" + "\n");
            }
            else
            {
                strSqlString.Append("                     , AVG(TARGET_TAT) AS TARGET_TAT" + "\n");
                strSqlString.Append("                     , AVG(SUM_TARGET_TAT) AS SUM_TARGET_TAT" + "\n");                
                strSqlString.Append("                     , SUM(TAT_QTY) / SUM(WIP_QTY) AS TAT" + "\n");
                strSqlString.Append("                     , SUM(SUM_TAT_QTY) / SUM(WIP_QTY) AS SUM_TAT" + "\n");
            }

            strSqlString.Append("                  FROM (" + "\n");
            strSqlString.Append("                        SELECT A.*" + "\n");
            strSqlString.Append("                             , NVL(B.TAT_DAY + B.TAT_DAY_WAIT, 0) AS TARGET_TAT" + "\n");
            strSqlString.Append("                             , SUM(NVL(B.TAT_DAY + B.TAT_DAY_WAIT, 0)) OVER(PARTITION BY A.MAT_ID ORDER BY A.SEQ_NUM) AS SUM_TARGET_TAT" + "\n");
            strSqlString.Append("                             , C.WIP_QTY" + "\n");
            strSqlString.Append("                             , C.TAT_QTY" + "\n");
            strSqlString.Append("                             , C.SUM_TAT_QTY     " + "\n");
            strSqlString.Append("                          FROM (" + "\n");
            strSqlString.Append("                                SELECT A.*, B.OPER, B.SEQ_NUM" + "\n");
            strSqlString.Append("                                  FROM MWIPMATDEF A          " + "\n");
            strSqlString.Append("                                     , MWIPFLWOPR@RPTTOMES B" + "\n");
            strSqlString.Append("                                 WHERE 1=1" + "\n");
            strSqlString.Append("                                   AND A.FACTORY = B.FACTORY      " + "\n");
            strSqlString.Append("                                   AND A.FIRST_FLOW = B.FLOW   " + "\n");
            strSqlString.Append("                                   AND A.FACTORY = '" + cdvFactory.txtValue + "'" + "\n");
            strSqlString.Append("                                   AND A.DELETE_FLAG = ' '" + "\n");
            strSqlString.Append("                                   AND A.MAT_TYPE = 'FG'" + "\n");
            strSqlString.Append("                                   AND A.MAT_ID IN (SELECT DISTINCT MAT_ID FROM DT)" + "\n");

            if (txtProduct.Text.Trim() != "%" && txtProduct.Text.Trim() != "")
            {
                strSqlString.AppendFormat("                                   AND A.MAT_ID LIKE '{0}'" + "\n", txtProduct.Text);
            }            
            
            #region 상세 조회에 따른 SQL문 생성생성
            if (cdvFactory.Text.Trim() == "HMKB1")
            {
                if (udcBUMPCondition1.Text != "ALL" && udcBUMPCondition1.Text != "")
                    strSqlString.AppendFormat("                                   AND A.MAT_GRP_1 {0} " + "\n", udcBUMPCondition1.SelectedValueToQueryString);

                if (udcBUMPCondition2.Text != "ALL" && udcBUMPCondition2.Text != "")
                    strSqlString.AppendFormat("                                   AND A.MAT_GRP_2 {0} " + "\n", udcBUMPCondition2.SelectedValueToQueryString);

                if (udcBUMPCondition3.Text != "ALL" && udcBUMPCondition3.Text != "")
                    strSqlString.AppendFormat("                                   AND A.MAT_GRP_3 {0} " + "\n", udcBUMPCondition3.SelectedValueToQueryString);

                if (udcBUMPCondition4.Text != "ALL" && udcBUMPCondition4.Text != "")
                    strSqlString.AppendFormat("                                   AND A.MAT_GRP_4 {0} " + "\n", udcBUMPCondition4.SelectedValueToQueryString);

                if (udcBUMPCondition5.Text != "ALL" && udcBUMPCondition5.Text != "")
                    strSqlString.AppendFormat("                                   AND A.MAT_GRP_5 {0} " + "\n", udcBUMPCondition5.SelectedValueToQueryString);

                if (udcBUMPCondition6.Text != "ALL" && udcBUMPCondition6.Text != "")
                    strSqlString.AppendFormat("                                   AND A.MAT_GRP_6 {0} " + "\n", udcBUMPCondition6.SelectedValueToQueryString);

                if (udcBUMPCondition7.Text != "ALL" && udcBUMPCondition7.Text != "")
                    strSqlString.AppendFormat("                                   AND A.MAT_GRP_7 {0} " + "\n", udcBUMPCondition7.SelectedValueToQueryString);

                if (udcBUMPCondition8.Text != "ALL" && udcBUMPCondition8.Text != "")
                    strSqlString.AppendFormat("                                   AND A.MAT_GRP_8 {0} " + "\n", udcBUMPCondition8.SelectedValueToQueryString);

                if (udcBUMPCondition9.Text != "ALL" && udcBUMPCondition9.Text != "")
                    strSqlString.AppendFormat("                                   AND A.MAT_CMF_14 {0} " + "\n", udcBUMPCondition9.SelectedValueToQueryString);

                if (udcBUMPCondition10.Text != "ALL" && udcBUMPCondition10.Text != "")
                    strSqlString.AppendFormat("                                   AND A.MAT_CMF_2 {0} " + "\n", udcBUMPCondition10.SelectedValueToQueryString);

                if (udcBUMPCondition11.Text != "ALL" && udcBUMPCondition11.Text != "")
                    strSqlString.AppendFormat("                                   AND A.MAT_CMF_3 {0} " + "\n", udcBUMPCondition11.SelectedValueToQueryString);

                if (udcBUMPCondition12.Text != "ALL" && udcBUMPCondition12.Text != "")
                    strSqlString.AppendFormat("                                   AND A.MAT_CMF_4 {0} " + "\n", udcBUMPCondition12.SelectedValueToQueryString);
            }
            else
            {
                //상세 조회에 따른 SQL문 생성                        
                if (udcWIPCondition1.Text != "ALL" && udcWIPCondition1.Text != "")
                    strSqlString.AppendFormat("                                   AND A.MAT_GRP_1 {0}" + "\n", udcWIPCondition1.SelectedValueToQueryString);

                if (udcWIPCondition2.Text != "ALL" && udcWIPCondition2.Text != "")
                    strSqlString.AppendFormat("                                   AND A.MAT_GRP_2 {0}" + "\n", udcWIPCondition2.SelectedValueToQueryString);

                if (udcWIPCondition3.Text != "ALL" && udcWIPCondition3.Text != "")
                    strSqlString.AppendFormat("                                   AND A.MAT_GRP_3 {0} " + "\n", udcWIPCondition3.SelectedValueToQueryString);

                if (udcWIPCondition4.Text != "ALL" && udcWIPCondition4.Text != "")
                    strSqlString.AppendFormat("                                   AND A.MAT_GRP_4 {0} " + "\n", udcWIPCondition4.SelectedValueToQueryString);

                if (udcWIPCondition5.Text != "ALL" && udcWIPCondition5.Text != "")
                    strSqlString.AppendFormat("                                   AND A.MAT_GRP_5 {0} " + "\n", udcWIPCondition5.SelectedValueToQueryString);

                if (udcWIPCondition6.Text != "ALL" && udcWIPCondition6.Text != "")
                    strSqlString.AppendFormat("                                   AND A.MAT_GRP_6 {0} " + "\n", udcWIPCondition6.SelectedValueToQueryString);

                if (udcWIPCondition7.Text != "ALL" && udcWIPCondition7.Text != "")
                    strSqlString.AppendFormat("                                   AND A.MAT_GRP_7 {0} " + "\n", udcWIPCondition7.SelectedValueToQueryString);

                if (udcWIPCondition8.Text != "ALL" && udcWIPCondition8.Text != "")
                    strSqlString.AppendFormat("                                   AND A.MAT_GRP_8 {0} " + "\n", udcWIPCondition8.SelectedValueToQueryString);

                if (udcWIPCondition9.Text != "ALL" && udcWIPCondition9.Text != "")
                    strSqlString.AppendFormat("                                   AND A.MAT_GRP_9 {0} " + "\n", udcWIPCondition9.SelectedValueToQueryString);
            }
            #endregion

            strSqlString.Append("                               ) A" + "\n");
            strSqlString.Append("                             , CWIPSAPTAT@RPTTOMES B" + "\n");
            strSqlString.Append("                             , DT C" + "\n");
            strSqlString.Append("                         WHERE 1=1" + "\n");
            strSqlString.Append("                           AND A.VENDOR_ID = B.SAP_CODE(+)" + "\n");
            strSqlString.Append("                           AND A.MAT_ID = C.MAT_ID(+)" + "\n");
            strSqlString.Append("                           AND A.OPER = B.OPER(+)" + "\n");
            strSqlString.Append("                           AND A.OPER = C.OPER(+)" + "\n");
            strSqlString.Append("                           AND B.FACTORY(+) = '" + cdvFactory.txtValue + "'" + "\n");
            strSqlString.Append("                           AND B.RESV_FIELD_1(+) = ' '" + "\n");
            strSqlString.Append("                       )" + "\n");
            strSqlString.AppendFormat("                 GROUP BY {0}, OPER" + "\n", QueryCond1);            
            strSqlString.Append("               ) A" + "\n");
            strSqlString.Append("             , (SELECT DECODE(LEVEL, 1, 'WIP', 2, '공정 Target', 3, '공정 TAT', 4, '누계 Target', 5, '누계 TAT') AS GUBUN FROM DUAL CONNECT BY LEVEL <= 5) B" + "\n");
            strSqlString.Append("       )" + "\n");
            strSqlString.AppendFormat(" GROUP BY {0}, GUBUN" + "\n", QueryCond1);
            strSqlString.AppendFormat(" ORDER BY {0}, DECODE(GUBUN, 'WIP', 1, '공정 Target', 2, '공정 TAT', 3, '누계 Target', 4, '누계 TAT', 5)" + "\n", QueryCond1);
            
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
            Color colorStep = System.Drawing.Color.FromArgb(((System.Byte)(200)), ((System.Byte)(200)), ((System.Byte)(200)));
            Color colorWip = System.Drawing.Color.FromArgb(((System.Byte)(255)), ((System.Byte)(251)), ((System.Byte)(170)));            
            int iGuBun = 0;

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

                spdData.DataSource = dt;

                if (cdvFactory.txtValue.Equals("HMKB1"))
                {
                    iGuBun = 13;
                }
                else
                {
                    iGuBun = 12;
                }
    
                for (int i = 0; i < spdData.ActiveSheet.RowCount; i++)
                {
                    if (spdData.ActiveSheet.Cells[i, iGuBun].Value.ToString() == "WIP")         // 구분
                    {               
                        spdData.ActiveSheet.Rows[i].BackColor = colorWip;               
                    }

                    if (spdData.ActiveSheet.Cells[i, iGuBun].Value.ToString() == "공정 TAT")    // 구분
                    {
                        for (int y = iGuBun + 1; y < spdData.ActiveSheet.ColumnCount; y++)
                        {
                            if (Convert.ToDouble(spdData.ActiveSheet.Cells[i, y].Value) > Convert.ToDouble(spdData.ActiveSheet.Cells[i - 1, y].Value))
                            {
                                spdData.ActiveSheet.Cells[i, y].BackColor = Color.Red;
                            }
                        }
                    }

                    if (spdData.ActiveSheet.Cells[i, iGuBun].Value.ToString() == "누계 TAT")    // 구분
                    {
                        for (int y = iGuBun + 1; y < spdData.ActiveSheet.ColumnCount; y++)
                        {
                            if (Convert.ToDouble(spdData.ActiveSheet.Cells[i, y].Value) > Convert.ToDouble(spdData.ActiveSheet.Cells[i - 1, y].Value))
                            {
                                spdData.ActiveSheet.Cells[i, y].BackColor = Color.Red;
                            }
                        }
                    }

                }

                //그루핑 순서 1순위만 SubTotal을 사용하기 위해서 첫번째 값을 가져옴
                //int sub = ((udcTableForm)(this.btnSort.BindingForm)).GetCheckedValue(2);

                // 0값 표시 설정
                //spdData.isShowZero = true;

                ////1.Griid 합계 표시
                //int[] rowType = spdData.RPT_DataBindingWithSubTotal(dt, 0, sub+1, 14, null, null, btnSort);

                ////int[] rowType = spdData.RPT_DataBindingWithSubTotal(dt, 0, sub + 1, 13, null, null, btnSort, true);
                ////3. Total부분 셀머지
                //spdData.RPT_FillDataSelectiveCells("Total", 0, 14, 0, 1, true, Align.Center, VerticalAlign.Center);
                                
                ////4. Column Auto Fit
                //spdData.RPT_AutoFit(false);

                //spdData.ActiveSheet.Columns[9].AllowAutoSort = true;
                //spdData.ActiveSheet.Columns[11].AllowAutoSort = true;
                //spdData.ActiveSheet.Columns[12].AllowAutoSort = true;
                //spdData.ActiveSheet.Columns[13].AllowAutoSort = true;

                //5. 데이타가 0인 부분은 제거(Add by John. 2009.1.13)
                //spdData.RPT_RemoveZeroColumn(15, 1);               

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

        private void cdvFactory_ValueSelectedItemChanged(object sender, Miracom.UI.MCCodeViewSelChanged_EventArgs e)
        {
            if (cdvFactory.txtValue.Equals("HMKB1"))
            {
                BaseFormType = eBaseFormType.BUMP_BASE;
                pnlBUMPDetail.Visible = false;

                ckbKpcs.Checked = false;
            }
            else
            {
                BaseFormType = eBaseFormType.WIP_BASE;
                pnlWIPDetail.Visible = false;
            }

            this.SetFactory(cdvFactory.txtValue);

            SortInit();                               
         }
         #endregion
       

        private void spdData_CellClick(object sender, FarPoint.Win.Spread.CellClickEventArgs e)
        {
            int iGuBun = 0;

            if (cdvFactory.txtValue.Equals("HMKB1"))
            {
                iGuBun = 13;
            }
            else
            {
                iGuBun = 12;
            }

            if (spdData.ActiveSheet.Cells[e.Row, iGuBun].Value.ToString() == "WIP" && e.Column >= iGuBun)       // 구분
            {
                string[] dataArry = new string[iGuBun];
                string sOper = spdData.ActiveSheet.Cells[e.Row, e.Column].Column.Label;

                // Group 정보 가져와서 담기... 상세 팝업에서 조건값으로 사용하기 위해
                for (int i = 0; i < iGuBun; i++)
                {
                    if (cdvFactory.txtValue.Equals("HMKB1"))
                    {
                        if (spdData.ActiveSheet.Columns[i].Label == "CUSTOMER")
                            dataArry[0] = spdData.ActiveSheet.Cells[e.Row, i].Value.ToString();

                        else if (spdData.ActiveSheet.Columns[i].Label == "Bumping Type")
                            dataArry[1] = spdData.ActiveSheet.Cells[e.Row, i].Value.ToString();

                        else if (spdData.ActiveSheet.Columns[i].Label == "Process Flow")
                            dataArry[2] = spdData.ActiveSheet.Cells[e.Row, i].Value.ToString();

                        else if (spdData.ActiveSheet.Columns[i].Label == "Layer classification")
                            dataArry[3] = spdData.ActiveSheet.Cells[e.Row, i].Value.ToString();

                        else if (spdData.ActiveSheet.Columns[i].Label == "PKG Type")
                            dataArry[4] = spdData.ActiveSheet.Cells[e.Row, i].Value.ToString();

                        else if (spdData.ActiveSheet.Columns[i].Label == "RDL Plating")
                            dataArry[5] = spdData.ActiveSheet.Cells[e.Row, i].Value.ToString();

                        else if (spdData.ActiveSheet.Columns[i].Label == "Final Bump")
                            dataArry[6] = spdData.ActiveSheet.Cells[e.Row, i].Value.ToString();

                        else if (spdData.ActiveSheet.Columns[i].Label == "Sub. Material")
                            dataArry[7] = spdData.ActiveSheet.Cells[e.Row, i].Value.ToString();

                        else if (spdData.ActiveSheet.Columns[i].Label == "Size")
                            dataArry[8] = spdData.ActiveSheet.Cells[e.Row, i].Value.ToString();

                        else if (spdData.ActiveSheet.Columns[i].Label == "Thickness")
                            dataArry[9] = spdData.ActiveSheet.Cells[e.Row, i].Value.ToString();

                        else if (spdData.ActiveSheet.Columns[i].Label == "Flat Type")
                            dataArry[10] = spdData.ActiveSheet.Cells[e.Row, i].Value.ToString();

                        else if (spdData.ActiveSheet.Columns[i].Label == "Wafer Orientation")
                            dataArry[11] = spdData.ActiveSheet.Cells[e.Row, i].Value.ToString();

                        else
                            dataArry[12] = spdData.ActiveSheet.Cells[e.Row, i].Value.ToString();
                    }
                    else
                    {
                        if (spdData.ActiveSheet.Columns[i].Label == "CUSTOMER")
                            dataArry[0] = spdData.ActiveSheet.Cells[e.Row, i].Value.ToString();

                        else if (spdData.ActiveSheet.Columns[i].Label == "MAJOR")
                            dataArry[1] = spdData.ActiveSheet.Cells[e.Row, i].Value.ToString();

                        else if (spdData.ActiveSheet.Columns[i].Label == "FAMILY")
                            dataArry[2] = spdData.ActiveSheet.Cells[e.Row, i].Value.ToString();

                        else if (spdData.ActiveSheet.Columns[i].Label == "PKG")
                            dataArry[3] = spdData.ActiveSheet.Cells[e.Row, i].Value.ToString();

                        else if (spdData.ActiveSheet.Columns[i].Label == "TYPE1")
                            dataArry[4] = spdData.ActiveSheet.Cells[e.Row, i].Value.ToString();

                        else if (spdData.ActiveSheet.Columns[i].Label == "TYPE2")
                            dataArry[5] = spdData.ActiveSheet.Cells[e.Row, i].Value.ToString();

                        else if (spdData.ActiveSheet.Columns[i].Label == "LD COUNT")
                            dataArry[6] = spdData.ActiveSheet.Cells[e.Row, i].Value.ToString();

                        else if (spdData.ActiveSheet.Columns[i].Label == "DENSITY")
                            dataArry[7] = spdData.ActiveSheet.Cells[e.Row, i].Value.ToString();

                        else if (spdData.ActiveSheet.Columns[i].Label == "GENERATION")
                            dataArry[8] = spdData.ActiveSheet.Cells[e.Row, i].Value.ToString();

                        else if (spdData.ActiveSheet.Columns[i].Label == "PIN TYPE")
                            dataArry[9] = spdData.ActiveSheet.Cells[e.Row, i].Value.ToString();

                        else if (spdData.ActiveSheet.Columns[i].Label == "PKG CODE")
                            dataArry[10] = spdData.ActiveSheet.Cells[e.Row, i].Value.ToString();

                        else
                            dataArry[11] = spdData.ActiveSheet.Cells[e.Row, i].Value.ToString();
                    }

                }

                DataTable dt = CmnFunction.oComm.GetFuncDataTable("DYNAMIC", MakeSqlDetail(dataArry, sOper));

                if (dt != null && dt.Rows.Count > 0)
                {
                    System.Windows.Forms.Form frm = new TAT050403_P1("", dt);
                    frm.ShowDialog();
                }
            }
        }

        private string MakeSqlDetail(string[] dataArry, string sOper)
        {
            StringBuilder strSqlString = new StringBuilder();

            string date = cdvDate.SelectedValue();
            string sTime = null;

            if (ckdTime.Checked == true)
            {
                sTime = "24";
            }
            else
            {
                sTime = "1";
            }

            strSqlString.Append("SELECT B.MAT_ID, B.OPER, B.LOT_ID, B.QTY_1" + "\n");

            if (date == DateTime.Now.ToString("yyyyMMdd"))
            { 
                strSqlString.Append("     , ROUND((SYSDATE - TO_DATE(B.OPER_IN_TIME, 'YYYYMMDDHH24MISS')) * " + sTime + ", 1) AS TAT" + "\n");
                strSqlString.Append("     , ROUND((DECODE(B.RESV_FIELD_1, ' ', 0, SYSDATE - TO_DATE(B.RESV_FIELD_1, 'YYYYMMDDHH24MISS'))) * " + sTime + ", 1) AS SUM_TAT" + "\n");  //-- ?????
            }
            else
            {                
                strSqlString.Append("     , ROUND((TO_DATE(" + date + "220000, 'YYYYMMDDHH24MISS') - TO_DATE(A.OPER_IN_TIME, 'YYYYMMDDHH24MISS')) * " + sTime + ", 2) AS TAT" + "\n");
                strSqlString.Append("     , ROUND((DECODE(A.RESV_FIELD_1, ' ', 0, TO_DATE(" + date + "220000, 'YYYYMMDDHH24MISS') - TO_DATE(A.RESV_FIELD_1, 'YYYYMMDDHH24MISS'))) * " + sTime + ", 2) AS SUM_TAT" + "\n");  //-- ?????
            }

            strSqlString.Append("     , ROUND(A.SUM_TARGET_TAT * " + sTime + ", 1) AS SUM_TARGET_TAT" + "\n");
            strSqlString.Append("  FROM (" + "\n");
            strSqlString.Append("        SELECT A.*" + "\n");
            strSqlString.Append("             , NVL(B.TAT_DAY + B.TAT_DAY_WAIT, 0) AS TARGET_TAT" + "\n");
            strSqlString.Append("             , SUM(NVL(B.TAT_DAY + B.TAT_DAY_WAIT, 0)) OVER(PARTITION BY A.MAT_ID ORDER BY A.SEQ_NUM) AS SUM_TARGET_TAT" + "\n");
            strSqlString.Append("          FROM (" + "\n");
            strSqlString.Append("                SELECT A.FACTORY, A.MAT_ID, A.VENDOR_ID, B.OPER, B.SEQ_NUM" + "\n");
            strSqlString.Append("                  FROM MWIPMATDEF A " + "\n");
            strSqlString.Append("                     , MWIPFLWOPR@RPTTOMES B" + "\n");
            strSqlString.Append("                 WHERE 1=1" + "\n");
            strSqlString.Append("                   AND A.FACTORY = B.FACTORY" + "\n");
            strSqlString.Append("                   AND A.FIRST_FLOW = B.FLOW   " + "\n");
            strSqlString.Append("                   AND A.FACTORY = '" + cdvFactory.txtValue + "'" + "\n");
            strSqlString.Append("                   AND A.DELETE_FLAG = ' '" + "\n");
            strSqlString.Append("                   AND A.MAT_TYPE = 'FG'" + "\n");

            #region 상세 조회에 따른 SQL문 생성
            if (cdvFactory.Text.Trim() == "HMKB1")
            {
                if (dataArry[0] != " ")
                    strSqlString.AppendFormat("                   AND A.MAT_GRP_1 = '" + dataArry[0] + "'" + "\n");

                if (dataArry[1] != " ")
                    strSqlString.AppendFormat("                   AND A.MAT_GRP_2 = '" + dataArry[1] + "'" + "\n");

                if (dataArry[2] != " ")
                    strSqlString.AppendFormat("                   AND A.MAT_GRP_3 = '" + dataArry[2] + "'" + "\n");

                if (dataArry[3] != " ")
                    strSqlString.AppendFormat("                   AND A.MAT_GRP_4 = '" + dataArry[3] + "'" + "\n");

                if (dataArry[4] != " ")
                    strSqlString.AppendFormat("                   AND A.MAT_GRP_5 = '" + dataArry[4] + "'" + "\n");

                if (dataArry[5] != " ")
                    strSqlString.AppendFormat("                   AND A.MAT_GRP_6 = '" + dataArry[5] + "'" + "\n");

                if (dataArry[6] != " ")
                    strSqlString.AppendFormat("                   AND A.MAT_GRP_7 = '" + dataArry[6] + "'" + "\n");

                if (dataArry[7] != " ")
                    strSqlString.AppendFormat("                   AND A.MAT_GRP_8 = '" + dataArry[7] + "'" + "\n");

                if (dataArry[8] != " ")
                    strSqlString.AppendFormat("                   AND A.MAT_CMF_14 = '" + dataArry[8] + "'" + "\n");

                if (dataArry[9] != " ")
                    strSqlString.AppendFormat("                   AND A.MAT_CMF_2 = '" + dataArry[9] + "'" + "\n");

                if (dataArry[10] != " ")
                    strSqlString.AppendFormat("                   AND A.MAT_CMF_3 = '" + dataArry[10] + "'" + "\n");

                if (dataArry[11] != " ")
                    strSqlString.AppendFormat("                   AND A.MAT_CMF_4 = '" + dataArry[11] + "'" + "\n");

                if (dataArry[12] != " ")
                    strSqlString.AppendFormat("                   AND A.MAT_ID = '" + dataArry[12] + "'" + "\n");
            }
            else
            {
                if (dataArry[0] != " ")
                    strSqlString.AppendFormat("                   AND A.MAT_GRP_1 = '" + dataArry[0] + "'" + "\n");

                if (dataArry[1] != " ")
                    strSqlString.AppendFormat("                   AND A.MAT_GRP_9 = '" + dataArry[1] + "'" + "\n");

                if (dataArry[2] != " ")
                    strSqlString.AppendFormat("                   AND A.MAT_GRP_2 = '" + dataArry[2] + "'" + "\n");

                if (dataArry[3] != " ")
                    strSqlString.AppendFormat("                   AND A.MAT_GRP_10 = '" + dataArry[3] + "'" + "\n");

                if (dataArry[4] != " ")
                    strSqlString.AppendFormat("                   AND A.MAT_GRP_4 = '" + dataArry[4] + "'" + "\n");

                if (dataArry[5] != " ")
                    strSqlString.AppendFormat("                   AND A.MAT_GRP_5 = '" + dataArry[5] + "'" + "\n");

                if (dataArry[6] != " ")
                    strSqlString.AppendFormat("                   AND A.MAT_GRP_6 = '" + dataArry[6] + "'" + "\n");

                if (dataArry[7] != " ")
                    strSqlString.AppendFormat("                   AND A.MAT_GRP_7 = '" + dataArry[7] + "'" + "\n");

                if (dataArry[8] != " ")
                    strSqlString.AppendFormat("                   AND A.MAT_GRP_8 = '" + dataArry[8] + "'" + "\n");

                if (dataArry[9] != " ")
                    strSqlString.AppendFormat("                   AND A.MAT_CMF_10 = '" + dataArry[9] + "'" + "\n");

                if (dataArry[10] != " ")
                    strSqlString.AppendFormat("                   AND A.MAT_CMF_11 = '" + dataArry[10] + "'" + "\n");

                if (dataArry[11] != " ")
                    strSqlString.AppendFormat("                   AND A.MAT_ID = '" + dataArry[11] + "'" + "\n");
            }
            #endregion

            strSqlString.Append("               ) A" + "\n");
            strSqlString.Append("             , CWIPSAPTAT@RPTTOMES B " + "\n");
            strSqlString.Append("         WHERE 1=1  " + "\n");
            strSqlString.Append("           AND A.FACTORY = B.FACTORY(+)" + "\n");
            strSqlString.Append("           AND A.VENDOR_ID = B.SAP_CODE(+) " + "\n");
            strSqlString.Append("           AND A.OPER = B.OPER(+) " + "\n");
            strSqlString.Append("           AND B.RESV_FIELD_1(+) = ' ' " + "\n");
            strSqlString.Append("       ) A" + "\n");

            if (date == DateTime.Now.ToString("yyyyMMdd"))
            {
                strSqlString.Append("     , RWIPLOTSTS B" + "\n");
                strSqlString.Append(" WHERE 1=1  " + "\n");                
            }
            else
            {
                strSqlString.Append("     , RWIPLOTSTS_BOH B" + "\n");
                strSqlString.Append(" WHERE 1=1  " + "\n");
                strSqlString.Append("   AND B.CUTOFF_DT = '" + date + "22' " + "\n");
            }

            strSqlString.Append("   AND A.FACTORY = B.FACTORY" + "\n");
            strSqlString.Append("   AND A.MAT_ID = B.MAT_ID" + "\n");
            strSqlString.Append("   AND A.OPER = B.OPER" + "\n");
            strSqlString.Append("   AND B.FACTORY = '" + cdvFactory.txtValue + "'" + "\n");
            strSqlString.Append("   AND B.LOT_DEL_FLAG = ' '" + "\n");
            strSqlString.Append("   AND B.LOT_TYPE = 'W'" + "\n");
            strSqlString.Append("   AND B.QTY_1 > 0" + "\n");

            if (sOper != "구분")
            {
                strSqlString.Append("   AND B.OPER = '" + sOper + "'" + "\n");
            }

            if (cdvLotType.Text != "ALL")
            {
                strSqlString.Append("   AND B.LOT_CMF_5 LIKE '" + cdvLotType.Text + "'" + "\n");
            }

            

            strSqlString.Append(" ORDER BY SUM_TAT DESC, MAT_ID, OPER" + "\n");

            if (GlobalVariable.gsUserGroup == "ADMIN_GROUP" || GlobalVariable.gsUserGroup == "HANA_ADMIN_GROUP")
            {
                System.Windows.Forms.Clipboard.SetText(strSqlString.ToString());
            }

            return strSqlString.ToString();
        }

     }
}
