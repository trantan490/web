//-----------------------------------------------------------------------------
//
//   System      : MES
//   File Name   : DBC_MFMBENVDEF.cs
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
public class DBC_MFMBENVDEF
    : ICloneable
{


#region " Fields Definition "

    // Fields
    private string m_factory;                                // VARCHAR(10)
    private string m_create_user_id;                         // VARCHAR(20)
    private string m_create_time;                            // VARCHAR(14)
    private string m_update_user_id;                         // VARCHAR(20)
    private string m_update_time;                            // VARCHAR(14)
    private string m_font_family;                            // VARCHAR(20)
    private int m_res_width;                                 // INTEGER(6)
    private int m_res_height;                                // INTEGER(6)
    private int m_rtg_width;                                 // INTEGER(6)
    private int m_rtg_height;                                // INTEGER(6)
    private int m_elp_width;                                 // INTEGER(6)
    private int m_elp_height;                                // INTEGER(6)
    private int m_tri_width;                                 // INTEGER(6)
    private int m_tri_height;                                // INTEGER(6)
    private int m_ver_width;                                 // INTEGER(6)
    private int m_ver_height;                                // INTEGER(6)
    private int m_hor_width;                                 // INTEGER(6)
    private int m_hor_height;                                // INTEGER(6)
    private int m_pie1_width;                                // INTEGER(6)
    private int m_pie1_height;                               // INTEGER(6)
    private int m_pie2_width;                                // INTEGER(6)
    private int m_pie2_height;                               // INTEGER(6)
    private int m_pie3_width;                                // INTEGER(6)
    private int m_pie3_height;                               // INTEGER(6)
    private int m_pie4_width;                                // INTEGER(6)
    private int m_pie4_height;                               // INTEGER(6)
    private int m_layout_width;                              // INTEGER(6)
    private int m_layout_height;                             // INTEGER(6)
    private int m_udr_width;                                 // INTEGER(6)
    private int m_udr_height;                                // INTEGER(6)
    private string m_event_color_flag;                       // VARCHAR(1)
    private string m_signal_flag;                            // VARCHAR(1)
    private int m_text_size;                                 // INTEGER(2)
    private int m_text_color;                                // INTEGER(8)
    private int m_back_color;                                // INTEGER(8)

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
    /// Gets or sets the 'FONT_FAMILY' value.
    /// </summary>
    public string FONT_FAMILY
    {
        get
        {
            if (m_font_family == null )
            {
                m_font_family = " ";
            }
            return m_font_family;
        }
        set
        {
            if (value == null || value == "" )
            {
                m_font_family = " ";
            }
            m_font_family = value;
        }
    }

    /// <summary>
    /// Gets or sets the 'RES_WIDTH' value.
    /// </summary>
    public int RES_WIDTH
    {
        get
        {
            return m_res_width;
        }
        set
        {
            m_res_width = value;
        }
    }

    /// <summary>
    /// Gets or sets the 'RES_HEIGHT' value.
    /// </summary>
    public int RES_HEIGHT
    {
        get
        {
            return m_res_height;
        }
        set
        {
            m_res_height = value;
        }
    }

    /// <summary>
    /// Gets or sets the 'RTG_WIDTH' value.
    /// </summary>
    public int RTG_WIDTH
    {
        get
        {
            return m_rtg_width;
        }
        set
        {
            m_rtg_width = value;
        }
    }

    /// <summary>
    /// Gets or sets the 'RTG_HEIGHT' value.
    /// </summary>
    public int RTG_HEIGHT
    {
        get
        {
            return m_rtg_height;
        }
        set
        {
            m_rtg_height = value;
        }
    }

    /// <summary>
    /// Gets or sets the 'ELP_WIDTH' value.
    /// </summary>
    public int ELP_WIDTH
    {
        get
        {
            return m_elp_width;
        }
        set
        {
            m_elp_width = value;
        }
    }

    /// <summary>
    /// Gets or sets the 'ELP_HEIGHT' value.
    /// </summary>
    public int ELP_HEIGHT
    {
        get
        {
            return m_elp_height;
        }
        set
        {
            m_elp_height = value;
        }
    }

    /// <summary>
    /// Gets or sets the 'TRI_WIDTH' value.
    /// </summary>
    public int TRI_WIDTH
    {
        get
        {
            return m_tri_width;
        }
        set
        {
            m_tri_width = value;
        }
    }

    /// <summary>
    /// Gets or sets the 'TRI_HEIGHT' value.
    /// </summary>
    public int TRI_HEIGHT
    {
        get
        {
            return m_tri_height;
        }
        set
        {
            m_tri_height = value;
        }
    }

    /// <summary>
    /// Gets or sets the 'VER_WIDTH' value.
    /// </summary>
    public int VER_WIDTH
    {
        get
        {
            return m_ver_width;
        }
        set
        {
            m_ver_width = value;
        }
    }

    /// <summary>
    /// Gets or sets the 'VER_HEIGHT' value.
    /// </summary>
    public int VER_HEIGHT
    {
        get
        {
            return m_ver_height;
        }
        set
        {
            m_ver_height = value;
        }
    }

    /// <summary>
    /// Gets or sets the 'HOR_WIDTH' value.
    /// </summary>
    public int HOR_WIDTH
    {
        get
        {
            return m_hor_width;
        }
        set
        {
            m_hor_width = value;
        }
    }

    /// <summary>
    /// Gets or sets the 'HOR_HEIGHT' value.
    /// </summary>
    public int HOR_HEIGHT
    {
        get
        {
            return m_hor_height;
        }
        set
        {
            m_hor_height = value;
        }
    }

    /// <summary>
    /// Gets or sets the 'PIE1_WIDTH' value.
    /// </summary>
    public int PIE1_WIDTH
    {
        get
        {
            return m_pie1_width;
        }
        set
        {
            m_pie1_width = value;
        }
    }

    /// <summary>
    /// Gets or sets the 'PIE1_HEIGHT' value.
    /// </summary>
    public int PIE1_HEIGHT
    {
        get
        {
            return m_pie1_height;
        }
        set
        {
            m_pie1_height = value;
        }
    }

    /// <summary>
    /// Gets or sets the 'PIE2_WIDTH' value.
    /// </summary>
    public int PIE2_WIDTH
    {
        get
        {
            return m_pie2_width;
        }
        set
        {
            m_pie2_width = value;
        }
    }

    /// <summary>
    /// Gets or sets the 'PIE2_HEIGHT' value.
    /// </summary>
    public int PIE2_HEIGHT
    {
        get
        {
            return m_pie2_height;
        }
        set
        {
            m_pie2_height = value;
        }
    }

    /// <summary>
    /// Gets or sets the 'PIE3_WIDTH' value.
    /// </summary>
    public int PIE3_WIDTH
    {
        get
        {
            return m_pie3_width;
        }
        set
        {
            m_pie3_width = value;
        }
    }

    /// <summary>
    /// Gets or sets the 'PIE3_HEIGHT' value.
    /// </summary>
    public int PIE3_HEIGHT
    {
        get
        {
            return m_pie3_height;
        }
        set
        {
            m_pie3_height = value;
        }
    }

    /// <summary>
    /// Gets or sets the 'PIE4_WIDTH' value.
    /// </summary>
    public int PIE4_WIDTH
    {
        get
        {
            return m_pie4_width;
        }
        set
        {
            m_pie4_width = value;
        }
    }

    /// <summary>
    /// Gets or sets the 'PIE4_HEIGHT' value.
    /// </summary>
    public int PIE4_HEIGHT
    {
        get
        {
            return m_pie4_height;
        }
        set
        {
            m_pie4_height = value;
        }
    }

    /// <summary>
    /// Gets or sets the 'LAYOUT_WIDTH' value.
    /// </summary>
    public int LAYOUT_WIDTH
    {
        get
        {
            return m_layout_width;
        }
        set
        {
            m_layout_width = value;
        }
    }

    /// <summary>
    /// Gets or sets the 'LAYOUT_HEIGHT' value.
    /// </summary>
    public int LAYOUT_HEIGHT
    {
        get
        {
            return m_layout_height;
        }
        set
        {
            m_layout_height = value;
        }
    }

    /// <summary>
    /// Gets or sets the 'UDR_WIDTH' value.
    /// </summary>
    public int UDR_WIDTH
    {
        get
        {
            return m_udr_width;
        }
        set
        {
            m_udr_width = value;
        }
    }

    /// <summary>
    /// Gets or sets the 'UDR_HEIGHT' value.
    /// </summary>
    public int UDR_HEIGHT
    {
        get
        {
            return m_udr_height;
        }
        set
        {
            m_udr_height = value;
        }
    }

    /// <summary>
    /// Gets or sets the 'EVENT_COLOR_FLAG' value.
    /// </summary>
    public string EVENT_COLOR_FLAG
    {
        get
        {
            if (m_event_color_flag == null )
            {
                m_event_color_flag = " ";
            }
            return m_event_color_flag;
        }
        set
        {
            if (value == null || value == "" )
            {
                m_event_color_flag = " ";
            }
            m_event_color_flag = value;
        }
    }

    /// <summary>
    /// Gets or sets the 'SIGNAL_FLAG' value.
    /// </summary>
    public string SIGNAL_FLAG
    {
        get
        {
            if (m_signal_flag == null )
            {
                m_signal_flag = " ";
            }
            return m_signal_flag;
        }
        set
        {
            if (value == null || value == "" )
            {
                m_signal_flag = " ";
            }
            m_signal_flag = value;
        }
    }

    /// <summary>
    /// Gets or sets the 'TEXT_SIZE' value.
    /// </summary>
    public int TEXT_SIZE
    {
        get
        {
            return m_text_size;
        }
        set
        {
            m_text_size = value;
        }
    }

    /// <summary>
    /// Gets or sets the 'TEXT_COLOR' value.
    /// </summary>
    public int TEXT_COLOR
    {
        get
        {
            return m_text_color;
        }
        set
        {
            m_text_color = value;
        }
    }

    /// <summary>
    /// Gets or sets the 'BACK_COLOR' value.
    /// </summary>
    public int BACK_COLOR
    {
        get
        {
            return m_back_color;
        }
        set
        {
            m_back_color = value;
        }
    }

#endregion

#region " Function Definition "

    /// <summary>
    /// Creator for Object
    /// <summary>
    public DBC_MFMBENVDEF(ref DB_Common dbc)
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
        CREATE_USER_ID = " ";
        CREATE_TIME = " ";
        UPDATE_USER_ID = " ";
        UPDATE_TIME = " ";
        FONT_FAMILY = " ";
        RES_WIDTH = 0;
        RES_HEIGHT = 0;
        RTG_WIDTH = 0;
        RTG_HEIGHT = 0;
        ELP_WIDTH = 0;
        ELP_HEIGHT = 0;
        TRI_WIDTH = 0;
        TRI_HEIGHT = 0;
        VER_WIDTH = 0;
        VER_HEIGHT = 0;
        HOR_WIDTH = 0;
        HOR_HEIGHT = 0;
        PIE1_WIDTH = 0;
        PIE1_HEIGHT = 0;
        PIE2_WIDTH = 0;
        PIE2_HEIGHT = 0;
        PIE3_WIDTH = 0;
        PIE3_HEIGHT = 0;
        PIE4_WIDTH = 0;
        PIE4_HEIGHT = 0;
        LAYOUT_WIDTH = 0;
        LAYOUT_HEIGHT = 0;
        UDR_WIDTH = 0;
        UDR_HEIGHT = 0;
        EVENT_COLOR_FLAG = " ";
        SIGNAL_FLAG = " ";
        TEXT_SIZE = 0;
        TEXT_COLOR = 0;
        BACK_COLOR = 0;

        return true;
    }

    /// <summary>
    /// Clone object
    /// </summary>
    /// <returns>object</returns>
    public object Clone()
    {
        DBC_MFMBENVDEF MFMBENVDEF = null;
        try
        {
            MFMBENVDEF = new DBC_MFMBENVDEF(ref _dbc);

            MFMBENVDEF.FACTORY = this.FACTORY;
            MFMBENVDEF.CREATE_USER_ID = this.CREATE_USER_ID;
            MFMBENVDEF.CREATE_TIME = this.CREATE_TIME;
            MFMBENVDEF.UPDATE_USER_ID = this.UPDATE_USER_ID;
            MFMBENVDEF.UPDATE_TIME = this.UPDATE_TIME;
            MFMBENVDEF.FONT_FAMILY = this.FONT_FAMILY;
            MFMBENVDEF.RES_WIDTH = this.RES_WIDTH;
            MFMBENVDEF.RES_HEIGHT = this.RES_HEIGHT;
            MFMBENVDEF.RTG_WIDTH = this.RTG_WIDTH;
            MFMBENVDEF.RTG_HEIGHT = this.RTG_HEIGHT;
            MFMBENVDEF.ELP_WIDTH = this.ELP_WIDTH;
            MFMBENVDEF.ELP_HEIGHT = this.ELP_HEIGHT;
            MFMBENVDEF.TRI_WIDTH = this.TRI_WIDTH;
            MFMBENVDEF.TRI_HEIGHT = this.TRI_HEIGHT;
            MFMBENVDEF.VER_WIDTH = this.VER_WIDTH;
            MFMBENVDEF.VER_HEIGHT = this.VER_HEIGHT;
            MFMBENVDEF.HOR_WIDTH = this.HOR_WIDTH;
            MFMBENVDEF.HOR_HEIGHT = this.HOR_HEIGHT;
            MFMBENVDEF.PIE1_WIDTH = this.PIE1_WIDTH;
            MFMBENVDEF.PIE1_HEIGHT = this.PIE1_HEIGHT;
            MFMBENVDEF.PIE2_WIDTH = this.PIE2_WIDTH;
            MFMBENVDEF.PIE2_HEIGHT = this.PIE2_HEIGHT;
            MFMBENVDEF.PIE3_WIDTH = this.PIE3_WIDTH;
            MFMBENVDEF.PIE3_HEIGHT = this.PIE3_HEIGHT;
            MFMBENVDEF.PIE4_WIDTH = this.PIE4_WIDTH;
            MFMBENVDEF.PIE4_HEIGHT = this.PIE4_HEIGHT;
            MFMBENVDEF.LAYOUT_WIDTH = this.LAYOUT_WIDTH;
            MFMBENVDEF.LAYOUT_HEIGHT = this.LAYOUT_HEIGHT;
            MFMBENVDEF.UDR_WIDTH = this.UDR_WIDTH;
            MFMBENVDEF.UDR_HEIGHT = this.UDR_HEIGHT;
            MFMBENVDEF.EVENT_COLOR_FLAG = this.EVENT_COLOR_FLAG;
            MFMBENVDEF.SIGNAL_FLAG = this.SIGNAL_FLAG;
            MFMBENVDEF.TEXT_SIZE = this.TEXT_SIZE;
            MFMBENVDEF.TEXT_COLOR = this.TEXT_COLOR;
            MFMBENVDEF.BACK_COLOR = this.BACK_COLOR;
        }
        catch(Exception ex)
        {
            if (ex.GetType().ToString() == "System.Data.OleDb.OleDbException")
                _dbc.gErrors.SetErrors(((OleDbException)ex).Errors);
            else
                _dbc.gErrors.SetErrors(ex);

            return null;
        }

        return MFMBENVDEF;
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
            CREATE_USER_ID = (string)(Row["CREATE_USER_ID"]);
            CREATE_TIME = (string)(Row["CREATE_TIME"]);
            UPDATE_USER_ID = (string)(Row["UPDATE_USER_ID"]);
            UPDATE_TIME = (string)(Row["UPDATE_TIME"]);
            FONT_FAMILY = (string)(Row["FONT_FAMILY"]);
            RES_WIDTH = Convert.ToInt32(Row["RES_WIDTH"]);
            RES_HEIGHT = Convert.ToInt32(Row["RES_HEIGHT"]);
            RTG_WIDTH = Convert.ToInt32(Row["RTG_WIDTH"]);
            RTG_HEIGHT = Convert.ToInt32(Row["RTG_HEIGHT"]);
            ELP_WIDTH = Convert.ToInt32(Row["ELP_WIDTH"]);
            ELP_HEIGHT = Convert.ToInt32(Row["ELP_HEIGHT"]);
            TRI_WIDTH = Convert.ToInt32(Row["TRI_WIDTH"]);
            TRI_HEIGHT = Convert.ToInt32(Row["TRI_HEIGHT"]);
            VER_WIDTH = Convert.ToInt32(Row["VER_WIDTH"]);
            VER_HEIGHT = Convert.ToInt32(Row["VER_HEIGHT"]);
            HOR_WIDTH = Convert.ToInt32(Row["HOR_WIDTH"]);
            HOR_HEIGHT = Convert.ToInt32(Row["HOR_HEIGHT"]);
            PIE1_WIDTH = Convert.ToInt32(Row["PIE1_WIDTH"]);
            PIE1_HEIGHT = Convert.ToInt32(Row["PIE1_HEIGHT"]);
            PIE2_WIDTH = Convert.ToInt32(Row["PIE2_WIDTH"]);
            PIE2_HEIGHT = Convert.ToInt32(Row["PIE2_HEIGHT"]);
            PIE3_WIDTH = Convert.ToInt32(Row["PIE3_WIDTH"]);
            PIE3_HEIGHT = Convert.ToInt32(Row["PIE3_HEIGHT"]);
            PIE4_WIDTH = Convert.ToInt32(Row["PIE4_WIDTH"]);
            PIE4_HEIGHT = Convert.ToInt32(Row["PIE4_HEIGHT"]);
            LAYOUT_WIDTH = Convert.ToInt32(Row["LAYOUT_WIDTH"]);
            LAYOUT_HEIGHT = Convert.ToInt32(Row["LAYOUT_HEIGHT"]);
            UDR_WIDTH = Convert.ToInt32(Row["UDR_WIDTH"]);
            UDR_HEIGHT = Convert.ToInt32(Row["UDR_HEIGHT"]);
            EVENT_COLOR_FLAG = (string)(Row["EVENT_COLOR_FLAG"]);
            SIGNAL_FLAG = (string)(Row["SIGNAL_FLAG"]);
            TEXT_SIZE = Convert.ToInt32(Row["TEXT_SIZE"]);
            TEXT_COLOR = Convert.ToInt32(Row["TEXT_COLOR"]);
            BACK_COLOR = Convert.ToInt32(Row["BACK_COLOR"]);
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
                    strQuery = "SELECT * FROM MFMBENVDEF"
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
                    case "FONT_FAMILY":
                        FONT_FAMILY = (string)(adoDataTable.Rows[0]["FONT_FAMILY"]);
                        break;
                    case "RES_WIDTH":
                        RES_WIDTH = Convert.ToInt32(adoDataTable.Rows[0]["RES_WIDTH"]);
                        break;
                    case "RES_HEIGHT":
                        RES_HEIGHT = Convert.ToInt32(adoDataTable.Rows[0]["RES_HEIGHT"]);
                        break;
                    case "RTG_WIDTH":
                        RTG_WIDTH = Convert.ToInt32(adoDataTable.Rows[0]["RTG_WIDTH"]);
                        break;
                    case "RTG_HEIGHT":
                        RTG_HEIGHT = Convert.ToInt32(adoDataTable.Rows[0]["RTG_HEIGHT"]);
                        break;
                    case "ELP_WIDTH":
                        ELP_WIDTH = Convert.ToInt32(adoDataTable.Rows[0]["ELP_WIDTH"]);
                        break;
                    case "ELP_HEIGHT":
                        ELP_HEIGHT = Convert.ToInt32(adoDataTable.Rows[0]["ELP_HEIGHT"]);
                        break;
                    case "TRI_WIDTH":
                        TRI_WIDTH = Convert.ToInt32(adoDataTable.Rows[0]["TRI_WIDTH"]);
                        break;
                    case "TRI_HEIGHT":
                        TRI_HEIGHT = Convert.ToInt32(adoDataTable.Rows[0]["TRI_HEIGHT"]);
                        break;
                    case "VER_WIDTH":
                        VER_WIDTH = Convert.ToInt32(adoDataTable.Rows[0]["VER_WIDTH"]);
                        break;
                    case "VER_HEIGHT":
                        VER_HEIGHT = Convert.ToInt32(adoDataTable.Rows[0]["VER_HEIGHT"]);
                        break;
                    case "HOR_WIDTH":
                        HOR_WIDTH = Convert.ToInt32(adoDataTable.Rows[0]["HOR_WIDTH"]);
                        break;
                    case "HOR_HEIGHT":
                        HOR_HEIGHT = Convert.ToInt32(adoDataTable.Rows[0]["HOR_HEIGHT"]);
                        break;
                    case "PIE1_WIDTH":
                        PIE1_WIDTH = Convert.ToInt32(adoDataTable.Rows[0]["PIE1_WIDTH"]);
                        break;
                    case "PIE1_HEIGHT":
                        PIE1_HEIGHT = Convert.ToInt32(adoDataTable.Rows[0]["PIE1_HEIGHT"]);
                        break;
                    case "PIE2_WIDTH":
                        PIE2_WIDTH = Convert.ToInt32(adoDataTable.Rows[0]["PIE2_WIDTH"]);
                        break;
                    case "PIE2_HEIGHT":
                        PIE2_HEIGHT = Convert.ToInt32(adoDataTable.Rows[0]["PIE2_HEIGHT"]);
                        break;
                    case "PIE3_WIDTH":
                        PIE3_WIDTH = Convert.ToInt32(adoDataTable.Rows[0]["PIE3_WIDTH"]);
                        break;
                    case "PIE3_HEIGHT":
                        PIE3_HEIGHT = Convert.ToInt32(adoDataTable.Rows[0]["PIE3_HEIGHT"]);
                        break;
                    case "PIE4_WIDTH":
                        PIE4_WIDTH = Convert.ToInt32(adoDataTable.Rows[0]["PIE4_WIDTH"]);
                        break;
                    case "PIE4_HEIGHT":
                        PIE4_HEIGHT = Convert.ToInt32(adoDataTable.Rows[0]["PIE4_HEIGHT"]);
                        break;
                    case "LAYOUT_WIDTH":
                        LAYOUT_WIDTH = Convert.ToInt32(adoDataTable.Rows[0]["LAYOUT_WIDTH"]);
                        break;
                    case "LAYOUT_HEIGHT":
                        LAYOUT_HEIGHT = Convert.ToInt32(adoDataTable.Rows[0]["LAYOUT_HEIGHT"]);
                        break;
                    case "UDR_WIDTH":
                        UDR_WIDTH = Convert.ToInt32(adoDataTable.Rows[0]["UDR_WIDTH"]);
                        break;
                    case "UDR_HEIGHT":
                        UDR_HEIGHT = Convert.ToInt32(adoDataTable.Rows[0]["UDR_HEIGHT"]);
                        break;
                    case "EVENT_COLOR_FLAG":
                        EVENT_COLOR_FLAG = (string)(adoDataTable.Rows[0]["EVENT_COLOR_FLAG"]);
                        break;
                    case "SIGNAL_FLAG":
                        SIGNAL_FLAG = (string)(adoDataTable.Rows[0]["SIGNAL_FLAG"]);
                        break;
                    case "TEXT_SIZE":
                        TEXT_SIZE = Convert.ToInt32(adoDataTable.Rows[0]["TEXT_SIZE"]);
                        break;
                    case "TEXT_COLOR":
                        TEXT_COLOR = Convert.ToInt32(adoDataTable.Rows[0]["TEXT_COLOR"]);
                        break;
                    case "BACK_COLOR":
                        BACK_COLOR = Convert.ToInt32(adoDataTable.Rows[0]["BACK_COLOR"]);
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
                        strQuery = "SELECT * FROM MFMBENVDEF"
                            + " WITH (UPDLOCK) WHERE FACTORY=?";
                    }
                    else if (_dbc.gDbType == (int)DB_TYPE.ORACLE)
                    {
                        strQuery = "SELECT * FROM MFMBENVDEF"
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
                    case "FONT_FAMILY":
                        FONT_FAMILY = (string)(adoDataTable.Rows[0]["FONT_FAMILY"]);
                        break;
                    case "RES_WIDTH":
                        RES_WIDTH = Convert.ToInt32(adoDataTable.Rows[0]["RES_WIDTH"]);
                        break;
                    case "RES_HEIGHT":
                        RES_HEIGHT = Convert.ToInt32(adoDataTable.Rows[0]["RES_HEIGHT"]);
                        break;
                    case "RTG_WIDTH":
                        RTG_WIDTH = Convert.ToInt32(adoDataTable.Rows[0]["RTG_WIDTH"]);
                        break;
                    case "RTG_HEIGHT":
                        RTG_HEIGHT = Convert.ToInt32(adoDataTable.Rows[0]["RTG_HEIGHT"]);
                        break;
                    case "ELP_WIDTH":
                        ELP_WIDTH = Convert.ToInt32(adoDataTable.Rows[0]["ELP_WIDTH"]);
                        break;
                    case "ELP_HEIGHT":
                        ELP_HEIGHT = Convert.ToInt32(adoDataTable.Rows[0]["ELP_HEIGHT"]);
                        break;
                    case "TRI_WIDTH":
                        TRI_WIDTH = Convert.ToInt32(adoDataTable.Rows[0]["TRI_WIDTH"]);
                        break;
                    case "TRI_HEIGHT":
                        TRI_HEIGHT = Convert.ToInt32(adoDataTable.Rows[0]["TRI_HEIGHT"]);
                        break;
                    case "VER_WIDTH":
                        VER_WIDTH = Convert.ToInt32(adoDataTable.Rows[0]["VER_WIDTH"]);
                        break;
                    case "VER_HEIGHT":
                        VER_HEIGHT = Convert.ToInt32(adoDataTable.Rows[0]["VER_HEIGHT"]);
                        break;
                    case "HOR_WIDTH":
                        HOR_WIDTH = Convert.ToInt32(adoDataTable.Rows[0]["HOR_WIDTH"]);
                        break;
                    case "HOR_HEIGHT":
                        HOR_HEIGHT = Convert.ToInt32(adoDataTable.Rows[0]["HOR_HEIGHT"]);
                        break;
                    case "PIE1_WIDTH":
                        PIE1_WIDTH = Convert.ToInt32(adoDataTable.Rows[0]["PIE1_WIDTH"]);
                        break;
                    case "PIE1_HEIGHT":
                        PIE1_HEIGHT = Convert.ToInt32(adoDataTable.Rows[0]["PIE1_HEIGHT"]);
                        break;
                    case "PIE2_WIDTH":
                        PIE2_WIDTH = Convert.ToInt32(adoDataTable.Rows[0]["PIE2_WIDTH"]);
                        break;
                    case "PIE2_HEIGHT":
                        PIE2_HEIGHT = Convert.ToInt32(adoDataTable.Rows[0]["PIE2_HEIGHT"]);
                        break;
                    case "PIE3_WIDTH":
                        PIE3_WIDTH = Convert.ToInt32(adoDataTable.Rows[0]["PIE3_WIDTH"]);
                        break;
                    case "PIE3_HEIGHT":
                        PIE3_HEIGHT = Convert.ToInt32(adoDataTable.Rows[0]["PIE3_HEIGHT"]);
                        break;
                    case "PIE4_WIDTH":
                        PIE4_WIDTH = Convert.ToInt32(adoDataTable.Rows[0]["PIE4_WIDTH"]);
                        break;
                    case "PIE4_HEIGHT":
                        PIE4_HEIGHT = Convert.ToInt32(adoDataTable.Rows[0]["PIE4_HEIGHT"]);
                        break;
                    case "LAYOUT_WIDTH":
                        LAYOUT_WIDTH = Convert.ToInt32(adoDataTable.Rows[0]["LAYOUT_WIDTH"]);
                        break;
                    case "LAYOUT_HEIGHT":
                        LAYOUT_HEIGHT = Convert.ToInt32(adoDataTable.Rows[0]["LAYOUT_HEIGHT"]);
                        break;
                    case "UDR_WIDTH":
                        UDR_WIDTH = Convert.ToInt32(adoDataTable.Rows[0]["UDR_WIDTH"]);
                        break;
                    case "UDR_HEIGHT":
                        UDR_HEIGHT = Convert.ToInt32(adoDataTable.Rows[0]["UDR_HEIGHT"]);
                        break;
                    case "EVENT_COLOR_FLAG":
                        EVENT_COLOR_FLAG = (string)(adoDataTable.Rows[0]["EVENT_COLOR_FLAG"]);
                        break;
                    case "SIGNAL_FLAG":
                        SIGNAL_FLAG = (string)(adoDataTable.Rows[0]["SIGNAL_FLAG"]);
                        break;
                    case "TEXT_SIZE":
                        TEXT_SIZE = Convert.ToInt32(adoDataTable.Rows[0]["TEXT_SIZE"]);
                        break;
                    case "TEXT_COLOR":
                        TEXT_COLOR = Convert.ToInt32(adoDataTable.Rows[0]["TEXT_COLOR"]);
                        break;
                    case "BACK_COLOR":
                        BACK_COLOR = Convert.ToInt32(adoDataTable.Rows[0]["BACK_COLOR"]);
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
                    strQuery = "SELECT COUNT(*) FROM MFMBENVDEF"
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
                    strQuery = "DELETE FROM MFMBENVDEF"
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

            strQuery = "INSERT INTO MFMBENVDEF ("
                + " FACTORY, CREATE_USER_ID, CREATE_TIME, UPDATE_USER_ID, UPDATE_TIME,"
                + " FONT_FAMILY, RES_WIDTH, RES_HEIGHT, RTG_WIDTH, RTG_HEIGHT,"
                + " ELP_WIDTH, ELP_HEIGHT, TRI_WIDTH, TRI_HEIGHT, VER_WIDTH,"
                + " VER_HEIGHT, HOR_WIDTH, HOR_HEIGHT, PIE1_WIDTH, PIE1_HEIGHT,"
                + " PIE2_WIDTH, PIE2_HEIGHT, PIE3_WIDTH, PIE3_HEIGHT, PIE4_WIDTH,"
                + " PIE4_HEIGHT, LAYOUT_WIDTH, LAYOUT_HEIGHT, UDR_WIDTH, UDR_HEIGHT,"
                + " EVENT_COLOR_FLAG, SIGNAL_FLAG, TEXT_SIZE, TEXT_COLOR, BACK_COLOR)"
                + " VALUES ("
                + " ?, ?, ?, ?, ?, ?, ?, ?, ?, ?,"
                + " ?, ?, ?, ?, ?, ?, ?, ?, ?, ?,"
                + " ?, ?, ?, ?, ?, ?, ?, ?, ?, ?,"
                + " ?, ?, ?, ?, ?)";

            // Add Parameters
            adoCommand.Parameters.Add("@FACTORY", OleDbType.VarChar).Value = FACTORY;
            adoCommand.Parameters.Add("@CREATE_USER_ID", OleDbType.VarChar).Value = CREATE_USER_ID;
            adoCommand.Parameters.Add("@CREATE_TIME", OleDbType.VarChar).Value = CREATE_TIME;
            adoCommand.Parameters.Add("@UPDATE_USER_ID", OleDbType.VarChar).Value = UPDATE_USER_ID;
            adoCommand.Parameters.Add("@UPDATE_TIME", OleDbType.VarChar).Value = UPDATE_TIME;
            adoCommand.Parameters.Add("@FONT_FAMILY", OleDbType.VarChar).Value = FONT_FAMILY;
            adoCommand.Parameters.Add("@RES_WIDTH", OleDbType.Numeric).Value = RES_WIDTH;
            adoCommand.Parameters.Add("@RES_HEIGHT", OleDbType.Numeric).Value = RES_HEIGHT;
            adoCommand.Parameters.Add("@RTG_WIDTH", OleDbType.Numeric).Value = RTG_WIDTH;
            adoCommand.Parameters.Add("@RTG_HEIGHT", OleDbType.Numeric).Value = RTG_HEIGHT;
            adoCommand.Parameters.Add("@ELP_WIDTH", OleDbType.Numeric).Value = ELP_WIDTH;
            adoCommand.Parameters.Add("@ELP_HEIGHT", OleDbType.Numeric).Value = ELP_HEIGHT;
            adoCommand.Parameters.Add("@TRI_WIDTH", OleDbType.Numeric).Value = TRI_WIDTH;
            adoCommand.Parameters.Add("@TRI_HEIGHT", OleDbType.Numeric).Value = TRI_HEIGHT;
            adoCommand.Parameters.Add("@VER_WIDTH", OleDbType.Numeric).Value = VER_WIDTH;
            adoCommand.Parameters.Add("@VER_HEIGHT", OleDbType.Numeric).Value = VER_HEIGHT;
            adoCommand.Parameters.Add("@HOR_WIDTH", OleDbType.Numeric).Value = HOR_WIDTH;
            adoCommand.Parameters.Add("@HOR_HEIGHT", OleDbType.Numeric).Value = HOR_HEIGHT;
            adoCommand.Parameters.Add("@PIE1_WIDTH", OleDbType.Numeric).Value = PIE1_WIDTH;
            adoCommand.Parameters.Add("@PIE1_HEIGHT", OleDbType.Numeric).Value = PIE1_HEIGHT;
            adoCommand.Parameters.Add("@PIE2_WIDTH", OleDbType.Numeric).Value = PIE2_WIDTH;
            adoCommand.Parameters.Add("@PIE2_HEIGHT", OleDbType.Numeric).Value = PIE2_HEIGHT;
            adoCommand.Parameters.Add("@PIE3_WIDTH", OleDbType.Numeric).Value = PIE3_WIDTH;
            adoCommand.Parameters.Add("@PIE3_HEIGHT", OleDbType.Numeric).Value = PIE3_HEIGHT;
            adoCommand.Parameters.Add("@PIE4_WIDTH", OleDbType.Numeric).Value = PIE4_WIDTH;
            adoCommand.Parameters.Add("@PIE4_HEIGHT", OleDbType.Numeric).Value = PIE4_HEIGHT;
            adoCommand.Parameters.Add("@LAYOUT_WIDTH", OleDbType.Numeric).Value = LAYOUT_WIDTH;
            adoCommand.Parameters.Add("@LAYOUT_HEIGHT", OleDbType.Numeric).Value = LAYOUT_HEIGHT;
            adoCommand.Parameters.Add("@UDR_WIDTH", OleDbType.Numeric).Value = UDR_WIDTH;
            adoCommand.Parameters.Add("@UDR_HEIGHT", OleDbType.Numeric).Value = UDR_HEIGHT;
            adoCommand.Parameters.Add("@EVENT_COLOR_FLAG", OleDbType.VarChar).Value = EVENT_COLOR_FLAG;
            adoCommand.Parameters.Add("@SIGNAL_FLAG", OleDbType.VarChar).Value = SIGNAL_FLAG;
            adoCommand.Parameters.Add("@TEXT_SIZE", OleDbType.Numeric).Value = TEXT_SIZE;
            adoCommand.Parameters.Add("@TEXT_COLOR", OleDbType.Numeric).Value = TEXT_COLOR;
            adoCommand.Parameters.Add("@BACK_COLOR", OleDbType.Numeric).Value = BACK_COLOR;

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
                    strQuery = "UPDATE MFMBENVDEF SET"
                        + " CREATE_USER_ID=?, CREATE_TIME=?, UPDATE_USER_ID=?, UPDATE_TIME=?, FONT_FAMILY=?,"
                        + " RES_WIDTH=?, RES_HEIGHT=?, RTG_WIDTH=?, RTG_HEIGHT=?, ELP_WIDTH=?,"
                        + " ELP_HEIGHT=?, TRI_WIDTH=?, TRI_HEIGHT=?, VER_WIDTH=?, VER_HEIGHT=?,"
                        + " HOR_WIDTH=?, HOR_HEIGHT=?, PIE1_WIDTH=?, PIE1_HEIGHT=?, PIE2_WIDTH=?,"
                        + " PIE2_HEIGHT=?, PIE3_WIDTH=?, PIE3_HEIGHT=?, PIE4_WIDTH=?, PIE4_HEIGHT=?,"
                        + " LAYOUT_WIDTH=?, LAYOUT_HEIGHT=?, UDR_WIDTH=?, UDR_HEIGHT=?, EVENT_COLOR_FLAG=?,"
                        + " SIGNAL_FLAG=?, TEXT_SIZE=?, TEXT_COLOR=?, BACK_COLOR=?"
                        + " WHERE FACTORY=?";

                    // Add Parameters
                    adoCommand.Parameters.Add("@CREATE_USER_ID", OleDbType.VarChar).Value = CREATE_USER_ID;
                    adoCommand.Parameters.Add("@CREATE_TIME", OleDbType.VarChar).Value = CREATE_TIME;
                    adoCommand.Parameters.Add("@UPDATE_USER_ID", OleDbType.VarChar).Value = UPDATE_USER_ID;
                    adoCommand.Parameters.Add("@UPDATE_TIME", OleDbType.VarChar).Value = UPDATE_TIME;
                    adoCommand.Parameters.Add("@FONT_FAMILY", OleDbType.VarChar).Value = FONT_FAMILY;
                    adoCommand.Parameters.Add("@RES_WIDTH", OleDbType.Numeric).Value = RES_WIDTH;
                    adoCommand.Parameters.Add("@RES_HEIGHT", OleDbType.Numeric).Value = RES_HEIGHT;
                    adoCommand.Parameters.Add("@RTG_WIDTH", OleDbType.Numeric).Value = RTG_WIDTH;
                    adoCommand.Parameters.Add("@RTG_HEIGHT", OleDbType.Numeric).Value = RTG_HEIGHT;
                    adoCommand.Parameters.Add("@ELP_WIDTH", OleDbType.Numeric).Value = ELP_WIDTH;
                    adoCommand.Parameters.Add("@ELP_HEIGHT", OleDbType.Numeric).Value = ELP_HEIGHT;
                    adoCommand.Parameters.Add("@TRI_WIDTH", OleDbType.Numeric).Value = TRI_WIDTH;
                    adoCommand.Parameters.Add("@TRI_HEIGHT", OleDbType.Numeric).Value = TRI_HEIGHT;
                    adoCommand.Parameters.Add("@VER_WIDTH", OleDbType.Numeric).Value = VER_WIDTH;
                    adoCommand.Parameters.Add("@VER_HEIGHT", OleDbType.Numeric).Value = VER_HEIGHT;
                    adoCommand.Parameters.Add("@HOR_WIDTH", OleDbType.Numeric).Value = HOR_WIDTH;
                    adoCommand.Parameters.Add("@HOR_HEIGHT", OleDbType.Numeric).Value = HOR_HEIGHT;
                    adoCommand.Parameters.Add("@PIE1_WIDTH", OleDbType.Numeric).Value = PIE1_WIDTH;
                    adoCommand.Parameters.Add("@PIE1_HEIGHT", OleDbType.Numeric).Value = PIE1_HEIGHT;
                    adoCommand.Parameters.Add("@PIE2_WIDTH", OleDbType.Numeric).Value = PIE2_WIDTH;
                    adoCommand.Parameters.Add("@PIE2_HEIGHT", OleDbType.Numeric).Value = PIE2_HEIGHT;
                    adoCommand.Parameters.Add("@PIE3_WIDTH", OleDbType.Numeric).Value = PIE3_WIDTH;
                    adoCommand.Parameters.Add("@PIE3_HEIGHT", OleDbType.Numeric).Value = PIE3_HEIGHT;
                    adoCommand.Parameters.Add("@PIE4_WIDTH", OleDbType.Numeric).Value = PIE4_WIDTH;
                    adoCommand.Parameters.Add("@PIE4_HEIGHT", OleDbType.Numeric).Value = PIE4_HEIGHT;
                    adoCommand.Parameters.Add("@LAYOUT_WIDTH", OleDbType.Numeric).Value = LAYOUT_WIDTH;
                    adoCommand.Parameters.Add("@LAYOUT_HEIGHT", OleDbType.Numeric).Value = LAYOUT_HEIGHT;
                    adoCommand.Parameters.Add("@UDR_WIDTH", OleDbType.Numeric).Value = UDR_WIDTH;
                    adoCommand.Parameters.Add("@UDR_HEIGHT", OleDbType.Numeric).Value = UDR_HEIGHT;
                    adoCommand.Parameters.Add("@EVENT_COLOR_FLAG", OleDbType.VarChar).Value = EVENT_COLOR_FLAG;
                    adoCommand.Parameters.Add("@SIGNAL_FLAG", OleDbType.VarChar).Value = SIGNAL_FLAG;
                    adoCommand.Parameters.Add("@TEXT_SIZE", OleDbType.Numeric).Value = TEXT_SIZE;
                    adoCommand.Parameters.Add("@TEXT_COLOR", OleDbType.Numeric).Value = TEXT_COLOR;
                    adoCommand.Parameters.Add("@BACK_COLOR", OleDbType.Numeric).Value = BACK_COLOR;

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
                    strQuery = "SELECT * FROM MFMBENVDEF"
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

