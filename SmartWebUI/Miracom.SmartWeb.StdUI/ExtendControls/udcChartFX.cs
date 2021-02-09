//-----------------------------------------------------------------------------
//
//   System      : MES Report
//   File Name   : 
//   Description : Client Common function Module 
//
//   MES Version : 4.x.x.x
//
//   History
//       - **** Do Not Modify in Site!!! ****
//       - 2008-10-01 : Created by John Seo
//
//
//   Copyright(C) 1998-2005 MIRACOM,INC.
//   All rights reserved.
//
//-----------------------------------------------------------------------------
using System;
using System.ComponentModel;
using System.Collections;
using System.Diagnostics;
using System.Data;
using System.Windows.Forms;
using FarPoint.Win.Spread;

namespace Miracom.SmartWeb.UI.Controls
{

	public enum DataTypes { Initeger = 0, Double, Double2, Percent, PercentDouble, PercentDouble2 };
	public enum SeriseType { Rows, Column };
	public enum AsixType { Y, Y2 };
    public enum ChartType { Stacked, Bar, Line, ThinLine, Area, Curve, CurveArea };

    public class udcChartFX : SoftwareFX.ChartFX.Chart  
	{
		/// <summary>
		/// �ʼ� �����̳� �����Դϴ�.
		/// </summary>
		private System.ComponentModel.Container components = null;
		private int SerCount;
		private int XCount;

		public udcChartFX(System.ComponentModel.IContainer container)
		{
			///
			/// Windows.Forms Ŭ���� �������� �����̳� ������ �ʿ��մϴ�.
			///
			container.Add(this);
			InitializeComponent();
			//
			// TODO: InitializeComponent�� ȣ���� ���� ������ �ڵ带 �߰��մϴ�.
			//
		}

		public udcChartFX()
		{
			///
			/// Windows.Forms Ŭ���� �������� �����̳� ������ �ʿ��մϴ�.
			///
			InitializeComponent();
			//
			// TODO: InitializeComponent�� ȣ���� ���� ������ �ڵ带 �߰��մϴ�.
			//
		}

		/// <summary> 
		/// ��� ���� ��� ���ҽ��� �����մϴ�.
		/// </summary>
		protected override void Dispose( bool disposing )
		{
			if( disposing )
			{
				if(components != null)
				{
					components.Dispose();
				}
			}
			base.Dispose( disposing );
		}


		#region ���� ��� �����̳ʿ��� ������ �ڵ�
		/// <summary>
		/// �����̳� ������ �ʿ��� �޼����Դϴ�.
		/// �� �޼����� ������ �ڵ� ������� �������� ���ʽÿ�.
		/// </summary>
		private void InitializeComponent()
		{
			components = new System.ComponentModel.Container();
		}
		#endregion
		
		public void SetChartType ( ChartType type , SoftwareFX.ChartFX.SeriesAttributes seris )
		{ 
            switch ( type )
			{
				case ChartType.Stacked:
					seris.Gallery = SoftwareFX.ChartFX.Gallery.Bar;
					seris.Stacked = true;
					seris.Scheme = SoftwareFX.ChartFX.Scheme.Solid;
					seris.CylSides = (short)(50);
					seris.Volume = (short)(50);	
					break;
				case ChartType.Bar:
					seris.Gallery = SoftwareFX.ChartFX.Gallery.Bar;
					seris.Scheme = SoftwareFX.ChartFX.Scheme.Solid;
					seris.CylSides = (short)(50);
					seris.Volume = (short)(50);	
					break;
				case ChartType.Area:
					seris.Gallery = SoftwareFX.ChartFX.Gallery.Area;
					break;
                case ChartType.CurveArea:
                    seris.Gallery = SoftwareFX.ChartFX.Gallery.CurveArea;
                    break;
				case ChartType.Line:
					seris.Gallery = SoftwareFX.ChartFX.Gallery.Lines;
					seris.LineWidth = 3;
					seris.MarkerShape = SoftwareFX.ChartFX.MarkerShape.Circle;
					seris.MarkerSize = ((short)(4));
					break;
				case ChartType.ThinLine:
					seris.Gallery = SoftwareFX.ChartFX.Gallery.Lines;                    
					seris.LineWidth = 1;
					seris.MarkerShape = SoftwareFX.ChartFX.MarkerShape.None;
					break;
                case ChartType.Curve:
                    seris.Gallery = SoftwareFX.ChartFX.Gallery.Curve;
                    seris.LineWidth = 1;
                    seris.MarkerShape = SoftwareFX.ChartFX.MarkerShape.None;
                    break;   
			}
		}
		

