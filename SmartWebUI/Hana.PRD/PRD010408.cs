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
    public partial class PRD010408 : Miracom.SmartWeb.UI.Controls.udcCUSReport002
    {
        /// <summary>
        /// 클  래  스: PRD010408<br/>
        /// 클래스요약: AO 결산<br/>
        /// 작  성  자: 하나마이크론 김준용<br/>
        /// 최초작성일: 2009-06-03<br/>
        /// 상세  설명: AO 결산<br/>
        /// IML 의 요청으로 결산 화면을 제작 함<br/>
		/// Blind Packing (EDS Test 하지 않음) 으로 인해 D/A In 값을 중요하게 생각함<br/>
		/// 
        /// 관련 테이블 
        /// 
        /// </summary>
        public PRD010408()
        {
            InitializeComponent();
            OptionInIt(); // 초기화
            SortInit();
            GridColumnInit(); //헤더 한줄짜리 
        }

        /// <summary>
        /// 
        /// </summary>
        private void OptionInIt()
        {
            cdvDate.AutoBinding(DateTime.Now.AddDays(-2).ToString(), DateTime.Now.AddDays(-1).ToString());
        }

        #region SortInit

        /// <summary>
        /// SortInit
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SortInit()
        {
			((udcTableForm)(this.btnSort.BindingForm)).AddRow("Customer", "(SELECT DATA_1 FROM MGCMTBLDAT WHERE TABLE_NAME = 'H_CUSTOMER' AND KEY_1 = MAT.MAT_GRP_1 AND ROWNUM=1) as Customer", "Customer", false);
			((udcTableForm)(this.btnSort.BindingForm)).AddRow("Family", "MAT.MAT_GRP_2 as Family", "Family", false);
			((udcTableForm)(this.btnSort.BindingForm)).AddRow("Package", "MAT.MAT_GRP_3 as Package", "Package", false);
			((udcTableForm)(this.btnSort.BindingForm)).AddRow("Type1", "MAT.MAT_GRP_4 as Type1", "Type1", false);
			((udcTableForm)(this.btnSort.BindingForm)).AddRow("Type2", "MAT.MAT_GRP_5 as Type2", "Type2", false);
			((udcTableForm)(this.btnSort.BindingForm)).AddRow("LD Count", "MAT.MAT_GRP_6 as LDCount", "LDCount", false);
			((udcTableForm)(this.btnSort.BindingForm)).AddRow("Density", "MAT.MAT_GRP_7 as Density", "Density", false);
			((udcTableForm)(this.btnSort.BindingForm)).AddRow("Generation", "MAT.MAT_GRP_8 as Generation", "Generation", false);
			((udcTableForm)(this.btnSort.BindingForm)).AddRow("MAJ Code", "MAT.MAT_GRP_9 as Maj_Code", "Maj_Code", false);
			((udcTableForm)(this.btnSort.BindingForm)).AddRow("PIN TYPE", "MAT.MAT_CMF_10 as PIN_TYPE", "PIN TYPE", true);
			((udcTableForm)(this.btnSort.BindingForm)).AddRow("Product", "MAT.MAT_ID as PIN_TYPE", "PIN TYPE", true);
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
			spdData.RPT_AddBasicColumn("Customer", 0, 0, Visibles.True, Frozen.True, Align.Left, Merge.True, Formatter.String, 70);
			spdData.RPT_AddBasicColumn("Family", 0, 1, Visibles.True, Frozen.True, Align.Left, Merge.True, Formatter.String, 70);
			spdData.RPT_AddBasicColumn("Package", 0, 2, Visibles.True, Frozen.True, Align.Left, Merge.True, Formatter.String, 70);
			spdData.RPT_AddBasicColumn("Type1", 0, 3, Visibles.True, Frozen.True, Align.Left, Merge.True, Formatter.String, 70);
			spdData.RPT_AddBasicColumn("Type2", 0, 4, Visibles.True, Frozen.True, Align.Left, Merge.True, Formatter.String, 70);
			spdData.RPT_AddBasicColumn("LD Count", 0, 5, Visibles.True, Frozen.True, Align.Left, Merge.True, Formatter.String, 70);
			spdData.RPT_AddBasicColumn("Density", 0, 6, Visibles.True, Frozen.True, Align.Left, Merge.True, Formatter.String, 70);
			spdData.RPT_AddBasicColumn("Generation", 0, 7, Visibles.True, Frozen.True, Align.Left, Merge.True, Formatter.String, 70);
			spdData.RPT_AddBasicColumn("Maj Code", 0, 8, Visibles.True, Frozen.True, Align.Left, Merge.True, Formatter.String, 70);
			spdData.RPT_AddBasicColumn("PIN TYPE", 0, 9, Visibles.True, Frozen.True, Align.Left, Merge.True, Formatter.String, 70);
			spdData.RPT_AddBasicColumn("Product", 0, 10, Visibles.True, Frozen.True, Align.Left, Merge.True, Formatter.String, 70);

            spdData.RPT_AddBasicColumn("LOT ID", 0, 11, Visibles.True, Frozen.True, Align.Left, Merge.True, Formatter.String, 80);
			spdData.RPT_AddBasicColumn("D/A IN", 0, 12, Visibles.True, Frozen.True, Align.Right, Merge.False, Formatter.Number, 80);
			spdData.RPT_AddBasicColumn("D/A OUT", 0, 13, Visibles.True, Frozen.True, Align.Right, Merge.False, Formatter.Number, 80);
            spdData.RPT_AddBasicColumn("PVI IN", 0, 14, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 80);
            spdData.RPT_AddBasicColumn("PVI OUT", 0, 15, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 80);
            spdData.RPT_AddBasicColumn("Yield", 0, 16, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Double2, 80);

            spdData.RPT_ColumnConfigFromTable(btnSort); //Group항목이 있을경우 반드시 선언해줄것.
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

                //그루핑 순서 1순위만 SubTotal을 사용하기 위해서 첫번째 값을 가져옴
                int sub = ((udcTableForm)(this.btnSort.BindingForm)).GetCheckedValue(1);

                ////by John (한줄짜리)
                ////1.Griid 합계 표시
				int[] rowType = spdData.RPT_DataBindingWithSubTotal(dt, 0, sub+1, 12, null, null, btnSort);

                ////2. 칼럼 고정(필요하다면..)
                spdData.Sheets[0].FrozenColumnCount = 11;

                ////3. Total부분 셀머지
				spdData.RPT_FillDataSelectiveCells("Total", 0, sub + 1, 0, 1, true, Align.Center, VerticalAlign.Center);

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

        #endregion

        #region CheckField

        /// <summary>
        /// CheckField
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private Boolean CheckField()
        {
			if (cdvFactory.Text.TrimEnd() == "")
			{
				CmnFunction.ShowMsgBox(RptMessages.GetMessage("STD001", GlobalVariable.gcLanguage));
				return false;
			}

			if (!cdvFactory.Text.Equals(GlobalVariable.gsAssyDefaultFactory))
			{
                CmnFunction.ShowMsgBox(RptMessages.GetMessage("STD054", GlobalVariable.gcLanguage));
				return false;
			}

            return true;
        }

        #endregion

        #region MakeSqlString
        private string MakeSqlString()
        {
			string QueryCond1 = null;
			//string QueryCond2 = null;

            //string sStart_Tran_Time = null;
            //string sEnd_Tran_Time = null;

            udcTableForm tableForm = (udcTableForm)btnSort.BindingForm;

            QueryCond1 = tableForm.SelectedValueToQueryContainNull;
            //QueryCond2 = tableForm.SelectedValue2ToQueryContainNull;

			StringBuilder strSqlString = new StringBuilder();
			strSqlString.Append("SELECT   " + "\n");
			strSqlString.Append("        " + QueryCond1 + ", \n");
			strSqlString.Append("        LOT_ID, " + "\n");
			strSqlString.Append("        DA_IN_QTY, " + "\n");
			strSqlString.Append("        DA_OUT_QTY, " + "\n");
			strSqlString.Append("        PVI_IN_QTY, " + "\n");
			strSqlString.Append("        PVI_OUT_QTY, " + "\n");
			strSqlString.Append("        DECODE(PVI_OUT_QTY,0,0,ROUND(PVI_OUT_QTY/DA_OUT_QTY * 100,2)) AS \"ASSY_YLD(DA OUT/PVI OUT)\" " + "\n");
			strSqlString.Append("FROM    ( " + "\n");
			strSqlString.Append("        SELECT    LOT.MAT_ID, " + "\n");
			strSqlString.Append("                LOT.LOT_ID, " + "\n");
			strSqlString.Append("                NVL(MAX(DECODE(HIS.OLD_OPER,'A0400',HIS.START_QTY_1)),0) AS DA_IN_QTY, " + "\n");
			strSqlString.Append("                NVL(MIN(DECODE(HIS.OLD_OPER,'A0400',HIS.QTY_1)),0) AS DA_OUT_QTY, " + "\n");
			strSqlString.Append("                NVL(MAX(DECODE(HIS.OLD_OPER,'A0400',ROUND((HIS.QTY_1/HIS.START_QTY_1)*100,2))),0)||'%' AS DA_YLD, " + "\n");
			strSqlString.Append("                NVL(MAX(DECODE(HIS.OLD_OPER,'A2100',HIS.START_QTY_1)),0) AS PVI_IN_QTY, " + "\n");
			strSqlString.Append("                NVL(MIN(DECODE(HIS.OLD_OPER,'A2100',HIS.QTY_1)),0) AS PVI_OUT_QTY, " + "\n");
			strSqlString.Append("                NVL(MAX(DECODE(HIS.OLD_OPER,'A2100',ROUND((HIS.QTY_1/HIS.START_QTY_1)*100,2))),0)||'%' AS PVI_YLD " + "\n");
			strSqlString.Append("        FROM    ( " + "\n");
			strSqlString.Append("                SELECT    MAT_ID, " + "\n");
			strSqlString.Append("                        LOT_ID, " + "\n");
			strSqlString.Append("                        QTY_1, " + "\n");
			strSqlString.Append("                        TRAN_TIME " + "\n");
			strSqlString.Append("                FROM    RWIPLOTHIS " + "\n");
			strSqlString.Append("                WHERE    1=1 " + "\n");
			strSqlString.Append("                        AND MAT_ID LIKE '" + txtProduct.Text + "' " + "\n");
			strSqlString.Append("                        AND TRAN_CODE = 'SHIP' " + "\n");
			strSqlString.Append("                        AND OLD_OPER IN ('AZ010') " + "\n");
			strSqlString.Append("                        AND OLD_FACTORY = '" + GlobalVariable.gsAssyDefaultFactory + "' " + "\n");
			strSqlString.Append("                        AND TRAN_TIME BETWEEN '" + cdvDate.Start_Tran_Time + "' AND '" + cdvDate.End_Tran_Time + "' " +"\n");
			strSqlString.Append("                        AND HIST_DEL_FLAG = ' ' " + "\n");
			strSqlString.Append("                ) LOT, " + "\n");
			strSqlString.Append("                RWIPLOTHIS HIS " + "\n");
			strSqlString.Append("        WHERE    1=1 " + "\n");
			strSqlString.Append("                AND LOT.LOT_ID = HIS.LOT_ID " + "\n");
			strSqlString.Append("                AND HIS.TRAN_CODE = 'END' " + "\n");
			strSqlString.Append("                AND HIS.LOT_CMF_5 " + cdvLotType.SelectedValueToQueryString + "\n");
			strSqlString.Append("                AND HIS.FACTORY " + cdvFactory.SelectedValueToQueryString + "\n");
			strSqlString.Append("                AND HIS.HIST_DEL_FLAG = ' ' " + "\n");
			strSqlString.Append("        GROUP BY LOT.MAT_ID,LOT.LOT_ID " + "\n");
			strSqlString.Append("        ) HIS, " + "\n");
			strSqlString.Append("         MWIPMATDEF MAT " + "\n");
			strSqlString.Append("WHERE   HIS.MAT_ID=MAT.MAT_ID " + "\n");
			strSqlString.Append("        AND MAT.FACTORY " + cdvFactory.SelectedValueToQueryString + "\n");

			//상세 조회에 따른 SQL문 생성                        
			if (udcWIPCondition1.Text != "ALL" && udcWIPCondition1.Text != "")
				strSqlString.AppendFormat("                          AND MAT.MAT_GRP_1 {0} " + "\n", udcWIPCondition1.SelectedValueToQueryString);

			if (udcWIPCondition2.Text != "ALL" && udcWIPCondition2.Text != "")
				strSqlString.AppendFormat("                          AND MAT.MAT_GRP_2 {0} " + "\n", udcWIPCondition2.SelectedValueToQueryString);

			if (udcWIPCondition3.Text != "ALL" && udcWIPCondition3.Text != "")
				strSqlString.AppendFormat("                          AND MAT.MAT_GRP_3 {0} " + "\n", udcWIPCondition3.SelectedValueToQueryString);

			if (udcWIPCondition4.Text != "ALL" && udcWIPCondition4.Text != "")
				strSqlString.AppendFormat("                          AND MAT.MAT_GRP_4 {0} " + "\n", udcWIPCondition4.SelectedValueToQueryString);

			if (udcWIPCondition5.Text != "ALL" && udcWIPCondition5.Text != "")
				strSqlString.AppendFormat("                          AND MAT.MAT_GRP_5 {0} " + "\n", udcWIPCondition5.SelectedValueToQueryString);

			if (udcWIPCondition6.Text != "ALL" && udcWIPCondition6.Text != "")
				strSqlString.AppendFormat("                          AND MAT.MAT_GRP_6 {0} " + "\n", udcWIPCondition6.SelectedValueToQueryString);

			if (udcWIPCondition7.Text != "ALL" && udcWIPCondition7.Text != "")
				strSqlString.AppendFormat("                          AND MAT.MAT_GRP_7 {0} " + "\n", udcWIPCondition7.SelectedValueToQueryString);

			if (udcWIPCondition8.Text != "ALL" && udcWIPCondition8.Text != "")
				strSqlString.AppendFormat("                          AND MAT.MAT_GRP_8 {0} " + "\n", udcWIPCondition8.SelectedValueToQueryString);

            if (udcWIPCondition9.Text != "ALL" && udcWIPCondition9.Text != "")
                strSqlString.AppendFormat("                          AND MAT.MAT_GRP_9 {0} " + "\n", udcWIPCondition9.SelectedValueToQueryString);

			strSqlString.Append("ORDER BY MAT.MAT_ID,LOT_ID " + "\n");


			//StringBuilder strSqlString = new StringBuilder();
			//strSqlString.Append("SELECT  MAT.MAT_CMF_10 AS PKG, " + "\n");
			//strSqlString.Append("        SHP.MAT_ID AS PRODUCT, " + "\n");
			//strSqlString.Append("        SHP.LOT_ID, " + "\n");
			//strSqlString.Append("        SHP.TEST_LOT_ID, " + "\n");
			//strSqlString.Append("        SHP.TEST_IN_QTY_1, " + "\n");
			//strSqlString.Append("        SHP.TEST_OUT_QTY_1, " + "\n");
			//strSqlString.Append("        SHP.TEST_BONUS_QTY_1, " + "\n");
			//strSqlString.Append("        SHP.TEST_LOSS_QTY_1, " + "\n");
			//strSqlString.Append("        TO_CHAR(TO_DATE(SHP.SHIP_TIME,'YYYYMMDDHH24MISS')) AS SHIP_TIME, " + "\n");
			//strSqlString.Append("        SHP.SHIP_LOT_FLAG " + "\n");
			//strSqlString.Append("FROM    CSUMTSTSHP@RPTTOMES SHP, " + "\n");
			//strSqlString.Append("        MWIPMATDEF MAT " + "\n");
			//strSqlString.Append("WHERE   1=1 " + "\n");
			//strSqlString.Append("        AND SHP.MAT_ID = MAT.MAT_ID " + "\n");
			//strSqlString.Append("        AND MAT.DELETE_FLAG = ' ' " + "\n");
			//strSqlString.Append("        AND MAT.FACTORY = '" + GlobalVariable.gsTestDefaultFactory + "' " + "\n");

			//if (checkHis.Checked == false)
			//{
			//    strSqlString.Append("        AND SHP.SHIP_LOT_FLAG='Y' " + "\n");
			//}

			//strSqlString.Append("        AND SHP.MAT_ID LIKE '"+ txtProduct.Text +"' " + "\n");
			//strSqlString.Append("        AND SHP.SHIP_TIME BETWEEN '"+ cdvDate.Start_Tran_Time +"' AND '"+ cdvDate.End_Tran_Time +"' " + "\n");
			//strSqlString.Append("ORDER BY SHP.MAT_ID,SHP.LOT_ID " + "\n");

            if (GlobalVariable.gsUserGroup == "ADMIN_GROUP" || GlobalVariable.gsUserGroup == "HANA_ADMIN_GROUP")
            {
                System.Windows.Forms.Clipboard.SetText(strSqlString.ToString());
            }            

            return strSqlString.ToString();
        }

        #endregion

        #region ToExcel

        /// <summary>
        /// Excel Export
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnExcelExport_Click(object sender, EventArgs e)
        {
            if (spdData.ActiveSheet.Rows.Count == 0)
            {
                CmnFunction.ShowMsgBox(RptMessages.GetMessage("STD002", GlobalVariable.gcLanguage));
                return;
            }
            
            // Excel 바로 보이기
            ExcelHelper.Instance.subMakeExcel(spdData, this.lblTitle.Text, " ^ ", " ^ ", true);
        }
        #endregion

        private void txtProduct_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                btnView_Click(sender, e);
            }
        }

        private void PRD010406_Load_1(object sender, EventArgs e)
        {
            txtProduct.Focus();
        }

		private void cdvFactory_ValueSelectedItemChanged(object sender, MCCodeViewSelChanged_EventArgs e)
		{
			this.SetFactory(cdvFactory.txtValue);
			cdvLotType.sFactory = cdvFactory.txtValue;
		}
    }
}