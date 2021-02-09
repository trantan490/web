using System;
using System.Data;
using System.IO;
using System.Windows.Forms;
using System.Configuration;
using Miracom.SmartWeb.FWX;
using Miracom.UI.Controls.MCCodeView;
using System.Drawing;
using System.Globalization;
using System.Diagnostics;
using System.Text;

namespace Miracom.SmartWeb.UI
{ 
    public sealed class CmnFunction
    {
        public static ROWebService oComm = new ROWebService();

        public CmnFunction()
        {
        } 
        /// <summary>
        /// Get Database DateTime.
        /// </summary>
        /// <returns>Boolean : Database DateTime</returns>
        public static string GetSysDateTime()
        {
            try
            {
                string QueryCond = null;

                oComm.SetUrl();
				
				return oComm.GetFuncDataString("GetSysTime", QueryCond);

            }
            catch (Exception ex)
            {
                CmnFunction.ShowMsgBox(ex.ToString());
                return null;
            }
        }

        public static string GetErrorMessage(string ErrorMessage)
        {
            try
            {
                GlobalVariable.gsResult = ErrorMessage.Substring(0, 1);
                GlobalVariable.gsErrorCode = ErrorMessage.Substring(1, 10);
                GlobalVariable.gsErrorMessage = ErrorMessage.Substring(11, 200);

                return ErrorMessage.Substring(211);

            }
            catch (Exception ex)
            {
                CmnFunction.ShowMsgBox(ex.ToString());
                return null;
            }
        }



        /// <summary>
        /// 지정된 Spread의 전체 Columns Width를 Columns에 표시된 값의 최소길이로 조정한다.
        /// </summary>
        /// <param name="spdCtl">Spread: object </param>
        public static void SpreadColumnsWidthAutoFit(FarPoint.Win.Spread.FpSpread spdCtl)
        {
            float colWidth;

            spdCtl.SuspendLayout();
            for (int i = 0; i < spdCtl.Sheets.Count; i++)
            {
                for (int j = 0; j < spdCtl.Sheets[i].ColumnCount; j++)
                {
                    colWidth = spdCtl.Sheets[i].Columns[j].GetPreferredWidth();
                    spdCtl.Sheets[i].Columns[j].Width = colWidth;
                }
            }
            spdCtl.ResumeLayout(false);
        }

        // CheckMaxLength()
        //       - check byte text length in TextBox
        // Return Value
        //       - boolean : True / False
        // Arguments
        //       - ByVal txt As Object			: TextBox Control
        //		- ByVal iMaxLength As Integer	: Max byte length
        public static bool CheckMaxLength(object txt, int iMaxLength)
        {
            int iByteLen;
            string sText = "";

            if (iMaxLength <= 0)
            {
                if (txt is System.Windows.Forms.TextBox)
                {
                    iMaxLength = ((System.Windows.Forms.TextBox)txt).MaxLength;
                    sText = ((System.Windows.Forms.TextBox)txt).Text;
                }
                else if (txt is MCCodeView)
                {
                    iMaxLength = ((MCCodeView)txt).MaxLength;
                    sText = ((MCCodeView)txt).Text;
                }
            }

            if (iMaxLength == 0)
            {
                return false;
            }

            iByteLen = CmnFunction.ByteLen(sText);
            if (iByteLen > iMaxLength)
            {
                if (txt is System.Windows.Forms.TextBox)
                {
                    ((System.Windows.Forms.TextBox)txt).Text = CmnFunction.ByteMid(sText, 0, iMaxLength);
                    ((System.Windows.Forms.TextBox)txt).SelectionStart = sText.Length;
                }
                else if (txt is MCCodeView)
                {
                    ((MCCodeView)txt).Text = CmnFunction.ByteMid(sText, 0, iMaxLength);
                    ((MCCodeView)txt).SelectionStart = sText.Length;
                }

                return false;
            }

            return true;
        }

        //********************************************************************
        //*
        //*  Function Name   :   FieldClear
        //*
        //*  File Name       :   MPlusCOMN00.bas
        //*
        //*  Program ID      :   MES
        //*
        //'*  Description     :   특정 form안의 내용을 지우고 초기화 한다.
        //*
        //*  Input Parameter :   f as Form
        //'*                      ExceptCtl1 as Control -초기화에서 제외되는 컨트롤
        //'*                      ExceptCtl2 as Control -초기화에서 제외되는 컨트롤
        //'*                      ExceptCtl3 as Control -초기화에서 제외되는 컨트롤
        //'*                      ExceptCtl4 as Control -초기화에서 제외되는 컨트롤
        //'*                      ExceptCtl5 as Control -초기화에서 제외되는 컨트롤
        //*
        //*  Output Value    :   None
        //*
        //*  Special Logic Notes :
        //*
        //*  Modification History    :
        //*
        //*  VERSION     DATE            AUTHOR      DESCRIPTION
        //*  ---------   -------------   ----------  ---------------
        //*  V1.0.0      June 30, 2001   SK Jin      Create
        //*  V1.0.1      Oct 12, 2002    CM Koo      Modify
        //*
        //*******************************************************************
        public static bool FieldClear(object ff, object ExceptCtl1, object ExceptCtl2, object ExceptCtl3, object ExceptCtl4, object ExceptCtl5, bool bItemsClear)
        {
            bool returnValue;

            //On Error Resume Next  - Cannot Convert to C#

            object l_Control;
            int i;
            bool bExceptFlag;

            returnValue = false;

            l_Control = null;

            //            System.Windows.Forms.Control.ControlCollection f = (System.Windows.Forms.Control.ControlCollection)ff;
            System.Windows.Forms.Control f = (System.Windows.Forms.Control)ff;

            for (i = 0; i <= f.Controls.Count - 1; i++)
            {
                bExceptFlag = false;

                l_Control = f.Controls[i];

                if (l_Control is Panel || l_Control is System.Windows.Forms.GroupBox || l_Control is TabControl || l_Control is TabPage)
                {
                    FieldClear(l_Control, ExceptCtl1, ExceptCtl2, ExceptCtl3, ExceptCtl4, ExceptCtl5, false);
                }
                else
                {
                    if (ExceptCtl1 != null)
                    {
                        if (ExceptCtl1 == l_Control)
                        {
                            bExceptFlag = true;
                        }
                        else
                        {
                            bExceptFlag = false;
                        }
                    }
                    if (bExceptFlag == false && ExceptCtl2 != null)
                    {
                        if (ExceptCtl2 == l_Control)
                        {
                            bExceptFlag = true;
                        }
                        else
                        {
                            bExceptFlag = false;
                        }
                    }
                    if (bExceptFlag == false && ExceptCtl3 != null)
                    {
                        if (ExceptCtl3 == l_Control)
                        {
                            bExceptFlag = true;
                        }
                        else
                        {
                            bExceptFlag = false;
                        }
                    }

                    if (bExceptFlag == false && ExceptCtl4 != null)
                    {
                        if (ExceptCtl4 == l_Control)
                        {
                            bExceptFlag = true;
                        }
                        else
                        {
                            bExceptFlag = false;
                        }
                    }

                    if (bExceptFlag == false && ExceptCtl5 != null)
                    {
                        if (ExceptCtl5 == l_Control)
                        {
                            bExceptFlag = true;
                        }
                        else
                        {
                            bExceptFlag = false;
                        }
                    }

                    if (bExceptFlag == false)
                    {
                        if (l_Control is System.Windows.Forms.TextBox)
                        {
                            ((System.Windows.Forms.TextBox)l_Control).Text = "";
                        }
                        else if (l_Control is System.Windows.Forms.CheckBox)
                        {
                            ((System.Windows.Forms.CheckBox)l_Control).Checked = false;
                        }
                        else if (l_Control is ComboBox)
                        {
                            ((ComboBox)l_Control).SelectedIndex = -1;
                            if (bItemsClear == true)
                            {
                                ((ComboBox)l_Control).Items.Clear();
                            }
                        }
                        else if (l_Control is RadioButton)
                        {
                            ((RadioButton)l_Control).Checked = false;
                        }
                        else if (l_Control is MCCodeView)
                        {
                            ((MCCodeView)l_Control).Text = "";
                            if (bItemsClear == true)
                            {
                                ((MCCodeView)l_Control).Items.Clear();
                            }
                        }
                        else if (l_Control is NumericUpDown)
                        {
                            ((NumericUpDown)l_Control).Value = 0;
                        }

                    }
                }
            }

            returnValue = true;

            return returnValue;
        }