		// Percent�� ��� 100�� �����ֱ� ���Ͽ�.
		private double GetCrossValue ( DataTypes type )
		{
			if ( ((int)	type) < 3 )
				return 1;
			else
				return 100.0;
		}

		// ��Ʈ �������� �ʱ�ȭ �ϴ� �κ�..
		public void RPT_1_ChartInit()
		{
			this.ClearData(SoftwareFX.ChartFX.ClearDataFlag.AllData);
			this.Series.Clear();
			this.SerLeg.Clear();
			this.SerCount = 0;
			this.XCount = 0;

			this.AxisY.Interlaced = true;
			this.AxisY.Font = new System.Drawing.Font("����ü", 6, System.Drawing.FontStyle.Regular);
			this.AxisY2.Font = new System.Drawing.Font("����ü", 6, System.Drawing.FontStyle.Regular);
			this.AxisX.Font = new System.Drawing.Font("����ü", 6, System.Drawing.FontStyle.Regular);
			this.Palette = "Default.ChartFX6";

			this.AxisY.Gridlines = true;
			this.AxisY.LabelsFormat.Decimals = 0;
			this.AxisY2.LabelsFormat.Decimals = 0;
			this.AxisY.Grid.Color = System.Drawing.Color.DimGray;
			this.AxisX.LabelAngle = ((short)(0));

			this.Scheme	= SoftwareFX.ChartFX.Scheme.Solid;
			this.CylSides	= (short)(50);
			this.Volume	= (short)(50);	

			this.Highlight.Enabled = true;
			this.Highlight.Dimmed = false;
			this.Highlight.PointAttributes.PointLabelColor = System.Drawing.Color.Black;
			this.Highlight.PointAttributes.Color = System.Drawing.Color.Red;
			this.Highlight.PointAttributes.PointLabels = true;
		}
         
		// ��Ʈ���� ��� ����Ÿ�� ����
		public void RPT_2_ClearData ()
		{
			this.ClearData(SoftwareFX.ChartFX.ClearDataFlag.AllData);
			this.SerCount = 0;
			this.XCount = 0;
		}

		// ��Ʈ�� ����Ÿ ���� �غ�
		public void RPT_3_OpenData ( int SeriseCount, int XasixCount )
		{
			this.OpenData(SoftwareFX.ChartFX.COD.Values, SeriseCount, XasixCount);
			XCount = XasixCount;
		}

		// ��Ʈ ����Ÿ ������ �Ϸ�
		public void RPT_5_CloseData ( )
		{
			this.CloseData(SoftwareFX.ChartFX.COD.Values);
		}

		// ��Ʈ ����Ÿ�� ����
		// ������ ȣ���Ͽ� ����ص� ����
		// �׻� RPT_3_OpenData() �� RPT_5_CloseData() ���̿� �����ؾ߸� ��.
		// SeriseType ���� Row �� Column ���߿� �ø�� ������. 
		public double RPT_4_AddData ( FpSpread spread, int startRowPos, int rowSize, int startColumnPos, int columnSize , SeriseType SerType )
		{
			int [] rows = new int [ rowSize ];
			int [] columns = new int [ columnSize ];
			
			for ( int i = 0; i < rows.Length ; i++ )
				rows[ i ] = i + startRowPos;
			for ( int i = 0; i < columns.Length ; i++ )
				columns[ i ] = i + startColumnPos;

			return RPT_4_AddData( spread, rows, columns , SerType, DataTypes.Initeger );
		}

