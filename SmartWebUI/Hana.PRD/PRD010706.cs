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
    public partial class PRD010706 : Miracom.SmartWeb.UI.Controls.udcCUSReport002
    {
        /// <summary>
        /// 클  래  스: PRD010706<br/>
        /// 클래스요약: Lot별 Merge List<br/>
        /// 작  성  자: 하나마이크론 임종우<br/>
        /// 최초작성일: 2017-03-09<br/>
        /// 상세  설명: Lot별 Merge List(황선미D 요청)<br/>
        /// 변경  내용: <br/>
        /// </summary>
        /// 2019-12-17-김미경 : 공정 다중 선택 - 이동진 D
        /// 2019-12-31-김미경 : NXP 고객사 모 LOT, MERGE 된 LOT의 SO NO 표기 - 이동진 D
        /// 2020-01-13-김미경 : 날짜 선택 조회 시, ship 날짜 기준 lot으로 조회한다. - 임종우 책임
        public PRD010706()
        {
            InitializeComponent();            
            cdvFromToDate.AutoBinding();
            OptionInit();
            SortInit();
            GridColumnInit();
        }

        private void OptionInit()
        {
            rbtWIP.Checked = true;
            udcDate.Enabled = false;
            udcDate.AutoBinding(DateTime.Now.ToString(), DateTime.Now.ToString());
        }

        #region 유효성검사 및 초기화
        /// <summary 1. 유효성 검사>
        /// <remarks></remarks>
        /// </summary>
        /// <returns></returns>
        private Boolean CheckField()
        {
            if (cdvFactory.Text.TrimEnd() == "")
            {
                CmnFunction.ShowMsgBox(RptMessages.GetMessage("STD001", GlobalVariable.gcLanguage));
                return false;
            }
            
            if ((cdvOper.Text == "ALL" || cdvOper.Text == ""))
            {
                CmnFunction.ShowMsgBox(RptMessages.GetMessage("STD005", GlobalVariable.gcLanguage));
                return false;
            }

            if (udcWIPCondition1.Text == "ALL" || udcWIPCondition1.Text == "")            
            {
                CmnFunction.ShowMsgBox(RptMessages.GetMessage("STD038", GlobalVariable.gcLanguage));
                return false;
            }

            return true;
        }

        /// <summary>
        /// 2. 헤더 생성
        /// </summary>
        private void GridColumnInit()
        {
            spdData.ActiveSheet.ColumnHeader.Rows.Count = 1;

            spdData.RPT_ColumnInit();            
            spdData.RPT_AddBasicColumn("CUSTOMER", 0, 0, Visibles.True, Frozen.True, Align.Left, Merge.True, Formatter.String, 80);
            spdData.RPT_AddBasicColumn("MAJOR CODE", 0, 1, Visibles.False, Frozen.True, Align.Left, Merge.True, Formatter.String, 60); 
            spdData.RPT_AddBasicColumn("FAMILY", 0, 2, Visibles.False, Frozen.True, Align.Left, Merge.True, Formatter.String, 60);
            spdData.RPT_AddBasicColumn("PACKAGE", 0, 3, Visibles.False, Frozen.True, Align.Left, Merge.True, Formatter.String, 60);
            spdData.RPT_AddBasicColumn("TYPE1", 0, 4, Visibles.False, Frozen.True, Align.Left, Merge.True, Formatter.String, 70);
            spdData.RPT_AddBasicColumn("TYPE2", 0, 5, Visibles.False, Frozen.True, Align.Left, Merge.True, Formatter.String, 70);
            spdData.RPT_AddBasicColumn("LD COUNT", 0, 6, Visibles.False, Frozen.True, Align.Left, Merge.True, Formatter.String, 70);
            spdData.RPT_AddBasicColumn("DENSITY", 0, 7, Visibles.False, Frozen.True, Align.Left, Merge.True, Formatter.String, 70);
            spdData.RPT_AddBasicColumn("GENERATION", 0, 8, Visibles.False, Frozen.True, Align.Center, Merge.True, Formatter.String, 70);
            spdData.RPT_AddBasicColumn("PIN TYPE", 0, 9, Visibles.True, Frozen.True, Align.Left, Merge.True, Formatter.String, 70);
            spdData.RPT_AddBasicColumn("PRODUCT", 0, 10, Visibles.True, Frozen.True, Align.Left, Merge.True, Formatter.String, 70);
            spdData.RPT_AddBasicColumn("CUST DEVICE", 0, 11, Visibles.False, Frozen.True, Align.Left, Merge.True, Formatter.String, 70);
            spdData.RPT_AddBasicColumn("SAP_CODE", 0, 12, Visibles.False, Frozen.True, Align.Right, Merge.False, Formatter.String, 70);

            int headerCount = 13;
           
            if (udcWIPCondition1.Text == "NX")
            {
                spdData.RPT_AddBasicColumn("SO_NO", 0, headerCount, Visibles.True, Frozen.True, Align.Left, Merge.False, Formatter.String, 70);
                headerCount++;
            }
            spdData.RPT_AddBasicColumn("LOT ID", 0, headerCount, Visibles.True, Frozen.True, Align.Right, Merge.False, Formatter.String, 70);
            headerCount++;
            spdData.RPT_AddBasicColumn("QTY", 0, headerCount, Visibles.True, Frozen.True, Align.Right, Merge.False, Formatter.Number, 70);
            headerCount++;
            spdData.RPT_AddBasicColumn("WW CODE", 0, headerCount, Visibles.True, Frozen.True, Align.Right, Merge.False, Formatter.String, 70);
            headerCount++;

            for (int i = 1; i <= 30; i++)
            {
                if (udcWIPCondition1.Text == "NX")
                {
                    spdData.RPT_AddBasicColumn("MERGE_LOT_ID_" + i+"_SO_NO", 0, headerCount, Visibles.True, Frozen.False, Align.Left, Merge.False, Formatter.String, 80);
                    headerCount++;
                }
                spdData.RPT_AddBasicColumn("MERGE_LOT_ID_" + i, 0, headerCount, Visibles.True, Frozen.False, Align.Left, Merge.False, Formatter.String, 80);
                headerCount++;
                spdData.RPT_AddBasicColumn("MERGE_QTY_" + i, 0, headerCount, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 80);
                headerCount++;
                spdData.RPT_AddBasicColumn("WW_CODE_" + i, 0, headerCount, Visibles.True, Frozen.False, Align.Left, Merge.False, Formatter.String, 80);
                headerCount++;
            }
            
            spdData.RPT_ColumnConfigFromTable(btnSort); //Group항목이 있을경우 반드시 선언해줄것.
       
        }

        /// <summary>
        /// 3. Group By 정의
        /// </summary>
        private void SortInit()
        {
            
            ((udcTableForm)(this.btnSort.BindingForm)).Clear();
            ((udcTableForm)(this.btnSort.BindingForm)).AddRow("CUSTOMER", "B.MAT_GRP_1", "MAT_GRP_1", "(SELECT DATA_1 FROM MGCMTBLDAT WHERE TABLE_NAME = 'H_CUSTOMER' AND KEY_1 = MAT_GRP_1 AND ROWNUM=1) AS CUSTOMER", "MAT_GRP_1", true);
            ((udcTableForm)(this.btnSort.BindingForm)).AddRow("MAJOR CODE", "B.MAT_GRP_9", "MAT_GRP_9", "MAT_GRP_9", "MAT_GRP_9", false);
            ((udcTableForm)(this.btnSort.BindingForm)).AddRow("FAMILY", "B.MAT_GRP_2", "MAT_GRP_2", "MAT_GRP_2", "MAT_GRP_2", false);
            ((udcTableForm)(this.btnSort.BindingForm)).AddRow("PACKAGE", "B.MAT_GRP_3", "MAT_GRP_3", "MAT_GRP_3", "MAT_GRP_3", false);
            ((udcTableForm)(this.btnSort.BindingForm)).AddRow("TYPE1", "B.MAT_GRP_4", "MAT_GRP_4", "MAT_GRP_4", "MAT_GRP_4", false);
            ((udcTableForm)(this.btnSort.BindingForm)).AddRow("TYPE2", "B.MAT_GRP_5", "MAT_GRP_5", "MAT_GRP_5", "MAT_GRP_5", false);
            ((udcTableForm)(this.btnSort.BindingForm)).AddRow("LD Count", "B.MAT_GRP_6", "MAT_GRP_6", "MAT_GRP_6", "MAT_GRP_6", false);
            ((udcTableForm)(this.btnSort.BindingForm)).AddRow("DENSITY", "B.MAT_GRP_7", "MAT_GRP_7", "MAT_GRP_7", "MAT_GRP_7", false);
            ((udcTableForm)(this.btnSort.BindingForm)).AddRow("GENERATION", "B.MAT_GRP_8", "MAT_GRP_8", "MAT_GRP_8", "MAT_GRP_8", false);
            ((udcTableForm)(this.btnSort.BindingForm)).AddRow("PIN_TYPE", "B.MAT_CMF_10", "MAT_CMF_10", "MAT_CMF_10", "MAT_CMF_10", true);
            ((udcTableForm)(this.btnSort.BindingForm)).AddRow("PRODUCT", "B.MAT_ID", "MAT.MAT_ID", "MAT.MAT_ID", "MAT_ID", true);            
            ((udcTableForm)(this.btnSort.BindingForm)).AddRow("CUST_DEVICE", "B.MAT_CMF_7", "MAT_CMF_7", "MAT_CMF_7", "MAT_CMF_7", false);
            ((udcTableForm)(this.btnSort.BindingForm)).AddRow("SAP_CODE", "VENDOR_MAT_ID", "VENDOR_MAT_ID", "VENDOR_MAT_ID AS SAP_CODE", "VENDOR_MAT_ID", false);
            
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

            string QueryCond1 = null;
            string QueryCond2 = null;
            string QueryCond3 = null;
            string QueryCond4 = null;

            string strDate = string.Empty;      
            string sGroupBy = string.Empty;
            string sHeader = string.Empty;
            string strVal1 = string.Empty;
            string strVal2 = string.Empty;
            string bbbb = string.Empty;
            string sSelectQty = string.Empty;

            string[] selectDate = new string[cdvFromToDate.SubtractBetweenFromToDate + 1];
            selectDate = cdvFromToDate.getSelectDate();
            
            udcTableForm tableForm = (udcTableForm)btnSort.BindingForm;

            QueryCond1 = tableForm.SelectedValueToQueryContainNull;
            QueryCond2 = tableForm.SelectedValue2ToQueryContainNull;
            QueryCond3 = tableForm.SelectedValue3ToQueryContainNull;
            QueryCond4 = tableForm.SelectedValue4ToQueryContainNull;

            String strStartDate = udcDate.Start_Tran_Time;
            String strEndDate = udcDate.End_Tran_Time;

            if (rbtShip.Checked == true)
            {
                strSqlString.Append("WITH DT AS" + "\n");
                strSqlString.Append("(" + "\n");
                strSqlString.Append(" SELECT LOT_ID, FROM_FACTORY AS FACTORY, MAT_ID, SHIP_QTY_1 AS QTY_1, LOT_TYPE, LOT_CMF_3, LOT_CMF_5, LOT_CMF_10" + "\n");
                strSqlString.Append("   FROM VWIPSHPLOT " + "\n");
                strSqlString.Append("  WHERE 1=1 " + "\n");
                strSqlString.Append("    AND FROM_FACTORY " + cdvFactory.SelectedValueToQueryString + "\n");
                strSqlString.Append("    AND LOT_TYPE = 'W'" + "\n");
                strSqlString.Append("    AND TO_FACTORY IN ('CUSTOMER', 'FGS')" + "\n");
                strSqlString.Append("    AND OWNER_CODE = 'PROD'" + "\n");
                strSqlString.Append("    AND FROM_OPER IN ('AZ010','AZ009','TZ010','EZ010', 'F0000', 'BZ010')" + "\n");
                strSqlString.Append("    AND TRAN_TIME BETWEEN '" + strStartDate + "' AND '" + strEndDate + "' " + "\n");
                strSqlString.Append(")" + "\n");    
            }
            else if(rbtWIP.Checked == true)
            {
                strSqlString.Append("WITH DT AS" + "\n");
                strSqlString.Append("(" + "\n");
                strSqlString.Append(" SELECT * " + "\n");
                strSqlString.Append("   FROM RWIPLOTSTS " + "\n");
                strSqlString.Append("  WHERE 1=1 " + "\n");
                strSqlString.Append("    AND LOT_DEL_FLAG = ' ' " + "\n");
                strSqlString.Append("    AND OPER " + cdvOper.SelectedValueToQueryString + "\n");
                strSqlString.Append(")" + "\n"); 
            }
            
            strSqlString.AppendFormat("SELECT {0}" + "\n", QueryCond3);          
            if (udcWIPCondition1.Text == "NX")
            {
                strSqlString.Append("             , MAX(LOT_CMF_3) AS SO_NO" + "\n");
            }
            strSqlString.Append("     , LOT_ID" + "\n");
            strSqlString.Append("     , MAX(QTY_1) AS QTY_1" + "\n");
            strSqlString.Append("     , MAX(WEEK_CODE) AS WEEK_CODE" + "\n");

            for (int i = 1;i <= 30; i++)
            {
                if (udcWIPCondition1.Text == "NX")
                {
                    strSqlString.Append("     , MAX(DECODE(RNK, " + i + ", MERGE_LOT_SO_NO)) AS MERGE_LOT_" + i + "_SO_NO\n");
                }
                strSqlString.Append("     , MAX(DECODE(RNK, " + i + ", MERGE_LOT_ID)) AS MERGE_LOT_" + i + "\n");
                strSqlString.Append("     , MAX(DECODE(RNK, " + i + ", MERGE_QTY)) AS MERGE_QTY_" + i + "\n");
                strSqlString.Append("     , MAX(DECODE(RNK, " + i + ", MERGE_WW_CODE)) AS MERGE_WW_CODE_" + i + "\n");
            }
                        
            strSqlString.Append("  FROM MWIPMATDEF MAT" + "\n");            
            strSqlString.Append("     , (" + "\n");
            strSqlString.Append("        SELECT A.FACTORY" + "\n");
            strSqlString.Append("             , A.MAT_ID" + "\n");        
            if (udcWIPCondition1.Text == "NX")
            {
                strSqlString.Append("             , A.LOT_CMF_3" + "\n");
            }
            strSqlString.Append("             , A.LOT_ID" + "\n");
            strSqlString.Append("             , A.QTY_1" + "\n");
            strSqlString.Append("             , A.LOT_CMF_10 AS WEEK_CODE" + "\n");
            strSqlString.Append("             , B.FROM_TO_LOT_ID AS MERGE_LOT_ID" + "\n");
            strSqlString.Append("             , B.QTY_1 - B.OLD_QTY_1 AS MERGE_QTY" + "\n");
            strSqlString.Append("             , B.HIST_SEQ" + "\n");
            strSqlString.Append("             , (SELECT LOT_CMF_10  FROM RWIPLOTSTS WHERE LOT_ID = B.FROM_TO_LOT_ID) AS MERGE_WW_CODE" + "\n");
            strSqlString.Append("             , RANK() OVER(PARTITION BY A.LOT_ID ORDER BY B.HIST_SEQ) AS RNK" + "\n");
            if (udcWIPCondition1.Text == "NX")
            {
                strSqlString.Append("             , (SELECT LOT_CMF_3  FROM RWIPLOTSTS WHERE LOT_ID = B.FROM_TO_LOT_ID) AS MERGE_LOT_SO_NO" + "\n");
            }
            //strSqlString.Append("        FROM RWIPLOTSTS A" + "\n");
            strSqlString.Append("        FROM DT A" + "\n");
            strSqlString.Append("           , RWIPLOTHIS B" + "\n");
            strSqlString.Append("       WHERE 1=1 " + "\n");
            strSqlString.Append("         AND A.LOT_ID = B.LOT_ID(+)" + "\n");
            strSqlString.Append("         AND A.FACTORY = '" + GlobalVariable.gsTestDefaultFactory + "'" + "\n");
            strSqlString.Append("         AND A.LOT_TYPE = 'W'" + "\n");
            //strSqlString.Append("         AND A.LOT_DEL_FLAG = ' '" + "\n");
            //strSqlString.Append("         AND A.OPER = '" + cdvOper.Text + "'" + "\n");
            //strSqlString.Append("         AND A.OPER " + cdvOper.SelectedValueToQueryString + "\n");
            strSqlString.Append("         AND B.TRAN_CODE(+) = 'MERGE'" + "\n");
            strSqlString.Append("         AND B.FROM_TO_FLAG(+) = 'T'" + "\n");
            strSqlString.Append("         AND B.HIST_DEL_FLAG(+) = ' ' " + "\n");                     

            if (cdvLotType.Text != "ALL")
            {
                strSqlString.AppendFormat("         AND A.LOT_CMF_5 LIKE '" + cdvLotType.Text + "'" + "\n");
            }

            strSqlString.Append("       ) DAT" + "\n");
            strSqlString.Append(" WHERE 1=1" + "\n");
            strSqlString.Append("   AND MAT.FACTORY = DAT.FACTORY" + "\n");
            strSqlString.Append("   AND MAT.MAT_ID = DAT.MAT_ID" + "\n");
            strSqlString.Append("   AND MAT.DELETE_FLAG = ' '" + "\n");

            strSqlString.AppendFormat("   AND MAT.MAT_ID LIKE '{0}'  " + "\n", txtSearchProduct.Text);

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

            strSqlString.AppendFormat("GROUP BY {0}, LOT_ID" + "\n", QueryCond2);
            strSqlString.AppendFormat("ORDER BY {0}" + "\n", QueryCond2);

            if (GlobalVariable.gsUserGroup == "ADMIN_GROUP" || GlobalVariable.gsUserGroup == "HANA_ADMIN_GROUP")
            {
                System.Windows.Forms.Clipboard.SetText(strSqlString.ToString());
            }
            
            return strSqlString.ToString();
        }
           
        #endregion


        #region EVENT 처리

        private void PRD010706_Load(object sender, EventArgs e)
        {
            pnlWIPDetail.Visible = true;
        }     

        private void btnView_Click(object sender, EventArgs e)
        {

            DataTable dt = null;
            if (CheckField() == false) return;

            GridColumnInit();

            spdData_Sheet1.RowCount = 0;

            try
            {
                LoadingPopUp.LoadIngPopUpShow(this);
                this.Refresh();

                dt = CmnFunction.oComm.GetFuncDataTable("DYNAMIC", MakeSqlString());

                if (dt.Rows.Count == 0)
                {
                    dt.Dispose();
                    LoadingPopUp.LoadingPopUpHidden();
                    CmnFunction.ShowMsgBox(RptMessages.GetMessage("STD010", GlobalVariable.gcLanguage));
                    return;
                }

                //int sub;

                //그루핑 순서 1순위만 SubTotal을 사용하기 위해서 첫번째 값을 가져옴
                //sub = ((udcTableForm)(this.btnSort.BindingForm)).GetCheckedValue(2);

                //by John
                //1.Griid 합계 표시
                //int[] rowType = spdData.RPT_DataBindingWithSubTotal(dt, 0, sub+1, 12, null, null, btnSort);

           
                //2. 칼럼 고정(필요하다면..)
                //spdData.Sheets[0].FrozenColumnCount = 10;

                //3. Total부분 셀머지
                //spdData.RPT_FillDataSelectiveCells("Total", 0, 13, 0, 1, true, Align.Center, VerticalAlign.Center);

                spdData.DataSource = dt;

                //4. Column Auto Fit
                spdData.RPT_AutoFit(false);

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

        private void cdvFactory_ValueSelectedItemChanged(object sender, Miracom.UI.MCCodeViewSelChanged_EventArgs e)
        {
            this.SetFactory(cdvFactory.txtValue);            
            cdvOper.sFactory = cdvFactory.txtValue;

            SortInit(); //Add. 20150602
        }

        private void Radio_CheckedChanged(object sender, EventArgs e)
        {
            if (rbtWIP.Checked == true)
            {
                udcDate.Enabled = false;
            }
            else if (rbtShip.Checked == true)
            {
                udcDate.AutoBinding(DateTime.Now.ToString(), DateTime.Now.ToString());
                udcDate.Enabled = true;
            }
        }
        #endregion           
    }
}