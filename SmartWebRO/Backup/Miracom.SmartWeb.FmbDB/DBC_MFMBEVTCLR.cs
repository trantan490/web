//-----------------------------------------------------------------------------
//
//   System      : MES
//   File Name   : DBC_MFMBEVTCLR.cs
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
public class DBC_MFMBEVTCLR
    : ICloneable
{


#region " Fields Definition "

    // Fields
    private string m_factory;                                // VARCHAR(10)
    private string m_event_id;                               // VARCHAR(12)
    private int m_color;                                     // INTEGER(8)
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
    /// Gets or sets the 'EVENT_ID' value.
    /// </summary>
    public string EVENT_ID
    {
        get
        {
            if (m_event_id == null )
            {
                m_event_id = " ";
            }
            return m_event_id;
        }
        set
        {
            if (value == null || value == "" )
            {
                m_event_id = " ";
            }
            m_event_id = value;
        }
    }

    /// <summary>
    /// Gets or sets the 'COLOR' value.
    /// </summary>
    public int COLOR
    {
        get
        {
            return m_color;
        }
        set
        {
            m_color = value;
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
    public DBC_MFMBEVTCLR(ref DB_Common dbc)
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
        EVENT_ID = " ";
        COLOR = 0;
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
        DBC_MFMBEVTCLR MFMBEVTCLR = null;
        try
        {
            MFMBEVTCLR = new DBC_MFMBEVTCLR(ref _dbc);

            MFMBEVTCLR.FACTORY = this.FACTORY;
            MFMBEVTCLR.EVENT_ID = this.EVENT_ID;
            MFMBEVTCLR.COLOR = this.COLOR;
            MFMBEVTCLR.CREATE_USER_ID = this.CREATE_USER_ID;
            MFMBEVTCLR.CREATE_TIME = this.CREATE_TIME;
            MFMBEVTCLR.UPDATE_USER_ID = this.UPDATE_USER_ID;
            MFMBEVTCLR.UPDATE_TIME = this.UPDATE_TIME;
        }
        catch(Exception ex)
        {
            if (ex.GetType().ToString() == "System.Data.OleDb.OleDbException")
                _dbc.gErrors.SetErrors(((OleDbException)ex).Errors);
            else
                _dbc.gErrors.SetErrors(ex);

            return null;
        }

        return MFMBEVTCLR;
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
            EVENT_ID = (string)(Row["EVENT_ID"]);
            COLOR = Convert.ToInt32(Row["COLOR"]);
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
                    strQuery = "SELECT * FROM MFMBEVTCLR"
                        + " WHERE FACTORY=?"
                        + " AND EVENT_ID=?";

                    // Add Parameters
                    adoAdapter.SelectCommand.Parameters.Add("@FACTORY", OleDbType.VarChar).Value = FACTORY;
                    adoAdapter.SelectCommand.Parameters.Add("@EVENT_ID", OleDbType.VarChar).Value = EVENT_ID;
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
                    case "EVENT_ID":
                        EVENT_ID = (string)(adoDataTable.Rows[0]["EVENT_ID"]);
                        break;
                    case "COLOR":
                        COLOR = Convert.ToInt32(adoDataTable.Rows[0]["COLOR"]);
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
                        strQuery = "SELECT * FROM MFMBEVTCLR"
                            + " WITH (UPDLOCK) WHERE FACTORY=?"
                            + " AND EVENT_ID=?";
                    }
                    else if (_dbc.gDbType == (int)DB_TYPE.ORACLE)
                    {
                        strQuery = "SELECT * FROM MFMBEVTCLR"
                            + " WHERE FACTORY=?"
                            + " AND EVENT_ID=?"
                            + " FOR UPDATE";
                    }
                    else if (_dbc.gDbType == (int)DB_TYPE.DB2) 
                    {
                    }

                    // Add Parameters
                    adoAdapter.SelectCommand.Parameters.Add("@FACTORY", OleDbType.VarChar).Value = FACTORY;
                    adoAdapter.SelectCommand.Parameters.Add("@EVENT_ID", OleDbType.VarChar).Value = EVENT_ID;
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
                    case "EVENT_ID":
                        EVENT_ID = (string)(adoDataTable.Rows[0]["EVENT_ID"]);
                        break;
                    case "COLOR":
                        COLOR = Convert.ToInt32(adoDataTable.Rows[0]["COLOR"]);
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
                    strQuery = "SELECT COUNT(*) FROM MFMBEVTCLR"
                        + " WHERE FACTORY=?"
                        + " AND EVENT_ID=?";

                    // Add Parameters
                    adoCommand.Parameters.Add("@FACTORY", OleDbType.VarChar).Value = FACTORY;
                    adoCommand.Parameters.Add("@EVENT_ID", OleDbType.VarChar).Value = EVENT_ID;
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
                    strQuery = "DELETE FROM MFMBEVTCLR"
                        + " WHERE FACTORY=?"
                        + " AND EVENT_ID=?";

                    // Add Parameters
                    adoCommand.Parameters.Add("@FACTORY", OleDbType.VarChar).Value = FACTORY;
                    adoCommand.Parameters.Add("@EVENT_ID", OleDbType.VarChar).Value = EVENT_ID;
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

            strQuery = "INSERT INTO MFMBEVTCLR ("
                + " FACTORY, EVENT_ID, COLOR, CREATE_USER_ID, CREATE_TIME,"
                + " UPDATE_USER_ID, UPDATE_TIME)"
                + " VALUES ("
                + " ?, ?, ?, ?, ?, ?, ?)";

            // Add Parameters
            adoCommand.Parameters.Add("@FACTORY", OleDbType.VarChar).Value = FACTORY;
            adoCommand.Parameters.Add("@EVENT_ID", OleDbType.VarChar).Value = EVENT_ID;
            adoCommand.Parameters.Add("@COLOR", OleDbType.Numeric).Value = COLOR;
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
                    strQuery = "UPDATE MFMBEVTCLR SET"
                        + " COLOR=?, CREATE_USER_ID=?, CREATE_TIME=?, UPDATE_USER_ID=?, UPDATE_TIME=?"
                        + " WHERE FACTORY=?"
                        + " AND EVENT_ID=?";

                    // Add Parameters
                    adoCommand.Parameters.Add("@COLOR", OleDbType.Numeric).Value = COLOR;
                    adoCommand.Parameters.Add("@CREATE_USER_ID", OleDbType.VarChar).Value = CREATE_USER_ID;
                    adoCommand.Parameters.Add("@CREATE_TIME", OleDbType.VarChar).Value = CREATE_TIME;
                    adoCommand.Parameters.Add("@UPDATE_USER_ID", OleDbType.VarChar).Value = UPDATE_USER_ID;
                    adoCommand.Parameters.Add("@UPDATE_TIME", OleDbType.VarChar).Value = UPDATE_TIME;

                    // Add Parameters for Condition
                    adoCommand.Parameters.Add("@C_FACTORY", OleDbType.VarChar).Value = FACTORY;
                    adoCommand.Parameters.Add("@C_EVENT_ID", OleDbType.VarChar).Value = EVENT_ID;
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
                    strQuery = "SELECT * FROM MFMBEVTCLR"
                        + " ORDER BY FACTORY ASC"
                        + " , EVENT_ID ASC";

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

