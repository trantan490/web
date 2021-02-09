
using System.Drawing;
using System.Data;
using System.Windows.Forms;
using System.Collections;
using System.Diagnostics;
using System;
using FarPoint.Win.Spread;
using Infragistics.Win.UltraWinEditors;
using com.miracom.transceiverx.session;
using Miracom.SmartWeb.FWX;
//using Miracom.H101Core;

//-----------------------------------------------------------------------------
//
//   System      : FMBClient
//   File Name   : modListRoutine.vb
//   Description : Common List Routine Module
//
//   FMB Version : 1.0.0
//
//   Function List
//		- ViewLayOutList()	: View LayOut List
//		- ViewUDR_GroupList()	: View User Define Resource Group List
//		- ViewFactoryList()	: View All Factory List
//		- ViewFacCmfData() : View FACCMF Table Item Data
//       - ViewResourceList() : View all Resource List
//       - ViewResEventList()  : View Resource - Event Relation List
//       - ViewResLotList() : View Lot List by Resource
//       - ViewResourceHistory()  : View Resource History
//       - ViewResOperList()  : View Resource - Operation Relation List
//       - ViewLotByResList()  : View Lot List by Resource
//		- ViewGCMDataList() : View General Code Table Data list
//		- ViewResourceListDetail() : View Reource List By Area/SubArea Condition
//		- ViewMaterialList() : View All Material List
//		- ViewFlowList() : View All Flow List
//		- ViewOperationList() : View All Operation List
//		- ViewLotListDetail() : View Lot List By Operation Condition
//       - ViewSecGroupList() : View All Security Group List
//       - ViewUserList() : View User List
//       - FindColSetVersion() : Find Collection Set Version
//       - ViewAttachCharacterList() : View Attach Character List
//       - ViewDefaultUnitList() : View Default Unit List
//       - ViewGlobalOptionList() : View Global Option List
//       - ViewEventColorList() : View Event Color List
//       - ViewResourceImageList() : View Resource Image List
//
//   Detail Description
//       -
//
//   History
//       - 2005-02-14 : Created by H.K.Kim
//
//
//   Copyright(C) 1998-2004 MIRACOM,INC.
//   All rights reserved.
//
//-----------------------------------------------------------------------------

namespace Miracom.SmartWeb.UI
{
    public sealed class modListRoutine
    {

        #region "Const Definition"

        private const int VALUE_START_COL = 11;

        #endregion

        #region "Enum Definition"

        private enum CHAR_COLUMN
        {
            CHAR_COL = 0,
            CHAR_DESC_COL = 1,
            SPEC_COL = 2,
            OPT_INPUT_COL = 3,
            VALUE_TYPE_COL = 4,
            VALUE_COUNT_COL = 5,
            DEF_UNIT_OVR_FLAG_COL = 6,
            DEF_VALUE_COL = 7,
            UNIT_TBL_COL = 8,
            VALUE_TBL_COL = 9,
            UNIT_COL = 10,
            VALUE_1_COL = 11,
            VALUE_2_COL = 12,
            VALUE_3_COL = 13,
            VALUE_4_COL = 14,
            VALUE_5_COL = 15,
            VALUE_6_COL = 16,
            VALUE_7_COL = 17,
            VALUE_8_COL = 18,
            VALUE_9_COL = 19,
            VALUE_10_COL = 20,
            VALUE_11_COL = 21,
            VALUE_12_COL = 22,
            VALUE_13_COL = 23,
            VALUE_14_COL = 24,
            VALUE_15_COL = 25,
            VALUE_16_COL = 26,
            VALUE_17_COL = 27,
            VALUE_18_COL = 28,
            VALUE_19_COL = 29,
            VALUE_20_COL = 30,
            VALUE_21_COL = 31,
            VALUE_22_COL = 32,
            VALUE_23_COL = 33,
            VALUE_24_COL = 34,
            VALUE_25_COL = 35,
            SPC_CHART_COL = 36
        }

        #endregion

        // ViewLayOutList()
        //       - View LayOut List
        // Return Value
        //       - Boolean : True / False
        // Arguments
        //       - ByVal control As Control					: List가 들어갈 control
        //		- ByVal sStep As String						: 확장 Process Step
        //		- Optional ByVal sFactory As String = ""    : 현재 Factory가 아닌경우의 Factory
        //
        public static bool ViewLayOutList(Control control, char sStep, string sFactory)
        {

            try
            {
                FMB_View_LayOut_List_In_Tag View_LayOut_List_In = new FMB_View_LayOut_List_In_Tag();
                FMB_View_LayOut_List_Out_Tag View_LayOut_List_Out = new FMB_View_LayOut_List_Out_Tag();

                int i;
                ListViewItem itmX;

                if (control is ListView)
                {
                    CmnInitFunction.InitListView((ListView)control);
                }

                View_LayOut_List_In.h_proc_step = sStep;
                View_LayOut_List_In.h_passport = GlobalVariable.gsPassport;
                View_LayOut_List_In.h_language = GlobalVariable.gcLanguage;
                View_LayOut_List_In.h_user_id = GlobalVariable.gsUserID;
                View_LayOut_List_In.h_password = GlobalVariable.gsPassword;
                if (sFactory != "")
                {
                    View_LayOut_List_In.h_factory = sFactory;
                }
                else
                {
                    View_LayOut_List_In.h_factory = GlobalVariable.gsFactory;
                }

                View_LayOut_List_In.next_layout_id = "";

                do
                {

                    if (FMBSender.FMB_View_LayOut_List(View_LayOut_List_In, ref View_LayOut_List_Out) == false)
                    {
                        CmnFunction.ShowMsgBox(h101stub.StatusMessage);
                        return false;
                    }
                   
                    if (View_LayOut_List_Out.h_status_value != modGlobalConstant.MP_SUCCESS_STATUS)
                    {
                        CmnFunction.ShowMsgBox(CmnFunction.MakeErrorMsg(View_LayOut_List_Out.h_msg_code, View_LayOut_List_Out.h_msg, View_LayOut_List_Out.h_db_err_msg, View_LayOut_List_Out.h_field_msg), "FMB Client", MessageBoxButtons.OK, 1);
                        return false;
                    }
                    for (i = 0; i <= View_LayOut_List_Out.count - 1; i++)
                    {

                        if (control is ListView)
                        {
                            itmX = new ListViewItem( CmnFunction.Trim(View_LayOut_List_Out.layout_list[i].layout_id), CmnFunction.ToInt(SMALLICON_INDEX.IDX_AREA));
                            if (((ListView)control).Columns.Count > 1)
                            {
                                itmX.SubItems.Add(CmnFunction.Trim(View_LayOut_List_Out.layout_list[i].layout_desc));
                                itmX.SubItems.Add(CmnFunction.Trim(View_LayOut_List_Out.layout_list[i].width));
                                itmX.SubItems.Add(CmnFunction.Trim(View_LayOut_List_Out.layout_list[i].height));
                            }
                            ((ListView)control).Items.Add(itmX);

                        }
                    }

                    View_LayOut_List_In.next_layout_id = View_LayOut_List_Out.next_layout_id;
                } while (string.IsNullOrEmpty(View_LayOut_List_Out.next_layout_id) == false);

                return true;

            }
            catch (Exception ex)
            {
                CmnFunction.ShowMsgBox("modListRoutine.ViewLayOutList()" + "\r\n" + ex.Message, "FMB Client", MessageBoxButtons.OK, 1);
                return false;
            }

        }

