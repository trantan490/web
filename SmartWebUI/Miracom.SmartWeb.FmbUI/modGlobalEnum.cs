
using System.Drawing;
using System.Data;
using System.Windows.Forms;
using System.Collections;
using System.Diagnostics;
using System;

namespace Miracom.SmartWeb.UI
{
	public sealed class modGlobalEnum
	{
		
		public enum IMLSMALLICON_INDEX
		{
			IDX_UDG = 0,
			IDX_FACTORY = 1,
			IDX_LAYOUT = 2,
			IDX_RES = 3,
			IDX_RES_DOWN = 4,
			IDX_REFRESH = 5,
			IDX_MAT = 6,
			IDX_FLOW = 7,
			IDX_OPER = 8,
			IDX_EVENT = 9,
			IDX_HISTORY = 10,
			IDX_HISTORY_DEL = 11,
			IDX_LOT = 12,
			IDX_USER = 13,
			IDX_USER_GRP = 14,
			IDX_GCM_DATA = 15,
			IDX_SAVE = 16,
			IDX_ZOOM_IN = 17,
			IDX_ZOOM_OUT = 18,
			IDX_HELP = 19,
			IDX_PRINT = 20,
			IDX_DELETE = 21,
			IDX_DESIGNER = 22,
			IDX_PROPERTIES = 23,
			IDX_FILE = 54
		}
		
		public enum IMLDESIGNTOOLBARS_INDEX
		{
			IDX_LEFTS = 0,
			IDX_CENTERS = 1,
			IDX_RIGHTS = 2,
			IDX_TOPS = 3,
			IDX_MIDDLES = 4,
			IDX_BOTTOMS = 5,
			IDX_HEIGHT = 6,
			IDX_WIDTH = 7,
			IDX_BOTH = 8,
			IDX_H_EQUAL = 9,
			IDX_H_INCREASE = 10,
			IDX_H_DECREASE = 11,
			IDX_H_REMOVE = 12,
			IDX_V_EQUAL = 13,
			IDX_V_INCREASE = 14,
			IDX_V_DECREASE = 15,
			IDX_V_REMOVE = 16,
			IDX_BRING_TO_FRONT = 17,
			IDX_SEND_TO_BACK = 18,
			IDX_REFRESH = 19,
			IDX_SAVE = 20,
			IDX_ZOOM_IN = 21,
			IDX_ZOOM_OUT = 22,
			IDX_PRINT = 23,
			IDX_DELETE = 24,
			IDX_DESIGNER = 25,
			IDX_PROPERTIES = 26,
			IDX_UPDATE = 27
		}
		
		public enum DATE_TIME_FORMAT
		{
			LONGDATETIME = 0,
			SHORDATETIME = 1,
			LONGDATE = 2,
			SHORTDATE = 3,
			LONGTIME = 4,
			SHORTTIME = 5,
			NONE = 6
			
			//STANDARD
			//YYYY/MM/DD HH:MM:SS
			//YYYY/MM/DD HH:MM:SS
			//YYYY/MM/DD
			//YYYY/MM/DD
			//HH:MM:SS
			//HH:MM:SS
			
			//ENGLISH
			//MM DD,YYYY HH:MM:SS
			//YYYY/MM/DD HH:MM:SS
			//MM DD,YYYY
			//YYYY/MM/DD
			//HH:MM:SS
			//HH:MM:SS
			
			//KOREA
			//YYYY??MM??DD???¤ì „(?¤í›„) hh??mmë¶?ssì´?
			//YYYY/MM/DD HH:MM:SS
			//YYYY??MM??DD??
			//YYYY/MM/DD
			//HH??mmë¶?ssì´?
			//HH:MM:SS
			
		}
		
		public enum LANG_FORMAT
		{
			STANDARD = 0,
			ENGLISH = 1,
			KOREA = 2,
			THIRDLANG = 3
		}
		
		public enum MENU_TYPE
		{
			MENU_TEXT = 0,
			MENU_NAME = 1,
			FUNCTION_NAME = 2
		}
		
	}
	
}
