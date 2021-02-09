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
    public partial class YLD040701 : Miracom.SmartWeb.UI.Controls.udcCUSReport002
    {

        private DataTable dtOper = null;
        private DataTable dtOperGrp = null;

        /// <summary>
        /// 클  래  스: YLD040701<br/>
        /// 클래스요약: DC YIELD 조회<br/>
        /// 작  성  자: 김민우<br/>
        /// 최초작성일: 2010-09-09<br/>
        /// 상세  설명: DC YIELD 조회.<br/>
        /// 변경  내용: <br/>
        /// 변  경  자: <br />
        /// Excel Export 저장 기능 변경<br />
        /// </summary>
        public YLD040701()
        {
            InitializeComponent();

            dtOper = new DataTable();
            dtOperGrp = new DataTable();

            SortInit();
            GridColumnInit(); //헤더 한줄짜리

            cdvFromToDate.AutoBinding();
            cdvFromToDate.DaySelector.SelectedValue = "DAY";
            
            this.SetFactory(GlobalVariable.gsAssyDefaultFactory);
            cdvFactory.Text = GlobalVariable.gsAssyDefaultFactory;

        }

        #region SortInit

        /// <summary>
        /// SortInit
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SortInit()
        {

            ((udcTableForm)(this.btnSort.BindingForm)).AddRow("Customer", "CUSTOMER", "CUSTOMER", true);
            ((udcTableForm)(this.btnSort.BindingForm)).AddRow("Family", "FAMILY", "FAMILY", false);
            ((udcTableForm)(this.btnSort.BindingForm)).AddRow("LD Count", "LD_COUNT", "LD_COUNT", true);
            ((udcTableForm)(this.btnSort.BindingForm)).AddRow("Package", "PKG", "PKG", true);
            ((udcTableForm)(this.btnSort.BindingForm)).AddRow("Type1", "TYPE1", "TYPE1", false);
            ((udcTableForm)(this.btnSort.BindingForm)).AddRow("Type2", "TYPE2", "TYPE2", false);
            ((udcTableForm)(this.btnSort.BindingForm)).AddRow("Density", "DENSITY", "DENSITY", false);
            ((udcTableForm)(this.btnSort.BindingForm)).AddRow("Generation", "GENERATION", "GENERATION", false);
            ((udcTableForm)(this.btnSort.BindingForm)).AddRow("Product", "MAT_ID", "MAT_ID", true);
             
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

            spdData.RPT_AddBasicColumn("Customer", 0, 0, Visibles.False, Frozen.True, Align.Center, Merge.False, Formatter.String, 80);
            spdData.RPT_AddBasicColumn("Family", 0, 1, Visibles.False, Frozen.True, Align.Center, Merge.False, Formatter.String, 80);
            spdData.RPT_AddBasicColumn("LD Count", 0, 2, Visibles.False, Frozen.True, Align.Center, Merge.False, Formatter.String, 80);
            spdData.RPT_AddBasicColumn("PKG", 0, 3, Visibles.False, Frozen.True, Align.Center, Merge.False, Formatter.String, 80);
            spdData.RPT_AddBasicColumn("Type1", 0, 4, Visibles.False, Frozen.True, Align.Center, Merge.False, Formatter.String, 80);
            spdData.RPT_AddBasicColumn("Type2", 0, 5, Visibles.False, Frozen.True, Align.Center, Merge.False, Formatter.String, 80);
            spdData.RPT_AddBasicColumn("Density", 0, 6, Visibles.False, Frozen.True, Align.Center, Merge.False, Formatter.String, 80);
            spdData.RPT_AddBasicColumn("Generation", 0, 7, Visibles.False, Frozen.True, Align.Center, Merge.False, Formatter.String, 80);
            spdData.RPT_AddBasicColumn("Product", 0, 8, Visibles.False, Frozen.True, Align.Left, Merge.False, Formatter.String, 80);
            spdData.RPT_AddBasicColumn("RECEIVE_DATE", 0, 9, Visibles.True, Frozen.False, Align.Left, Merge.False, Formatter.String, 80);
            spdData.RPT_AddBasicColumn("LotID", 0, 10, Visibles.True, Frozen.False, Align.Left, Merge.False, Formatter.String, 80);

            spdData.RPT_AddBasicColumn("B/G Start Time", 0, 11, Visibles.True, Frozen.False, Align.Left, Merge.False, Formatter.String, 80);
            spdData.RPT_AddBasicColumn("B/G End Time", 0, 12, Visibles.True, Frozen.False, Align.Left, Merge.False, Formatter.String, 80);
            spdData.RPT_AddBasicColumn("B/G Machine", 0, 13, Visibles.True, Frozen.False, Align.Left, Merge.False, Formatter.String, 80);
            spdData.RPT_AddBasicColumn("B/G Start Q'ty", 0, 14, Visibles.True, Frozen.False, Align.Left, Merge.False, Formatter.Number, 80);
            spdData.RPT_AddBasicColumn("B/G End Q'ty", 0, 15, Visibles.True, Frozen.False, Align.Left, Merge.False, Formatter.Number, 80);
            spdData.RPT_AddBasicColumn("B/G Loss Q'ty", 0, 16, Visibles.True, Frozen.False, Align.Left, Merge.False, Formatter.Number, 80);

            spdData.RPT_AddBasicColumn("Saw Start Time", 0, 17, Visibles.True, Frozen.False, Align.Left, Merge.False, Formatter.String, 80);
            spdData.RPT_AddBasicColumn("Saw End Time", 0, 18, Visibles.True, Frozen.False, Align.Left, Merge.False, Formatter.String, 80);
            spdData.RPT_AddBasicColumn("Saw Machine", 0, 19, Visibles.True, Frozen.False, Align.Left, Merge.False, Formatter.String, 80);
            spdData.RPT_AddBasicColumn("Saw Start Q'ty", 0, 20, Visibles.True, Frozen.False, Align.Left, Merge.False, Formatter.Number, 80);
            spdData.RPT_AddBasicColumn("Saw End Q'ty", 0, 21, Visibles.True, Frozen.False, Align.Left, Merge.False, Formatter.Number, 80);
            spdData.RPT_AddBasicColumn("Saw Loss Q'ty", 0, 22, Visibles.True, Frozen.False, Align.Left, Merge.False, Formatter.Number, 80);

            spdData.RPT_AddBasicColumn("D/A1 Start Time", 0, 23, Visibles.True, Frozen.False, Align.Left, Merge.False, Formatter.String, 80);
            spdData.RPT_AddBasicColumn("D/A1 End Time", 0, 24, Visibles.True, Frozen.False, Align.Left, Merge.False, Formatter.String, 80);
            spdData.RPT_AddBasicColumn("D/A1 Machine", 0, 25, Visibles.True, Frozen.False, Align.Left, Merge.False, Formatter.String, 80);
            spdData.RPT_AddBasicColumn("D/A1 Start Q'ty", 0, 26, Visibles.True, Frozen.False, Align.Left, Merge.False, Formatter.Number, 80);
            spdData.RPT_AddBasicColumn("D/A1 End Q'ty", 0, 27, Visibles.True, Frozen.False, Align.Left, Merge.False, Formatter.Number, 80);
            spdData.RPT_AddBasicColumn("D/A1 Loss Q'ty", 0, 28, Visibles.True, Frozen.False, Align.Left, Merge.False, Formatter.Number, 80);


            spdData.RPT_AddBasicColumn("D/A2 Start Time", 0, 29, Visibles.True, Frozen.False, Align.Left, Merge.False, Formatter.String, 80);
            spdData.RPT_AddBasicColumn("D/A2 End Time", 0, 30, Visibles.True, Frozen.False, Align.Left, Merge.False, Formatter.String, 80);
            spdData.RPT_AddBasicColumn("D/A2 Machine", 0, 31, Visibles.True, Frozen.False, Align.Left, Merge.False, Formatter.String, 80);
            spdData.RPT_AddBasicColumn("D/A2 Start Q'ty", 0, 32, Visibles.True, Frozen.False, Align.Left, Merge.False, Formatter.Number, 80);
            spdData.RPT_AddBasicColumn("D/A2 End Q'ty", 0, 33, Visibles.True, Frozen.False, Align.Left, Merge.False, Formatter.Number, 80);
            spdData.RPT_AddBasicColumn("D/A2 Loss Q'ty", 0, 34, Visibles.True, Frozen.False, Align.Left, Merge.False, Formatter.Number, 80);

            spdData.RPT_AddBasicColumn("D/A3 Start Time", 0, 35, Visibles.True, Frozen.False, Align.Left, Merge.False, Formatter.String, 80);
            spdData.RPT_AddBasicColumn("D/A3 End Time", 0, 36, Visibles.True, Frozen.False, Align.Left, Merge.False, Formatter.String, 80);
            spdData.RPT_AddBasicColumn("D/A3 Machine", 0, 37, Visibles.True, Frozen.False, Align.Left, Merge.False, Formatter.String, 80);
            spdData.RPT_AddBasicColumn("D/A3 Start Q'ty", 0, 38, Visibles.True, Frozen.False, Align.Left, Merge.False, Formatter.Number, 80);
            spdData.RPT_AddBasicColumn("D/A3 End Q'ty", 0, 39, Visibles.True, Frozen.False, Align.Left, Merge.False, Formatter.Number, 80);
            spdData.RPT_AddBasicColumn("D/A3 Loss Q'ty", 0, 40, Visibles.True, Frozen.False, Align.Left, Merge.False, Formatter.Number, 80);

            spdData.RPT_AddBasicColumn("W/B1 Start Time", 0, 41, Visibles.True, Frozen.False, Align.Left, Merge.False, Formatter.String, 80);
            spdData.RPT_AddBasicColumn("W/B1 End Time", 0, 42, Visibles.True, Frozen.False, Align.Left, Merge.False, Formatter.String, 80);
            spdData.RPT_AddBasicColumn("W/B1 Machine", 0, 43, Visibles.True, Frozen.False, Align.Left, Merge.False, Formatter.String, 80);
            spdData.RPT_AddBasicColumn("W/B1 Start Q'ty", 0, 44, Visibles.True, Frozen.False, Align.Left, Merge.False, Formatter.Number, 80);
            spdData.RPT_AddBasicColumn("W/B1 End Q'ty", 0, 45, Visibles.True, Frozen.False, Align.Left, Merge.False, Formatter.Number, 80);
            spdData.RPT_AddBasicColumn("W/B1 Loss Q'ty", 0, 46, Visibles.True, Frozen.False, Align.Left, Merge.False, Formatter.Number, 80);

            spdData.RPT_AddBasicColumn("W/B2 Start Time", 0, 47, Visibles.True, Frozen.False, Align.Left, Merge.False, Formatter.String, 80);
            spdData.RPT_AddBasicColumn("W/B2 End Time", 0, 48, Visibles.True, Frozen.False, Align.Left, Merge.False, Formatter.String, 80);
            spdData.RPT_AddBasicColumn("W/B2 Machine", 0, 49, Visibles.True, Frozen.False, Align.Left, Merge.False, Formatter.String, 80);
            spdData.RPT_AddBasicColumn("W/B2 Start Q'ty", 0, 50, Visibles.True, Frozen.False, Align.Left, Merge.False, Formatter.Number, 80);
            spdData.RPT_AddBasicColumn("W/B2 End Q'ty", 0, 51, Visibles.True, Frozen.False, Align.Left, Merge.False, Formatter.Number, 80);
            spdData.RPT_AddBasicColumn("W/B2 Loss Q'ty", 0, 52, Visibles.True, Frozen.False, Align.Left, Merge.False, Formatter.Number, 80);

            spdData.RPT_AddBasicColumn("W/B3 Start Time", 0, 53, Visibles.True, Frozen.False, Align.Left, Merge.False, Formatter.String, 80);
            spdData.RPT_AddBasicColumn("W/B3 End Time", 0, 54, Visibles.True, Frozen.False, Align.Left, Merge.False, Formatter.String, 80);
            spdData.RPT_AddBasicColumn("W/B3 Machine", 0, 55, Visibles.True, Frozen.False, Align.Left, Merge.False, Formatter.String, 80);
            spdData.RPT_AddBasicColumn("W/B3 Start Q'ty", 0, 56, Visibles.True, Frozen.False, Align.Left, Merge.False, Formatter.Number, 80);
            spdData.RPT_AddBasicColumn("W/B3 End Q'ty", 0, 57, Visibles.True, Frozen.False, Align.Left, Merge.False, Formatter.Number, 80);
            spdData.RPT_AddBasicColumn("W/B3 Loss Q'ty", 0, 58, Visibles.True, Frozen.False, Align.Left, Merge.False, Formatter.Number, 80);

            spdData.RPT_AddBasicColumn("MOLD Start Time", 0, 59, Visibles.True, Frozen.False, Align.Left, Merge.False, Formatter.String, 80);
            spdData.RPT_AddBasicColumn("MOLD End Time", 0, 60, Visibles.True, Frozen.False, Align.Left, Merge.False, Formatter.String, 80);
            spdData.RPT_AddBasicColumn("MOLD Machine", 0, 61, Visibles.True, Frozen.False, Align.Left, Merge.False, Formatter.String, 80);
            spdData.RPT_AddBasicColumn("MOLD Start Q'ty", 0, 62, Visibles.True, Frozen.False, Align.Left, Merge.False, Formatter.Number, 80);
            spdData.RPT_AddBasicColumn("MOLD End Q'ty", 0, 63, Visibles.True, Frozen.False, Align.Left, Merge.False, Formatter.Number, 80);
            spdData.RPT_AddBasicColumn("MOLD Loss Q'ty", 0, 64, Visibles.True, Frozen.False, Align.Left, Merge.False, Formatter.Number, 80);

            spdData.RPT_AddBasicColumn("IN", 0, 65, Visibles.True, Frozen.False, Align.Left, Merge.False, Formatter.Number, 80);
            spdData.RPT_AddBasicColumn("OUT", 0, 66, Visibles.True, Frozen.False, Align.Left, Merge.False, Formatter.Number, 80);
            spdData.RPT_AddBasicColumn("YLD", 0, 67, Visibles.True, Frozen.False, Align.Left, Merge.False, Formatter.Double2, 80);
            spdData.RPT_AddBasicColumn("LOSS", 0, 68, Visibles.True, Frozen.False, Align.Left, Merge.False, Formatter.Number, 80);

            spdData.RPT_AddBasicColumn("DC_IN_QTY", 0, 69, Visibles.True, Frozen.False, Align.Left, Merge.False, Formatter.Number, 80);
            spdData.RPT_AddBasicColumn("DC_OUT_QTY", 0, 70, Visibles.True, Frozen.False, Align.Left, Merge.False, Formatter.Number, 80);
            spdData.RPT_AddBasicColumn("DC_BIN_QTY_6", 0, 71, Visibles.True, Frozen.False, Align.Left, Merge.False, Formatter.Number, 80);
            spdData.RPT_AddBasicColumn("DC_BIN_QTY_7", 0, 72, Visibles.True, Frozen.False, Align.Left, Merge.False, Formatter.Number, 80);
            spdData.RPT_AddBasicColumn("DC_BIN_QTY_8", 0, 73, Visibles.True, Frozen.False, Align.Left, Merge.False, Formatter.Number, 80);
            spdData.RPT_AddBasicColumn("DC_YLD", 0, 74, Visibles.True, Frozen.False, Align.Left, Merge.False, Formatter.Double2, 80);




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

                if (cdvCustomer.Text.Equals("") || cdvCustomer.Text.Equals("ALL"))
                {
                    CmnFunction.ShowMsgBox(RptMessages.GetMessage("STD038", GlobalVariable.gcLanguage));
                    return;
                }

                if (cdvFromToDate.DaySelector.SelectedValue.ToString() == "MONTH")
                {
                    if (txtSearchProduct.Text.Length < 3 && txtLotID.Text.Length < 3)
                    {

                        CmnFunction.ShowMsgBox(RptMessages.GetMessage("STD077", GlobalVariable.gcLanguage));
                        return;

                    }
                }
                else if (cdvFromToDate.DaySelector.SelectedValue.ToString() == "DAY")
                {
                    
                }
                else
                {
                    if (txtSearchProduct.Text.Length < 3 && txtLotID.Text.Length < 3)
                    {
                        CmnFunction.ShowMsgBox(RptMessages.GetMessage("STD077", GlobalVariable.gcLanguage));
                        return;
                    }

                    
                }
                
                
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
                int[] rowType = spdData.RPT_DataBindingWithSubTotal(dt, 0, sub + 1, 9, null, null, btnSort);

                ////2. 칼럼 고정(필요하다면..)
                spdData.Sheets[0].FrozenColumnCount = 11;

                ////3. Total부분 셀머지
                spdData.RPT_FillDataSelectiveCells("Total", 0, 11, 0, 1, true, Align.Center, VerticalAlign.Center);
                //4. Column Auto Fit
                spdData.RPT_AutoFit(false);

                //5. 데이타가 0인 부분은 제거(Add by John. 2009.1.13)
                //spdData.RPT_RemoveZeroColumn(15, 1);

                spdData.ActiveSheet.Cells[0, 67].Value = (Convert.ToDouble(spdData.ActiveSheet.Cells[0, 66].Value) / Convert.ToDouble(spdData.ActiveSheet.Cells[0, 65].Value)) * 100;
                spdData.ActiveSheet.Cells[0, 74].Value = (Convert.ToDouble(spdData.ActiveSheet.Cells[0, 70].Value) / Convert.ToDouble(spdData.ActiveSheet.Cells[0, 69].Value)) * 100;
                SetAvgVertical(1, 67, true);
                SetAvgVertical(1, 74, true); 
                                
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

        //2010-09-18-김민우 YLD 구하기
        public void SetAvgVertical(int nSampleNormalRowPos, int nColPos, bool bWithNull)
        {
            Color color = spdData.ActiveSheet.Cells[nSampleNormalRowPos, nColPos].BackColor;

            for (int i = 1; i < spdData.ActiveSheet.Rows.Count; i++)
            {
                if (spdData.ActiveSheet.Cells[i, nColPos].BackColor != color)
                {
                    if (nColPos == 74)
                    {
                        //spdData.ActiveSheet.Cells[i, nColPos].Value = Convert.ToDecimal((Convert.ToDouble(spdData.ActiveSheet.Cells[i, 70].Value) / Convert.ToDouble(spdData.ActiveSheet.Cells[i, 69].Value))) * 100;
                        spdData.ActiveSheet.Cells[i, nColPos].Value = Convert.ToDouble((Convert.ToDouble(spdData.ActiveSheet.Cells[i, 70].Value) / Convert.ToDouble(spdData.ActiveSheet.Cells[i, 69].Value))) * 100;
                    }
                    else
                    {
                        //spdData.ActiveSheet.Cells[i, nColPos].Value = Convert.ToDecimal((Convert.ToDouble(spdData.ActiveSheet.Cells[i, 66].Value) / Convert.ToDouble(spdData.ActiveSheet.Cells[i, 65].Value))) * 100;
                        spdData.ActiveSheet.Cells[i, nColPos].Value = Convert.ToDouble((Convert.ToDouble(spdData.ActiveSheet.Cells[i, 70].Value) / Convert.ToDouble(spdData.ActiveSheet.Cells[i, 69].Value))) * 100;
                    }
                }
                          
            }
           

            return;
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
            
            string[] strDate = cdvFromToDate.getSelectDate();
            int LastDate = strDate.Length;

            // Decode 반복문 셋팅
            string strDecode = string.Empty;
          
            
            strSqlString.Append("        SELECT " + QueryCond1 + " ,WORK_DATE, LOT_ID " + "\n");
            strSqlString.Append("        , BG_START_TIME, BG_END_TIME, BG_RES_ID, BG_IN_QTY, BG_OUT_QTY, BG_LOSS_QTY " + "\n");
            strSqlString.Append("        , SAW_START_TIME, SAW_END_TIME, SAW_RES_ID, SAW_IN_QTY, SAW_OUT_QTY, SAW_LOSS_QTY " + "\n");
            strSqlString.Append("        , DA1_START_TIME, DA1_END_TIME, DA1_RES_ID, DA1_IN_QTY, DA1_OUT_QTY, DA1_LOSS_QTY " + "\n");
            strSqlString.Append("        , DA2_START_TIME, DA2_END_TIME, DA2_RES_ID, DA2_IN_QTY, DA2_OUT_QTY, DA2_LOSS_QTY " + "\n");
            strSqlString.Append("        , DA3_START_TIME, DA3_END_TIME, DA3_RES_ID, DA3_IN_QTY, DA3_OUT_QTY, DA3_LOSS_QTY " + "\n");
            strSqlString.Append("        , WB1_START_TIME, WB1_END_TIME, WB1_RES_ID, WB1_IN_QTY, WB1_OUT_QTY, WB1_LOSS_QTY " + "\n");
            strSqlString.Append("        , WB2_START_TIME, WB2_END_TIME, WB2_RES_ID, WB2_IN_QTY, WB2_OUT_QTY, WB2_LOSS_QTY " + "\n");
            strSqlString.Append("        , WB3_START_TIME, WB3_END_TIME, WB3_RES_ID, WB3_IN_QTY, WB3_OUT_QTY, WB3_LOSS_QTY " + "\n");
            strSqlString.Append("        , MD_START_TIME, MD_END_TIME, MD_RES_ID, MD_IN_QTY, MD_OUT_QTY, MD_LOSS_QTY " + "\n");
            strSqlString.Append("        , TOTAL_IN_QTY, TOTAL_OUT_QTY, TOTAL_YLD, TOTAL_LOSS " + "\n");
            strSqlString.Append("        , DC_IN_QTY, DC_OUT_QTY, DC_BIN_QTY_6, DC_BIN_QTY_7, DC_BIN_QTY_8, DC_YLD " + "\n");
            strSqlString.Append("        FROM RSUMDCLHIS A, MWIPCALDEF B" + "\n");
            strSqlString.Append("       WHERE 1=1 " + "\n");

            #region 상세 조회에 따른 SQL문 생성
            if (udcWIPCondition1.Text != "ALL" && udcWIPCondition1.Text != "")
                //strSqlString.AppendFormat("   AND CUSTOMER {0} " + "\n", udcWIPCondition1.SelectedValueToQueryString);

            if (udcWIPCondition2.Text != "ALL" && udcWIPCondition2.Text != "")
                strSqlString.AppendFormat("   AND FAMILY {0} " + "\n", udcWIPCondition2.SelectedValueToQueryString);

            if (udcWIPCondition3.Text != "ALL" && udcWIPCondition3.Text != "")
                strSqlString.AppendFormat("   AND PKG {0} " + "\n", udcWIPCondition3.SelectedValueToQueryString);

            if (udcWIPCondition4.Text != "ALL" && udcWIPCondition4.Text != "")
                strSqlString.AppendFormat("   AND TYPE1 {0} " + "\n", udcWIPCondition4.SelectedValueToQueryString);

            if (udcWIPCondition5.Text != "ALL" && udcWIPCondition5.Text != "")
                strSqlString.AppendFormat("   AND TYPE2 {0} " + "\n", udcWIPCondition5.SelectedValueToQueryString);

            if (udcWIPCondition6.Text != "ALL" && udcWIPCondition6.Text != "")
                strSqlString.AppendFormat("   AND LD_COUNT {0} " + "\n", udcWIPCondition6.SelectedValueToQueryString);

            if (udcWIPCondition7.Text != "ALL" && udcWIPCondition7.Text != "")
                strSqlString.AppendFormat("   AND DENSITY {0} " + "\n", udcWIPCondition7.SelectedValueToQueryString);

            if (udcWIPCondition8.Text != "ALL" && udcWIPCondition8.Text != "")
                strSqlString.AppendFormat("   AND GENERATION {0} " + "\n", udcWIPCondition8.SelectedValueToQueryString);

            if (udcRASCondition6.Text != "ALL" && udcRASCondition6.Text != "")
            {
                strSqlString.AppendFormat("   AND (BG_RES_ID {0} " + "\n", udcRASCondition6.SelectedValueToQueryString);
                strSqlString.AppendFormat("     OR SAW_RES_ID {0} " + "\n", udcRASCondition6.SelectedValueToQueryString);
                strSqlString.AppendFormat("     OR DA1_RES_ID {0} " + "\n", udcRASCondition6.SelectedValueToQueryString);
                strSqlString.AppendFormat("     OR DA2_RES_ID {0} " + "\n", udcRASCondition6.SelectedValueToQueryString);
                strSqlString.AppendFormat("     OR DA3_RES_ID {0} " + "\n", udcRASCondition6.SelectedValueToQueryString);
                strSqlString.AppendFormat("     OR WB1_RES_ID {0} " + "\n", udcRASCondition6.SelectedValueToQueryString);
                strSqlString.AppendFormat("     OR WB2_RES_ID {0} " + "\n", udcRASCondition6.SelectedValueToQueryString);
                strSqlString.AppendFormat("     OR WB3_RES_ID {0} " + "\n", udcRASCondition6.SelectedValueToQueryString);
                strSqlString.AppendFormat("     OR MD_RES_ID {0} )" + "\n", udcRASCondition6.SelectedValueToQueryString);

            }

            #endregion

            if (cdvCustomer.Text != "" && cdvCustomer.Text != "ALL")
            {
                strSqlString.Append("         AND CUSTOMER = '" + cdvCustomer.Text + "' " + "\n");
            }


            
            strSqlString.Append("         AND LOT_ID LIKE '" + txtLotID.Text + "' " + "\n");
            strSqlString.Append("         AND MAT_ID LIKE '" + txtSearchProduct.Text + "' " + "\n");
            strSqlString.Append("         AND A.WORK_DATE = B.SYS_DATE " + "\n");
            strSqlString.Append("         AND B.CALENDAR_ID = 'HM' " + "\n");
            
            if (cdvFromToDate.DaySelector.SelectedValue.ToString() == "MONTH")
            {
                strSqlString.Append("           AND WORK_MONTH BETWEEN '" + cdvFromToDate.FromYearMonth.Value.ToString("yyyyMM") + "' AND '" + cdvFromToDate.ToYearMonth.Value.ToString("yyyyMM") + "'" + "\n");
            }
            else if (cdvFromToDate.DaySelector.SelectedValue.ToString() == "DAY")
            {
                strSqlString.Append("         AND WORK_DATE >=" + strDate[0] + "\n");
                strSqlString.Append("         AND WORK_DATE <=" + strDate[LastDate - 1] + "\n");
            }
            else
            {
                strSqlString.Append("           AND B.SYS_YEAR||B.PLAN_WEEK BETWEEN '" + cdvFromToDate.HmFromWeek + "' AND '" + cdvFromToDate.HmToWeek + "'" + "\n");
            }

            
            
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

        private void txtSearchProduct_KeyDown(object sender, KeyEventArgs e)
        {

        }

        private void txtSearchProduct_TextChanged(object sender, EventArgs e)
        {

        }
    }
}

