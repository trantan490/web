using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.IO;
using System.Runtime.InteropServices;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

using Miracom.SmartWeb.FWX;

namespace Miracom.SmartWeb.UI.Board
{
    public partial class BRD0001 : Miracom.SmartWeb.FWX.udcUIBase
    {
        //private AxWebBrowser wbControl = null;

        public BRD0001()
        {
            InitializeComponent();
        }

        private string GetURL()
        {
            try
            {
                return GlobalVariable.AppConfig.GetValue("//appSettings//add[@key='urlQnABoard']");
            }
            catch (Exception ex)
            {
                GlobalVariable.AppErrorLog.WriteExceptionLog(ex, "Config File:");
                throw ex;
            }
        }

        private void BRD0001_Load(object sender, EventArgs e)
        {
            object objURL;

            try
            {
                //objURL = GetURL() + GlobalVariable.gsUserID;
                objURL = "http://12.230.58.144/Board/List.aspx?USERID=" + GlobalVariable.gsUserID;

                //wbControl = new AxWebBrowser();
                //wbControl.Dock = DockStyle.Fill;
                //wbControl.NavigateComplete2 += new DWebBrowserEvents2_NavigateComplete2EventHandler(wbControl_NavigateComplete2);
                //this.pnlBoard.Controls.Add(wbControl);

                //WinXpStyle.ControlLoad(sender, e, this);

                //wbControl.Navigate2(ref objURL);

                webBrowser1.Navigate("http://12.230.58.144/Board/List.aspx?USERID=" + GlobalVariable.gsUserID);
            }
            catch (Exception ex)
            {
                CmnFunction.ShowMsgBox(ex.ToString());
            }
        }

        //private void wbControl_NavigateComplete2(object sender, DWebBrowserEvents2_NavigateComplete2Event e)
        //{

        //}
    }
}

