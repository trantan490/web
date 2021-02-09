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
    public partial class PRD010313 : Miracom.SmartWeb.UI.Controls.udcCUSReport002
    {
        /// <summary>
        /// 클  래  스: PRD010313<br/>
        /// 클래스요약: HOLD 조회<br/>
        /// 작  성  자: 하나마이크론 임종우<br/>
        /// 최초작성일: 2011-05-03<br/>
        /// 상세  설명: HOLD 전용 조회 화면<br/>
        /// 변경  내용: <br/>
        
        /// </summary>
        public PRD010313()
        {
            InitializeComponent();            

            SortInit();
            GridColumnInit(); //헤더 한줄짜리
            cdvFromToDate.AutoBinding();
            cdvFromToDate.Visible = false;
                        
            //cdvDate.Value = DateTime.Today;            
        }

        #region SortInit

        /// <summary>
        /// SortInit
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SortInit()
        {
            if (rbtHoldIn.Checked == true)
            {
                ((udcTableForm)(this.btnSort.BindingForm)).AddRow("Customer", "NVL((SELECT DATA_1 FROM MGCMTBLDAT WHERE TABLE_NAME = 'H_CUSTOMER' AND KEY_1 = MAT_GRP_1 AND ROWNUM=1), '-') AS CUSTOMER", "MAT_GRP_1", true);
                ((udcTableForm)(this.btnSort.BindingForm)).AddRow("Family", "MAT_GRP_2 AS FAMILY", "MAT_GRP_2", false);
                ((udcTableForm)(this.btnSort.BindingForm)).AddRow("Package", "MAT_GRP_3 AS PACKAGE", "MAT_GRP_3", false);
                ((udcTableForm)(this.btnSort.BindingForm)).AddRow("Type1", "MAT_GRP_4 AS TYPE1", "MAT_GRP_4", false);
                ((udcTableForm)(this.btnSort.BindingForm)).AddRow("Type2", "MAT_GRP_5 AS TYPE2", "MAT_GRP_5", false);
                ((udcTableForm)(this.btnSort.BindingForm)).AddRow("LD Count", "MAT_GRP_6 AS LDCOUNT", "MAT_GRP_6", false);
                ((udcTableForm)(this.btnSort.BindingForm)).AddRow("Density", "MAT_GRP_7 AS DENSITY", "MAT_GRP_7", false);
                ((udcTableForm)(this.btnSort.BindingForm)).AddRow("Generation", "MAT_GRP_8 AS GENERATION", "MAT_GRP_8", false);
                ((udcTableForm)(this.btnSort.BindingForm)).AddRow("Pin Type", "MAT_CMF_10 AS PIN_TYPE", "MAT_CMF_10", false);
                ((udcTableForm)(this.btnSort.BindingForm)).AddRow("Product", "MAT_ID AS PRODUCT", "MAT_ID", false);
                ((udcTableForm)(this.btnSort.BindingForm)).AddRow("Lot ID", "LOT_ID AS LOT_ID", "LOT_ID", false);
                ((udcTableForm)(this.btnSort.BindingForm)).AddRow("Lot Type", "LOT_CMF_5", "LOT_CMF_5", true);
                ((udcTableForm)(this.btnSort.BindingForm)).AddRow("Hold Code", "HOLD_CODE", "HOLD_CODE", false);
                ((udcTableForm)(this.btnSort.BindingForm)).AddRow("Hold Desc", "HOLD_DESC", "HOLD_DESC", false);
            }
            else
            {
            }
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
            if (rbtHoldIn.Checked == true)
            {
                spdData.ActiveSheet.ColumnHeader.Rows.Count = 1;
                spdData.RPT_ColumnInit();
                spdData.RPT_AddBasicColumn("Customer", 0, 0, Visibles.True, Frozen.True, Align.Center, Merge.True, Formatter.String, 80);
                spdData.RPT_AddBasicColumn("Family", 0, 1, Visibles.False, Frozen.True, Align.Center, Merge.True, Formatter.String, 80);
                spdData.RPT_AddBasicColumn("Package", 0, 2, Visibles.False, Frozen.True, Align.Center, Merge.True, Formatter.String, 80);
                spdData.RPT_AddBasicColumn("Type1", 0, 3, Visibles.False, Frozen.True, Align.Center, Merge.True, Formatter.String, 80);
                spdData.RPT_AddBasicColumn("Type2", 0, 4, Visibles.False, Frozen.True, Align.Center, Merge.True, Formatter.String, 80);
                spdData.RPT_AddBasicColumn("LD Count", 0, 5, Visibles.False, Frozen.True, Align.Center, Merge.True, Formatter.String, 80);
                spdData.RPT_AddBasicColumn("Density", 0, 6, Visibles.False, Frozen.True, Align.Center, Merge.True, Formatter.String, 80);
                spdData.RPT_AddBasicColumn("Generation", 0, 7, Visibles.False, Frozen.True, Align.Center, Merge.True, Formatter.String, 80);
                spdData.RPT_AddBasicColumn("Pin Type", 0, 8, Visibles.False, Frozen.True, Align.Left, Merge.True, Formatter.String, 80);
                spdData.RPT_AddBasicColumn("Product", 0, 9, Visibles.False, Frozen.True, Align.Left, Merge.True, Formatter.String, 80);
                spdData.RPT_AddBasicColumn("Lot ID", 0, 10, Visibles.False, Frozen.True, Align.Left, Merge.True, Formatter.String, 80);
                spdData.RPT_AddBasicColumn("Lot Type", 0, 11, Visibles.True, Frozen.True, Align.Left, Merge.True, Formatter.String, 80);
                spdData.RPT_AddBasicColumn("Hold Code", 0, 12, Visibles.False, Frozen.True, Align.Center, Merge.True, Formatter.String, 80);
                spdData.RPT_AddBasicColumn("Hold Desc", 0, 13, Visibles.False, Frozen.True, Align.Left, Merge.False, Formatter.String, 80);
                spdData.RPT_AddBasicColumn("Lot 수", 0, 14, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 80);
                spdData.RPT_AddBasicColumn("quantity", 0, 15, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 80);
                spdData.RPT_AddBasicColumn("Share", 0, 16, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Double2, 80);
                spdData.RPT_ColumnConfigFromTable(btnSort); //Group항목이 있을경우 반드시 선업해줄것.
            }
            else
            {
                //spdData.ActiveSheet.ColumnHeader.Rows.Count = 1;
                spdData.RPT_ColumnInit();
                spdData.RPT_AddBasicColumn("Step", 0, 0, Visibles.True, Frozen.True, Align.Center, Merge.True, Formatter.String, 80);
                spdData.RPT_AddBasicColumn("Pin Type", 0, 1, Visibles.True, Frozen.False, Align.Center, Merge.True, Formatter.String, 80);
                spdData.RPT_AddBasicColumn("Product", 0, 2, Visibles.True, Frozen.False, Align.Center, Merge.True, Formatter.String, 80);
                spdData.RPT_AddBasicColumn("LOT_ID", 0, 3, Visibles.True, Frozen.False, Align.Center, Merge.True, Formatter.String, 80);
                spdData.RPT_AddBasicColumn("Type", 0, 4, Visibles.True, Frozen.False, Align.Center, Merge.True, Formatter.String, 80);
                spdData.RPT_AddBasicColumn("Hold_Code", 0, 5, Visibles.True, Frozen.False, Align.Center, Merge.False, Formatter.String, 80);
                spdData.RPT_AddBasicColumn("Hold Time", 0, 6, Visibles.True, Frozen.False, Align.Center, Merge.False, Formatter.String, 80);
                spdData.RPT_AddBasicColumn("Release Time", 0, 7, Visibles.True, Frozen.False, Align.Left, Merge.False, Formatter.String, 80);
                spdData.RPT_AddBasicColumn("Hold TAT", 0, 8, Visibles.True, Frozen.False, Align.Center, Merge.False, Formatter.String, 80);
                spdData.RPT_AddBasicColumn("Qty", 0, 9, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 80);
                spdData.RPT_AddBasicColumn("Hold Comment", 0, 10, Visibles.True, Frozen.False, Align.Left, Merge.False, Formatter.String, 80);
                spdData.RPT_AddBasicColumn("Release Comment", 0, 11, Visibles.True, Frozen.False, Align.Left, Merge.False, Formatter.String, 80);
                
                //spdData.RPT_ColumnConfigFromTable(btnSort); //Group항목이 있을경우 반드시 선업해줄것.
            }
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
                if (rbtHoldIn.Checked == true)
                {

                    //그루핑 순서 1순위만 SubTotal을 사용하기 위해서 첫번째 값을 가져옴
                    int sub = ((udcTableForm)(this.btnSort.BindingForm)).GetCheckedValue(2);

                    ////1.Griid 합계 표시
                    int[] rowType = spdData.RPT_DataBindingWithSubTotal(dt, 0, sub + 1, 13, null, null, btnSort);

                    ////3. Total부분 셀머지
                    spdData.RPT_FillDataSelectiveCells("Total", 0, 13, 0, 1, true, Align.Center, VerticalAlign.Center);
                }
                else
                {
                    //spdData.DataSource = dt;

                    int[] rowType = spdData.RPT_DataBindingWithSubTotal(dt, 0, 0, 8, null, null, btnSort);

                    ////3. Total부분 셀머지
                    spdData.RPT_FillDataSelectiveCells("Total", 0, 8, 0, 1, true, Align.Center, VerticalAlign.Center);
                }
                //4. Column Auto Fit
                spdData.RPT_AutoFit(false);

                SetAvgVertical2(8);

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

            //bool isToday = false;
            QueryCond1 = tableForm.SelectedValueToQueryContainNull;
            QueryCond2 = tableForm.SelectedValue2ToQueryContainNull;

            string sStart_Tran_Time = string.Empty;
            string sEnd_Tran_Time = string.Empty;
   
            sStart_Tran_Time = cdvFromToDate.Start_Tran_Time;
            sEnd_Tran_Time = cdvFromToDate.End_Tran_Time;

            //string strDate = cdvDate.Value.ToString("yyyyMMdd");

            //if (DateTime.Now.ToString("yyyyMMdd").Equals(strDate))
            //{
            //    strDate = DateTime.Now.ToString("yyyyMMddHHmmss");
            //    isToday = true;
            //}
            //else
            //{
            //    strDate = strDate + "220000";
            //}
            if (rbtHoldIn.Checked == true)
            {

                strSqlString.AppendFormat("SELECT {0} " + "\n", QueryCond1);
                strSqlString.AppendFormat("     , COUNT(*) AS LOT_CNT  " + "\n");
                strSqlString.AppendFormat("     , SUM(QTY_1) AS QTY_1  " + "\n");
                strSqlString.AppendFormat("     , ROUND((RATIO_TO_REPORT(SUM(QTY_1)) OVER())*100, 2) AS RATIO " + "\n");
                strSqlString.AppendFormat("  FROM (" + "\n");
                strSqlString.AppendFormat("        SELECT MAT.MAT_GRP_1, MAT.MAT_GRP_2, MAT.MAT_GRP_3, MAT.MAT_GRP_4, MAT.MAT_GRP_5, MAT.MAT_GRP_6 " + "\n");
                strSqlString.AppendFormat("             , MAT.MAT_GRP_7, MAT.MAT_GRP_8, MAT.MAT_CMF_10, STS.MAT_ID, STS.LOT_ID, STS.LOT_CMF_5, STS.HOLD_CODE " + "\n");
                strSqlString.AppendFormat("             , (SELECT DATA_1 FROM MGCMTBLDAT WHERE TABLE_NAME = 'HOLD_CODE' AND FACTORY = '" + cdvFactory.Text + "' AND KEY_1 = HOLD_CODE) AS HOLD_DESC " + "\n");
                strSqlString.AppendFormat("             , QTY_1 " + "\n");
                strSqlString.AppendFormat("          FROM RWIPLOTSTS STS" + "\n");
                strSqlString.AppendFormat("             , MWIPMATDEF MAT" + "\n");
                strSqlString.AppendFormat("         WHERE 1=1" + "\n");
                strSqlString.AppendFormat("           AND STS.FACTORY = MAT.FACTORY " + "\n");
                strSqlString.AppendFormat("           AND STS.MAT_ID = MAT.MAT_ID " + "\n");
                strSqlString.AppendFormat("           AND STS.FACTORY {0} \n", cdvFactory.SelectedValueToQueryString);
                strSqlString.AppendFormat("           AND STS.LOT_TYPE = 'W' " + "\n");
                strSqlString.AppendFormat("           AND STS.LOT_DEL_FLAG = ' ' " + "\n");
                strSqlString.AppendFormat("           AND STS.HOLD_FLAG = 'Y' " + "\n");
                strSqlString.AppendFormat("           AND MAT.DELETE_FLAG = ' ' " + "\n");

                #region 상세 조회에 따른 SQL문 생성
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
                #endregion

                // Lot Type
                if (cdvLotType.Text != "ALL" && cdvStep.Text != "")
                    strSqlString.AppendFormat("           AND STS.LOT_CMF_5 LIKE '" + cdvLotType.Text + "'" + "\n");

                // Step
                if (cdvStep.Text != "ALL" && cdvStep.Text != "")
                    strSqlString.AppendFormat("           AND STS.OPER {0}" + "\n", cdvStep.SelectedValueToQueryString);

                // Product
                strSqlString.AppendFormat("           AND STS.MAT_ID LIKE '{0}'" + "\n", txtSearchProduct.Text);


                // Hold Code
                if (cdvHoldCode.Text != "ALL" && cdvHoldCode.Text != "")
                    strSqlString.AppendFormat("           AND STS.HOLD_CODE {0}" + "\n", cdvHoldCode.SelectedValueToQueryString);



                strSqlString.AppendFormat("       )" + "\n");
                strSqlString.AppendFormat(" GROUP BY {0} " + "\n", QueryCond2);
                strSqlString.AppendFormat(" ORDER BY {0} " + "\n", QueryCond2);
            }
            else
            {
                strSqlString.AppendFormat("SELECT HLD.OPER, MAT.MAT_CMF_10 , MAT.MAT_ID, HLD.LOT_ID, HLD.CMF_3, HLD.HOLD_CODE, HOLD_TRAN_TIME, RELEASE_TRAN_TIME" + "\n");
                strSqlString.AppendFormat("     , TRUNC(DECODE(RELEASE_TRAN_TIME, ' ', SYSDATE, TO_DATE(RELEASE_TRAN_TIME, 'YYYYMMDDHH24MISS')) - TO_DATE(HOLD_TRAN_TIME, 'YYYYMMDDHH24MISS'), 2) AS HOLD_TAT" + "\n");
                strSqlString.AppendFormat("     , QTY_1,HOLD_COMMENT,RELEASE_COMMENT" + "\n");
                strSqlString.AppendFormat("  FROM RWIPLOTHLD HLD" + "\n");
                strSqlString.AppendFormat("     , MWIPMATDEF MAT" + "\n");
                strSqlString.AppendFormat(" WHERE HOLD_TRAN_TIME BETWEEN '" + sStart_Tran_Time + "' AND '" + sEnd_Tran_Time + "' " + "\n");
                strSqlString.AppendFormat("   AND HLD.FACTORY {0} \n", cdvFactory.SelectedValueToQueryString);
                strSqlString.AppendFormat("   AND HLD.FACTORY = MAT.FACTORY" + "\n");
                strSqlString.AppendFormat("   AND HLD.MAT_ID = MAT.MAT_ID" + "\n");
                strSqlString.AppendFormat("   AND MAT.MAT_VER = 1" + "\n");


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



                // Lot Type
                if (cdvLotType.Text != "ALL" && cdvStep.Text != "")
                    strSqlString.AppendFormat("   AND HLD.CMF_3 LIKE '" + cdvLotType.Text + "'" + "\n");

                // Step
                if (cdvStep.Text != "ALL" && cdvStep.Text != "")
                    strSqlString.AppendFormat("   AND HLD.OPER {0}" + "\n", cdvStep.SelectedValueToQueryString);

                // Product
                strSqlString.AppendFormat("   AND MAT.MAT_ID LIKE '{0}'" + "\n", txtSearchProduct.Text);

                // Hold Code
                if (cdvHoldCode.Text != "ALL" && cdvHoldCode.Text != "")
                    strSqlString.AppendFormat("   AND HLD.HOLD_CODE {0}" + "\n", cdvHoldCode.SelectedValueToQueryString);
                strSqlString.AppendFormat("ORDER BY HOLD_TRAN_TIME" + "\n");



            }
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
        }

        private void ckbHold_CheckChanged(object sender, EventArgs e)
        {
            if (rbtHoldIn.Checked == false)
            {
                cdvFromToDate.Visible = true;
            }
            else
            {
                cdvFromToDate.Visible = false;
            }

        }

        public void SetAvgVertical2(int nColPos)
        {
            double sum = 0;
            double divide = 0;


            for (int i = 1; i < spdData.ActiveSheet.Rows.Count; i++)
            {
                sum += Convert.ToDouble(spdData.ActiveSheet.Cells[i, nColPos].Value);

                if (spdData.ActiveSheet.Cells[i, nColPos].Value != null)
                {
                    divide += 1;
                }
            }
            if (divide == 0)
            {
                divide = 1;
            }
            spdData.ActiveSheet.Cells[0, nColPos].Value = Math.Round(sum / divide,2);
        }

    }
}

