using System;
using System.Data;
using System.Data.OleDb;
using Miracom.SmartWeb.FWX;
using Miracom.SmartWeb;

namespace Miracom.SmartWeb.RO
{
    public partial class FMBTransferImp : FMBTransfer
    {
        public const int MAX_LAYOUT_LIST = 1000;

        public override void FMB_View_LayOut_List(FMB_View_LayOut_List_In_Tag View_LayOut_List_In, ref FMB_View_LayOut_List_Out_Tag View_LayOut_List_Out)
        {
            
            int i =0;
            DataTable adoDataTable = null;

            DB_Common DBC = new DB_Common();
            DBC_MFMBFACLYT MFMBFACLYT = new DBC_MFMBFACLYT(ref DBC);

            try
            {
                DBC.gDBConnectionString = StdGlobalVariable.ROXml.GetValue("//appSettings//add[@key='MESConnectionString']");
                
                if (DBC.OpenDB() != true)
                {
                    View_LayOut_List_Out.h_db_err_msg = DBC.gErrors.ErrMsg;
                    return;
                }

                DBC.DB_BeginTransaction();

                View_LayOut_List_Out.h_status_value = StdGlobalConstant.MP_FAIL;
                View_LayOut_List_Out._size_layout_list = 0;
                View_LayOut_List_Out.count = 0;

                MFMBFACLYT.Init();
                MFMBFACLYT.FACTORY = View_LayOut_List_In.h_factory;
                MFMBFACLYT.LAYOUT_ID = View_LayOut_List_In.next_layout_id;
                

                if (MFMBFACLYT.FillData(1, ref adoDataTable) == false)
                {
                    if (DBC.gErrors.SqlCode == SQL_CODE.SQL_NOT_FOUND)
                    {
                        View_LayOut_List_Out.h_status_value = StdGlobalConstant.MP_SUCCESS;
                        return;
                    }
                    View_LayOut_List_Out.h_msg = DBC.gErrors.SqlCode.ToString();
                    View_LayOut_List_Out.h_db_err_msg = DBC.gErrors.ErrMsg;
                    DBC.DB_Rollback();
                    return;
                }

                if (adoDataTable.Rows.Count > 0)
                {
                    View_LayOut_List_Out.layout_list = new  FMB_View_LayOut_List_Out_Tag_layout_list[adoDataTable.Rows.Count];
                    for (i = 0; i < adoDataTable.Rows.Count; i++)
                    {
                        if(i == MAX_LAYOUT_LIST)
		                {
                            View_LayOut_List_Out.next_layout_id = adoDataTable.Rows[i]["LAYOUT_ID"].ToString();
                            break;
		                }
                        View_LayOut_List_Out.layout_list[i] = new FMB_View_LayOut_List_Out_Tag_layout_list();
                        View_LayOut_List_Out.layout_list[i].layout_id = adoDataTable.Rows[i]["LAYOUT_ID"].ToString();
                        View_LayOut_List_Out.layout_list[i].layout_desc = adoDataTable.Rows[i]["LAYOUT_DESC"].ToString();
                        View_LayOut_List_Out.layout_list[i].width = Convert.ToInt32(adoDataTable.Rows[i]["WIDTH"]);
                        View_LayOut_List_Out.layout_list[i].height = Convert.ToInt32(adoDataTable.Rows[i]["HEIGHT"]);
		                
	                }
                }

                View_LayOut_List_Out.count = i;
                View_LayOut_List_Out._size_layout_list = i;
                View_LayOut_List_Out.h_status_value = StdGlobalConstant.MP_SUCCESS;

            }
            catch (Exception ex)
            {
                View_LayOut_List_Out.h_field_msg = ex.Message;
                return;
            }
            finally
            {
                DBC.DB_Close();

            }

        }

    }
    
}