        /// <summary>
        /// 지정된 Object를 임의의 파일에 이미지로 저장하고, 저장되 이미지 파일명를 리턴한다.
        /// </summary>
        /// <param name="ctlObject">Save to file Object</param>
        /// <returns>String: Temp file name or NULL</returns>
        public static string SaveObjectToFile(System.Windows.Forms.Control ctlObject)
        {
            string tempSaveFileName = null;

            try
            {
                if (ctlObject is Infragistics.Win.UltraWinChart.UltraChart)
                {
                    Infragistics.Win.UltraWinChart.UltraChart TempChart;
                    TempChart = (Infragistics.Win.UltraWinChart.UltraChart)ctlObject;
                    tempSaveFileName = Path.GetTempFileName();
                    TempChart.Image.Save(tempSaveFileName, System.Drawing.Imaging.ImageFormat.Jpeg);
                }

                return tempSaveFileName;
            }
            catch
            {
                return null;
            }
        }

        // ShowMsgBox()
        //       - Show Message box by Modal
        // Return Value
        //       -
        // Arguments
        //       - ByVal sStr As String : Message string to show
        //       - Optional ByVal Buttons As MessageBoxButtons : Message box button type
        //       - Optional ByVal DefaultFocus As Integer : Default focused button
        //
        public static System.Windows.Forms.DialogResult ShowMsgBox(string sStr)
        {

            try
            {
                frmMessageBox frmMsg = new frmMessageBox();
                System.Windows.Forms.DialogResult retValue = new DialogResult();

                if (GlobalVariable.gbShowMsgFlag == false)
                {
                    if (sStr == LanguageFunction.GetMessage(52) && sStr != "No Error Message")
                    {
                        //SetStatusMsg(sStr);
                        retValue = DialogResult.None;
                        return retValue;
                    }
                }

                frmMsg.Owner = GlobalVariable.gfrmMDI;
                retValue = frmMsg.Show(sStr, "Smart Web", 0, 1);

                frmMsg.Owner = null;
                frmMsg = null;

                return retValue;

            }
            catch (Exception ex)
            {
                ShowMsgBox("CommonFunction.ShowMsgBox()" + "\r\n" + ex.Message, "Smart Web", MessageBoxButtons.OK, 1);
                return DialogResult.None;
            }

        }

        public static System.Windows.Forms.DialogResult ShowMsgBox(string sStr, string Caption, MessageBoxButtons Buttons, int DefaultFocus)
        {
            try
            {
                frmMessageBox frmMsg = new frmMessageBox();
                System.Windows.Forms.DialogResult retValue = new DialogResult();

                if (GlobalVariable.gbShowMsgFlag == false)
                {
                    if (sStr == LanguageFunction.GetMessage(52) && sStr != "No Error Message")
                    {
                        //SetStatusMsg(sStr);
                        retValue = DialogResult.None;
                        return retValue;
                    }
                }

                frmMsg.Owner = GlobalVariable.gfrmMDI;
                retValue = frmMsg.Show(sStr, Caption, Buttons, DefaultFocus);

                frmMsg.Owner = null;
                frmMsg = null;

                return retValue;

            }
            catch (Exception ex)
            {
                ShowMsgBox("CommonFunction.ShowMsgBox()" + "\r\n" + ex.Message, "Smart Web", MessageBoxButtons.OK, 1);
                return DialogResult.None;
            }

        }

        //public static System.Windows.Forms.DialogResult ShowMsgBox(string sStr, ref Form owner, string Caption, MessageBoxButtons Buttons, int DefaultFocus)
        //{

        //    try
        //    {
        //        frmMessageBox frmMsg = new frmMessageBox();
        //        System.Windows.Forms.DialogResult retValue = new DialogResult();

        //        if (GlobalVariable.gbShowMsgFlag == false)
        //        {
        //            if (sStr == LanguageFunction.GetMessage(52) && sStr != "No Error Message")
        //            {
        //                //SetStatusMsg(sStr);
        //                retValue = DialogResult.None;
        //                return retValue;
        //            }
        //        }

        //        frmMsg.Owner = owner;
        //        retValue = frmMsg.Show(sStr, Caption, Buttons, DefaultFocus, ref owner);
        //        frmMsg.Owner = null;
        //        frmMsg = null;

        //        return retValue;

        //    }
        //    catch (Exception ex)
        //    {
        //        ShowMsgBox("CommonFunction.ShowMsgBox()" + "\r\n" + ex.Message, "Smart Web", MessageBoxButtons.OK, 1);
        //        return DialogResult.None;
        //    }

        //}

        public static System.Windows.Forms.DialogResult ShowMsgBox(ReturnMessageString MsgString)
        {
            return ShowMsgBox(MsgString, Application.ProductName, MessageBoxButtons.OK, 1);
        }

        public static System.Windows.Forms.DialogResult ShowMsgBox(ReturnMessageString MsgString, string Caption, MessageBoxButtons Buttons, int DefaultFocus)
        {

            try
            {
                frmMessageBox frmMsg = new frmMessageBox();
                System.Windows.Forms.DialogResult retValue;

                frmMsg.Owner = GlobalVariable.gfrmMDI;
                retValue = frmMsg.Show(ref MsgString, Caption, Buttons, DefaultFocus);

                MsgString = null;
                frmMsg.Owner = null;
                frmMsg = null;

                return retValue;

            }
            catch (Exception ex)
            {
                ShowMsgBox("MPCF.ShowMsgBox()\n" + ex.Message);
                return DialogResult.None;
            }

        }
        //
        // MakeErrorMsg()
        //       - Make error message
        // Return Value
        //       - clsMsgString : Return Error Message Class
        // Arguments
        //       - error_msg as String  : Error Message
        //       - db_error_msg as String : DB Error Message
        //       - key_msg1 as String : Key Message 1
        //       - key_msg2 as String : Key Message 2
        //       - key_msg3 as String : Key Message 3
        //
        public static ReturnMessageString MakeErrorMsg(string Msg_Code, string error_msg, string db_error_msg, params string[] field_msg)
        {

            ReturnMessageString MsgString = new ReturnMessageString();

            try
            {
                int i;

                MsgString.IsServerMsgFlag = true;
                MsgString.MsgCode = CmnFunction.Trim(Msg_Code);
                MsgString.ErrorMsg = CmnFunction.Trim(error_msg);
                MsgString.DBErrorMsg = CmnFunction.Trim(db_error_msg);
                for (i = 0; i <= (field_msg.Length - 1); i++)
                {
                    if (CmnFunction.Trim(field_msg[i]) != "")
                    {
                        MsgString.FieldMsg.Add(CmnFunction.Trim(field_msg[i]));
                    }
                }

            }
            catch (Exception ex)
            {
                ShowMsgBox(ex.Message);
            }

            return MsgString;

        }

