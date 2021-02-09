//-----------------------------------------------------------------------------
//
//   System      : MES
//   File Name   : DBC_MSECUSRDEF.cs
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
public class DBC_MSECUSRDEF
    : ICloneable
{


#region " Fields Definition "

    // Fields
    private string m_factory;                                // VARCHAR(10)
    private string m_user_id;                                // VARCHAR(20)
    private string m_user_desc;                              // VARCHAR(50)
    private string m_password;                               // VARCHAR(20)
    private string m_chg_pass_flag;                          // VARCHAR(1)
    private string m_user_grp_1;                             // VARCHAR(20)
    private string m_user_grp_2;                             // VARCHAR(20)
    private string m_user_grp_3;                             // VARCHAR(20)
    private string m_user_grp_4;                             // VARCHAR(20)
    private string m_user_grp_5;                             // VARCHAR(20)
    private string m_user_grp_6;                             // VARCHAR(20)
    private string m_user_grp_7;                             // VARCHAR(20)
    private string m_user_grp_8;                             // VARCHAR(20)
    private string m_user_grp_9;                             // VARCHAR(20)
    private string m_user_grp_10;                            // VARCHAR(20)
    private string m_user_cmf_1;                             // VARCHAR(30)
    private string m_user_cmf_2;                             // VARCHAR(30)
    private string m_user_cmf_3;                             // VARCHAR(30)
    private string m_user_cmf_4;                             // VARCHAR(30)
    private string m_user_cmf_5;                             // VARCHAR(30)
    private string m_user_cmf_6;                             // VARCHAR(30)
    private string m_user_cmf_7;                             // VARCHAR(30)
    private string m_user_cmf_8;                             // VARCHAR(30)
    private string m_user_cmf_9;                             // VARCHAR(30)
    private string m_user_cmf_10;                            // VARCHAR(30)
    private string m_sec_grp_id;                             // VARCHAR(20)
    private string m_phone_office;                           // VARCHAR(20)
    private string m_phone_mobile;                           // VARCHAR(20)
    private string m_phone_home;                             // VARCHAR(20)
    private string m_phone_other;                            // VARCHAR(20)
    private string m_expire_date;                            // VARCHAR(8)
    private string m_pass_expire_date;                       // VARCHAR(8)
    private string m_enter_date;                             // VARCHAR(8)
    private string m_retire_date;                            // VARCHAR(8)
    private string m_email_id;                               // VARCHAR(50)
    private string m_birthday;                               // VARCHAR(8)
    private string m_sex_flag;                               // VARCHAR(1)
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
    /// Gets or sets the 'USER_ID' value.
    /// </summary>
    public string USER_ID
    {
        get
        {
            if (m_user_id == null )
            {
                m_user_id = " ";
            }
            return m_user_id;
        }
        set
        {
            if (value == null || value == "" )
            {
                m_user_id = " ";
            }
            m_user_id = value;
        }
    }

    /// <summary>
    /// Gets or sets the 'USER_DESC' value.
    /// </summary>
    public string USER_DESC
    {
        get
        {
            if (m_user_desc == null )
            {
                m_user_desc = " ";
            }
            return m_user_desc;
        }
        set
        {
            if (value == null || value == "" )
            {
                m_user_desc = " ";
            }
            m_user_desc = value;
        }
    }

    /// <summary>
    /// Gets or sets the 'PASSWORD' value.
    /// </summary>
    public string PASSWORD
    {
        get
        {
            if (m_password == null )
            {
                m_password = " ";
            }
            return m_password;
        }
        set
        {
            if (value == null || value == "" )
            {
                m_password = " ";
            }
            m_password = value;
        }
    }

    /// <summary>
    /// Gets or sets the 'CHG_PASS_FLAG' value.
    /// </summary>
    public string CHG_PASS_FLAG
    {
        get
        {
            if (m_chg_pass_flag == null )
            {
                m_chg_pass_flag = " ";
            }
            return m_chg_pass_flag;
        }
        set
        {
            if (value == null || value == "" )
            {
                m_chg_pass_flag = " ";
            }
            m_chg_pass_flag = value;
        }
    }

    /// <summary>
    /// Gets or sets the 'USER_GRP_1' value.
    /// </summary>
    public string USER_GRP_1
    {
        get
        {
            if (m_user_grp_1 == null )
            {
                m_user_grp_1 = " ";
            }
            return m_user_grp_1;
        }
        set
        {
            if (value == null || value == "" )
            {
                m_user_grp_1 = " ";
            }
            m_user_grp_1 = value;
        }
    }

    /// <summary>
    /// Gets or sets the 'USER_GRP_2' value.
    /// </summary>
    public string USER_GRP_2
    {
        get
        {
            if (m_user_grp_2 == null )
            {
                m_user_grp_2 = " ";
            }
            return m_user_grp_2;
        }
        set
        {
            if (value == null || value == "" )
            {
                m_user_grp_2 = " ";
            }
            m_user_grp_2 = value;
        }
    }

    /// <summary>
    /// Gets or sets the 'USER_GRP_3' value.
    /// </summary>
    public string USER_GRP_3
    {
        get
        {
            if (m_user_grp_3 == null )
            {
                m_user_grp_3 = " ";
            }
            return m_user_grp_3;
        }
        set
        {
            if (value == null || value == "" )
            {
                m_user_grp_3 = " ";
            }
            m_user_grp_3 = value;
        }
    }

    /// <summary>
    /// Gets or sets the 'USER_GRP_4' value.
    /// </summary>
    public string USER_GRP_4
    {
        get
        {
            if (m_user_grp_4 == null )
            {
                m_user_grp_4 = " ";
            }
            return m_user_grp_4;
        }
        set
        {
            if (value == null || value == "" )
            {
                m_user_grp_4 = " ";
            }
            m_user_grp_4 = value;
        }
    }

    /// <summary>
    /// Gets or sets the 'USER_GRP_5' value.
    /// </summary>
    public string USER_GRP_5
    {
        get
        {
            if (m_user_grp_5 == null )
            {
                m_user_grp_5 = " ";
            }
            return m_user_grp_5;
        }
        set
        {
            if (value == null || value == "" )
            {
                m_user_grp_5 = " ";
            }
            m_user_grp_5 = value;
        }
    }

    /// <summary>
    /// Gets or sets the 'USER_GRP_6' value.
    /// </summary>
    public string USER_GRP_6
    {
        get
        {
            if (m_user_grp_6 == null )
            {
                m_user_grp_6 = " ";
            }
            return m_user_grp_6;
        }
        set
        {
            if (value == null || value == "" )
            {
                m_user_grp_6 = " ";
            }
            m_user_grp_6 = value;
        }
    }

    /// <summary>
    /// Gets or sets the 'USER_GRP_7' value.
    /// </summary>
    public string USER_GRP_7
    {
        get
        {
            if (m_user_grp_7 == null )
            {
                m_user_grp_7 = " ";
            }
            return m_user_grp_7;
        }
        set
        {
            if (value == null || value == "" )
            {
                m_user_grp_7 = " ";
            }
            m_user_grp_7 = value;
        }
    }

    /// <summary>
    /// Gets or sets the 'USER_GRP_8' value.
    /// </summary>
    public string USER_GRP_8
    {
        get
        {
            if (m_user_grp_8 == null )
            {
                m_user_grp_8 = " ";
            }
            return m_user_grp_8;
        }
        set
        {
            if (value == null || value == "" )
            {
                m_user_grp_8 = " ";
            }
            m_user_grp_8 = value;
        }
    }

    /// <summary>
    /// Gets or sets the 'USER_GRP_9' value.
    /// </summary>
    public string USER_GRP_9
    {
        get
        {
            if (m_user_grp_9 == null )
            {
                m_user_grp_9 = " ";
            }
            return m_user_grp_9;
        }
        set
        {
            if (value == null || value == "" )
            {
                m_user_grp_9 = " ";
            }
            m_user_grp_9 = value;
        }
    }

    /// <summary>
    /// Gets or sets the 'USER_GRP_10' value.
    /// </summary>
    public string USER_GRP_10
    {
        get
        {
            if (m_user_grp_10 == null )
            {
                m_user_grp_10 = " ";
            }
            return m_user_grp_10;
        }
        set
        {
            if (value == null || value == "" )
            {
                m_user_grp_10 = " ";
            }
            m_user_grp_10 = value;
        }
    }

    /// <summary>
    /// Gets or sets the 'USER_CMF_1' value.
    /// </summary>
    public string USER_CMF_1
    {
        get
        {
            if (m_user_cmf_1 == null )
            {
                m_user_cmf_1 = " ";
            }
            return m_user_cmf_1;
        }
        set
        {
            if (value == null || value == "" )
            {
                m_user_cmf_1 = " ";
            }
            m_user_cmf_1 = value;
        }
    }

    /// <summary>
    /// Gets or sets the 'USER_CMF_2' value.
    /// </summary>
    public string USER_CMF_2
    {
        get
        {
            if (m_user_cmf_2 == null )
            {
                m_user_cmf_2 = " ";
            }
            return m_user_cmf_2;
        }
        set
        {
            if (value == null || value == "" )
            {
                m_user_cmf_2 = " ";
            }
            m_user_cmf_2 = value;
        }
    }

    /// <summary>
    /// Gets or sets the 'USER_CMF_3' value.
    /// </summary>
    public string USER_CMF_3
    {
        get
        {
            if (m_user_cmf_3 == null )
            {
                m_user_cmf_3 = " ";
            }
            return m_user_cmf_3;
        }
        set
        {
            if (value == null || value == "" )
            {
                m_user_cmf_3 = " ";
            }
            m_user_cmf_3 = value;
        }
    }

    /// <summary>
    /// Gets or sets the 'USER_CMF_4' value.
    /// </summary>
    public string USER_CMF_4
    {
        get
        {
            if (m_user_cmf_4 == null )
            {
                m_user_cmf_4 = " ";
            }
            return m_user_cmf_4;
        }
        set
        {
            if (value == null || value == "" )
            {
                m_user_cmf_4 = " ";
            }
            m_user_cmf_4 = value;
        }
    }

    /// <summary>
    /// Gets or sets the 'USER_CMF_5' value.
    /// </summary>
    public string USER_CMF_5
    {
        get
        {
            if (m_user_cmf_5 == null )
            {
                m_user_cmf_5 = " ";
            }
            return m_user_cmf_5;
        }
        set
        {
            if (value == null || value == "" )
            {
                m_user_cmf_5 = " ";
            }
            m_user_cmf_5 = value;
        }
    }

    /// <summary>
    /// Gets or sets the 'USER_CMF_6' value.
    /// </summary>
    public string USER_CMF_6
    {
        get
        {
            if (m_user_cmf_6 == null )
            {
                m_user_cmf_6 = " ";
            }
            return m_user_cmf_6;
        }
        set
        {
            if (value == null || value == "" )
            {
                m_user_cmf_6 = " ";
            }
            m_user_cmf_6 = value;
        }
    }

    /// <summary>
    /// Gets or sets the 'USER_CMF_7' value.
    /// </summary>
    public string USER_CMF_7
    {
        get
        {
            if (m_user_cmf_7 == null )
            {
                m_user_cmf_7 = " ";
            }
            return m_user_cmf_7;
        }
        set
        {
            if (value == null || value == "" )
            {
                m_user_cmf_7 = " ";
            }
            m_user_cmf_7 = value;
        }
    }

    /// <summary>
    /// Gets or sets the 'USER_CMF_8' value.
    /// </summary>
    public string USER_CMF_8
    {
        get
        {
            if (m_user_cmf_8 == null )
            {
                m_user_cmf_8 = " ";
            }
            return m_user_cmf_8;
        }
        set
        {
            if (value == null || value == "" )
            {
                m_user_cmf_8 = " ";
            }
            m_user_cmf_8 = value;
        }
    }

    /// <summary>
    /// Gets or sets the 'USER_CMF_9' value.
    /// </summary>
    public string USER_CMF_9
    {
        get
        {
            if (m_user_cmf_9 == null )
            {
                m_user_cmf_9 = " ";
            }
            return m_user_cmf_9;
        }
        set
        {
            if (value == null || value == "" )
            {
                m_user_cmf_9 = " ";
            }
            m_user_cmf_9 = value;
        }
    }

    /// <summary>
    /// Gets or sets the 'USER_CMF_10' value.
    /// </summary>
    public string USER_CMF_10
    {
        get
        {
            if (m_user_cmf_10 == null )
            {
                m_user_cmf_10 = " ";
            }
            return m_user_cmf_10;
        }
        set
        {
            if (value == null || value == "" )
            {
                m_user_cmf_10 = " ";
            }
            m_user_cmf_10 = value;
        }
    }

    /// <summary>
    /// Gets or sets the 'SEC_GRP_ID' value.
    /// </summary>
    public string SEC_GRP_ID
    {
        get
        {
            if (m_sec_grp_id == null )
            {
                m_sec_grp_id = " ";
            }
            return m_sec_grp_id;
        }
        set
        {
            if (value == null || value == "" )
            {
                m_sec_grp_id = " ";
            }
            m_sec_grp_id = value;
        }
    }

    /// <summary>
    /// Gets or sets the 'PHONE_OFFICE' value.
    /// </summary>
    public string PHONE_OFFICE
    {
        get
        {
            if (m_phone_office == null )
            {
                m_phone_office = " ";
            }
            return m_phone_office;
        }
        set
        {
            if (value == null || value == "" )
            {
                m_phone_office = " ";
            }
            m_phone_office = value;
        }
    }

    /// <summary>
    /// Gets or sets the 'PHONE_MOBILE' value.
    /// </summary>
    public string PHONE_MOBILE
    {
        get
        {
            if (m_phone_mobile == null )
            {
                m_phone_mobile = " ";
            }
            return m_phone_mobile;
        }
        set
        {
            if (value == null || value == "" )
            {
                m_phone_mobile = " ";
            }
            m_phone_mobile = value;
        }
    }

    /// <summary>
    /// Gets or sets the 'PHONE_HOME' value.
    /// </summary>
    public string PHONE_HOME
    {
        get
        {
            if (m_phone_home == null )
            {
                m_phone_home = " ";
            }
            return m_phone_home;
        }
        set
        {
            if (value == null || value == "" )
            {
                m_phone_home = " ";
            }
            m_phone_home = value;
        }
    }

    /// <summary>
    /// Gets or sets the 'PHONE_OTHER' value.
    /// </summary>
    public string PHONE_OTHER
    {
        get
        {
            if (m_phone_other == null )
            {
                m_phone_other = " ";
            }
            return m_phone_other;
        }
        set
        {
            if (value == null || value == "" )
            {
                m_phone_other = " ";
            }
            m_phone_other = value;
        }
    }

    /// <summary>
    /// Gets or sets the 'EXPIRE_DATE' value.
    /// </summary>
    public string EXPIRE_DATE
    {
        get
        {
            if (m_expire_date == null )
            {
                m_expire_date = " ";
            }
            return m_expire_date;
        }
        set
        {
            if (value == null || value == "" )
            {
                m_expire_date = " ";
            }
            m_expire_date = value;
        }
    }

    /// <summary>
    /// Gets or sets the 'PASS_EXPIRE_DATE' value.
    /// </summary>
    public string PASS_EXPIRE_DATE
    {
        get
        {
            if (m_pass_expire_date == null )
            {
                m_pass_expire_date = " ";
            }
            return m_pass_expire_date;
        }
        set
        {
            if (value == null || value == "" )
            {
                m_pass_expire_date = " ";
            }
            m_pass_expire_date = value;
        }
    }

    /// <summary>
    /// Gets or sets the 'ENTER_DATE' value.
    /// </summary>
    public string ENTER_DATE
    {
        get
        {
            if (m_enter_date == null )
            {
                m_enter_date = " ";
            }
            return m_enter_date;
        }
        set
        {
            if (value == null || value == "" )
            {
                m_enter_date = " ";
            }
            m_enter_date = value;
        }
    }

    /// <summary>
    /// Gets or sets the 'RETIRE_DATE' value.
    /// </summary>
    public string RETIRE_DATE
    {
        get
        {
            if (m_retire_date == null )
            {
                m_retire_date = " ";
            }
            return m_retire_date;
        }
        set
        {
            if (value == null || value == "" )
            {
                m_retire_date = " ";
            }
            m_retire_date = value;
        }
    }

    /// <summary>
    /// Gets or sets the 'EMAIL_ID' value.
    /// </summary>
    public string EMAIL_ID
    {
        get
        {
            if (m_email_id == null )
            {
                m_email_id = " ";
            }
            return m_email_id;
        }
        set
        {
            if (value == null || value == "" )
            {
                m_email_id = " ";
            }
            m_email_id = value;
        }
    }

    /// <summary>
    /// Gets or sets the 'BIRTHDAY' value.
    /// </summary>
    public string BIRTHDAY
    {
        get
        {
            if (m_birthday == null )
            {
                m_birthday = " ";
            }
            return m_birthday;
        }
        set
        {
            if (value == null || value == "" )
            {
                m_birthday = " ";
            }
            m_birthday = value;
        }
    }

    /// <summary>
    /// Gets or sets the 'SEX_FLAG' value.
    /// </summary>
    public string SEX_FLAG
    {
        get
        {
            if (m_sex_flag == null )
            {
                m_sex_flag = " ";
            }
            return m_sex_flag;
        }
        set
        {
            if (value == null || value == "" )
            {
                m_sex_flag = " ";
            }
            m_sex_flag = value;
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
    public DBC_MSECUSRDEF(ref DB_Common dbc)
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
        USER_ID = " ";
        USER_DESC = " ";
        PASSWORD = " ";
        CHG_PASS_FLAG = " ";
        USER_GRP_1 = " ";
        USER_GRP_2 = " ";
        USER_GRP_3 = " ";
        USER_GRP_4 = " ";
        USER_GRP_5 = " ";
        USER_GRP_6 = " ";
        USER_GRP_7 = " ";
        USER_GRP_8 = " ";
        USER_GRP_9 = " ";
        USER_GRP_10 = " ";
        USER_CMF_1 = " ";
        USER_CMF_2 = " ";
        USER_CMF_3 = " ";
        USER_CMF_4 = " ";
        USER_CMF_5 = " ";
        USER_CMF_6 = " ";
        USER_CMF_7 = " ";
        USER_CMF_8 = " ";
        USER_CMF_9 = " ";
        USER_CMF_10 = " ";
        SEC_GRP_ID = " ";
        PHONE_OFFICE = " ";
        PHONE_MOBILE = " ";
        PHONE_HOME = " ";
        PHONE_OTHER = " ";
        EXPIRE_DATE = " ";
        PASS_EXPIRE_DATE = " ";
        ENTER_DATE = " ";
        RETIRE_DATE = " ";
        EMAIL_ID = " ";
        BIRTHDAY = " ";
        SEX_FLAG = " ";
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
        DBC_MSECUSRDEF MSECUSRDEF = null;
        try
        {
            MSECUSRDEF = new DBC_MSECUSRDEF(ref _dbc);

            MSECUSRDEF.FACTORY = this.FACTORY;
            MSECUSRDEF.USER_ID = this.USER_ID;
            MSECUSRDEF.USER_DESC = this.USER_DESC;
            MSECUSRDEF.PASSWORD = this.PASSWORD;
            MSECUSRDEF.CHG_PASS_FLAG = this.CHG_PASS_FLAG;
            MSECUSRDEF.USER_GRP_1 = this.USER_GRP_1;
            MSECUSRDEF.USER_GRP_2 = this.USER_GRP_2;
            MSECUSRDEF.USER_GRP_3 = this.USER_GRP_3;
            MSECUSRDEF.USER_GRP_4 = this.USER_GRP_4;
            MSECUSRDEF.USER_GRP_5 = this.USER_GRP_5;
            MSECUSRDEF.USER_GRP_6 = this.USER_GRP_6;
            MSECUSRDEF.USER_GRP_7 = this.USER_GRP_7;
            MSECUSRDEF.USER_GRP_8 = this.USER_GRP_8;
            MSECUSRDEF.USER_GRP_9 = this.USER_GRP_9;
            MSECUSRDEF.USER_GRP_10 = this.USER_GRP_10;
            MSECUSRDEF.USER_CMF_1 = this.USER_CMF_1;
            MSECUSRDEF.USER_CMF_2 = this.USER_CMF_2;
            MSECUSRDEF.USER_CMF_3 = this.USER_CMF_3;
            MSECUSRDEF.USER_CMF_4 = this.USER_CMF_4;
            MSECUSRDEF.USER_CMF_5 = this.USER_CMF_5;
            MSECUSRDEF.USER_CMF_6 = this.USER_CMF_6;
            MSECUSRDEF.USER_CMF_7 = this.USER_CMF_7;
            MSECUSRDEF.USER_CMF_8 = this.USER_CMF_8;
            MSECUSRDEF.USER_CMF_9 = this.USER_CMF_9;
            MSECUSRDEF.USER_CMF_10 = this.USER_CMF_10;
            MSECUSRDEF.SEC_GRP_ID = this.SEC_GRP_ID;
            MSECUSRDEF.PHONE_OFFICE = this.PHONE_OFFICE;
            MSECUSRDEF.PHONE_MOBILE = this.PHONE_MOBILE;
            MSECUSRDEF.PHONE_HOME = this.PHONE_HOME;
            MSECUSRDEF.PHONE_OTHER = this.PHONE_OTHER;
            MSECUSRDEF.EXPIRE_DATE = this.EXPIRE_DATE;
            MSECUSRDEF.PASS_EXPIRE_DATE = this.PASS_EXPIRE_DATE;
            MSECUSRDEF.ENTER_DATE = this.ENTER_DATE;
            MSECUSRDEF.RETIRE_DATE = this.RETIRE_DATE;
            MSECUSRDEF.EMAIL_ID = this.EMAIL_ID;
            MSECUSRDEF.BIRTHDAY = this.BIRTHDAY;
            MSECUSRDEF.SEX_FLAG = this.SEX_FLAG;
            MSECUSRDEF.CREATE_USER_ID = this.CREATE_USER_ID;
            MSECUSRDEF.CREATE_TIME = this.CREATE_TIME;
            MSECUSRDEF.UPDATE_USER_ID = this.UPDATE_USER_ID;
            MSECUSRDEF.UPDATE_TIME = this.UPDATE_TIME;
        }
        catch(Exception ex)
        {
            if (ex.GetType().ToString() == "System.Data.OleDb.OleDbException")
                _dbc.gErrors.SetErrors(((OleDbException)ex).Errors);
            else
                _dbc.gErrors.SetErrors(ex);

            return null;
        }

        return MSECUSRDEF;
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
            USER_ID = (string)(Row["USER_ID"]);
            USER_DESC = (string)(Row["USER_DESC"]);
            PASSWORD = (string)(Row["PASSWORD"]);
            CHG_PASS_FLAG = (string)(Row["CHG_PASS_FLAG"]);
            USER_GRP_1 = (string)(Row["USER_GRP_1"]);
            USER_GRP_2 = (string)(Row["USER_GRP_2"]);
            USER_GRP_3 = (string)(Row["USER_GRP_3"]);
            USER_GRP_4 = (string)(Row["USER_GRP_4"]);
            USER_GRP_5 = (string)(Row["USER_GRP_5"]);
            USER_GRP_6 = (string)(Row["USER_GRP_6"]);
            USER_GRP_7 = (string)(Row["USER_GRP_7"]);
            USER_GRP_8 = (string)(Row["USER_GRP_8"]);
            USER_GRP_9 = (string)(Row["USER_GRP_9"]);
            USER_GRP_10 = (string)(Row["USER_GRP_10"]);
            USER_CMF_1 = (string)(Row["USER_CMF_1"]);
            USER_CMF_2 = (string)(Row["USER_CMF_2"]);
            USER_CMF_3 = (string)(Row["USER_CMF_3"]);
            USER_CMF_4 = (string)(Row["USER_CMF_4"]);
            USER_CMF_5 = (string)(Row["USER_CMF_5"]);
            USER_CMF_6 = (string)(Row["USER_CMF_6"]);
            USER_CMF_7 = (string)(Row["USER_CMF_7"]);
            USER_CMF_8 = (string)(Row["USER_CMF_8"]);
            USER_CMF_9 = (string)(Row["USER_CMF_9"]);
            USER_CMF_10 = (string)(Row["USER_CMF_10"]);
            SEC_GRP_ID = (string)(Row["SEC_GRP_ID"]);
            PHONE_OFFICE = (string)(Row["PHONE_OFFICE"]);
            PHONE_MOBILE = (string)(Row["PHONE_MOBILE"]);
            PHONE_HOME = (string)(Row["PHONE_HOME"]);
            PHONE_OTHER = (string)(Row["PHONE_OTHER"]);
            EXPIRE_DATE = (string)(Row["EXPIRE_DATE"]);
            PASS_EXPIRE_DATE = (string)(Row["PASS_EXPIRE_DATE"]);
            ENTER_DATE = (string)(Row["ENTER_DATE"]);
            RETIRE_DATE = (string)(Row["RETIRE_DATE"]);
            EMAIL_ID = (string)(Row["EMAIL_ID"]);
            BIRTHDAY = (string)(Row["BIRTHDAY"]);
            SEX_FLAG = (string)(Row["SEX_FLAG"]);
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
                    strQuery = "SELECT * FROM MSECUSRDEF"
                        + " WHERE FACTORY=?"
                        + " AND USER_ID=?";

                    // Add Parameters
                    adoAdapter.SelectCommand.Parameters.Add("@FACTORY", OleDbType.VarChar).Value = FACTORY;
                    adoAdapter.SelectCommand.Parameters.Add("@USER_ID", OleDbType.VarChar).Value = USER_ID;
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
                    case "USER_ID":
                        USER_ID = (string)(adoDataTable.Rows[0]["USER_ID"]);
                        break;
                    case "USER_DESC":
                        USER_DESC = (string)(adoDataTable.Rows[0]["USER_DESC"]);
                        break;
                    case "PASSWORD":
                        PASSWORD = (string)(adoDataTable.Rows[0]["PASSWORD"]);
                        break;
                    case "CHG_PASS_FLAG":
                        CHG_PASS_FLAG = (string)(adoDataTable.Rows[0]["CHG_PASS_FLAG"]);
                        break;
                    case "USER_GRP_1":
                        USER_GRP_1 = (string)(adoDataTable.Rows[0]["USER_GRP_1"]);
                        break;
                    case "USER_GRP_2":
                        USER_GRP_2 = (string)(adoDataTable.Rows[0]["USER_GRP_2"]);
                        break;
                    case "USER_GRP_3":
                        USER_GRP_3 = (string)(adoDataTable.Rows[0]["USER_GRP_3"]);
                        break;
                    case "USER_GRP_4":
                        USER_GRP_4 = (string)(adoDataTable.Rows[0]["USER_GRP_4"]);
                        break;
                    case "USER_GRP_5":
                        USER_GRP_5 = (string)(adoDataTable.Rows[0]["USER_GRP_5"]);
                        break;
                    case "USER_GRP_6":
                        USER_GRP_6 = (string)(adoDataTable.Rows[0]["USER_GRP_6"]);
                        break;
                    case "USER_GRP_7":
                        USER_GRP_7 = (string)(adoDataTable.Rows[0]["USER_GRP_7"]);
                        break;
                    case "USER_GRP_8":
                        USER_GRP_8 = (string)(adoDataTable.Rows[0]["USER_GRP_8"]);
                        break;
                    case "USER_GRP_9":
                        USER_GRP_9 = (string)(adoDataTable.Rows[0]["USER_GRP_9"]);
                        break;
                    case "USER_GRP_10":
                        USER_GRP_10 = (string)(adoDataTable.Rows[0]["USER_GRP_10"]);
                        break;
                    case "USER_CMF_1":
                        USER_CMF_1 = (string)(adoDataTable.Rows[0]["USER_CMF_1"]);
                        break;
                    case "USER_CMF_2":
                        USER_CMF_2 = (string)(adoDataTable.Rows[0]["USER_CMF_2"]);
                        break;
                    case "USER_CMF_3":
                        USER_CMF_3 = (string)(adoDataTable.Rows[0]["USER_CMF_3"]);
                        break;
                    case "USER_CMF_4":
                        USER_CMF_4 = (string)(adoDataTable.Rows[0]["USER_CMF_4"]);
                        break;
                    case "USER_CMF_5":
                        USER_CMF_5 = (string)(adoDataTable.Rows[0]["USER_CMF_5"]);
                        break;
                    case "USER_CMF_6":
                        USER_CMF_6 = (string)(adoDataTable.Rows[0]["USER_CMF_6"]);
                        break;
                    case "USER_CMF_7":
                        USER_CMF_7 = (string)(adoDataTable.Rows[0]["USER_CMF_7"]);
                        break;
                    case "USER_CMF_8":
                        USER_CMF_8 = (string)(adoDataTable.Rows[0]["USER_CMF_8"]);
                        break;
                    case "USER_CMF_9":
                        USER_CMF_9 = (string)(adoDataTable.Rows[0]["USER_CMF_9"]);
                        break;
                    case "USER_CMF_10":
                        USER_CMF_10 = (string)(adoDataTable.Rows[0]["USER_CMF_10"]);
                        break;
                    case "SEC_GRP_ID":
                        SEC_GRP_ID = (string)(adoDataTable.Rows[0]["SEC_GRP_ID"]);
                        break;
                    case "PHONE_OFFICE":
                        PHONE_OFFICE = (string)(adoDataTable.Rows[0]["PHONE_OFFICE"]);
                        break;
                    case "PHONE_MOBILE":
                        PHONE_MOBILE = (string)(adoDataTable.Rows[0]["PHONE_MOBILE"]);
                        break;
                    case "PHONE_HOME":
                        PHONE_HOME = (string)(adoDataTable.Rows[0]["PHONE_HOME"]);
                        break;
                    case "PHONE_OTHER":
                        PHONE_OTHER = (string)(adoDataTable.Rows[0]["PHONE_OTHER"]);
                        break;
                    case "EXPIRE_DATE":
                        EXPIRE_DATE = (string)(adoDataTable.Rows[0]["EXPIRE_DATE"]);
                        break;
                    case "PASS_EXPIRE_DATE":
                        PASS_EXPIRE_DATE = (string)(adoDataTable.Rows[0]["PASS_EXPIRE_DATE"]);
                        break;
                    case "ENTER_DATE":
                        ENTER_DATE = (string)(adoDataTable.Rows[0]["ENTER_DATE"]);
                        break;
                    case "RETIRE_DATE":
                        RETIRE_DATE = (string)(adoDataTable.Rows[0]["RETIRE_DATE"]);
                        break;
                    case "EMAIL_ID":
                        EMAIL_ID = (string)(adoDataTable.Rows[0]["EMAIL_ID"]);
                        break;
                    case "BIRTHDAY":
                        BIRTHDAY = (string)(adoDataTable.Rows[0]["BIRTHDAY"]);
                        break;
                    case "SEX_FLAG":
                        SEX_FLAG = (string)(adoDataTable.Rows[0]["SEX_FLAG"]);
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
                        strQuery = "SELECT * FROM MSECUSRDEF"
                            + " WITH (UPDLOCK) WHERE FACTORY=?"
                            + " AND USER_ID=?";
                    }
                    else if (_dbc.gDbType == (int)DB_TYPE.ORACLE)
                    {
                        strQuery = "SELECT * FROM MSECUSRDEF"
                            + " WHERE FACTORY=?"
                            + " AND USER_ID=?"
                            + " FOR UPDATE";
                    }
                    else if (_dbc.gDbType == (int)DB_TYPE.DB2) 
                    {
                    }

                    // Add Parameters
                    adoAdapter.SelectCommand.Parameters.Add("@FACTORY", OleDbType.VarChar).Value = FACTORY;
                    adoAdapter.SelectCommand.Parameters.Add("@USER_ID", OleDbType.VarChar).Value = USER_ID;
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
                    case "USER_ID":
                        USER_ID = (string)(adoDataTable.Rows[0]["USER_ID"]);
                        break;
                    case "USER_DESC":
                        USER_DESC = (string)(adoDataTable.Rows[0]["USER_DESC"]);
                        break;
                    case "PASSWORD":
                        PASSWORD = (string)(adoDataTable.Rows[0]["PASSWORD"]);
                        break;
                    case "CHG_PASS_FLAG":
                        CHG_PASS_FLAG = (string)(adoDataTable.Rows[0]["CHG_PASS_FLAG"]);
                        break;
                    case "USER_GRP_1":
                        USER_GRP_1 = (string)(adoDataTable.Rows[0]["USER_GRP_1"]);
                        break;
                    case "USER_GRP_2":
                        USER_GRP_2 = (string)(adoDataTable.Rows[0]["USER_GRP_2"]);
                        break;
                    case "USER_GRP_3":
                        USER_GRP_3 = (string)(adoDataTable.Rows[0]["USER_GRP_3"]);
                        break;
                    case "USER_GRP_4":
                        USER_GRP_4 = (string)(adoDataTable.Rows[0]["USER_GRP_4"]);
                        break;
                    case "USER_GRP_5":
                        USER_GRP_5 = (string)(adoDataTable.Rows[0]["USER_GRP_5"]);
                        break;
                    case "USER_GRP_6":
                        USER_GRP_6 = (string)(adoDataTable.Rows[0]["USER_GRP_6"]);
                        break;
                    case "USER_GRP_7":
                        USER_GRP_7 = (string)(adoDataTable.Rows[0]["USER_GRP_7"]);
                        break;
                    case "USER_GRP_8":
                        USER_GRP_8 = (string)(adoDataTable.Rows[0]["USER_GRP_8"]);
                        break;
                    case "USER_GRP_9":
                        USER_GRP_9 = (string)(adoDataTable.Rows[0]["USER_GRP_9"]);
                        break;
                    case "USER_GRP_10":
                        USER_GRP_10 = (string)(adoDataTable.Rows[0]["USER_GRP_10"]);
                        break;
                    case "USER_CMF_1":
                        USER_CMF_1 = (string)(adoDataTable.Rows[0]["USER_CMF_1"]);
                        break;
                    case "USER_CMF_2":
                        USER_CMF_2 = (string)(adoDataTable.Rows[0]["USER_CMF_2"]);
                        break;
                    case "USER_CMF_3":
                        USER_CMF_3 = (string)(adoDataTable.Rows[0]["USER_CMF_3"]);
                        break;
                    case "USER_CMF_4":
                        USER_CMF_4 = (string)(adoDataTable.Rows[0]["USER_CMF_4"]);
                        break;
                    case "USER_CMF_5":
                        USER_CMF_5 = (string)(adoDataTable.Rows[0]["USER_CMF_5"]);
                        break;
                    case "USER_CMF_6":
                        USER_CMF_6 = (string)(adoDataTable.Rows[0]["USER_CMF_6"]);
                        break;
                    case "USER_CMF_7":
                        USER_CMF_7 = (string)(adoDataTable.Rows[0]["USER_CMF_7"]);
                        break;
                    case "USER_CMF_8":
                        USER_CMF_8 = (string)(adoDataTable.Rows[0]["USER_CMF_8"]);
                        break;
                    case "USER_CMF_9":
                        USER_CMF_9 = (string)(adoDataTable.Rows[0]["USER_CMF_9"]);
                        break;
                    case "USER_CMF_10":
                        USER_CMF_10 = (string)(adoDataTable.Rows[0]["USER_CMF_10"]);
                        break;
                    case "SEC_GRP_ID":
                        SEC_GRP_ID = (string)(adoDataTable.Rows[0]["SEC_GRP_ID"]);
                        break;
                    case "PHONE_OFFICE":
                        PHONE_OFFICE = (string)(adoDataTable.Rows[0]["PHONE_OFFICE"]);
                        break;
                    case "PHONE_MOBILE":
                        PHONE_MOBILE = (string)(adoDataTable.Rows[0]["PHONE_MOBILE"]);
                        break;
                    case "PHONE_HOME":
                        PHONE_HOME = (string)(adoDataTable.Rows[0]["PHONE_HOME"]);
                        break;
                    case "PHONE_OTHER":
                        PHONE_OTHER = (string)(adoDataTable.Rows[0]["PHONE_OTHER"]);
                        break;
                    case "EXPIRE_DATE":
                        EXPIRE_DATE = (string)(adoDataTable.Rows[0]["EXPIRE_DATE"]);
                        break;
                    case "PASS_EXPIRE_DATE":
                        PASS_EXPIRE_DATE = (string)(adoDataTable.Rows[0]["PASS_EXPIRE_DATE"]);
                        break;
                    case "ENTER_DATE":
                        ENTER_DATE = (string)(adoDataTable.Rows[0]["ENTER_DATE"]);
                        break;
                    case "RETIRE_DATE":
                        RETIRE_DATE = (string)(adoDataTable.Rows[0]["RETIRE_DATE"]);
                        break;
                    case "EMAIL_ID":
                        EMAIL_ID = (string)(adoDataTable.Rows[0]["EMAIL_ID"]);
                        break;
                    case "BIRTHDAY":
                        BIRTHDAY = (string)(adoDataTable.Rows[0]["BIRTHDAY"]);
                        break;
                    case "SEX_FLAG":
                        SEX_FLAG = (string)(adoDataTable.Rows[0]["SEX_FLAG"]);
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
                    strQuery = "SELECT COUNT(*) FROM MSECUSRDEF"
                        + " WHERE FACTORY=?"
                        + " AND USER_ID=?";

                    // Add Parameters
                    adoCommand.Parameters.Add("@FACTORY", OleDbType.VarChar).Value = FACTORY;
                    adoCommand.Parameters.Add("@USER_ID", OleDbType.VarChar).Value = USER_ID;
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
                    strQuery = "DELETE FROM MSECUSRDEF"
                        + " WHERE FACTORY=?"
                        + " AND USER_ID=?";

                    // Add Parameters
                    adoCommand.Parameters.Add("@FACTORY", OleDbType.VarChar).Value = FACTORY;
                    adoCommand.Parameters.Add("@USER_ID", OleDbType.VarChar).Value = USER_ID;
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

            strQuery = "INSERT INTO MSECUSRDEF ("
                + " FACTORY, USER_ID, USER_DESC, PASSWORD, CHG_PASS_FLAG,"
                + " USER_GRP_1, USER_GRP_2, USER_GRP_3, USER_GRP_4, USER_GRP_5,"
                + " USER_GRP_6, USER_GRP_7, USER_GRP_8, USER_GRP_9, USER_GRP_10,"
                + " USER_CMF_1, USER_CMF_2, USER_CMF_3, USER_CMF_4, USER_CMF_5,"
                + " USER_CMF_6, USER_CMF_7, USER_CMF_8, USER_CMF_9, USER_CMF_10,"
                + " SEC_GRP_ID, PHONE_OFFICE, PHONE_MOBILE, PHONE_HOME, PHONE_OTHER,"
                + " EXPIRE_DATE, PASS_EXPIRE_DATE, ENTER_DATE, RETIRE_DATE, EMAIL_ID,"
                + " BIRTHDAY, SEX_FLAG, CREATE_USER_ID, CREATE_TIME, UPDATE_USER_ID,"
                + " UPDATE_TIME)"
                + " VALUES ("
                + " ?, ?, ?, ?, ?, ?, ?, ?, ?, ?,"
                + " ?, ?, ?, ?, ?, ?, ?, ?, ?, ?,"
                + " ?, ?, ?, ?, ?, ?, ?, ?, ?, ?,"
                + " ?, ?, ?, ?, ?, ?, ?, ?, ?, ?,"
                + " ?)";

            // Add Parameters
            adoCommand.Parameters.Add("@FACTORY", OleDbType.VarChar).Value = FACTORY;
            adoCommand.Parameters.Add("@USER_ID", OleDbType.VarChar).Value = USER_ID;
            adoCommand.Parameters.Add("@USER_DESC", OleDbType.VarChar).Value = USER_DESC;
            adoCommand.Parameters.Add("@PASSWORD", OleDbType.VarChar).Value = PASSWORD;
            adoCommand.Parameters.Add("@CHG_PASS_FLAG", OleDbType.VarChar).Value = CHG_PASS_FLAG;
            adoCommand.Parameters.Add("@USER_GRP_1", OleDbType.VarChar).Value = USER_GRP_1;
            adoCommand.Parameters.Add("@USER_GRP_2", OleDbType.VarChar).Value = USER_GRP_2;
            adoCommand.Parameters.Add("@USER_GRP_3", OleDbType.VarChar).Value = USER_GRP_3;
            adoCommand.Parameters.Add("@USER_GRP_4", OleDbType.VarChar).Value = USER_GRP_4;
            adoCommand.Parameters.Add("@USER_GRP_5", OleDbType.VarChar).Value = USER_GRP_5;
            adoCommand.Parameters.Add("@USER_GRP_6", OleDbType.VarChar).Value = USER_GRP_6;
            adoCommand.Parameters.Add("@USER_GRP_7", OleDbType.VarChar).Value = USER_GRP_7;
            adoCommand.Parameters.Add("@USER_GRP_8", OleDbType.VarChar).Value = USER_GRP_8;
            adoCommand.Parameters.Add("@USER_GRP_9", OleDbType.VarChar).Value = USER_GRP_9;
            adoCommand.Parameters.Add("@USER_GRP_10", OleDbType.VarChar).Value = USER_GRP_10;
            adoCommand.Parameters.Add("@USER_CMF_1", OleDbType.VarChar).Value = USER_CMF_1;
            adoCommand.Parameters.Add("@USER_CMF_2", OleDbType.VarChar).Value = USER_CMF_2;
            adoCommand.Parameters.Add("@USER_CMF_3", OleDbType.VarChar).Value = USER_CMF_3;
            adoCommand.Parameters.Add("@USER_CMF_4", OleDbType.VarChar).Value = USER_CMF_4;
            adoCommand.Parameters.Add("@USER_CMF_5", OleDbType.VarChar).Value = USER_CMF_5;
            adoCommand.Parameters.Add("@USER_CMF_6", OleDbType.VarChar).Value = USER_CMF_6;
            adoCommand.Parameters.Add("@USER_CMF_7", OleDbType.VarChar).Value = USER_CMF_7;
            adoCommand.Parameters.Add("@USER_CMF_8", OleDbType.VarChar).Value = USER_CMF_8;
            adoCommand.Parameters.Add("@USER_CMF_9", OleDbType.VarChar).Value = USER_CMF_9;
            adoCommand.Parameters.Add("@USER_CMF_10", OleDbType.VarChar).Value = USER_CMF_10;
            adoCommand.Parameters.Add("@SEC_GRP_ID", OleDbType.VarChar).Value = SEC_GRP_ID;
            adoCommand.Parameters.Add("@PHONE_OFFICE", OleDbType.VarChar).Value = PHONE_OFFICE;
            adoCommand.Parameters.Add("@PHONE_MOBILE", OleDbType.VarChar).Value = PHONE_MOBILE;
            adoCommand.Parameters.Add("@PHONE_HOME", OleDbType.VarChar).Value = PHONE_HOME;
            adoCommand.Parameters.Add("@PHONE_OTHER", OleDbType.VarChar).Value = PHONE_OTHER;
            adoCommand.Parameters.Add("@EXPIRE_DATE", OleDbType.VarChar).Value = EXPIRE_DATE;
            adoCommand.Parameters.Add("@PASS_EXPIRE_DATE", OleDbType.VarChar).Value = PASS_EXPIRE_DATE;
            adoCommand.Parameters.Add("@ENTER_DATE", OleDbType.VarChar).Value = ENTER_DATE;
            adoCommand.Parameters.Add("@RETIRE_DATE", OleDbType.VarChar).Value = RETIRE_DATE;
            adoCommand.Parameters.Add("@EMAIL_ID", OleDbType.VarChar).Value = EMAIL_ID;
            adoCommand.Parameters.Add("@BIRTHDAY", OleDbType.VarChar).Value = BIRTHDAY;
            adoCommand.Parameters.Add("@SEX_FLAG", OleDbType.VarChar).Value = SEX_FLAG;
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
                    strQuery = "UPDATE MSECUSRDEF SET"
                        + " USER_DESC=?, PASSWORD=?, CHG_PASS_FLAG=?, USER_GRP_1=?, USER_GRP_2=?,"
                        + " USER_GRP_3=?, USER_GRP_4=?, USER_GRP_5=?, USER_GRP_6=?, USER_GRP_7=?,"
                        + " USER_GRP_8=?, USER_GRP_9=?, USER_GRP_10=?, USER_CMF_1=?, USER_CMF_2=?,"
                        + " USER_CMF_3=?, USER_CMF_4=?, USER_CMF_5=?, USER_CMF_6=?, USER_CMF_7=?,"
                        + " USER_CMF_8=?, USER_CMF_9=?, USER_CMF_10=?, SEC_GRP_ID=?, PHONE_OFFICE=?,"
                        + " PHONE_MOBILE=?, PHONE_HOME=?, PHONE_OTHER=?, EXPIRE_DATE=?, PASS_EXPIRE_DATE=?,"
                        + " ENTER_DATE=?, RETIRE_DATE=?, EMAIL_ID=?, BIRTHDAY=?, SEX_FLAG=?,"
                        + " CREATE_USER_ID=?, CREATE_TIME=?, UPDATE_USER_ID=?, UPDATE_TIME=?"
                        + " WHERE FACTORY=?"
                        + " AND USER_ID=?";

                    // Add Parameters
                    adoCommand.Parameters.Add("@USER_DESC", OleDbType.VarChar).Value = USER_DESC;
                    adoCommand.Parameters.Add("@PASSWORD", OleDbType.VarChar).Value = PASSWORD;
                    adoCommand.Parameters.Add("@CHG_PASS_FLAG", OleDbType.VarChar).Value = CHG_PASS_FLAG;
                    adoCommand.Parameters.Add("@USER_GRP_1", OleDbType.VarChar).Value = USER_GRP_1;
                    adoCommand.Parameters.Add("@USER_GRP_2", OleDbType.VarChar).Value = USER_GRP_2;
                    adoCommand.Parameters.Add("@USER_GRP_3", OleDbType.VarChar).Value = USER_GRP_3;
                    adoCommand.Parameters.Add("@USER_GRP_4", OleDbType.VarChar).Value = USER_GRP_4;
                    adoCommand.Parameters.Add("@USER_GRP_5", OleDbType.VarChar).Value = USER_GRP_5;
                    adoCommand.Parameters.Add("@USER_GRP_6", OleDbType.VarChar).Value = USER_GRP_6;
                    adoCommand.Parameters.Add("@USER_GRP_7", OleDbType.VarChar).Value = USER_GRP_7;
                    adoCommand.Parameters.Add("@USER_GRP_8", OleDbType.VarChar).Value = USER_GRP_8;
                    adoCommand.Parameters.Add("@USER_GRP_9", OleDbType.VarChar).Value = USER_GRP_9;
                    adoCommand.Parameters.Add("@USER_GRP_10", OleDbType.VarChar).Value = USER_GRP_10;
                    adoCommand.Parameters.Add("@USER_CMF_1", OleDbType.VarChar).Value = USER_CMF_1;
                    adoCommand.Parameters.Add("@USER_CMF_2", OleDbType.VarChar).Value = USER_CMF_2;
                    adoCommand.Parameters.Add("@USER_CMF_3", OleDbType.VarChar).Value = USER_CMF_3;
                    adoCommand.Parameters.Add("@USER_CMF_4", OleDbType.VarChar).Value = USER_CMF_4;
                    adoCommand.Parameters.Add("@USER_CMF_5", OleDbType.VarChar).Value = USER_CMF_5;
                    adoCommand.Parameters.Add("@USER_CMF_6", OleDbType.VarChar).Value = USER_CMF_6;
                    adoCommand.Parameters.Add("@USER_CMF_7", OleDbType.VarChar).Value = USER_CMF_7;
                    adoCommand.Parameters.Add("@USER_CMF_8", OleDbType.VarChar).Value = USER_CMF_8;
                    adoCommand.Parameters.Add("@USER_CMF_9", OleDbType.VarChar).Value = USER_CMF_9;
                    adoCommand.Parameters.Add("@USER_CMF_10", OleDbType.VarChar).Value = USER_CMF_10;
                    adoCommand.Parameters.Add("@SEC_GRP_ID", OleDbType.VarChar).Value = SEC_GRP_ID;
                    adoCommand.Parameters.Add("@PHONE_OFFICE", OleDbType.VarChar).Value = PHONE_OFFICE;
                    adoCommand.Parameters.Add("@PHONE_MOBILE", OleDbType.VarChar).Value = PHONE_MOBILE;
                    adoCommand.Parameters.Add("@PHONE_HOME", OleDbType.VarChar).Value = PHONE_HOME;
                    adoCommand.Parameters.Add("@PHONE_OTHER", OleDbType.VarChar).Value = PHONE_OTHER;
                    adoCommand.Parameters.Add("@EXPIRE_DATE", OleDbType.VarChar).Value = EXPIRE_DATE;
                    adoCommand.Parameters.Add("@PASS_EXPIRE_DATE", OleDbType.VarChar).Value = PASS_EXPIRE_DATE;
                    adoCommand.Parameters.Add("@ENTER_DATE", OleDbType.VarChar).Value = ENTER_DATE;
                    adoCommand.Parameters.Add("@RETIRE_DATE", OleDbType.VarChar).Value = RETIRE_DATE;
                    adoCommand.Parameters.Add("@EMAIL_ID", OleDbType.VarChar).Value = EMAIL_ID;
                    adoCommand.Parameters.Add("@BIRTHDAY", OleDbType.VarChar).Value = BIRTHDAY;
                    adoCommand.Parameters.Add("@SEX_FLAG", OleDbType.VarChar).Value = SEX_FLAG;
                    adoCommand.Parameters.Add("@CREATE_USER_ID", OleDbType.VarChar).Value = CREATE_USER_ID;
                    adoCommand.Parameters.Add("@CREATE_TIME", OleDbType.VarChar).Value = CREATE_TIME;
                    adoCommand.Parameters.Add("@UPDATE_USER_ID", OleDbType.VarChar).Value = UPDATE_USER_ID;
                    adoCommand.Parameters.Add("@UPDATE_TIME", OleDbType.VarChar).Value = UPDATE_TIME;

                    // Add Parameters for Condition
                    adoCommand.Parameters.Add("@C_FACTORY", OleDbType.VarChar).Value = FACTORY;
                    adoCommand.Parameters.Add("@C_USER_ID", OleDbType.VarChar).Value = USER_ID;
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
                    strQuery = "SELECT * FROM MSECUSRDEF"
                        + " ORDER BY FACTORY ASC"
                        + " , USER_ID ASC";

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

