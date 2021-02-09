using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;

using Miracom.UI;
using Miracom.SmartWeb.FWX;

namespace Miracom.SmartWeb.UI.Controls
{
    public partial class udcCondition : UserControl
    {
        public udcCondition()
        {
            InitializeComponent();
        }

        //Condition Type
        public enum eConditionype
        {
            FACTORY = 0,
            MATERIAL = 1,
            FLOW = 2,
            OPER = 3,
            RESOURCE = 4,
            EVENT = 5,
            TABLE = 6,
            AREA = 7, 
            RAS_TBL = 8,
            WIP_TBL = 9,
            HQ_TABLE = 10,

            // 2010-04-19-임종우 : 원부자재 조회용 MATERIAL_TYPE 추가
            MATERIAL_TYPE = 11,
            // 2015-02-25 : HMKA1, HMKB1 FACTORY 조회용 FACTORY_ASSEMBLY 추가
            FACTORY_ASSEMBLY = 12,
            TEXT = 99,
            CUSTOM = 100
        }

        public enum SMALLICON_INDEX
        {
            IDX_FACTORY = 0,
            IDX_SHIP_FACTORY = 2,
            IDX_TRANSACTION = 10,
            IDX_SETUP = 11,
            IDX_EVENT = 12,
            IDX_CODE_TABLE = 13,
            IDX_CODE_DATA = 14,
            IDX_MATERIAL = 15,
            IDX_FLOW = 16,
            IDX_REWORK_FLOW = 17,
            IDX_OPER = 18,
            IDX_MFO = 19,
            IDX_SEC_GROUP = 20,
            IDX_USER = 21,
            IDX_RESOURCE = 22,
            IDX_RESOURCE_PROC = 23,
            IDX_RESOURCE_DOWN = 24,
            IDX_RESOURCE_GROUP = 25,
            IDX_SUB_EQUIP = 26,
            IDX_STATUS = 27,
            IDX_CHARACTER = 28,
            IDX_COL_SET = 29,
            IDX_COL_SET_DELETE = 30,
            IDX_COL_SET_AUTO = 31,
            IDX_CARRIER = 32,
            IDX_CARRIER_EMPTY = 33,
            IDX_CARRIER_FULL = 34,
            IDX_LOT = 35,
            IDX_LOT_CHECK = 36,
            IDX_LOT_HOLD = 37,
            IDX_LOT_HOLD_CHECK = 38,
            IDX_LOT_RELEASE = 39,
            IDX_LOT_REWORK = 40,
            IDX_LOT_REWORK_CHECK = 41,
            IDX_LOT_ALTER = 42,
            IDX_LOT_ALTER_CHECK = 43,
            IDX_LOT_START = 44,
            IDX_LOT_START_CHECK = 45,
            IDX_CHART = 46,
            IDX_CHART_LINE = 47,
            IDX_LOG_SHEET = 48,
            IDX_AREA = 49,
            IDX_SUB_AREA = 50,
            IDX_FUNCTION_GROUP = 51,
            IDX_FUNCTION = 52,
            IDX_MESSAGE_GROUP = 53,
            IDX_MESSAGE = 54,
            IDX_PRIORITY = 55,
            IDX_BOM = 56,
            IDX_PORT = 57,
            IDX_ORDER = 58,
            IDX_ORDER_DELETE = 59,
            IDX_PART = 60,
            IDX_INQUIRY = 61,
            IDX_SUB_LOT = 62,
            IDX_HISTORY = 63,
            IDX_HISTORY_DELETE = 64,
            IDX_ALARM = 65,
            IDX_DEPARTMENT = 66,
            IDX_CALENDAR = 67,
            IDX_KEY = 68,
            IDX_CUSTOMER = 69,
            IDX_CATEGORY = 70,
            IDX_YEAR = 71,
            IDX_MONTH = 72,
            IDX_PM = 73,
            IDX_POLICY = 74,
            IDX_OPTION = 75,
            IDX_SLOT_EMPTY = 76,
            IDX_SLOT_FULL = 77,
            IDX_SLOT_FULL_NEW = 78,
            IDX_SLOT_COMBINE = 79,
            IDX_RECIPE = 80,
            IDX_STOCKER = 81,
            IDX_LABEL = 84,
            IDX_ALARM_ERROR = 88,
            IDX_ALARM_WARN = 89,
            IDX_ALARM_INFO = 90,
            IDX_REPAIR_LOT = 92,
            IDX_DISPATCHER = 93,
            //현재 아이콘이 없어서 다른 아이콘을 대신 사용하므로 나중에 수정되어야 함
            IDX_COL_SET_VERSION = 29,
            IDX_PRIVILEGE = 82,
            IDX_PRIVILEGE_GROUP = 83,
            IDX_TOOL = 85,
            IDX_TOOL_EVENT = 86,
            IDX_TOOL_TYPE = 87,
            IDX_TOOL_SCRAP = 91,
            IDX_TOOL_RETURN = 94,
            IDX_EQ_TYPE = 95,
            IDX_RCP_MGR_DIR = 96,
            IDX_MODULE_DIR = 97,
            IDX_MODULE = 98,
            IDX_RCP_HOLD = 99,
            IDX_VERSION = 100,
            IDX_VERSION_REQUEST = 101,
            IDX_VERSION_APPROVAL = 102,
            IDX_VERSION_ACTIVATE = 103,
            IDX_WHITE_IMAGE = 104
            //앞으로 추가되어야 함

        }

