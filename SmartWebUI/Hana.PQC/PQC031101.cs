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

namespace Hana.PQC
{
    /// <summary>
    /// 클  래  스: PQC031101<br/>
    /// 클래스요약: 표준정착 관리<br/>
    /// 작  성  자: 하나마이크론 임종우<br/>
    /// 최초작성일: 2010-08-19<br/>
    /// 상세  설명: 표준점검데이터를 조회한다.<br/>
    
    /// </summary>
    public partial class PQC031101 : Miracom.SmartWeb.UI.Controls.udcCUSReport002
    {

        #region " PQC031101 : Program Initial "

        public PQC031101()
        {
            InitializeComponent();
            SortInit();

            cdvFromToDate.AutoBinding();
            cdvFromToDate.DaySelector.MaxDropDownItems = 2;
            cdvFromToDate.DaySelector.SelectedValue = "MONTH";
            
            GridColumnInit();
            udcChartFX1.RPT_1_ChartInit();
            udcChartFX2.RPT_1_ChartInit();
            udcChartFX3.RPT_1_ChartInit();

            this.SetFactory(GlobalVariable.gsAssyDefaultFactory);
            cdvFactory.Text = GlobalVariable.gsAssyDefaultFactory;
        }

        #endregion


        #region " Function Definition "

        /// <summary 1. 유효성 검사>
        /// <remarks></remarks>
        /// </summary>
        /// <returns></returns>
        private Boolean CheckField()
        {
            return true;
        }

        #endregion


        #region " GridColumnInit : Sheet Title 설정 "

        /// <summary>
        /// 2. 헤더 생성
        /// </summary>
        private void GridColumnInit()
        {
            int j = 1;

            try
            {
                spdData.RPT_ColumnInit();
                spdData.ActiveSheet.ColumnHeader.Rows.Count = 1;
                string[] strDate = cdvFromToDate.getSelectDate();

                spdData.RPT_AddBasicColumn("Responsible Department", 0, 0, Visibles.True, Frozen.True, Align.Center, Merge.False, Formatter.String, 60);

                if (rbtStandard.Checked == true)
                {                   
                    for (int i = 0; i <= cdvFromToDate.SubtractBetweenFromToDate; i++)
                    {
                        //if (cboType.Text == "ALL")
                        if (cboType.SelectedIndex == 0)
                        {
                            spdData.RPT_AddBasicColumn(strDate[i], 0, j, Visibles.True, Frozen.False, Align.Center, Merge.False, Formatter.String, 70);
                            spdData.RPT_AddBasicColumn("Standard perfect rate", 1, j, Visibles.True, Frozen.False, Align.Center, Merge.False, Formatter.Double2, 70);
                            spdData.RPT_AddBasicColumn("Standard compliance rate", 1, j + 1, Visibles.True, Frozen.False, Align.Center, Merge.False, Formatter.Double2, 70);
                            spdData.RPT_AddBasicColumn("Standard fixation rate", 1, j + 2, Visibles.True, Frozen.False, Align.Center, Merge.False, Formatter.Double2, 70);
                            spdData.RPT_MerageHeaderColumnSpan(0, j, 3);

                            j = j + 3;
                        }
                        else
                        {
                            spdData.RPT_AddBasicColumn(strDate[i], 0, j, Visibles.True, Frozen.False, Align.Center, Merge.False, Formatter.String, 70);
                            spdData.RPT_AddBasicColumn(cboType.Text, 1, j, Visibles.True, Frozen.False, Align.Center, Merge.False, Formatter.Double2, 70);
                            
                            j = j + 1;
                        }
                    }
                }
                else if (rbt1.Checked == true)
                {         
                    for (int i = 0; i <= cdvFromToDate.SubtractBetweenFromToDate; i++)
                    {
                        spdData.RPT_AddBasicColumn(strDate[i], 0, j, Visibles.True, Frozen.False, Align.Center, Merge.False, Formatter.String, 70);
                        spdData.RPT_AddBasicColumn("Standard action rate", 1, j, Visibles.True, Frozen.False, Align.Center, Merge.False, Formatter.Double2, 70);
                                                
                        j = j + 1;
                    }
                }
                else if (rbt2.Checked == true)
                {
                    for (int i = 0; i <= cdvFromToDate.SubtractBetweenFromToDate; i++)
                    {
                        spdData.RPT_AddBasicColumn(strDate[i], 0, j, Visibles.True, Frozen.False, Align.Center, Merge.False, Formatter.String, 70);
                        spdData.RPT_AddBasicColumn("Standard Recurrence Rate", 1, j, Visibles.True, Frozen.False, Align.Center, Merge.False, Formatter.Double2, 70);
                        
                        j = j + 1;
                    }
                }

                spdData.RPT_MerageHeaderRowSpan(0, 0, 2);

                // Group항목이 있을경우 반드시 선언해줄것.
                spdData.RPT_ColumnConfigFromTable(btnSort);
            }
            catch (Exception ex)
            {
                CmnFunction.ShowMsgBox(ex.Message);
                LoadingPopUp.LoadingPopUpHidden();
            }
        }

