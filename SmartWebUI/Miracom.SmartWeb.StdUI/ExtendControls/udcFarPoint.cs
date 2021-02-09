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
using System.Data;
using System.Diagnostics;
using FarPoint.Win.Spread;
using FarPoint.Win.Spread.CellType;
using FarPoint.Win.Spread.Model;
using System.Windows.Forms;
using System.Drawing;

namespace Miracom.SmartWeb.UI.Controls
{
	public enum Align { Center, Left, Right };
	public enum VerticalAlign { Center, Top, Botton };
	public enum Visibles { True, False };
	public enum Frozen { True, False };
	public enum Merge { True, False, Left, Always };

    //2010-06-07-임종우 : 소숫점 4째자리 사용 위해 Double4 추가 함.
	public enum Formatter { Number, Double1, Double2, Double3, Double4, String, Percent, PercentDouble, PercentDouble2, Image, ComboBox };
	public enum RowColorType { General, None };
    public enum ShowTip { True, False };

	/// <summary>
	/// udcFarPoint에 대한 요약 설명입니다.
	/// </summary>
    /// 작성자 : 서영국(John)
    /// 작성일 : 2008.09.25
    
	public class udcFarPoint : FarPoint.Win.Spread.FpSpread
	{
		/// <summary>
		/// 필수 디자이너 변수입니다.
		/// </summary>
		private System.ComponentModel.Container components = null;
		
		// 표구성에서 컬럼 정보를 저장하기 위하여
		private struct ColumnInfo 
		{ 
			public CellHorizontalAlignment aling;
			public ICellType format;
			public float width;
		};

        public bool isShowZero = false;

		private ColumnInfo [] columnType;
		private ArrayList _cellType = new ArrayList(10);
		private static FarPoint.Win.Spread.CellType.NumberCellType _number;
        private static FarPoint.Win.Spread.CellType.TextCellType _text;
        private static FarPoint.Win.Spread.CellType.NumberCellType _double1;
		private static FarPoint.Win.Spread.CellType.NumberCellType _double2;
		private static FarPoint.Win.Spread.CellType.NumberCellType _double3;

        //2010-06-07-임종우 : 소숫점 4째자리 추가 함.
        private static FarPoint.Win.Spread.CellType.NumberCellType _double4;

		private static FarPoint.Win.Spread.CellType.PercentCellType _percent;
		private static FarPoint.Win.Spread.CellType.PercentCellType _percentDouble;
		private static FarPoint.Win.Spread.CellType.PercentCellType _percentDouble2;
		private static FarPoint.Win.Spread.CellType.ImageCellType _image;
		private static FarPoint.Win.Spread.CellType.ComboBoxCellType _ComboBox;
		private bool isFirstAdded;
		// 셀타입을 지정할는것을 수동으로 하고 싶을때 false 주면 됨..
		private bool isPreCellsType = true;

		// 이값 이하는 blank 로 만듬.
		private double RoundValue = 0.0009;
		private double RoundValueMinus = -0.0009;
		private double RoundValue2 = 0.009;
		private double RoundValue2Minus = -0.009;
		private double RoundValue2Per = 0.0009;
        private double RoundValue2PerMinus = -0.0009;
        private double RoundValue2Double1 = 0.04;
        private double RoundValue2Double1Minus = -0.04;
        private double RoundValue2Double2 = 0.004;
        private double RoundValue2Double2Minus = -0.004;
        private double RoundValue2Double3 = 0.0004;
        private double RoundValue2Double3Minus = -0.0004;

        private DataTable dtNote = null;

		public udcFarPoint(System.ComponentModel.IContainer container)
		{
			container.Add(this);
			InitializeComponent();
			PreDefinedCellType();
		}