        #region "Variables"

        private bool b_mandatory_flag = false;
        private bool b_refuse_event_exec = false;
        private string s_factory_txt = "";
        private string s_val_txt = "";
        private string s_subVal_txt = "";
        private eConditionype m_e_condition_type;

        #endregion

        #region "Control Events"

        private MCCodeViewSelChangedHandler ValueSelectedItemChangedEvent;
        public event MCCodeViewSelChangedHandler ValueSelectedItemChanged
        {
            add
            {
                ValueSelectedItemChangedEvent = (MCCodeViewSelChangedHandler)System.Delegate.Combine(ValueSelectedItemChangedEvent, value);
            }
            remove
            {
                ValueSelectedItemChangedEvent = (MCCodeViewSelChangedHandler)System.Delegate.Remove(ValueSelectedItemChangedEvent, value);
            }
        }

        private System.EventHandler ValueButtonPressEvent;
        public event System.EventHandler ValueButtonPress
        {
            add
            {
                ValueButtonPressEvent = (System.EventHandler)System.Delegate.Combine(ValueButtonPressEvent, value);
            }
            remove
            {
                ValueButtonPressEvent = (System.EventHandler)System.Delegate.Remove(ValueButtonPressEvent, value);
            }
        }

        private System.Windows.Forms.KeyPressEventHandler ValueTextKeyPressEvent;
        public event System.Windows.Forms.KeyPressEventHandler ValueTextKeyPress
        {
            add
            {
                ValueTextKeyPressEvent = (System.Windows.Forms.KeyPressEventHandler)System.Delegate.Combine(ValueTextKeyPressEvent, value);
            }
            remove
            {
                ValueTextKeyPressEvent = (System.Windows.Forms.KeyPressEventHandler)System.Delegate.Remove(ValueTextKeyPressEvent, value);
            }
        }

        private System.EventHandler ValueTextChangedEvent;
        public event System.EventHandler ValueTextChanged
        {
            add
            {
                ValueTextChangedEvent = (System.EventHandler)System.Delegate.Combine(ValueTextChangedEvent, value);
            }
            remove
            {
                ValueTextChangedEvent = (System.EventHandler)System.Delegate.Remove(ValueTextChangedEvent, value);
            }
        }

        private System.EventHandler ValueTextLostFocusEvent;
        public event System.EventHandler ValueTextLostFocus
        {
            add
            {
                ValueTextLostFocusEvent = (System.EventHandler)System.Delegate.Combine(ValueTextLostFocusEvent, value);
            }
            remove
            {
                ValueTextLostFocusEvent = (System.EventHandler)System.Delegate.Remove(ValueTextLostFocusEvent, value);
            }
        }

        private System.EventHandler ValueTextGotFocusEvent;
        public event System.EventHandler ValueTextGotFocus
        {
            add
            {
                ValueTextGotFocusEvent = (System.EventHandler)System.Delegate.Combine(ValueTextGotFocusEvent, value);
            }
            remove
            {
                ValueTextGotFocusEvent = (System.EventHandler)System.Delegate.Remove(ValueTextGotFocusEvent, value);
            }
        }

        private MCCodeViewSelChangedHandler SubValueSelectedItemChangedEvent;
        public event MCCodeViewSelChangedHandler SubValueSelectedItemChanged
        {
            add
            {
                SubValueSelectedItemChangedEvent = (MCCodeViewSelChangedHandler)System.Delegate.Combine(SubValueSelectedItemChangedEvent, value);
            }
            remove
            {
                SubValueSelectedItemChangedEvent = (MCCodeViewSelChangedHandler)System.Delegate.Remove(SubValueSelectedItemChangedEvent, value);
            }
        }

