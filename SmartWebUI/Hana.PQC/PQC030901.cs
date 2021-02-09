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
    public partial class PQC030901 : Miracom.SmartWeb.UI.Controls.udcCUSReport002
    {

        #region " PQC030901 : Program Initial "

        /// <summary>
        /// 클  래  스: PQC030901<br/>
        /// 클래스요약: 특성검사 결과 조회<br/>
        /// 작  성  자: 하나마이크론 임종우<br/>
        /// 최초작성일: 2009-06-29<br/>
        /// 상세  설명: 특성검사 결과 조회<br/>
        /// 변경  내용: 김준용대리 요청
        ///            [2009-07-24] DETAIL, GROUP 버튼 추가 <br/>
        ///            [2009-07-27] GROUP : PIN_TYPE 추가
        /// 변  경  자: 조앤시스템 장은희 <br />
        /// 2012-04-12-임종우 : 검색 기간 컨트롤 변경.
        /// 2018-12-19-임종우 : HMKT1 조회 오류 수정
        /// </summary>
        /// 
        public PQC030901()
        {
            InitializeComponent();
            cdvFromToDate.AutoBinding();
            SortInit();
            GridColumnInit();
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
            if (cdvCharID.Text.Equals("ALL"))
            {
                if (cdvFactory.Text.Trim() == "HMKB1")
                    CmnFunction.ShowMsgBox(RptMessages.GetMessage("STD048", GlobalVariable.gcLanguage));
                else
                    //CHAR_ID와 Character ID와 같은 용어여서 STD043으로 통일
                    CmnFunction.ShowMsgBox(RptMessages.GetMessage("STD043", GlobalVariable.gcLanguage));

                return false;
            }

            return true;
        }

        private void GetCharIDData()
        {
            string strQuery = string.Empty;
            
            cdvCharID.Init();

            if (cdvFactory.Text.Trim() == "HMKB1")
            {
                cdvCharID.ConditionText = "SPEC_ID";

                //strQuery += "SELECT DISTINCT SPEC_ID Code, '' Data" + "\n";
                //strQuery += "  FROM VSPCSPECDAT@TESTMES " + "\n";
                //strQuery += "ORDER BY SPEC_ID " + "\n";

                strQuery += "SELECT DISTINCT C.SPEC_ID AS Code, '' Data" + "\n";
                strQuery += "  FROM TQS_EDCPLAN_CONFIG@RPTTOFA A  " + "\n";
                strQuery += "     , TQS_SPEC_CONFIG@RPTTOFA B  " + "\n";
                strQuery += "     , TQS_SPEC_DEFINE@RPTTOFA C  " + "\n";
                strQuery += "WHERE 1=1 " + "\n";
                strQuery += "AND A.PLAN_SEQ = B.PLAN_SEQ " + "\n";
                strQuery += "AND B.SPEC_PARENT_SEQ = C.PARENT_SEQ " + "\n";
                strQuery += "AND A.FACTORY "+ cdvFactory.SelectedValueToQueryString + "\n";
                strQuery += "ORDER BY SPEC_ID " + "\n";
            }
            else
            {
                cdvCharID.ConditionText = "CHAR_ID";

                strQuery += "SELECT DISTINCT CHAR_ID Code, '' Data" + "\n";
                strQuery += "  FROM MEDCCHRDEF " + "\n";
                strQuery += "ORDER BY CHAR_ID " + "\n";
            }

            cdvCharID.sDynamicQuery = strQuery;
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
                if (cdvFactory.Text.Trim() == "HMKB1")
                {
                    spdData.RPT_ColumnInit();

                    spdData.RPT_AddBasicColumn("Customer", 0, 0, Visibles.True, Frozen.True, Align.Center, Merge.True, Formatter.String, 70);
                    spdData.RPT_AddBasicColumn("Bumping Type", 0, 1, Visibles.True, Frozen.True, Align.Center, Merge.True, Formatter.String, 70);
                    spdData.RPT_AddBasicColumn("Operation Flow", 0, 2, Visibles.False, Frozen.True, Align.Left, Merge.True, Formatter.String, 70);
                    spdData.RPT_AddBasicColumn("Layer classification", 0, 3, Visibles.False, Frozen.True, Align.Center, Merge.True, Formatter.String, 70);
                    spdData.RPT_AddBasicColumn("PKG Type", 0, 4, Visibles.True, Frozen.True, Align.Center, Merge.True, Formatter.String, 50);
                    spdData.RPT_AddBasicColumn("RDL Plating", 0, 5, Visibles.False, Frozen.True, Align.Center, Merge.True, Formatter.String, 50);
                    spdData.RPT_AddBasicColumn("Final Bump", 0, 6, Visibles.True, Frozen.True, Align.Center, Merge.True, Formatter.String, 50);
                    spdData.RPT_AddBasicColumn("Sub. Material", 0, 7, Visibles.False, Frozen.True, Align.Center, Merge.True, Formatter.String, 50);
                    spdData.RPT_AddBasicColumn("Size", 0, 8, Visibles.False, Frozen.True, Align.Center, Merge.True, Formatter.String, 50);
                    spdData.RPT_AddBasicColumn("Thickness", 0, 9, Visibles.False, Frozen.True, Align.Center, Merge.True, Formatter.String, 50);
                    spdData.RPT_AddBasicColumn("Flat Type", 0, 10, Visibles.False, Frozen.True, Align.Center, Merge.True, Formatter.String, 50);
                    spdData.RPT_AddBasicColumn("Wafer Orientation", 0, 11, Visibles.False, Frozen.True, Align.Center, Merge.True, Formatter.String, 50);
                    
                    spdData.RPT_AddBasicColumn("TRAN_TIME", 0, 12, Visibles.True, Frozen.False, Align.Left, Merge.False, Formatter.String, 90);
                    spdData.RPT_AddBasicColumn("LOT_ID", 0, 13, Visibles.True, Frozen.False, Align.Left, Merge.False, Formatter.String, 100);
                    spdData.RPT_AddBasicColumn("MAT_ID", 0, 14, Visibles.True, Frozen.False, Align.Left, Merge.False, Formatter.String, 170);
                    spdData.RPT_AddBasicColumn("OPER", 0, 15, Visibles.True, Frozen.False, Align.Left, Merge.False, Formatter.String, 70);
                    spdData.RPT_AddBasicColumn("MEAS_RES_ID", 0, 16, Visibles.True, Frozen.False, Align.Left, Merge.False, Formatter.String, 70);
                    spdData.RPT_AddBasicColumn("EDC_PLAN", 0, 17, Visibles.True, Frozen.False, Align.Left, Merge.False, Formatter.String, 100);
                    spdData.RPT_AddBasicColumn("SPEC_ID", 0, 18, Visibles.True, Frozen.False, Align.Left, Merge.False, Formatter.String, 130);
                    spdData.RPT_AddBasicColumn("UNIT_ID", 0, 19, Visibles.True, Frozen.False, Align.Left, Merge.False, Formatter.String, 70);
                    //spdData.RPT_AddBasicColumn("Upper Spec", 0, 20, Visibles.True, Frozen.False, Align.Left, Merge.False, Formatter.String, 70);
                    //spdData.RPT_AddBasicColumn("Lower Spec", 0, 21, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.String, 70);
                    spdData.RPT_AddBasicColumn("Upper Spec", 0, 20, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 70);
                    spdData.RPT_AddBasicColumn("Lower Spec", 0, 21, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 70);
                    spdData.RPT_AddBasicColumn("VALUE_1", 0, 22, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.String, 50);
                    spdData.RPT_AddBasicColumn("VALUE_2", 0, 23, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.String, 50);
                    spdData.RPT_AddBasicColumn("VALUE_3", 0, 24, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.String, 50);
                    spdData.RPT_AddBasicColumn("VALUE_4", 0, 25, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.String, 50);
                    spdData.RPT_AddBasicColumn("VALUE_5", 0, 26, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.String, 50);
                    spdData.RPT_AddBasicColumn("VALUE_6", 0, 27, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.String, 50);
                    spdData.RPT_AddBasicColumn("VALUE_7", 0, 28, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.String, 50);
                    spdData.RPT_AddBasicColumn("VALUE_8", 0, 29, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.String, 50);
                    spdData.RPT_AddBasicColumn("VALUE_9", 0, 30, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.String, 50);
                    spdData.RPT_AddBasicColumn("VALUE_10", 0, 31, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.String, 50);
                    spdData.RPT_AddBasicColumn("VALUE_11", 0, 32, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.String, 50);
                    spdData.RPT_AddBasicColumn("VALUE_12", 0, 33, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.String, 50);
                    spdData.RPT_AddBasicColumn("VALUE_13", 0, 34, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.String, 50);
                    spdData.RPT_AddBasicColumn("VALUE_14", 0, 35, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.String, 50);
                    spdData.RPT_AddBasicColumn("VALUE_15", 0, 36, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.String, 50);
                    spdData.RPT_AddBasicColumn("VALUE_16", 0, 37, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.String, 50);
                    spdData.RPT_AddBasicColumn("VALUE_17", 0, 38, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.String, 50);
                    spdData.RPT_AddBasicColumn("VALUE_18", 0, 39, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.String, 50);
                    spdData.RPT_AddBasicColumn("VALUE_19", 0, 40, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.String, 50);
                    spdData.RPT_AddBasicColumn("VALUE_20", 0, 41, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.String, 50);
                    
                    // Group항목이 있을경우 반드시 선언해줄것.
                    //spdData.RPT_ColumnConfigFromTable(btnSort);
                    spdData.RPT_ColumnConfigFromTable_New(btnSort);
                }
                else
                {
                    spdData.RPT_ColumnInit();

                    spdData.RPT_AddBasicColumn("CUSTOMER", 0, 0, Visibles.False, Frozen.True, Align.Center, Merge.True, Formatter.String, 70);
                    spdData.RPT_AddBasicColumn("FAMILY", 0, 1, Visibles.False, Frozen.True, Align.Center, Merge.True, Formatter.String, 60);
                    spdData.RPT_AddBasicColumn("PACKAGE", 0, 2, Visibles.True, Frozen.True, Align.Center, Merge.True, Formatter.String, 60);
                    spdData.RPT_AddBasicColumn("TYPE1", 0, 3, Visibles.False, Frozen.True, Align.Center, Merge.True, Formatter.String, 50);
                    spdData.RPT_AddBasicColumn("TYPE2", 0, 4, Visibles.False, Frozen.True, Align.Center, Merge.True, Formatter.String, 50);
                    spdData.RPT_AddBasicColumn("LD COUNT", 0, 5, Visibles.True, Frozen.True, Align.Center, Merge.True, Formatter.String, 60);
                    spdData.RPT_AddBasicColumn("DENSITY", 0, 6, Visibles.False, Frozen.True, Align.Center, Merge.True, Formatter.String, 60);
                    spdData.RPT_AddBasicColumn("GENERATION", 0, 7, Visibles.False, Frozen.True, Align.Center, Merge.True, Formatter.String, 80);
                    spdData.RPT_AddBasicColumn("PIN TYPE", 0, 8, Visibles.True, Frozen.True, Align.Center, Merge.True, Formatter.String, 130);

                    spdData.RPT_AddBasicColumn("TRAN_TIME", 0, 9, Visibles.True, Frozen.False, Align.Left, Merge.False, Formatter.String, 90);
                    spdData.RPT_AddBasicColumn("LOT_ID", 0, 10, Visibles.True, Frozen.False, Align.Left, Merge.False, Formatter.String, 100);
                    spdData.RPT_AddBasicColumn("MAT_ID", 0, 11, Visibles.True, Frozen.False, Align.Left, Merge.False, Formatter.String, 170);
                    spdData.RPT_AddBasicColumn("OPER", 0, 12, Visibles.True, Frozen.False, Align.Left, Merge.False, Formatter.String, 70);
                    spdData.RPT_AddBasicColumn("MEAS_RES_ID", 0, 13, Visibles.True, Frozen.False, Align.Left, Merge.False, Formatter.String, 70);
                    spdData.RPT_AddBasicColumn("COL_SET_ID", 0, 14, Visibles.True, Frozen.False, Align.Left, Merge.False, Formatter.String, 100);
                    spdData.RPT_AddBasicColumn("CHAR_ID", 0, 15, Visibles.True, Frozen.False, Align.Left, Merge.False, Formatter.String, 130);
                    spdData.RPT_AddBasicColumn("UNIT_ID", 0, 16, Visibles.True, Frozen.False, Align.Left, Merge.False, Formatter.String, 70);
                    spdData.RPT_AddBasicColumn("VALUE_1", 0, 17, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.String, 50);
                    spdData.RPT_AddBasicColumn("VALUE_2", 0, 18, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.String, 50);
                    spdData.RPT_AddBasicColumn("VALUE_3", 0, 19, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.String, 50);
                    spdData.RPT_AddBasicColumn("VALUE_4", 0, 20, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.String, 50);
                    spdData.RPT_AddBasicColumn("VALUE_5", 0, 21, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.String, 50);
                    spdData.RPT_AddBasicColumn("VALUE_6", 0, 22, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.String, 50);
                    spdData.RPT_AddBasicColumn("VALUE_7", 0, 23, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.String, 50);
                    spdData.RPT_AddBasicColumn("VALUE_8", 0, 24, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.String, 50);
                    spdData.RPT_AddBasicColumn("VALUE_9", 0, 25, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.String, 50);
                    spdData.RPT_AddBasicColumn("VALUE_10", 0, 26, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.String, 50);
                    spdData.RPT_AddBasicColumn("VALUE_11", 0, 27, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.String, 50);
                    spdData.RPT_AddBasicColumn("VALUE_12", 0, 28, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.String, 50);
                    spdData.RPT_AddBasicColumn("VALUE_13", 0, 29, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.String, 50);
                    spdData.RPT_AddBasicColumn("VALUE_14", 0, 30, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.String, 50);
                    spdData.RPT_AddBasicColumn("VALUE_15", 0, 31, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.String, 50);
                    spdData.RPT_AddBasicColumn("Inspector", 0, 32, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.String, 50);

                    // Group항목이 있을경우 반드시 선언해줄것.
                    //spdData.RPT_ColumnConfigFromTable(btnSort);
                    spdData.RPT_ColumnConfigFromTable_New(btnSort);
                }
                                
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
            if (cdvFactory.Text.Trim() == "HMKB1")
            {
                ((udcTableForm)(this.btnSort.BindingForm)).Clear();
                ((udcTableForm)(this.btnSort.BindingForm)).AddRow("CUSTOMER", "MAT.MAT_GRP_1", "NVL((SELECT DATA_1 FROM MGCMTBLDAT WHERE TABLE_NAME = 'H_CUSTOMER' AND KEY_1 = MAT.MAT_GRP_1 AND ROWNUM=1), '-') AS CUSTOMER", "MAT_GRP_1", true);
                ((udcTableForm)(this.btnSort.BindingForm)).AddRow("BUMPING TYPE", "MAT.MAT_GRP_2", "MAT.MAT_GRP_2 AS BUMPING_TYPE", "MAT_GRP_2", true);
                ((udcTableForm)(this.btnSort.BindingForm)).AddRow("PROCESS FLOW", "MAT.MAT_GRP_3", "MAT.MAT_GRP_3 AS PROCESS_FLOW", "MAT_GRP_3", false);
                ((udcTableForm)(this.btnSort.BindingForm)).AddRow("Layer classification", "MAT.MAT_GRP_4", "MAT.MAT_GRP_4 AS LAYER", "MAT_GRP_4", false);
                ((udcTableForm)(this.btnSort.BindingForm)).AddRow("PKG TYPE", "MAT.MAT_GRP_5", "MAT.MAT_GRP_5 AS PKG_TYPE", "MAT_GRP_5", true);
                ((udcTableForm)(this.btnSort.BindingForm)).AddRow("RDL PLATING", "MAT.MAT_GRP_6", "MAT.MAT_GRP_6 AS RDL_PLATING", "MAT_GRP_6", false);
                ((udcTableForm)(this.btnSort.BindingForm)).AddRow("FINAL BUMP", "MAT.MAT_GRP_7", "MAT.MAT_GRP_7 AS FINAL_BUMP", "MAT_GRP_7", true);
                ((udcTableForm)(this.btnSort.BindingForm)).AddRow("SUB. MATERIAL", "MAT.MAT_GRP_8", "MAT.MAT_GRP_8 AS SUB_MATERIAL", "MAT_GRP_8", false);
                ((udcTableForm)(this.btnSort.BindingForm)).AddRow("SIZE", "MAT.MAT_CMF_14", "MAT.MAT_CMF_14 AS WF_SIZE", "MAT_CMF_14", false);
                ((udcTableForm)(this.btnSort.BindingForm)).AddRow("THICKNESS", "MAT.MAT_CMF_2", "MAT.MAT_CMF_2 AS THICKNESS", "MAT_CMF_2", false);
                ((udcTableForm)(this.btnSort.BindingForm)).AddRow("FLAT TYPE", "MAT.MAT_CMF_3", "MAT.MAT_CMF_3 AS FLAT_TYPE", "MAT_CMF_3", false);
                ((udcTableForm)(this.btnSort.BindingForm)).AddRow("WAFER ORIENTATION", "MAT.MAT_CMF_4", "MAT.MAT_CMF_4 AS WAFER_ORIENTATION", "MAT_CMF_4", false);
            }
            else
            {
                ((udcTableForm)(this.btnSort.BindingForm)).Clear();
                ((udcTableForm)(this.btnSort.BindingForm)).AddRow("CUSTOMER", "MAT.MAT_GRP_1", "MAT_GRP_1", "NVL((SELECT DATA_1 FROM MGCMTBLDAT WHERE TABLE_NAME = 'H_CUSTOMER' AND KEY_1 = MAT_GRP_1 AND ROWNUM=1), '-') AS CUSTOMER", true);
                ((udcTableForm)(this.btnSort.BindingForm)).AddRow("FAMILY", "MAT.MAT_GRP_2", "MAT.MAT_GRP_2 AS FAMILY", "MIN(FAB_SEQ) AS FAMILY", false);
                ((udcTableForm)(this.btnSort.BindingForm)).AddRow("PACKAGE", "MAT.MAT_GRP_3", "MAT.MAT_GRP_3 AS PACKAGE", false);
                ((udcTableForm)(this.btnSort.BindingForm)).AddRow("TYPE1", "MAT.MAT_GRP_4", "MAT.MAT_GRP_4 AS TYPE1", false);
                ((udcTableForm)(this.btnSort.BindingForm)).AddRow("TYPE2", "MAT.MAT_GRP_5", "MAT.MAT_GRP_5 AS TYPE2", false);
                ((udcTableForm)(this.btnSort.BindingForm)).AddRow("LD COUNT", "MAT.MAT_GRP_6", "MAT.MAT_GRP_6 AS \"LD COUNT\"", false);
                ((udcTableForm)(this.btnSort.BindingForm)).AddRow("DENSITY", "MAT.MAT_GRP_7", "MAT.MAT_GRP_7 AS DENSITY", false);
                ((udcTableForm)(this.btnSort.BindingForm)).AddRow("GENERATION", "MAT.MAT_GRP_8", "MAT.MAT_GRP_8 AS GENERATION", false);
                ((udcTableForm)(this.btnSort.BindingForm)).AddRow("PIN TYPE", "MAT.MAT_CMF_10", "MAT.MAT_CMF_10 AS PIN_TYPE", true); 
            }
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
            string strFromDate = null, strToDate = null;

            strFromDate = cdvFromToDate.ExactFromDate;
            strToDate = cdvFromToDate.ExactToDate;

            //추가
            udcTableForm tableForm = (udcTableForm)btnSort.BindingForm;

            QueryCond1 = tableForm.SelectedValueToQueryContainNull;

            // 쿼리
            if (cdvFactory.Text.Trim() == "HMKB1")
            {
                QueryCond2 = tableForm.SelectedValue3ToQueryContainNull;

                //VSPCLOTDAT 에서 조회하도록 수정   @RPTTOMES
                strSqlString.Append("WITH DT AS ( " + " \n");
                strSqlString.Append("    SELECT " + QueryCond1 + " \n");
                strSqlString.Append("         , DAT.DATA_SEQ, DAT.TRAN_TIME, DAT.LOT_ID, DAT.MAT_ID, DAT.STEP_ID AS OPER, DAT.RES_ID   " + "\n");
                strSqlString.Append("         , DAT.USR_CMF_07 AS PLAN_NAME, DAT.SPEC_ID, DAT.USR_CMF_08 AS VALUE_UNIT, DAT.USL, DAT.LSL " + "\n");
                strSqlString.Append("      FROM TQS_SUMMARY_DATA@RPTTOFA DAT, MWIPMATDEF MAT " + "\n");
                strSqlString.Append("     WHERE 1=1 " + "\n");
                strSqlString.Append("       AND DAT.FACTORY = MAT.FACTORY  " + "\n");
                strSqlString.Append("       AND DAT.MAT_ID = MAT.MAT_ID " + "\n");
                strSqlString.Append("       AND TRAN_TIME BETWEEN TO_TIMESTAMP('" + strFromDate + "', 'yyyy-mm-dd hh24:mi:ss:ff') AND TO_TIMESTAMP('" + strToDate + "', 'yyyy-mm-dd hh24:mi:ss:ff')" + "\n");
                strSqlString.Append("       AND MAT.FACTORY = '" + cdvFactory.Text + "'" + "\n");
                strSqlString.Append("       AND MAT.MAT_VER = 1 " + "\n");
                strSqlString.Append("       AND LOT_ID LIKE '" + txtLotID.Text + "' " + "\n");
                strSqlString.Append("       AND SPEC_ID " + cdvCharID.SelectedValueToQueryString + "\n");

                #region 상세 조회에 따른 SQL문 생성
                if (udcBUMPCondition1.Text != "ALL" && udcBUMPCondition1.Text != "")
                    strSqlString.AppendFormat("       AND MAT.MAT_GRP_1 {0} " + "\n", udcBUMPCondition1.SelectedValueToQueryString);

                if (udcBUMPCondition2.Text != "ALL" && udcBUMPCondition2.Text != "")
                    strSqlString.AppendFormat("       AND MAT.MAT_GRP_2 {0} " + "\n", udcBUMPCondition2.SelectedValueToQueryString);

                if (udcBUMPCondition3.Text != "ALL" && udcBUMPCondition3.Text != "")
                    strSqlString.AppendFormat("       AND MAT.MAT_GRP_3 {0} " + "\n", udcBUMPCondition3.SelectedValueToQueryString);

                if (udcBUMPCondition4.Text != "ALL" && udcBUMPCondition4.Text != "")
                    strSqlString.AppendFormat("       AND MAT.MAT_GRP_4 {0} " + "\n", udcBUMPCondition4.SelectedValueToQueryString);

                if (udcBUMPCondition5.Text != "ALL" && udcBUMPCondition5.Text != "")
                    strSqlString.AppendFormat("       AND MAT.MAT_GRP_5 {0} " + "\n", udcBUMPCondition5.SelectedValueToQueryString);

                if (udcBUMPCondition6.Text != "ALL" && udcBUMPCondition6.Text != "")
                    strSqlString.AppendFormat("       AND MAT.MAT_GRP_6 {0} " + "\n", udcBUMPCondition6.SelectedValueToQueryString);

                if (udcBUMPCondition7.Text != "ALL" && udcBUMPCondition7.Text != "")
                    strSqlString.AppendFormat("       AND MAT.MAT_GRP_7 {0} " + "\n", udcBUMPCondition7.SelectedValueToQueryString);

                if (udcBUMPCondition8.Text != "ALL" && udcBUMPCondition8.Text != "")
                    strSqlString.AppendFormat("       AND MAT.MAT_GRP_8 {0} " + "\n", udcBUMPCondition8.SelectedValueToQueryString);

                if (udcBUMPCondition9.Text != "ALL" && udcBUMPCondition9.Text != "")
                    strSqlString.AppendFormat("       AND MAT.MAT_CMF_14 {0} " + "\n", udcBUMPCondition9.SelectedValueToQueryString);

                if (udcBUMPCondition10.Text != "ALL" && udcBUMPCondition10.Text != "")
                    strSqlString.AppendFormat("       AND MAT.MAT_CMF_2 {0} " + "\n", udcBUMPCondition10.SelectedValueToQueryString);

                if (udcBUMPCondition11.Text != "ALL" && udcBUMPCondition11.Text != "")
                    strSqlString.AppendFormat("       AND MAT.MAT_CMF_3 {0} " + "\n", udcBUMPCondition11.SelectedValueToQueryString);

                if (udcBUMPCondition12.Text != "ALL" && udcBUMPCondition12.Text != "")
                    strSqlString.AppendFormat("       AND MAT.MAT_CMF_4 {0} " + "\n", udcBUMPCondition12.SelectedValueToQueryString);
                #endregion

                strSqlString.Append(") " + " \n");
                strSqlString.Append("SELECT " + QueryCond2 + " \n");
                strSqlString.Append("     , TRAN_TIME, LOT_ID, MAT_ID, OPER, RES_ID " + " \n");
                strSqlString.Append("     , PLAN_NAME, SPEC_ID, VALUE_UNIT, USL, LSL " + " \n");
                strSqlString.Append("     , SITE_01, SITE_02, SITE_03, SITE_04, SITE_05, SITE_06, SITE_07 " + " \n");
                strSqlString.Append("     , SITE_08, SITE_09, SITE_10, SITE_11, SITE_12, SITE_13, SITE_14 " + " \n");
                strSqlString.Append("     , SITE_15, SITE_16, SITE_17, SITE_18, SITE_19, SITE_20 " + " \n");
                strSqlString.Append("  FROM " + " \n");

                strSqlString.Append("        (SELECT DATA_SEQ," + " \n");
                strSqlString.Append("            MAX (VALUE_1) AS SITE_01," + " \n");
                strSqlString.Append("            MAX (VALUE_2) AS SITE_02," + " \n");
                strSqlString.Append("            MAX (VALUE_3) AS SITE_03," + " \n");
                strSqlString.Append("            MAX (VALUE_4) AS SITE_04," + " \n");
                strSqlString.Append("            MAX (VALUE_5) AS SITE_05," + " \n");
                strSqlString.Append("            MAX (VALUE_6) AS SITE_06," + " \n");
                strSqlString.Append("            MAX (VALUE_7) AS SITE_07," + " \n");
                strSqlString.Append("            MAX (VALUE_8) AS SITE_08," + " \n");
                strSqlString.Append("            MAX (VALUE_9) AS SITE_09," + " \n");
                strSqlString.Append("            MAX (VALUE_10) AS SITE_10," + " \n");
                strSqlString.Append("            MAX (VALUE_11) AS SITE_11," + " \n");
                strSqlString.Append("            MAX (VALUE_12) AS SITE_12," + " \n");
                strSqlString.Append("            MAX (VALUE_13) AS SITE_13," + " \n");
                strSqlString.Append("            MAX (VALUE_14) AS SITE_14," + " \n");
                strSqlString.Append("            MAX (VALUE_15) AS SITE_15," + " \n");
                strSqlString.Append("            MAX (VALUE_16) AS SITE_16," + " \n");
                strSqlString.Append("            MAX (VALUE_17) AS SITE_17," + " \n");
                strSqlString.Append("            MAX (VALUE_18) AS SITE_18," + " \n");
                strSqlString.Append("            MAX (VALUE_19) AS SITE_19," + " \n");
                strSqlString.Append("            MAX (VALUE_20) AS SITE_20" + " \n");
                strSqlString.Append("           FROM (SELECT RW.DATA_SEQ," + " \n");
                strSqlString.Append("                        DECODE (SITE_NUM, 1, RAW_VALUE) AS VALUE_1," + " \n");
                strSqlString.Append("                        DECODE (SITE_NUM, 2, RAW_VALUE) AS VALUE_2," + " \n");
                strSqlString.Append("                        DECODE (SITE_NUM, 3, RAW_VALUE) AS VALUE_3," + " \n");
                strSqlString.Append("                        DECODE (SITE_NUM, 4, RAW_VALUE) AS VALUE_4," + " \n");
                strSqlString.Append("                        DECODE (SITE_NUM, 5, RAW_VALUE) AS VALUE_5," + " \n");
                strSqlString.Append("                        DECODE (SITE_NUM, 6, RAW_VALUE) AS VALUE_6," + " \n");
                strSqlString.Append("                        DECODE (SITE_NUM, 7, RAW_VALUE) AS VALUE_7," + " \n");
                strSqlString.Append("                        DECODE (SITE_NUM, 8, RAW_VALUE) AS VALUE_8," + " \n");
                strSqlString.Append("                        DECODE (SITE_NUM, 9, RAW_VALUE) AS VALUE_9," + " \n");
                strSqlString.Append("                        DECODE (SITE_NUM, 10, RAW_VALUE) AS VALUE_10," + " \n");
                strSqlString.Append("                        DECODE (SITE_NUM, 11, RAW_VALUE) AS VALUE_11," + " \n");
                strSqlString.Append("                        DECODE (SITE_NUM, 12, RAW_VALUE) AS VALUE_12," + " \n");
                strSqlString.Append("                        DECODE (SITE_NUM, 13, RAW_VALUE) AS VALUE_13," + " \n");
                strSqlString.Append("                        DECODE (SITE_NUM, 14, RAW_VALUE) AS VALUE_14," + " \n");
                strSqlString.Append("                        DECODE (SITE_NUM, 15, RAW_VALUE) AS VALUE_15," + " \n");
                strSqlString.Append("                        DECODE (SITE_NUM, 16, RAW_VALUE) AS VALUE_16," + " \n");
                strSqlString.Append("                        DECODE (SITE_NUM, 17, RAW_VALUE) AS VALUE_17," + " \n");
                strSqlString.Append("                        DECODE (SITE_NUM, 18, RAW_VALUE) AS VALUE_18," + " \n");
                strSqlString.Append("                        DECODE (SITE_NUM, 19, RAW_VALUE) AS VALUE_19," + " \n");
                strSqlString.Append("                        DECODE (SITE_NUM, 20, RAW_VALUE) AS VALUE_20" + " \n");
                strSqlString.Append("                   FROM TQS_RAW_DATA@RPTTOFA RW, DT A" + " \n");
                strSqlString.Append("                  WHERE RW.DATA_SEQ = A.DATA_SEQ)" + " \n");
                strSqlString.Append("          GROUP BY DATA_SEQ) SD, DT PL" + " \n");
                strSqlString.Append(" WHERE SD.DATA_SEQ = PL.DATA_SEQ" + " \n");

                strSqlString.Append(" ORDER BY " + QueryCond2 + " , MAT_ID, LOT_ID" + "\n");
            }
            else
            {
                strSqlString.Append("SELECT " + QueryCond1 + " \n");
                strSqlString.Append("     , DAT.TRAN_TIME,DAT.LOT_ID,DAT.MAT_ID,DAT.OPER,DAT.MEAS_RES_ID,DAT.COL_SET_ID,DAT.CHAR_ID,DAT.UNIT_ID,DAT.VALUE_1 " + "\n");
                strSqlString.Append("     , DAT.VALUE_2,DAT.VALUE_3,DAT.VALUE_4,DAT.VALUE_5,DAT.VALUE_6,DAT.VALUE_7,DAT.VALUE_8,DAT.VALUE_9,DAT.VALUE_10 " + "\n");
                strSqlString.Append("     , DAT.VALUE_11,DAT.VALUE_12,DAT.VALUE_13,DAT.VALUE_14,DAT.VALUE_15" + "\n");
                strSqlString.Append("     , (SELECT USER_DESC FROM MSECUSRDEF WHERE USER_ID = DAT.CREATE_USER_ID AND FACTORY = '" + GlobalVariable.gsAssyDefaultFactory + "')" + "\n");


                strSqlString.Append("  FROM MEDCLOTDAT@RPTTOMES DAT, MWIPMATDEF MAT " + "\n");
                strSqlString.Append(" WHERE 1=1 " + "\n");
                strSqlString.Append("   AND DAT.FACTORY = MAT.FACTORY  " + "\n");
                strSqlString.Append("   AND DAT.MAT_ID = MAT.MAT_ID " + "\n");
                strSqlString.Append("   AND DAT.MAT_VER = MAT.MAT_VER  " + "\n");
                strSqlString.Append("   AND TRAN_TIME BETWEEN '" + strFromDate + "' AND '" + strToDate + "'" + "\n");
                strSqlString.Append("   AND MAT.FACTORY = '" + cdvFactory.Text + "'" + "\n");
                strSqlString.Append("   AND MAT.MAT_VER = 1 " + "\n");
                strSqlString.Append("   AND LOT_ID LIKE '" + txtLotID.Text + "' " + "\n");
                strSqlString.Append("   AND CHAR_ID " + cdvCharID.SelectedValueToQueryString + "\n");

                #region 상세 조회에 따른 SQL문 생성
                if (udcWIPCondition1.Text != "ALL" && udcWIPCondition1.Text != "")
                    strSqlString.AppendFormat("   AND MAT.MAT_GRP_1 {0} " + "\n", udcWIPCondition1.SelectedValueToQueryString);

                if (udcWIPCondition2.Text != "ALL" && udcWIPCondition2.Text != "")
                    strSqlString.AppendFormat("   AND MAT.MAT_GRP_2 {0} " + "\n", udcWIPCondition2.SelectedValueToQueryString);

                if (udcWIPCondition3.Text != "ALL" && udcWIPCondition3.Text != "")
                    strSqlString.AppendFormat("   AND MAT.MAT_GRP_3 {0} " + "\n", udcWIPCondition3.SelectedValueToQueryString);

                if (udcWIPCondition4.Text != "ALL" && udcWIPCondition4.Text != "")
                    strSqlString.AppendFormat("   AND MAT.MAT_GRP_4 {0} " + "\n", udcWIPCondition4.SelectedValueToQueryString);

                if (udcWIPCondition5.Text != "ALL" && udcWIPCondition5.Text != "")
                    strSqlString.AppendFormat("   AND MAT.MAT_GRP_5 {0} " + "\n", udcWIPCondition5.SelectedValueToQueryString);

                if (udcWIPCondition6.Text != "ALL" && udcWIPCondition6.Text != "")
                    strSqlString.AppendFormat("   AND MAT.MAT_GRP_6 {0} " + "\n", udcWIPCondition6.SelectedValueToQueryString);

                if (udcWIPCondition7.Text != "ALL" && udcWIPCondition7.Text != "")
                    strSqlString.AppendFormat("   AND MAT.MAT_GRP_7 {0} " + "\n", udcWIPCondition7.SelectedValueToQueryString);

                if (udcWIPCondition8.Text != "ALL" && udcWIPCondition8.Text != "")
                    strSqlString.AppendFormat("   AND MAT.MAT_GRP_8 {0} " + "\n", udcWIPCondition8.SelectedValueToQueryString);

                if (udcWIPCondition9.Text != "ALL" && udcWIPCondition9.Text != "")
                    strSqlString.AppendFormat("   AND MAT.MAT_GRP_9 {0} " + "\n", udcWIPCondition9.SelectedValueToQueryString);
                #endregion

                strSqlString.Append(" ORDER BY " + QueryCond1 + " , DAT.MAT_ID, LOT_ID" + "\n");
            }          

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
                // LoadingPopUp.LoadIngPopUpShow(this);
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

                //// Sub Total
                //int[] rowType = spdData.RPT_DataBindingWithSubTotal(dt, 0, 0, 16, null, null);

                //// Total
                //spdData.RPT_FillDataSelectiveCells("Total", 0, 8, 0, 1, true, Align.Center, VerticalAlign.Center);

                // Column Count 정리
                if (cdvFactory.Text.Trim() == "HMKB1")
                {
                    int iValCnt = dt.Columns.Count;
                    double dSumVal = 0.0f;

                    if(dt.Rows.Count > 0)
                    {
                        for (int i = dt.Columns.Count - 1; i > 0; i--)
                        {
                            dSumVal = 0.0f;

                            for (int j = 0; j < dt.Rows.Count; j++)
                            {
                                if (dt.Rows[j][i].ToString() != "")
                                    dSumVal += double.Parse(dt.Rows[j][i].ToString());
                            }

                            if (dSumVal > 0)
                                break;

                            iValCnt = i;
                        }

                        spdData.ActiveSheet.ColumnCount = iValCnt;
                    }
                }

                ////4. Column Auto Fit
                spdData.RPT_AutoFit(false);

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

        private void PQC030901_Load(object sender, EventArgs e)
        {
            //string startDate = DateTime.Now.AddDays(-1).ToString("yyyy-MM-dd");
            //cdvStartDate.Value = Convert.ToDateTime(startDate);

            //string endDate = DateTime.Now.ToString("yyyy-MM-dd");
            //cdvEndDate.Value = Convert.ToDateTime(endDate);

            /*
            string strQuery = string.Empty;
            
            if (cdvFactory.Text.Trim() == "HMKB1")
            {
                cdvCharID.ConditionText = "SPEC_ID";

                // VSPCSPECDAT에서 조회하도록 추가  @RPTTOMES
                strQuery += "SELECT DISTINCT SPEC_ID Code, '' Data" + "\n";
                strQuery += "  FROM VSPCSPECDAT@TESTMES " + "\n";
                strQuery += "ORDER BY SPEC_ID " + "\n";
            }
            else
            {
                cdvCharID.ConditionText = "CHAR_ID";

                strQuery += "SELECT DISTINCT CHAR_ID Code, '' Data" + "\n";
                strQuery += "  FROM MEDCCHRDEF " + "\n";
                strQuery += "ORDER BY CHAR_ID " + "\n";
            }
            
            cdvCharID.sDynamicQuery = strQuery;
            */
            GetCharIDData();
        }

        private void cdvFactory_ValueSelectedItemChanged(object sender, MCCodeViewSelChanged_EventArgs e)
        {
            if (cdvFactory.Text.Trim() == "HMKB1")
            {
                BaseFormType = eBaseFormType.BUMP_BASE;
                pnlBUMPDetail.Visible = false;
            }
            else
            {
                BaseFormType = eBaseFormType.WIP_BASE;
                pnlWIPDetail.Visible = false;
            }

            this.SetFactory(cdvFactory.txtValue);
            cdvCharID.sFactory = cdvFactory.txtValue;

            SortInit();     //add 150529
            GetCharIDData();
        }

    }
}
