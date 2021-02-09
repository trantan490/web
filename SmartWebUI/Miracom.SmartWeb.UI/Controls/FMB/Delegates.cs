using System.Drawing;
using System.Data;
using System.Windows.Forms;
using System.Collections;
using System.Diagnostics;
using System;

namespace Miracom.FMBUI
{
	public delegate void CtrlStyleChangedEventHandler(object sender, CtrlStyle_EventArgs e);
	
	public delegate void CtrlPressedEventHandler(object sender, System.EventArgs e);
	public delegate void CtrlMouseEnterEventHandler(object sender, System.EventArgs e);
	public delegate void CtrlMouseLeaveEventHandler(object sender, System.EventArgs e);
	public delegate void CtrlGotFocusEventHandler(object sender, System.EventArgs e);
	public delegate void CtrlLostFocusEventHandler(object sender, System.EventArgs e);
	public delegate void CtrlMouseDownEventHandler(object sender, System.Windows.Forms.MouseEventArgs e);
	public delegate void CtrlMouseUpEventHandler(object sender, System.Windows.Forms.MouseEventArgs e);
	public delegate void CtrlMouseMoveEventHandler(object sender, System.Windows.Forms.MouseEventArgs e);
	public delegate void CtrlKeyDownEventHandler(object sender, System.Windows.Forms.KeyEventArgs e);
	
	public delegate void CtrlContextMenuEventHandler(object sender, CtrlContextMenu_EventArgs e);
	
	public class CtrlStyle_EventArgs : System.EventArgs
	{


        private Enums.eCtrlProperty m_ePropertyName = new Enums.eCtrlProperty();
		private object m_oPropertyValue = null;
		
		public CtrlStyle_EventArgs()
		{
			
		}
		
		public CtrlStyle_EventArgs(Enums.eCtrlProperty propertyName, object propertyValue)
		{
			
			m_ePropertyName = propertyName;
			m_oPropertyValue = propertyValue;
			
		}
		
		public Enums.eCtrlProperty PropertyName
		{
			get
			{
				return m_ePropertyName;
			}
		}
		
		public object PropertyValue
		{
			get
			{
				return m_oPropertyValue;
			}
		}
		
	}
	
	public class CtrlContextMenu_EventArgs : System.EventArgs
	{
		
		
		private object m_oCtrlSender = null;
		private string m_sFunctionName = "";
		
		public CtrlContextMenu_EventArgs()
		{
			
		}
		
		public CtrlContextMenu_EventArgs(string sFunctionName, object oCtrlSender)
		{
			
			m_sFunctionName = sFunctionName;
			m_oCtrlSender = oCtrlSender;
			
		}
		
		public string FunctionName
		{
			get
			{
				return m_sFunctionName;
			}
			set
			{
				if (m_sFunctionName.Equals(value) == false)
				{
					m_sFunctionName = value;
				}
			}
		}
		
		public object CtrlSender
		{
			get
			{
				return m_oCtrlSender;
			}
			set
			{
				if (m_oCtrlSender.Equals(value) == false)
				{
					m_oCtrlSender = value;
				}
			}
		}
		
	}
}
