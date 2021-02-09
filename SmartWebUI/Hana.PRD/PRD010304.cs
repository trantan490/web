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
    public partial class PRD010304 : Miracom.SmartWeb.UI.Controls.udcCUSReport002
    {
        /// <summary>
        /// 클  래  스: PRD010304<br/>
        /// 클래스요약: 공정별 재공 조회<br/>
        /// 작  성  자: 미라콤 양형석<br/>
        /// 최초작성일: 2008-12-10<br/>
        /// 상세  설명: 공정별 재공 조회<br/>
        /// 변경  내용: <br/>
        /// 변  경  자: 하나마이크론 김준용<br />
        /// Excel Export 저장 기능 변경<br />
        /// 2010-04-14-임종우 : FACTORY 가 FGS 일 경우 기본 정렬로...
        /// 2011-05-19-임종우 : HMKT1일 경우 H72(장기정체) HOLD 재공 제외 (김권수 요청)
        /// 2011-06-13-김민우 : HMKT1일 경우 H72(장기정체) HOLD 재공 제외 원복 (김권수 요청)
        /// 2012-05-21-배수민 : HMKA1일 경우 WAFER QTY 추가 (김문한K 요청)
        /// 2013-05-26-임종우 : 속도 문제로 인해 쿼리 튜닝 작업
        /// 2013-10-14-김민우 : LOT TYPE ALL, P%, E% 구분으로변경
        /// 2013-11-25-임종우 : SEC Run Merge (NCB Code) 정보 표시 (김성업 요청)
        /// 2014-01-09-임종우 : NCH Code 추가 (김성업 요청)
        /// 2015-04-03-임종우 : HANA_LOT_TYPE 추가 (박형순 요청)
        /// </summary>
        public PRD010304()
        {
            InitializeComponent();

            SortInit();
            GridColumnInit(); //헤더 한줄짜리

            cdvDate.Value = DateTime.Today;
            cboTimeBase.SelectedIndex = 1;
        }

        #region SortInit

        private void SortInit()
        {
            ((udcTableForm)(this.btnSort.BindingForm)).AddRow("Step", "LOT.OPER AS STEP", "LOT.OPER", "", true);
            ((udcTableForm)(this.btnSort.BindingForm)).AddRow("Customer", "(SELECT DATA_1 FROM MGCMTBLDAT WHERE TABLE_NAME = 'H_CUSTOMER' AND KEY_1 = MAT.MAT_GRP_1 AND ROWNUM=1) AS CUSTOMER", "MAT.MAT_GRP_1",true);
            ((udcTableForm)(this.btnSort.BindingForm)).AddRow("Family", "MAT.MAT_GRP_2 AS FAMILY", "MAT.MAT_GRP_2",false);
            ((udcTableForm)(this.btnSort.BindingForm)).AddRow("Package", "MAT.MAT_GRP_3 AS PKG", "MAT.MAT_GRP_3", false);
            ((udcTableForm)(this.btnSort.BindingForm)).AddRow("Type1", "MAT.MAT_GRP_4 AS TYPE1", "MAT.MAT_GRP_4",  false);
            ((udcTableForm)(this.btnSort.BindingForm)).AddRow("Type2", "MAT.MAT_GRP_5 AS TYPE2", "MAT.MAT_GRP_5", false);
            ((udcTableForm)(this.btnSort.BindingForm)).AddRow("LD Count", "MAT.MAT_GRP_6 AS LEAD", "MAT.MAT_GRP_6", false);
            ((udcTableForm)(this.btnSort.BindingForm)).AddRow("Density", "MAT.MAT_GRP_7 AS DENSITY", "MAT.MAT_GRP_7",  false);
            ((udcTableForm)(this.btnSort.BindingForm)).AddRow("Generation", "MAT.MAT_GRP_8 AS GEN", "MAT.MAT_GRP_8", false);
            ((udcTableForm)(this.btnSort.BindingForm)).AddRow("PIN_TYPE", "MAT.MAT_CMF_10 AS PIN_TYPE", "MAT.MAT_CMF_10",  true);
            ((udcTableForm)(this.btnSort.BindingForm)).AddRow("Product", "MAT.MAT_ID", "MAT.MAT_ID",  true);
            ((udcTableForm)(this.btnSort.BindingForm)).AddRow("CUST_DEVICE", "MAT.MAT_CMF_7 AS CUST_DEVICE", "MAT.MAT_CMF_7",  false);
            ((udcTableForm)(this.btnSort.BindingForm)).AddRow("Run ID", "LOT.LOT_CMF_4", "LOT.LOT_CMF_4",  true);
            ((udcTableForm)(this.btnSort.BindingForm)).AddRow("Lot ID", "LOT.LOT_ID", "LOT.LOT_ID",  true);
            ((udcTableForm)(this.btnSort.BindingForm)).AddRow("SAP_CODE", "CASE WHEN OPER <= 'A0200' THEN VENDOR_MAT_ID ELSE BASE_MAT_ID END SAP_CODE", "BASE_MAT_ID,VENDOR_MAT_ID", false);
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

            spdData.RPT_ColumnInit();

            //if(cdvFactory.txtValue == "HMKE1")
            //{
            //    spdData.RPT_AddBasicColumn("Step", 0, 0, Visibles.True, Frozen.True, Align.Center, Merge.True, Formatter.String, 50);
            //    spdData.RPT_AddBasicColumn("Customer", 0, 1, Visibles.True, Frozen.True, Align.Center, Merge.True, Formatter.String, 70);
            //    spdData.RPT_AddBasicColumn("Family", 0, 2, Visibles.False, Frozen.True, Align.Center, Merge.True, Formatter.String, 50);
            //    spdData.RPT_AddBasicColumn("Package", 0, 3, Visibles.False, Frozen.True, Align.Center, Merge.True, Formatter.String, 50);
            //    spdData.RPT_AddBasicColumn("Type1", 0, 4, Visibles.False, Frozen.True, Align.Center, Merge.True, Formatter.String, 50);
            //    spdData.RPT_AddBasicColumn("Type2", 0, 5, Visibles.False, Frozen.True, Align.Center, Merge.True, Formatter.String, 50);
            //    spdData.RPT_AddBasicColumn("LD Count", 0, 6, Visibles.False, Frozen.True, Align.Center, Merge.True, Formatter.String, 50);
            //    spdData.RPT_AddBasicColumn("Density", 0, 7, Visibles.False, Frozen.True, Align.Center, Merge.True, Formatter.String, 50);
            //    spdData.RPT_AddBasicColumn("Generation", 0, 8, Visibles.False, Frozen.True, Align.Center, Merge.True, Formatter.String, 50);
            //    spdData.RPT_AddBasicColumn("PIN TYPE", 0, 9, Visibles.True, Frozen.True, Align.Left, Merge.True, Formatter.String, 70);
            //    spdData.RPT_AddBasicColumn("Product", 0, 10, Visibles.True, Frozen.True, Align.Left, Merge.True, Formatter.String, 90);
            //    spdData.RPT_AddBasicColumn("CUST_DEVICE", 0, 11, Visibles.False, Frozen.True, Align.Left, Merge.True, Formatter.String, 70);
            //    spdData.RPT_AddBasicColumn("Run ID", 0, 12, Visibles.True, Frozen.True, Align.Left, Merge.True, Formatter.String, 60);
            //    spdData.RPT_AddBasicColumn("Lot ID", 0, 13, Visibles.True, Frozen.True, Align.Left, Merge.True, Formatter.String, 60);
            //    spdData.RPT_AddBasicColumn("Qty", 0, 14, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 50);
            //    spdData.RPT_AddBasicColumn("W_Qty", 0, 15, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 50);
            //    spdData.RPT_AddBasicColumn("Status", 0, 16, Visibles.True, Frozen.False, Align.Center, Merge.False, Formatter.String, 50);
            //    spdData.RPT_AddBasicColumn("Lot Type", 0, 17, Visibles.True, Frozen.False, Align.Center, Merge.False, Formatter.String, 50);
            //    spdData.RPT_AddBasicColumn("Delay(H)", 0, 18, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 50);
            //    spdData.RPT_AddBasicColumn("Start Time", 0, 19, Visibles.True, Frozen.False, Align.Center, Merge.False, Formatter.String, 90);
            //    spdData.RPT_AddBasicColumn("Equip", 0, 20, Visibles.True, Frozen.False, Align.Center, Merge.False, Formatter.String, 50);
            //    spdData.RPT_AddBasicColumn("Hold Code", 0, 21, Visibles.True, Frozen.False, Align.Center, Merge.False, Formatter.String, 50);
            //    spdData.RPT_AddBasicColumn("Last Comment", 0, 22, Visibles.True, Frozen.False, Align.Left, Merge.False, Formatter.String, 100);
            //    spdData.RPT_ColumnConfigFromTable(btnSort); //Group항목이 있을경우 반드시 선업해줄것.
            //}
            //else
            //{
                spdData.RPT_AddBasicColumn("Step", 0, 0, Visibles.True, Frozen.True, Align.Center, Merge.True, Formatter.String, 50);
                spdData.RPT_AddBasicColumn("Customer", 0, 1, Visibles.True, Frozen.True, Align.Center, Merge.True, Formatter.String, 70);
                spdData.RPT_AddBasicColumn("Family", 0, 2, Visibles.False, Frozen.True, Align.Center, Merge.True, Formatter.String, 50);
                spdData.RPT_AddBasicColumn("Package", 0, 3, Visibles.False, Frozen.True, Align.Center, Merge.True, Formatter.String, 50);
                spdData.RPT_AddBasicColumn("Type1", 0, 4, Visibles.False, Frozen.True, Align.Center, Merge.True, Formatter.String, 50);
                spdData.RPT_AddBasicColumn("Type2", 0, 5, Visibles.False, Frozen.True, Align.Center, Merge.True, Formatter.String, 50);
                spdData.RPT_AddBasicColumn("LD Count", 0, 6, Visibles.False, Frozen.True, Align.Center, Merge.True, Formatter.String, 50);
                spdData.RPT_AddBasicColumn("Density", 0, 7, Visibles.False, Frozen.True, Align.Center, Merge.True, Formatter.String, 50);
                spdData.RPT_AddBasicColumn("Generation", 0, 8, Visibles.False, Frozen.True, Align.Center, Merge.True, Formatter.String, 50);
                spdData.RPT_AddBasicColumn("PIN TYPE", 0, 9, Visibles.True, Frozen.True, Align.Left, Merge.True, Formatter.String, 70);
                spdData.RPT_AddBasicColumn("Product", 0, 10, Visibles.True, Frozen.True, Align.Left, Merge.True, Formatter.String, 90);
                spdData.RPT_AddBasicColumn("CUST_DEVICE", 0, 11, Visibles.False, Frozen.True, Align.Left, Merge.True, Formatter.String, 70);
                spdData.RPT_AddBasicColumn("Run ID", 0, 12, Visibles.True, Frozen.True, Align.Left, Merge.True, Formatter.String, 60);
                spdData.RPT_AddBasicColumn("Lot ID", 0, 13, Visibles.True, Frozen.True, Align.Left, Merge.True, Formatter.String, 60);
                spdData.RPT_AddBasicColumn("SAP_CODE", 0, 14, Visibles.False, Frozen.False, Align.Right, Merge.False, Formatter.String, 70);
                spdData.RPT_AddBasicColumn("Qty", 0, 15, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 50);
                spdData.RPT_AddBasicColumn("W_Qty", 0, 16, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 50);
                spdData.RPT_AddBasicColumn("Status", 0, 17, Visibles.True, Frozen.False, Align.Center, Merge.False, Formatter.String, 50);
                spdData.RPT_AddBasicColumn("Lot Type", 0, 18, Visibles.True, Frozen.False, Align.Center, Merge.False, Formatter.String, 50);
                spdData.RPT_AddBasicColumn("Delay(H)", 0, 19, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 50);
                spdData.RPT_AddBasicColumn("Start Time", 0, 20, Visibles.True, Frozen.False, Align.Center, Merge.False, Formatter.String, 90);
                spdData.RPT_AddBasicColumn("Equip", 0, 21, Visibles.True, Frozen.False, Align.Center, Merge.False, Formatter.String, 50);
                spdData.RPT_AddBasicColumn("Hold Code", 0, 22, Visibles.True, Frozen.False, Align.Center, Merge.False, Formatter.String, 50);
                spdData.RPT_AddBasicColumn("NCF Code", 0, 23, Visibles.True, Frozen.False, Align.Center, Merge.False, Formatter.String, 50);
                spdData.RPT_AddBasicColumn("RUN_MERGE_CODE", 0, 24, Visibles.True, Frozen.False, Align.Center, Merge.False, Formatter.String, 50);
                spdData.RPT_AddBasicColumn("NCH Code", 0, 25, Visibles.True, Frozen.False, Align.Center, Merge.False, Formatter.String, 50);
                spdData.RPT_AddBasicColumn("Last Comment", 0, 26, Visibles.True, Frozen.False, Align.Left, Merge.False, Formatter.String, 100);
                spdData.RPT_AddBasicColumn("HANA_LOT_TYPE", 0, 27, Visibles.True, Frozen.False, Align.Left, Merge.False, Formatter.String, 80);
                
                spdData.RPT_ColumnConfigFromTable(btnSort); //Group항목이 있을경우 반드시 선업해줄것.
            //}            
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

                ////by John (한줄짜리)
                ////1.Griid 합계 표시
                int[] rowType = spdData.RPT_DataBindingWithSubTotal(dt, 0, sub+1, 14, null, null, btnSort);

                ////2. 칼럼 고정(필요하다면..)
                //spdData.Sheets[0].FrozenColumnCount = 9;

                ////3. Total부분 셀머지
                spdData.RPT_FillDataSelectiveCells("Total", 0, 14, 0, 1, true, Align.Center, VerticalAlign.Center);

                //4. Column Auto Fit
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

            if (cdvOper.txtFromValue.Trim().Length == 0 || cdvOper.txtToValue.Trim().Length == 0)
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

            QueryCond1 = tableForm.SelectedValueToQueryContainNull;
            QueryCond2 = tableForm.SelectedValue2ToQueryContainNull;


            string strDate = cdvDate.Value.ToString("yyyyMMdd");
            bool isRealTime = false;

            if (strDate.Equals(DateTime.Now.ToString("yyyyMMdd")))
            {
                strDate = DateTime.Now.ToString("yyyyMMddHHmmss");
                isRealTime = true;
            }
            else
            {
                //if (cboTimeBase.Text == "06시")
                if (cboTimeBase.SelectedIndex == 0)
                    strDate = strDate + "060000";
                else
                    strDate = strDate + "220000";

                isRealTime = false;
            }

        
            strSqlString.AppendFormat("SELECT {0}  " + "\n", QueryCond1);
            strSqlString.Append("     , SUM(LOT.QTY_1) AS QTY " + "\n");
            strSqlString.Append("     , SUM(LOT.QTY_2) AS WQTY " + "\n");
            strSqlString.Append("     , CASE WHEN LoT.HOLD_FLAG = 'Y' " + "\n");
            strSqlString.Append("            THEN 'HOLD' " + "\n");
            strSqlString.Append("            ELSE DECODE(LOT.LOT_STATUS, 'PROC', 'RUN', LOT.LOT_STATUS)   " + "\n");
            strSqlString.Append("       END AS STATUS " + "\n");
            strSqlString.Append("     , LOT.LOT_CMF_5 AS TYPE " + "\n");
            strSqlString.Append("     , DECODE(LOT.START_TIME, ' ', ' ', TRUNC((TO_DATE('" + strDate + "', 'YYYYMMDDHH24MISS') - TO_DATE(LOT.START_TIME, 'YYYYMMDDHH24MISS')), 2)) AS DELAY_HOUR " + "\n");
            strSqlString.Append("     , DECODE(LOT.START_TIME, ' ', ' ', TO_CHAR(TO_DATE(LOT.START_TIME, 'YYYYMMDDHH24MISS'), 'YY/MM/DD HH24:MI:SS')) AS START_TIME " + "\n");
            strSqlString.Append("     , LOT.START_RES_ID AS START_EQUIP " + "\n");
            strSqlString.Append("     , LOT.HOLD_CODE AS HOLDCODE " + "\n");
            strSqlString.Append("     , MAX(MESMGR.F_GET_SEC_NCX@RPTTOMES(LOT.FACTORY, 'NCFCODE', LOT_ID, 'V')) AS NCFCODE " + "\n");
            strSqlString.Append("     , MAX(MESMGR.F_GET_SEC_NCX@RPTTOMES(LOT.FACTORY, 'NCBCODE', LOT_ID, 'V')) AS RUN_MERGE_CODE " + "\n");
            strSqlString.Append("     , MAX(MESMGR.F_GET_SEC_NCX@RPTTOMES(LOT.FACTORY, 'NCHCODE', LOT_ID, 'V')) AS NCHCODE " + "\n");
            strSqlString.Append("     , LOT.LAST_COMMENT AS LASTCOMMENT " + "\n");
            strSqlString.Append("     , LOT.LOT_CMF_17 AS HANA_LOT_TYPE " + "\n");
  
            if (!isRealTime)
                strSqlString.Append("  FROM  RWIPLOTSTS_BOH LOT " + "\n");
            else
                strSqlString.Append("  FROM  RWIPLOTSTS LOT " + "\n");

            strSqlString.Append("      , MWIPMATDEF MAT " + "\n");
            //strSqlString.Append("        MGCMTBLDAT GCM, " + "\n");
            //strSqlString.Append("        MATRNAMSTS ATT" + "\n");

            strSqlString.Append(" WHERE  1=1 " + "\n");
            
            if (!isRealTime)
                strSqlString.AppendFormat("   AND LOT.CUTOFF_DT = '{0}'" + "\n", strDate.Substring(0, 10));

            strSqlString.Append("   AND LOT.MAT_ID NOT IN (SELECT MAT_ID FROM MWIPMATDEF WHERE FIRST_FLOW = 'A-BANK' AND DELETE_FLAG = ' ') " + "\n"); 
            strSqlString.Append("   AND LOT.FACTORY = MAT.FACTORY " + "\n");            
            strSqlString.Append("   AND LOT.MAT_ID = MAT.MAT_ID " + "\n");            
            strSqlString.Append("   AND LOT_DEL_FLAG = ' '  " + "\n");
            strSqlString.Append("   AND LOT.LOT_TYPE = 'W' " + "\n");
            strSqlString.Append("   AND MAT.DELETE_FLAG = ' ' " + "\n");

            #region 조회조건(FACTORY, STEP, LOT_TYPE, PRODUCT, DATE)

            strSqlString.Append("   AND LOT.FACTORY " + cdvFactory.SelectedValueToQueryString + "\n");

            strSqlString.Append("   AND LOT.OPER IN (" + cdvOper.getInQuery() + ")" + "\n");

            if (cdvFactory.Text == GlobalVariable.gsAssyDefaultFactory)
                strSqlString.Append("   AND LOT.OPER NOT IN ('00001','00002') " + "\n");

            /*
            // 2011-05-19-임종우 : HMKT1일 경우 H72(장기정체) HOLD 재공 제외 (김권수 요청)
            if (cdvFactory.Text.Trim() == GlobalVariable.gsTestDefaultFactory)
                strSqlString.Append("   AND LOT.HOLD_CODE <> 'H72'  " + "\n");
            */
            /*
            if (cdvLotType.Text != "ALL" && cdvLotType.Text.Trim() != "")
                strSqlString.Append("   AND LOT.LOT_CMF_5 " + cdvLotType.SelectedValueToQueryString + "\n");
            */
            if (cdvLotType.Text != "ALL")
            {
                strSqlString.Append("   AND LOT.LOT_CMF_5 LIKE '" + cdvLotType.Text + "'" + "\n");
            }



            if (txtSearchProduct.Text.Trim() != "%" && txtSearchProduct.Text.Trim() != "")
                strSqlString.AppendFormat("   AND LOT.MAT_ID LIKE '{0}'" + "\n", txtSearchProduct.Text);

            #endregion

            #region 상세 조회에 따른 SQL문 생성
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

            if (udcWIPCondition9.Text != "ALL" && udcWIPCondition9.Text != "")
                strSqlString.AppendFormat("   AND MAT.MAT_GRP_9 {0} " + "\n", udcWIPCondition9.SelectedValueToQueryString);
            #endregion

            strSqlString.AppendFormat("GROUP BY {0} " + "\n", QueryCond2);

            strSqlString.Append("       , LOT.LOT_STATUS, LOT.HOLD_FLAG, LOT.LOT_CMF_5, LOT.START_TIME, LOT.START_RES_ID, LOT.HOLD_CODE, LOT.LAST_COMMENT, LOT.LOT_CMF_17" + "\n");

            // 2010-04-14-임종우 : FACTORY 가 FGS 일 경우 기본 정렬...
            if (cdvFactory.Text == "FGS" || cdvFactory.Text == "HMKE1")
            {
                strSqlString.AppendFormat("ORDER BY {0} " + "\n", QueryCond2);
            }
            else
            {
                strSqlString.AppendFormat("ORDER BY (SELECT TO_NUMBER(OPER_CMF_2) FROM MWIPOPRDEF WHERE FACTORY " + cdvFactory.SelectedValueToQueryString + " AND OPER=LOT.OPER), {0} " + "\n", QueryCond2);
            }

            if (GlobalVariable.gsUserGroup == "ADMIN_GROUP" || GlobalVariable.gsUserGroup == "HANA_ADMIN_GROUP")
            {
                System.Windows.Forms.Clipboard.SetText(strSqlString.ToString());
            }

            return strSqlString.ToString();
        }
        #endregion

        #endregion

        #region Event

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
            
            //cdvLotType.sFactory = cdvFactory.txtValue;
            cdvOper.sFactory = cdvFactory.txtValue;
        }
    }
}

