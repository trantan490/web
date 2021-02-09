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
    public partial class PRD010303 : Miracom.SmartWeb.UI.Controls.udcCUSReport002
    {

        private DataTable dtOper = null;
        private DataTable dtOperGrp = null;

        /// <summary>
        /// 클  래  스: PRD010303<br/>
        /// 클래스요약: LOT 별 재공 조회<br/>
        /// 작  성  자: 미라콤 양형석<br/>
        /// 최초작성일: 2008-12-12<br/>
        /// 상세  설명: LOT 별 재공 현황 조회.<br/>
        /// 변경  내용: <br/>
        /// 변  경  자: 하나마이크론 김준용<br />
        /// Excel Export 저장 기능 변경<br />
        /// 2011-05-19-임종우 : HMKT1일 경우 H72(장기정체) HOLD 재공 제외 (김권수 요청)
        /// 2011-06-14-김민우 : HMKT1일 경우 H72(장기정체) HOLD 재공 제외 원복 (김권수 요청)
        /// 2013-10-14-김민우 : LOT TYPE ALL, P%, E% 구분으로변경
        /// 2018-01-04-임종우 : SubTotal, GrandTotal 평균값 구하기 Function 변경
        /// 2018-03-22-임종우 : Egis 업체 PCB Strip, PCB BAD Mark 정보 추가 (백성호D 요청)
        /// 2020-12-28-임종우 : Group 조건 추가..Run 기준으로 보기 위해.. - Run ID, Lot ID (마세렬과장 요청)
        /// </summary>
        public PRD010303()
        {
            InitializeComponent();

            dtOper = new DataTable();
            dtOperGrp = new DataTable();

            SortInit();
            GridColumnInit(); //헤더 한줄짜리

            cdvDate.Value = DateTime.Today;
            cboTimeBase.SelectedIndex = 1;
        }

        #region SortInit

        /// <summary>
        /// SortInit
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SortInit()
        {
            ((udcTableForm)(this.btnSort.BindingForm)).AddRow("Customer", "NVL((SELECT DATA_1 FROM MGCMTBLDAT WHERE TABLE_NAME = 'H_CUSTOMER' AND KEY_1 = MAT.MAT_GRP_1 AND ROWNUM=1), '-') AS Customer", "MAT.MAT_GRP_1", true);
            ((udcTableForm)(this.btnSort.BindingForm)).AddRow("Family", "MAT.MAT_GRP_2 AS Family", "MAT.MAT_GRP_2", false);
            ((udcTableForm)(this.btnSort.BindingForm)).AddRow("Package", "MAT.MAT_GRP_3 AS Package", "MAT.MAT_GRP_3", false);
            ((udcTableForm)(this.btnSort.BindingForm)).AddRow("Type1", "MAT.MAT_GRP_4 AS Type1", "MAT.MAT_GRP_4", false);
            ((udcTableForm)(this.btnSort.BindingForm)).AddRow("Type2", "MAT.MAT_GRP_5 AS Type2", "MAT.MAT_GRP_5", false);
            ((udcTableForm)(this.btnSort.BindingForm)).AddRow("Lead", "MAT.MAT_GRP_6 AS Lead", "MAT.MAT_GRP_6", false);
            ((udcTableForm)(this.btnSort.BindingForm)).AddRow("Density", "MAT.MAT_GRP_7 AS Density", "MAT.MAT_GRP_7", false);
            ((udcTableForm)(this.btnSort.BindingForm)).AddRow("Generation", "MAT.MAT_GRP_8 AS Generation", "MAT.MAT_GRP_8", false);
            ((udcTableForm)(this.btnSort.BindingForm)).AddRow("PIN_TYPE", "MAT.MAT_CMF_10 AS PIN_TYPE", "MAT.MAT_CMF_10", true);
            ((udcTableForm)(this.btnSort.BindingForm)).AddRow("Product", "LOT.MAT_ID AS Product", "LOT.MAT_ID", true);
            ((udcTableForm)(this.btnSort.BindingForm)).AddRow("CUST_DEVICE", "MAT.MAT_CMF_7 AS CUST_DEVICE", "MAT.MAT_CMF_7", false);
            ((udcTableForm)(this.btnSort.BindingForm)).AddRow("Run ID", "LOT.LOT_CMF_4 AS RUN_ID", "LOT.LOT_CMF_4", true);
            ((udcTableForm)(this.btnSort.BindingForm)).AddRow("Lot ID", "LOT.LOT_ID", "LOT.LOT_ID", true);
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
            spdData.RPT_AddBasicColumn("Lead", 0, 5, Visibles.False, Frozen.True, Align.Center, Merge.True, Formatter.String, 80);
            spdData.RPT_AddBasicColumn("Density", 0, 6, Visibles.False, Frozen.True, Align.Center, Merge.True, Formatter.String, 80);
            spdData.RPT_AddBasicColumn("Generation", 0, 7, Visibles.False, Frozen.True, Align.Center, Merge.True, Formatter.String, 80);
            spdData.RPT_AddBasicColumn("PIN TYPE", 0, 8, Visibles.True, Frozen.True, Align.Left, Merge.True, Formatter.String, 80);
            spdData.RPT_AddBasicColumn("Product", 0, 9, Visibles.True, Frozen.True, Align.Left, Merge.True, Formatter.String, 80);
            spdData.RPT_AddBasicColumn("CUST_DEVICE", 0, 10, Visibles.False, Frozen.True, Align.Left, Merge.True, Formatter.String, 80);
            spdData.RPT_AddBasicColumn("Run ID", 0, 11, Visibles.True, Frozen.True, Align.Left, Merge.True, Formatter.String, 80);
            spdData.RPT_AddBasicColumn("Lot ID", 0, 12, Visibles.True, Frozen.True, Align.Left, Merge.True, Formatter.String, 80);
            spdData.RPT_AddBasicColumn("IN DATE", 0, 13, Visibles.True, Frozen.False, Align.Center, Merge.False, Formatter.String, 80);
            spdData.RPT_AddBasicColumn("Operation MOVE", 0, 14, Visibles.True, Frozen.False, Align.Center, Merge.False, Formatter.String, 80);
            spdData.RPT_AddBasicColumn("IN QTY", 0, 15, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 80);

            if (rbtBrief.Checked)
            {
                for (int i = 0; i < dtOperGrp.Rows.Count; i++)
                {
                    spdData.RPT_AddBasicColumn(dtOperGrp.Rows[i][0].ToString(), 0, spdData.ActiveSheet.Columns.Count, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 60);
                }

            }
            else
            {
                for (int i = 0; i < dtOper.Rows.Count; i++)
                {
                    spdData.RPT_AddBasicColumn(dtOper.Rows[i][0].ToString(), 0, spdData.ActiveSheet.Columns.Count, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 60);
                }
            }

            spdData.RPT_AddBasicColumn("TTL TAT", 0, spdData.ActiveSheet.Columns.Count, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Double2, 80);
            spdData.RPT_AddBasicColumn("Operation TAT", 0, spdData.ActiveSheet.Columns.Count, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Double2, 80);

            if (ckbPCB.Checked == true)
            {
                spdData.RPT_AddBasicColumn("PCB Strip", 0, spdData.ActiveSheet.Columns.Count, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 80);
                spdData.RPT_AddBasicColumn("PCB BAD Mark", 0, spdData.ActiveSheet.Columns.Count, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 80);
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



                //그루핑 순서 1순위만 SubTotal을 사용하기 위해서 첫번째 값을 가져옴
                int sub = ((udcTableForm)(this.btnSort.BindingForm)).GetCheckedValue(2);
                int nGroupCount = ((udcTableForm)(this.btnSort.BindingForm)).GetSelectedCount();

                ////by John (한줄짜리)
                ////1.Griid 합계 표시
                int[] rowType = spdData.RPT_DataBindingWithSubTotal(dt, 0, sub + 1, 15, null, null, btnSort);


                ////2. 칼럼 고정(필요하다면..)
                //spdData.Sheets[0].FrozenColumnCount = 9;

                ////3. Total부분 셀머지
                spdData.RPT_FillDataSelectiveCells("Total", 0, 15, 0, 1, true, Align.Center, VerticalAlign.Center);

                //4. Column Auto Fit
                spdData.RPT_AutoFit(false);

                if (ckbPCB.Checked == true)
                {
                    //5. 데이타가 0인 부분은 제거(Add by John. 2009.1.13)
                    spdData.RPT_RemoveZeroColumn(15, spdData.ActiveSheet.Columns.Count - 3, 1);

                    #region TTL TAT, 공정 TAT은 SUM 대신 AVG로 바꿔주기
                    spdData.RPT_SetAvgSubTotalAndGrandTotal(1, spdData.ActiveSheet.Columns.Count - 4, nGroupCount, false);
                    spdData.RPT_SetAvgSubTotalAndGrandTotal(1, spdData.ActiveSheet.Columns.Count - 3, nGroupCount, false);
                    #endregion
                }
                else
                {
                    //5. 데이타가 0인 부분은 제거(Add by John. 2009.1.13)
                    spdData.RPT_RemoveZeroColumn(15, 1);

                    #region TTL TAT, 공정 TAT은 SUM 대신 AVG로 바꿔주기
                    spdData.RPT_SetAvgSubTotalAndGrandTotal(1, spdData.ActiveSheet.Columns.Count - 2, nGroupCount, false);
                    spdData.RPT_SetAvgSubTotalAndGrandTotal(1, spdData.ActiveSheet.Columns.Count - 1, nGroupCount, false);
                    #endregion
                }

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
            
            // 요약, 상세
            bool isBrief = rbtBrief.Checked;

            // Decode 반복문 셋팅
            string strDecode = string.Empty;
            if (isBrief)
            {
                for (int i = 0; i < dtOperGrp.Rows.Count; i++)
                {

                    strDecode += "     , SUM(DECODE(LOT.OPER_GROUP, '" + dtOperGrp.Rows[i][0].ToString() + "', QTY_1, 0)) QTY_" + i.ToString() + "\n";

                }
            }
            else
            {
                for (int i = 0; i < dtOper.Rows.Count; i++)
                {
                    strDecode += "     , SUM(DECODE(LOT.OPER, '" + dtOper.Rows[i][0].ToString() + "', QTY_1, 0)) QTY_" + i.ToString() + "\n";
                }
            }

            // 시간 관련 셋팅
            string strDate = cdvDate.Value.ToString("yyyyMMdd");
            bool isRealTime = false;
            if (strDate.Equals(DateTime.Now.ToString("yyyyMMdd")))
            {
                strDate = DateTime.Now.ToString("yyyyMMddHHmmss");
                isRealTime = true;
            }
            else
            {
                if (cboTimeBase.SelectedIndex == 0)
                    strDate = strDate + "060000";
                else
                    strDate = strDate + "220000";

                isRealTime = false;
            }

            // 쿼리
            strSqlString.Append("SELECT " + QueryCond1 + "\n");
            //strSqlString.Append("     , LOT.LOT_CMF_4 RUN_ID " + "\n");
            //strSqlString.Append("     , LOT.LOT_ID " + "\n");
            strSqlString.Append("     , MAX(TO_CHAR(TO_DATE(LOT.LOT_CMF_14,'YYYYMMDDHH24MISS'),'YYYY-MM-DD HH24:MI')) AS IN_TIME " + "\n");
            strSqlString.Append("     , MAX(TO_CHAR(TO_DATE(LOT.OPER_IN_TIME,'YYYYMMDDHH24MISS'),'YYYY-MM-DD HH24:MI')) AS MOVE_TIME " + "\n");

            // kpcs
            if (chkKpcs.Checked)
            {
                strSqlString.Append("     , ROUND(SUM(LOT.CREATE_QTY_1)/1000) AS IN_QTY " + "\n");
                strDecode = strDecode.Replace("SUM(DECODE", "ROUND(SUM(DECODE").Replace("QTY_1, 0))", "QTY_1, 0))/1000)");
            }
            else
                strSqlString.Append("     , SUM(LOT.CREATE_QTY_1) AS IN_QTY " + "\n");

            strSqlString.Append(strDecode);
            strSqlString.Append("     , TRUNC(SUM(TO_DATE('" + strDate + "', 'YYYYMMDDHH24MISS') - TO_DATE(LOT.LOT_CMF_14,'YYYYMMDDHH24MISS')) / COUNT(LOT_ID), 2) AS TTL_TAT " + "\n");
            strSqlString.Append("     , TRUNC(SUM(TO_DATE('" + strDate + "', 'YYYYMMDDHH24MISS') - TO_DATE(LOT.OPER_IN_TIME,'YYYYMMDDHH24MISS')) / COUNT(LOT_ID), 2) AS OPER_TAT " + "\n");

            // 2018-03-22-임종우 : PCB Strip 정보
            if (ckbPCB.Checked == true)
            {
                strSqlString.Append("     , MAX(NVL(TO_NUMBER((SELECT ATTR_VALUE FROM MATRNAMSTS@RPTTOMES WHERE ATTR_KEY = LOT.M_LOT_ID AND ATTR_TYPE = 'LOT' AND FACTORY = LOT.FACTORY AND ATTR_NAME = 'PCB_STRIP')),0)) AS PCB_STRIP" + "\n");
                strSqlString.Append("     , MAX(NVL(TO_NUMBER((SELECT ATTR_VALUE FROM MATRNAMSTS@RPTTOMES WHERE ATTR_KEY = LOT.M_LOT_ID AND ATTR_TYPE = 'LOT' AND FACTORY = LOT.FACTORY AND ATTR_NAME = 'PCB_BAD_QTY')),0)) AS PCB_BAD_QTY" + "\n");
            }

            strSqlString.Append("  FROM (" + "\n");                          
            strSqlString.Append("        SELECT LOT.FACTORY, LOT.MAT_ID, LOT.LOT_CMF_4, LOT.LOT_ID, LOT.LOT_CMF_14, LOT.OPER_IN_TIME, LOT.CREATE_QTY_1, OPR.OPER_GRP_1 AS OPER_GROUP, LOT.OPER, LOT.QTY_1 " + "\n");

            if (ckbPCB.Checked == true)
            {
                strSqlString.Append("             , CASE WHEN LOT.MAT_ID LIKE 'EI%' THEN (  " + "\n");
                strSqlString.Append("                                                     SELECT LOT_ID  " + "\n");
                strSqlString.Append("                                                       FROM CRESMATHIS@RPTTOMES  " + "\n");
                strSqlString.Append("                                                      WHERE FACTORY = '" + GlobalVariable.gsAssyDefaultFactory + "' " + "\n");
                strSqlString.Append("                                                        AND START_LOT_ID = LOT.LOT_ID " + "\n");
                strSqlString.Append("                                                        AND M_MAT_ID IN (SELECT MAT_ID FROM MWIPMATDEF WHERE FACTORY = '" + GlobalVariable.gsAssyDefaultFactory + "' AND MAT_TYPE = 'PB' AND DELETE_FLAG = ' ') " + "\n");
                strSqlString.Append("                                                    ) END AS M_LOT_ID " + "\n");
            }

            if (isRealTime)
            {
                strSqlString.Append("          FROM RWIPLOTSTS LOT, " + "\n");
                strSqlString.Append("               MWIPOPRDEF OPR " + "\n");
                strSqlString.Append("         WHERE 1=1 " + "\n");
            }
            else
            {
                strSqlString.Append("          FROM RWIPLOTSTS_BOH LOT, " + "\n");
                strSqlString.Append("               MWIPOPRDEF OPR " + "\n");
                strSqlString.AppendFormat("         WHERE LOT.CUTOFF_DT = '{0}'" + "\n", strDate.Substring(0, 10));
            }
            
            strSqlString.Append("           AND LOT.FACTORY = OPR.FACTORY " + "\n");
            strSqlString.Append("           AND LOT.OPER = OPR.OPER " + "\n");
            strSqlString.Append("           AND LOT.FACTORY = '" + cdvFactory.Text + "' " + "\n");
            
            if (txtSearchProduct.Text.Trim() != "%" && txtSearchProduct.Text.Trim() != "")
                strSqlString.AppendFormat("        AND LOT.MAT_ID LIKE '{0}'" + "\n", txtSearchProduct.Text);

            if (txtRunID.Text.Trim() != "")
                strSqlString.AppendFormat("        AND LOT.LOT_CMF_4 = '{0}'" + "\n", txtRunID.Text);

            if (ckbPCB.Checked == true)
                strSqlString.Append("        AND LOT.MAT_ID LIKE 'EI%'" + "\n");

            strSqlString.Append("           AND LOT.LOT_DEL_FLAG = ' ' " + "\n");
            strSqlString.Append("           AND LOT.MAT_VER = 1 " + "\n");
            strSqlString.Append("           AND LOT.LOT_TYPE = 'W' " + "\n");
             
            if (cdvLotType.Text != "ALL")
            {
                strSqlString.Append("           AND LOT.LOT_CMF_5 LIKE '" + cdvLotType.Text + "'" + "\n");
            }
          
            strSqlString.Append("       ) LOT " + "\n");
            strSqlString.Append("     , MWIPMATDEF MAT " + "\n");
            strSqlString.Append(" WHERE LOT.FACTORY = MAT.FACTORY " + "\n");            
            strSqlString.Append("   AND LOT.MAT_ID = MAT.MAT_ID " + "\n");            
                        
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

            strSqlString.Append("GROUP BY " + QueryCond2 + "\n");
            strSqlString.Append("ORDER BY " + QueryCond2 + "\n");
            //strSqlString.Append("ORDER BY Customer, LOT.MAT_ID, LOT.LOT_CMF_4, LOT.LOT_ID, LOT.LOT_CMF_14 " + "\n");

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

        private void cdvFactory_ValueSelectedItemChanged(object sender, MCCodeViewSelChanged_EventArgs e)
        {
            this.SetFactory(cdvFactory.txtValue);
            //cdvLotType.sFactory = cdvFactory.txtValue;

            StringBuilder strSqlString = new StringBuilder();
            StringBuilder strSqlString1 = new StringBuilder();
            
            strSqlString.Append("SELECT OPER_GRP_1" + "\n");            
            strSqlString.Append("  FROM MWIPOPRDEF " + "\n");
            strSqlString.Append(" WHERE FACTORY " + cdvFactory.SelectedValueToQueryString + "\n");
            strSqlString.Append("   AND OPER_CMF_4 <> ' '    " + "\n");
            strSqlString.Append(" GROUP BY OPER_GRP_1" + "\n");
            strSqlString.Append(" ORDER BY TO_NUMBER(MIN(OPER_CMF_4)) ASC" + "\n");

            dtOperGrp = CmnFunction.oComm.GetFuncDataTable("DYNAMIC", strSqlString.ToString());

            strSqlString1.Append("SELECT OPER " + "\n");
            strSqlString1.Append("  FROM MWIPOPRDEF A" + "\n");
            strSqlString1.Append(" WHERE FACTORY " + cdvFactory.SelectedValueToQueryString + "\n");
            strSqlString1.Append("   AND OPER NOT IN ('00001', '00002') " + "\n");
            strSqlString1.Append("   AND OPER LIKE SUBSTR(A.FACTORY, 4, 1) || '%' " + "\n");
            strSqlString1.Append(" ORDER BY DECODE(OPER_CMF_2, ' ', 99999, TO_NUMBER(OPER_CMF_2)), OPER" + "\n");            
            
            dtOper = CmnFunction.oComm.GetFuncDataTable("DYNAMIC", strSqlString1.ToString());
        }

        private void spdData_CellClick(object sender, FarPoint.Win.Spread.CellClickEventArgs e)
        {
            if (rbtDetail.Checked == true)
            {
                int cnt = spdData.ActiveSheet.Columns.Count - 2;
                if (e.ColumnHeader == true && e.Column > 12 && cnt > e.Column)
                {
                    string Query = "SELECT OPER_DESC FROM MWIPOPRDEF WHERE OPER = '" + spdData.ActiveSheet.Columns[e.Column].Label + "' AND ROWNUM=1";
                    DataTable dt = CmnFunction.oComm.GetFuncDataTable("DYNAMIC", Query);

                    ToolTip desc = new ToolTip();
                    desc.Show(dt.Rows[0][0].ToString(), spdData, e.X + 10, e.Y, 1000);
                }
            }            
        }
    }
}

