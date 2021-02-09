using System;
using System.Data;
using System.Data.OleDb;
using Miracom.SmartWeb.FWX;
using Miracom.SmartWeb;

namespace Miracom.SmartWeb.RO
{
    public partial class FMBTransferImp : FMBTransfer
    {
        public override void FMB_View_UDR_Group(FMB_View_UDR_Group_In_Tag View_UDR_Group_In, ref FMB_View_UDR_Group_Out_Tag View_UDR_Group_Out)
        {
            DataTable adoFacTable = null;
            DataTable adoGroupTable = null;
            DB_Common DBC = new DB_Common();

            DBC_MWIPFACDEF MWIPFACDEF = new DBC_MWIPFACDEF(ref DBC);
            DBC_MFMBUDRDEF MFMBUDRDEF = new DBC_MFMBUDRDEF(ref DBC);

            try
            {
                
                DBC.gDBConnectionString = StdGlobalVariable.ROXml.GetValue("//appSettings//add[@key='MESConnectionString']");

                if (DBC.OpenDB() != true)
                {
                    View_UDR_Group_Out.h_db_err_msg = DBC.gErrors.ErrMsg;
                    return;
                }

                DBC.DB_BeginTransaction();

                View_UDR_Group_Out.h_status_value = StdGlobalConstant.MP_FAIL;

                MWIPFACDEF.Init();
                MWIPFACDEF.FACTORY = View_UDR_Group_In.h_factory;

                if (MWIPFACDEF.SelectData(1, ref adoFacTable) == false)
                {
                    if (DBC.gErrors.SqlCode == SQL_CODE.SQL_NOT_FOUND)
                    {
                        View_UDR_Group_Out.h_status_value = StdGlobalConstant.MP_SUCCESS;
                        return;
                    }
                    View_UDR_Group_Out.h_msg = DBC.gErrors.SqlCode.ToString();
                    View_UDR_Group_Out.h_db_err_msg = DBC.gErrors.ErrMsg;
                    DBC.DB_Rollback();
                    return;
                }

                MFMBUDRDEF.Init();
                MFMBUDRDEF.FACTORY = View_UDR_Group_In.h_factory;
                MFMBUDRDEF.GROUP_ID = View_UDR_Group_In.group_id;

                if (MFMBUDRDEF.SelectData(1, ref adoGroupTable) == false)
                {
                    if (DBC.gErrors.SqlCode == SQL_CODE.SQL_NOT_FOUND)
                    {
                        View_UDR_Group_Out.h_status_value = StdGlobalConstant.MP_SUCCESS;
                        return;
                    }
                    View_UDR_Group_Out.h_msg = DBC.gErrors.SqlCode.ToString();
                    View_UDR_Group_Out.h_db_err_msg = DBC.gErrors.ErrMsg;
                    DBC.DB_Rollback();
                    return;
                }

                View_UDR_Group_Out.group_id = MFMBUDRDEF.GROUP_ID;
                View_UDR_Group_Out.group_desc = MFMBUDRDEF.GROUP_DESC;
                View_UDR_Group_Out.create_time = MFMBUDRDEF.CREATE_TIME;
                View_UDR_Group_Out.update_time = MFMBUDRDEF.UPDATE_TIME;
                View_UDR_Group_Out.width = MFMBUDRDEF.WIDTH;
                View_UDR_Group_Out.height = MFMBUDRDEF.HEIGHT;

                View_UDR_Group_Out.h_status_value = StdGlobalConstant.MP_SUCCESS;

            }
            catch (Exception ex)
            {
                View_UDR_Group_Out.h_field_msg = ex.Message;
                return;
            }
            finally
            {
                DBC.DB_Close();

            }
        }
    }
        
}
