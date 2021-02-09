using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using System.Collections;
using System.Windows.Forms;

using Miracom.UI.Controls.MCCodeView;

namespace Miracom.SmartWeb
{
    public sealed class CmnInitFunction
    {
        private const int SP_MAX_COLUMN_WIDTH  = 500;
        private const int SP_MIN_COLUMN_WIDTH  = 30;

        public static void InitSender(string sUrl, int iTimeOut)
        {
            SPLSender.SPLUrl = sUrl;
            SPLSender.SPLTimeOut = iTimeOut;
            FMBSender.FMBUrl = sUrl;
            FMBSender.FMBTimeOut = iTimeOut;
            WIPSender.WIPUrl = sUrl;
            WIPSender.WIPTimeOut = iTimeOut;
        }

        public static void InitListView(ListView MyListView)
        {
            MyListView.Items.Clear();
            MyListView.FullRowSelect = true;
            MyListView.HideSelection = false;
            MyListView.GridLines = false;
            MyListView.View = System.Windows.Forms.View.Details;
        }

        public static void InitTreeView(TreeView MyTreeView)
        {
            MyTreeView.Nodes.Clear();
            MyTreeView.FullRowSelect = true;
            MyTreeView.HideSelection = false;
        }

        /// <summary>
        /// Chart를 초기화한다.
        /// </summary>
        /// <param name="ctlChart">Infragistics.Win.UltraWinChart.UltraChart object </param>
        public static void InitChart(Infragistics.Win.UltraWinChart.UltraChart ctlChart)
        {
            Color[] ChartColors;

            ChartColors = new Color[] {   Color.Blue, 
                                          Color.BlueViolet, 
                                          Color.Brown, 
                                          Color.BurlyWood, 
                                          Color.CadetBlue, 
                                          Color.Chartreuse,
                                          Color.Chocolate,
                                          Color.Coral,
                                          Color.CornflowerBlue,
                                          Color.Crimson,
                                          Color.Cyan,
                                          Color.DarkBlue,
                                          Color.DarkCyan,
                                          Color.DarkGoldenrod,
                                          Color.DarkGray,
                                          Color.DarkGreen,
                                          Color.DarkKhaki,
                                          Color.DarkMagenta,
                                          Color.DarkOliveGreen,
                                          Color.DarkOrange,
                                          Color.DarkOrchid,
                                          Color.DarkRed,
                                          Color.DarkSalmon,
                                          Color.DarkSeaGreen,
                                          Color.DarkSlateBlue,
                                          Color.DarkSlateGray,
                                          Color.DarkTurquoise,
                                          Color.DarkViolet,
                                          Color.DeepPink,
                                          Color.DeepSkyBlue,
                                          Color.DimGray,
                                          Color.DodgerBlue,
                                          Color.Firebrick,
                                          Color.ForestGreen,
                                          Color.Gainsboro,
                                          Color.Gold,
                                          Color.Goldenrod,
                                          Color.Gray,
                                          Color.Green,
                                          Color.HotPink,
                                          Color.IndianRed,
                                          Color.Aqua,
                                          Color.LightBlue,
                                          Color.LightCoral,
                                          Color.LightGray,
                                          Color.LightGreen,
                                          Color.LightPink,
                                          Color.LightSalmon,
                                          Color.LightSeaGreen,
                                          Color.LightSkyBlue,
                                          Color.LightSlateGray,
                                          Color.LightSteelBlue,
                                          Color.Lime,
                                          Color.LimeGreen,
                                          Color.Magenta,
                                          Color.Maroon,
                                          Color.MediumAquamarine,
                                          Color.MediumBlue,
                                          Color.MediumOrchid,
                                          Color.MediumPurple,
                                          Color.MediumSeaGreen,
                                          Color.MediumSlateBlue,
                                          Color.MediumSpringGreen,
                                          Color.MediumTurquoise,
                                          Color.MediumVioletRed,
                                          Color.MidnightBlue,
                                          Color.Navy,
                                          Color.Olive,
                                          Color.OliveDrab,
                                          Color.Orange,
                                          Color.OrangeRed,
                                          Color.Orchid,
                                          Color.PaleGoldenrod,
                                          Color.PaleGreen,
                                          Color.PaleTurquoise,
                                          Color.PaleVioletRed,
                                          Color.PeachPuff,
                                          Color.Peru,
                                          Color.Pink,
                                          Color.Plum,
                                          Color.PowderBlue,
                                          Color.Purple,
                                          Color.Red,
                                          Color.RosyBrown,
                                          Color.RoyalBlue,
                                          Color.SaddleBrown,
                                          Color.Salmon,
                                          Color.SeaGreen,
                                          Color.SeaShell,
                                          Color.Sienna,
                                          Color.Silver,
                                          Color.SkyBlue,
                                          Color.SlateBlue,
                                          Color.SlateGray,
                                          Color.Snow,
                                          Color.SpringGreen,
                                          Color.SteelBlue,
                                          Color.Tan,
                                          Color.Teal,
                                          Color.Thistle,
                                          Color.Tomato,
                                          Color.Transparent,
                                          Color.Turquoise,
                                          Color.Violet,
                                          Color.Wheat,
                                          Color.Yellow,
                                          Color.YellowGreen};
            

            // Init UntraChartChart.
            ctlChart.Series.Clear();
            ctlChart.TitleBottom.Text = "";
            ctlChart.TitleLeft.Text = "";
            ctlChart.TitleRight.Text = "";
            ctlChart.TitleTop.Text = "";

            ctlChart.ColorModel.ModelStyle = Infragistics.UltraChart.Shared.Styles.ColorModels.CustomLinear;
            ctlChart.ColorModel.CustomPalette = ChartColors;
        }