		public double RPT_4_AddData ( FpSpread spread, int [] Rows, int [] Columns , SeriseType SerType )
		{
			return RPT_4_AddData( spread, Rows, Columns , SerType, DataTypes.Initeger );
		}

		public double RPT_4_AddData ( FpSpread spread, int [] Rows, int [] Columns , SeriseType SerType, DataTypes type )
		{
			double Max = 0;

			try
			{
				// Serise Type ���� Row ������ ���
				if ( SerType == SeriseType.Rows )
				{
					for ( int serise = 0; serise < Rows.Length; serise++, SerCount++ )
					{
						for ( int x = 0; x < Columns.Length; x++ )
						{
                            this.Value[SerCount, x] = Convert.ToDouble(spread.ActiveSheet.Cells[Rows[serise], Columns[x]].Value) * GetCrossValue(type);                         
							Max = Math.Max( Max, this.Value[ SerCount, x ] );
						}
					}
				}
				else
				{
					// Serise Type ���� �÷��� ������ ���
					for ( int serise = 0; serise < Columns.Length; serise++, SerCount++ )
					{
						for ( int x = 0; x < Rows.Length; x++ )
						{
                            this.Value[SerCount, x] = Convert.ToDouble(spread.ActiveSheet.Cells[Rows[x], Columns[serise]].Value) * GetCrossValue(type);                            
							Max = Math.Max( Max, this.Value[ SerCount, x ] );
						}
					}

				}

				return Max;
			}
			catch(Exception ex)
			{
				throw new Exception(" RPT_AddDataUsingColumns : " + ex.Message );
			}
		}


        public double RPT_4_AddData(DataTable dt, int[] Rows, int[] Columns, SeriseType SerType)
        {
            return RPT_4_AddData(dt, Rows, Columns, SerType, DataTypes.Initeger);
        }
		
		public double RPT_4_AddData ( DataTable dt, int [] Rows, int [] Columns , SeriseType SerType, DataTypes type )
		{
			double Max = 0;

			try
			{
				// Serise Type ���� Row ������ ���
				if ( SerType == SeriseType.Rows )
				{
					for ( int serise = 0; serise < Rows.Length; serise++, SerCount++ )
					{
						for ( int x = 0; x < Columns.Length; x++ )
						{
							if ( dt.Rows [ Rows[ serise ] ] [ Columns[ x ] ] != null && dt.Rows [ Rows[ serise ] ] [ Columns[ x ] ] != DBNull.Value )
							{
                                this.Value[x,SerCount] = Convert.ToDouble(dt.Rows[Rows[serise]][Columns[x]]) * GetCrossValue(type);                                
								Max = Math.Max( Max, this.Value[ x,SerCount ] );
							}
						}
					}
				}
				else
				{
					// Serise Type ���� �÷��� ������ ���
					for ( int serise = 0; serise < Columns.Length; serise++, SerCount++ )
					{
						for ( int x = 0; x < Rows.Length; x++ )
						{
							if ( dt.Rows [ Rows[ x ] ] [ Columns[ serise ] ] != null && dt.Rows [ Rows[ x ] ] [ Columns[ serise ] ] != DBNull.Value )
							{
                                this.Value[SerCount, x] = Convert.ToDouble(dt.Rows[Rows[x]][Columns[serise]]) * GetCrossValue(type);                                
								Max = Math.Max( Max, this.Value[ SerCount, x ] );
							}
						}
					}

				}

				return Max;
			}
			catch(Exception ex)
			{
				throw new Exception(" RPT_AddDataUsingColumns : " + ex.Message );
			}
		}

