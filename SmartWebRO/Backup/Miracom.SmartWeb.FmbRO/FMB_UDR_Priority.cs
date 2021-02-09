using System;
using System.Data;
using System.Data.OleDb;
using Miracom.SmartWeb.FWX;
using Miracom.SmartWeb;

namespace Miracom.SmartWeb.RO
{
    public partial class FMBTransferImp : FMBTransfer
    {
        public override void FMB_UDR_Priority(FMB_UDR_Priority_In_Tag UDR_Priority_In, ref FMB_Cmn_Out_Tag Cmn_Out)
        {
            DataTable adoDataTable = null;
            DB_Common DBC = new DB_Common();

            DBC_MFMBUDRLOC MFMBUDRLOC = new DBC_MFMBUDRLOC(ref DBC);

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

                MFMBUDRLOC.Init();
                
                MFMBUDRLOC.FACTORY = UDR_Priority_In._C.h_factory;
                MFMBUDRLOC.GROUP_ID = UDR_Priority_In._C.group;
                MFMBUDRLOC.RES_ID = UDR_Priority_In._C.res_id;
                MFMBUDRLOC.RES_TYPE = UDR_Priority_In._C.res_type.ToString();

                if (MFMBUDRLOC.SelectData(1, ref adoDataTable) == false)
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
                if (UDR_Priority_In._C.h_proc_step == '1')
                {
                    if (MFMBUDRLOC.UpdateData(4) == false)
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
                    
                    if (MFMBUDRLOC.SelectData(2, ref adoDataTable) == false)
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
                    
                    MFMBUDRLOC.SEQ = MFMBUDRLOC.SEQ +1 ;
                    
                    if (MFMBUDRLOC.UpdateData(1) == false)
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
                else if(UDR_Priority_In._C.h_proc_step == '2')
                {
                    if (MFMBUDRLOC.UpdateData(5) == false)
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
                    
                    MFMBUDRLOC.SEQ = 1 ;
                    if (MFMBUDRLOC.UpdateData(1) == false)
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
