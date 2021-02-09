
using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using System.Collections;
using System.Diagnostics;
using Miracom.UI.Controls;
using Miracom.FMBUI;
using Miracom.FMBUI.Controls;
using Infragistics.Win.UltraWinEditors;
using System.Globalization;
using System.Reflection;

using com.miracom.transceiverx;
using com.miracom.transceiverx.session;
using com.miracom.transceiverx.message;
using Miracom.SmartWeb.FWX;
//using Miracom.H101Core;
//using Miracom.MESCore;


//-----------------------------------------------------------------------------
//
//   System      : FMBClient
//   File Name   : modCommonFunction.vb
//   Description : Common Function Definition Module
//
//   FMB Version : 1.0.0
//
//   Function List
//       - MakeErrorMsg() : Make error message
//       - FieldClear() : Control included field clear
//       - GetStringBySeperator() : Get string by seperator
//       - InitListView() : Clear Listview and Set Icon source
//       - FindListItem() : ListView에서 특정 Item을 찾는다.
//       - MakeDateFormat() : 일반 문자열을 Time Format으로 변환
//       - DestroyDateFormat() : 날짜 형식의 문자열을 일반 문자열로 변환한다.
//       - RefreshFactoryList() : Refresh Factory List of trvDesignList
//       - RefreshDesignList() : Refresh Design List
//       - RefreshUdrGroupList() : Refresh User Define Group List
//       - SetEnumList() : Make Enum to List
//       - SetTagTypeList() : Make Miracom.FMBUI.Enums.eToolType Enum to List
//       - SetFontSize() : Set Font Size
//       - GetIndexedControl() : parentControl 위에 있는 동일한 Prefix이름을 가진 control만을 이름순으로 정렬하여 리스트로 만든다.
//       - PublishMsgTune() : Publish Message Tune
//       - ChangeFromDateFormat() : Change 14 byte DateTime format of DateTimePicker
//       - ChangeToDateFormat() : Change 14 byte DateTime format of DateTimePicker
//       - GetChildForm() : Child Form이 있는지 확인한다
//       - GetScale() : Get Scale
//       - StartTimerProgress() : Start Timer
//       - StopTimerProgress() : Stop Timer
//       - SelectText() : Select Text of the TextBox
//       - GetControl() : Get Control from the panel
//       - RefreshControl() : Refresh Control Status
//       -CmnFunction.ShowMsgBox() : Show Message Box
//       - CheckGRPCMFValue() : Check Group/Cmf Value
//       - FieldVisableStatus() : Change Field Visible
//       - CheckCMFKeyPress() : Check Cmf CodeView Key Press Event
//       - InitGRPCMFControl() : initial Group/Cmf Control
//       - ClearList() : Clear List Control
//       - FindTreeNode() : Find Tree Node
//       - GetFactoryNode() : Get Factory Node
//       - SetStatusMsg() : Set Statusbar Message
//       - ToAsc() : String to Ascii
//       - ToChar() : String to Character
//       - GetTextboxStyle() : Set Textbox Style by Option
//       - Client_Upgrade() : Upgrade Client
//       - GetHelpURL() : Get Help URL
//       - CheckSecurityFormControl() : Check Security Form Control
//       - CheckAvailableFunction() : Check Available Function
//		- ByteLen() : Get byte length in String
//		- ByteMid() : Get Middle String at byte length in String
//		- CheckMaxLength() : check byte text length in TextBox
//       - CheckValue() :  Check the value is correct
//       - UpdateResourceLocation() :  Update Resource Location
//       - UpdateUDRResourceLocation() :  Update User Define Resource Location
//       - ViewLayOut() :  View Layout Information
//       - ViewUDRGroup() : View User Define Group Information
//       - ConvertColorToString() :  Convert color to string
//       - ConvertStringToColor() :  Convert string to color
//       - ViewGlobalOption() :  View Global Options
//       - CheckAllFactoryOption() :  Set All Factory Option
//
//   Detail Description
//       -
//
//   History
//       - 2005-02-01 : Created by Laverwon
//       - 2005-02-01 : Insert MakeErrorMsg() by Laverwon
//       - 2005-02-11 : Insert FieldClear() by Hkyung
//       - 2005-02-11 : Insert GetStringBySeperator() by Laverwon
//       - 2005-02-12 : Insert RefreshFactoryList() by Laverwon
//
//   Copyright(C) 1998-2004 MIRACOM,INC.
//   All rights reserved.
//
//-----------------------------------------------------------------------------

namespace Miracom.SmartWeb.UI
{
    public sealed class modCommonFunction
    {

        #region " Variable Definition"
        #endregion

      

        // GetStringBySeperator()
        //       - Get string by seperator
        // Return Value
        //       - String : Return string
        // Arguments
        //       - ByVal sSource As String  : Source String Data
        //       - ByVal sSeperator As String : Seperator
        //       - ByVal iPosition As Integer : Seperator Position
        //
        public static string GetStringBySeperator(string sSource, string sSeperator, int iPosition)
        {

            string sReturn = "";

            try
            {
                int i = 0;
                int iStartPos = 0;
                int iFindPos = 0;
                if (sSource.IndexOf(sSeperator) < 0)
                {
                    return sReturn;
                }
                while (i < iPosition)
                {
                    iFindPos = sSource.IndexOf(sSeperator, iStartPos);
                    if (iFindPos < 0)
                    {
                        sReturn = sSource.Substring(iStartPos);
                        break;
                    }
                    else
                    {
                        sReturn = sSource.Substring(iStartPos, iFindPos - iStartPos);
                    }
                    iStartPos = iFindPos + sSeperator.Length;
                    i++;
                }

            }
            catch (Exception ex)
            {
                CmnFunction.ShowMsgBox("modCommonFunction.GetStringBySeperator()" + "\r\n" + ex.Message, "FMB Client", MessageBoxButtons.OK, 1);
                sReturn = "";
            }

            return sReturn;

        }

      
        // SetEnumList()
        //       - Enum to List
        // Return Value
        //       -
        // Arguments
        //       - ByVal cboTarget As ComboBox   :  Combo Box
        //       - ByVal enumType As Type        :  Type
        //
        public static void SetEnumList(ComboBox cboTarget, Type enumType)
        {

            try
            {
                cboTarget.Items.Clear();
                string sEnumString = "";
                foreach (string tempLoopVar_sEnumString in @Enum.GetNames(enumType))
                {
                    sEnumString = tempLoopVar_sEnumString;
                    cboTarget.Items.Add(sEnumString);
                }

            }
            catch (Exception ex)
            {
                CmnFunction.ShowMsgBox("modCommonFunction.SetEnumList()" + "\r\n" + ex.Message, "FMB Client", MessageBoxButtons.OK, 1);
            }

        }

        // SetEnumList()
        //       - Enum to List
        // Return Value
        //       -
        // Arguments
        //       - ByVal cboTarget As Infragistics.Win.UltraWinEditors.UltraComboEditor   :  Combo Box
        //       - ByVal enumType As Type        :  Type
        //
        public static void SetEnumList(Infragistics.Win.UltraWinEditors.UltraComboEditor cboTarget, Type enumType)
        {

            try
            {
                cboTarget.Items.Clear();
                string sEnumString = "";
                foreach (string tempLoopVar_sEnumString in @Enum.GetNames(enumType))
                {
                    sEnumString = tempLoopVar_sEnumString;
                    cboTarget.Items.Add(sEnumString);
                }

            }
            catch (Exception ex)
            {
                CmnFunction.ShowMsgBox("modCommonFunction.SetEnumList()" + "\r\n" + ex.Message, "FMB Client", MessageBoxButtons.OK, 1);
            }

        }

        // SetTagTypeList()
        //       - Miracom.FMBUI.Enums.eToolType Enum to List
        // Return Value
        //       -
        // Arguments
        //       - ByVal cboShape As ComboBox   :  Combo Box
        //
        public static void SetTagTypeList(ComboBox cboShape)
        {

            try
            {
                cboShape.Items.Clear();
                string sToolType;
                foreach (string tempLoopVar_sToolType in @Enum.GetNames(typeof(Miracom.FMBUI.Enums.eToolType)))
                {
                    sToolType = tempLoopVar_sToolType;
                    if (sToolType != Miracom.FMBUI.Enums.eToolType.Resource.ToString() && sToolType != Miracom.FMBUI.Enums.eToolType.Null.ToString())
                    {
                        cboShape.Items.Add(sToolType);
                    }
                }

            }
            catch (Exception ex)
            {
                CmnFunction.ShowMsgBox("modCommonFunction.SetTagTypeList()" + "\r\n" + ex.Message, "FMB Client", MessageBoxButtons.OK, 1);
            }

        }

        // SetTagTypeList()
        //       - Miracom.FMBUI.Enums.eToolType Enum to List
        // Return Value
        //       -
        // Arguments
        //       - ByVal cboShape As Infragistics.Win.UltraWinEditors.UltraComboEditor   :  Combo Box
        //
        public static void SetTagTypeList(Infragistics.Win.UltraWinEditors.UltraComboEditor cboShape)
        {

            try
            {
                cboShape.Items.Clear();
                string sToolType;
                foreach (string tempLoopVar_sToolType in @Enum.GetNames(typeof(Miracom.FMBUI.Enums.eToolType)))
                {
                    sToolType = tempLoopVar_sToolType;
                    if (sToolType != System.Convert.ToString(Miracom.FMBUI.Enums.eToolType.Resource) && sToolType != System.Convert.ToString(Miracom.FMBUI.Enums.eToolType.Null))
                    {
                        cboShape.Items.Add(sToolType);
                    }
                }

            }
            catch (Exception ex)
            {
                CmnFunction.ShowMsgBox("modCommonFunction.SetTagTypeList()" + "\r\n" + ex.Message, "FMB Client", MessageBoxButtons.OK, 1);
            }

        }

        // SetFontSize()
        //       - Set Font Size
        // Return Value
        //       -
        // Arguments
        //       - ByVal cboFontSize As ComboBox   :  Combo Box
        //
        public static void SetFontSize(ComboBox cboFontSize)
        {

            int i;

            try
            {
                cboFontSize.Items.Clear();
                for (i = 5; i <= 20; i++)
                {
                    cboFontSize.Items.Add(i);
                }

            }
            catch (Exception ex)
            {
                CmnFunction.ShowMsgBox("modCommonFunction.SetFontSize()" + "\r\n" + ex.Message, "FMB Client", MessageBoxButtons.OK, 1);
            }

        }

        // SetFontSize()
        //       - Set Font Size
        // Return Value
        //       -
        // Arguments
        //       - ByVal cboFontSize As Infragistics.Win.UltraWinEditors.UltraComboEditor   :  Combo Box
        //
        public static void SetFontSize(Infragistics.Win.UltraWinEditors.UltraComboEditor cboFontSize)
        {

            int i;

            try
            {
                cboFontSize.Items.Clear();
                for (i = 5; i <= 20; i++)
                {
                    cboFontSize.Items.Add(i);
                }

            }
            catch (Exception ex)
            {
                CmnFunction.ShowMsgBox("modCommonFunction.SetFontSize()" + "\r\n" + ex.Message, "FMB Client", MessageBoxButtons.OK, 1);
            }

        }

        // GetIndexedControl()
        //       - parentControl 위에 있는 동일한 Prefix이름을 가진 control만을 이름순으로 정렬하여 리스트로 만든다.
        // Return Value
        //       - ArrayLiat : 이름으로 정렬된 control의 리스트
        // Arguments
        //       - ByVal sControlName As String : Control의 Prefix이름
        //		- ByVal parentControl As Control : Control들이 올라가 있는 parentControl
        //
        public static ArrayList GetIndexedControl(string sControlName, Control parentControl)
        {

            try
            {
                Control control;
                ArrayList alControl = new ArrayList();
                ControlNameSort nameSort = new ControlNameSort();

                foreach (Control tempLoopVar_control in parentControl.Controls)
                {
                    control = tempLoopVar_control;
                    if (control.Name.Length > sControlName.Length)
                    {
                        if (control.Name.ToUpper().Substring(0, sControlName.Length) == sControlName.ToUpper())
                        {
                            alControl.Add(control);
                        }
                    }
                }
                alControl.Sort(nameSort);
                return alControl;

            }
            catch (Exception ex)
            {
               CmnFunction.ShowMsgBox("modCommonFunction.GetIndexedControl()" + "\r\n" + ex.Message, "FMB Client", MessageBoxButtons.OK, 1);
                return null;
            }

        }

