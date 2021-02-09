using System.Drawing;
using System.Data;
using System.Windows.Forms;
using System.Collections;
using System.Diagnostics;
using System;
using System.Runtime.InteropServices;


namespace Miracom.UI
{
	sealed class MCModuleAPI
	{
		
		public const int MAX_COLUMN_WIDTH = 500;
		public const int MIN_COLUMN_WIDTH = 50;
		public const int MAX_LIST_COUNT = 20;
		public const int BONUS_LISTVIEW_HEIGHT = 2;
		public const int BONUS_COLUMN_WIDTH = 4;
		public const int BONUS_COLUMN_WIDTH_WITH_IMAGE = 2;
        public const int BONUS_COLUMN_WIDTH_WITH_CHECK_BOX = 16;

        #region "API ฐüทร"

        public const int LVM_FIRST = 0x1000;
		public const int LVM_SETCOLUMN = (LVM_FIRST + 26);
		public const int LVM_GETHEADER = (LVM_FIRST + 31);
		
		public const int HDI_FORMAT = 0x1;
		public const int HDI_IMAGE = 0x10;
		
		public const int HDF_LEFT = 0x0;
		public const int HDF_RIGHT = 0x1;
		public const int HDF_CENTER = 0x2;
		public const int HDF_JUSTIFYMASK = 0x3;
		public const int HDF_RTLREADING = 0x4;
		public const int HDF_SORTDOWN = 0x200;
		public const int HDF_SORTUP = 0x400;
		public const int HDF_IMAGE = 0x800;
		public const int HDF_BITMAP_ON_RIGHT = 0x1000;
		public const int HDF_BITMAP = 0x2000;
		public const int HDF_STRING = 0x4000;
		public const int HDF_OWNERDRAW = 0x8000;
		
		public const int HDM_FIRST = 0x1200;
		public const int HDM_SETITEM = (HDM_FIRST + 4);
		public const int HDM_SETIMAGELIST = (HDM_FIRST + 8);
		
		[StructLayout(LayoutKind.Sequential, Pack = 8, CharSet = CharSet.Auto)]
        public struct LVCOLUMN
		{
			public int mask;
			public int fmt;
			public int cx;
			public IntPtr pszText;
			public int cchTextMax;
			public int iSubItem;
			public int iImage;
			public int iOrder;
		}
		
		[DllImport("User32.dll")]
        public static  extern IntPtr SendMessage(IntPtr hWnd, int Msg, int wParam, int lParam);
		
		[DllImport("User32.dll", CharSet = CharSet.Auto)]
        public static  extern IntPtr SendMessage(IntPtr hWnd, int Msg, int wParam, ref LVCOLUMN lParam);
		
		[DllImport("User32.dll", CharSet = CharSet.Auto)]
        public static  extern int ShowWindow(IntPtr hWnd, short cmdShow);
		
		#endregion
		
	}
	
}
