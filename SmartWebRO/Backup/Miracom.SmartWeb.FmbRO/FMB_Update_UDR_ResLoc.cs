using System;
using System.Data;
using System.Data.OleDb;
using Miracom.SmartWeb.FWX;
using Miracom.SmartWeb;
namespace Miracom.SmartWeb.RO
{
    public partial class FMBTransferImp : FMBTransfer
    {
        public override void FMB_Update_UDR_ResLoc(FMB_Update_UDR_ResLoc_In_Tag Update_UDR_ResLoc_In, ref FMB_Cmn_Out_Tag Cmn_Out)
        {
            string sSysTime = String.Empty;

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

                DBC.DB_GetSysTime(ref sSysTime);

                MFMBUDRLOC.Init();

                MFMBUDRLOC.FACTORY = Update_UDR_ResLoc_In._C.h_factory;
                MFMBUDRLOC.GROUP_ID = Update_UDR_ResLoc_In._C.group; 
                MFMBUDRLOC.RES_ID = Update_UDR_ResLoc_In._C.res_id;
                MFMBUDRLOC.RES_TYPE = Update_UDR_ResLoc_In._C.res_type.ToString();
                
                if (MFMBUDRLOC.SelectDataForUpdate(1, ref adoDataTable) == false)
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

                if (Update_UDR_ResLoc_In._C.h_proc_step == StdGlobalConstant.MP_STEP_CREATE)
                {
                    MFMBUDRLOC.Init();

                    MFMBUDRLOC.FACTORY = Update_UDR_ResLoc_In._C.h_factory;
                    MFMBUDRLOC.GROUP_ID = Update_UDR_ResLoc_In._C.group;

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

                    if (MFMBUDRLOC.SEQ == 0)
                    {
                        MFMBUDRLOC.SEQ = 1;
                    }
                    else
                    {
                        MFMBUDRLOC.SEQ = MFMBUDRLOC.SEQ + 1;
                    }
                }

                MFMBUDRLOC.FACTORY = Update_UDR_ResLoc_In._C.h_factory;
                MFMBUDRLOC.GROUP_ID = Update_UDR_ResLoc_In._C.group;
                MFMBUDRLOC.RES_ID = Update_UDR_ResLoc_In._C.res_id;
                MFMBUDRLOC.RES_TYPE = Update_UDR_ResLoc_In._C.res_type.ToString();
                MFMBUDRLOC.LOC_X = Update_UDR_ResLoc_In._C.loc_x;
                MFMBUDRLOC.LOC_Y = Update_UDR_ResLoc_In._C.loc_y;
                MFMBUDRLOC.LOC_WIDTH = Update_UDR_ResLoc_In._C.loc_width;
                MFMBUDRLOC.LOC_HEIGHT = Update_UDR_ResLoc_In._C.loc_height;
                MFMBUDRLOC.TEXT = Update_UDR_ResLoc_In._C.text;
                MFMBUDRLOC.TEXT_SIZE = Update_UDR_ResLoc_In._C.text_size;
                MFMBUDRLOC.TEXT_COLOR = Update_UDR_ResLoc_In._C.text_color;
                MFMBUDRLOC.TEXT_STYLE = Update_UDR_ResLoc_In._C.text_style.ToString();
                MFMBUDRLOC.TAG_TYPE = Update_UDR_ResLoc_In._C.tag_type;
                MFMBUDRLOC.BACK_COLOR = Update_UDR_ResLoc_In._C.back_color;
                MFMBUDRLOC.NO_MOUSE_EVENT = Update_UDR_ResLoc_In._C.no_mouse_event.ToString();
                MFMBUDRLOC.SIGNAL_FLAG = Update_UDR_ResLoc_In._C.signal_flag.ToString();

                if (Update_UDR_ResLoc_In._C.h_proc_step == StdGlobalConstant.MP_STEP_CREATE)
                {
                    MFMBUDRLOC.CREATE_TIME = sSysTime;

                    if (MFMBUDRLOC.InsertData() == false)
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
                else if (Update_UDR_ResLoc_In._C.h_proc_step == StdGlobalConstant.MP_STEP_UPDATE)
                {
                    MFMBUDRLOC.UPDATE_TIME = sSysTime;

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
                else if (Update_UDR_ResLoc_In._C.h_proc_step == StdGlobalConstant.MP_STEP_DELETE)
                {
                    if (MFMBUDRLOC.DeleteData(1) == false)
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
