using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;

using System.Collections;
using System.Diagnostics;
using FarPoint.Win.Spread;
using System.Windows.Forms.DataVisualization.Charting;

namespace Miracom.SmartWeb.UI.Controls
{

    //public enum DataTypes { Initeger = 0, Double, Double2, Percent, PercentDouble, PercentDouble2 };
    //public enum SeriseType { Rows, Column };
    //public enum AsixType { Y, Y2 };
    //public enum MSChartType { Stacked, Column, Line, ThinLine, Area, Curve, CurveArea };

    public partial class udcMSChart : System.Windows.Forms.DataVisualization.Charting.Chart
    {
        
        private int SerCount;
        private int XCount;

        public udcMSChart()
        {
            InitializeComponent();
        }


        public void SetChartType(SeriesChartType type, Series seris)
        {
            switch (type)
            {
                case SeriesChartType.Column:
                    seris.ChartType = SeriesChartType.Column;
                    //seris.Palette = ChartColorPalette.BrightPastel;
                    break;
                case SeriesChartType.Line:
                    seris.ChartType = SeriesChartType.Line;
                    seris.BorderWidth = 3;
                    seris.MarkerStyle = MarkerStyle.Circle;
                    seris.MarkerSize = 4;
                    break;
                //case ChartType.Stacked:
                //    seris.Gallery = SoftwareFX.ChartFX.Gallery.Bar;
                //    seris.Stacked = true;
                //    seris.Scheme = SoftwareFX.ChartFX.Scheme.Solid;
                //    seris.CylSides = (short)(50);
                //    seris.Volume = (short)(50);
                //    break;
                //case ChartType.Bar:
                //    seris.Gallery = SoftwareFX.ChartFX.Gallery.Bar;
                //    seris.Scheme = SoftwareFX.ChartFX.Scheme.Solid;
                //    seris.CylSides = (short)(50);
                //    seris.Volume = (short)(50);
                //    break;
                //case ChartType.Area:
                //    seris.Gallery = SoftwareFX.ChartFX.Gallery.Area;
                //    break;
                //case ChartType.CurveArea:
                //    seris.Gallery = SoftwareFX.ChartFX.Gallery.CurveArea;
                //    break;
                //case ChartType.Line:
                //    seris.Gallery = SoftwareFX.ChartFX.Gallery.Lines;
                //    seris.LineWidth = 3;
                //    seris.MarkerShape = SoftwareFX.ChartFX.MarkerShape.Circle;
                //    seris.MarkerSize = ((short)(4));
                //    break;
                //case ChartType.ThinLine:
                //    seris.Gallery = SoftwareFX.ChartFX.Gallery.Lines;
                //    seris.LineWidth = 1;
                //    seris.MarkerShape = SoftwareFX.ChartFX.MarkerShape.None;
                //    break;
                //case ChartType.Curve:
                //    seris.Gallery = SoftwareFX.ChartFX.Gallery.Curve;
                //    seris.LineWidth = 1;
                //    seris.MarkerShape = SoftwareFX.ChartFX.MarkerShape.None;
                //    break;
            }
        }

        // Percent인 경우 100을 곱해주기 위하여.
        private double GetCrossValue(DataTypes type)
        {
            if (((int)type) < 3)
                return 1;
            else
                return 100.0;
        }

        // 차트 디자인을 초기화 하는 부분..
        public void RPT_1_ChartInit()
        {
            Legend legend = null;
            ChartArea chartArea = null;
            //Series series = null;

            //this.ClearData(SoftwareFX.ChartFX.ClearDataFlag.AllData);     // ms chart에서 ???            
                   
            this.Series.Clear();
            this.ChartAreas.Clear();
            this.Legends.Clear();
            this.Titles.Clear();
            this.SerCount = 0;
            this.XCount = 0;

            chartArea = new ChartArea("Default");            
            this.ChartAreas.Add(chartArea);

            legend = new Legend("Default");
            this.Legends.Add(legend);

            //this.AxisY.Interlaced = true;
            //this.AxisY.Font = new System.Drawing.Font("굴림체", 6, System.Drawing.FontStyle.Regular);
            //this.AxisY2.Font = new System.Drawing.Font("굴림체", 6, System.Drawing.FontStyle.Regular);
            //this.AxisX.Font = new System.Drawing.Font("굴림체", 6, System.Drawing.FontStyle.Regular);
            //this.Palette = "Default.ChartFX6";            
            this.ChartAreas[0].AxisY.IsInterlaced = true;
            this.ChartAreas[0].AxisY.LabelStyle.Font = new System.Drawing.Font("굴림체", 8, System.Drawing.FontStyle.Regular);
            this.ChartAreas[0].AxisY2.LabelStyle.Font = new System.Drawing.Font("굴림체", 8, System.Drawing.FontStyle.Regular);
            this.ChartAreas[0].AxisX.LabelStyle.Font = new System.Drawing.Font("굴림체", 8, System.Drawing.FontStyle.Regular);            
            //this.Series[0].Palette = ChartColorPalette.BrightPastel;
                        
            
            //this.AxisY.Gridlines = true;
            //this.AxisY.LabelsFormat.Decimals = 0;
            //this.AxisY2.LabelsFormat.Decimals = 0;
            //this.AxisY.Grid.Color = System.Drawing.Color.DimGray;
            //this.AxisX.LabelAngle = ((short)(0));
            
            this.ChartAreas[0].AxisY.MajorGrid.LineColor = System.Drawing.Color.DimGray;

            //this.Scheme = SoftwareFX.ChartFX.Scheme.Solid;
            //this.CylSides = (short)(50);
            //this.Volume = (short)(50);

            //this.Highlight.Enabled = true;
            //this.Highlight.Dimmed = false;
            //this.Highlight.PointAttributes.PointLabelColor = System.Drawing.Color.Black;
            //this.Highlight.PointAttributes.Color = System.Drawing.Color.Red;
            //this.Highlight.PointAttributes.PointLabels = true;
        }

