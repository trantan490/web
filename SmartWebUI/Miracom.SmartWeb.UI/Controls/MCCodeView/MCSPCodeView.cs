using System.Drawing;
using System.Data;
using System.Windows.Forms;
using System.Collections;
using System.Diagnostics;
using System;
using System.ComponentModel;
using System.ComponentModel.Design;


namespace Miracom.UI
{
	namespace Controls
	{
		namespace MCCodeView
		{

			[DesignerAttribute(typeof(ComponentDesigner), typeof(IDesigner))]
            public class MCSPCodeView : Miracom.UI.Controls.MCControlBase
			{
				
				private MCSSCodeViewSelChangedHandler SelectedItemChangedEvent;
                protected ImageList imlSmallIcon;
            
				public event MCSSCodeViewSelChangedHandler SelectedItemChanged
				{
					add
					{
						SelectedItemChangedEvent = (MCSSCodeViewSelChangedHandler) System.Delegate.Combine(SelectedItemChangedEvent, value);
					}
					remove
					{
						SelectedItemChangedEvent = (MCSSCodeViewSelChangedHandler) System.Delegate.Remove(SelectedItemChangedEvent, value);
					}
				}
				
				
				private MCSPCodeViewPopup m_MCSPCodeViewPopup = null;
				
				#region " Windows Form auto generated code "
				
				public MCSPCodeView()
				{
					
					InitializeComponent();
					
				}
				
				protected override void Dispose(bool disposing)
				{
					if (disposing)
					{
                        if (this.m_MCSPCodeViewPopup != null)
                        {
                            this.m_MCSPCodeViewPopup.Dispose();
                            this.m_MCSPCodeViewPopup = null;
                        }

						if (!(components == null))
						{
							components.Dispose();
						}
					}
					base.Dispose(disposing);
                }

                private IContainer components;
		