        #endregion


        #region " SortInit : Group By 설정 "

        /// <summary>
        /// 3. Group By 정의
        /// </summary>
        private void SortInit()
        {
            //((udcTableForm)(this.btnSort.BindingForm)).Clear();
            //((udcTableForm)(this.btnSort.BindingForm)).AddRow("Classification", "VENDOR", "VENDOR", true);
            //((udcTableForm)(this.btnSort.BindingForm)).AddRow("Operation Name", "MAT_TYPE", "MAT_TYPE", true);
            
        }

        #endregion

        #region " MakeSqlString : Sql Query문 "

        /// <summary>
        /// 4. 쿼리 생성
        /// </summary>
        /// <returns></returns>
        private string MakeSqlString()
        {
            StringBuilder strSqlString = new StringBuilder();

            string QueryCond1 = null;
            string[] strDate = cdvFromToDate.getSelectDate();

            //추가
            udcTableForm tableForm = (udcTableForm)btnSort.BindingForm;

            QueryCond1 = tableForm.SelectedValueToQueryContainNull;

            // 쿼리

            if (rbtStandard.Checked == true) //표준 정착 현황
            {
                strSqlString.Append("SELECT (SELECT DATA_1 FROM MGCMTBLDAT WHERE FACTORY = '" + GlobalVariable.gsAssyDefaultFactory + "' AND TABLE_NAME = 'H_DEPARTMENT' AND KEY_1 = A.RESP_DEPARTMENT) AS DEP" + "\n");

                for (int i = 0; i <= cdvFromToDate.SubtractBetweenFromToDate; i++)
                {
                    //if (cboType.Text == "ALL")
                    if (cboType.SelectedIndex == 0)
                    {
                        strSqlString.Append("     , A" + i + "\n");
                        strSqlString.Append("     , B" + i + "\n");
                        strSqlString.Append("     , ROUND(A" + i + " * B" + i + " / 100, 2) AS C" + i + "\n");
                    }
                    //else if (cboType.Text == "Standard fixation rate")
                    else if (cboType.SelectedIndex == 3)
                    {
                        strSqlString.Append("     , ROUND(A" + i + " * B" + i + " / 100, 2) AS C" + i + "\n");
                    }
                    //else if (cboType.Text == "Standard perfect rate")
                    else if (cboType.SelectedIndex == 1)
                    {
                        strSqlString.Append("     , A" + i + "\n");
                    }
                    else
                    {
                        strSqlString.Append("     , B" + i + "\n");
                    }
                }

                strSqlString.Append("  FROM (" + "\n");
                strSqlString.Append("        SELECT RESP_DEPARTMENT" + "\n");

                for (int i = 0; i <= cdvFromToDate.SubtractBetweenFromToDate; i++)
                {
                    strSqlString.Append("             , SUM(DECODE(CHECK_DAY, '" + strDate[i] + "', DECODE(TYPE, 'WANJUN', PER, 0), 0)) AS A" + i + "\n");
                    strSqlString.Append("             , SUM(DECODE(CHECK_DAY, '" + strDate[i] + "', DECODE(TYPE, 'JUNSU', PER, 0), 0)) AS B" + i + "\n");
                    //strSqlString.Append("     , ROUND(SUM(DECODE(CHECK_DAY, '" + strDate[i] + "', DECODE(TYPE, 'JUNSU', PER, 0), 0)) * SUM(DECODE(CHECK_DAY, '" + strDate[i] + "', DECODE(TYPE, 'WANJUN', PER, 0), 0)) / 100, 2) AS C" + i + "\n");
                }

                strSqlString.Append("          FROM (" + "\n");
                strSqlString.Append("                SELECT RESP_DEPARTMENT, 'JUNSU' AS TYPE, CHECK_DAY, SUM(TOTAL_QTY) AS TOTAL_QTY, SUM(ERROR_QTY) AS ERROR_QTY" + "\n");
                strSqlString.Append("                     , ROUND((SUM(TOTAL_QTY) - SUM(ERROR_QTY)) / SUM(TOTAL_QTY) * 100, 2) AS PER" + "\n");
                strSqlString.Append("                  FROM (" + "\n");
                strSqlString.Append("                        SELECT DOC_NAME, RESP_DEPARTMENT" + "\n");

                if (cdvFromToDate.DaySelector.SelectedValue.ToString() == "MONTH")
                {
                    strSqlString.Append("                             , GET_WORK_DATE(SUBSTR(CHECK_DAY,1,8),'M') AS CHECK_DAY" + "\n");
                }
                else
                {
                    strSqlString.Append("                             , GET_WORK_DATE(SUBSTR(CHECK_DAY,1,8),'W') AS CHECK_DAY" + "\n");
                }

                strSqlString.Append("                             , AVG(RESV_CMF_1) AS TOTAL_QTY" + "\n");
                strSqlString.Append("                             , SUM(DECODE(RESV_CMF_2, '표준 미준수', 1, 0)) AS ERROR_QTY" + "\n");
                strSqlString.Append("                          FROM (" + "\n");
                strSqlString.Append("                                SELECT ROW_NUMBER() OVER(PARTITION BY DOC_NO ORDER BY VERSION DESC) AS NUM" + "\n");
                strSqlString.Append("                                     , A.*" + "\n");
                strSqlString.Append("                                  FROM CQCMSCRDAT@RPTTOMES A" + "\n");
                strSqlString.Append("                                 WHERE 1=1" + "\n");
                strSqlString.Append("                                   AND DOC_NO LIKE 'HAS%'" + "\n");
                strSqlString.Append("                                   AND HIST_DELETE_FLAG = ' '" + "\n");
                strSqlString.Append("                                   AND RESV_CMF_2 <> '표준 불완전'" + "\n");
                strSqlString.Append("                                   AND RESV_CMF_1 <> ' '" + "\n");

                if (cdvFromToDate.DaySelector.SelectedValue.ToString() == "MONTH")
                {
                    strSqlString.Append("                                   AND GET_WORK_DATE(SUBSTR(CHECK_DAY,1,8),'M') BETWEEN '" + cdvFromToDate.FromYearMonth.Value.ToString("yyyyMM") + "' AND '" + cdvFromToDate.ToYearMonth.Value.ToString("yyyyMM") + "'" + "\n");
                }
                else
                {
                    strSqlString.Append("                                   AND GET_WORK_DATE(SUBSTR(CHECK_DAY,1,8),'W') BETWEEN '" + cdvFromToDate.HmFromWeek + "' AND '" + cdvFromToDate.HmToWeek + "'" + "\n");
                }

                strSqlString.Append("                               )" + "\n");
                strSqlString.Append("                         WHERE 1=1" + "\n");
                strSqlString.Append("                           AND NUM = 1" + "\n");
                strSqlString.Append("                         GROUP BY DOC_NAME, RESP_DEPARTMENT, CHECK_DAY" + "\n");
                strSqlString.Append("                       ) " + "\n");
                strSqlString.Append("                 GROUP BY RESP_DEPARTMENT, CHECK_DAY" + "\n");
                strSqlString.Append("                UNION ALL" + "\n");
                strSqlString.Append("                SELECT RESP_DEPARTMENT, 'WANJUN' AS TYPE, CHECK_DAY, SUM(TOTAL_QTY) AS TOTAL_QTY, SUM(ERROR_QTY) AS ERROR_QTY" + "\n");
                strSqlString.Append("                     , ROUND((SUM(TOTAL_QTY) - SUM(ERROR_QTY)) / SUM(TOTAL_QTY) * 100, 2) AS PER" + "\n");
                strSqlString.Append("                  FROM (" + "\n");
                strSqlString.Append("                        SELECT DOC_NAME, RESP_DEPARTMENT" + "\n");

                if (cdvFromToDate.DaySelector.SelectedValue.ToString() == "MONTH")
                {
                    strSqlString.Append("                             , GET_WORK_DATE(SUBSTR(CHECK_DAY,1,8),'M') AS CHECK_DAY" + "\n");
                }
                else
                {
                    strSqlString.Append("                             , GET_WORK_DATE(SUBSTR(CHECK_DAY,1,8),'W') AS CHECK_DAY" + "\n");
                }

                strSqlString.Append("                             , AVG(RESV_CMF_1) AS TOTAL_QTY" + "\n");
                strSqlString.Append("                             , SUM(DECODE(RESV_CMF_2, '표준 불완전', 1, 0)) AS ERROR_QTY" + "\n");
                strSqlString.Append("                          FROM (" + "\n");
                strSqlString.Append("                                SELECT ROW_NUMBER() OVER(PARTITION BY DOC_NO ORDER BY VERSION DESC) AS NUM" + "\n");
                strSqlString.Append("                                     , A.*" + "\n");
                strSqlString.Append("                                  FROM CQCMSCRDAT@RPTTOMES A" + "\n");
                strSqlString.Append("                                 WHERE 1=1" + "\n");
                strSqlString.Append("                                   AND DOC_NO LIKE 'HAS%'" + "\n");
                strSqlString.Append("                                   AND HIST_DELETE_FLAG = ' '" + "\n");
                strSqlString.Append("                                   AND RESV_CMF_2 <> '표준 미준수'" + "\n");
                strSqlString.Append("                                   AND RESV_CMF_1 <> ' '" + "\n");

                if (cdvFromToDate.DaySelector.SelectedValue.ToString() == "MONTH")
                {
                    strSqlString.Append("                                   AND GET_WORK_DATE(SUBSTR(CHECK_DAY,1,8),'M') BETWEEN '" + cdvFromToDate.FromYearMonth.Value.ToString("yyyyMM") + "' AND '" + cdvFromToDate.ToYearMonth.Value.ToString("yyyyMM") + "'" + "\n");
                }
                else
                {
                    strSqlString.Append("                                   AND GET_WORK_DATE(SUBSTR(CHECK_DAY,1,8),'W') BETWEEN '" + cdvFromToDate.HmFromWeek + "' AND '" + cdvFromToDate.HmToWeek + "'" + "\n");
                }

                strSqlString.Append("                               )" + "\n");
                strSqlString.Append("                         WHERE 1=1" + "\n");
                strSqlString.Append("                           AND NUM = 1" + "\n");
                strSqlString.Append("                         GROUP BY DOC_NAME, RESP_DEPARTMENT, CHECK_DAY" + "\n");
                strSqlString.Append("                       ) " + "\n");
                strSqlString.Append("                 GROUP BY RESP_DEPARTMENT, CHECK_DAY" + "\n");
                strSqlString.Append("               ) " + "\n");
                strSqlString.Append("         WHERE RESP_DEPARTMENT " + cdvDep.SelectedValueToQueryString + "\n");
                strSqlString.Append("         GROUP BY RESP_DEPARTMENT" + "\n");
                strSqlString.Append("       ) A" + "\n");

                //if (cboType.Text != "ALL")
                if (cboType.SelectedIndex != 0)
                {
                    strSqlString.Append(" WHERE (" + "\n");

                    for (int i = 0; i <= cdvFromToDate.SubtractBetweenFromToDate; i++)
                    {
                        //if (cboType.Text == "Standard fixation rate")
                        if (cboType.SelectedIndex == 3)
                        {
                            strSqlString.Append("         A" + i + " * B" + i);
                        }
                        //else if (cboType.Text == "Standard perfect rate")
                        else if (cboType.SelectedIndex == 1)
                        {
                            strSqlString.Append("         A" + i);
                        }
                        else
                        {
                            strSqlString.Append("         B" + i);
                        }

                        if (i != cdvFromToDate.SubtractBetweenFromToDate)
                            strSqlString.Append(" +" + "\n");
                    }

                    strSqlString.Append("       ) > 0" + "\n");
                }

                strSqlString.Append(" ORDER BY DEP" + "\n");
            }
            else 
            {
                strSqlString.Append("SELECT (SELECT DATA_1 FROM MGCMTBLDAT WHERE FACTORY = '" + GlobalVariable.gsAssyDefaultFactory + "' AND TABLE_NAME = 'H_DEPARTMENT' AND KEY_1 = RESP_DEPARTMENT) AS DEP" + "\n");                

                for (int i = 0; i <= cdvFromToDate.SubtractBetweenFromToDate; i++)
                {
                    strSqlString.Append("     , SUM(DECODE(CHECK_DAY, '" + strDate[i] + "', ROUND(QTY_1 / TOTAL_QTY * 100, 2), 0)) AS A" + i + "\n");                    
                }

                strSqlString.Append("  FROM (" + "\n");
                strSqlString.Append("        SELECT RESP_DEPARTMENT" + "\n");

                if (cdvFromToDate.DaySelector.SelectedValue.ToString() == "MONTH")
                {                    
                    strSqlString.Append("             , GET_WORK_DATE(SUBSTR(CHECK_DAY,1,8),'M') AS CHECK_DAY" + "\n");
                }
                else
                {
                    strSqlString.Append("             , GET_WORK_DATE(SUBSTR(CHECK_DAY,1,8),'W') AS CHECK_DAY" + "\n");
                }

                strSqlString.Append("             , COUNT(*) AS TOTAL_QTY" + "\n");

                if (rbt1.Checked == true) // 대책 조치율 현황
                {
                    strSqlString.Append("             , SUM(DECODE(REG_STEP, '품질팀-승인', 1, 0)) AS QTY_1" + "\n");
                }
                else // 재발생율 현황
                {
                    strSqlString.Append("             , SUM(DECODE(REG_TYPE, '재발생', 1, 0)) AS QTY_1" + "\n");
                }
                strSqlString.Append("          FROM (" + "\n");
                strSqlString.Append("                 SELECT ROW_NUMBER() OVER(PARTITION BY DOC_NO ORDER BY VERSION DESC) AS NUM" + "\n");
                strSqlString.Append("                      , A.*" + "\n");
                strSqlString.Append("                   FROM CQCMSCRDAT@RPTTOMES A" + "\n");
                strSqlString.Append("                  WHERE 1=1" + "\n");
                strSqlString.Append("                    AND DOC_NO LIKE 'HAS%'" + "\n");
                strSqlString.Append("                    AND HIST_DELETE_FLAG = ' '" + "\n");

                if (cdvFromToDate.DaySelector.SelectedValue.ToString() == "MONTH")
                {
                    strSqlString.Append("                    AND GET_WORK_DATE(SUBSTR(CHECK_DAY,1,8),'M') BETWEEN '" + cdvFromToDate.FromYearMonth.Value.ToString("yyyyMM") + "' AND '" + cdvFromToDate.ToYearMonth.Value.ToString("yyyyMM") + "'" + "\n");
                }
                else
                {
                    strSqlString.Append("                    AND GET_WORK_DATE(SUBSTR(CHECK_DAY,1,8),'W') BETWEEN '" + cdvFromToDate.HmFromWeek + "' AND '" + cdvFromToDate.HmToWeek + "'" + "\n");
                }
                
                strSqlString.Append("               )" + "\n");
                strSqlString.Append("         WHERE 1=1" + "\n");
                strSqlString.Append("           AND NUM = 1" + "\n");
                strSqlString.Append("         GROUP BY RESP_DEPARTMENT " + "\n");

                if (cdvFromToDate.DaySelector.SelectedValue.ToString() == "MONTH")
                {                    
                    strSqlString.Append("                , GET_WORK_DATE(SUBSTR(CHECK_DAY,1,8),'M')" + "\n");
                }
                else
                {
                    strSqlString.Append("                , GET_WORK_DATE(SUBSTR(CHECK_DAY,1,8),'W')" + "\n");
                }

                strSqlString.Append("       ) " + "\n");
                strSqlString.Append(" WHERE RESP_DEPARTMENT " + cdvDep.SelectedValueToQueryString + "\n");
                strSqlString.Append(" GROUP BY RESP_DEPARTMENT" + "\n");
            }

            if (GlobalVariable.gsUserGroup == "ADMIN_GROUP" || GlobalVariable.gsUserGroup == "HANA_ADMIN_GROUP")
            {
                System.Windows.Forms.Clipboard.SetText(strSqlString.ToString());
            }
            return strSqlString.ToString();
        }


