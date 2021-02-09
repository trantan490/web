using System;
using System.IO;
using System.Collections;
using System.Diagnostics;
using System.Windows.Forms;
using System.Xml;
using FarPoint.Win.Spread;
using Miracom.SmartWeb.FWX;
//using FarPoint.Win.Spread;

namespace Miracom.SmartWeb.UI
{
	public sealed class LanguageFunction
	{
		//
		// ToClientLanuage()
		//       -  Change Language of the form' Text to Client Language
		// Return Value
		//       -  True / False
		// Arguments
		//       -  ByVal l_form As Form :
		//       -

		public static bool ToClientLanguage(Form l_form)
		{

			try
			{
				l_form.Text = FindLanguage(l_form.Text, 2);
				if (ConvertMessage(l_form.Controls) == false)
				{
					return false;
				}

			}
			catch (Exception ex)
			{
                CmnFunction.ShowMsgBox("ToClientLanguage() is Failed." + "\r\n" + ex.Message, "Smart Web", MessageBoxButtons.OK, 1);
				return false;
			}

			return true;

		}

        public static bool ToClientLanguage(UserControl l_control)
        {

            try
            {
                l_control.Text = FindLanguage(l_control.Text, 2);
                if (ConvertMessage(l_control.Controls) == false)
                {
                    return false;
                }

            }
            catch (Exception ex)
            {
                CmnFunction.ShowMsgBox("ToClientLanguage() is Failed." + "\r\n" + ex.Message, "Smart Web", MessageBoxButtons.OK, 1);
                return false;
            }

            return true;

        }

		//
		// ConvertMessage()
		//       -  Change Language of the form' Control' Text to Client Language
		// Return Value
		//       -  True / False
		// Arguments
		//       -  ByRef colControl As System.Windows.Forms.Control.ControlCollection : Form's Controls
		//       -

		//public static bool ConvertMessage(ref System.Windows.Forms.Control.ControlCollection colControl)
		public static bool ConvertMessage(System.Windows.Forms.Control.ControlCollection colControl)
		{
			try
			{
				int i;
				int j;
				System.Windows.Forms.Control l_Control;

				l_Control = null;

                foreach (System.Windows.Forms.Control tempLoopVar_l_Control in colControl)
				{
					l_Control = tempLoopVar_l_Control;
                    if (l_Control is Panel)
                    {
                        if (l_Control is TabPage)
                        {
                            /**if (Miracom.SmartClient.UI.MCCore.StringTrim(((TabPage) l_Control).Text) != "")
							{
								((TabPage) l_Control).Text = FindLanguage(((TabPage) l_Control).Text, 0);
							}*/
                        }
                        ConvertMessage(l_Control.Controls);
                    }
                    else if (l_Control is GroupBox)
                    {
                        /**if (Miracom.SmartClient.UI.MCCore.StringTrim(((GroupBox) l_Control).Text) != "")
						{
							((GroupBox) l_Control).Text = FindLanguage(((GroupBox) l_Control).Text, 0);
						}*/
                        ConvertMessage(l_Control.Controls);
                    }
                    else if (l_Control is UserControl)
                    {
                        ConvertMessage(l_Control.Controls);
                    }
                    else if (l_Control is TabControl)
                    {
                        ConvertMessage(l_Control.Controls);
                    }
                    else if (l_Control is Label)
                    {
                        ((Label)l_Control).Text = FindLanguage(((Label)l_Control).Text, 0);
                    }
                    else if (l_Control is RadioButton)
                    {
                        ((RadioButton)l_Control).Text = FindLanguage(((RadioButton)l_Control).Text, 0);
                    }
                    else if (l_Control is Button)
                    {
                        ((Button)l_Control).Text = FindLanguage(((Button)l_Control).Text, 1);
                    }
                    else if (l_Control is CheckBox)
                    {
                        ((CheckBox)l_Control).Text = FindLanguage(((CheckBox)l_Control).Text, 0);
                    }
                    else if (l_Control is ListView)
                    {
                        for (i = 0; i <= ((ListView)l_Control).Columns.Count - 1; i++)
                        {
                            ((ListView)l_Control).Columns[i].Text = FindLanguage(((ListView)l_Control).Columns[i].Text, 0);
                        }
                    }
                    else if (l_Control is FpSpread)
                    {
                        if (((FpSpread)l_Control).Tag != null)
                        {
                            if ((((FpSpread)l_Control).Tag).ToString().Trim() == "Header No Change")
                            {
                                return true;
                            }
                            else if ((((FpSpread)l_Control).Tag).ToString().Trim() == "Change Cell")
                            {
                                for (j = 0; j <= ((FpSpread)l_Control).Sheets[0].RowCount - 1; j++)
                                {
                                    for (i = 0; i <= ((FpSpread)l_Control).Sheets[0].ColumnCount - 1; i++)
                                    {
                                        if (((FpSpread)l_Control).Sheets[0].Cells[j, i].Value != null)
                                        {
                                            ((FpSpread)l_Control).Sheets[0].Cells[j, i].Value = FindLanguage(((FpSpread)l_Control).Sheets[0].Cells[j, i].Value.ToString(), 0);
                                        }
                                    }
                                }
                            }
                        }
                        else
                        {
                            for (j = 0; j <= ((FpSpread)l_Control).Sheets[0].ColumnHeader.RowCount - 1; j++)
                            {
                                for (i = 0; i <= ((FpSpread)l_Control).Sheets[0].ColumnHeader.Columns.Count - 1; i++)
                                {
                                    if (((FpSpread)l_Control).Sheets[0].ColumnHeader.Cells[j, i].Value != null)
                                    {
                                        ((FpSpread)l_Control).Sheets[0].ColumnHeader.Cells[j, i].Value = FindLanguage(((FpSpread)l_Control).Sheets[0].ColumnHeader.Cells[j, i].Value.ToString(), 0);
                                    }
                                }
                            }
                            for (j = 0; j <= ((FpSpread)l_Control).Sheets[0].RowHeader.Rows.Count - 1; j++)
                            {
                                for (i = 0; i <= ((FpSpread)l_Control).Sheets[0].RowHeader.ColumnCount - 1; i++)
                                {
                                    if (((FpSpread)l_Control).Sheets[0].RowHeader.Cells[j, i].Value != null)
                                    {
                                        ((FpSpread)l_Control).Sheets[0].RowHeader.Cells[j, i].Value = FindLanguage(((FpSpread)l_Control).Sheets[0].RowHeader.Cells[j, i].Value.ToString(), 0);
                                    }
                                }
                            }
                        }
                    }
                    // 2020-01-28-김미경 : splitcontainer 추가
                    else if (l_Control is SplitContainer)
                    {
                        ConvertMessage(l_Control.Controls);
                    }
                    // 2020-02-07-김미경 : commbobox item 번역 추가
                    else if (l_Control is ComboBox)
                    {
                        ((ComboBox)l_Control).Text = FindLanguage(((ComboBox)l_Control).Text, 0);
                        for (i = 0; i < ((ComboBox)l_Control).Items.Count; i++)
                        {
                            if (!((ComboBox)l_Control).Items[i].ToString().Contains("System"))
                            {
                                 ((ComboBox)l_Control).Items[i] = FindLanguage(((ComboBox)l_Control).Items[i].ToString(), 0);
                            }
                        }
                    }
                }

				return true;

			}
			catch (Exception ex)
			{
                CmnFunction.ShowMsgBox("modLanguageFunction.ConvertMessage()" + "\r\n" + ex.Message, "Smart Web", MessageBoxButtons.OK, 1);
				return false;
			}

		}

