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
    /// <summary>
    /// 2010-04-19-임종우 : 원부자재 조회용 MATERIAL_TYPE 추가
    /// 2010-08-04-임종우 : Customer가 멀티 타입이 아닌 경우 PKG 클릭시 에러 발생 함에 따라 조건문 추가 함.
    /// 2010-10-14-임종우 : 설비명에 $가 붙어있는 더미 설비 제외 함.(김준용D 요청)
    /// 2010-12-13-임종우 : 삭제 된 설비는 제외.
    /// 2011-05-03-임종우 : GCM 테이블 공용 사용을 위해 추가 함.
    /// 2011-11-21-임종우 : RESOURCE(설비) 부분 추가 함.
    /// 2012-02-14-임종우 : MAJOR CODE 검색 기능 추가 함.
    /// </summary>
    public partial class udcCUSCondition : UserControl
    {
        public udcCUSCondition()
        {
            InitializeComponent();
        }

        #region "Variables"

        private bool b_mandatory_flag = false;
        private bool b_refuse_event_exec = false;
        private string s_factory_txt = "";
        private string s_table_txt = "";
        private string s_val_txt = "";
        private Miracom.SmartWeb.UI.Controls.udcCondition.eConditionype m_e_condition_type;

        private string s_query_txt = "";    // 직접 쿼리를 통해 값을 가져오도록 하기 위해 추가함. '08.12.17. 양형석
        private string s_code_txt = "";    // 직접 쿼리를 통해 값을 가져오도록 하기 위해 추가함. '08.12.17. 양형석
        private string s_value_txt = "";    // 직접 쿼리를 통해 값을 가져오도록 하기 위해 추가함. '08.12.17. 양형석
        
        // Add By John. Seo. 2008.12.17
        private bool IsDataChange = false;

        // 자신의 값이 변경시 다른컨트롤에 영향을 받을지 여부
        private bool _IsControlRef = false;

        // 자신의 값이 변경시 다른컨트롤을 초기화 시킬 컨트롤(최대4개까지만 지원)
        private Control _ForcInitControl1;
        private Control _ForcInitControl2;
        private Control _ForcInitControl3;
        private Control _ForcInitControl4;

        // 자신의 값 조회시 참조할 컨트롤 명.
        private Control _RefFactory;
        private Control _RefControl1;
        private Control _RefControl2;
        private Control _RefControl3;
        private Control _RefControl4;
        private Control _RefControl5;
        private Control _RefControl6;
        private Control _RefControl7;
        private Control _RefControl8;
        //Add. 20150526_BUMP Detail 검색 조건
        //private Control _RefControl9;
        //private Control _RefControl10;
        //private Control _RefControl11;

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
            try
			{
                StringBuilder sMySql = new StringBuilder();
                DataTable rtDataTable = new DataTable();
                ListViewItem itmX = null;
                string sTmpColumn = "";
                string sTmpValue = "";

                if (ValueButtonPressEvent != null)
                    ValueButtonPressEvent(this, e);

                if (bMultiSelect == true)
                {
                    if (IsDataChange == false || (cdvValue.Text != "ALL" && cdvValue.Text != ""))
                        return;
                }

                switch (m_e_condition_type)
                {
                   
                    case Miracom.SmartWeb.UI.Controls.udcCondition.eConditionype.FACTORY:
                        sMySql = new StringBuilder();

                        sMySql.Append("SELECT FACTORY Code, FAC_DESC Data" + "\n");
                        sMySql.Append("  FROM MWIPFACDEF" + "\n");
                        sMySql.Append(" WHERE FAC_GRP_5 = 'Y' " + "\n");
                        sMySql.Append(" ORDER BY FAC_GRP_4 ASC");

                        RptCUSListFunction.GetTableList(cdvValue.GetListView, sMySql.ToString(), "Code", "Data");
                        cdvValue.SelectedSubItemIndex = 0;

                        break;

                    // 2015-02-25 : HMKA1, HMKB1 FACTORY 조회용 FACTORY_ASSEMBLY 추가
                    case Miracom.SmartWeb.UI.Controls.udcCondition.eConditionype.FACTORY_ASSEMBLY:
                        sMySql = new StringBuilder();

                        sMySql.Append("SELECT FACTORY Code, FAC_DESC Data" + "\n");
                        sMySql.Append("  FROM MWIPFACDEF" + "\n");
                        sMySql.Append(" WHERE FACTORY IN ('" + GlobalVariable.gsAssyDefaultFactory + "', 'HMKB1') " + "\n");
                        sMySql.Append("   AND FAC_GRP_5 = 'Y' " + "\n");
                        sMySql.Append(" ORDER BY FAC_GRP_4 ASC");

                        RptCUSListFunction.GetTableList(cdvValue.GetListView, sMySql.ToString(), "Code", "Data");
                        cdvValue.SelectedSubItemIndex = 0;

                        break;
                    case Miracom.SmartWeb.UI.Controls.udcCondition.eConditionype.MATERIAL:
                        if (CmnFunction.Trim(sFactory) == "")
                        {
                            CmnFunction.ShowMsgBox(RptMessages.GetMessage("STD001", GlobalVariable.gcLanguage));
                            return;
                        }
                        RptCUSListFunction.GetMatList(cdvValue.GetListView, this.sFactory);
                        cdvValue.SelectedSubItemIndex = 0;
                        break;
                    case Miracom.SmartWeb.UI.Controls.udcCondition.eConditionype.FLOW:
                        if (CmnFunction.Trim(sFactory) == "")
                        {
                            CmnFunction.ShowMsgBox(RptMessages.GetMessage("STD001", GlobalVariable.gcLanguage));
                            return;
                        }
                        RptCUSListFunction.GetFlwList(cdvValue.GetListView, this.sFactory);
                        cdvValue.SelectedSubItemIndex = 0;
                        break;
                    case Miracom.SmartWeb.UI.Controls.udcCondition.eConditionype.OPER:
                        if (CmnFunction.Trim(sFactory) == "")
                        {
                            CmnFunction.ShowMsgBox(RptMessages.GetMessage("STD001", GlobalVariable.gcLanguage));
                            return;
                        }
                        RptCUSListFunction.GetOprList(cdvValue.GetListView, this.sFactory);
                        cdvValue.SelectedSubItemIndex = 0;
                        break;
                    case Miracom.SmartWeb.UI.Controls.udcCondition.eConditionype.RESOURCE:
                        if (CmnFunction.Trim(sFactory) == "")
                        {
                            CmnFunction.ShowMsgBox(RptMessages.GetMessage("STD001", GlobalVariable.gcLanguage));
                            return;
                        }
                        // 2011-11-21-임종우 : 설비 추가 함.
                        sMySql = new StringBuilder();

                        sMySql.Append("SELECT RES_ID Code, '' Data" + "\n");
                        sMySql.Append("  FROM MRASRESDEF" + "\n");
                        sMySql.Append(" WHERE FACTORY = '" + this.sFactory + "' " + "\n");
                        sMySql.Append("   AND DELETE_FLAG = ' '");
                        sMySql.Append("   AND RES_ID NOT LIKE '%$%'");
                        sMySql.Append("   AND RES_CMF_9 = 'Y'");
                        sMySql.Append("   AND RES_CMF_20 <= TO_CHAR(SYSDATE,'YYYYMMDD')");
                        sMySql.Append(" ORDER BY RES_ID");

                        RptCUSListFunction.GetTableList(cdvValue.GetListView, sMySql.ToString(), "Code", "Data");
                        cdvValue.SelectedSubItemIndex = 0;
                        
                        break;
                    case Miracom.SmartWeb.UI.Controls.udcCondition.eConditionype.EVENT:
                        if (CmnFunction.Trim(sFactory) == "")
                        {
                            CmnFunction.ShowMsgBox(RptMessages.GetMessage("STD001", GlobalVariable.gcLanguage));
                            return;
                        }
                        break;
                    case Miracom.SmartWeb.UI.Controls.udcCondition.eConditionype.TABLE:
                        #region " 쿼리를 직접 입력했을 경우 리스트박스에서 쓸 code, value 칼럼명 또한 입력받아야 함. 08.12.17. 양형석 "
                        if (CmnFunction.Trim(sDynamicQuery) != "")
                        {
                            if (CmnFunction.Trim(sCodeColumnName) == "" || CmnFunction.Trim(sValueColumnName) == "")
                            {
                                CmnFunction.ShowMsgBox(RptMessages.GetMessage("STD025", GlobalVariable.gcLanguage));
                                return;
                            }

                            RptCUSListFunction.GetTableList(cdvValue.GetListView, sDynamicQuery, sCodeColumnName, sValueColumnName);
                            cdvValue.SelectedSubItemIndex = 0;
                            break;
                        }
                        #endregion

                        if (CmnFunction.Trim(sFactory) == "")
                        {
                            CmnFunction.ShowMsgBox(RptMessages.GetMessage("STD001", GlobalVariable.gcLanguage));
                            return;
                        }
                        RptCUSListFunction.GetTableList(cdvValue.GetListView, this.sFactory, sTableName);
                        cdvValue.SelectedSubItemIndex = 0;
                        break;
                    case Miracom.SmartWeb.UI.Controls.udcCondition.eConditionype.HQ_TABLE:

                        if (this.RefFactory.Name == "")
                        {
                            CmnFunction.ShowMsgBox(RptMessages.GetMessage("STD029", GlobalVariable.gcLanguage));
                            return;
                        }

                        if (((udcCUSCondition)_RefFactory).Text == "")
                        {
                            CmnFunction.ShowMsgBox(RptMessages.GetMessage("STD001", GlobalVariable.gcLanguage));
                            return;
                        }

                        if (cdvValue.GetListView is ListView)
                        {
                            CmnInitFunction.InitListView((ListView)cdvValue.GetListView);
                        }

                        CmnFunction.oComm.SetUrl();

                        DataTable dt = null;
                        string sHQSql = "";

                        sMySql = new StringBuilder();
                        sMySql.Append("SELECT SQL_1" + "\n");
                        sMySql.Append("  FROM MGCMTBLDEF" + "\n");                        
                        sMySql.AppendFormat(" WHERE FACTORY    = '{0}' " + "\n", ((udcCUSCondition)_RefFactory).Text);
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
                                if (cdvValue.GetListView is ListView)
                                {
                                    itmX = new ListViewItem(rtDataTable.Rows[i][0].ToString().TrimEnd(), (int)SMALLICON_INDEX.IDX_OPER);
                                    if (rtDataTable.Columns.Count > 1 )                                        
                                        itmX.SubItems.Add(rtDataTable.Rows[i][1].ToString().TrimEnd());
                                    else
                                        itmX.SubItems.Add(" ".TrimEnd());

                                    ((ListView)cdvValue.GetListView).Items.Add(itmX);
                                }
                            }
                        }
                        if (rtDataTable.Rows.Count > 0)
                        {
                            rtDataTable.Dispose();
                        }
                        cdvValue.SelectedSubItemIndex = 0;

                        //RptCUSListFunction.GetTableList(cdvValue.GetListView, sHQSql, "Code", "Data");
                        //cdvValue.SelectedSubItemIndex = 0;

                        break;
                    case Miracom.SmartWeb.UI.Controls.udcCondition.eConditionype.AREA:
                        //RptCUSListFunction.GetTableList2(cdvValue.GetListView, sTableName);
                        

                        if (cdvValue.GetListView is ListView)
                        {
                            CmnInitFunction.InitListView((ListView)cdvValue.GetListView);
                        }

                        CmnFunction.oComm.SetUrl();

                        sMySql = new StringBuilder();

                        sMySql.Append("SELECT DISTINCT(KEY_1) AS AREA FROM MGCMTBLDAT WHERE TABLE_NAME = 'AREA' ORDER BY KEY_1");

                        rtDataTable = CmnFunction.oComm.GetFuncDataTable("DYNAMIC", sMySql.ToString());

                        if (rtDataTable.Rows.Count > 0)
                        {
                            for (int i = 0; i < rtDataTable.Rows.Count; i++)
                            {
                                if (cdvValue.GetListView is ListView)
                                {
                                    itmX = new ListViewItem(rtDataTable.Rows[i][0].ToString().TrimEnd(), (int)SMALLICON_INDEX.IDX_AREA);
                                    itmX.SubItems.Add(" ".TrimEnd());
                                    ((ListView)cdvValue.GetListView).Items.Add(itmX);
                                }
                            }
                        }
                        if (rtDataTable.Rows.Count > 0)
                        {
                            rtDataTable.Dispose();
                        }
                        cdvValue.SelectedSubItemIndex = 0;
                        break;

                    // 2010-04-19-임종우 : 원부자재 조회용 MATERIAL_TYPE 추가
                    case Miracom.SmartWeb.UI.Controls.udcCondition.eConditionype.MATERIAL_TYPE:

                        sMySql = new StringBuilder();
                        
                        sMySql.Append("SELECT KEY_1 Code, DATA_1 Data" + "\n");
                        sMySql.Append("  FROM MGCMTBLDAT" + "\n");
                        sMySql.Append(" WHERE 1=1 " + "\n");
                        sMySql.AppendFormat("   AND FACTORY = '{0}' " + "\n", ((udcCUSCondition)_RefFactory).Text);
                        sMySql.Append("   AND TABLE_NAME = 'MATERIAL_TYPE' " + "\n");
                        sMySql.Append("   AND DATA_2 = 'Y' " + "\n");
                        sMySql.Append(" ORDER BY KEY_1 ASC");

                        RptCUSListFunction.GetTableList(cdvValue.GetListView, sMySql.ToString(), "Code", "Data");
                        cdvValue.SelectedSubItemIndex = 0;

                        break;

                    case Miracom.SmartWeb.UI.Controls.udcCondition.eConditionype.RAS_TBL:
                        #region " MRASRESDEF테이블의 GRP정보."
                        
                        sMySql = new StringBuilder();

                        if (this.RefFactory.Name == "")
                        {
                            CmnFunction.ShowMsgBox(RptMessages.GetMessage("STD029", GlobalVariable.gcLanguage));
                            return;
                        }

                        if (((udcCUSCondition)_RefFactory).Text == "")
                        {
                            CmnFunction.ShowMsgBox(RptMessages.GetMessage("STD001", GlobalVariable.gcLanguage));
                            return;
                        }

                        if (CmnFunction.Trim(sTableName) == "")
                        {
                            CmnFunction.ShowMsgBox(RptMessages.GetMessage("STD025", GlobalVariable.gcLanguage));
                            return;
                        }

                        InitControl();

                        switch ( CmnFunction.Trim(sTableName) )
                        {
                            case  "TEAM":                        
                                sMySql.Append("SELECT UNIQUE A.RES_GRP_1 Code, B.DATA_1 Data" + "\n");
                                sMySql.Append("  FROM MRASRESDEF A, MGCMTBLDAT B" + "\n");
                                sMySql.Append(" WHERE A.FACTORY = B.FACTORY" + "\n");
                                sMySql.Append("   AND A.RES_GRP_1 = B.KEY_1" + "\n");
                                sMySql.Append("   AND B.TABLE_NAME = 'H_DEPARTMENT'" + "\n");
                                if (((udcCUSCondition)_RefFactory).SelectedValueToQueryString == null ||
                                    ((udcCUSCondition)_RefFactory).SelectedValueToQueryString == "")
                                    sMySql.AppendFormat("   AND A.FACTORY ='{0}' " + "\n", ((udcCUSCondition)_RefFactory).Text);                                
                                else 
                                    sMySql.AppendFormat("   AND A.FACTORY {0} " + "\n", ((udcCUSCondition)_RefFactory).SelectedValueToQueryString);
                                sMySql.Append(" ORDER BY 1,2");
                                break;

                            case "PART":
                                sMySql.Append("SELECT UNIQUE A.RES_GRP_2 Code, B.DATA_1 Data" + "\n");
                                sMySql.Append("  FROM MRASRESDEF A, MGCMTBLDAT B" + "\n");
                                sMySql.Append(" WHERE A.FACTORY = B.FACTORY" + "\n");
                                sMySql.Append("   AND A.RES_GRP_2 = B.KEY_1" + "\n");
                                sMySql.Append("   AND B.TABLE_NAME = 'H_DEPARTMENT'" + "\n");
                                if (((udcCUSCondition)_RefFactory).SelectedValueToQueryString == null ||
                                    ((udcCUSCondition)_RefFactory).SelectedValueToQueryString == "")
                                    sMySql.AppendFormat("   AND A.FACTORY = '{0}' " + "\n", ((udcCUSCondition)_RefFactory).Text);
                                else
                                    sMySql.AppendFormat("   AND A.FACTORY {0} " + "\n", ((udcCUSCondition)_RefFactory).SelectedValueToQueryString);

                                if (_RefControl1 != null)
                                {
                                    //sTmp = ((udcCUSCondition)_RefControl1).cdvValue.SelectedValueToQueryText;

                                    sTmpColumn = ((udcCUSCondition)_RefControl1).sValueColumnName;                       
                                    sTmpValue = ((udcCUSCondition)_RefControl1).SelectedValueToQueryString;
                                    sMySql.AppendFormat("   AND A.{0} {1} " + "\n", sTmpColumn, sTmpValue);
                                }

                                sMySql.Append(" ORDER BY 1,2");
                                break;

                            case "TYPE":                        
                                sMySql.Append("SELECT UNIQUE A.RES_GRP_3 Code, ' ' Data" + "\n");
                                sMySql.Append("  FROM MRASRESDEF A" + "\n");
                                sMySql.Append(" WHERE 1 = 1 " + "\n");

                                // 2010-12-13-임종우 : 삭제 된 설비는 제외.
                                sMySql.Append("   AND DELETE_FLAG = ' ' " + "\n"); 
                                
                                if (((udcCUSCondition)_RefFactory).SelectedValueToQueryString == null ||
                                    ((udcCUSCondition)_RefFactory).SelectedValueToQueryString == "")
                                    sMySql.AppendFormat("   AND A.FACTORY = '{0}' " + "\n", ((udcCUSCondition)_RefFactory).Text);
                                else
                                    sMySql.AppendFormat("   AND A.FACTORY {0} " + "\n", ((udcCUSCondition)_RefFactory).SelectedValueToQueryString);
                                
                                if (_RefControl1 != null)
                                {
                                    sTmpColumn = ((udcCUSCondition)_RefControl1).sValueColumnName;
                                    sTmpValue = ((udcCUSCondition)_RefControl1).SelectedValueToQueryString;
                                    sMySql.AppendFormat("   AND A.{0} {1} " + "\n", sTmpColumn, sTmpValue);
                                }

                                if (_RefControl2 != null)
                                {
                                    sTmpColumn = ((udcCUSCondition)_RefControl2).sValueColumnName;
                                    sTmpValue = ((udcCUSCondition)_RefControl2).SelectedValueToQueryString;
                                    sMySql.AppendFormat("   AND A.{0} {1} " + "\n", sTmpColumn, sTmpValue);
                                }

                                sMySql.Append(" ORDER BY 1,2");
                                break;

                            case "MAKER":                        
                                sMySql.Append("SELECT UNIQUE A.RES_GRP_5 Code, ' ' Data" + "\n");
                                sMySql.Append("  FROM MRASRESDEF A" + "\n");
                                sMySql.Append(" WHERE 1 = 1 " + "\n");

                                if (((udcCUSCondition)_RefFactory).SelectedValueToQueryString == null ||
                                    ((udcCUSCondition)_RefFactory).SelectedValueToQueryString == "")
                                    sMySql.AppendFormat("   AND A.FACTORY = '{0}' " + "\n", ((udcCUSCondition)_RefFactory).Text);
                                else
                                    sMySql.AppendFormat("   AND A.FACTORY {0} " + "\n", ((udcCUSCondition)_RefFactory).SelectedValueToQueryString);

                                if (_RefControl1 != null)
                                {
                                    sTmpColumn = ((udcCUSCondition)_RefControl1).sValueColumnName;
                                    sTmpValue = ((udcCUSCondition)_RefControl1).SelectedValueToQueryString;
                                    sMySql.AppendFormat("   AND A.{0} {1} " + "\n", sTmpColumn, sTmpValue);
                                }

                                if (_RefControl2 != null)
                                {
                                    sTmpColumn = ((udcCUSCondition)_RefControl2).sValueColumnName;
                                    sTmpValue = ((udcCUSCondition)_RefControl2).SelectedValueToQueryString;
                                    sMySql.AppendFormat("   AND A.{0} {1} " + "\n", sTmpColumn, sTmpValue);
                                }

                                if (_RefControl3 != null)
                                {
                                    sTmpColumn = ((udcCUSCondition)_RefControl3).sValueColumnName;
                                    sTmpValue = ((udcCUSCondition)_RefControl3).SelectedValueToQueryString;
                                    sMySql.AppendFormat("   AND A.{0} {1} " + "\n", sTmpColumn, sTmpValue);
                                }

                                sMySql.Append(" ORDER BY 1,2");
                                break;
                                
                            case "MODEL":                        
                                sMySql.Append("SELECT UNIQUE A.RES_GRP_6 Code, ' ' Data" + "\n");
                                sMySql.Append("  FROM MRASRESDEF A" + "\n");
                                sMySql.Append(" WHERE 1 = 1 " + "\n");

                                if (((udcCUSCondition)_RefFactory).SelectedValueToQueryString == null ||
                                    ((udcCUSCondition)_RefFactory).SelectedValueToQueryString == "")
                                    sMySql.AppendFormat("   AND A.FACTORY = '{0}' " + "\n", ((udcCUSCondition)_RefFactory).Text);
                                else
                                    sMySql.AppendFormat("   AND A.FACTORY {0} " + "\n", ((udcCUSCondition)_RefFactory).SelectedValueToQueryString);

                                if (_RefControl1 != null)
                                {
                                    sTmpColumn = ((udcCUSCondition)_RefControl1).sValueColumnName;
                                    sTmpValue = ((udcCUSCondition)_RefControl1).SelectedValueToQueryString;
                                    sMySql.AppendFormat("   AND A.{0} {1} " + "\n", sTmpColumn, sTmpValue);
                                }

                                if (_RefControl2 != null)
                                {
                                    sTmpColumn = ((udcCUSCondition)_RefControl2).sValueColumnName;
                                    sTmpValue = ((udcCUSCondition)_RefControl2).SelectedValueToQueryString;
                                    sMySql.AppendFormat("   AND A.{0} {1} " + "\n", sTmpColumn, sTmpValue);
                                }

                                if (_RefControl3 != null)
                                {
                                    sTmpColumn = ((udcCUSCondition)_RefControl3).sValueColumnName;
                                    sTmpValue = ((udcCUSCondition)_RefControl3).SelectedValueToQueryString;
                                    sMySql.AppendFormat("   AND A.{0} {1} " + "\n", sTmpColumn, sTmpValue);
                                }

                                if (_RefControl4 != null)
                                {
                                    sTmpColumn = ((udcCUSCondition)_RefControl4).sValueColumnName;
                                    sTmpValue = ((udcCUSCondition)_RefControl4).SelectedValueToQueryString;
                                    sMySql.AppendFormat("   AND A.{0} {1} " + "\n", sTmpColumn, sTmpValue);
                                }

                                sMySql.Append(" ORDER BY 1,2");
                                break;

                            case "RES":                        
                                sMySql.Append("SELECT A.RES_ID Code, ' ' Data" + "\n");
                                sMySql.Append("  FROM MRASRESDEF A" + "\n");
                                sMySql.Append(" WHERE 1 = 1 " + "\n");
                                sMySql.Append("   AND DELETE_FLAG = ' ' " + "\n");

                                // 2010-10-14-임종우 : 설비명에 $가 붙어있는 더미 설비 제외 함.(김준용D 요청)
                                sMySql.Append("   AND RES_ID NOT LIKE '%$%' " + "\n");

                                if (((udcCUSCondition)_RefFactory).SelectedValueToQueryString == null ||
                                    ((udcCUSCondition)_RefFactory).SelectedValueToQueryString == "")
                                    sMySql.AppendFormat("   AND A.FACTORY = '{0}' " + "\n", ((udcCUSCondition)_RefFactory).Text);
                                else
                                    sMySql.AppendFormat("   AND A.FACTORY {0} " + "\n", ((udcCUSCondition)_RefFactory).SelectedValueToQueryString);
                                
                                if (_RefControl1 != null)
                                {
                                    sTmpColumn = ((udcCUSCondition)_RefControl1).sValueColumnName;
                                    sTmpValue = ((udcCUSCondition)_RefControl1).SelectedValueToQueryString;
                                    sMySql.AppendFormat("   AND A.{0} {1} " + "\n", sTmpColumn, sTmpValue);
                                }

                                if (_RefControl2 != null)
                                {
                                    sTmpColumn = ((udcCUSCondition)_RefControl2).sValueColumnName;
                                    sTmpValue = ((udcCUSCondition)_RefControl2).SelectedValueToQueryString;
                                    sMySql.AppendFormat("   AND A.{0} {1} " + "\n", sTmpColumn, sTmpValue);
                                }

                                if (_RefControl3 != null)
                                {
                                    sTmpColumn = ((udcCUSCondition)_RefControl3).sValueColumnName;
                                    sTmpValue = ((udcCUSCondition)_RefControl3).SelectedValueToQueryString;
                                    sMySql.AppendFormat("   AND A.{0} {1} " + "\n", sTmpColumn, sTmpValue);
                                }

                                if (_RefControl4 != null)
                                {
                                    sTmpColumn = ((udcCUSCondition)_RefControl4).sValueColumnName;
                                    sTmpValue = ((udcCUSCondition)_RefControl4).SelectedValueToQueryString;
                                    sMySql.AppendFormat("   AND A.{0} {1} " + "\n", sTmpColumn, sTmpValue);
                                }

                                if (_RefControl5 != null)
                                {
                                    sTmpColumn = ((udcCUSCondition)_RefControl5).sValueColumnName;
                                    sTmpValue = ((udcCUSCondition)_RefControl5).SelectedValueToQueryString;
                                    sMySql.AppendFormat("   AND A.{0} {1} " + "\n", sTmpColumn, sTmpValue);
                                }

                                sMySql.Append(" ORDER BY 1,2");
                                break;

                            case "OPERATOR":                        
                                sMySql.Append("SELECT UNIQUE USER_ID Code, USER_DESC Data FROM CRASRESUSR WHERE 1 = 1 " + "\n");
                                sMySql.Append("   AND RES_ID IN (" + "\n");
                                sMySql.Append("SELECT A.RES_ID " + "\n");
                                sMySql.Append("  FROM MRASRESDEF A" + "\n");
                                sMySql.Append(" WHERE 1 = 1 " + "\n");

                                if (((udcCUSCondition)_RefFactory).SelectedValueToQueryString == null ||
                                    ((udcCUSCondition)_RefFactory).SelectedValueToQueryString == "")
                                    sMySql.AppendFormat("   AND A.FACTORY = '{0}' " + "\n", ((udcCUSCondition)_RefFactory).Text);
                                else
                                    sMySql.AppendFormat("   AND A.FACTORY {0} " + "\n", ((udcCUSCondition)_RefFactory).SelectedValueToQueryString);

                                if (_RefControl1 != null)
                                {
                                    sTmpColumn = ((udcCUSCondition)_RefControl1).sValueColumnName;
                                    sTmpValue = ((udcCUSCondition)_RefControl1).SelectedValueToQueryString;
                                    sMySql.AppendFormat("   AND A.{0} {1} " + "\n", sTmpColumn, sTmpValue);
                                }

                                if (_RefControl2 != null)
                                {
                                    sTmpColumn = ((udcCUSCondition)_RefControl2).sValueColumnName;
                                    sTmpValue = ((udcCUSCondition)_RefControl2).SelectedValueToQueryString;
                                    sMySql.AppendFormat("   AND A.{0} {1} " + "\n", sTmpColumn, sTmpValue);
                                }

                                if (_RefControl3 != null)
                                {
                                    sTmpColumn = ((udcCUSCondition)_RefControl3).sValueColumnName;
                                    sTmpValue = ((udcCUSCondition)_RefControl3).SelectedValueToQueryString;
                                    sMySql.AppendFormat("   AND A.{0} {1} " + "\n", sTmpColumn, sTmpValue);
                                }

                                if (_RefControl4 != null)
                                {
                                    sTmpColumn = ((udcCUSCondition)_RefControl4).sValueColumnName;
                                    sTmpValue = ((udcCUSCondition)_RefControl4).SelectedValueToQueryString;
                                    sMySql.AppendFormat("   AND A.{0} {1} " + "\n", sTmpColumn, sTmpValue);
                                }

                                if (_RefControl5 != null)
                                {
                                    sTmpColumn = ((udcCUSCondition)_RefControl5).sValueColumnName;
                                    sTmpValue = ((udcCUSCondition)_RefControl5).SelectedValueToQueryString;
                                    sMySql.AppendFormat("   AND A.{0} {1} " + "\n", sTmpColumn, sTmpValue);
                                }

                                if (_RefControl6 != null)
                                {
                                    sTmpColumn = ((udcCUSCondition)_RefControl6).sValueColumnName;
                                    sTmpValue = ((udcCUSCondition)_RefControl6).SelectedValueToQueryString;
                                    sMySql.AppendFormat("   AND A.{0} {1} " + "\n", sTmpColumn, sTmpValue);
                                }

                                sMySql.Append(") ORDER BY 1,2");
                                break;
                        }

                        if (sMySql.ToString() != "")
                        {
                            RptCUSListFunction.GetTableList(cdvValue.GetListView, sMySql.ToString(), "Code", "Data");
                            cdvValue.SelectedSubItemIndex = 0;
                        }
                        break;
                         
                        #endregion
                    case Miracom.SmartWeb.UI.Controls.udcCondition.eConditionype.WIP_TBL:
                        #region " MWIPMATDEF테이블의 GRP정보."

                        sMySql = new StringBuilder();

                        if (this.RefFactory.Name == "")
                        {
                            CmnFunction.ShowMsgBox(RptMessages.GetMessage("STD029", GlobalVariable.gcLanguage));
                            return;
                        }

                        if (((udcCUSCondition)_RefFactory).Text == "")
                        {
                            CmnFunction.ShowMsgBox(RptMessages.GetMessage("STD001", GlobalVariable.gcLanguage));
                            return;
                        }

                        if (CmnFunction.Trim(sTableName) == "")
                        {
                            CmnFunction.ShowMsgBox(RptMessages.GetMessage("STD025", GlobalVariable.gcLanguage));
                            return;
                        }

                        InitControl();

                        switch (CmnFunction.Trim(sTableName))
                        {
                            case "CUSTOMER":
                                sMySql.Append("SELECT UNIQUE A.MAT_GRP_1 Code, NVL(B.DATA_1, ' ') Data" + "\n");
                                sMySql.Append("  FROM MWIPMATDEF A, MGCMTBLDAT B" + "\n");
                                sMySql.Append(" WHERE A.FACTORY = B.FACTORY(+)" + "\n");
                                sMySql.Append("   AND A.MAT_GRP_1 = B.KEY_1(+)" + "\n");
                                sMySql.Append("   AND B.TABLE_NAME(+) = 'H_CUSTOMER'" + "\n");
                                sMySql.Append("   AND A.MAT_TYPE = 'FG'" + "\n");

                                if (((udcCUSCondition)_RefFactory).SelectedValueToQueryString == null ||
                                    ((udcCUSCondition)_RefFactory).SelectedValueToQueryString == "")
                                    sMySql.AppendFormat("   AND A.FACTORY = '{0}' " + "\n", ((udcCUSCondition)_RefFactory).Text);
                                else
                                    sMySql.AppendFormat("   AND A.FACTORY {0} " + "\n", ((udcCUSCondition)_RefFactory).SelectedValueToQueryString);

                                sMySql.Append(" ORDER BY 1,2");
                                break;

                            case "FAMILY":
                                sMySql.Append("SELECT UNIQUE A.MAT_GRP_2 Code, NVL(B.DATA_1, ' ') Data" + "\n");
                                sMySql.Append("  FROM MWIPMATDEF A, MGCMTBLDAT B" + "\n");
                                sMySql.Append(" WHERE A.FACTORY = B.FACTORY(+)" + "\n");
                                sMySql.Append("   AND A.MAT_GRP_2 = B.KEY_1(+)" + "\n");
                                sMySql.Append("   AND B.TABLE_NAME(+) = 'H_FAMILY'" + "\n");
                                sMySql.Append("   AND A.MAT_TYPE = 'FG'" + "\n");

                                if (((udcCUSCondition)_RefFactory).SelectedValueToQueryString == null ||
                                    ((udcCUSCondition)_RefFactory).SelectedValueToQueryString == "")
                                    sMySql.AppendFormat("   AND A.FACTORY = '{0}' " + "\n", ((udcCUSCondition)_RefFactory).Text);
                                else
                                    sMySql.AppendFormat("   AND A.FACTORY {0} " + "\n", ((udcCUSCondition)_RefFactory).SelectedValueToQueryString);


                                if (_RefControl1 != null)
                                {
                                    //sTmp = ((udcCUSCondition)_RefControl1).cdvValue.SelectedValueToQueryText;

                                    sTmpColumn = ((udcCUSCondition)_RefControl1).sValueColumnName;
                                    sTmpValue = ((udcCUSCondition)_RefControl1).SelectedValueToQueryString;
                                    sMySql.AppendFormat("   AND A.{0} {1} " + "\n", sTmpColumn, sTmpValue);
                                }

                                sMySql.Append(" ORDER BY 1,2");
                                break;

                            case "PACKAGE":
                                sMySql.Append("SELECT UNIQUE A.MAT_GRP_3 Code, NVL(B.DATA_1, ' ') Data" + "\n");
                                sMySql.Append("  FROM MWIPMATDEF A, MGCMTBLDAT B" + "\n");
                                sMySql.Append(" WHERE A.FACTORY = B.FACTORY(+)" + "\n");
                                sMySql.Append("   AND A.MAT_GRP_3 = B.KEY_1(+)" + "\n");
                                sMySql.Append("   AND B.TABLE_NAME(+) = 'H_PACKAGE'" + "\n");
                                sMySql.Append("   AND A.MAT_TYPE = 'FG'" + "\n");

                                if (((udcCUSCondition)_RefFactory).SelectedValueToQueryString == null ||
                                    ((udcCUSCondition)_RefFactory).SelectedValueToQueryString == "")
                                    sMySql.AppendFormat("   AND A.FACTORY = '{0}' " + "\n", ((udcCUSCondition)_RefFactory).Text);
                                else
                                    sMySql.AppendFormat("   AND A.FACTORY {0} " + "\n", ((udcCUSCondition)_RefFactory).SelectedValueToQueryString);

                                if (_RefControl1 != null)
                                {
                                    sTmpColumn = ((udcCUSCondition)_RefControl1).sValueColumnName;
                                    sTmpValue = ((udcCUSCondition)_RefControl1).SelectedValueToQueryString;

                                    // 2010-08-04-임종우 : Customer가 멀티 타입이 아닌 경우 PKG 클릭시 에러 발생 함에 따라 조건문 추가 함.
                                    if (sTmpColumn != "" && sTmpValue != "")
                                    {
                                        sMySql.AppendFormat("   AND A.{0} {1} " + "\n", sTmpColumn, sTmpValue);
                                    }
                                }

                                if (_RefControl2 != null)
                                {
                                    sTmpColumn = ((udcCUSCondition)_RefControl2).sValueColumnName;
                                    sTmpValue = ((udcCUSCondition)_RefControl2).SelectedValueToQueryString;
                                    sMySql.AppendFormat("   AND A.{0} {1} " + "\n", sTmpColumn, sTmpValue);
                                }

                                sMySql.Append(" ORDER BY 1,2");
                                break;

                            case "DEV_TYPE_1":
                                sMySql.Append("SELECT UNIQUE A.MAT_GRP_4 Code, NVL(B.DATA_1, ' ') Data" + "\n");
                                sMySql.Append("  FROM MWIPMATDEF A, MGCMTBLDAT B" + "\n");
                                sMySql.Append(" WHERE A.FACTORY = B.FACTORY(+)" + "\n");
                                sMySql.Append("   AND A.MAT_GRP_4 = B.KEY_1(+)" + "\n");
                                sMySql.Append("   AND B.TABLE_NAME(+) = 'H_DEV_TYPE_1'" + "\n");
                                sMySql.Append("   AND A.MAT_TYPE = 'FG'" + "\n");

                                if (((udcCUSCondition)_RefFactory).SelectedValueToQueryString == null ||
                                    ((udcCUSCondition)_RefFactory).SelectedValueToQueryString == "")
                                    sMySql.AppendFormat("   AND A.FACTORY = '{0}' " + "\n", ((udcCUSCondition)_RefFactory).Text);
                                else
                                    sMySql.AppendFormat("   AND A.FACTORY {0} " + "\n", ((udcCUSCondition)_RefFactory).SelectedValueToQueryString);

                                if (_RefControl1 != null)
                                {
                                    sTmpColumn = ((udcCUSCondition)_RefControl1).sValueColumnName;
                                    sTmpValue = ((udcCUSCondition)_RefControl1).SelectedValueToQueryString;
                                    sMySql.AppendFormat("   AND A.{0} {1} " + "\n", sTmpColumn, sTmpValue);
                                }

                                if (_RefControl2 != null)
                                {
                                    sTmpColumn = ((udcCUSCondition)_RefControl2).sValueColumnName;
                                    sTmpValue = ((udcCUSCondition)_RefControl2).SelectedValueToQueryString;
                                    sMySql.AppendFormat("   AND A.{0} {1} " + "\n", sTmpColumn, sTmpValue);
                                }

                                if (_RefControl3 != null)
                                {
                                    sTmpColumn = ((udcCUSCondition)_RefControl3).sValueColumnName;
                                    sTmpValue = ((udcCUSCondition)_RefControl3).SelectedValueToQueryString;
                                    sMySql.AppendFormat("   AND A.{0} {1} " + "\n", sTmpColumn, sTmpValue);
                                }

                                sMySql.Append(" ORDER BY 1,2");
                                break;

                            case "DEV_TYPE_2":
                                sMySql.Append("SELECT UNIQUE A.MAT_GRP_5 Code, NVL(B.DATA_1, ' ') Data" + "\n");
                                sMySql.Append("  FROM MWIPMATDEF A, MGCMTBLDAT B" + "\n");
                                sMySql.Append(" WHERE A.FACTORY = B.FACTORY(+)" + "\n");
                                sMySql.Append("   AND A.MAT_GRP_5 = B.KEY_1(+)" + "\n");
                                sMySql.Append("   AND B.TABLE_NAME(+) = 'H_DEV_TYPE_2'" + "\n");
                                sMySql.Append("   AND A.MAT_TYPE = 'FG'" + "\n");

                                if (((udcCUSCondition)_RefFactory).SelectedValueToQueryString == null ||
                                    ((udcCUSCondition)_RefFactory).SelectedValueToQueryString == "")
                                    sMySql.AppendFormat("   AND A.FACTORY = '{0}' " + "\n", ((udcCUSCondition)_RefFactory).Text);
                                else
                                    sMySql.AppendFormat("   AND A.FACTORY {0} " + "\n", ((udcCUSCondition)_RefFactory).SelectedValueToQueryString);

                                if (_RefControl1 != null)
                                {
                                    sTmpColumn = ((udcCUSCondition)_RefControl1).sValueColumnName;
                                    sTmpValue = ((udcCUSCondition)_RefControl1).SelectedValueToQueryString;
                                    sMySql.AppendFormat("   AND A.{0} {1} " + "\n", sTmpColumn, sTmpValue);
                                }

                                if (_RefControl2 != null)
                                {
                                    sTmpColumn = ((udcCUSCondition)_RefControl2).sValueColumnName;
                                    sTmpValue = ((udcCUSCondition)_RefControl2).SelectedValueToQueryString;
                                    sMySql.AppendFormat("   AND A.{0} {1} " + "\n", sTmpColumn, sTmpValue);
                                }

                                if (_RefControl3 != null)
                                {
                                    sTmpColumn = ((udcCUSCondition)_RefControl3).sValueColumnName;
                                    sTmpValue = ((udcCUSCondition)_RefControl3).SelectedValueToQueryString;
                                    sMySql.AppendFormat("   AND A.{0} {1} " + "\n", sTmpColumn, sTmpValue);
                                }

                                if (_RefControl4 != null)
                                {
                                    sTmpColumn = ((udcCUSCondition)_RefControl4).sValueColumnName;
                                    sTmpValue = ((udcCUSCondition)_RefControl4).SelectedValueToQueryString;
                                    sMySql.AppendFormat("   AND A.{0} {1} " + "\n", sTmpColumn, sTmpValue);
                                }

                                sMySql.Append(" ORDER BY 1,2");
                                break;

                            case "LEAD_COUNT":
                                sMySql.Append("SELECT UNIQUE A.MAT_GRP_6 Code, NVL(B.DATA_1, ' ') Data" + "\n");
                                sMySql.Append("  FROM MWIPMATDEF A, MGCMTBLDAT B" + "\n");
                                sMySql.Append(" WHERE A.FACTORY = B.FACTORY(+)" + "\n");
                                sMySql.Append("   AND A.MAT_GRP_6 = B.KEY_1(+)" + "\n");
                                sMySql.Append("   AND B.TABLE_NAME(+) = 'H_LEAD_COUNT'" + "\n");
                                sMySql.Append("   AND A.MAT_TYPE = 'FG'" + "\n");

                                if (((udcCUSCondition)_RefFactory).SelectedValueToQueryString == null ||
                                    ((udcCUSCondition)_RefFactory).SelectedValueToQueryString == "")
                                    sMySql.AppendFormat("   AND A.FACTORY = '{0}' " + "\n", ((udcCUSCondition)_RefFactory).Text);
                                else
                                    sMySql.AppendFormat("   AND A.FACTORY {0} " + "\n", ((udcCUSCondition)_RefFactory).SelectedValueToQueryString);

                                if (_RefControl1 != null)
                                {
                                    sTmpColumn = ((udcCUSCondition)_RefControl1).sValueColumnName;
                                    sTmpValue = ((udcCUSCondition)_RefControl1).SelectedValueToQueryString;
                                    sMySql.AppendFormat("   AND A.{0} {1} " + "\n", sTmpColumn, sTmpValue);
                                }

                                if (_RefControl2 != null)
                                {
                                    sTmpColumn = ((udcCUSCondition)_RefControl2).sValueColumnName;
                                    sTmpValue = ((udcCUSCondition)_RefControl2).SelectedValueToQueryString;
                                    sMySql.AppendFormat("   AND A.{0} {1} " + "\n", sTmpColumn, sTmpValue);
                                }

                                if (_RefControl3 != null)
                                {
                                    sTmpColumn = ((udcCUSCondition)_RefControl3).sValueColumnName;
                                    sTmpValue = ((udcCUSCondition)_RefControl3).SelectedValueToQueryString;
                                    sMySql.AppendFormat("   AND A.{0} {1} " + "\n", sTmpColumn, sTmpValue);
                                }

                                if (_RefControl4 != null)
                                {
                                    sTmpColumn = ((udcCUSCondition)_RefControl4).sValueColumnName;
                                    sTmpValue = ((udcCUSCondition)_RefControl4).SelectedValueToQueryString;
                                    sMySql.AppendFormat("   AND A.{0} {1} " + "\n", sTmpColumn, sTmpValue);
                                }

                                if (_RefControl5 != null)
                                {
                                    sTmpColumn = ((udcCUSCondition)_RefControl5).sValueColumnName;
                                    sTmpValue = ((udcCUSCondition)_RefControl5).SelectedValueToQueryString;
                                    sMySql.AppendFormat("   AND A.{0} {1} " + "\n", sTmpColumn, sTmpValue);
                                }

                                sMySql.Append(" ORDER BY 1,2");
                                break;

                            case "DENSITY":
                                sMySql.Append("SELECT UNIQUE A.MAT_GRP_7 Code, NVL(B.DATA_1, ' ') Data" + "\n");
                                sMySql.Append("  FROM MWIPMATDEF A, MGCMTBLDAT B" + "\n");
                                sMySql.Append(" WHERE A.FACTORY = B.FACTORY(+)" + "\n");
                                sMySql.Append("   AND A.MAT_GRP_7 = B.KEY_1(+)" + "\n");
                                sMySql.Append("   AND B.TABLE_NAME(+) = 'H_DENSITY'" + "\n");
                                sMySql.Append("   AND A.MAT_TYPE = 'FG'" + "\n");

                                if (((udcCUSCondition)_RefFactory).SelectedValueToQueryString == null ||
                                    ((udcCUSCondition)_RefFactory).SelectedValueToQueryString == "")
                                    sMySql.AppendFormat("   AND A.FACTORY = '{0}' " + "\n", ((udcCUSCondition)_RefFactory).Text);
                                else
                                    sMySql.AppendFormat("   AND A.FACTORY {0} " + "\n", ((udcCUSCondition)_RefFactory).SelectedValueToQueryString);

                                if (_RefControl1 != null)
                                {
                                    sTmpColumn = ((udcCUSCondition)_RefControl1).sValueColumnName;
                                    sTmpValue = ((udcCUSCondition)_RefControl1).SelectedValueToQueryString;
                                    sMySql.AppendFormat("   AND A.{0} {1} " + "\n", sTmpColumn, sTmpValue);
                                }

                                if (_RefControl2 != null)
                                {
                                    sTmpColumn = ((udcCUSCondition)_RefControl2).sValueColumnName;
                                    sTmpValue = ((udcCUSCondition)_RefControl2).SelectedValueToQueryString;
                                    sMySql.AppendFormat("   AND A.{0} {1} " + "\n", sTmpColumn, sTmpValue);
                                }

                                if (_RefControl3 != null)
                                {
                                    sTmpColumn = ((udcCUSCondition)_RefControl3).sValueColumnName;
                                    sTmpValue = ((udcCUSCondition)_RefControl3).SelectedValueToQueryString;
                                    sMySql.AppendFormat("   AND A.{0} {1} " + "\n", sTmpColumn, sTmpValue);
                                }

                                if (_RefControl4 != null)
                                {
                                    sTmpColumn = ((udcCUSCondition)_RefControl4).sValueColumnName;
                                    sTmpValue = ((udcCUSCondition)_RefControl4).SelectedValueToQueryString;
                                    sMySql.AppendFormat("   AND A.{0} {1} " + "\n", sTmpColumn, sTmpValue);
                                }

                                if (_RefControl5 != null)
                                {
                                    sTmpColumn = ((udcCUSCondition)_RefControl5).sValueColumnName;
                                    sTmpValue = ((udcCUSCondition)_RefControl5).SelectedValueToQueryString;
                                    sMySql.AppendFormat("   AND A.{0} {1} " + "\n", sTmpColumn, sTmpValue);
                                }

                                if (_RefControl6 != null)
                                {
                                    sTmpColumn = ((udcCUSCondition)_RefControl6).sValueColumnName;
                                    sTmpValue = ((udcCUSCondition)_RefControl6).SelectedValueToQueryString;
                                    sMySql.AppendFormat("   AND A.{0} {1} " + "\n", sTmpColumn, sTmpValue);
                                }

                                sMySql.Append(" ORDER BY 1,2");
                                break;

                            case "GENERATION":
                                sMySql.Append("SELECT UNIQUE A.MAT_GRP_8 Code, NVL(B.DATA_1, ' ') Data" + "\n");
                                sMySql.Append("  FROM MWIPMATDEF A, MGCMTBLDAT B" + "\n");
                                sMySql.Append(" WHERE A.FACTORY = B.FACTORY(+)" + "\n");
                                sMySql.Append("   AND A.MAT_GRP_8 = B.KEY_1(+)" + "\n");
                                sMySql.Append("   AND B.TABLE_NAME(+) = 'H_GENERATION'" + "\n");
                                sMySql.Append("   AND A.MAT_TYPE = 'FG'" + "\n");

                                if (((udcCUSCondition)_RefFactory).SelectedValueToQueryString == null ||
                                    ((udcCUSCondition)_RefFactory).SelectedValueToQueryString == "")
                                    sMySql.AppendFormat("   AND A.FACTORY = '{0}' " + "\n", ((udcCUSCondition)_RefFactory).Text);
                                else
                                    sMySql.AppendFormat("   AND A.FACTORY {0} " + "\n", ((udcCUSCondition)_RefFactory).SelectedValueToQueryString);

                                if (_RefControl1 != null)
                                {
                                    sTmpColumn = ((udcCUSCondition)_RefControl1).sValueColumnName;
                                    sTmpValue = ((udcCUSCondition)_RefControl1).SelectedValueToQueryString;
                                    sMySql.AppendFormat("   AND A.{0} {1} " + "\n", sTmpColumn, sTmpValue);
                                }

                                if (_RefControl2 != null)
                                {
                                    sTmpColumn = ((udcCUSCondition)_RefControl2).sValueColumnName;
                                    sTmpValue = ((udcCUSCondition)_RefControl2).SelectedValueToQueryString;
                                    sMySql.AppendFormat("   AND A.{0} {1} " + "\n", sTmpColumn, sTmpValue);
                                }

                                if (_RefControl3 != null)
                                {
                                    sTmpColumn = ((udcCUSCondition)_RefControl3).sValueColumnName;
                                    sTmpValue = ((udcCUSCondition)_RefControl3).SelectedValueToQueryString;
                                    sMySql.AppendFormat("   AND A.{0} {1} " + "\n", sTmpColumn, sTmpValue);
                                }

                                if (_RefControl4 != null)
                                {
                                    sTmpColumn = ((udcCUSCondition)_RefControl4).sValueColumnName;
                                    sTmpValue = ((udcCUSCondition)_RefControl4).SelectedValueToQueryString;
                                    sMySql.AppendFormat("   AND A.{0} {1} " + "\n", sTmpColumn, sTmpValue);
                                }

                                if (_RefControl5 != null)
                                {
                                    sTmpColumn = ((udcCUSCondition)_RefControl5).sValueColumnName;
                                    sTmpValue = ((udcCUSCondition)_RefControl5).SelectedValueToQueryString;
                                    sMySql.AppendFormat("   AND A.{0} {1} " + "\n", sTmpColumn, sTmpValue);
                                }

                                if (_RefControl6 != null)
                                {
                                    sTmpColumn = ((udcCUSCondition)_RefControl6).sValueColumnName;
                                    sTmpValue = ((udcCUSCondition)_RefControl6).SelectedValueToQueryString;
                                    sMySql.AppendFormat("   AND A.{0} {1} " + "\n", sTmpColumn, sTmpValue);
                                }

                                if (_RefControl7 != null)
                                {
                                    sTmpColumn = ((udcCUSCondition)_RefControl7).sValueColumnName;
                                    sTmpValue = ((udcCUSCondition)_RefControl7).SelectedValueToQueryString;
                                    sMySql.AppendFormat("   AND A.{0} {1} " + "\n", sTmpColumn, sTmpValue);
                                }

                                sMySql.Append(" ORDER BY 1,2");

                                break;

                            // 2012-02-14-임종우 : MAJOR CODE 검색 기능 추가 함.
                            case "MAJOR_CODE":
                                sMySql.Append("SELECT UNIQUE A.MAT_GRP_9 Code, NVL(B.DATA_1, ' ') Data" + "\n");
                                sMySql.Append("  FROM MWIPMATDEF A, MGCMTBLDAT B" + "\n");
                                sMySql.Append(" WHERE A.FACTORY = B.FACTORY(+)" + "\n");
                                sMySql.Append("   AND A.MAT_GRP_8 = B.KEY_1(+)" + "\n");
                                sMySql.Append("   AND B.TABLE_NAME(+) = 'H_MAJOR_CODE'" + "\n");
                                sMySql.Append("   AND A.MAT_TYPE = 'FG'" + "\n");

                                if (((udcCUSCondition)_RefFactory).SelectedValueToQueryString == null ||
                                    ((udcCUSCondition)_RefFactory).SelectedValueToQueryString == "")
                                    sMySql.AppendFormat("   AND A.FACTORY = '{0}' " + "\n", ((udcCUSCondition)_RefFactory).Text);
                                else
                                    sMySql.AppendFormat("   AND A.FACTORY {0} " + "\n", ((udcCUSCondition)_RefFactory).SelectedValueToQueryString);

                                if (_RefControl1 != null)
                                {
                                    sTmpColumn = ((udcCUSCondition)_RefControl1).sValueColumnName;
                                    sTmpValue = ((udcCUSCondition)_RefControl1).SelectedValueToQueryString;
                                    sMySql.AppendFormat("   AND A.{0} {1} " + "\n", sTmpColumn, sTmpValue);
                                }

                                if (_RefControl2 != null)
                                {
                                    sTmpColumn = ((udcCUSCondition)_RefControl2).sValueColumnName;
                                    sTmpValue = ((udcCUSCondition)_RefControl2).SelectedValueToQueryString;
                                    sMySql.AppendFormat("   AND A.{0} {1} " + "\n", sTmpColumn, sTmpValue);
                                }

                                if (_RefControl3 != null)
                                {
                                    sTmpColumn = ((udcCUSCondition)_RefControl3).sValueColumnName;
                                    sTmpValue = ((udcCUSCondition)_RefControl3).SelectedValueToQueryString;
                                    sMySql.AppendFormat("   AND A.{0} {1} " + "\n", sTmpColumn, sTmpValue);
                                }

                                if (_RefControl4 != null)
                                {
                                    sTmpColumn = ((udcCUSCondition)_RefControl4).sValueColumnName;
                                    sTmpValue = ((udcCUSCondition)_RefControl4).SelectedValueToQueryString;
                                    sMySql.AppendFormat("   AND A.{0} {1} " + "\n", sTmpColumn, sTmpValue);
                                }

                                if (_RefControl5 != null)
                                {
                                    sTmpColumn = ((udcCUSCondition)_RefControl5).sValueColumnName;
                                    sTmpValue = ((udcCUSCondition)_RefControl5).SelectedValueToQueryString;
                                    sMySql.AppendFormat("   AND A.{0} {1} " + "\n", sTmpColumn, sTmpValue);
                                }

                                if (_RefControl6 != null)
                                {
                                    sTmpColumn = ((udcCUSCondition)_RefControl6).sValueColumnName;
                                    sTmpValue = ((udcCUSCondition)_RefControl6).SelectedValueToQueryString;
                                    sMySql.AppendFormat("   AND A.{0} {1} " + "\n", sTmpColumn, sTmpValue);
                                }

                                if (_RefControl7 != null)
                                {
                                    sTmpColumn = ((udcCUSCondition)_RefControl7).sValueColumnName;
                                    sTmpValue = ((udcCUSCondition)_RefControl7).SelectedValueToQueryString;
                                    sMySql.AppendFormat("   AND A.{0} {1} " + "\n", sTmpColumn, sTmpValue);
                                }

                                if (_RefControl8 != null)
                                {
                                    sTmpColumn = ((udcCUSCondition)_RefControl8).sValueColumnName;
                                    sTmpValue = ((udcCUSCondition)_RefControl8).SelectedValueToQueryString;
                                    sMySql.AppendFormat("   AND A.{0} {1} " + "\n", sTmpColumn, sTmpValue);
                                }

                                sMySql.Append(" ORDER BY 1,2");

                                break;

                            case "H_ACTIVITY_CODE":
                                sMySql.Append("SELECT A.KEY_1 AS CODE, A.DATA_1 AS DATA" + "\n");
                                sMySql.Append("  FROM MGCMTBLDAT A" + "\n");
                                sMySql.Append(" WHERE A.TABLE_NAME = 'H_ACTIVITY_CODE'" + "\n");

                                if (((udcCUSCondition)_RefFactory).SelectedValueToQueryString == null ||
                                    ((udcCUSCondition)_RefFactory).SelectedValueToQueryString == "")
                                    sMySql.AppendFormat("   AND A.FACTORY = '{0}' " + "\n", ((udcCUSCondition)_RefFactory).Text);
                                else
                                    sMySql.AppendFormat("   AND A.FACTORY {0} " + "\n", ((udcCUSCondition)_RefFactory).SelectedValueToQueryString);

                                sMySql.Append(" ORDER BY A.KEY_1 ASC");
                                break;
                        }

                        if (sMySql.ToString() != "")
                        {
                            RptCUSListFunction.GetTableList(cdvValue.GetListView, sMySql.ToString(), "Code", "Data");
                            cdvValue.SelectedSubItemIndex = 0;
                        }
                        break;

                        #endregion
                    case Miracom.SmartWeb.UI.Controls.udcCondition.eConditionype.TEXT:
                        break;
                    case Miracom.SmartWeb.UI.Controls.udcCondition.eConditionype.CUSTOM:
                        #region " MGCMTBLDAT테이블의 데이터 조회용"
                        // 2011-05-03-임종우 : GCM 테이블 공용 사용을 위해 추가 함.
                        sMySql = new StringBuilder();

                        if (this.RefFactory.Name == "")
                        {
                            CmnFunction.ShowMsgBox(RptMessages.GetMessage("STD029", GlobalVariable.gcLanguage));
                            return;
                        }

                        if (((udcCUSCondition)_RefFactory).Text == "")
                        {
                            CmnFunction.ShowMsgBox(RptMessages.GetMessage("STD001", GlobalVariable.gcLanguage));
                            return;
                        }

                        if (CmnFunction.Trim(sTableName) == "")
                        {
                            CmnFunction.ShowMsgBox(RptMessages.GetMessage("STD025", GlobalVariable.gcLanguage));
                            return;
                        }

                        InitControl();

                        sMySql.Append("SELECT A.KEY_1 AS CODE, A.DATA_1 AS DATA" + "\n");
                        sMySql.Append("  FROM MGCMTBLDAT A" + "\n");
                        sMySql.Append(" WHERE A.TABLE_NAME = '" + CmnFunction.Trim(sTableName) + "'" + "\n");

                        if (((udcCUSCondition)_RefFactory).SelectedValueToQueryString == null ||
                            ((udcCUSCondition)_RefFactory).SelectedValueToQueryString == "")
                            sMySql.AppendFormat("   AND A.FACTORY = '{0}' " + "\n", ((udcCUSCondition)_RefFactory).Text);
                        else
                            sMySql.AppendFormat("   AND A.FACTORY {0} " + "\n", ((udcCUSCondition)_RefFactory).SelectedValueToQueryString);

                        sMySql.Append(" ORDER BY A.KEY_1 ASC");

                        if (sMySql.ToString() != "")
                        {
                            RptCUSListFunction.GetTableList(cdvValue.GetListView, sMySql.ToString(), "Code", "Data");
                            cdvValue.SelectedSubItemIndex = 0;
                        }

                        break;
                        #endregion
                }

                IsDataChange = false;
            }
            catch (Exception ex)
            {
                //CmnFunction.ShowMsgBox("데이타 바인딩 에러 : 체크 콤보 박스에 데이타를 연결하는데 문제가 발생하였습니다.");
                String errorMessage = "";
                errorMessage = String.Concat(errorMessage, ex.Message);
                errorMessage = String.Concat(errorMessage, " Line: ");
                errorMessage = String.Concat(errorMessage, ex.Source);

                CmnFunction.ShowMsgBox(errorMessage, "Error [" + ex.Source + "]", MessageBoxButtons.OK, 1);
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

            switch (m_e_condition_type)
            {
                case Miracom.SmartWeb.UI.Controls.udcCondition.eConditionype.FACTORY:
                    break;
                case Miracom.SmartWeb.UI.Controls.udcCondition.eConditionype.FACTORY_ASSEMBLY:
                    break;
                case Miracom.SmartWeb.UI.Controls.udcCondition.eConditionype.MATERIAL:
                    break;
                case Miracom.SmartWeb.UI.Controls.udcCondition.eConditionype.FLOW:
                    break;
                case Miracom.SmartWeb.UI.Controls.udcCondition.eConditionype.OPER:
                    break;
                case Miracom.SmartWeb.UI.Controls.udcCondition.eConditionype.RESOURCE:
                    break;
                case Miracom.SmartWeb.UI.Controls.udcCondition.eConditionype.EVENT:
                    break;
                case Miracom.SmartWeb.UI.Controls.udcCondition.eConditionype.TABLE:
                    break;
                case Miracom.SmartWeb.UI.Controls.udcCondition.eConditionype.RAS_TBL:
                    break;
                case Miracom.SmartWeb.UI.Controls.udcCondition.eConditionype.WIP_TBL:
                    break;
                case Miracom.SmartWeb.UI.Controls.udcCondition.eConditionype.AREA:
                    break;
                case Miracom.SmartWeb.UI.Controls.udcCondition.eConditionype.TEXT:
                    break;
                case Miracom.SmartWeb.UI.Controls.udcCondition.eConditionype.CUSTOM:
                    break;
                // 2010-04-19-임종우 : 원부자재 조회용 MATERIAL_TYPE 추가
                case Miracom.SmartWeb.UI.Controls.udcCondition.eConditionype.MATERIAL_TYPE:
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

            IsDataChange = true;

            if (_IsControlRef == false) return;

            if (_ForcInitControl1 != null)
            {
                if (_ForcInitControl1.GetType().Name == this.GetType().Name)
                {
                    ((udcCUSCondition)_ForcInitControl1).AutoBinding();
                }
                else
                {
                    //서로 다른 컨트롤일경우는 일단 무시한다..
                    //Do nothing.
                }
            }

            if (_ForcInitControl2 != null)
            {
                if (_ForcInitControl2.GetType().Name == this.GetType().Name)
                {
                    ((udcCUSCondition)_ForcInitControl2).AutoBinding();
                }
                else
                {
                    //서로 다른 컨트롤일경우는 일단 무시한다..
                    //Do nothing.
                }
            }

            if (_ForcInitControl3 != null)
            {
                if (_ForcInitControl3.GetType().Name == this.GetType().Name)
                {
                    ((udcCUSCondition)_ForcInitControl3).AutoBinding();
                }
                else
                {
                    //서로 다른 컨트롤일경우는 일단 무시한다..
                    //Do nothing.
                }
            }

            if (_ForcInitControl4 != null)
            {
                if (_ForcInitControl4.GetType().Name == this.GetType().Name)
                {
                    ((udcCUSCondition)_ForcInitControl4).AutoBinding();
                }
                else
                {
                    //서로 다른 컨트롤일경우는 일단 무시한다..
                    //Do nothing.
                }
            }

        }

        #endregion

        #region "Properties"

        [Browsable(true), DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        public Miracom.SmartWeb.UI.Controls.udcCondition.eConditionype CondtionType
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
        public string SelectedValueToQueryString
        {
            get
            {
                return cdvValue.SelectedValueToQueryText;
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

        [Browsable(true), DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        //[Editor(typeof(System.ComponentModel.Design.MultilineStringEditor), typeof(System.Drawing.Design.UITypeEditor))]
        public string sDynamicQuery
        {
            get
            {
                return s_query_txt;
            }
            set
            {
                s_query_txt = value;
            }
        }

        [Browsable(true), DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        public string sCodeColumnName
        {
            get
            {
                return s_code_txt;
            }
            set
            {
                s_code_txt = value;
            }
        }

        [Browsable(true), DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        public string sValueColumnName
        {
            get
            {
                return s_value_txt;
            }
            set
            {
                s_value_txt = value;
            }
        }

        [Browsable(true), DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        public bool bMultiSelect
        {
            get
            {
                return cdvValue.MultiSelect;
            }
            set
            {
                cdvValue.MultiSelect = value;
            }
        }

        // Add By John Seo. 2008.12.17
        [DefaultValue(false), Description("값변경시 다른컨트롤에 영향을 받을지 여부")]
        public bool ControlRef
        {
            get { return _IsControlRef; }
            set
            {
                _IsControlRef = value;                
            }
        }

        [DefaultValue(null), Description("자신의 값이 변경시 초기화 시킬 첫번째 컨트롤이름")]
        public Control ForcInitControl1
        {
            get
            {
                return _ForcInitControl1;
            }
            set
            {
                _ForcInitControl1 = value;
            }
        }

        [DefaultValue(null), Description("자신의 값이 변경시 초기화 시킬 두번째 컨트롤이름")]
        public Control ForcInitControl2
        {
            get
            {
                return _ForcInitControl2;
            }
            set
            {
                _ForcInitControl2 = value;
            }
        }

        [DefaultValue(null), Description("자신의 값이 변경시 초기화 시킬 세번째 컨트롤이름")]
        public Control ForcInitControl3
        {
            get
            {
                return _ForcInitControl3;
            }
            set
            {
                _ForcInitControl3 = value;
            }
        }

        [DefaultValue(null), Description("자신의 값이 변경시 초기화 시킬 네번째 컨트롤이름")]
        public Control ForcInitControl4
        {
            get
            {
                return _ForcInitControl4;
            }
            set
            {
                _ForcInitControl4 = value;
            }
        }

        [DefaultValue(null), Description("조회시 참조할 Factory이름")]
        public Control RefFactory
        {
            get
            {
                return _RefFactory ;
            }
            set
            {
                _RefFactory = value;
            }
        }

        [DefaultValue(null), Description("조회시 참조할 첫번째 컨트롤이름")]
        public Control RefControl1
        {
            get
            {
                return _RefControl1;
            }
            set
            {
                _RefControl1 = value;
            }
        }

        [DefaultValue(null), Description("조회시 참조할 두번째 컨트롤이름")]
        public Control RefControl2
        {
            get
            {
                return _RefControl2;
            }
            set
            {
                _RefControl2 = value;
            }
        }

        [DefaultValue(null), Description("조회시 참조할 세번째 컨트롤이름")]
        public Control RefControl3
        {
            get
            {
                return _RefControl3;
            }
            set
            {
                _RefControl3 = value;
            }
        }

        [DefaultValue(null), Description("조회시 참조할 네번째 컨트롤이름")]
        public Control RefControl4
        {
            get
            {
                return _RefControl4;
            }
            set
            {
                _RefControl4 = value;
            }
        }

        [DefaultValue(null), Description("조회시 참조할 다섯번째 컨트롤이름")]
        public Control RefControl5
        {
            get
            {
                return _RefControl5;
            }
            set
            {
                _RefControl5 = value;
            }
        }

        [DefaultValue(null), Description("조회시 참조할 여섯번째 컨트롤이름")]
        public Control RefControl6
        {
            get
            {
                return _RefControl6;
            }
            set
            {
                _RefControl6 = value;
            }
        }

        [DefaultValue(null), Description("조회시 참조할 일곱번째 컨트롤이름")]
        public Control RefControl7
        {
            get
            {
                return _RefControl7;
            }
            set
            {
                _RefControl7 = value;
            }
        }

        [DefaultValue(null), Description("조회시 참조할 여덟번째 컨트롤이름")]
        public Control RefControl8
        {
            get
            {
                return _RefControl8;
            }
            set
            {
                _RefControl8 = value;
            }
        }

        #endregion

        public void AutoBinding ()
		{
			try
			{
                cdvValue.Text = "";
            }
            catch (Exception)
            {
                CmnFunction.ShowMsgBox(RptMessages.GetMessage("STD36", GlobalVariable.gcLanguage));
            }

        }
        public void Init()
        {
            cdvValue.Text = "";
            cdvValue.Init();            
        }

        // From TO 에서 선택된 값을 ListView로 리턴 ( ex : OPER 사용 할때 )
        public ListView getSelectedItems()
        {
            ListView lvFrom = new ListView();
            ListView SelectedItem = new ListView();
            
            try
            {
                lvFrom = this.ValueGetListView;
                

                if (lvFrom.Items.Count == 0)
                {
                    SelectedItem.Items.Clear();
                    SelectedItem.Items.Add("");
                    return SelectedItem;
                }
            
                SelectedItem.Items.Clear();
                for (int i = 0; i < lvFrom.Items.Count; i++)
                {
                    if(lvFrom.Items[i].Checked == true)
                    {
                        SelectedItem.Items.Add(lvFrom.Items[i].Text);
                    }                    
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

        // From에서 선택된 갯수 리턴.
        public int SelectCount
        {
            get
            {
                ListView lvFrom = new ListView();

                int iFromIdx = 0;

                lvFrom = this.ValueGetListView;

                if (lvFrom.Items.Count == 0)
                    return 0;

                for (int i = 0; i < lvFrom.Items.Count; i++)
                {
                    if (lvFrom.Items[i].Checked == true)
                    {
                        iFromIdx++;
                    }
                }

                if (iFromIdx == 0) iFromIdx = lvFrom.Items.Count;
                
                return iFromIdx;
             
            }
        }

        // SQL문 생성. by John Seo. 2008.11.25
        public string getDecodeQuery(string sFront, string sBack, string sAlias)
        {
            System.Text.StringBuilder strSqlString = new System.Text.StringBuilder();
            string sHeader = null;
            ListView lvFrom = new ListView();

            int idx = 0;

            try
            {
                lvFrom = this.ValueGetListView;

                if (lvFrom.Items.Count == 0) return "";

                for (int i = 0; i < lvFrom.Items.Count; i++)    // ALL을 선택했는지 확인
                {
                    if (lvFrom.Items[i].Checked == true)
                    {                     
                        idx++;
                    }
                }

                if(idx == 0)   // ALL을 선택했을경우  (하나도 체크하지 않음)
                {
                    idx = 0;
                    for (int i = 0; i < lvFrom.Items.Count; i++)
                    {
                        if (lvFrom.Items[i].Checked == false)
                        {
                            sHeader = lvFrom.Items[i].Text;
                            strSqlString.AppendFormat(", " + "{0}, '{1}', {2} {3}{4}" + "\n", sFront, sHeader, sBack, sAlias, idx.ToString());
                            idx++;
                        }
                    }
                }
                else    // 원하는 OPER를 선택 했을 경우
                {
                    idx = 0;
                    for (int i = 0; i < lvFrom.Items.Count; i++)
                    {
                        if (lvFrom.Items[i].Checked == true)
                        {
                            sHeader = lvFrom.Items[i].Text;
                            strSqlString.AppendFormat(", " + "{0}, '{1}', {2} {3}{4}" + "\n", sFront, sHeader, sBack, sAlias, idx.ToString());
                            idx++;
                        }
                    }
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
            int iCount = 0;

            try
            {
                iCount = this.SelectCount;

                if (iCount < 1) return "";

                for (int i = 0; i < iCount; i++)
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

        // SQL문 생성 Add by  MgKim 2008.11.15 (특정 case에 사용)
        public string getRepeatQuery(string first, string second, string third, string fourth)
        {
            System.Text.StringBuilder strSqlString = new System.Text.StringBuilder();
            int iCount = 0;

            try
            {
                iCount = this.SelectCount;

                if (iCount < 1) return "";

                for (int i = 0; i < iCount; i++)
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
            int iCount = 0;

            try
            {
                iCount = this.SelectCount;

                if (iCount < 1) return "";

                string sBack = string.Empty;
                for (int i = 0; i < iCount; i++)
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
            switch (m_e_condition_type)
            {
                case Miracom.SmartWeb.UI.Controls.udcCondition.eConditionype.FACTORY:
                    cdvValue.Init();
                    CmnInitFunction.InitListView(cdvValue.GetListView);
                    lblConditionLabel.Text = "Factory";
                    cdvValue.Columns.Add("Factory", 50, HorizontalAlignment.Left);
                    cdvValue.Columns.Add("Desc", 100, HorizontalAlignment.Left);
                    cdvValue.SelectedSubItemIndex = 0;
                    break;
                case Miracom.SmartWeb.UI.Controls.udcCondition.eConditionype.FACTORY_ASSEMBLY:
                    cdvValue.Init();
                    CmnInitFunction.InitListView(cdvValue.GetListView);
                    lblConditionLabel.Text = "Factory";
                    cdvValue.Columns.Add("Factory", 50, HorizontalAlignment.Left);
                    cdvValue.Columns.Add("Desc", 100, HorizontalAlignment.Left);
                    cdvValue.SelectedSubItemIndex = 0;
                    break;
                case Miracom.SmartWeb.UI.Controls.udcCondition.eConditionype.MATERIAL:
                    cdvValue.Init();
                    CmnInitFunction.InitListView(cdvValue.GetListView);
                    lblConditionLabel.Text = "Material";
                    cdvValue.Columns.Add("Material", 50, HorizontalAlignment.Left);
                    cdvValue.Columns.Add("Desc", 100, HorizontalAlignment.Left);
                    break;
                case Miracom.SmartWeb.UI.Controls.udcCondition.eConditionype.FLOW:
                    cdvValue.Init();
                    CmnInitFunction.InitListView(cdvValue.GetListView);
                    lblConditionLabel.Text = "Flow";
                    cdvValue.Columns.Add("Flow", 50, HorizontalAlignment.Left);
                    cdvValue.Columns.Add("Desc", 100, HorizontalAlignment.Left);
                    break;
                case Miracom.SmartWeb.UI.Controls.udcCondition.eConditionype.OPER:
                    cdvValue.Init();
                    CmnInitFunction.InitListView(cdvValue.GetListView);
                    lblConditionLabel.Text = "Operation";
                    cdvValue.Columns.Add("Oper", 50, HorizontalAlignment.Left);
                    cdvValue.Columns.Add("Desc", 100, HorizontalAlignment.Left);
                    break;
                case Miracom.SmartWeb.UI.Controls.udcCondition.eConditionype.RESOURCE:
                    cdvValue.Init();
                    CmnInitFunction.InitListView(cdvValue.GetListView);
                    lblConditionLabel.Text = "Resource";
                    break;
                case Miracom.SmartWeb.UI.Controls.udcCondition.eConditionype.EVENT:
                    cdvValue.Init();
                    CmnInitFunction.InitListView(cdvValue.GetListView);
                    lblConditionLabel.Text = "Event";
                    break;
                case Miracom.SmartWeb.UI.Controls.udcCondition.eConditionype.TABLE:
                    cdvValue.Init();
                    CmnInitFunction.InitListView(cdvValue.GetListView);
                    if (lblConditionLabel.Text.Trim() == "")
                    {
                        lblConditionLabel.Text = "Table";
                    }
                    cdvValue.Columns.Add("Code", 50, HorizontalAlignment.Left);
                    cdvValue.Columns.Add("Data", 100, HorizontalAlignment.Left);
                    break;
                case Miracom.SmartWeb.UI.Controls.udcCondition.eConditionype.RAS_TBL:
                    cdvValue.Init();
                    CmnInitFunction.InitListView(cdvValue.GetListView);
                    if (lblConditionLabel.Text.Trim() == "")
                    {
                        lblConditionLabel.Text = "RAS Table";
                    }
                    cdvValue.Columns.Add("Code", 50, HorizontalAlignment.Left);
                    cdvValue.Columns.Add("Data", 100, HorizontalAlignment.Left);
                    break;
                case Miracom.SmartWeb.UI.Controls.udcCondition.eConditionype.WIP_TBL:
                    cdvValue.Init();
                    CmnInitFunction.InitListView(cdvValue.GetListView);
                    if (lblConditionLabel.Text.Trim() == "")
                    {
                        lblConditionLabel.Text = "WIP Table";
                    }
                    cdvValue.Columns.Add("Code", 50, HorizontalAlignment.Left);
                    cdvValue.Columns.Add("Data", 100, HorizontalAlignment.Left);
                    break;
                case Miracom.SmartWeb.UI.Controls.udcCondition.eConditionype.AREA:
                    cdvValue.Init();
                    CmnInitFunction.InitListView(cdvValue.GetListView);
                    lblConditionLabel.Text = "Area";
                    cdvValue.Columns.Add("Area", 50, HorizontalAlignment.Left);
                    cdvValue.Columns.Add("Data", 100, HorizontalAlignment.Left);
                    break;
                case Miracom.SmartWeb.UI.Controls.udcCondition.eConditionype.TEXT:
                    if (lblConditionLabel.Text.Trim() == "")
                    {
                        lblConditionLabel.Text = "Text";
                    }
                    break;
                case Miracom.SmartWeb.UI.Controls.udcCondition.eConditionype.CUSTOM:
                    cdvValue.Init();
                    CmnInitFunction.InitListView(cdvValue.GetListView);
                    if (lblConditionLabel.Text.Trim() == "")
                    {
                        lblConditionLabel.Text = "Custom";
                    }
                    cdvValue.Columns.Add("Code", 50, HorizontalAlignment.Left);
                    cdvValue.Columns.Add("Data", 100, HorizontalAlignment.Left);
                    break;
                case Miracom.SmartWeb.UI.Controls.udcCondition.eConditionype.HQ_TABLE:
                    cdvValue.Init();
                    CmnInitFunction.InitListView(cdvValue.GetListView);
                    if (lblConditionLabel.Text.Trim() == "")
                    {
                        lblConditionLabel.Text = "HQ_TABLE";
                    }
                    cdvValue.Columns.Add("Code", 50, HorizontalAlignment.Left);
                    cdvValue.Columns.Add("Data", 100, HorizontalAlignment.Left);
                    break;
                // 2010-04-19-임종우 : 원부자재 조회용 MATERIAL_TYPE 추가
                case Miracom.SmartWeb.UI.Controls.udcCondition.eConditionype.MATERIAL_TYPE:
                    cdvValue.Init();
                    CmnInitFunction.InitListView(cdvValue.GetListView);
                    lblConditionLabel.Text = "Mat_Type";
                    cdvValue.Columns.Add("Material", 50, HorizontalAlignment.Left);
                    cdvValue.Columns.Add("Desc", 100, HorizontalAlignment.Left);
                    break;
            }
        }

        public void SetChangedFlag(bool flag)
        {
            IsDataChange = flag;
        }
    }
}