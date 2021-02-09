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
    public partial class PRD010301 : Miracom.SmartWeb.UI.Controls.udcCUSReport002
    {
        /// <summary>
        /// 클  래  스: PRD010301<br/>
        /// 클래스요약: 창고 재공 조회<br/>
        /// 작  성  자: 미라콤 김태순<br/>
        /// 최초작성일: 2008-11-27<br/>
        /// 상세  설명: 창고 제공 현황 조회.<br/>
        /// 변경  내용: <br/>
        /// 변  경  자: 하나마이크론 김준용<br />
        /// Excel Export 저장 기능 변경<br />
        /// </summary>
        public PRD010301()
        {
            InitializeComponent();
            //udcDurationDate1.AutoBinding();
            DelayInit();

            SortInit();
            GridColumnInit(); //헤더 한줄짜리 

            this.cdvType.sFactory = GlobalVariable.gsAssyDefaultFactory;
            cdvFactory.Text = GlobalVariable.gsAssyDefaultFactory;
        }

        #region DelayInit
        /// <summary>
        /// DelayInit
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DelayInit()
        {
            cboDelayDay.Items.Clear();
            for (int i = 0; i < 100; i++)
            {
                cboDelayDay.Items.Add(i);
            }
        }
        #endregion

        #region SortInit

        /// <summary>
        /// SortInit
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SortInit()
        {
            ((udcTableForm)(this.btnSort.BindingForm)).AddRow("Customer", "(SELECT DATA_1 FROM MGCMTBLDAT WHERE TABLE_NAME = 'H_CUSTOMER' AND KEY_1 = B.MAT_GRP_1 AND ROWNUM=1) as Customer", "Customer", false);
            ((udcTableForm)(this.btnSort.BindingForm)).AddRow("Family", "B.MAT_GRP_2 AS Family", "Family", false);
            ((udcTableForm)(this.btnSort.BindingForm)).AddRow("Package", "B.MAT_GRP_3 AS Package", "Package", false);
            ((udcTableForm)(this.btnSort.BindingForm)).AddRow("Type1", "B.MAT_GRP_4 AS Type1", "Type1", false);
            ((udcTableForm)(this.btnSort.BindingForm)).AddRow("Type2", "B.MAT_GRP_5 AS Type2", "Type2", false);
            ((udcTableForm)(this.btnSort.BindingForm)).AddRow("LD Count", "B.MAT_GRP_6 AS LDCount", "LDCount", false);
            ((udcTableForm)(this.btnSort.BindingForm)).AddRow("Density", "B.MAT_GRP_7 AS Density", "Density", false);
            ((udcTableForm)(this.btnSort.BindingForm)).AddRow("Generation", "B.MAT_GRP_8 AS Generation", "Generation", false);
            ((udcTableForm)(this.btnSort.BindingForm)).AddRow("PIN_TYPE", "B.MAT_CMF_10 AS PIN_TYPE", "PIN_TYPE", false);
            ((udcTableForm)(this.btnSort.BindingForm)).AddRow("Oper", "A.OPER AS OPER", "OPER", true);
            ((udcTableForm)(this.btnSort.BindingForm)).AddRow("CUST_DEVICE", "B.MAT_CMF_7 AS CUST_DEVICE", "CUST_DEVICE", false);
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
            spdData.RPT_AddBasicColumn("Customer", 0, 0, Visibles.False, Frozen.True, Align.Center, Merge.True, Formatter.String, 80);
            spdData.RPT_AddBasicColumn("Family", 0, 1, Visibles.False, Frozen.True, Align.Center, Merge.True, Formatter.String, 80);
            spdData.RPT_AddBasicColumn("Package", 0, 2, Visibles.False, Frozen.True, Align.Center, Merge.True, Formatter.String, 80);
            spdData.RPT_AddBasicColumn("Type1", 0, 3, Visibles.False, Frozen.True, Align.Center, Merge.True, Formatter.String, 80);
            spdData.RPT_AddBasicColumn("Type2", 0, 4, Visibles.False, Frozen.True, Align.Center, Merge.True, Formatter.String, 80);
            spdData.RPT_AddBasicColumn("LD Count", 0, 5, Visibles.False, Frozen.True, Align.Center, Merge.True, Formatter.String, 80);
            spdData.RPT_AddBasicColumn("Density", 0, 6, Visibles.False, Frozen.True, Align.Center, Merge.True, Formatter.String, 80);
            spdData.RPT_AddBasicColumn("Generation", 0, 7, Visibles.False, Frozen.True, Align.Center, Merge.True, Formatter.String, 80);
            spdData.RPT_AddBasicColumn("PIN TYPE", 0, 8, Visibles.False, Frozen.True, Align.Left, Merge.True, Formatter.String, 80);
            spdData.RPT_AddBasicColumn("Oper", 0, 9, Visibles.True, Frozen.True, Align.Center, Merge.True, Formatter.String, 80);
            spdData.RPT_AddBasicColumn("CUST_DEVICE", 0, 10, Visibles.False, Frozen.True, Align.Left, Merge.True, Formatter.String, 80);
            spdData.RPT_AddBasicColumn("LOT", 0, 11, Visibles.True, Frozen.True, Align.Left, Merge.True, Formatter.String, 80);
            spdData.RPT_AddBasicColumn("Product", 0, 12, Visibles.True, Frozen.False, Align.Left, Merge.True, Formatter.String, 80);
            spdData.RPT_AddBasicColumn("PRI", 0, 13, Visibles.True, Frozen.False, Align.Center, Merge.False, Formatter.String, 80);
            spdData.RPT_AddBasicColumn("Type", 0, 14, Visibles.True, Frozen.False, Align.Center, Merge.False, Formatter.String, 80);

            if (cdvArea.Text == "HMK2")
            {
                spdData.RPT_AddBasicColumn("Wf Qty", 0, 15, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 80);
                spdData.RPT_AddBasicColumn("Start Qty", 0, 16, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 80);
                spdData.RPT_AddBasicColumn("Qty", 0, 17, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 80);
                spdData.RPT_AddBasicColumn("Stock In Time", 0, 18, Visibles.True, Frozen.False, Align.Center, Merge.False, Formatter.String, 80);
                spdData.RPT_AddBasicColumn("Ship site", 0, 19, Visibles.True, Frozen.False, Align.Center, Merge.False, Formatter.String, 80);
                spdData.RPT_AddBasicColumn("Fab Site", 0, 20, Visibles.True, Frozen.False, Align.Center, Merge.False, Formatter.String, 80);
                spdData.RPT_AddBasicColumn("NCF_CODE", 0, 21, Visibles.True, Frozen.False, Align.Center, Merge.False, Formatter.String, 80);
                spdData.RPT_AddBasicColumn("Delay(H)", 0, 22, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Double2, 80);
                spdData.RPT_AddBasicColumn("Delay(D)", 0, 23, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Double2, 80);                
            }
            else
            {
                spdData.RPT_AddBasicColumn("Start Qty", 0, 15, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 80);
                spdData.RPT_AddBasicColumn("Qty", 0, 16, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 80);
                spdData.RPT_AddBasicColumn("Stock In Time", 0, 17, Visibles.True, Frozen.False, Align.Center, Merge.False, Formatter.String, 80);
                spdData.RPT_AddBasicColumn("Ship site", 0, 18, Visibles.True, Frozen.False, Align.Center, Merge.False, Formatter.String, 80);
                spdData.RPT_AddBasicColumn("Fab Site", 0, 19, Visibles.True, Frozen.False, Align.Center, Merge.False, Formatter.String, 80);
                spdData.RPT_AddBasicColumn("NCF_CODE", 0, 20, Visibles.True, Frozen.False, Align.Center, Merge.False, Formatter.String, 80);
                spdData.RPT_AddBasicColumn("Delay(H)", 0, 21, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Double2, 80);
                spdData.RPT_AddBasicColumn("Delay(D)", 0, 22, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Double2, 80);
                
            }


            //spdData.RPT_AddDynamicColumn(udcDurationDate1, 0, 14, Visibles.False, Frozen.False, Align.Right, Merge.False, Formatter.String,60);
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



                //그루핑 순서 1순위만 SubTotal을 사용하기 위해서 첫번째 값을 가져옴
                int sub = ((udcTableForm)(this.btnSort.BindingForm)).GetCheckedValue(2);

                ////by John (한줄짜리)
                ////1.Griid 합계 표시
                int[] rowType = spdData.RPT_DataBindingWithSubTotal(dt, 0, sub + 1, 15, null, null, btnSort);

                ////2. 칼럼 고정(필요하다면..)
                //spdData.Sheets[0].FrozenColumnCount = 9;

                ////3. Total부분 셀머지
                spdData.RPT_FillDataSelectiveCells("Total", 0, 15, 0, 1, true, Align.Center, VerticalAlign.Center);

                //4. Column Auto Fit
                spdData.RPT_AutoFit(false);

                dt.Dispose();

                txtLotqty.Text = Convert.ToString(spdData.ActiveSheet.Rows.Count - 1);

                if (cdvArea.Text == "HMK2")
                {
                    txtTotqty.Text = spdData.ActiveSheet.Cells[0, 17].Value.ToString();
                }
                else
                {
                    txtTotqty.Text = spdData.ActiveSheet.Cells[0, 16].Value.ToString();
                }
                        

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
            if (cdvArea.Text.Trim().Length == 0)
            {
                CmnFunction.ShowMsgBox(RptMessages.GetMessage("STD024", GlobalVariable.gcLanguage));
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
            //string QueryCond3 = null;
            //string sGroupBy = null;
            //string sHeader = null;

            udcTableForm tableForm = (udcTableForm)btnSort.BindingForm;

            QueryCond1 = tableForm.SelectedValueToQueryContainNull;
            QueryCond2 = tableForm.SelectedValue2ToQueryContainNull;


            strSqlString.AppendFormat("  SELECT {0}" + "\n", QueryCond2);
            strSqlString.Append("       , LOT_ID, MAT_ID, LOT_PRIORITY " + "\n");

            if (cdvArea.Text == "HMK2")
            {
                strSqlString.Append("       , LOT_CMF_5, QTY_2, START_QTY, QTY" + "\n");
            }
            else
            {
                strSqlString.Append("       , LOT_CMF_5, START_QTY, QTY" + "\n");
            }
                    
            
            strSqlString.Append("       , STOCK_IN_TIME" + "\n");
            strSqlString.Append("       , SHIP_SITE" + "\n");
            strSqlString.Append("       , FAB_SITE" + "\n");
            strSqlString.Append("       , MAX(CODE)" + "\n");
            strSqlString.Append("       , DELAYH" + "\n");
            strSqlString.Append("       , DELAYD" + "\n");
            strSqlString.Append("    FROM (" + "\n");

            strSqlString.AppendFormat(" SELECT {0} " + "\n", QueryCond1);
            strSqlString.AppendFormat("     , A.LOT_ID , A.MAT_ID , A.LOT_PRIORITY " + "\n");

            if (cdvArea.Text == "HMK2")
                strSqlString.AppendFormat("     , A.LOT_CMF_5 , A.QTY_2, A.CREATE_QTY_1 AS START_QTY, A.QTY_1 AS QTY " + "\n");
            else
                strSqlString.AppendFormat("     , A.LOT_CMF_5 , A.CREATE_QTY_1 AS START_QTY, A.QTY_1 AS QTY " + "\n");

            strSqlString.AppendFormat("     , TO_DATE(A.OPER_IN_TIME,'YYYY-MM-DD HH24MISS') AS STOCK_IN_TIME " + "\n");
            strSqlString.AppendFormat("     , A.LOT_CMF_11 AS SHIP_SITE, A.LOT_CMF_6 AS FAB_SITE " + "\n");            
            strSqlString.Append("           , ATT.ATTR_VALUE AS CODE " + "\n");            
            strSqlString.AppendFormat("     , TRUNC(to_char(sysdate - to_date(A.OPER_IN_TIME,'YYYY-MM-DD HH24MISS'))*24,2) AS DELAYH " + "\n");
            strSqlString.AppendFormat("     , TRUNC(to_char(sysdate - to_date(A.OPER_IN_TIME,'YYYY-MM-DD HH24MISS')),2) AS DELAYD " + "\n");
            strSqlString.AppendFormat(" FROM RWIPLOTSTS A, MWIPMATDEF B, MWIPOPRDEF C, MATRNAMSTS ATT " + "\n");
            strSqlString.AppendFormat(" WHERE 1=1 " + "\n");
            strSqlString.Append(" AND A.FACTORY = B.FACTORY(+) " + "\n");
            strSqlString.Append(" AND A.FACTORY = C.FACTORY (+)" + "\n");
            strSqlString.Append(" AND A.MAT_ID = B.MAT_ID(+) " + "\n");
            strSqlString.Append(" AND A.OPER = C.OPER(+) " + "\n");
            strSqlString.Append(" AND A.LOT_ID = ATT.ATTR_KEY(+)" + "\n");
            strSqlString.Append(" AND A.FACTORY = ATT.FACTORY(+)" + "\n");
            strSqlString.Append(" AND A.MAT_VER = 1 " + "\n");
            strSqlString.Append(" AND A.LOT_DEL_FLAG = ' ' " + "\n");
            strSqlString.Append(" AND A.OWNER_CODE = 'PROD'" + "\n");
            strSqlString.Append(" AND B.MAT_VER(+) = 1 " + "\n");
            strSqlString.AppendFormat(" AND C.AREA_ID = '{0}' " + "\n", CmnFunction.Trim(cdvArea.Text));
            strSqlString.Append(" AND ATT.ATTR_TYPE(+) = 'LOT_SEC_INFO' " + "\n");
            strSqlString.Append(" AND ATT.ATTR_NAME(+) = 'NCFCODE' " + "\n");

            // Product
            if (txtProduct.Text.Trim() != "%" && txtProduct.Text.Trim() != "")
                strSqlString.AppendFormat(" AND B.MAT_ID LIKE '{0}'" + "\n", txtProduct.Text);

            // Lot Type
            if (cdvType.Text != "ALL" && cdvType.Text.Trim() != "")
                strSqlString.Append(" AND A.LOT_CMF_5 " + cdvType.SelectedValueToQueryString + "  \n");

            // Lot ID
            if (txtLotID.Text.Trim() != "")
                strSqlString.AppendFormat(" AND A.LOT_ID = '" + txtLotID.Text + "' " + "\n");

            strSqlString.AppendFormat(" AND A.OWNER_CODE = 'PROD'" + "\n");
            if (cboDelayDay.Text.ToString().Trim().Length > 0)
            {
                strSqlString.AppendFormat(" AND TRUNC(to_char(sysdate - to_date(A.OPER_IN_TIME,'YYYY-MM-DD HH24MISS')),1) > " + cboDelayDay.Text.ToString().Trim() + "\n");
            }

            #region 상세 조회에 따른 SQL문 생성
            if (udcWIPCondition1.Text != "ALL" && udcWIPCondition1.Text != "")
                strSqlString.AppendFormat("        AND B.MAT_GRP_1 {0} " + "\n", udcWIPCondition1.SelectedValueToQueryString);

            if (udcWIPCondition2.Text != "ALL" && udcWIPCondition2.Text != "")
                strSqlString.AppendFormat("        AND B.MAT_GRP_2 {0} " + "\n", udcWIPCondition2.SelectedValueToQueryString);

            if (udcWIPCondition3.Text != "ALL" && udcWIPCondition3.Text != "")
                strSqlString.AppendFormat("        AND B.MAT_GRP_3 {0} " + "\n", udcWIPCondition3.SelectedValueToQueryString);

            if (udcWIPCondition4.Text != "ALL" && udcWIPCondition4.Text != "")
                strSqlString.AppendFormat("        AND B.MAT_GRP_4 {0} " + "\n", udcWIPCondition4.SelectedValueToQueryString);

            if (udcWIPCondition5.Text != "ALL" && udcWIPCondition5.Text != "")
                strSqlString.AppendFormat("        AND B.MAT_GRP_5 {0} " + "\n", udcWIPCondition5.SelectedValueToQueryString);

            if (udcWIPCondition6.Text != "ALL" && udcWIPCondition6.Text != "")
                strSqlString.AppendFormat("        AND B.MAT_GRP_6 {0} " + "\n", udcWIPCondition6.SelectedValueToQueryString);

            if (udcWIPCondition7.Text != "ALL" && udcWIPCondition7.Text != "")
                strSqlString.AppendFormat("        AND B.MAT_GRP_7 {0} " + "\n", udcWIPCondition7.SelectedValueToQueryString);

            if (udcWIPCondition8.Text != "ALL" && udcWIPCondition8.Text != "")
                strSqlString.AppendFormat("        AND B.MAT_GRP_8 {0} " + "\n", udcWIPCondition8.SelectedValueToQueryString);

            if (udcWIPCondition9.Text != "ALL" && udcWIPCondition9.Text != "")
                strSqlString.AppendFormat("        AND B.MAT_GRP_9 {0} " + "\n", udcWIPCondition9.SelectedValueToQueryString);

            strSqlString.Append("         )" + "\n");
            strSqlString.AppendFormat("GROUP BY {0}" + "\n", QueryCond2);
            strSqlString.Append("       , LOT_ID, MAT_ID, LOT_PRIORITY " + "\n");

            if (cdvArea.Text == "HMK2")
            {
                strSqlString.Append("       , LOT_CMF_5, QTY_2, START_QTY, QTY" + "\n");
            }
            else
            {
                strSqlString.Append("       , LOT_CMF_5, START_QTY, QTY" + "\n");
            }

            strSqlString.Append("       , STOCK_IN_TIME" + "\n");
            strSqlString.Append("       , SHIP_SITE" + "\n");
            strSqlString.Append("       , FAB_SITE" + "\n");            
            strSqlString.Append("       , DELAYH" + "\n");
            strSqlString.Append("       , DELAYD" + "\n");

            strSqlString.AppendFormat("ORDER BY {0}" + "\n", QueryCond2);
            strSqlString.Append("       , LOT_ID, MAT_ID, LOT_PRIORITY, LOT_CMF_5" + "\n");
            

            #endregion            

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

    }
}