        //
        // MakeDateFormat()
        //        - 일반 문자열을 Time Format으로 변환
        // Return Value
        //       - String : Return Get Time Format
        // Arguments
        //       - ByVal Fmt As FORMAT : Format (STANDARD, SYSTEM_FORMAT)
        //       - Optional ByVal DateTimeFormat As DATE_TIME_FORMAT : Time format ("YYYYMMDDHHMMSS", "YYYYMMDD", "HHMMSS")
        //
        public static string MakeDateFormat(string S)
        {
            return MakeDateFormat(S, DATE_TIME_FORMAT.NONE);
        }
        public static string MakeDateFormat(string S, DATE_TIME_FORMAT DateTimeFormat)
        {
            CultureInfo ci = CultureInfo.CurrentCulture;
            string sReturn = "";

            try
            {

                if (string.IsNullOrEmpty(S) == true)
                    return "";

                S = CmnFunction.Trim(S);
                if (S == "")
                    return "";

                if (DateTimeFormat == DATE_TIME_FORMAT.NONE)
                {
                    DateTimeFormat = GlobalVariable.geDateTimeFormat;
                }

                //Length Validation
                if (DateTimeFormat == DATE_TIME_FORMAT.SHORTDATETIME)
                {
                    if (S.Length < 14)
                    {
                        sReturn = "";
                        return sReturn;
                    }
                    else if (S.Length > 14)
                    {
                        S = S.Substring(0, 14);
                    }
                }

                if (DateTimeFormat == DATE_TIME_FORMAT.SHORTDATE)
                {
                    // 2007.01.16. Aiden Koo
                    // 문자열의 개수가 반드시 8일 필요는 없다.
                    if (S.Length < 8)
                    {
                        sReturn = "";
                        return sReturn;
                    }
                    else if (S.Length > 8)
                    {
                        S = S.Substring(0, 8);
                    }
                }

                if (DateTimeFormat == DATE_TIME_FORMAT.SHORTTIME)
                {
                    // 2007.01.16. Aiden Koo
                    // 문자열의 개수가 반드시 6일 필요는 없다.
                    if (S.Length < 6)
                    {
                        sReturn = "";
                        return sReturn;
                    }
                    else if (S.Length > 6)
                    {
                        S = S.Substring(0, 6);
                    }
                }

                switch (GlobalVariable.geLanguageFormat)
                {
                    case LANG_FORMAT.STANDARD:

                        switch (DateTimeFormat)
                        {
                            case DATE_TIME_FORMAT.SHORTDATETIME: //DateTime (YYYY/MM/DD/ hh:mm:ss)

                                sReturn = S.Substring(0, 4) + GlobalVariable.gsDateDelimiter + S.Substring(4, 2) + GlobalVariable.gsDateDelimiter + S.Substring(6, 2) + " " + S.Substring(8, 2) + ":" + S.Substring(10, 2) + ":" + S.Substring(12, 2);
                                break;
                            case DATE_TIME_FORMAT.SHORTDATE: //Date (YYYY/MM/DD)

                                sReturn = S.Substring(0, 4) + GlobalVariable.gsDateDelimiter + S.Substring(4, 2) + GlobalVariable.gsDateDelimiter + S.Substring(6, 2);
                                break;
                            case DATE_TIME_FORMAT.SHORTTIME: //Time (hh:mm:ss)

                                sReturn = S.Substring(0, 2) + ":" + S.Substring(2, 2) + ":" + S.Substring(4, 2);
                                break;
                            default:

                                sReturn = "";
                                break;
                        }
                        break;

                    default:
                        sReturn = S;
                        break;
                }

                return sReturn;

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return "";
            }

        }

        //
        // DestroyDateFormat()
        //       - ?좎쭨 ?뺤떇??臾몄옄?댁쓣 ?쇰컲 臾몄옄?대줈 蹂?섑븳??
        // Return Value
        //       - String :
        // Arguments
        //       - ByVal S As String : ?좎쭨 ?뺤떇??臾몄옄??
        //
        public static string DestroyDateFormat(string S)
        {
            return DestroyDateFormat(S, DATE_TIME_FORMAT.NONE);
        }
        public static string DestroyDateFormat(string S, DATE_TIME_FORMAT DateTimeFormat)
        {
            string sDateTime = "";
            DateTime dt;

            try
            {
                S = CmnFunction.Trim(S);
                if (S == "")
                    return "";

                if (DateTimeFormat == DATE_TIME_FORMAT.NONE)
                {
                    DateTimeFormat = GlobalVariable.geDateTimeFormat;
                }

                if (GlobalVariable.geLanguageFormat == LANG_FORMAT.STANDARD)
                {
                    dt = Convert.ToDateTime(S);

                    switch (DateTimeFormat)
                    {
                        case DATE_TIME_FORMAT.SHORTDATETIME:
                            sDateTime = dt.ToString("yyyyMMddHHmmss");
                            break;
                        case DATE_TIME_FORMAT.SHORTDATE:
                            sDateTime = dt.ToString("yyyyMMdd");
                            break;
                        case DATE_TIME_FORMAT.SHORTTIME:
                            sDateTime = dt.ToString("HHmmss");
                            break;
                    }
                }
                else
                {
                    sDateTime = S;
                }

                return sDateTime;

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return "";
            }

        }

        //********************************************************************
        //*
        //*  Function Name   :   FindListItem
        //*
        //*  File Name       :   MPlusCOMN00.bas
        //*
        //*  Program ID      :   MES
        //*
        //*  Description     :   ?뱀젙 Item??Listview?먯꽌 李얠븘?몃떎.
        //*
        //*  Input Parameter :   MyListView as ListView
        //*                      Item as String
        //*
        //*  Output Value    :   True / False
        //*
        //*  Special Logic Notes :
        //*
        //*  Modification History    :
        //*
        //*  VERSION     DATE            AUTHOR      DESCRIPTION
        //*  ---------   -------------   ----------  ---------------
        //*  V1.0.0      June 30, 2001   SK Jin      Create
        //*
        //*******************************************************************
        public static bool FindListItem(ListView MyListView, string Item, bool bUpperCase)
        {
            int index;

            if (CmnFunction.Trim(Item) == "")
            {
                return false;
            }

            Item = CmnFunction.Trim(Item);

            for (index = 0; index <= MyListView.Items.Count - 1; index++)
            {
                if (bUpperCase == true)
                {
                    if (CmnFunction.Trim(MyListView.Items[index].Text).ToUpper() == Item.ToUpper())
                    {
                        MyListView.Items[index].Selected = true;
                        MyListView.Items[index].EnsureVisible();
                        return true;
                    }
                }
                else
                {
                    if (CmnFunction.Trim(MyListView.Items[index].Text) == Item)
                    {
                        MyListView.Items[index].Selected = true;
                        MyListView.Items[index].EnsureVisible();
                        return true;
                    }
                }
            }

            return false;

        }

        public static bool FindListItem(ListView MyListView, string Item, int iColumnIndex, bool bUpperCase)
        {
            int index;

            if (CmnFunction.Trim(Item) == "")
            {
                return false;
            }

            Item = CmnFunction.Trim(Item);

            for (index = 0; index <= MyListView.Items.Count - 1; index++)
            {
                if (bUpperCase == true)
                {
                    if (CmnFunction.Trim(MyListView.Items[index].SubItems[iColumnIndex].Text).ToUpper() == Item.ToUpper())
                    {
                        MyListView.Items[index].Selected = true;
                        MyListView.Items[index].EnsureVisible();
                        return true;
                    }
                }
                else
                {
                    if (CmnFunction.Trim(MyListView.Items[index].SubItems[iColumnIndex].Text) == Item)
                    {
                        MyListView.Items[index].Selected = true;
                        MyListView.Items[index].EnsureVisible();
                        return true;
                    }
                }
            }

            return false;
        }