        public static void InitLockSpread(FarPoint.Win.Spread.FpSpread spdCtl)
        {
            try
            {
                for (int i = 0; i < spdCtl.Sheets.Count; i++)
                {
                    for (int j = 0; j < spdCtl.Sheets[i].ColumnCount; j++)
                    {
                        spdCtl.Sheets[i].Columns[j].Locked = true;
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="spdCtl">Spread: object </param>
        /// 
        public static void InitSpread(FarPoint.Win.Spread.FpSpread spdCtl)
        {
            InitSpread(spdCtl, false);
        }

        public static void InitSpread(FarPoint.Win.Spread.FpSpread spdCtl, bool bRowHearder)
        {
            InitSpread(spdCtl, bRowHearder, true);
        }

        public static void InitSpread(FarPoint.Win.Spread.FpSpread spdCtl, bool bRowHearder, bool bSort)
        {
            FarPoint.Win.Spread.SheetSkin spSkin;

            try
            {
                FarPoint.Win.Spread.DefaultSpreadSkins.GetAt(1).Apply(spdCtl);
                spSkin = new FarPoint.Win.Spread.SheetSkin("InitSkin",
                                           System.Drawing.Color.White,
                                           System.Drawing.Color.Empty,
                                           System.Drawing.Color.Empty,
                                           System.Drawing.Color.White,
                                           FarPoint.Win.Spread.GridLines.Both,
                                           System.Drawing.Color.FromArgb(108, 144, 188),
                                           System.Drawing.Color.White,
                                           System.Drawing.Color.FromArgb(192, 192, 255),
                                           System.Drawing.Color.White,
                                           System.Drawing.Color.Empty,
                                           System.Drawing.Color.White,
                    //System.Drawing.Color.FromArgb(231, 231, 255),
                    //System.Drawing.Color.FromArgb(247, 247, 222),
                                           true,
                                           true,
                                           false,
                                           true,
                                           bRowHearder);

                spdCtl.BorderStyle = BorderStyle.FixedSingle;
                spdCtl.BackColor = System.Drawing.Color.Silver;
                spdCtl.HorizontalScrollBarPolicy = FarPoint.Win.Spread.ScrollBarPolicy.AsNeeded;
                spdCtl.VerticalScrollBarPolicy = FarPoint.Win.Spread.ScrollBarPolicy.AsNeeded;
                spdCtl.MoveActiveOnFocus = true;

                spdCtl.ColumnSplitBoxAlignment = FarPoint.Win.Spread.SplitBoxAlignment.Leading;
                spdCtl.ColumnSplitBoxPolicy = FarPoint.Win.Spread.SplitBoxPolicy.AsNeeded;

                spdCtl.RowSplitBoxPolicy = FarPoint.Win.Spread.SplitBoxPolicy.Never;

                spdCtl.SetCursor(FarPoint.Win.Spread.CursorType.Normal, System.Windows.Forms.Cursors.Arrow);

                for (int i = 0; i < spdCtl.Sheets.Count; i++)
                {
                    //"Verdana", "Tahoma", "Times New Roman", "돋움"
                    Font myFonts = new Font("Tahoma", 8F, FontStyle.Regular);

                    spSkin.Apply(spdCtl.Sheets[i]);
                    
                    spdCtl.Sheets[i].RowCount = 0;
                    spdCtl.Sheets[i].OperationMode = FarPoint.Win.Spread.OperationMode.Normal;
                    spdCtl.Sheets[i].Columns[-1].VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;

                    spdCtl.Sheets[i].ColumnHeader.DefaultStyle.Font = myFonts;

                    spdCtl.Sheets[i].DefaultStyle.Font = myFonts;
                    spdCtl.Sheets[i].DefaultStyle.VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;

                    // Auto sort setting.
                    spdCtl.Sheets[i].SetColumnAllowAutoSort(0, spdCtl.Sheets[i].ColumnCount, bSort);
                    spdCtl.Sheets[i].ColumnHeader.AutoSortIndex = 0;
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static void ResizeSpreadColumnHeader(FarPoint.Win.Spread.FpSpread spdCtl)
        {
            Single size;
            int i;
            int j;
            int iIndex;

            spdCtl.SuspendLayout();
            for (iIndex = 0; iIndex < spdCtl.Sheets.Count; iIndex++)
            {
                for (j = 0; j < spdCtl.Sheets[iIndex].ColumnHeader.RowCount; j++)
                {
                    spdCtl.Sheets[iIndex].RowCount++;


                    for (i = 0; i < spdCtl.Sheets[iIndex].Columns.Count; i++)
                    {
                        spdCtl.Sheets[iIndex].Cells[spdCtl.Sheets[iIndex].RowCount - 1, i].Value = spdCtl.Sheets[iIndex].ColumnHeader.Cells[j, i].Value;
                    }

                    for (i = 0; i < spdCtl.Sheets[iIndex].Columns.Count; i++)
                    {
                        size = spdCtl.Sheets[iIndex].GetPreferredColumnWidth(i) + 5;

                        if (size > SP_MAX_COLUMN_WIDTH)
                        {
                            size = SP_MAX_COLUMN_WIDTH;
                        }
                        else if (size < SP_MIN_COLUMN_WIDTH)
                        {
                            size = SP_MIN_COLUMN_WIDTH;
                        }

                        spdCtl.Sheets[iIndex].ColumnHeader.Columns[i].Width = size;

                    }

                    spdCtl.Sheets[iIndex].RowCount = spdCtl.Sheets[iIndex].RowCount - spdCtl.Sheets[iIndex].ColumnHeader.RowCount;
                }

            }
            spdCtl.ResumeLayout(false);
        }

        public static void ResizeSpreadColumnHeader(FarPoint.Win.Spread.FpSpread spdCtl, int SheetIndex, int startCol, int EndCol)
        {
            Single size;
            int i;
            int j;
            
            spdCtl.SuspendLayout();
            for (j = 0; j < spdCtl.Sheets[SheetIndex].ColumnHeader.RowCount; j++)
            {
                spdCtl.Sheets[SheetIndex].RowCount++;


                for (i = startCol; i < EndCol; i++)
                {
                    spdCtl.Sheets[SheetIndex].Cells[spdCtl.Sheets[SheetIndex].RowCount - 1, i].Value = spdCtl.Sheets[SheetIndex].ColumnHeader.Cells[j, i].Value;
                }

                for (i = startCol; i < EndCol; i++)
                {
                    size = spdCtl.Sheets[SheetIndex].GetPreferredColumnWidth(i) + 5;

                    if (size > SP_MAX_COLUMN_WIDTH)
                    {
                        size = SP_MAX_COLUMN_WIDTH;
                    }
                    else if (size < SP_MIN_COLUMN_WIDTH)
                    {
                        size = SP_MIN_COLUMN_WIDTH;
                    }

                    spdCtl.Sheets[SheetIndex].ColumnHeader.Columns[i].Width = size;

                }

                spdCtl.Sheets[SheetIndex].RowCount = spdCtl.Sheets[SheetIndex].RowCount - spdCtl.Sheets[SheetIndex].ColumnHeader.RowCount;
            }

            spdCtl.ResumeLayout(false);
        }

        /// <summary>
        /// 지정된 Spread의 전체 Columns Width를 Columns에 표시된 값의 최소길이로 조정한다.
        /// </summary>
        /// <param name="spdCtl">Spread: object </param>
        public static void SpreadColumnsWidthAutoFit(FarPoint.Win.Spread.FpSpread spdCtl)
        {
            float colWidth;

            spdCtl.SuspendLayout();
            for (int i = 0; i < spdCtl.Sheets.Count; i++)
            {
                for (int j = 0; j < spdCtl.Sheets[i].ColumnCount; j++)
                {
                    colWidth = spdCtl.Sheets[i].Columns[j].GetPreferredWidth();
                    spdCtl.Sheets[i].Columns[j].Width = colWidth;
                }
            }
            spdCtl.ResumeLayout(false);
        }

		/// <summary>
		/// Clear List Control(ListView, ComboBox, TreeView, FpSpread)
		/// </summary>
		/// <param name="ListControl"></param>
		/// <param name="SpaceFlag"></param>
        public static void ClearList(System.Windows.Forms.Control ListControl, Boolean SpaceFlag)
		{
			if (ListControl is ListView) {
				((ListView)ListControl).Items.Clear();
			}
			else if (ListControl is ComboBox) {
				((ComboBox)ListControl).Items.Clear();
				if (SpaceFlag == true) {
					((ListView)ListControl).Items.Add("");
				}
			}
			else if (ListControl is TreeView) {
				((TreeView)ListControl).Nodes.Clear();
			}
			else if (ListControl is FarPoint.Win.Spread.FpSpread) {
				FarPoint.Win.Spread.SheetView spdView = null;

				spdView = ((FarPoint.Win.Spread.FpSpread)ListControl).Sheets[0];
				spdView.ClearRange(0, 0, spdView.RowCount, spdView.ColumnCount, true);
				spdView.RowCount = 0;
			}
		}

		/// <summary>
		/// Win.Form안의 Field(ListView, TreeView, FpSpread은 제외)내용을 지우고 초기화 한다.
		/// </summary>
		/// <param name="wForm">Win.Form</param>
		/// <param name="ExceptCtls">초기화에서 제외되는 컨트롤배열</param>
		/// <param name="bItemsClear">컨트롤의 하위항목 초기화 여부</param>
		/// <returns>true, false</returns>
		public static Boolean FieldClear(object wForm, object [] ExceptCtls, Boolean bItemsClear)
		{
			Boolean bExceptFlag;

			foreach (System.Windows.Forms.Control controlObj in ((System.Windows.Forms.Control)wForm).Controls)
			{
				bExceptFlag = false;

				// 그룹항목인지 비교하여 재귀호출여부를 결정.
				if (controlObj is SplitContainer || controlObj is SplitterPanel ||
					controlObj is Panel || controlObj is GroupBox || 
					controlObj is TabControl || controlObj is TabPage) {
					FieldClear(controlObj, ExceptCtls, false);
				} 
				else {
					bExceptFlag = false;
					if (ExceptCtls != null)
					{
						// 제외항목의 검색.
						for (int j = 0; j < ExceptCtls.Length; j++)
						{
							if (ExceptCtls[j].Equals(controlObj))
							{
								bExceptFlag = true;
								break;
							}
							else
							{
								bExceptFlag = false;
							}
						}
					}

					if (bExceptFlag == false) {
						if (controlObj is TextBox) {
							((TextBox)controlObj).Text = "";
						}
						else if (controlObj is CheckBox) {
							((CheckBox)controlObj).Checked = false;
						}
						else if (controlObj is ComboBox) {
							((ComboBox)controlObj).SelectedIndex = -1;
							if (bItemsClear == true) {
								((ComboBox)controlObj).Items.Clear();
							}
						}
						else if (controlObj is RadioButton) {
							((RadioButton)controlObj).Checked = false;
						}
						else if (controlObj is MCCodeView)
						{
							((MCCodeView)controlObj).Text = "";
							if (bItemsClear == true)
							{
								((MCCodeView)controlObj).Items.Clear();
							}
						}
						else if (controlObj is DateTimePicker)
						{
							if (((DateTimePicker)controlObj).Format == DateTimePickerFormat.Custom)
							{
								((DateTimePicker)controlObj).Value = ((DateTimePicker)controlObj).MinDate;
								((DateTimePicker)controlObj).CustomFormat = " ";
							}
							else
							{
								((DateTimePicker)controlObj).Value = DateTime.MinValue;
							}
						}
						else if (controlObj is NumericUpDown) {
							((NumericUpDown)controlObj).Value = 0;
						}
					}
				}
			}

			return true;
		}

    }
}