        // ViewUDR_GroupList()
        //       - View User Define Resource Group List
        // Return Value
        //       - Boolean : True / False
        // Arguments
        //       - ByVal control As Control					: List가 들어갈 control
        //		- ByVal sStep As String						: 확장 Process Step
        //
        public static bool ViewUDR_GroupList(Control control, char sStep)
        {

            try
            {
                FMB_View_UDR_Group_List_In_Tag View_UDR_Group_List_In = new FMB_View_UDR_Group_List_In_Tag();
                FMB_View_UDR_Group_List_Out_Tag View_UDR_Group_List_Out = new FMB_View_UDR_Group_List_Out_Tag();

                int i;
                ListViewItem itmX;

                if (control is ListView)
                {
                    CmnInitFunction.InitListView((ListView)control);
                }

                View_UDR_Group_List_In.h_proc_step = sStep;
                View_UDR_Group_List_In.h_passport = GlobalVariable.gsPassport;
                View_UDR_Group_List_In.h_language = GlobalVariable.gcLanguage;
                View_UDR_Group_List_In.h_user_id = GlobalVariable.gsUserID;
                View_UDR_Group_List_In.h_password = GlobalVariable.gsPassword;
                View_UDR_Group_List_In.h_factory = GlobalVariable.gsFactory;

                View_UDR_Group_List_In.next_group = "";

                do
                {

                    if (FMBSender.FMB_View_UDR_Group_List(View_UDR_Group_List_In, ref View_UDR_Group_List_Out) == false)
                    {
                        CmnFunction.ShowMsgBox(h101stub.StatusMessage);
                        return false;
                    }
                   
                    if (View_UDR_Group_List_Out.h_status_value != modGlobalConstant.MP_SUCCESS_STATUS)
                    {
                        CmnFunction.ShowMsgBox(CmnFunction.MakeErrorMsg(View_UDR_Group_List_Out.h_msg_code, View_UDR_Group_List_Out.h_msg, View_UDR_Group_List_Out.h_db_err_msg, View_UDR_Group_List_Out.h_field_msg), "FMB Client", MessageBoxButtons.OK, 1);
                        return false;
                    }
                    for (i = 0; i <= View_UDR_Group_List_Out.count - 1; i++)
                    {

                        if (control is ListView)
                        {
                            itmX = new ListViewItem(CmnFunction.Trim(View_UDR_Group_List_Out.udr_group_list[i].group), CmnFunction.ToInt(SMALLICON_INDEX.IDX_AREA));
                            if (((ListView)control).Columns.Count > 1)
                            {
                                itmX.SubItems.Add(CmnFunction.Trim(View_UDR_Group_List_Out.udr_group_list[i].group_desc));
                                itmX.SubItems.Add(CmnFunction.Trim(View_UDR_Group_List_Out.udr_group_list[i].width));
                                itmX.SubItems.Add(CmnFunction.Trim(View_UDR_Group_List_Out.udr_group_list[i].height));
                            }
                            ((ListView)control).Items.Add(itmX);

                        }
                    }

                    View_UDR_Group_List_In.next_group = View_UDR_Group_List_Out.next_group;
                } while (string.IsNullOrEmpty(View_UDR_Group_List_Out.next_group) == false);

                return true;

            }
            catch (Exception ex)
            {
                CmnFunction.ShowMsgBox("modListRoutine.ViewUDR_GroupList()" + "\r\n" + ex.Message, "FMB Client", MessageBoxButtons.OK, 1);
                return false;
            }

        }

      

        // ViewFacCmfData()
        //       - View FACCMF Table Item Data
        // Return Value
        //       - boolean : True / False
        // Arguments
        //		- ByVal sStep As String						: 확장 Process Step
        //		- ByVal sItemName As String					: 가져올 Item Name
        //		- ByRef View_FacCmf_Item_Out As FMB_View_FacCmf_Item_Out_Tag
        //													: 가져온 Item Data의 구조체
        //		- Optional ByVal sExtFactory As String = ""	: 현재 Factory가 아닌경우
        //
        public static bool ViewFacCmfData(char sStep, string sItemName, ref WIP_View_Factory_Cmf_Item_Out_Tag View_FacCmf_Item_Out, string sExtFactory, bool bShowError)
        {

            try
            {
                WIP_View_Factory_Cmf_Item_In_Tag View_FacCmf_Item_In = new WIP_View_Factory_Cmf_Item_In_Tag();

                View_FacCmf_Item_In.h_proc_step = sStep;
                View_FacCmf_Item_In.h_passport = GlobalVariable.gsPassport;
                View_FacCmf_Item_In.h_language = GlobalVariable.gcLanguage;
                if (sExtFactory == "")
                {
                    View_FacCmf_Item_In.h_factory = GlobalVariable.gsFactory;
                }
                else
                {
                    View_FacCmf_Item_In.h_factory = sExtFactory;
                }
                View_FacCmf_Item_In.h_user_id = GlobalVariable.gsUserID;
                View_FacCmf_Item_In.h_password = GlobalVariable.gsPassword;
                View_FacCmf_Item_In.item_name = sItemName;


                if (MESSender.WIP_View_Factory_Cmf_Item(View_FacCmf_Item_In, ref View_FacCmf_Item_Out) == false)
                {
                    if (bShowError == true)
                    {
                        CmnFunction.ShowMsgBox(h101stub.StatusMessage, "FMB Client", MessageBoxButtons.OK, 1);
                    }
                    return false;
                }
                if (View_FacCmf_Item_Out.h_status_value != modGlobalConstant.MP_SUCCESS_STATUS)
                {
                    if (bShowError == true)
                    {
                        CmnFunction.ShowMsgBox(CmnFunction.MakeErrorMsg(View_FacCmf_Item_Out.h_msg_code, View_FacCmf_Item_Out.h_msg, View_FacCmf_Item_Out.h_db_err_msg, View_FacCmf_Item_Out.h_field_msg), "FMB Client", MessageBoxButtons.OK, 1);
                    }
                    return false;
                }

                return true;

            }
            catch (Exception ex)
            {
                CmnFunction.ShowMsgBox("modListRoutine.ViewFacCmfData()" + "\r\n" + ex.Message, "FMB Client", MessageBoxButtons.OK, 1);
                return false;
            }

        }
  
