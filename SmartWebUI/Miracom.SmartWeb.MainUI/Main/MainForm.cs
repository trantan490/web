using System;
using System.IO;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Configuration;
using Miracom.SmartWeb.FWX;
using System.Runtime.InteropServices;
using System.Reflection;
using System.Net;

namespace Miracom.SmartWeb.UI
{
    [ClassInterface(ClassInterfaceType.None), ComSourceInterfaces(typeof(ILogOutEvent))]
    
    public partial class MainForm : udcUIBase, IControlCOMIncoming 
    {
        /// <summary>
        /// 2011-04-01-������ : Version ���� ǥ�� �ǵ��� ����
        /// 2017-11-14-������ : �ֽ� Version ���� üũ�Ͽ� �˸� Popup â �κ� �߰�
        /// 2019-11-22-������ : Language ��� �߰�
        /// 2020-04-16-��̰� : �ֽ� ���� �˸� POPâ 10 min -> 1hr ���� (���¼� ����, ������ å�� ���� ��)
        /// </summary>
        /// <param name="hWnd"></param>
        /// <param name="msg"></param>
        /// <param name="wParam"></param>
        /// <param name="lParam"></param>
        /// <returns></returns>
        [DllImport("User32.Dll")]
        public static extern IntPtr PostMessage(IntPtr hWnd, int msg, int wParam, int lParam);  
        
        private object BackImage;
        private int oldSplitterSize = 0;
        private string sCaptionFile = null;
        private string m_TimeOut = "N";
        
        // HTML Script ���� ���� �̺�Ʈ
        public delegate void LogOutClickedHandler();
        public event LogOutClickedHandler LogOutClicked;

        // �ڽ������� ���ο� ���� �����Ҷ� ���� ��������Ʈ (udcUIBase�� ��������Ʈ�� ���)
        private NewForm_FromChild my_del;

        protected void OnLogOutClicked()
        {
            if (LogOutClicked != null)
            {
                GlobalVariable.gsFactory = "";
                GlobalVariable.gsUserID = "";
                GlobalVariable.gsPassword = "";
                Invoke(LogOutClicked, null);
            }
        }
        // HTML Script ���� ���� �Ӽ���.
        public string TimeOut
        {
            get
            {
                return m_TimeOut;
            }
        }

        #region WEB Call object(CreateObj.aspx)
        private string m_UserIdName;

        public string USER_ID
        {
            get
            {
                if (m_UserIdName == null)
                {
                    m_UserIdName = "";
                }
                return m_UserIdName.ToString();
            }

            set
            {
                if (value == null || value.TrimEnd() == "")
                {
                    value = "";
                }
                m_UserIdName = value;
            }
        }
        #endregion

        public MainForm()
        {
            //XP �׸� ������ ���Ͽ� �߰�
            Application.EnableVisualStyles();
            Application.DoEvents();

            InitializeComponent();

            System.Diagnostics.Process CurrentProc = System.Diagnostics.Process.GetCurrentProcess();
            
            Config SmartWebCaption = new Config();

			if (CurrentProc.ProcessName == "UserControlTestContainer")
			{
                //GlobalVariable.AppConfig.LocalcfgFile = @"D:\SYSTEM_HANA\MESPLUS_HANA\SmartWebV42\SmartWeb\bin\SmartWeb.xml";
                //SmartWebCaption.LocalcfgFile = @"D:\SYSTEM_HANA\MESPLUS_HANA\SmartWebV42\SmartWeb\bin\SmartWebCaption.xml";

                //2015-06-23-������: �����ο�������ηκ�����(...MESPLUS_HANA\SmartWebV42\SmartWebUI\Miracom.SmartWeb.MainUI\bin\Debug)
                GlobalVariable.AppConfig.LocalcfgFile = Environment.CurrentDirectory + @"\SmartWeb.xml";
                SmartWebCaption.LocalcfgFile = Environment.CurrentDirectory + @"\SmartWebCaption.xml";
			}
			else
			{
				//Load Config
				GlobalVariable.AppConfig.WebcfgFile = @"/SmartWeb.xml";
				SmartWebCaption.WebcfgFile = @"/SmartWebCaption.xml";
			}
			
            sCaptionFile = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + @"\SmartWeb\SmartWebCaption.xml";
            SmartWebCaption.CopyDoc(sCaptionFile);
            
            Config SmartWebOption = new Config();
            SmartWebOption.LocalcfgFile = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + @"\SmartWeb\SmartWebOption.xml";

            GlobalVariable.giLogOutTime = Convert.ToInt32(SmartWebOption.GetValue("//appSettings//add[@key='AutoLogOutTime']"));

            try
            {
                GlobalVariable.gcLanguage = SmartWebOption.GetValue("//appSettings//add[@key='Language']").Trim()[0];

                 // SOURCE MERGE : JUHYEON.JUNG , FACTORY ���� �ʱ�ȭ. ( SmartWeb.XML ) ���� �ش簪�� ����.
                GlobalVariable.gsAssyDefaultFactory = GlobalVariable.AppConfig.GetValue("//appSettings//add[@key='AssyDefaultFactory']");
                GlobalVariable.gsTestDefaultFactory = GlobalVariable.AppConfig.GetValue("//appSettings//add[@key='TestDefaultFactory']");
                GlobalVariable.gsFgsDefaultFactory = GlobalVariable.AppConfig.GetValue("//appSettings//add[@key='FgsDefaultFactory']");
                GlobalVariable.gsGlovalSite = GlobalVariable.AppConfig.GetValue("//appSettings//add[@key='GlovalSite']");

                
            }
            catch
            {
                GlobalVariable.gcLanguage = '1';

                // SOURCE MERGE : JUHYEON.JUNG , FACTORY ���� �ʱ�ȭ. : KOREA ����
                GlobalVariable.gsAssyDefaultFactory = "HMKA1";
                GlobalVariable.gsTestDefaultFactory = "HMKT1";
                GlobalVariable.gsFgsDefaultFactory = "FGS";
                GlobalVariable.gsGlovalSite = "K1";
            }

            if (CmnFunction.Trim(GlobalVariable.gsAssyDefaultFactory) == "")
            {
                GlobalVariable.gsAssyDefaultFactory = "HMKA1";
            }
            if (CmnFunction.Trim(GlobalVariable.gsTestDefaultFactory) == "")
            {
                GlobalVariable.gsTestDefaultFactory = "HMKT1";
            }
            if (CmnFunction.Trim(GlobalVariable.gsFgsDefaultFactory) == "")
            {
                GlobalVariable.gsFgsDefaultFactory = "FGS";
            }
            if (CmnFunction.Trim(GlobalVariable.gsGlovalSite) == "")
            {
                GlobalVariable.gsGlovalSite = "K1";
            }
            
            LanguageFunction.LoadCaptionResource(sCaptionFile);
            LanguageFunction.ToClientLanguage(this);

            //==== Tab Control�� �ʱ�ȭ�Ͽ� ����̹����� ���̵��� �Ѵ�. ====
            tabLayout.Visible = false;
            BackImage = splitContainer.Panel2.BackgroundImage.Clone();
            CmnFunction.oComm.SetUrl();
            CmnFunction.oComm.SetTimeOut(20000);
            CmnInitFunction.InitSender(GlobalVariable.gsSiteID, 20000);

        }

        //public MainForm(string UserName)
        //{

        //    //XP �׸� ������ ���Ͽ� �߰�
        //    Application.EnableVisualStyles();
        //    Application.DoEvents();

        //    InitializeComponent();
        //    this.USER_ID = UserName;

        //    //Load Config
        //    GlobalVariable.AppConfig.WebcfgFile = @"/SmartWeb.xml";

        //    Config SmartWebCaption = new Config();
        //    SmartWebCaption.WebcfgFile = "/SmartWebCaption.xml";
        //    sCaptionFile = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + @"\SmartWeb\SmartWebCaption.xml";
        //    SmartWebCaption.CopyDoc(sCaptionFile);

        //    Config SmartWebOption = new Config();
        //    SmartWebOption.LocalcfgFile = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + @"\SmartWeb\SmartWebOption.xml";

        //    GlobalVariable.giLogOutTime = Convert.ToInt32(SmartWebOption.GetValue("//appSettings//add[@key='AutoLogOutTime']"));

        //    try
        //    {
        //        GlobalVariable.gcLanguage = SmartWebOption.GetValue("//appSettings//add[@key='Language']").Trim();
        //    }
        //    catch
        //    {
        //        GlobalVariable.gcLanguage = "1";
        //    }

        //    LanguageFunction.LoadCaptionResource(sCaptionFile);
        //    LanguageFunction.ToClientLanguage(this);

        //    //==== Tab Control�� �ʱ�ȭ�Ͽ� ����̹����� ���̵��� �Ѵ�. ====
        //    tabLayout.Visible = false;
        //    BackImage = splitContainer.Panel2.BackgroundImage.Clone();
        //    CmnFunction.oComm.SetUrl();
        //    CmnFunction.oComm.SetTimeOut(20000);
        //    CmnInitFunction.InitSender(GlobalVariable.gsSiteID, 20000);

        //}

        /// <summary>
        /// Static �����ڸ� �̿��ؼ� �ʱ�ȭ �ؾ��Ұ͵��� �ѹ��� �����Ű���� ��
        /// �ʱ�ȭ����: �����α�, ����ī���Ͱ�ü �ʱ�ȭ, ���Ṯ�ڿ�ó��(��ȣȭ����)
        /// </summary>
        /// 


        static MainForm()
        {
            System.Threading.Thread.CurrentThread.CurrentUICulture = new System.Globalization.CultureInfo("");
        }

