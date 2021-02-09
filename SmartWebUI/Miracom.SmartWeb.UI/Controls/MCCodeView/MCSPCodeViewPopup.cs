using System.Drawing;
using System.Data;
using System.Windows.Forms;
using System.Collections;
using System.Diagnostics;
using System;

namespace Miracom.UI
{
	namespace Controls
	{
		namespace MCCodeView
		{
			
			
			public class MCSPCodeViewPopup : MCPopupFormBase
			{
				
				private MCSSCodeViewSelChangedHandler SelectionChangedEvent;
				public event MCSSCodeViewSelChangedHandler SelectionChanged
				{
					add
					{
						SelectionChangedEvent = (MCSSCodeViewSelChangedHandler) System.Delegate.Combine(SelectionChangedEvent, value);
					}
					remove
					{
						SelectionChangedEvent = (MCSSCodeViewSelChangedHandler) System.Delegate.Remove(SelectionChangedEvent, value);
					}
				}
				
				
				private Point m_ptLocation = new Point(0, 0);
                				
				public MCSPCodeViewPopup()
				{
					
					InitializeComponent();

                    DroppedDownFlag = false;
                    this.Visible = false;
					
				}

                //UserControl은 Dispose를 재정의하여 구성 요소 목록을 정리합니다.
                protected override void Dispose(bool disposing)
                {
                    if (disposing)
                    {
                        if (this.m_MCCodeDropList != null)
                        {
                            this.m_MCCodeDropList.Dispose();
                            this.m_MCCodeDropList = null;
                        }

                        foreach (Control c in this.Controls)
                        {
                            if (c != null)
                            {
                                c.Dispose();
                            }
                        }

                    }

                    base.Dispose(disposing);
                }

                private System.Windows.Forms.Panel m_panel;
                internal Miracom.UI.Controls.MCCodeView.MCCodeDropList m_MCCodeDropList;
                private Panel pnlTextBox;
                private System.Windows.Forms.TextBox m_MCTextBox5;
                private System.Windows.Forms.TextBox m_MCTextBox4;
                private System.Windows.Forms.TextBox m_MCTextBox3;
                private System.Windows.Forms.TextBox m_MCTextBox2;
                private System.Windows.Forms.TextBox m_MCTextBox1;
				