        // ChangeFromDateFormat()
        //       -   Change 14 byte DateTime format of DateTimePicker
        // Return Value
        //       - String : 14 byte DateTime
        // Arguments
        //       - ByVal dtpTime As DateTimePicker
        //
        public static string ChangeFromDateFormat(DateTimePicker dtpTime)
        {

            try
            {
                string sDateTime = "";
                DateTime DTime;

                DTime = dtpTime.Value;

                sDateTime = DTime.Year.ToString("0000");
                sDateTime = sDateTime + DTime.Month.ToString("00");
                sDateTime = sDateTime + DTime.Day.ToString("00");
                sDateTime = sDateTime + DTime.Hour.ToString("00");
                sDateTime = sDateTime + DTime.Minute.ToString("00");
                sDateTime = sDateTime + DTime.Second.ToString("00");

                return sDateTime;

            }
            catch (Exception ex)
            {
               CmnFunction.ShowMsgBox("modCommonFunction.ChangeFromDateFormat()" + "\r\n" + ex.Message, "FMB Client", MessageBoxButtons.OK, 1);
                return "";
            }

        }

        // ChangeToDateFormat()
        //       -   Change 14 byte DateTime format of DateTimePicker
        // Return Value
        //       - String : 14 byte DateTime
        // Arguments
        //       - ByVal dtpTime As DateTimePicker
        //
        public static string ChangeToDateFormat(DateTimePicker dtpTime)
        {

            try
            {
                string sDateTime = "";
                DateTime DTime;

                DTime = dtpTime.Value;

                sDateTime = DTime.Year.ToString("0000");
                sDateTime = sDateTime + DTime.Month.ToString("00");
                sDateTime = sDateTime + DTime.Day.ToString("00");
                sDateTime = sDateTime + "235959";

                return sDateTime;

            }
            catch (Exception ex)
            {
               CmnFunction.ShowMsgBox("modCommonFunction.ChangeToDateFormat()" + "\r\n" + ex.Message, "FMB Client", MessageBoxButtons.OK, 1);
                return "";
            }

        }

        // GetChildForm()
        //       - Child Form이 있는지 확인한다
        // Return Value
        //       - True or False
        // Arguments
        //       - ByVal MDIForm As Form : MDI Parent Form
        //       - ByVal ChildForm As String : 현재 띄울 Child Form Name
        //
        static public Form GetChildForm(Form MDIForm, string ChildForm)
        {

            Form SForm;
            int iCount;

            try
            {
                ChildForm = ChildForm.ToUpper();
                iCount = 0;
                //if (!(MDIForm.ActiveForm == null))
                if (MDIForm.ActiveMdiChild != null)
                {
                   // foreach (Form tempLoopVar_SForm in MDIForm.ActiveForm.MdiChildren)
                    foreach (Form tempLoopVar_SForm in MDIForm.MdiChildren)
                    {
                        SForm = tempLoopVar_SForm;
                        if (SForm.Name.ToUpper() == ChildForm)
                        {
                            if (iCount < 9 && (ChildForm == "FRMFMBVIEWRESOURCESTATUS" || ChildForm == "FRMFMBVIEWRESOURCEHISTORY"))
                            {
                                iCount++;
                            }
                            else
                            {
                                SForm.Activate();
                                return SForm;
                            }
                        }
                    }
                }

                return null;

            }
            catch (Exception ex)
            {
               CmnFunction.ShowMsgBox("modCommonFunction.GetChildForm()" + "\r\n" + ex.Message, "FMB Client", MessageBoxButtons.OK, 1);
            }
            return null;
        }

        // GetScale()
        //       - Get Scale
        // Return Value
        //       - Double : 비율
        // Arguments
        //       - ByVal iZoomScale As Integer : case
        //
        public static double GetScale(int iZoomScale)
        {

            double dScale = 1;

            try
            {
                switch (iZoomScale)
                {
                    case 5:

                        dScale = 2;
                        break;
                    case 4:

                        dScale = 1.8;
                        break;
                    case 3:

                        dScale = 1.6;
                        break;
                    case 2:

                        dScale = 1.4;
                        break;
                    case 1:

                        dScale = 1.2;
                        break;
                    case 0:

                        dScale = 1;
                        break;
                    case -1:

                        dScale = 0.9;
                        break;
                    case -2:

                        dScale = 0.8;
                        break;
                    case -3:

                        dScale = 0.7;
                        break;
                    case -4:

                        dScale = 0.6;
                        break;
                    case -5:

                        dScale = 0.5;
                        break;
                }

            }
            catch (Exception ex)
            {
               CmnFunction.ShowMsgBox("modCommonFunction.GetScale()" + "\r\n" + ex.Message, "FMB Client", MessageBoxButtons.OK, 1);
            }

            return dScale;

        }

        // GetControl()
        //       - Get Control from the panel
        // Return Value
        //       - udcCtrlBase : Control which named skey
        // Arguments
        //		- ByVal sKey As String : Name of the control to get
        //
        public static udcCtrlBase GetControl(Control.ControlCollection CtrlCollection, string sKey, Enums.eToolType ToolType)
        {

            try
            {
                Control ctrl = null;
                foreach (Control tempLoopVar_ctrl in CtrlCollection)
                {
                    ctrl = tempLoopVar_ctrl;
                    if (ctrl is udcCtrlBase)
                    {
                        if (((udcCtrlBase)ctrl).Name == sKey && ((udcCtrlBase)ctrl).CtrlStatus.ToolType == ToolType)
                        {
                            return ((udcCtrlBase)ctrl);
                        }
                    }
                }

                return null;

            }
            catch (Exception ex)
            {
               CmnFunction.ShowMsgBox("frmFMBDesign.GetControl()" + "\r\n" + ex.Message, "FMB Client", MessageBoxButtons.OK, 1);
                return null;
            }

        }


         // CheckGRPCMFValue()
        //       - Check Group/Cmf Value
        // Return Value
        //       -
        // Arguments
        //       - ByVal sLabelName As String    : Label Control Name
        //       - ByVal sCodeViewName As String : CodeView Control Name
        //       - ByVal parentControl As Control : Parent Control
        //
        public static bool CheckGRPCMFValue(string sLabelName, string sCodeViewName, Control parentControl)
        {
            ArrayList lblList;
            ArrayList cdvList;
            Label lblTemp;
            Miracom.UI.Controls.MCCodeView.MCCodeView cdvTemp;
            int i;

            try
            {
                lblList = GetIndexedControl(sLabelName, parentControl);
                cdvList = GetIndexedControl(sCodeViewName, parentControl);

                for (i = 0; i <= lblList.Count - 1; i++)
                {
                    lblTemp = (Label)lblList[i];

                    if (lblTemp.Text != "" && lblTemp.Font.Bold == true)
                    {
                        cdvTemp = (Miracom.UI.Controls.MCCodeView.MCCodeView)cdvList[i];
                        if (cdvTemp.Tag.ToString() == "A")
                        {
                            if (cdvTemp.Text == "")
                            {
                               CmnFunction.ShowMsgBox(modLanguageFunction.GetMessage(6) + " [" + lblTemp.Text + "]", "FMB Client", MessageBoxButtons.OK, 1);
                                return false;
                            }
                        }
                        else
                        {
                            if (Microsoft.VisualBasic.Information.IsNumeric(cdvTemp.Text) == false)
                            {
                               CmnFunction.ShowMsgBox(modLanguageFunction.GetMessage(14) + " [" + lblTemp.Text + "]", "FMB Client", MessageBoxButtons.OK, 1);
                                return false;
                            }
                        }
                    }
                }

                return true;

            }
            catch (Exception ex)
            {
               CmnFunction.ShowMsgBox("modCommonFunction.CheckGRPCMFValue()" + "\r\n" + ex.Message, "FMB Client", MessageBoxButtons.OK, 1);
                return false;
            }

        }

       
        // SetCMFItem()
        //       - Setting Cmf Control Property
        // Return Value
        //       -
        // Arguments
        //       - ByVal sItemName As String             : Group Item Name
        //       - ByVal sLabelName As String			: Label Control Prefix Name
        //		- ByVal sCodeViewName As String			: CodeView Control Prefix Name
        //		- ByVal parentControl As Control		: ParentControl
        //
        public static void SetCMFItem(string sItemName, string sLabelName, string sCodeViewName, Control parentControl, string sFactory)
        {
            WIP_View_Factory_Cmf_Item_Out_Tag View_FacCmf_Item_Out = new WIP_View_Factory_Cmf_Item_Out_Tag();
            ArrayList lblList;
            ArrayList cdvList;
            Label lblTemp;
            Miracom.UI.Controls.MCCodeView.MCCodeView cdvTemp;
            int i;

            try
            {
                InitGRPCMFControl(sLabelName, sCodeViewName, parentControl);

                if (modListRoutine.ViewFacCmfData('1', sItemName, ref View_FacCmf_Item_Out, sFactory, false) == false)
                {
                    return;
                }

                lblList = GetIndexedControl(sLabelName, parentControl);
                cdvList = GetIndexedControl(sCodeViewName, parentControl);

                for (i = 0; i <= View_FacCmf_Item_Out.count - 1; i++)
                {
                    lblTemp = (Label)lblList[i];
                    cdvTemp = (Miracom.UI.Controls.MCCodeView.MCCodeView)cdvList[i];

                    lblTemp.Text = CmnFunction.Trim(View_FacCmf_Item_Out.data_tbl[i].prompt);
                    if (lblTemp.Text != "")
                    {
                        if (CmnFunction.Trim(View_FacCmf_Item_Out.data_tbl[i].opt) == "Y")
                        {
                            lblTemp.Font = new System.Drawing.Font(lblTemp.Font, System.Drawing.FontStyle.Bold);
                        }

                        cdvTemp.Tag = CmnFunction.Trim(View_FacCmf_Item_Out.data_tbl[i].format);
                        cdvTemp.Tag = cdvTemp.Tag + CmnFunction.Trim(View_FacCmf_Item_Out.data_tbl[i].table_name);
                        if (CmnFunction.Trim(View_FacCmf_Item_Out.data_tbl[i].table_name) == "")
                        {
                            cdvTemp.ReadOnly = false;
                            cdvTemp.VisibleButton = false;
                            cdvTemp.DisplaySubItemIndex = cdvTemp.SelectedSubItemIndex;
                        }
                        else if (CmnFunction.Trim(View_FacCmf_Item_Out.data_tbl[i].opt) != "Y")
                        {
                            cdvTemp.ReadOnly = false;
                        }
                        lblTemp.Visible = true;
                        cdvTemp.Visible = true;

                        if (CmnFunction.Trim(View_FacCmf_Item_Out.data_tbl[i].table_name) != "")
                        {
                            if (cdvTemp.DisplaySubItemIndex != cdvTemp.SelectedSubItemIndex)
                            {
                                //BASLIST.ViewGCMDataList(cdvTemp.GetListView, '2', CmnFunction.Trim(View_FacCmf_Item_Out.data_tbl[i].table_name),-1,null,sFactory);
                            }
                        }

                    }
                }

            }
            catch (Exception ex)
            {
               CmnFunction.ShowMsgBox("modCommonFunction.SetCMFItem()" + "\r\n" + ex.Message, "FMB Client", MessageBoxButtons.OK, 1);
            }

        }

