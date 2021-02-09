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

namespace Hana.MAT
{
    public partial class MAT070201 : Miracom.SmartWeb.UI.Controls.udcCUSReport002
    {
        /// <summary>
        /// 클  래  스: MAT070201<br/>
        /// 클래스요약: 자재 일필요량 조회<br/>
        /// 작  성  자: 하나마이크론 임종우<br/>
        /// 최초작성일: 2009-01-12<br/>
        /// 상세  설명: Product별 자재 일필요량 조회.<br/>
        /// 변경  내용: <br/>
        /// </summary>
        public MAT070201()
        {
            InitializeComponent();            

            //cdvDate.Value = DateTime.Today;
            cboMatType.SelectedIndex = 0;

            SortInit();
            GridColumnInit(); //헤더 한줄짜리
        }

        #region SortInit

        private void SortInit()
        {
            ((udcTableForm)(this.btnSort.BindingForm)).AddRow("Item name", "A.MAT_TYPE", "' '", "A.MAT_TYPE", false);
            ((udcTableForm)(this.btnSort.BindingForm)).AddRow("Item Code", "A.MATCODE", "' '", "A.MATCODE", true);
            ((udcTableForm)(this.btnSort.BindingForm)).AddRow("Item specification", "A.MAT_DESC", "' '", "A.MAT_DESC", true);
            ((udcTableForm)(this.btnSort.BindingForm)).AddRow("BASE QTY", "A.BASE_QTY", "' '", "' '", false);            
            ((udcTableForm)(this.btnSort.BindingForm)).AddRow("A", "A.A_COUNT", "' '", "' '", true);
            ((udcTableForm)(this.btnSort.BindingForm)).AddRow("B", "A.B_COUNT", "' '", "' '", true);
            ((udcTableForm)(this.btnSort.BindingForm)).AddRow("Equipment", "A.C_COUNT", "' '", "' '", true);
            ((udcTableForm)(this.btnSort.BindingForm)).AddRow("TOTAL(SP)", "A.TOTAL_COUNT", "' '", "' '", true);
            ((udcTableForm)(this.btnSort.BindingForm)).AddRow("TOTAL(QTY)", "A.TOTAL_QTY", "' '", "' '", true);
            ((udcTableForm)(this.btnSort.BindingForm)).AddRow("USAGE", "A.USAGE", "' '", "' '", true);
            ((udcTableForm)(this.btnSort.BindingForm)).AddRow("Estimated Quantity", "ROUND(B.TOTAL/(DECODE(A.BASE_QTY, 0, 1, 0) + A.BASE_QTY) * A.USAGE) AS E_QTY", "' '", "' '", true);
            ((udcTableForm)(this.btnSort.BindingForm)).AddRow("Required number", "' '", "' '", "' '", true);
            ((udcTableForm)(this.btnSort.BindingForm)).AddRow("Unit", "A.UNIT_1", "' '", "' '", true);
            ((udcTableForm)(this.btnSort.BindingForm)).AddRow("Customer", "B.DATA_1 AS Customer", "D.DATA_1", "B.DATA_1",false);
            ((udcTableForm)(this.btnSort.BindingForm)).AddRow("Family", "B.MAT_GRP_2 as Family", "B.MAT_GRP_2", "B.MAT_GRP_2", false);
            ((udcTableForm)(this.btnSort.BindingForm)).AddRow("Package", "B.MAT_GRP_3 as PKG", "B.MAT_GRP_3", "B.MAT_GRP_3", false);
            ((udcTableForm)(this.btnSort.BindingForm)).AddRow("Type1", "B.MAT_GRP_4 as Type1", "B.MAT_GRP_4", "B.MAT_GRP_4", false);
            ((udcTableForm)(this.btnSort.BindingForm)).AddRow("Type2", "B.MAT_GRP_5 as Type2", "B.MAT_GRP_5", "B.MAT_GRP_5", false);
            ((udcTableForm)(this.btnSort.BindingForm)).AddRow("LD Count", "B.MAT_GRP_6 as LEAD", "B.MAT_GRP_6", "B.MAT_GRP_6", false);
            ((udcTableForm)(this.btnSort.BindingForm)).AddRow("Density", "B.MAT_GRP_7 as Density", "B.MAT_GRP_7", "B.MAT_GRP_7", false);
            ((udcTableForm)(this.btnSort.BindingForm)).AddRow("Generation", "B.MAT_GRP_8 as Generation", "B.MAT_GRP_8", "B.MAT_GRP_8", false);
            ((udcTableForm)(this.btnSort.BindingForm)).AddRow("Pin Type", "B.MAT_CMF_10 as Pin_Type", "B.MAT_CMF_10", "B.MAT_CMF_10", false);
            ((udcTableForm)(this.btnSort.BindingForm)).AddRow("Product", "B.MAT_ID AS Product", "A.MAT_ID", "B.MAT_ID", true);
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

            try
            {
                //spdData.ActiveSheet.ColumnHeader.Rows.Count = 1;                
                spdData.RPT_AddBasicColumn("Item name", 0, 0, Visibles.False, Frozen.True, Align.Center, Merge.True, Formatter.String, 50);
                spdData.RPT_AddBasicColumn("Item Code", 0, 1, Visibles.True, Frozen.True, Align.Center, Merge.True, Formatter.String, 70);
                spdData.RPT_AddBasicColumn("Item specification", 0, 2, Visibles.True, Frozen.True, Align.Left, Merge.True, Formatter.String, 150);
                spdData.RPT_AddBasicColumn("BASE UNIT", 0, 3, Visibles.False, Frozen.False, Align.Center, Merge.True, Formatter.String, 50);                

                //spdData.RPT_AddBasicColumn("Operation WIP", 0, 4, Visibles.True, Frozen.False, Align.Center, Merge.True, Formatter.String, 120);
                spdData.RPT_AddBasicColumn("A", 0, 4, Visibles.True, Frozen.False, Align.Right, Merge.True, Formatter.String, 40);
                spdData.RPT_AddBasicColumn("B", 0, 5, Visibles.True, Frozen.False, Align.Right, Merge.True, Formatter.String, 40);
                spdData.RPT_AddBasicColumn("Equipment", 0, 6, Visibles.True, Frozen.False, Align.Right, Merge.True, Formatter.String, 40);
                spdData.RPT_AddBasicColumn("TOTAL(SP)", 0, 7, Visibles.True, Frozen.False, Align.Right, Merge.True, Formatter.String, 70);
                spdData.RPT_AddBasicColumn("TOTAL(QTY)", 0, 8, Visibles.True, Frozen.False, Align.Right, Merge.True, Formatter.String, 70);
                //spdData.RPT_MerageHeaderColumnSpan(0, 4, 5);

                spdData.RPT_AddBasicColumn("USAGE", 0, 9, Visibles.True, Frozen.False, Align.Right, Merge.True, Formatter.String, 50);
                spdData.RPT_AddBasicColumn("Estimated Requirements", 0, 10, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 70);
                spdData.RPT_AddBasicColumn("Required number", 0, 11, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 70);
                spdData.RPT_AddBasicColumn("Unit", 0, 12, Visibles.True, Frozen.False, Align.Center, Merge.False, Formatter.String, 40);
                spdData.RPT_AddBasicColumn("Customer", 0, 13, Visibles.False, Frozen.False, Align.Center, Merge.False, Formatter.String, 60);
                spdData.RPT_AddBasicColumn("Family", 0, 14, Visibles.False, Frozen.False, Align.Center, Merge.False, Formatter.String, 80);
                spdData.RPT_AddBasicColumn("Package", 0, 15, Visibles.False, Frozen.False, Align.Center, Merge.False, Formatter.String, 60);
                spdData.RPT_AddBasicColumn("Type1", 0, 16, Visibles.False, Frozen.False, Align.Center, Merge.False, Formatter.String, 70);
                spdData.RPT_AddBasicColumn("Type2", 0, 17, Visibles.False, Frozen.False, Align.Center, Merge.False, Formatter.String, 70);
                spdData.RPT_AddBasicColumn("LD Count", 0, 18, Visibles.False, Frozen.False, Align.Center, Merge.False, Formatter.String, 70);
                spdData.RPT_AddBasicColumn("Density", 0, 19, Visibles.False, Frozen.False, Align.Center, Merge.False, Formatter.String, 70);
                spdData.RPT_AddBasicColumn("Generation", 0, 20, Visibles.False, Frozen.False, Align.Center, Merge.False, Formatter.String, 70);
                spdData.RPT_AddBasicColumn("Pin Type", 0, 21, Visibles.False, Frozen.False, Align.Left, Merge.False, Formatter.String, 70);
                spdData.RPT_AddBasicColumn("Product", 0, 22, Visibles.True, Frozen.False, Align.Left, Merge.False, Formatter.String, 120);

                //spdData.RPT_AddBasicColumn("WIP", 0, 23, Visibles.True, Frozen.False, Align.Center, Merge.False, Formatter.String, 120);
                spdData.RPT_AddBasicColumn("HMKA2", 1, 23, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 70);
                spdData.RPT_AddBasicColumn("B/G", 1, 24, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 70);
                spdData.RPT_AddBasicColumn("SAW", 1, 25, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 70);
                spdData.RPT_AddBasicColumn("D/A", 1, 26, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 70);

                if (cboMatType.Text == "LF" || cboMatType.Text == "PB" || cboMatType.Text == "AE")
                {
                    spdData.RPT_AddBasicColumn("W/B", 1, 27, Visibles.False, Frozen.False, Align.Right, Merge.False, Formatter.Number, 70);
                    spdData.RPT_AddBasicColumn("MOLD", 1, 28, Visibles.False, Frozen.False, Align.Right, Merge.False, Formatter.Number, 70);
                    spdData.RPT_AddBasicColumn("SBA", 1, 29, Visibles.False, Frozen.False, Align.Right, Merge.False, Formatter.Number, 70);
                    spdData.RPT_AddBasicColumn("TOTAL", 1, 30, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 70);
                }
                else if (cboMatType.Text == "GW")
                {
                    spdData.RPT_AddBasicColumn("W/B", 1, 27, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 70);
                    spdData.RPT_AddBasicColumn("MOLD", 1, 28, Visibles.False, Frozen.False, Align.Right, Merge.False, Formatter.Number, 70);
                    spdData.RPT_AddBasicColumn("SBA", 1, 29, Visibles.False, Frozen.False, Align.Right, Merge.False, Formatter.Number, 70);
                    spdData.RPT_AddBasicColumn("TOTAL", 1, 30, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 70);
                }
                else if (cboMatType.Text == "MC")
                {
                    spdData.RPT_AddBasicColumn("W/B", 1, 27, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 70);
                    spdData.RPT_AddBasicColumn("MOLD", 1, 28, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 70);
                    spdData.RPT_AddBasicColumn("SBA", 1, 29, Visibles.False, Frozen.False, Align.Right, Merge.False, Formatter.Number, 70);
                    spdData.RPT_AddBasicColumn("TOTAL", 1, 30, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 70);
                }
                else
                {
                    spdData.RPT_AddBasicColumn("W/B", 1, 27, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 70);
                    spdData.RPT_AddBasicColumn("MOLD", 1, 28, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 70);
                    spdData.RPT_AddBasicColumn("SBA", 1, 29, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 70);
                    spdData.RPT_AddBasicColumn("TOTAL", 1, 30, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 70);
                }
                spdData.RPT_MerageHeaderColumnSpan(0, 23, 8);

                //spdData.RPT_AddDynamicColumn("PRODUCT WIP", new string[] { "B/G", "SAW", "D/A", "W/B", "MOLD", "TOTAL" },
                //    0, 14, Visibles.True, Frozen.False, Align.Right, Merge.False, new Formatter[] { Formatter.Number, Formatter.Number, Formatter.Number, Formatter.Number, Formatter.Number, Formatter.Number }, 60);

                for (int i = 0; i < 23; i++)
                {
                    spdData.RPT_MerageHeaderRowSpan(0, i, 2);
                }
                //spdData.RPT_MerageHeaderRowSpan(0, 0, 2);
                //spdData.RPT_MerageHeaderRowSpan(0, 1, 2);
                //spdData.RPT_MerageHeaderRowSpan(0, 2, 2);
                //spdData.RPT_MerageHeaderRowSpan(0, 3, 2);
                //spdData.RPT_MerageHeaderRowSpan(0, 4, 2);
                //spdData.RPT_MerageHeaderRowSpan(0, 5, 2);
                //spdData.RPT_MerageHeaderRowSpan(0, 6, 2);
                //spdData.RPT_MerageHeaderRowSpan(0, 7, 2);
                //spdData.RPT_MerageHeaderRowSpan(0, 8, 2);
                //spdData.RPT_MerageHeaderRowSpan(0, 9, 2);
                //spdData.RPT_MerageHeaderRowSpan(0, 10, 2);
                //spdData.RPT_MerageHeaderRowSpan(0, 11, 2);
                //spdData.RPT_MerageHeaderRowSpan(0, 12, 2);
                //spdData.RPT_MerageHeaderRowSpan(0, 13, 2);
                //spdData.RPT_MerageHeaderRowSpan(0, 14, 2);
                //spdData.RPT_MerageHeaderRowSpan(0, 15, 2);
                //spdData.RPT_MerageHeaderRowSpan(0, 16, 2);
                //spdData.RPT_MerageHeaderRowSpan(0, 17, 2);
                //spdData.RPT_MerageHeaderRowSpan(0, 18, 2);
                //spdData.RPT_MerageHeaderRowSpan(0, 19, 2);
                //spdData.RPT_MerageHeaderRowSpan(0, 20, 2);
                //spdData.RPT_MerageHeaderRowSpan(0, 21, 2);
                //spdData.RPT_MerageHeaderRowSpan(0, 22, 2);

                //spdData.RPT_AddDynamicColumn(udcDurationDate1, 0, 14, Visibles.False, Frozen.False, Align.Right, Merge.False, Formatter.String, 60);
                spdData.RPT_ColumnConfigFromTable(btnSort); //Group항목이 있을경우 반드시 선업해줄것.
            }
            catch (Exception ex)
            {
                CmnFunction.ShowMsgBox(ex.Message);
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

                //그루핑 순서 1순위만 SubTotal을 사용하기 위해서 첫번째 값을 가져옴
                int sub = ((udcTableForm)(this.btnSort.BindingForm)).GetCheckedValue(2);

                ////by John (한줄짜리)
                ////1.Griid 합계 표시
                int[] rowType = spdData.RPT_DataBindingWithSubTotal(dt, 0, sub+1, 4, null, null, btnSort);
                //spdData.DataSource = dt;

                ////2. 칼럼 고정(필요하다면..)
                //spdData.Sheets[0].FrozenColumnCount = 9;x

                ////3. Total부분 셀머지
                spdData.RPT_FillDataSelectiveCells("Total", 0, 4, 0, 1, true, Align.Center, VerticalAlign.Center);
                                
                //4. Column Auto Fit
                spdData.RPT_AutoFit(false);

                SetAvgVertical(1, 4, false);
                SetAvgVertical(1, 5, false);
                SetAvgVertical(1, 6, false);
                SetAvgVertical(1, 7, false);
                SetAvgVertical(1, 8, false);

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
            //if (cdvFactory.Text.Trim().Length == 0)
            //{
            //    CmnFunction.ShowMsgBox(RptMessages.GetMessage("STD001", GlobalVariable.gcLanguage));
            //    return false;
            //}

            //if (cdvStep.Text.Trim().Length == 0)
            //{
            //    CmnFunction.ShowMsgBox(RptMessages.GetMessage("STD005", GlobalVariable.gcLanguage));
            //    return false;
            //}

            return true;
        }

        #endregion

        #region MakeSqlString
        private string MakeSqlString()
        {
            StringBuilder strSqlString = new StringBuilder();

            string QueryCond1 = null;
            string QueryCond2 = null;
            string QueryCond3 = null;

            udcTableForm tableForm = (udcTableForm)btnSort.BindingForm;

            QueryCond1 = tableForm.SelectedValueToQueryContainNull;
            QueryCond2 = tableForm.SelectedValue2ToQueryContainNull;
            QueryCond3 = tableForm.SelectedValue3ToQueryContainNull;

            strSqlString.AppendFormat("SELECT {0},  " + "\n", QueryCond1);
            strSqlString.Append("       B.HMK2A, " + "\n");
            strSqlString.Append("       B.BG, " + "\n");
            strSqlString.Append("       B.SAW, " + "\n");
            strSqlString.Append("       B.DA, " + "\n");

            if (cboMatType.Text == "LF" || cboMatType.Text == "PB" || cboMatType.Text == "AE")
                strSqlString.Append("       '','','', " + "\n");
            else if (cboMatType.Text == "GW")
                strSqlString.Append("       B.WB,'','', " + "\n");
            else if (cboMatType.Text == "MC")
                strSqlString.Append("       B.WB,B.MOLD,'', " + "\n");
            else
                strSqlString.Append("       B.WB,B.MOLD,B.SBA, " + "\n");
            //strSqlString.Append("       B.WB, " + "\n");
            //strSqlString.Append("       B.MOLD, " + "\n");
            //strSqlString.Append("       B.SBA, " + "\n");
            strSqlString.Append("       B.TOTAL " + "\n");
            //strSqlString.Append("       A.MATCODE, " + "\n");
            //strSqlString.Append("       A.MAT_TYPE, " + "\n");
            //strSqlString.Append("       A.MAT_DESC, " + "\n");
            //strSqlString.Append("       A.BASE_UNIT, " + "\n");
            //strSqlString.Append("       A.USAGE, " + "\n");
            //strSqlString.Append("       A.A_QTY, " + "\n");
            //strSqlString.Append("       A.B_QTY, " + "\n");
            //strSqlString.Append("       A.C_QTY, " + "\n");
            //strSqlString.Append("       A.TOTAL, " + "\n");
            //strSqlString.Append("       ROUND(B.TOTAL/(DECODE(A.BASE_UNIT, 0, 1, 0) + A.BASE_UNIT)*A.USAGE) AS N_QTY, " + "\n");
            //strSqlString.Append("       ROUND((B.TOTAL/(DECODE(A.BASE_QTY, 0, 1, 0) + A.BASE_QTY) * A.USAGE) - A.TOTAL_QTY) AS N_QTY, " + "\n");
            //strSqlString.Append("       A.UNIT_1 " + "\n");
            strSqlString.Append(" FROM( " + "\n");
            strSqlString.Append("       SELECT A.PARTNUMBER, " + "\n");
            strSqlString.Append("              A.MATCODE, " + "\n");
            strSqlString.Append("              A.MAT_TYPE, " + "\n");
            strSqlString.Append("              A.MAT_DESC, " + "\n");
            strSqlString.Append("              A.BASE_QTY AS BASE_QTY, " + "\n");
            strSqlString.Append("              A.USAGE AS USAGE, " + "\n");
            strSqlString.Append("              A.UNIT_1, " + "\n");
            strSqlString.Append("              NVL(B.A_COUNT,0) AS A_COUNT, " + "\n");
            strSqlString.Append("              NVL(B.A_QTY,0) AS A_QTY, " + "\n");
            strSqlString.Append("              NVL(B.B_COUNT,0) AS B_COUNT, " + "\n");
            strSqlString.Append("              NVL(B.B_QTY,0) AS B_QTY, " + "\n");
            strSqlString.Append("              NVL(B.C_COUNT,0) AS C_COUNT, " + "\n");
            strSqlString.Append("              NVL(B.C_QTY,0) AS C_QTY, " + "\n");
            strSqlString.Append("              NVL(B.TOTAL_COUNT,0) AS TOTAL_COUNT, " + "\n");
            strSqlString.Append("              NVL(B.TOTAL_QTY,0) AS TOTAL_QTY " + "\n");
            strSqlString.Append("        FROM( " + "\n");
            strSqlString.Append("              SELECT DISTINCT A.PARTNUMBER, " + "\n");
            strSqlString.Append("                     A.MATCODE, " + "\n");
            strSqlString.Append("                     B.MAT_TYPE, " + "\n");
            strSqlString.Append("                     B.MAT_DESC, " + "\n");
            strSqlString.Append("                     A.PAR_BASE_QTY AS BASE_QTY, " + "\n");
            strSqlString.Append("                     A.UNIT_QTY AS USAGE, " + "\n");
            strSqlString.Append("                     B.UNIT_1 " + "\n");
            strSqlString.Append("                FROM CWIPBOMDEF A, " + "\n");
            strSqlString.Append("                     MWIPMATDEF B " + "\n");
            strSqlString.Append("               WHERE A.MATCODE=B.MAT_ID " + "\n");
            strSqlString.Append("              GROUP BY A.PARTNUMBER,A.MATCODE,B.MAT_TYPE,B.MAT_DESC,A.PAR_BASE_QTY,A.UNIT_QTY,B.UNIT_1 " + "\n");
            strSqlString.Append("            )A, " + "\n");
            strSqlString.Append("            ( " + "\n");
            strSqlString.Append("              SELECT MAT_ID, " + "\n");
            strSqlString.Append("                     SUM(DECODE(RES_COUNT,0,A_COUNT,0)) AS A_COUNT, " + "\n");
            strSqlString.Append("                     SUM(DECODE(RES_QTY,0,A_QTY,0)) AS A_QTY, " + "\n");
            strSqlString.Append("                     SUM(DECODE(RES_COUNT,0,B_COUNT,0)) AS B_COUNT, " + "\n");
            strSqlString.Append("                     SUM(DECODE(RES_QTY,0,B_QTY,0)) AS B_QTY, " + "\n");
            strSqlString.Append("                     SUM(RES_COUNT) AS C_COUNT, " + "\n");
            strSqlString.Append("                     SUM(RES_QTY) C_QTY, " + "\n");
            strSqlString.Append("                     SUM(DECODE(RES_COUNT,0,A_COUNT,0)+DECODE(RES_COUNT,0,B_COUNT,0)+RES_COUNT) AS TOTAL_COUNT, " + "\n");
            strSqlString.Append("                     SUM(DECODE(RES_QTY,0,A_QTY,0)+DECODE(RES_QTY,0,B_QTY,0)+RES_QTY) AS TOTAL_QTY " + "\n");
            strSqlString.Append("               FROM( " + "\n");
            strSqlString.Append("                     SELECT LOT_ID, " + "\n");
            strSqlString.Append("                            MAT_ID, " + "\n");
            strSqlString.Append("                            GRADE, " + "\n");
            strSqlString.Append("                            SUM(DECODE(GRADE,'A',LOT_COUNT,0)) AS A_COUNT, " + "\n");
            strSqlString.Append("                            SUM(DECODE(GRADE,'A',LOT_QTY,0)) AS A_QTY, " + "\n");
            strSqlString.Append("                            SUM(DECODE(GRADE,'B',LOT_COUNT,0)) AS B_COUNT, " + "\n");
            strSqlString.Append("                            SUM(DECODE(GRADE,'B',LOT_QTY,0)) AS B_QTY, " + "\n");
            strSqlString.Append("                            SUM(RES_COUNT) AS RES_COUNT, " + "\n");
            strSqlString.Append("                            SUM(RES_QTY) RES_QTY " + "\n");
            strSqlString.Append("                      FROM( " + "\n");
            strSqlString.Append("                            ( " + "\n");
            strSqlString.Append("                              SELECT LOT_ID, " + "\n");
            strSqlString.Append("                                     MAT_ID, " + "\n");
            strSqlString.Append("                                     REPLACE(LOT_CMF_9,' ','A') AS GRADE, " + "\n");
            strSqlString.Append("                                     COUNT(LOT_ID) AS LOT_COUNT, " + "\n");
            strSqlString.Append("                                     QTY_1 AS LOT_QTY, " + "\n");
            strSqlString.Append("                                     0 AS RES_COUNT, " + "\n");
            strSqlString.Append("                                     0 AS RES_QTY " + "\n");
            strSqlString.Append("                                FROM RWIPLOTSTS " + "\n");
            strSqlString.Append("                               WHERE 1=1 " + "\n");
            strSqlString.Append("                                     AND FACTORY = '" + GlobalVariable.gsAssyDefaultFactory + "' " + "\n");
            strSqlString.Append("                                     AND OPER IN ( " + "\n");
            strSqlString.Append("                                                   SELECT ATTR_KEY " + "\n");
            strSqlString.Append("                                                     FROM MATRNAMSTS " + "\n");
            strSqlString.Append("                                                    WHERE 1=1 " + "\n");
            strSqlString.Append("                                                          AND FACTORY = '" + GlobalVariable.gsAssyDefaultFactory + "' " + "\n");
            strSqlString.Append("                                                          AND ATTR_TYPE = 'OPER' " + "\n");
            strSqlString.Append("                                                          AND ATTR_NAME = 'MATERIAL_OPER' " + "\n");
            strSqlString.Append("                                                          AND ATTR_VALUE = 'Y' " + "\n");
            strSqlString.Append("                                                          AND ATTR_KEY <> 'V0000' " + "\n");
            strSqlString.Append("                                                 ) " + "\n");
            strSqlString.Append("                                     AND LOT_DEL_FLAG = ' ' " + "\n");
            strSqlString.Append("                                     AND LOT_TYPE <> 'W' " + "\n");
            strSqlString.Append("                               GROUP BY LOT_ID,MAT_ID,LOT_CMF_9,QTY_1 " + "\n");
            strSqlString.Append("                            ) " + "\n");
            strSqlString.Append("                            UNION ALL " + "\n");
            strSqlString.Append("                            ( " + "\n");
            strSqlString.Append("                              SELECT A.LOT_ID, " + "\n");
            strSqlString.Append("                                     A.MAT_ID, " + "\n");
            strSqlString.Append("                                     REPLACE(LOT_CMF_9,' ','A') AS GRADE, " + "\n");
            strSqlString.Append("                                     0 AS LOT_COUNT, " + "\n");
            strSqlString.Append("                                     0 AS LOT_QTY, " + "\n");
            strSqlString.Append("                                     COUNT(A.LOT_ID) AS RES_COUNT, " + "\n");
            strSqlString.Append("                                     A.QTY_1 AS RES_QTY " + "\n");
            strSqlString.Append("                                FROM RWIPLOTSTS A, " + "\n");
            strSqlString.Append("                                     CRASRESMAT B " + "\n");
            strSqlString.Append("                               WHERE A.LOT_ID=B.LOT_ID " + "\n");
            strSqlString.Append("                               GROUP BY A.LOT_ID,A.MAT_ID,LOT_CMF_9,QTY_1" + "\n");
            strSqlString.Append("                           ) " + "\n");
            strSqlString.Append("                          ) " + "\n");
            strSqlString.Append("                     GROUP BY LOT_ID,MAT_ID,GRADE " + "\n");
            strSqlString.Append("                   ) " + "\n");
            strSqlString.Append("              GROUP BY MAT_ID " + "\n");
            strSqlString.Append("            )B " + "\n");
            strSqlString.Append("        WHERE A.MATCODE=B.MAT_ID(+) " + "\n");
            strSqlString.Append("     ) A, " + "\n");
            strSqlString.Append("     ( "  + "\n");
            strSqlString.AppendFormat("       SELECT {0},  " + "\n", QueryCond2);
            strSqlString.Append("              SUM(DECODE(C.OPER_GRP_1, 'HMK2A', A.QTY_1,0)) HMK2A, " + "\n");
            strSqlString.Append("              SUM(DECODE(C.OPER_GRP_1,'B/G',A.QTY_1,0)) AS BG, " + "\n");
            strSqlString.Append("              SUM(DECODE(C.OPER_GRP_1,'SAW',A.QTY_1,0)) AS SAW, " + "\n");
            strSqlString.Append("              SUM(CASE C.OPER_GRP_1 WHEN 'SMT' THEN A.QTY_1 WHEN 'S/P' THEN A.QTY_1 WHEN 'D/A' THEN A.QTY_1 ELSE 0 END) AS DA, " + "\n");
            strSqlString.Append("              SUM(DECODE(C.OPER_GRP_1,'W/B',A.QTY_1,0)) AS WB, " + "\n");
            strSqlString.Append("              SUM(DECODE(C.OPER_GRP_1,'MOLD',A.QTY_1,0)) AS MOLD, " + "\n");
            strSqlString.Append("              SUM(CASE C.OPER_GRP_1 WHEN 'CURE' THEN A.QTY_1 WHEN 'M/K' THEN A.QTY_1 WHEN 'V/I' THEN A.QTY_1 WHEN 'S/B/A' THEN A.QTY_1 ELSE 0 END) AS SBA, " + "\n");
            strSqlString.Append("              SUM(A.QTY_1) AS TOTAL " + "\n");
            strSqlString.Append("         FROM RWIPLOTSTS A, MWIPMATDEF B, MWIPOPRDEF C, MGCMTBLDAT D " + "\n");
            strSqlString.Append("        WHERE A.FACTORY = '" + GlobalVariable.gsAssyDefaultFactory + "' " + "\n");
            strSqlString.Append("              AND B.MAT_VER = 1 " + "\n");
            strSqlString.Append("              AND A.OWNER_CODE = 'PROD' " + "\n");
            strSqlString.Append("              AND A.FACTORY = B.FACTORY " + "\n");
            strSqlString.Append("              AND A.MAT_ID = B.MAT_ID " + "\n");
            strSqlString.Append("              AND A.OPER=C.OPER " + "\n");
            strSqlString.Append("              AND A.MAT_VER = 1 " + "\n");
            strSqlString.Append("              AND A.LOT_DEL_FLAG = ' ' " + "\n");
            strSqlString.Append("              AND B.MAT_GRP_1 = D.KEY_1 " + "\n");
            strSqlString.Append("              AND B.FACTORY=D.FACTORY " + "\n");
            strSqlString.Append("              AND D.TABLE_NAME='H_CUSTOMER' " + "\n");

            // 자재 TYPE에 따른 Product 재공 값 가져오기
            if (cboMatType.Text == "LF" || cboMatType.Text == "PB")
            {
                strSqlString.Append("              AND C.OPER_GRP_1 IN ('HMK2A','B/G','SAW','SMT','S/P','D/A') " + "\n");
            }
            else if (cboMatType.Text == "GW")
            {
                strSqlString.Append("              AND C.OPER_GRP_1 IN ('HMK2A','B/G','SAW','SMT','S/P','D/A','W/B') " + "\n");
            }
            else if (cboMatType.Text == "MC")
            {
                strSqlString.Append("              AND C.OPER_GRP_1 IN ('HMK2A','B/G','SAW','SMT','S/P','D/A','W/B','MOLD') " + "\n");
            }
            else
            {
                strSqlString.Append("              AND C.OPER_GRP_1 IN ('HMK2A','B/G','SAW','SMT','S/P','D/A','W/B','MOLD','CURE','M/K','V/I','S/B/A') " + "\n");
            }

            strSqlString.AppendFormat("       GROUP BY {0}" + "\n", QueryCond2);
            strSqlString.Append("       HAVING SUM(A.QTY_1) > 0 " + "\n");
            strSqlString.Append("     ) B " + "\n");
            strSqlString.Append(" WHERE A.PARTNUMBER(+)=B.MAT_ID" + "\n");


            //#region 조회조건(FACTORY, STEP, LOT_TYPE, PRODUCT, DATE)

            strSqlString.Append("      AND A.MAT_TYPE='" + cboMatType.Text + "'\n");

            //if (cdvStep.Text != "ALL" && cdvStep.Text.Trim() != "")
            //    strSqlString.Append("        AND LOT.OPER " + cdvStep.SelectedValueToQueryString + "\n");

            //if (cdvFactory.Text == GlobalVariable.gsAssyDefaultFactory)
            //    strSqlString.Append("        AND LOT.OPER NOT IN ('00001','00002') " + "\n");

            //if (cdvLotType.Text != "ALL" && cdvLotType.Text.Trim() != "")
            //    strSqlString.Append("        AND LOT.LOT_TYPE " + cdvLotType.SelectedValueToQueryString + "\n");

            if (txtSearchProduct.Text.Trim() != "%" && txtSearchProduct.Text.Trim() != "")
                strSqlString.AppendFormat("      AND A.PARTNUMBER LIKE '{0}'" + "\n", txtSearchProduct.Text);

            //#endregion

            //#region 상세 조회에 따른 SQL문 생성
            if (udcWIPCondition1.Text != "ALL" && udcWIPCondition1.Text != "")
                strSqlString.AppendFormat("      AND B.MAT_GRP_1 {0} " + "\n", udcWIPCondition1.SelectedValueToQueryString);

            if (udcWIPCondition2.Text != "ALL" && udcWIPCondition2.Text != "")
                strSqlString.AppendFormat("      AND B.MAT_GRP_2 {0} " + "\n", udcWIPCondition2.SelectedValueToQueryString);

            if (udcWIPCondition3.Text != "ALL" && udcWIPCondition3.Text != "")
                strSqlString.AppendFormat("      AND B.MAT_GRP_3 {0} " + "\n", udcWIPCondition3.SelectedValueToQueryString);

            if (udcWIPCondition4.Text != "ALL" && udcWIPCondition4.Text != "")
                strSqlString.AppendFormat("      AND B.MAT_GRP_4 {0} " + "\n", udcWIPCondition4.SelectedValueToQueryString);

            if (udcWIPCondition5.Text != "ALL" && udcWIPCondition5.Text != "")
                strSqlString.AppendFormat("      AND B.MAT_GRP_5 {0} " + "\n", udcWIPCondition5.SelectedValueToQueryString);

            if (udcWIPCondition6.Text != "ALL" && udcWIPCondition6.Text != "")
                strSqlString.AppendFormat("      AND B.MAT_GRP_6 {0} " + "\n", udcWIPCondition6.SelectedValueToQueryString);

            if (udcWIPCondition7.Text != "ALL" && udcWIPCondition7.Text != "")
                strSqlString.AppendFormat("      AND B.MAT_GRP_7 {0} " + "\n", udcWIPCondition7.SelectedValueToQueryString);

            if (udcWIPCondition8.Text != "ALL" && udcWIPCondition8.Text != "")
                strSqlString.AppendFormat("      AND B.MAT_GRP_8 {0} " + "\n", udcWIPCondition8.SelectedValueToQueryString);
            //#endregion

            //strSqlString.AppendFormat("ORDER BY {0}, LOT.OPER , LOT.LOT_ID, MAT.MAT_ID" + "\n", QueryCond2);
            strSqlString.AppendFormat("ORDER BY {0} , A.MATCODE" + "\n", QueryCond3);
            //strSqlString.Append(" ORDER BY A.MATCODE,A.PARTNUMBER ");

            if (GlobalVariable.gsUserGroup == "ADMIN_GROUP" || GlobalVariable.gsUserGroup == "HANA_ADMIN_GROUP")
            {
                System.Windows.Forms.Clipboard.SetText(strSqlString.ToString());
            }

            return strSqlString.ToString();
        }
        #endregion

        #endregion

        #region Event

        private void btnExcelExport_Click(object sender, EventArgs e)
        {
            ExcelHelper.Instance.subMakeExcel(spdData, this.lblTitle.Text, " ^ ", " ^ ");
        }


        #endregion

        public void SetAvgVertical(int nSampleNormalRowPos, int nColPos, bool bWithNull) // Group 2단까지만 됨. 그 이상 적용 안됨.
        {
            double sum = 0;
            double totalSum = 0;
            double subSum = 0;

            double divide = 0;
            double totalDivide = 0;
            double subDivide = 0;

            int nQtyColPos = 0; // 필요량 컬럼 위치 구하기 위해 사용
            int checkValue = ((udcTableForm)(this.btnSort.BindingForm)).GetCheckedValue(1);

            Color color = spdData.ActiveSheet.Cells[nSampleNormalRowPos, nColPos].BackColor;
            Color checkValueColor = spdData.ActiveSheet.Cells[nSampleNormalRowPos, checkValue].BackColor; // 첫번째 그룹의 첫 열 색 (기준색)

            // 필요량 컬럼 위치 구하기
            for (int x = 0; x < spdData.ActiveSheet.Columns.Count; x++)
            {
                if (spdData.ActiveSheet.Columns[x].Label == "Required number")
                {
                    nQtyColPos = x;
                }
            }

            for (int i = 1; i < spdData.ActiveSheet.Rows.Count; i++)
            {
                if (spdData.ActiveSheet.Cells[i, checkValue].BackColor == checkValueColor) //subTotal Sum 구하기 위함.
                {
                    if (spdData.ActiveSheet.Cells[i, nColPos].BackColor == color)
                    {
                        sum += Convert.ToDouble(spdData.ActiveSheet.Cells[i, nColPos].Value);

                        if (!bWithNull && (spdData.ActiveSheet.Cells[i, nColPos].Value == null || spdData.ActiveSheet.Cells[i, nColPos].Value.ToString().Trim() == ""))
                            continue;

                        divide += 1;
                    }
                    else
                    {
                        // 주의!! subTotal Sum 구할때나, GrandTotal 구할때는 각 항목을 더해 Avg 구해야함. SubTatal을 더한 것으로 Avg 구하면 수치 차이가 심함.
                        // 예로 35.00 + 47.81 + 7.49 = 90.30 (avg:30.10) , 15.97 + 4.66 + 9.35 + 19.25 = 49.23 (avg:12.31) 일때
                        // 1.각 항목 더해서 구할때 : 35.00 + 47.81 + 7.49 + 15.97 + 4.66 + 9.35 + 19.25 = 139.53 (avg:19.93), 2. 30.10 + 12.31 = 21.20 나옴.

                        // SubTotal 구함.
                        if (divide != 0)
                        {
                            spdData.ActiveSheet.Cells[i, nColPos].Value = sum / divide;

                            // 필요량 SubTotal 부분 (소요량 SubTotal - 자재재공 SubTotal)
                            if (nColPos == 8)
                            {
                                spdData.ActiveSheet.Cells[i, nQtyColPos].Value = Convert.ToDouble(spdData.ActiveSheet.Cells[i, 10].Value) - Convert.ToDouble(spdData.ActiveSheet.Cells[i, 8].Value);
                            }
                        }

                        subSum += sum;
                        subDivide += divide;

                        //totalSum += sum;
                        totalDivide += divide;

                        sum = 0;
                        divide = 0;

                        //subSum += Convert.ToDouble(spdData.ActiveSheet.Cells[i, nColPos].Value);

                        //if (!bWithNull && (spdData.ActiveSheet.Cells[i, nColPos].Value == null || spdData.ActiveSheet.Cells[i, nColPos].Value.ToString().Trim() == ""))
                        //    continue;

                        //subDivide += 1;
                    }
                }
                else
                {
                    // SubTotal Sum 구함.
                    if (subDivide != 0)
                    {
                        spdData.ActiveSheet.Cells[i, nColPos].Value = subSum / subDivide;

                        if (nColPos == 8)
                        {
                            spdData.ActiveSheet.Cells[i, nQtyColPos].Value = Convert.ToDouble(spdData.ActiveSheet.Cells[i, 10].Value) - Convert.ToDouble(spdData.ActiveSheet.Cells[i, 8].Value);
                        }
                    }

                    // SubTotal 값들만 더해 Grand Total 구함.
                    totalSum += subSum / subDivide;

                    subSum = 0;
                    subDivide = 0;
                }
            }
            // GrandTotal 구함.
            if (totalDivide != 0)
                //spdData.ActiveSheet.Cells[0, nColPos].Value = totalSum / totalDivide;
                spdData.ActiveSheet.Cells[0, nColPos].Value = totalSum;

            else
            {
                // subTotal이 하나도 없을때 즉 Raw Data만 있을때 GrandTotal 구함.
                if (divide != 0)
                {
                    spdData.ActiveSheet.Cells[0, nColPos].Value = sum / divide;
                }
            }
            return;
        }

        private void cdvFactory_ValueSelectedItemChanged(object sender, MCCodeViewSelChanged_EventArgs e)
        {
            //this.SetFactory(cdvFactory.txtValue);
            //cdvStep.sFactory = cdvFactory.txtValue;
            //cdvLotType.sFactory = cdvFactory.txtValue;
        }
    }
}