        public static bool ViewResourceListDetail(Control control, string sRes_Type, string sRes_Grp, string sArea_ID, string sSub_Area_ID, string sFilter, bool bInclude_Delete_Resource, string sExtFactory, bool bIgnoreError)
        {

            try
            {
                FMB_View_ResDetail_In_Tag View_ResDetail_In = new FMB_View_ResDetail_In_Tag();
                FMB_View_ResDetail_Out_Tag View_ResDetail_Out = new FMB_View_ResDetail_Out_Tag();
                int i;
                ListViewItem itmX;

                if (control is ListView)
                {
                    CmnInitFunction.InitListView((ListView)control);
                }

                View_ResDetail_In.h_proc_step = '1';
                View_ResDetail_In.h_passport = GlobalVariable.gsPassport;
                View_ResDetail_In.h_language = GlobalVariable.gcLanguage;
                View_ResDetail_In.h_user_id = GlobalVariable.gsUserID;
                View_ResDetail_In.h_password = GlobalVariable.gsPassword;

                View_ResDetail_In.h_factory = sExtFactory;

                View_ResDetail_In.res_type = sRes_Type;
                View_ResDetail_In.res_grp = sRes_Grp;
                View_ResDetail_In.area_id = sArea_ID;
                View_ResDetail_In.sub_area_id = sSub_Area_ID;
                View_ResDetail_In.filter = sFilter;
                View_ResDetail_In.include_del_res = bInclude_Delete_Resource == true ? 'Y' : ' ';
                View_ResDetail_In.next_res_id = "";

                do
                {
                    if (FMBSender.FMB_View_ResDetail_List(View_ResDetail_In, ref View_ResDetail_Out) == false)
                    {
                        CmnFunction.ShowMsgBox(h101stub.StatusMessage);
                        return false;
                    }
    
                    if (View_ResDetail_Out.h_status_value != modGlobalConstant.MP_SUCCESS_STATUS)
                    {
                        CmnFunction.ShowMsgBox(CmnFunction.MakeErrorMsg(View_ResDetail_Out.h_msg_code, View_ResDetail_Out.h_msg, View_ResDetail_Out.h_db_err_msg, View_ResDetail_Out.h_field_msg), "FMB Client", MessageBoxButtons.OK, 1);
                        return false;
                    }

                    for (i = 0; i <= View_ResDetail_Out.count - 1; i++)
                    {
                        FMB_View_ResDetail_Out_Tag_res_list with_1 = View_ResDetail_Out.res_list[i];
                        if (control is ListView)
                        {
                            itmX = new ListViewItem(System.Convert.ToString(i + 1), CmnFunction.ToInt(SMALLICON_INDEX.IDX_EQUIP));

                            //itmX.SubItems.Add(Trim(.factory))
                            itmX.SubItems.Add(CmnFunction.Trim(with_1.res_id));
                            itmX.SubItems.Add(CmnFunction.Trim(with_1.res_desc));
                            itmX.SubItems.Add(CmnFunction.Trim(with_1.res_type));

                            if (CmnFunction.Trim(with_1.res_proc_mode).PadLeft(1, ' ').Substring(0, 1) == "M")
                            {
                                itmX.SubItems.Add("MANUAL");
                            }
                            else if (CmnFunction.Trim(with_1.res_proc_mode).PadLeft(1, ' ').Substring(0, 1) == "S")
                            {
                                itmX.SubItems.Add("SEMI AUTO");
                            }
                            else if (CmnFunction.Trim(with_1.res_proc_mode).PadLeft(1, ' ').Substring(0, 1) == "F")
                            {
                                itmX.SubItems.Add("FULL AUTO");
                            }
                            else
                            {
                                itmX.SubItems.Add(" ");
                            }

                            if (CmnFunction.Trim(with_1.res_ctrl_mode).PadLeft(2, ' ').Substring(0, 2) == "OL")
                            {
                                itmX.SubItems.Add("ON LINE");
                            }
                            else if (CmnFunction.Trim(with_1.res_ctrl_mode).PadLeft(2, ' ').Substring(0, 2) == "OR")
                            {
                                itmX.SubItems.Add("ON LINE REAL");
                            }
                            else if (CmnFunction.Trim(with_1.res_ctrl_mode).PadLeft(2, ' ').Substring(0, 2) == "OF")
                            {
                                itmX.SubItems.Add("OFF LINE");
                            }
                            else
                            {
                                itmX.SubItems.Add(" ");
                            }

                            if (CmnFunction.Trim(with_1.res_up_down_flag) == "U")
                            {
                                itmX.SubItems.Add("UP");
                            }
                            else
                            {
                                itmX.SubItems.Add("DOWN");
                            }

                            itmX.SubItems.Add(CmnFunction.Trim(with_1.res_pri_sts));
                            itmX.SubItems.Add(CmnFunction.Trim(with_1.res_sts_prt_1));
                            itmX.SubItems.Add(CmnFunction.Trim(with_1.res_sts_prt_2));
                            itmX.SubItems.Add(CmnFunction.Trim(with_1.res_sts_prt_3));
                            itmX.SubItems.Add(CmnFunction.Trim(with_1.res_sts_prt_4));
                            itmX.SubItems.Add(CmnFunction.Trim(with_1.res_sts_prt_5));
                            itmX.SubItems.Add(CmnFunction.Trim(with_1.res_sts_prt_6));
                            itmX.SubItems.Add(CmnFunction.Trim(with_1.res_sts_prt_7));
                            itmX.SubItems.Add(CmnFunction.Trim(with_1.res_sts_prt_8));
                            itmX.SubItems.Add(CmnFunction.Trim(with_1.res_sts_prt_9));
                            itmX.SubItems.Add(CmnFunction.Trim(with_1.res_sts_prt_10));
                            itmX.SubItems.Add(CmnFunction.Trim(with_1.res_sts_1));
                            itmX.SubItems.Add(CmnFunction.Trim(with_1.res_sts_2));
                            itmX.SubItems.Add(CmnFunction.Trim(with_1.res_sts_3));
                            itmX.SubItems.Add(CmnFunction.Trim(with_1.res_sts_4));
                            itmX.SubItems.Add(CmnFunction.Trim(with_1.res_sts_5));
                            itmX.SubItems.Add(CmnFunction.Trim(with_1.res_sts_6));
                            itmX.SubItems.Add(CmnFunction.Trim(with_1.res_sts_7));
                            itmX.SubItems.Add(CmnFunction.Trim(with_1.res_sts_8));
                            itmX.SubItems.Add(CmnFunction.Trim(with_1.res_sts_9));
                            itmX.SubItems.Add(CmnFunction.Trim(with_1.res_sts_10));
                            itmX.SubItems.Add(CmnFunction.Trim(with_1.use_fac_prt_flag));
                            itmX.SubItems.Add(CmnFunction.Trim(with_1.last_event_id));
                            itmX.SubItems.Add(CmnFunction.MakeDateFormat(CmnFunction.Trim(with_1.last_event_time), DATE_TIME_FORMAT.NONE));
                            itmX.SubItems.Add(CmnFunction.MakeDateFormat(CmnFunction.Trim(with_1.last_start_time), DATE_TIME_FORMAT.NONE));
                            itmX.SubItems.Add(CmnFunction.MakeDateFormat(CmnFunction.Trim(with_1.last_end_time), DATE_TIME_FORMAT.NONE));
                            itmX.SubItems.Add(CmnFunction.Trim(with_1.proc_count));
                            itmX.SubItems.Add(CmnFunction.Trim(with_1.max_proc_count));

                            //itmX.SubItems.Add(Trim(.res_grp_1))
                            //itmX.SubItems.Add(Trim(.res_grp_2))
                            //itmX.SubItems.Add(Trim(.res_grp_3))
                            //itmX.SubItems.Add(Trim(.res_grp_4))
                            //itmX.SubItems.Add(Trim(.res_grp_5))
                            //itmX.SubItems.Add(Trim(.res_grp_6))
                            //itmX.SubItems.Add(Trim(.res_grp_7))
                            //itmX.SubItems.Add(Trim(.res_grp_8))
                            //itmX.SubItems.Add(Trim(.res_grp_9))
                            //itmX.SubItems.Add(Trim(.res_grp_10))
                            //itmX.SubItems.Add(Trim(.res_cmf_1))
                            //itmX.SubItems.Add(Trim(.res_cmf_2))
                            //itmX.SubItems.Add(Trim(.res_cmf_3))
                            //itmX.SubItems.Add(Trim(.res_cmf_4))
                            //itmX.SubItems.Add(Trim(.res_cmf_5))
                            //itmX.SubItems.Add(Trim(.res_cmf_6))
                            //itmX.SubItems.Add(Trim(.res_cmf_7))
                            //itmX.SubItems.Add(Trim(.res_cmf_8))
                            //itmX.SubItems.Add(Trim(.res_cmf_9))
                            //itmX.SubItems.Add(Trim(.res_cmf_10))
                            //itmX.SubItems.Add(Trim(.area_id))
                            //itmX.SubItems.Add(Trim(.sub_area_id))
                            //itmX.SubItems.Add(Trim(.dsp_id))
                            //itmX.SubItems.Add(Trim(.res_location))
                            //itmX.SubItems.Add(Trim(.proc_rule))
                            //itmX.SubItems.Add(Trim(.batch_cond_1))
                            //itmX.SubItems.Add(Trim(.batch_cond_2))
                            //itmX.SubItems.Add(Trim(.pm_sch_enable_flag))
                            //itmX.SubItems.Add(Trim(.unit_base_st_flag))
                            //itmX.SubItems.Add(Trim(.sec_chk_flag))

                            //itmX.SubItems.Add(Trim(.last_recipe_id))
                            itmX.SubItems.Add(CmnFunction.Trim(with_1.last_active_hist_seq));
                            itmX.SubItems.Add(CmnFunction.Trim(with_1.last_hist_seq));
                            //itmX.SubItems.Add(Trim(.delete_flag))
                            //itmX.SubItems.Add(Trim(.delete_user_id))
                            //itmX.SubItems.Add(Trim(.delete_time))
                            itmX.SubItems.Add(CmnFunction.Trim(with_1.create_user_id));
                            itmX.SubItems.Add(CmnFunction.MakeDateFormat(CmnFunction.Trim(with_1.create_time), DATE_TIME_FORMAT.NONE));
                            itmX.SubItems.Add(CmnFunction.Trim(with_1.update_user_id));
                            itmX.SubItems.Add(CmnFunction.MakeDateFormat(CmnFunction.Trim(with_1.update_time), DATE_TIME_FORMAT.NONE));
                            itmX.SubItems.Add(CmnFunction.Trim(with_1.res_tag_flag));
                            itmX.SubItems.Add(CmnFunction.Trim(with_1.layout_id));
                            itmX.SubItems.Add(CmnFunction.Trim(with_1.loc_x));
                            itmX.SubItems.Add(CmnFunction.Trim(with_1.loc_y));
                            itmX.SubItems.Add(CmnFunction.Trim(with_1.loc_width));
                            itmX.SubItems.Add(CmnFunction.Trim(with_1.loc_height));
                            itmX.SubItems.Add(CmnFunction.Trim(with_1.text));
                            itmX.SubItems.Add(CmnFunction.Trim(with_1.text_size));
                            if (with_1.text_color < 0)
                            {
                                itmX.SubItems.Add(System.Drawing.Color.FromArgb(with_1.text_color).ToString());
                                itmX.SubItems[46].BackColor = System.Drawing.Color.FromArgb(with_1.text_color);
                            }
                            else if (with_1.text_color > 0)
                            {
                                itmX.SubItems.Add(System.Drawing.Color.FromKnownColor((KnownColor)with_1.text_color).ToString());
                                itmX.SubItems[46].BackColor = System.Drawing.Color.FromKnownColor((KnownColor)with_1.text_color);
                            }
                            else
                            {
                                itmX.SubItems.Add("0");
                            }

                            if (CmnFunction.Trim(with_1.text_style) != "")
                            {
                                itmX.SubItems.Add(@Enum.GetName(typeof(FontStyle), @Enum.GetValues(typeof(FontStyle)).GetValue(CmnFunction.ToInt(with_1.text_style.ToString()))));
                            }
                            if (with_1.back_color < 0)
                            {
                                itmX.SubItems.Add(System.Drawing.Color.FromArgb(with_1.back_color).ToString());
                                itmX.SubItems[48].BackColor = System.Drawing.Color.FromArgb(with_1.back_color);
                            }
                            else if (with_1.back_color > 0)
                            {
                                itmX.SubItems.Add(System.Drawing.Color.FromKnownColor((KnownColor)with_1.back_color).ToString());
                                itmX.SubItems[48].BackColor = System.Drawing.Color.FromKnownColor((KnownColor)with_1.back_color);
                            }
                            else
                            {
                                itmX.SubItems.Add("0");
                            }

                            if (with_1.res_up_down_flag == 'U')
                            {
                                itmX.ImageIndex = CmnFunction.ToInt(SMALLICON_INDEX.IDX_EQUIP);
                            }
                            else
                            {
                                itmX.ImageIndex = CmnFunction.ToInt(SMALLICON_INDEX.IDX_EQUIP_DOWN);
                            }
                            ((ListView)control).Items.Add(itmX);
                            if (CmnFunction.Trim(with_1.delete_flag) == "Y")
                            {
                                itmX.SubItems[0].ForeColor = Color.Magenta;
                            }
                        }

                    }

                    View_ResDetail_In.next_res_id = View_ResDetail_Out.next_res_id;
                } while (CmnFunction.Trim(View_ResDetail_In.next_res_id) != "");

                return true;

            }
            catch (Exception ex)
            {
                CmnFunction.ShowMsgBox("modListRoutine.ViewResourceListDetail()" + "\r\n" + ex.Message, "FMB Client", MessageBoxButtons.OK, 1);
                return false;
            }

        }