        // 차트내에 모든 데이타를 제거
        public void RPT_2_ClearData()
        {
            //this.ClearData(SoftwareFX.ChartFX.ClearDataFlag.AllData);            
            this.Series.Clear();
            //this.ChartAreas.Clear();
            //this.Legends.Clear();
            //this.Titles.Clear();

            this.SerCount = 0;
            this.XCount = 0;
        }

        // 차트내 데이타 셋팅 준비
        public void RPT_3_OpenData(int SeriseCount, int XasixCount)
        {
            //this.OpenData(SoftwareFX.ChartFX.COD.Values, SeriseCount, XasixCount);
            
            Series series = null;
            for (int i = 0; i < SeriseCount; i++)
            {
                series = new Series();
                this.Series.Add(series);
            }

            XCount = XasixCount;
        }

        // Pie 차트 일때 동적으로 chart control 생성 관련
        public void RPT_3_OpenData(int SeriseCount, int XasixCount, DataTable dt)
        {
            //this.OpenData(SoftwareFX.ChartFX.COD.Values, SeriseCount, XasixCount);

            ChartArea pieChartArea = null;
            Title pieTitle = null;
            Series series = null;

            for (int i = 0; i < SeriseCount; i++)
            {
                pieChartArea = new ChartArea();
                pieChartArea.Name = "pieChartArea" + (i + 1);
                this.ChartAreas.Add(pieChartArea);

                pieTitle = new Title(Convert.ToString(dt.Columns[i + 1]));
                pieTitle.DockedToChartArea = pieChartArea.Name.ToString();
                this.Titles.Add(pieTitle);

                series = new Series();
                this.Series.Add(series);
            }

            XCount = XasixCount;
        }

        // 차트 데이타 셋팅을 완료
        public void RPT_5_CloseData()
        {
            //this.CloseData(SoftwareFX.ChartFX.COD.Values);
        }

        // 차트 데이타를 셋팅
        // 여러번 호출하여 사용해도 가능
        // 항상 RPT_3_OpenData() 와 RPT_5_CloseData() 사이에 존재해야만 함.
        // SeriseType 따라서 Row 와 Column 둘중에 시리즈가 결정됨. 
        public double RPT_4_AddData(FpSpread spread, int startRowPos, int rowSize, int startColumnPos, int columnSize, SeriseType SerType)
        {
            int[] rows = new int[rowSize];
            int[] columns = new int[columnSize];

            for (int i = 0; i < rows.Length; i++)
                rows[i] = i + startRowPos;
            for (int i = 0; i < columns.Length; i++)
                columns[i] = i + startColumnPos;

            return RPT_4_AddData(spread, rows, columns, SerType, DataTypes.Initeger);
        }

        public double RPT_4_AddData(FpSpread spread, int[] Rows, int[] Columns, SeriseType SerType)
        {
            return RPT_4_AddData(spread, Rows, Columns, SerType, DataTypes.Initeger);
        }

        public double RPT_4_AddData(FpSpread spread, int[] Rows, int[] Columns, SeriseType SerType, DataTypes type)
        {
            double Max = 0;

            try
            {
                // Serise Type 으로 Row 선택한 경우
                if (SerType == SeriseType.Rows)
                {
                    for (int serise = 0; serise < Rows.Length; serise++, SerCount++)
                    {
                        for (int x = 0; x < Columns.Length; x++)
                        {
                            //this.Value[SerCount, x] = Convert.ToDouble(spread.ActiveSheet.Cells[Rows[serise], Columns[x]].Value) * GetCrossValue(type);
                            //Max = Math.Max(Max, this.Value[SerCount, x]);

                            //if (spread.ActiveSheet.Cells[Rows[serise], Columns[x]].Value != null && spread.ActiveSheet.Cells[Rows[serise], Columns[x]].Value != DBNull.Value)
                            //{
                                //this.Series[SerCount].Points.AddXY(spread.ActiveSheet.Cells[Rows[serise], x].Value, spread.ActiveSheet.Cells[Rows[serise], Columns[x]].Value);
                                this.Series[SerCount].Points.AddY(Convert.ToDouble(spread.ActiveSheet.Cells[Rows[serise], Columns[x]].Value) * GetCrossValue(type) );
                                Max = Math.Max(Max, this.Series[SerCount].Points.FindMaxByValue().YValues[0]);
                                //Max

                            //}                            
                        }
                    }
                }
                else
                {
                    // Serise Type 으로 컬럼을 선택한 경우
                    for (int serise = 0; serise < Columns.Length; serise++, SerCount++)
                    {
                        for (int x = 0; x < Rows.Length; x++)
                        {
                            //this.Value[SerCount, x] = Convert.ToDouble(spread.ActiveSheet.Cells[Rows[x], Columns[serise]].Value) * GetCrossValue(type);
                            //Max = Math.Max(Max, this.Value[SerCount, x]);

                            this.Series[SerCount].Points.AddY(Convert.ToDouble(spread.ActiveSheet.Cells[Rows[x], Columns[serise]].Value) * GetCrossValue(type));
                            Max = Math.Max(Max, this.Series[SerCount].Points.FindMaxByValue().YValues[0]);
                            // Max
                        }
                    }

                }

                return Max;
            }
            catch (Exception ex)
            {
                throw new Exception(RptMessages.GetMessage("STD096", GlobalVariable.gcLanguage) + ex.Message);
            }
        }

