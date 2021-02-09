using System.Drawing;
using System.Data;
using System.Windows.Forms;
using System.Collections;
using System.Diagnostics;
using System;

namespace Miracom.FMBUI
{
	public sealed class Enums
	{
		
		public enum eToolType
		{
			Null = - 1,
			Resource = 0,
			Rectangle = 1,
			Ellipse = 2,
			Triangle = 3,
			VerticalLine = 4,
			HorizontalLine = 5,
			PieType1 = 6,
			PieType2 = 7,
			PieType3 = 8,
			PieType4 = 9,
			TextType = 10
		}
		
		public enum eLineType
		{
			Null = 0,
			Vertical = 1,
			Horizontal = 2
		}
		
		public enum eCtrlProperty
		{
			PROP_ISNOEVENT = 0,
			PROP_ISPROCESSMODE = 1,
			PROP_ISUSEEVENTCOLOR = 2,
			PROP_FACTORY = 3,
			PROP_ISUSERGROUPDESIGN = 4,
			PROP_ISDELETERES = 5,
			PROP_ISSAVEFLAG = 6,
			PROP_KEY = 7,
			PROP_SIZE = 8,
			PROP_LOCATION = 9,
			PROP_TOOLTYPE = 10,
			PROP_RESTAGFLAG = 11,
			PROP_TEXT = 12,
			PROP_TEXTCOLOR = 13,
			PROP_TEXTSIZE = 14,
			PROP_TEXTSTYLE = 15,
			PROP_TEXTFONTNAME = 16,
			PROP_PRIMARYSTATUS = 17,
			PROP_UPDOWNFLAG = 18,
			PROP_RESOURCETYPE = 19,
			PROP_LASTEVENT = 20,
			PROP_PROCMODE = 21,
			PROP_CTRLMODE = 22,
			PROP_BACKCOLOR = 23,
			PROP_BACKHOTCOLOR = 24,
			PROP_BACKPRESSEDCOLOR = 25,
			PROP_BOARDERCOLOR = 26,
			PROP_BOARDERHOTCOLOR = 27,
			PROP_BOARDERPRESSEDCOLOR = 28,
			PROP_EVENTBACKCOLOR = 29,
			PROP_EVENTBACKHOTCOLOR = 30,
			PROP_EVENTBACKPRESSEDCOLOR = 31,
			PROP_IMAGEINDEX = 32,
			PROP_ISVIEWSIGNAL = 33,
			PROP_ZOOMSCALE = 34,
			PROP_TOOLTIPCOMMENT = 35,
			PROP_SIGNALCOLOR = 36,
			PROP_AREAID = 37,
			PROP_SUBAREAID = 38
		}
		
	}
	
}