				private void InitializeComponent()
				{
                    this.m_panel = new System.Windows.Forms.Panel();
                    this.m_MCCodeDropList = new Miracom.UI.Controls.MCCodeView.MCCodeDropList();
                    this.pnlTextBox = new System.Windows.Forms.Panel();
                    this.m_MCTextBox5 = new System.Windows.Forms.TextBox();
                    this.m_MCTextBox4 = new System.Windows.Forms.TextBox();
                    this.m_MCTextBox3 = new System.Windows.Forms.TextBox();
                    this.m_MCTextBox2 = new System.Windows.Forms.TextBox();
                    this.m_MCTextBox1 = new System.Windows.Forms.TextBox();
                    ((System.ComponentModel.ISupportInitialize)(this.m_MCCodeDropList)).BeginInit();
                    this.pnlTextBox.SuspendLayout();
                    this.SuspendLayout();
                    // 
                    // m_panel
                    // 
                    this.m_panel.BackColor = System.Drawing.SystemColors.Control;
                    this.m_panel.Dock = System.Windows.Forms.DockStyle.Bottom;
                    this.m_panel.Location = new System.Drawing.Point(0, 127);
                    this.m_panel.Name = "m_panel";
                    this.m_panel.Size = new System.Drawing.Size(100, 1);
                    this.m_panel.TabIndex = 3;
                    // 
                    // m_MCCodeDropList
                    // 
                    this.m_MCCodeDropList.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
                    this.m_MCCodeDropList.Dock = System.Windows.Forms.DockStyle.Fill;
                    this.m_MCCodeDropList.EnableSortIcon = false;
                    this.m_MCCodeDropList.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                    this.m_MCCodeDropList.FullRowSelect = true;
                    this.m_MCCodeDropList.GridLines = true;
                    this.m_MCCodeDropList.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.None;
                    this.m_MCCodeDropList.HideSelection = false;
                    this.m_MCCodeDropList.Location = new System.Drawing.Point(0, 0);
                    this.m_MCCodeDropList.MultiSelect = false;
                    this.m_MCCodeDropList.Name = "m_MCCodeDropList";
                    this.m_MCCodeDropList.Size = new System.Drawing.Size(100, 105);
                    this.m_MCCodeDropList.TabIndex = 4;
                    this.m_MCCodeDropList.UseCompatibleStateImageBehavior = false;
                    this.m_MCCodeDropList.View = System.Windows.Forms.View.Details;
                    this.m_MCCodeDropList.KeyUp += new System.Windows.Forms.KeyEventHandler(this.m_MCDropListView_KeyUp);
                    // 
                    // pnlTextBox
                    // 
                    this.pnlTextBox.BackColor = System.Drawing.Color.Transparent;
                    this.pnlTextBox.Controls.Add(this.m_MCTextBox5);
                    this.pnlTextBox.Controls.Add(this.m_MCTextBox4);
                    this.pnlTextBox.Controls.Add(this.m_MCTextBox3);
                    this.pnlTextBox.Controls.Add(this.m_MCTextBox2);
                    this.pnlTextBox.Controls.Add(this.m_MCTextBox1);
                    this.pnlTextBox.Dock = System.Windows.Forms.DockStyle.Bottom;
                    this.pnlTextBox.Location = new System.Drawing.Point(0, 105);
                    this.pnlTextBox.Name = "pnlTextBox";
                    this.pnlTextBox.Padding = new System.Windows.Forms.Padding(0, 1, 0, 0);
                    this.pnlTextBox.Size = new System.Drawing.Size(100, 22);
                    this.pnlTextBox.TabIndex = 6;
                    // 
                    // m_MCTextBox5
                    // 
                    this.m_MCTextBox5.Dock = System.Windows.Forms.DockStyle.Fill;
                    this.m_MCTextBox5.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                    this.m_MCTextBox5.Location = new System.Drawing.Point(40, 1);
                    this.m_MCTextBox5.Name = "m_MCTextBox5";
                    this.m_MCTextBox5.Size = new System.Drawing.Size(60, 20);
                    this.m_MCTextBox5.TabIndex = 5;
                    this.m_MCTextBox5.Tag = "5";
                    this.m_MCTextBox5.KeyUp += new System.Windows.Forms.KeyEventHandler(this.m_MCTextBox_KeyUp);
                    // 
                    // m_MCTextBox4
                    // 
                    this.m_MCTextBox4.Dock = System.Windows.Forms.DockStyle.Left;
                    this.m_MCTextBox4.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                    this.m_MCTextBox4.Location = new System.Drawing.Point(30, 1);
                    this.m_MCTextBox4.Name = "m_MCTextBox4";
                    this.m_MCTextBox4.Size = new System.Drawing.Size(10, 20);
                    this.m_MCTextBox4.TabIndex = 4;
                    this.m_MCTextBox4.Tag = "4";
                    this.m_MCTextBox4.KeyUp += new System.Windows.Forms.KeyEventHandler(this.m_MCTextBox_KeyUp);
                    // 
                    // m_MCTextBox3
                    // 
                    this.m_MCTextBox3.Dock = System.Windows.Forms.DockStyle.Left;
                    this.m_MCTextBox3.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                    this.m_MCTextBox3.Location = new System.Drawing.Point(20, 1);
                    this.m_MCTextBox3.Name = "m_MCTextBox3";
                    this.m_MCTextBox3.Size = new System.Drawing.Size(10, 20);
                    this.m_MCTextBox3.TabIndex = 3;
                    this.m_MCTextBox3.Tag = "3";
                    this.m_MCTextBox3.KeyUp += new System.Windows.Forms.KeyEventHandler(this.m_MCTextBox_KeyUp);
                    // 
                    // m_MCTextBox2
                    // 
                    this.m_MCTextBox2.Dock = System.Windows.Forms.DockStyle.Left;
                    this.m_MCTextBox2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                    this.m_MCTextBox2.Location = new System.Drawing.Point(10, 1);
                    this.m_MCTextBox2.Name = "m_MCTextBox2";
                    this.m_MCTextBox2.Size = new System.Drawing.Size(10, 20);
                    this.m_MCTextBox2.TabIndex = 2;
                    this.m_MCTextBox2.Tag = "2";
                    this.m_MCTextBox2.KeyUp += new System.Windows.Forms.KeyEventHandler(this.m_MCTextBox_KeyUp);
                    // 
                    // m_MCTextBox1
                    // 
                    this.m_MCTextBox1.Dock = System.Windows.Forms.DockStyle.Left;
                    this.m_MCTextBox1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                    this.m_MCTextBox1.Location = new System.Drawing.Point(0, 1);
                    this.m_MCTextBox1.Name = "m_MCTextBox1";
                    this.m_MCTextBox1.Size = new System.Drawing.Size(10, 20);
                    this.m_MCTextBox1.TabIndex = 1;
                    this.m_MCTextBox1.Tag = "1";
                    this.m_MCTextBox1.KeyUp += new System.Windows.Forms.KeyEventHandler(this.m_MCTextBox_KeyUp);
                    // 
                    // MCSPCodeViewPopup
                    // 
                    this.BackColor = System.Drawing.Color.White;
                    this.ClientSize = new System.Drawing.Size(100, 128);
                    this.Controls.Add(this.m_MCCodeDropList);
                    this.Controls.Add(this.pnlTextBox);
                    this.Controls.Add(this.m_panel);
                    this.Name = "MCSPCodeViewPopup";
                    this.Deactivate += new System.EventHandler(this.MCDropListPopup_Deactivate);
                    this.FontChanged += new System.EventHandler(this.MCCodeViewPopup_FontChanged);
                    ((System.ComponentModel.ISupportInitialize)(this.m_MCCodeDropList)).EndInit();
                    this.pnlTextBox.ResumeLayout(false);
                    this.pnlTextBox.PerformLayout();
                    this.ResumeLayout(false);

				}