        public double RPT_4_AddData(DataTable dt, int[] Rows, int[] Columns, SeriseType SerType)
        {
            return RPT_4_AddData(dt, Rows, Columns, SerType, DataTypes.Initeger);
        }

        public double RPT_4_AddData(DataTable dt, int[] Rows, int[] Columns, SeriseType SerType, DataTypes type)
        {
            double Max = 0;

            try
            {
                // Serise Type 으로 Row 선택한 경우
                if (SerType == SeriseType.Rows)
                {
                    for (int serise = 0; serise < Rows.Length; serise++, SerCount++)
                    {
                        for (int x = 0; x < Columns.Length; x++)
                        {
                            if (dt.Rows[Rows[serise]][Columns[x]] != null && dt.Rows[Rows[serise]][Columns[x]] != DBNull.Value)
                            {
                                //this.Value[x, SerCount] = Convert.ToDouble(dt.Rows[Rows[serise]][Columns[x]]) * GetCrossValue(type);
                                //Max = Math.Max(Max, this.Value[x, SerCount]);                                
                                
                                this.Series[x].Points.AddXY(dt.Rows[Rows[serise]][x].ToString(), dt.Rows[Rows[serise]][Columns[x]].ToString());
                                //chart1.Series[0].Points.AddXY(dt_graph.Columns[i + 1].ToString(), Convert.ToInt32(dt_graph.Rows[0][i + 1]));
                                Max = Math.Max(Max, this.Series[x].Points.FindMaxByValue().YValues[0]);

                            }
                        }
                    }
                }
                else 
                {
                    // Serise Type 으로 컬럼을 선택한 경우
                    for (int serise = 0; serise < Columns.Length; serise++, SerCount++)
                    {
                        for (int x = 0; x < Rows.Length; x++)
                        {
                            if (dt.Rows[Rows[x]][Columns[serise]] != null && dt.Rows[Rows[x]][Columns[serise]] != DBNull.Value)
                            {
                                //this.Value[SerCount, x] = Convert.ToDouble(dt.Rows[Rows[x]][Columns[serise]]) * GetCrossValue(type);
                                //Max = Math.Max(Max, this.Value[SerCount, x]);

                                this.Series[SerCount].Points.AddXY(dt.Rows[Rows[x]][serise].ToString(), dt.Rows[Rows[x]][Columns[serise]].ToString());
                                //this.Series[serise].Points.AddXY(Convert.ToDouble(dt.Rows[x][serise]), Convert.ToDouble(dt.Rows[x][serise + 1]));
                                Max = Math.Max(Max, this.Series[SerCount].Points.FindMaxByValue().YValues[0]);
                                
                            }
                        }
                    }
                }

                return Max;
            }
            catch (Exception ex)
            {
                throw new Exception(RptMessages.GetMessage("STD096", GlobalVariable.gcLanguage) + ex.Message);
            }
        }

        //2010-03-26-임종우 : DataTable용 AddData 원본 공용 함수 추가.. 공용함수를 왜 맘대로 바꾸냐고..추가를 하지..
        public double RPT_4_AddData_Original(DataTable dt, int[] Rows, int[] Columns, SeriseType SerType, DataTypes type)
        {
            double Max = 0;

            try
            {
                // Serise Type 으로 Row 선택한 경우
                if (SerType == SeriseType.Rows)
                {
                    for (int serise = 0; serise < Rows.Length; serise++, SerCount++)
                    {
                        for (int x = 0; x < Columns.Length; x++)
                        {
                            if (dt.Rows[Rows[serise]][Columns[x]] != null && dt.Rows[Rows[serise]][Columns[x]] != DBNull.Value)
                            {
                                //this.Value[SerCount, x] = Convert.ToDouble(dt.Rows[Rows[serise]][Columns[x]]) * GetCrossValue(type);
                                //Max = Math.Max(Max, this.Value[SerCount, x]);

                                //this.Series[serise].Points.AddXY(dt.Columns[x + 1].ToString(), Convert.ToDouble(dt.Rows[serise][x + 1]));
                                this.Series[SerCount].Points.AddXY(dt.Rows[Rows[serise]][x].ToString(), dt.Rows[Rows[serise]][Columns[x]].ToString());                                
                                Max = Math.Max(Max, this.Series[SerCount].Points.FindMaxByValue().YValues[0]);
                            }
                        }
                    }

                }
                else
                {
                    // Serise Type 으로 컬럼을 선택한 경우
                    for (int serise = 0; serise < Columns.Length; serise++, SerCount++)
                    {
                        for (int x = 0; x < Rows.Length; x++)
                        {
                            if (dt.Rows[Rows[x]][Columns[serise]] != null && dt.Rows[Rows[x]][Columns[serise]] != DBNull.Value)
                            {
                                //this.Value[SerCount, x] = Convert.ToDouble(dt.Rows[Rows[x]][Columns[serise]]) * GetCrossValue(type);
                                //Max = Math.Max(Max, this.Value[SerCount, x]);

                                this.Series[SerCount].Points.AddXY(dt.Rows[Rows[x]][serise].ToString(), dt.Rows[Rows[x]][Columns[serise]].ToString());
                                Max = Math.Max(Max, this.Series[SerCount].Points.FindMaxByValue().YValues[0]);
                            }
                        }
                    }

                }

                return Max;
            }
            catch (Exception ex)
            {
                throw new Exception(RptMessages.GetMessage("STD096", GlobalVariable.gcLanguage) + ex.Message);
            }
        }

