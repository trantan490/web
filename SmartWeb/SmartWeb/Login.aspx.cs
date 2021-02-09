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
using System.Net;

public partial class Login : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            LoadCookie(); // 반드시 처음로드할 때에만 저장된 쿠키 요청 
        }

    }    

    // 아이디 저장 체크박스에 따른 저장된 쿠키 읽어오기
    private void LoadCookie()
    {
        if (Request.Cookies["SavedUserID"] != null)
        {
            txtUserID.Text = Request.Cookies["SavedUserID"].Value;
            if (Request.Cookies["SavedchkID"] != null)

            {
                if (Request.Cookies["SavedchkID"].Value.ToString().Equals("Y"))
                {
                    chkSaveID.Checked = true;
                    txtPwd.Focus();
                }
                else
                {
                    chkSaveID.Checked = false;
                    txtUserID.Focus();
                }
            }
            
        }
    }   


    protected void Page_LoadComplete(object sender, EventArgs e)
    {
        Response.Cookies.Clear();
        Session.Clear();
    }

    protected void btnLogin_Click(object sender, ImageClickEventArgs e)
    {
        string strConnectionString = "";
        string strMainPath = "";
        string strQuery = "";

        // 2009-07-21-임종우 : 하나 내부 IP & 분당 IP
        // 2015-02-10-임종우 : 미국 법인 IP 추가
        // 2020-05-19-임종우 : V2 법인 IP 추가
        string HanaIP = "12.230";
        string HanaIP_BunDang = "192.168.1";
        string HanaIP_US = "198.91.99.34";
        string HanaIP_VINA1 = "14.238.27.174";
        string HanaIP_VINA2 = "117.4.107.0";

        strMainPath = ConfigurationManager.AppSettings["MainPath"];

        if(txtUserID.Text == "WEBADMIN" && txtPwd.Text == "WEBADMIN")
        {
            Response.Cookies["UserInfo"]["USERID"] = txtUserID.Text.ToLower();
            Response.Cookies["UserInfo"]["FACTORY"] = "SYSTEM";
            Response.Cookies["UserInfo"]["USER_DESC"] = "ADMIN USER";
            Session.Add("USERID", "WEBADMIN");
            Session.Add("FACTORY", "SYSTEM");
            Session.Add("USER_DESC", "ADMIN USER");
            Session.Add("MAIN_PATH", strMainPath);
            Session.Add("LOGIN", "Y");
            FormsAuthentication.SetAuthCookie(txtUserID.Text, false);

            //========User LogIn Log등록========//
            if (GetLoginCount("SYSTEM", txtUserID.Text.ToUpper(), "ADMIN_GROUP") != 0)
            {
                UpdateLogIn("SYSTEM", txtUserID.Text.ToUpper(), "ADMIN_GROUP");
            }
            else
            {
                InsertLogIn("SYSTEM", txtUserID.Text.ToUpper(), "ADMIN_GROUP");
            }
            //========User LogIn Log등록========//

            Response.Redirect(strMainPath + "LoadMain.aspx");
            
        }
        else
        {
            try
            {
                OleDbDataAdapter adoAdapter = new OleDbDataAdapter();
                OleDbConnection gOleDbConnection = new OleDbConnection();
                DataTable adoDataTable = new DataTable();

                strConnectionString = ConfigurationManager.AppSettings["ConnectionString"];
                adoAdapter.SelectCommand = new OleDbCommand();
                gOleDbConnection.ConnectionString = strConnectionString;
                adoAdapter.SelectCommand.Connection = gOleDbConnection;
                adoAdapter.SelectCommand.Connection.Open();

                strQuery = "SELECT * FROM RWEBUSRDEF WHERE FACTORY=? AND USER_ID=? AND PASSWORD=TRIM(PK_MESPWD.ENCPASSWORD(?, ?))";

                adoAdapter.SelectCommand.Parameters.Add("@FACTORY", OleDbType.VarChar).Value = "SYSTEM";
                adoAdapter.SelectCommand.Parameters.Add("@USER_ID_1", OleDbType.VarChar).Value = txtUserID.Text;
                adoAdapter.SelectCommand.Parameters.Add("@USER_ID_2", OleDbType.VarChar).Value = txtUserID.Text;
                adoAdapter.SelectCommand.Parameters.Add("@PASSWORD", OleDbType.VarChar).Value = txtPwd.Text;

                adoAdapter.SelectCommand.CommandText = strQuery;

                adoAdapter.Fill(adoDataTable);

                if (adoDataTable.Rows.Count < 1)
                {
                    adoAdapter.SelectCommand.Connection.Close();

                    Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "alert", "<script language='javascript'>alert('Please Check User Info.'); location.href='" + strMainPath + "Login.aspx';</script>");
                    //Page.RegisterClientScriptBlock("alert", "<script language='javascript'>alert('Please Check User Info.'); location.href='" + strMainPath + "Login.aspx';</script>");

                    //Response.Redirect(strMainPath + "Login.aspx");
                }
                else
                {
                    string myIP = string.Empty;

                    myIP = Request.UserHostAddress;

                    // 2009-07-21-임종우 : 외부 IP 일 경우 외부접속 허용자 인지 체크
                    // 2015-02-10-임종우 : 미국 법인 IP 추가
                    // 2020-05-19-임종우 : V2 법인 IP 추가
                    if (myIP.Substring(0, 6) != HanaIP && myIP.Substring(0, 9) != HanaIP_BunDang && myIP.Substring(0, 12) != HanaIP_US && myIP.Substring(0, 13) != HanaIP_VINA1 && myIP.Substring(0, 11) != HanaIP_VINA2)
                    {
                        string strOutSide = adoDataTable.Rows[0]["USER_CMF_10"].ToString();

                        if (strOutSide != "Y")
                        {
                            Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "alert", "<script language='javascript'>alert('허가 된 외부 접속 사용자가 아닙니다.'); location.href='" + strMainPath + "Login.aspx';</script>");
                        }
                        else
                        {
                            //========User LogIn Log등록========//
                            if (GetLoginCount(adoDataTable.Rows[0]["FACTORY"].ToString(), txtUserID.Text.ToUpper(), adoDataTable.Rows[0]["SEC_GRP_ID"].ToString()) != 0)
                            {
                                UpdateLogIn(adoDataTable.Rows[0]["FACTORY"].ToString(), txtUserID.Text.ToUpper(), adoDataTable.Rows[0]["SEC_GRP_ID"].ToString());
                            }
                            else
                            {
                                InsertLogIn(adoDataTable.Rows[0]["FACTORY"].ToString(), txtUserID.Text.ToUpper(), adoDataTable.Rows[0]["SEC_GRP_ID"].ToString());
                            }
                            //========User LogIn Log등록========//

                            Response.Cookies["UserInfo"]["USERID"] = txtUserID.Text.ToLower();
                            Response.Cookies["UserInfo"]["FACTORY"] = adoDataTable.Rows[0]["FACTORY"].ToString();
                            Response.Cookies["UserInfo"]["USER_DESC"] = adoDataTable.Rows[0]["USER_DESC"].ToString();
                            Session.Add("USERID", adoDataTable.Rows[0]["USER_ID"].ToString());
                            Session.Add("FACTORY", adoDataTable.Rows[0]["FACTORY"].ToString());
                            Session.Add("USER_DESC", adoDataTable.Rows[0]["USER_DESC"].ToString());
                            Session.Add("MAIN_PATH", strMainPath);
                            Session.Add("LOGIN", "Y");
                            //Session.Add("CAS_STATUS", this.u);

                            SaveCookie();

                            FormsAuthentication.SetAuthCookie(txtUserID.Text, false);

                            adoAdapter.SelectCommand.Connection.Close();
                            Response.Redirect(strMainPath + "LoadMain.aspx");
                        }

                    }
                    else
                    {
                        //========User LogIn Log등록========//
                        if (GetLoginCount(adoDataTable.Rows[0]["FACTORY"].ToString(), txtUserID.Text.ToUpper(), adoDataTable.Rows[0]["SEC_GRP_ID"].ToString()) != 0)
                        {
                            UpdateLogIn(adoDataTable.Rows[0]["FACTORY"].ToString(), txtUserID.Text.ToUpper(), adoDataTable.Rows[0]["SEC_GRP_ID"].ToString());
                        }
                        else
                        {
                            InsertLogIn(adoDataTable.Rows[0]["FACTORY"].ToString(), txtUserID.Text.ToUpper(), adoDataTable.Rows[0]["SEC_GRP_ID"].ToString());
                        }
                        //========User LogIn Log등록========//

                        Response.Cookies["UserInfo"]["USERID"] = txtUserID.Text.ToLower();
                        Response.Cookies["UserInfo"]["FACTORY"] = adoDataTable.Rows[0]["FACTORY"].ToString();
                        Response.Cookies["UserInfo"]["USER_DESC"] = adoDataTable.Rows[0]["USER_DESC"].ToString();
                        Session.Add("USERID", adoDataTable.Rows[0]["USER_ID"].ToString());
                        Session.Add("FACTORY", adoDataTable.Rows[0]["FACTORY"].ToString());
                        Session.Add("USER_DESC", adoDataTable.Rows[0]["USER_DESC"].ToString());
                        Session.Add("MAIN_PATH", strMainPath);
                        Session.Add("LOGIN", "Y");
                        //Session.Add("CAS_STATUS", this.u);

                        SaveCookie();

                        FormsAuthentication.SetAuthCookie(txtUserID.Text, false);

                        adoAdapter.SelectCommand.Connection.Close();
                        Response.Redirect(strMainPath + "LoadMain.aspx");

                        //String script = "window.open('" + strMainPath + "LoadMain.aspx','MES_Report','fullscreen=yes');";
                        //Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "regkey", script, true);
                        //Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "regkey", "self.close();", true);
                        //Response.ExpiresAbsolute = DateTime.Now;
                    }
                    
                }

            }catch(OleDbException ex)
            {
                throw ex;
            }
        }
    }

    // 현재 사용자의 아이디를 원하는 이름의 쿠키로 하루동안 저장하기
    private void SaveCookie()
    {
        if (chkSaveID.Checked)
        {
            // 하루동안 쿠키 값 보관
            Response.Cookies["SavedUserID"].Expires = DateTime.Now.AddYears(1);
            Response.Cookies["SavedUserID"].Value = txtUserID.Text;
            Response.Cookies["SavedchkID"].Expires = DateTime.Now.AddYears(1); 
            Response.Cookies["SavedchkID"].Value = "Y";
        }
        else
        {
            // 체크가 되어있지 않으면 빈값으로 초기화
            Response.Cookies["SavedUserID"].Value = String.Empty;
            Response.Cookies["SavedchkID"].Value = "N";
        }
    }

    /*Log-in 상태의 해당유저 count */
    public int GetLoginCount(String sFactory, String sUser_id, String sUser_group)
    {
        string strConn = "";
        strConn = ConfigurationManager.AppSettings["ConnectionString"];
        OleDbConnection OleDbConn = new OleDbConnection(strConn);

        String strSQL = "SELECT Count(*) FROM RWEBUSRLOG ";
        strSQL += " Where FACTORY = '" + sFactory + "' ";
        strSQL += " AND USER_ID = '" + sUser_id + "' ";
        strSQL += " AND USER_GROUP ='" + sUser_group + "' ";
        strSQL += " AND INOUT_FLAG ='I' ";

        OleDbCommand SelectCommand = new OleDbCommand(strSQL, OleDbConn);
        OleDbConn.Open();

        int rowCount = 0;

        try
        {
            rowCount = Convert.ToInt32(SelectCommand.ExecuteScalar().ToString());
        }
        catch (OleDbException)
        {
            rowCount = 0;
        }

        OleDbConn.Close();
        return rowCount;
    }

    private bool InsertLogIn(String sFactory, String sUser_id, String sUser_group)
    {
        string strConn = "";
        strConn = ConfigurationManager.AppSettings["ConnectionString"];
        OleDbConnection OleDbConn = new OleDbConnection(strConn);

        string strSQL = "Insert into RWEBUSRLOG(factory, user_id, user_group, login_time) ";
        strSQL += " values('" + sFactory + "','" + sUser_id + "' ,'" + sUser_group + "', TO_CHAR(SYSDATE,'YYYYMMDDHH24MISS'))";

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


    private bool UpdateLogIn(String sFactory, String sUser_id, String sUser_group)
    {
        string strConn = "";
        strConn = ConfigurationManager.AppSettings["ConnectionString"];
        OleDbConnection OleDbConn = new OleDbConnection(strConn);

        string strSQL = "UPDATE RWEBUSRLOG ";
        strSQL += " SET LOGIN_TIME = TO_CHAR(SYSDATE,'YYYYMMDDHH24MISS') , ";
        strSQL += " UPDATE_TIME = ' ', ";
        strSQL += " LOGOUT_TIME = ' '  ";
        strSQL += " Where FACTORY = '" + sFactory + "' ";
        strSQL += " AND USER_ID = '" + sUser_id + "' ";
        strSQL += " AND USER_GROUP ='" + sUser_group + "' ";
        strSQL += " AND INOUT_FLAG ='I' ";

        OleDbCommand UpdateCommand = new OleDbCommand(strSQL, OleDbConn);
        OleDbConn.Open();

        try
        {
            UpdateCommand.ExecuteNonQuery();
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