        //2010-03-26-������ : DataTable�� AddData ���� ���� �Լ� �߰�.. �����Լ��� �� ����� �ٲٳİ�..�߰��� ����..
        public double RPT_4_AddData_Original(DataTable dt, int[] Rows, int[] Columns, SeriseType SerType, DataTypes type)
        {
            double Max = 0;

            try
            {
                // Serise Type ���� Row ������ ���
                if (SerType == SeriseType.Rows)
                {
                    for (int serise = 0; serise < Rows.Length; serise++, SerCount++)
                    {
                        for (int x = 0; x < Columns.Length; x++)
                        {
                            if (dt.Rows[Rows[serise]][Columns[x]] != null && dt.Rows[Rows[serise]][Columns[x]] != DBNull.Value)
                            {
                                this.Value[SerCount, x] = Convert.ToDouble(dt.Rows[Rows[serise]][Columns[x]]) * GetCrossValue(type);
                                Max = Math.Max(Max, this.Value[SerCount, x]);
                            }
                        }
                    }
                }
                else
                {
                    // Serise Type ���� �÷��� ������ ���
                    for (int serise = 0; serise < Columns.Length; serise++, SerCount++)
                    {
                        for (int x = 0; x < Rows.Length; x++)
                        {
                            if (dt.Rows[Rows[x]][Columns[serise]] != null && dt.Rows[Rows[x]][Columns[serise]] != DBNull.Value)
                            {
                                this.Value[SerCount, x] = Convert.ToDouble(dt.Rows[Rows[x]][Columns[serise]]) * GetCrossValue(type);
                                Max = Math.Max(Max, this.Value[SerCount, x]);
                            }
                        }
                    }

                }

                return Max;
            }
            catch (Exception ex)
            {
                throw new Exception(" RPT_AddDataUsingColumns : " + ex.Message);
            }
        }

		// ȭ���� ��ƮŸ���� ����
		public void RPT_6_SetGallery ( ChartType gallery, string title, AsixType asixType, DataTypes type, double maxValue )
		{
			// minValue �� 0.6221 �̸� ���þ���.. 
			RPT_6_SetGallery( gallery, title, asixType, type, maxValue, 0.6221 );
		}

		// minValue �� 0.6221 �̸� ���þ���.. 
		public void RPT_6_SetGallery ( ChartType gallery, string title, AsixType asixType, DataTypes type, double maxValue, double minValue )
		{
			for ( int i = 0; i < SerCount; i++ )
				SetChartType ( gallery, this.Series[ i ] );
			
			if ( asixType == AsixType.Y )
			{
				for ( int i = 0; i < SerCount; i++ )
					this.Series[i].YAxis = SoftwareFX.ChartFX.YAxis.Main;

				this.AxisY.Title.Text = title;

				// Percent ������... ^^
                if (((int)type) < 3)
                    this.AxisY.LabelsFormat.Decimals = (int)type;
                else
                    this.AxisY.LabelsFormat.Decimals = (int)type - 3;
                    this.AxisY.Max = maxValue;
				if ( minValue != 0.6221 )
					this.AxisY.Min = minValue;

                //this.AxisY.DataFormat.Decimals = 3;
			}
			else
			{
				for ( int i = 0; i < SerCount; i++ )
					this.Series[i].YAxis = SoftwareFX.ChartFX.YAxis.Secondary;

				this.AxisY2.Title.Text = title;

				// Percent ������... ^^
				if ( ((int)type) < 3 )
					this.AxisY2.LabelsFormat.Decimals = (int)type;
				else
					this.AxisY2.LabelsFormat.Decimals = (int)type - 3;
				this.AxisY2.Max = maxValue;
				if ( minValue != 0.6221 )
					this.AxisY2.Min = minValue;
                //this.AxisY.DataFormat.Decimals = 3;
			}
			
			// Secondary ��ǥ�� ���� ��� �� ȣ������� ��. 
			this.RecalcScale();
		}