                public void SetTextBoxInit()
                {
                    m_MCTextBox1.Text = "";
                    m_MCTextBox2.Text = "";
                    m_MCTextBox3.Text = "";
                    m_MCTextBox4.Text = "";
                    m_MCTextBox5.Text = "";
                }
				
				private bool DropListView()
				{
					
					try
					{
						Graphics g = this.CreateGraphics();
						float colWidth = 0;
						ArrayList maxColWidthArray = new ArrayList();
						int lvWidth;
						int lvHeight;
						int itemWidth;
						int itemHeight;
						int i;
						int j;
						
						if (Columns.Count < 1)
						{
							Columns.Add("Default", 0, HorizontalAlignment.Left);
						}
						
						if (Items.Count < 1)
						{
							string[] sSpace = new string[Columns.Count-1 + 1];
							for (i = 0; i <= Columns.Count - 1; i++)
							{
								sSpace.SetValue("", i);
							}
							Items.Add(new ListViewItem(sSpace));
						}
						
						if (Columns.Count == 1)
						{
							maxColWidthArray.Add(MCModuleAPI.MIN_COLUMN_WIDTH * 2);
						}
						else
						{
							for (i = 0; i <= Columns.Count - 1; i++)
							{
								maxColWidthArray.Add(MCModuleAPI.MIN_COLUMN_WIDTH);
							}
						}
						
						//============================================================
                        // ListView에 ListItem를 채우면서 각 Column의 최대 크기를 계산
						//============================================================
						ListViewItem item = null;
						for (i = 0; i <= Items.Count - 1; i++)
						{
							item = Items[i];
							for (j = 0; j <= Columns.Count - 1; j++)
							{
								itemWidth = System.Convert.ToInt32(g.MeasureString(item.SubItems[j].Text, this.Font).Width) + MCModuleAPI.BONUS_COLUMN_WIDTH;
								if (j == 0 && item.ImageIndex > - 1)
								{
									itemWidth += m_MCCodeDropList.SmallImageList.ImageSize.Width + MCModuleAPI.BONUS_COLUMN_WIDTH_WITH_IMAGE;
								}
								colWidth = itemWidth;
								if (colWidth > System.Convert.ToInt32 (maxColWidthArray[j]))
								{
									if (colWidth > MCModuleAPI.MAX_COLUMN_WIDTH)
									{
										maxColWidthArray[j] = MCModuleAPI.MAX_COLUMN_WIDTH;
									}
									else
									{
										maxColWidthArray[j] = (int)colWidth;
									}
								}
							}
						}

                        m_MCTextBox1.Visible = false;
                        m_MCTextBox2.Visible = false;
                        m_MCTextBox3.Visible = false;
                        m_MCTextBox4.Visible = false;
                        m_MCTextBox5.Visible = false;

                        for (i = 0; i < maxColWidthArray.Count; i++)
                        {
                            if (i == 0)
                            {
                                m_MCTextBox1.Visible = true;
                                m_MCTextBox1.Width = System.Convert.ToInt32(maxColWidthArray[i]) + 7;
                            }
                            else if (i == 1)
                            {
                                m_MCTextBox2.Visible = true;
                                m_MCTextBox2.Width = System.Convert.ToInt32(maxColWidthArray[i]) + 4;
                            }
                            else if (i == 2)
                            {
                                m_MCTextBox3.Visible = true;
                                m_MCTextBox3.Width = System.Convert.ToInt32(maxColWidthArray[i]) + 4;
                            }
                            else if (i == 3)
                            {
                                m_MCTextBox4.Visible = true;
                                m_MCTextBox4.Width = System.Convert.ToInt32(maxColWidthArray[i]) + 4;
                            }
                            else if (i == 4)
                            {
                                m_MCTextBox5.Visible = true;
                                m_MCTextBox5.Width = System.Convert.ToInt32(maxColWidthArray[i]) + 4;
                            }
                            else
                            {
                                break;
                            }
                        } 
						
						//=================================
                        // Popup 되는 ListView의 Width 계산
						//=================================
						lvWidth = 0;
						for (i = 0; i <= m_MCCodeDropList.Columns.Count - 1; i++)
						{
							colWidth = System.Convert.ToInt32(maxColWidthArray[i]) + 5;
							m_MCCodeDropList.Columns[i].Width = (int)colWidth;
							lvWidth += (int)colWidth;
						}
						if (m_MCCodeDropList.Scrollable == true)
						{
							lvWidth += 20;
						}
						this.Width = lvWidth;
						
						//==================================
                        // Popup 되는 ListView의 Height 계산
						//==================================
						itemHeight = m_MCCodeDropList.Items[0].Bounds.Height;
						lvHeight = m_MCCodeDropList.Items.Count * itemHeight;
						if (lvHeight > itemHeight * MCModuleAPI.MAX_LIST_COUNT)
						{
							this.Height = itemHeight * MCModuleAPI.MAX_LIST_COUNT;
						}
						else
						{
							this.Height = lvHeight;
						}
						this.Height += MCModuleAPI.BONUS_LISTVIEW_HEIGHT;
						this.Height += m_MCTextBox1.Height + 2;
						
						//===================================
                        // Popup 되는 ListView의 위치 재 계산
						//===================================
						this.Location = Position;
						if (this.Location.Y + this.Height > Screen.PrimaryScreen.Bounds.Height)
						{
							this.Location = new Point(this.Location.X, this.Location.Y - this.Height);
						}
						if (this.Location.X < 0)
						{
							this.Location = new Point(2, this.Location.Y);
						}
						else if (this.Location.X + this.Width > Screen.PrimaryScreen.Bounds.Width)
						{
							this.Location = new Point(Screen.PrimaryScreen.Bounds.Width - this.Width - 2, this.Location.Y);
						}

                        g.Dispose();
					}
					catch (Exception ex)
					{
						MessageBox.Show(ex.Message, "DropListView()", MessageBoxButtons.OK, MessageBoxIcon.Error);
					}

                    return true;
				}