        // 화면의 차트타입을 결정
        public void RPT_6_SetGallery(SeriesChartType gallery, string title, AsixType asixType, DataTypes type, double maxValue)
        {
            // minValue 가 0.6221 이면 셋팅안함.. 
            RPT_6_SetGallery(gallery, title, asixType, type, maxValue, 0.6221);
        }

        // minValue 가 0.6221 이면 셋팅안함.. 
        public void RPT_6_SetGallery(SeriesChartType gallery, string title, AsixType asixType, DataTypes type, double maxValue, double minValue)
        {
            for (int i = 0; i < SerCount; i++)
                SetChartType(gallery, this.Series[i]);

            if (asixType == AsixType.Y)
            {
                for (int i = 0; i < SerCount; i++)
                    this.Series[i].YAxisType = AxisType.Primary;

                this.ChartAreas[0].AxisY.Title = title;
                //this.AxisY.Title.Text = title;
                

                //// Percent 때문에... ^^
                //if (((int)type) < 3)
                //    this.AxisY.LabelsFormat.Decimals = (int)type;
                //else
                //    this.AxisY.LabelsFormat.Decimals = (int)type - 3;

                this.ChartAreas[0].AxisY.Maximum = maxValue;
                //this.AxisY.Max = maxValue;
                
                if (minValue != 0.6221)
                    this.ChartAreas[0].AxisY.Minimum = minValue;
                //    this.AxisY.Min = minValue;

                ////this.AxisY.DataFormat.Decimals = 3;
            }
            else
            {
                for (int i = 0; i < SerCount; i++)
                    this.Series[i].YAxisType = AxisType.Secondary;

                this.ChartAreas[0].AxisY2.Title = title;
                //this.AxisY2.Title.Text = title;

                //// Percent 때문에... ^^
                //if (((int)type) < 3)
                //    this.AxisY2.LabelsFormat.Decimals = (int)type;
                //else
                //    this.AxisY2.LabelsFormat.Decimals = (int)type - 3;

                this.ChartAreas[0].AxisY2.Maximum = maxValue;
                //this.AxisY2.Max = maxValue;

                if (minValue != 0.6221)
                    this.ChartAreas[0].AxisY2.Minimum = minValue;
                //    this.AxisY2.Min = minValue;

                ////this.AxisY.DataFormat.Decimals = 3;
            }

            // Secondary 좌표를 쓰는 경우 꼭 호출해줘야 함. 
            //this.RecalcScale();
            
        }

        public void RPT_6_SetGallery(SeriesChartType gallery, int startSerisePos, int SeriseSize, string title, AsixType asixType, DataTypes type, double maxValue)
        {
            RPT_6_SetGallery(gallery, startSerisePos, SeriseSize, title, asixType, type, maxValue, 0.6221);
        }

        public void RPT_6_SetGallery(SeriesChartType gallery, int startSerisePos, int SeriseSize, string title, AsixType asixType, DataTypes type, double maxValue, double minValue)
        {
            for (int i = startSerisePos; i < startSerisePos + SeriseSize; i++)
            {
                SetChartType(gallery, this.Series[i]);

                if (asixType == AsixType.Y)
                    this.Series[i].YAxisType = AxisType.Primary;
                else
                    this.Series[i].YAxisType = AxisType.Secondary;
            }

            //// Secondary 좌표를 쓰는 경우 꼭 호출해줘야 함. 
            //this.RecalcScale();

            if (asixType == AsixType.Y)
            {
                this.ChartAreas[0].AxisY.Title = title;
                //// Percent 때문에... ^^
                //if (((int)type) < 3)
                //    this.AxisY.LabelsFormat.Decimals = (int)type;
                //else
                //    this.AxisY.LabelsFormat.Decimals = (int)type - 3;
                
                this.ChartAreas[0].AxisY.Maximum = maxValue;

                if (minValue != 0.6221)
                    this.ChartAreas[0].AxisY.Minimum = minValue;

                //this.AxisY.DataFormat.Decimals = 3;
                ////this.AxisY.DataFormat.Format = type;
            }
            else
            {
                this.ChartAreas[0].AxisY2.Title = title;
                //// Percent 때문에... ^^
                //if (((int)type) < 3)
                //    this.AxisY2.LabelsFormat.Decimals = (int)type;
                //else
                //    this.AxisY2.LabelsFormat.Decimals = (int)type - 3;

                this.ChartAreas[0].AxisY2.Maximum = maxValue;

                if (minValue != 0.6221)
                    this.ChartAreas[0].AxisY.Minimum = minValue;
                
                //this.AxisY.DataFormat.Decimals = 3;
                ////this.AxisY.DataFormat.Format = type;
            }
        }