        // ProcGRPCMFButtonPress()
        //       - Process Group/Cmf CodeView Button Press Event
        // Return Value
        //       -
        // Arguments
        //       - ByVal sender As Object    : Occur ButtonPress Event CodeView
        //
        public static void ProcGRPCMFButtonPress(Miracom.UI.Controls.MCCodeView.MCCodeView sender)
        {

            try
            {
                Miracom.UI.Controls.MCCodeView.MCCodeView cdvTemp;
                string sTableName;

                cdvTemp = (Miracom.UI.Controls.MCCodeView.MCCodeView)sender;
                if (System.Convert.ToString(cdvTemp.Tag) != "")
                {
                    sTableName = System.Convert.ToString(cdvTemp.Tag);
                    sTableName = sTableName.Substring(1, sTableName.Length - 1);
                    //BASLIST.ViewGCMDataList(cdvTemp.GetListView, '2', sTableName, -1,null, GlobalVariable.gsFactory);
                }

            }
            catch (Exception ex)
            {
               CmnFunction.ShowMsgBox("modCommonFunction.ProcGRPCMFButtonPress()" + "\r\n" + ex.Message, "FMB Client", MessageBoxButtons.OK, 1);
            }

        }

        // CheckCMFKeyPress()
        //       - Check Cmf CodeView Key Press Event
        // Return Value
        //       -
        // Arguments
        //       - ByVal sender As Object    : Occur KeyPress Event CodeView
        //       - ByVal e As System.Windows.Forms.KeyPressEventArgs : KeyPress Event Argument
        public static void CheckCMFKeyPress(Miracom.UI.Controls.MCCodeView.MCCodeView sender, System.Windows.Forms.KeyPressEventArgs e)
        {

            try
            {
                Miracom.UI.Controls.MCCodeView.MCCodeView cdvTemp;
                string sFormat;

                cdvTemp = (Miracom.UI.Controls.MCCodeView.MCCodeView)sender;

                if (System.Convert.ToString(cdvTemp.Tag) != "")
                {
                    sFormat = System.Convert.ToString(cdvTemp.Tag);
                    sFormat = sFormat.Substring(0, 1);
                    if (sFormat == "N" || sFormat == "F")
                    {
                        if (e.KeyChar < Microsoft.VisualBasic.Strings.Chr(48) || e.KeyChar > Microsoft.VisualBasic.Strings.Chr(57))
                        {
                            if (!(e.KeyChar == Microsoft.VisualBasic.Strings.Chr(43) || e.KeyChar == Microsoft.VisualBasic.Strings.Chr(45) || e.KeyChar == Microsoft.VisualBasic.Strings.Chr(8)))
                            {
                                if (sFormat == "F")
                                {
                                    if (!(e.KeyChar == Microsoft.VisualBasic.Strings.Chr(46)))
                                    {
                                        e.Handled = true;
                                    }
                                }
                                else
                                {
                                    e.Handled = true;
                                }
                            }
                        }
                    }
                }

            }
            catch (Exception ex)
            {
               CmnFunction.ShowMsgBox("modCommonFunction.CheckCMFKeyPress()" + "\r\n" + ex.Message, "FMB Client", MessageBoxButtons.OK, 1);
            }

        }

        // InitGRPCMFControl()
        //       - initial Group/Cmf Control
        // Return Value
        //       -
        // Arguments
        //       - ByVal sLabelName As String			: Label Control Prefix Name
        //		- ByVal sCodeViewName As String			: CodeView Control Prefix Name
        //		- ByVal parentControl As Control		: ParentControl
        public static void InitGRPCMFControl(string sLabelName, string sCodeViewName, Control parentControl)
        {

            try
            {
                ArrayList controls;
                Miracom.UI.Controls.MCCodeView.MCCodeView cdvTemp;
                int i;

                controls = GetIndexedControl(sLabelName, parentControl);
                for (i = 0; i <= controls.Count - 1; i++)
                {
                    ((Label)controls[i]).Visible = false;
                    ((Label)controls[i]).Text = "";
                }

                controls = GetIndexedControl(sCodeViewName, parentControl);
                for (i = 0; i <= controls.Count - 1; i++)
                {
                    cdvTemp = (Miracom.UI.Controls.MCCodeView.MCCodeView)controls[i];
                    cdvTemp.Init();

                    cdvTemp.ReadOnly = true;
                    cdvTemp.VisibleButton = true;
                    cdvTemp.Visible = false;

                    CmnInitFunction.InitListView(cdvTemp.GetListView);
                    cdvTemp.Columns.Add("Value", 100, HorizontalAlignment.Left);
                    cdvTemp.Columns.Add("Desc", 200, HorizontalAlignment.Left);
                    cdvTemp.SelectedSubItemIndex = 0;
                }

            }
            catch (Exception ex)
            {
               CmnFunction.ShowMsgBox("modCommonFunction.InitGRPCMFControl()" + "\r\n" + ex.Message, "FMB Client", MessageBoxButtons.OK, 1);
            }

        }

        // DeleteResource()
        //       - Delete Resource
        // Return Value
        //       - Boolean : True / False
        // Arguments
        //       - ByVal sCategory As String        :  Category
        //       - ByVal sFactory As String         :  Factory
        //       - ByVal sLayout As String          :  Layout or Group
        //       - ByVal sResource As String        :  Resource
        //       - ByVal sType As String            :  Resource or Tag Type
        //
        public static bool DeleteResource(string sCategory, string sFactory, string sLayout, string sResource, char sType)
        {

            try
            {
                FMB_Update_ResLoc_In_Tag Update_ResLoc_In = new FMB_Update_ResLoc_In_Tag();
                FMB_Update_UDR_ResLoc_In_Tag Update_UDR_ResLoc_In = new FMB_Update_UDR_ResLoc_In_Tag();
                FMB_Cmn_Out_Tag Cmn_Out = new FMB_Cmn_Out_Tag();

                if (sCategory == modGlobalConstant.FMB_CATEGORY_LAYOUT)
                {
                    Update_ResLoc_In._C.h_factory = sFactory;
                    Update_ResLoc_In._C.h_language = GlobalVariable.gcLanguage;
                    Update_ResLoc_In._C.h_user_id = GlobalVariable.gsUserID;
                    Update_ResLoc_In._C.h_proc_step = modGlobalConstant.MP_STEP_DELETE;
                    Update_ResLoc_In._C.h_passport = GlobalVariable.gsPassport;
                    Update_ResLoc_In._C.h_password = GlobalVariable.gsPassword;
                    Update_ResLoc_In._C.res_id = sResource;
                    Update_ResLoc_In._C.res_type = sType;
                    Update_ResLoc_In._C.layout_id = sLayout;


                    if (FMBSender.FMB_Update_Resource_Location(Update_ResLoc_In, ref Cmn_Out) == false)
                    {
                        CmnFunction.ShowMsgBox(h101stub.StatusMessage);
                        return false;
                    }
                  
                    if (Cmn_Out.h_status_value != modGlobalConstant.MP_SUCCESS_STATUS)
                    {
                       CmnFunction.ShowMsgBox(CmnFunction.MakeErrorMsg(Cmn_Out.h_msg_code, Cmn_Out.h_msg, Cmn_Out.h_db_err_msg, Cmn_Out.h_field_msg), "FMB Client", MessageBoxButtons.OK, 1);
                        return false;
                    }
                }
                else if (sCategory == modGlobalConstant.FMB_CATEGORY_GROUP)
                {
                    Update_UDR_ResLoc_In._C.h_factory = GlobalVariable.gsFactory;
                    Update_UDR_ResLoc_In._C.h_language = GlobalVariable.gcLanguage;
                    Update_UDR_ResLoc_In._C.h_user_id = GlobalVariable.gsUserID;
                    Update_UDR_ResLoc_In._C.h_proc_step = modGlobalConstant.MP_STEP_DELETE;
                    Update_UDR_ResLoc_In._C.h_passport = GlobalVariable.gsPassport;
                    Update_UDR_ResLoc_In._C.h_password = GlobalVariable.gsPassword;
                    Update_UDR_ResLoc_In._C.group = sLayout;
                    Update_UDR_ResLoc_In._C.res_id = sResource;
                    Update_UDR_ResLoc_In._C.res_type = sType;


                    if (FMBSender.FMB_Update_UDR_ResLoc(Update_UDR_ResLoc_In, ref Cmn_Out) == false)
                    {
                        CmnFunction.ShowMsgBox(h101stub.StatusMessage);
                        return false;
                    }
                 
                    if (Cmn_Out.h_status_value != modGlobalConstant.MP_SUCCESS_STATUS)
                    {
                       CmnFunction.ShowMsgBox(CmnFunction.MakeErrorMsg(Cmn_Out.h_msg_code, Cmn_Out.h_msg, Cmn_Out.h_db_err_msg, Cmn_Out.h_field_msg), "FMB Client", MessageBoxButtons.OK, 1);
                        return false;
                    }

                }

                return true;

            }
            catch (Exception ex)
            {
               CmnFunction.ShowMsgBox("modCommonFunction.DeleteResource()" + "\r\n" + ex.Message, "FMB Client", MessageBoxButtons.OK, 1);
                return false;
            }

        }

        // FindTreeNode()
        //       - Find Tree Node
        // Return Value
        //       - TreeNode
        // Arguments
        //       - ByRef trvCtrl As TreeView : TreeView
        //       - ByRef nodeParent As TreeNode : Parent Node
        //       - ByVal sKey As String : Key to Find
        //
        public static TreeNode FindTreeNode(TreeView trvCtrl, TreeNode nodeParent, string sKey, bool bSubTreeSearch)
        {

            try
            {
                //int i = 0;
                TreeNode nodeCurrent = null;
                TreeNode nodeFinded = null;
                if (nodeParent == null)
                {
                    foreach (TreeNode tempLoopVar_nodeCurrent in trvCtrl.Nodes)
                    {
                        nodeCurrent = tempLoopVar_nodeCurrent;
                        if (((clsDesignListTag)nodeCurrent.Tag).Key == sKey)
                        {
                            return nodeCurrent;
                        }
                        else
                        {
                            if (bSubTreeSearch == true)
                            {
                                nodeFinded = FindTreeNode(trvCtrl, nodeCurrent, sKey, true);
                                if (!(nodeFinded == null))
                                {
                                    return nodeFinded;
                                }
                            }
                        }
                    }
                }
                else
                {
                    foreach (TreeNode tempLoopVar_nodeCurrent in nodeParent.Nodes)
                    {
                        nodeCurrent = tempLoopVar_nodeCurrent;
                        if (((clsDesignListTag)nodeCurrent.Tag).Key == sKey)
                        {
                            return nodeCurrent;
                        }
                        else
                        {
                            if (bSubTreeSearch == true)
                            {
                                nodeFinded = FindTreeNode(trvCtrl, nodeCurrent, sKey, true);
                                if (!(nodeFinded == null))
                                {
                                    return nodeFinded;
                                }
                            }
                        }
                    }
                }

                return null;

            }
            catch (Exception ex)
            {
               CmnFunction.ShowMsgBox("modCommonFunction.FindTreeNode()" + "\r\n" + ex.Message, "FMB Client", MessageBoxButtons.OK, 1);
                return null;
            }

        }

        // GetFactoryNode()
        //       - Get Factory Tree Node
        // Return Value
        //       - TreeNode
        // Arguments
        //       - ByRef trvParent As TreeView : TreeView
        //       - ByVal sFactory As String : Factory to Find
        //
        public static TreeNode GetFactoryNode(ref TreeView trvParent, string sFactory)
        {

            try
            {
                int i = 0;
                for (i = 0; i <= trvParent.GetNodeCount(false) - 1; i++)
                {
                    if (((clsDesignListTag)trvParent.Nodes[i].Tag).Key == sFactory)
                    {
                        return trvParent.Nodes[i];
                    }
                }

                return null;

            }
            catch (Exception ex)
            {
               CmnFunction.ShowMsgBox("modCommonFunction.GetFactoryNode()" + "\r\n" + ex.Message, "FMB Client", MessageBoxButtons.OK, 1);
                return null;
            }

        }

        // ToAsc()
        //       - String to Ascii
        // Return Value
        //       - String
        // Arguments
        //       - ByVal sText As String
        //
        public static string ToAsc(string sText)
        {

            try
            {
                string sOutputText = "";
                int i;
                for (i = 0; i <= sText.Length - 1; i++)
                {
                    sOutputText += Microsoft.VisualBasic.Strings.Asc(sText[i]);
                    if (i < sText.Length - 1)
                    {
                        sOutputText += "|";
                    }
                }

                return sOutputText;

            }
            catch (Exception ex)
            {
               CmnFunction.ShowMsgBox("frmFMBDesign.ToAsc()" + "\r\n" + ex.Message, "FMB Client", MessageBoxButtons.OK, 1);
                return null;
            }

        }