        #endregion

        #region CHART 쿼리문
        private string MakeChartSqlString(int type)
        {
            StringBuilder strSqlString = new StringBuilder();

            string QueryCond1 = null;
            string[] strDate = cdvFromToDate.getSelectDate();

            //추가
            udcTableForm tableForm = (udcTableForm)btnSort.BindingForm;

            QueryCond1 = tableForm.SelectedValueToQueryContainNull;

            // 쿼리

            if (type == 2) // Chart 2
            {
                if (cdvFromToDate.DaySelector.SelectedValue.Equals("MONTH"))
                {
                    strSqlString.Append("SELECT GET_WORK_DATE(SUBSTR(CHECK_DAY,1,8),'M') AS CHECK_DAY" + "\n");
                }
                else
                {
                    strSqlString.Append("SELECT GET_WORK_DATE(SUBSTR(CHECK_DAY,1,8),'W') AS CHECK_DAY" + "\n");
                }

                // 표준 정착
                if (rbtStandard.Checked == true)
                {
                    strSqlString.Append("     , SUM(DECODE(RESV_CMF_2, '표준 미준수', 1, 0)) AS \"표준 미준수\"" + "\n");
                    strSqlString.Append("     , SUM(DECODE(RESV_CMF_2, '표준 불완전', 1, 0)) AS \"표준 오류\"" + "\n");
                }
                else if (rbt1.Checked == true) // 대책 조치
                {
                    strSqlString.Append("     , SUM(DECODE(REG_STEP, '품질팀-승인', 1, 0)) AS \"조치\"" + "\n");
                    strSqlString.Append("     , SUM(DECODE(REG_STEP, '품질팀-승인', 0, 1)) AS \"미조치\"" + "\n");
                }
                else // 재 발생
                {
                    strSqlString.Append("     , SUM(DECODE(REG_TYPE, '신규', 1, 0)) AS \"신규\"" + "\n");
                    strSqlString.Append("     , SUM(DECODE(REG_TYPE, '신규', 0, 1)) AS \"재발생\"" + "\n");
                }

                strSqlString.Append("  FROM (" + "\n");
                strSqlString.Append("        SELECT ROW_NUMBER() OVER(PARTITION BY DOC_NO ORDER BY VERSION DESC) AS NUM" + "\n");
                strSqlString.Append("             , A.*" + "\n");
                strSqlString.Append("          FROM CQCMSCRDAT@RPTTOMES A" + "\n");
                strSqlString.Append("         WHERE 1=1" + "\n");
                strSqlString.Append("           AND DOC_NO LIKE 'HAS%'" + "\n");
                strSqlString.Append("           AND HIST_DELETE_FLAG = ' '" + "\n");

                if (cdvFromToDate.DaySelector.SelectedValue.ToString() == "MONTH")
                {
                    strSqlString.Append("           AND GET_WORK_DATE(SUBSTR(CHECK_DAY,1,8),'M') BETWEEN '" + cdvFromToDate.FromYearMonth.Value.ToString("yyyyMM") + "' AND '" + cdvFromToDate.ToYearMonth.Value.ToString("yyyyMM") + "'" + "\n");
                }
                else
                {
                    strSqlString.Append("           AND GET_WORK_DATE(SUBSTR(CHECK_DAY,1,8),'W') BETWEEN '" + cdvFromToDate.HmFromWeek + "' AND '" + cdvFromToDate.HmToWeek + "'" + "\n");
                }

                strSqlString.Append("       )" + "\n");
                strSqlString.Append(" WHERE 1=1" + "\n");
                strSqlString.Append("   AND NUM = 1" + "\n");
                strSqlString.Append("   AND RESP_DEPARTMENT " + cdvDep.SelectedValueToQueryString + "\n");

                if (cdvFromToDate.DaySelector.SelectedValue.Equals("MONTH"))
                {
                    strSqlString.Append(" GROUP BY GET_WORK_DATE(SUBSTR(CHECK_DAY,1,8),'M')" + "\n");
                }
                else
                {
                    strSqlString.Append(" GROUP BY GET_WORK_DATE(SUBSTR(CHECK_DAY,1,8),'W')" + "\n");
                }

                strSqlString.Append(" ORDER BY CHECK_DAY" + "\n");
            }
            else // Chart 3 (부서별)
            {
                strSqlString.Append("SELECT (SELECT DATA_1 FROM MGCMTBLDAT WHERE TABLE_NAME = 'H_DEPARTMENT' AND FACTORY = '" + GlobalVariable.gsAssyDefaultFactory + "' AND KEY_1 = RESP_DEPARTMENT) AS RESP_DEPARTMENT" + "\n");
                strSqlString.Append("     , COUNT(*) AS QTY_1" + "\n");
                strSqlString.Append("  FROM (" + "\n");
                strSqlString.Append("        SELECT ROW_NUMBER() OVER(PARTITION BY DOC_NO ORDER BY VERSION DESC) AS NUM" + "\n");
                strSqlString.Append("             , A.*" + "\n");
                strSqlString.Append("          FROM CQCMSCRDAT@RPTTOMES A" + "\n");
                strSqlString.Append("         WHERE 1=1" + "\n");
                strSqlString.Append("           AND DOC_NO LIKE 'HAS%'" + "\n");
                strSqlString.Append("           AND HIST_DELETE_FLAG = ' '" + "\n");

                if (cdvFromToDate.DaySelector.SelectedValue.ToString() == "MONTH")
                {
                    strSqlString.Append("           AND GET_WORK_DATE(SUBSTR(CHECK_DAY,1,8),'M') BETWEEN '" + cdvFromToDate.FromYearMonth.Value.ToString("yyyyMM") + "' AND '" + cdvFromToDate.ToYearMonth.Value.ToString("yyyyMM") + "'" + "\n");
                }
                else
                {
                    strSqlString.Append("           AND GET_WORK_DATE(SUBSTR(CHECK_DAY,1,8),'W') BETWEEN '" + cdvFromToDate.HmFromWeek + "' AND '" + cdvFromToDate.HmToWeek + "'" + "\n");
                }
                                
                strSqlString.Append("       )" + "\n");
                strSqlString.Append(" WHERE 1=1" + "\n");
                strSqlString.Append("   AND NUM = 1" + "\n");
                strSqlString.Append("   AND RESP_DEPARTMENT " + cdvDep.SelectedValueToQueryString + "\n");
                strSqlString.Append(" GROUP BY RESP_DEPARTMENT" + "\n");
                strSqlString.Append(" ORDER BY RESP_DEPARTMENT" + "\n");
            }
            //if (GlobalVariable.gsUserGroup == "ADMIN_GROUP" || GlobalVariable.gsUserGroup == "HANA_ADMIN_GROUP")
            //{
            //    System.Windows.Forms.Clipboard.SetText(strSqlString.ToString());
            //}

            return strSqlString.ToString();
        }


