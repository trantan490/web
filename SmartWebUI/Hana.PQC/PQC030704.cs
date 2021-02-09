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
    public partial class PQC030704 : Miracom.SmartWeb.UI.Controls.udcCUSReport002
    {

        #region " PQC030704 : Program Initial "

        /// <summary>
        /// 클  래  스: PQC030704<br/>
        /// 클래스요약: 수입검사 주보 관리<br/>
        /// 작  성  자: 하나마이크론 임종우<br/>
        /// 최초작성일: 2010-01-20<br/>
        /// 상세  설명: 수입검사 주보 관리 화면<br/>
        /// 2010-03-09-임종우 : 접수 구분 조회 추가

        /// </summary>
        /// 
        public PQC030704()
        {
            InitializeComponent();
            cdvFromToDate.AutoBinding(DateTime.Today.ToString(), DateTime.Today.ToString());
            cdvFromToDate.DaySelector.Visible = false;
            SortInit();
            GridColumnInit();
            this.SetFactory(GlobalVariable.gsAssyDefaultFactory);
            cdvFactory.Text = GlobalVariable.gsAssyDefaultFactory;
            this.cdvMatType.sFactory = GlobalVariable.gsAssyDefaultFactory;
            this.cdvChar.sFactory = GlobalVariable.gsAssyDefaultFactory;
            this.cdvRcvType.sFactory = GlobalVariable.gsAssyDefaultFactory;
        }

        #endregion


        #region " Function Definition "

        /// <summary 1. 유효성 검사>
        /// <remarks></remarks>
        /// </summary>
        /// <returns></returns>
        private Boolean CheckField()
        {
            if (cdvChar.Text == "ALL" || cdvChar.SelectCount > 10)
            {
                CmnFunction.ShowMsgBox(RptMessages.GetMessage("STD047", GlobalVariable.gcLanguage));
                return false;
            }

            return true;
        }

        #endregion


        #region " GridColumnInit : Sheet Title 설정 "

        /// <summary>
        /// 2. 헤더 생성
        /// </summary>
        private void GridColumnInit()
        {
            try
            {
                spdData.RPT_ColumnInit();


                spdData.RPT_AddBasicColumn("Material Type", 0, 0, Visibles.True, Frozen.True, Align.Center, Merge.False, Formatter.String, 60);
                spdData.RPT_AddBasicColumn("IQC NO", 0, 1, Visibles.True, Frozen.True, Align.Center, Merge.False, Formatter.String, 100);
                spdData.RPT_AddBasicColumn("Warehousing date", 0, 2, Visibles.True, Frozen.False, Align.Center, Merge.False, Formatter.String, 70);
                spdData.RPT_AddBasicColumn("Reception date", 0, 3, Visibles.True, Frozen.False, Align.Center, Merge.False, Formatter.String, 70);
                spdData.RPT_AddBasicColumn("Import inspection date", 0, 4, Visibles.True, Frozen.False, Align.Center, Merge.False, Formatter.String, 70);
                spdData.RPT_AddBasicColumn("Vendor description", 0, 5, Visibles.True, Frozen.False, Align.Left, Merge.False, Formatter.String, 70);
                spdData.RPT_AddBasicColumn("Standard", 0, 6, Visibles.True, Frozen.False, Align.Left, Merge.False, Formatter.String, 100);
                spdData.RPT_AddBasicColumn("LOT ID", 0, 7, Visibles.True, Frozen.False, Align.Left, Merge.False, Formatter.String, 100);
                spdData.RPT_AddBasicColumn("Warehousing quantity", 0, 8, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 70);
                spdData.RPT_AddBasicColumn("Inspection level", 0, 9, Visibles.True, Frozen.False, Align.Left, Merge.False, Formatter.String, 70);
                spdData.RPT_AddBasicColumn("sample amount", 0, 10, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 70);
                spdData.RPT_AddBasicColumn("Reports", 0, 11, Visibles.True, Frozen.False, Align.Center, Merge.False, Formatter.String, 50);
                spdData.RPT_AddBasicColumn("surface", 0, 12, Visibles.True, Frozen.False, Align.Center, Merge.False, Formatter.String, 50);
                spdData.RPT_AddBasicColumn("measurement", 0, 13, Visibles.True, Frozen.False, Align.Center, Merge.False, Formatter.String, 50);
                spdData.RPT_AddBasicColumn("characteristic", 0, 14, Visibles.True, Frozen.False, Align.Center, Merge.False, Formatter.String, 50);

                if(cdvFactory.Text != "")
                {
                    spdData.RPT_AddDynamicColumn(cdvChar, 0, 15, Visibles.True, Frozen.False, Align.Center, Merge.False, Formatter.String, 50);
                }

                spdData.RPT_AddBasicColumn("Final decision", 0, spdData.ActiveSheet.Columns.Count, Visibles.True, Frozen.False, Align.Center, Merge.False, Formatter.String, 50);
                
                // Group항목이 있을경우 반드시 선언해줄것.
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
            //((udcTableForm)(this.btnSort.BindingForm)).Clear();
            //((udcTableForm)(this.btnSort.BindingForm)).AddRow("Vendor description", "VENDOR", "VENDOR", true);
            //((udcTableForm)(this.btnSort.BindingForm)).AddRow("Material classification", "MAT_TYPE", "MAT_TYPE", true);
            //((udcTableForm)(this.btnSort.BindingForm)).AddRow("Model", "MODEL", "MODEL", false);
            //((udcTableForm)(this.btnSort.BindingForm)).AddRow("Standard", "MAT_DESC", "MAT_DESC", false);
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
            string QueryCond2 = null;
            string strDecode = null;
            string strSelect = null;
            string strItem = null;
            //string strFromDate = null, strToDate = null;

            //strFromDate = cdvStartDate.SelectedValue() + "220000";
            //strToDate = cdvEndDate.SelectedValue() + "215959";

            //추가
            udcTableForm tableForm = (udcTableForm)btnSort.BindingForm;

            QueryCond1 = tableForm.SelectedValueToQueryContainNull;
            QueryCond2 = tableForm.SelectedValue2ToQueryContainNull;

            ListView character = cdvChar.getSelectedItems() ;

            for (int i = 0; i < character.Items.Count; i++)
            {
                int strFind = character.Items[i].Text.IndexOf("'");
                if (strFind > -1)
                {
                    character.Items[i].Text = character.Items[i].Text.Replace("'", "''");
                }

                strDecode += "             , MAX(DECODE(CHAR_ID, '" + character.Items[i].Text + "', VALUE_1)) AS C" + i + " \n";
                strItem += "'" + character.Items[i].Text + "',";
            }

            strItem = strItem.Substring(0, strItem.Length - 1);
            // 쿼리
            strSelect += cdvChar.getRepeatQuery("EDC.C","AS", "C").Replace(", EDC.C", "     , EDC.C") ;
            //strDecode += cdvChar.getDecodeQuery("DECODE(CHAR_ID,", "VALUE_1)", "C").Replace(", DECODE", "     , DECODE");
            
            strSqlString.Append("SELECT REQ.MAT_TYPE" + "\n");
            strSqlString.Append("     , BAT.IQC_NO" + "\n");
            strSqlString.Append("     , TO_CHAR(TO_DATE(REQ.RCV_DATE, 'YYYYMMDDHH24MISS'), 'YYYY-MM-DD') AS RCV_DATE" + "\n");
            strSqlString.Append("     , TO_CHAR(TO_DATE(BAT.CREATE_TIME, 'YYYYMMDDHH24MISS'), 'YYYY-MM-DD') AS CREATE_TIME" + "\n");
            strSqlString.Append("     , DECODE(BAT.UPDATE_TIME,' ','',TO_CHAR(TO_DATE(BAT.UPDATE_TIME, 'YYYYMMDDHH24MISS'), 'YYYY-MM-DD')) AS FINAL_DATE" + "\n");
            strSqlString.Append("     , REQ.VENODR" + "\n");
            strSqlString.Append("     , MAT.MODEL" + "\n");
            strSqlString.Append("     , REQ.LOT_LIST" + "\n");
            strSqlString.Append("     , REQ.RCV_QTY" + "\n");
            strSqlString.Append("     , BAT.SAMPLE_CHAR" + "\n");
            strSqlString.Append("     , BAT.RESV_FIELD_1 AS SAMPLE_QTY" + "\n");
            strSqlString.Append("     , BAT.DOC_PASS_FLAG" + "\n");
            strSqlString.Append("     , BAT.VIS_PASS_FLAG" + "\n");
            strSqlString.Append("     , BAT.FUNC_PASS_FLAG" + "\n");
            strSqlString.Append("     , BAT.QC_PASS_FLAG" + "\n");            
            strSqlString.Append(strSelect);
            strSqlString.Append("     , BAT.FINAL_DECISION" + "\n");

            strSqlString.Append("  FROM CIQCBATSTS BAT" + "\n");
            strSqlString.Append("     , (SELECT MAT_ID, LEAD || PKG || ' ' || NORM AS MODEL FROM CWIPMATDEF@RPTTOMES WHERE FACTORY = '" + GlobalVariable.gsAssyDefaultFactory + "') MAT" + "\n");
            strSqlString.Append("     , (" + "\n");
            strSqlString.Append("        SELECT REQ.RESV_FIELD_8 AS IQC_NO" + "\n");
            strSqlString.Append("             , REQ.VENDOR_CODE" + "\n");
            strSqlString.Append("             , REQ.RESV_FIELD_3 AS MAT_TYPE" + "\n");
            strSqlString.Append("             , REQ.RESV_FIELD_7 AS VENODR" + "\n");
            strSqlString.Append("             , REQ_LOT.CREATE_TIME AS RCV_DATE" + "\n");
            strSqlString.Append("             , DECODE(COUNT(REQ.LOT_ID), 1, REQ_LOT.LOT_ID, REQ_LOT.LOT_ID || ' 외 ' || TO_CHAR(COUNT(REQ.LOT_ID) - 1) || '건') AS LOT_LIST" + "\n");
            strSqlString.Append("             , SUM(REQ.QTY_1) AS RCV_QTY" + "\n");
            strSqlString.Append("             , COUNT(REQ.LOT_ID)" + "\n");
            strSqlString.Append("          FROM CIQCLOTREQ@RPTTOMES REQ" + "\n");
            strSqlString.Append("             , (" + "\n");
            strSqlString.Append("                 SELECT RESV_FIELD_8,LOT_ID,CREATE_TIME" + "\n");
            strSqlString.Append("                   FROM (" + "\n");
            strSqlString.Append("                          SELECT RESV_FIELD_8,LOT_ID,CREATE_TIME,ROW_NUMBER() OVER(PARTITION BY RESV_FIELD_8 ORDER BY LOT_ID) AS RN " + "\n");
            strSqlString.Append("                            FROM CIQCLOTREQ@RPTTOMES" + "\n");
            strSqlString.Append("                        )" + "\n");
            strSqlString.Append("                  WHERE RN = 1" + "\n");
            strSqlString.Append("                ) REQ_LOT" + "\n");
            strSqlString.Append("         WHERE REQ.RESV_FIELD_8 = REQ_LOT.RESV_FIELD_8" + "\n");
            strSqlString.Append("           AND REQ.RESV_FIELD_3 NOT IN ('GW','SB')" + "\n");
            strSqlString.Append("         GROUP BY REQ.RESV_FIELD_8, REQ.VENDOR_CODE, REQ.RESV_FIELD_3, REQ.RESV_FIELD_7,REQ_LOT.CREATE_TIME,REQ_LOT.LOT_ID" + "\n");
            strSqlString.Append("       ) REQ" + "\n");
            strSqlString.Append("      ,(" + "\n");
            
            // IQC별 해당 EDC DATA 가져오기
            strSqlString.Append("        SELECT IQC_NO" + "\n");

            strSqlString.Append(strDecode);

            strSqlString.Append("          FROM (" + "\n");
            strSqlString.Append("                SELECT BAT.IQC_NO" + "\n");
            strSqlString.Append("                     , STS.LOT_ID" + "\n");
            strSqlString.Append("                     , EDC.COL_SET_ID" + "\n");
            strSqlString.Append("                     , EDC.CHAR_ID" + "\n");
            strSqlString.Append("                     , EDC.VALUE_1" + "\n");
            strSqlString.Append("                  FROM CIQCBATSTS BAT" + "\n");
            strSqlString.Append("                     , ( " + "\n"); // IQC 별 샘플 랏 중 한개 가져오기
            strSqlString.Append("                         SELECT IQC_NO,MAT_TYPE,LOT_ID" + "\n");
            strSqlString.Append("                           FROM (" + "\n");
            strSqlString.Append("                                  SELECT IQC_NO,MAT_TYPE,LOT_ID,ROW_NUMBER() OVER(PARTITION BY IQC_NO ORDER BY LOT_ID) AS RN " + "\n");
            strSqlString.Append("                                    FROM CIQCLOTSTS@RPTTOMES" + "\n");
            strSqlString.Append("                                )" + "\n");
            strSqlString.Append("                          WHERE RN = 1" + "\n");
            strSqlString.Append("                        ) STS" + "\n");
            strSqlString.Append("                      , ( " + "\n"); // 캐릭터별 데이터 가져오기
            strSqlString.Append("                          SELECT LOT_ID, COL_SET_ID, CHAR_ID, VALUE_1" + "\n");
            strSqlString.Append("                            FROM MEDCLOTDAT" + "\n");
            strSqlString.Append("                           WHERE 1=1" + "\n");
            strSqlString.Append("                             AND FACTORY='" + GlobalVariable.gsAssyDefaultFactory + "'" + "\n");
            strSqlString.Append("                             AND CHAR_ID IN (" + strItem + ")" + "\n");
            strSqlString.Append("                             AND FLOW = 'STOCK_FLOW'" + "\n");
            strSqlString.Append("                             AND OPER = '00001'" + "\n");
            strSqlString.Append("                             AND VALUE_1 <> ' '" + "\n");
            strSqlString.Append("                        ) EDC" + "\n");
            strSqlString.Append("                 WHERE 1=1" + "\n");
            strSqlString.Append("                   AND BAT.FINAL_DECISION <> ' '" + "\n");
            strSqlString.Append("                   AND CREATE_TIME BETWEEN '" + cdvFromToDate.ExactFromDate + "' AND '" + cdvFromToDate.ExactToDate + "'" + "\n");
            strSqlString.Append("                   AND BAT.IQC_NO = STS.IQC_NO" + "\n");
            strSqlString.Append("                   AND STS.LOT_ID = EDC.LOT_ID" + "\n");
            strSqlString.Append("                 )" + "\n");
            strSqlString.Append("         GROUP BY IQC_NO " + "\n");
            strSqlString.Append("       ) EDC" + "\n");
            strSqlString.Append(" WHERE 1=1" + "\n");
            strSqlString.Append("   AND BAT.FINAL_DECISION <> ' '" + "\n");
            strSqlString.Append("   AND BAT.IQC_NO = REQ.IQC_NO" + "\n");
            strSqlString.Append("   AND BAT.MAT_ID = MAT.MAT_ID(+)" + "\n");
            strSqlString.Append("   AND BAT.IQC_NO = EDC.IQC_NO(+)" + "\n");
            strSqlString.Append("   AND BAT.CREATE_TIME BETWEEN '" + cdvFromToDate.ExactFromDate + "' AND '" + cdvFromToDate.ExactToDate + "'" + "\n");
            strSqlString.Append("   AND REQ.MAT_TYPE " + cdvMatType.SelectedValueToQueryString + "\n");

            // 2010-03-09-임종우 : 접수 구분 조회 추가
            if (cdvRcvType.Text != "ALL" && cdvRcvType.Text != "")
            {
                strSqlString.Append("   AND BAT.IQC_TYPE " + cdvRcvType.SelectedValueToQueryString + "\n");
            }

            strSqlString.Append(" ORDER BY REQ.MAT_TYPE, BAT.CREATE_TIME " + "\n");

            #region 상세 조회에 따른 SQL문 생성
            //if (udcWIPCondition1.Text != "ALL" && udcWIPCondition1.Text != "")
            //    strSqlString.AppendFormat("   AND MAT.MAT_GRP_1 {0} " + "\n", udcWIPCondition1.SelectedValueToQueryString);

            //if (udcWIPCondition2.Text != "ALL" && udcWIPCondition2.Text != "")
            //    strSqlString.AppendFormat("   AND MAT.MAT_GRP_2 {0} " + "\n", udcWIPCondition2.SelectedValueToQueryString);

            //if (udcWIPCondition3.Text != "ALL" && udcWIPCondition3.Text != "")
            //    strSqlString.AppendFormat("   AND MAT.MAT_GRP_3 {0} " + "\n", udcWIPCondition3.SelectedValueToQueryString);

            //if (udcWIPCondition4.Text != "ALL" && udcWIPCondition4.Text != "")
            //    strSqlString.AppendFormat("   AND MAT.MAT_GRP_4 {0} " + "\n", udcWIPCondition4.SelectedValueToQueryString);

            //if (udcWIPCondition5.Text != "ALL" && udcWIPCondition5.Text != "")
            //    strSqlString.AppendFormat("   AND MAT.MAT_GRP_5 {0} " + "\n", udcWIPCondition5.SelectedValueToQueryString);

            //if (udcWIPCondition6.Text != "ALL" && udcWIPCondition6.Text != "")
            //    strSqlString.AppendFormat("   AND MAT.MAT_GRP_6 {0} " + "\n", udcWIPCondition6.SelectedValueToQueryString);

            //if (udcWIPCondition7.Text != "ALL" && udcWIPCondition7.Text != "")
            //    strSqlString.AppendFormat("   AND MAT.MAT_GRP_7 {0} " + "\n", udcWIPCondition7.SelectedValueToQueryString);

            //if (udcWIPCondition8.Text != "ALL" && udcWIPCondition8.Text != "")
            //    strSqlString.AppendFormat("   AND MAT.MAT_GRP_8 {0} " + "\n", udcWIPCondition8.SelectedValueToQueryString);
            #endregion


            if (GlobalVariable.gsUserGroup == "ADMIN_GROUP" || GlobalVariable.gsUserGroup == "HANA_ADMIN_GROUP")
            {
                System.Windows.Forms.Clipboard.SetText(strSqlString.ToString());
            }

            return strSqlString.ToString();
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

            LoadingPopUp.LoadIngPopUpShow(this);

            Cursor.Current = Cursors.WaitCursor;
            try
            {
                // 검색중 화면 표시
             //   LoadingPopUp.LoadIngPopUpShow(this);
                this.Refresh();

                GridColumnInit();

                // Query문으로 데이터를 검색한다.

                dt = CmnFunction.oComm.GetFuncDataTable("DYNAMIC", MakeSqlString());

                if (dt.Rows.Count == 0)
                {
                    dt.Dispose();
                    LoadingPopUp.LoadingPopUpHidden();
                    CmnFunction.ShowMsgBox(RptMessages.GetMessage("STD010", GlobalVariable.gcLanguage));
                    return;
                }

                spdData.DataSource = dt;

                spdData.isShowZero = true;

                //// Sub Total
                //int[] rowType = spdData.RPT_DataBindingWithSubTotal(dt, 0, 0, 4, null, null);

                //// Total
                //spdData.RPT_FillDataSelectiveCells("Total", 0, 4, 0, 1, true, Align.Center, VerticalAlign.Center);

                ////4. Column Auto Fit
                spdData.RPT_AutoFit(false);

                spdData.ActiveSheet.Columns[0].BackColor = Color.LightYellow;
                spdData.ActiveSheet.Columns[1].BackColor = Color.LightYellow;

                //dt.Dispose();

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

        /// <summary>
        /// Excel Export
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnExcelExport_Click(object sender, EventArgs e)
        {
            Cursor.Current = Cursors.WaitCursor;
            try
            {
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

        #endregion

        private void cdvChar_ValueButtonPress(object sender, EventArgs e)
        {
            //cdvChar.Text = "";
            string strQuery = string.Empty;

            strQuery += "SELECT CHAR_ID Code, '' Data" + "\n";
            strQuery += "  FROM MEDCCHRDEF " + "\n";

            cdvChar.sDynamicQuery = strQuery;

            //for(int i = 0 ; i < cdvChar.ValueItems.Count ; i++)
            //{
            //    if (cdvChar.ValueItems[i].Text == "LF_C of C_Ag PLATING")
            //    {
            //        cdvChar.ValueItems[i].Checked = true;
            //    }
            //}            
        }

        //2010-03-09-임종우 : 접수 구분별 조회하기 위해 추가 함.
        private void cdvRcvType_ValueButtonPress(object sender, EventArgs e)
        {
            string strQuery = string.Empty;

            strQuery += "SELECT DECODE (ROWNUM, 1, A, 2, B, 3, C) AS Code, '' Data" + "\n";
            strQuery += "  FROM ( " + "\n";
            strQuery += "         SELECT 1 " + "\n";
            strQuery += "           FROM DUAL" + "\n";
            strQuery += "        CONNECT BY LEVEL <=3 " + "\n";
            strQuery += "       ) " + "\n";
            strQuery += "     , ( " + "\n";
            strQuery += "         SELECT '양산' AS A " + "\n";
            strQuery += "              , 'QUAL' AS B " + "\n";
            strQuery += "              , 'ER' AS C " + "\n";
            strQuery += "           FROM DUAL" + "\n";
            strQuery += "       ) " + "\n";

            cdvRcvType.sDynamicQuery = strQuery;
        }
    }
}