        private System.EventHandler SubValueButtonPressEvent;
        public event System.EventHandler SubValueButtonPress
        {
            add
            {
                SubValueButtonPressEvent = (System.EventHandler)System.Delegate.Combine(SubValueButtonPressEvent, value);
            }
            remove
            {
                SubValueButtonPressEvent = (System.EventHandler)System.Delegate.Remove(SubValueButtonPressEvent, value);
            }
        }

        private System.Windows.Forms.KeyPressEventHandler SubValueKeyPressEvent;
        public event System.Windows.Forms.KeyPressEventHandler SubValueKeyPress
        {
            add
            {
                SubValueKeyPressEvent = (System.Windows.Forms.KeyPressEventHandler)System.Delegate.Combine(SubValueKeyPressEvent, value);
            }
            remove
            {
                SubValueKeyPressEvent = (System.Windows.Forms.KeyPressEventHandler)System.Delegate.Remove(SubValueKeyPressEvent, value);
            }
        }

        private System.EventHandler SubValueChangedEvent;
        public event System.EventHandler SubValueChanged
        {
            add
            {
                SubValueChangedEvent = (System.EventHandler)System.Delegate.Combine(SubValueChangedEvent, value);
            }
            remove
            {
                SubValueChangedEvent = (System.EventHandler)System.Delegate.Remove(SubValueChangedEvent, value);
            }
        }

        private System.EventHandler SubValueLostFocusEvent;
        public event System.EventHandler SubValueLostFocus
        {
            add
            {
                SubValueLostFocusEvent = (System.EventHandler)System.Delegate.Combine(SubValueLostFocusEvent, value);
            }
            remove
            {
                SubValueLostFocusEvent = (System.EventHandler)System.Delegate.Remove(SubValueLostFocusEvent, value);
            }
        }

        private System.EventHandler SubValueGotFocusEvent;
        public event System.EventHandler SubValueGotFocus
        {
            add
            {
                SubValueGotFocusEvent = (System.EventHandler)System.Delegate.Combine(SubValueGotFocusEvent, value);
            }
            remove
            {
                SubValueGotFocusEvent = (System.EventHandler)System.Delegate.Remove(SubValueGotFocusEvent, value);
            }
        }


        private void cdvValue_ButtonPress(object sender, EventArgs e)
        {
            if (ValueButtonPressEvent != null)
                ValueButtonPressEvent(this, e);

            switch (m_e_condition_type)
            {
                case eConditionype.FACTORY:
                    GetFactoryList(cdvValue.GetListView);
                    cdvValue.SelectedSubItemIndex = 0;
                    break;
                case eConditionype.FACTORY_ASSEMBLY:
                    GetFactoryList(cdvValue.GetListView);
                    cdvValue.SelectedSubItemIndex = 0;
                    break;
                case eConditionype.MATERIAL:
                    GetMatList(cdvValue.GetListView);
                    cdvValue.SelectedSubItemIndex = 0;
                    break;
                case eConditionype.FLOW:
                    GetFlwList(cdvValue.GetListView);
                    cdvValue.SelectedSubItemIndex = 0;
                    break;
                case eConditionype.OPER:
                    GetOprList(cdvValue.GetListView);
                    cdvValue.SelectedSubItemIndex = 0;
                    break;
                case eConditionype.RESOURCE:
                    break;
                case eConditionype.EVENT:
                    break;
                case eConditionype.TABLE:
                    break;
                case eConditionype.AREA:
                    break;
                case eConditionype.TEXT:
                    break;
                case eConditionype.CUSTOM:
                    break;
                case eConditionype.HQ_TABLE:
                    break;

                // 2010-04-19-임종우 : 원부자재 조회용 MATERIAL_TYPE 추가
                case eConditionype.MATERIAL_TYPE:
                    break;
            }
        }