        // ViewGlobalOptionList()
        //       - View Global Option List
        // Return Value
        //       - Boolean : True or False
        // Arguments
        //       -
        public static bool ViewGlobalOptionList()
        {

            try
            {
                FMB_View_Environment_List_In_Tag View_Environment_List_In = new FMB_View_Environment_List_In_Tag();
                FMB_View_Environment_List_Out_Tag View_Environment_List_Out = new FMB_View_Environment_List_Out_Tag();
                int i;
                Size cSize;

                View_Environment_List_In.h_proc_step = '1';
                View_Environment_List_In.h_passport = GlobalVariable.gsPassport;
                View_Environment_List_In.h_language = GlobalVariable.gcLanguage;
                View_Environment_List_In.h_user_id = GlobalVariable.gsUserID;
                View_Environment_List_In.h_password = GlobalVariable.gsPassword;
                View_Environment_List_In.next_factory = "";

                do
                {
                    if (FMBSender.FMB_View_Environment_List(View_Environment_List_In, ref View_Environment_List_Out) == false)
                    {
                        CmnFunction.ShowMsgBox(h101stub.StatusMessage);
                        return false;
                    }
    
                    if (View_Environment_List_Out.h_status_value != modGlobalConstant.MP_SUCCESS_STATUS)
                    {
                        CmnFunction.ShowMsgBox(CmnFunction.MakeErrorMsg(View_Environment_List_Out.h_msg_code, View_Environment_List_Out.h_msg, View_Environment_List_Out.h_db_err_msg, View_Environment_List_Out.h_field_msg), "FMB Client", MessageBoxButtons.OK, 1);
                        return false;
                    }

                    for (i = 0; i <= View_Environment_List_Out.count - 1; i++)
                    {
                        modGlobalVariable.gGlobalOptions.AddOption(View_Environment_List_Out.evn_list[i].factory, clsOptionData.Options.Factory, View_Environment_List_Out.evn_list[i].factory);
                        modGlobalVariable.gGlobalOptions.AddOption(View_Environment_List_Out.evn_list[i].factory, clsOptionData.Options.DefaultFontName, View_Environment_List_Out.evn_list[i].font_family);
                        cSize = new Size(View_Environment_List_Out.evn_list[i].layout_width, View_Environment_List_Out.evn_list[i].layout_height);
                        modGlobalVariable.gGlobalOptions.AddOption(View_Environment_List_Out.evn_list[i].factory, clsOptionData.Options.DefaultLayoutSize, cSize);
                        cSize = new Size(View_Environment_List_Out.evn_list[i].udr_width, View_Environment_List_Out.evn_list[i].udr_height);
                        modGlobalVariable.gGlobalOptions.AddOption(View_Environment_List_Out.evn_list[i].factory, clsOptionData.Options.DefaultUDRSize, cSize);
                        cSize = new Size(View_Environment_List_Out.evn_list[i].res_width, View_Environment_List_Out.evn_list[i].res_height);
                        modGlobalVariable.gGlobalOptions.AddOption(View_Environment_List_Out.evn_list[i].factory, clsOptionData.Options.DefaultResourceSize, cSize);
                        cSize = new Size(View_Environment_List_Out.evn_list[i].rtg_width, View_Environment_List_Out.evn_list[i].rtg_height);
                        modGlobalVariable.gGlobalOptions.AddOption(View_Environment_List_Out.evn_list[i].factory, clsOptionData.Options.DefaultRectangleSize, cSize);
                        cSize = new Size(View_Environment_List_Out.evn_list[i].elp_width, View_Environment_List_Out.evn_list[i].elp_height);
                        modGlobalVariable.gGlobalOptions.AddOption(View_Environment_List_Out.evn_list[i].factory, clsOptionData.Options.DefaultEllipseSize, cSize);
                        cSize = new Size(View_Environment_List_Out.evn_list[i].tri_width, View_Environment_List_Out.evn_list[i].tri_height);
                        modGlobalVariable.gGlobalOptions.AddOption(View_Environment_List_Out.evn_list[i].factory, clsOptionData.Options.DefaultTriangleSize, cSize);
                        cSize = new Size(View_Environment_List_Out.evn_list[i].ver_width, View_Environment_List_Out.evn_list[i].ver_height);
                        modGlobalVariable.gGlobalOptions.AddOption(View_Environment_List_Out.evn_list[i].factory, clsOptionData.Options.DefaultVerticalLineSize, cSize);
                        cSize = new Size(View_Environment_List_Out.evn_list[i].hor_width, View_Environment_List_Out.evn_list[i].hor_height);
                        modGlobalVariable.gGlobalOptions.AddOption(View_Environment_List_Out.evn_list[i].factory, clsOptionData.Options.DefaultHorizontalLineSize, cSize);
                        cSize = new Size(View_Environment_List_Out.evn_list[i].pie1_width, View_Environment_List_Out.evn_list[i].pie1_height);
                        modGlobalVariable.gGlobalOptions.AddOption(View_Environment_List_Out.evn_list[i].factory, clsOptionData.Options.DefaultPie1Size, cSize);
                        cSize = new Size(View_Environment_List_Out.evn_list[i].pie2_width, View_Environment_List_Out.evn_list[i].pie2_height);
                        modGlobalVariable.gGlobalOptions.AddOption(View_Environment_List_Out.evn_list[i].factory, clsOptionData.Options.DefaultPie2Size, cSize);
                        cSize = new Size(View_Environment_List_Out.evn_list[i].pie3_width, View_Environment_List_Out.evn_list[i].pie3_height);
                        modGlobalVariable.gGlobalOptions.AddOption(View_Environment_List_Out.evn_list[i].factory, clsOptionData.Options.DefaultPie3Size, cSize);
                        cSize = new Size(View_Environment_List_Out.evn_list[i].pie4_width, View_Environment_List_Out.evn_list[i].pie4_height);
                        modGlobalVariable.gGlobalOptions.AddOption(View_Environment_List_Out.evn_list[i].factory, clsOptionData.Options.DefaultPie4Size, cSize);
                        modGlobalVariable.gGlobalOptions.AddOption(View_Environment_List_Out.evn_list[i].factory, clsOptionData.Options.UseEventColor, View_Environment_List_Out.evn_list[i].event_color_flag);
                        modGlobalVariable.gGlobalOptions.AddOption(View_Environment_List_Out.evn_list[i].factory, clsOptionData.Options.IsProcessMode, View_Environment_List_Out.evn_list[i].signal_flag);
                        if (CmnFunction.Trim(View_Environment_List_Out.evn_list[i].event_color_flag) == "Y")
                        {
                            if (ViewEventColorList('1', View_Environment_List_Out.evn_list[i].factory) == false)
                            {
                                return false;
                            }
                        }
                        if (View_Environment_List_Out.evn_list[i].text_color < 0)
                        {
                            modGlobalVariable.gGlobalOptions.AddOption(View_Environment_List_Out.evn_list[i].factory, clsOptionData.Options.DefaultTextColor, System.Drawing.Color.FromArgb(View_Environment_List_Out.evn_list[i].text_color));
                        }
                        else if (View_Environment_List_Out.evn_list[i].text_color > 0)
                        {
                            modGlobalVariable.gGlobalOptions.AddOption(View_Environment_List_Out.evn_list[i].factory, clsOptionData.Options.DefaultTextColor, System.Drawing.Color.FromKnownColor((KnownColor)View_Environment_List_Out.evn_list[i].text_color));
                        }
                        else
                        {
                            modGlobalVariable.gGlobalOptions.AddOption(View_Environment_List_Out.evn_list[i].factory, clsOptionData.Options.DefaultTextColor, SystemColors.Control);
                        }
                        if (View_Environment_List_Out.evn_list[i].back_color < 0)
                        {
                            modGlobalVariable.gGlobalOptions.AddOption(View_Environment_List_Out.evn_list[i].factory, clsOptionData.Options.DefaultBackColor, System.Drawing.Color.FromArgb(View_Environment_List_Out.evn_list[i].back_color));
                        }
                        else if (View_Environment_List_Out.evn_list[i].back_color > 0)
                        {
                            modGlobalVariable.gGlobalOptions.AddOption(View_Environment_List_Out.evn_list[i].factory, clsOptionData.Options.DefaultBackColor, System.Drawing.Color.FromKnownColor((KnownColor)View_Environment_List_Out.evn_list[i].back_color));
                        }
                        else
                        {
                            modGlobalVariable.gGlobalOptions.AddOption(View_Environment_List_Out.evn_list[i].factory, clsOptionData.Options.DefaultBackColor, SystemColors.Control);
                        }
                        modGlobalVariable.gGlobalOptions.AddOption(View_Environment_List_Out.evn_list[i].factory, clsOptionData.Options.DefaultTextSize, View_Environment_List_Out.evn_list[i].text_size);
                    }

                    View_Environment_List_In.next_factory = View_Environment_List_Out.next_factory;

                } while (CmnFunction.Trim(View_Environment_List_In.next_factory) != "");

                return true;

            }
            catch (Exception ex)
            {
                CmnFunction.ShowMsgBox("modListRoutine.ViewGlobalOptionList()" + "\r\n" + ex.Message, "FMB Client", MessageBoxButtons.OK, 1);
                return false;
            }

        }

