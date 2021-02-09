//-----------------------------------------------------------------------------
//
//   System      : MES
//   File Name   : DBC_MWIPLOTSTS.cs
//   Description : MES Data Access Layer
//
//   DB Type     : Oracle
//
//   MES Version : 4.0.0
//
//   Function List
//       - New() : Creator for Object
//       - Select() : Select Table
//       -
//
//   Detail Description
//       - 
//       -
//
//   History
//       - 2008-05-07 : Created by DBLib Generator
//
//
//   Copyright(C) 1998-2007 MIRACOM,INC.
//   All rights reserved.
//
//-----------------------------------------------------------------------------

//using
using System;
using System.Data;
using System.Data.OleDb;
using Miracom.SmartWeb.FWX;

namespace Miracom.SmartWeb
{
public class DBC_MWIPLOTSTS
    : ICloneable
{


#region " Fields Definition "

    // Fields
    private string m_lot_id;                                 // VARCHAR(25)
    private string m_lot_desc;                               // VARCHAR(50)
    private string m_factory;                                // VARCHAR(10)
    private string m_mat_id;                                 // VARCHAR(30)
    private int m_mat_ver;                                   // INTEGER(6)
    private string m_flow;                                   // VARCHAR(20)
    private int m_flow_seq_num;                              // INTEGER(6)
    private string m_oper;                                   // VARCHAR(10)
    private double m_qty_1;                                  // NUMERIC(10.3)
    private double m_qty_2;                                  // NUMERIC(10.3)
    private double m_qty_3;                                  // NUMERIC(10.3)
    private string m_crr_id;                                 // VARCHAR(20)
    private string m_lot_type;                               // VARCHAR(1)
    private string m_owner_code;                             // VARCHAR(10)
    private string m_create_code;                            // VARCHAR(10)
    private string m_lot_priority;                           // VARCHAR(1)
    private string m_lot_status;                             // VARCHAR(10)
    private string m_hold_flag;                              // VARCHAR(1)
    private string m_hold_code;                              // VARCHAR(10)
    private string m_hold_password;                          // VARCHAR(20)
    private string m_hold_prv_grp_id;                        // VARCHAR(20)
    private double m_oper_in_qty_1;                          // NUMERIC(10.3)
    private double m_oper_in_qty_2;                          // NUMERIC(10.3)
    private double m_oper_in_qty_3;                          // NUMERIC(10.3)
    private double m_create_qty_1;                           // NUMERIC(10.3)
    private double m_create_qty_2;                           // NUMERIC(10.3)
    private double m_create_qty_3;                           // NUMERIC(10.3)
    private double m_start_qty_1;                            // NUMERIC(10.3)
    private double m_start_qty_2;                            // NUMERIC(10.3)
    private double m_start_qty_3;                            // NUMERIC(10.3)
    private string m_inv_flag;                               // VARCHAR(1)
    private string m_transit_flag;                           // VARCHAR(1)
    private string m_unit_exist_flag;                        // VARCHAR(1)
    private string m_inv_unit;                               // VARCHAR(10)
    private string m_rwk_flag;                               // VARCHAR(1)
    private string m_rwk_code;                               // VARCHAR(10)
    private int m_rwk_count;                                 // INTEGER(6)
    private string m_rwk_ret_flow;                           // VARCHAR(20)
    private int m_rwk_ret_flow_seq_num;                      // INTEGER(6)
    private string m_rwk_ret_oper;                           // VARCHAR(10)
    private string m_rwk_end_flow;                           // VARCHAR(20)
    private int m_rwk_end_flow_seq_num;                      // INTEGER(6)
    private string m_rwk_end_oper;                           // VARCHAR(10)
    private string m_rwk_ret_clear_flag;                     // VARCHAR(1)
    private string m_rwk_time;                               // VARCHAR(14)
    private string m_nstd_flag;                              // VARCHAR(1)
    private string m_nstd_ret_flow;                          // VARCHAR(20)
    private int m_nstd_ret_flow_seq_num;                     // INTEGER(6)
    private string m_nstd_ret_oper;                          // VARCHAR(10)
    private string m_nstd_time;                              // VARCHAR(14)
    private string m_rep_flag;                               // VARCHAR(1)
    private string m_rep_ret_oper;                           // VARCHAR(10)
    private string m_start_flag;                             // VARCHAR(1)
    private string m_start_time;                             // VARCHAR(14)
    private string m_start_res_id;                           // VARCHAR(20)
    private string m_end_flag;                               // VARCHAR(1)
    private string m_end_time;                               // VARCHAR(14)
    private string m_end_res_id;                             // VARCHAR(20)
    private string m_sample_flag;                            // VARCHAR(1)
    private string m_sample_wait_flag;                       // VARCHAR(1)
    private string m_sample_result;                          // VARCHAR(1)
    private string m_from_to_flag;                           // VARCHAR(1)
    private string m_from_to_lot_id;                         // VARCHAR(25)
    private string m_ship_code;                              // VARCHAR(10)
    private string m_ship_time;                              // VARCHAR(14)
    private string m_org_due_time;                           // VARCHAR(14)
    private string m_sch_due_time;                           // VARCHAR(14)
    private string m_create_time;                            // VARCHAR(14)
    private string m_fac_in_time;                            // VARCHAR(14)
    private string m_flow_in_time;                           // VARCHAR(14)
    private string m_oper_in_time;                           // VARCHAR(14)
    private string m_reserve_res_id;                         // VARCHAR(20)
    private string m_batch_id;                               // VARCHAR(25)
    private int m_batch_seq;                                 // INTEGER(3)
    private string m_order_id;                               // VARCHAR(25)
    private string m_add_order_id_1;                         // VARCHAR(25)
    private string m_add_order_id_2;                         // VARCHAR(25)
    private string m_add_order_id_3;                         // VARCHAR(25)
    private string m_lot_location;                           // VARCHAR(20)
    private string m_lot_cmf_1;                              // VARCHAR(30)
    private string m_lot_cmf_2;                              // VARCHAR(30)
    private string m_lot_cmf_3;                              // VARCHAR(30)
    private string m_lot_cmf_4;                              // VARCHAR(30)
    private string m_lot_cmf_5;                              // VARCHAR(30)
    private string m_lot_cmf_6;                              // VARCHAR(30)
    private string m_lot_cmf_7;                              // VARCHAR(30)
    private string m_lot_cmf_8;                              // VARCHAR(30)
    private string m_lot_cmf_9;                              // VARCHAR(30)
    private string m_lot_cmf_10;                             // VARCHAR(30)
    private string m_lot_cmf_11;                             // VARCHAR(30)
    private string m_lot_cmf_12;                             // VARCHAR(30)
    private string m_lot_cmf_13;                             // VARCHAR(30)
    private string m_lot_cmf_14;                             // VARCHAR(30)
    private string m_lot_cmf_15;                             // VARCHAR(30)
    private string m_lot_cmf_16;                             // VARCHAR(30)
    private string m_lot_cmf_17;                             // VARCHAR(30)
    private string m_lot_cmf_18;                             // VARCHAR(30)
    private string m_lot_cmf_19;                             // VARCHAR(30)
    private string m_lot_cmf_20;                             // VARCHAR(30)
    private string m_lot_del_flag;                           // VARCHAR(1)
    private string m_lot_del_code;                           // VARCHAR(10)
    private string m_lot_del_time;                           // VARCHAR(14)
    private string m_bom_set_id;                             // VARCHAR(25)
    private int m_bom_set_version;                           // INTEGER(3)
    private int m_bom_active_hist_seq;                       // INTEGER(10)
    private int m_bom_hist_seq;                              // INTEGER(10)
    private string m_last_tran_code;                         // VARCHAR(12)
    private string m_last_tran_time;                         // VARCHAR(14)
    private string m_last_comment;                           // VARCHAR(400)
    private int m_last_active_hist_seq;                      // INTEGER(10)
    private int m_last_hist_seq;                             // INTEGER(10)
    private string m_critical_res_id;                        // VARCHAR(20)
    private string m_critical_res_group_id;                  // VARCHAR(20)
    private string m_save_res_id_1;                          // VARCHAR(20)
    private string m_save_res_id_2;                          // VARCHAR(20)
    private string m_subres_id;                              // VARCHAR(20)
    private string m_lot_group_id_1;                         // VARCHAR(25)
    private string m_lot_group_id_2;                         // VARCHAR(25)
    private string m_lot_group_id_3;                         // VARCHAR(25)
    private string m_resv_field_1;                           // VARCHAR(30)
    private string m_resv_field_2;                           // VARCHAR(30)
    private string m_resv_field_3;                           // VARCHAR(30)
    private string m_resv_field_4;                           // VARCHAR(30)
    private string m_resv_field_5;                           // VARCHAR(30)
    private string m_resv_flag_1;                            // VARCHAR(1)
    private string m_resv_flag_2;                            // VARCHAR(1)
    private string m_resv_flag_3;                            // VARCHAR(1)
    private string m_resv_flag_4;                            // VARCHAR(1)
    private string m_resv_flag_5;                            // VARCHAR(1)

#endregion

#region " Variable Definition "
    DB_Common _dbc;      //DB Connection
    // TODO : Declare Variable for Query Condition

#endregion

#region " Property Definition "

    /// <summary>
    /// Gets or sets the 'LOT_ID' value.
    /// </summary>
    public string LOT_ID
    {
        get
        {
            if (m_lot_id == null )
            {
                m_lot_id = " ";
            }
            return m_lot_id;
        }
        set
        {
            if (value == null || value == "" )
            {
                m_lot_id = " ";
            }
            m_lot_id = value;
        }
    }

    /// <summary>
    /// Gets or sets the 'LOT_DESC' value.
    /// </summary>
    public string LOT_DESC
    {
        get
        {
            if (m_lot_desc == null )
            {
                m_lot_desc = " ";
            }
            return m_lot_desc;
        }
        set
        {
            if (value == null || value == "" )
            {
                m_lot_desc = " ";
            }
            m_lot_desc = value;
        }
    }

    /// <summary>
    /// Gets or sets the 'FACTORY' value.
    /// </summary>
    public string FACTORY
    {
        get
        {
            if (m_factory == null )
            {
                m_factory = " ";
            }
            return m_factory;
        }
        set
        {
            if (value == null || value == "" )
            {
                m_factory = " ";
            }
            m_factory = value;
        }
    }

    /// <summary>
    /// Gets or sets the 'MAT_ID' value.
    /// </summary>
    public string MAT_ID
    {
        get
        {
            if (m_mat_id == null )
            {
                m_mat_id = " ";
            }
            return m_mat_id;
        }
        set
        {
            if (value == null || value == "" )
            {
                m_mat_id = " ";
            }
            m_mat_id = value;
        }
    }

    /// <summary>
    /// Gets or sets the 'MAT_VER' value.
    /// </summary>
    public int MAT_VER
    {
        get
        {
            return m_mat_ver;
        }
        set
        {
            m_mat_ver = value;
        }
    }

    /// <summary>
    /// Gets or sets the 'FLOW' value.
    /// </summary>
    public string FLOW
    {
        get
        {
            if (m_flow == null )
            {
                m_flow = " ";
            }
            return m_flow;
        }
        set
        {
            if (value == null || value == "" )
            {
                m_flow = " ";
            }
            m_flow = value;
        }
    }

    /// <summary>
    /// Gets or sets the 'FLOW_SEQ_NUM' value.
    /// </summary>
    public int FLOW_SEQ_NUM
    {
        get
        {
            return m_flow_seq_num;
        }
        set
        {
            m_flow_seq_num = value;
        }
    }

    /// <summary>
    /// Gets or sets the 'OPER' value.
    /// </summary>
    public string OPER
    {
        get
        {
            if (m_oper == null )
            {
                m_oper = " ";
            }
            return m_oper;
        }
        set
        {
            if (value == null || value == "" )
            {
                m_oper = " ";
            }
            m_oper = value;
        }
    }

    /// <summary>
    /// Gets or sets the 'QTY_1' value.
    /// </summary>
    public double QTY_1
    {
        get
        {
            return m_qty_1;
        }
        set
        {
            m_qty_1 = value;
        }
    }

    /// <summary>
    /// Gets or sets the 'QTY_2' value.
    /// </summary>
    public double QTY_2
    {
        get
        {
            return m_qty_2;
        }
        set
        {
            m_qty_2 = value;
        }
    }

    /// <summary>
    /// Gets or sets the 'QTY_3' value.
    /// </summary>
    public double QTY_3
    {
        get
        {
            return m_qty_3;
        }
        set
        {
            m_qty_3 = value;
        }
    }

    /// <summary>
    /// Gets or sets the 'CRR_ID' value.
    /// </summary>
    public string CRR_ID
    {
        get
        {
            if (m_crr_id == null )
            {
                m_crr_id = " ";
            }
            return m_crr_id;
        }
        set
        {
            if (value == null || value == "" )
            {
                m_crr_id = " ";
            }
            m_crr_id = value;
        }
    }

    /// <summary>
    /// Gets or sets the 'LOT_TYPE' value.
    /// </summary>
    public string LOT_TYPE
    {
        get
        {
            if (m_lot_type == null )
            {
                m_lot_type = " ";
            }
            return m_lot_type;
        }
        set
        {
            if (value == null || value == "" )
            {
                m_lot_type = " ";
            }
            m_lot_type = value;
        }
    }

    /// <summary>
    /// Gets or sets the 'OWNER_CODE' value.
    /// </summary>
    public string OWNER_CODE
    {
        get
        {
            if (m_owner_code == null )
            {
                m_owner_code = " ";
            }
            return m_owner_code;
        }
        set
        {
            if (value == null || value == "" )
            {
                m_owner_code = " ";
            }
            m_owner_code = value;
        }
    }

    /// <summary>
    /// Gets or sets the 'CREATE_CODE' value.
    /// </summary>
    public string CREATE_CODE
    {
        get
        {
            if (m_create_code == null )
            {
                m_create_code = " ";
            }
            return m_create_code;
        }
        set
        {
            if (value == null || value == "" )
            {
                m_create_code = " ";
            }
            m_create_code = value;
        }
    }

    /// <summary>
    /// Gets or sets the 'LOT_PRIORITY' value.
    /// </summary>
    public string LOT_PRIORITY
    {
        get
        {
            if (m_lot_priority == null )
            {
                m_lot_priority = " ";
            }
            return m_lot_priority;
        }
        set
        {
            if (value == null || value == "" )
            {
                m_lot_priority = " ";
            }
            m_lot_priority = value;
        }
    }

    /// <summary>
    /// Gets or sets the 'LOT_STATUS' value.
    /// </summary>
    public string LOT_STATUS
    {
        get
        {
            if (m_lot_status == null )
            {
                m_lot_status = " ";
            }
            return m_lot_status;
        }
        set
        {
            if (value == null || value == "" )
            {
                m_lot_status = " ";
            }
            m_lot_status = value;
        }
    }

    /// <summary>
    /// Gets or sets the 'HOLD_FLAG' value.
    /// </summary>
    public string HOLD_FLAG
    {
        get
        {
            if (m_hold_flag == null )
            {
                m_hold_flag = " ";
            }
            return m_hold_flag;
        }
        set
        {
            if (value == null || value == "" )
            {
                m_hold_flag = " ";
            }
            m_hold_flag = value;
        }
    }

    /// <summary>
    /// Gets or sets the 'HOLD_CODE' value.
    /// </summary>
    public string HOLD_CODE
    {
        get
        {
            if (m_hold_code == null )
            {
                m_hold_code = " ";
            }
            return m_hold_code;
        }
        set
        {
            if (value == null || value == "" )
            {
                m_hold_code = " ";
            }
            m_hold_code = value;
        }
    }

    /// <summary>
    /// Gets or sets the 'HOLD_PASSWORD' value.
    /// </summary>
    public string HOLD_PASSWORD
    {
        get
        {
            if (m_hold_password == null )
            {
                m_hold_password = " ";
            }
            return m_hold_password;
        }
        set
        {
            if (value == null || value == "" )
            {
                m_hold_password = " ";
            }
            m_hold_password = value;
        }
    }

    /// <summary>
    /// Gets or sets the 'HOLD_PRV_GRP_ID' value.
    /// </summary>
    public string HOLD_PRV_GRP_ID
    {
        get
        {
            if (m_hold_prv_grp_id == null )
            {
                m_hold_prv_grp_id = " ";
            }
            return m_hold_prv_grp_id;
        }
        set
        {
            if (value == null || value == "" )
            {
                m_hold_prv_grp_id = " ";
            }
            m_hold_prv_grp_id = value;
        }
    }

    /// <summary>
    /// Gets or sets the 'OPER_IN_QTY_1' value.
    /// </summary>
    public double OPER_IN_QTY_1
    {
        get
        {
            return m_oper_in_qty_1;
        }
        set
        {
            m_oper_in_qty_1 = value;
        }
    }

    /// <summary>
    /// Gets or sets the 'OPER_IN_QTY_2' value.
    /// </summary>
    public double OPER_IN_QTY_2
    {
        get
        {
            return m_oper_in_qty_2;
        }
        set
        {
            m_oper_in_qty_2 = value;
        }
    }

    /// <summary>
    /// Gets or sets the 'OPER_IN_QTY_3' value.
    /// </summary>
    public double OPER_IN_QTY_3
    {
        get
        {
            return m_oper_in_qty_3;
        }
        set
        {
            m_oper_in_qty_3 = value;
        }
    }

    /// <summary>
    /// Gets or sets the 'CREATE_QTY_1' value.
    /// </summary>
    public double CREATE_QTY_1
    {
        get
        {
            return m_create_qty_1;
        }
        set
        {
            m_create_qty_1 = value;
        }
    }

    /// <summary>
    /// Gets or sets the 'CREATE_QTY_2' value.
    /// </summary>
    public double CREATE_QTY_2
    {
        get
        {
            return m_create_qty_2;
        }
        set
        {
            m_create_qty_2 = value;
        }
    }

    /// <summary>
    /// Gets or sets the 'CREATE_QTY_3' value.
    /// </summary>
    public double CREATE_QTY_3
    {
        get
        {
            return m_create_qty_3;
        }
        set
        {
            m_create_qty_3 = value;
        }
    }

    /// <summary>
    /// Gets or sets the 'START_QTY_1' value.
    /// </summary>
    public double START_QTY_1
    {
        get
        {
            return m_start_qty_1;
        }
        set
        {
            m_start_qty_1 = value;
        }
    }

    /// <summary>
    /// Gets or sets the 'START_QTY_2' value.
    /// </summary>
    public double START_QTY_2
    {
        get
        {
            return m_start_qty_2;
        }
        set
        {
            m_start_qty_2 = value;
        }
    }

    /// <summary>
    /// Gets or sets the 'START_QTY_3' value.
    /// </summary>
    public double START_QTY_3
    {
        get
        {
            return m_start_qty_3;
        }
        set
        {
            m_start_qty_3 = value;
        }
    }

    /// <summary>
    /// Gets or sets the 'INV_FLAG' value.
    /// </summary>
    public string INV_FLAG
    {
        get
        {
            if (m_inv_flag == null )
            {
                m_inv_flag = " ";
            }
            return m_inv_flag;
        }
        set
        {
            if (value == null || value == "" )
            {
                m_inv_flag = " ";
            }
            m_inv_flag = value;
        }
    }

    /// <summary>
    /// Gets or sets the 'TRANSIT_FLAG' value.
    /// </summary>
    public string TRANSIT_FLAG
    {
        get
        {
            if (m_transit_flag == null )
            {
                m_transit_flag = " ";
            }
            return m_transit_flag;
        }
        set
        {
            if (value == null || value == "" )
            {
                m_transit_flag = " ";
            }
            m_transit_flag = value;
        }
    }

    /// <summary>
    /// Gets or sets the 'UNIT_EXIST_FLAG' value.
    /// </summary>
    public string UNIT_EXIST_FLAG
    {
        get
        {
            if (m_unit_exist_flag == null )
            {
                m_unit_exist_flag = " ";
            }
            return m_unit_exist_flag;
        }
        set
        {
            if (value == null || value == "" )
            {
                m_unit_exist_flag = " ";
            }
            m_unit_exist_flag = value;
        }
    }

    /// <summary>
    /// Gets or sets the 'INV_UNIT' value.
    /// </summary>
    public string INV_UNIT
    {
        get
        {
            if (m_inv_unit == null )
            {
                m_inv_unit = " ";
            }
            return m_inv_unit;
        }
        set
        {
            if (value == null || value == "" )
            {
                m_inv_unit = " ";
            }
            m_inv_unit = value;
        }
    }

    /// <summary>
    /// Gets or sets the 'RWK_FLAG' value.
    /// </summary>
    public string RWK_FLAG
    {
        get
        {
            if (m_rwk_flag == null )
            {
                m_rwk_flag = " ";
            }
            return m_rwk_flag;
        }
        set
        {
            if (value == null || value == "" )
            {
                m_rwk_flag = " ";
            }
            m_rwk_flag = value;
        }
    }

    /// <summary>
    /// Gets or sets the 'RWK_CODE' value.
    /// </summary>
    public string RWK_CODE
    {
        get
        {
            if (m_rwk_code == null )
            {
                m_rwk_code = " ";
            }
            return m_rwk_code;
        }
        set
        {
            if (value == null || value == "" )
            {
                m_rwk_code = " ";
            }
            m_rwk_code = value;
        }
    }

    /// <summary>
    /// Gets or sets the 'RWK_COUNT' value.
    /// </summary>
    public int RWK_COUNT
    {
        get
        {
            return m_rwk_count;
        }
        set
        {
            m_rwk_count = value;
        }
    }

    /// <summary>
    /// Gets or sets the 'RWK_RET_FLOW' value.
    /// </summary>
    public string RWK_RET_FLOW
    {
        get
        {
            if (m_rwk_ret_flow == null )
            {
                m_rwk_ret_flow = " ";
            }
            return m_rwk_ret_flow;
        }
        set
        {
            if (value == null || value == "" )
            {
                m_rwk_ret_flow = " ";
            }
            m_rwk_ret_flow = value;
        }
    }

    /// <summary>
    /// Gets or sets the 'RWK_RET_FLOW_SEQ_NUM' value.
    /// </summary>
    public int RWK_RET_FLOW_SEQ_NUM
    {
        get
        {
            return m_rwk_ret_flow_seq_num;
        }
        set
        {
            m_rwk_ret_flow_seq_num = value;
        }
    }

    /// <summary>
    /// Gets or sets the 'RWK_RET_OPER' value.
    /// </summary>
    public string RWK_RET_OPER
    {
        get
        {
            if (m_rwk_ret_oper == null )
            {
                m_rwk_ret_oper = " ";
            }
            return m_rwk_ret_oper;
        }
        set
        {
            if (value == null || value == "" )
            {
                m_rwk_ret_oper = " ";
            }
            m_rwk_ret_oper = value;
        }
    }

    /// <summary>
    /// Gets or sets the 'RWK_END_FLOW' value.
    /// </summary>
    public string RWK_END_FLOW
    {
        get
        {
            if (m_rwk_end_flow == null )
            {
                m_rwk_end_flow = " ";
            }
            return m_rwk_end_flow;
        }
        set
        {
            if (value == null || value == "" )
            {
                m_rwk_end_flow = " ";
            }
            m_rwk_end_flow = value;
        }
    }

    /// <summary>
    /// Gets or sets the 'RWK_END_FLOW_SEQ_NUM' value.
    /// </summary>
    public int RWK_END_FLOW_SEQ_NUM
    {
        get
        {
            return m_rwk_end_flow_seq_num;
        }
        set
        {
            m_rwk_end_flow_seq_num = value;
        }
    }

    /// <summary>
    /// Gets or sets the 'RWK_END_OPER' value.
    /// </summary>
    public string RWK_END_OPER
    {
        get
        {
            if (m_rwk_end_oper == null )
            {
                m_rwk_end_oper = " ";
            }
            return m_rwk_end_oper;
        }
        set
        {
            if (value == null || value == "" )
            {
                m_rwk_end_oper = " ";
            }
            m_rwk_end_oper = value;
        }
    }

    /// <summary>
    /// Gets or sets the 'RWK_RET_CLEAR_FLAG' value.
    /// </summary>
    public string RWK_RET_CLEAR_FLAG
    {
        get
        {
            if (m_rwk_ret_clear_flag == null )
            {
                m_rwk_ret_clear_flag = " ";
            }
            return m_rwk_ret_clear_flag;
        }
        set
        {
            if (value == null || value == "" )
            {
                m_rwk_ret_clear_flag = " ";
            }
            m_rwk_ret_clear_flag = value;
        }
    }

    /// <summary>
    /// Gets or sets the 'RWK_TIME' value.
    /// </summary>
    public string RWK_TIME
    {
        get
        {
            if (m_rwk_time == null )
            {
                m_rwk_time = " ";
            }
            return m_rwk_time;
        }
        set
        {
            if (value == null || value == "" )
            {
                m_rwk_time = " ";
            }
            m_rwk_time = value;
        }
    }

    /// <summary>
    /// Gets or sets the 'NSTD_FLAG' value.
    /// </summary>
    public string NSTD_FLAG
    {
        get
        {
            if (m_nstd_flag == null )
            {
                m_nstd_flag = " ";
            }
            return m_nstd_flag;
        }
        set
        {
            if (value == null || value == "" )
            {
                m_nstd_flag = " ";
            }
            m_nstd_flag = value;
        }
    }

    /// <summary>
    /// Gets or sets the 'NSTD_RET_FLOW' value.
    /// </summary>
    public string NSTD_RET_FLOW
    {
        get
        {
            if (m_nstd_ret_flow == null )
            {
                m_nstd_ret_flow = " ";
            }
            return m_nstd_ret_flow;
        }
        set
        {
            if (value == null || value == "" )
            {
                m_nstd_ret_flow = " ";
            }
            m_nstd_ret_flow = value;
        }
    }

    /// <summary>
    /// Gets or sets the 'NSTD_RET_FLOW_SEQ_NUM' value.
    /// </summary>
    public int NSTD_RET_FLOW_SEQ_NUM
    {
        get
        {
            return m_nstd_ret_flow_seq_num;
        }
        set
        {
            m_nstd_ret_flow_seq_num = value;
        }
    }

    /// <summary>
    /// Gets or sets the 'NSTD_RET_OPER' value.
    /// </summary>
    public string NSTD_RET_OPER
    {
        get
        {
            if (m_nstd_ret_oper == null )
            {
                m_nstd_ret_oper = " ";
            }
            return m_nstd_ret_oper;
        }
        set
        {
            if (value == null || value == "" )
            {
                m_nstd_ret_oper = " ";
            }
            m_nstd_ret_oper = value;
        }
    }

    /// <summary>
    /// Gets or sets the 'NSTD_TIME' value.
    /// </summary>
    public string NSTD_TIME
    {
        get
        {
            if (m_nstd_time == null )
            {
                m_nstd_time = " ";
            }
            return m_nstd_time;
        }
        set
        {
            if (value == null || value == "" )
            {
                m_nstd_time = " ";
            }
            m_nstd_time = value;
        }
    }

    /// <summary>
    /// Gets or sets the 'REP_FLAG' value.
    /// </summary>
    public string REP_FLAG
    {
        get
        {
            if (m_rep_flag == null )
            {
                m_rep_flag = " ";
            }
            return m_rep_flag;
        }
        set
        {
            if (value == null || value == "" )
            {
                m_rep_flag = " ";
            }
            m_rep_flag = value;
        }
    }

    /// <summary>
    /// Gets or sets the 'REP_RET_OPER' value.
    /// </summary>
    public string REP_RET_OPER
    {
        get
        {
            if (m_rep_ret_oper == null )
            {
                m_rep_ret_oper = " ";
            }
            return m_rep_ret_oper;
        }
        set
        {
            if (value == null || value == "" )
            {
                m_rep_ret_oper = " ";
            }
            m_rep_ret_oper = value;
        }
    }

    /// <summary>
    /// Gets or sets the 'START_FLAG' value.
    /// </summary>
    public string START_FLAG
    {
        get
        {
            if (m_start_flag == null )
            {
                m_start_flag = " ";
            }
            return m_start_flag;
        }
        set
        {
            if (value == null || value == "" )
            {
                m_start_flag = " ";
            }
            m_start_flag = value;
        }
    }

    /// <summary>
    /// Gets or sets the 'START_TIME' value.
    /// </summary>
    public string START_TIME
    {
        get
        {
            if (m_start_time == null )
            {
                m_start_time = " ";
            }
            return m_start_time;
        }
        set
        {
            if (value == null || value == "" )
            {
                m_start_time = " ";
            }
            m_start_time = value;
        }
    }

    /// <summary>
    /// Gets or sets the 'START_RES_ID' value.
    /// </summary>
    public string START_RES_ID
    {
        get
        {
            if (m_start_res_id == null )
            {
                m_start_res_id = " ";
            }
            return m_start_res_id;
        }
        set
        {
            if (value == null || value == "" )
            {
                m_start_res_id = " ";
            }
            m_start_res_id = value;
        }
    }

    /// <summary>
    /// Gets or sets the 'END_FLAG' value.
    /// </summary>
    public string END_FLAG
    {
        get
        {
            if (m_end_flag == null )
            {
                m_end_flag = " ";
            }
            return m_end_flag;
        }
        set
        {
            if (value == null || value == "" )
            {
                m_end_flag = " ";
            }
            m_end_flag = value;
        }
    }

    /// <summary>
    /// Gets or sets the 'END_TIME' value.
    /// </summary>
    public string END_TIME
    {
        get
        {
            if (m_end_time == null )
            {
                m_end_time = " ";
            }
            return m_end_time;
        }
        set
        {
            if (value == null || value == "" )
            {
                m_end_time = " ";
            }
            m_end_time = value;
        }
    }

    /// <summary>
    /// Gets or sets the 'END_RES_ID' value.
    /// </summary>
    public string END_RES_ID
    {
        get
        {
            if (m_end_res_id == null )
            {
                m_end_res_id = " ";
            }
            return m_end_res_id;
        }
        set
        {
            if (value == null || value == "" )
            {
                m_end_res_id = " ";
            }
            m_end_res_id = value;
        }
    }

    /// <summary>
    /// Gets or sets the 'SAMPLE_FLAG' value.
    /// </summary>
    public string SAMPLE_FLAG
    {
        get
        {
            if (m_sample_flag == null )
            {
                m_sample_flag = " ";
            }
            return m_sample_flag;
        }
        set
        {
            if (value == null || value == "" )
            {
                m_sample_flag = " ";
            }
            m_sample_flag = value;
        }
    }

    /// <summary>
    /// Gets or sets the 'SAMPLE_WAIT_FLAG' value.
    /// </summary>
    public string SAMPLE_WAIT_FLAG
    {
        get
        {
            if (m_sample_wait_flag == null )
            {
                m_sample_wait_flag = " ";
            }
            return m_sample_wait_flag;
        }
        set
        {
            if (value == null || value == "" )
            {
                m_sample_wait_flag = " ";
            }
            m_sample_wait_flag = value;
        }
    }

    /// <summary>
    /// Gets or sets the 'SAMPLE_RESULT' value.
    /// </summary>
    public string SAMPLE_RESULT
    {
        get
        {
            if (m_sample_result == null )
            {
                m_sample_result = " ";
            }
            return m_sample_result;
        }
        set
        {
            if (value == null || value == "" )
            {
                m_sample_result = " ";
            }
            m_sample_result = value;
        }
    }

    /// <summary>
    /// Gets or sets the 'FROM_TO_FLAG' value.
    /// </summary>
    public string FROM_TO_FLAG
    {
        get
        {
            if (m_from_to_flag == null )
            {
                m_from_to_flag = " ";
            }
            return m_from_to_flag;
        }
        set
        {
            if (value == null || value == "" )
            {
                m_from_to_flag = " ";
            }
            m_from_to_flag = value;
        }
    }

    /// <summary>
    /// Gets or sets the 'FROM_TO_LOT_ID' value.
    /// </summary>
    public string FROM_TO_LOT_ID
    {
        get
        {
            if (m_from_to_lot_id == null )
            {
                m_from_to_lot_id = " ";
            }
            return m_from_to_lot_id;
        }
        set
        {
            if (value == null || value == "" )
            {
                m_from_to_lot_id = " ";
            }
            m_from_to_lot_id = value;
        }
    }

    /// <summary>
    /// Gets or sets the 'SHIP_CODE' value.
    /// </summary>
    public string SHIP_CODE
    {
        get
        {
            if (m_ship_code == null )
            {
                m_ship_code = " ";
            }
            return m_ship_code;
        }
        set
        {
            if (value == null || value == "" )
            {
                m_ship_code = " ";
            }
            m_ship_code = value;
        }
    }

    /// <summary>
    /// Gets or sets the 'SHIP_TIME' value.
    /// </summary>
    public string SHIP_TIME
    {
        get
        {
            if (m_ship_time == null )
            {
                m_ship_time = " ";
            }
            return m_ship_time;
        }
        set
        {
            if (value == null || value == "" )
            {
                m_ship_time = " ";
            }
            m_ship_time = value;
        }
    }

    /// <summary>
    /// Gets or sets the 'ORG_DUE_TIME' value.
    /// </summary>
    public string ORG_DUE_TIME
    {
        get
        {
            if (m_org_due_time == null )
            {
                m_org_due_time = " ";
            }
            return m_org_due_time;
        }
        set
        {
            if (value == null || value == "" )
            {
                m_org_due_time = " ";
            }
            m_org_due_time = value;
        }
    }

    /// <summary>
    /// Gets or sets the 'SCH_DUE_TIME' value.
    /// </summary>
    public string SCH_DUE_TIME
    {
        get
        {
            if (m_sch_due_time == null )
            {
                m_sch_due_time = " ";
            }
            return m_sch_due_time;
        }
        set
        {
            if (value == null || value == "" )
            {
                m_sch_due_time = " ";
            }
            m_sch_due_time = value;
        }
    }

    /// <summary>
    /// Gets or sets the 'CREATE_TIME' value.
    /// </summary>
    public string CREATE_TIME
    {
        get
        {
            if (m_create_time == null )
            {
                m_create_time = " ";
            }
            return m_create_time;
        }
        set
        {
            if (value == null || value == "" )
            {
                m_create_time = " ";
            }
            m_create_time = value;
        }
    }

    /// <summary>
    /// Gets or sets the 'FAC_IN_TIME' value.
    /// </summary>
    public string FAC_IN_TIME
    {
        get
        {
            if (m_fac_in_time == null )
            {
                m_fac_in_time = " ";
            }
            return m_fac_in_time;
        }
        set
        {
            if (value == null || value == "" )
            {
                m_fac_in_time = " ";
            }
            m_fac_in_time = value;
        }
    }

    /// <summary>
    /// Gets or sets the 'FLOW_IN_TIME' value.
    /// </summary>
    public string FLOW_IN_TIME
    {
        get
        {
            if (m_flow_in_time == null )
            {
                m_flow_in_time = " ";
            }
            return m_flow_in_time;
        }
        set
        {
            if (value == null || value == "" )
            {
                m_flow_in_time = " ";
            }
            m_flow_in_time = value;
        }
    }

    /// <summary>
    /// Gets or sets the 'OPER_IN_TIME' value.
    /// </summary>
    public string OPER_IN_TIME
    {
        get
        {
            if (m_oper_in_time == null )
            {
                m_oper_in_time = " ";
            }
            return m_oper_in_time;
        }
        set
        {
            if (value == null || value == "" )
            {
                m_oper_in_time = " ";
            }
            m_oper_in_time = value;
        }
    }

    /// <summary>
    /// Gets or sets the 'RESERVE_RES_ID' value.
    /// </summary>
    public string RESERVE_RES_ID
    {
        get
        {
            if (m_reserve_res_id == null )
            {
                m_reserve_res_id = " ";
            }
            return m_reserve_res_id;
        }
        set
        {
            if (value == null || value == "" )
            {
                m_reserve_res_id = " ";
            }
            m_reserve_res_id = value;
        }
    }

    /// <summary>
    /// Gets or sets the 'BATCH_ID' value.
    /// </summary>
    public string BATCH_ID
    {
        get
        {
            if (m_batch_id == null )
            {
                m_batch_id = " ";
            }
            return m_batch_id;
        }
        set
        {
            if (value == null || value == "" )
            {
                m_batch_id = " ";
            }
            m_batch_id = value;
        }
    }

    /// <summary>
    /// Gets or sets the 'BATCH_SEQ' value.
    /// </summary>
    public int BATCH_SEQ
    {
        get
        {
            return m_batch_seq;
        }
        set
        {
            m_batch_seq = value;
        }
    }

    /// <summary>
    /// Gets or sets the 'ORDER_ID' value.
    /// </summary>
    public string ORDER_ID
    {
        get
        {
            if (m_order_id == null )
            {
                m_order_id = " ";
            }
            return m_order_id;
        }
        set
        {
            if (value == null || value == "" )
            {
                m_order_id = " ";
            }
            m_order_id = value;
        }
    }

    /// <summary>
    /// Gets or sets the 'ADD_ORDER_ID_1' value.
    /// </summary>
    public string ADD_ORDER_ID_1
    {
        get
        {
            if (m_add_order_id_1 == null )
            {
                m_add_order_id_1 = " ";
            }
            return m_add_order_id_1;
        }
        set
        {
            if (value == null || value == "" )
            {
                m_add_order_id_1 = " ";
            }
            m_add_order_id_1 = value;
        }
    }

    /// <summary>
    /// Gets or sets the 'ADD_ORDER_ID_2' value.
    /// </summary>
    public string ADD_ORDER_ID_2
    {
        get
        {
            if (m_add_order_id_2 == null )
            {
                m_add_order_id_2 = " ";
            }
            return m_add_order_id_2;
        }
        set
        {
            if (value == null || value == "" )
            {
                m_add_order_id_2 = " ";
            }
            m_add_order_id_2 = value;
        }
    }

    /// <summary>
    /// Gets or sets the 'ADD_ORDER_ID_3' value.
    /// </summary>
    public string ADD_ORDER_ID_3
    {
        get
        {
            if (m_add_order_id_3 == null )
            {
                m_add_order_id_3 = " ";
            }
            return m_add_order_id_3;
        }
        set
        {
            if (value == null || value == "" )
            {
                m_add_order_id_3 = " ";
            }
            m_add_order_id_3 = value;
        }
    }

    /// <summary>
    /// Gets or sets the 'LOT_LOCATION' value.
    /// </summary>
    public string LOT_LOCATION
    {
        get
        {
            if (m_lot_location == null )
            {
                m_lot_location = " ";
            }
            return m_lot_location;
        }
        set
        {
            if (value == null || value == "" )
            {
                m_lot_location = " ";
            }
            m_lot_location = value;
        }
    }

    /// <summary>
    /// Gets or sets the 'LOT_CMF_1' value.
    /// </summary>
    public string LOT_CMF_1
    {
        get
        {
            if (m_lot_cmf_1 == null )
            {
                m_lot_cmf_1 = " ";
            }
            return m_lot_cmf_1;
        }
        set
        {
            if (value == null || value == "" )
            {
                m_lot_cmf_1 = " ";
            }
            m_lot_cmf_1 = value;
        }
    }

    /// <summary>
    /// Gets or sets the 'LOT_CMF_2' value.
    /// </summary>
    public string LOT_CMF_2
    {
        get
        {
            if (m_lot_cmf_2 == null )
            {
                m_lot_cmf_2 = " ";
            }
            return m_lot_cmf_2;
        }
        set
        {
            if (value == null || value == "" )
            {
                m_lot_cmf_2 = " ";
            }
            m_lot_cmf_2 = value;
        }
    }

    /// <summary>
    /// Gets or sets the 'LOT_CMF_3' value.
    /// </summary>
    public string LOT_CMF_3
    {
        get
        {
            if (m_lot_cmf_3 == null )
            {
                m_lot_cmf_3 = " ";
            }
            return m_lot_cmf_3;
        }
        set
        {
            if (value == null || value == "" )
            {
                m_lot_cmf_3 = " ";
            }
            m_lot_cmf_3 = value;
        }
    }

    /// <summary>
    /// Gets or sets the 'LOT_CMF_4' value.
    /// </summary>
    public string LOT_CMF_4
    {
        get
        {
            if (m_lot_cmf_4 == null )
            {
                m_lot_cmf_4 = " ";
            }
            return m_lot_cmf_4;
        }
        set
        {
            if (value == null || value == "" )
            {
                m_lot_cmf_4 = " ";
            }
            m_lot_cmf_4 = value;
        }
    }

    /// <summary>
    /// Gets or sets the 'LOT_CMF_5' value.
    /// </summary>
    public string LOT_CMF_5
    {
        get
        {
            if (m_lot_cmf_5 == null )
            {
                m_lot_cmf_5 = " ";
            }
            return m_lot_cmf_5;
        }
        set
        {
            if (value == null || value == "" )
            {
                m_lot_cmf_5 = " ";
            }
            m_lot_cmf_5 = value;
        }
    }

    /// <summary>
    /// Gets or sets the 'LOT_CMF_6' value.
    /// </summary>
    public string LOT_CMF_6
    {
        get
        {
            if (m_lot_cmf_6 == null )
            {
                m_lot_cmf_6 = " ";
            }
            return m_lot_cmf_6;
        }
        set
        {
            if (value == null || value == "" )
            {
                m_lot_cmf_6 = " ";
            }
            m_lot_cmf_6 = value;
        }
    }

    /// <summary>
    /// Gets or sets the 'LOT_CMF_7' value.
    /// </summary>
    public string LOT_CMF_7
    {
        get
        {
            if (m_lot_cmf_7 == null )
            {
                m_lot_cmf_7 = " ";
            }
            return m_lot_cmf_7;
        }
        set
        {
            if (value == null || value == "" )
            {
                m_lot_cmf_7 = " ";
            }
            m_lot_cmf_7 = value;
        }
    }

    /// <summary>
    /// Gets or sets the 'LOT_CMF_8' value.
    /// </summary>
    public string LOT_CMF_8
    {
        get
        {
            if (m_lot_cmf_8 == null )
            {
                m_lot_cmf_8 = " ";
            }
            return m_lot_cmf_8;
        }
        set
        {
            if (value == null || value == "" )
            {
                m_lot_cmf_8 = " ";
            }
            m_lot_cmf_8 = value;
        }
    }

    /// <summary>
    /// Gets or sets the 'LOT_CMF_9' value.
    /// </summary>
    public string LOT_CMF_9
    {
        get
        {
            if (m_lot_cmf_9 == null )
            {
                m_lot_cmf_9 = " ";
            }
            return m_lot_cmf_9;
        }
        set
        {
            if (value == null || value == "" )
            {
                m_lot_cmf_9 = " ";
            }
            m_lot_cmf_9 = value;
        }
    }

    /// <summary>
    /// Gets or sets the 'LOT_CMF_10' value.
    /// </summary>
    public string LOT_CMF_10
    {
        get
        {
            if (m_lot_cmf_10 == null )
            {
                m_lot_cmf_10 = " ";
            }
            return m_lot_cmf_10;
        }
        set
        {
            if (value == null || value == "" )
            {
                m_lot_cmf_10 = " ";
            }
            m_lot_cmf_10 = value;
        }
    }

    /// <summary>
    /// Gets or sets the 'LOT_CMF_11' value.
    /// </summary>
    public string LOT_CMF_11
    {
        get
        {
            if (m_lot_cmf_11 == null )
            {
                m_lot_cmf_11 = " ";
            }
            return m_lot_cmf_11;
        }
        set
        {
            if (value == null || value == "" )
            {
                m_lot_cmf_11 = " ";
            }
            m_lot_cmf_11 = value;
        }
    }

    /// <summary>
    /// Gets or sets the 'LOT_CMF_12' value.
    /// </summary>
    public string LOT_CMF_12
    {
        get
        {
            if (m_lot_cmf_12 == null )
            {
                m_lot_cmf_12 = " ";
            }
            return m_lot_cmf_12;
        }
        set
        {
            if (value == null || value == "" )
            {
                m_lot_cmf_12 = " ";
            }
            m_lot_cmf_12 = value;
        }
    }

    /// <summary>
    /// Gets or sets the 'LOT_CMF_13' value.
    /// </summary>
    public string LOT_CMF_13
    {
        get
        {
            if (m_lot_cmf_13 == null )
            {
                m_lot_cmf_13 = " ";
            }
            return m_lot_cmf_13;
        }
        set
        {
            if (value == null || value == "" )
            {
                m_lot_cmf_13 = " ";
            }
            m_lot_cmf_13 = value;
        }
    }

    /// <summary>
    /// Gets or sets the 'LOT_CMF_14' value.
    /// </summary>
    public string LOT_CMF_14
    {
        get
        {
            if (m_lot_cmf_14 == null )
            {
                m_lot_cmf_14 = " ";
            }
            return m_lot_cmf_14;
        }
        set
        {
            if (value == null || value == "" )
            {
                m_lot_cmf_14 = " ";
            }
            m_lot_cmf_14 = value;
        }
    }

    /// <summary>
    /// Gets or sets the 'LOT_CMF_15' value.
    /// </summary>
    public string LOT_CMF_15
    {
        get
        {
            if (m_lot_cmf_15 == null )
            {
                m_lot_cmf_15 = " ";
            }
            return m_lot_cmf_15;
        }
        set
        {
            if (value == null || value == "" )
            {
                m_lot_cmf_15 = " ";
            }
            m_lot_cmf_15 = value;
        }
    }

    /// <summary>
    /// Gets or sets the 'LOT_CMF_16' value.
    /// </summary>
    public string LOT_CMF_16
    {
        get
        {
            if (m_lot_cmf_16 == null )
            {
                m_lot_cmf_16 = " ";
            }
            return m_lot_cmf_16;
        }
        set
        {
            if (value == null || value == "" )
            {
                m_lot_cmf_16 = " ";
            }
            m_lot_cmf_16 = value;
        }
    }

    /// <summary>
    /// Gets or sets the 'LOT_CMF_17' value.
    /// </summary>
    public string LOT_CMF_17
    {
        get
        {
            if (m_lot_cmf_17 == null )
            {
                m_lot_cmf_17 = " ";
            }
            return m_lot_cmf_17;
        }
        set
        {
            if (value == null || value == "" )
            {
                m_lot_cmf_17 = " ";
            }
            m_lot_cmf_17 = value;
        }
    }

    /// <summary>
    /// Gets or sets the 'LOT_CMF_18' value.
    /// </summary>
    public string LOT_CMF_18
    {
        get
        {
            if (m_lot_cmf_18 == null )
            {
                m_lot_cmf_18 = " ";
            }
            return m_lot_cmf_18;
        }
        set
        {
            if (value == null || value == "" )
            {
                m_lot_cmf_18 = " ";
            }
            m_lot_cmf_18 = value;
        }
    }

    /// <summary>
    /// Gets or sets the 'LOT_CMF_19' value.
    /// </summary>
    public string LOT_CMF_19
    {
        get
        {
            if (m_lot_cmf_19 == null )
            {
                m_lot_cmf_19 = " ";
            }
            return m_lot_cmf_19;
        }
        set
        {
            if (value == null || value == "" )
            {
                m_lot_cmf_19 = " ";
            }
            m_lot_cmf_19 = value;
        }
    }

    /// <summary>
    /// Gets or sets the 'LOT_CMF_20' value.
    /// </summary>
    public string LOT_CMF_20
    {
        get
        {
            if (m_lot_cmf_20 == null )
            {
                m_lot_cmf_20 = " ";
            }
            return m_lot_cmf_20;
        }
        set
        {
            if (value == null || value == "" )
            {
                m_lot_cmf_20 = " ";
            }
            m_lot_cmf_20 = value;
        }
    }

    /// <summary>
    /// Gets or sets the 'LOT_DEL_FLAG' value.
    /// </summary>
    public string LOT_DEL_FLAG
    {
        get
        {
            if (m_lot_del_flag == null )
            {
                m_lot_del_flag = " ";
            }
            return m_lot_del_flag;
        }
        set
        {
            if (value == null || value == "" )
            {
                m_lot_del_flag = " ";
            }
            m_lot_del_flag = value;
        }
    }

    /// <summary>
    /// Gets or sets the 'LOT_DEL_CODE' value.
    /// </summary>
    public string LOT_DEL_CODE
    {
        get
        {
            if (m_lot_del_code == null )
            {
                m_lot_del_code = " ";
            }
            return m_lot_del_code;
        }
        set
        {
            if (value == null || value == "" )
            {
                m_lot_del_code = " ";
            }
            m_lot_del_code = value;
        }
    }

    /// <summary>
    /// Gets or sets the 'LOT_DEL_TIME' value.
    /// </summary>
    public string LOT_DEL_TIME
    {
        get
        {
            if (m_lot_del_time == null )
            {
                m_lot_del_time = " ";
            }
            return m_lot_del_time;
        }
        set
        {
            if (value == null || value == "" )
            {
                m_lot_del_time = " ";
            }
            m_lot_del_time = value;
        }
    }

    /// <summary>
    /// Gets or sets the 'BOM_SET_ID' value.
    /// </summary>
    public string BOM_SET_ID
    {
        get
        {
            if (m_bom_set_id == null )
            {
                m_bom_set_id = " ";
            }
            return m_bom_set_id;
        }
        set
        {
            if (value == null || value == "" )
            {
                m_bom_set_id = " ";
            }
            m_bom_set_id = value;
        }
    }

    /// <summary>
    /// Gets or sets the 'BOM_SET_VERSION' value.
    /// </summary>
    public int BOM_SET_VERSION
    {
        get
        {
            return m_bom_set_version;
        }
        set
        {
            m_bom_set_version = value;
        }
    }

    /// <summary>
    /// Gets or sets the 'BOM_ACTIVE_HIST_SEQ' value.
    /// </summary>
    public int BOM_ACTIVE_HIST_SEQ
    {
        get
        {
            return m_bom_active_hist_seq;
        }
        set
        {
            m_bom_active_hist_seq = value;
        }
    }

    /// <summary>
    /// Gets or sets the 'BOM_HIST_SEQ' value.
    /// </summary>
    public int BOM_HIST_SEQ
    {
        get
        {
            return m_bom_hist_seq;
        }
        set
        {
            m_bom_hist_seq = value;
        }
    }

    /// <summary>
    /// Gets or sets the 'LAST_TRAN_CODE' value.
    /// </summary>
    public string LAST_TRAN_CODE
    {
        get
        {
            if (m_last_tran_code == null )
            {
                m_last_tran_code = " ";
            }
            return m_last_tran_code;
        }
        set
        {
            if (value == null || value == "" )
            {
                m_last_tran_code = " ";
            }
            m_last_tran_code = value;
        }
    }

    /// <summary>
    /// Gets or sets the 'LAST_TRAN_TIME' value.
    /// </summary>
    public string LAST_TRAN_TIME
    {
        get
        {
            if (m_last_tran_time == null )
            {
                m_last_tran_time = " ";
            }
            return m_last_tran_time;
        }
        set
        {
            if (value == null || value == "" )
            {
                m_last_tran_time = " ";
            }
            m_last_tran_time = value;
        }
    }

    /// <summary>
    /// Gets or sets the 'LAST_COMMENT' value.
    /// </summary>
    public string LAST_COMMENT
    {
        get
        {
            if (m_last_comment == null )
            {
                m_last_comment = " ";
            }
            return m_last_comment;
        }
        set
        {
            if (value == null || value == "" )
            {
                m_last_comment = " ";
            }
            m_last_comment = value;
        }
    }

    /// <summary>
    /// Gets or sets the 'LAST_ACTIVE_HIST_SEQ' value.
    /// </summary>
    public int LAST_ACTIVE_HIST_SEQ
    {
        get
        {
            return m_last_active_hist_seq;
        }
        set
        {
            m_last_active_hist_seq = value;
        }
    }

    /// <summary>
    /// Gets or sets the 'LAST_HIST_SEQ' value.
    /// </summary>
    public int LAST_HIST_SEQ
    {
        get
        {
            return m_last_hist_seq;
        }
        set
        {
            m_last_hist_seq = value;
        }
    }

    /// <summary>
    /// Gets or sets the 'CRITICAL_RES_ID' value.
    /// </summary>
    public string CRITICAL_RES_ID
    {
        get
        {
            if (m_critical_res_id == null )
            {
                m_critical_res_id = " ";
            }
            return m_critical_res_id;
        }
        set
        {
            if (value == null || value == "" )
            {
                m_critical_res_id = " ";
            }
            m_critical_res_id = value;
        }
    }

    /// <summary>
    /// Gets or sets the 'CRITICAL_RES_GROUP_ID' value.
    /// </summary>
    public string CRITICAL_RES_GROUP_ID
    {
        get
        {
            if (m_critical_res_group_id == null )
            {
                m_critical_res_group_id = " ";
            }
            return m_critical_res_group_id;
        }
        set
        {
            if (value == null || value == "" )
            {
                m_critical_res_group_id = " ";
            }
            m_critical_res_group_id = value;
        }
    }

    /// <summary>
    /// Gets or sets the 'SAVE_RES_ID_1' value.
    /// </summary>
    public string SAVE_RES_ID_1
    {
        get
        {
            if (m_save_res_id_1 == null )
            {
                m_save_res_id_1 = " ";
            }
            return m_save_res_id_1;
        }
        set
        {
            if (value == null || value == "" )
            {
                m_save_res_id_1 = " ";
            }
            m_save_res_id_1 = value;
        }
    }

    /// <summary>
    /// Gets or sets the 'SAVE_RES_ID_2' value.
    /// </summary>
    public string SAVE_RES_ID_2
    {
        get
        {
            if (m_save_res_id_2 == null )
            {
                m_save_res_id_2 = " ";
            }
            return m_save_res_id_2;
        }
        set
        {
            if (value == null || value == "" )
            {
                m_save_res_id_2 = " ";
            }
            m_save_res_id_2 = value;
        }
    }

    /// <summary>
    /// Gets or sets the 'SUBRES_ID' value.
    /// </summary>
    public string SUBRES_ID
    {
        get
        {
            if (m_subres_id == null )
            {
                m_subres_id = " ";
            }
            return m_subres_id;
        }
        set
        {
            if (value == null || value == "" )
            {
                m_subres_id = " ";
            }
            m_subres_id = value;
        }
    }

    /// <summary>
    /// Gets or sets the 'LOT_GROUP_ID_1' value.
    /// </summary>
    public string LOT_GROUP_ID_1
    {
        get
        {
            if (m_lot_group_id_1 == null )
            {
                m_lot_group_id_1 = " ";
            }
            return m_lot_group_id_1;
        }
        set
        {
            if (value == null || value == "" )
            {
                m_lot_group_id_1 = " ";
            }
            m_lot_group_id_1 = value;
        }
    }

    /// <summary>
    /// Gets or sets the 'LOT_GROUP_ID_2' value.
    /// </summary>
    public string LOT_GROUP_ID_2
    {
        get
        {
            if (m_lot_group_id_2 == null )
            {
                m_lot_group_id_2 = " ";
            }
            return m_lot_group_id_2;
        }
        set
        {
            if (value == null || value == "" )
            {
                m_lot_group_id_2 = " ";
            }
            m_lot_group_id_2 = value;
        }
    }

    /// <summary>
    /// Gets or sets the 'LOT_GROUP_ID_3' value.
    /// </summary>
    public string LOT_GROUP_ID_3
    {
        get
        {
            if (m_lot_group_id_3 == null )
            {
                m_lot_group_id_3 = " ";
            }
            return m_lot_group_id_3;
        }
        set
        {
            if (value == null || value == "" )
            {
                m_lot_group_id_3 = " ";
            }
            m_lot_group_id_3 = value;
        }
    }

    /// <summary>
    /// Gets or sets the 'RESV_FIELD_1' value.
    /// </summary>
    public string RESV_FIELD_1
    {
        get
        {
            if (m_resv_field_1 == null )
            {
                m_resv_field_1 = " ";
            }
            return m_resv_field_1;
        }
        set
        {
            if (value == null || value == "" )
            {
                m_resv_field_1 = " ";
            }
            m_resv_field_1 = value;
        }
    }

    /// <summary>
    /// Gets or sets the 'RESV_FIELD_2' value.
    /// </summary>
    public string RESV_FIELD_2
    {
        get
        {
            if (m_resv_field_2 == null )
            {
                m_resv_field_2 = " ";
            }
            return m_resv_field_2;
        }
        set
        {
            if (value == null || value == "" )
            {
                m_resv_field_2 = " ";
            }
            m_resv_field_2 = value;
        }
    }

    /// <summary>
    /// Gets or sets the 'RESV_FIELD_3' value.
    /// </summary>
    public string RESV_FIELD_3
    {
        get
        {
            if (m_resv_field_3 == null )
            {
                m_resv_field_3 = " ";
            }
            return m_resv_field_3;
        }
        set
        {
            if (value == null || value == "" )
            {
                m_resv_field_3 = " ";
            }
            m_resv_field_3 = value;
        }
    }

    /// <summary>
    /// Gets or sets the 'RESV_FIELD_4' value.
    /// </summary>
    public string RESV_FIELD_4
    {
        get
        {
            if (m_resv_field_4 == null )
            {
                m_resv_field_4 = " ";
            }
            return m_resv_field_4;
        }
        set
        {
            if (value == null || value == "" )
            {
                m_resv_field_4 = " ";
            }
            m_resv_field_4 = value;
        }
    }

    /// <summary>
    /// Gets or sets the 'RESV_FIELD_5' value.
    /// </summary>
    public string RESV_FIELD_5
    {
        get
        {
            if (m_resv_field_5 == null )
            {
                m_resv_field_5 = " ";
            }
            return m_resv_field_5;
        }
        set
        {
            if (value == null || value == "" )
            {
                m_resv_field_5 = " ";
            }
            m_resv_field_5 = value;
        }
    }

    /// <summary>
    /// Gets or sets the 'RESV_FLAG_1' value.
    /// </summary>
    public string RESV_FLAG_1
    {
        get
        {
            if (m_resv_flag_1 == null )
            {
                m_resv_flag_1 = " ";
            }
            return m_resv_flag_1;
        }
        set
        {
            if (value == null || value == "" )
            {
                m_resv_flag_1 = " ";
            }
            m_resv_flag_1 = value;
        }
    }

    /// <summary>
    /// Gets or sets the 'RESV_FLAG_2' value.
    /// </summary>
    public string RESV_FLAG_2
    {
        get
        {
            if (m_resv_flag_2 == null )
            {
                m_resv_flag_2 = " ";
            }
            return m_resv_flag_2;
        }
        set
        {
            if (value == null || value == "" )
            {
                m_resv_flag_2 = " ";
            }
            m_resv_flag_2 = value;
        }
    }

    /// <summary>
    /// Gets or sets the 'RESV_FLAG_3' value.
    /// </summary>
    public string RESV_FLAG_3
    {
        get
        {
            if (m_resv_flag_3 == null )
            {
                m_resv_flag_3 = " ";
            }
            return m_resv_flag_3;
        }
        set
        {
            if (value == null || value == "" )
            {
                m_resv_flag_3 = " ";
            }
            m_resv_flag_3 = value;
        }
    }

    /// <summary>
    /// Gets or sets the 'RESV_FLAG_4' value.
    /// </summary>
    public string RESV_FLAG_4
    {
        get
        {
            if (m_resv_flag_4 == null )
            {
                m_resv_flag_4 = " ";
            }
            return m_resv_flag_4;
        }
        set
        {
            if (value == null || value == "" )
            {
                m_resv_flag_4 = " ";
            }
            m_resv_flag_4 = value;
        }
    }

    /// <summary>
    /// Gets or sets the 'RESV_FLAG_5' value.
    /// </summary>
    public string RESV_FLAG_5
    {
        get
        {
            if (m_resv_flag_5 == null )
            {
                m_resv_flag_5 = " ";
            }
            return m_resv_flag_5;
        }
        set
        {
            if (value == null || value == "" )
            {
                m_resv_flag_5 = " ";
            }
            m_resv_flag_5 = value;
        }
    }

#endregion

#region " Function Definition "

    /// <summary>
    /// Creator for Object
    /// <summary>
    public DBC_MWIPLOTSTS(ref DB_Common dbc)
    {
        this._dbc = dbc;
        Init();
    }

    /// <summary>
    /// Initialization
    /// <summary>
    public bool Init()
    {
        LOT_ID = " ";
        LOT_DESC = " ";
        FACTORY = " ";
        MAT_ID = " ";
        MAT_VER = 0;
        FLOW = " ";
        FLOW_SEQ_NUM = 0;
        OPER = " ";
        QTY_1 = 0;
        QTY_2 = 0;
        QTY_3 = 0;
        CRR_ID = " ";
        LOT_TYPE = " ";
        OWNER_CODE = " ";
        CREATE_CODE = " ";
        LOT_PRIORITY = " ";
        LOT_STATUS = " ";
        HOLD_FLAG = " ";
        HOLD_CODE = " ";
        HOLD_PASSWORD = " ";
        HOLD_PRV_GRP_ID = " ";
        OPER_IN_QTY_1 = 0;
        OPER_IN_QTY_2 = 0;
        OPER_IN_QTY_3 = 0;
        CREATE_QTY_1 = 0;
        CREATE_QTY_2 = 0;
        CREATE_QTY_3 = 0;
        START_QTY_1 = 0;
        START_QTY_2 = 0;
        START_QTY_3 = 0;
        INV_FLAG = " ";
        TRANSIT_FLAG = " ";
        UNIT_EXIST_FLAG = " ";
        INV_UNIT = " ";
        RWK_FLAG = " ";
        RWK_CODE = " ";
        RWK_COUNT = 0;
        RWK_RET_FLOW = " ";
        RWK_RET_FLOW_SEQ_NUM = 0;
        RWK_RET_OPER = " ";
        RWK_END_FLOW = " ";
        RWK_END_FLOW_SEQ_NUM = 0;
        RWK_END_OPER = " ";
        RWK_RET_CLEAR_FLAG = " ";
        RWK_TIME = " ";
        NSTD_FLAG = " ";
        NSTD_RET_FLOW = " ";
        NSTD_RET_FLOW_SEQ_NUM = 0;
        NSTD_RET_OPER = " ";
        NSTD_TIME = " ";
        REP_FLAG = " ";
        REP_RET_OPER = " ";
        START_FLAG = " ";
        START_TIME = " ";
        START_RES_ID = " ";
        END_FLAG = " ";
        END_TIME = " ";
        END_RES_ID = " ";
        SAMPLE_FLAG = " ";
        SAMPLE_WAIT_FLAG = " ";
        SAMPLE_RESULT = " ";
        FROM_TO_FLAG = " ";
        FROM_TO_LOT_ID = " ";
        SHIP_CODE = " ";
        SHIP_TIME = " ";
        ORG_DUE_TIME = " ";
        SCH_DUE_TIME = " ";
        CREATE_TIME = " ";
        FAC_IN_TIME = " ";
        FLOW_IN_TIME = " ";
        OPER_IN_TIME = " ";
        RESERVE_RES_ID = " ";
        BATCH_ID = " ";
        BATCH_SEQ = 0;
        ORDER_ID = " ";
        ADD_ORDER_ID_1 = " ";
        ADD_ORDER_ID_2 = " ";
        ADD_ORDER_ID_3 = " ";
        LOT_LOCATION = " ";
        LOT_CMF_1 = " ";
        LOT_CMF_2 = " ";
        LOT_CMF_3 = " ";
        LOT_CMF_4 = " ";
        LOT_CMF_5 = " ";
        LOT_CMF_6 = " ";
        LOT_CMF_7 = " ";
        LOT_CMF_8 = " ";
        LOT_CMF_9 = " ";
        LOT_CMF_10 = " ";
        LOT_CMF_11 = " ";
        LOT_CMF_12 = " ";
        LOT_CMF_13 = " ";
        LOT_CMF_14 = " ";
        LOT_CMF_15 = " ";
        LOT_CMF_16 = " ";
        LOT_CMF_17 = " ";
        LOT_CMF_18 = " ";
        LOT_CMF_19 = " ";
        LOT_CMF_20 = " ";
        LOT_DEL_FLAG = " ";
        LOT_DEL_CODE = " ";
        LOT_DEL_TIME = " ";
        BOM_SET_ID = " ";
        BOM_SET_VERSION = 0;
        BOM_ACTIVE_HIST_SEQ = 0;
        BOM_HIST_SEQ = 0;
        LAST_TRAN_CODE = " ";
        LAST_TRAN_TIME = " ";
        LAST_COMMENT = " ";
        LAST_ACTIVE_HIST_SEQ = 0;
        LAST_HIST_SEQ = 0;
        CRITICAL_RES_ID = " ";
        CRITICAL_RES_GROUP_ID = " ";
        SAVE_RES_ID_1 = " ";
        SAVE_RES_ID_2 = " ";
        SUBRES_ID = " ";
        LOT_GROUP_ID_1 = " ";
        LOT_GROUP_ID_2 = " ";
        LOT_GROUP_ID_3 = " ";
        RESV_FIELD_1 = " ";
        RESV_FIELD_2 = " ";
        RESV_FIELD_3 = " ";
        RESV_FIELD_4 = " ";
        RESV_FIELD_5 = " ";
        RESV_FLAG_1 = " ";
        RESV_FLAG_2 = " ";
        RESV_FLAG_3 = " ";
        RESV_FLAG_4 = " ";
        RESV_FLAG_5 = " ";

        return true;
    }

    /// <summary>
    /// Clone object
    /// </summary>
    /// <returns>object</returns>
    public object Clone()
    {
        DBC_MWIPLOTSTS MWIPLOTSTS = null;
        try
        {
            MWIPLOTSTS = new DBC_MWIPLOTSTS(ref _dbc);

            MWIPLOTSTS.LOT_ID = this.LOT_ID;
            MWIPLOTSTS.LOT_DESC = this.LOT_DESC;
            MWIPLOTSTS.FACTORY = this.FACTORY;
            MWIPLOTSTS.MAT_ID = this.MAT_ID;
            MWIPLOTSTS.MAT_VER = this.MAT_VER;
            MWIPLOTSTS.FLOW = this.FLOW;
            MWIPLOTSTS.FLOW_SEQ_NUM = this.FLOW_SEQ_NUM;
            MWIPLOTSTS.OPER = this.OPER;
            MWIPLOTSTS.QTY_1 = this.QTY_1;
            MWIPLOTSTS.QTY_2 = this.QTY_2;
            MWIPLOTSTS.QTY_3 = this.QTY_3;
            MWIPLOTSTS.CRR_ID = this.CRR_ID;
            MWIPLOTSTS.LOT_TYPE = this.LOT_TYPE;
            MWIPLOTSTS.OWNER_CODE = this.OWNER_CODE;
            MWIPLOTSTS.CREATE_CODE = this.CREATE_CODE;
            MWIPLOTSTS.LOT_PRIORITY = this.LOT_PRIORITY;
            MWIPLOTSTS.LOT_STATUS = this.LOT_STATUS;
            MWIPLOTSTS.HOLD_FLAG = this.HOLD_FLAG;
            MWIPLOTSTS.HOLD_CODE = this.HOLD_CODE;
            MWIPLOTSTS.HOLD_PASSWORD = this.HOLD_PASSWORD;
            MWIPLOTSTS.HOLD_PRV_GRP_ID = this.HOLD_PRV_GRP_ID;
            MWIPLOTSTS.OPER_IN_QTY_1 = this.OPER_IN_QTY_1;
            MWIPLOTSTS.OPER_IN_QTY_2 = this.OPER_IN_QTY_2;
            MWIPLOTSTS.OPER_IN_QTY_3 = this.OPER_IN_QTY_3;
            MWIPLOTSTS.CREATE_QTY_1 = this.CREATE_QTY_1;
            MWIPLOTSTS.CREATE_QTY_2 = this.CREATE_QTY_2;
            MWIPLOTSTS.CREATE_QTY_3 = this.CREATE_QTY_3;
            MWIPLOTSTS.START_QTY_1 = this.START_QTY_1;
            MWIPLOTSTS.START_QTY_2 = this.START_QTY_2;
            MWIPLOTSTS.START_QTY_3 = this.START_QTY_3;
            MWIPLOTSTS.INV_FLAG = this.INV_FLAG;
            MWIPLOTSTS.TRANSIT_FLAG = this.TRANSIT_FLAG;
            MWIPLOTSTS.UNIT_EXIST_FLAG = this.UNIT_EXIST_FLAG;
            MWIPLOTSTS.INV_UNIT = this.INV_UNIT;
            MWIPLOTSTS.RWK_FLAG = this.RWK_FLAG;
            MWIPLOTSTS.RWK_CODE = this.RWK_CODE;
            MWIPLOTSTS.RWK_COUNT = this.RWK_COUNT;
            MWIPLOTSTS.RWK_RET_FLOW = this.RWK_RET_FLOW;
            MWIPLOTSTS.RWK_RET_FLOW_SEQ_NUM = this.RWK_RET_FLOW_SEQ_NUM;
            MWIPLOTSTS.RWK_RET_OPER = this.RWK_RET_OPER;
            MWIPLOTSTS.RWK_END_FLOW = this.RWK_END_FLOW;
            MWIPLOTSTS.RWK_END_FLOW_SEQ_NUM = this.RWK_END_FLOW_SEQ_NUM;
            MWIPLOTSTS.RWK_END_OPER = this.RWK_END_OPER;
            MWIPLOTSTS.RWK_RET_CLEAR_FLAG = this.RWK_RET_CLEAR_FLAG;
            MWIPLOTSTS.RWK_TIME = this.RWK_TIME;
            MWIPLOTSTS.NSTD_FLAG = this.NSTD_FLAG;
            MWIPLOTSTS.NSTD_RET_FLOW = this.NSTD_RET_FLOW;
            MWIPLOTSTS.NSTD_RET_FLOW_SEQ_NUM = this.NSTD_RET_FLOW_SEQ_NUM;
            MWIPLOTSTS.NSTD_RET_OPER = this.NSTD_RET_OPER;
            MWIPLOTSTS.NSTD_TIME = this.NSTD_TIME;
            MWIPLOTSTS.REP_FLAG = this.REP_FLAG;
            MWIPLOTSTS.REP_RET_OPER = this.REP_RET_OPER;
            MWIPLOTSTS.START_FLAG = this.START_FLAG;
            MWIPLOTSTS.START_TIME = this.START_TIME;
            MWIPLOTSTS.START_RES_ID = this.START_RES_ID;
            MWIPLOTSTS.END_FLAG = this.END_FLAG;
            MWIPLOTSTS.END_TIME = this.END_TIME;
            MWIPLOTSTS.END_RES_ID = this.END_RES_ID;
            MWIPLOTSTS.SAMPLE_FLAG = this.SAMPLE_FLAG;
            MWIPLOTSTS.SAMPLE_WAIT_FLAG = this.SAMPLE_WAIT_FLAG;
            MWIPLOTSTS.SAMPLE_RESULT = this.SAMPLE_RESULT;
            MWIPLOTSTS.FROM_TO_FLAG = this.FROM_TO_FLAG;
            MWIPLOTSTS.FROM_TO_LOT_ID = this.FROM_TO_LOT_ID;
            MWIPLOTSTS.SHIP_CODE = this.SHIP_CODE;
            MWIPLOTSTS.SHIP_TIME = this.SHIP_TIME;
            MWIPLOTSTS.ORG_DUE_TIME = this.ORG_DUE_TIME;
            MWIPLOTSTS.SCH_DUE_TIME = this.SCH_DUE_TIME;
            MWIPLOTSTS.CREATE_TIME = this.CREATE_TIME;
            MWIPLOTSTS.FAC_IN_TIME = this.FAC_IN_TIME;
            MWIPLOTSTS.FLOW_IN_TIME = this.FLOW_IN_TIME;
            MWIPLOTSTS.OPER_IN_TIME = this.OPER_IN_TIME;
            MWIPLOTSTS.RESERVE_RES_ID = this.RESERVE_RES_ID;
            MWIPLOTSTS.BATCH_ID = this.BATCH_ID;
            MWIPLOTSTS.BATCH_SEQ = this.BATCH_SEQ;
            MWIPLOTSTS.ORDER_ID = this.ORDER_ID;
            MWIPLOTSTS.ADD_ORDER_ID_1 = this.ADD_ORDER_ID_1;
            MWIPLOTSTS.ADD_ORDER_ID_2 = this.ADD_ORDER_ID_2;
            MWIPLOTSTS.ADD_ORDER_ID_3 = this.ADD_ORDER_ID_3;
            MWIPLOTSTS.LOT_LOCATION = this.LOT_LOCATION;
            MWIPLOTSTS.LOT_CMF_1 = this.LOT_CMF_1;
            MWIPLOTSTS.LOT_CMF_2 = this.LOT_CMF_2;
            MWIPLOTSTS.LOT_CMF_3 = this.LOT_CMF_3;
            MWIPLOTSTS.LOT_CMF_4 = this.LOT_CMF_4;
            MWIPLOTSTS.LOT_CMF_5 = this.LOT_CMF_5;
            MWIPLOTSTS.LOT_CMF_6 = this.LOT_CMF_6;
            MWIPLOTSTS.LOT_CMF_7 = this.LOT_CMF_7;
            MWIPLOTSTS.LOT_CMF_8 = this.LOT_CMF_8;
            MWIPLOTSTS.LOT_CMF_9 = this.LOT_CMF_9;
            MWIPLOTSTS.LOT_CMF_10 = this.LOT_CMF_10;
            MWIPLOTSTS.LOT_CMF_11 = this.LOT_CMF_11;
            MWIPLOTSTS.LOT_CMF_12 = this.LOT_CMF_12;
            MWIPLOTSTS.LOT_CMF_13 = this.LOT_CMF_13;
            MWIPLOTSTS.LOT_CMF_14 = this.LOT_CMF_14;
            MWIPLOTSTS.LOT_CMF_15 = this.LOT_CMF_15;
            MWIPLOTSTS.LOT_CMF_16 = this.LOT_CMF_16;
            MWIPLOTSTS.LOT_CMF_17 = this.LOT_CMF_17;
            MWIPLOTSTS.LOT_CMF_18 = this.LOT_CMF_18;
            MWIPLOTSTS.LOT_CMF_19 = this.LOT_CMF_19;
            MWIPLOTSTS.LOT_CMF_20 = this.LOT_CMF_20;
            MWIPLOTSTS.LOT_DEL_FLAG = this.LOT_DEL_FLAG;
            MWIPLOTSTS.LOT_DEL_CODE = this.LOT_DEL_CODE;
            MWIPLOTSTS.LOT_DEL_TIME = this.LOT_DEL_TIME;
            MWIPLOTSTS.BOM_SET_ID = this.BOM_SET_ID;
            MWIPLOTSTS.BOM_SET_VERSION = this.BOM_SET_VERSION;
            MWIPLOTSTS.BOM_ACTIVE_HIST_SEQ = this.BOM_ACTIVE_HIST_SEQ;
            MWIPLOTSTS.BOM_HIST_SEQ = this.BOM_HIST_SEQ;
            MWIPLOTSTS.LAST_TRAN_CODE = this.LAST_TRAN_CODE;
            MWIPLOTSTS.LAST_TRAN_TIME = this.LAST_TRAN_TIME;
            MWIPLOTSTS.LAST_COMMENT = this.LAST_COMMENT;
            MWIPLOTSTS.LAST_ACTIVE_HIST_SEQ = this.LAST_ACTIVE_HIST_SEQ;
            MWIPLOTSTS.LAST_HIST_SEQ = this.LAST_HIST_SEQ;
            MWIPLOTSTS.CRITICAL_RES_ID = this.CRITICAL_RES_ID;
            MWIPLOTSTS.CRITICAL_RES_GROUP_ID = this.CRITICAL_RES_GROUP_ID;
            MWIPLOTSTS.SAVE_RES_ID_1 = this.SAVE_RES_ID_1;
            MWIPLOTSTS.SAVE_RES_ID_2 = this.SAVE_RES_ID_2;
            MWIPLOTSTS.SUBRES_ID = this.SUBRES_ID;
            MWIPLOTSTS.LOT_GROUP_ID_1 = this.LOT_GROUP_ID_1;
            MWIPLOTSTS.LOT_GROUP_ID_2 = this.LOT_GROUP_ID_2;
            MWIPLOTSTS.LOT_GROUP_ID_3 = this.LOT_GROUP_ID_3;
            MWIPLOTSTS.RESV_FIELD_1 = this.RESV_FIELD_1;
            MWIPLOTSTS.RESV_FIELD_2 = this.RESV_FIELD_2;
            MWIPLOTSTS.RESV_FIELD_3 = this.RESV_FIELD_3;
            MWIPLOTSTS.RESV_FIELD_4 = this.RESV_FIELD_4;
            MWIPLOTSTS.RESV_FIELD_5 = this.RESV_FIELD_5;
            MWIPLOTSTS.RESV_FLAG_1 = this.RESV_FLAG_1;
            MWIPLOTSTS.RESV_FLAG_2 = this.RESV_FLAG_2;
            MWIPLOTSTS.RESV_FLAG_3 = this.RESV_FLAG_3;
            MWIPLOTSTS.RESV_FLAG_4 = this.RESV_FLAG_4;
            MWIPLOTSTS.RESV_FLAG_5 = this.RESV_FLAG_5;
        }
        catch(Exception ex)
        {
            if (ex.GetType().ToString() == "System.Data.OleDb.OleDbException")
                _dbc.gErrors.SetErrors(((OleDbException)ex).Errors);
            else
                _dbc.gErrors.SetErrors(ex);

            return null;
        }

        return MWIPLOTSTS;
    }

    /// <summary>
    /// Copy Column Data
    /// </summary>
    /// <param name="Row">One Record of AdoDataTable</param>
    /// <returns>true or false</returns>
    public bool CopyDataRow(DataRow Row)
    {
        try
        {
            Init();

            LOT_ID = (string)(Row["LOT_ID"]);
            LOT_DESC = (string)(Row["LOT_DESC"]);
            FACTORY = (string)(Row["FACTORY"]);
            MAT_ID = (string)(Row["MAT_ID"]);
            MAT_VER = Convert.ToInt32(Row["MAT_VER"]);
            FLOW = (string)(Row["FLOW"]);
            FLOW_SEQ_NUM = Convert.ToInt32(Row["FLOW_SEQ_NUM"]);
            OPER = (string)(Row["OPER"]);
            QTY_1 = Convert.ToDouble(Row["QTY_1"]);
            QTY_2 = Convert.ToDouble(Row["QTY_2"]);
            QTY_3 = Convert.ToDouble(Row["QTY_3"]);
            CRR_ID = (string)(Row["CRR_ID"]);
            LOT_TYPE = (string)(Row["LOT_TYPE"]);
            OWNER_CODE = (string)(Row["OWNER_CODE"]);
            CREATE_CODE = (string)(Row["CREATE_CODE"]);
            LOT_PRIORITY = (string)(Row["LOT_PRIORITY"]);
            LOT_STATUS = (string)(Row["LOT_STATUS"]);
            HOLD_FLAG = (string)(Row["HOLD_FLAG"]);
            HOLD_CODE = (string)(Row["HOLD_CODE"]);
            HOLD_PASSWORD = (string)(Row["HOLD_PASSWORD"]);
            HOLD_PRV_GRP_ID = (string)(Row["HOLD_PRV_GRP_ID"]);
            OPER_IN_QTY_1 = Convert.ToDouble(Row["OPER_IN_QTY_1"]);
            OPER_IN_QTY_2 = Convert.ToDouble(Row["OPER_IN_QTY_2"]);
            OPER_IN_QTY_3 = Convert.ToDouble(Row["OPER_IN_QTY_3"]);
            CREATE_QTY_1 = Convert.ToDouble(Row["CREATE_QTY_1"]);
            CREATE_QTY_2 = Convert.ToDouble(Row["CREATE_QTY_2"]);
            CREATE_QTY_3 = Convert.ToDouble(Row["CREATE_QTY_3"]);
            START_QTY_1 = Convert.ToDouble(Row["START_QTY_1"]);
            START_QTY_2 = Convert.ToDouble(Row["START_QTY_2"]);
            START_QTY_3 = Convert.ToDouble(Row["START_QTY_3"]);
            INV_FLAG = (string)(Row["INV_FLAG"]);
            TRANSIT_FLAG = (string)(Row["TRANSIT_FLAG"]);
            UNIT_EXIST_FLAG = (string)(Row["UNIT_EXIST_FLAG"]);
            INV_UNIT = (string)(Row["INV_UNIT"]);
            RWK_FLAG = (string)(Row["RWK_FLAG"]);
            RWK_CODE = (string)(Row["RWK_CODE"]);
            RWK_COUNT = Convert.ToInt32(Row["RWK_COUNT"]);
            RWK_RET_FLOW = (string)(Row["RWK_RET_FLOW"]);
            RWK_RET_FLOW_SEQ_NUM = Convert.ToInt32(Row["RWK_RET_FLOW_SEQ_NUM"]);
            RWK_RET_OPER = (string)(Row["RWK_RET_OPER"]);
            RWK_END_FLOW = (string)(Row["RWK_END_FLOW"]);
            RWK_END_FLOW_SEQ_NUM = Convert.ToInt32(Row["RWK_END_FLOW_SEQ_NUM"]);
            RWK_END_OPER = (string)(Row["RWK_END_OPER"]);
            RWK_RET_CLEAR_FLAG = (string)(Row["RWK_RET_CLEAR_FLAG"]);
            RWK_TIME = (string)(Row["RWK_TIME"]);
            NSTD_FLAG = (string)(Row["NSTD_FLAG"]);
            NSTD_RET_FLOW = (string)(Row["NSTD_RET_FLOW"]);
            NSTD_RET_FLOW_SEQ_NUM = Convert.ToInt32(Row["NSTD_RET_FLOW_SEQ_NUM"]);
            NSTD_RET_OPER = (string)(Row["NSTD_RET_OPER"]);
            NSTD_TIME = (string)(Row["NSTD_TIME"]);
            REP_FLAG = (string)(Row["REP_FLAG"]);
            REP_RET_OPER = (string)(Row["REP_RET_OPER"]);
            START_FLAG = (string)(Row["START_FLAG"]);
            START_TIME = (string)(Row["START_TIME"]);
            START_RES_ID = (string)(Row["START_RES_ID"]);
            END_FLAG = (string)(Row["END_FLAG"]);
            END_TIME = (string)(Row["END_TIME"]);
            END_RES_ID = (string)(Row["END_RES_ID"]);
            SAMPLE_FLAG = (string)(Row["SAMPLE_FLAG"]);
            SAMPLE_WAIT_FLAG = (string)(Row["SAMPLE_WAIT_FLAG"]);
            SAMPLE_RESULT = (string)(Row["SAMPLE_RESULT"]);
            FROM_TO_FLAG = (string)(Row["FROM_TO_FLAG"]);
            FROM_TO_LOT_ID = (string)(Row["FROM_TO_LOT_ID"]);
            SHIP_CODE = (string)(Row["SHIP_CODE"]);
            SHIP_TIME = (string)(Row["SHIP_TIME"]);
            ORG_DUE_TIME = (string)(Row["ORG_DUE_TIME"]);
            SCH_DUE_TIME = (string)(Row["SCH_DUE_TIME"]);
            CREATE_TIME = (string)(Row["CREATE_TIME"]);
            FAC_IN_TIME = (string)(Row["FAC_IN_TIME"]);
            FLOW_IN_TIME = (string)(Row["FLOW_IN_TIME"]);
            OPER_IN_TIME = (string)(Row["OPER_IN_TIME"]);
            RESERVE_RES_ID = (string)(Row["RESERVE_RES_ID"]);
            BATCH_ID = (string)(Row["BATCH_ID"]);
            BATCH_SEQ = Convert.ToInt32(Row["BATCH_SEQ"]);
            ORDER_ID = (string)(Row["ORDER_ID"]);
            ADD_ORDER_ID_1 = (string)(Row["ADD_ORDER_ID_1"]);
            ADD_ORDER_ID_2 = (string)(Row["ADD_ORDER_ID_2"]);
            ADD_ORDER_ID_3 = (string)(Row["ADD_ORDER_ID_3"]);
            LOT_LOCATION = (string)(Row["LOT_LOCATION"]);
            LOT_CMF_1 = (string)(Row["LOT_CMF_1"]);
            LOT_CMF_2 = (string)(Row["LOT_CMF_2"]);
            LOT_CMF_3 = (string)(Row["LOT_CMF_3"]);
            LOT_CMF_4 = (string)(Row["LOT_CMF_4"]);
            LOT_CMF_5 = (string)(Row["LOT_CMF_5"]);
            LOT_CMF_6 = (string)(Row["LOT_CMF_6"]);
            LOT_CMF_7 = (string)(Row["LOT_CMF_7"]);
            LOT_CMF_8 = (string)(Row["LOT_CMF_8"]);
            LOT_CMF_9 = (string)(Row["LOT_CMF_9"]);
            LOT_CMF_10 = (string)(Row["LOT_CMF_10"]);
            LOT_CMF_11 = (string)(Row["LOT_CMF_11"]);
            LOT_CMF_12 = (string)(Row["LOT_CMF_12"]);
            LOT_CMF_13 = (string)(Row["LOT_CMF_13"]);
            LOT_CMF_14 = (string)(Row["LOT_CMF_14"]);
            LOT_CMF_15 = (string)(Row["LOT_CMF_15"]);
            LOT_CMF_16 = (string)(Row["LOT_CMF_16"]);
            LOT_CMF_17 = (string)(Row["LOT_CMF_17"]);
            LOT_CMF_18 = (string)(Row["LOT_CMF_18"]);
            LOT_CMF_19 = (string)(Row["LOT_CMF_19"]);
            LOT_CMF_20 = (string)(Row["LOT_CMF_20"]);
            LOT_DEL_FLAG = (string)(Row["LOT_DEL_FLAG"]);
            LOT_DEL_CODE = (string)(Row["LOT_DEL_CODE"]);
            LOT_DEL_TIME = (string)(Row["LOT_DEL_TIME"]);
            BOM_SET_ID = (string)(Row["BOM_SET_ID"]);
            BOM_SET_VERSION = Convert.ToInt32(Row["BOM_SET_VERSION"]);
            BOM_ACTIVE_HIST_SEQ = Convert.ToInt32(Row["BOM_ACTIVE_HIST_SEQ"]);
            BOM_HIST_SEQ = Convert.ToInt32(Row["BOM_HIST_SEQ"]);
            LAST_TRAN_CODE = (string)(Row["LAST_TRAN_CODE"]);
            LAST_TRAN_TIME = (string)(Row["LAST_TRAN_TIME"]);
            LAST_COMMENT = (string)(Row["LAST_COMMENT"]);
            LAST_ACTIVE_HIST_SEQ = Convert.ToInt32(Row["LAST_ACTIVE_HIST_SEQ"]);
            LAST_HIST_SEQ = Convert.ToInt32(Row["LAST_HIST_SEQ"]);
            CRITICAL_RES_ID = (string)(Row["CRITICAL_RES_ID"]);
            CRITICAL_RES_GROUP_ID = (string)(Row["CRITICAL_RES_GROUP_ID"]);
            SAVE_RES_ID_1 = (string)(Row["SAVE_RES_ID_1"]);
            SAVE_RES_ID_2 = (string)(Row["SAVE_RES_ID_2"]);
            SUBRES_ID = (string)(Row["SUBRES_ID"]);
            LOT_GROUP_ID_1 = (string)(Row["LOT_GROUP_ID_1"]);
            LOT_GROUP_ID_2 = (string)(Row["LOT_GROUP_ID_2"]);
            LOT_GROUP_ID_3 = (string)(Row["LOT_GROUP_ID_3"]);
            RESV_FIELD_1 = (string)(Row["RESV_FIELD_1"]);
            RESV_FIELD_2 = (string)(Row["RESV_FIELD_2"]);
            RESV_FIELD_3 = (string)(Row["RESV_FIELD_3"]);
            RESV_FIELD_4 = (string)(Row["RESV_FIELD_4"]);
            RESV_FIELD_5 = (string)(Row["RESV_FIELD_5"]);
            RESV_FLAG_1 = (string)(Row["RESV_FLAG_1"]);
            RESV_FLAG_2 = (string)(Row["RESV_FLAG_2"]);
            RESV_FLAG_3 = (string)(Row["RESV_FLAG_3"]);
            RESV_FLAG_4 = (string)(Row["RESV_FLAG_4"]);
            RESV_FLAG_5 = (string)(Row["RESV_FLAG_5"]);
        }
        catch(Exception ex)
        {
            if (ex.GetType().ToString() == "System.Data.OleDb.OleDbException")
                _dbc.gErrors.SetErrors(((OleDbException)ex).Errors);
            else
                _dbc.gErrors.SetErrors(ex);

            return false;
        }

        return true;
    }

    /// <summary>
    /// Select Data
    /// </summary>
    /// <param name="_step">Query Selector</param>
    /// <param name="adoDataTable">Output DataTable</param>
    /// <returns>true or false</returns>
    public bool SelectData(int _step,ref DataTable adoDataTable)
    {
        int i = 0;
        string strQuery = "";

        try
        {
            if (_dbc.gErrors.MsgInit() == false) 
                return false;

            OleDbDataAdapter adoAdapter = new OleDbDataAdapter();
            adoAdapter.SelectCommand = new OleDbCommand();

            // Set Connection, Transaction
            adoAdapter.SelectCommand.Connection = _dbc.gOleDbConnection;
            adoAdapter.SelectCommand.Transaction = _dbc.gOleDbTransaction;
            adoDataTable = new DataTable();

            switch(_step)
            {
                case 1:
                    // Select by Primary Key
                    strQuery = "SELECT * FROM MWIPLOTSTS"
                        + " WHERE LOT_ID=?";

                    // Add Parameters
                    adoAdapter.SelectCommand.Parameters.Add("@LOT_ID", OleDbType.VarChar).Value = LOT_ID;
                    break;

                case 101:
                    // TODO : User Select Query
                    break;

                default:
                    // Error Handling
                    _dbc.gErrors.SqlCode = SQL_CODE.SQL_CASE_ERROR;
                    return false;
            }

            // Fill()
            adoAdapter.SelectCommand.CommandText = strQuery;
            _dbc.gErrors.AddQuery(strQuery, adoAdapter.SelectCommand.Parameters);
            adoAdapter.Fill(adoDataTable);

            // Check Data Not Found
            if (adoDataTable.Rows.Count < 1)
            {
                _dbc.gErrors.SqlCode = SQL_CODE.SQL_NOT_FOUND;
                return false;
            }

            // Set Fields
            for(i = 0 ; i <= adoDataTable.Columns.Count - 1; i++)
            {
                switch(adoDataTable.Columns[i].ColumnName)
                {
                    case "LOT_ID":
                        LOT_ID = (string)(adoDataTable.Rows[0]["LOT_ID"]);
                        break;
                    case "LOT_DESC":
                        LOT_DESC = (string)(adoDataTable.Rows[0]["LOT_DESC"]);
                        break;
                    case "FACTORY":
                        FACTORY = (string)(adoDataTable.Rows[0]["FACTORY"]);
                        break;
                    case "MAT_ID":
                        MAT_ID = (string)(adoDataTable.Rows[0]["MAT_ID"]);
                        break;
                    case "MAT_VER":
                        MAT_VER = Convert.ToInt32(adoDataTable.Rows[0]["MAT_VER"]);
                        break;
                    case "FLOW":
                        FLOW = (string)(adoDataTable.Rows[0]["FLOW"]);
                        break;
                    case "FLOW_SEQ_NUM":
                        FLOW_SEQ_NUM = Convert.ToInt32(adoDataTable.Rows[0]["FLOW_SEQ_NUM"]);
                        break;
                    case "OPER":
                        OPER = (string)(adoDataTable.Rows[0]["OPER"]);
                        break;
                    case "QTY_1":
                        QTY_1 = Convert.ToDouble(adoDataTable.Rows[0]["QTY_1"]);
                        break;
                    case "QTY_2":
                        QTY_2 = Convert.ToDouble(adoDataTable.Rows[0]["QTY_2"]);
                        break;
                    case "QTY_3":
                        QTY_3 = Convert.ToDouble(adoDataTable.Rows[0]["QTY_3"]);
                        break;
                    case "CRR_ID":
                        CRR_ID = (string)(adoDataTable.Rows[0]["CRR_ID"]);
                        break;
                    case "LOT_TYPE":
                        LOT_TYPE = (string)(adoDataTable.Rows[0]["LOT_TYPE"]);
                        break;
                    case "OWNER_CODE":
                        OWNER_CODE = (string)(adoDataTable.Rows[0]["OWNER_CODE"]);
                        break;
                    case "CREATE_CODE":
                        CREATE_CODE = (string)(adoDataTable.Rows[0]["CREATE_CODE"]);
                        break;
                    case "LOT_PRIORITY":
                        LOT_PRIORITY = (string)(adoDataTable.Rows[0]["LOT_PRIORITY"]);
                        break;
                    case "LOT_STATUS":
                        LOT_STATUS = (string)(adoDataTable.Rows[0]["LOT_STATUS"]);
                        break;
                    case "HOLD_FLAG":
                        HOLD_FLAG = (string)(adoDataTable.Rows[0]["HOLD_FLAG"]);
                        break;
                    case "HOLD_CODE":
                        HOLD_CODE = (string)(adoDataTable.Rows[0]["HOLD_CODE"]);
                        break;
                    case "HOLD_PASSWORD":
                        HOLD_PASSWORD = (string)(adoDataTable.Rows[0]["HOLD_PASSWORD"]);
                        break;
                    case "HOLD_PRV_GRP_ID":
                        HOLD_PRV_GRP_ID = (string)(adoDataTable.Rows[0]["HOLD_PRV_GRP_ID"]);
                        break;
                    case "OPER_IN_QTY_1":
                        OPER_IN_QTY_1 = Convert.ToDouble(adoDataTable.Rows[0]["OPER_IN_QTY_1"]);
                        break;
                    case "OPER_IN_QTY_2":
                        OPER_IN_QTY_2 = Convert.ToDouble(adoDataTable.Rows[0]["OPER_IN_QTY_2"]);
                        break;
                    case "OPER_IN_QTY_3":
                        OPER_IN_QTY_3 = Convert.ToDouble(adoDataTable.Rows[0]["OPER_IN_QTY_3"]);
                        break;
                    case "CREATE_QTY_1":
                        CREATE_QTY_1 = Convert.ToDouble(adoDataTable.Rows[0]["CREATE_QTY_1"]);
                        break;
                    case "CREATE_QTY_2":
                        CREATE_QTY_2 = Convert.ToDouble(adoDataTable.Rows[0]["CREATE_QTY_2"]);
                        break;
                    case "CREATE_QTY_3":
                        CREATE_QTY_3 = Convert.ToDouble(adoDataTable.Rows[0]["CREATE_QTY_3"]);
                        break;
                    case "START_QTY_1":
                        START_QTY_1 = Convert.ToDouble(adoDataTable.Rows[0]["START_QTY_1"]);
                        break;
                    case "START_QTY_2":
                        START_QTY_2 = Convert.ToDouble(adoDataTable.Rows[0]["START_QTY_2"]);
                        break;
                    case "START_QTY_3":
                        START_QTY_3 = Convert.ToDouble(adoDataTable.Rows[0]["START_QTY_3"]);
                        break;
                    case "INV_FLAG":
                        INV_FLAG = (string)(adoDataTable.Rows[0]["INV_FLAG"]);
                        break;
                    case "TRANSIT_FLAG":
                        TRANSIT_FLAG = (string)(adoDataTable.Rows[0]["TRANSIT_FLAG"]);
                        break;
                    case "UNIT_EXIST_FLAG":
                        UNIT_EXIST_FLAG = (string)(adoDataTable.Rows[0]["UNIT_EXIST_FLAG"]);
                        break;
                    case "INV_UNIT":
                        INV_UNIT = (string)(adoDataTable.Rows[0]["INV_UNIT"]);
                        break;
                    case "RWK_FLAG":
                        RWK_FLAG = (string)(adoDataTable.Rows[0]["RWK_FLAG"]);
                        break;
                    case "RWK_CODE":
                        RWK_CODE = (string)(adoDataTable.Rows[0]["RWK_CODE"]);
                        break;
                    case "RWK_COUNT":
                        RWK_COUNT = Convert.ToInt32(adoDataTable.Rows[0]["RWK_COUNT"]);
                        break;
                    case "RWK_RET_FLOW":
                        RWK_RET_FLOW = (string)(adoDataTable.Rows[0]["RWK_RET_FLOW"]);
                        break;
                    case "RWK_RET_FLOW_SEQ_NUM":
                        RWK_RET_FLOW_SEQ_NUM = Convert.ToInt32(adoDataTable.Rows[0]["RWK_RET_FLOW_SEQ_NUM"]);
                        break;
                    case "RWK_RET_OPER":
                        RWK_RET_OPER = (string)(adoDataTable.Rows[0]["RWK_RET_OPER"]);
                        break;
                    case "RWK_END_FLOW":
                        RWK_END_FLOW = (string)(adoDataTable.Rows[0]["RWK_END_FLOW"]);
                        break;
                    case "RWK_END_FLOW_SEQ_NUM":
                        RWK_END_FLOW_SEQ_NUM = Convert.ToInt32(adoDataTable.Rows[0]["RWK_END_FLOW_SEQ_NUM"]);
                        break;
                    case "RWK_END_OPER":
                        RWK_END_OPER = (string)(adoDataTable.Rows[0]["RWK_END_OPER"]);
                        break;
                    case "RWK_RET_CLEAR_FLAG":
                        RWK_RET_CLEAR_FLAG = (string)(adoDataTable.Rows[0]["RWK_RET_CLEAR_FLAG"]);
                        break;
                    case "RWK_TIME":
                        RWK_TIME = (string)(adoDataTable.Rows[0]["RWK_TIME"]);
                        break;
                    case "NSTD_FLAG":
                        NSTD_FLAG = (string)(adoDataTable.Rows[0]["NSTD_FLAG"]);
                        break;
                    case "NSTD_RET_FLOW":
                        NSTD_RET_FLOW = (string)(adoDataTable.Rows[0]["NSTD_RET_FLOW"]);
                        break;
                    case "NSTD_RET_FLOW_SEQ_NUM":
                        NSTD_RET_FLOW_SEQ_NUM = Convert.ToInt32(adoDataTable.Rows[0]["NSTD_RET_FLOW_SEQ_NUM"]);
                        break;
                    case "NSTD_RET_OPER":
                        NSTD_RET_OPER = (string)(adoDataTable.Rows[0]["NSTD_RET_OPER"]);
                        break;
                    case "NSTD_TIME":
                        NSTD_TIME = (string)(adoDataTable.Rows[0]["NSTD_TIME"]);
                        break;
                    case "REP_FLAG":
                        REP_FLAG = (string)(adoDataTable.Rows[0]["REP_FLAG"]);
                        break;
                    case "REP_RET_OPER":
                        REP_RET_OPER = (string)(adoDataTable.Rows[0]["REP_RET_OPER"]);
                        break;
                    case "START_FLAG":
                        START_FLAG = (string)(adoDataTable.Rows[0]["START_FLAG"]);
                        break;
                    case "START_TIME":
                        START_TIME = (string)(adoDataTable.Rows[0]["START_TIME"]);
                        break;
                    case "START_RES_ID":
                        START_RES_ID = (string)(adoDataTable.Rows[0]["START_RES_ID"]);
                        break;
                    case "END_FLAG":
                        END_FLAG = (string)(adoDataTable.Rows[0]["END_FLAG"]);
                        break;
                    case "END_TIME":
                        END_TIME = (string)(adoDataTable.Rows[0]["END_TIME"]);
                        break;
                    case "END_RES_ID":
                        END_RES_ID = (string)(adoDataTable.Rows[0]["END_RES_ID"]);
                        break;
                    case "SAMPLE_FLAG":
                        SAMPLE_FLAG = (string)(adoDataTable.Rows[0]["SAMPLE_FLAG"]);
                        break;
                    case "SAMPLE_WAIT_FLAG":
                        SAMPLE_WAIT_FLAG = (string)(adoDataTable.Rows[0]["SAMPLE_WAIT_FLAG"]);
                        break;
                    case "SAMPLE_RESULT":
                        SAMPLE_RESULT = (string)(adoDataTable.Rows[0]["SAMPLE_RESULT"]);
                        break;
                    case "FROM_TO_FLAG":
                        FROM_TO_FLAG = (string)(adoDataTable.Rows[0]["FROM_TO_FLAG"]);
                        break;
                    case "FROM_TO_LOT_ID":
                        FROM_TO_LOT_ID = (string)(adoDataTable.Rows[0]["FROM_TO_LOT_ID"]);
                        break;
                    case "SHIP_CODE":
                        SHIP_CODE = (string)(adoDataTable.Rows[0]["SHIP_CODE"]);
                        break;
                    case "SHIP_TIME":
                        SHIP_TIME = (string)(adoDataTable.Rows[0]["SHIP_TIME"]);
                        break;
                    case "ORG_DUE_TIME":
                        ORG_DUE_TIME = (string)(adoDataTable.Rows[0]["ORG_DUE_TIME"]);
                        break;
                    case "SCH_DUE_TIME":
                        SCH_DUE_TIME = (string)(adoDataTable.Rows[0]["SCH_DUE_TIME"]);
                        break;
                    case "CREATE_TIME":
                        CREATE_TIME = (string)(adoDataTable.Rows[0]["CREATE_TIME"]);
                        break;
                    case "FAC_IN_TIME":
                        FAC_IN_TIME = (string)(adoDataTable.Rows[0]["FAC_IN_TIME"]);
                        break;
                    case "FLOW_IN_TIME":
                        FLOW_IN_TIME = (string)(adoDataTable.Rows[0]["FLOW_IN_TIME"]);
                        break;
                    case "OPER_IN_TIME":
                        OPER_IN_TIME = (string)(adoDataTable.Rows[0]["OPER_IN_TIME"]);
                        break;
                    case "RESERVE_RES_ID":
                        RESERVE_RES_ID = (string)(adoDataTable.Rows[0]["RESERVE_RES_ID"]);
                        break;
                    case "BATCH_ID":
                        BATCH_ID = (string)(adoDataTable.Rows[0]["BATCH_ID"]);
                        break;
                    case "BATCH_SEQ":
                        BATCH_SEQ = Convert.ToInt32(adoDataTable.Rows[0]["BATCH_SEQ"]);
                        break;
                    case "ORDER_ID":
                        ORDER_ID = (string)(adoDataTable.Rows[0]["ORDER_ID"]);
                        break;
                    case "ADD_ORDER_ID_1":
                        ADD_ORDER_ID_1 = (string)(adoDataTable.Rows[0]["ADD_ORDER_ID_1"]);
                        break;
                    case "ADD_ORDER_ID_2":
                        ADD_ORDER_ID_2 = (string)(adoDataTable.Rows[0]["ADD_ORDER_ID_2"]);
                        break;
                    case "ADD_ORDER_ID_3":
                        ADD_ORDER_ID_3 = (string)(adoDataTable.Rows[0]["ADD_ORDER_ID_3"]);
                        break;
                    case "LOT_LOCATION":
                        LOT_LOCATION = (string)(adoDataTable.Rows[0]["LOT_LOCATION"]);
                        break;
                    case "LOT_CMF_1":
                        LOT_CMF_1 = (string)(adoDataTable.Rows[0]["LOT_CMF_1"]);
                        break;
                    case "LOT_CMF_2":
                        LOT_CMF_2 = (string)(adoDataTable.Rows[0]["LOT_CMF_2"]);
                        break;
                    case "LOT_CMF_3":
                        LOT_CMF_3 = (string)(adoDataTable.Rows[0]["LOT_CMF_3"]);
                        break;
                    case "LOT_CMF_4":
                        LOT_CMF_4 = (string)(adoDataTable.Rows[0]["LOT_CMF_4"]);
                        break;
                    case "LOT_CMF_5":
                        LOT_CMF_5 = (string)(adoDataTable.Rows[0]["LOT_CMF_5"]);
                        break;
                    case "LOT_CMF_6":
                        LOT_CMF_6 = (string)(adoDataTable.Rows[0]["LOT_CMF_6"]);
                        break;
                    case "LOT_CMF_7":
                        LOT_CMF_7 = (string)(adoDataTable.Rows[0]["LOT_CMF_7"]);
                        break;
                    case "LOT_CMF_8":
                        LOT_CMF_8 = (string)(adoDataTable.Rows[0]["LOT_CMF_8"]);
                        break;
                    case "LOT_CMF_9":
                        LOT_CMF_9 = (string)(adoDataTable.Rows[0]["LOT_CMF_9"]);
                        break;
                    case "LOT_CMF_10":
                        LOT_CMF_10 = (string)(adoDataTable.Rows[0]["LOT_CMF_10"]);
                        break;
                    case "LOT_CMF_11":
                        LOT_CMF_11 = (string)(adoDataTable.Rows[0]["LOT_CMF_11"]);
                        break;
                    case "LOT_CMF_12":
                        LOT_CMF_12 = (string)(adoDataTable.Rows[0]["LOT_CMF_12"]);
                        break;
                    case "LOT_CMF_13":
                        LOT_CMF_13 = (string)(adoDataTable.Rows[0]["LOT_CMF_13"]);
                        break;
                    case "LOT_CMF_14":
                        LOT_CMF_14 = (string)(adoDataTable.Rows[0]["LOT_CMF_14"]);
                        break;
                    case "LOT_CMF_15":
                        LOT_CMF_15 = (string)(adoDataTable.Rows[0]["LOT_CMF_15"]);
                        break;
                    case "LOT_CMF_16":
                        LOT_CMF_16 = (string)(adoDataTable.Rows[0]["LOT_CMF_16"]);
                        break;
                    case "LOT_CMF_17":
                        LOT_CMF_17 = (string)(adoDataTable.Rows[0]["LOT_CMF_17"]);
                        break;
                    case "LOT_CMF_18":
                        LOT_CMF_18 = (string)(adoDataTable.Rows[0]["LOT_CMF_18"]);
                        break;
                    case "LOT_CMF_19":
                        LOT_CMF_19 = (string)(adoDataTable.Rows[0]["LOT_CMF_19"]);
                        break;
                    case "LOT_CMF_20":
                        LOT_CMF_20 = (string)(adoDataTable.Rows[0]["LOT_CMF_20"]);
                        break;
                    case "LOT_DEL_FLAG":
                        LOT_DEL_FLAG = (string)(adoDataTable.Rows[0]["LOT_DEL_FLAG"]);
                        break;
                    case "LOT_DEL_CODE":
                        LOT_DEL_CODE = (string)(adoDataTable.Rows[0]["LOT_DEL_CODE"]);
                        break;
                    case "LOT_DEL_TIME":
                        LOT_DEL_TIME = (string)(adoDataTable.Rows[0]["LOT_DEL_TIME"]);
                        break;
                    case "BOM_SET_ID":
                        BOM_SET_ID = (string)(adoDataTable.Rows[0]["BOM_SET_ID"]);
                        break;
                    case "BOM_SET_VERSION":
                        BOM_SET_VERSION = Convert.ToInt32(adoDataTable.Rows[0]["BOM_SET_VERSION"]);
                        break;
                    case "BOM_ACTIVE_HIST_SEQ":
                        BOM_ACTIVE_HIST_SEQ = Convert.ToInt32(adoDataTable.Rows[0]["BOM_ACTIVE_HIST_SEQ"]);
                        break;
                    case "BOM_HIST_SEQ":
                        BOM_HIST_SEQ = Convert.ToInt32(adoDataTable.Rows[0]["BOM_HIST_SEQ"]);
                        break;
                    case "LAST_TRAN_CODE":
                        LAST_TRAN_CODE = (string)(adoDataTable.Rows[0]["LAST_TRAN_CODE"]);
                        break;
                    case "LAST_TRAN_TIME":
                        LAST_TRAN_TIME = (string)(adoDataTable.Rows[0]["LAST_TRAN_TIME"]);
                        break;
                    case "LAST_COMMENT":
                        LAST_COMMENT = (string)(adoDataTable.Rows[0]["LAST_COMMENT"]);
                        break;
                    case "LAST_ACTIVE_HIST_SEQ":
                        LAST_ACTIVE_HIST_SEQ = Convert.ToInt32(adoDataTable.Rows[0]["LAST_ACTIVE_HIST_SEQ"]);
                        break;
                    case "LAST_HIST_SEQ":
                        LAST_HIST_SEQ = Convert.ToInt32(adoDataTable.Rows[0]["LAST_HIST_SEQ"]);
                        break;
                    case "CRITICAL_RES_ID":
                        CRITICAL_RES_ID = (string)(adoDataTable.Rows[0]["CRITICAL_RES_ID"]);
                        break;
                    case "CRITICAL_RES_GROUP_ID":
                        CRITICAL_RES_GROUP_ID = (string)(adoDataTable.Rows[0]["CRITICAL_RES_GROUP_ID"]);
                        break;
                    case "SAVE_RES_ID_1":
                        SAVE_RES_ID_1 = (string)(adoDataTable.Rows[0]["SAVE_RES_ID_1"]);
                        break;
                    case "SAVE_RES_ID_2":
                        SAVE_RES_ID_2 = (string)(adoDataTable.Rows[0]["SAVE_RES_ID_2"]);
                        break;
                    case "SUBRES_ID":
                        SUBRES_ID = (string)(adoDataTable.Rows[0]["SUBRES_ID"]);
                        break;
                    case "LOT_GROUP_ID_1":
                        LOT_GROUP_ID_1 = (string)(adoDataTable.Rows[0]["LOT_GROUP_ID_1"]);
                        break;
                    case "LOT_GROUP_ID_2":
                        LOT_GROUP_ID_2 = (string)(adoDataTable.Rows[0]["LOT_GROUP_ID_2"]);
                        break;
                    case "LOT_GROUP_ID_3":
                        LOT_GROUP_ID_3 = (string)(adoDataTable.Rows[0]["LOT_GROUP_ID_3"]);
                        break;
                    case "RESV_FIELD_1":
                        RESV_FIELD_1 = (string)(adoDataTable.Rows[0]["RESV_FIELD_1"]);
                        break;
                    case "RESV_FIELD_2":
                        RESV_FIELD_2 = (string)(adoDataTable.Rows[0]["RESV_FIELD_2"]);
                        break;
                    case "RESV_FIELD_3":
                        RESV_FIELD_3 = (string)(adoDataTable.Rows[0]["RESV_FIELD_3"]);
                        break;
                    case "RESV_FIELD_4":
                        RESV_FIELD_4 = (string)(adoDataTable.Rows[0]["RESV_FIELD_4"]);
                        break;
                    case "RESV_FIELD_5":
                        RESV_FIELD_5 = (string)(adoDataTable.Rows[0]["RESV_FIELD_5"]);
                        break;
                    case "RESV_FLAG_1":
                        RESV_FLAG_1 = (string)(adoDataTable.Rows[0]["RESV_FLAG_1"]);
                        break;
                    case "RESV_FLAG_2":
                        RESV_FLAG_2 = (string)(adoDataTable.Rows[0]["RESV_FLAG_2"]);
                        break;
                    case "RESV_FLAG_3":
                        RESV_FLAG_3 = (string)(adoDataTable.Rows[0]["RESV_FLAG_3"]);
                        break;
                    case "RESV_FLAG_4":
                        RESV_FLAG_4 = (string)(adoDataTable.Rows[0]["RESV_FLAG_4"]);
                        break;
                    case "RESV_FLAG_5":
                        RESV_FLAG_5 = (string)(adoDataTable.Rows[0]["RESV_FLAG_5"]);
                        break;
                }
            }

        }
        catch(Exception ex)
        {
            if (ex.GetType().ToString() == "System.Data.OleDb.OleDbException")
                _dbc.gErrors.SetErrors(((OleDbException)ex).Errors);
            else
                _dbc.gErrors.SetErrors(ex);

            return false;
        }

        return true;
    }
    /// <summary>
    /// Select Data For Update
    /// </summary>
    /// <param name="_step">Query Selector</param>
    /// <param name="adoDataTable">Output DataTable</param>
    /// <returns>true or false</returns>
    public bool SelectDataForUpdate(int _step, ref DataTable adoDataTable)
    {
        int i = 0;
        string strQuery = "";

        try
        {
            if (_dbc.gErrors.MsgInit() == false) 
                return false;

            OleDbDataAdapter adoAdapter = new OleDbDataAdapter();
            adoAdapter.SelectCommand = new OleDbCommand();

            // Set Connection, Transaction
            adoAdapter.SelectCommand.Connection = _dbc.gOleDbConnection;
            adoAdapter.SelectCommand.Transaction = _dbc.gOleDbTransaction;
            adoDataTable = new DataTable();

            switch(_step)
            {
                case 1:
                    // Select for Update by Primary Key
                    if (_dbc.gDbType == (int)DB_TYPE.MSSQL) 
                    {
                        strQuery = "SELECT * FROM MWIPLOTSTS"
                            + " WITH (UPDLOCK) WHERE LOT_ID=?";
                    }
                    else if (_dbc.gDbType == (int)DB_TYPE.ORACLE)
                    {
                        strQuery = "SELECT * FROM MWIPLOTSTS"
                            + " WHERE LOT_ID=?"
                            + " FOR UPDATE";
                    }
                    else if (_dbc.gDbType == (int)DB_TYPE.DB2) 
                    {
                    }

                    // Add Parameters
                    adoAdapter.SelectCommand.Parameters.Add("@LOT_ID", OleDbType.VarChar).Value = LOT_ID;
                    break;

                case 101:
                    // TODO : User Select Query
                    break;

                default:
                    // Error Handling
                    _dbc.gErrors.SqlCode = SQL_CODE.SQL_CASE_ERROR;
                    return false;
            }

            // Fill()
            adoAdapter.SelectCommand.CommandText = strQuery;
            _dbc.gErrors.AddQuery(strQuery, adoAdapter.SelectCommand.Parameters);
            adoAdapter.Fill(adoDataTable);

            // Check Data Not Found
            if (adoDataTable.Rows.Count < 1)
            {
                _dbc.gErrors.SqlCode = SQL_CODE.SQL_NOT_FOUND;
                return false;
            }

            // Set Fields
            for (i = 0; i <= adoDataTable.Columns.Count - 1; i++)
            {
                switch(adoDataTable.Columns[i].ColumnName)
                {
                    case "LOT_ID":
                        LOT_ID = (string)(adoDataTable.Rows[0]["LOT_ID"]);
                        break;
                    case "LOT_DESC":
                        LOT_DESC = (string)(adoDataTable.Rows[0]["LOT_DESC"]);
                        break;
                    case "FACTORY":
                        FACTORY = (string)(adoDataTable.Rows[0]["FACTORY"]);
                        break;
                    case "MAT_ID":
                        MAT_ID = (string)(adoDataTable.Rows[0]["MAT_ID"]);
                        break;
                    case "MAT_VER":
                        MAT_VER = Convert.ToInt32(adoDataTable.Rows[0]["MAT_VER"]);
                        break;
                    case "FLOW":
                        FLOW = (string)(adoDataTable.Rows[0]["FLOW"]);
                        break;
                    case "FLOW_SEQ_NUM":
                        FLOW_SEQ_NUM = Convert.ToInt32(adoDataTable.Rows[0]["FLOW_SEQ_NUM"]);
                        break;
                    case "OPER":
                        OPER = (string)(adoDataTable.Rows[0]["OPER"]);
                        break;
                    case "QTY_1":
                        QTY_1 = Convert.ToDouble(adoDataTable.Rows[0]["QTY_1"]);
                        break;
                    case "QTY_2":
                        QTY_2 = Convert.ToDouble(adoDataTable.Rows[0]["QTY_2"]);
                        break;
                    case "QTY_3":
                        QTY_3 = Convert.ToDouble(adoDataTable.Rows[0]["QTY_3"]);
                        break;
                    case "CRR_ID":
                        CRR_ID = (string)(adoDataTable.Rows[0]["CRR_ID"]);
                        break;
                    case "LOT_TYPE":
                        LOT_TYPE = (string)(adoDataTable.Rows[0]["LOT_TYPE"]);
                        break;
                    case "OWNER_CODE":
                        OWNER_CODE = (string)(adoDataTable.Rows[0]["OWNER_CODE"]);
                        break;
                    case "CREATE_CODE":
                        CREATE_CODE = (string)(adoDataTable.Rows[0]["CREATE_CODE"]);
                        break;
                    case "LOT_PRIORITY":
                        LOT_PRIORITY = (string)(adoDataTable.Rows[0]["LOT_PRIORITY"]);
                        break;
                    case "LOT_STATUS":
                        LOT_STATUS = (string)(adoDataTable.Rows[0]["LOT_STATUS"]);
                        break;
                    case "HOLD_FLAG":
                        HOLD_FLAG = (string)(adoDataTable.Rows[0]["HOLD_FLAG"]);
                        break;
                    case "HOLD_CODE":
                        HOLD_CODE = (string)(adoDataTable.Rows[0]["HOLD_CODE"]);
                        break;
                    case "HOLD_PASSWORD":
                        HOLD_PASSWORD = (string)(adoDataTable.Rows[0]["HOLD_PASSWORD"]);
                        break;
                    case "HOLD_PRV_GRP_ID":
                        HOLD_PRV_GRP_ID = (string)(adoDataTable.Rows[0]["HOLD_PRV_GRP_ID"]);
                        break;
                    case "OPER_IN_QTY_1":
                        OPER_IN_QTY_1 = Convert.ToDouble(adoDataTable.Rows[0]["OPER_IN_QTY_1"]);
                        break;
                    case "OPER_IN_QTY_2":
                        OPER_IN_QTY_2 = Convert.ToDouble(adoDataTable.Rows[0]["OPER_IN_QTY_2"]);
                        break;
                    case "OPER_IN_QTY_3":
                        OPER_IN_QTY_3 = Convert.ToDouble(adoDataTable.Rows[0]["OPER_IN_QTY_3"]);
                        break;
                    case "CREATE_QTY_1":
                        CREATE_QTY_1 = Convert.ToDouble(adoDataTable.Rows[0]["CREATE_QTY_1"]);
                        break;
                    case "CREATE_QTY_2":
                        CREATE_QTY_2 = Convert.ToDouble(adoDataTable.Rows[0]["CREATE_QTY_2"]);
                        break;
                    case "CREATE_QTY_3":
                        CREATE_QTY_3 = Convert.ToDouble(adoDataTable.Rows[0]["CREATE_QTY_3"]);
                        break;
                    case "START_QTY_1":
                        START_QTY_1 = Convert.ToDouble(adoDataTable.Rows[0]["START_QTY_1"]);
                        break;
                    case "START_QTY_2":
                        START_QTY_2 = Convert.ToDouble(adoDataTable.Rows[0]["START_QTY_2"]);
                        break;
                    case "START_QTY_3":
                        START_QTY_3 = Convert.ToDouble(adoDataTable.Rows[0]["START_QTY_3"]);
                        break;
                    case "INV_FLAG":
                        INV_FLAG = (string)(adoDataTable.Rows[0]["INV_FLAG"]);
                        break;
                    case "TRANSIT_FLAG":
                        TRANSIT_FLAG = (string)(adoDataTable.Rows[0]["TRANSIT_FLAG"]);
                        break;
                    case "UNIT_EXIST_FLAG":
                        UNIT_EXIST_FLAG = (string)(adoDataTable.Rows[0]["UNIT_EXIST_FLAG"]);
                        break;
                    case "INV_UNIT":
                        INV_UNIT = (string)(adoDataTable.Rows[0]["INV_UNIT"]);
                        break;
                    case "RWK_FLAG":
                        RWK_FLAG = (string)(adoDataTable.Rows[0]["RWK_FLAG"]);
                        break;
                    case "RWK_CODE":
                        RWK_CODE = (string)(adoDataTable.Rows[0]["RWK_CODE"]);
                        break;
                    case "RWK_COUNT":
                        RWK_COUNT = Convert.ToInt32(adoDataTable.Rows[0]["RWK_COUNT"]);
                        break;
                    case "RWK_RET_FLOW":
                        RWK_RET_FLOW = (string)(adoDataTable.Rows[0]["RWK_RET_FLOW"]);
                        break;
                    case "RWK_RET_FLOW_SEQ_NUM":
                        RWK_RET_FLOW_SEQ_NUM = Convert.ToInt32(adoDataTable.Rows[0]["RWK_RET_FLOW_SEQ_NUM"]);
                        break;
                    case "RWK_RET_OPER":
                        RWK_RET_OPER = (string)(adoDataTable.Rows[0]["RWK_RET_OPER"]);
                        break;
                    case "RWK_END_FLOW":
                        RWK_END_FLOW = (string)(adoDataTable.Rows[0]["RWK_END_FLOW"]);
                        break;
                    case "RWK_END_FLOW_SEQ_NUM":
                        RWK_END_FLOW_SEQ_NUM = Convert.ToInt32(adoDataTable.Rows[0]["RWK_END_FLOW_SEQ_NUM"]);
                        break;
                    case "RWK_END_OPER":
                        RWK_END_OPER = (string)(adoDataTable.Rows[0]["RWK_END_OPER"]);
                        break;
                    case "RWK_RET_CLEAR_FLAG":
                        RWK_RET_CLEAR_FLAG = (string)(adoDataTable.Rows[0]["RWK_RET_CLEAR_FLAG"]);
                        break;
                    case "RWK_TIME":
                        RWK_TIME = (string)(adoDataTable.Rows[0]["RWK_TIME"]);
                        break;
                    case "NSTD_FLAG":
                        NSTD_FLAG = (string)(adoDataTable.Rows[0]["NSTD_FLAG"]);
                        break;
                    case "NSTD_RET_FLOW":
                        NSTD_RET_FLOW = (string)(adoDataTable.Rows[0]["NSTD_RET_FLOW"]);
                        break;
                    case "NSTD_RET_FLOW_SEQ_NUM":
                        NSTD_RET_FLOW_SEQ_NUM = Convert.ToInt32(adoDataTable.Rows[0]["NSTD_RET_FLOW_SEQ_NUM"]);
                        break;
                    case "NSTD_RET_OPER":
                        NSTD_RET_OPER = (string)(adoDataTable.Rows[0]["NSTD_RET_OPER"]);
                        break;
                    case "NSTD_TIME":
                        NSTD_TIME = (string)(adoDataTable.Rows[0]["NSTD_TIME"]);
                        break;
                    case "REP_FLAG":
                        REP_FLAG = (string)(adoDataTable.Rows[0]["REP_FLAG"]);
                        break;
                    case "REP_RET_OPER":
                        REP_RET_OPER = (string)(adoDataTable.Rows[0]["REP_RET_OPER"]);
                        break;
                    case "START_FLAG":
                        START_FLAG = (string)(adoDataTable.Rows[0]["START_FLAG"]);
                        break;
                    case "START_TIME":
                        START_TIME = (string)(adoDataTable.Rows[0]["START_TIME"]);
                        break;
                    case "START_RES_ID":
                        START_RES_ID = (string)(adoDataTable.Rows[0]["START_RES_ID"]);
                        break;
                    case "END_FLAG":
                        END_FLAG = (string)(adoDataTable.Rows[0]["END_FLAG"]);
                        break;
                    case "END_TIME":
                        END_TIME = (string)(adoDataTable.Rows[0]["END_TIME"]);
                        break;
                    case "END_RES_ID":
                        END_RES_ID = (string)(adoDataTable.Rows[0]["END_RES_ID"]);
                        break;
                    case "SAMPLE_FLAG":
                        SAMPLE_FLAG = (string)(adoDataTable.Rows[0]["SAMPLE_FLAG"]);
                        break;
                    case "SAMPLE_WAIT_FLAG":
                        SAMPLE_WAIT_FLAG = (string)(adoDataTable.Rows[0]["SAMPLE_WAIT_FLAG"]);
                        break;
                    case "SAMPLE_RESULT":
                        SAMPLE_RESULT = (string)(adoDataTable.Rows[0]["SAMPLE_RESULT"]);
                        break;
                    case "FROM_TO_FLAG":
                        FROM_TO_FLAG = (string)(adoDataTable.Rows[0]["FROM_TO_FLAG"]);
                        break;
                    case "FROM_TO_LOT_ID":
                        FROM_TO_LOT_ID = (string)(adoDataTable.Rows[0]["FROM_TO_LOT_ID"]);
                        break;
                    case "SHIP_CODE":
                        SHIP_CODE = (string)(adoDataTable.Rows[0]["SHIP_CODE"]);
                        break;
                    case "SHIP_TIME":
                        SHIP_TIME = (string)(adoDataTable.Rows[0]["SHIP_TIME"]);
                        break;
                    case "ORG_DUE_TIME":
                        ORG_DUE_TIME = (string)(adoDataTable.Rows[0]["ORG_DUE_TIME"]);
                        break;
                    case "SCH_DUE_TIME":
                        SCH_DUE_TIME = (string)(adoDataTable.Rows[0]["SCH_DUE_TIME"]);
                        break;
                    case "CREATE_TIME":
                        CREATE_TIME = (string)(adoDataTable.Rows[0]["CREATE_TIME"]);
                        break;
                    case "FAC_IN_TIME":
                        FAC_IN_TIME = (string)(adoDataTable.Rows[0]["FAC_IN_TIME"]);
                        break;
                    case "FLOW_IN_TIME":
                        FLOW_IN_TIME = (string)(adoDataTable.Rows[0]["FLOW_IN_TIME"]);
                        break;
                    case "OPER_IN_TIME":
                        OPER_IN_TIME = (string)(adoDataTable.Rows[0]["OPER_IN_TIME"]);
                        break;
                    case "RESERVE_RES_ID":
                        RESERVE_RES_ID = (string)(adoDataTable.Rows[0]["RESERVE_RES_ID"]);
                        break;
                    case "BATCH_ID":
                        BATCH_ID = (string)(adoDataTable.Rows[0]["BATCH_ID"]);
                        break;
                    case "BATCH_SEQ":
                        BATCH_SEQ = Convert.ToInt32(adoDataTable.Rows[0]["BATCH_SEQ"]);
                        break;
                    case "ORDER_ID":
                        ORDER_ID = (string)(adoDataTable.Rows[0]["ORDER_ID"]);
                        break;
                    case "ADD_ORDER_ID_1":
                        ADD_ORDER_ID_1 = (string)(adoDataTable.Rows[0]["ADD_ORDER_ID_1"]);
                        break;
                    case "ADD_ORDER_ID_2":
                        ADD_ORDER_ID_2 = (string)(adoDataTable.Rows[0]["ADD_ORDER_ID_2"]);
                        break;
                    case "ADD_ORDER_ID_3":
                        ADD_ORDER_ID_3 = (string)(adoDataTable.Rows[0]["ADD_ORDER_ID_3"]);
                        break;
                    case "LOT_LOCATION":
                        LOT_LOCATION = (string)(adoDataTable.Rows[0]["LOT_LOCATION"]);
                        break;
                    case "LOT_CMF_1":
                        LOT_CMF_1 = (string)(adoDataTable.Rows[0]["LOT_CMF_1"]);
                        break;
                    case "LOT_CMF_2":
                        LOT_CMF_2 = (string)(adoDataTable.Rows[0]["LOT_CMF_2"]);
                        break;
                    case "LOT_CMF_3":
                        LOT_CMF_3 = (string)(adoDataTable.Rows[0]["LOT_CMF_3"]);
                        break;
                    case "LOT_CMF_4":
                        LOT_CMF_4 = (string)(adoDataTable.Rows[0]["LOT_CMF_4"]);
                        break;
                    case "LOT_CMF_5":
                        LOT_CMF_5 = (string)(adoDataTable.Rows[0]["LOT_CMF_5"]);
                        break;
                    case "LOT_CMF_6":
                        LOT_CMF_6 = (string)(adoDataTable.Rows[0]["LOT_CMF_6"]);
                        break;
                    case "LOT_CMF_7":
                        LOT_CMF_7 = (string)(adoDataTable.Rows[0]["LOT_CMF_7"]);
                        break;
                    case "LOT_CMF_8":
                        LOT_CMF_8 = (string)(adoDataTable.Rows[0]["LOT_CMF_8"]);
                        break;
                    case "LOT_CMF_9":
                        LOT_CMF_9 = (string)(adoDataTable.Rows[0]["LOT_CMF_9"]);
                        break;
                    case "LOT_CMF_10":
                        LOT_CMF_10 = (string)(adoDataTable.Rows[0]["LOT_CMF_10"]);
                        break;
                    case "LOT_CMF_11":
                        LOT_CMF_11 = (string)(adoDataTable.Rows[0]["LOT_CMF_11"]);
                        break;
                    case "LOT_CMF_12":
                        LOT_CMF_12 = (string)(adoDataTable.Rows[0]["LOT_CMF_12"]);
                        break;
                    case "LOT_CMF_13":
                        LOT_CMF_13 = (string)(adoDataTable.Rows[0]["LOT_CMF_13"]);
                        break;
                    case "LOT_CMF_14":
                        LOT_CMF_14 = (string)(adoDataTable.Rows[0]["LOT_CMF_14"]);
                        break;
                    case "LOT_CMF_15":
                        LOT_CMF_15 = (string)(adoDataTable.Rows[0]["LOT_CMF_15"]);
                        break;
                    case "LOT_CMF_16":
                        LOT_CMF_16 = (string)(adoDataTable.Rows[0]["LOT_CMF_16"]);
                        break;
                    case "LOT_CMF_17":
                        LOT_CMF_17 = (string)(adoDataTable.Rows[0]["LOT_CMF_17"]);
                        break;
                    case "LOT_CMF_18":
                        LOT_CMF_18 = (string)(adoDataTable.Rows[0]["LOT_CMF_18"]);
                        break;
                    case "LOT_CMF_19":
                        LOT_CMF_19 = (string)(adoDataTable.Rows[0]["LOT_CMF_19"]);
                        break;
                    case "LOT_CMF_20":
                        LOT_CMF_20 = (string)(adoDataTable.Rows[0]["LOT_CMF_20"]);
                        break;
                    case "LOT_DEL_FLAG":
                        LOT_DEL_FLAG = (string)(adoDataTable.Rows[0]["LOT_DEL_FLAG"]);
                        break;
                    case "LOT_DEL_CODE":
                        LOT_DEL_CODE = (string)(adoDataTable.Rows[0]["LOT_DEL_CODE"]);
                        break;
                    case "LOT_DEL_TIME":
                        LOT_DEL_TIME = (string)(adoDataTable.Rows[0]["LOT_DEL_TIME"]);
                        break;
                    case "BOM_SET_ID":
                        BOM_SET_ID = (string)(adoDataTable.Rows[0]["BOM_SET_ID"]);
                        break;
                    case "BOM_SET_VERSION":
                        BOM_SET_VERSION = Convert.ToInt32(adoDataTable.Rows[0]["BOM_SET_VERSION"]);
                        break;
                    case "BOM_ACTIVE_HIST_SEQ":
                        BOM_ACTIVE_HIST_SEQ = Convert.ToInt32(adoDataTable.Rows[0]["BOM_ACTIVE_HIST_SEQ"]);
                        break;
                    case "BOM_HIST_SEQ":
                        BOM_HIST_SEQ = Convert.ToInt32(adoDataTable.Rows[0]["BOM_HIST_SEQ"]);
                        break;
                    case "LAST_TRAN_CODE":
                        LAST_TRAN_CODE = (string)(adoDataTable.Rows[0]["LAST_TRAN_CODE"]);
                        break;
                    case "LAST_TRAN_TIME":
                        LAST_TRAN_TIME = (string)(adoDataTable.Rows[0]["LAST_TRAN_TIME"]);
                        break;
                    case "LAST_COMMENT":
                        LAST_COMMENT = (string)(adoDataTable.Rows[0]["LAST_COMMENT"]);
                        break;
                    case "LAST_ACTIVE_HIST_SEQ":
                        LAST_ACTIVE_HIST_SEQ = Convert.ToInt32(adoDataTable.Rows[0]["LAST_ACTIVE_HIST_SEQ"]);
                        break;
                    case "LAST_HIST_SEQ":
                        LAST_HIST_SEQ = Convert.ToInt32(adoDataTable.Rows[0]["LAST_HIST_SEQ"]);
                        break;
                    case "CRITICAL_RES_ID":
                        CRITICAL_RES_ID = (string)(adoDataTable.Rows[0]["CRITICAL_RES_ID"]);
                        break;
                    case "CRITICAL_RES_GROUP_ID":
                        CRITICAL_RES_GROUP_ID = (string)(adoDataTable.Rows[0]["CRITICAL_RES_GROUP_ID"]);
                        break;
                    case "SAVE_RES_ID_1":
                        SAVE_RES_ID_1 = (string)(adoDataTable.Rows[0]["SAVE_RES_ID_1"]);
                        break;
                    case "SAVE_RES_ID_2":
                        SAVE_RES_ID_2 = (string)(adoDataTable.Rows[0]["SAVE_RES_ID_2"]);
                        break;
                    case "SUBRES_ID":
                        SUBRES_ID = (string)(adoDataTable.Rows[0]["SUBRES_ID"]);
                        break;
                    case "LOT_GROUP_ID_1":
                        LOT_GROUP_ID_1 = (string)(adoDataTable.Rows[0]["LOT_GROUP_ID_1"]);
                        break;
                    case "LOT_GROUP_ID_2":
                        LOT_GROUP_ID_2 = (string)(adoDataTable.Rows[0]["LOT_GROUP_ID_2"]);
                        break;
                    case "LOT_GROUP_ID_3":
                        LOT_GROUP_ID_3 = (string)(adoDataTable.Rows[0]["LOT_GROUP_ID_3"]);
                        break;
                    case "RESV_FIELD_1":
                        RESV_FIELD_1 = (string)(adoDataTable.Rows[0]["RESV_FIELD_1"]);
                        break;
                    case "RESV_FIELD_2":
                        RESV_FIELD_2 = (string)(adoDataTable.Rows[0]["RESV_FIELD_2"]);
                        break;
                    case "RESV_FIELD_3":
                        RESV_FIELD_3 = (string)(adoDataTable.Rows[0]["RESV_FIELD_3"]);
                        break;
                    case "RESV_FIELD_4":
                        RESV_FIELD_4 = (string)(adoDataTable.Rows[0]["RESV_FIELD_4"]);
                        break;
                    case "RESV_FIELD_5":
                        RESV_FIELD_5 = (string)(adoDataTable.Rows[0]["RESV_FIELD_5"]);
                        break;
                    case "RESV_FLAG_1":
                        RESV_FLAG_1 = (string)(adoDataTable.Rows[0]["RESV_FLAG_1"]);
                        break;
                    case "RESV_FLAG_2":
                        RESV_FLAG_2 = (string)(adoDataTable.Rows[0]["RESV_FLAG_2"]);
                        break;
                    case "RESV_FLAG_3":
                        RESV_FLAG_3 = (string)(adoDataTable.Rows[0]["RESV_FLAG_3"]);
                        break;
                    case "RESV_FLAG_4":
                        RESV_FLAG_4 = (string)(adoDataTable.Rows[0]["RESV_FLAG_4"]);
                        break;
                    case "RESV_FLAG_5":
                        RESV_FLAG_5 = (string)(adoDataTable.Rows[0]["RESV_FLAG_5"]);
                        break;
                }
            }
        }
        catch(Exception ex)
        {
            if (ex.GetType().ToString() == "System.Data.OleDb.OleDbException")
                _dbc.gErrors.SetErrors(((OleDbException)ex).Errors);
            else
                _dbc.gErrors.SetErrors(ex);

            return false;
        }

        return true;
    }

    /// <summary>
    /// Select Scalar
    /// </summary>
    /// <param name="_step">Query Selector</param>
    /// <param name="dResult">Output Scalar</param>
    /// <returns>true or false</returns>
    public bool SelectDataScalar(int _step, ref double dResult)
    {
        string strQuery = "";

        try
        {
            if (_dbc.gErrors.MsgInit() == false)
                return false;
            OleDbCommand adoCommand = new OleDbCommand();

            // Set Connection, Transaction
            adoCommand.Connection = _dbc.gOleDbConnection;
            adoCommand.Transaction = _dbc.gOleDbTransaction;

            switch(_step)
            {
                case 1:
                    // Select Count by Primary Key
                    strQuery = "SELECT COUNT(*) FROM MWIPLOTSTS"
                        + " WHERE LOT_ID=?";

                    // Add Parameters
                    adoCommand.Parameters.Add("@LOT_ID", OleDbType.VarChar).Value = LOT_ID;
                    break;

                case 101:
                    // TODO : User Select Query
                    break;

                default:
                    // Error Handling
                    _dbc.gErrors.SqlCode = SQL_CODE.SQL_CASE_ERROR;
                    return false;
            }

            // Execute Scalar
            adoCommand.CommandText = strQuery;
            _dbc.gErrors.AddQuery(strQuery, adoCommand.Parameters);
            dResult = (double)adoCommand.ExecuteScalar();
        }
        catch(Exception ex)
        {
            if (ex.GetType().ToString() == "System.Data.OleDb.OleDbException")
                _dbc.gErrors.SetErrors(((OleDbException)ex).Errors);
            else
                _dbc.gErrors.SetErrors(ex);

            return false;
        }

        return true;
    }

    /// <summary>
    /// Delete Data
    /// </summary>
    /// <param name="_step">Query Selector</param>
    /// <returns>true or false</returns>
    public bool DeleteData(int _step)
    {
        string strQuery = "";

        try
        {
            if (_dbc.gErrors.MsgInit() == false)
                return false;
            OleDbCommand adoCommand = new OleDbCommand();

            // Set Connection, Transaction
            adoCommand.Connection = _dbc.gOleDbConnection;
            adoCommand.Transaction = _dbc.gOleDbTransaction;

            switch(_step)
            {
                case 1:
                    // Delete by Primary Key
                    strQuery = "DELETE FROM MWIPLOTSTS"
                        + " WHERE LOT_ID=?";

                    // Add Parameters
                    adoCommand.Parameters.Add("@LOT_ID", OleDbType.VarChar).Value = LOT_ID;
                    break;

                case 101:
                    // TODO : User Select Query
                    break;

                default:
                    // Error Handling
                    _dbc.gErrors.SqlCode = SQL_CODE.SQL_CASE_ERROR;
                    return false;
            }

            // ExecuteNonQuery()
            adoCommand.CommandText = strQuery;
            _dbc.gErrors.AddQuery(strQuery, adoCommand.Parameters);
            if (adoCommand.ExecuteNonQuery() < 1)
            {
                // Data Not Found
                _dbc.gErrors.SqlCode = SQL_CODE.SQL_NOT_FOUND;
                // Dispose
                adoCommand.Dispose();
                return false;
            }

            // Dispose
            adoCommand.Dispose();
        }
        catch(Exception ex)
        {
            if (ex.GetType().ToString() == "System.Data.OleDb.OleDbException")
                _dbc.gErrors.SetErrors(((OleDbException)ex).Errors);
            else
                _dbc.gErrors.SetErrors(ex);

            return false;
        }

        return true;
    }

    /// <summary>
    /// Insert Data
    /// </summary>
    /// <returns>true or false</returns>
    public bool InsertData()
    {
        string strQuery = "";

        try
        {
            if (_dbc.gErrors.MsgInit() == false)
                return false;
            OleDbCommand adoCommand = new OleDbCommand();

            // Set Connection, Transaction
            adoCommand.Connection = _dbc.gOleDbConnection;
            adoCommand.Transaction = _dbc.gOleDbTransaction;

            strQuery = "INSERT INTO MWIPLOTSTS ("
                + " LOT_ID, LOT_DESC, FACTORY, MAT_ID, MAT_VER,"
                + " FLOW, FLOW_SEQ_NUM, OPER, QTY_1, QTY_2,"
                + " QTY_3, CRR_ID, LOT_TYPE, OWNER_CODE, CREATE_CODE,"
                + " LOT_PRIORITY, LOT_STATUS, HOLD_FLAG, HOLD_CODE, HOLD_PASSWORD,"
                + " HOLD_PRV_GRP_ID, OPER_IN_QTY_1, OPER_IN_QTY_2, OPER_IN_QTY_3, CREATE_QTY_1,"
                + " CREATE_QTY_2, CREATE_QTY_3, START_QTY_1, START_QTY_2, START_QTY_3,"
                + " INV_FLAG, TRANSIT_FLAG, UNIT_EXIST_FLAG, INV_UNIT, RWK_FLAG,"
                + " RWK_CODE, RWK_COUNT, RWK_RET_FLOW, RWK_RET_FLOW_SEQ_NUM, RWK_RET_OPER,"
                + " RWK_END_FLOW, RWK_END_FLOW_SEQ_NUM, RWK_END_OPER, RWK_RET_CLEAR_FLAG, RWK_TIME,"
                + " NSTD_FLAG, NSTD_RET_FLOW, NSTD_RET_FLOW_SEQ_NUM, NSTD_RET_OPER, NSTD_TIME,"
                + " REP_FLAG, REP_RET_OPER, START_FLAG, START_TIME, START_RES_ID,"
                + " END_FLAG, END_TIME, END_RES_ID, SAMPLE_FLAG, SAMPLE_WAIT_FLAG,"
                + " SAMPLE_RESULT, FROM_TO_FLAG, FROM_TO_LOT_ID, SHIP_CODE, SHIP_TIME,"
                + " ORG_DUE_TIME, SCH_DUE_TIME, CREATE_TIME, FAC_IN_TIME, FLOW_IN_TIME,"
                + " OPER_IN_TIME, RESERVE_RES_ID, BATCH_ID, BATCH_SEQ, ORDER_ID,"
                + " ADD_ORDER_ID_1, ADD_ORDER_ID_2, ADD_ORDER_ID_3, LOT_LOCATION, LOT_CMF_1,"
                + " LOT_CMF_2, LOT_CMF_3, LOT_CMF_4, LOT_CMF_5, LOT_CMF_6,"
                + " LOT_CMF_7, LOT_CMF_8, LOT_CMF_9, LOT_CMF_10, LOT_CMF_11,"
                + " LOT_CMF_12, LOT_CMF_13, LOT_CMF_14, LOT_CMF_15, LOT_CMF_16,"
                + " LOT_CMF_17, LOT_CMF_18, LOT_CMF_19, LOT_CMF_20, LOT_DEL_FLAG,"
                + " LOT_DEL_CODE, LOT_DEL_TIME, BOM_SET_ID, BOM_SET_VERSION, BOM_ACTIVE_HIST_SEQ,"
                + " BOM_HIST_SEQ, LAST_TRAN_CODE, LAST_TRAN_TIME, LAST_COMMENT, LAST_ACTIVE_HIST_SEQ,"
                + " LAST_HIST_SEQ, CRITICAL_RES_ID, CRITICAL_RES_GROUP_ID, SAVE_RES_ID_1, SAVE_RES_ID_2,"
                + " SUBRES_ID, LOT_GROUP_ID_1, LOT_GROUP_ID_2, LOT_GROUP_ID_3, RESV_FIELD_1,"
                + " RESV_FIELD_2, RESV_FIELD_3, RESV_FIELD_4, RESV_FIELD_5, RESV_FLAG_1,"
                + " RESV_FLAG_2, RESV_FLAG_3, RESV_FLAG_4, RESV_FLAG_5)"
                + " VALUES ("
                + " ?, ?, ?, ?, ?, ?, ?, ?, ?, ?,"
                + " ?, ?, ?, ?, ?, ?, ?, ?, ?, ?,"
                + " ?, ?, ?, ?, ?, ?, ?, ?, ?, ?,"
                + " ?, ?, ?, ?, ?, ?, ?, ?, ?, ?,"
                + " ?, ?, ?, ?, ?, ?, ?, ?, ?, ?,"
                + " ?, ?, ?, ?, ?, ?, ?, ?, ?, ?,"
                + " ?, ?, ?, ?, ?, ?, ?, ?, ?, ?,"
                + " ?, ?, ?, ?, ?, ?, ?, ?, ?, ?,"
                + " ?, ?, ?, ?, ?, ?, ?, ?, ?, ?,"
                + " ?, ?, ?, ?, ?, ?, ?, ?, ?, ?,"
                + " ?, ?, ?, ?, ?, ?, ?, ?, ?, ?,"
                + " ?, ?, ?, ?, ?, ?, ?, ?, ?, ?,"
                + " ?, ?, ?, ?, ?, ?, ?, ?, ?)";

            // Add Parameters
            adoCommand.Parameters.Add("@LOT_ID", OleDbType.VarChar).Value = LOT_ID;
            adoCommand.Parameters.Add("@LOT_DESC", OleDbType.VarChar).Value = LOT_DESC;
            adoCommand.Parameters.Add("@FACTORY", OleDbType.VarChar).Value = FACTORY;
            adoCommand.Parameters.Add("@MAT_ID", OleDbType.VarChar).Value = MAT_ID;
            adoCommand.Parameters.Add("@MAT_VER", OleDbType.Numeric).Value = MAT_VER;
            adoCommand.Parameters.Add("@FLOW", OleDbType.VarChar).Value = FLOW;
            adoCommand.Parameters.Add("@FLOW_SEQ_NUM", OleDbType.Numeric).Value = FLOW_SEQ_NUM;
            adoCommand.Parameters.Add("@OPER", OleDbType.VarChar).Value = OPER;
            adoCommand.Parameters.Add("@QTY_1", OleDbType.Numeric).Value = QTY_1;
            adoCommand.Parameters.Add("@QTY_2", OleDbType.Numeric).Value = QTY_2;
            adoCommand.Parameters.Add("@QTY_3", OleDbType.Numeric).Value = QTY_3;
            adoCommand.Parameters.Add("@CRR_ID", OleDbType.VarChar).Value = CRR_ID;
            adoCommand.Parameters.Add("@LOT_TYPE", OleDbType.VarChar).Value = LOT_TYPE;
            adoCommand.Parameters.Add("@OWNER_CODE", OleDbType.VarChar).Value = OWNER_CODE;
            adoCommand.Parameters.Add("@CREATE_CODE", OleDbType.VarChar).Value = CREATE_CODE;
            adoCommand.Parameters.Add("@LOT_PRIORITY", OleDbType.VarChar).Value = LOT_PRIORITY;
            adoCommand.Parameters.Add("@LOT_STATUS", OleDbType.VarChar).Value = LOT_STATUS;
            adoCommand.Parameters.Add("@HOLD_FLAG", OleDbType.VarChar).Value = HOLD_FLAG;
            adoCommand.Parameters.Add("@HOLD_CODE", OleDbType.VarChar).Value = HOLD_CODE;
            adoCommand.Parameters.Add("@HOLD_PASSWORD", OleDbType.VarChar).Value = HOLD_PASSWORD;
            adoCommand.Parameters.Add("@HOLD_PRV_GRP_ID", OleDbType.VarChar).Value = HOLD_PRV_GRP_ID;
            adoCommand.Parameters.Add("@OPER_IN_QTY_1", OleDbType.Numeric).Value = OPER_IN_QTY_1;
            adoCommand.Parameters.Add("@OPER_IN_QTY_2", OleDbType.Numeric).Value = OPER_IN_QTY_2;
            adoCommand.Parameters.Add("@OPER_IN_QTY_3", OleDbType.Numeric).Value = OPER_IN_QTY_3;
            adoCommand.Parameters.Add("@CREATE_QTY_1", OleDbType.Numeric).Value = CREATE_QTY_1;
            adoCommand.Parameters.Add("@CREATE_QTY_2", OleDbType.Numeric).Value = CREATE_QTY_2;
            adoCommand.Parameters.Add("@CREATE_QTY_3", OleDbType.Numeric).Value = CREATE_QTY_3;
            adoCommand.Parameters.Add("@START_QTY_1", OleDbType.Numeric).Value = START_QTY_1;
            adoCommand.Parameters.Add("@START_QTY_2", OleDbType.Numeric).Value = START_QTY_2;
            adoCommand.Parameters.Add("@START_QTY_3", OleDbType.Numeric).Value = START_QTY_3;
            adoCommand.Parameters.Add("@INV_FLAG", OleDbType.VarChar).Value = INV_FLAG;
            adoCommand.Parameters.Add("@TRANSIT_FLAG", OleDbType.VarChar).Value = TRANSIT_FLAG;
            adoCommand.Parameters.Add("@UNIT_EXIST_FLAG", OleDbType.VarChar).Value = UNIT_EXIST_FLAG;
            adoCommand.Parameters.Add("@INV_UNIT", OleDbType.VarChar).Value = INV_UNIT;
            adoCommand.Parameters.Add("@RWK_FLAG", OleDbType.VarChar).Value = RWK_FLAG;
            adoCommand.Parameters.Add("@RWK_CODE", OleDbType.VarChar).Value = RWK_CODE;
            adoCommand.Parameters.Add("@RWK_COUNT", OleDbType.Numeric).Value = RWK_COUNT;
            adoCommand.Parameters.Add("@RWK_RET_FLOW", OleDbType.VarChar).Value = RWK_RET_FLOW;
            adoCommand.Parameters.Add("@RWK_RET_FLOW_SEQ_NUM", OleDbType.Numeric).Value = RWK_RET_FLOW_SEQ_NUM;
            adoCommand.Parameters.Add("@RWK_RET_OPER", OleDbType.VarChar).Value = RWK_RET_OPER;
            adoCommand.Parameters.Add("@RWK_END_FLOW", OleDbType.VarChar).Value = RWK_END_FLOW;
            adoCommand.Parameters.Add("@RWK_END_FLOW_SEQ_NUM", OleDbType.Numeric).Value = RWK_END_FLOW_SEQ_NUM;
            adoCommand.Parameters.Add("@RWK_END_OPER", OleDbType.VarChar).Value = RWK_END_OPER;
            adoCommand.Parameters.Add("@RWK_RET_CLEAR_FLAG", OleDbType.VarChar).Value = RWK_RET_CLEAR_FLAG;
            adoCommand.Parameters.Add("@RWK_TIME", OleDbType.VarChar).Value = RWK_TIME;
            adoCommand.Parameters.Add("@NSTD_FLAG", OleDbType.VarChar).Value = NSTD_FLAG;
            adoCommand.Parameters.Add("@NSTD_RET_FLOW", OleDbType.VarChar).Value = NSTD_RET_FLOW;
            adoCommand.Parameters.Add("@NSTD_RET_FLOW_SEQ_NUM", OleDbType.Numeric).Value = NSTD_RET_FLOW_SEQ_NUM;
            adoCommand.Parameters.Add("@NSTD_RET_OPER", OleDbType.VarChar).Value = NSTD_RET_OPER;
            adoCommand.Parameters.Add("@NSTD_TIME", OleDbType.VarChar).Value = NSTD_TIME;
            adoCommand.Parameters.Add("@REP_FLAG", OleDbType.VarChar).Value = REP_FLAG;
            adoCommand.Parameters.Add("@REP_RET_OPER", OleDbType.VarChar).Value = REP_RET_OPER;
            adoCommand.Parameters.Add("@START_FLAG", OleDbType.VarChar).Value = START_FLAG;
            adoCommand.Parameters.Add("@START_TIME", OleDbType.VarChar).Value = START_TIME;
            adoCommand.Parameters.Add("@START_RES_ID", OleDbType.VarChar).Value = START_RES_ID;
            adoCommand.Parameters.Add("@END_FLAG", OleDbType.VarChar).Value = END_FLAG;
            adoCommand.Parameters.Add("@END_TIME", OleDbType.VarChar).Value = END_TIME;
            adoCommand.Parameters.Add("@END_RES_ID", OleDbType.VarChar).Value = END_RES_ID;
            adoCommand.Parameters.Add("@SAMPLE_FLAG", OleDbType.VarChar).Value = SAMPLE_FLAG;
            adoCommand.Parameters.Add("@SAMPLE_WAIT_FLAG", OleDbType.VarChar).Value = SAMPLE_WAIT_FLAG;
            adoCommand.Parameters.Add("@SAMPLE_RESULT", OleDbType.VarChar).Value = SAMPLE_RESULT;
            adoCommand.Parameters.Add("@FROM_TO_FLAG", OleDbType.VarChar).Value = FROM_TO_FLAG;
            adoCommand.Parameters.Add("@FROM_TO_LOT_ID", OleDbType.VarChar).Value = FROM_TO_LOT_ID;
            adoCommand.Parameters.Add("@SHIP_CODE", OleDbType.VarChar).Value = SHIP_CODE;
            adoCommand.Parameters.Add("@SHIP_TIME", OleDbType.VarChar).Value = SHIP_TIME;
            adoCommand.Parameters.Add("@ORG_DUE_TIME", OleDbType.VarChar).Value = ORG_DUE_TIME;
            adoCommand.Parameters.Add("@SCH_DUE_TIME", OleDbType.VarChar).Value = SCH_DUE_TIME;
            adoCommand.Parameters.Add("@CREATE_TIME", OleDbType.VarChar).Value = CREATE_TIME;
            adoCommand.Parameters.Add("@FAC_IN_TIME", OleDbType.VarChar).Value = FAC_IN_TIME;
            adoCommand.Parameters.Add("@FLOW_IN_TIME", OleDbType.VarChar).Value = FLOW_IN_TIME;
            adoCommand.Parameters.Add("@OPER_IN_TIME", OleDbType.VarChar).Value = OPER_IN_TIME;
            adoCommand.Parameters.Add("@RESERVE_RES_ID", OleDbType.VarChar).Value = RESERVE_RES_ID;
            adoCommand.Parameters.Add("@BATCH_ID", OleDbType.VarChar).Value = BATCH_ID;
            adoCommand.Parameters.Add("@BATCH_SEQ", OleDbType.Numeric).Value = BATCH_SEQ;
            adoCommand.Parameters.Add("@ORDER_ID", OleDbType.VarChar).Value = ORDER_ID;
            adoCommand.Parameters.Add("@ADD_ORDER_ID_1", OleDbType.VarChar).Value = ADD_ORDER_ID_1;
            adoCommand.Parameters.Add("@ADD_ORDER_ID_2", OleDbType.VarChar).Value = ADD_ORDER_ID_2;
            adoCommand.Parameters.Add("@ADD_ORDER_ID_3", OleDbType.VarChar).Value = ADD_ORDER_ID_3;
            adoCommand.Parameters.Add("@LOT_LOCATION", OleDbType.VarChar).Value = LOT_LOCATION;
            adoCommand.Parameters.Add("@LOT_CMF_1", OleDbType.VarChar).Value = LOT_CMF_1;
            adoCommand.Parameters.Add("@LOT_CMF_2", OleDbType.VarChar).Value = LOT_CMF_2;
            adoCommand.Parameters.Add("@LOT_CMF_3", OleDbType.VarChar).Value = LOT_CMF_3;
            adoCommand.Parameters.Add("@LOT_CMF_4", OleDbType.VarChar).Value = LOT_CMF_4;
            adoCommand.Parameters.Add("@LOT_CMF_5", OleDbType.VarChar).Value = LOT_CMF_5;
            adoCommand.Parameters.Add("@LOT_CMF_6", OleDbType.VarChar).Value = LOT_CMF_6;
            adoCommand.Parameters.Add("@LOT_CMF_7", OleDbType.VarChar).Value = LOT_CMF_7;
            adoCommand.Parameters.Add("@LOT_CMF_8", OleDbType.VarChar).Value = LOT_CMF_8;
            adoCommand.Parameters.Add("@LOT_CMF_9", OleDbType.VarChar).Value = LOT_CMF_9;
            adoCommand.Parameters.Add("@LOT_CMF_10", OleDbType.VarChar).Value = LOT_CMF_10;
            adoCommand.Parameters.Add("@LOT_CMF_11", OleDbType.VarChar).Value = LOT_CMF_11;
            adoCommand.Parameters.Add("@LOT_CMF_12", OleDbType.VarChar).Value = LOT_CMF_12;
            adoCommand.Parameters.Add("@LOT_CMF_13", OleDbType.VarChar).Value = LOT_CMF_13;
            adoCommand.Parameters.Add("@LOT_CMF_14", OleDbType.VarChar).Value = LOT_CMF_14;
            adoCommand.Parameters.Add("@LOT_CMF_15", OleDbType.VarChar).Value = LOT_CMF_15;
            adoCommand.Parameters.Add("@LOT_CMF_16", OleDbType.VarChar).Value = LOT_CMF_16;
            adoCommand.Parameters.Add("@LOT_CMF_17", OleDbType.VarChar).Value = LOT_CMF_17;
            adoCommand.Parameters.Add("@LOT_CMF_18", OleDbType.VarChar).Value = LOT_CMF_18;
            adoCommand.Parameters.Add("@LOT_CMF_19", OleDbType.VarChar).Value = LOT_CMF_19;
            adoCommand.Parameters.Add("@LOT_CMF_20", OleDbType.VarChar).Value = LOT_CMF_20;
            adoCommand.Parameters.Add("@LOT_DEL_FLAG", OleDbType.VarChar).Value = LOT_DEL_FLAG;
            adoCommand.Parameters.Add("@LOT_DEL_CODE", OleDbType.VarChar).Value = LOT_DEL_CODE;
            adoCommand.Parameters.Add("@LOT_DEL_TIME", OleDbType.VarChar).Value = LOT_DEL_TIME;
            adoCommand.Parameters.Add("@BOM_SET_ID", OleDbType.VarChar).Value = BOM_SET_ID;
            adoCommand.Parameters.Add("@BOM_SET_VERSION", OleDbType.Numeric).Value = BOM_SET_VERSION;
            adoCommand.Parameters.Add("@BOM_ACTIVE_HIST_SEQ", OleDbType.Numeric).Value = BOM_ACTIVE_HIST_SEQ;
            adoCommand.Parameters.Add("@BOM_HIST_SEQ", OleDbType.Numeric).Value = BOM_HIST_SEQ;
            adoCommand.Parameters.Add("@LAST_TRAN_CODE", OleDbType.VarChar).Value = LAST_TRAN_CODE;
            adoCommand.Parameters.Add("@LAST_TRAN_TIME", OleDbType.VarChar).Value = LAST_TRAN_TIME;
            adoCommand.Parameters.Add("@LAST_COMMENT", OleDbType.VarChar).Value = LAST_COMMENT;
            adoCommand.Parameters.Add("@LAST_ACTIVE_HIST_SEQ", OleDbType.Numeric).Value = LAST_ACTIVE_HIST_SEQ;
            adoCommand.Parameters.Add("@LAST_HIST_SEQ", OleDbType.Numeric).Value = LAST_HIST_SEQ;
            adoCommand.Parameters.Add("@CRITICAL_RES_ID", OleDbType.VarChar).Value = CRITICAL_RES_ID;
            adoCommand.Parameters.Add("@CRITICAL_RES_GROUP_ID", OleDbType.VarChar).Value = CRITICAL_RES_GROUP_ID;
            adoCommand.Parameters.Add("@SAVE_RES_ID_1", OleDbType.VarChar).Value = SAVE_RES_ID_1;
            adoCommand.Parameters.Add("@SAVE_RES_ID_2", OleDbType.VarChar).Value = SAVE_RES_ID_2;
            adoCommand.Parameters.Add("@SUBRES_ID", OleDbType.VarChar).Value = SUBRES_ID;
            adoCommand.Parameters.Add("@LOT_GROUP_ID_1", OleDbType.VarChar).Value = LOT_GROUP_ID_1;
            adoCommand.Parameters.Add("@LOT_GROUP_ID_2", OleDbType.VarChar).Value = LOT_GROUP_ID_2;
            adoCommand.Parameters.Add("@LOT_GROUP_ID_3", OleDbType.VarChar).Value = LOT_GROUP_ID_3;
            adoCommand.Parameters.Add("@RESV_FIELD_1", OleDbType.VarChar).Value = RESV_FIELD_1;
            adoCommand.Parameters.Add("@RESV_FIELD_2", OleDbType.VarChar).Value = RESV_FIELD_2;
            adoCommand.Parameters.Add("@RESV_FIELD_3", OleDbType.VarChar).Value = RESV_FIELD_3;
            adoCommand.Parameters.Add("@RESV_FIELD_4", OleDbType.VarChar).Value = RESV_FIELD_4;
            adoCommand.Parameters.Add("@RESV_FIELD_5", OleDbType.VarChar).Value = RESV_FIELD_5;
            adoCommand.Parameters.Add("@RESV_FLAG_1", OleDbType.VarChar).Value = RESV_FLAG_1;
            adoCommand.Parameters.Add("@RESV_FLAG_2", OleDbType.VarChar).Value = RESV_FLAG_2;
            adoCommand.Parameters.Add("@RESV_FLAG_3", OleDbType.VarChar).Value = RESV_FLAG_3;
            adoCommand.Parameters.Add("@RESV_FLAG_4", OleDbType.VarChar).Value = RESV_FLAG_4;
            adoCommand.Parameters.Add("@RESV_FLAG_5", OleDbType.VarChar).Value = RESV_FLAG_5;

            // ExecuteNonQuery()
            adoCommand.CommandText = strQuery;
            _dbc.gErrors.AddQuery(strQuery, adoCommand.Parameters);
            adoCommand.ExecuteNonQuery();

            // Dispose
            adoCommand.Dispose();

            // INFORMATION : Duplicate 에러는 Exception으로 처리됨.
        }
        catch(Exception ex)
        {
            if (ex.GetType().ToString() == "System.Data.OleDb.OleDbException")
                _dbc.gErrors.SetErrors(((OleDbException)ex).Errors);
            else
                _dbc.gErrors.SetErrors(ex);

            return false;
        }

        return true;
    }

    /// <summary>
    /// Update Data
    /// </summary>
    /// <param name="_step">Query Selector</param>
    /// <returns>true or false</returns>
    public bool UpdateData(int _step)
    {
        string strQuery = "";

        try
        {
            if (_dbc.gErrors.MsgInit() == false)                return false;
            OleDbCommand adoCommand = new OleDbCommand();

            // Set Connection, Transaction
            adoCommand.Connection = _dbc.gOleDbConnection;
            adoCommand.Transaction = _dbc.gOleDbTransaction;

            switch(_step)
            {
                case 1:
                    // Update by Primary Key
                    strQuery = "UPDATE MWIPLOTSTS SET"
                        + " LOT_DESC=?, FACTORY=?, MAT_ID=?, MAT_VER=?, FLOW=?,"
                        + " FLOW_SEQ_NUM=?, OPER=?, QTY_1=?, QTY_2=?, QTY_3=?,"
                        + " CRR_ID=?, LOT_TYPE=?, OWNER_CODE=?, CREATE_CODE=?, LOT_PRIORITY=?,"
                        + " LOT_STATUS=?, HOLD_FLAG=?, HOLD_CODE=?, HOLD_PASSWORD=?, HOLD_PRV_GRP_ID=?,"
                        + " OPER_IN_QTY_1=?, OPER_IN_QTY_2=?, OPER_IN_QTY_3=?, CREATE_QTY_1=?, CREATE_QTY_2=?,"
                        + " CREATE_QTY_3=?, START_QTY_1=?, START_QTY_2=?, START_QTY_3=?, INV_FLAG=?,"
                        + " TRANSIT_FLAG=?, UNIT_EXIST_FLAG=?, INV_UNIT=?, RWK_FLAG=?, RWK_CODE=?,"
                        + " RWK_COUNT=?, RWK_RET_FLOW=?, RWK_RET_FLOW_SEQ_NUM=?, RWK_RET_OPER=?, RWK_END_FLOW=?,"
                        + " RWK_END_FLOW_SEQ_NUM=?, RWK_END_OPER=?, RWK_RET_CLEAR_FLAG=?, RWK_TIME=?, NSTD_FLAG=?,"
                        + " NSTD_RET_FLOW=?, NSTD_RET_FLOW_SEQ_NUM=?, NSTD_RET_OPER=?, NSTD_TIME=?, REP_FLAG=?,"
                        + " REP_RET_OPER=?, START_FLAG=?, START_TIME=?, START_RES_ID=?, END_FLAG=?,"
                        + " END_TIME=?, END_RES_ID=?, SAMPLE_FLAG=?, SAMPLE_WAIT_FLAG=?, SAMPLE_RESULT=?,"
                        + " FROM_TO_FLAG=?, FROM_TO_LOT_ID=?, SHIP_CODE=?, SHIP_TIME=?, ORG_DUE_TIME=?,"
                        + " SCH_DUE_TIME=?, CREATE_TIME=?, FAC_IN_TIME=?, FLOW_IN_TIME=?, OPER_IN_TIME=?,"
                        + " RESERVE_RES_ID=?, BATCH_ID=?, BATCH_SEQ=?, ORDER_ID=?, ADD_ORDER_ID_1=?,"
                        + " ADD_ORDER_ID_2=?, ADD_ORDER_ID_3=?, LOT_LOCATION=?, LOT_CMF_1=?, LOT_CMF_2=?,"
                        + " LOT_CMF_3=?, LOT_CMF_4=?, LOT_CMF_5=?, LOT_CMF_6=?, LOT_CMF_7=?,"
                        + " LOT_CMF_8=?, LOT_CMF_9=?, LOT_CMF_10=?, LOT_CMF_11=?, LOT_CMF_12=?,"
                        + " LOT_CMF_13=?, LOT_CMF_14=?, LOT_CMF_15=?, LOT_CMF_16=?, LOT_CMF_17=?,"
                        + " LOT_CMF_18=?, LOT_CMF_19=?, LOT_CMF_20=?, LOT_DEL_FLAG=?, LOT_DEL_CODE=?,"
                        + " LOT_DEL_TIME=?, BOM_SET_ID=?, BOM_SET_VERSION=?, BOM_ACTIVE_HIST_SEQ=?, BOM_HIST_SEQ=?,"
                        + " LAST_TRAN_CODE=?, LAST_TRAN_TIME=?, LAST_COMMENT=?, LAST_ACTIVE_HIST_SEQ=?, LAST_HIST_SEQ=?,"
                        + " CRITICAL_RES_ID=?, CRITICAL_RES_GROUP_ID=?, SAVE_RES_ID_1=?, SAVE_RES_ID_2=?, SUBRES_ID=?,"
                        + " LOT_GROUP_ID_1=?, LOT_GROUP_ID_2=?, LOT_GROUP_ID_3=?, RESV_FIELD_1=?, RESV_FIELD_2=?,"
                        + " RESV_FIELD_3=?, RESV_FIELD_4=?, RESV_FIELD_5=?, RESV_FLAG_1=?, RESV_FLAG_2=?,"
                        + " RESV_FLAG_3=?, RESV_FLAG_4=?, RESV_FLAG_5=?"
                        + " WHERE LOT_ID=?";

                    // Add Parameters
                    adoCommand.Parameters.Add("@LOT_DESC", OleDbType.VarChar).Value = LOT_DESC;
                    adoCommand.Parameters.Add("@FACTORY", OleDbType.VarChar).Value = FACTORY;
                    adoCommand.Parameters.Add("@MAT_ID", OleDbType.VarChar).Value = MAT_ID;
                    adoCommand.Parameters.Add("@MAT_VER", OleDbType.Numeric).Value = MAT_VER;
                    adoCommand.Parameters.Add("@FLOW", OleDbType.VarChar).Value = FLOW;
                    adoCommand.Parameters.Add("@FLOW_SEQ_NUM", OleDbType.Numeric).Value = FLOW_SEQ_NUM;
                    adoCommand.Parameters.Add("@OPER", OleDbType.VarChar).Value = OPER;
                    adoCommand.Parameters.Add("@QTY_1", OleDbType.Numeric).Value = QTY_1;
                    adoCommand.Parameters.Add("@QTY_2", OleDbType.Numeric).Value = QTY_2;
                    adoCommand.Parameters.Add("@QTY_3", OleDbType.Numeric).Value = QTY_3;
                    adoCommand.Parameters.Add("@CRR_ID", OleDbType.VarChar).Value = CRR_ID;
                    adoCommand.Parameters.Add("@LOT_TYPE", OleDbType.VarChar).Value = LOT_TYPE;
                    adoCommand.Parameters.Add("@OWNER_CODE", OleDbType.VarChar).Value = OWNER_CODE;
                    adoCommand.Parameters.Add("@CREATE_CODE", OleDbType.VarChar).Value = CREATE_CODE;
                    adoCommand.Parameters.Add("@LOT_PRIORITY", OleDbType.VarChar).Value = LOT_PRIORITY;
                    adoCommand.Parameters.Add("@LOT_STATUS", OleDbType.VarChar).Value = LOT_STATUS;
                    adoCommand.Parameters.Add("@HOLD_FLAG", OleDbType.VarChar).Value = HOLD_FLAG;
                    adoCommand.Parameters.Add("@HOLD_CODE", OleDbType.VarChar).Value = HOLD_CODE;
                    adoCommand.Parameters.Add("@HOLD_PASSWORD", OleDbType.VarChar).Value = HOLD_PASSWORD;
                    adoCommand.Parameters.Add("@HOLD_PRV_GRP_ID", OleDbType.VarChar).Value = HOLD_PRV_GRP_ID;
                    adoCommand.Parameters.Add("@OPER_IN_QTY_1", OleDbType.Numeric).Value = OPER_IN_QTY_1;
                    adoCommand.Parameters.Add("@OPER_IN_QTY_2", OleDbType.Numeric).Value = OPER_IN_QTY_2;
                    adoCommand.Parameters.Add("@OPER_IN_QTY_3", OleDbType.Numeric).Value = OPER_IN_QTY_3;
                    adoCommand.Parameters.Add("@CREATE_QTY_1", OleDbType.Numeric).Value = CREATE_QTY_1;
                    adoCommand.Parameters.Add("@CREATE_QTY_2", OleDbType.Numeric).Value = CREATE_QTY_2;
                    adoCommand.Parameters.Add("@CREATE_QTY_3", OleDbType.Numeric).Value = CREATE_QTY_3;
                    adoCommand.Parameters.Add("@START_QTY_1", OleDbType.Numeric).Value = START_QTY_1;
                    adoCommand.Parameters.Add("@START_QTY_2", OleDbType.Numeric).Value = START_QTY_2;
                    adoCommand.Parameters.Add("@START_QTY_3", OleDbType.Numeric).Value = START_QTY_3;
                    adoCommand.Parameters.Add("@INV_FLAG", OleDbType.VarChar).Value = INV_FLAG;
                    adoCommand.Parameters.Add("@TRANSIT_FLAG", OleDbType.VarChar).Value = TRANSIT_FLAG;
                    adoCommand.Parameters.Add("@UNIT_EXIST_FLAG", OleDbType.VarChar).Value = UNIT_EXIST_FLAG;
                    adoCommand.Parameters.Add("@INV_UNIT", OleDbType.VarChar).Value = INV_UNIT;
                    adoCommand.Parameters.Add("@RWK_FLAG", OleDbType.VarChar).Value = RWK_FLAG;
                    adoCommand.Parameters.Add("@RWK_CODE", OleDbType.VarChar).Value = RWK_CODE;
                    adoCommand.Parameters.Add("@RWK_COUNT", OleDbType.Numeric).Value = RWK_COUNT;
                    adoCommand.Parameters.Add("@RWK_RET_FLOW", OleDbType.VarChar).Value = RWK_RET_FLOW;
                    adoCommand.Parameters.Add("@RWK_RET_FLOW_SEQ_NUM", OleDbType.Numeric).Value = RWK_RET_FLOW_SEQ_NUM;
                    adoCommand.Parameters.Add("@RWK_RET_OPER", OleDbType.VarChar).Value = RWK_RET_OPER;
                    adoCommand.Parameters.Add("@RWK_END_FLOW", OleDbType.VarChar).Value = RWK_END_FLOW;
                    adoCommand.Parameters.Add("@RWK_END_FLOW_SEQ_NUM", OleDbType.Numeric).Value = RWK_END_FLOW_SEQ_NUM;
                    adoCommand.Parameters.Add("@RWK_END_OPER", OleDbType.VarChar).Value = RWK_END_OPER;
                    adoCommand.Parameters.Add("@RWK_RET_CLEAR_FLAG", OleDbType.VarChar).Value = RWK_RET_CLEAR_FLAG;
                    adoCommand.Parameters.Add("@RWK_TIME", OleDbType.VarChar).Value = RWK_TIME;
                    adoCommand.Parameters.Add("@NSTD_FLAG", OleDbType.VarChar).Value = NSTD_FLAG;
                    adoCommand.Parameters.Add("@NSTD_RET_FLOW", OleDbType.VarChar).Value = NSTD_RET_FLOW;
                    adoCommand.Parameters.Add("@NSTD_RET_FLOW_SEQ_NUM", OleDbType.Numeric).Value = NSTD_RET_FLOW_SEQ_NUM;
                    adoCommand.Parameters.Add("@NSTD_RET_OPER", OleDbType.VarChar).Value = NSTD_RET_OPER;
                    adoCommand.Parameters.Add("@NSTD_TIME", OleDbType.VarChar).Value = NSTD_TIME;
                    adoCommand.Parameters.Add("@REP_FLAG", OleDbType.VarChar).Value = REP_FLAG;
                    adoCommand.Parameters.Add("@REP_RET_OPER", OleDbType.VarChar).Value = REP_RET_OPER;
                    adoCommand.Parameters.Add("@START_FLAG", OleDbType.VarChar).Value = START_FLAG;
                    adoCommand.Parameters.Add("@START_TIME", OleDbType.VarChar).Value = START_TIME;
                    adoCommand.Parameters.Add("@START_RES_ID", OleDbType.VarChar).Value = START_RES_ID;
                    adoCommand.Parameters.Add("@END_FLAG", OleDbType.VarChar).Value = END_FLAG;
                    adoCommand.Parameters.Add("@END_TIME", OleDbType.VarChar).Value = END_TIME;
                    adoCommand.Parameters.Add("@END_RES_ID", OleDbType.VarChar).Value = END_RES_ID;
                    adoCommand.Parameters.Add("@SAMPLE_FLAG", OleDbType.VarChar).Value = SAMPLE_FLAG;
                    adoCommand.Parameters.Add("@SAMPLE_WAIT_FLAG", OleDbType.VarChar).Value = SAMPLE_WAIT_FLAG;
                    adoCommand.Parameters.Add("@SAMPLE_RESULT", OleDbType.VarChar).Value = SAMPLE_RESULT;
                    adoCommand.Parameters.Add("@FROM_TO_FLAG", OleDbType.VarChar).Value = FROM_TO_FLAG;
                    adoCommand.Parameters.Add("@FROM_TO_LOT_ID", OleDbType.VarChar).Value = FROM_TO_LOT_ID;
                    adoCommand.Parameters.Add("@SHIP_CODE", OleDbType.VarChar).Value = SHIP_CODE;
                    adoCommand.Parameters.Add("@SHIP_TIME", OleDbType.VarChar).Value = SHIP_TIME;
                    adoCommand.Parameters.Add("@ORG_DUE_TIME", OleDbType.VarChar).Value = ORG_DUE_TIME;
                    adoCommand.Parameters.Add("@SCH_DUE_TIME", OleDbType.VarChar).Value = SCH_DUE_TIME;
                    adoCommand.Parameters.Add("@CREATE_TIME", OleDbType.VarChar).Value = CREATE_TIME;
                    adoCommand.Parameters.Add("@FAC_IN_TIME", OleDbType.VarChar).Value = FAC_IN_TIME;
                    adoCommand.Parameters.Add("@FLOW_IN_TIME", OleDbType.VarChar).Value = FLOW_IN_TIME;
                    adoCommand.Parameters.Add("@OPER_IN_TIME", OleDbType.VarChar).Value = OPER_IN_TIME;
                    adoCommand.Parameters.Add("@RESERVE_RES_ID", OleDbType.VarChar).Value = RESERVE_RES_ID;
                    adoCommand.Parameters.Add("@BATCH_ID", OleDbType.VarChar).Value = BATCH_ID;
                    adoCommand.Parameters.Add("@BATCH_SEQ", OleDbType.Numeric).Value = BATCH_SEQ;
                    adoCommand.Parameters.Add("@ORDER_ID", OleDbType.VarChar).Value = ORDER_ID;
                    adoCommand.Parameters.Add("@ADD_ORDER_ID_1", OleDbType.VarChar).Value = ADD_ORDER_ID_1;
                    adoCommand.Parameters.Add("@ADD_ORDER_ID_2", OleDbType.VarChar).Value = ADD_ORDER_ID_2;
                    adoCommand.Parameters.Add("@ADD_ORDER_ID_3", OleDbType.VarChar).Value = ADD_ORDER_ID_3;
                    adoCommand.Parameters.Add("@LOT_LOCATION", OleDbType.VarChar).Value = LOT_LOCATION;
                    adoCommand.Parameters.Add("@LOT_CMF_1", OleDbType.VarChar).Value = LOT_CMF_1;
                    adoCommand.Parameters.Add("@LOT_CMF_2", OleDbType.VarChar).Value = LOT_CMF_2;
                    adoCommand.Parameters.Add("@LOT_CMF_3", OleDbType.VarChar).Value = LOT_CMF_3;
                    adoCommand.Parameters.Add("@LOT_CMF_4", OleDbType.VarChar).Value = LOT_CMF_4;
                    adoCommand.Parameters.Add("@LOT_CMF_5", OleDbType.VarChar).Value = LOT_CMF_5;
                    adoCommand.Parameters.Add("@LOT_CMF_6", OleDbType.VarChar).Value = LOT_CMF_6;
                    adoCommand.Parameters.Add("@LOT_CMF_7", OleDbType.VarChar).Value = LOT_CMF_7;
                    adoCommand.Parameters.Add("@LOT_CMF_8", OleDbType.VarChar).Value = LOT_CMF_8;
                    adoCommand.Parameters.Add("@LOT_CMF_9", OleDbType.VarChar).Value = LOT_CMF_9;
                    adoCommand.Parameters.Add("@LOT_CMF_10", OleDbType.VarChar).Value = LOT_CMF_10;
                    adoCommand.Parameters.Add("@LOT_CMF_11", OleDbType.VarChar).Value = LOT_CMF_11;
                    adoCommand.Parameters.Add("@LOT_CMF_12", OleDbType.VarChar).Value = LOT_CMF_12;
                    adoCommand.Parameters.Add("@LOT_CMF_13", OleDbType.VarChar).Value = LOT_CMF_13;
                    adoCommand.Parameters.Add("@LOT_CMF_14", OleDbType.VarChar).Value = LOT_CMF_14;
                    adoCommand.Parameters.Add("@LOT_CMF_15", OleDbType.VarChar).Value = LOT_CMF_15;
                    adoCommand.Parameters.Add("@LOT_CMF_16", OleDbType.VarChar).Value = LOT_CMF_16;
                    adoCommand.Parameters.Add("@LOT_CMF_17", OleDbType.VarChar).Value = LOT_CMF_17;
                    adoCommand.Parameters.Add("@LOT_CMF_18", OleDbType.VarChar).Value = LOT_CMF_18;
                    adoCommand.Parameters.Add("@LOT_CMF_19", OleDbType.VarChar).Value = LOT_CMF_19;
                    adoCommand.Parameters.Add("@LOT_CMF_20", OleDbType.VarChar).Value = LOT_CMF_20;
                    adoCommand.Parameters.Add("@LOT_DEL_FLAG", OleDbType.VarChar).Value = LOT_DEL_FLAG;
                    adoCommand.Parameters.Add("@LOT_DEL_CODE", OleDbType.VarChar).Value = LOT_DEL_CODE;
                    adoCommand.Parameters.Add("@LOT_DEL_TIME", OleDbType.VarChar).Value = LOT_DEL_TIME;
                    adoCommand.Parameters.Add("@BOM_SET_ID", OleDbType.VarChar).Value = BOM_SET_ID;
                    adoCommand.Parameters.Add("@BOM_SET_VERSION", OleDbType.Numeric).Value = BOM_SET_VERSION;
                    adoCommand.Parameters.Add("@BOM_ACTIVE_HIST_SEQ", OleDbType.Numeric).Value = BOM_ACTIVE_HIST_SEQ;
                    adoCommand.Parameters.Add("@BOM_HIST_SEQ", OleDbType.Numeric).Value = BOM_HIST_SEQ;
                    adoCommand.Parameters.Add("@LAST_TRAN_CODE", OleDbType.VarChar).Value = LAST_TRAN_CODE;
                    adoCommand.Parameters.Add("@LAST_TRAN_TIME", OleDbType.VarChar).Value = LAST_TRAN_TIME;
                    adoCommand.Parameters.Add("@LAST_COMMENT", OleDbType.VarChar).Value = LAST_COMMENT;
                    adoCommand.Parameters.Add("@LAST_ACTIVE_HIST_SEQ", OleDbType.Numeric).Value = LAST_ACTIVE_HIST_SEQ;
                    adoCommand.Parameters.Add("@LAST_HIST_SEQ", OleDbType.Numeric).Value = LAST_HIST_SEQ;
                    adoCommand.Parameters.Add("@CRITICAL_RES_ID", OleDbType.VarChar).Value = CRITICAL_RES_ID;
                    adoCommand.Parameters.Add("@CRITICAL_RES_GROUP_ID", OleDbType.VarChar).Value = CRITICAL_RES_GROUP_ID;
                    adoCommand.Parameters.Add("@SAVE_RES_ID_1", OleDbType.VarChar).Value = SAVE_RES_ID_1;
                    adoCommand.Parameters.Add("@SAVE_RES_ID_2", OleDbType.VarChar).Value = SAVE_RES_ID_2;
                    adoCommand.Parameters.Add("@SUBRES_ID", OleDbType.VarChar).Value = SUBRES_ID;
                    adoCommand.Parameters.Add("@LOT_GROUP_ID_1", OleDbType.VarChar).Value = LOT_GROUP_ID_1;
                    adoCommand.Parameters.Add("@LOT_GROUP_ID_2", OleDbType.VarChar).Value = LOT_GROUP_ID_2;
                    adoCommand.Parameters.Add("@LOT_GROUP_ID_3", OleDbType.VarChar).Value = LOT_GROUP_ID_3;
                    adoCommand.Parameters.Add("@RESV_FIELD_1", OleDbType.VarChar).Value = RESV_FIELD_1;
                    adoCommand.Parameters.Add("@RESV_FIELD_2", OleDbType.VarChar).Value = RESV_FIELD_2;
                    adoCommand.Parameters.Add("@RESV_FIELD_3", OleDbType.VarChar).Value = RESV_FIELD_3;
                    adoCommand.Parameters.Add("@RESV_FIELD_4", OleDbType.VarChar).Value = RESV_FIELD_4;
                    adoCommand.Parameters.Add("@RESV_FIELD_5", OleDbType.VarChar).Value = RESV_FIELD_5;
                    adoCommand.Parameters.Add("@RESV_FLAG_1", OleDbType.VarChar).Value = RESV_FLAG_1;
                    adoCommand.Parameters.Add("@RESV_FLAG_2", OleDbType.VarChar).Value = RESV_FLAG_2;
                    adoCommand.Parameters.Add("@RESV_FLAG_3", OleDbType.VarChar).Value = RESV_FLAG_3;
                    adoCommand.Parameters.Add("@RESV_FLAG_4", OleDbType.VarChar).Value = RESV_FLAG_4;
                    adoCommand.Parameters.Add("@RESV_FLAG_5", OleDbType.VarChar).Value = RESV_FLAG_5;

                    // Add Parameters for Condition
                    adoCommand.Parameters.Add("@C_LOT_ID", OleDbType.VarChar).Value = LOT_ID;
                    break;

                case 101:
                    // TODO : User Select Query
                    break;

                default:
                    // Error Handling
                    _dbc.gErrors.SqlCode = SQL_CODE.SQL_CASE_ERROR;
                    return false;
            }

            // ExecuteNonQuery();
            adoCommand.CommandText = strQuery;
            _dbc.gErrors.AddQuery(strQuery, adoCommand.Parameters);
            if (adoCommand.ExecuteNonQuery() < 1)
            {
                // Data Not Found
                _dbc.gErrors.SqlCode = SQL_CODE.SQL_NOT_FOUND;
                // Dispose
                adoCommand.Dispose();
                return false;
            }

            // Dispose
            adoCommand.Dispose();
        }
        catch(Exception ex)
        {
            if (ex.GetType().ToString() == "System.Data.OleDb.OleDbException")
                _dbc.gErrors.SetErrors(((OleDbException)ex).Errors);
            else
                _dbc.gErrors.SetErrors(ex);

            return false;
        }

        return true;
    }
    /// <summary>
    /// Fill Data
    /// </summary>
    /// <param name="_step">Query Selector</param>
    /// <param name="adoDataTable">Output DataTable</param>
    /// <returns>true or false</returns>
    public bool FillData(int _step , ref DataTable adoDataTable)
    {
        string strQuery = "";

        try
        {
            if (_dbc.gErrors.MsgInit() == false)
                return false;
            OleDbDataAdapter adoAdapter = new OleDbDataAdapter();
            adoAdapter.SelectCommand = new OleDbCommand();

            // Set Connection, Transaction
            adoAdapter.SelectCommand.Connection = _dbc.gOleDbConnection;
            adoAdapter.SelectCommand.Transaction = _dbc.gOleDbTransaction;
            adoDataTable = new DataTable();

            switch(_step)
            {
                case 1:
                    // Select All Record
                    strQuery = "SELECT * FROM MWIPLOTSTS"
                        + " ORDER BY LOT_ID ASC";

                    break;

                case 101:
                    // TODO : User Select Query
                    break;

                default:
                    // Error Handling
                    _dbc.gErrors.SqlCode = SQL_CODE.SQL_CASE_ERROR;
                    return false;
            }

            // Fill()
            adoAdapter.SelectCommand.CommandText = strQuery;
            _dbc.gErrors.AddQuery(strQuery, adoAdapter.SelectCommand.Parameters);
            adoAdapter.Fill(adoDataTable);

            // Check Data Not Found
            if (adoDataTable.Rows.Count < 1)
            {
                _dbc.gErrors.SqlCode = SQL_CODE.SQL_NOT_FOUND;
                return false;
            }
        }
        catch(Exception ex)
        {
            if (ex.GetType().ToString() == "System.Data.OleDb.OleDbException")
                _dbc.gErrors.SetErrors(((OleDbException)ex).Errors);
            else
                _dbc.gErrors.SetErrors(ex);

            return false;
        }

        return true;
    }

#endregion

}

}

