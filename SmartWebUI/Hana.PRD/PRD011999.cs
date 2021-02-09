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
    public partial class PRD011999 : Miracom.SmartWeb.UI.Controls.udcCUSReport002
    {
        /// <summary>
        /// 클  래  스: PRD011999<br/>
        /// 클래스요약: 공정별 재공 조회<br/>
        /// 작  성  자: 미라콤 양형석<br/>
        /// 최초작성일: 2008-12-10<br/>
        /// 상세  설명: 공정별 재공 조회<br/>
        /// 변경  내용: <br/>
        /// 변  경  자: 하나마이크론 김준용<br /> 
        /// </summary>
        public PRD011999()
        {
            InitializeComponent();

            SortInit();
            GridColumnInit(); //헤더 한줄짜리

            cdvDate.Value = DateTime.Today;
            cboTimeBase.SelectedIndex = 1;
        }

        #region SortInit

        private void SortInit()
        {
            //((udcTableForm)(this.btnSort.BindingForm)).AddRow("Step", "LOT.OPER AS STEP", "LOT.OPER", true);
            //((udcTableForm)(this.btnSort.BindingForm)).AddRow("Customer", "(SELECT DATA_1 FROM MGCMTBLDAT WHERE TABLE_NAME = 'H_CUSTOMER' AND KEY_1 = MAT.MAT_GRP_1 AND ROWNUM=1) AS CUSTOMER", "MAT.MAT_GRP_1", true);
            //((udcTableForm)(this.btnSort.BindingForm)).AddRow("Family", "MAT.MAT_GRP_2 AS FAMILY", "MAT.MAT_GRP_2", false);
            //((udcTableForm)(this.btnSort.BindingForm)).AddRow("Package", "MAT.MAT_GRP_3 AS PKG", "MAT.MAT_GRP_3", false);
            //((udcTableForm)(this.btnSort.BindingForm)).AddRow("Type1", "MAT.MAT_GRP_4 AS TYPE1", "MAT.MAT_GRP_4", false);
            //((udcTableForm)(this.btnSort.BindingForm)).AddRow("Type2", "MAT.MAT_GRP_5 AS TYPE2", "MAT.MAT_GRP_5", false);
            //((udcTableForm)(this.btnSort.BindingForm)).AddRow("LD Count", "MAT.MAT_GRP_6 AS LEAD", "MAT.MAT_GRP_6", false);
            //((udcTableForm)(this.btnSort.BindingForm)).AddRow("Density", "MAT.MAT_GRP_7 AS DENSITY", "MAT.MAT_GRP_7", false);
            //((udcTableForm)(this.btnSort.BindingForm)).AddRow("Generation", "MAT.MAT_GRP_8 AS GEN", "MAT.MAT_GRP_8", false);
            //((udcTableForm)(this.btnSort.BindingForm)).AddRow("PIN_TYPE", "MAT.MAT_CMF_10 AS PIN_TYPE", "MAT.MAT_CMF_10", true);
            //((udcTableForm)(this.btnSort.BindingForm)).AddRow("Product", "MAT.MAT_ID", "MAT.MAT_ID", true);
            //((udcTableForm)(this.btnSort.BindingForm)).AddRow("CUST_DEVICE", "MAT.MAT_CMF_7 AS CUST_DEVICE", "MAT.MAT_CMF_7", false);
            //((udcTableForm)(this.btnSort.BindingForm)).AddRow("Run ID", "LOT.LOT_CMF_4", "LOT.LOT_CMF_4", true);
            //((udcTableForm)(this.btnSort.BindingForm)).AddRow("Lot ID", "LOT.LOT_ID", "LOT.LOT_ID", true);
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

            spdData.RPT_AddBasicColumn("MAT_ID", 0, 0, Visibles.True, Frozen.False, Align.Center, Merge.True, Formatter.String, 150);
            spdData.RPT_AddBasicColumn("LOT_ID", 0, 1, Visibles.True, Frozen.False, Align.Center, Merge.True, Formatter.String, 70);
            spdData.RPT_AddBasicColumn("OPER", 0, 2, Visibles.True, Frozen.False, Align.Center, Merge.True, Formatter.String, 50);
            spdData.RPT_AddBasicColumn("QTY_1", 0, 3, Visibles.True, Frozen.False, Align.Center, Merge.True, Formatter.Number, 50);
            spdData.RPT_AddBasicColumn("LOT_STATUS", 0, 4, Visibles.True, Frozen.False, Align.Center, Merge.True, Formatter.String, 70);
            spdData.RPT_AddBasicColumn("HOLD_FLAG", 0, 5, Visibles.True, Frozen.False, Align.Center, Merge.True, Formatter.String, 70);
            spdData.RPT_AddBasicColumn("HOLD_CODE", 0, 6, Visibles.True, Frozen.False, Align.Center, Merge.True, Formatter.String, 70); 
           
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
                //int sub = ((udcTableForm)(this.btnSort.BindingForm)).GetCheckedValue(2);

                ////by John (한줄짜리)
                ////1.Griid 합계 표시
                //int[] rowType = spdData.RPT_DataBindingWithSubTotal(dt, 0, sub+1, 14, null, null, btnSort);

                ////2. 칼럼 고정(필요하다면..)
                //spdData.Sheets[0].FrozenColumnCount = 9;

                ////3. Total부분 셀머지
                //spdData.RPT_FillDataSelectiveCells("Total", 0, 14, 0, 1, true, Align.Center, VerticalAlign.Center);

                //4. Column Auto Fit
                //spdData.RPT_AutoFit(false);

                spdData.DataSource = dt;

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

            //if (cdvOper.txtFromValue.Trim().Length == 0 || cdvOper.txtToValue.Trim().Length == 0)
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

            //string QueryCond1 = null;
            //string QueryCond2 = null;

            //udcTableForm tableForm = (udcTableForm)btnSort.BindingForm;

            //QueryCond1 = tableForm.SelectedValueToQueryContainNull;
            //QueryCond2 = tableForm.SelectedValue2ToQueryContainNull;

            //string strDate = cdvDate.Value.ToString("yyyyMMdd");
            //bool isRealTime = false;

            //if (strDate.Equals(DateTime.Now.ToString("yyyyMMdd")))
            //{
            //    strDate = DateTime.Now.ToString("yyyyMMddHHmmss");
            //    isRealTime = true;
            //}
            //else
            //{
            //    if (cboTimeBase.Text == "06시")
            //        strDate = strDate + "060000";
            //    else
            //        strDate = strDate + "220000";

            //    isRealTime = false;
            //}

            //strSqlString.AppendFormat("SELECT {0}  " + "\n", QueryCond1);
            strSqlString.Append("SELECT MAT_ID, LOT_ID, OPER, QTY_1, LOT_STATUS, HOLD_FLAG, HOLD_CODE " + "\n");
            strSqlString.Append("  FROM RWIPLOTSTS " + "\n");
            strSqlString.Append(" WHERE 1=1 " + "\n");
            strSqlString.Append("   AND FACTORY = '" + GlobalVariable.gsAssyDefaultFactory + "' " + "\n");
            strSqlString.Append("   AND LOT_DEL_FLAG = ' '   " + "\n");
            strSqlString.Append("   AND LOT_TYPE = 'W' " + "\n");
            strSqlString.Append("   AND OPER = 'A0400' " + "\n");
            strSqlString.Append(" ORDER BY MAT_ID, LOT_ID, OPER, LOT_STATUS " + "\n");
            
            if (GlobalVariable.gsUserGroup == "ADMIN_GROUP" || GlobalVariable.gsUserGroup == "HANA_ADMIN_GROUP")
            {
                System.Windows.Forms.Clipboard.SetText(strSqlString.ToString());
            }

            return strSqlString.ToString();
        }
        #endregion

        #endregion

        #region Event

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
            
            //cdvLotType.sFactory = cdvFactory.txtValue;
            cdvOper.sFactory = cdvFactory.txtValue;
        }
    }
}