        // x 축 좌표 셋팅
        public void RPT_7_SetXAsixTitle(string[] Titles)
        {
            if (XCount < Titles.Length)
                throw new Exception(RptMessages.GetMessage("STD084", GlobalVariable.gcLanguage) + XCount.ToString());

            for (int i = 0; i < Titles.Length; i++)
                this.Series[0].Points[i].AxisLabel = Titles[i];
                //this.Legend[i] = Titles[i];

        }

        public void RPT_7_SetXAsixTitleUsingSpreadHeader(FpSpread spread, int startHeaderRow, int startHeaderColumn)
        {
            int maxCount = Math.Min(XCount, spread.ActiveSheet.Columns.Count - startHeaderColumn);
            for (int i = 0; i < maxCount; i++)
                this.Series[0].Points[i].AxisLabel = spread.ActiveSheet.ColumnHeader.Cells[startHeaderRow, i + startHeaderColumn].Text;
                //this.Legend[i] = spread.ActiveSheet.ColumnHeader.Cells[startHeaderRow, i + startHeaderColumn].Text;
        }

        public void RPT_7_SetXAsixTitleUsingSpreadHeader(DataTable dt)
        {
            int maxCount = Math.Min(XCount, dt.Rows.Count);
            for (int i = 0; i < maxCount; i++)
                this.Series[0].Points[i].AxisLabel = dt.Rows[i]["OPER"].ToString();
            //    this.Legend[i] = dt.Rows[i]["OPER"].ToString();
        }

        public void RPT_7_SetXAsixTitleUsingSpreadHeader(FpSpread spread, int startHeaderRow, int[] HeaderColumn)
        {
            if (XCount < HeaderColumn.Length)
                throw new Exception(RptMessages.GetMessage("STD084", GlobalVariable.gcLanguage) + XCount.ToString());

            for (int i = 0; i < HeaderColumn.Length; i++)
                this.Series[0].Points[i].AxisLabel = spread.ActiveSheet.ColumnHeader.Cells[startHeaderRow, HeaderColumn[i]].Text;
                //this.Legend[i] = spread.ActiveSheet.ColumnHeader.Cells[startHeaderRow, HeaderColumn[i]].Text;
        }

        public void RPT_7_SetXAsixTitleUsingSpread(FpSpread spread, int[] RowPos, int ColumnPos)
        {

            for (int i = 0; i < RowPos.Length; i++)
                this.Series[0].Points[i].AxisLabel = spread.ActiveSheet.Cells[RowPos[i], ColumnPos].Text;
                //this.Legend[i] = spread.ActiveSheet.Cells[RowPos[i], ColumnPos].Text;
        }

        // 2010-05-25-임종우 : DataTable 의 특정 컬럼 사용 하기 위해 추가함.
        public void RPT_7_SetXAsixTitleUsingDataTable(DataTable dt, int ColumnPos)
        {
            int maxCount = Math.Min(XCount, dt.Rows.Count);
            
            for (int i = 0; i < maxCount; i++)
                this.Series[0].Points[i].AxisLabel = dt.Rows[i][ColumnPos].ToString();
                //this.Legends[i]. = dt.Rows[i][ColumnPos].ToString();            
                //this.Series[i].AxisLabel = dt.Rows[i][ColumnPos].ToString();                
        }

        public void RPT_7_SetXAsixTitleUsingDuration(udcDurationDate duration)
        {
            // 동적으로 생기는 컬럼 생성
            if (duration.DaySelector.SelectedValue.ToString().ToUpper() == "DAY")
            {
                for (int i = 0; i <= duration.SubtractBetweenFromToDate; i++)
                {
                    string Header = duration.FromDate.Value.AddDays(i).ToString("MM.dd");
                    //this.Legend[i] = Header;
                    this.Series[0].Points[i].AxisLabel = Header;
                }
            }
            else if (duration.DaySelector.SelectedValue.ToString().ToUpper() == "WEEK")
            {
                int thisWeek = Convert.ToInt32(duration.FromWeek.SelectedValue.ToString().Substring(4, 2));
                int thisMax = duration.FromWeek.Items.Count;
                int maxWeek = Convert.ToInt32(duration.ToWeek.SelectedValue.ToString().Substring(4, 2));
                int idx = 0;
                string Header = null;

                //년도가 같을 경우
                if (duration.FromWeek.SelectedValue.ToString().Substring(0, 4) ==
                    duration.ToWeek.SelectedValue.ToString().Substring(0, 4))
                {
                    idx = 0;

                    for (int i = thisWeek; i <= maxWeek; i++)
                    {
                        Header = "WW" + (i).ToString();

                        //this.Legend[idx] = Header;
                        this.Series[0].Points[idx].AxisLabel = Header;
                        idx++;
                    }
                }
                else  //다를 경우
                {
                    idx = 0;

                    for (int i = thisWeek; i <= thisMax; i++)
                    {
                        Header = "WW" + (i).ToString();

                        //this.Legend[idx] = Header;
                        this.Series[0].Points[idx].AxisLabel = Header;
                        idx++;
                    }

                    for (int i = 1; i <= maxWeek; i++)
                    {
                        Header = "WW" + (i).ToString();

                        //this.Legend[idx] = Header;
                        this.Series[0].Points[idx].AxisLabel = Header;
                        idx++;
                    }
                }
            }
            else // 달인 경우
            {
                for (int i = 0; i <= duration.SubtractBetweenFromToDate; i++)
                {
                    string Header = duration.FromYearMonth.Value.AddMonths(i).ToString("yy.MM");
                    //this.Legend[i] = Header;
                    this.Series[0].Points[i].AxisLabel = Header;
                }
            }
        }

