using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace Miracom.SmartWeb.UI.Controls
{
    public partial class udcCUSReport001 : Miracom.SmartWeb.FWX.udcUIBase
    {
        private string m_factory = "";

        public udcCUSReport001()
        {
            InitializeComponent();

            // 2012-02-13-임종우 : 화면 오픈시 메뉴의 Desc값으로 Title변경
            if (GlobalVariable.gsSelFuncName != "")
                this.lblTitle.Text = GlobalVariable.gsSelFuncName;
        }

        #region "Properties"

        [Browsable(true), DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        public string CurrentFactory
        {
            get
            {
                return m_factory;
            }
            set
            {
                m_factory = value;
            }
        }

        #endregion

        private void udcCUSReport001_Load(object sender, EventArgs e)
        {
            if (this.DesignMode == false)
            {
                pnlDetail.Visible = false;
                // 2020-02-05-김미경 : 다국어 변환 함수 추가
                LanguageFunction.ToClientLanguage(this);
            }          
        }
        private void btnView_MouseUp(object sender, MouseEventArgs e)
        {
            // 2020-02-05-김미경 : 다국어 변환 함수 추가
            LanguageFunction.ToClientLanguage(this);
        }
        private void btnDetail_Click(object sender, EventArgs e)
        {
            try
            {
                if (pnlDetail.Visible == false)
                {
                    pnlDetail.Visible = true;
                    this.btnDetail.Image = global::Miracom.SmartWeb.UI.Properties.Resources._020prjup;
                }
                else
                {
                    pnlDetail.Visible = false;
                    this.btnDetail.Image = global::Miracom.SmartWeb.UI.Properties.Resources._021prjdown;
                }
            }
            catch (Exception ex)
            {
                CmnFunction.ShowMsgBox("udcCUSReport001.btnDetail_Click()" + "\r\n" + ex.Message);
            }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            try
            {
                this.OnCloseLayoutForm();
                this.Dispose();
            }
            catch (Exception ex)
            {
                CmnFunction.ShowMsgBox("udcCUSReport001.btnClose_Click()" + "\r\n" + ex.Message);
            }
        }

        public void SetFactory(string strFactory)
        {
            m_factory = strFactory;
            udcCondition1.sFactory = strFactory;
            udcCondition2.sFactory = strFactory;
            udcCondition3.sFactory = strFactory;
            udcCondition4.sFactory = strFactory;
            udcCondition5.sFactory = strFactory;
            udcCondition6.sFactory = strFactory;
            udcCondition7.sFactory = strFactory;
            udcCondition8.sFactory = strFactory;
        }       
    }
}

