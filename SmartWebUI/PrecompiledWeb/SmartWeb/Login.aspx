<%@ page language="C#" autoeventwireup="true" inherits="Login, App_Web_oh8nr2mf" %>

<html >
<head id="Head1" runat="server">
    <title>:::Welcome To Smart Web System</title>
        <meta name="GENERATOR" content="Microsoft Visual Studio .NET 7.1"/>
        <meta name="CODE_LANGUAGE" content="Visual Basic .NET 7.1"/>
        <meta name="vs_defaultClientScript" content="JavaScript"/>
        <meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5"/>
        <meta http-equiv='Cache-Control' content='no-cache'/>
        <meta http-equiv='Pragma' content='no-cache'/> 
        <style> BODY { FONT-SIZE: 9pt; COLOR: #151515; FONT-FAMILY: arial } TD { FONT-SIZE: 9pt; COLOR: #151515; FONT-FAMILY: arial } SELECT { FONT-SIZE: 9pt; COLOR: #151515; FONT-FAMILY: arial } INPUT { FONT-SIZE: 9pt; COLOR: #151515; FONT-FAMILY: arial } DIV { FONT-SIZE: 9pt; COLOR: #151515; FONT-FAMILY: arial } FORM { FONT-SIZE: 9pt; COLOR: #151515; FONT-FAMILY: arial } TEXTAREA { FONT-SIZE: 9pt; COLOR: #151515; FONT-FAMILY: arial } CENTER { FONT-SIZE: 9pt; COLOR: #151515; FONT-FAMILY: arial } P { FONT-SIZE: 9pt } BLOCKQUOTE { FONT-SIZE: 9pt } TD { FONT-SIZE: 9pt } BR { FONT-SIZE: 9pt } A:link { FONT-SIZE: 9pt; COLOR: #222222; FONT-FAMILY: arial; TEXT-DECORATION: none } A:visited { FONT-SIZE: 9pt; COLOR: #222222; FONT-FAMILY: arial; TEXT-DECORATION: none } A:active { FONT-SIZE: 9pt; COLOR: #222222 } A:hover { COLOR: #3469d5; TEXT-DECORATION: none } .t { LINE-HEIGHT: 14pt; TEXT-ALIGN: justify } </style>

        <script language="javascript">
        <!--
			
			javascript:window.history.forward(1);

			function StrToUpperCase(sStep)
			{
			    if ((event.keyCode > 64) && (event.keyCode < 122)) 
                { 
                    if(sStep=='1')
                        UserInfo.txtUserID.value = UserInfo.txtUserID.value.toUpperCase();
                    else if(sStep=='2')
                        UserInfo.txtPwd.value = UserInfo.txtPwd.value.toUpperCase();
                }
			}
            
            function GetCasStatus()
            {
                try
                {
                    if(document.UserInfo.CASCheck.CAS_STATUS!='SUCCESS')
                    {
                      alert("Move to CAS Setup Page");
                      location ='/install/CASpublish.htm';
                    }
                }
                catch(ex)
                {
                    alert(ex);
                }
            }
            
        
        -->
        </script>
    </head>
    <%--onload="setTimeout('reloadFunction()',1000)" --%>
    <body MS_POSITIONING="FlowLayout" bgcolor="#ffffff" scroll="no">

        <form id="UserInfo" method="post" runat="server">
        <OBJECT id=CASCheck width=0% height=0% classid=/bin/Miracom.SmartWeb.MainUI.dll#Miracom.SmartWeb.UI.CASCheck>
        </OBJECT>
        
            <table onactivate="GetCasStatus()" border="0" bottommargin="0" cellpadding="0" cellspacing="0" height="100%"
                leftmargin="0" rightmargin="0" topmargin="0" width="100%">
                <tr>
                    <td align="center" valign="middle" >
                        <table border="0" bottommargin="0" cellpadding="0" cellspacing="0" height="487" leftmargin="0"
                            rightmargin="0" topmargin="0" width="789">
                            <tr>
                                <td align="center" height="119" valign="top" width="877">
                                    <img border="0" src="img/img01_hanalogo.jpg" /><br />
                                </td>
                            </tr>
                            <tr>
                                <td align="left" valign="bottom" width="877">
                                    <table background="img/img02.jpg" border="0" bottommargin="0" cellpadding="0" cellspacing="0"
                                        height="152" leftmargin="0" rightmargin="0" topmargin="0" width="877">
                                        <tr>
                                            <td width="140">
                                            </td>
                                            <td>
                                                <!--- LOGIN--->
                                                <table border="0" bottommargin="0" cellpadding="0" cellspacing="0" height="50" leftmargin="0"
                                                    rightmargin="0" topmargin="0" width="150">
                                                    <tr>
                                                        <td height="20" width="76">
                                                            <img src="img/id.gif" width="76" /><br />
                                                        </td>
                                                        <td height="18" rowspan="4">
                                                            <asp:ImageButton ID="btnLogin" runat="server" ImageUrl="~/img/btn.gif" OnClick="btnLogin_Click" /></td>
                                                    </tr>
                                                    <tr>
                                                        <td height="18">
                                                            <asp:TextBox ID="txtUserID" runat="server" OnKeyUp="javascript:StrToUpperCase('1');" ></asp:TextBox></td>
                                                    </tr>
                                                    <tr>
                                                        <td height="19" width="62">
                                                            <img src="img/pw.gif" /><br />
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td height="18">
                                                            <asp:TextBox ID="txtPwd" runat="server" TextMode="Password" OnKeyUp="javascript:StrToUpperCase('2');"></asp:TextBox><br />
                                                        </td>
                                                    </tr>
                                                </table>
                                                
                                            <td width="3">
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                            <tr>
                                <td align="center" height="216" valign="top" width="877">
                                    <img border="0" src="img/img03.jpg" /><br />
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
            </table>
        </form>
    </body>
</html>