        // ViewEventColorList()
        //       - View Event Color List
        // Return Value
        //       - boolean : True / False
        // Arguments
        //       - ByVal sStep As String       : Process Step
        //       - ByVal sFactory As String    : Factory
        //
        public static bool ViewEventColorList(char sStep, string sFactory)
        {

            int i;

            FMB_View_Event_Color_List_In_Tag View_Event_Color_List_In = new FMB_View_Event_Color_List_In_Tag();
            FMB_View_Event_Color_List_Out_Tag View_Event_Color_List_Out = new FMB_View_Event_Color_List_Out_Tag();

            try
            {
                View_Event_Color_List_In.h_proc_step = sStep;
                View_Event_Color_List_In.h_passport = GlobalVariable.gsPassport;
                View_Event_Color_List_In.h_language = GlobalVariable.gcLanguage;
                View_Event_Color_List_In.h_factory = CmnFunction.RTrim(sFactory);
                View_Event_Color_List_In.h_user_id = GlobalVariable.gsUserID;
                View_Event_Color_List_In.h_password = GlobalVariable.gsPassword;
                View_Event_Color_List_In.next_event = "";

                do
                {

                    if (FMBSender.FMB_View_Event_Color_List(View_Event_Color_List_In, ref View_Event_Color_List_Out) == false)
                    {
                        CmnFunction.ShowMsgBox(h101stub.StatusMessage);
                        return false;
                    }
                 
                    if (View_Event_Color_List_Out.h_status_value != modGlobalConstant.MP_SUCCESS_STATUS)
                    {
                        CmnFunction.ShowMsgBox(CmnFunction.MakeErrorMsg(View_Event_Color_List_Out.h_msg_code, View_Event_Color_List_Out.h_msg, View_Event_Color_List_Out.h_db_err_msg, View_Event_Color_List_Out.h_field_msg), "FMB Client", MessageBoxButtons.OK, 1);
                        return false;
                    }

                    for (i = 0; i <= View_Event_Color_List_Out.count - 1; i++)
                    {
                        if (View_Event_Color_List_Out.event_list[i].color < 0)
                        {
                            modGlobalVariable.gGlobalOptions.AddOption(sFactory, CmnFunction.Trim(View_Event_Color_List_Out.event_list[i].event_id), System.Drawing.Color.FromArgb(View_Event_Color_List_Out.event_list[i].color));
                        }
                        else if (View_Event_Color_List_Out.event_list[i].color > 0)
                        {
                            modGlobalVariable.gGlobalOptions.AddOption(sFactory, CmnFunction.Trim(View_Event_Color_List_Out.event_list[i].event_id), System.Drawing.Color.FromKnownColor((KnownColor)View_Event_Color_List_Out.event_list[i].color));
                        }
                        else
                        {
                            modGlobalVariable.gGlobalOptions.AddOption(sFactory, CmnFunction.Trim(View_Event_Color_List_Out.event_list[i].event_id), SystemColors.Control);
                        }

                    }

                    View_Event_Color_List_In.next_event = View_Event_Color_List_Out.next_event;

                } while (CmnFunction.Trim(View_Event_Color_List_Out.next_event) != "");

                return true;

            }
            catch (Exception ex)
            {
                CmnFunction.ShowMsgBox("modListRoutine.ViewEventColorList()" + "\r\n" + ex.Message, "FMB Client", MessageBoxButtons.OK, 1);
                return false;
            }

        }