        #endregion

        #region " MakeChart : Chart 처리 "

        /// <summary>
        ///  Chart 1 생성
        /// </summary>
        private void ShowChart1()
        {
            int chcnt = 1;
            int cnt = 0;
            int iColumnsCount = cdvFromToDate.SubtractBetweenFromToDate + 1;

            // 차트 설정
            udcChartFX1.RPT_2_ClearData();
            udcChartFX1.RPT_3_OpenData(2, iColumnsCount);
            int[] w_columns = new Int32[iColumnsCount];
            int[] j_columns = new Int32[iColumnsCount];
            int[] t_columns = new Int32[iColumnsCount];
            int[] columnsHeader = new Int32[iColumnsCount];

            for (int i = 0; i < w_columns.Length; i++)
            {
                //if (rbtStandard.Checked == true && cboType.Text == "ALL")
                if (rbtStandard.Checked == true && cboType.SelectedIndex == 0)
                {
                    w_columns[i] = chcnt;
                    chcnt += 1;

                    j_columns[i] = chcnt;
                    chcnt += 1;

                    t_columns[i] = chcnt;
                    chcnt += 1;
                }
                else
                {
                    w_columns[i] = chcnt;
                    chcnt += 1;
                }                
            }

            //if (rbtStandard.Checked == true && cboType.Text == "ALL")
            if (rbtStandard.Checked == true && cboType.SelectedIndex == 0)
            {
                //표준 완전율
                double max1 = udcChartFX1.RPT_4_AddData(spdData, new int[] { cnt }, w_columns, SeriseType.Rows);

                //표준 준수율
                max1 = udcChartFX1.RPT_4_AddData(spdData, new int[] { cnt }, j_columns, SeriseType.Rows);

                //표준 정착율
                max1 = udcChartFX1.RPT_4_AddData(spdData, new int[] { cnt }, t_columns, SeriseType.Rows);

                udcChartFX1.RPT_5_CloseData();

                udcChartFX1.RPT_6_SetGallery(ChartType.Line, 0, 1, "", AsixType.Y, DataTypes.Initeger, 100 * 1.1);
                udcChartFX1.RPT_6_SetGallery(ChartType.Line, 1, 1, "", AsixType.Y, DataTypes.Initeger, 100 * 1.1);
                udcChartFX1.RPT_6_SetGallery(ChartType.Line, 2, 1, "", AsixType.Y, DataTypes.Initeger, 100 * 1.1);
            }
            else
            {
                double max1 = udcChartFX1.RPT_4_AddData(spdData, new int[] { cnt }, w_columns, SeriseType.Rows);

                udcChartFX1.RPT_5_CloseData();

                udcChartFX1.RPT_6_SetGallery(ChartType.Line, 0, 1, "", AsixType.Y, DataTypes.Initeger, 100 * 1.1);
            }

            
            udcChartFX1.PointLabels = false;
            udcChartFX1.AxisY.Gridlines = false;
            udcChartFX1.AxisY.DataFormat.Decimals = 2;

            string[] strDate = cdvFromToDate.getSelectDate();
            // x축 Label 표시 부분
            for (int i = 0; i <= cdvFromToDate.SubtractBetweenFromToDate; i++)
            {
                udcChartFX1.Legend[i] = strDate[i];
            }
                        
            udcChartFX1.SerLegBox = true;
            udcChartFX1.SerLegBoxObj.Docked = SoftwareFX.ChartFX.Docked.Top;

            //if (rbtStandard.Checked == true && cboType.Text == "ALL")
            if (rbtStandard.Checked == true && cboType.SelectedIndex == 0)
            {
                udcChartFX1.SerLeg[0] = "Standard perfect rate";
                udcChartFX1.SerLeg[1] = "Standard compliance rate";
                udcChartFX1.SerLeg[2] = "Standard fixation rate";
            }
            else
            {
                udcChartFX1.SerLeg[0] = cboType.Text;
            }


            udcChartFX1.AxisX.Staggered = false;            
        }

