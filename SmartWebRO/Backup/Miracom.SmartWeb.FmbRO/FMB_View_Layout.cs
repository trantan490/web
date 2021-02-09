using System;
using System.Data;
using System.Data.OleDb;
using Miracom.SmartWeb.FWX;
using Miracom.SmartWeb;

namespace Miracom.SmartWeb.RO
{
    public partial class FMBTransferImp : FMBTransfer
    {
        public override void FMB_View_LayOut(FMB_View_LayOut_In_Tag View_LayOut_In, ref FMB_View_LayOut_Out_Tag View_LayOut_Out)
        {
            DataTable adoFacTable = null;
            DataTable adoGroupTable = null;
            DB_Common DBC = new DB_Common();

            DBC_MWIPFACDEF MWIPFACDEF = new DBC_MWIPFACDEF(ref DBC);
            DBC_MFMBFACLYT MFMBFACLYT = new DBC_MFMBFACLYT(ref DBC);

            try
            {

                DBC.gDBConnectionString = StdGlobalVariable.ROXml.GetValue("//appSettings//add[@key='MESConnectionString']");

                if (DBC.OpenDB() != true)
                {
                    View_LayOut_Out.h_db_err_msg = DBC.gErrors.ErrMsg;
                    return;
                }

                DBC.DB_BeginTransaction();

                View_LayOut_Out.h_status_value = StdGlobalConstant.MP_FAIL;

                MWIPFACDEF.Init();
                MWIPFACDEF.FACTORY = View_LayOut_In.h_factory;

                if (MWIPFACDEF.SelectData(1, ref adoFacTable) == false)
                {
                    if (DBC.gErrors.SqlCode == SQL_CODE.SQL_NOT_FOUND)
                    {
                        View_LayOut_Out.h_status_value = StdGlobalConstant.MP_SUCCESS;
                        return;
                    }
                    View_LayOut_Out.h_msg = DBC.gErrors.SqlCode.ToString();
                    View_LayOut_Out.h_db_err_msg = DBC.gErrors.ErrMsg;
                    DBC.DB_Rollback();
                    return;
                }

                MFMBFACLYT.Init();
                MFMBFACLYT.FACTORY = View_LayOut_In.h_factory;
                MFMBFACLYT.LAYOUT_ID = View_LayOut_In.layout_id;

                if (MFMBFACLYT.SelectData(1, ref adoGroupTable) == false)
                {
                    if (DBC.gErrors.SqlCode == SQL_CODE.SQL_NOT_FOUND)
                    {
                        View_LayOut_Out.h_status_value = StdGlobalConstant.MP_SUCCESS;
                        return;
                    }
                    View_LayOut_Out.h_msg = DBC.gErrors.SqlCode.ToString();
                    View_LayOut_Out.h_db_err_msg = DBC.gErrors.ErrMsg;
                    DBC.DB_Rollback();
                    return;
                }
                
                View_LayOut_Out.layout_id = MFMBFACLYT.LAYOUT_ID;
                View_LayOut_Out.layout_desc = MFMBFACLYT.LAYOUT_DESC;
                View_LayOut_Out.create_user_id = MFMBFACLYT.CREATE_USER_ID;
                View_LayOut_Out.create_time = MFMBFACLYT.CREATE_TIME;
                View_LayOut_Out.update_user_id = MFMBFACLYT.UPDATE_USER_ID;
                View_LayOut_Out.update_time = MFMBFACLYT.UPDATE_TIME;
                View_LayOut_Out.width = MFMBFACLYT.WIDTH;
                View_LayOut_Out.height = MFMBFACLYT.HEIGHT;

                View_LayOut_Out.h_status_value = StdGlobalConstant.MP_SUCCESS;

            }
            catch (Exception ex)
            {
                View_LayOut_Out.h_field_msg = ex.Message;
                return;
            }
            finally
            {
                DBC.DB_Close();

            }
        }
    }

}