        // ViewEventColorList()
        //       - View Event Color List
        // Return Value
        //       - boolean : True / False
        // Arguments
        //       - ByVal control As Control    : Control
        //       - ByVal sStep As String       : Process Step
        //       - ByVal sFactory As String    : Factory
        //
        public static bool ViewEventColorList(Control control, char sStep, string sFactory)
        {


            int i;
            ListViewItem itmX;

            FMB_View_Event_Color_List_In_Tag View_Event_Color_List_In = new FMB_View_Event_Color_List_In_Tag();
            FMB_View_Event_Color_List_Out_Tag View_Event_Color_List_Out = new FMB_View_Event_Color_List_Out_Tag();

            try
            {
                if (control is ListView)
                {
                    CmnInitFunction.InitListView((ListView)control);
                }

                View_Event_Color_List_In.h_proc_step = sStep;
                View_Event_Color_List_In.h_passport = GlobalVariable.gsPassport;
                View_Event_Color_List_In.h_language = GlobalVariable.gcLanguage;
                View_Event_Color_List_In.h_factory = CmnFunction.RTrim(sFactory);
                View_Event_Color_List_In.h_user_id = GlobalVariable.gsUserID;
                View_Event_Color_List_In.h_password = GlobalVariable.gsPassword;
                View_Event_Color_List_In.next_event = "";
                do
                {
                    if (FMBSender.FMB_View_Event_Color_List(View_Event_Color_List_In, ref View_Event_Color_List_Out) == false)
                    {
                        CmnFunction.ShowMsgBox(h101stub.StatusMessage);
                        return false;
                    }
    
                    if (View_Event_Color_List_Out.h_status_value != modGlobalConstant.MP_SUCCESS_STATUS)
                    {
                        CmnFunction.ShowMsgBox(CmnFunction.MakeErrorMsg(View_Event_Color_List_Out.h_msg_code, View_Event_Color_List_Out.h_msg, View_Event_Color_List_Out.h_db_err_msg, View_Event_Color_List_Out.h_field_msg), "FMB Client", MessageBoxButtons.OK, 1);
                        return false;
                    }

                    for (i = 0; i <= View_Event_Color_List_Out.count - 1; i++)
                    {
                        if (control is ListView)
                        {
                            itmX = new ListViewItem(CmnFunction.Trim(View_Event_Color_List_Out.event_list[i].event_id), 0);
                            if (((ListView)control).Columns.Count > 1)
                            {
                                if (View_Event_Color_List_Out.event_list[i].color < 0)
                                {
                                    itmX.SubItems.Add(System.Drawing.Color.FromArgb(View_Event_Color_List_Out.event_list[i].color).ToString());
                                    itmX.SubItems[0].BackColor = System.Drawing.Color.FromArgb(View_Event_Color_List_Out.event_list[i].color);
                                }
                                else if (View_Event_Color_List_Out.event_list[i].color > 0)
                                {
                                    itmX.SubItems.Add(System.Drawing.Color.FromKnownColor((KnownColor)View_Event_Color_List_Out.event_list[i].color).ToString());
                                    itmX.SubItems[0].BackColor = System.Drawing.Color.FromKnownColor((KnownColor)View_Event_Color_List_Out.event_list[i].color);
                                }
                                else
                                {
                                    itmX.SubItems.Add(SystemColors.Control.ToString());
                                    itmX.SubItems[0].BackColor = SystemColors.Control;
                                }

                            }
                            ((ListView)control).Items.Add(itmX);
                        }
                    }

                    View_Event_Color_List_In.next_event = View_Event_Color_List_Out.next_event;

                } while (CmnFunction.Trim(View_Event_Color_List_Out.next_event) != "");
                return true;

            }
            catch (Exception ex)
            {
                CmnFunction.ShowMsgBox("modListRoutine.ViewEventColorList()" + "\r\n" + ex.Message, "FMB Client", MessageBoxButtons.OK, 1);
                return false;
            }

        }