        /// <summary>
        ///  Chart 2 생성
        /// </summary>
        private void ShowChart2()
        {            
            udcChartFX2.RPT_2_ClearData();

            DataTable dt = CmnFunction.oComm.GetFuncDataTable("DYNAMIC", MakeChartSqlString(2));

            if (dt == null || dt.Rows.Count < 1)
                return;

            udcChartFX2.DataSource = dt;
            udcChartFX2.Gallery = SoftwareFX.ChartFX.Gallery.Bar;
            udcChartFX2.Stacked = SoftwareFX.ChartFX.Stacked.Normal;
            udcChartFX2.SerLegBox = true;
            udcChartFX2.SerLegBoxObj.Docked = SoftwareFX.ChartFX.Docked.Top;
            udcChartFX2.LegendBox = false;
            udcChartFX2.PointLabels = true;
            udcChartFX2.AxisX.Staggered = false;
            udcChartFX2.AxisY.Gridlines = false;

            udcChartFX2.Series[0].PointLabelColor = Color.Red;
            udcChartFX2.Series[1].PointLabelColor = Color.Blue;

            udcChartFX2.AxisY.Max = udcChartFX2.AxisY.Max * 1.5;
        }

        /// <summary>
        ///  Chart 3 생성
        /// </summary>
        private void ShowChart3()
        {
            udcChartFX3.RPT_2_ClearData();

            DataTable dt = CmnFunction.oComm.GetFuncDataTable("DYNAMIC", MakeChartSqlString(3));

            if (dt == null || dt.Rows.Count < 1)
                return;

            udcChartFX3.DataSource = dt;
            udcChartFX3.Gallery = SoftwareFX.ChartFX.Gallery.Bar;            
            udcChartFX3.SerLegBox = true;
            udcChartFX3.SerLegBoxObj.Docked = SoftwareFX.ChartFX.Docked.Top;
            udcChartFX3.SerLeg[0] = "부서별 발생 건";
            udcChartFX3.LegendBox = false;
            udcChartFX3.PointLabels = true;
            udcChartFX3.AxisX.Staggered = true;
            udcChartFX3.AxisY.Gridlines = false;

            udcChartFX3.Series[0].PointLabelColor = Color.Red;
            
            udcChartFX3.AxisY.Max = udcChartFX3.AxisY.Max * 1.2;
        }

