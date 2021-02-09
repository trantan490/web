//-----------------------------------------------------------------------------
//
//   System      : MES
//   File Name   : DBC_MFMBGRPUSR.cs
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
public class DBC_MFMBGRPUSR
    : ICloneable
{


#region " Fields Definition "

    // Fields
    private string m_factory;                                // VARCHAR(10)
    private string m_group_id;                               // VARCHAR(20)
    private string m_user_id;                                // VARCHAR(20)

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
    /// Gets or sets the 'GROUP_ID' value.
    /// </summary>
    public string GROUP_ID
    {
        get
        {
            if (m_group_id == null )
            {
                m_group_id = " ";
            }
            return m_group_id;
        }
        set
        {
            if (value == null || value == "" )
            {
                m_group_id = " ";
            }
            m_group_id = value;
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

#endregion

#region " Function Definition "

    /// <summary>
    /// Creator for Object
    /// <summary>
    public DBC_MFMBGRPUSR(ref DB_Common dbc)
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
        GROUP_ID = " ";
        USER_ID = " ";

        return true;
    }

    /// <summary>
    /// Clone object
    /// </summary>
    /// <returns>object</returns>
    public object Clone()
    {
        DBC_MFMBGRPUSR MFMBGRPUSR = null;
        try
        {
            MFMBGRPUSR = new DBC_MFMBGRPUSR(ref _dbc);

            MFMBGRPUSR.FACTORY = this.FACTORY;
            MFMBGRPUSR.GROUP_ID = this.GROUP_ID;
            MFMBGRPUSR.USER_ID = this.USER_ID;
        }
        catch(Exception ex)
        {
            if (ex.GetType().ToString() == "System.Data.OleDb.OleDbException")
                _dbc.gErrors.SetErrors(((OleDbException)ex).Errors);
            else
                _dbc.gErrors.SetErrors(ex);

            return null;
        }

        return MFMBGRPUSR;
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
            GROUP_ID = (string)(Row["GROUP_ID"]);
            USER_ID = (string)(Row["USER_ID"]);
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
                    strQuery = "SELECT * FROM MFMBGRPUSR"
                        + " WHERE FACTORY=?"
                        + " AND GROUP_ID=?"
                        + " AND USER_ID=?";
                    // Add Parameters
                    adoAdapter.SelectCommand.Parameters.Add("@FACTORY", OleDbType.VarChar).Value = FACTORY;
                    adoAdapter.SelectCommand.Parameters.Add("@GROUP_ID", OleDbType.VarChar).Value = GROUP_ID;
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
                    case "GROUP_ID":
                        GROUP_ID = (string)(adoDataTable.Rows[0]["GROUP_ID"]);
                        break;
                    case "USER_ID":
                        USER_ID = (string)(adoDataTable.Rows[0]["USER_ID"]);
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
                        strQuery = "SELECT * FROM MFMBGRPUSR"
                            + " WITH (UPDLOCK) WHERE FACTORY=?"
                            + " AND GROUP_ID=?"
                            + " AND USER_ID=?";
                    }
                    else if (_dbc.gDbType == (int)DB_TYPE.ORACLE)
                    {
                        strQuery = "SELECT * FROM MFMBGRPUSR"
                            + " WHERE FACTORY=?"
                            + " AND GROUP_ID=?"
                            + " AND USER_ID=?"
                            + " FOR UPDATE";
                    }
                    else if (_dbc.gDbType == (int)DB_TYPE.DB2) 
                    {
                    }

                    // Add Parameters
                    adoAdapter.SelectCommand.Parameters.Add("@FACTORY", OleDbType.VarChar).Value = FACTORY;
                    adoAdapter.SelectCommand.Parameters.Add("@GROUP_ID", OleDbType.VarChar).Value = GROUP_ID;
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
                    case "GROUP_ID":
                        GROUP_ID = (string)(adoDataTable.Rows[0]["GROUP_ID"]);
                        break;
                    case "USER_ID":
                        USER_ID = (string)(adoDataTable.Rows[0]["USER_ID"]);
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
                    strQuery = "SELECT COUNT(*) FROM MFMBGRPUSR"
                        + " WHERE FACTORY=?"
                        + " AND GROUP_ID=?"
                        + " AND USER_ID=?";
                    // Add Parameters
                    adoCommand.Parameters.Add("@FACTORY", OleDbType.VarChar).Value = FACTORY;
                    adoCommand.Parameters.Add("@GROUP_ID", OleDbType.VarChar).Value = GROUP_ID;
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
                    strQuery = "DELETE FROM MFMBGRPUSR"
                        + " WHERE FACTORY=?"
                        + " AND GROUP_ID=?"
                        + " AND USER_ID=?";
                    // Add Parameters
                    adoCommand.Parameters.Add("@FACTORY", OleDbType.VarChar).Value = FACTORY;
                    adoCommand.Parameters.Add("@GROUP_ID", OleDbType.VarChar).Value = GROUP_ID;
                    adoCommand.Parameters.Add("@USER_ID", OleDbType.VarChar).Value = USER_ID;
                    break;

                case 2:
                    // Delete by Primary Key
                    strQuery = "DELETE FROM MFMBGRPUSR"
                        + " WHERE FACTORY=?"
                        + " AND GROUP_ID=?";
                    // Add Parameters
                    adoCommand.Parameters.Add("@FACTORY", OleDbType.VarChar).Value = FACTORY;
                    adoCommand.Parameters.Add("@GROUP_ID", OleDbType.VarChar).Value = GROUP_ID;
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

            strQuery = "INSERT INTO MFMBGRPUSR ("
                + " FACTORY, GROUP_ID, USER_ID)"
                + " VALUES ("
                + " ?, ?, ?)";

            // Add Parameters
            adoCommand.Parameters.Add("@FACTORY", OleDbType.VarChar).Value = FACTORY;
            adoCommand.Parameters.Add("@GROUP_ID", OleDbType.VarChar).Value = GROUP_ID;
            adoCommand.Parameters.Add("@USER_ID", OleDbType.VarChar).Value = USER_ID;

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
                    strQuery = "UPDATE MFMBGRPUSR SET"
                        + " WHERE FACTORY=?"
                        + " AND GROUP_ID=?"
                        + " AND USER_ID=?";
                    // Add Parameters

                    // Add Parameters for Condition
                    adoCommand.Parameters.Add("@C_FACTORY", OleDbType.VarChar).Value = FACTORY;
                    adoCommand.Parameters.Add("@C_GROUP_ID", OleDbType.VarChar).Value = GROUP_ID;
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
                    strQuery = "SELECT * FROM MFMBGRPUSR"
                        + " WHERE FACTORY=?"
                        + " AND GROUP_ID=?"
                        + " AND USER_ID>=?"
                        + " ORDER BY USER_ID ASC";

                    adoAdapter.SelectCommand.Parameters.Add("@FACTORY", OleDbType.VarChar).Value = FACTORY;
                    adoAdapter.SelectCommand.Parameters.Add("@USER_NAME", OleDbType.VarChar).Value = USER_ID;
                    adoAdapter.SelectCommand.Parameters.Add("@GROUP_ID", OleDbType.VarChar).Value = GROUP_ID;
                    break;

            
                case 2:
                    // Select All Record
                    strQuery = "SELECT * FROM MFMBGRPUSR"
                        + " WHERE FACTORY=?"
                        + " AND USER_ID=?"
                        + " AND GROUP_ID>=?"
                        + " ORDER BY GROUP_ID ASC";

                    adoAdapter.SelectCommand.Parameters.Add("@FACTORY", OleDbType.VarChar).Value = FACTORY;
                    adoAdapter.SelectCommand.Parameters.Add("@GROUP_ID", OleDbType.VarChar).Value = GROUP_ID;
                    adoAdapter.SelectCommand.Parameters.Add("@USER_NAME", OleDbType.VarChar).Value = USER_ID;
                    
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

