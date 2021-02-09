using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Miracom.UI;
using Miracom.SmartWeb;
using Miracom.SmartWeb.FWX;
using Miracom.SmartWeb.UI;
using Miracom.SmartWeb.UI.Controls;

namespace Hana.TAT
{
    public partial class TAT050501 : Miracom.SmartWeb.UI.Controls.udcCUSReport002
    {
        private DataTable dtOper;

        /// <summary>
        /// 클  래  스: TAT050501<br/>
        /// 클래스요약: TAT Lot By Oper<br/>
        /// 작  성  자: 정보시스템팀 김준용<br/>
        /// 최초작성일: 2009-04-03<br/>
        /// 상세  설명: TAT Lot 별.<br/>
        /// 변경  내용: <br/>
        /// </summary>
        public TAT050501()
        {
            InitializeComponent();
            UserInit();
            SortInit();
            //GridColumnInit(); //헤더 한줄짜리 
        }

        /// <summary>
        /// 
        /// </summary>
        private void UserInit()
        {
            // 검색 기간 초기화 (전전일,전일)
            udcDate.AutoBinding(DateTime.Now.AddDays(-2).ToString("yyyy-MM-dd"), DateTime.Now.AddDays(-1).ToString("yyyy-MM-dd"));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        private String MakeOperTable()
        {
            StringBuilder strSqlString = new StringBuilder();

            strSqlString.Append("SELECT OPER FROM MWIPOPRDEF WHERE FACTORY " + cdvFactory.SelectedValueToQueryString + " AND OPER NOT IN ('00001','00002') ORDER BY OPER" + "\n");


            return strSqlString.ToString();
        }

        #region SortInit

        /// <summary>
        /// SortInit
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SortInit()
        {
            ((udcTableForm)(this.btnSort.BindingForm)).AddRow("Customer", "MAT.MAT_GRP_1", "MAT.MAT_GRP_1", true);
            ((udcTableForm)(this.btnSort.BindingForm)).AddRow("Family", "MAT.MAT_GRP_2", "MAT.MAT_GRP_2", false);
            ((udcTableForm)(this.btnSort.BindingForm)).AddRow("Package", "MAT.MAT_GRP_3", "MAT.MAT_GRP_3", true);
            ((udcTableForm)(this.btnSort.BindingForm)).AddRow("Type1", "MAT.MAT_GRP_4", "MAT.MAT_GRP_4", false);
            ((udcTableForm)(this.btnSort.BindingForm)).AddRow("Type2", "MAT.MAT_GRP_5", "MAT.MAT_GRP_5", false);
            ((udcTableForm)(this.btnSort.BindingForm)).AddRow("LD Count", "MAT.MAT_GRP_6", "MAT.MAT_GRP_6", true);
            ((udcTableForm)(this.btnSort.BindingForm)).AddRow("Density", "MAT.MAT_GRP_7", "MAT.MAT_GRP_7", false);
            ((udcTableForm)(this.btnSort.BindingForm)).AddRow("Generation", "MAT.MAT_GRP_8", "MAT.MAT_GRP_8", false);
            ((udcTableForm)(this.btnSort.BindingForm)).AddRow("PIN_TYPE", "MAT.MAT_CMF_10", "MAT.MAT_CMF_10", false);
            ((udcTableForm)(this.btnSort.BindingForm)).AddRow("Product", "MAT.MAT_ID", "MAT.MAT_ID", false);
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
            try
            {
                spdData.ActiveSheet.ColumnHeader.Rows.Count = 1;
                spdData.RPT_ColumnInit();

                spdData.RPT_AddBasicColumn("Customer", 0, 0, Visibles.True, Frozen.True, Align.Center, Merge.True, Formatter.String, 80);
                spdData.RPT_AddBasicColumn("Family", 0, 1, Visibles.False, Frozen.True, Align.Center, Merge.True, Formatter.String, 80);
                spdData.RPT_AddBasicColumn("Package", 0, 2, Visibles.True, Frozen.True, Align.Center, Merge.True, Formatter.String, 80);
                spdData.RPT_AddBasicColumn("Type1", 0, 3, Visibles.False, Frozen.True, Align.Center, Merge.True, Formatter.String, 80);
                spdData.RPT_AddBasicColumn("Type2", 0, 4, Visibles.False, Frozen.True, Align.Center, Merge.True, Formatter.String, 80);
                spdData.RPT_AddBasicColumn("LD Count", 0, 5, Visibles.True, Frozen.True, Align.Center, Merge.True, Formatter.String, 80);
                spdData.RPT_AddBasicColumn("Density", 0, 6, Visibles.False, Frozen.True, Align.Center, Merge.True, Formatter.String, 80);
                spdData.RPT_AddBasicColumn("Generation", 0, 7, Visibles.False, Frozen.True, Align.Center, Merge.True, Formatter.String, 80);
                spdData.RPT_AddBasicColumn("Pin Type", 0, 8, Visibles.True, Frozen.True, Align.Center, Merge.True, Formatter.String, 80);
                spdData.RPT_AddBasicColumn("Product", 0, 9, Visibles.True, Frozen.True, Align.Center, Merge.True, Formatter.String, 80);
                spdData.RPT_AddBasicColumn("LOT ID", 0, 10, Visibles.True, Frozen.True, Align.Center, Merge.True, Formatter.String, 80);
                spdData.RPT_AddBasicColumn("Ship Factory", 0, 11, Visibles.True, Frozen.True, Align.Center, Merge.True, Formatter.String, 80);
                spdData.RPT_AddBasicColumn("Ship Qty", 0, 12, Visibles.True, Frozen.True, Align.Center, Merge.True, Formatter.String, 80);
                spdData.RPT_AddBasicColumn("TAT", 0, 13, Visibles.True, Frozen.True, Align.Center, Merge.True, Formatter.String, 80);

                // header 의 Colnum Count
                int headerCount = 13;

                // Header 에 공정 추가하기
                for(int i = 0;i < dtOper.Rows.Count;i++)
                {
                    spdData.RPT_AddBasicColumn("Wait_" + dtOper.Rows[i][0], 0, headerCount, Visibles.True, Frozen.True, Align.Center, Merge.True, Formatter.String, 80);
                    headerCount++;

                    spdData.RPT_AddBasicColumn("Run_" + dtOper.Rows[i][0], 0, headerCount, Visibles.True, Frozen.True, Align.Center, Merge.True, Formatter.String, 80);
                    headerCount++;
                }

                spdData.RPT_ColumnConfigFromTable(btnSort); //Group항목이 있을경우 반드시 선업해줄것.
            }
            catch (Exception ex)
            {
                CmnFunction.ShowMsgBox(ex.Message);
            }
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

            // 공정 코드 가져오기
            dtOper = CmnFunction.oComm.GetFuncDataTable("DYNAMIC", MakeOperTable());

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

                spdData.RPT_ColumnConfigFromTable(btnSort);

                spdData.RPT_AddTotalRow2(10);

                spdData.RPT_AddRowsColor(RowColorType.General, 0, 0, 9, true);

                //3. Total부분 셀머지
                spdData.RPT_FillDataSelectiveCells("Total", 0, 13, 0, 1, true, Align.Center, VerticalAlign.Center);

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

            //    if (cboType.Text.ToString().CompareTo("ALL")==0)
            //    {
            //        //// 세줄짜리 샘플코드(부분합계 제공 안됨-총 합계만 가능.)
            //        spdData.DataSource = dt;

            //        ////표구성에따른 항목 Display
            //        spdData.RPT_ColumnConfigFromTable(btnSort);

            //        ////최상단에 합계 표시
            //        spdData.RPT_AddTotalRow2(10);

            //        ////3행단위로 Sheet 재구성(구분부터 이므로 11개임)
            //        spdData.RPT_DivideRows(8, 3, cdvOper.CountFromToValue + 1);

            //        ////구분항목 값 생성(구분이 들어가는 위치임. 0부터 시작)
            //        spdData.RPT_FillColumnData(9, new string[] { "ACUM", "WAIT", "RUN" });
            //        //spdData.ActiveSheet.DataAutoSizeColumns = false;

            //        ////색상 바꾸고(0부터 구분까지 이므로 11개임)
            //        spdData.RPT_AddRowsColor(RowColorType.General, 3, 0, 9, true);

            //        ////최 상위 셀 Merge(0부터 공정까지 셀머지 : 10개임)
            //        spdData.RPT_FillDataSelectiveCells("Total", 0, 9, 0, 3, true, Align.Center, VerticalAlign.Center);

            //        ////2줄단위로 포멧 생성(Total부터 : 위치는 11임)
            //        //spdData.RPT_RepeatableRowCellTypeChange(11, new Formatter[] { Formatter.Number, Formatter.Number});
            //    }
            //    else
            //    {

            //    }

            //    //4. Column Auto Fit
            //    spdData.RPT_AutoFit(false);
            //    //--------------------------------------------

            //    //Chart 생성
            //    //if (spdData.ActiveSheet.RowCount > 0)
            //        //ShowChart(0);
            //}
            //catch (Exception ex)
            //{
            //    LoadingPopUp.LoadingPopUpHidden();
            //    CmnFunction.ShowMsgBox(ex.Message);
            //}
            //finally
            //{
            //    LoadingPopUp.LoadingPopUpHidden();
            //}
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
        private string MakeSqlString()
        {
            StringBuilder strSqlString = new StringBuilder();

            string QueryCond1 = null;
            string QueryCond2 = null;

            udcTableForm tableForm = (udcTableForm)btnSort.BindingForm;

            QueryCond1 = tableForm.SelectedValueToQueryContainNull;
            QueryCond2 = tableForm.SelectedValue2ToQueryContainNull;

            StringBuilder query1 = new StringBuilder();
            StringBuilder query2 = new StringBuilder();
            for (int i = 0; i < dtOper.Rows.Count; i++)
            {
                query1.Append("		ROUND(SUM(WAIT_00001),2) AS WAIT_00001," + "\n");
                query1.Append("		ROUND(SUM(RUN_00001),2) AS RUN_00001 " + "\n");

                query2.Append("		ROUND(SUM(WAIT_00001),2) AS WAIT_00001," + "\n");
                query2.Append("		ROUND(SUM(RUN_00001),2) AS RUN_00001 " + "\n");

                if (i != dtOper.Rows.Count - 1)
                {
                    query1.Append(" ," + "\n");
                    query2.Append(" ," + "\n");
                }
                else
                {
                    query1.Append("\n");
                    query2.Append("\n");
                }
            }

//            strSqlString.Append("SELECT " + QueryCond1 + ",LOT_ID,SHIP_FACTORY,SHIP_QTY,TAT," + "\n");
//            strSqlString.Append(query1.ToString());
//            strSqlString.Append("FROM   (" + "\n");
//            strSqlString.Append("		SELECT 	HIS.MAT_ID," + "\n");
//            strSqlString.Append("				LTH.LOT_ID, " + "\n");
//            strSqlString.Append("				'HMKA1' AS SHIP_FACTORY, " + "\n");
//            strSqlString.Append("				HIS.SHIP_QTY, " + "\n");
//            strSqlString.Append("				ROUND(TO_DATE(HIS.TRAN_TIME,'YYYYMMDDHH24MISS') - TO_DATE(HIS.LOT_CMF_14,'YYYYMMDDHH24MISS'),2) AS TAT, " + "\n");
//            strSqlString.Append("				LTH.OPER," + "\n");
//            strSqlString.Append("				ROUND(LTH.QUEUE_TIME/60/60/24,2) AS OPER_TAT," + "\n");
//            strSqlString.Append(query2.ToString());
//            //strSqlString.Append("		CASE WHEN ROUND(SUM(WAIT_A0000) + (TAT-SUM(OPER_TAT)),2) < 0" + "\n");
//            //strSqlString.Append("		THEN 0" + "\n");
//            //strSqlString.Append("		ELSE ROUND(SUM(WAIT_A0000) + (TAT-SUM(OPER_TAT)),2)" + "\n");
//            //strSqlString.Append("		END " + "\n");
//            //strSqlString.Append("		AS WAIT_A0000," + "\n");
//            strSqlString.Append("		FROM 	RSUMOUTLTH LTH," + "\n");
//            strSqlString.Append("		(
//                SELECT	MAT_ID,
//                        LOT_ID,
//                        QTY_1,
//                        OLD_QTY_1,
//                        FACTORY,
//                        OLD_FACTORY,
//                        TRAN_TIME,
//                        DECODE(FACTORY,OLD_FACTORY,OLD_QTY_1 - QTY_1,QTY_1) AS SHIP_QTY,
//                        LOT_CMF_14
//                FROM	RWIPLOTHIS
//                WHERE	1=1
//                        AND HIST_DEL_FLAG = ' '
//                        AND TRAN_CODE = 'SHIP'
//                        AND OLD_FACTORY  = 'HMKA1' 
//                        AND FACTORY IN ('HMKT1','CUSTOMER')
//                        AND OLD_OPER IN ('AZ010')
//                        AND MAT_ID LIKE '%'
//                        --AND CM_FIELD_3 = 'PP'
//                        AND TRAN_TIME BETWEEN '20090401220000' AND '20090402215959'
//                ) HIS
//        WHERE	1=1
//                AND LTH.MAT_ID = HIS.MAT_ID
//                AND LTH.LOT_ID = HIS.LOT_ID
//                AND LTH.FACTORY = 'HMKA1'
//        ) TAT,
//        MWIPMATDEF MAT
//WHERE	1=1
//        AND TAT.MAT_ID=MAT.MAT_ID
//        AND MAT.FACTORY = 'HMKA1'
//GROUP BY MAT.MAT_GRP_1,  ' ', MAT.MAT_GRP_3,  ' ',  ' ', MAT.MAT_GRP_6,  ' ',  ' ',  ' ',  ' ',LOT_ID,SHIP_FACTORY,SHIP_QTY,TAT
//ORDER BY MAT.MAT_GRP_1,  ' ', MAT.MAT_GRP_3,  ' ',  ' ', MAT.MAT_GRP_6,  ' ',  ' ',  ' ',  ' ',LOT_ID,SHIP_FACTORY,SHIP_QTY,TAT
//            strSqlString.Append("		FROM 	RSUMOUTLTH LTH," + "\n");
//            strSqlString.Append("				(" + "\n");
//            strSqlString.Append("				SELECT	MAT_ID," + "\n");
//            strSqlString.Append("						LOT_ID," + "\n");
//            strSqlString.Append("						QTY_1," + "\n");
//            strSqlString.Append("						OLD_QTY_1," + "\n");
//            strSqlString.Append("						FACTORY," + "\n");
//            strSqlString.Append("						OLD_FACTORY," + "\n");
//            strSqlString.Append("						TRAN_TIME," + "\n");
//            strSqlString.Append("						DECODE(FACTORY,OLD_FACTORY,OLD_QTY_1 - QTY_1,QTY_1) AS SHIP_QTY," + "\n");
//            strSqlString.Append("						LOT_CMF_14" + "\n");
//            strSqlString.Append("				FROM	RWIPLOTHIS" + "\n");
//            strSqlString.Append("				WHERE	1=1" + "\n");
//            strSqlString.Append("						AND HIST_DEL_FLAG = ' '" + "\n");
//            strSqlString.Append("						AND TRAN_CODE = 'SHIP'" + "\n");
//            strSqlString.Append("						AND OLD_FACTORY " + cdvFactory.SelectedValueToQueryString + "\n");

//            if (cdvFactory.txtValue.Equals("HMKA1"))
//            {
//                strSqlString.Append("						AND FACTORY IN ('HMKT1','CUSTOMER')" + "\n");
//                strSqlString.Append("						AND OLD_OPER IN ('AZ010')" + "\n");
//            }
//            else if (cdvFactory.txtValue.Equals("HMKT1"))
//            {
//                strSqlString.Append("						AND FACTORY IN ('FGS','CUSTOMER')" + "\n");
//                strSqlString.Append("						AND OLD_OPER IN ('TZ010')" + "\n");
//            }
//            else if (cdvFactory.txtValue.Equals("HMKE1"))
//            {
//                strSqlString.Append("						AND FACTORY IN ('HMKA1','CUSTOMER')" + "\n");
//                strSqlString.Append("						AND OLD_OPER IN ('EZ010')" + "\n");
//            }

//            if (!String.IsNullOrEmpty(txtSearchProduct.Text))
//            {
//                strSqlString.Append("						AND MAT_ID LIKE '" + txtSearchProduct.Text + "'" + "\n");
//            }
            
//            strSqlString.Append("						--AND CM_FIELD_3 = 'PP'" + "\n");
//            strSqlString.Append("						AND TRAN_TIME BETWEEN '" + udcDate.Start_Tran_Time + "' AND '" + udcDate.End_Tran_Time + "'" + "\n");
//            strSqlString.Append("				) HIS" + "\n");
//            strSqlString.Append("		WHERE	1=1" + "\n");
//            strSqlString.Append("				AND LTH.MAT_ID = HIS.MAT_ID" + "\n");
//            strSqlString.Append("				AND LTH.LOT_ID = HIS.LOT_ID" + "\n");
//            strSqlString.Append("				AND LTH.FACTORY = 'HMKA1'" + "\n");
//            strSqlString.Append("		) TAT," + "\n");
//            strSqlString.Append("		MWIPMATDEF MAT" + "\n");
//            strSqlString.Append("WHERE	1=1" + "\n");
//            strSqlString.Append("		AND TAT.MAT_ID=MAT.MAT_ID" + "\n");
//            strSqlString.Append("		AND MAT.FACTORY = 'HMKA1'" + "\n");
//            strSqlString.Append("GROUP BY " + QueryCond1 + ",LOT_ID,SHIP_FACTORY,SHIP_QTY,TAT" + "\n");
//            strSqlString.Append("ORDER BY " + QueryCond2 + ",LOT_ID,SHIP_FACTORY,SHIP_QTY,TAT" + "\n");

//        strSqlString.Append(@" SELECT " + QueryCond1 + @",LOT_ID,SHIP_FACTORY,SHIP_QTY,TAT,
//        " + query1 + @" FROM ( SELECT HIS.MAT_ID,
// LTH.LOT_ID,
// 'HMKA1' AS SHIP_FACTORY,
// HIS.SHIP_QTY,
// ROUND(TO_DATE(HIS.TRAN_TIME,'YYYYMMDDHH24MISS') - TO_DATE(HIS.LOT_CMF_14,'YYYYMMDDHH24MISS'),2) AS TAT,
// LTH.OPER,
// ROUND(LTH.QUEUE_TIME/60/60/24,2) AS OPER_TAT,				DECODE(LTH.OPER,'00001',LTH.PROC_TIME/60/60/24) AS RUN_00001,
// " + query2 + @"
// FROM RSUMOUTLTH LTH,
// (
// SELECT MAT_ID,
// LOT_ID,
// QTY_1,
// OLD_QTY_1,
// FACTORY,
// OLD_FACTORY,
// TRAN_TIME,
// DECODE(FACTORY,OLD_FACTORY,OLD_QTY_1 - QTY_1,QTY_1) AS SHIP_QTY,
// LOT_CMF_14
// FROM RWIPLOTHIS
// WHERE 1=1
// AND HIST_DEL_FLAG = ' '
// AND TRAN_CODE = 'SHIP'
// AND OLD_FACTORY  " + cdvFactory.SelectedValueToQueryString + @"
//");

//             if (cdvFactory.txtValue.Equals("HMKA1")){
//                 strSqlString.Append(" AND FACTORY IN ('HMKT1','CUSTOMER')" + "\n");
//                 strSqlString.Append(" AND OLD_OPER IN ('AZ010')");
//             }else if (cdvFactory.txtValue.Equals("HMKT1")){
//                 strSqlString.Append(" AND FACTORY IN ('FGS','CUSTOMER')" + "\n");
//                 strSqlString.Append(" AND OLD_OPER IN ('TZ010')");
//             }else if (cdvFactory.txtValue.Equals("HMKE1")){
//                 strSqlString.Append(" AND FACTORY IN ('HMKA1','CUSTOMER')" + "\n");
//                 strSqlString.Append(" AND OLD_OPER IN ('EZ010')");
//             }
//             strSqlString.Append(@"
// AND MAT_ID LIKE '%'
// --AND CM_FIELD_3 = 'PP'
// AND TRAN_TIME BETWEEN '" + udcDate.Start_Tran_Time + " ' AND '" + udcDate.End_Tran_Time + @"'
// ) HIS
// WHERE 1=1
// AND LTH.MAT_ID = HIS.MAT_ID
// AND LTH.LOT_ID = HIS.LOT_ID
// AND LTH.FACTORY " + cdvFactory.SelectedValueToQueryString + @"
// ) TAT,
// MWIPMATDEF MAT
//WHERE	1=1
// AND TAT.MAT_ID=MAT.MAT_ID
// AND MAT.FACTORY " + cdvFactory.SelectedValueToQueryString + @"
//GROUP BY " + QueryCond1 + @",LOT_ID,SHIP_FACTORY,SHIP_QTY,TAT
//ORDER BY " + QueryCond2 + @" ,LOT_ID,SHIP_FACTORY,SHIP_QTY,TAT
//");

            

strSqlString.Append("SELECT " + QueryCond1 + ",LOT_ID,SHIP_FACTORY,SHIP_QTY,TAT," + "\n");
strSqlString.Append(query1.ToString());
strSqlString.Append("FROM	(" + "\n");
strSqlString.Append("		SELECT 	HIS.MAT_ID," + "\n");
strSqlString.Append("				LTH.LOT_ID," + "\n");
strSqlString.Append("				'HMKA1' AS SHIP_FACTORY," + "\n");
strSqlString.Append("				HIS.SHIP_QTY," + "\n");
strSqlString.Append("				ROUND(TO_DATE(HIS.TRAN_TIME,'YYYYMMDDHH24MISS') - TO_DATE(HIS.LOT_CMF_14,'YYYYMMDDHH24MISS'),2) AS TAT," + "\n");
strSqlString.Append("				LTH.OPER," + "\n");
strSqlString.Append("				ROUND(LTH.QUEUE_TIME/60/60/24,2) AS OPER_TAT,				DECODE(LTH.OPER,'00001',LTH.PROC_TIME/60/60/24) AS RUN_00001," + "\n");
strSqlString.Append(query2.ToString());
strSqlString.Append("		FROM RSUMOUTLTH LTH," + "\n");
strSqlString.Append("				(" + "\n");
strSqlString.Append("				SELECT	MAT_ID," + "\n");
strSqlString.Append("						LOT_ID," + "\n");
strSqlString.Append("						QTY_1," + "\n");
strSqlString.Append("						OLD_QTY_1," + "\n");
strSqlString.Append("						FACTORY," + "\n");
strSqlString.Append("						OLD_FACTORY," + "\n");
strSqlString.Append("						TRAN_TIME," + "\n");
strSqlString.Append("						DECODE(FACTORY,OLD_FACTORY,OLD_QTY_1 - QTY_1,QTY_1) AS SHIP_QTY," + "\n");
strSqlString.Append("						LOT_CMF_14" + "\n");
strSqlString.Append("				FROM	RWIPLOTHIS" + "\n");
strSqlString.Append("				WHERE	1=1" + "\n");
strSqlString.Append("						AND HIST_DEL_FLAG = ' '" + "\n");
strSqlString.Append("						AND TRAN_CODE = 'SHIP'" + "\n");
strSqlString.Append("						AND OLD_FACTORY " + cdvFactory.SelectedValueToQueryString + "\n");
            if (cdvFactory.txtValue.Equals("HMKA1")){
                strSqlString.Append("						AND FACTORY IN ('HMKT1','CUSTOMER')" + "\n");
                strSqlString.Append("						AND OLD_OPER IN ('AZ010')" + "\n");
            }
strSqlString.Append("						AND MAT_ID LIKE '%'" + "\n");
strSqlString.Append("						--AND CM_FIELD_3 = 'PP'" + "\n");
strSqlString.Append("						AND TRAN_TIME BETWEEN '20090401220000' AND '20090402215959'" + "\n");
strSqlString.Append("				) HIS" + "\n");
strSqlString.Append("		WHERE	1=1" + "\n");
strSqlString.Append("				AND LTH.MAT_ID = HIS.MAT_ID" + "\n");
strSqlString.Append("				AND LTH.LOT_ID = HIS.LOT_ID" + "\n");
strSqlString.Append("				AND LTH.FACTORY = 'HMKA1'" + "\n");
strSqlString.Append("		) TAT," + "\n");
strSqlString.Append("		MWIPMATDEF MAT" + "\n");
strSqlString.Append("WHERE	1=1" + "\n");
strSqlString.Append("		AND TAT.MAT_ID=MAT.MAT_ID" + "\n");
strSqlString.Append("		AND MAT.FACTORY = 'HMKA1'" + "\n");
strSqlString.Append("GROUP BY " + QueryCond1 + ",LOT_ID,SHIP_FACTORY,SHIP_QTY,TAT" + "\n");
strSqlString.Append("ORDER BY " + QueryCond2 + ",LOT_ID,SHIP_FACTORY,SHIP_QTY,TAT " + "\n");
            System.Windows.Forms.Clipboard.SetText(strSqlString.ToString());

            return strSqlString.ToString();
        }

        #endregion

        #region ShowChart

        private void ShowChart(int rowCount)
        {
            //// 차트 설정
            //udcChartFX1.RPT_2_ClearData();
            //udcChartFX1.RPT_3_OpenData(4, udcDurationDate1.SubtractBetweenFromToDate + 1);
            //int[] tat_columns = new Int32[udcDurationDate1.SubtractBetweenFromToDate + 1];
            //int[] in_columns = new Int32[udcDurationDate1.SubtractBetweenFromToDate + 1];
            //int[] out_columns = new Int32[udcDurationDate1.SubtractBetweenFromToDate + 1];
            //int[] wip_columns = new Int32[udcDurationDate1.SubtractBetweenFromToDate + 1];
            //int[] columnsHeader = new Int32[udcDurationDate1.SubtractBetweenFromToDate + 1];

            //for (int i = 0; i < wip_columns.Length; i++)
            //{
            //    columnsHeader[i] = 9 + i;
            //    tat_columns[i] = 9 + i;
            //    in_columns[i] = 9 + i;
            //    out_columns[i] = 9 + i;
            //    wip_columns[i] = 9 + i;
            //}

            ////max1 = udcChartFX1.RPT_4_AddData(spdData, new int[] { max_rownum }, wip_columns, SeriseType.Rows);

            ////double max1 = 0;
            ////max1 = udcChartFX1.RPT_4_AddData(spdData, new int[] { rowCount + 0 }, wip_columns, SeriseType.Rows);

            ////TAT
            //double max1 = udcChartFX1.RPT_4_AddData(spdData, new int[] { rowCount + 0 }, tat_columns, SeriseType.Rows);
            ////IN
            //double max2 = udcChartFX1.RPT_4_AddData(spdData, new int[] { rowCount + 1 }, in_columns, SeriseType.Rows);
            ////OUT
            //double max3 = udcChartFX1.RPT_4_AddData(spdData, new int[] { rowCount + 2 }, out_columns, SeriseType.Rows);
            ////WIP
            //double max4 = udcChartFX1.RPT_4_AddData(spdData, new int[] { rowCount + 3 }, wip_columns, SeriseType.Rows);

            //udcChartFX1.RPT_5_CloseData();

            ////각 Serise별로 다른 타입을 사용할 경우
            //udcChartFX1.RPT_6_SetGallery(ChartType.Line, 0, 1, "TAT [단위 : day]", AsixType.Y, DataTypes.Initeger, max1 * 1.2);
            //udcChartFX1.RPT_6_SetGallery(ChartType.Bar, 1, 1, "IN [단위 : ea]", AsixType.Y2, DataTypes.Initeger, max2 * 1.2);
            //udcChartFX1.RPT_6_SetGallery(ChartType.Bar, 2, 1, "OUT [단위 : ea]", AsixType.Y2, DataTypes.Initeger, max3 * 1.2);
            //udcChartFX1.RPT_6_SetGallery(ChartType.Bar, 3, 1, "WIP [단위 : ea]", AsixType.Y2, DataTypes.Initeger, max4 * 1.2);


            ////각 Serise별로 동일한 타입을 사용할 경우
            ////udcChartFX1.RPT_6_SetGallery(ChartType.Bar, "[단위 : sls]", AsixType.Y, DataTypes.Initeger, max1 * 1.2);
            ////udcChartFX1.RPT_6_SetGallery(ChartType.Line, "[단위 : EA]", AsixType.Y, DataTypes.Initeger, max1 * 1.2);

            //udcChartFX1.RPT_7_SetXAsixTitleUsingSpreadHeader(spdData, 0, columnsHeader);
            ////udcChartFX1.RPT_8_SetSeriseLegend(spdData, rows, 9-1, SoftwareFX.ChartFX.Docked.Top);
            //udcChartFX1.RPT_8_SetSeriseLegend(new string[] { "TAT", "IN", "OUT", "WIP" }, SoftwareFX.ChartFX.Docked.Top);
            //udcChartFX1.PointLabels = true;
            //udcChartFX1.AxisY.Gridlines = true;
            //udcChartFX1.AxisY.DataFormat.Decimals = 3;
        }
        #endregion

        #endregion

        #region ToExcel

        /// <summary>
        /// ToExcel
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnExcelExport_Click(object sender, EventArgs e)
        {
            //ExcelHelper.Instance.subMakeExcel(spdData, udcChartFX1, this.lblTitle.Text, null, null);
        }

        #endregion
    }
}
