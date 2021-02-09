//-----------------------------------------------------------------------------
//
//   System      : MES
//   File Name   : DBC_MRASRESDEF.cs
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
//       - 2008-05-29 : Created by DBLib Generator
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
public class DBC_MRASRESDEF
    : ICloneable
{


#region " Fields Definition "

    // Fields
    private string m_factory;                                // VARCHAR(10)
    private string m_res_id;                                 // VARCHAR(20)
    private string m_res_desc;                               // VARCHAR(50)
    private string m_res_type;                               // VARCHAR(20)
    private string m_res_grp_1;                              // VARCHAR(20)
    private string m_res_grp_2;                              // VARCHAR(20)
    private string m_res_grp_3;                              // VARCHAR(20)
    private string m_res_grp_4;                              // VARCHAR(20)
    private string m_res_grp_5;                              // VARCHAR(20)
    private string m_res_grp_6;                              // VARCHAR(20)
    private string m_res_grp_7;                              // VARCHAR(20)
    private string m_res_grp_8;                              // VARCHAR(20)
    private string m_res_grp_9;                              // VARCHAR(20)
    private string m_res_grp_10;                             // VARCHAR(20)
    private string m_use_fac_prt_flag;                       // VARCHAR(1)
    private string m_res_sts_prt_1;                          // VARCHAR(30)
    private string m_res_sts_prt_2;                          // VARCHAR(30)
    private string m_res_sts_prt_3;                          // VARCHAR(30)
    private string m_res_sts_prt_4;                          // VARCHAR(30)
    private string m_res_sts_prt_5;                          // VARCHAR(30)
    private string m_res_sts_prt_6;                          // VARCHAR(30)
    private string m_res_sts_prt_7;                          // VARCHAR(30)
    private string m_res_sts_prt_8;                          // VARCHAR(30)
    private string m_res_sts_prt_9;                          // VARCHAR(30)
    private string m_res_sts_prt_10;                         // VARCHAR(30)
    private string m_res_cmf_1;                              // VARCHAR(30)
    private string m_res_cmf_2;                              // VARCHAR(30)
    private string m_res_cmf_3;                              // VARCHAR(30)
    private string m_res_cmf_4;                              // VARCHAR(30)
    private string m_res_cmf_5;                              // VARCHAR(30)
    private string m_res_cmf_6;                              // VARCHAR(30)
    private string m_res_cmf_7;                              // VARCHAR(30)
    private string m_res_cmf_8;                              // VARCHAR(30)
    private string m_res_cmf_9;                              // VARCHAR(30)
    private string m_res_cmf_10;                             // VARCHAR(30)
    private string m_res_cmf_11;                             // VARCHAR(30)
    private string m_res_cmf_12;                             // VARCHAR(30)
    private string m_res_cmf_13;                             // VARCHAR(30)
    private string m_res_cmf_14;                             // VARCHAR(30)
    private string m_res_cmf_15;                             // VARCHAR(30)
    private string m_res_cmf_16;                             // VARCHAR(30)
    private string m_res_cmf_17;                             // VARCHAR(30)
    private string m_res_cmf_18;                             // VARCHAR(30)
    private string m_res_cmf_19;                             // VARCHAR(30)
    private string m_res_cmf_20;                             // VARCHAR(30)
    private string m_area_id;                                // VARCHAR(20)
    private string m_sub_area_id;                            // VARCHAR(20)
    private string m_res_location;                           // VARCHAR(20)
    private string m_proc_rule;                              // VARCHAR(1)
    private int m_max_proc_count;                            // INTEGER(3)
    private string m_batch_cond_1;                           // VARCHAR(12)
    private string m_batch_cond_2;                           // VARCHAR(12)
    private string m_pm_sch_enable_flag;                     // VARCHAR(1)
    private string m_unit_base_st_flag;                      // VARCHAR(1)
    private string m_sec_chk_flag;                           // VARCHAR(1)
    private string m_gather_alarm_flag;                      // VARCHAR(1)
    private string m_delete_flag;                            // VARCHAR(1)
    private string m_delete_user_id;                         // VARCHAR(20)
    private string m_delete_time;                            // VARCHAR(14)
    private string m_create_user_id;                         // VARCHAR(20)
    private string m_create_time;                            // VARCHAR(14)
    private string m_update_user_id;                         // VARCHAR(20)
    private string m_update_time;                            // VARCHAR(14)
    private string m_res_up_down_flag;                       // VARCHAR(1)
    private string m_res_pri_sts;                            // VARCHAR(30)
    private string m_res_sts_1;                              // VARCHAR(30)
    private string m_res_sts_2;                              // VARCHAR(30)
    private string m_res_sts_3;                              // VARCHAR(30)
    private string m_res_sts_4;                              // VARCHAR(30)
    private string m_res_sts_5;                              // VARCHAR(30)
    private string m_res_sts_6;                              // VARCHAR(30)
    private string m_res_sts_7;                              // VARCHAR(30)
    private string m_res_sts_8;                              // VARCHAR(30)
    private string m_res_sts_9;                              // VARCHAR(30)
    private string m_res_sts_10;                             // VARCHAR(30)
    private string m_lot_id;                                 // VARCHAR(25)
    private string m_sublot_id;                              // VARCHAR(30)
    private string m_crr_id;                                 // VARCHAR(20)
    private string m_res_ctrl_mode;                          // VARCHAR(2)
    private string m_res_proc_mode;                          // VARCHAR(10)
    private string m_last_recipe_id;                         // VARCHAR(30)
    private int m_proc_count;                                // INTEGER(3)
    private string m_last_start_time;                        // VARCHAR(14)
    private string m_last_end_time;                          // VARCHAR(14)
    private string m_last_down_time;                         // VARCHAR(14)
    private int m_last_down_hist_seq;                        // INTEGER(10)
    private string m_last_event_id;                          // VARCHAR(12)
    private string m_last_event_time;                        // VARCHAR(14)
    private int m_last_active_hist_seq;                      // INTEGER(10)
    private int m_last_hist_seq;                             // INTEGER(10)

#endregion

#region " Variable Definition "
    DB_Common _dbc;      //DB Connection
    // TODO : Declare Variable for Query Condition

#endregion

#region " Property Definition "

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
    /// Gets or sets the 'RES_ID' value.
    /// </summary>
    public string RES_ID
    {
        get
        {
            if (m_res_id == null )
            {
                m_res_id = " ";
            }
            return m_res_id;
        }
        set
        {
            if (value == null || value == "" )
            {
                m_res_id = " ";
            }
            m_res_id = value;
        }
    }

    /// <summary>
    /// Gets or sets the 'RES_DESC' value.
    /// </summary>
    public string RES_DESC
    {
        get
        {
            if (m_res_desc == null )
            {
                m_res_desc = " ";
            }
            return m_res_desc;
        }
        set
        {
            if (value == null || value == "" )
            {
                m_res_desc = " ";
            }
            m_res_desc = value;
        }
    }

    /// <summary>
    /// Gets or sets the 'RES_TYPE' value.
    /// </summary>
    public string RES_TYPE
    {
        get
        {
            if (m_res_type == null )
            {
                m_res_type = " ";
            }
            return m_res_type;
        }
        set
        {
            if (value == null || value == "" )
            {
                m_res_type = " ";
            }
            m_res_type = value;
        }
    }

    /// <summary>
    /// Gets or sets the 'RES_GRP_1' value.
    /// </summary>
    public string RES_GRP_1
    {
        get
        {
            if (m_res_grp_1 == null )
            {
                m_res_grp_1 = " ";
            }
            return m_res_grp_1;
        }
        set
        {
            if (value == null || value == "" )
            {
                m_res_grp_1 = " ";
            }
            m_res_grp_1 = value;
        }
    }

    /// <summary>
    /// Gets or sets the 'RES_GRP_2' value.
    /// </summary>
    public string RES_GRP_2
    {
        get
        {
            if (m_res_grp_2 == null )
            {
                m_res_grp_2 = " ";
            }
            return m_res_grp_2;
        }
        set
        {
            if (value == null || value == "" )
            {
                m_res_grp_2 = " ";
            }
            m_res_grp_2 = value;
        }
    }

    /// <summary>
    /// Gets or sets the 'RES_GRP_3' value.
    /// </summary>
    public string RES_GRP_3
    {
        get
        {
            if (m_res_grp_3 == null )
            {
                m_res_grp_3 = " ";
            }
            return m_res_grp_3;
        }
        set
        {
            if (value == null || value == "" )
            {
                m_res_grp_3 = " ";
            }
            m_res_grp_3 = value;
        }
    }

    /// <summary>
    /// Gets or sets the 'RES_GRP_4' value.
    /// </summary>
    public string RES_GRP_4
    {
        get
        {
            if (m_res_grp_4 == null )
            {
                m_res_grp_4 = " ";
            }
            return m_res_grp_4;
        }
        set
        {
            if (value == null || value == "" )
            {
                m_res_grp_4 = " ";
            }
            m_res_grp_4 = value;
        }
    }

    /// <summary>
    /// Gets or sets the 'RES_GRP_5' value.
    /// </summary>
    public string RES_GRP_5
    {
        get
        {
            if (m_res_grp_5 == null )
            {
                m_res_grp_5 = " ";
            }
            return m_res_grp_5;
        }
        set
        {
            if (value == null || value == "" )
            {
                m_res_grp_5 = " ";
            }
            m_res_grp_5 = value;
        }
    }

    /// <summary>
    /// Gets or sets the 'RES_GRP_6' value.
    /// </summary>
    public string RES_GRP_6
    {
        get
        {
            if (m_res_grp_6 == null )
            {
                m_res_grp_6 = " ";
            }
            return m_res_grp_6;
        }
        set
        {
            if (value == null || value == "" )
            {
                m_res_grp_6 = " ";
            }
            m_res_grp_6 = value;
        }
    }

    /// <summary>
    /// Gets or sets the 'RES_GRP_7' value.
    /// </summary>
    public string RES_GRP_7
    {
        get
        {
            if (m_res_grp_7 == null )
            {
                m_res_grp_7 = " ";
            }
            return m_res_grp_7;
        }
        set
        {
            if (value == null || value == "" )
            {
                m_res_grp_7 = " ";
            }
            m_res_grp_7 = value;
        }
    }

    /// <summary>
    /// Gets or sets the 'RES_GRP_8' value.
    /// </summary>
    public string RES_GRP_8
    {
        get
        {
            if (m_res_grp_8 == null )
            {
                m_res_grp_8 = " ";
            }
            return m_res_grp_8;
        }
        set
        {
            if (value == null || value == "" )
            {
                m_res_grp_8 = " ";
            }
            m_res_grp_8 = value;
        }
    }

    /// <summary>
    /// Gets or sets the 'RES_GRP_9' value.
    /// </summary>
    public string RES_GRP_9
    {
        get
        {
            if (m_res_grp_9 == null )
            {
                m_res_grp_9 = " ";
            }
            return m_res_grp_9;
        }
        set
        {
            if (value == null || value == "" )
            {
                m_res_grp_9 = " ";
            }
            m_res_grp_9 = value;
        }
    }

    /// <summary>
    /// Gets or sets the 'RES_GRP_10' value.
    /// </summary>
    public string RES_GRP_10
    {
        get
        {
            if (m_res_grp_10 == null )
            {
                m_res_grp_10 = " ";
            }
            return m_res_grp_10;
        }
        set
        {
            if (value == null || value == "" )
            {
                m_res_grp_10 = " ";
            }
            m_res_grp_10 = value;
        }
    }

    /// <summary>
    /// Gets or sets the 'USE_FAC_PRT_FLAG' value.
    /// </summary>
    public string USE_FAC_PRT_FLAG
    {
        get
        {
            if (m_use_fac_prt_flag == null )
            {
                m_use_fac_prt_flag = " ";
            }
            return m_use_fac_prt_flag;
        }
        set
        {
            if (value == null || value == "" )
            {
                m_use_fac_prt_flag = " ";
            }
            m_use_fac_prt_flag = value;
        }
    }

    /// <summary>
    /// Gets or sets the 'RES_STS_PRT_1' value.
    /// </summary>
    public string RES_STS_PRT_1
    {
        get
        {
            if (m_res_sts_prt_1 == null )
            {
                m_res_sts_prt_1 = " ";
            }
            return m_res_sts_prt_1;
        }
        set
        {
            if (value == null || value == "" )
            {
                m_res_sts_prt_1 = " ";
            }
            m_res_sts_prt_1 = value;
        }
    }

    /// <summary>
    /// Gets or sets the 'RES_STS_PRT_2' value.
    /// </summary>
    public string RES_STS_PRT_2
    {
        get
        {
            if (m_res_sts_prt_2 == null )
            {
                m_res_sts_prt_2 = " ";
            }
            return m_res_sts_prt_2;
        }
        set
        {
            if (value == null || value == "" )
            {
                m_res_sts_prt_2 = " ";
            }
            m_res_sts_prt_2 = value;
        }
    }

    /// <summary>
    /// Gets or sets the 'RES_STS_PRT_3' value.
    /// </summary>
    public string RES_STS_PRT_3
    {
        get
        {
            if (m_res_sts_prt_3 == null )
            {
                m_res_sts_prt_3 = " ";
            }
            return m_res_sts_prt_3;
        }
        set
        {
            if (value == null || value == "" )
            {
                m_res_sts_prt_3 = " ";
            }
            m_res_sts_prt_3 = value;
        }
    }

    /// <summary>
    /// Gets or sets the 'RES_STS_PRT_4' value.
    /// </summary>
    public string RES_STS_PRT_4
    {
        get
        {
            if (m_res_sts_prt_4 == null )
            {
                m_res_sts_prt_4 = " ";
            }
            return m_res_sts_prt_4;
        }
        set
        {
            if (value == null || value == "" )
            {
                m_res_sts_prt_4 = " ";
            }
            m_res_sts_prt_4 = value;
        }
    }

    /// <summary>
    /// Gets or sets the 'RES_STS_PRT_5' value.
    /// </summary>
    public string RES_STS_PRT_5
    {
        get
        {
            if (m_res_sts_prt_5 == null )
            {
                m_res_sts_prt_5 = " ";
            }
            return m_res_sts_prt_5;
        }
        set
        {
            if (value == null || value == "" )
            {
                m_res_sts_prt_5 = " ";
            }
            m_res_sts_prt_5 = value;
        }
    }

    /// <summary>
    /// Gets or sets the 'RES_STS_PRT_6' value.
    /// </summary>
    public string RES_STS_PRT_6
    {
        get
        {
            if (m_res_sts_prt_6 == null )
            {
                m_res_sts_prt_6 = " ";
            }
            return m_res_sts_prt_6;
        }
        set
        {
            if (value == null || value == "" )
            {
                m_res_sts_prt_6 = " ";
            }
            m_res_sts_prt_6 = value;
        }
    }

    /// <summary>
    /// Gets or sets the 'RES_STS_PRT_7' value.
    /// </summary>
    public string RES_STS_PRT_7
    {
        get
        {
            if (m_res_sts_prt_7 == null )
            {
                m_res_sts_prt_7 = " ";
            }
            return m_res_sts_prt_7;
        }
        set
        {
            if (value == null || value == "" )
            {
                m_res_sts_prt_7 = " ";
            }
            m_res_sts_prt_7 = value;
        }
    }

    /// <summary>
    /// Gets or sets the 'RES_STS_PRT_8' value.
    /// </summary>
    public string RES_STS_PRT_8
    {
        get
        {
            if (m_res_sts_prt_8 == null )
            {
                m_res_sts_prt_8 = " ";
            }
            return m_res_sts_prt_8;
        }
        set
        {
            if (value == null || value == "" )
            {
                m_res_sts_prt_8 = " ";
            }
            m_res_sts_prt_8 = value;
        }
    }

    /// <summary>
    /// Gets or sets the 'RES_STS_PRT_9' value.
    /// </summary>
    public string RES_STS_PRT_9
    {
        get
        {
            if (m_res_sts_prt_9 == null )
            {
                m_res_sts_prt_9 = " ";
            }
            return m_res_sts_prt_9;
        }
        set
        {
            if (value == null || value == "" )
            {
                m_res_sts_prt_9 = " ";
            }
            m_res_sts_prt_9 = value;
        }
    }

    /// <summary>
    /// Gets or sets the 'RES_STS_PRT_10' value.
    /// </summary>
    public string RES_STS_PRT_10
    {
        get
        {
            if (m_res_sts_prt_10 == null )
            {
                m_res_sts_prt_10 = " ";
            }
            return m_res_sts_prt_10;
        }
        set
        {
            if (value == null || value == "" )
            {
                m_res_sts_prt_10 = " ";
            }
            m_res_sts_prt_10 = value;
        }
    }

    /// <summary>
    /// Gets or sets the 'RES_CMF_1' value.
    /// </summary>
    public string RES_CMF_1
    {
        get
        {
            if (m_res_cmf_1 == null )
            {
                m_res_cmf_1 = " ";
            }
            return m_res_cmf_1;
        }
        set
        {
            if (value == null || value == "" )
            {
                m_res_cmf_1 = " ";
            }
            m_res_cmf_1 = value;
        }
    }

    /// <summary>
    /// Gets or sets the 'RES_CMF_2' value.
    /// </summary>
    public string RES_CMF_2
    {
        get
        {
            if (m_res_cmf_2 == null )
            {
                m_res_cmf_2 = " ";
            }
            return m_res_cmf_2;
        }
        set
        {
            if (value == null || value == "" )
            {
                m_res_cmf_2 = " ";
            }
            m_res_cmf_2 = value;
        }
    }

    /// <summary>
    /// Gets or sets the 'RES_CMF_3' value.
    /// </summary>
    public string RES_CMF_3
    {
        get
        {
            if (m_res_cmf_3 == null )
            {
                m_res_cmf_3 = " ";
            }
            return m_res_cmf_3;
        }
        set
        {
            if (value == null || value == "" )
            {
                m_res_cmf_3 = " ";
            }
            m_res_cmf_3 = value;
        }
    }

    /// <summary>
    /// Gets or sets the 'RES_CMF_4' value.
    /// </summary>
    public string RES_CMF_4
    {
        get
        {
            if (m_res_cmf_4 == null )
            {
                m_res_cmf_4 = " ";
            }
            return m_res_cmf_4;
        }
        set
        {
            if (value == null || value == "" )
            {
                m_res_cmf_4 = " ";
            }
            m_res_cmf_4 = value;
        }
    }

    /// <summary>
    /// Gets or sets the 'RES_CMF_5' value.
    /// </summary>
    public string RES_CMF_5
    {
        get
        {
            if (m_res_cmf_5 == null )
            {
                m_res_cmf_5 = " ";
            }
            return m_res_cmf_5;
        }
        set
        {
            if (value == null || value == "" )
            {
                m_res_cmf_5 = " ";
            }
            m_res_cmf_5 = value;
        }
    }

    /// <summary>
    /// Gets or sets the 'RES_CMF_6' value.
    /// </summary>
    public string RES_CMF_6
    {
        get
        {
            if (m_res_cmf_6 == null )
            {
                m_res_cmf_6 = " ";
            }
            return m_res_cmf_6;
        }
        set
        {
            if (value == null || value == "" )
            {
                m_res_cmf_6 = " ";
            }
            m_res_cmf_6 = value;
        }
    }

    /// <summary>
    /// Gets or sets the 'RES_CMF_7' value.
    /// </summary>
    public string RES_CMF_7
    {
        get
        {
            if (m_res_cmf_7 == null )
            {
                m_res_cmf_7 = " ";
            }
            return m_res_cmf_7;
        }
        set
        {
            if (value == null || value == "" )
            {
                m_res_cmf_7 = " ";
            }
            m_res_cmf_7 = value;
        }
    }

    /// <summary>
    /// Gets or sets the 'RES_CMF_8' value.
    /// </summary>
    public string RES_CMF_8
    {
        get
        {
            if (m_res_cmf_8 == null )
            {
                m_res_cmf_8 = " ";
            }
            return m_res_cmf_8;
        }
        set
        {
            if (value == null || value == "" )
            {
                m_res_cmf_8 = " ";
            }
            m_res_cmf_8 = value;
        }
    }

    /// <summary>
    /// Gets or sets the 'RES_CMF_9' value.
    /// </summary>
    public string RES_CMF_9
    {
        get
        {
            if (m_res_cmf_9 == null )
            {
                m_res_cmf_9 = " ";
            }
            return m_res_cmf_9;
        }
        set
        {
            if (value == null || value == "" )
            {
                m_res_cmf_9 = " ";
            }
            m_res_cmf_9 = value;
        }
    }

    /// <summary>
    /// Gets or sets the 'RES_CMF_10' value.
    /// </summary>
    public string RES_CMF_10
    {
        get
        {
            if (m_res_cmf_10 == null )
            {
                m_res_cmf_10 = " ";
            }
            return m_res_cmf_10;
        }
        set
        {
            if (value == null || value == "" )
            {
                m_res_cmf_10 = " ";
            }
            m_res_cmf_10 = value;
        }
    }

    /// <summary>
    /// Gets or sets the 'RES_CMF_11' value.
    /// </summary>
    public string RES_CMF_11
    {
        get
        {
            if (m_res_cmf_11 == null )
            {
                m_res_cmf_11 = " ";
            }
            return m_res_cmf_11;
        }
        set
        {
            if (value == null || value == "" )
            {
                m_res_cmf_11 = " ";
            }
            m_res_cmf_11 = value;
        }
    }

    /// <summary>
    /// Gets or sets the 'RES_CMF_12' value.
    /// </summary>
    public string RES_CMF_12
    {
        get
        {
            if (m_res_cmf_12 == null )
            {
                m_res_cmf_12 = " ";
            }
            return m_res_cmf_12;
        }
        set
        {
            if (value == null || value == "" )
            {
                m_res_cmf_12 = " ";
            }
            m_res_cmf_12 = value;
        }
    }

    /// <summary>
    /// Gets or sets the 'RES_CMF_13' value.
    /// </summary>
    public string RES_CMF_13
    {
        get
        {
            if (m_res_cmf_13 == null )
            {
                m_res_cmf_13 = " ";
            }
            return m_res_cmf_13;
        }
        set
        {
            if (value == null || value == "" )
            {
                m_res_cmf_13 = " ";
            }
            m_res_cmf_13 = value;
        }
    }

    /// <summary>
    /// Gets or sets the 'RES_CMF_14' value.
    /// </summary>
    public string RES_CMF_14
    {
        get
        {
            if (m_res_cmf_14 == null )
            {
                m_res_cmf_14 = " ";
            }
            return m_res_cmf_14;
        }
        set
        {
            if (value == null || value == "" )
            {
                m_res_cmf_14 = " ";
            }
            m_res_cmf_14 = value;
        }
    }

    /// <summary>
    /// Gets or sets the 'RES_CMF_15' value.
    /// </summary>
    public string RES_CMF_15
    {
        get
        {
            if (m_res_cmf_15 == null )
            {
                m_res_cmf_15 = " ";
            }
            return m_res_cmf_15;
        }
        set
        {
            if (value == null || value == "" )
            {
                m_res_cmf_15 = " ";
            }
            m_res_cmf_15 = value;
        }
    }

    /// <summary>
    /// Gets or sets the 'RES_CMF_16' value.
    /// </summary>
    public string RES_CMF_16
    {
        get
        {
            if (m_res_cmf_16 == null )
            {
                m_res_cmf_16 = " ";
            }
            return m_res_cmf_16;
        }
        set
        {
            if (value == null || value == "" )
            {
                m_res_cmf_16 = " ";
            }
            m_res_cmf_16 = value;
        }
    }

    /// <summary>
    /// Gets or sets the 'RES_CMF_17' value.
    /// </summary>
    public string RES_CMF_17
    {
        get
        {
            if (m_res_cmf_17 == null )
            {
                m_res_cmf_17 = " ";
            }
            return m_res_cmf_17;
        }
        set
        {
            if (value == null || value == "" )
            {
                m_res_cmf_17 = " ";
            }
            m_res_cmf_17 = value;
        }
    }

    /// <summary>
    /// Gets or sets the 'RES_CMF_18' value.
    /// </summary>
    public string RES_CMF_18
    {
        get
        {
            if (m_res_cmf_18 == null )
            {
                m_res_cmf_18 = " ";
            }
            return m_res_cmf_18;
        }
        set
        {
            if (value == null || value == "" )
            {
                m_res_cmf_18 = " ";
            }
            m_res_cmf_18 = value;
        }
    }

    /// <summary>
    /// Gets or sets the 'RES_CMF_19' value.
    /// </summary>
    public string RES_CMF_19
    {
        get
        {
            if (m_res_cmf_19 == null )
            {
                m_res_cmf_19 = " ";
            }
            return m_res_cmf_19;
        }
        set
        {
            if (value == null || value == "" )
            {
                m_res_cmf_19 = " ";
            }
            m_res_cmf_19 = value;
        }
    }

    /// <summary>
    /// Gets or sets the 'RES_CMF_20' value.
    /// </summary>
    public string RES_CMF_20
    {
        get
        {
            if (m_res_cmf_20 == null )
            {
                m_res_cmf_20 = " ";
            }
            return m_res_cmf_20;
        }
        set
        {
            if (value == null || value == "" )
            {
                m_res_cmf_20 = " ";
            }
            m_res_cmf_20 = value;
        }
    }

    /// <summary>
    /// Gets or sets the 'AREA_ID' value.
    /// </summary>
    public string AREA_ID
    {
        get
        {
            if (m_area_id == null )
            {
                m_area_id = " ";
            }
            return m_area_id;
        }
        set
        {
            if (value == null || value == "" )
            {
                m_area_id = " ";
            }
            m_area_id = value;
        }
    }

    /// <summary>
    /// Gets or sets the 'SUB_AREA_ID' value.
    /// </summary>
    public string SUB_AREA_ID
    {
        get
        {
            if (m_sub_area_id == null )
            {
                m_sub_area_id = " ";
            }
            return m_sub_area_id;
        }
        set
        {
            if (value == null || value == "" )
            {
                m_sub_area_id = " ";
            }
            m_sub_area_id = value;
        }
    }

    /// <summary>
    /// Gets or sets the 'RES_LOCATION' value.
    /// </summary>
    public string RES_LOCATION
    {
        get
        {
            if (m_res_location == null )
            {
                m_res_location = " ";
            }
            return m_res_location;
        }
        set
        {
            if (value == null || value == "" )
            {
                m_res_location = " ";
            }
            m_res_location = value;
        }
    }

    /// <summary>
    /// Gets or sets the 'PROC_RULE' value.
    /// </summary>
    public string PROC_RULE
    {
        get
        {
            if (m_proc_rule == null )
            {
                m_proc_rule = " ";
            }
            return m_proc_rule;
        }
        set
        {
            if (value == null || value == "" )
            {
                m_proc_rule = " ";
            }
            m_proc_rule = value;
        }
    }

    /// <summary>
    /// Gets or sets the 'MAX_PROC_COUNT' value.
    /// </summary>
    public int MAX_PROC_COUNT
    {
        get
        {
            return m_max_proc_count;
        }
        set
        {
            m_max_proc_count = value;
        }
    }

    /// <summary>
    /// Gets or sets the 'BATCH_COND_1' value.
    /// </summary>
    public string BATCH_COND_1
    {
        get
        {
            if (m_batch_cond_1 == null )
            {
                m_batch_cond_1 = " ";
            }
            return m_batch_cond_1;
        }
        set
        {
            if (value == null || value == "" )
            {
                m_batch_cond_1 = " ";
            }
            m_batch_cond_1 = value;
        }
    }

    /// <summary>
    /// Gets or sets the 'BATCH_COND_2' value.
    /// </summary>
    public string BATCH_COND_2
    {
        get
        {
            if (m_batch_cond_2 == null )
            {
                m_batch_cond_2 = " ";
            }
            return m_batch_cond_2;
        }
        set
        {
            if (value == null || value == "" )
            {
                m_batch_cond_2 = " ";
            }
            m_batch_cond_2 = value;
        }
    }

    /// <summary>
    /// Gets or sets the 'PM_SCH_ENABLE_FLAG' value.
    /// </summary>
    public string PM_SCH_ENABLE_FLAG
    {
        get
        {
            if (m_pm_sch_enable_flag == null )
            {
                m_pm_sch_enable_flag = " ";
            }
            return m_pm_sch_enable_flag;
        }
        set
        {
            if (value == null || value == "" )
            {
                m_pm_sch_enable_flag = " ";
            }
            m_pm_sch_enable_flag = value;
        }
    }

    /// <summary>
    /// Gets or sets the 'UNIT_BASE_ST_FLAG' value.
    /// </summary>
    public string UNIT_BASE_ST_FLAG
    {
        get
        {
            if (m_unit_base_st_flag == null )
            {
                m_unit_base_st_flag = " ";
            }
            return m_unit_base_st_flag;
        }
        set
        {
            if (value == null || value == "" )
            {
                m_unit_base_st_flag = " ";
            }
            m_unit_base_st_flag = value;
        }
    }

    /// <summary>
    /// Gets or sets the 'SEC_CHK_FLAG' value.
    /// </summary>
    public string SEC_CHK_FLAG
    {
        get
        {
            if (m_sec_chk_flag == null )
            {
                m_sec_chk_flag = " ";
            }
            return m_sec_chk_flag;
        }
        set
        {
            if (value == null || value == "" )
            {
                m_sec_chk_flag = " ";
            }
            m_sec_chk_flag = value;
        }
    }

    /// <summary>
    /// Gets or sets the 'GATHER_ALARM_FLAG' value.
    /// </summary>
    public string GATHER_ALARM_FLAG
    {
        get
        {
            if (m_gather_alarm_flag == null )
            {
                m_gather_alarm_flag = " ";
            }
            return m_gather_alarm_flag;
        }
        set
        {
            if (value == null || value == "" )
            {
                m_gather_alarm_flag = " ";
            }
            m_gather_alarm_flag = value;
        }
    }

    /// <summary>
    /// Gets or sets the 'DELETE_FLAG' value.
    /// </summary>
    public string DELETE_FLAG
    {
        get
        {
            if (m_delete_flag == null )
            {
                m_delete_flag = " ";
            }
            return m_delete_flag;
        }
        set
        {
            if (value == null || value == "" )
            {
                m_delete_flag = " ";
            }
            m_delete_flag = value;
        }
    }

    /// <summary>
    /// Gets or sets the 'DELETE_USER_ID' value.
    /// </summary>
    public string DELETE_USER_ID
    {
        get
        {
            if (m_delete_user_id == null )
            {
                m_delete_user_id = " ";
            }
            return m_delete_user_id;
        }
        set
        {
            if (value == null || value == "" )
            {
                m_delete_user_id = " ";
            }
            m_delete_user_id = value;
        }
    }

    /// <summary>
    /// Gets or sets the 'DELETE_TIME' value.
    /// </summary>
    public string DELETE_TIME
    {
        get
        {
            if (m_delete_time == null )
            {
                m_delete_time = " ";
            }
            return m_delete_time;
        }
        set
        {
            if (value == null || value == "" )
            {
                m_delete_time = " ";
            }
            m_delete_time = value;
        }
    }

    /// <summary>
    /// Gets or sets the 'CREATE_USER_ID' value.
    /// </summary>
    public string CREATE_USER_ID
    {
        get
        {
            if (m_create_user_id == null )
            {
                m_create_user_id = " ";
            }
            return m_create_user_id;
        }
        set
        {
            if (value == null || value == "" )
            {
                m_create_user_id = " ";
            }
            m_create_user_id = value;
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
    /// Gets or sets the 'UPDATE_USER_ID' value.
    /// </summary>
    public string UPDATE_USER_ID
    {
        get
        {
            if (m_update_user_id == null )
            {
                m_update_user_id = " ";
            }
            return m_update_user_id;
        }
        set
        {
            if (value == null || value == "" )
            {
                m_update_user_id = " ";
            }
            m_update_user_id = value;
        }
    }

    /// <summary>
    /// Gets or sets the 'UPDATE_TIME' value.
    /// </summary>
    public string UPDATE_TIME
    {
        get
        {
            if (m_update_time == null )
            {
                m_update_time = " ";
            }
            return m_update_time;
        }
        set
        {
            if (value == null || value == "" )
            {
                m_update_time = " ";
            }
            m_update_time = value;
        }
    }

    /// <summary>
    /// Gets or sets the 'RES_UP_DOWN_FLAG' value.
    /// </summary>
    public string RES_UP_DOWN_FLAG
    {
        get
        {
            if (m_res_up_down_flag == null )
            {
                m_res_up_down_flag = " ";
            }
            return m_res_up_down_flag;
        }
        set
        {
            if (value == null || value == "" )
            {
                m_res_up_down_flag = " ";
            }
            m_res_up_down_flag = value;
        }
    }

    /// <summary>
    /// Gets or sets the 'RES_PRI_STS' value.
    /// </summary>
    public string RES_PRI_STS
    {
        get
        {
            if (m_res_pri_sts == null )
            {
                m_res_pri_sts = " ";
            }
            return m_res_pri_sts;
        }
        set
        {
            if (value == null || value == "" )
            {
                m_res_pri_sts = " ";
            }
            m_res_pri_sts = value;
        }
    }

    /// <summary>
    /// Gets or sets the 'RES_STS_1' value.
    /// </summary>
    public string RES_STS_1
    {
        get
        {
            if (m_res_sts_1 == null )
            {
                m_res_sts_1 = " ";
            }
            return m_res_sts_1;
        }
        set
        {
            if (value == null || value == "" )
            {
                m_res_sts_1 = " ";
            }
            m_res_sts_1 = value;
        }
    }

    /// <summary>
    /// Gets or sets the 'RES_STS_2' value.
    /// </summary>
    public string RES_STS_2
    {
        get
        {
            if (m_res_sts_2 == null )
            {
                m_res_sts_2 = " ";
            }
            return m_res_sts_2;
        }
        set
        {
            if (value == null || value == "" )
            {
                m_res_sts_2 = " ";
            }
            m_res_sts_2 = value;
        }
    }

    /// <summary>
    /// Gets or sets the 'RES_STS_3' value.
    /// </summary>
    public string RES_STS_3
    {
        get
        {
            if (m_res_sts_3 == null )
            {
                m_res_sts_3 = " ";
            }
            return m_res_sts_3;
        }
        set
        {
            if (value == null || value == "" )
            {
                m_res_sts_3 = " ";
            }
            m_res_sts_3 = value;
        }
    }

    /// <summary>
    /// Gets or sets the 'RES_STS_4' value.
    /// </summary>
    public string RES_STS_4
    {
        get
        {
            if (m_res_sts_4 == null )
            {
                m_res_sts_4 = " ";
            }
            return m_res_sts_4;
        }
        set
        {
            if (value == null || value == "" )
            {
                m_res_sts_4 = " ";
            }
            m_res_sts_4 = value;
        }
    }

    /// <summary>
    /// Gets or sets the 'RES_STS_5' value.
    /// </summary>
    public string RES_STS_5
    {
        get
        {
            if (m_res_sts_5 == null )
            {
                m_res_sts_5 = " ";
            }
            return m_res_sts_5;
        }
        set
        {
            if (value == null || value == "" )
            {
                m_res_sts_5 = " ";
            }
            m_res_sts_5 = value;
        }
    }

    /// <summary>
    /// Gets or sets the 'RES_STS_6' value.
    /// </summary>
    public string RES_STS_6
    {
        get
        {
            if (m_res_sts_6 == null )
            {
                m_res_sts_6 = " ";
            }
            return m_res_sts_6;
        }
        set
        {
            if (value == null || value == "" )
            {
                m_res_sts_6 = " ";
            }
            m_res_sts_6 = value;
        }
    }

    /// <summary>
    /// Gets or sets the 'RES_STS_7' value.
    /// </summary>
    public string RES_STS_7
    {
        get
        {
            if (m_res_sts_7 == null )
            {
                m_res_sts_7 = " ";
            }
            return m_res_sts_7;
        }
        set
        {
            if (value == null || value == "" )
            {
                m_res_sts_7 = " ";
            }
            m_res_sts_7 = value;
        }
    }

    /// <summary>
    /// Gets or sets the 'RES_STS_8' value.
    /// </summary>
    public string RES_STS_8
    {
        get
        {
            if (m_res_sts_8 == null )
            {
                m_res_sts_8 = " ";
            }
            return m_res_sts_8;
        }
        set
        {
            if (value == null || value == "" )
            {
                m_res_sts_8 = " ";
            }
            m_res_sts_8 = value;
        }
    }

    /// <summary>
    /// Gets or sets the 'RES_STS_9' value.
    /// </summary>
    public string RES_STS_9
    {
        get
        {
            if (m_res_sts_9 == null )
            {
                m_res_sts_9 = " ";
            }
            return m_res_sts_9;
        }
        set
        {
            if (value == null || value == "" )
            {
                m_res_sts_9 = " ";
            }
            m_res_sts_9 = value;
        }
    }

    /// <summary>
    /// Gets or sets the 'RES_STS_10' value.
    /// </summary>
    public string RES_STS_10
    {
        get
        {
            if (m_res_sts_10 == null )
            {
                m_res_sts_10 = " ";
            }
            return m_res_sts_10;
        }
        set
        {
            if (value == null || value == "" )
            {
                m_res_sts_10 = " ";
            }
            m_res_sts_10 = value;
        }
    }

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
    /// Gets or sets the 'SUBLOT_ID' value.
    /// </summary>
    public string SUBLOT_ID
    {
        get
        {
            if (m_sublot_id == null )
            {
                m_sublot_id = " ";
            }
            return m_sublot_id;
        }
        set
        {
            if (value == null || value == "" )
            {
                m_sublot_id = " ";
            }
            m_sublot_id = value;
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
    /// Gets or sets the 'RES_CTRL_MODE' value.
    /// </summary>
    public string RES_CTRL_MODE
    {
        get
        {
            if (m_res_ctrl_mode == null )
            {
                m_res_ctrl_mode = " ";
            }
            return m_res_ctrl_mode;
        }
        set
        {
            if (value == null || value == "" )
            {
                m_res_ctrl_mode = " ";
            }
            m_res_ctrl_mode = value;
        }
    }

    /// <summary>
    /// Gets or sets the 'RES_PROC_MODE' value.
    /// </summary>
    public string RES_PROC_MODE
    {
        get
        {
            if (m_res_proc_mode == null )
            {
                m_res_proc_mode = " ";
            }
            return m_res_proc_mode;
        }
        set
        {
            if (value == null || value == "" )
            {
                m_res_proc_mode = " ";
            }
            m_res_proc_mode = value;
        }
    }

    /// <summary>
    /// Gets or sets the 'LAST_RECIPE_ID' value.
    /// </summary>
    public string LAST_RECIPE_ID
    {
        get
        {
            if (m_last_recipe_id == null )
            {
                m_last_recipe_id = " ";
            }
            return m_last_recipe_id;
        }
        set
        {
            if (value == null || value == "" )
            {
                m_last_recipe_id = " ";
            }
            m_last_recipe_id = value;
        }
    }

    /// <summary>
    /// Gets or sets the 'PROC_COUNT' value.
    /// </summary>
    public int PROC_COUNT
    {
        get
        {
            return m_proc_count;
        }
        set
        {
            m_proc_count = value;
        }
    }

    /// <summary>
    /// Gets or sets the 'LAST_START_TIME' value.
    /// </summary>
    public string LAST_START_TIME
    {
        get
        {
            if (m_last_start_time == null )
            {
                m_last_start_time = " ";
            }
            return m_last_start_time;
        }
        set
        {
            if (value == null || value == "" )
            {
                m_last_start_time = " ";
            }
            m_last_start_time = value;
        }
    }

    /// <summary>
    /// Gets or sets the 'LAST_END_TIME' value.
    /// </summary>
    public string LAST_END_TIME
    {
        get
        {
            if (m_last_end_time == null )
            {
                m_last_end_time = " ";
            }
            return m_last_end_time;
        }
        set
        {
            if (value == null || value == "" )
            {
                m_last_end_time = " ";
            }
            m_last_end_time = value;
        }
    }

    /// <summary>
    /// Gets or sets the 'LAST_DOWN_TIME' value.
    /// </summary>
    public string LAST_DOWN_TIME
    {
        get
        {
            if (m_last_down_time == null )
            {
                m_last_down_time = " ";
            }
            return m_last_down_time;
        }
        set
        {
            if (value == null || value == "" )
            {
                m_last_down_time = " ";
            }
            m_last_down_time = value;
        }
    }

    /// <summary>
    /// Gets or sets the 'LAST_DOWN_HIST_SEQ' value.
    /// </summary>
    public int LAST_DOWN_HIST_SEQ
    {
        get
        {
            return m_last_down_hist_seq;
        }
        set
        {
            m_last_down_hist_seq = value;
        }
    }

    /// <summary>
    /// Gets or sets the 'LAST_EVENT_ID' value.
    /// </summary>
    public string LAST_EVENT_ID
    {
        get
        {
            if (m_last_event_id == null )
            {
                m_last_event_id = " ";
            }
            return m_last_event_id;
        }
        set
        {
            if (value == null || value == "" )
            {
                m_last_event_id = " ";
            }
            m_last_event_id = value;
        }
    }

    /// <summary>
    /// Gets or sets the 'LAST_EVENT_TIME' value.
    /// </summary>
    public string LAST_EVENT_TIME
    {
        get
        {
            if (m_last_event_time == null )
            {
                m_last_event_time = " ";
            }
            return m_last_event_time;
        }
        set
        {
            if (value == null || value == "" )
            {
                m_last_event_time = " ";
            }
            m_last_event_time = value;
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

#endregion

#region " Function Definition "

    /// <summary>
    /// Creator for Object
    /// <summary>
    public DBC_MRASRESDEF(ref DB_Common dbc)
    {
        this._dbc = dbc;
        Init();
    }

    /// <summary>
    /// Initialization
    /// <summary>
    public bool Init()
    {
        FACTORY = " ";
        RES_ID = " ";
        RES_DESC = " ";
        RES_TYPE = " ";
        RES_GRP_1 = " ";
        RES_GRP_2 = " ";
        RES_GRP_3 = " ";
        RES_GRP_4 = " ";
        RES_GRP_5 = " ";
        RES_GRP_6 = " ";
        RES_GRP_7 = " ";
        RES_GRP_8 = " ";
        RES_GRP_9 = " ";
        RES_GRP_10 = " ";
        USE_FAC_PRT_FLAG = " ";
        RES_STS_PRT_1 = " ";
        RES_STS_PRT_2 = " ";
        RES_STS_PRT_3 = " ";
        RES_STS_PRT_4 = " ";
        RES_STS_PRT_5 = " ";
        RES_STS_PRT_6 = " ";
        RES_STS_PRT_7 = " ";
        RES_STS_PRT_8 = " ";
        RES_STS_PRT_9 = " ";
        RES_STS_PRT_10 = " ";
        RES_CMF_1 = " ";
        RES_CMF_2 = " ";
        RES_CMF_3 = " ";
        RES_CMF_4 = " ";
        RES_CMF_5 = " ";
        RES_CMF_6 = " ";
        RES_CMF_7 = " ";
        RES_CMF_8 = " ";
        RES_CMF_9 = " ";
        RES_CMF_10 = " ";
        RES_CMF_11 = " ";
        RES_CMF_12 = " ";
        RES_CMF_13 = " ";
        RES_CMF_14 = " ";
        RES_CMF_15 = " ";
        RES_CMF_16 = " ";
        RES_CMF_17 = " ";
        RES_CMF_18 = " ";
        RES_CMF_19 = " ";
        RES_CMF_20 = " ";
        AREA_ID = " ";
        SUB_AREA_ID = " ";
        RES_LOCATION = " ";
        PROC_RULE = " ";
        MAX_PROC_COUNT = 0;
        BATCH_COND_1 = " ";
        BATCH_COND_2 = " ";
        PM_SCH_ENABLE_FLAG = " ";
        UNIT_BASE_ST_FLAG = " ";
        SEC_CHK_FLAG = " ";
        GATHER_ALARM_FLAG = " ";
        DELETE_FLAG = " ";
        DELETE_USER_ID = " ";
        DELETE_TIME = " ";
        CREATE_USER_ID = " ";
        CREATE_TIME = " ";
        UPDATE_USER_ID = " ";
        UPDATE_TIME = " ";
        RES_UP_DOWN_FLAG = " ";
        RES_PRI_STS = " ";
        RES_STS_1 = " ";
        RES_STS_2 = " ";
        RES_STS_3 = " ";
        RES_STS_4 = " ";
        RES_STS_5 = " ";
        RES_STS_6 = " ";
        RES_STS_7 = " ";
        RES_STS_8 = " ";
        RES_STS_9 = " ";
        RES_STS_10 = " ";
        LOT_ID = " ";
        SUBLOT_ID = " ";
        CRR_ID = " ";
        RES_CTRL_MODE = " ";
        RES_PROC_MODE = " ";
        LAST_RECIPE_ID = " ";
        PROC_COUNT = 0;
        LAST_START_TIME = " ";
        LAST_END_TIME = " ";
        LAST_DOWN_TIME = " ";
        LAST_DOWN_HIST_SEQ = 0;
        LAST_EVENT_ID = " ";
        LAST_EVENT_TIME = " ";
        LAST_ACTIVE_HIST_SEQ = 0;
        LAST_HIST_SEQ = 0;

        return true;
    }

    /// <summary>
    /// Clone object
    /// </summary>
    /// <returns>object</returns>
    public object Clone()
    {
        DBC_MRASRESDEF MRASRESDEF = null;
        try
        {
            MRASRESDEF = new DBC_MRASRESDEF(ref _dbc);

            MRASRESDEF.FACTORY = this.FACTORY;
            MRASRESDEF.RES_ID = this.RES_ID;
            MRASRESDEF.RES_DESC = this.RES_DESC;
            MRASRESDEF.RES_TYPE = this.RES_TYPE;
            MRASRESDEF.RES_GRP_1 = this.RES_GRP_1;
            MRASRESDEF.RES_GRP_2 = this.RES_GRP_2;
            MRASRESDEF.RES_GRP_3 = this.RES_GRP_3;
            MRASRESDEF.RES_GRP_4 = this.RES_GRP_4;
            MRASRESDEF.RES_GRP_5 = this.RES_GRP_5;
            MRASRESDEF.RES_GRP_6 = this.RES_GRP_6;
            MRASRESDEF.RES_GRP_7 = this.RES_GRP_7;
            MRASRESDEF.RES_GRP_8 = this.RES_GRP_8;
            MRASRESDEF.RES_GRP_9 = this.RES_GRP_9;
            MRASRESDEF.RES_GRP_10 = this.RES_GRP_10;
            MRASRESDEF.USE_FAC_PRT_FLAG = this.USE_FAC_PRT_FLAG;
            MRASRESDEF.RES_STS_PRT_1 = this.RES_STS_PRT_1;
            MRASRESDEF.RES_STS_PRT_2 = this.RES_STS_PRT_2;
            MRASRESDEF.RES_STS_PRT_3 = this.RES_STS_PRT_3;
            MRASRESDEF.RES_STS_PRT_4 = this.RES_STS_PRT_4;
            MRASRESDEF.RES_STS_PRT_5 = this.RES_STS_PRT_5;
            MRASRESDEF.RES_STS_PRT_6 = this.RES_STS_PRT_6;
            MRASRESDEF.RES_STS_PRT_7 = this.RES_STS_PRT_7;
            MRASRESDEF.RES_STS_PRT_8 = this.RES_STS_PRT_8;
            MRASRESDEF.RES_STS_PRT_9 = this.RES_STS_PRT_9;
            MRASRESDEF.RES_STS_PRT_10 = this.RES_STS_PRT_10;
            MRASRESDEF.RES_CMF_1 = this.RES_CMF_1;
            MRASRESDEF.RES_CMF_2 = this.RES_CMF_2;
            MRASRESDEF.RES_CMF_3 = this.RES_CMF_3;
            MRASRESDEF.RES_CMF_4 = this.RES_CMF_4;
            MRASRESDEF.RES_CMF_5 = this.RES_CMF_5;
            MRASRESDEF.RES_CMF_6 = this.RES_CMF_6;
            MRASRESDEF.RES_CMF_7 = this.RES_CMF_7;
            MRASRESDEF.RES_CMF_8 = this.RES_CMF_8;
            MRASRESDEF.RES_CMF_9 = this.RES_CMF_9;
            MRASRESDEF.RES_CMF_10 = this.RES_CMF_10;
            MRASRESDEF.RES_CMF_11 = this.RES_CMF_11;
            MRASRESDEF.RES_CMF_12 = this.RES_CMF_12;
            MRASRESDEF.RES_CMF_13 = this.RES_CMF_13;
            MRASRESDEF.RES_CMF_14 = this.RES_CMF_14;
            MRASRESDEF.RES_CMF_15 = this.RES_CMF_15;
            MRASRESDEF.RES_CMF_16 = this.RES_CMF_16;
            MRASRESDEF.RES_CMF_17 = this.RES_CMF_17;
            MRASRESDEF.RES_CMF_18 = this.RES_CMF_18;
            MRASRESDEF.RES_CMF_19 = this.RES_CMF_19;
            MRASRESDEF.RES_CMF_20 = this.RES_CMF_20;
            MRASRESDEF.AREA_ID = this.AREA_ID;
            MRASRESDEF.SUB_AREA_ID = this.SUB_AREA_ID;
            MRASRESDEF.RES_LOCATION = this.RES_LOCATION;
            MRASRESDEF.PROC_RULE = this.PROC_RULE;
            MRASRESDEF.MAX_PROC_COUNT = this.MAX_PROC_COUNT;
            MRASRESDEF.BATCH_COND_1 = this.BATCH_COND_1;
            MRASRESDEF.BATCH_COND_2 = this.BATCH_COND_2;
            MRASRESDEF.PM_SCH_ENABLE_FLAG = this.PM_SCH_ENABLE_FLAG;
            MRASRESDEF.UNIT_BASE_ST_FLAG = this.UNIT_BASE_ST_FLAG;
            MRASRESDEF.SEC_CHK_FLAG = this.SEC_CHK_FLAG;
            MRASRESDEF.GATHER_ALARM_FLAG = this.GATHER_ALARM_FLAG;
            MRASRESDEF.DELETE_FLAG = this.DELETE_FLAG;
            MRASRESDEF.DELETE_USER_ID = this.DELETE_USER_ID;
            MRASRESDEF.DELETE_TIME = this.DELETE_TIME;
            MRASRESDEF.CREATE_USER_ID = this.CREATE_USER_ID;
            MRASRESDEF.CREATE_TIME = this.CREATE_TIME;
            MRASRESDEF.UPDATE_USER_ID = this.UPDATE_USER_ID;
            MRASRESDEF.UPDATE_TIME = this.UPDATE_TIME;
            MRASRESDEF.RES_UP_DOWN_FLAG = this.RES_UP_DOWN_FLAG;
            MRASRESDEF.RES_PRI_STS = this.RES_PRI_STS;
            MRASRESDEF.RES_STS_1 = this.RES_STS_1;
            MRASRESDEF.RES_STS_2 = this.RES_STS_2;
            MRASRESDEF.RES_STS_3 = this.RES_STS_3;
            MRASRESDEF.RES_STS_4 = this.RES_STS_4;
            MRASRESDEF.RES_STS_5 = this.RES_STS_5;
            MRASRESDEF.RES_STS_6 = this.RES_STS_6;
            MRASRESDEF.RES_STS_7 = this.RES_STS_7;
            MRASRESDEF.RES_STS_8 = this.RES_STS_8;
            MRASRESDEF.RES_STS_9 = this.RES_STS_9;
            MRASRESDEF.RES_STS_10 = this.RES_STS_10;
            MRASRESDEF.LOT_ID = this.LOT_ID;
            MRASRESDEF.SUBLOT_ID = this.SUBLOT_ID;
            MRASRESDEF.CRR_ID = this.CRR_ID;
            MRASRESDEF.RES_CTRL_MODE = this.RES_CTRL_MODE;
            MRASRESDEF.RES_PROC_MODE = this.RES_PROC_MODE;
            MRASRESDEF.LAST_RECIPE_ID = this.LAST_RECIPE_ID;
            MRASRESDEF.PROC_COUNT = this.PROC_COUNT;
            MRASRESDEF.LAST_START_TIME = this.LAST_START_TIME;
            MRASRESDEF.LAST_END_TIME = this.LAST_END_TIME;
            MRASRESDEF.LAST_DOWN_TIME = this.LAST_DOWN_TIME;
            MRASRESDEF.LAST_DOWN_HIST_SEQ = this.LAST_DOWN_HIST_SEQ;
            MRASRESDEF.LAST_EVENT_ID = this.LAST_EVENT_ID;
            MRASRESDEF.LAST_EVENT_TIME = this.LAST_EVENT_TIME;
            MRASRESDEF.LAST_ACTIVE_HIST_SEQ = this.LAST_ACTIVE_HIST_SEQ;
            MRASRESDEF.LAST_HIST_SEQ = this.LAST_HIST_SEQ;
        }
        catch(Exception ex)
        {
            if (ex.GetType().ToString() == "System.Data.OleDb.OleDbException")
                _dbc.gErrors.SetErrors(((OleDbException)ex).Errors);
            else
                _dbc.gErrors.SetErrors(ex);

            return null;
        }

        return MRASRESDEF;
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

            FACTORY = (string)(Row["FACTORY"]);
            RES_ID = (string)(Row["RES_ID"]);
            RES_DESC = (string)(Row["RES_DESC"]);
            RES_TYPE = (string)(Row["RES_TYPE"]);
            RES_GRP_1 = (string)(Row["RES_GRP_1"]);
            RES_GRP_2 = (string)(Row["RES_GRP_2"]);
            RES_GRP_3 = (string)(Row["RES_GRP_3"]);
            RES_GRP_4 = (string)(Row["RES_GRP_4"]);
            RES_GRP_5 = (string)(Row["RES_GRP_5"]);
            RES_GRP_6 = (string)(Row["RES_GRP_6"]);
            RES_GRP_7 = (string)(Row["RES_GRP_7"]);
            RES_GRP_8 = (string)(Row["RES_GRP_8"]);
            RES_GRP_9 = (string)(Row["RES_GRP_9"]);
            RES_GRP_10 = (string)(Row["RES_GRP_10"]);
            USE_FAC_PRT_FLAG = (string)(Row["USE_FAC_PRT_FLAG"]);
            RES_STS_PRT_1 = (string)(Row["RES_STS_PRT_1"]);
            RES_STS_PRT_2 = (string)(Row["RES_STS_PRT_2"]);
            RES_STS_PRT_3 = (string)(Row["RES_STS_PRT_3"]);
            RES_STS_PRT_4 = (string)(Row["RES_STS_PRT_4"]);
            RES_STS_PRT_5 = (string)(Row["RES_STS_PRT_5"]);
            RES_STS_PRT_6 = (string)(Row["RES_STS_PRT_6"]);
            RES_STS_PRT_7 = (string)(Row["RES_STS_PRT_7"]);
            RES_STS_PRT_8 = (string)(Row["RES_STS_PRT_8"]);
            RES_STS_PRT_9 = (string)(Row["RES_STS_PRT_9"]);
            RES_STS_PRT_10 = (string)(Row["RES_STS_PRT_10"]);
            RES_CMF_1 = (string)(Row["RES_CMF_1"]);
            RES_CMF_2 = (string)(Row["RES_CMF_2"]);
            RES_CMF_3 = (string)(Row["RES_CMF_3"]);
            RES_CMF_4 = (string)(Row["RES_CMF_4"]);
            RES_CMF_5 = (string)(Row["RES_CMF_5"]);
            RES_CMF_6 = (string)(Row["RES_CMF_6"]);
            RES_CMF_7 = (string)(Row["RES_CMF_7"]);
            RES_CMF_8 = (string)(Row["RES_CMF_8"]);
            RES_CMF_9 = (string)(Row["RES_CMF_9"]);
            RES_CMF_10 = (string)(Row["RES_CMF_10"]);
            RES_CMF_11 = (string)(Row["RES_CMF_11"]);
            RES_CMF_12 = (string)(Row["RES_CMF_12"]);
            RES_CMF_13 = (string)(Row["RES_CMF_13"]);
            RES_CMF_14 = (string)(Row["RES_CMF_14"]);
            RES_CMF_15 = (string)(Row["RES_CMF_15"]);
            RES_CMF_16 = (string)(Row["RES_CMF_16"]);
            RES_CMF_17 = (string)(Row["RES_CMF_17"]);
            RES_CMF_18 = (string)(Row["RES_CMF_18"]);
            RES_CMF_19 = (string)(Row["RES_CMF_19"]);
            RES_CMF_20 = (string)(Row["RES_CMF_20"]);
            AREA_ID = (string)(Row["AREA_ID"]);
            SUB_AREA_ID = (string)(Row["SUB_AREA_ID"]);
            RES_LOCATION = (string)(Row["RES_LOCATION"]);
            PROC_RULE = (string)(Row["PROC_RULE"]);
            MAX_PROC_COUNT = Convert.ToInt32(Row["MAX_PROC_COUNT"]);
            BATCH_COND_1 = (string)(Row["BATCH_COND_1"]);
            BATCH_COND_2 = (string)(Row["BATCH_COND_2"]);
            PM_SCH_ENABLE_FLAG = (string)(Row["PM_SCH_ENABLE_FLAG"]);
            UNIT_BASE_ST_FLAG = (string)(Row["UNIT_BASE_ST_FLAG"]);
            SEC_CHK_FLAG = (string)(Row["SEC_CHK_FLAG"]);
            GATHER_ALARM_FLAG = (string)(Row["GATHER_ALARM_FLAG"]);
            DELETE_FLAG = (string)(Row["DELETE_FLAG"]);
            DELETE_USER_ID = (string)(Row["DELETE_USER_ID"]);
            DELETE_TIME = (string)(Row["DELETE_TIME"]);
            CREATE_USER_ID = (string)(Row["CREATE_USER_ID"]);
            CREATE_TIME = (string)(Row["CREATE_TIME"]);
            UPDATE_USER_ID = (string)(Row["UPDATE_USER_ID"]);
            UPDATE_TIME = (string)(Row["UPDATE_TIME"]);
            RES_UP_DOWN_FLAG = (string)(Row["RES_UP_DOWN_FLAG"]);
            RES_PRI_STS = (string)(Row["RES_PRI_STS"]);
            RES_STS_1 = (string)(Row["RES_STS_1"]);
            RES_STS_2 = (string)(Row["RES_STS_2"]);
            RES_STS_3 = (string)(Row["RES_STS_3"]);
            RES_STS_4 = (string)(Row["RES_STS_4"]);
            RES_STS_5 = (string)(Row["RES_STS_5"]);
            RES_STS_6 = (string)(Row["RES_STS_6"]);
            RES_STS_7 = (string)(Row["RES_STS_7"]);
            RES_STS_8 = (string)(Row["RES_STS_8"]);
            RES_STS_9 = (string)(Row["RES_STS_9"]);
            RES_STS_10 = (string)(Row["RES_STS_10"]);
            LOT_ID = (string)(Row["LOT_ID"]);
            SUBLOT_ID = (string)(Row["SUBLOT_ID"]);
            CRR_ID = (string)(Row["CRR_ID"]);
            RES_CTRL_MODE = (string)(Row["RES_CTRL_MODE"]);
            RES_PROC_MODE = (string)(Row["RES_PROC_MODE"]);
            LAST_RECIPE_ID = (string)(Row["LAST_RECIPE_ID"]);
            PROC_COUNT = Convert.ToInt32(Row["PROC_COUNT"]);
            LAST_START_TIME = (string)(Row["LAST_START_TIME"]);
            LAST_END_TIME = (string)(Row["LAST_END_TIME"]);
            LAST_DOWN_TIME = (string)(Row["LAST_DOWN_TIME"]);
            LAST_DOWN_HIST_SEQ = Convert.ToInt32(Row["LAST_DOWN_HIST_SEQ"]);
            LAST_EVENT_ID = (string)(Row["LAST_EVENT_ID"]);
            LAST_EVENT_TIME = (string)(Row["LAST_EVENT_TIME"]);
            LAST_ACTIVE_HIST_SEQ = Convert.ToInt32(Row["LAST_ACTIVE_HIST_SEQ"]);
            LAST_HIST_SEQ = Convert.ToInt32(Row["LAST_HIST_SEQ"]);
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
                    strQuery = "SELECT * FROM MRASRESDEF"
                        + " WHERE FACTORY=?"
                        + " AND RES_ID=?";

                    // Add Parameters
                    adoAdapter.SelectCommand.Parameters.Add("@FACTORY", OleDbType.VarChar).Value = FACTORY;
                    adoAdapter.SelectCommand.Parameters.Add("@RES_ID", OleDbType.VarChar).Value = RES_ID;
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
                    case "FACTORY":
                        FACTORY = (string)(adoDataTable.Rows[0]["FACTORY"]);
                        break;
                    case "RES_ID":
                        RES_ID = (string)(adoDataTable.Rows[0]["RES_ID"]);
                        break;
                    case "RES_DESC":
                        RES_DESC = (string)(adoDataTable.Rows[0]["RES_DESC"]);
                        break;
                    case "RES_TYPE":
                        RES_TYPE = (string)(adoDataTable.Rows[0]["RES_TYPE"]);
                        break;
                    case "RES_GRP_1":
                        RES_GRP_1 = (string)(adoDataTable.Rows[0]["RES_GRP_1"]);
                        break;
                    case "RES_GRP_2":
                        RES_GRP_2 = (string)(adoDataTable.Rows[0]["RES_GRP_2"]);
                        break;
                    case "RES_GRP_3":
                        RES_GRP_3 = (string)(adoDataTable.Rows[0]["RES_GRP_3"]);
                        break;
                    case "RES_GRP_4":
                        RES_GRP_4 = (string)(adoDataTable.Rows[0]["RES_GRP_4"]);
                        break;
                    case "RES_GRP_5":
                        RES_GRP_5 = (string)(adoDataTable.Rows[0]["RES_GRP_5"]);
                        break;
                    case "RES_GRP_6":
                        RES_GRP_6 = (string)(adoDataTable.Rows[0]["RES_GRP_6"]);
                        break;
                    case "RES_GRP_7":
                        RES_GRP_7 = (string)(adoDataTable.Rows[0]["RES_GRP_7"]);
                        break;
                    case "RES_GRP_8":
                        RES_GRP_8 = (string)(adoDataTable.Rows[0]["RES_GRP_8"]);
                        break;
                    case "RES_GRP_9":
                        RES_GRP_9 = (string)(adoDataTable.Rows[0]["RES_GRP_9"]);
                        break;
                    case "RES_GRP_10":
                        RES_GRP_10 = (string)(adoDataTable.Rows[0]["RES_GRP_10"]);
                        break;
                    case "USE_FAC_PRT_FLAG":
                        USE_FAC_PRT_FLAG = (string)(adoDataTable.Rows[0]["USE_FAC_PRT_FLAG"]);
                        break;
                    case "RES_STS_PRT_1":
                        RES_STS_PRT_1 = (string)(adoDataTable.Rows[0]["RES_STS_PRT_1"]);
                        break;
                    case "RES_STS_PRT_2":
                        RES_STS_PRT_2 = (string)(adoDataTable.Rows[0]["RES_STS_PRT_2"]);
                        break;
                    case "RES_STS_PRT_3":
                        RES_STS_PRT_3 = (string)(adoDataTable.Rows[0]["RES_STS_PRT_3"]);
                        break;
                    case "RES_STS_PRT_4":
                        RES_STS_PRT_4 = (string)(adoDataTable.Rows[0]["RES_STS_PRT_4"]);
                        break;
                    case "RES_STS_PRT_5":
                        RES_STS_PRT_5 = (string)(adoDataTable.Rows[0]["RES_STS_PRT_5"]);
                        break;
                    case "RES_STS_PRT_6":
                        RES_STS_PRT_6 = (string)(adoDataTable.Rows[0]["RES_STS_PRT_6"]);
                        break;
                    case "RES_STS_PRT_7":
                        RES_STS_PRT_7 = (string)(adoDataTable.Rows[0]["RES_STS_PRT_7"]);
                        break;
                    case "RES_STS_PRT_8":
                        RES_STS_PRT_8 = (string)(adoDataTable.Rows[0]["RES_STS_PRT_8"]);
                        break;
                    case "RES_STS_PRT_9":
                        RES_STS_PRT_9 = (string)(adoDataTable.Rows[0]["RES_STS_PRT_9"]);
                        break;
                    case "RES_STS_PRT_10":
                        RES_STS_PRT_10 = (string)(adoDataTable.Rows[0]["RES_STS_PRT_10"]);
                        break;
                    case "RES_CMF_1":
                        RES_CMF_1 = (string)(adoDataTable.Rows[0]["RES_CMF_1"]);
                        break;
                    case "RES_CMF_2":
                        RES_CMF_2 = (string)(adoDataTable.Rows[0]["RES_CMF_2"]);
                        break;
                    case "RES_CMF_3":
                        RES_CMF_3 = (string)(adoDataTable.Rows[0]["RES_CMF_3"]);
                        break;
                    case "RES_CMF_4":
                        RES_CMF_4 = (string)(adoDataTable.Rows[0]["RES_CMF_4"]);
                        break;
                    case "RES_CMF_5":
                        RES_CMF_5 = (string)(adoDataTable.Rows[0]["RES_CMF_5"]);
                        break;
                    case "RES_CMF_6":
                        RES_CMF_6 = (string)(adoDataTable.Rows[0]["RES_CMF_6"]);
                        break;
                    case "RES_CMF_7":
                        RES_CMF_7 = (string)(adoDataTable.Rows[0]["RES_CMF_7"]);
                        break;
                    case "RES_CMF_8":
                        RES_CMF_8 = (string)(adoDataTable.Rows[0]["RES_CMF_8"]);
                        break;
                    case "RES_CMF_9":
                        RES_CMF_9 = (string)(adoDataTable.Rows[0]["RES_CMF_9"]);
                        break;
                    case "RES_CMF_10":
                        RES_CMF_10 = (string)(adoDataTable.Rows[0]["RES_CMF_10"]);
                        break;
                    case "RES_CMF_11":
                        RES_CMF_11 = (string)(adoDataTable.Rows[0]["RES_CMF_11"]);
                        break;
                    case "RES_CMF_12":
                        RES_CMF_12 = (string)(adoDataTable.Rows[0]["RES_CMF_12"]);
                        break;
                    case "RES_CMF_13":
                        RES_CMF_13 = (string)(adoDataTable.Rows[0]["RES_CMF_13"]);
                        break;
                    case "RES_CMF_14":
                        RES_CMF_14 = (string)(adoDataTable.Rows[0]["RES_CMF_14"]);
                        break;
                    case "RES_CMF_15":
                        RES_CMF_15 = (string)(adoDataTable.Rows[0]["RES_CMF_15"]);
                        break;
                    case "RES_CMF_16":
                        RES_CMF_16 = (string)(adoDataTable.Rows[0]["RES_CMF_16"]);
                        break;
                    case "RES_CMF_17":
                        RES_CMF_17 = (string)(adoDataTable.Rows[0]["RES_CMF_17"]);
                        break;
                    case "RES_CMF_18":
                        RES_CMF_18 = (string)(adoDataTable.Rows[0]["RES_CMF_18"]);
                        break;
                    case "RES_CMF_19":
                        RES_CMF_19 = (string)(adoDataTable.Rows[0]["RES_CMF_19"]);
                        break;
                    case "RES_CMF_20":
                        RES_CMF_20 = (string)(adoDataTable.Rows[0]["RES_CMF_20"]);
                        break;
                    case "AREA_ID":
                        AREA_ID = (string)(adoDataTable.Rows[0]["AREA_ID"]);
                        break;
                    case "SUB_AREA_ID":
                        SUB_AREA_ID = (string)(adoDataTable.Rows[0]["SUB_AREA_ID"]);
                        break;
                    case "RES_LOCATION":
                        RES_LOCATION = (string)(adoDataTable.Rows[0]["RES_LOCATION"]);
                        break;
                    case "PROC_RULE":
                        PROC_RULE = (string)(adoDataTable.Rows[0]["PROC_RULE"]);
                        break;
                    case "MAX_PROC_COUNT":
                        MAX_PROC_COUNT = Convert.ToInt32(adoDataTable.Rows[0]["MAX_PROC_COUNT"]);
                        break;
                    case "BATCH_COND_1":
                        BATCH_COND_1 = (string)(adoDataTable.Rows[0]["BATCH_COND_1"]);
                        break;
                    case "BATCH_COND_2":
                        BATCH_COND_2 = (string)(adoDataTable.Rows[0]["BATCH_COND_2"]);
                        break;
                    case "PM_SCH_ENABLE_FLAG":
                        PM_SCH_ENABLE_FLAG = (string)(adoDataTable.Rows[0]["PM_SCH_ENABLE_FLAG"]);
                        break;
                    case "UNIT_BASE_ST_FLAG":
                        UNIT_BASE_ST_FLAG = (string)(adoDataTable.Rows[0]["UNIT_BASE_ST_FLAG"]);
                        break;
                    case "SEC_CHK_FLAG":
                        SEC_CHK_FLAG = (string)(adoDataTable.Rows[0]["SEC_CHK_FLAG"]);
                        break;
                    case "GATHER_ALARM_FLAG":
                        GATHER_ALARM_FLAG = (string)(adoDataTable.Rows[0]["GATHER_ALARM_FLAG"]);
                        break;
                    case "DELETE_FLAG":
                        DELETE_FLAG = (string)(adoDataTable.Rows[0]["DELETE_FLAG"]);
                        break;
                    case "DELETE_USER_ID":
                        DELETE_USER_ID = (string)(adoDataTable.Rows[0]["DELETE_USER_ID"]);
                        break;
                    case "DELETE_TIME":
                        DELETE_TIME = (string)(adoDataTable.Rows[0]["DELETE_TIME"]);
                        break;
                    case "CREATE_USER_ID":
                        CREATE_USER_ID = (string)(adoDataTable.Rows[0]["CREATE_USER_ID"]);
                        break;
                    case "CREATE_TIME":
                        CREATE_TIME = (string)(adoDataTable.Rows[0]["CREATE_TIME"]);
                        break;
                    case "UPDATE_USER_ID":
                        UPDATE_USER_ID = (string)(adoDataTable.Rows[0]["UPDATE_USER_ID"]);
                        break;
                    case "UPDATE_TIME":
                        UPDATE_TIME = (string)(adoDataTable.Rows[0]["UPDATE_TIME"]);
                        break;
                    case "RES_UP_DOWN_FLAG":
                        RES_UP_DOWN_FLAG = (string)(adoDataTable.Rows[0]["RES_UP_DOWN_FLAG"]);
                        break;
                    case "RES_PRI_STS":
                        RES_PRI_STS = (string)(adoDataTable.Rows[0]["RES_PRI_STS"]);
                        break;
                    case "RES_STS_1":
                        RES_STS_1 = (string)(adoDataTable.Rows[0]["RES_STS_1"]);
                        break;
                    case "RES_STS_2":
                        RES_STS_2 = (string)(adoDataTable.Rows[0]["RES_STS_2"]);
                        break;
                    case "RES_STS_3":
                        RES_STS_3 = (string)(adoDataTable.Rows[0]["RES_STS_3"]);
                        break;
                    case "RES_STS_4":
                        RES_STS_4 = (string)(adoDataTable.Rows[0]["RES_STS_4"]);
                        break;
                    case "RES_STS_5":
                        RES_STS_5 = (string)(adoDataTable.Rows[0]["RES_STS_5"]);
                        break;
                    case "RES_STS_6":
                        RES_STS_6 = (string)(adoDataTable.Rows[0]["RES_STS_6"]);
                        break;
                    case "RES_STS_7":
                        RES_STS_7 = (string)(adoDataTable.Rows[0]["RES_STS_7"]);
                        break;
                    case "RES_STS_8":
                        RES_STS_8 = (string)(adoDataTable.Rows[0]["RES_STS_8"]);
                        break;
                    case "RES_STS_9":
                        RES_STS_9 = (string)(adoDataTable.Rows[0]["RES_STS_9"]);
                        break;
                    case "RES_STS_10":
                        RES_STS_10 = (string)(adoDataTable.Rows[0]["RES_STS_10"]);
                        break;
                    case "LOT_ID":
                        LOT_ID = (string)(adoDataTable.Rows[0]["LOT_ID"]);
                        break;
                    case "SUBLOT_ID":
                        SUBLOT_ID = (string)(adoDataTable.Rows[0]["SUBLOT_ID"]);
                        break;
                    case "CRR_ID":
                        CRR_ID = (string)(adoDataTable.Rows[0]["CRR_ID"]);
                        break;
                    case "RES_CTRL_MODE":
                        RES_CTRL_MODE = (string)(adoDataTable.Rows[0]["RES_CTRL_MODE"]);
                        break;
                    case "RES_PROC_MODE":
                        RES_PROC_MODE = (string)(adoDataTable.Rows[0]["RES_PROC_MODE"]);
                        break;
                    case "LAST_RECIPE_ID":
                        LAST_RECIPE_ID = (string)(adoDataTable.Rows[0]["LAST_RECIPE_ID"]);
                        break;
                    case "PROC_COUNT":
                        PROC_COUNT = Convert.ToInt32(adoDataTable.Rows[0]["PROC_COUNT"]);
                        break;
                    case "LAST_START_TIME":
                        LAST_START_TIME = (string)(adoDataTable.Rows[0]["LAST_START_TIME"]);
                        break;
                    case "LAST_END_TIME":
                        LAST_END_TIME = (string)(adoDataTable.Rows[0]["LAST_END_TIME"]);
                        break;
                    case "LAST_DOWN_TIME":
                        LAST_DOWN_TIME = (string)(adoDataTable.Rows[0]["LAST_DOWN_TIME"]);
                        break;
                    case "LAST_DOWN_HIST_SEQ":
                        LAST_DOWN_HIST_SEQ = Convert.ToInt32(adoDataTable.Rows[0]["LAST_DOWN_HIST_SEQ"]);
                        break;
                    case "LAST_EVENT_ID":
                        LAST_EVENT_ID = (string)(adoDataTable.Rows[0]["LAST_EVENT_ID"]);
                        break;
                    case "LAST_EVENT_TIME":
                        LAST_EVENT_TIME = (string)(adoDataTable.Rows[0]["LAST_EVENT_TIME"]);
                        break;
                    case "LAST_ACTIVE_HIST_SEQ":
                        LAST_ACTIVE_HIST_SEQ = Convert.ToInt32(adoDataTable.Rows[0]["LAST_ACTIVE_HIST_SEQ"]);
                        break;
                    case "LAST_HIST_SEQ":
                        LAST_HIST_SEQ = Convert.ToInt32(adoDataTable.Rows[0]["LAST_HIST_SEQ"]);
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
                        strQuery = "SELECT * FROM MRASRESDEF"
                            + " WITH (UPDLOCK) WHERE FACTORY=?"
                            + " AND RES_ID=?";
                    }
                    else if (_dbc.gDbType == (int)DB_TYPE.ORACLE)
                    {
                        strQuery = "SELECT * FROM MRASRESDEF"
                            + " WHERE FACTORY=?"
                            + " AND RES_ID=?"
                            + " FOR UPDATE";
                    }
                    else if (_dbc.gDbType == (int)DB_TYPE.DB2) 
                    {
                    }

                    // Add Parameters
                    adoAdapter.SelectCommand.Parameters.Add("@FACTORY", OleDbType.VarChar).Value = FACTORY;
                    adoAdapter.SelectCommand.Parameters.Add("@RES_ID", OleDbType.VarChar).Value = RES_ID;
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
                    case "FACTORY":
                        FACTORY = (string)(adoDataTable.Rows[0]["FACTORY"]);
                        break;
                    case "RES_ID":
                        RES_ID = (string)(adoDataTable.Rows[0]["RES_ID"]);
                        break;
                    case "RES_DESC":
                        RES_DESC = (string)(adoDataTable.Rows[0]["RES_DESC"]);
                        break;
                    case "RES_TYPE":
                        RES_TYPE = (string)(adoDataTable.Rows[0]["RES_TYPE"]);
                        break;
                    case "RES_GRP_1":
                        RES_GRP_1 = (string)(adoDataTable.Rows[0]["RES_GRP_1"]);
                        break;
                    case "RES_GRP_2":
                        RES_GRP_2 = (string)(adoDataTable.Rows[0]["RES_GRP_2"]);
                        break;
                    case "RES_GRP_3":
                        RES_GRP_3 = (string)(adoDataTable.Rows[0]["RES_GRP_3"]);
                        break;
                    case "RES_GRP_4":
                        RES_GRP_4 = (string)(adoDataTable.Rows[0]["RES_GRP_4"]);
                        break;
                    case "RES_GRP_5":
                        RES_GRP_5 = (string)(adoDataTable.Rows[0]["RES_GRP_5"]);
                        break;
                    case "RES_GRP_6":
                        RES_GRP_6 = (string)(adoDataTable.Rows[0]["RES_GRP_6"]);
                        break;
                    case "RES_GRP_7":
                        RES_GRP_7 = (string)(adoDataTable.Rows[0]["RES_GRP_7"]);
                        break;
                    case "RES_GRP_8":
                        RES_GRP_8 = (string)(adoDataTable.Rows[0]["RES_GRP_8"]);
                        break;
                    case "RES_GRP_9":
                        RES_GRP_9 = (string)(adoDataTable.Rows[0]["RES_GRP_9"]);
                        break;
                    case "RES_GRP_10":
                        RES_GRP_10 = (string)(adoDataTable.Rows[0]["RES_GRP_10"]);
                        break;
                    case "USE_FAC_PRT_FLAG":
                        USE_FAC_PRT_FLAG = (string)(adoDataTable.Rows[0]["USE_FAC_PRT_FLAG"]);
                        break;
                    case "RES_STS_PRT_1":
                        RES_STS_PRT_1 = (string)(adoDataTable.Rows[0]["RES_STS_PRT_1"]);
                        break;
                    case "RES_STS_PRT_2":
                        RES_STS_PRT_2 = (string)(adoDataTable.Rows[0]["RES_STS_PRT_2"]);
                        break;
                    case "RES_STS_PRT_3":
                        RES_STS_PRT_3 = (string)(adoDataTable.Rows[0]["RES_STS_PRT_3"]);
                        break;
                    case "RES_STS_PRT_4":
                        RES_STS_PRT_4 = (string)(adoDataTable.Rows[0]["RES_STS_PRT_4"]);
                        break;
                    case "RES_STS_PRT_5":
                        RES_STS_PRT_5 = (string)(adoDataTable.Rows[0]["RES_STS_PRT_5"]);
                        break;
                    case "RES_STS_PRT_6":
                        RES_STS_PRT_6 = (string)(adoDataTable.Rows[0]["RES_STS_PRT_6"]);
                        break;
                    case "RES_STS_PRT_7":
                        RES_STS_PRT_7 = (string)(adoDataTable.Rows[0]["RES_STS_PRT_7"]);
                        break;
                    case "RES_STS_PRT_8":
                        RES_STS_PRT_8 = (string)(adoDataTable.Rows[0]["RES_STS_PRT_8"]);
                        break;
                    case "RES_STS_PRT_9":
                        RES_STS_PRT_9 = (string)(adoDataTable.Rows[0]["RES_STS_PRT_9"]);
                        break;
                    case "RES_STS_PRT_10":
                        RES_STS_PRT_10 = (string)(adoDataTable.Rows[0]["RES_STS_PRT_10"]);
                        break;
                    case "RES_CMF_1":
                        RES_CMF_1 = (string)(adoDataTable.Rows[0]["RES_CMF_1"]);
                        break;
                    case "RES_CMF_2":
                        RES_CMF_2 = (string)(adoDataTable.Rows[0]["RES_CMF_2"]);
                        break;
                    case "RES_CMF_3":
                        RES_CMF_3 = (string)(adoDataTable.Rows[0]["RES_CMF_3"]);
                        break;
                    case "RES_CMF_4":
                        RES_CMF_4 = (string)(adoDataTable.Rows[0]["RES_CMF_4"]);
                        break;
                    case "RES_CMF_5":
                        RES_CMF_5 = (string)(adoDataTable.Rows[0]["RES_CMF_5"]);
                        break;
                    case "RES_CMF_6":
                        RES_CMF_6 = (string)(adoDataTable.Rows[0]["RES_CMF_6"]);
                        break;
                    case "RES_CMF_7":
                        RES_CMF_7 = (string)(adoDataTable.Rows[0]["RES_CMF_7"]);
                        break;
                    case "RES_CMF_8":
                        RES_CMF_8 = (string)(adoDataTable.Rows[0]["RES_CMF_8"]);
                        break;
                    case "RES_CMF_9":
                        RES_CMF_9 = (string)(adoDataTable.Rows[0]["RES_CMF_9"]);
                        break;
                    case "RES_CMF_10":
                        RES_CMF_10 = (string)(adoDataTable.Rows[0]["RES_CMF_10"]);
                        break;
                    case "RES_CMF_11":
                        RES_CMF_11 = (string)(adoDataTable.Rows[0]["RES_CMF_11"]);
                        break;
                    case "RES_CMF_12":
                        RES_CMF_12 = (string)(adoDataTable.Rows[0]["RES_CMF_12"]);
                        break;
                    case "RES_CMF_13":
                        RES_CMF_13 = (string)(adoDataTable.Rows[0]["RES_CMF_13"]);
                        break;
                    case "RES_CMF_14":
                        RES_CMF_14 = (string)(adoDataTable.Rows[0]["RES_CMF_14"]);
                        break;
                    case "RES_CMF_15":
                        RES_CMF_15 = (string)(adoDataTable.Rows[0]["RES_CMF_15"]);
                        break;
                    case "RES_CMF_16":
                        RES_CMF_16 = (string)(adoDataTable.Rows[0]["RES_CMF_16"]);
                        break;
                    case "RES_CMF_17":
                        RES_CMF_17 = (string)(adoDataTable.Rows[0]["RES_CMF_17"]);
                        break;
                    case "RES_CMF_18":
                        RES_CMF_18 = (string)(adoDataTable.Rows[0]["RES_CMF_18"]);
                        break;
                    case "RES_CMF_19":
                        RES_CMF_19 = (string)(adoDataTable.Rows[0]["RES_CMF_19"]);
                        break;
                    case "RES_CMF_20":
                        RES_CMF_20 = (string)(adoDataTable.Rows[0]["RES_CMF_20"]);
                        break;
                    case "AREA_ID":
                        AREA_ID = (string)(adoDataTable.Rows[0]["AREA_ID"]);
                        break;
                    case "SUB_AREA_ID":
                        SUB_AREA_ID = (string)(adoDataTable.Rows[0]["SUB_AREA_ID"]);
                        break;
                    case "RES_LOCATION":
                        RES_LOCATION = (string)(adoDataTable.Rows[0]["RES_LOCATION"]);
                        break;
                    case "PROC_RULE":
                        PROC_RULE = (string)(adoDataTable.Rows[0]["PROC_RULE"]);
                        break;
                    case "MAX_PROC_COUNT":
                        MAX_PROC_COUNT = Convert.ToInt32(adoDataTable.Rows[0]["MAX_PROC_COUNT"]);
                        break;
                    case "BATCH_COND_1":
                        BATCH_COND_1 = (string)(adoDataTable.Rows[0]["BATCH_COND_1"]);
                        break;
                    case "BATCH_COND_2":
                        BATCH_COND_2 = (string)(adoDataTable.Rows[0]["BATCH_COND_2"]);
                        break;
                    case "PM_SCH_ENABLE_FLAG":
                        PM_SCH_ENABLE_FLAG = (string)(adoDataTable.Rows[0]["PM_SCH_ENABLE_FLAG"]);
                        break;
                    case "UNIT_BASE_ST_FLAG":
                        UNIT_BASE_ST_FLAG = (string)(adoDataTable.Rows[0]["UNIT_BASE_ST_FLAG"]);
                        break;
                    case "SEC_CHK_FLAG":
                        SEC_CHK_FLAG = (string)(adoDataTable.Rows[0]["SEC_CHK_FLAG"]);
                        break;
                    case "GATHER_ALARM_FLAG":
                        GATHER_ALARM_FLAG = (string)(adoDataTable.Rows[0]["GATHER_ALARM_FLAG"]);
                        break;
                    case "DELETE_FLAG":
                        DELETE_FLAG = (string)(adoDataTable.Rows[0]["DELETE_FLAG"]);
                        break;
                    case "DELETE_USER_ID":
                        DELETE_USER_ID = (string)(adoDataTable.Rows[0]["DELETE_USER_ID"]);
                        break;
                    case "DELETE_TIME":
                        DELETE_TIME = (string)(adoDataTable.Rows[0]["DELETE_TIME"]);
                        break;
                    case "CREATE_USER_ID":
                        CREATE_USER_ID = (string)(adoDataTable.Rows[0]["CREATE_USER_ID"]);
                        break;
                    case "CREATE_TIME":
                        CREATE_TIME = (string)(adoDataTable.Rows[0]["CREATE_TIME"]);
                        break;
                    case "UPDATE_USER_ID":
                        UPDATE_USER_ID = (string)(adoDataTable.Rows[0]["UPDATE_USER_ID"]);
                        break;
                    case "UPDATE_TIME":
                        UPDATE_TIME = (string)(adoDataTable.Rows[0]["UPDATE_TIME"]);
                        break;
                    case "RES_UP_DOWN_FLAG":
                        RES_UP_DOWN_FLAG = (string)(adoDataTable.Rows[0]["RES_UP_DOWN_FLAG"]);
                        break;
                    case "RES_PRI_STS":
                        RES_PRI_STS = (string)(adoDataTable.Rows[0]["RES_PRI_STS"]);
                        break;
                    case "RES_STS_1":
                        RES_STS_1 = (string)(adoDataTable.Rows[0]["RES_STS_1"]);
                        break;
                    case "RES_STS_2":
                        RES_STS_2 = (string)(adoDataTable.Rows[0]["RES_STS_2"]);
                        break;
                    case "RES_STS_3":
                        RES_STS_3 = (string)(adoDataTable.Rows[0]["RES_STS_3"]);
                        break;
                    case "RES_STS_4":
                        RES_STS_4 = (string)(adoDataTable.Rows[0]["RES_STS_4"]);
                        break;
                    case "RES_STS_5":
                        RES_STS_5 = (string)(adoDataTable.Rows[0]["RES_STS_5"]);
                        break;
                    case "RES_STS_6":
                        RES_STS_6 = (string)(adoDataTable.Rows[0]["RES_STS_6"]);
                        break;
                    case "RES_STS_7":
                        RES_STS_7 = (string)(adoDataTable.Rows[0]["RES_STS_7"]);
                        break;
                    case "RES_STS_8":
                        RES_STS_8 = (string)(adoDataTable.Rows[0]["RES_STS_8"]);
                        break;
                    case "RES_STS_9":
                        RES_STS_9 = (string)(adoDataTable.Rows[0]["RES_STS_9"]);
                        break;
                    case "RES_STS_10":
                        RES_STS_10 = (string)(adoDataTable.Rows[0]["RES_STS_10"]);
                        break;
                    case "LOT_ID":
                        LOT_ID = (string)(adoDataTable.Rows[0]["LOT_ID"]);
                        break;
                    case "SUBLOT_ID":
                        SUBLOT_ID = (string)(adoDataTable.Rows[0]["SUBLOT_ID"]);
                        break;
                    case "CRR_ID":
                        CRR_ID = (string)(adoDataTable.Rows[0]["CRR_ID"]);
                        break;
                    case "RES_CTRL_MODE":
                        RES_CTRL_MODE = (string)(adoDataTable.Rows[0]["RES_CTRL_MODE"]);
                        break;
                    case "RES_PROC_MODE":
                        RES_PROC_MODE = (string)(adoDataTable.Rows[0]["RES_PROC_MODE"]);
                        break;
                    case "LAST_RECIPE_ID":
                        LAST_RECIPE_ID = (string)(adoDataTable.Rows[0]["LAST_RECIPE_ID"]);
                        break;
                    case "PROC_COUNT":
                        PROC_COUNT = Convert.ToInt32(adoDataTable.Rows[0]["PROC_COUNT"]);
                        break;
                    case "LAST_START_TIME":
                        LAST_START_TIME = (string)(adoDataTable.Rows[0]["LAST_START_TIME"]);
                        break;
                    case "LAST_END_TIME":
                        LAST_END_TIME = (string)(adoDataTable.Rows[0]["LAST_END_TIME"]);
                        break;
                    case "LAST_DOWN_TIME":
                        LAST_DOWN_TIME = (string)(adoDataTable.Rows[0]["LAST_DOWN_TIME"]);
                        break;
                    case "LAST_DOWN_HIST_SEQ":
                        LAST_DOWN_HIST_SEQ = Convert.ToInt32(adoDataTable.Rows[0]["LAST_DOWN_HIST_SEQ"]);
                        break;
                    case "LAST_EVENT_ID":
                        LAST_EVENT_ID = (string)(adoDataTable.Rows[0]["LAST_EVENT_ID"]);
                        break;
                    case "LAST_EVENT_TIME":
                        LAST_EVENT_TIME = (string)(adoDataTable.Rows[0]["LAST_EVENT_TIME"]);
                        break;
                    case "LAST_ACTIVE_HIST_SEQ":
                        LAST_ACTIVE_HIST_SEQ = Convert.ToInt32(adoDataTable.Rows[0]["LAST_ACTIVE_HIST_SEQ"]);
                        break;
                    case "LAST_HIST_SEQ":
                        LAST_HIST_SEQ = Convert.ToInt32(adoDataTable.Rows[0]["LAST_HIST_SEQ"]);
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
                    strQuery = "SELECT COUNT(*) FROM MRASRESDEF"
                        + " WHERE FACTORY=?"
                        + " AND RES_ID=?";

                    // Add Parameters
                    adoCommand.Parameters.Add("@FACTORY", OleDbType.VarChar).Value = FACTORY;
                    adoCommand.Parameters.Add("@RES_ID", OleDbType.VarChar).Value = RES_ID;
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
                    strQuery = "DELETE FROM MRASRESDEF"
                        + " WHERE FACTORY=?"
                        + " AND RES_ID=?";

                    // Add Parameters
                    adoCommand.Parameters.Add("@FACTORY", OleDbType.VarChar).Value = FACTORY;
                    adoCommand.Parameters.Add("@RES_ID", OleDbType.VarChar).Value = RES_ID;
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

            strQuery = "INSERT INTO MRASRESDEF ("
                + " FACTORY, RES_ID, RES_DESC, RES_TYPE, RES_GRP_1,"
                + " RES_GRP_2, RES_GRP_3, RES_GRP_4, RES_GRP_5, RES_GRP_6,"
                + " RES_GRP_7, RES_GRP_8, RES_GRP_9, RES_GRP_10, USE_FAC_PRT_FLAG,"
                + " RES_STS_PRT_1, RES_STS_PRT_2, RES_STS_PRT_3, RES_STS_PRT_4, RES_STS_PRT_5,"
                + " RES_STS_PRT_6, RES_STS_PRT_7, RES_STS_PRT_8, RES_STS_PRT_9, RES_STS_PRT_10,"
                + " RES_CMF_1, RES_CMF_2, RES_CMF_3, RES_CMF_4, RES_CMF_5,"
                + " RES_CMF_6, RES_CMF_7, RES_CMF_8, RES_CMF_9, RES_CMF_10,"
                + " RES_CMF_11, RES_CMF_12, RES_CMF_13, RES_CMF_14, RES_CMF_15,"
                + " RES_CMF_16, RES_CMF_17, RES_CMF_18, RES_CMF_19, RES_CMF_20,"
                + " AREA_ID, SUB_AREA_ID, RES_LOCATION, PROC_RULE, MAX_PROC_COUNT,"
                + " BATCH_COND_1, BATCH_COND_2, PM_SCH_ENABLE_FLAG, UNIT_BASE_ST_FLAG, SEC_CHK_FLAG,"
                + " GATHER_ALARM_FLAG, DELETE_FLAG, DELETE_USER_ID, DELETE_TIME, CREATE_USER_ID,"
                + " CREATE_TIME, UPDATE_USER_ID, UPDATE_TIME, RES_UP_DOWN_FLAG, RES_PRI_STS,"
                + " RES_STS_1, RES_STS_2, RES_STS_3, RES_STS_4, RES_STS_5,"
                + " RES_STS_6, RES_STS_7, RES_STS_8, RES_STS_9, RES_STS_10,"
                + " LOT_ID, SUBLOT_ID, CRR_ID, RES_CTRL_MODE, RES_PROC_MODE,"
                + " LAST_RECIPE_ID, PROC_COUNT, LAST_START_TIME, LAST_END_TIME, LAST_DOWN_TIME,"
                + " LAST_DOWN_HIST_SEQ, LAST_EVENT_ID, LAST_EVENT_TIME, LAST_ACTIVE_HIST_SEQ, LAST_HIST_SEQ)"
                + " VALUES ("
                + " ?, ?, ?, ?, ?, ?, ?, ?, ?, ?,"
                + " ?, ?, ?, ?, ?, ?, ?, ?, ?, ?,"
                + " ?, ?, ?, ?, ?, ?, ?, ?, ?, ?,"
                + " ?, ?, ?, ?, ?, ?, ?, ?, ?, ?,"
                + " ?, ?, ?, ?, ?, ?, ?, ?, ?, ?,"
                + " ?, ?, ?, ?, ?, ?, ?, ?, ?, ?,"
                + " ?, ?, ?, ?, ?, ?, ?, ?, ?, ?,"
                + " ?, ?, ?, ?, ?, ?, ?, ?, ?, ?,"
                + " ?, ?, ?, ?, ?, ?, ?, ?, ?, ?)";

            // Add Parameters
            adoCommand.Parameters.Add("@FACTORY", OleDbType.VarChar).Value = FACTORY;
            adoCommand.Parameters.Add("@RES_ID", OleDbType.VarChar).Value = RES_ID;
            adoCommand.Parameters.Add("@RES_DESC", OleDbType.VarChar).Value = RES_DESC;
            adoCommand.Parameters.Add("@RES_TYPE", OleDbType.VarChar).Value = RES_TYPE;
            adoCommand.Parameters.Add("@RES_GRP_1", OleDbType.VarChar).Value = RES_GRP_1;
            adoCommand.Parameters.Add("@RES_GRP_2", OleDbType.VarChar).Value = RES_GRP_2;
            adoCommand.Parameters.Add("@RES_GRP_3", OleDbType.VarChar).Value = RES_GRP_3;
            adoCommand.Parameters.Add("@RES_GRP_4", OleDbType.VarChar).Value = RES_GRP_4;
            adoCommand.Parameters.Add("@RES_GRP_5", OleDbType.VarChar).Value = RES_GRP_5;
            adoCommand.Parameters.Add("@RES_GRP_6", OleDbType.VarChar).Value = RES_GRP_6;
            adoCommand.Parameters.Add("@RES_GRP_7", OleDbType.VarChar).Value = RES_GRP_7;
            adoCommand.Parameters.Add("@RES_GRP_8", OleDbType.VarChar).Value = RES_GRP_8;
            adoCommand.Parameters.Add("@RES_GRP_9", OleDbType.VarChar).Value = RES_GRP_9;
            adoCommand.Parameters.Add("@RES_GRP_10", OleDbType.VarChar).Value = RES_GRP_10;
            adoCommand.Parameters.Add("@USE_FAC_PRT_FLAG", OleDbType.VarChar).Value = USE_FAC_PRT_FLAG;
            adoCommand.Parameters.Add("@RES_STS_PRT_1", OleDbType.VarChar).Value = RES_STS_PRT_1;
            adoCommand.Parameters.Add("@RES_STS_PRT_2", OleDbType.VarChar).Value = RES_STS_PRT_2;
            adoCommand.Parameters.Add("@RES_STS_PRT_3", OleDbType.VarChar).Value = RES_STS_PRT_3;
            adoCommand.Parameters.Add("@RES_STS_PRT_4", OleDbType.VarChar).Value = RES_STS_PRT_4;
            adoCommand.Parameters.Add("@RES_STS_PRT_5", OleDbType.VarChar).Value = RES_STS_PRT_5;
            adoCommand.Parameters.Add("@RES_STS_PRT_6", OleDbType.VarChar).Value = RES_STS_PRT_6;
            adoCommand.Parameters.Add("@RES_STS_PRT_7", OleDbType.VarChar).Value = RES_STS_PRT_7;
            adoCommand.Parameters.Add("@RES_STS_PRT_8", OleDbType.VarChar).Value = RES_STS_PRT_8;
            adoCommand.Parameters.Add("@RES_STS_PRT_9", OleDbType.VarChar).Value = RES_STS_PRT_9;
            adoCommand.Parameters.Add("@RES_STS_PRT_10", OleDbType.VarChar).Value = RES_STS_PRT_10;
            adoCommand.Parameters.Add("@RES_CMF_1", OleDbType.VarChar).Value = RES_CMF_1;
            adoCommand.Parameters.Add("@RES_CMF_2", OleDbType.VarChar).Value = RES_CMF_2;
            adoCommand.Parameters.Add("@RES_CMF_3", OleDbType.VarChar).Value = RES_CMF_3;
            adoCommand.Parameters.Add("@RES_CMF_4", OleDbType.VarChar).Value = RES_CMF_4;
            adoCommand.Parameters.Add("@RES_CMF_5", OleDbType.VarChar).Value = RES_CMF_5;
            adoCommand.Parameters.Add("@RES_CMF_6", OleDbType.VarChar).Value = RES_CMF_6;
            adoCommand.Parameters.Add("@RES_CMF_7", OleDbType.VarChar).Value = RES_CMF_7;
            adoCommand.Parameters.Add("@RES_CMF_8", OleDbType.VarChar).Value = RES_CMF_8;
            adoCommand.Parameters.Add("@RES_CMF_9", OleDbType.VarChar).Value = RES_CMF_9;
            adoCommand.Parameters.Add("@RES_CMF_10", OleDbType.VarChar).Value = RES_CMF_10;
            adoCommand.Parameters.Add("@RES_CMF_11", OleDbType.VarChar).Value = RES_CMF_11;
            adoCommand.Parameters.Add("@RES_CMF_12", OleDbType.VarChar).Value = RES_CMF_12;
            adoCommand.Parameters.Add("@RES_CMF_13", OleDbType.VarChar).Value = RES_CMF_13;
            adoCommand.Parameters.Add("@RES_CMF_14", OleDbType.VarChar).Value = RES_CMF_14;
            adoCommand.Parameters.Add("@RES_CMF_15", OleDbType.VarChar).Value = RES_CMF_15;
            adoCommand.Parameters.Add("@RES_CMF_16", OleDbType.VarChar).Value = RES_CMF_16;
            adoCommand.Parameters.Add("@RES_CMF_17", OleDbType.VarChar).Value = RES_CMF_17;
            adoCommand.Parameters.Add("@RES_CMF_18", OleDbType.VarChar).Value = RES_CMF_18;
            adoCommand.Parameters.Add("@RES_CMF_19", OleDbType.VarChar).Value = RES_CMF_19;
            adoCommand.Parameters.Add("@RES_CMF_20", OleDbType.VarChar).Value = RES_CMF_20;
            adoCommand.Parameters.Add("@AREA_ID", OleDbType.VarChar).Value = AREA_ID;
            adoCommand.Parameters.Add("@SUB_AREA_ID", OleDbType.VarChar).Value = SUB_AREA_ID;
            adoCommand.Parameters.Add("@RES_LOCATION", OleDbType.VarChar).Value = RES_LOCATION;
            adoCommand.Parameters.Add("@PROC_RULE", OleDbType.VarChar).Value = PROC_RULE;
            adoCommand.Parameters.Add("@MAX_PROC_COUNT", OleDbType.Numeric).Value = MAX_PROC_COUNT;
            adoCommand.Parameters.Add("@BATCH_COND_1", OleDbType.VarChar).Value = BATCH_COND_1;
            adoCommand.Parameters.Add("@BATCH_COND_2", OleDbType.VarChar).Value = BATCH_COND_2;
            adoCommand.Parameters.Add("@PM_SCH_ENABLE_FLAG", OleDbType.VarChar).Value = PM_SCH_ENABLE_FLAG;
            adoCommand.Parameters.Add("@UNIT_BASE_ST_FLAG", OleDbType.VarChar).Value = UNIT_BASE_ST_FLAG;
            adoCommand.Parameters.Add("@SEC_CHK_FLAG", OleDbType.VarChar).Value = SEC_CHK_FLAG;
            adoCommand.Parameters.Add("@GATHER_ALARM_FLAG", OleDbType.VarChar).Value = GATHER_ALARM_FLAG;
            adoCommand.Parameters.Add("@DELETE_FLAG", OleDbType.VarChar).Value = DELETE_FLAG;
            adoCommand.Parameters.Add("@DELETE_USER_ID", OleDbType.VarChar).Value = DELETE_USER_ID;
            adoCommand.Parameters.Add("@DELETE_TIME", OleDbType.VarChar).Value = DELETE_TIME;
            adoCommand.Parameters.Add("@CREATE_USER_ID", OleDbType.VarChar).Value = CREATE_USER_ID;
            adoCommand.Parameters.Add("@CREATE_TIME", OleDbType.VarChar).Value = CREATE_TIME;
            adoCommand.Parameters.Add("@UPDATE_USER_ID", OleDbType.VarChar).Value = UPDATE_USER_ID;
            adoCommand.Parameters.Add("@UPDATE_TIME", OleDbType.VarChar).Value = UPDATE_TIME;
            adoCommand.Parameters.Add("@RES_UP_DOWN_FLAG", OleDbType.VarChar).Value = RES_UP_DOWN_FLAG;
            adoCommand.Parameters.Add("@RES_PRI_STS", OleDbType.VarChar).Value = RES_PRI_STS;
            adoCommand.Parameters.Add("@RES_STS_1", OleDbType.VarChar).Value = RES_STS_1;
            adoCommand.Parameters.Add("@RES_STS_2", OleDbType.VarChar).Value = RES_STS_2;
            adoCommand.Parameters.Add("@RES_STS_3", OleDbType.VarChar).Value = RES_STS_3;
            adoCommand.Parameters.Add("@RES_STS_4", OleDbType.VarChar).Value = RES_STS_4;
            adoCommand.Parameters.Add("@RES_STS_5", OleDbType.VarChar).Value = RES_STS_5;
            adoCommand.Parameters.Add("@RES_STS_6", OleDbType.VarChar).Value = RES_STS_6;
            adoCommand.Parameters.Add("@RES_STS_7", OleDbType.VarChar).Value = RES_STS_7;
            adoCommand.Parameters.Add("@RES_STS_8", OleDbType.VarChar).Value = RES_STS_8;
            adoCommand.Parameters.Add("@RES_STS_9", OleDbType.VarChar).Value = RES_STS_9;
            adoCommand.Parameters.Add("@RES_STS_10", OleDbType.VarChar).Value = RES_STS_10;
            adoCommand.Parameters.Add("@LOT_ID", OleDbType.VarChar).Value = LOT_ID;
            adoCommand.Parameters.Add("@SUBLOT_ID", OleDbType.VarChar).Value = SUBLOT_ID;
            adoCommand.Parameters.Add("@CRR_ID", OleDbType.VarChar).Value = CRR_ID;
            adoCommand.Parameters.Add("@RES_CTRL_MODE", OleDbType.VarChar).Value = RES_CTRL_MODE;
            adoCommand.Parameters.Add("@RES_PROC_MODE", OleDbType.VarChar).Value = RES_PROC_MODE;
            adoCommand.Parameters.Add("@LAST_RECIPE_ID", OleDbType.VarChar).Value = LAST_RECIPE_ID;
            adoCommand.Parameters.Add("@PROC_COUNT", OleDbType.Numeric).Value = PROC_COUNT;
            adoCommand.Parameters.Add("@LAST_START_TIME", OleDbType.VarChar).Value = LAST_START_TIME;
            adoCommand.Parameters.Add("@LAST_END_TIME", OleDbType.VarChar).Value = LAST_END_TIME;
            adoCommand.Parameters.Add("@LAST_DOWN_TIME", OleDbType.VarChar).Value = LAST_DOWN_TIME;
            adoCommand.Parameters.Add("@LAST_DOWN_HIST_SEQ", OleDbType.Numeric).Value = LAST_DOWN_HIST_SEQ;
            adoCommand.Parameters.Add("@LAST_EVENT_ID", OleDbType.VarChar).Value = LAST_EVENT_ID;
            adoCommand.Parameters.Add("@LAST_EVENT_TIME", OleDbType.VarChar).Value = LAST_EVENT_TIME;
            adoCommand.Parameters.Add("@LAST_ACTIVE_HIST_SEQ", OleDbType.Numeric).Value = LAST_ACTIVE_HIST_SEQ;
            adoCommand.Parameters.Add("@LAST_HIST_SEQ", OleDbType.Numeric).Value = LAST_HIST_SEQ;

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
                    strQuery = "UPDATE MRASRESDEF SET"
                        + " RES_DESC=?, RES_TYPE=?, RES_GRP_1=?, RES_GRP_2=?, RES_GRP_3=?,"
                        + " RES_GRP_4=?, RES_GRP_5=?, RES_GRP_6=?, RES_GRP_7=?, RES_GRP_8=?,"
                        + " RES_GRP_9=?, RES_GRP_10=?, USE_FAC_PRT_FLAG=?, RES_STS_PRT_1=?, RES_STS_PRT_2=?,"
                        + " RES_STS_PRT_3=?, RES_STS_PRT_4=?, RES_STS_PRT_5=?, RES_STS_PRT_6=?, RES_STS_PRT_7=?,"
                        + " RES_STS_PRT_8=?, RES_STS_PRT_9=?, RES_STS_PRT_10=?, RES_CMF_1=?, RES_CMF_2=?,"
                        + " RES_CMF_3=?, RES_CMF_4=?, RES_CMF_5=?, RES_CMF_6=?, RES_CMF_7=?,"
                        + " RES_CMF_8=?, RES_CMF_9=?, RES_CMF_10=?, RES_CMF_11=?, RES_CMF_12=?,"
                        + " RES_CMF_13=?, RES_CMF_14=?, RES_CMF_15=?, RES_CMF_16=?, RES_CMF_17=?,"
                        + " RES_CMF_18=?, RES_CMF_19=?, RES_CMF_20=?, AREA_ID=?, SUB_AREA_ID=?,"
                        + " RES_LOCATION=?, PROC_RULE=?, MAX_PROC_COUNT=?, BATCH_COND_1=?, BATCH_COND_2=?,"
                        + " PM_SCH_ENABLE_FLAG=?, UNIT_BASE_ST_FLAG=?, SEC_CHK_FLAG=?, GATHER_ALARM_FLAG=?, DELETE_FLAG=?,"
                        + " DELETE_USER_ID=?, DELETE_TIME=?, CREATE_USER_ID=?, CREATE_TIME=?, UPDATE_USER_ID=?,"
                        + " UPDATE_TIME=?, RES_UP_DOWN_FLAG=?, RES_PRI_STS=?, RES_STS_1=?, RES_STS_2=?,"
                        + " RES_STS_3=?, RES_STS_4=?, RES_STS_5=?, RES_STS_6=?, RES_STS_7=?,"
                        + " RES_STS_8=?, RES_STS_9=?, RES_STS_10=?, LOT_ID=?, SUBLOT_ID=?,"
                        + " CRR_ID=?, RES_CTRL_MODE=?, RES_PROC_MODE=?, LAST_RECIPE_ID=?, PROC_COUNT=?,"
                        + " LAST_START_TIME=?, LAST_END_TIME=?, LAST_DOWN_TIME=?, LAST_DOWN_HIST_SEQ=?, LAST_EVENT_ID=?,"
                        + " LAST_EVENT_TIME=?, LAST_ACTIVE_HIST_SEQ=?, LAST_HIST_SEQ=?"
                        + " WHERE FACTORY=?"
                        + " AND RES_ID=?";

                    // Add Parameters
                    adoCommand.Parameters.Add("@RES_DESC", OleDbType.VarChar).Value = RES_DESC;
                    adoCommand.Parameters.Add("@RES_TYPE", OleDbType.VarChar).Value = RES_TYPE;
                    adoCommand.Parameters.Add("@RES_GRP_1", OleDbType.VarChar).Value = RES_GRP_1;
                    adoCommand.Parameters.Add("@RES_GRP_2", OleDbType.VarChar).Value = RES_GRP_2;
                    adoCommand.Parameters.Add("@RES_GRP_3", OleDbType.VarChar).Value = RES_GRP_3;
                    adoCommand.Parameters.Add("@RES_GRP_4", OleDbType.VarChar).Value = RES_GRP_4;
                    adoCommand.Parameters.Add("@RES_GRP_5", OleDbType.VarChar).Value = RES_GRP_5;
                    adoCommand.Parameters.Add("@RES_GRP_6", OleDbType.VarChar).Value = RES_GRP_6;
                    adoCommand.Parameters.Add("@RES_GRP_7", OleDbType.VarChar).Value = RES_GRP_7;
                    adoCommand.Parameters.Add("@RES_GRP_8", OleDbType.VarChar).Value = RES_GRP_8;
                    adoCommand.Parameters.Add("@RES_GRP_9", OleDbType.VarChar).Value = RES_GRP_9;
                    adoCommand.Parameters.Add("@RES_GRP_10", OleDbType.VarChar).Value = RES_GRP_10;
                    adoCommand.Parameters.Add("@USE_FAC_PRT_FLAG", OleDbType.VarChar).Value = USE_FAC_PRT_FLAG;
                    adoCommand.Parameters.Add("@RES_STS_PRT_1", OleDbType.VarChar).Value = RES_STS_PRT_1;
                    adoCommand.Parameters.Add("@RES_STS_PRT_2", OleDbType.VarChar).Value = RES_STS_PRT_2;
                    adoCommand.Parameters.Add("@RES_STS_PRT_3", OleDbType.VarChar).Value = RES_STS_PRT_3;
                    adoCommand.Parameters.Add("@RES_STS_PRT_4", OleDbType.VarChar).Value = RES_STS_PRT_4;
                    adoCommand.Parameters.Add("@RES_STS_PRT_5", OleDbType.VarChar).Value = RES_STS_PRT_5;
                    adoCommand.Parameters.Add("@RES_STS_PRT_6", OleDbType.VarChar).Value = RES_STS_PRT_6;
                    adoCommand.Parameters.Add("@RES_STS_PRT_7", OleDbType.VarChar).Value = RES_STS_PRT_7;
                    adoCommand.Parameters.Add("@RES_STS_PRT_8", OleDbType.VarChar).Value = RES_STS_PRT_8;
                    adoCommand.Parameters.Add("@RES_STS_PRT_9", OleDbType.VarChar).Value = RES_STS_PRT_9;
                    adoCommand.Parameters.Add("@RES_STS_PRT_10", OleDbType.VarChar).Value = RES_STS_PRT_10;
                    adoCommand.Parameters.Add("@RES_CMF_1", OleDbType.VarChar).Value = RES_CMF_1;
                    adoCommand.Parameters.Add("@RES_CMF_2", OleDbType.VarChar).Value = RES_CMF_2;
                    adoCommand.Parameters.Add("@RES_CMF_3", OleDbType.VarChar).Value = RES_CMF_3;
                    adoCommand.Parameters.Add("@RES_CMF_4", OleDbType.VarChar).Value = RES_CMF_4;
                    adoCommand.Parameters.Add("@RES_CMF_5", OleDbType.VarChar).Value = RES_CMF_5;
                    adoCommand.Parameters.Add("@RES_CMF_6", OleDbType.VarChar).Value = RES_CMF_6;
                    adoCommand.Parameters.Add("@RES_CMF_7", OleDbType.VarChar).Value = RES_CMF_7;
                    adoCommand.Parameters.Add("@RES_CMF_8", OleDbType.VarChar).Value = RES_CMF_8;
                    adoCommand.Parameters.Add("@RES_CMF_9", OleDbType.VarChar).Value = RES_CMF_9;
                    adoCommand.Parameters.Add("@RES_CMF_10", OleDbType.VarChar).Value = RES_CMF_10;
                    adoCommand.Parameters.Add("@RES_CMF_11", OleDbType.VarChar).Value = RES_CMF_11;
                    adoCommand.Parameters.Add("@RES_CMF_12", OleDbType.VarChar).Value = RES_CMF_12;
                    adoCommand.Parameters.Add("@RES_CMF_13", OleDbType.VarChar).Value = RES_CMF_13;
                    adoCommand.Parameters.Add("@RES_CMF_14", OleDbType.VarChar).Value = RES_CMF_14;
                    adoCommand.Parameters.Add("@RES_CMF_15", OleDbType.VarChar).Value = RES_CMF_15;
                    adoCommand.Parameters.Add("@RES_CMF_16", OleDbType.VarChar).Value = RES_CMF_16;
                    adoCommand.Parameters.Add("@RES_CMF_17", OleDbType.VarChar).Value = RES_CMF_17;
                    adoCommand.Parameters.Add("@RES_CMF_18", OleDbType.VarChar).Value = RES_CMF_18;
                    adoCommand.Parameters.Add("@RES_CMF_19", OleDbType.VarChar).Value = RES_CMF_19;
                    adoCommand.Parameters.Add("@RES_CMF_20", OleDbType.VarChar).Value = RES_CMF_20;
                    adoCommand.Parameters.Add("@AREA_ID", OleDbType.VarChar).Value = AREA_ID;
                    adoCommand.Parameters.Add("@SUB_AREA_ID", OleDbType.VarChar).Value = SUB_AREA_ID;
                    adoCommand.Parameters.Add("@RES_LOCATION", OleDbType.VarChar).Value = RES_LOCATION;
                    adoCommand.Parameters.Add("@PROC_RULE", OleDbType.VarChar).Value = PROC_RULE;
                    adoCommand.Parameters.Add("@MAX_PROC_COUNT", OleDbType.Numeric).Value = MAX_PROC_COUNT;
                    adoCommand.Parameters.Add("@BATCH_COND_1", OleDbType.VarChar).Value = BATCH_COND_1;
                    adoCommand.Parameters.Add("@BATCH_COND_2", OleDbType.VarChar).Value = BATCH_COND_2;
                    adoCommand.Parameters.Add("@PM_SCH_ENABLE_FLAG", OleDbType.VarChar).Value = PM_SCH_ENABLE_FLAG;
                    adoCommand.Parameters.Add("@UNIT_BASE_ST_FLAG", OleDbType.VarChar).Value = UNIT_BASE_ST_FLAG;
                    adoCommand.Parameters.Add("@SEC_CHK_FLAG", OleDbType.VarChar).Value = SEC_CHK_FLAG;
                    adoCommand.Parameters.Add("@GATHER_ALARM_FLAG", OleDbType.VarChar).Value = GATHER_ALARM_FLAG;
                    adoCommand.Parameters.Add("@DELETE_FLAG", OleDbType.VarChar).Value = DELETE_FLAG;
                    adoCommand.Parameters.Add("@DELETE_USER_ID", OleDbType.VarChar).Value = DELETE_USER_ID;
                    adoCommand.Parameters.Add("@DELETE_TIME", OleDbType.VarChar).Value = DELETE_TIME;
                    adoCommand.Parameters.Add("@CREATE_USER_ID", OleDbType.VarChar).Value = CREATE_USER_ID;
                    adoCommand.Parameters.Add("@CREATE_TIME", OleDbType.VarChar).Value = CREATE_TIME;
                    adoCommand.Parameters.Add("@UPDATE_USER_ID", OleDbType.VarChar).Value = UPDATE_USER_ID;
                    adoCommand.Parameters.Add("@UPDATE_TIME", OleDbType.VarChar).Value = UPDATE_TIME;
                    adoCommand.Parameters.Add("@RES_UP_DOWN_FLAG", OleDbType.VarChar).Value = RES_UP_DOWN_FLAG;
                    adoCommand.Parameters.Add("@RES_PRI_STS", OleDbType.VarChar).Value = RES_PRI_STS;
                    adoCommand.Parameters.Add("@RES_STS_1", OleDbType.VarChar).Value = RES_STS_1;
                    adoCommand.Parameters.Add("@RES_STS_2", OleDbType.VarChar).Value = RES_STS_2;
                    adoCommand.Parameters.Add("@RES_STS_3", OleDbType.VarChar).Value = RES_STS_3;
                    adoCommand.Parameters.Add("@RES_STS_4", OleDbType.VarChar).Value = RES_STS_4;
                    adoCommand.Parameters.Add("@RES_STS_5", OleDbType.VarChar).Value = RES_STS_5;
                    adoCommand.Parameters.Add("@RES_STS_6", OleDbType.VarChar).Value = RES_STS_6;
                    adoCommand.Parameters.Add("@RES_STS_7", OleDbType.VarChar).Value = RES_STS_7;
                    adoCommand.Parameters.Add("@RES_STS_8", OleDbType.VarChar).Value = RES_STS_8;
                    adoCommand.Parameters.Add("@RES_STS_9", OleDbType.VarChar).Value = RES_STS_9;
                    adoCommand.Parameters.Add("@RES_STS_10", OleDbType.VarChar).Value = RES_STS_10;
                    adoCommand.Parameters.Add("@LOT_ID", OleDbType.VarChar).Value = LOT_ID;
                    adoCommand.Parameters.Add("@SUBLOT_ID", OleDbType.VarChar).Value = SUBLOT_ID;
                    adoCommand.Parameters.Add("@CRR_ID", OleDbType.VarChar).Value = CRR_ID;
                    adoCommand.Parameters.Add("@RES_CTRL_MODE", OleDbType.VarChar).Value = RES_CTRL_MODE;
                    adoCommand.Parameters.Add("@RES_PROC_MODE", OleDbType.VarChar).Value = RES_PROC_MODE;
                    adoCommand.Parameters.Add("@LAST_RECIPE_ID", OleDbType.VarChar).Value = LAST_RECIPE_ID;
                    adoCommand.Parameters.Add("@PROC_COUNT", OleDbType.Numeric).Value = PROC_COUNT;
                    adoCommand.Parameters.Add("@LAST_START_TIME", OleDbType.VarChar).Value = LAST_START_TIME;
                    adoCommand.Parameters.Add("@LAST_END_TIME", OleDbType.VarChar).Value = LAST_END_TIME;
                    adoCommand.Parameters.Add("@LAST_DOWN_TIME", OleDbType.VarChar).Value = LAST_DOWN_TIME;
                    adoCommand.Parameters.Add("@LAST_DOWN_HIST_SEQ", OleDbType.Numeric).Value = LAST_DOWN_HIST_SEQ;
                    adoCommand.Parameters.Add("@LAST_EVENT_ID", OleDbType.VarChar).Value = LAST_EVENT_ID;
                    adoCommand.Parameters.Add("@LAST_EVENT_TIME", OleDbType.VarChar).Value = LAST_EVENT_TIME;
                    adoCommand.Parameters.Add("@LAST_ACTIVE_HIST_SEQ", OleDbType.Numeric).Value = LAST_ACTIVE_HIST_SEQ;
                    adoCommand.Parameters.Add("@LAST_HIST_SEQ", OleDbType.Numeric).Value = LAST_HIST_SEQ;

                    // Add Parameters for Condition
                    adoCommand.Parameters.Add("@C_FACTORY", OleDbType.VarChar).Value = FACTORY;
                    adoCommand.Parameters.Add("@C_RES_ID", OleDbType.VarChar).Value = RES_ID;
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
                    strQuery = "SELECT * FROM MRASRESDEF"
                        + " ORDER BY FACTORY ASC"
                        + " , RES_ID ASC";
                    break;

                case 6:
                    // Select All Record
                    strQuery = "SELECT * FROM MRASRESDEF"
                        + " WHERE FACTORY=?"
                        + " AND RES_ID >=?"
                        + " ORDER BY RES_ID ASC";

                    adoAdapter.SelectCommand.Parameters.Add("@FACTORY", OleDbType.VarChar).Value = FACTORY;
                    adoAdapter.SelectCommand.Parameters.Add("@RES_ID", OleDbType.VarChar).Value = RES_ID;
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

