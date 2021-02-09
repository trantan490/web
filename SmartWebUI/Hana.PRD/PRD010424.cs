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
    public partial class PRD010424 : Miracom.SmartWeb.UI.Controls.udcCUSReport002
    {
        int iMonthDay = 0;
        /// <summary>
        /// 클  래  스: PRD010424<br/>
        /// 클래스요약: 삼우엠스 결산<br/>
        /// 작  성  자: 하나마이크론 임종우<br/>
        /// 최초작성일: 2016-11-15<br/>
        /// 상세  설명: 삼우엠스 결산(박형순 요청)<br/>
        /// 변경  내용: <br/> 
        /// </summary>
        public PRD010424()
        {
            InitializeComponent();
            cdvDate.Value = DateTime.Now.AddDays(-1);

            SortInit();
            GridColumnInit();
            this.cdvFactory.sFactory = GlobalVariable.gsAssyDefaultFactory;
            this.SetFactory(GlobalVariable.gsAssyDefaultFactory);
            cdvFactory.Text = GlobalVariable.gsAssyDefaultFactory;                        
        }

        #region 초기화 및 유효성 검사
        /// <summary 1. 유효성 검사>
        /// <remarks></remarks>
        /// </summary>
        /// <returns></returns>
        private Boolean CheckField()
        {
            if (Convert.ToInt32(cdvDate.SelectedValue()) >= Convert.ToInt32(DateTime.Now.ToString("yyyyMMdd")))
            {
                CmnFunction.ShowMsgBox(RptMessages.GetMessage("STD057", GlobalVariable.gcLanguage));
                return false;
            }
            return true;
        }

        /// <summary>
        /// 2. 헤더 생성
        /// </summary>
        private void GridColumnInit()
        {
            iMonthDay = Convert.ToInt32(cdvDate.Value.ToString("yyyyMMdd").Substring(6, 2));

            try
            {
                spdData.RPT_ColumnInit();

                spdData.RPT_AddBasicColumn("CUSTOMER", 0, 0, Visibles.False, Frozen.True, Align.Center, Merge.True, Formatter.String, 80);
                spdData.RPT_AddBasicColumn("MAJOR CODE", 0, 1, Visibles.False, Frozen.True, Align.Left, Merge.True, Formatter.String, 70);
                spdData.RPT_AddBasicColumn("FAMILY", 0, 2, Visibles.False, Frozen.True, Align.Center, Merge.True, Formatter.String, 80);
                spdData.RPT_AddBasicColumn("PACKAGE", 0, 3, Visibles.False, Frozen.True, Align.Center, Merge.True, Formatter.String, 80);
                spdData.RPT_AddBasicColumn("TYPE1", 0, 4, Visibles.False, Frozen.True, Align.Center, Merge.True, Formatter.String, 70);
                spdData.RPT_AddBasicColumn("TYPE2", 0, 5, Visibles.False, Frozen.True, Align.Center, Merge.True, Formatter.String, 70);
                spdData.RPT_AddBasicColumn("LD COUNT", 0, 6, Visibles.False, Frozen.True, Align.Center, Merge.True, Formatter.String, 70);
                spdData.RPT_AddBasicColumn("PKG CODE", 0, 7, Visibles.False, Frozen.True, Align.Center, Merge.True, Formatter.String, 70);
                spdData.RPT_AddBasicColumn("DENSITY", 0, 8, Visibles.False, Frozen.True, Align.Center, Merge.True, Formatter.String, 70);
                spdData.RPT_AddBasicColumn("GENERATION", 0, 9, Visibles.False, Frozen.True, Align.Left, Merge.True, Formatter.String, 70);
                spdData.RPT_AddBasicColumn("PIN TYPE", 0, 10, Visibles.False, Frozen.True, Align.Left, Merge.True, Formatter.String, 150);
                spdData.RPT_AddBasicColumn("PRODUCT", 0, 11, Visibles.False, Frozen.True, Align.Left, Merge.True, Formatter.String, 150);
                spdData.RPT_AddBasicColumn("MODEL", 0, 12, Visibles.True, Frozen.True, Align.Left, Merge.True, Formatter.String, 150);
                spdData.RPT_AddBasicColumn("Classification", 0, 13, Visibles.True, Frozen.True, Align.Left, Merge.True, Formatter.String, 120);

                //선택된 달의 일수에 맞게 컬럼 표시
                for (int i = 1; i <= iMonthDay; i++)
                {
                    spdData.RPT_AddBasicColumn(string.Format("{0}.{1:D2}", cdvDate.Value.ToString("MM"), i), 0, 14 + i - 1, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 70);
                }
                
                spdData.RPT_ColumnConfigFromTable(btnSort);                
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

            ((udcTableForm)(this.btnSort.BindingForm)).AddRow("CUSTOMER", "MAT_GRP_1", "NVL((SELECT DATA_1 FROM MGCMTBLDAT WHERE TABLE_NAME = 'H_CUSTOMER' AND KEY_1 = MAT_GRP_1 AND ROWNUM=1), '-') AS CUSTOMER", "A.MAT_GRP_1", false);
            ((udcTableForm)(this.btnSort.BindingForm)).AddRow("MAJOR CODE", "MAT_GRP_9", "MAT_GRP_9 AS MAJOR_CODE", "A.MAT_GRP_9", false);
            ((udcTableForm)(this.btnSort.BindingForm)).AddRow("FAMILY", "MAT_GRP_2", "MAT_GRP_2 AS FAMILY", "A.MAT_GRP_2", false);
            ((udcTableForm)(this.btnSort.BindingForm)).AddRow("PACKAGE", "MAT_GRP_10", "MAT_GRP_10 AS PACKAGE", "A.MAT_GRP_10", false);
            ((udcTableForm)(this.btnSort.BindingForm)).AddRow("TYPE1", "MAT_GRP_4", "MAT_GRP_4 AS TYPE1", "A.MAT_GRP_4", false);
            ((udcTableForm)(this.btnSort.BindingForm)).AddRow("TYPE2", "MAT_GRP_5", "MAT_GRP_5 AS TYPE2", "A.MAT_GRP_5", false);
            ((udcTableForm)(this.btnSort.BindingForm)).AddRow("LD COUNT", "MAT_GRP_6", "MAT_GRP_6 AS LD_COUNT", "A.MAT_GRP_6", false);
            ((udcTableForm)(this.btnSort.BindingForm)).AddRow("PKG CODE", "MAT_CMF_11", "MAT_CMF_11 AS PKG_CODE", "A.MAT_CMF_11", false);
            ((udcTableForm)(this.btnSort.BindingForm)).AddRow("DENSITY", "MAT_GRP_7", "MAT_GRP_7 AS DENSITY", "A.MAT_GRP_7", false);
            ((udcTableForm)(this.btnSort.BindingForm)).AddRow("GENERATION", "MAT_GRP_8", "MAT_GRP_8 AS GENERATION", "A.MAT_GRP_8", false);
            ((udcTableForm)(this.btnSort.BindingForm)).AddRow("PIN TYPE", "MAT_CMF_10", "MAT_CMF_10 AS PIN_TYPE", "A.MAT_CMF_10", false);
            ((udcTableForm)(this.btnSort.BindingForm)).AddRow("PRODUCT", "MAT_ID", "MAT_ID AS PRODUCT", "A.MAT_ID", false);
            ((udcTableForm)(this.btnSort.BindingForm)).AddRow("MODEL", "MAT_CMF_8", "MAT_CMF_8 AS MODEL", "A.MAT_CMF_8", true); 
          
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

            string QueryCond1;
            string QueryCond2;
            string QueryCond3;
            string sMonth;
            string sStartday;
            string sEndday;
            string sEnddayTomorrow;
            
            udcTableForm tableForm = (udcTableForm)btnSort.BindingForm;
            QueryCond1 = tableForm.SelectedValueToQueryContainNull;
            QueryCond2 = tableForm.SelectedValue2ToQueryContainNull;
            QueryCond3 = tableForm.SelectedValue3ToQueryContainNull;



            sMonth = cdvDate.Value.ToString("yyyyMM");
            sStartday = sMonth + "01";
            sEndday = cdvDate.Value.ToString("yyyyMMdd"); // 조회 월의 마지막 일
            sEnddayTomorrow = cdvDate.Value.AddDays(1).ToString("yyyyMMdd");

            strSqlString.AppendFormat("SELECT {0}, GUBUN " + "\n", QueryCond2);

            for (int i = 1; i <= iMonthDay; i++)
            {
                strSqlString.Append(string.Format("     , SUM(DECODE(WORK_DATE,'{0}{1:D2}', QTY, 0)) AS DAY{2:D2} \n", sMonth, i, i));
            }

            strSqlString.Append("  FROM (" + "\n");
            strSqlString.Append("        SELECT " + QueryCond1 + "\n");
            strSqlString.Append("             , WORK_DATE " + "\n");
            strSqlString.Append("             , DECODE(SEQ, 1, 'BOH', 2, '입고 (IN)', 3, '반품 (R-IN)', 4, 'AO (OUT)', 5, '반품입고 (R-OUT)', 6, 'LOSS', 7, 'EOH') AS GUBUN " + "\n");
            strSqlString.Append("             , DECODE(SEQ, 1, BOH_QTY, 2, RCV_QTY, 3, R_IN_QTY, 4, SHP_QTY, 5, R_OUT_QTY, 6, LOSS_QTY, 7, EOH_QTY) AS QTY" + "\n");
            strSqlString.Append("          FROM (" + "\n");
            strSqlString.Append("                SELECT " + QueryCond1 + "\n");            
            strSqlString.Append("                     , WORK_DATE" + "\n");
            strSqlString.Append("                     , BOH_QTY" + "\n");
            strSqlString.Append("                     , RCV_QTY" + "\n");
            strSqlString.Append("                     , R_IN_QTY" + "\n");
            strSqlString.Append("                     , SHP_QTY" + "\n");
            strSqlString.Append("                     , R_OUT_QTY" + "\n");
            strSqlString.Append("                     , BOH_QTY + RCV_QTY + R_IN_QTY - SHP_QTY - R_OUT_QTY - EOH_QTY AS LOSS_QTY" + "\n");
            strSqlString.Append("                     , EOH_QTY" + "\n");
            strSqlString.Append("                  FROM (" + "\n");
            strSqlString.Append("                        SELECT " + QueryCond3 + "\n");
            strSqlString.Append("                             , A.WORK_DATE" + "\n");
            strSqlString.Append("                             , SUM(NVL(B.BOH_QTY,0)) AS BOH_QTY" + "\n");
            strSqlString.Append("                             , SUM(NVL(C.RCV_QTY,0)+NVL(D.PC_IN_QTY,0)) AS RCV_QTY" + "\n");
            strSqlString.Append("                             , SUM(NVL(C.R_IN_QTY,0)) AS R_IN_QTY" + "\n");
            strSqlString.Append("                             , SUM(NVL(C.SHP_QTY,0)) AS SHP_QTY" + "\n");
            strSqlString.Append("                             , SUM(NVL(C.R_OUT_QTY,0)+NVL(D.PC_OUT_QTY,0)) AS R_OUT_QTY " + "\n");
            strSqlString.Append("                             , SUM(NVL(D.PC_IN_QTY,0)) AS PC_IN_QTY " + "\n");
            strSqlString.Append("                             , SUM(NVL(D.PC_OUT_QTY,0)) AS PC_OUT_QTY" + "\n");
            strSqlString.Append("                             , LEAD(SUM(NVL(BOH_QTY,0))) OVER(PARTITION BY " + QueryCond3 + " ORDER BY A.WORK_DATE) AS EOH_QTY " + "\n");
            strSqlString.Append("                             , SUM(SUM(NVL(B.BOH_QTY,0)+NVL(C.RCV_QTY,0)+NVL(C.R_IN_QTY,0)+NVL(C.SHP_QTY,0)+NVL(C.R_OUT_QTY,0)+NVL(D.PC_IN_QTY,0)+NVL(D.PC_OUT_QTY,0))) OVER(PARTITION BY A.MAT_CMF_8 ORDER BY A.WORK_DATE ROWS BETWEEN UNBOUNDED PRECEDING AND UNBOUNDED FOLLOWING) AS TTL_QTY" + "\n");
            strSqlString.Append("                          FROM (" + "\n");
            strSqlString.Append("                                SELECT *" + "\n");
            strSqlString.Append("                                  FROM MWIPMATDEF A" + "\n");
            strSqlString.Append("                                     , (" + "\n");
            strSqlString.Append("                                        SELECT TO_CHAR(TO_DATE('" + sStartday + "', 'YYYYMMDD') + (ROWNUM-1), 'YYYYMMDD') AS WORK_DATE" + "\n");
            strSqlString.Append("                                          FROM DUAL" + "\n");
            strSqlString.Append("                                         CONNECT BY LEVEL <= TO_CHAR(LAST_DAY(TO_DATE('" + sStartday + "', 'YYYYMMDD')), 'DD') + 1" + "\n");
            strSqlString.Append("                                       ) B" + "\n");
            strSqlString.Append("                                 WHERE 1=1" + "\n");
            strSqlString.Append("                                   AND FACTORY = '" + cdvFactory.Text + "'" + "\n");
            strSqlString.Append("                                   AND DELETE_FLAG = ' '" + "\n");
            strSqlString.Append("                                   AND MAT_ID LIKE 'CC%' " + "\n");

            if (txtProduct.Text.Trim() != "%" && txtProduct.Text.Trim() != "")
                strSqlString.Append("                                   AND MAT_ID LIKE '" + txtProduct.Text + "'" + "\n");

            #region 상세 조회에 따른 SQL문 생성
            if (udcWIPCondition1.Text != "ALL" && udcWIPCondition1.Text != "")
                strSqlString.AppendFormat("                                   AND MAT_GRP_1 {0}" + "\n", udcWIPCondition1.SelectedValueToQueryString);

            if (udcWIPCondition2.Text != "ALL" && udcWIPCondition2.Text != "")
                strSqlString.AppendFormat("                                   AND MAT_GRP_2 {0} " + "\n", udcWIPCondition2.SelectedValueToQueryString);

            if (udcWIPCondition3.Text != "ALL" && udcWIPCondition3.Text != "")
                strSqlString.AppendFormat("                                   AND MAT_GRP_3 {0} " + "\n", udcWIPCondition3.SelectedValueToQueryString);

            if (udcWIPCondition4.Text != "ALL" && udcWIPCondition4.Text != "")
                strSqlString.AppendFormat("                                   AND MAT_GRP_4 {0} " + "\n", udcWIPCondition4.SelectedValueToQueryString);

            if (udcWIPCondition5.Text != "ALL" && udcWIPCondition5.Text != "")
                strSqlString.AppendFormat("                                   AND MAT_GRP_5 {0} " + "\n", udcWIPCondition5.SelectedValueToQueryString);

            if (udcWIPCondition6.Text != "ALL" && udcWIPCondition6.Text != "")
                strSqlString.AppendFormat("                                   AND MAT_GRP_6 {0} " + "\n", udcWIPCondition6.SelectedValueToQueryString);

            if (udcWIPCondition7.Text != "ALL" && udcWIPCondition7.Text != "")
                strSqlString.AppendFormat("                                   AND MAT_GRP_7 {0} " + "\n", udcWIPCondition7.SelectedValueToQueryString);

            if (udcWIPCondition8.Text != "ALL" && udcWIPCondition8.Text != "")
                strSqlString.AppendFormat("                                   AND MAT_GRP_8 {0} " + "\n", udcWIPCondition8.SelectedValueToQueryString);

            if (udcWIPCondition9.Text != "ALL" && udcWIPCondition9.Text != "")
                strSqlString.AppendFormat("                                   AND MAT_GRP_9 {0} " + "\n", udcWIPCondition9.SelectedValueToQueryString);
            #endregion

            strSqlString.Append("                               ) A" + "\n");
            strSqlString.Append("                             , (" + "\n");
            strSqlString.Append("                                SELECT WORK_DATE" + "\n");
            strSqlString.Append("                                     , MAT_ID " + "\n");
            strSqlString.Append("                                     , SUM(EOH_QTY_1) AS BOH_QTY" + "\n");
            strSqlString.Append("                                  FROM RSUMWIPEOH" + "\n");
            strSqlString.Append("                                 WHERE 1=1" + "\n");
            strSqlString.Append("                                   AND FACTORY = '" + cdvFactory.Text + "'" + "\n");
            strSqlString.Append("                                   AND LOT_TYPE = 'W'" + "\n");
            strSqlString.Append("                                   AND WORK_DATE BETWEEN '" + sStartday + "' AND '" + sEnddayTomorrow + "'" + "\n");
            strSqlString.Append("                                   AND MAT_ID LIKE 'CC%'" + "\n");
            strSqlString.Append("                                   AND SHIFT = '1'" + "\n");
            strSqlString.Append("                                   AND CM_KEY_3 LIKE 'P%' " + "\n");
            strSqlString.Append("                                 GROUP BY WORK_DATE, MAT_ID" + "\n");
            strSqlString.Append("                               ) B" + "\n");
            strSqlString.Append("                             , (" + "\n");
            strSqlString.Append("                                SELECT WORK_DATE " + "\n");
            strSqlString.Append("                                     , MAT_ID " + "\n");
            strSqlString.Append("                                     , SUM(CASE WHEN OPER = 'A0000' THEN (S1_FAC_IN_QTY_1+S2_FAC_IN_QTY_1+S3_FAC_IN_QTY_1+S4_FAC_IN_QTY_1) ELSE 0 END) AS RCV_QTY" + "\n");
            strSqlString.Append("                                     , SUM(CASE WHEN OPER = 'AZ010' THEN (S1_FAC_IN_QTY_1+S2_FAC_IN_QTY_1+S3_FAC_IN_QTY_1+S4_FAC_IN_QTY_1) ELSE 0 END) AS R_IN_QTY" + "\n");
            strSqlString.Append("                                     , SUM(CASE WHEN FACTORY <> 'RETURN' THEN (S1_FAC_OUT_QTY_1+S2_FAC_OUT_QTY_1+S3_FAC_OUT_QTY_1+S4_FAC_OUT_QTY_1) ELSE 0 END) AS SHP_QTY     " + "\n");
            strSqlString.Append("                                     , SUM(CASE WHEN FACTORY = 'RETURN' THEN (S1_FAC_OUT_QTY_1+S2_FAC_OUT_QTY_1+S3_FAC_OUT_QTY_1+S4_FAC_OUT_QTY_1) ELSE 0 END) AS R_OUT_QTY" + "\n");
            strSqlString.Append("                                  FROM CSUMFACMOV" + "\n");
            strSqlString.Append("                                 WHERE 1=1" + "\n");
            strSqlString.Append("                                   AND CM_KEY_1 = '" + cdvFactory.Text + "' " + "\n");
            strSqlString.Append("                                   AND WORK_DATE BETWEEN '" + sStartday + "' AND '" + sEndday + "'" + "\n");
            strSqlString.Append("                                   AND MAT_ID LIKE 'CC%'" + "\n");
            strSqlString.Append("                                   AND CM_KEY_3 LIKE 'P%'" + "\n");
            strSqlString.Append("                                 GROUP BY WORK_DATE, MAT_ID" + "\n");
            strSqlString.Append("                               ) C " + "\n");
            strSqlString.Append("                             , (" + "\n");
            strSqlString.Append("                                SELECT WORK_DATE" + "\n");
            strSqlString.Append("                                     , DECODE(RN, 1, MAT_ID, OLD_MAT_ID) AS MAT_ID" + "\n");
            strSqlString.Append("                                     , SUM(DECODE(RN, 1, QTY_1, 0)) AS PC_IN_QTY" + "\n");
            strSqlString.Append("                                     , SUM(DECODE(RN, 1, 0, QTY_1)) AS PC_OUT_QTY " + "\n");
            strSqlString.Append("                                  FROM (" + "\n");
            strSqlString.Append("                                        SELECT TO_CHAR(TO_DATE(TRAN_TIME, 'YYYYMMDDHH24MISS') + 2/24, 'YYYYMMDD') AS WORK_DATE, MAT_ID, OLD_MAT_ID, SUM(QTY_1) AS QTY_1" + "\n");
            strSqlString.Append("                                          FROM MWIPADTHIS@RPTTOMES" + "\n");
            strSqlString.Append("                                         WHERE LOT_TYPE = 'W'" + "\n");
            strSqlString.Append("                                           AND FACTORY = '" + cdvFactory.Text + "' " + "\n");
            strSqlString.Append("                                           AND TRAN_CODE = 'ADAPT'" + "\n");
            strSqlString.Append("                                           AND TRAN_TIME BETWEEN '" + sStartday + "060000' AND '" + sEnddayTomorrow + "055959'" + "\n");
            strSqlString.Append("                                           AND MAT_ID LIKE 'CC%'" + "\n");
            strSqlString.Append("                                           AND TRAN_CMF_5 = 'ADAPT-PART_CHANGE'" + "\n");
            strSqlString.Append("                                           AND HIST_DEL_FLAG = ' '" + "\n");
            strSqlString.Append("                                           AND OPER LIKE 'A%'" + "\n");
            strSqlString.Append("                                           AND LOT_CMF_5 LIKE 'P%'" + "\n");
            strSqlString.Append("                                         GROUP BY TO_CHAR(TO_DATE(TRAN_TIME, 'YYYYMMDDHH24MISS') + 2/24, 'YYYYMMDD'), MAT_ID, OLD_MAT_ID " + "\n");
            strSqlString.Append("                                       ) A" + "\n");
            strSqlString.Append("                                     , (" + "\n");
            strSqlString.Append("                                        SELECT LEVEL AS RN FROM DUAL CONNECT BY LEVEL = 2" + "\n");
            strSqlString.Append("                                       ) B" + "\n");
            strSqlString.Append("                                 GROUP BY WORK_DATE, DECODE(RN, 1, MAT_ID, OLD_MAT_ID)" + "\n");
            strSqlString.Append("                               ) D" + "\n");
            strSqlString.Append("                         WHERE 1=1" + "\n");
            strSqlString.Append("                           AND A.WORK_DATE = B.WORK_DATE(+)" + "\n");
            strSqlString.Append("                           AND A.WORK_DATE = C.WORK_DATE(+)" + "\n");
            strSqlString.Append("                           AND A.WORK_DATE = D.WORK_DATE(+)" + "\n");
            strSqlString.Append("                           AND A.MAT_ID = B.MAT_ID(+)" + "\n");
            strSqlString.Append("                           AND A.MAT_ID = C.MAT_ID(+)" + "\n");
            strSqlString.Append("                           AND A.MAT_ID = D.MAT_ID(+)" + "\n");
            strSqlString.Append("                           AND A.FACTORY = '" + GlobalVariable.gsAssyDefaultFactory + "'" + "\n");
            strSqlString.Append("                           AND A.DELETE_FLAG = ' '" + "\n");
            strSqlString.Append("                           AND A.MAT_ID LIKE 'CC%'" + "\n");
            strSqlString.Append("                         GROUP BY " + QueryCond3 + ", A.WORK_DATE " + "\n");
            strSqlString.Append("                       ) " + "\n");
            strSqlString.Append("                 WHERE 1=1" + "\n");
            strSqlString.Append("                   AND TTL_QTY > 0" + "\n");
            strSqlString.Append("                   AND WORK_DATE BETWEEN '" + sStartday + "' AND '" + sEndday + "'" + "\n");
            strSqlString.Append("               )" + "\n");
            strSqlString.Append("             , (SELECT LEVEL AS SEQ FROM DUAL CONNECT BY LEVEL <= 7)" + "\n");
            strSqlString.Append("       )" + "\n");
            strSqlString.Append(" GROUP BY " + QueryCond1 + ", GUBUN" + "\n");
            strSqlString.Append(" ORDER BY " + QueryCond1 + ", DECODE(GUBUN, 'BOH', 1, '입고 (IN)', 2, '반품 (R-IN)', 3, 'AO (OUT)', 4, '반품입고 (R-OUT)', 5, 'LOSS', 6, 'EOH', 7)" + "\n");

            if (GlobalVariable.gsUserGroup == "ADMIN_GROUP" || GlobalVariable.gsUserGroup == "HANA_ADMIN_GROUP")
            {
                System.Windows.Forms.Clipboard.SetText(strSqlString.ToString());
            }

            return strSqlString.ToString();
        }
        
        #endregion


        #region EVENT 처리
        /// <summary>
        /// 6. View 버튼 Action
        /// </summary>        ///         
        private void btnView_Click(object sender, EventArgs e)
        {
            DataTable dt = null;

            if (CheckField() == false) return;

            GridColumnInit();
            spdData_Sheet1.RowCount = 0;

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

                //int[] rowType = spdData.RPT_DataBindingWithSubTotal(dt, 0, sub+1, 14, null, null, btnSort);
                //데이타테이블, 토탈 시작할 셀넘버, 시작 부터 서브토탈 몇개 쓸건지, 첫셀부터 몇번째 셀까지 TOTAL 적용안함

                //Total부분 셀머지
                //spdData.RPT_FillDataSelectiveCells("Total", 0, 14, 0, 1, true, Align.Center, VerticalAlign.Center);

                //spdData.RPT_AutoFit(false);                

                spdData.DataSource = dt;

                for (int i = 0; i < spdData.ActiveSheet.RowCount; i++)
                {
                    if (spdData.ActiveSheet.Cells[i, 13].Value.ToString() == "BOH")
                    {
                        spdData.ActiveSheet.Rows[i].BackColor = Color.LemonChiffon;
                    }
                }      

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
                spdData.ExportExcel();            
            }
        }

        /// <summary>
        /// Factory 설정
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cdvFactory_ValueSelectedItemChanged(object sender, Miracom.UI.MCCodeViewSelChanged_EventArgs e)
        {
            this.SetFactory(cdvFactory.txtValue);        
        }

        #endregion
        
    }
}
