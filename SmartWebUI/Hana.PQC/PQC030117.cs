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

namespace Hana.TRN
{
    public partial class PQC030117 : Miracom.SmartWeb.UI.Controls.udcCUSReport002
    {


        /// <summary>
        /// 클  래  스: PQC030117<br/>
        /// 클래스요약: 고객사별 LOSS TOP 10 <br/>
        /// 작  성  자: 김민우<br/>
        /// 최초작성일: 2010-10-15<br/>
        /// 상세  설명: 제품별 LOSS TOP 10<br/>
        /// 변경  내용: <br/> 

        private String[] dayArry = new String[7];

        /// </summary>
        public PQC030117()
        {
            InitializeComponent();
            SortInit();
            GridColumnInit();
            this.SetFactory(GlobalVariable.gsAssyDefaultFactory);
            cdvFactory.Text = GlobalVariable.gsAssyDefaultFactory;
            udcDate.AutoBinding(DateTime.Now.AddDays(-2).ToString(), DateTime.Now.AddDays(-1).ToString());
        }

        #region 초기화 및 유효성 검사
        /// <summary 1. 유효성 검사>
        /// <remarks></remarks>
        /// </summary>
        /// <returns></returns>
        private Boolean CheckField()
        {
            return true;
        }

        /// <summary>
        /// 2. 헤더 생성
        /// </summary>
        private void GridColumnInit()
        {
            spdData.RPT_ColumnInit();
            LabelTextChange();

            try
            {
                spdData.RPT_ColumnInit();
                spdData.RPT_AddBasicColumn("Customer", 0, 0, Visibles.False, Frozen.True, Align.Center, Merge.False, Formatter.String, 80);
                spdData.RPT_AddBasicColumn("Family", 0, 1, Visibles.False, Frozen.True, Align.Center, Merge.False, Formatter.String, 80);
                spdData.RPT_AddBasicColumn("PKG", 0, 2, Visibles.True, Frozen.True, Align.Center, Merge.False, Formatter.String, 80);
                spdData.RPT_AddBasicColumn("LD Count", 0, 3, Visibles.True, Frozen.True, Align.Center, Merge.False, Formatter.String, 80);
                spdData.RPT_AddBasicColumn("Type1", 0, 4, Visibles.False, Frozen.True, Align.Center, Merge.False, Formatter.String, 80);
                spdData.RPT_AddBasicColumn("Type2", 0, 5, Visibles.False, Frozen.True, Align.Center, Merge.False, Formatter.String, 80);
                spdData.RPT_AddBasicColumn("Density", 0, 6, Visibles.False, Frozen.True, Align.Center, Merge.False, Formatter.String, 80);
                spdData.RPT_AddBasicColumn("Generation", 0, 7, Visibles.False, Frozen.True, Align.Center, Merge.False, Formatter.String, 80);
                spdData.RPT_AddBasicColumn("REJECT", 0, 8, Visibles.True, Frozen.False, Align.Center, Merge.False, Formatter.String, 150);
                spdData.RPT_AddBasicColumn("REJECT_CODE", 0, 9, Visibles.True, Frozen.False, Align.Center, Merge.False, Formatter.String, 100);
                spdData.RPT_AddBasicColumn("LOSS_QTY", 0, 10, Visibles.True, Frozen.False, Align.Center, Merge.False, Formatter.Number, 50);
                spdData.RPT_AddBasicColumn("PPM", 0, 11, Visibles.True, Frozen.False, Align.Center, Merge.False, Formatter.Number, 50);
                spdData.RPT_AddBasicColumn("Generation operation", 0, 12, Visibles.True, Frozen.False, Align.Center, Merge.False, Formatter.String, 100);
                spdData.RPT_ColumnConfigFromTable(btnSort); //Group항목이 있을경우 반드시 선업해줄것.
            }
            catch (Exception ex)
            {
                CmnFunction.ShowMsgBox(ex.Message);
            }
        }

        /// <summary>
        /// 3. Group By 정의
        /// </summary>

