using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using Miracom.SmartWeb.FWX;

namespace Miracom.SmartWeb.RO
{
    public partial class FMBTransferImp : FMBTransfer
    {
        public const int MAX_RES_LIST = 1000;


        public override void FMB_View_Resource_List(FMB_View_Resource_List_In_Tag View_Resource_List_In, ref FMB_View_Resource_List_Out_Tag View_Resource_List_Out)
        {
            int i, i_case = 0;
            DataTable adoDataTable = null;
            DataTable adoResTable = null;
            DataTable adoImgTable = null;

            DB_Common DBC = new DB_Common();
            DBC_MRASRESDEF MRASRESDEF = new DBC_MRASRESDEF(ref DBC);
            DBC_MFMBRESIMG MFMBRESIMG = new DBC_MFMBRESIMG(ref DBC);
            DBC_MFMBRESLOC MFMBRESLOC = new DBC_MFMBRESLOC(ref DBC);
            DBC_FMBJOIN    FMBJOIN = new DBC_FMBJOIN(ref DBC);

            try
            {
                DBC.gDBConnectionString = StdGlobalVariable.ROXml.GetValue("//appSettings//add[@key='MESConnectionString']");

                if (DBC.OpenDB() != true)
                {
                    View_Resource_List_Out.h_db_err_msg = DBC.gErrors.ErrMsg;
                    return;
                }

                DBC.DB_BeginTransaction();

                View_Resource_List_Out.h_status_value = StdGlobalConstant.MP_FAIL;
                View_Resource_List_Out._size_res_list = 0;
                View_Resource_List_Out.count = 0;

                i = 0;
                i_case = 0;

                if (View_Resource_List_In.h_proc_step == '1')
                {
                    i_case = 1;
                }
                else if (View_Resource_List_In.h_proc_step == '4')
                {
                    i_case = 2;
                }

                if (View_Resource_List_In.h_proc_step == '1' || View_Resource_List_In.h_proc_step == '4')
                {
                    FMBJOIN.Init();
                    FMBJOIN.FACTORY = View_Resource_List_In.h_factory;
                    FMBJOIN.RES_ID = View_Resource_List_In.next_res_id;


                    if (FMBJOIN.FillResLocList(1, ref adoDataTable) == false)
                    {
                        if (DBC.gErrors.SqlCode == SQL_CODE.SQL_NOT_FOUND)
                        {
                            View_Resource_List_Out.h_status_value = StdGlobalConstant.MP_SUCCESS;
                            return;
                        }
                        View_Resource_List_Out.h_msg = DBC.gErrors.SqlCode.ToString();
                        View_Resource_List_Out.h_db_err_msg = DBC.gErrors.ErrMsg;
                        DBC.DB_Rollback();
                        return;
                    }

                    if (adoDataTable.Rows.Count > 0)
                    {
                        View_Resource_List_Out.res_list = new FMB_View_Resource_List_Out_Tag_res_list[adoDataTable.Rows.Count];
                        for (i = 0; i < adoDataTable.Rows.Count; i++)
                        {
                            if (i == MAX_RES_LIST)
                            {
                                View_Resource_List_Out.next_res_id = adoDataTable.Rows[i]["RES_ID"].ToString();
                                break;
                            }

                            MRASRESDEF.Init();
                            MRASRESDEF.FACTORY = View_Resource_List_In.h_factory;
                            MRASRESDEF.RES_ID = adoDataTable.Rows[i]["RES_ID"].ToString();
                            MRASRESDEF.SelectData(1, ref adoResTable);

                            MFMBRESIMG.Init();
                            MFMBRESIMG.FACTORY = View_Resource_List_In.h_factory;
                            MFMBRESIMG.RES_ID = adoDataTable.Rows[i]["RES_ID"].ToString();
                            MFMBRESIMG.SelectData(1, ref adoImgTable);

                            View_Resource_List_Out.res_list[i] = new FMB_View_Resource_List_Out_Tag_res_list();
                            View_Resource_List_Out.res_list[i].res_id = adoDataTable.Rows[i]["RES_ID"].ToString();
                            View_Resource_List_Out.res_list[i].res_desc = adoDataTable.Rows[i]["RES_DESC"].ToString();
                            View_Resource_List_Out.res_list[i].attached_flag = adoDataTable.Rows[i]["ATTACHED_FLAG"].ToString()[0];
                            View_Resource_List_Out.res_list[i].layout_id = adoDataTable.Rows[i]["LAYOUT_ID"].ToString();
                            View_Resource_List_Out.res_list[i].res_up_down_flag = adoDataTable.Rows[i]["UP_DOWN_FLAG"].ToString()[0];
                            View_Resource_List_Out.res_list[i].res_type = MRASRESDEF.RES_TYPE;
			                View_Resource_List_Out.res_list[i].area_id = MRASRESDEF.AREA_ID;
			                View_Resource_List_Out.res_list[i].sub_area_id = MRASRESDEF.SUB_AREA_ID;
			                View_Resource_List_Out.res_list[i].res_pri_sts = MRASRESDEF.RES_PRI_STS;
			                View_Resource_List_Out.res_list[i].res_ctrl_mode = MRASRESDEF.RES_CTRL_MODE;
			                View_Resource_List_Out.res_list[i].res_proc_mode = MRASRESDEF.RES_PROC_MODE;
			                View_Resource_List_Out.res_list[i].last_start_time = MRASRESDEF.LAST_START_TIME;
			                View_Resource_List_Out.res_list[i].last_end_time = MRASRESDEF.LAST_END_TIME;
			                View_Resource_List_Out.res_list[i].last_down_time = MRASRESDEF.LAST_DOWN_TIME;
			                View_Resource_List_Out.res_list[i].last_event = MRASRESDEF.LAST_EVENT_ID;
			                View_Resource_List_Out.res_list[i].last_event_time = MRASRESDEF.LAST_EVENT_TIME;
                            View_Resource_List_Out.res_list[i].img_idx = MFMBRESIMG.IMAGE_IDX;
                            
                        }
                    }
                }

                if (View_Resource_List_In.h_proc_step == '2' || View_Resource_List_In.h_proc_step == '3')
                {
                    MRASRESDEF.Init();
                    MRASRESDEF.FACTORY = View_Resource_List_In.h_factory;
                    MRASRESDEF.RES_ID = View_Resource_List_In.next_res_id;

                    if (MRASRESDEF.FillData(6, ref adoDataTable) == false)
                    {
                        if (DBC.gErrors.SqlCode == SQL_CODE.SQL_NOT_FOUND)
                        {
                            View_Resource_List_Out.h_status_value = StdGlobalConstant.MP_SUCCESS;
                            return;
                        }
                        View_Resource_List_Out.h_msg = DBC.gErrors.SqlCode.ToString();
                        View_Resource_List_Out.h_db_err_msg = DBC.gErrors.ErrMsg;
                        DBC.DB_Rollback();
                        return;
                    }

                    if (adoDataTable.Rows.Count > 0)
                    {
                        View_Resource_List_Out.res_list = new FMB_View_Resource_List_Out_Tag_res_list[adoDataTable.Rows.Count];
                        for (i = 0; i < adoDataTable.Rows.Count; i++)
                        {
                            if (i == MAX_RES_LIST)
                            {
                                View_Resource_List_Out.next_res_id = adoDataTable.Rows[i]["RES_ID"].ToString();
                                break;
                            }

                            View_Resource_List_Out.res_list[i] = new FMB_View_Resource_List_Out_Tag_res_list();
                            View_Resource_List_Out.res_list[i].res_id = adoDataTable.Rows[i]["RES_ID"].ToString();
                            View_Resource_List_Out.res_list[i].res_desc = adoDataTable.Rows[i]["RES_DESC"].ToString();
                            View_Resource_List_Out.res_list[i].res_up_down_flag = adoDataTable.Rows[i]["RES_UP_DOWN_FLAG"].ToString()[0];
                            View_Resource_List_Out.res_list[i].delete_flag = adoDataTable.Rows[i]["DELETE_FLAG"].ToString()[0];

                        }
                    }
                }

                if (View_Resource_List_In.h_proc_step == '5')
                {
                    MFMBRESLOC.Init();
                    MFMBRESLOC.FACTORY = View_Resource_List_In.h_factory;
                    MFMBRESLOC.RES_ID = View_Resource_List_In.next_res_id;
                    MFMBRESLOC.LAYOUT_ID = View_Resource_List_In.layout_id;

                    if (MFMBRESLOC.FillData(2, ref adoDataTable) == false)
                    {
                        if (DBC.gErrors.SqlCode == SQL_CODE.SQL_NOT_FOUND)
                        {
                            View_Resource_List_Out.h_status_value = StdGlobalConstant.MP_SUCCESS;
                            return;
                        }
                        View_Resource_List_Out.h_msg = DBC.gErrors.SqlCode.ToString();
                        View_Resource_List_Out.h_db_err_msg = DBC.gErrors.ErrMsg;
                        DBC.DB_Rollback();
                        return;
                    }

                    if (adoDataTable.Rows.Count > 0)
                    {
                        View_Resource_List_Out.res_list = new FMB_View_Resource_List_Out_Tag_res_list[adoDataTable.Rows.Count];
                        for (i = 0; i < adoDataTable.Rows.Count; i++)
                        {
                            if (i == MAX_RES_LIST)
                            {
                                View_Resource_List_Out.next_res_id = adoDataTable.Rows[i]["RES_ID"].ToString();
                                break;
                            }

                            MRASRESDEF.Init();
                            MRASRESDEF.FACTORY = View_Resource_List_In.h_factory;
                            MRASRESDEF.RES_ID = adoDataTable.Rows[i]["RES_ID"].ToString();
                            MRASRESDEF.SelectData(1, ref adoResTable);

                            View_Resource_List_Out.res_list[i] = new FMB_View_Resource_List_Out_Tag_res_list();
                            View_Resource_List_Out.res_list[i].res_id = adoDataTable.Rows[i]["RES_ID"].ToString();
                            View_Resource_List_Out.res_list[i].res_desc = adoDataTable.Rows[i]["RES_DESC"].ToString();
                            View_Resource_List_Out.res_list[i].res_up_down_flag = adoDataTable.Rows[i]["RES_UP_DOWN_FLAG"].ToString()[0];

                        }
                    }

                }

                View_Resource_List_Out.count = i;
                View_Resource_List_Out._size_res_list = i;
                View_Resource_List_Out.h_status_value = StdGlobalConstant.MP_SUCCESS;

            }
            catch (Exception ex)
            {
                View_Resource_List_Out.h_field_msg = ex.Message;
                return;
            }
            finally
            {
                DBC.DB_Close();

            }
        }

    }

}
