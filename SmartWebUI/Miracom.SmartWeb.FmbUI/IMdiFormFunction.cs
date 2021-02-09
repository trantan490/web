
using System.Drawing;
using System.Data;
using System.Windows.Forms;
using System.Collections;
using System.Diagnostics;
using System;

namespace Miracom.SmartWeb.UI
{
	public interface IMdiFormFunction
	{
		
		bool RefreshUdrGroupList();
		bool RefreshFileList();
		bool RefreshDesignList(string sStep, string sFactory, string sLayout, string sResource, string sMoveLayout);
		void CreateResourceEvent(string sFactory, string sResource, string sUpDownflag);
		void DeleteResourceEvent(string sFactory, string sResource);
		void ArrageLayouts(ArrayList sLayouts, int iLayoutCount);

        ToolStrip GetToolBar();
		
	}
	
	public sealed class modInterface
	{
		
		public static IMdiFormFunction gIMdiForm;
		
	}
}
