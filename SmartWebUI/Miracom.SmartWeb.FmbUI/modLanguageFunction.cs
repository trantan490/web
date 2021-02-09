
using System.Drawing;
using System.Data;
using System.Windows.Forms;
using System.Collections;
using System.Diagnostics;
using System;
using System.Xml;
using Infragistics.Win.UltraWinEditors;
using Infragistics.Win.UltraWinDock;
using Miracom.SmartWeb.FWX;

//-----------------------------------------------------------------------------
//
//   System      : FMBClient
//   File Name   : LoadCaptionResource.vb
//   Description : Common Laguage Funcion Module
//
//   FMB Version : 1.0.0
//
//   Function List
//       - LoadCaptionResource() : Check the conditions before transaction
//       - ToClientLanguage() : Change Language of the form' Text to Client Language
//       - FindLanguage() : Find Client Language of source string
//       - ConvertMessage() : Change Language of the form' Control' Text to Client Language
//       - GetMessage() : Find desired Message data
//       - LoadMessageResource() : Load Message Data
//
//   Detail Description
//       -
//
//   History
//       - 2005-03-21 : Created by H.K.Kim
//
//   Copyright(C) 1998-2004 MIRACOM,INC.
//   All rights reserved.
//
//-----------------------------------------------------------------------------

namespace Miracom.SmartWeb.UI
{
	public sealed class modLanguageFunction
	{
		
		// LoadCaptionResource()
		//       -  Load Text Data
		// Return Value
		//       -  True / False
		// Arguments
		//       -
		//
		public class CaptionLangSort : IComparer
		{
			
			public int Compare(object l1, object l2)
			{
                GlobalVariable.gLanguageData lang1;
                GlobalVariable.gLanguageData lang2;

                lang1 = (GlobalVariable.gLanguageData)l1;
                lang2 = (GlobalVariable.gLanguageData)l2;
				
				return new CaseInsensitiveComparer().Compare(lang1.Key, lang2.Key);
			}
		}
		
		public static bool LoadCaptionResource()
		{
			
			const int MENU_TYPE = 0;
			const int BUTTON_TYPE = 1;
			const int OTHER_TYPE = 2;
			
			System.IO.FileStream fs = null;
			System.Xml.XmlTextReader xtr = null;

            GlobalVariable.gLanguageData lang;
			int iCapType = 0;
			string[] sTemp = new string[3];
			int i;
			CaptionLangSort langSort;
			
			try
			{
				GlobalVariable.gaButtonLanguage.Clear();
				GlobalVariable.gaMenuLanguage.Clear();
				GlobalVariable.gaTextLanguage.Clear();
				
				// Open XML File
				if (Microsoft.VisualBasic.FileSystem.Dir(modGlobalConstant.MP_CAPTION_FILE, 0) == "")
				{
					return true;
				}
				
				fs = new System.IO.FileStream(modGlobalConstant.MP_CAPTION_FILE, System.IO.FileMode.Open, System.IO.FileAccess.Read, System.IO.FileShare.ReadWrite);
				
				// Set xmlReader
				xtr = new XmlTextReader(fs);
				
				while (xtr.Read())
				{
					switch (xtr.NodeType)
					{
						case XmlNodeType.Element:
							
							switch (xtr.Name.ToUpper())
							{
								case "BUTTON":
									
									iCapType = BUTTON_TYPE;
									break;
								case "MENU":
									
									iCapType = MENU_TYPE;
									break;
								case "OTHER":
									
									iCapType = OTHER_TYPE;
									break;
								case "TEXT":
									
									xtr.MoveToAttribute("Key");
									lang.Key = xtr.Value.ToUpper();
									
									i = 0;
									while (xtr.Read())
									{
										if (xtr.NodeType == XmlNodeType.Text)
										{
											sTemp[i] = xtr.Value;
											i++;
										}
										else if (xtr.NodeType == XmlNodeType.EndElement && xtr.Name.ToUpper() == "TEXT")
										{
											break;
										}
									}
									lang.Lang_1 = sTemp[0];
									lang.Lang_2 = sTemp[1];
									lang.Lang_3 = sTemp[2];
									
									switch (iCapType)
									{
										case BUTTON_TYPE:
											
											GlobalVariable.gaButtonLanguage.Add(lang);
											break;
										case MENU_TYPE:
											
											GlobalVariable.gaMenuLanguage.Add(lang);
											break;
										case OTHER_TYPE:
											
											GlobalVariable.gaTextLanguage.Add(lang);
											break;
									}
									break;
							}
							break;
							
					}
				}
				
				langSort = new CaptionLangSort();
				GlobalVariable.gaButtonLanguage.Sort(langSort);
				GlobalVariable.gaMenuLanguage.Sort(langSort);
				GlobalVariable.gaTextLanguage.Sort(langSort);
				
			}
			catch (Exception)
			{
				if (!(xtr == null))
				{
					CmnFunction.ShowMsgBox("Occured error in read language file." + "\r\n" + "\r\n" + "\'" + xtr.Name + xtr.Value + "\'", "FMB Client", MessageBoxButtons.OK, 1);
				}
			}
			finally
			{
				// Close XML File
				if (!(xtr == null))
				{
					xtr.Close();
				}
				if (!(fs == null))
				{
					fs.Close();
				}
			}
			
			return true;
			
		}
		