		public void RPT_6_SetGallery (ChartType gallery, int startSerisePos, int SeriseSize, string title, AsixType asixType, DataTypes type, double maxValue )
		{
			RPT_6_SetGallery ( gallery, startSerisePos, SeriseSize, title, asixType, type, maxValue, 0.6221 );
		}

		public void RPT_6_SetGallery (ChartType gallery, int startSerisePos, int SeriseSize, string title, AsixType asixType, DataTypes type, double maxValue, double minValue )
		{
			for ( int i = startSerisePos; i < startSerisePos + SeriseSize; i++ )
			{
				SetChartType ( gallery, this.Series[ i ] );

				if ( asixType == AsixType.Y )
					this.Series[i].YAxis = SoftwareFX.ChartFX.YAxis.Main;
				else
					this.Series[i].YAxis = SoftwareFX.ChartFX.YAxis.Secondary;
			}
			
			// Secondary ��ǥ�� ���� ��� �� ȣ������� ��. 
			this.RecalcScale();

			if ( asixType == AsixType.Y )
			{
				this.AxisY.Title.Text = title;
				// Percent ������... ^^
				if ( ((int)type) < 3 )
                    this.AxisY.LabelsFormat.Decimals = (int)type;
				else
					this.AxisY.LabelsFormat.Decimals = (int)type - 3;
				this.AxisY.Max = maxValue;
				if ( minValue != 0.6221 )
					this.AxisY.Min = minValue;

                this.AxisY.DataFormat.Decimals = 3;
                //this.AxisY.DataFormat.Format = type;
			}
			else
			{
				this.AxisY2.Title.Text = title;
				// Percent ������... ^^
				if ( ((int)type) < 3 )
                    this.AxisY2.LabelsFormat.Decimals = (int)type;
				else
					this.AxisY2.LabelsFormat.Decimals = (int)type - 3;
				this.AxisY2.Max = maxValue;
				if ( minValue != 0.6221 )
					this.AxisY.Min = minValue;

                this.AxisY.DataFormat.Decimals = 3;
                //this.AxisY.DataFormat.Format = type;
			}
		}


		// x �� ��ǥ ����
		public void RPT_7_SetXAsixTitle ( string [] Titles )
		{
			if ( XCount < Titles.Length )
				throw new Exception (RptMessages.GetMessage("STD084", GlobalVariable.gcLanguage) + XCount.ToString() );
			
			for ( int i=0 ; i< Titles.Length; i++ )
				this.Legend[ i ] = Titles[ i ];
		}

		public void RPT_7_SetXAsixTitleUsingSpreadHeader ( FpSpread spread, int startHeaderRow, int startHeaderColumn )
		{
			int maxCount = Math.Min( XCount, spread.ActiveSheet.Columns.Count - startHeaderColumn );
			for ( int i=0 ; i< maxCount; i++ )
				this.Legend[ i ] = spread.ActiveSheet.ColumnHeader.Cells [ startHeaderRow, i + startHeaderColumn ].Text;
		}

        public void RPT_7_SetXAsixTitleUsingSpreadHeader(DataTable dt)
        {
            int maxCount = Math.Min(XCount, dt.Rows.Count);
            for (int i = 0; i < maxCount; i++)
                this.Legend[i] = dt.Rows[i]["OPER"].ToString();
        }

		public void RPT_7_SetXAsixTitleUsingSpreadHeader ( FpSpread spread, int startHeaderRow, int [] HeaderColumn )
		{
			if ( XCount < HeaderColumn.Length )
				throw new Exception (RptMessages.GetMessage("STD084", GlobalVariable.gcLanguage) + XCount.ToString() );
			
			for ( int i=0 ; i< HeaderColumn.Length ; i++ )
				this.Legend[ i ] = spread.ActiveSheet.ColumnHeader.Cells [ startHeaderRow, HeaderColumn[ i ] ].Text;
		}

