using System;
using System.IO;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using Miracom.UI;
using Miracom.SmartWeb;
using Miracom.SmartWeb.UI;
using Miracom.SmartWeb.FWX;
using Miracom.SmartWeb.UI.Controls;

namespace Hana.TAT
{
    public partial class TAT050502 : Miracom.SmartWeb.UI.Controls.udcCUSReport002
    {

        /// <summary>
        /// 클  래  스: TAT050502<br/>
        /// 클래스요약: Dynamic TAT<br/>
        /// 작  성  자: 에이스텍 황종환<br/>
        /// 최초작성일: 2013-08-27<br/>
        /// 상세  설명: Dynamic TAT<br/>
        /// 변경  내용: <br/>
        /// 2013-08-28-황종환 : 월간 TAT를 구하는 부분을 쿼리로 할 경우 속도 저하가 심하여 월간 TAT는 UI상에서 계산하는 것으로 변경<br />
        /// 2013-08-29-황종환 : EQ기준 조회 기능 추가, GCM H_DENSITY 에서 MAT_GRP_7(KEY_1)정보로 DENSITY(DATA_1) 환산값을 구한다.<br />
        /// 2013-08-30-황종환 : 1. MAT_ID 조회 박스에 'HX%'를 기본으로 설정
        ///                     2. KPCS기준을 추가하고 기본 조회 값으로 설정
        ///                     3. WIP 기준 '-', 'Merge', 'Middle%'로
        /// 2013-08-31-황종환 : 버그 수정 게이트에 있는 재공이 화면에 표시가 안되 있었음.
        /// 2013-12-13-임종우 : INPUT 기준 변경 : Receive -> Issue (김권수 요청)
        /// 2014-10-16-임종우 : B/G 재공 포함 유무 체크 박스 추가 (임태성K 요청)
        /// </summary>
        /// 

        
        private static int groupCount = 5;
        private static int headerColumnCnt = 12;

        public TAT050502()
        {
            InitializeComponent();

            this.udcWIPCondition1.sFactory = GlobalVariable.gsAssyDefaultFactory;
            cdvDate.Value = DateTime.Now;
            this.SetFactory(GlobalVariable.gsAssyDefaultFactory);
            cdvFactory.Text = GlobalVariable.gsAssyDefaultFactory;
            cdvFactory.Enabled = false;

            cboTimeBase.SelectedIndex = 0;

            SortInit();
            GridColumnInit(); //헤더 한줄짜리
        }

        #region SortInit

        /// <summary>
        /// SortInit
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SortInit()
        {
            ((udcTableForm)(this.btnSort.BindingForm)).AddRow("Customer", "MAT.MAT_GRP_1", "MAT_GRP_1", "MAT_GRP_1", "DECODE(MAT_GRP_1,'SE','SEC','HX','HYNIX','IM','iML','FC','FCI','IG','IMAGIS' , (SELECT DATA_1 FROM MGCMTBLDAT@RPTTOMES WHERE FACTORY = '" + GlobalVariable.gsAssyDefaultFactory + "' AND TABLE_NAME = 'H_CUSTOMER' AND KEY_1 = MAT_GRP_1)) CUSTOMER", true);
            ((udcTableForm)(this.btnSort.BindingForm)).AddRow("Family", "MAT.MAT_GRP_2", "MAT_GRP_2", "MAT_GRP_2", "NVL(MAT_GRP_2, ' ')", false);
            ((udcTableForm)(this.btnSort.BindingForm)).AddRow("Package", "MAT.MAT_GRP_3", "MAT_GRP_3", "MAT_GRP_3", "NVL(MAT_GRP_3, ' ')", true);
            ((udcTableForm)(this.btnSort.BindingForm)).AddRow("Type1", "MAT.MAT_GRP_4", "MAT_GRP_4", "MAT_GRP_4", "NVL(MAT_GRP_4, ' ')", false);
            ((udcTableForm)(this.btnSort.BindingForm)).AddRow("Type2", "MAT.MAT_GRP_5", "MAT_GRP_5", "MAT_GRP_5", "NVL(MAT_GRP_5, ' ')", false);
            ((udcTableForm)(this.btnSort.BindingForm)).AddRow("LD Count", "MAT.MAT_GRP_6", "MAT_GRP_6", "MAT_GRP_6", "NVL(MAT_GRP_6, ' ')", true);
            ((udcTableForm)(this.btnSort.BindingForm)).AddRow("Density", "MAT.MAT_GRP_7", "MAT_GRP_7", "MAT_GRP_7", "NVL(MAT_GRP_7, ' ')", true);
            ((udcTableForm)(this.btnSort.BindingForm)).AddRow("Generation", "MAT.MAT_GRP_8", "MAT_GRP_8", "MAT_GRP_8", "NVL(MAT_GRP_8, ' ')", true);
            ((udcTableForm)(this.btnSort.BindingForm)).AddRow("PIN_TYPE", "MAT.MAT_CMF_10", "MAT_CMF_10", "MAT_CMF_10", "NVL(MAT_CMF_10, ' ')", false);
            ((udcTableForm)(this.btnSort.BindingForm)).AddRow("Product", "MAT.MAT_ID", "MAT_ID", "A.MAT_ID", "NVL(MAT_ID, ' ')", false);
            ((udcTableForm)(this.btnSort.BindingForm)).AddRow("PKG2", "MAT.MAT_GRP_10", "MAT_GRP_10", "MAT_GRP_10", "NVL(MAT_GRP_10, ' ')", false);
            ((udcTableForm)(this.btnSort.BindingForm)).AddRow("Process group", "F.OPER_GRP", "OPER_GRP", "OPER_GRP", "NVL(OPER_GRP, ' ')", false);
        }

        #endregion

        #region 한줄헤더생성

        /// <summary>
        /// 한줄헤더생성
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void GridColumnInit()
        {
            spdData.RPT_ColumnInit();
            spdData.RPT_AddBasicColumn("Customer", 0, 0, Visibles.True, Frozen.True, Align.Center, Merge.True, Formatter.String, 80);
            spdData.RPT_AddBasicColumn("Family", 0, 1, Visibles.False, Frozen.True, Align.Center, Merge.True, Formatter.String, 80);
            spdData.RPT_AddBasicColumn("Package", 0, 2, Visibles.False, Frozen.True, Align.Center, Merge.True, Formatter.String, 80);
            spdData.RPT_AddBasicColumn("Type1", 0, 3, Visibles.False, Frozen.True, Align.Center, Merge.True, Formatter.String, 80);
            spdData.RPT_AddBasicColumn("Type2", 0, 4, Visibles.False, Frozen.True, Align.Center, Merge.True, Formatter.String, 80);
            spdData.RPT_AddBasicColumn("LD Count", 0, 5, Visibles.False, Frozen.True, Align.Center, Merge.True, Formatter.String, 80);
            spdData.RPT_AddBasicColumn("Density", 0, 6, Visibles.False, Frozen.True, Align.Center, Merge.True, Formatter.String, 80);
            spdData.RPT_AddBasicColumn("Generation", 0, 7, Visibles.False, Frozen.True, Align.Center, Merge.True, Formatter.String, 80);
            spdData.RPT_AddBasicColumn("PIN TYPE", 0, 8, Visibles.True, Frozen.True, Align.Left, Merge.True, Formatter.String, 80);
            spdData.RPT_AddBasicColumn("Product", 0, 9, Visibles.True, Frozen.True, Align.Left, Merge.True, Formatter.String, 80);
            spdData.RPT_AddBasicColumn("PKG", 0, 10, Visibles.True, Frozen.True, Align.Left, Merge.True, Formatter.String, 80);
            spdData.RPT_AddBasicColumn("Process group", 0, 11, Visibles.True, Frozen.True, Align.Left, Merge.True, Formatter.String, 80);
            spdData.RPT_AddBasicColumn("Target TAT", 0, 12, Visibles.True, Frozen.True, Align.Center, Merge.True, Formatter.String, 80);
            spdData.RPT_AddBasicColumn("구분", 0, 13, Visibles.True, Frozen.True, Align.Left, Merge.True, Formatter.String, 80);

            //선택된 달의 일수에 맞게 컬럼 표시
            for (int i = 1; i <= Convert.ToInt32(cdvDate.Value.ToString("dd")); i++)
            {
                spdData.RPT_AddBasicColumn(string.Format("{0}.{1:D2}", cdvDate.Value.ToString("MM"), i), 0, 13 + i, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.String, 70);
            }
            spdData.RPT_ColumnConfigFromTable(btnSort); //Group항목이 있을경우 반드시 선업해줄것.
        }

        #endregion

        #region 조회

        /// <summary>
        /// 조회
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnView_Click(object sender, EventArgs e)
        {
            if (!CheckField()) return;

            DataTable dt = null;

            GridColumnInit();

            try
            {
                LoadingPopUp.LoadIngPopUpShow(this);
                spdData_Sheet1.RowCount = 0;

                this.Refresh();
                dt = CmnFunction.oComm.GetFuncDataTable("DYNAMIC", MakeSqlString());

                if (dt.Rows.Count == 0)
                {
                    dt.Dispose();
                    LoadingPopUp.LoadingPopUpHidden();
                    CmnFunction.ShowMsgBox(RptMessages.GetMessage("STD010", GlobalVariable.gcLanguage));
                    return;
                }

                spdData.DataSource = dt;
                spdData.RPT_AutoFit(false);

                // Cell 색상 변화
                ChangeCellColor();

                // 월간 TAT 계산
                CalculationMonthTAT();
                
                dt.Dispose();
            }
            catch (Exception ex)
            {
                LoadingPopUp.LoadingPopUpHidden();
                CmnFunction.ShowMsgBox(ex.Message);
            }
            finally
            {
                LoadingPopUp.LoadingPopUpHidden();
            }
        }

        // 월간 TAT 계산
        public void CalculationMonthTAT()
        {
            int rowCnt = spdData.ActiveSheet.Rows.Count;
            int colCnt = spdData.ActiveSheet.Columns.Count;
            int totalWIP = 0;
            int totalOutput = 0;
            for (int i = 1; i < rowCnt; i += groupCount)
            {
                //spdData.ActiveSheet.Cells[i, 11].Text
                for (int j = headerColumnCnt + 2; j < colCnt; j++)
                {
                    if (spdData.ActiveSheet.Cells[i + 2, j].Text.Trim() != "")
                    {
                        totalOutput += Convert.ToInt32(spdData.ActiveSheet.Cells[i + 2, j].Text.Replace(",","").Trim());
                    }
                    if (spdData.ActiveSheet.Cells[i + 3, j].Text.Trim() != "")
                    {
                        totalWIP += Convert.ToInt32(spdData.ActiveSheet.Cells[i + 3, j].Text.Replace(",", "").Trim());
                    }
                    if (totalOutput == 0)
                    {
                        spdData.ActiveSheet.Cells[i, j].Text = "";
                    }
                    else
                    {
                        if (chkTime.Checked == true) // 시간 단위
                        {
                            spdData.ActiveSheet.Cells[i, j].Text = string.Format("{0:f2}", (double)totalWIP / totalOutput * 24); 
                        }
                        else
                        {
                            spdData.ActiveSheet.Cells[i, j].Text = string.Format("{0:f2}", (double)totalWIP / totalOutput); 
                        }
                    }
                }
                totalWIP = 0;
                totalOutput = 0;
            }
        }

