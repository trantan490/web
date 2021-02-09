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
    public partial class TAT050602 : Miracom.SmartWeb.UI.Controls.udcCUSReport002
    {
        private DataTable dtPkg;

        /// <summary>
        /// 클  래  스: TAT050602<br/>
        /// 클래스요약: Lot별 TAT 조회<br/>
        /// 작  성  자: 하나마이크론 임종우<br/>
        /// 최초작성일: 2009-04-0<br/>
        /// 상세  설명: Lot별 TAT 조회<br/>
        /// 변경  내용: <br/>
        /// 변  경  자: <br />
        /// <br />
        /// </summary>
        /// 
        public TAT050602()
        {
            InitializeComponent();
            cdvDate.Value = DateTime.Now;
            //cdvDate.Value = DateTime.Now.AddDays(-1).ToString();
          //  SortInit();           
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

        /// <summary>
        /// 2. 헤더 생성
        /// </summary>
        private void GridColumnInit()
        {
            try
            {
                spdData.ActiveSheet.ColumnHeader.Rows.Count = 1;
                spdData.RPT_ColumnInit();

                if (dtPkg == null)
                {
                    spdData.RPT_AddBasicColumn("Customer", 0, 0, Visibles.True, Frozen.False, Align.Center, Merge.True, Formatter.String, 100);
                }
                else
                {
                    spdData.RPT_AddBasicColumn(dtPkg.Rows[0][0].ToString(), 0, 0, Visibles.True, Frozen.False, Align.Center, Merge.True, Formatter.String, 100);
                    // header 의 Colnum Count
                    int headerCount = 1;

                // Header 에 Pkg 추가하기


                    for (int i = 0; i < dtPkg.Rows.Count; i++)
                    {
                        spdData.RPT_AddBasicColumn(dtPkg.Rows[i][1].ToString(), 0, headerCount, Visibles.True, Frozen.False, Align.Center, Merge.True, Formatter.String, 240);
                        spdData.RPT_AddBasicColumn("goal", 1, headerCount, Visibles.True, Frozen.False, Align.Center, Merge.True, Formatter.Double2, 80);
                        headerCount++;

                        spdData.RPT_AddBasicColumn("Cumulative monthly", 1, headerCount, Visibles.True, Frozen.False, Align.Center, Merge.True, Formatter.Double2, 80);
                        headerCount++;

                        spdData.RPT_AddBasicColumn("Daily Performance", 1, headerCount, Visibles.True, Frozen.False, Align.Center, Merge.True, Formatter.Double2, 80);
                        headerCount++;

                        spdData.RPT_MerageHeaderColumnSpan(0, headerCount - 3, 3);

                    }
                }

                spdData.RPT_MerageHeaderRowSpan(0, 0, 2);

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
            ((udcTableForm)(this.btnSort.BindingForm)).AddRow("Customer", "MAT.MAT_GRP_1", "MAT.MAT_GRP_1", false);
            ((udcTableForm)(this.btnSort.BindingForm)).AddRow("Family", "MAT.MAT_GRP_2", "MAT.MAT_GRP_2", false);
            ((udcTableForm)(this.btnSort.BindingForm)).AddRow("Package", "MAT.MAT_GRP_3", "MAT.MAT_GRP_3", false);
            ((udcTableForm)(this.btnSort.BindingForm)).AddRow("Type1", "MAT.MAT_GRP_4", "MAT.MAT_GRP_4", false);
            ((udcTableForm)(this.btnSort.BindingForm)).AddRow("Type2", "MAT.MAT_GRP_5", "MAT.MAT_GRP_5", false);
            ((udcTableForm)(this.btnSort.BindingForm)).AddRow("LD Count", "MAT.MAT_GRP_6", "MAT.MAT_GRP_6", false);
            ((udcTableForm)(this.btnSort.BindingForm)).AddRow("Density", "MAT.MAT_GRP_7", "MAT.MAT_GRP_7", false);
            ((udcTableForm)(this.btnSort.BindingForm)).AddRow("Generation", "MAT.MAT_GRP_8", "MAT.MAT_GRP_8", false);
            ((udcTableForm)(this.btnSort.BindingForm)).AddRow("Pin Type", "MAT.MAT_CMF_10", "MAT.MAT_CMF_10", false);
            ((udcTableForm)(this.btnSort.BindingForm)).AddRow("Product", "MAT.MAT_ID", "MAT.MAT_ID", false);
            ((udcTableForm)(this.btnSort.BindingForm)).AddRow("Cust Device", "MAT.MAT_CMF_7", "MAT.MAT_CMF_7", false);   
        }
        #endregion


        #region SQL 쿼리 Build
        /// <summary>
        /// 4. 쿼리 생성
        /// </summary>
        /// <returns></returns>


        private string PkgSqlString()
        {

            string Tran_Time = cdvDate.Value.ToString("yyyy-MM-dd");
            string sWeekTime = DateTime.Parse(Tran_Time).AddDays(-31).ToString("yyyyMMdd");
            string eTime = DateTime.Parse(Tran_Time).ToString("yyyyMMdd");

            StringBuilder strSqlString = new StringBuilder();

            strSqlString.AppendFormat("SELECT (SELECT DATA_1 FROM MGCMTBLDAT WHERE TABLE_NAME = 'H_CUSTOMER' AND KEY_1 = MAT.MAT_GRP_1 AND ROWNUM=1) AS MAT_GRP_1, MAT.MAT_GRP_3" + "\n");
            strSqlString.AppendFormat("FROM (" + "\n");
            strSqlString.AppendFormat("   SELECT  TAT.MAT_ID,TAT.FACTORY, OPER_DESC" + "\n");
            strSqlString.AppendFormat("   FROM    CSUMTATLOT@RPTTOMES TAT," + "\n");
            strSqlString.AppendFormat("   MWIPOPRDEF OPR," + "\n");
            strSqlString.AppendFormat("   MGCMTBLDAT GCM" + "\n");
            strSqlString.AppendFormat("   WHERE   1=1" + "\n");
            strSqlString.AppendFormat("       AND TAT.OPER=OPR.OPER" + "\n");
            strSqlString.AppendFormat("       AND OPR.OPER_GRP_3 = GCM.KEY_1" + "\n");
            strSqlString.AppendFormat("       AND GCM.FACTORY='" + cdvFactory.Text.ToString() + "'" + "\n");
            strSqlString.AppendFormat("       AND GCM.TABLE_NAME='H_DEPARTMENT'" + "\n");
            strSqlString.AppendFormat("       AND OPR.OPER_GRP_2 " + cdvTeam.SelectedValueToQueryString + "\n");
            strSqlString.AppendFormat("       AND GCM.KEY_1 " + cdvPart.SelectedValueToQueryString + "\n");
            strSqlString.AppendFormat("       AND OPR.FACTORY ='" + cdvFactory.Text.ToString() + "'" + "\n");
            strSqlString.AppendFormat("       AND TAT.FACTORY ='" + cdvFactory.Text.ToString() + "'" + "\n");
            strSqlString.AppendFormat("       AND TAT.OPER = OPR.OPER" + "\n");
            strSqlString.AppendFormat("       AND TAT.SHIP_TIME BETWEEN '" + sWeekTime + "220000' AND '" + eTime + "215959' " + "\n");
            strSqlString.AppendFormat(") TAT," + "\n");
            strSqlString.AppendFormat("MWIPMATDEF MAT" + "\n");
            strSqlString.AppendFormat("WHERE MAT.MAT_GRP_1 " + udcWIPCondition1.SelectedValueToQueryString + "\n");
            strSqlString.AppendFormat("   AND MAT.FACTORY=TAT.FACTORY" + "\n");
            strSqlString.AppendFormat("   AND MAT.FACTORY='" + cdvFactory.Text.ToString() + "'" + "\n");
            strSqlString.AppendFormat("   AND MAT.MAT_ID=TAT.MAT_ID" + "\n");
            strSqlString.AppendFormat("   AND MAT.MAT_VER=1" + "\n");
            strSqlString.AppendFormat("   AND MAT.DELETE_FLAG=' '" + "\n");
            strSqlString.AppendFormat("GROUP BY   MAT_GRP_1, MAT_GRP_3 " + "\n");
            strSqlString.AppendFormat("ORDER BY MAT_GRP_3" + "\n");

            System.Windows.Forms.Clipboard.SetText(strSqlString.ToString());

            return strSqlString.ToString();
        }








        private string MakeSqlString()
        {
            StringBuilder strSqlString = new StringBuilder();

            string QueryCond1 = null;
            string QueryCond2 = null;

            string Tran_Time = cdvDate.Value.ToString("yyyy-MM-dd");
            
            udcTableForm tableForm = (udcTableForm)btnSort.BindingForm;

            QueryCond1 = tableForm.SelectedValueToQueryContainNull;            
            QueryCond2 = tableForm.SelectedValue2ToQueryContainNull;


            StringBuilder query1 = new StringBuilder();
            StringBuilder query2 = new StringBuilder();

            string sTime = DateTime.Parse(Tran_Time).AddDays(-1).ToString("yyyyMMdd");
            string sWeekTime = DateTime.Parse(Tran_Time).AddDays(-31).ToString("yyyyMMdd");
            string eTime = DateTime.Parse(Tran_Time).ToString("yyyyMMdd");

            strSqlString.Append("SELECT DATA_1" + "\n");
            for (int i = 0; i < dtPkg.Rows.Count; i++)
            {
                strSqlString.Append("   ,SUM(DATA" + i + ") AS DATA"+i+ "\n");
                //strSqlString.Append("   ,SUM(WEEKCNT" + i + ") AS WEEKCNT" + i + "\n");
                //strSqlString.Append("   ,SUM(DAYCNT" + i + ") AS DAYCNT" + i + "\n");

                strSqlString.Append("   ,ROUND(SUM(WEEKTAT" + i + ")/SUM(WEEKSHIP" + i + ") ,2)   AS WEEKCNT" + i + "\n");
                strSqlString.Append("   ,ROUND(SUM(DAYTAT" + i + ")/SUM(DAYSHIP" + i + ") ,2)   AS DAYCNT" + i + "\n");
            }
            strSqlString.Append("FROM (" + "\n");            

            strSqlString.Append("SELECT MAT_GRP_3, DATA_1" + "\n");
            for (int i = 0; i < dtPkg.Rows.Count; i++)
            {
                strSqlString.Append("   ,DECODE(MAT_GRP_3, '" + dtPkg.Rows[i][1].ToString() + "', SUM(DATA_2),0) AS DATA"+i+ "\n");
                //strSqlString.Append("   ,DECODE(MAT_GRP_3, '" + dtPkg.Rows[i][1].ToString() + "', SUM(WEEKCNT),0) AS WEEKCNT" +i+ "\n");
                //strSqlString.Append("   ,DECODE(MAT_GRP_3, '" + dtPkg.Rows[i][1].ToString() + "', SUM(DAYCNT),0) AS DAYCNT" +i+ "\n");

                //strSqlString.Append("   ,DECODE(MAT_GRP_3, '" + dtPkg.Rows[i][1].ToString() + "' ,ROUND(SUM(WEEKTAT )/SUM(WEEKSHIP ) ,2) ) AS WEEKCNT" + i + "\n");
                //strSqlString.Append("   ,DECODE(MAT_GRP_3, '" + dtPkg.Rows[i][1].ToString() + "' ,ROUND(SUM(DAYTAT)/SUM(DAYSHIP) ,2) ) AS DAYCNT" + i + "\n");


                strSqlString.Append("   ,DECODE(MAT_GRP_3, '" + dtPkg.Rows[i][1].ToString() + "', SUM(WEEKSHIP)) AS WEEKSHIP" + i + "\n");
                strSqlString.Append("   ,DECODE(MAT_GRP_3, '" + dtPkg.Rows[i][1].ToString() + "', SUM(WEEKTAT)) AS WEEKTAT" + i + "\n");
                strSqlString.Append("   ,DECODE(MAT_GRP_3, '" + dtPkg.Rows[i][1].ToString() + "', SUM(DAYSHIP)) AS DAYSHIP" + i + "\n");
                strSqlString.Append("   ,DECODE(MAT_GRP_3, '" + dtPkg.Rows[i][1].ToString() + "', SUM(DAYTAT)) AS DAYTAT" + i + "\n");

            }
            strSqlString.Append("FROM (" + "\n");
            //strSqlString.Append("       SELECT MAT_GRP_1_1 AS MAT_GRP_1, MAT_GRP_3, TAT.DATA_1, DATA_2, DAYCNT, null AS WEEKCNT" + "\n");

            strSqlString.Append("       SELECT MAT_GRP_1_1 AS MAT_GRP_1, MAT_GRP_3, TAT.DATA_1, DATA_2, DAYSHIP,DAYTAT, null AS WEEKSHIP ,NULL  AS WEEKTAT" + "\n");

            strSqlString.Append("       FROM(" + "\n");
            //strSqlString.Append("           SELECT (SELECT DATA_1 FROM MGCMTBLDAT WHERE TABLE_NAME = 'H_CUSTOMER' AND KEY_1 = MAT.MAT_GRP_1 AND ROWNUM=1) AS MAT_GRP_1_1, MAT.MAT_GRP_1, TAT.FACTORY, MAT.MAT_GRP_3, DATA_1, KEY_1" + "\n");
            //strSqlString.Append("               ,ROUND(SUM(DATA_TOT)/COUNT(*),2) AS DAYCNT" + "\n");

            strSqlString.Append("           SELECT (SELECT DATA_1 FROM MGCMTBLDAT WHERE TABLE_NAME = 'H_CUSTOMER' AND KEY_1 = MAT.MAT_GRP_1 AND ROWNUM=1) AS MAT_GRP_1_1, MAT.MAT_GRP_1, TAT.FACTORY, MAT.MAT_GRP_3, DATA_1, KEY_1" + "\n");
            strSqlString.Append("               ,SUM(DAYSHIP) DAYSHIP, SUM(DAYTAT) DAYTAT" + "\n");

            strSqlString.Append("           FROM  (" + "\n");
            strSqlString.Append("               SELECT  GCM.DATA_1,GCM.KEY_1,TAT.MAT_ID,TAT.FACTORY, OPER_DESC\n");
            //strSqlString.Append("                   , SUM(TAT.TOTAL_TIME)/60/60/24 AS DATA_TOT" + "\n");
            strSqlString.Append("                   , SUM(TAT.TOTAL_TAT_QTY) AS DAYTAT , SUM(A.SHIP_QTY) DAYSHIP  " + "\n");

            strSqlString.Append("                   , SUBSTR(GET_WORK_DATE(TAT.SHIP_TIME,'D'),LENGTH(GET_WORK_DATE(TAT.SHIP_TIME,'D'))-3,4) AS SHIP_TIME " + "\n");
            strSqlString.Append("               FROM    CSUMTATLOT@RPTTOMES TAT," + "\n");
            strSqlString.Append("                   MWIPOPRDEF OPR," + "\n");
            strSqlString.Append("                   MGCMTBLDAT GCM" + "\n");
            strSqlString.Append("                   ,(SELECT SUM(SHIP_QTY) SHIP_QTY,MAT_ID FROM  CSUMTATMAT@RPTTOMES GROUP BY MAT_ID) A " + "\n");

            strSqlString.Append("               WHERE   1=1" + "\n");
            strSqlString.Append("                   AND TAT.OPER=OPR.OPER" + "\n");
            strSqlString.Append("                   AND OPR.OPER_GRP_3 = GCM.KEY_1" + "\n");
            strSqlString.Append("                   AND GCM.FACTORY='" + cdvFactory.Text.ToString() + "'" + "\n");
            strSqlString.Append("                   AND GCM.TABLE_NAME='H_DEPARTMENT'" + "\n");
            strSqlString.Append("                   AND (OPR.OPER_GRP_2 " + cdvTeam.SelectedValueToQueryString + " OR OPR.OPER " + cdvTeam.SelectedValueToQueryString + ") \n");
            strSqlString.Append("                   AND (GCM.KEY_1 " + cdvPart.SelectedValueToQueryString + " OR TAT.OPER " + cdvPart.SelectedValueToQueryString + ") \n");
            strSqlString.Append("                   AND OPR.FACTORY ='" + cdvFactory.Text.ToString() + "'" + "\n");
            strSqlString.Append("                   AND TAT.FACTORY ='" + cdvFactory.Text.ToString() + "'" + "\n");
            strSqlString.Append("                   AND TAT.OPER = OPR.OPER" + "\n");
            strSqlString.Append("                   AND TAT.SHIP_TIME BETWEEN '" + sTime + "220000' AND '" + eTime + "215959' " + "\n");

            strSqlString.Append("                   AND TAT.MAT_ID = A.MAT_ID " + "\n");

            strSqlString.Append("               GROUP BY DATA_1,KEY_1,TAT.MAT_ID,TAT.FACTORY,OPR.OPER_DESC,TAT.SHIP_TIME" + "\n");
            if (cdvTeam.SelectedValueToQueryString.IndexOf("A0000") > 0)
            {
                strSqlString.Append("           UNION ALL " + "\n");
                strSqlString.Append("               SELECT '투입대기' AS DATA_1, 'A0000' AS KEY_1,TAT.MAT_ID,TAT.FACTORY, OPER_DESC " + "\n");
                //strSqlString.Append("               , SUM(TAT.TOTAL_TIME)/60/60/24 AS DATA_TOT" + "\n");
                strSqlString.Append("                   , SUM(TAT.TOTAL_TAT_QTY) AS DAYTAT , SUM(A.SHIP_QTY) DAYSHIP  " + "\n");
                strSqlString.Append("               , SUBSTR(GET_WORK_DATE(TAT.SHIP_TIME,'D'),LENGTH(GET_WORK_DATE(TAT.SHIP_TIME,'D'))-3,4) AS SHIP_TIME " + "\n");
                strSqlString.Append("               FROM     CSUMTATLOT@RPTTOMES TAT," + "\n");
                strSqlString.Append("               MWIPOPRDEF OPR" + "\n");
                strSqlString.Append("                   ,(SELECT SUM(SHIP_QTY) SHIP_QTY,MAT_ID FROM  CSUMTATMAT@RPTTOMES GROUP BY MAT_ID) A " + "\n");

                strSqlString.Append("               WHERE   1=1" + "\n");
                strSqlString.Append("               AND OPR.FACTORY ='" + cdvFactory.Text.ToString() + "'" + "\n");
                strSqlString.Append("               AND TAT.FACTORY ='" + cdvFactory.Text.ToString() + "'" + "\n");
                strSqlString.Append("               AND TAT.OPER=OPR.OPER" + "\n");
                strSqlString.Append("               AND TAT.OPER " + cdvPart.SelectedValueToQueryString + "\n");
                strSqlString.Append("               AND TAT.OPER = OPR.OPER" + "\n");
                strSqlString.Append("               AND TAT.SHIP_TIME BETWEEN '" + sTime + "220000' AND '" + eTime + "215959' " + "\n");

                strSqlString.Append("                   AND TAT.MAT_ID = A.MAT_ID " + "\n");
                strSqlString.Append("               GROUP BY '투입대기','A0000',TAT.MAT_ID,TAT.FACTORY,OPR.OPER_DESC,TAT.SHIP_TIME" + "\n");
            }
            if (cdvTeam.SelectedValueToQueryString.IndexOf("A0800") > 0)
            {
                strSqlString.Append("           UNION ALL " + "\n");
                strSqlString.Append("               SELECT 'IVIGATE' AS DATA_1, 'A0800' AS KEY_1,TAT.MAT_ID,TAT.FACTORY, OPER_DESC " + "\n");
                //strSqlString.Append("               , SUM(TAT.TOTAL_TIME)/60/60/24 AS DATA_TOT" + "\n");
                strSqlString.Append("                   , SUM(TAT.TOTAL_TAT_QTY) AS DAYTAT , SUM(A.SHIP_QTY) DAYSHIP  " + "\n");
                strSqlString.Append("               , SUBSTR(GET_WORK_DATE(TAT.SHIP_TIME,'D'),LENGTH(GET_WORK_DATE(TAT.SHIP_TIME,'D'))-3,4) AS SHIP_TIME " + "\n");
                strSqlString.Append("               FROM     CSUMTATLOT@RPTTOMES TAT," + "\n");
                strSqlString.Append("               MWIPOPRDEF OPR" + "\n");
                strSqlString.Append("                   ,(SELECT SUM(SHIP_QTY) SHIP_QTY,MAT_ID FROM  CSUMTATMAT@RPTTOMES GROUP BY MAT_ID) A " + "\n");

                strSqlString.Append("               WHERE   1=1" + "\n");
                strSqlString.Append("               AND OPR.FACTORY ='" + cdvFactory.Text.ToString() + "'" + "\n");
                strSqlString.Append("               AND TAT.FACTORY ='" + cdvFactory.Text.ToString() + "'" + "\n");
                strSqlString.Append("               AND TAT.OPER=OPR.OPER" + "\n");
                strSqlString.Append("               AND TAT.OPER " + cdvPart.SelectedValueToQueryString + "\n");
                strSqlString.Append("               AND TAT.OPER = OPR.OPER" + "\n");
                strSqlString.Append("               AND TAT.SHIP_TIME BETWEEN '" + sTime + "220000' AND '" + eTime + "215959' " + "\n");

                strSqlString.Append("                   AND TAT.MAT_ID = A.MAT_ID " + "\n");
                strSqlString.Append("               GROUP BY 'IVIGATE','A0800',TAT.MAT_ID,TAT.FACTORY,OPR.OPER_DESC,TAT.SHIP_TIME" + "\n");
            }
            if (cdvTeam.SelectedValueToQueryString.IndexOf("AZ010") > 0)
            {
                strSqlString.Append("           UNION ALL " + "\n");
                strSqlString.Append("               SELECT '출하대기' AS DATA_1, 'AZ010' AS KEY_1,TAT.MAT_ID,TAT.FACTORY, OPER_DESC " + "\n");
                //strSqlString.Append("               , SUM(TAT.TOTAL_TIME)/60/60/24 AS DATA_TOT" + "\n");
                strSqlString.Append("                   , SUM(TAT.TOTAL_TAT_QTY) AS DAYTAT , SUM(A.SHIP_QTY) DAYSHIP  " + "\n");
                strSqlString.Append("               , SUBSTR(GET_WORK_DATE(TAT.SHIP_TIME,'D'),LENGTH(GET_WORK_DATE(TAT.SHIP_TIME,'D'))-3,4) AS SHIP_TIME " + "\n");
                strSqlString.Append("               FROM     CSUMTATLOT@RPTTOMES TAT," + "\n");
                strSqlString.Append("               MWIPOPRDEF OPR" + "\n");
                strSqlString.Append("                   ,(SELECT SUM(SHIP_QTY) SHIP_QTY,MAT_ID FROM  CSUMTATMAT@RPTTOMES GROUP BY MAT_ID) A " + "\n");

                strSqlString.Append("               WHERE   1=1" + "\n");
                strSqlString.Append("               AND OPR.FACTORY ='" + cdvFactory.Text.ToString() + "'" + "\n");
                strSqlString.Append("               AND TAT.FACTORY ='" + cdvFactory.Text.ToString() + "'" + "\n");
                strSqlString.Append("               AND TAT.OPER=OPR.OPER" + "\n");
                strSqlString.Append("               AND TAT.OPER " + cdvPart.SelectedValueToQueryString + "\n");
                strSqlString.Append("               AND TAT.OPER = OPR.OPER" + "\n");
                strSqlString.Append("               AND TAT.SHIP_TIME BETWEEN '" + sTime + "220000' AND '" + eTime + "215959' " + "\n");

                strSqlString.Append("                   AND TAT.MAT_ID = A.MAT_ID " + "\n");
                strSqlString.Append("               GROUP BY '출하대기','AZ010',TAT.MAT_ID,TAT.FACTORY,OPR.OPER_DESC,TAT.SHIP_TIME" + "\n");
            }
            strSqlString.Append("           ) TAT," + "\n");
            strSqlString.Append("           MWIPMATDEF MAT" + "\n");
            strSqlString.Append("           WHERE MAT.MAT_GRP_1 " + udcWIPCondition1.SelectedValueToQueryString + "\n");
            strSqlString.Append("               AND MAT.FACTORY=TAT.FACTORY" + "\n");
            strSqlString.Append("               AND MAT.FACTORY='" + cdvFactory.Text.ToString() + "'" + "\n");
            strSqlString.Append("               AND MAT.MAT_ID=TAT.MAT_ID" + "\n");
            strSqlString.Append("               AND MAT.MAT_VER=1" + "\n");
            strSqlString.Append("               AND MAT.DELETE_FLAG=' '" + "\n");
            strSqlString.Append("           GROUP BY    MAT_GRP_1, TAT.FACTORY, MAT_GRP_3  , DATA_1, KEY_1" + "\n");
            strSqlString.Append("       ) TAT" + "\n");
            strSqlString.Append("       , MGCMTBLDAT GCM" + "\n");
            strSqlString.Append("        WHERE   1=1" + "\n");
            strSqlString.Append("           AND TAT.FACTORY=GCM.FACTORY" + "\n");
            strSqlString.Append("           AND GCM.FACTORY='" + cdvFactory.Text.ToString() + "'" + "\n");
            strSqlString.Append("           AND GCM.TABLE_NAME='H_RPT_TAT_OBJECT'" + "\n");
            strSqlString.Append("           AND GCM.KEY_1=(SELECT MAX(KEY_1) FROM MGCMTBLDAT WHERE FACTORY='" + cdvFactory.Text.ToString() + "' AND TABLE_NAME='H_RPT_TAT_OBJECT')" + "\n");
            strSqlString.Append("           AND TAT.KEY_1 = GCM.DATA_4" + "\n");
            strSqlString.Append("           AND TAT.MAT_GRP_1 = GCM.DATA_1" + "\n");
            strSqlString.Append("           AND TAT.MAT_GRP_3 = GCM.DATA_3" + "\n");
            strSqlString.Append("       UNION ALL" + "\n");
            //strSqlString.Append("       SELECT (SELECT DATA_1 FROM MGCMTBLDAT WHERE TABLE_NAME = 'H_CUSTOMER' AND KEY_1 = MAT.MAT_GRP_1 AND ROWNUM=1) AS MAT_GRP_1, MAT.MAT_GRP_3, DATA_1,null AS DATA_2, null AS DAYCNT," + "\n");
            //strSqlString.Append("           ROUND(SUM(DATA_TOT)/COUNT(*),2) AS WEEKCNT" + "\n");


            strSqlString.Append("       SELECT (SELECT DATA_1 FROM MGCMTBLDAT WHERE TABLE_NAME = 'H_CUSTOMER' AND KEY_1 = MAT.MAT_GRP_1 AND ROWNUM=1) AS MAT_GRP_1, MAT.MAT_GRP_3, DATA_1,null AS DATA_2, null AS DAYSHIP ,null AS DAYTAT ," + "\n");
            strSqlString.Append("           SUM(SHIP_QTY) WEEKSHIP, SUM(TAT_QTY) WEEKTAT" + "\n");
            strSqlString.Append("       FROM (" + "\n");
            strSqlString.Append("           SELECT  GCM.DATA_1,GCM.KEY_1,TAT.MAT_ID,TAT.FACTORY, OPER_DESC" + "\n");
            //strSqlString.Append("               , SUM(TAT.TOTAL_TIME)/60/60/24 AS DATA_TOT" + "\n");
            strSqlString.Append("              , SUM(TAT.TOTAL_TAT_QTY) AS TAT_QTY , SUM(A.SHIP_QTY) SHIP_QTY " + "\n");

            strSqlString.Append("               , SUBSTR(GET_WORK_DATE(TAT.SHIP_TIME,'D'),LENGTH(GET_WORK_DATE(TAT.SHIP_TIME,'D'))-3,4) AS SHIP_TIME " + "\n");
            strSqlString.Append("               , COUNT(*) AS LOT_COUNT" + "\n");
            strSqlString.Append("           FROM    CSUMTATLOT@RPTTOMES TAT," + "\n");
            strSqlString.Append("           MWIPOPRDEF OPR," + "\n");
            strSqlString.Append("           MGCMTBLDAT GCM" + "\n");
            strSqlString.Append("           ,(SELECT SUM(SHIP_QTY) SHIP_QTY,MAT_ID FROM  CSUMTATMAT@RPTTOMES GROUP BY MAT_ID) A " + "\n");
            strSqlString.Append("           WHERE   1=1" + "\n");
            strSqlString.Append("               AND TAT.OPER=OPR.OPER" + "\n");
            strSqlString.Append("               AND OPR.OPER_GRP_3 = GCM.KEY_1" + "\n");
            strSqlString.Append("               AND GCM.FACTORY='" + cdvFactory.Text.ToString() + "'" + "\n");
            strSqlString.Append("               AND GCM.TABLE_NAME='H_DEPARTMENT'" + "\n");
            strSqlString.Append("               AND OPR.OPER_GRP_2 " + cdvTeam.SelectedValueToQueryString + " \n");
            strSqlString.Append("               AND GCM.KEY_1 " + cdvPart.SelectedValueToQueryString + " \n");
            strSqlString.Append("               AND OPR.FACTORY ='" + cdvFactory.Text.ToString() + "'" + "\n");
            strSqlString.Append("               AND TAT.FACTORY ='" + cdvFactory.Text.ToString() + "'" + "\n");
            strSqlString.Append("               AND TAT.OPER = OPR.OPER" + "\n");
            strSqlString.Append("               AND TAT.SHIP_TIME BETWEEN '" + sWeekTime + "220000' AND '" + eTime + "215959' " + "\n");
            strSqlString.Append("               AND TAT.MAT_ID = A.MAT_ID" + "\n");

            strSqlString.Append("               GROUP BY DATA_1,KEY_1,TAT.MAT_ID,TAT.FACTORY,OPR.OPER_DESC,TAT.SHIP_TIME" + "\n");
            if (cdvTeam.SelectedValueToQueryString.IndexOf("A0000") > 0)
            {
                strSqlString.Append("           UNION ALL " + "\n");
                strSqlString.Append("               SELECT '투입대기' AS DATA_1, 'A0000' AS KEY_1,TAT.MAT_ID,TAT.FACTORY, OPER_DESC " + "\n");
                //strSqlString.Append("               , SUM(TAT.TOTAL_TIME)/60/60/24 AS DATA_TOT" + "\n");
                strSqlString.Append("              , SUM(TAT.TOTAL_TAT_QTY) AS TAT_QTY , SUM(A.SHIP_QTY) SHIP_QTY " + "\n");
                strSqlString.Append("               , SUBSTR(GET_WORK_DATE(TAT.SHIP_TIME,'D'),LENGTH(GET_WORK_DATE(TAT.SHIP_TIME,'D'))-3,4) AS SHIP_TIME " + "\n");
                strSqlString.Append("               , COUNT(*) AS LOT_COUNT" + "\n");
                strSqlString.Append("               FROM     CSUMTATLOT@RPTTOMES TAT," + "\n");
                strSqlString.Append("               MWIPOPRDEF OPR" + "\n");
                strSqlString.Append("               ,(SELECT SUM(SHIP_QTY) SHIP_QTY,MAT_ID FROM  CSUMTATMAT@RPTTOMES GROUP BY MAT_ID) A " + "\n");
                strSqlString.Append("               WHERE   1=1" + "\n");
                strSqlString.Append("               AND OPR.FACTORY ='" + cdvFactory.Text.ToString() + "'" + "\n");
                strSqlString.Append("               AND TAT.FACTORY ='" + cdvFactory.Text.ToString() + "'" + "\n");
                strSqlString.Append("               AND TAT.OPER=OPR.OPER" + "\n");
                strSqlString.Append("               AND TAT.OPER " + cdvPart.SelectedValueToQueryString + "\n");
                strSqlString.Append("               AND TAT.OPER = OPR.OPER" + "\n");
                strSqlString.Append("               AND TAT.SHIP_TIME BETWEEN '" + sWeekTime + "220000' AND '" + eTime + "215959' " + "\n");
                strSqlString.Append("               AND TAT.MAT_ID = A.MAT_ID" + "\n");
                strSqlString.Append("               GROUP BY '투입대기','A0000',TAT.MAT_ID,TAT.FACTORY,OPR.OPER_DESC,TAT.SHIP_TIME" + "\n");
            }
            if (cdvTeam.SelectedValueToQueryString.IndexOf("A0800") > 0)
            {
                strSqlString.Append("           UNION ALL " + "\n");
                strSqlString.Append("               SELECT 'IVIGATE' AS DATA_1, 'A0800' AS KEY_1,TAT.MAT_ID,TAT.FACTORY, OPER_DESC " + "\n");
                //strSqlString.Append("               , SUM(TAT.TOTAL_TIME)/60/60/24 AS DATA_TOT" + "\n");
                strSqlString.Append("              , SUM(TAT.TOTAL_TAT_QTY) AS TAT_QTY , SUM(A.SHIP_QTY) SHIP_QTY " + "\n");
                strSqlString.Append("               , SUBSTR(GET_WORK_DATE(TAT.SHIP_TIME,'D'),LENGTH(GET_WORK_DATE(TAT.SHIP_TIME,'D'))-3,4) AS SHIP_TIME " + "\n");
                strSqlString.Append("               , COUNT(*) AS LOT_COUNT" + "\n");
                strSqlString.Append("               FROM     CSUMTATLOT@RPTTOMES TAT," + "\n");
                strSqlString.Append("               MWIPOPRDEF OPR" + "\n");
                strSqlString.Append("               ,(SELECT SUM(SHIP_QTY) SHIP_QTY,MAT_ID FROM  CSUMTATMAT@RPTTOMES GROUP BY MAT_ID) A " + "\n");
                strSqlString.Append("               WHERE   1=1" + "\n");
                strSqlString.Append("               AND OPR.FACTORY ='" + cdvFactory.Text.ToString() + "'" + "\n");
                strSqlString.Append("               AND TAT.FACTORY ='" + cdvFactory.Text.ToString() + "'" + "\n");
                strSqlString.Append("               AND TAT.OPER=OPR.OPER" + "\n");
                strSqlString.Append("               AND TAT.OPER " + cdvPart.SelectedValueToQueryString + "\n");
                strSqlString.Append("               AND TAT.OPER = OPR.OPER" + "\n");
                strSqlString.Append("               AND TAT.SHIP_TIME BETWEEN '" + sWeekTime + "220000' AND '" + eTime + "215959' " + "\n");
                strSqlString.Append("               AND TAT.MAT_ID = A.MAT_ID" + "\n");
                strSqlString.Append("               GROUP BY 'IVIGATE','A0800',TAT.MAT_ID,TAT.FACTORY,OPR.OPER_DESC,TAT.SHIP_TIME" + "\n");
            }
            if (cdvTeam.SelectedValueToQueryString.IndexOf("AZ010") > 0)
            {
                strSqlString.Append("           UNION ALL " + "\n");
                strSqlString.Append("               SELECT '출하대기' AS DATA_1, 'AZ010' AS KEY_1,TAT.MAT_ID,TAT.FACTORY, OPER_DESC " + "\n");
                //strSqlString.Append("               , SUM(TAT.TOTAL_TIME)/60/60/24 AS DATA_TOT" + "\n");
                strSqlString.Append("              , SUM(TAT.TOTAL_TAT_QTY) AS TAT_QTY , SUM(A.SHIP_QTY) SHIP_QTY " + "\n");
                strSqlString.Append("               , SUBSTR(GET_WORK_DATE(TAT.SHIP_TIME,'D'),LENGTH(GET_WORK_DATE(TAT.SHIP_TIME,'D'))-3,4) AS SHIP_TIME " + "\n");
                strSqlString.Append("               , COUNT(*) AS LOT_COUNT" + "\n");
                strSqlString.Append("               FROM     CSUMTATLOT@RPTTOMES TAT," + "\n");
                strSqlString.Append("               MWIPOPRDEF OPR" + "\n");
                strSqlString.Append("               ,(SELECT SUM(SHIP_QTY) SHIP_QTY,MAT_ID FROM  CSUMTATMAT@RPTTOMES GROUP BY MAT_ID) A " + "\n");
                strSqlString.Append("               WHERE   1=1" + "\n");
                strSqlString.Append("               AND OPR.FACTORY ='" + cdvFactory.Text.ToString() + "'" + "\n");
                strSqlString.Append("               AND TAT.FACTORY ='" + cdvFactory.Text.ToString() + "'" + "\n");
                strSqlString.Append("               AND TAT.OPER=OPR.OPER" + "\n");
                strSqlString.Append("               AND TAT.OPER " + cdvPart.SelectedValueToQueryString + "\n");
                strSqlString.Append("               AND TAT.OPER = OPR.OPER" + "\n");
                strSqlString.Append("               AND TAT.SHIP_TIME BETWEEN '" + sWeekTime + "220000' AND '" + eTime + "215959' " + "\n");
                strSqlString.Append("               AND TAT.MAT_ID = A.MAT_ID" + "\n");
                strSqlString.Append("               GROUP BY '출하대기','AZ010',TAT.MAT_ID,TAT.FACTORY,OPR.OPER_DESC,TAT.SHIP_TIME" + "\n");
            }
            strSqlString.Append("       ) TAT," + "\n");
            strSqlString.Append("       MWIPMATDEF MAT" + "\n");
            strSqlString.Append("       WHERE MAT.MAT_GRP_1 " + udcWIPCondition1.SelectedValueToQueryString + "\n");
            strSqlString.Append("           AND MAT.FACTORY=TAT.FACTORY" + "\n");
            strSqlString.Append("           AND MAT.FACTORY='" + cdvFactory.Text.ToString() + "'" + "\n");
            strSqlString.Append("           AND MAT.MAT_ID=TAT.MAT_ID" + "\n");
            strSqlString.Append("           AND MAT.MAT_VER=1" + "\n");
            strSqlString.Append("           AND MAT.DELETE_FLAG=' '" + "\n");
            strSqlString.Append("       GROUP BY     MAT_GRP_1, MAT_GRP_3  , DATA_1" + "\n");
            strSqlString.Append("       )" + "\n");
            strSqlString.Append(" GROUP BY     MAT_GRP_3  , DATA_1" + "\n");
            strSqlString.Append("       )" + "\n");
            strSqlString.Append(" GROUP BY  DATA_1" + "\n");
            strSqlString.Append(" ORDER BY  DATA_1" + "\n"); 

            System.Windows.Forms.Clipboard.SetText(strSqlString.ToString()); 

            return strSqlString.ToString();
        }
        #endregion

        #region EVENT 처리
        private void btnView_Click(object sender, EventArgs e)
        {
            DataTable dt = null;
            if (CheckField() == false) return;
            
            // 공정 코드 가져오기
            //dtOper = CmnFunction.oComm.GetFuncDataTable("DYNAMIC", MakeOperTable());

            try
            {
                LoadingPopUp.LoadIngPopUpShow(this);             
                this.Refresh();
                dtPkg = CmnFunction.oComm.GetFuncDataTable("DYNAMIC", PkgSqlString());                                  
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

                //그루핑 순서 1순위만 SubTotal을 사용하기 위해서 첫번째 값을 가져옴
                //int sub = ((udcTableForm)(this.btnSort.BindingForm)).GetCheckedValue(2);

                // 0값 표시 설정
              

                spdData.isShowZero = true;


               // 2. 칼럼 고정(필요하다면..)
                spdData.Sheets[0].FrozenColumnCount = 0;
                //spdData.DataSource
                //1.Griid 합계 표시
               ///// int[] rowType = spdData.RPT_DataBindingWithSubTotal(dt, 0, 1, 0, null, null, btnSort);

                //int[] rowType = spdData.RPT_DataBindingWithSubTotal(dt, 0, sub + 1, 13, null, null, btnSort, true);
                //3. Total부분 셀머지
       //         spdData.RPT_FillDataSelectiveCells("Total", 0, 13, 0, 1, true, Align.Center, VerticalAlign.Center);
                                
                //4. Column Auto Fit
                spdData.RPT_AutoFit(false);

                //spdData.ActiveSheet.Columns[9].AllowAutoSort = true;
                //spdData.ActiveSheet.Columns[11].AllowAutoSort = true;
                //spdData.ActiveSheet.Columns[12].AllowAutoSort = true;
                //spdData.ActiveSheet.Columns[13].AllowAutoSort = true;

                //5. 데이타가 0인 부분은 제거(Add by John. 2009.1.13)
                //spdData.RPT_RemoveZeroColumn(15, 1);                

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

        //private void cdvFactory_ValueSelectedItemChanged(object sender, Miracom.UI.MCCodeViewSelChanged_EventArgs e)
        //{
        //    this.SetFactory(cdvFactory.txtValue);                        

        // }
         #endregion



        #region cdvFactory_ValueSelectedItemChanged

        /// <summary>
        /// cdvFactory_ValueSelectedItemChanged
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cdvFactory_ValueSelectedItemChanged(object sender, MCCodeViewSelChanged_EventArgs e)
        {
            this.SetFactory(cdvFactory.txtValue);
            this.cdvTeam.sFactory = cdvFactory.txtValue;

            string strQuery = string.Empty;

            strQuery += "SELECT KEY_1 CODE, DATA_1 DATA" + "\n";
            strQuery += "  FROM MGCMTBLDAT " + "\n";
            strQuery += "  WHERE FACTORY " + cdvFactory.SelectedValueToQueryString + "\n";
            strQuery += "  AND TABLE_NAME = 'H_DEPARTMENT' " + "\n";
            strQuery += "  AND KEY_1 IN ('650','660')" + "\n";
            strQuery += "  UNION ALL" + "\n";
            strQuery += "  SELECT 'A0000' CODE, '투입대기' DATA FROM DUAL" + "\n";
            strQuery += "  UNION ALL" + "\n";
            strQuery += "  SELECT 'A0800' CODE, 'IVI Gate' DATA FROM DUAL" + "\n";
            strQuery += "  UNION ALL" + "\n";
            strQuery += "  SELECT 'AZ010' CODE, '출하대기' DATA FROM DUAL" + "\n";
            strQuery += "ORDER BY DATA, CODE " + "\n";
            System.Windows.Forms.Clipboard.SetText(strQuery.ToString());

            if (cdvFactory.txtValue != "")
                cdvTeam.sDynamicQuery = strQuery;
            else
                cdvTeam.sDynamicQuery = "";


        }

        #endregion


        private void cdvPart_ValueButtonPress(object sender, EventArgs e)
        {
            cdvPart.SetChangedFlag(true);
            cdvPart.Text = "";

            string strQuery = string.Empty;

            strQuery += "SELECT DISTINCT B.KEY_1 CODE, B.DATA_1 DATA" + "\n";
            strQuery += "  FROM MWIPOPRDEF A, MGCMTBLDAT B " + "\n";
            strQuery += "  WHERE A.FACTORY " + cdvFactory.SelectedValueToQueryString + "\n";
            strQuery += "  AND A.FACTORY = B.FACTORY" + "\n";
            strQuery += "  AND B.TABLE_NAME = 'H_DEPARTMENT' " + "\n";
            strQuery += "  AND A.OPER_GRP_2 " + cdvTeam.SelectedValueToQueryString + "\n";
            strQuery += "  AND B.KEY_1 = A.OPER_GRP_3 " + "\n";
            if (cdvTeam.SelectedValueToQueryString.IndexOf("A0000") > 0)
            {
                strQuery += "  UNION ALL" + "\n";
                strQuery += "  SELECT 'A0000' CODE, '투입대기' DATA FROM DUAL" + "\n";
            }
            if (cdvTeam.SelectedValueToQueryString.IndexOf("A0800") > 0)
            {
                strQuery += "  UNION ALL" + "\n";
                strQuery += "  SELECT 'A0800' CODE, 'IVI Gate' DATA FROM DUAL" + "\n";
            }
            if (cdvTeam.SelectedValueToQueryString.IndexOf("AZ010") > 0)
            {
            strQuery += "  UNION ALL" + "\n";
            strQuery += "  SELECT 'AZ010' CODE, '출하대기' DATA FROM DUAL" + "\n";
            }
            strQuery += "ORDER BY DATA, CODE " + "\n";

            if (GlobalVariable.gsUserGroup == "ADMIN_GROUP" || GlobalVariable.gsUserGroup == "HANA_ADMIN_GROUP")
            {
                System.Windows.Forms.Clipboard.SetText(strQuery.ToString());
            }

            if (cdvFactory.txtValue != "")
                cdvPart.sDynamicQuery = strQuery;
            else
                cdvPart.sDynamicQuery = "";
        }


     }
}