                public override void RaiseSelectionchanged()
				{
					
					OnSelectionChanged();
					
				}
				
				private void OnSelectionChanged()
				{
					
					if (this.m_MCCodeDropList.SelectedItems.Count > 0)
					{
                        MCSSCodeViewSelChanged_EventArgs oArgs = new MCSSCodeViewSelChanged_EventArgs((ListViewItem)this.m_MCCodeDropList.SelectedItems[0]);
                        ShowPopup(false);
                        if (SelectionChangedEvent != null)
							SelectionChangedEvent(this, oArgs);
					}
				}

                // Form이 보여질때의 위치
				public Point Position
				{
					get
					{
						return m_ptLocation;
					}
					set
					{
                        m_ptLocation = value;
					}
				}
				
				public ListView.ListViewItemCollection Items
				{
					get
					{
						return m_MCCodeDropList.Items;
					}
				}
				
				public ListView.ColumnHeaderCollection Columns
				{
					get
					{
						return m_MCCodeDropList.Columns;
					}
				}
				
				public bool ShowPopup(bool visable)
				{

                    if (visable == true)
					{
                        DroppedDownFlag = true;
                        SetTextBoxInit();
						DropListView();
						this.Visible = true;
					}
					else
					{
						DroppedDownFlag = false;
						this.Visible = false;
					}
                    return true;
					
				}
				