        // 시리즈 Legend를 셋팅
        public void RPT_8_SetSeriseLegend(string[] SeriseTitle, System.Windows.Forms.DataVisualization.Charting.Docking dock)
        {
            if (SerCount < SeriseTitle.Length)
                throw new Exception(RptMessages.GetMessage("STD085", GlobalVariable.gcLanguage) + SerCount.ToString());

            //this.SerLegBox = true;
            //this.SerLegBoxObj.Docked = dock;

            //for (int i = 0; i < SeriseTitle.Length; i++)
            //    this.SerLeg[i] = SeriseTitle[i];


            this.Series[0].IsVisibleInLegend = true;
            this.Legends[0].Docking = dock;
                        
            for (int i = 0; i < SeriseTitle.Length; i++)
                this.Series[i].LegendText = SeriseTitle[i];
        }

        public void RPT_8_SetSeriseLegend(FpSpread spread, int startRowPos, int RowSize, int ColumnPos, System.Windows.Forms.DataVisualization.Charting.Docking dock)
        {
            if (SerCount < RowSize)
                throw new Exception(RptMessages.GetMessage("STD085", GlobalVariable.gcLanguage) + SerCount.ToString());

            //this.SerLegBox = true;
            //this.SerLegBoxObj.Docked = dock;

            //for (int i = 0; i < RowSize; i++)
            //    this.SerLeg[i] = spread.ActiveSheet.Cells[i + startRowPos, ColumnPos].Text;


            this.Series[0].IsVisibleInLegend = true;
            this.Legends[0].Docking = dock;

            for (int i = 0; i < RowSize; i++)
                this.Series[i].LegendText = spread.ActiveSheet.Cells[i + startRowPos, ColumnPos].Text;
        }

        public void RPT_8_SetSeriseLegend(FpSpread spread, int[] Rows, int Columns, System.Windows.Forms.DataVisualization.Charting.Docking dock)
        {
            if (SerCount < Rows.Length)
                throw new Exception(RptMessages.GetMessage("STD085", GlobalVariable.gcLanguage) + SerCount.ToString());

            //this.SerLegBox = true;
            //this.SerLegBoxObj.Docked = dock;

            //for (int i = 0; i < Rows.Length; i++)
            //    this.SerLeg[i] = spread.ActiveSheet.Cells[Rows[i], Columns].Text;


            this.Series[0].IsVisibleInLegend = true;
            this.Legends[0].Docking = dock;

            for (int i = 0; i < Rows.Length; i++)
                this.Series[i].LegendText = spread.ActiveSheet.Cells[Rows[i], Columns].Text;
        }

        public void RPT_8_SetSeriseLegend(FpSpread spread, int Rows, int[] Columns, System.Windows.Forms.DataVisualization.Charting.Docking dock)
        {
            if (SerCount < Columns.Length)
                throw new Exception(RptMessages.GetMessage("STD085", GlobalVariable.gcLanguage) + SerCount.ToString());

            //this.SerLegBox = true;
            //this.SerLegBoxObj.Docked = dock;

            //for (int i = 0; i < Columns.Length; i++)
            //    this.SerLeg[i] = spread.ActiveSheet.Cells[Rows, Columns[i]].Text;


            this.Series[0].IsVisibleInLegend = true;
            this.Legends[0].Docking = dock;

            for (int i = 0; i < Columns.Length; i++)
                this.Series[i].LegendText = spread.ActiveSheet.Cells[Rows, Columns[i]].Text;
        }

        public void RPT_8_SetSeriseLegend(DataTable dt, int startRowPos, int RowSize, int ColumnPos, System.Windows.Forms.DataVisualization.Charting.Docking dock)
        {
            //this.SerLegBox = true;
            //this.SerLegBoxObj.Docked = dock;

            //for (int i = 0; i < RowSize; i++)
            //    this.SerLeg[i] = (string)dt.Rows[i + startRowPos][ColumnPos];


            this.Series[0].IsVisibleInLegend = true;
            this.Legends[0].Docking = dock;

            for (int i = 0; i < RowSize; i++)
                this.Series[i].LegendText = (string)dt.Rows[i + startRowPos][ColumnPos];
        }

        // 차트 시리즈를 제어할수 있는 체크박스를 생성해줌.. 
        public void RPT_9_SetCheckBox()
        {
            RPT_9_SetCheckBox(110);
        }

        public void RPT_9_SetCheckBox(int width)
        {
            RPT_9_SetCheckBox(width, -1, null);

        }

        public void RPT_9_SetCheckBox(int width, int height)
        {
            RPT_9_SetCheckBox(width, height, null);
        }

        public void RPT_9_SetCheckBox(int width, int[] CheckedList)
        {
            RPT_9_SetCheckBox(width, -1, CheckedList);
        }