        private Miracom.SmartWeb.FWX.udcUIBase SelectForm(string FormName)
        {
            Miracom.SmartWeb.FWX.udcUIBase tempForm = null;
            Assembly assm = null;

            my_del += new NewForm_FromChild(NewForm_From_Child_1);  // ��������Ʈ�� ȣ���� �Լ� ����
                        
            System.Diagnostics.Process CurrentProc = System.Diagnostics.Process.GetCurrentProcess();
            // 2010-07-31-������ : ������ �����̶� �ּ�ó���ϰ�, �� �ٷ� ������.
            assm = Assembly.Load("Miracom.SmartWeb.StdUI");
            //if (CurrentProc.ProcessName == "UserControlTestContainer")
            //{
            //    assm = Assembly.Load("Miracom.SmartWeb.StdUI");
            //    //assm = Assembly.LoadFile(@"D:\MESPLUS_HANA\SmartWebV42\SmartWebUI\Miracom.SmartWeb.MainUI\bin\Debug\Miracom.SmartWeb.StdUI.dll");
            //}
            //else
            //{
            //    assm = Assembly.Load("Miracom.SmartWeb.StdUI");
            //}

            Type[] Types = assm.GetTypes();

            foreach (Type TempType in Types)
            {
                if (TempType.Name.ToString() == FormName)
                {
                    tempForm = (udcUIBase)Activator.CreateInstance(TempType);
                    tempForm.Form_delegate = my_del;    // �ڽĿ��� ��������Ʈ �Ѱ���
                    break;
                }
            }

            // Miracom.SmartWeb.CusUI
            if (tempForm == null)
            {
				if (CurrentProc.ProcessName == "UserControlTestContainer")
				{
					assm = Assembly.Load("Miracom.SmartWeb.CusUI");
					//assm = Assembly.LoadFile(@"D:\MESPLUS_HANA\SmartWebV42\SmartWebUI\Miracom.SmartWeb.MainUI\bin\Debug\Miracom.SmartWeb.CusUI.dll");
				}
				else
				{
					assm = Assembly.Load("Miracom.SmartWeb.CusUI");
				}

                Types = assm.GetTypes();

                foreach (Type TempType in Types)
                {
                    if (TempType.Name.ToString() == FormName)
                    {
                        tempForm = (udcUIBase)Activator.CreateInstance(TempType);
                        tempForm.Form_delegate = my_del;
                        break;
                    }
                }
            }

            // Hana.CUS
            if (tempForm == null)
            {
				if (CurrentProc.ProcessName == "UserControlTestContainer")
				{
					assm = Assembly.Load("Hana.CUS");
					//assm = Assembly.LoadFile(@"D:\MESPLUS_HANA\SmartWebV42\SmartWebUI\Miracom.SmartWeb.MainUI\bin\Debug\Miracom.SmartWeb.CusUI.dll");
				}
				else
				{
					assm = Assembly.Load("Hana.CUS");
				}

                Types = assm.GetTypes();

                foreach (Type TempType in Types)
                {
                    if (TempType.Name.ToString() == FormName)
                    {
                        tempForm = (udcUIBase)Activator.CreateInstance(TempType);
                        tempForm.Form_delegate = my_del;
                        break;
                    }
                }
            }

            // Hana.PQC
            if (tempForm == null)
            {
				if (CurrentProc.ProcessName == "UserControlTestContainer")
				{
					assm = Assembly.Load("Hana.PQC");
					//assm = Assembly.LoadFile(@"D:\MESPLUS_HANA\SmartWebV42\SmartWebUI\Miracom.SmartWeb.MainUI\bin\Debug\Miracom.SmartWeb.CusUI.dll");
				}
				else
				{
					assm = Assembly.Load("Hana.PQC");
				}

                Types = assm.GetTypes();

                foreach (Type TempType in Types)
                {
                    if (TempType.Name.ToString() == FormName)
                    {
                        tempForm = (udcUIBase)Activator.CreateInstance(TempType);
                        tempForm.Form_delegate = my_del;
                        break;
                    }
                }
            }

            // Hana.PRD
            if (tempForm == null)
            {
				if (CurrentProc.ProcessName == "UserControlTestContainer")
				{
					assm = Assembly.Load("Hana.PRD");
					//assm = Assembly.LoadFile(@"D:\MESPLUS_HANA\SmartWebV42\SmartWebUI\Miracom.SmartWeb.MainUI\bin\Debug\Miracom.SmartWeb.CusUI.dll");
				}
				else
				{
					assm = Assembly.Load("Hana.PRD");
				}

                Types = assm.GetTypes();

                foreach (Type TempType in Types)
                {
                    if (TempType.Name.ToString() == FormName)
                    {
                        tempForm = (udcUIBase)Activator.CreateInstance(TempType);
                        tempForm.Form_delegate = my_del;
                        break;
                    }
                }
            }

            // Hana.RAS
            if (tempForm == null)
            {
				if (CurrentProc.ProcessName == "UserControlTestContainer")
				{
					assm = Assembly.Load("Hana.RAS");
					//assm = Assembly.LoadFile(@"D:\MESPLUS_HANA\SmartWebV42\SmartWebUI\Miracom.SmartWeb.MainUI\bin\Debug\Miracom.SmartWeb.CusUI.dll");
				}
				else
				{
					assm = Assembly.Load("Hana.RAS");
				}

                Types = assm.GetTypes();

                foreach (Type TempType in Types)
                {
                    if (TempType.Name.ToString() == FormName)
                    {
                        tempForm = (udcUIBase)Activator.CreateInstance(TempType);
                        tempForm.Form_delegate = my_del;
                        break;
                    }
                }
            }

            // Hana.TAT
            if (tempForm == null)
            {
				if (CurrentProc.ProcessName == "UserControlTestContainer")
				{
					assm = Assembly.Load("Hana.TAT");
					//assm = Assembly.LoadFile(@"D:\MESPLUS_HANA\SmartWebV42\SmartWebUI\Miracom.SmartWeb.MainUI\bin\Debug\Miracom.SmartWeb.CusUI.dll");
				}
				else
				{
					assm = Assembly.Load("Hana.TAT");
				}

                Types = assm.GetTypes();

                foreach (Type TempType in Types)
                {
                    if (TempType.Name.ToString() == FormName)
                    {
                        tempForm = (udcUIBase)Activator.CreateInstance(TempType);
                        tempForm.Form_delegate = my_del;
                        break;
                    }
                }
            }

            // Hana.YLD
            if (tempForm == null)
            {
				if (CurrentProc.ProcessName == "UserControlTestContainer")
				{
					assm = Assembly.Load("Hana.YLD");
					//assm = Assembly.LoadFile(@"D:\MESPLUS_HANA\SmartWebV42\SmartWebUI\Miracom.SmartWeb.MainUI\bin\Debug\Miracom.SmartWeb.CusUI.dll");
				}
				else
				{
					assm = Assembly.Load("Hana.YLD");
				}

                Types = assm.GetTypes();

                foreach (Type TempType in Types)
                {
                    if (TempType.Name.ToString() == FormName)
                    {
                        tempForm = (udcUIBase)Activator.CreateInstance(TempType);
                        tempForm.Form_delegate = my_del;
                        break;
                    }
                }
            }

            // Hana.MAT
            if (tempForm == null)
            {
				if (CurrentProc.ProcessName == "UserControlTestContainer")
				{
					assm = Assembly.Load("Hana.MAT");
					//assm = Assembly.LoadFile(@"D:\MESPLUS_HANA\SmartWebV42\SmartWebUI\Miracom.SmartWeb.MainUI\bin\Debug\Miracom.SmartWeb.CusUI.dll");
				}
				else
				{
					assm = Assembly.Load("Hana.MAT");
				}

                Types = assm.GetTypes();

                foreach (Type TempType in Types)
                {
                    if (TempType.Name.ToString() == FormName)
                    {
                        tempForm = (udcUIBase)Activator.CreateInstance(TempType);
                        tempForm.Form_delegate = my_del;
                        break;
                    }
                }
            }

            // Hana.TRN
            if (tempForm == null)
            {
				if (CurrentProc.ProcessName == "UserControlTestContainer")
				{
					assm = Assembly.Load("Hana.TRN");
					//assm = Assembly.LoadFile(@"D:\MESPLUS_HANA\SmartWebV42\SmartWebUI\Miracom.SmartWeb.MainUI\bin\Debug\Miracom.SmartWeb.CusUI.dll");
				}
				else
				{
					assm = Assembly.Load("Hana.TRN");
				}

                Types = assm.GetTypes();

                foreach (Type TempType in Types)
                {
                    if (TempType.Name.ToString() == FormName)
                    {
                        tempForm = (udcUIBase)Activator.CreateInstance(TempType);
                        tempForm.Form_delegate = my_del;
                        break;
                    }
                }
            }

            // 2012-12-17-������ : Hana.RFID
            if (tempForm == null)
            {
                if (CurrentProc.ProcessName == "UserControlTestContainer")
                {
                    assm = Assembly.Load("Hana.RFID");
                }
                else
                {
                    assm = Assembly.Load("Hana.RFID");
                }

                Types = assm.GetTypes();

                foreach (Type TempType in Types)
                {
                    if (TempType.Name.ToString() == FormName)
                    {
                        tempForm = (udcUIBase)Activator.CreateInstance(TempType);
                        tempForm.Form_delegate = my_del;
                        break;
                    }
                }
            }

            // 2013-09-13-������ : Hana.REG
            if (tempForm == null)
            {
                if (CurrentProc.ProcessName == "UserControlTestContainer")
                {
                    assm = Assembly.Load("Hana.REG");                    
                }
                else
                {
                    assm = Assembly.Load("Hana.REG");
                }

                Types = assm.GetTypes();

                foreach (Type TempType in Types)
                {
                    if (TempType.Name.ToString() == FormName)
                    {
                        tempForm = (udcUIBase)Activator.CreateInstance(TempType);
                        tempForm.Form_delegate = my_del;
                        break;
                    }
                }
            }

            return tempForm;
        }