		public void RPT_7_SetXAsixTitleUsingSpread ( FpSpread spread, int [] RowPos, int ColumnPos )
		{
			
			for ( int i=0 ; i< RowPos.Length ; i++ )
				this.Legend[ i ] = spread.ActiveSheet.Cells [ RowPos[ i ] , ColumnPos ].Text;
		}

        // 2010-05-25-������ : DataTable �� Ư�� �÷� ��� �ϱ� ���� �߰���.
        public void RPT_7_SetXAsixTitleUsingDataTable(DataTable dt, int ColumnPos)
        {
            int maxCount = Math.Min(XCount, dt.Rows.Count);
            for (int i = 0; i < maxCount; i++)
                this.Legend[i] = dt.Rows[i][ColumnPos].ToString();
        }

        public void RPT_7_SetXAsixTitleUsingDuration(udcDurationDate duration)
		{
			// �������� ����� �÷� ����
			if ( duration.DaySelector.SelectedValue.ToString().ToUpper() == "DAY" )
			{
				for ( int i = 0 ; i <= duration.SubtractBetweenFromToDate ; i++ )
				{
					string Header = duration.FromDate.Value.AddDays(i).ToString("MM.dd");
					this.Legend[ i ] = Header;
				}
			}
			else if ( duration.DaySelector.SelectedValue.ToString().ToUpper() == "WEEK" )
			{
                int thisWeek = Convert.ToInt32(duration.FromWeek.SelectedValue.ToString().Substring(4, 2));
                int thisMax = duration.FromWeek.Items.Count;
                int maxWeek = Convert.ToInt32(duration.ToWeek.SelectedValue.ToString().Substring(4, 2));
                int idx = 0;
                string Header = null;

                //�⵵�� ���� ���
                if (duration.FromWeek.SelectedValue.ToString().Substring(0, 4) ==
                    duration.ToWeek.SelectedValue.ToString().Substring(0, 4))
                {
                    idx = 0;

                    for (int i = thisWeek; i <= maxWeek; i++)
                    {
                        Header = "WW" + (i).ToString();

                        this.Legend[idx] = Header;
                        idx++;
                    }
                }
                else  //�ٸ� ���
                {
                    idx = 0;

                    for (int i = thisWeek; i <= thisMax; i++)
                    {
                        Header = "WW" + (i).ToString();

                        this.Legend[idx] = Header;
                        idx++;
                    }

                    for (int i = 1; i <= maxWeek; i++)
                    {
                        Header = "WW" + (i).ToString();

                        this.Legend[idx] = Header;
                        idx++;
                    }
                } 
			}
			else // ���� ���
			{
				for ( int i = 0 ; i <= duration.SubtractBetweenFromToDate ; i++ )
				{
					string Header = duration.FromYearMonth.Value.AddMonths(i).ToString("yy.MM");
					this.Legend[ i ] = Header;
				}
			}
		}
		
		// �ø��� Legend�� ����
		public void RPT_8_SetSeriseLegend( string [] SeriseTitle, SoftwareFX.ChartFX.Docked dock )
		{
			if ( SerCount < SeriseTitle.Length )
				throw new Exception (RptMessages.GetMessage("STD085", GlobalVariable.gcLanguage) + SerCount.ToString() );
			
			this.SerLegBox = true;
			this.SerLegBoxObj.Docked = dock;
			
			for ( int i = 0; i < SeriseTitle.Length ; i++ )
				this.SerLeg[i] = SeriseTitle [ i ];
		}
		
		public void RPT_8_SetSeriseLegend(  FpSpread spread, int startRowPos, int RowSize,  int ColumnPos, SoftwareFX.ChartFX.Docked dock)
		{
			if ( SerCount < RowSize )
                throw new Exception (RptMessages.GetMessage("STD085", GlobalVariable.gcLanguage) + SerCount.ToString());
			
			this.SerLegBox = true;
			this.SerLegBoxObj.Docked = dock;
			
			for ( int i = 0; i < RowSize ; i++ )
				this.SerLeg[i] = spread.ActiveSheet.Cells [ i + startRowPos , ColumnPos ].Text;
		}

