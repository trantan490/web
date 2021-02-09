using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Miracom.SmartWeb.FWX;
using Miracom.SmartWeb.UI.Controls;

namespace Miracom.SmartWeb.UI
{
    public partial class Sample10 : Miracom.SmartWeb.UI.Controls.udcCUSReport002
    {
        static int oper_count = 0;
        static DataTable global_oper = null;

        public Sample10()
        {
            InitializeComponent();           

            SortInit();  
            GridColumnInit(); //해더 한줄짜리 샘플

            udcChartFX1.RPT_1_ChartInit();  //차트 초기화.           
            
        }

        private Boolean CheckField()
        {
            if (cdvFactory.Text.TrimEnd() == "")
            {
                CmnFunction.ShowMsgBox(RptMessages.GetMessage("STD001", GlobalVariable.gcLanguage));
                return false;
            }
            if (cdvOper.FromText.TrimEnd() == "" || cdvOper.ToText.TrimEnd() == "")
            {
                CmnFunction.ShowMsgBox(RptMessages.GetMessage("STD005", GlobalVariable.gcLanguage));
                return false;
            }
            return true;
        }

        private string MakeSqlString()
        {
            StringBuilder strSqlString = new StringBuilder();

            string QueryCond1 = null;
            string QueryCond2 = null;          

            udcTableForm tableForm = (udcTableForm)btnSort.BindingForm;
                        
            QueryCond1 = tableForm.SelectedValueToQueryContainNull;            
            QueryCond2 = tableForm.SelectedValue2ToQueryContainNull;


            //global_oper = CmnFunction.oComm.GetFuncDataTable("DYNAMIC", MakeSqlString1());
            String[] oper_data = new String[global_oper.Rows.Count];

            for (int i = 0; i < global_oper.Rows.Count; i++)
            {
                oper_data[i] = global_oper.Rows[i][0].ToString();
            }

            oper_count = global_oper.Rows.Count;
            strSqlString.AppendFormat("    SELECT {0} " + "\n", QueryCond2);

            strSqlString.Append("         , SUM(QTY_1)" + "\n");                

            for (int j = 0; j < global_oper.Rows.Count; j++)
            {                
                strSqlString.Append("         , SUM(DECODE(OPER, '" + oper_data[j] + "', QTY_1, 0)) AS \"" + oper_data[j] + "\"" + "\n");                
            }
            strSqlString.Append("      FROM (SELECT ROW_NUMBER() OVER(PARTITION BY OPER ORDER BY MAT_ID) OPER_ME, QTY_1, OPER, MAT_ID, FACTORY  FROM RWIPLOTSTS) A ");
            strSqlString.Append("         , MWIPMATDEF B " + "\n");            
            strSqlString.Append("     WHERE A.OPER  BETWEEN '" + CmnFunction.Trim(cdvOper.txtFromValue.ToString()) + "' AND '" + CmnFunction.Trim(cdvOper.txtToValue.ToString()) + "'  " + "\n");
            strSqlString.Append("       AND A.FACTORY = B.FACTORY " + "\n" );
            strSqlString.Append("       AND A.MAT_ID = B.MAT_ID " + "\n");
            strSqlString.Append("       AND A.MAT_ID IN (SELECT MAT_ID FROM RWIPLOTSTS WHERE LOT_CMF_5 " + cdvLotType.SelectedValueToQueryString + " ) " + "\n");

            //상세 조회에 따른 SQL문 생성                        
            if (udcWIPCondition1.Text != "ALL" && udcWIPCondition1.Text != "")
                strSqlString.AppendFormat("       AND B.MAT_GRP_1 {0} " + "\n", udcWIPCondition1.SelectedValueToQueryString);

            if (udcWIPCondition2.Text != "ALL" && udcWIPCondition2.Text != "")
                strSqlString.AppendFormat("       AND B.MAT_GRP_2 {0} " + "\n", udcWIPCondition2.SelectedValueToQueryString);

            if (udcWIPCondition3.Text != "ALL" && udcWIPCondition3.Text != "")
                strSqlString.AppendFormat("       AND B.MAT_GRP_3 {0} " + "\n", udcWIPCondition3.SelectedValueToQueryString);

            if (udcWIPCondition4.Text != "ALL" && udcWIPCondition4.Text != "")
                strSqlString.AppendFormat("       AND B.MAT_GRP_4 {0} " + "\n", udcWIPCondition4.SelectedValueToQueryString);

            if (udcWIPCondition5.Text != "ALL" && udcWIPCondition5.Text != "")
                strSqlString.AppendFormat("       AND B.MAT_GRP_5 {0} " + "\n", udcWIPCondition5.SelectedValueToQueryString);

            if (udcWIPCondition6.Text != "ALL" && udcWIPCondition6.Text != "")
                strSqlString.AppendFormat("       AND B.MAT_GRP_6 {0} " + "\n", udcWIPCondition6.SelectedValueToQueryString);

            if (udcWIPCondition7.Text != "ALL" && udcWIPCondition7.Text != "")
                strSqlString.AppendFormat("       AND B.MAT_GRP_7 {0} " + "\n", udcWIPCondition7.SelectedValueToQueryString);

            if (udcWIPCondition8.Text != "ALL" && udcWIPCondition8.Text != "")
                strSqlString.AppendFormat("       AND B.MAT_GRP_8 {0} " + "\n", udcWIPCondition8.SelectedValueToQueryString);

            strSqlString.AppendFormat("  GROUP BY {0}", QueryCond2);
            strSqlString.AppendFormat("  ORDER BY {0}", QueryCond2);

            return strSqlString.ToString();

        }

