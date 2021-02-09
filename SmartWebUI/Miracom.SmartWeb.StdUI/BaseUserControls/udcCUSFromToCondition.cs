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
    public partial class udcCUSFromToCondition : UserControl
    {
        public udcCUSFromToCondition()
        {
            InitializeComponent();
        }

        #region "Variables"

        //Condition Type
        public enum eFromToType
        {
            FACTORY = 0,
            MATERIAL = 1,
            FLOW = 2,
            OPER = 3,
            RESOURCE = 4,
            EVENT = 5,
            TABLE = 6,
            HQ_TABLE = 7,
            TEXT = 99,
            CUSTOM = 100
        }

        private bool b_mandatory_flag = false;
        private string s_factory_txt = "";
        private string s_table_txt = "";
        private string s_val_txt = "";
        private eFromToType m_e_FromTo_type;

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

        /// <summary>
        /// Button Press 이벤트 발생시 조건 타입에 해당하는 로직처리.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cdvValue_ButtonPress(object sender, EventArgs e)
        {
            StringBuilder sMySql = new StringBuilder();
            DataTable rtDataTable = new DataTable();
            ListViewItem itmX = null;

            if (ValueButtonPressEvent != null)
                ValueButtonPressEvent(this, e);

            switch (m_e_FromTo_type)
            {
                case eFromToType.FACTORY:
                    RptCUSListFunction.GetFactoryList(((Miracom.UI.Controls.MCCodeView.MCCodeView)sender).GetListView);
                    ((Miracom.UI.Controls.MCCodeView.MCCodeView)sender).SelectedSubItemIndex = 0;
                    break;
                case eFromToType.MATERIAL:
                    if (CmnFunction.Trim(sFactory) == "")
                    {
                        CmnFunction.ShowMsgBox(RptMessages.GetMessage("STD001", GlobalVariable.gcLanguage));
                        return;
                    }
                    RptCUSListFunction.GetMatList(((Miracom.UI.Controls.MCCodeView.MCCodeView)sender).GetListView, this.sFactory);
                    ((Miracom.UI.Controls.MCCodeView.MCCodeView)sender).SelectedSubItemIndex = 0;
                    break;
                case eFromToType.FLOW:
                    if (CmnFunction.Trim(sFactory) == "")
                    {
                        CmnFunction.ShowMsgBox(RptMessages.GetMessage("STD001", GlobalVariable.gcLanguage));
                        return;
                    }
                    RptCUSListFunction.GetFlwList(((Miracom.UI.Controls.MCCodeView.MCCodeView)sender).GetListView, this.sFactory);
                    ((Miracom.UI.Controls.MCCodeView.MCCodeView)sender).SelectedSubItemIndex = 0;
                    break;
                case eFromToType.OPER:
                    if (CmnFunction.Trim(sFactory) == "")
                    {
                        CmnFunction.ShowMsgBox(RptMessages.GetMessage("STD001", GlobalVariable.gcLanguage));
                        return;
                    }
                    RptCUSListFunction.GetOprList(((Miracom.UI.Controls.MCCodeView.MCCodeView)sender).GetListView, this.sFactory);
                    ((Miracom.UI.Controls.MCCodeView.MCCodeView)sender).SelectedSubItemIndex = 0;
                    break;
                case eFromToType.RESOURCE:
                    if (CmnFunction.Trim(sFactory) == "")
                    {
                        CmnFunction.ShowMsgBox(RptMessages.GetMessage("STD001", GlobalVariable.gcLanguage));
                        return;
                    }
                    break;
                case eFromToType.EVENT:
                    if (CmnFunction.Trim(sFactory) == "")
                    {
                        CmnFunction.ShowMsgBox(RptMessages.GetMessage("STD001", GlobalVariable.gcLanguage));
                        return;
                    }
                    break;
                case eFromToType.TABLE:
                    if (CmnFunction.Trim(sFactory) == "")
                    {
                        CmnFunction.ShowMsgBox(RptMessages.GetMessage("STD001", GlobalVariable.gcLanguage));
                        return;
                    }
                    RptCUSListFunction.GetTableList(((Miracom.UI.Controls.MCCodeView.MCCodeView)sender).GetListView, this.sFactory, sTableName);
                    ((Miracom.UI.Controls.MCCodeView.MCCodeView)sender).SelectedSubItemIndex = 0;
                    break;
                case eFromToType.TEXT:
                    break;
                case eFromToType.HQ_TABLE:
                    if (CmnFunction.Trim(sFactory) == "")
                    {
                        CmnFunction.ShowMsgBox(RptMessages.GetMessage("STD001", GlobalVariable.gcLanguage));
                        return;
                    }

                    if (cdvFromValue.GetListView is ListView)
                    {
                        CmnInitFunction.InitListView((ListView)cdvFromValue.GetListView);
                    }

                    if (cdvToValue.GetListView is ListView)
                    {
                        CmnInitFunction.InitListView((ListView)cdvToValue.GetListView);
                    }

                    CmnFunction.oComm.SetUrl();

                    DataTable dt = null;
                    string sHQSql = "";

                    sMySql = new StringBuilder();
                    sMySql.Append("SELECT SQL_1" + "\n");
                    sMySql.Append("  FROM MGCMTBLDEF" + "\n");
                    sMySql.AppendFormat(" WHERE FACTORY    = '{0}' " + "\n", this.sFactory);
                    sMySql.AppendFormat("   AND TABLE_NAME = '{0}' " + "\n", this.sTableName.ToString().Trim());

                    dt = CmnFunction.oComm.GetFuncDataTable("DYNAMIC", sMySql.ToString());

                    if (dt == null) break;

                    if (dt.Rows.Count == 0) break;

                    sHQSql = dt.Rows[0][0].ToString().Trim();

                    if (sHQSql == "") break;

                    rtDataTable = CmnFunction.oComm.GetFuncDataTable("DYNAMIC", sHQSql);

                    if (rtDataTable.Rows.Count > 0)
                    {
                        for (int i = 0; i < rtDataTable.Rows.Count; i++)
                        {
                            if (cdvFromValue.GetListView is ListView && cdvToValue.GetListView is ListView)
                            {
                                itmX = new ListViewItem(rtDataTable.Rows[i][0].ToString().TrimEnd(), (int)SMALLICON_INDEX.IDX_OPER);
                                if (rtDataTable.Columns.Count > 1)
                                    itmX.SubItems.Add(rtDataTable.Rows[i][1].ToString().TrimEnd());
                                else
                                    itmX.SubItems.Add(" ".TrimEnd());

                                ((ListView)cdvFromValue.GetListView).Items.Add(itmX);
                                ((ListView)cdvToValue.GetListView).Items.Add((ListViewItem)itmX.Clone());
                            }
                        }
                    }
                    if (rtDataTable.Rows.Count > 0)
                    {
                        rtDataTable.Dispose();
                    }
                    cdvFromValue.SelectedSubItemIndex = 0;
                    cdvToValue.SelectedSubItemIndex = 0;

                    break;
                case eFromToType.CUSTOM:
                    break;
            }
        }

        /// <summary>
        /// Selected Item Change 이벤트 발생시 조건 타입에 해당하는 로직처리.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cdvValue_SelectedItemChanged(object sender, MCCodeViewSelChanged_EventArgs e)
        {
            if (ValueSelectedItemChangedEvent != null)
                ValueSelectedItemChangedEvent(this, e);

            switch (m_e_FromTo_type)
            {
                case eFromToType.FACTORY:
                    break;
                case eFromToType.MATERIAL:
                    break;
                case eFromToType.FLOW:
                    break;
                case eFromToType.OPER:
                    if (cdvFromValue.Equals((Miracom.UI.Controls.MCCodeView.MCCodeView)sender))
                    {
                        if (cdvToValue.Text.Trim().Length != 0)
                        {
                            if (cdvFromValue.SelectedItem.Index > cdvToValue.SelectedItem.Index) cdvToValue.Text = cdvFromValue.Text;
                        }                        
                    }
                    else
                    {
                        if (cdvFromValue.Text.Trim().Length != 0)
                        {
                            if (cdvFromValue.SelectedItem.Index > cdvToValue.SelectedItem.Index) cdvToValue.Text = cdvFromValue.Text;                            
                        }
                    }
                    break;
                case eFromToType.RESOURCE:
                    break;
                case eFromToType.EVENT:
                    break;
                case eFromToType.TABLE:
                    break;
                case eFromToType.TEXT:
                    break;
                case eFromToType.HQ_TABLE:
                    break;
                case eFromToType.CUSTOM:
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

        #endregion

        #region "Properties"

        [Browsable(true), DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        public eFromToType CondtionType
        {
            get
            {
                return m_e_FromTo_type;
            }
            set
            {
                m_e_FromTo_type = value;
                InitControl();
            }
        }

        [Browsable(false), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public string FromText
        {
            get
            {
                return cdvFromValue.Text;
            }
            set
            {
                cdvFromValue.Text = value;
            }
        }

        [Browsable(false), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public string ToText
        {
            get
            {
                return cdvToValue.Text;
            }
            set
            {
                cdvToValue.Text = value;
            }
        }

        [Browsable(false), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public string FromValueDisplayText
        {
            get
            {
                return cdvFromValue.DisplayText;
            }
            set
            {
                cdvFromValue.DisplayText = value;
            }
        }

        [Browsable(false), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public string ToValueDisplayText
        {
            get
            {
                return cdvToValue.DisplayText;
            }
            set
            {
                cdvToValue.DisplayText = value;
            }
        }

        [Browsable(false), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public ListView FromValueGetListView
        {
            get
            {
                return cdvFromValue.GetListView;
            }
        }

        [Browsable(false), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public ListView ToValueGetListView
        {
            get
            {
                return cdvToValue.GetListView;
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
        public string txtFromValue
        {
            get
            {
                return cdvFromValue.Text;
            }
        }

        [Browsable(false), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public string txtToValue
        {
            get
            {
                return cdvToValue.Text;
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
        public string sFactory
        {
            get
            {
                return s_factory_txt;
            }
            set
            {
                s_factory_txt = value;
                if (this.DesignMode == false) InitFromToByFactory();
            }
        }

        [Browsable(true), DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        public string sTableName
        {
            get
            {
                return s_table_txt;
            }
            set
            {
                s_table_txt = value;
            }
        }

        #endregion

        public void Init()
        {
            cdvFromValue.Text = "";
            cdvToValue.Text = "";
        }

        /// <summary>
        /// 조건 타입에 해당하는 Factory 입력시의 Control 초기화
        /// </summary>
        public void InitFromToByFactory()
        {
            Init();

            switch (m_e_FromTo_type)
            {
                case eFromToType.FACTORY:
                    
                    break;
                case eFromToType.MATERIAL:
                    
                    break;
                case eFromToType.FLOW:
                    
                    break;
                case eFromToType.OPER:
                    //if (this.sFactory.Trim() == "") return;
                    //RptCUSListFunction.GetOprList(cdvFromValue.GetTextBox, cdvToValue.GetTextBox, this.sFactory);
                    break;
                case eFromToType.RESOURCE:
                    
                    break;
                case eFromToType.EVENT:
                    
                    break;
                case eFromToType.TABLE:

                    break;
                case eFromToType.TEXT:

                    break;
                case eFromToType.HQ_TABLE:

                    break;
                case eFromToType.CUSTOM:
                    
                    break;
            }
        }

        // From TO 에서 선택된 값을 ListView로 리턴 ( ex : OPER From To 사용 할때 )
        public ListView getItemsFromToValue()
        {                     
            ListView lvFrom = new ListView();
            ListView lvTo = new ListView();
            ListView SelectedItem = new ListView();

            int iFromIdx = 0;
            int iToIdx = 0;           

            try
            {
                lvFrom = this.FromValueGetListView;
                lvTo = this.ToValueGetListView;

                if (lvFrom.Items.Count == 0)
                {
                    SelectedItem.Items.Clear();
                    SelectedItem.Items.Add("");
                    return SelectedItem;
                }
                    
                if (lvTo.Items.Count == 0)
                {
                    SelectedItem.Items.Clear();
                    SelectedItem.Items.Add("");
                    return SelectedItem;
                }

                //iFromIdx = lvFrom.Items.IndexOf(lvFrom.SelectedItems[0]);
                //iToIdx = lvTo.Items.IndexOf(lvTo.SelectedItems[0]);
                for (int i = 0; i < lvFrom.Items.Count; i++)
                {
                    if (lvFrom.Items[i].Text == this.FromText)
                    {
                        iFromIdx = i;
                        break;
                    }
                }

                for (int i = 0; i < lvTo.Items.Count; i++)
                {
                    if (lvTo.Items[i].Text == this.ToText)
                    {
                        iToIdx = i;
                        break;
                    }
                }

                SelectedItem.Items.Clear();
                for (int i = iFromIdx; i <= iToIdx; i++)
                {
                    SelectedItem.Items.Add(lvFrom.Items[i].Text);                    
                }

                return SelectedItem;
            }
            catch (Exception)
            {
                //CmnFunction.ShowMsgBox(ex.Message, "Error", MessageBoxButtons.OK, 1);
                SelectedItem.Items.Clear();
                SelectedItem.Items.Add("");
                return SelectedItem;
            }

        }
        
        // OPER 선택시 BETWEEN 쿼리 생성
        public string getInQuery()
        {
            ListView Oper_Items = getItemsFromToValue();
            string sQuery = null;

            if (Oper_Items.Items.Count > 0)
            {
                for (int i = 0; i < Oper_Items.Items.Count; i++)
                {
                    if( i == 0)
                    {
                        sQuery = sQuery + "'" + Oper_Items.Items[i].Text + "'";
                    }
                    else
                    {
                        sQuery = sQuery + ", '" + Oper_Items.Items[i].Text + "'";
                    }
                    
                }
            }

            return sQuery;
        }


        // From To에서 선택된 갯수 리턴.
        public int CountFromToValue
        {
            get
            {
                ListView lvFrom = new ListView();
                ListView lvTo = new ListView();

                int iFromIdx = 0;
                int iToIdx = 0;

                lvFrom = this.FromValueGetListView;
                lvTo = this.ToValueGetListView;

                if (lvFrom.Items.Count == 0) 
                    throw new Exception(RptMessages.GetMessage("STD083", GlobalVariable.gcLanguage));

                if (lvTo.Items.Count == 0)
                    throw new Exception(RptMessages.GetMessage("STD083", GlobalVariable.gcLanguage));

                for (int i = 0; i < lvFrom.Items.Count; i++)
                {
                    if (lvFrom.Items[i].Text == this.FromText)
                    {
                        iFromIdx = i;
                        break;
                    }
                }

                for (int i = 0; i < lvTo.Items.Count; i++)
                {
                    if (lvTo.Items[i].Text == this.ToText)
                    {
                        iToIdx = i;
                        break;
                    }
                }

                //아래 부분은 Bug발생.
                //iFromIdx = lvFrom.Items.IndexOf(lvFrom.SelectedItems[0]);
                //iToIdx = lvTo.Items.IndexOf(lvTo.SelectedItems[0]);

                return iToIdx - iFromIdx + 1;                              

            }
        }

        // SQL문 생성. by John Seo. 2008.11.25
        public string getDecodeQuery(string sFront, string sBack, string sAlias)
        {
            System.Text.StringBuilder strSqlString = new System.Text.StringBuilder();
            string sHeader = null;
            ListView lvFrom = new ListView();
            ListView lvTo = new ListView();
                         
            int iFromIdx = 0;
            int iToIdx = 0;
            int idx = 0;

            try
            {
                lvFrom = this.FromValueGetListView;
                lvTo = this.ToValueGetListView;

                if (lvFrom.Items.Count == 0) return "";
                if (lvTo.Items.Count == 0) return "";

                //iFromIdx = lvFrom.Items.IndexOf(lvFrom.SelectedItems[0]);
                //iToIdx = lvTo.Items.IndexOf(lvTo.SelectedItems[0]);
                for (int i = 0; i < lvFrom.Items.Count; i++)
                {
                    if (lvFrom.Items[i].Text == this.FromText)
                    {
                        iFromIdx = i;
                        break;
                    }
                }

                for (int i = 0; i < lvTo.Items.Count; i++)
                {
                    if (lvTo.Items[i].Text == this.ToText)
                    {
                        iToIdx = i;
                        break;
                    }
                }

                for (int i = iFromIdx; i <= iToIdx; i++)
                {
                    sHeader = lvFrom.Items[i].Text;
                    strSqlString.AppendFormat(", " + "{0}, '{1}', {2} {3}{4}" + "\n", sFront, sHeader, sBack, sAlias, idx.ToString());
                    idx++;
                }

                return strSqlString.ToString();
            }
            catch (Exception)
            {
                //CmnFunction.ShowMsgBox(ex.Message, "Error", MessageBoxButtons.OK, 1);
                return "";
            }

        }

        // SQL문 생성.
        public string getRepeatQuery(string sFront, string sBack, string sAlias)
        {
            System.Text.StringBuilder strSqlString = new System.Text.StringBuilder();            
            ListView lvFrom = new ListView();
            ListView lvTo = new ListView();
                         
            int iFromIdx = 0;
            int iToIdx = 0;            
            int iCount = 0;

            try
            {
                lvFrom = this.FromValueGetListView;
                lvTo = this.ToValueGetListView;

                if (lvFrom.Items.Count == 0) return "";
                if (lvTo.Items.Count == 0) return "";

                //iFromIdx = lvFrom.Items.IndexOf(lvFrom.SelectedItems[0]);
                //iToIdx = lvTo.Items.IndexOf(lvTo.SelectedItems[0]);
                for (int i = 0; i < lvFrom.Items.Count; i++)
                {
                    if (lvFrom.Items[i].Text == this.FromText)
                    {
                        iFromIdx = i;
                        break;
                    }
                }

                for (int i = 0; i < lvTo.Items.Count; i++)
                {
                    if (lvTo.Items[i].Text == this.ToText)
                    {
                        iToIdx = i;
                        break;
                    }
                }

                iCount = iToIdx - iFromIdx;

                for (int i = 0; i <= iCount; i++)
                {
                    strSqlString.AppendFormat(", " + "{0}{1} " + "{2}" + " {3}{1}" + "\n", sFront, i.ToString(), sBack, sAlias);
                }

                return strSqlString.ToString();
            }
            catch (Exception)
            {
                //CmnFunction.ShowMsgBox(ex.Message, "Error", MessageBoxButtons.OK, 1);
                return "";
            }

        }

        public string getRepeatQuery(string first, string second, string third, string fourth)
        {
            System.Text.StringBuilder strSqlString = new System.Text.StringBuilder();
            ListView lvFrom = new ListView();
            ListView lvTo = new ListView();

            int iFromIdx = 0;
            int iToIdx = 0;
            int iCount = 0;

            try
            {
                lvFrom = this.FromValueGetListView;
                lvTo = this.ToValueGetListView;

                if (lvFrom.Items.Count == 0) return "";
                if (lvTo.Items.Count == 0) return "";

                //iFromIdx = lvFrom.Items.IndexOf(lvFrom.SelectedItems[0]);
                //iToIdx = lvTo.Items.IndexOf(lvTo.SelectedItems[0]);
                for (int i = 0; i < lvFrom.Items.Count; i++)
                {
                    if (lvFrom.Items[i].Text == this.FromText)
                    {
                        iFromIdx = i;
                        break;
                    }
                }

                for (int i = 0; i < lvTo.Items.Count; i++)
                {
                    if (lvTo.Items[i].Text == this.ToText)
                    {
                        iToIdx = i;
                        break;
                    }
                }

                iCount = iToIdx - iFromIdx;

                for (int i = 0; i <= iCount; i++)
                {
                    strSqlString.AppendFormat(", " + "{0}{4} " + "{1}{4}" + " {2}{4}{3}{4}" + "\n", first, second, third, fourth, i.ToString());
                }

                return strSqlString.ToString();
            }
            catch (Exception)
            {
                //CmnFunction.ShowMsgBox(ex.Message, "Error", MessageBoxButtons.OK, 1);
                return "";
            }
        }

        // SQL문 생성.
        // strSql의 char '?' 를 반복 숫자로 바꾼다.
        public string getMultyRepeatQuery(string sFront, string sAlias)
        {
            System.Text.StringBuilder strSqlString = new System.Text.StringBuilder();
            ListView lvFrom = new ListView();
            ListView lvTo = new ListView();

            int iFromIdx = 0;
            int iToIdx = 0;
            int iCount = 0;

            try
            {
                lvFrom = this.FromValueGetListView;
                lvTo = this.ToValueGetListView;

                if (lvFrom.Items.Count == 0) return "";
                if (lvTo.Items.Count == 0) return "";

                //iFromIdx = lvFrom.Items.IndexOf(lvFrom.SelectedItems[0]);
                //iToIdx = lvTo.Items.IndexOf(lvTo.SelectedItems[0]);
                for (int i = 0; i < lvFrom.Items.Count; i++)
                {
                    if (lvFrom.Items[i].Text == this.FromText)
                    {
                        iFromIdx = i;
                        break;
                    }
                }

                for (int i = 0; i < lvTo.Items.Count; i++)
                {
                    if (lvTo.Items[i].Text == this.ToText)
                    {
                        iToIdx = i;
                        break;
                    }
                }

                iCount = iToIdx - iFromIdx;

                string sBack = string.Empty;
                for (int i = 0; i <= iCount; i++)
                {
                    sBack = sFront.Replace("?", i.ToString());
                    strSqlString.AppendFormat(", " + "{0} {2}{1}" + "\n", sBack, i.ToString(), sAlias);
                }

                return strSqlString.ToString();
            }
            catch (Exception)
            {
                //CmnFunction.ShowMsgBox(ex.Message, "Error", MessageBoxButtons.OK, 1);
                return "";
            }
        }


        /// <summary>
        /// 조건 타입에 해당하는 Control 초기화
        /// </summary>
        private void InitControl()
        {
            switch (m_e_FromTo_type)
            {
                case eFromToType.FACTORY:
                    lblConditionLabel.Text = "Factory";

                    cdvFromValue.Init();
                    CmnInitFunction.InitListView(cdvFromValue.GetListView);
                    cdvFromValue.Columns.Add("Factory", 50, HorizontalAlignment.Left);
                    cdvFromValue.Columns.Add("Desc", 100, HorizontalAlignment.Left);

                    cdvToValue.Init();
                    CmnInitFunction.InitListView(cdvToValue.GetListView);
                    cdvToValue.Columns.Add("Factory", 50, HorizontalAlignment.Left);
                    cdvToValue.Columns.Add("Desc", 100, HorizontalAlignment.Left);

                    break;
                case eFromToType.MATERIAL:
                    lblConditionLabel.Text = "Material";

                    cdvFromValue.Init();
                    CmnInitFunction.InitListView(cdvFromValue.GetListView);
                    cdvFromValue.Columns.Add("Material", 50, HorizontalAlignment.Left);
                    cdvFromValue.Columns.Add("Desc", 100, HorizontalAlignment.Left);

                    cdvToValue.Init();
                    CmnInitFunction.InitListView(cdvToValue.GetListView);
                    cdvToValue.Columns.Add("Material", 50, HorizontalAlignment.Left);
                    cdvToValue.Columns.Add("Desc", 100, HorizontalAlignment.Left);

                    break;
                case eFromToType.FLOW:
                    lblConditionLabel.Text = "Flow";

                    cdvFromValue.Init();
                    CmnInitFunction.InitListView(cdvFromValue.GetListView);
                    cdvFromValue.Columns.Add("Flow", 50, HorizontalAlignment.Left);
                    cdvFromValue.Columns.Add("Desc", 100, HorizontalAlignment.Left);

                    cdvToValue.Init();
                    CmnInitFunction.InitListView(cdvToValue.GetListView);
                    cdvToValue.Columns.Add("Flow", 50, HorizontalAlignment.Left);
                    cdvToValue.Columns.Add("Desc", 100, HorizontalAlignment.Left);

                    break;
                case eFromToType.OPER:
                    lblConditionLabel.Text = "Operation";

                    cdvFromValue.Init();
                    CmnInitFunction.InitListView(cdvFromValue.GetListView);
                    cdvFromValue.Columns.Add("Oper", 50, HorizontalAlignment.Left);
                    cdvFromValue.Columns.Add("Desc", 100, HorizontalAlignment.Left);

                    cdvToValue.Init();
                    CmnInitFunction.InitListView(cdvToValue.GetListView);
                    cdvToValue.Columns.Add("Oper", 50, HorizontalAlignment.Left);
                    cdvToValue.Columns.Add("Desc", 100, HorizontalAlignment.Left);

                    break;
                case eFromToType.RESOURCE:
                    lblConditionLabel.Text = "Resource";

                    cdvFromValue.Init();
                    CmnInitFunction.InitListView(cdvFromValue.GetListView);

                    cdvToValue.Init();
                    CmnInitFunction.InitListView(cdvToValue.GetListView);

                    break;
                case eFromToType.EVENT:
                    lblConditionLabel.Text = "Event";

                    cdvFromValue.Init();
                    CmnInitFunction.InitListView(cdvFromValue.GetListView);

                    cdvToValue.Init();
                    CmnInitFunction.InitListView(cdvToValue.GetListView);

                    break;
                case eFromToType.TABLE:
                    if (lblConditionLabel.Text.Trim() == "")
                    {
                        lblConditionLabel.Text = "Table";
                    }

                    cdvFromValue.Init();
                    CmnInitFunction.InitListView(cdvFromValue.GetListView);
                    cdvFromValue.Columns.Add("Code", 50, HorizontalAlignment.Left);
                    cdvFromValue.Columns.Add("Data", 100, HorizontalAlignment.Left);

                    cdvToValue.Init();
                    CmnInitFunction.InitListView(cdvToValue.GetListView);
                    cdvToValue.Columns.Add("Code", 50, HorizontalAlignment.Left);
                    cdvToValue.Columns.Add("Data", 100, HorizontalAlignment.Left);

                    break;
                case eFromToType.TEXT:
                    if (lblConditionLabel.Text.Trim() == "")
                    {
                        lblConditionLabel.Text = "Text";
                    }
                    break;
                case eFromToType.HQ_TABLE:
                    lblConditionLabel.Text = "Operation";

                    cdvFromValue.Init();
                    CmnInitFunction.InitListView(cdvFromValue.GetListView);
                    cdvFromValue.Columns.Add("Code", 50, HorizontalAlignment.Left);
                    cdvFromValue.Columns.Add("Data", 100, HorizontalAlignment.Left);

                    cdvToValue.Init();
                    CmnInitFunction.InitListView(cdvToValue.GetListView);
                    cdvToValue.Columns.Add("Code", 50, HorizontalAlignment.Left);
                    cdvToValue.Columns.Add("Data", 100, HorizontalAlignment.Left);

                    break;
                case eFromToType.CUSTOM:
                    if (lblConditionLabel.Text.Trim() == "")
                    {
                        lblConditionLabel.Text = "Custom";
                    }

                    cdvFromValue.Init();
                    CmnInitFunction.InitListView(cdvFromValue.GetListView);

                    cdvToValue.Init();
                    CmnInitFunction.InitListView(cdvToValue.GetListView);
                    
                    break;
            }
        }
    }
}