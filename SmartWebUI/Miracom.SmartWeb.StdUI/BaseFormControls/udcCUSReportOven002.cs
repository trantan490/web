using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
//using Excel = Microsoft.Office.Interop.Excel;
using Excel;

namespace Miracom.SmartWeb.UI.Controls
{
    public partial class udcCUSReportOven002 : Miracom.SmartWeb.FWX.udcUIBase
    {
        private string m_factory = "";
        private string m_s_title;
        private int m_cond_count;
        private int m_i_condition_custom_panel_height = 0;

        private eBaseFormType m_eformtype = eBaseFormType.BOTH;

        //Condition Type
        public enum eBaseFormType
        {
            BOTH = 0,
            WIP_BASE = 1,
            RAS_BASE = 2,
            NONE = 3
        }

        public udcCUSReportOven002()
        {
            InitializeComponent();

            //화면 오픈시 메뉴의 Desc값으로 Title변경. by John Seo. 2008.10.06.
            if (GlobalVariable.gsSelFuncName != "")
                this.lblTitle.Text = GlobalVariable.gsSelFuncName;
        }

        #region "Properties"

        [Category("BaseForm"), Browsable(true), DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
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

        [Category("BaseForm"), Browsable(true), DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        public int ConditionCount
        {
            get
            {
                return m_cond_count;
            }
            set
            {
                if (value < 0) value = 0;
                if (value > 8) value = 8;

                m_cond_count = value;
                Resize_Condition();
            }
        }

        [Category("BaseForm"), Browsable(true), DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        public string Title
        {
            get
            {
                return m_s_title;
            }
            set
            {
                if (value == "") value = "Title";
                m_s_title = value;
                lblTitle.Text = m_s_title;
            }
        }

        [Category("BaseForm"), Browsable(true), DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        public eBaseFormType BaseFormType
        {
            get
            {
                return m_eformtype;
            }
            set
            {
                m_eformtype = value;
            }
        }

        public int CustomPanelHeight
        {
            set
            {
                m_i_condition_custom_panel_height = value;
            }
        }
        #endregion

        private void Resize_Condition()
        {
            if (m_cond_count < 3) { pnlCondition3.Visible = false; } else { pnlCondition3.Visible = true; }
            if (m_cond_count < 2) { pnlCondition2.Visible = false; } else { pnlCondition2.Visible = true; }
            if (m_cond_count < 1) { pnlCondition1.Visible = false; } else { pnlCondition1.Visible = true; }

            pnlMiddle.Height = 18 + (m_cond_count * 24) + m_i_condition_custom_panel_height;
        }

        private void udcCUSReportOven002_Load(object sender, EventArgs e)
        {
            if (this.DesignMode == false)
            {
                LanguageFunction.ToClientLanguage(this);
            }
        }
        private void btnView_MouseUp(object sender, MouseEventArgs e)
        {
            // 2020-02-05-김미경 : 다국어 변환 함수 추가
            LanguageFunction.ToClientLanguage(this);
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
                CmnFunction.ShowMsgBox("udcCUSReportOven002.btnClose_Click()" + "\r\n" + ex.Message);
            }
        }
    }
}
