using System;
using System.Data;
using System.Data.OleDb;
using Miracom.SmartWeb.FWX;
using Miracom.SmartWeb;

namespace Miracom.SmartWeb.RO
{
    public partial class FMBTransferImp : FMBTransfer
    {
        public const int MAX_FMB_GROUP_COUNT = 1000;

        public override void FMB_View_FMB_Group_List(FMB_View_FMB_Group_List_In_Tag View_FMB_Group_List_In, ref FMB_View_FMB_Group_List_Out_Tag View_FMB_Group_List_Out)
        {
            int i_case, i =0;
            DataTable adoDataTable = null;

            DB_Common DBC = new DB_Common();
            DBC_MFMBGRPUSR MFMBGRPUSR = new DBC_MFMBGRPUSR(ref DBC);
            DBC_MFMBUDRDEF MFMBUDRDEF = new DBC_MFMBUDRDEF(ref DBC);
	        DBC_MSECUSRDEF MSECUSRDEF = new DBC_MSECUSRDEF(ref DBC);

            try
            {
                DBC.gDBConnectionString = StdGlobalVariable.ROXml.GetValue("//appSettings//add[@key='MESConnectionString']");
                
                if (DBC.OpenDB() != true)
                {
                    View_FMB_Group_List_Out.h_db_err_msg = DBC.gErrors.ErrMsg;
                    return;
                }

                DBC.DB_BeginTransaction();

                View_FMB_Group_List_Out.h_status_value = StdGlobalConstant.MP_FAIL;
                View_FMB_Group_List_Out._size_udr_group_list = 0;
                View_FMB_Group_List_Out.count = 0;

                i_case = 0;
	            if(View_FMB_Group_List_In.h_proc_step == '1')
	            {
		            i_case = 1;
	            }
	            else if(View_FMB_Group_List_In.h_proc_step == '2')
	            {
		            i_case = 2;
	            }

                MFMBGRPUSR.Init();
                MFMBGRPUSR.FACTORY = View_FMB_Group_List_In.h_factory;
                MFMBGRPUSR.GROUP_ID = View_FMB_Group_List_In.next_group;
                MFMBGRPUSR.USER_ID = View_FMB_Group_List_In.next_user;
                
                if (MFMBGRPUSR.FillData(i_case, ref adoDataTable) == false)
                {
                    if (DBC.gErrors.SqlCode == SQL_CODE.SQL_NOT_FOUND)
                    {
                        View_FMB_Group_List_Out.h_status_value = StdGlobalConstant.MP_SUCCESS;
                        return;
                    }

                    View_FMB_Group_List_Out.h_msg = DBC.gErrors.SqlCode.ToString();
                    View_FMB_Group_List_Out.h_db_err_msg = DBC.gErrors.ErrMsg;
                    DBC.DB_Rollback();
                    return;
                }
                
                if (adoDataTable.Rows.Count > 0)
                {
                    View_FMB_Group_List_Out.udr_group_list = new  FMB_View_FMB_Group_List_Out_Tag_udr_group_list[adoDataTable.Rows.Count];
                    for (i = 0; i < adoDataTable.Rows.Count; i++)
                    {

                        if (i == MAX_FMB_GROUP_COUNT)
		                {
                            View_FMB_Group_List_Out.next_group = adoDataTable.Rows[i]["GROUP_ID"].ToString();
                            View_FMB_Group_List_Out.next_user = adoDataTable.Rows[i]["USER_ID"].ToString();
                            break;
		                }

                        View_FMB_Group_List_Out.udr_group_list[i] = new  FMB_View_FMB_Group_List_Out_Tag_udr_group_list();
                        if(View_FMB_Group_List_In.h_proc_step == '1')
    		            {
                            View_FMB_Group_List_Out.udr_group_list[i].user = adoDataTable.Rows[i]["USER_ID"].ToString();
            			
			                MSECUSRDEF.Init();
			                MSECUSRDEF.FACTORY = View_FMB_Group_List_In.h_factory;
                            MSECUSRDEF.USER_ID = adoDataTable.Rows[i]["USER_ID"].ToString();
			                MSECUSRDEF.SelectData(i_case, ref adoDataTable);
			                View_FMB_Group_List_Out.udr_group_list[i].user_desc = MSECUSRDEF.USER_DESC;

                        }
                        else if(View_FMB_Group_List_In.h_proc_step == '2')
                        {
                            View_FMB_Group_List_Out.udr_group_list[i].group = adoDataTable.Rows[i]["USER_ID"].ToString();
            			
			                MFMBUDRDEF.Init();
			                MFMBUDRDEF.FACTORY = View_FMB_Group_List_In.h_factory;
                            MFMBUDRDEF.GROUP_ID = adoDataTable.Rows[i]["USER_ID"].ToString();
			                MFMBUDRDEF.SelectData(i_case, ref adoDataTable);

			                View_FMB_Group_List_Out.udr_group_list[i].group_desc = MFMBUDRDEF.GROUP_DESC;
                        }

	                }
                    i++;
                }

	            View_FMB_Group_List_Out.count = i;
                View_FMB_Group_List_Out._size_udr_group_list = i;
                View_FMB_Group_List_Out.h_status_value = StdGlobalConstant.MP_SUCCESS;
            }
            catch (Exception ex)
            {
                View_FMB_Group_List_Out.h_field_msg = ex.Message;
                return;
            }
            finally
            {
                DBC.DB_Close();

            }

        }
    }
}
