using System.Drawing;
using System.Data;
using System.Windows.Forms;
using System.Collections;
using System.Diagnostics;
using System;
using System.Globalization;


namespace Miracom.UI
{
    namespace Controls
    {
        namespace MCListView
        {


            public class MCListView : System.Windows.Forms.ListView
            {


                #region " Windows Form auto generated code "

                public MCListView()
                {

                    
                    InitializeComponent();

                    
                    EnableSort = true;
                    EnableSortIcon = true;
                    FullRowSelect = true;
                    View = System.Windows.Forms.View.Details;

                }

                //UserControl1?Ä DisposeÎ•??¨Ï†ï?òÌïò??Íµ¨ÏÑ± ?îÏÜå Î™©Î°ù???ïÎ¶¨?©Îãà??
                protected override void Dispose(bool disposing)
                {
                    if (disposing)
                    {
                        if (!(components == null))
                        {
                            components.Dispose();
                        }
                    }
                    base.Dispose(disposing);
                }

                private System.ComponentModel.IContainer components;

                

                //Ï∞∏Í≥†: ?§Ïùå ?ÑÎ°ú?úÏ???Windows Form ?îÏûê?¥ÎÑà???ÑÏöî?©Îãà??
                //Windows Form ?îÏûê?¥ÎÑàÎ•??¨Ïö©?òÏó¨ ?òÏ†ï?????àÏäµ?àÎã§.
                //ÏΩîÎìú ?∏ÏßëÍ∏∞Î? ?¨Ïö©?òÏó¨ ?òÏ†ï?òÏ? ÎßàÏã≠?úÏò§.
                private System.Windows.Forms.ImageList imlUpDown;
                [System.Diagnostics.DebuggerStepThrough()]
                private void InitializeComponent()
                {
                    this.components = new System.ComponentModel.Container();
                    System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MCListView));
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
                    // MCListView
                    // 
                    this.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                    this.ResumeLayout(false);

                }

                #endregion

                private bool m_bEnableSort;
                private bool m_bEnableSortIcon;
                // Sort ?úÏÑú : True - Ascending , False - Descending
                private bool m_bSortOrder = true;
                private int m_nSortColumn = -1;

                public bool EnableSort
                {
                    get
                    {
                        return m_bEnableSort;
                    }
                    set
                    {
                        m_bEnableSort = value;
                    }
                }

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

                    if (EnableSort == true)
                    {
                        SortColumn(e.Column);
                    }

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
                    // «Ï¥ı ≈¨∏Ø«œø© º“∆Æ«ÿ ≥ı∞Ì ¥ŸΩ√ ∏ÆΩ∫∆Æ ∞°¡Æø√∂ß æ∆¿Ã≈€¿ª ±∏º∫Ω√ ∞«∫∞∑Œ º“∆Æ∞° ¿œæÓ≥™º≠ ¥¿∏Æ∞‘ √≥∏Æµ .
                    // ±◊∑°º≠ ListViewItemSorter∏¶ æ¯æ÷¡‹
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

                ~MCListView()
                {
                    //base.Finalize();

                    m_nSortColumn = -1;

                }
            }

            public class ListViewItemComparer : IComparer
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

                public int Compare(object x, object y)
                {
                    int iResult;
                    string sTextX;
                    string sTextY;

                    // 2007.01.16. Aiden Koo
                    // πÆ¿⁄ø≠¿Ã º˝¿⁄«¸Ωƒ¿Œ ∞ÊøÏ¥¬ 10 ¿Ã 9 ∫∏¥Ÿ ¿€¿∫∞Õ¿∏∑Œ √≥∏Æµ . µ˚∂Ûº≠ πÆ¿⁄ø≠¿Ã º˝¿⁄«¸¿Œ ∞ÊøÏ º˝¿⁄∑Œ ∫Ø»Ø«œø© ∫Ò±≥«œµµ∑œ «‘.
                    // 2007.08.20. Aiden Koo
                    // æ∆∑° ∑Œ¡˜¿Ã µÈæÓ∞• ∞ÊøÏ º“∆√ Ω√∞£¿Ã ø¿∑°∞…∏≤. µ˚∂Ûº≠ πÆ¿⁄ø≠ ±‚∫ª¿∏∑Œ ¥ŸΩ√ ∫π±Õ«‘.
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
                        if (((ListViewItem)x).SubItems.Count > m_iCol)
                        {
                            sTextX = ((ListViewItem)x).SubItems[m_iCol].Text;
                        }
                        else
                        {
                            sTextX = "";
                        }
                        if (((ListViewItem)y).SubItems.Count > m_iCol)
                        {
                            sTextY = ((ListViewItem)y).SubItems[m_iCol].Text;
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

                        return (m_bSortOrder ? iResult : iResult * -1);
                    }
                    catch (Exception e)
                    {
                        MessageBox.Show(e.Message, "Compare Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return -1;
                    }
                }
            }
        }
    }
}