        private void SortInit()
        {
            /*
            ((udcTableForm)(this.btnSort.BindingForm)).AddRow("Customer", "MAT_GRP_1", "MAT_GRP_1", false);
            ((udcTableForm)(this.btnSort.BindingForm)).AddRow("Family", "MAT_GRP_2", "MAT_GRP_2", false);
            ((udcTableForm)(this.btnSort.BindingForm)).AddRow("Package", "MAT_GRP_3", "MAT_GRP_3", true);
            ((udcTableForm)(this.btnSort.BindingForm)).AddRow("LD Count", "MAT_GRP_6", "TO_NUMBER(MAT_GRP_6) DESC", true);
            //((udcTableForm)(this.btnSort.BindingForm)).AddRow("Package", "MAT_GRP_3", "MAT_GRP_3", true);
            ((udcTableForm)(this.btnSort.BindingForm)).AddRow("Type1", "MAT_GRP_4", "MAT_GRP_4", false);
            ((udcTableForm)(this.btnSort.BindingForm)).AddRow("Type2", "MAT_GRP_5", "MAT_GRP_5", false);
            ((udcTableForm)(this.btnSort.BindingForm)).AddRow("Density", "MAT_GRP_7", "MAT_GRP_7", false);
            ((udcTableForm)(this.btnSort.BindingForm)).AddRow("Generation", "MAT_GRP_8", "MAT_GRP_8", false);
             */

            ((udcTableForm)(this.btnSort.BindingForm)).AddRow("Customer", "MAT_GRP_1", "MAT_GRP_1", "A.MAT_GRP_1", false);
            ((udcTableForm)(this.btnSort.BindingForm)).AddRow("Family", "MAT_GRP_2", "MAT_GRP_2", "A.MAT_GRP_2", false);
            ((udcTableForm)(this.btnSort.BindingForm)).AddRow("Package", "MAT_GRP_3", "MAT_GRP_3", "A.MAT_GRP_3", true);
            ((udcTableForm)(this.btnSort.BindingForm)).AddRow("LD Count", "MAT_GRP_6", "TO_NUMBER(MAT_GRP_6) DESC", "A.MAT_GRP_6", true);
            ((udcTableForm)(this.btnSort.BindingForm)).AddRow("Type1", "MAT_GRP_4", "MAT_GRP_4", "A.MAT_GRP_4", false);
            ((udcTableForm)(this.btnSort.BindingForm)).AddRow("Type2", "MAT_GRP_5", "MAT_GRP_5", "A.MAT_GRP_5", false);
            ((udcTableForm)(this.btnSort.BindingForm)).AddRow("Density", "MAT_GRP_7", "MAT_GRP_7", "A.MAT_GRP_7", false);
            ((udcTableForm)(this.btnSort.BindingForm)).AddRow("Generation", "MAT_GRP_8", "MAT_GRP_8", "A.MAT_GRP_8", false);

        }

        #endregion