        // Cell 색상 변화
        private void ChangeCellColor()
        {
            udcTableForm tableForm = (udcTableForm)btnSort.BindingForm;
            string[] colList = tableForm.SelectedValueToQueryContainNull.Split(',');
            int[] subTotalIndexList = new int[2];
            int notNullColCnt = tableForm.SelectedValueToQuery.Split(',').Length;
            for (int i = 1, j = 0; i < colList.Length; i++)
            {
                if (!colList[i].Trim().Equals("' '"))
                {
                    subTotalIndexList[j] = i;
                    j++;
                    if (j == 2)
                    {
                        break;
                    }
                }
            }

            // 칼럼 고정(total sum fixed)
            spdData.Sheets[0].FrozenRowCount = groupCount;
            //  1. column header
            for (int i = 0; i < headerColumnCnt + 1; i++)
            {
                spdData.ActiveSheet.Columns[i].BackColor = Color.LemonChiffon;
            }
            //  2. grand total Cell Color setting
            for (int i = 0; i < groupCount; i++)
            {
                spdData.ActiveSheet.Rows[i].BackColor = System.Drawing.Color.FromArgb(((System.Byte)(255)), ((System.Byte)(233)), ((System.Byte)(204)));
            }
            //  3. 시트에 대한 색 지정, 콜스팬, 텍스트 지정
            int colCnt = spdData.ActiveSheet.Columns.Count;
            for (int i = 0; i < spdData.ActiveSheet.Rows.Count; i++)
            {
                if (spdData.ActiveSheet.Cells[i, subTotalIndexList[0]].Text.Trim().Equals("") && !spdData.ActiveSheet.Cells[i, 0].Text.Trim().Equals("")) // 1st total
                {
                    spdData.ActiveSheet.Rows[i].BackColor = System.Drawing.Color.FromArgb(((System.Byte)(222)), ((System.Byte)(236)), ((System.Byte)(242)));
                    spdData.ActiveSheet.Cells[i, subTotalIndexList[0]].Text = spdData.ActiveSheet.Cells[i, 0].Text + " TOTAL";
                    spdData.ActiveSheet.Cells[i, subTotalIndexList[1]].Text = "  ";
                }
                else if (spdData.ActiveSheet.Cells[i, subTotalIndexList[1]].Text.Trim().Equals("") && !spdData.ActiveSheet.Cells[i, 0].Text.Trim().Equals("")) // 2nd total
                {
                    spdData.ActiveSheet.Rows[i].BackColor = System.Drawing.Color.FromArgb(((System.Byte)(222)), ((System.Byte)(236)), ((System.Byte)(242)));
                    spdData.ActiveSheet.Cells[i, subTotalIndexList[1]].Text = spdData.ActiveSheet.Cells[i, subTotalIndexList[0]].Text + " TOTAL";
                }
                // 일간 월간 색 지정
                if (spdData.ActiveSheet.Cells[i, 13].Text.Equals("일간") )
                {
                    if (i < 5)
                    {
                        spdData.ActiveSheet.Cells[i, 13, i, colCnt - 1].BackColor = System.Drawing.Color.FromArgb(((System.Byte)(255)), ((System.Byte)(233)), ((System.Byte)(204)));                        
                    }
                    else
                    {
                        spdData.ActiveSheet.Cells[i, 13, i, colCnt - 1].BackColor = Color.LemonChiffon; 
                    }
                }
                // 일간 월간 색 지정
                if ( spdData.ActiveSheet.Cells[i, 13].Text.Equals("월간"))
                {
                    spdData.ActiveSheet.Rows[i].BackColor = System.Drawing.Color.FromArgb(((System.Byte)(227)), ((System.Byte)(107)), ((System.Byte)(10)));
                }
            }

            // grand total 관련 col,row span 설정
            for (int i = 0; i < groupCount; i++)
            {
                spdData.ActiveSheet.Cells[i, 0].ColumnSpan = headerColumnCnt + 1;
            }
            spdData.ActiveSheet.Cells[0, 0].RowSpan = groupCount;

            spdData.ActiveSheet.Cells[0, 0, 4, headerColumnCnt].Text = "TOTAL";
        }

        #region CheckField

        /// <summary>
        /// CheckField
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private Boolean CheckField()
        {
            if (cdvFactory.Text.Trim().Length == 0)
            {
                CmnFunction.ShowMsgBox(RptMessages.GetMessage("STD001", GlobalVariable.gcLanguage));
                return false;
            }

            return true;
        }

        #endregion

        #region MakeSqlString

        /// <summary>
        /// makeGroupingSetList
        /// 그룹핑 셋 문자열을 만든다.
        /// 
        /// </summary>
        /// <param name="columnList">칼럼 리스트</param>
        /// <param name="sDefaultColumn">기본적으로 그룹핑 셋에 포함할 칼럼 명 리스트</param>
        private string makeGroupingSetList(string columList, string sDefaultColumn)
        {
            string groupingSetValue = null;
            string[] groupingSetList = null;

            groupingSetList = columList.Split(',');

            // 총 4개의 그룹이 필요 : grand total, sub total 2 , detail data
            // detail data
            groupingSetValue += "(";
            for (int j = 0; j < groupingSetList.Length; j++) // grand total + subtotal 2 + detail data = 4
            {
                groupingSetValue = groupingSetValue + (j == 0 ? "" : ",") + groupingSetList[j];
            }
            groupingSetValue += ", " + sDefaultColumn + ")";
            if (groupingSetList.Length > 2)
            {
                // 2st sub total
                groupingSetValue += ", (";
                for (int j = 0; j < 2; j++) // grand total + subtotal 2 + detail data = 4
                {
                    groupingSetValue = groupingSetValue + (j == 0 ? "" : ",") + groupingSetList[j];
                }
                groupingSetValue += ", " + sDefaultColumn + ")";
            }
            if (groupingSetList.Length > 1)
            {
                // 1st sub total
                groupingSetValue += ", (";
                for (int j = 0; j < 1; j++) // grand total + subtotal 2 + detail data = 4
                {
                    groupingSetValue = groupingSetValue + (j == 0 ? "" : ",") + groupingSetList[j];
                }
                groupingSetValue += ", " + sDefaultColumn + ")";
            }
            // grand total
            groupingSetValue += ", (" + sDefaultColumn + ")";
            return groupingSetValue;
        }

