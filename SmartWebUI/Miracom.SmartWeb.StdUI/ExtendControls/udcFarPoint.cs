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

    //2010-06-07-������ : �Ҽ��� 4°�ڸ� ��� ���� Double4 �߰� ��.
	public enum Formatter { Number, Double1, Double2, Double3, Double4, String, Percent, PercentDouble, PercentDouble2, Image, ComboBox };
	public enum RowColorType { General, None };
    public enum ShowTip { True, False };

	/// <summary>
	/// udcFarPoint�� ���� ��� �����Դϴ�.
	/// </summary>
    /// �ۼ��� : ������(John)
    /// �ۼ��� : 2008.09.25
    
	public class udcFarPoint : FarPoint.Win.Spread.FpSpread
	{
		/// <summary>
		/// �ʼ� �����̳� �����Դϴ�.
		/// </summary>
		private System.ComponentModel.Container components = null;
		
		// ǥ�������� �÷� ������ �����ϱ� ���Ͽ�
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

        //2010-06-07-������ : �Ҽ��� 4°�ڸ� �߰� ��.
        private static FarPoint.Win.Spread.CellType.NumberCellType _double4;

		private static FarPoint.Win.Spread.CellType.PercentCellType _percent;
		private static FarPoint.Win.Spread.CellType.PercentCellType _percentDouble;
		private static FarPoint.Win.Spread.CellType.PercentCellType _percentDouble2;
		private static FarPoint.Win.Spread.CellType.ImageCellType _image;
		private static FarPoint.Win.Spread.CellType.ComboBoxCellType _ComboBox;
		private bool isFirstAdded;
		// ��Ÿ���� �����Ҵ°��� �������� �ϰ� ������ false �ָ� ��..
		private bool isPreCellsType = true;

		// �̰� ���ϴ� blank �� ����.
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
					// AutoGernerationColumn �� true �� ��Ȳ����
					// ����Ÿ ���ε� ���� �ϸ� ������ ������ �ȵ�
					// ����Ÿ ���ε� ���Ŀ� ������ ��Ÿ���� �����ϱ� ���Ͽ�
					RPT_SetCellsType();
				}
			}
		}

		// �ڵ����� �����ߴ� �Լ�..
		public void RPT_AutoCellLineColor()
		{
			this.RPT_AutoCellLineColor( -1 );
		}
			
		// �ڵ����� �����ߴ� �Լ�..
		// MergeSize ���� ���� ������
		// -1 �� ����Ʈ�� �ڵ����. 
		public void RPT_AutoCellLineColor( int MergeSize )
		{
			if ( this.ActiveSheet.RowCount < 1 )
				return;

			System.Drawing.Color LineColor = System.Drawing.Color.FromArgb(111, 117, 234);

			// ���μ� �߱�..
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

			// Merge  ù��° �κ� ���� 
			for ( int j = 0; j < MergeSize ; j++ )
				this.ActiveSheet.Cells[ 0, j ].Border = new FarPoint.Win.LineBorder( LineColor, 1, false, false, false, true);

			for ( int i = 0; i < this.ActiveSheet.RowCount-1 ; i++ )
			{
	
				// Merge �Ȱ��� ����Ǿ����� �˻�. 
				for ( int j = 0; j < MergeSize ; j++ )
				{
					if ( this.ActiveSheet.Columns[ j ].Visible == false )
						continue;

					if ( this.ActiveSheet.Cells[ i , j ].Text != this.ActiveSheet.Cells[ i+1 , j ].Text )
					{
						Lined = true;
						// Merge �� �κ� ���� 
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

				// ��ĥ�ϱ�
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

			// Merge  �κ� ������ ����
			if ( MergeSize > 0 )
			{
				rowLine [ this.ActiveSheet.RowCount-1 ] = 1;
				this.ActiveSheet.Rows[ this.ActiveSheet.RowCount-1 ].Border = new FarPoint.Win.LineBorder( LineColor, 1, false, false, false, true);
			}

			// ���μ� �߱�..
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

        // �ڵ����� Į���� ���̸� �����ϴ� �Լ�.
        public void RPT_AutoFit(bool bIgnoreHeaders)
        {
            for (int i = 0; i < this.ActiveSheet.Columns.Count; i++)
            {
               float width_max = this.ActiveSheet.GetPreferredColumnWidth(i,bIgnoreHeaders);
                this.ActiveSheet.Columns[i].Width = width_max;
            }
        }

        // ��Ÿ���� ���� ���ֱ� ���Ͽ�
        // ������ �ڵ����� ��..
        // RptSpreadUtil �� �� ��츸 ��.. 
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
        /// Į���� ��� 0�̰ų� �����ΰ�� �ش� Į���� ����.
        /// </summary>
        /// <param name="iStartColPosition">���ںκ��� ����Į�� ��ġ(0���� ������)</param>        
        /// <param name="iTotalRowSize">���հ谡 ������� ���հ� Row Size. �հ谡 ������� 0���� �Է�.</param>
        public void RPT_RemoveZeroColumn(int iStartColPosition, int iTotalRowSize)
        {
            RPT_RemoveZeroColumn(iStartColPosition, this.ActiveSheet.Columns.Count-1 , iTotalRowSize);
        }

        /// <summary>
        /// Į���� ��� 0�̰ų� �����ΰ�� �ش� Į���� ����.
        /// </summary>
        /// <param name="iStartColPosition">���ںκ��� ����Į�� ��ġ(0���� ������)</param>
        /// <param name="iEndColPosition">���ںκ��� ��Į�� ��ġ(0���� ������)</param>
        /// <param name="iTotalRowSize">���հ谡 ������� ���հ� Row Size. �հ谡 ������� 0���� �Է�.</param>
        public void RPT_RemoveZeroColumn(int iStartColPosition, int iEndColPosition, int iTotalRowSize)
        { 
            try
            {
                double dTmpTotal = 0.0;
                int iEndRow = 0;

                if (iTotalRowSize == 0) //Total�� ���°�� ��ü Row�� üũ��.                    
                    iEndRow = this.ActiveSheet.Rows.Count;
                else
                    iEndRow = iTotalRowSize;

                for (int j = iEndColPosition; j >= iStartColPosition; j--)
                {   
                    dTmpTotal = 0;
                    
                    //�հ谡 �ִ°�� �հ�Į���� ���� 0�̸� �����Ѵ�.                        
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

		// �÷� Ÿ���� ���� ������ ��� 0 �̸� �� ������ �������� ó����..  
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

		// �÷� Ÿ���� ���� ������ Ư�� row�� 0�� ��ĭ���� ����� ������ 
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

        
		// CellType �� ���� ������ ���� �κ�
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

            // 2010-06-07-������ : �Ҽ��� 4°�ڸ� ǥ�ø� ���� �߰���.
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

			// 2013-09-24-������ : combobox�� ����ϱ� ���Ͽ� �߰���.
			if (_ComboBox == null)
			{
				_ComboBox = new FarPoint.Win.Spread.CellType.ComboBoxCellType();
				_ComboBox.ButtonAlign  = FarPoint.Win.ButtonAlign.Right;
			}
		}

		// ��Ÿ���� �������ִ� �޼ҵ�
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

		// ǥ�������κ��� �׸��� �÷��� Visual ���θ� ����
        public void RPT_ColumnConfigFromTable(udcButton Mbt)
        {
            this.RPT_ColumnConfigFromTable(Mbt, this.ActiveSheet.ColumnHeader.Rows.Count - 1);
        }

		// ǥ�������κ��� �׸��� �÷��� Visual ���θ� ����
		// rowPos �� ��� �ؽ�Ʈ�� ���� �÷�
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
                

                // ǥ������ ó������ ���� ��� �÷� Ÿ���� �̸������س��� ������ ���� ��������
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
                        // �÷��� ������ �����ϱ� ���Ͽ�
                        if (isFirstColumn == false)
                        {
                            isFirstColumn = true;
                            // ù��° ����� Restricted �̸� ������ �ȵ�
                            if (this.ActiveSheet.Columns[i].MergePolicy == MergePolicy.Restricted)
                                this.ActiveSheet.Columns[i].MergePolicy = MergePolicy.Always;
                        }
                        else
                        {
                            // �ι�° ������ʹ� Always �̸� ������ �̻���.. 
                            if (this.ActiveSheet.Columns[i].MergePolicy == MergePolicy.Always)
                                this.ActiveSheet.Columns[i].MergePolicy = MergePolicy.Restricted;
                        }

                        this.ActiveSheet.Columns[i].Visible = true;
                        this.ActiveSheet.ColumnHeader.Cells.Get(0, i).Text = node.Text;

                        //�ش� Į�� ���� ����.
                        for (int k = 1; k < this.ActiveSheet.ColumnHeader.RowCount ; k++)
                            this.ActiveSheet.ColumnHeader.Cells.Get(k, i).Text = node.Text;

                        // �÷� ������ ������.
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


        // sortinit() �� ������ ��� �� ��� by MgKim 
        public void RPT_ColumnConfigFromTable_New(udcButton Mbt)
        {
            this.RPT_ColumnConfigFromTable_New(Mbt, this.ActiveSheet.ColumnHeader.Rows.Count - 1);
        }

        // sortinit() �� ������ ��� �� ��� by MgKim
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

                // ǥ������ ó������ ���� ��� �÷� Ÿ���� �̸������س��� ������ ���� ��������
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
                        // �÷��� ������ �����ϱ� ���Ͽ�
                        if (isFirstColumn == false)
                        {
                            isFirstColumn = true;
                            // ù��° ����� Restricted �̸� ������ �ȵ�
                            if (this.ActiveSheet.Columns[i].MergePolicy == MergePolicy.Restricted)
                                this.ActiveSheet.Columns[i].MergePolicy = MergePolicy.Always;
                        }
                        else
                        {
                            // �ι�° ������ʹ� Always �̸� ������ �̻���.. 
                            if (this.ActiveSheet.Columns[i].MergePolicy == MergePolicy.Always)
                                this.ActiveSheet.Columns[i].MergePolicy = MergePolicy.Restricted;
                        }

                        this.ActiveSheet.Columns[i].Visible = true;
                        this.ActiveSheet.ColumnHeader.Cells.Get(0, i).Text = node.Text;

                        // �÷� ������ ������.
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

		// ���� ��ܿ� Total ��� 
		/// <summary>
		///  ��ü �հ������� �׸��� ��ܿ� ��Ÿ���� �޼ҵ�
		///  startPosition ������ ��Ÿ���� ������ ��� �׳� ���� ó���� ���ڸ� ������
		/// </summary>
		/// <param name="startPosition"> ����� ������ ��ġ (������ 0 ����) </param>
		public void RPT_AddTotalRow ( int startPosition )
		{
			this.ActiveSheet.AddRows(0,1);
			FarPoint.Win.Spread.SheetView Sheet = this.ActiveSheet;
			
			// ���� ó�� �÷��� Total 
			Sheet.Cells.Get( 0, 0 ).Value = "Total";

			// ��ü �÷��� �ѹ� ��ȸ��
			for ( int columnPos = startPosition; columnPos < Sheet.ColumnCount; columnPos++)
			{
				switch ( Sheet.Columns[ columnPos ].CellType.ToString()  )
				{
					case "NumberCellType":
						// ��ü row �ѹ� ��ȸ
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


		// Ư�� �÷� exceptColumnPos �� Ư�� �� exceptValue �� �ִ� �κ��� �հ����� �ʰ� �Ѿ.. 
		public void RPT_AddTotalRow ( int startPosition , int exceptColumnPos, object exceptValue )
		{
			this.ActiveSheet.AddRows(0,1);
			FarPoint.Win.Spread.SheetView Sheet = this.ActiveSheet;
			
			// ���� ó�� �÷��� Total 
			Sheet.Cells.Get( 0, 0 ).Value = "Total";

			// ��ü �÷��� �ѹ� ��ȸ��
			for ( int columnPos = startPosition; columnPos < Sheet.ColumnCount; columnPos++)
			{
				switch ( Sheet.Columns[ columnPos ].CellType.ToString()  )
				{
					case "NumberCellType":
						// ��ü row �ѹ� ��ȸ
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

		// startPosition�� �����Ͽ� ���� ����
		public void RPT_HiddenColumnFromStartPosition ( int startPosition )
		{
			this.ActiveSheet.ColumnCount = startPosition;
		}
	

		/// <summary>
		/// RPT_AddTotalRow �����ϳ� �ӵ��� ����.. but Sheet ����� �ƴ� ���� DataTable ��ܿ� ����Ÿ�� �Է���
		/// ���� ���۾� ���Ŀ� �ٸ� �۾��� �ʿ��Ҷ� �� �޼ҵ带 �¿�����..
		/// </summary>
		/// <param name="startPosition"> ������ 0���� </param>
		public void RPT_AddTotalRow2 ( int startPosition )
		{
			DataTable org = (DataTable) this.DataSource;
			DataTable dt = new DataTable();
			this.DataSource = null;
			// �÷� Ÿ�� ����
			for ( int i = 0; i < org.Columns.Count; i++)
			{
				dt.Columns.Add( org.Columns[i].ColumnName, org.Columns[i].DataType );
			}

			DataRow total = dt.NewRow();

			total[0] = "Total";

			// ��ü �÷��� �ѹ� ��ȸ �Ͽ� �հ��÷��� ����
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

			// �հ踦 ����
			dt.Rows.Add(total);
			
			// ������ row �� �߰���
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
		/// ������ �����ٷ� ��Ȱ�ϴ� �޼ҵ�,, ������ ���� ���õ�
		/// </summary>
		/// <param name="basisColumnSize">��Ȱ���� ���� ���� �÷���</param>
		/// <param name="divideRowsSize">��Ȱ�� Row ��</param>
		/// <param name="repeatColumnSize">���ٿ� �� Column �� - ���������� �ݺ��Ǿ����</param>
		public void RPT_DivideRows( int basisColumnSize, int divideRowsSize, int repeatColumnSize )
		{	
			RPT_DivideRows ( basisColumnSize, divideRowsSize, repeatColumnSize, 0 );
		}

		/// <summary>
		/// ������ �����ٷ� ��Ȱ�ϴ� �޼ҵ�,, ������ ���� ���õ�
		/// </summary>
		/// <param name="basisColumnSize">��Ȱ���� ���� ���� �÷��� 1����</param>
		/// <param name="divideRowsSize">��Ȱ�� Row �� 1����</param>
		/// <param name="repeatColumnSize">���ٿ� �� Column �� - ���������� �ݺ��Ǿ����</param>
		/// <param name="dummyColumn">�� �޼ҵ带 ���� �÷�Ÿ�������þ� �浹�� �����ϱ� ���Ͽ� �̸� �÷��� �÷�����</param>
		public void RPT_DivideRows( int basisColumnSize, int divideRowsSize, int repeatColumnSize, int dummyColumn )
		{	

			// �ڵ� Sort �� ����
			for(int i=0; i< this.ActiveSheet.ColumnCount; i++)
				this.ActiveSheet.Columns.Get(i).AllowAutoSort = false;

			DataTable dt = new DataTable();
			DataTable org = (DataTable)this.DataSource;
			//			this.DataSource = null;

			// Ÿ�Կ� �´� �÷��� ����
			for ( int i = 0; i < ( basisColumnSize + repeatColumnSize ) ; i++ )
			{
				dt.Columns.Add( org.Columns[i].ColumnName, org.Columns[i].DataType );
			}

			// ���̺��� ���������� �ݺ�
			for ( int currentOrgRow = 0; currentOrgRow < org.Rows.Count ; currentOrgRow++ )
			{
				// �������� ���̺��� column ��(��Ȯ���� basisColumnSize �� ���ؾ���)  .. row ���� currentOrgRow
				int currentOrgColum = 0;

				// ��Ȱ�� Row ����ŭ ����
				for ( int j = 0 ; j < divideRowsSize; j++ )
				{
					DataRow row = dt.NewRow();
					
					// �⺻ �÷��� ����
					for ( int k =0 ; k <  basisColumnSize ; k++ )
					{
						row[k] = org.Rows[ currentOrgRow ][ k ];
					}

					// ������ �� ����
					for ( int m = 0 ; m < repeatColumnSize ;  m++, currentOrgColum++ )
					{
						row[ basisColumnSize +m ] = org.Rows[ currentOrgRow ][ basisColumnSize +currentOrgColum ];
					}
					
					// row ���
					dt.Rows.Add( row );
				}

			}
	
			// ���� �÷� �߰� 
			for ( int i = 0; i < dummyColumn ; i++ )
			{
				dt.Columns.Add();
			}

			// ������ ����
			this.DataSource = dt;

			// row fix
			this.ActiveSheet.FrozenRowCount *= divideRowsSize;
			
			if ( isPreCellsType )
			{
				RPT_SetCellsType();
			}

		}


		/// <summary>
		/// ���� �÷� ������ ���� ����� ���ΰ� ����
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

            //Data�� �ִ°�츸 Note���� ǥ������.
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
        /// ���� �⺻���� �÷� ������
        /// �÷��� �ϳ��� ���ذ��� row �� �� �������� Header row�� �߰�
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
			// ���������� ������ �κ�
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

                // �÷� ����� ���� ������ ���� ū��� �÷� ������ ����
                if (columnPos >= this.ActiveSheet.Columns.Count)
                    this.ActiveSheet.Columns.Count = columnPos + 1;

                // row ����� ���� ������ ���� ū��� row ������ ����
                if (rowPos >= this.ActiveSheet.ColumnHeader.Rows.Count)
                    this.ActiveSheet.ColumnHeader.Rows.Count = rowPos + 1;

                this.ActiveSheet.ColumnHeader.Cells.Get(rowPos, columnPos).Text = header_Text;

                //Spread�� Row�� 0�̸� ���� �߻���.                
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

                // ��Ÿ�� ����
                // AutoGernerationColumn �� true �� ��Ȳ����
                // ����Ÿ ���ε� ���� �ϸ� ������ ������ �ȵ�
                // ����Ÿ ���ε� ���Ŀ� ������ ��Ÿ���� �����ϱ� ���Ͽ�
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
			 * comment : sheet���� �Է� ������ ���Ͽ� option �߰�
			 * 
			 * created by : bee-jae jung(2013-09-24-ȭ����)
			 * 
			 * modified by : bee-jae jung(2013-09-24-ȭ����)
			 ************************************************************/
			// ���������� ������ �κ�
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

				// �÷� ����� ���� ������ ���� ū��� �÷� ������ ����
				if (columnPos >= this.ActiveSheet.Columns.Count)
					this.ActiveSheet.Columns.Count = columnPos + 1;

				// row ����� ���� ������ ���� ū��� row ������ ����
				if (rowPos >= this.ActiveSheet.ColumnHeader.Rows.Count)
					this.ActiveSheet.ColumnHeader.Rows.Count = rowPos + 1;

				this.ActiveSheet.ColumnHeader.Cells.Get(rowPos, columnPos).Text = header_Text;

				//Spread�� Row�� 0�̸� ���� �߻���.                
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

				// ��Ÿ�� ����
				// AutoGernerationColumn �� true �� ��Ȳ����
				// ����Ÿ ���ε� ���� �ϸ� ������ ������ �ȵ�
				// ����Ÿ ���ε� ���Ŀ� ������ ��Ÿ���� �����ϱ� ���Ͽ�
				//			_cellType.Add( getCellType( format ) );
				if (_cellType.Capacity < this.ActiveSheet.ColumnCount)
					_cellType.Capacity++;
				_cellType.Insert(columnPos, getCellType(format));

				// 2013-09-24-������ : sheet�� �����͸� �����ϱ� ���Ͽ� �߰���.
				// ReadOnly 
				this.ActiveSheet.Columns[columnPos].Locked = bLocked;
			}
			catch (Exception ex)
			{
				CmnFunction.ShowMsgBox(ex.Message);
			}
		}




		// Row ���� Formatter Type �� �ٸ� ��� .. 
		public void RPT_RepeatableRowCellTypeChange( int startColumnPos, Formatter [] formatter )
		{
			RPT_RepeatableRowCellTypeChange ( startColumnPos, this.ActiveSheet.Columns.Count - startColumnPos, formatter );
		}

		// Row ���� Formatter Type �� �ٸ� ��� .. 
		public void RPT_RepeatableRowCellTypeChange( int startColumnPos, int columnSize, Formatter [] formatter )
		{
			// ��� row �ݺ�
			for ( int i = 0; i < this.ActiveSheet.Rows.Count; i++ )
			{
				this.ActiveSheet.Cells [ i, startColumnPos, i, startColumnPos + columnSize -1 ].CellType = getCellType( formatter[ i%formatter.Length ]  );
				if ( formatter[ i%formatter.Length ] != Formatter.String && formatter[ i%formatter.Length ] != Formatter.Number )
					RPT_SetBlankFromZero ( i, startColumnPos, columnSize );
			}
		}
         
		// ����� Column �� size ��ŭ ���� �Ѵ�.
		public void RPT_MerageHeaderColumnSpan( string header_Text, int header_RowPos, int hader_ColumnPos, int size )
		{
			this.ActiveSheet.ColumnHeader.Cells [header_RowPos ,hader_ColumnPos].ColumnSpan = size;
			this.ActiveSheet.ColumnHeader.Cells [header_RowPos ,hader_ColumnPos].Text = header_Text;
		}

		public void RPT_MerageHeaderColumnSpan( int header_RowPos, int hader_ColumnPos, int size )
		{
            this.ActiveSheet.ColumnHeader.Cells [header_RowPos, hader_ColumnPos].ColumnSpan = size;
		}

        // ����� row�� size ��ŭ ���� �Ѵ�.
        public void RPT_MerageHeaderRowSpan(string header_Text, int header_RowPos, int hader_ColumnPos, int size)
		{
			this.ActiveSheet.ColumnHeader.Cells [header_RowPos ,hader_ColumnPos].RowSpan = size;
			this.ActiveSheet.ColumnHeader.Cells [header_RowPos ,hader_ColumnPos].Text = header_Text;;
		}

		public void RPT_MerageHeaderRowSpan( int header_RowPos, int hader_ColumnPos, int size )
		{            
            this.ActiveSheet.ColumnHeader.Cells [header_RowPos ,hader_ColumnPos].RowSpan = size;
		}

        //Ư�� ���� ��ŭ �÷��� �ݺ��Ͽ� ��������..
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

        // Ư�� ������ ��ŭ �÷��� �ݺ��Ͽ� ��������..
        // Ư�� ���� �÷��������� �ڵ� ������..
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


        // Ư�� ������*�ݺ�Ƚ�� ��ŭ �÷��� �ݺ��Ͽ� ��������..
        // ������ ��ܿ� ù��° �����  �ϴܿ� ������� headers �ݺ���ŭ ������..
        // Ư�� ���� �÷��������� �ڵ� ������..
        public void RPT_AddDynamicColumn(udcCUSCondition CusCondition, string[] headers, int header_rowPos, int header_columnStartPos, Visibles isVisible, Frozen isFrozen, Align aling, Merge isMerge, Formatter format, int size)
        {
            Formatter[] formats = new Formatter[headers.Length];
            for (int i = 0; i < formats.Length; i++)
                formats[i] = format;

            RPT_AddDynamicColumn(CusCondition, headers, header_rowPos, header_columnStartPos, isVisible, isFrozen, aling, isMerge, formats, size);
        }

        // Ư�� ������*�ݺ�Ƚ�� ��ŭ �÷��� �ݺ��Ͽ� ��������..
        // ������ ��ܿ� ù��° �����  �ϴܿ� ������� headers �ݺ���ŭ ������..
        // Ư�� ���� �÷��������� �ڵ� ������..
        public void RPT_AddDynamicColumn(udcCUSCondition CusCondition, string[] headers, int header_rowPos, int header_columnStartPos, Visibles isVisible, Frozen isFrozen, Align aling, Merge isMerge, Formatter[] format, int size)
        {
            ListView lvFrom = new ListView();

            lvFrom = CusCondition.ValueGetListView;

            if (lvFrom.Items.Count == 0) return;

            // headers �� format �� ����� �����ؾ���. 
            if (headers.Length != format.Length)
                throw new Exception(RptMessages.GetMessage("STD090", GlobalVariable.gcLanguage));

            int rowPos = header_rowPos;
            int columnPos = header_columnStartPos;
            int count = 0;
            // �������� ����� �÷� ����

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
                        // ��ܿ� ���� ��� ����
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
                        // ��ܿ� ���� ��� ����
                        this.RPT_MerageHeaderColumnSpan(Header, rowPos, columnPos - headers.Length, headers.Length);
                    }
                }

            }                      
        }

        // CusCondition�� ���������� Column �߰��ÿ� CusCondition�� SubItem���� ��� ��Ʈ���� ���Եǵ��� �� ��° ���ڸ� �߰��� ����. 
        // PRD010502���� ����ϱ� ���� �����ε���. 2009.1.14 ������
        public void RPT_AddDynamicColumn(udcCUSCondition CusCondition, bool withSubItemString, string[] headers, int header_rowPos, int header_columnStartPos, Visibles isVisible, Frozen isFrozen, Align aling, Merge isMerge, Formatter[] format, int size)
        {
            ListView lvFrom = new ListView();

            lvFrom = CusCondition.ValueGetListView;

            if (lvFrom.Items.Count == 0) return;

            // headers �� format �� ����� �����ؾ���. 
            if (headers.Length != format.Length)
                throw new Exception(RptMessages.GetMessage("STD090", GlobalVariable.gcLanguage));

            int rowPos = header_rowPos;
            int columnPos = header_columnStartPos;
            int count = 0;
            // �������� ����� �÷� ����

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
                        // ��ܿ� ���� ��� ����
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
                        // ��ܿ� ���� ��� ����
                        this.RPT_MerageHeaderColumnSpan(Header, rowPos, columnPos - headers.Length, headers.Length);
                    }
                }

            }
        }
		
        //Add by John Seo. 2008.11.25
        //Ư�� ���� ��ŭ �÷��� �ݺ��Ͽ� ��������..
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

            // �������� ����� �÷� ����            
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

        // Ư�� ������ ��ŭ �÷��� �ݺ��Ͽ� ��������..
        // Ư�� ���� �÷��������� �ڵ� ������..
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

            // �������� ����� �÷� ����
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


        // Ư�� ������*�ݺ�Ƚ�� ��ŭ �÷��� �ݺ��Ͽ� ��������..
        // ������ ��ܿ� ù��° �����  �ϴܿ� ������� headers �ݺ���ŭ ������..
        // Ư�� ���� �÷��������� �ڵ� ������..
        public void RPT_AddDynamicColumn(udcCUSFromToCondition FromToCondition, string[] headers, int header_rowPos, int header_columnStartPos, Visibles isVisible
                                         , Frozen isFrozen, Align aling, Merge isMerge, Formatter format, int size)
        {
            Formatter[] formats = new Formatter[headers.Length];
            for (int i = 0; i < formats.Length; i++)
                formats[i] = format;

            RPT_AddDynamicColumn(FromToCondition, headers, header_rowPos, header_columnStartPos, isVisible, isFrozen, aling, isMerge, formats, size);
        }

        // Ư�� ������*�ݺ�Ƚ�� ��ŭ �÷��� �ݺ��Ͽ� ��������..
        // ������ ��ܿ� ù��° �����  �ϴܿ� ������� headers �ݺ���ŭ ������..
        // Ư�� ���� �÷��������� �ڵ� ������..        
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

            // headers �� format �� ����� �����ؾ���. 
            if (headers.Length != format.Length)
                throw new Exception(RptMessages.GetMessage("STD090", GlobalVariable.gcLanguage));

            int rowPos = header_rowPos;
            int columnPos = header_columnStartPos;

            // �������� ����� �÷� ����

            for (int i = iFromIdx; i <= iToIdx; i++)
            {
                string Header = lvFrom.Items[i].Text;
                 
                for (int j = 0; j < headers.Length; j++)
                    this.RPT_AddBasicColumn(headers[j], rowPos + 1, columnPos++, isVisible, isFrozen, aling, isMerge, format[j], size, null);
                                 
                // ��ܿ� ���� ��� ����
                this.RPT_MerageHeaderColumnSpan(Header, rowPos, columnPos - headers.Length, headers.Length);
            }
        }
		

		//Ư�� ��¥�� ��ŭ �÷��� �ݺ��Ͽ� ��������..
        public void RPT_AddDynamicColumn(udcDurationDate duration, Visibles isVisible, Frozen isFrozen, Align aling, Merge isMerge, Formatter format, int size)
        {  
            // �������� ����� �÷� ����
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

                //�⵵�� ���� ���
                if (duration.FromWeek.SelectedValue.ToString().Substring(0, 4) ==
                    duration.ToWeek.SelectedValue.ToString().Substring(0, 4))
                {
                    for (int i = thisWeek; i <= maxWeek; i++)
                    {
                        Header = "WW" + (i).ToString();

                        this.RPT_AddBasicColumn(Header, isVisible, isFrozen, aling, isMerge, format, size);
                    }
                }
                else  //�ٸ� ���
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
            else // ���� ���
            {
                for ( int i = 0 ; i <= duration.SubtractBetweenFromToDate ; i++ )
                {
                    string Header = duration.FromYearMonth.Value.AddMonths(i).ToString("yy.MM");
                    this.RPT_AddBasicColumn( Header,		isVisible, isFrozen, aling, isMerge, format, size);
                }
            }             
             
        }

		// Ư�� ��¥�� ��ŭ �÷��� �ݺ��Ͽ� ��������..
		// Ư�� ���� �÷��������� �ڵ� ������..
        public void RPT_AddDynamicColumn(udcDurationDate duration, int header_rowPos, int header_columnStartPos, Visibles isVisible, Frozen isFrozen, Align aling, Merge isMerge, Formatter format, int size)
        {                          
            // �������� ����� �÷� ����
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

                //�⵵�� ���� ���
                if (duration.FromWeek.SelectedValue.ToString().Substring(0, 4) ==
                    duration.ToWeek.SelectedValue.ToString().Substring(0, 4))
                {
                    for (int i = thisWeek; i <= maxWeek; i++)
                    {
                        Header = "WW" + (i).ToString();

                        this.RPT_AddBasicColumn(Header, header_rowPos, header_columnStartPos++, isVisible, isFrozen, aling, isMerge, format, size);
                    }
                }
                else  //�ٸ� ���
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
            else // ���� ���
            {
                for (int i = 0; i <= duration.SubtractBetweenFromToDate; i++)
                {
                    string Header = duration.FromYearMonth.Value.AddMonths(i).ToString("yy.MM");
                    this.RPT_AddBasicColumn(Header, header_rowPos, header_columnStartPos++, isVisible, isFrozen, aling, isMerge, format, size);
                }
            }
             
        }


		// Ư�� ��¥��*�ݺ�Ƚ�� ��ŭ �÷��� �ݺ��Ͽ� ��������..
		// ��¥�� ��ܿ� ù��° �����  �ϴܿ� ������� headers �ݺ���ŭ ������..
		// Ư�� ���� �÷��������� �ڵ� ������..
        public void RPT_AddDynamicColumn(udcDurationDate duration, string[] headers, int header_rowPos, int header_columnStartPos, Visibles isVisible, Frozen isFrozen, Align aling, Merge isMerge, Formatter format, int size)
        {
            Formatter[] formats = new Formatter[headers.Length];
            for (int i = 0; i < formats.Length; i++)
                formats[i] = format;

            RPT_AddDynamicColumn(duration, headers, header_rowPos, header_columnStartPos, isVisible, isFrozen, aling, isMerge, formats, size);
        }

		// Ư�� ��¥��*�ݺ�Ƚ�� ��ŭ �÷��� �ݺ��Ͽ� ��������..
		// ��¥�� ��ܿ� ù��° �����  �ϴܿ� ������� headers �ݺ���ŭ ������..
		// Ư�� ���� �÷��������� �ڵ� ������..
        public void RPT_AddDynamicColumn(udcDurationDate duration, string[] headers, int header_rowPos, int header_columnStartPos, Visibles isVisible, Frozen isFrozen, Align aling, Merge isMerge, Formatter[] format, int size)
        {

            // headers �� format �� ����� �����ؾ���. 
            if (headers.Length != format.Length)
                throw new Exception(RptMessages.GetMessage("STD090", GlobalVariable.gcLanguage));

            int rowPos = header_rowPos;
            int columnPos = header_columnStartPos;

            // �������� ����� �÷� ����
            if (duration.DaySelector.SelectedValue.ToString().ToUpper() == "DAY")
            {
                for (int i = 0; i <= duration.SubtractBetweenFromToDate; i++)
                {
                    string Header = duration.FromDate.Value.AddDays(i).ToString("MM.dd");
                    for (int j = 0; j < headers.Length; j++)
                        this.RPT_AddBasicColumn(headers[j], rowPos + 1, columnPos++, isVisible, isFrozen, aling, isMerge, format[j], size);
                    // ��ܿ� ��¥ ��� ����
                    this.RPT_MerageHeaderColumnSpan(Header, rowPos, columnPos - headers.Length, headers.Length);
                }
            }
            else if (duration.DaySelector.SelectedValue.ToString().ToUpper() == "WEEK")
            {
                int thisWeek = Convert.ToInt32(duration.FromWeek.SelectedValue.ToString().Substring(4, 2));
                int thisMax = duration.FromWeek.Items.Count;
                int maxWeek = Convert.ToInt32(duration.ToWeek.SelectedValue.ToString().Substring(4, 2));
                string Header = null;

                //�⵵�� ���� ���
                if (duration.FromWeek.SelectedValue.ToString().Substring(0, 4) ==
                    duration.ToWeek.SelectedValue.ToString().Substring(0, 4))
                {  
                    for (int i = thisWeek; i <= maxWeek; i++)
                    {
                        Header = "WW" + (i).ToString();

                        for (int j = 0; j < headers.Length; j++)
                            this.RPT_AddBasicColumn(headers[j], rowPos + 1, columnPos++, isVisible, isFrozen, aling, isMerge, format[j], size);
                        // ��ܿ� ��¥ ��� ����
                        this.RPT_MerageHeaderColumnSpan(Header, rowPos, columnPos - headers.Length, headers.Length);
                    }
                }
                else  //�ٸ� ���
                {
                    for (int i = thisWeek; i <= thisMax; i++)
                    {
                        Header = "WW" + (i).ToString();

                        for (int j = 0; j < headers.Length; j++)
                            this.RPT_AddBasicColumn(headers[j], rowPos + 1, columnPos++, isVisible, isFrozen, aling, isMerge, format[j], size);
                        // ��ܿ� ��¥ ��� ����
                        this.RPT_MerageHeaderColumnSpan(Header, rowPos, columnPos - headers.Length, headers.Length);
                    }

                    for (int i = 1; i <= maxWeek; i++)
                    {
                        Header = "WW" + (i).ToString();

                        for (int j = 0; j < headers.Length; j++)
                            this.RPT_AddBasicColumn(headers[j], rowPos + 1, columnPos++, isVisible, isFrozen, aling, isMerge, format[j], size);
                        // ��ܿ� ��¥ ��� ����
                        this.RPT_MerageHeaderColumnSpan(Header, rowPos, columnPos - headers.Length, headers.Length);
                    }
                }
            }
            else // ���� ���
            {
                for (int i = 0; i <= duration.SubtractBetweenFromToDate; i++)
                {
                    string Header = duration.FromYearMonth.Value.AddMonths(i).ToString("yy.MM");

                    for (int j = 0; j < headers.Length; j++)
                        this.RPT_AddBasicColumn(headers[j], rowPos + 1, columnPos++, isVisible, isFrozen, aling, isMerge, format[j], size);
                    // ��ܿ� ��¥ ��� ����
                    this.RPT_MerageHeaderColumnSpan(Header, rowPos, columnPos - headers.Length, headers.Length);
                }
            }
        }

        #region udcDurationDatePlus �� ���ڷ� �޴� �����ε��� �޼���
        //Ư�� ��¥�� ��ŭ �÷��� �ݺ��Ͽ� ��������..
        public void RPT_AddDynamicColumn(udcDurationDatePlus duration, Visibles isVisible, Frozen isFrozen, Align aling, Merge isMerge, Formatter format, int size)
        {
            // �������� ����� �÷� ����
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

                //�⵵�� ���� ���
                if (duration.FromWeek.SelectedValue.ToString().Substring(0, 4) ==
                    duration.ToWeek.SelectedValue.ToString().Substring(0, 4))
                {
                    for (int i = thisWeek; i <= maxWeek; i++)
                    {
                        Header = "WW" + (i).ToString();

                        this.RPT_AddBasicColumn(Header, isVisible, isFrozen, aling, isMerge, format, size);
                    }
                }
                else  //�ٸ� ���
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
            else // ���� ���
            {
                for (int i = 0; i <= duration.SubtractBetweenFromToDate; i++)
                {
                    string Header = duration.FromYearMonth.Value.AddMonths(i).ToString("yy.MM");
                    this.RPT_AddBasicColumn(Header, isVisible, isFrozen, aling, isMerge, format, size);
                }
            }

        }

        // Ư�� ��¥�� ��ŭ �÷��� �ݺ��Ͽ� ��������..
        // Ư�� ���� �÷��������� �ڵ� ������..
        public void RPT_AddDynamicColumn(udcDurationDatePlus duration, int header_rowPos, int header_columnStartPos, Visibles isVisible, Frozen isFrozen, Align aling, Merge isMerge, Formatter format, int size)
        {
            // �������� ����� �÷� ����
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

                //�⵵�� ���� ���
                if (duration.FromWeek.SelectedValue.ToString().Substring(0, 4) ==
                    duration.ToWeek.SelectedValue.ToString().Substring(0, 4))
                {
                    for (int i = thisWeek; i <= maxWeek; i++)
                    {
                        Header = "WW" + (i).ToString();

                        this.RPT_AddBasicColumn(Header, header_rowPos, header_columnStartPos++, isVisible, isFrozen, aling, isMerge, format, size);
                    }
                }
                else  //�ٸ� ���
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
            else // ���� ���
            {
                for (int i = 0; i <= duration.SubtractBetweenFromToDate; i++)
                {
                    string Header = duration.FromYearMonth.Value.AddMonths(i).ToString("yy.MM");
                    this.RPT_AddBasicColumn(Header, header_rowPos, header_columnStartPos++, isVisible, isFrozen, aling, isMerge, format, size);
                }
            }

        }


        // Ư�� ��¥��*�ݺ�Ƚ�� ��ŭ �÷��� �ݺ��Ͽ� ��������..
        // ��¥�� ��ܿ� ù��° �����  �ϴܿ� ������� headers �ݺ���ŭ ������..
        // Ư�� ���� �÷��������� �ڵ� ������..
        public void RPT_AddDynamicColumn(udcDurationDatePlus duration, string[] headers, int header_rowPos, int header_columnStartPos, Visibles isVisible, Frozen isFrozen, Align aling, Merge isMerge, Formatter format, int size)
        {
            Formatter[] formats = new Formatter[headers.Length];
            for (int i = 0; i < formats.Length; i++)
                formats[i] = format;

            RPT_AddDynamicColumn(duration, headers, header_rowPos, header_columnStartPos, isVisible, isFrozen, aling, isMerge, formats, size);
        }

        // Ư�� ��¥��*�ݺ�Ƚ�� ��ŭ �÷��� �ݺ��Ͽ� ��������..
        // ��¥�� ��ܿ� ù��° �����  �ϴܿ� ������� headers �ݺ���ŭ ������..
        // Ư�� ���� �÷��������� �ڵ� ������..
        public void RPT_AddDynamicColumn(udcDurationDatePlus duration, string[] headers, int header_rowPos, int header_columnStartPos, Visibles isVisible, Frozen isFrozen, Align aling, Merge isMerge, Formatter[] format, int size)
        {

            // headers �� format �� ����� �����ؾ���. 
            if (headers.Length != format.Length)
                throw new Exception(RptMessages.GetMessage("STD090", GlobalVariable.gcLanguage));

            int rowPos = header_rowPos;
            int columnPos = header_columnStartPos;

            // �������� ����� �÷� ����
            if (duration.DaySelector.SelectedValue.ToString().ToUpper() == "DAY")
            {
                for (int i = 0; i <= duration.SubtractBetweenFromToDate; i++)
                {
                    string Header = duration.FromDate.Value.AddDays(i).ToString("MM.dd");
                    for (int j = 0; j < headers.Length; j++)
                        this.RPT_AddBasicColumn(headers[j], rowPos + 1, columnPos++, isVisible, isFrozen, aling, isMerge, format[j], size);
                    // ��ܿ� ��¥ ��� ����
                    this.RPT_MerageHeaderColumnSpan(Header, rowPos, columnPos - headers.Length, headers.Length);
                }
            }
            else if (duration.DaySelector.SelectedValue.ToString().ToUpper() == "WEEK")
            {
                int thisWeek = Convert.ToInt32(duration.FromWeek.SelectedValue.ToString().Substring(4, 2));
                int thisMax = duration.FromWeek.Items.Count;
                int maxWeek = Convert.ToInt32(duration.ToWeek.SelectedValue.ToString().Substring(4, 2));
                string Header = null;

                //�⵵�� ���� ���
                if (duration.FromWeek.SelectedValue.ToString().Substring(0, 4) ==
                    duration.ToWeek.SelectedValue.ToString().Substring(0, 4))
                {
                    for (int i = thisWeek; i <= maxWeek; i++)
                    {
                        Header = "WW" + (i).ToString();

                        for (int j = 0; j < headers.Length; j++)
                            this.RPT_AddBasicColumn(headers[j], rowPos + 1, columnPos++, isVisible, isFrozen, aling, isMerge, format[j], size);
                        // ��ܿ� ��¥ ��� ����
                        this.RPT_MerageHeaderColumnSpan(Header, rowPos, columnPos - headers.Length, headers.Length);
                    }
                }
                else  //�ٸ� ���
                {
                    for (int i = thisWeek; i <= thisMax; i++)
                    {
                        Header = "WW" + (i).ToString();

                        for (int j = 0; j < headers.Length; j++)
                            this.RPT_AddBasicColumn(headers[j], rowPos + 1, columnPos++, isVisible, isFrozen, aling, isMerge, format[j], size);
                        // ��ܿ� ��¥ ��� ����
                        this.RPT_MerageHeaderColumnSpan(Header, rowPos, columnPos - headers.Length, headers.Length);
                    }

                    for (int i = 1; i <= maxWeek; i++)
                    {
                        Header = "WW" + (i).ToString();

                        for (int j = 0; j < headers.Length; j++)
                            this.RPT_AddBasicColumn(headers[j], rowPos + 1, columnPos++, isVisible, isFrozen, aling, isMerge, format[j], size);
                        // ��ܿ� ��¥ ��� ����
                        this.RPT_MerageHeaderColumnSpan(Header, rowPos, columnPos - headers.Length, headers.Length);
                    }
                }
            }
            else // ���� ���
            {
                for (int i = 0; i <= duration.SubtractBetweenFromToDate; i++)
                {
                    string Header = duration.FromYearMonth.Value.AddMonths(i).ToString("yy.MM");

                    for (int j = 0; j < headers.Length; j++)
                        this.RPT_AddBasicColumn(headers[j], rowPos + 1, columnPos++, isVisible, isFrozen, aling, isMerge, format[j], size);
                    // ��ܿ� ��¥ ��� ����
                    this.RPT_MerageHeaderColumnSpan(Header, rowPos, columnPos - headers.Length, headers.Length);
                }
            }
        }
        #endregion

        #region udcDurationDate2 �� ���ڷ� �޴� �����ε��� �޼���
        //Ư�� ��¥�� ��ŭ �÷��� �ݺ��Ͽ� ��������..
        public void RPT_AddDynamicColumn(udcDurationDate2 duration, Visibles isVisible, Frozen isFrozen, Align aling, Merge isMerge, Formatter format, int size)
        {
            // �������� ����� �÷� ����
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

                //�⵵�� ���� ���
                if (duration.FromWeek.SelectedValue.ToString().Substring(0, 4) ==
                    duration.ToWeek.SelectedValue.ToString().Substring(0, 4))
                {
                    for (int i = thisWeek; i <= maxWeek; i++)
                    {
                        Header = "WW" + (i).ToString();

                        this.RPT_AddBasicColumn(Header, isVisible, isFrozen, aling, isMerge, format, size);
                    }
                }
                else  //�ٸ� ���
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
            else // ���� ���
            {
                for (int i = 0; i <= duration.SubtractBetweenFromToDate; i++)
                {
                    string Header = duration.FromYearMonth.Value.AddMonths(i).ToString("yy.MM");
                    this.RPT_AddBasicColumn(Header, isVisible, isFrozen, aling, isMerge, format, size);
                }
            }

        }

        // Ư�� ��¥�� ��ŭ �÷��� �ݺ��Ͽ� ��������..
        // Ư�� ���� �÷��������� �ڵ� ������..
        public void RPT_AddDynamicColumn(udcDurationDate2 duration, int header_rowPos, int header_columnStartPos, Visibles isVisible, Frozen isFrozen, Align aling, Merge isMerge, Formatter format, int size)
        {
            // �������� ����� �÷� ����
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

                //�⵵�� ���� ���
                if (duration.FromWeek.SelectedValue.ToString().Substring(0, 4) ==
                    duration.ToWeek.SelectedValue.ToString().Substring(0, 4))
                {
                    for (int i = thisWeek; i <= maxWeek; i++)
                    {
                        Header = "WW" + (i).ToString();

                        this.RPT_AddBasicColumn(Header, header_rowPos, header_columnStartPos++, isVisible, isFrozen, aling, isMerge, format, size);
                    }
                }
                else  //�ٸ� ���
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
            else // ���� ���
            {
                for (int i = 0; i <= duration.SubtractBetweenFromToDate; i++)
                {
                    string Header = duration.FromYearMonth.Value.AddMonths(i).ToString("yy.MM");
                    this.RPT_AddBasicColumn(Header, header_rowPos, header_columnStartPos++, isVisible, isFrozen, aling, isMerge, format, size);
                }
            }

        }


        // Ư�� ��¥��*�ݺ�Ƚ�� ��ŭ �÷��� �ݺ��Ͽ� ��������..
        // ��¥�� ��ܿ� ù��° �����  �ϴܿ� ������� headers �ݺ���ŭ ������..
        // Ư�� ���� �÷��������� �ڵ� ������..
        public void RPT_AddDynamicColumn(udcDurationDate2 duration, string[] headers, int header_rowPos, int header_columnStartPos, Visibles isVisible, Frozen isFrozen, Align aling, Merge isMerge, Formatter format, int size)
        {
            Formatter[] formats = new Formatter[headers.Length];
            for (int i = 0; i < formats.Length; i++)
                formats[i] = format;

            RPT_AddDynamicColumn(duration, headers, header_rowPos, header_columnStartPos, isVisible, isFrozen, aling, isMerge, formats, size);
        }

        // Ư�� ��¥��*�ݺ�Ƚ�� ��ŭ �÷��� �ݺ��Ͽ� ��������..
        // ��¥�� ��ܿ� ù��° �����  �ϴܿ� ������� headers �ݺ���ŭ ������..
        // Ư�� ���� �÷��������� �ڵ� ������..
        public void RPT_AddDynamicColumn(udcDurationDate2 duration, string[] headers, int header_rowPos, int header_columnStartPos, Visibles isVisible, Frozen isFrozen, Align aling, Merge isMerge, Formatter[] format, int size)
        {

            // headers �� format �� ����� �����ؾ���. 
            if (headers.Length != format.Length)
                throw new Exception(RptMessages.GetMessage("STD090", GlobalVariable.gcLanguage));

            int rowPos = header_rowPos;
            int columnPos = header_columnStartPos;

            // �������� ����� �÷� ����
            if (duration.DaySelector.SelectedValue.ToString().ToUpper() == "DAY")
            {
                for (int i = 0; i <= duration.SubtractBetweenFromToDate; i++)
                {
                    string Header = duration.FromDate.Value.AddDays(i).ToString("MM.dd");
                    for (int j = 0; j < headers.Length; j++)
                        this.RPT_AddBasicColumn(headers[j], rowPos + 1, columnPos++, isVisible, isFrozen, aling, isMerge, format[j], size);
                    // ��ܿ� ��¥ ��� ����
                    this.RPT_MerageHeaderColumnSpan(Header, rowPos, columnPos - headers.Length, headers.Length);
                }
            }
            else if (duration.DaySelector.SelectedValue.ToString().ToUpper() == "WEEK")
            {
                int thisWeek = Convert.ToInt32(duration.FromWeek.SelectedValue.ToString().Substring(4, 2));
                int thisMax = duration.FromWeek.Items.Count;
                int maxWeek = Convert.ToInt32(duration.ToWeek.SelectedValue.ToString().Substring(4, 2));
                string Header = null;

                //�⵵�� ���� ���
                if (duration.FromWeek.SelectedValue.ToString().Substring(0, 4) ==
                    duration.ToWeek.SelectedValue.ToString().Substring(0, 4))
                {
                    for (int i = thisWeek; i <= maxWeek; i++)
                    {
                        Header = "WW" + (i).ToString();

                        for (int j = 0; j < headers.Length; j++)
                            this.RPT_AddBasicColumn(headers[j], rowPos + 1, columnPos++, isVisible, isFrozen, aling, isMerge, format[j], size);
                        // ��ܿ� ��¥ ��� ����
                        this.RPT_MerageHeaderColumnSpan(Header, rowPos, columnPos - headers.Length, headers.Length);
                    }
                }
                else  //�ٸ� ���
                {
                    for (int i = thisWeek; i <= thisMax; i++)
                    {
                        Header = "WW" + (i).ToString();

                        for (int j = 0; j < headers.Length; j++)
                            this.RPT_AddBasicColumn(headers[j], rowPos + 1, columnPos++, isVisible, isFrozen, aling, isMerge, format[j], size);
                        // ��ܿ� ��¥ ��� ����
                        this.RPT_MerageHeaderColumnSpan(Header, rowPos, columnPos - headers.Length, headers.Length);
                    }

                    for (int i = 1; i <= maxWeek; i++)
                    {
                        Header = "WW" + (i).ToString();

                        for (int j = 0; j < headers.Length; j++)
                            this.RPT_AddBasicColumn(headers[j], rowPos + 1, columnPos++, isVisible, isFrozen, aling, isMerge, format[j], size);
                        // ��ܿ� ��¥ ��� ����
                        this.RPT_MerageHeaderColumnSpan(Header, rowPos, columnPos - headers.Length, headers.Length);
                    }
                }
            }
            else // ���� ���
            {
                for (int i = 0; i <= duration.SubtractBetweenFromToDate; i++)
                {
                    string Header = duration.FromYearMonth.Value.AddMonths(i).ToString("yy.MM");

                    for (int j = 0; j < headers.Length; j++)
                        this.RPT_AddBasicColumn(headers[j], rowPos + 1, columnPos++, isVisible, isFrozen, aling, isMerge, format[j], size);
                    // ��ܿ� ��¥ ��� ����
                    this.RPT_MerageHeaderColumnSpan(Header, rowPos, columnPos - headers.Length, headers.Length);
                }
            }
        }
        #endregion

        // Ư�� Row ����ŭ �ݺ������� ���� ��Ȱ �Ҷ�. 
		// ��Ư�� Column Merage �� ������ ����
		public void RPT_AddRowsColor( RowColorType color, int RowCount ,bool hasTotal )
		{
			this.RPT_AddRowsColor ( color, RowCount, 0, 0, hasTotal );
		}
                
		// Ư�� Row ����ŭ �ݺ������� ���� ��Ȱ �Ҷ�. 
		// Merage �� �ø��� �ٸ� ������ ä��� ����
		public void RPT_AddRowsColor( RowColorType color, int RowCount , int fixStart, int fixSize ,bool hasTotal )
		{
			
			if ( RowCount == 0 )
				throw new Exception(RptMessages.GetMessage("STD091", GlobalVariable.gcLanguage));

			if ( (fixStart + fixSize) > this.ActiveSheet.Columns.Count )
				throw new Exception(RptMessages.GetMessage("STD092", GlobalVariable.gcLanguage));
			
			// fix ��
//			System.Drawing.Color foreFix= System.Drawing.Color.Black;
			System.Drawing.Color backFix = System.Drawing.Color.White;

			// total 
//			System.Drawing.Color foreTotal = System.Drawing.Color.Black;
			System.Drawing.Color backTotal = System.Drawing.Color.White;
			
			// ù��° �ݺ���
//			System.Drawing.Color foreFirst = System.Drawing.Color.Black;
			System.Drawing.Color backFirst = System.Drawing.Color.White;

			// �ι�° �ݺ���
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
			
			// �ݺ��ο찡 �� �Ѱ��̸� �� �κ��� �¿�� �� ������.. 
			// Row ���� ������ �ٽ� ������ �ʿ䰡 ��������. 
			if ( RowCount == 1 )
			{
				// fix �� �÷� ��ŭ �ݺ�
				for ( int i = fixStart ; i < fixSize; i++ )
				{
					this.ActiveSheet.Columns[i].BackColor = backFix;
				}
	
				
				// ��Ż�� ��ĥ��.. 
				if ( hasTotal )
					for ( int i = 0; i < RowCount; i++ )
						this.ActiveSheet.Rows[ i ].BackColor = backTotal;
	
				return;
			}
             
			// ��Ż�� ��ĥ��.. 
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
				// �����̸� ����ĥ���� ����.. �ӵ��� ���ؼ�.. 
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


		// �ݺ����� ����Ÿ�� �̿��Ͽ� Ư�� �ø��� ä��� �޼ҵ�
		public void RPT_FillColumnData( int columnPos, object [] datas )
		{
			if ( datas == null || datas.Length == 0 )
				throw new Exception(RptMessages.GetMessage("STD093", GlobalVariable.gcLanguage));

			if ( this.ActiveSheet.Columns.Count < columnPos )
				throw new Exception(RptMessages.GetMessage("STD094", GlobalVariable.gcLanguage));

			// ��ü����Ÿ�� �ݺ�
			for ( int k = 0; k < datas.Length; k++ ) 
			{
                // 2020-03-18-��̰� : ��� ��ȯ
				object data  = LanguageFunction.FindLanguage(datas[k].ToString(), 0);
				for ( int i = k ; i < this.ActiveSheet.Rows.Count; i+=datas.Length )
				{
					this.ActiveSheet.Cells.Get( i, columnPos ).Value = data;                   
				}
			}
		}

        // �ݺ����� ����Ÿ�� �̿��Ͽ� Ư�� �ø��� ä��� �޼ҵ�, ���° ROW ���� ���� �ϴ��� ���ڸ� ����
        public void RPT_FillColumnData(int columnPos, object[] datas, int row_num)
        {
            if (datas == null || datas.Length == 0)
                throw new Exception(RptMessages.GetMessage("STD093", GlobalVariable.gcLanguage));

            if (this.ActiveSheet.Columns.Count < columnPos)
                throw new Exception(RptMessages.GetMessage("STD094", GlobalVariable.gcLanguage));

            // ��ü����Ÿ�� �ݺ�
            for (int j=0,k = row_num; k < datas.Length; k++)
            {
                // 2020-03-18-��̰� : ��� ��ȯ
                object data = LanguageFunction.FindLanguage(datas[j].ToString(), 0);
                for (int i = k; i < this.ActiveSheet.Rows.Count; i += datas.Length)
                {
                    this.ActiveSheet.Cells.Get(i, columnPos).Value = data;                   
                }
            }
        }


		//�޺��ڽ� ��Ÿ�Խ� DataTable�� �̿��� ���ε�
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

		// �ʿ��� �κ��� Ư�� �׸����� ä��� �޼ҵ�
		public void RPT_FillDataSelectiveCells(object values, int columnPos, int columnSize, int rowPos, int rowSize)
		{
			this.RPT_FillDataSelectiveCells( values, columnPos, columnSize, rowPos, rowSize, false, Align.Center, VerticalAlign.Center);
		}

		// �ʿ��� �κ��� Ư�� �׸����� ä��� �޼ҵ�
		// align, vAling �� isMerage �� true �� ��츸 ���
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
		

		// Ư�� ���� ������ ���ϴ� �޼���
		public void RPT_AddFormula( int rowPos, int columnPos, string formula )
		{
			RPT_AddFormula ( rowPos, 1, columnPos, 1, formula );
			RPT_SetBlankFromZero ( rowPos, columnPos, 1 );
		}

		// Ư�� ���� ���� ������ ���ϴ� �޼���
		public void RPT_AddFormula ( int rowPos, int rowSize, int columnPos, int columnSize, string formula )
		{
			if ( rowPos < 0 || columnPos < 0 || rowSize < 1 || columnSize < 1 ||  rowPos + rowSize > this.ActiveSheet.Rows.Count || columnPos + columnSize > this.ActiveSheet.Columns.Count )
				throw new Exception(RptMessages.GetMessage("STD095", GlobalVariable.gcLanguage));

			this.ActiveSheet.Cells [ rowPos, columnPos, rowPos + rowSize - 1, columnPos + columnSize - 1 ].Formula = formula;
			
			for ( int i = rowPos; i < rowSize; i++ )
				RPT_SetBlankFromZero ( i, columnPos, columnSize );

		}
		  
		// Ư�� �÷��� Ư�� ����Ÿ �Ǽ��� ������.. 
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

        //    // ù��° row �� ������ ��Ż������ ����
        //    // ������ row �� ������ �հ���..  ���� �˻����ʿ� ����
        //    for ( int i = 1; i < this.ActiveSheet.Rows.Count - 1; i++ )
        //    {
        //        if ( this.ActiveSheet.Cells[ i, SummaryColumnPos ].Text != this.ActiveSheet.Cells[ i + 1, SummaryColumnPos ].Text )
        //        {
        //            list.Add( i );
        //        }
        //    }
			
        //    //������ row �߰�
        //    list.Add( this.ActiveSheet.Rows.Count - 1 );

        //    return (int [])list.ToArray( typeof(int));
        //}


		// Ư�� ������ Merge Ÿ������ �����ϴ°�.
		public void RPT_SetMerge ( int ColumnPos, int ColumnSize )
		{
			this.ActiveSheet.Columns[ ColumnPos ].MergePolicy = MergePolicy.Always;
			for ( int i = ColumnPos + 1; i < ColumnSize; i++ )
				this.ActiveSheet.Columns[ i ].MergePolicy = MergePolicy.Restricted;
		}


		// �÷� Ÿ���� �𸦶� ������ 0 �� �����ϴ� �޼ҵ�
		// ColumnPos ������ ��� �÷��� ����
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
		///  �κ� �հ踦 �������.. DataSource �� ���� �����ϸ� �ȉ�. 
		/// </summary>
		/// <param name="dt">���� ����Ÿ ���̺�</param>
		/// <param name="startSubTotalColumnPosition">�κ��հ踦 ������ �÷�..</param>
		/// <param name="SubTotalColumnSize">�κ��հ踦 ����� �÷� ������</param>
		/// <param name="StartValueColumnPos">���� ����� ����Ÿ�� ����ִ� �÷�</param>
		/// <returns>Row Ÿ�� : �ο쵥��Ÿ�� 1, �հ�� 2, ������ SubTotal �� +1 ��, ù �κ��հ� ���� 3 ����..</returns>
		public int [] RPT_DataBindingWithSubTotal ( DataTable dt, int startSubTotalColumnPosition, int SubTotalColumnSize, int StartValueColumnPos )
		{
			return this.RPT_DataBindingWithSubTotal ( dt, startSubTotalColumnPosition, SubTotalColumnSize, StartValueColumnPos, null, null );
		}

		/// <summary>
		///  �κ� �հ踦 �������.. DataSource �� ���� �����ϸ� �ȉ�. 
		/// </summary>
		/// <param name="dt">���� ����Ÿ ���̺�</param>
		/// <param name="startSubTotalColumnPosition">�κ��հ踦 ������ �÷�..</param>
		/// <param name="SubTotalColumnSize">�κ��հ踦 ����� �÷� ������</param>
		/// <param name="StartValueColumnPos">���� ����� ����Ÿ�� ����ִ� �÷�</param>
		/// <param name="Formula">���� ����Ÿ�� ���� ����</param>
		/// <param name="SumFormula">�հ�� �κ��հ迡�� ���� ����.</param>
		/// <returns>Row Ÿ�� : �ο쵥��Ÿ�� 1, �հ�� 2, ������ SubTotal �� +1 ��, ù �κ��հ� ���� 3 ����..</returns>
		public int [] RPT_DataBindingWithSubTotal ( DataTable dt, int startSubTotalColumnPosition, int SubTotalColumnSize, int StartValueColumnPos, string[,] Formula, string[,] SumFormula )
		{ 
			if ( dt == null )
				return null;

			if ( dt.Rows.Count == 0 )
			{
				this.DataSource = dt;
				return null;	
			}

			// ���ġ�� ��� ���̺�.. 
			DataTable desc = new DataTable();
			// �ο쵥��Ÿ ������ ��� �÷�..
			int iColumnType = dt.Columns.Count;
			// ��Ż���� ������ �迭
			double [] Total = new double [ dt.Columns.Count - StartValueColumnPos ];
			// ���� ��Ż���� ������ �迭
			double [ , ] SubTotalValue = new double [ SubTotalColumnSize, dt.Columns.Count - StartValueColumnPos ];
			// ������Ż�� ������ �Ǵ� ��
			string [] SubTotal = new string [ SubTotalColumnSize ];
			// ������Ż�� �߰����� �����Ҷ� ������ ���ذ���..
			string [] Base = new string [ StartValueColumnPos ];
			DataRow rows = null;
			// �κ� �հ�� ����Ÿ row
			DataRow [] subTotalRows = new DataRow[ SubTotalColumnSize ];
			// �κ��հ踦 ���� ����� �κ��հ谡 �ٲ�� ������ �ϴ��� �κ��հ踦 �ٲٱ�����.
			bool isSuperChanged;
			// ����Ÿ Ÿ���� �����س��� ���
			string [] DataTypes = new string[ dt.Columns.Count ];
			// DataTable �� Row�� { �Ϲ�, �հ�, �κ��հ� } �� ������ �迭
			int [] rowType;
			// �ӽ� ����  RoundValue ������ ���� ������ ���Ͽ�
			double temp = 0;
			
			// �÷� Ÿ�� ����
			for ( int i = 0; i < dt.Columns.Count; i++)
			{
				desc.Columns.Add( dt.Columns[i].ColumnName, dt.Columns[i].DataType );
				DataTypes[ i ] = dt.Columns[i].DataType.Name.ToUpper();
			}


			// �հ����� �ο� ����Ÿ���� �����ϱ� ���� �÷���..
			// �ο쵥��Ÿ�� 1
			// �հ�� 2
			// ������ SubTotal �� +1 ��
            desc.Columns.Add("iDataRowType", typeof(int));

			// ��Ż��
			DataRow TotalRow = desc.NewRow();

			// ������Ż ���񱳸� ���� �� �̸� ����. 
			if ( dt.Rows.Count > 0 )
			{
				for ( int i = 0; i < SubTotalColumnSize ; i++ )
				{
					SubTotal[ i ] =  (dt.Rows [0][ startSubTotalColumnPosition +i ]==null?null:dt.Rows [0][ startSubTotalColumnPosition + i ].ToString());
				}
				// base ����
				for ( int i = 0; i < Base.Length ; i++ )
				{
					Base[ i ] =  (dt.Rows [0][ i ]==null?null:dt.Rows [0][ i ].ToString());
				}

			}

			// ��Ż �߰���.. 
			desc.Rows.Add( TotalRow );

			// ��ü row �� ��ȸ��.. 
			for ( int row = 0; row <  dt.Rows.Count; row++ )
			{
				rows = desc.NewRow();

				isSuperChanged = false;

				// ��ü �÷��� ��ȸ��.. 
				for ( int column = 0; column < dt.Columns.Count; column++ )
				{
					// �κ��հ� ��� ������ ���鵵 �κ��հ踦 ���� ���Ͽ� �˻�. ( �߰��κк��� �κ��հ踦 ���� ���׸� ���� ���Ͽ� )
					if ( column < startSubTotalColumnPosition )
					{
						if ( Base[ column ] != (dt.Rows [row][column]==null?null:((string)dt.Rows [row][column])) )
						{
							isSuperChanged = true;
							Base[ column ] = (dt.Rows [row][column]==null?null:dt.Rows [row][column].ToString());
						}
					}
					// �κ��հ踦 ���� ���Ͽ� �����Ͱ� ������ ��
					else if ( column >= startSubTotalColumnPosition && column < ( startSubTotalColumnPosition + SubTotalColumnSize )  )
					{
						// �ڱ��ڽ��� Ʋ���ų� ������ �κ��հ谡 ����Ȱ��  ���������� ������ ����. 
						if ( SubTotal[ column-startSubTotalColumnPosition ] != (dt.Rows [row][column]==null?null:((string)dt.Rows [row][column])) || isSuperChanged )
						{	
							subTotalRows[ column-startSubTotalColumnPosition ] = desc.NewRow();
							
							// ���� ������Ż row �� �����ϰ� �ٽ� ���� ����.. �ϱ� ���Ͽ� 0 ���� �ʱ�ȭ
							for ( int subValue = 0; subValue < ( dt.Columns.Count - StartValueColumnPos ) ; subValue++ )
							{
								// ������Ż�� �����ΰ�츸 ������.. 
								if ( DataTypes[ StartValueColumnPos+subValue ] == "INT" || DataTypes[ StartValueColumnPos+subValue ] == "DOUBLE" ||
                                     DataTypes[StartValueColumnPos + subValue] == "INT32" || DataTypes[StartValueColumnPos + subValue] == "DECIMAL")
									subTotalRows[ column-startSubTotalColumnPosition ][ StartValueColumnPos+subValue ] = SubTotalValue [ column-startSubTotalColumnPosition, subValue ];

								SubTotalValue[ column-startSubTotalColumnPosition, subValue ] = 0;
								
							}

							// �κ��հ� ���� ������ ü��. Merge �� ���ؼ�. 
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

					// StartValueColumnPos �̻��� �÷��鸸 �����س���.. 
					// ��Ż�� ������Ż ���� �����..
					if ( column >= StartValueColumnPos )
					{
						// ���� Ÿ���� ������Ż�� �����
						if ( DataTypes[ column ] == "INT" || DataTypes[ column ] == "DOUBLE" ||
                             DataTypes[ column ] == "INT32" || DataTypes[column] == "DECIMAL")
						{
							// ��Ż�� ���..
							if ( dt.Rows[ row ][ column ] != null && dt.Rows[ row ][ column ] != DBNull.Value )
							{
								Total [ column-StartValueColumnPos ] += Convert.ToDouble  ( dt.Rows[ row ][ column ]);
						
								//���� ��Ż�� ������. 
								for ( int sub = 0; sub < SubTotal.Length; sub++ )
								{
									SubTotalValue[ sub, column-StartValueColumnPos ] += Convert.ToDouble  ( dt.Rows[ row ][ column ]);
								}
							}
						}
					}

					
					// �Ϲ� low data ����
					// Double Ÿ���� �ӵ��� ���ؼ� 0.009 ������ ������ ������ null ������ ������.
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
                				
				// �κ��հ迭 �߰�.. 
				// �������� �־�߸�.. ��.. 
				for ( int sub = subTotalRows.Length - 1; sub >= 0; sub-- )
				{
					if ( subTotalRows[ sub ] != null )
					{
						// ����Ÿ Ÿ���� �Է���.. 
						// �ο쵥��Ÿ�� 1
						// �հ�� 2
						// ������ SubTotal �� +1 ��, ù �κ��հ� ���� 3 ����..
						subTotalRows[ sub ][ iColumnType ] = sub + 3;
						desc.Rows.Add( subTotalRows[ sub ] );
						subTotalRows[ sub ] = null;
					}
				}
                
				// ����Ÿ Ÿ���� �Է���.. 
				// �ο쵥��Ÿ�� 1
				// �հ�� 2
				// ������ SubTotal �� +1 ��
				rows [ iColumnType ] = 1;

				desc.Rows.Add( rows );
			}

			// ��Ż���� ä��.. 
			TotalRow[ 0 ] = "Total";
			// ����Ÿ Ÿ���� �Է���.. 
			// �ο쵥��Ÿ�� 1
			// �հ�� 2
			TotalRow[ iColumnType ] = 2;
			for ( int i = StartValueColumnPos; i < dt.Columns.Count; i++ )
			{
				// ���� Ÿ���� ������Ż�� �����
				if ( DataTypes[ i ] == "INT" || DataTypes[ i ] == "DOUBLE" ||
                     DataTypes[i] == "INT32" || DataTypes[i] == "DECIMAL")
					TotalRow[ i ] = Total[ i -StartValueColumnPos ];
			}
            
			// �������� �����ִ� �κ��հ� �ڿ������� ä��.. 
			for ( int subValue = SubTotal.Length-1 ; subValue >= 0 ; subValue-- )
			{
				DataRow subTotalRow = desc.NewRow();

				for ( int col = StartValueColumnPos ; col < dt.Columns.Count; col++ )
				{
					if ( DataTypes[ col ] == "INT" || DataTypes[ col ] == "DOUBLE" ||
                         DataTypes[col] == "INT32" || DataTypes[col] == "DECIMAL")
						subTotalRow[ col ] = SubTotalValue [ subValue, col-StartValueColumnPos ];
				}
				
				// �κ��հ� ���� ������ ü��. 
				for ( int i = 0; i < (subValue+1+startSubTotalColumnPosition)  ; i++ )
				{
					subTotalRow[ i ] = dt.Rows[ dt.Rows.Count -1 ][ i ];
				}

				// ����Ÿ Ÿ���� �Է���.. 
				// �ο쵥��Ÿ�� 1
				// �հ�� 2
				// ������ SubTotal �� +1 ��, ù �κ��հ� ���� 3 ����..
				subTotalRow[ iColumnType ] = subValue + 3;
                subTotalRow[startSubTotalColumnPosition + subValue] = SubTotal[subValue] + " Sub Total";
				desc.Rows.Add( subTotalRow );
			}

			//
			// �� ���ϴ� FarPoint ���� => �ٸ� �׸��忡���� ��������..
			//
			
			// �÷��� �̸� �ʱ�ȭ ���� ���ϵ��� ����. 
			isPreCellsType = false;

			int [] merge = new int[ StartValueColumnPos ];

			// Merge ������ �����.. 
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
            
            // �׸��忡 ����Ÿ ���̺� ����
			this.DataSource = desc;

			// ����Ÿ�� ���� ��� ������ �ʱ�ȭ ���� ����. 
			if ( this.ActiveSheet.RowCount == 0 )
				return null;

            //Į���ش��� Note����
            SetNoteInfo();

			// ������ �ʱ�ȭ..
			this.ActiveSheet.FrozenRowCount = 1;
            // iDataRowType Ÿ���� ���� 
			this.ActiveSheet.ColumnCount = desc.Columns.Count - 1;

			// ��������.. 
			// fix �� �÷� ��ŭ �ݺ�
			for ( int i = 0 ; i < StartValueColumnPos ; i++ )
				this.ActiveSheet.Columns[i].BackColor = System.Drawing.Color.FromArgb(((System.Byte)(255)), ((System.Byte)(255)), ((System.Byte)(218)));;
			// ��Ż �÷�.
			this.ActiveSheet.Rows[ 0 ].BackColor = System.Drawing.Color.FromArgb(((System.Byte)(255)), ((System.Byte)(233)), ((System.Byte)(204)));

			// �հ� Formula ����
			if ( SumFormula != null )
			{
				for ( int cel = 0; cel < SumFormula.Length/2 ; cel++ )
				{
					this.ActiveSheet.Cells [ 0, Convert.ToInt32(SumFormula[cel, 0]) ].Formula = SumFormula[cel, 1];                    
				}
			}

			// Row Type�� ������ �Լ�..
			rowType = new int[ desc.Rows.Count ];
			rowType[ 0 ] = 2;	// ù��°�� ������ �հ���.

			// Formula ����( �κ��հ� + Low ����Ÿ )
			// �κ��հ� �÷�
			for ( int i = 1; i < desc.Rows.Count; i++ )
			{
				rowType[ i ] = ((int)desc.Rows[ i ][ iColumnType ]);

				// �ο쵥��Ÿ�� �հ�� �����ϰ� ��ĥ��.. 
				if (  rowType[ i ] > 2 )
				{
					for ( int j = ( (int)startSubTotalColumnPosition + (int)desc.Rows[ i ][ iColumnType ] - 3 ); j < this.ActiveSheet.ColumnCount; j++ )
					{
						this.ActiveSheet.Cells [ i, j ].BackColor = System.Drawing.Color.FromArgb(((System.Byte)(222)), ((System.Byte)(236)), ((System.Byte)(242)));	

					}
					// �κ� �հ�κ� ����.. 
					this.ActiveSheet.Cells [ i, ( (int)startSubTotalColumnPosition + (int)desc.Rows[ i ][ iColumnType ] - 3 ) ].ColumnSpan = StartValueColumnPos -  ( (int)startSubTotalColumnPosition + (int)desc.Rows[ i ][ iColumnType ] - 3 );
					this.ActiveSheet.Cells [ i, ( (int)startSubTotalColumnPosition + (int)desc.Rows[ i ][ iColumnType ] - 3 ) ].VerticalAlignment = CellVerticalAlignment.Center;
					this.ActiveSheet.Cells [ i, ( (int)startSubTotalColumnPosition + (int)desc.Rows[ i ][ iColumnType ] - 3 ) ].HorizontalAlignment = CellHorizontalAlignment.Left;

					// �κ� �հ� Formula ����
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
					// Low Data Formula ����
					if ( Formula != null )
					{
						for ( int cel = 0; cel < Formula.Length/2 ; cel++ )
						{
							this.ActiveSheet.Cells [ i, Convert.ToInt32(Formula[cel, 0]) ].Formula = Formula[cel, 1];
						}
					}

				}

			}

            // 0�� ǥ�� ���� ���� (�⺻�� ǥ�� ����)
            //if (this.isShowZero != true)
            //{
                // 0 �� blank �� �ٲ�..
                this.RPT_SetCellsType();
            //}

			isPreCellsType = true;

			// Merge �� �ٽ� ��.. 
			for ( int i = 0; i < merge.Length ; i++ )
			{
				if ( merge[i] == 1 )
					this.ActiveSheet.Columns[ i ].MergePolicy = FarPoint.Win.Spread.Model.MergePolicy.Always;
				else if ( merge[i] == 2 )
					this.ActiveSheet.Columns[ i ].MergePolicy = FarPoint.Win.Spread.Model.MergePolicy.None;
				else 
					this.ActiveSheet.Columns[ i ].MergePolicy = FarPoint.Win.Spread.Model.MergePolicy.Restricted;
			}	

			// �ο쵥��Ÿ�� 1
			// �հ�� 2
			// ������ SubTotal �� +1 ��, ù �κ��հ� ���� 3 ����..
			return rowType;
		}


        // ǥ������ �̿��Ͽ� �κ��հ踦 �����Ҷ�. 
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
                // ���õ��� �ʴ� �κ��� ����..
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

            this.ActiveSheet.Tag = iVisibleCnt + "^1";  //�κ��հ� ������ Į���� ����.
            
            //���õ� �κ��� ���� ������ �κе� ����
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

            //����� Row ����
            for (int i = this.ActiveSheet.Rows.Count-1; i >=0 ; i--)
            {
                if (this.ActiveSheet.Rows[i].Visible==false)
                    this.ActiveSheet.Rows[i].Remove();
            } 

            return rowType;
        }


        // ǥ������ �̿��Ͽ� �κ��հ踦 �����Ҷ�. 
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
                // ���õ��� �ʴ� �κ��� ����..
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

            this.ActiveSheet.Tag = iVisibleCnt + "^" + divideRowsSize;  //�κ��հ� ������ Į���� ����.
            
            //���õ� �κ��� ���� ������ �κе� ����
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

            //����� Row ����
            for (int i = this.ActiveSheet.Rows.Count - 1; i >= 0; i--)
            {
                if (this.ActiveSheet.Rows[i].Visible == false)
                    this.ActiveSheet.Rows[i].Remove();
            }

            return rowType;
        }


        /// <summary>
		///  �߰� : 2008.10.09. By ������
		///  2�� �̻��� �κ� �հ踦 �������.. DataSource �� ���� �����ϸ� �ȉ�. 
		/// </summary>
		/// <param name="dt">���� ����Ÿ ���̺�</param>
		/// <param name="startSubTotalColumnPosition">�κ��հ踦 ������ �÷�..</param>
		/// <param name="SubTotalColumnSize">�κ��հ踦 ����� �÷� ������</param>
		/// <param name="StartValueColumnPos">���� ����� ����Ÿ�� ����ִ� �÷�</param>
		/// <returns>Row Ÿ�� : �ο쵥��Ÿ�� 1, �հ�� 2, ������ SubTotal �� +1 ��, ù �κ��հ� ���� 3 ����..</returns>
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

			// ���ġ�� ��� ���̺�.. 
			DataTable desc = new DataTable();
			// �ο쵥��Ÿ ������ ��� �÷�..
			int iColumnType = dt.Columns.Count;
			// ��Ż���� ������ �迭
			double [] Total = new double [ dt.Columns.Count - StartValueColumnPos ];
			// ���� ��Ż���� ������ �迭
			double [ , ] SubTotalValue = new double [ SubTotalColumnSize, dt.Columns.Count - StartValueColumnPos ];
			// ������Ż�� ������ �Ǵ� ��
			string [] SubTotal = new string [ SubTotalColumnSize ];
			// ������Ż�� �߰����� �����Ҷ� ������ ���ذ���..
			string [] Base = new string [ StartValueColumnPos ];
			DataRow rows = null;
			// �κ� �հ�� ����Ÿ row
			DataRow [] subTotalRows = new DataRow[ SubTotalColumnSize ];
			// �κ��հ踦 ���� ����� �κ��հ谡 �ٲ�� ������ �ϴ��� �κ��հ踦 �ٲٱ�����.
			bool isSuperChanged;
			// ����Ÿ Ÿ���� �����س��� ���
			string [] DataTypes = new string[ dt.Columns.Count ];
			// DataTable �� Row�� { �Ϲ�, �հ�, �κ��հ� } �� ������ �迭
			int [] rowType;

			// �÷� Ÿ�� ����
			for ( int i = 0; i < dt.Columns.Count; i++)
			{
				desc.Columns.Add( dt.Columns[i].ColumnName, dt.Columns[i].DataType );
				DataTypes[ i ] = dt.Columns[i].DataType.Name.ToUpper();
			}

			// �հ����� �ο� ����Ÿ���� �����ϱ� ���� �÷���..
			// �ο쵥��Ÿ�� 1
			// �հ�� 2
			// ������ SubTotal �� +1 ��
			desc.Columns.Add("iDataRowType", typeof(int) );

			// ��Ż��
			DataRow TotalRow = desc.NewRow();

			// ������Ż ���񱳸� ���� �� �̸� ����. 
			if ( dt.Rows.Count > 0 )
			{
				for ( int i = 0; i < SubTotalColumnSize ; i++ )
				{
					SubTotal[ i ] =  (dt.Rows [0][ startSubTotalColumnPosition +i ]==null?null:dt.Rows [0][ startSubTotalColumnPosition + i ].ToString());
				}
				// base ����
				for ( int i = 0; i < Base.Length ; i++ )
				{
					Base[ i ] =  (dt.Rows [0][ i ]==null?null:dt.Rows [0][ i ].ToString());
				}
			}

			// ��Ż �߰���.. 
			desc.Rows.Add( TotalRow );

			// ��ü row �� ��ȸ��.. 
			for ( int row = 0; row <  dt.Rows.Count; row++ )
			{
				rows = desc.NewRow();

				isSuperChanged = false;

				// ��ü �÷��� ��ȸ��.. 
				for ( int column = 0; column < dt.Columns.Count; column++ )
				{
					// �κ��հ� ��� ������ ���鵵 �κ��հ踦 ���� ���Ͽ� �˻�. ( �߰��κк��� �κ��հ踦 ���� ���׸� ���� ���Ͽ� )
					if ( column < startSubTotalColumnPosition )
					{
						if ( Base[ column ] != (dt.Rows [row][column]==null?null:((string)dt.Rows [row][column])) )
						{
							isSuperChanged = true;
							Base[ column ] = (dt.Rows [row][column]==null?null:dt.Rows [row][column].ToString());
						}
					}
						// �κ��հ踦 ���� ���Ͽ� �����Ͱ� ������ ��
					else if ( column >= startSubTotalColumnPosition && column < ( startSubTotalColumnPosition + SubTotalColumnSize )  )
					{
						// �ڱ��ڽ��� Ʋ���ų� ������ �κ��հ谡 ����Ȱ��  ���������� ������ ����. 
						if ( SubTotal[ column-startSubTotalColumnPosition ] != (dt.Rows [row][column]==null?null:((string)dt.Rows [row][column])) || isSuperChanged )
						{	
							subTotalRows[ column-startSubTotalColumnPosition ] = desc.NewRow();
							
							// ���� ������Ż row �� �����ϰ� �ٽ� ���� ����.. �ϱ� ���Ͽ� 0 ���� �ʱ�ȭ
							for ( int subValue = 0; subValue < ( dt.Columns.Count - StartValueColumnPos ) ; subValue++ )
							{
								// ������Ż�� �����ΰ�츸 ������.. 
								if ( DataTypes[ StartValueColumnPos+subValue ] == "INT" || DataTypes[ StartValueColumnPos+subValue ] == "DOUBLE" ||
                                     DataTypes[StartValueColumnPos + subValue] == "INT32" || DataTypes[StartValueColumnPos + subValue] == "DECIMAL")
									subTotalRows[ column-startSubTotalColumnPosition ][ StartValueColumnPos+subValue ] = SubTotalValue [ column-startSubTotalColumnPosition, subValue ];

								SubTotalValue[ column-startSubTotalColumnPosition, subValue ] = 0;								
							}

							// �κ��հ� ���� ������ ü��. Merge �� ���ؼ�. 
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

					// StartValueColumnPos �̻��� �÷��鸸 �����س���.. 
					// ��Ż�� ������Ż ���� �����..
					if ( column >= StartValueColumnPos )
					{
						// ���� Ÿ���� ������Ż�� �����
						if ( DataTypes[ column ] == "INT" || DataTypes[ column ] == "DOUBLE" ||
                             DataTypes[column] == "INT32" || DataTypes[column] == "DECIMAL")
						{
							// ��Ż�� ���..
							if ( dt.Rows[ row ][ column ] != null && dt.Rows[ row ][ column ] != DBNull.Value )
							{
								Total [ column-StartValueColumnPos ] += Convert.ToDouble  ( dt.Rows[ row ][ column ]);
						
								//���� ��Ż�� ������. 
								for ( int sub = 0; sub < SubTotal.Length; sub++ )
								{
									SubTotalValue[ sub, column-StartValueColumnPos ] += Convert.ToDouble  ( dt.Rows[ row ][ column ]);
								}
							}
						}
					}

					// �Ϲ� low data ����
					// Double Ÿ���� �ӵ��� ���ؼ� 0.009 ������ ������ ������ null ������ ������.
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
				
				// �κ��հ迭 �߰�.. 
				// �������� �־�߸�.. ��.. 
				for ( int sub = subTotalRows.Length - 1; sub >= 0; sub-- )
				{
					if ( subTotalRows[ sub ] != null )
					{
						// ����Ÿ Ÿ���� �Է���.. 
						// �ο쵥��Ÿ�� 1
						// �հ�� 2
						// ������ SubTotal �� +1 ��, ù �κ��հ� ���� 3 ����..
						subTotalRows[ sub ][ iColumnType ] = sub + 3;
						desc.Rows.Add( subTotalRows[ sub ] );
						subTotalRows[ sub ] = null;
					}
				}

				// ����Ÿ Ÿ���� �Է���.. 
				// �ο쵥��Ÿ�� 1
				// �հ�� 2
				// ������ SubTotal �� +1 ��
				rows [ iColumnType ] = 1;

				desc.Rows.Add( rows );
			}

			// ��Ż���� ä��.. 
			TotalRow[ 0 ] = "Total";
			// ����Ÿ Ÿ���� �Է���.. 
			// �ο쵥��Ÿ�� 1
			// �հ�� 2
			TotalRow[ iColumnType ] = 2;
			for ( int i = StartValueColumnPos; i < dt.Columns.Count; i++ )
			{
				// ���� Ÿ���� ������Ż�� �����
				if ( DataTypes[ i ] == "INT" || DataTypes[ i ] == "DOUBLE" ||
                     DataTypes[i] == "INT32" || DataTypes[i] == "DECIMAL")
					TotalRow[ i ] = Total[ i -StartValueColumnPos ];
			}

			// �������� �����ִ� �κ��հ� �ڿ������� ä��.. 
			for ( int subValue = SubTotal.Length-1 ; subValue >= 0 ; subValue-- )
			{
				DataRow subTotalRow = desc.NewRow();

				for ( int col = StartValueColumnPos ; col < dt.Columns.Count; col++ )
				{
					if ( DataTypes[ col ] == "INT" || DataTypes[ col ] == "DOUBLE" ||
                         DataTypes[col] == "INT32" || DataTypes[col] == "DECIMAL")
						subTotalRow[ col ] = SubTotalValue [ subValue, col-StartValueColumnPos ];
				}
				
				// �κ��հ� ���� ������ ü��. 
				for ( int i = 0; i < (subValue+1+startSubTotalColumnPosition)  ; i++ )
				{
					subTotalRow[ i ] = dt.Rows[ dt.Rows.Count -1 ][ i ];
				}

				// ����Ÿ Ÿ���� �Է���.. 
				// �ο쵥��Ÿ�� 1
				// �հ�� 2
				// ������ SubTotal �� +1 ��, ù �κ��հ� ���� 3 ����..
				subTotalRow[ iColumnType ] = subValue + 3;
                subTotalRow[startSubTotalColumnPosition + subValue] = SubTotal[subValue] + " Sub Total";
				desc.Rows.Add( subTotalRow );
			}
			
			//
			// �� ���ϴ� FarPoint ���� => �ٸ� �׸��忡���� ��������..
			//
			
			// �÷��� �̸� �ʱ�ȭ ���� ���ϵ��� ����. 
			isPreCellsType = false;

			int [] merge = new int[ StartValueColumnPos ];

			// Merge ������ �����.. 
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

			// �׸��忡 ����Ÿ ���̺� ����
			this.DataSource = desc;

            
			// ����Ÿ�� ���� ��� ������ �ʱ�ȭ ���� ����. 
			if ( this.ActiveSheet.RowCount == 0 )
				return null;

            //Į���ش��� Note����
            SetNoteInfo();

			// ������ �ʱ�ȭ..
			this.ActiveSheet.FrozenRowCount = 1;
            // iDataRowType Ÿ���� ���� 
			this.ActiveSheet.ColumnCount = desc.Columns.Count - 1;
            			
			this.RPT_DivideRows(basisColumnSize, divideRowsSize, repeatColumnSize );

			// ��������.. 
			// fix �� �÷� ��ŭ �ݺ�
			for ( int i = 0 ; i < StartValueColumnPos ; i++ )
				this.ActiveSheet.Columns[i].BackColor = System.Drawing.Color.FromArgb(((System.Byte)(255)), ((System.Byte)(255)), ((System.Byte)(218)));;
			// ��Ż �÷�.
			for ( int k = 0; k < divideRowsSize; k++ )
				this.ActiveSheet.Rows[ 0+k ].BackColor = System.Drawing.Color.FromArgb(((System.Byte)(255)), ((System.Byte)(233)), ((System.Byte)(204)));

			// Row Type�� ������ �Լ�..
			rowType = new int[ desc.Rows.Count ];
			rowType[ 0 ] = 2;	// ù��°�� ������ �հ���.

			///////////////////////////////////////////////////////////////////////////////////
			// Formula ����( �κ��հ� + Low ����Ÿ )
			// �κ��հ� �÷�
			for ( int i = 1; i < desc.Rows.Count; i++ )
			{
				rowType[ i ] = ((int)desc.Rows[ i ][ iColumnType ]);

				// �ο쵥��Ÿ�� �հ�� �����ϰ� ��ĥ��.. 
				if (  rowType[ i ] > 2 )
				{
					for ( int j = ( (int)startSubTotalColumnPosition + (int)desc.Rows[ i ][ iColumnType ] - 3 ); j < this.ActiveSheet.ColumnCount; j++ )
					{
						for ( int k = 0; k < divideRowsSize; k++ )
							this.ActiveSheet.Cells [ i*divideRowsSize+k, j ].BackColor = System.Drawing.Color.FromArgb(((System.Byte)(222)), ((System.Byte)(236)), ((System.Byte)(242)));	

					}
						// �κ� �հ�κ� ����.. 
					this.ActiveSheet.Cells [ i*divideRowsSize, ( (int)startSubTotalColumnPosition + (int)desc.Rows[ i ][ iColumnType ] - 3 ) ].ColumnSpan = StartValueColumnPos -  ( (int)startSubTotalColumnPosition + (int)desc.Rows[ i ][ iColumnType ] - 3 );
					this.ActiveSheet.Cells [ i*divideRowsSize, ( (int)startSubTotalColumnPosition + (int)desc.Rows[ i ][ iColumnType ] - 3 ) ].VerticalAlignment = CellVerticalAlignment.Center;
					this.ActiveSheet.Cells [ i*divideRowsSize, ( (int)startSubTotalColumnPosition + (int)desc.Rows[ i ][ iColumnType ] - 3 ) ].HorizontalAlignment = CellHorizontalAlignment.Left;
					this.ActiveSheet.Cells [ i*divideRowsSize, ( (int)startSubTotalColumnPosition + (int)desc.Rows[ i ][ iColumnType ] - 3 ) ].RowSpan = divideRowsSize;
				}
			}
			///////////////////////////////////////////////////////////

			// 0 �� blank �� �ٲ�..
			this.RPT_SetCellsType();

			isPreCellsType = true;

			// Merge �� �ٽ� ��.. 
			for ( int i = 0; i < merge.Length ; i++ )
			{
				if ( merge[i] == 1 )
					this.ActiveSheet.Columns[ i ].MergePolicy = FarPoint.Win.Spread.Model.MergePolicy.Always;
				else if ( merge[i] == 2 )
					this.ActiveSheet.Columns[ i ].MergePolicy = FarPoint.Win.Spread.Model.MergePolicy.None;
				else 
					this.ActiveSheet.Columns[ i ].MergePolicy = FarPoint.Win.Spread.Model.MergePolicy.Restricted;
			}	

			// �ο쵥��Ÿ�� 1
			// �հ�� 2
			// ������ SubTotal �� +1 ��, ù �κ��հ� ���� 3 ����..
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


                        DialogResult result = MessageBox.Show("����� Excel ������ �ٷ� Open �Ͻðڽ��ϱ�?", "HANA Micron Web Report", MessageBoxButtons.OKCancel);
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


                        DialogResult result = MessageBox.Show("����� Excel ������ �ٷ� Open �Ͻðڽ��ϱ�?", "HANA Micron Web Report", MessageBoxButtons.OKCancel);
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

        // 2010-09-03-������ : �������� �̸� ����ڰ� ���� �ϱ� ���� �߰���.
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


                        DialogResult result = MessageBox.Show("����� Excel ������ �ٷ� Open �Ͻðڽ��ϱ�?", "HANA Micron Web Report", MessageBoxButtons.OKCancel);
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
        /// 2017-12-15-������ : Sub Total, Grand Total ��հ� ���ϱ�
        /// </summary>
        /// <param name="nSampleNormalRowPos"></param>
        /// <param name="nColPos"></param>
        /// <param name="nGroupCount"></param>
        /// <param name="bWithNull"></param>
        public void RPT_SetAvgSubTotalAndGrandTotal(int nSampleNormalRowPos, int nColPos, int nGroupCount, bool bWithNull)
        {
            double sum = 0;
            double totalSum = 0;
            double subSum = 0; // ���� �з��� ���� ��Ż�� ��

            double divide = 0;
            double totalDivide = 0;
            double subDivide = 0; // ���� �з��� ���� ��Ż ����

            try
            {
                Color color = this.ActiveSheet.Cells[nSampleNormalRowPos, nColPos].BackColor;

                for (int i = 1; i < this.ActiveSheet.Rows.Count; i++)
                {
                    if (this.ActiveSheet.Cells[i, nColPos].BackColor == color)
                    {
                        sum += Convert.ToDouble(this.ActiveSheet.Cells[i, nColPos].Value);

                        // bWithNull ���� true �̸� null, "" �� �����Ͽ� ��հ� ���, false �̸� null, "" �� �����ϰ� ��հ� ���
                        if (!bWithNull && (this.ActiveSheet.Cells[i, nColPos].Value == null || this.ActiveSheet.Cells[i, nColPos].Value.ToString().Trim() == ""))
                            continue;

                        divide += 1;
                    }
                    else
                    {
                        if (divide != 0)
                        {

                            this.ActiveSheet.Cells[i, nColPos].Value = sum / divide;

                            if (nGroupCount > 2) // Group �׸񿡼� üũ�Ȱ� 2�� �̻��ΰ�(������Ż�� 2 Depth �̻��ΰ�)
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
                else if (divide != 0) // ���� Subtotal �� ���� ���
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

                // GrandTotal �κ�
                dBunja = Convert.ToDouble(this.ActiveSheet.Cells[0, nBunjaColPos].Value);
                dBunmo = Convert.ToDouble(this.ActiveSheet.Cells[0, nBunmoColPos].Value);

                if (dBunmo > 0)
                {
                    this.ActiveSheet.Cells[0, nColPos].Value = dBunja / dBunmo * 100;
                }

                // SubTotal �κ�
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
