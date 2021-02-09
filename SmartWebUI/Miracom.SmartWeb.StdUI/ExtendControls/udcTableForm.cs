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
using System.Drawing;
using System.Collections;
using System.Text;
using System.ComponentModel;
using System.Windows.Forms;
using FarPoint.Win.Spread.Model;
using FarPoint.Win.Spread;

namespace Miracom.SmartWeb.UI.Controls
{
	/// <summary>
	/// udcTableForm에 대한 요약 설명입니다.
	/// </summary>
    public partial class udcTableForm : System.Windows.Forms.Form
	{
		private int rowCount;
        private System.Windows.Forms.Panel crt_Panel1;
        private System.Windows.Forms.Panel crt_Panel3;
        private Miracom.SmartWeb.UI.Controls.udcFarPoint ctr_FarPoint1;
		private FarPoint.Win.Spread.SheetView ctr_FarPoint1_Sheet1;
		private System.Windows.Forms.Button upButton;
		private System.Windows.Forms.Button downButton;
        private System.Windows.Forms.Label label1;
		private System.ComponentModel.IContainer components;

		public udcTableForm()
		{
			//
			// Windows Form 디자이너 지원에 필요합니다.
			//
			InitializeComponent();		
			SetGrid();
			
			//
			// TODO: InitializeComponent를 호출한 다음 생성자 코드를 추가합니다.
			//

		}

		public string SelectedValueToQuery
		{
			get
			{
				return getQueryString( 3, false );
			}
		}

		public string SelectedValueToQueryContainNull
		{
			get
			{
				return getQueryString( 3, true );
			}
		}

		public string SelectedValue2ToQuery
		{
			get
			{
				return getQueryString( 4, false );
			}
		}

		public string SelectedValue2ToQueryContainNull
		{
			get
			{
				return getQueryString( 4, true );
			}
		}

        public string SelectedValue3ToQuery
        {
            get
            {
                return getQueryString(5, false);
            }
        }

        public string SelectedValue3ToQueryContainNull
        {
            get
            {
                return getQueryString(5, true);
            }
        }

        public string SelectedValue4ToQuery
        {
            get
            {
                return getQueryString(6, false);
            }
        }

        public string SelectedValue4ToQueryContainNull
        {
            get
            {
                return getQueryString(6, true);
            }
        }

        // QueryCond5 추가 (2009.08.24 임종우)
        public string SelectedValue5ToQuery
        {
            get
            {
                return getQueryString(7, false);
            }
        }

        public string SelectedValue5ToQueryContainNull
        {
            get
            {
                return getQueryString(7, true);
            }
        }

		public ArrayList SelectedValueToArrayListToValuedTreeNode
		{
			get
			{
				ArrayList array = new ArrayList(ctr_FarPoint1_Sheet1.Rows.Count);
				for ( int i = 0; i < ctr_FarPoint1_Sheet1.Rows.Count; i++ )
				{
					ValuedTreeNode node = new ValuedTreeNode( ctr_FarPoint1_Sheet1.Cells[i,2].Text, ctr_FarPoint1_Sheet1.Cells[i,1].Text.ToUpper());

					// 초기 순서를 리턴해줌
                    // QueryCond5 추가 (2009.08.24 임종우) 
                    // QueryCond 추가시 node.Seq = Convert.ToInt32( ctr_FarPoint1_Sheet1.Cells[i,8].Value ); --> 숫자 + 1 해줌
					node.Seq = Convert.ToInt32( ctr_FarPoint1_Sheet1.Cells[i,8].Value );
					array.Add( node );
				}

				return array;
			}
		}
        
        // --- Sortinit() 함수를 여러번 호출 하게 될 경우에 사용 됨  by MgKim ---
        public ArrayList SelectedValueToArrayListToValuedTreeNode_New
        {
            get
            {
                ArrayList array = new ArrayList(ctr_FarPoint1_Sheet1.Rows.Count);
                for (int i = 0; i < ctr_FarPoint1_Sheet1.Rows.Count; i++)
                {
                    ValuedTreeNode node = new ValuedTreeNode(ctr_FarPoint1_Sheet1.Cells[i, 2].Text, ctr_FarPoint1_Sheet1.Cells[i, 1].Text.ToUpper());                    
                    node.Seq = i;       // 기존 소스를 수정한 부분
                    array.Add(node);
                }

                return array;
            }
        }