        public static bool FindListItem(ListView MyListView, string Item1, int iColumnIndex1, string Item2, int iColumnIndex2, bool bUpperCase)
        {
            int index;

            if (CmnFunction.Trim(Item1) == "" || CmnFunction.Trim(Item2) == "")
            {
                return false;
            }

            Item1 = CmnFunction.Trim(Item1);
            Item2 = CmnFunction.Trim(Item2);

            for (index = 0; index <= MyListView.Items.Count - 1; index++)
            {
                if (bUpperCase == true)
                {
                    if (CmnFunction.Trim(MyListView.Items[index].SubItems[iColumnIndex1].Text).ToUpper() == Item1.ToUpper() &&
                        CmnFunction.Trim(MyListView.Items[index].SubItems[iColumnIndex2].Text).ToUpper() == Item2.ToUpper())
                    {
                        MyListView.Items[index].Selected = true;
                        MyListView.Items[index].EnsureVisible();
                        return true;
                    }
                }
                else
                {
                    if (CmnFunction.Trim(MyListView.Items[index].SubItems[iColumnIndex1].Text) == Item1 &&
                        CmnFunction.Trim(MyListView.Items[index].SubItems[iColumnIndex2].Text) == Item2)
                    {
                        MyListView.Items[index].Selected = true;
                        MyListView.Items[index].EnsureVisible();
                        return true;
                    }
                }
            }

            return false;
        }

        public static int FindListItemIndex(ListView MyListView, string Item, bool bUpperCase)
        {
            int index;

            if (CmnFunction.Trim(Item) == "")
            {
                return -1;
            }

            Item = CmnFunction.Trim(Item);

            for (index = 0; index <= MyListView.Items.Count - 1; index++)
            {
                if (bUpperCase == true)
                {
                    if (CmnFunction.Trim(MyListView.Items[index].Text).ToUpper() == Item.ToUpper())
                    {
                        return index;
                    }
                }
                else
                {
                    if (CmnFunction.Trim(MyListView.Items[index].Text) == Item)
                    {
                        return index;
                    }
                }

            }

            return -1;
        }

        public static int FindListItemIndex(ListView MyListView, string Item, int iColumnIndex, bool bUpperCase)
        {
            int index;

            if (CmnFunction.Trim(Item) == "")
            {
                return -1;
            }

            Item = CmnFunction.Trim(Item);

            for (index = 0; index <= MyListView.Items.Count - 1; index++)
            {
                if (bUpperCase == true)
                {
                    if (CmnFunction.Trim(MyListView.Items[index].SubItems[iColumnIndex].Text).ToUpper() == Item.ToUpper())
                    {
                        return index;
                    }
                }
                else
                {
                    if (CmnFunction.Trim(MyListView.Items[index].SubItems[iColumnIndex].Text) == Item)
                    {
                        return index;
                    }
                }
            }

            return -1;
        }

        public static TreeNode FindTreeNodeNextPartial(TreeNode first_node, TreeNode cur_node, string Item)
        {
            TreeNode t_node_1 = first_node;
            TreeNode t_node_2 = cur_node;
            TreeNode t_node_3;

            t_node_3 = FindTreeNodeNextPartialSub01(t_node_1, ref t_node_2, Item, false);
            if (t_node_3 == null)
            {
                t_node_2 = null;
                t_node_3 = FindTreeNodeNextPartialSub01(t_node_1, ref t_node_2, Item, false);
            }

            return t_node_3;
        }

        public static TreeNode FindTreeNodeNextPartialByTag(TreeNode first_node, TreeNode cur_node, string Item)
        {
            TreeNode t_node_1 = first_node;
            TreeNode t_node_2 = cur_node;
            TreeNode t_node_3;

            t_node_3 = FindTreeNodeNextPartialSub01(t_node_1, ref t_node_2, Item, true);
            if (t_node_3 == null)
            {
                t_node_2 = null;
                t_node_3 = FindTreeNodeNextPartialSub01(t_node_1, ref t_node_2, Item, true);
            }

            return t_node_3;
        }

        private static TreeNode FindTreeNodeNextPartialSub01(TreeNode first_node, ref TreeNode cur_node, string Item, bool bCheckTag)
        {
            TreeNode t_node_1;
            TreeNode t_node_2;
            try
            {
                t_node_1 = first_node;
                while (t_node_1 != null)
                {
                    if (cur_node == null)
                    {
                        if (bCheckTag == false)
                        {
                            if (t_node_1.Text.IndexOf(Item) >= 0) return t_node_1;
                        }
                        else
                        {
                            if (t_node_1.Tag != null)
                            {
                                if (t_node_1.Tag.ToString().IndexOf(Item) >= 0) return t_node_1;
                            }
                        }
                    }
                    else
                    {
                        if (t_node_1.Equals(cur_node) == true)
                        {
                            cur_node = null;
                        }
                    }

                    if (t_node_1.GetNodeCount(true) > 0)
                    {
                        t_node_2 = FindTreeNodeNextPartialSub01(t_node_1.Nodes[0], ref cur_node, Item, bCheckTag);
                        if (t_node_2 != null) return t_node_2;
                    }

                    t_node_1 = t_node_1.NextNode;
                }
            }
            catch (Exception ex)
            {
                ShowMsgBox(ex.Message);
            }

            return null;
        }

        /// <summary>
        ///  Control의 칼럼 헤더 사이즈를 내용에 맞게 조절한다.
        /// </summary>
        /// <param name="obj">List Control</param>
        public static void FitColumnHeader(object obj)
        {
            FitColumnHeader(obj, -1, -1, -1, -1);
        }
        public static void FitColumnHeader(object obj, int i_start_column_index, int i_end_column_index)
        {
            FitColumnHeader(obj, i_start_column_index, i_end_column_index, -1, -1);
        }
        public static void FitColumnHeader(object obj, int i_start_column_index, int i_end_column_index, int i_word_wrap_col, int i_word_wrap_max_size)
        {
            float colWidth;
            int i;
            int j;

            try
            {
                if (obj is FarPoint.Win.Spread.FpSpread)
                {
                    FarPoint.Win.Spread.SheetView s_view = ((FarPoint.Win.Spread.FpSpread)obj).ActiveSheet;

                    if (i_start_column_index < 0)
                        i_start_column_index = 0;
                    if (i_end_column_index < 0)
                        i_end_column_index = s_view.ColumnCount - 1;
                    if (i_end_column_index > s_view.ColumnCount - 1)
                        i_end_column_index = s_view.ColumnCount - 1;

                    for (j = 0; j < s_view.ColumnHeader.RowCount; j++)
                    {
                        s_view.RowCount++;

                        for (i = 0; i < s_view.ColumnCount; i++)
                        {
                            s_view.Cells[s_view.RowCount - 1, i].Value = s_view.ColumnHeader.Cells[j, i].Text;
                        }
                    }

                    for (i = i_start_column_index; i <= i_end_column_index; i++)
                    {
                        if (i_word_wrap_col > -1)
                        {
                            if (i_word_wrap_col == i)
                            {
                                s_view.Columns[i].CellType = new FarPoint.Win.Spread.CellType.GeneralCellType();
                            }
                        }

                        try
                        {
                            colWidth = s_view.GetPreferredColumnWidth(i) + 5;
                        }
                        catch (Exception)
                        {
                            colWidth = s_view.ColumnHeader.Columns[i].Width;
                        }

                        if (colWidth > GlobalConstant.SP_MAX_COLUMN_WIDTH)
                        {
                            colWidth = GlobalConstant.SP_MAX_COLUMN_WIDTH;
                        }
                        else if (colWidth < GlobalConstant.SP_MIN_COLUMN_WIDTH)
                        {
                            colWidth = GlobalConstant.SP_MIN_COLUMN_WIDTH;
                        }

                        s_view.ColumnHeader.Columns[i].Width = colWidth;

                        if (i_word_wrap_col > -1)
                        {
                            if (i_word_wrap_col == i)
                            {
                                if (i_word_wrap_max_size > -1)
                                {
                                    if (colWidth > i_word_wrap_max_size)
                                    {
                                        s_view.ColumnHeader.Columns[i].Width = i_word_wrap_max_size;
                                        FarPoint.Win.Spread.CellType.TextCellType cText = new FarPoint.Win.Spread.CellType.TextCellType();
                                        cText.WordWrap = true;
                                        s_view.Columns[i].CellType = cText;
                                    }
                                }
                            }
                        }
                    }


                    if (i_word_wrap_col > -1)
                    {
                        int rowSpanCnt = 0;
                        int rowHeight = 0;

                        for (i = 0; i < s_view.RowCount; i++)
                        {
                            rowSpanCnt = s_view.Cells[i, i_word_wrap_col].RowSpan;

                            if (rowSpanCnt > 1 || s_view.Columns[i_word_wrap_col].MergePolicy == FarPoint.Win.Spread.Model.MergePolicy.None)
                            {
                                if (s_view.Rows[i].Height * s_view.Cells[i, i_word_wrap_col].RowSpan < s_view.GetPreferredRowHeight(i))
                                {
                                    rowHeight = Convert.ToInt32(s_view.GetPreferredRowHeight(i) / s_view.Cells[i, i_word_wrap_col].RowSpan);
                                    for (j = i; j < i + s_view.Cells[i, i_word_wrap_col].RowSpan; j++)
                                    {
                                        s_view.Rows[j].Height = rowHeight;
                                    }
                                }
                                i += (s_view.Cells[i, i_word_wrap_col].RowSpan - 1);
                            }
                        }
                    }

                    s_view.RowCount -= s_view.ColumnHeader.RowCount;
                }
            }
            catch (Exception ex)
            {
                ShowMsgBox(ex.Message);
            }
        }