				private void MCDropListPopup_Deactivate(object sender, System.EventArgs e)
				{
					
					ShowPopup(false);
					
				}

                private void MCCodeViewPopup_FontChanged(object sender, EventArgs e)
                {
                    this.m_MCCodeDropList.Font = this.Font;
                    this.m_MCTextBox1.Font = this.Font;
                    this.m_MCTextBox2.Font = this.Font;
                    this.m_MCTextBox3.Font = this.Font;
                    this.m_MCTextBox4.Font = this.Font;
                    this.m_MCTextBox5.Font = this.Font;
                }
				
				private void m_MCDropListView_KeyUp(object sender, System.Windows.Forms.KeyEventArgs e)
				{
					
					if (e.KeyData == Keys.Escape)
					{
						ShowPopup(false);
					}
					if (e.KeyData == Keys.Enter)
					{
						OnSelectionChanged();
						ShowPopup(false);
					}
					
				}
				
				private void m_MCTextBox_KeyUp(object sender, System.Windows.Forms.KeyEventArgs e)
				{
					
					if (e.KeyData == Keys.Escape)
					{
						ShowPopup(false);
					}
					else if (e.KeyData == Keys.Enter)
					{
						OnSelectionChanged();
						ShowPopup(false);
					}
					else
					{
                        System.Windows.Forms.TextBox txtTemp;
                        txtTemp = (System.Windows.Forms.TextBox)sender;
                        string inputText = txtTemp.Text;
                        int iSearchSubItem = Convert.ToInt32(txtTemp.Tag) - 1;
						int i;
						for (i = 0; i <= m_MCCodeDropList.Items.Count - 1; i++)
						{
                            if (m_MCCodeDropList.Items[i].SubItems[iSearchSubItem].Text.Length >= inputText.Length)
                            {
                                if (m_MCCodeDropList.Items[i].SubItems[iSearchSubItem].Text.Substring(0, inputText.Length).ToUpper().CompareTo(inputText.ToUpper()) == 0)
                                {
									m_MCCodeDropList.Items[i].Selected = true;
									m_MCCodeDropList.EnsureVisible(i);
									return;
								}
							}
						}
					}
					
				}
				
			}
			
		}
	}
	
}