		// ToClientLanuage()
		//       -  Change Language of the form' Text to Client Language
		// Return Value
		//       -  True / False
		// Arguments
		//       -  ByVal l_form As Form
		//
		public static bool ToClientLanguage(Form l_form)
		{
			
			try
			{
				l_form.Text = FindLanguage(l_form.Text, 0);
				
				if (ConvertMessage(l_form.Controls) == false)
				{
					return false;
				}
				if (ConvertContextMenu(l_form.Controls) == false)
				{
					return false;
				}
			}
			catch (Exception ex)
			{
				CmnFunction.ShowMsgBox("modLanguageFunction.ToClientLanguage()" + "\r\n" + ex.Message, "FMB Client", MessageBoxButtons.OK, 1);
				return false;
			}
			
			return true;
			
		}
		
		// ToClientLanuage()
		//       -  Change Language of the form' Text to Client Language
		// Return Value
		//       -  True / False
		// Arguments
		//       -  ByVal mnuItems As Menu.MenuItemCollection
		//
		public static bool ToClientLanguage(Menu.MenuItemCollection mnuItems)
		{
			
			try
			{
				MenuItem mnuItem;
				
				foreach (MenuItem tempLoopVar_mnuItem in mnuItems)
				{
					mnuItem = tempLoopVar_mnuItem;
					if (mnuItem.Text != "")
					{
						mnuItem.Text = FindLanguage(mnuItem.Text, 2);
					}
				}
			}
			catch (Exception ex)
			{
				CmnFunction.ShowMsgBox("modLanguageFunction.ToClientLanguage()" + "\r\n" + ex.Message, "FMB Client", MessageBoxButtons.OK, 1);
				return false;
			}
			
			return true;
			
		}
		
		// FindLanguage()
		//       -  Find Client Language of source string
		// Return Value
		//       -  True / False
		// Arguments
		//       -  ByVal S As String :
		//       -
		//
		public class CaptionBinarySearch : IComparer
		{
			
			public int Compare(object lang, object txt)
			{
                GlobalVariable.gLanguageData l;

                l = (GlobalVariable.gLanguageData)lang;
				
				return new CaseInsensitiveComparer().Compare(l.Key, txt);
			}
		}
		