        public static void FieldAdjust(Control cParent, Control cLeft, Control cRight, Control cMid, int iMidSize)
        {

            int iSideSize = 0;
            int iWidth = 0;
            int iHeight = 0;
            int iLeftPad = 0;
            int iTopPad = 0;

            if (cParent is System.Windows.Forms.GroupBox)
            {
                iWidth = cParent.Width - 6;
                iHeight = cParent.Height - 20;

                iLeftPad = 3;
                iTopPad = 18;
            }
            else if (cParent is Panel)
            {
                iLeftPad = ((Panel)cParent).DockPadding.Left;
                iTopPad = ((Panel)cParent).DockPadding.Top;

                iWidth = cParent.Width - (iLeftPad + ((Panel)cParent).DockPadding.Right);
                iHeight = cParent.Height - (iTopPad + ((Panel)cParent).DockPadding.Bottom);
            }

            iSideSize = (iWidth - iMidSize) / 2;
            cLeft.Width = iSideSize;
            cRight.Width = iSideSize;

            cMid.Width = iMidSize - 2;

            if (cMid.Dock == DockStyle.None)
            {
                cMid.Left = iLeftPad + iSideSize + 1;
                cMid.Top = iTopPad + 1;
                cMid.Height = iHeight - 2;
            }
            if (cLeft.Dock == DockStyle.None)
            {
                cLeft.Height = iHeight;
                cLeft.Left = iLeftPad;
                cLeft.Top = iTopPad;
            }
            if (cRight.Dock == DockStyle.None)
            {
                cRight.Height = iHeight;
                cRight.Left = iLeftPad + iSideSize + iMidSize;
                cRight.Top = iTopPad;
            }

        }


        // ByteLen()
        //       - Get byte length in String
        // Return Value
        //       - Integer : byte length
        // Arguments
        //       - ByVal sStr As String			: String
        public static int ByteLen(string sStr)
        {

            System.Text.Encoding twobyte;

            twobyte = System.Text.Encoding.GetEncoding(949);
            return twobyte.GetByteCount(sStr);

        }

        // ByteMid()
        //       - Get Middle String at byte length in String
        // Return Value
        //       - String : Middle String
        // Arguments
        //       - ByVal sStr As String			: Source String
        //		- ByVal iStartIndex As Integer	: Start Index
        //		- ByVal iLength As Integer		: Middle String byte length
        public static string ByteMid(string sStr, int iStartIndex, int iLength)
        {

            System.Text.Encoding twobyte;
            string sTemp;
            byte[] bTemp;

            twobyte = System.Text.Encoding.GetEncoding(949);

            sTemp = "";
            bTemp = twobyte.GetBytes(sStr);
            if (bTemp.Length < iStartIndex + iLength)
            {
                iLength = bTemp.Length - iStartIndex;
            }

            sTemp = twobyte.GetString(bTemp, iStartIndex, iLength);

            return sTemp;

        }

        // StringTrim()
        //       - Trim Space Data
        // Return Value
        //       - boolean : True / False
        // Arguments
        //       - Form
        public static string Trim(object str)
        {
            try
            {
                if (str == null)
                    return "";


                if (str is System.Windows.Forms.Form)
                {
                    System.Windows.Forms.Form f = (System.Windows.Forms.Form)str;

                    MessageBox.Show(f.Name + " Trim을 사용하고 있음. 수정 바람.");

                    if (f.Tag == null)
                    {
                        return "";
                    }
                    else
                    {
                        return f.Tag.ToString().Trim();
                    }
                }
                else
                {
                    string sTemp = Convert.ToString(str);

                    if (string.IsNullOrEmpty(sTemp) == true)
                    {
                        return "";
                    }
                    else
                    {
                        return sTemp.Trim();
                    }
                }
            }
            catch (Exception Ex)
            {
                Debug.Print(Ex.Message + " Trim");
                return "";
            }
        }

        // StringTrim()
        //       - Trim Space Data
        // Return Value
        //       - boolean : True / False
        // Arguments
        //       - string
        public static string RTrim(object str)
        {
            try
            {
                if (str == null)
                    return "";

                if (str is System.Windows.Forms.Form)
                {
                    System.Windows.Forms.Form f = (System.Windows.Forms.Form)str;

                    if (f.Tag == null)
                    {
                        return "";
                    }
                    else
                    {
                        return f.Tag.ToString().TrimEnd();
                    }
                }
                else
                {
                    string sTemp = Convert.ToString(str);

                    if (string.IsNullOrEmpty(sTemp) == true)
                    {
                        return "";
                    }
                    else
                    {
                        return sTemp.TrimEnd();
                    }
                }
            }
            catch (Exception Ex)
            {
                Debug.Print(Ex.Message + " RTrim");
                return "";
            }
        }

        public static string LTrim(object str)
        {
            try
            {
                if (str == null)
                    return "";

                if (str is System.Windows.Forms.Form)
                {
                    System.Windows.Forms.Form f = (System.Windows.Forms.Form)str;

                    if (f.Tag == null)
                    {
                        return "";
                    }
                    else
                    {
                        return f.Tag.ToString().TrimStart();
                    }
                }
                else
                {
                    string sTemp = Convert.ToString(str);

                    if (string.IsNullOrEmpty(sTemp) == true)
                    {
                        return "";
                    }
                    else
                    {
                        return sTemp.TrimStart();
                    }
                }
            }
            catch (Exception Ex)
            {
                Debug.Print(Ex.Message + " LTrim");
                return "";
            }
        }

        // StringTrim()
        //       - Trim Space Data
        // Return Value
        //       - boolean : True / False
        // Arguments
        //       - Form
        public static string StringTrim(object ff)
        {
            if (ff == null)
                return "";

            if (ff is System.Windows.Forms.Form)
            {
                System.Windows.Forms.Form f = (System.Windows.Forms.Form)ff;

                if (f.Tag == null)
                {
                    return "";
                }
                else
                {
                    return f.Tag.ToString().Trim();
                }
            }
            else
            {
                string f = System.Convert.ToString(ff);

                if (string.IsNullOrEmpty(f) == true)
                {
                    return "";
                }
                else
                {
                    return f.Trim();
                }
            }
        }

