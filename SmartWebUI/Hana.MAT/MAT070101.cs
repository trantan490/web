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
using FarPoint.Win.Spread;

namespace Hana.MAT
{
    public partial class MAT070101 : Miracom.SmartWeb.UI.Controls.udcCUSReport002
    {
        /// <summary>
        /// 클  래  스: MAT070101<br/>
        /// 클래스요약: 자재 공정재공 조회<br/>
        /// 작  성  자: 하나마이크론 임종우<br/>
        /// 최초작성일: 2009-01-21<br/>
        /// 상세  설명: 품목별 자재 공정재공 조회.<br/>
        /// 변경  내용: <br/>
        /// </summary>
        public MAT070101()
        {
            InitializeComponent();

            SortInit();
            GridColumnInit(); //헤더 한줄짜리

            //cdvDate.Value = DateTime.Today;
            cboMatType.SelectedIndex = 0;          
        }

        #region SortInit

        private void SortInit()
        {
            ((udcTableForm)(this.btnSort.BindingForm)).AddRow("Item Code", "B.MAT_ID", "A.MAT_ID", "A.MAT_ID", true);
            ((udcTableForm)(this.btnSort.BindingForm)).AddRow("Item specification", "C.MAT_DESC", "B.MAT_DESC", "A.MAT_DESC", true);
            //((udcTableForm)(this.btnSort.BindingForm)).AddRow("Package", "B.MAT_GRP_3 as PKG", "B.MAT_GRP_3", "B.MAT_GRP_3", false);
            //((udcTableForm)(this.btnSort.BindingForm)).AddRow("Type1", "B.MAT_GRP_4 as Type1", "B.MAT_GRP_4", "B.MAT_GRP_4", false);
            //((udcTableForm)(this.btnSort.BindingForm)).AddRow("Type2", "B.MAT_GRP_5 as Type2", "B.MAT_GRP_5", "B.MAT_GRP_5", false);
            //((udcTableForm)(this.btnSort.BindingForm)).AddRow("LD Count", "B.MAT_GRP_6 as LEAD", "B.MAT_GRP_6", "B.MAT_GRP_6", false);
            //((udcTableForm)(this.btnSort.BindingForm)).AddRow("Density", "B.MAT_GRP_7 as Density", "B.MAT_GRP_7", "B.MAT_GRP_7", false);
            //((udcTableForm)(this.btnSort.BindingForm)).AddRow("Generation", "B.MAT_GRP_8 as Generation", "B.MAT_GRP_8", "B.MAT_GRP_8", false);
            //((udcTableForm)(this.btnSort.BindingForm)).AddRow("Product", "B.MAT_ID AS Product", "A.MAT_ID", "B.MAT_ID", true);
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
            spdData.ActiveSheet.ColumnHeader.Rows.Count = 1;
            spdData.RPT_ColumnInit();
            //spdData.RPT_AddBasicColumn("Customer", 0, 0, Visibles.False, Frozen.True, Align.Center, Merge.True, Formatter.String, 60);
            //spdData.RPT_AddBasicColumn("Family", 0, 1, Visibles.False, Frozen.True, Align.Center, Merge.True, Formatter.String, 80);
            //spdData.RPT_AddBasicColumn("Package", 0, 2, Visibles.False, Frozen.True, Align.Center, Merge.True, Formatter.String, 60);
            //spdData.RPT_AddBasicColumn("Type1", 0, 3, Visibles.False, Frozen.True, Align.Center, Merge.True, Formatter.String, 70);
            //spdData.RPT_AddBasicColumn("Type2", 0, 4, Visibles.False, Frozen.True, Align.Center, Merge.True, Formatter.String, 70);
            //spdData.RPT_AddBasicColumn("LD Count", 0, 5, Visibles.False, Frozen.True, Align.Center, Merge.True, Formatter.String, 70);
            //spdData.RPT_AddBasicColumn("Density", 0, 6, Visibles.False, Frozen.True, Align.Center, Merge.True, Formatter.String, 70);
            //spdData.RPT_AddBasicColumn("Generation", 0, 7, Visibles.False, Frozen.True, Align.Center, Merge.True, Formatter.String, 70);
            //spdData.RPT_AddBasicColumn("Product", 0, 8, Visibles.True, Frozen.True, Align.Center, Merge.True, Formatter.String, 120);

            spdData.RPT_AddBasicColumn("Item Code", 0, 0, Visibles.True, Frozen.True, Align.Center, Merge.True, Formatter.String, 120);
            spdData.RPT_AddBasicColumn("Item specification", 0, 1, Visibles.True, Frozen.True, Align.Center, Merge.True, Formatter.String, 120);

            spdData.RPT_AddBasicColumn("Today's receipts and disbursements", 0, 2, Visibles.True, Frozen.False, Align.Center, Merge.True, Formatter.String, 140);
            spdData.RPT_AddBasicColumn("warehousing", 1, 2, Visibles.True, Frozen.False, Align.Center, Merge.True, Formatter.String, 40);
            spdData.RPT_AddBasicColumn("exhaustion", 1, 3, Visibles.True, Frozen.False, Align.Center, Merge.True, Formatter.String, 40);

            if (cboMatType.Text == "GW")
            {
                spdData.RPT_AddBasicColumn("Return All", 1, 4, Visibles.True, Frozen.False, Align.Center, Merge.True, Formatter.String, 60);
            }
            else
            {
                spdData.RPT_AddBasicColumn("Return All", 1, 4, Visibles.False, Frozen.False, Align.Center, Merge.True, Formatter.String, 60);
            }

            spdData.RPT_MerageHeaderColumnSpan(0, 2, 3);

            spdData.RPT_AddBasicColumn("Process WIP", 0, 5, Visibles.True, Frozen.False, Align.Center, Merge.True, Formatter.String, 160);

            if (cboMatType.Text == "GW")
            {
                spdData.RPT_AddBasicColumn("A", 1, 5, Visibles.True, Frozen.False, Align.Center, Merge.True, Formatter.String, 40);
                spdData.RPT_AddBasicColumn("B", 1, 6, Visibles.True, Frozen.False, Align.Center, Merge.True, Formatter.String, 40);
                spdData.RPT_AddBasicColumn("Equipment", 1, 7, Visibles.True, Frozen.False, Align.Center, Merge.True, Formatter.String, 40);
                spdData.RPT_AddBasicColumn("TOTAL", 1, 8, Visibles.True, Frozen.False, Align.Center, Merge.True, Formatter.String, 40);
            }
            else
            {
                spdData.RPT_AddBasicColumn("A", 1, 5, Visibles.True, Frozen.False, Align.Center, Merge.True, Formatter.String, 40);
                spdData.RPT_AddBasicColumn("B", 1, 6, Visibles.True, Frozen.False, Align.Center, Merge.True, Formatter.String, 40);
                spdData.RPT_AddBasicColumn("Equipment", 1, 7, Visibles.False, Frozen.False, Align.Center, Merge.True, Formatter.String, 40);
                spdData.RPT_AddBasicColumn("TOTAL", 1, 8, Visibles.True, Frozen.False, Align.Center, Merge.True, Formatter.String, 40);
            }
            spdData.RPT_MerageHeaderColumnSpan(0, 5, 4);
            //spdData.RPT_AddDynamicColumn("PRODUCT WIP", new string[] { "B/G", "SAW", "D/A", "W/B", "MOLD", "TOTAL" },
            //    0, 14, Visibles.True, Frozen.False, Align.Right, Merge.False, new Formatter[] { Formatter.Number, Formatter.Number, Formatter.Number, Formatter.Number, Formatter.Number, Formatter.Number }, 60);

            spdData.RPT_MerageHeaderRowSpan(0, 0, 2);
            spdData.RPT_MerageHeaderRowSpan(0, 1, 2);

            //spdData.RPT_AddDynamicColumn(udcDurationDate1, 0, 14, Visibles.False, Frozen.False, Align.Right, Merge.False, Formatter.String, 60);
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

                ////by John (한줄짜리)
                //1.Griid 합계 표시
                int[] rowType = spdData.RPT_DataBindingWithSubTotal(dt, 0, 0, 2, null, null, btnSort);
                //spdData.DataSource = dt;
                ////2. 칼럼 고정(필요하다면..)
                //spdData.Sheets[0].FrozenColumnCount = 9;x

                ////3. Total부분 셀머지
                spdData.RPT_FillDataSelectiveCells("Total", 0, 2, 0, 1, true, Align.Center, VerticalAlign.Center);

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

        #region CheckField

        /// <summary>
        /// CheckField
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private Boolean CheckField()
        {
            //if (cdvFactory.Text.Trim().Length == 0)
            //{
            //    CmnFunction.ShowMsgBox(RptMessages.GetMessage("STD001", GlobalVariable.gcLanguage));
            //    return false;
            //}

            //if (cdvStep.Text.Trim().Length == 0)
            //{
            //    CmnFunction.ShowMsgBox(RptMessages.GetMessage("STD005", GlobalVariable.gcLanguage));
            //    return false;
            //}

            return true;
        }

        #endregion

        #region MakeSqlString
        private string MakeSqlString()
        {
            StringBuilder strSqlString = new StringBuilder();

            string QueryCond1 = null;
            string QueryCond2 = null;

            udcTableForm tableForm = (udcTableForm)btnSort.BindingForm;

            QueryCond1 = tableForm.SelectedValueToQueryContainNull;
            QueryCond2 = tableForm.SelectedValue2ToQueryContainNull;
            strSqlString.AppendFormat("SELECT {0},  " + "\n", QueryCond1);
            strSqlString.Append("       NVL(A.IN_QTY,0),NVL(A.OUT_QTY,0),NVL(A.OUT_CASE_QTY,0),B.A_QTY,B.B_QTY,B.C_QTY,B.TOTAL " + "\n");
            strSqlString.Append(" FROM( " + "\n");
            strSqlString.Append("       SELECT MAT_ID, " + "\n");
            strSqlString.Append("              COUNT(DECODE(SUBSTR(OPER_IN_TIME,1,8),TO_CHAR(SYSDATE,'YYYYMMDD'),'IN')) AS IN_QTY, " + "\n");
            strSqlString.Append("              COUNT(DECODE(QTY_1,0,DECODE(SUBSTR(LAST_TRAN_TIME,1,8),TO_CHAR(SYSDATE,'YYYYMMDD'),'OUT'))) AS OUT_QTY, " + "\n");
            strSqlString.Append("              COUNT(DECODE(QTY_1,0,DECODE(LOT_DEL_FLAG,'Y',DECODE(LOT_DEL_CODE,'M00',DECODE(SUBSTR(LAST_TRAN_TIME,1,8),TO_CHAR(SYSDATE,'YYYYMMDD'),'CASE'))))) AS OUT_CASE_QTY  " + "\n");
            strSqlString.Append("         FROM RWIPLOTSTS " + "\n");
            strSqlString.Append("        WHERE 1 = 1 " + "\n");
            strSqlString.Append("              AND FACTORY = '" + GlobalVariable.gsAssyDefaultFactory + "' " + "\n");
            strSqlString.Append("              AND LOT_TYPE <> 'W' " + "\n");
            strSqlString.Append("              AND OPER IN ( " + "\n");
            strSqlString.Append("                            SELECT ATTR_KEY " + "\n");
            strSqlString.Append("                              FROM MATRNAMSTS " + "\n");
            strSqlString.Append("                             WHERE 1 = 1 " + "\n");
            strSqlString.Append("                                   AND FACTORY = '" + GlobalVariable.gsAssyDefaultFactory + "' " + "\n");
            strSqlString.Append("                                   AND ATTR_TYPE = 'OPER' " + "\n");
            strSqlString.Append("                                   AND ATTR_NAME = 'MATERIAL_OPER' " + "\n");
            strSqlString.Append("                                   AND ATTR_VALUE = 'Y' " + "\n");
            strSqlString.Append("                                   AND ATTR_KEY <> 'V0000' " + "\n");
            strSqlString.Append("                           ) " + "\n");
            //strSqlString.Append("              AND SUBSTR(LAST_TRAN_TIME,1,8) = TO_CHAR(SYSDATE,'YYYYMMDD') " + "\n");
            strSqlString.Append("       GROUP BY MAT_ID " + "\n");
            strSqlString.Append("     )A, " + "\n");
            strSqlString.Append("     ( " + "\n");
            strSqlString.Append("       SELECT MAT_ID, " + "\n");
            strSqlString.Append("              SUM(DECODE(RES_QTY,0,A,0)) A_QTY, " + "\n");
            strSqlString.Append("              SUM(DECODE(RES_QTY,0,B,0)) B_QTY, " + "\n");
            strSqlString.Append("              SUM(RES_QTY) C_QTY, " + "\n");
            strSqlString.Append("              SUM(DECODE(RES_QTY,0,A,0)+DECODE(RES_QTY,0,B,0)+RES_QTY) TOTAL " + "\n");
            strSqlString.Append("        FROM( " + "\n");
            strSqlString.Append("              SELECT LOT_ID,MAT_ID, " + "\n");
            strSqlString.Append("                     GRADE, " + "\n");
            strSqlString.Append("                     SUM(DECODE(GRADE,'A',LOT_QTY,0)) AS A, " + "\n");
            strSqlString.Append("                     SUM(DECODE(GRADE,'B',LOT_QTY,0)) AS B, " + "\n");
            strSqlString.Append("                     SUM(RES_QTY) RES_QTY " + "\n");
            strSqlString.Append("               FROM( " + "\n");
            strSqlString.Append("                     ( " + "\n");
            strSqlString.Append("                       SELECT LOT_ID,MAT_ID, " + "\n");
            strSqlString.Append("                              REPLACE(LOT_CMF_9,' ','A') GRADE, " + "\n");
            strSqlString.Append("                              COUNT(LOT_ID) AS LOT_QTY, " + "\n");
            strSqlString.Append("                              0 AS RES_QTY " + "\n");
            strSqlString.Append("                         FROM RWIPLOTSTS " + "\n");
            strSqlString.Append("                        WHERE 1=1 " + "\n");
            strSqlString.Append("                              AND FACTORY='" + GlobalVariable.gsAssyDefaultFactory + "' " + "\n");
            strSqlString.Append("                              AND LOT_TYPE <> 'W' " + "\n");
            strSqlString.Append("                              AND LOT_DEL_FLAG=' ' " + "\n");
            strSqlString.Append("                              AND OPER IN ( " + "\n");
            strSqlString.Append("                                            SELECT ATTR_KEY " + "\n");
            strSqlString.Append("                                              FROM MATRNAMSTS " + "\n");
            strSqlString.Append("                                             WHERE 1 = 1 " + "\n");
            strSqlString.Append("                                                   AND FACTORY = '" + GlobalVariable.gsAssyDefaultFactory + "' " + "\n");
            strSqlString.Append("                                                   AND ATTR_TYPE = 'OPER' " + "\n");
            strSqlString.Append("                                                   AND ATTR_NAME = 'MATERIAL_OPER' " + "\n");
            strSqlString.Append("                                                   AND ATTR_VALUE = 'Y' " + "\n");
            strSqlString.Append("                                                   AND ATTR_KEY <> 'V0000' " + "\n");
            strSqlString.Append("                                          ) " + "\n");
            strSqlString.Append("                       GROUP BY LOT_ID,MAT_ID,LOT_CMF_9 " + "\n");
            strSqlString.Append("                     ) " + "\n");
            strSqlString.Append("                     UNION ALL " + "\n");
            strSqlString.Append("                     ( " + "\n");
            strSqlString.Append("                       SELECT A.LOT_ID,A.MAT_ID, " + "\n");
            strSqlString.Append("                              REPLACE(A.LOT_CMF_9,' ','A') GRADE, " + "\n");
            strSqlString.Append("                              0 AS LOT_QTY, " + "\n");
            strSqlString.Append("                              COUNT(A.LOT_ID) AS RES_QTY " + "\n");
            strSqlString.Append("                         FROM RWIPLOTSTS A, " + "\n");
            strSqlString.Append("                              CRASRESMAT B " + "\n");
            strSqlString.Append("                        WHERE A.LOT_ID =B.LOT_ID " + "\n");
            strSqlString.Append("                       GROUP BY A.LOT_ID,A.MAT_ID,A.LOT_CMF_9 " + "\n");
            strSqlString.Append("                     )  " + "\n");
            strSqlString.Append("                   ) " + "\n");
            strSqlString.Append("              GROUP BY LOT_ID,MAT_ID,GRADE " + "\n");
            strSqlString.Append("            ) " + "\n");
            strSqlString.Append("       GROUP BY MAT_ID " + "\n");
            strSqlString.Append("     )B, MWIPMATDEF C " + "\n");
            strSqlString.Append(" WHERE 1 = 1 " + "\n");
            strSqlString.Append("       AND A.MAT_ID(+)=B.MAT_ID " + "\n");
            strSqlString.Append("       AND B.MAT_ID=C.MAT_ID " + "\n");
            strSqlString.Append("       AND C.MAT_TYPE='" + cboMatType.Text + "'\n");

            //strSqlString.Append("               AND C.MAT_TYPE='" + cboMatType.Text + "'\n");

            // 자재 TYPE에 따른 값 가져오기
//            if (cboMatType.Text == "GW")
//            {
//                strSqlString.Append(@"              AND C.OPER_GRP_1 IN ('HMK2A','B/G','SAW','SMT','S/P','D/A') " + "\n");
//            }
//            else if (cboMatType.Text == "GW")
//            {
//                strSqlString.Append(@"              AND C.OPER_GRP_1 IN ('HMK2A','B/G','SAW','SMT','S/P','D/A','W/B') " + "\n");
//            }
//            else if (cboMatType.Text == "MC")
//            {
//                strSqlString.Append(@"              AND C.OPER_GRP_1 IN ('HMK2A','B/G','SAW','SMT','S/P','D/A','W/B','MOLD') " + "\n");
//            }
//            else
//            {
//                strSqlString.Append(@"              AND C.OPER_GRP_1 IN ('HMK2A','B/G','SAW','SMT','S/P','D/A','W/B','MOLD','CURE','M/K','V/I','S/B/A') " + "\n");
//            }

//            strSqlString.AppendFormat(" GROUP BY {0}" + "\n", QueryCond2);
//            strSqlString.Append(@"
//                                            HAVING SUM(A.OPER_IN_QTY_1) > 0
//                                            )B
//                                            WHERE   A.PARTNUMBER=B.MAT_ID" + "\n");


            //#region 조회조건(FACTORY, STEP, LOT_TYPE, PRODUCT, DATE)

            //strSqlString.Append(@"        AND A.MAT_TYPE='" + cboMatType.Text + "'\n");

            //if (cdvStep.Text != "ALL" && cdvStep.Text.Trim() != "")
            //    strSqlString.Append("        AND LOT.OPER " + cdvStep.SelectedValueToQueryString + "\n");

            //if (cdvFactory.Text == GlobalVariable.gsAssyDefaultFactory)
            //    strSqlString.Append("        AND LOT.OPER NOT IN ('00001','00002') " + "\n");

            //if (cdvLotType.Text != "ALL" && cdvLotType.Text.Trim() != "")
            //    strSqlString.Append("        AND LOT.LOT_TYPE " + cdvLotType.SelectedValueToQueryString + "\n");

            //if (txtSearchProduct.Text.Trim() != "%" && txtSearchProduct.Text.Trim() != "")
            //    strSqlString.AppendFormat("        AND A.PARTNUMBER LIKE '{0}'" + "\n", txtSearchProduct.Text);

            //#endregion

            //#region 상세 조회에 따른 SQL문 생성
            //if (udcWIPCondition1.Text != "ALL" && udcWIPCondition1.Text != "")
            //    strSqlString.AppendFormat("        AND B.MAT_GRP_1 {0} " + "\n", udcWIPCondition1.SelectedValueToQueryString);

            //if (udcWIPCondition2.Text != "ALL" && udcWIPCondition2.Text != "")
            //    strSqlString.AppendFormat("        AND B.MAT_GRP_2 {0} " + "\n", udcWIPCondition2.SelectedValueToQueryString);

            //if (udcWIPCondition3.Text != "ALL" && udcWIPCondition3.Text != "")
            //    strSqlString.AppendFormat("        AND B.MAT_GRP_3 {0} " + "\n", udcWIPCondition3.SelectedValueToQueryString);

            //if (udcWIPCondition4.Text != "ALL" && udcWIPCondition4.Text != "")
            //    strSqlString.AppendFormat("        AND B.MAT_GRP_4 {0} " + "\n", udcWIPCondition4.SelectedValueToQueryString);

            //if (udcWIPCondition5.Text != "ALL" && udcWIPCondition5.Text != "")
            //    strSqlString.AppendFormat("        AND B.MAT_GRP_5 {0} " + "\n", udcWIPCondition5.SelectedValueToQueryString);

            //if (udcWIPCondition6.Text != "ALL" && udcWIPCondition6.Text != "")
            //    strSqlString.AppendFormat("        AND B.MAT_GRP_6 {0} " + "\n", udcWIPCondition6.SelectedValueToQueryString);

            //if (udcWIPCondition7.Text != "ALL" && udcWIPCondition7.Text != "")
            //    strSqlString.AppendFormat("        AND B.MAT_GRP_7 {0} " + "\n", udcWIPCondition7.SelectedValueToQueryString);

            //if (udcWIPCondition8.Text != "ALL" && udcWIPCondition8.Text != "")
            //    strSqlString.AppendFormat("        AND B.MAT_GRP_8 {0} " + "\n", udcWIPCondition8.SelectedValueToQueryString);
            //#endregion

            strSqlString.AppendFormat("ORDER BY {0} " + "\n", QueryCond1);

            if (GlobalVariable.gsUserGroup == "ADMIN_GROUP" || GlobalVariable.gsUserGroup == "HANA_ADMIN_GROUP")
            {
                System.Windows.Forms.Clipboard.SetText(strSqlString.ToString());
            }

            return strSqlString.ToString();
        }
        #endregion

        #endregion

        #region Event

        private void btnExcelExport_Click(object sender, EventArgs e)
        {
            ExcelHelper.Instance.subMakeExcel(spdData, this.lblTitle.Text, " ^ ", " ^ ");
        }


        #endregion

        private void cdvFactory_ValueSelectedItemChanged(object sender, MCCodeViewSelChanged_EventArgs e)
        {
            //this.SetFactory(cdvFactory.txtValue);
            //cdvStep.sFactory = cdvFactory.txtValue;
            //cdvLotType.sFactory = cdvFactory.txtValue;
        }

        private void spdData_CellClick(object sender, FarPoint.Win.Spread.CellClickEventArgs e)
        {
            if (e.Column < 2 || e.Row < 1)
                return;
            //if (e.Row < 1)
            //    return;
            //string strtest = spdData.ActiveSheet.Rows[e.Row].Label;
            //string strtest2 = spdData.ActiveSheet.Cells[e.Row, e.Column].Value.ToString();

            // 품목코드와 Type(입고,출고..) 을 문자열로 저장한다.
            string strMatId = spdData.ActiveSheet.Cells[e.Row, 0].Value.ToString();
            string strType = spdData.ActiveSheet.Columns[e.Column].Label;

            DataTable dt = CmnFunction.oComm.GetFuncDataTable("DYNAMIC", MakeSqlStringForPopup(strMatId,strType));

            if (dt != null && dt.Rows.Count > 0)
            {
                System.Windows.Forms.Form frm = new MAT070101_P1(strType,cboMatType.Text, dt);
                frm.ShowDialog();                
            }
        }

        private string MakeSqlStringForPopup(string strMatId, String strType)
        {
            StringBuilder strSqlString = new StringBuilder();

            string QueryCond1 = null;
            string QueryCond2 = null;
            string QueryCond3 = null;

            udcTableForm tableForm = (udcTableForm)btnSort.BindingForm;

            QueryCond1 = tableForm.SelectedValueToQueryContainNull;
            QueryCond2 = tableForm.SelectedValue2ToQueryContainNull;
            QueryCond3 = tableForm.SelectedValue3ToQueryContainNull;

            if (cboMatType.Text == "GW")
            {
                if (!strType.Equals("TOTAL"))
                {
                    strSqlString.AppendFormat("SELECT   {0},  " + "\n", QueryCond2);

                    if (strType.Equals("설비"))
                    {
                        strSqlString.Append("       C.RES_ID AS EQP_NO," + "\n");
                        strSqlString.Append("       DECODE(D.RES_PRI_STS,'PROC','ENABLE','DISABLE') EQP_STATE," + "\n");
                        strSqlString.Append("       A.LOT_ID,A.OPER_IN_QTY_1,B.UNIT_1" + "\n");
                    }
                    else
                    {
                        // 소진,통반납의 경우 현재 재공수량을 보여준다.
                        if (strType.Equals("소진") || strType.Equals("통반납"))
                        {
                            strSqlString.Append("       '-','-',A.LOT_ID,A.QTY_1,B.UNIT_1" + "\n");
                        }
                        else strSqlString.Append("      '-','-',A.LOT_ID,A.OPER_IN_QTY_1,B.UNIT_1" + "\n");
                    }

                    strSqlString.Append("       ,'-','-','-','-' " + "\n");
                    strSqlString.Append("FROM   RWIPLOTSTS A, MWIPMATDEF B" + "\n");

                    if (strType.Equals("설비"))
                        strSqlString.Append("       ,CRASRESMAT C,MRASRESDEF D" + "\n");
                    strSqlString.Append("WHERE  1=1" + "\n");
                    strSqlString.Append("       AND A.MAT_ID=B.MAT_ID" + "\n");
                    strSqlString.Append("       AND A.FACTORY='" + GlobalVariable.gsAssyDefaultFactory + "'" + "\n");
                    strSqlString.Append("       AND A.LOT_TYPE='M'" + "\n");
                    strSqlString.Append("       AND A.OPER='00002'" + "\n");
                    strSqlString.Append("       AND A.MAT_ID='" + strMatId + "'\n");

                    if (strType.Equals("입고") || strType.Equals("소진") || strType.Equals("통반납"))
                        strSqlString.Append("       AND SUBSTR(A.OPER_IN_TIME,1,8)=TO_CHAR(SYSDATE,'YYYYMMDD')" + "\n");

                    if (strType.Equals("소진"))
                        strSqlString.Append("       AND A.QTY_1=0" + "\n");

                    if (strType.Equals("통반납"))
                    {
                        strSqlString.Append("       AND LOT_DEL_FLAG='Y'" + "\n");
                        strSqlString.Append("       AND LOT_DEL_CODE='AUTO_TERM'" + "\n");
                    }

                    if (strType.Equals("설비"))
                    {
                        strSqlString.Append("       AND A.LOT_ID=C.LOT_ID" + "\n");
                        strSqlString.Append("       AND C.RES_ID=D.RES_ID" + "\n");
                    }
                    if (strType.Equals("A") || strType.Equals("B"))
                    {
                        if (strType.Equals("A"))
                            strSqlString.Append("       AND A.LOT_CMF_9=' '" + "\n");
                        else
                            strSqlString.Append("       AND A.LOT_CMF_9='B'" + "\n");

                        strSqlString.Append("       AND A.LOT_DEL_FLAG=' '" + "\n");
                        strSqlString.Append("       AND A.LOT_ID NOT IN" + "\n");
                        strSqlString.Append("       (SELECT LOT_ID  FROM    CRASRESMAT)" + "\n");
                    }

                    strSqlString.AppendFormat("ORDER BY {0},A.LOT_ID " + "\n", QueryCond2);
                }
                else // 공정재공 TOTAL 부분(A+B+설비)
                {
                    strSqlString.AppendFormat("SELECT   {0},    " + "\n", QueryCond3);
                    strSqlString.Append("       NVL(B.EQP_NO,'-'),NVL(B.EQP_STATE,'-'),A.LOT_ID,A.OPER_IN_QTY_1,A.UNIT_1 " + "\n");
                    strSqlString.Append("FROM " + "\n");
                    strSqlString.AppendFormat("(    SELECT  {0},    " + "\n", QueryCond2);
                    strSqlString.Append("           A.LOT_ID,A.OPER_IN_QTY_1,B.UNIT_1 " + "\n");
                    strSqlString.Append("   FROM    RWIPLOTSTS A, MWIPMATDEF B " + "\n");
                    strSqlString.Append("   WHERE   1=1 " + "\n");
                    strSqlString.Append("           AND A.MAT_ID=B.MAT_ID " + "\n");
                    strSqlString.Append("           AND A.FACTORY='" + GlobalVariable.gsAssyDefaultFactory + "' " + "\n");
                    strSqlString.Append("           AND A.LOT_TYPE='M' " + "\n");
                    strSqlString.Append("           AND A.OPER='00002' " + "\n");
                    strSqlString.Append("           AND A.LOT_CMF_9 IN (' ','B') " + "\n");
                    strSqlString.Append("           AND A.LOT_DEL_FLAG=' ' " + "\n");
                    strSqlString.Append(")A, " + "\n");
                    strSqlString.Append("(  SELECT  C.RES_ID AS EQP_NO,DECODE(D.RES_PRI_STS,'PROC','ENABLE','DISABLE') EQP_STATE,C.LOT_ID " + "\n");
                    strSqlString.Append("   FROM    CRASRESMAT C,MRASRESDEF D " + "\n");
                    strSqlString.Append("   WHERE   1=1 " + "\n");
                    strSqlString.Append("           AND C.RES_ID=D.RES_ID " + "\n");
                    strSqlString.Append(")B " + "\n");
                    strSqlString.Append("WHERE  A.LOT_ID=B.LOT_ID(+) " + "\n");
                    strSqlString.Append("       AND A.MAT_ID='" + strMatId + "'\n");
                    strSqlString.AppendFormat("ORDER BY {0},A.LOT_ID " + "\n", QueryCond3);
                }
            }
            else // EMC 자재 부분
            {
                strSqlString.AppendFormat("SELECT   {0},    " + "\n", QueryCond2);

                if(strType.Equals("입고")) // 입고 부분 (공정 입고 당시의 수량)
                    strSqlString.Append("       '-','-',A.LOT_ID,A.OPER_IN_QTY_1,B.UNIT_1, " + "\n");
                else // 그 외 부분 (현재 재공 수량)
                    strSqlString.Append("       '-','-',A.LOT_ID,A.QTY_1,B.UNIT_1, " + "\n");


                strSqlString.Append("       DECODE(C.CUSTODY,NULL,'N','Y') AS CUSTODY,C.AGING_TIME,C.END_TIME,C.L_TIME " + "\n");
                strSqlString.Append("FROM   RWIPLOTSTS A, MWIPMATDEF B, " + "\n");
                strSqlString.Append("(  SELECT  LOT_ID,DECODE(RESV_FIELD_3,' ','N','Y') AS CUSTODY,AGING_TIME,RESV_FIELD_2 AS END_TIME,RESV_FIELD_4 AS L_TIME " + "\n");
                strSqlString.Append("   FROM    CWIPMATAGI " + "\n");
                strSqlString.Append(")C " + "\n");
                strSqlString.Append("WHERE  1=1 " + "\n");
                strSqlString.Append("       AND A.MAT_ID=B.MAT_ID " + "\n");
                strSqlString.Append("       AND A.FACTORY='" + GlobalVariable.gsAssyDefaultFactory + "' " + "\n");
                strSqlString.Append("       AND A.LOT_TYPE='M' " + "\n");
                strSqlString.Append("       AND A.OPER='00002' " + "\n");

                if (strType.Equals("입고") || strType.Equals("소진"))
                    strSqlString.Append("       AND SUBSTR(A.OPER_IN_TIME,1,8)=TO_CHAR(SYSDATE,'YYYYMMDD') " + "\n");
                else
                {
                    if (strType.Equals("A"))
                        strSqlString.Append("       AND A.LOT_CMF_9=' ' " + "\n");
                    if (strType.Equals("B"))
                        strSqlString.Append("       AND A.LOT_CMF_9='B' " + "\n");
                    if (strType.Equals("TOTAL"))
                        strSqlString.Append("       AND A.LOT_CMF_9 IN (' ','B') " + "\n");

                    strSqlString.Append("       AND A.LOT_DEL_FLAG=' ' " + "\n");
                    strSqlString.Append("       AND A.QTY_1 <> 0 " + "\n");
                }

                if (strType.Equals("소진"))
                    strSqlString.Append("       AND A.QTY_1=0 " + "\n");

                strSqlString.Append("       AND A.MAT_ID='" + strMatId + "'\n");
                strSqlString.Append("       AND A.LOT_ID=C.LOT_ID(+) " + "\n");
                strSqlString.AppendFormat("ORDER BY {0},A.LOT_ID " + "\n", QueryCond2);
            }
            System.Windows.Forms.Clipboard.SetText(strSqlString.ToString());
            return strSqlString.ToString();
        }
    }
}

