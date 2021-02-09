using System.Drawing;
using System.Data;
using System.Windows.Forms;
using System.Collections;
using System.Diagnostics;
using System;


namespace Miracom.UI
{
	public delegate void MCValidate_EventHandler(object sender, MCValidate_EventArgs e);
	public delegate void MCCodeViewSelChangedHandler(object sender, MCCodeViewSelChanged_EventArgs e);
    public delegate void MCCodeViewButtonPressedHandler(object sender, MCCodeViewButtonPressed_EventArgs e);
	public delegate void MCMessage_EventHandler(object sender, ref Message m);
	public delegate void ButtonPressedEventHandler(object sender, System.EventArgs e);
	public delegate void MCSSCodeViewSelChangedHandler(object sender, MCSSCodeViewSelChanged_EventArgs e);
	
	public interface IMCValidate
	{

        bool Validate();
		
	}
	
	public class MCValidate_EventArgs
	{
		
		
		private string m_ControlName = "";
		private object m_Value = null;
		private bool m_IsValid = true;
		private bool m_AllowMoveFocus = true;
		private bool m_Flash = true;
		
		public MCValidate_EventArgs()
		{
			
		}
		
		public MCValidate_EventArgs(string controlName, object controlValue)
		{
			
			m_ControlName = controlName;
			m_Value = controlValue;
			
		}
		
		#region "Property Implements"
		
		public string ControlName
		{
			get
			{
				return m_ControlName;
			}
		}
		
		public object Value
		{
			get
			{
				return m_Value;
			}
			set
			{
                m_Value = value;
			}
		}
		
		public bool IsValid
		{
			get
			{
				return m_IsValid;
			}
			set
			{
                m_IsValid = value;
			}
		}
		
		public bool AllowMoveFocus
		{
			get
			{
				return m_AllowMoveFocus;
			}
			set
			{
                m_AllowMoveFocus = value;
				
			}
		}
		
		public bool FlashControl
		{
			get
			{
				return m_Flash;
			}
			set
			{
                m_Flash = value;
				
			}
		}
		
		#endregion
		
	}
	
	public class MCCodeViewSelChanged_EventArgs
	{
		
		
		private ListViewItem m_SelectedItem;
		private int m_SeletedSubItemIndex;
		private int m_DisplaySubItemIndex;
		
		public MCCodeViewSelChanged_EventArgs(ListViewItem selectedItem, int selectedSubItemIndex, int displaySubItemIndex)
		{
			
			m_SelectedItem = selectedItem;
			m_SeletedSubItemIndex = selectedSubItemIndex;
			m_DisplaySubItemIndex = displaySubItemIndex;
			
		}
		
		public string Text
		{
			get
			{
				string strText = "";
				int i;
				
				if (m_SeletedSubItemIndex == - 1)
				{
					for (i = 0; i <= m_SelectedItem.SubItems.Count - 1; i++)
					{
						strText += m_SelectedItem.SubItems[i].Text;
						if (i == 0 && strText.Trim(null) == "")
						{
							strText = "";
							break;
						}
						if (i < m_SelectedItem.SubItems.Count - 1)
						{
							strText += " : ";
						}
					}
				}
				else
				{
					if (m_SeletedSubItemIndex < m_SelectedItem.SubItems.Count)
					{
						strText = m_SelectedItem.SubItems[m_SeletedSubItemIndex].Text;
					}
				}
				return strText;
			}
		}
		
		public string DisplayText
		{
			get
			{
				string strText = "";
				int i;
				
				if (m_DisplaySubItemIndex == - 1)
				{
					for (i = 0; i <= m_SelectedItem.SubItems.Count - 1; i++)
					{
						strText += m_SelectedItem.SubItems[i].Text;
						if (i == 0 && strText.Trim(null) == "")
						{
							strText = "";
							break;
						}
						if (i < m_SelectedItem.SubItems.Count - 1)
						{
							strText += " : ";
						}
					}
				}
				else
				{
					if (m_DisplaySubItemIndex < m_SelectedItem.SubItems.Count)
					{
						strText = m_SelectedItem.SubItems[m_DisplaySubItemIndex].Text;
					}
				}
				return strText;
			}
		}

        public string SelectedValueToQueryString
        {
            get
			{
				string strText = "";
				int i;
				
				if (m_SeletedSubItemIndex == - 1)
				{
					for (i = 0; i <= m_SelectedItem.SubItems.Count - 1; i++)
					{
						strText += m_SelectedItem.SubItems[i].Text;
						if (i == 0 && strText.Trim(null) == "")
						{
							strText = "";
							break;
						}
						if (i < m_SelectedItem.SubItems.Count - 1)
						{
							strText += " : ";
						}
					}
				}
				else
				{
					if (m_SeletedSubItemIndex < m_SelectedItem.SubItems.Count)
					{
						strText = " = '" + m_SelectedItem.SubItems[m_SeletedSubItemIndex].Text + "' ";
					}
				}
				return strText;
			}
        }


