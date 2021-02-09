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
    public delegate void ListItemChangedEventHandler(object sender, System.EventArgs e);
    [DefaultEvent("SelectedItemChanged")]

    public partial class udcMCondition : UserControl
    {
        public udcMCondition()
        {
            InitializeComponent();
        }

        //Condition Type
        public enum eConditionype
        {
            FACCMF = 0,
            GCMGRP = 1
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

        private eConditionype m_e_condition_type;

        #endregion

        #region "Control Events"

        private ListItemChangedEventHandler SelectedItemChangedEvent;
        public event ListItemChangedEventHandler SelectedItemChanged
        {
            add
            {
                SelectedItemChangedEvent = (ListItemChangedEventHandler)System.Delegate.Combine(SelectedItemChangedEvent, value);
            }
            remove
            {
                SelectedItemChangedEvent = (ListItemChangedEventHandler)System.Delegate.Remove(SelectedItemChangedEvent, value);
            }
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
        public ListView.ListViewItemCollection ValueItems
        {
            get
            {
                return lisList.Items;
            }
        }

        [Browsable(false), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public ListView.ColumnHeaderCollection ValueColumns
        {
            get
            {
                return lisList.Columns;
            }
        }

        [Browsable(true), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public ListView.SelectedListViewItemCollection SelectedItems
        {
            get
            {
                return lisList.SelectedItems;
            }
        }

        [Browsable(true), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public ListView GetListView
        {
            get
            {
                return lisList;
            }
        }

        #endregion

        public void Init()
        {

        }

        private void InitControl()
        {            
            CmnInitFunction.InitListView(lisList);
            
            switch (m_e_condition_type)
            {
                case eConditionype.FACCMF:
                    lisList.Columns.Clear();
                    lisList.Columns.Add("PROMPT", 100, HorizontalAlignment.Left);
                    lisList.Columns.Add("TABLE_NAME", 130, HorizontalAlignment.Left);
                    lisList.MultiSelect = false;
                    break;
                case eConditionype.GCMGRP:
                    lisList.Columns.Clear();
                    lisList.Columns.Add("GROUP", 100, HorizontalAlignment.Left);
                    lisList.Columns.Add("DESC", 130, HorizontalAlignment.Left);
                    lisList.MultiSelect = true;
                    break;
            }
        }

        private void lisList_MouseUp(object sender, MouseEventArgs e)
        {
            if (SelectedItemChangedEvent != null)
                SelectedItemChangedEvent(this, e);
        }

        private void lisList_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Up || e.KeyCode == Keys.Down)
            {
                if (SelectedItemChangedEvent != null)
                    SelectedItemChangedEvent(this, e);
            }
        }
    }
}
