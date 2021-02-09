using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Data.OleDb;

public partial class LogOut : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        string MainPath  = "";
        MainPath = Request.QueryString["sMainPath"];
       // MainPath = Session["MAIN_PATH"].ToString();

        //========User LogOut Log등록========//
        UpdateUserLog(Request.QueryString["sUid"]);
        //UpdateUserLog(Session["USERID"].ToString());
        //========User LogOut Log등록========//

        Response.Cookies.Clear();
        Session.Clear();

        Response.Redirect(MainPath + "Login.aspx"); //For Calcel Back button - Not Login.aspx
    }


    private bool UpdateUserLog(String sUser_id)
    {
        string strConn = "";
        strConn = ConfigurationManager.AppSettings["ConnectionString"];
        OleDbConnection OleDbConn = new OleDbConnection(strConn);

        string strSQL = "UPDATE RWEBUSRLOG ";
        strSQL += " SET LOGOUT_TIME = TO_CHAR(SYSDATE,'YYYYMMDDHH24MISS') , ";
        strSQL += " TIME_INTERVAL = ROUND(((SYSDATE - TO_DATE(LOGIN_TIME,'YYYYMMDDHH24MISS'))  * 86400) / 60) , ";//분단위 INTERVAL_TIME 
        strSQL += " INOUT_FLAG = 'O' , UPDATE_TIME = TO_CHAR(SYSDATE,'YYYYMMDDHH24MISS') ";
        strSQL += " WHERE USER_ID = '" + sUser_id + "' ";
        strSQL += " AND INOUT_FLAG = 'I' ";
        strSQL += " AND (SELECT COUNT(*) FROM RWEBUSRLOG  WHERE USER_ID = '" + sUser_id + "' AND INOUT_FLAG = 'I') = 1 "; //userid로 정상로그인되어있는 row가 1인것

        OleDbCommand InsertCommand = new OleDbCommand(strSQL, OleDbConn);
        OleDbConn.Open();

        try
        {
            InsertCommand.ExecuteNonQuery();
            OleDbConn.Close();
            return true;
        }
        catch (OleDbException ex)
        {
            throw ex;
        }
        finally
        {
            OleDbConn.Close();
        }
    }

}
