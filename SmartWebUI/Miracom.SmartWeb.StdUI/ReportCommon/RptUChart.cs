using System;
using System.Data;
using System.IO;
using System.Windows.Forms;
using System.Configuration;
using Miracom.SmartWeb.FWX;
using Infragistics.UltraChart.Shared.Styles;
using Infragistics.UltraChart.Resources.Appearance;
using Infragistics.UltraChart.Core.Layers;

namespace Miracom.SmartWeb.UI
{
    public sealed class RptUChart
    {
        private static int i;
        private static int j;
        private static int iIndex;
        private static int iCol;
        public static string[] NumericChartTypes = new string[] { "ColumnChart", "StackColumnChart", "BarChart", "StackBarChart", "LineChart", "SplineChart", "AreaChart", "SplineAreaChart", "StackLineChart", "StackSplineChart", "StackAreaChart", "StackSplineAreaChart", "RadarChart", "PieChart", "DoughnutChart", "ParetoChart" };
            
        public static DataTable GetData(DataTable DT, int KeyCol, int StartCol, int ColGap)
        {

            DataTable Table = new DataTable();
            DataColumn Col = new DataColumn();            

            Col.ReadOnly = false;
            Col.Unique = false;
            Col.ColumnName = "Key Column";
            Col.DataType = System.Type.GetType("System.String");
            Table.Columns.Add(Col);
                        
            for (i = StartCol; i < DT.Columns.Count; i += ColGap)
            {
                Col = new DataColumn();

                Col.ReadOnly = false;
                Col.Unique = false;
               
                Col.ColumnName = DT.Columns[i].ColumnName;
                Col.DataType = System.Type.GetType("System.Double");
                Table.Columns.Add(Col);                
            }

            for (i = 0; i < DT.Rows.Count; i++)
            {
                if (Convert.ToString(DT.Rows[i].ItemArray[0]) != "Total" && Convert.ToString(DT.Rows[i].ItemArray[0]) != "Sub Total")
                {
                    iIndex = 1;
                    DataRow Row = null;
                    //string Xcol = null;
                    //int k;
                    //for (k = 0; k < KeyCol.Length; k++)
                    //{
                    //    Xcol += Convert.ToString(DT.Rows[k].ItemArray[KeyCol[k]]);
                    //    if (k != KeyCol.Length) { Xcol += " / "; }
                    //}
                    Row = Table.NewRow();
                    Row[0] = Convert.ToString(DT.Rows[i].ItemArray[KeyCol]); //Xcol;
                    Table.Rows.Add(Row);
                    for (j = StartCol; j < DT.Columns.Count; j += ColGap)
                    {
                        Table.Rows[i][iIndex] = DT.Rows[i].ItemArray[j];
                        iIndex++;
                    }
                }
            }
            return Table;
        }

        public static DataTable GetDataColKey(DataTable DT,int KeyCol, int StartCol, int ColGap)
        {
            DataTable Table = new DataTable();
            DataColumn Col = new DataColumn();

            Col.ReadOnly = false;
            Col.Unique = false;
            Col.ColumnName = "Key Column";
            Col.DataType = System.Type.GetType("System.String");
            Table.Columns.Add(Col);

            for (i = 0; i < DT.Rows.Count; i ++)
            {
                if (Convert.ToString(DT.Rows[i].ItemArray[0]) != "Total" && Convert.ToString(DT.Rows[i].ItemArray[0]) != "Sub Total")
                {
                    Col = new DataColumn();

                    Col.ReadOnly = false;
                    Col.Unique = false;

                    Col.ColumnName = Convert.ToString(DT.Rows[i].ItemArray[KeyCol]);
                    Col.DataType = System.Type.GetType("System.Double");
                    Table.Columns.Add(Col);
                }
            }

            iIndex = 0;
            for (i = StartCol; i < DT.Columns.Count; i+=ColGap)
            {
                iCol = 1;
                DataRow Row = null;
                Row = Table.NewRow();
                Row[0] = Convert.ToString(DT.Columns[i].ColumnName);
                Table.Rows.Add(Row);
                for (j = 0; j < DT.Rows.Count; j ++)
                {
                    if (Convert.ToString(DT.Rows[j].ItemArray[0]) != "Total" && Convert.ToString(DT.Rows[j].ItemArray[0]) != "Sub Total")
                    {
                        Table.Rows[iIndex][iCol] = DT.Rows[j].ItemArray[i];
                        iCol++;
                    }                    
                }
                iIndex++;
            }
            return Table;
        }

