using System.Drawing;
using System.Data;
using System.Windows.Forms;
using System.Collections;
using System.Diagnostics;
using System;
using System.ComponentModel;
using System.Globalization;

namespace Miracom.UI
{
	namespace Controls
	{
		namespace MCCodeView
		{
			
			[ToolboxItem(false)]public class MCCodeDropList : System.Windows.Forms.ListView, ISupportInitialize
			{
				
				#region " Windows Form auto generated code "
				
				public MCCodeDropList()
				{
					
					InitializeComponent();
					
					this.EnableSortIcon = true;
					this.FullRowSelect = true;
					this.View = System.Windows.Forms.View.Details;
					this.HideSelection = false;
					this.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
					
				}
				
				protected override void Dispose(bool disposing)
				{
					if (disposing)
					{
                        this.Clear();

                        if (this.SmallImageList != null)
                        {
                            this.SmallImageList.Images.Clear();
                            this.SmallImageList.Dispose();
                            this.SmallImageList = null;
                        }

                        if (this.imlUpDown != null)
                        {
                            this.imlUpDown.Images.Clear();
                            this.imlUpDown.Dispose();
                            this.imlUpDown = null;
                        }

						if (!(components == null))
						{
							components.Dispose();
						}
					}
					base.Dispose(disposing);
				}

                private IContainer components;

                private System.Windows.Forms.ImageList imlUpDown;
				[System.Diagnostics.DebuggerStepThrough()]
                private void InitializeComponent()
				{
                    this.components = new System.ComponentModel.Container();
                    System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MCCodeDropList));
                    this.imlUpDown = new System.Windows.Forms.ImageList(this.components);
                    this.SuspendLayout();
                    // 
                    // imlUpDown
                    // 
                    this.imlUpDown.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imlUpDown.ImageStream")));
                    this.imlUpDown.TransparentColor = System.Drawing.Color.Transparent;
                    this.imlUpDown.Images.SetKeyName(0, "");
                    this.imlUpDown.Images.SetKeyName(1, "");
                    // 
                    // MCCodeDropList
                    // 
                    this.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                    this.HideSelection = false;
                    this.Size = new System.Drawing.Size(200, 200);
                    this.ResumeLayout(false);

				}
				
				#endregion

                private bool m_bEnableSortIcon;
				// Sort : True - Ascending , False - Descending
				private bool m_bSortOrder = true;
				private int m_nSortColumn = - 1;
				
				public bool EnableSortIcon
				{
					get
					{
						return m_bEnableSortIcon;
					}
					set
					{
						m_bEnableSortIcon = value;
					}
				}
				
				protected override void OnColumnClick(System.Windows.Forms.ColumnClickEventArgs e)
				{
					base.OnColumnClick(e);
					
					SortColumn(e.Column);
					
				}
				
				protected void SortColumn(int col)
				{
					
					if (m_nSortColumn == col)
					{
						m_bSortOrder = m_bSortOrder ? false : true;
					}
					else
					{
						m_bSortOrder = true;
					}
					
					m_nSortColumn = col;
					
					if (EnableSortIcon == true)
					{
						ShowHeaderIcon(col);
					}
					
					ListViewItemSorter = new ListViewItemComparer(col, m_bSortOrder);
					Sort();
					
                    // 2007.01.16. Aiden Koo.
                    // ��� Ŭ���Ͽ� ��Ʈ�� ���� �ٽ� ����Ʈ �����ö� �������� ������ �Ǻ��� ��Ʈ�� �Ͼ�� ������ ó����.
                    // �׷��� ListViewItemSorter�� ������
                    ListViewItemSorter = null;
				}

                protected void ShowHeaderIcon(int col)
                {
                    IntPtr hdrHeader;
                    IntPtr hdrResult;
                    int i;

                    hdrHeader = MCModuleAPI.SendMessage(Handle, MCModuleAPI.LVM_GETHEADER, 0, 0);
                    hdrResult = MCModuleAPI.SendMessage(hdrHeader, MCModuleAPI.HDM_SETIMAGELIST, 0, imlUpDown.Handle.ToInt32());

                    for (i = 0; i <= Columns.Count - 1; i++)
                    {
                        MCModuleAPI.LVCOLUMN lv = new MCModuleAPI.LVCOLUMN();

                        lv.mask = MCModuleAPI.HDI_FORMAT | MCModuleAPI.HDI_IMAGE;
                        if (i == col)
                        {
                            lv.fmt = MCModuleAPI.HDF_IMAGE | MCModuleAPI.HDF_LEFT | MCModuleAPI.HDF_BITMAP_ON_RIGHT;
                            lv.iImage = m_bSortOrder ? 0 : 1;
                        }
                        else
                        {
                            lv.fmt = 0;
                        }

                        lv.cchTextMax = 0;
                        lv.cx = 0;
                        lv.iOrder = 0;
                        lv.iSubItem = 0;
                        lv.pszText = IntPtr.Zero;
                        hdrResult = MCModuleAPI.SendMessage(Handle, MCModuleAPI.LVM_SETCOLUMN, i, ref lv);
                    }

                }
				