        private string MakeSqlString()
        {
            StringBuilder strSqlString = new StringBuilder();

            string QueryCond1 = null;
            string QueryCond2 = null;
            string QueryCond3 = null;
            string QueryCond4 = null;
            string QueryCond1NotNull;
            string QueryCond2NotNull;
            string QueryCond3NotNull;
            string QueryCond4NotNull;
            string strOperGroupCond1 = "";
            string strOperGroupCond2 = "";
            string strOperGroupCond3 = "";
            //string strOperGroupCond4 = "";
            //string strOperGroupCond5 = "";

            string groupingSetValue = ""; // for subtotal 
            bool IsOperGroup = false; //공정그룹이 조건그룹에 포함되었는지 여부(조건에 공정그룹을 선택한 경우, 공정별 검색할 경우)

            udcTableForm tableForm = (udcTableForm)btnSort.BindingForm;

            QueryCond1 = tableForm.SelectedValueToQueryContainNull;
            QueryCond2 = tableForm.SelectedValue2ToQueryContainNull;
            QueryCond3 = tableForm.SelectedValue3ToQueryContainNull;
            QueryCond4 = tableForm.SelectedValue4ToQueryContainNull;
            QueryCond1NotNull = tableForm.SelectedValueToQuery; ;
            QueryCond2NotNull = tableForm.SelectedValue2ToQuery;
            QueryCond3NotNull = tableForm.SelectedValue3ToQuery;
            QueryCond4NotNull = tableForm.SelectedValue4ToQuery;

            #region 공정그룹 여부 판단 및 GroupCond 추가
            if (cdvOperGroup.Text != "ALL")
            {
                IsOperGroup = true;
            }

            if (!IsOperGroup)
            {
                if (QueryCond2.IndexOf("OPER_GRP") != -1)
                {
                    IsOperGroup = true;
                }
            }

            if (QueryCond2.IndexOf("OPER_GRP") == -1)
            {
                strOperGroupCond1 = ", F.OPER_GRP";
                strOperGroupCond2 = ", OPER_GRP";
                strOperGroupCond3 = ", OPER_GRP";
                //strOperGroupCond4 = ", NVL(OPER_GRP, ' ')";
                //strOperGroupCond5 = "";
            }
            #endregion 공정그룹 여부 판단 및 GroupCond 추가

            // 그룹핑 셋 리스트의 문자열 값   ex)    "(COL1,COL2,COL3,COL4,GUBUN), (COL1,COL2,GUBUN), (COL1,GUBUN), (GUBUN)"
            if (IsOperGroup)
            {
                groupingSetValue = makeGroupingSetList(QueryCond2NotNull + strOperGroupCond2, "GUBUN,TOTAL_SEQ");
            }
            else
            {
                groupingSetValue = makeGroupingSetList(QueryCond2NotNull, "GUBUN,TOTAL_SEQ");
            }

            // 시간 관련 셋팅
            string sYear = cdvDate.Value.ToString("yyyy");
            string sMonth = cdvDate.Value.ToString("MM");
            string sDay = cdvDate.Value.ToString("dd");
            string sToday = DateTime.Now.ToString("yyyyMMdd");
            Boolean isToday = false;

            // 선택한 날짜가 오늘인지 체크
            if (sToday == cdvDate.SelectedValue())
            {
                isToday = true;
            }
            else
            {
                isToday = false;
            }

            DateTime dt = new DateTime(cdvDate.Value.Year, cdvDate.Value.Month, 1);

            string sStartDate = dt.ToString("yyyyMMdd");
            string sEndDate = cdvDate.Value.ToString("yyyyMMdd");
            int iDate = cdvDate.Value.Day;
            
            // 오늘 날짜에 해당하는 데이터가 BOH에 있는지 확인
            DataTable dt1 = null;
            string existTodayWipData = "TRUE";
            if (isToday)
            {
                //if (cboTimeBase.Text == "06시")
                if(cboTimeBase.SelectedIndex == 0)
                {
                    existTodayWipData = "SELECT DECODE(COUNT(*), 1, 'TRUE', 'FALSE') FROM RWIPLOTSTS_BOH WHERE 1 =1 AND CUTOFF_DT = '" + sYear + sMonth + sDay + "06' AND ROWNUM = 1";
                }
                else
                {
                    existTodayWipData = "SELECT DECODE(COUNT(*), 1, 'TRUE', 'FALSE') FROM RWIPLOTSTS_BOH WHERE 1 =1 AND CUTOFF_DT = '" + sYear + sMonth + sDay + "22' AND ROWNUM = 1";
                }
                dt1 = CmnFunction.oComm.GetFuncDataTable("DYNAMIC", existTodayWipData);
                existTodayWipData = dt1.Rows[0][0].ToString();
            }

            // 쿼리

            if (IsOperGroup)
            {
                #region 공정그룹별일 경우 view 테이블

                strSqlString.Append("WITH TEMP_V AS (         \n");
                strSqlString.Append("SELECT TOTAL_SEQ, DECODE(TOTAL_SEQ, 1, '일간', 2, '월간', 3, 'INPUT', 4, 'OUTPUT', 5, 'WIP') GUBUN, " + QueryCond2 + strOperGroupCond2 + "         \n");
                for (int i = 0; i < iDate; i++)
                {
                    strSqlString.Append("     , SUM(DECODE(TO_CHAR(TOTAL_SEQ), SUBSTR(GUBUN, 1, 1), \"" + dt.AddDays(i).ToString("yyyyMMdd") + "\", 0))  \"" + dt.AddDays(i).ToString("yyyyMMdd") + "\"                          \n");
                }
                strSqlString.Append("  FROM (            \n");
                strSqlString.Append("            SELECT GUBUN, " + QueryCond2 + strOperGroupCond2 + "         \n");
                for (int i = 0; i < iDate; i++)
                {
                    strSqlString.Append("                 , DECODE(WORK_DATE, '" + dt.AddDays(i).ToString("yyyyMMdd") + "', DAILY, 0) \"" + dt.AddDays(i).ToString("yyyyMMdd") + "\"         \n");
                }
                strSqlString.Append("              FROM (         \n");
                strSqlString.Append("                    SELECT " + QueryCond3 + strOperGroupCond3 + "      , MAT_GRP_7 AS DENSITY    \n");
                strSqlString.Append("                        , WORK_DATE " + "\n");
                strSqlString.Append("                        , GUBUN " + "\n");
                
                if (chkEQ.Checked)
                {
                    strSqlString.Append("                        , SUM(QTY*DENSITY.VALUE) AS DAILY " + "\n");
                }
                else
                {
                    strSqlString.Append("                        , SUM(QTY) AS DAILY " + "\n");
                }
                strSqlString.Append("                     FROM (         \n");
                strSqlString.Append("                          SELECT FACTORY, MAT_ID, OPER, LOT_TYPE, MAT_VER, WORK_DATE, CM_KEY_3, DECODE(OPER_SEQ, 1, 'SAW', 2, 'SAW', 3, 'DA', 4, 'DA', 5, 'WB', 6, 'WB', 7, 'MOLD', 8, 'MOLD', 9, 'FINISH', 10, 'FINISH') OPER_GRP, DECODE(OPER_SEQ, 1, '3_INPUT', 2, '4_DAILY', 3, '3_INPUT', 4, '4_DAILY', 5, '3_INPUT', 6, '4_DAILY', 7, '3_INPUT', 8, '4_DAILY', 9, '3_INPUT', 10, '4_DAILY', '') GUBUN         \n");
                strSqlString.Append("                               , CASE WHEN OPER_SEQ=1 AND OPER IN ('A0040') THEN SAW_IN " + "\n");
                strSqlString.Append("                                      WHEN OPER_SEQ=2 AND OPER IN ('A0200', 'A0230') THEN SAW_OUT " + "\n");
                strSqlString.Append("                                      WHEN OPER_SEQ=3 AND OPER IN ('A0200', 'A0230') THEN DA_IN " + "\n");
                strSqlString.Append("                                      WHEN OPER_SEQ=4 AND (OPER LIKE 'A040%') THEN DA_OUT " + "\n");
                strSqlString.Append("                                      WHEN OPER_SEQ=5 AND (OPER LIKE 'A050%' OR OPER LIKE 'A053%') THEN WB_IN " + "\n");
                strSqlString.Append("                                      WHEN OPER_SEQ=6 AND OPER LIKE 'A060%' THEN WB_OUT " + "\n");
                strSqlString.Append("                                      WHEN OPER_SEQ=7 AND OPER LIKE 'A080%' THEN MOLD_IN " + "\n");
                strSqlString.Append("                                      WHEN OPER_SEQ=8 AND OPER IN ('A1000') THEN MOLD_OUT " + "\n");
                strSqlString.Append("                                      WHEN OPER_SEQ=9 AND OPER IN ('A1100', 'A1230') THEN FINISH_IN " + "\n");
                strSqlString.Append("                                      WHEN OPER_SEQ=10 AND OPER IN ('AZ010') THEN FINISH_OUT " + "\n");
                strSqlString.Append("                                      ELSE 0 " + "\n");
                strSqlString.Append("                                  END QTY " + "\n");
                strSqlString.Append("                            FROM (         \n");
                strSqlString.Append("                                 SELECT FACTORY, MAT_ID, OPER, LOT_TYPE, MAT_VER, WORK_DATE, CM_KEY_3         \n");
                strSqlString.Append("                                       , SUM(SAW_IN) AS SAW_IN " + "\n");
                strSqlString.Append("                                       , SUM(SAW_OUT) AS SAW_OUT " + "\n");
                strSqlString.Append("                                       , SUM(DA_IN) AS DA_IN " + "\n");
                strSqlString.Append("                                       , SUM(DA_OUT) AS DA_OUT " + "\n");
                strSqlString.Append("                                       , SUM(WB_IN) AS WB_IN " + "\n");
                strSqlString.Append("                                       , SUM(WB_OUT) AS WB_OUT " + "\n");
                strSqlString.Append("                                       , SUM(MOLD_IN) AS MOLD_IN " + "\n");
                strSqlString.Append("                                       , SUM(MOLD_OUT) AS MOLD_OUT " + "\n");
                strSqlString.Append("                                       , SUM(FINISH_IN) AS FINISH_IN " + "\n");
                strSqlString.Append("                                       , SUM(FINISH_OUT) FINISH_OUT " + "\n");
                strSqlString.Append("                                    FROM ( " + "\n");
                strSqlString.Append("                                         SELECT FACTORY, MAT_ID, OPER, LOT_TYPE, MAT_VER, WORK_DATE, CM_KEY_3 " + "\n");
                strSqlString.Append("                                              , SUM(CASE WHEN OPER IN ('A0040') THEN S1_END_QTY_1+S2_END_QTY_1+S3_END_QTY_1+S1_END_RWK_QTY_1 + S2_END_RWK_QTY_1 + S3_END_RWK_QTY_1 ELSE 0 END) SAW_IN " + "\n");
                strSqlString.Append("                                              , SUM(CASE WHEN OPER IN ('A0200', 'A0230') THEN S1_END_QTY_1+S2_END_QTY_1+S3_END_QTY_1+S1_END_RWK_QTY_1 + S2_END_RWK_QTY_1 + S3_END_RWK_QTY_1 ELSE 0 END) SAW_OUT " + "\n");
                strSqlString.Append("                                              , SUM(CASE WHEN OPER IN ('A0200', 'A0230') THEN S1_END_QTY_1+S2_END_QTY_1+S3_END_QTY_1+S1_END_RWK_QTY_1 + S2_END_RWK_QTY_1 + S3_END_RWK_QTY_1 ELSE 0 END) DA_IN " + "\n");
                strSqlString.Append("                                              , SUM(CASE WHEN OPER LIKE 'A040%' THEN S1_END_QTY_1+S2_END_QTY_1+S3_END_QTY_1+S1_END_RWK_QTY_1 + S2_END_RWK_QTY_1 + S3_END_RWK_QTY_1 ELSE 0 END) DA_OUT " + "\n");
                strSqlString.Append("                                              , SUM(CASE WHEN OPER LIKE 'A050%' OR OPER LIKE 'A053%' THEN S1_END_QTY_1+S2_END_QTY_1+S3_END_QTY_1+S1_END_RWK_QTY_1 + S2_END_RWK_QTY_1 + S3_END_RWK_QTY_1 ELSE 0 END) WB_IN " + "\n");
                strSqlString.Append("                                              , SUM(CASE WHEN OPER LIKE 'A060%' THEN S1_END_QTY_1+S2_END_QTY_1+S3_END_QTY_1+S1_END_RWK_QTY_1 + S2_END_RWK_QTY_1 + S3_END_RWK_QTY_1 ELSE 0 END) WB_OUT " + "\n");
                strSqlString.Append("                                              , SUM(CASE WHEN OPER LIKE 'A080%' THEN S1_END_QTY_1+S2_END_QTY_1+S3_END_QTY_1+S1_END_RWK_QTY_1 + S2_END_RWK_QTY_1 + S3_END_RWK_QTY_1 ELSE 0 END) MOLD_IN " + "\n");
                strSqlString.Append("                                              , SUM(CASE WHEN OPER IN ('A1000') THEN S1_END_QTY_1+S2_END_QTY_1+S3_END_QTY_1+S1_END_RWK_QTY_1 + S2_END_RWK_QTY_1 + S3_END_RWK_QTY_1 ELSE 0 END) MOLD_OUT " + "\n");
                strSqlString.Append("                                              , SUM(CASE WHEN OPER IN ('A1100', 'A1230') THEN S1_END_QTY_1+S2_END_QTY_1+S3_END_QTY_1+S1_END_RWK_QTY_1 + S2_END_RWK_QTY_1 + S3_END_RWK_QTY_1 ELSE 0 END) FINISH_IN " + "\n");
                strSqlString.Append("                                              , SUM(CASE WHEN OPER IN ('AZ010') THEN S1_END_QTY_1+S2_END_QTY_1+S3_END_QTY_1+S1_END_RWK_QTY_1 + S2_END_RWK_QTY_1 + S3_END_RWK_QTY_1 ELSE 0 END) FINISH_OUT " + "\n");

                //if (cboTimeBase.Text == "06시")
                if(cboTimeBase.SelectedIndex == 0)
                {
                    strSqlString.Append("                                           FROM CSUMWIPMOV          \n");
                }
                else
                {
                    strSqlString.Append("                                           FROM RSUMWIPMOV          \n");
                }

                strSqlString.Append("                                  WHERE OPER NOT IN ('AZ010')         \n");
                strSqlString.Append("                                  GROUP BY FACTORY, MAT_ID, OPER, LOT_TYPE, MAT_VER, WORK_DATE, CM_KEY_3         \n");
                strSqlString.Append("                                 UNION ALL         \n");
                strSqlString.Append("                                 SELECT CM_KEY_1 AS FACTORY, MAT_ID         \n");
                strSqlString.Append("                                      , DECODE(CM_KEY_1,'" + GlobalVariable.gsAssyDefaultFactory + "','AZ010','" + GlobalVariable.gsTestDefaultFactory + "','TZ010','FGS','F0000','HMKE1','EZ010','HMKS1','SZ010') OPER         \n");
                strSqlString.Append("                                      , LOT_TYPE, MAT_VER,  WORK_DATE,CM_KEY_3         \n");
                strSqlString.Append("                                              , 0 SAW_IN " + "\n");
                strSqlString.Append("                                              , 0 SAW_OUT " + "\n");
                strSqlString.Append("                                              , 0 DA_IN " + "\n");
                strSqlString.Append("                                              , 0 DA_OUT " + "\n");
                strSqlString.Append("                                              , 0 WB_IN " + "\n");
                strSqlString.Append("                                              , 0 WB_OUT " + "\n");
                strSqlString.Append("                                              , 0 MOLD_IN " + "\n");
                strSqlString.Append("                                              , 0 MOLD_OUT " + "\n");
                strSqlString.Append("                                              , 0 FINISH_IN " + "\n");
                strSqlString.Append("                                              , SUM(S1_FAC_OUT_QTY_1+S2_FAC_OUT_QTY_1+S3_FAC_OUT_QTY_1) FINISH_OUT " + "\n");

                //if (cboTimeBase.Text == "06시")
                if (cboTimeBase.SelectedIndex == 0)
                {
                    strSqlString.Append("                                   FROM CSUMFACMOV          \n");
                }
                else
                {
                    strSqlString.Append("                                   FROM RSUMFACMOV          \n");
                }
                strSqlString.Append("                                  WHERE FACTORY NOT IN ('RETURN')         \n");
                strSqlString.Append("                                  GROUP BY CM_KEY_1, MAT_ID, OPER, LOT_TYPE, MAT_VER, WORK_DATE, CM_KEY_3         \n");
                strSqlString.Append("                                 )         \n");
                strSqlString.Append("                           GROUP BY FACTORY, MAT_ID, OPER, LOT_TYPE, MAT_VER, WORK_DATE, CM_KEY_3         \n");
                strSqlString.Append("                                 ), (SELECT ROWNUM OPER_SEQ FROM DUAL CONNECT BY LEVEL <= 10) SEQ  " + "\n");
                strSqlString.Append("                          )A         \n");
                strSqlString.Append("                          , MWIPMATDEF B         \n");
                strSqlString.Append("                          , (SELECT KEY_1 AS KEY, DATA_3 AS VALUE         \n");
                strSqlString.Append("                               FROM MGCMTBLDAT         \n");
                strSqlString.Append("                              WHERE FACTORY = '" + GlobalVariable.gsAssyDefaultFactory + "'         \n");
                strSqlString.Append("                                AND TABLE_NAME = 'H_DENSITY') DENSITY            \n");
                strSqlString.Append("                     WHERE 1=1          \n");
                strSqlString.Append("                       AND A.FACTORY  = '" + GlobalVariable.gsAssyDefaultFactory + "'          \n");
                strSqlString.Append("                       AND A.FACTORY = B.FACTORY          \n");
                strSqlString.Append("                       AND A.MAT_ID = B.MAT_ID                         \n");
                strSqlString.Append("                       AND A.MAT_VER = 1          \n");
                strSqlString.Append("                       AND B.MAT_VER = 1          \n");
                strSqlString.Append("                       AND B.MAT_TYPE = 'FG'          \n");
                strSqlString.Append("                       AND A.MAT_ID LIKE '%'           \n");
                /*
                if (cdvLotType.txtValue != "" && cdvLotType.txtValue != "ALL")
                {
                    strSqlString.Append("                       AND A.CM_KEY_3 " + cdvLotType.SelectedValueToQueryString + "\n");
                }
                */
                if (cdvLotType.Text != "ALL")
                {
                    strSqlString.Append("                       AND A.CM_KEY_3 LIKE '" + cdvLotType.Text + "'" + "\n");
                }


                strSqlString.Append("                       AND A.OPER NOT IN ('00001','00002')          \n");
                strSqlString.Append("                       AND A.WORK_DATE BETWEEN '" + sStartDate + "' AND '" + sEndDate + "'          \n");
                strSqlString.Append("                       AND B.MAT_ID LIKE '" + txtSearchProduct.Text + "'" + "\n");
                strSqlString.Append("                       AND B.MAT_GRP_3 <> '-'" + "\n");
                strSqlString.Append("                       AND DENSITY.KEY = B.MAT_GRP_7         \n");
                // 2013. 08. 30 WIP 기준 추가
                strSqlString.Append("                       AND (B.MAT_GRP_5 IN ('-','Merge')  OR B.MAT_GRP_5 LIKE 'Middle%')         \n");

                #region 상세조회
                //상세조회
                if (udcWIPCondition1.Text != "ALL" && udcWIPCondition1.Text != "")
                    strSqlString.AppendFormat("                       AND B.MAT_GRP_1 {0} " + "\n", udcWIPCondition1.SelectedValueToQueryString);
                if (udcWIPCondition2.Text != "ALL" && udcWIPCondition2.Text != "")
                    strSqlString.AppendFormat("                       AND B.MAT_GRP_2 {0} " + "\n", udcWIPCondition2.SelectedValueToQueryString);
                if (udcWIPCondition3.Text != "ALL" && udcWIPCondition3.Text != "")
                    strSqlString.AppendFormat("                       AND B.MAT_GRP_3 {0} " + "\n", udcWIPCondition3.SelectedValueToQueryString);
                if (udcWIPCondition4.Text != "ALL" && udcWIPCondition4.Text != "")
                    strSqlString.AppendFormat("                       AND B.MAT_GRP_4 {0} " + "\n", udcWIPCondition4.SelectedValueToQueryString);
                if (udcWIPCondition5.Text != "ALL" && udcWIPCondition5.Text != "")
                    strSqlString.AppendFormat("                       AND B.MAT_GRP_5 {0} " + "\n", udcWIPCondition5.SelectedValueToQueryString);
                if (udcWIPCondition6.Text != "ALL" && udcWIPCondition6.Text != "")
                    strSqlString.AppendFormat("                       AND B.MAT_GRP_6 {0} " + "\n", udcWIPCondition6.SelectedValueToQueryString);
                if (udcWIPCondition7.Text != "ALL" && udcWIPCondition7.Text != "")
                    strSqlString.AppendFormat("                       AND B.MAT_GRP_7 {0} " + "\n", udcWIPCondition7.SelectedValueToQueryString);
                if (udcWIPCondition8.Text != "ALL" && udcWIPCondition8.Text != "")
                    strSqlString.AppendFormat("                       AND B.MAT_GRP_8 {0} " + "\n", udcWIPCondition8.SelectedValueToQueryString);
                if (udcWIPCondition9.Text != "ALL" && udcWIPCondition9.Text != "")
                    strSqlString.AppendFormat("                       AND B.MAT_GRP_9 {0} " + "\n", udcWIPCondition9.SelectedValueToQueryString);
                #endregion

                strSqlString.Append("                     GROUP BY " + QueryCond3 + strOperGroupCond3 + " , MAT_GRP_7, WORK_DATE, GUBUN         \n");
                strSqlString.Append("    )         \n");
                strSqlString.Append("    UNION ALL         \n");
                //strSqlString.Append("    /* 재공 */         \n");
                strSqlString.Append("    SELECT '5_WIP' GUBUN, " + QueryCond2 + strOperGroupCond2 + "    \n");
                for (int i = 0; i < iDate; i++)
                {
                    strSqlString.Append("         , SUM( DECODE(WORK_DATE, '" + dt.AddDays(i).ToString("yyyyMMdd") + "', TTL, 0)) \"" + dt.AddDays(i).ToString("yyyyMMdd") + "\"         \n");
                }
                strSqlString.Append("      FROM (         \n");
                strSqlString.Append("        SELECT " + QueryCond1 + strOperGroupCond1 + "      , MAT.MAT_GRP_7 AS DENSITY   \n");
                strSqlString.Append("             , WORK_DATE         \n");
                if (chkEQ.Checked)
                {
                    strSqlString.Append("             , SUM(NVL((CASE WHEN MAT.MAT_CMF_11 IN (SELECT DISTINCT(MAT_CMF_11) FROM MWIPMATDEF WHERE FACTORY = '" + GlobalVariable.gsAssyDefaultFactory + "' AND MAT_GRP_5 = '1st' AND MAT_ID LIKE 'SEKS%') THEN DECODE(MAT.MAT_GRP_5, '2nd', NVL(F.V0,0), 'Merge', NVL(F.V0,0), 0)         \n");
                    strSqlString.Append("                             WHEN MAT_GRP_4 NOT IN ('-','FD','FU') AND MAT_GRP_5 <> '-' THEN CASE WHEN MAT.MAT_GRP_5 IN ('1st','Merge') OR MAT_GRP_5 LIKE 'Middle%' THEN NVL(F.V0,0) ELSE 0 END         \n");
                    strSqlString.Append("                             ELSE NVL(F.V0,0)         \n");
                    strSqlString.Append("                        END),0)*DENSITY.VALUE) AS TTL         \n");
                }
                else
                {
                    strSqlString.Append("             , SUM(NVL((CASE WHEN MAT.MAT_CMF_11 IN (SELECT DISTINCT(MAT_CMF_11) FROM MWIPMATDEF WHERE FACTORY = '" + GlobalVariable.gsAssyDefaultFactory + "' AND MAT_GRP_5 = '1st' AND MAT_ID LIKE 'SEKS%') THEN DECODE(MAT.MAT_GRP_5, '2nd', NVL(F.V0,0), 'Merge', NVL(F.V0,0), 0)         \n");
                    strSqlString.Append("                             WHEN MAT_GRP_4 NOT IN ('-','FD','FU') AND MAT_GRP_5 <> '-' THEN CASE WHEN MAT.MAT_GRP_5 IN ('1st','Merge') OR MAT_GRP_5 LIKE 'Middle%' THEN NVL(F.V0,0) ELSE 0 END         \n");
                    strSqlString.Append("                             ELSE NVL(F.V0,0)         \n");
                    strSqlString.Append("                        END),0)) AS TTL         \n");
                }
                strSqlString.Append("          FROM (         \n");
                strSqlString.Append("                SELECT *         \n");
                strSqlString.Append("                  FROM MWIPMATDEF MAT          \n");
                strSqlString.Append("                 WHERE 1 = 1          \n");
                strSqlString.Append("                   AND MAT.FACTORY = '" + GlobalVariable.gsAssyDefaultFactory + "'          \n");
                strSqlString.Append("                   AND MAT.DELETE_FLAG <> 'Y'          \n");
                // 2013. 08. 30 WIP 기준 추가
                strSqlString.Append("                   AND (MAT_GRP_5 IN ('-','Merge')  OR MAT_GRP_5 LIKE 'Middle%')          \n");
                strSqlString.Append("               ) MAT           \n");
                strSqlString.Append("             , (          \n");
                strSqlString.Append("                SELECT LOT.MAT_ID, MAT.MAT_GRP_3, LOT.OPER_GRP, WORK_DATE          \n");
                strSqlString.Append("                     , SUM(DECODE(MAT.MAT_GRP_3,'COB',ROUND(QTY/TO_NUMBER(DECODE(MAT.MAT_CMF_13,' ',1,MAT.MAT_CMF_13)),0),'BGN',ROUND(QTY/TO_NUMBER(DECODE(MAT.MAT_CMF_13,' ',1,MAT.MAT_CMF_13)),0),QTY)) V0          \n");
                strSqlString.Append("                  FROM (           \n");
                strSqlString.Append("                        SELECT FACTORY, MAT_ID, OPER_GRP_1, OPER_GRP, WORK_DATE         \n");
                strSqlString.Append("                             , SUM(CASE WHEN OPER <= 'A0395' THEN QTY_1 / NVL(COMP_CNT,1)          \n");
                strSqlString.Append("                                        ELSE QTY_1          \n");
                strSqlString.Append("                                   END) QTY          \n");
                strSqlString.Append("                          FROM (          \n");
                strSqlString.Append("                                SELECT A.FACTORY, A.MAT_ID, B.OPER_GRP_1, B.OPER, SUBSTR(A.CUTOFF_DT,1,8) WORK_DATE, A.QTY_1          \n");
                strSqlString.Append("                                     , CASE WHEN OPER_GRP_1 IN ('B/G', 'SAW', 'Back Side Coating') THEN 'SAW' " + "\n");
                strSqlString.Append("                                            WHEN OPER_GRP_1 IN ('D/A') THEN 'DA' " + "\n");
                strSqlString.Append("                                            WHEN OPER_GRP_1 IN ('W/B') THEN 'WB' " + "\n");
                strSqlString.Append("                                            WHEN OPER_GRP_1 IN ('MOLD', 'CURE') THEN 'MOLD' " + "\n");
                strSqlString.Append("                                            WHEN OPER_GRP_1 IN ('M/K', 'V/I', 'TRIM', 'S/B/A', 'TIN', 'SIG', 'AVI', 'HMK3A') THEN 'FINISH' " + "\n");
                strSqlString.Append("                                        END OPER_GRP " + "\n");
                strSqlString.Append("                                     , (SELECT DATA_1 FROM MGCMTBLDAT WHERE FACTORY = '" + GlobalVariable.gsAssyDefaultFactory + "' AND TABLE_NAME IN ('H_SEC_AUTO_LOSS','H_HX_AUTO_LOSS') AND KEY_1 = A.MAT_ID) AS COMP_CNT           \n");
                if (isToday && existTodayWipData == "FALSE")
                {
                    //if (cboTimeBase.Text == "06시")
                    if (cboTimeBase.SelectedIndex == 0)
                    {
                        strSqlString.Append("                                  FROM (SELECT FACTORY, MAT_ID, OPER, LOT_TYPE, LOT_DEL_FLAG, LOT_CMF_5, CUTOFF_DT, QTY_1  FROM RWIPLOTSTS_BOH UNION ALL SELECT FACTORY, MAT_ID, OPER, LOT_TYPE, LOT_DEL_FLAG, LOT_CMF_5, '" + sToday + "'||'06', QTY_1 FROM RWIPLOTSTS) A          \n");
                    }
                    else
                    {
                        strSqlString.Append("                                  FROM (SELECT FACTORY, MAT_ID, OPER, LOT_TYPE, LOT_DEL_FLAG, LOT_CMF_5, CUTOFF_DT, QTY_1  FROM RWIPLOTSTS_BOH UNION ALL SELECT FACTORY, MAT_ID, OPER, LOT_TYPE, LOT_DEL_FLAG, LOT_CMF_5, '" + sToday + "'||'22', QTY_1 FROM RWIPLOTSTS) A          \n");
                    }

                }
                else
                {
                    strSqlString.Append("                                  FROM (SELECT FACTORY, MAT_ID, OPER, LOT_TYPE, LOT_DEL_FLAG, LOT_CMF_5, CUTOFF_DT, QTY_1  FROM RWIPLOTSTS_BOH) A          \n");
                }
                strSqlString.Append("                                     , MWIPOPRDEF B          \n");
                strSqlString.Append("                                 WHERE 1 = 1         \n");
                strSqlString.Append("                                   AND A.CUTOFF_DT IN(");
                for (int i = 0; i < iDate; i++)
                {
                    //if (cboTimeBase.Text == "06시")
                    if (cboTimeBase.SelectedIndex == 0)
                    {
                        strSqlString.Append(" " + (i == 0 ? "" : ",") + " '" + dt.AddDays(i).ToString("yyyyMMdd") + "06'");
                    }
                    else
                    {
                        strSqlString.Append(" " + (i == 0 ? "" : ",") + " '" + dt.AddDays(i).ToString("yyyyMMdd") + "22'");
                    }

                }
                strSqlString.Append("                                                     )         \n");
                strSqlString.Append("                                   AND A.FACTORY = B.FACTORY(+)           \n");
                strSqlString.Append("                                   AND A.OPER = B.OPER(+)           \n");
                strSqlString.Append("                                   AND A.FACTORY = '" + GlobalVariable.gsAssyDefaultFactory + "'           \n");
                strSqlString.Append("                                   AND A.LOT_TYPE = 'W'          \n");
                strSqlString.Append("                                   AND A.LOT_DEL_FLAG = ' '          \n");
                /*
                if (cdvLotType.txtValue != "" && cdvLotType.txtValue != "ALL")
                {
                    strSqlString.Append("                                   AND A.LOT_CMF_5 " + cdvLotType.SelectedValueToQueryString + "\n");
                }
                */
                if (cdvLotType.Text != "ALL")
                {
                    strSqlString.Append("                                   AND A.LOT_CMF_5 LIKE '" + cdvLotType.Text + "'" + "\n");
                }

                // 2014-10-16-임종우 : BG 공정 포함 유무 선택 가능하도록..
                if (ckbBG.Checked == true)
                {
                    strSqlString.Append("                                   AND A.OPER <> 'A0020' " + "\n");
                }
                else
                {
                    strSqlString.Append("                                   AND A.OPER NOT IN ('A0020', 'A0040') " + "\n");
                }

                strSqlString.Append("                               )          \n");
                strSqlString.Append("                         GROUP BY FACTORY, MAT_ID, OPER_GRP_1, OPER_GRP, WORK_DATE          \n");
                strSqlString.Append("                       ) LOT          \n");
                strSqlString.Append("                     , MWIPMATDEF MAT          \n");
                strSqlString.Append("                 WHERE 1 = 1          \n");
                strSqlString.Append("                   AND LOT.FACTORY = MAT.FACTORY          \n");
                strSqlString.Append("                   AND LOT.MAT_ID = MAT.MAT_ID          \n");
                strSqlString.Append("                   AND MAT.DELETE_FLAG <> 'Y'          \n");
                strSqlString.Append("                   AND MAT.MAT_GRP_2 <> '-'          \n");
                strSqlString.Append("                 GROUP BY LOT.MAT_ID ,MAT.MAT_GRP_3, LOT.OPER_GRP, WORK_DATE         \n");
                strSqlString.Append("               ) F         \n");

                strSqlString.Append("             , (SELECT KEY_1 AS KEY, DATA_3 AS VALUE         \n");
                strSqlString.Append("                  FROM MGCMTBLDAT         \n");
                strSqlString.Append("                 WHERE FACTORY = '" + GlobalVariable.gsAssyDefaultFactory + "'         \n");
                strSqlString.Append("                   AND TABLE_NAME = 'H_DENSITY') DENSITY          \n");
                strSqlString.Append("         WHERE 1 = 1          \n");
                strSqlString.Append("           AND MAT.MAT_ID = F.MAT_ID(+)         \n");
                strSqlString.Append("           AND MAT.MAT_ID LIKE '" + txtSearchProduct.Text + "'" + "\n");
                strSqlString.Append("           AND MAT.MAT_GRP_3 <> '-'" + "\n");
                strSqlString.Append("           AND MAT.MAT_GRP_7 = DENSITY.KEY         \n");
                #region 상세조회
                //상세조회
                if (udcWIPCondition1.Text != "ALL" && udcWIPCondition1.Text != "")
                    strSqlString.AppendFormat("           AND MAT.MAT_GRP_1 {0} " + "\n", udcWIPCondition1.SelectedValueToQueryString);
                if (udcWIPCondition2.Text != "ALL" && udcWIPCondition2.Text != "")
                    strSqlString.AppendFormat("           AND MAT.MAT_GRP_2 {0} " + "\n", udcWIPCondition2.SelectedValueToQueryString);
                if (udcWIPCondition3.Text != "ALL" && udcWIPCondition3.Text != "")
                    strSqlString.AppendFormat("           AND MAT.MAT_GRP_3 {0} " + "\n", udcWIPCondition3.SelectedValueToQueryString);
                if (udcWIPCondition4.Text != "ALL" && udcWIPCondition4.Text != "")
                    strSqlString.AppendFormat("           AND MAT.MAT_GRP_4 {0} " + "\n", udcWIPCondition4.SelectedValueToQueryString);
                if (udcWIPCondition5.Text != "ALL" && udcWIPCondition5.Text != "")
                    strSqlString.AppendFormat("           AND MAT.MAT_GRP_5 {0} " + "\n", udcWIPCondition5.SelectedValueToQueryString);
                if (udcWIPCondition6.Text != "ALL" && udcWIPCondition6.Text != "")
                    strSqlString.AppendFormat("           AND MAT.MAT_GRP_6 {0} " + "\n", udcWIPCondition6.SelectedValueToQueryString);
                if (udcWIPCondition7.Text != "ALL" && udcWIPCondition7.Text != "")
                    strSqlString.AppendFormat("           AND MAT.MAT_GRP_7 {0} " + "\n", udcWIPCondition7.SelectedValueToQueryString);
                if (udcWIPCondition8.Text != "ALL" && udcWIPCondition8.Text != "")
                    strSqlString.AppendFormat("           AND MAT.MAT_GRP_8 {0} " + "\n", udcWIPCondition8.SelectedValueToQueryString);
                if (udcWIPCondition9.Text != "ALL" && udcWIPCondition9.Text != "")
                    strSqlString.AppendFormat("           AND MAT.MAT_GRP_9 {0} " + "\n", udcWIPCondition9.SelectedValueToQueryString);
                #endregion

                strSqlString.Append("         GROUP BY " + QueryCond1 + strOperGroupCond1 + ",  MAT.MAT_GRP_7, WORK_DATE         \n");
                strSqlString.Append("        HAVING (         \n");
                strSqlString.Append("                NVL(SUM(F.V0), 0)           \n");
                strSqlString.Append("               ) <> 0                 \n");
                strSqlString.Append("         )                    \n");
                strSqlString.Append("        GROUP BY " + QueryCond2 + strOperGroupCond2 + "         \n");
                strSqlString.Append("    ) , (SELECT ROWNUM TOTAL_SEQ FROM DUAL CONNECT BY LEVEL <= 5) SEQ             \n");

                if (cdvOperGroup.Text != "ALL")
                    strSqlString.Append(" WHERE OPER_GRP = '" + cdvOperGroup.Text + "' " + "\n");
                
                strSqlString.Append(" GROUP BY TOTAL_SEQ, DECODE(TOTAL_SEQ, 1, '일간', 2, '월간', 3, 'INPUT', 4, 'OUTPUT', 5, 'WIP') , " + QueryCond2 + strOperGroupCond2 + "                  \n");
                strSqlString.Append(")         \n");
                #endregion 공정그룹별일 경우 view 테이블
            }
            else
            {
                #region 공정그룹별이 아닌 경우 view 테이블

                strSqlString.Append("WITH TEMP_V AS (         \n");
                strSqlString.Append("SELECT TOTAL_SEQ, DECODE(TOTAL_SEQ, 1, '일간', 2, '월간', 3, 'INPUT', 4, 'OUTPUT', 5, 'WIP') GUBUN, " + QueryCond2 + "         \n");
                for (int i = 0; i < iDate; i++)
                {
                    strSqlString.Append("     , SUM(DECODE(TO_CHAR(TOTAL_SEQ), SUBSTR(GUBUN, 1, 1), \"" + dt.AddDays(i).ToString("yyyyMMdd") + "\", 0))  \"" + dt.AddDays(i).ToString("yyyyMMdd") + "\"                          \n");
                }
                strSqlString.Append("  FROM (            \n");
                // input, output, output 누계 
                strSqlString.Append("    SELECT DECODE(OPER,'A0000', '3_INPUT', 'AZ010', '4_DAILY','') GUBUN, " + QueryCond2 + "         \n");
                for (int i = 0; i < iDate; i++)
                {
                    strSqlString.Append("         , SUM(\"" + dt.AddDays(i).ToString("yyyyMMdd") + "\") \"" + dt.AddDays(i).ToString("yyyyMMdd") + "\"         \n");
                }
                strSqlString.Append("      FROM (         \n");
                strSqlString.Append("            SELECT OPER, " + QueryCond2 + "         \n");
                for (int i = 0; i < iDate; i++)
                {
                    strSqlString.Append("                 , DECODE(WORK_DATE, '" + dt.AddDays(i).ToString("yyyyMMdd") + "', DAILY, 0) \"" + dt.AddDays(i).ToString("yyyyMMdd") + "\"         \n");
                }
                strSqlString.Append("              FROM (         \n");
                strSqlString.Append("                    SELECT " + QueryCond3 + "      , MAT_GRP_7 AS DENSITY    \n");
                strSqlString.Append("                        , WORK_DATE         \n");
                strSqlString.Append("                        , OPER         \n");
                if (chkEQ.Checked)
                {
                    strSqlString.Append("                        , SUM((CASE WHEN OPER IN ('AZ010','SHIP','TZ010','F0000','EZ010', 'SZ010') THEN SHIP_QTY_1*DENSITY.VALUE ELSE END_QTY_1*DENSITY.VALUE END)) DAILY         \n");
                }
                else
                {
                    strSqlString.Append("                        , SUM((CASE WHEN OPER IN ('AZ010','SHIP','TZ010','F0000','EZ010', 'SZ010') THEN SHIP_QTY_1 ELSE END_QTY_1 END)) DAILY         \n");
                }
                strSqlString.Append("                     FROM (         \n");
                strSqlString.Append("                          SELECT FACTORY, MAT_ID, OPER, LOT_TYPE, MAT_VER, WORK_DATE, CM_KEY_3         \n");
                strSqlString.Append("                               , SUM(END_LOT) AS END_LOT         \n");
                strSqlString.Append("                               , SUM(END_QTY_1) AS END_QTY_1         \n");
                strSqlString.Append("                               , SUM(END_QTY_2) AS END_QTY_2         \n");
                strSqlString.Append("                               , SUM(SHIP_QTY_1) AS SHIP_QTY_1         \n");
                strSqlString.Append("                               , SUM(SHIP_QTY_2) AS SHIP_QTY_2         \n");
                strSqlString.Append("                            FROM (         \n");
                strSqlString.Append("                                 SELECT FACTORY, MAT_ID, OPER, LOT_TYPE, MAT_VER, WORK_DATE, CM_KEY_3         \n");
                strSqlString.Append("                                      , SUM(S1_END_LOT+S2_END_LOT+S3_END_LOT) END_LOT         \n");
                strSqlString.Append("                                      , SUM(S1_END_QTY_1+S2_END_QTY_1+S3_END_QTY_1) END_QTY_1         \n");
                strSqlString.Append("                                      , SUM(S1_END_QTY_2+S2_END_QTY_2+S3_END_QTY_2) END_QTY_2         \n");
                strSqlString.Append("                                      , 0 SHIP_QTY_1         \n");
                strSqlString.Append("                                      , 0 SHIP_QTY_2         \n");
                //if (cboTimeBase.Text == "06시")
                if (cboTimeBase.SelectedIndex == 0)
                {
                    strSqlString.Append("                                   FROM CSUMWIPMOV          \n");
                }
                else
                {
                    strSqlString.Append("                                   FROM RSUMWIPMOV          \n");
                }

                strSqlString.Append("                                  WHERE OPER = 'A0000'         \n");
                strSqlString.Append("                                  GROUP BY FACTORY, MAT_ID, OPER, LOT_TYPE, MAT_VER, WORK_DATE, CM_KEY_3         \n");
                strSqlString.Append("                                 UNION ALL         \n");
                strSqlString.Append("                                 SELECT CM_KEY_1 AS FACTORY, MAT_ID         \n");
                strSqlString.Append("                                      , DECODE(CM_KEY_1,'" + GlobalVariable.gsAssyDefaultFactory + "','AZ010','" + GlobalVariable.gsTestDefaultFactory + "','TZ010','FGS','F0000','HMKE1','EZ010','HMKS1','SZ010') OPER         \n");
                strSqlString.Append("                                      , LOT_TYPE, MAT_VER,  WORK_DATE,CM_KEY_3         \n");
                strSqlString.Append("                                      , 0 END_LOT         \n");
                strSqlString.Append("                                      , 0 END_QTY_1         \n");
                strSqlString.Append("                                      , 0 END_QTY_2         \n");
                strSqlString.Append("                                      , SUM(S1_FAC_OUT_QTY_1+S2_FAC_OUT_QTY_1+S3_FAC_OUT_QTY_1) SHIP_QTY_1         \n");
                strSqlString.Append("                                      , SUM(S1_FAC_OUT_QTY_2+S2_FAC_OUT_QTY_2+S3_FAC_OUT_QTY_2) SHIP_QTY_2         \n");
                //if (cboTimeBase.Text == "06시")
                if (cboTimeBase.SelectedIndex == 0)
                {
                    strSqlString.Append("                                   FROM CSUMFACMOV          \n");
                }
                else
                {
                    strSqlString.Append("                                   FROM RSUMFACMOV          \n");
                }
                strSqlString.Append("                                  WHERE FACTORY NOT IN ('RETURN')         \n");
                strSqlString.Append("                                  GROUP BY CM_KEY_1, MAT_ID, OPER, LOT_TYPE, MAT_VER, WORK_DATE, CM_KEY_3         \n");
                strSqlString.Append("                                 )         \n");
                strSqlString.Append("                           GROUP BY FACTORY, MAT_ID, OPER, LOT_TYPE, MAT_VER, WORK_DATE, CM_KEY_3         \n");
                strSqlString.Append("                          )A         \n");
                strSqlString.Append("                          , MWIPMATDEF B         \n");
                strSqlString.Append("                          , (SELECT KEY_1 AS KEY, DATA_3 AS VALUE         \n");
                strSqlString.Append("                               FROM MGCMTBLDAT         \n");
                strSqlString.Append("                              WHERE FACTORY = '" + GlobalVariable.gsAssyDefaultFactory + "'         \n");
                strSqlString.Append("                                AND TABLE_NAME = 'H_DENSITY') DENSITY            \n");
                strSqlString.Append("                     WHERE 1=1          \n");
                strSqlString.Append("                       AND A.FACTORY  = '" + GlobalVariable.gsAssyDefaultFactory + "'          \n");
                strSqlString.Append("                       AND A.FACTORY = B.FACTORY          \n");
                strSqlString.Append("                       AND A.MAT_ID = B.MAT_ID                         \n");
                strSqlString.Append("                       AND A.MAT_VER = 1          \n");
                strSqlString.Append("                       AND B.MAT_VER = 1          \n");
                strSqlString.Append("                       AND B.MAT_TYPE = 'FG'          \n");
                strSqlString.Append("                       AND A.OPER IN ('A0000', 'AZ010')         \n");
                strSqlString.Append("                       AND A.MAT_ID LIKE '%'           \n");
                /*
                if (cdvLotType.txtValue != "" && cdvLotType.txtValue != "ALL")
                {
                    strSqlString.Append("                       AND A.CM_KEY_3 " + cdvLotType.SelectedValueToQueryString + "\n");
                }
                */
                if (cdvLotType.Text != "ALL")
                {
                    strSqlString.Append("                       AND A.CM_KEY_3 LIKE '" + cdvLotType.Text + "'" + "\n");
                }


                strSqlString.Append("                       AND A.OPER NOT IN ('00001','00002')          \n");
                strSqlString.Append("                       AND A.WORK_DATE BETWEEN '" + sStartDate + "' AND '" + sEndDate + "'          \n");
                strSqlString.Append("                       AND B.MAT_ID LIKE '" + txtSearchProduct.Text + "'" + "\n");
                strSqlString.Append("                       AND B.MAT_GRP_3 <> '-'" + "\n");
                strSqlString.Append("                       AND DENSITY.KEY = B.MAT_GRP_7         \n");
                // 2013. 08. 30 WIP 기준 추가
                strSqlString.Append("                       AND (B.MAT_GRP_5 IN ('-','Merge')  OR B.MAT_GRP_5 LIKE 'Middle%')         \n");

                #region 상세조회
                //상세조회
                if (udcWIPCondition1.Text != "ALL" && udcWIPCondition1.Text != "")
                    strSqlString.AppendFormat("                       AND B.MAT_GRP_1 {0} " + "\n", udcWIPCondition1.SelectedValueToQueryString);
                if (udcWIPCondition2.Text != "ALL" && udcWIPCondition2.Text != "")
                    strSqlString.AppendFormat("                       AND B.MAT_GRP_2 {0} " + "\n", udcWIPCondition2.SelectedValueToQueryString);
                if (udcWIPCondition3.Text != "ALL" && udcWIPCondition3.Text != "")
                    strSqlString.AppendFormat("                       AND B.MAT_GRP_3 {0} " + "\n", udcWIPCondition3.SelectedValueToQueryString);
                if (udcWIPCondition4.Text != "ALL" && udcWIPCondition4.Text != "")
                    strSqlString.AppendFormat("                       AND B.MAT_GRP_4 {0} " + "\n", udcWIPCondition4.SelectedValueToQueryString);
                if (udcWIPCondition5.Text != "ALL" && udcWIPCondition5.Text != "")
                    strSqlString.AppendFormat("                       AND B.MAT_GRP_5 {0} " + "\n", udcWIPCondition5.SelectedValueToQueryString);
                if (udcWIPCondition6.Text != "ALL" && udcWIPCondition6.Text != "")
                    strSqlString.AppendFormat("                       AND B.MAT_GRP_6 {0} " + "\n", udcWIPCondition6.SelectedValueToQueryString);
                if (udcWIPCondition7.Text != "ALL" && udcWIPCondition7.Text != "")
                    strSqlString.AppendFormat("                       AND B.MAT_GRP_7 {0} " + "\n", udcWIPCondition7.SelectedValueToQueryString);
                if (udcWIPCondition8.Text != "ALL" && udcWIPCondition8.Text != "")
                    strSqlString.AppendFormat("                       AND B.MAT_GRP_8 {0} " + "\n", udcWIPCondition8.SelectedValueToQueryString);
                if (udcWIPCondition9.Text != "ALL" && udcWIPCondition9.Text != "")
                    strSqlString.AppendFormat("                       AND B.MAT_GRP_9 {0} " + "\n", udcWIPCondition9.SelectedValueToQueryString);
                #endregion

                strSqlString.Append("                     GROUP BY " + QueryCond3 + " , MAT_GRP_7, WORK_DATE, OPER)         \n");
                strSqlString.Append("    )         \n");
                strSqlString.Append("     GROUP BY DECODE(OPER,'A0000', '3_INPUT', 'AZ010', '4_DAILY',''), " + QueryCond2 + "         \n");
                strSqlString.Append("    UNION ALL         \n");
                //strSqlString.Append("    /* 재공 */         \n");
                strSqlString.Append("    SELECT '5_WIP' GUBUN, " + QueryCond2 + "    \n");
                for (int i = 0; i < iDate; i++)
                {
                    strSqlString.Append("         , SUM( DECODE(WORK_DATE, '" + dt.AddDays(i).ToString("yyyyMMdd") + "', TTL, 0)) \"" + dt.AddDays(i).ToString("yyyyMMdd") + "\"         \n");
                }
                strSqlString.Append("      FROM (         \n");
                strSqlString.Append("        SELECT " + QueryCond1 + "      , MAT.MAT_GRP_7 AS DENSITY   \n");
                strSqlString.Append("             , WORK_DATE         \n");
                if (chkEQ.Checked)
                {
                    strSqlString.Append("             , SUM(NVL((CASE WHEN MAT.MAT_CMF_11 IN (SELECT DISTINCT(MAT_CMF_11) FROM MWIPMATDEF WHERE FACTORY = '" + GlobalVariable.gsAssyDefaultFactory + "' AND MAT_GRP_5 = '1st' AND MAT_ID LIKE 'SEKS%') THEN DECODE(MAT.MAT_GRP_5, '2nd', NVL(F.V0+F.V1+F.V2+F.V3+F.V4+F.V5+F.V6+F.V7+F.V8+F.V9+F.V10+F.V11+F.V12+F.V13+F.V14+F.V15+F.V16,0), 'Merge', NVL(F.V0+F.V1+F.V2+F.V3+F.V4+F.V5+F.V6+F.V7+F.V8+F.V9+F.V10+F.V11+F.V12+F.V13+F.V14+F.V15+F.V16,0), 0)         \n");
                    strSqlString.Append("                             WHEN MAT_GRP_4 NOT IN ('-','FD','FU') AND MAT_GRP_5 <> '-' THEN CASE WHEN MAT.MAT_GRP_5 IN ('1st','Merge') OR MAT_GRP_5 LIKE 'Middle%' THEN NVL(F.V0+F.V1+F.V2+F.V3+F.V4+F.V5+F.V6+F.V7+F.V8+F.V9+F.V10+F.V11+F.V12+F.V13+F.V14+F.V15+F.V16,0) ELSE 0 END         \n");
                    strSqlString.Append("                             ELSE NVL(F.V0+F.V1+F.V2+F.V3+F.V4+F.V5+F.V6+F.V7+F.V8+F.V9+F.V10+F.V11+F.V12+F.V13+F.V14+F.V15+F.V16,0)         \n");
                    strSqlString.Append("                        END),0)*DENSITY.VALUE) AS TTL         \n");
                }
                else
                {
                    strSqlString.Append("             , SUM(NVL((CASE WHEN MAT.MAT_CMF_11 IN (SELECT DISTINCT(MAT_CMF_11) FROM MWIPMATDEF WHERE FACTORY = '" + GlobalVariable.gsAssyDefaultFactory + "' AND MAT_GRP_5 = '1st' AND MAT_ID LIKE 'SEKS%') THEN DECODE(MAT.MAT_GRP_5, '2nd', NVL(F.V0+F.V1+F.V2+F.V3+F.V4+F.V5+F.V6+F.V7+F.V8+F.V9+F.V10+F.V11+F.V12+F.V13+F.V14+F.V15+F.V16,0), 'Merge', NVL(F.V0+F.V1+F.V2+F.V3+F.V4+F.V5+F.V6+F.V7+F.V8+F.V9+F.V10+F.V11+F.V12+F.V13+F.V14+F.V15+F.V16,0), 0)         \n");
                    strSqlString.Append("                             WHEN MAT_GRP_4 NOT IN ('-','FD','FU') AND MAT_GRP_5 <> '-' THEN CASE WHEN MAT.MAT_GRP_5 IN ('1st','Merge') OR MAT_GRP_5 LIKE 'Middle%' THEN NVL(F.V0+F.V1+F.V2+F.V3+F.V4+F.V5+F.V6+F.V7+F.V8+F.V9+F.V10+F.V11+F.V12+F.V13+F.V14+F.V15+F.V16,0) ELSE 0 END         \n");
                    strSqlString.Append("                             ELSE NVL(F.V0+F.V1+F.V2+F.V3+F.V4+F.V5+F.V6+F.V7+F.V8+F.V9+F.V10+F.V11+F.V12+F.V13+F.V14+F.V15+F.V16,0)         \n");
                    strSqlString.Append("                        END),0)) AS TTL         \n");
                }
                strSqlString.Append("          FROM (         \n");
                strSqlString.Append("                SELECT *         \n");
                strSqlString.Append("                  FROM MWIPMATDEF MAT          \n");
                strSqlString.Append("                 WHERE 1 = 1          \n");
                strSqlString.Append("                   AND MAT.FACTORY = '" + GlobalVariable.gsAssyDefaultFactory + "'          \n");
                strSqlString.Append("                   AND MAT.DELETE_FLAG <> 'Y'          \n");
                // 2013. 08. 30 WIP 기준 추가
                strSqlString.Append("                   AND (MAT_GRP_5 IN ('-','Merge')  OR MAT_GRP_5 LIKE 'Middle%')          \n");
                strSqlString.Append("               ) MAT           \n");
                strSqlString.Append("             , (          \n");
                strSqlString.Append("                SELECT LOT.MAT_ID, MAT.MAT_GRP_3, WORK_DATE          \n");
                strSqlString.Append("                     , SUM(DECODE(OPER_GRP_1, 'HMK2A', DECODE(MAT.MAT_GRP_3,'COB',ROUND(QTY/TO_NUMBER(DECODE(MAT.MAT_CMF_13,' ',1,MAT.MAT_CMF_13)),0),'BGN',ROUND(QTY/TO_NUMBER(DECODE(MAT.MAT_CMF_13,' ',1,MAT.MAT_CMF_13)),0),QTY), 0)) V0          \n");
                strSqlString.Append("                     , SUM(DECODE(OPER_GRP_1, 'B/G', DECODE(MAT.MAT_GRP_3,'COB',ROUND(QTY/TO_NUMBER(DECODE(MAT.MAT_CMF_13,' ',1,MAT.MAT_CMF_13)),0),'BGN',ROUND(QTY/TO_NUMBER(DECODE(MAT.MAT_CMF_13,' ',1,MAT.MAT_CMF_13)),0),QTY), 0)) V1          \n");
                strSqlString.Append("                     , SUM(DECODE(OPER_GRP_1, 'SAW', DECODE(MAT.MAT_GRP_3,'COB',ROUND(QTY/TO_NUMBER(DECODE(MAT.MAT_CMF_13,' ',1,MAT.MAT_CMF_13)),0),'BGN',ROUND(QTY/TO_NUMBER(DECODE(MAT.MAT_CMF_13,' ',1,MAT.MAT_CMF_13)),0),QTY), 0)) V2          \n");
                strSqlString.Append("                     , SUM(DECODE(OPER_GRP_1, 'S/P', DECODE(MAT.MAT_GRP_3,'COB',ROUND(QTY/TO_NUMBER(DECODE(MAT.MAT_CMF_13,' ',1,MAT.MAT_CMF_13)),0),'BGN',ROUND(QTY/TO_NUMBER(DECODE(MAT.MAT_CMF_13,' ',1,MAT.MAT_CMF_13)),0),QTY), 0)) V3          \n");
                strSqlString.Append("                     , SUM(DECODE(OPER_GRP_1, 'D/A', DECODE(MAT.MAT_GRP_3,'COB',ROUND(QTY/TO_NUMBER(DECODE(MAT.MAT_CMF_13,' ',1,MAT.MAT_CMF_13)),0),'BGN',ROUND(QTY/TO_NUMBER(DECODE(MAT.MAT_CMF_13,' ',1,MAT.MAT_CMF_13)),0),QTY), 0)) V4          \n");
                strSqlString.Append("                     , SUM(DECODE(OPER_GRP_1, 'W/B', DECODE(MAT.MAT_GRP_3,'COB',ROUND(QTY/TO_NUMBER(DECODE(MAT.MAT_CMF_13,' ',1,MAT.MAT_CMF_13)),0),'BGN',ROUND(QTY/TO_NUMBER(DECODE(MAT.MAT_CMF_13,' ',1,MAT.MAT_CMF_13)),0),QTY), 0)) V5          \n");
                strSqlString.Append("                     , SUM(DECODE(OPER_GRP_1, 'MOLD', DECODE(MAT.MAT_GRP_3,'COB',ROUND(QTY/TO_NUMBER(DECODE(MAT.MAT_CMF_13,' ',1,MAT.MAT_CMF_13)),0),'BGN',ROUND(QTY/TO_NUMBER(DECODE(MAT.MAT_CMF_13,' ',1,MAT.MAT_CMF_13)),0),QTY), 0)) V6          \n");
                strSqlString.Append("                     , SUM(DECODE(OPER_GRP_1, 'CURE', DECODE(MAT.MAT_GRP_3,'COB',ROUND(QTY/TO_NUMBER(DECODE(MAT.MAT_CMF_13,' ',1,MAT.MAT_CMF_13)),0),'BGN',ROUND(QTY/TO_NUMBER(DECODE(MAT.MAT_CMF_13,' ',1,MAT.MAT_CMF_13)),0),QTY), 0)) V7          \n");
                strSqlString.Append("                     , SUM(DECODE(OPER_GRP_1, 'M/K', DECODE(MAT.MAT_GRP_3,'COB',ROUND(QTY/TO_NUMBER(DECODE(MAT.MAT_CMF_13,' ',1,MAT.MAT_CMF_13)),0),'BGN',ROUND(QTY/TO_NUMBER(DECODE(MAT.MAT_CMF_13,' ',1,MAT.MAT_CMF_13)),0),QTY), 0)) V8          \n");
                strSqlString.Append("                     , SUM(DECODE(OPER_GRP_1, 'TRIM', DECODE(MAT.MAT_GRP_3,'COB',ROUND(QTY/TO_NUMBER(DECODE(MAT.MAT_CMF_13,' ',1,MAT.MAT_CMF_13)),0),'BGN',ROUND(QTY/TO_NUMBER(DECODE(MAT.MAT_CMF_13,' ',1,MAT.MAT_CMF_13)),0),QTY), 0)) V9          \n");
                strSqlString.Append("                     , SUM(DECODE(OPER_GRP_1, 'TIN', DECODE(MAT.MAT_GRP_3,'COB',ROUND(QTY/TO_NUMBER(DECODE(MAT.MAT_CMF_13,' ',1,MAT.MAT_CMF_13)),0),'BGN',ROUND(QTY/TO_NUMBER(DECODE(MAT.MAT_CMF_13,' ',1,MAT.MAT_CMF_13)),0),QTY), 0)) V10         \n");
                strSqlString.Append("                     , SUM(DECODE(OPER_GRP_1, 'S/B/A', DECODE(MAT.MAT_GRP_3,'COB',ROUND(QTY/TO_NUMBER(DECODE(MAT.MAT_CMF_13,' ',1,MAT.MAT_CMF_13)),0),'BGN',ROUND(QTY/TO_NUMBER(DECODE(MAT.MAT_CMF_13,' ',1,MAT.MAT_CMF_13)),0),QTY), 0)) V11          \n");
                strSqlString.Append("                     , SUM(DECODE(OPER_GRP_1, 'SIG', DECODE(MAT.MAT_GRP_3,'COB',ROUND(QTY/TO_NUMBER(DECODE(MAT.MAT_CMF_13,' ',1,MAT.MAT_CMF_13)),0),'BGN',ROUND(QTY/TO_NUMBER(DECODE(MAT.MAT_CMF_13,' ',1,MAT.MAT_CMF_13)),0),QTY), 0)) V12          \n");
                strSqlString.Append("                     , SUM(DECODE(OPER_GRP_1, 'AVI', DECODE(MAT.MAT_GRP_3,'COB',ROUND(QTY/TO_NUMBER(DECODE(MAT.MAT_CMF_13,' ',1,MAT.MAT_CMF_13)),0),'BGN',ROUND(QTY/TO_NUMBER(DECODE(MAT.MAT_CMF_13,' ',1,MAT.MAT_CMF_13)),0),QTY), 0)) V13          \n");
                strSqlString.Append("                     , SUM(DECODE(OPER_GRP_1, 'V/I', DECODE(MAT.MAT_GRP_3,'COB',ROUND(QTY/TO_NUMBER(DECODE(MAT.MAT_CMF_13,' ',1,MAT.MAT_CMF_13)),0),'BGN',ROUND(QTY/TO_NUMBER(DECODE(MAT.MAT_CMF_13,' ',1,MAT.MAT_CMF_13)),0),QTY), 0)) V14          \n");
                strSqlString.Append("                     , SUM(DECODE(OPER_GRP_1, 'HMK3A', DECODE(MAT.MAT_GRP_3,'COB',ROUND(QTY/TO_NUMBER(DECODE(MAT.MAT_CMF_13,' ',1,MAT.MAT_CMF_13)),0),'BGN',ROUND(QTY/TO_NUMBER(DECODE(MAT.MAT_CMF_13,' ',1,MAT.MAT_CMF_13)),0),QTY), 0)) V15          \n");
                strSqlString.Append("                     , SUM(DECODE(OPER_GRP_1, 'GATE', DECODE(MAT.MAT_GRP_3,'COB',ROUND(QTY/TO_NUMBER(DECODE(MAT.MAT_CMF_13,' ',1,MAT.MAT_CMF_13)),0),'BGN',ROUND(QTY/TO_NUMBER(DECODE(MAT.MAT_CMF_13,' ',1,MAT.MAT_CMF_13)),0),QTY), 0)) V16          \n");
                strSqlString.Append("                  FROM (           \n");
                strSqlString.Append("                        SELECT FACTORY, MAT_ID, OPER_GRP_1 , WORK_DATE         \n");
                strSqlString.Append("                             , SUM(CASE WHEN OPER <= 'A0395' THEN QTY_1 / NVL(COMP_CNT,1)          \n");
                strSqlString.Append("                                        ELSE QTY_1          \n");
                strSqlString.Append("                                   END) QTY          \n");
                strSqlString.Append("                          FROM (          \n");
                strSqlString.Append("                                SELECT A.FACTORY, A.MAT_ID, B.OPER_GRP_1, B.OPER, SUBSTR(A.CUTOFF_DT,1,8) WORK_DATE, A.QTY_1          \n");
                strSqlString.Append("                                     , (SELECT DATA_1 FROM MGCMTBLDAT WHERE FACTORY = '" + GlobalVariable.gsAssyDefaultFactory + "' AND TABLE_NAME IN ('H_SEC_AUTO_LOSS','H_HX_AUTO_LOSS') AND KEY_1 = A.MAT_ID) AS COMP_CNT           \n");
                if (isToday && existTodayWipData == "FALSE")
                {
                    //if (cboTimeBase.Text == "06시")
                    if (cboTimeBase.SelectedIndex == 0)
                    {
                        strSqlString.Append("                                  FROM (SELECT FACTORY, MAT_ID, OPER, LOT_TYPE, LOT_DEL_FLAG, LOT_CMF_5, CUTOFF_DT, QTY_1  FROM RWIPLOTSTS_BOH UNION ALL SELECT FACTORY, MAT_ID, OPER, LOT_TYPE, LOT_DEL_FLAG, LOT_CMF_5, '" + sToday + "'||'06', QTY_1 FROM RWIPLOTSTS) A          \n");
                    }
                    else
                    {
                        strSqlString.Append("                                  FROM (SELECT FACTORY, MAT_ID, OPER, LOT_TYPE, LOT_DEL_FLAG, LOT_CMF_5, CUTOFF_DT, QTY_1  FROM RWIPLOTSTS_BOH UNION ALL SELECT FACTORY, MAT_ID, OPER, LOT_TYPE, LOT_DEL_FLAG, LOT_CMF_5, '" + sToday + "'||'22', QTY_1 FROM RWIPLOTSTS) A          \n");
                    }

                }
                else
                {
                    strSqlString.Append("                                  FROM (SELECT FACTORY, MAT_ID, OPER, LOT_TYPE, LOT_DEL_FLAG, LOT_CMF_5, CUTOFF_DT, QTY_1  FROM RWIPLOTSTS_BOH) A          \n");
                }
                strSqlString.Append("                                     , MWIPOPRDEF B          \n");
                strSqlString.Append("                                 WHERE 1 = 1         \n");
                strSqlString.Append("                                   AND A.CUTOFF_DT IN(");
                for (int i = 0; i < iDate; i++)
                {
                    //if (cboTimeBase.Text == "06시")
                    if (cboTimeBase.SelectedIndex == 0)
                    {
                        strSqlString.Append(" " + (i == 0 ? "" : ",") + " '" + dt.AddDays(i).ToString("yyyyMMdd") + "06'");
                    }
                    else
                    {
                        strSqlString.Append(" " + (i == 0 ? "" : ",") + " '" + dt.AddDays(i).ToString("yyyyMMdd") + "22'");
                    }

                }
                strSqlString.Append("                                                     )         \n");
                strSqlString.Append("                                   AND A.FACTORY = B.FACTORY(+)           \n");
                strSqlString.Append("                                   AND A.OPER = B.OPER(+)           \n");
                strSqlString.Append("                                   AND A.FACTORY = '" + GlobalVariable.gsAssyDefaultFactory + "'           \n");
                strSqlString.Append("                                   AND A.LOT_TYPE = 'W'          \n");
                strSqlString.Append("                                   AND A.LOT_DEL_FLAG = ' '          \n");
                /*
                if (cdvLotType.txtValue != "" && cdvLotType.txtValue != "ALL")
                {
                    strSqlString.Append("                                   AND A.LOT_CMF_5 " + cdvLotType.SelectedValueToQueryString + "\n");
                }
                */
                if (cdvLotType.Text != "ALL")
                {
                    strSqlString.Append("                                   AND A.LOT_CMF_5 LIKE '" + cdvLotType.Text + "'" + "\n");
                }

                // 2014-10-16-임종우 : BG 공정 포함 유무 선택 가능하도록..
                if (ckbBG.Checked == true)
                {
                    strSqlString.Append("                                   AND A.OPER NOT IN ('A0000', 'A0020') " + "\n");
                }
                else
                {
                    strSqlString.Append("                                   AND A.OPER NOT IN ('A0000', 'A0020', 'A0040') " + "\n");
                }

                strSqlString.Append("                               )          \n");
                strSqlString.Append("                         GROUP BY FACTORY, MAT_ID, OPER_GRP_1, WORK_DATE          \n");
                strSqlString.Append("                       ) LOT          \n");
                strSqlString.Append("                     , MWIPMATDEF MAT          \n");
                strSqlString.Append("                 WHERE 1 = 1          \n");
                strSqlString.Append("                   AND LOT.FACTORY = MAT.FACTORY          \n");
                strSqlString.Append("                   AND LOT.MAT_ID = MAT.MAT_ID          \n");
                strSqlString.Append("                   AND MAT.DELETE_FLAG <> 'Y'          \n");
                strSqlString.Append("                   AND MAT.MAT_GRP_2 <> '-'          \n");
                strSqlString.Append("                 GROUP BY LOT.MAT_ID ,MAT.MAT_GRP_3, WORK_DATE         \n");
                strSqlString.Append("               ) F         \n");
                strSqlString.Append("             , (SELECT KEY_1 AS KEY, DATA_3 AS VALUE         \n");
                strSqlString.Append("                  FROM MGCMTBLDAT         \n");
                strSqlString.Append("                 WHERE FACTORY = '" + GlobalVariable.gsAssyDefaultFactory + "'         \n");
                strSqlString.Append("                   AND TABLE_NAME = 'H_DENSITY') DENSITY          \n");
                strSqlString.Append("         WHERE 1 = 1          \n");
                strSqlString.Append("           AND MAT.MAT_ID = F.MAT_ID(+)         \n");
                strSqlString.Append("           AND MAT.MAT_ID LIKE '" + txtSearchProduct.Text + "'" + "\n");
                strSqlString.Append("           AND MAT.MAT_GRP_3 <> '-'" + "\n");
                strSqlString.Append("           AND MAT.MAT_GRP_7 = DENSITY.KEY         \n");
                #region 상세조회
                //상세조회
                if (udcWIPCondition1.Text != "ALL" && udcWIPCondition1.Text != "")
                    strSqlString.AppendFormat("           AND MAT.MAT_GRP_1 {0} " + "\n", udcWIPCondition1.SelectedValueToQueryString);
                if (udcWIPCondition2.Text != "ALL" && udcWIPCondition2.Text != "")
                    strSqlString.AppendFormat("           AND MAT.MAT_GRP_2 {0} " + "\n", udcWIPCondition2.SelectedValueToQueryString);
                if (udcWIPCondition3.Text != "ALL" && udcWIPCondition3.Text != "")
                    strSqlString.AppendFormat("           AND MAT.MAT_GRP_3 {0} " + "\n", udcWIPCondition3.SelectedValueToQueryString);
                if (udcWIPCondition4.Text != "ALL" && udcWIPCondition4.Text != "")
                    strSqlString.AppendFormat("           AND MAT.MAT_GRP_4 {0} " + "\n", udcWIPCondition4.SelectedValueToQueryString);
                if (udcWIPCondition5.Text != "ALL" && udcWIPCondition5.Text != "")
                    strSqlString.AppendFormat("           AND MAT.MAT_GRP_5 {0} " + "\n", udcWIPCondition5.SelectedValueToQueryString);
                if (udcWIPCondition6.Text != "ALL" && udcWIPCondition6.Text != "")
                    strSqlString.AppendFormat("           AND MAT.MAT_GRP_6 {0} " + "\n", udcWIPCondition6.SelectedValueToQueryString);
                if (udcWIPCondition7.Text != "ALL" && udcWIPCondition7.Text != "")
                    strSqlString.AppendFormat("           AND MAT.MAT_GRP_7 {0} " + "\n", udcWIPCondition7.SelectedValueToQueryString);
                if (udcWIPCondition8.Text != "ALL" && udcWIPCondition8.Text != "")
                    strSqlString.AppendFormat("           AND MAT.MAT_GRP_8 {0} " + "\n", udcWIPCondition8.SelectedValueToQueryString);
                if (udcWIPCondition9.Text != "ALL" && udcWIPCondition9.Text != "")
                    strSqlString.AppendFormat("           AND MAT.MAT_GRP_9 {0} " + "\n", udcWIPCondition9.SelectedValueToQueryString);
                #endregion

                strSqlString.Append("         GROUP BY " + QueryCond1 + ",  MAT.MAT_GRP_7, WORK_DATE         \n");
                strSqlString.Append("        HAVING (         \n");
                strSqlString.Append("                NVL(SUM(F.V0+F.V1+F.V2+F.V3+F.V4+F.V5+F.V6+F.V7+F.V8+F.V9+F.V10+F.V11+F.V12+F.V13+F.V14+F.V15+F.V16), 0)           \n");
                strSqlString.Append("               ) <> 0                 \n");
                strSqlString.Append("         )                    \n");
                strSqlString.Append("        GROUP BY " + QueryCond2 + "         \n");
                strSqlString.Append("    ) , (SELECT ROWNUM TOTAL_SEQ FROM DUAL CONNECT BY LEVEL <= 5) SEQ             \n");
                strSqlString.Append(" GROUP BY TOTAL_SEQ, DECODE(TOTAL_SEQ, 1, '일간', 2, '월간', 3, 'INPUT', 4, 'OUTPUT', 5, 'WIP') , " + QueryCond2 + "                  \n");
                strSqlString.Append(")         \n");

                #endregion 공정그룹별이 아닌 경우 view 테이블
            }
            
            if (chkTime.Checked == true) // 시간 단위
            {
                strSqlString.Append("SELECT " + QueryCond4 + ", NVL(TO_CHAR(TARGET_TAT*24),' ') TAT, GUBUN  \n");
                for (int i = 0; i < iDate; i++)
                {
                    strSqlString.Append("     , NULLIF(TRIM(DECODE(RNUM, 1, TO_CHAR(\"" + dt.AddDays(i).ToString("yyyyMMdd") + "\"*24,'9999990D99'), 2, TO_CHAR(\"" + dt.AddDays(i).ToString("yyyyMMdd") + "\"*24,'9999990D99'), TO_CHAR(\"" + dt.AddDays(i).ToString("yyyyMMdd") + "\",'999,999,999,999'))),'0')  \"" + dt.AddDays(i).ToString("yyyyMMdd") + "\"\n");
                }
            }
            else // 날짜 단위
            {
                strSqlString.Append("SELECT " + QueryCond4 + ", NVL(TO_CHAR(TARGET_TAT),' ') TAT, GUBUN  \n");
                for (int i = 0; i < iDate; i++)
                {
                    strSqlString.Append("     , NULLIF(TRIM(DECODE(RNUM, 1, TO_CHAR(\"" + dt.AddDays(i).ToString("yyyyMMdd") + "\",'9999990D99'), 2, TO_CHAR(\"" + dt.AddDays(i).ToString("yyyyMMdd") + "\",'9999990D99'), TO_CHAR(\"" + dt.AddDays(i).ToString("yyyyMMdd") + "\",'999,999,999,999'))),'0')  \"" + dt.AddDays(i).ToString("yyyyMMdd") + "\"\n");
                }
            }

            if (IsOperGroup)
            {
                strSqlString.Append("  FROM (SELECT " + QueryCond2 + strOperGroupCond2 + "         \n");
                strSqlString.Append("             , (SELECT /*CUSTOMER, PKGAGE, MAT_GRP_4, MAT_GRP_3, OPER_GRP,*/ MIN(TAT)           " + "\n");
                strSqlString.Append("                  FROM (SELECT DISTINCT CUSTOMER, PKGAGE, MAT_GRP_4, MAT_GRP_3, OPER_GRP, TAT          " + "\n");
                strSqlString.Append("                          FROM (SELECT DISTINCT MAT_GRP_1, MAT_GRP_2, MAT_GRP_3, MAT_GRP_4, MAT_GRP_5, MAT_GRP_6, MAT_GRP_7, MAT_GRP_8, MAT_CMF_10, MAT_ID          " + "\n");
                strSqlString.Append("                                  FROM MWIPMATDEF          " + "\n");
                strSqlString.Append("                                 WHERE FACTORY = '" + GlobalVariable.gsAssyDefaultFactory + "'          " + "\n");
                strSqlString.Append("                                   AND DELETE_FLAG = ' ') MAT,          " + "\n");
                strSqlString.Append("                                (SELECT KEY_2 AS CUSTOMER, KEY_3 AS PKGAGE, DECODE(KEY_4, 'SAW', 'SAW', 'D/A', 'DA', 'W/B', 'WB', 'WB', 'WB', 'MOLD', 'MOLD', 'FINISH', 'FINISH') AS OPER_GRP, SUM(DATA_2) AS TAT          " + "\n");
                strSqlString.Append("                                  FROM MGCMTBLDAT          " + "\n");
                strSqlString.Append("                                 WHERE TABLE_NAME = 'H_RPT_TAT_HANA'          " + "\n");
                strSqlString.Append("                                   AND KEY_1 <= TO_CHAR(SYSDATE, 'YYYYMMDD')          " + "\n");
                strSqlString.Append("                                   AND DATA_1 >= TO_CHAR(SYSDATE, 'YYYYMMDD') " + "\n");
                strSqlString.Append("                                   AND KEY_4 NOT IN ('GATE', 'HMK2A', 'HMK3A') " + "\n");
                strSqlString.Append("                                 GROUP BY KEY_2, KEY_3, DECODE(KEY_4, 'SAW', 'SAW', 'D/A', 'DA', 'W/B', 'WB', 'WB', 'WB', 'MOLD', 'MOLD', 'FINISH', 'FINISH')          " + "\n");
                strSqlString.Append("                               ) TAT               " + "\n");
                strSqlString.Append("                         WHERE 1 = 1          " + "\n");
                strSqlString.Append("                           AND  MAT.MAT_GRP_1 = TAT.CUSTOMER          " + "\n");
                strSqlString.Append("                           AND ((MAT.MAT_GRP_4 IN ('FU', 'FD') AND MAT.MAT_GRP_3 = TAT.PKGAGE)           " + "\n");
                strSqlString.Append("                                 OR (REGEXP_LIKE(MAT.MAT_GRP_4, 'SD*|SS*') AND TAT.PKGAGE = SUBSTR(MAT.MAT_GRP_4, LENGTH(MAT.MAT_GRP_4) , 1)||MAT.MAT_GRP_3))          " + "\n");
                strSqlString.Append("                        )          " + "\n");
                strSqlString.Append("                  WHERE CUSTOMER = V.MAT_GRP_1           " + "\n");
                strSqlString.Append("                    AND PKGAGE = V.MAT_GRP_3 " + "\n");
                strSqlString.Append("                    AND OPER_GRP = V.OPER_GRP) AS TARGET_TAT   " + "\n");
                strSqlString.Append("             , GUBUN " + "\n");
                for (int i = 0; i < iDate; i++)
                {
                    // 월간 tat를 계산할 때 LEAD사용으로 할 경우 쿼리가 길어지고 속도 문제로 UI상에서 계산하는 것으로 변경
                    // 2013. 08. 30 KCPS단위 추가
                    if (chkKcps.Checked)
                    {
                        strSqlString.Append("         , CASE WHEN TOTAL_SEQ = '1' THEN ROUND(LEAD(SUM(\"" + dt.AddDays(i).ToString("yyyyMMdd") + "\"), 4) OVER(ORDER BY " + QueryCond2 + strOperGroupCond2 + ", TOTAL_SEQ) / NULLIF(LEAD(SUM(\"" + dt.AddDays(i).ToString("yyyyMMdd") + "\"), 3) OVER(ORDER BY " + QueryCond2 + strOperGroupCond2 + ", TOTAL_SEQ),0),2) ELSE ROUND(SUM(\"" + dt.AddDays(i).ToString("yyyyMMdd") + "\")/1000,0) END \"" + dt.AddDays(i).ToString("yyyyMMdd") + "\"         \n");
                    }
                    else
                    {
                        strSqlString.Append("         , CASE WHEN TOTAL_SEQ = '1' THEN ROUND(LEAD(SUM(\"" + dt.AddDays(i).ToString("yyyyMMdd") + "\"), 4) OVER(ORDER BY " + QueryCond2 + strOperGroupCond2 + ", TOTAL_SEQ) / NULLIF(LEAD(SUM(\"" + dt.AddDays(i).ToString("yyyyMMdd") + "\"), 3) OVER(ORDER BY " + QueryCond2 + strOperGroupCond2 + ", TOTAL_SEQ),0),2) ELSE ROUND(SUM(\"" + dt.AddDays(i).ToString("yyyyMMdd") + "\"),0) END \"" + dt.AddDays(i).ToString("yyyyMMdd") + "\"         \n");
                    }
                }
                strSqlString.Append("             , ROW_NUMBER() OVER(PARTITION BY " + QueryCond2 + strOperGroupCond2+" ORDER BY " + QueryCond2 + strOperGroupCond2+", DECODE(GUBUN, '일간', 1, '월간', 2, 'INPUT', 3, 'OUTPUT', 4, 'WIP', 5, 'OUTPUT_TOTAL', 6, 'WIP_TOTAL', 7, 0)) AS rnum \n");
                strSqlString.Append("  FROM (         \n");
                strSqlString.Append("    SELECT TOTAL_SEQ, " + QueryCond2 + strOperGroupCond2 + "     \n");
                strSqlString.Append("         ,  GUBUN         \n");
            }
            else
            {
                strSqlString.Append("  FROM (SELECT " + QueryCond2 + "         \n");
                strSqlString.Append("             , (SELECT /*CUSTOMER, PKGAGE, MAT_GRP_4, MAT_GRP_3,*/ MIN(TAT)          \n");
                strSqlString.Append("                  FROM (SELECT DISTINCT CUSTOMER, PKGAGE, MAT_GRP_4, MAT_GRP_3, TAT         \n");
                strSqlString.Append("                          FROM (SELECT DISTINCT MAT_GRP_1, MAT_GRP_2, MAT_GRP_3, MAT_GRP_4, MAT_GRP_5, MAT_GRP_6, MAT_GRP_7, MAT_GRP_8, MAT_CMF_10, MAT_ID         \n");
                strSqlString.Append("                                  FROM MWIPMATDEF         \n");
                strSqlString.Append("                                 WHERE FACTORY = '" + GlobalVariable.gsAssyDefaultFactory + "'         \n");
                strSqlString.Append("                                   AND DELETE_FLAG = ' ') MAT,         \n");
                strSqlString.Append("                               (SELECT KEY_2 AS CUSTOMER, KEY_3 AS PKGAGE, SUM(DATA_2) AS TAT         \n");
                strSqlString.Append("                                  FROM MGCMTBLDAT         \n");
                strSqlString.Append("                                 WHERE TABLE_NAME = 'H_RPT_TAT_HANA'         \n");
                strSqlString.Append("                                   AND KEY_1 <= TO_CHAR(SYSDATE, 'YYYYMMDD')         \n");
                strSqlString.Append("                                   AND DATA_1 >= TO_CHAR(SYSDATE, 'YYYYMMDD')         \n");
                strSqlString.Append("                                 GROUP BY KEY_2, KEY_3         \n");
                strSqlString.Append("                               ) TAT               \n");
                strSqlString.Append("                         WHERE 1 = 1         \n");
                strSqlString.Append("                           AND  MAT.MAT_GRP_1 = TAT.CUSTOMER         \n");
                strSqlString.Append("                           AND ((MAT.MAT_GRP_4 IN ('FU', 'FD') AND MAT.MAT_GRP_3 = TAT.PKGAGE)          \n");
                strSqlString.Append("                                 OR (REGEXP_LIKE(MAT.MAT_GRP_4, 'SD*|SS*') AND TAT.PKGAGE = SUBSTR(MAT.MAT_GRP_4, LENGTH(MAT.MAT_GRP_4) , 1)||MAT.MAT_GRP_3))         \n");
                strSqlString.Append("                        )         \n");
                strSqlString.Append("                  WHERE CUSTOMER = V.MAT_GRP_1         \n");
                strSqlString.Append("                    AND PKGAGE = V.MAT_GRP_3) AS TARGET_TAT         \n");
                strSqlString.Append("             , GUBUN \n");
                for (int i = 0; i < iDate; i++)
                {
                    // 월간 tat를 계산할 때 LEAD사용으로 할 경우 쿼리가 길어지고 속도 문제로 UI상에서 계산하는 것으로 변경
                    // 2013. 08. 30 KCPS단위 추가
                    if (chkKcps.Checked)
                    {
                        strSqlString.Append("         , CASE WHEN TOTAL_SEQ = '1' THEN ROUND(LEAD(SUM(\"" + dt.AddDays(i).ToString("yyyyMMdd") + "\"), 4) OVER(ORDER BY " + QueryCond2 + ", TOTAL_SEQ) / NULLIF(LEAD(SUM(\"" + dt.AddDays(i).ToString("yyyyMMdd") + "\"), 3) OVER(ORDER BY " + QueryCond2 + ", TOTAL_SEQ),0),2) ELSE ROUND(SUM(\"" + dt.AddDays(i).ToString("yyyyMMdd") + "\")/1000,0) END \"" + dt.AddDays(i).ToString("yyyyMMdd") + "\"         \n");
                    }
                    else
                    {
                        strSqlString.Append("         , CASE WHEN TOTAL_SEQ = '1' THEN ROUND(LEAD(SUM(\"" + dt.AddDays(i).ToString("yyyyMMdd") + "\"), 4) OVER(ORDER BY " + QueryCond2 + ", TOTAL_SEQ) / NULLIF(LEAD(SUM(\"" + dt.AddDays(i).ToString("yyyyMMdd") + "\"), 3) OVER(ORDER BY " + QueryCond2 + ", TOTAL_SEQ),0),2) ELSE ROUND(SUM(\"" + dt.AddDays(i).ToString("yyyyMMdd") + "\"),0) END \"" + dt.AddDays(i).ToString("yyyyMMdd") + "\"         \n");
                    }
                }
                strSqlString.Append("             , ROW_NUMBER() OVER(PARTITION BY " + QueryCond2 + " ORDER BY " + QueryCond2 + ", DECODE(GUBUN, '일간', 1, '월간', 2, 'INPUT', 3, 'OUTPUT', 4, 'WIP', 5, 'OUTPUT_TOTAL', 6, 'WIP_TOTAL', 7, 0)) AS rnum \n");
                strSqlString.Append("  FROM (         \n");
                strSqlString.Append("    SELECT TOTAL_SEQ, " + QueryCond2 + "     \n");
                strSqlString.Append("         ,  GUBUN         \n");
            }
            
            for (int i = 0; i < iDate; i++)
            {
                strSqlString.Append("         , \"" + dt.AddDays(i).ToString("yyyyMMdd") + "\"         \n");
            }
            
            strSqlString.Append("      FROM TEMP_V         \n");
            strSqlString.Append("       ) V         \n");
            strSqlString.Append(" WHERE 1 = 1         \n");
            strSqlString.Append(" GROUP BY GROUPING SETS(  " + groupingSetValue + "  )            \n");
            strSqlString.Append(" ) \n");
            strSqlString.Append(" WHERE RNUM <= 5  \n");


            strSqlString.Append(" ORDER BY DECODE(MAT_GRP_1, NULL, 1, 'SE', 2, 'HX', 3, 'IM', 4, 'FC', 5, 6), " + QueryCond2.Replace("OPER_GRP", "DECODE(OPER_GRP, 'SAW', 1, 'DA', 2, 'WB', 3, 'MOLD', 4, 'FINISH', 5, 6)") + "     , DECODE(GUBUN, '일간', 1, '월간', 2, 'INPUT', 3, 'OUTPUT', 4, 'WIP', 5, 6)     \n");

            if (GlobalVariable.gsUserGroup == "ADMIN_GROUP" || GlobalVariable.gsUserGroup == "HANA_ADMIN_GROUP")
            {
                System.Windows.Forms.Clipboard.SetText(strSqlString.ToString());
            }

            return strSqlString.ToString();
        }

        #endregion

        #endregion

        #region Event Handler

        /// <summary>
        /// Excel Export
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnExcelExport_Click(object sender, EventArgs e)
        {
            // Excel 바로 보이기
            //ExcelHelper.Instance.subMakeExcel(spdData, this.lblTitle.Text, " ^ ", " ^ ", true);
            spdData.ExportExcel();
        }

        private void rbtBrief_CheckedChanged(object sender, EventArgs e)
        {
            //cdvStep.Visible = !(rbtBrief.Checked);
        }       

        #endregion
             
    }
}