        private void cdvValue_SelectedItemChanged(object sender, MCCodeViewSelChanged_EventArgs e)
        {
            if (ValueSelectedItemChangedEvent != null)
                ValueSelectedItemChangedEvent(this, e);

            switch (m_e_condition_type)
            {
                case eConditionype.FACTORY:
                    break;
                case eConditionype.FACTORY_ASSEMBLY:
                    break;
                case eConditionype.MATERIAL:
                    break;
                case eConditionype.FLOW:
                    if (this.ParentValue != "" && this.ParentSubValue != "")
                    {
                        int iSeq = cdvValue.SelectedItem.Index + 1;
                        cdvSubValue.Text = iSeq.ToString();//기본선택된순번을 Seq값에 넣어줌
                    }                    
                    break;
                case eConditionype.OPER:
                    break;
                case eConditionype.RESOURCE:
                    break;
                case eConditionype.EVENT:
                    break;
                case eConditionype.TABLE:
                    break;
                case eConditionype.AREA:
                    break;
                case eConditionype.TEXT:
                    break;
                case eConditionype.CUSTOM:
                    break;
                case eConditionype.HQ_TABLE:
                    break;

                // 2010-04-19-임종우 : 원부자재 조회용 MATERIAL_TYPE 추가
                case eConditionype.MATERIAL_TYPE:
                    break;
            }
        }

        private void cdvValue_TextBoxGotFocus(object sender, EventArgs e)
        {
            if (ValueTextGotFocusEvent != null)
                ValueTextGotFocusEvent(this, e);
        }

        private void cdvValue_TextBoxKeyPress(object sender, KeyPressEventArgs e)
        {
            if (ValueTextKeyPressEvent != null)
                ValueTextKeyPressEvent(this, e);
        }

        private void cdvValue_TextBoxLostFocus(object sender, EventArgs e)
        {
            if (ValueTextLostFocusEvent != null)
                ValueTextLostFocusEvent(this, e);
        }

        private void cdvValue_TextBoxTextChanged(object sender, EventArgs e)
        {
            if (ValueTextChangedEvent != null)
                ValueTextChangedEvent(this, e);
        }

        private void cdvSubValue_ButtonPress(object sender, EventArgs e)
        {
            if (SubValueButtonPressEvent != null)
                SubValueButtonPressEvent(this, e);
            switch (m_e_condition_type)
            {
                case eConditionype.MATERIAL:
                    GetMatVerList(cdvSubValue.GetListView);
                    break;
                case eConditionype.FLOW:
                    GetFlwSeqList(cdvSubValue.GetListView);
                    break;
            }
        }

        private void cdvSubValue_SelectedItemChanged(object sender, MCCodeViewSelChanged_EventArgs e)
        {
            if (SubValueSelectedItemChangedEvent != null)
                SubValueSelectedItemChangedEvent(this, e);
        }

        private void cdvSubValue_TextBoxGotFocus(object sender, EventArgs e)
        {
            if (SubValueGotFocusEvent != null)
                SubValueGotFocusEvent(this, e);
        }

        private void cdvSubValue_TextBoxKeyPress(object sender, KeyPressEventArgs e)
        {
            if (SubValueKeyPressEvent != null)
                SubValueKeyPressEvent(this, e);
        }

        private void cdvSubValue_TextBoxLostFocus(object sender, EventArgs e)
        {
            if (SubValueLostFocusEvent != null)
                SubValueLostFocusEvent(this, e);
        }

        private void cdvSubValue_TextBoxTextChanged(object sender, EventArgs e)
        {
            if (SubValueChangedEvent != null)
                SubValueChangedEvent(this, e);
        }

        #endregion

        #region "Properties"