		public static string FindLanguage(string S, int iType)
		{
			
			bool flag;
            GlobalVariable.gLanguageData lang;
			ArrayList langArray = null;
			//CaptionBinarySearch langCompare;
			string stxt = "";
			string sRight = "";
			string sLeft = "";
			int i = 0;
			
			try
			{
				flag = false;
				if (S == "")
				{
					return "";
				}
				
				switch (iType)
				{
					case 0:
						
						langArray = GlobalVariable.gaTextLanguage;
						break;
					case 1:
						
						langArray = GlobalVariable.gaButtonLanguage;
						break;
					case 2:
						
						langArray = GlobalVariable.gaMenuLanguage;
						break;
				}
				if (langArray.Count <= 0)
				{
					return S;
				}
				
				stxt = S;
				for (i = stxt.Length - 1; i >= 0; i--)
				{
					if (stxt[i] != ' ')
					{
						break;
					}
				}
				stxt = stxt.Substring(0, i + 1);
				
				for (i = 0; i <= stxt.Length - 1; i++)
				{
					if (stxt[i] != ' ')
					{
						break;
					}
				}
				sLeft = stxt.Substring(0, i);
				stxt = stxt.Substring(i);
				
				if (stxt.Substring(stxt.Length - 1, 1) == ":")
				{
					for (i = stxt.Length - 2; i >= 0; i--)
					{
                        if (stxt[i] != ' ')
						{
							break;
						}
					}
					
					sRight = stxt.Substring(i + 1);
					stxt = stxt.Substring(0, i + 1);
					flag = true;
				}
				
				string sCaption = "";
				for (i = 0; i <= langArray.Count - 1; i++)
				{
					if (modGlobalVariable.gsPrevLanguage == '1')
					{
                        sCaption = ((GlobalVariable.gLanguageData)langArray[i]).Lang_1;
					}
					else if (modGlobalVariable.gsPrevLanguage == '2')
					{
                        sCaption = ((GlobalVariable.gLanguageData)langArray[i]).Lang_2;
					}
					else
					{
                        sCaption = ((GlobalVariable.gLanguageData)langArray[i]).Lang_3;
					}
					
					if (sCaption == stxt)
					{
						break;
					}
				}
				if (i == langArray.Count)
				{
					return S;
				}

                lang = (GlobalVariable.gLanguageData)langArray[i];
				
				switch (GlobalVariable.gcLanguage)
				{
					case '1':
						
						S = sLeft + lang.Lang_1;
						break;
					case '2':
						
						S = sLeft + lang.Lang_2;
						break;
					case '3':
						
						S = sLeft + lang.Lang_3;
						break;
				}
				
				if (flag == true)
				{
					S = S + sRight;
				}
				
				return S;
				
			}
			catch (Exception ex)
			{
				CmnFunction.ShowMsgBox("modLanguageFunction.FindLanguage()" + "\r\n" + ex.Message, "FMB Client", MessageBoxButtons.OK, 1);
				return S;
			}
			
		}
		