        public override void LoadForm(Miracom.SmartWeb.FWX.FormStyle_EventArgs ChangeFormStyle)
        {
            string FormName;
            int i;
            bool bFindFlag;

            GlobalVariable.giIdleTime = 0;
            
            FormName = (string)ChangeFormStyle.PropertyValue;            

            // Set Cursor
            Cursor.Current = Cursors.WaitCursor;

            try
            {
                // Finding Previous Loaded Form
                bFindFlag = false;                

                for (i = 0; i <= tabLayout.TabPages.Count - 1; i++)
                {
                    if (tabLayout.TabPages[i].Tag.ToString() == FormName)   
                    {                                                       
                        tabLayout.TabPages[i].Tag = GlobalVariable.gaButtonLanguage;
                        bFindFlag = true;
                        break;
                    }                   
                }


                if (bFindFlag == false)
                {
                    Miracom.SmartWeb.FWX.udcUIBase newForm;

                    //=======================================================
                    // Select Form
                    //=======================================================
                    newForm = SelectForm(FormName);

                    if (newForm != null)
                    {
                        tabLayout.Visible = true;
                        tabLayout.ShowToolTips = true;
                        splitContainer.Panel2.BackgroundImage = null;
                        newForm.OnCloseLayoutForm = new Miracom.SmartWeb.FWX.CloseLayoutForm(CloseTabControl);
                        newForm.Tag = GlobalVariable.gsSelFuncGrp;//����Tag���� function group���
                        newForm.Text = FormName;
                        newForm.Dock = DockStyle.Fill;

                        // TAB�� �ڵ带 ǥ��
                        //tabLayout.TabPages.Add(FormName);
                        //tabLayout.TabPages[tabLayout.TabCount - 1].Text = FormName;
                        //tabLayout.TabPages[tabLayout.TabCount - 1].Tag = GlobalVariable.gsSelFuncGrp;//����Tag���� function group���
                        //tabLayout.TabPages[tabLayout.TabCount - 1].ToolTipText = FormName;
                        //tabLayout.TabPages[tabLayout.TabCount - 1].Controls.Add(newForm);
                        //tabLayout.SelectedIndex = tabLayout.TabCount - 1;                     

                        //TAB�� DESC�� ǥ��
                        // Modify by MgKim 2008.11.14 (������)
                        tabLayout.TabPages.Add(FormName);

                        // 2010-08-02-������ : �� �̸� �ѱ۷� �����ϰ� 4�ڸ��� ¥����.
                        tabLayout.TabPages[tabLayout.TabCount - 1].Text = TabLayout_Text_Change2(GlobalVariable.gsSelFunc);
                        //tabLayout.TabPages[tabLayout.TabCount - 1].Text = GlobalVariable.gsSelFunc;                        

                        tabLayout.TabPages[tabLayout.TabCount - 1].Tag = FormName;
                        tabLayout.TabPages[tabLayout.TabCount - 1].ToolTipText = tvwMenu.SelectedNode.Text;
                        tabLayout.TabPages[tabLayout.TabCount - 1].Controls.Add(newForm);
                        tabLayout.SelectedIndex = tabLayout.TabCount - 1;                        
                    }
                }
                else
                {
                    tabLayout.Visible = true;
                    tabLayout.SelectedIndex = i;
                    tabLayout.SelectedTab.Tag = GlobalVariable.gsSelFuncGrp;//����Tag���� function group���

                    // Modify by MgKim 2008.11.14 (������)                    
                    String TabLayout_Text = TabLayout_Text_Change(tvwMenu.SelectedNode.Text); // TagLayout ���� ����

                    // 2010-07-31-������ : ������.
                    // 2010-07-30-������ : function name�� ��ü�̸����� ������.
                    //GlobalVariable.gsSelFunc = TabLayout_Text;
                    GlobalVariable.gsSelFunc = tvwMenu.SelectedNode.Tag.ToString();

                    tabLayout.SelectedTab.Tag = FormName;
                }
            }
            finally
            {
                // Set Cursor
                Cursor.Current = Cursors.Default;
            }
        }

        //============================================================================
        // Function Name : CloseTabControl()
        //    - Close Tab Control
        //============================================================================
        public void CloseTabControl()    //Tabbed Layout�� ���Ͽ� �߰�
        {
            try
            {
                if (tabLayout.SelectedTab == null)
                { 
                }
                else
                {
                    int itabIndex;
                    itabIndex = tabLayout.SelectedIndex;
                    tabLayout.TabPages.RemoveAt(itabIndex);
                }
            }
            catch
            {
            }
            finally
            {
                if (tabLayout.TabPages.Count == 0)
                {
                    splitContainer.Panel2.BackgroundImage = (Image)BackImage;
                    tabLayout.Visible = false;
                }
                else
                {
                    splitContainer.Panel2.BackgroundImage = null;
                    tabLayout.Visible = true;
                }
            }
        }

        //============================================================================
        // Function Name : ParentRefresh()
        //    - ���� �ٲ� �� ���ΰ�ħ
        //============================================================================
        private void ParentRefresh()
        {
            System.Diagnostics.Process procResource = System.Diagnostics.Process.GetCurrentProcess();
            PostMessage(procResource.MainWindowHandle, 256, 116, 0);
        }



        /// <summary>
        /// ����ƮŬ���̾�Ʈ�� ���������� �������� ������ ���� ���� ��Ű�� �Լ�.
        /// </summary>
        private void ParentProcessClose()
        {
            System.Diagnostics.Process procResource = System.Diagnostics.Process.GetCurrentProcess();

            Application.DoEvents();
            PostMessage(procResource.MainWindowHandle, 0x0010/*WM_CLOSE*/, 0, 0);
            //System.AppDomain.Unload(System.AppDomain.CurrentDomain);
        }

        private void GetFuncList(string UserGroup)
        {
            int i;
            string CurFuncGrp;
            string CurFuncItem;
            string FuncGrp;
            string FuncItem;

            int iImageIndex1 = 0;
            int iImageIndex2 = 0;

            TreeNode rootNode = new TreeNode();
            TreeNode childNode = new TreeNode();

            CurFuncGrp = null;
            CurFuncItem = null;
            FuncGrp = null;
            FuncItem = null;

            DataTable dt = null;

            if (m_DS != null) m_DS.Dispose();
            m_DS = new DataSet();

            string QueryCond = null;

            QueryCond = FwxCmnFunction.PackCondition(QueryCond, "SYSTEM");
			if (USER_ID == "WEBADMIN")
            {
                QueryCond = FwxCmnFunction.PackCondition(QueryCond, "ADMIN_GROUP");
            }
            else
            {
                QueryCond = FwxCmnFunction.PackCondition(QueryCond, UserGroup);
            }


            dt = CmnFunction.oComm.GetFuncDataTable("GetFunctionList", QueryCond);

            for (i = 0; i < dt.Rows.Count; i++)
            {
                if ((string)dt.Rows[i]["FUNC_GRP_ID"] != CurFuncGrp)
                {
                    CurFuncGrp = (string)dt.Rows[i]["FUNC_GRP_ID"];
                    FuncGrp = CurFuncGrp;

                    CurFuncItem = (string)dt.Rows[i]["FUNC_DESC"];
                    FuncItem = (string)dt.Rows[i]["FUNC_NAME"];

                    if (CurFuncGrp == "Security")
                    {
                        iImageIndex1 = 16;
                        iImageIndex2 = 15;
                    }
                    else
                    {
                        iImageIndex1 = 1;
                        iImageIndex2 = 2;
                    }

                    rootNode = tvwMenu.Nodes.Add(CurFuncGrp, LanguageFunction.FindLanguage(CurFuncGrp, 2), 8);
                    rootNode.SelectedImageIndex = 8;
                    rootNode.Tag = CurFuncGrp;

                    //childNode = rootNode.Nodes.Add(CurFuncItem, FuncItem + " : " + LanguageFunction.FindLanguage(CurFuncItem, 2), iImageIndex1);
                    //childNode.SelectedImageIndex = iImageIndex2;
                    //childNode.Tag = FuncItem;
                }
                else
                {
                    //CurFuncItem = (string)dt.Rows[i]["FUNC_DESC"];
                    FuncItem = (string)dt.Rows[i]["FUNC_NAME"];

                    // 2019-11-22-������ : Language �߰�
                    switch (GlobalVariable.gcLanguage)
                    {
                        case '1':

                            CurFuncItem = (string)dt.Rows[i]["LANGUAGE_1"];
                            break;
                        case '2':

                            CurFuncItem = (string)dt.Rows[i]["LANGUAGE_2"];
                            break;
                        case '3':

                            CurFuncItem = (string)dt.Rows[i]["LANGUAGE_3"];
                            break;
                    }

                    childNode = rootNode.Nodes.Add(CurFuncItem, FuncItem + " : " + CurFuncItem, iImageIndex1);
                    //childNode = rootNode.Nodes.Add(CurFuncItem, FuncItem + " : " + LanguageFunction.FindLanguage(CurFuncItem, 2), iImageIndex1);
                    childNode.SelectedImageIndex = iImageIndex2;
                    childNode.Tag = FuncItem;
                }
            }

            dt.Dispose();

            if (USER_ID == "WEBADMIN")
            {
                chkFunc.Visible = true;
                chkFunc.Checked = false;
            }
        }