        public string SelectedDescToQueryString
        {
            get
            {
                string strText = "";
                int i;

                if (m_SeletedSubItemIndex == -1)
                {
                    for (i = 0; i <= m_SelectedItem.SubItems.Count - 1; i++)
                    {
                        strText += m_SelectedItem.SubItems[1].Text;
                        if (i == 0 && strText.Trim(null) == "")
                        {
                            strText = "";
                            break;
                        }
                        if (i < m_SelectedItem.SubItems.Count - 1)
                        {
                            strText += " : ";
                        }
                    }
                }
                else
                {
                    if (m_SeletedSubItemIndex < m_SelectedItem.SubItems.Count)
                    {
                        if (m_SelectedItem.SubItems.Count > 1)
                        {
                            strText = " = '" + m_SelectedItem.SubItems[1].Text + "' ";
                        }
                        //else
                        //{
                        //    strText = " = ' ' ";
                        //}
                    }
                }
                return strText;
            }
        }

		public ListViewItem SelectedItem
		{
			get
			{
				if (!(m_SelectedItem == null))
				{
					return m_SelectedItem;
				}
				else
				{
					return null;
				}
			}
		}
		
		public int SelectedSubItemIndex
		{
			get
			{
				return m_SeletedSubItemIndex;
			}
			set
			{
                m_SeletedSubItemIndex = value;
			}
		}
		
		public int DisplaySubItemIndex
		{
			get
			{
				return m_DisplaySubItemIndex;
			}
			set
			{
                m_DisplaySubItemIndex = value;
			}
		}
		
	}

    public class MCCodeViewButtonPressed_EventArgs
    {


        private ListViewItem[] m_SelectedItem;
        private int m_SeletedSubItemIndex;
        private int m_DisplaySubItemIndex;
        private bool m_SelectAll;

        public MCCodeViewButtonPressed_EventArgs(ListViewItem[] selectedItem, int selectedSubItemIndex, int displaySubItemIndex, bool bSelectAll)
        {
            m_SelectedItem = selectedItem;
            m_SeletedSubItemIndex = selectedSubItemIndex;
            m_DisplaySubItemIndex = displaySubItemIndex;
            m_SelectAll = bSelectAll;
        }

        public string Text
        {
            get
            {
                string strText = "";
                int i;
                int iIndex;

                if (m_SelectAll == false)
                {
                    for (iIndex = 0; iIndex < m_SelectedItem.Length; iIndex++)
                    {
                        if (m_SeletedSubItemIndex == -1)
                        {
                            for (i = 0; i <= m_SelectedItem[iIndex].SubItems.Count; i++)
                            {
                                strText += m_SelectedItem[iIndex].SubItems[i].Text;
                                if (i == 0 && strText.Trim(null) == "")
                                {
                                    strText = "";
                                    break;
                                }
                                if (i < m_SelectedItem[iIndex].SubItems.Count)
                                {
                                    strText += " : ";
                                }
                            }
                        }
                        else
                        {
                            if (m_SeletedSubItemIndex < m_SelectedItem[iIndex].SubItems.Count)
                            {
                                strText += m_SelectedItem[iIndex].SubItems[m_SeletedSubItemIndex].Text;
                            }
                        }
                    }
                }
                else
                {
                    strText = "ALL";
                }
                
                return strText;
            }
        }

        public string DisplayText
        {
            get
            {
                string strText = "";
                int i;
                int iIndex;

                if (m_SelectAll == false)
                {
                    for (iIndex = 0; iIndex < m_SelectedItem.Length; iIndex++)
                    {
                        if (m_DisplaySubItemIndex == -1)
                        {
                            for (i = 0; i <= m_SelectedItem[iIndex].SubItems.Count; i++)
                            {
                                strText += m_SelectedItem[iIndex].SubItems[i].Text;
                                if (i == 0 && strText.Trim(null) == "")
                                {
                                    strText = "";
                                    break;
                                }
                                if (i < m_SelectedItem[iIndex].SubItems.Count - 1)
                                {
                                    strText += " : ";
                                }
                            }
                        }
                        else
                        {
                            if (m_DisplaySubItemIndex < m_SelectedItem[iIndex].SubItems.Count)
                            {
                                if (iIndex == 0)
                                {
                                    strText = m_SelectedItem[iIndex].SubItems[m_DisplaySubItemIndex].Text;
                                }
                                else
                                {
                                    strText = strText + ", " + m_SelectedItem[iIndex].SubItems[m_DisplaySubItemIndex].Text;
                                }
                            }
                        }
                    }
                }
                else
                {
                    strText = "ALL";
                }

                return strText;
            }
        }