        #region SQL 쿼리 Build
        /// <summary>
        /// 4. 쿼리 생성
        /// </summary>
        /// <returns></returns>
        private string MakeSqlString()
        {
            StringBuilder strSqlString = new StringBuilder();

            String strStartDate = udcDate.Start_Tran_Time;
            String strEndDate = udcDate.End_Tran_Time;

            string QueryCond1;
            string QueryCond2;
            string QueryCond3;

            udcTableForm tableForm = (udcTableForm)btnSort.BindingForm;
            QueryCond1 = tableForm.SelectedValueToQueryContainNull;
            QueryCond2 = tableForm.SelectedValue2ToQueryContainNull;
            QueryCond3 = tableForm.SelectedValue3ToQueryContainNull;


            strSqlString.Append("SELECT " + QueryCond3 + "\n");
            strSqlString.Append("      , LOSS_DESC, LOSS_CODE, A.LOSS_QTY" + "\n");
            strSqlString.Append("      , ROUND((A.LOSS_QTY/(C.LOSS_QTY+QTY)) * 1000000)" + "\n");
            strSqlString.Append("      , LOSS_OPER_DESC" + "\n");
            strSqlString.Append("   FROM(" + "\n");
            strSqlString.Append("SELECT " + QueryCond1 + "\n");
            strSqlString.Append("      , LOSS_DESC, LOSS_CODE, LOSS_QTY" + "\n");
            strSqlString.Append("      , LOSS_OPER_DESC" + "\n");
            strSqlString.Append("  FROM (" + "\n");
            strSqlString.Append("        SELECT " + QueryCond1 + ", ROW_NUMBER() OVER(PARTITION BY " + QueryCond1 + " ORDER BY MAT_GRP_3 DESC, MAT_GRP_6 DESC, LOSS_QTY DESC, A.LOSS_CODE ASC) AS NUM, B.LOSS_DESC,A.LOSS_CODE, A.LOSS_QTY, B.LOSS_OPER_DESC" + "\n");
            strSqlString.Append("            FROM" + "\n");
            strSqlString.Append("               (" + "\n");
            strSqlString.Append("               SELECT " + QueryCond1 + "\n");
            strSqlString.Append("                     , LOSS_CODE, SUM(NVL(LOSS_QTY,0)) AS LOSS_QTY" + "\n");
            strSqlString.Append("                 FROM RWIPLOTLSM A," + "\n");
            strSqlString.Append("                      (" + "\n");
            strSqlString.Append("                      SELECT * " + "\n");
            strSqlString.Append("                        FROM VWIPSHPLOT LOT, MWIPMATDEF MAT" + "\n");
            strSqlString.Append("                       WHERE FROM_FACTORY = '" + GlobalVariable.gsAssyDefaultFactory + "'" + "\n");
            strSqlString.Append("                         AND LOT.MAT_ID = MAT.MAT_ID" + "\n");
            strSqlString.Append("                         AND MAT.FACTORY ='" + GlobalVariable.gsAssyDefaultFactory + "'" + "\n");
            strSqlString.Append("                         AND LOT_TYPE = 'W'" + "\n");
            strSqlString.Append("                         AND TRAN_TIME BETWEEN '" + strStartDate + "' AND '" + strEndDate + "' " + "\n");
            if (udcWIPCondition1.Text != "ALL" && udcWIPCondition1.Text != "")
                strSqlString.Append("                         AND (LOT.MAT_ID LIKE '" + udcWIPCondition1.Text + "%'" + "\n");
            strSqlString.Append("                         AND LOT.MAT_ID NOT LIKE '%2JU')" + "\n");
            strSqlString.Append("                         AND FROM_OPER = 'AZ010'" + "\n");
            #region 상세 조회에 따른 SQL문 생성
            /*
            if (udcWIPCondition1.Text != "ALL" && udcWIPCondition1.Text != "")
                strSqlString.AppendFormat("                         AND MAT.MAT_GRP_1 {0} " + "\n", udcWIPCondition1.SelectedValueToQueryString);
            */
            if (udcWIPCondition2.Text != "ALL" && udcWIPCondition2.Text != "")
                strSqlString.AppendFormat("                         AND MAT.MAT_GRP_2 {0} " + "\n", udcWIPCondition2.SelectedValueToQueryString);

            if (udcWIPCondition3.Text != "ALL" && udcWIPCondition3.Text != "")
                strSqlString.AppendFormat("                         AND MAT.MAT_GRP_3 {0} " + "\n", udcWIPCondition3.SelectedValueToQueryString);

            if (udcWIPCondition4.Text != "ALL" && udcWIPCondition4.Text != "")
                strSqlString.AppendFormat("                         AND MAT.MAT_GRP_4 {0} " + "\n", udcWIPCondition4.SelectedValueToQueryString);

            if (udcWIPCondition5.Text != "ALL" && udcWIPCondition5.Text != "")
                strSqlString.AppendFormat("                         AND MAT.MAT_GRP_5 {0} " + "\n", udcWIPCondition5.SelectedValueToQueryString);

            if (udcWIPCondition6.Text != "ALL" && udcWIPCondition6.Text != "")
                strSqlString.AppendFormat("                         AND MAT.MAT_GRP_6 {0} " + "\n", udcWIPCondition6.SelectedValueToQueryString);

            if (udcWIPCondition7.Text != "ALL" && udcWIPCondition7.Text != "")
                strSqlString.AppendFormat("                         AND MAT.MAT_GRP_7 {0} " + "\n", udcWIPCondition7.SelectedValueToQueryString);

            if (udcWIPCondition8.Text != "ALL" && udcWIPCondition8.Text != "")
                strSqlString.AppendFormat("                         AND MAT.MAT_GRP_8 {0} " + "\n", udcWIPCondition8.SelectedValueToQueryString);

            if (udcWIPCondition9.Text != "ALL" && udcWIPCondition9.Text != "")
                strSqlString.AppendFormat("                         AND MAT.MAT_GRP_9 {0} " + "\n", udcWIPCondition9.SelectedValueToQueryString);

            #endregion
            strSqlString.Append("                       ) B" + "\n");
            strSqlString.Append("                WHERE A.LOT_ID(+) = B.LOT_ID" + "\n");
            strSqlString.Append("                  AND A.FACTORY = '" + GlobalVariable.gsAssyDefaultFactory + "'" + "\n");
            strSqlString.Append("                GROUP BY " + QueryCond1 + ", LOSS_CODE " + "\n");

            strSqlString.Append("                ORDER BY " + QueryCond2 + ", LOSS_QTY DESC, LOSS_CODE ASC " + "\n");
            strSqlString.Append("                ) A," + "\n");
            strSqlString.Append("                (" + "\n");
            strSqlString.Append("                SELECT KEY_1 AS LOSS_CODE" + "\n");
            strSqlString.Append("                     , DATA_1 AS LOSS_DESC" + "\n");
            strSqlString.Append("                     , DATA_5 AS LOSS_OPER_DESC" + "\n");
            strSqlString.Append("                  FROM MGCMTBLDAT" + "\n");
            strSqlString.Append("                 WHERE FACTORY = '" + GlobalVariable.gsAssyDefaultFactory + "'" + "\n");
            strSqlString.Append("                   AND TABLE_NAME = 'LOSS_CODE'" + "\n");
            strSqlString.Append("                ) B" + "\n");
            strSqlString.Append("            WHERE A.LOSS_CODE = B.LOSS_CODE" + "\n");
            strSqlString.Append(")" + "\n");
            //2010-11-04-김민우: 하기 조건은 TOP 10
            //strSqlString.Append("WHERE NUM < 11" + "\n");
            strSqlString.Append(") A," + "\n");
            strSqlString.Append("(SELECT MAT_GRP_3, MAT_GRP_6, SUM(SHIP_QTY_1) AS QTY" + "\n");
            strSqlString.Append("   FROM VWIPSHPLOT LOT, MWIPMATDEF MAT" + "\n");
            strSqlString.Append("  WHERE FROM_FACTORY = '" + GlobalVariable.gsAssyDefaultFactory + "'" + "\n");
            strSqlString.Append("    AND LOT.MAT_ID = MAT.MAT_ID" + "\n");
            strSqlString.Append("    AND MAT.FACTORY ='" + GlobalVariable.gsAssyDefaultFactory + "'" + "\n");
            strSqlString.Append("    AND LOT_TYPE = 'W'" + "\n");
            strSqlString.Append("    AND TRAN_TIME BETWEEN '" + strStartDate + "' AND '" + strEndDate + "' " + "\n");
            if (udcWIPCondition1.Text != "ALL" && udcWIPCondition1.Text != "")
                strSqlString.Append("    AND (LOT.MAT_ID LIKE '" + udcWIPCondition1.Text + "%'" + "\n");
            strSqlString.Append("    AND LOT.MAT_ID NOT LIKE '%2JU')" + "\n");
            strSqlString.Append("    AND FROM_OPER = 'AZ010'" + "\n");
            #region 상세 조회에 따른 SQL문 생성
            /*
            if (udcWIPCondition1.Text != "ALL" && udcWIPCondition1.Text != "")
                strSqlString.AppendFormat("                         AND MAT.MAT_GRP_1 {0} " + "\n", udcWIPCondition1.SelectedValueToQueryString);
            */
            if (udcWIPCondition2.Text != "ALL" && udcWIPCondition2.Text != "")
                strSqlString.AppendFormat("    AND MAT.MAT_GRP_2 {0} " + "\n", udcWIPCondition2.SelectedValueToQueryString);

            if (udcWIPCondition3.Text != "ALL" && udcWIPCondition3.Text != "")
                strSqlString.AppendFormat("    AND MAT.MAT_GRP_3 {0} " + "\n", udcWIPCondition3.SelectedValueToQueryString);

            if (udcWIPCondition4.Text != "ALL" && udcWIPCondition4.Text != "")
                strSqlString.AppendFormat("    AND MAT.MAT_GRP_4 {0} " + "\n", udcWIPCondition4.SelectedValueToQueryString);

            if (udcWIPCondition5.Text != "ALL" && udcWIPCondition5.Text != "")
                strSqlString.AppendFormat("    AND MAT.MAT_GRP_5 {0} " + "\n", udcWIPCondition5.SelectedValueToQueryString);

            if (udcWIPCondition6.Text != "ALL" && udcWIPCondition6.Text != "")
                strSqlString.AppendFormat("    AND MAT.MAT_GRP_6 {0} " + "\n", udcWIPCondition6.SelectedValueToQueryString);

            if (udcWIPCondition7.Text != "ALL" && udcWIPCondition7.Text != "")
                strSqlString.AppendFormat("    AND MAT.MAT_GRP_7 {0} " + "\n", udcWIPCondition7.SelectedValueToQueryString);

            if (udcWIPCondition8.Text != "ALL" && udcWIPCondition8.Text != "")
                strSqlString.AppendFormat("    AND MAT.MAT_GRP_8 {0} " + "\n", udcWIPCondition8.SelectedValueToQueryString);

            if (udcWIPCondition9.Text != "ALL" && udcWIPCondition9.Text != "")
                strSqlString.AppendFormat("    AND MAT.MAT_GRP_9 {0} " + "\n", udcWIPCondition9.SelectedValueToQueryString);

            #endregion
            strSqlString.Append("    GROUP BY " + QueryCond1 + "\n");
            strSqlString.Append(") B ," + "\n");

            strSqlString.Append("( " + "\n");
            strSqlString.Append(" SELECT " + QueryCond1 + "\n");
            strSqlString.Append("      , SUM(NVL(LOSS_QTY,0)) AS LOSS_QTY" + "\n");
            strSqlString.Append("   FROM RWIPLOTLSM A," + "\n");
            strSqlString.Append("       (" + "\n");
            strSqlString.Append("       SELECT * " + "\n");
            strSqlString.Append("         FROM VWIPSHPLOT LOT, MWIPMATDEF MAT" + "\n");
            strSqlString.Append("        WHERE FROM_FACTORY = '" + GlobalVariable.gsAssyDefaultFactory + "'" + "\n");
            strSqlString.Append("          AND LOT.MAT_ID = MAT.MAT_ID" + "\n");
            strSqlString.Append("          AND MAT.FACTORY ='" + GlobalVariable.gsAssyDefaultFactory + "'" + "\n");
            strSqlString.Append("          AND LOT_TYPE = 'W'" + "\n");
            strSqlString.Append("          AND TRAN_TIME BETWEEN '" + strStartDate + "' AND '" + strEndDate + "' " + "\n");
            if (udcWIPCondition1.Text != "ALL" && udcWIPCondition1.Text != "")
                strSqlString.Append("          AND (LOT.MAT_ID LIKE '" + udcWIPCondition1.Text + "%'" + "\n");
            strSqlString.Append("          AND LOT.MAT_ID NOT LIKE '%2JU')" + "\n");
            strSqlString.Append("                         AND FROM_OPER = 'AZ010'" + "\n");
            #region 상세 조회에 따른 SQL문 생성
            /*
            if (udcWIPCondition1.Text != "ALL" && udcWIPCondition1.Text != "")
                strSqlString.AppendFormat("                         AND MAT.MAT_GRP_1 {0} " + "\n", udcWIPCondition1.SelectedValueToQueryString);
            */
            if (udcWIPCondition2.Text != "ALL" && udcWIPCondition2.Text != "")
                strSqlString.AppendFormat("    AND MAT.MAT_GRP_2 {0} " + "\n", udcWIPCondition2.SelectedValueToQueryString);

            if (udcWIPCondition3.Text != "ALL" && udcWIPCondition3.Text != "")
                strSqlString.AppendFormat("    AND MAT.MAT_GRP_3 {0} " + "\n", udcWIPCondition3.SelectedValueToQueryString);

            if (udcWIPCondition4.Text != "ALL" && udcWIPCondition4.Text != "")
                strSqlString.AppendFormat("    AND MAT.MAT_GRP_4 {0} " + "\n", udcWIPCondition4.SelectedValueToQueryString);

            if (udcWIPCondition5.Text != "ALL" && udcWIPCondition5.Text != "")
                strSqlString.AppendFormat("    AND MAT.MAT_GRP_5 {0} " + "\n", udcWIPCondition5.SelectedValueToQueryString);

            if (udcWIPCondition6.Text != "ALL" && udcWIPCondition6.Text != "")
                strSqlString.AppendFormat("    AND MAT.MAT_GRP_6 {0} " + "\n", udcWIPCondition6.SelectedValueToQueryString);

            if (udcWIPCondition7.Text != "ALL" && udcWIPCondition7.Text != "")
                strSqlString.AppendFormat("    AND MAT.MAT_GRP_7 {0} " + "\n", udcWIPCondition7.SelectedValueToQueryString);

            if (udcWIPCondition8.Text != "ALL" && udcWIPCondition8.Text != "")
                strSqlString.AppendFormat("    AND MAT.MAT_GRP_8 {0} " + "\n", udcWIPCondition8.SelectedValueToQueryString);

            if (udcWIPCondition9.Text != "ALL" && udcWIPCondition9.Text != "")
                strSqlString.AppendFormat("    AND MAT.MAT_GRP_9 {0} " + "\n", udcWIPCondition9.SelectedValueToQueryString);

            #endregion
            strSqlString.Append("                       ) B" + "\n");
            strSqlString.Append("                WHERE A.LOT_ID(+) = B.LOT_ID" + "\n");
            strSqlString.Append("                  AND A.FACTORY = '" + GlobalVariable.gsAssyDefaultFactory + "'" + "\n");
            strSqlString.Append("                GROUP BY " + QueryCond1 + "" + "\n");
            strSqlString.Append("               ) C " + "" + "\n");
            strSqlString.Append("WHERE A.MAT_GRP_3 = B.MAT_GRP_3" + "\n");
            strSqlString.Append("  AND A.MAT_GRP_6 = B.MAT_GRP_6" + "\n");
            strSqlString.Append("  AND A.MAT_GRP_3 = C.MAT_GRP_3" + "\n");
            strSqlString.Append("  AND A.MAT_GRP_6 = C.MAT_GRP_6" + "\n");
            strSqlString.Append("  ORDER BY " + QueryCond2 + ", LOSS_QTY DESC, LOSS_CODE" + "\n");

            if (GlobalVariable.gsUserGroup == "ADMIN_GROUP" || GlobalVariable.gsUserGroup == "HANA_ADMIN_GROUP")
            {
                System.Windows.Forms.Clipboard.SetText(strSqlString.ToString());
            }

            return strSqlString.ToString();
        }