		private string getQueryString(int index, bool containNulls)
		{
			System.Text.StringBuilder sb = new System.Text.StringBuilder(100);
			for ( int i = 0; i < ctr_FarPoint1_Sheet1.Rows.Count; i++ )
			{
					if ( ctr_FarPoint1_Sheet1.Cells[i,1].Text.ToUpper() == "TRUE"  )
					{
						sb.Append( ctr_FarPoint1_Sheet1.Cells[i,index].Text );
						sb.Append( ", ");
					}
					else
					{
						if ( containNulls )
						{
							sb.Append( " ' ', " );
						}
					}
			}

			// 젤 마지막 " , " 제거
			if ( sb.ToString().Length > 2 )
				return sb.ToString().Substring(0, sb.ToString().Length - 2);
			else
				return null;
			
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

		#region Windows Form 디자이너에서 생성한 코드
		/// <summary>
		/// 디자이너 지원에 필요한 메서드입니다.
		/// 이 메서드의 내용을 코드 편집기로 수정하지 마십시오.
		/// </summary>
		private void InitializeComponent()
		{
            this.components = new System.ComponentModel.Container();
            FarPoint.Win.Spread.CellType.CheckBoxCellType checkBoxCellType8 = new FarPoint.Win.Spread.CellType.CheckBoxCellType();
            FarPoint.Win.Spread.CellType.CheckBoxCellType checkBoxCellType9 = new FarPoint.Win.Spread.CellType.CheckBoxCellType();
            FarPoint.Win.Spread.CellType.CheckBoxCellType checkBoxCellType10 = new FarPoint.Win.Spread.CellType.CheckBoxCellType();
            FarPoint.Win.Spread.CellType.CheckBoxCellType checkBoxCellType11 = new FarPoint.Win.Spread.CellType.CheckBoxCellType();
            FarPoint.Win.Spread.CellType.CheckBoxCellType checkBoxCellType12 = new FarPoint.Win.Spread.CellType.CheckBoxCellType();
            FarPoint.Win.Spread.CellType.CheckBoxCellType checkBoxCellType13 = new FarPoint.Win.Spread.CellType.CheckBoxCellType();
            FarPoint.Win.Spread.CellType.CheckBoxCellType checkBoxCellType14 = new FarPoint.Win.Spread.CellType.CheckBoxCellType();
            this.crt_Panel3 = new System.Windows.Forms.Panel();
            this.ctr_FarPoint1 = new Miracom.SmartWeb.UI.Controls.udcFarPoint(this.components);
            this.ctr_FarPoint1_Sheet1 = new FarPoint.Win.Spread.SheetView();
            this.crt_Panel1 = new System.Windows.Forms.Panel();
            this.label1 = new System.Windows.Forms.Label();
            this.downButton = new System.Windows.Forms.Button();
            this.upButton = new System.Windows.Forms.Button();
            this.crt_Panel3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ctr_FarPoint1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ctr_FarPoint1_Sheet1)).BeginInit();
            this.crt_Panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // crt_Panel3
            // 
            this.crt_Panel3.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.crt_Panel3.Controls.Add(this.ctr_FarPoint1);
            this.crt_Panel3.Controls.Add(this.crt_Panel1);
            this.crt_Panel3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.crt_Panel3.Location = new System.Drawing.Point(0, 0);
            this.crt_Panel3.Name = "crt_Panel3";
            this.crt_Panel3.Size = new System.Drawing.Size(219, 52);
            this.crt_Panel3.TabIndex = 2;
            // 
            // ctr_FarPoint1
            // 
            this.ctr_FarPoint1.About = "4.0.2001.2005";
            this.ctr_FarPoint1.AccessibleDescription = "ctr_FarPoint1, Sheet1, Row 0, Column 0, ";
            this.ctr_FarPoint1.BackColor = System.Drawing.SystemColors.Control;
            this.ctr_FarPoint1.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.ctr_FarPoint1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ctr_FarPoint1.HorizontalScrollBarPolicy = FarPoint.Win.Spread.ScrollBarPolicy.Never;
            this.ctr_FarPoint1.Location = new System.Drawing.Point(0, 30);
            this.ctr_FarPoint1.Name = "ctr_FarPoint1";
            this.ctr_FarPoint1.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.ctr_FarPoint1.RPT_IsPreCellsType = true;
            this.ctr_FarPoint1.Sheets.AddRange(new FarPoint.Win.Spread.SheetView[] {
            this.ctr_FarPoint1_Sheet1});
            this.ctr_FarPoint1.Size = new System.Drawing.Size(217, 20);
            this.ctr_FarPoint1.TabIndex = 0;
            this.ctr_FarPoint1.TabStripPolicy = FarPoint.Win.Spread.TabStripPolicy.Never;
            this.ctr_FarPoint1.VerticalScrollBarPolicy = FarPoint.Win.Spread.ScrollBarPolicy.Never;
            // 
            // ctr_FarPoint1_Sheet1
            // 
            this.ctr_FarPoint1_Sheet1.Reset();
            this.ctr_FarPoint1_Sheet1.SheetName = "Sheet1";
            // Formulas and custom names must be loaded with R1C1 reference style
            this.ctr_FarPoint1_Sheet1.ReferenceStyle = FarPoint.Win.Spread.Model.ReferenceStyle.R1C1;
            this.ctr_FarPoint1_Sheet1.ColumnCount = 3;
            this.ctr_FarPoint1_Sheet1.RowCount = 7;
            this.ctr_FarPoint1_Sheet1.Cells.Get(0, 1).CellType = checkBoxCellType8;
            this.ctr_FarPoint1_Sheet1.Cells.Get(1, 1).CellType = checkBoxCellType9;
            this.ctr_FarPoint1_Sheet1.Cells.Get(2, 1).CellType = checkBoxCellType10;
            this.ctr_FarPoint1_Sheet1.Cells.Get(3, 1).CellType = checkBoxCellType11;
            this.ctr_FarPoint1_Sheet1.Cells.Get(4, 1).CellType = checkBoxCellType12;
            this.ctr_FarPoint1_Sheet1.Cells.Get(5, 1).CellType = checkBoxCellType13;
            this.ctr_FarPoint1_Sheet1.Cells.Get(6, 1).CellType = checkBoxCellType14;
            this.ctr_FarPoint1_Sheet1.DataAutoCellTypes = false;
            this.ctr_FarPoint1_Sheet1.RowHeader.AutoText = FarPoint.Win.Spread.HeaderAutoText.Blank;
            this.ctr_FarPoint1_Sheet1.RowHeader.Columns.Default.Resizable = false;
            this.ctr_FarPoint1_Sheet1.SelectionStyle = FarPoint.Win.Spread.SelectionStyles.SelectionColors;
            this.ctr_FarPoint1_Sheet1.ReferenceStyle = FarPoint.Win.Spread.Model.ReferenceStyle.A1;
            // 
            // crt_Panel1
            // 
            this.crt_Panel1.BackColor = System.Drawing.SystemColors.Menu;
            this.crt_Panel1.Controls.Add(this.label1);
            this.crt_Panel1.Controls.Add(this.downButton);
            this.crt_Panel1.Controls.Add(this.upButton);
            this.crt_Panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.crt_Panel1.Location = new System.Drawing.Point(0, 0);
            this.crt_Panel1.Name = "crt_Panel1";
            this.crt_Panel1.Size = new System.Drawing.Size(217, 30);
            this.crt_Panel1.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.Font = new System.Drawing.Font("굴림", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.label1.Location = new System.Drawing.Point(44, 5);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(36, 20);
            this.label1.TabIndex = 2;
            this.label1.Text = "Sequence";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // downButton
            // 
            this.downButton.Image = global::Miracom.SmartWeb.UI.Properties.Resources._320datasort;
            this.downButton.Location = new System.Drawing.Point(110, 5);
            this.downButton.Name = "downButton";
            this.downButton.Size = new System.Drawing.Size(20, 20);
            this.downButton.TabIndex = 1;
            this.downButton.Click += new System.EventHandler(this.downButton_Click);
            // 
            // upButton
            // 
            this.upButton.Image = global::Miracom.SmartWeb.UI.Properties.Resources._320datasort_up;
            this.upButton.Location = new System.Drawing.Point(86, 5);
            this.upButton.Name = "upButton";
            this.upButton.Size = new System.Drawing.Size(20, 20);
            this.upButton.TabIndex = 0;
            this.upButton.Click += new System.EventHandler(this.upButton_Click);
            // 
            // udcTableForm
            // 
            this.AutoScaleBaseSize = new System.Drawing.Size(6, 14);
            this.ClientSize = new System.Drawing.Size(219, 52);
            this.Controls.Add(this.crt_Panel3);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "udcTableForm";
            this.crt_Panel3.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.ctr_FarPoint1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ctr_FarPoint1_Sheet1)).EndInit();
            this.crt_Panel1.ResumeLayout(false);
            this.ResumeLayout(false);

		}
		#endregion



		#region IBaseExtendForm 멤버

		public void AutoBinding()
		{
		}

		#endregion

		private void SetGrid()
		{
			ctr_FarPoint1_Sheet1.Reset();
			ctr_FarPoint1_Sheet1.ReferenceStyle = ReferenceStyle.A1;
            ctr_FarPoint1_Sheet1.ColumnCount = 9; // QueryCond5 추가 (2009.08.24 임종우) - QueryCond 추가시 +1 해줌
			ctr_FarPoint1_Sheet1.AutoGenerateColumns = false;
			ctr_FarPoint1_Sheet1.ColumnHeader.Cells[0,0].Text = "No.";
			ctr_FarPoint1_Sheet1.ColumnHeader.Cells[0,0].Column.Resizable = false;
			ctr_FarPoint1_Sheet1.ColumnHeader.Cells[0,0].Column.Width = 30;
			ctr_FarPoint1_Sheet1.ColumnHeader.Cells[0,1].Text = "Selection";
			ctr_FarPoint1_Sheet1.ColumnHeader.Cells[0,1].Column.Resizable = false;
			ctr_FarPoint1_Sheet1.ColumnHeader.Cells[0,1].Column.Width = 40;
			ctr_FarPoint1_Sheet1.ColumnHeader.Cells[0,2].Text = "Item";
			ctr_FarPoint1_Sheet1.ColumnHeader.Cells[0,2].Column.Resizable = false;
			ctr_FarPoint1_Sheet1.ColumnHeader.Cells[0,2].Column.Width = 100;
			ctr_FarPoint1_Sheet1.ColumnHeader.Cells[0,3].Text = "";
			ctr_FarPoint1_Sheet1.ColumnHeader.Cells[0,3].Column.Resizable = false;
			ctr_FarPoint1_Sheet1.ColumnHeader.Cells[0,3].Column.Width = 0;
			ctr_FarPoint1_Sheet1.ColumnHeader.Cells[0,3].Column.Visible = true;
			ctr_FarPoint1_Sheet1.ColumnHeader.Cells[0,4].Text = "";
			ctr_FarPoint1_Sheet1.ColumnHeader.Cells[0,4].Column.Resizable = false;
			ctr_FarPoint1_Sheet1.ColumnHeader.Cells[0,4].Column.Width = 0;
            ctr_FarPoint1_Sheet1.ColumnHeader.Cells[0,4].Column.Visible = true;
            ctr_FarPoint1_Sheet1.ColumnHeader.Cells[0, 5].Text = "";
            ctr_FarPoint1_Sheet1.ColumnHeader.Cells[0, 5].Column.Resizable = false;
            ctr_FarPoint1_Sheet1.ColumnHeader.Cells[0, 5].Column.Width = 0;
            ctr_FarPoint1_Sheet1.ColumnHeader.Cells[0, 5].Column.Visible = true;
            ctr_FarPoint1_Sheet1.ColumnHeader.Cells[0, 6].Text = "";
            ctr_FarPoint1_Sheet1.ColumnHeader.Cells[0, 6].Column.Resizable = false;
            ctr_FarPoint1_Sheet1.ColumnHeader.Cells[0, 6].Column.Width = 0;
            ctr_FarPoint1_Sheet1.ColumnHeader.Cells[0, 6].Column.Visible = true;
			// 처음의 상태를 유지하기 위하여
			ctr_FarPoint1_Sheet1.ColumnHeader.Cells[0,7].Text = "";
			ctr_FarPoint1_Sheet1.ColumnHeader.Cells[0,7].Column.Resizable = false;
			ctr_FarPoint1_Sheet1.ColumnHeader.Cells[0,7].Column.Width = 0;
            ctr_FarPoint1_Sheet1.ColumnHeader.Cells[0, 7].Column.Visible = true;

            // QueryCond5 추가 (2009.08.24 임종우) - QueryCond 추가시 동일하게 속성 추가 해줌.
            ctr_FarPoint1_Sheet1.ColumnHeader.Cells[0, 8].Text = "";
            ctr_FarPoint1_Sheet1.ColumnHeader.Cells[0, 8].Column.Resizable = false;
            ctr_FarPoint1_Sheet1.ColumnHeader.Cells[0, 8].Column.Width = 0;
            ctr_FarPoint1_Sheet1.ColumnHeader.Cells[0, 8].Column.Visible = true;
			ctr_FarPoint1_Sheet1.ColumnHeader.DefaultStyle.BackColor =	System.Drawing.Color.LightBlue;
			ctr_FarPoint1_Sheet1.RowHeaderColumnCount = 0;
			ctr_FarPoint1_Sheet1.Rows.Count = 0;			
		}

        public void Clear()
        {
            ctr_FarPoint1_Sheet1.Rows.Count = 0;
            rowCount = 0;                           // 20150624-clearlim : rowCount 초기화
        }

        public int GetCheckedValue(int subtotal_cnt)
        {
            int value = 0;
            for (int i = 0; i < this.ctr_FarPoint1.ActiveSheet.Rows.Count; i++)
            {
                if(Convert.ToBoolean(ctr_FarPoint1.ActiveSheet.Cells[i, 1].Value) == true)
                {
                    if(subtotal_cnt==1)
                    {
                        value = i;
                        break;
                    }
                    subtotal_cnt--;
                }            
            }
            return value;
        }        
                
		public void AddRow(string Text, object values, bool isSelected)
		{
            AddRow(LanguageFunction.FindLanguage(Text, 0), values, values, values, values, values, isSelected);
		}

        public void AddRow(string Text, object values1, object values2, bool isSelected)
        {
            AddRow(LanguageFunction.FindLanguage(Text, 0), values1, values2, values2, values2, values2, isSelected);
        }

        public void AddRow(string Text, object values1, object values2, object values3, bool isSelected)
        {
            AddRow(LanguageFunction.FindLanguage(Text, 0), values1, values2, values3, values3, values3, isSelected);
        }

        public void AddRow(string Text, object values1, object values2, object values3, object values4, bool isSelected)
		{
            AddRow(LanguageFunction.FindLanguage(Text, 0), values1, values2, values3, values4, values4, isSelected);
		}

        // QueryCond5 추가 (2009.08.24 임종우)
        // QueryCond 추가시 인자값에 맞는 메소드 추가 할것.
        // 주의!! 메소드 추가는 인자값 수에 맞게 선언 하지만 내부에서 AddRow() 호출하는것은 모두 동일한 것 호출(마지막거 호출)
        // 즉 QueryCond 추가하면 모든 AddRow() 호출 부분을 인자 수량 동일하게 추가하여야 함. 현재 7개 임.[AddRow(1,2,3,4,5,6,7)]
        public void AddRow(string Text, object values1, object values2, object values3, object values4, object values5, bool isSelected)
        {
            int index = ctr_FarPoint1_Sheet1.Rows.Count;
            ctr_FarPoint1_Sheet1.Rows.Add(index, 1);

            ctr_FarPoint1_Sheet1.Cells.Get(index, 0).Text = (index + 1).ToString();
            ctr_FarPoint1_Sheet1.Cells.Get(index, 0).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;

            ctr_FarPoint1_Sheet1.Cells.Get(index, 1).Value = isSelected;
            ctr_FarPoint1_Sheet1.Cells.Get(index, 1).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;

            ctr_FarPoint1_Sheet1.Cells.Get(index, 1).CellType = new FarPoint.Win.Spread.CellType.CheckBoxCellType();
            ctr_FarPoint1_Sheet1.Cells.Get(index, 2).Text = Text;
            ctr_FarPoint1_Sheet1.Cells.Get(index, 2).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Left;
            ctr_FarPoint1_Sheet1.Cells.Get(index, 3).Value = values1;
            ctr_FarPoint1_Sheet1.Cells.Get(index, 4).Value = values2;
            ctr_FarPoint1_Sheet1.Cells.Get(index, 5).Value = values3;
            ctr_FarPoint1_Sheet1.Cells.Get(index, 6).Value = values4;

            //QueryCond5 추가 (2009.08.24 임종우) - QueryCond 추가시 (index, ?).Value = values? 한개씩 증가
            ctr_FarPoint1_Sheet1.Cells.Get(index, 7).Value = values5;

            // 처음 순서를 유지하기 위하여
            //QueryCond5 추가 (2009.08.24 임종우) - QueryCond 추가시 (index, ?).Value = rowCount++ 한개씩 증가            
            ctr_FarPoint1_Sheet1.Cells.Get(index, 8).Value = rowCount++;

            this.Size = new Size(175, 57 + (rowCount * 20));
            //this.Size = new Size(this.Size.Width, this.Size.Height + 20);
            //this.crt_Panel3.Size = new Size(this.crt_Panel3.Size.Width, this.crt_Panel3.Size.Height + 20);
        }

		// up button
		private void upButton_Click(object sender, System.EventArgs e)
		{
			int index = ctr_FarPoint1_Sheet1.ActiveRowIndex;

			// 더이상 올라갈곳이 없으므로 종료
			if ( index == 0)
				return;
			
			string text = ctr_FarPoint1_Sheet1.Cells.Get(index,2).Text;
			object values = ctr_FarPoint1_Sheet1.Cells.Get(index,3).Value;
			object values2 = ctr_FarPoint1_Sheet1.Cells.Get(index,4).Value;
			object values3 = ctr_FarPoint1_Sheet1.Cells.Get(index,5).Value;
            object values4 = ctr_FarPoint1_Sheet1.Cells.Get(index, 6).Value;
            object values5 = ctr_FarPoint1_Sheet1.Cells.Get(index, 7).Value;
			object selection = ctr_FarPoint1_Sheet1.Cells.Get(index,1).Value;
	
			// 값 교환
			ctr_FarPoint1_Sheet1.Cells.Get(index,2).Text = ctr_FarPoint1_Sheet1.Cells.Get(index-1,2).Text;
			ctr_FarPoint1_Sheet1.Cells.Get(index,3).Value = ctr_FarPoint1_Sheet1.Cells.Get(index-1,3).Value;
			ctr_FarPoint1_Sheet1.Cells.Get(index,4).Value = ctr_FarPoint1_Sheet1.Cells.Get(index-1,4).Value;
			ctr_FarPoint1_Sheet1.Cells.Get(index,5).Value = ctr_FarPoint1_Sheet1.Cells.Get(index-1,5).Value;
            ctr_FarPoint1_Sheet1.Cells.Get(index, 6).Value = ctr_FarPoint1_Sheet1.Cells.Get(index - 1, 6).Value;
            ctr_FarPoint1_Sheet1.Cells.Get(index, 7).Value = ctr_FarPoint1_Sheet1.Cells.Get(index - 1, 7).Value;
			ctr_FarPoint1_Sheet1.Cells.Get(index,1).Value = ctr_FarPoint1_Sheet1.Cells.Get(index-1,1).Value;

			ctr_FarPoint1_Sheet1.Cells.Get(index-1,2).Text = text;
			ctr_FarPoint1_Sheet1.Cells.Get(index-1,3).Value = values;
			ctr_FarPoint1_Sheet1.Cells.Get(index-1,4).Value = values2;
			ctr_FarPoint1_Sheet1.Cells.Get(index-1,5).Value = values3;
            ctr_FarPoint1_Sheet1.Cells.Get(index - 1, 6).Value = values4;
            ctr_FarPoint1_Sheet1.Cells.Get(index - 1, 7).Value = values5;
			ctr_FarPoint1_Sheet1.Cells.Get(index-1,1).Value = selection;
			
			ctr_FarPoint1_Sheet1.ActiveRowIndex = ctr_FarPoint1_Sheet1.ActiveRowIndex - 1;

		}
			
		// down button
		private void downButton_Click(object sender, System.EventArgs e)
		{
			int index = ctr_FarPoint1_Sheet1.ActiveRowIndex;

            if (index < 0) return;

			// 더이상 내려갈곳이 없으므로 종료
			if ( index == ctr_FarPoint1_Sheet1.Rows.Count-1)
				return;
			
			string text = ctr_FarPoint1_Sheet1.Cells.Get(index,2).Text;
			object values = ctr_FarPoint1_Sheet1.Cells.Get(index,3).Value;
			object values2 = ctr_FarPoint1_Sheet1.Cells.Get(index,4).Value;
			object values3 = ctr_FarPoint1_Sheet1.Cells.Get(index,5).Value;
            object values4 = ctr_FarPoint1_Sheet1.Cells.Get(index, 6).Value;
            object values5 = ctr_FarPoint1_Sheet1.Cells.Get(index, 7).Value;
			object selection = ctr_FarPoint1_Sheet1.Cells.Get(index,1).Value;
				
			// 값 교환
			ctr_FarPoint1_Sheet1.Cells.Get(index,2).Text = ctr_FarPoint1_Sheet1.Cells.Get(index+1,2).Text;
			ctr_FarPoint1_Sheet1.Cells.Get(index,3).Value = ctr_FarPoint1_Sheet1.Cells.Get(index+1,3).Value;
			ctr_FarPoint1_Sheet1.Cells.Get(index,4).Value = ctr_FarPoint1_Sheet1.Cells.Get(index+1,4).Value;
			ctr_FarPoint1_Sheet1.Cells.Get(index,5).Value = ctr_FarPoint1_Sheet1.Cells.Get(index+1,5).Value;
            ctr_FarPoint1_Sheet1.Cells.Get(index, 6).Value = ctr_FarPoint1_Sheet1.Cells.Get(index + 1, 6).Value;
            ctr_FarPoint1_Sheet1.Cells.Get(index, 7).Value = ctr_FarPoint1_Sheet1.Cells.Get(index + 1, 7).Value;
			ctr_FarPoint1_Sheet1.Cells.Get(index,1).Value = ctr_FarPoint1_Sheet1.Cells.Get(index+1,1).Value;

			ctr_FarPoint1_Sheet1.Cells.Get(index+1,2).Text = text;
			ctr_FarPoint1_Sheet1.Cells.Get(index+1,3).Value = values;
			ctr_FarPoint1_Sheet1.Cells.Get(index+1,4).Value = values2;
			ctr_FarPoint1_Sheet1.Cells.Get(index+1,5).Value = values3;
            ctr_FarPoint1_Sheet1.Cells.Get(index + 1, 6).Value = values4;
            ctr_FarPoint1_Sheet1.Cells.Get(index + 1, 7).Value = values5;
			ctr_FarPoint1_Sheet1.Cells.Get(index+1,1).Value = selection;
			
			ctr_FarPoint1_Sheet1.ActiveRowIndex = ctr_FarPoint1_Sheet1.ActiveRowIndex + 1;
		}


		// 아무것도 선택안하고 나갈시에 전부 선택으로 전환
		public void SelectedAll()
		{
			bool isSelected = false;

			// 한개라도 선택되었는지 검사
			for ( int i = 0; i <  this.ctr_FarPoint1.ActiveSheet.Rows.Count; i++ )
			{
				if ( ctr_FarPoint1.ActiveSheet.Cells[i, 1].Value.ToString().ToUpper() == "TRUE" )
				{
					isSelected = true;
					break;
				}
			}

			//  전체선택
			if ( !isSelected )
			{
				for ( int i = 0; i <  this.ctr_FarPoint1.ActiveSheet.Rows.Count; i++ )
				{
					ctr_FarPoint1.ActiveSheet.Cells[i, 1].Value = true;
				}
				
			}
		}

        /// <summary>
        /// 선택되어 있는 갯수를 Return
        /// </summary>
        /// <returns></returns>
        public int GetSelectedCount()
        {
            int iCheckedCount = 0;
            // 한개라도 선택되었는지 검사
            for (int i = 0; i < this.ctr_FarPoint1.ActiveSheet.Rows.Count; i++)
            {
                if (ctr_FarPoint1.ActiveSheet.Cells[i, 1].Value.ToString().ToUpper() == "TRUE")
                {
                    iCheckedCount++;
                }
            }

            return iCheckedCount;
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }  
	}
}