        // ToChar()
        //       - String to Character
        // Return Value
        //       - String
        // Arguments
        //       - ByVal sText As String
        //
        public static string ToChar(string sText)
        {

            try
            {
                string[] sTextArray;
                sTextArray = sText.Split('|');
                string sOutputText = "";
                int i = 0;
                for (i = 0; i <= sTextArray.Length - 1; i++)
                {
                    sOutputText += Microsoft.VisualBasic.Strings.Chr(CmnFunction.ToInt(sTextArray[i]));
                }

                return sOutputText;

            }
            catch (Exception ex)
            {
               CmnFunction.ShowMsgBox("frmFMBDesign.ToChar()" + "\r\n" + ex.Message, "FMB Client", MessageBoxButtons.OK, 1);
                return null;
            }

        }

        // GetTextboxStyle()
        //       - Set Textbox Style
        // Return Value
        //       - Boolean : True of False
        // Arguments
        //       - ByRef colControl As System.Windows.Forms.Control.ControlCollection
        //
        public static bool GetTextboxStyle(System.Windows.Forms.Control.ControlCollection colControl)
        {

            try
            {
                Control l_Control;

                l_Control = null;

                foreach (Control tempLoopVar_l_Control in colControl)
                {
                    l_Control = tempLoopVar_l_Control;
                    if (l_Control is Panel)
                    {
                        GetTextboxStyle(l_Control.Controls);
                    }
                    else if (l_Control is GroupBox)
                    {
                        GetTextboxStyle(l_Control.Controls);
                    }
                    else if (l_Control is TabControl)
                    {
                        GetTextboxStyle(l_Control.Controls);
                    }
                    else if (l_Control is TextBox)
                    {
                        if (GlobalVariable.gsStyleName == "FLAT")
                        {
                            ((TextBox)l_Control).BorderStyle = BorderStyle.FixedSingle;
                        }
                        else
                        {
                            ((TextBox)l_Control).BorderStyle = BorderStyle.Fixed3D;
                        }
                    }
                    else if (l_Control is UltraTextEditor)
                    {
                        if (GlobalVariable.gsStyleName == "FLAT")
                        {
                            ((UltraTextEditor)l_Control).UseFlatMode = Infragistics.Win.DefaultableBoolean.True;
                        }
                        else
                        {
                            ((UltraTextEditor)l_Control).UseFlatMode = Infragistics.Win.DefaultableBoolean.False;
                        }
                    }
                    else if (l_Control is Miracom.UI.Controls.MCCodeView.MCCodeView)
                    {
                        if (GlobalVariable.gsStyleName == "FLAT")
                        {
                            ((Miracom.UI.Controls.MCCodeView.MCCodeView)l_Control).StyleBorder = BorderStyle.FixedSingle;
                        }
                        else
                        {
                            ((Miracom.UI.Controls.MCCodeView.MCCodeView)l_Control).StyleBorder = BorderStyle.Fixed3D;
                        }
                    }
                    else if (l_Control is UltraCheckEditor)
                    {
                        if (GlobalVariable.gsStyleName == "FLAT")
                        {
                            ((UltraCheckEditor)l_Control).UseFlatMode = Infragistics.Win.DefaultableBoolean.True;
                        }
                        else
                        {
                            ((UltraCheckEditor)l_Control).UseFlatMode = Infragistics.Win.DefaultableBoolean.False;
                        }
                    }
                    else if (l_Control is UltraComboEditor)
                    {
                        if (GlobalVariable.gsStyleName == "FLAT")
                        {
                            ((UltraComboEditor)l_Control).UseFlatMode = Infragistics.Win.DefaultableBoolean.True;
                        }
                        else
                        {
                            ((UltraComboEditor)l_Control).UseFlatMode = Infragistics.Win.DefaultableBoolean.False;
                        }
                    }
                    else if (l_Control is UltraColorPicker)
                    {
                        if (GlobalVariable.gsStyleName == "FLAT")
                        {
                            ((UltraColorPicker)l_Control).UseFlatMode = Infragistics.Win.DefaultableBoolean.True;
                        }
                        else
                        {
                            ((UltraColorPicker)l_Control).UseFlatMode = Infragistics.Win.DefaultableBoolean.False;
                        }
                    }
                    else if (l_Control is FarPoint.Win.Spread.FpSpread)
                    {
                        if (GlobalVariable.gsStyleName == "FLAT")
                        {
                            ((FarPoint.Win.Spread.FpSpread)l_Control).BorderStyle = BorderStyle.FixedSingle;
                        }
                        else
                        {
                            ((FarPoint.Win.Spread.FpSpread)l_Control).BorderStyle = BorderStyle.Fixed3D;
                        }
                    }
                }

                return true;

            }
            catch (Exception ex)
            {
               CmnFunction.ShowMsgBox("modCommonFunction.GetTextboxStyle()" + "\r\n" + ex.Message, "FMB Client", MessageBoxButtons.OK, 1);
                return false;
            }

        }

        // GetHelpURL()
        //       - Get Help URL
        // Return Value
        //       - Boolean : True of False
        // Arguments
        //       -
        //
        public static bool GetHelpURL()
        {
            FMB_Get_HelpURL_In_Tag Get_HelpURL_In = new FMB_Get_HelpURL_In_Tag();
            FMB_Get_HelpURL_Out_Tag Get_HelpURL_Out = new FMB_Get_HelpURL_Out_Tag();
            string sHelpURL;

            try
            {
                Get_HelpURL_In.h_passport = GlobalVariable.gsPassport;
                Get_HelpURL_In.h_language = GlobalVariable.gcLanguage;
                Get_HelpURL_In.h_factory = GlobalVariable.gsFactory;
                Get_HelpURL_In.h_user_id = GlobalVariable.gsUserID;
                Get_HelpURL_In.h_password = GlobalVariable.gsPassword;
                Get_HelpURL_In.h_proc_step = '1';

                if (GlobalVariable.gfrmMDI.ActiveMdiChild == null)
                {
                    Microsoft.VisualBasic.Interaction.Shell("explorer.exe D:\\Document\\MESplusV4_Doc\\HTML\\FMB_UserManual_Word\\Output\\WebWorks Help 5.0\\FMB_UserManual_Word\\index.html", Microsoft.VisualBasic.AppWinStyle.MinimizedFocus , false, -1);
                    return false;
                }

                Get_HelpURL_In.func_name = System.Convert.ToString(GlobalVariable.gfrmMDI.ActiveMdiChild.Tag);

                if (FMBSender.FMB_Get_HelpURL(Get_HelpURL_In, ref Get_HelpURL_Out) == false)
                {
                    CmnFunction.ShowMsgBox(h101stub.StatusMessage);
                    return false;
                }
              
                if ((Get_HelpURL_Out.h_status_value != modGlobalConstant.MP_SUCCESS_STATUS) || (Get_HelpURL_Out.help_url == ""))
                {
                    Microsoft.VisualBasic.Interaction.Shell("explorer.exe D:\\Document\\MESplusV4_Doc\\HTML\\FMB_UserManual_Word\\Output\\WebWorks Help 5.0\\FMB_UserManual_Word\\index.html", Microsoft.VisualBasic.AppWinStyle.MinimizedFocus, false, -1);
                }
                else
                {
                    sHelpURL = "explorer.exe D:\\Document\\MESplusV4_Doc\\HTML\\FMB_UserManual_Word\\Output\\WebWorks Help 5.0\\FMB_UserManual_Word\\" + Get_HelpURL_Out.help_url;
                    Microsoft.VisualBasic.Interaction.Shell(sHelpURL, Microsoft.VisualBasic.AppWinStyle.MinimizedFocus, false, -1);
                }

            }
            catch (Exception ex)
            {
               CmnFunction.ShowMsgBox("modCommonFunction.Client_Upgrade()" + "\r\n" + ex.Message, "FMB Client", MessageBoxButtons.OK, 1);
                return false;
            }
            return true;
        }

        public struct FuncCtrlList
        {
            public string ctrlName;
            public string ctrlValue;
        }

