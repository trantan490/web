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
    public partial class PRD010908 : Miracom.SmartWeb.UI.Controls.udcCUSReport002
    {
        /// <summary>
        /// 클  래  스: PRD010908<br/>
        /// 클래스요약: 하이닉스 투입 관리(임시)<br/>
        /// 작  성  자: 하나마이크론 임종우<br/>
        /// 최초작성일: 2014-06-03<br/>
        /// 상세  설명: 하이닉스 투입 관리(임시)<br/>
        /// 변경  내용: <br/>      
        /// 2014-06-23-임종우 : 실적 06시 기준으로 변경 (임태성K 요청)
        /// </summary>
        GlobalVariable.FindWeek FindWeek_SOP_A = new GlobalVariable.FindWeek();       

        public PRD010908()
        {
            InitializeComponent();
            SortInit();
            cdvDate.Value = DateTime.Now;
            GridColumnInit();
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
                spdData.RPT_AddBasicColumn("Package", 0, 0, Visibles.True, Frozen.True, Align.Center, Merge.True, Formatter.String, 80);
                spdData.RPT_AddBasicColumn("Pin Type", 0, 1, Visibles.True, Frozen.True, Align.Left, Merge.True, Formatter.String, 120);                
                
                spdData.RPT_AddBasicColumn("WW" + FindWeek_SOP_A.ThisWeek.Substring(4), 0, 2, Visibles.True, Frozen.False, Align.Center, Merge.False, Formatter.String, 80);               
                spdData.RPT_AddBasicColumn("plan", 1, 2, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 80);                
                spdData.RPT_AddBasicColumn("actual", 1, 3, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 70);
                spdData.RPT_AddBasicColumn("residual quantity", 1, 4, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 70);
                spdData.RPT_MerageHeaderColumnSpan(0, 2, 3);

                spdData.RPT_AddBasicColumn("PLAN", 0, 5, Visibles.True, Frozen.False, Align.Center, Merge.False, Formatter.String, 70);
                spdData.RPT_AddBasicColumn("D0", 1, 5, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 70);
                spdData.RPT_AddBasicColumn("actual", 1, 6, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 70);
                spdData.RPT_AddBasicColumn("residual quantity", 1, 7, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 70);
                spdData.RPT_AddBasicColumn("D1", 1, 8, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 70);                
                spdData.RPT_MerageHeaderColumnSpan(0, 5, 4);

                spdData.RPT_AddBasicColumn("STOCK", 0, 9, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 70);
                
                spdData.RPT_AddBasicColumn("WIP", 0, 10, Visibles.True, Frozen.False, Align.Center, Merge.False, Formatter.String, 70);
                spdData.RPT_AddBasicColumn("BG", 1, 10, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 70);
                spdData.RPT_AddBasicColumn("SAW", 1, 11, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 70);
                spdData.RPT_AddBasicColumn("DA1", 1, 12, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 70);
                spdData.RPT_AddBasicColumn("TTL", 1, 13, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 70);
                spdData.RPT_MerageHeaderColumnSpan(0, 10, 4);

                spdData.RPT_AddBasicColumn("DA CAPA", 0, 14, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 70);

                spdData.RPT_AddBasicColumn("Input Status", 0, 15, Visibles.True, Frozen.False, Align.Center, Merge.False, Formatter.String, 70);
                spdData.RPT_AddBasicColumn("Proper WIP", 1, 15, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 70);
                spdData.RPT_AddBasicColumn("Acquisition rate", 1, 16, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 70);                                                             
                spdData.RPT_MerageHeaderColumnSpan(0, 15, 2);                
                                
                spdData.RPT_MerageHeaderRowSpan(0, 0, 2);
                spdData.RPT_MerageHeaderRowSpan(0, 1, 2);
                spdData.RPT_MerageHeaderRowSpan(0, 9, 2);
                spdData.RPT_MerageHeaderRowSpan(0, 14, 2);                
                
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
          
        }
        #endregion


        #region 시간관련 함수
        private void GetWorkDay()
        {
            DateTime Now = cdvDate.Value;
            FindWeek_SOP_A = CmnFunction.GetWeekInfo(cdvDate.SelectedValue(), "OTD");                      
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

            //string QueryCond1;
            //string QueryCond2;
            string Today;
            string sWeek;
            string sStartDate;
            string sEndDate;
            string sKpcsValue;         // Kpcs 구분에 의한 나누기 분모 값

            sWeek = FindWeek_SOP_A.ThisWeek;
            Today = cdvDate.Value.ToString("yyyyMMdd");
            sStartDate = FindWeek_SOP_A.StartDay_ThisWeek;
            sEndDate = FindWeek_SOP_A.EndDay_ThisWeek;

            //udcTableForm tableForm = (udcTableForm)btnSort.BindingForm;
            //QueryCond1 = tableForm.SelectedValueToQueryContainNull;
            //QueryCond2 = tableForm.SelectedValue2ToQueryContainNull;

            // kpcs 선택에 의한 분모 값 저장 한다.
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
            strSqlString.Append("SELECT A.MAT_ID, A.MAT_GRP_10, A.MAT_CMF_10, A.VENDOR_ID" + "\n");
            strSqlString.Append("     , B.DATA_3 AS D0, B.DATA_4 AS D1, B.DATA_6 AS WIP_TYPE" + "\n");
            strSqlString.Append("  FROM MWIPMATDEF A" + "\n");
            strSqlString.Append("     , MGCMTBLDAT B" + "\n");
            strSqlString.Append(" WHERE 1=1" + "\n");
            strSqlString.Append("   AND A.FACTORY = B.FACTORY(+)   " + "\n");
            strSqlString.Append("   AND A.MAT_CMF_10 = B.DATA_1(+)" + "\n");
            strSqlString.Append("   AND A.FACTORY = '" + GlobalVariable.gsAssyDefaultFactory + "'" + "\n");
            strSqlString.Append("   AND A.DELETE_FLAG = ' '" + "\n");
            strSqlString.Append("   AND A.MAT_TYPE = 'FG'" + "\n");
            strSqlString.Append("   AND A.MAT_GRP_1 = 'HX'" + "\n");
            strSqlString.Append("   AND A.MAT_GRP_5 IN ('Merge', '-')" + "\n");
            strSqlString.Append("   AND B.TABLE_NAME(+) = 'H_HX_OVER_LOCKING' " + "\n");

            if (txtProduct.Text.Trim() != "%" && txtProduct.Text.Trim() != "")
            {
                strSqlString.AppendFormat("   AND A.MAT_ID LIKE '{0}'" + "\n", txtProduct.Text);
            }

            //상세 조회에 따른 SQL문 생성                                    
            if (udcWIPCondition1.Text != "ALL" && udcWIPCondition1.Text != "")
                strSqlString.AppendFormat("   AND A.MAT_GRP_1 {0}" + "\n", udcWIPCondition1.SelectedValueToQueryString);

            if (udcWIPCondition2.Text != "ALL" && udcWIPCondition2.Text != "")
                strSqlString.AppendFormat("   AND A.MAT_GRP_2 {0} " + "\n", udcWIPCondition2.SelectedValueToQueryString);

            if (udcWIPCondition3.Text != "ALL" && udcWIPCondition3.Text != "")
                strSqlString.AppendFormat("   AND A.MAT_GRP_3 {0} " + "\n", udcWIPCondition3.SelectedValueToQueryString);

            if (udcWIPCondition4.Text != "ALL" && udcWIPCondition4.Text != "")
                strSqlString.AppendFormat("   AND A.MAT_GRP_4 {0} " + "\n", udcWIPCondition4.SelectedValueToQueryString);

            if (udcWIPCondition5.Text != "ALL" && udcWIPCondition5.Text != "")
                strSqlString.AppendFormat("   AND A.MAT_GRP_5 {0} " + "\n", udcWIPCondition5.SelectedValueToQueryString);

            if (udcWIPCondition6.Text != "ALL" && udcWIPCondition6.Text != "")
                strSqlString.AppendFormat("   AND A.MAT_GRP_6 {0} " + "\n", udcWIPCondition6.SelectedValueToQueryString);

            if (udcWIPCondition7.Text != "ALL" && udcWIPCondition7.Text != "")
                strSqlString.AppendFormat("   AND A.MAT_GRP_7 {0} " + "\n", udcWIPCondition7.SelectedValueToQueryString);

            if (udcWIPCondition8.Text != "ALL" && udcWIPCondition8.Text != "")
                strSqlString.AppendFormat("   AND A.MAT_GRP_8 {0} " + "\n", udcWIPCondition8.SelectedValueToQueryString);

            if (udcWIPCondition9.Text != "ALL" && udcWIPCondition9.Text != "")
                strSqlString.AppendFormat("   AND A.MAT_GRP_9 {0} " + "\n", udcWIPCondition9.SelectedValueToQueryString);

            strSqlString.Append(")" + "\n");
            strSqlString.Append("SELECT MAT.MAT_GRP_10" + "\n");
            strSqlString.Append("     , MAT.MAT_CMF_10" + "\n");
            strSqlString.Append("     , ROUND(SUM(PLN.WW_PLN) / " + sKpcsValue + ", 0) AS WW_PLN" + "\n");
            strSqlString.Append("     , ROUND(SUM(NVL(SHP.WW_AO,0) + NVL(WIP.IN_WIP,0)) / " + sKpcsValue + ", 0) AS WW_SHP" + "\n");
            strSqlString.Append("     , ROUND(SUM(NVL(PLN.WW_PLN,0) - (NVL(SHP.WW_AO,0) + NVL(WIP.IN_WIP,0))) / " + sKpcsValue + ", 0) AS WW_DEF" + "\n");
            strSqlString.Append("     , ROUND(MAX(MAT.D0) / " + sKpcsValue + ", 0) AS D0" + "\n");
            strSqlString.Append("     , ROUND(SUM(ISS.ISS_QTY) / " + sKpcsValue + ", 0) AS ISS_QTY" + "\n");
            strSqlString.Append("     , ROUND((MAX(NVL(MAT.D0,0)) - SUM(NVL(ISS.ISS_QTY,0))) / " + sKpcsValue + ", 0) AS D0_DEF" + "\n");
            strSqlString.Append("     , ROUND(MAX(MAT.D1) / " + sKpcsValue + ", 0) AS D1" + "\n");
            strSqlString.Append("     , ROUND(SUM(WIP.STOCK) / " + sKpcsValue + ", 0) AS STOCK" + "\n");
            strSqlString.Append("     , ROUND(SUM(WIP.BG) / " + sKpcsValue + ", 0) AS BG" + "\n");
            strSqlString.Append("     , ROUND(SUM(WIP.SAW) / " + sKpcsValue + ", 0) AS SAW" + "\n");
            strSqlString.Append("     , ROUND(SUM(WIP.DA1) / " + sKpcsValue + ", 0) AS DA1" + "\n");
            strSqlString.Append("     , ROUND(SUM(WIP.IN_WIP) / " + sKpcsValue + ", 0) AS IN_WIP" + "\n");
            strSqlString.Append("     , ROUND(SUM(RES.DA1_CAPA) / " + sKpcsValue + ", 0) AS DA1_CAPA" + "\n");
            strSqlString.Append("     , ROUND(ROUND(MAX(MAT.D0) / 24 * 13, 0) / " + sKpcsValue + ", 0) AS OPT_WIP" + "\n");
            strSqlString.Append("     , CASE WHEN ROUND(MAX(MAT.D0) / 24 * 13, 0) = 0 THEN 0" + "\n");
            strSqlString.Append("            ELSE ROUND(SUM(WIP.IN_WIP) / (MAX(MAT.D0) / 24 * 13) * 100, 2)" + "\n");
            strSqlString.Append("       END AS SECURE_PER" + "\n");
            strSqlString.Append("  FROM DT MAT" + "\n");
            strSqlString.Append("     , (" + "\n");
            strSqlString.Append("        SELECT MAT_ID, SUM(WW_QTY) AS WW_PLN" + "\n");
            strSqlString.Append("          FROM RWIPPLNWEK" + "\n");
            strSqlString.Append("         WHERE FACTORY = '" + GlobalVariable.gsAssyDefaultFactory + "'" + "\n");
            strSqlString.Append("           AND PLAN_WEEK = '" + sWeek + "'" + "\n");
            strSqlString.Append("           AND GUBUN = '3'" + "\n");
            strSqlString.Append("           AND MAT_ID LIKE 'HX%'" + "\n");
            strSqlString.Append("         GROUP BY MAT_ID" + "\n");
            strSqlString.Append("       ) PLN" + "\n");
            strSqlString.Append("     , (" + "\n");
            strSqlString.Append("        SELECT MAT_ID, SUM(S1_FAC_OUT_QTY_1+S2_FAC_OUT_QTY_1+S3_FAC_OUT_QTY_1) AS WW_AO" + "\n");
            strSqlString.Append("          FROM CSUMFACMOV" + "\n");
            strSqlString.Append("         WHERE 1=1" + "\n");
            strSqlString.Append("           AND WORK_DATE BETWEEN '" + sStartDate + "' AND '" + sEndDate + "' " + "\n");
            strSqlString.Append("           AND CM_KEY_1 = '" + GlobalVariable.gsAssyDefaultFactory + "'" + "\n");
            strSqlString.Append("           AND MAT_ID LIKE 'HX%'" + "\n");
            strSqlString.Append("           AND LOT_TYPE = 'W'" + "\n");
            strSqlString.Append("           AND FACTORY NOT IN ('RETURN')" + "\n");
            strSqlString.Append("         GROUP BY MAT_ID" + "\n");
            strSqlString.Append("       ) SHP" + "\n");
            strSqlString.Append("     , (" + "\n");
            strSqlString.Append("        SELECT MAT_ID" + "\n");
            strSqlString.Append("             , ROUND(ISS_QTY / NVL(COMP_CNT,1),0) AS ISS_QTY" + "\n");
            strSqlString.Append("          FROM (" + "\n");
            strSqlString.Append("                SELECT MAT_ID " + "\n");
            strSqlString.Append("                     , SUM(S1_END_QTY_1+S2_END_QTY_1+S3_END_QTY_1+S1_END_RWK_QTY_1 + S2_END_RWK_QTY_1 + S3_END_RWK_QTY_1) AS ISS_QTY" + "\n");
            strSqlString.Append("                     , (SELECT DATA_1 FROM MGCMTBLDAT WHERE FACTORY = '" + GlobalVariable.gsAssyDefaultFactory + "' AND TABLE_NAME = 'H_HX_AUTO_LOSS' AND KEY_1 = A.MAT_ID) AS COMP_CNT" + "\n");
            strSqlString.Append("                  FROM CSUMWIPMOV A" + "\n");
            strSqlString.Append("                 WHERE 1=1 " + "\n");
            strSqlString.Append("                   AND FACTORY  = '" + GlobalVariable.gsAssyDefaultFactory + "' " + "\n");
            strSqlString.Append("                   AND WORK_DATE = '" + Today + "'" + "\n");
            strSqlString.Append("                   AND LOT_TYPE = 'W' " + "\n");
            strSqlString.Append("                   AND OPER = 'A0000' " + "\n");
            strSqlString.Append("                   AND MAT_ID LIKE 'HX%' " + "\n");
            strSqlString.Append("                 GROUP BY MAT_ID " + "\n");
            strSqlString.Append("               )" + "\n");
            strSqlString.Append("         WHERE ISS_QTY > 0" + "\n");
            strSqlString.Append("       ) ISS" + "\n");
            strSqlString.Append("     , (" + "\n");
            strSqlString.Append("        SELECT MAT_ID" + "\n");
            strSqlString.Append("             , SUM(DECODE(OPER_GRP, 'STOCK', WIP_QTY, 0)) AS STOCK" + "\n");
            strSqlString.Append("             , SUM(DECODE(OPER_GRP, 'BG', WIP_QTY, 0)) AS BG" + "\n");
            strSqlString.Append("             , SUM(DECODE(OPER_GRP, 'SAW', WIP_QTY, 0)) AS SAW" + "\n");
            strSqlString.Append("             , SUM(DECODE(OPER_GRP, 'DA1', WIP_QTY, 0)) AS DA1" + "\n");
            strSqlString.Append("             , SUM(CASE WHEN OPER_GRP IN ('BG', 'SAW', 'DA1') THEN WIP_QTY ELSE 0 END) AS IN_WIP" + "\n");            
            strSqlString.Append("          FROM (" + "\n");
            strSqlString.Append("                SELECT MAT_ID" + "\n");
            strSqlString.Append("                     , OPER_GRP" + "\n");
            strSqlString.Append("                     , ROUND(CASE WHEN WIP_TYPE = '2' AND OPER <= 'A0015'THEN WIP_QTY / NVL(COMP_CNT,1)" + "\n");
            strSqlString.Append("                                  WHEN WIP_TYPE = '3' AND OPER <= 'A0395'THEN WIP_QTY / NVL(COMP_CNT,1)" + "\n");
            strSqlString.Append("                                  ELSE WIP_QTY" + "\n");
            strSqlString.Append("                             END,0) WIP_QTY" + "\n");
            strSqlString.Append("                     , WIP_TYPE, COMP_CNT" + "\n");
            strSqlString.Append("                  FROM (" + "\n");
            strSqlString.Append("                        SELECT MAT_ID" + "\n");
            strSqlString.Append("                             , OPER" + "\n");
            strSqlString.Append("                             , CASE WHEN OPER = 'A0000' THEN 'STOCK'" + "\n");
            strSqlString.Append("                                    WHEN OPER <= 'A0040' THEN 'BG'" + "\n");
            strSqlString.Append("                                    WHEN OPER <= 'A0250' THEN 'SAW'" + "\n");
            strSqlString.Append("                                    ELSE 'DA1'" + "\n");
            strSqlString.Append("                               END AS OPER_GRP" + "\n");
            strSqlString.Append("                             , SUM(QTY_1) AS WIP_QTY" + "\n");
            strSqlString.Append("                             , (SELECT DATA_1 FROM MGCMTBLDAT WHERE FACTORY = '" + GlobalVariable.gsAssyDefaultFactory + "' AND TABLE_NAME = 'H_HX_AUTO_LOSS' AND KEY_1 = A.MAT_ID) AS COMP_CNT" + "\n");
            strSqlString.Append("                             , (SELECT WIP_TYPE FROM DT WHERE MAT_ID = A.MAT_ID) AS WIP_TYPE" + "\n");

            if (DateTime.Now.ToString("yyyyMMdd") == Today)
            {
                strSqlString.Append("                          FROM RWIPLOTSTS A  " + "\n");
                strSqlString.Append("                         WHERE 1=1 " + "\n");
            }
            else
            {
                strSqlString.Append("                          FROM RWIPLOTSTS_BOH A  " + "\n");
                strSqlString.Append("                         WHERE CUTOFF_DT = '" + Today + "22' " + "\n");
            }            
            
            strSqlString.Append("                           AND FACTORY = '" + GlobalVariable.gsAssyDefaultFactory + "'" + "\n");
            strSqlString.Append("                           AND LOT_TYPE = 'W'" + "\n");
            strSqlString.Append("                           AND LOT_DEL_FLAG = ' '" + "\n");
            strSqlString.Append("                           AND MAT_ID LIKE 'HX%'" + "\n");
            strSqlString.Append("                           AND OPER <= 'A0401'" + "\n");
            strSqlString.Append("                         GROUP BY MAT_ID, OPER   " + "\n");
            strSqlString.Append("                       )" + "\n");
            strSqlString.Append("               )" + "\n");
            strSqlString.Append("         GROUP BY MAT_ID" + "\n");
            strSqlString.Append("       ) WIP" + "\n");
            strSqlString.Append("     , (" + "\n");
            strSqlString.Append("        SELECT RES.MAT_ID " + "\n");
            strSqlString.Append("             , SUM(TRUNC(NVL(NVL(UPH.UPEH,0) * 24 * 0.7 * RES.RAS_CNT, 0))) AS DA1_CAPA" + "\n");
            strSqlString.Append("          FROM ( " + "\n");
            strSqlString.Append("                SELECT FACTORY, RES_STS_2 AS MAT_ID, RES_STS_8 AS OPER " + "\n");
            strSqlString.Append("                     , RES_GRP_6 AS RES_MODEL, RES_GRP_7 AS UPEH_GRP" + "\n");
            strSqlString.Append("                     , COUNT(RES_ID) AS RAS_CNT " + "\n");

            if (DateTime.Now.ToString("yyyyMMdd") == Today)
            {
                strSqlString.Append("                  FROM MRASRESDEF " + "\n");
                strSqlString.Append("                 WHERE 1=1 " + "\n");
            }
            else
            {
                strSqlString.Append("                  FROM MRASRESDEF_BOH " + "\n");
                strSqlString.Append("                 WHERE 1=1 " + "\n");
                strSqlString.Append("                   AND CUTOFF_DT = '" + Today + "22' " + "\n");
            }
            
            strSqlString.Append("                   AND FACTORY  = '" + GlobalVariable.gsAssyDefaultFactory + "' " + "\n");
            strSqlString.Append("                   AND RES_CMF_9 = 'Y' " + "\n");
            strSqlString.Append("                   AND DELETE_FLAG = ' ' " + "\n");
            strSqlString.Append("                   AND RES_STS_2 LIKE 'HX%'" + "\n");
            strSqlString.Append("                   AND RES_STS_8 IN ('A0400', 'A0401') " + "\n");
            strSqlString.Append("                 GROUP BY FACTORY,RES_STS_2,RES_STS_8,RES_GRP_6,RES_GRP_7 " + "\n");
            strSqlString.Append("               ) RES " + "\n");
            strSqlString.Append("             , CRASUPHDEF UPH " + "\n");
            strSqlString.Append("         WHERE 1 = 1 " + "\n");
            strSqlString.Append("           AND RES.FACTORY = UPH.FACTORY(+) " + "\n");
            strSqlString.Append("           AND RES.OPER = UPH.OPER(+) " + "\n");
            strSqlString.Append("           AND RES.RES_MODEL = UPH.RES_MODEL(+) " + "\n");
            strSqlString.Append("           AND RES.UPEH_GRP = UPH.UPEH_GRP(+) " + "\n");
            strSqlString.Append("           AND RES.MAT_ID = UPH.MAT_ID(+) " + "\n");
            strSqlString.Append("         GROUP BY RES.MAT_ID " + "\n");
            strSqlString.Append("       ) RES" + "\n");
            strSqlString.Append(" WHERE 1=1" + "\n");
            strSqlString.Append("   AND MAT.MAT_ID = PLN.MAT_ID(+)" + "\n");
            strSqlString.Append("   AND MAT.MAT_ID = SHP.MAT_ID(+)" + "\n");
            strSqlString.Append("   AND MAT.MAT_ID = ISS.MAT_ID(+)" + "\n");
            strSqlString.Append("   AND MAT.MAT_ID = WIP.MAT_ID(+)" + "\n");
            strSqlString.Append("   AND MAT.MAT_ID = RES.MAT_ID(+)  " + "\n");
            strSqlString.Append(" GROUP BY MAT_GRP_10, MAT_CMF_10, VENDOR_ID " + "\n");
            strSqlString.Append("HAVING SUM(NVL(PLN.WW_PLN,0) + NVL(SHP.WW_AO,0) + NVL(WIP.IN_WIP,0) + NVL(ISS.ISS_QTY,0) + NVL(WIP.IN_WIP,0) + NVL(RES.DA1_CAPA,0)) > 0     " + "\n");
            strSqlString.Append(" ORDER BY MAT_GRP_10, MAT_CMF_10, VENDOR_ID" + "\n");

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
                //int sub = ((udcTableForm)(this.btnSort.BindingForm)).GetCheckedValue(2);

                int[] rowType = spdData.RPT_DataBindingWithSubTotal(dt, 0, 1, 2, null, null, btnSort);

                //Total부분 셀머지
                spdData.RPT_FillDataSelectiveCells("Total", 0, 2, 0, 1, true, Align.Center, VerticalAlign.Center);

                spdData.RPT_AutoFit(false);

                spdData.RPT_SetPerSubTotalAndGrandTotal(1, 13, 15, 16);

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
    }
}
