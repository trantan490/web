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
    public partial class udcCUSReport002 : Miracom.SmartWeb.FWX.udcUIBase
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
            NONE = 3,
            BUMP_BASE = 4,  //Add. 20150526  
            BOTH_BUMP = 5            
        }

        public udcCUSReport002()
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
                Change_FormType();
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
            if (m_cond_count < 8) { pnlCondition8.Visible = false; } else { pnlCondition8.Visible = true; }
            if (m_cond_count < 7) { pnlCondition7.Visible = false; } else { pnlCondition7.Visible = true; }
            if (m_cond_count < 6) { pnlCondition6.Visible = false; } else { pnlCondition6.Visible = true; }
            if (m_cond_count < 5) { pnlCondition5.Visible = false; } else { pnlCondition5.Visible = true; }
            if (m_cond_count < 4) { pnlCondition4.Visible = false; } else { pnlCondition4.Visible = true; }
            if (m_cond_count < 3) { pnlCondition3.Visible = false; } else { pnlCondition3.Visible = true; }
            if (m_cond_count < 2) { pnlCondition2.Visible = false; } else { pnlCondition2.Visible = true; }
            if (m_cond_count < 1) { pnlCondition1.Visible = false; } else { pnlCondition1.Visible = true; }

            pnlMiddle.Height = 18 + (m_cond_count * 24) + m_i_condition_custom_panel_height;
        }

        private void Change_FormType()
        {
            switch (m_eformtype)
            {
                case eBaseFormType.BOTH:
                    pnlRASDetail.Visible = true;
                    pnlWIPDetail.Visible = true;
                    pnlBUMPDetail.Visible = false;
                    break;
                case eBaseFormType.RAS_BASE:
                    pnlRASDetail.Visible = true;
                    pnlWIPDetail.Visible = false;
                    break;
                case eBaseFormType.WIP_BASE:
                    pnlRASDetail.Visible = false;
                    pnlWIPDetail.Visible = true;
                    pnlBUMPDetail.Visible = false;
                    break;
                case eBaseFormType.NONE:
                    pnlRASDetail.Visible = false;
                    pnlWIPDetail.Visible = false;
                    pnlBUMPDetail.Visible = false;
                    break;
                //Add. 20150526
                case eBaseFormType.BUMP_BASE:
                    pnlRASDetail.Visible = false;
                    pnlWIPDetail.Visible = false;
                    pnlBUMPDetail.Visible = true;
                    break;
                case eBaseFormType.BOTH_BUMP:
                    pnlRASDetail.Visible = true;
                    pnlWIPDetail.Visible = false;
                    pnlBUMPDetail.Visible = true;
                    break;
            }
        }

        private void udcCUSReport002_Load(object sender, EventArgs e)
        {
            if (this.DesignMode == false)
            {
                pnlRASDetail.Visible = false;
                pnlWIPDetail.Visible = false;
                pnlBUMPDetail.Visible = false;
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
                switch (m_eformtype)
                {
                    case eBaseFormType.BOTH:
                        if (pnlWIPDetail.Visible == false)
                        {
                            pnlWIPDetail.Visible = true;
                            pnlRASDetail.Visible = true;
                            this.btnDetail.Image = global::Miracom.SmartWeb.UI.Properties.Resources._020prjup;
                        }
                        else
                        {
                            pnlWIPDetail.Visible = false;
                            pnlRASDetail.Visible = false;                            
                            this.btnDetail.Image = global::Miracom.SmartWeb.UI.Properties.Resources._021prjdown;
                        }
                        break;
                    case eBaseFormType.RAS_BASE:
                        if (pnlRASDetail.Visible == false)
                        {
                            pnlRASDetail.Visible = true;
                            this.btnDetail.Image = global::Miracom.SmartWeb.UI.Properties.Resources._020prjup;
                        }
                        else
                        {
                            pnlRASDetail.Visible = false;
                            this.btnDetail.Image = global::Miracom.SmartWeb.UI.Properties.Resources._021prjdown;
                        }
                        break;
                    case eBaseFormType.WIP_BASE:
                        if (pnlWIPDetail.Visible == false)
                        {
                            pnlWIPDetail.Visible = true;
                            this.btnDetail.Image = global::Miracom.SmartWeb.UI.Properties.Resources._020prjup;
                        }
                        else
                        {
                            pnlWIPDetail.Visible = false;
                            this.btnDetail.Image = global::Miracom.SmartWeb.UI.Properties.Resources._021prjdown;
                        }
                        break;
                    case eBaseFormType.NONE:
                        if (pnlWIPDetail.Visible == false)
                        {
                            pnlWIPDetail.Visible = false;
                            pnlRASDetail.Visible = false;
                            pnlBUMPDetail.Visible = false;
                            this.btnDetail.Image = global::Miracom.SmartWeb.UI.Properties.Resources._020prjup;
                        }
                        else
                        {
                            pnlWIPDetail.Visible = true;
                            pnlRASDetail.Visible = true;
                            pnlBUMPDetail.Visible = true;
                            this.btnDetail.Image = global::Miracom.SmartWeb.UI.Properties.Resources._021prjdown;
                        }
                        break;
                    //Add. 20150526
                    case eBaseFormType.BUMP_BASE:
                        if (pnlBUMPDetail.Visible == false)
                        {
                            pnlBUMPDetail.Visible = true;
                            this.btnDetail.Image = global::Miracom.SmartWeb.UI.Properties.Resources._020prjup;
                        }
                        else
                        {
                            pnlBUMPDetail.Visible = false;
                            this.btnDetail.Image = global::Miracom.SmartWeb.UI.Properties.Resources._021prjdown;
                        }
                        break;
                    case eBaseFormType.BOTH_BUMP:
                        if (pnlBUMPDetail.Visible == false)
                        {
                            pnlBUMPDetail.Visible = true;
                            pnlRASDetail.Visible = true;                            
                            this.btnDetail.Image = global::Miracom.SmartWeb.UI.Properties.Resources._020prjup;
                        }
                        else
                        {
                            pnlBUMPDetail.Visible = false;
                            pnlRASDetail.Visible = false;                            
                            this.btnDetail.Image = global::Miracom.SmartWeb.UI.Properties.Resources._021prjdown;
                        }
                        break;
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

        /// <summary>
        /// 조건 컨트롤에 기본 Factory 를 입력받은 값으로 초기화한다.
        /// </summary>
        /// <param name="strFactory"></param>
        public void SetFactory(string strFactory)
        {
            m_factory = strFactory;
            udcRASCondition1.sFactory = strFactory;
            udcRASCondition2.sFactory = strFactory;
            udcRASCondition3.sFactory = strFactory;
            udcRASCondition4.sFactory = strFactory;
            udcRASCondition5.sFactory = strFactory;
            udcRASCondition6.sFactory = strFactory;
            
            udcWIPCondition1.sFactory = strFactory;
            udcWIPCondition2.sFactory = strFactory;
            udcWIPCondition3.sFactory = strFactory;
            udcWIPCondition4.sFactory = strFactory;
            udcWIPCondition5.sFactory = strFactory;
            udcWIPCondition6.sFactory = strFactory;
            udcWIPCondition7.sFactory = strFactory;
            udcWIPCondition8.sFactory = strFactory;
            udcWIPCondition9.sFactory = strFactory;

            udcBUMPCondition1.sFactory = strFactory;
            udcBUMPCondition2.sFactory = strFactory;
            udcBUMPCondition3.sFactory = strFactory;
            udcBUMPCondition4.sFactory = strFactory;
            udcBUMPCondition5.sFactory = strFactory;
            udcBUMPCondition6.sFactory = strFactory;
            udcBUMPCondition7.sFactory = strFactory;
            udcBUMPCondition8.sFactory = strFactory;
            udcBUMPCondition9.sFactory = strFactory;
            udcBUMPCondition10.sFactory = strFactory;
            udcBUMPCondition11.sFactory = strFactory;
            udcBUMPCondition12.sFactory = strFactory;
        }

        /// <summary>
        /// 조건 컨트롤 초기화
        /// </summary>
        public void InitCondition()
        {
            udcRASCondition1.Init();
            udcRASCondition2.Init();
            udcRASCondition3.Init();
            udcRASCondition4.Init();
            udcRASCondition5.Init();
            udcRASCondition6.Init();

            udcWIPCondition1.Init();
            udcWIPCondition2.Init();
            udcWIPCondition3.Init();
            udcWIPCondition4.Init();
            udcWIPCondition5.Init();
            udcWIPCondition6.Init();
            udcWIPCondition7.Init();
            udcWIPCondition8.Init();
            udcWIPCondition9.Init();

            udcBUMPCondition1.Init();
            udcBUMPCondition2.Init();
            udcBUMPCondition3.Init();
            udcBUMPCondition4.Init();
            udcBUMPCondition5.Init();
            udcBUMPCondition6.Init();
            udcBUMPCondition7.Init();
            udcBUMPCondition8.Init();
            udcBUMPCondition9.Init();
            udcBUMPCondition10.Init();
            udcBUMPCondition11.Init();
            udcBUMPCondition12.Init();
        }
    }
}