        // CheckSecurityFormControl()
        //       - Check Security Form Control
        // Return Value
        //       - Boolean : True of False
        // Arguments
        //       - ByVal frm As Form
        //
        public static bool CheckSecurityFormControl(Form frm)
        {
            FMB_View_Function_Detail_In_Tag View_Function_Detail_In = new FMB_View_Function_Detail_In_Tag();
            FMB_View_Function_Detail_Out_Tag View_Function_Detail_Out = new FMB_View_Function_Detail_Out_Tag();
            FuncCtrlList[] CtrlList = new FuncCtrlList[10];

            try
            {
                if (CmnFunction.Trim(frm.Tag) == "")
                {
                    return true;
                }

                View_Function_Detail_In.h_proc_step = '1';
                View_Function_Detail_In.h_passport = GlobalVariable.gsPassport;
                View_Function_Detail_In.h_language = GlobalVariable.gcLanguage;
                View_Function_Detail_In.h_user_id = GlobalVariable.gsUserID;
                View_Function_Detail_In.h_password = GlobalVariable.gsPassword;
                View_Function_Detail_In.h_factory = GlobalVariable.gsFactory;
                View_Function_Detail_In.func_name = CmnFunction.Trim(frm.Tag);

                if (FMBSender.FMB_View_Function_Detail(View_Function_Detail_In, ref View_Function_Detail_Out) == false)
                {
                    CmnFunction.ShowMsgBox(h101stub.StatusMessage);
                    return false;
                }
        
                if (View_Function_Detail_Out.h_status_value != modGlobalConstant.MP_SUCCESS_STATUS)
                {
                   CmnFunction.ShowMsgBox(CmnFunction.MakeErrorMsg(View_Function_Detail_Out.h_msg_code, View_Function_Detail_Out.h_msg, View_Function_Detail_Out.h_db_err_msg, View_Function_Detail_Out.h_field_msg), "FMB Client", MessageBoxButtons.OK, 1);
                    return false;
                }

                CtrlList[0].ctrlName = string.IsNullOrEmpty(View_Function_Detail_Out.ctl_name_1) ? " " : View_Function_Detail_Out.ctl_name_1;
                CtrlList[0].ctrlValue = View_Function_Detail_Out.ctl_en_flag_1.ToString();
                CtrlList[1].ctrlName = string.IsNullOrEmpty(View_Function_Detail_Out.ctl_name_2) ? " " : View_Function_Detail_Out.ctl_name_2;
                CtrlList[1].ctrlValue = View_Function_Detail_Out.ctl_en_flag_2.ToString();
                CtrlList[2].ctrlName = string.IsNullOrEmpty(View_Function_Detail_Out.ctl_name_3) ? " " : View_Function_Detail_Out.ctl_name_3;
                CtrlList[2].ctrlValue = View_Function_Detail_Out.ctl_en_flag_3.ToString();
                CtrlList[3].ctrlName = string.IsNullOrEmpty(View_Function_Detail_Out.ctl_name_4) ? " " : View_Function_Detail_Out.ctl_name_4;
                CtrlList[3].ctrlValue = View_Function_Detail_Out.ctl_en_flag_4.ToString();
                CtrlList[4].ctrlName = string.IsNullOrEmpty(View_Function_Detail_Out.ctl_name_5) ? " " : View_Function_Detail_Out.ctl_name_5;
                CtrlList[4].ctrlValue = View_Function_Detail_Out.ctl_en_flag_5.ToString();
                CtrlList[5].ctrlName = string.IsNullOrEmpty(View_Function_Detail_Out.ctl_name_6) ? " " : View_Function_Detail_Out.ctl_name_6;
                CtrlList[5].ctrlValue = View_Function_Detail_Out.ctl_en_flag_6.ToString();
                CtrlList[6].ctrlName = string.IsNullOrEmpty(View_Function_Detail_Out.ctl_name_7) ? " " : View_Function_Detail_Out.ctl_name_7;
                CtrlList[6].ctrlValue = View_Function_Detail_Out.ctl_en_flag_7.ToString();
                CtrlList[7].ctrlName = string.IsNullOrEmpty(View_Function_Detail_Out.ctl_name_8) ? " " : View_Function_Detail_Out.ctl_name_8;
                CtrlList[7].ctrlValue = View_Function_Detail_Out.ctl_en_flag_8.ToString();
                CtrlList[8].ctrlName = string.IsNullOrEmpty(View_Function_Detail_Out.ctl_name_9) ? " " : View_Function_Detail_Out.ctl_name_9;
                CtrlList[8].ctrlValue = View_Function_Detail_Out.ctl_en_flag_9.ToString();
                CtrlList[9].ctrlName = string.IsNullOrEmpty(View_Function_Detail_Out.ctl_name_10) ? " " : View_Function_Detail_Out.ctl_name_10;
                CtrlList[9].ctrlValue = View_Function_Detail_Out.ctl_en_flag_10.ToString();
                CheckSecurityFormControlSub(frm, "BUTTON", CtrlList);

                CtrlList[0].ctrlName = string.IsNullOrEmpty(View_Function_Detail_Out.tab_name_1) ? " " : View_Function_Detail_Out.tab_name_1;
                CtrlList[0].ctrlValue = View_Function_Detail_Out.tab_ds_flag_1.ToString();
                CtrlList[1].ctrlName = string.IsNullOrEmpty(View_Function_Detail_Out.tab_name_2) ? " " : View_Function_Detail_Out.tab_name_2;
                CtrlList[1].ctrlValue = View_Function_Detail_Out.tab_ds_flag_2.ToString();
                CtrlList[2].ctrlName = string.IsNullOrEmpty(View_Function_Detail_Out.tab_name_3) ? " " : View_Function_Detail_Out.tab_name_3;
                CtrlList[2].ctrlValue = View_Function_Detail_Out.tab_ds_flag_3.ToString();
                CtrlList[3].ctrlName = string.IsNullOrEmpty(View_Function_Detail_Out.tab_name_4) ? " " : View_Function_Detail_Out.tab_name_4;
                CtrlList[3].ctrlValue = View_Function_Detail_Out.tab_ds_flag_4.ToString();
                CtrlList[4].ctrlName = string.IsNullOrEmpty(View_Function_Detail_Out.tab_name_5) ? " " : View_Function_Detail_Out.tab_name_5;
                CtrlList[4].ctrlValue = View_Function_Detail_Out.tab_ds_flag_5.ToString();
                CtrlList[5].ctrlName = string.IsNullOrEmpty(View_Function_Detail_Out.tab_name_6) ? " " : View_Function_Detail_Out.tab_name_6;
                CtrlList[5].ctrlValue = View_Function_Detail_Out.tab_ds_flag_6.ToString();
                CtrlList[6].ctrlName = string.IsNullOrEmpty(View_Function_Detail_Out.tab_name_7) ? " " : View_Function_Detail_Out.tab_name_7;
                CtrlList[6].ctrlValue = View_Function_Detail_Out.tab_ds_flag_7.ToString();
                CtrlList[7].ctrlName = string.IsNullOrEmpty(View_Function_Detail_Out.tab_name_8) ? " " : View_Function_Detail_Out.tab_name_8;
                CtrlList[7].ctrlValue = View_Function_Detail_Out.tab_ds_flag_8.ToString();
                CtrlList[8].ctrlName = string.IsNullOrEmpty(View_Function_Detail_Out.tab_name_9) ? " " : View_Function_Detail_Out.tab_name_9;
                CtrlList[8].ctrlValue = View_Function_Detail_Out.tab_ds_flag_9.ToString();
                CtrlList[9].ctrlName = string.IsNullOrEmpty(View_Function_Detail_Out.tab_name_10) ? " " : View_Function_Detail_Out.tab_name_10;
                CtrlList[9].ctrlValue = View_Function_Detail_Out.tab_ds_flag_10.ToString();
                //CheckSecurityFormControlSub(frm, "TABPAGE", CtrlList)

                CtrlList[0].ctrlName = string.IsNullOrEmpty(View_Function_Detail_Out.opt_name_1) ? " " : View_Function_Detail_Out.opt_name_1;
                CtrlList[0].ctrlValue = View_Function_Detail_Out.opt_value_1;
                CtrlList[1].ctrlName = string.IsNullOrEmpty(View_Function_Detail_Out.opt_name_2) ? " " : View_Function_Detail_Out.opt_name_2;
                CtrlList[1].ctrlValue = View_Function_Detail_Out.opt_value_2;
                CtrlList[2].ctrlName = string.IsNullOrEmpty(View_Function_Detail_Out.opt_name_3) ? " " : View_Function_Detail_Out.opt_name_3;
                CtrlList[2].ctrlValue = View_Function_Detail_Out.opt_value_3;
                CtrlList[3].ctrlName = string.IsNullOrEmpty(View_Function_Detail_Out.opt_name_4) ? " " : View_Function_Detail_Out.opt_name_4;
                CtrlList[3].ctrlValue = View_Function_Detail_Out.opt_value_4;
                CtrlList[4].ctrlName = string.IsNullOrEmpty(View_Function_Detail_Out.opt_name_5) ? " " : View_Function_Detail_Out.opt_name_5;
                CtrlList[4].ctrlValue = View_Function_Detail_Out.opt_value_5;
                CtrlList[5].ctrlName = string.IsNullOrEmpty(View_Function_Detail_Out.opt_name_6) ? " " : View_Function_Detail_Out.opt_name_6;
                CtrlList[5].ctrlValue = View_Function_Detail_Out.opt_value_6;
                CtrlList[6].ctrlName = string.IsNullOrEmpty(View_Function_Detail_Out.opt_name_7) ? " " : View_Function_Detail_Out.opt_name_7;
                CtrlList[6].ctrlValue = View_Function_Detail_Out.opt_value_7;
                CtrlList[7].ctrlName = string.IsNullOrEmpty(View_Function_Detail_Out.opt_name_8) ? " " : View_Function_Detail_Out.opt_name_8;
                CtrlList[7].ctrlValue = View_Function_Detail_Out.opt_value_8;
                CtrlList[8].ctrlName = string.IsNullOrEmpty(View_Function_Detail_Out.opt_name_9) ? " " : View_Function_Detail_Out.opt_name_9;
                CtrlList[8].ctrlValue = View_Function_Detail_Out.opt_value_9;
                CtrlList[9].ctrlName = string.IsNullOrEmpty(View_Function_Detail_Out.opt_name_10) ? " " : View_Function_Detail_Out.opt_name_10;
                CtrlList[9].ctrlValue = View_Function_Detail_Out.opt_value_10;
                //CheckSecurityFormControlSub(frm, "OPTION", CtrlList)

            }
            catch (Exception ex)
            {
               CmnFunction.ShowMsgBox("modCommonFunction.CheckSecurityFormControl()" + "\r\n" + ex.Message, "FMB Client", MessageBoxButtons.OK, 1);
                return false;
            }

            return true;

        }

        // CheckSecurityFormControlSub()
        //       - CheckS ecurity Form Control
        // Return Value
        //       - Boolean : True of False
        // Arguments
        //       - ByVal ctrl As Control
        //       - ByVal ctrlType As String, ByVal ParamArray ctrlList() As FuncCtrlList
        //
        private static void CheckSecurityFormControlSub(Control ctrl, string ctrlType, params FuncCtrlList[] ctrlList)
        {
            try
            {
                Control control;
                bool bFindControl;
                string ctrlValue = "";

                foreach (Control tempLoopVar_control in ctrl.Controls)
                {
                    control = tempLoopVar_control;

                    if (control is Panel || control is GroupBox || control is TabControl || control is TabPage)
                    {

                        CheckSecurityFormControlSub(control, ctrlType, ctrlList);

                    }
                    else
                    {
                        bFindControl = false;
                        switch (ctrlType)
                        {
                            case "BUTTON":

                                if (!(control is Button))
                                {
                                    goto NextControl;
                                }
                                break;
                            case "TABPAGE":

                                if (!(control is TabPage))
                                {
                                    goto NextControl;
                                }
                                break;
                            case "OPTION":

                                break;

                        }

                        if (control.Name.ToUpper() == ctrlList[0].ctrlName.ToUpper())
                        {
                            bFindControl = true;
                            ctrlValue = ctrlList[0].ctrlValue.ToUpper();
                        }
                        else if (control.Name.ToUpper() == ctrlList[1].ctrlName.ToUpper())
                        {
                            bFindControl = true;
                            ctrlValue = ctrlList[1].ctrlValue.ToUpper();
                        }
                        else if (control.Name.ToUpper() == ctrlList[2].ctrlName.ToUpper())
                        {
                            bFindControl = true;
                            ctrlValue = ctrlList[2].ctrlValue.ToUpper();
                        }
                        else if (control.Name.ToUpper() == ctrlList[3].ctrlName.ToUpper())
                        {
                            bFindControl = true;
                            ctrlValue = ctrlList[3].ctrlValue.ToUpper();
                        }
                        else if (control.Name.ToUpper() == ctrlList[4].ctrlName.ToUpper())
                        {
                            bFindControl = true;
                            ctrlValue = ctrlList[4].ctrlValue.ToUpper();
                        }
                        else if (control.Name.ToUpper() == ctrlList[5].ctrlName.ToUpper())
                        {
                            bFindControl = true;
                            ctrlValue = ctrlList[5].ctrlValue.ToUpper();
                        }
                        else if (control.Name.ToUpper() == ctrlList[6].ctrlName.ToUpper())
                        {
                            bFindControl = true;
                            ctrlValue = ctrlList[6].ctrlValue.ToUpper();
                        }
                        else if (control.Name.ToUpper() == ctrlList[7].ctrlName.ToUpper())
                        {
                            bFindControl = true;
                            ctrlValue = ctrlList[7].ctrlValue.ToUpper();
                        }
                        else if (control.Name.ToUpper() == ctrlList[8].ctrlName.ToUpper())
                        {
                            bFindControl = true;
                            ctrlValue = ctrlList[8].ctrlValue.ToUpper();
                        }
                        else if (control.Name.ToUpper() == ctrlList[9].ctrlName.ToUpper())
                        {
                            bFindControl = true;
                            ctrlValue = ctrlList[9].ctrlValue.ToUpper();
                        }

                        if (bFindControl == true)
                        {
                            switch (ctrlType)
                            {
                                case "BUTTON":

                                    if (ctrlValue == "Y")
                                    {
                                        control.Enabled = true;
                                    }
                                    else
                                    {
                                        control.Enabled = false;
                                    }
                                    break;
                                case "TABPAGE":

                                    break;

                                case "OPTION":

                                    break;

                            }
                        }
                    }
                NextControl:
                    1.GetHashCode(); //nop
                }
            }
            catch (Exception ex)
            {
               CmnFunction.ShowMsgBox("modCommonFunction.CheckSecurityFormControlSub()" + "\r\n" + ex.Message, "FMB Client", MessageBoxButtons.OK, 1);
            }

        }