        private string GetUserId(string UserId)
        {
            DataTable dt = null;
            string QueryCond = null;
            string strUserName, strUserFactory, strUserId, strUserGroup, strCustomer, strOutside;
            
            try
            {
                if(UserId == "")
                {
                    return "";
                }
                
                QueryCond = FwxCmnFunction.PackCondition(QueryCond, "SYSTEM");
                QueryCond = FwxCmnFunction.PackCondition(QueryCond, UserId);
                dt = CmnFunction.oComm.SelectData("RWEBUSRDEF", "1", QueryCond);
                
                if (dt.Rows.Count > 0)
                {
                    strUserFactory = dt.Rows[0]["FACTORY"].ToString();
                    strUserId = dt.Rows[0]["USER_ID"].ToString();
                    strUserName = dt.Rows[0]["USER_DESC"].ToString();
                    strUserGroup = dt.Rows[0]["SEC_GRP_ID"].ToString();
                    strCustomer = dt.Rows[0]["USER_GRP_3"].ToString();
                    strOutside = dt.Rows[0]["USER_GRP_10"].ToString();
                    lblUserValue.Text = strUserId + "  ( " + strUserName + " )";

                    if (strUserFactory.Length > 0)
                    {
                        GlobalVariable.gsFactory = strUserFactory;
                    }

                    if (strUserId.Length > 0)
                    {
                        GlobalVariable.gsUserID = strUserId;
                    }

                    if (strUserGroup.Length > 0)
                    {
                        GlobalVariable.gsUserGroup = strUserGroup;
                    }
                    if(strCustomer.Length > 0)   //���� id�� ���
                    {
                        if(strCustomer != " ")
                        {
                            GlobalVariable.gsCustomer = strCustomer;
                            btnPassword.Visible = true;
                        }
                        else
                        {
                            GlobalVariable.gsCustomer = strCustomer;
                        }                        
                    }
                }
                else
                {
                    return "";
                }
                dt.Dispose();
                return strUserGroup;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        // 2009.07.21 ������
        //private bool CheckOutSide(string UserId)
        //{
        //    DataTable dt = null;
        //    string QueryCond = null;
        //    string strOutside;

        //    try
        //    {
        //        QueryCond = FwxCmnFunction.PackCondition(QueryCond, "SYSTEM");
        //        QueryCond = FwxCmnFunction.PackCondition(QueryCond, UserId);
        //        dt = CmnFunction.oComm.SelectData("RWEBUSRDEF", "1", QueryCond);

        //        if (dt.Rows.Count > 0)
        //        {
        //            strOutside = dt.Rows[0]["USER_GRP_10"].ToString();


        //            if (strOutside.Equals("Y"))   //�ܺ� ������ Ȯ��
        //            {
        //                return true;
        //            }
        //            else
        //            {
        //                return false;
        //            }

        //        }
        //        else
        //        {
        //            return false;
        //        }
        //        dt.Dispose();

        //    }
        //    catch (Exception ex)
        //    {
        //        throw new Exception(ex.Message);
        //    }


        //}

        #region  Main Menu Click Routine

        private void InitMenu(TreeView tvMenu)
        {
            //Init for MES Menu
            pnlMES.Visible = true;
            pnlFMB.Visible = false;
            tolMain.Visible = false;
            tvMenu.Controls.Clear();
            tvMenu.ImageList = imlSmallIconSmartWeb;

            //olbMain.Buttons.Add("MES Report", imlSmallIconMES.Images[1]);
            //olbMain.Buttons.Add("FMB", imlSmallIconMES.Images[3]);

            //olbMain.Height = 70;

            //olbMain.GradientButtonNormalDark = Color.FromArgb(126, 166, 225);
            //olbMain.GradientButtonNormalLight = Color.FromArgb(203, 225, 252);
            //olbMain.Invalidate();
        }

        # endregion

        private void MainForm_Load(object sender, EventArgs e)
        {
            try
            {
                string sUserGroup;
                string sServerVersion;
                //string myIp;
                //bool outSide;
                        
                // �ϳ� ���� IP
                //string HanaIP = "12.230";

                System.Diagnostics.Process CurrentProc = System.Diagnostics.Process.GetCurrentProcess();
                if (CurrentProc.ProcessName == "UserControlTestContainer")
                {
                    // 2010-09-27-������ : Main Form�� �ִ� ȭ���� ũ�⸦ �����Ѵ�.
					Size SizeValue = new Size();
					SizeValue.Width = 1330;
                    SizeValue.Height = 880;

					this.MinimumSize = SizeValue;
                    USER_ID = "WEBADMIN";
										
                    // 2009.07.17 ������
                    // ����� TEST �Ҷ� ���. ���� �ڵ� �־� �����
                    // ** �׽�Ʈ �� ���� �ּ��� ó�� �Ұ�.
                    //USER_ID = "IM00001";                    
                }

                sUserGroup = GetUserId(Convert.ToString(USER_ID));
                lblLoginTimeValue.Text = DateTime.Now.ToLocalTime().ToString();

                // 2011-04-01-������ : ���� ���� ǥ�� �ǵ��� ����. ������ ���� �����Ͽ� ���� �Ͽ��� ��.
                // 2016-06-30-������ : ���� ������ ����������..Ȯ���� ������ ���� �ٽ� ������.
                lblVersion.Text = "2021.01.06-00";
               
                // 2012-12-28-������ : Main Ui ���� ���� �ϱ� �÷�... Config ���Ͽ��� ���� ��..
                //lblVersion.Text = GlobalVariable.AppConfig.GetValue("//appSettings//add[@key='ApplicationVersion']");

                // 2012-08-30-������ : Server Info �߰�(���߹�������, ���������)
                lblServerInfo.Text = GlobalVariable.AppConfig.GetValue("//appSettings//add[@key='ApplicationName']");

                if (sUserGroup == "")
                {
                    MessageBox.Show("Invalid User ID: " + USER_ID + "\r\n.", "Smart Web Information");
                    ParentProcessClose();
                }
                else
                {
                    // 2009.07.21 ������
                    //IPHostEntry host = Dns.Resolve(Dns.GetHostName());
                    //myIp = host.AddressList[0].ToString();

                    //// �ܺ� IP �� ��� �ܺ����� ��� üũ
                    //if (myIp.Substring(0, 6) != HanaIP)
                    //{
                    //    outSide = CheckOutSide(Convert.ToString(USER_ID));

                    //    if (!outSide)
                    //    {
                    //        MessageBox.Show("������ " + USER_ID + " �� �㰡 �� �ܺ� ���� ����ڰ� �ƴմϴ�.");
                    //        ParentProcessClose();
                    //    }
                    //}

                    InitMenu(tvwMenu);
                    GetFuncList(sUserGroup);
                    //WinXpStyle.ControlLoad(sender, e, this);
                }

				// 2010-05-11-������ : �ش� ����� Form ID�� ǥ���Ѵ�.
                if (USER_ID == "WEBADMIN" || USER_ID == "ADMIN" || USER_ID == "1080701" || USER_ID == "1050410" || USER_ID == "1100438" || USER_ID == "1101135" || USER_ID == "1140505" || USER_ID == "1191214")
                {
                    chkFunc.Checked = false;
				}
                else
                {
                    chkFunc.Checked = true;
                    chkFunc.Visible = false;
                    txtFind.Visible = false;
                    lblIcon.Visible = false;
                }                

                sServerVersion = GlobalVariable.AppConfig.GetValue("//appSettings//add[@key='ApplicationVersion']");

                if (lblVersion.Text != sServerVersion)
                {
                    if (GlobalVariable.gsGlovalSite == "K1")
                    {
                        lblComment.Visible = true;
                        lblComment.Text = "���� �ֽŹ����� " + sServerVersion + " �Դϴ�.";
                        MessageBox.Show("���� �ֽŹ����� " + sServerVersion + " �Դϴ�." + "\r\n" + "���ͳ� â ��� ������ �� �ٽ� �����Ͻñ� �ٶ��ϴ�.", " �˸�");
                    }
                }
                else
                {
                    lblComment.Visible = false;
                }
            }
            catch (Exception ex)
            {
                //throw new Exception(ex.Message);
                if (GlobalVariable.gsUserGroup == "ADMIN_GROUP" || GlobalVariable.gsUserGroup == "HANA_ADMIN_GROUP")
                {
                    Clipboard.SetText(ex.Message + "\r\n Exit Program.");
                }
                MessageBox.Show(ex.Message + "\r\nExit Program.", "Smart Web error");
                ParentProcessClose();
            }
        }

        private void splitContainer1_DoubleClick(object sender, EventArgs e)
        {
            int moveSplitterSize;

            this.SuspendLayout();
            if (splitContainer.Panel1MinSize < this.splitContainer.SplitterDistance)
            {
                moveSplitterSize = splitContainer.Panel1MinSize;
                oldSplitterSize = this.splitContainer.SplitterDistance;
            }
            else
            {
                if (oldSplitterSize <= splitContainer.Panel1MinSize) // ������ �ʹ� ���� ��� �⺻������� ������.
                {
                    oldSplitterSize = 160;
                }
                moveSplitterSize = oldSplitterSize;
                oldSplitterSize = 0;
            }
            this.splitContainer.SplitterDistance = moveSplitterSize;
            this.ResumeLayout();
        }

        private void tmrLogOut_Tick(object sender, EventArgs e)
        {
            GlobalVariable.giIdleTime += 1; // 1 min
            if (GlobalVariable.giLogOutTime == 0)
            {
                return;
            }
            if (GlobalVariable.giIdleTime >= GlobalVariable.giLogOutTime)
            {
                GlobalVariable.giIdleTime = 0;
                Application.DoEvents();
                tmrLogOut.Enabled = false;
                m_TimeOut = "Y";
                OnLogOutClicked();
            }
        }

        private void btnOption_Click(object sender, EventArgs e)
        {
            char TempLanguage;

            TempLanguage = GlobalVariable.gcLanguage;
            frmOption frmOpt = new frmOption();
            frmOpt.ShowDialog();
            if (GlobalVariable.gcLanguage != TempLanguage)
            {
                ParentRefresh();
            }
        }

        private void btnPassword_Click(object sender, EventArgs e)
        {            
            frmPassword frmPwd = new frmPassword();
            frmPwd.ShowDialog();           
        }

        private void tvwMenu_NodeMouseClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            if (e.Node.Level != 0 && e.Node.Tag != null)
            {
                GlobalVariable.gsSelFuncGrp = e.Node.Parent.Text;//Function���ý� function group�� ���
                GlobalVariable.gsSelFunc = e.Node.Tag.ToString();  //tab�����Ҷ� function ID���� global������ �Է�                
                GlobalVariable.gsSelFuncName = e.Node.Name; //ȭ�� Title. by John Seo. 2008.10.06            
            }                    
            //else if (e.Node.Level == 0 && e.Node.Tag != null)
            //{
            //    GlobalVariable.gsSelFuncGrp = e.Node.Tag.ToString();//Function���ý� function group�� ���
            //    GlobalVariable.gsSelFunc = e.Node.Tag.ToString();  //tab�����Ҷ� function ID���� global������ �Է�
            //    GlobalVariable.gsSelFuncName = e.Node.Name; //ȭ�� Title. by John Seo. 2008.10.06
            //}                           
        }

        private void tvwMenu_AfterSelect(object sender, TreeViewEventArgs e)
        {
            if (e.Node.Level != 0 && e.Node.Tag != null)
            {
                GlobalVariable.gsSelFuncGrp = e.Node.Parent.Text;//Function���ý� function group�� ���

                // 2010-07-30-������ : ��ü �޴��� �����ϵ��� ����.. 4�ڸ� �̻� �ڸ��°� �������..
                GlobalVariable.gsSelFunc = e.Node.Tag.ToString();  //tab�����Ҷ� function ID���� global������ �Է�                

                // 2010-07-31-������ : Report Tab�� 4�ڸ� �ڸ��°Ŵ� �Ʒ� �Լ���.(15�ڸ��� ������)
                // 2010-07-30-������ : �������
                //String TabLayout_Text = TabLayout_Text_Change(tvwMenu.SelectedNode.Text); // TagLayout ���� ����
                //GlobalVariable.gsSelFunc = TabLayout_Text;                                // Modify by MgKim 2008.11.14    
                                          
                GlobalVariable.gsSelFuncName = e.Node.Name; //ȭ�� Title. by John Seo. 2008.10.06
                this.FormStyle.FormName = e.Node.Tag.ToString();
            }
            //else if (e.Node.Level == 0 && e.Node.Tag != null)
            //{
            //    GlobalVariable.gsSelFuncGrp = e.Node.Tag.ToString();//Function���ý� function group�� ���
            //    GlobalVariable.gsSelFunc = e.Node.Tag.ToString();  //tab�����Ҷ� function ID���� global������ �Է�
            //    GlobalVariable.gsSelFuncName = e.Node.Name; //ȭ�� Title. by John Seo. 2008.10.06
            //    this.FormStyle.FormName = e.Node.Tag.ToString();
            //}        
        }


        private void tvwMenu_NodeMouseDoubleClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            if (e.Node.Level != 0 && e.Node.Tag != null)
            {
                GlobalVariable.gsSelFuncGrp = e.Node.Parent.Text;//Function���ý� function group�� ���

                // 2010-07-30-������ : ��ü �޴��� �����ϵ��� ����.. 4�ڸ� �̻� �ڸ��°� �������..
                GlobalVariable.gsSelFunc = e.Node.Tag.ToString();  //tab�����Ҷ� function ID���� global������ �Է�                

                // 2010-07-31-������ : Report Tab�� 4�ڸ� �ڸ��°Ŵ� �Ʒ� �Լ���.(15�ڸ��� ������)
                // 2010-07-30-������ : �������
                //String TabLayout_Text = TabLayout_Text_Change(tvwMenu.SelectedNode.Text); // TagLayout ���� ����
                //GlobalVariable.gsSelFunc = TabLayout_Text;                                // Modify by MgKim 2008.11.14    

                GlobalVariable.gsSelFuncName = e.Node.Name; //ȭ�� Title. by John Seo. 2008.10.06
                this.FormStyle.FormName = e.Node.Tag.ToString();
            }
        }       