        #endregion

        #region EVENT 처리
        /// <summary>
        /// 6. View 버튼 Action
        /// </summary>        ///         
        private void btnView_Click(object sender, EventArgs e)
        {
            DataTable dt = null;

            if (CheckField() == false) return;


            if (udcWIPCondition1.Text.Equals(""))
            {
                CmnFunction.ShowMsgBox(RptMessages.GetMessage("STD038", GlobalVariable.gcLanguage));
                return;
            }
            
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
                //그루핑 순서 1순위만 SubTotal을 사용하기 위해서 첫번째 값을 가져옴
                //int sub = ((udcTableForm)(this.btnSort.BindingForm)).GetCheckedValue(2);
                spdData.DataSource = dt;


                dt.Dispose();

                /*
                //Chart 생성(LOSS 수량, 수율)
                if (spdData.ActiveSheet.RowCount > 0)
                     fnMakeChart2(spdData, spdData.ActiveSheet.RowCount);
                */
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
        /*
        // 공정 미선택 시 불량율 차트
        private void fnMakeChart(Miracom.SmartWeb.UI.Controls.udcFarPoint SS, int iselrow)
        {
            
            int[] ich_mm = new int[6]; int[] icols_mm = new int[6]; int[] irows_mm = new int[1]; string[] stitle_mm = new string[1];
            int[] ich_ww = new int[iselrow - 1]; int[] icols_ww = new int[iselrow - 1]; int[] irows_ww = new int[iselrow]; string[] stitle_ww = new string[iselrow];
            
            try
            {
                Cursor.Current = Cursors.WaitCursor;

                if (SS.Sheets[0].RowCount <= 0)
                {
                    return;
                }

                cf01.RPT_1_ChartInit();
                cf01.RPT_2_ClearData();

                cf01.AxisX.Title.Text = "Defect Status";
                cf01.Gallery = SoftwareFX.ChartFX.Gallery.Pie;

                cf01.LeftGap = 1;
                cf01.RightGap = 1;
                cf01.TopGap = 1;
                cf01.BottomGap = 1;

                cf01.AxisY.DataFormat.Format = 0;
                cf01.AxisX.Staggered = false;
                cf01.PointLabels = true;
                
                icols_mm[0] = 7;
                icols_mm[1] = 9;
                icols_mm[2] = 11;
                icols_mm[3] = 13;
                icols_mm[4] = 15;
                icols_mm[5] = 17;
                
                irows_mm[0] = iselrow;
                stitle_mm[0] = SS.Sheets[0].Cells[iselrow, 0].Text;

                cf01.RPT_3_OpenData(irows_mm.Length, icols_mm.Length);
                cf01.RPT_4_AddData(SS, irows_mm, icols_mm, SeriseType.Rows);
                cf01.RPT_5_CloseData();

                //cf01.RPT_7_SetXAsixTitleUsingSpreadHeader(SS, 1, 6);

                cf01.Legend[0] = SS.Sheets[0].Cells[iselrow, 6].Text;
                cf01.Legend[1] = SS.Sheets[0].Cells[iselrow, 8].Text;
                cf01.Legend[2] = SS.Sheets[0].Cells[iselrow, 10].Text;
                cf01.Legend[3] = SS.Sheets[0].Cells[iselrow, 12].Text;
                cf01.Legend[4] = SS.Sheets[0].Cells[iselrow, 14].Text;
                cf01.Legend[5] = SS.Sheets[0].Cells[iselrow, 16].Text;
                cf01.RPT_8_SetSeriseLegend(stitle_mm, SoftwareFX.ChartFX.Docked.Top);

                cf01.AxisY.DataFormat.Decimals = 0;
            }
            catch (Exception ex)
            {
                CmnFunction.ShowMsgBox(ex.Message);
                return;
            }
            finally
            {
                Cursor.Current = Cursors.Default;
            }
        }
       */


        /// <summary>
        /// Excel Export
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnExcelExport_Click(object sender, EventArgs e)
        {

            //StringBuilder Condition = new StringBuilder();
            //ExcelHelper.Instance.subMakeExcel(spdData, this.lblTitle.Text, Condition.ToString(), null, true);
            //ExcelHelper.Instance.subMakeExcel(spdData, this.lblTitle.Text, null, null);
            spdData.ExportExcel();            


        }

        /// <summary>
        /// Factory 설정
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cdvFactory_ValueSelectedItemChanged(object sender, Miracom.UI.MCCodeViewSelChanged_EventArgs e)
        {
            this.SetFactory(cdvFactory.txtValue);

        }

        /// <summary>
        /// 7. 상단 Lebel 표시
        /// </summary>
        private void LabelTextChange()
        {


        }
        #endregion

        private void PQC030117_Load(object sender, EventArgs e)
        {
            //기본적으로 Detail이 보이도록..
            pnlWIPDetail.Visible = true;
        }

      
    }
}