        // ViewResourceImageList()
        //       - View Resource Image List
        // Return Value
        //       - boolean : True / False
        // Arguments
        //       - ByVal control As Control    : Control
        //       - ByVal sStep As String       : Process Step
        //       - ByVal sFactory As String    : Factory
        //
        public static bool ViewResourceImageList(Control control, char sStep, string sFactory)
        {


            int i;
            ListViewItem itmX;

            FMB_View_Resource_Image_List_In_Tag View_Resource_Image_List_In = new FMB_View_Resource_Image_List_In_Tag();
            FMB_View_Resource_Image_List_Out_Tag View_Resource_Image_List_Out = new FMB_View_Resource_Image_List_Out_Tag();

            try
            {
                View_Resource_Image_List_In.h_proc_step = sStep;
                View_Resource_Image_List_In.h_passport = GlobalVariable.gsPassport;
                View_Resource_Image_List_In.h_language = GlobalVariable.gcLanguage;
                View_Resource_Image_List_In.h_factory = CmnFunction.RTrim(sFactory);
                View_Resource_Image_List_In.h_user_id = GlobalVariable.gsUserID;
                View_Resource_Image_List_In.h_password = GlobalVariable.gsPassword;
                View_Resource_Image_List_In.next_res_id = "";

                do
                {

                    if (FMBSender.FMB_View_Resource_Image_List(View_Resource_Image_List_In, ref View_Resource_Image_List_Out) == false)
                    {
                        CmnFunction.ShowMsgBox(h101stub.StatusMessage);
                        return false;
                    }
                  
                    if (View_Resource_Image_List_Out.h_status_value != modGlobalConstant.MP_SUCCESS_STATUS)
                    {
                        CmnFunction.ShowMsgBox(CmnFunction.MakeErrorMsg(View_Resource_Image_List_Out.h_msg_code, View_Resource_Image_List_Out.h_msg, View_Resource_Image_List_Out.h_db_err_msg, View_Resource_Image_List_Out.h_field_msg), "FMB Client", MessageBoxButtons.OK, 1);
                        return false;
                    }

                    for (i = 0; i <= View_Resource_Image_List_Out.count - 1; i++)
                    {
                        if (control is ListView)
                        {
                            if (View_Resource_Image_List_Out.res_list[i].img_idx == -1)
                            {
                                itmX = new ListViewItem("");
                            }
                            else
                            {
                                itmX = new ListViewItem("", View_Resource_Image_List_Out.res_list[i].img_idx);
                            }
                            if (((ListView)control).Columns.Count > 1)
                            {
                                itmX.SubItems.Add(View_Resource_Image_List_Out.res_list[i].res_id);
                                itmX.SubItems.Add(View_Resource_Image_List_Out.res_list[i].res_desc);
                            }
                            ((ListView)control).Items.Add(itmX);
                        }
                    }

                    View_Resource_Image_List_In.next_res_id = View_Resource_Image_List_Out.next_res_id;

                } while (CmnFunction.Trim(View_Resource_Image_List_Out.next_res_id) != "");

                return true;

            }
            catch (Exception ex)
            {
                CmnFunction.ShowMsgBox("modListRoutine.ViewResourceImageList()" + "\r\n" + ex.Message, "FMB Client", MessageBoxButtons.OK, 1);
                return false;
            }

        }

