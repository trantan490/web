using System;
using System.Data;
using System.Data.OleDb;
using Miracom.SmartWeb.FWX;
using Miracom.SmartWeb;

namespace Miracom.SmartWeb.RO
{
    public partial class FMBTransferImp : FMBTransfer
    {
        public override void FMB_Update_Resource_Location(FMB_Update_ResLoc_In_Tag Update_ResLoc_In, ref FMB_Cmn_Out_Tag Cmn_Out)
        {
            string sSysTime = String.Empty;

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

                DBC.DB_GetSysTime(ref sSysTime);

                if (Update_ResLoc_In._C.res_type == 'R')
                {
                    MFMBRESLOC.Init();

                    MFMBRESLOC.FACTORY = Update_ResLoc_In._C.h_factory;
                    MFMBRESLOC.RES_ID = Update_ResLoc_In._C.res_id;
                    MFMBRESLOC.RES_TYPE = Update_ResLoc_In._C.res_type.ToString();

                    if (MFMBRESLOC.SelectDataForUpdate(2, ref adoDataTable) == false)
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

                    if(Update_ResLoc_In._C.h_proc_step == StdGlobalConstant.MP_STEP_CREATE)
                    {
                        MFMBRESLOC.Init();

                        MFMBRESLOC.FACTORY = Update_ResLoc_In._C.h_factory;
                        MFMBRESLOC.LAYOUT_ID = Update_ResLoc_In._C.layout_id;
                        
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

                        if(MFMBRESLOC.SEQ == 0)
                        {
                            MFMBRESLOC.SEQ = 1;
                        }
                        else
                        {
                            MFMBRESLOC.SEQ = MFMBRESLOC.SEQ + 1;
                        }
                    }

                    MFMBRESLOC.FACTORY = Update_ResLoc_In._C.h_factory;
                    MFMBRESLOC.RES_ID = Update_ResLoc_In._C.res_id;
                    MFMBRESLOC.RES_TYPE =  Update_ResLoc_In._C.res_type.ToString();
                    MFMBRESLOC.LAYOUT_ID = Update_ResLoc_In._C.layout_id;
                    MFMBRESLOC.LOC_X = Update_ResLoc_In._C.loc_x;
                    MFMBRESLOC.LOC_Y = Update_ResLoc_In._C.loc_y;
                    MFMBRESLOC.LOC_WIDTH = Update_ResLoc_In._C.loc_width;
                    MFMBRESLOC.LOC_HEIGHT = Update_ResLoc_In._C.loc_height;
                    MFMBRESLOC.TEXT = Update_ResLoc_In._C.text;
                    MFMBRESLOC.TEXT_SIZE = Update_ResLoc_In._C.text_size;
                    MFMBRESLOC.TEXT_COLOR = Update_ResLoc_In._C.text_color;
                    MFMBRESLOC.TEXT_STYLE = Update_ResLoc_In._C.text_style.ToString();
                    MFMBRESLOC.TAG_TYPE = Update_ResLoc_In._C.tag_type;
                    MFMBRESLOC.BACK_COLOR = Update_ResLoc_In._C.back_color;
                    MFMBRESLOC.NO_MOUSE_EVENT = Update_ResLoc_In._C.no_mouse_event.ToString();
                    MFMBRESLOC.SIGNAL_FLAG = Update_ResLoc_In._C.signal_flag.ToString();

                    if(Update_ResLoc_In._C.h_proc_step == StdGlobalConstant.MP_STEP_CREATE)
                    {
                        MFMBRESLOC.CREATE_USER_ID = Update_ResLoc_In._C.h_user_id;
                        MFMBRESLOC.CREATE_TIME = sSysTime;
                        
                        if (MFMBRESLOC.InsertData() == false)
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
                    else if (Update_ResLoc_In._C.h_proc_step == StdGlobalConstant.MP_STEP_UPDATE)
                    {
                        MFMBRESLOC.UPDATE_USER_ID = Update_ResLoc_In._C.h_user_id;
                        MFMBRESLOC.UPDATE_TIME = sSysTime;
                        
                        if (MFMBRESLOC.UpdateData(2) == false)
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
                    else if (Update_ResLoc_In._C.h_proc_step == StdGlobalConstant.MP_STEP_DELETE)
                    {
                        if (MFMBRESLOC.DeleteData(3) == false)
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

                    }

                }
                else if(Update_ResLoc_In._C.res_type == 'T')
                {
                    MFMBRESLOC.Init();

                    MFMBRESLOC.FACTORY = Update_ResLoc_In._C.h_factory;
                    MFMBRESLOC.RES_ID = Update_ResLoc_In._C.res_id;
                    MFMBRESLOC.RES_TYPE = Update_ResLoc_In._C.res_type.ToString();
                    MFMBRESLOC.LAYOUT_ID = Update_ResLoc_In._C.layout_id;

                    if (MFMBRESLOC.SelectDataForUpdate(1, ref adoDataTable) == false)
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

                    if (Update_ResLoc_In._C.h_proc_step == StdGlobalConstant.MP_STEP_CREATE)
                    {
                        MFMBRESLOC.Init();

                        MFMBRESLOC.FACTORY = Update_ResLoc_In._C.h_factory;
                        MFMBRESLOC.LAYOUT_ID = Update_ResLoc_In._C.layout_id;

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

                        if (MFMBRESLOC.SEQ == 0)
                        {
                            MFMBRESLOC.SEQ = 1;
                        }
                        else
                        {
                            MFMBRESLOC.SEQ = MFMBRESLOC.SEQ + 1;
                        }
                    }

                    MFMBRESLOC.FACTORY = Update_ResLoc_In._C.h_factory;
                    MFMBRESLOC.RES_ID = Update_ResLoc_In._C.res_id;
                    MFMBRESLOC.RES_TYPE = Update_ResLoc_In._C.res_type.ToString();
                    MFMBRESLOC.LAYOUT_ID = Update_ResLoc_In._C.layout_id;
                    MFMBRESLOC.LOC_X = Update_ResLoc_In._C.loc_x;
                    MFMBRESLOC.LOC_Y = Update_ResLoc_In._C.loc_y;
                    MFMBRESLOC.LOC_WIDTH = Update_ResLoc_In._C.loc_width;
                    MFMBRESLOC.LOC_HEIGHT = Update_ResLoc_In._C.loc_height;
                    MFMBRESLOC.TEXT = Update_ResLoc_In._C.text;
                    MFMBRESLOC.TEXT_SIZE = Update_ResLoc_In._C.text_size;
                    MFMBRESLOC.TEXT_COLOR = Update_ResLoc_In._C.text_color;
                    MFMBRESLOC.TEXT_STYLE = Update_ResLoc_In._C.text_style.ToString();
                    MFMBRESLOC.TAG_TYPE = Update_ResLoc_In._C.tag_type;
                    MFMBRESLOC.BACK_COLOR = Update_ResLoc_In._C.back_color;
                    MFMBRESLOC.NO_MOUSE_EVENT = Update_ResLoc_In._C.no_mouse_event.ToString();
                    MFMBRESLOC.SIGNAL_FLAG = Update_ResLoc_In._C.signal_flag.ToString();

                    if (Update_ResLoc_In._C.h_proc_step == StdGlobalConstant.MP_STEP_CREATE)
                    {
                        MFMBRESLOC.CREATE_USER_ID = Update_ResLoc_In._C.h_user_id;
                        MFMBRESLOC.CREATE_TIME = sSysTime;

                        if (MFMBRESLOC.InsertData() == false)
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
                    else if (Update_ResLoc_In._C.h_proc_step == StdGlobalConstant.MP_STEP_UPDATE)
                    {
                        MFMBRESLOC.UPDATE_USER_ID = Update_ResLoc_In._C.h_user_id;
                        MFMBRESLOC.UPDATE_TIME = sSysTime;

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
                    else if (Update_ResLoc_In._C.h_proc_step == StdGlobalConstant.MP_STEP_DELETE)
                    {
                        if (MFMBRESLOC.DeleteData(1) == false)
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
