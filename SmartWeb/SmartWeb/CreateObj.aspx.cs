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

public partial class CreateObj : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        string StrObjScript = "";

        if(Session["LOGIN"] == null)
        {
            Response.Write("window.location.href='Login.aspx';");
            Response.End();
        }
        else if(Session["USERID"] == null)
        {
            Response.Write("alert('This user is not exist. Please Check the user name.');");
            Response.Write("window.location.href='Login.aspx';");
            Response.End();
        }
        else if (Session["USERID"] == null)
        {
            Response.Write("alert('This user is not exist. Please Check the user name.');");
            Response.Write("window.location.href='Login.aspx';");
            Response.End();
        }
        else
        {
            StrObjScript = "document.write('<link href=/bin/Miracom.SmartWeb.MainUI.dll.config rel=Configuration>');" +
                           "document.write('<OBJECT id=_Control1 width=100% height=100% classid=/bin/Miracom.SmartWeb.MainUI.dll#Miracom.SmartWeb.UI.MainForm>');" + 
                           "document.write('<PARAM NAME=USER_ID VALUE=" + Request.QueryString["UserIdName"] + ">');" + 
                           "document.write('</OBJECT>');";
            Response.Write(StrObjScript);
        }
    }
}
