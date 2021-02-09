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
//       - 일단 기본적인 조건은 기존 udcChartFX  를 그대로 복사해서 사용했으며 SPC용 X-bar, R Chart 를 구현한다.
//      - 가져오는 query는 꼭 order by  key로 되어야 한다.
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
		/// 필수 디자이너 변수입니다.
		/// </summary>
		private System.ComponentModel.Container components = null;
		private int SerCount;
		private int XCount;

        private DataTable dtX;       // Xbar용
        private DataTable dtR;       // R용
        
        private double dAvg;
        private double dSt;
        private double dUcl;
        private double dLcl;
        private double dUsl;
        private double dLsl;
        private cSpc spc = new cSpc();   //수학 함수 모아둔 Class선언.

		public udcChartFXSpc(System.ComponentModel.IContainer container)
		{
			///
			/// Windows.Forms 클래스 컴퍼지션 디자이너 지원에 필요합니다.
			///
			container.Add(this);
			InitializeComponent();
			//
			// TODO: InitializeComponent를 호출한 다음 생성자 코드를 추가합니다.
			//
		}

		public udcChartFXSpc()
		{
			///
			/// Windows.Forms 클래스 컴퍼지션 디자이너 지원에 필요합니다.
			///
			InitializeComponent();
			//
			// TODO: InitializeComponent를 호출한 다음 생성자 코드를 추가합니다.
			//
		}

		/// <summary> 
		/// 사용 중인 모든 리소스를 정리합니다.
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


		#region 구성 요소 디자이너에서 생성한 코드
		/// <summary>
		/// 디자이너 지원에 필요한 메서드입니다.
		/// 이 메서드의 내용을 코드 편집기로 수정하지 마십시오.
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
		

		// Percent인 경우 100을 곱해주기 위하여.
		private double GetCrossValue ( DataTypes type )
		{
			if ( ((int)	type) < 3 )
				return 1;
			else
				return 100.0;
		}

		// 차트 디자인을 초기화 하는 부분..
		public void RPT_1_ChartInit()
		{
			this.ClearData(SoftwareFX.ChartFX.ClearDataFlag.AllData);
			this.Series.Clear();
			this.SerLeg.Clear();
			this.SerCount = 0;
			this.XCount = 0;            

			this.AxisY.Interlaced = true;
			this.AxisY.Font = new System.Drawing.Font("굴림체", 6, System.Drawing.FontStyle.Regular);
			this.AxisY2.Font = new System.Drawing.Font("굴림체", 6, System.Drawing.FontStyle.Bold);
			this.AxisX.Font = new System.Drawing.Font("굴림체", 6, System.Drawing.FontStyle.Regular);
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
         
		// 차트내에 모든 데이타를 제거
		public void RPT_2_ClearData ()
		{
			this.ClearData(SoftwareFX.ChartFX.ClearDataFlag.AllData);
            this.SerCount =0;
            this.XCount = 0;

		}

		// 차트내 데이타 셋팅 준비
		public void RPT_3_OpenData ( int SeriseCount, int XasixCount )
		{
			this.OpenData(SoftwareFX.ChartFX.COD.Values, SeriseCount, XasixCount);
			XCount = XasixCount;
            //this.AxisY2.Visible = false;

		
        }

        /// <summary>
        ///  DataTable(dt_s)을 받아와서 Chart에 그리고 Title(sTitle) 보여준다.
        /// </summary>
        /// <param name="dt_s"> Chart 에 그려줄 DataTable Name</param>
        /// <param name="sTitle">Chart Title</param>
        /// Created by Eunhi Chang [2009-09-10] :
        public void RPT_3_1_SetData(DataTable dt_s, string sTitle)
        {
            DataTable dtNew = new DataTable();   // 새로운 DataTable dt를 만들어서
            dtNew = dt_s;                                       // dt에다가 받아온 a_dt 를 넣어준다.

            this.DataSource = dtNew;     
            this.AxisX.Title.Text = sTitle;
        }

        /// <summary> 
        ///   Xbar Chart 에서 시그마 별 색깔을 나타내고 Y2값 지정
        /// </summary>
        /// <param name="dLcl">LCL</param>
        /// <param name="dCl">평균값,Chart에서 CL값으로 표기</param>
        /// <param name="dUcl">UCL</param>
        /// <param name="dUsl">Y2 최대값</param>
        /// <param name="dLsl">Y2 최소값</param>
        /// Created by Eunhi Chang [2009-09-10] 

        // Xbar Chart 에서 시그마 별 색깔을 나타내고 받아온 값들을 Y2값 지정 
        //  -. dLcl(LCL), dCl(평균, CL), dUcl(UCL), dUsl(Y2축 최대값), dLsl(Y2축 최소값)
        public void RPT_3_2_SetColor(double dLcl, double dCl, double dUcl, double dUsl, double dLsl)
        {
            // Y2 좌표를 총 8등분해서 나타낸다(나중에 색깔 칠하고 값넣기 편하게)
            this.AxisY2.Min = 0;
            this.AxisY2.Max = 8;
            this.AxisY2.Step = 1;

            //////// 색깔칠하기
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

            // LCL, CL, UCL 값에 가져온 값들과 함께 하기와 같이 표기한다.
            this.AxisY2.Label[1] = "LCL=" + "\n" + dLcl;
            this.AxisY2.Label[4] = "CL=" + "\n" + dCl;
            this.AxisY2.Label[7] = "UCL=" + "\n" + dUcl;
            
            //////CL가운데 선긋기 : 평균값을 보여주기 위해서,,,근데 0일때,,,,,,,,도 같이 줄이 보이니,,,,,,,,문제점 발견<<<해결요망//////
            SoftwareFX.ChartFX.ConstantLine constantLine = this.ConstantLines[0];
            constantLine.Value = 4;           
            constantLine.Color = System.Drawing.Color.DimGray;
            constantLine.Axis = SoftwareFX.ChartFX.AxisItem.Y2;

            // Y축의 min과 max값도 정해주고 보여줄 필요 없기 때문에 visible=false 했음
            this.AxisY.Max = dUsl;
            this.AxisY.Min = dLsl;
            this.AxisY.Visible = false;
            

        }

		// 차트 데이타 셋팅을 완료
		public void RPT_5_CloseData ( )
		{
			this.CloseData(SoftwareFX.ChartFX.COD.Values);
		}

		// 차트 데이타를 셋팅
		// 여러번 호출하여 사용해도 가능
		// 항상 RPT_3_OpenData() 와 RPT_5_CloseData() 사이에 존재해야만 함.
		// SeriseType 따라서 Row 와 Column 둘중에 시리즈가 결정됨. 
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
				// Serise Type 으로 Row 선택한 경우
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
					// Serise Type 으로 컬럼을 선택한 경우
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
				// Serise Type 으로 Row 선택한 경우
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
					// Serise Type 으로 컬럼을 선택한 경우
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
 
		// 화면의 차트타입을 결정
		public void RPT_6_SetGallery ( ChartType gallery, string title, AsixType asixType, DataTypes type, double maxValue )
		{
			// minValue 가 0.6221 이면 셋팅안함.. 
			RPT_6_SetGallery( gallery, title, asixType, type, maxValue, 0.6221 );
		}

		// minValue 가 0.6221 이면 셋팅안함.. 
		public void RPT_6_SetGallery ( ChartType gallery, string title, AsixType asixType, DataTypes type, double maxValue, double minValue )
		{
			for ( int i = 0; i < SerCount; i++ )
				SetChartType ( gallery, this.Series[ i ] );
			
			if ( asixType == AsixType.Y )
			{
				for ( int i = 0; i < SerCount; i++ )
					this.Series[i].YAxis = SoftwareFX.ChartFX.YAxis.Main;

				this.AxisY.Title.Text = title;

				// Percent 때문에... ^^
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

				// Percent 때문에... ^^
				if ( ((int)type) < 3 )
					this.AxisY2.LabelsFormat.Decimals = (int)type;
				else
					this.AxisY2.LabelsFormat.Decimals = (int)type - 3;
				this.AxisY2.Max = maxValue;
				if ( minValue != 0.6221 )
					this.AxisY2.Min = minValue;
                //this.AxisY.DataFormat.Decimals = 3;
			}
			
			// Secondary 좌표를 쓰는 경우 꼭 호출해줘야 함. 
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
			
			// Secondary 좌표를 쓰는 경우 꼭 호출해줘야 함. 
			this.RecalcScale();

			if ( asixType == AsixType.Y )
			{
				this.AxisY.Title.Text = title;
				// Percent 때문에... ^^
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
				// Percent 때문에... ^^
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


		// x 축 좌표 셋팅
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
			// 동적으로 생기는 컬럼 생성
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

                //년도가 같을 경우
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
                else  //다를 경우
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
			else // 달인 경우
			{
				for ( int i = 0 ; i <= duration.SubtractBetweenFromToDate ; i++ )
				{
					string Header = duration.FromYearMonth.Value.AddMonths(i).ToString("yy.MM");
					this.Legend[ i ] = Header;
				}
			}
		}
		
		// 시리즈 Legend를 셋팅
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

		// 차트 시리즈를 제어할수 있는 체크박스를 생성해줌.. 
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
            			
			// -1 는 더미
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


        #region " 해당 화면에서 DataTable 가져와서 필요한 값들을 각각 구한다."
      
        ///  <summary>
        ///    RPT_10_Xbar_GetData :  DataTable Name과 VALUE_1 컬럼 INDEX 값을 받아서 평균, 표준편차, UCL, LCL 구한다.(Xbar용)
        /// </summary>
        /// <param name="a_dt">DataTabe.</param>
        /// <param name="iValue1">value_1의 컬럼의 반환되는 Index(위치).</param>

        // DataTable Name과 VALUE_1 컬럼 INDEX 값을 받아서 평균, 표준편차, UCL, LCL 구한다.(Xbar용)
        public void RPT_10_Xbar_GetData(DataTable a_dt, int iValue1)
        {
            int iValue25 = iValue1 + 25 ;  // Value_25까지 있기 때문에,,            

            DataTable dt = new DataTable();   // 새로운 DataTable dt를 만들어서
            dt = a_dt;                                       // dt에다가 받아온 a_dt 를 넣어준다.
            dt.Columns.Add("AVG", typeof(String));         // 각 ROW별로 VALUE_1~25까지 컬럼값의 평균을 넣어줄것이다.

            //1. 평균 구하기(Xbar)            
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                spc.clear();

                for (int j = iValue1; j < iValue25; j++)
                {
                    if (dt.Rows[i][j].Equals(" ") || dt.Rows[i][j].Equals(""))        // 값이 없으면 그 뒤로도 없기 때문에 
                    {
                        break;
                    }
                    else
                    {
                        double x = Convert.ToDouble(dt.Rows[i][j]);     // x에 컬럼값들 대입한다음에
                        spc.Add(x);           //spc.Add에 x값을 넣어주면 spc.clear() 호출하지 않는 이상 계속 축적되어 더해지고(sum), 자기네들끼리 비교해서 min,max값 구해놓고 있다,.
                    }                              
                }             
                dt.Rows[i]["AVG"] = spc.mean();    // 각 row별 (value1~25까지의) 평균을 넣어준다.          
            }

            // 2. 표준편차 구하기 : 표준편차는 모든 값들에(dt에 있는)   대한 표준편차를 구한다,(여동욱k께 문의한 결과)
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
            dSt = spc.stdev();   // 표준편차 구해서(spc.stdev()) dSt 에 넣어준다.


            //3. 새로운 DataTabe(dtX)를 생성해준다.
            dtX = new DataTable();

            if (dtX.Columns.Count == 0) // X-bar용
            {
                // 새로운 Datatable에 넣을 column도 정의하고            
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


            // 4. 1번에서 구한 각Row들의 평균을 날짜별로 Group By 해서 각각 총 평균을 구해서 DATE, AVG 컬럼만 뽑아서
            //     새로운 DataTable(dtX)를 생성해서 넣어준다.

            // TranTime 기준을 Lot ID 기준으로 변경 - Xbar Chart 기준 변경..여동욱K요청(2009.09.28 임종우)
            spc.clear();    // 수학관련해서 모두 초기화하고
            string sLot = " ";
            int k = 0;

            for (int i = 0; i < dt.Rows.Count; i++)
            {
                if (!sLot.Equals(dt.Rows[i][0]))
                {
                    if (i == 0) // 만약 맨 첫줄이라면
                    {
                        //dtX.Rows.Add();           // 아무일도 안한다.
                    }
                    else if (i != 0)
                    {
                        dtX.Rows.Add();                         // row한줄 추가하고
                        dtX.Rows[k]["LOT_ID"] = sLot;        //  바로 전의 값들을  새로운 DataTable dtX에 넣어준다. Lot ID넣어주고
                        dtX.Rows[k]["AVG"] = spc.mean();         // 평균넣어주고
                        spc.clear();                           // 수학관련해서 초기화해주고: 왜냐하면 다음 번 값들을 새로 받아야 하니깐
                        k++;
                    }
                    sLot = Convert.ToString(dt.Rows[i][0]);                 // sLot 에 해당 컬럼값(LOT ID) 대입하고
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

            //spc.clear();    // 수학관련해서 모두 초기화하고
            //string sTime = " ";
            //int k = 0;

            //for (int i = 0; i < dt.Rows.Count; i++)
            //{
            //    if (!sTime.Equals(dt.Rows[i][1]))
            //    {
            //        if (i == 0) // 만약 맨 첫줄이라면
            //        {
            //            //dtX.Rows.Add();           // 아무일도 안한다.
            //        }
            //        else if (i != 0)
            //        {
            //            dtX.Rows.Add();                         // row한줄 추가하고
            //            dtX.Rows[k]["DATE"] = sTime;        //  바로 전의 값들을  새로운 DataTable dtX에 넣어준다. 시간넣어주고
            //            dtX.Rows[k]["AVG"] = spc.mean();         // 평균넣어주고
            //            spc.clear();                           // 수학관련해서 초기화해주고: 왜냐하면 다음 번 값들을 새로 받아야 하니깐
            //            k++;
            //        }
            //        sTime = Convert.ToString(dt.Rows[i][1]);                 // sTime 에 해당 컬럼값(TRAN_TIME) 대입하고
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

            // Chart에 뿌려주기 위해서 모든 AVG값들의 평균을 구하고 2번에서 구한 표준편차를 이용해서 Chart에 넘겨줄 UCL, LCL, USL, LSL을 구한다.

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

                // Chart 그리고
                RPT_3_1_SetData(dtX, "X-bar Chart");

                RPT_3_2_SetColor(dLcl, dAvg, dUcl, dUsl, dLsl);    // 색깔 칠해주고
            }
        }

        ///  <summary>
        ///    RPT_10_R_GetData:  DataTable Name과 VALUE_1 컬럼 INDEX 값을 받아서 R(Max-Min) 구한다(R Chart용)
        /// </summary>
        /// <param name="a_dt">DataTabe.</param>
        /// <param name="iValue1">value_1의 컬럼의 반환되는 Index(위치).</param>
        public void RPT_10_R_GetData(DataTable a_dt, int iValue1)
        {
            int iValue25 = iValue1 + 25 ;

            DataTable dt = new DataTable();   // 새로운 DataTable dt를 만들어서
            dt = a_dt;                                       // dt에다가 받아온 a_dt 를 넣어준다.
            dt.Columns.Add("MIN", typeof(String));              
            dt.Columns.Add("MAX",typeof(String));


            //1. 각 Row 별로 MIN, MAX 값 구하기    
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


            //2. 새로운 DataTabe(dtR)를 생성해준다.
            dtR = new DataTable();

            if (dtR.Columns.Count == 0) // R-bar용
            {
                // 새로운 Datatable에 넣을 column도 정의하고            
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

            // 3. 1번에서 구한 각Row들의 MIN, MAX 값 가지고 LOT_ID로 Group By 해서 각각 총 평균을 구해서 LOT_ID, R값(MAX-MIN) 을
            //     새로운 DataTable(dtR)를 생성해서 넣어준다.

            spc.clear();    // 수학관련해서 모두 초기화하고
            string sLot = " ";
             int k = 0;

            for (int i = 0; i < dt.Rows.Count; i++)
            {

                if (!sLot.Equals(dt.Rows[i][0]))
                {
                    if (i==0) // 만약 맨 첫줄이라면
                    {
                      //  dtR.Rows.Add();
                      //   아무일도 안한다.
                    }
                    else if (i != 0)
                    {
                        dtR.Rows.Add();
                        dtR.Rows[k]["LOT_ID"] = sLot;       //   바로 전의 값들을  새로운 DataTable dtR에 넣어준다. LOT_ID넣어주ㅗㄱ
                        dtR.Rows[k]["R"] = spc.gap();       //   max-min값넣어주고
                        spc.clear();                          //  수학관련해서 초기화해주고: 왜냐하면 다음 번 값들을 새로 받아야 하니깐
                        k++;
                    }
                    sLot = Convert.ToString(dt.Rows[i][0]);               //   sLot 에 해당 컬럼값(LOT_ID) 대입하고



                    for (int s = dt.Columns.IndexOf("MIN")-1; s < dt.Columns.IndexOf("MIN")+1 ; s++)
                    {
                        double x = Convert.ToDouble(dt.Rows[i][s]);
                        spc.Add(x);                                                         //        x값들이 모아져서 sum......
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
                            spc.Add(x);                                                           //      x값들이 모아져서 sum......
                        }
                    }
                }

            }

            RPT_3_1_SetData(dtR, "R Chart");
            this.AxisY.Min = 0;              // R Chart는 무조건 Y축 최소값이 0
            this.AxisY.Gridlines=true;
            }
      


        #endregion


        #region " SPC용 수학공식 "
        /// <summary>
        /// Created by Eunhi Chang [2009-09-10]
        ///    - SPC Chart를 구현하기 위해서 필요한 수학공식들 모아둔다.
        /// </summary>
       
        // SPC CHART를 구현하기 위해서 필요한 수학함수 
        //  - SUM, MIN, MAX, STDEV(표준편차), GAP(MAX-MIN), MEAN(평균), N(COUNT)
        public  class cSpc   
        {
            private double sum, sum2, cmax, cmin;
            private int n;

            public cSpc()
            {
                clear();      
            }

            public void clear()   // 초기화
            {
                n = 0;
                cmin = 0;
                cmax = 0;
                sum = 0.00;
                sum2 = 0.00;
            }

            public void Add(double x)        // 값이 들어오면 초기화 전까지는 sum하고 min값과max값들을 비교하면서 구한다.
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

            public Double min()     // 들어온 값들  중에 min값 return
            {
                return cmin;
            }

            public Double max()        // 들어온 값들  중에 max값 r
            {
                return cmax;
            }

            // 수집된 데이터의 평균을 리턴
            public double mean()
            {
                return sum / (double)n;
            }

            // 모집단 데이터인 경우의 표준편차 리턴

            public double stdevp()
            {
                return Math.Sqrt(((double)n * sum2 - sum * sum) / ((double)n * (double)n));
            }

            // 모집단에서 추출된 샘플 데이터인 경우의 표준편차 리턴
            public double stdev()
            {
                return Math.Sqrt(((double)n * sum2 - sum * sum) / ((double)n * (double)(n - 1)));
            }

            public double gap()        // R-Chart를 구현하기 위한 R(max값 - min값) 을 구해준다.
            {
                return (cmax - cmin);
            }
        }
        #endregion
	}
}