        public void RPT_9_SetCheckBox(int width, int height, int[] CheckedList)
        {
            int base_height = height;

            // -1 는 더미
            if (height == -1)
                base_height = SerCount * 20 + 15;

            CheckBox[] chk_List = new CheckBox[SerCount];

            for (int i = 0; i < chk_List.Length; i++)
            {
                chk_List[i] = new CheckBox();
                //chk_List[i].Text = this.SerLeg[i];
                chk_List[i].Checked = true;
                chk_List[i].Size = new System.Drawing.Size(width - 10, 20);
                chk_List[i].Location = new System.Drawing.Point(5, 20 * i + 12);
                chk_List[i].CheckedChanged += new EventHandler(udcMSChart_CheckedChanged);
            }

            if (CheckedList != null && CheckedList.Length > 0)
            {
                foreach (CheckBox cbox in chk_List)
                    cbox.Checked = false;

                for (int i = 0; i < CheckedList.Length; i++)
                    chk_List[CheckedList[i]].Checked = true;
            }

        }

        private void udcMSChart_CheckedChanged(object sender, EventArgs e)
        {
            CheckBox box = (CheckBox)sender;
            for (int i = 0; i < SerCount; i++)
            {
                //if (box.Text == this.SerLeg[i])
                //{
                //    this.Series[i].Visible = box.Checked;
                //}
            }
        }

        private void udcMSChart_PrePaint(object sender, ChartPaintEventArgs e)
        {
            System.Windows.Forms.DataVisualization.Charting.Chart iChart = new System.Windows.Forms.DataVisualization.Charting.Chart();
            iChart = (System.Windows.Forms.DataVisualization.Charting.Chart)sender;

            try
            {
                for (int p = 0; p < iChart.Series.Count; p++)
                {
                    //for (int s = 0; s < iChart.Series[p].Points.Count; s++)
                    foreach (DataPoint point in iChart.Series[p].Points.FindAllByValue(0))
                    {
                        point.Label = " ";
                    }
                }

                //foreach (DataPoint dp in chart1.Series["Series1"].Points.FindAllByValue(0))                
                //{
                //    dp.Label = " ";
                //}
            }
            catch (Exception ex)
            {
                throw new Exception(RptMessages.GetMessage("STD097", GlobalVariable.gcLanguage) + ex.Message);
                //LoadingPopUp.LoadingPopUpHidden();
                //CmnFunction.ShowMsgBox(ex.Message);
            }
        }

