using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

using Miracom.SmartWeb.FWX;

namespace Miracom.SmartWeb.UI
{
    public partial class udcFLXViewInquiry : Miracom.SmartWeb.FWX.udcUIBase
    {
        public udcFLXViewInquiry()
        {
            InitializeComponent();
        }

        #region " Constant Definition "

        #endregion

        #region " Variable Definition "

        struct FACTORY_SHIFT
        {
            public FACTORY_SHIFT_SHIFT[] shift;
        }

        struct FACTORY_SHIFT_SHIFT
        {
            public string shift_day_flag;
            public string shift_start;
        }

        struct WORK_TIME
        {
            public string work_date;
            public int work_days;
            public int work_month;
            public int last_shift;
            public int work_shift;
            public int work_week;
            public int work_year;
        }

        struct COLUMN
        {
            public FLX_COLUMN[] column;
            public int count;
        }

        struct FLX_COLUMN
        {
            public string period;
            public string query;
            public string column_name;
            public string column_header_name;
            public string column_alias_1;
            public string column_alias_2;
        }

        private FACTORY_SHIFT Factory_Shift = new FACTORY_SHIFT();
        
        #endregion

        #region " Function Definition "

        private void InitCodeView()
        {
            cdvFactory.Init();
            CmnInitFunction.InitListView(cdvFactory.GetListView);
            cdvFactory.Columns.Add("Factory", 150, HorizontalAlignment.Left);
            cdvFactory.Columns.Add("Desc", 200, HorizontalAlignment.Left);
            cdvFactory.SelectedSubItemIndex = 0;
            CmnListFunction.ViewFactoryList(cdvFactory.GetListView, '1',"");

            cdvInqGrp.Init();
            CmnInitFunction.InitListView(cdvInqGrp.GetListView);
            cdvInqGrp.Columns.Add("Group", 150, HorizontalAlignment.Left);
            cdvInqGrp.Columns.Add("Desc", 200, HorizontalAlignment.Left);
            cdvInqGrp.SelectedSubItemIndex = 0;

            cdvInqName.Init();
            CmnInitFunction.InitListView(cdvInqName.GetListView);
            cdvInqName.Columns.Add("Name", 150, HorizontalAlignment.Left);
            cdvInqName.Columns.Add("Desc", 200, HorizontalAlignment.Left);
            cdvInqName.SelectedSubItemIndex = 0;
        }

        private FACTORY_SHIFT Get_Factory_Shift()
        {
            FACTORY_SHIFT FactoryShift_tmp = new FACTORY_SHIFT();

            DataTable rtDataTable = new DataTable();
            string QueryCond = null;

            try
            {
                CmnFunction.oComm.SetUrl();

                QueryCond = FwxCmnFunction.PackCondition(QueryCond, cdvFactory.Text);

                rtDataTable = CmnFunction.oComm.SelectData("MWIPFACDEF", "1", QueryCond);

                if (rtDataTable.Rows.Count == 0)
                {
                    QueryCond = null;
                    QueryCond = FwxCmnFunction.PackCondition(QueryCond, "SYSTEM");

                    rtDataTable = CmnFunction.oComm.SelectData("MWIPFACDEF", "1", QueryCond);
                }

                if (rtDataTable.Rows.Count > 0)
                {
                    int i = 0;

                    FactoryShift_tmp.shift = new FACTORY_SHIFT_SHIFT[4];
                    FactoryShift_tmp.shift[0] = new FACTORY_SHIFT_SHIFT();
                    FactoryShift_tmp.shift[1] = new FACTORY_SHIFT_SHIFT();
                    FactoryShift_tmp.shift[2] = new FACTORY_SHIFT_SHIFT();
                    FactoryShift_tmp.shift[3] = new FACTORY_SHIFT_SHIFT();

                    FactoryShift_tmp.shift[0].shift_day_flag = rtDataTable.Rows[i]["SHIFT_1_DAY_FLAG"].ToString();
                    FactoryShift_tmp.shift[0].shift_start = rtDataTable.Rows[i]["SHIFT_1_START"].ToString();

                    FactoryShift_tmp.shift[1].shift_day_flag = rtDataTable.Rows[i]["SHIFT_2_DAY_FLAG"].ToString();
                    FactoryShift_tmp.shift[1].shift_start = rtDataTable.Rows[i]["SHIFT_2_START"].ToString();

                    FactoryShift_tmp.shift[2].shift_day_flag = rtDataTable.Rows[i]["SHIFT_3_DAY_FLAG"].ToString();
                    FactoryShift_tmp.shift[2].shift_start = rtDataTable.Rows[i]["SHIFT_3_START"].ToString();

                    FactoryShift_tmp.shift[3].shift_day_flag = rtDataTable.Rows[i]["SHIFT_4_DAY_FLAG"].ToString();
                    FactoryShift_tmp.shift[3].shift_start = rtDataTable.Rows[i]["SHIFT_4_START"].ToString();
                }

                if (rtDataTable.Rows.Count > 0)
                {
                    rtDataTable.Dispose();
                }

                return FactoryShift_tmp;
            }
            catch (Exception ex)
            {
                CmnFunction.ShowMsgBox(ex.ToString());
                return FactoryShift_tmp;
            }
        }