		//
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
			ArrayList langArray = new ArrayList();
			CaptionBinarySearch langCompare;
			string stxt = string.Empty;
			string sRight = string.Empty;
			string sLeft = string.Empty;
			int i;

			try
			{
				flag = false;
				if (S.Trim() == "")
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

				//*** ?먮옒 ?뚯뒪 ***
				langCompare = new CaptionBinarySearch();
				i = langArray.BinarySearch(stxt.ToUpper(), langCompare);


				if (i < 0)
				{
					if (S.Trim().Length > 1)
					{
						Debug.WriteLine(iType.ToString() + "   <Text Key=\"" + S + "\"><L>" + S + "</L><L>" + S + "</L><L>" + S + "</L></Text>");
					}

					return S;
				}

				lang = (GlobalVariable.gLanguageData)langArray[i];
                // 2020-01-31-김미경 : 다국어 변환
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

                    case '4':
                        S = sLeft + lang.Lang_4;
                        break;

                    case '5':
                        S = sLeft + lang.Lang_5;
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
                CmnFunction.ShowMsgBox(ex.Message, "Smart Web", MessageBoxButtons.OK, 1);
				return S;
			}

		}


		//
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
					returnValue = "No Error Message";
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
                CmnFunction.ShowMsgBox(ex.Message, "Smart Web", MessageBoxButtons.OK, 1);
				return "";

			}
		}

		//
		// LoadMessageResource()
		//       -  Load message resource data
		// Return Value
		//       -  True / False
		// Arguments
		//       -
		//       -
		//
		public static bool LoadMessageResource()
		{

			System.IO.FileStream fs = null;
			System.Xml.XmlTextReader xtr = null;

			string[] sTemp = new string[5];
			int i;
			int j;
			GlobalVariable.gsMessageData = new string[GlobalVariable.giMaxMessageDataOption + 1, GlobalVariable.giMaxMessageData + 1];
			try
			{

				// Open XML File
				if (File.Exists(Application.StartupPath + "\\" + GlobalConstant.MP_MESSAGE_FILE) == false)
				//if (FileSystem.Dir(Application.StartupPath + "\\" + modGlobalConstant.MP_MESSAGE_FILE, 0) == "")
				{
					return true;
				}

				fs = new System.IO.FileStream(Application.StartupPath + "\\" + GlobalConstant.MP_MESSAGE_FILE, System.IO.FileMode.Open, System.IO.FileAccess.Read, System.IO.FileShare.ReadWrite);

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

									i = 0;
									xtr.MoveToAttribute("Key");
									j = System.Convert.ToInt32(xtr.Value);
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

                                    // 2020-01-28-김미경 : message 를 다국어 버전으로 만들기 위하여 배열을 늘림
									GlobalVariable.gsMessageData[0, j] = sTemp[0];
									GlobalVariable.gsMessageData[1, j] = sTemp[1];
									GlobalVariable.gsMessageData[2, j] = sTemp[2];
                                    GlobalVariable.gsMessageData[3, j] = sTemp[3];
                                    GlobalVariable.gsMessageData[4, j] = sTemp[4];
									break;

								//                  j = j + 1
							}
							break;

					}
				}

			}
			catch (Exception)
			{
				if (!(xtr == null))
				{
                    CmnFunction.ShowMsgBox("Occured error in read language file." + "\r\n" + "\r\n" + "\'" + xtr.Name + xtr.Value + "\'", "Smart Web", MessageBoxButtons.OK, 1);
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

		//
		// LoadCaptionResource()
		//       -  Load Text Data
		// Return Value
		//       -  True / False
		// Arguments
		//       -
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
			string[] sTemp = new string[5];
			int i;
			CaptionLangSort langSort;
			string sCaptionFile = "MESCaption.xml";

			try
			{
				GlobalVariable.gaButtonLanguage.Clear();
				GlobalVariable.gaMenuLanguage.Clear();
				GlobalVariable.gaTextLanguage.Clear();

				// Open XML File
				if (File.Exists(Application.StartupPath + "\\" + sCaptionFile) == false)
				//if (FileSystem.Dir(Application.StartupPath + "\\" + sCaptionFile, 0) == "")
				{
					return true;
				}

				fs = new System.IO.FileStream(Application.StartupPath + "\\" + sCaptionFile, System.IO.FileMode.Open, System.IO.FileAccess.Read, System.IO.FileShare.ReadWrite);

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
									lang.Key = xtr.Value.Trim().ToUpper();

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

                                    // 2020-01-28-김미경 : message 를 다국어 버전으로 만들기 위하여 배열을 늘림
									lang.Lang_1 = sTemp[0];
									lang.Lang_2 = sTemp[1];
									lang.Lang_3 = sTemp[2];
                                    lang.Lang_4 = sTemp[3];
                                    lang.Lang_5 = sTemp[4];

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
					CmnFunction.ShowMsgBox(GetMessage(8) + "\r\n" + "\r\n" + "\'" + xtr.Name + xtr.Value + "\'", "XML Read Error", MessageBoxButtons.OK, System.Convert.ToInt32(MessageBoxIcon.Warning));
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

		public static bool LoadCaptionResource(string sCaptionFile)
		{

			const int MENU_TYPE = 0;
			const int BUTTON_TYPE = 1;
			const int OTHER_TYPE = 2;

			System.IO.FileStream fs = null;
			System.Xml.XmlTextReader xtr = null;

			GlobalVariable.gLanguageData lang;
			int iCapType = 0;
			string[] sTemp = new string[5];
			int i;
			CaptionLangSort langSort;

			try
			{
				GlobalVariable.gaButtonLanguage.Clear();
				GlobalVariable.gaMenuLanguage.Clear();
				GlobalVariable.gaTextLanguage.Clear();

				// Open XML File
				if (File.Exists(sCaptionFile) == false)
				{
					return true;
				}

				fs = new System.IO.FileStream(sCaptionFile, System.IO.FileMode.Open, System.IO.FileAccess.Read, System.IO.FileShare.ReadWrite);

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
									lang.Key = xtr.Value.Trim().ToUpper();

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

                                    // 2020-01-28-김미경 : message 를 다국어 버전으로 만들기 위하여 배열을 늘림
									lang.Lang_1 = sTemp[0];
									lang.Lang_2 = sTemp[1];
									lang.Lang_3 = sTemp[2];
                                    lang.Lang_4 = sTemp[3];
                                    lang.Lang_5 = sTemp[4];

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
					CmnFunction.ShowMsgBox(GetMessage(8) + "\r\n" + "\r\n" + "\'" + xtr.Name + xtr.Value + "\'", "XML Read Error", MessageBoxButtons.OK, System.Convert.ToInt32(MessageBoxIcon.Warning));
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
	}
}
