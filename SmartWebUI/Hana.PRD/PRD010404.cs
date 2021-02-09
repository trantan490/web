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

namespace Hana.PRD
{
    public partial class PRD010404 : Miracom.SmartWeb.UI.Controls.udcCUSReport002
    {

        /****************************************************/
        /* Comment : 공정간수불.                            */
        /*                                                  */
        /* Created By : bee-jae jung (2009-06-05-금요일)    */
        /*                                                  */
        /* Modified By : bee-jae jung (2009-07-02-목요일)   */
        /****************************************************/
        
        #region " PRD010404 : Program Initial "

        public PRD010404()
        {
            try
            {
                Cursor.Current = Cursors.WaitCursor;

                InitializeComponent();
                cdvFromToDate.AutoBinding();
                SortInit();
                GridColumnInit();

                // 2009-02-20-정비재 : Factory Control을 사용하지 않기 위하여, 값을 입력한다.
                cdvFactory.Text = "HMKA1, HMKT1";
                cdvFactory.Visible = false;

                // 2009-06-05-정비재 : Form의 초기값을 설정한다.
                rbSearchGubn01.Checked = true;      // 과거기준

                cbCutoff_DT.Visible = true;
                cdvFromToDate.Visible = false;
                gbBaseGubn.Visible = false;

                // 2009-06-05-정비재 : 공정간수불 Table에 있는 CUTOFF_DT 조회
                DataTable dt = null;
                string Qry = "SELECT CUTOFF_DT || ' ' || SUBSTR(FROM_TIME, 1, 4) || '년' || SUBSTR(FROM_TIME, 5, 2) || '월' || SUBSTR(FROM_TIME, 7, 2) || '일' || SUBSTR(FROM_TIME, 9, 2) || '시'"
                           + "       || ' ~ ' || SUBSTR(TO_TIME, 1, 4) || '년' || SUBSTR(TO_TIME, 5, 2) || '월' || SUBSTR(TO_TIME, 7, 2) || '일' || SUBSTR(TO_TIME, 9, 2) || '시'"
                           + "  FROM RWIPMATCLT"
                           + " GROUP BY CUTOFF_DT, FROM_TIME, TO_TIME"
                           + " ORDER BY CUTOFF_DT DESC, FROM_TIME, TO_TIME";
                dt = CmnFunction.oComm.GetFuncDataTable("DYNAMIC", Qry);
                for (int iRow = 0; iRow < dt.Rows.Count; iRow++)
                {
                    cbCutoff_DT.Items.Add(dt.Rows[iRow][0].ToString());
                }
            }
            catch (Exception ex)
            {
                CmnFunction.ShowMsgBox(ex.Message);
            }
            finally
            {
                Cursor.Current = Cursors.Default;
            }
        }

        #endregion


        #region " Constant Definition "

        #endregion


        #region " CheckField : 유효성 검사 "

        /// <summary 1. 유효성 검사>
        /// <remarks></remarks>
        /// </summary>
        /// <returns></returns>
        private Boolean CheckField()
        {
            //if (cdvFactory.Text.TrimEnd() == "")
            //{
            //    CmnFunction.ShowMsgBox(RptMessages.GetMessage("STD001", GlobalVariable.gcLanguage));
            //    return false;
            //}

            return true;
        }

        #endregion


        #region " GridColumnInit : Sheet Title 설정 "