				#region "ISupportInitialize Implementation"
				
				public virtual void BeginInit()
				{
					
					
				}
				
				public virtual void EndInit()
				{
					
					
				}
				
				#endregion
				
				protected override void WndProc(ref System.Windows.Forms.Message m)
				{
					base.WndProc(ref m);
					
					if (m.Msg == System.Convert.ToInt32(MCEnums.Msgs.WM_LBUTTONDOWN) || m.Msg == System.Convert.ToInt32(MCEnums.Msgs.WM_RBUTTONDOWN))
					{
                        UI.Controls.MCPopupFormBase frm = (UI.Controls.MCPopupFormBase)this.FindForm();
						frm.RaiseSelectionchanged();
					}
					
				}
				
			}
			
			public class ListViewItemComparer : IComparer, IDisposable
			{
				
				
				private int m_iCol;
				private bool m_bSortOrder;
                private CompareInfo cmpi = new CultureInfo("").CompareInfo;
                private CompareOptions cmpo = CompareOptions.Ordinal;
				
				public ListViewItemComparer()
				{
					m_iCol = 0;
				}
				
				public ListViewItemComparer(int col, bool order)
				{
					m_iCol = col;
					m_bSortOrder = order;
				}

                public void Dispose()
                {
                    cmpi = null;
                }
				
				public int Compare(object x, object y)
				{
					int iResult;
					string sTextX;
					string sTextY;

                    // 2007.01.16. Aiden Koo
                    // ���ڿ��� ���������� ���� 10 �� 9 ���� ���������� ó����. ���� ���ڿ��� �������� ��� ���ڷ� ��ȯ�Ͽ� ���ϵ��� ��.
                    // 2007.08.20. Aiden Koo
                    // �Ʒ� ������ �� ��� ���� �ð��� �����ɸ�. ���� ���ڿ� �⺻���� �ٽ� ������.
                    //////double dNumX;
                    //////double dNumY;
                    //////bool bNumX;
                    //////bool bNumY;

                    //////dNumX = 0;
                    //////dNumY = 0;
                    //////bNumX = false;
                    //////bNumY = false;
                    iResult = 0;
                    
                    try
					{
						if (((ListViewItem) x).SubItems.Count > m_iCol)
						{
							sTextX = ((ListViewItem) x).SubItems[m_iCol].Text;
						}
						else
						{
							sTextX = "";
						}
						if (((ListViewItem) y).SubItems.Count > m_iCol)
						{
							sTextY = ((ListViewItem) y).SubItems[m_iCol].Text;
						}
						else
						{
							sTextY = "";
						}


                        //////if (MCCommons.CheckNumeric(sTextX))
                        //////{
                        //////    dNumX = Convert.ToDouble(sTextX);
                        //////    bNumX = true;
                        //////}
                        //////if (MCCommons.CheckNumeric(sTextY))
                        //////{
                        //////    dNumY = Convert.ToDouble(sTextY);
                        //////    bNumY = true;
                        //////}

                        //////if (bNumX == true && bNumY == true)
                        //////{
                        //////    if (dNumX == dNumY)
                        //////        iResult = 0;
                        //////    else if (dNumX < dNumY)
                        //////        iResult = -1;
                        //////    else if (dNumX > dNumY)
                        //////        iResult = 1;
                        //////}
                        //////else
                        //////{
                            iResult = cmpi.Compare(sTextX, sTextY, cmpo);
                        //////}

						return (m_bSortOrder ? iResult : iResult * - 1);
					}
					catch (Exception e)
					{
                        MessageBox.Show(e.Message, "Compare Function", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return -1;
					}
					
				}
				
			}
			
		}
	}
	
}