        private WORK_TIME Get_Current_Work_Time(string strDateTime, int iDay)
        {
            return Get_Current_Work_Time(strDateTime, iDay, 0);
        }
        private WORK_TIME Get_Current_Work_Time(string strDateTime, int iDay, int iMonth)
        {
            DataTable rtDataTable = new DataTable();
            WORK_TIME WorkTime_tmp = new WORK_TIME();
            DateTime CurrentDateTime;

            string QueryCond = null;
            int i;

            try
            {
                Factory_Shift = Get_Factory_Shift();

                for (i = 0; i < 4; i++)
                {
                    if (Factory_Shift.shift[i].shift_day_flag == Factory_Shift.shift[i + 1].shift_day_flag)
                    {
                        if (System.Convert.ToInt32(strDateTime.Substring(8, 4)) >= System.Convert.ToInt32(Factory_Shift.shift[i].shift_start) && System.Convert.ToInt32(strDateTime.Substring(8, 4)) < System.Convert.ToInt32(Factory_Shift.shift[i + 1].shift_start))
                        {
                            WorkTime_tmp.work_shift = i + 1;
                            break;

                        }
                    }
                    else if (Factory_Shift.shift[i].shift_day_flag != Factory_Shift.shift[i + 1].shift_day_flag && Factory_Shift.shift[i + 1].shift_day_flag != " ")
                    {
                        if (System.Convert.ToInt32(strDateTime.Substring(8, 4)) >= System.Convert.ToInt32(Factory_Shift.shift[i].shift_start) || System.Convert.ToInt32(strDateTime.Substring(8, 4)) < System.Convert.ToInt32(Factory_Shift.shift[i + 1].shift_start))
                        {
                            WorkTime_tmp.work_shift = i + 1;
                            break;
                        }
                    }
                    else if (Factory_Shift.shift[i + 1].shift_day_flag == " ")
                    {
                        WorkTime_tmp.work_shift = i + 1;
                        break;
                    }
                }

                if (Factory_Shift.shift[0].shift_day_flag == "T")
                {
                    WorkTime_tmp.last_shift = 1;
                }
                else
                {
                    WorkTime_tmp.last_shift = 0;
                }
                if (Factory_Shift.shift[1].shift_day_flag == "T")
                {
                    WorkTime_tmp.last_shift = 2;
                }
                if (Factory_Shift.shift[2].shift_day_flag == "T")
                {
                    WorkTime_tmp.last_shift = 3;
                }
                if (Factory_Shift.shift[3].shift_day_flag == "T")
                {
                    WorkTime_tmp.last_shift = 4;
                }

                CurrentDateTime = new DateTime(System.Convert.ToInt32(strDateTime.Substring(0, 4)),
                                               System.Convert.ToInt32(strDateTime.Substring(4, 2)),
                                               System.Convert.ToInt32(strDateTime.Substring(6, 2)),
                                               System.Convert.ToInt32(strDateTime.Substring(8, 2)),
                                               System.Convert.ToInt32(strDateTime.Substring(10, 2)),
                                               System.Convert.ToInt32(strDateTime.Substring(12, 2)));

                if (iDay != 0)
                {
                    CurrentDateTime.AddDays(-iDay);
                }

                if (iMonth != 0)
                {
                    CurrentDateTime.AddMonths(-iMonth);
                }

                if (System.Convert.ToInt32(strDateTime.Substring(8, 4)) < System.Convert.ToInt32(Factory_Shift.shift[0].shift_start))
                {
                    CurrentDateTime.AddDays(-1);
                }

                CmnFunction.oComm.SetUrl();

                QueryCond = FwxCmnFunction.PackCondition(QueryCond, cdvFactory.Text);
                QueryCond = FwxCmnFunction.PackCondition(QueryCond, CurrentDateTime.Year.ToString());
                QueryCond = FwxCmnFunction.PackCondition(QueryCond, CurrentDateTime.Month.ToString());
                QueryCond = FwxCmnFunction.PackCondition(QueryCond, CurrentDateTime.Day.ToString());

                rtDataTable = CmnFunction.oComm.SelectData("MWIPCALDEF", "1", QueryCond);

                if (rtDataTable.Rows.Count == 0)
                {
                    QueryCond = null;
                    QueryCond = FwxCmnFunction.PackCondition(QueryCond, "SYSTEM");
                    QueryCond = FwxCmnFunction.PackCondition(QueryCond, CurrentDateTime.Year.ToString());
                    QueryCond = FwxCmnFunction.PackCondition(QueryCond, CurrentDateTime.Month.ToString());
                    QueryCond = FwxCmnFunction.PackCondition(QueryCond, CurrentDateTime.Day.ToString());

                    rtDataTable = CmnFunction.oComm.SelectData("MWIPCALDEF", "1", QueryCond);
                }

                if (rtDataTable.Rows.Count == 0)
                {
                    //default

                }
                if (rtDataTable.Rows.Count > 0)
                {
                    i = 0;

                    WorkTime_tmp.work_year = System.Convert.ToInt32(rtDataTable.Rows[i]["PLAN_YEAR"]);
                    WorkTime_tmp.work_month = System.Convert.ToInt32(rtDataTable.Rows[i]["PLAN_MONTH"]);
                    WorkTime_tmp.work_week = System.Convert.ToInt32(rtDataTable.Rows[i]["PLAN_WEEK"]);
                    WorkTime_tmp.work_days = System.Convert.ToInt32(rtDataTable.Rows[i]["JULIAN_DAY"]);

                    int iTime;
                    iTime = CurrentDateTime.Hour * 100 + CurrentDateTime.Minute;

                    if (rtDataTable.Rows[i]["PREV_DAY_FG"].ToString() == "Y")
                    {
                        if (System.Convert.ToInt32(rtDataTable.Rows[i]["START_TIME"]) <= iTime)
                        {
                            CurrentDateTime.AddDays(-1);
                        }
                    }
                    else
                    {
                        if (System.Convert.ToInt32(rtDataTable.Rows[i]["START_TIME"]) > iTime)
                        {
                            CurrentDateTime.AddDays(-1);
                        }
                    }

                    WorkTime_tmp.work_date = CurrentDateTime.ToString("yyyyMMdd");
                }

                if (rtDataTable.Rows.Count > 0)
                {
                    rtDataTable.Dispose();
                }

                return WorkTime_tmp;
            }
            catch (Exception ex)
            {
                CmnFunction.ShowMsgBox(ex.ToString());
                return WorkTime_tmp;
            }
        }

