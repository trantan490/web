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
    public partial class PRD010314 : Miracom.SmartWeb.UI.Controls.udcCUSReport002
    {
        /// <summary>
        /// 클  래  스: PRD010314<br/>
        /// 클래스요약: 예약 HOLD LOT 조회<br/>
        /// 작  성  자: 하나마이크론 임종우<br/>
        /// 최초작성일: 2014-11-24<br/>
        /// 상세  설명: 예약 HOLD LOT 조회<br/>
        /// 변경  내용: <br/>
        /// 2014-11-26-임종우 : WIP 재공 존재 LOT 만 볼때 TEST 공정 제외, 예약 HOLD 공정 지난것들은 제외 (임태성K 요청)
        /// 2014-12-23-임종우 : 예약 HOLD 재공과 실제 HOLD 재공 둘다 데이터가 존재하면 실제 HOLD 재공만 표시 (임태성K 요청)
        /// 2014-12-24-임종우 : 공정 검색기능 재공공정으로 변경, 정렬도 재공공정으로 변경 (임태성K 요청)
        /// 2019-04-09-임종우 : 변곡점 LOT (PCL CODE) 표시 (임태성차장 요청)
        /// </summary>
        public PRD010314()
        {
            InitializeComponent();            

            SortInit();
            GridColumnInit(); //헤더 한줄짜리

            this.SetFactory(GlobalVariable.gsAssyDefaultFactory);
            cdvFactory.Text = GlobalVariable.gsAssyDefaultFactory;
            cdvOper.sFactory = GlobalVariable.gsAssyDefaultFactory;
        }

        #region SortInit

        /// <summary>
        /// SortInit
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SortInit()
        {
            ((udcTableForm)(this.btnSort.BindingForm)).AddRow("Customer", "NVL((SELECT DATA_1 FROM MGCMTBLDAT WHERE TABLE_NAME = 'H_CUSTOMER' AND KEY_1 = MAT_GRP_1 AND ROWNUM=1), '-') AS CUSTOMER", "MAT_GRP_1", true);
            ((udcTableForm)(this.btnSort.BindingForm)).AddRow("Family", "NVL(MAT_GRP_2, ' ') AS FAMILY", "MAT_GRP_2", false);
            ((udcTableForm)(this.btnSort.BindingForm)).AddRow("Package", "NVL(MAT_GRP_3, ' ') AS PACKAGE", "MAT_GRP_3", true);
            ((udcTableForm)(this.btnSort.BindingForm)).AddRow("Type1", "NVL(MAT_GRP_4, ' ') AS TYPE1", "MAT_GRP_4", false);
            ((udcTableForm)(this.btnSort.BindingForm)).AddRow("Type2", "NVL(MAT_GRP_5, ' ') AS TYPE2", "MAT_GRP_5", false);
            ((udcTableForm)(this.btnSort.BindingForm)).AddRow("LD Count", "NVL(MAT_GRP_6, ' ') AS LDCOUNT", "MAT_GRP_6", false);
            ((udcTableForm)(this.btnSort.BindingForm)).AddRow("Density", "NVL(MAT_GRP_7, ' ') AS DENSITY", "MAT_GRP_7", false);
            ((udcTableForm)(this.btnSort.BindingForm)).AddRow("Generation", "NVL(MAT_GRP_8, ' ') AS GENERATION", "MAT_GRP_8", false);
            ((udcTableForm)(this.btnSort.BindingForm)).AddRow("Pin Type", "NVL(MAT_CMF_10, ' ') AS PIN_TYPE", "MAT_CMF_10", true);
            ((udcTableForm)(this.btnSort.BindingForm)).AddRow("PKG Code", "NVL(MAT_CMF_11, ' ') AS PKG_CODE", "MAT_CMF_11", false);
            ((udcTableForm)(this.btnSort.BindingForm)).AddRow("Product", "NVL(B.MAT_ID, ' ') AS PRODUCT", "B.MAT_ID", true);                
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
            spdData.RPT_AddBasicColumn("Customer", 0, 0, Visibles.True, Frozen.True, Align.Center, Merge.True, Formatter.String, 70);
            spdData.RPT_AddBasicColumn("Family", 0, 1, Visibles.False, Frozen.True, Align.Center, Merge.True, Formatter.String, 70);
            spdData.RPT_AddBasicColumn("Package", 0, 2, Visibles.True, Frozen.True, Align.Center, Merge.True, Formatter.String, 70);
            spdData.RPT_AddBasicColumn("Type1", 0, 3, Visibles.False, Frozen.True, Align.Center, Merge.True, Formatter.String, 70);
            spdData.RPT_AddBasicColumn("Type2", 0, 4, Visibles.False, Frozen.True, Align.Center, Merge.True, Formatter.String, 70);
            spdData.RPT_AddBasicColumn("LD Count", 0, 5, Visibles.False, Frozen.True, Align.Center, Merge.True, Formatter.String, 60);
            spdData.RPT_AddBasicColumn("Density", 0, 6, Visibles.False, Frozen.True, Align.Center, Merge.True, Formatter.String, 70);
            spdData.RPT_AddBasicColumn("Generation", 0, 7, Visibles.False, Frozen.True, Align.Center, Merge.True, Formatter.String, 70);
            spdData.RPT_AddBasicColumn("Pin Type", 0, 8, Visibles.True, Frozen.True, Align.Left, Merge.True, Formatter.String, 120);
            spdData.RPT_AddBasicColumn("PKG Code", 0, 9, Visibles.True, Frozen.True, Align.Center, Merge.True, Formatter.String, 70);
            spdData.RPT_AddBasicColumn("Product", 0, 10, Visibles.True, Frozen.True, Align.Left, Merge.True, Formatter.String, 120);
            spdData.RPT_AddBasicColumn("Reservation division", 0, 11, Visibles.True, Frozen.False, Align.Center, Merge.False, Formatter.String, 40);
            spdData.RPT_AddBasicColumn("Reservation Step", 0, 12, Visibles.True, Frozen.False, Align.Center, Merge.False, Formatter.String, 70);
            spdData.RPT_AddBasicColumn("Run ID", 0, 13, Visibles.True, Frozen.False, Align.Left, Merge.False, Formatter.String, 70);
            spdData.RPT_AddBasicColumn("Lot ID", 0, 14, Visibles.True, Frozen.False, Align.Left, Merge.False, Formatter.String, 70);
            spdData.RPT_AddBasicColumn("Qty", 0, 15, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 70);
            spdData.RPT_AddBasicColumn("현 Step", 0, 16, Visibles.True, Frozen.False, Align.Center, Merge.False, Formatter.String, 70);
            spdData.RPT_AddBasicColumn("Comment", 0, 17, Visibles.True, Frozen.False, Align.Left, Merge.False, Formatter.String, 150);
            spdData.RPT_AddBasicColumn("Hold Time", 0, 18, Visibles.True, Frozen.False, Align.Center, Merge.False, Formatter.String, 70);
            spdData.RPT_AddBasicColumn("Hold User", 0, 19, Visibles.True, Frozen.False, Align.Center, Merge.False, Formatter.String, 70);            
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

                ////1.Griid 합계 표시
                int[] rowType = spdData.RPT_DataBindingWithSubTotal(dt, 0, sub + 1, 11, null, null, btnSort);

                ////3. Total부분 셀머지
                spdData.RPT_FillDataSelectiveCells("Total", 0, 11, 0, 1, true, Align.Center, VerticalAlign.Center);
              
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
            if (cdvFactory.Text.Trim().Length == 0)
            {
                CmnFunction.ShowMsgBox(RptMessages.GetMessage("STD001", GlobalVariable.gcLanguage));
                return false;
            }

            if (cdvOper.FromText.Trim().Length == 0 || cdvOper.ToText.Trim().Length == 0)
            {
                CmnFunction.ShowMsgBox(RptMessages.GetMessage("STD005", GlobalVariable.gcLanguage));
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

            //bool isToday = false;
            QueryCond1 = tableForm.SelectedValueToQueryContainNull;
            QueryCond2 = tableForm.SelectedValue2ToQueryContainNull;
         
            strSqlString.AppendFormat("SELECT {0} " + "\n", QueryCond1);
            strSqlString.Append("     , A.HOLD_GUBUN  " + "\n");
            strSqlString.Append("     , A.HOLD_OPER " + "\n");
            strSqlString.Append("     , A.RUN_ID " + "\n");
            strSqlString.Append("     , A.LOT_ID" + "\n");
            strSqlString.Append("     , A.QTY_1" + "\n");
            strSqlString.Append("     , A.WIP_OPER" + "\n");
            strSqlString.Append("     , A.LOT_COMMENT" + "\n");
            strSqlString.Append("     , A.HOLD_TIME" + "\n");
            strSqlString.Append("     , CASE WHEN A.HOLD_GUBUN = 'P' THEN A.HOLD_USER_ID" + "\n");
            strSqlString.Append("            ELSE (SELECT USER_DESC || '(' || USER_ID || ')' FROM RWEBUSRDEF WHERE USER_ID = A.HOLD_USER_ID)" + "\n");
            strSqlString.Append("       END AS OPER_USER" + "\n");
            strSqlString.Append("  FROM (" + "\n");
            strSqlString.Append("        SELECT A.*" + "\n");
            strSqlString.Append("             , ROW_NUMBER() OVER(PARTITION BY MAT_ID, HOLD_OPER, RUN_ID, LOT_ID ORDER BY HOLD_GUBUN) AS RNK" + "\n");
            strSqlString.Append("          FROM (" + "\n");
            strSqlString.Append("                SELECT 'Y' AS HOLD_GUBUN, B.MAT_ID, A.OPER AS HOLD_OPER, B.LOT_CMF_4 AS RUN_ID, A.LOT_ID" + "\n");
            strSqlString.Append("                     , B.QTY_1, B.OPER AS WIP_OPER, A.LOT_COMMENT, A.CREATE_TIME AS HOLD_TIME, A.CREATE_USER_ID AS HOLD_USER_ID" + "\n");
            strSqlString.Append("                  FROM MWIPFATDEF@RPTTOMES A" + "\n");
            strSqlString.Append("                     , RWIPLOTSTS B" + "\n");            
            strSqlString.Append("                 WHERE 1=1" + "\n");            
            strSqlString.Append("                   AND A.LOT_ID = B.LOT_ID(+)" + "\n");
            strSqlString.Append("                   AND A.FACTORY = '" + GlobalVariable.gsAssyDefaultFactory + "' " + "\n");
            strSqlString.Append("                   AND A.LOT_ID <> ' '" + "\n");
            strSqlString.Append("                   AND A.LOT_TYPE = 'W'" + "\n");
            strSqlString.Append("                   AND B.LOT_TYPE(+) = 'W' " + "\n");

            if (ckbWip.Checked == true)
            {
                strSqlString.Append("                   AND B.LOT_DEL_FLAG = ' ' " + "\n");
                strSqlString.Append("                   AND B.OPER LIKE 'A%' " + "\n");
                strSqlString.Append("                   AND A.OPER >= B.OPER " + "\n");
            }

            strSqlString.Append("                 UNION ALL" + "\n");
            strSqlString.Append("                SELECT ' ' AS HOLD_GUBUN, MAT_ID, OPER, LOT_CMF_4, LOT_ID, QTY_1, OPER, LAST_COMMENT, LAST_TRAN_TIME" + "\n");
            strSqlString.Append("                     , (SELECT HOLD_USER_ID FROM RWIPLOTHLD WHERE LOT_ID = A.LOT_ID AND HIST_SEQ = A.LAST_HIST_SEQ) AS HOLD_USER_ID" + "\n");
            strSqlString.Append("                  FROM RWIPLOTSTS A" + "\n");
            strSqlString.Append("                 WHERE 1=1" + "\n");
            strSqlString.Append("                   AND FACTORY = '" + GlobalVariable.gsAssyDefaultFactory + "'" + "\n");
            strSqlString.Append("                   AND LOT_TYPE = 'W'" + "\n");
            strSqlString.Append("                   AND LOT_DEL_FLAG = ' '" + "\n");
            strSqlString.Append("                   AND HOLD_FLAG = 'Y' " + "\n");
            strSqlString.Append("                 UNION ALL" + "\n");

            // 2019-04-09-임종우 :  변곡점 LOT (PCL CODE) 표시, Hold oper 는 결국 'A1790' 또는 'A1795' 공정에 걸리기에 'A1790' 으로 고정 (임태성차장 요청)
            strSqlString.Append("                SELECT 'P' AS HOLD_GUBUN, MAT_ID, 'A1790' AS HOLD_OPER, LOT_CMF_4, LOT_ID, QTY_1, OPER AS WIP_OPER" + "\n");
            strSqlString.Append("                     , (SELECT DATA_1 FROM MGCMTBLDAT@RPTTOMES WHERE FACTORY = A.FACTORY AND TABLE_NAME = 'H_QCM_PCLCODE' AND KEY_1 = A.LOT_CMF_16) AS LAST_COMMENT" + "\n");
            strSqlString.Append("                     , LAST_TRAN_TIME, A.LOT_CMF_16 AS HOLD_USER_ID" + "\n");
            strSqlString.Append("                  FROM RWIPLOTSTS A" + "\n");
            strSqlString.Append("                 WHERE 1=1" + "\n");
            strSqlString.Append("                   AND FACTORY = '" + GlobalVariable.gsAssyDefaultFactory + "'" + "\n");
            strSqlString.Append("                   AND LOT_TYPE = 'W'" + "\n");
            strSqlString.Append("                   AND LOT_DEL_FLAG = ' '" + "\n");
            strSqlString.Append("                   AND LOT_CMF_16 <> ' ' " + "\n");
            strSqlString.Append("                   AND OPER <= 'A1790'  " + "\n");
            strSqlString.Append("               ) A" + "\n");
            strSqlString.Append("       ) A" + "\n");
            strSqlString.Append("     , MWIPMATDEF B" + "\n");
            strSqlString.Append(" WHERE 1=1   " + "\n");
            strSqlString.Append("   AND A.MAT_ID = B.MAT_ID(+)" + "\n");
            strSqlString.Append("   AND B.FACTORY(+) = '" + GlobalVariable.gsAssyDefaultFactory + "'" + "\n");

            if (ckbHold.Checked == false)
            {
                strSqlString.Append("   AND A.HOLD_GUBUN = 'Y' " + "\n");
            }

            #region 상세 조회에 따른 SQL문 생성
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

            if (udcWIPCondition9.Text != "ALL" && udcWIPCondition9.Text != "")
                strSqlString.AppendFormat("   AND B.MAT_GRP_9 {0} " + "\n", udcWIPCondition9.SelectedValueToQueryString);
            #endregion

            // Lot Type
            //if (cdvLotType.Text != "ALL" && cdvStep.Text != "")
            //    strSqlString.AppendFormat("           AND STS.LOT_CMF_5 LIKE '" + cdvLotType.Text + "'" + "\n");


            strSqlString.AppendFormat("   AND A.WIP_OPER BETWEEN '" + cdvOper.FromText.ToString() + "' AND '" + cdvOper.ToText.ToString() + "' " + "\n");                

            // Product
            if (txtSearchProduct.Text.Trim() != "%" && txtSearchProduct.Text.Trim() != "")
            {
                strSqlString.AppendFormat("   AND A.MAT_ID LIKE '{0}'" + "\n", txtSearchProduct.Text);
            }


            // Hold Code
            //if (cdvHoldCode.Text != "ALL" && cdvHoldCode.Text != "")
            //    strSqlString.AppendFormat("           AND STS.HOLD_CODE {0}" + "\n", cdvHoldCode.SelectedValueToQueryString);

            strSqlString.Append("   AND A.RNK = 1 " + "\n");
            strSqlString.AppendFormat(" ORDER BY {0}, A.WIP_OPER " + "\n", QueryCond2);
            
            if (GlobalVariable.gsUserGroup == "ADMIN_GROUP" || GlobalVariable.gsUserGroup == "HANA_ADMIN_GROUP")
            {
                System.Windows.Forms.Clipboard.SetText(strSqlString.ToString());
            }

            return strSqlString.ToString();
        }

        #endregion

        #endregion

        #region ToExcel

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
        #endregion

        private void cdvFactory_ValueSelectedItemChanged(object sender, MCCodeViewSelChanged_EventArgs e)
        {
            this.SetFactory(cdvFactory.txtValue);                      
        }
    }
}