        private string MakeSqlString1()
        {
            StringBuilder strSqlString = new StringBuilder();

            string QueryCond1 = null;
            
            udcTableForm tableForm = (udcTableForm)btnSort.BindingForm;

            QueryCond1 = tableForm.SelectedValueToQueryContainNull;
            strSqlString.Append(" SELECT OPER FROM MWIPOPRDEF WHERE  ");
            
            if(CmnFunction.Trim(cdvFactory.Text.ToString()) ==  "")
            {
                        
            }  
            else
            {
                strSqlString.Append(" FACTORY = '" + CmnFunction.Trim(cdvFactory.Text.ToString()) + "'   AND  " );
            }

            strSqlString.Append("  OPER  BETWEEN '" + cdvOper.FromText + "' AND '" + cdvOper.ToText + "' \n");
            strSqlString.Append("  ORDER BY OPER ASC ");
            return strSqlString.ToString();
        }
        
              

        private void ShowChart(int rowCount)
        {
            // 차트 설정
            udcChartFX1.RPT_2_ClearData();
            udcChartFX1.RPT_3_OpenData(1, oper_count);
            int[] wip_columns = new Int32[oper_count];
            int[] tat_columns = new Int32[oper_count];
            int[] columnsHeader = new Int32[oper_count];

            for (int i = 0; i < wip_columns.Length; i++)
            {
                columnsHeader[i] = 10 + i;
                wip_columns[i] = 10 + i;
                tat_columns[i] = 10 + i;
            }
            
            //MOVE
            double max1 = udcChartFX1.RPT_4_AddData(spdData, new int[] { rowCount+0 }, wip_columns, SeriseType.Rows);

            //TAT
            //double max2 = udcChartFX1.RPT_4_AddData(spdData, new int[] { rowCount+1 }, wip_columns, SeriseType.Rows);
            
            udcChartFX1.RPT_5_CloseData();
            
            //각 Serise별로 다른 타입을 사용할 경우
            udcChartFX1.RPT_6_SetGallery(ChartType.Line, 0, 1, "OPER [단위 : EA]", AsixType.Y, DataTypes.Initeger, max1 * 1);
            //udcChartFX1.RPT_6_SetGallery(ChartType.Area, 1, 1, "END [단위 : EA]", AsixType.Y2, DataTypes.Initeger, max2 * 1.5);

            //각 Serise별로 동일한 타입을 사용할 경우
            //udcChartFX1.RPT_6_SetGallery(ChartType.Line,  "END [단위 : EA]", AsixType.Y, DataTypes.Initeger, max1 * 1.5);
                        
            udcChartFX1.RPT_7_SetXAsixTitleUsingSpreadHeader(spdData, 0, columnsHeader);
            udcChartFX1.RPT_8_SetSeriseLegend(new string[] { "OPER" }, SoftwareFX.ChartFX.Docked.Top);

            // 기타 설정
            udcChartFX1.PointLabels = true;
            udcChartFX1.AxisY.DataFormat.Decimals = 0;
        }
        
        private void Sample02_Load(object sender, EventArgs e)
        {
         
        }

        private void btnView_Click(object sender, EventArgs e)
        {
            if (CheckField() == false)
                return;

            DataTable dt = null;
            GridColumnInit();
            
            spdData_Sheet1.RowCount = 0;
            this.Refresh();
            dt = CmnFunction.oComm.GetFuncDataTable("DYNAMIC", MakeSqlString());

            if (dt.Rows.Count == 0)
            {
                dt.Dispose();
                CmnFunction.ShowMsgBox(RptMessages.GetMessage("STD010", GlobalVariable.gcLanguage));
                return;
            }

            ////by John (한줄짜리)
            ////1.Griid 합계 표시
            int[] rowType = spdData.RPT_DataBindingWithSubTotal(dt, 0, 1, 8, null, null, btnSort);
            // 토탈 시작할 셀넘버, 시작 부터 서브토탈 몇개 쓸건지, 첫셀부터 몇번째 셀까지 TOTAL 적용안함


            ////2. 칼럼 고정(필요하다면..)
            //spdData.Sheets[0].FrozenColumnCount = 9;

            ////3. Total부분 셀머지
            spdData.RPT_FillDataSelectiveCells("Total", 0, 9, 0, 1, true, Align.Center, VerticalAlign.Center);


            
            //4. Column Auto Fit
            spdData.RPT_AutoFit(false);

            //Chart 생성
            if (spdData.ActiveSheet.RowCount > 0)
                ShowChart(0);

        }