		public void RPT_8_SetSeriseLegend(  FpSpread spread, int [] Rows,  int Columns, SoftwareFX.ChartFX.Docked dock)
		{
			if ( SerCount < Rows.Length )
                throw new Exception (RptMessages.GetMessage("STD085", GlobalVariable.gcLanguage) + SerCount.ToString());
			
			this.SerLegBox = true;
			this.SerLegBoxObj.Docked = dock;
			
			for ( int i = 0; i < Rows.Length ; i++ )
				this.SerLeg[i] = spread.ActiveSheet.Cells [ Rows [ i ], Columns ].Text;
		}

		public void RPT_8_SetSeriseLegend(  FpSpread spread, int Rows,  int [] Columns, SoftwareFX.ChartFX.Docked dock)
		{
			if ( SerCount < Columns.Length )
                throw new Exception (RptMessages.GetMessage("STD085", GlobalVariable.gcLanguage) + SerCount.ToString());
			
			this.SerLegBox = true;
			this.SerLegBoxObj.Docked = dock;
			
			for ( int i = 0; i < Columns.Length ; i++ )
				this.SerLeg[i] = spread.ActiveSheet.Cells [ Rows, Columns [ i ] ].Text;
		}

		public void RPT_8_SetSeriseLegend(  DataTable dt, int startRowPos, int RowSize,  int ColumnPos, SoftwareFX.ChartFX.Docked dock)
		{
			this.SerLegBox = true;
			this.SerLegBoxObj.Docked = dock;
			
			for ( int i = 0; i < RowSize ; i++ )
				this.SerLeg[i] = (string)dt.Rows[ i + startRowPos ][ ColumnPos ];
		}

		// ��Ʈ �ø�� �����Ҽ� �ִ� üũ�ڽ��� ��������.. 
		public void RPT_9_SetCheckBox ()
		{
			RPT_9_SetCheckBox( 110 );
		}

		public void RPT_9_SetCheckBox (int width)
		{
			RPT_9_SetCheckBox ( width, -1, null );
			
		}

		public void RPT_9_SetCheckBox (int width, int height)
		{
			RPT_9_SetCheckBox ( width, height, null );
		}

		public void RPT_9_SetCheckBox (  int width, int [] CheckedList)
		{
			RPT_9_SetCheckBox ( width, -1, CheckedList);
		}

		public void RPT_9_SetCheckBox ( int width, int height, int [] CheckedList)
		{
			int base_height = height;
            			
			// -1 �� ����
			if ( height == -1 )
				base_height = SerCount * 20 + 15;
			
			CheckBox [] chk_List = new CheckBox [ SerCount ];

			for ( int i = 0; i < chk_List.Length; i++ )
			{
				chk_List[ i ] = new CheckBox ();
				chk_List[ i ].Text = this.SerLeg[ i ];
				chk_List[ i ].Checked = true;
				chk_List[ i ].Size = new System.Drawing.Size( width - 10, 20 );
				chk_List[ i ].Location = new System.Drawing.Point ( 5 , 20 * i + 12  );
				chk_List[ i ].CheckedChanged +=new EventHandler(udcChartFX_CheckedChanged);				
			}

			if ( CheckedList != null && CheckedList.Length > 0 )
			{
				foreach ( CheckBox cbox in chk_List )
					cbox.Checked = false;

				for ( int i = 0; i < CheckedList.Length; i++ )
					chk_List[ CheckedList[i] ].Checked = true;
			}
			
		}

		private void udcChartFX_CheckedChanged(object sender, EventArgs e)
		{
			CheckBox box = ( CheckBox ) sender;
			for ( int i = 0; i < SerCount; i++ )
			{
				if ( box.Text == this.SerLeg[ i ] )
				{
					this.Series[ i ].Visible = box.Checked;
				}
			}
		}		
	}
}