        /// <summary>
        /// 2. 헤더 생성
        /// </summary>
        private void GridColumnInit()
        {
            int iIdx;
            try
            {
                spdData.RPT_ColumnInit();
                // 2009-03-23-정비재 : 임태성대리 요청으로 표시되는 순서를 변경함
                iIdx = 0;
                spdData.RPT_AddBasicColumn("CUSTOMER", 0, iIdx, Visibles.True, Frozen.False, Align.Center, Merge.True, Formatter.String, 50);
                iIdx += 1;
                spdData.RPT_AddBasicColumn("PIN_TYPE", 0, iIdx, Visibles.True, Frozen.False, Align.Center, Merge.True, Formatter.String, 40);
                // FAMILY
                if (udcWIPCondition2.Text != "ALL" && udcWIPCondition2.Text != "")
                {
                    iIdx += 1;
                    spdData.RPT_AddBasicColumn("FAMILY", 0, iIdx, Visibles.True, Frozen.False, Align.Center, Merge.True, Formatter.String, 40);
                }
                // PACKAGE
                if (udcWIPCondition3.Text != "ALL" && udcWIPCondition3.Text != "")
                {
                    iIdx += 1;
                    spdData.RPT_AddBasicColumn("PACKAGE", 0, iIdx, Visibles.True, Frozen.False, Align.Center, Merge.True, Formatter.String, 40);
                }
                // TYPE1
                if (udcWIPCondition4.Text != "ALL" && udcWIPCondition4.Text != "")
                {
                    iIdx += 1;
                    spdData.RPT_AddBasicColumn("TYPE1", 0, iIdx, Visibles.True, Frozen.False, Align.Center, Merge.True, Formatter.String, 40);
                }
                // TYPE2
                if (udcWIPCondition5.Text != "ALL" && udcWIPCondition5.Text != "")
                {
                    iIdx += 1;
                    spdData.RPT_AddBasicColumn("TYPE2", 0, iIdx, Visibles.True, Frozen.False, Align.Center, Merge.True, Formatter.String, 40);
                }
                // LEAD COUNT
                if (udcWIPCondition6.Text != "ALL" && udcWIPCondition6.Text != "")
                {
                    iIdx += 1;
                    spdData.RPT_AddBasicColumn("LEAD_COUNT", 0, iIdx, Visibles.True, Frozen.False, Align.Center, Merge.True, Formatter.String, 40);
                }
                // DENSITY
                if (udcWIPCondition7.Text != "ALL" && udcWIPCondition7.Text != "")
                {
                    iIdx += 1;
                    spdData.RPT_AddBasicColumn("DENSITY", 0, iIdx, Visibles.True, Frozen.False, Align.Center, Merge.True, Formatter.String, 40);
                }
                // GENERATION
                if (udcWIPCondition8.Text != "ALL" && udcWIPCondition8.Text != "")
                {
                    iIdx += 1;
                    spdData.RPT_AddBasicColumn("GENERATION", 0, iIdx, Visibles.True, Frozen.False, Align.Center, Merge.True, Formatter.String, 40);
                }
                iIdx += 1;
                spdData.RPT_AddBasicColumn("PRODUCT", 0, iIdx, Visibles.True, Frozen.False, Align.Center, Merge.True, Formatter.String, 80);

                spdData.RPT_AddBasicColumn("AREA", 0, iIdx + 1, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.String, 70);
                spdData.RPT_AddBasicColumn("BOHA", 0, iIdx + 2, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 70);
                spdData.RPT_AddBasicColumn("BOHT", 0, iIdx + 3, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 70);
                spdData.RPT_AddBasicColumn("TKINA", 0, iIdx + 4, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 70);
                spdData.RPT_AddBasicColumn("TKINT", 0, iIdx + 5, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 70);
                spdData.RPT_AddBasicColumn("EOHA", 0, iIdx + 6, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 70);
                spdData.RPT_AddBasicColumn("EOHT", 0, iIdx + 7, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 70);
                spdData.RPT_AddBasicColumn("H3 SHIP", 0, iIdx + 8, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 70);
                spdData.RPT_AddBasicColumn("H4 SHIP", 0, iIdx + 9, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 70);
                spdData.RPT_AddBasicColumn("TKOUTA", 0, iIdx + 10, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 70);
                spdData.RPT_AddBasicColumn("TKOUTT", 0, iIdx + 11, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 70);
                spdData.RPT_AddBasicColumn("LOSSA", 0, iIdx + 12, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 70);
                spdData.RPT_AddBasicColumn("LOSST", 0, iIdx + 13, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 70);
                spdData.RPT_AddBasicColumn("BONUSA", 0, iIdx + 14, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 70);
                spdData.RPT_AddBasicColumn("BONUST", 0, iIdx + 15, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 70);
                spdData.RPT_AddBasicColumn("RES HA H2", 0, iIdx + 16, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 70);
                spdData.RPT_AddBasicColumn("CHG PROD", 0, iIdx + 17, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 70);
                spdData.RPT_AddBasicColumn("MULT TRANS", 0, iIdx + 18, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 70);
                spdData.RPT_AddBasicColumn("RET H3 H2", 0, iIdx + 19, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 70);
                spdData.RPT_AddBasicColumn("RET H4 H3", 0, iIdx + 20, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 70);
                spdData.RPT_AddBasicColumn("H3 RECV", 0, iIdx + 21, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 70);
                spdData.RPT_AddBasicColumn("RET CO H3", 0, iIdx + 22, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 70);
                spdData.RPT_AddBasicColumn("RET H3 CO", 0, iIdx + 23, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 70);
                spdData.RPT_AddBasicColumn("RET CO H4", 0, iIdx + 24, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 70);
                spdData.RPT_AddBasicColumn("H4 RET", 0, iIdx + 25, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 70);
                spdData.RPT_AddBasicColumn("DIFFER", 0, iIdx + 26, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 70);

                // 2009-03-24-정비재 : 사용않함, 필요없는 것 같음
                //Group항목이 있을경우 반드시 선언해줄것.
                //spdData.RPT_ColumnConfigFromTable(btnSort);
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
            try
            {
                ((udcTableForm)(this.btnSort.BindingForm)).Clear();
                ((udcTableForm)(this.btnSort.BindingForm)).AddRow("CUSTOMER", "B.MAT_GRP_1", "MAT_GRP_1", "NVL((SELECT DATA_1 FROM MGCMTBLDAT WHERE TABLE_NAME = 'H_CUSTOMER' AND KEY_1 = MAT_GRP_1 AND ROWNUM=1), '-') AS CUSTOMER", true);
                ((udcTableForm)(this.btnSort.BindingForm)).AddRow("PIN_TYPE", "B.MAT_CMF_10", "PIN_TYPE", "B.MAT_CMF_10", true);
                // FAMILY
                if (udcWIPCondition2.Text != "ALL" && udcWIPCondition2.Text != "")
                {
                    ((udcTableForm)(this.btnSort.BindingForm)).AddRow("FAMILY", "B.MAT_GRP_2", "MAT_GRP_2", "B.MAT_GRP_2", true);
                }
                // PACKAGE
                if (udcWIPCondition3.Text != "ALL" && udcWIPCondition3.Text != "")
                {
                    ((udcTableForm)(this.btnSort.BindingForm)).AddRow("PACKAGE", "B.MAT_GRP_3", "MAT_GRP_3", "B.MAT_GRP_3", true);
                }
                // TYPE1
                if (udcWIPCondition4.Text != "ALL" && udcWIPCondition4.Text != "")
                {
                    ((udcTableForm)(this.btnSort.BindingForm)).AddRow("TYPE1", "B.MAT_GRP_4", "MAT_GRP_4", "B.MAT_GRP_4", true);
                }
                // TYPE2
                if (udcWIPCondition5.Text != "ALL" && udcWIPCondition5.Text != "")
                {
                    ((udcTableForm)(this.btnSort.BindingForm)).AddRow("TYPE2", "B.MAT_GRP_5", "MAT_GRP_5", "B.MAT_GRP_5", true);
                }
                // LEAD COUNT
                if (udcWIPCondition6.Text != "ALL" && udcWIPCondition6.Text != "")
                {
                    ((udcTableForm)(this.btnSort.BindingForm)).AddRow("LEAD_COUNT", "B.MAT_GRP_6", "MAT_GRP_6", "B.MAT_GRP_6", true);
                }
                // DENSITY
                if (udcWIPCondition7.Text != "ALL" && udcWIPCondition7.Text != "")
                {
                    ((udcTableForm)(this.btnSort.BindingForm)).AddRow("DENSITY", "B.MAT_GRP_7", "MAT_GRP_7", "B.MAT_GRP_7", true);
                }
                // GENERATION
                if (udcWIPCondition8.Text != "ALL" && udcWIPCondition8.Text != "")
                {
                    ((udcTableForm)(this.btnSort.BindingForm)).AddRow("GENERATION", "B.MAT_GRP_8", "MAT_GRP_8", "B.MAT_GRP_8", true);
                }
                ((udcTableForm)(this.btnSort.BindingForm)).AddRow("PRODUCT", "A.MAT_ID", "MAT_ID", "A.MAT_ID", true);
            }
            catch (Exception ex)
            {
                CmnFunction.ShowMsgBox(ex.Message);
                LoadingPopUp.LoadingPopUpHidden();
            }
        }

        #endregion


        #region " MakeSqlString : Sql Query문 "

        /// <summary>
        /// 4. 쿼리 생성
        /// </summary>
        /// <returns></returns>
        /// 
        private string MakeSqlString_과거기준()
        {
            /****************************************************/
            /* Comment : 공정간수불 과거data를 검색하는 Query문 */
            /*                                                  */
            /* Created By : bee-jae jung(2009-06-05-금요일)     */
            /*                                                  */
            /* Modified By : bee-jae jung(2009-07-02-목요일)    */
            /****************************************************/
            StringBuilder strSqlString = new StringBuilder();
            string strOrderBy = "";

            try
            {
                Cursor.Current = Cursors.WaitCursor;

                strSqlString.Append("SELECT A.CUST_CODE, A.PACK_PIN_TYPE " + "\n");
                strOrderBy = "A.CUST_CODE, A.PACK_PIN_TYPE";
                //상세 조회에 따른 SQL문 생성
                /********************************************************************************************************************/
                if (udcWIPCondition2.Text != "ALL" && udcWIPCondition2.Text != "")
                {
                    strSqlString.Append("     , A.FAMILY " + "\n");
                    strOrderBy = strOrderBy + ", A.FAMILY";
                }
                if (udcWIPCondition3.Text != "ALL" && udcWIPCondition3.Text != "")
                {
                    strSqlString.Append("     , A.PACKAGE " + "\n");
                    strOrderBy = strOrderBy + ", A.PACKAGE";
                }
                if (udcWIPCondition4.Text != "ALL" && udcWIPCondition4.Text != "")
                {
                    strSqlString.Append("     , A.TYPE1 " + "\n");
                    strOrderBy = strOrderBy + ", A.TYPE1";
                }
                if (udcWIPCondition5.Text != "ALL" && udcWIPCondition5.Text != "")
                {
                    strSqlString.Append("     , A.TYPE2 " + "\n");
                    strOrderBy = strOrderBy + ", A.TYPE2";
                }
                if (udcWIPCondition6.Text != "ALL" && udcWIPCondition6.Text != "")
                {
                    strSqlString.Append("     , A.LEAD_COUNT " + "\n");
                    strOrderBy = strOrderBy + ", A.LEAD_COUNT";
                }
                if (udcWIPCondition7.Text != "ALL" && udcWIPCondition7.Text != "")
                {
                    strSqlString.Append("     , A.DENSITY " + "\n");
                    strOrderBy = strOrderBy + ", A.DENSITY";
                }
                if (udcWIPCondition8.Text != "ALL" && udcWIPCondition8.Text != "")
                {
                    strSqlString.Append("     , A.GENERATION " + "\n");
                    strOrderBy = strOrderBy + ", A.GENERATION";
                }
                strSqlString.Append("     , A.MAT_ID " + "\n");
                strOrderBy = strOrderBy + ", A.MAT_ID";
                /********************************************************************************************************************/
                strSqlString.Append("     , A.AREA AS AREA " + "\n");
                strSqlString.Append("     , SUM(A.BOHA) AS BOHA " + "\n");
                strSqlString.Append("     , SUM(A.BOHT) AS BOHT " + "\n");
                strSqlString.Append("     , SUM(A.TKINA) AS TKINA " + "\n");
                strSqlString.Append("     , SUM(A.TKINT) AS TKINT " + "\n");
                strSqlString.Append("     , SUM(A.EOHA) AS EOHA " + "\n");
                strSqlString.Append("     , SUM(A.EOHT) AS EOHT " + "\n");
                strSqlString.Append("     , SUM(A.SHIP_H3) AS SHIP_H3 " + "\n");
                strSqlString.Append("     , SUM(A.SHIP_H4) AS SHIP_H4 " + "\n");
                strSqlString.Append("     , SUM(A.TKOUTA) AS TKOUTA " + "\n");
                strSqlString.Append("     , SUM(A.TKOUTT) AS TKOUTT " + "\n");
                strSqlString.Append("     , SUM(A.LOSSA) AS LOSSA " + "\n");
                strSqlString.Append("     , SUM(A.LOSST) AS LOSST " + "\n");
                strSqlString.Append("     , SUM(A.BONUSA) AS BONUSA " + "\n");
                strSqlString.Append("     , SUM(A.BONUST) AS BONUST " + "\n");
                strSqlString.Append("     , SUM(A.RES_HA_H2) AS RES_HA_H2 " + "\n");
                strSqlString.Append("     , SUM(A.CHAG_PROD) AS CHAG_PROD " + "\n");
                strSqlString.Append("     , SUM(A.MULT_TRANS) AS MULT_TRANS " + "\n");
                strSqlString.Append("     , SUM(A.RET_H3_H2) AS RET_H3_H2 " + "\n");
                strSqlString.Append("     , SUM(A.RET_H4_H3) AS RET_H4_H3 " + "\n");
                strSqlString.Append("     , SUM(A.RECV_H3) AS RECV_H3 " + "\n");
                strSqlString.Append("     , SUM(A.RET_CO_H3) AS RET_CO_H3 " + "\n");
                strSqlString.Append("     , SUM(A.RET_H3_CO) AS RET_H3_CO " + "\n");
                strSqlString.Append("     , SUM(A.RET_CO_H4) AS RET_CO_H4 " + "\n");
                strSqlString.Append("     , SUM(A.RET_H4) AS RET_H4 " + "\n");
                strSqlString.Append("     , SUM(A.DIFF) AS DIFF " + "\n");
                strSqlString.Append("  FROM RWIPMATCLT A " + "\n");
                strSqlString.AppendFormat(" WHERE A.CUTOFF_DT = '{0}' " + "\n", cbCutoff_DT.Text.Split(' ')[0].Trim());
                //상세 조회에 따른 SQL문 생성
                /********************************************************************************************************************/
                if (udcWIPCondition1.Text != "ALL" && udcWIPCondition1.Text != "")
                    strSqlString.AppendFormat("   AND A.CUST_CODE {0} " + "\n", udcWIPCondition1.SelectedValueToQueryString);
                if (udcWIPCondition2.Text != "ALL" && udcWIPCondition2.Text != "")
                    strSqlString.AppendFormat("   AND A.FAMILY {0} " + "\n", udcWIPCondition2.SelectedValueToQueryString);
                if (udcWIPCondition3.Text != "ALL" && udcWIPCondition3.Text != "")
                    strSqlString.AppendFormat("   AND A.PACKAGE {0} " + "\n", udcWIPCondition3.SelectedValueToQueryString);
                if (udcWIPCondition4.Text != "ALL" && udcWIPCondition4.Text != "")
                    strSqlString.AppendFormat("   AND A.TYPE1 {0} " + "\n", udcWIPCondition4.SelectedValueToQueryString);
                if (udcWIPCondition5.Text != "ALL" && udcWIPCondition5.Text != "")
                    strSqlString.AppendFormat("   AND A.TYPE2 {0} " + "\n", udcWIPCondition5.SelectedValueToQueryString);
                if (udcWIPCondition6.Text != "ALL" && udcWIPCondition6.Text != "")
                    strSqlString.AppendFormat("   AND A.LEAD_COUNT {0} " + "\n", udcWIPCondition6.SelectedValueToQueryString);
                if (udcWIPCondition7.Text != "ALL" && udcWIPCondition7.Text != "")
                    strSqlString.AppendFormat("   AND A.DENSITY {0} " + "\n", udcWIPCondition7.SelectedValueToQueryString);
                if (udcWIPCondition8.Text != "ALL" && udcWIPCondition8.Text != "")
                    strSqlString.AppendFormat("   AND A.GENERATION {0} " + "\n", udcWIPCondition8.SelectedValueToQueryString);
                /********************************************************************************************************************/
                strSqlString.AppendFormat("   AND A.MAT_ID LIKE '{0}%' " + "\n", txtProduct.Text.Trim());
                strSqlString.AppendFormat("   AND A.LOT_TYPE LIKE '{0}%' " + "\n", txtLotType.Text.Trim());
                strSqlString.Append(" GROUP BY A.CUST_CODE, A.PACK_PIN_TYPE, A.MAT_ID, A.AREA " + "\n");
                strSqlString.AppendFormat(" ORDER BY {0}, DECODE(A.AREA, 'HMK2', 1, 'D/A', 2, 'BLAB', 3, 'BOND', 4, 'MOLD', 5, 'FINI', 6, 'HMK3', 7, 'TEST', 8, 'HMK4', 9, 'HMK5', 10) " + "\n", strOrderBy);

                Clipboard.SetText(strSqlString.ToString());
                return strSqlString.ToString();
            }
            catch (Exception ex)
            {
                LoadingPopUp.LoadingPopUpHidden();
                CmnFunction.ShowMsgBox(ex.Message);
                return "";
            }
            finally
            {
                Cursor.Current = Cursors.Default;
            }
        }

        private string MakeSqlString_현재기준()
        {
            /********************************************************/
            /* Comment : 공정간수불 현재 data를 검색하는 Query문    */
            /*                                                      */
            /* Created By : bee-jae jung(2009-02-16)                */
            /*                                                      */
            /* Modified By : bee-jae jung(2009-06-02-화요일)        */
            /********************************************************/

            StringBuilder strSqlString = new StringBuilder();

            string QueryCond1 = null, QueryCond2 = null, QueryCond3 = null;
            string strFromDate = null, strToDate = null, strFromCutOffDate = null, strToCutOffDate = null;

            udcTableForm tableForm = (udcTableForm)btnSort.BindingForm;
            DateTime dtDate;

            try
            {
                QueryCond1 = tableForm.SelectedValueToQueryContainNull;
                QueryCond2 = tableForm.SelectedValue2ToQueryContainNull;
                QueryCond3 = tableForm.SelectedValue3ToQueryContainNull;

                // 수불기간
                switch (cdvFromToDate.DaySelector.SelectedValue.ToString())
                {
                    case "DAY":
                        // 2009-03-31-정비재 : 기준정보 시간을 선택한다.
                        if (rbBaseTime01.Checked == true)
                        {
                            // 22시 기준
                            dtDate = DateTime.Parse(cdvFromToDate.FromDate.Text);
                            dtDate = dtDate.AddDays(-1);
                            strFromDate = dtDate.ToString("yyyyMMdd") + "220001";
                            strToDate = cdvFromToDate.ToDate.Text.Replace("-", "") + "220000";
                        }
                        else if (rbBaseTime02.Checked == true)
                        {
                            // 06시 기준
                            strFromDate = cdvFromToDate.FromDate.Text.Replace("-", "") + "060001";
                            strToDate = cdvFromToDate.ToDate.Text.Replace("-", "") + "060000";
                        }
                        strFromCutOffDate = strFromDate.Substring(0, 10);
                        strToCutOffDate = strToDate.Substring(0, 10);
                        break;
                   default:
                        MessageBox.Show("수불기간은 일(Day) 단위로만 할 수 있습니다!" + "\r\n\r\n"
                                      + "일(Day) 단위를 선택하여 주십시오!", this.Title, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        return "";
                }

                strSqlString.AppendFormat("SELECT {0} " + "\n", QueryCond3);
                strSqlString.Append("     , A.AREA AS AREA " + "\n");
                strSqlString.Append("     , SUM(A.BOHA) AS BOHA " + "\n");
                strSqlString.Append("     , SUM(A.BOHT) AS BOHT " + "\n");
                strSqlString.Append("     , SUM(A.TKINA) AS TKINA " + "\n");
                strSqlString.Append("     , SUM(A.TKINT) AS TKINT " + "\n");
                strSqlString.Append("     , SUM(A.EOHA) AS EOHA " + "\n");
                strSqlString.Append("     , SUM(A.EOHT) AS EOHT " + "\n");
                strSqlString.Append("     , SUM(A.SHIP_H3) AS SHIP_H3 " + "\n");
                strSqlString.Append("     , SUM(A.SHIP_H4) AS SHIP_H4 " + "\n");
                strSqlString.Append("     , SUM(A.TKOUTA) AS TKOUTA " + "\n");
                strSqlString.Append("     , SUM(A.TKOUTT) AS TKOUTT " + "\n");
                strSqlString.Append("     , SUM(A.REJECTA) AS LOSSA " + "\n");
                strSqlString.Append("     , SUM(A.REJECTT) AS LOSST " + "\n");
                strSqlString.Append("     , SUM(A.BONUSA) AS BONUSA " + "\n");
                strSqlString.Append("     , SUM(A.BONUST) AS BONUST " + "\n");
                strSqlString.Append("     , SUM(A.RES_HA_H2) AS RES_HA_H2 " + "\n");
                strSqlString.Append("     , SUM(A.CHAG_PROD) AS CHAG_PROD " + "\n");
                strSqlString.Append("     , SUM(A.MULT_TRANS) AS MULT_TRANS " + "\n");
                strSqlString.Append("     , SUM(A.RET_H3_H2) AS RET_H3_H2 " + "\n");
                strSqlString.Append("     , SUM(A.RET_H4_H3) AS RET_H4_H3 " + "\n");
                strSqlString.Append("     , SUM(A.RECV_H3) AS RECV_H3 " + "\n");
                strSqlString.Append("     , SUM(A.RET_CO_H3) AS RET_CO_H3 " + "\n");
                strSqlString.Append("     , SUM(A.RET_H3_CO) AS RET_H3_CO " + "\n");
                strSqlString.Append("     , SUM(A.RET_CO_H4) AS RET_CO_H4 " + "\n");
                strSqlString.Append("     , SUM(A.RET_H4) AS RET_H4 " + "\n");
                strSqlString.Append("     , (SUM(A.TKINA) + SUM(A.TKINT) - SUM(A.TKOUTA) - SUM(A.TKOUTT) + SUM(A.BOHA) + SUM(A.BOHT) - SUM(A.EOHA) - SUM(A.EOHT) " + "\n");
                strSqlString.Append("      - SUM(A.REJECTA) - SUM(A.REJECTT)  -SUM(A.RES_HA_H2) - SUM(A.CHAG_PROD) + SUM(A.MULT_TRANS) " + "\n");
                strSqlString.Append("      - SUM(A.RET_H3_H2) + SUM(A.RET_H4_H3) - SUM(A.SHIP_H3) + SUM(A.RECV_H3) " + "\n");
                strSqlString.Append("      + SUM(A.BONUSA) + SUM(A.BONUST) - SUM(A.RET_H3_CO) + SUM(A.RET_CO_H3) - SUM(A.SHIP_H4) + SUM(A.RET_CO_H4) - SUM(A.RET_H4)) AS DIFF " + "\n");
                strSqlString.Append("  FROM ( " + "\n");
                /********************************************************************************************************************/
                /* BOHA : ASSY, TEST FACTORY를 이동하는 제품의 BOHA 수량을 조회한다.                                                */
                /*      : ASSY + TEST FACTORY를 이동하는 제품의 BOHA 수량을 조회한다.                                               */
                strSqlString.Append("        SELECT A.MAT_ID AS MAT_ID " + "\n");
                strSqlString.Append("             , CASE WHEN (A.OPER = 'A0040') THEN 'BLAB' " + "\n");
                strSqlString.Append("                    WHEN (A.OPER > 'A0400' AND A.OPER < 'A0401') THEN 'BOND' " + "\n");
                strSqlString.Append("                    WHEN (A.OPER > 'A0401' AND A.OPER <= 'A0609') THEN 'BOND' " + "\n");
                strSqlString.Append("                    WHEN (A.OPER >= 'A0610' AND A.OPER <= 'A1000') THEN 'MOLD' " + "\n");
                strSqlString.Append("                    WHEN (A.OPER >= 'A1001' AND A.OPER <= 'A2300') THEN 'FINI' " + "\n");
                strSqlString.Append("                    WHEN (A.OPER >= 'AZ009' AND A.OPER <= 'T0000') THEN 'HMK3' " + "\n");
                strSqlString.Append("                    WHEN (A.OPER >= 'T0100' AND A.OPER <= 'T1300') THEN 'TEST' " + "\n");
                strSqlString.Append("                    WHEN (A.OPER = 'TZ010') THEN 'HMK4' " + "\n");
                strSqlString.Append("                    ELSE ' ' " + "\n");
                strSqlString.Append("               END AS AREA " + "\n");
                strSqlString.Append("             , SUM(A.QTY_1) AS BOHA " + "\n");
                strSqlString.Append("             , 0 AS BOHT " + "\n");
                strSqlString.Append("             , 0 AS TKINA " + "\n");
                strSqlString.Append("             , 0 AS TKINT " + "\n");
                strSqlString.Append("             , 0 AS REJECTA " + "\n");
                strSqlString.Append("             , 0 AS REJECTT " + "\n");
                strSqlString.Append("             , 0 AS BONUSA " + "\n");
                strSqlString.Append("             , 0 AS BONUST " + "\n");
                strSqlString.Append("             , 0 AS TKOUTA " + "\n");
                strSqlString.Append("             , 0 AS TKOUTT " + "\n");
                strSqlString.Append("             , 0 AS EOHA " + "\n");
                strSqlString.Append("             , 0 AS EOHT " + "\n");
                strSqlString.Append("             , 0 AS RES_HA_H2 " + "\n");
                strSqlString.Append("             , 0 AS CHAG_PROD " + "\n");
                strSqlString.Append("             , 0 AS MULT_TRANS " + "\n");
                strSqlString.Append("             , 0 AS RET_H3_H2 " + "\n");
                strSqlString.Append("             , 0 AS RET_H4_H3 " + "\n");
                strSqlString.Append("             , 0 AS SHIP_H3 " + "\n");
                strSqlString.Append("             , 0 AS RECV_H3 " + "\n");
                strSqlString.Append("             , 0 AS RET_CO_H3 " + "\n");
                strSqlString.Append("             , 0 AS RET_H3_CO " + "\n");
                strSqlString.Append("             , 0 AS SHIP_H4 " + "\n");
                strSqlString.Append("             , 0 AS RET_CO_H4 " + "\n");
                strSqlString.Append("             , 0 AS RET_H4 " + "\n");
                strSqlString.Append("             , 0 AS DIFFER " + "\n");
                strSqlString.Append("          FROM RWIPLOTSTS_BOH A " + "\n");
                strSqlString.Append("         WHERE A.FACTORY IN ('" + GlobalVariable.gsAssyDefaultFactory + "', '" + GlobalVariable.gsTestDefaultFactory + "') " + "\n");
                strSqlString.Append("           AND A.LOT_TYPE = 'W' " + "\n");
                strSqlString.Append("           AND A.OWNER_CODE = 'PROD' " + "\n");
                strSqlString.Append("           AND (TRIM(A.LOT_CMF_7) = 'HM' OR TRIM(A.LOT_CMF_7) = '') " + "\n");
                strSqlString.AppendFormat("           AND A.LOT_CMF_5 LIKE '{0}%' " + "\n", txtLotType.Text.Trim());
                strSqlString.AppendFormat("           AND A.MAT_ID LIKE '{0}%' " + "\n", txtProduct.Text.Trim());
                strSqlString.AppendFormat("           AND A.CUTOFF_DT = '{0}' " + "\n", strFromCutOffDate);
                strSqlString.Append("         GROUP BY A.MAT_ID, A.OPER " + "\n");
                strSqlString.Append("        UNION ALL " + "\n");
                /********************************************************************************************************************/
                /* BOHT : TEST ONLY FACTORY를 이동하는 제품의 BOHT의 수량을 조회한다.                                               */
                strSqlString.Append("        SELECT A.MAT_ID AS MAT_ID " + "\n");
                strSqlString.Append("             , CASE WHEN (A.OPER = 'T0000') THEN 'HMK3' " + "\n");
                strSqlString.Append("                    WHEN (A.OPER >= 'T0100' AND A.OPER <= 'T1300') THEN 'TEST' " + "\n");
                strSqlString.Append("                    WHEN (A.OPER = 'TZ010') THEN 'HMK4' " + "\n");
                strSqlString.Append("                    ELSE ' ' " + "\n");
                strSqlString.Append("               END AS AREA " + "\n");
                strSqlString.Append("             , 0 AS BOHA " + "\n");
                strSqlString.Append("             , SUM(A.QTY_1) AS BOHT " + "\n");
                strSqlString.Append("             , 0 AS TKINA " + "\n");
                strSqlString.Append("             , 0 AS TKINT " + "\n");
                strSqlString.Append("             , 0 AS REJECTA " + "\n");
                strSqlString.Append("             , 0 AS REJECTT " + "\n");
                strSqlString.Append("             , 0 AS BONUSA " + "\n");
                strSqlString.Append("             , 0 AS BONUST " + "\n");
                strSqlString.Append("             , 0 AS TKOUTA " + "\n");
                strSqlString.Append("             , 0 AS TKOUTT " + "\n");
                strSqlString.Append("             , 0 AS EOHA " + "\n");
                strSqlString.Append("             , 0 AS EOHT " + "\n");
                strSqlString.Append("             , 0 AS RES_HA_H2 " + "\n");
                strSqlString.Append("             , 0 AS CHAG_PROD " + "\n");
                strSqlString.Append("             , 0 AS MULT_TRANS " + "\n");
                strSqlString.Append("             , 0 AS RET_H3_H2 " + "\n");
                strSqlString.Append("             , 0 AS RET_H4_H3 " + "\n");
                strSqlString.Append("             , 0 AS SHIP_H3 " + "\n");
                strSqlString.Append("             , 0 AS RECV_H3 " + "\n");
                strSqlString.Append("             , 0 AS RET_CO_H3 " + "\n");
                strSqlString.Append("             , 0 AS RET_H3_CO " + "\n");
                strSqlString.Append("             , 0 AS SHIP_H4 " + "\n");
                strSqlString.Append("             , 0 AS RET_CO_H4 " + "\n");
                strSqlString.Append("             , 0 AS RET_H4 " + "\n");
                strSqlString.Append("             , 0 AS DIFFER " + "\n");
                strSqlString.Append("          FROM RWIPLOTSTS_BOH A " + "\n");
                strSqlString.Append("         WHERE A.FACTORY = '" + GlobalVariable.gsTestDefaultFactory + "' " + "\n");
                strSqlString.Append("           AND A.LOT_TYPE = 'W' " + "\n");
                strSqlString.Append("           AND A.OWNER_CODE = 'PROD' " + "\n");
                // 2009-04-02-정비재 : BOH를 구할 때도 ASSY SITE는 구분하지 않는다.
                //strSqlString.Append("           AND TRIM(A.LOT_CMF_7) <> 'HM' " + "\n");
                strSqlString.AppendFormat("           AND A.LOT_CMF_5 LIKE '{0}%' " + "\n", txtLotType.Text.Trim());
                strSqlString.AppendFormat("           AND A.MAT_ID LIKE '{0}%' " + "\n", txtProduct.Text.Trim());
                strSqlString.AppendFormat("           AND A.CUTOFF_DT = '{0}' " + "\n", strFromCutOffDate);
                strSqlString.Append("         GROUP BY A.MAT_ID, A.OPER " + "\n");
                strSqlString.Append("        UNION ALL " + "\n");
                /********************************************************************************************************************/
                /* TKINA : ASSY, TEST FACTORY를 이동하는 제품을 조회한다.                                                           */
                strSqlString.Append("        SELECT A.OLD_MAT_ID AS MAT_ID " + "\n");
                strSqlString.Append("             , CASE WHEN (A.OLD_OPER = 'A0040') THEN 'BLAB' " + "\n");
                strSqlString.Append("                    WHEN (A.OLD_OPER = B.FLOW_CMF_1) THEN 'BOND' " + "\n");
                strSqlString.Append("                    WHEN (A.OLD_OPER = B.FLOW_CMF_4) THEN 'MOLD' " + "\n");
                strSqlString.Append("                    WHEN (A.OLD_OPER = B.FLOW_CMF_6) THEN 'FINI' " + "\n");
                strSqlString.Append("                    WHEN (A.OLD_OPER = B.FLOW_CMF_8) THEN 'HMK3' " + "\n");
                strSqlString.Append("                    ELSE ' ' " + "\n");
                strSqlString.Append("               END AS AREA " + "\n");
                strSqlString.Append("             , 0 AS BOHA " + "\n");
                strSqlString.Append("             , 0 AS BOHT " + "\n");
                strSqlString.Append("             , CASE WHEN (A.OLD_OPER = 'A0040') THEN SUM(A.OLD_QTY_1) " + "\n");
                strSqlString.Append("                    WHEN (A.OLD_OPER = B.FLOW_CMF_1) THEN SUM(A.QTY_1) " + "\n");
                strSqlString.Append("                    WHEN (A.OLD_OPER = B.FLOW_CMF_4) THEN SUM(A.QTY_1) " + "\n");
                strSqlString.Append("                    WHEN (A.OLD_OPER = B.FLOW_CMF_6) THEN SUM(A.QTY_1) " + "\n");
                strSqlString.Append("                    WHEN (A.OLD_OPER = B.FLOW_CMF_8) THEN SUM(A.QTY_1) " + "\n");
                strSqlString.Append("                    ELSE 0 " + "\n");
                strSqlString.Append("               END AS TKINA " + "\n");
                strSqlString.Append("             , 0 AS TKINT " + "\n");
                strSqlString.Append("             , 0 AS REJECTA " + "\n");
                strSqlString.Append("             , 0 AS REJECTT " + "\n");
                strSqlString.Append("             , 0 AS BONUSA " + "\n");
                strSqlString.Append("             , 0 AS BONUST " + "\n");
                strSqlString.Append("             , 0 AS TKOUTA " + "\n");
                strSqlString.Append("             , 0 AS TKOUTT " + "\n");
                strSqlString.Append("             , 0 AS EOHA " + "\n");
                strSqlString.Append("             , 0 AS EOHT " + "\n");
                strSqlString.Append("             , 0 AS RES_HA_H2 " + "\n");
                strSqlString.Append("             , 0 AS CHAG_PROD " + "\n");
                strSqlString.Append("             , 0 AS MULT_TRANS " + "\n");
                strSqlString.Append("             , 0 AS RET_H3_H2 " + "\n");
                strSqlString.Append("             , 0 AS RET_H4_H3 " + "\n");
                strSqlString.Append("             , 0 AS SHIP_H3 " + "\n");
                strSqlString.Append("             , 0 AS RECV_H3 " + "\n");
                strSqlString.Append("             , 0 AS RET_CO_H3 " + "\n");
                strSqlString.Append("             , 0 AS RET_H3_CO " + "\n");
                strSqlString.Append("             , 0 AS SHIP_H4 " + "\n");
                strSqlString.Append("             , 0 AS RET_CO_H4 " + "\n");
                strSqlString.Append("             , 0 AS RET_H4 " + "\n");
                strSqlString.Append("             , 0 AS DIFFER " + "\n");
                strSqlString.Append("          FROM RWIPLOTHIS A, (SELECT FACTORY, FLOW, FLOW_CMF_1, FLOW_CMF_4, FLOW_CMF_6, FLOW_CMF_8 " + "\n");
                strSqlString.Append("                                FROM MWIPFLWDEF " + "\n");
                strSqlString.Append("                               WHERE FACTORY = '" + GlobalVariable.gsAssyDefaultFactory + "') B " + "\n");
                strSqlString.Append("         WHERE A.OLD_FACTORY = B.FACTORY " + "\n");
                strSqlString.Append("           AND A.OLD_FLOW = B.FLOW " + "\n");
                strSqlString.Append("           AND A.OLD_FACTORY = '" + GlobalVariable.gsAssyDefaultFactory + "' " + "\n");
                strSqlString.Append("           AND A.TRAN_CODE = 'END' " + "\n");
                strSqlString.Append("           AND A.LOT_TYPE = 'W' " + "\n");
                strSqlString.Append("           AND A.OWNER_CODE = 'PROD' " + "\n");
                strSqlString.Append("           AND (TRIM(A.LOT_CMF_7) = 'HM' OR TRIM(A.LOT_CMF_7) = '') " + "\n");
                strSqlString.Append("           AND A.HIST_DEL_FLAG <> 'Y' " + "\n");
                strSqlString.AppendFormat("           AND A.LOT_CMF_5 LIKE '{0}%' " + "\n", txtLotType.Text.Trim());
                strSqlString.AppendFormat("           AND A.OLD_MAT_ID LIKE '{0}%' " + "\n", txtProduct.Text.Trim());
                strSqlString.AppendFormat("           AND A.TRAN_TIME BETWEEN '{0}' AND '{1}' " + "\n", strFromDate, strToDate);
                strSqlString.Append("         GROUP BY A.OLD_MAT_ID, A.OLD_OPER, B.FLOW_CMF_1, B.FLOW_CMF_4, B.FLOW_CMF_6, B.FLOW_CMF_8 " + "\n");
                strSqlString.Append("        UNION ALL " + "\n");
                /********************************************************************************************************************/
                /* TKINA : ASSY, TEST FACTORY를 이동하는 제품의 TEST IN값을 조회한다.                                                */
                strSqlString.Append("        SELECT A.MAT_ID AS MAT_ID " + "\n");
                strSqlString.Append("             , CASE WHEN (A.OLD_OPER = 'T0000') THEN 'TEST' " + "\n");
                strSqlString.Append("                    ELSE ' ' " + "\n");
                strSqlString.Append("               END AS AREA " + "\n");
                strSqlString.Append("             , 0 AS BOHA " + "\n");
                strSqlString.Append("             , 0 AS BOHT " + "\n");
                strSqlString.Append("             , CASE WHEN (A.OLD_OPER = 'T0000') THEN SUM(A.QTY_1) " + "\n");
                strSqlString.Append("                    ELSE 0 " + "\n");
                strSqlString.Append("               END AS TKINA " + "\n");
                strSqlString.Append("             , 0 AS TKINT " + "\n");
                strSqlString.Append("             , 0 AS REJECTA " + "\n");
                strSqlString.Append("             , 0 AS REJECTT " + "\n");
                strSqlString.Append("             , 0 AS BONUSA " + "\n");
                strSqlString.Append("             , 0 AS BONUST " + "\n");
                strSqlString.Append("             , 0 AS TKOUTA " + "\n");
                strSqlString.Append("             , 0 AS TKOUTT " + "\n");
                strSqlString.Append("             , 0 AS EOHA " + "\n");
                strSqlString.Append("             , 0 AS EOHT " + "\n");
                strSqlString.Append("             , 0 AS RES_HA_H2 " + "\n");
                strSqlString.Append("             , 0 AS CHAG_PROD " + "\n");
                strSqlString.Append("             , 0 AS MULT_TRANS " + "\n");
                strSqlString.Append("             , 0 AS RET_H3_H2 " + "\n");
                strSqlString.Append("             , 0 AS RET_H4_H3 " + "\n");
                strSqlString.Append("             , 0 AS SHIP_H3 " + "\n");
                strSqlString.Append("             , 0 AS RECV_H3 " + "\n");
                strSqlString.Append("             , 0 AS RET_CO_H3 " + "\n");
                strSqlString.Append("             , 0 AS RET_H3_CO " + "\n");
                strSqlString.Append("             , 0 AS SHIP_H4 " + "\n");
                strSqlString.Append("             , 0 AS RET_CO_H4 " + "\n");
                strSqlString.Append("             , 0 AS RET_H4 " + "\n");
                strSqlString.Append("             , 0 AS DIFFER " + "\n");
                strSqlString.Append("          FROM RWIPLOTHIS A " + "\n");
                strSqlString.Append("         WHERE A.FACTORY = '" + GlobalVariable.gsTestDefaultFactory + "' " + "\n");
                strSqlString.Append("           AND A.OLD_OPER = 'T0000' " + "\n");
                strSqlString.Append("           AND A.TRAN_CMF_1 = 'ISSUE' " + "\n");
                strSqlString.Append("           AND A.LOT_TYPE = 'W' " + "\n");
                strSqlString.Append("           AND A.OWNER_CODE = 'PROD' " + "\n");
                strSqlString.Append("           AND (TRIM(A.LOT_CMF_7) = 'HM' OR TRIM(A.LOT_CMF_7) = '') " + "\n");
                strSqlString.Append("           AND A.HIST_DEL_FLAG <> 'Y' " + "\n");
                strSqlString.AppendFormat("           AND A.LOT_CMF_5 LIKE '{0}%' " + "\n", txtLotType.Text.Trim());
                strSqlString.AppendFormat("           AND A.MAT_ID LIKE '{0}%' " + "\n", txtProduct.Text.Trim());
                strSqlString.AppendFormat("           AND A.TRAN_TIME BETWEEN '{0}' AND '{1}' " + "\n", strFromDate, strToDate);
                strSqlString.Append("         GROUP BY A.MAT_ID, A.OLD_OPER " + "\n");
                strSqlString.Append("        UNION ALL " + "\n");
                /********************************************************************************************************************/
                /* TKINA : ASSY, TEST FACTORY를 이동하는 제품의 HMK4 IN값을 조회한다.                                                */
                strSqlString.Append("        SELECT A.MAT_ID AS MAT_ID " + "\n");
                strSqlString.Append("             , CASE WHEN (A.OLD_OPER = 'T1300') THEN 'HMK4' " + "\n");
                strSqlString.Append("                    ELSE ' ' " + "\n");
                strSqlString.Append("               END AS AREA " + "\n");
                strSqlString.Append("             , 0 AS BOHA " + "\n");
                strSqlString.Append("             , 0 AS BOHT " + "\n");
                strSqlString.Append("             , CASE WHEN (A.OLD_OPER = 'T1300') THEN SUM(A.QTY_1) " + "\n");
                strSqlString.Append("                    ELSE 0 " + "\n");
                strSqlString.Append("               END AS TKINA " + "\n");
                strSqlString.Append("             , 0 AS TKINT " + "\n");
                strSqlString.Append("             , 0 AS REJECTA " + "\n");
                strSqlString.Append("             , 0 AS REJECTT " + "\n");
                strSqlString.Append("             , 0 AS BONUSA " + "\n");
                strSqlString.Append("             , 0 AS BONUST " + "\n");
                strSqlString.Append("             , 0 AS TKOUTA " + "\n");
                strSqlString.Append("             , 0 AS TKOUTT " + "\n");
                strSqlString.Append("             , 0 AS EOHA " + "\n");
                strSqlString.Append("             , 0 AS EOHT " + "\n");
                strSqlString.Append("             , 0 AS RES_HA_H2 " + "\n");
                strSqlString.Append("             , 0 AS CHAG_PROD " + "\n");
                strSqlString.Append("             , 0 AS MULT_TRANS " + "\n");
                strSqlString.Append("             , 0 AS RET_H3_H2 " + "\n");
                strSqlString.Append("             , 0 AS RET_H4_H3 " + "\n");
                strSqlString.Append("             , 0 AS SHIP_H3 " + "\n");
                strSqlString.Append("             , 0 AS RECV_H3 " + "\n");
                strSqlString.Append("             , 0 AS RET_CO_H3 " + "\n");
                strSqlString.Append("             , 0 AS RET_H3_CO " + "\n");
                strSqlString.Append("             , 0 AS SHIP_H4 " + "\n");
                strSqlString.Append("             , 0 AS RET_CO_H4 " + "\n");
                strSqlString.Append("             , 0 AS RET_H4 " + "\n");
                strSqlString.Append("             , 0 AS DIFFER " + "\n");
                strSqlString.Append("          FROM RWIPLOTHIS A " + "\n");
                strSqlString.Append("         WHERE A.FACTORY = '" + GlobalVariable.gsTestDefaultFactory + "' " + "\n");
                strSqlString.Append("           AND A.OLD_OPER = 'T1300' " + "\n");
                strSqlString.Append("           AND A.TRAN_CODE = 'END' " + "\n");
                strSqlString.Append("           AND A.LOT_TYPE = 'W' " + "\n");
                strSqlString.Append("           AND A.OWNER_CODE = 'PROD' " + "\n");
                strSqlString.Append("           AND (TRIM(A.LOT_CMF_7) = 'HM' OR TRIM(A.LOT_CMF_7) = '') " + "\n");
                strSqlString.Append("           AND A.HIST_DEL_FLAG <> 'Y' " + "\n");
                strSqlString.AppendFormat("           AND A.LOT_CMF_5 LIKE '{0}%' " + "\n", txtLotType.Text.Trim());
                strSqlString.AppendFormat("           AND A.MAT_ID LIKE '{0}%' " + "\n", txtProduct.Text.Trim());
                strSqlString.AppendFormat("           AND A.TRAN_TIME BETWEEN '{0}' AND '{1}' " + "\n", strFromDate, strToDate);
                strSqlString.Append("         GROUP BY A.MAT_ID, A.OLD_OPER " + "\n");
                strSqlString.Append("        UNION ALL " + "\n");
                /********************************************************************************************************************/
                /* TKINT : TEST ONLY FACTORY를 이동하는 제품을 조회한다.                                                            */
                strSqlString.Append("        SELECT A.MAT_ID AS MAT_ID " + "\n");
                strSqlString.Append("              , CASE WHEN (A.OLD_OPER = 'T0000') THEN 'TEST' " + "\n");
                strSqlString.Append("                     WHEN (A.OLD_OPER = 'T1300') THEN 'HMK4' " + "\n");
                strSqlString.Append("                     ELSE ' ' " + "\n");
                strSqlString.Append("                END AS AREA " + "\n");
                strSqlString.Append("              , 0 AS BOHA " + "\n");
                strSqlString.Append("              , 0 AS BOHT " + "\n");
                strSqlString.Append("              , 0 AS TKINA " + "\n");
                strSqlString.Append("              , CASE WHEN (A.OLD_OPER = 'T0000') THEN SUM(A.QTY_1) " + "\n");
                strSqlString.Append("                     WHEN (A.OLD_OPER = 'T1300') THEN SUM(A.QTY_1) " + "\n");
                strSqlString.Append("                     ELSE 0 " + "\n");
                strSqlString.Append("                END AS TKINT " + "\n");
                strSqlString.Append("              , 0 AS REJECTA " + "\n");
                strSqlString.Append("              , 0 AS REJECTT " + "\n");
                strSqlString.Append("              , 0 AS BONUSA " + "\n");
                strSqlString.Append("              , 0 AS BONUST " + "\n");
                strSqlString.Append("              , 0 AS TKOUTA " + "\n");
                strSqlString.Append("              , 0 AS TKOUTT " + "\n");
                strSqlString.Append("              , 0 AS EOHA " + "\n");
                strSqlString.Append("              , 0 AS EOHT " + "\n");
                strSqlString.Append("              , 0 AS RES_HA_H2 " + "\n");
                strSqlString.Append("              , 0 AS CHAG_PROD " + "\n");
                strSqlString.Append("              , 0 AS MULT_TRANS " + "\n");
                strSqlString.Append("              , 0 AS RET_H3_H2 " + "\n");
                strSqlString.Append("              , 0 AS RET_H4_H3 " + "\n");
                strSqlString.Append("              , 0 AS SHIP_H3 " + "\n");
                strSqlString.Append("              , 0 AS RECV_H3 " + "\n");
                strSqlString.Append("              , 0 AS RET_CO_H3 " + "\n");
                strSqlString.Append("              , 0 AS RET_H3_CO " + "\n");
                strSqlString.Append("              , 0 AS SHIP_H4 " + "\n");
                strSqlString.Append("              , 0 AS RET_CO_H4 " + "\n");
                strSqlString.Append("              , 0 AS RET_H4 " + "\n");
                strSqlString.Append("              , 0 AS DIFFER " + "\n");
                strSqlString.Append("           FROM RWIPLOTHIS A " + "\n");
                strSqlString.Append("          WHERE A.FACTORY = '" + GlobalVariable.gsTestDefaultFactory + "' " + "\n");
                strSqlString.Append("            AND A.OLD_OPER IN ('T0000', 'T1300') " + "\n");
                strSqlString.Append("            AND A.TRAN_CODE = 'END' " + "\n");
                strSqlString.Append("            AND A.LOT_TYPE = 'W' " + "\n");
                strSqlString.Append("            AND A.OWNER_CODE = 'PROD' " + "\n");
                strSqlString.Append("            AND A.HIST_DEL_FLAG <> 'Y' " + "\n");
                strSqlString.Append("            AND TRIM(A.LOT_CMF_7) <> 'HM' " + "\n");
                strSqlString.AppendFormat("           AND A.LOT_CMF_5 LIKE '{0}%' " + "\n", txtLotType.Text.Trim());
                strSqlString.AppendFormat("           AND A.MAT_ID LIKE '{0}%' " + "\n", txtProduct.Text.Trim());
                strSqlString.AppendFormat("           AND A.TRAN_TIME BETWEEN '{0}' AND '{1}' " + "\n", strFromDate, strToDate);
                strSqlString.Append("          GROUP BY A.MAT_ID, A.OLD_OPER " + "\n");
                strSqlString.Append("         UNION ALL " + "\n");
                /********************************************************************************************************************/
                /* REJECTA : ASSY, TEST FACTORY를 이동하는 제품의 REJECT수량을 조회한다.                                            */
                strSqlString.Append("        SELECT A.MAT_ID AS MAT_ID " + "\n");
                strSqlString.Append("             , CASE WHEN (A.OPER >= 'A0020' AND A.OPER <= 'A0040') THEN 'BLAB' " + "\n");
                strSqlString.Append("                    WHEN (A.OPER >= 'A0200' AND A.OPER <= 'A0609') THEN 'BOND' " + "\n");
                strSqlString.Append("                    WHEN (A.OPER >= 'A0610' AND A.OPER <= 'A1000') THEN 'MOLD' " + "\n");
                strSqlString.Append("                    WHEN (A.OPER >= 'A1001' AND A.OPER <= 'A2300') THEN 'FINI' " + "\n");
                strSqlString.Append("                    WHEN (A.OPER >= 'AZ009' AND A.OPER <= 'AZ010') THEN 'HMK3' " + "\n");
                strSqlString.Append("                    WHEN (A.OPER  = 'T0000') THEN 'HMK3' " + "\n");
                strSqlString.Append("                    WHEN (A.OPER >= 'T0100' AND A.OPER <= 'T1300') THEN 'TEST' " + "\n");
                strSqlString.Append("                    WHEN (A.OPER  = 'TZ010') THEN 'HMK4' " + "\n");
                strSqlString.Append("                    ELSE ' ' " + "\n");
                strSqlString.Append("               END AS AREA " + "\n");
                strSqlString.Append("             , 0 AS BOHA " + "\n");
                strSqlString.Append("             , 0 AS BOHT " + "\n");
                strSqlString.Append("             , 0 AS TKINA " + "\n");
                strSqlString.Append("             , 0 AS TKINT " + "\n");
                strSqlString.Append("             , SUM(A.REJECTA) AS REJECTA " + "\n");
                strSqlString.Append("             , 0 AS REJECTT " + "\n");
                strSqlString.Append("             , 0 AS BONUSA " + "\n");
                strSqlString.Append("             , 0 AS BONUST " + "\n");
                strSqlString.Append("             , 0 AS TKOUTA " + "\n");
                strSqlString.Append("             , 0 AS TKOUTT " + "\n");
                strSqlString.Append("             , 0 AS EOHA " + "\n");
                strSqlString.Append("             , 0 AS EOHT " + "\n");
                strSqlString.Append("             , 0 AS RES_HA_H2 " + "\n");
                strSqlString.Append("             , 0 AS CHAG_PROD " + "\n");
                strSqlString.Append("             , 0 AS MULT_TRANS " + "\n");
                strSqlString.Append("             , 0 AS RET_H3_H2 " + "\n");
                strSqlString.Append("             , 0 AS RET_H4_H3 " + "\n");
                strSqlString.Append("             , 0 AS SHIP_H3 " + "\n");
                strSqlString.Append("             , 0 AS RECV_H3 " + "\n");
                strSqlString.Append("             , 0 AS RET_CO_H3 " + "\n");
                strSqlString.Append("             , 0 AS RET_H3_CO " + "\n");
                strSqlString.Append("             , 0 AS SHIP_H4 " + "\n");
                strSqlString.Append("             , 0 AS RET_CO_H4 " + "\n");
                strSqlString.Append("             , 0 AS RET_H4 " + "\n");
                strSqlString.Append("             , 0 AS DIFFER " + "\n");
                strSqlString.Append("          FROM ( " + "\n");
                strSqlString.Append("                SELECT A.MAT_ID AS MAT_ID " + "\n");
                strSqlString.Append("                     , A.OPER AS OPER " + "\n");
                strSqlString.Append("                     , SUM(A.OLD_QTY_1 - A.QTY_1) AS REJECTA " + "\n");
                strSqlString.Append("                  FROM RWIPLOTHIS A " + "\n");
                strSqlString.Append("                 WHERE A.FACTORY IN ('" + GlobalVariable.gsAssyDefaultFactory + "', '" + GlobalVariable.gsTestDefaultFactory + "') " + "\n");
                strSqlString.Append("                   AND A.TRAN_CODE = 'LOSS' " + "\n");
                strSqlString.Append("                   AND A.LOT_TYPE = 'W' " + "\n");
                strSqlString.Append("                   AND A.OWNER_CODE = 'PROD' " + "\n");
                strSqlString.Append("                   AND A.HIST_DEL_FLAG <> 'Y' " + "\n");
                strSqlString.Append("                   AND (TRIM(A.LOT_CMF_7) = 'HM' OR TRIM(A.LOT_CMF_7) = '') " + "\n");
                strSqlString.AppendFormat("                   AND A.LOT_CMF_5 LIKE '{0}%' " + "\n", txtLotType.Text.Trim());
                strSqlString.AppendFormat("                   AND A.MAT_ID LIKE '{0}%' " + "\n", txtProduct.Text.Trim());
                strSqlString.AppendFormat("                   AND A.TRAN_TIME BETWEEN '{0}' AND '{1}' " + "\n", strFromDate, strToDate);
                strSqlString.Append("                GROUP BY A.MAT_ID, A.OPER " + "\n");
                strSqlString.Append("               UNION ALL " + "\n");
                strSqlString.Append("                SELECT A.MAT_ID AS MAT_ID " + "\n");
                strSqlString.Append("                     , A.OPER AS OPER " + "\n");
                strSqlString.Append("                     , SUM(A.OLD_QTY_1) AS REJECTA " + "\n");
                strSqlString.Append("                  FROM RWIPLOTHIS A " + "\n");
                strSqlString.Append("                 WHERE A.FACTORY = A.OLD_FACTORY " + "\n");
                strSqlString.Append("                   AND A.OPER = A.OLD_OPER " + "\n");
                strSqlString.Append("                   AND A.FACTORY IN ('" + GlobalVariable.gsAssyDefaultFactory + "', '" + GlobalVariable.gsTestDefaultFactory + "') " + "\n");
                strSqlString.Append("                   AND A.TRAN_CODE = 'TERMINATE' " + "\n");
                strSqlString.Append("                   AND A.LOT_TYPE = 'W' " + "\n");
                strSqlString.Append("                   AND A.OWNER_CODE = 'PROD' " + "\n");
                strSqlString.Append("                   AND A.HIST_DEL_FLAG <> 'Y' " + "\n");
                strSqlString.Append("                   AND (TRIM(A.LOT_CMF_7) = 'HM' OR TRIM(A.LOT_CMF_7) = '') " + "\n");
                strSqlString.AppendFormat("                    AND A.LOT_CMF_5 LIKE '{0}%' " + "\n", txtLotType.Text.Trim());
                strSqlString.AppendFormat("                    AND A.MAT_ID LIKE '{0}%' " + "\n", txtProduct.Text.Trim());
                strSqlString.AppendFormat("                    AND A.TRAN_TIME BETWEEN '{0}' AND '{1}' " + "\n", strFromDate, strToDate);
                strSqlString.Append("                 GROUP BY A.MAT_ID, A.OPER " + "\n");
                strSqlString.Append("                ) A " + "\n");
                strSqlString.Append("         GROUP BY A.MAT_ID, A.OPER " + "\n");
                strSqlString.Append("        UNION ALL " + "\n");
                /********************************************************************************************************************/
                /* REJECTT : TEST ONLY FACTORY를 이동하는 제품의 REJECT수량을 조회한다.                                             */
                strSqlString.Append("        SELECT A.MAT_ID AS MAT_ID " + "\n");
                strSqlString.Append("             , CASE WHEN (A.OPER = 'T0000') THEN 'HMK3' " + "\n");
                strSqlString.Append("                    WHEN (A.OPER >= 'T0100' AND A.OPER <= 'T1300') THEN 'TEST' " + "\n");
                strSqlString.Append("                    WHEN (A.OPER = 'TZ010') THEN 'HMK4' " + "\n");
                strSqlString.Append("                    ELSE ' ' " + "\n");
                strSqlString.Append("               END AS AREA " + "\n");
                strSqlString.Append("             , 0 AS BOHA " + "\n");
                strSqlString.Append("             , 0 AS BOHT " + "\n");
                strSqlString.Append("             , 0 AS TKINA " + "\n");
                strSqlString.Append("             , 0 AS TKINT " + "\n");
                strSqlString.Append("             , 0 AS REJECTA " + "\n");
                strSqlString.Append("             , SUM(A.REJECTT) AS REJECTT " + "\n");
                strSqlString.Append("             , 0 AS BONUSA " + "\n");
                strSqlString.Append("             , 0 AS BONUST " + "\n");
                strSqlString.Append("             , 0 AS TKOUTA " + "\n");
                strSqlString.Append("             , 0 AS TKOUTT " + "\n");
                strSqlString.Append("             , 0 AS EOHA " + "\n");
                strSqlString.Append("             , 0 AS EOHT " + "\n");
                strSqlString.Append("             , 0 AS RES_HA_H2 " + "\n");
                strSqlString.Append("             , 0 AS CHAG_PROD " + "\n");
                strSqlString.Append("             , 0 AS MULT_TRANS " + "\n");
                strSqlString.Append("             , 0 AS RET_H3_H2 " + "\n");
                strSqlString.Append("             , 0 AS RET_H4_H3 " + "\n");
                strSqlString.Append("             , 0 AS SHIP_H3 " + "\n");
                strSqlString.Append("             , 0 AS RECV_H3 " + "\n");
                strSqlString.Append("             , 0 AS RET_CO_H3 " + "\n");
                strSqlString.Append("             , 0 AS RET_H3_CO " + "\n");
                strSqlString.Append("             , 0 AS SHIP_H4 " + "\n");
                strSqlString.Append("             , 0 AS RET_CO_H4 " + "\n");
                strSqlString.Append("             , 0 AS RET_H4 " + "\n");
                strSqlString.Append("             , 0 AS DIFFER " + "\n");
                strSqlString.Append("          FROM ( " + "\n");
                strSqlString.Append("                SELECT A.MAT_ID AS MAT_ID " + "\n");
                strSqlString.Append("                     , A.OPER AS OPER " + "\n");
                strSqlString.Append("                     , SUM(A.OLD_QTY_1 - A.QTY_1) AS REJECTT " + "\n");
                strSqlString.Append("                  FROM RWIPLOTHIS A " + "\n");
                strSqlString.Append("                 WHERE A.FACTORY = '" + GlobalVariable.gsTestDefaultFactory + "' " + "\n");
                strSqlString.Append("                   AND A.TRAN_CODE = 'LOSS' " + "\n");
                strSqlString.Append("                   AND A.LOT_TYPE = 'W' " + "\n");
                strSqlString.Append("                   AND A.OWNER_CODE = 'PROD' " + "\n");
                strSqlString.Append("                   AND A.HIST_DEL_FLAG <> 'Y' " + "\n");
                strSqlString.Append("                   AND TRIM(A.LOT_CMF_7) <> 'HM' " + "\n");
                strSqlString.AppendFormat("                   AND A.LOT_CMF_5 LIKE '{0}%' " + "\n", txtLotType.Text.Trim());
                strSqlString.AppendFormat("                   AND A.MAT_ID LIKE '{0}%' " + "\n", txtProduct.Text.Trim());
                strSqlString.AppendFormat("                   AND A.TRAN_TIME BETWEEN '{0}' AND '{1}' " + "\n", strFromDate, strToDate);
                strSqlString.Append("                GROUP BY A.MAT_ID, A.OPER " + "\n");
                strSqlString.Append("               UNION ALL " + "\n");
                strSqlString.Append("                SELECT A.MAT_ID AS MAT_ID " + "\n");
                strSqlString.Append("                     , A.OPER AS OPER " + "\n");
                strSqlString.Append("                     , SUM(A.OLD_QTY_1) AS REJECTT " + "\n");
                strSqlString.Append("                  FROM RWIPLOTHIS A " + "\n");
                strSqlString.Append("                 WHERE A.FACTORY = A.OLD_FACTORY " + "\n");
                strSqlString.Append("                   AND A.OPER = A.OLD_OPER " + "\n");
                strSqlString.Append("                   AND A.FACTORY = '" + GlobalVariable.gsTestDefaultFactory + "' " + "\n");
                strSqlString.Append("                   AND A.TRAN_CODE = 'TERMINATE' " + "\n");
                strSqlString.Append("                   AND A.LOT_TYPE = 'W' " + "\n");
                strSqlString.Append("                   AND A.OWNER_CODE = 'PROD' " + "\n");
                strSqlString.Append("                   AND A.HIST_DEL_FLAG <> 'Y' " + "\n");
                strSqlString.Append("                   AND TRIM(A.LOT_CMF_7) <> 'HM' " + "\n");
                strSqlString.AppendFormat("                    AND A.LOT_CMF_5 LIKE '{0}%' " + "\n", txtLotType.Text.Trim());
                strSqlString.AppendFormat("                    AND A.MAT_ID LIKE '{0}%' " + "\n", txtProduct.Text.Trim());
                strSqlString.AppendFormat("                    AND A.TRAN_TIME BETWEEN '{0}' AND '{1}' " + "\n", strFromDate, strToDate);
                strSqlString.Append("                 GROUP BY A.MAT_ID, A.OPER " + "\n");
                strSqlString.Append("               ) A " + "\n");
                strSqlString.Append("         GROUP BY A.MAT_ID, A.OPER " + "\n");
                strSqlString.Append("        UNION ALL " + "\n");
                /********************************************************************************************************************/
                /* BONUSA : ASSY, TEST FACTORY를 이동하는 제품의 BONUS수량을 조회한다.                                              */
                strSqlString.Append("        SELECT A.MAT_ID AS MAT_ID " + "\n");
                strSqlString.Append("             , CASE WHEN (A.OPER >= 'A0020' AND A.OPER <= 'A0040') THEN 'BLAB' " + "\n");
                strSqlString.Append("                    WHEN (A.OPER >= 'A0200' AND A.OPER <= 'A0609') THEN 'BOND' " + "\n");
                strSqlString.Append("                    WHEN (A.OPER >= 'A0610' AND A.OPER <= 'A1000') THEN 'MOLD' " + "\n");
                strSqlString.Append("                    WHEN (A.OPER >= 'A1001' AND A.OPER <= 'A2300') THEN 'FINI' " + "\n");
                strSqlString.Append("                    WHEN (A.OPER >= 'AZ009' AND A.OPER <= 'T0000') THEN 'HMK3' " + "\n");
                strSqlString.Append("                    WHEN (A.OPER >= 'T0100' AND A.OPER <= 'T1300') THEN 'TEST' " + "\n");
                strSqlString.Append("                    WHEN (A.OPER = 'TZ010') THEN 'HMK4' " + "\n");
                strSqlString.Append("                    ELSE ' ' " + "\n");
                strSqlString.Append("               END AS AREA " + "\n");
                strSqlString.Append("             , 0 AS BOHA " + "\n");
                strSqlString.Append("             , 0 AS BOHT " + "\n");
                strSqlString.Append("             , 0 AS TKINA " + "\n");
                strSqlString.Append("             , 0 AS TKINT " + "\n");
                strSqlString.Append("             , 0 AS REJECTA " + "\n");
                strSqlString.Append("             , 0 AS REJECTT " + "\n");
                strSqlString.Append("             , SUM(A.TOTAL_BONUS_QTY) AS BONUSA " + "\n");
                strSqlString.Append("             , 0 AS BONUST " + "\n");
                strSqlString.Append("             , 0 AS TKOUTA " + "\n");
                strSqlString.Append("             , 0 AS TKOUTT " + "\n");
                strSqlString.Append("             , 0 AS EOHA " + "\n");
                strSqlString.Append("             , 0 AS EOHT " + "\n");
                strSqlString.Append("             , 0 AS RES_HA_H2 " + "\n");
                strSqlString.Append("             , 0 AS CHAG_PROD " + "\n");
                strSqlString.Append("             , 0 AS MULT_TRANS " + "\n");
                strSqlString.Append("             , 0 AS RET_H3_H2 " + "\n");
                strSqlString.Append("             , 0 AS RET_H4_H3 " + "\n");
                strSqlString.Append("             , 0 AS SHIP_H3 " + "\n");
                strSqlString.Append("             , 0 AS RECV_H3 " + "\n");
                strSqlString.Append("             , 0 AS RET_CO_H3 " + "\n");
                strSqlString.Append("             , 0 AS RET_H3_CO " + "\n");
                strSqlString.Append("             , 0 AS SHIP_H4 " + "\n");
                strSqlString.Append("             , 0 AS RET_CO_H4 " + "\n");
                strSqlString.Append("             , 0 AS RET_H4 " + "\n");
                strSqlString.Append("             , 0 AS DIFFER " + "\n");
                strSqlString.Append("          FROM RWIPLOTBNS A, RWIPLOTSTS B " + "\n");
                strSqlString.Append("         WHERE A.FACTORY = B.FACTORY " + "\n");
                strSqlString.Append("           AND A.MAT_ID = B.MAT_ID " + "\n");
                strSqlString.Append("           AND A.LOT_ID = B.LOT_ID " + "\n");
                strSqlString.Append("           AND A.FACTORY = '" + GlobalVariable.gsAssyDefaultFactory + "' " + "\n");
                strSqlString.AppendFormat("           AND B.LOT_CMF_5 LIKE '{0}%' " + "\n", txtLotType.Text.Trim());
                strSqlString.AppendFormat("           AND A.MAT_ID LIKE '{0}%' " + "\n", txtProduct.Text.Trim());
                strSqlString.AppendFormat("           AND A.TRAN_TIME BETWEEN '{0}' AND '{1}' " + "\n", strFromDate, strToDate);
                strSqlString.Append("         GROUP BY A.MAT_ID, A.OPER " + "\n");
                strSqlString.Append("        UNION ALL " + "\n");
                /********************************************************************************************************************/
                /* BONUST : TEST ONLY FACTORY를 이동하는 제품의 BONUS수량을 조회한다.                                               */
                strSqlString.Append("        SELECT A.MAT_ID AS MAT_ID " + "\n");
                strSqlString.Append("             , CASE WHEN (A.OPER = 'T0000') THEN 'HMK3' " + "\n");
                strSqlString.Append("                    WHEN (A.OPER >= 'T0100' AND A.OPER <= 'T1300') THEN 'TEST' " + "\n");
                strSqlString.Append("                    WHEN (A.OPER = 'TZ010') THEN 'HMK4' " + "\n");
                strSqlString.Append("                    ELSE ' ' " + "\n");
                strSqlString.Append("               END AS AREA " + "\n");
                strSqlString.Append("             , 0 AS BOHA " + "\n");
                strSqlString.Append("             , 0 AS BOHT " + "\n");
                strSqlString.Append("             , 0 AS TKINA " + "\n");
                strSqlString.Append("             , 0 AS TKINT " + "\n");
                strSqlString.Append("             , 0 AS REJECTA " + "\n");
                strSqlString.Append("             , 0 AS REJECTT " + "\n");
                strSqlString.Append("             , 0 AS BONUSA " + "\n");
                strSqlString.Append("             , SUM(A.TOTAL_BONUS_QTY) AS BONUST " + "\n");
                strSqlString.Append("             , 0 AS TKOUTA " + "\n");
                strSqlString.Append("             , 0 AS TKOUTT " + "\n");
                strSqlString.Append("             , 0 AS EOHA " + "\n");
                strSqlString.Append("             , 0 AS EOHT " + "\n");
                strSqlString.Append("             , 0 AS RES_HA_H2 " + "\n");
                strSqlString.Append("             , 0 AS CHAG_PROD " + "\n");
                strSqlString.Append("             , 0 AS MULT_TRANS " + "\n");
                strSqlString.Append("             , 0 AS RET_H3_H2 " + "\n");
                strSqlString.Append("             , 0 AS RET_H4_H3 " + "\n");
                strSqlString.Append("             , 0 AS SHIP_H3 " + "\n");
                strSqlString.Append("             , 0 AS RECV_H3 " + "\n");
                strSqlString.Append("             , 0 AS RET_CO_H3 " + "\n");
                strSqlString.Append("             , 0 AS RET_H3_CO " + "\n");
                strSqlString.Append("             , 0 AS SHIP_H4 " + "\n");
                strSqlString.Append("             , 0 AS RET_CO_H4 " + "\n");
                strSqlString.Append("             , 0 AS RET_H4 " + "\n");
                strSqlString.Append("             , 0 AS DIFFER " + "\n");
                strSqlString.Append("          FROM RWIPLOTBNS A, RWIPLOTSTS B " + "\n");
                strSqlString.Append("         WHERE A.FACTORY = B.FACTORY " + "\n");
                strSqlString.Append("           AND A.MAT_ID = B.MAT_ID " + "\n");
                strSqlString.Append("           AND A.LOT_ID = B.LOT_ID " + "\n");
                strSqlString.Append("           AND A.FACTORY = '" + GlobalVariable.gsTestDefaultFactory + "' " + "\n");
                strSqlString.AppendFormat("           AND B.LOT_CMF_5 LIKE '{0}%' " + "\n", txtLotType.Text.Trim());
                strSqlString.AppendFormat("           AND A.MAT_ID LIKE '{0}%' " + "\n", txtProduct.Text.Trim());
                strSqlString.AppendFormat("           AND A.TRAN_TIME BETWEEN '{0}' AND '{1}' " + "\n", strFromDate, strToDate);
                strSqlString.Append("         GROUP BY A.MAT_ID, A.OPER " + "\n");
                strSqlString.Append("        UNION ALL " + "\n");
                /********************************************************************************************************************/
                /* TKOUTA : ASSY, TEST FACTORY를 이동하는 제품의 TKOUTA수량을 조회한다.                                             */
                strSqlString.Append("        SELECT A.MAT_ID AS MAT_ID " + "\n");
                strSqlString.Append("             , CASE WHEN (A.OLD_OPER = 'A0040') THEN 'BLAB' " + "\n");
                strSqlString.Append("                    WHEN (A.OLD_OPER = B.FLOW_CMF_4) THEN 'BOND' " + "\n");
                strSqlString.Append("                    WHEN (A.OLD_OPER = B.FLOW_CMF_6) THEN 'MOLD' " + "\n");
                strSqlString.Append("                    WHEN (A.OLD_OPER = B.FLOW_CMF_8) THEN 'FINI' " + "\n");
                strSqlString.Append("                    ELSE ' ' " + "\n");
                strSqlString.Append("               END AS AREA " + "\n");
                strSqlString.Append("             , 0 AS BOHA " + "\n");
                strSqlString.Append("             , 0 AS BOHT " + "\n");
                strSqlString.Append("             , 0 AS TKINA " + "\n");
                strSqlString.Append("             , 0 AS TKINT " + "\n");
                strSqlString.Append("             , 0 AS REJECTA " + "\n");
                strSqlString.Append("             , 0 AS REJECTT " + "\n");
                strSqlString.Append("             , 0 AS BONUSA " + "\n");
                strSqlString.Append("             , 0 AS BONUST " + "\n");
                strSqlString.Append("             , CASE WHEN (A.OLD_OPER = 'A0040') THEN SUM(A.QTY_1) " + "\n");
                strSqlString.Append("                    WHEN (A.OLD_OPER = B.FLOW_CMF_4) THEN SUM(A.QTY_1) " + "\n");
                strSqlString.Append("                    WHEN (A.OLD_OPER = B.FLOW_CMF_6) THEN SUM(A.QTY_1) " + "\n");
                strSqlString.Append("                    WHEN (A.OLD_OPER = B.FLOW_CMF_8) THEN SUM(A.QTY_1) " + "\n");
                strSqlString.Append("                    ELSE 0 " + "\n");
                strSqlString.Append("               END AS TKOUTA " + "\n");
                strSqlString.Append("             , 0 AS TKOUTT " + "\n");
                strSqlString.Append("             , 0 AS EOHA " + "\n");
                strSqlString.Append("             , 0 AS EOHT " + "\n");
                strSqlString.Append("             , 0 AS RES_HA_H2 " + "\n");
                strSqlString.Append("             , 0 AS CHAG_PROD " + "\n");
                strSqlString.Append("             , 0 AS MULT_TRANS " + "\n");
                strSqlString.Append("             , 0 AS RET_H3_H2 " + "\n");
                strSqlString.Append("             , 0 AS RET_H4_H3 " + "\n");
                strSqlString.Append("             , 0 AS SHIP_H3 " + "\n");
                strSqlString.Append("             , 0 AS RECV_H3 " + "\n");
                strSqlString.Append("             , 0 AS RET_CO_H3 " + "\n");
                strSqlString.Append("             , 0 AS RET_H3_CO " + "\n");
                strSqlString.Append("             , 0 AS SHIP_H4 " + "\n");
                strSqlString.Append("             , 0 AS RET_CO_H4 " + "\n");
                strSqlString.Append("             , 0 AS RET_H4 " + "\n");
                strSqlString.Append("             , 0 AS DIFFER " + "\n");
                strSqlString.Append("          FROM RWIPLOTHIS A, (SELECT FACTORY, FLOW, FLOW_CMF_4, FLOW_CMF_6, FLOW_CMF_8 " + "\n");
                strSqlString.Append("                                FROM MWIPFLWDEF " + "\n");
                strSqlString.Append("                               WHERE FACTORY = '" + GlobalVariable.gsAssyDefaultFactory + "') B " + "\n");
                strSqlString.Append("         WHERE A.FACTORY = B.FACTORY " + "\n");
                strSqlString.Append("           AND A.FLOW = B.FLOW " + "\n");
                strSqlString.Append("           AND A.FACTORY = '" + GlobalVariable.gsAssyDefaultFactory + "' " + "\n");
                strSqlString.Append("           AND A.TRAN_CODE = 'END' " + "\n");
                strSqlString.Append("           AND A.LOT_TYPE = 'W' " + "\n");
                strSqlString.Append("           AND A.OWNER_CODE = 'PROD' " + "\n");
                strSqlString.Append("           AND (TRIM(A.LOT_CMF_7) = 'HM' OR TRIM(A.LOT_CMF_7) = '') " + "\n");
                strSqlString.Append("           AND A.HIST_DEL_FLAG <> 'Y' " + "\n");
                strSqlString.AppendFormat("           AND A.LOT_CMF_5 LIKE '{0}%' " + "\n", txtLotType.Text.Trim());
                strSqlString.AppendFormat("           AND A.MAT_ID LIKE '{0}%' " + "\n", txtProduct.Text.Trim());
                strSqlString.AppendFormat("           AND A.TRAN_TIME BETWEEN '{0}' AND '{1}' " + "\n", strFromDate, strToDate);
                strSqlString.Append("         GROUP BY A.MAT_ID, A.OLD_OPER, B.FLOW_CMF_4, B.FLOW_CMF_6, B.FLOW_CMF_8 " + "\n");
                strSqlString.Append("        UNION ALL " + "\n");
                /********************************************************************************************************************/
                /* TKOUTA : ASSY, TEST FACTORY를 이동하는 제품의 HMK3 TKOUTA수량을 조회한다.                                        */
                strSqlString.Append("        SELECT A.MAT_ID AS MAT_ID " + "\n");
                strSqlString.Append("             , CASE WHEN (A.OLD_OPER = 'T0000') THEN 'HMK3' " + "\n");
                strSqlString.Append("                    ELSE ' ' " + "\n");
                strSqlString.Append("               END AS AREA " + "\n");
                strSqlString.Append("             , 0 AS BOHA " + "\n");
                strSqlString.Append("             , 0 AS BOHT " + "\n");
                strSqlString.Append("             , 0 AS TKINA " + "\n");
                strSqlString.Append("             , 0 AS TKINT " + "\n");
                strSqlString.Append("             , 0 AS REJECTA " + "\n");
                strSqlString.Append("             , 0 AS REJECTT " + "\n");
                strSqlString.Append("             , 0 AS BONUSA " + "\n");
                strSqlString.Append("             , 0 AS BONUST " + "\n");
                strSqlString.Append("             , CASE WHEN (A.OLD_OPER = 'T0000') THEN SUM(A.QTY_1) " + "\n");
                strSqlString.Append("                    ELSE 0 " + "\n");
                strSqlString.Append("               END AS TKOUTA " + "\n");
                strSqlString.Append("             , 0 AS TKOUTT " + "\n");
                strSqlString.Append("             , 0 AS EOHA " + "\n");
                strSqlString.Append("             , 0 AS EOHT " + "\n");
                strSqlString.Append("             , 0 AS RES_HA_H2 " + "\n");
                strSqlString.Append("             , 0 AS CHAG_PROD " + "\n");
                strSqlString.Append("             , 0 AS MULT_TRANS " + "\n");
                strSqlString.Append("             , 0 AS RET_H3_H2 " + "\n");
                strSqlString.Append("             , 0 AS RET_H4_H3 " + "\n");
                strSqlString.Append("             , 0 AS SHIP_H3 " + "\n");
                strSqlString.Append("             , 0 AS RECV_H3 " + "\n");
                strSqlString.Append("             , 0 AS RET_CO_H3 " + "\n");
                strSqlString.Append("             , 0 AS RET_H3_CO " + "\n");
                strSqlString.Append("             , 0 AS SHIP_H4 " + "\n");
                strSqlString.Append("             , 0 AS RET_CO_H4 " + "\n");
                strSqlString.Append("             , 0 AS RET_H4 " + "\n");
                strSqlString.Append("             , 0 AS DIFFER " + "\n");
                strSqlString.Append("          FROM RWIPLOTHIS A " + "\n");
                strSqlString.Append("         WHERE A.FACTORY = '" + GlobalVariable.gsTestDefaultFactory + "' " + "\n");
                strSqlString.Append("           AND A.OLD_OPER = 'T0000' " + "\n");
                strSqlString.Append("           AND A.TRAN_CMF_1 = 'ISSUE' " + "\n");
                strSqlString.Append("           AND A.LOT_TYPE = 'W' " + "\n");
                strSqlString.Append("           AND A.OWNER_CODE = 'PROD' " + "\n");
                strSqlString.Append("           AND (TRIM(A.LOT_CMF_7) = 'HM' OR TRIM(A.LOT_CMF_7) = '') " + "\n");
                strSqlString.Append("           AND A.HIST_DEL_FLAG <> 'Y' " + "\n");
                strSqlString.AppendFormat("           AND A.LOT_CMF_5 LIKE '{0}%' " + "\n", txtLotType.Text.Trim());
                strSqlString.AppendFormat("           AND A.MAT_ID LIKE '{0}%' " + "\n", txtProduct.Text.Trim());
                strSqlString.AppendFormat("           AND A.TRAN_TIME BETWEEN '{0}' AND '{1}' " + "\n", strFromDate, strToDate);
                strSqlString.Append("         GROUP BY A.MAT_ID, A.OLD_OPER " + "\n");
                strSqlString.Append("        UNION ALL " + "\n");
                /********************************************************************************************************************/
                /* TKOUTA : ASSY, TEST FACTORY를 이동하는 제품의 TEST TKOUTA수량을 조회한다.                                        */
                strSqlString.Append("        SELECT A.MAT_ID AS MAT_ID " + "\n");
                strSqlString.Append("             , CASE WHEN (A.OLD_OPER = 'T1300') THEN 'TEST' " + "\n");
                strSqlString.Append("                    ELSE ' ' " + "\n");
                strSqlString.Append("               END AS AREA " + "\n");
                strSqlString.Append("             , 0 AS BOHA " + "\n");
                strSqlString.Append("             , 0 AS BOHT " + "\n");
                strSqlString.Append("             , 0 AS TKINA " + "\n");
                strSqlString.Append("             , 0 AS TKINT " + "\n");
                strSqlString.Append("             , 0 AS REJECTA " + "\n");
                strSqlString.Append("             , 0 AS REJECTT " + "\n");
                strSqlString.Append("             , 0 AS BONUSA " + "\n");
                strSqlString.Append("             , 0 AS BONUST " + "\n");
                strSqlString.Append("             , CASE WHEN (A.OLD_OPER = 'T1300') THEN SUM(A.QTY_1) " + "\n");
                strSqlString.Append("                    ELSE 0 " + "\n");
                strSqlString.Append("               END AS TKOUTA " + "\n");
                strSqlString.Append("             , 0 AS TKOUTT " + "\n");
                strSqlString.Append("             , 0 AS EOHA " + "\n");
                strSqlString.Append("             , 0 AS EOHT " + "\n");
                strSqlString.Append("             , 0 AS RES_HA_H2 " + "\n");
                strSqlString.Append("             , 0 AS CHAG_PROD " + "\n");
                strSqlString.Append("             , 0 AS MULT_TRANS " + "\n");
                strSqlString.Append("             , 0 AS RET_H3_H2 " + "\n");
                strSqlString.Append("             , 0 AS RET_H4_H3 " + "\n");
                strSqlString.Append("             , 0 AS SHIP_H3 " + "\n");
                strSqlString.Append("             , 0 AS RECV_H3 " + "\n");
                strSqlString.Append("             , 0 AS RET_CO_H3 " + "\n");
                strSqlString.Append("             , 0 AS RET_H3_CO " + "\n");
                strSqlString.Append("             , 0 AS SHIP_H4 " + "\n");
                strSqlString.Append("             , 0 AS RET_CO_H4 " + "\n");
                strSqlString.Append("             , 0 AS RET_H4 " + "\n");
                strSqlString.Append("             , 0 AS DIFFER " + "\n");
                strSqlString.Append("          FROM RWIPLOTHIS A " + "\n");
                strSqlString.Append("         WHERE A.FACTORY = '" + GlobalVariable.gsTestDefaultFactory + "' " + "\n");
                strSqlString.Append("           AND A.OLD_OPER = 'T1300' " + "\n");
                strSqlString.Append("           AND A.TRAN_CODE = 'END' " + "\n");
                strSqlString.Append("           AND A.LOT_TYPE = 'W' " + "\n");
                strSqlString.Append("           AND A.OWNER_CODE = 'PROD' " + "\n");
                strSqlString.Append("           AND (TRIM(A.LOT_CMF_7) = 'HM' OR TRIM(A.LOT_CMF_7) = '') " + "\n");
                strSqlString.Append("           AND A.HIST_DEL_FLAG <> 'Y' " + "\n");
                strSqlString.AppendFormat("           AND A.LOT_CMF_5 LIKE '{0}%' " + "\n", txtLotType.Text.Trim());
                strSqlString.AppendFormat("           AND A.MAT_ID LIKE '{0}%' " + "\n", txtProduct.Text.Trim());
                strSqlString.AppendFormat("           AND A.TRAN_TIME BETWEEN '{0}' AND '{1}' " + "\n", strFromDate, strToDate);
                strSqlString.Append("         GROUP BY A.MAT_ID, A.OLD_OPER " + "\n");
                strSqlString.Append("        UNION ALL " + "\n");
                /********************************************************************************************************************/
                /* TKOUTT : TEST ONLY FACTORY를 이동하는 제품의 TKOUT수량을 조회한다.                                               */
                strSqlString.Append("        SELECT A.MAT_ID AS MAT_ID " + "\n");
                strSqlString.Append("             , CASE WHEN (A.OLD_OPER = 'T0000') THEN 'HMK3' " + "\n");
                strSqlString.Append("                    WHEN (A.OLD_OPER = 'T1300') THEN 'TEST' " + "\n");
                strSqlString.Append("                    ELSE ' ' " + "\n");
                strSqlString.Append("               END AS AREA " + "\n");
                strSqlString.Append("             , 0 AS BOHA " + "\n");
                strSqlString.Append("             , 0 AS BOHT " + "\n");
                strSqlString.Append("             , 0 AS TKINA " + "\n");
                strSqlString.Append("             , 0 AS TKINT " + "\n");
                strSqlString.Append("             , 0 AS REJECTA " + "\n");
                strSqlString.Append("             , 0 AS REJECTT " + "\n");
                strSqlString.Append("             , 0 AS BONUSA " + "\n");
                strSqlString.Append("             , 0 AS BONUST " + "\n");
                strSqlString.Append("             , 0 AS TKOUTA " + "\n");
                strSqlString.Append("             , CASE WHEN (A.OLD_OPER = 'T0000') THEN SUM(A.QTY_1) " + "\n");
                strSqlString.Append("                    WHEN (A.OLD_OPER = 'T1300') THEN SUM(A.QTY_1) " + "\n");
                strSqlString.Append("                    ELSE 0 " + "\n");
                strSqlString.Append("               END AS TKOUTT " + "\n");
                strSqlString.Append("             , 0 AS EOHA " + "\n");
                strSqlString.Append("             , 0 AS EOHT " + "\n");
                strSqlString.Append("             , 0 AS RES_HA_H2 " + "\n");
                strSqlString.Append("             , 0 AS CHAG_PROD " + "\n");
                strSqlString.Append("             , 0 AS MULT_TRANS " + "\n");
                strSqlString.Append("             , 0 AS RET_H3_H2 " + "\n");
                strSqlString.Append("             , 0 AS RET_H4_H3 " + "\n");
                strSqlString.Append("             , 0 AS SHIP_H3 " + "\n");
                strSqlString.Append("             , 0 AS RECV_H3 " + "\n");
                strSqlString.Append("             , 0 AS RET_CO_H3 " + "\n");
                strSqlString.Append("             , 0 AS RET_H3_CO " + "\n");
                strSqlString.Append("             , 0 AS SHIP_H4 " + "\n");
                strSqlString.Append("             , 0 AS RET_CO_H4 " + "\n");
                strSqlString.Append("             , 0 AS RET_H4 " + "\n");
                strSqlString.Append("             , 0 AS DIFFER " + "\n");
                strSqlString.Append("           FROM RWIPLOTHIS A " + "\n");
                strSqlString.Append("          WHERE A.FACTORY = '" + GlobalVariable.gsTestDefaultFactory + "' " + "\n");
                strSqlString.Append("            AND A.OLD_OPER IN ('T0000', 'T1300') " + "\n");
                strSqlString.Append("            AND A.TRAN_CODE = 'END' " + "\n");
                strSqlString.Append("            AND A.LOT_TYPE = 'W' " + "\n");
                strSqlString.Append("            AND A.OWNER_CODE = 'PROD' " + "\n");
                strSqlString.Append("            AND A.HIST_DEL_FLAG <> 'Y' " + "\n");
                strSqlString.Append("            AND TRIM(A.LOT_CMF_7) <> 'HM' " + "\n");
                strSqlString.AppendFormat("           AND A.LOT_CMF_5 LIKE '{0}%' " + "\n", txtLotType.Text.Trim());
                strSqlString.AppendFormat("           AND A.MAT_ID LIKE '{0}%' " + "\n", txtProduct.Text.Trim());
                strSqlString.AppendFormat("           AND A.TRAN_TIME BETWEEN '{0}' AND '{1}' " + "\n", strFromDate, strToDate);
                strSqlString.Append("          GROUP BY A.MAT_ID, A.OLD_OPER " + "\n");
                strSqlString.Append("        UNION ALL " + "\n");
                /********************************************************************************************************************/
                /* EOHA : ASSY, TEST FACTORY를 이동하는 제품의 EOHA수량을 조회한다.                                                 */
                /*      : ASSY + TEST FACTORY를 이동하는 제품의 EOHA수량은 T0000을 제외한다.                                        */
                strSqlString.Append("        SELECT A.MAT_ID AS MAT_ID " + "\n");
                strSqlString.Append("             , CASE WHEN (A.OPER = 'A0040') THEN 'BLAB' " + "\n");
                strSqlString.Append("                    WHEN (A.OPER > 'A0400' AND A.OPER < 'A0401') THEN 'BOND' " + "\n");
                strSqlString.Append("                    WHEN (A.OPER > 'A0401' AND A.OPER <= 'A0609') THEN 'BOND' " + "\n");
                strSqlString.Append("                    WHEN (A.OPER >= 'A0610' AND A.OPER <= 'A1000') THEN 'MOLD' " + "\n");
                strSqlString.Append("                    WHEN (A.OPER >= 'A1001' AND A.OPER <= 'A2300') THEN 'FINI' " + "\n");
                strSqlString.Append("                    WHEN (A.OPER >= 'AZ009' AND A.OPER <= 'T0000') THEN 'HMK3' " + "\n");
                strSqlString.Append("                    WHEN (A.OPER >= 'T0100' AND A.OPER <= 'T1300') THEN 'TEST' " + "\n");
                strSqlString.Append("                    WHEN (A.OPER = 'TZ010') THEN 'HMK4' " + "\n");
                strSqlString.Append("                    ELSE ' ' " + "\n");
                strSqlString.Append("               END AS AREA " + "\n");
                strSqlString.Append("             , 0 AS BOHA " + "\n");
                strSqlString.Append("             , 0 AS BOHT " + "\n");
                strSqlString.Append("             , 0 AS TKINA " + "\n");
                strSqlString.Append("             , 0 AS TKINT " + "\n");
                strSqlString.Append("             , 0 AS REJECTA " + "\n");
                strSqlString.Append("             , 0 AS REJECTT " + "\n");
                strSqlString.Append("             , 0 AS BONUSA " + "\n");
                strSqlString.Append("             , 0 AS BONUST " + "\n");
                strSqlString.Append("             , 0 AS TKOUTA " + "\n");
                strSqlString.Append("             , 0 AS TKOUTT " + "\n");
                strSqlString.Append("             , SUM(A.QTY_1) AS EOHA " + "\n");
                strSqlString.Append("             , 0 AS EOHT " + "\n");
                strSqlString.Append("             , 0 AS RES_HA_H2 " + "\n");
                strSqlString.Append("             , 0 AS CHAG_PROD " + "\n");
                strSqlString.Append("             , 0 AS MULT_TRANS " + "\n");
                strSqlString.Append("             , 0 AS RET_H3_H2 " + "\n");
                strSqlString.Append("             , 0 AS RET_H4_H3 " + "\n");
                strSqlString.Append("             , 0 AS SHIP_H3 " + "\n");
                strSqlString.Append("             , 0 AS RECV_H3 " + "\n");
                strSqlString.Append("             , 0 AS RET_CO_H3 " + "\n");
                strSqlString.Append("             , 0 AS RET_H3_CO " + "\n");
                strSqlString.Append("             , 0 AS SHIP_H4 " + "\n");
                strSqlString.Append("             , 0 AS RET_CO_H4 " + "\n");
                strSqlString.Append("             , 0 AS RET_H4 " + "\n");
                strSqlString.Append("             , 0 AS DIFFER " + "\n");
                strSqlString.Append("          FROM RWIPLOTSTS_BOH A " + "\n");
                strSqlString.Append("         WHERE A.FACTORY IN ('" + GlobalVariable.gsAssyDefaultFactory + "', '" + GlobalVariable.gsTestDefaultFactory + "') " + "\n");
                strSqlString.Append("            AND A.LOT_TYPE = 'W' " + "\n");
                strSqlString.Append("            AND A.OWNER_CODE = 'PROD' " + "\n");
                strSqlString.Append("            AND (TRIM(A.LOT_CMF_7) = 'HM' OR TRIM(A.LOT_CMF_7) = '') " + "\n");
                strSqlString.AppendFormat("           AND A.LOT_CMF_5 LIKE '{0}%' " + "\n", txtLotType.Text.Trim());
                strSqlString.AppendFormat("           AND A.MAT_ID LIKE '{0}%' " + "\n", txtProduct.Text.Trim());
                strSqlString.AppendFormat("           AND A.CUTOFF_DT = '{0}' " + "\n", strToCutOffDate);
                strSqlString.Append("          GROUP BY A.MAT_ID, A.OPER " + "\n");
                strSqlString.Append("        UNION ALL " + "\n");
                /********************************************************************************************************************/
                /* EOHT : TEST ONLY FACTORY를 이동하는 제품의 EOH수량을 조회한다.                                                   */
                strSqlString.Append("        SELECT A.MAT_ID AS MAT_ID " + "\n");
                strSqlString.Append("             , CASE WHEN (A.OPER = 'T0000') THEN 'HMK3' " + "\n");
                strSqlString.Append("                    WHEN (A.OPER >= 'T0100' AND A.OPER <= 'T1300') THEN 'TEST' " + "\n");
                strSqlString.Append("                    WHEN (A.OPER = 'TZ010') THEN 'HMK4' " + "\n");
                strSqlString.Append("                    ELSE ' ' " + "\n");
                strSqlString.Append("               END AS AREA " + "\n");
                strSqlString.Append("             , 0 AS BOHA " + "\n");
                strSqlString.Append("             , 0 AS BOHT " + "\n");
                strSqlString.Append("             , 0 AS TKINA " + "\n");
                strSqlString.Append("             , 0 AS TKINT " + "\n");
                strSqlString.Append("             , 0 AS REJECTA " + "\n");
                strSqlString.Append("             , 0 AS REJECTT " + "\n");
                strSqlString.Append("             , 0 AS BONUSA " + "\n");
                strSqlString.Append("             , 0 AS BONUST " + "\n");
                strSqlString.Append("             , 0 AS TKOUTA " + "\n");
                strSqlString.Append("             , 0 AS TKOUTT " + "\n");
                strSqlString.Append("             , 0 AS EOHA " + "\n");
                strSqlString.Append("             , SUM(A.QTY_1) AS EOHT " + "\n");
                strSqlString.Append("             , 0 AS RES_HA_H2 " + "\n");
                strSqlString.Append("             , 0 AS CHAG_PROD " + "\n");
                strSqlString.Append("             , 0 AS MULT_TRANS " + "\n");
                strSqlString.Append("             , 0 AS RET_H3_H2 " + "\n");
                strSqlString.Append("             , 0 AS RET_H4_H3 " + "\n");
                strSqlString.Append("             , 0 AS SHIP_H3 " + "\n");
                strSqlString.Append("             , 0 AS RECV_H3 " + "\n");
                strSqlString.Append("             , 0 AS RET_CO_H3 " + "\n");
                strSqlString.Append("             , 0 AS RET_H3_CO " + "\n");
                strSqlString.Append("             , 0 AS SHIP_H4 " + "\n");
                strSqlString.Append("             , 0 AS RET_CO_H4 " + "\n");
                strSqlString.Append("             , 0 AS RET_H4 " + "\n");
                strSqlString.Append("             , 0 AS DIFFER " + "\n");
                strSqlString.Append("          FROM RWIPLOTSTS_BOH A " + "\n");
                strSqlString.Append("         WHERE A.FACTORY = '" + GlobalVariable.gsTestDefaultFactory + "' " + "\n");
                strSqlString.Append("           AND A.LOT_TYPE = 'W' " + "\n");
                strSqlString.Append("           AND A.OWNER_CODE = 'PROD' " + "\n");
                // 2009-04-02-정비재 : Test로 넘어온 제품의 Assy Site를 구분하지 않는다.
                //strSqlString.Append("           AND TRIM(A.LOT_CMF_7) <> 'HM' " + "\n");
                strSqlString.AppendFormat("           AND A.LOT_CMF_5 LIKE '{0}%' " + "\n", txtLotType.Text.Trim());
                strSqlString.AppendFormat("           AND A.MAT_ID LIKE '{0}%' " + "\n", txtProduct.Text.Trim());
                strSqlString.AppendFormat("           AND A.CUTOFF_DT = '{0}' " + "\n", strToCutOffDate);
                strSqlString.Append("          GROUP BY A.MAT_ID, A.OPER " + "\n");
                strSqlString.Append("        UNION ALL " + "\n");
                /********************************************************************************************************************/
                /* RETURN_HMKA_HMK2 : ASSY, TEST FACTORY를 이동하는 제품의 RETURN_HMKA_HMK2수량을 조회한다.                         */
                strSqlString.Append("        SELECT A.MAT_ID AS MAT_ID " + "\n");
                strSqlString.Append("             , CASE WHEN (A.OLD_OPER = 'A0040') THEN 'BLAB' " + "\n");
                strSqlString.Append("                    WHEN (A.OLD_OPER >  'A0200' AND A.OLD_OPER <= 'A0609') THEN 'BOND' " + "\n");
                strSqlString.Append("                    WHEN (A.OLD_OPER >= 'A0610' AND A.OLD_OPER <= 'A1000') THEN 'MOLD' " + "\n");
                strSqlString.Append("                    WHEN (A.OLD_OPER >= 'A1001' AND A.OLD_OPER <= 'A2100') THEN 'FINI' " + "\n");
                strSqlString.Append("                    WHEN (A.OLD_OPER >= 'AZ009' AND A.OLD_OPER <= 'AZ010') THEN 'HMK3' " + "\n");
                strSqlString.Append("                    WHEN (A.OLD_OPER = 'T0000') THEN 'HMK3' " + "\n");
                strSqlString.Append("                    WHEN (A.OLD_OPER >= 'T0100' AND A.OLD_OPER <= 'T1300') THEN 'TEST' " + "\n");
                strSqlString.Append("                    WHEN (A.OLD_OPER = 'TZ010') THEN 'HMK4' " + "\n");
                strSqlString.Append("                    ELSE ' ' " + "\n");
                strSqlString.Append("               END AS AREA " + "\n");
                strSqlString.Append("             , 0 AS BOHA " + "\n");
                strSqlString.Append("             , 0 AS BOHT " + "\n");
                strSqlString.Append("             , 0 AS TKINA " + "\n");
                strSqlString.Append("             , 0 AS TKINT " + "\n");
                strSqlString.Append("             , 0 AS REJECTA " + "\n");
                strSqlString.Append("             , 0 AS REJECTT " + "\n");
                strSqlString.Append("             , 0 AS BONUSA " + "\n");
                strSqlString.Append("             , 0 AS BONUST " + "\n");
                strSqlString.Append("             , 0 AS TKOUTA " + "\n");
                strSqlString.Append("             , 0 AS TKOUTT " + "\n");
                strSqlString.Append("             , 0 AS EOHA " + "\n");
                strSqlString.Append("             , 0 AS EOHT " + "\n");
                strSqlString.Append("             , SUM(A.QTY_1) AS RES_HA_H2 " + "\n");
                strSqlString.Append("             , 0 AS CHAG_PROD " + "\n");
                strSqlString.Append("             , 0 AS MULT_TRANS " + "\n");
                strSqlString.Append("             , 0 AS RET_H3_H2 " + "\n");
                strSqlString.Append("             , 0 AS RET_H4_H3 " + "\n");
                strSqlString.Append("             , 0 AS SHIP_H3 " + "\n");
                strSqlString.Append("             , 0 AS RECV_H3 " + "\n");
                strSqlString.Append("             , 0 AS RET_CO_H3 " + "\n");
                strSqlString.Append("             , 0 AS RET_H3_CO " + "\n");
                strSqlString.Append("             , 0 AS SHIP_H4 " + "\n");
                strSqlString.Append("             , 0 AS RET_CO_H4 " + "\n");
                strSqlString.Append("             , 0 AS RET_H4 " + "\n");
                strSqlString.Append("             , 0 AS DIFFER " + "\n");
                strSqlString.Append("          FROM CWIPLOTRTN A " + "\n");
                strSqlString.Append("         WHERE A.FACTORY IN ('" + GlobalVariable.gsAssyDefaultFactory + "', '" + GlobalVariable.gsTestDefaultFactory + "') " + "\n");
                strSqlString.Append("           AND A.OPER = 'A0000' " + "\n");
                strSqlString.Append("           AND A.OLD_OPER NOT IN ('A0000', 'AZ009', 'AZ010') " + "\n");
                strSqlString.Append("           AND A.LOT_TYPE = 'W' " + "\n");
                strSqlString.Append("           AND A.OWNER_CODE = 'PROD' " + "\n");
                strSqlString.Append("           AND (TRIM(A.LOT_CMF_7) = 'HM' OR TRIM(A.LOT_CMF_7) = '') " + "\n");
                strSqlString.AppendFormat("           AND A.LOT_CMF_5 LIKE '{0}%' " + "\n", txtLotType.Text.Trim());
                strSqlString.AppendFormat("           AND A.MAT_ID LIKE '{0}%' " + "\n", txtProduct.Text.Trim());
                strSqlString.AppendFormat("           AND A.TRAN_TIME BETWEEN '{0}' AND '{1}' " + "\n", strFromDate, strToDate);
                strSqlString.Append("          GROUP BY A.MAT_ID, A.OLD_OPER " + "\n");
                strSqlString.Append("        UNION ALL " + "\n");
                /********************************************************************************************************************/
                /* CHANGE_PRODUCT : ASSY, TEST FACTORY를 이동하는 제품의 CHANGE PRODUCT수량을 조회한다.                             */
                strSqlString.Append("        SELECT A.MAT_ID AS MAT_ID " + "\n");
                strSqlString.Append("             , 'BOND' AS AREA " + "\n");
                strSqlString.Append("             , 0 AS BOHA " + "\n");
                strSqlString.Append("             , 0 AS BOHT " + "\n");
                strSqlString.Append("             , 0 AS TKINA " + "\n");
                strSqlString.Append("             , 0 AS TKINT " + "\n");
                strSqlString.Append("             , 0 AS REJECTA " + "\n");
                strSqlString.Append("             , 0 AS REJECTT " + "\n");
                strSqlString.Append("             , 0 AS BONUSA " + "\n");
                strSqlString.Append("             , 0 AS BONUST " + "\n");
                strSqlString.Append("             , 0 AS TKOUTA " + "\n");
                strSqlString.Append("             , 0 AS TKOUTT " + "\n");
                strSqlString.Append("             , 0 AS EOHA " + "\n");
                strSqlString.Append("             , 0 AS EOHT " + "\n");
                strSqlString.Append("             , 0 AS RES_HA_H2 " + "\n");
                strSqlString.Append("             , SUM(A.QTY_1) AS CHAN_PROD " + "\n");
                strSqlString.Append("             , 0 AS MULT_TRANS " + "\n");
                strSqlString.Append("             , 0 AS RET_H3_H2 " + "\n");
                strSqlString.Append("             , 0 AS RET_H4_H3 " + "\n");
                strSqlString.Append("             , 0 AS SHIP_H3 " + "\n");
                strSqlString.Append("             , 0 AS RECV_H3 " + "\n");
                strSqlString.Append("             , 0 AS RET_CO_H3 " + "\n");
                strSqlString.Append("             , 0 AS RET_H3_CO " + "\n");
                strSqlString.Append("             , 0 AS SHIP_H4 " + "\n");
                strSqlString.Append("             , 0 AS RET_CO_H4 " + "\n");
                strSqlString.Append("             , 0 AS RET_H4 " + "\n");
                strSqlString.Append("             , 0 AS DIFFER " + "\n");
                strSqlString.Append("          FROM RWIPLOTHIS A " + "\n");
                strSqlString.Append("         WHERE A.MAT_ID <> A.OLD_MAT_ID " + "\n");
                strSqlString.Append("           AND A.FACTORY = '" + GlobalVariable.gsAssyDefaultFactory + "' " + "\n");
                strSqlString.Append("           AND A.OPER LIKE 'A06%' " + "\n");
                strSqlString.Append("           AND A.TRAN_CODE = 'ADAPT' " + "\n");
                strSqlString.Append("           AND A.LOT_TYPE = 'W' " + "\n");
                strSqlString.Append("           AND A.OWNER_CODE = 'PROD' " + "\n");
                strSqlString.Append("           AND (TRIM(A.LOT_CMF_7) = 'HM' OR TRIM(A.LOT_CMF_7) = '') " + "\n");
                strSqlString.AppendFormat("           AND A.LOT_CMF_5 LIKE '{0}%' " + "\n", txtLotType.Text.Trim());
                strSqlString.AppendFormat("           AND A.MAT_ID LIKE '{0}%' " + "\n", txtProduct.Text.Trim());
                strSqlString.AppendFormat("           AND A.TRAN_TIME BETWEEN '{0}' AND '{1}' " + "\n", strFromDate, strToDate);
                strSqlString.Append("          GROUP BY A.MAT_ID " + "\n");
                strSqlString.Append("        UNION ALL " + "\n");
                /********************************************************************************************************************/
                /* MULTI_TRANSFER : ASSY, TEST FACTORY를 이동하는 제품의 MULTI TRANSFER수량을 조회한다.                             */
                strSqlString.Append("        SELECT A.MAT_ID AS MAT_ID " + "\n");
                strSqlString.Append("             , 'BOND' AS AREA " + "\n");
                strSqlString.Append("             , 0 AS BOHA " + "\n");
                strSqlString.Append("             , 0 AS BOHT " + "\n");
                strSqlString.Append("             , 0 AS TKINA " + "\n");
                strSqlString.Append("             , 0 AS TKINT " + "\n");
                strSqlString.Append("             , 0 AS REJECTA " + "\n");
                strSqlString.Append("             , 0 AS REJECTT " + "\n");
                strSqlString.Append("             , 0 AS BONUSA " + "\n");
                strSqlString.Append("             , 0 AS BONUST " + "\n");
                strSqlString.Append("             , 0 AS TKOUTA " + "\n");
                strSqlString.Append("             , 0 AS TKOUTT " + "\n");
                strSqlString.Append("             , 0 AS EOHA " + "\n");
                strSqlString.Append("             , 0 AS EOHT " + "\n");
                strSqlString.Append("             , 0 AS RES_HA_H2 " + "\n");
                strSqlString.Append("             , 0 AS CHAN_PROD " + "\n");
                strSqlString.Append("             , SUM(A.QTY_1) AS MULT_TRANS " + "\n");
                strSqlString.Append("             , 0 AS RET_H3_H2 " + "\n");
                strSqlString.Append("             , 0 AS RET_H4_H3 " + "\n");
                strSqlString.Append("             , 0 AS SHIP_H3 " + "\n");
                strSqlString.Append("             , 0 AS RECV_H3 " + "\n");
                strSqlString.Append("             , 0 AS RET_CO_H3 " + "\n");
                strSqlString.Append("             , 0 AS RET_H3_CO " + "\n");
                strSqlString.Append("             , 0 AS SHIP_H4 " + "\n");
                strSqlString.Append("             , 0 AS RET_CO_H4 " + "\n");
                strSqlString.Append("             , 0 AS RET_H4 " + "\n");
                strSqlString.Append("             , 0 AS DIFFER " + "\n");
                strSqlString.Append("          FROM RWIPLOTHIS A " + "\n");
                strSqlString.Append("         WHERE A.FACTORY = '" + GlobalVariable.gsAssyDefaultFactory + "' " + "\n");
                strSqlString.Append("           AND A.OPER LIKE 'A06%' " + "\n");
                strSqlString.Append("           AND A.TRAN_CODE = 'COMBINE' " + "\n");
                strSqlString.Append("           AND A.LOT_TYPE = 'W' " + "\n");
                strSqlString.Append("           AND A.OWNER_CODE = 'PROD' " + "\n");
                strSqlString.Append("           AND A.LOT_DEL_FLAG = 'Y' " + "\n");
                strSqlString.Append("           AND (TRIM(A.LOT_CMF_7) = 'HM' OR TRIM(A.LOT_CMF_7) = '') " + "\n");
                strSqlString.AppendFormat("           AND A.LOT_CMF_5 LIKE '{0}%' " + "\n", txtLotType.Text.Trim());
                strSqlString.AppendFormat("           AND A.MAT_ID LIKE '{0}%' " + "\n", txtProduct.Text.Trim());
                strSqlString.AppendFormat("           AND A.TRAN_TIME BETWEEN '{0}' AND '{1}' " + "\n", strFromDate, strToDate);
                strSqlString.Append("         GROUP BY A.MAT_ID " + "\n");
                strSqlString.Append("        UNION ALL " + "\n");
                /********************************************************************************************************************/
                /* RETURN_HMK3_HMK2 : ASSY, TEST FACTORY를 이동하는 제품의 HMK3 STOCK에서 HMK2 STOCK로 이동한 수량을 조회한다.      */
                strSqlString.Append("        SELECT A.MAT_ID AS MAT_ID " + "\n");
                strSqlString.Append("             , 'HMK3' AS AREA " + "\n");
                strSqlString.Append("             , 0 AS BOHA " + "\n");
                strSqlString.Append("             , 0 AS BOHT " + "\n");
                strSqlString.Append("             , 0 AS TKINA " + "\n");
                strSqlString.Append("             , 0 AS TKINT " + "\n");
                strSqlString.Append("             , 0 AS REJECTA " + "\n");
                strSqlString.Append("             , 0 AS REJECTT " + "\n");
                strSqlString.Append("             , 0 AS BONUSA " + "\n");
                strSqlString.Append("             , 0 AS BONUST " + "\n");
                strSqlString.Append("             , 0 AS TKOUTA " + "\n");
                strSqlString.Append("             , 0 AS TKOUTT " + "\n");
                strSqlString.Append("             , 0 AS EOHA " + "\n");
                strSqlString.Append("             , 0 AS EOHT " + "\n");
                strSqlString.Append("             , 0 AS RES_HA_H2 " + "\n");
                strSqlString.Append("             , 0 AS CHAN_PROD " + "\n");
                strSqlString.Append("             , 0 AS MULT_TRANS " + "\n");
                strSqlString.Append("             , SUM(A.QTY_1) AS RET_H3_H2 " + "\n");
                strSqlString.Append("             , 0 AS RET_H4_H3 " + "\n");
                strSqlString.Append("             , 0 AS SHIP_H3 " + "\n");
                strSqlString.Append("             , 0 AS RECV_H3 " + "\n");
                strSqlString.Append("             , 0 AS RET_CO_H3 " + "\n");
                strSqlString.Append("             , 0 AS RET_H3_CO " + "\n");
                strSqlString.Append("             , 0 AS SHIP_H4 " + "\n");
                strSqlString.Append("             , 0 AS RET_CO_H4 " + "\n");
                strSqlString.Append("             , 0 AS RET_H4 " + "\n");
                strSqlString.Append("             , 0 AS DIFFER " + "\n");
                strSqlString.Append("          FROM CWIPLOTRTN A " + "\n");
                strSqlString.Append("         WHERE A.FACTORY = '" + GlobalVariable.gsAssyDefaultFactory + "' " + "\n");
                strSqlString.Append("           AND A.OPER = 'A0000' " + "\n");
                strSqlString.Append("           AND A.OLD_OPER IN ('AZ009', 'AZ010') " + "\n");
                strSqlString.Append("           AND A.LOT_TYPE = 'W' " + "\n");
                strSqlString.Append("           AND A.OWNER_CODE = 'PROD' " + "\n");
                strSqlString.Append("           AND (TRIM(A.LOT_CMF_7) = 'HM' OR TRIM(A.LOT_CMF_7) = '') " + "\n");
                strSqlString.AppendFormat("           AND A.LOT_CMF_5 LIKE '{0}%' " + "\n", txtLotType.Text.Trim());
                strSqlString.AppendFormat("           AND A.MAT_ID LIKE '{0}%' " + "\n", txtProduct.Text.Trim());
                strSqlString.AppendFormat("           AND A.TRAN_TIME BETWEEN '{0}' AND '{1}' " + "\n", strFromDate, strToDate);
                strSqlString.Append("         GROUP BY A.MAT_ID " + "\n");
                strSqlString.Append("        UNION ALL " + "\n");
                /********************************************************************************************************************/
                /* RETURN_HMK4_HMK3 : ASSY, TEST FACTORY를 이동하는 제품의 HMK4 STOCK에서 HMK3 STOCK로 이동한 수량을 조회한다.      */
                strSqlString.Append("        SELECT A.MAT_ID AS MAT_ID " + "\n");
                strSqlString.Append("             , 'HMK4' AS AREA " + "\n");
                strSqlString.Append("             , 0 AS BOHA " + "\n");
                strSqlString.Append("             , 0 AS BOHT " + "\n");
                strSqlString.Append("             , 0 AS TKINA " + "\n");
                strSqlString.Append("             , 0 AS TKINT " + "\n");
                strSqlString.Append("             , 0 AS REJECTA " + "\n");
                strSqlString.Append("             , 0 AS REJECTT " + "\n");
                strSqlString.Append("             , 0 AS BONUSA " + "\n");
                strSqlString.Append("             , 0 AS BONUST " + "\n");
                strSqlString.Append("             , 0 AS TKOUTA " + "\n");
                strSqlString.Append("             , 0 AS TKOUTT " + "\n");
                strSqlString.Append("             , 0 AS EOHA " + "\n");
                strSqlString.Append("             , 0 AS EOHT " + "\n");
                strSqlString.Append("             , 0 AS RES_HA_H2 " + "\n");
                strSqlString.Append("             , 0 AS CHAN_PROD " + "\n");
                strSqlString.Append("             , 0 AS MULT_TRANS " + "\n");
                strSqlString.Append("             , 0 AS RET_H3_H2 " + "\n");
                strSqlString.Append("             , SUM(A.QTY_1) AS RET_H4_H3 " + "\n");
                strSqlString.Append("             , 0 AS SHIP_H3 " + "\n");
                strSqlString.Append("             , 0 AS RECV_H3 " + "\n");
                strSqlString.Append("             , 0 AS RET_CO_H3 " + "\n");
                strSqlString.Append("             , 0 AS RET_H3_CO " + "\n");
                strSqlString.Append("             , 0 AS SHIP_H4 " + "\n");
                strSqlString.Append("             , 0 AS RET_CO_H4 " + "\n");
                strSqlString.Append("             , 0 AS RET_H4 " + "\n");
                strSqlString.Append("             , 0 AS DIFFER " + "\n");
                strSqlString.Append("          FROM CWIPLOTRTN A " + "\n");
                strSqlString.Append("         WHERE A.FACTORY = '" + GlobalVariable.gsTestDefaultFactory + "' " + "\n");
                strSqlString.Append("           AND A.OPER = 'T0000' " + "\n");
                strSqlString.Append("           AND A.OLD_OPER ='TZ010' " + "\n");
                strSqlString.Append("           AND A.LOT_TYPE = 'W' " + "\n");
                strSqlString.Append("           AND A.OWNER_CODE = 'PROD' " + "\n");
                strSqlString.Append("           AND TRIM(A.LOT_CMF_7) <> 'HM' " + "\n");
                strSqlString.AppendFormat("           AND A.LOT_CMF_5 LIKE '{0}%' " + "\n", txtLotType.Text.Trim());
                strSqlString.AppendFormat("           AND A.MAT_ID LIKE '{0}%' " + "\n", txtProduct.Text.Trim());
                strSqlString.AppendFormat("           AND A.TRAN_TIME BETWEEN '{0}' AND '{1}' " + "\n", strFromDate, strToDate);
                strSqlString.Append("         GROUP BY A.MAT_ID " + "\n");
                strSqlString.Append("        UNION ALL " + "\n");
                /********************************************************************************************************************/
                /* SHIP_HMK3 : ASSY, TEST FACTORY를 이동하는 제품의 HMK3 STOCK에서 SHIP된 수량을 조회한다.                          */
                strSqlString.Append("        SELECT A.MAT_ID AS MAT_ID " + "\n");
                strSqlString.Append("             , 'HMK3' AS AREA " + "\n");
                strSqlString.Append("             , 0 AS BOHA " + "\n");
                strSqlString.Append("             , 0 AS BOHT " + "\n");
                strSqlString.Append("             , 0 AS TKINA " + "\n");
                strSqlString.Append("             , 0 AS TKINT " + "\n");
                strSqlString.Append("             , 0 AS REJECTA " + "\n");
                strSqlString.Append("             , 0 AS REJECTT " + "\n");
                strSqlString.Append("             , 0 AS BONUSA " + "\n");
                strSqlString.Append("             , 0 AS BONUST " + "\n");
                strSqlString.Append("             , 0 AS TKOUTA " + "\n");
                strSqlString.Append("             , 0 AS TKOUTT " + "\n");
                strSqlString.Append("             , 0 AS EOHA " + "\n");
                strSqlString.Append("             , 0 AS EOHT " + "\n");
                strSqlString.Append("             , 0 AS RES_HA_H2 " + "\n");
                strSqlString.Append("             , 0 AS CHAN_PROD " + "\n");
                strSqlString.Append("             , 0 AS MULT_TRANS " + "\n");
                strSqlString.Append("             , 0 AS RET_H3_H2 " + "\n");
                strSqlString.Append("             , 0 AS RET_H4_H3 " + "\n");
                strSqlString.Append("             , SUM(A.QTY_1) AS SHIP_H3 " + "\n");
                strSqlString.Append("             , 0 AS RECV_H3 " + "\n");
                strSqlString.Append("             , 0 AS RET_CO_H3 " + "\n");
                strSqlString.Append("             , 0 AS RET_H3_CO " + "\n");
                strSqlString.Append("             , 0 AS SHIP_H4 " + "\n");
                strSqlString.Append("             , 0 AS RET_CO_H4 " + "\n");
                strSqlString.Append("             , 0 AS RET_H4 " + "\n");
                strSqlString.Append("             , 0 AS DIFFER " + "\n");
                strSqlString.Append("          FROM RWIPLOTHIS A " + "\n");
                strSqlString.Append("         WHERE A.FACTORY IN ('" + GlobalVariable.gsTestDefaultFactory + "', 'CUSTOMER') " + "\n");
                strSqlString.Append("           AND A.OLD_FACTORY = '" + GlobalVariable.gsAssyDefaultFactory + "' " + "\n");
                strSqlString.Append("           AND A.OPER = ' ' " + "\n");
                strSqlString.Append("           AND A.OLD_OPER IN ('AZ009', 'AZ010') " + "\n");
                strSqlString.Append("           AND A.TRAN_CODE = 'SHIP' " + "\n");
                strSqlString.Append("           AND A.LOT_TYPE = 'W' " + "\n");
                strSqlString.Append("           AND A.OWNER_CODE = 'PROD' " + "\n");
                strSqlString.Append("           AND A.HIST_DEL_FLAG <> 'Y' " + "\n");
                strSqlString.Append("           AND (TRIM(A.LOT_CMF_7) = 'HM' OR TRIM(A.LOT_CMF_7) = '') " + "\n");
                strSqlString.AppendFormat("           AND A.LOT_CMF_5 LIKE '{0}%' " + "\n", txtLotType.Text.Trim());
                strSqlString.AppendFormat("           AND A.MAT_ID LIKE '{0}%' " + "\n", txtProduct.Text.Trim());
                strSqlString.AppendFormat("           AND A.TRAN_TIME BETWEEN '{0}' AND '{1}' " + "\n", strFromDate, strToDate);
                strSqlString.Append("         GROUP BY A.MAT_ID " + "\n");
                strSqlString.Append("        UNION ALL " + "\n");
                /********************************************************************************************************************/
                /* RECIVE_HMK3 : TEST ONLY FACTORY를 이동하는 제품의 HMK3 STOCK에서 RECEIVE된 수량을 조회한다.                      */
                strSqlString.Append("        SELECT A.MAT_ID AS MAT_ID " + "\n");
                strSqlString.Append("             , 'HMK3' AS AREA " + "\n");
                strSqlString.Append("             , 0 AS BOHA " + "\n");
                strSqlString.Append("             , 0 AS BOHT " + "\n");
                strSqlString.Append("             , 0 AS TKINA " + "\n");
                strSqlString.Append("             , 0 AS TKINT " + "\n");
                strSqlString.Append("             , 0 AS REJECTA " + "\n");
                strSqlString.Append("             , 0 AS REJECTT " + "\n");
                strSqlString.Append("             , 0 AS BONUSA " + "\n");
                strSqlString.Append("             , 0 AS BONUST " + "\n");
                strSqlString.Append("             , 0 AS TKOUTA " + "\n");
                strSqlString.Append("             , 0 AS TKOUTT " + "\n");
                strSqlString.Append("             , 0 AS EOHA " + "\n");
                strSqlString.Append("             , 0 AS EOHT " + "\n");
                strSqlString.Append("             , 0 AS RES_HA_H2 " + "\n");
                strSqlString.Append("             , 0 AS CHAN_PROD " + "\n");
                strSqlString.Append("             , 0 AS MULT_TRANS " + "\n");
                strSqlString.Append("             , 0 AS RET_H3_H2 " + "\n");
                strSqlString.Append("             , 0 AS RET_H4_H3 " + "\n");
                strSqlString.Append("             , 0 AS SHIP_H3 " + "\n");
                strSqlString.Append("             , SUM(A.QTY_1) AS RECV_H3 " + "\n");
                strSqlString.Append("             , 0 AS RET_CO_H3 " + "\n");
                strSqlString.Append("             , 0 AS RET_H3_CO " + "\n");
                strSqlString.Append("             , 0 AS SHIP_H4 " + "\n");
                strSqlString.Append("             , 0 AS RET_CO_H4 " + "\n");
                strSqlString.Append("             , 0 AS RET_H4 " + "\n");
                strSqlString.Append("             , 0 AS DIFFER " + "\n");
                strSqlString.Append("          FROM RWIPLOTHIS A " + "\n");
                strSqlString.Append("         WHERE A.FACTORY = '" + GlobalVariable.gsTestDefaultFactory + "' " + "\n");
                strSqlString.Append("           AND A.TRAN_CODE = 'CREATE' " + "\n");
                strSqlString.Append("           AND A.OPER = 'T0000' " + "\n");
                strSqlString.Append("           AND A.LOT_TYPE = 'W' " + "\n");
                strSqlString.Append("           AND A.OWNER_CODE = 'PROD' " + "\n");
                strSqlString.Append("           AND TRIM(A.LOT_CMF_7) <> 'HM' " + "\n");
                strSqlString.AppendFormat("           AND A.LOT_CMF_5 LIKE '{0}%' " + "\n", txtLotType.Text.Trim());
                strSqlString.AppendFormat("           AND A.MAT_ID LIKE '{0}%' " + "\n", txtProduct.Text.Trim());
                strSqlString.AppendFormat("           AND A.TRAN_TIME BETWEEN '{0}' AND '{1}' " + "\n", strFromDate, strToDate);
                strSqlString.Append("         GROUP BY A.MAT_ID " + "\n");
                strSqlString.Append("        UNION ALL " + "\n");
                /********************************************************************************************************************/
                /* RETURN_CUSTOMER_HMK3 : ASSY, TEST FACTORY를 이동하는 제품의 HMK3 STOCK에서 RETURN된 수량을 조회한다.             */
                strSqlString.Append("        SELECT A.MAT_ID AS MAT_ID " + "\n");
                strSqlString.Append("             , 'HMK3' AS AREA " + "\n");
                strSqlString.Append("             , 0 AS BOHA " + "\n");
                strSqlString.Append("             , 0 AS BOHT " + "\n");
                strSqlString.Append("             , 0 AS TKINA " + "\n");
                strSqlString.Append("             , 0 AS TKINT " + "\n");
                strSqlString.Append("             , 0 AS REJECTA " + "\n");
                strSqlString.Append("             , 0 AS REJECTT " + "\n");
                strSqlString.Append("             , 0 AS BONUSA " + "\n");
                strSqlString.Append("             , 0 AS BONUST " + "\n");
                strSqlString.Append("             , 0 AS TKOUTA " + "\n");
                strSqlString.Append("             , 0 AS TKOUTT " + "\n");
                strSqlString.Append("             , 0 AS EOHA " + "\n");
                strSqlString.Append("             , 0 AS EOHT " + "\n");
                strSqlString.Append("             , 0 AS RES_HA_H2 " + "\n");
                strSqlString.Append("             , 0 AS CHAN_PROD " + "\n");
                strSqlString.Append("             , 0 AS MULT_TRANS " + "\n");
                strSqlString.Append("             , 0 AS RET_H3_H2 " + "\n");
                strSqlString.Append("             , 0 AS RET_H4_H3 " + "\n");
                strSqlString.Append("             , 0 AS SHIP_H3 " + "\n");
                strSqlString.Append("             , 0 AS RECV_H3 " + "\n");
                strSqlString.Append("             , SUM(A.QTY_1) AS RET_CO_H3 " + "\n");
                strSqlString.Append("             , 0 AS RET_H3_CO " + "\n");
                strSqlString.Append("             , 0 AS SHIP_H4 " + "\n");
                strSqlString.Append("             , 0 AS RET_CO_H4 " + "\n");
                strSqlString.Append("             , 0 AS RET_H4 " + "\n");
                strSqlString.Append("             , 0 AS DIFFER " + "\n");
                strSqlString.Append("          FROM RWIPLOTHIS A " + "\n");
                strSqlString.Append("         WHERE A.FACTORY = '" + GlobalVariable.gsAssyDefaultFactory + "' " + "\n");
                strSqlString.Append("           AND A.OLD_FACTORY = '" + GlobalVariable.gsAssyDefaultFactory + "' " + "\n");
                strSqlString.Append("           AND A.OPER = 'AZ010' " + "\n");
                strSqlString.Append("           AND A.OLD_OPER = 'AZ010' " + "\n");
                strSqlString.Append("           AND A.TRAN_CODE = 'CREATE' " + "\n");
                strSqlString.Append("           AND A.LOT_TYPE = 'W' " + "\n");
                strSqlString.Append("           AND A.OWNER_CODE = 'PROD' " + "\n");
                strSqlString.Append("           AND A.HIST_DEL_FLAG <> 'Y' " + "\n");
                strSqlString.AppendFormat("           AND A.LOT_CMF_5 LIKE '{0}%' " + "\n", txtLotType.Text.Trim());
                strSqlString.AppendFormat("           AND A.MAT_ID LIKE '{0}%' " + "\n", txtProduct.Text.Trim());
                strSqlString.AppendFormat("           AND A.TRAN_TIME BETWEEN '{0}' AND '{1}' " + "\n", strFromDate, strToDate);
                strSqlString.Append("         GROUP BY A.MAT_ID " + "\n");
                strSqlString.Append("        UNION ALL " + "\n");
                /********************************************************************************************************************/
                /* RETURN_HMK3_CUSTOMER : ASSY, TEST FACTORY를 이동하는 제품의 HMK3 STOCK에서 RETURN된 수량을 조회한다.             */
                strSqlString.Append("        SELECT A.MAT_ID AS MAT_ID " + "\n");
                strSqlString.Append("             , 'HMK3' AS AREA " + "\n");
                strSqlString.Append("             , 0 AS BOHA " + "\n");
                strSqlString.Append("             , 0 AS BOHT " + "\n");
                strSqlString.Append("             , 0 AS TKINA " + "\n");
                strSqlString.Append("             , 0 AS TKINT " + "\n");
                strSqlString.Append("             , 0 AS REJECTA " + "\n");
                strSqlString.Append("             , 0 AS REJECTT " + "\n");
                strSqlString.Append("             , 0 AS BONUSA " + "\n");
                strSqlString.Append("             , 0 AS BONUST " + "\n");
                strSqlString.Append("             , 0 AS TKOUTA " + "\n");
                strSqlString.Append("             , 0 AS TKOUTT " + "\n");
                strSqlString.Append("             , 0 AS EOHA " + "\n");
                strSqlString.Append("             , 0 AS EOHT " + "\n");
                strSqlString.Append("             , 0 AS RES_HA_H2 " + "\n");
                strSqlString.Append("             , 0 AS CHAN_PROD " + "\n");
                strSqlString.Append("             , 0 AS MULT_TRANS " + "\n");
                strSqlString.Append("             , 0 AS RET_H3_H2 " + "\n");
                strSqlString.Append("             , 0 AS RET_H4_H3 " + "\n");
                strSqlString.Append("             , 0 AS SHIP_H3 " + "\n");
                strSqlString.Append("             , 0 AS RECV_H3 " + "\n");
                strSqlString.Append("             , 0 AS RET_CO_H3 " + "\n");
                strSqlString.Append("             , SUM(A.QTY_1) AS RET_H3_CO " + "\n");
                strSqlString.Append("             , 0 AS SHIP_H4 " + "\n");
                strSqlString.Append("             , 0 AS RET_CO_H4 " + "\n");
                strSqlString.Append("             , 0 AS RET_H4 " + "\n");
                strSqlString.Append("             , 0 AS DIFFER " + "\n");
                strSqlString.Append("          FROM RWIPLOTHIS A " + "\n");
                strSqlString.Append("         WHERE A.FACTORY = 'CUSTOMER' " + "\n");
                strSqlString.Append("           AND A.OLD_OPER = 'T0000' " + "\n");
                strSqlString.Append("           AND A.TRAN_CODE = 'SHIP' " + "\n");
                strSqlString.Append("           AND A.LOT_TYPE = 'W' " + "\n");
                strSqlString.Append("           AND A.OWNER_CODE = 'PROD' " + "\n");
                strSqlString.Append("           AND TRIM(A.LOT_CMF_7) <> 'HM' " + "\n");
                strSqlString.AppendFormat("           AND A.LOT_CMF_5 LIKE '{0}%' " + "\n", txtLotType.Text.Trim());
                strSqlString.AppendFormat("           AND A.MAT_ID LIKE '{0}%' " + "\n", txtProduct.Text.Trim());
                strSqlString.AppendFormat("           AND A.TRAN_TIME BETWEEN '{0}' AND '{1}' " + "\n", strFromDate, strToDate);
                strSqlString.Append("         GROUP BY A.MAT_ID " + "\n");
                strSqlString.Append("        UNION ALL " + "\n");
                /********************************************************************************************************************/
                /* SHIP_HMK4 : ASSY, TEST FACTORY, TEST ONLY를 이동하는 제품의 HMK4 STOCK에서 SHIP된 수량을 조회한다.               */
                strSqlString.Append("        SELECT A.MAT_ID AS MAT_ID " + "\n");
                strSqlString.Append("             , 'HMK4' AS AREA " + "\n");
                strSqlString.Append("             , 0 AS BOHA " + "\n");
                strSqlString.Append("             , 0 AS BOHT " + "\n");
                strSqlString.Append("             , 0 AS TKINA " + "\n");
                strSqlString.Append("             , 0 AS TKINT " + "\n");
                strSqlString.Append("             , 0 AS REJECTA " + "\n");
                strSqlString.Append("             , 0 AS REJECTT " + "\n");
                strSqlString.Append("             , 0 AS BONUSA " + "\n");
                strSqlString.Append("             , 0 AS BONUST " + "\n");
                strSqlString.Append("             , 0 AS TKOUTA " + "\n");
                strSqlString.Append("             , 0 AS TKOUTT " + "\n");
                strSqlString.Append("             , 0 AS EOHA " + "\n");
                strSqlString.Append("             , 0 AS EOHT " + "\n");
                strSqlString.Append("             , 0 AS RES_HA_H2 " + "\n");
                strSqlString.Append("             , 0 AS CHAN_PROD " + "\n");
                strSqlString.Append("             , 0 AS MULT_TRANS " + "\n");
                strSqlString.Append("             , 0 AS RET_H3_H2 " + "\n");
                strSqlString.Append("             , 0 AS RET_H4_H3 " + "\n");
                strSqlString.Append("             , 0 AS SHIP_H3 " + "\n");
                strSqlString.Append("             , 0 AS RECV_H3 " + "\n");
                strSqlString.Append("             , 0 AS RET_CO_H3 " + "\n");
                strSqlString.Append("             , 0 AS RET_H3_CO " + "\n");
                strSqlString.Append("             , SUM(A.SHIP_H4) AS SHIP_H4 " + "\n");
                strSqlString.Append("             , 0 AS RET_CO_H4 " + "\n");
                strSqlString.Append("             , 0 AS RET_H4 " + "\n");
                strSqlString.Append("             , 0 AS DIFFER " + "\n");
                strSqlString.Append("          FROM ( " + "\n");
                /* 정상적으로 SHPPING된 수량을 조회한다.    */
                strSqlString.Append("                SELECT A.MAT_ID " + "\n");
                strSqlString.Append("                      , SUM(A.QTY_1) AS SHIP_H4 " + "\n");
                strSqlString.Append("                  FROM RWIPLOTHIS A " + "\n");
                strSqlString.Append("                 WHERE A.FACTORY IN ('CUSTOMER', 'FGS') " + "\n");
                strSqlString.Append("                   AND A.OLD_FACTORY = '" + GlobalVariable.gsTestDefaultFactory + "' " + "\n");
                strSqlString.Append("                   AND A.OPER IN (' ', 'F000N') " + "\n");
                strSqlString.Append("                   AND A.OLD_OPER = 'TZ010' " + "\n");
                strSqlString.Append("                   AND A.TRAN_CODE = 'SHIP' " + "\n");
                strSqlString.Append("                   AND A.LOT_TYPE = 'W' " + "\n");
                strSqlString.Append("                   AND A.OWNER_CODE = 'PROD' " + "\n");
                strSqlString.Append("                   AND A.HIST_DEL_FLAG <> 'Y' " + "\n");
                strSqlString.AppendFormat("                   AND A.LOT_CMF_5 LIKE '{0}%' " + "\n", txtLotType.Text.Trim());
                strSqlString.AppendFormat("                   AND A.MAT_ID LIKE '{0}%' " + "\n", txtProduct.Text.Trim());
                strSqlString.AppendFormat("                   AND A.TRAN_TIME BETWEEN '{0}' AND '{1}' " + "\n", strFromDate, strToDate);
                strSqlString.Append("                 GROUP BY A.MAT_ID " + "\n");
                strSqlString.Append("                UNION ALL " + "\n");
                /* 부분 SHPPING된 수량을 조회한다.  */
                strSqlString.Append("                SELECT A.MAT_ID " + "\n");
                strSqlString.Append("                     , SUM(A.OLD_QTY_1 - A.QTY_1) AS SHIP_H4 " + "\n");
                strSqlString.Append("                  FROM RWIPLOTHIS A " + "\n");
                strSqlString.Append("                 WHERE A.FACTORY = '" + GlobalVariable.gsTestDefaultFactory + "' " + "\n");
                strSqlString.Append("                   AND A.OLD_FACTORY = '" + GlobalVariable.gsTestDefaultFactory + "' " + "\n");
                strSqlString.Append("                   AND A.OPER = 'TZ010' " + "\n");
                strSqlString.Append("                   AND A.OLD_OPER = 'TZ010' " + "\n");
                strSqlString.Append("                   AND A.TRAN_CODE = 'SHIP' " + "\n");
                strSqlString.Append("                   AND A.LOT_TYPE = 'W' " + "\n");
                strSqlString.Append("                   AND A.OWNER_CODE = 'PROD' " + "\n");
                strSqlString.Append("                   AND A.HIST_DEL_FLAG <> 'Y' " + "\n");
                strSqlString.AppendFormat("                   AND A.LOT_CMF_5 LIKE '{0}%' " + "\n", txtLotType.Text.Trim());
                strSqlString.AppendFormat("                   AND A.MAT_ID LIKE '{0}%' " + "\n", txtProduct.Text.Trim());
                strSqlString.AppendFormat("                   AND A.TRAN_TIME BETWEEN '{0}' AND '{1}' " + "\n", strFromDate, strToDate);
                strSqlString.Append("                 GROUP BY A.MAT_ID " + "\n");
                strSqlString.Append("               ) A " + "\n");
                strSqlString.Append("         GROUP BY A.MAT_ID " + "\n");
                strSqlString.Append("        UNION ALL " + "\n");
                /********************************************************************************************************************/
                /* RETURN_CUSTOMER_HMK4 : ASSY, TEST FACTORY, TEST ONLY를 이동하는 제품의 HMK4 STOCK에서 RETURN된 수량을 조회한다.  */
                strSqlString.Append("        SELECT A.MAT_ID AS MAT_ID " + "\n");
                strSqlString.Append("             , 'HMK4' AS AREA " + "\n");
                strSqlString.Append("             , 0 AS BOHA " + "\n");
                strSqlString.Append("             , 0 AS BOHT " + "\n");
                strSqlString.Append("             , 0 AS TKINA " + "\n");
                strSqlString.Append("             , 0 AS TKINT " + "\n");
                strSqlString.Append("             , 0 AS REJECTA " + "\n");
                strSqlString.Append("             , 0 AS REJECTT " + "\n");
                strSqlString.Append("             , 0 AS BONUSA " + "\n");
                strSqlString.Append("             , 0 AS BONUST " + "\n");
                strSqlString.Append("             , 0 AS TKOUTA " + "\n");
                strSqlString.Append("             , 0 AS TKOUTT " + "\n");
                strSqlString.Append("             , 0 AS EOHA " + "\n");
                strSqlString.Append("             , 0 AS EOHT " + "\n");
                strSqlString.Append("             , 0 AS RES_HA_H2 " + "\n");
                strSqlString.Append("             , 0 AS CHAN_PROD " + "\n");
                strSqlString.Append("             , 0 AS MULT_TRANS " + "\n");
                strSqlString.Append("             , 0 AS RET_H3_H2 " + "\n");
                strSqlString.Append("             , 0 AS RET_H4_H3 " + "\n");
                strSqlString.Append("             , 0 AS SHIP_H3 " + "\n");
                strSqlString.Append("             , 0 AS RECV_H3 " + "\n");
                strSqlString.Append("             , 0 AS RET_CO_H3 " + "\n");
                strSqlString.Append("             , 0 AS RET_H3_CO " + "\n");
                strSqlString.Append("             , 0 AS SHIP_H4 " + "\n");
                strSqlString.Append("             , SUM(A.QTY_1) AS RET_CO_H4 " + "\n");
                strSqlString.Append("             , 0 AS RET_H4 " + "\n");
                strSqlString.Append("             , 0 AS DIFFER " + "\n");
                strSqlString.Append("          FROM RWIPLOTHIS A " + "\n");
                strSqlString.Append("         WHERE A.FACTORY = '" + GlobalVariable.gsTestDefaultFactory + "' " + "\n");
                strSqlString.Append("           AND A.OLD_FACTORY = '" + GlobalVariable.gsTestDefaultFactory + "' " + "\n");
                strSqlString.Append("           AND A.OPER = 'TZ010' " + "\n");
                strSqlString.Append("           AND A.OLD_OPER = 'TZ010' " + "\n");
                strSqlString.Append("           AND A.TRAN_CODE = 'CREATE' " + "\n");
                strSqlString.Append("           AND A.LOT_TYPE = 'W' " + "\n");
                strSqlString.Append("           AND A.OWNER_CODE = 'PROD' " + "\n");
                strSqlString.Append("           AND A.HIST_DEL_FLAG <> 'Y' " + "\n");
                strSqlString.AppendFormat("           AND A.LOT_CMF_5 LIKE '{0}%' " + "\n", txtLotType.Text.Trim());
                strSqlString.AppendFormat("           AND A.MAT_ID LIKE '{0}%' " + "\n", txtProduct.Text.Trim());
                strSqlString.AppendFormat("           AND A.TRAN_TIME BETWEEN '{0}' AND '{1}' " + "\n", strFromDate, strToDate);
                strSqlString.Append("         GROUP BY A.MAT_ID " + "\n");
                strSqlString.Append("        ) A, (SELECT DISTINCT MAT_ID AS MAT_ID " + "\n");
                strSqlString.Append("                   , MAT_TYPE AS MAT_TYPE " + "\n");
                strSqlString.Append("                   , DELETE_FLAG AS DELETE_FLAG " + "\n");
                strSqlString.Append("                   , DECODE(MAT_GRP_1, ' ', '-', MAT_GRP_1) AS MAT_GRP_1 " + "\n");
                strSqlString.Append("                   , DECODE(MAT_GRP_2, ' ', '-', MAT_GRP_2) AS MAT_GRP_2 " + "\n");
                strSqlString.Append("                   , DECODE(MAT_GRP_3, ' ', '-', MAT_GRP_3) AS MAT_GRP_3 " + "\n");
                strSqlString.Append("                   , DECODE(MAT_GRP_4, ' ', '-', MAT_GRP_4) AS MAT_GRP_4 " + "\n");
                strSqlString.Append("                   , DECODE(MAT_GRP_5, ' ', '-', MAT_GRP_5) AS MAT_GRP_5 " + "\n");
                strSqlString.Append("                   , DECODE(MAT_GRP_6, ' ', '-', MAT_GRP_6) AS MAT_GRP_6 " + "\n");
                strSqlString.Append("                   , DECODE(MAT_GRP_7, ' ', '-', MAT_GRP_7) AS MAT_GRP_7 " + "\n");
                strSqlString.Append("                   , DECODE(MAT_GRP_8, ' ', '-', MAT_GRP_8) AS MAT_GRP_8 " + "\n");
                strSqlString.Append("                   , DECODE(MAT_CMF_10, ' ', '-', MAT_CMF_10) AS MAT_CMF_10 " + "\n");
                strSqlString.Append("                FROM MWIPMATDEF " + "\n");
                strSqlString.Append("               WHERE MAT_TYPE = 'FG' " + "\n");
                strSqlString.Append("                 AND DELETE_FLAG <> 'Y') B " + "\n");
                strSqlString.Append(" WHERE A.MAT_ID = B.MAT_ID " + "\n");
                strSqlString.Append("   AND B.MAT_TYPE = 'FG' " + "\n");
                strSqlString.Append("   AND B.DELETE_FLAG <> 'Y' " + "\n");
                strSqlString.Append("   AND A.AREA <> ' ' " + "\n");
                //상세 조회에 따른 SQL문 생성
                /********************************************************************************************************************/
                if (udcWIPCondition1.Text != "ALL" && udcWIPCondition1.Text != "")
                    strSqlString.AppendFormat("   AND B.MAT_GRP_1 {0} " + "\n", udcWIPCondition1.SelectedValueToQueryString);
                if (udcWIPCondition2.Text != "ALL" && udcWIPCondition2.Text != "")
                    strSqlString.AppendFormat("   AND B.MAT_GRP_2 {0} " + "\n", udcWIPCondition2.SelectedValueToQueryString);
                if (udcWIPCondition3.Text != "ALL" && udcWIPCondition3.Text != "")
                    strSqlString.AppendFormat("   AND B.MAT_GRP_3 {0} " + "\n", udcWIPCondition3.SelectedValueToQueryString);
                if (udcWIPCondition4.Text != "ALL" && udcWIPCondition4.Text != "")
                    strSqlString.AppendFormat("   AND B.MAT_GRP_4 {0} " + "\n", udcWIPCondition4.SelectedValueToQueryString);
                if (udcWIPCondition5.Text != "ALL" && udcWIPCondition5.Text != "")
                    strSqlString.AppendFormat("   AND B.MAT_GRP_5 {0} " + "\n", udcWIPCondition5.SelectedValueToQueryString);
                if (udcWIPCondition6.Text != "ALL" && udcWIPCondition6.Text != "")
                    strSqlString.AppendFormat("   AND B.MAT_GRP_6 {0} " + "\n", udcWIPCondition6.SelectedValueToQueryString);
                if (udcWIPCondition7.Text != "ALL" && udcWIPCondition7.Text != "")
                    strSqlString.AppendFormat("   AND B.MAT_GRP_7 {0} " + "\n", udcWIPCondition7.SelectedValueToQueryString);
                if (udcWIPCondition8.Text != "ALL" && udcWIPCondition8.Text != "")
                    strSqlString.AppendFormat("   AND B.MAT_GRP_8 {0} " + "\n", udcWIPCondition8.SelectedValueToQueryString);
                /********************************************************************************************************************/
                strSqlString.AppendFormat(" GROUP BY {0}, A.AREA " + "\n", QueryCond1);
                strSqlString.AppendFormat(" ORDER BY {0}, DECODE(A.AREA, 'HMK2', 1, 'D/A', 2, 'BLAB', 3, 'BOND', 4, 'MOLD', 5, 'FINI', 6, 'HMK3', 7, 'TEST', 8, 'HMK4', 9, 'HMK5', 10) " + "\n", QueryCond1);

                if (GlobalVariable.gsUserGroup == "ADMIN_GROUP" || GlobalVariable.gsUserGroup == "HANA_ADMIN_GROUP")
                {
                    System.Windows.Forms.Clipboard.SetText(strSqlString.ToString());
                }

                return strSqlString.ToString();
            }
            catch (Exception ex)
            {
                LoadingPopUp.LoadingPopUpHidden();
                CmnFunction.ShowMsgBox(ex.Message);
                return "";
            }
        }

        #endregion


        #region " MakeChart : Chart 처리 "

        /// <summary>
        /// 5. Chart 생성
        /// </summary>
        /// <param name="DT">Chart를 생성할 데이터 테이블</param>
        private void MakeChart(DataTable DT)
        {

        }

        #endregion


        #region " Form Control Event "

        private void btnView_Click(object sender, EventArgs e)
        {
            string strSqlString = "";
            int iSubTotalCount = 2;
            DataTable dt = null;
            if (CheckField() == false) return;

            try
            {
                // 검색중 화면 표시
                LoadingPopUp.LoadIngPopUpShow(this);
                this.Refresh();

                // 2009-03-24-정비재 : Sheet의 Title를 동적으로 표시하기 위한 함수
                SortInit();
                GridColumnInit();

                // 김준용 (2009.08.01 주석 처리 - Controller 연결 오류)
                //// Query문으로 데이터를 검색한다.
                //if (rbBaseTime01.Checked == true)
                //{
                //    // 과거기준
                //    strSqlString = MakeSqlString_과거기준();
                //}
                //else if (rbBaseTime02.Checked == true)
                //{
                //    // 현재기준
                //    strSqlString = MakeSqlString_현재기준();
                //}

                // Query문으로 데이터를 검색한다.
                if (rbSearchGubn01.Checked == true)
                {
                    // 과거기준
                    strSqlString = MakeSqlString_과거기준();
                }
                else if (rbSearchGubn02.Checked == true)
                {
                    // 현재기준
                    strSqlString = MakeSqlString_현재기준();
                }

                if (strSqlString.Trim() == "")
                {
                    return;
                }

                dt = CmnFunction.oComm.GetFuncDataTable("DYNAMIC", strSqlString);

                if (dt.Rows.Count == 0)
                {
                    dt.Dispose();
                    LoadingPopUp.LoadingPopUpHidden();
                    CmnFunction.ShowMsgBox(RptMessages.GetMessage("STD010", GlobalVariable.gcLanguage));
                    return;
                }

                //1.Griid 합계 표시
                // FAMILY
                if (udcWIPCondition2.Text != "ALL" && udcWIPCondition2.Text != "")
                {
                    iSubTotalCount += 1;
                }
                // PACKAGE
                if (udcWIPCondition3.Text != "ALL" && udcWIPCondition3.Text != "")
                {
                    iSubTotalCount += 1;
                }
                // TYPE1
                if (udcWIPCondition4.Text != "ALL" && udcWIPCondition4.Text != "")
                {
                    iSubTotalCount += 1;
                }
                // TYPE2
                if (udcWIPCondition5.Text != "ALL" && udcWIPCondition5.Text != "")
                {
                    iSubTotalCount += 1;
                }
                // LEAD COUNT
                if (udcWIPCondition6.Text != "ALL" && udcWIPCondition6.Text != "")
                {
                    iSubTotalCount += 1;
                }
                // DENSITY
                if (udcWIPCondition7.Text != "ALL" && udcWIPCondition7.Text != "")
                {
                    iSubTotalCount += 1;
                }
                // GENERATION
                if (udcWIPCondition8.Text != "ALL" && udcWIPCondition8.Text != "")
                {
                    iSubTotalCount += 1;
                }

                int[] rowType = spdData.RPT_DataBindingWithSubTotal(dt, 0, iSubTotalCount, 1, null, null);
                spdData.RPT_FillDataSelectiveCells("Total", 0, iSubTotalCount, 0, 1, true, Align.Center, VerticalAlign.Center);

                //4. Column Auto Fit
                spdData.RPT_AutoFit(false);

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

        private void btnExcelExport_Click(object sender, EventArgs e)
        {
            /****************************************************/
            /* Comment : Excel로 내보내기 버튼을 클릭하면       */
            /*                                                  */
            /* Created By : bee-jae jung (2009-02-24)           */
            /*                                                  */
            /* Modified By : bee-jae jung (2009-02-24)          */
            /****************************************************/
            try
            {
                Cursor.Current = Cursors.WaitCursor;

                spdData.ExportExcel();
            }
            catch (Exception ex)
            {
                CmnFunction.ShowMsgBox(ex.Message);
            }
            finally
            {
                Cursor.Current = Cursors.Default;
            }
        }

        private void txtProduct_TextChanged(object sender, EventArgs e)
        {
            /****************************************************/
            /* Comment : 입력한 제품코드를 대문자로 변경한다.   */
            /*                                                  */
            /* Created By : bee-jae jung (2009-06-05-금요일)    */
            /*                                                  */
            /* Modified By : bee-jae jung (2009-06-05-금요일)   */
            /****************************************************/
            try
            {
                Cursor.Current = Cursors.WaitCursor;

                txtProduct.Text = txtProduct.Text.ToUpper();
                if (txtProduct.Text.IndexOf("%") > 0)
                {
                    txtProduct.SelectionStart = txtProduct.Text.Length - 1;
                }
                else
                {
                    txtProduct.SelectionStart = txtProduct.Text.Length;
                }
            }
            catch (Exception ex)
            {
                CmnFunction.ShowMsgBox(ex.Message);
            }
            finally
            {
                Cursor.Current = Cursors.Default;
            }
        }

        private void rbSearchGubn_CheckedChanged(object sender, EventArgs e)
        {
            /****************************************************/
            /* Comment : 검색기준(과거/현재)를 선택할 때.       */
            /*                                                  */
            /* Created By : bee-jae jung (2009-06-05-금요일)    */
            /*                                                  */
            /* Modified By : bee-jae jung (2009-06-05-금요일)   */
            /****************************************************/
            try
            {
                Cursor.Current = Cursors.WaitCursor;

                if (rbSearchGubn01.Checked == true)
                {
                    // 과거기준
                    cbCutoff_DT.Visible = true;
                    cdvFromToDate.Visible = false;
                    gbBaseGubn.Visible = false;
                }
                else if (rbSearchGubn02.Checked == true)
                {
                    // 현재기준
                    cbCutoff_DT.Visible = false;
                    cdvFromToDate.Visible = true;
                    gbBaseGubn.Visible = true;
                }
            }
            catch (Exception ex)
            {
                CmnFunction.ShowMsgBox(ex.Message);
            }
            finally
            {
                Cursor.Current = Cursors.Default;
            }
        }

        #endregion

        
    }
}
