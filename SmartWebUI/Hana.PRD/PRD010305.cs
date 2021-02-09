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
    public partial class PRD010305 : Miracom.SmartWeb.UI.Controls.udcCUSReport002
    {
        /// <summary>
        /// 클  래  스: PRD010305<br/>
        /// 클래스요약: 정체 HOLD LOT 조회<br/>
        /// 작  성  자: 미라콤 양형석<br/>
        /// 최초작성일: 2008-12-06<br/>
        /// 상세  설명: 정체 HOLD LOT 조회.<br/>
        /// 변경  내용: <br/>
        /// 변  경  자: 하나마이크론 김준용<br />
        /// Excel Export 저장 기능 변경<br />
        /// 2011-05-19-임종우 : HMKT1일 경우 H72(장기정체) HOLD 재공f 제외 (김권수 요청)
        /// 2011-05-31-임종우 : HOLD CODE 검색 기능 추가 요청 (김주인 요청)
        /// 2011-06-28-임종우 : ISSUE 일 기준 추가
        /// 2011-11-28-임종우 : 시간 기준 검색 기능 추가 (박성모 요청)
        /// 2013-07-15-임종우 : ASSY SITE 추가 (황혜리 요청)
        /// 2013-10-14-김민우 : LOT TYPE ALL, P%, E% 구분으로변경
        /// 2014-07-18-임종우 : DA 정체 시간 추가 (임태성 요청)
        /// 2014-11-27-임종우 : DA 정체 시간 -> A0250 END ~ A0400 SPLIT 전 까지의 시간으로 변경 (조형진 요청)
        ///                     WB Multi Lot 제외 기능 추가 (조형진 요청)
        /// </summary>
        public PRD010305()
        {
            InitializeComponent();
            //udcDurationDate1.AutoBinding();

            SortInit();
            GridColumnInit(); //헤더 한줄짜리

            cdvHoldKind.SelectedIndex = 0;
            cdvDate.Value = DateTime.Today;
            txtHoldDate.Text = "2";
        }

        #region SortInit

        /// <summary>
        /// SortInit
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SortInit()
        {
            ((udcTableForm)(this.btnSort.BindingForm)).AddRow("Step", "A.OPER AS STEP", "A.OPER", true);
            ((udcTableForm)(this.btnSort.BindingForm)).AddRow("Pin Type", "A.MAT_CMF_10 AS PIN_TYPE", "A.MAT_CMF_10", true);
            ((udcTableForm)(this.btnSort.BindingForm)).AddRow("Product", "A.MAT_ID AS PRODUCT", "A.MAT_ID", true);
            ((udcTableForm)(this.btnSort.BindingForm)).AddRow("Customer", "A.MAT_GRP_1 AS CUSTOMER", "A.MAT_GRP_1", false);
            ((udcTableForm)(this.btnSort.BindingForm)).AddRow("Family", "A.MAT_GRP_2 AS FAMILY", "A.MAT_GRP_2", false);
            ((udcTableForm)(this.btnSort.BindingForm)).AddRow("Package", "A.MAT_GRP_3 AS PACKAGE", "A.MAT_GRP_3", false);
            ((udcTableForm)(this.btnSort.BindingForm)).AddRow("Type1", "A.MAT_GRP_4 AS TYPE1", "A.MAT_GRP_4", false);
            ((udcTableForm)(this.btnSort.BindingForm)).AddRow("Type2", "A.MAT_GRP_5 AS TYPE2", "A.MAT_GRP_5", false);
            ((udcTableForm)(this.btnSort.BindingForm)).AddRow("LD Count", "A.MAT_GRP_6 AS LD_COUNT", "A.MAT_GRP_6", false);
            ((udcTableForm)(this.btnSort.BindingForm)).AddRow("Density", "A.MAT_GRP_7 AS DENSITY", "A.MAT_GRP_7", false);
            ((udcTableForm)(this.btnSort.BindingForm)).AddRow("Generation", "A.MAT_GRP_8 AS GENERATION", "A.MAT_GRP_8", false);
            ((udcTableForm)(this.btnSort.BindingForm)).AddRow("Cust Device", "A.MAT_CMF_7 AS CUST_DEVICE", "A.MAT_CMF_7", false);
        }

        #endregion

        #region 한줄헤더생성

        /// <summary>
        /// 한줄헤더생성
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void GridColumnInit()
        {
            spdData.ActiveSheet.ColumnHeader.Rows.Count = 1;
            spdData.RPT_ColumnInit();
            spdData.RPT_AddBasicColumn("Step", 0, 0, Visibles.True, Frozen.True, Align.Center, Merge.True, Formatter.String, 80);
            spdData.RPT_AddBasicColumn("Product", 0, 1, Visibles.True, Frozen.True, Align.Left, Merge.True, Formatter.String, 80);
            spdData.RPT_AddBasicColumn("Customer", 0, 2, Visibles.False, Frozen.True, Align.Center, Merge.True, Formatter.String, 80);
            spdData.RPT_AddBasicColumn("Family", 0, 3, Visibles.False, Frozen.True, Align.Center, Merge.True, Formatter.String, 80);
            spdData.RPT_AddBasicColumn("Package", 0, 4, Visibles.False, Frozen.True, Align.Center, Merge.True, Formatter.String, 80);
            spdData.RPT_AddBasicColumn("Type1", 0, 5, Visibles.False, Frozen.True, Align.Center, Merge.True, Formatter.String, 80);
            spdData.RPT_AddBasicColumn("Type2", 0, 6, Visibles.False, Frozen.True, Align.Center, Merge.True, Formatter.String, 80);
            spdData.RPT_AddBasicColumn("LD Count", 0, 7, Visibles.False, Frozen.True, Align.Center, Merge.True, Formatter.String, 80);
            spdData.RPT_AddBasicColumn("Density", 0, 8, Visibles.False, Frozen.True, Align.Center, Merge.True, Formatter.String, 80);
            spdData.RPT_AddBasicColumn("Generation", 0, 9, Visibles.False, Frozen.True, Align.Center, Merge.True, Formatter.String, 80);
            spdData.RPT_AddBasicColumn("Pin Type", 0, 10, Visibles.True, Frozen.True, Align.Left, Merge.True, Formatter.String, 80);
            spdData.RPT_AddBasicColumn("Cust Device", 0, 11, Visibles.False, Frozen.True, Align.Left, Merge.True, Formatter.String, 80);
            spdData.RPT_AddBasicColumn("Lot Id", 0, 12, Visibles.True, Frozen.False, Align.Left, Merge.False, Formatter.String, 80);
            spdData.RPT_AddBasicColumn("Type", 0, 13, Visibles.True, Frozen.False, Align.Center, Merge.False, Formatter.String, 50);
            spdData.RPT_AddBasicColumn("Status", 0, 14, Visibles.True, Frozen.False, Align.Center, Merge.False, Formatter.String, 80);
            spdData.RPT_AddBasicColumn("H Code", 0, 15, Visibles.True, Frozen.False, Align.Center, Merge.False, Formatter.String, 80);
            spdData.RPT_AddBasicColumn("H Desc", 0, 16, Visibles.True, Frozen.False, Align.Center, Merge.False, Formatter.String, 80);

            // 2011-11-28-임종우 : 시간 기준 검색 기능 추가 (박성모 요청)
            if (ckbTime.Checked == true)
            {
                spdData.RPT_AddBasicColumn("Our company (time)", 0, 17, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Double2, 80);
                spdData.RPT_AddBasicColumn("ISSUE (hours)", 0, 18, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Double2, 80);
                spdData.RPT_AddBasicColumn("Operation (time)", 0, 19, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Double2, 80);
            }
            else
            {
                spdData.RPT_AddBasicColumn("Our company (day)", 0, 17, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Double2, 80);
                spdData.RPT_AddBasicColumn("ISSUE (days)", 0, 18, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Double2, 80);
                spdData.RPT_AddBasicColumn("Operation (days)", 0, 19, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Double2, 80);
            }

            //spdData.RPT_AddBasicColumn("CREATE_QTY", 0, 16, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 80);
            spdData.RPT_AddBasicColumn("QTY", 0, 20, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 80);
            //spdData.RPT_AddBasicColumn("YIELD", 0, 18, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.String, 80);
            spdData.RPT_AddBasicColumn("Our input time", 0, 21, Visibles.True, Frozen.False, Align.Center, Merge.False, Formatter.String, 80);
            spdData.RPT_AddBasicColumn("ISSUE time", 0, 22, Visibles.True, Frozen.False, Align.Center, Merge.False, Formatter.String, 80);
            spdData.RPT_AddBasicColumn("Step In time", 0, 23, Visibles.True, Frozen.False, Align.Center, Merge.False, Formatter.String, 80);
            spdData.RPT_AddBasicColumn("Comment", 0, 24, Visibles.True, Frozen.False, Align.Left, Merge.False, Formatter.String, 80);
            spdData.RPT_AddBasicColumn("ASSY SITE", 0, 25, Visibles.True, Frozen.False, Align.Center, Merge.False, Formatter.String, 80);

            spdData.RPT_AddBasicColumn("DA stagnation time", 0, 26, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Double2, 80);
            //spdData.RPT_AddDynamicColumn(udcDurationDate1, 0, 14, Visibles.False, Frozen.False, Align.Right, Merge.False, Formatter.String,60);
            spdData.RPT_ColumnConfigFromTable(btnSort); //Group항목이 있을경우 반드시 선업해줄것.
        }

        #endregion

        #region 조회

        /// <summary>
        /// 조회
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnView_Click(object sender, EventArgs e)
        {
            if (!CheckField()) return;

            DataTable dt = null;

            GridColumnInit();

            try
            {
                LoadingPopUp.LoadIngPopUpShow(this);

                spdData_Sheet1.RowCount = 0;
                this.Refresh();
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
                int nGroupCount = ((udcTableForm)(this.btnSort.BindingForm)).GetSelectedCount();

                ////by John (한줄짜리)
                ////1.Griid 합계 표시
                int[] rowType = spdData.RPT_DataBindingWithSubTotal(dt, 0, sub + 1, 17, null, null, btnSort);
                //spdData.DataSource = dt;

                ////2. 칼럼 고정(필요하다면..)
                //spdData.Sheets[0].FrozenColumnCount = 9;

                ////3. Total부분 셀머지
                spdData.RPT_FillDataSelectiveCells("Total", 0, 17, 0, 1, true, Align.Center, VerticalAlign.Center);

                //4. Column Auto Fit
                spdData.RPT_AutoFit(false);

                spdData.ActiveSheet.Columns[spdData.ActiveSheet.Columns.Count - 1].Width = 100;

                #region 공정일, issue일, 당사일 Subtotal, GrandTotal은 SUM 대신 AVG로 바꿔주기
                spdData.RPT_SetAvgSubTotalAndGrandTotal(1, 17, nGroupCount, false);
                spdData.RPT_SetAvgSubTotalAndGrandTotal(1, 18, nGroupCount, false);
                spdData.RPT_SetAvgSubTotalAndGrandTotal(1, 19, nGroupCount, false);
                spdData.RPT_SetAvgSubTotalAndGrandTotal(1, 26, nGroupCount, false);               
                #endregion

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
        
        #region CheckField

        /// <summary>
        /// CheckField
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private Boolean CheckField()
        {
            if (cdvFactory.Text.Trim().Length == 0)
            {
                CmnFunction.ShowMsgBox(RptMessages.GetMessage("STD001", GlobalVariable.gcLanguage));
                return false;
            }

            if (cdvStep.Text.Trim().Length == 0)
            {
                CmnFunction.ShowMsgBox(RptMessages.GetMessage("STD005", GlobalVariable.gcLanguage));
                return false;
            }

            return true;
        }

        #endregion

        #region MakeSqlString
        private string MakeSqlString()
        {
            StringBuilder strSqlString = new StringBuilder();

            string QueryCond1 = null;
            string QueryCond2 = null;

            udcTableForm tableForm = (udcTableForm)btnSort.BindingForm;

            bool isToday = false;
            QueryCond1 = tableForm.SelectedValueToQueryContainNull;
            QueryCond2 = tableForm.SelectedValue2ToQueryContainNull;

            string strDate = cdvDate.Value.ToString("yyyyMMdd");

            if (DateTime.Now.ToString("yyyyMMdd").Equals(strDate))
            {
                strDate = DateTime.Now.ToString("yyyyMMddHHmmss");
                isToday = true;
            }
            else
            {
                strDate = strDate + "220000";
            }

            strSqlString.AppendFormat("SELECT {0} " + "\n", QueryCond1);
            strSqlString.AppendFormat("     , A.LOT_ID " + "\n");
            strSqlString.AppendFormat("     , A.LOT_CMF_5 " + "\n");
            strSqlString.AppendFormat("     , A.STATUS " + "\n");
            strSqlString.AppendFormat("     , A.HOLD_CODE " + "\n");
            strSqlString.AppendFormat("     , A.HOLD_DESC " + "\n");
            strSqlString.AppendFormat("     , A.CREATE_DAYDIFF " + "\n");
            strSqlString.AppendFormat("     , A.ISSUE_DAYDIFF " + "\n");
            strSqlString.AppendFormat("     , A.OPER_IN_DAYDIFF " + "\n");
            strSqlString.AppendFormat("     , A.QTY_1 " + "\n");
            strSqlString.AppendFormat("     , A.LOT_CMF_14 " + "\n");
            strSqlString.AppendFormat("     , A.ISSUE_TIME " + "\n");
            strSqlString.AppendFormat("     , A.OPER_IN_TIME " + "\n");
            strSqlString.AppendFormat("     , A.LAST_COMMENT " + "\n");
            strSqlString.AppendFormat("     , A.LOT_CMF_7 " + "\n");

            if (ckbTime.Checked == true)
            {
                //strSqlString.AppendFormat("     , CASE WHEN A.S_TIME IS NOT NULL AND A.E_TIME IS NOT NULL THEN TRUNC((TO_DATE(A.E_TIME, 'YYYYMMDDHH24MISS') - TO_DATE(A.S_TIME, 'YYYYMMDDHH24MISS')) * 24, 2) END AS DA_DAYDIFF" + "\n");
                strSqlString.AppendFormat("     , CASE WHEN A.S_TIME IS NOT NULL AND A.S_TIME <= A.E_TIME THEN TRUNC((TO_DATE(A.E_TIME, 'YYYYMMDDHH24MISS') - TO_DATE(A.S_TIME, 'YYYYMMDDHH24MISS')) * 24, 2) END AS DA_DAYDIFF" + "\n");
            }
            else
            {
                //strSqlString.AppendFormat("     , CASE WHEN A.S_TIME IS NOT NULL AND A.E_TIME IS NOT NULL THEN TRUNC(TO_DATE(A.E_TIME, 'YYYYMMDDHH24MISS') - TO_DATE(A.S_TIME, 'YYYYMMDDHH24MISS'), 2) END AS DA_DAYDIFF" + "\n");
                strSqlString.AppendFormat("     , CASE WHEN A.S_TIME IS NOT NULL AND A.S_TIME <= A.E_TIME THEN TRUNC(TO_DATE(A.E_TIME, 'YYYYMMDDHH24MISS') - TO_DATE(A.S_TIME, 'YYYYMMDDHH24MISS'), 2) END AS DA_DAYDIFF" + "\n");
            }
     
            strSqlString.AppendFormat("  FROM ( " + "\n");
            strSqlString.AppendFormat("        SELECT C.* " + "\n");
            strSqlString.AppendFormat("             , A.OPER  " + "\n");
            strSqlString.AppendFormat("             , A.LOT_ID  " + "\n");
            strSqlString.AppendFormat("             , A.LOT_CMF_5  " + "\n");
            strSqlString.AppendFormat("             , CASE WHEN HOLD_FLAG = 'Y' THEN 'HOLD'" + "\n");            
            strSqlString.AppendFormat("                    ELSE DECODE(A.LOT_STATUS, 'PROC', 'RUN', A.LOT_STATUS)   " + "\n");
            strSqlString.AppendFormat("               END AS STATUS " + "\n");
            strSqlString.AppendFormat("             , A.HOLD_CODE " + "\n");
            strSqlString.AppendFormat("             , DECODE(A.HOLD_CODE, ' ', ' ', (SELECT DATA_1 FROM MGCMTBLDAT WHERE FACTORY=A.FACTORY AND TABLE_NAME = 'HOLD_CODE' AND KEY_1 = A.HOLD_CODE AND ROWNUM=1)) HOLD_DESC " + "\n");

            // 2011-11-28-임종우 : 시간 기준 추가
            if (ckbTime.Checked == true)
            {
                strSqlString.AppendFormat("             , TRUNC((TO_DATE('{0}','YYYYMMDDHH24MISS') - DECODE(A.LOT_CMF_14, ' ', TO_DATE(A.LOT_CMF_14,'YYYYMMDDHH24MISS'), TO_DATE(A.LOT_CMF_14,'YYYYMMDDHH24MISS'))) * 24, 2) AS CREATE_DAYDIFF " + "\n", strDate);
                strSqlString.AppendFormat("             , TRUNC((TO_DATE('{0}','YYYYMMDDHH24MISS') - DECODE(A.RESV_FIELD_1, ' ', SYSDATE, TO_DATE(A.RESV_FIELD_1,'YYYYMMDDHH24MISS'))) * 24, 2) AS ISSUE_DAYDIFF " + "\n", strDate);
                strSqlString.AppendFormat("             , TRUNC((TO_DATE('{0}','YYYYMMDDHH24MISS') - TO_DATE(A.OPER_IN_TIME,'YYYYMMDDHH24MISS')) * 24, 2) AS OPER_IN_DAYDIFF " + "\n", strDate);
            }
            else
            {
                strSqlString.AppendFormat("             , TRUNC(TO_DATE('{0}','YYYYMMDDHH24MISS') - DECODE(A.LOT_CMF_14, ' ', TO_DATE(A.LOT_CMF_14,'YYYYMMDDHH24MISS'), TO_DATE(A.LOT_CMF_14,'YYYYMMDDHH24MISS')), 2) AS CREATE_DAYDIFF " + "\n", strDate);
                strSqlString.AppendFormat("             , TRUNC(TO_DATE('{0}','YYYYMMDDHH24MISS') - DECODE(A.RESV_FIELD_1, ' ', SYSDATE, TO_DATE(A.RESV_FIELD_1,'YYYYMMDDHH24MISS')), 2) AS ISSUE_DAYDIFF " + "\n", strDate);
                strSqlString.AppendFormat("             , TRUNC(TO_DATE('{0}','YYYYMMDDHH24MISS') - TO_DATE(A.OPER_IN_TIME,'YYYYMMDDHH24MISS'), 2) AS OPER_IN_DAYDIFF " + "\n", strDate);
            }

            strSqlString.AppendFormat("             , A.QTY_1  " + "\n");
            strSqlString.AppendFormat("             , TO_CHAR(TO_DATE(A.LOT_CMF_14, 'YYYYMMDDHH24MISS'), 'YY/MM/DD HH24:MI:SS') AS LOT_CMF_14 " + "\n");
            strSqlString.AppendFormat("             , DECODE(A.RESV_FIELD_1, ' ', ' ', TO_CHAR(TO_DATE(A.RESV_FIELD_1, 'YYYYMMDDHH24MISS'), 'YY/MM/DD HH24:MI:SS')) AS ISSUE_TIME " + "\n");
            strSqlString.AppendFormat("             , DECODE(A.OPER_IN_TIME, ' ', ' ', TO_CHAR(TO_DATE(A.OPER_IN_TIME, 'YYYYMMDDHH24MISS'), 'YY/MM/DD HH24:MI:SS')) AS OPER_IN_TIME " + "\n");
            strSqlString.AppendFormat("             , A.LAST_COMMENT" + "\n");
            strSqlString.AppendFormat("             , A.LOT_CMF_7" + "\n");
            strSqlString.AppendFormat("             , (SELECT TRAN_TIME FROM RWIPLOTHIS WHERE FACTORY = '" + GlobalVariable.gsAssyDefaultFactory + "' AND HIST_DEL_FLAG = ' ' AND TRAN_CODE = 'END' AND OLD_OPER = 'A0250' AND LOT_ID = DECODE(D.FROM_TO_LOT_ID, NULL, A.LOT_ID, D.FROM_TO_LOT_ID)) AS S_TIME " + "\n");
            //strSqlString.AppendFormat("             , (SELECT TRAN_TIME FROM RWIPLOTHIS WHERE FACTORY = '" + GlobalVariable.gsAssyDefaultFactory + "' AND HIST_DEL_FLAG = ' ' AND TRAN_CODE = 'START' AND OPER IN ('A0333', 'A0400', 'A0401') AND LOT_ID = A.LOT_ID) AS E_TIME " + "\n");
            strSqlString.AppendFormat("             , A.CREATE_TIME AS E_TIME " + "\n");

            if (isToday)
            {
                strSqlString.AppendFormat("          FROM RWIPLOTSTS A, MWIPMATDEF C " + "\n");
                strSqlString.AppendFormat("             , (SELECT * FROM RWIPLOTSPL WHERE FACTORY = '" + GlobalVariable.gsAssyDefaultFactory + "' AND (OPER LIKE 'A04%' OR OPER = 'A0333') AND FROM_TO_FLAG = 'T') D" + "\n");
                strSqlString.AppendFormat("         WHERE 1=1" + "\n");
            }
            else
            {
                strSqlString.AppendFormat("          FROM RWIPLOTSTS_BOH A, MWIPMATDEF C " + "\n");
                strSqlString.AppendFormat("             , (SELECT * FROM RWIPLOTSPL WHERE FACTORY = '" + GlobalVariable.gsAssyDefaultFactory + "' AND (OPER LIKE 'A04%' OR OPER = 'A0333') AND FROM_TO_FLAG = 'T') D" + "\n");
                strSqlString.AppendFormat("         WHERE A.CUTOFF_DT = '" + strDate.Substring(0, 10) + "'" + "\n");
            }
            
            strSqlString.AppendFormat("           AND A.FACTORY = C.FACTORY " + "\n");
            strSqlString.AppendFormat("           AND A.MAT_ID = C.MAT_ID " + "\n");
            strSqlString.AppendFormat("           AND A.LOT_ID = D.LOT_ID(+) " + "\n");
            strSqlString.AppendFormat("           AND A.FACTORY {0} \n", cdvFactory.SelectedValueToQueryString);

            if (cdvFactory.Text == GlobalVariable.gsAssyDefaultFactory)
                strSqlString.AppendFormat("           AND A.OPER NOT IN ('00001','00002') " + "\n");

            // 2011-05-19-임종우 : HMKT1일 경우 H72(장기정체) HOLD 재공 제외 (김권수 요청)
            if (cdvFactory.Text == GlobalVariable.gsTestDefaultFactory)
                strSqlString.AppendFormat("           AND A.HOLD_CODE <> 'H72'  " + "\n");

            strSqlString.AppendFormat("           AND A.MAT_VER = 1" + "\n");
            strSqlString.AppendFormat("           AND A.LOT_DEL_FLAG = ' ' " + "\n");
            strSqlString.AppendFormat("           AND A.LOT_TYPE = 'W' " + "\n");

            #region 상세 조회에 따른 SQL문 생성
            if (udcWIPCondition1.Text != "ALL" && udcWIPCondition1.Text != "")
                strSqlString.AppendFormat("           AND C.MAT_GRP_1 {0} " + "\n", udcWIPCondition1.SelectedValueToQueryString);

            if (udcWIPCondition2.Text != "ALL" && udcWIPCondition2.Text != "")
                strSqlString.AppendFormat("           AND C.MAT_GRP_2 {0} " + "\n", udcWIPCondition2.SelectedValueToQueryString);

            if (udcWIPCondition3.Text != "ALL" && udcWIPCondition3.Text != "")
                strSqlString.AppendFormat("           AND C.MAT_GRP_3 {0} " + "\n", udcWIPCondition3.SelectedValueToQueryString);

            if (udcWIPCondition4.Text != "ALL" && udcWIPCondition4.Text != "")
                strSqlString.AppendFormat("           AND C.MAT_GRP_4 {0} " + "\n", udcWIPCondition4.SelectedValueToQueryString);

            if (udcWIPCondition5.Text != "ALL" && udcWIPCondition5.Text != "")
                strSqlString.AppendFormat("           AND C.MAT_GRP_5 {0} " + "\n", udcWIPCondition5.SelectedValueToQueryString);

            if (udcWIPCondition6.Text != "ALL" && udcWIPCondition6.Text != "")
                strSqlString.AppendFormat("           AND C.MAT_GRP_6 {0} " + "\n", udcWIPCondition6.SelectedValueToQueryString);

            if (udcWIPCondition7.Text != "ALL" && udcWIPCondition7.Text != "")
                strSqlString.AppendFormat("           AND C.MAT_GRP_7 {0} " + "\n", udcWIPCondition7.SelectedValueToQueryString);

            if (udcWIPCondition8.Text != "ALL" && udcWIPCondition8.Text != "")
                strSqlString.AppendFormat("           AND C.MAT_GRP_8 {0} " + "\n", udcWIPCondition8.SelectedValueToQueryString);

            if (udcWIPCondition9.Text != "ALL" && udcWIPCondition9.Text != "")
                strSqlString.AppendFormat("           AND C.MAT_GRP_9 {0} " + "\n", udcWIPCondition9.SelectedValueToQueryString);
            #endregion 

            // Step
            if (cdvStep.Text != "ALL" && cdvStep.Text != "")
                strSqlString.AppendFormat("           AND A.OPER {0}" + "\n", cdvStep.SelectedValueToQueryString);

            // Product
            if (txtSearchProduct.Text.Trim() != "%" && txtSearchProduct.Text.Trim() != "")
                strSqlString.AppendFormat("           AND A.MAT_ID LIKE '{0}'" + "\n", txtSearchProduct.Text);

            // Lot Type
            if (cdvLotType.Text != "ALL")            
                strSqlString.AppendFormat("           AND A.LOT_CMF_5 LIKE '" + cdvLotType.Text + "'" + "\n");
            

            // 2011-11-28-임종우 : 시간 기준 검색 기능 추가 (박성모 요청)
            if (ckbTime.Checked == true)
            {
                // Hold Date (Lot의 Status의 Hold 개념이 아님. 투입일, 공정일 기준으로 설정된 날까지의 기간)
                if (txtHoldDate.Text.Trim() != "" && rbtCreate.Checked)
                {
                    strSqlString.AppendFormat("           AND TRUNC((TO_DATE('{0}','YYYYMMDDHH24MISS') - TO_DATE(A.LOT_CMF_14,'YYYYMMDDHH24MISS')) * 24, 2) > {1}" + "\n", strDate, txtHoldDate.Text);
                }
                else if (txtHoldDate.Text.Trim() != "" && rbtOper.Checked)
                {
                    strSqlString.AppendFormat("           AND TRUNC((TO_DATE('{0}','YYYYMMDDHH24MISS') - TO_DATE(A.OPER_IN_TIME,'YYYYMMDDHH24MISS')) * 24, 2) > {1}" + "\n", strDate, txtHoldDate.Text);
                }
                else
                {
                    strSqlString.AppendFormat("           AND TRUNC((TO_DATE('{0}','YYYYMMDDHH24MISS') - DECODE(A.RESV_FIELD_1, ' ', SYSDATE, TO_DATE(A.RESV_FIELD_1,'YYYYMMDDHH24MISS'))) * 24, 2) > {1}" + "\n", strDate, txtHoldDate.Text);
                }
            }
            else
            {
                // Hold Date (Lot의 Status의 Hold 개념이 아님. 투입일, 공정일 기준으로 설정된 날까지의 기간)
                if (txtHoldDate.Text.Trim() != "" && rbtCreate.Checked)
                {
                    strSqlString.AppendFormat("           AND TRUNC(TO_DATE('{0}','YYYYMMDDHH24MISS') - TO_DATE(A.LOT_CMF_14,'YYYYMMDDHH24MISS'), 2) > {1}" + "\n", strDate, txtHoldDate.Text);
                }
                else if (txtHoldDate.Text.Trim() != "" && rbtOper.Checked)
                {
                    strSqlString.AppendFormat("           AND TRUNC(TO_DATE('{0}','YYYYMMDDHH24MISS') - TO_DATE(A.OPER_IN_TIME,'YYYYMMDDHH24MISS'), 2) > {1}" + "\n", strDate, txtHoldDate.Text);
                }
                else
                {
                    strSqlString.AppendFormat("           AND TRUNC(TO_DATE('{0}','YYYYMMDDHH24MISS') - DECODE(A.RESV_FIELD_1, ' ', SYSDATE, TO_DATE(A.RESV_FIELD_1,'YYYYMMDDHH24MISS')), 2) > {1}" + "\n", strDate, txtHoldDate.Text);
                }
            }

            // Hold (Hold Flag)
            if (cdvHoldKind.Text == "Hold")
                strSqlString.AppendFormat("           AND A.HOLD_FLAG = 'Y' " + "\n");
            else if (cdvHoldKind.Text == "Non Hold")
                strSqlString.AppendFormat("           AND A.HOLD_FLAG = ' ' " + "\n");

            // 2011-05-31-임종우 : HOLD_CODE 검색 기능 추가 (김주인 요청)
            if (cdvHoldCode.Text != "ALL" && cdvHoldCode.Text != "")
            {
                strSqlString.AppendFormat("           AND A.HOLD_CODE {0}" + "\n", cdvHoldCode.SelectedValueToQueryString);
            }

            // 2014-11-27-임종우 : W/B Multi Lot 제외 기능 추가 (조형진 요청)
            if (ckbMulti.Checked == true)
            {
                strSqlString.AppendFormat("           AND A.ADD_ORDER_ID_2 <> 'MULTIEQP' " + "\n");
            }
            
            strSqlString.AppendFormat("       ) A " + "\n");

            //strSqlString.AppendFormat("GROUP BY A.FACTORY, {0}, A.LOT_ID, A.LOT_CMF_5, A.LOT_STATUS, A.HOLD_CODE, A.HOLD_FLAG, A.LOT_CMF_14, A.RESV_FIELD_1, A.OPER_IN_TIME, A.LAST_COMMENT, A.LOT_CMF_7 " + "\n", QueryCond2);
            strSqlString.AppendFormat("ORDER BY {0}, A.LOT_ID, A.LOT_CMF_5, A.STATUS, A.HOLD_CODE, A.LOT_CMF_14, A.OPER_IN_TIME, A.LAST_COMMENT, A.LOT_CMF_7 " + "\n", QueryCond2);

            if (GlobalVariable.gsUserGroup == "ADMIN_GROUP" || GlobalVariable.gsUserGroup == "HANA_ADMIN_GROUP")
            {
                System.Windows.Forms.Clipboard.SetText(strSqlString.ToString());
            }

            return strSqlString.ToString();
        }

        #endregion

        #endregion

        #region ToExcel

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
        #endregion

        private void cdvFactory_ValueSelectedItemChanged(object sender, MCCodeViewSelChanged_EventArgs e)
        {
            this.SetFactory(cdvFactory.txtValue);
            cdvStep.sFactory = cdvFactory.txtValue;
            //cdvLotType.sFactory = cdvFactory.txtValue;
        }

        private void ckbTime_CheckedChanged(object sender, EventArgs e)
        {
            if (ckbTime.Checked == true)
            {
                lblHoldDate.Text = "Stagnation time";
            }
            else
            {
                lblHoldDate.Text = "Stagnant days";
            }
        }
    }
}