        // CheckValue()
        //       - Check space of the specific control
        // Return Value
        //       - Boolean : True of False
        // Arguments
        //       -
        //
        public static bool CheckValue(Control Form_control, int _Step, bool Not_Confirm_Flag, bool Not_Focus_Flag, string message, string Cond1, string Cond2)
        {
            bool returnValue;

            try
            {
                returnValue = false;

                if (message == "")
                {
                    if (_Step == 1)
                    {
                        message = modLanguageFunction.GetMessage(27);
                    }
                    else if (_Step == 2 || _Step == 3)
                    {
                        message = modLanguageFunction.GetMessage(29);
                    }
                }

                if ((Form_control is TextBox) || (Form_control is Label) || (Form_control is UltraTextEditor))
                {
                    if (_Step == 1)
                    {
                        if (Form_control.Text != "")
                        {
                            returnValue = true;
                        }
                    }
                    else if (_Step == 2)
                    {
                        if (Microsoft.VisualBasic.Information.IsNumeric(Form_control.Text) == true)
                        {
                            returnValue = true;
                        }
                    }
                    else if (_Step == 3)
                    {
                        if (Microsoft.VisualBasic.Information.IsNumeric(Form_control.Text) == true)
                        {
                            if (Microsoft.VisualBasic.Strings.InStr(Form_control.Text, ".", 0) <= 0)
                            {
                                returnValue = true;
                            }
                        }
                    }
                }
                else if (Form_control is ComboBox)
                {
                    if (_Step == 1)
                    {
                        if (Form_control.Text != "")
                        {
                            returnValue = true;
                        }
                    }
                    else if (_Step == 2)
                    {
                        if (Microsoft.VisualBasic.Information.IsNumeric(Form_control.Text) == true)
                        {
                            returnValue = true;
                        }
                    }
                    else if (_Step == 3)
                    {
                        if (Microsoft.VisualBasic.Information.IsNumeric(Form_control.Text) == true)
                        {
                            if (Microsoft.VisualBasic.Strings.InStr(Form_control.Text, ".", 0) <= 0)
                            {
                                returnValue = true;
                            }
                        }
                    }
                }
                else if (Form_control is Miracom.UI.Controls.MCCodeView.MCCodeView)
                {
                    if (_Step == 1)
                    {
                        if (CmnFunction.Trim(((Miracom.UI.Controls.MCCodeView.MCCodeView)Form_control).Text) != "")
                        {
                            returnValue = true;
                        }
                    }
                    else if (_Step == 2)
                    {
                        if (Microsoft.VisualBasic.Information.IsNumeric(((Miracom.UI.Controls.MCCodeView.MCCodeView)Form_control).Text) == true)
                        {
                            returnValue = true;
                        }
                    }
                    else if (_Step == 3)
                    {
                        if (Microsoft.VisualBasic.Information.IsNumeric(((Miracom.UI.Controls.MCCodeView.MCCodeView)Form_control).Text) == true)
                        {
                            if (Microsoft.VisualBasic.Strings.InStr(((Miracom.UI.Controls.MCCodeView.MCCodeView)Form_control).Text, ".", 0) <= 0)
                            {
                                returnValue = true;
                            }
                        }
                    }
                }
                else if (Form_control is Infragistics.Win.UltraWinEditors.UltraNumericEditor)
                {
                    if (_Step == 1)
                    {
                        if (CmnFunction.Trim(((Infragistics.Win.UltraWinEditors.UltraNumericEditor)Form_control).Text) != "")
                        {
                            returnValue = true;
                        }
                    }
                    else if (_Step == 2)
                    {
                        if (Microsoft.VisualBasic.Information.IsNumeric(((Infragistics.Win.UltraWinEditors.UltraNumericEditor)Form_control).Text) == true)
                        {
                            returnValue = true;
                        }
                    }
                    else if (_Step == 3)
                    {
                        if (Microsoft.VisualBasic.Information.IsNumeric(((Infragistics.Win.UltraWinEditors.UltraNumericEditor)Form_control).Text) == true)
                        {
                            if (Microsoft.VisualBasic.Strings.InStr(((Infragistics.Win.UltraWinEditors.UltraNumericEditor)Form_control).Text, ".", 0) <= 0)
                            {
                                returnValue = true;
                            }
                        }
                    }
                }

                if (returnValue == false)
                {
                    if (Not_Confirm_Flag == false)
                    {
                       CmnFunction.ShowMsgBox(message, "FMB Client", MessageBoxButtons.OK, 1);
                        if (Not_Focus_Flag == false)
                        {
                            if (Form_control.Visible == true && Form_control.Enabled == true)
                            {
                                Form_control.Focus();
                            }
                        }
                    }
                    return returnValue;
                }

                return returnValue;

            }
            catch (Exception ex)
            {
               CmnFunction.ShowMsgBox("modCommonFunction.CheckValue()" + "\r\n" + ex.Message, "FMB Client", MessageBoxButtons.OK, 1);
                return false;
            }
        }

        // CheckMaxLength()
        //       - check byte text length in TextBox
        // Return Value
        //       - boolean : True / False
        // Arguments
        //       - ByVal txt As Object			: TextBox Control
        //		- ByVal iMaxLength As Integer	: Max byte length
        //
        public static bool CheckMaxLength(object txt, int iMaxLength)
        {

            try
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
                }

                if (iMaxLength == 0)
                {
                    return false;
                }

                iByteLen = ByteLen(sText);
                if (iByteLen > iMaxLength)
                {
                    if (txt is System.Windows.Forms.TextBox)
                    {
                        ((System.Windows.Forms.TextBox)txt).Text = ByteMid(sText, 0, iMaxLength);
                        ((System.Windows.Forms.TextBox)txt).SelectionStart = sText.Length;
                    }

                    return false;
                }