        // StringTrim()
        //       - Trim Space Data
        // Return Value
        //       - boolean : True / False
        // Arguments
        //       - string
        public static string StringTrim(object ff, string optSide)
        {
            try
            {
                if (ff == null)
                    return "";

                if (ff is System.Windows.Forms.Form)
                {
                    System.Windows.Forms.Form f = (System.Windows.Forms.Form)ff;

                    if (f.Tag == null)
                    {
                        return "";
                    }
                    else
                    {
                        if (optSide == "R")
                            return f.Tag.ToString().TrimEnd();
                        else if (optSide == "L")
                            return f.Tag.ToString().TrimStart();
                        else
                            return f.Tag.ToString().Trim();
                    }
                }
                else
                {
                    string f = System.Convert.ToString(ff);

                    if (string.IsNullOrEmpty(f) == true)
                    {
                        return "";
                    }
                    else
                    {
                        if (optSide == "R")
                            return f.TrimEnd();
                        else if (optSide == "L")
                            return f.TrimEnd();
                        else
                            return f.Trim();
                    }
                }
            }
            catch (Exception Ex)
            {
                Debug.Print(Ex.Message + " StringTrim");
                return "";
            }
        }

        public static char ToChar(object obj)
        {
            if (Trim(obj) == "")
            {
                return ' ';
            }
            else
            {
                return Trim(obj)[0];
            }
        }

        public static int ToInt(object obj)
        {
            CultureInfo ci_local = CultureInfo.CurrentCulture;
            CultureInfo ci_inter = new CultureInfo("en-US");
            int result;
            object val;

            try
            {
                if (obj == null)
                {
                    return 0;
                }

                if (obj is Enum)
                {
                    return (int)obj;
                }

                if (obj is Char)
                {
                    val = Convert.ToString(obj);
                }
                else
                {
                    val = obj;
                }

                if (CheckNumeric(val) == false)
                {
                    return 0;
                }

                try
                {
                    result = Convert.ToInt32(val, ci_inter.NumberFormat);
                    if (val.ToString().IndexOf(ci_local.NumberFormat.NumberDecimalSeparator) >= 0)
                    {
                        result = Convert.ToInt32(val, ci_local.NumberFormat);
                    }
                }
                catch
                {
                    result = Convert.ToInt32(val, ci_local.NumberFormat);
                }

                return result;
            }
            catch (Exception Ex)
            {
                Debug.Print(Ex.Message + " ToInt");
                return 0;
            }
        }

        public static double ToDbl(object obj)
        {
            CultureInfo ci_local = CultureInfo.CurrentCulture;
            CultureInfo ci_inter = new CultureInfo("en-US");
            double result;
            object val;

            try
            {
                if (obj == null)
                {
                    return 0;
                }

                if (obj is Char)
                {
                    val = Convert.ToString(obj);
                }
                else
                {
                    val = obj;
                }

                if (CheckNumeric(val) == false)
                {
                    return 0;
                }

                try
                {
                    result = Convert.ToDouble(val, ci_inter.NumberFormat);
                    if (val.ToString().IndexOf(ci_local.NumberFormat.NumberDecimalSeparator) >= 0)
                    {
                        result = Convert.ToDouble(val, ci_local.NumberFormat);
                    }
                }
                catch
                {
                    result = Convert.ToDouble(val, ci_local.NumberFormat);
                }

                return result;
            }
            catch (Exception Ex)
            {
                Debug.Print(Ex.Message + " ToDbl");
                return 0;
            }
        }

        // CheckNumeric()
        //       - Check Numeric Data
        // Return Value
        //       - boolean : True / False
        // Arguments
        //       - string
        public static bool CheckNumeric(object val)
        {
            double result;
            object obj;

            try
            {
                if (val == null) return false;

                if (val is Char)
                {
                    obj = Convert.ToString(val);
                }
                else
                {
                    obj = val;
                }

                result = Convert.ToDouble(obj);
                return true;
            }

            catch
            {
                return false;
            }
        }

        // StringMid()
        //       - Mid String
        // Return Value
        //       - boolean : True / False
        // Arguments
        //       - Form
        public static string StringMid(string str, int i_start_index)
        {
            if (str == null) return "";
            if (str.Length < i_start_index) return "";

            return str.Substring(i_start_index);
        }

        public static string StringMid(string str, int i_start_index, int i_length)
        {
            if (str == null) return "";
            if (str.Length < i_start_index) return "";
            if (str.Length < (i_start_index + i_length - 1)) return "";

            return str.Substring(i_start_index, i_length);
        }