        private void udcMSChart_MouseMove(object sender, MouseEventArgs e)
        {
            System.Windows.Forms.DataVisualization.Charting.Chart iChart = new System.Windows.Forms.DataVisualization.Charting.Chart();
            iChart = (System.Windows.Forms.DataVisualization.Charting.Chart)sender;

            try
            {
                HitTestResult result = iChart.HitTest(e.X, e.Y);
                string SeriesPoint = string.Empty;
                int seriesCnt = iChart.Series.Count;

                if (result != null)
                {
                    if (seriesCnt != 0)
                    {
                        if (iChart.Series[0].ChartType.ToString() == "Pie") // Pie Chart (공정&출하검사현황조회-공정별 불량 종류 비율)
                        {
                            for (int p = 0; p < seriesCnt; p++)
                            {
                                foreach (DataPoint point in iChart.Series[p].Points)
                                {
                                    point.BackSecondaryColor = Color.Black;
                                    point.BackHatchStyle = ChartHatchStyle.None;
                                    point.BorderWidth = 1;

                                    point.CustomProperties = "PieLabelStyle=Disabled";
                                    //point2.CustomProperties = "PieLineColor=Black";
                                    //point.CustomProperties = "";
                                    //point["PieLabelStyle"] = "Disabled";                             
                                    //point.LabelForeColor = Color.Transparent;
                                }
                            }                            
                        }
                        else
                        {
                            if (seriesCnt > 1)
                            {
                                for (int p = 0; p < iChart.Series.Count; p++)
                                {
                                    iChart.Series[p].BorderWidth = 1;
                                    iChart.Series[p].MarkerSize = 7;
                                    iChart.Series[p].BorderDashStyle = ChartDashStyle.Solid;
                                    iChart.Series[p].BorderColor = Color.Transparent;

                                    //if (cboGraph.SelectedIndex == 2 || cboGraph.SelectedIndex == 3 || cboGraph.SelectedIndex == 4)
                                    //iChart.Series[p].IsValueShownAsLabel = false;
                                }
                            }
                            else if (seriesCnt != 0)
                            {
                                for (int p = 0; p < iChart.Series[0].Points.Count; p++)
                                {
                                    iChart.Series[0].Points[p].BorderWidth = 1;
                                    iChart.Series[0].Points[p].MarkerSize = 7;
                                    iChart.Series[0].Points[p].BorderDashStyle = ChartDashStyle.Solid;
                                    iChart.Series[0].Points[p].BorderColor = Color.Transparent;

                                    //if (cboGraph.SelectedIndex == 2 || cboGraph.SelectedIndex == 3 || cboGraph.SelectedIndex == 4)
                                    //iChart.Series[0].Points[p].IsValueShownAsLabel = false;
                                }
                            }
                        }
                    }
                }

                if (seriesCnt != 0)
                {
                    if (iChart.Series[0].ChartType.ToString() == "Pie") // Graph : 공정별 불량 종류 비율 경우에만 해당
                    {
                        if (result.ChartElementType == ChartElementType.DataPoint)
                        {
                            SeriesPoint = result.Series.Name.ToString();
                            DataPoint point2 = iChart.Series[SeriesPoint].Points[result.PointIndex];
                            DataPoint tooltipPoint = iChart.Series[SeriesPoint].Points[result.PointIndex];

                            tooltipPoint.ToolTip = iChart.Series[SeriesPoint].Name + "\n"
                                       + iChart.Series[SeriesPoint].Points[result.PointIndex].AxisLabel + "\n"
                                       + iChart.Series[SeriesPoint].Points[result.PointIndex].YValues[0].ToString();

                            point2.BackSecondaryColor = Color.Red;
                            point2.BackHatchStyle = ChartHatchStyle.Percent90;
                            point2.BorderWidth = 2;

                            point2.CustomProperties = "PieLabelStyle=Outside";
                            point2.CustomProperties = "PieLineColor=Black";
                            //point2["PieLabelStyle"] = "Outside";
                            //point2.LabelForeColor = Color.Black;
                        }

                        if (result.ChartElementType == ChartElementType.LegendItem)
                        {
                            string legendText = result.Object.ToString();
                            legendText = legendText.Replace("LegendItem-", "");

                            for (int p = 0; p < iChart.Series.Count / 2; p++)
                            {
                                for (int k = 0; k < iChart.Series[p].Points.Count; k++)
                                {
                                    if (iChart.Series[p].Points[k].AxisLabel == legendText)
                                    {
                                        DataPoint point2 = iChart.Series[p].Points[result.PointIndex];

                                        point2.BackSecondaryColor = Color.Red;
                                        point2.BackHatchStyle = ChartHatchStyle.Percent90;
                                        point2.BorderWidth = 2;

                                        //point2["PieLabelStyle"] = "Outside";
                                        //point2.LabelForeColor = Color.Black;

                                        point2.CustomProperties = "PieLabelStyle=Outside";
                                        point2.CustomProperties = "PieLineColor=Black";

                                        point2.Label = "#PERCENT";
                                    }
                                }
                            }
                        }
                    }
                    else
                    {
                        if (result.ChartElementType == ChartElementType.DataPoint)
                        {
                            SeriesPoint = result.Series.Name.ToString();
                            DataPoint point2 = iChart.Series[SeriesPoint].Points[result.PointIndex];
                            DataPoint tooltipPoint = iChart.Series[SeriesPoint].Points[result.PointIndex];

                            tooltipPoint.ToolTip = 
                                //iChart.Series[SeriesPoint].Name + "\n"
                                iChart.Series[SeriesPoint].LegendText + "\n"
                                       + iChart.Series[SeriesPoint].Points[result.PointIndex].AxisLabel + "\n"
                                       + iChart.Series[SeriesPoint].Points[result.PointIndex].YValues[0].ToString();


                            if (seriesCnt > 1)
                            {
                                for (int p = 0; p < iChart.Series.Count; p++)
                                {
                                    iChart.Series[p].BorderWidth = 1;
                                    iChart.Series[p].MarkerSize = 5;
                                    iChart.Series[p].BorderDashStyle = ChartDashStyle.Solid;
                                }

                                iChart.Series[SeriesPoint].BorderWidth = 3;
                                iChart.Series[SeriesPoint].MarkerSize = 10;
                                iChart.Series[SeriesPoint].BorderDashStyle = ChartDashStyle.Dot;
                                iChart.Series[SeriesPoint].BorderColor = Color.Red;
                            }
                            else
                            {
                                point2.BorderWidth = 3;
                                point2.MarkerSize = 10;
                                point2.BorderDashStyle = ChartDashStyle.Dot;
                                point2.BorderColor = Color.Red;
                            }
                        }

                        if (result.ChartElementType == ChartElementType.LegendItem)
                        {
                            SeriesPoint = result.Series.Name.ToString();
                            DataPoint point2 = null;

                            if (seriesCnt > 1)
                            {
                                iChart.Series[SeriesPoint].BorderWidth = 3;
                                iChart.Series[SeriesPoint].MarkerSize = 10;
                                iChart.Series[SeriesPoint].BorderDashStyle = ChartDashStyle.Dot;
                                iChart.Series[SeriesPoint].BorderColor = Color.Red;

                                //if (cboGraph.SelectedIndex == 2 || cboGraph.SelectedIndex == 3 || cboGraph.SelectedIndex == 4)
                                //iChart.Series[SeriesPoint].IsValueShownAsLabel = true;
                            }
                            else
                            {
                                for (int p = 0; p < iChart.Series[0].Points.Count; p++)
                                {
                                    point2 = iChart.Series[SeriesPoint].Points[p];

                                    point2.BorderWidth = 3;
                                    point2.MarkerSize = 10;
                                    point2.BorderDashStyle = ChartDashStyle.Dot;
                                    point2.BorderColor = Color.Red;
                                }
                            }
                        }
                    }
                }

            }
            catch (Exception ex)
            {
                throw new Exception(RptMessages.GetMessage("STD098", GlobalVariable.gcLanguage) + ex.Message);
                //LoadingPopUp.LoadingPopUpHidden();
                //CmnFunction.ShowMsgBox(ex.Message);
            }
        }


    }
}