		// ConvertMessage()
		//       -  Change Language of the form' Control' Text to Client Language
		// Return Value
		//       -  True / False
		// Arguments
		//       -  ByRef colControl As System.Windows.Forms.Control.ControlCollection : Form's Controls
		//
		private static bool ConvertMessage(System.Windows.Forms.Control.ControlCollection colControl)
		{
			
			try
			{
				int i;
				int j;
				Control l_Control;
				
				l_Control = null;
				
				foreach (Control tempLoopVar_l_Control in colControl)
				{
					l_Control = tempLoopVar_l_Control;
					if (l_Control is Panel)
					{
						if (l_Control is TabPage)
						{
							if (CmnFunction.Trim(((TabPage) l_Control).Text) != "")
							{
								((TabPage) l_Control).Text = FindLanguage(((TabPage) l_Control).Text, 0);
							}
						}
						ConvertMessage(l_Control.Controls);
					}
					else if (l_Control is GroupBox)
					{
						if (CmnFunction.Trim(((GroupBox) l_Control).Text) != "")
						{
							((GroupBox) l_Control).Text = FindLanguage(((GroupBox) l_Control).Text, 0);
						}
						ConvertMessage(l_Control.Controls);
					}
					else if (l_Control is TabControl)
					{
						ConvertMessage(l_Control.Controls);
					}
					else if (l_Control is Label)
					{
						((Label) l_Control).Text = FindLanguage(((Label) l_Control).Text, 0);
					}
					else if (l_Control is RadioButton)
					{
						((RadioButton) l_Control).Text = FindLanguage(((RadioButton) l_Control).Text, 0);
					}
					else if (l_Control is Button)
					{
						((Button) l_Control).Text = FindLanguage(((Button) l_Control).Text, 1);
					}
					else if (l_Control is CheckBox)
					{
						((CheckBox) l_Control).Text = FindLanguage(((CheckBox) l_Control).Text, 0);
					}
					else if (l_Control is UltraCheckEditor)
					{
						((UltraCheckEditor) l_Control).Text = FindLanguage(((UltraCheckEditor) l_Control).Text, 0);
					}
					else if (l_Control is ListView)
					{
						for (i = 0; i <= ((ListView) l_Control).Columns.Count - 1; i++)
						{
							((ListView) l_Control).Columns[i].Text = FindLanguage(((ListView) l_Control).Columns[i].Text, 0);
						}
					}
					else if (l_Control is FarPoint.Win.Spread.FpSpread)
					{
                        if (CmnFunction.Trim(((FarPoint.Win.Spread.FpSpread)l_Control).Tag) == "Header No Change")
						{
							return true;
						}
                        else if (CmnFunction.Trim(((FarPoint.Win.Spread.FpSpread)l_Control).Tag) == "Change Cell")
						{
                            for (j = 0; j <= ((FarPoint.Win.Spread.FpSpread)l_Control).Sheets[0].RowCount - 1; j++)
							{
                                for (i = 0; i <= ((FarPoint.Win.Spread.FpSpread)l_Control).Sheets[0].ColumnCount - 1; i++)
								{
                                    ((FarPoint.Win.Spread.FpSpread)l_Control).Sheets[0].Cells[j, i].Text = FindLanguage(((FarPoint.Win.Spread.FpSpread)l_Control).Sheets[0].Cells[j, i].Text, 0);
								}
							}
						}
						else
						{
                            for (j = 0; j <= ((FarPoint.Win.Spread.FpSpread)l_Control).Sheets[0].ColumnHeader.RowCount - 1; j++)
							{
                                for (i = 0; i <= ((FarPoint.Win.Spread.FpSpread)l_Control).Sheets[0].ColumnHeader.Columns.Count - 1; i++)
								{
                                    ((FarPoint.Win.Spread.FpSpread)l_Control).Sheets[0].ColumnHeader.Cells[j, i].Text = FindLanguage(((FarPoint.Win.Spread.FpSpread)l_Control).Sheets[0].ColumnHeader.Cells[j, i].Text, 0);
								}
							}
                            for (j = 0; j <= ((FarPoint.Win.Spread.FpSpread)l_Control).Sheets[0].RowHeader.Rows.Count - 1; j++)
							{
                                for (i = 0; i <= ((FarPoint.Win.Spread.FpSpread)l_Control).Sheets[0].RowHeader.ColumnCount - 1; i++)
								{
                                    ((FarPoint.Win.Spread.FpSpread)l_Control).Sheets[0].RowHeader.Cells[j, i].Text = FindLanguage(((FarPoint.Win.Spread.FpSpread)l_Control).Sheets[0].RowHeader.Cells[j, i].Text, 0);
								}
							}
						}
						
					}
				}
				
				return true;
				
			}
			catch (Exception ex)
			{
				CmnFunction.ShowMsgBox("modLanguageFunction.ConvertMessage()" + "\r\n" + ex.Message, "FMB Client", MessageBoxButtons.OK, 1);
				return false;
			}
			
		}
		
		// GetMessage()
		//       -  Find desired Message data
		// Return Value
		//       -  string : Return Message
		// Arguments
		//       -  i as integer : Message index
		//       -
		//
		public static string GetMessage(int i)
		{
			string returnValue;
			try
			{
                if (GlobalVariable.giMaxMessageData < i)
				{
					returnValue = "";
				}
				else
				{
					if (GlobalVariable.gcLanguage == '2')
					{
                        if (GlobalVariable.gsMessageData[1, i] == null)
						{
							returnValue = "No Error Message";
						}
						else
						{
                            returnValue = GlobalVariable.gsMessageData[1, i];
						}
					}
					else if (GlobalVariable.gcLanguage == '3')
					{
                        if (GlobalVariable.gsMessageData[2, i] == null)
						{
							returnValue = "No Error Message";
						}
						else
						{
                            returnValue = GlobalVariable.gsMessageData[2, i];
						}
					}
					else
					{
                        if (GlobalVariable.gsMessageData[0, i] == null)
						{
							returnValue = "No Error Message";
						}
						else
						{
                            returnValue = GlobalVariable.gsMessageData[0, i];
						}
					}
				}
				return returnValue;
				
			}
			catch (Exception ex)
			{
				CmnFunction.ShowMsgBox("modLanguageFunction.GetMessage()" + "\r\n" + ex.Message, "FMB Client", MessageBoxButtons.OK, 1);
				return string.Empty;
			}
			
		}
		
