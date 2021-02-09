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
//       - [2009-09-10] Created by Eunhi Chang
//       - �ϴ� �⺻���� ������ ���� udcChartFX  �� �״�� �����ؼ� ��������� SPC�� X-bar, R Chart �� �����Ѵ�.
//      - �������� query�� �� order by  key�� �Ǿ�� �Ѵ�.
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
    public class udcChartFXSpc : SoftwareFX.ChartFX.Chart  
	{
		/// <summary>
		/// �ʼ� �����̳� �����Դϴ�.
		/// </summary>
		private System.ComponentModel.Container components = null;
		private int SerCount;
		private int XCount;

        private DataTable dtX;       // Xbar��
        private DataTable dtR;       // R��
        
        private double dAvg;
        private double dSt;
        private double dUcl;
        private double dLcl;
        private double dUsl;
        private double dLsl;
        private cSpc spc = new cSpc();   //���� �Լ� ��Ƶ� Class����.

		public udcChartFXSpc(System.ComponentModel.IContainer container)
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

		public udcChartFXSpc()
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
            this.SuspendLayout();
            // 
            // udcChartFXSpc
            // 
            this.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.BorderColor = System.Drawing.Color.Gainsboro;
            this.Font = new System.Drawing.Font("Arial", 8F, System.Drawing.FontStyle.Bold);
            this.Gallery = SoftwareFX.ChartFX.Gallery.Lines;
            this.LineWidth = 2;
            this.MarkerShape = SoftwareFX.ChartFX.MarkerShape.Circle;
            this.MarkerSize = ((short)(5));
            this.Name = "udcChartFXSpc";
            this.NSeries = 1;
            this.NValues = 5;
            this.Scrollable = true;
            this.Size = new System.Drawing.Size(238, 136);
            this.ResumeLayout(false);

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
			this.AxisY2.Font = new System.Drawing.Font("����ü", 6, System.Drawing.FontStyle.Bold);
			this.AxisX.Font = new System.Drawing.Font("����ü", 6, System.Drawing.FontStyle.Regular);
			this.Palette = "Default.ChartFX6";

			this.AxisY.Gridlines = false;
			this.AxisY.LabelsFormat.Decimals = 2;
			this.AxisY2.LabelsFormat.Decimals = 2;
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
            this.SerCount =0;
            this.XCount = 0;

		}

		// ��Ʈ�� ����Ÿ ���� �غ�
		public void RPT_3_OpenData ( int SeriseCount, int XasixCount )
		{
			this.OpenData(SoftwareFX.ChartFX.COD.Values, SeriseCount, XasixCount);
			XCount = XasixCount;
            //this.AxisY2.Visible = false;

		
        }

        /// <summary>
        ///  DataTable(dt_s)�� �޾ƿͼ� Chart�� �׸��� Title(sTitle) �����ش�.
        /// </summary>
        /// <param name="dt_s"> Chart �� �׷��� DataTable Name</param>
        /// <param name="sTitle">Chart Title</param>
        /// Created by Eunhi Chang [2009-09-10] :
        public void RPT_3_1_SetData(DataTable dt_s, string sTitle)
        {
            DataTable dtNew = new DataTable();   // ���ο� DataTable dt�� ����
            dtNew = dt_s;                                       // dt���ٰ� �޾ƿ� a_dt �� �־��ش�.

            this.DataSource = dtNew;     
            this.AxisX.Title.Text = sTitle;
        }

        /// <summary> 
        ///   Xbar Chart ���� �ñ׸� �� ������ ��Ÿ���� Y2�� ����
        /// </summary>
        /// <param name="dLcl">LCL</param>
        /// <param name="dCl">��հ�,Chart���� CL������ ǥ��</param>
        /// <param name="dUcl">UCL</param>
        /// <param name="dUsl">Y2 �ִ밪</param>
        /// <param name="dLsl">Y2 �ּҰ�</param>
        /// Created by Eunhi Chang [2009-09-10] 

        // Xbar Chart ���� �ñ׸� �� ������ ��Ÿ���� �޾ƿ� ������ Y2�� ���� 
        //  -. dLcl(LCL), dCl(���, CL), dUcl(UCL), dUsl(Y2�� �ִ밪), dLsl(Y2�� �ּҰ�)
        public void RPT_3_2_SetColor(double dLcl, double dCl, double dUcl, double dUsl, double dLsl)
        {
            // Y2 ��ǥ�� �� 8����ؼ� ��Ÿ����(���߿� ���� ĥ�ϰ� ���ֱ� ���ϰ�)
            this.AxisY2.Min = 0;
            this.AxisY2.Max = 8;
            this.AxisY2.Step = 1;

            //////// ����ĥ�ϱ�
            this.Series[0].Color = System.Drawing.Color.Black;

            SoftwareFX.ChartFX.AxisSection section1 = this.AxisY2.Sections[0];
            section1.From = 1;
            section1.To = 7;
            section1.BackColor = System.Drawing.Color.Red;

            SoftwareFX.ChartFX.AxisSection section2 = this.AxisY2.Sections[1];
            section2.From = 2;
            section2.To = 6;
            section2.BackColor = System.Drawing.Color.Yellow;

            SoftwareFX.ChartFX.AxisSection section3 = this.AxisY2.Sections[2];
            section3.From = 3;
            section3.To = 5;
            section3.BackColor = System.Drawing.Color.Chartreuse;

            // LCL, CL, UCL ���� ������ ����� �Բ� �ϱ�� ���� ǥ���Ѵ�.
            this.AxisY2.Label[1] = "LCL=" + "\n" + dLcl;
            this.AxisY2.Label[4] = "CL=" + "\n" + dCl;
            this.AxisY2.Label[7] = "UCL=" + "\n" + dUcl;
            
            //////CL��� ���߱� : ��հ��� �����ֱ� ���ؼ�,,,�ٵ� 0�϶�,,,,,,,,�� ���� ���� ���̴�,,,,,,,,������ �߰�<<<�ذ���//////
            SoftwareFX.ChartFX.ConstantLine constantLine = this.ConstantLines[0];
            constantLine.Value = 4;           
            constantLine.Color = System.Drawing.Color.DimGray;
            constantLine.Axis = SoftwareFX.ChartFX.AxisItem.Y2;

            // Y���� min�� max���� �����ְ� ������ �ʿ� ���� ������ visible=false ����
            this.AxisY.Max = dUsl;
            this.AxisY.Min = dLsl;
            this.AxisY.Visible = false;
            

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
                throw new Exception (RptMessages.GetMessage("STD084", GlobalVariable.gcLanguage) + XCount.ToString());
			
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
                throw new Exception (RptMessages.GetMessage("STD084", GlobalVariable.gcLanguage) + XCount.ToString());
			
			for ( int i=0 ; i< HeaderColumn.Length ; i++ )
				this.Legend[ i ] = spread.ActiveSheet.ColumnHeader.Cells [ startHeaderRow, HeaderColumn[ i ] ].Text;
		}

		public void RPT_7_SetXAsixTitleUsingSpread ( FpSpread spread, int [] RowPos, int ColumnPos )
		{
			
			for ( int i=0 ; i< RowPos.Length ; i++ )
				this.Legend[ i ] = spread.ActiveSheet.Cells [ RowPos[ i ] , ColumnPos ].Text;
		}

		public void RPT_7_SetXAsixTitleUsingDuration ( udcDurationDate duration )
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
                throw new Exception (RptMessages.GetMessage("STD085", GlobalVariable.gcLanguage) + SerCount.ToString());
			
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
				chk_List[ i ].CheckedChanged +=new EventHandler(udcChartFXSpc_CheckedChanged);				
			}

			if ( CheckedList != null && CheckedList.Length > 0 )
			{
				foreach ( CheckBox cbox in chk_List )
					cbox.Checked = false;

				for ( int i = 0; i < CheckedList.Length; i++ )
					chk_List[ CheckedList[i] ].Checked = true;
			}
			
		}

		private void udcChartFXSpc_CheckedChanged(object sender, EventArgs e)
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


        #region " �ش� ȭ�鿡�� DataTable �����ͼ� �ʿ��� ������ ���� ���Ѵ�."
      
        ///  <summary>
        ///    RPT_10_Xbar_GetData :  DataTable Name�� VALUE_1 �÷� INDEX ���� �޾Ƽ� ���, ǥ������, UCL, LCL ���Ѵ�.(Xbar��)
        /// </summary>
        /// <param name="a_dt">DataTabe.</param>
        /// <param name="iValue1">value_1�� �÷��� ��ȯ�Ǵ� Index(��ġ).</param>

        // DataTable Name�� VALUE_1 �÷� INDEX ���� �޾Ƽ� ���, ǥ������, UCL, LCL ���Ѵ�.(Xbar��)
        public void RPT_10_Xbar_GetData(DataTable a_dt, int iValue1)
        {
            int iValue25 = iValue1 + 25 ;  // Value_25���� �ֱ� ������,,            

            DataTable dt = new DataTable();   // ���ο� DataTable dt�� ����
            dt = a_dt;                                       // dt���ٰ� �޾ƿ� a_dt �� �־��ش�.
            dt.Columns.Add("AVG", typeof(String));         // �� ROW���� VALUE_1~25���� �÷����� ����� �־��ٰ��̴�.

            //1. ��� ���ϱ�(Xbar)            
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                spc.clear();

                for (int j = iValue1; j < iValue25; j++)
                {
                    if (dt.Rows[i][j].Equals(" ") || dt.Rows[i][j].Equals(""))        // ���� ������ �� �ڷε� ���� ������ 
                    {
                        break;
                    }
                    else
                    {
                        double x = Convert.ToDouble(dt.Rows[i][j]);     // x�� �÷����� �����Ѵ�����
                        spc.Add(x);           //spc.Add�� x���� �־��ָ� spc.clear() ȣ������ �ʴ� �̻� ��� �����Ǿ� ��������(sum), �ڱ�׵鳢�� ���ؼ� min,max�� ���س��� �ִ�,.
                    }                              
                }             
                dt.Rows[i]["AVG"] = spc.mean();    // �� row�� (value1~25������) ����� �־��ش�.          
            }

            // 2. ǥ������ ���ϱ� : ǥ�������� ��� ���鿡(dt�� �ִ�)   ���� ǥ�������� ���Ѵ�,(������k�� ������ ���)
            spc.clear();
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                for (int j = iValue1; j < iValue25; j++)
                {
                    if (dt.Rows[i][j].Equals(" ") || dt.Rows[i][j].Equals(""))
                    {
                        break;
                    }
                    else
                    {
                        double x = Convert.ToDouble(dt.Rows[i][j]);
                        spc.Add(x);
                    }
                 }
            }
            dSt = spc.stdev();   // ǥ������ ���ؼ�(spc.stdev()) dSt �� �־��ش�.


            //3. ���ο� DataTabe(dtX)�� �������ش�.
            dtX = new DataTable();

            if (dtX.Columns.Count == 0) // X-bar��
            {
                // ���ο� Datatable�� ���� column�� �����ϰ�            
                DataColumn dc = new DataColumn();
                dc.DataType = System.Type.GetType("System.String");
                dc.ColumnName = "LOT_ID";
                dc.Caption = "LOT_ID";
                dc.DefaultValue = "";
                dtX.Columns.Add(dc);

                DataColumn dd = new DataColumn();
                dd.DataType = System.Type.GetType("System.Double");
                dd.ColumnName = "AVG";
                dd.Caption = "AVG";
                dd.DefaultValue = 0.00;
                dtX.Columns.Add(dd);
            }


            // 4. 1������ ���� ��Row���� ����� ��¥���� Group By �ؼ� ���� �� ����� ���ؼ� DATE, AVG �÷��� �̾Ƽ�
            //     ���ο� DataTable(dtX)�� �����ؼ� �־��ش�.

            // TranTime ������ Lot ID �������� ���� - Xbar Chart ���� ����..������K��û(2009.09.28 ������)
            spc.clear();    // ���а����ؼ� ��� �ʱ�ȭ�ϰ�
            string sLot = " ";
            int k = 0;

            for (int i = 0; i < dt.Rows.Count; i++)
            {
                if (!sLot.Equals(dt.Rows[i][0]))
                {
                    if (i == 0) // ���� �� ù���̶��
                    {
                        //dtX.Rows.Add();           // �ƹ��ϵ� ���Ѵ�.
                    }
                    else if (i != 0)
                    {
                        dtX.Rows.Add();                         // row���� �߰��ϰ�
                        dtX.Rows[k]["LOT_ID"] = sLot;        //  �ٷ� ���� ������  ���ο� DataTable dtX�� �־��ش�. Lot ID�־��ְ�
                        dtX.Rows[k]["AVG"] = spc.mean();         // ��ճ־��ְ�
                        spc.clear();                           // ���а����ؼ� �ʱ�ȭ���ְ�: �ֳ��ϸ� ���� �� ������ ���� �޾ƾ� �ϴϱ�
                        k++;
                    }
                    sLot = Convert.ToString(dt.Rows[i][0]);                 // sLot �� �ش� �÷���(LOT ID) �����ϰ�
                    double x = Convert.ToDouble(dt.Rows[i]["AVG"]);
                    spc.Add(x);
                }
                else
                {
                    if (i == dt.Rows.Count - 1)
                    {
                        dtX.Rows.Add();
                        dtX.Rows[k]["LOT_ID"] = sLot;
                        dtX.Rows[k]["AVG"] = spc.mean();
                    }
                    else
                    {
                        double x = Convert.ToDouble(dt.Rows[i]["AVG"]);
                        spc.Add(x);
                    }
                }
            }

            //spc.clear();    // ���а����ؼ� ��� �ʱ�ȭ�ϰ�
            //string sTime = " ";
            //int k = 0;

            //for (int i = 0; i < dt.Rows.Count; i++)
            //{
            //    if (!sTime.Equals(dt.Rows[i][1]))
            //    {
            //        if (i == 0) // ���� �� ù���̶��
            //        {
            //            //dtX.Rows.Add();           // �ƹ��ϵ� ���Ѵ�.
            //        }
            //        else if (i != 0)
            //        {
            //            dtX.Rows.Add();                         // row���� �߰��ϰ�
            //            dtX.Rows[k]["DATE"] = sTime;        //  �ٷ� ���� ������  ���ο� DataTable dtX�� �־��ش�. �ð��־��ְ�
            //            dtX.Rows[k]["AVG"] = spc.mean();         // ��ճ־��ְ�
            //            spc.clear();                           // ���а����ؼ� �ʱ�ȭ���ְ�: �ֳ��ϸ� ���� �� ������ ���� �޾ƾ� �ϴϱ�
            //            k++;
            //        }
            //        sTime = Convert.ToString(dt.Rows[i][1]);                 // sTime �� �ش� �÷���(TRAN_TIME) �����ϰ�
            //        double x = Convert.ToDouble(dt.Rows[i]["AVG"]);
            //        spc.Add(x);                                                                
            //    }
            //    else
            //    {
            //        if (i == dt.Rows.Count - 1)
            //        {
            //            dtX.Rows.Add();
            //            dtX.Rows[k]["DATE"] = sTime;
            //            dtX.Rows[k]["AVG"] = spc.mean();
            //        }
            //        else
            //        {
            //            double x = Convert.ToDouble(dt.Rows[i]["AVG"]);
            //            spc.Add(x);
            //        }
            //    }
            //}

            // Chart�� �ѷ��ֱ� ���ؼ� ��� AVG������ ����� ���ϰ� 2������ ���� ǥ�������� �̿��ؼ� Chart�� �Ѱ��� UCL, LCL, USL, LSL�� ���Ѵ�.

            if (dtX.Rows.Count != 0)
            {
                spc.clear();
                dAvg = 0.0;

                for (int i = 0; i < dtX.Rows.Count; i++)
                {
                    double x = Convert.ToDouble(dtX.Rows[i]["AVG"]);
                    spc.Add(x);
                }
                dAvg = Math.Round(spc.mean(), 2);

                dUcl = Math.Round(dAvg + (3 * dSt),2);
                dLcl = Math.Round(dAvg - (3 * dSt),2);
                dUsl = Math.Round(dAvg + (4 * dSt),2);
                dLsl = Math.Round(dAvg - (4 * dSt), 2);

                // Chart �׸���
                RPT_3_1_SetData(dtX, "X-bar Chart");

                RPT_3_2_SetColor(dLcl, dAvg, dUcl, dUsl, dLsl);    // ���� ĥ���ְ�
            }
        }

        ///  <summary>
        ///    RPT_10_R_GetData:  DataTable Name�� VALUE_1 �÷� INDEX ���� �޾Ƽ� R(Max-Min) ���Ѵ�(R Chart��)
        /// </summary>
        /// <param name="a_dt">DataTabe.</param>
        /// <param name="iValue1">value_1�� �÷��� ��ȯ�Ǵ� Index(��ġ).</param>
        public void RPT_10_R_GetData(DataTable a_dt, int iValue1)
        {
            int iValue25 = iValue1 + 25 ;

            DataTable dt = new DataTable();   // ���ο� DataTable dt�� ����
            dt = a_dt;                                       // dt���ٰ� �޾ƿ� a_dt �� �־��ش�.
            dt.Columns.Add("MIN", typeof(String));              
            dt.Columns.Add("MAX",typeof(String));


            //1. �� Row ���� MIN, MAX �� ���ϱ�    
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                spc.clear();

                for (int j = iValue1; j < iValue25; j++)
                {
                    if (dt.Rows[i][j].Equals(" ") || dt.Rows[i][j].Equals(""))
                    {
                        break;
                    }
                    else
                    {
                        double x = Convert.ToDouble(dt.Rows[i][j]);
                        spc.Add(x);
                    }
                              
                }
                dt.Rows[i]["MIN"] = spc.min();
                dt.Rows[i]["MAX"] = spc.max();    
            }


            //2. ���ο� DataTabe(dtR)�� �������ش�.
            dtR = new DataTable();

            if (dtR.Columns.Count == 0) // R-bar��
            {
                // ���ο� Datatable�� ���� column�� �����ϰ�            
                DataColumn dc = new DataColumn();
                dc.DataType = System.Type.GetType("System.String");
                dc.ColumnName = "LOT_ID";
                dc.Caption = "LOT_ID";
                dc.DefaultValue = "";
                dtR.Columns.Add(dc);

                DataColumn dd = new DataColumn();
                dd.DataType = System.Type.GetType("System.Double");
                dd.ColumnName = "R";
                dd.Caption = "R";
                dd.DefaultValue = 0.00;
                dtR.Columns.Add(dd);
            }

            // 3. 1������ ���� ��Row���� MIN, MAX �� ������ LOT_ID�� Group By �ؼ� ���� �� ����� ���ؼ� LOT_ID, R��(MAX-MIN) ��
            //     ���ο� DataTable(dtR)�� �����ؼ� �־��ش�.

            spc.clear();    // ���а����ؼ� ��� �ʱ�ȭ�ϰ�
            string sLot = " ";
             int k = 0;

            for (int i = 0; i < dt.Rows.Count; i++)
            {

                if (!sLot.Equals(dt.Rows[i][0]))
                {
                    if (i==0) // ���� �� ù���̶��
                    {
                      //  dtR.Rows.Add();
                      //   �ƹ��ϵ� ���Ѵ�.
                    }
                    else if (i != 0)
                    {
                        dtR.Rows.Add();
                        dtR.Rows[k]["LOT_ID"] = sLot;       //   �ٷ� ���� ������  ���ο� DataTable dtR�� �־��ش�. LOT_ID�־��֤Ǥ�
                        dtR.Rows[k]["R"] = spc.gap();       //   max-min���־��ְ�
                        spc.clear();                          //  ���а����ؼ� �ʱ�ȭ���ְ�: �ֳ��ϸ� ���� �� ������ ���� �޾ƾ� �ϴϱ�
                        k++;
                    }
                    sLot = Convert.ToString(dt.Rows[i][0]);               //   sLot �� �ش� �÷���(LOT_ID) �����ϰ�



                    for (int s = dt.Columns.IndexOf("MIN")-1; s < dt.Columns.IndexOf("MIN")+1 ; s++)
                    {
                        double x = Convert.ToDouble(dt.Rows[i][s]);
                        spc.Add(x);                                                         //        x������ ������� sum......
                    }
                }
                else
                {
                    if (i == dt.Rows.Count - 1)
                    {
                        dtR.Rows.Add();
                        dtR.Rows[k]["LOT_ID"] = sLot;
                        dtR.Rows[k]["R"] = spc.gap();       
                    }
                    else
                    {
                        for (int s = dt.Columns.IndexOf("MIN") - 1; s < dt.Columns.IndexOf("MIN") + 1; s++)
                        {
                            double x = Convert.ToDouble(dt.Rows[i][s]);
                            spc.Add(x);                                                           //      x������ ������� sum......
                        }
                    }
                }

            }

            RPT_3_1_SetData(dtR, "R Chart");
            this.AxisY.Min = 0;              // R Chart�� ������ Y�� �ּҰ��� 0
            this.AxisY.Gridlines=true;
            }
      


        #endregion


        #region " SPC�� ���а��� "
        /// <summary>
        /// Created by Eunhi Chang [2009-09-10]
        ///    - SPC Chart�� �����ϱ� ���ؼ� �ʿ��� ���а��ĵ� ��Ƶд�.
        /// </summary>
       
        // SPC CHART�� �����ϱ� ���ؼ� �ʿ��� �����Լ� 
        //  - SUM, MIN, MAX, STDEV(ǥ������), GAP(MAX-MIN), MEAN(���), N(COUNT)
        public  class cSpc   
        {
            private double sum, sum2, cmax, cmin;
            private int n;

            public cSpc()
            {
                clear();      
            }

            public void clear()   // �ʱ�ȭ
            {
                n = 0;
                cmin = 0;
                cmax = 0;
                sum = 0.00;
                sum2 = 0.00;
            }

            public void Add(double x)        // ���� ������ �ʱ�ȭ �������� sum�ϰ� min����max������ ���ϸ鼭 ���Ѵ�.
            {
                n++;
                sum += x;
                sum2 += x * x;

                if (cmax < x)
                {
                    cmax = x;
                }
                if (cmin == 0)
                {
                    cmin = x;
                }
                else if (cmin > x)
                {
                    cmin = x;
                }
                return;
            }

            public Double min()     // ���� ����  �߿� min�� return
            {
                return cmin;
            }

            public Double max()        // ���� ����  �߿� max�� r
            {
                return cmax;
            }

            // ������ �������� ����� ����
            public double mean()
            {
                return sum / (double)n;
            }

            // ������ �������� ����� ǥ������ ����

            public double stdevp()
            {
                return Math.Sqrt(((double)n * sum2 - sum * sum) / ((double)n * (double)n));
            }

            // �����ܿ��� ����� ���� �������� ����� ǥ������ ����
            public double stdev()
            {
                return Math.Sqrt(((double)n * sum2 - sum * sum) / ((double)n * (double)(n - 1)));
            }

            public double gap()        // R-Chart�� �����ϱ� ���� R(max�� - min��) �� �����ش�.
            {
                return (cmax - cmin);
            }
        }
        #endregion
	}
}