        private void btnExcelExport_Click(object sender, EventArgs e)
        {            
            ExcelHelper.Instance.subMakeExcel(spdData, udcChartFX1,this.lblTitle.Text, "조회조건 1^조회조건 2", "조회조건 3^조회조건4");                                   
        }

        private void cdvFactory_ValueSelectedItemChanged(object sender, Miracom.UI.MCCodeViewSelChanged_EventArgs e)
        {
            this.SetFactory(cdvFactory.txtValue);
            this.cdvLotType.sFactory = cdvFactory.txtValue;
            this.cdvOper.sFactory = cdvFactory.txtValue;            
        }
                       

        //한줄짜리 해더 샘플
        private void GridColumnInit()
        {
            spdData.ActiveSheet.ColumnHeader.Rows.Count = 1;
                      
            spdData.RPT_ColumnInit();
            spdData.RPT_AddBasicColumn("Customer", 0, 0, Visibles.True, Frozen.True, Align.Left, Merge.True, Formatter.String, 60);
            spdData.RPT_AddBasicColumn("Family", 0, 1, Visibles.False, Frozen.True, Align.Left, Merge.True, Formatter.String, 60);
            spdData.RPT_AddBasicColumn("Package", 0, 2, Visibles.True, Frozen.True, Align.Left, Merge.True, Formatter.String, 60);
            spdData.RPT_AddBasicColumn("Type1", 0, 3, Visibles.False, Frozen.True, Align.Left, Merge.True, Formatter.String, 70);
            spdData.RPT_AddBasicColumn("Type2", 0, 4, Visibles.False, Frozen.True, Align.Left, Merge.True, Formatter.String, 70);
            spdData.RPT_AddBasicColumn("LD Count", 0, 5, Visibles.False, Frozen.True, Align.Left, Merge.True, Formatter.String, 70);
            spdData.RPT_AddBasicColumn("Density", 0, 6, Visibles.False, Frozen.True, Align.Left, Merge.True, Formatter.String, 70);
            spdData.RPT_AddBasicColumn("Generation", 0, 7, Visibles.False, Frozen.True, Align.Center, Merge.True, Formatter.String, 70);
            spdData.RPT_AddBasicColumn("Product", 0, 8, Visibles.True, Frozen.True, Align.Left, Merge.True, Formatter.String, 70);
            spdData.RPT_AddBasicColumn("TOTAL", 0, 9, Visibles.True, Frozen.True, Align.Right, Merge.False, Formatter.Number, 70);

            global_oper = CmnFunction.oComm.GetFuncDataTable("DYNAMIC", MakeSqlString1());

            if(cdvOper.FromText != "" && cdvOper.ToText != "")
            {
                for (int i = 0; i < global_oper.Rows.Count; i++)
                {
                    spdData.RPT_AddBasicColumn(global_oper.Rows[i][0].ToString(), 0, (10 + i), Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 80);
                }             
            }

            spdData.RPT_ColumnConfigFromTable(btnSort); //Group항목이 있을경우 반드시 선언해줄것.
            //spdData.ActiveSheet.RowCount = 5;
        }
               

        private void SortInit()
        {
            ((udcTableForm)(this.btnSort.BindingForm)).AddRow("Customer", "MAT_GRP_1", "B.MAT_GRP_1", "A.MAT_GRP_1", true);
            ((udcTableForm)(this.btnSort.BindingForm)).AddRow("Family", "MAT_GRP_2", "B.MAT_GRP_2", "A.MAT_GRP_2", false);
            ((udcTableForm)(this.btnSort.BindingForm)).AddRow("Package", "MAT_GRP_3", "B.MAT_GRP_3", "A.MAT_GRP_3", true);
            ((udcTableForm)(this.btnSort.BindingForm)).AddRow("Type1", "MAT_GRP_4", "B.MAT_GRP_4", "A.MAT_GRP_4", false);
            ((udcTableForm)(this.btnSort.BindingForm)).AddRow("Type2", "MAT_GRP_5", "B.MAT_GRP_5", "A.MAT_GRP_5", false);
            ((udcTableForm)(this.btnSort.BindingForm)).AddRow("LD Count", "MAT_GRP_6", "B.MAT_GRP_6", "A.MAT_GRP_6", false);
            ((udcTableForm)(this.btnSort.BindingForm)).AddRow("Density", "MAT_GRP_7", "B.MAT_GRP_7", "A.MAT_GRP_7", false);
            ((udcTableForm)(this.btnSort.BindingForm)).AddRow("Generation", "MAT_GRP_8", "B.MAT_GRP_8", "A.MAT_GRP_8", false);
            ((udcTableForm)(this.btnSort.BindingForm)).AddRow("Product", "MAT_ID", "B.MAT_ID", "A.MAT_ID", true);                      
        }
        
        
        private void spdData_CellDoubleClick(object sender, FarPoint.Win.Spread.CellClickEventArgs e)
        {
            if (e.ColumnHeader)
                return;

            int i = 0;
            i = e.Row;
            ShowChart(i);
        }

        
    }
}