		public udcFarPoint()
		{
			InitializeComponent();
			PreDefinedCellType();

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
			components = new System.ComponentModel.Container();
			this.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
				| System.Windows.Forms.AnchorStyles.Left) 
				| System.Windows.Forms.AnchorStyles.Right)));

		}
		#endregion

		public new object DataSource
		{
			get
			{
				return base.DataSource;
			}
			set
			{
				base.DataSource = value;
				if ( isPreCellsType )
				{
					// AutoGernerationColumn 이 true 이 상황에서
					// 데이타 바인딩 전에 하면 실제로 적용이 안됨
					// 데이타 바인딩 이후에 실제로 셀타입을 지정하기 위하여
					RPT_SetCellsType();
				}
			}
		}

		// 자동으로 선을긋는 함수..
		public void RPT_AutoCellLineColor()
		{
			this.RPT_AutoCellLineColor( -1 );
		}
			
		// 자동으로 선을긋는 함수..
		// MergeSize 선을 긋을 사이즈
		// -1 은 디폴트로 자동계산. 
		public void RPT_AutoCellLineColor( int MergeSize )
		{
			if ( this.ActiveSheet.RowCount < 1 )
				return;

			System.Drawing.Color LineColor = System.Drawing.Color.FromArgb(111, 117, 234);

			// 가로선 긋기..
			int [] rowLine = new int[ this.ActiveSheet.RowCount ];
			int realMergeSize = 0;
			
			for ( int i = 0; i < this.ActiveSheet.ColumnCount ; i++ )
			{
				if ( this.ActiveSheet.Columns[ i ].MergePolicy == MergePolicy.None )
					break;

				realMergeSize++;
			}

			if ( MergeSize == -1 )
			{
				MergeSize = realMergeSize;
			}

			bool Lined = false;
			bool Lined2 = false;

			// Merge  첫번째 부분 색상 
			for ( int j = 0; j < MergeSize ; j++ )
				this.ActiveSheet.Cells[ 0, j ].Border = new FarPoint.Win.LineBorder( LineColor, 1, false, false, false, true);

			for ( int i = 0; i < this.ActiveSheet.RowCount-1 ; i++ )
			{
	
				// Merge 된값이 변경되었는지 검사. 
				for ( int j = 0; j < MergeSize ; j++ )
				{
					if ( this.ActiveSheet.Columns[ j ].Visible == false )
						continue;

					if ( this.ActiveSheet.Cells[ i , j ].Text != this.ActiveSheet.Cells[ i+1 , j ].Text )
					{
						Lined = true;
						// Merge 된 부분 색상 
						for ( int k = j; k < MergeSize ; k++ )
							this.ActiveSheet.Cells[ i + 1, k ].Border = new FarPoint.Win.LineBorder( LineColor, 1, false, false, false, true);
						
						for ( int k = MergeSize; k <= realMergeSize-MergeSize ; k++ )
						{
							if ( i == 0 )
								break;
							
							for ( int h = i ; h > 0 ; h-- )
							{
								for ( int p = k ; p > 0 ; p-- )
								{
									if ( this.ActiveSheet.Cells[ h , p ].Text != this.ActiveSheet.Cells[ h-1 , p ].Text )
									{
										Lined2 = true;
										break;
									}
								}

								if ( Lined2 )
								{
									Lined2 = false;
									this.ActiveSheet.Cells[ h, k ].Border = new FarPoint.Win.LineBorder( LineColor, 1, false, false, false, true);
									break;
								}
							}
						}
						break;
					}
				}
				
				for ( int k = MergeSize; k <= realMergeSize-MergeSize ; k++ )
				{
					for ( int h = this.ActiveSheet.RowCount-1 ; h > 0 ; h-- )
					{
						for ( int p = k ; p > 0 ; p-- )
						{
							if ( this.ActiveSheet.Cells[ h , p ].Text != this.ActiveSheet.Cells[ h-1 , p ].Text )
							{
								Lined2 = true;
								break;
							}
						}

						if ( Lined2 )
						{
							Lined2 = false;
							this.ActiveSheet.Cells[ h, k ].Border = new FarPoint.Win.LineBorder( LineColor, 1, false, false, false, true);
							break;
						}
					}
				}

				// 색칠하기
				if ( Lined )
				{
					Lined = false;
					rowLine [ i ] = 1;
					this.ActiveSheet.Rows[ i ].Border = new FarPoint.Win.LineBorder( LineColor, 1, false, false, false, true);
				}
				else
				{
					rowLine [ i ] = 0;
				}
			}

			// Merge  부분 마지막 색상
			if ( MergeSize > 0 )
			{
				rowLine [ this.ActiveSheet.RowCount-1 ] = 1;
				this.ActiveSheet.Rows[ this.ActiveSheet.RowCount-1 ].Border = new FarPoint.Win.LineBorder( LineColor, 1, false, false, false, true);
			}

			// 세로선 긋기..
			if ( this.ActiveSheet.ColumnHeader.Rows.Count > 1 )
			{
				int nextLine = 0;
				int preLine = 0;

				for ( int i = 0; i < this.ActiveSheet.ColumnHeader.Columns.Count; i+= nextLine )
				{
					nextLine = this.ActiveSheet.ColumnHeader.Cells[ 0, i ].ColumnSpan;

					if ( this.ActiveSheet.Columns[ i ].Visible == false )
						continue;
				
					if ( nextLine > 1 )
					{
						// right
						if ( i + nextLine == this.ActiveSheet.ColumnCount )
						{
							for ( int r = 0; r < this.ActiveSheet.RowCount ; r++ )
							{
								if ( rowLine [ r ] == 0 )
									this.ActiveSheet.Cells[ r, i + nextLine-1 ].Border = new FarPoint.Win.LineBorder( LineColor, 1, false, false, true, false);
								else
									this.ActiveSheet.Cells[ r, i + nextLine-1 ].Border = new FarPoint.Win.LineBorder( LineColor, 1, false, false, true, true);
							}
						}
						else
						{
							for ( int r = 0; r < this.ActiveSheet.RowCount ; r++ )
							{
								if ( rowLine [ r ] == 0 )
									this.ActiveSheet.Cells[  r, i + nextLine ].Border = new FarPoint.Win.LineBorder( LineColor, 1, true, false, false, false);
								else
									this.ActiveSheet.Cells[  r, i + nextLine ].Border = new FarPoint.Win.LineBorder( LineColor, 1, true, false, false, true);
							}
						}
						// left
						if ( i != preLine+1 )
						{
							for ( int r = 0; r < this.ActiveSheet.RowCount ; r++ )
							{
								if ( rowLine [ r ] == 0 )
									this.ActiveSheet.Cells[ r, i ].Border = new FarPoint.Win.LineBorder( LineColor, 1, true, false, false, false);
								else
									this.ActiveSheet.Cells[ r, i ].Border = new FarPoint.Win.LineBorder( LineColor, 1, true, false, false, true);
							}
							
						}
						preLine = i + nextLine-1;
					}
				}
			}
		}

		public bool RPT_IsPreCellsType
		{
			get { return isPreCellsType; }
			set { isPreCellsType = value; }
		}

        // 자동으로 칼럼의 넓이를 조정하는 함수.
        public void RPT_AutoFit(bool bIgnoreHeaders)
        {
            for (int i = 0; i < this.ActiveSheet.Columns.Count; i++)
            {
               float width_max = this.ActiveSheet.GetPreferredColumnWidth(i,bIgnoreHeaders);
                this.ActiveSheet.Columns[i].Width = width_max;
            }
        }

        // 셀타입을 정의 해주기 위하여
        // 보통은 자동으로 됨..
        // RptSpreadUtil 을 쓴 경우만 콜.. 
        public void RPT_SetCellsType()
        {
            int size = 0;
            if (_cellType.Count > this.ActiveSheet.Columns.Count)
                size = this.ActiveSheet.Columns.Count;
            else
                size = _cellType.Count;

            for (int i = 0; i < size; i++)
            {
                this.ActiveSheet.Columns[i].CellType = (FarPoint.Win.Spread.CellType.ICellType)_cellType[i];
                if (_cellType[i] != _text && _cellType[i] != _number && this.isShowZero != true)
                    RPT_SetBlankFromZero(i);
            }
        }

        /// <summary>
        /// 칼럼이 모두 0이거나 공백인경우 해당 칼럼을 삭제.
        /// </summary>
        /// <param name="iStartColPosition">숫자부분의 시작칼럼 위치(0부터 시작함)</param>        
        /// <param name="iTotalRowSize">총합계가 있을경우 총합계 Row Size. 합계가 없을경우 0으로 입력.</param>
        public void RPT_RemoveZeroColumn(int iStartColPosition, int iTotalRowSize)
        {
            RPT_RemoveZeroColumn(iStartColPosition, this.ActiveSheet.Columns.Count-1 , iTotalRowSize);
        }

        /// <summary>
        /// 칼럼이 모두 0이거나 공백인경우 해당 칼럼을 삭제.
        /// </summary>
        /// <param name="iStartColPosition">숫자부분의 시작칼럼 위치(0부터 시작함)</param>
        /// <param name="iEndColPosition">숫자부분의 끝칼럼 위치(0부터 시작함)</param>
        /// <param name="iTotalRowSize">총합계가 있을경우 총합계 Row Size. 합계가 없을경우 0으로 입력.</param>
        public void RPT_RemoveZeroColumn(int iStartColPosition, int iEndColPosition, int iTotalRowSize)
        { 
            try
            {
                double dTmpTotal = 0.0;
                int iEndRow = 0;

                if (iTotalRowSize == 0) //Total이 없는경우 전체 Row을 체크함.                    
                    iEndRow = this.ActiveSheet.Rows.Count;
                else
                    iEndRow = iTotalRowSize;

                for (int j = iEndColPosition; j >= iStartColPosition; j--)
                {   
                    dTmpTotal = 0;
                    
                    //합계가 있는경우 합계칼럼의 합이 0이면 삭제한다.                        
                    for (int i = 0; i < iEndRow; i++)
                    {
                        if (this.ActiveSheet.Cells[i, j].Text != "")
                        {
                            if (CmnFunction.CheckNumeric(this.ActiveSheet.Cells[i, j].Text) == false)
                                dTmpTotal = +1;
                            else
                                dTmpTotal = +Convert.ToDouble(this.ActiveSheet.Cells[i, j].Value);
                        }                         
                    }

                    if (dTmpTotal == 0.0)
                    {
                        this.ActiveSheet.Columns[j].Remove();
                    }                                                              
                }
            }
            catch (Exception)
            {
                //Do nothign
            }
        }

		// 컬럼 타입이 숫자 형식인 경우 0 이면 빈 공백이 나오도록 처리함..  
		public void RPT_SetBlankFromZero( int columnPos )
		{
			double tempRound = RoundValue2;
			double tempRoundMinus = RoundValue2Minus;
			double temp = 0;

			if ( _cellType[columnPos] == _percentDouble2 )
			{
				tempRound = RoundValue2Per;
				tempRoundMinus = RoundValue2PerMinus;
            }
            else if (_cellType[columnPos] == _double1)
            {
                tempRound = RoundValue2Double1;
                tempRoundMinus = RoundValue2Double1Minus;
            }
			else if ( _cellType[columnPos] == _double2 )
			{
				tempRound = RoundValue2Double2;
				tempRoundMinus = RoundValue2Double2Minus;
            }
            else if (_cellType[columnPos] == _double3)
            {
                tempRound = RoundValue2Double3;
                tempRoundMinus = RoundValue2Double3Minus;
            }

			try 
			{
				for ( int i = 0; i < this.ActiveSheet.Rows.Count; i++)
				{
					if ( this.ActiveSheet.Cells [ i, columnPos ].Value == null ||  this.ActiveSheet.Cells [ i, columnPos ].Value.ToString() == " ")
						continue;
					
					temp = Convert.ToDouble ( this.ActiveSheet.Cells [ i, columnPos ].Value );
					if (  temp < tempRound &&  temp > tempRoundMinus )
					{
						
						this.ActiveSheet.Cells [ i, columnPos ].Formula = "";
						this.ActiveSheet.Cells [ i, columnPos ].Text = "";
					}
				}
			}
			catch(Exception)
			{
				throw new Exception(RptMessages.GetMessage("STD089", GlobalVariable.gcLanguage)); 
			}
		}

		// 컬럼 타입이 숫자 형식인 특정 row만 0을 빈칸으로 만들고 싶을때 
		public void RPT_SetBlankFromZero( int row, int columnPos, int columnSize )
		{
			double tempRound = RoundValue2;
			double tempRoundMinus = RoundValue2Minus;
			double temp = 0;

			if ( _cellType[columnPos] == _percentDouble2 )
			{
				tempRound = RoundValue2Per;
				tempRoundMinus = RoundValue2PerMinus;
            }
            else if (_cellType[columnPos] == _double1)
            {
                tempRound = RoundValue2Double1;
                tempRoundMinus = RoundValue2Double1Minus;
            }
			else if ( _cellType[columnPos] == _double2 )
			{
				tempRound = RoundValue2Double2;
				tempRoundMinus = RoundValue2Double2Minus;
            }
            else if (_cellType[columnPos] == _double3)
            {
                tempRound = RoundValue2Double3;
                tempRoundMinus = RoundValue2Double3Minus;
            }

			try
			{
				for ( int i = columnPos ; i < columnPos + columnSize; i++)
				{

					if ( this.ActiveSheet.Cells [row, columnPos ].Value == null )
						continue;

					temp = Convert.ToDouble ( this.ActiveSheet.Cells [ row , i  ].Value );

					if (  temp < tempRound &&  temp > tempRoundMinus )
					{
						this.ActiveSheet.Cells [ row , i ].Formula = "";
						this.ActiveSheet.Cells [ row , i ].Text = "";
					}
				}
			}
			catch(Exception)
			{
				throw new Exception(RptMessages.GetMessage("STD089", GlobalVariable.gcLanguage)); 
			}
		}

        
		// CellType 을 먼저 정의해 놓은 부분
		private void PreDefinedCellType()
		{
			if ( _number == null )
			{
				_number = new FarPoint.Win.Spread.CellType.NumberCellType();
				_number.ButtonAlign = FarPoint.Win.ButtonAlign.Right;
				_number.DecimalPlaces = 0;
				_number.DropDownButton = false;
				_number.MaximumValue = 9999999;
				_number.MinimumValue = -9999999;
				_number.LeadingZero = FarPoint.Win.Spread.CellType.LeadingZero.No;
				_number.Separator = ",";
				_number.ShowSeparator = true;
				_number.NegativeRed = true;

				_text =  new FarPoint.Win.Spread.CellType.TextCellType();
			}

            if (_double1 == null)
            {
                _double1 = new FarPoint.Win.Spread.CellType.NumberCellType();
                _double1.ButtonAlign = FarPoint.Win.ButtonAlign.Right;
                _double1.DecimalPlaces = 1;
                _double1.DropDownButton = false;
                _double1.MaximumValue = 9999999.9;
                _double1.MinimumValue = -9999999.9;
                _double1.Separator = ",";
                _double1.ShowSeparator = true;
                _double1.NegativeRed = true;
            }

			if ( _double2 == null )
			{
				_double2 = new FarPoint.Win.Spread.CellType.NumberCellType();
				_double2.ButtonAlign = FarPoint.Win.ButtonAlign.Right;
				_double2.DecimalPlaces = 2;
				_double2.DropDownButton = false;
				_double2.MaximumValue = 9999999.99;
				_double2.MinimumValue = -9999999.99;
				_double2.Separator = ",";
				_double2.ShowSeparator = true;
				_double2.NegativeRed = true;
			}

			if ( _double3 == null )
			{
				_double3 = new FarPoint.Win.Spread.CellType.NumberCellType();
				_double3.ButtonAlign = FarPoint.Win.ButtonAlign.Right;
				_double3.DecimalPlaces = 3;
				_double3.DropDownButton = false;
				_double3.MaximumValue = 9999999.999;
				_double3.MinimumValue = -9999999.999;
				_double3.Separator = ",";
				_double3.ShowSeparator = true;
				_double3.NegativeRed = true;
			}

            // 2010-06-07-임종우 : 소숫점 4째자리 표시를 위해 추가함.
            if (_double4 == null)
            {
                _double4 = new FarPoint.Win.Spread.CellType.NumberCellType();
                _double4.ButtonAlign = FarPoint.Win.ButtonAlign.Right;
                _double4.DecimalPlaces = 4;
                _double4.DropDownButton = false;
                _double4.MaximumValue = 9999999.9999;
                _double4.MinimumValue = -9999999.9999;
                _double4.Separator = ",";
                _double4.ShowSeparator = true;
                _double4.NegativeRed = true;
            }

			if ( _percent == null )
			{
				_percent = new FarPoint.Win.Spread.CellType.PercentCellType();
				_percent.ButtonAlign = FarPoint.Win.ButtonAlign.Right;
				_percent.DecimalPlaces = 0;
				_percent.DecimalSeparator= ".";
				_percent.DropDownButton = false;
				_percent.WordWrap = true;
				_percent.PercentSign = "%";
				_percent.Separator = ",";
				_percent.ShowSeparator = true;
				_percent.NegativeRed = true;
			}

			if ( _percentDouble == null )
			{
				_percentDouble = new FarPoint.Win.Spread.CellType.PercentCellType();
				_percentDouble.ButtonAlign = FarPoint.Win.ButtonAlign.Right;
				_percentDouble.DropDownButton = false;
				_percentDouble.PercentSign = "%";
				_percentDouble.Separator = ",";
				_percentDouble.DecimalPlaces = 1;
				_percentDouble.ShowSeparator = true;
				_percentDouble.NegativeRed = true;
			}

			if ( _percentDouble2 == null )
			{
				_percentDouble2 = new FarPoint.Win.Spread.CellType.PercentCellType();
				_percentDouble2.ButtonAlign = FarPoint.Win.ButtonAlign.Right;
				_percentDouble2.DropDownButton = false;
				_percentDouble2.PercentSign = "%";
				_percentDouble2.Separator = ",";
				_percentDouble2.DecimalPlaces = 3;
				_percentDouble2.ShowSeparator = true;
				_percentDouble2.NegativeRed = true;
			}

			if ( _image == null )
			{
				_image = new FarPoint.Win.Spread.CellType.ImageCellType();
				_image.Style = FarPoint.Win.RenderStyle.Stretch;
			}

			// 2013-09-24-정비재 : combobox를 사용하기 위하여 추가함.
			if (_ComboBox == null)
			{
				_ComboBox = new FarPoint.Win.Spread.CellType.ComboBoxCellType();
				_ComboBox.ButtonAlign  = FarPoint.Win.ButtonAlign.Right;
			}
		}

		// 셀타입을 리턴해주는 메소드
		private FarPoint.Win.Spread.CellType.ICellType getCellType( Formatter formatter )
		{
            if (formatter == Formatter.Number)
                return _number;
            else if (formatter == Formatter.Double1)
                return _double1;
            else if (formatter == Formatter.Double2)
                return _double2;
            else if (formatter == Formatter.Double3)
                return _double3;
            else if (formatter == Formatter.Double4)
                return _double4;
            else if (formatter == Formatter.Percent)
                return _percent;
            else if (formatter == Formatter.PercentDouble)
                return _percentDouble;
            else if (formatter == Formatter.PercentDouble2)
                return _percentDouble2;
            else if (formatter == Formatter.Image)
                return _image;
			else if (formatter == Formatter.ComboBox)
				return _ComboBox;
            else
                return _text;
		}		 

		// 표구성으로부터 그리드 컬럼의 Visual 여부를 결정
        public void RPT_ColumnConfigFromTable(udcButton Mbt)
        {
            this.RPT_ColumnConfigFromTable(Mbt, this.ActiveSheet.ColumnHeader.Rows.Count - 1);
        }

		// 표구성으로부터 그리드 컬럼의 Visual 여부를 결정
		// rowPos 는 헤더 텍스트가 생길 컬럼
        public void RPT_ColumnConfigFromTable(udcButton Mbt, int HeaderRowPos)
        {
            try
            {
                if (Mbt.AutoBindingMultiButtonType.ToString() != "S_Group")
                {
                    CmnFunction.ShowMsgBox(RptMessages.GetMessage("STD37", GlobalVariable.gcLanguage));
                    return;
                }

                bool isFirstColumn = false;
                                
                ArrayList arr = ((udcTableForm)Mbt.BindingForm).SelectedValueToArrayListToValuedTreeNode;
                

                // 표구성에 처음으로 들어온 경우 컬럼 타입을 미리저장해놓구 순서에 따라 적용해줌
                if (columnType == null)
                {
                    columnType = new ColumnInfo[arr.Count];

                    for (int i = 0; i < columnType.Length; i++)
                    {
                        ColumnInfo info;
                        info.aling = this.ActiveSheet.Columns[i].HorizontalAlignment;
                        info.format = this.ActiveSheet.Columns[i].CellType;
                        info.width = this.ActiveSheet.Columns[i].Width;

                        columnType[i] = info;
                    }
                }

                for (int i = 0; i < arr.Count; i++)
                {
                    ValuedTreeNode node = (ValuedTreeNode)arr[i];
                    if (node.Value == "FALSE")
                        this.ActiveSheet.Columns[i].Visible = false;
                    else
                    { 
                        // 컬럼의 병합을 제어하기 위하여
                        if (isFirstColumn == false)
                        {
                            isFirstColumn = true;
                            // 첫번째 헤더는 Restricted 이면 병합이 안됨
                            if (this.ActiveSheet.Columns[i].MergePolicy == MergePolicy.Restricted)
                                this.ActiveSheet.Columns[i].MergePolicy = MergePolicy.Always;
                        }
                        else
                        {
                            // 두번째 헤더부터는 Always 이면 병합이 이상함.. 
                            if (this.ActiveSheet.Columns[i].MergePolicy == MergePolicy.Always)
                                this.ActiveSheet.Columns[i].MergePolicy = MergePolicy.Restricted;
                        }

                        this.ActiveSheet.Columns[i].Visible = true;
                        this.ActiveSheet.ColumnHeader.Cells.Get(0, i).Text = node.Text;

                        //해더 칼럼 정보 복사.
                        for (int k = 1; k < this.ActiveSheet.ColumnHeader.RowCount ; k++)
                            this.ActiveSheet.ColumnHeader.Cells.Get(k, i).Text = node.Text;

                        // 컬럼 정보를 변경함.
                        this.ActiveSheet.Columns[i].HorizontalAlignment = columnType[node.Seq].aling;
                        this.ActiveSheet.Columns[i].CellType = columnType[node.Seq].format;
                        this.ActiveSheet.Columns[i].Width = columnType[node.Seq].width;
                    }
                }
            }
            catch (Exception ex)
            {
                CmnFunction.ShowMsgBox("RPT_ColumnConfigFromTable" + "\r\n" + ex.Message);
            }
        }


        // sortinit() 를 여러번 사용 할 경우 by MgKim 
        public void RPT_ColumnConfigFromTable_New(udcButton Mbt)
        {
            this.RPT_ColumnConfigFromTable_New(Mbt, this.ActiveSheet.ColumnHeader.Rows.Count - 1);
        }

        // sortinit() 를 여러번 사용 할 경우 by MgKim
        public void RPT_ColumnConfigFromTable_New(udcButton Mbt, int HeaderRowPos)
        {
            try
            {

                if (Mbt.AutoBindingMultiButtonType.ToString() != "S_Group")
                {
                    CmnFunction.ShowMsgBox(RptMessages.GetMessage("STD037", GlobalVariable.gcLanguage));
                    return;
                }

                bool isFirstColumn = false;


                ArrayList arr = ((udcTableForm)Mbt.BindingForm).SelectedValueToArrayListToValuedTreeNode_New;

                columnType = null;  //add 20150529

                // 표구성에 처음으로 들어온 경우 컬럼 타입을 미리저장해놓구 순서에 따라 적용해줌
                if (columnType == null)
                {
                    columnType = new ColumnInfo[arr.Count];

                    for (int i = 0; i < columnType.Length; i++)
                    {
                        ColumnInfo info;
                        info.aling = this.ActiveSheet.Columns[i].HorizontalAlignment;
                        info.format = this.ActiveSheet.Columns[i].CellType;
                        info.width = this.ActiveSheet.Columns[i].Width;

                        columnType[i] = info;
                    }
                }

                for (int i = 0; i < arr.Count; i++)
                {
                    ValuedTreeNode node = (ValuedTreeNode)arr[i];
                    if (node.Value == "FALSE")
                        this.ActiveSheet.Columns[i].Visible = false;
                    else
                    {
                        // 컬럼의 병합을 제어하기 위하여
                        if (isFirstColumn == false)
                        {
                            isFirstColumn = true;
                            // 첫번째 헤더는 Restricted 이면 병합이 안됨
                            if (this.ActiveSheet.Columns[i].MergePolicy == MergePolicy.Restricted)
                                this.ActiveSheet.Columns[i].MergePolicy = MergePolicy.Always;
                        }
                        else
                        {
                            // 두번째 헤더부터는 Always 이면 병합이 이상함.. 
                            if (this.ActiveSheet.Columns[i].MergePolicy == MergePolicy.Always)
                                this.ActiveSheet.Columns[i].MergePolicy = MergePolicy.Restricted;
                        }

                        this.ActiveSheet.Columns[i].Visible = true;
                        this.ActiveSheet.ColumnHeader.Cells.Get(0, i).Text = node.Text;

                        // 컬럼 정보를 변경함.
                        this.ActiveSheet.Columns[i].HorizontalAlignment = columnType[node.Seq].aling;
                        this.ActiveSheet.Columns[i].CellType = columnType[node.Seq].format;
                        this.ActiveSheet.Columns[i].Width = columnType[node.Seq].width;

                    }
                }
            }
            catch (Exception ex)
            {
                CmnFunction.ShowMsgBox("RPT_ColumnConfigFromTable" + "\r\n" + ex.Message);
            }
        }

		// 제일 상단에 Total 계산 
		/// <summary>
		///  전체 합계한줄을 그리드 상단에 나타내는 메소드
		///  startPosition 이후의 셀타입이 문자인 경우 그냥 제일 처음의 문자를 가져옴
		/// </summary>
		/// <param name="startPosition"> 계산을 시작할 위치 (시작은 0 부터) </param>
		public void RPT_AddTotalRow ( int startPosition )
		{
			this.ActiveSheet.AddRows(0,1);
			FarPoint.Win.Spread.SheetView Sheet = this.ActiveSheet;
			
			// 제일 처음 컬럼에 Total 
			Sheet.Cells.Get( 0, 0 ).Value = "Total";

			// 전체 컬럼을 한번 순회함
			for ( int columnPos = startPosition; columnPos < Sheet.ColumnCount; columnPos++)
			{
				switch ( Sheet.Columns[ columnPos ].CellType.ToString()  )
				{
					case "NumberCellType":
						// 전체 row 한번 순회
						double total = 0;
						for ( int rowPos = 1; rowPos < Sheet.Rows.Count ;  rowPos++ )
							total += Convert.ToDouble(Sheet.Cells.Get( rowPos, columnPos ).Value);
						Sheet.Cells.Get( 0, columnPos ).Value = total;
						RPT_SetBlankFromZero ( 0, columnPos, 1);
						break;
					case "TextCellType":
						if (  Sheet.Rows.Count > 1 )
							Sheet.Cells.Get( 0, columnPos ).Value = Sheet.Cells.Get( 1, columnPos ).Value;
						break;
				}				
			}

			this.ActiveSheet.FrozenRowCount = 1;
		}


		// 특정 컬럼 exceptColumnPos 의 특정 값 exceptValue 이 있는 부분은 합계하지 않고 넘어감.. 
		public void RPT_AddTotalRow ( int startPosition , int exceptColumnPos, object exceptValue )
		{
			this.ActiveSheet.AddRows(0,1);
			FarPoint.Win.Spread.SheetView Sheet = this.ActiveSheet;
			
			// 제일 처음 컬럼에 Total 
			Sheet.Cells.Get( 0, 0 ).Value = "Total";

			// 전체 컬럼을 한번 순회함
			for ( int columnPos = startPosition; columnPos < Sheet.ColumnCount; columnPos++)
			{
				switch ( Sheet.Columns[ columnPos ].CellType.ToString()  )
				{
					case "NumberCellType":
						// 전체 row 한번 순회
						double total = 0;
						for ( int rowPos = 1; rowPos < Sheet.Rows.Count ;  rowPos++ )
						{
							if ( Sheet.Cells.Get( rowPos, exceptColumnPos ).Value.ToString() != exceptValue.ToString() )
								total += Convert.ToDouble(Sheet.Cells.Get( rowPos, columnPos ).Value);
						}
						Sheet.Cells.Get( 0, columnPos ).Value = total;
						RPT_SetBlankFromZero ( 0, columnPos, 1);
						break;
					case "TextCellType":
						if (  Sheet.Rows.Count > 1 )
							Sheet.Cells.Get( 0, columnPos ).Value = Sheet.Cells.Get( 1, columnPos ).Value;
						break;
				}
			}
			
			this.ActiveSheet.FrozenRowCount = 1;
		}

		// startPosition를 포함하여 전부 히든
		public void RPT_HiddenColumnFromStartPosition ( int startPosition )
		{
			this.ActiveSheet.ColumnCount = startPosition;
		}
	

		/// <summary>
		/// RPT_AddTotalRow 동일하나 속도가 느림.. but Sheet 상단이 아닌 직접 DataTable 상단에 데이타를 입력함
		/// 따라서 이작업 이후에 다른 작업이 필요할때 이 메소드를 태워야함..
		/// </summary>
		/// <param name="startPosition"> 시작은 0부터 </param>
		public void RPT_AddTotalRow2 ( int startPosition )
		{
			DataTable org = (DataTable) this.DataSource;
			DataTable dt = new DataTable();
			this.DataSource = null;
			// 컬럼 타입 생성
			for ( int i = 0; i < org.Columns.Count; i++)
			{
				dt.Columns.Add( org.Columns[i].ColumnName, org.Columns[i].DataType );
			}

			DataRow total = dt.NewRow();

			total[0] = "Total";

			// 전체 컬럼을 한번 순회 하여 합계컬럼을 만듬
			for ( int columnPos = startPosition; columnPos < dt.Columns.Count ; columnPos++)
			{
				switch  ( dt.Columns[ columnPos ].DataType.Name.ToUpper()  )
				{
					case "STRING":
						if (  org.Rows.Count > 1 )
							total[columnPos] = org.Rows[0][columnPos];
						break;
					case "INT":
                    case "INT32":
						int sumInt = 0;
						for ( int rowPos = 0; rowPos < org.Rows.Count ;  rowPos++ )
						{
							if ( org.Rows[rowPos][columnPos] != DBNull.Value )
								sumInt += Convert.ToInt32( org.Rows[rowPos][columnPos] );
						}
						total[columnPos] = sumInt;
						break;
                    case "DECIMAL":
					case "DOUBLE":
					case "PERCENT":
						double sumDouble = 0;
						for ( int rowPos = 0; rowPos < org.Rows.Count ;  rowPos++ )
						{
							if ( org.Rows[rowPos][columnPos] != DBNull.Value )
								sumDouble += Convert.ToDouble( org.Rows[rowPos][columnPos] );
						}
						total[columnPos] = sumDouble;
						break;
					default:
						break;
				}

			}

			// 합계를 더함
			dt.Rows.Add(total);
			
			// 나머지 row 을 추가함
			for ( int i = 0; i <  org.Rows.Count; i++)
			{
				DataRow newRow = dt.NewRow();
				newRow.ItemArray = org.Rows[i].ItemArray;
				dt.Rows.Add( newRow);
			}

			this.DataSource = dt;
			this.ActiveSheet.FrozenRowCount = 1;

		}


		/// <summary>
		/// 한줄을 여러줄로 분활하는 메소드,, 나머지 줄은 무시됨
		/// </summary>
		/// <param name="basisColumnSize">분활되지 않을 기준 컬럼들</param>
		/// <param name="divideRowsSize">분활한 Row 수</param>
		/// <param name="repeatColumnSize">한줄에 들어갈 Column 수 - 연속적으로 반복되어야함</param>
		public void RPT_DivideRows( int basisColumnSize, int divideRowsSize, int repeatColumnSize )
		{	
			RPT_DivideRows ( basisColumnSize, divideRowsSize, repeatColumnSize, 0 );
		}

		/// <summary>
		/// 한줄을 여러줄로 분활하는 메소드,, 나머지 줄은 무시됨
		/// </summary>
		/// <param name="basisColumnSize">분활되지 않을 기준 컬럼들 1부터</param>
		/// <param name="divideRowsSize">분활한 Row 수 1부터</param>
		/// <param name="repeatColumnSize">한줄에 들어갈 Column 수 - 연속적으로 반복되어야함</param>
		/// <param name="dummyColumn">이 메소드를 쓰면 컬럼타입지정시애 충돌를 방지하기 위하여 미리 컬럼을 늘려놓음</param>
		public void RPT_DivideRows( int basisColumnSize, int divideRowsSize, int repeatColumnSize, int dummyColumn )
		{	

			// 자동 Sort 를 막음
			for(int i=0; i< this.ActiveSheet.ColumnCount; i++)
				this.ActiveSheet.Columns.Get(i).AllowAutoSort = false;

			DataTable dt = new DataTable();
			DataTable org = (DataTable)this.DataSource;
			//			this.DataSource = null;

			// 타입에 맞는 컬럼을 생성
			for ( int i = 0; i < ( basisColumnSize + repeatColumnSize ) ; i++ )
			{
				dt.Columns.Add( org.Columns[i].ColumnName, org.Columns[i].DataType );
			}

			// 테이블이 끝날때까지 반복
			for ( int currentOrgRow = 0; currentOrgRow < org.Rows.Count ; currentOrgRow++ )
			{
				// 오리지널 테이블의 column 값(정확히는 basisColumnSize 를 더해야함)  .. row 값은 currentOrgRow
				int currentOrgColum = 0;

				// 분활될 Row 수만큼 생성
				for ( int j = 0 ; j < divideRowsSize; j++ )
				{
					DataRow row = dt.NewRow();
					
					// 기본 컬럼값 셋팅
					for ( int k =0 ; k <  basisColumnSize ; k++ )
					{
						row[k] = org.Rows[ currentOrgRow ][ k ];
					}

					// 나머지 값 세팅
					for ( int m = 0 ; m < repeatColumnSize ;  m++, currentOrgColum++ )
					{
						row[ basisColumnSize +m ] = org.Rows[ currentOrgRow ][ basisColumnSize +currentOrgColum ];
					}
					
					// row 등록
					dt.Rows.Add( row );
				}

			}
	
			// 더미 컬럼 추가 
			for ( int i = 0; i < dummyColumn ; i++ )
			{
				dt.Columns.Add();
			}

			// 마지막 리턴
			this.DataSource = dt;

			// row fix
			this.ActiveSheet.FrozenRowCount *= divideRowsSize;
			
			if ( isPreCellsType )
			{
				RPT_SetCellsType();
			}

		}


		/// <summary>
		/// 이전 컬럼 세팅을 전부 지우고 새로게 셋팅
		/// </summary>
		public void RPT_ColumnInit()
		{
			isFirstAdded = true;
			this.ActiveSheet.Columns.Count = 0;
			this.ActiveSheet.DataAutoSizeColumns = false;
			this.ActiveSheet.FrozenColumnCount = 0;
			this.ActiveSheet.Rows.Count = 0;
			this._cellType.Clear();
            this.InitNoteTable();            
		}

        private void InitNoteTable()
        {             
            DataColumn column;
            
            if (dtNote != null)
            {
                dtNote.Dispose();
            }

            dtNote = new DataTable("NoteInfo");

            column = new DataColumn();
            column.DataType = System.Type.GetType("System.Int16");
            column.ColumnName = "Row";
            column.ReadOnly = false;
            column.Unique = false;
            // Add the Column to the DataColumnCollection.
            dtNote.Columns.Add(column);

            column = new DataColumn();
            column.DataType = System.Type.GetType("System.Int16");
            column.ColumnName = "Col";
            column.ReadOnly = false;
            column.Unique = false;
            // Add the Column to the DataColumnCollection.
            dtNote.Columns.Add(column);

            column = new DataColumn();
            column.DataType = System.Type.GetType("System.String");
            column.ColumnName = "Note";
            column.ReadOnly = false;
            column.Unique = false;
            // Add the Column to the DataColumnCollection.
            dtNote.Columns.Add(column);

            System.Data.DataSet dataSet = new DataSet();
            // Add the new DataTable to the DataSet.
            dataSet.Tables.Add(dtNote);
        }

        private void SetNoteInfo()
        {
            int iRow = 0;
            int iCol = 0;
            string sNote = "";

            //Data가 있는경우만 Note정보 표시해줌.
            if (this.ActiveSheet.RowCount > 0)
            {
                for (int i = 0; i < dtNote.Rows.Count; i++)
                {
                    sNote = dtNote.Rows[i][2].ToString().TrimEnd();

                    if (sNote != "")
                    {
                        iRow = Convert.ToInt16(dtNote.Rows[i][0]);
                        iCol = Convert.ToInt16(dtNote.Rows[i][1]);

                        this.ActiveSheet.ColumnHeader.Cells.Get(iRow, iCol).Note = sNote;
                        this.ActiveSheet.ColumnHeader.Cells.Get(iRow, iCol).NoteIndicatorSize = new System.Drawing.Size(5, 5);
                    }
                }
            }
        }

        /// <summary>
        /// 가장 기본적인 컬럼 생성기
        /// 컬럼은 하나씩 더해가고 row 는 젤 마지막의 Header row에 추가
        /// </summary>
        /// <param name="header_Text"></param>
        /// <param name="isVisible"></param>
        /// <param name="isFrozen"></param>
        /// <param name="aling"></param>
        /// <param name="isMerge"></param>
        /// <param name="format"></param>
        /// <param name="size"></param>
        public void RPT_AddBasicColumn(string header_Text, Visibles isVisible, Frozen isFrozen, Align aling, Merge isMerge, Formatter format, int width)
		{
			int rowPos = (this.ActiveSheet.ColumnHeader.Rows.Count==0?0:this.ActiveSheet.ColumnHeader.Rows.Count-1);
			this.RPT_AddBasicColumn(header_Text, rowPos, this.ActiveSheet.Columns.Count, isVisible, isFrozen, aling, isMerge, format, width, null);
		}

        public void RPT_AddBasicColumn(string header_Text, Visibles isVisible, Frozen isFrozen, Align aling, Merge isMerge, Formatter format, int width, string header_Note)
        {
            int rowPos = (this.ActiveSheet.ColumnHeader.Rows.Count == 0 ? 0 : this.ActiveSheet.ColumnHeader.Rows.Count - 1);
			this.RPT_AddBasicColumn(header_Text, rowPos, this.ActiveSheet.Columns.Count, isVisible, isFrozen, aling, isMerge, format, width, header_Note);
        }
        //----------------

        public void RPT_AddBasicColumn(string header_Text, int header_rowPos, int header_columnPos, Visibles isVisible, Frozen isFrozen, Align aling, Merge isMerge, Formatter format, int width)
        {
            RPT_AddBasicColumn(header_Text, header_rowPos, header_columnPos, isVisible, isFrozen, aling, isMerge, format, width, null);
        }

		public void RPT_AddBasicColumn(string header_Text, int header_rowPos, int header_columnPos, Visibles isVisible, Frozen isFrozen, Align aling, Merge isMerge, Formatter format, int width, string header_Note)
		{
			// 공통적으로 적용할 부분
            DataRow dtRow;

            try
            {
                if (!isFirstAdded)
                {
                    isFirstAdded = true;
                    this.ActiveSheet.Columns.Count = 0;
                    this.ActiveSheet.DataAutoSizeColumns = false;
                    this.ActiveSheet.FrozenColumnCount = 0;
                    this.ActiveSheet.Rows.Count = 0;
                    this._cellType.Clear();
                }

                int columnPos = header_columnPos;
                int rowPos = header_rowPos;

                // 컬럼 사이즈가 현재 사이즈 보다 큰경우 컬럼 사이즈 증가
                if (columnPos >= this.ActiveSheet.Columns.Count)
                    this.ActiveSheet.Columns.Count = columnPos + 1;

                // row 사이즈가 현재 사이즈 보다 큰경우 row 사이즈 증가
                if (rowPos >= this.ActiveSheet.ColumnHeader.Rows.Count)
                    this.ActiveSheet.ColumnHeader.Rows.Count = rowPos + 1;

                this.ActiveSheet.ColumnHeader.Cells.Get(rowPos, columnPos).Text = header_Text;

                //Spread의 Row가 0이면 오류 발생함.                
                //this.ActiveSheet.ColumnHeader.Cells.Get(rowPos, columnPos).Note = header_Text;
                //this.ActiveSheet.ColumnHeader.Cells.Get(rowPos, columnPos).NoteIndicatorSize = new System.Drawing.Size(7, 7);

                if (header_Note == null) header_Note = "";

                dtRow = dtNote.NewRow();
                dtRow["Row"] = rowPos;
                dtRow["Col"] = columnPos;
                dtRow["Note"] = header_Note;
                dtNote.Rows.Add(dtRow);                

                if (isVisible == Visibles.True)
                    this.ActiveSheet.Columns[columnPos].Visible = true;
                else
                    this.ActiveSheet.Columns[columnPos].Visible = false;

                this.ActiveSheet.Columns[columnPos].Width = width;

                if (aling == Align.Center)
                    this.ActiveSheet.Columns[columnPos].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
                else if (aling == Align.Left)
                    this.ActiveSheet.Columns[columnPos].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Left;
                else if (aling == Align.Right)
                    this.ActiveSheet.Columns[columnPos].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Right;

                if (isFrozen == Frozen.True)
                    this.ActiveSheet.FrozenColumnCount++;

                if (isMerge == Merge.True)
                {
                    if (columnPos == 0 || this.ActiveSheet.Columns[columnPos - 1].MergePolicy == MergePolicy.None)
                        this.ActiveSheet.Columns[columnPos].MergePolicy = FarPoint.Win.Spread.Model.MergePolicy.Always;
                    else
                        this.ActiveSheet.Columns[columnPos].MergePolicy = FarPoint.Win.Spread.Model.MergePolicy.Restricted;
                    this.ActiveSheet.Columns[columnPos].VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Top;
                }
                else if (isMerge == Merge.Left)
                {
                    this.ActiveSheet.Columns[columnPos].MergePolicy = FarPoint.Win.Spread.Model.MergePolicy.Restricted;
                    this.ActiveSheet.Columns[columnPos].VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Top;
                }
                else if (isMerge == Merge.Always)
                {
                    this.ActiveSheet.Columns[columnPos].MergePolicy = FarPoint.Win.Spread.Model.MergePolicy.Always;
                    this.ActiveSheet.Columns[columnPos].VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Top;
                }

                // 셀타입 저장
                // AutoGernerationColumn 이 true 이 상황에서
                // 데이타 바인딩 전에 하면 실제로 적용이 안됨
                // 데이타 바인딩 이후에 실제로 셀타입을 지정하기 위하여
                //			_cellType.Add( getCellType( format ) );
                if (_cellType.Capacity < this.ActiveSheet.ColumnCount)
                    _cellType.Capacity++;
                _cellType.Insert(columnPos, getCellType(format));

				
                // ReadOnly 
                this.ActiveSheet.Columns[columnPos].Locked = true;
            }
            catch (Exception ex)
            {
                CmnFunction.ShowMsgBox(ex.Message);
            }
		}

		

		public void RPT_AddBasicColumn2(string header_Text, Visibles isVisible, Frozen isFrozen, Align aling, Merge isMerge, Formatter format, Boolean bLocked, int width)
		{
			int rowPos = (this.ActiveSheet.ColumnHeader.Rows.Count == 0 ? 0 : this.ActiveSheet.ColumnHeader.Rows.Count - 1);
			this.RPT_AddBasicColumn2(header_Text, rowPos, this.ActiveSheet.Columns.Count, isVisible, isFrozen, aling, isMerge, format, bLocked, width, null);
		}

		public void RPT_AddBasicColumn2(string header_Text, Visibles isVisible, Frozen isFrozen, Align aling, Merge isMerge, Formatter format, Boolean bLocked, int width, string header_Note)
		{
			int rowPos = (this.ActiveSheet.ColumnHeader.Rows.Count == 0 ? 0 : this.ActiveSheet.ColumnHeader.Rows.Count - 1);
			this.RPT_AddBasicColumn2(header_Text, rowPos, this.ActiveSheet.Columns.Count, isVisible, isFrozen, aling, isMerge, format, bLocked, width, header_Note);
		}
		//----------------

		public void RPT_AddBasicColumn2(string header_Text, int header_rowPos, int header_columnPos, Visibles isVisible, Frozen isFrozen, Align aling, Merge isMerge, Formatter format, Boolean bLocked, int width)
		{
			RPT_AddBasicColumn2(header_Text, header_rowPos, header_columnPos, isVisible, isFrozen, aling, isMerge, format, bLocked, width, null);
		}

		public void RPT_AddBasicColumn2(string header_Text, int header_rowPos, int header_columnPos, Visibles isVisible, Frozen isFrozen, Align aling, Merge isMerge, Formatter format, Boolean bLocked, int width, string header_Note)
		{
			/************************************************************
			 * comment : sheet에서 입력 가능을 위하여 option 추가
			 * 
			 * created by : bee-jae jung(2013-09-24-화요일)
			 * 
			 * modified by : bee-jae jung(2013-09-24-화요일)
			 ************************************************************/
			// 공통적으로 적용할 부분
			DataRow dtRow;
			try
			{
				if (!isFirstAdded)
				{
					isFirstAdded = true;
					this.ActiveSheet.Columns.Count = 0;
					this.ActiveSheet.DataAutoSizeColumns = false;
					this.ActiveSheet.FrozenColumnCount = 0;
					this.ActiveSheet.Rows.Count = 0;
					this._cellType.Clear();
				}

				int columnPos = header_columnPos;
				int rowPos = header_rowPos;

				// 컬럼 사이즈가 현재 사이즈 보다 큰경우 컬럼 사이즈 증가
				if (columnPos >= this.ActiveSheet.Columns.Count)
					this.ActiveSheet.Columns.Count = columnPos + 1;

				// row 사이즈가 현재 사이즈 보다 큰경우 row 사이즈 증가
				if (rowPos >= this.ActiveSheet.ColumnHeader.Rows.Count)
					this.ActiveSheet.ColumnHeader.Rows.Count = rowPos + 1;

				this.ActiveSheet.ColumnHeader.Cells.Get(rowPos, columnPos).Text = header_Text;

				//Spread의 Row가 0이면 오류 발생함.                
				//this.ActiveSheet.ColumnHeader.Cells.Get(rowPos, columnPos).Note = header_Text;
				//this.ActiveSheet.ColumnHeader.Cells.Get(rowPos, columnPos).NoteIndicatorSize = new System.Drawing.Size(7, 7);

				if (header_Note == null) header_Note = "";

				dtRow = dtNote.NewRow();
				dtRow["Row"] = rowPos;
				dtRow["Col"] = columnPos;
				dtRow["Note"] = header_Note;
				dtNote.Rows.Add(dtRow);

				if (isVisible == Visibles.True)
					this.ActiveSheet.Columns[columnPos].Visible = true;
				else
					this.ActiveSheet.Columns[columnPos].Visible = false;

				this.ActiveSheet.Columns[columnPos].Width = width;

				if (aling == Align.Center)
					this.ActiveSheet.Columns[columnPos].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
				else if (aling == Align.Left)
					this.ActiveSheet.Columns[columnPos].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Left;
				else if (aling == Align.Right)
					this.ActiveSheet.Columns[columnPos].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Right;

				if (isFrozen == Frozen.True)
					this.ActiveSheet.FrozenColumnCount++;

				if (isMerge == Merge.True)
				{
					if (columnPos == 0 || this.ActiveSheet.Columns[columnPos - 1].MergePolicy == MergePolicy.None)
						this.ActiveSheet.Columns[columnPos].MergePolicy = FarPoint.Win.Spread.Model.MergePolicy.Always;
					else
						this.ActiveSheet.Columns[columnPos].MergePolicy = FarPoint.Win.Spread.Model.MergePolicy.Restricted;
					this.ActiveSheet.Columns[columnPos].VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Top;
				}
				else if (isMerge == Merge.Left)
				{
					this.ActiveSheet.Columns[columnPos].MergePolicy = FarPoint.Win.Spread.Model.MergePolicy.Restricted;
					this.ActiveSheet.Columns[columnPos].VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Top;
				}
				else if (isMerge == Merge.Always)
				{
					this.ActiveSheet.Columns[columnPos].MergePolicy = FarPoint.Win.Spread.Model.MergePolicy.Always;
					this.ActiveSheet.Columns[columnPos].VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Top;
				}

				// 셀타입 저장
				// AutoGernerationColumn 이 true 이 상황에서
				// 데이타 바인딩 전에 하면 실제로 적용이 안됨
				// 데이타 바인딩 이후에 실제로 셀타입을 지정하기 위하여
				//			_cellType.Add( getCellType( format ) );
				if (_cellType.Capacity < this.ActiveSheet.ColumnCount)
					_cellType.Capacity++;
				_cellType.Insert(columnPos, getCellType(format));

				// 2013-09-24-정비재 : sheet의 데이터를 수정하기 위하여 추가함.
				// ReadOnly 
				this.ActiveSheet.Columns[columnPos].Locked = bLocked;
			}
			catch (Exception ex)
			{
				CmnFunction.ShowMsgBox(ex.Message);
			}
		}




		// Row 별로 Formatter Type 이 다른 경우 .. 
		public void RPT_RepeatableRowCellTypeChange( int startColumnPos, Formatter [] formatter )
		{
			RPT_RepeatableRowCellTypeChange ( startColumnPos, this.ActiveSheet.Columns.Count - startColumnPos, formatter );
		}

		// Row 별로 Formatter Type 이 다른 경우 .. 
		public void RPT_RepeatableRowCellTypeChange( int startColumnPos, int columnSize, Formatter [] formatter )
		{
			// 모든 row 반복
			for ( int i = 0; i < this.ActiveSheet.Rows.Count; i++ )
			{
				this.ActiveSheet.Cells [ i, startColumnPos, i, startColumnPos + columnSize -1 ].CellType = getCellType( formatter[ i%formatter.Length ]  );
				if ( formatter[ i%formatter.Length ] != Formatter.String && formatter[ i%formatter.Length ] != Formatter.Number )
					RPT_SetBlankFromZero ( i, startColumnPos, columnSize );
			}
		}
         
		// 헤더의 Column 을 size 만큼 병합 한다.
		public void RPT_MerageHeaderColumnSpan( string header_Text, int header_RowPos, int hader_ColumnPos, int size )
		{
			this.ActiveSheet.ColumnHeader.Cells [header_RowPos ,hader_ColumnPos].ColumnSpan = size;
			this.ActiveSheet.ColumnHeader.Cells [header_RowPos ,hader_ColumnPos].Text = header_Text;
		}

		public void RPT_MerageHeaderColumnSpan( int header_RowPos, int hader_ColumnPos, int size )
		{
            this.ActiveSheet.ColumnHeader.Cells [header_RowPos, hader_ColumnPos].ColumnSpan = size;
		}

        // 헤더의 row을 size 만큼 병합 한다.
        public void RPT_MerageHeaderRowSpan(string header_Text, int header_RowPos, int hader_ColumnPos, int size)
		{
			this.ActiveSheet.ColumnHeader.Cells [header_RowPos ,hader_ColumnPos].RowSpan = size;
			this.ActiveSheet.ColumnHeader.Cells [header_RowPos ,hader_ColumnPos].Text = header_Text;;
		}

		public void RPT_MerageHeaderRowSpan( int header_RowPos, int hader_ColumnPos, int size )
		{            
            this.ActiveSheet.ColumnHeader.Cells [header_RowPos ,hader_ColumnPos].RowSpan = size;
		}

        //특정 범위 만큼 컬럼을 반복하여 생성해줌..
        public void RPT_AddDynamicColumn(udcCUSCondition CusCondition, Visibles isVisible, Frozen isFrozen
                                         , Align aling, Merge isMerge, Formatter format, int size)
        {
            RPT_AddDynamicColumn(CusCondition, isVisible, isFrozen, aling, isMerge, format, size, ShowTip.False);
        }

        public void RPT_AddDynamicColumn(udcCUSCondition CusCondition, Visibles isVisible, Frozen isFrozen
                                         , Align aling, Merge isMerge, Formatter format, int size, ShowTip isShowTip)
        {
            ListView lvFrom = new ListView();
            string sHeaderNote = "";

            lvFrom = CusCondition.ValueGetListView;

            int count = 0;

            if (lvFrom.Items.Count == 0) return;
            
            for (int i = 0; i < lvFrom.Items.Count; i++)
            {
                if (lvFrom.Items[i].Checked == true)
                {
                    count++;
                }
            }

            if(count == 0)
            {
                for (int i = 0; i < lvFrom.Items.Count; i++)
                {
                    if (lvFrom.Items[i].Checked == false)
                    {
                        string Header = lvFrom.Items[i].Text;
                        
                        if (isShowTip == ShowTip.False)
                        {
                            this.RPT_AddBasicColumn(Header, isVisible, isFrozen, aling, isMerge, format, size, null);
                        }
                        else
                        {
                            if (lvFrom.Items[i].SubItems.Count >= 2)
                            {
                                sHeaderNote = lvFrom.Items[i].SubItems[1].Text;
                            }
                            else
                            {
                                sHeaderNote = lvFrom.Items[i].SubItems[0].Text;
                            }

                            this.RPT_AddBasicColumn(Header, isVisible, isFrozen, aling, isMerge, format, size, sHeaderNote);
                        }                        
                    }
                }
            }
            else
            {
                for (int i = 0; i < lvFrom.Items.Count; i++)
                {
                    if (lvFrom.Items[i].Checked == true)
                    {
                        string Header = lvFrom.Items[i].Text;
                        this.RPT_AddBasicColumn(Header, isVisible, isFrozen, aling, isMerge, format, size);
                    }
                }
            }           
        }

        // 특정 범위수 만큼 컬럼을 반복하여 생성해줌..
        // 특정 시작 컬럼에서부터 자동 증가함..
        public void RPT_AddDynamicColumn(udcCUSCondition CusCondition, int header_rowPos, int header_columnStartPos, Visibles isVisible
                                         , Frozen isFrozen, Align aling, Merge isMerge, Formatter format, int size)
        {
            RPT_AddDynamicColumn(CusCondition, header_rowPos, header_columnStartPos, isVisible, isFrozen, aling, isMerge, format, size, ShowTip.False);
        }

        public void RPT_AddDynamicColumn(udcCUSCondition CusCondition, int header_rowPos, int header_columnStartPos, Visibles isVisible
                                         , Frozen isFrozen, Align aling, Merge isMerge, Formatter format, int size, ShowTip isShowTip)
        {
            ListView lvFrom = new ListView();
            string sHeaderNote = "";

            lvFrom = CusCondition.ValueGetListView;
            int count = 0;

            if (lvFrom.Items.Count == 0) return;
            
            for (int i = 0; i < lvFrom.Items.Count; i++)
            {
                if (lvFrom.Items[i].Checked == true)
                {
                    count++;                
                }                
            }
            if(count ==0)
            {
                for (int i = 0; i < lvFrom.Items.Count; i++)
                {
                    if (lvFrom.Items[i].Checked == false)
                    {
                        string Header = lvFrom.Items[i].Text;
                        if (isShowTip == ShowTip.False)
                        {
                            this.RPT_AddBasicColumn(Header, header_rowPos, header_columnStartPos++, isVisible, isFrozen, aling, isMerge, format, size, null);
                        }
                        else
                        {
                            if (lvFrom.Items[i].SubItems.Count >= 2)
                            {
                                sHeaderNote = lvFrom.Items[i].SubItems[1].Text;
                            }
                            else
                            {
                                sHeaderNote = lvFrom.Items[i].SubItems[0].Text;
                            }

                            this.RPT_AddBasicColumn(Header, header_rowPos, header_columnStartPos++, isVisible, isFrozen, aling, isMerge, format, size, sHeaderNote);
                        }
                    }
                }
            }
            else
            {
                for (int i = 0; i < lvFrom.Items.Count; i++)
                {
                    if (lvFrom.Items[i].Checked == true)
                    {
                        string Header = lvFrom.Items[i].Text;
                        if (isShowTip == ShowTip.False)
                        {
                            this.RPT_AddBasicColumn(Header, header_rowPos, header_columnStartPos++, isVisible, isFrozen, aling, isMerge, format, size, null);
                        }
                        else
                        {
                            if (lvFrom.Items[i].SubItems.Count >= 2)
                            {
                                sHeaderNote = lvFrom.Items[i].SubItems[1].Text;
                            }
                            else
                            {
                                sHeaderNote = lvFrom.Items[i].SubItems[0].Text;
                            }

                            this.RPT_AddBasicColumn(Header, header_rowPos, header_columnStartPos++, isVisible, isFrozen, aling, isMerge, format, size, sHeaderNote);
                        }                        
                    }
                }
            }            
        }


        // 특정 범위수*반복횟수 만큼 컬럼을 반복하여 생성해줌..
        // 범위는 상단에 첫번째 헤더에  하단에 헤더에는 headers 반복만큼 증가함..
        // 특정 시작 컬럼에서부터 자동 증가함..
        public void RPT_AddDynamicColumn(udcCUSCondition CusCondition, string[] headers, int header_rowPos, int header_columnStartPos, Visibles isVisible, Frozen isFrozen, Align aling, Merge isMerge, Formatter format, int size)
        {
            Formatter[] formats = new Formatter[headers.Length];
            for (int i = 0; i < formats.Length; i++)
                formats[i] = format;

            RPT_AddDynamicColumn(CusCondition, headers, header_rowPos, header_columnStartPos, isVisible, isFrozen, aling, isMerge, formats, size);
        }

        // 특정 범위수*반복횟수 만큼 컬럼을 반복하여 생성해줌..
        // 범위는 상단에 첫번째 헤더에  하단에 헤더에는 headers 반복만큼 증가함..
        // 특정 시작 컬럼에서부터 자동 증가함..
        public void RPT_AddDynamicColumn(udcCUSCondition CusCondition, string[] headers, int header_rowPos, int header_columnStartPos, Visibles isVisible, Frozen isFrozen, Align aling, Merge isMerge, Formatter[] format, int size)
        {
            ListView lvFrom = new ListView();

            lvFrom = CusCondition.ValueGetListView;

            if (lvFrom.Items.Count == 0) return;

            // headers 와 format 의 사이즈가 동일해야함. 
            if (headers.Length != format.Length)
                throw new Exception(RptMessages.GetMessage("STD090", GlobalVariable.gcLanguage));

            int rowPos = header_rowPos;
            int columnPos = header_columnStartPos;
            int count = 0;
            // 동적으로 생기는 컬럼 생성

            for (int i = 0; i < lvFrom.Items.Count; i++)
            {
                if (lvFrom.Items[i].Checked == true)
                {
                    count++;
                }
            }

            if(count == 0)
            {
                for (int i = 0; i < lvFrom.Items.Count; i++)
                {
                    if (lvFrom.Items[i].Checked == false)
                    {
                        string Header = lvFrom.Items[i].Text;
                        for (int j = 0; j < headers.Length; j++)
                            this.RPT_AddBasicColumn(headers[j], rowPos + 1, columnPos++, isVisible, isFrozen, aling, isMerge, format[j], size, null);
                        // 상단에 범위 헤더 삽입
                        this.RPT_MerageHeaderColumnSpan(Header, rowPos, columnPos - headers.Length, headers.Length);
                    }
                }
            }
            else
            {
                for (int i = 0; i < lvFrom.Items.Count; i++)
                {
                    if (lvFrom.Items[i].Checked == true)
                    {
                        string Header = lvFrom.Items[i].Text;
                        for (int j = 0; j < headers.Length; j++)
                            this.RPT_AddBasicColumn(headers[j], rowPos + 1, columnPos++, isVisible, isFrozen, aling, isMerge, format[j], size, null);
                        // 상단에 범위 헤더 삽입
                        this.RPT_MerageHeaderColumnSpan(Header, rowPos, columnPos - headers.Length, headers.Length);
                    }
                }

            }                      
        }

        // CusCondition의 아이템으로 Column 추가시에 CusCondition의 SubItem까지 헤더 스트링에 포함되도록 두 번째 인자를 추가로 받음. 
        // PRD010502에서 사용하기 위해 오버로딩함. 2009.1.14 양형석
        public void RPT_AddDynamicColumn(udcCUSCondition CusCondition, bool withSubItemString, string[] headers, int header_rowPos, int header_columnStartPos, Visibles isVisible, Frozen isFrozen, Align aling, Merge isMerge, Formatter[] format, int size)
        {
            ListView lvFrom = new ListView();

            lvFrom = CusCondition.ValueGetListView;

            if (lvFrom.Items.Count == 0) return;

            // headers 와 format 의 사이즈가 동일해야함. 
            if (headers.Length != format.Length)
                throw new Exception(RptMessages.GetMessage("STD090", GlobalVariable.gcLanguage));

            int rowPos = header_rowPos;
            int columnPos = header_columnStartPos;
            int count = 0;
            // 동적으로 생기는 컬럼 생성

            for (int i = 0; i < lvFrom.Items.Count; i++)
            {
                if (lvFrom.Items[i].Checked == true)
                {
                    count++;
                }
            }

            if (count == 0)
            {
                for (int i = 0; i < lvFrom.Items.Count; i++)
                {
                    if (lvFrom.Items[i].Checked == false)
                    {
                        string Header = (withSubItemString == true) ? lvFrom.Items[i].Text + " (" + lvFrom.Items[i].SubItems[1].Text + ")" : lvFrom.Items[i].Text;
                        for (int j = 0; j < headers.Length; j++)
                            this.RPT_AddBasicColumn(headers[j], rowPos + 1, columnPos++, isVisible, isFrozen, aling, isMerge, format[j], size, null);
                        // 상단에 범위 헤더 삽입
                        this.RPT_MerageHeaderColumnSpan(Header, rowPos, columnPos - headers.Length, headers.Length);
                    }
                }
            }
            else
            {
                for (int i = 0; i < lvFrom.Items.Count; i++)
                {
                    if (lvFrom.Items[i].Checked == true)
                    {
                        string Header = (withSubItemString == true) ? lvFrom.Items[i].Text + " (" + lvFrom.Items[i].SubItems[1].Text + ")" : lvFrom.Items[i].Text;
                        for (int j = 0; j < headers.Length; j++)
                            this.RPT_AddBasicColumn(headers[j], rowPos + 1, columnPos++, isVisible, isFrozen, aling, isMerge, format[j], size, null);
                        // 상단에 범위 헤더 삽입
                        this.RPT_MerageHeaderColumnSpan(Header, rowPos, columnPos - headers.Length, headers.Length);
                    }
                }

            }
        }
		
        //Add by John Seo. 2008.11.25
        //특정 범위 만큼 컬럼을 반복하여 생성해줌..
        public void RPT_AddDynamicColumn(udcCUSFromToCondition FromToCondition, Visibles isVisible, Frozen isFrozen
                                       , Align aling, Merge isMerge, Formatter format, int size)
        {
            RPT_AddDynamicColumn(FromToCondition, isVisible, isFrozen, aling, isMerge, format, size, ShowTip.False);
        }
        public void RPT_AddDynamicColumn(udcCUSFromToCondition FromToCondition, Visibles isVisible, Frozen isFrozen
                                       , Align aling, Merge isMerge, Formatter format, int size, ShowTip isShowTip)
        {
            ListView lvFrom = new ListView();
            ListView lvTo = new ListView();
            string sHeaderNote = "";

            int iFromIdx = 0;
            int iToIdx = 0;

            lvFrom = FromToCondition.FromValueGetListView;
            lvTo = FromToCondition.ToValueGetListView;

            if (lvFrom.Items.Count == 0) return;
            if (lvTo.Items.Count == 0) return;

            //iFromIdx = lvFrom.Items.IndexOf(lvFrom.SelectedItems[0]);
            //iToIdx = lvTo.Items.IndexOf(lvTo.SelectedItems[0]);
            for (int i = 0; i < lvFrom.Items.Count; i++)
            {
                if (lvFrom.Items[i].Text == FromToCondition.FromText)
                {
                    iFromIdx = i;
                    break;
                }
            }

            for (int i = 0; i < lvTo.Items.Count; i++)
            {
                if (lvTo.Items[i].Text == FromToCondition.ToText)
                {
                    iToIdx = i;
                    break;
                }
            }

            // 동적으로 생기는 컬럼 생성            
            for (int i = iFromIdx; i <= iToIdx; i++)
            {
                string Header = lvFrom.Items[i].Text;
                if (isShowTip == ShowTip.False)
                {
                    this.RPT_AddBasicColumn(Header, isVisible, isFrozen, aling, isMerge, format, size, null);
                }
                else
                {
                    if (lvFrom.Items[i].SubItems.Count >= 2)
                    {
                        sHeaderNote = lvFrom.Items[i].SubItems[1].Text;
                    }
                    else
                    {
                        sHeaderNote = lvFrom.Items[i].SubItems[0].Text;
                    }

                    this.RPT_AddBasicColumn(Header, isVisible, isFrozen, aling, isMerge, format, size, sHeaderNote);
                }                
            }
        }

        // 특정 범위수 만큼 컬럼을 반복하여 생성해줌..
        // 특정 시작 컬럼에서부터 자동 증가함..
        public void RPT_AddDynamicColumn(udcCUSFromToCondition FromToCondition, int header_rowPos, int header_columnStartPos, Visibles isVisible
                                        , Frozen isFrozen, Align aling, Merge isMerge, Formatter format, int size)
        {
            RPT_AddDynamicColumn(FromToCondition, header_rowPos, header_columnStartPos, isVisible, isFrozen, aling, isMerge, format, size, ShowTip.False);
        }
        public void RPT_AddDynamicColumn(udcCUSFromToCondition FromToCondition, int header_rowPos, int header_columnStartPos, Visibles isVisible
                                        , Frozen isFrozen, Align aling, Merge isMerge, Formatter format, int size, ShowTip isShowTip)
        {
            ListView lvFrom = new ListView();
            ListView lvTo = new ListView();
            string sHeaderNote = "";

            int iFromIdx = 0;
            int iToIdx = 0;

            lvFrom = FromToCondition.FromValueGetListView;
            lvTo = FromToCondition.ToValueGetListView;

            if (lvFrom.Items.Count == 0) return;
            if (lvTo.Items.Count == 0) return;

            //iFromIdx = lvFrom.Items.IndexOf(lvFrom.SelectedItems[0]);
            //iToIdx = lvTo.Items.IndexOf(lvTo.SelectedItems[0]);
            for (int i = 0; i < lvFrom.Items.Count; i++)
            {
                if (lvFrom.Items[i].Text == FromToCondition.FromText)
                {
                    iFromIdx = i;
                    break;
                }
            }

            for (int i = 0; i < lvTo.Items.Count; i++)
            {
                if (lvTo.Items[i].Text == FromToCondition.ToText)
                {
                    iToIdx = i;
                    break;
                }
            }

            // 동적으로 생기는 컬럼 생성
            for (int i = iFromIdx; i <= iToIdx; i++)
            {
                string Header = lvFrom.Items[i].Text;                 

                if (isShowTip == ShowTip.False)
                {
                    this.RPT_AddBasicColumn(Header, header_rowPos, header_columnStartPos++, isVisible, isFrozen, aling, isMerge, format, size, null);
                }
                else
                {
                    if (lvFrom.Items[i].SubItems.Count >= 2)
                    {
                        sHeaderNote = lvFrom.Items[i].SubItems[1].Text;
                    }
                    else
                    {
                        sHeaderNote = lvFrom.Items[i].SubItems[0].Text;
                    }

                    this.RPT_AddBasicColumn(Header, header_rowPos, header_columnStartPos++, isVisible, isFrozen, aling, isMerge, format, size, sHeaderNote);
                }
                
            }
        }


        // 특정 범위수*반복횟수 만큼 컬럼을 반복하여 생성해줌..
        // 범위는 상단에 첫번째 헤더에  하단에 헤더에는 headers 반복만큼 증가함..
        // 특정 시작 컬럼에서부터 자동 증가함..
        public void RPT_AddDynamicColumn(udcCUSFromToCondition FromToCondition, string[] headers, int header_rowPos, int header_columnStartPos, Visibles isVisible
                                         , Frozen isFrozen, Align aling, Merge isMerge, Formatter format, int size)
        {
            Formatter[] formats = new Formatter[headers.Length];
            for (int i = 0; i < formats.Length; i++)
                formats[i] = format;

            RPT_AddDynamicColumn(FromToCondition, headers, header_rowPos, header_columnStartPos, isVisible, isFrozen, aling, isMerge, formats, size);
        }

        // 특정 범위수*반복횟수 만큼 컬럼을 반복하여 생성해줌..
        // 범위는 상단에 첫번째 헤더에  하단에 헤더에는 headers 반복만큼 증가함..
        // 특정 시작 컬럼에서부터 자동 증가함..        
        public void RPT_AddDynamicColumn(udcCUSFromToCondition FromToCondition, string[] headers, int header_rowPos, int header_columnStartPos, Visibles isVisible
                                         , Frozen isFrozen, Align aling, Merge isMerge, Formatter[] format, int size)
        {
            ListView lvFrom = new ListView();
            ListView lvTo = new ListView();
            
            int iFromIdx = 0;
            int iToIdx = 0;

            lvFrom = FromToCondition.FromValueGetListView;
            lvTo = FromToCondition.ToValueGetListView;
            
            if (lvFrom.Items.Count == 0) return;
            if (lvTo.Items.Count == 0) return;

            //iFromIdx = lvFrom.Items.IndexOf(lvFrom.SelectedItems[0]);
            //iToIdx = lvTo.Items.IndexOf(lvTo.SelectedItems[0]);
            for (int i = 0; i < lvFrom.Items.Count; i++)
            {
                if (lvFrom.Items[i].Text == FromToCondition.FromText)
                {
                    iFromIdx = i;
                    break;
                }
            }

            for (int i = 0; i < lvTo.Items.Count; i++)
            {
                if (lvTo.Items[i].Text == FromToCondition.ToText)
                {
                    iToIdx = i;
                    break;
                }
            }

            // headers 와 format 의 사이즈가 동일해야함. 
            if (headers.Length != format.Length)
                throw new Exception(RptMessages.GetMessage("STD090", GlobalVariable.gcLanguage));

            int rowPos = header_rowPos;
            int columnPos = header_columnStartPos;

            // 동적으로 생기는 컬럼 생성

            for (int i = iFromIdx; i <= iToIdx; i++)
            {
                string Header = lvFrom.Items[i].Text;
                 
                for (int j = 0; j < headers.Length; j++)
                    this.RPT_AddBasicColumn(headers[j], rowPos + 1, columnPos++, isVisible, isFrozen, aling, isMerge, format[j], size, null);
                                 
                // 상단에 범위 헤더 삽입
                this.RPT_MerageHeaderColumnSpan(Header, rowPos, columnPos - headers.Length, headers.Length);
            }
        }
		

		//특정 날짜수 만큼 컬럼을 반복하여 생성해줌..
        public void RPT_AddDynamicColumn(udcDurationDate duration, Visibles isVisible, Frozen isFrozen, Align aling, Merge isMerge, Formatter format, int size)
        {  
            // 동적으로 생기는 컬럼 생성
            if ( duration.DaySelector.SelectedValue.ToString().ToUpper() == "DAY" )
            {
                for ( int i = 0 ; i <= duration.SubtractBetweenFromToDate ; i++ )
                {
                    string Header = duration.FromDate.Value.AddDays(i).ToString("MM.dd");
                    this.RPT_AddBasicColumn( Header,		isVisible, isFrozen, aling, isMerge, format, size);
                }
            }
            else if ( duration.DaySelector.SelectedValue.ToString().ToUpper() == "WEEK" )
            {
                int thisWeek = Convert.ToInt32(duration.FromWeek.SelectedValue.ToString().Substring(4, 2));
                int thisMax = duration.FromWeek.Items.Count;
                int maxWeek = Convert.ToInt32(duration.ToWeek.SelectedValue.ToString().Substring(4, 2));
                string Header = null;

                //년도가 같을 경우
                if (duration.FromWeek.SelectedValue.ToString().Substring(0, 4) ==
                    duration.ToWeek.SelectedValue.ToString().Substring(0, 4))
                {
                    for (int i = thisWeek; i <= maxWeek; i++)
                    {
                        Header = "WW" + (i).ToString();

                        this.RPT_AddBasicColumn(Header, isVisible, isFrozen, aling, isMerge, format, size);
                    }
                }
                else  //다를 경우
                {
                    for (int i = thisWeek; i <= thisMax; i++)
                    {
                        Header = "WW" + (i).ToString();

                        this.RPT_AddBasicColumn(Header, isVisible, isFrozen, aling, isMerge, format, size);
                    }

                    for (int i = 1; i <= maxWeek; i++)
                    {
                        Header = "WW" + (i).ToString();

                        this.RPT_AddBasicColumn(Header, isVisible, isFrozen, aling, isMerge, format, size);
                    }
                }
            }
            else // 달인 경우
            {
                for ( int i = 0 ; i <= duration.SubtractBetweenFromToDate ; i++ )
                {
                    string Header = duration.FromYearMonth.Value.AddMonths(i).ToString("yy.MM");
                    this.RPT_AddBasicColumn( Header,		isVisible, isFrozen, aling, isMerge, format, size);
                }
            }             
             
        }

		// 특정 날짜수 만큼 컬럼을 반복하여 생성해줌..
		// 특정 시작 컬럼에서부터 자동 증가함..
        public void RPT_AddDynamicColumn(udcDurationDate duration, int header_rowPos, int header_columnStartPos, Visibles isVisible, Frozen isFrozen, Align aling, Merge isMerge, Formatter format, int size)
        {                          
            // 동적으로 생기는 컬럼 생성
            if (duration.DaySelector.SelectedValue.ToString().ToUpper() == "DAY")
            {
                for (int i = 0; i <= duration.SubtractBetweenFromToDate; i++)
                {
                    string Header = duration.FromDate.Value.AddDays(i).ToString("MM.dd");
                    this.RPT_AddBasicColumn(Header, header_rowPos, header_columnStartPos++, isVisible, isFrozen, aling, isMerge, format, size);
                }
            }
            else if (duration.DaySelector.SelectedValue.ToString().ToUpper() == "WEEK")
            {
                int thisWeek = Convert.ToInt32(duration.FromWeek.SelectedValue.ToString().Substring(4, 2));
                int thisMax = duration.FromWeek.Items.Count;
                int maxWeek = Convert.ToInt32(duration.ToWeek.SelectedValue.ToString().Substring(4, 2));
                string Header = null;

                //년도가 같을 경우
                if (duration.FromWeek.SelectedValue.ToString().Substring(0, 4) ==
                    duration.ToWeek.SelectedValue.ToString().Substring(0, 4))
                {
                    for (int i = thisWeek; i <= maxWeek; i++)
                    {
                        Header = "WW" + (i).ToString();

                        this.RPT_AddBasicColumn(Header, header_rowPos, header_columnStartPos++, isVisible, isFrozen, aling, isMerge, format, size);
                    }
                }
                else  //다를 경우
                {
                    for (int i = thisWeek; i <= thisMax; i++)
                    {
                        Header = "WW" + (i).ToString();

                        this.RPT_AddBasicColumn(Header, header_rowPos, header_columnStartPos++, isVisible, isFrozen, aling, isMerge, format, size);
                    }

                    for (int i = 1; i <= maxWeek; i++)
                    {
                        Header = "WW" + (i).ToString();

                        this.RPT_AddBasicColumn(Header, header_rowPos, header_columnStartPos++, isVisible, isFrozen, aling, isMerge, format, size);
                    }
                }
            }
            else // 달인 경우
            {
                for (int i = 0; i <= duration.SubtractBetweenFromToDate; i++)
                {
                    string Header = duration.FromYearMonth.Value.AddMonths(i).ToString("yy.MM");
                    this.RPT_AddBasicColumn(Header, header_rowPos, header_columnStartPos++, isVisible, isFrozen, aling, isMerge, format, size);
                }
            }
             
        }


		// 특정 날짜수*반복횟수 만큼 컬럼을 반복하여 생성해줌..
		// 날짜는 상단에 첫번째 헤더에  하단에 헤더에는 headers 반복만큼 증가함..
		// 특정 시작 컬럼에서부터 자동 증가함..
        public void RPT_AddDynamicColumn(udcDurationDate duration, string[] headers, int header_rowPos, int header_columnStartPos, Visibles isVisible, Frozen isFrozen, Align aling, Merge isMerge, Formatter format, int size)
        {
            Formatter[] formats = new Formatter[headers.Length];
            for (int i = 0; i < formats.Length; i++)
                formats[i] = format;

            RPT_AddDynamicColumn(duration, headers, header_rowPos, header_columnStartPos, isVisible, isFrozen, aling, isMerge, formats, size);
        }

		// 특정 날짜수*반복횟수 만큼 컬럼을 반복하여 생성해줌..
		// 날짜는 상단에 첫번째 헤더에  하단에 헤더에는 headers 반복만큼 증가함..
		// 특정 시작 컬럼에서부터 자동 증가함..
        public void RPT_AddDynamicColumn(udcDurationDate duration, string[] headers, int header_rowPos, int header_columnStartPos, Visibles isVisible, Frozen isFrozen, Align aling, Merge isMerge, Formatter[] format, int size)
        {

            // headers 와 format 의 사이즈가 동일해야함. 
            if (headers.Length != format.Length)
                throw new Exception(RptMessages.GetMessage("STD090", GlobalVariable.gcLanguage));

            int rowPos = header_rowPos;
            int columnPos = header_columnStartPos;

            // 동적으로 생기는 컬럼 생성
            if (duration.DaySelector.SelectedValue.ToString().ToUpper() == "DAY")
            {
                for (int i = 0; i <= duration.SubtractBetweenFromToDate; i++)
                {
                    string Header = duration.FromDate.Value.AddDays(i).ToString("MM.dd");
                    for (int j = 0; j < headers.Length; j++)
                        this.RPT_AddBasicColumn(headers[j], rowPos + 1, columnPos++, isVisible, isFrozen, aling, isMerge, format[j], size);
                    // 상단에 날짜 헤더 삽입
                    this.RPT_MerageHeaderColumnSpan(Header, rowPos, columnPos - headers.Length, headers.Length);
                }
            }
            else if (duration.DaySelector.SelectedValue.ToString().ToUpper() == "WEEK")
            {
                int thisWeek = Convert.ToInt32(duration.FromWeek.SelectedValue.ToString().Substring(4, 2));
                int thisMax = duration.FromWeek.Items.Count;
                int maxWeek = Convert.ToInt32(duration.ToWeek.SelectedValue.ToString().Substring(4, 2));
                string Header = null;

                //년도가 같을 경우
                if (duration.FromWeek.SelectedValue.ToString().Substring(0, 4) ==
                    duration.ToWeek.SelectedValue.ToString().Substring(0, 4))
                {  
                    for (int i = thisWeek; i <= maxWeek; i++)
                    {
                        Header = "WW" + (i).ToString();

                        for (int j = 0; j < headers.Length; j++)
                            this.RPT_AddBasicColumn(headers[j], rowPos + 1, columnPos++, isVisible, isFrozen, aling, isMerge, format[j], size);
                        // 상단에 날짜 헤더 삽입
                        this.RPT_MerageHeaderColumnSpan(Header, rowPos, columnPos - headers.Length, headers.Length);
                    }
                }
                else  //다를 경우
                {
                    for (int i = thisWeek; i <= thisMax; i++)
                    {
                        Header = "WW" + (i).ToString();

                        for (int j = 0; j < headers.Length; j++)
                            this.RPT_AddBasicColumn(headers[j], rowPos + 1, columnPos++, isVisible, isFrozen, aling, isMerge, format[j], size);
                        // 상단에 날짜 헤더 삽입
                        this.RPT_MerageHeaderColumnSpan(Header, rowPos, columnPos - headers.Length, headers.Length);
                    }

                    for (int i = 1; i <= maxWeek; i++)
                    {
                        Header = "WW" + (i).ToString();

                        for (int j = 0; j < headers.Length; j++)
                            this.RPT_AddBasicColumn(headers[j], rowPos + 1, columnPos++, isVisible, isFrozen, aling, isMerge, format[j], size);
                        // 상단에 날짜 헤더 삽입
                        this.RPT_MerageHeaderColumnSpan(Header, rowPos, columnPos - headers.Length, headers.Length);
                    }
                }
            }
            else // 달인 경우
            {
                for (int i = 0; i <= duration.SubtractBetweenFromToDate; i++)
                {
                    string Header = duration.FromYearMonth.Value.AddMonths(i).ToString("yy.MM");

                    for (int j = 0; j < headers.Length; j++)
                        this.RPT_AddBasicColumn(headers[j], rowPos + 1, columnPos++, isVisible, isFrozen, aling, isMerge, format[j], size);
                    // 상단에 날짜 헤더 삽입
                    this.RPT_MerageHeaderColumnSpan(Header, rowPos, columnPos - headers.Length, headers.Length);
                }
            }
        }

        #region udcDurationDatePlus 를 인자로 받는 오버로딩된 메서드
        //특정 날짜수 만큼 컬럼을 반복하여 생성해줌..
        public void RPT_AddDynamicColumn(udcDurationDatePlus duration, Visibles isVisible, Frozen isFrozen, Align aling, Merge isMerge, Formatter format, int size)
        {
            // 동적으로 생기는 컬럼 생성
            if (duration.DaySelector.SelectedValue.ToString().ToUpper() == "DAY")
            {
                for (int i = 0; i <= duration.SubtractBetweenFromToDate; i++)
                {
                    string Header = duration.FromDate.Value.AddDays(i).ToString("MM.dd");
                    this.RPT_AddBasicColumn(Header, isVisible, isFrozen, aling, isMerge, format, size);
                }
            }
            else if (duration.DaySelector.SelectedValue.ToString().ToUpper() == "WEEK")
            {
                int thisWeek = Convert.ToInt32(duration.FromWeek.SelectedValue.ToString().Substring(4, 2));
                int thisMax = duration.FromWeek.Items.Count;
                int maxWeek = Convert.ToInt32(duration.ToWeek.SelectedValue.ToString().Substring(4, 2));
                string Header = null;

                //년도가 같을 경우
                if (duration.FromWeek.SelectedValue.ToString().Substring(0, 4) ==
                    duration.ToWeek.SelectedValue.ToString().Substring(0, 4))
                {
                    for (int i = thisWeek; i <= maxWeek; i++)
                    {
                        Header = "WW" + (i).ToString();

                        this.RPT_AddBasicColumn(Header, isVisible, isFrozen, aling, isMerge, format, size);
                    }
                }
                else  //다를 경우
                {
                    for (int i = thisWeek; i <= thisMax; i++)
                    {
                        Header = "WW" + (i).ToString();

                        this.RPT_AddBasicColumn(Header, isVisible, isFrozen, aling, isMerge, format, size);
                    }

                    for (int i = 1; i <= maxWeek; i++)
                    {
                        Header = "WW" + (i).ToString();

                        this.RPT_AddBasicColumn(Header, isVisible, isFrozen, aling, isMerge, format, size);
                    }
                }
            }
            else // 달인 경우
            {
                for (int i = 0; i <= duration.SubtractBetweenFromToDate; i++)
                {
                    string Header = duration.FromYearMonth.Value.AddMonths(i).ToString("yy.MM");
                    this.RPT_AddBasicColumn(Header, isVisible, isFrozen, aling, isMerge, format, size);
                }
            }

        }

        // 특정 날짜수 만큼 컬럼을 반복하여 생성해줌..
        // 특정 시작 컬럼에서부터 자동 증가함..
        public void RPT_AddDynamicColumn(udcDurationDatePlus duration, int header_rowPos, int header_columnStartPos, Visibles isVisible, Frozen isFrozen, Align aling, Merge isMerge, Formatter format, int size)
        {
            // 동적으로 생기는 컬럼 생성
            if (duration.DaySelector.SelectedValue.ToString().ToUpper() == "DAY")
            {
                for (int i = 0; i <= duration.SubtractBetweenFromToDate; i++)
                {
                    string Header = duration.FromDate.Value.AddDays(i).ToString("MM.dd");
                    this.RPT_AddBasicColumn(Header, header_rowPos, header_columnStartPos++, isVisible, isFrozen, aling, isMerge, format, size);
                }
            }
            else if (duration.DaySelector.SelectedValue.ToString().ToUpper() == "WEEK")
            {
                int thisWeek = Convert.ToInt32(duration.FromWeek.SelectedValue.ToString().Substring(4, 2));
                int thisMax = duration.FromWeek.Items.Count;
                int maxWeek = Convert.ToInt32(duration.ToWeek.SelectedValue.ToString().Substring(4, 2));
                string Header = null;

                //년도가 같을 경우
                if (duration.FromWeek.SelectedValue.ToString().Substring(0, 4) ==
                    duration.ToWeek.SelectedValue.ToString().Substring(0, 4))
                {
                    for (int i = thisWeek; i <= maxWeek; i++)
                    {
                        Header = "WW" + (i).ToString();

                        this.RPT_AddBasicColumn(Header, header_rowPos, header_columnStartPos++, isVisible, isFrozen, aling, isMerge, format, size);
                    }
                }
                else  //다를 경우
                {
                    for (int i = thisWeek; i <= thisMax; i++)
                    {
                        Header = "WW" + (i).ToString();

                        this.RPT_AddBasicColumn(Header, header_rowPos, header_columnStartPos++, isVisible, isFrozen, aling, isMerge, format, size);
                    }

                    for (int i = 1; i <= maxWeek; i++)
                    {
                        Header = "WW" + (i).ToString();

                        this.RPT_AddBasicColumn(Header, header_rowPos, header_columnStartPos++, isVisible, isFrozen, aling, isMerge, format, size);
                    }
                }
            }
            else // 달인 경우
            {
                for (int i = 0; i <= duration.SubtractBetweenFromToDate; i++)
                {
                    string Header = duration.FromYearMonth.Value.AddMonths(i).ToString("yy.MM");
                    this.RPT_AddBasicColumn(Header, header_rowPos, header_columnStartPos++, isVisible, isFrozen, aling, isMerge, format, size);
                }
            }

        }


        // 특정 날짜수*반복횟수 만큼 컬럼을 반복하여 생성해줌..
        // 날짜는 상단에 첫번째 헤더에  하단에 헤더에는 headers 반복만큼 증가함..
        // 특정 시작 컬럼에서부터 자동 증가함..
        public void RPT_AddDynamicColumn(udcDurationDatePlus duration, string[] headers, int header_rowPos, int header_columnStartPos, Visibles isVisible, Frozen isFrozen, Align aling, Merge isMerge, Formatter format, int size)
        {
            Formatter[] formats = new Formatter[headers.Length];
            for (int i = 0; i < formats.Length; i++)
                formats[i] = format;

            RPT_AddDynamicColumn(duration, headers, header_rowPos, header_columnStartPos, isVisible, isFrozen, aling, isMerge, formats, size);
        }

        // 특정 날짜수*반복횟수 만큼 컬럼을 반복하여 생성해줌..
        // 날짜는 상단에 첫번째 헤더에  하단에 헤더에는 headers 반복만큼 증가함..
        // 특정 시작 컬럼에서부터 자동 증가함..
        public void RPT_AddDynamicColumn(udcDurationDatePlus duration, string[] headers, int header_rowPos, int header_columnStartPos, Visibles isVisible, Frozen isFrozen, Align aling, Merge isMerge, Formatter[] format, int size)
        {

            // headers 와 format 의 사이즈가 동일해야함. 
            if (headers.Length != format.Length)
                throw new Exception(RptMessages.GetMessage("STD090", GlobalVariable.gcLanguage));

            int rowPos = header_rowPos;
            int columnPos = header_columnStartPos;

            // 동적으로 생기는 컬럼 생성
            if (duration.DaySelector.SelectedValue.ToString().ToUpper() == "DAY")
            {
                for (int i = 0; i <= duration.SubtractBetweenFromToDate; i++)
                {
                    string Header = duration.FromDate.Value.AddDays(i).ToString("MM.dd");
                    for (int j = 0; j < headers.Length; j++)
                        this.RPT_AddBasicColumn(headers[j], rowPos + 1, columnPos++, isVisible, isFrozen, aling, isMerge, format[j], size);
                    // 상단에 날짜 헤더 삽입
                    this.RPT_MerageHeaderColumnSpan(Header, rowPos, columnPos - headers.Length, headers.Length);
                }
            }
            else if (duration.DaySelector.SelectedValue.ToString().ToUpper() == "WEEK")
            {
                int thisWeek = Convert.ToInt32(duration.FromWeek.SelectedValue.ToString().Substring(4, 2));
                int thisMax = duration.FromWeek.Items.Count;
                int maxWeek = Convert.ToInt32(duration.ToWeek.SelectedValue.ToString().Substring(4, 2));
                string Header = null;

                //년도가 같을 경우
                if (duration.FromWeek.SelectedValue.ToString().Substring(0, 4) ==
                    duration.ToWeek.SelectedValue.ToString().Substring(0, 4))
                {
                    for (int i = thisWeek; i <= maxWeek; i++)
                    {
                        Header = "WW" + (i).ToString();

                        for (int j = 0; j < headers.Length; j++)
                            this.RPT_AddBasicColumn(headers[j], rowPos + 1, columnPos++, isVisible, isFrozen, aling, isMerge, format[j], size);
                        // 상단에 날짜 헤더 삽입
                        this.RPT_MerageHeaderColumnSpan(Header, rowPos, columnPos - headers.Length, headers.Length);
                    }
                }
                else  //다를 경우
                {
                    for (int i = thisWeek; i <= thisMax; i++)
                    {
                        Header = "WW" + (i).ToString();

                        for (int j = 0; j < headers.Length; j++)
                            this.RPT_AddBasicColumn(headers[j], rowPos + 1, columnPos++, isVisible, isFrozen, aling, isMerge, format[j], size);
                        // 상단에 날짜 헤더 삽입
                        this.RPT_MerageHeaderColumnSpan(Header, rowPos, columnPos - headers.Length, headers.Length);
                    }

                    for (int i = 1; i <= maxWeek; i++)
                    {
                        Header = "WW" + (i).ToString();

                        for (int j = 0; j < headers.Length; j++)
                            this.RPT_AddBasicColumn(headers[j], rowPos + 1, columnPos++, isVisible, isFrozen, aling, isMerge, format[j], size);
                        // 상단에 날짜 헤더 삽입
                        this.RPT_MerageHeaderColumnSpan(Header, rowPos, columnPos - headers.Length, headers.Length);
                    }
                }
            }
            else // 달인 경우
            {
                for (int i = 0; i <= duration.SubtractBetweenFromToDate; i++)
                {
                    string Header = duration.FromYearMonth.Value.AddMonths(i).ToString("yy.MM");

                    for (int j = 0; j < headers.Length; j++)
                        this.RPT_AddBasicColumn(headers[j], rowPos + 1, columnPos++, isVisible, isFrozen, aling, isMerge, format[j], size);
                    // 상단에 날짜 헤더 삽입
                    this.RPT_MerageHeaderColumnSpan(Header, rowPos, columnPos - headers.Length, headers.Length);
                }
            }
        }
        #endregion

        #region udcDurationDate2 를 인자로 받는 오버로딩된 메서드
        //특정 날짜수 만큼 컬럼을 반복하여 생성해줌..
        public void RPT_AddDynamicColumn(udcDurationDate2 duration, Visibles isVisible, Frozen isFrozen, Align aling, Merge isMerge, Formatter format, int size)
        {
            // 동적으로 생기는 컬럼 생성
            if (duration.DaySelector.SelectedValue.ToString().ToUpper() == "DAY")
            {
                for (int i = 0; i <= duration.SubtractBetweenFromToDate; i++)
                {
                    string Header = duration.FromDate.Value.AddDays(i).ToString("MM.dd");
                    this.RPT_AddBasicColumn(Header, isVisible, isFrozen, aling, isMerge, format, size);
                }
            }
            else if (duration.DaySelector.SelectedValue.ToString().ToUpper() == "WEEK")
            {
                int thisWeek = Convert.ToInt32(duration.FromWeek.SelectedValue.ToString().Substring(4, 2));
                int thisMax = duration.FromWeek.Items.Count;
                int maxWeek = Convert.ToInt32(duration.ToWeek.SelectedValue.ToString().Substring(4, 2));
                string Header = null;

                //년도가 같을 경우
                if (duration.FromWeek.SelectedValue.ToString().Substring(0, 4) ==
                    duration.ToWeek.SelectedValue.ToString().Substring(0, 4))
                {
                    for (int i = thisWeek; i <= maxWeek; i++)
                    {
                        Header = "WW" + (i).ToString();

                        this.RPT_AddBasicColumn(Header, isVisible, isFrozen, aling, isMerge, format, size);
                    }
                }
                else  //다를 경우
                {
                    for (int i = thisWeek; i <= thisMax; i++)
                    {
                        Header = "WW" + (i).ToString();

                        this.RPT_AddBasicColumn(Header, isVisible, isFrozen, aling, isMerge, format, size);
                    }

                    for (int i = 1; i <= maxWeek; i++)
                    {
                        Header = "WW" + (i).ToString();

                        this.RPT_AddBasicColumn(Header, isVisible, isFrozen, aling, isMerge, format, size);
                    }
                }
            }
            else // 달인 경우
            {
                for (int i = 0; i <= duration.SubtractBetweenFromToDate; i++)
                {
                    string Header = duration.FromYearMonth.Value.AddMonths(i).ToString("yy.MM");
                    this.RPT_AddBasicColumn(Header, isVisible, isFrozen, aling, isMerge, format, size);
                }
            }

        }

        // 특정 날짜수 만큼 컬럼을 반복하여 생성해줌..
        // 특정 시작 컬럼에서부터 자동 증가함..
        public void RPT_AddDynamicColumn(udcDurationDate2 duration, int header_rowPos, int header_columnStartPos, Visibles isVisible, Frozen isFrozen, Align aling, Merge isMerge, Formatter format, int size)
        {
            // 동적으로 생기는 컬럼 생성
            if (duration.DaySelector.SelectedValue.ToString().ToUpper() == "DAY")
            {
                for (int i = 0; i <= duration.SubtractBetweenFromToDate; i++)
                {
                    string Header = duration.FromDate.Value.AddDays(i).ToString("MM.dd");
                    this.RPT_AddBasicColumn(Header, header_rowPos, header_columnStartPos++, isVisible, isFrozen, aling, isMerge, format, size);
                }
            }
            else if (duration.DaySelector.SelectedValue.ToString().ToUpper() == "WEEK")
            {
                int thisWeek = Convert.ToInt32(duration.FromWeek.SelectedValue.ToString().Substring(4, 2));
                int thisMax = duration.FromWeek.Items.Count;
                int maxWeek = Convert.ToInt32(duration.ToWeek.SelectedValue.ToString().Substring(4, 2));
                string Header = null;

                //년도가 같을 경우
                if (duration.FromWeek.SelectedValue.ToString().Substring(0, 4) ==
                    duration.ToWeek.SelectedValue.ToString().Substring(0, 4))
                {
                    for (int i = thisWeek; i <= maxWeek; i++)
                    {
                        Header = "WW" + (i).ToString();

                        this.RPT_AddBasicColumn(Header, header_rowPos, header_columnStartPos++, isVisible, isFrozen, aling, isMerge, format, size);
                    }
                }
                else  //다를 경우
                {
                    for (int i = thisWeek; i <= thisMax; i++)
                    {
                        Header = "WW" + (i).ToString();

                        this.RPT_AddBasicColumn(Header, header_rowPos, header_columnStartPos++, isVisible, isFrozen, aling, isMerge, format, size);
                    }

                    for (int i = 1; i <= maxWeek; i++)
                    {
                        Header = "WW" + (i).ToString();

                        this.RPT_AddBasicColumn(Header, header_rowPos, header_columnStartPos++, isVisible, isFrozen, aling, isMerge, format, size);
                    }
                }
            }
            else // 달인 경우
            {
                for (int i = 0; i <= duration.SubtractBetweenFromToDate; i++)
                {
                    string Header = duration.FromYearMonth.Value.AddMonths(i).ToString("yy.MM");
                    this.RPT_AddBasicColumn(Header, header_rowPos, header_columnStartPos++, isVisible, isFrozen, aling, isMerge, format, size);
                }
            }

        }


        // 특정 날짜수*반복횟수 만큼 컬럼을 반복하여 생성해줌..
        // 날짜는 상단에 첫번째 헤더에  하단에 헤더에는 headers 반복만큼 증가함..
        // 특정 시작 컬럼에서부터 자동 증가함..
        public void RPT_AddDynamicColumn(udcDurationDate2 duration, string[] headers, int header_rowPos, int header_columnStartPos, Visibles isVisible, Frozen isFrozen, Align aling, Merge isMerge, Formatter format, int size)
        {
            Formatter[] formats = new Formatter[headers.Length];
            for (int i = 0; i < formats.Length; i++)
                formats[i] = format;

            RPT_AddDynamicColumn(duration, headers, header_rowPos, header_columnStartPos, isVisible, isFrozen, aling, isMerge, formats, size);
        }

        // 특정 날짜수*반복횟수 만큼 컬럼을 반복하여 생성해줌..
        // 날짜는 상단에 첫번째 헤더에  하단에 헤더에는 headers 반복만큼 증가함..
        // 특정 시작 컬럼에서부터 자동 증가함..
        public void RPT_AddDynamicColumn(udcDurationDate2 duration, string[] headers, int header_rowPos, int header_columnStartPos, Visibles isVisible, Frozen isFrozen, Align aling, Merge isMerge, Formatter[] format, int size)
        {

            // headers 와 format 의 사이즈가 동일해야함. 
            if (headers.Length != format.Length)
                throw new Exception(RptMessages.GetMessage("STD090", GlobalVariable.gcLanguage));

            int rowPos = header_rowPos;
            int columnPos = header_columnStartPos;

            // 동적으로 생기는 컬럼 생성
            if (duration.DaySelector.SelectedValue.ToString().ToUpper() == "DAY")
            {
                for (int i = 0; i <= duration.SubtractBetweenFromToDate; i++)
                {
                    string Header = duration.FromDate.Value.AddDays(i).ToString("MM.dd");
                    for (int j = 0; j < headers.Length; j++)
                        this.RPT_AddBasicColumn(headers[j], rowPos + 1, columnPos++, isVisible, isFrozen, aling, isMerge, format[j], size);
                    // 상단에 날짜 헤더 삽입
                    this.RPT_MerageHeaderColumnSpan(Header, rowPos, columnPos - headers.Length, headers.Length);
                }
            }
            else if (duration.DaySelector.SelectedValue.ToString().ToUpper() == "WEEK")
            {
                int thisWeek = Convert.ToInt32(duration.FromWeek.SelectedValue.ToString().Substring(4, 2));
                int thisMax = duration.FromWeek.Items.Count;
                int maxWeek = Convert.ToInt32(duration.ToWeek.SelectedValue.ToString().Substring(4, 2));
                string Header = null;

                //년도가 같을 경우
                if (duration.FromWeek.SelectedValue.ToString().Substring(0, 4) ==
                    duration.ToWeek.SelectedValue.ToString().Substring(0, 4))
                {
                    for (int i = thisWeek; i <= maxWeek; i++)
                    {
                        Header = "WW" + (i).ToString();

                        for (int j = 0; j < headers.Length; j++)
                            this.RPT_AddBasicColumn(headers[j], rowPos + 1, columnPos++, isVisible, isFrozen, aling, isMerge, format[j], size);
                        // 상단에 날짜 헤더 삽입
                        this.RPT_MerageHeaderColumnSpan(Header, rowPos, columnPos - headers.Length, headers.Length);
                    }
                }
                else  //다를 경우
                {
                    for (int i = thisWeek; i <= thisMax; i++)
                    {
                        Header = "WW" + (i).ToString();

                        for (int j = 0; j < headers.Length; j++)
                            this.RPT_AddBasicColumn(headers[j], rowPos + 1, columnPos++, isVisible, isFrozen, aling, isMerge, format[j], size);
                        // 상단에 날짜 헤더 삽입
                        this.RPT_MerageHeaderColumnSpan(Header, rowPos, columnPos - headers.Length, headers.Length);
                    }

                    for (int i = 1; i <= maxWeek; i++)
                    {
                        Header = "WW" + (i).ToString();

                        for (int j = 0; j < headers.Length; j++)
                            this.RPT_AddBasicColumn(headers[j], rowPos + 1, columnPos++, isVisible, isFrozen, aling, isMerge, format[j], size);
                        // 상단에 날짜 헤더 삽입
                        this.RPT_MerageHeaderColumnSpan(Header, rowPos, columnPos - headers.Length, headers.Length);
                    }
                }
            }
            else // 달인 경우
            {
                for (int i = 0; i <= duration.SubtractBetweenFromToDate; i++)
                {
                    string Header = duration.FromYearMonth.Value.AddMonths(i).ToString("yy.MM");

                    for (int j = 0; j < headers.Length; j++)
                        this.RPT_AddBasicColumn(headers[j], rowPos + 1, columnPos++, isVisible, isFrozen, aling, isMerge, format[j], size);
                    // 상단에 날짜 헤더 삽입
                    this.RPT_MerageHeaderColumnSpan(Header, rowPos, columnPos - headers.Length, headers.Length);
                }
            }
        }
        #endregion

        // 특정 Row 수만큼 반복적으로 색을 전활 할때. 
		// 불특정 Column Merage 가 있을때 비추
		public void RPT_AddRowsColor( RowColorType color, int RowCount ,bool hasTotal )
		{
			this.RPT_AddRowsColor ( color, RowCount, 0, 0, hasTotal );
		}
                
		// 특정 Row 수만큼 반복적으로 색을 전활 할때. 
		// Merage 된 컬름을 다른 색으로 채울수 있음
		public void RPT_AddRowsColor( RowColorType color, int RowCount , int fixStart, int fixSize ,bool hasTotal )
		{
			
			if ( RowCount == 0 )
				throw new Exception(RptMessages.GetMessage("STD091", GlobalVariable.gcLanguage));

			if ( (fixStart + fixSize) > this.ActiveSheet.Columns.Count )
				throw new Exception(RptMessages.GetMessage("STD092", GlobalVariable.gcLanguage));
			
			// fix 색
//			System.Drawing.Color foreFix= System.Drawing.Color.Black;
			System.Drawing.Color backFix = System.Drawing.Color.White;

			// total 
//			System.Drawing.Color foreTotal = System.Drawing.Color.Black;
			System.Drawing.Color backTotal = System.Drawing.Color.White;
			
			// 첫번째 반복색
//			System.Drawing.Color foreFirst = System.Drawing.Color.Black;
			System.Drawing.Color backFirst = System.Drawing.Color.White;

			// 두번째 반복색
//			System.Drawing.Color foreSecond = System.Drawing.Color.Black;
			System.Drawing.Color backSecond =System.Drawing.Color.White;
			
			switch ( color )
			{
				case RowColorType.General:
					backFix = System.Drawing.Color.FromArgb(((System.Byte)(255)), ((System.Byte)(255)), ((System.Byte)(218)));
					backTotal = System.Drawing.Color.FromArgb(((System.Byte)(255)), ((System.Byte)(233)), ((System.Byte)(204)));
					backFirst = System.Drawing.Color.FromArgb(((System.Byte)(255)), ((System.Byte)(255)), ((System.Byte)(255)));
					backSecond = System.Drawing.Color.FromArgb(((System.Byte)(234)), ((System.Byte)(234)), ((System.Byte)(234)));
					break;
				case RowColorType.None:
					backFix = System.Drawing.Color.FromArgb(((System.Byte)(255)), ((System.Byte)(255)), ((System.Byte)(255)));
					backTotal = System.Drawing.Color.FromArgb(((System.Byte)(255)), ((System.Byte)(255)), ((System.Byte)(255)));
					backFirst = System.Drawing.Color.FromArgb(((System.Byte)(255)), ((System.Byte)(255)), ((System.Byte)(255)));
					backSecond = System.Drawing.Color.FromArgb(((System.Byte)(233)), ((System.Byte)(233)), ((System.Byte)(233)));
					break;
			}
			
			// 반복로우가 이 한개이면 이 부분을 태우면 더 빨라짐.. 
			// Row 별로 색상을 다시 적용할 필요가 없음으로. 
			if ( RowCount == 1 )
			{
				// fix 된 컬럼 만큼 반복
				for ( int i = fixStart ; i < fixSize; i++ )
				{
					this.ActiveSheet.Columns[i].BackColor = backFix;
				}
	
				
				// 토탈을 색칠함.. 
				if ( hasTotal )
					for ( int i = 0; i < RowCount; i++ )
						this.ActiveSheet.Rows[ i ].BackColor = backTotal;
	
				return;
			}
             
			// 토탈을 색칠함.. 
			if ( hasTotal )
				for ( int i = 0; i < RowCount; i++ )
					this.ActiveSheet.Rows[ i ].BackColor = backTotal;


			System.Drawing.Color currColor = backFirst;
			for ( int row = (hasTotal?RowCount:0) ; row < this.ActiveSheet.RowCount; row+=RowCount )
			{
				for ( int i = 0; i < RowCount; i++ )
				{
					this.ActiveSheet.Rows [ i + row ].BackColor = currColor;
				}		
	
				if ( currColor == backFirst )
					currColor = backSecond;
				else
					currColor = backFirst;
			}
			
//			ArrayList merge = new ArrayList(10);
			int merge = 0;
			for ( int column = fixStart  ; column < (fixStart + fixSize); column++ )
			{
				// 히든이면 색상칠하지 않음.. 속도를 위해서.. 
				if ( this.ActiveSheet.Columns[ column ].Visible == false )
					continue;
				
				if ( this.ActiveSheet.Columns[ column ].MergePolicy == MergePolicy.Always )
					merge = 1;
				else if ( this.ActiveSheet.Columns[ column ].MergePolicy == MergePolicy.None )
					merge = 2;
				else 
					merge = 3;

				this.ActiveSheet.Columns[ column ].MergePolicy = MergePolicy.None;


				object a = null;
				for ( int row = (hasTotal?RowCount:0) ; row < this.ActiveSheet.RowCount; row++ )
				{
					if ( a==this.ActiveSheet.Cells [ row, column].Value)
						continue;
					else
					{
						a = this.ActiveSheet.Cells [ row, column].Value;
						this.ActiveSheet.Cells [ row, column].BackColor = backFix;
					}
				}

				if ( merge == 1 )
					this.ActiveSheet.Columns[ column ].MergePolicy = MergePolicy.Always;
				else if ( merge == 2 )
					this.ActiveSheet.Columns[ column ].MergePolicy = MergePolicy.None;
				else 
					this.ActiveSheet.Columns[ column ].MergePolicy = MergePolicy.Restricted;

			}


		}


		// 반복적인 데이타를 이용하여 특정 컬름을 채우는 메소드
		public void RPT_FillColumnData( int columnPos, object [] datas )
		{
			if ( datas == null || datas.Length == 0 )
				throw new Exception(RptMessages.GetMessage("STD093", GlobalVariable.gcLanguage));

			if ( this.ActiveSheet.Columns.Count < columnPos )
				throw new Exception(RptMessages.GetMessage("STD094", GlobalVariable.gcLanguage));

			// 전체데이타를 반복
			for ( int k = 0; k < datas.Length; k++ ) 
			{
                // 2020-03-18-김미경 : 언어 변환
				object data  = LanguageFunction.FindLanguage(datas[k].ToString(), 0);
				for ( int i = k ; i < this.ActiveSheet.Rows.Count; i+=datas.Length )
				{
					this.ActiveSheet.Cells.Get( i, columnPos ).Value = data;                   
				}
			}
		}

        // 반복적인 데이타를 이용하여 특정 컬름을 채우는 메소드, 몇번째 ROW 부터 시작 하는지 인자를 받음
        public void RPT_FillColumnData(int columnPos, object[] datas, int row_num)
        {
            if (datas == null || datas.Length == 0)
                throw new Exception(RptMessages.GetMessage("STD093", GlobalVariable.gcLanguage));

            if (this.ActiveSheet.Columns.Count < columnPos)
                throw new Exception(RptMessages.GetMessage("STD094", GlobalVariable.gcLanguage));

            // 전체데이타를 반복
            for (int j=0,k = row_num; k < datas.Length; k++)
            {
                // 2020-03-18-김미경 : 언어 변환
                object data = LanguageFunction.FindLanguage(datas[j].ToString(), 0);
                for (int i = k; i < this.ActiveSheet.Rows.Count; i += datas.Length)
                {
                    this.ActiveSheet.Cells.Get(i, columnPos).Value = data;                   
                }
            }
        }


		//콤보박스 셀타입시 DataTable을 이용한 바인딩
		public void RPT_SetCombo(FpSpread aFpSpread, int row, int column, DataTable dt)
		{
			
			int iAreaCnt = 0;
			FarPoint.Win.Spread.CellType.ComboBoxCellType comboType = new FarPoint.Win.Spread.CellType.ComboBoxCellType();
			iAreaCnt = dt.Rows.Count;
		
			comboType.Items = new string[iAreaCnt];

			string[] sArea = new string[iAreaCnt];

			for(int i=0; i<iAreaCnt; i++)
				sArea[i] = dt.Rows[i][0].ToString(); 

			for(int i=0; i<iAreaCnt; i++)
				comboType.Items[i] = sArea[i];
			aFpSpread.Sheets[0].Cells[row, column].CellType = comboType;
			aFpSpread.Sheets[0].Cells[row, column].Text = comboType.Items[0].ToString(); 
		}

		// 필요한 부분을 특정 항목으로 채우는 메소드
		public void RPT_FillDataSelectiveCells(object values, int columnPos, int columnSize, int rowPos, int rowSize)
		{
			this.RPT_FillDataSelectiveCells( values, columnPos, columnSize, rowPos, rowSize, false, Align.Center, VerticalAlign.Center);
		}

		// 필요한 부분을 특정 항목으로 채우는 메소드
		// align, vAling 는 isMerage 가 true 인 경우만 통용
		public void RPT_FillDataSelectiveCells(object values, int columnPos, int columnSize, int rowPos, int rowSize, bool isMerage, Align align, VerticalAlign vAling)
		{
			if ( this.ActiveSheet.Rows.Count == 0 )
				return;

			if ( isMerage )
			{
				this.ActiveSheet.Cells.Get(  rowPos, columnPos ).ColumnSpan = columnSize;
				this.ActiveSheet.Cells.Get(  rowPos, columnPos ).RowSpan = rowSize;
				if ( align == Align.Center )
					this.ActiveSheet.Cells.Get(  rowPos, columnPos ).HorizontalAlignment = CellHorizontalAlignment.Center;
				else if ( align == Align.Left )
					this.ActiveSheet.Cells.Get(  rowPos, columnPos ).HorizontalAlignment = CellHorizontalAlignment.Left;
				else if ( align == Align.Right )
					this.ActiveSheet.Cells.Get(  rowPos, columnPos ).HorizontalAlignment = CellHorizontalAlignment.Right;

				if ( vAling == VerticalAlign.Center )
					this.ActiveSheet.Cells.Get(  rowPos, columnPos ).VerticalAlignment = CellVerticalAlignment.Center;
				else if ( vAling == VerticalAlign.Botton )
					this.ActiveSheet.Cells.Get(  rowPos, columnPos ).VerticalAlignment = CellVerticalAlignment.Bottom;
				else if ( vAling == VerticalAlign.Top )
					this.ActiveSheet.Cells.Get(  rowPos, columnPos ).VerticalAlignment = CellVerticalAlignment.Top;

			}

			this.ActiveSheet.Cells[ rowPos, columnPos, rowPos + rowSize - 1, columnPos + columnSize - 1 ].Value = values;
		}
		

		// 특정 셀에 공식을 더하는 메서드
		public void RPT_AddFormula( int rowPos, int columnPos, string formula )
		{
			RPT_AddFormula ( rowPos, 1, columnPos, 1, formula );
			RPT_SetBlankFromZero ( rowPos, columnPos, 1 );
		}

		// 특정 범위 셀에 공식을 더하는 메서드
		public void RPT_AddFormula ( int rowPos, int rowSize, int columnPos, int columnSize, string formula )
		{
			if ( rowPos < 0 || columnPos < 0 || rowSize < 1 || columnSize < 1 ||  rowPos + rowSize > this.ActiveSheet.Rows.Count || columnPos + columnSize > this.ActiveSheet.Columns.Count )
				throw new Exception(RptMessages.GetMessage("STD095", GlobalVariable.gcLanguage));

			this.ActiveSheet.Cells [ rowPos, columnPos, rowPos + rowSize - 1, columnPos + columnSize - 1 ].Formula = formula;
			
			for ( int i = rowPos; i < rowSize; i++ )
				RPT_SetBlankFromZero ( i, columnPos, columnSize );

		}
		  
		// 특정 컬럼의 특정 데이타 건수를 리턴함.. 
		public int RPT_GetCount ( int ValueColumnPos, object ValueColumnValue )
		{
			int Count = 0;

			for ( int i = 0; i < this.ActiveSheet.Rows.Count; i++ )
			{
				if ( this.ActiveSheet.Cells [ i, ValueColumnPos ].Value == null )
					continue;

				if ( this.ActiveSheet.Cells [ i, ValueColumnPos ].Value.ToString() == ValueColumnValue.ToString() )
					Count++;
			}

			return Count;
		}


        //public int [] RPT_GetSummaryRows ( int SummaryColumnPos )
        //{
        //    ArrayList list = new ArrayList();

        //    // 첫번째 row 는 무조건 토탈임으로 제거
        //    // 마지막 row 는 무조건 합계임..  따라서 검사할필요 없음
        //    for ( int i = 1; i < this.ActiveSheet.Rows.Count - 1; i++ )
        //    {
        //        if ( this.ActiveSheet.Cells[ i, SummaryColumnPos ].Text != this.ActiveSheet.Cells[ i + 1, SummaryColumnPos ].Text )
        //        {
        //            list.Add( i );
        //        }
        //    }
			
        //    //마지막 row 추가
        //    list.Add( this.ActiveSheet.Rows.Count - 1 );

        //    return (int [])list.ToArray( typeof(int));
        //}


		// 특정 범위를 Merge 타입으로 지정하는것.
		public void RPT_SetMerge ( int ColumnPos, int ColumnSize )
		{
			this.ActiveSheet.Columns[ ColumnPos ].MergePolicy = MergePolicy.Always;
			for ( int i = ColumnPos + 1; i < ColumnSize; i++ )
				this.ActiveSheet.Columns[ i ].MergePolicy = MergePolicy.Restricted;
		}


		// 컬럼 타입을 모를때 강제로 0 을 제거하는 메소드
		// ColumnPos 이후의 모든 컬럼에 적용
		public void RPT_SetToZero ( int ColumnPos )
		{
			for ( int i = ColumnPos; i < this.ActiveSheet.ColumnCount; i++ )
			{
				try
				{
					RPT_SetBlankFromZero( i );
				}
				catch(Exception){}
			}
		}
		
		/// <summary>
		///  부분 합계를 만들어줌.. DataSource 를 먼저 셋팅하면 안됌. 
		/// </summary>
		/// <param name="dt">원본 데이타 테이블</param>
		/// <param name="startSubTotalColumnPosition">부분합계를 시작할 컬럼..</param>
		/// <param name="SubTotalColumnSize">부분합계를 계산할 컬럼 사이즈</param>
		/// <param name="StartValueColumnPos">실제 계산할 데이타가 들어있는 컬럼</param>
		/// <returns>Row 타입 : 로우데이타는 1, 합계는 2, 각각의 SubTotal 은 +1 씩, 첫 부분합계 값은 3 부터..</returns>
		public int [] RPT_DataBindingWithSubTotal ( DataTable dt, int startSubTotalColumnPosition, int SubTotalColumnSize, int StartValueColumnPos )
		{
			return this.RPT_DataBindingWithSubTotal ( dt, startSubTotalColumnPosition, SubTotalColumnSize, StartValueColumnPos, null, null );
		}

		/// <summary>
		///  부분 합계를 만들어줌.. DataSource 를 먼저 셋팅하면 안됌. 
		/// </summary>
		/// <param name="dt">원본 데이타 테이블</param>
		/// <param name="startSubTotalColumnPosition">부분합계를 시작할 컬럼..</param>
		/// <param name="SubTotalColumnSize">부분합계를 계산할 컬럼 사이즈</param>
		/// <param name="StartValueColumnPos">실제 계산할 데이타가 들어있는 컬럼</param>
		/// <param name="Formula">실제 데이타에 넣을 공식</param>
		/// <param name="SumFormula">합계와 부분합계에만 넣을 공식.</param>
		/// <returns>Row 타입 : 로우데이타는 1, 합계는 2, 각각의 SubTotal 은 +1 씩, 첫 부분합계 값은 3 부터..</returns>
		public int [] RPT_DataBindingWithSubTotal ( DataTable dt, int startSubTotalColumnPosition, int SubTotalColumnSize, int StartValueColumnPos, string[,] Formula, string[,] SumFormula )
		{ 
			if ( dt == null )
				return null;

			if ( dt.Rows.Count == 0 )
			{
				this.DataSource = dt;
				return null;	
			}

			// 결과치가 담길 테이블.. 
			DataTable desc = new DataTable();
			// 로우데이타 구분이 담길 컬럼..
			int iColumnType = dt.Columns.Count;
			// 토탈값을 유지할 배열
			double [] Total = new double [ dt.Columns.Count - StartValueColumnPos ];
			// 서브 토탈값을 유지할 배열
			double [ , ] SubTotalValue = new double [ SubTotalColumnSize, dt.Columns.Count - StartValueColumnPos ];
			// 서브토탈의 기준이 되는 값
			string [] SubTotal = new string [ SubTotalColumnSize ];
			// 서브토탈이 중간부터 시작할때 상위의 기준값들..
			string [] Base = new string [ StartValueColumnPos ];
			DataRow rows = null;
			// 부분 합계용 데이타 row
			DataRow [] subTotalRows = new DataRow[ SubTotalColumnSize ];
			// 부분합계를 낼때 상단의 부분합계가 바뀌면 무조건 하단의 부분합계를 바꾸기위해.
			bool isSuperChanged;
			// 데이타 타입을 저장해놓을 장소
			string [] DataTypes = new string[ dt.Columns.Count ];
			// DataTable 을 Row별 { 일반, 합계, 부분합계 } 을 리턴할 배열
			int [] rowType;
			// 임시 변수  RoundValue 이하의 값은 버리기 위하여
			double temp = 0;
			
			// 컬럼 타입 생성
			for ( int i = 0; i < dt.Columns.Count; i++)
			{
				desc.Columns.Add( dt.Columns[i].ColumnName, dt.Columns[i].DataType );
				DataTypes[ i ] = dt.Columns[i].DataType.Name.ToUpper();
			}


			// 합계인지 로우 데이타인지 구분하기 위한 플래그..
			// 로우데이타는 1
			// 합계는 2
			// 각각의 SubTotal 은 +1 씩
            desc.Columns.Add("iDataRowType", typeof(int));

			// 토탈값
			DataRow TotalRow = desc.NewRow();

			// 서브토탈 값비교를 위한 값 미리 셋팅. 
			if ( dt.Rows.Count > 0 )
			{
				for ( int i = 0; i < SubTotalColumnSize ; i++ )
				{
					SubTotal[ i ] =  (dt.Rows [0][ startSubTotalColumnPosition +i ]==null?null:dt.Rows [0][ startSubTotalColumnPosition + i ].ToString());
				}
				// base 설정
				for ( int i = 0; i < Base.Length ; i++ )
				{
					Base[ i ] =  (dt.Rows [0][ i ]==null?null:dt.Rows [0][ i ].ToString());
				}

			}

			// 토탈 추가함.. 
			desc.Rows.Add( TotalRow );

			// 전체 row 을 순회함.. 
			for ( int row = 0; row <  dt.Rows.Count; row++ )
			{
				rows = desc.NewRow();

				isSuperChanged = false;

				// 전체 컬럼을 순회함.. 
				for ( int column = 0; column < dt.Columns.Count; column++ )
				{
					// 부분합계 계산 이전의 값들도 부분합계를 내기 위하여 검사. ( 중간부분부터 부분합계를 낼때 버그를 막기 위하여 )
					if ( column < startSubTotalColumnPosition )
					{
						if ( Base[ column ] != (dt.Rows [row][column]==null?null:((string)dt.Rows [row][column])) )
						{
							isSuperChanged = true;
							Base[ column ] = (dt.Rows [row][column]==null?null:dt.Rows [row][column].ToString());
						}
					}
					// 부분합계를 내기 위하여 이전것과 같은지 비교
					else if ( column >= startSubTotalColumnPosition && column < ( startSubTotalColumnPosition + SubTotalColumnSize )  )
					{
						// 자기자신이 틀리거나 상위의 부분합계가 변경된경우  하위에것은 무조건 변경. 
						if ( SubTotal[ column-startSubTotalColumnPosition ] != (dt.Rows [row][column]==null?null:((string)dt.Rows [row][column])) || isSuperChanged )
						{	
							subTotalRows[ column-startSubTotalColumnPosition ] = desc.NewRow();
							
							// 값을 서브토탈 row 에 복사하고 다시 누적 시작.. 하기 위하여 0 으로 초기화
							for ( int subValue = 0; subValue < ( dt.Columns.Count - StartValueColumnPos ) ; subValue++ )
							{
								// 서브토탈이 숫자인경우만 더해줌.. 
								if ( DataTypes[ StartValueColumnPos+subValue ] == "INT" || DataTypes[ StartValueColumnPos+subValue ] == "DOUBLE" ||
                                     DataTypes[StartValueColumnPos + subValue] == "INT32" || DataTypes[StartValueColumnPos + subValue] == "DECIMAL")
									subTotalRows[ column-startSubTotalColumnPosition ][ StartValueColumnPos+subValue ] = SubTotalValue [ column-startSubTotalColumnPosition, subValue ];

								SubTotalValue[ column-startSubTotalColumnPosition, subValue ] = 0;
								
							}

							// 부분합계 이전 값들을 체움. Merge 를 위해서. 
							if ( row != 0 )
							{
								for ( int i = 0; i < column+1  ; i++ )
								{
									subTotalRows[ column-startSubTotalColumnPosition ][ i ] = dt.Rows[ row -1 ][ i ];
								}
							}
                            subTotalRows[column - startSubTotalColumnPosition][column] = SubTotal[column - startSubTotalColumnPosition] + " Sub Total";
							
							SubTotal[ column-startSubTotalColumnPosition ] = (dt.Rows [row][column]==null?null:dt.Rows [row][column].ToString());

							isSuperChanged = true;							
						}
					}

					// StartValueColumnPos 이상의 컬럼들만 누적해나감.. 
					// 토탈및 서브토탈 값을 계산함..
					if ( column >= StartValueColumnPos )
					{
						// 숫자 타입인 서브토탈을 계산함
						if ( DataTypes[ column ] == "INT" || DataTypes[ column ] == "DOUBLE" ||
                             DataTypes[ column ] == "INT32" || DataTypes[column] == "DECIMAL")
						{
							// 토탈값 계산..
							if ( dt.Rows[ row ][ column ] != null && dt.Rows[ row ][ column ] != DBNull.Value )
							{
								Total [ column-StartValueColumnPos ] += Convert.ToDouble  ( dt.Rows[ row ][ column ]);
						
								//서브 토탈을 누적함. 
								for ( int sub = 0; sub < SubTotal.Length; sub++ )
								{
									SubTotalValue[ sub, column-StartValueColumnPos ] += Convert.ToDouble  ( dt.Rows[ row ][ column ]);
								}
							}
						}
					}

					
					// 일반 low data 복사
					// Double 타입은 속도를 위해서 0.009 이하의 값들은 버리고 null 값으로 셋팅함.
					if ( dt.Rows[ row ][ column ] != null && dt.Rows[ row ][ column ] != DBNull.Value )
						if ( DataTypes[ column ] == "DOUBLE")
						{
							temp = Convert.ToDouble  (  dt.Rows[ row ][ column ] ) ;
							if ( temp < RoundValue && temp > RoundValueMinus )
								rows [ column ] = DBNull.Value;
							else
								rows [ column ] = dt.Rows[ row ][ column ];
						}
						else
							rows [ column ] = dt.Rows[ row ][ column ];
					else
						rows [ column ] = dt.Rows[ row ][ column ];
				}
                				
				// 부분합계열 추가.. 
				// 역순으로 넣어야만.. 됨.. 
				for ( int sub = subTotalRows.Length - 1; sub >= 0; sub-- )
				{
					if ( subTotalRows[ sub ] != null )
					{
						// 데이타 타입을 입력함.. 
						// 로우데이타는 1
						// 합계는 2
						// 각각의 SubTotal 은 +1 씩, 첫 부분합계 값은 3 부터..
						subTotalRows[ sub ][ iColumnType ] = sub + 3;
						desc.Rows.Add( subTotalRows[ sub ] );
						subTotalRows[ sub ] = null;
					}
				}
                
				// 데이타 타입을 입력함.. 
				// 로우데이타는 1
				// 합계는 2
				// 각각의 SubTotal 은 +1 씩
				rows [ iColumnType ] = 1;

				desc.Rows.Add( rows );
			}

			// 토탈값을 채움.. 
			TotalRow[ 0 ] = "Total";
			// 데이타 타입을 입력함.. 
			// 로우데이타는 1
			// 합계는 2
			TotalRow[ iColumnType ] = 2;
			for ( int i = StartValueColumnPos; i < dt.Columns.Count; i++ )
			{
				// 숫자 타입인 서브토탈을 계산함
				if ( DataTypes[ i ] == "INT" || DataTypes[ i ] == "DOUBLE" ||
                     DataTypes[i] == "INT32" || DataTypes[i] == "DECIMAL")
					TotalRow[ i ] = Total[ i -StartValueColumnPos ];
			}
            
			// 마지막에 남아있는 부분합계 뒤에서부터 채움.. 
			for ( int subValue = SubTotal.Length-1 ; subValue >= 0 ; subValue-- )
			{
				DataRow subTotalRow = desc.NewRow();

				for ( int col = StartValueColumnPos ; col < dt.Columns.Count; col++ )
				{
					if ( DataTypes[ col ] == "INT" || DataTypes[ col ] == "DOUBLE" ||
                         DataTypes[col] == "INT32" || DataTypes[col] == "DECIMAL")
						subTotalRow[ col ] = SubTotalValue [ subValue, col-StartValueColumnPos ];
				}
				
				// 부분합계 이전 값들을 체움. 
				for ( int i = 0; i < (subValue+1+startSubTotalColumnPosition)  ; i++ )
				{
					subTotalRow[ i ] = dt.Rows[ dt.Rows.Count -1 ][ i ];
				}

				// 데이타 타입을 입력함.. 
				// 로우데이타는 1
				// 합계는 2
				// 각각의 SubTotal 은 +1 씩, 첫 부분합계 값은 3 부터..
				subTotalRow[ iColumnType ] = subValue + 3;
                subTotalRow[startSubTotalColumnPosition + subValue] = SubTotal[subValue] + " Sub Total";
				desc.Rows.Add( subTotalRow );
			}

			//
			// 이 이하는 FarPoint 전용 => 다른 그리드에서는 지워야함..
			//
			
			// 컬럼을 미리 초기화 하지 못하도록 막음. 
			isPreCellsType = false;

			int [] merge = new int[ StartValueColumnPos ];

			// Merge 정보를 기록함.. 
			for ( int i = 0; i < merge.Length ; i++ )
			{
				if ( this.ActiveSheet.Columns[ i ].MergePolicy == FarPoint.Win.Spread.Model.MergePolicy.Always )
					merge[i]  = 1;
				else if ( this.ActiveSheet.Columns[ i ].MergePolicy == FarPoint.Win.Spread.Model.MergePolicy.None )
					merge[i] = 2;
				else 
					merge[i] = 3;
				this.ActiveSheet.Columns[ i ].MergePolicy = FarPoint.Win.Spread.Model.MergePolicy.None;
			}
            
            // 그리드에 데이타 테이블 적용
			this.DataSource = desc;

			// 데이타가 없는 경우 나머지 초기화 하지 않음. 
			if ( this.ActiveSheet.RowCount == 0 )
				return null;

            //칼럼해더에 Note적용
            SetNoteInfo();

			// 나머지 초기화..
			this.ActiveSheet.FrozenRowCount = 1;
            // iDataRowType 타입은 제거 
			this.ActiveSheet.ColumnCount = desc.Columns.Count - 1;

			// 색상지정.. 
			// fix 된 컬럼 만큼 반복
			for ( int i = 0 ; i < StartValueColumnPos ; i++ )
				this.ActiveSheet.Columns[i].BackColor = System.Drawing.Color.FromArgb(((System.Byte)(255)), ((System.Byte)(255)), ((System.Byte)(218)));;
			// 토탈 컬러.
			this.ActiveSheet.Rows[ 0 ].BackColor = System.Drawing.Color.FromArgb(((System.Byte)(255)), ((System.Byte)(233)), ((System.Byte)(204)));

			// 합계 Formula 적용
			if ( SumFormula != null )
			{
				for ( int cel = 0; cel < SumFormula.Length/2 ; cel++ )
				{
					this.ActiveSheet.Cells [ 0, Convert.ToInt32(SumFormula[cel, 0]) ].Formula = SumFormula[cel, 1];                    
				}
			}

			// Row Type을 리턴할 함수..
			rowType = new int[ desc.Rows.Count ];
			rowType[ 0 ] = 2;	// 첫번째는 무조건 합계임.

			// Formula 적용( 부분합계 + Low 데이타 )
			// 부분합계 컬러
			for ( int i = 1; i < desc.Rows.Count; i++ )
			{
				rowType[ i ] = ((int)desc.Rows[ i ][ iColumnType ]);

				// 로우데이타와 합계는 제외하고 색칠함.. 
				if (  rowType[ i ] > 2 )
				{
					for ( int j = ( (int)startSubTotalColumnPosition + (int)desc.Rows[ i ][ iColumnType ] - 3 ); j < this.ActiveSheet.ColumnCount; j++ )
					{
						this.ActiveSheet.Cells [ i, j ].BackColor = System.Drawing.Color.FromArgb(((System.Byte)(222)), ((System.Byte)(236)), ((System.Byte)(242)));	

					}
					// 부분 합계부분 머지.. 
					this.ActiveSheet.Cells [ i, ( (int)startSubTotalColumnPosition + (int)desc.Rows[ i ][ iColumnType ] - 3 ) ].ColumnSpan = StartValueColumnPos -  ( (int)startSubTotalColumnPosition + (int)desc.Rows[ i ][ iColumnType ] - 3 );
					this.ActiveSheet.Cells [ i, ( (int)startSubTotalColumnPosition + (int)desc.Rows[ i ][ iColumnType ] - 3 ) ].VerticalAlignment = CellVerticalAlignment.Center;
					this.ActiveSheet.Cells [ i, ( (int)startSubTotalColumnPosition + (int)desc.Rows[ i ][ iColumnType ] - 3 ) ].HorizontalAlignment = CellHorizontalAlignment.Left;

					// 부분 합계 Formula 적용
					if ( SumFormula != null )
					{
						for ( int cel = 0; cel < SumFormula.Length/2 ; cel++ )
						{
							this.ActiveSheet.Cells [ i, Convert.ToInt32(SumFormula[cel, 0]) ].Formula = SumFormula[cel, 1];
						}
					}
				}
				else
				{
					// Low Data Formula 적용
					if ( Formula != null )
					{
						for ( int cel = 0; cel < Formula.Length/2 ; cel++ )
						{
							this.ActiveSheet.Cells [ i, Convert.ToInt32(Formula[cel, 0]) ].Formula = Formula[cel, 1];
						}
					}

				}

			}

            // 0값 표시 유무 설정 (기본은 표시 안함)
            //if (this.isShowZero != true)
            //{
                // 0 을 blank 로 바꿈..
                this.RPT_SetCellsType();
            //}

			isPreCellsType = true;

			// Merge 를 다시 켬.. 
			for ( int i = 0; i < merge.Length ; i++ )
			{
				if ( merge[i] == 1 )
					this.ActiveSheet.Columns[ i ].MergePolicy = FarPoint.Win.Spread.Model.MergePolicy.Always;
				else if ( merge[i] == 2 )
					this.ActiveSheet.Columns[ i ].MergePolicy = FarPoint.Win.Spread.Model.MergePolicy.None;
				else 
					this.ActiveSheet.Columns[ i ].MergePolicy = FarPoint.Win.Spread.Model.MergePolicy.Restricted;
			}	

			// 로우데이타는 1
			// 합계는 2
			// 각각의 SubTotal 은 +1 씩, 첫 부분합계 값은 3 부터..
			return rowType;
		}


        // 표구성일 이용하여 부분합계를 구성할때. 
        public int[] RPT_DataBindingWithSubTotal(DataTable dt, int startSubTotalColumnPosition, int SubTotalColumnSize, int StartValueColumnPos, string[,] Formula, string[,] SumFormula, udcButton mbt_Button)
        {
            int iVisibleCnt = 0;

            if (mbt_Button.AutoBindingMultiButtonType.ToString() != "S_Group")
            {
                CmnFunction.ShowMsgBox(RptMessages.GetMessage("STD037", GlobalVariable.gcLanguage));
                return null;
            }

            int[] rowType = this.RPT_DataBindingWithSubTotal(dt, startSubTotalColumnPosition, SubTotalColumnSize, StartValueColumnPos, Formula, SumFormula);

            if (rowType == null) return null;

            udcTableForm mbt = (udcTableForm)mbt_Button.BindingForm;
            ArrayList list = mbt.SelectedValueToArrayListToValuedTreeNode;

            for (int i = 0; i < list.Count; i++)
            {
                // 선택되지 않는 부분은 히든..
                if (((ValuedTreeNode)list[i]).Value == "FALSE")
                {
                    for (int j = 0; j < rowType.Length; j++)
                    {
                        if (rowType[j] == (i + 3))
                        {
                            this.ActiveSheet.Rows[j].Visible = false;
                        }
                    }
                }
                else
                {
                    iVisibleCnt++;
                }
            }

            this.ActiveSheet.Tag = iVisibleCnt + "^1";  //부부합계 머지할 칼럼주 저장.
            
            //선택된 부분의 제일 마지막 부분도 히든
            for (int i = list.Count - 1; i >= 0; i--)
            {
                if (((ValuedTreeNode)list[i]).Value == "TRUE")
                {
                    for (int j = 0; j < rowType.Length; j++)
                    {
                        if (rowType[j] == (i + 3))
                        {
                            this.ActiveSheet.Rows[j].Visible = false;
                        }
                    }

                    break;
                }
            }

            //히든된 Row 삭제
            for (int i = this.ActiveSheet.Rows.Count-1; i >=0 ; i--)
            {
                if (this.ActiveSheet.Rows[i].Visible==false)
                    this.ActiveSheet.Rows[i].Remove();
            } 

            return rowType;
        }


        // 표구성일 이용하여 부분합계를 구성할때. 
        public int[] RPT_DataBindingWithSubTotalAndDivideRows(DataTable dt, int startSubTotalColumnPosition, int SubTotalColumnSize, int StartValueColumnPos,
            int basisColumnSize, int divideRowsSize, int repeatColumnSize, udcButton mbt_Button)
        {
            if (mbt_Button.AutoBindingMultiButtonType.ToString() != "S_Group")
            {
                CmnFunction.ShowMsgBox(RptMessages.GetMessage("STD037", GlobalVariable.gcLanguage));
                return null;
            }

            int iVisibleCnt = 0;

            int[] rowType = this.RPT_DataBindingWithSubTotalAndDivideRows(dt, startSubTotalColumnPosition, SubTotalColumnSize, 
                                                      StartValueColumnPos, basisColumnSize, divideRowsSize, repeatColumnSize);

            if (rowType == null) return null;

            udcTableForm mbt = (udcTableForm)mbt_Button.BindingForm;
            ArrayList list = mbt.SelectedValueToArrayListToValuedTreeNode;

            for (int i = 0; i < list.Count; i++)
            {
                // 선택되지 않는 부분은 히든..
                if (((ValuedTreeNode)list[i]).Value == "FALSE")
                {
                    for (int j = 0; j < rowType.Length; j++)
                    {                        
                        if (rowType[j] == (i + 3))
                        {
                            for (int k = 0; k < divideRowsSize; k++)
                            {
                                this.ActiveSheet.Rows[(j*divideRowsSize) + k].Visible = false;
                            }
                        }                        
                    }
                }
                else
                {
                    iVisibleCnt++;
                }
            }

            this.ActiveSheet.Tag = iVisibleCnt + "^" + divideRowsSize;  //부부합계 머지할 칼럼주 저장.
            
            //선택된 부분의 제일 마지막 부분도 히든
            for (int i = list.Count - 1; i >= 0; i--)
            {
                if (((ValuedTreeNode)list[i]).Value == "TRUE")
                {
                    for (int j = 0; j < rowType.Length; j++)
                    {                         
                        if (rowType[j] == (i + 3))
                        {
                            for (int k = 0; k < divideRowsSize; k++)
                            {
                                this.ActiveSheet.Rows[(j * divideRowsSize) + k].Visible = false;
                            }
                        }                         
                    }

                    break;
                }
            }

            //히든된 Row 삭제
            for (int i = this.ActiveSheet.Rows.Count - 1; i >= 0; i--)
            {
                if (this.ActiveSheet.Rows[i].Visible == false)
                    this.ActiveSheet.Rows[i].Remove();
            }

            return rowType;
        }


        /// <summary>
		///  추가 : 2008.10.09. By 서영국
		///  2열 이상의 부분 합계를 만들어줌.. DataSource 를 먼저 셋팅하면 안됌. 
		/// </summary>
		/// <param name="dt">원본 데이타 테이블</param>
		/// <param name="startSubTotalColumnPosition">부분합계를 시작할 컬럼..</param>
		/// <param name="SubTotalColumnSize">부분합계를 계산할 컬럼 사이즈</param>
		/// <param name="StartValueColumnPos">실제 계산할 데이타가 들어있는 컬럼</param>
		/// <returns>Row 타입 : 로우데이타는 1, 합계는 2, 각각의 SubTotal 은 +1 씩, 첫 부분합계 값은 3 부터..</returns>
		public int [] RPT_DataBindingWithSubTotalAndDivideRows ( DataTable dt, int startSubTotalColumnPosition, int SubTotalColumnSize, int StartValueColumnPos,
			int basisColumnSize, int divideRowsSize, int repeatColumnSize )
		{

			//TIMES
			//int T1 = Common.clsUtility.getJustTime();

			if ( dt == null )
				return null;

			if ( dt.Rows.Count == 0 )
			{
				this.DataSource = dt;
				return null;	
			}

			// 결과치가 담길 테이블.. 
			DataTable desc = new DataTable();
			// 로우데이타 구분이 담길 컬럼..
			int iColumnType = dt.Columns.Count;
			// 토탈값을 유지할 배열
			double [] Total = new double [ dt.Columns.Count - StartValueColumnPos ];
			// 서브 토탈값을 유지할 배열
			double [ , ] SubTotalValue = new double [ SubTotalColumnSize, dt.Columns.Count - StartValueColumnPos ];
			// 서브토탈의 기준이 되는 값
			string [] SubTotal = new string [ SubTotalColumnSize ];
			// 서브토탈이 중간부터 시작할때 상위의 기준값들..
			string [] Base = new string [ StartValueColumnPos ];
			DataRow rows = null;
			// 부분 합계용 데이타 row
			DataRow [] subTotalRows = new DataRow[ SubTotalColumnSize ];
			// 부분합계를 낼때 상단의 부분합계가 바뀌면 무조건 하단의 부분합계를 바꾸기위해.
			bool isSuperChanged;
			// 데이타 타입을 저장해놓을 장소
			string [] DataTypes = new string[ dt.Columns.Count ];
			// DataTable 을 Row별 { 일반, 합계, 부분합계 } 을 리턴할 배열
			int [] rowType;

			// 컬럼 타입 생성
			for ( int i = 0; i < dt.Columns.Count; i++)
			{
				desc.Columns.Add( dt.Columns[i].ColumnName, dt.Columns[i].DataType );
				DataTypes[ i ] = dt.Columns[i].DataType.Name.ToUpper();
			}

			// 합계인지 로우 데이타인지 구분하기 위한 플래그..
			// 로우데이타는 1
			// 합계는 2
			// 각각의 SubTotal 은 +1 씩
			desc.Columns.Add("iDataRowType", typeof(int) );

			// 토탈값
			DataRow TotalRow = desc.NewRow();

			// 서브토탈 값비교를 위한 값 미리 셋팅. 
			if ( dt.Rows.Count > 0 )
			{
				for ( int i = 0; i < SubTotalColumnSize ; i++ )
				{
					SubTotal[ i ] =  (dt.Rows [0][ startSubTotalColumnPosition +i ]==null?null:dt.Rows [0][ startSubTotalColumnPosition + i ].ToString());
				}
				// base 설정
				for ( int i = 0; i < Base.Length ; i++ )
				{
					Base[ i ] =  (dt.Rows [0][ i ]==null?null:dt.Rows [0][ i ].ToString());
				}
			}

			// 토탈 추가함.. 
			desc.Rows.Add( TotalRow );

			// 전체 row 을 순회함.. 
			for ( int row = 0; row <  dt.Rows.Count; row++ )
			{
				rows = desc.NewRow();

				isSuperChanged = false;

				// 전체 컬럼을 순회함.. 
				for ( int column = 0; column < dt.Columns.Count; column++ )
				{
					// 부분합계 계산 이전의 값들도 부분합계를 내기 위하여 검사. ( 중간부분부터 부분합계를 낼때 버그를 막기 위하여 )
					if ( column < startSubTotalColumnPosition )
					{
						if ( Base[ column ] != (dt.Rows [row][column]==null?null:((string)dt.Rows [row][column])) )
						{
							isSuperChanged = true;
							Base[ column ] = (dt.Rows [row][column]==null?null:dt.Rows [row][column].ToString());
						}
					}
						// 부분합계를 내기 위하여 이전것과 같은지 비교
					else if ( column >= startSubTotalColumnPosition && column < ( startSubTotalColumnPosition + SubTotalColumnSize )  )
					{
						// 자기자신이 틀리거나 상위의 부분합계가 변경된경우  하위에것은 무조건 변경. 
						if ( SubTotal[ column-startSubTotalColumnPosition ] != (dt.Rows [row][column]==null?null:((string)dt.Rows [row][column])) || isSuperChanged )
						{	
							subTotalRows[ column-startSubTotalColumnPosition ] = desc.NewRow();
							
							// 값을 서브토탈 row 에 복사하고 다시 누적 시작.. 하기 위하여 0 으로 초기화
							for ( int subValue = 0; subValue < ( dt.Columns.Count - StartValueColumnPos ) ; subValue++ )
							{
								// 서브토탈이 숫자인경우만 더해줌.. 
								if ( DataTypes[ StartValueColumnPos+subValue ] == "INT" || DataTypes[ StartValueColumnPos+subValue ] == "DOUBLE" ||
                                     DataTypes[StartValueColumnPos + subValue] == "INT32" || DataTypes[StartValueColumnPos + subValue] == "DECIMAL")
									subTotalRows[ column-startSubTotalColumnPosition ][ StartValueColumnPos+subValue ] = SubTotalValue [ column-startSubTotalColumnPosition, subValue ];

								SubTotalValue[ column-startSubTotalColumnPosition, subValue ] = 0;								
							}

							// 부분합계 이전 값들을 체움. Merge 를 위해서. 
							if ( row != 0 )
							{
								for ( int i = 0; i < column+1  ; i++ )
								{
									subTotalRows[ column-startSubTotalColumnPosition ][ i ] = dt.Rows[ row -1 ][ i ];
								}
							}
                            subTotalRows[column - startSubTotalColumnPosition][column] = SubTotal[column - startSubTotalColumnPosition] + " Sub Total";
							
							SubTotal[ column-startSubTotalColumnPosition ] = (dt.Rows [row][column]==null?null:dt.Rows [row][column].ToString());

							isSuperChanged = true;							
						}
					}

					// StartValueColumnPos 이상의 컬럼들만 누적해나감.. 
					// 토탈및 서브토탈 값을 계산함..
					if ( column >= StartValueColumnPos )
					{
						// 숫자 타입인 서브토탈을 계산함
						if ( DataTypes[ column ] == "INT" || DataTypes[ column ] == "DOUBLE" ||
                             DataTypes[column] == "INT32" || DataTypes[column] == "DECIMAL")
						{
							// 토탈값 계산..
							if ( dt.Rows[ row ][ column ] != null && dt.Rows[ row ][ column ] != DBNull.Value )
							{
								Total [ column-StartValueColumnPos ] += Convert.ToDouble  ( dt.Rows[ row ][ column ]);
						
								//서브 토탈을 누적함. 
								for ( int sub = 0; sub < SubTotal.Length; sub++ )
								{
									SubTotalValue[ sub, column-StartValueColumnPos ] += Convert.ToDouble  ( dt.Rows[ row ][ column ]);
								}
							}
						}
					}

					// 일반 low data 복사
					// Double 타입은 속도를 위해서 0.009 이하의 값들은 버리고 null 값으로 셋팅함.
					if ( dt.Rows[ row ][ column ] != null && dt.Rows[ row ][ column ] != DBNull.Value )
						if ( DataTypes[ column ] == "DOUBLE")
							if ( Convert.ToDouble  (  dt.Rows[ row ][ column ] ) < RoundValue )
								rows [ column ] = DBNull.Value;
							else
								rows [ column ] = dt.Rows[ row ][ column ];
						else
							rows [ column ] = dt.Rows[ row ][ column ];
					else
						rows [ column ] = dt.Rows[ row ][ column ];

				}
				
				// 부분합계열 추가.. 
				// 역순으로 넣어야만.. 됨.. 
				for ( int sub = subTotalRows.Length - 1; sub >= 0; sub-- )
				{
					if ( subTotalRows[ sub ] != null )
					{
						// 데이타 타입을 입력함.. 
						// 로우데이타는 1
						// 합계는 2
						// 각각의 SubTotal 은 +1 씩, 첫 부분합계 값은 3 부터..
						subTotalRows[ sub ][ iColumnType ] = sub + 3;
						desc.Rows.Add( subTotalRows[ sub ] );
						subTotalRows[ sub ] = null;
					}
				}

				// 데이타 타입을 입력함.. 
				// 로우데이타는 1
				// 합계는 2
				// 각각의 SubTotal 은 +1 씩
				rows [ iColumnType ] = 1;

				desc.Rows.Add( rows );
			}

			// 토탈값을 채움.. 
			TotalRow[ 0 ] = "Total";
			// 데이타 타입을 입력함.. 
			// 로우데이타는 1
			// 합계는 2
			TotalRow[ iColumnType ] = 2;
			for ( int i = StartValueColumnPos; i < dt.Columns.Count; i++ )
			{
				// 숫자 타입인 서브토탈을 계산함
				if ( DataTypes[ i ] == "INT" || DataTypes[ i ] == "DOUBLE" ||
                     DataTypes[i] == "INT32" || DataTypes[i] == "DECIMAL")
					TotalRow[ i ] = Total[ i -StartValueColumnPos ];
			}

			// 마지막에 남아있는 부분합계 뒤에서부터 채움.. 
			for ( int subValue = SubTotal.Length-1 ; subValue >= 0 ; subValue-- )
			{
				DataRow subTotalRow = desc.NewRow();

				for ( int col = StartValueColumnPos ; col < dt.Columns.Count; col++ )
				{
					if ( DataTypes[ col ] == "INT" || DataTypes[ col ] == "DOUBLE" ||
                         DataTypes[col] == "INT32" || DataTypes[col] == "DECIMAL")
						subTotalRow[ col ] = SubTotalValue [ subValue, col-StartValueColumnPos ];
				}
				
				// 부분합계 이전 값들을 체움. 
				for ( int i = 0; i < (subValue+1+startSubTotalColumnPosition)  ; i++ )
				{
					subTotalRow[ i ] = dt.Rows[ dt.Rows.Count -1 ][ i ];
				}

				// 데이타 타입을 입력함.. 
				// 로우데이타는 1
				// 합계는 2
				// 각각의 SubTotal 은 +1 씩, 첫 부분합계 값은 3 부터..
				subTotalRow[ iColumnType ] = subValue + 3;
                subTotalRow[startSubTotalColumnPosition + subValue] = SubTotal[subValue] + " Sub Total";
				desc.Rows.Add( subTotalRow );
			}
			
			//
			// 이 이하는 FarPoint 전용 => 다른 그리드에서는 지워야함..
			//
			
			// 컬럼을 미리 초기화 하지 못하도록 막음. 
			isPreCellsType = false;

			int [] merge = new int[ StartValueColumnPos ];

			// Merge 정보를 기록함.. 
			for ( int i = 0; i < merge.Length ; i++ )
			{
				if ( this.ActiveSheet.Columns[ i ].MergePolicy == FarPoint.Win.Spread.Model.MergePolicy.Always )
					merge[i]  = 1;
				else if ( this.ActiveSheet.Columns[ i ].MergePolicy == FarPoint.Win.Spread.Model.MergePolicy.None )
					merge[i] = 2;
				else 
					merge[i] = 3;
				this.ActiveSheet.Columns[ i ].MergePolicy = FarPoint.Win.Spread.Model.MergePolicy.None;
			}

			// 그리드에 데이타 테이블 적용
			this.DataSource = desc;

            
			// 데이타가 없는 경우 나머지 초기화 하지 않음. 
			if ( this.ActiveSheet.RowCount == 0 )
				return null;

            //칼럼해더에 Note적용
            SetNoteInfo();

			// 나머지 초기화..
			this.ActiveSheet.FrozenRowCount = 1;
            // iDataRowType 타입은 제거 
			this.ActiveSheet.ColumnCount = desc.Columns.Count - 1;
            			
			this.RPT_DivideRows(basisColumnSize, divideRowsSize, repeatColumnSize );

			// 색상지정.. 
			// fix 된 컬럼 만큼 반복
			for ( int i = 0 ; i < StartValueColumnPos ; i++ )
				this.ActiveSheet.Columns[i].BackColor = System.Drawing.Color.FromArgb(((System.Byte)(255)), ((System.Byte)(255)), ((System.Byte)(218)));;
			// 토탈 컬러.
			for ( int k = 0; k < divideRowsSize; k++ )
				this.ActiveSheet.Rows[ 0+k ].BackColor = System.Drawing.Color.FromArgb(((System.Byte)(255)), ((System.Byte)(233)), ((System.Byte)(204)));

			// Row Type을 리턴할 함수..
			rowType = new int[ desc.Rows.Count ];
			rowType[ 0 ] = 2;	// 첫번째는 무조건 합계임.

			///////////////////////////////////////////////////////////////////////////////////
			// Formula 적용( 부분합계 + Low 데이타 )
			// 부분합계 컬러
			for ( int i = 1; i < desc.Rows.Count; i++ )
			{
				rowType[ i ] = ((int)desc.Rows[ i ][ iColumnType ]);

				// 로우데이타와 합계는 제외하고 색칠함.. 
				if (  rowType[ i ] > 2 )
				{
					for ( int j = ( (int)startSubTotalColumnPosition + (int)desc.Rows[ i ][ iColumnType ] - 3 ); j < this.ActiveSheet.ColumnCount; j++ )
					{
						for ( int k = 0; k < divideRowsSize; k++ )
							this.ActiveSheet.Cells [ i*divideRowsSize+k, j ].BackColor = System.Drawing.Color.FromArgb(((System.Byte)(222)), ((System.Byte)(236)), ((System.Byte)(242)));	

					}
						// 부분 합계부분 머지.. 
					this.ActiveSheet.Cells [ i*divideRowsSize, ( (int)startSubTotalColumnPosition + (int)desc.Rows[ i ][ iColumnType ] - 3 ) ].ColumnSpan = StartValueColumnPos -  ( (int)startSubTotalColumnPosition + (int)desc.Rows[ i ][ iColumnType ] - 3 );
					this.ActiveSheet.Cells [ i*divideRowsSize, ( (int)startSubTotalColumnPosition + (int)desc.Rows[ i ][ iColumnType ] - 3 ) ].VerticalAlignment = CellVerticalAlignment.Center;
					this.ActiveSheet.Cells [ i*divideRowsSize, ( (int)startSubTotalColumnPosition + (int)desc.Rows[ i ][ iColumnType ] - 3 ) ].HorizontalAlignment = CellHorizontalAlignment.Left;
					this.ActiveSheet.Cells [ i*divideRowsSize, ( (int)startSubTotalColumnPosition + (int)desc.Rows[ i ][ iColumnType ] - 3 ) ].RowSpan = divideRowsSize;
				}
			}
			///////////////////////////////////////////////////////////

			// 0 을 blank 로 바꿈..
			this.RPT_SetCellsType();

			isPreCellsType = true;

			// Merge 를 다시 켬.. 
			for ( int i = 0; i < merge.Length ; i++ )
			{
				if ( merge[i] == 1 )
					this.ActiveSheet.Columns[ i ].MergePolicy = FarPoint.Win.Spread.Model.MergePolicy.Always;
				else if ( merge[i] == 2 )
					this.ActiveSheet.Columns[ i ].MergePolicy = FarPoint.Win.Spread.Model.MergePolicy.None;
				else 
					this.ActiveSheet.Columns[ i ].MergePolicy = FarPoint.Win.Spread.Model.MergePolicy.Restricted;
			}	

			// 로우데이타는 1
			// 합계는 2
			// 각각의 SubTotal 은 +1 씩, 첫 부분합계 값은 3 부터..
			return rowType;
		}
        
        public void ExportExcel(FarPoint.Win.Spread.PrintInfo Pinfo)
        {
            if (this.ActiveSheet.Rows.Count == 0)
            {
                CmnFunction.ShowMsgBox(RptMessages.GetMessage("STD002", GlobalVariable.gcLanguage));
                return;
            }

            this.ActiveSheet.PrintInfo = Pinfo;
            this.ActiveSheet.Protect = false;

            SaveFileDialog saveFileDialog1 = new SaveFileDialog();
            saveFileDialog1.Filter = "Excel Files (*.xls)|*.xls";
            saveFileDialog1.Title = "Save Excel File";
            saveFileDialog1.FileName = GlobalVariable.gsSelFuncName.Replace(" ", "") + "_" + DateTime.Now.ToString("yyyyMMddhhmm");

            try
            {
                if (saveFileDialog1.ShowDialog() == DialogResult.OK)
                {
                    if (saveFileDialog1.FileName != "")
                    {
                        this.SaveExcel(saveFileDialog1.FileName, FarPoint.Win.Spread.Model.IncludeHeaders.BothCustomOnly);
                        this.ActiveSheet.Protect = true;


                        DialogResult result = MessageBox.Show("저장된 Excel 파일을 바로 Open 하시겠습니까?", "HANA Micron Web Report", MessageBoxButtons.OKCancel);
                        if (result == DialogResult.OK)
                        {
                            Process.Start(saveFileDialog1.FileName);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return;
            }
        }

        public void ExportExcel()
        {
            if (this.ActiveSheet.Rows.Count == 0)
            {
                CmnFunction.ShowMsgBox(RptMessages.GetMessage("STD002", GlobalVariable.gcLanguage));
                return;
            }
                        
            this.ActiveSheet.PrintInfo.Orientation = FarPoint.Win.Spread.PrintOrientation.Landscape;
            this.ActiveSheet.ColumnHeader.Visible = true;
            this.ActiveSheet.RowHeader.Visible = true;
            this.ActiveSheet.PrintInfo.ShowGrid = false;
            this.ActiveSheet.PrintInfo.ShowColor = false;
            this.ActiveSheet.PrintInfo.ZoomFactor = 100;
            this.ActiveSheet.PrintInfo.ShowBorder = false;
            this.ActiveSheet.PrintInfo.ShowRowHeader = FarPoint.Win.Spread.PrintHeader.Hide;
            this.ActiveSheet.PrintInfo.ShowColumnHeader = FarPoint.Win.Spread.PrintHeader.Hide;
            this.ActiveSheet.Protect = false;

            SaveFileDialog saveFileDialog1 = new SaveFileDialog();
            saveFileDialog1.Filter = "Excel Files (*.xls)|*.xls";
            saveFileDialog1.Title = "Save Excel File";
            saveFileDialog1.FileName = GlobalVariable.gsSelFuncName.Replace(" ", "") + "_" + DateTime.Now.ToString("yyyyMMddhhmm");

            try
            {
                if (saveFileDialog1.ShowDialog() == DialogResult.OK)
                {
                    if (saveFileDialog1.FileName != "")
                    {
                        this.SaveExcel(saveFileDialog1.FileName, FarPoint.Win.Spread.Model.IncludeHeaders.BothCustomOnly);
                        this.ActiveSheet.Protect = true;


                        DialogResult result = MessageBox.Show("저장된 Excel 파일을 바로 Open 하시겠습니까?", "HANA Micron Web Report", MessageBoxButtons.OKCancel);
                        if(result == DialogResult.OK)
                        {
                            Process.Start(saveFileDialog1.FileName);
                        }                        
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return;
            }
        }

        // 2010-09-03-임종우 : 엑셀저장 이름 사용자가 정의 하기 위해 추가함.
        public void ExportExcel(string title)
        {
            if (this.ActiveSheet.Rows.Count == 0)
            {
                CmnFunction.ShowMsgBox(RptMessages.GetMessage("STD002", GlobalVariable.gcLanguage));
                return;
            }

            this.ActiveSheet.PrintInfo.Orientation = FarPoint.Win.Spread.PrintOrientation.Landscape;
            this.ActiveSheet.ColumnHeader.Visible = true;
            this.ActiveSheet.RowHeader.Visible = true;
            this.ActiveSheet.PrintInfo.ShowGrid = false;
            this.ActiveSheet.PrintInfo.ShowColor = false;
            this.ActiveSheet.PrintInfo.ZoomFactor = 100;
            this.ActiveSheet.PrintInfo.ShowBorder = false;
            this.ActiveSheet.PrintInfo.ShowRowHeader = FarPoint.Win.Spread.PrintHeader.Hide;
            this.ActiveSheet.PrintInfo.ShowColumnHeader = FarPoint.Win.Spread.PrintHeader.Hide;
            this.ActiveSheet.Protect = false;

            SaveFileDialog saveFileDialog1 = new SaveFileDialog();
            saveFileDialog1.Filter = "Excel Files (*.xls)|*.xls";
            saveFileDialog1.Title = "Save Excel File";
            saveFileDialog1.FileName = title + "_" + DateTime.Now.ToString("yyyyMMddhhmm");

            try
            {
                if (saveFileDialog1.ShowDialog() == DialogResult.OK)
                {
                    if (saveFileDialog1.FileName != "")
                    {
                        this.SaveExcel(saveFileDialog1.FileName, FarPoint.Win.Spread.Model.IncludeHeaders.BothCustomOnly);
                        this.ActiveSheet.Protect = true;


                        DialogResult result = MessageBox.Show("저장된 Excel 파일을 바로 Open 하시겠습니까?", "HANA Micron Web Report", MessageBoxButtons.OKCancel);
                        if (result == DialogResult.OK)
                        {
                            Process.Start(saveFileDialog1.FileName);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return;
            }
        }

        /// <summary>
        /// 2017-12-15-임종우 : Sub Total, Grand Total 평균값 구하기
        /// </summary>
        /// <param name="nSampleNormalRowPos"></param>
        /// <param name="nColPos"></param>
        /// <param name="nGroupCount"></param>
        /// <param name="bWithNull"></param>
        public void RPT_SetAvgSubTotalAndGrandTotal(int nSampleNormalRowPos, int nColPos, int nGroupCount, bool bWithNull)
        {
            double sum = 0;
            double totalSum = 0;
            double subSum = 0; // 동일 분류의 서브 토탈의 합

            double divide = 0;
            double totalDivide = 0;
            double subDivide = 0; // 동일 분류의 서브 토탈 수량

            try
            {
                Color color = this.ActiveSheet.Cells[nSampleNormalRowPos, nColPos].BackColor;

                for (int i = 1; i < this.ActiveSheet.Rows.Count; i++)
                {
                    if (this.ActiveSheet.Cells[i, nColPos].BackColor == color)
                    {
                        sum += Convert.ToDouble(this.ActiveSheet.Cells[i, nColPos].Value);

                        // bWithNull 값이 true 이면 null, "" 값 포함하여 평균값 계산, false 이면 null, "" 값 제외하고 평균값 계산
                        if (!bWithNull && (this.ActiveSheet.Cells[i, nColPos].Value == null || this.ActiveSheet.Cells[i, nColPos].Value.ToString().Trim() == ""))
                            continue;

                        divide += 1;
                    }
                    else
                    {
                        if (divide != 0)
                        {

                            this.ActiveSheet.Cells[i, nColPos].Value = sum / divide;

                            if (nGroupCount > 2) // Group 항목에서 체크된게 2개 이상인것(서브토탈이 2 Depth 이상인것)
                            {
                                subSum += sum;
                                subDivide += divide;

                                if (this.ActiveSheet.Cells[i, nColPos].BackColor == this.ActiveSheet.Cells[i + 1, nColPos].BackColor)
                                {
                                    this.ActiveSheet.Cells[i + 1, nColPos].Value = subSum / subDivide;

                                    subSum = 0;
                                    subDivide = 0;
                                }
                            }

                        }

                        totalSum += sum;
                        totalDivide += divide;

                        sum = 0;
                        divide = 0;
                    }
                }

                if (totalDivide != 0)
                {
                    this.ActiveSheet.Cells[0, nColPos].Value = totalSum / totalDivide;
                }
                else if (divide != 0) // 만약 Subtotal 이 없을 경우
                {
                    this.ActiveSheet.Cells[0, nColPos].Value = sum / divide;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        public void RPT_SetPerSubTotalAndGrandTotal(int nSampleNormalRowPos, int nBunjaColPos, int nBunmoColPos, int nColPos)
        {
            double dBunja = 0;
            double dBunmo = 0;            
                
            try
            {
                Color color = this.ActiveSheet.Cells[nSampleNormalRowPos, nColPos].BackColor;

                // GrandTotal 부분
                dBunja = Convert.ToDouble(this.ActiveSheet.Cells[0, nBunjaColPos].Value);
                dBunmo = Convert.ToDouble(this.ActiveSheet.Cells[0, nBunmoColPos].Value);

                if (dBunmo > 0)
                {
                    this.ActiveSheet.Cells[0, nColPos].Value = dBunja / dBunmo * 100;
                }

                // SubTotal 부분
                for (int i = 1; i < this.ActiveSheet.Rows.Count; i++)
                {
                    if (this.ActiveSheet.Cells[i, nColPos].BackColor != color)
                    {
                        dBunja = Convert.ToDouble(this.ActiveSheet.Cells[i, nBunjaColPos].Value);
                        dBunmo = Convert.ToDouble(this.ActiveSheet.Cells[i, nBunmoColPos].Value);

                        if (dBunmo > 0)
                        {
                            this.ActiveSheet.Cells[i, nColPos].Value = dBunja / dBunmo * 100;
                        }
                    }                    
                }        
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
	}
}