        private bool View_Inquiry()
        {
            DataTable rtDataTable = new DataTable();
            StringBuilder strQuery = new StringBuilder();

            COLUMN colColumn = new COLUMN();

            string QueryCond = null;
            string strSelectItem = null;
            string strSelectValue = null;
            string strTable = null;
            string strGroup = null;

            int iStartColumn = 2;
            int i = 0;

            try
            {
                QueryCond = FwxCmnFunction.PackCondition(QueryCond, cdvFactory.Text);
                QueryCond = FwxCmnFunction.PackCondition(QueryCond, cdvInqName.Text);

                rtDataTable = CmnFunction.oComm.SelectData("RWEBFLXINQ", "1", QueryCond);

                if (rtDataTable.Rows.Count > 0)
                {
                    strSelectItem = rtDataTable.Rows[i]["SELECT_ITEM"].ToString();
                    strSelectValue = rtDataTable.Rows[i]["SELECT_VALUE"].ToString();

                    txtGrpItem.Text = rtDataTable.Rows[i]["GROUP_ITEM"].ToString();
                    txtValue.Text = strSelectItem + " : " + strSelectValue;
                    txtFilter.Text = rtDataTable.Rows[i]["FILTER_QUERY"].ToString();
                }

                if (rtDataTable.Rows.Count > 0)
                {
                    rtDataTable.Dispose();
                }

                switch (txtGrpItem.Text)
                {
                    case "MAT":
                        strQuery.Append("SELECT MAT.MAT_ID, MAT.MAT_VER \n");
                        break;
                    case "FLOW":
                        strQuery.Append("SELECT FLW.FLOW, RSW.FLOW_SEQ_NUM \n");
                        break;
                    case "OPER":
                        strQuery.Append("SELECT OPR.OPER, OPR.OPER_DESC \n");
                        break;
                    default:
                        strQuery.AppendFormat("SELECT GCM.KEY_1 AS {0} \n", txtGrpItem.Text);
                        break;
                }

                QueryCond = null;

                QueryCond = FwxCmnFunction.PackCondition(QueryCond, cdvFactory.Text);
                QueryCond = FwxCmnFunction.PackCondition(QueryCond, cdvInqName.Text);

                rtDataTable = CmnFunction.oComm.FillData("RWEBFLXCOL", "1", QueryCond);

                if (rtDataTable.Rows.Count == 0)
                {
                    return false;
                }

                if (rtDataTable.Rows.Count > 0)
                {
                    colColumn.column = new FLX_COLUMN[rtDataTable.Rows.Count];
                    colColumn.count = rtDataTable.Rows.Count;

                    for (i = 0; i < rtDataTable.Rows.Count; i++)
                    {
                        colColumn.column[i] = new FLX_COLUMN();
                        colColumn.column[i].period = rtDataTable.Rows[i]["COLUMN_PERIOD"].ToString();
                        colColumn.column[i].column_name = rtDataTable.Rows[i]["COLUMN_NAME"].ToString();
                        colColumn.column[i].column_alias_1 = rtDataTable.Rows[i]["COLUMN_ALIAS_1"].ToString();
                        colColumn.column[i].column_alias_2 = rtDataTable.Rows[i]["COLUMN_ALIAS_2"].ToString();
                        if (Make_Column_Query(ref colColumn.column[i]) == false) return false;
                    }
                }

                for (i = 0; i < colColumn.count; i++)
                {
                    strQuery.AppendFormat(", NVL(SUM(RSW.{0}), 0) AS {1}_{2} \n", colColumn.column[i].column_header_name, colColumn.column[i].period, colColumn.column[i].column_name);
                }

                strQuery.Append("FROM ( \n");

                for (i = 0; i < colColumn.count; i++)
                {
                    strQuery.Append(Make_Sub_Query(colColumn, i));

                    if (i + 1 != colColumn.count)
                    {
                        strQuery.Append("UNION ALL \n");
                    }
                }

                strQuery.Append(" ) RSW, MWIPMATDEF MAT, MWIPFLWDEF FLW, MWIPOPRDEF OPR, MGCMTBLDAT GCM \n");

                strQuery.Append("WHERE RSW.FACTORY = MAT.FACTORY ");
                strQuery.Append(" AND RSW.MAT_ID = MAT.MAT_ID ");
                strQuery.Append(" AND RSW.MAT_VER = MAT.MAT_VER ");
                strQuery.Append(" AND RSW.FACTORY = FLW.FACTORY ");
                strQuery.Append(" AND RSW.FLOW = FLW.FLOW ");
                strQuery.Append(" AND RSW.FACTORY = OPR.FACTORY ");
                strQuery.Append(" AND RSW.OPER = OPR.OPER ");
                strQuery.Append(" AND RSW.FACTORY = GCM.FACTORY ");
                strQuery.Append(" AND (GCM.TABLE_NAME like 'MATERIAL_GRP%' OR GCM.TABLE_NAME like 'FLOW_GRP%' OR GCM.TABLE_NAME like 'OPER_GRP%') ");

                switch (strSelectItem)
                {
                    case "MAT":
                        strQuery.AppendFormat(" AND MAT.MATERIAL = '{0}'", strSelectValue);
                        break;
                    case "FLOW":
                        strQuery.AppendFormat(" AND FLW.FLOW = '{0}'", strSelectValue);
                        break;
                    case "OPER":
                        strQuery.AppendFormat(" AND OPR.OPER = '{0}'", strSelectValue);
                        break;

                    default:

                        if (strSelectItem.LastIndexOf("MAT") > -1)
                        {
                            strSelectItem = strSelectItem.Replace("MATERIAL", "MAT");
                            strQuery.AppendFormat(" AND MAT.{0} = '{1}' \n", strSelectItem, strSelectValue);
                        }
                        else if (strSelectItem.LastIndexOf("FLOW") > -1)
                        {
                            strQuery.AppendFormat(" AND FLW.{0} = '{1}' \n", strSelectItem, strSelectValue);
                        }
                        else if (strSelectItem.LastIndexOf("OPER") > -1)
                        {
                            strQuery.AppendFormat(" AND OPR.{0} = '{1}' \n", strSelectItem, strSelectValue);
                        }

                        break;
                }

                strQuery.Append(txtFilter.Text);

                switch (txtGrpItem.Text)
                {
                    case "MAT":
                        strQuery.Append(" GROUP BY MAT.MAT_ID, MAT.MAT_VER \n");
                        break;
                    case "FLOW":
                        strQuery.Append(" GROUP BY FLW.FLOW, RSW.FLOW_SEQ_NUM \n");
                        break;
                    case "OPER":
                        strQuery.Append(" GROUP BY OPR.OPER, OPR.OPER_DESC \n");
                        break;

                    default:

                        strGroup = txtGrpItem.Text;

                        if (txtGrpItem.Text.LastIndexOf("MAT") > -1)
                        {
                            strTable = "MAT";
                            strGroup = strGroup.Replace("MATERIAL", "MAT");
                        }
                        else if (txtGrpItem.Text.LastIndexOf("FLOW") > -1)
                        {
                            strTable = "FLW";
                        }
                        else if (txtGrpItem.Text.LastIndexOf("OPER") > -1)
                        {
                            strTable = "OPR";
                        }

                        strQuery.AppendFormat(" AND GCM.FACTORY = '{0}' \n", cdvFactory.Text);
                        strQuery.AppendFormat(" AND GCM.TABLE_NAME = '{0}' \n", txtGrpItem.Text);
                        strQuery.AppendFormat(" AND GCM.KEY_1 = {0}.{1} \n", strTable, strGroup);
                        strQuery.Append(" GROUP BY GCM.KEY_1 \n");

                        //Start Column
                        iStartColumn = 1;
                        break;
                }

                strQuery = strQuery.Replace("\n", "");

                if (strQuery.ToString().Trim() == "") return false;

                QueryCond = null;
                QueryCond = FwxCmnFunction.PackCondition(QueryCond, strQuery.ToString());

                spdData_Sheet1.RowCount = 0;
                this.Refresh();
                rtDataTable = CmnFunction.oComm.FillData("RSUMWIPLTH", "1", QueryCond);
                spdData_Sheet1.DataSource = rtDataTable;
                CmnFunction.FitColumnHeader(spdData);

                spdData_Sheet1.ColumnHeader.RowCount = 2;

                for (i = 0; i < colColumn.count; i++)
                {
                    spdData_Sheet1.ColumnHeader.Cells[0, iStartColumn + i].Text = colColumn.column[i].period + "_" + colColumn.column[i].column_name;
                    spdData_Sheet1.ColumnHeader.Cells[1, iStartColumn + i].Text = colColumn.column[i].column_alias_1;
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

        private bool Make_Column_Query(ref FLX_COLUMN colColumn)
        {
            WORK_TIME WorkTime_tmp = new WORK_TIME();
            string strTime = "";

            try
            {
                strTime = CmnFunction.GetSysDateTime();

                if (dtpDateTime.Visible == true)
                {
                    strTime = dtpDateTime.Value.ToString("yyyyMMddHHmmss");
                }

                switch (colColumn.period)
                {
                    case "CS1":
                        WorkTime_tmp = Get_Current_Work_Time(strTime, 0);
                        colColumn.column_header_name = "CS1_" + colColumn.column_name;
                        break;
                    case "CS2":
                        WorkTime_tmp = Get_Current_Work_Time(strTime, 0);
                        colColumn.column_header_name = "CS2_" + colColumn.column_name;
                        break;
                    case "CS3":
                        WorkTime_tmp = Get_Current_Work_Time(strTime, 0);
                        colColumn.column_header_name = "CS3_" + colColumn.column_name;
                        break;
                    case "PS1":
                        WorkTime_tmp = Get_Current_Work_Time(strTime, 1);
                        colColumn.column_header_name = "PS1_" + colColumn.column_name;
                        break;
                    case "PS2":
                        WorkTime_tmp = Get_Current_Work_Time(strTime, 1);
                        colColumn.column_header_name = "PS2_" + colColumn.column_name;
                        break;
                    case "PS3":
                        WorkTime_tmp = Get_Current_Work_Time(strTime, 1);
                        colColumn.column_header_name = "PS3_" + colColumn.column_name;
                        break;
                    case "CS":
                        WorkTime_tmp = Get_Current_Work_Time(strTime, 0);
                        colColumn.column_header_name = "S" + WorkTime_tmp.work_shift.ToString() + "_" + colColumn.column_name;
                        break;
                    case "PS":
                        WorkTime_tmp = Get_Current_Work_Time(strTime, 1);
                        colColumn.column_header_name = "S" + WorkTime_tmp.work_shift.ToString() + "_" + colColumn.column_name;
                        break;
                    case "CD":
                        WorkTime_tmp = Get_Current_Work_Time(strTime, 0);
                        colColumn.column_header_name = colColumn.period + colColumn.column_name;
                        break;
                    case "PD":
                        WorkTime_tmp = Get_Current_Work_Time(strTime, 1);
                        colColumn.column_header_name = colColumn.period + colColumn.column_name;
                        break;
                    case "CW":
                        WorkTime_tmp = Get_Current_Work_Time(strTime, 0);
                        colColumn.column_header_name = colColumn.period + colColumn.column_name;
                        break;
                    case "PW":
                        WorkTime_tmp = Get_Current_Work_Time(strTime, 7);
                        colColumn.column_header_name = colColumn.period + colColumn.column_name;
                        break;
                    case "CM":
                        WorkTime_tmp = Get_Current_Work_Time(strTime, 0);
                        colColumn.column_header_name = colColumn.period + colColumn.column_name;
                        break;
                    case "PM":
                        WorkTime_tmp = Get_Current_Work_Time(strTime, 0, 1);
                        colColumn.column_header_name = colColumn.period + colColumn.column_name;
                        break;
                }

                switch (colColumn.period)
                {
                    case "CS1":
                    case "CS2":
                    case "CS3":
                    case "PS1":
                    case "PS2":
                    case "PS3":
                    case "CS":
                    case "PS":
                    case "CD":
                    case "PD":
                        colColumn.query = "WHERE FACTORY = '" + cdvFactory.Text + "' \n";
                        colColumn.query += "AND WORK_MONTH = '" + WorkTime_tmp.work_date.Substring(0, 4) + WorkTime_tmp.work_month.ToString("00") + "' AND WORK_WEEK = '" + WorkTime_tmp.work_date.Substring(0, 4) + WorkTime_tmp.work_week.ToString("00") + "' AND WORK_DAYS = '" + WorkTime_tmp.work_days.ToString("000") + "' \n";
                        break;

                    case "CW":
                    case "PW":
                        colColumn.query = "WHERE FACTORY = '" + cdvFactory.Text + "' \n";
                        colColumn.query += "AND WORK_MONTH = '" + WorkTime_tmp.work_date.Substring(0, 4) + WorkTime_tmp.work_month.ToString("00") + "' AND WORK_WEEK = '" + WorkTime_tmp.work_date.Substring(0, 4) + WorkTime_tmp.work_week.ToString("00") + "' \n";
                        break;
                    case "CM":
                    case "PM":
                        colColumn.query = "WHERE FACTORY = '" + cdvFactory.Text + "' \n";
                        colColumn.query += "AND WORK_MONTH = '" + WorkTime_tmp.work_date.Substring(0, 4) + WorkTime_tmp.work_month.ToString("00") + "' \n";
                        break;
                }

                return true;
            }
            catch (Exception ex)
            {
                CmnFunction.ShowMsgBox(ex.ToString());
                return false;
            }
        }
        private string Make_Sub_Query(COLUMN colColumnList, int iCurrentIndex)
        {
            StringBuilder strQuery = new StringBuilder();

            int i;

            strQuery.Append("SELECT FACTORY, MAT_ID, MAT_VER, FLOW, FLOW_SEQ_NUM, OPER, LOT_TYPE, ORDER_ID, CM_KEY_1, CM_KEY_2, CM_KEY_3, CM_KEY_4, CM_KEY_5 \n");

            for (i = 0; i < colColumnList.count; i++)
            {
                if (i == iCurrentIndex)
                {
                    switch (colColumnList.column[i].period)
                    {
                        case "CS1":
                        case "CS2":
                        case "CS3":
                        case "PS1":
                        case "PS2":
                        case "PS3":
                            strQuery.AppendFormat(", NVL(SUM({0}), 0) AS {1} \n", colColumnList.column[i].column_header_name.Substring(1), colColumnList.column[i].column_header_name);
                            break;
                        case "CS":
                        case "PS":
                            strQuery.AppendFormat(", NVL(SUM({0}), 0) AS {0} \n", colColumnList.column[i].column_header_name);
                            break;
                        case "CD":
                        case "PD":
                        case "CW":
                        case "PW":
                        case "CM":
                        case "PM":
                            strQuery.AppendFormat(", NVL(SUM(S1_{0}), 0) + NVL(SUM(S2_{0}), 0) + NVL(SUM(S3_{0}), 0) + NVL(SUM(S4_{0}), 0) AS {1} \n", colColumnList.column[i].column_header_name.Substring(2), colColumnList.column[i].column_header_name);
                            break;
                    }
                }
                else
                {
                    strQuery.AppendFormat(", 0 AS {0} \n", colColumnList.column[i].column_header_name);
                }
            }

            if (colColumnList.column[iCurrentIndex].column_header_name.IndexOf("BOH") > -1)
            {
                strQuery.Append("FROM RSUMWIPBOH \n");
            }
            else if (colColumnList.column[iCurrentIndex].column_header_name.IndexOf("EOH") > -1)
            {
                strQuery.Append("FROM RSUMWIPBOH \n");
            }
            else
            {
                strQuery.Append("FROM RSUMWIPMOV \n");
            }

            strQuery.Append(colColumnList.column[iCurrentIndex].query);
            strQuery.Append("GROUP BY FACTORY, MAT_ID, MAT_VER, FLOW, FLOW_SEQ_NUM, OPER, LOT_TYPE, ORDER_ID, CM_KEY_1, CM_KEY_2, CM_KEY_3, CM_KEY_4, CM_KEY_5 \n");

            return strQuery.ToString();
        }

        #endregion

        private void udcFLXViewInquiry_Load(object sender, EventArgs e)
        {
            InitCodeView();

            CmnInitFunction.InitSpread(spdData);
            dtpDateTime.Visible = false;
        }

        private void btnView_Click(object sender, EventArgs e)
        {
            if (cdvFactory.Text.Trim() == "")
            {
                CmnFunction.ShowMsgBox(RptMessages.GetMessage("STD999", GlobalVariable.gcLanguage));
                cdvFactory.Focus();
                return ;
            }

            if (cdvInqGrp.Text.Trim() == "")
            {
                CmnFunction.ShowMsgBox(RptMessages.GetMessage("STD999", GlobalVariable.gcLanguage));
                cdvInqGrp.Focus();
                return;
            }

            if (cdvInqName.Text.Trim() == "")
            {
                CmnFunction.ShowMsgBox(RptMessages.GetMessage("STD999", GlobalVariable.gcLanguage));
                cdvInqName.Focus();
                return;
            }


            View_Inquiry();
        }

        private void cdvInqGrp_ButtonPress(object sender, EventArgs e)
        {
            CmnListFunction.ViewDataList(cdvInqGrp.GetListView, '1', cdvFactory.Text, GlobalConstant.MP_GCM_FLEXWIP_GROUP);
        }
        
        private void cdvInqName_ButtonPress(object sender, EventArgs e)
        {
            CmnListFunction.View_Inquiry_List(cdvInqName.GetListView, '2', cdvFactory.Text, cdvInqGrp.Text);
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
                CmnFunction.ShowMsgBox(ex.ToString());
            }
        }

        private void pnlTitle_MouseDown(object sender, MouseEventArgs e)
        {
            //For Debug
            if (e.Button == MouseButtons.Right)
            {
                if (e.Clicks == 2)
                {
                    dtpDateTime.Visible = true;
                }
            }
        }

    }
}