        public string SelectedValueToQueryString
        {
            get
            {
                string strText = "";
                int i;
                int iIndex;

                if (m_SelectAll == false)
                {
                    for (iIndex = 0; iIndex < m_SelectedItem.Length; iIndex++)
                    {
                        if (m_DisplaySubItemIndex == -1)
                        {
                            for (i = 0; i <= m_SelectedItem[iIndex].SubItems.Count; i++)
                            {
                                strText += m_SelectedItem[iIndex].SubItems[i].Text;
                                if (i == 0 && strText.Trim(null) == "")
                                {
                                    strText = "";
                                    break;
                                }
                                if (i < m_SelectedItem[iIndex].SubItems.Count - 1)
                                {
                                    strText += " : ";
                                }
                            }
                        }
                        else
                        {
                            if (m_DisplaySubItemIndex < m_SelectedItem[iIndex].SubItems.Count)
                            {
                                if (iIndex == 0)
                                {
                                    strText = "IN ('" + m_SelectedItem[iIndex].SubItems[m_DisplaySubItemIndex].Text + "'";
                                }
                                else
                                {
                                    strText = strText + ", '" + m_SelectedItem[iIndex].SubItems[m_DisplaySubItemIndex].Text + "'";
                                }

                                if (iIndex == m_SelectedItem.Length - 1)
                                {
                                    strText = strText + ")";
                                }
                            }
                        }
                    }
                }
                else
                {
                    strText = "LIKE '%'";
                }

                return strText;
            }
        }

        public string SelectedDescToQueryString
        {
            get
            {
                string strText = "";
                int i;
                int iIndex;

                if (m_SelectAll == false)
                {
                    for (iIndex = 0; iIndex < m_SelectedItem.Length; iIndex++)
                    {
                        if (m_DisplaySubItemIndex == -1)
                        {
                            for (i = 0; i <= m_SelectedItem[iIndex].SubItems.Count; i++)
                            {
                                if(m_SelectedItem[iIndex].SubItems.Count > 1)
                                    strText += m_SelectedItem[iIndex].SubItems[1].Text;
                                else
                                    strText += m_SelectedItem[iIndex].SubItems[0].Text;
                                if (i == 0 && strText.Trim(null) == "")
                                {
                                    strText = "";
                                    break;
                                }
                                if (i < m_SelectedItem[iIndex].SubItems.Count - 1)
                                {
                                    strText += " : ";
                                }
                            }
                        }
                        else
                        {
                            if (m_DisplaySubItemIndex < m_SelectedItem[iIndex].SubItems.Count)
                            {
                                if (iIndex == 0)
                                {
                                    if(m_SelectedItem[iIndex].SubItems.Count > 1)
                                        strText = "IN ('" + m_SelectedItem[iIndex].SubItems[1].Text + "'";
                                    else
                                        strText = "IN ('" + m_SelectedItem[iIndex].SubItems[0].Text + "'";
                                }
                                else
                                {
                                    if (m_SelectedItem[iIndex].SubItems.Count > 1)
                                        strText = strText + ", '" + m_SelectedItem[iIndex].SubItems[1].Text + "'";
                                    else
                                        strText = strText + ", '" + m_SelectedItem[iIndex].SubItems[0].Text + "'";
                                }

                                if (iIndex == m_SelectedItem.Length - 1)
                                {
                                    strText = strText + ")";
                                }
                            }
                        }
                    }
                }
                else
                {
                    strText = "LIKE '%'";
                }

                return strText;
            }
        }

        public ListViewItem[] SelectedItem
        {
            get
            {
                if (!(m_SelectedItem == null))
                {
                    return m_SelectedItem;
                }
                else
                {
                    return null;
                }
            }
        }

        public int SelectedSubItemIndex
        {
            get
            {
                return m_SeletedSubItemIndex;
            }
            set
            {
                m_SeletedSubItemIndex = value;
            }
        }

        public int DisplaySubItemIndex
        {
            get
            {
                return m_DisplaySubItemIndex;
            }
            set
            {
                m_DisplaySubItemIndex = value;
            }
        }

        public bool SelectAll
        {
            get
            {
                return m_SelectAll;
            }
            set
            {
                m_SelectAll = value;
            }
        }

    }

	public class MCSSCodeViewSelChanged_EventArgs
	{
		
		
		private int m_iRow;
		private int m_iCol;
		private ListViewItem m_SelectedItem;
		
		public MCSSCodeViewSelChanged_EventArgs(ListViewItem selectedItem)
		{
			
			m_SelectedItem = selectedItem;
			
		}
		
		public ListViewItem SelectedItem
		{
			get
			{
				if (!(m_SelectedItem == null))
				{
					return m_SelectedItem;
				}
				else
				{
					return null;
				}
			}
		}
		
		public int Row
		{
			get
			{
				return m_iRow;
			}
			set
			{
                m_iRow = value;
			}
		}
		
		public int Col
		{
			get
			{
				return m_iCol;
			}
			set
			{
                m_iCol = value;
			}
		}
		
	}
	
	
	
}