        public static Infragistics.Win.UltraWinChart.UltraChart ShowUChart(Infragistics.Win.UltraWinChart.UltraChart UChart, DataTable Table, string UChartType)
        {
            switch ((ChartType)Enum.Parse(typeof(ChartType), UChartType))
            {
                case ChartType.ColumnChart:
                    UChart.ChartType = ChartType.ColumnChart;
                    break;
                case ChartType.BarChart:
                    UChart.ChartType = ChartType.BarChart;
                    break;
                case ChartType.StackColumnChart:
                    UChart.ChartType = ChartType.StackColumnChart;
                    break;
                case ChartType.StackBarChart:
                    UChart.ChartType = ChartType.StackBarChart;
                    break;
                case ChartType.LineChart:
                    UChart.ChartType = ChartType.LineChart;
                    break;
                case ChartType.SplineChart:
                    UChart.ChartType = ChartType.SplineChart;
                    break;
                case ChartType.AreaChart:
                    UChart.ChartType = ChartType.AreaChart;
                    break;
                case ChartType.SplineAreaChart:
                    UChart.ChartType = ChartType.SplineAreaChart;
                    break;
                case ChartType.StackSplineAreaChart:
                    UChart.ChartType = ChartType.StackSplineAreaChart;
                    break;
                case ChartType.StackLineChart:
                    UChart.ChartType = ChartType.StackLineChart;
                    break;
                case ChartType.StackAreaChart:
                    UChart.ChartType = ChartType.StackAreaChart;
                    break;
                case ChartType.RadarChart:
                    UChart.ChartType = ChartType.RadarChart;
                    break;
                case ChartType.PieChart:
                    UChart.ChartType = ChartType.PieChart;
                    break;
                case ChartType.DoughnutChart:
                    UChart.ChartType = ChartType.DoughnutChart;
                    UChart.DoughnutChart.Concentric = true;
                    break;
            }
            
            UChart.Axis.X.Extent = 19; 
            UChart.Axis.Y.Extent = 33;
            UChart.Data.DataSource = Table;
            UChart.Data.DataBind();

            return UChart;
        }

        public class UChartOption
        {
            public static Infragistics.Win.UltraWinChart.UltraChart AxisItemFormat(Infragistics.Win.UltraWinChart.UltraChart UChart, string XItemFormatString, string YItemFormatString)
            {
                UChart.Axis.X.Labels.Orientation = TextOrientation.Horizontal;
                UChart.Axis.X.Labels.ItemFormatString = XItemFormatString;
                UChart.Axis.Y.Labels.ItemFormatString = YItemFormatString;
               
                return UChart;
            }

            public static Infragistics.Win.UltraWinChart.UltraChart DataPointLabel(Infragistics.Win.UltraWinChart.UltraChart UChart, Boolean Visible, string UChartType, string DataFormat)
            {
                ChartTextAppearance chartTextAppearance1 = new ChartTextAppearance();
                ColumnChartAppearance columnChartAppearance1 = new ColumnChartAppearance();
                LineChartAppearance lineChartAppearance1 = new LineChartAppearance();
                DoughnutChartAppearance doughnuChartAppearance1 = new DoughnutChartAppearance();                
                PieChartAppearance pieChartAppearance1 = new PieChartAppearance();
                BarChartAppearance barChartAppearance1 = new BarChartAppearance();

                if (Visible == true)
                {
                    chartTextAppearance1.Visible = true;
                    chartTextAppearance1.ChartTextFont = new System.Drawing.Font("Arial", 7F);
                    chartTextAppearance1.Column = -2;
                    chartTextAppearance1.ItemFormatString = DataFormat;
                    chartTextAppearance1.Row = -2;
                }
                else
                {
                    chartTextAppearance1.Visible = false;                                    
                }

                switch ((ChartType)Enum.Parse(typeof(ChartType), UChartType))
                {
                    case ChartType.ColumnChart:
                        columnChartAppearance1.ChartText.Add(chartTextAppearance1);
                        UChart.ColumnChart = columnChartAppearance1;
                        break;
                    case ChartType.LineChart:
                        lineChartAppearance1.ChartText.Add(chartTextAppearance1);
                        UChart.LineChart = lineChartAppearance1;
                        break;
                    case ChartType.DoughnutChart:
                        doughnuChartAppearance1.ChartText.Add(chartTextAppearance1);
                        UChart.DoughnutChart  = doughnuChartAppearance1;                        
                        break;
                    case ChartType.PieChart:
                        pieChartAppearance1.ChartText.Add(chartTextAppearance1);
                        UChart.PieChart = pieChartAppearance1;
                        break;
                    case ChartType.BarChart:
                        barChartAppearance1.ChartText.Add(chartTextAppearance1);
                        UChart.BarChart = barChartAppearance1;
                        break;
                }
                
                return UChart;
            }

            public static Infragistics.Win.UltraWinChart.UltraChart VisibleYaxis(Infragistics.Win.UltraWinChart.UltraChart UChart)
            {
                UChart.Axis.Y.Visible = false;
                return UChart;
            }
            
            public static Infragistics.Win.UltraWinChart.UltraChart UChartTitle(Infragistics.Win.UltraWinChart.UltraChart UChart, string Toptitle)
            {
                UChart.TitleTop.Text = Toptitle;
                UChart.TitleTop.HorizontalAlign = System.Drawing.StringAlignment.Center;
                return UChart;
            }

            public static Infragistics.Win.UltraWinChart.UltraChart MinMaxRange(Infragistics.Win.UltraWinChart.UltraChart UChart, double MinRange, double MaxRange)
            {
                UChart.Data.UseMinMax = true;
                UChart.Data.MinValue = MinRange;
                UChart.Data.MaxValue = MaxRange;
                return UChart;
            }

            public class Legend
            {
                public static Infragistics.Win.UltraWinChart.UltraChart ShowLegend(Infragistics.Win.UltraWinChart.UltraChart UChart, string Location)
                {
                    UChart.Legend.Visible = true;
                    UChart.Legend.SpanPercentage = 20;
                    UChart.Legend.Location = (LegendLocation)System.Enum.Parse(typeof(LegendLocation), Location);
                    return UChart;
                }

                public class Legend_Location
                {
                    public static string Top() { return "Top"; }
                    public static string Left() { return "Left"; }
                    public static string Right() { return "Right"; }
                    public static string Bottom() { return "Bottom"; }
                }
            }   
        }
    }
}
