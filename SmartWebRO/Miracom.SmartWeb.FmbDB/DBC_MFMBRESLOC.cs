//-----------------------------------------------------------------------------
//
//   System      : MES
//   File Name   : DBC_MFMBRESLOC.cs
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
public class DBC_MFMBRESLOC
    : ICloneable
{


#region " Fields Definition "

    // Fields
    private string m_factory;                                // VARCHAR(10)
    private string m_res_id;                                 // VARCHAR(20)
    private string m_res_type;                               // VARCHAR(1)
    private string m_layout_id;                              // VARCHAR(20)
    private int m_seq;                                       // INTEGER(6)
    private int m_loc_x;                                     // INTEGER(6)
    private int m_loc_y;                                     // INTEGER(6)
    private int m_loc_width;                                 // INTEGER(6)
    private int m_loc_height;                                // INTEGER(6)
    private string m_text;                                   // VARCHAR(40)
    private int m_text_size;                                 // INTEGER(2)
    private int m_text_color;                                // INTEGER(8)
    private string m_text_style;                             // VARCHAR(1)
    private int m_tag_type;                                  // INTEGER(2)
    private int m_back_color;                                // INTEGER(8)
    private string m_create_user_id;                         // VARCHAR(20)
    private string m_create_time;                            // VARCHAR(14)
    private string m_update_user_id;                         // VARCHAR(20)
    private string m_update_time;                            // VARCHAR(14)
    private string m_no_mouse_event;                         // VARCHAR(1)
    private string m_signal_flag;                            // VARCHAR(1)

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
    /// Gets or sets the 'LAYOUT_ID' value.
    /// </summary>
    public string LAYOUT_ID
    {
        get
        {
            if (m_layout_id == null )
            {
                m_layout_id = " ";
            }
            return m_layout_id;
        }
        set
        {
            if (value == null || value == "" )
            {
                m_layout_id = " ";
            }
            m_layout_id = value;
        }
    }

    /// <summary>
    /// Gets or sets the 'SEQ' value.
    /// </summary>
    public int SEQ
    {
        get
        {
            return m_seq;
        }
        set
        {
            m_seq = value;
        }
    }

    /// <summary>
    /// Gets or sets the 'LOC_X' value.
    /// </summary>
    public int LOC_X
    {
        get
        {
            return m_loc_x;
        }
        set
        {
            m_loc_x = value;
        }
    }

    /// <summary>
    /// Gets or sets the 'LOC_Y' value.
    /// </summary>
    public int LOC_Y
    {
        get
        {
            return m_loc_y;
        }
        set
        {
            m_loc_y = value;
        }
    }

    /// <summary>
    /// Gets or sets the 'LOC_WIDTH' value.
    /// </summary>
    public int LOC_WIDTH
    {
        get
        {
            return m_loc_width;
        }
        set
        {
            m_loc_width = value;
        }
    }

    /// <summary>
    /// Gets or sets the 'LOC_HEIGHT' value.
    /// </summary>
    public int LOC_HEIGHT
    {
        get
        {
            return m_loc_height;
        }
        set
        {
            m_loc_height = value;
        }
    }

    /// <summary>
    /// Gets or sets the 'TEXT' value.
    /// </summary>
    public string TEXT
    {
        get
        {
            if (m_text == null )
            {
                m_text = " ";
            }
            return m_text;
        }
        set
        {
            if (value == null || value == "" )
            {
                m_text = " ";
            }
            m_text = value;
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
    /// Gets or sets the 'TEXT_STYLE' value.
    /// </summary>
    public string TEXT_STYLE
    {
        get
        {
            if (m_text_style == null )
            {
                m_text_style = " ";
            }
            return m_text_style;
        }
        set
        {
            if (value == null || value == "" )
            {
                m_text_style = " ";
            }
            m_text_style = value;
        }
    }

    /// <summary>
    /// Gets or sets the 'TAG_TYPE' value.
    /// </summary>
    public int TAG_TYPE
    {
        get
        {
            return m_tag_type;
        }
        set
        {
            m_tag_type = value;
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
    /// Gets or sets the 'NO_MOUSE_EVENT' value.
    /// </summary>
    public string NO_MOUSE_EVENT
    {
        get
        {
            if (m_no_mouse_event == null )
            {
                m_no_mouse_event = " ";
            }
            return m_no_mouse_event;
        }
        set
        {
            if (value == null || value == "" )
            {
                m_no_mouse_event = " ";
            }
            m_no_mouse_event = value;
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

#endregion

#region " Function Definition "

    /// <summary>
    /// Creator for Object
    /// <summary>
    public DBC_MFMBRESLOC(ref DB_Common dbc)
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
        RES_TYPE = " ";
        LAYOUT_ID = " ";
        SEQ = 0;
        LOC_X = 0;
        LOC_Y = 0;
        LOC_WIDTH = 0;
        LOC_HEIGHT = 0;
        TEXT = " ";
        TEXT_SIZE = 0;
        TEXT_COLOR = 0;
        TEXT_STYLE = " ";
        TAG_TYPE = 0;
        BACK_COLOR = 0;
        CREATE_USER_ID = " ";
        CREATE_TIME = " ";
        UPDATE_USER_ID = " ";
        UPDATE_TIME = " ";
        NO_MOUSE_EVENT = " ";
        SIGNAL_FLAG = " ";

        return true;
    }

    /// <summary>
    /// Clone object
    /// </summary>
    /// <returns>object</returns>
    public object Clone()
    {
        DBC_MFMBRESLOC MFMBRESLOC = null;
        try
        {
            MFMBRESLOC = new DBC_MFMBRESLOC(ref _dbc);

            MFMBRESLOC.FACTORY = this.FACTORY;
            MFMBRESLOC.RES_ID = this.RES_ID;
            MFMBRESLOC.RES_TYPE = this.RES_TYPE;
            MFMBRESLOC.LAYOUT_ID = this.LAYOUT_ID;
            MFMBRESLOC.SEQ = this.SEQ;
            MFMBRESLOC.LOC_X = this.LOC_X;
            MFMBRESLOC.LOC_Y = this.LOC_Y;
            MFMBRESLOC.LOC_WIDTH = this.LOC_WIDTH;
            MFMBRESLOC.LOC_HEIGHT = this.LOC_HEIGHT;
            MFMBRESLOC.TEXT = this.TEXT;
            MFMBRESLOC.TEXT_SIZE = this.TEXT_SIZE;
            MFMBRESLOC.TEXT_COLOR = this.TEXT_COLOR;
            MFMBRESLOC.TEXT_STYLE = this.TEXT_STYLE;
            MFMBRESLOC.TAG_TYPE = this.TAG_TYPE;
            MFMBRESLOC.BACK_COLOR = this.BACK_COLOR;
            MFMBRESLOC.CREATE_USER_ID = this.CREATE_USER_ID;
            MFMBRESLOC.CREATE_TIME = this.CREATE_TIME;
            MFMBRESLOC.UPDATE_USER_ID = this.UPDATE_USER_ID;
            MFMBRESLOC.UPDATE_TIME = this.UPDATE_TIME;
            MFMBRESLOC.NO_MOUSE_EVENT = this.NO_MOUSE_EVENT;
            MFMBRESLOC.SIGNAL_FLAG = this.SIGNAL_FLAG;
        }
        catch(Exception ex)
        {
            if (ex.GetType().ToString() == "System.Data.OleDb.OleDbException")
                _dbc.gErrors.SetErrors(((OleDbException)ex).Errors);
            else
                _dbc.gErrors.SetErrors(ex);

            return null;
        }

        return MFMBRESLOC;
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
            RES_TYPE = (string)(Row["RES_TYPE"]);
            LAYOUT_ID = (string)(Row["LAYOUT_ID"]);
            SEQ = Convert.ToInt32(Row["SEQ"]);
            LOC_X = Convert.ToInt32(Row["LOC_X"]);
            LOC_Y = Convert.ToInt32(Row["LOC_Y"]);
            LOC_WIDTH = Convert.ToInt32(Row["LOC_WIDTH"]);
            LOC_HEIGHT = Convert.ToInt32(Row["LOC_HEIGHT"]);
            TEXT = (string)(Row["TEXT"]);
            TEXT_SIZE = Convert.ToInt32(Row["TEXT_SIZE"]);
            TEXT_COLOR = Convert.ToInt32(Row["TEXT_COLOR"]);
            TEXT_STYLE = (string)(Row["TEXT_STYLE"]);
            TAG_TYPE = Convert.ToInt32(Row["TAG_TYPE"]);
            BACK_COLOR = Convert.ToInt32(Row["BACK_COLOR"]);
            CREATE_USER_ID = (string)(Row["CREATE_USER_ID"]);
            CREATE_TIME = (string)(Row["CREATE_TIME"]);
            UPDATE_USER_ID = (string)(Row["UPDATE_USER_ID"]);
            UPDATE_TIME = (string)(Row["UPDATE_TIME"]);
            NO_MOUSE_EVENT = (string)(Row["NO_MOUSE_EVENT"]);
            SIGNAL_FLAG = (string)(Row["SIGNAL_FLAG"]);
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
                    strQuery = "SELECT * FROM MFMBRESLOC"
                        + " WHERE FACTORY=?"
                        + " AND RES_ID=?"
                        + " AND RES_TYPE=?"
                        + " AND LAYOUT_ID=?";

                    // Add Parameters
                    adoAdapter.SelectCommand.Parameters.Add("@FACTORY", OleDbType.VarChar).Value = FACTORY;
                    adoAdapter.SelectCommand.Parameters.Add("@RES_ID", OleDbType.VarChar).Value = RES_ID;
                    adoAdapter.SelectCommand.Parameters.Add("@RES_TYPE", OleDbType.VarChar).Value = RES_TYPE;
                    adoAdapter.SelectCommand.Parameters.Add("@LAYOUT_ID", OleDbType.VarChar).Value = LAYOUT_ID;
                    break;

                case 3:
                    strQuery = "SELECT NVL(MAX(SEQ), 0) AS SEQ"
                        + " FROM MFMBRESLOC"
                        + " WHERE FACTORY=?"
                        + " AND LAYOUT_ID=?";

                    // Add Parameters
                    adoAdapter.SelectCommand.Parameters.Add("@FACTORY", OleDbType.VarChar).Value = FACTORY;
                    adoAdapter.SelectCommand.Parameters.Add("@LAYOUT_ID", OleDbType.VarChar).Value = LAYOUT_ID;
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
                    case "RES_TYPE":
                        RES_TYPE = (string)(adoDataTable.Rows[0]["RES_TYPE"]);
                        break;
                    case "LAYOUT_ID":
                        LAYOUT_ID = (string)(adoDataTable.Rows[0]["LAYOUT_ID"]);
                        break;
                    case "SEQ":
                        SEQ = Convert.ToInt32(adoDataTable.Rows[0]["SEQ"]);
                        break;
                    case "LOC_X":
                        LOC_X = Convert.ToInt32(adoDataTable.Rows[0]["LOC_X"]);
                        break;
                    case "LOC_Y":
                        LOC_Y = Convert.ToInt32(adoDataTable.Rows[0]["LOC_Y"]);
                        break;
                    case "LOC_WIDTH":
                        LOC_WIDTH = Convert.ToInt32(adoDataTable.Rows[0]["LOC_WIDTH"]);
                        break;
                    case "LOC_HEIGHT":
                        LOC_HEIGHT = Convert.ToInt32(adoDataTable.Rows[0]["LOC_HEIGHT"]);
                        break;
                    case "TEXT":
                        TEXT = (string)(adoDataTable.Rows[0]["TEXT"]);
                        break;
                    case "TEXT_SIZE":
                        TEXT_SIZE = Convert.ToInt32(adoDataTable.Rows[0]["TEXT_SIZE"]);
                        break;
                    case "TEXT_COLOR":
                        TEXT_COLOR = Convert.ToInt32(adoDataTable.Rows[0]["TEXT_COLOR"]);
                        break;
                    case "TEXT_STYLE":
                        TEXT_STYLE = (string)(adoDataTable.Rows[0]["TEXT_STYLE"]);
                        break;
                    case "TAG_TYPE":
                        TAG_TYPE = Convert.ToInt32(adoDataTable.Rows[0]["TAG_TYPE"]);
                        break;
                    case "BACK_COLOR":
                        BACK_COLOR = Convert.ToInt32(adoDataTable.Rows[0]["BACK_COLOR"]);
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
                    case "NO_MOUSE_EVENT":
                        NO_MOUSE_EVENT = (string)(adoDataTable.Rows[0]["NO_MOUSE_EVENT"]);
                        break;
                    case "SIGNAL_FLAG":
                        SIGNAL_FLAG = (string)(adoDataTable.Rows[0]["SIGNAL_FLAG"]);
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
                        strQuery = "SELECT * FROM MFMBRESLOC"
                            + " WITH (UPDLOCK) WHERE FACTORY=?"
                            + " AND RES_ID=?"
                            + " AND RES_TYPE=?"
                            + " AND LAYOUT_ID=?";
                    }
                    else if (_dbc.gDbType == (int)DB_TYPE.ORACLE)
                    {
                        strQuery = "SELECT * FROM MFMBRESLOC"
                            + " WHERE FACTORY=?"
                            + " AND RES_ID=?"
                            + " AND RES_TYPE=?"
                            + " AND LAYOUT_ID=?"
                            + " FOR UPDATE";
                    }
                    else if (_dbc.gDbType == (int)DB_TYPE.DB2) 
                    {
                    }

                    // Add Parameters
                    adoAdapter.SelectCommand.Parameters.Add("@FACTORY", OleDbType.VarChar).Value = FACTORY;
                    adoAdapter.SelectCommand.Parameters.Add("@RES_ID", OleDbType.VarChar).Value = RES_ID;
                    adoAdapter.SelectCommand.Parameters.Add("@RES_TYPE", OleDbType.VarChar).Value = RES_TYPE;
                    adoAdapter.SelectCommand.Parameters.Add("@LAYOUT_ID", OleDbType.VarChar).Value = LAYOUT_ID;
                    break;

                case 2:
                    // Select for Update by Primary Key
                    if (_dbc.gDbType == (int)DB_TYPE.MSSQL)
                    {
                        strQuery = "SELECT * FROM MFMBRESLOC"
                            + " WITH (UPDLOCK) WHERE FACTORY=?"
                            + " AND RES_ID=?"
                            + " AND RES_TYPE=?";
                    }
                    else if (_dbc.gDbType == (int)DB_TYPE.ORACLE)
                    {
                        strQuery = "SELECT * FROM MFMBRESLOC"
                            + " WHERE FACTORY=?"
                            + " AND RES_ID=?"
                            + " AND RES_TYPE=?"
                            + " FOR UPDATE";
                    }
                    else if (_dbc.gDbType == (int)DB_TYPE.DB2)
                    {
                    }

                    // Add Parameters
                    adoAdapter.SelectCommand.Parameters.Add("@FACTORY", OleDbType.VarChar).Value = FACTORY;
                    adoAdapter.SelectCommand.Parameters.Add("@RES_ID", OleDbType.VarChar).Value = RES_ID;
                    adoAdapter.SelectCommand.Parameters.Add("@RES_TYPE", OleDbType.VarChar).Value = RES_TYPE;
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
                    case "RES_TYPE":
                        RES_TYPE = (string)(adoDataTable.Rows[0]["RES_TYPE"]);
                        break;
                    case "LAYOUT_ID":
                        LAYOUT_ID = (string)(adoDataTable.Rows[0]["LAYOUT_ID"]);
                        break;
                    case "SEQ":
                        SEQ = Convert.ToInt32(adoDataTable.Rows[0]["SEQ"]);
                        break;
                    case "LOC_X":
                        LOC_X = Convert.ToInt32(adoDataTable.Rows[0]["LOC_X"]);
                        break;
                    case "LOC_Y":
                        LOC_Y = Convert.ToInt32(adoDataTable.Rows[0]["LOC_Y"]);
                        break;
                    case "LOC_WIDTH":
                        LOC_WIDTH = Convert.ToInt32(adoDataTable.Rows[0]["LOC_WIDTH"]);
                        break;
                    case "LOC_HEIGHT":
                        LOC_HEIGHT = Convert.ToInt32(adoDataTable.Rows[0]["LOC_HEIGHT"]);
                        break;
                    case "TEXT":
                        TEXT = (string)(adoDataTable.Rows[0]["TEXT"]);
                        break;
                    case "TEXT_SIZE":
                        TEXT_SIZE = Convert.ToInt32(adoDataTable.Rows[0]["TEXT_SIZE"]);
                        break;
                    case "TEXT_COLOR":
                        TEXT_COLOR = Convert.ToInt32(adoDataTable.Rows[0]["TEXT_COLOR"]);
                        break;
                    case "TEXT_STYLE":
                        TEXT_STYLE = (string)(adoDataTable.Rows[0]["TEXT_STYLE"]);
                        break;
                    case "TAG_TYPE":
                        TAG_TYPE = Convert.ToInt32(adoDataTable.Rows[0]["TAG_TYPE"]);
                        break;
                    case "BACK_COLOR":
                        BACK_COLOR = Convert.ToInt32(adoDataTable.Rows[0]["BACK_COLOR"]);
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
                    case "NO_MOUSE_EVENT":
                        NO_MOUSE_EVENT = (string)(adoDataTable.Rows[0]["NO_MOUSE_EVENT"]);
                        break;
                    case "SIGNAL_FLAG":
                        SIGNAL_FLAG = (string)(adoDataTable.Rows[0]["SIGNAL_FLAG"]);
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
                    strQuery = "SELECT COUNT(*) FROM MFMBRESLOC"
                        + " WHERE FACTORY=?"
                        + " AND RES_ID=?"
                        + " AND RES_TYPE=?"
                        + " AND LAYOUT_ID=?";

                    // Add Parameters
                    adoCommand.Parameters.Add("@FACTORY", OleDbType.VarChar).Value = FACTORY;
                    adoCommand.Parameters.Add("@RES_ID", OleDbType.VarChar).Value = RES_ID;
                    adoCommand.Parameters.Add("@RES_TYPE", OleDbType.VarChar).Value = RES_TYPE;
                    adoCommand.Parameters.Add("@LAYOUT_ID", OleDbType.VarChar).Value = LAYOUT_ID;
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
                    strQuery = "DELETE FROM MFMBRESLOC"
                        + " WHERE FACTORY=?"
                        + " AND RES_ID=?"
                        + " AND RES_TYPE=?"
                        + " AND LAYOUT_ID=?";

                    // Add Parameters
                    adoCommand.Parameters.Add("@FACTORY", OleDbType.VarChar).Value = FACTORY;
                    adoCommand.Parameters.Add("@RES_ID", OleDbType.VarChar).Value = RES_ID;
                    adoCommand.Parameters.Add("@RES_TYPE", OleDbType.VarChar).Value = RES_TYPE;
                    adoCommand.Parameters.Add("@LAYOUT_ID", OleDbType.VarChar).Value = LAYOUT_ID;
                    break;

                case 3:
                    strQuery = "DELETE FROM MFMBRESLOC"
                        + " WHERE FACTORY=?"
                        + " AND RES_ID=?"
                        + " AND RES_TYPE=?";

                    // Add Parameters
                    adoCommand.Parameters.Add("@FACTORY", OleDbType.VarChar).Value = FACTORY;
                    adoCommand.Parameters.Add("@RES_ID", OleDbType.VarChar).Value = RES_ID;
                    adoCommand.Parameters.Add("@RES_TYPE", OleDbType.VarChar).Value = RES_TYPE;
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

            strQuery = "INSERT INTO MFMBRESLOC ("
                + " FACTORY, RES_ID, RES_TYPE, LAYOUT_ID, SEQ,"
                + " LOC_X, LOC_Y, LOC_WIDTH, LOC_HEIGHT, TEXT,"
                + " TEXT_SIZE, TEXT_COLOR, TEXT_STYLE, TAG_TYPE, BACK_COLOR,"
                + " CREATE_USER_ID, CREATE_TIME, UPDATE_USER_ID, UPDATE_TIME, NO_MOUSE_EVENT,"
                + " SIGNAL_FLAG)"
                + " VALUES ("
                + " ?, ?, ?, ?, ?, ?, ?, ?, ?, ?,"
                + " ?, ?, ?, ?, ?, ?, ?, ?, ?, ?,"
                + " ?)";

            // Add Parameters
            adoCommand.Parameters.Add("@FACTORY", OleDbType.VarChar).Value = FACTORY;
            adoCommand.Parameters.Add("@RES_ID", OleDbType.VarChar).Value = RES_ID;
            adoCommand.Parameters.Add("@RES_TYPE", OleDbType.VarChar).Value = RES_TYPE;
            adoCommand.Parameters.Add("@LAYOUT_ID", OleDbType.VarChar).Value = LAYOUT_ID;
            adoCommand.Parameters.Add("@SEQ", OleDbType.Numeric).Value = SEQ;
            adoCommand.Parameters.Add("@LOC_X", OleDbType.Numeric).Value = LOC_X;
            adoCommand.Parameters.Add("@LOC_Y", OleDbType.Numeric).Value = LOC_Y;
            adoCommand.Parameters.Add("@LOC_WIDTH", OleDbType.Numeric).Value = LOC_WIDTH;
            adoCommand.Parameters.Add("@LOC_HEIGHT", OleDbType.Numeric).Value = LOC_HEIGHT;
            adoCommand.Parameters.Add("@TEXT", OleDbType.VarChar).Value = TEXT;
            adoCommand.Parameters.Add("@TEXT_SIZE", OleDbType.Numeric).Value = TEXT_SIZE;
            adoCommand.Parameters.Add("@TEXT_COLOR", OleDbType.Numeric).Value = TEXT_COLOR;
            adoCommand.Parameters.Add("@TEXT_STYLE", OleDbType.VarChar).Value = TEXT_STYLE;
            adoCommand.Parameters.Add("@TAG_TYPE", OleDbType.Numeric).Value = TAG_TYPE;
            adoCommand.Parameters.Add("@BACK_COLOR", OleDbType.Numeric).Value = BACK_COLOR;
            adoCommand.Parameters.Add("@CREATE_USER_ID", OleDbType.VarChar).Value = CREATE_USER_ID;
            adoCommand.Parameters.Add("@CREATE_TIME", OleDbType.VarChar).Value = CREATE_TIME;
            adoCommand.Parameters.Add("@UPDATE_USER_ID", OleDbType.VarChar).Value = UPDATE_USER_ID;
            adoCommand.Parameters.Add("@UPDATE_TIME", OleDbType.VarChar).Value = UPDATE_TIME;
            adoCommand.Parameters.Add("@NO_MOUSE_EVENT", OleDbType.VarChar).Value = NO_MOUSE_EVENT;
            adoCommand.Parameters.Add("@SIGNAL_FLAG", OleDbType.VarChar).Value = SIGNAL_FLAG;

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
                    strQuery = "UPDATE MFMBRESLOC SET"
                        + " SEQ=?, LOC_X=?, LOC_Y=?, LOC_WIDTH=?, LOC_HEIGHT=?,"
                        + " TEXT=?, TEXT_SIZE=?, TEXT_COLOR=?, TEXT_STYLE=?, TAG_TYPE=?,"
                        + " BACK_COLOR=?, CREATE_USER_ID=?, CREATE_TIME=?, UPDATE_USER_ID=?, UPDATE_TIME=?,"
                        + " NO_MOUSE_EVENT=?, SIGNAL_FLAG=?"
                        + " WHERE FACTORY=?"
                        + " AND RES_ID=?"
                        + " AND RES_TYPE=?"
                        + " AND LAYOUT_ID=?";

                    // Add Parameters
                    adoCommand.Parameters.Add("@SEQ", OleDbType.Numeric).Value = SEQ;
                    adoCommand.Parameters.Add("@LOC_X", OleDbType.Numeric).Value = LOC_X;
                    adoCommand.Parameters.Add("@LOC_Y", OleDbType.Numeric).Value = LOC_Y;
                    adoCommand.Parameters.Add("@LOC_WIDTH", OleDbType.Numeric).Value = LOC_WIDTH;
                    adoCommand.Parameters.Add("@LOC_HEIGHT", OleDbType.Numeric).Value = LOC_HEIGHT;
                    adoCommand.Parameters.Add("@TEXT", OleDbType.VarChar).Value = TEXT;
                    adoCommand.Parameters.Add("@TEXT_SIZE", OleDbType.Numeric).Value = TEXT_SIZE;
                    adoCommand.Parameters.Add("@TEXT_COLOR", OleDbType.Numeric).Value = TEXT_COLOR;
                    adoCommand.Parameters.Add("@TEXT_STYLE", OleDbType.VarChar).Value = TEXT_STYLE;
                    adoCommand.Parameters.Add("@TAG_TYPE", OleDbType.Numeric).Value = TAG_TYPE;
                    adoCommand.Parameters.Add("@BACK_COLOR", OleDbType.Numeric).Value = BACK_COLOR;
                    adoCommand.Parameters.Add("@CREATE_USER_ID", OleDbType.VarChar).Value = CREATE_USER_ID;
                    adoCommand.Parameters.Add("@CREATE_TIME", OleDbType.VarChar).Value = CREATE_TIME;
                    adoCommand.Parameters.Add("@UPDATE_USER_ID", OleDbType.VarChar).Value = UPDATE_USER_ID;
                    adoCommand.Parameters.Add("@UPDATE_TIME", OleDbType.VarChar).Value = UPDATE_TIME;
                    adoCommand.Parameters.Add("@NO_MOUSE_EVENT", OleDbType.VarChar).Value = NO_MOUSE_EVENT;
                    adoCommand.Parameters.Add("@SIGNAL_FLAG", OleDbType.VarChar).Value = SIGNAL_FLAG;

                    // Add Parameters for Condition
                    adoCommand.Parameters.Add("@C_FACTORY", OleDbType.VarChar).Value = FACTORY;
                    adoCommand.Parameters.Add("@C_RES_ID", OleDbType.VarChar).Value = RES_ID;
                    adoCommand.Parameters.Add("@C_RES_TYPE", OleDbType.VarChar).Value = RES_TYPE;
                    adoCommand.Parameters.Add("@C_LAYOUT_ID", OleDbType.VarChar).Value = LAYOUT_ID;
                    break;

                case 2:
                    strQuery = "UPDATE MFMBRESLOC SET"
                        + " SEQ=?, LAYOUT_ID=?, LOC_X=?, LOC_Y=?, LOC_WIDTH=?, LOC_HEIGHT=?,"
                        + " TEXT=?, TEXT_SIZE=?, TEXT_COLOR=?, TEXT_STYLE=?, TAG_TYPE=?,"
                        + " BACK_COLOR=?, CREATE_USER_ID=?, CREATE_TIME=?, UPDATE_USER_ID=?, UPDATE_TIME=?,"
                        + " NO_MOUSE_EVENT=?, SIGNAL_FLAG=?"
                        + " WHERE FACTORY=?"
                        + " AND RES_ID=?"
                        + " AND RES_TYPE=?";

                    // Add Parameters
                    adoCommand.Parameters.Add("@SEQ", OleDbType.Numeric).Value = SEQ;
                    adoCommand.Parameters.Add("@LAYOUT_ID", OleDbType.VarChar).Value = LAYOUT_ID;
                    adoCommand.Parameters.Add("@LOC_X", OleDbType.Numeric).Value = LOC_X;
                    adoCommand.Parameters.Add("@LOC_Y", OleDbType.Numeric).Value = LOC_Y;
                    adoCommand.Parameters.Add("@LOC_WIDTH", OleDbType.Numeric).Value = LOC_WIDTH;
                    adoCommand.Parameters.Add("@LOC_HEIGHT", OleDbType.Numeric).Value = LOC_HEIGHT;
                    adoCommand.Parameters.Add("@TEXT", OleDbType.VarChar).Value = TEXT;
                    adoCommand.Parameters.Add("@TEXT_SIZE", OleDbType.Numeric).Value = TEXT_SIZE;
                    adoCommand.Parameters.Add("@TEXT_COLOR", OleDbType.Numeric).Value = TEXT_COLOR;
                    adoCommand.Parameters.Add("@TEXT_STYLE", OleDbType.VarChar).Value = TEXT_STYLE;
                    adoCommand.Parameters.Add("@TAG_TYPE", OleDbType.Numeric).Value = TAG_TYPE;
                    adoCommand.Parameters.Add("@BACK_COLOR", OleDbType.Numeric).Value = BACK_COLOR;
                    adoCommand.Parameters.Add("@CREATE_USER_ID", OleDbType.VarChar).Value = CREATE_USER_ID;
                    adoCommand.Parameters.Add("@CREATE_TIME", OleDbType.VarChar).Value = CREATE_TIME;
                    adoCommand.Parameters.Add("@UPDATE_USER_ID", OleDbType.VarChar).Value = UPDATE_USER_ID;
                    adoCommand.Parameters.Add("@UPDATE_TIME", OleDbType.VarChar).Value = UPDATE_TIME;
                    adoCommand.Parameters.Add("@NO_MOUSE_EVENT", OleDbType.VarChar).Value = NO_MOUSE_EVENT;
                    adoCommand.Parameters.Add("@SIGNAL_FLAG", OleDbType.VarChar).Value = SIGNAL_FLAG;

                    // Add Parameters for Condition
                    adoCommand.Parameters.Add("@C_FACTORY", OleDbType.VarChar).Value = FACTORY;
                    adoCommand.Parameters.Add("@C_RES_ID", OleDbType.VarChar).Value = RES_ID;
                    adoCommand.Parameters.Add("@C_RES_TYPE", OleDbType.VarChar).Value = RES_TYPE;
                    
                    break;
             

                case 5:
                    strQuery = "UPDATE MFMBRESLOC SET"
                        + " SEQ = SEQ - 1"
                        + " WHERE FACTORY=?"
                        + " AND LAYOUT_ID=?"
                        + " AND SEQ>?";

                    // Add Parameters for Condition
                    adoCommand.Parameters.Add("@C_FACTORY", OleDbType.VarChar).Value = FACTORY;
                    adoCommand.Parameters.Add("@C_LAYOUT_ID", OleDbType.VarChar).Value = LAYOUT_ID;
                    adoCommand.Parameters.Add("@C_SEQ", OleDbType.Integer).Value = SEQ;
                    break;
      
                case 6:
                    strQuery = "UPDATE MFMBRESLOC SET"
                        + " SEQ = SEQ + 1"
                        + " WHERE FACTORY=?"
                        + " AND LAYOUT_ID=?"
                        + " AND SEQ<?";

                    // Add Parameters for Condition
                    adoCommand.Parameters.Add("@C_FACTORY", OleDbType.VarChar).Value = FACTORY;
                    adoCommand.Parameters.Add("@C_LAYOUT_ID", OleDbType.VarChar).Value = LAYOUT_ID;
                    adoCommand.Parameters.Add("@C_SEQ", OleDbType.Integer).Value = SEQ;
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
                    strQuery = "SELECT * FROM MFMBRESLOC"
                        + " ORDER BY FACTORY ASC"
                        + " , RES_ID ASC"
                        + " , RES_TYPE ASC"
                        + " , LAYOUT_ID ASC";

                    break;

                case 2:
                    strQuery = "SELECT * FROM MFMBRESLOC"
                        + " WHERE FACTORY=?"
                        + " AND LAYOUT_ID=?"
                        + " AND RES_ID=?"
                        + " AND RES_TYPE=?"
                        + " ORDER BY RES_ID";

                    adoAdapter.SelectCommand.Parameters.Add("@FACTORY", OleDbType.VarChar).Value = FACTORY;
                    adoAdapter.SelectCommand.Parameters.Add("@LAYOUT_ID", OleDbType.VarChar).Value = LAYOUT_ID;
                    adoAdapter.SelectCommand.Parameters.Add("@RES_ID", OleDbType.VarChar).Value = RES_ID;
                    adoAdapter.SelectCommand.Parameters.Add("@RES_TYPE", OleDbType.VarChar).Value = "R";
    
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