        [Browsable(true), DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        public eConditionype CondtionType
        {
            get
            {
                return m_e_condition_type;
            }
            set
            {
                m_e_condition_type = value;
                InitControl();
            }
        }

        [Browsable(false), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public bool RefuseEventExec
        {
            get
            {
                return b_refuse_event_exec;
            }
            set
            {
                b_refuse_event_exec = value;
            }
        }

        [Browsable(false), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public ListView.ListViewItemCollection ValueItems
        {
            get
            {
                return cdvValue.Items;
            }
        }

        [Browsable(false), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public ListView.ColumnHeaderCollection ValueColumns
        {
            get
            {
                return cdvValue.Columns;
            }
        }

        [Browsable(false), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public ListViewItem ValueSelectedItem
        {
            get
            {
                return cdvValue.SelectedItem;
            }
        }

        [Browsable(false), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public bool ValueIsPopup
        {
            get
            {
                return cdvValue.IsPopup;
            }
            set
            {
                cdvValue.IsPopup = value;
            }
        }

        [Browsable(false), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public override string Text
        {
            get
            {
                return cdvValue.Text;
            }
            set
            {
                cdvValue.Text = value;
            }
        }

        [Browsable(false), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public string ValueDisplayText
        {
            get
            {
                return cdvValue.DisplayText;
            }
            set
            {
                cdvValue.DisplayText = value;
            }
        }

        [Browsable(false), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public ListView ValueGetListView
        {
            get
            {
                return cdvValue.GetListView;
            }
        }

        [Browsable(false), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public ListView.ListViewItemCollection SubValueItems
        {
            get
            {
                return cdvSubValue.Items;
            }
        }

        [Browsable(false), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public ListViewItem SubValueSelectedItem
        {
            get
            {
                return cdvSubValue.SelectedItem;
            }
        }

        [Browsable(false), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public bool SubValueIsPopup
        {
            get
            {
                return cdvSubValue.IsPopup;
            }
            set
            {
                cdvSubValue.IsPopup = value;
            }
        }

        [Browsable(false), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public ListView SubValueGetListView
        {
            get
            {
                return cdvSubValue.GetListView;
            }
        }

        [Browsable(true), DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        public int ValueMaxLength
        {
            get
            {
                return cdvValue.MaxLength;
            }
        }

        [Browsable(true), DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        public string ConditionText
        {
            get
            {
                return lblConditionLabel.Text;
            }
            set
            {
                lblConditionLabel.Text = value;
            }
        }

        [Browsable(true), DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        public bool VisibleValueButton
        {
            get
            {
                return cdvValue.VisibleButton;
            }
            set
            {
                cdvValue.VisibleButton = value;
            }
        }

        [Browsable(true), DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        public bool VisibleSubValueButton
        {
            get
            {
                return cdvSubValue.VisibleButton;
            }
            set
            {
                cdvSubValue.VisibleButton = value;
                if (value == true) cdvSubValue.Width = 45;
                if (value == false) cdvSubValue.Width = 25;
            }
        }

        [Browsable(true), DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        public bool VisibleSubValue
        {
            get
            {
                return cdvSubValue.Visible;
            }
            set
            {
                cdvSubValue.Visible = value;
                if (value == true) pnlSubValue.Width = 45;
                if (value == false) pnlSubValue.Width = 0;
            }
        }

        [Browsable(true), DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        public bool MandatoryFlag
        {
            get
            {
                return b_mandatory_flag;
            }
            set
            {
                b_mandatory_flag = value;

                if (b_mandatory_flag == true) 
                { 
                    lblConditionLabel.Font = new System.Drawing.Font("Tahoma", 8.25F, FontStyle.Bold); 
                } 
                else 
                {
                    lblConditionLabel.Font = new System.Drawing.Font("Tahoma", 8.25F, FontStyle.Regular); 
                }
            }
        }
        
        [Browsable(false), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public string txtValue
        {
            get
            {
                return cdvValue.Text;
            }
        }

        [Browsable(false), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public string txtSubValue
        {
            get
            {
                return cdvSubValue.Text;
            }
        }

        [Browsable(true), DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        public string ParentValue
        {
            get
            {
                return s_val_txt;
            }
            set
            {
                s_val_txt = value;
            }
        }

        [Browsable(true), DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        public string ParentSubValue
        {
            get
            {
                return s_subVal_txt;
            }
            set
            {
                s_subVal_txt = value;
            }
        }

        [Browsable(true), DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        public string sFactory
        {
            get
            {
                return s_factory_txt;
            }
            set
            {
                s_factory_txt = value;
            }
        }

        #endregion

        public void Init()
        {
            cdvValue.Text = "";
            cdvSubValue.Text = "";
        }

        private void InitControl()
        {
            VisibleSubValue = false;
            
            cdvValue.Init();
            CmnInitFunction.InitListView(cdvValue.GetListView);
            cdvSubValue.Init();
            CmnInitFunction.InitListView(cdvSubValue.GetListView);
            
            switch (m_e_condition_type)
            {
                case eConditionype.FACTORY:
                    lblConditionLabel.Text = "Factory";
                    cdvValue.Columns.Add("Factory", 50, HorizontalAlignment.Left);
                    cdvValue.Columns.Add("Desc", 100, HorizontalAlignment.Left);
                    cdvValue.SelectedSubItemIndex = 0;
                    break;
                case eConditionype.FACTORY_ASSEMBLY:
                    lblConditionLabel.Text = "Factory";
                    cdvValue.Columns.Add("Factory", 50, HorizontalAlignment.Left);
                    cdvValue.Columns.Add("Desc", 100, HorizontalAlignment.Left);
                    cdvValue.SelectedSubItemIndex = 0;
                    break;
                case eConditionype.MATERIAL:
                    lblConditionLabel.Text = "Material";
                    VisibleSubValue = true;
                    cdvValue.Columns.Add("Material", 50, HorizontalAlignment.Left);
                    cdvValue.Columns.Add("Desc", 100, HorizontalAlignment.Left);
                    cdvSubValue.Columns.Add("Version", 50, HorizontalAlignment.Left);                    
                    break;
                case eConditionype.FLOW:
                    lblConditionLabel.Text = "Flow";
                    VisibleSubValue = true;
                    cdvValue.Columns.Add("Flow", 50, HorizontalAlignment.Left);
                    cdvValue.Columns.Add("Desc", 100, HorizontalAlignment.Left);
                    cdvSubValue.Columns.Add("Seq", 50, HorizontalAlignment.Left); 
                    break;
                case eConditionype.OPER:
                    lblConditionLabel.Text = "Operation";
                    cdvValue.Columns.Add("Oper", 50, HorizontalAlignment.Left);
                    cdvValue.Columns.Add("Desc", 100, HorizontalAlignment.Left);
                    break;
                case eConditionype.RESOURCE:
                    lblConditionLabel.Text = "Resource";
                    break;
                case eConditionype.EVENT:
                    lblConditionLabel.Text = "Event";
                    break;
                case eConditionype.TABLE:
                    lblConditionLabel.Text = "Table";
                    break;
                case eConditionype.RAS_TBL:
                    lblConditionLabel.Text = "RAS Table";
                    break;
                case eConditionype.WIP_TBL:
                    lblConditionLabel.Text = "WIP Table";
                    break;
                case eConditionype.AREA:
                    lblConditionLabel.Text = "Area";
                    break;
                case eConditionype.TEXT:
                    lblConditionLabel.Text = "Text";
                    break;
                case eConditionype.CUSTOM:
                    lblConditionLabel.Text = "Custom";
                    break;
                case eConditionype.HQ_TABLE:
                    lblConditionLabel.Text = "HQ_Table";
                    break;

                // 2010-04-19-임종우 : 원부자재 조회용 MATERIAL_TYPE 추가
                case eConditionype.MATERIAL_TYPE:
                    lblConditionLabel.Text = "Mat_Type";
                    cdvValue.Columns.Add("Material", 50, HorizontalAlignment.Left);
                    cdvValue.Columns.Add("Desc", 100, HorizontalAlignment.Left);                    
                    break;
            }
        }

        private bool GetFactoryList(Control ctlList)
        {
            DataTable rtDataTable = new DataTable();
            ListViewItem itmX = null;
            string QueryCond = null;

            try
            {
                if (ctlList is ListView)
                {
                    CmnInitFunction.InitListView((ListView)ctlList);
                }

                ((ListView)ctlList).SmallImageList = imlSmallIcon;

                CmnFunction.oComm.SetUrl();

                rtDataTable = CmnFunction.oComm.FillData("MWIPFACDEF", "1", QueryCond);

                if (rtDataTable.Rows.Count > 0)
                {
                    for (int i = 0; i < rtDataTable.Rows.Count; i++)
                    {
                        if (ctlList is ListView)
                        {
                            itmX = new ListViewItem(rtDataTable.Rows[i]["FACTORY"].ToString().TrimEnd(), (int)SMALLICON_INDEX.IDX_FACTORY);
                            if (((ListView)ctlList).Columns.Count > 1)
                            {
                                itmX.SubItems.Add(rtDataTable.Rows[i]["FAC_DESC"].ToString().TrimEnd());
                            }
                            ((ListView)ctlList).Items.Add(itmX);
                        }
                        else if (ctlList is TreeView)
                        {

                        }
                        else if (ctlList is ComboBox)
                        {

                        }
                    }
                }

                if (rtDataTable.Rows.Count > 0)
                {
                    rtDataTable.Dispose();
                }
                return true;
            }
            catch (Exception ex)
            {
                CmnFunction.ShowMsgBox(ex.ToString());
                return false;
            }
        }
        
        private bool GetMatList(Control ctlList)
        {
            DataTable rtDataTable = new DataTable();
            ListViewItem itmX = null;
            string DinamicQuery = " ";

            try
            {
                if (ctlList is ListView)
                {
                    CmnInitFunction.InitListView((ListView)ctlList);
                }

                ((ListView)ctlList).SmallImageList = imlSmallIcon;

                CmnFunction.oComm.SetUrl();

                if (this.sFactory != "")
                {
                    DinamicQuery += " WHERE FACTORY = '" + this.sFactory + "'";

                }
                DinamicQuery += " ORDER BY MAT_ID";

                rtDataTable = CmnFunction.oComm.FillData("MWIPMATDEF", "1", DinamicQuery);

                if (rtDataTable.Rows.Count > 0)
                {
                    for (int i = 0; i < rtDataTable.Rows.Count; i++)
                    {
                        if (ctlList is ListView)
                        {
                            itmX = new ListViewItem(rtDataTable.Rows[i][0].ToString().TrimEnd(), (int)SMALLICON_INDEX.IDX_MATERIAL);
                            if (((ListView)ctlList).Columns.Count > 1)
                            {
                                itmX.SubItems.Add(rtDataTable.Rows[i][1].ToString().TrimEnd());
                            }
                            ((ListView)ctlList).Items.Add(itmX);
                        }
                    }
                }

                if (rtDataTable.Rows.Count > 0)
                {
                    rtDataTable.Dispose();
                }
                return true;
            }
            catch (Exception ex)
            {
                CmnFunction.ShowMsgBox(ex.ToString());
                return false;
            }
        }

        private bool GetMatVerList(Control ctlList)
        {
            DataTable rtDataTable = new DataTable();
            ListViewItem itmX = null;
            string DinamicQuery = " ";

            try
            {
                if (ctlList is ListView)
                {
                    CmnInitFunction.InitListView((ListView)ctlList);
                }

                CmnFunction.oComm.SetUrl();

                if (this.sFactory != "")
                {
                    DinamicQuery += " WHERE FACTORY = '" + this.sFactory + "' AND MAT_ID = '" + cdvValue.Text + "' ";

                }
                else
                {
                    DinamicQuery += " WHERE MAT_ID = '" + cdvValue.Text + "' ";
                }

                rtDataTable = CmnFunction.oComm.FillData("MWIPMATDEF", "2", DinamicQuery);

                if (rtDataTable.Rows.Count > 0)
                {
                    for (int i = 0; i < rtDataTable.Rows.Count; i++)
                    {
                        if (ctlList is ListView)
                        {
                            itmX = new ListViewItem(rtDataTable.Rows[i][0].ToString().TrimEnd());
                            ((ListView)ctlList).Items.Add(itmX);
                        }
                    }
                }

                if (rtDataTable.Rows.Count > 0)
                {
                    rtDataTable.Dispose();
                }
                return true;
            }
            catch (Exception ex)
            {
                CmnFunction.ShowMsgBox(ex.ToString());
                return false;
            }
        }

        private bool GetFlwList(Control ctlList)
        {
            DataTable rtDataTable = new DataTable();
            ListViewItem itmX = null;
            string DinamicQuery = " ";

            try
            {
                if (ctlList is ListView)
                {
                    CmnInitFunction.InitListView((ListView)ctlList);
                }

                ((ListView)ctlList).SmallImageList = imlSmallIcon;

                CmnFunction.oComm.SetUrl();


                if (this.ParentValue != "" && this.ParentSubValue != "")
                {//Material과Material Version값이 선택되었을경우
                    if (this.sFactory != "")
                    {
                        DinamicQuery += " WHERE FACTORY = '" + this.sFactory + "' ";
                        DinamicQuery += " AND MAT_ID = '" + this.ParentValue + "' AND MAT_VER = '" + this.ParentSubValue + "' ";
                    }
                    else
                    {
                        DinamicQuery += " WHERE MAT_ID = '" + this.ParentValue + "' AND MAT_VER = '" + this.ParentSubValue + "' ";
                    }
                    DinamicQuery += " ORDER BY FLOW_SEQ_NUM";

                    rtDataTable = CmnFunction.oComm.FillData("MWIPFLWDEF", "2", DinamicQuery);

                    cdvSubValue.Text = "";
                    cdvSubValue.Visible = true;
                }
                else
                {
                    if (this.sFactory != "")
                    {
                        DinamicQuery += " WHERE FACTORY = '" + this.sFactory + "'";
                    }
                    rtDataTable = CmnFunction.oComm.FillData("MWIPFLWDEF", "4", DinamicQuery);

                    cdvSubValue.Text = "";
                    cdvSubValue.Visible = false;
                }

                if (rtDataTable.Rows.Count > 0)
                {
                    for (int i = 0; i < rtDataTable.Rows.Count; i++)
                    {
                        if (ctlList is ListView)
                        {
                            itmX = new ListViewItem(rtDataTable.Rows[i][0].ToString().TrimEnd(), (int)SMALLICON_INDEX.IDX_FLOW);
                            if (((ListView)ctlList).Columns.Count > 1)
                            {
                                itmX.SubItems.Add(rtDataTable.Rows[i][1].ToString().TrimEnd());
                            }
                            ((ListView)ctlList).Items.Add(itmX);
                        }
                    }
                }

                if (rtDataTable.Rows.Count > 0)
                {
                    rtDataTable.Dispose();
                }
                return true;
            }
            catch (Exception ex)
            {
                CmnFunction.ShowMsgBox(ex.ToString());
                return false;
            }
        }

        private bool GetFlwSeqList(Control ctlList)
        {
            DataTable rtDataTable = new DataTable();
            ListViewItem itmX = null;
            string DinamicQuery = " ";

            try
            {
                if (ctlList is ListView)
                {
                    CmnInitFunction.InitListView((ListView)ctlList);
                }

                CmnFunction.oComm.SetUrl();

                if (this.sFactory != "")
                {
                    DinamicQuery += " WHERE FACTORY = '" + this.sFactory + "' ";
                    DinamicQuery += " AND MAT_ID = '" + this.ParentValue + "' AND MAT_VER = '" + this.ParentSubValue + "' ";
                    DinamicQuery += " AND FLOW = '" + cdvValue.Text + "' ";
                }
                else
                {//Flow Seq는 MWIPMATFLW에서 가져오므로 MAT_ID,MAT_VER의 정보가 없으면 Seq데이터 조회 불가.(Flow Seq Control의 Visible이 False됨)
                    DinamicQuery += " WHERE MAT_ID = '" + this.ParentValue + "' AND MAT_VER = '" + this.ParentSubValue + "' ";
                    DinamicQuery += " AND FLOW = '" + cdvValue.Text + "' ";
                }
                DinamicQuery += " ORDER BY FLOW_SEQ_NUM";

                rtDataTable = CmnFunction.oComm.FillData("MWIPFLWDEF", "3", DinamicQuery);


                if (rtDataTable.Rows.Count > 0)
                {
                    for (int i = 0; i < rtDataTable.Rows.Count; i++)
                    {
                        if (ctlList is ListView)
                        {
                            itmX = new ListViewItem(rtDataTable.Rows[i][0].ToString().TrimEnd());
                            ((ListView)ctlList).Items.Add(itmX);
                        }
                    }
                }

                if (rtDataTable.Rows.Count > 0)
                {
                    rtDataTable.Dispose();
                }
                return true;
            }
            catch (Exception ex)
            {
                CmnFunction.ShowMsgBox(ex.ToString());
                return false;
            }
        }

        private bool GetOprList(Control ctlList)
        {
            DataTable rtDataTable = new DataTable();
            ListViewItem itmX = null;
            string DinamicQuery = " ";

            try
            {
                if (ctlList is ListView)
                {
                    CmnInitFunction.InitListView((ListView)ctlList);
                }

                ((ListView)ctlList).SmallImageList = imlSmallIcon;

                CmnFunction.oComm.SetUrl();

                if (this.ParentValue != "")
                {//FLOW 값이 선택되었을경우
                    if (this.sFactory != "")
                    {
                        DinamicQuery += " WHERE FACTORY = '" + this.sFactory + "' ";
                        DinamicQuery += " AND FLOW = '" + this.ParentValue + "' ";
                    }
                    else
                    {
                        DinamicQuery += " WHERE FLOW = '" + this.ParentValue + "' ";
                    }
                    DinamicQuery += " ORDER BY SEQ_NUM";
                    rtDataTable = CmnFunction.oComm.FillData("MWIPOPRDEF", "2", DinamicQuery);
                }
                else
                {
                    if (this.sFactory != "")
                    {
                        DinamicQuery += " WHERE FACTORY = '" + this.sFactory + "' ";
                    }
                    rtDataTable = CmnFunction.oComm.FillData("MWIPOPRDEF", "3", DinamicQuery);
                }

                if (rtDataTable.Rows.Count > 0)
                {
                    for (int i = 0; i < rtDataTable.Rows.Count; i++)
                    {
                        if (ctlList is ListView)
                        {
                            itmX = new ListViewItem(rtDataTable.Rows[i][0].ToString().TrimEnd(), (int)SMALLICON_INDEX.IDX_FLOW);
                            if (((ListView)ctlList).Columns.Count > 1)
                            {
                                itmX.SubItems.Add(rtDataTable.Rows[i][1].ToString().TrimEnd());
                            }
                            ((ListView)ctlList).Items.Add(itmX);
                        }
                    }
                }

                if (rtDataTable.Rows.Count > 0)
                {
                    rtDataTable.Dispose();
                }
                return true;
            }
            catch (Exception ex)
            {
                CmnFunction.ShowMsgBox(ex.ToString());
                return false;
            }
        }
        
    }
}
