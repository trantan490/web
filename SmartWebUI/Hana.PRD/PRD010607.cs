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
    public partial class PRD010607 : Miracom.SmartWeb.UI.Controls.udcCUSReport002
    {
        /// <summary>
        /// 클  래  스: PRD010607<br/>
        /// 클래스요약: 반품 이력 조회<br/>
        /// 작  성  자: 하나마이크론 임종우<br/>
        /// 최초작성일: 2009-03-09<br/>
        /// 상세  설명: 반품 이력 조회<br/>
        /// 변경  내용: <br/>
        /// 변  경  자: <br />
        /// 2013-10-17-김민우: LOT TYPE ALL, P%, E% 구분으로변경
        /// <br />
        /// </summary>
        /// 
        public PRD010607()
        {
            InitializeComponent();
            cdvFromToDate.AutoBinding();
            SortInit();           
            GridColumnInit();            
        }

        #region 초기화 및 유효성 검사
        /// <summary 1. 유효성 검사>
        ///
        /// </summary>        
        private Boolean CheckField()
        {
            if (cdvFactory.Text.TrimEnd() == "")
            {
                CmnFunction.ShowMsgBox(RptMessages.GetMessage("STD001", GlobalVariable.gcLanguage));
                return false;
            }

            return true;
        }

        /// <summary 2. 헤더 생성> 
        ///
        /// </summary>
        private void GridColumnInit()
        {
            spdData.ActiveSheet.ColumnHeader.Rows.Count = 1;

            try
            {
                spdData.RPT_ColumnInit();
                spdData.RPT_AddBasicColumn("Customer", 0, 0, Visibles.True, Frozen.True, Align.Center, Merge.True, Formatter.String, 60);
                spdData.RPT_AddBasicColumn("Family", 0, 1, Visibles.False, Frozen.True, Align.Center, Merge.True, Formatter.String, 60);
                spdData.RPT_AddBasicColumn("Package", 0, 2, Visibles.False, Frozen.True, Align.Center, Merge.True, Formatter.String, 60);
                spdData.RPT_AddBasicColumn("Type1", 0, 3, Visibles.False, Frozen.True, Align.Center, Merge.True, Formatter.String, 60);
                spdData.RPT_AddBasicColumn("Type2", 0, 4, Visibles.False, Frozen.True, Align.Center, Merge.True, Formatter.String, 60);
                spdData.RPT_AddBasicColumn("LD Count", 0, 5, Visibles.False, Frozen.True, Align.Center, Merge.True, Formatter.String, 60);
                spdData.RPT_AddBasicColumn("Density", 0, 6, Visibles.False, Frozen.True, Align.Center, Merge.True, Formatter.String, 60);
                spdData.RPT_AddBasicColumn("Generation", 0, 7, Visibles.False, Frozen.True, Align.Center, Merge.True, Formatter.String, 70);
                spdData.RPT_AddBasicColumn("Pin Type", 0, 8, Visibles.True, Frozen.True, Align.Left, Merge.True, Formatter.String, 70);
                spdData.RPT_AddBasicColumn("Product", 0, 9, Visibles.True, Frozen.True, Align.Left, Merge.True, Formatter.String, 70);
                spdData.RPT_AddBasicColumn("Cust Device", 0, 10, Visibles.False, Frozen.True, Align.Left, Merge.True, Formatter.String, 70);

                spdData.RPT_AddBasicColumn("Step", 0, 11, Visibles.True, Frozen.True, Align.Center, Merge.True, Formatter.String, 60);
                spdData.RPT_AddBasicColumn("Lot Id", 0, 12, Visibles.True, Frozen.True, Align.Left, Merge.True, Formatter.String, 70);
                spdData.RPT_AddBasicColumn("Wafer Qty", 0, 13, Visibles.True, Frozen.False, Align.Center, Merge.False, Formatter.Number, 60);
                spdData.RPT_AddBasicColumn("Qty", 0, 14, Visibles.True, Frozen.False, Align.Center, Merge.False, Formatter.Number, 60);
                spdData.RPT_AddBasicColumn("Type", 0, 15, Visibles.True, Frozen.False, Align.Center, Merge.False, Formatter.String, 60);
                spdData.RPT_AddBasicColumn("Return Time", 0, 16, Visibles.True, Frozen.False, Align.Center, Merge.False, Formatter.String, 140);
                spdData.RPT_AddBasicColumn("Operator", 0, 17, Visibles.True, Frozen.False, Align.Center, Merge.False, Formatter.String, 70);                                
                spdData.RPT_AddBasicColumn("Return Site", 0, 18, Visibles.True, Frozen.False, Align.Center, Merge.False, Formatter.String, 70);
                spdData.RPT_AddBasicColumn("Comment", 0, 19, Visibles.True, Frozen.False, Align.Left, Merge.False, Formatter.String, 70);                

                spdData.RPT_ColumnConfigFromTable(btnSort); //Group항목이 있을경우 반드시 선언해줄것.
            }

            catch (Exception ex)
            {
                CmnFunction.ShowMsgBox(ex.Message);
            }
        }

        /// <summary 3. SortInit>
        /// 
        /// </summary>
        private void SortInit()
        {
            ((udcTableForm)(this.btnSort.BindingForm)).AddRow("Customer", "MAT_GRP_1","(SELECT DATA_1 FROM MGCMTBLDAT WHERE TABLE_NAME = 'H_CUSTOMER' AND KEY_1 = MAT.MAT_GRP_1 AND ROWNUM=1) AS CUSTOMER", true);
            ((udcTableForm)(this.btnSort.BindingForm)).AddRow("Family", "MAT.MAT_GRP_2", "MAT.MAT_GRP_2 AS FAMILY", false);
            ((udcTableForm)(this.btnSort.BindingForm)).AddRow("Package", "MAT.MAT_GRP_3", "MAT.MAT_GRP_3 AS PACKAGE", false);
            ((udcTableForm)(this.btnSort.BindingForm)).AddRow("Type1", "MAT.MAT_GRP_4", "MAT.MAT_GRP_4 AS TYPE1", false);
            ((udcTableForm)(this.btnSort.BindingForm)).AddRow("Type2", "MAT.MAT_GRP_5", "MAT.MAT_GRP_5 AS TYPE2", false);
            ((udcTableForm)(this.btnSort.BindingForm)).AddRow("LD Count", "MAT.MAT_GRP_6", "MAT.MAT_GRP_6 AS LD_COUNT", false);
            ((udcTableForm)(this.btnSort.BindingForm)).AddRow("Density", "MAT.MAT_GRP_7", "MAT.MAT_GRP_7 AS DENSITY", false);
            ((udcTableForm)(this.btnSort.BindingForm)).AddRow("Generation", "MAT.MAT_GRP_8", "MAT.MAT_GRP_8 AS GENERATION", false);
            ((udcTableForm)(this.btnSort.BindingForm)).AddRow("Pin Type", "MAT.MAT_CMF_10", "MAT.MAT_CMF_10 AS PIN_TYPE", true);
            ((udcTableForm)(this.btnSort.BindingForm)).AddRow("Product", "HIS.MAT_ID", "HIS.MAT_ID AS PRODUCT", true);
            ((udcTableForm)(this.btnSort.BindingForm)).AddRow("Cust Device", "MAT.MAT_CMF_7", "MAT.MAT_CMF_7 AS CUST_DEVICE", false);     
        }
        #endregion
        

        #region SQL 쿼리 Build
        /// <summary 4. SQL 쿼리 Build>
        /// 
        /// </summary>
        /// <returns> strSqlString </returns>
      
        private string MakeSqlString()
        {
            StringBuilder strSqlString = new StringBuilder();

            string QueryCond1 = null;
            string QueryCond2 = null;

            string sStart_Tran_Time = string.Empty;
            string sEnd_Tran_Time = string.Empty;
            string bbbb = string.Empty;

            //  하나, 고객사
            bool isHana = rbtHana.Checked;
         
            udcTableForm tableForm = (udcTableForm)btnSort.BindingForm;
            QueryCond1 = tableForm.SelectedValueToQueryContainNull;            
            QueryCond2 = tableForm.SelectedValue2ToQueryContainNull;

            sStart_Tran_Time = cdvFromToDate.Start_Tran_Time;
            sEnd_Tran_Time = cdvFromToDate.End_Tran_Time;
                 
         
            strSqlString.AppendFormat("SELECT {0}, " + "\n", QueryCond2);
            strSqlString.Append("       HIS.OLD_OPER AS OPER, " + "\n");
            strSqlString.Append("       HIS.LOT_ID AS LOT_ID, " + "\n");
            strSqlString.Append("       HIS.QTY_2 AS WAFER_QTY, " + "\n");
            strSqlString.Append("       HIS.QTY_1 AS CHIP_QTY, " + "\n");
            strSqlString.Append("       HIS.LOT_CMF_5 AS LOT_TYPE, " + "\n");
            strSqlString.Append("       TO_CHAR(TO_DATE(HIS.TRAN_TIME, 'YYYYMMDDHH24MISS'),'YYYY-MM-DD PM HH12:MI:SS') AS RETURN_TIME, " + "\n");
            strSqlString.Append("       USR.USER_DESC || '(' || HIS.TRAN_USER_ID || ')' AS USER_ID, " + "\n");
            strSqlString.Append("       HIS.LOT_CMF_11 AS RETURN_SITE, " + "\n");
            strSqlString.Append("       HIS.TRAN_COMMENT " + "\n");
            strSqlString.Append("FROM   RWIPLOTHIS HIS, " + "\n");
            strSqlString.Append("       MWIPMATDEF MAT, " + "\n");
            strSqlString.Append("       RWEBUSRDEF USR " + "\n");
            strSqlString.Append("WHERE  1=1 " + "\n");
            strSqlString.Append("       AND HIS.MAT_ID=MAT.MAT_ID " + "\n");
            strSqlString.Append("       AND HIS.TRAN_TIME BETWEEN '" + sStart_Tran_Time + "' AND '" + sEnd_Tran_Time + "' " + "\n");
            strSqlString.Append("       AND HIS.OLD_FACTORY=MAT.FACTORY " + "\n");
            strSqlString.Append("       AND HIS.TRAN_USER_ID=USR.USER_ID " + "\n");
            strSqlString.Append("       AND HIS.OLD_FACTORY " + cdvFactory.SelectedValueToQueryString + "\n");

            if (isHana)
            {
                strSqlString.Append("       AND HIS.FACTORY='RETURN' " + "\n");
                strSqlString.Append("       AND HIS.TRAN_CODE='SHIP' " + "\n");
            }
            else
            {
                strSqlString.Append("       AND HIS.OLD_OPER IN ('AZ010','TZ010','EZ010') " + "\n");
                strSqlString.Append("       AND HIS.TRAN_CODE='CREATE' " + "\n");
                //strSqlString.Append("       AND HIS.CREATE_CODE='RETN' " + "\n");
            }
            //if(cdvFactory.txtValue == GlobalVariable.gsAssyDefaultFactory)
            //{
            //    strSqlString.Append("                   AND OPER IN ('A0000', 'A000N') " + "\n");
            //}
            //else if(cdvFactory.txtValue == GlobalVariable.gsTestDefaultFactory)
            //{
            //    strSqlString.Append("                   AND OPER IN ('T0000', 'T000N') " + "\n");
            //}
            strSqlString.Append("       AND HIS.HIST_DEL_FLAG=' '" + "\n");
            //strSqlString.Append("       AND HIS.LOT_CMF_5 " + cdvLotType.SelectedValueToQueryString + "\n");
            if (cdvLotType.Text != "ALL")
            {
                strSqlString.Append("       AND HIS.LOT_CMF_5 LIKE '" + cdvLotType.Text + "'" + "\n");
            }

            strSqlString.Append("       AND HIS.MAT_ID LIKE '" + txtProduct.Text.ToString().Trim() + "' " + "\n");
            
            //상세 조회에 따른 SQL문 생성                        
            if (udcWIPCondition1.Text != "ALL" && udcWIPCondition1.Text != "")
                strSqlString.AppendFormat("           AND MAT.MAT_GRP_1 {0} " + "\n", udcWIPCondition1.SelectedValueToQueryString);

            if (udcWIPCondition2.Text != "ALL" && udcWIPCondition2.Text != "")
                strSqlString.AppendFormat("           AND MAT.MAT_GRP_2 {0} " + "\n", udcWIPCondition2.SelectedValueToQueryString);

            if (udcWIPCondition3.Text != "ALL" && udcWIPCondition3.Text != "")
                strSqlString.AppendFormat("           AND MAT.MAT_GRP_3 {0} " + "\n", udcWIPCondition3.SelectedValueToQueryString);

            if (udcWIPCondition4.Text != "ALL" && udcWIPCondition4.Text != "")
                strSqlString.AppendFormat("           AND MAT.MAT_GRP_4 {0} " + "\n", udcWIPCondition4.SelectedValueToQueryString);

            if (udcWIPCondition5.Text != "ALL" && udcWIPCondition5.Text != "")
                strSqlString.AppendFormat("           AND MAT.MAT_GRP_5 {0} " + "\n", udcWIPCondition5.SelectedValueToQueryString);

            if (udcWIPCondition6.Text != "ALL" && udcWIPCondition6.Text != "")
                strSqlString.AppendFormat("           AND MAT.MAT_GRP_6 {0} " + "\n", udcWIPCondition6.SelectedValueToQueryString);

            if (udcWIPCondition7.Text != "ALL" && udcWIPCondition7.Text != "")
                strSqlString.AppendFormat("           AND MAT.MAT_GRP_7 {0} " + "\n", udcWIPCondition7.SelectedValueToQueryString);

            if (udcWIPCondition8.Text != "ALL" && udcWIPCondition8.Text != "")
                strSqlString.AppendFormat("           AND MAT.MAT_GRP_8 {0} " + "\n", udcWIPCondition8.SelectedValueToQueryString);

            if (udcWIPCondition9.Text != "ALL" && udcWIPCondition9.Text != "")
                strSqlString.AppendFormat("           AND MAT.MAT_GRP_9 {0} " + "\n", udcWIPCondition9.SelectedValueToQueryString);

            //strSqlString.AppendFormat( "      GROUP BY {0}" + "\n", QueryCond1);
            //strSqlString.Append("              , LOT_CMF_3, OPER, LOT_ID, LOT_CMF_5, OPER_IN_TIME, LOT_CMF_6, TRAN_USER_ID " + "\n");

            strSqlString.AppendFormat("ORDER BY {0} " + "\n", QueryCond1);
            strSqlString.Append("       , HIS.OLD_FACTORY, HIS.OPER, HIS.TRAN_TIME, HIS.LOT_ID " + "\n");

            if (GlobalVariable.gsUserGroup == "ADMIN_GROUP" || GlobalVariable.gsUserGroup == "HANA_ADMIN_GROUP")
            {
                System.Windows.Forms.Clipboard.SetText(strSqlString.ToString());
            }

            return strSqlString.ToString();
        }
        #endregion


        #region EVENT 처리
        /// <summary 5. View>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnView_Click(object sender, EventArgs e)
        {
            DataTable dt = null;
         
            if (CheckField() == false) return;
            
            spdData_Sheet1.RowCount = 0;
            
            try
            {
                LoadingPopUp.LoadIngPopUpShow(this);              
                this.Refresh();
                
                dt = CmnFunction.oComm.GetFuncDataTable("DYNAMIC", MakeSqlString());
                
                GridColumnInit();

                if (dt.Rows.Count == 0)
                {
                    dt.Dispose();
                    LoadingPopUp.LoadingPopUpHidden();
                    CmnFunction.ShowMsgBox(RptMessages.GetMessage("STD010", GlobalVariable.gcLanguage));
                    return;
                }

                //그루핑 순서 1순위만 SubTotal을 사용하기 위해서 첫번째 값을 가져옴
                int sub = ((udcTableForm)(this.btnSort.BindingForm)).GetCheckedValue(2);
                                
                //by John
                //1.Griid 합계 표시
                int[] rowType = spdData.RPT_DataBindingWithSubTotal(dt, 0, sub+1, 13, null, null, btnSort);
                //                  토탈 시작할 셀넘버, 시작 부터 서브토탈 몇개 쓸건지, 첫셀부터 몇번째 셀까지 TOTAL 적용안함

                //2. 칼럼 고정(필요하다면..)
                //spdData.Sheets[0].FrozenColumnCount = 11;

                //by John (두줄짜리이상 + 부분합계.)
                //1.Griid 합계 표시
                //int[] rowType = spdData.RPT_DataBindingWithSubTotalAndDivideRows(dt, 0, 1, 9, 10, 4, udcCUSFromToCondition1.CountFromToValue, btnSort);
                //int[] rowType = spdData.RPT_DataBindingWithSubTotalAndDivideRows(dt, 0, 1, 9, 10, 4, udcCUSCondition1.SelectCount, btnSort);
                //// 서브토탈시작할 컬럼, 서브토탈 몇개쓸껀지, 몇 컬럼까지 서브토탈 ~ 까지 안함, 자동채움시작컬럼 전컬럼, 디바이드 로우수                                                                                        
                
                //구분항목 값 생성(구분이 들어가는 위치임. 0부터 시작)                

                //3. Total부분 셀머지
                spdData.RPT_FillDataSelectiveCells("Total", 0, 13, 0, 1, true, Align.Center, VerticalAlign.Center);
                
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

        /// <summary 6. Excel Export>
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
    }
}
