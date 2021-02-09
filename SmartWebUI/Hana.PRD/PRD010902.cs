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
    public partial class PRD010902 : Miracom.SmartWeb.UI.Controls.udcCUSReport002
    {

        private DataTable dtOper = null;
        private DataTable dtOperGrp = null;

        /// <summary>
        /// 클  래  스: PRD010902<br/>
        /// 클래스요약: LOT 별 재공 조회<br/>
        /// 작  성  자: 미라콤 양형석<br/>
        /// 최초작성일: 2008-12-12<br/>
        /// 상세  설명: LOT 별 제공 현황 조회.<br/>
        /// 변경  내용: <br/>
        /// 변  경  자: 하나마이크론 김준용<br />
        /// Excel Export 저장 기능 변경<br />
        /// </summary>
        public PRD010902()
        {
            InitializeComponent();

            dtOper = new DataTable();
            dtOperGrp = new DataTable();

            SortInit();
            GridColumnInit(); //헤더 한줄짜리

            cdvDate.Value = DateTime.Today;
            this.SetFactory(GlobalVariable.gsTestDefaultFactory);
            cdvFactory.Text = GlobalVariable.gsTestDefaultFactory; 
           
        }

        #region SortInit

        /// <summary>
        /// SortInit
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SortInit()
        {
            /*
            ((udcTableForm)(this.btnSort.BindingForm)).AddRow("Sales Code", "GCM.DATA_1 as Customer", "GCM.DATA_1", true);
            ((udcTableForm)(this.btnSort.BindingForm)).AddRow("Family", "MAT.MAT_GRP_2 as Family", "MAT.MAT_GRP_2", false);
            ((udcTableForm)(this.btnSort.BindingForm)).AddRow("Package", "MAT.MAT_GRP_3 as Package", "MAT.MAT_GRP_3", false);
            ((udcTableForm)(this.btnSort.BindingForm)).AddRow("Type1", "MAT.MAT_GRP_4 as Type1", "MAT.MAT_GRP_4", false);
            ((udcTableForm)(this.btnSort.BindingForm)).AddRow("Type2", "MAT.MAT_GRP_5 as Type2", "MAT.MAT_GRP_5", false);
            ((udcTableForm)(this.btnSort.BindingForm)).AddRow("Lead", "MAT.MAT_GRP_6 as Lead", "MAT.MAT_GRP_6", false);
            ((udcTableForm)(this.btnSort.BindingForm)).AddRow("Density", "MAT.MAT_GRP_7 as Density", "MAT.MAT_GRP_7", false);
            ((udcTableForm)(this.btnSort.BindingForm)).AddRow("Generation", "MAT.MAT_GRP_8 as Generation", "MAT.MAT_GRP_8", false);
            ((udcTableForm)(this.btnSort.BindingForm)).AddRow("PIN_TYPE", "MAT.MAT_CMF_10 as PIN_TYPE", "MAT.MAT_CMF_10", true);
            ((udcTableForm)(this.btnSort.BindingForm)).AddRow("Product", "LOT.MAT_ID as Product", "LOT.MAT_ID", true);
            ((udcTableForm)(this.btnSort.BindingForm)).AddRow("CUST_DEVICE", "MAT.MAT_CMF_7 as CUST_DEVICE", "MAT.MAT_CMF_7", false);
        */
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
            spdData.RPT_AddBasicColumn("Sales Code", 0, 0, Visibles.True, Frozen.True, Align.Center, Merge.True, Formatter.String, 80);
            spdData.RPT_AddBasicColumn("TEST QUE WIP Qty", 0, 1, Visibles.True, Frozen.True, Align.Center, Merge.True, Formatter.Number, 110);
            spdData.RPT_AddBasicColumn("FT Start", 0, 2, Visibles.True, Frozen.True, Align.Center, Merge.True, Formatter.Number, 80);
            spdData.RPT_AddBasicColumn("FT Out", 0, 3, Visibles.True, Frozen.True, Align.Center, Merge.True, Formatter.Number, 80);
            spdData.RPT_AddBasicColumn("FT WIP Qty", 0, 4, Visibles.True, Frozen.True, Align.Center, Merge.True, Formatter.Number, 80);
            spdData.RPT_AddBasicColumn("FT WIP-HOLD Qty", 0, 5, Visibles.True, Frozen.True, Align.Center, Merge.True, Formatter.Number, 100);
            spdData.RPT_AddBasicColumn("EOL WIP Qty", 0, 6, Visibles.True, Frozen.True, Align.Center, Merge.True, Formatter.Number, 80);
            spdData.RPT_AddBasicColumn("FG WIP Qty", 0, 7, Visibles.True, Frozen.True, Align.Center, Merge.True, Formatter.Number, 80);
            
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
                //int sub = ((udcTableForm)(this.btnSort.BindingForm)).GetCheckedValue(2);

                ////by John (한줄짜리)
                ////1.Griid 합계 표시
               // int[] rowType = spdData.RPT_DataBindingWithSubTotal(dt, 0, sub + 1, 6, null, null, btnSort);

                int[] rowType = spdData.RPT_DataBindingWithSubTotal(dt, 0, 0, 1, null, null, btnSort);

                ////2. 칼럼 고정(필요하다면..)
                //spdData.Sheets[0].FrozenColumnCount = 9;

                ////3. Total부분 셀머지
                spdData.RPT_FillDataSelectiveCells("Total", 0, 1, 0, 1, true, Align.Center, VerticalAlign.Center);
                //4. Column Auto Fit
                spdData.RPT_AutoFit(false);

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
            
         
            // Decode 반복문 셋팅
            string strDecode = string.Empty;
          

            // 시간 관련 셋팅
            string strDate = cdvDate.Value.ToString("yyyyMMdd");
            // 전일 TEST(T0100) START, END를 조회 하기 위해           
            DateTime getStartDate1 = Convert.ToDateTime(cdvDate.Value).AddDays(-1);
            DateTime getStartDate2 = Convert.ToDateTime(cdvDate.Value).AddDays(-2);
            string startDate = getStartDate2.ToString("yyyyMMdd") + "215959";
            string endDate = getStartDate1.ToString("yyyyMMdd") + "220000";
            


            // 쿼리
            strSqlString.Append("SELECT MAT_CMF_8 AS SALES_CODE " + "\n");
            // kpcs
            if (chkKpcs.Checked)
            {
                strSqlString.Append("     , ROUND(SUM(DECODE(OPER, 'T0000', QTY_TOTAL, 0))/1000) AS T0 " + "\n");
                strSqlString.Append("     , ROUND(SUM(DECODE(OPER, 'START', FT_START_QTY, 0))/1000) AS FT_START_QTY " + "\n");
                strSqlString.Append("     , ROUND(SUM(DECODE(OPER, 'END', FT_END_QTY, 0))/1000) AS FT_END_QTY " + "\n");
                strSqlString.Append("     , ROUND(SUM(DECODE(OPER " + "\n");
                strSqlString.Append("               , 'T0100', QTY_1, 'T0200', QTY_1, 'T0300', QTY_1, 'T0400', QTY_1 " + "\n");
                strSqlString.Append("               , 'T0500', QTY_1, 'T0540', QTY_1, 'T0550', QTY_1, 'T0560', QTY_1 " + "\n");
                strSqlString.Append("               , 'T0600', QTY_1, 'T0650', QTY_1, 'T0670', QTY_1, 'T0700', QTY_1 " + "\n");
                strSqlString.Append("               , 'T0800', QTY_1, 'T0900', QTY_1, 'T1040', QTY_1, 'T1050', QTY_1 " + "\n");
                strSqlString.Append("               , 'T1060', QTY_1, 0))/1000) AS T1 " + "\n");
                strSqlString.Append("     , ROUND(SUM(DECODE(OPER " + "\n");
                strSqlString.Append("               , 'T0100', HOLD_QTY, 'T0200', HOLD_QTY, 'T0300', HOLD_QTY, 'T0400', HOLD_QTY " + "\n");
                strSqlString.Append("               , 'T0500', HOLD_QTY, 'T0540', HOLD_QTY, 'T0550', HOLD_QTY, 'T0560', HOLD_QTY " + "\n");
                strSqlString.Append("               , 'T0600', HOLD_QTY, 'T0650', HOLD_QTY, 'T0670', HOLD_QTY, 'T0700', HOLD_QTY " + "\n");
                strSqlString.Append("               , 'T0800', HOLD_QTY, 'T0900', HOLD_QTY, 'T1040', HOLD_QTY, 'T1050', HOLD_QTY " + "\n");
                strSqlString.Append("               , 'T1060', HOLD_QTY, 0))/1000) AS T1_HOLD " + "\n");
                strSqlString.Append("     , ROUND(SUM(DECODE(OPER " + "\n");
                strSqlString.Append("               , 'T1080', QTY_TOTAL, 'T1100', QTY_TOTAL, 'T1200', QTY_TOTAL, 'T1300', QTY_TOTAL, 0))/1000) AS T2 " + "\n");
                strSqlString.Append("     , ROUND(SUM(DECODE(OPER, 'TZ010', QTY_TOTAL, 0))/1000) AS TZ " + "\n");
            }
            else
            {
                strSqlString.Append("     , SUM(DECODE(OPER, 'T0000', QTY_TOTAL, 0)) AS T0 " + "\n");
                strSqlString.Append("     , SUM(DECODE(OPER, 'START', FT_START_QTY, 0)) AS FT_START_QTY " + "\n");
                strSqlString.Append("     , SUM(DECODE(OPER, 'END', FT_END_QTY, 0)) AS FT_END_QTY " + "\n");
                strSqlString.Append("     , SUM(DECODE(OPER " + "\n");
                strSqlString.Append("               , 'T0100', QTY_1, 'T0200', QTY_1, 'T0300', QTY_1, 'T0400', QTY_1 " + "\n");
                strSqlString.Append("               , 'T0500', QTY_1, 'T0540', QTY_1, 'T0550', QTY_1, 'T0560', QTY_1 " + "\n");
                strSqlString.Append("               , 'T0600', QTY_1, 'T0650', QTY_1, 'T0670', QTY_1, 'T0700', QTY_1 " + "\n");
                strSqlString.Append("               , 'T0800', QTY_1, 'T0900', QTY_1, 'T1040', QTY_1, 'T1050', QTY_1 " + "\n");
                strSqlString.Append("               , 'T1060', QTY_1, 0)) AS T1 " + "\n");
                strSqlString.Append("     , SUM(DECODE(OPER " + "\n");
                strSqlString.Append("               , 'T0100', HOLD_QTY, 'T0200', HOLD_QTY, 'T0300', HOLD_QTY, 'T0400', HOLD_QTY " + "\n");
                strSqlString.Append("               , 'T0500', HOLD_QTY, 'T0540', HOLD_QTY, 'T0550', HOLD_QTY, 'T0560', HOLD_QTY " + "\n");
                strSqlString.Append("               , 'T0600', HOLD_QTY, 'T0650', HOLD_QTY, 'T0670', HOLD_QTY, 'T0700', HOLD_QTY " + "\n");
                strSqlString.Append("               , 'T0800', HOLD_QTY, 'T0900', HOLD_QTY, 'T1040', HOLD_QTY, 'T1050', HOLD_QTY " + "\n");
                strSqlString.Append("               , 'T1060', HOLD_QTY, 0)) AS T1_HOLD " + "\n");
                strSqlString.Append("     , SUM(DECODE(OPER " + "\n");
                strSqlString.Append("               , 'T1080', QTY_TOTAL, 'T1100', QTY_TOTAL, 'T1200', QTY_TOTAL, 'T1300', QTY_TOTAL, 0)) AS T2 " + "\n");
                strSqlString.Append("     , SUM(DECODE(OPER, 'TZ010', QTY_TOTAL, 0)) AS TZ " + "\n");
            }
            strSqlString.Append("  FROM (" + "\n");
            if (DateTime.Now.ToString("yyyyMMdd") == cdvDate.SelectedValue())
            {
                strSqlString.Append("        SELECT MAT.MAT_CMF_8 " + "\n");
                strSqlString.Append("             , OPER  " + "\n");
                strSqlString.Append("             , SUM(DECODE(HOLD_FLAG,'Y', QTY_1, 0)) AS HOLD_QTY  " + "\n");
                strSqlString.Append("             , SUM(DECODE(HOLD_FLAG,' ', QTY_1, 0)) AS QTY_1  " + "\n");
                strSqlString.Append("             , SUM(QTY_1) AS QTY_TOTAL  " + "\n");
                strSqlString.Append("             , 0 AS FT_START_QTY  " + "\n");
                strSqlString.Append("             , 0 as FT_END_QTY  " + "\n");
                strSqlString.Append("        FROM RWIPLOTSTS LOT " + "\n");
                strSqlString.Append("           , MWIPMATDEF MAT  " + "\n");
                strSqlString.Append("        WHERE 1=1 " + "\n");
                strSqlString.Append("          AND LOT.FACTORY = MAT.FACTORY " + "\n");
                strSqlString.Append("          AND LOT.MAT_ID = MAT.MAT_ID " + "\n");
                strSqlString.Append("          AND LOT.OWNER_CODE = 'PROD' " + "\n");
                strSqlString.Append("          AND LOT.MAT_VER = 1 " + "\n");
                strSqlString.Append("          AND LOT_DEL_FLAG = ' ' " + "\n");
                strSqlString.Append("          AND LOT.OPER IN ('T0000', 'T0100', 'T0200', 'T0300', 'T0400', 'T0500', 'T0540', 'T0550', 'T0560' " + "\n");
                strSqlString.Append("                         , 'T0600', 'T0650', 'T0670', 'T0700', 'T0800', 'T0900', 'T1040', 'T1050', 'T1060' " + "\n");
                strSqlString.Append("                         , 'T1080', 'T1100', 'T1200', 'T1300', 'TZ010') " + "\n");
                strSqlString.Append("          AND LOT_CMF_2 = 'GC' " + "\n");
                strSqlString.Append("          AND MAT.FACTORY = '" + cdvFactory.Text + "' " + "\n");
                strSqlString.Append("          GROUP BY MAT.MAT_CMF_8, OPER " + "\n");
            }
            else
            {
                strSqlString.Append("        SELECT MAT.MAT_CMF_8 " + "\n");
                strSqlString.Append("             , OPER  " + "\n");
                strSqlString.Append("             , SUM(DECODE(HOLD_FLAG,'Y', QTY_1, 0)) AS HOLD_QTY  " + "\n");
                strSqlString.Append("             , SUM(DECODE(HOLD_FLAG,' ', QTY_1, 0)) AS QTY_1  " + "\n");
                strSqlString.Append("             , SUM(QTY_1) AS QTY_TOTAL  " + "\n");
                strSqlString.Append("             , 0 AS FT_START_QTY  " + "\n");
                strSqlString.Append("             , 0 as FT_END_QTY  " + "\n");
                strSqlString.Append("        FROM RWIPLOTSTS_BOH LOT " + "\n");
                strSqlString.Append("           , MWIPMATDEF MAT  " + "\n");
                strSqlString.Append("        WHERE 1=1 " + "\n");
                strSqlString.Append("          AND LOT.CUTOFF_DT = '"+ strDate + "22'" + "\n");
                strSqlString.Append("          AND LOT.FACTORY = MAT.FACTORY " + "\n");
                strSqlString.Append("          AND LOT.MAT_ID = MAT.MAT_ID " + "\n");
                strSqlString.Append("          AND LOT.OWNER_CODE = 'PROD' " + "\n");
                strSqlString.Append("          AND LOT.MAT_VER = 1 " + "\n");
                strSqlString.Append("          AND LOT_DEL_FLAG = ' ' " + "\n");
                strSqlString.Append("          AND LOT.OPER IN ('T0000', 'T0100', 'T0200', 'T0300', 'T0400', 'T0500', 'T0540', 'T0550', 'T0560' " + "\n");
                strSqlString.Append("                         , 'T0600', 'T0650', 'T0670', 'T0700', 'T0800', 'T0900', 'T1040', 'T1050', 'T1060' " + "\n");
                strSqlString.Append("                         , 'T1080', 'T1100', 'T1200', 'T1300', 'TZ010') " + "\n");
                strSqlString.Append("          AND LOT_CMF_2 = 'GC' " + "\n");
                strSqlString.Append("          AND MAT.FACTORY = '" + cdvFactory.Text + "' " + "\n");
                strSqlString.Append("          GROUP BY MAT.MAT_CMF_8, OPER " + "\n");
            }
            strSqlString.Append("        UNION ALL " + "\n");
            strSqlString.Append("        SELECT MAT_CMF_8, 'START' AS OPER, 0 AS HOLD_QTY, 0 AS QTY_1, 0 AS QTY_TOTAL " + "\n");
            strSqlString.Append("             , SUM(QTY_1) as FT_START_QTY " + "\n");
            strSqlString.Append("             , 0 as FT_END_QTY " + "\n");
            strSqlString.Append("        FROM RWIPLOTHIS LOT " + "\n");
            strSqlString.Append("           , MWIPMATDEF MAT  " + "\n");
            strSqlString.Append("        WHERE 1=1 " + "\n");
            strSqlString.Append("          AND LOT.FACTORY = MAT.FACTORY " + "\n");
            strSqlString.Append("          AND LOT.MAT_ID = MAT.MAT_ID " + "\n");
            strSqlString.Append("          AND LOT.OWNER_CODE = 'PROD' " + "\n");
            strSqlString.Append("          AND LOT.MAT_VER = 1 " + "\n");
            strSqlString.Append("          AND LOT_DEL_FLAG = ' ' " + "\n");
            strSqlString.Append("          AND LOT_CMF_2 = 'GC' " + "\n");
            strSqlString.Append("          AND MAT.FACTORY = '" + cdvFactory.Text + "' " + "\n");
            strSqlString.Append("          AND TRAN_TIME BETWEEN '" + startDate + "' AND '" + endDate + "'  " + "\n");
            strSqlString.Append("          AND TRAN_CODE = 'START' " + "\n");
            strSqlString.Append("          AND OPER = 'T0100' " + "\n");
            strSqlString.Append("          GROUP BY MAT.MAT_CMF_8 " + "\n");
            strSqlString.Append("        UNION ALL " + "\n");
            strSqlString.Append("        SELECT MAT_CMF_8, 'END' AS OPER, 0 AS HOLD_QTY, 0 AS QTY_1, 0 AS QTY_TOTAL, 0 AS FT_START_QTY " + "\n");
            strSqlString.Append("             , SUM(QTY_1) as FT_END_QTY " + "\n");
            strSqlString.Append("        FROM RWIPLOTHIS LOT " + "\n");
            strSqlString.Append("           , MWIPMATDEF MAT  " + "\n");
            strSqlString.Append("        WHERE 1=1 " + "\n");
            strSqlString.Append("          AND LOT.FACTORY = MAT.FACTORY " + "\n");
            strSqlString.Append("          AND LOT.MAT_ID = MAT.MAT_ID " + "\n");
            strSqlString.Append("          AND LOT.OWNER_CODE = 'PROD' " + "\n");
            strSqlString.Append("          AND LOT.MAT_VER = 1 " + "\n");
            strSqlString.Append("          AND LOT_DEL_FLAG = ' ' " + "\n");
            strSqlString.Append("          AND LOT_CMF_2 = 'GC' " + "\n");
            strSqlString.Append("          AND MAT.FACTORY = '" + cdvFactory.Text + "' " + "\n");
            strSqlString.Append("          AND TRAN_TIME BETWEEN '" + startDate + "' AND '" + endDate + "'  " + "\n");
            strSqlString.Append("          AND TRAN_CODE = 'END' " + "\n");
            strSqlString.Append("          AND OLD_OPER = 'T0100' " + "\n");
            strSqlString.Append("          GROUP BY MAT.MAT_CMF_8 " + "\n");
            strSqlString.Append("          ORDER BY MAT_CMF_8 " + "\n");
            strSqlString.Append("        ) " + "\n");
            strSqlString.Append("GROUP BY MAT_CMF_8 " + "\n");
            strSqlString.Append("ORDER BY MAT_CMF_8 " + "\n");
            
            if (GlobalVariable.gsUserGroup == "ADMIN_GROUP" || GlobalVariable.gsUserGroup == "HANA_ADMIN_GROUP")
            {
                System.Windows.Forms.Clipboard.SetText(strSqlString.ToString());
            }

            return strSqlString.ToString();
        }

        #endregion

        #endregion

        #region Event Handler

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

        private void rbtBrief_CheckedChanged(object sender, EventArgs e)
        {
            //cdvStep.Visible = !(rbtBrief.Checked);
        }

        #endregion

        private void cdvFactory_ValueSelectedItemChanged(object sender, MCCodeViewSelChanged_EventArgs e)
        {
            this.SetFactory(cdvFactory.txtValue);
           

            StringBuilder strSqlString = new StringBuilder();
            StringBuilder strSqlString1 = new StringBuilder();
            
            strSqlString.Append("SELECT OPER_GRP_1" + "\n");            
            strSqlString.Append("  FROM MWIPOPRDEF " + "\n");
            strSqlString.Append(" WHERE FACTORY " + cdvFactory.SelectedValueToQueryString + "\n");
            strSqlString.Append("   AND OPER_CMF_4 <> ' '    " + "\n");
            strSqlString.Append(" GROUP BY OPER_GRP_1" + "\n");
            strSqlString.Append(" ORDER BY TO_NUMBER(MIN(OPER_CMF_4)) ASC" + "\n");

            dtOperGrp = CmnFunction.oComm.GetFuncDataTable("DYNAMIC", strSqlString.ToString());


            strSqlString1.Append("SELECT OPER " + "\n");
            strSqlString1.Append("  FROM MWIPOPRDEF" + "\n");
            strSqlString1.Append(" WHERE FACTORY " + cdvFactory.SelectedValueToQueryString + "\n");
            strSqlString1.Append("   AND OPER NOT IN ('00001', '00002')   " + "\n");
            strSqlString1.Append(" ORDER BY DECODE(OPER_CMF_2, ' ', 99999, TO_NUMBER(OPER_CMF_2)), OPER" + "\n");            
            
            dtOper = CmnFunction.oComm.GetFuncDataTable("DYNAMIC", strSqlString1.ToString());
        }

        private void spdData_CellClick(object sender, FarPoint.Win.Spread.CellClickEventArgs e)
        {
                
        }
    }
}

