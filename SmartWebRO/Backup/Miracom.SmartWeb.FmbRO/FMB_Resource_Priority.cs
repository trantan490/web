using System;
using System.Data;
using System.Data.OleDb;
using Miracom.SmartWeb.FWX;
using Miracom.SmartWeb;

namespace Miracom.SmartWeb.RO
{
    public partial class FMBTransferImp : FMBTransfer
    {
        public override void FMB_Resource_Priority(FMB_Resource_Priority_In_Tag Resource_Priority_In, ref FMB_Cmn_Out_Tag Cmn_Out)
        {
            DataTable adoDataTable = null;
            DB_Common DBC = new DB_Common();

            DBC_MFMBRESLOC MFMBRESLOC = new DBC_MFMBRESLOC(ref DBC);

            try
            {

                DBC.gDBConnectionString = StdGlobalVariable.ROXml.GetValue("//appSettings//add[@key='MESConnectionString']");

                if (DBC.OpenDB() != true)
                {
                    Cmn_Out.h_db_err_msg = DBC.gErrors.ErrMsg;
                    return;
                }

                DBC.DB_BeginTransaction();

                Cmn_Out.h_status_value = StdGlobalConstant.MP_FAIL;

                MFMBRESLOC.Init();

                MFMBRESLOC.FACTORY = Resource_Priority_In._C.h_factory;
                MFMBRESLOC.RES_ID = Resource_Priority_In._C.res_id;
                MFMBRESLOC.RES_TYPE = Resource_Priority_In._C.res_type.ToString();
                MFMBRESLOC.LAYOUT_ID = Resource_Priority_In._C.layout_id;

                if (MFMBRESLOC.SelectData(1, ref adoDataTable) == false)
                {
                    if (DBC.gErrors.SqlCode == SQL_CODE.SQL_NOT_FOUND)
                    {
                        Cmn_Out.h_status_value = StdGlobalConstant.MP_SUCCESS;
                        return;
                    }
                    Cmn_Out.h_msg = DBC.gErrors.SqlCode.ToString();
                    Cmn_Out.h_db_err_msg = DBC.gErrors.ErrMsg;
                    DBC.DB_Rollback();
                    return;
                }

                /*Bring to Front*/
                if (Resource_Priority_In._C.h_proc_step == '1')
                {
                    if (MFMBRESLOC.UpdateData(5) == false)
                    {
                        if (DBC.gErrors.SqlCode == SQL_CODE.SQL_NOT_FOUND)
                        {
                            Cmn_Out.h_status_value = StdGlobalConstant.MP_SUCCESS;
                            return;
                        }
                        Cmn_Out.h_msg = DBC.gErrors.SqlCode.ToString();
                        Cmn_Out.h_db_err_msg = DBC.gErrors.ErrMsg;
                        DBC.DB_Rollback();
                        return;
                    }

                    if (MFMBRESLOC.SelectData(3, ref adoDataTable) == false)
                    {
                        if (DBC.gErrors.SqlCode == SQL_CODE.SQL_NOT_FOUND)
                        {
                            Cmn_Out.h_status_value = StdGlobalConstant.MP_SUCCESS;
                            return;
                        }
                        Cmn_Out.h_msg = DBC.gErrors.SqlCode.ToString();
                        Cmn_Out.h_db_err_msg = DBC.gErrors.ErrMsg;
                        DBC.DB_Rollback();
                        return;
                    }

                    MFMBRESLOC.SEQ = MFMBRESLOC.SEQ + 1;

                    if (MFMBRESLOC.UpdateData(1) == false)
                    {
                        if (DBC.gErrors.SqlCode == SQL_CODE.SQL_NOT_FOUND)
                        {
                            Cmn_Out.h_status_value = StdGlobalConstant.MP_SUCCESS;
                            return;
                        }
                        Cmn_Out.h_msg = DBC.gErrors.SqlCode.ToString();
                        Cmn_Out.h_db_err_msg = DBC.gErrors.ErrMsg;
                        DBC.DB_Rollback();
                        return;
                    }

                }
                /*Send to Back*/
                else if (Resource_Priority_In._C.h_proc_step == '2')
                {
                    if (MFMBRESLOC.UpdateData(6) == false)
                    {
                        if (DBC.gErrors.SqlCode == SQL_CODE.SQL_NOT_FOUND)
                        {
                            Cmn_Out.h_status_value = StdGlobalConstant.MP_SUCCESS;
                            return;
                        }
                        Cmn_Out.h_msg = DBC.gErrors.SqlCode.ToString();
                        Cmn_Out.h_db_err_msg = DBC.gErrors.ErrMsg;
                        DBC.DB_Rollback();
                        return;
                    }

                    MFMBRESLOC.SEQ = 1;
                    if (MFMBRESLOC.UpdateData(1) == false)
                    {
                        if (DBC.gErrors.SqlCode == SQL_CODE.SQL_NOT_FOUND)
                        {
                            Cmn_Out.h_status_value = StdGlobalConstant.MP_SUCCESS;
                            return;
                        }
                        Cmn_Out.h_msg = DBC.gErrors.SqlCode.ToString();
                        Cmn_Out.h_db_err_msg = DBC.gErrors.ErrMsg;
                        DBC.DB_Rollback();
                        return;
                    }
                }

                Cmn_Out.h_status_value = StdGlobalConstant.MP_SUCCESS;

            }
            catch (Exception ex)
            {
                Cmn_Out.h_field_msg = ex.Message;
                return;
            }
            finally
            {
                DBC.DB_Close();

            }
        }

    }
        
}
