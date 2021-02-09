using System.Drawing;
using System.Data;
using System.Windows.Forms;
using System.Collections;
using System.Diagnostics;
using System;

namespace Miracom.FMBUI
{
	namespace Controls
	{
		public sealed class modDefines
		{
			
			public const int CTRL_TRACKER_SIZE = 3;
			public const int LINE_MININUM_SIZE = 1;
			public const int CTRL_MININUM_SIZE = 15;
			public const int CTRL_MAXIMUM_SIZE = 500;
			
			public const int CTRL_HITTEST_TRACKER_NULL = 0;
			public const int CTRL_HITTEST_TRACKER_LTM = 1; // ??
			public const int CTRL_HITTEST_TRACKER_RBM = 2; // ??
			public const int CTRL_HITTEST_TRACKER_L = 3; // ??
			public const int CTRL_HITTEST_TRACKER_R = 4; // ??
			public const int CTRL_HITTEST_TRACKER_RB = 5; // ??
			public const int CTRL_HITTEST_TRACKER_LB = 6; // ??
			public const int CTRL_HITTEST_TRACKER_LT = 7; // ??
			public const int CTRL_HITTEST_TRACKER_RT = 8; // ??
			public const int CTRL_HITTEST_TRACKER_ALL = 9;
			
			public static Color CTRL_TRACKER_FOCUS_COLOR = Color.FromArgb(0, 255, 0);
			public static Color CTRL_TRACKER_SELECT_COLOR = Color.FromArgb(240, 240, 240);
			
			public const int CTRL_LIGHT_SIZE = 15;
			
		}
	}
	
}