        #region ���ο� â�� ����ִ� ��������Ʈ�� �޼ҵ�  Add by MgKim 2008.11.17
        public void NewForm_From_Child_1(string target) 
        {               
            for ( int i=0 ; i < tvwMenu.Nodes.Count ; i++ )
            {
                for( int j=0 ; j < tvwMenu.Nodes[i].Nodes.Count ; j++ )
                {
                    if(tvwMenu.Nodes[i].Nodes[j].Tag.ToString() == target)
                    {
                        tvwMenu.SelectedNode = tvwMenu.Nodes[i].Nodes[j];
                    }
                }

            }          

            string aaa = tvwMenu.SelectedNode.GetType().ToString();
            GlobalVariable.gsSelFuncGrp = tvwMenu.SelectedNode.Parent.Text;
            GlobalVariable.gsSelFunc = TabLayout_Text_Change(tvwMenu.SelectedNode.Text);
            GlobalVariable.gsSelFuncName = tvwMenu.SelectedNode.Name.ToString();
            this.FormStyle.FormName = tvwMenu.SelectedNode.Tag.ToString();                        
        }
        #endregion

        private void tabLayout_Selecting(object sender, TabControlCancelEventArgs e)
        {
            //GlobalVariable.gsSelFuncGrp = tabLayout.SelectedTab.Tag.ToString();//tab�����Ҷ�Function group���� global������ �Է�
            //GlobalVariable.gsSelFunc = tabLayout.SelectedTab.Text;  //tab�����Ҷ� function ID���� global������ �Է�
            
            // 2010-08-02-������ : function ID (�� : PRD010101) ����ǵ��� ����..
            GlobalVariable.gsSelFunc = tabLayout.SelectedTab.Tag.ToString();  //tab�����Ҷ� function ID���� global������ �Է�            
        }

        private void chkFunc_CheckedChanged(object sender, EventArgs e)
        {
            int i;
            int j;

            for (i = 0; i < tvwMenu.Nodes.Count; i++)
            {
                for (j = 0; j < tvwMenu.Nodes[i].Nodes.Count; j++)
                {
                    if (chkFunc.Checked)
                    {
                        tvwMenu.Nodes[i].Nodes[j].Text = tvwMenu.Nodes[i].Nodes[j].Text.Replace(tvwMenu.Nodes[i].Nodes[j].Tag + " : ", "");
                    }
                    else
                    {
                        tvwMenu.Nodes[i].Nodes[j].Text = tvwMenu.Nodes[i].Nodes[j].Tag + " : " + tvwMenu.Nodes[i].Nodes[j].Text;
                    }
                }
            }
        }