        #endregion


        #region " Button Event "
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>


        private void btnView_Click(object sender, EventArgs e)
        {

            DataTable dt = null;

            if (CheckField() == false) return;

            GridColumnInit();
            udcChartFX1.RPT_1_ChartInit();
            udcChartFX2.RPT_1_ChartInit();
            udcChartFX3.RPT_1_ChartInit();

            spdData_Sheet1.RowCount = 0;

            try
            {
                LoadingPopUp.LoadIngPopUpShow(this);

                dt = CmnFunction.oComm.GetFuncDataTable("DYNAMIC", MakeSqlString());

                if (dt.Rows.Count == 0)
                {
                    dt.Dispose();
                    LoadingPopUp.LoadingPopUpHidden();
                    CmnFunction.ShowMsgBox(RptMessages.GetMessage("STD010", GlobalVariable.gcLanguage));
                    return;
                }

                int[] rowType = spdData.RPT_DataBindingWithSubTotal(dt, 0, 0, 1, null, null, btnSort);
                //Total부분 셀머지
                spdData.RPT_FillDataSelectiveCells("TOTAL", 0, 1, 0, 1, true, Align.Center, VerticalAlign.Center);

                //Totla의 평균 값
                for (int i = 1; i < spdData.ActiveSheet.ColumnCount; i++)
                {
                    SetAvgVertical2(i);
                }

                spdData.RPT_AutoFit(false);

                dt.Dispose();

                //int a = spdData.ActiveSheet.ColumnCount;

                ShowChart1();
                ShowChart2();
                ShowChart3();

                //Chart 생성
                
                //if (spdData.ActiveSheet.RowCount > 0)
                //    fnMakeChart(spdData, spdData.ActiveSheet.RowCount);
                  
                
            }
            catch (Exception ex)
            {
                LoadingPopUp.LoadingPopUpHidden();
                CmnFunction.ShowMsgBox(ex.Message);
            }
            finally
            {
                LoadingPopUp.LoadingPopUpHidden();
                Cursor.Current = Cursors.Default;
            }
        }



