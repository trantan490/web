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
    public partial class PRD010901 : Miracom.SmartWeb.UI.Controls.udcCUSReport002
    {
        /// <summary>
        /// 클  래  스: PRD010901<br/>
        /// 클래스요약: IML FGS Shipment SBL DATA<br/>
        /// 작  성  자: 하나마이크론 임종우<br/>
        /// 최초작성일: 2010-02-12<br/>
        /// 상세  설명: IML FGS Shipment 기준의 TIN / TOUT 조회 Merge 이력 감안 안되어 있음..<br/>
        /// 변경  사항: 
        /// 2011-07-27-김민우: 공정 별 END 시점을 기준으로 검색 기능 추가
        ///                    TEST DATE 기준변경 (IN_TIME에서 OUT_TIME으로)
        ///                    설비번호, PGM 정보 추가
        /// 2012-02-13-임종우: 속도 개선을 위한 쿼리 튜닝 작업
        


        /// </summary>
        public PRD010901()
        {
            InitializeComponent();
            this.cdvOper.sFactory = GlobalVariable.gsTestDefaultFactory;
            OptionInIt(); // 초기화
            SortInit();
            GridColumnInit(); //헤더 한줄짜리 
        }

        /// <summary>
        /// 
        /// </summary>
        private void OptionInIt()
        {
            udcDate.AutoBinding(DateTime.Now.AddDays(-1).ToString(), DateTime.Now.ToString());
            udcDate.DaySelector.SelectedValue = "DAY";
            udcDate.DaySelector.Visible = false;
        }

        #region SortInit

        /// <summary>
        /// SortInit
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SortInit()
        {
            ((udcTableForm)(this.btnSort.BindingForm)).AddRow("Customer", "(SELECT DATA_1 FROM MGCMTBLDAT WHERE TABLE_NAME = 'H_CUSTOMER' AND KEY_1 = MAT_GRP_1 AND ROWNUM=1) AS MAT_GRP_1", "MAT_GRP_1", true);
            ((udcTableForm)(this.btnSort.BindingForm)).AddRow("Device", "MAT_CMF_7", "MAT_CMF_7", true);
            ((udcTableForm)(this.btnSort.BindingForm)).AddRow("Test Date", "TO_CHAR(TO_DATE(T_IN_TIME, 'YYYYMMDDHH24MISS') + 2/24, 'YYYYMMDD') AS TEST_DATE", "T_IN_TIME", true);
            ((udcTableForm)(this.btnSort.BindingForm)).AddRow("END RES ID", "END_RES_ID", "END_RES_ID", true);
            ((udcTableForm)(this.btnSort.BindingForm)).AddRow("PGM", "PGM", "PGM", true);
            ((udcTableForm)(this.btnSort.BindingForm)).AddRow("Lot No", "LOT_ID", "LOT_ID", true);
            ((udcTableForm)(this.btnSort.BindingForm)).AddRow("Fab Lot No", "LOT_CMF_13", "LOT_CMF_13", true);
            ((udcTableForm)(this.btnSort.BindingForm)).AddRow("Lot Size", "T_IN", "T_IN", true);
            ((udcTableForm)(this.btnSort.BindingForm)).AddRow("GOOD", "T_OUT", "T_OUT", true);          
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
            spdData.RPT_AddBasicColumn("Customer", 0, 0, Visibles.True, Frozen.True, Align.Center, Merge.False, Formatter.String, 70);
            spdData.RPT_AddBasicColumn("Device", 0, 1, Visibles.True, Frozen.True, Align.Center, Merge.False, Formatter.String, 70);
            spdData.RPT_AddBasicColumn("Test Date", 0, 2, Visibles.True, Frozen.True, Align.Center, Merge.False, Formatter.String, 70);
            spdData.RPT_AddBasicColumn("END RES ID", 0, 3, Visibles.True, Frozen.True, Align.Center, Merge.False, Formatter.String, 70);
            spdData.RPT_AddBasicColumn("PGM", 0, 4, Visibles.True, Frozen.True, Align.Center, Merge.False, Formatter.String, 70);
            spdData.RPT_AddBasicColumn("Lot No", 0, 5, Visibles.True, Frozen.True, Align.Center, Merge.False, Formatter.String, 70);
            spdData.RPT_AddBasicColumn("Fab Lot No", 0, 6, Visibles.True, Frozen.False, Align.Center, Merge.False, Formatter.String, 70);
            spdData.RPT_AddBasicColumn("Lot Size", 0, 7, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 70);
            spdData.RPT_AddBasicColumn("GOOD", 0, 8, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 70);

            for (int i = 1; i <= 32; i++)
            {
                spdData.RPT_AddBasicColumn("BIN" + i, 0, 8 + i, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Double2, 70);
            }
            

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
            //if (!CheckField()) return;

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
                //int sub = ((udcTableForm)(this.btnSort.BindingForm)).GetCheckedValue(2);

                ////by John (한줄짜리)
                ////1.Griid 합계 표시
                //int[] rowType = spdData.RPT_DataBindingWithSubTotal(dt, 0, sub + 1, 9, null, null, btnSort);

                ////2. 칼럼 고정(필요하다면..)
                //spdData.Sheets[0].FrozenColumnCount = 10;

                ////3. Total부분 셀머지
                //spdData.RPT_FillDataSelectiveCells("Total", 0, 9, 0, 1, true, Align.Center, VerticalAlign.Center);

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
        //private Boolean CheckField()
        //{
        //if (cdvFactory.Text.TrimEnd() == "")
        //{
        //    CmnFunction.ShowMsgBox(RptMessages.GetMessage("STD001", GlobalVariable.gcLanguage));
        //    return false;
        //}

        //return true;
        //}

        #endregion

        #region MakeSqlString
        private string MakeSqlString()
        {
            string QueryCond1 = null;
            string QueryCond2 = null;

            String strStartDate = udcDate.Start_Tran_Time;
            String strEndDate = udcDate.End_Tran_Time;

            udcTableForm tableForm = (udcTableForm)btnSort.BindingForm;
            
            QueryCond1 = tableForm.SelectedValueToQueryContainNull;
            QueryCond2 = tableForm.SelectedValue2ToQueryContainNull;
            
            StringBuilder strSqlString = new StringBuilder();
            strSqlString.AppendFormat("SELECT {0}, ROUND((T_OUT/T_IN)*100,2) AS BIN1 " + "\n", QueryCond1);

            for (int y = 2; y <= 32; y++)
            {
                strSqlString.Append("     , ' ' AS BIN" + y + "\n");                
            }

            strSqlString.AppendFormat("  FROM (  " + "\n");
            strSqlString.AppendFormat("        SELECT MAT.MAT_GRP_1" + "\n");
            strSqlString.AppendFormat("             , MAT.MAT_CMF_7" + "\n");
            strSqlString.AppendFormat("             , LOT_ID " + "\n");
            strSqlString.AppendFormat("             , LOT_CMF_13" + "\n");
            strSqlString.AppendFormat("             , END_RES_ID " + "\n");
            strSqlString.AppendFormat("             , TRAN_TIME AS T_IN_TIME " + "\n");
            strSqlString.AppendFormat("             , TRAN_CMF_3 AS PGM " + "\n");
            strSqlString.AppendFormat("             , START_QTY_1 AS T_IN " + "\n");
            strSqlString.AppendFormat("             , QTY_1 AS T_OUT " + "\n");
            strSqlString.AppendFormat("          FROM CWIPLOTEND HIS" + "\n");
            strSqlString.AppendFormat("             , MWIPMATDEF MAT" + "\n");
            strSqlString.AppendFormat("         WHERE 1=1" + "\n");
            strSqlString.AppendFormat("           AND HIS.FACTORY = MAT.FACTORY" + "\n");
            strSqlString.AppendFormat("           AND HIS.MAT_ID = MAT.MAT_ID " + "\n");
            strSqlString.AppendFormat("           AND HIS.TRAN_TIME  BETWEEN '" + strStartDate + "' AND '" + strEndDate + "'" + "\n");
            strSqlString.AppendFormat("           AND HIS.FACTORY = '" + GlobalVariable.gsTestDefaultFactory + "'" + "\n");
            strSqlString.AppendFormat("           AND HIS.MAT_ID LIKE 'IM%' " + "\n");
            strSqlString.AppendFormat("           AND HIS.OLD_OPER='" + cdvOper.Text + "'" + "\n");
            strSqlString.AppendFormat("           AND HIS.HIST_DEL_FLAG = ' '" + "\n");
            strSqlString.AppendFormat("           AND HIS.LOT_TYPE = 'W' " + "\n");
            strSqlString.AppendFormat("           AND MAT.MAT_TYPE = 'FG' " + "\n");
            strSqlString.AppendFormat("       )" + "\n");
            strSqlString.AppendFormat(" WHERE T_IN IS NOT NULL" + "\n");
            strSqlString.AppendFormat(" ORDER BY {0} " + "\n", QueryCond2);

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
            // ExcelHelper.Instance.subMakeExcel(spdData, this.lblTitle.Text, " ^ ", " ^ ", true);

            //저장
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

        private void txtProduct_KeyDown(object sender, KeyEventArgs e)
        {
            //if (e.KeyCode == Keys.Enter)
            //{
            //    btnView_Click(sender, e);
            //}
        }

        private void PRD010901_Load_1(object sender, EventArgs e)
        {
            //txtProduct.Focus();
        }

        private void cdvFactory_ValueSelectedItemChanged(object sender, MCCodeViewSelChanged_EventArgs e)
        {
            //this.SetFactory(cdvFactory.txtValue);
            //cdvLotType.sFactory = cdvFactory.txtValue;
        }
    }
}

