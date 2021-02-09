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
    public partial class PRD010911 : Miracom.SmartWeb.UI.Controls.udcCUSReport002
    {        
        /// <summary>
        /// 클  래  스: PRD010911<br/>
        /// 클래스요약: 모짜르트 Plan vs Actual<br/>
        /// 작  성  자: 하나마이크론 임종우<br/>
        /// 최초작성일: 2015-06-12<br/>
        /// 상세  설명: 모짜르트 Plan vs Actual(임태성K 요청)<br/>
        /// 변경  내용: <br/> 
        /// 2015-08-04-임종우 : SO QTY 추가
        /// 2015-09-21-임종우 : 주차 W2 까지 확대 (임태성K 요청)
        /// </summary>

        static string[] DateArray = new string[3];
        GlobalVariable.FindWeek FindWeek = new GlobalVariable.FindWeek();

        public PRD010911()
        {
            InitializeComponent();
            SortInit();
            cdvDate.Value = DateTime.Now;
            GridColumnInit();
            this.cdvOper.sFactory = GlobalVariable.gsAssyDefaultFactory;
            this.SetFactory(GlobalVariable.gsAssyDefaultFactory);
            cdvFactory.Text = GlobalVariable.gsAssyDefaultFactory;
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

            if (cdvVersion.Text.TrimEnd() == "")
            {
                CmnFunction.ShowMsgBox(RptMessages.GetMessage("STD063", GlobalVariable.gcLanguage));
                return false;
            }

            return true;
        }

        /// <summary>
        /// 2. 헤더 생성
        /// </summary>
        private void GridColumnInit()
        {
            GetWorkDay();
            spdData.RPT_ColumnInit();           

            try
            {                
                spdData.RPT_AddBasicColumn("CUSTOMER", 0, 0, Visibles.True, Frozen.True, Align.Center, Merge.True, Formatter.String, 80);
                spdData.RPT_AddBasicColumn("MAJOR CODE", 0, 1, Visibles.False, Frozen.True, Align.Left, Merge.True, Formatter.String, 70);
                spdData.RPT_AddBasicColumn("PACKAGE", 0, 2, Visibles.True, Frozen.True, Align.Center, Merge.True, Formatter.String, 80);
                spdData.RPT_AddBasicColumn("PKG CODE", 0, 3, Visibles.True, Frozen.True, Align.Center, Merge.True, Formatter.String, 70);
                spdData.RPT_AddBasicColumn("TYPE2", 0, 4, Visibles.True, Frozen.True, Align.Center, Merge.True, Formatter.String, 50);
                spdData.RPT_AddBasicColumn("LD COUNT", 0, 5, Visibles.True, Frozen.True, Align.Center, Merge.True, Formatter.String, 70);
                spdData.RPT_AddBasicColumn("PIN TYPE", 0, 6, Visibles.False, Frozen.True, Align.Left, Merge.True, Formatter.String, 120);
                spdData.RPT_AddBasicColumn("PRODUCT", 0, 7, Visibles.True, Frozen.True, Align.Left, Merge.True, Formatter.String, 120);
                spdData.RPT_AddBasicColumn("STEP", 0, 8, Visibles.True, Frozen.True, Align.Center, Merge.True, Formatter.String, 50);
                spdData.RPT_AddBasicColumn("SO", 0, 9, Visibles.True, Frozen.True, Align.Right, Merge.False, Formatter.Number, 80);
                spdData.RPT_AddBasicColumn("WIP", 0, 10, Visibles.True, Frozen.True, Align.Right, Merge.False, Formatter.Number, 80);

                spdData.RPT_AddBasicColumn("FIX PLAN", 0, 11, Visibles.True, Frozen.False, Align.Center, Merge.False, Formatter.Number, 70);
                spdData.RPT_AddBasicColumn("DO", 1, 11, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 70);
                spdData.RPT_AddBasicColumn("Origin", 2, 11, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 70);
                spdData.RPT_AddBasicColumn("Rev", 2, 12, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 70);
                spdData.RPT_AddBasicColumn("Actual", 2, 13, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 70);
                spdData.RPT_AddBasicColumn("Gap", 2, 14, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 70);                
                spdData.RPT_MerageHeaderColumnSpan(1, 11, 4);

                spdData.RPT_AddBasicColumn("D1", 1, 15, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 70);
                spdData.RPT_AddBasicColumn("D2", 1, 16, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 70);
                spdData.RPT_MerageHeaderRowSpan(1, 15, 2);
                spdData.RPT_MerageHeaderRowSpan(1, 16, 2);
                spdData.RPT_MerageHeaderColumnSpan(0, 11, 6);

                spdData.RPT_AddBasicColumn("Guide Plan [Step Target]", 0, 17, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 70);
                spdData.RPT_AddBasicColumn("D0", 1, 17, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 70);
                spdData.RPT_AddBasicColumn("D1", 1, 18, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 70);
                spdData.RPT_AddBasicColumn("D2", 1, 19, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 70);
                spdData.RPT_AddBasicColumn("D3", 1, 20, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 70);
                spdData.RPT_MerageHeaderColumnSpan(0, 17, 4);
                spdData.RPT_MerageHeaderRowSpan(1, 17, 2);
                spdData.RPT_MerageHeaderRowSpan(1, 18, 2);
                spdData.RPT_MerageHeaderRowSpan(1, 19, 2);
                spdData.RPT_MerageHeaderRowSpan(1, 20, 2);

                for (int i = 0; i <= 10; i++)
                {
                    spdData.RPT_MerageHeaderRowSpan(0, i, 3);
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
            ((udcTableForm)(this.btnSort.BindingForm)).AddRow("CUSTOMER", "MAT.MAT_GRP_1", "DECODE(MAT.MAT_GRP_1, 'SE', 1, 'HX', 2, 3), MAT.MAT_GRP_1", "(SELECT DATA_1 FROM MGCMTBLDAT WHERE TABLE_NAME = 'H_CUSTOMER' AND KEY_1 = MAT.MAT_GRP_1 AND ROWNUM=1) AS CUSTOMER", "CUSTOMER", true);
            ((udcTableForm)(this.btnSort.BindingForm)).AddRow("MAJOR CODE", "MAT.MAT_GRP_9", "MAT.MAT_GRP_9", "MAT.MAT_GRP_9", "MAT_GRP_9", true);
            ((udcTableForm)(this.btnSort.BindingForm)).AddRow("PACKAGE", "MAT.MAT_GRP_10", "MAT.MAT_GRP_10", "MAT.MAT_GRP_10", "MAT_GRP_10", true);
            ((udcTableForm)(this.btnSort.BindingForm)).AddRow("PKG CODE", "MAT.MAT_CMF_11", "MAT.MAT_CMF_11", "MAT.MAT_CMF_11", "MAT_CMF_11", true);
            ((udcTableForm)(this.btnSort.BindingForm)).AddRow("TYPE2", "MAT.MAT_GRP_5", "MAT.MAT_GRP_5", "MAT.MAT_GRP_5", "MAT_GRP_5", true);
            ((udcTableForm)(this.btnSort.BindingForm)).AddRow("LD COUNT", "MAT.MAT_GRP_6", "MAT.MAT_GRP_6", "MAT.MAT_GRP_6", "MAT_GRP_6", false);
            ((udcTableForm)(this.btnSort.BindingForm)).AddRow("PIN TYPE", "MAT.MAT_CMF_10", "MAT.MAT_CMF_10", "MAT.MAT_CMF_10", "MAT_CMF_10", false);
            ((udcTableForm)(this.btnSort.BindingForm)).AddRow("PRODUCT", "MAT.MAT_ID", "MAT.MAT_ID", "MAT.MAT_ID", "MAT_ID", true);          
        }
        #endregion


        #region 시간관련 함수
        private void GetWorkDay()
        {
            DateTime Now = cdvDate.Value;            

            for (int i = 0; i < 3; i++)
            {
                DateArray[i] = Now.ToString("yyyyMMdd");                
                Now = Now.AddDays(1);
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
            FindWeek = CmnFunction.GetWeekInfo(cdvDate.SelectedValue(), "OTD");

            string QueryCond1;
            string QueryCond2;
            string QueryCond3;
            //string QueryCond4;
            string Yesterday;            
            string sD0;
            string sD1;
            string sD2;
            string sTime;            
            string sKpcsValue;         // Kpcs 구분에 의한 나누기 분모 값            
            string sD;

            sD = cdvDate.Value.AddDays(-6).ToString("yyyyMMdd");
            sD0 = cdvDate.Value.ToString("yyyyMMdd");            
            sD1 = cdvDate.Value.AddDays(1).ToString("yyyyMMdd");
            sD2 = cdvDate.Value.AddDays(2).ToString("yyyyMMdd");
            Yesterday = cdvDate.Value.AddDays(-1).ToString("yyyyMMdd");            

            udcTableForm tableForm = (udcTableForm)btnSort.BindingForm;
            QueryCond1 = tableForm.SelectedValueToQueryContainNull;
            QueryCond2 = tableForm.SelectedValue2ToQueryContainNull;
            QueryCond3 = tableForm.SelectedValue3ToQueryContainNull;
            //QueryCond4 = tableForm.SelectedValue4ToQueryContainNull;

            // kpcs 선택에 의한 분모 값 저장 한다.
            if (ckbKpcs.Checked == true)
            {
                sKpcsValue = "1000";
            }
            else
            {
                sKpcsValue = "1";
            }

            sTime = cdvVersion.Text.Substring(18, 2);

            //strSqlString.Append("     , ROUND(SUM(NVL(SHP_TODAY, 0)) / " + sKpcsValue + ", 0) AS SHP_TODAY" + "\n");
            //strSqlString.Append("     , ROUND(SUM(NVL(WIP.HMK3A, 0)) / " + sKpcsValue + ", 0) AS HMK3A" + "\n");
            //strSqlString.Append("     , ROUND(SUM(NVL(WIP.QC_GATE, 0)) / " + sKpcsValue + ", 0) AS QC_GATE" + "\n");
            //strSqlString.Append("     , ROUND(SUM(NVL(WIP.PVI, 0)) / " + sKpcsValue + ", 0) AS PVI" + "\n");
            //strSqlString.Append("     , ROUND(SUM(NVL(WIP.BAKE,0)) / " + sKpcsValue + ", 0) AS BAKE" + "\n");

            if (ckbWF.Checked == true)
            {
                strSqlString.Append("SELECT " + QueryCond3 + ", MAT.OPER" + "\n");
                strSqlString.Append("     , ROUND(SUM(NVL(SLP.SO_QTY, 0) / WF_NET_DIE), 0) AS SO_QTY" + "\n");
                strSqlString.Append("     , ROUND(SUM(NVL(WIP.WIP_QTY, 0) / WF_NET_DIE), 0) AS WIP_QTY" + "\n");
                strSqlString.Append("     , ROUND(SUM(NVL(TAR_Y.Y_STEP_TARGET, 0) / WF_NET_DIE), 0) AS ORIGIN_PLAN" + "\n");
                strSqlString.Append("     , ROUND(SUM((NVL(SHP.LAST_END_QTY, 0) + NVL(MOV_T.D0_STEP_MOVE, 0)) / WF_NET_DIE), 0) AS REV_PLAN" + "\n");
                strSqlString.Append("     , ROUND(SUM(NVL(SHP.END_QTY, 0) / WF_NET_DIE), 0) AS END_QTY" + "\n");
                strSqlString.Append("     , ROUND(SUM((NVL(SHP.END_QTY, 0) - (NVL(SHP.LAST_END_QTY, 0) + NVL(MOV_T.D0_STEP_MOVE, 0))) / WF_NET_DIE), 0) AS GAP" + "\n");
                strSqlString.Append("     , ROUND(SUM(NVL(MOV_T.D1_STEP_MOVE, 0) / WF_NET_DIE), 0) AS D1_STEP_MOVE" + "\n");
                strSqlString.Append("     , ROUND(SUM(NVL(MOV_T.D2_STEP_MOVE, 0) / WF_NET_DIE), 0) AS D2_STEP_MOVE" + "\n");
                //strSqlString.Append("     , ROUND(SUM((NVL(TAR.D0_STEP_TARGET, 0) + NVL(SHP.END_QTY, 0)) / WF_NET_DIE), 0) AS D0_STEP_TARGET" + "\n");
                strSqlString.Append("     , ROUND(SUM(NVL(TAR_Y.Y_STEP_TARGET, 0) / WF_NET_DIE), 0) AS D0_STEP_TARGET" + "\n");
                strSqlString.Append("     , ROUND(SUM(NVL(TAR.D1_STEP_TARGET, 0) / WF_NET_DIE), 0) AS D1_STEP_TARGET" + "\n");
                strSqlString.Append("     , ROUND(SUM(NVL(TAR.D2_STEP_TARGET, 0) / WF_NET_DIE), 0) AS D2_STEP_TARGET" + "\n");
            }
            else
            {
                strSqlString.Append("SELECT " + QueryCond3 + ", MAT.OPER" + "\n");
                strSqlString.Append("     , ROUND(SUM(NVL(SLP.SO_QTY, 0)) / " + sKpcsValue + ", 0) AS SO_QTY" + "\n");
                strSqlString.Append("     , ROUND(SUM(NVL(WIP.WIP_QTY, 0)) / " + sKpcsValue + ", 0) AS WIP_QTY" + "\n");
                strSqlString.Append("     , ROUND(SUM(NVL(TAR_Y.Y_STEP_TARGET, 0)) / " + sKpcsValue + ", 0) AS ORIGIN_PLAN" + "\n");
                strSqlString.Append("     , ROUND(SUM(NVL(SHP.LAST_END_QTY, 0) + NVL(MOV_T.D0_STEP_MOVE, 0)) / " + sKpcsValue + ", 0) AS REV_PLAN" + "\n");
                strSqlString.Append("     , ROUND(SUM(NVL(SHP.END_QTY, 0)) / " + sKpcsValue + ", 0) AS END_QTY" + "\n");
                strSqlString.Append("     , ROUND(SUM(NVL(SHP.END_QTY, 0) - (NVL(SHP.LAST_END_QTY, 0) + NVL(MOV_T.D0_STEP_MOVE, 0))) / " + sKpcsValue + ", 0) AS GAP" + "\n");
                strSqlString.Append("     , ROUND(SUM(NVL(MOV_T.D1_STEP_MOVE, 0)) / " + sKpcsValue + ", 0) AS D1_STEP_MOVE" + "\n");
                strSqlString.Append("     , ROUND(SUM(NVL(MOV_T.D2_STEP_MOVE, 0)) / " + sKpcsValue + ", 0) AS D2_STEP_MOVE" + "\n");
                //strSqlString.Append("     , ROUND(SUM(NVL(TAR.D0_STEP_TARGET, 0) + NVL(SHP.END_QTY, 0)) / " + sKpcsValue + ", 0) AS D0_STEP_TARGET" + "\n");
                strSqlString.Append("     , ROUND(SUM(NVL(TAR_Y.Y_STEP_TARGET, 0)) / " + sKpcsValue + ", 0) AS D0_STEP_TARGET" + "\n");
                strSqlString.Append("     , ROUND(SUM(NVL(TAR.D1_STEP_TARGET, 0)) / " + sKpcsValue + ", 0) AS D1_STEP_TARGET" + "\n");
                strSqlString.Append("     , ROUND(SUM(NVL(TAR.D2_STEP_TARGET, 0)) / " + sKpcsValue + ", 0) AS D2_STEP_TARGET" + "\n");
            }

            strSqlString.Append("  FROM (" + "\n");
            strSqlString.Append("        SELECT A.*, B.OPER" + "\n");
            strSqlString.Append("          FROM VWIPMATDEF A" + "\n");
            strSqlString.Append("             , MWIPFLWOPR@RPTTOMES B" + "\n");
            strSqlString.Append("         WHERE 1=1" + "\n");
            strSqlString.Append("           AND A.FACTORY = B.FACTORY" + "\n");
            strSqlString.Append("           AND A.FIRST_FLOW = B.FLOW" + "\n");
            strSqlString.Append("           AND A.FACTORY = '" + cdvFactory.Text + "'" + "\n");
            strSqlString.Append("           AND A.DELETE_FLAG = ' '" + "\n");
            strSqlString.Append("           AND A.MAT_TYPE = 'FG'" + "\n");

            #region 상세 조회에 따른 SQL문 생성
            if (udcWIPCondition1.Text != "ALL" && udcWIPCondition1.Text != "")
                strSqlString.AppendFormat("           AND A.MAT_GRP_1 {0}" + "\n", udcWIPCondition1.SelectedValueToQueryString);

            if (udcWIPCondition2.Text != "ALL" && udcWIPCondition2.Text != "")
                strSqlString.AppendFormat("           AND A.MAT_GRP_2 {0} " + "\n", udcWIPCondition2.SelectedValueToQueryString);

            if (udcWIPCondition3.Text != "ALL" && udcWIPCondition3.Text != "")
                strSqlString.AppendFormat("           AND A.MAT_GRP_3 {0} " + "\n", udcWIPCondition3.SelectedValueToQueryString);

            if (udcWIPCondition4.Text != "ALL" && udcWIPCondition4.Text != "")
                strSqlString.AppendFormat("           AND A.MAT_GRP_4 {0} " + "\n", udcWIPCondition4.SelectedValueToQueryString);

            if (udcWIPCondition5.Text != "ALL" && udcWIPCondition5.Text != "")
                strSqlString.AppendFormat("           AND A.MAT_GRP_5 {0} " + "\n", udcWIPCondition5.SelectedValueToQueryString);

            if (udcWIPCondition6.Text != "ALL" && udcWIPCondition6.Text != "")
                strSqlString.AppendFormat("           AND A.MAT_GRP_6 {0} " + "\n", udcWIPCondition6.SelectedValueToQueryString);

            if (udcWIPCondition7.Text != "ALL" && udcWIPCondition7.Text != "")
                strSqlString.AppendFormat("           AND A.MAT_GRP_7 {0} " + "\n", udcWIPCondition7.SelectedValueToQueryString);

            if (udcWIPCondition8.Text != "ALL" && udcWIPCondition8.Text != "")
                strSqlString.AppendFormat("           AND A.MAT_GRP_8 {0} " + "\n", udcWIPCondition8.SelectedValueToQueryString);

            if (udcWIPCondition9.Text != "ALL" && udcWIPCondition9.Text != "")
                strSqlString.AppendFormat("           AND A.MAT_GRP_9 {0} " + "\n", udcWIPCondition9.SelectedValueToQueryString);
            #endregion

            strSqlString.Append("           AND A.MAT_ID LIKE '" + txtProduct.Text + "'" + "\n");
            strSqlString.Append("       ) MAT" + "\n");
            strSqlString.Append("     , (" + "\n");
            strSqlString.Append("        SELECT MAT_ID, OPER   " + "\n");
            strSqlString.Append("             , SUM(QTY_1) AS WIP_QTY" + "\n");

            // 금일조회 기준
            if (DateTime.Now.ToString("yyyyMMdd") == sD0)
            {
                strSqlString.Append("          FROM RWIPLOTSTS " + "\n");
                strSqlString.Append("         WHERE 1=1 " + "\n");
            }
            else
            {
                strSqlString.Append("          FROM RWIPLOTSTS_BOH " + "\n");
                strSqlString.Append("         WHERE CUTOFF_DT = '" + sD0 + "22' " + "\n");                
            }

            strSqlString.Append("           AND FACTORY = '" + cdvFactory.Text + "'" + "\n");
            strSqlString.Append("           AND LOT_DEL_FLAG = ' '" + "\n");
            strSqlString.Append("           AND LOT_TYPE = 'W'" + "\n");

            if (cdvType.Text != "ALL")
            {
                strSqlString.Append("           AND LOT_CMF_5 LIKE '" + cdvType.Text + "' " + "\n");
            }

            strSqlString.Append("         GROUP BY MAT_ID, OPER" + "\n");
            strSqlString.Append("       ) WIP" + "\n");
            strSqlString.Append("     , (       " + "\n");
            strSqlString.Append("        SELECT MAT_ID, OPER" + "\n");
            strSqlString.Append("             , SUM(LAST_END_QTY) AS LAST_END_QTY" + "\n");
            strSqlString.Append("             , SUM(END_QTY) AS END_QTY" + "\n");
            strSqlString.Append("          FROM (" + "\n");
            strSqlString.Append("                SELECT MAT_ID, OPER   " + "\n");
            strSqlString.Append("                     , (S1_END_QTY_1+S2_END_QTY_1+S3_END_QTY_1) AS LAST_END_QTY" + "\n");
            strSqlString.Append("                     , 0 AS END_QTY" + "\n");
            strSqlString.Append("                  FROM RSUMWIPMOV_BOH" + "\n");
            strSqlString.Append("                 WHERE 1=1" + "\n");
            strSqlString.Append("                   AND CM_KEY_1 = '" + cdvFactory.Text + "'" + "\n");
            strSqlString.Append("                   AND WORK_DATE = '" + sD0 + "'" + "\n");
            strSqlString.Append("                   AND WORK_TIME = '" + sTime + "'" + "\n");
            strSqlString.Append("                   AND LOT_TYPE = 'W'" + "\n");

            if (cdvType.Text != "ALL")
            {
                strSqlString.Append("                   AND CM_KEY_3 LIKE '" + cdvType.Text + "' " + "\n");
            }

            strSqlString.Append("                 UNION ALL" + "\n");
            strSqlString.Append("                SELECT MAT_ID, OPER" + "\n");
            strSqlString.Append("                     , 0 AS LAST_END_QTY" + "\n");
            strSqlString.Append("                     , (S1_END_QTY_1+S2_END_QTY_1+S3_END_QTY_1) AS END_QTY" + "\n");
            strSqlString.Append("                  FROM RSUMWIPMOV" + "\n");
            strSqlString.Append("                 WHERE 1=1" + "\n");
            strSqlString.Append("                   AND CM_KEY_1 = '" + cdvFactory.Text + "'" + "\n");
            strSqlString.Append("                   AND WORK_DATE = '" + sD0 + "'" + "\n");
            strSqlString.Append("                   AND LOT_TYPE = 'W'" + "\n");

            if (cdvType.Text != "ALL")
            {
                strSqlString.Append("                   AND CM_KEY_3 LIKE '" + cdvType.Text + "' " + "\n");

            }
            strSqlString.Append("               )" + "\n");
            strSqlString.Append("         WHERE OPER NOT IN (' ', 'AZ010', 'T000N')" + "\n");
            strSqlString.Append("         GROUP BY MAT_ID, OPER" + "\n");
            strSqlString.Append("       ) SHP" + "\n");
            //strSqlString.Append("     , (     " + "\n");
            //strSqlString.Append("        SELECT MAT_ID, OPER, SUM(OUT_UNIT_QTY) AS Y_STEP_MOVE" + "\n");
            //strSqlString.Append("          FROM SCHMGR.SOUTOPRMOV@RPTTOMES" + "\n");
            //strSqlString.Append("         WHERE 1=1" + "\n");
            //strSqlString.Append("           AND VERSION_HR = '" + Yesterday + "22'" + "\n");
            //strSqlString.Append("           AND WORK_DATE = '" + sD0 + "'" + "\n");
            //strSqlString.Append("         GROUP BY MAT_ID, OPER" + "\n");
            //strSqlString.Append("       ) MOV_Y" + "\n");
            strSqlString.Append("     , (" + "\n");
            strSqlString.Append("        SELECT MAT_ID, OPER" + "\n");
            strSqlString.Append("             , SUM(DECODE(WORK_DATE, '" + sD0 + "', OUT_UNIT_QTY, 0)) AS D0_STEP_MOVE" + "\n");
            strSqlString.Append("             , SUM(DECODE(WORK_DATE, '" + sD1 + "', OUT_UNIT_QTY, 0)) AS D1_STEP_MOVE" + "\n");
            strSqlString.Append("             , SUM(DECODE(WORK_DATE, '" + sD2 + "', OUT_UNIT_QTY, 0)) AS D2_STEP_MOVE" + "\n");
            strSqlString.Append("          FROM SCHMGR.SOUTOPRMOV@RPTTOMES" + "\n");
            strSqlString.Append("         WHERE 1=1" + "\n");
            strSqlString.Append("           AND VERSION = '" + cdvVersion.Text + "'" + "\n");
            strSqlString.Append("         GROUP BY MAT_ID, OPER" + "\n");
            strSqlString.Append("       ) MOV_T  " + "\n");
            strSqlString.Append("     , (" + "\n");
            strSqlString.Append("        SELECT MAT_ID, OPER" + "\n");
            strSqlString.Append("             , SUM(DECODE(TARGET_DATE, '" + sD0 + "', OUT_TARGET_QTY, 0)) AS D0_STEP_TARGET" + "\n");
            strSqlString.Append("             , SUM(DECODE(TARGET_DATE, '" + sD1 + "', OUT_TARGET_QTY, 0)) AS D1_STEP_TARGET" + "\n");
            strSqlString.Append("             , SUM(DECODE(TARGET_DATE, '" + sD2 + "', OUT_TARGET_QTY, 0)) AS D2_STEP_TARGET" + "\n");
            strSqlString.Append("          FROM SCHMGR.SOUTSTPTGT@RPTTOMES" + "\n");
            strSqlString.Append("         WHERE 1=1" + "\n");
            strSqlString.Append("           AND VERSION = '" + cdvVersion.Text + "'" + "\n");
            strSqlString.Append("           AND FACTORY = '" + cdvFactory.Text + "'" + "\n");
            
            if (cdvWeek.Text == "W0")
            {
                strSqlString.Append("           AND PLAN_WEEK = '" + FindWeek.ThisWeek + "'" + "\n");
            }
            else if (cdvWeek.Text == "W1")
            {
                strSqlString.Append("           AND PLAN_WEEK IN ('" + FindWeek.ThisWeek + "','" + FindWeek.NextWeek + "')" + "\n");
            }
            else
            {                
                strSqlString.Append("           AND PLAN_WEEK IN ('" + FindWeek.ThisWeek + "','" + FindWeek.NextWeek + "','" + FindWeek.W2_Week + "')" + "\n");
            }

            strSqlString.Append("         GROUP BY MAT_ID, OPER" + "\n");
            strSqlString.Append("       ) TAR" + "\n");
            strSqlString.Append("     , (" + "\n");
            strSqlString.Append("        SELECT MAT_ID, OPER" + "\n");
            strSqlString.Append("             , SUM(OUT_TARGET_QTY) AS Y_STEP_TARGET" + "\n");            
            strSqlString.Append("          FROM SCHMGR.SOUTSTPTGT@RPTTOMES" + "\n");
            strSqlString.Append("         WHERE 1=1" + "\n");
            strSqlString.Append("           AND VERSION_HR = '" + Yesterday + "22'" + "\n");
            strSqlString.Append("           AND TARGET_DATE BETWEEN '" + sD + "' AND '" + sD0 + "'" + "\n");
            strSqlString.Append("           AND FACTORY = '" + cdvFactory.Text + "'" + "\n");

            if (cdvWeek.Text == "W0")
            {
                strSqlString.Append("           AND PLAN_WEEK = '" + FindWeek.ThisWeek + "'" + "\n");
            }
            else if (cdvWeek.Text == "W1")
            {
                strSqlString.Append("           AND PLAN_WEEK IN ('" + FindWeek.ThisWeek + "','" + FindWeek.NextWeek + "')" + "\n");
            }
            else
            {
                strSqlString.Append("           AND PLAN_WEEK IN ('" + FindWeek.ThisWeek + "','" + FindWeek.NextWeek + "','" + FindWeek.W2_Week + "')" + "\n");
            }

            strSqlString.Append("         GROUP BY MAT_ID, OPER" + "\n");
            strSqlString.Append("       ) TAR_Y" + "\n");
            strSqlString.Append("     , (" + "\n");
            strSqlString.Append("        SELECT PROD_CODE AS MAT_ID, 'A0000' AS OPER" + "\n");
            strSqlString.Append("             , SUM(CHIP_QTY) AS SO_QTY" + "\n");
            strSqlString.Append("          FROM CWIPSECSLP@RPTTOMES" + "\n");
            strSqlString.Append("         WHERE 1=1" + "\n");
            strSqlString.Append("           AND RECV_FLAG = ' '" + "\n");
            strSqlString.Append("           AND TO_AREA IN ('HMK2')" + "\n");
            strSqlString.Append("           AND FACTORY = '" + GlobalVariable.gsAssyDefaultFactory + "'" + "\n");
            strSqlString.Append("           AND ISSUE_YMD >= '" + Yesterday + "'" + "\n");
            strSqlString.Append("         GROUP BY PROD_CODE" + "\n");
            strSqlString.Append("         UNION ALL" + "\n");
            strSqlString.Append("        SELECT DEVICE AS MAT_ID, 'A0000' AS OPER" + "\n");
            strSqlString.Append("             , SUM(DIE_QTY) AS SO_QTY " + "\n");
            strSqlString.Append("          FROM CWIPHSOHIS@RPTTOMES" + "\n");
            strSqlString.Append("         WHERE 1=1" + "\n");
            strSqlString.Append("           AND RECV_FLAG = ' '" + "\n");
            strSqlString.Append("           AND WORK_DATE >= '" + Yesterday + "'" + "\n");
            strSqlString.Append("         GROUP BY DEVICE" + "\n");
            strSqlString.Append("       ) SLP" + "\n");
            strSqlString.Append(" WHERE 1=1" + "\n");
            strSqlString.Append("   AND MAT.MAT_ID = WIP.MAT_ID(+)" + "\n");
            strSqlString.Append("   AND MAT.MAT_ID = SHP.MAT_ID(+)" + "\n");
            //strSqlString.Append("   AND MAT.MAT_ID = MOV_Y.MAT_ID(+)" + "\n");
            strSqlString.Append("   AND MAT.MAT_ID = MOV_T.MAT_ID(+)" + "\n");
            strSqlString.Append("   AND MAT.MAT_ID = TAR.MAT_ID(+)" + "\n");
            strSqlString.Append("   AND MAT.MAT_ID = TAR_Y.MAT_ID(+)" + "\n");
            strSqlString.Append("   AND MAT.MAT_ID = SLP.MAT_ID(+)" + "\n");
            strSqlString.Append("   AND MAT.OPER = SHP.OPER(+)" + "\n");
            strSqlString.Append("   AND MAT.OPER = WIP.OPER(+)" + "\n");
            //strSqlString.Append("   AND MAT.OPER = MOV_Y.OPER(+)" + "\n");
            strSqlString.Append("   AND MAT.OPER = MOV_T.OPER(+)" + "\n");
            strSqlString.Append("   AND MAT.OPER = TAR.OPER(+)" + "\n");
            strSqlString.Append("   AND MAT.OPER = TAR_Y.OPER(+)" + "\n");
            strSqlString.Append("   AND MAT.OPER = SLP.OPER(+)" + "\n");
            strSqlString.Append("   AND MAT.FACTORY = '" + cdvFactory.Text + "'" + "\n");
            strSqlString.Append("   AND MAT.DELETE_FLAG = ' '" + "\n");
            strSqlString.Append("   AND MAT.MAT_TYPE = 'FG'" + "\n");

            if (cdvOper.Text != "ALL" && cdvOper.Text != "")
            {
                strSqlString.Append("   AND MAT.OPER " + cdvOper.SelectedValueToQueryString + "\n");
            }

            strSqlString.Append("   AND NVL(WIP.WIP_QTY,0) + NVL(SHP.LAST_END_QTY,0) + NVL(SHP.END_QTY,0) +" + "\n");
            strSqlString.Append("       NVL(MOV_T.D0_STEP_MOVE,0) + NVL(MOV_T.D1_STEP_MOVE,0) + NVL(MOV_T.D2_STEP_MOVE,0) +" + "\n");
            strSqlString.Append("       NVL(TAR.D0_STEP_TARGET,0) + NVL(TAR.D1_STEP_TARGET,0) + NVL(TAR.D2_STEP_TARGET,0) +" + "\n");
            strSqlString.Append("       NVL(TAR_Y.Y_STEP_TARGET,0) + NVL(SLP.SO_QTY,0) > 0" + "\n");
            strSqlString.AppendFormat(" GROUP BY {0}, MAT.OPER " + "\n", QueryCond1);

            if (ckbPlan.Checked == true)
            {
                strSqlString.Append("HAVING SUM(NVL(SHP.LAST_END_QTY, 0) + NVL(MOV_T.D0_STEP_MOVE, 0)) > 0" + "\n");
            }

            strSqlString.AppendFormat(" ORDER BY {0}, MAT.OPER " + "\n", QueryCond2);

         

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
                int sub = ((udcTableForm)(this.btnSort.BindingForm)).GetCheckedValue(2);

                int[] rowType = spdData.RPT_DataBindingWithSubTotal(dt, 0, sub + 1, 9, null, null, btnSort);

                //Total부분 셀머지
                spdData.RPT_FillDataSelectiveCells("Total", 0, 9, 0, 1, true, Align.Center, VerticalAlign.Center);

                //spdData.DataSource = dt;
                spdData.RPT_AutoFit(false);               
                
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
            if (spdData.ActiveSheet.Rows.Count > 0)
            {
                StringBuilder Condition = new StringBuilder();                

                ExcelHelper.Instance.subMakeExcel(spdData, this.lblTitle.Text, Condition.ToString(), null, true);
                //(데이타, 제목(타이틀), 좌측문구, 우측문구, 자동사이즈조정)

            }
            // Excel 바로 보이기
            //ExcelHelper.Instance.subMakeExcel(spdData, this.lblTitle.Text, " ^ ", " ^ ", true);
            //spdData.ExportExcel();            
        }

        private void cdvFactory_ValueSelectedItemChanged(object sender, Miracom.UI.MCCodeViewSelChanged_EventArgs e)
        {
            this.SetFactory(cdvFactory.txtValue);
        }
        #endregion

        private void cdvVersion_ValueButtonPress(object sender, EventArgs e)
        {
            string strQuery = string.Empty;
            string sFrom = cdvDate.Value.AddDays(-1).ToString("yyyyMMdd") + "22";
            string sTo = cdvDate.Value.ToString("yyyyMMdd") + "21";

            strQuery += "SELECT DISTINCT VERSION AS Code, '' AS Data" + "\n";
            strQuery += "  FROM SCHMGR.SOUTOPRMOV@RPTTOMES " + "\n";
            strQuery += " WHERE 1=1 " + "\n";
            strQuery += "   AND VERSION_HR BETWEEN '" + sFrom + "' AND '" + sTo + "'" + "\n";
            strQuery += " ORDER BY VERSION DESC" + "\n";            

            cdvVersion.sDynamicQuery = strQuery;
        }

        private void cdvOper_ValueButtonPress(object sender, EventArgs e)
        {
            string strQuery = string.Empty;

            strQuery += "SELECT DISTINCT OPER AS Code, OPER_DESC AS Data " + "\n";
            strQuery += "  FROM MWIPOPRDEF " + "\n";
            strQuery += " WHERE FACTORY = '" + cdvFactory.Text + "' " + "\n";
            strQuery += "   AND OPER_CMF_5 = ' '" + "\n";
            strQuery += "   AND OPER LIKE 'A%'" + "\n";
            strQuery += "   AND OPER <> 'A000N'" + "\n";
            strQuery += " ORDER BY OPER " + "\n";

            cdvOper.sDynamicQuery = strQuery;
        }

        #region MakeSqlCustomer
        //고객사 명으로 고객사 코드 가져오기 위해..
        private string MakeSqlCustomer(string customerName)
        {
            StringBuilder strSqlString = new StringBuilder();

            strSqlString.AppendFormat("SELECT KEY_1 FROM MGCMTBLDAT WHERE TABLE_NAME = 'H_CUSTOMER' AND DATA_1 = '" + customerName + "' AND ROWNUM=1" + "\n");

            return strSqlString.ToString();

        }
        #endregion

        private void spdData_CellClick(object sender, FarPoint.Win.Spread.CellClickEventArgs e)
        {
            string[] dataArry = new string[8];
            string sOper = null;


            // WIP 클릭 시 팝업 창 띄움
            if (e.Column == 10 && spdData.ActiveSheet.Cells[e.Row, e.Column].Text != "")
            {
                // Group 정보 가져와서 담기... 상세 팝업에서 조건값으로 사용하기 위해
                for (int i = 0; i <= 7; i++)
                {
                    if (spdData.ActiveSheet.Columns[i].Label == "CUSTOMER")
                        dataArry[0] = spdData.ActiveSheet.Cells[e.Row, i].Value.ToString();

                    else if (spdData.ActiveSheet.Columns[i].Label == "MAJOR CODE")
                        dataArry[1] = spdData.ActiveSheet.Cells[e.Row, i].Value.ToString();

                    else if (spdData.ActiveSheet.Columns[i].Label == "PACKAGE")
                        dataArry[2] = spdData.ActiveSheet.Cells[e.Row, i].Value.ToString();

                    else if (spdData.ActiveSheet.Columns[i].Label == "PKG CODE")
                        dataArry[3] = spdData.ActiveSheet.Cells[e.Row, i].Value.ToString();

                    else if (spdData.ActiveSheet.Columns[i].Label == "TYPE2")
                        dataArry[4] = spdData.ActiveSheet.Cells[e.Row, i].Value.ToString();

                    else if (spdData.ActiveSheet.Columns[i].Label == "LD COUNT")
                        dataArry[5] = spdData.ActiveSheet.Cells[e.Row, i].Value.ToString();

                    else if (spdData.ActiveSheet.Columns[i].Label == "PIN TYPE")
                        dataArry[6] = spdData.ActiveSheet.Cells[e.Row, i].Value.ToString();

                    else if (spdData.ActiveSheet.Columns[i].Label == "PRODUCT")
                        dataArry[7] = spdData.ActiveSheet.Cells[e.Row, i].Value.ToString();
                   
                }

                // 고객사 명을 고객사 코드로 변경하기 위해..
                if (dataArry[0] != " " && dataArry[0] != null)
                {
                    DataTable dtCustomer = CmnFunction.oComm.GetFuncDataTable("DYNAMIC", MakeSqlCustomer(dataArry[0].ToString()));

                    dataArry[0] = dtCustomer.Rows[0][0].ToString();
                }

                sOper = spdData.ActiveSheet.Cells[e.Row, 8].Value.ToString();

                
                DataTable dt = CmnFunction.oComm.GetFuncDataTable("DYNAMIC", MakeSqlDetailWip(sOper, dataArry));

                if (dt != null && dt.Rows.Count > 0)
                {
                    System.Windows.Forms.Form frm = new PRD010911_P1("", dt);
                    frm.ShowDialog();
                }
             
            }
            else
            {
                return;
            }
        }

        private string MakeSqlDetailWip(string sOper, string[] dataArry)
        {
            StringBuilder strSqlString = new StringBuilder();

            strSqlString.AppendFormat("SELECT B.MAT_CMF_10, B.MAT_ID, A.LOT_CMF_4, A.LOT_ID, A.QTY_1, A.QTY_2" + "\n");
            strSqlString.AppendFormat("     , A.LOT_CMF_5 AS LOT_TYPE" + "\n");
            strSqlString.AppendFormat("     , DECODE(HOLD_FLAG, 'Y', 'HOLD', DECODE(LOT_STATUS, 'PROC', 'RUN', 'WAIT')) AS STATUS" + "\n");
            strSqlString.AppendFormat("     , TRUNC(SYSDATE-TO_DATE(OPER_IN_TIME,'YYYYMMDDHH24MISS')) || '일 ' ||" + "\n");
            strSqlString.AppendFormat("       TRUNC(MOD((SYSDATE-TO_DATE(OPER_IN_TIME,'YYYYMMDDHH24MISS')),1)*24)|| '시간 ' ||" + "\n");
            strSqlString.AppendFormat("       TRUNC(MOD((SYSDATE-TO_DATE(OPER_IN_TIME,'YYYYMMDDHH24MISS'))*24,1)*60)|| '분 '" + "\n");
            strSqlString.AppendFormat("       AS TIME_INTERVAL" + "\n");
            strSqlString.AppendFormat("     , LAST_COMMENT" + "\n");

            if (DateTime.Now.ToString("yyyyMMdd") == cdvDate.Value.ToString("yyyyMMdd"))
            {
                strSqlString.AppendFormat("  FROM RWIPLOTSTS A" + "\n");
                strSqlString.AppendFormat("     , MWIPMATDEF B" + "\n");
                strSqlString.AppendFormat(" WHERE 1=1" + "\n");
            }
            else
            {
                strSqlString.AppendFormat("  FROM RWIPLOTSTS_BOH A" + "\n");
                strSqlString.AppendFormat("     , MWIPMATDEF B" + "\n");
                strSqlString.AppendFormat(" WHERE CUTOFF_DT = '" + cdvDate.Value.ToString("yyyyMMdd") + "22' " + "\n");                
            }

            strSqlString.AppendFormat("   AND A.FACTORY = B.FACTORY" + "\n");
            strSqlString.AppendFormat("   AND A.MAT_ID = B.MAT_ID  " + "\n");
            strSqlString.AppendFormat("   AND A.FACTORY = '" + cdvFactory.Text + "' " + "\n");
            strSqlString.AppendFormat("   AND A.LOT_DEL_FLAG = ' '" + "\n");
            strSqlString.AppendFormat("   AND A.LOT_TYPE = 'W'" + "\n");

            #region 상세 조회에 따른 SQL문 생성
            if (dataArry[0] != " " && dataArry[0] != null)
                strSqlString.AppendFormat("   AND B.MAT_GRP_1 = '" + dataArry[0] + "'" + "\n");

            if (dataArry[1] != " " && dataArry[1] != null)
                strSqlString.AppendFormat("   AND B.MAT_GRP_9 = '" + dataArry[1] + "'" + "\n");

            if (dataArry[2] != " " && dataArry[2] != null)
                strSqlString.AppendFormat("   AND B.MAT_GRP_10 = '" + dataArry[2] + "'" + "\n");

            if (dataArry[3] != " " && dataArry[3] != null)
                strSqlString.AppendFormat("   AND B.MAT_CMF_11 = '" + dataArry[3] + "'" + "\n");

            if (dataArry[4] != " " && dataArry[4] != null)
                strSqlString.AppendFormat("   AND B.MAT_GRP_5 = '" + dataArry[4] + "'" + "\n");

            if (dataArry[5] != " " && dataArry[5] != null)
                strSqlString.AppendFormat("   AND B.MAT_GRP_6 = '" + dataArry[5] + "'" + "\n");

            if (dataArry[6] != " " && dataArry[6] != null)
                strSqlString.AppendFormat("   AND B.MAT_CMF_10= '" + dataArry[6] + "'" + "\n");

            if (dataArry[7] != " " && dataArry[7] != null)
                strSqlString.AppendFormat("   AND B.MAT_ID = '" + dataArry[7] + "'" + "\n");
            #endregion

            strSqlString.AppendFormat("   AND A.OPER = '" + sOper + "'" + "\n");
            strSqlString.AppendFormat(" ORDER BY TIME_INTERVAL DESC, B.MAT_CMF_10, B.MAT_ID, A.LOT_CMF_4, A.LOT_ID" + "\n");
            

            if (GlobalVariable.gsUserGroup == "ADMIN_GROUP" || GlobalVariable.gsUserGroup == "HANA_ADMIN_GROUP")
            {
                System.Windows.Forms.Clipboard.SetText(strSqlString.ToString());
            }

            return strSqlString.ToString();
        }
    }
}