        private void txtFind_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)13)
            {
                TreeNode tn;

                tn = CmnFunction.FindTreeNodeNextPartialByTag(tvwMenu.Nodes[0], tvwMenu.Nodes[0], txtFind.Text.Trim());

                if (tn != null)
                {
                    tvwMenu.SelectedNode = tn;

                    TreeNodeMouseClickEventArgs ev = new TreeNodeMouseClickEventArgs(tn, MouseButtons.Left, 2, 0, 0);

                    tvwMenu.SelectedNode.EnsureVisible();
                }
            }
        }

        private void btnLogout_Click(object sender, EventArgs e)
        {
            OnLogOutClicked();
        }

        private void olbMain_Click(object sender, Miracom.SmartWeb.FWX.MCOutlockBar.MCOutlookBar.ButtonClickEventArgs e)
        {
            int i = olbMain.Buttons.IndexOf(e.SelectedButton);
            switch (i)
            {
                case 0:
                    pnlMES.Visible = true;
                    pnlFMB.Visible = false;
                    tolMain.Visible = false;
                    break;
                case 1:
                    pnlMES.Visible = false;
                    pnlFMB.Visible = true;
                    tolMain.Visible = true;
                    tabLayout.TabPages.Clear();
                    InitDesignList();
                    break;
                default:
                    MessageBox.Show("Default");
                    break;
            }
        }

        // InitDesignList()
        //       - Initialize Design List
        // Return Value
        //       - Boolean : True or False
        // Arguments
        //       -
        //
        public bool InitDesignList()
        {
            try
            {
                CmnInitFunction.InitTreeView(trvDesignList);
                trvDesignList.ImageList = imlSmallIconMES;
                if (ViewFactoryList() == false)
                {
                    return false;
                }
                //if (ViewFMBGroupList() == false)
                //{
                //    return false;
                //}

                return true;

            }
            catch (Exception ex)
            {
                CmnFunction.ShowMsgBox("frmMDIMain.InitDesignList()" + "\r\n" + ex.Message);
                return false;
            }

        }

        // AddNewForm()
        //       - Add Form
        // Return Value
        //       -
        // Arguments
        //       - ByVal sFormName As String : Form Name
        //       - ByVal sTag As String : Form Tag
        //
        private void AddNewForm(string sFormName, string sTag)
        {
            try
            {
                FMBDesign newForm = new FMBDesign();
                newForm.Name = sFormName;
                newForm.Tag = sTag;
                newForm.Text = sFormName;
                //newForm.Parent = this;
                newForm.Show();
            }
            catch (Exception ex)
            {
                CmnFunction.ShowMsgBox("frmMDIMain.AddNewForm()" + "\r\n" + ex.Message);
            }
        }

        private void trvDesignList_Click(object sender, EventArgs e)
        {
            try
            {
                bool bFindForm = false;

                if (trvDesignList.SelectedNode == null)
                {
                    return;
                }
                //Form frmChild;
                string sSelectedForm = string.Empty;

                switch (((clsDesignListTag)trvDesignList.SelectedNode.Tag).Category)
                {
                    case modGlobalConstant.FMB_CATEGORY_FACTORY:

                        if (((clsDesignListTag)trvDesignList.SelectedNode.Tag).LoadFlag == false)
                        {
                            if (ViewLayoutList(((clsDesignListTag)trvDesignList.SelectedNode.Tag).Factory) == false)
                            {
                                break;
                            }
                            //if (ViewResourceList(((clsDesignListTag)trvDesignList.SelectedNode.Tag).Factory, "") == false)
                            //{
                            //    break;
                            //}
                            ((clsDesignListTag)trvDesignList.SelectedNode.Tag).LoadFlag = true;
                            trvDesignList.SelectedNode.Expand();
                        }
                        break;
                    case modGlobalConstant.FMB_CATEGORY_LAYOUT:

                        sSelectedForm = ((clsDesignListTag)trvDesignList.SelectedNode.Tag).Factory + ":" + ((clsDesignListTag)trvDesignList.SelectedNode.Tag).Layout;
                        //Form frmChild;
                        //foreach (Form tempLoopVar_frmChild in this.MdiChildren)
                        //{
                        //    frmChild = tempLoopVar_frmChild;
                        //    if (sSelectedForm == frmChild.Name)
                        //    {
                        //        frmChild.Activate();
                        //        bFindForm = true;
                        //        break;
                        //    }
                        //}

                        if (bFindForm == false)
                        {
                            string sTag = ((clsDesignListTag)trvDesignList.SelectedNode.Tag).Category;
                            AddNewForm(sSelectedForm, sTag);
                        }
                        if (((clsDesignListTag)trvDesignList.SelectedNode.Tag).LoadFlag == false)
                        {
                            ViewResourceList(((clsDesignListTag)trvDesignList.SelectedNode.Tag).Factory, ((clsDesignListTag)trvDesignList.SelectedNode.Tag).Layout);
                            ((clsDesignListTag)trvDesignList.SelectedNode.Tag).LoadFlag = true;
                            trvDesignList.SelectedNode.Expand();
                        }
                        break;
                    case modGlobalConstant.FMB_CATEGORY_GROUP:

                        sSelectedForm = ((clsDesignListTag)trvDesignList.SelectedNode.Tag).Layout;
                        //Form frmChild;
                        //foreach (Form tempLoopVar_frmChild in this.MdiChildren)
                        //{
                        //    frmChild = tempLoopVar_frmChild;
                        //    if (sSelectedForm == frmChild.Name)
                        //    {
                        //        frmChild.Activate();
                        //        bFindForm = true;
                        //        break;
                        //    }
                        //}

                        if (bFindForm == false)
                        {
                            string strTag = ((clsDesignListTag)trvDesignList.SelectedNode.Tag).Category;
                            AddNewForm(sSelectedForm, strTag);
                        }
                        break;
                    case modGlobalConstant.FMB_CATEGORY_RESOURCE:

                        //System.Windows.Forms.Form frmChild;
                        //foreach (System.Windows.Forms.Form tempLoopVar_frmChild in this.MdiChildren)
                        //{
                        //    frmChild = tempLoopVar_frmChild;
                        //    if (frmChild is frmFMBDesign)
                        //    {
                        //        if (((frmFMBDesign)frmChild).Name == ((clsDesignListTag)trvDesignList.SelectedNode.Tag).Factory + ":" + ((clsDesignListTag)trvDesignList.SelectedNode.Tag).Layout)
                        //        {
                        //            ((frmFMBDesign)frmChild).Activate();
                        //            modCommonFunction.GetControl(((frmFMBDesign)frmChild).pnlFMBDesign.Controls, ((clsDesignListTag)trvDesignList.SelectedNode.Tag).Key, FMBUI.Enums.eToolType.Resource).Select();
                        //            break;
                        //        }
                        //    }
                        //}
                        break;
                    case modGlobalConstant.FMB_CATEGORY_TAG:

                        break;
                    case modGlobalConstant.FMB_CATEGORY_FILE:

                        ReadLayOut(((clsDesignListTag)trvDesignList.SelectedNode.Tag).Layout);
                        break;
                    default:

                        break;

                }

            }
            catch (Exception ex)
            {
                CmnFunction.ShowMsgBox("frmMDIMain.trvDesignList_Click()" + "\r\n" + ex.Message);
            }
			
        }

        public bool ViewFMBGroupList()
        {

            try
            {
                FMB_View_FMB_Group_List_In_Tag View_FMB_Group_List_In = new FMB_View_FMB_Group_List_In_Tag();
                FMB_View_FMB_Group_List_Out_Tag View_FMB_Group_List_Out = new FMB_View_FMB_Group_List_Out_Tag();

                int i;
                TreeNode nodeGroup;
                string sLayout = "";

                View_FMB_Group_List_In.h_proc_step = '2';
                View_FMB_Group_List_In.h_passport = GlobalVariable.gsPassport;
                View_FMB_Group_List_In.h_language = GlobalVariable.gcLanguage;
                View_FMB_Group_List_In.h_user_id = GlobalVariable.gsUserID;
                View_FMB_Group_List_In.h_password = GlobalVariable.gsPassword;
                View_FMB_Group_List_In.h_factory = GlobalVariable.gsFactory;
                View_FMB_Group_List_In.next_user = GlobalVariable.gsUserID;
                View_FMB_Group_List_In.next_group = "";

                trvDesignList.BeginUpdate();

                for (i = trvDesignList.Nodes.Count - 1; i >= 0; i += -1)
                {
                    if (((clsDesignListTag)trvDesignList.Nodes[i].Tag).Category == modGlobalConstant.FMB_CATEGORY_GROUP)
                    {
                        trvDesignList.Nodes[i].Remove();
                    }
                }

                do
                {
                    if (FMBSender.FMB_View_FMB_Group_List(View_FMB_Group_List_In, ref View_FMB_Group_List_Out) == false)
                    {
                        //MPCF.ShowMsgBox(Miracom.H101Core.h101stub.StatusMessage);
                        return false;
                    }

                    if ((View_FMB_Group_List_Out.h_status_value != modGlobalConstant.MP_SUCCESS_STATUS))
                    {
                        CmnFunction.ShowMsgBox(CmnFunction.MakeErrorMsg(View_FMB_Group_List_Out.h_msg_code, View_FMB_Group_List_Out.h_msg, View_FMB_Group_List_Out.h_db_err_msg, View_FMB_Group_List_Out.h_field_msg));
                        return false;
                    }

                    for (i = 0; i <= View_FMB_Group_List_Out.count - 1; i++)
                    {
                        sLayout = CmnFunction.RTrim(View_FMB_Group_List_Out.udr_group_list[i].group);
                        nodeGroup = trvDesignList.Nodes.Add(sLayout);
                        clsDesignListTag nodeTag = new clsDesignListTag();
                        nodeTag.SetTagData(sLayout, modGlobalConstant.FMB_CATEGORY_GROUP, "", sLayout, false);
                        nodeGroup.Tag = nodeTag;
                        nodeGroup.ImageIndex = CmnFunction.ToInt(SMALLICON_INDEX.IDX_BAY);
                        nodeGroup.SelectedImageIndex = CmnFunction.ToInt(SMALLICON_INDEX.IDX_BAY);
                    }

                    View_FMB_Group_List_In.next_group = View_FMB_Group_List_Out.next_group;
                }
                while (!((CmnFunction.Trim(View_FMB_Group_List_Out.next_group) == "")));

                trvDesignList.EndUpdate();
                return true;
            }

            catch (Exception ex)
            {
                CmnFunction.ShowMsgBox("frmMDIMain.ViewUDRGroupList()" + "\r\n" + ex.Message);
                return false;
            }

        }

        // ViewFactoryList()
        //       - View Factory List
        // Return Value
        //       - Boolean : True or False
        // Arguments
        //		-
        //
        public bool ViewFactoryList()
        {

            try
            {
                WIP_View_Factory_List_In_Tag View_Factory_List_In = new WIP_View_Factory_List_In_Tag();
                WIP_View_Factory_List_Out_Tag View_Factory_List_Out = new WIP_View_Factory_List_Out_Tag();
                int i;
                TreeNode nodeFactory;
                string sFactory = "";

                View_Factory_List_In.h_proc_step = '1';
                View_Factory_List_In.h_passport = GlobalVariable.gsPassport;
                View_Factory_List_In.h_language = GlobalVariable.gcLanguage;
                View_Factory_List_In.h_user_id = GlobalVariable.gsUserID;
                View_Factory_List_In.h_password = GlobalVariable.gsPassword;
                View_Factory_List_In.h_factory = GlobalVariable.gsFactory;

                View_Factory_List_In.next_factory = "";

                trvDesignList.BeginUpdate();

                trvDesignList.Nodes.Clear();
                modGlobalVariable.gbAllFactory = true; //�ӽ÷� ������
                if (modGlobalVariable.gbAllFactory == false)
                {
                    nodeFactory = trvDesignList.Nodes.Add(GlobalVariable.gsFactory);
                    clsDesignListTag nodeTag = new clsDesignListTag();
                    nodeTag.SetTagData(GlobalVariable.gsFactory, modGlobalConstant.FMB_CATEGORY_FACTORY, GlobalVariable.gsFactory, "", false);
                    nodeFactory.Tag = nodeTag;
                    nodeFactory.ImageIndex = 0;
                    nodeFactory.SelectedImageIndex = 0;
                }
                else
                {
                    do
                    {
                        if (WIPSender.WIP_View_Factory_List(View_Factory_List_In, ref View_Factory_List_Out) == false)
                        {
                            //MPCF.ShowMsgBox(Miracom.H101Core.h101stub.StatusMessage);
                            return false;
                        }

                        if (View_Factory_List_Out.h_status_value != modGlobalConstant.MP_SUCCESS_STATUS)
                        {
                            CmnFunction.ShowMsgBox(CmnFunction.MakeErrorMsg(View_Factory_List_Out.h_msg_code, View_Factory_List_Out.h_msg, View_Factory_List_Out.h_db_err_msg, View_Factory_List_Out.h_field_msg));
                            return false;
                        }

                        for (i = 0; i <= View_Factory_List_Out.count - 1; i++)
                        {
                            sFactory = CmnFunction.RTrim(View_Factory_List_Out.factory_list[i].factory);
                            nodeFactory = trvDesignList.Nodes.Add(sFactory);
                            clsDesignListTag nodeTag = new clsDesignListTag();
                            nodeTag.SetTagData(sFactory, modGlobalConstant.FMB_CATEGORY_FACTORY, sFactory, "", false);
                            nodeFactory.Tag = nodeTag;
                            nodeFactory.ImageIndex = CmnFunction.ToInt(SMALLICON_INDEX.IDX_FACTORY);
                            nodeFactory.SelectedImageIndex = CmnFunction.ToInt(SMALLICON_INDEX.IDX_FACTORY);
                        }

                        View_Factory_List_In.next_factory = View_Factory_List_Out.next_factory;

                    } while (CmnFunction.Trim(View_Factory_List_In.next_factory) != "");
                }

                trvDesignList.EndUpdate();

                return true;

            }
            catch (Exception ex)
            {
                CmnFunction.ShowMsgBox("frmMDIMain.ViewFactoryList()" + "\r\n" + ex.Message);
                return false;
            }

        }

        // ViewLayoutList()
        //       - View Layout List
        // Return Value
        //       - Boolean : True or False
        // Arguments
        //		- ByVal sFactory As String
        //
        public bool ViewLayoutList(string sFactory)
        {

            try
            {
                FMB_View_LayOut_List_In_Tag FMB_View_LayOut_List_In = new FMB_View_LayOut_List_In_Tag();
                FMB_View_LayOut_List_Out_Tag FMB_View_LayOut_List_Out = new FMB_View_LayOut_List_Out_Tag();
                int i;
                TreeNode nodeFactory;
                TreeNode nodeLayout;
                string sLayout = "";

                nodeFactory = modCommonFunction.FindTreeNode(trvDesignList, null, sFactory, false);

                FMB_View_LayOut_List_In.h_proc_step = '1';
                FMB_View_LayOut_List_In.h_passport = GlobalVariable.gsPassport;
                FMB_View_LayOut_List_In.h_language = GlobalVariable.gcLanguage;
                FMB_View_LayOut_List_In.h_user_id = GlobalVariable.gsUserID;
                FMB_View_LayOut_List_In.h_user_id = "ADMIN";
                FMB_View_LayOut_List_In.h_password = GlobalVariable.gsPassword;
                FMB_View_LayOut_List_In.h_factory = sFactory;

                FMB_View_LayOut_List_In.next_layout_id = "";

                trvDesignList.BeginUpdate();

                nodeFactory.Nodes.Clear();

                do
                {
                    if (FMBSender.FMB_View_LayOut_List(FMB_View_LayOut_List_In, ref FMB_View_LayOut_List_Out) == false)
                    {
                        //MPCF.ShowMsgBox(Miracom.H101Core.h101stub.StatusMessage);
                        return false;
                    }

                    if (FMB_View_LayOut_List_Out.h_status_value != modGlobalConstant.MP_SUCCESS_STATUS)
                    {
                        //MPCF.ShowMsgBox(MPCF.MakeErrorMsg(FMB_View_LayOut_List_Out.h_msg_code, FMB_View_LayOut_List_Out.h_msg, FMB_View_LayOut_List_Out.h_db_err_msg, FMB_View_LayOut_List_Out.h_field_msg));
                        return false;
                    }

                    for (i = 0; i <= FMB_View_LayOut_List_Out.count - 1; i++)
                    {
                        sLayout = CmnFunction.RTrim(FMB_View_LayOut_List_Out.layout_list[i].layout_id);
                        nodeLayout = nodeFactory.Nodes.Add(sLayout);
                        clsDesignListTag nodeTag = new clsDesignListTag();
                        nodeTag.SetTagData(sLayout, modGlobalConstant.FMB_CATEGORY_LAYOUT, sFactory, sLayout, false);
                        nodeLayout.Tag = nodeTag;
                        nodeLayout.ImageIndex = Convert.ToInt32(SMALLICON_INDEX.IDX_AREA);
                        nodeLayout.SelectedImageIndex = Convert.ToInt32(SMALLICON_INDEX.IDX_AREA);
                    }

                    FMB_View_LayOut_List_In.next_layout_id = FMB_View_LayOut_List_Out.next_layout_id;
                } while (CmnFunction.Trim(FMB_View_LayOut_List_In.next_layout_id) != "");

                trvDesignList.EndUpdate();

                return true;

            }
            catch (Exception ex)
            {
                CmnFunction.ShowMsgBox("frmMDIMain.ViewLayoutList()" + "\r\n" + ex.Message);
                return false;
            }

        }

        // ViewResourceList()
        //       - View Resource List
        // Return Value
        //       - Boolean : True or False
        // Arguments
        //		- ByVal sFactory As String
        //
        public bool ViewResourceList(string sFactory, string sLayout)
        {
            try
            {
                FMB_View_Resource_List_In_Tag FMB_View_Resource_List_In = new FMB_View_Resource_List_In_Tag();
                FMB_View_Resource_List_Out_Tag FMB_View_Resource_List_Out = new FMB_View_Resource_List_Out_Tag();
                int i;
                TreeNode nodeFactory;
                TreeNode nodeLayout = null;
                TreeNode nodeResource;
                string sResource = "";

                nodeFactory = modCommonFunction.FindTreeNode(trvDesignList, null, sFactory, false);

                if (sLayout.TrimEnd() == "")
                {
                    FMB_View_Resource_List_In.h_proc_step = '4';
                }
                else
                {
                    FMB_View_Resource_List_In.h_proc_step = '5';
                    FMB_View_Resource_List_In.layout_id = sLayout.TrimEnd();
                    nodeLayout = modCommonFunction.FindTreeNode(trvDesignList, nodeFactory, sLayout, false);
                    if (nodeLayout != null)
                    {
                        nodeLayout.Nodes.Clear();
                    }
                }
                FMB_View_Resource_List_In.h_passport = GlobalVariable.gsPassport;
                FMB_View_Resource_List_In.h_language = GlobalVariable.gcLanguage;
                FMB_View_Resource_List_In.h_user_id = GlobalVariable.gsUserID;
                FMB_View_Resource_List_In.h_password = GlobalVariable.gsPassword;
                FMB_View_Resource_List_In.h_factory = sFactory;

                FMB_View_Resource_List_In.res_type = ' ';
                FMB_View_Resource_List_In.next_seq = 0;
                FMB_View_Resource_List_In.include_del_res = ' ';
                FMB_View_Resource_List_In.include_proc_lot_info = ' ';

                FMB_View_Resource_List_In.next_res_id = " ";

                trvDesignList.BeginUpdate();

                do
                {
                    if (FMBSender.FMB_View_Resource_List(FMB_View_Resource_List_In, ref FMB_View_Resource_List_Out) == false)
                    {
                        //MPCF.ShowMsgBox(Miracom.H101Core.h101stub.StatusMessage);
                        return false;
                    }

                    if (FMB_View_Resource_List_Out.h_status_value != modGlobalConstant.MP_SUCCESS_STATUS)
                    {
                        //MPCF.ShowMsgBox(MPCF.MakeErrorMsg(FMB_View_Resource_List_Out.h_msg_code, FMB_View_Resource_List_Out.h_msg, FMB_View_Resource_List_Out.h_db_err_msg, FMB_View_Resource_List_Out.h_field_msg));
                        return false;
                    }

                    for (i = 0; i <= FMB_View_Resource_List_Out.count - 1; i++)
                    {
                        sResource = CmnFunction.RTrim(FMB_View_Resource_List_Out.res_list[i].res_id);
                        if (sLayout.TrimEnd() == "")
                        {
                            sLayout = "";
                            nodeResource = nodeFactory.Nodes.Add(sResource);
                            clsDesignListTag nodeTag = new clsDesignListTag();
                            nodeTag.SetTagData(sResource, modGlobalConstant.FMB_CATEGORY_RESOURCE, sFactory, sLayout, false);
                            nodeResource.Tag = nodeTag;
                            nodeResource.ImageIndex = Convert.ToInt32(SMALLICON_INDEX.IDX_EQUIP_DOWN);
                            nodeResource.SelectedImageIndex = Convert.ToInt32(SMALLICON_INDEX.IDX_EQUIP_DOWN);
                        }
                        else
                        {
                            nodeResource = nodeLayout.Nodes.Add(sResource);
                            clsDesignListTag nodeTag = new clsDesignListTag();
                            nodeTag.SetTagData(sResource, modGlobalConstant.FMB_CATEGORY_RESOURCE, sFactory, sLayout, true);
                            nodeResource.Tag = nodeTag;
                            nodeResource.ImageIndex = Convert.ToInt32(SMALLICON_INDEX.IDX_EQUIP);
                            nodeResource.SelectedImageIndex = Convert.ToInt32(SMALLICON_INDEX.IDX_EQUIP);
                        }
                    }

                    FMB_View_Resource_List_In.next_res_id = FMB_View_Resource_List_Out.next_res_id;
                } while (CmnFunction.Trim(FMB_View_Resource_List_Out.next_res_id) != "");

                if (sLayout.TrimEnd() != "")
                {
                    nodeLayout.Expand();
                }

                trvDesignList.EndUpdate();

                return true;

            }
            catch (Exception ex)
            {
                CmnFunction.ShowMsgBox("frmMDIMain.ViewResourceList()" + "\r\n" + ex.Message);
                return false;
            }

        }

        private bool ReadLayOut(string sFile)
        {

            string sfilename;
            string sCount;
            int iCount;
            int i;
            //Form frmChild;
            bool bFindForm;
            try
            {
                sfilename = Application.StartupPath + "\\" + sFile + ".fmb";

                StreamReader sr = File.OpenText(sfilename);
                sCount = sr.ReadLine();
                iCount = CmnFunction.ToInt(sCount);
                string[] sName = new string[iCount + 1];
                string[] sLayout = new string[iCount + 1];
                string[] sCategory = new string[iCount + 1];
                string[] sFactory = new string[iCount + 1];
                int[] iLocX = new int[iCount + 1];
                int[] iLocY = new int[iCount + 1];
                int[] iWidth = new int[iCount + 1];
                int[] iHeight = new int[iCount + 1];

                for (i = 0; i <= iCount - 1; i++)
                {
                    bFindForm = false;
                    sLayout[i] = sr.ReadLine();
                    sName[i] = modCommonFunction.GetStringBySeperator(sLayout[i], "/", 1);
                    iLocX[i] = CmnFunction.ToInt(modCommonFunction.GetStringBySeperator(sLayout[i], "/", 2));
                    iLocY[i] = CmnFunction.ToInt(modCommonFunction.GetStringBySeperator(sLayout[i], "/", 3));
                    iWidth[i] = CmnFunction.ToInt(modCommonFunction.GetStringBySeperator(sLayout[i], "/", 4));
                    iHeight[i] = CmnFunction.ToInt(modCommonFunction.GetStringBySeperator(sLayout[i], "/", 5));
                    sCategory[i] = modCommonFunction.GetStringBySeperator(sLayout[i], "/", 6);
                    sFactory[i] = modCommonFunction.GetStringBySeperator(sLayout[i], "/", 7);
                    //foreach (Form tempLoopVar_frmChild in this.MdiChildren)
                    //{
                    //    frmChild = tempLoopVar_frmChild;
                    //    if (sName[i] == frmChild.Name)
                    //    {
                    //        frmChild.Activate();
                    //        bFindForm = true;
                    //        break;
                    //    }
                    //}

                    if (bFindForm == false)
                    {
                        if (modGlobalVariable.gbAllFactory == false)
                        {
                            if (GlobalVariable.gsFactory == sFactory[i])
                            {
                                AddNewForm(sName[i], sCategory[i]);
                            }
                        }
                        else
                        {
                            if (sCategory[i] == modGlobalConstant.FMB_CATEGORY_GROUP)
                            {
                                if (GlobalVariable.gsFactory == sFactory[i])
                                {
                                    AddNewForm(sName[i], sCategory[i]);
                                }
                            }
                            else
                            {
                                AddNewForm(sName[i], sCategory[i]);
                            }
                        }
                    }
                }

                //foreach (Form tempLoopVar_frmChild in this.MdiChildren)
                //{
                //    frmChild = tempLoopVar_frmChild;
                //    for (i = 0; i <= iCount - 1; i++)
                //    {
                //        if (sName[i] == frmChild.Name)
                //        {
                //            frmChild.StartPosition = FormStartPosition.Manual;
                //            frmChild.Location = new System.Drawing.Point(iLocX[i], iLocY[i]);
                //            frmChild.Size = new Size(iWidth[i], iHeight[i]);
                //            break;
                //        }
                //    }
                //}

                sr.Close();


            }
            catch (Exception ex)
            {
                CmnFunction.ShowMsgBox("frmMDIMain.ReadLayOut()" + "\r\n" + ex.Message);
                return false;
            }

            return true;

        }




        private String TabLayout_Text_Change(string selectedNode)  // TagLayout ������ 4�ڸ� �ʰ��Ұ�� Trim
        {
            int count = 0;                                         // Added by MgKim 2008.11.14
            if (chkFunc.Checked)
            {
                count = selectedNode.Length;

                // 2010-07-31-������ : 4�ڸ����� 15�ڸ��� ������.
                // 2010-07-31-������ : DataBase�� RWEBFUNLOG Table�� Func_Name File�� ���̰� 12byte�� ������(6��)
                if (count > 10)
                //if (count > 4)
                {
                    // 2010-07-31-������ : 4�ڸ����� 15�ڸ��� ������.
                    // 2010-07-31-������ : DataBase�� RWEBFUNLOG Table�� Func_Name File�� ���̰� 12byte�� ������(6��)
                    selectedNode = selectedNode.Substring(0, 10).ToString();
                    //selectedNode = selectedNode.Substring(0, 4).ToString();
                    selectedNode = selectedNode + "..";
                    return selectedNode;
                }
                else
                {
                    return selectedNode;
                }
            }
            else
            {
                string[] selectedValue = selectedNode.Split(new char[] { ':' });
                selectedNode = selectedValue[1].ToString();
                selectedNode = selectedNode.Trim();
                count = selectedNode.Length;
                // 2010-07-31-������ : 4�ڸ����� 15�ڸ��� ������.
                // 2010-07-31-������ : DataBase�� RWEBFUNLOG Table�� Func_Name File�� ���̰� 12byte�� ������(6��)
                if (count > 10)
                //if (count > 15)
                {
                    // 2010-07-31-������ : 4�ڸ����� 15�ڸ��� ������.
                    // 2010-07-31-������ : DataBase�� RWEBFUNLOG Table�� Func_Name File�� ���̰� 12byte�� ������(6��)
                    selectedNode = selectedNode.Substring(0, 10).ToString();
                    //selectedNode = selectedNode.Substring(0, 15).ToString();
                    selectedNode = selectedNode + "..";
                    return selectedNode;
                }
                else
                {
                    return selectedNode;
                }
            }
        }

        /// <summary>
        /// 2010-08-02-������ : TabLayout �̸��� �ѱ۷� �����ϰ� 4�ڸ��� ¥���� ���� �߰���.
        /// </summary>
        /// <param name="funName"></param>
        /// <returns></returns>
        private String TabLayout_Text_Change2(string funName)
        {
            DataTable dt = null;
            String rtName = String.Empty;
            String sLanguage = String.Empty;

            // 2019-11-22-������ : Language ��� �߰�
            switch (GlobalVariable.gcLanguage)
            {
                case '1':

                    sLanguage = "LANGUAGE_1";
                    break;
                case '2':

                    sLanguage = "LANGUAGE_2";
                    break;
                case '3':

                    sLanguage = "LANGUAGE_3";
                    break;
            }

            string strQuery = "SELECT " + sLanguage + " FROM RWEBFUNDEF WHERE FUNC_NAME = '" + funName + "'";
            //string strQuery = "SELECT FUNC_DESC FROM RWEBFUNDEF WHERE FUNC_NAME = '" + funName + "'" ;
            dt = CmnFunction.oComm.GetFuncDataTable("DYNAMIC", strQuery);

            if (dt.Rows[0][0].ToString().Length > 10)
            {
                rtName = dt.Rows[0][0].ToString().Substring(0, 10) + "...";
            }
            else
            {
                rtName = dt.Rows[0][0].ToString();
            }

            return rtName;
        }

        /// <summary>
        /// 2017-11-14-������ : �ֽŹ��� Ȯ���Ͽ� �˸� â ����.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tmrVersion_Tick(object sender, EventArgs e)
        {
            string sServerVersion;

            sServerVersion = GlobalVariable.AppConfig.GetValue("//appSettings//add[@key='ApplicationVersion']");

            if (lblVersion.Text != sServerVersion)
            {
                if (GlobalVariable.gsGlovalSite == "K1")
                {
                    lblComment.Visible = true;
                    lblComment.Text = "���� �ֽŹ����� " + sServerVersion + " �Դϴ�.";
                    MessageBox.Show("���� �ֽŹ����� " + sServerVersion + " �Դϴ�." + "\r\n" + "���ͳ� â ��� ������ �� �ٽ� �����Ͻñ� �ٶ��ϴ�.", " �˸�");
                }
            }
            else
            {
                lblComment.Visible = false;
            }
        }

    }
}