                return true;

            }
            catch (Exception ex)
            {
               CmnFunction.ShowMsgBox("modCommonFunction.CheckMaxLength()" + "\r\n" + ex.Message, "FMB Client", MessageBoxButtons.OK, 1);
                return false;
            }

        }

        // ByteLen()
        //       - Get byte length in String
        // Return Value
        //       - Integer : byte length
        // Arguments
        //       - ByVal sStr As String : String
        //
        public static int ByteLen(string sStr)
        {

            try
            {
               // System.Text.Encoding encoding;
                System.Text.Encoding twobyte;

                twobyte = System.Text.Encoding.GetEncoding(949);
                return twobyte.GetByteCount(sStr);

            }
            catch (Exception ex)
            {
               CmnFunction.ShowMsgBox("modCommonFunction.ByteLen()" + "\r\n" + ex.Message, "FMB Client", MessageBoxButtons.OK, 1);
                return 0;
            }

        }

        // ByteMid()
        //       - Get Middle String at byte length in String
        // Return Value
        //       - String : Middle String
        // Arguments
        //       - ByVal sStr As String			: Source String
        //		- ByVal iStartIndex As Integer	: Start Index
        //		- ByVal iLength As Integer		: Middle String byte length
        //
        public static string ByteMid(string sStr, int iStartIndex, int iLength)
        {

            try
            {
               // System.Text.Encoding encoding;
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
            catch (Exception ex)
            {
               CmnFunction.ShowMsgBox("modCommonFunction.ByteLen()" + "\r\n" + ex.Message, "FMB Client", MessageBoxButtons.OK, 1);
                return "";
            }

        }

        // UpdateResourceLocation()
        //       - Create/Update/Delete Resource Location
        // Return Value
        //       - Boolean : Return True/False
        // Arguments
        //       - sStep as String  : Proc Step
        //       - ByVal sFactory As String
        //       - ByVal sLayoutID As String
        //       - ByVal sTagID As String
        //       - ByVal sResType As String
        //       - ByVal sText As String
        //       - ByVal iTagType As Integer
        //       - ByVal iTextSize As Integer
        //       - ByVal sTextStyle As String
        //       - ByVal iTextColor As Integer
        //       - ByVal iBackColor As Integer
        //       - ByVal iLocX As Integer
        //       - ByVal iLocY As Integer
        //       - ByVal iLocWidth As Integer
        //       - ByVal iLocHeight As Integer
        //       - ByVal sNoMouseEvent As String

        public static bool UpdateResourceLocation(char sStep, string sFactory, string sLayoutID, string sTagID, char sResType, string sText, int iTagType, int iTextSize, char sTextStyle, int iTextColor, int iBackColor, int iLocX, int iLocY, int iLocWidth, int iLocHeight, char sNoMouseEvent, char sSignalFlag)
        {

            try
            {
                FMB_Update_ResLoc_In_Tag Update_ResLoc_In = new FMB_Update_ResLoc_In_Tag();
                FMB_Cmn_Out_Tag Cmn_Out = new FMB_Cmn_Out_Tag();

                Update_ResLoc_In._C.h_factory = sFactory;
                Update_ResLoc_In._C.h_language = GlobalVariable.gcLanguage;
                Update_ResLoc_In._C.h_user_id = GlobalVariable.gsUserID;
                Update_ResLoc_In._C.h_proc_step = sStep;
                Update_ResLoc_In._C.h_passport = GlobalVariable.gsPassport;
                Update_ResLoc_In._C.h_password = GlobalVariable.gsPassword;

                Update_ResLoc_In._C.res_id = sTagID;
                Update_ResLoc_In._C.res_type = sResType;
                Update_ResLoc_In._C.text = sText;
                Update_ResLoc_In._C.tag_type = iTagType;
                Update_ResLoc_In._C.text_color = iTextColor;
                Update_ResLoc_In._C.text_size = iTextSize;
                Update_ResLoc_In._C.text_style = sTextStyle;
                Update_ResLoc_In._C.back_color = iBackColor;
                Update_ResLoc_In._C.layout_id = sLayoutID;
                Update_ResLoc_In._C.loc_x = iLocX;
                Update_ResLoc_In._C.loc_y = iLocY;
                Update_ResLoc_In._C.loc_width = iLocWidth;
                Update_ResLoc_In._C.loc_height = iLocHeight;
                Update_ResLoc_In._C.no_mouse_event = sNoMouseEvent;
                Update_ResLoc_In._C.signal_flag = sSignalFlag;

                if (FMBSender.FMB_Update_Resource_Location(Update_ResLoc_In, ref Cmn_Out) == false)
                {
                    CmnFunction.ShowMsgBox(h101stub.StatusMessage);
                    return false;
                }
               
                if (Cmn_Out.h_status_value != modGlobalConstant.MP_SUCCESS_STATUS)
                {
                   CmnFunction.ShowMsgBox(CmnFunction.MakeErrorMsg(Cmn_Out.h_msg_code, Cmn_Out.h_msg, Cmn_Out.h_db_err_msg, Cmn_Out.h_field_msg), "FMB Client", MessageBoxButtons.OK, 1);
                    return false;
                }

                return true;

            }
            catch (Exception ex)
            {
               CmnFunction.ShowMsgBox("modCommonFunction.UpdateResourceLocation()" + "\r\n" + ex.Message, "FMB Client", MessageBoxButtons.OK, 1);
                return false;
            }

        }

        // UpdateUDRResourceLocation()
        //       - Create/Update/Delete User Define Resource Information
        // Return Value
        //       - Boolean : Return True/False
        // Arguments
        //       - sStep as String    : Proc Step
        //       - ByVal sFactory As String
        //       - ByVal sLayoutID As String
        //       - ByVal sTagID As String
        //       - ByVal sResType As String
        //       - ByVal sText As String
        //       - ByVal iTagType As Integer
        //       - ByVal iTextSize As Integer
        //       - ByVal sTextStyle As String
        //       - ByVal iTextColor As Integer
        //       - ByVal iBackColor As Integer
        //       - ByVal iLocX As Integer
        //       - ByVal iLocY As Integer
        //       - ByVal iLocWidth As Integer
        //       - ByVal iLocHeight As Integer
        //       - ByVal sNoMouseEvent As String

        public static bool UpdateUDRResourceLocation(char sStep, string sFactory, string sLayoutID, string sTagID, char sResType, string sText, int iTagType, int iTextSize, char sTextStyle, int iTextColor, int iBackColor, int iLocX, int iLocY, int iLocWidth, int iLocHeight, char sNoMouseEvent, char sSignalFlag)
        {

            try
            {
                FMB_Update_UDR_ResLoc_In_Tag Update_UDR_ResLoc_In = new FMB_Update_UDR_ResLoc_In_Tag();
                FMB_Cmn_Out_Tag Cmn_Out = new FMB_Cmn_Out_Tag();

                Update_UDR_ResLoc_In._C.h_factory = GlobalVariable.gsFactory;
                Update_UDR_ResLoc_In._C.h_language = GlobalVariable.gcLanguage;
                Update_UDR_ResLoc_In._C.h_user_id = GlobalVariable.gsUserID;
                Update_UDR_ResLoc_In._C.h_proc_step = sStep;
                Update_UDR_ResLoc_In._C.h_passport = GlobalVariable.gsPassport;
                Update_UDR_ResLoc_In._C.h_password = GlobalVariable.gsPassword;
                Update_UDR_ResLoc_In._C.group = sLayoutID;

                Update_UDR_ResLoc_In._C.res_id = sTagID;
                Update_UDR_ResLoc_In._C.res_type = sResType;
                Update_UDR_ResLoc_In._C.text = sText;
                Update_UDR_ResLoc_In._C.text_color = iTextColor;
                Update_UDR_ResLoc_In._C.tag_type = iTagType;

                Update_UDR_ResLoc_In._C.text_size = iTextSize;
                Update_UDR_ResLoc_In._C.text_style = sTextStyle;

                Update_UDR_ResLoc_In._C.back_color = iBackColor;
                Update_UDR_ResLoc_In._C.loc_x = iLocX;
                Update_UDR_ResLoc_In._C.loc_y = iLocY;

                Update_UDR_ResLoc_In._C.loc_width = iLocWidth;
                Update_UDR_ResLoc_In._C.loc_height = iLocHeight;
                Update_UDR_ResLoc_In._C.no_mouse_event = sNoMouseEvent;
                Update_UDR_ResLoc_In._C.signal_flag = sSignalFlag;

                if (FMBSender.FMB_Update_UDR_ResLoc(Update_UDR_ResLoc_In, ref Cmn_Out) == false)
                {
                    CmnFunction.ShowMsgBox(h101stub.StatusMessage);
                    return false;
                }
             
                if (Cmn_Out.h_status_value != modGlobalConstant.MP_SUCCESS_STATUS)
                {
                   CmnFunction.ShowMsgBox(CmnFunction.MakeErrorMsg(Cmn_Out.h_msg_code, Cmn_Out.h_msg, Cmn_Out.h_db_err_msg, Cmn_Out.h_field_msg), "FMB Client", MessageBoxButtons.OK, 1);
                    return false;
                }

                return true;

            }
            catch (Exception ex)
            {
               CmnFunction.ShowMsgBox("modCommonFunction.UpdateUDRResourceLocation()" + "\r\n" + ex.Message, "FMB Client", MessageBoxButtons.OK, 1);
                return false;
            }

        }

        // ViewLayOut()
        //       - View Layout Information
        // Return Value
        //       - Boolean : Return True/False
        // Arguments
        //       - ByVal sFactory As String
        //       - ByVal sLayoutID As String
        //       - ByRef iWidth As Integer
        //       - ByRef iHeight As Integer
        //
        public static bool ViewLayOut(string sFactory, string sLayoutID, ref int iWidth, ref int iHeight)
        {

            try
            {
                FMB_View_LayOut_In_Tag View_LayOut_In = new FMB_View_LayOut_In_Tag();
                FMB_View_LayOut_Out_Tag View_LayOut_Out = new FMB_View_LayOut_Out_Tag();

                View_LayOut_In.h_passport = GlobalVariable.gsPassport;
                View_LayOut_In.h_language = GlobalVariable.gcLanguage;
                View_LayOut_In.h_factory = sFactory;
                View_LayOut_In.h_user_id = GlobalVariable.gsUserID;
                View_LayOut_In.h_password = GlobalVariable.gsPassword;
                View_LayOut_In.h_proc_step = '1';
                View_LayOut_In.layout_id = sLayoutID;

                if (FMBSender.FMB_View_LayOut(View_LayOut_In, ref View_LayOut_Out) == false)
                {
                    CmnFunction.ShowMsgBox(h101stub.StatusMessage);
                    return false;
                }
                
                if (View_LayOut_Out.h_status_value != modGlobalConstant.MP_SUCCESS_STATUS)
                {
                   CmnFunction.ShowMsgBox(CmnFunction.MakeErrorMsg(View_LayOut_Out.h_msg_code, View_LayOut_Out.h_msg, View_LayOut_Out.h_db_err_msg, View_LayOut_Out.h_field_msg), "FMB Client", MessageBoxButtons.OK, 1);
                    return false;
                }

                iWidth = View_LayOut_Out.width;
                iHeight = View_LayOut_Out.height;

                return true;

            }
            catch (Exception ex)
            {
               CmnFunction.ShowMsgBox("modCommonFunction.ViewLayOut()" + "\r\n" + ex.Message, "FMB Client", MessageBoxButtons.OK, 1);
                return false;
            }

        }

        // ViewUDRGroup()
        //       - View User Define Resource Group Information
        // Return Value
        //       - Boolean : Return True/False
        // Arguments
        //       - ByVal sLayoutID As String
        //       - ByRef iWidth As Integer
        //       - ByRef iHeight As Integer
        //
        public static bool ViewUDRGroup(string sLayoutID, ref int iWidth, ref int iHeight)
        {

            try
            {
                FMB_View_UDR_Group_In_Tag View_UDR_Group_In = new FMB_View_UDR_Group_In_Tag();
                FMB_View_UDR_Group_Out_Tag View_UDR_Group_Out = new FMB_View_UDR_Group_Out_Tag();

                View_UDR_Group_In.h_passport = GlobalVariable.gsPassport;
                View_UDR_Group_In.h_language = GlobalVariable.gcLanguage;
                View_UDR_Group_In.h_factory = GlobalVariable.gsFactory;
                View_UDR_Group_In.h_user_id = GlobalVariable.gsUserID;
                View_UDR_Group_In.h_password = GlobalVariable.gsPassword;
                View_UDR_Group_In.h_proc_step = '1';
                View_UDR_Group_In.group_id = sLayoutID;

                if (FMBSender.FMB_View_UDR_Group(View_UDR_Group_In, ref View_UDR_Group_Out) == false)
                {
                    CmnFunction.ShowMsgBox(h101stub.StatusMessage);
                    return false;
                }
               
                if (View_UDR_Group_Out.h_status_value != modGlobalConstant.MP_SUCCESS_STATUS)
                {
                   CmnFunction.ShowMsgBox(CmnFunction.MakeErrorMsg(View_UDR_Group_Out.h_msg_code, View_UDR_Group_Out.h_msg, View_UDR_Group_Out.h_db_err_msg, View_UDR_Group_Out.h_field_msg), "FMB Client", MessageBoxButtons.OK, 1);
                    return false;
                }

                iWidth = View_UDR_Group_Out.width;
                iHeight = View_UDR_Group_Out.height;

                return true;

            }
            catch (Exception ex)
            {
               CmnFunction.ShowMsgBox("modCommonFunction.ViewUDRGroup()" + "\r\n" + ex.Message, "FMB Client", MessageBoxButtons.OK, 1);
                return false;
            }

        }

        // ConvertColorToString()
        //       - Convert Color to String
        // Return Value
        //       - String
        // Arguments
        //       - ByVal crSource As Color
        //
        public static string ConvertColorToString(Color crSource)
        {

            try
            {
                string sDest = "";

                sDest = "(";
                sDest += int.Parse(crSource.R.ToString()).ToString();
                sDest += ",";
                sDest += int.Parse(crSource.G.ToString()).ToString();
                sDest += ",";
                sDest += int.Parse(crSource.B.ToString()).ToString();
                sDest += ")";

                return sDest;

            }
            catch (Exception ex)
            {
               CmnFunction.ShowMsgBox("modCommonFunction.ConvertColorToString()" + "\r\n" + ex.Message, "FMB Client", MessageBoxButtons.OK, 1);
                return string.Empty;
            }

        }

        // ConvertStringToColor()
        //       - Convert String To Color
        // Return Value
        //       - Integer
        // Arguments
        //       - ByVal sSource As String
        //
        public static int ConvertStringToColor(string sSource)
        {

            try
            {
                int iDestColor;
                string sTemp = sSource;
                string[] arrSource;
                sTemp = sTemp.Substring(1, sTemp.Length - 2);
                arrSource = sTemp.Split(',');
                iDestColor = Color.FromArgb(255, Convert.ToInt32(arrSource[0]), Convert.ToInt32(arrSource[1]), Convert.ToInt32(arrSource[2])).ToArgb();

                return iDestColor;

            }
            catch (Exception ex)
            {
               CmnFunction.ShowMsgBox("modCommonFunction.ConvertStringToColor()" + "\r\n" + ex.Message, "FMB Client", MessageBoxButtons.OK, 1);
                return Color.Empty.ToArgb();
            }

        }

        // ViewGlobalOption()
        //       - Set Global Options
        // Return Value
        //       - Boolean : True or False
        // Arguments
        //       - ByVal sFactory As String
        //
        public static bool ViewGlobalOption(string sFactory)
        {

            try
            {
                FMB_View_Environment_In_Tag View_Environment_In = new FMB_View_Environment_In_Tag();
                FMB_View_Environment_Out_Tag View_Environment_Out = new FMB_View_Environment_Out_Tag();
                Size cSize;

                View_Environment_In.h_proc_step = '1';
                View_Environment_In.h_passport = GlobalVariable.gsPassport;
                View_Environment_In.h_language = GlobalVariable.gcLanguage;
                View_Environment_In.h_user_id = GlobalVariable.gsUserID;
                View_Environment_In.h_password = GlobalVariable.gsPassword;
                View_Environment_In.h_factory = CmnFunction.RTrim(sFactory);


                if (FMBSender.FMB_View_Environment(View_Environment_In, ref View_Environment_Out) == false)
                {
                    CmnFunction.ShowMsgBox(h101stub.StatusMessage);
                    return false;
                }
               
                if (View_Environment_Out.h_status_value != modGlobalConstant.MP_SUCCESS_STATUS)
                {
                   CmnFunction.ShowMsgBox(CmnFunction.MakeErrorMsg(View_Environment_Out.h_msg_code, View_Environment_Out.h_msg, View_Environment_Out.h_db_err_msg, View_Environment_Out.h_field_msg), "FMB Client", MessageBoxButtons.OK, 1);
                    return false;
                }

                modGlobalVariable.gGlobalOptions.RemoveOptions(CmnFunction.RTrim(sFactory));
                modGlobalVariable.gGlobalOptions.AddOption(View_Environment_Out.factory, clsOptionData.Options.Factory, View_Environment_Out.factory);
                modGlobalVariable.gGlobalOptions.AddOption(View_Environment_Out.factory, clsOptionData.Options.DefaultFontName, View_Environment_Out.font_family);
                cSize = new Size(View_Environment_Out.layout_width, View_Environment_Out.layout_height);
                modGlobalVariable.gGlobalOptions.AddOption(View_Environment_Out.factory, clsOptionData.Options.DefaultLayoutSize, cSize);
                cSize = new Size(View_Environment_Out.udr_width, View_Environment_Out.udr_height);
                modGlobalVariable.gGlobalOptions.AddOption(View_Environment_Out.factory, clsOptionData.Options.DefaultUDRSize, cSize);
                cSize = new Size(View_Environment_Out.res_width, View_Environment_Out.res_height);
                modGlobalVariable.gGlobalOptions.AddOption(View_Environment_Out.factory, clsOptionData.Options.DefaultResourceSize, cSize);
                cSize = new Size(View_Environment_Out.rtg_width, View_Environment_Out.rtg_height);
                modGlobalVariable.gGlobalOptions.AddOption(View_Environment_Out.factory, clsOptionData.Options.DefaultRectangleSize, cSize);
                cSize = new Size(View_Environment_Out.elp_width, View_Environment_Out.elp_height);
                modGlobalVariable.gGlobalOptions.AddOption(View_Environment_Out.factory, clsOptionData.Options.DefaultEllipseSize, cSize);
                cSize = new Size(View_Environment_Out.tri_width, View_Environment_Out.tri_height);
                modGlobalVariable.gGlobalOptions.AddOption(View_Environment_Out.factory, clsOptionData.Options.DefaultTriangleSize, cSize);
                cSize = new Size(View_Environment_Out.ver_width, View_Environment_Out.ver_height);
                modGlobalVariable.gGlobalOptions.AddOption(View_Environment_Out.factory, clsOptionData.Options.DefaultVerticalLineSize, cSize);
                cSize = new Size(View_Environment_Out.hor_width, View_Environment_Out.hor_height);
                modGlobalVariable.gGlobalOptions.AddOption(View_Environment_Out.factory, clsOptionData.Options.DefaultHorizontalLineSize, cSize);
                cSize = new Size(View_Environment_Out.pie1_width, View_Environment_Out.pie1_height);
                modGlobalVariable.gGlobalOptions.AddOption(View_Environment_Out.factory, clsOptionData.Options.DefaultPie1Size, cSize);
                cSize = new Size(View_Environment_Out.pie2_width, View_Environment_Out.pie2_height);
                modGlobalVariable.gGlobalOptions.AddOption(View_Environment_Out.factory, clsOptionData.Options.DefaultPie2Size, cSize);
                cSize = new Size(View_Environment_Out.pie3_width, View_Environment_Out.pie3_height);
                modGlobalVariable.gGlobalOptions.AddOption(View_Environment_Out.factory, clsOptionData.Options.DefaultPie3Size, cSize);
                cSize = new Size(View_Environment_Out.pie4_width, View_Environment_Out.pie4_height);
                modGlobalVariable.gGlobalOptions.AddOption(View_Environment_Out.factory, clsOptionData.Options.DefaultPie4Size, cSize);
                modGlobalVariable.gGlobalOptions.AddOption(View_Environment_Out.factory, clsOptionData.Options.UseEventColor, View_Environment_Out.event_color_flag);
                modGlobalVariable.gGlobalOptions.AddOption(View_Environment_Out.factory, clsOptionData.Options.IsProcessMode, View_Environment_Out.signal_flag);
                if (CmnFunction.RTrim(View_Environment_Out.event_color_flag) == "Y")
                {
                    if (modListRoutine.ViewEventColorList('1', View_Environment_Out.factory) == false)
                    {
                        return false;
                    }
                }
                if (View_Environment_Out.text_color < 0)
                {
                    modGlobalVariable.gGlobalOptions.AddOption(View_Environment_Out.factory, clsOptionData.Options.DefaultTextColor, System.Drawing.Color.FromArgb(View_Environment_Out.text_color));
                }
                else if (View_Environment_Out.text_color > 0)
                {
                    modGlobalVariable.gGlobalOptions.AddOption(View_Environment_Out.factory, clsOptionData.Options.DefaultTextColor, System.Drawing.Color.FromKnownColor((KnownColor)View_Environment_Out.text_color));
                }
                else
                {
                    modGlobalVariable.gGlobalOptions.AddOption(View_Environment_Out.factory, clsOptionData.Options.DefaultTextColor, SystemColors.Control);
                }
                if (View_Environment_Out.back_color < 0)
                {
                    modGlobalVariable.gGlobalOptions.AddOption(View_Environment_Out.factory, clsOptionData.Options.DefaultBackColor, System.Drawing.Color.FromArgb(View_Environment_Out.back_color));
                }
                else if (View_Environment_Out.back_color > 0)
                {
                    modGlobalVariable.gGlobalOptions.AddOption(View_Environment_Out.factory, clsOptionData.Options.DefaultBackColor, System.Drawing.Color.FromKnownColor((KnownColor)View_Environment_Out.back_color));
                }
                else
                {
                    modGlobalVariable.gGlobalOptions.AddOption(View_Environment_Out.factory, clsOptionData.Options.DefaultBackColor, SystemColors.Control);
                }
                modGlobalVariable.gGlobalOptions.AddOption(View_Environment_Out.factory, clsOptionData.Options.DefaultTextSize, View_Environment_Out.text_size);

                return true;

            }
            catch (Exception ex)
            {
               CmnFunction.ShowMsgBox("modCommonFunction.ViewGlobalOption()" + "\r\n" + ex.Message, "FMB Client", MessageBoxButtons.OK, 1);
                return false;
            }

        }

        // CheckAllFactoryOption()
        //       - Set All Factory Option
        // Return Value
        //       -
        // Arguments
        //       - ByRef ctrlFactory As Control
        //
        public static void CheckAllFactoryOption(Control ctrlFactory)
        {

            try
            {
                if (modGlobalVariable.gbAllFactory == false)
                {
                    if (ctrlFactory is Miracom.UI.Controls.MCCodeView.MCCodeView)
                    {
                        ((Miracom.UI.Controls.MCCodeView.MCCodeView)ctrlFactory).Text = GlobalVariable.gsFactory;
                        ((Miracom.UI.Controls.MCCodeView.MCCodeView)ctrlFactory).VisibleButton = false;
                        ((Miracom.UI.Controls.MCCodeView.MCCodeView)ctrlFactory).ReadOnly = true;
                        ((Miracom.UI.Controls.MCCodeView.MCCodeView)ctrlFactory).BackColor = SystemColors.Control;
                    }
                    else if (ctrlFactory is UltraTextEditor)
                    {
                        ((UltraTextEditor)ctrlFactory).Text = GlobalVariable.gsFactory;
                        ((UltraTextEditor)ctrlFactory).ReadOnly = true;
                    }
                }
                else
                {
                    if (ctrlFactory is Miracom.UI.Controls.MCCodeView.MCCodeView)
                    {
                        ((Miracom.UI.Controls.MCCodeView.MCCodeView)ctrlFactory).VisibleButton = true;
                        ((Miracom.UI.Controls.MCCodeView.MCCodeView)ctrlFactory).ReadOnly = false;
                        ((Miracom.UI.Controls.MCCodeView.MCCodeView)ctrlFactory).BackColor = SystemColors.Window;
                    }
                    else if (ctrlFactory is UltraTextEditor)
                    {
                        ((UltraTextEditor)ctrlFactory).ReadOnly = false;
                    }
                }

            }
            catch (Exception ex)
            {
               CmnFunction.ShowMsgBox("frmFMBCreateLayOut.AllFactory()" + "\r\n" + ex.Message, "FMB Client", MessageBoxButtons.OK, 1);
            }

        }

       // ChangeStringToDate()
        //       -   Change 14 byte DateTime format of DateTimePicker
        // Return Value
        //       - String : 14 byte DateTime
        // Arguments
        //       - ByVal dtpTime As DateTimePicker
        public static DateTime ToDate(string str)
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

                if (CmnFunction.CheckNumeric(sTime) == true)
                {
                    if (sTime.Length >= 8)
                    {
                        year = CmnFunction.ToInt(sTime.Substring(0, 4));
                        month = CmnFunction.ToInt(sTime.Substring(4, 2));
                        day = CmnFunction.ToInt(sTime.Substring(6, 2));
                    }
                    if (sTime.Length >= 14)
                    {
                        hour = CmnFunction.ToInt(sTime.Substring(8, 2));
                        minute = CmnFunction.ToInt(sTime.Substring(10, 2));
                        second = CmnFunction.ToInt(sTime.Substring(12, 2));
                    }

                    DTime = new DateTime(year, month, day, hour, minute, second);
                }
                else
                {
                    DTime = DateTime.Now;
                }
                
                return DTime;


            }
            catch (Exception ex)
            {
               CmnFunction.ShowMsgBox("MPCF.ToDate()\n" + ex.Message);
                return DateTime.Now;
            }
        }

        // ToDate()
        //       -   Change 14 byte DateTime format of DateTimePicker
        // Return Value
        //       - String : 14 byte DateTime
        // Arguments
        //       - ByVal dtpTime As DateTimePicker
        public static string ToDate(DateTimePicker dtpTime)
        {
            string sDateTime = "";
            DateTime DTime;

            DTime = dtpTime.Value;

            if (GlobalVariable.gShiftInfor.bVariableShift == false)
            {
                if (GlobalVariable.gShiftInfor.cShift1DayFlag == 'T')
                {
                    DTime = DTime.AddDays(+1);
                }
            }
            else
            {
                DTime = DTime.AddDays(+1);
            }
            sDateTime = DTime.ToString("yyyyMMdd");
            sDateTime = sDateTime + GlobalVariable.gShiftInfor.sShift1StartTime;

            return sDateTime;
        }

        public static bool LoadEnviroment()
        {

            try
            {
                if (modLanguageFunction.LoadMessageResource() == false)
                {
                    return false;
                }
                if (modLanguageFunction.LoadCaptionResource() == false)
                {
                    return false;
                }

                //Get Client Options
                if (GetClientOptions() == false)
                {
                    return false;
                }

                return true;

            }
            catch (Exception ex)
            {
                CmnFunction.ShowMsgBox("clsClientInit.LoadEnviroment()" + "\r\n" + ex.Message, "FMB Client", MessageBoxButtons.OK, 1);
                return false;
            }

        }

        //
        // GetClientOptions()
        //       - Get Client Options
        // Return Value
        //       - Boolean : True or False
        // Arguments
        //       -
        //
        public static bool GetClientOptions()
        {
            //RegistryKey reg = Registry.CurrentUser.OpenSubKey("Software").OpenSubKey(
            try
            {
                Application.DoEvents();

                GlobalVariable.gsComputerName = System.Net.Dns.GetHostName();

                GlobalVariable.gsServerName = Microsoft.VisualBasic.Interaction.GetSetting(Application.ProductName, "Option", "Server", "MESServer");
                GlobalVariable.giLogOutTime = CmnFunction.ToInt(Microsoft.VisualBasic.Interaction.GetSetting(Application.ProductName, "Option", "LogOutTime", "20"));

                GlobalVariable.gsFactory = Microsoft.VisualBasic.Interaction.GetSetting(Application.ProductName, "Settings", "Factory", "");
                GlobalVariable.gsUserID = Microsoft.VisualBasic.Interaction.GetSetting(Application.ProductName, "Settings", "UserName", "");
                GlobalVariable.gsSiteID = Microsoft.VisualBasic.Interaction.GetSetting(Application.ProductName, "Option", "SiteID", "");
                GlobalVariable.gsRemoteAddress = Microsoft.VisualBasic.Interaction.GetSetting(Application.ProductName, "Option", "RemoteAddress", "localhost");
                GlobalVariable.giTimeOut = CmnFunction.ToInt(Microsoft.VisualBasic.Interaction.GetSetting(Application.ProductName, "Option", "TimeOut", "20"));

                //GlobalVariable.gcLanguage = MPCF.GetRegSetting(Application.ProductName, "Option", "Language", "1")[0];
                //if (Microsoft.VisualBasic.Interaction.GetSetting(Application.ProductName, "Option", "Grid", "2") == "1")
                //{
                //    GlobalVariable.gbGridFlag = true;
                //}
                //else
                //{
                //    GlobalVariable.gbGridFlag = false;
                //}

                //if (Microsoft.VisualBasic.Interaction.GetSetting(Application.ProductName, "Option", "Message", "1") == "1")
                //{
                //    GlobalVariable.gbShowMsgFlag = true;
                //}
                //else
                //{
                //    GlobalVariable.gbShowMsgFlag = false;
                //}

                //if (Microsoft.VisualBasic.Interaction.GetSetting(Application.ProductName, "Option", "Style", "1") == "1")
                //{
                //    GlobalVariable.gsStyleName = "FLAT";
                //}
                //else
                //{
                //    GlobalVariable.gsStyleName = "3D";
                //}

                //if (Microsoft.VisualBasic.Interaction.GetSetting(Application.ProductName, "Option", "AutoRefresh", "2") == "1")
                //{
                //    GlobalVariable.gbAutoRefresh = true;
                //    GlobalVariable.gbListAutoRefresh = true;
                //    GlobalVariable.giAutoRefreshTime = CmnFunction.ToInt(Microsoft.VisualBasic.Interaction.GetSetting(Application.ProductName, "Option", "AutoRefreshTime", "300"));
                //}
                //else
                //{
                //    GlobalVariable.gbAutoRefresh = false;
                //    GlobalVariable.gbListAutoRefresh = false;
                //    GlobalVariable.giAutoRefreshTime = 0;
                //}

                //if (Microsoft.VisualBasic.Interaction.GetSetting(Application.ProductName, "Option", "AllFactory", "1") == "1")
                //{
                //    modGlobalVariable.gbAllFactory = true;
                //}
                //else
                //{
                //    modGlobalVariable.gbAllFactory = false;
                //}

                return true;

            }
            catch (Exception ex)
            {
                CmnFunction.ShowMsgBox("clsClientInit.GetClientOptions()" + "\r\n" + ex.Message, "FMB Client", MessageBoxButtons.OK, 1);
                return false;
            }

        }

    }

}