				[System.Diagnostics.DebuggerStepThrough()]
                private void InitializeComponent()
				{
                    this.components = new System.ComponentModel.Container();
                    System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MCSPCodeView));
                    this.m_MCSPCodeViewPopup = new Miracom.UI.Controls.MCCodeView.MCSPCodeViewPopup();
                    this.imlSmallIcon = new System.Windows.Forms.ImageList(this.components);
                    ((System.ComponentModel.ISupportInitialize)(this)).BeginInit();
                    this.SuspendLayout();
                    // 
                    // m_MCSPCodeViewPopup
                    // 
                    this.m_MCSPCodeViewPopup.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
                    this.m_MCSPCodeViewPopup.BackColor = System.Drawing.Color.White;
                    this.m_MCSPCodeViewPopup.ClientSize = new System.Drawing.Size(127, 128);
                    this.m_MCSPCodeViewPopup.ControlBox = false;
                    this.m_MCSPCodeViewPopup.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
                    this.m_MCSPCodeViewPopup.Location = new System.Drawing.Point(0, 0);
                    this.m_MCSPCodeViewPopup.MaximizeBox = false;
                    this.m_MCSPCodeViewPopup.MinimizeBox = false;
                    this.m_MCSPCodeViewPopup.Name = "m_MCSPCodeViewPopup";
                    this.m_MCSPCodeViewPopup.Position = new System.Drawing.Point(0, 0);
                    this.m_MCSPCodeViewPopup.ShowInTaskbar = false;
                    this.m_MCSPCodeViewPopup.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
                    this.m_MCSPCodeViewPopup.Text = "MCPopupFormBase";
                    this.m_MCSPCodeViewPopup.Visible = false;
                    this.m_MCSPCodeViewPopup.SelectionChanged += new Miracom.UI.MCSSCodeViewSelChangedHandler(this.OnPopUp_SelectionChanged);
                    // 
                    // imlSmallIcon
                    // 
                    this.imlSmallIcon.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imlSmallIcon.ImageStream")));
                    this.imlSmallIcon.TransparentColor = System.Drawing.Color.Transparent;
                    this.imlSmallIcon.Images.SetKeyName(0, "");
                    this.imlSmallIcon.Images.SetKeyName(1, "");
                    this.imlSmallIcon.Images.SetKeyName(2, "");
                    this.imlSmallIcon.Images.SetKeyName(3, "");
                    this.imlSmallIcon.Images.SetKeyName(4, "");
                    this.imlSmallIcon.Images.SetKeyName(5, "");
                    this.imlSmallIcon.Images.SetKeyName(6, "");
                    this.imlSmallIcon.Images.SetKeyName(7, "");
                    this.imlSmallIcon.Images.SetKeyName(8, "");
                    this.imlSmallIcon.Images.SetKeyName(9, "");
                    this.imlSmallIcon.Images.SetKeyName(10, "");
                    this.imlSmallIcon.Images.SetKeyName(11, "");
                    this.imlSmallIcon.Images.SetKeyName(12, "");
                    this.imlSmallIcon.Images.SetKeyName(13, "");
                    this.imlSmallIcon.Images.SetKeyName(14, "");
                    this.imlSmallIcon.Images.SetKeyName(15, "");
                    this.imlSmallIcon.Images.SetKeyName(16, "");
                    this.imlSmallIcon.Images.SetKeyName(17, "");
                    this.imlSmallIcon.Images.SetKeyName(18, "");
                    this.imlSmallIcon.Images.SetKeyName(19, "");
                    this.imlSmallIcon.Images.SetKeyName(20, "");
                    this.imlSmallIcon.Images.SetKeyName(21, "");
                    this.imlSmallIcon.Images.SetKeyName(22, "");
                    this.imlSmallIcon.Images.SetKeyName(23, "");
                    this.imlSmallIcon.Images.SetKeyName(24, "");
                    this.imlSmallIcon.Images.SetKeyName(25, "");
                    this.imlSmallIcon.Images.SetKeyName(26, "");
                    this.imlSmallIcon.Images.SetKeyName(27, "");
                    this.imlSmallIcon.Images.SetKeyName(28, "");
                    this.imlSmallIcon.Images.SetKeyName(29, "");
                    this.imlSmallIcon.Images.SetKeyName(30, "");
                    this.imlSmallIcon.Images.SetKeyName(31, "");
                    this.imlSmallIcon.Images.SetKeyName(32, "");
                    this.imlSmallIcon.Images.SetKeyName(33, "");
                    this.imlSmallIcon.Images.SetKeyName(34, "");
                    this.imlSmallIcon.Images.SetKeyName(35, "");
                    this.imlSmallIcon.Images.SetKeyName(36, "");
                    this.imlSmallIcon.Images.SetKeyName(37, "");
                    this.imlSmallIcon.Images.SetKeyName(38, "");
                    this.imlSmallIcon.Images.SetKeyName(39, "");
                    this.imlSmallIcon.Images.SetKeyName(40, "");
                    this.imlSmallIcon.Images.SetKeyName(41, "");
                    this.imlSmallIcon.Images.SetKeyName(42, "");
                    this.imlSmallIcon.Images.SetKeyName(43, "");
                    this.imlSmallIcon.Images.SetKeyName(44, "");
                    this.imlSmallIcon.Images.SetKeyName(45, "");
                    this.imlSmallIcon.Images.SetKeyName(46, "");
                    this.imlSmallIcon.Images.SetKeyName(47, "");
                    this.imlSmallIcon.Images.SetKeyName(48, "");
                    this.imlSmallIcon.Images.SetKeyName(49, "");
                    this.imlSmallIcon.Images.SetKeyName(50, "");
                    this.imlSmallIcon.Images.SetKeyName(51, "");
                    this.imlSmallIcon.Images.SetKeyName(52, "");
                    this.imlSmallIcon.Images.SetKeyName(53, "");
                    this.imlSmallIcon.Images.SetKeyName(54, "");
                    this.imlSmallIcon.Images.SetKeyName(55, "");
                    this.imlSmallIcon.Images.SetKeyName(56, "");
                    this.imlSmallIcon.Images.SetKeyName(57, "");
                    this.imlSmallIcon.Images.SetKeyName(58, "");
                    this.imlSmallIcon.Images.SetKeyName(59, "");
                    this.imlSmallIcon.Images.SetKeyName(60, "");
                    this.imlSmallIcon.Images.SetKeyName(61, "");
                    this.imlSmallIcon.Images.SetKeyName(62, "");
                    this.imlSmallIcon.Images.SetKeyName(63, "");
                    this.imlSmallIcon.Images.SetKeyName(64, "");
                    this.imlSmallIcon.Images.SetKeyName(65, "");
                    this.imlSmallIcon.Images.SetKeyName(66, "");
                    this.imlSmallIcon.Images.SetKeyName(67, "");
                    this.imlSmallIcon.Images.SetKeyName(68, "");
                    this.imlSmallIcon.Images.SetKeyName(69, "");
                    this.imlSmallIcon.Images.SetKeyName(70, "");
                    this.imlSmallIcon.Images.SetKeyName(71, "");
                    this.imlSmallIcon.Images.SetKeyName(72, "");
                    this.imlSmallIcon.Images.SetKeyName(73, "");
                    this.imlSmallIcon.Images.SetKeyName(74, "");
                    this.imlSmallIcon.Images.SetKeyName(75, "");
                    this.imlSmallIcon.Images.SetKeyName(76, "");
                    this.imlSmallIcon.Images.SetKeyName(77, "");
                    this.imlSmallIcon.Images.SetKeyName(78, "");
                    this.imlSmallIcon.Images.SetKeyName(79, "");
                    this.imlSmallIcon.Images.SetKeyName(80, "");
                    this.imlSmallIcon.Images.SetKeyName(81, "");
                    this.imlSmallIcon.Images.SetKeyName(82, "");
                    this.imlSmallIcon.Images.SetKeyName(83, "");
                    this.imlSmallIcon.Images.SetKeyName(84, "");
                    this.imlSmallIcon.Images.SetKeyName(85, "");
                    this.imlSmallIcon.Images.SetKeyName(86, "");
                    this.imlSmallIcon.Images.SetKeyName(87, "");
                    this.imlSmallIcon.Images.SetKeyName(88, "");
                    this.imlSmallIcon.Images.SetKeyName(89, "");
                    this.imlSmallIcon.Images.SetKeyName(90, "");
                    this.imlSmallIcon.Images.SetKeyName(91, "");
                    this.imlSmallIcon.Images.SetKeyName(92, "");
                    this.imlSmallIcon.Images.SetKeyName(93, "");
                    this.imlSmallIcon.Images.SetKeyName(94, "");
                    this.imlSmallIcon.Images.SetKeyName(95, "");
                    this.imlSmallIcon.Images.SetKeyName(96, "");
                    this.imlSmallIcon.Images.SetKeyName(97, "");
                    this.imlSmallIcon.Images.SetKeyName(98, "");
                    this.imlSmallIcon.Images.SetKeyName(99, "");
                    this.imlSmallIcon.Images.SetKeyName(100, "");
                    this.imlSmallIcon.Images.SetKeyName(101, "");
                    this.imlSmallIcon.Images.SetKeyName(102, "");
                    this.imlSmallIcon.Images.SetKeyName(103, "");
                    this.imlSmallIcon.Images.SetKeyName(104, "White Image");
                    // 
                    // MCSPCodeView
                    // 
                    this.BackColor = System.Drawing.SystemColors.Control;
                    this.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                    this.MCViewStyle.BorderColor = System.Drawing.Color.DarkGray;
                    this.MCViewStyle.BorderHotColor = System.Drawing.Color.Black;
                    this.Name = "MCSPCodeView";
                    this.Size = new System.Drawing.Size(20, 20);
                    this.FontChanged += new System.EventHandler(this.MCSPCodeView_FontChanged);
                    ((System.ComponentModel.ISupportInitialize)(this)).EndInit();
                    this.ResumeLayout(false);

				}
				
