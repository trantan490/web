using System;
using System.Data;
using System.Data.OleDb;
using Miracom.SmartWeb.FWX;
using Miracom.SmartWeb;

namespace Miracom.SmartWeb.RO
{
    public partial class FMBTransferImp : FMBTransfer
    {
        public const int MAX_UDR_LIST = 1000;
        public override void FMB_View_UDR_Resource_List_Detail(FMB_View_UDR_Resource_List_In_Tag View_UDR_Resource_List_In, ref FMB_View_UDR_Resource_List_Out_Detail_Tag View_UDR_Resource_List_Out)
        {
            int i =0;
            DataTable adoDataTable = null;
            DataTable adoImgTable = null;

            DB_Common DBC = new DB_Common();
            DBC_FMBJOIN FMBJOIN = new DBC_FMBJOIN(ref DBC);
            DBC_MFMBUDRLOC MFMBUDRLOC = new DBC_MFMBUDRLOC(ref DBC);
            DBC_MFMBRESIMG MFMBRESIMG = new DBC_MFMBRESIMG(ref DBC);

            try
            {
                DBC.gDBConnectionString = StdGlobalVariable.ROXml.GetValue("//appSettings//add[@key='MESConnectionString']");
                
                if (DBC.OpenDB() != true)
                {
                    View_UDR_Resource_List_Out.h_db_err_msg = DBC.gErrors.ErrMsg;
                    return;
                }

                DBC.DB_BeginTransaction();

                View_UDR_Resource_List_Out.h_status_value = StdGlobalConstant.MP_FAIL;
                View_UDR_Resource_List_Out._size_udr_list = 0;
                View_UDR_Resource_List_Out.count = 0;
                View_UDR_Resource_List_Out.next_res_id = String.Empty;
                View_UDR_Resource_List_Out.next_seq = 0;

                /*Type 별 List*/
                if(View_UDR_Resource_List_In.h_proc_step == '1')
                {
                    if(View_UDR_Resource_List_In.res_type == 'R')
                    {
                        FMBJOIN.Init();
                        FMBJOIN.FACTORY = View_UDR_Resource_List_In.h_factory;
                        FMBJOIN.GROUP_ID = View_UDR_Resource_List_In.group;
                        FMBJOIN.SEQ = View_UDR_Resource_List_In.next_seq;
                        if (FMBJOIN.FillUdrDetail(1, ref adoDataTable) == false)
                        {
                            if (DBC.gErrors.SqlCode == SQL_CODE.SQL_NOT_FOUND)
                            {
                                View_UDR_Resource_List_Out.h_status_value = StdGlobalConstant.MP_SUCCESS;
                                return;
                            }
                            View_UDR_Resource_List_Out.h_msg = DBC.gErrors.SqlCode.ToString();
                            View_UDR_Resource_List_Out.h_db_err_msg = DBC.gErrors.ErrMsg;
                            DBC.DB_Rollback();
                            return;
                        }
                        if (adoDataTable.Rows.Count > 0)
                        {
                            View_UDR_Resource_List_Out.udr_list = new FMB_View_UDR_Resource_List_Out_Detail_Tag_udr_list[adoDataTable.Rows.Count];
                            for (i = 0; i < adoDataTable.Rows.Count; i++)
                            {
                                if(i == MAX_UDR_LIST)
		                        {
                                    View_UDR_Resource_List_Out.next_seq = Convert.ToInt32(adoDataTable.Rows[i]["SEQ"]);
                                    break;
		                        }
                                
                                MFMBRESIMG.Init();
                                MFMBRESIMG.FACTORY = View_UDR_Resource_List_In.h_factory;
                                MFMBRESIMG.RES_ID = adoDataTable.Rows[i]["RES_ID"].ToString();
                                MFMBRESIMG.SelectData(1, ref adoImgTable);

                                View_UDR_Resource_List_Out.udr_list[i] = new FMB_View_UDR_Resource_List_Out_Detail_Tag_udr_list();
                                View_UDR_Resource_List_Out.udr_list[i].res_id = adoDataTable.Rows[i]["RES_ID"].ToString();
                                View_UDR_Resource_List_Out.udr_list[i].res_tag_flag = adoDataTable.Rows[i]["RES_TAG_FLAG"].ToString()[0];
                                View_UDR_Resource_List_Out.udr_list[i].loc_x = Convert.ToInt32(adoDataTable.Rows[i]["LOC_X"]);
                                View_UDR_Resource_List_Out.udr_list[i].loc_y = Convert.ToInt32(adoDataTable.Rows[i]["LOC_Y"]);
                                View_UDR_Resource_List_Out.udr_list[i].loc_width = Convert.ToInt32(adoDataTable.Rows[i]["LOC_WIDTH"]);
                                View_UDR_Resource_List_Out.udr_list[i].loc_height = Convert.ToInt32(adoDataTable.Rows[i]["LOC_HEIGHT"]);
                                View_UDR_Resource_List_Out.udr_list[i].text = adoDataTable.Rows[i]["TEXT"].ToString();
                                View_UDR_Resource_List_Out.udr_list[i].text_size = Convert.ToInt32(adoDataTable.Rows[i]["TEXT_SIZE"]);
                                View_UDR_Resource_List_Out.udr_list[i].text_color = Convert.ToInt32(adoDataTable.Rows[i]["TEXT_COLOR"]);
                                View_UDR_Resource_List_Out.udr_list[i].text_style = adoDataTable.Rows[i]["TEXT_STYLE"].ToString()[0];
                                View_UDR_Resource_List_Out.udr_list[i].tag_type = Convert.ToInt32(adoDataTable.Rows[i]["TAG_TYPE"]);
                                View_UDR_Resource_List_Out.udr_list[i].back_color = Convert.ToInt32(adoDataTable.Rows[i]["BACK_COLOR"]);
                                View_UDR_Resource_List_Out.udr_list[i].create_time = adoDataTable.Rows[i]["CREATE_TIME"].ToString();
                                View_UDR_Resource_List_Out.udr_list[i].update_time = adoDataTable.Rows[i]["UPDATE_TIME"].ToString();
                                View_UDR_Resource_List_Out.udr_list[i].no_mouse_event = adoDataTable.Rows[i]["NO_MOUSE_EVENT"].ToString()[0];
				                View_UDR_Resource_List_Out.udr_list[i].signal_flag = adoDataTable.Rows[i]["SIGNAL_FLAG"].ToString()[0];
                                View_UDR_Resource_List_Out.udr_list[i].seq = Convert.ToInt32(adoDataTable.Rows[i]["SEQ"]);
                                View_UDR_Resource_List_Out.udr_list[i].res_desc = adoDataTable.Rows[i]["RES_DESC"].ToString();
                                View_UDR_Resource_List_Out.udr_list[i].res_type = adoDataTable.Rows[i]["RES_TYPE"].ToString();
                                View_UDR_Resource_List_Out.udr_list[i].area_id = adoDataTable.Rows[i]["AREA_ID"].ToString();
                                View_UDR_Resource_List_Out.udr_list[i].sub_area_id = adoDataTable.Rows[i]["SUB_AREA_ID"].ToString();
		                        View_UDR_Resource_List_Out.udr_list[i].res_location = adoDataTable.Rows[i]["RES_LOCATION"].ToString();
                                View_UDR_Resource_List_Out.udr_list[i].proc_rule = adoDataTable.Rows[i]["PROC_RULE"].ToString()[0];
                                View_UDR_Resource_List_Out.udr_list[i].max_proc_count = Convert.ToInt32(adoDataTable.Rows[i]["MAX_PROC_COUNT"]);
                                View_UDR_Resource_List_Out.udr_list[i].res_up_down_flag = adoDataTable.Rows[i]["RES_UP_DOWN_FLAG"].ToString()[0];
                                View_UDR_Resource_List_Out.udr_list[i].res_pri_sts = adoDataTable.Rows[i]["RES_PRI_STS"].ToString();
                                View_UDR_Resource_List_Out.udr_list[i].res_ctrl_mode = adoDataTable.Rows[i]["RES_CTRL_MODE"].ToString();
                                View_UDR_Resource_List_Out.udr_list[i].res_proc_mode = adoDataTable.Rows[i]["RES_PROC_MODE"].ToString();
                                View_UDR_Resource_List_Out.udr_list[i].last_recipe_id = adoDataTable.Rows[i]["LAST_RECIPE_ID"].ToString();
                                View_UDR_Resource_List_Out.udr_list[i].proc_count = Convert.ToInt32(adoDataTable.Rows[i]["PROC_COUNT"]);
                                View_UDR_Resource_List_Out.udr_list[i].last_start_time = adoDataTable.Rows[i]["LAST_START_TIME"].ToString();
                                View_UDR_Resource_List_Out.udr_list[i].last_end_time = adoDataTable.Rows[i]["LAST_END_TIME"].ToString();
                                View_UDR_Resource_List_Out.udr_list[i].last_down_time = adoDataTable.Rows[i]["LAST_DOWN_TIME"].ToString();
                                View_UDR_Resource_List_Out.udr_list[i].last_down_hist_seq = Convert.ToInt32(adoDataTable.Rows[i]["LAST_DOWN_HIST_SEQ"]);
                                View_UDR_Resource_List_Out.udr_list[i].last_event = adoDataTable.Rows[i]["LAST_EVENT_ID"].ToString();
                                View_UDR_Resource_List_Out.udr_list[i].last_event_time = adoDataTable.Rows[i]["LAST_EVENT_TIME"].ToString();
                                View_UDR_Resource_List_Out.udr_list[i].last_active_hist_seq = Convert.ToInt32(adoDataTable.Rows[i]["LAST_ACTIVE_HIST_SEQ"]);
                                View_UDR_Resource_List_Out.udr_list[i].last_hist_seq = Convert.ToInt32(adoDataTable.Rows[i]["LAST_HIST_SEQ"]);
                                View_UDR_Resource_List_Out.udr_list[i].img_idx = MFMBRESIMG.IMAGE_IDX;
                                
	                        }
                        }
                    }
                    else if(View_UDR_Resource_List_In.res_type == 'T')
                    {
                        MFMBUDRLOC.Init();
                        MFMBUDRLOC.FACTORY = View_UDR_Resource_List_In.h_factory;
                        MFMBUDRLOC.GROUP_ID = View_UDR_Resource_List_In.group;
                        MFMBUDRLOC.SEQ = View_UDR_Resource_List_In.next_seq;
                        if (MFMBUDRLOC.FillData(1, ref adoDataTable) == false)
                        {
                            if (DBC.gErrors.SqlCode == SQL_CODE.SQL_NOT_FOUND)
                            {
                                View_UDR_Resource_List_Out.h_status_value = StdGlobalConstant.MP_SUCCESS;
                                return;
                            }
                            View_UDR_Resource_List_Out.h_msg = DBC.gErrors.SqlCode.ToString();
                            View_UDR_Resource_List_Out.h_db_err_msg = DBC.gErrors.ErrMsg;
                            DBC.DB_Rollback();
                            return;
                        }
                        
                        for (i = 0; i < adoDataTable.Rows.Count; i++)
                        {
                            if(i == MAX_UDR_LIST)
	                        {
                                View_UDR_Resource_List_Out.next_seq = Convert.ToInt32(adoDataTable.Rows[i]["SEQ"]);
                                break;
	                        }
                            View_UDR_Resource_List_Out.udr_list[i].seq = Convert.ToInt32(adoDataTable.Rows[i]["SEQ"]);
		                    View_UDR_Resource_List_Out.udr_list[i].res_id = adoDataTable.Rows[i]["RES_ID"].ToString();
                            View_UDR_Resource_List_Out.udr_list[i].res_tag_flag = adoDataTable.Rows[i]["RES_TYPE"].ToString()[0];
                            View_UDR_Resource_List_Out.udr_list[i].loc_x = Convert.ToInt32(adoDataTable.Rows[i]["LOC_X"]);
                            View_UDR_Resource_List_Out.udr_list[i].loc_y = Convert.ToInt32(adoDataTable.Rows[i]["LOC_Y"]);
                            View_UDR_Resource_List_Out.udr_list[i].loc_width = Convert.ToInt32(adoDataTable.Rows[i]["LOC_WIDTH"]);
                            View_UDR_Resource_List_Out.udr_list[i].loc_height = Convert.ToInt32(adoDataTable.Rows[i]["LOC_HEIGHT"]);
                            View_UDR_Resource_List_Out.udr_list[i].text = adoDataTable.Rows[i]["TEXT"].ToString();
                            View_UDR_Resource_List_Out.udr_list[i].text_size = Convert.ToInt32(adoDataTable.Rows[i]["TEXT_SIZE"]);
                            View_UDR_Resource_List_Out.udr_list[i].text_color = Convert.ToInt32(adoDataTable.Rows[i]["TEXT_COLOR"]);
                            View_UDR_Resource_List_Out.udr_list[i].text_size = Convert.ToInt32(adoDataTable.Rows[i]["TEXT_SIZE"]);
                            View_UDR_Resource_List_Out.udr_list[i].text_style = adoDataTable.Rows[i]["TEXT_STYLE"].ToString()[0];
                            View_UDR_Resource_List_Out.udr_list[i].tag_type = Convert.ToInt32(adoDataTable.Rows[i]["TAG_TYPE"]);
                            View_UDR_Resource_List_Out.udr_list[i].back_color = Convert.ToInt32(adoDataTable.Rows[i]["BACK_COLOR"]);
                            View_UDR_Resource_List_Out.udr_list[i].create_time = adoDataTable.Rows[i]["CREATE_TIME"].ToString();
                            View_UDR_Resource_List_Out.udr_list[i].update_time = adoDataTable.Rows[i]["UPDATE_TIME"].ToString();
                            View_UDR_Resource_List_Out.udr_list[i].no_mouse_event = adoDataTable.Rows[i]["NO_MOUSE_EVENT"].ToString()[0];
                            View_UDR_Resource_List_Out.udr_list[i].signal_flag = adoDataTable.Rows[i]["SIGNAL_FLAG"].ToString()[0];
                        }
                    }
                }
                /*Control Sequence 별 List*/
                else if(View_UDR_Resource_List_In.h_proc_step == '2')
                {
                    FMBJOIN.Init();
                    FMBJOIN.FACTORY = View_UDR_Resource_List_In.h_factory;
                    FMBJOIN.GROUP_ID = View_UDR_Resource_List_In.group;
                    FMBJOIN.SEQ = View_UDR_Resource_List_In.next_seq;
                    if (FMBJOIN.FillUdrDetail(2, ref adoDataTable) == false)
                    {
                        if (DBC.gErrors.SqlCode == SQL_CODE.SQL_NOT_FOUND)
                        {
                            View_UDR_Resource_List_Out.h_status_value = StdGlobalConstant.MP_SUCCESS;
                            return;
                        }
                        View_UDR_Resource_List_Out.h_msg = DBC.gErrors.SqlCode.ToString();
                        View_UDR_Resource_List_Out.h_db_err_msg = DBC.gErrors.ErrMsg;
                        DBC.DB_Rollback();
                        return;
                    }

                    if (adoDataTable.Rows.Count > 0)
                    {
                        View_UDR_Resource_List_Out.udr_list = new FMB_View_UDR_Resource_List_Out_Detail_Tag_udr_list[adoDataTable.Rows.Count];
                        for (i = 0; i < adoDataTable.Rows.Count; i++)
                        {
                            if(i == MAX_UDR_LIST)
	                        {
                                View_UDR_Resource_List_Out.next_seq = Convert.ToInt32(adoDataTable.Rows[i]["SEQ"]); 
                                break;
	                        }
                            
                            MFMBRESIMG.Init();
                            MFMBRESIMG.FACTORY = View_UDR_Resource_List_In.h_factory;
                            MFMBRESIMG.RES_ID = adoDataTable.Rows[i]["RES_ID"].ToString();
                            MFMBRESIMG.SelectData(1, ref adoImgTable);

                            View_UDR_Resource_List_Out.udr_list[i].res_id = adoDataTable.Rows[i]["RES_ID"].ToString();
                            View_UDR_Resource_List_Out.udr_list[i].res_tag_flag = adoDataTable.Rows[i]["RES_TAG_FLAG"].ToString()[0];
                            View_UDR_Resource_List_Out.udr_list[i].loc_x = Convert.ToInt32(adoDataTable.Rows[i]["LOC_X"]);
                            View_UDR_Resource_List_Out.udr_list[i].loc_y = Convert.ToInt32(adoDataTable.Rows[i]["LOC_Y"]);
                            View_UDR_Resource_List_Out.udr_list[i].loc_width = Convert.ToInt32(adoDataTable.Rows[i]["LOC_WIDTH"]);
                            View_UDR_Resource_List_Out.udr_list[i].loc_height = Convert.ToInt32(adoDataTable.Rows[i]["LOC_HEIGHT"]);
                            View_UDR_Resource_List_Out.udr_list[i].text = adoDataTable.Rows[i]["TEXT"].ToString();
                            View_UDR_Resource_List_Out.udr_list[i].text_size = Convert.ToInt32(adoDataTable.Rows[i]["TEXT_SIZE"]);
                            View_UDR_Resource_List_Out.udr_list[i].text_color = Convert.ToInt32(adoDataTable.Rows[i]["TEXT_COLOR"]);
                            View_UDR_Resource_List_Out.udr_list[i].text_size = Convert.ToInt32(adoDataTable.Rows[i]["TEXT_SIZE"]);
                            View_UDR_Resource_List_Out.udr_list[i].text_style = adoDataTable.Rows[i]["TEXT_STYLE"].ToString()[0];
                            View_UDR_Resource_List_Out.udr_list[i].tag_type = Convert.ToInt32(adoDataTable.Rows[i]["TAG_TYPE"]);
                            View_UDR_Resource_List_Out.udr_list[i].back_color = Convert.ToInt32(adoDataTable.Rows[i]["BACK_COLOR"]);
                            View_UDR_Resource_List_Out.udr_list[i].create_time = adoDataTable.Rows[i]["CREATE_TIME"].ToString();
                            View_UDR_Resource_List_Out.udr_list[i].update_time = adoDataTable.Rows[i]["UPDATE_TIME"].ToString();
                            View_UDR_Resource_List_Out.udr_list[i].no_mouse_event = adoDataTable.Rows[i]["NO_MOUSE_EVENT"].ToString()[0];
                            View_UDR_Resource_List_Out.udr_list[i].signal_flag = adoDataTable.Rows[i]["SIGNAL_FLAG"].ToString()[0];
                            View_UDR_Resource_List_Out.udr_list[i].seq = Convert.ToInt32(adoDataTable.Rows[i]["SEQ"]);
                            View_UDR_Resource_List_Out.udr_list[i].res_desc = adoDataTable.Rows[i]["RES_DESC"].ToString();
                            View_UDR_Resource_List_Out.udr_list[i].res_type = adoDataTable.Rows[i]["RES_TYPE"].ToString();
                            View_UDR_Resource_List_Out.udr_list[i].area_id = adoDataTable.Rows[i]["AREA_ID"].ToString();
                            View_UDR_Resource_List_Out.udr_list[i].sub_area_id = adoDataTable.Rows[i]["SUB_AREA_ID"].ToString();
		                    View_UDR_Resource_List_Out.udr_list[i].res_location = adoDataTable.Rows[i]["RES_LOCATION"].ToString();
                            View_UDR_Resource_List_Out.udr_list[i].proc_rule = adoDataTable.Rows[i]["PROC_RULE"].ToString()[0];
                            View_UDR_Resource_List_Out.udr_list[i].max_proc_count = Convert.ToInt32(adoDataTable.Rows[i]["MAX_PROC_COUNT"]);
                            View_UDR_Resource_List_Out.udr_list[i].res_up_down_flag = adoDataTable.Rows[i]["RES_UP_DOWN_FLAG"].ToString()[0];
                            View_UDR_Resource_List_Out.udr_list[i].res_pri_sts = adoDataTable.Rows[i]["RES_PRI_STS"].ToString();
                            View_UDR_Resource_List_Out.udr_list[i].res_ctrl_mode = adoDataTable.Rows[i]["RES_CTRL_MODE"].ToString();
                            View_UDR_Resource_List_Out.udr_list[i].res_proc_mode = adoDataTable.Rows[i]["RES_PROC_MODE"].ToString();
                            View_UDR_Resource_List_Out.udr_list[i].last_recipe_id = adoDataTable.Rows[i]["LAST_RECIPE_ID"].ToString();
                            View_UDR_Resource_List_Out.udr_list[i].proc_count = Convert.ToInt32(adoDataTable.Rows[i]["PROC_COUNT"]);
                            View_UDR_Resource_List_Out.udr_list[i].last_start_time = adoDataTable.Rows[i]["LAST_START_TIME"].ToString();
                            View_UDR_Resource_List_Out.udr_list[i].last_end_time = adoDataTable.Rows[i]["LAST_END_TIME"].ToString();
                            View_UDR_Resource_List_Out.udr_list[i].last_down_time = adoDataTable.Rows[i]["LAST_DOWN_TIME"].ToString();
                            View_UDR_Resource_List_Out.udr_list[i].last_down_hist_seq = Convert.ToInt32(adoDataTable.Rows[i]["LAST_DOWN_HIST_SEQ"]);
                            View_UDR_Resource_List_Out.udr_list[i].last_event = adoDataTable.Rows[i]["LAST_EVENT_ID"].ToString();
                            View_UDR_Resource_List_Out.udr_list[i].last_event_time = adoDataTable.Rows[i]["LAST_EVENT_TIME"].ToString();
                            View_UDR_Resource_List_Out.udr_list[i].last_active_hist_seq = Convert.ToInt32(adoDataTable.Rows[i]["LAST_ACTIVE_HIST_SEQ"]);
                            View_UDR_Resource_List_Out.udr_list[i].last_hist_seq = Convert.ToInt32(adoDataTable.Rows[i]["LAST_HIST_SEQ"]);
                            View_UDR_Resource_List_Out.udr_list[i].img_idx = Convert.ToInt32(adoDataTable.Rows[i]["IMAGE_IDX"]);

                        }
                    }
                   
                }
            }
            catch (Exception ex)
            {
                View_UDR_Resource_List_Out.h_field_msg = ex.Message;
                return;
            }
            finally
            {
                DBC.DB_Close();

            }

        }
    }
        
}
