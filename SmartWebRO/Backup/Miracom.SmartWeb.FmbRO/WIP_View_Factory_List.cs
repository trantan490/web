using System;
using System.Data;
using System.Data.OleDb;
using Miracom.SmartWeb.FWX;
using Miracom.SmartWeb;

namespace Miracom.SmartWeb.RO
{
    public partial class WIPTransferImp : WIPTransfer
    {
        public override void WIP_View_Factory_List(WIP_View_Factory_List_In_Tag View_Factory_List_In, ref WIP_View_Factory_List_Out_Tag View_Factory_List_Out)
        {

            int i = 0;
            DataTable adoDataTable = null;

            DB_Common DBC = new DB_Common();
            DBC_MWIPFACDEF MWIPFACDEF = new DBC_MWIPFACDEF(ref DBC);

            try
            {
                DBC.gDBConnectionString = StdGlobalVariable.ROXml.GetValue("//appSettings//add[@key='MESConnectionString']");

                if (DBC.OpenDB() != true)
                {
                    View_Factory_List_Out.h_db_err_msg = DBC.gErrors.ErrMsg;
                    return;
                }

                DBC.DB_BeginTransaction();

                View_Factory_List_Out.h_status_value = StdGlobalConstant.MP_FAIL;
                View_Factory_List_Out._size_factory_list = 0;
                View_Factory_List_Out.count = 0;

                MWIPFACDEF.Init();
                
                if (MWIPFACDEF.FillData(1, ref adoDataTable) == false)
                {
                    if (DBC.gErrors.SqlCode == SQL_CODE.SQL_NOT_FOUND)
                    {
                        View_Factory_List_Out.h_status_value = StdGlobalConstant.MP_SUCCESS;
                        return;
                    }
                    View_Factory_List_Out.h_msg = DBC.gErrors.SqlCode.ToString();
                    View_Factory_List_Out.h_db_err_msg = DBC.gErrors.ErrMsg;
                    DBC.DB_Rollback();
                    return;
                }

                if (adoDataTable.Rows.Count > 0)
                {
                    View_Factory_List_Out.factory_list = new WIP_View_Factory_List_Out_Tag_factory_list[adoDataTable.Rows.Count];
                    for (i = 0; i < adoDataTable.Rows.Count; i++)
                    {
                        View_Factory_List_Out.factory_list[i] = new WIP_View_Factory_List_Out_Tag_factory_list();
                        View_Factory_List_Out.factory_list[i].factory = adoDataTable.Rows[i]["FACTORY"].ToString();
                        View_Factory_List_Out.factory_list[i].fac_desc = adoDataTable.Rows[i]["FAC_DESC"].ToString();
                        
                    }
                }

                View_Factory_List_Out.count = i;
                View_Factory_List_Out._size_factory_list = i;
                View_Factory_List_Out.h_status_value = StdGlobalConstant.MP_SUCCESS;

            }
            catch (Exception ex)
            {
                View_Factory_List_Out.h_field_msg = ex.Message;
                return;
            }
            finally
            {
                DBC.DB_Close();

            }

        }

    }

}