				#endregion
				
				#region "Properties Implements"
				
				private ListViewItem m_SelectedItem = null;
				private int m_iRow;
				private int m_iCol;
				
				private Size m_DropDownSize;
				private bool m_bVisibleColumnHeader = false;
				
				public ListView GetListView
				{
					get
					{
						return ((ListView) m_MCSPCodeViewPopup.m_MCCodeDropList);
					}
				}
				
				[Browsable(false), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
                public Size DropDownSize
				{
					get
					{
						return m_DropDownSize;
					}
					set
					{
						m_DropDownSize = value;
					}
				}
				
				[Browsable(false), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
                public ListView.ListViewItemCollection Items
				{
					get
					{
						return m_MCSPCodeViewPopup.Items;
					}
				}
				
				[Browsable(false), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
                public ListView.ColumnHeaderCollection Columns
				{
					get
					{
						return m_MCSPCodeViewPopup.Columns;
					}
				}
				
				[Browsable(false), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
                public ListViewItem SelectedItem
				{
					get
					{
						return m_SelectedItem;
					}
				}
				
				public ImageList SmallImageList
				{
					get
					{
						return m_MCSPCodeViewPopup.m_MCCodeDropList.SmallImageList;
					}
					set
					{
						m_MCSPCodeViewPopup.m_MCCodeDropList.SmallImageList = value;
					}
				}
				
				public bool VisibleColumnHeader
				{
					get
					{
						return m_bVisibleColumnHeader;
					}
					set
					{
						m_bVisibleColumnHeader = value;
						if (value == true)
						{
							this.m_MCSPCodeViewPopup.m_MCCodeDropList.HeaderStyle = ColumnHeaderStyle.Clickable;
						}
						else
						{
							this.m_MCSPCodeViewPopup.m_MCCodeDropList.HeaderStyle = ColumnHeaderStyle.None;
						}
                        this.m_MCSPCodeViewPopup.VisibleColumnHeader = value;
					}
				}
				
				public int Row
				{
					get
					{
						return m_iRow;
					}
				}
				
				public int Col
				{
					get
					{
						return m_iCol;
					}
				}
				
                public System.Drawing.Point ViewPosition
                {
                    get
                    {
                        return m_MCSPCodeViewPopup.Position;
                    }
                    set
                    {
                        m_MCSPCodeViewPopup.Position = value;
                    }
                }

                #endregion
				
				public void Init()
				{
					
					m_MCSPCodeViewPopup.m_MCCodeDropList.Clear();
                    this.SmallImageList = this.imlSmallIcon;
				}
				
				public bool AddEmptyRow(int iRowCount)
				{
					
					int i = 0;
					int j = 0;
					ListViewItem itmx = null;
					
					try
					{
						for (i = 0; i <= iRowCount - 1; i++)
						{
							itmx = new ListViewItem("");
							for (j = 0; j <= Columns.Count - 1; j++)
							{
								itmx.SubItems.Add("");
							}
							Items.Add(itmx);
						}
					}
					catch (Exception ex)
					{
						MessageBox.Show(ex.Message, "AddEmptyRow()", MessageBoxButtons.OK, MessageBoxIcon.Error);
						return false;
					}
					
					return true;
					
				}
				
				public bool InsertEmptyRow(int InsertIndex, int iRowCount)
				{
					
					int i = 0;
					int j = 0;
					ListViewItem itmx = null;
					
					try
					{
						for (i = 0; i <= iRowCount - 1; i++)
						{
							itmx = new ListViewItem("");
							for (j = 0; j <= Columns.Count - 1; j++)
							{
								itmx.SubItems.Add("");
							}
							Items.Insert(InsertIndex, itmx);
						}
					}
					catch (Exception ex)
					{
						MessageBox.Show(ex.Message, "AddEmptyRow()", MessageBoxButtons.OK, MessageBoxIcon.Error);
						return false;
					}
					
					return true;
					
				}
				
				private void OnPopUp_SelectionChanged(object sender, MCSSCodeViewSelChanged_EventArgs e)
				{
					m_MCSPCodeViewPopup.DroppedDownFlag = false;
					m_SelectedItem = e.SelectedItem;
					e.Row = m_iRow;
					e.Col = m_iCol;
					
					if (SelectedItemChangedEvent != null)
						SelectedItemChangedEvent(this, e);
					
				}
				
				//add by CM Koo. 2005.08.25
				private void MCSPCodeView_FontChanged(object sender, System.EventArgs e)
				{
                    this.m_MCSPCodeViewPopup.Font = this.Font;
				}
				
				public bool ShowPopupList(int iRow, int iCol)
				{
                    if (m_MCSPCodeViewPopup.DroppedDownFlag == true)
						return false;
					
					m_MCSPCodeViewPopup.Position = Control.MousePosition;
					ShowPopUp(iRow, iCol);					
					return true;					
				}
				
				public void ListClear()
				{					
					Columns.Clear();
					Items.Clear();					
				}

                public void ShowPopUp(int iRow, int iCol)
				{
					m_iRow = iRow;  m_iCol = iCol;
					m_MCSPCodeViewPopup.ShowPopup(true);
					
                    m_MCSPCodeViewPopup.SetTextBoxInit();
                    m_MCSPCodeViewPopup.DroppedDownFlag = true;
					m_SelectedItem = null;
					
					m_MCSPCodeViewPopup.m_MCCodeDropList.Focus();					
				}
				
			}
			
		}
	}
	
}