        public static bool ViewFMBGroupList(Control control, char sStep, string sKey, string sExtFactory)
        {

            try
            {
                int i;
                ListViewItem itmX;
                
                FMB_View_FMB_Group_List_In_Tag View_FMB_Group_List_In = new FMB_View_FMB_Group_List_In_Tag();
                FMB_View_FMB_Group_List_Out_Tag View_FMB_Group_List_Out = new FMB_View_FMB_Group_List_Out_Tag();

                if (control is ListView)
                {
                    CmnInitFunction.InitListView((ListView)control);
                }

                View_FMB_Group_List_In.h_proc_step = sStep;
                View_FMB_Group_List_In.h_passport = GlobalVariable.gsPassport;
                View_FMB_Group_List_In.h_language = GlobalVariable.gcLanguage;
                View_FMB_Group_List_In.h_user_id = GlobalVariable.gsUserID;
                View_FMB_Group_List_In.h_password = GlobalVariable.gsPassword;
                if (sExtFactory == "")
                {
                    sExtFactory = GlobalVariable.gsFactory;
                }
                View_FMB_Group_List_In.h_factory = CmnFunction.Trim(sExtFactory);
                View_FMB_Group_List_In.next_group = "";
                View_FMB_Group_List_In.next_user = "";
                if (sStep == '1')
                {
                    View_FMB_Group_List_In.next_group = sKey;
                }
                else if (sStep == '2')
                {
                    View_FMB_Group_List_In.next_user = sKey;
                }

                do
                {
                    if (FMBSender.FMB_View_FMB_Group_List(View_FMB_Group_List_In, ref View_FMB_Group_List_Out) ==false)
                    {
                        CmnFunction.ShowMsgBox(h101stub.StatusMessage);
                        return false;
                    }

                    if ((View_FMB_Group_List_Out.h_status_value != GlobalConstant.MP_SUCCESS_STATUS))
                    {
                        CmnFunction.ShowMsgBox(CmnFunction.MakeErrorMsg(View_FMB_Group_List_Out.h_msg_code, View_FMB_Group_List_Out.h_msg, View_FMB_Group_List_Out.h_db_err_msg, View_FMB_Group_List_Out.h_field_msg));
                        return false;
                    }

                    for (i = 0; i <= View_FMB_Group_List_Out.count - 1; i++)
                    {
                        if (control is ListView)
                        {
                            if (sStep == '1')
                            {
                                itmX = new ListViewItem(CmnFunction.Trim(View_FMB_Group_List_Out.udr_group_list[i].user), CmnFunction.ToInt(SMALLICON_INDEX.IDX_USER));
                                if (((ListView)control).Columns.Count > 1)
                                {
                                    itmX.SubItems.Add(CmnFunction.Trim(View_FMB_Group_List_Out.udr_group_list[i].user_desc));
                                }
                                ((ListView)control).Items.Add(itmX);
                            }
                            else if (sStep == '2')
                            {
                                itmX = new ListViewItem(CmnFunction.Trim(View_FMB_Group_List_Out.udr_group_list[i].group), CmnFunction.ToInt(SMALLICON_INDEX.IDX_AREA));
                                if (((ListView)control).Columns.Count > 1)
                                {
                                    itmX.SubItems.Add(CmnFunction.Trim(View_FMB_Group_List_Out.udr_group_list[i].group_desc));
                                }
                                ((ListView)control).Items.Add(itmX);
                            }
                            
                        }

                    }
                    View_FMB_Group_List_In.next_user = View_FMB_Group_List_Out.next_user;
                    View_FMB_Group_List_In.next_group = View_FMB_Group_List_Out.next_group;
                }
                while (!((CmnFunction.Trim(View_FMB_Group_List_Out.next_user) == "" & CmnFunction.Trim(View_FMB_Group_List_Out.next_group) == "")));

                return true;
            }

            catch (Exception ex)
            {
                CmnFunction.ShowMsgBox("modListRoutine.ViewFMBGroupList()" + "\r\n" + ex.Message, "FMB Client", MessageBoxButtons.OK, 1);
                return false;
            }

        }

    }


}