		// LoadMessageResource()
		//       -  Load Message Data
		// Return Value
		//       -  Boolean : Return True/False
		// Arguments
		//       -
		//
		public static bool LoadMessageResource()
		{
			
			System.IO.FileStream fs = null;
			System.Xml.XmlTextReader xtr = null;
			
			string[] sTemp = new string[3];
			int i;
			int j;
            GlobalVariable.gsMessageData = new string[3, GlobalVariable.giMaxMessageData];
			j = 1;
			try
			{
				
				// Open XML File
				if (Microsoft.VisualBasic.FileSystem.Dir(modGlobalConstant.MP_MESSAGE_FILE, 0) == "")
				{
					return true;
				}
				
				fs = new System.IO.FileStream(modGlobalConstant.MP_MESSAGE_FILE, System.IO.FileMode.Open, System.IO.FileAccess.Read, System.IO.FileShare.ReadWrite);
				
				// Set xmlReader
				xtr = new XmlTextReader(fs);
				
				while (xtr.Read())
				{
					switch (xtr.NodeType)
					{
						case XmlNodeType.Element:

                            switch (xtr.Name.ToUpper())
                            {
                                case "TEXT":

                                    xtr.MoveToAttribute("Key");
                                    j = CmnFunction.ToInt(xtr.Value);

                                    i = 0;
                                    while (xtr.Read())
                                    {
                                        if (xtr.NodeType == XmlNodeType.Text)
                                        {
                                            sTemp[i] = xtr.Value.Trim();
                                            i++;
                                        }
                                        else if (xtr.NodeType == XmlNodeType.EndElement && xtr.Name.ToUpper() == "TEXT")
                                        {
                                            break;
                                        }
                                    }

                                    GlobalVariable.gsMessageData[0, j] = sTemp[0];
                                    GlobalVariable.gsMessageData[1, j] = sTemp[1];
                                    GlobalVariable.gsMessageData[2, j] = sTemp[2];
                                    break;
                            }
                            break;
							
					}
				}
				
			}
			catch (Exception)
			{
				if (!(xtr == null))
				{
					CmnFunction.ShowMsgBox("Occured error in read language file." + "\r\n" + "\r\n" + "\'" + xtr.Name + xtr.Value + "\'", "FMB Client", MessageBoxButtons.OK, 1);
				}
			}
			finally
			{
				// Close XML File
				if (!(xtr == null))
				{
					xtr.Close();
				}
				if (!(fs == null))
				{
					fs.Close();
				}
			}
			
			return true;
			
		}
		
		// ConvertContextMenu()
		//       -  Change Language of context menu Text to Client Language
		// Return Value
		//       -  True / False
		// Arguments
		//       -  ByRef colControl As System.Windows.Forms.Control.ControlCollection : Form's Controls
		//
		private static bool ConvertContextMenu(System.Windows.Forms.Control.ControlCollection control)
		{
			
			try
			{
				MenuItem mnuItem;
				Control l_control;
				
				mnuItem = null;
				l_control = null;
				foreach (Control tempLoopVar_l_control in control)
				{
					l_control = tempLoopVar_l_control;
					if (l_control is Panel)
					{
						ConvertContextMenu(l_control.Controls);
					}
					else if (l_control is GroupBox)
					{
						if (CmnFunction.Trim(((GroupBox) l_control).Text) != "")
						{
							((GroupBox) l_control).Text = FindLanguage(((GroupBox) l_control).Text, 0);
						}
						ConvertContextMenu(l_control.Controls);
					}
					else if (l_control is TabControl)
					{
						ConvertContextMenu(l_control.Controls);
					}
					else if (l_control is DockableWindow)
					{
						ConvertContextMenu(l_control.Controls);
					}
					else if (l_control is WindowDockingArea)
					{
						ConvertContextMenu(l_control.Controls);
					}
					else if (l_control is DockableWindow)
					{
						ConvertContextMenu(l_control.Controls);
					}
					else
					{
						
						if (l_control.ContextMenu != null)
						{
							foreach (MenuItem tempLoopVar_mnuItem in l_control.ContextMenu.MenuItems)
							{
								mnuItem = tempLoopVar_mnuItem;
								if (mnuItem.Text != "")
								{
									mnuItem.Text = FindLanguage(mnuItem.Text, 2);
								}
							}
						}
					}
				}
				
				return true;
				
			}
			catch (Exception ex)
			{
				CmnFunction.ShowMsgBox("modLanguageFunction.ConvertMessage()" + "\r\n" + ex.Message, "FMB Client", MessageBoxButtons.OK, 1);
				return false;
			}
			
		}
		
	}
	
}
