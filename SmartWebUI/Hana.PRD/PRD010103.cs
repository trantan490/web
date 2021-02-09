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
    public partial class PRD010103 : Miracom.SmartWeb.UI.Controls.udcCUSReport002
    {
        /// <summary>
        /// 변경  내용: <br/>     
        /// 2014-12-22-임종우 : GCT_DEVICE -> SALES CODE, CUSTOMER_DEVICE -> CUST DEVICE 변경 (박우순 요청)
        /// 2018-01-12-임종우 : 변경된 이력에 대해 색상으로 표시 (정비재수석 요청)
        ///                   : 셀 머지 부분 삭제 (백성호D 요청)
        /// </summary>
        #region " PRD010103 : Program Initial "

        public PRD010103()
        {
            InitializeComponent();
            fnSSInitial(SS01);
            fnSSSortInit();
        }

        #endregion


        #region " Common Function "

        private void fnSSInitial(Miracom.SmartWeb.UI.Controls.udcFarPoint SS)
        {
            /****************************************************
             * comment : ss의 header를 설정한다.
             * 
             * created by : bee-jae jung(2010-07-20-화요일)
             * 
             * modified by : bee-jae jung(2010-07-20-화요일)
             ****************************************************/
            int iindex = 0;
            try
            {
                Cursor.Current = Cursors.WaitCursor;

                SS.RPT_ColumnInit();
                // CUSTOMER
                if (udcWIPCondition1.Text != "ALL" && udcWIPCondition1.Text != "")
                {
                    iindex = 0;
                    SS.RPT_AddBasicColumn("CUSTOMER", 0, iindex, Visibles.True, Frozen.False, Align.Center, Merge.False, Formatter.String, 40);
                }
                // FAMILY
                if (udcWIPCondition2.Text != "ALL" && udcWIPCondition2.Text != "")
                {
                    iindex++;
                    SS.RPT_AddBasicColumn("FAMILY", 0, iindex, Visibles.True, Frozen.False, Align.Center, Merge.False, Formatter.String, 40);
                }
                // PACKAGE
                if (udcWIPCondition3.Text != "ALL" && udcWIPCondition3.Text != "")
                {
                    iindex++;
                    SS.RPT_AddBasicColumn("PACKAGE", 0, iindex, Visibles.True, Frozen.False, Align.Center, Merge.False, Formatter.String, 40);
                }
                // TYPE1
                if (udcWIPCondition4.Text != "ALL" && udcWIPCondition4.Text != "")
                {
                    iindex++;
                    SS.RPT_AddBasicColumn("TYPE1", 0, iindex, Visibles.True, Frozen.False, Align.Center, Merge.False, Formatter.String, 40);
                }
                // TYPE2
                if (udcWIPCondition5.Text != "ALL" && udcWIPCondition5.Text != "")
                {
                    iindex++;
                    SS.RPT_AddBasicColumn("TYPE2", 0, iindex, Visibles.True, Frozen.False, Align.Center, Merge.False, Formatter.String, 40);
                }
                // LEAD COUNT
                if (udcWIPCondition6.Text != "ALL" && udcWIPCondition6.Text != "")
                {
                    iindex++;
                    SS.RPT_AddBasicColumn("LEAD_COUNT", 0, iindex, Visibles.True, Frozen.False, Align.Center, Merge.False, Formatter.String, 40);
                }
                // DENSITY
                if (udcWIPCondition7.Text != "ALL" && udcWIPCondition7.Text != "")
                {
                    iindex++;
                    SS.RPT_AddBasicColumn("DENSITY", 0, iindex, Visibles.True, Frozen.False, Align.Center, Merge.False, Formatter.String, 40);
                }
                // GENERATION
                if (udcWIPCondition8.Text != "ALL" && udcWIPCondition8.Text != "")
                {
                    iindex++;
                    SS.RPT_AddBasicColumn("GENERATION", 0, iindex, Visibles.True, Frozen.False, Align.Center, Merge.False, Formatter.String, 40);
                }

                // 2010-07-20-정비재 : sheet의 headeer를 설정한다.
                SS.RPT_AddBasicColumn("GCT_PROJECT_NO", 0, iindex++, Visibles.True, Frozen.False, Align.Center, Merge.False, Formatter.String, 120);
                SS.RPT_AddBasicColumn("GCT_DEVICE", 0, iindex++, Visibles.True, Frozen.False, Align.Center, Merge.False, Formatter.String, 120);
                SS.RPT_AddBasicColumn("PACKAGE", 0, iindex++, Visibles.True, Frozen.False, Align.Center, Merge.False, Formatter.String, 100);
                SS.RPT_AddBasicColumn("LD_COUNT", 0, iindex++, Visibles.True, Frozen.False, Align.Center, Merge.False, Formatter.String, 100);
                SS.RPT_AddBasicColumn("DIMENSION", 0, iindex++, Visibles.True, Frozen.False, Align.Center, Merge.False, Formatter.String, 100);
                SS.RPT_AddBasicColumn("CUSTOMER_DEVICE", 0, iindex++, Visibles.True, Frozen.False, Align.Center, Merge.False, Formatter.String, 120);
                SS.RPT_AddBasicColumn("WFR_LOT_ID", 0, iindex++, Visibles.True, Frozen.False, Align.Center, Merge.False, Formatter.String, 100);
                SS.RPT_AddBasicColumn("GCT_ASSY_LOT_ID", 0, iindex++, Visibles.True, Frozen.False, Align.Center, Merge.False, Formatter.String, 120);
                SS.RPT_AddBasicColumn("GCT_TEST_LOT_ID", 0, iindex++, Visibles.True, Frozen.False, Align.Center, Merge.False, Formatter.String, 120);
                SS.RPT_AddBasicColumn("VENDOR_LOT_ID", 0, iindex++, Visibles.True, Frozen.False, Align.Center, Merge.False, Formatter.String, 120);
                SS.RPT_AddBasicColumn("PROD_RLSESCHED_NO", 0, iindex++, Visibles.True, Frozen.False, Align.Center, Merge.False, Formatter.String, 120);
                SS.RPT_AddBasicColumn("VENDOR_ORDER_NO", 0, iindex++, Visibles.True, Frozen.False, Align.Center, Merge.False, Formatter.String, 120);
                SS.RPT_AddBasicColumn("TEST_RCVD_QTY", 0, iindex++, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 120);
                SS.RPT_AddBasicColumn("TEST_RCVD_DATE", 0, iindex++, Visibles.True, Frozen.False, Align.Center, Merge.False, Formatter.String, 120);
                SS.RPT_AddBasicColumn("TEST_QUE", 0, iindex++, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 120);
                SS.RPT_AddBasicColumn("TEST_START_DATE", 0, iindex++, Visibles.True, Frozen.False, Align.Center, Merge.False, Formatter.String, 120);
                SS.RPT_AddBasicColumn("FINAL_TEST_CAS_OS", 0, iindex++, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 120);
                SS.RPT_AddBasicColumn("QA_EL_TEST", 0, iindex++, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 120);
                SS.RPT_AddBasicColumn("TEST_HOLD", 0, iindex++, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 120);
                SS.RPT_AddBasicColumn("LOT_HOLD_DATE", 0, iindex++, Visibles.True, Frozen.False, Align.Center, Merge.False, Formatter.String, 120);
                SS.RPT_AddBasicColumn("LOT_HOLD_REL_DATE", 0, iindex++, Visibles.True, Frozen.False, Align.Center, Merge.False, Formatter.String, 120);
                SS.RPT_AddBasicColumn("TEST_OUT_QTY", 0, iindex++, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 120);
                SS.RPT_AddBasicColumn("TEST_OUT_FCST_DATE", 0, iindex++, Visibles.True, Frozen.False, Align.Center, Merge.False, Formatter.String, 120);
                SS.RPT_AddBasicColumn("TEST_OUT_DATE", 0, iindex++, Visibles.True, Frozen.False, Align.Center, Merge.False, Formatter.String, 120);
                SS.RPT_AddBasicColumn("VM", 0, iindex++, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 120);
                SS.RPT_AddBasicColumn("BAKE", 0, iindex++, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 120);
                SS.RPT_AddBasicColumn("TNR", 0, iindex++, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 120);
                SS.RPT_AddBasicColumn("PACK", 0, iindex++, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 120);
                SS.RPT_AddBasicColumn("PACK_OUT_QTY", 0, iindex++, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 120);
                SS.RPT_AddBasicColumn("PACK_OUT_DATE", 0, iindex++, Visibles.True, Frozen.False, Align.Center, Merge.False, Formatter.String, 120);
                SS.RPT_AddBasicColumn("FGS", 0, iindex++, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 120);
                SS.RPT_AddBasicColumn("SHIP_OUT_QTY", 0, iindex++, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 120);
                SS.RPT_AddBasicColumn("FCST_SHIP_DATE", 0, iindex++, Visibles.True, Frozen.False, Align.Center, Merge.False, Formatter.String, 120);
                SS.RPT_AddBasicColumn("ACT_SHIP_DATE", 0, iindex++, Visibles.True, Frozen.False, Align.Center, Merge.False, Formatter.String, 120);
                SS.RPT_AddBasicColumn("CUST_REQ_DATE", 0, iindex++, Visibles.True, Frozen.False, Align.Center, Merge.False, Formatter.String, 120);
                SS.RPT_AddBasicColumn("PR_NO", 0, iindex++, Visibles.True, Frozen.False, Align.Center, Merge.False, Formatter.String, 120);
                SS.RPT_AddBasicColumn("TEST_PO", 0, iindex++, Visibles.True, Frozen.False, Align.Center, Merge.False, Formatter.String, 120);
                SS.RPT_AddBasicColumn("HOLD_CODE", 0, iindex++, Visibles.True, Frozen.False, Align.Center, Merge.False, Formatter.String, 120);
                SS.RPT_AddBasicColumn("REJECT", 0, iindex++, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 120);
                SS.RPT_AddBasicColumn("REMARK", 0, iindex++, Visibles.True, Frozen.False, Align.Center, Merge.False, Formatter.String, 120);
                SS.RPT_AddBasicColumn("UPDATE_TIME", 0, iindex++, Visibles.False, Frozen.False, Align.Center, Merge.False, Formatter.String, 120);
            }
            catch (Exception ex)
            {
                CmnFunction.ShowMsgBox(ex.Message);
                return;
            }
            finally
            {
                Cursor.Current = Cursors.Default;
            }
        }

        private void fnSSSortInit()
        {
            /****************************************************
             * comment : ss의 데이터의 정렬규칙을 설정하다.
             * 
             * created by : bee-jae jung(2010-07-20-화요일)
             * 
             * modified by : bee-jae jung(2010-07-20-화요일)
             ****************************************************/
            try
            {
                Cursor.Current = Cursors.WaitCursor;

                ((udcTableForm)(this.btnSort.BindingForm)).Clear();
                // CUSTOMER
                if (udcWIPCondition1.Text != "ALL" && udcWIPCondition1.Text != "")
                {
                    ((udcTableForm)(this.btnSort.BindingForm)).AddRow("CUSTOMER", "B.MAT_GRP_1", "MAT_GRP_1", "NVL((SELECT DATA_1 FROM MGCMTBLDAT WHERE TABLE_NAME = 'H_CUSTOMER' AND KEY_1 = MAT_GRP_1 AND ROWNUM=1), '-') AS CUSTOMER", true);
                }
                // FAMILY
                if (udcWIPCondition2.Text != "ALL" && udcWIPCondition2.Text != "")
                {
                    ((udcTableForm)(this.btnSort.BindingForm)).AddRow("FAMILY", "B.MAT_GRP_2", "MAT_GRP_2", "B.MAT_GRP_2", true);
                }
                // PACKAGE
                if (udcWIPCondition3.Text != "ALL" && udcWIPCondition3.Text != "")
                {
                    ((udcTableForm)(this.btnSort.BindingForm)).AddRow("PACKAGE", "B.MAT_GRP_3", "MAT_GRP_3", "B.MAT_GRP_3", true);
                }
                // TYPE1
                if (udcWIPCondition4.Text != "ALL" && udcWIPCondition4.Text != "")
                {
                    ((udcTableForm)(this.btnSort.BindingForm)).AddRow("TYPE1", "B.MAT_GRP_4", "MAT_GRP_4", "B.MAT_GRP_4", true);
                }
                // TYPE2
                if (udcWIPCondition5.Text != "ALL" && udcWIPCondition5.Text != "")
                {
                    ((udcTableForm)(this.btnSort.BindingForm)).AddRow("TYPE2", "B.MAT_GRP_5", "MAT_GRP_5", "B.MAT_GRP_5", true);
                }
                // LEAD COUNT
                if (udcWIPCondition6.Text != "ALL" && udcWIPCondition6.Text != "")
                {
                    ((udcTableForm)(this.btnSort.BindingForm)).AddRow("LEAD_COUNT", "B.MAT_GRP_6", "MAT_GRP_6", "B.MAT_GRP_6", true);
                }
                // DENSITY
                if (udcWIPCondition7.Text != "ALL" && udcWIPCondition7.Text != "")
                {
                    ((udcTableForm)(this.btnSort.BindingForm)).AddRow("DENSITY", "B.MAT_GRP_7", "MAT_GRP_7", "B.MAT_GRP_7", true);
                }
                // GENERATION
                if (udcWIPCondition8.Text != "ALL" && udcWIPCondition8.Text != "")
                {
                    ((udcTableForm)(this.btnSort.BindingForm)).AddRow("GENERATION", "B.MAT_GRP_8", "MAT_GRP_8", "B.MAT_GRP_8", true);
                }
                ((udcTableForm)(this.btnSort.BindingForm)).AddRow("PRODUCT", "A.MAT_ID", "MAT_ID", "A.MAT_ID", true);
                ((udcTableForm)(this.btnSort.BindingForm)).AddRow("RETURN_TYPE", "A.RETURN_TYPE", "RETURN_TYPE", "A.RETURN_TYPE", true);
            }
            catch (Exception ex)
            {
                CmnFunction.ShowMsgBox(ex.Message);
                return;
            }
            finally
            {
                Cursor.Current = Cursors.Default;
            }
        }

        private bool fnDataFind()
        {
            /****************************************************
             * comment : database에 저장된 데이터를 조회한다.
             * 
             * created by : bee-jae jung(2010-07-20-화요일)
             * 
             * modified by : bee-jae jung(2010-07-20-화요일)
             ****************************************************/
            DataTable dt = null;
            //String sPackage = "", sParameter = "";
            String sQuery = "";
            try
            {
                Cursor.Current = Cursors.WaitCursor;
                LoadingPopUp.LoadIngPopUpShow(this);

                // 2010-07-20-정비재 : Sheet / Listview를 초기화 한다.
                CmnInitFunction.ClearList(SS01, true);

                // 2011-07-12-정비재 : Queyr문의 실행시간이 오래 걸려서 1시간 Batch job으로 RWIPLOTGCT Table에 입력하고
                //                   : RWIPLOTGCT Table에 데이터를 검색한다.
                // 2011-07-12-정비재 : 기존 Source Backup
                /*
                // 2010-07-20-정비재 : gct wip report를 검색한다.
                sPackage = "PACKAGE_PRD010103.PROC_PRD010103";
                sParameter = "P_MAT_ID:" + txtMatID.Text
                        + "│ P_LOT_TYPE:" + txtLotType.Text;
                  
                 dt = CmnFunction.oComm.fnExecutePackage("DYNAMIC", sPackage, sParameter);
                */

                // 2017-07-03-mgkim : MES GCT WIP Manual 수정본과 JOIN하여 VIEW
                sQuery = "SELECT NVL(C.GCT_PROJECT_NO, A.GCT_PROJECT_NO) AS GCT_PROJECT_NO, B.MAT_CMF_8 AS GCT_DEVICE, NVL(C.PACKAGE, A.PACKAGE) AS PACKAGE, \n"
                       + "	     NVL(C.LD_COUNT, A.LD_COUNT) AS LD_COUNT, NVL(C.DIMENSION, A.DIMENSION) AS DIMENSION, B.MAT_CMF_7 AS VENDOR_PART_NO, \n"
                       + "       NVL(C.WFR_LOT_ID, A.WFR_LOT_ID) AS WFR_LOT_ID, NVL(C.GCT_ASSY_LOT_ID, A.GCT_ASSY_LOT_ID) AS GCT_ASSY_LOT_ID, NVL(C.GCT_TEST_LOT_ID, A.GCT_TEST_LOT_ID) AS GCT_TEST_LOT_ID, \n"
                       + "       NVL(C.VENDOR_LOT_ID, A.VENDOR_LOT_ID) AS VENDOR_LOT_ID, NVL(C.PROD_RLSESCHED_NO, A.PROD_RLSESCHED_NO) AS PROD_RLSESCHED_NO,  NVL(C.VENDOR_ORDER_NO, A.VENDOR_ORDER_NO) AS VENDOR_ORDER_NO, \n"
                       + "       NVL(C.TEST_RCVD_QTY, A.TEST_RCVD_QTY) AS TEST_RCVD_QTY, NVL(C.TEST_RCVD_DATE, A.TEST_RCVD_DATE) AS TEST_RCVD_DATE, NVL(C.TEST_QUE, A.TEST_QUE) AS TEST_QUE,\n"
                       + "       NVL(C.TEST_START_DATE, A.TEST_START_DATE) AS TEST_START_DATE, NVL(C.FINAL_TEST_CAS_OS, A.FINAL_TEST_CAS_OS) AS FINAL_TEST_CAS_OS,  NVL(C.QA_EL_TEST, A.QA_EL_TEST) AS QA_EL_TEST, \n"
                       + "       NVL(C.TEST_HOLD, A.TEST_HOLD) AS TEST_HOLD, NVL(C.LOT_HOLD_DATE, A.LOT_HOLD_DATE) AS LOT_HOLD_DATE, NVL(C.LOT_HOLD_REL_DATE, A.LOT_HOLD_REL_DATE) AS LOT_HOLD_REL_DATE, \n"
                       + "       NVL(C.TEST_OUT_QTY, A.TEST_OUT_QTY) AS TEST_OUT_QTY, NVL(C.TEST_OUT_FCST_DATE, A.TEST_OUT_FCST_DATE) AS TEST_OUT_FCST_DATE, NVL(C.TEST_OUT_DATE, A.TEST_OUT_DATE) AS TEST_OUT_DATE, \n"
                       + "       NVL(C.VM, A.VM) AS VM, NVL(C.BAKE, A.BAKE) AS BAKE, NVL(C.TNR, A.TNR) AS TNR, \n"
                       + "       NVL(C.PACK, A.PACK) AS PACK,  NVL(C.PACK_OUT_QTY, A.PACK_OUT_QTY) AS PACK_OUT_QTY, NVL(C.PACK_OUT_DATE, A.PACK_OUT_DATE) AS PACK_OUT_DATE, \n"
                       + "       NVL(C.FGS_QTY, A.FGS_QTY) AS FGS_QTY, NVL(C.SHIP_OUT_QTY, A.SHIP_OUT_QTY) AS SHIP_OUT_QTY,  NVL(C.FCST_SHIP_DATE, A.FCST_SHIP_DATE) AS FCST_SHIP_DATE, \n"
                       + "       NVL(C.ACT_SHIP_TIME, A.ACT_SHIP_TIME) AS ACT_SHIP_TIME, NVL(C.CUST_REQ_DATE, A.CUST_REQ_DATE) AS CUST_REQ_DATE, NVL(C.PR_NO, A.PR_NO) AS PR_NO,\n"
                       + "       NVL(C.TEST_PO, A.TEST_PO) AS TEST_PO, NVL(C.HOLD_CODE, A.HOLD_CODE) AS HOLD_CODE, NVL(C.REJECT, A.REJECT) AS REJECT, \n"
                       + "       NVL(C.REMARK, A.REMARK) AS REMARK, \n"
                       + "       C.UPDATE_TIME \n"
                       + "  FROM RWIPLOTGCT A \n"
                       + "     , (SELECT DISTINCT MAT_ID AS MAT_ID \n"
     		 		   + "			   , MAT_CMF_7 AS MAT_CMF_7 \n"
             		   + "			   , MAT_CMF_8 AS MAT_CMF_8 \n"
		  			   + "			FROM MWIPMATDEF \n"
         			   + "		   WHERE MAT_GRP_1 = 'GC') B, \n"
                       + "		 CWIPLOTGCT@RPTTOMES C \n"
                       + " WHERE A.VENDOR_PART_NO = B.MAT_ID \n"
                       + " AND A.GCT_TEST_LOT_ID = C.GCT_TEST_LOT_ID(+) \n"
                       + " ORDER BY A.GCT_TEST_LOT_ID ASC";

                if (GlobalVariable.gsUserGroup == "ADMIN_GROUP" || GlobalVariable.gsUserGroup == "HANA_ADMIN_GROUP")
                {
                    System.Windows.Forms.Clipboard.SetText(sQuery);
                }

                dt = CmnFunction.oComm.GetFuncDataTable("DYNAMIC", sQuery.Replace((char)Keys.Tab, ' '));

                if (dt.Rows.Count <= 0)
                {
                    dt.Dispose();
                    LoadingPopUp.LoadingPopUpHidden();
                    CmnFunction.ShowMsgBox(RptMessages.GetMessage("STD010", GlobalVariable.gcLanguage));
                    return false;
                }

                int[] rowType = SS01.RPT_DataBindingWithSubTotal(dt, 0, 0, 1, null, null);

                int iColPos = 0;

                for (int iCol = 0; iCol < SS01.ActiveSheet.Columns.Count; iCol++)
                {
                    if (SS01.ActiveSheet.GetColumnLabel(0, iCol).ToString() == "UPDATE_TIME")
                    {
                        iColPos = iCol;
                        break;
                    }
                }

                for (int i = 0; i < SS01.ActiveSheet.RowCount; i++)
                {                    
                    if (SS01.ActiveSheet.Cells[i, iColPos].Value != null && SS01.ActiveSheet.Cells[i, iColPos].Value.ToString().Trim() != "")
                    {
                        SS01.ActiveSheet.Rows[i].BackColor = Color.Pink;                 
                    }                    
                }             

                return true;
            }
            catch (Exception ex)
            {
                LoadingPopUp.LoadingPopUpHidden();
                MessageBox.Show(ex.Message);
                return false;
            }
            finally
            {
                LoadingPopUp.LoadingPopUpHidden();
                Cursor.Current = Cursors.Default;
            }
        }

        #endregion


        #region " Form Event "

        private void btnView_Click(object sender, EventArgs e)
        {
            /****************************************************
             * Comment : View Button을 클릭하면
             * 
             * Created By : bee-jae jung(2010-05-11-화요일)
             * 
             * Modified By : bee-jae jung(2010-05-11-화요일)
             ****************************************************/
            try
            {
                Cursor.Current = Cursors.WaitCursor;

                fnSSInitial(SS01);

                fnDataFind();
            }
            catch (Exception ex)
            {
                CmnFunction.ShowMsgBox(ex.Message);
                return;
            }
            finally
            {
                Cursor.Current = Cursors.Default;
            }
        }

        private void btnExcelExport_Click(object sender, EventArgs e)
        {
            /****************************************************
             * Comment : Excel Export Button을 클릭하면
             * 
             * Created By : bee-jae jung(2010-05-11-화요일)
             * 
             * Modified By : bee-jae jung(2010-05-11-화요일)
             ****************************************************/
            try
            {
                Cursor.Current = Cursors.WaitCursor;

                SS01.ExportExcel();
            }
            catch (Exception ex)
            {
                CmnFunction.ShowMsgBox(ex.Message);
                return;
            }
            finally
            {
                Cursor.Current = Cursors.Default;
            }
        }

        #endregion

        
    }
}
