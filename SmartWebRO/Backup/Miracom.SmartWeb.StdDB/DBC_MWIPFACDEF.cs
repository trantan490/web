//-----------------------------------------------------------------------------
//
//   System      : MES
//   File Name   : DBC_MWIPFACDEF.cs
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
//       - 2008-05-30 : Created by DBLib Generator
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
public class DBC_MWIPFACDEF
    : ICloneable
{


#region " Fields Definition "

    // Fields
    private string m_factory;                                // VARCHAR(10)
    private string m_fac_desc;                               // VARCHAR(50)
    private string m_fac_type;                               // VARCHAR(2)
    private string m_fac_grp_1;                              // VARCHAR(20)
    private string m_fac_grp_2;                              // VARCHAR(20)
    private string m_fac_grp_3;                              // VARCHAR(20)
    private string m_fac_grp_4;                              // VARCHAR(20)
    private string m_fac_grp_5;                              // VARCHAR(20)
    private double m_days_per_week;                          // NUMERIC(4.2)
    private double m_hours_per_day;                          // NUMERIC(4.2)
    private string m_variable_shift_flag;                    // VARCHAR(1)
    private string m_shift_1_start;                          // VARCHAR(4)
    private string m_shift_1_day_flag;                       // VARCHAR(1)
    private string m_shift_2_start;                          // VARCHAR(4)
    private string m_shift_2_day_flag;                       // VARCHAR(1)
    private string m_shift_3_start;                          // VARCHAR(4)
    private string m_shift_3_day_flag;                       // VARCHAR(1)
    private string m_shift_4_start;                          // VARCHAR(4)
    private string m_shift_4_day_flag;                       // VARCHAR(1)
    private string m_remote_fac_flag;                        // VARCHAR(1)
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
    private string m_create_user_id;                         // VARCHAR(20)
    private string m_create_time;                            // VARCHAR(14)
    private string m_update_user_id;                         // VARCHAR(20)
    private string m_update_time;                            // VARCHAR(14)

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
    /// Gets or sets the 'FAC_DESC' value.
    /// </summary>
    public string FAC_DESC
    {
        get
        {
            if (m_fac_desc == null )
            {
                m_fac_desc = " ";
            }
            return m_fac_desc;
        }
        set
        {
            if (value == null || value == "" )
            {
                m_fac_desc = " ";
            }
            m_fac_desc = value;
        }
    }

    /// <summary>
    /// Gets or sets the 'FAC_TYPE' value.
    /// </summary>
    public string FAC_TYPE
    {
        get
        {
            if (m_fac_type == null )
            {
                m_fac_type = " ";
            }
            return m_fac_type;
        }
        set
        {
            if (value == null || value == "" )
            {
                m_fac_type = " ";
            }
            m_fac_type = value;
        }
    }

    /// <summary>
    /// Gets or sets the 'FAC_GRP_1' value.
    /// </summary>
    public string FAC_GRP_1
    {
        get
        {
            if (m_fac_grp_1 == null )
            {
                m_fac_grp_1 = " ";
            }
            return m_fac_grp_1;
        }
        set
        {
            if (value == null || value == "" )
            {
                m_fac_grp_1 = " ";
            }
            m_fac_grp_1 = value;
        }
    }

    /// <summary>
    /// Gets or sets the 'FAC_GRP_2' value.
    /// </summary>
    public string FAC_GRP_2
    {
        get
        {
            if (m_fac_grp_2 == null )
            {
                m_fac_grp_2 = " ";
            }
            return m_fac_grp_2;
        }
        set
        {
            if (value == null || value == "" )
            {
                m_fac_grp_2 = " ";
            }
            m_fac_grp_2 = value;
        }
    }

    /// <summary>
    /// Gets or sets the 'FAC_GRP_3' value.
    /// </summary>
    public string FAC_GRP_3
    {
        get
        {
            if (m_fac_grp_3 == null )
            {
                m_fac_grp_3 = " ";
            }
            return m_fac_grp_3;
        }
        set
        {
            if (value == null || value == "" )
            {
                m_fac_grp_3 = " ";
            }
            m_fac_grp_3 = value;
        }
    }

    /// <summary>
    /// Gets or sets the 'FAC_GRP_4' value.
    /// </summary>
    public string FAC_GRP_4
    {
        get
        {
            if (m_fac_grp_4 == null )
            {
                m_fac_grp_4 = " ";
            }
            return m_fac_grp_4;
        }
        set
        {
            if (value == null || value == "" )
            {
                m_fac_grp_4 = " ";
            }
            m_fac_grp_4 = value;
        }
    }

    /// <summary>
    /// Gets or sets the 'FAC_GRP_5' value.
    /// </summary>
    public string FAC_GRP_5
    {
        get
        {
            if (m_fac_grp_5 == null )
            {
                m_fac_grp_5 = " ";
            }
            return m_fac_grp_5;
        }
        set
        {
            if (value == null || value == "" )
            {
                m_fac_grp_5 = " ";
            }
            m_fac_grp_5 = value;
        }
    }

    /// <summary>
    /// Gets or sets the 'DAYS_PER_WEEK' value.
    /// </summary>
    public double DAYS_PER_WEEK
    {
        get
        {
            return m_days_per_week;
        }
        set
        {
            m_days_per_week = value;
        }
    }

    /// <summary>
    /// Gets or sets the 'HOURS_PER_DAY' value.
    /// </summary>
    public double HOURS_PER_DAY
    {
        get
        {
            return m_hours_per_day;
        }
        set
        {
            m_hours_per_day = value;
        }
    }

    /// <summary>
    /// Gets or sets the 'VARIABLE_SHIFT_FLAG' value.
    /// </summary>
    public string VARIABLE_SHIFT_FLAG
    {
        get
        {
            if (m_variable_shift_flag == null )
            {
                m_variable_shift_flag = " ";
            }
            return m_variable_shift_flag;
        }
        set
        {
            if (value == null || value == "" )
            {
                m_variable_shift_flag = " ";
            }
            m_variable_shift_flag = value;
        }
    }

    /// <summary>
    /// Gets or sets the 'SHIFT_1_START' value.
    /// </summary>
    public string SHIFT_1_START
    {
        get
        {
            if (m_shift_1_start == null )
            {
                m_shift_1_start = " ";
            }
            return m_shift_1_start;
        }
        set
        {
            if (value == null || value == "" )
            {
                m_shift_1_start = " ";
            }
            m_shift_1_start = value;
        }
    }

    /// <summary>
    /// Gets or sets the 'SHIFT_1_DAY_FLAG' value.
    /// </summary>
    public string SHIFT_1_DAY_FLAG
    {
        get
        {
            if (m_shift_1_day_flag == null )
            {
                m_shift_1_day_flag = " ";
            }
            return m_shift_1_day_flag;
        }
        set
        {
            if (value == null || value == "" )
            {
                m_shift_1_day_flag = " ";
            }
            m_shift_1_day_flag = value;
        }
    }

    /// <summary>
    /// Gets or sets the 'SHIFT_2_START' value.
    /// </summary>
    public string SHIFT_2_START
    {
        get
        {
            if (m_shift_2_start == null )
            {
                m_shift_2_start = " ";
            }
            return m_shift_2_start;
        }
        set
        {
            if (value == null || value == "" )
            {
                m_shift_2_start = " ";
            }
            m_shift_2_start = value;
        }
    }

    /// <summary>
    /// Gets or sets the 'SHIFT_2_DAY_FLAG' value.
    /// </summary>
    public string SHIFT_2_DAY_FLAG
    {
        get
        {
            if (m_shift_2_day_flag == null )
            {
                m_shift_2_day_flag = " ";
            }
            return m_shift_2_day_flag;
        }
        set
        {
            if (value == null || value == "" )
            {
                m_shift_2_day_flag = " ";
            }
            m_shift_2_day_flag = value;
        }
    }

    /// <summary>
    /// Gets or sets the 'SHIFT_3_START' value.
    /// </summary>
    public string SHIFT_3_START
    {
        get
        {
            if (m_shift_3_start == null )
            {
                m_shift_3_start = " ";
            }
            return m_shift_3_start;
        }
        set
        {
            if (value == null || value == "" )
            {
                m_shift_3_start = " ";
            }
            m_shift_3_start = value;
        }
    }

    /// <summary>
    /// Gets or sets the 'SHIFT_3_DAY_FLAG' value.
    /// </summary>
    public string SHIFT_3_DAY_FLAG
    {
        get
        {
            if (m_shift_3_day_flag == null )
            {
                m_shift_3_day_flag = " ";
            }
            return m_shift_3_day_flag;
        }
        set
        {
            if (value == null || value == "" )
            {
                m_shift_3_day_flag = " ";
            }
            m_shift_3_day_flag = value;
        }
    }

    /// <summary>
    /// Gets or sets the 'SHIFT_4_START' value.
    /// </summary>
    public string SHIFT_4_START
    {
        get
        {
            if (m_shift_4_start == null )
            {
                m_shift_4_start = " ";
            }
            return m_shift_4_start;
        }
        set
        {
            if (value == null || value == "" )
            {
                m_shift_4_start = " ";
            }
            m_shift_4_start = value;
        }
    }

    /// <summary>
    /// Gets or sets the 'SHIFT_4_DAY_FLAG' value.
    /// </summary>
    public string SHIFT_4_DAY_FLAG
    {
        get
        {
            if (m_shift_4_day_flag == null )
            {
                m_shift_4_day_flag = " ";
            }
            return m_shift_4_day_flag;
        }
        set
        {
            if (value == null || value == "" )
            {
                m_shift_4_day_flag = " ";
            }
            m_shift_4_day_flag = value;
        }
    }

    /// <summary>
    /// Gets or sets the 'REMOTE_FAC_FLAG' value.
    /// </summary>
    public string REMOTE_FAC_FLAG
    {
        get
        {
            if (m_remote_fac_flag == null )
            {
                m_remote_fac_flag = " ";
            }
            return m_remote_fac_flag;
        }
        set
        {
            if (value == null || value == "" )
            {
                m_remote_fac_flag = " ";
            }
            m_remote_fac_flag = value;
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

#endregion

#region " Function Definition "

    /// <summary>
    /// Creator for Object
    /// <summary>
    public DBC_MWIPFACDEF(ref DB_Common dbc)
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
        FAC_DESC = " ";
        FAC_TYPE = " ";
        FAC_GRP_1 = " ";
        FAC_GRP_2 = " ";
        FAC_GRP_3 = " ";
        FAC_GRP_4 = " ";
        FAC_GRP_5 = " ";
        DAYS_PER_WEEK = 0;
        HOURS_PER_DAY = 0;
        VARIABLE_SHIFT_FLAG = " ";
        SHIFT_1_START = " ";
        SHIFT_1_DAY_FLAG = " ";
        SHIFT_2_START = " ";
        SHIFT_2_DAY_FLAG = " ";
        SHIFT_3_START = " ";
        SHIFT_3_DAY_FLAG = " ";
        SHIFT_4_START = " ";
        SHIFT_4_DAY_FLAG = " ";
        REMOTE_FAC_FLAG = " ";
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
        CREATE_USER_ID = " ";
        CREATE_TIME = " ";
        UPDATE_USER_ID = " ";
        UPDATE_TIME = " ";

        return true;
    }

    /// <summary>
    /// Clone object
    /// </summary>
    /// <returns>object</returns>
    public object Clone()
    {
        DBC_MWIPFACDEF MWIPFACDEF = null;
        try
        {
            MWIPFACDEF = new DBC_MWIPFACDEF(ref _dbc);

            MWIPFACDEF.FACTORY = this.FACTORY;
            MWIPFACDEF.FAC_DESC = this.FAC_DESC;
            MWIPFACDEF.FAC_TYPE = this.FAC_TYPE;
            MWIPFACDEF.FAC_GRP_1 = this.FAC_GRP_1;
            MWIPFACDEF.FAC_GRP_2 = this.FAC_GRP_2;
            MWIPFACDEF.FAC_GRP_3 = this.FAC_GRP_3;
            MWIPFACDEF.FAC_GRP_4 = this.FAC_GRP_4;
            MWIPFACDEF.FAC_GRP_5 = this.FAC_GRP_5;
            MWIPFACDEF.DAYS_PER_WEEK = this.DAYS_PER_WEEK;
            MWIPFACDEF.HOURS_PER_DAY = this.HOURS_PER_DAY;
            MWIPFACDEF.VARIABLE_SHIFT_FLAG = this.VARIABLE_SHIFT_FLAG;
            MWIPFACDEF.SHIFT_1_START = this.SHIFT_1_START;
            MWIPFACDEF.SHIFT_1_DAY_FLAG = this.SHIFT_1_DAY_FLAG;
            MWIPFACDEF.SHIFT_2_START = this.SHIFT_2_START;
            MWIPFACDEF.SHIFT_2_DAY_FLAG = this.SHIFT_2_DAY_FLAG;
            MWIPFACDEF.SHIFT_3_START = this.SHIFT_3_START;
            MWIPFACDEF.SHIFT_3_DAY_FLAG = this.SHIFT_3_DAY_FLAG;
            MWIPFACDEF.SHIFT_4_START = this.SHIFT_4_START;
            MWIPFACDEF.SHIFT_4_DAY_FLAG = this.SHIFT_4_DAY_FLAG;
            MWIPFACDEF.REMOTE_FAC_FLAG = this.REMOTE_FAC_FLAG;
            MWIPFACDEF.RES_STS_1 = this.RES_STS_1;
            MWIPFACDEF.RES_STS_2 = this.RES_STS_2;
            MWIPFACDEF.RES_STS_3 = this.RES_STS_3;
            MWIPFACDEF.RES_STS_4 = this.RES_STS_4;
            MWIPFACDEF.RES_STS_5 = this.RES_STS_5;
            MWIPFACDEF.RES_STS_6 = this.RES_STS_6;
            MWIPFACDEF.RES_STS_7 = this.RES_STS_7;
            MWIPFACDEF.RES_STS_8 = this.RES_STS_8;
            MWIPFACDEF.RES_STS_9 = this.RES_STS_9;
            MWIPFACDEF.RES_STS_10 = this.RES_STS_10;
            MWIPFACDEF.CREATE_USER_ID = this.CREATE_USER_ID;
            MWIPFACDEF.CREATE_TIME = this.CREATE_TIME;
            MWIPFACDEF.UPDATE_USER_ID = this.UPDATE_USER_ID;
            MWIPFACDEF.UPDATE_TIME = this.UPDATE_TIME;
        }
        catch(Exception ex)
        {
            if (ex.GetType().ToString() == "System.Data.OleDb.OleDbException")
                _dbc.gErrors.SetErrors(((OleDbException)ex).Errors);
            else
                _dbc.gErrors.SetErrors(ex);

            return null;
        }

        return MWIPFACDEF;
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
            FAC_DESC = (string)(Row["FAC_DESC"]);
            FAC_TYPE = (string)(Row["FAC_TYPE"]);
            FAC_GRP_1 = (string)(Row["FAC_GRP_1"]);
            FAC_GRP_2 = (string)(Row["FAC_GRP_2"]);
            FAC_GRP_3 = (string)(Row["FAC_GRP_3"]);
            FAC_GRP_4 = (string)(Row["FAC_GRP_4"]);
            FAC_GRP_5 = (string)(Row["FAC_GRP_5"]);
            DAYS_PER_WEEK = Convert.ToDouble(Row["DAYS_PER_WEEK"]);
            HOURS_PER_DAY = Convert.ToDouble(Row["HOURS_PER_DAY"]);
            VARIABLE_SHIFT_FLAG = (string)(Row["VARIABLE_SHIFT_FLAG"]);
            SHIFT_1_START = (string)(Row["SHIFT_1_START"]);
            SHIFT_1_DAY_FLAG = (string)(Row["SHIFT_1_DAY_FLAG"]);
            SHIFT_2_START = (string)(Row["SHIFT_2_START"]);
            SHIFT_2_DAY_FLAG = (string)(Row["SHIFT_2_DAY_FLAG"]);
            SHIFT_3_START = (string)(Row["SHIFT_3_START"]);
            SHIFT_3_DAY_FLAG = (string)(Row["SHIFT_3_DAY_FLAG"]);
            SHIFT_4_START = (string)(Row["SHIFT_4_START"]);
            SHIFT_4_DAY_FLAG = (string)(Row["SHIFT_4_DAY_FLAG"]);
            REMOTE_FAC_FLAG = (string)(Row["REMOTE_FAC_FLAG"]);
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
            CREATE_USER_ID = (string)(Row["CREATE_USER_ID"]);
            CREATE_TIME = (string)(Row["CREATE_TIME"]);
            UPDATE_USER_ID = (string)(Row["UPDATE_USER_ID"]);
            UPDATE_TIME = (string)(Row["UPDATE_TIME"]);
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
                    strQuery = "SELECT * FROM MWIPFACDEF"
                        + " WHERE FACTORY=?";

                    // Add Parameters
                    adoAdapter.SelectCommand.Parameters.Add("@FACTORY", OleDbType.VarChar).Value = FACTORY;
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
                    case "FAC_DESC":
                        FAC_DESC = (string)(adoDataTable.Rows[0]["FAC_DESC"]);
                        break;
                    case "FAC_TYPE":
                        FAC_TYPE = (string)(adoDataTable.Rows[0]["FAC_TYPE"]);
                        break;
                    case "FAC_GRP_1":
                        FAC_GRP_1 = (string)(adoDataTable.Rows[0]["FAC_GRP_1"]);
                        break;
                    case "FAC_GRP_2":
                        FAC_GRP_2 = (string)(adoDataTable.Rows[0]["FAC_GRP_2"]);
                        break;
                    case "FAC_GRP_3":
                        FAC_GRP_3 = (string)(adoDataTable.Rows[0]["FAC_GRP_3"]);
                        break;
                    case "FAC_GRP_4":
                        FAC_GRP_4 = (string)(adoDataTable.Rows[0]["FAC_GRP_4"]);
                        break;
                    case "FAC_GRP_5":
                        FAC_GRP_5 = (string)(adoDataTable.Rows[0]["FAC_GRP_5"]);
                        break;
                    case "DAYS_PER_WEEK":
                        DAYS_PER_WEEK = Convert.ToDouble(adoDataTable.Rows[0]["DAYS_PER_WEEK"]);
                        break;
                    case "HOURS_PER_DAY":
                        HOURS_PER_DAY = Convert.ToDouble(adoDataTable.Rows[0]["HOURS_PER_DAY"]);
                        break;
                    case "VARIABLE_SHIFT_FLAG":
                        VARIABLE_SHIFT_FLAG = (string)(adoDataTable.Rows[0]["VARIABLE_SHIFT_FLAG"]);
                        break;
                    case "SHIFT_1_START":
                        SHIFT_1_START = (string)(adoDataTable.Rows[0]["SHIFT_1_START"]);
                        break;
                    case "SHIFT_1_DAY_FLAG":
                        SHIFT_1_DAY_FLAG = (string)(adoDataTable.Rows[0]["SHIFT_1_DAY_FLAG"]);
                        break;
                    case "SHIFT_2_START":
                        SHIFT_2_START = (string)(adoDataTable.Rows[0]["SHIFT_2_START"]);
                        break;
                    case "SHIFT_2_DAY_FLAG":
                        SHIFT_2_DAY_FLAG = (string)(adoDataTable.Rows[0]["SHIFT_2_DAY_FLAG"]);
                        break;
                    case "SHIFT_3_START":
                        SHIFT_3_START = (string)(adoDataTable.Rows[0]["SHIFT_3_START"]);
                        break;
                    case "SHIFT_3_DAY_FLAG":
                        SHIFT_3_DAY_FLAG = (string)(adoDataTable.Rows[0]["SHIFT_3_DAY_FLAG"]);
                        break;
                    case "SHIFT_4_START":
                        SHIFT_4_START = (string)(adoDataTable.Rows[0]["SHIFT_4_START"]);
                        break;
                    case "SHIFT_4_DAY_FLAG":
                        SHIFT_4_DAY_FLAG = (string)(adoDataTable.Rows[0]["SHIFT_4_DAY_FLAG"]);
                        break;
                    case "REMOTE_FAC_FLAG":
                        REMOTE_FAC_FLAG = (string)(adoDataTable.Rows[0]["REMOTE_FAC_FLAG"]);
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
                        strQuery = "SELECT * FROM MWIPFACDEF"
                            + " WITH (UPDLOCK) WHERE FACTORY=?";
                    }
                    else if (_dbc.gDbType == (int)DB_TYPE.ORACLE)
                    {
                        strQuery = "SELECT * FROM MWIPFACDEF"
                            + " WHERE FACTORY=?"
                            + " FOR UPDATE";
                    }
                    else if (_dbc.gDbType == (int)DB_TYPE.DB2) 
                    {
                    }

                    // Add Parameters
                    adoAdapter.SelectCommand.Parameters.Add("@FACTORY", OleDbType.VarChar).Value = FACTORY;
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
                    case "FAC_DESC":
                        FAC_DESC = (string)(adoDataTable.Rows[0]["FAC_DESC"]);
                        break;
                    case "FAC_TYPE":
                        FAC_TYPE = (string)(adoDataTable.Rows[0]["FAC_TYPE"]);
                        break;
                    case "FAC_GRP_1":
                        FAC_GRP_1 = (string)(adoDataTable.Rows[0]["FAC_GRP_1"]);
                        break;
                    case "FAC_GRP_2":
                        FAC_GRP_2 = (string)(adoDataTable.Rows[0]["FAC_GRP_2"]);
                        break;
                    case "FAC_GRP_3":
                        FAC_GRP_3 = (string)(adoDataTable.Rows[0]["FAC_GRP_3"]);
                        break;
                    case "FAC_GRP_4":
                        FAC_GRP_4 = (string)(adoDataTable.Rows[0]["FAC_GRP_4"]);
                        break;
                    case "FAC_GRP_5":
                        FAC_GRP_5 = (string)(adoDataTable.Rows[0]["FAC_GRP_5"]);
                        break;
                    case "DAYS_PER_WEEK":
                        DAYS_PER_WEEK = Convert.ToDouble(adoDataTable.Rows[0]["DAYS_PER_WEEK"]);
                        break;
                    case "HOURS_PER_DAY":
                        HOURS_PER_DAY = Convert.ToDouble(adoDataTable.Rows[0]["HOURS_PER_DAY"]);
                        break;
                    case "VARIABLE_SHIFT_FLAG":
                        VARIABLE_SHIFT_FLAG = (string)(adoDataTable.Rows[0]["VARIABLE_SHIFT_FLAG"]);
                        break;
                    case "SHIFT_1_START":
                        SHIFT_1_START = (string)(adoDataTable.Rows[0]["SHIFT_1_START"]);
                        break;
                    case "SHIFT_1_DAY_FLAG":
                        SHIFT_1_DAY_FLAG = (string)(adoDataTable.Rows[0]["SHIFT_1_DAY_FLAG"]);
                        break;
                    case "SHIFT_2_START":
                        SHIFT_2_START = (string)(adoDataTable.Rows[0]["SHIFT_2_START"]);
                        break;
                    case "SHIFT_2_DAY_FLAG":
                        SHIFT_2_DAY_FLAG = (string)(adoDataTable.Rows[0]["SHIFT_2_DAY_FLAG"]);
                        break;
                    case "SHIFT_3_START":
                        SHIFT_3_START = (string)(adoDataTable.Rows[0]["SHIFT_3_START"]);
                        break;
                    case "SHIFT_3_DAY_FLAG":
                        SHIFT_3_DAY_FLAG = (string)(adoDataTable.Rows[0]["SHIFT_3_DAY_FLAG"]);
                        break;
                    case "SHIFT_4_START":
                        SHIFT_4_START = (string)(adoDataTable.Rows[0]["SHIFT_4_START"]);
                        break;
                    case "SHIFT_4_DAY_FLAG":
                        SHIFT_4_DAY_FLAG = (string)(adoDataTable.Rows[0]["SHIFT_4_DAY_FLAG"]);
                        break;
                    case "REMOTE_FAC_FLAG":
                        REMOTE_FAC_FLAG = (string)(adoDataTable.Rows[0]["REMOTE_FAC_FLAG"]);
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
                    strQuery = "SELECT COUNT(*) FROM MWIPFACDEF"
                        + " WHERE FACTORY=?";

                    // Add Parameters
                    adoCommand.Parameters.Add("@FACTORY", OleDbType.VarChar).Value = FACTORY;
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
                    strQuery = "DELETE FROM MWIPFACDEF"
                        + " WHERE FACTORY=?";

                    // Add Parameters
                    adoCommand.Parameters.Add("@FACTORY", OleDbType.VarChar).Value = FACTORY;
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

            strQuery = "INSERT INTO MWIPFACDEF ("
                + " FACTORY, FAC_DESC, FAC_TYPE, FAC_GRP_1, FAC_GRP_2,"
                + " FAC_GRP_3, FAC_GRP_4, FAC_GRP_5, DAYS_PER_WEEK, HOURS_PER_DAY,"
                + " VARIABLE_SHIFT_FLAG, SHIFT_1_START, SHIFT_1_DAY_FLAG, SHIFT_2_START, SHIFT_2_DAY_FLAG,"
                + " SHIFT_3_START, SHIFT_3_DAY_FLAG, SHIFT_4_START, SHIFT_4_DAY_FLAG, REMOTE_FAC_FLAG,"
                + " RES_STS_1, RES_STS_2, RES_STS_3, RES_STS_4, RES_STS_5,"
                + " RES_STS_6, RES_STS_7, RES_STS_8, RES_STS_9, RES_STS_10,"
                + " CREATE_USER_ID, CREATE_TIME, UPDATE_USER_ID, UPDATE_TIME)"
                + " VALUES ("
                + " ?, ?, ?, ?, ?, ?, ?, ?, ?, ?,"
                + " ?, ?, ?, ?, ?, ?, ?, ?, ?, ?,"
                + " ?, ?, ?, ?, ?, ?, ?, ?, ?, ?,"
                + " ?, ?, ?, ?)";

            // Add Parameters
            adoCommand.Parameters.Add("@FACTORY", OleDbType.VarChar).Value = FACTORY;
            adoCommand.Parameters.Add("@FAC_DESC", OleDbType.VarChar).Value = FAC_DESC;
            adoCommand.Parameters.Add("@FAC_TYPE", OleDbType.VarChar).Value = FAC_TYPE;
            adoCommand.Parameters.Add("@FAC_GRP_1", OleDbType.VarChar).Value = FAC_GRP_1;
            adoCommand.Parameters.Add("@FAC_GRP_2", OleDbType.VarChar).Value = FAC_GRP_2;
            adoCommand.Parameters.Add("@FAC_GRP_3", OleDbType.VarChar).Value = FAC_GRP_3;
            adoCommand.Parameters.Add("@FAC_GRP_4", OleDbType.VarChar).Value = FAC_GRP_4;
            adoCommand.Parameters.Add("@FAC_GRP_5", OleDbType.VarChar).Value = FAC_GRP_5;
            adoCommand.Parameters.Add("@DAYS_PER_WEEK", OleDbType.Numeric).Value = DAYS_PER_WEEK;
            adoCommand.Parameters.Add("@HOURS_PER_DAY", OleDbType.Numeric).Value = HOURS_PER_DAY;
            adoCommand.Parameters.Add("@VARIABLE_SHIFT_FLAG", OleDbType.VarChar).Value = VARIABLE_SHIFT_FLAG;
            adoCommand.Parameters.Add("@SHIFT_1_START", OleDbType.VarChar).Value = SHIFT_1_START;
            adoCommand.Parameters.Add("@SHIFT_1_DAY_FLAG", OleDbType.VarChar).Value = SHIFT_1_DAY_FLAG;
            adoCommand.Parameters.Add("@SHIFT_2_START", OleDbType.VarChar).Value = SHIFT_2_START;
            adoCommand.Parameters.Add("@SHIFT_2_DAY_FLAG", OleDbType.VarChar).Value = SHIFT_2_DAY_FLAG;
            adoCommand.Parameters.Add("@SHIFT_3_START", OleDbType.VarChar).Value = SHIFT_3_START;
            adoCommand.Parameters.Add("@SHIFT_3_DAY_FLAG", OleDbType.VarChar).Value = SHIFT_3_DAY_FLAG;
            adoCommand.Parameters.Add("@SHIFT_4_START", OleDbType.VarChar).Value = SHIFT_4_START;
            adoCommand.Parameters.Add("@SHIFT_4_DAY_FLAG", OleDbType.VarChar).Value = SHIFT_4_DAY_FLAG;
            adoCommand.Parameters.Add("@REMOTE_FAC_FLAG", OleDbType.VarChar).Value = REMOTE_FAC_FLAG;
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
            adoCommand.Parameters.Add("@CREATE_USER_ID", OleDbType.VarChar).Value = CREATE_USER_ID;
            adoCommand.Parameters.Add("@CREATE_TIME", OleDbType.VarChar).Value = CREATE_TIME;
            adoCommand.Parameters.Add("@UPDATE_USER_ID", OleDbType.VarChar).Value = UPDATE_USER_ID;
            adoCommand.Parameters.Add("@UPDATE_TIME", OleDbType.VarChar).Value = UPDATE_TIME;

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
                    strQuery = "UPDATE MWIPFACDEF SET"
                        + " FAC_DESC=?, FAC_TYPE=?, FAC_GRP_1=?, FAC_GRP_2=?, FAC_GRP_3=?,"
                        + " FAC_GRP_4=?, FAC_GRP_5=?, DAYS_PER_WEEK=?, HOURS_PER_DAY=?, VARIABLE_SHIFT_FLAG=?,"
                        + " SHIFT_1_START=?, SHIFT_1_DAY_FLAG=?, SHIFT_2_START=?, SHIFT_2_DAY_FLAG=?, SHIFT_3_START=?,"
                        + " SHIFT_3_DAY_FLAG=?, SHIFT_4_START=?, SHIFT_4_DAY_FLAG=?, REMOTE_FAC_FLAG=?, RES_STS_1=?,"
                        + " RES_STS_2=?, RES_STS_3=?, RES_STS_4=?, RES_STS_5=?, RES_STS_6=?,"
                        + " RES_STS_7=?, RES_STS_8=?, RES_STS_9=?, RES_STS_10=?, CREATE_USER_ID=?,"
                        + " CREATE_TIME=?, UPDATE_USER_ID=?, UPDATE_TIME=?"
                        + " WHERE FACTORY=?";

                    // Add Parameters
                    adoCommand.Parameters.Add("@FAC_DESC", OleDbType.VarChar).Value = FAC_DESC;
                    adoCommand.Parameters.Add("@FAC_TYPE", OleDbType.VarChar).Value = FAC_TYPE;
                    adoCommand.Parameters.Add("@FAC_GRP_1", OleDbType.VarChar).Value = FAC_GRP_1;
                    adoCommand.Parameters.Add("@FAC_GRP_2", OleDbType.VarChar).Value = FAC_GRP_2;
                    adoCommand.Parameters.Add("@FAC_GRP_3", OleDbType.VarChar).Value = FAC_GRP_3;
                    adoCommand.Parameters.Add("@FAC_GRP_4", OleDbType.VarChar).Value = FAC_GRP_4;
                    adoCommand.Parameters.Add("@FAC_GRP_5", OleDbType.VarChar).Value = FAC_GRP_5;
                    adoCommand.Parameters.Add("@DAYS_PER_WEEK", OleDbType.Numeric).Value = DAYS_PER_WEEK;
                    adoCommand.Parameters.Add("@HOURS_PER_DAY", OleDbType.Numeric).Value = HOURS_PER_DAY;
                    adoCommand.Parameters.Add("@VARIABLE_SHIFT_FLAG", OleDbType.VarChar).Value = VARIABLE_SHIFT_FLAG;
                    adoCommand.Parameters.Add("@SHIFT_1_START", OleDbType.VarChar).Value = SHIFT_1_START;
                    adoCommand.Parameters.Add("@SHIFT_1_DAY_FLAG", OleDbType.VarChar).Value = SHIFT_1_DAY_FLAG;
                    adoCommand.Parameters.Add("@SHIFT_2_START", OleDbType.VarChar).Value = SHIFT_2_START;
                    adoCommand.Parameters.Add("@SHIFT_2_DAY_FLAG", OleDbType.VarChar).Value = SHIFT_2_DAY_FLAG;
                    adoCommand.Parameters.Add("@SHIFT_3_START", OleDbType.VarChar).Value = SHIFT_3_START;
                    adoCommand.Parameters.Add("@SHIFT_3_DAY_FLAG", OleDbType.VarChar).Value = SHIFT_3_DAY_FLAG;
                    adoCommand.Parameters.Add("@SHIFT_4_START", OleDbType.VarChar).Value = SHIFT_4_START;
                    adoCommand.Parameters.Add("@SHIFT_4_DAY_FLAG", OleDbType.VarChar).Value = SHIFT_4_DAY_FLAG;
                    adoCommand.Parameters.Add("@REMOTE_FAC_FLAG", OleDbType.VarChar).Value = REMOTE_FAC_FLAG;
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
                    adoCommand.Parameters.Add("@CREATE_USER_ID", OleDbType.VarChar).Value = CREATE_USER_ID;
                    adoCommand.Parameters.Add("@CREATE_TIME", OleDbType.VarChar).Value = CREATE_TIME;
                    adoCommand.Parameters.Add("@UPDATE_USER_ID", OleDbType.VarChar).Value = UPDATE_USER_ID;
                    adoCommand.Parameters.Add("@UPDATE_TIME", OleDbType.VarChar).Value = UPDATE_TIME;

                    // Add Parameters for Condition
                    adoCommand.Parameters.Add("@C_FACTORY", OleDbType.VarChar).Value = FACTORY;
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
                    strQuery = "SELECT * FROM MWIPFACDEF"
                        + " ORDER BY FACTORY ASC";

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

