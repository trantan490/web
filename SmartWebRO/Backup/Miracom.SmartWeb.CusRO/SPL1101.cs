/*******************************************************************************
' SECTuner.vb
'
' Copyright (c) 2007 by Miracom,Inc.
' All rights reserved.
'
' Generated by DevTool XMLGen 1.0
'
' Created at 2008-04-28 10:08:03
'
' Author : Miracom. R&D.
' Description : DevTool Xml Generator Version 1.0
'******************************************************************************/

using System;
using System.Data;
using System.Data.OleDb;
using Miracom.SmartWeb.FWX;

namespace Miracom.SmartWeb.RO
{

    public partial class SPLTransferImp : SPLTransfer
    {
        public override void SPL_View_Lot_List(SPL_View_Lot_List_In_Tag View_Lot_List_In, ref SPL_View_Lot_List_Out_Tag View_Lot_List_Out)
        {

            DataTable adoDataTable = null;

            DB_Common DBC = new DB_Common();
            DBC_MWIPLOTSTS MWIPLOTSTS = new DBC_MWIPLOTSTS(ref DBC);

            try
            {
                DBC.gDBConnectionString = StdGlobalVariable.ROXml.GetValue("//appSettings//add[@key='MESConnectionString']");
                //DBC.gDBConnectionString = "Provider=OraOLEDB.Oracle.1;Password=mesmgr_v42;Persist Security Info=True;User ID=mesmgr_v42;Data Source=ORAMES";

                if (DBC.OpenDB() != true)
                {
                    View_Lot_List_Out.h_db_err_msg = DBC.gErrors.ErrMsg;
                    return;
                }

                DBC.DB_BeginTransaction();

                MWIPLOTSTS.Init();
                MWIPLOTSTS.LOT_ID = "TESTLOT01";
                if (MWIPLOTSTS.SelectData(1, ref adoDataTable) == false)
                {
                    View_Lot_List_Out.h_msg = DBC.gErrors.SqlCode.ToString();
                    View_Lot_List_Out.h_db_err_msg = DBC.gErrors.ErrMsg;
                    DBC.DB_Rollback();
                    return;
                }

                if (MWIPLOTSTS.InsertData() == false)
                {
                    View_Lot_List_Out.h_msg = DBC.gErrors.SqlCode.ToString();
                    View_Lot_List_Out.h_db_err_msg = DBC.gErrors.ErrMsg;
                    DBC.DB_Rollback();
                    return;
                }
                View_Lot_List_Out.next_lot_id = "�ǽ��ϸ��׾�";
                DBC.DB_Rollback();

                return;
            }
            catch (Exception ex)
            {
                View_Lot_List_Out.h_field_msg = ex.Message;
                return;
            }
            finally
            {
                DBC.DB_Close();

            }

            return;
        }

    }
}

