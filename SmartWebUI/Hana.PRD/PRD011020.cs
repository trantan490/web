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
    public partial class PRD011020 : Miracom.SmartWeb.UI.Controls.udcCUSReport002
    {
        /// <summary>
        /// 클  래  스: PRD011020<br/>
        /// 클래스요약: PLAN 대비 공정별 잔량 관리<br/>
        /// 작  성  자: 하나마이크론 이희석<br/>
        /// 최초작성일: 2020-12-24<br/>
        /// 상세  설명: PLAN 대비 공정별 잔량 관리<br/> 
        /// 변경  내용: <br/>    
        /// </summary>
        public PRD011020()
        {
            InitializeComponent();

            SortInit();

            DateTime localDate = DateTime.Now;
            this.cdvDate.Value = localDate;
            cdvFactory.Text = GlobalVariable.gsAssyDefaultFactory;
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
           // string QueryCond = null;
            //udcTableForm tableForm = (udcTableForm)btnSort.BindingForm;
            //QueryCond = tableForm.SelectedValueToQueryContainNull;

            //spdData.ActiveSheet.ColumnHeader.Rows.Count = 1;
            spdData.RPT_ColumnInit();

                spdData.RPT_AddBasicColumn("CUSTOMER", 0, 0, Visibles.True, Frozen.True, Align.Left, Merge.True, Formatter.String, 80);

                spdData.RPT_AddBasicColumn("MAT_ID", 0, 1, Visibles.True, Frozen.True, Align.Left, Merge.True, Formatter.String, 60);
                spdData.RPT_AddBasicColumn("SHIPCODE", 0, 2, Visibles.True, Frozen.True, Align.Left, Merge.True, Formatter.String, 100);
                spdData.RPT_AddBasicColumn("MAT_ID2", 0, 3, Visibles.False, Frozen.True, Align.Left, Merge.True, Formatter.String, 100);
                spdData.RPT_AddBasicColumn("SAPCODE", 0, 4, Visibles.True, Frozen.True, Align.Left, Merge.True, Formatter.String, 60);
                spdData.RPT_AddBasicColumn("Type1", 0, 5, Visibles.True, Frozen.False, Align.Left, Merge.False, Formatter.String, 70);
                spdData.RPT_AddBasicColumn("Type2", 0, 6, Visibles.True, Frozen.False, Align.Left, Merge.False, Formatter.String, 70);
                spdData.RPT_AddBasicColumn("FLOW", 0, 7, Visibles.True, Frozen.False, Align.Left, Merge.False, Formatter.String, 70);
                spdData.RPT_AddBasicColumn("OPER", 0, 8, Visibles.True, Frozen.False, Align.Left, Merge.False, Formatter.String, 70);
                spdData.RPT_AddBasicColumn("SEQ_NUM", 0, 9, Visibles.False, Frozen.False, Align.Left, Merge.False, Formatter.String, 70);
                spdData.RPT_AddBasicColumn("WF_TYPE", 0, 10, Visibles.True, Frozen.False, Align.Center, Merge.False, Formatter.String, 70);
                spdData.RPT_AddBasicColumn("BUFFER STEP", 0, 11, Visibles.True, Frozen.False, Align.Left, Merge.False, Formatter.String, 70);
                spdData.RPT_AddBasicColumn("Same Die", 0, 12, Visibles.True, Frozen.False, Align.Left, Merge.False, Formatter.String, 70);
                spdData.RPT_AddBasicColumn("W0 PLAN", 0, 13, Visibles.True, Frozen.False, Align.Left, Merge.False, Formatter.String, 70);
                spdData.RPT_AddBasicColumn("WIP", 0, 14, Visibles.True, Frozen.False, Align.Center, Merge.False, Formatter.String, 70);
                spdData.RPT_AddBasicColumn("W0 Output", 0, 15, Visibles.True, Frozen.False, Align.Center, Merge.False, Formatter.String, 90);
                for(int i=0;i<16;i++)spdData.RPT_MerageHeaderRowSpan(0, i, 2);

                spdData.RPT_AddBasicColumn("Week", 0, 16, Visibles.True, Frozen.False, Align.Center, Merge.False, Formatter.String, 70);
                spdData.RPT_AddBasicColumn("W0", 1, 16, Visibles.True, Frozen.False, Align.Center, Merge.False, Formatter.String, 70);
                spdData.RPT_AddBasicColumn("W1", 1, 17, Visibles.True, Frozen.False, Align.Center, Merge.False, Formatter.String, 70);
                spdData.RPT_AddBasicColumn("W2", 1, 18, Visibles.True, Frozen.False, Align.Center, Merge.False, Formatter.String, 70);
                spdData.RPT_AddBasicColumn("W3", 1, 19, Visibles.True, Frozen.False, Align.Center, Merge.False, Formatter.String, 70);
                spdData.RPT_AddBasicColumn("W4", 1, 20, Visibles.True, Frozen.False, Align.Center, Merge.False, Formatter.String, 70);
                spdData.RPT_AddBasicColumn("W5", 1, 21, Visibles.True, Frozen.False, Align.Center, Merge.False, Formatter.String, 70);
                spdData.RPT_AddBasicColumn("W6", 1, 22, Visibles.True, Frozen.False, Align.Center, Merge.False, Formatter.String, 70);
                spdData.RPT_AddBasicColumn("W7", 1, 23, Visibles.True, Frozen.False, Align.Center, Merge.False, Formatter.String, 70);
                spdData.RPT_AddBasicColumn("W8", 1, 24, Visibles.True, Frozen.False, Align.Center, Merge.False, Formatter.String, 70);
                spdData.RPT_AddBasicColumn("W9", 1, 25, Visibles.True, Frozen.False, Align.Center, Merge.False, Formatter.String, 70);
                spdData.RPT_AddBasicColumn("W10", 1, 26, Visibles.True, Frozen.False, Align.Center, Merge.False, Formatter.String, 70);
                spdData.RPT_MerageHeaderColumnSpan(0, 16, 11);
                

                spdData.RPT_AddBasicColumn("Logic", 0, 27, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.String, 70);
                spdData.RPT_MerageHeaderRowSpan(0, 27, 2);

            //spdData.RPT_ColumnConfigFromTable(btnSort); //Group항목이 있을경우 반드시 선언해줄것.
            spdData.RPT_ColumnConfigFromTable_New(btnSort);
        }

        /// <summary>
        /// 3. Group By 정의
        /// </summary>
        private void SortInit()
        {

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
            string QueryCond3 = null;            
                        
            
            string strDate = null;
            string sKpcsValue;         // Kpcs 구분에 의한 나누기 분모 값

            strDate = cdvDate.SelectedValue();

            udcTableForm tableForm = (udcTableForm)btnSort.BindingForm;

            //QueryCond1 = tableForm.SelectedValueToQueryContainNull;
            //QueryCond2 = tableForm.SelectedValue2ToQueryContainNull;
            //QueryCond3 = tableForm.SelectedValue3ToQueryContainNull;
            
            

            // kpcs 선택에 의한 분모 값 저장 한다.
            if (ckbKpcs.Checked == true)
            {
                sKpcsValue = "1000";
            }
            else
            {
                sKpcsValue = "1";
            }



                    #region 메인
                    strSqlString.Append("WITH DT AS" + "\n");
                    strSqlString.Append("(" + "\n");
                    strSqlString.Append("SELECT MAT_ID,SUM(DECODE(PLAN_MONTH, TO_CHAR(ADD_MONTHS('" + strDate + "',0), 'YYYYMM'), REV_QTY, 0)) AS M0, SUM(DECODE(PLAN_MONTH, TO_CHAR(ADD_MONTHS('" + strDate + "',1), 'YYYYMM'), REV_QTY, 0)) AS M1, SUM(DECODE(PLAN_MONTH, TO_CHAR(ADD_MONTHS('" + strDate + "',2), 'YYYYMM'), REV_QTY, 0)) AS M2" + "\n");
                    strSqlString.Append("FROM(" + "\n");
                    strSqlString.Append(" SELECT B.PLAN_MONTH, A.PLAN_WEEK, A.GUBUN, B.CKD_WEEK, A.MAT_ID, A.WW_QTY, B.CNT" + "\n");
                    strSqlString.Append("      , ROUND((A.WW_QTY / 7) * B.CNT, 0) AS REV_QTY" + "\n");
                    strSqlString.Append("   FROM RWIPPLNWEK A" + "\n");
                    strSqlString.Append("      , (" + "\n");
                    strSqlString.Append("         SELECT MAX(TRIM(TO_CHAR(PLAN_YEAR))||LPAD(PLAN_MONTH,2,'0')) PLAN_MONTH" + "\n");
                    strSqlString.Append("              , MAX(TRIM(TO_CHAR(PLAN_YEAR))||LPAD(PLAN_WEEK,2,'0')) PLAN_WEEK" + "\n");
                    strSqlString.Append("              , COUNT(*) AS CNT" + "\n");
                    strSqlString.Append("              , (SELECT MAX(TRIM(TO_CHAR(PLAN_YEAR))||LPAD(PLAN_WEEK,2,'0')) PLAN_WEEK FROM MWIPCALDEF WHERE CALENDAR_ID = 'OTD' AND SYS_DATE = TO_CHAR('" + cdvFactory.Text.Trim() + "','YYYYMMDD')) AS CKD_WEEK" + "\n");
                    strSqlString.Append("           FROM MWIPCALDEF" + "\n");
                    strSqlString.Append("          WHERE 1=1" + "\n");
                    strSqlString.Append("            AND CALENDAR_ID = 'OTD'" + "\n");
                    strSqlString.Append("            AND SYS_YEAR IN (TO_CHAR(ADD_MONTHS('" + strDate + "',0), 'YYYY'),TO_CHAR(ADD_MONTHS('" + strDate + "',1), 'YYYY'),TO_CHAR(ADD_MONTHS('" + strDate + "',2), 'YYYY'))" + "\n");
                    strSqlString.Append("            AND SYS_MONTH IN (TO_CHAR(ADD_MONTHS('" + strDate + "',0), 'MM'),TO_CHAR(ADD_MONTHS('" + strDate + "',1), 'MM'),TO_CHAR(ADD_MONTHS('" + strDate + "',2), 'MM'))" + "\n");
                    strSqlString.Append("            AND SYS_DATE > '" +  (Int32.Parse(strDate) - 7) + "'" + "\n");
                    strSqlString.Append("          GROUP BY PLAN_MONTH, PLAN_WEEK" + "\n");
                    strSqlString.Append("        ) B" + "\n");
                    strSqlString.Append("  WHERE 1=1" + "\n");
                    strSqlString.Append("    AND A.PLAN_WEEK = B.PLAN_WEEK" + "\n");
                    strSqlString.Append("    AND A.FACTORY = '" + cdvFactory.Text.Trim() + "' \n");
                    strSqlString.Append("    AND A.GUBUN='3'" + "\n");
                    strSqlString.Append("    )" + "\n");
                    strSqlString.Append("    GROUP BY MAT_ID" + "\n");
                    strSqlString.Append(")" + "\n");
                    strSqlString.Append("SELECT CUSTOMER,MAT_ID,SHIPCODE,MAT_ID2,SAPCODE,TYPE1,TYPE2,FLOW,P.OPER,SEQ_NUM,WF_TYPE,OPER2 \"BUFFER STEP\",QTY \"Same Die\",SUM(NVL(WIP.QTY_1,0)) WIP,'' \"W0 PLAN\",SUM(NVL(VAL0,0)) \"W0 Output\"" + "\n");
                    strSqlString.Append("      ,SUM(NVL(W0,0)) W0,SUM(NVL(W1,0)) W1,SUM(NVL(W2,0)) W2,SUM(NVL(W3,0)) W3,SUM(NVL(W4,0)) W4,SUM(NVL(W5,0)) W5,SUM(NVL(W6,0)) W6,SUM(NVL(W7,0)) W7,SUM(NVL(W8,0)) W8,SUM(NVL(W9,0)) W9,SUM(NVL(W10,0)) W10" + "\n");
                    strSqlString.Append("      " + "\n");
                    strSqlString.Append("      ,CASE WHEN NVL((SELECT COUNT(*) FROM MWIPMATDEF WHERE DELETE_FLAG=' ' AND FACTORY='" + cdvFactory.Text.Trim() + "' AND FIRST_FLOW != ' ' AND BASE_MAT_ID=P.SHIPCODE AND VENDOR_MAT_ID!=' '),0) < 3 THEN 1" + "\n");
                    strSqlString.Append("            WHEN MAT_ID LIKE 'HX%' THEN 3" + "\n");
                    strSqlString.Append("            ELSE 2 END PLUS_DATA" + "\n");
                    strSqlString.Append("FROM (" + "\n");
                    strSqlString.Append("SELECT DISTINCT MAT.CUSTOMER,MAX(MAT.MAT_ID) MAT_ID,MAT.SHIPCODE,MAX(MAT.MAT_ID2) MAT_ID2," + "\n");
                    strSqlString.Append("      CASE WHEN TYPE1 NOT LIKE 'S%' AND FIRST_FLOW = (SELECT FLOW FROM MWIPFLWOPR WHERE FLOW = FIRST_FLOW AND OPER LIKE 'A040%' AND ROWNUM=1) AND F.OPER BETWEEN 'A0000' AND 'A0399' THEN MAT.VENDOR_MAT_ID" + "\n");
                    strSqlString.Append("           WHEN TYPE1 NOT LIKE 'S%' AND FIRST_FLOW = (SELECT FLOW FROM MWIPFLWOPR WHERE FLOW = FIRST_FLOW AND OPER LIKE 'A040%' AND ROWNUM=1) AND F.OPER > 'A0399' THEN MAT.BASE_MAT_ID" + "\n");
                    strSqlString.Append("           WHEN TYPE1 NOT LIKE 'S%' AND (SELECT FLOW FROM MWIPFLWOPR WHERE FLOW = FIRST_FLOW AND OPER LIKE 'A040%' AND ROWNUM=1) IS NULL AND F.OPER BETWEEN 'A0000' AND 'A0332' THEN MAT.VENDOR_MAT_ID" + "\n");
                    strSqlString.Append("           WHEN TYPE1 NOT LIKE 'S%' AND (SELECT FLOW FROM MWIPFLWOPR WHERE FLOW = FIRST_FLOW AND OPER LIKE 'A040%' AND ROWNUM=1) IS NULL AND F.OPER > 'A0332' THEN MAT.BASE_MAT_ID" + "\n");
                    strSqlString.Append("           WHEN TYPE1 LIKE 'S%' AND MAT.MAT_GRP_5 != 'Merge' AND FIRST_FLOW = (SELECT FLOW FROM MWIPFLWOPR WHERE FLOW = FIRST_FLOW AND OPER LIKE 'A040%' AND ROWNUM=1) AND F.OPER BETWEEN 'A0000' AND 'A0399' THEN MAT.VENDOR_MAT_ID" + "\n");
                    strSqlString.Append("           WHEN TYPE1 LIKE 'S%' AND MAT.MAT_GRP_5 != 'Merge' AND (SELECT FLOW FROM MWIPFLWOPR WHERE FLOW = FIRST_FLOW AND OPER LIKE 'A040%' AND ROWNUM=1) IS NULL AND F.OPER BETWEEN 'A0000' AND 'A0332' THEN MAT.VENDOR_MAT_ID" + "\n");
                    strSqlString.Append("           ELSE MAT.BASE_MAT_ID" + "\n");
                    strSqlString.Append("      END SAPCODE" + "\n");
                    strSqlString.Append("      ,TYPE1,MAT.MAT_GRP_5 TYPE2,FIRST_FLOW FLOW" + "\n");
                    strSqlString.Append("      ,F.OPER,F.SEQ_NUM,MAX(WF_TYPE) WF_TYPE,MAX(DATA_4) OPER2,MAX(DATA_1) QTY,SUM(NVL(W0,0)) W0,SUM(NVL(W1,0)) W1,SUM(NVL(W2,0)) W2,SUM(NVL(W3,0)) W3,SUM(NVL(W4,0)) W4,SUM(NVL(W5,0)) W5,SUM(NVL(W6,0)) W6,SUM(NVL(W7,0)) W7,SUM(NVL(W8,0)) W8,SUM(NVL(W9,0)) W9,SUM(NVL(W10,0)) W10" + "\n");
                    strSqlString.Append("FROM (" + "\n");
                    strSqlString.Append("            SELECT SHIP.CUSTOMER,MAX(SHIP.MAT_ID) MAT_ID,SHIP.SHIPCODE,MAX(D.MAT_ID) MAT_ID2" + "\n");
                    strSqlString.Append("                  ,D.BASE_MAT_ID ,D.VENDOR_MAT_ID ,MAX(SAPCODE) SAPCODE" + "\n");
                    strSqlString.Append("                  ,TYPE1,D.MAT_GRP_5,MAX(FIRST_FLOW) FIRST_FLOW, MAX(D.MAT_CMF_16) WF_TYPE" + "\n");
                    strSqlString.Append("            FROM (" + "\n");
                    strSqlString.Append("                        SELECT MAX(MAT_GRP_1) CUSTOMER,MAX(A.MAT_ID) MAT_ID,SHIPCODE,SAPCODE,MAX(TYPE1) TYPE1 FROM(" + "\n");
                    strSqlString.Append("                        SELECT MAT_GRP_1,A.MAT_ID,NVL(B.MATNR, A.BASE_MAT_ID) SHIPCODE,NVL(B.IDNRK, A.BASE_MAT_ID) SAPCODE,A.MAT_GRP_4 TYPE1" + "\n");
                    strSqlString.Append("                        FROM MWIPMATDEF A ,ZHPPT029@SAPREAL B,DT C" + "\n");
                    strSqlString.Append("                        WHERE FACTORY='" + cdvFactory.Text.Trim() + "'" + "\n");
                    strSqlString.Append("                        AND A.DELETE_FLAG=' '" + "\n");
                    strSqlString.Append("                        AND A.BASE_MAT_ID = B.MATNR(+)" + "\n");
                    strSqlString.Append("                        AND A.MAT_ID = C.MAT_ID(+)" + "\n");
                    strSqlString.Append("                        AND BASE_MAT_ID != ' '" + "\n");
                    strSqlString.Append("                        AND (M0+M1+M2)>0" + "\n");
                    strSqlString.Append("                        ) A" + "\n");
                    strSqlString.Append("                        GROUP BY SHIPCODE,SAPCODE" + "\n");
                    strSqlString.Append("                        HAVING SHIPCODE!=SAPCODE" + "\n");
                    strSqlString.Append("                        UNION" + "\n");
                    strSqlString.Append("                        SELECT MAX(MAT_GRP_1) CUSTOMER,MAX(A.MAT_ID) MAT_ID,SHIPCODE,SAPCODE,MAX(TYPE1) TYPE1 FROM(" + "\n");
                    strSqlString.Append("                        SELECT MAT_GRP_1,A.MAT_ID,NVL(B.MATNR, A.BASE_MAT_ID) SHIPCODE,NVL(B.MATNR, A.BASE_MAT_ID) SAPCODE,A.MAT_GRP_4 TYPE1" + "\n");
                    strSqlString.Append("                        FROM MWIPMATDEF A ,ZHPPT029@SAPREAL B,DT C" + "\n");
                    strSqlString.Append("                        WHERE FACTORY='" + cdvFactory.Text.Trim() + "'" + "\n");
                    strSqlString.Append("                        AND A.DELETE_FLAG=' '" + "\n");
                    strSqlString.Append("                        AND A.BASE_MAT_ID = B.MATNR(+)" + "\n");
                    strSqlString.Append("                        AND A.MAT_ID = C.MAT_ID(+)" + "\n");
                    strSqlString.Append("                        AND BASE_MAT_ID != ' '" + "\n");
                    strSqlString.Append("                        AND (M0+M1+M2)>0" + "\n");
                    strSqlString.Append("                        ) A" + "\n");
                    strSqlString.Append("                        GROUP BY SHIPCODE,SAPCODE" + "\n");
                    strSqlString.Append("                        HAVING SHIPCODE=SAPCODE" + "\n");
                    strSqlString.Append("                    ) SHIP, (" + "\n");
                    strSqlString.Append("                    SELECT BASE_MAT_ID,VENDOR_MAT_ID,FIRST_FLOW,MAT_ID,MAT_GRP_5,MAT_CMF_16" + "\n");
                    strSqlString.Append("                    FROM MWIPMATDEF" + "\n");
                    strSqlString.Append("                    WHERE 1=1" + "\n");
                    strSqlString.Append("                    AND DELETE_FLAG=' '" + "\n");
                    strSqlString.Append("                    AND FACTORY='" + cdvFactory.Text.Trim() + "'" + "\n");
                    strSqlString.Append("                    AND FIRST_FLOW != ' '" + "\n");
                    strSqlString.Append("                    )D" + "\n");
                    strSqlString.Append("                    WHERE 1=1" + "\n");
                    strSqlString.Append("                    AND SHIP.SAPCODE=D.BASE_MAT_ID(+)" + "\n");
                    strSqlString.Append("                    AND SUBSTR(SHIP.MAT_ID,-3) = SUBSTR(SHIPCODE,6,3)" + "\n");
                    strSqlString.Append("                    GROUP BY CUSTOMER,SHIP.SHIPCODE,BASE_MAT_ID,VENDOR_MAT_ID,TYPE1,MAT_GRP_5" + "\n");
                    strSqlString.Append("   ) MAT,MWIPFLWOPR F,(SELECT KEY_1,DATA_1,DATA_4 FROM MGCMTBLDAT WHERE TABLE_NAME IN ('H_HX_AUTO_LOSS','H_SEC_AUTO_LOSS')  AND FACTORY = '" + cdvFactory.Text.Trim() + "') D" + "\n");
                    strSqlString.Append("   ,(" + "\n");
                    strSqlString.Append("    SELECT VENDOR_ID AS SAP_CODE " + "\n");
                    strSqlString.Append("     , BASE_MAT_ID AS SHIP_CODE" + "\n");
                    strSqlString.Append("     , ROUND(MAX(W0 * CONVERT_QTY) /" + sKpcsValue + ",0) AS W0" + "\n");
                    strSqlString.Append("     , ROUND(MAX(W1 * CONVERT_QTY) /" + sKpcsValue + ",0) AS W1" + "\n");
                    strSqlString.Append("     , ROUND(MAX(W2 * CONVERT_QTY) /" + sKpcsValue + ",0) AS W2" + "\n");
                    strSqlString.Append("     , ROUND(MAX(W3 * CONVERT_QTY) /" + sKpcsValue + ",0) AS W3" + "\n");
                    strSqlString.Append("     , ROUND(MAX(W4 * CONVERT_QTY) /" + sKpcsValue + ",0) AS W4" + "\n");
                    strSqlString.Append("     , ROUND(MAX(W5 * CONVERT_QTY) /" + sKpcsValue + ",0) AS W5" + "\n");
                    strSqlString.Append("     , ROUND(MAX(W6 * CONVERT_QTY) /" + sKpcsValue + ",0) AS W6 " + "\n");
                    strSqlString.Append("     , ROUND(MAX(W7 * CONVERT_QTY) /" + sKpcsValue + ",0) AS W7" + "\n");
                    strSqlString.Append("     , ROUND(MAX(W8 * CONVERT_QTY) /" + sKpcsValue + ",0) AS W8" + "\n");
                    strSqlString.Append("     , ROUND(MAX(W9 * CONVERT_QTY) /" + sKpcsValue + ",0) AS W9" + "\n");
                    strSqlString.Append("     , ROUND(MAX(W10 * CONVERT_QTY) /" + sKpcsValue + ",0) AS W10" + "\n");
                    strSqlString.Append("  FROM (" + "\n");
                    strSqlString.Append("            SELECT MAT.VENDOR_ID,BASE_MAT_ID, MAT_GRP_1, MAT_GRP_9, MAT_GRP_2" + "\n");
                    strSqlString.Append("                 , NVL((SELECT DATA_10 FROM MGCMTBLDAT WHERE TABLE_NAME = 'H_CUSTOMER' AND FACTORY = '" + cdvFactory.Text.Trim() + "' AND  KEY_1 = MAT_GRP_1), '-') AS CUST_TYPE" + "\n");
                    strSqlString.Append("                 , CASE WHEN MAT.MAT_GRP_3 IN ('COB', 'BGN') THEN W0 / TO_NUMBER(DECODE(MAT.MAT_CMF_13,' ',1,MAT.MAT_CMF_13)) ELSE W0 END AS W0" + "\n");
                    strSqlString.Append("                 , CASE WHEN MAT.MAT_GRP_3 IN ('COB', 'BGN') THEN W1 / TO_NUMBER(DECODE(MAT.MAT_CMF_13,' ',1,MAT.MAT_CMF_13)) ELSE W1 END AS W1" + "\n");
                    strSqlString.Append("                 , CASE WHEN MAT.MAT_GRP_3 IN ('COB', 'BGN') THEN W2 / TO_NUMBER(DECODE(MAT.MAT_CMF_13,' ',1,MAT.MAT_CMF_13)) ELSE W2 END AS W2" + "\n");
                    strSqlString.Append("                 , CASE WHEN MAT.MAT_GRP_3 IN ('COB', 'BGN') THEN W3 / TO_NUMBER(DECODE(MAT.MAT_CMF_13,' ',1,MAT.MAT_CMF_13)) ELSE W3 END AS W3" + "\n");
                    strSqlString.Append("                 , CASE WHEN MAT.MAT_GRP_3 IN ('COB', 'BGN') THEN W4 / TO_NUMBER(DECODE(MAT.MAT_CMF_13,' ',1,MAT.MAT_CMF_13)) ELSE W4 END AS W4" + "\n");
                    strSqlString.Append("                 , CASE WHEN MAT.MAT_GRP_3 IN ('COB', 'BGN') THEN W5 / TO_NUMBER(DECODE(MAT.MAT_CMF_13,' ',1,MAT.MAT_CMF_13)) ELSE W5 END AS W5" + "\n");
                    strSqlString.Append("                 , CASE WHEN MAT.MAT_GRP_3 IN ('COB', 'BGN') THEN W6 / TO_NUMBER(DECODE(MAT.MAT_CMF_13,' ',1,MAT.MAT_CMF_13)) ELSE W6 END AS W6" + "\n");
                    strSqlString.Append("                 , CASE WHEN MAT.MAT_GRP_3 IN ('COB', 'BGN') THEN W7 / TO_NUMBER(DECODE(MAT.MAT_CMF_13,' ',1,MAT.MAT_CMF_13)) ELSE W7 END AS W7" + "\n");
                    strSqlString.Append("                 , CASE WHEN MAT.MAT_GRP_3 IN ('COB', 'BGN') THEN W8 / TO_NUMBER(DECODE(MAT.MAT_CMF_13,' ',1,MAT.MAT_CMF_13)) ELSE W8 END AS W8" + "\n");
                    strSqlString.Append("                 , CASE WHEN MAT.MAT_GRP_3 IN ('COB', 'BGN') THEN W9 / TO_NUMBER(DECODE(MAT.MAT_CMF_13,' ',1,MAT.MAT_CMF_13)) ELSE W9 END AS W9" + "\n");
                    strSqlString.Append("                 , CASE WHEN MAT.MAT_GRP_3 IN ('COB', 'BGN') THEN W10 / TO_NUMBER(DECODE(MAT.MAT_CMF_13,' ',1,MAT.MAT_CMF_13)) ELSE W10 END AS W10" + "\n");
                    strSqlString.Append("                 , 1 AS CONVERT_QTY" + "\n");
                    strSqlString.Append("              FROM MWIPMATDEF MAT" + "\n");
                    strSqlString.Append("                 , (" + "\n");
                    strSqlString.Append("                    SELECT MAT_ID" + "\n");
                    strSqlString.Append("                         , SUM(DECODE(PLAN_WEEK, (SELECT PLAN_YEAR || PLAN_WEEK FROM MWIPCALDEF WHERE SYS_DATE = '" + strDate + "' AND CALENDAR_ID='OTD'), WW_QTY, 0)) AS W0" + "\n");
                    strSqlString.Append("                         , SUM(DECODE(PLAN_WEEK, (SELECT PLAN_YEAR || PLAN_WEEK FROM MWIPCALDEF WHERE SYS_DATE = TO_DATE('" + strDate + "') + 7 AND CALENDAR_ID='OTD'), WW_QTY, 0)) AS W1" + "\n");
                    strSqlString.Append("                         , SUM(DECODE(PLAN_WEEK, (SELECT PLAN_YEAR || PLAN_WEEK FROM MWIPCALDEF WHERE SYS_DATE = TO_DATE('" + strDate + "') + 14 AND CALENDAR_ID='OTD'), WW_QTY, 0)) AS W2" + "\n");
                    strSqlString.Append("                         , SUM(DECODE(PLAN_WEEK, (SELECT PLAN_YEAR || PLAN_WEEK FROM MWIPCALDEF WHERE SYS_DATE = TO_DATE('" + strDate + "') + 21 AND CALENDAR_ID='OTD'), WW_QTY, 0)) AS W3" + "\n");
                    strSqlString.Append("                         , SUM(DECODE(PLAN_WEEK, (SELECT PLAN_YEAR || PLAN_WEEK FROM MWIPCALDEF WHERE SYS_DATE = TO_DATE('" + strDate + "') + 28 AND CALENDAR_ID='OTD'), WW_QTY, 0)) AS W4" + "\n");
                    strSqlString.Append("                         , SUM(DECODE(PLAN_WEEK, (SELECT PLAN_YEAR || PLAN_WEEK FROM MWIPCALDEF WHERE SYS_DATE = TO_DATE('" + strDate + "') + 35 AND CALENDAR_ID='OTD'), WW_QTY, 0)) AS W5" + "\n");
                    strSqlString.Append("                         , SUM(DECODE(PLAN_WEEK, (SELECT PLAN_YEAR || PLAN_WEEK FROM MWIPCALDEF WHERE SYS_DATE = TO_DATE('" + strDate + "') + 42 AND CALENDAR_ID='OTD'), WW_QTY, 0)) AS W6" + "\n");
                    strSqlString.Append("                         , SUM(DECODE(PLAN_WEEK, (SELECT PLAN_YEAR || PLAN_WEEK FROM MWIPCALDEF WHERE SYS_DATE = TO_DATE('" + strDate + "') + 49 AND CALENDAR_ID='OTD'), WW_QTY, 0)) AS W7" + "\n");
                    strSqlString.Append("                         , SUM(DECODE(PLAN_WEEK, (SELECT PLAN_YEAR || PLAN_WEEK FROM MWIPCALDEF WHERE SYS_DATE = TO_DATE('" + strDate + "') + 56 AND CALENDAR_ID='OTD'), WW_QTY, 0)) AS W8" + "\n");
                    strSqlString.Append("                         , SUM(DECODE(PLAN_WEEK, (SELECT PLAN_YEAR || PLAN_WEEK FROM MWIPCALDEF WHERE SYS_DATE = TO_DATE('" + strDate + "') + 63 AND CALENDAR_ID='OTD'), WW_QTY, 0)) AS W9" + "\n");
                    strSqlString.Append("                         , SUM(DECODE(PLAN_WEEK, (SELECT PLAN_YEAR || PLAN_WEEK FROM MWIPCALDEF WHERE SYS_DATE = TO_DATE('" + strDate + "') + 70 AND CALENDAR_ID='OTD'), WW_QTY, 0)) AS W10" + "\n");
                    strSqlString.Append("                      FROM RWIPPLNWEK" + "\n");
                    strSqlString.Append("                     WHERE 1=1" + "\n");
                    strSqlString.Append("                       AND FACTORY = '" + cdvFactory.Text.Trim() + "'" + "\n");
                    strSqlString.Append("                       AND GUBUN = '3' " + "\n");
                    strSqlString.Append("                       AND PLAN_WEEK BETWEEN (SELECT PLAN_YEAR || PLAN_WEEK FROM MWIPCALDEF WHERE SYS_DATE = '" + strDate + "' AND CALENDAR_ID='OTD') " + "\n");
                    strSqlString.Append("                                         AND (SELECT PLAN_YEAR || PLAN_WEEK FROM MWIPCALDEF WHERE SYS_DATE = TO_DATE('" + strDate + "') + 70 AND CALENDAR_ID='OTD')" + "\n");
                    strSqlString.Append("                     GROUP BY MAT_ID" + "\n");
                    strSqlString.Append("                   ) PLN" + "\n");
                    strSqlString.Append("             WHERE 1=1" + "\n");
                    strSqlString.Append("               AND MAT.MAT_ID = PLN.MAT_ID" + "\n");
                    strSqlString.Append("               AND MAT.FACTORY = '" + cdvFactory.Text.Trim() + "'" + "\n");
                    strSqlString.Append("               AND MAT.DELETE_FLAG = ' '" + "\n");
                    strSqlString.Append("               AND MAT.MAT_TYPE = 'FG'" + "\n");
                    strSqlString.Append("           )" + "\n");
                    strSqlString.Append("     GROUP BY CUST_TYPE, MAT_GRP_1, MAT_GRP_9, MAT_GRP_2,  ' ',  ' ',  ' ',  ' ',  ' ',  ' ',  ' ',  ' ',  ' ', BASE_MAT_ID,VENDOR_ID " + "\n");
                    strSqlString.Append("        )PLN" + "\n");
                    strSqlString.Append("   WHERE 1=1" + "\n");
                    strSqlString.Append("AND MAT.FIRST_FLOW = F.FLOW" + "\n");
                    strSqlString.Append("AND MAT.MAT_ID2 = D.KEY_1(+)" + "\n");
                    strSqlString.Append("AND MAT.SHIPCODE = PLN.SHIP_CODE(+)" + "\n");
                    strSqlString.Append("AND F.FACTORY='" + cdvFactory.Text.Trim() + "'" + "\n");
                    strSqlString.Append("AND F.OPER =     CASE WHEN TYPE1 LIKE 'S%' AND MAT.MAT_GRP_5 = 'Merge' AND F.SEQ_NUM < (SELECT SEQ_NUM FROM MWIPFLWOPR WHERE FLOW = F.FLOW AND OPER = (SELECT MAX(OPER) FROM MWIPFLWOPR WHERE FLOW = F.FLOW AND OPER LIKE 'A040%')) THEN ' ' " + "\n");
                    strSqlString.Append("                 ELSE F.OPER" + "\n");
                    strSqlString.Append("                 END " + "\n");
                    strSqlString.Append("GROUP BY CUSTOMER,MAT.SHIPCODE,MAT.BASE_MAT_ID,MAT.VENDOR_MAT_ID,FIRST_FLOW,F.OPER,F.SEQ_NUM,TYPE1,MAT_GRP_5" + "\n");
                    strSqlString.Append("HAVING F.OPER != ' '" + "\n");
                    strSqlString.Append(") P,(" + "\n");
                    strSqlString.Append("                          SELECT CASE WHEN OPER <= 'A0300' THEN VENDOR_MAT_ID ELSE BASE_MAT_ID END SAP_CODE, LOT.OPER " + "\n");
                    strSqlString.Append("                             ,SUM(LOT.QTY_1) AS QTY_1 " + "\n");
                    strSqlString.Append("                          FROM  RWIPLOTSTS LOT " + "\n");
                    strSqlString.Append("                              , MWIPMATDEF MAT " + "\n");
                    strSqlString.Append("                         WHERE  1=1 " + "\n");
                    strSqlString.Append("                           AND LOT.MAT_ID NOT IN (SELECT MAT_ID FROM MWIPMATDEF WHERE FIRST_FLOW = 'A-BANK' AND DELETE_FLAG = ' ') " + "\n");
                    strSqlString.Append("                           AND LOT.FACTORY = MAT.FACTORY " + "\n");
                    strSqlString.Append("                           AND LOT.MAT_ID = MAT.MAT_ID " + "\n");
                    strSqlString.Append("                           AND LOT_DEL_FLAG = ' '  " + "\n");
                    strSqlString.Append("                           AND LOT.LOT_TYPE = 'W' " + "\n");
                    strSqlString.Append("                           AND MAT.DELETE_FLAG = ' ' " + "\n");
                    strSqlString.Append("                           AND LOT.FACTORY  = '" + cdvFactory.Text.Trim() + "' " + "\n");
                    strSqlString.Append("                           AND LOT.OPER like 'A%'" + "\n");
                    strSqlString.Append("                        GROUP BY BASE_MAT_ID,VENDOR_MAT_ID, LOT.OPER" + "\n");
                    strSqlString.Append("                        ORDER BY (SELECT TO_NUMBER(OPER_CMF_2) FROM MWIPOPRDEF WHERE FACTORY  = '" + cdvFactory.Text.Trim() + "'  AND OPER=LOT.OPER), BASE_MAT_ID,VENDOR_MAT_ID, LOT.OPER" + "\n");
                    strSqlString.Append("                " + "\n");
                    strSqlString.Append("      ) WIP" + "\n");
                    strSqlString.Append("            ,(" + "\n");
                    strSqlString.Append("      SELECT OPER, CASE WHEN OPER <= 'A0300' THEN VENDOR_MAT_ID ELSE BASE_MAT_ID END SAP_CODE" + "\n");
                    strSqlString.Append("             , SUM(ASSY_END_QTY0 ) VAL0" + "\n");
                    strSqlString.Append("          FROM (" + "\n");
                    strSqlString.Append("               SELECT B.MAT_GRP_1,  ' ',  ' ',  ' ',  ' ',  ' ',  ' ',  ' ',  ' ', B.MAT_CMF_10, B.MAT_ID, A.OPER,  ' ', BASE_MAT_ID,VENDOR_MAT_ID " + "\n");
                    strSqlString.Append("                    , DECODE(WORK_WEEK, (SELECT SYS_YEAR || PLAN_WEEK FROM MWIPCALDEF WHERE SYS_DATE = '" + strDate + "' AND CALENDAR_ID='OTD'), (CASE WHEN OPER IN ('AZ010','SHIP','TZ010','F0000','EZ010', 'SZ010', 'BZ010') THEN SHIP_QTY_1 ELSE END_QTY_1 END),0) ASSY_END_QTY0" + "\n");
                    strSqlString.Append("                    , (CASE WHEN OPER IN ('AZ010','SHIP','TZ010','F0000','EZ010', 'SZ010', 'BZ010') THEN SHIP_QTY_1 ELSE END_QTY_1 END) TOTAL" + "\n");
                    strSqlString.Append("                 FROM (" + "\n");
                    strSqlString.Append("                      SELECT FACTORY, MAT_ID, OPER, LOT_TYPE, MAT_VER, WORK_WEEK, CM_KEY_3" + "\n");
                    strSqlString.Append("                           , SUM(END_LOT) AS END_LOT" + "\n");
                    strSqlString.Append("                           , SUM(END_QTY_1) AS END_QTY_1" + "\n");
                    strSqlString.Append("                           , SUM(END_QTY_2) AS END_QTY_2" + "\n");
                    strSqlString.Append("                           , SUM(SHIP_QTY_1) AS SHIP_QTY_1" + "\n");
                    strSqlString.Append("                           , SUM(SHIP_QTY_2) AS SHIP_QTY_2" + "\n");
                    strSqlString.Append("                        FROM (" + "\n");
                    strSqlString.Append("                             SELECT FACTORY, MAT_ID, OPER, LOT_TYPE, MAT_VER, '202050' WORK_WEEK, CM_KEY_3" + "\n");
                    strSqlString.Append("                           , DECODE(SUBSTR(OPER,2,4),'0000',SUM(S1_OPER_IN_LOT+S2_OPER_IN_LOT+S3_OPER_IN_LOT),SUM(S1_END_LOT+S2_END_LOT+S3_END_LOT)) END_LOT" + "\n");
                    strSqlString.Append("                           , DECODE(SUBSTR(OPER,2,4),'0000',SUM(S1_OPER_IN_QTY_1+S2_OPER_IN_QTY_1+S3_OPER_IN_QTY_1),SUM(S1_END_QTY_1+S2_END_QTY_1+S3_END_QTY_1+S1_END_RWK_QTY_1 + S2_END_RWK_QTY_1 + S3_END_RWK_QTY_1)) END_QTY_1" + "\n");
                    strSqlString.Append("                           , DECODE(SUBSTR(OPER,2,4),'0000',SUM(S1_OPER_IN_QTY_2+S2_OPER_IN_QTY_2+S3_OPER_IN_QTY_2),SUM(S1_END_QTY_2+S2_END_QTY_2+S3_END_QTY_2+S1_END_RWK_QTY_2 + S2_END_RWK_QTY_2 + S3_END_RWK_QTY_2)) END_QTY_2" + "\n");
                    strSqlString.Append("                           , 0 SHIP_QTY_1" + "\n");
                    strSqlString.Append("                           , 0 SHIP_QTY_2" + "\n");
                    strSqlString.Append("                        FROM RSUMWIPMOV " + "\n");
                    strSqlString.Append("                       WHERE OPER NOT IN ('AZ010','TZ010','F0000','EZ010', 'SZ010', 'BZ010')" + "\n");
                    strSqlString.Append("                             AND CM_KEY_1='" + cdvFactory.Text.Trim() + "' AND WORK_DATE BETWEEN " + "\n");
                    strSqlString.Append("                             (SELECT MIN(SYS_DATE) FROM MWIPCALDEF WHERE SYS_YEAR='" + strDate.Substring(0, 4) + "' AND PLAN_WEEK = (SELECT PLAN_WEEK FROM MWIPCALDEF WHERE SYS_DATE = '" + strDate + "' AND CALENDAR_ID='OTD') AND CALENDAR_ID='OTD') " + "\n");
                    strSqlString.Append("                             AND " + "\n");
                    strSqlString.Append("                             (SELECT MAX(SYS_DATE) FROM MWIPCALDEF WHERE SYS_YEAR='" + strDate.Substring(0, 4) + "' AND PLAN_WEEK = (SELECT PLAN_WEEK FROM MWIPCALDEF WHERE SYS_DATE = '" + strDate + "' AND CALENDAR_ID='OTD') AND CALENDAR_ID='OTD')" + "\n");
                    strSqlString.Append("                    GROUP BY FACTORY, MAT_ID, OPER, LOT_TYPE, MAT_VER, WORK_WEEK, CM_KEY_3" + "\n");
                    strSqlString.Append("                   UNION ALL" + "\n");
                    strSqlString.Append("                      SELECT CM_KEY_1 AS FACTORY, MAT_ID" + "\n");
                    strSqlString.Append("                           , DECODE(CM_KEY_1,'" + cdvFactory.Text.Trim() + "','AZ010','HMKT1','TZ010','FGS','F0000','HMKE1','EZ010','HMKS1','SZ010', 'BZ010') OPER" + "\n");
                    strSqlString.Append("                           , LOT_TYPE, MAT_VER, (SELECT SYS_YEAR || PLAN_WEEK FROM MWIPCALDEF WHERE SYS_DATE = '" + strDate + "' AND CALENDAR_ID='OTD') WORK_WEEK,CM_KEY_3" + "\n");
                    strSqlString.Append("                           , 0 END_LOT" + "\n");
                    strSqlString.Append("                           , 0 END_QTY_1" + "\n");
                    strSqlString.Append("                           , 0 END_QTY_2" + "\n");
                    strSqlString.Append("                           , SUM(S1_FAC_OUT_QTY_1+S2_FAC_OUT_QTY_1+S3_FAC_OUT_QTY_1) SHIP_QTY_1" + "\n");
                    strSqlString.Append("                           , SUM(S1_FAC_OUT_QTY_2+S2_FAC_OUT_QTY_2+S3_FAC_OUT_QTY_2) SHIP_QTY_2" + "\n");
                    strSqlString.Append("                        FROM RSUMFACMOV " + "\n");
                    strSqlString.Append("                       WHERE FACTORY NOT IN ('RETURN') AND CM_KEY_1='" + cdvFactory.Text.Trim() + "' AND WORK_DATE BETWEEN" + "\n");
                    strSqlString.Append("                            (SELECT MIN(SYS_DATE) FROM MWIPCALDEF WHERE SYS_YEAR='" + strDate.Substring(0, 4) + "' AND PLAN_WEEK = (SELECT PLAN_WEEK FROM MWIPCALDEF WHERE SYS_DATE = '" + strDate + "' AND CALENDAR_ID='OTD') AND CALENDAR_ID='OTD') " + "\n");
                    strSqlString.Append("                             AND " + "\n");
                    strSqlString.Append("                            (SELECT MAX(SYS_DATE) FROM MWIPCALDEF WHERE SYS_YEAR='" + strDate.Substring(0, 4) + "' AND PLAN_WEEK = (SELECT PLAN_WEEK FROM MWIPCALDEF WHERE SYS_DATE = '" + strDate + "' AND CALENDAR_ID='OTD') AND CALENDAR_ID='OTD')" + "\n");
                    strSqlString.Append("                    GROUP BY CM_KEY_1, MAT_ID, OPER, LOT_TYPE, MAT_VER, WORK_WEEK, CM_KEY_3" + "\n");
                    strSqlString.Append("                             )" + "\n");
                    strSqlString.Append("                    GROUP BY FACTORY, MAT_ID, OPER, LOT_TYPE, MAT_VER, WORK_WEEK, CM_KEY_3" + "\n");
                    strSqlString.Append("                    )A" + "\n");
                    strSqlString.Append("                  , MWIPMATDEF B " + "\n");
                    strSqlString.Append("              WHERE 1=1 " + "\n");
                    strSqlString.Append("                AND A.FACTORY  = '" + cdvFactory.Text.Trim() + "' " + "\n");
                    strSqlString.Append("                AND A.FACTORY = B.FACTORY " + "\n");
                    strSqlString.Append("                AND A.MAT_ID = B.MAT_ID                " + "\n");
                    strSqlString.Append("                AND A.MAT_VER = 1 " + "\n");
                    strSqlString.Append("                AND B.MAT_VER = 1 " + "\n");
                    strSqlString.Append("                AND B.MAT_TYPE = 'FG' " + "\n");
                    strSqlString.Append("                AND A.OPER LIKE '%'" + "\n");
                    strSqlString.Append("                AND A.MAT_ID LIKE '%'  " + "\n");
                    strSqlString.Append("                AND A.CM_KEY_3 LIKE 'P%'" + "\n");
                    strSqlString.Append("                AND A.OPER NOT IN ('00001','00002') " + "\n");
                    strSqlString.Append("         )   " + "\n");
                    strSqlString.Append("   WHERE TOTAL > 0           " + "\n");
                    strSqlString.Append("GROUP BY BASE_MAT_ID,VENDOR_MAT_ID, OPER" + "\n");
                    strSqlString.Append("ORDER BY BASE_MAT_ID,VENDOR_MAT_ID,DECODE(OPER,'AISSUE','A0001','TISSUE','T0001','EISSUE','E0001',OPER)" + "\n");
                    strSqlString.Append(") SHIP" + "\n");
                    strSqlString.Append("     WHERE 1=1" + "\n");
                    strSqlString.Append("     AND P.SAPCODE = WIP.SAP_CODE(+)" + "\n");
                    strSqlString.Append("     AND P.SAPCODE = SHIP.SAP_CODE(+)" + "\n");
                    strSqlString.Append("     AND P.SAPCODE like '" + txtSearchProduct.Text + "' \n");
                    strSqlString.Append("     AND P.OPER = WIP.OPER(+)" + "\n");
                    strSqlString.Append("     AND P.OPER = SHIP.OPER(+)" + "\n");
                    
                         

                    strSqlString.Append("     GROUP BY CUSTOMER,MAT_ID,SHIPCODE,MAT_ID2,SAPCODE,WIP.SAP_CODE,TYPE1,TYPE2,FLOW,P.OPER,SEQ_NUM,WF_TYPE,OPER2,QTY" + "\n");
                    strSqlString.Append("     ORDER BY SHIPCODE,TYPE1,SAPCODE,FLOW,SEQ_NUM,OPER" + "\n");           

                    #endregion
                
            
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
                int[] rowType = spdData.RPT_DataBindingWithSubTotal(dt, 0, 0, 5, null, null, btnSort);

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
        #endregion

        private void cdvBigOperGroup_ValueButtonPress(object sender, EventArgs e)
        {
            string strQuery = string.Empty;

        }


    }
}