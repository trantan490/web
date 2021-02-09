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
    /// 클  래  스: RAS020105<br/>
    /// 클래스요약: 설비 생산 이력<br/>
    /// 작  성  자: 미라콤 양형석<br/>
    /// 최초작성일: 2008-12-17<br/>
    /// 상세  설명: 설비 생산 이력<br/>
    /// 변경  내용: <br/>
    /// 
    /// 2009-11-10-임종우 : 설비 LOT END 테이블 변경 및 IN/OUT Time 동일해도 생산량 값 표시 수정    
    /// 2009-11-23-임종우 : 설비 UPEH 2개 이상인 데이터 HIST_SEQ가 최근값인 것 하나만 가져오기
    /// 2013-05-09-임종우 : 공정 추가 표시 (이종걸 요청)
    /// 2015-01-30-임종우 : 시간 검색 가능하도록 변경, YIELD 추가 (황혜리 요청)
    /// 2015-03-23-임종우 : 모듈 CNT 정보 추가 (김준용K 요청)
    /// 2015-11-16-임종우 : 기준 UPEH 데이터 테이블 변경 CWIPRESLTH -> CWIPLOTEND --기존 테이블에서 데이터가 누락 되는 경우가 많아서 사용 금지
    /// 2016-02-16-임종우 : 쿼리 1차 튜닝..불필요 테이블 정리.. 
    /// 2016-02-16-임종우 : Back Lap Multi 기능 추가 (배진우C 요청) 
    /// </summary>
    public partial class RAS020105 : Miracom.SmartWeb.UI.Controls.udcCUSReport002
    {
        public RAS020105()
        {
            InitializeComponent();
            cdvFromToDate.AutoBinding();
            SortInit();
            GridColumnInit();
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
            spdData.ActiveSheet.ColumnHeader.Rows.Count = 1;
            spdData.ActiveSheet.RowHeader.ColumnCount = 0;
            spdData.RPT_ColumnInit();


            spdData.RPT_AddBasicColumn("date", 0, 0, Visibles.True, Frozen.True, Align.Center, Merge.True, Formatter.String, 80);
            spdData.RPT_AddBasicColumn("Operation", 0, 1, Visibles.True, Frozen.True, Align.Center, Merge.False, Formatter.String, 80);
            spdData.RPT_AddBasicColumn("Equipment name", 0, 2, Visibles.True, Frozen.True, Align.Center, Merge.True, Formatter.String, 80);
            spdData.RPT_AddBasicColumn("Team in charge", 0, 3, Visibles.True, Frozen.True, Align.Center, Merge.True, Formatter.String, 80);
            spdData.RPT_AddBasicColumn("파트", 0, 4, Visibles.True, Frozen.True, Align.Center, Merge.True, Formatter.String, 80);
            spdData.RPT_AddBasicColumn("EQP Type", 0, 5, Visibles.True, Frozen.False, Align.Center, Merge.True, Formatter.String, 80);
            spdData.RPT_AddBasicColumn("Maker", 0, 6, Visibles.True, Frozen.False, Align.Center, Merge.True, Formatter.String, 80);
            spdData.RPT_AddBasicColumn("Model", 0, 7, Visibles.True, Frozen.False, Align.Center, Merge.True, Formatter.String, 80);

            spdData.RPT_AddBasicColumn("Customer", 0, 8, Visibles.True, Frozen.False, Align.Center, Merge.True, Formatter.String, 80);
            spdData.RPT_AddBasicColumn("Family", 0, 9, Visibles.False, Frozen.False, Align.Center, Merge.True, Formatter.String, 80);
            spdData.RPT_AddBasicColumn("Package", 0, 10, Visibles.True, Frozen.False, Align.Center, Merge.True, Formatter.String, 80);
            spdData.RPT_AddBasicColumn("Type1", 0, 11, Visibles.False, Frozen.False, Align.Center, Merge.True, Formatter.String, 80);
            spdData.RPT_AddBasicColumn("Type2", 0, 12, Visibles.False, Frozen.False, Align.Center, Merge.True, Formatter.String, 80);
            spdData.RPT_AddBasicColumn("LD Count", 0, 13, Visibles.False, Frozen.False, Align.Center, Merge.True, Formatter.String, 80);
            spdData.RPT_AddBasicColumn("Density", 0, 14, Visibles.False, Frozen.False, Align.Center, Merge.True, Formatter.String, 80);
            spdData.RPT_AddBasicColumn("Generation", 0, 15, Visibles.False, Frozen.False, Align.Center, Merge.True, Formatter.String, 80);

            spdData.RPT_AddBasicColumn("Product", 0, 16, Visibles.True, Frozen.False, Align.Center, Merge.True, Formatter.String, 120);
            spdData.RPT_AddBasicColumn("Lot Id", 0, 17, Visibles.True, Frozen.False, Align.Center, Merge.False, Formatter.String, 80);
            spdData.RPT_AddBasicColumn("Standard UPEH", 0, 18, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 80);
            spdData.RPT_AddBasicColumn("실UPEH", 0, 19, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 80);
            spdData.RPT_AddBasicColumn("Input", 0, 20, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 80);
            spdData.RPT_AddBasicColumn("Output", 0, 21, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 80);
            spdData.RPT_AddBasicColumn("Loss", 0, 22, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 80);
            spdData.RPT_AddBasicColumn("Yield", 0, 23, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Double2, 80);
            spdData.RPT_AddBasicColumn("working time", 0, 24, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 80);
            spdData.RPT_AddBasicColumn("In Time", 0, 25, Visibles.True, Frozen.False, Align.Center, Merge.False, Formatter.String, 120);
            spdData.RPT_AddBasicColumn("Out Time", 0, 26, Visibles.True, Frozen.False, Align.Center, Merge.False, Formatter.String, 120);
            spdData.RPT_AddBasicColumn("Module CNT", 0, 27, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 70);

            spdData.RPT_ColumnConfigFromTable(btnSort); //Group항목이 있을경우 반드시 선언해줄것.
        }

        /// <summary>
        /// 3. Group By 정의
        /// </summary>
        private void SortInit()
        {
            ((udcTableForm)(this.btnSort.BindingForm)).AddRow("date", "SUBSTR(TRAN_TIME, 1, 8) WORK_DATE", "1", "SUBSTR(TRAN_TIME, 1, 8)", true);
            ((udcTableForm)(this.btnSort.BindingForm)).AddRow("Operation", "RES.OPER", "RES.OPER", "RES.OPER", false);
            ((udcTableForm)(this.btnSort.BindingForm)).AddRow("Equipment name", "RES.RES_ID AS RES", "RES.RES_ID", "RES.RES_ID", true);
            ((udcTableForm)(this.btnSort.BindingForm)).AddRow("Team in charge", "NVL((SELECT DATA_1 FROM MGCMTBLDAT WHERE FACTORY = RES.FACTORY AND TABLE_NAME = 'H_DEPARTMENT' AND ROWNUM=1 AND KEY_1 = RES.RES_GRP_1), '-') AS TEAM", "RES.RES_GRP_1", "RES.RES_GRP_1", true);
            ((udcTableForm)(this.btnSort.BindingForm)).AddRow("Part", "NVL((SELECT DATA_1 FROM MGCMTBLDAT WHERE FACTORY = RES.FACTORY AND TABLE_NAME = 'H_DEPARTMENT' AND ROWNUM=1 AND KEY_1 = RES.RES_GRP_2), '-') AS PART", "RES.RES_GRP_2", "RES.RES_GRP_2", true);
            ((udcTableForm)(this.btnSort.BindingForm)).AddRow("EQP Type", "RES.RES_GRP_3 AS EQP_TYPE", "RES.RES_GRP_3", "RES.RES_GRP_3", true);
            ((udcTableForm)(this.btnSort.BindingForm)).AddRow("Maker", "RES.RES_GRP_5 AS MAKER", "RES.RES_GRP_5", "RES.RES_GRP_5", true);
            ((udcTableForm)(this.btnSort.BindingForm)).AddRow("Model", "RES.RES_GRP_6 AS MODEL", "RES.RES_GRP_6", "RES.RES_GRP_6", true);

            ((udcTableForm)(this.btnSort.BindingForm)).AddRow("Customer", "NVL((SELECT DATA_1 FROM MGCMTBLDAT WHERE FACTORY = RES.FACTORY AND TABLE_NAME = 'H_CUSTOMER' AND ROWNUM=1 AND KEY_1 = MAT.MAT_GRP_1), '-') AS CUSTOMER", "MAT.MAT_GRP_1", "MAT.MAT_GRP_1", true);
            ((udcTableForm)(this.btnSort.BindingForm)).AddRow("Family", "MAT.MAT_GRP_2 as Family", "MAT.MAT_GRP_2", "MAT.MAT_GRP_2", false);
            ((udcTableForm)(this.btnSort.BindingForm)).AddRow("Package", "MAT.MAT_GRP_3 as Package", "MAT.MAT_GRP_3", "MAT.MAT_GRP_3", true);
            ((udcTableForm)(this.btnSort.BindingForm)).AddRow("Type1", "MAT.MAT_GRP_4 as Type1", "MAT.MAT_GRP_4", "MAT.MAT_GRP_4", false);
            ((udcTableForm)(this.btnSort.BindingForm)).AddRow("Type2", "MAT.MAT_GRP_5 as Type2", "MAT.MAT_GRP_5", "MAT.MAT_GRP_5", false);
            ((udcTableForm)(this.btnSort.BindingForm)).AddRow("LD Count", "MAT.MAT_GRP_6 as LD_Count", "MAT.MAT_GRP_6", "MAT.MAT_GRP_6", false);
            ((udcTableForm)(this.btnSort.BindingForm)).AddRow("Density", "MAT.MAT_GRP_7 as Density", "MAT.MAT_GRP_7", "MAT.MAT_GRP_7", false);
            ((udcTableForm)(this.btnSort.BindingForm)).AddRow("Generation", "MAT.MAT_GRP_8 as Generation", "MAT.MAT_GRP_8", "MAT.MAT_GRP_8", false);
        }

        /// <summary>
        /// 4. 쿼리 생성
        /// </summary>
        /// <returns></returns>*
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

            string strFromDate = cdvFromToDate.Start_Tran_Time;
            string strToDate = cdvFromToDate.End_Tran_Time;

            #region " LOSS 코드 구하기 : dt "

            strSqlString.Remove(0, strSqlString.Length);
            strSqlString.Append("SELECT DISTINCT(LOSS.CODE) CODE" + "\n");
            strSqlString.Append("  FROM ( " + "\n");
            strSqlString.Append("        SELECT FACTORY, END_RES_ID AS RES_ID, MAT_ID, LOT_ID, START_TIME AS IN_TIME, TRAN_TIME AS OUT_TIME " + "\n");
            strSqlString.Append("          FROM CWIPLOTEND" + "\n");
            strSqlString.Append("         WHERE FACTORY " + cdvFactory.SelectedValueToQueryString + "\n");
            strSqlString.Append("           AND TRAN_TIME BETWEEN '" + strFromDate + "' AND '" + strToDate + "'" + "\n");
            strSqlString.Append("           AND END_RES_ID != ' ' " + "\n");            

            //if (cboGubun.Text == "PCB")
            if (cboGubun.SelectedIndex == 1)
            {
                strSqlString.Append("           AND LOT_TYPE = 'P' " + "\n");
            }
            else
            {
                strSqlString.Append("           AND LOT_TYPE = 'W' " + "\n");
            }

            strSqlString.Append("           AND START_RES_ID = END_RES_ID  " + "\n");
            strSqlString.Append("       ) LTH  " + "\n");
            strSqlString.Append("     , ( " + "\n");
            strSqlString.Append("        SELECT LOSS_CODE CODE, LOSS_QTY QTY, FACTORY, RES_ID, MAT_ID, LOT_ID, TRAN_TIME " + "\n");
            strSqlString.Append("          FROM RWIPLOTLSM " + "\n");
            strSqlString.Append("         WHERE FACTORY " + cdvFactory.SelectedValueToQueryString + "\n");
            strSqlString.Append("           AND TRAN_TIME BETWEEN '" + strFromDate + "' AND '" + strToDate + "'" + "\n");
            strSqlString.Append("           AND MAT_VER = 1   " + "\n");
            strSqlString.Append("           AND HIST_DEL_FLAG = ' ' " + "\n");
            strSqlString.Append("       ) LOSS  " + "\n");
            strSqlString.Append("     , MRASRESDEF RES " + "\n");
            strSqlString.Append("     , MWIPMATDEF MAT " + "\n");
            strSqlString.Append(" WHERE 1=1 " + "\n");
            strSqlString.Append("   AND RES.FACTORY = LTH.FACTORY " + "\n");
            strSqlString.Append("   AND RES.RES_ID = LTH.RES_ID " + "\n");
            strSqlString.Append("   AND MAT.FACTORY = LTH.FACTORY " + "\n");
            strSqlString.Append("   AND MAT.MAT_ID = LTH.MAT_ID " + "\n");
            strSqlString.Append("   AND RES.FACTORY " + cdvFactory.SelectedValueToQueryString + "\n");
            strSqlString.Append("   AND LTH.FACTORY = LOSS.FACTORY(+) " + "\n");
            strSqlString.Append("   AND LTH.LOT_ID = LOSS.LOT_ID(+) " + "\n");
            strSqlString.Append("   AND LTH.MAT_ID = LOSS.MAT_ID(+) " + "\n");
            strSqlString.Append("   AND LTH.RES_ID = LOSS.RES_ID(+) " + "\n");
            strSqlString.Append("   AND LOSS.TRAN_TIME(+) BETWEEN LTH.IN_TIME AND LTH.OUT_TIME " + "\n");
            strSqlString.Append("   AND RES.RES_TYPE NOT IN ('DUMMY')" + "\n");
            strSqlString.Append("   AND LOSS.CODE<>' '" + "\n");

            if (ckdMulti.Checked == true)
            {
                strSqlString.Append("   AND RES.RES_GRP_3 = 'BACK LAP' " + "\n");
            }

            #region " 조회 조건 "

            if (txtSearchProduct.Text.Trim() != "%" && txtSearchProduct.Text.Trim() != "")
                strSqlString.AppendFormat("   AND MAT.MAT_ID LIKE '{0}'" + "\n", txtSearchProduct.Text);

            #endregion

            #region " WIP 상세 조회 "
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
            #endregion

            #region " RAS 상세 조회 "
            if (udcRASCondition1.Text != "ALL" && udcRASCondition1.Text != "")
                strSqlString.AppendFormat("   AND RES.RES_GRP_1 {0} " + "\n", udcRASCondition1.SelectedValueToQueryString);

            if (udcRASCondition2.Text != "ALL" && udcRASCondition2.Text != "")
                strSqlString.AppendFormat("   AND RES.RES_GRP_2 {0} " + "\n", udcRASCondition2.SelectedValueToQueryString);

            if (udcRASCondition3.Text != "ALL" && udcRASCondition3.Text != "")
                strSqlString.AppendFormat("   AND RES.RES_GRP_3 {0} " + "\n", udcRASCondition3.SelectedValueToQueryString);

            if (udcRASCondition4.Text != "ALL" && udcRASCondition4.Text != "")
                strSqlString.AppendFormat("   AND RES.RES_GRP_5 {0} " + "\n", udcRASCondition4.SelectedValueToQueryString);

            if (udcRASCondition5.Text != "ALL" && udcRASCondition5.Text != "")
                strSqlString.AppendFormat("   AND RES.RES_GRP_6 {0} " + "\n", udcRASCondition5.SelectedValueToQueryString);

            if (udcRASCondition6.Text != "ALL" && udcRASCondition6.Text != "")
                strSqlString.AppendFormat("   AND RES.RES_ID {0} " + "\n", udcRASCondition6.SelectedValueToQueryString);

            if (udcRASCondition7.Text != "ALL" && udcRASCondition7.Text != "")
                strSqlString.AppendFormat("   AND RES.RES_ID IN ( SELECT UNIQUE RES_ID FROM CRASRESUSR WHERE USER_DESC {0} ) " + "\n", udcRASCondition7.SelectedValueToQueryString);

            #endregion


            strSqlString.Append("ORDER BY LOSS.CODE \n");
            System.Windows.Forms.Clipboard.SetText(strSqlString.ToString()); 
            DataTable dt = CmnFunction.oComm.GetFuncDataTable("DYNAMIC", strSqlString.ToString());

            #endregion
            ////////////////////LOSS CODE가져오기 끝.....아래 실제 DATA가져오는 쿼리와 동일 하게 적용할것....

            strSqlString.Remove(0, strSqlString.Length);

            if (ckdMulti.Checked == false)
            {
                strSqlString.Append("SELECT " + QueryCond1 + "\n");
                strSqlString.Append("     , RES.MAT_ID AS PRODUCT " + "\n");
                strSqlString.Append("     , RES.LOT_ID AS LOT_ID " + "\n");
                strSqlString.Append("     , NVL(RES.UPEH,0) AS STD_UPEH " + "\n");
                strSqlString.Append("     , DECODE(RES.WORK_TIME,0,1,TRUNC(RES.OUT_QTY/RES.WORK_TIME)) AS REAL_UPEH " + "\n");
                strSqlString.Append("     , RES.OUT_QTY + RES.LOSS AS IN_QTY " + "\n");
                strSqlString.Append("     , RES.OUT_QTY " + "\n");
                strSqlString.Append("     , RES.LOSS " + "\n");
                strSqlString.Append("     , ROUND(RES.OUT_QTY / (RES.OUT_QTY + RES.LOSS) * 100, 2) AS YIELD " + "\n");
                strSqlString.Append("     , TRUNC(WORK_TIME*60) WORK_TIME " + "\n");
                strSqlString.Append("     , TO_CHAR(TO_DATE(RES.IN_TIME, 'YYYYMMDDHH24MISS'), 'YYYY-MM-DD HH24:MI:SS') IN_TIME " + "\n");
                strSqlString.Append("     , TO_CHAR(TO_DATE(RES.OUT_TIME, 'YYYYMMDDHH24MISS'), 'YYYY-MM-DD HH24:MI:SS') OUT_TIME " + "\n");
                strSqlString.Append("     , RES.MODULE_CNT " + "\n");
            }
            else
            {
                strSqlString.Append("SELECT " + QueryCond1 + "\n");
                strSqlString.Append("     , RES.MAT_ID AS PRODUCT " + "\n");
                strSqlString.Append("     , ' ' AS LOT_ID " + "\n");
                strSqlString.Append("     , MAX(NVL(RES.UPEH,0)) AS STD_UPEH " + "\n");
                strSqlString.Append("     , DECODE(SUM(RES.WORK_TIME),0,1,TRUNC(SUM(RES.OUT_QTY)/SUM(RES.WORK_TIME))) AS REAL_UPEH  " + "\n");
                strSqlString.Append("     , SUM(RES.OUT_QTY) + SUM(RES.LOSS) AS IN_QTY " + "\n");
                strSqlString.Append("     , SUM(RES.OUT_QTY) AS OUT_QTY " + "\n");
                strSqlString.Append("     , SUM(RES.LOSS) AS LOSS " + "\n");
                strSqlString.Append("     , ROUND(SUM(RES.OUT_QTY) / (SUM(RES.OUT_QTY) + SUM(RES.LOSS)) * 100, 2) AS YIELD  " + "\n");
                strSqlString.Append("     , SUM(TRUNC(WORK_TIME*60)) WORK_TIME " + "\n");
                strSqlString.Append("     , TO_CHAR(TO_DATE(SUBSTR(RES.IN_TIME, 1, 12) || '00', 'YYYYMMDDHH24MISS'), 'YYYY-MM-DD HH24:MI:SS') IN_TIME " + "\n");
                strSqlString.Append("     , TO_CHAR(TO_DATE(SUBSTR(RES.OUT_TIME, 1, 12) || '00', 'YYYYMMDDHH24MISS'), 'YYYY-MM-DD HH24:MI:SS') OUT_TIME " + "\n");
                strSqlString.Append("     , RES.MODULE_CNT " + "\n");
            }

            for (int i = 0; i < dt.Rows.Count; i++)
            {
                // 미리 구한 Loss 코드별로 칼럼 동적 생성 및 Select 절에 추가
                spdData.RPT_AddBasicColumn(dt.Rows[i]["CODE"].ToString(), 0, spdData.ActiveSheet.Columns.Count, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 30);
                strSqlString.Append( "     , SUM(DECODE(RES.CODE, '" + dt.Rows[i]["CODE"].ToString() + "', RES.QTY, 0)) AS LOSS_"+ i.ToString() + " \n");
            }
            strSqlString.Append("  FROM (   " + "\n");
            strSqlString.Append("        SELECT LTH.TRAN_TIME, LTH.FACTORY, LTH.RES_ID, LTH.MAT_ID, LTH.LOT_ID, RES.RES_TYPE, RES.RES_GRP_1, RES.RES_GRP_2, RES.RES_GRP_3, RES.RES_GRP_5, RES.RES_GRP_6, RES.RES_GRP_7 " + "\n");
            strSqlString.Append("             , LTH.IN_QTY, LTH.OUT_QTY, LTH.LOSS " + "\n");
            strSqlString.Append("             , LTH.WORK_TIME, LTH.UPEH " + "\n");
            strSqlString.Append("             , LTH.IN_TIME, LTH.OUT_TIME " + "\n");
            strSqlString.Append("             , LOSS.CODE " + "\n");
            strSqlString.Append("             , LOSS.QTY " + "\n");
            strSqlString.Append("             , LTH.OPER " + "\n");
            strSqlString.Append("             , LTH.MODULE_CNT " + "\n");
            strSqlString.Append("          FROM ( " + "\n");
            strSqlString.Append("                SELECT START_TIME AS TRAN_TIME, FACTORY, END_RES_ID AS RES_ID, MAT_ID, LOT_ID " + "\n");
            strSqlString.Append("                     , HIST_SEQ, OLD_OPER AS OPER, START_QTY_1 AS IN_QTY, QTY_1 AS OUT_QTY, LOSS_QTY_1 AS LOSS" + "\n");
            strSqlString.Append("                     , START_TIME AS IN_TIME, TRAN_TIME AS OUT_TIME" + "\n");
            strSqlString.Append("                     , (TO_DATE(TRAN_TIME, 'YYYYMMDDHH24MISS') - TO_DATE(START_TIME, 'YYYYMMDDHH24MISS'))*24 WORK_TIME" + "\n");
            strSqlString.Append("                     , UPEH, TRAN_CMF_6 AS MODULE_CNT " + "\n");
            strSqlString.Append("                  FROM CWIPLOTEND LOT " + "\n");
            strSqlString.Append("                 WHERE FACTORY " + cdvFactory.SelectedValueToQueryString + "\n");
            strSqlString.Append("                   AND TRAN_TIME BETWEEN '" + strFromDate + "' AND '" + strToDate + "'" + "\n");            
            strSqlString.Append("                   AND END_RES_ID != ' ' " + "\n");
            strSqlString.Append("                   AND START_RES_ID = END_RES_ID " + "\n");

            //2014-08-21-장한별  : 구분 체크박스 체크 시 생산 ,PCB 정보를 추가하기 위해★
            //if (cboGubun.Text == "PCB")
            if (cboGubun.SelectedIndex == 1)
            {
                strSqlString.Append("                   AND LOT_TYPE = 'P' " + "\n");           
            }
            else
            {
                strSqlString.Append("                   AND LOT_TYPE = 'W' " + "\n");           
            }
             
            strSqlString.Append("               ) LTH  " + "\n");
            strSqlString.Append("             , ( " + "\n");
            strSqlString.Append("                SELECT LOSS_CODE CODE, LOSS_QTY QTY, FACTORY, RES_ID, MAT_ID, LOT_ID, TRAN_TIME, HIST_SEQ " + "\n");
            strSqlString.Append("                  FROM RWIPLOTLSM     " + "\n");
            strSqlString.Append("                 WHERE FACTORY " + cdvFactory.SelectedValueToQueryString + "\n");
            strSqlString.Append("                   AND TRAN_TIME BETWEEN '" + strFromDate + "' AND '" + strToDate + "'" + "\n");
            strSqlString.Append("                   AND MAT_VER = 1   " + "\n");
            strSqlString.Append("                   AND HIST_DEL_FLAG = ' ' " + "\n");
            strSqlString.Append("               ) LOSS  " + "\n");
            strSqlString.Append("             , MRASRESDEF RES " + "\n");
            strSqlString.Append("         WHERE 1=1 " + "\n");
            strSqlString.Append("           AND RES.FACTORY = LTH.FACTORY " + "\n");
            strSqlString.Append("           AND RES.RES_ID = LTH.RES_ID " + "\n");
            strSqlString.Append("           AND RES.FACTORY " + cdvFactory.SelectedValueToQueryString + "\n");
            strSqlString.Append("           AND LTH.FACTORY = LOSS.FACTORY(+) " + "\n");
            strSqlString.Append("           AND LTH.LOT_ID = LOSS.LOT_ID(+) " + "\n");
            strSqlString.Append("           AND LTH.MAT_ID = LOSS.MAT_ID(+) " + "\n");
            strSqlString.Append("           AND LTH.RES_ID = LOSS.RES_ID(+) " + "\n");
            strSqlString.Append("           AND LOSS.TRAN_TIME(+) BETWEEN LTH.IN_TIME AND LTH.OUT_TIME " + "\n");
            strSqlString.Append("           AND RES.RES_TYPE NOT IN ('DUMMY')" + "\n");
            strSqlString.Append("       ) RES " + "\n");
            //strSqlString.Append("       , CRASUPHDEF UPH " + "\n");
            strSqlString.Append("       , MWIPMATDEF MAT " + "\n");
            strSqlString.Append(" WHERE 1=1 " + "\n");
            strSqlString.Append("   AND RES.FACTORY = MAT.FACTORY " + "\n");
            strSqlString.Append("   AND RES.MAT_ID = MAT.MAT_ID " + "\n");

            // 2014-08-21-장한별  : 구분 체크박스 체크 시 생산 ,PCB 정보를 추가하기 위해
            //if (cboGubun.Text == "PCB")
            if (cboGubun.SelectedIndex == 1)
            {
                strSqlString.Append("   AND MAT.MAT_TYPE = 'PB' " + "\n");
            }
            else
            {
                strSqlString.Append("   AND MAT.MAT_TYPE = 'FG' " + "\n");
            }


            //strSqlString.Append("   AND RES.FACTORY = UPH.FACTORY(+) " + "\n");
            //strSqlString.Append("   AND RES.PREV_OPER = UPH.OPER(+) " + "\n");
            //strSqlString.Append("   AND RES.RES_GRP_6 = UPH.RES_MODEL(+) " + "\n");
            //strSqlString.Append("   AND RES.RES_GRP_7 = UPH.UPEH_GRP(+) " + "\n");
            //strSqlString.Append("   AND RES.MAT_ID = UPH.MAT_ID(+) " + "\n");

            #region " 조회 조건 "

            if (txtSearchProduct.Text.Trim() != "%" && txtSearchProduct.Text.Trim() != "")
                strSqlString.AppendFormat("   AND MAT.MAT_ID LIKE '{0}'" + "\n", txtSearchProduct.Text);

            #endregion

            #region " WIP 상세 조회 "
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
            #endregion

            #region " RAS 상세 조회 "
            if (udcRASCondition1.Text != "ALL" && udcRASCondition1.Text != "")
                strSqlString.AppendFormat("   AND RES.RES_GRP_1 {0} " + "\n", udcRASCondition1.SelectedValueToQueryString);

            if (udcRASCondition2.Text != "ALL" && udcRASCondition2.Text != "")
                strSqlString.AppendFormat("   AND RES.RES_GRP_2 {0} " + "\n", udcRASCondition2.SelectedValueToQueryString);

            if (udcRASCondition3.Text != "ALL" && udcRASCondition3.Text != "")
                strSqlString.AppendFormat("   AND RES.RES_GRP_3 {0} " + "\n", udcRASCondition3.SelectedValueToQueryString);

            if (udcRASCondition4.Text != "ALL" && udcRASCondition4.Text != "")
                strSqlString.AppendFormat("   AND RES.RES_GRP_5 {0} " + "\n", udcRASCondition4.SelectedValueToQueryString);

            if (udcRASCondition5.Text != "ALL" && udcRASCondition5.Text != "")
                strSqlString.AppendFormat("   AND RES.RES_GRP_6 {0} " + "\n", udcRASCondition5.SelectedValueToQueryString);

            if (udcRASCondition6.Text != "ALL" && udcRASCondition6.Text != "")
                strSqlString.AppendFormat("   AND RES.RES_ID {0} " + "\n", udcRASCondition6.SelectedValueToQueryString);

            if (udcRASCondition7.Text != "ALL" && udcRASCondition7.Text != "")
                strSqlString.AppendFormat("   AND RES.RES_ID IN ( SELECT UNIQUE RES_ID FROM CRASRESUSR WHERE USER_DESC {0} ) " + "\n", udcRASCondition7.SelectedValueToQueryString);
            
            #endregion

            if (ckdMulti.Checked == false)
            {
                strSqlString.Append(" GROUP BY " + QueryCond2 + ", RES.FACTORY, RES.UPEH, RES.MAT_ID, RES.LOT_ID, RES.IN_QTY, RES.OUT_QTY, RES.LOSS, RES.WORK_TIME, RES.IN_TIME, RES.OUT_TIME, RES.MODULE_CNT \n");
                strSqlString.Append(" ORDER BY " + QueryCond2 + ", RES.FACTORY, RES.UPEH, RES.MAT_ID, RES.LOT_ID, RES.IN_QTY, RES.OUT_QTY, RES.LOSS, RES.WORK_TIME, RES.IN_TIME, RES.OUT_TIME, RES.MODULE_CNT \n");
            }
            else
            {
                strSqlString.Append("   AND RES.RES_GRP_3 = 'BACK LAP' " + "\n");
                strSqlString.Append(" GROUP BY " + QueryCond3 + ", RES.FACTORY, RES.MAT_ID, ' ', SUBSTR(RES.IN_TIME, 1, 12), SUBSTR(RES.OUT_TIME, 1, 12), RES.MODULE_CNT \n");
                strSqlString.Append(" ORDER BY " + QueryCond2 + ", RES.FACTORY, RES.MAT_ID, ' ', IN_TIME, OUT_TIME, RES.MODULE_CNT \n");
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

        private void ShowChart(int rowCount)
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

                //by John
                //1.Griid 합계 표시
                spdData.DataSource = dt;
                //int[] rowType = spdData.RPT_DataBindingWithSubTotal(dt, 0, 0, 0, null, null, btnSort);
                //spdData.Sheets[0].Rows[0].Remove();
                //2. 칼럼 고정(필요하다면..)
                //spdData.Sheets[0].FrozenColumnCount = 9;

                //3. Total부분 셀머지
                //spdData.RPT_FillDataSelectiveCells("Total", 0, 10, 0, 1, true, Align.Center, VerticalAlign.Center);

                //4. Column Auto Fit
                //spdData.RPT_AutoFit(false);

                //ShowChart(5);
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
            spdData.ExportExcel();
        }

        #region 기타
        private void cdvFactory_ValueSelectedItemChanged(object sender, Miracom.UI.MCCodeViewSelChanged_EventArgs e)
        {
            this.SetFactory(cdvFactory.txtValue);
        }
        #endregion        
    }
}