        public void SetAvgVertical2(int nColPos)
        {
            double sum = 0;
            double divide = 0;


            for (int i = 1; i < spdData.ActiveSheet.Rows.Count; i++)
            {
                sum += Convert.ToDouble(spdData.ActiveSheet.Cells[i, nColPos].Value);
                
                if (spdData.ActiveSheet.Cells[i, nColPos].Value != null)
                {
                    divide += 1;
                }                
            }
            if (divide == 0)
            {
                divide = 1;
            }
            spdData.ActiveSheet.Cells[0, nColPos].Value = sum / divide;
        }

        /// <summary>
        /// Excel Export
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnExcelExport_Click(object sender, EventArgs e)
        {    
            ExcelHelper.Instance.subMakeExcel(spdData, udcChartFX1, this.lblTitle.Text, " ^ ", " ^ ");
        }

        #endregion

        private void PQC031101_Load(object sender, EventArgs e)
        {
            // 테이블레이아웃 챠트부분 셀 병합
            //tableLayoutPanel1.SetColumnSpan(spdData, 2);            
        }
        
        private void rbtStandard_CheckedChanged(object sender, EventArgs e)
        {
            if (rbtStandard.Checked == true)
                cboType.Visible = true;
            else
                cboType.Visible = false;
        }

        private void cdvDep_ValueButtonPress(object sender, EventArgs e)
        {
            string strQuery = string.Empty;

            strQuery += "SELECT KEY_1 AS CODE, DATA_1 AS DATA" + "\n";
            strQuery += "  FROM MGCMTBLDAT " + "\n";
            strQuery += " WHERE FACTORY = '" + GlobalVariable.gsAssyDefaultFactory + "' " + "\n";
            strQuery += "   AND TABLE_NAME = 'H_DEPARTMENT' " + "\n";
            strQuery += " ORDER BY DATA " + "\n";

            cdvDep.sDynamicQuery = strQuery;
        }
    }
}
