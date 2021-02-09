
using System.Drawing;
using System.Data;
using System.Windows.Forms;
using System.Collections;
using System.Diagnostics;
using System;
using Miracom.SmartWeb.FWX;
//-----------------------------------------------------------------------------
//
//   System      : FMBClient
//   File Name   : clsCommon.vb
//   Description : Common Class
//
//   FMB Version : 1.0.0
//
//   Function List
//       -
//
//   Detail Description
//       -
//
//   History
//       - 2005-03-04 : Created by H.K.Kim
//
//   Copyright(C) 1998-2004 MIRACOM,INC.
//   All rights reserved.
//
//-----------------------------------------------------------------------------

// GetIndexedControl()?ì„œ??Control?¤ì„ ?´ë¦„?¼ë¡œ ?•ë ¬?˜ê¸° ?„í•´ ë§Œë“  class
namespace Miracom.SmartWeb.UI
{
	public class ControlNameSort : IComparer
	{
		
		public int Compare(object con1, object con2)
		{
			
			try
			{
				string sConName;
				int iCon1;
				int iCon2;
				int i;
				
				sConName = ((Control) con1).Name;
				for (i = sConName.Length - 1; i >= 0; i--)
				{
					if (! char.IsNumber(sConName[i]))
					{
						break;
					}
				}
				iCon1 = CmnFunction.ToInt(sConName.Substring(i + 1, sConName.Length - i - 1));
				
				sConName = ((Control) con2).Name;
				for (i = sConName.Length - 1; i >= 0; i--)
				{
					if (! char.IsNumber(sConName[i]))
					{
						break;
					}
				}
				iCon2 = CmnFunction.ToInt(sConName.Substring(i + 1, sConName.Length - i - 1));
				
				return new CaseInsensitiveComparer().Compare(iCon1, iCon2);
				
			}
			catch (Exception ex)
			{
				CmnFunction.ShowMsgBox("ControlNameSort.Compare()" + "\r\n" + ex.Message, "FMB Client", MessageBoxButtons.OK, 1);
                return 0;
			}
			
		}
		
	}
	
	// Implements the manual sorting of items by columns.
	public class ListViewItemComparer : IComparer
	{
		
		
		private int col;
		private bool IsDecending = false;
		
		public ListViewItemComparer()
		{
			col = 0;
		}
		
		public ListViewItemComparer(int column, bool bDec)
		{
			col = column;
			IsDecending = bDec;
		}
		
		public int Compare(object x, object y)
		{
			
			try
			{
				int iResult;
				string sTextX;
				string sTextY;
				
				if (((ListViewItem) x).SubItems.Count > col)
				{
					sTextX = ((ListViewItem) x).SubItems[col].Text;
				}
				else
				{
					sTextX = "";
				}
				if (((ListViewItem) y).SubItems.Count > col)
				{
					sTextY = ((ListViewItem) y).SubItems[col].Text;
				}
				else
				{
					sTextY = "";
				}
				iResult = string.Compare(sTextX, sTextY);
				return (IsDecending ? iResult * - 1 : iResult);
				
			}
			catch (Exception ex)
			{
				CmnFunction.ShowMsgBox("ListViewItemComparer.Compare()" + "\r\n" + ex.Message, "FMB Client", MessageBoxButtons.OK, 1);
				return 0;
			}
			
		}
		
	}
	
	public class clsDesignListTag : object
	{
		
		
		public clsDesignListTag()
		{
			
		}
		
		private string m_sKey = "";
		private string m_sCategory = "";
		private string m_sFactory = "";
		private string m_sLayout = "";
		private bool m_bResAttachFlag = false;
		private bool m_bLoadFlag = false;
		
		public string Key
		{
			get
			{
				return m_sKey;
			}
			set
			{
				if (m_sKey.Equals(value) == false)
				{
                    m_sKey = value;
				}
			}
		}
		
		public string Category
		{
			get
			{
				return m_sCategory;
			}
			set
			{
                if (m_sCategory.Equals(value) == false)
				{
                    m_sCategory = value;
				}
			}
		}
		
		public string Factory
		{
			get
			{
				return m_sFactory;
			}
			set
			{
                if (m_sFactory.Equals(value) == false)
				{
                    m_sFactory = value;
				}
			}
		}
		
		public string Layout
		{
			get
			{
				return m_sLayout;
			}
			set
			{
                if (m_sLayout.Equals(value) == false)
				{
                    m_sLayout = value;
				}
			}
		}
		
		public bool ResAttachFlag
		{
			get
			{
				return m_bResAttachFlag;
			}
			set
			{
                if (m_bResAttachFlag.Equals(value) == false)
				{
                    m_bResAttachFlag = value;
				}
			}
		}
		
		public bool LoadFlag
		{
			get
			{
				return m_bLoadFlag;
			}
			set
			{
                if (m_bLoadFlag.Equals(value) == false)
				{
                    m_bLoadFlag = value;
				}
			}
		}
		
		public void SetTagData(string sKey, string sCategory, string sFactory, string sLayout, bool bResAttachFlag)
		{
			
			try
			{
				Key = sKey;
				Category = sCategory;
				Factory = sFactory;
				Layout = sLayout;
				ResAttachFlag = bResAttachFlag;
				
			}
			catch (Exception ex)
			{
				CmnFunction.ShowMsgBox("clsDesignListTag.SetTagData()" + "\r\n" + ex.Message, "FMB Client", MessageBoxButtons.OK, 1);
			}
			
		}
		
	}
}