        public static void SelectText(System.Windows.Forms.TextBox myText, bool Focus_Flag)
        {

            try
            {
                myText.SelectionStart = 0;
                myText.SelectionLength = myText.Text.Length;
                if (Focus_Flag == true)
                {
                    myText.Focus();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }

        public static String ToDate(string str)
        {
            try
            {
                string sTime = str.Trim();
                DateTime DTime;

                int year;
                int month;
                int day;
                int hour;
                int minute;
                int second;

                year = 0;
                month = 0;
                day = 0;
                hour = 0;
                minute = 0;
                second = 0;

                if (str.Trim() == "") return "";

                if (CheckNumeric(sTime) == true)
                {
                    if (sTime.Length >= 8)
                    {
                        year = Convert.ToInt32(sTime.Substring(0, 4));
                        month = Convert.ToInt32(sTime.Substring(4, 2));
                        day = Convert.ToInt32(sTime.Substring(6, 2));
                    }
                    if (sTime.Length >= 14)
                    {
                        hour = Convert.ToInt32(sTime.Substring(8, 2));
                        minute = Convert.ToInt32(sTime.Substring(10, 2));
                        second = Convert.ToInt32(sTime.Substring(12, 2));
                    }

                    DTime = new DateTime(year, month, day, hour, minute, second);
                }
                else
                {
                    DTime = DateTime.Now;
                }

                if (sTime.Length >= 14)
                {
                    return DTime.ToLocalTime().ToString();
                }
                else if (sTime.Length == 8)
                {
                    return DTime.ToShortDateString();
                }
                else
                {
                    return DateTime.Now.ToLocalTime().ToString();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return DateTime.Now.ToString();
            }
        }


        /// <summary>
        /// 날짜형식의 문자열을 날짜(DataTime)으로 변환하여 리턴한다.
        /// </summary>
        /// <param name="value">변경할 문자열값</param>
        /// <param name="stringFormat">문자열의 날짜형태</param>
        /// <param name="dtResult">결과를 리턴할 DateTime 변수</param>
        /// <returns>Boolean : True or False</returns>
        public static Boolean StringToDateTime(string value, string stringFormat, out DateTime dtResult)
        {
            try
            {
                dtResult = DateTime.ParseExact(value, stringFormat,
                        System.Globalization.DateTimeFormatInfo.InvariantInfo,
                        System.Globalization.DateTimeStyles.AllowLeadingWhite |
                        System.Globalization.DateTimeStyles.AllowTrailingWhite);
                return true;
            }
            catch
            {
                dtResult = DateTime.MinValue;
                return false;
            }
        }

        /// <summary>
        /// DateTimePicker를 문자열값으로 변환하여 결과를 리턴한다.
        /// </summary>
        /// <param name="dtpObject">DateTimePicker Object</param>
        /// <param name="outFormat">변환할 형식(yyyy-MM-dd HH:mm:ss, yyyy/MM/dd hh:mm:ss)</param>
        /// <param name="strResult">결과를 리턴할 String 변수</param>
        /// <returns>Boolean : True or False</returns>
        public static Boolean DateTimeToString(DateTimePicker dtpObject, string outFormat, out string strResult)
        {
            try
            {
                DateTime dt = DateTime.Parse(dtpObject.Text);
                strResult = dt.ToString(outFormat);
                return true;
            }
            catch
            {
                strResult = "";
                return false;
            }
        }

        /// <summary>
        /// 날짜형식의 문자열을 지정된 형식의 날짜문자열로 변환하여 리턴한다.
        /// </summary>
        /// <param name="value">날짜형식의 문자열</param>
        /// <param name="sourceFormat">vlaue가 가지는 날짜형식(yyyyMMddHHmmss,yyyyMMddhhmmss)</param>
        /// <param name="outFormat">변환할 형식(yyyy-MM-dd HH:mm:ss, yyyy/MM/dd hh:mm:ss)</param>
        /// <returns>Boolean : True or False</returns>
        public static string ChangeDateTimeString(string value, string sourceFormat, string outFormat)
        {
            DateTime dt;

            try
            {
                if (StringToDateTime(value, sourceFormat, out dt) == true)
                {
                    return dt.ToString(outFormat);
                }
                else
                {
                    return value;
                }
            }
            catch
            {
                return "";
            }
        }

        /// <summary>
        /// 쿼리에서 사용할 Decode 구문 스트링을 반환하는 메서드입니다.
        /// '09.01.06 양형석
        /// </summary>
        /// <param name="prevStr">앞 부분에서 반복되는 스트링</param>
        /// <param name="postStr">뒷 부분에서 반복되는 스트링</param>
        /// <param name="cnt">반복할 횟수</param>
        /// <returns>결과 스트링</returns>
        public static string GetDecodeQueryString(string prevStr, string postStr, int cnt)
        {
            string strReturn = string.Empty;

            try
            {
                for (int i = 1; i < cnt + 1; i++)
                {
                    strReturn += prevStr + i.ToString() + postStr + i.ToString() + " " + "\n";
                }

                return strReturn;
            }
            catch
            {
                return "";
            }
        }

        /// <summary>
        /// 2010-05-26-임종우 : DataTable을 이용하여 Decode 쿼리문 생성하여 반환하는 메서드 추가
        /// </summary>
        /// <param name="prevStr">앞 부분에서 반복되는 스트링</param>
        /// <param name="strArray">비교 데이터를 담은 DataTable</param>
        /// <param name="postStr">뒷 부분에서 반복되는 스트링</param>        
        /// <param name="usingDtValue">AS 이후 값을 DataTable 값으로 할지 아니면 생성 할지 구분</param>
        /// <returns>결과 스트링</returns>
        public static string GetDecodeQueryStringFromDataTable(string prevStr, DataTable dt, string postStr, bool usingDtValue )
        {
            string strReturn = string.Empty;

            try
            {
                if (usingDtValue)
                {
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        strReturn += prevStr + "'" + dt.Rows[i][0] + "'" + postStr + "\"" + dt.Rows[i][0] + "\"" + " " + "\n";
                    }

                }
                else
                {
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        strReturn += prevStr + "'" + dt.Rows[i][0] + "'" + postStr + i.ToString() + " " + "\n";
                    }
                }

                return strReturn;
            }
            catch
            {
                return "";
            }            
        }

		//public static bool fnDataFind(OracleCommand oracmd, ref DataSet ds)
		//{
		//    /****************************************************
		//     * Comment : Oracle Command를 이용하여 데이터르 조회한다.
		//     * 
		//     * Created By : bee-jae jung(2010-07-01-목요일)
		//     * 
		//     * Modified By : bee-jae jung(2010-07-01-목요일)
		//     ****************************************************/
		//    OracleConnection oraconn = new OracleConnection();
		//    OracleDataAdapter orada = new OracleDataAdapter();
		//    try
		//    {
		//        Cursor.Current = Cursors.WaitCursor;

		//        // 2010-06-24-정비재 : Oracle에 Direct로 연결한다.
		//        oraconn.ConnectionString = GlobalVariable.gDataBase.ConnectionString;
		//        oraconn.Open();
		//        oracmd.Connection = oraconn;
		//        orada.SelectCommand = oracmd;
		//        orada.Fill(ds);

		//        return true;
		//    }
		//    catch (Exception ex)
		//    {
		//        MessageBox.Show(ex.Message);
		//        return false;
		//    }
		//    finally
		//    {
		//        Cursor.Current = Cursors.Default;
		//    }
		//}

        /// <summary>
        /// 2012-10-30-임종우 : 지난주,금주,차주의 시작일,종료일,주차를 구한다.
        /// 2015-09-21-임종우 : 6주차까지 확대
        /// </summary>
        /// <param name="sysdate">기준일</param>
        /// <param name="calendar_id">캘린더 아이디</param>
        /// <returns></returns>
        public static GlobalVariable.FindWeek GetWeekInfo(string sysdate, string calendar_id)
        {
            GlobalVariable.FindWeek FindWeek = new GlobalVariable.FindWeek();
            StringBuilder strSqlString = new StringBuilder();
            DataTable dt = new DataTable();

            // 2015-12-25-임종우 : 1주일에 7일이 안되는 주차가 존재하여 쿼리 변경 함.
            strSqlString.Append("SELECT MIN(SYS_DATE) AS START_DAY " + "\n");
            strSqlString.Append("     , MAX(SYS_DATE) AS END_DAY " + "\n");
            strSqlString.Append("     , PLAN_YEAR||DECODE(LENGTH(PLAN_WEEK), 1, '0'||PLAN_WEEK, PLAN_WEEK) AS WEEK" + "\n");
            strSqlString.Append("  FROM MWIPCALDEF " + "\n");
            strSqlString.Append(" WHERE CALENDAR_ID = '" + calendar_id + "'" + "\n");
            strSqlString.Append("   AND (PLAN_YEAR || LPAD(PLAN_WEEK,2,'0')) IN ( " + "\n");
            strSqlString.Append("                                                SELECT WW" + "\n");
            strSqlString.Append("                                                  FROM (" + "\n");
            strSqlString.Append("                                                        SELECT WW" + "\n");
            strSqlString.Append("                                                             , ROW_NUMBER() OVER(ORDER BY WW) AS RNK" + "\n");
            strSqlString.Append("                                                          FROM (" + "\n");
            strSqlString.Append("                                                                SELECT DISTINCT PLAN_YEAR || LPAD(PLAN_WEEK,2,'0') AS WW" + "\n");
            strSqlString.Append("                                                                  FROM MWIPCALDEF" + "\n");
            strSqlString.Append("                                                                 WHERE CALENDAR_ID = '" + calendar_id + "'" + "\n");
            strSqlString.Append("                                                                   AND PLAN_YEAR || LPAD(PLAN_WEEK,2,'0') >= (" + "\n");
            strSqlString.Append("                                                                                                              SELECT MAX(PLAN_YEAR || LPAD(PLAN_WEEK,2,'0')) AS WW" + "\n");
            strSqlString.Append("                                                                                                                FROM MWIPCALDEF" + "\n");
            strSqlString.Append("                                                                                                               WHERE CALENDAR_ID = '" + calendar_id + "'" + "\n");
            strSqlString.Append("                                                                                                                 AND PLAN_YEAR || LPAD(PLAN_WEEK,2,'0') < (SELECT PLAN_YEAR || LPAD(PLAN_WEEK,2,'0') AS WW FROM MWIPCALDEF WHERE CALENDAR_ID = '" + calendar_id + "' AND SYS_DATE = '" + sysdate + "')" + "\n");
            strSqlString.Append("                                                                                                                 AND SYS_DATE >= TO_CHAR(TO_DATE('" + sysdate + "', 'YYYYMMDD')-14, 'YYYYMMDD')" + "\n");
            strSqlString.Append("                                                                                                             )" + "\n");
            strSqlString.Append("                                                               ) " + "\n");
            strSqlString.Append("                                                       )" + "\n");
            strSqlString.Append("                                                 WHERE RNK <= 7" + "\n");
            strSqlString.Append("                                               ) " + "\n");
            strSqlString.Append(" GROUP BY PLAN_YEAR, PLAN_WEEK " + "\n");
            strSqlString.Append(" ORDER BY PLAN_YEAR, PLAN_WEEK" + "\n");

            //strSqlString.Append("SELECT MIN(SYS_DATE) AS START_DAY " + "\n");
            //strSqlString.Append("     , MAX(SYS_DATE) AS END_DAY " + "\n");
            //strSqlString.Append("     , PLAN_YEAR||DECODE(LENGTH(PLAN_WEEK), 1, '0'||PLAN_WEEK, PLAN_WEEK) AS WEEK " + "\n");
            //strSqlString.Append("  FROM MWIPCALDEF " + "\n");
            //strSqlString.Append(" WHERE CALENDAR_ID = '" + calendar_id + "'" + "\n");
            //strSqlString.Append("   AND (PLAN_YEAR, PLAN_WEEK) IN ( " + "\n");
            //strSqlString.Append("                                  SELECT PLAN_YEAR, PLAN_WEEK" + "\n");
            //strSqlString.Append("                                    FROM MWIPCALDEF" + "\n");
            //strSqlString.Append("                                   WHERE CALENDAR_ID = '" + calendar_id + "'" + "\n");
            //strSqlString.Append("                                     AND SYS_DATE IN ( " + "\n");
            //strSqlString.Append("                                                      TO_CHAR(TO_DATE('" + sysdate + "', 'YYYYMMDD')-7, 'YYYYMMDD') " + "\n");
            //strSqlString.Append("                                                    , '" + sysdate + "'" + "\n");
            //strSqlString.Append("                                                    , TO_CHAR(TO_DATE('" + sysdate + "', 'YYYYMMDD')+7, 'YYYYMMDD') " + "\n");
            //strSqlString.Append("                                                    , TO_CHAR(TO_DATE('" + sysdate + "', 'YYYYMMDD')+14, 'YYYYMMDD') " + "\n");
            //strSqlString.Append("                                                    , TO_CHAR(TO_DATE('" + sysdate + "', 'YYYYMMDD')+21, 'YYYYMMDD') " + "\n");
            //strSqlString.Append("                                                    , TO_CHAR(TO_DATE('" + sysdate + "', 'YYYYMMDD')+28, 'YYYYMMDD') " + "\n");
            //strSqlString.Append("                                                    , TO_CHAR(TO_DATE('" + sysdate + "', 'YYYYMMDD')+35, 'YYYYMMDD') " + "\n");
            //strSqlString.Append("                                                     ) " + "\n");
            //strSqlString.Append("                                 ) " + "\n");
            //strSqlString.Append(" GROUP BY PLAN_YEAR, PLAN_WEEK " + "\n");
            //strSqlString.Append(" ORDER BY PLAN_YEAR, PLAN_WEEK " + "\n");

            dt = CmnFunction.oComm.GetFuncDataTable("DYNAMIC", strSqlString.ToString());

            // 원본
            FindWeek.StartDay_LastWeek = dt.Rows[0][0].ToString();
            FindWeek.EndDay_LastWeek = dt.Rows[0][1].ToString();
            FindWeek.LastWeek = dt.Rows[0][2].ToString();
            FindWeek.StartDay_ThisWeek = dt.Rows[1][0].ToString();
            FindWeek.EndDay_ThisWeek = dt.Rows[1][1].ToString();
            FindWeek.ThisWeek = dt.Rows[1][2].ToString();
            FindWeek.StartDay_NextWeek = dt.Rows[2][0].ToString();
            FindWeek.EndDay_NextWeek = dt.Rows[2][1].ToString();
            FindWeek.NextWeek = dt.Rows[2][2].ToString();
            FindWeek.StartDay_W2_Week = dt.Rows[3][0].ToString();
            FindWeek.EndDay_W2_Week = dt.Rows[3][1].ToString();
            FindWeek.W2_Week = dt.Rows[3][2].ToString();
            FindWeek.StartDay_W3_Week = dt.Rows[4][0].ToString();
            FindWeek.EndDay_W3_Week = dt.Rows[4][1].ToString();
            FindWeek.W3_Week = dt.Rows[4][2].ToString();
            FindWeek.StartDay_W4_Week = dt.Rows[5][0].ToString();
            FindWeek.EndDay_W4_Week = dt.Rows[5][1].ToString();
            FindWeek.W4_Week = dt.Rows[5][2].ToString();
            FindWeek.StartDay_W5_Week = dt.Rows[6][0].ToString();
            FindWeek.EndDay_W5_Week = dt.Rows[6][1].ToString();
            FindWeek.W5_Week = dt.Rows[6][2].ToString();

            // 2015-11-27-정비재 : 수정
            //if (dt.Rows.Count >= 1)
            //{
            //    FindWeek.StartDay_LastWeek = (dt.Rows[0].IsNull(0) == false ? dt.Rows[0][0].ToString() : "");
            //    FindWeek.EndDay_LastWeek = (dt.Rows[0].IsNull(1) == false ? dt.Rows[0][1].ToString() : "");
            //    FindWeek.LastWeek = (dt.Rows[0].IsNull(2) == false ? dt.Rows[0][2].ToString() : "");
            //}
            //if (dt.Rows.Count >= 2)
            //{
            //    FindWeek.StartDay_ThisWeek = (dt.Rows[1].IsNull(0) == false ? dt.Rows[1][0].ToString() : "");
            //    FindWeek.EndDay_ThisWeek = (dt.Rows[1].IsNull(1) == false ? dt.Rows[1][1].ToString() : "");
            //    FindWeek.ThisWeek = (dt.Rows[1].IsNull(2) == false ? dt.Rows[1][2].ToString() : "");
            //}
            //if (dt.Rows.Count >= 3)
            //{
            //    FindWeek.StartDay_NextWeek = (dt.Rows[2].IsNull(0) == false ? dt.Rows[2][0].ToString() : "");
            //    FindWeek.EndDay_NextWeek = (dt.Rows[2].IsNull(1) == false ? dt.Rows[2][1].ToString() : "");
            //    FindWeek.NextWeek = (dt.Rows[2].IsNull(2) == false ? dt.Rows[2][2].ToString() : "");
            //}
            //if (dt.Rows.Count >= 4)
            //{
            //    FindWeek.StartDay_W2_Week = (dt.Rows[3].IsNull(0) == false ? dt.Rows[3][0].ToString() : "");
            //    FindWeek.EndDay_W2_Week = (dt.Rows[3].IsNull(1) == false ? dt.Rows[3][1].ToString() : "");
            //    FindWeek.W2_Week = (dt.Rows[3].IsNull(2) == false ? dt.Rows[3][2].ToString() : "");
            //}
            //if (dt.Rows.Count >= 5)
            //{
            //    FindWeek.StartDay_W3_Week = (dt.Rows[4].IsNull(0) == false ? dt.Rows[4][0].ToString() : "");
            //    FindWeek.EndDay_W3_Week = (dt.Rows[4].IsNull(1) == false ? dt.Rows[4][1].ToString() : "");
            //    FindWeek.W3_Week = (dt.Rows[4].IsNull(2) == false ? dt.Rows[4][2].ToString() : "");
            //}
            //if (dt.Rows.Count >= 6)
            //{
            //    FindWeek.StartDay_W4_Week = (dt.Rows[5].IsNull(0) == false ? dt.Rows[5][0].ToString() : "");
            //    FindWeek.EndDay_W4_Week = (dt.Rows[5].IsNull(1) == false ? dt.Rows[5][1].ToString() : "");
            //    FindWeek.W4_Week = (dt.Rows[5].IsNull(2) == false ? dt.Rows[5][2].ToString() : "");
            //}
            //if (dt.Rows.Count >= 7)
            //{
            //    FindWeek.StartDay_W5_Week = (dt.Rows[6].IsNull(0) == false ? dt.Rows[6][0].ToString() : "");
            //    FindWeek.EndDay_W5_Week = (dt.Rows[6].IsNull(1) == false ? dt.Rows[6][1].ToString() : "");
            //    FindWeek.W5_Week = (dt.Rows[6].IsNull(2) == false ? dt.Rows[6][2].ToString() : "");
            //}
                        
            return FindWeek;
        }

    }
}
