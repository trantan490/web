using System;
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

//using System.Windows.Forms.DataVisualization;
//using System.Windows.Forms.DataVisualization.Charting;


namespace Hana.YLD
{
    public partial class BUM010101 : Miracom.SmartWeb.UI.Controls.udcCUSReport002
    {
        public BUM010101()
        {
            InitializeComponent();

            this.SetFactory("HMKB1");
            cdvFactory.Text = "HMKB1";
            cdvOper.sFactory = cdvFactory.txtValue;

            //SortInit();                       
        }

        #region " Constant Definition"

        private enum COL_LIST
        {
             LOT_ID
            ,MOTHER_LOT_ID
            ,TESTAREA
            ,PRODUCT
            ,START_TIME
            ,END_TIME
            ,YIELD
            ,WAFER_CAT
            ,LOSS_DIE
            ,TESTED_DIE
            ,NETDIE
            ,GEC
        }

        private enum COL_BIN
        {
            LOT_ID
            ,MOTHER_LOT_ID
            ,TESTAREA
            ,PRODUCT
            ,WAFER_ID
            ,WAFER_SEQ
            ,START_TIME
            ,END_TIME
            ,YIELD
            ,WAFER_CAT
            ,LOSS_DIE
            ,TESTED_DIE
            ,NETDIE
            ,GEC
        }
        #endregion

        private string sFromDateTime = null;
        private string sToDateTime = null;
        private string strOper = null;

        #region [ Control Event ]

        private void txtLotID_KeyPress(object sender, KeyPressEventArgs e)
        {
            try
            {
                //if (e.KeyChar == (char)13)
                //{
                //    if (string.IsNullOrEmpty(txtLotID.Text) == false)
                //        btnView.PerformClick();
                //}
            }
            catch (Exception ex)
            {
                CmnFunction.ShowMsgBox(ex.ToString());
            }
        }

        #endregion

        #region [ Button Event ]
        private void btnView_Click(object sender, EventArgs e)
        {
            DataTable dt = null;
            string QueryCond = null;
            long iFrom = 0;
            long iTo = 0;
            string strlotId = string.Empty;
            //int binNo = 0;
            //int prebinNo = 0;
            //int colcnt = 0;            
            //int maxbinno = 0;

            int iRow = 0;

            try
            {
                spdList.ActiveSheet.RowCount = 0;
                spdData.ActiveSheet.RowCount = 0;

                InitSpdColumnHeader();

                if (rdoOper.Checked == true && string.IsNullOrEmpty(cdvOper.Text.Trim()) == true)
                {
                    //Combo Box가 아닌 TextBox에 직접 입력해야 할 수도 있기 때문에 공정을 선택하는 Message이지만 공정을 입력하는 Message로 수정. 
                    CmnFunction.ShowMsgBox(RptMessages.GetMessage("STD050", GlobalVariable.gcLanguage));
                    return;
                }

                sFromDateTime = dtpDateFrom.Value.ToString("yyyyMMdd") + dtpTimeFrom.Value.ToString("HHmm");
                sToDateTime = dtpDateTo.Value.ToString("yyyyMMdd") + dtpTimeTo.Value.ToString("HHmm");

                iFrom = Convert.ToInt64(sFromDateTime);
                iTo = Convert.ToInt64(sToDateTime);
                sFromDateTime = sFromDateTime + "00";
                sToDateTime = sToDateTime + "00";

                if (iFrom > iTo)
                {
                    CmnFunction.ShowMsgBox(RptMessages.GetMessage("STD0076", GlobalVariable.gcLanguage));
                    dtpDateFrom.Focus();
                    return;
                }

                //StdFunction.SetRegister(this.Name, "FACTORY", txtFactory.Text.Trim());
                
                // Factory
                //QueryCond = FwxCmnFunction.PackCondition(QueryCond, txtFactory.Text.Trim());
                QueryCond = FwxCmnFunction.PackCondition(QueryCond, cdvFactory.Text.Trim());

                this.Refresh();
                LoadingPopUp.LoadIngPopUpShow(this);

                dt = CmnFunction.oComm.GetFuncDataTable("DYNAMIC", MakeSqlString());

                if (dt == null || dt.Rows.Count <= 0)
                {
                    //MessageBox.Show(StdLangFunction.FindMessageLanguage("STD002"), "YLD041001");
                    //spdList.Sheets[0].RowCount = 0;
                    
                    dt.Dispose();
                    LoadingPopUp.LoadingPopUpHidden();
                    CmnFunction.ShowMsgBox(RptMessages.GetMessage("STD010", GlobalVariable.gcLanguage));
                    spdList.ActiveSheet.RowCount = 0;
                    return;
                }

                if (dt != null)
                {
                    //spdList.DataSource = dt;


                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        iRow = spdList.ActiveSheet.RowCount;
                        spdList.ActiveSheet.RowCount++;

                        spdList.ActiveSheet.Cells[iRow, (int)COL_LIST.LOT_ID].Value = dt.Rows[i]["LOT_ID"].ToString();
                        spdList.ActiveSheet.Cells[iRow, (int)COL_LIST.MOTHER_LOT_ID].Value = dt.Rows[i]["MOTHER_LOT_ID"].ToString();
                        spdList.ActiveSheet.Cells[iRow, (int)COL_LIST.TESTAREA].Value = dt.Rows[i]["TESTAREA"].ToString();
                        spdList.ActiveSheet.Cells[iRow, (int)COL_LIST.PRODUCT].Value = dt.Rows[i]["PRODUCT"].ToString();
                        spdList.ActiveSheet.Cells[iRow, (int)COL_LIST.START_TIME].Value = dt.Rows[i]["START_TIME"].ToString();
                        spdList.ActiveSheet.Cells[iRow, (int)COL_LIST.END_TIME].Value = dt.Rows[i]["END_TIME"].ToString();
                        spdList.ActiveSheet.Cells[iRow, (int)COL_LIST.YIELD].Value = dt.Rows[i]["YIELD"].ToString();
                        spdList.ActiveSheet.Cells[iRow, (int)COL_LIST.WAFER_CAT].Value = dt.Rows[i]["WAFER_CAT"].ToString();
                        spdList.ActiveSheet.Cells[iRow, (int)COL_LIST.LOSS_DIE].Value = dt.Rows[i]["LOSS_DIE"].ToString();
                        spdList.ActiveSheet.Cells[iRow, (int)COL_LIST.TESTED_DIE].Value = dt.Rows[i]["TESTED_DIE"].ToString();
                        spdList.ActiveSheet.Cells[iRow, (int)COL_LIST.NETDIE].Value = dt.Rows[i]["NETDIE"].ToString();
                        spdList.ActiveSheet.Cells[iRow, (int)COL_LIST.GEC].Value = dt.Rows[i]["GEC"].ToString();
                    }
                }

                //4. Column Auto Fit
                spdList.RPT_AutoFit(false);


                //if (dt != null)
                //{
                //    for (int i = 0; i < dt.Rows.Count; i++) 
                //    {
                        //binNo = Convert.ToInt32(dt.Rows[i]["BIN"].ToString());

                        //if (i == 0)
                            //spdList.ActiveSheet.RowCount++;
                        //else if (i > 0)
                        //{
                        //    if (strlotId == dt.Rows[i]["LOT_ID"].ToString())
                        //    {
                        //        colcnt = spdList.ActiveSheet.ColumnCount;

                        //        if (binNo > maxbinno)
                        //        {
                        //            for (int k = 0; k < binNo - maxbinno; k++)                                    
                        //            {
                        //                spdList.RPT_AddBasicColumn("BIN" + (maxbinno + k + 1).ToString("00"), 0, colcnt + k, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.String, 120);
                        //            }
                        //        }

                        //        spdList.ActiveSheet.Cells[spdList.ActiveSheet.RowCount - 1, (int)COL_LIST.GOOD_DIE + binNo].Text = dt.Rows[i]["BIN_QTY"].ToString();

                        //        prebinNo = Convert.ToInt32(dt.Rows[i]["BIN"].ToString());

                        //        if (prebinNo > maxbinno)
                        //            maxbinno = prebinNo; 

                        //        continue;
                        //    }
                        //    else if (strlotId != dt.Rows[i]["LOT_ID"].ToString())
                        //    {
                        //        spdList.ActiveSheet.RowCount++;
                        //    }
                        //}                        

                        //spdList.ActiveSheet.Cells[spdList.ActiveSheet.RowCount - 1, (int)COL_LIST.LOT_ID].Text = dt.Rows[i]["LOT_ID"].ToString();
                        //spdList.ActiveSheet.Cells[spdList.ActiveSheet.RowCount - 1, (int)COL_LIST.CUST_LOT_ID].Text = dt.Rows[i]["CUST_LOT_ID"].ToString();
                        //spdList.ActiveSheet.Cells[spdList.ActiveSheet.RowCount - 1, (int)COL_LIST.LOT_SUB].Text = dt.Rows[i]["LOT_SUB"].ToString();                        
                        //spdList.ActiveSheet.Cells[spdList.ActiveSheet.RowCount - 1, (int)COL_LIST.DEVICE].Text = dt.Rows[i]["DEVICE"].ToString();
                        //spdList.ActiveSheet.Cells[spdList.ActiveSheet.RowCount - 1, (int)COL_LIST.CUST_DEVICE].Text = dt.Rows[i]["CUST_DEVICE"].ToString();                        
                        //spdList.ActiveSheet.Cells[spdList.ActiveSheet.RowCount - 1, (int)COL_LIST.START_TIME].Text = dt.Rows[i]["START_TIME"].ToString();
                        //spdList.ActiveSheet.Cells[spdList.ActiveSheet.RowCount - 1, (int)COL_LIST.END_TIME].Text = dt.Rows[i]["END_TIME"].ToString();
                        //spdList.ActiveSheet.Cells[spdList.ActiveSheet.RowCount - 1, (int)COL_LIST.TESTER_ID].Text = dt.Rows[i]["TESTER_ID"].ToString();
                        //spdList.ActiveSheet.Cells[spdList.ActiveSheet.RowCount - 1, (int)COL_LIST.PROBE_CARD].Text = dt.Rows[i]["PROBE_CARD"].ToString();
                        //spdList.ActiveSheet.Cells[spdList.ActiveSheet.RowCount - 1, (int)COL_LIST.YIELD].Text = dt.Rows[i]["YIELD"].ToString();
                        //spdList.ActiveSheet.Cells[spdList.ActiveSheet.RowCount - 1, (int)COL_LIST.WAFER_CNT].Text = dt.Rows[i]["WAFER_CNT"].ToString();
                        //spdList.ActiveSheet.Cells[spdList.ActiveSheet.RowCount - 1, (int)COL_LIST.NET_DIE].Text = dt.Rows[i]["NET_DIE"].ToString();
                        //spdList.ActiveSheet.Cells[spdList.ActiveSheet.RowCount - 1, (int)COL_LIST.TOTAL_DIE].Text = dt.Rows[i]["TOTAL_DIE"].ToString();
                        //spdList.ActiveSheet.Cells[spdList.ActiveSheet.RowCount - 1, (int)COL_LIST.GOOD_DIE].Text = dt.Rows[i]["GOOD_DIE"].ToString();

                        //if (i == 0)
                        //{
                        //    for (int k = 0; k < Convert.ToInt32(dt.Rows[i]["BIN"].ToString()); k++)
                        //    {
                                //spdList.RPT_AddBasicColumn("BIN" + (k + 1).ToString("00"), 0, (int)COL_LIST.GOOD_DIE + k + 1, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.String, 120);
                        //    }

                        //    maxbinno = binNo; 
                        //}
                        //else
                        //{
                        //    if(prebinNo > maxbinno)
                        //        maxbinno = prebinNo; 
                        //}

                        //spdList.ActiveSheet.Cells[spdList.ActiveSheet.RowCount - 1, (int)COL_LIST.GOOD_DIE + binNo].Text = dt.Rows[i]["BIN_QTY"].ToString();

                        //prebinNo = Convert.ToInt32(dt.Rows[i]["BIN"].ToString());
                        //strlotId = dt.Rows[i]["LOT_ID"].ToString();
                    //}
                //}

                //StdFunction.FitColumnHeader(spdList);
                //CmnFunction.FitColumnHeader(spdList);
                //spdList.RPT_AlternatingRows();

                //SpreadSetSperator();
                //SpreadDataZeroClear();
            }
            catch (Exception ex)
            {
                CmnFunction.ShowMsgBox("YLD041001.btnView_Click() : " + ex.ToString());
            }
            finally
            {
                LoadingPopUp.LoadingPopUpHidden();
                //if (dt != null) dt.Dispose();
            }
        }

        private void btnExcelExport_Click(object sender, EventArgs e)
        {
            try
            {
                if (saveExcel.ShowDialog(this) == DialogResult.Cancel) return;

                //spdList.ActiveSheet.Protect = false;
                //spdList.SaveExcel(saveExcel.FileName, FarPoint.Excel.ExcelSaveFlags.SaveBothCustomRowAndColumnHeaders);
            }
            catch (Exception ex)
            {
                CmnFunction.ShowMsgBox(ex.ToString());
            }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            try
            {
                //StdFunction.UserLogOut(this.Name);
                this.OnCloseLayoutForm();
                this.Dispose();
            }
            catch (Exception ex)
            {
                CmnFunction.ShowMsgBox(ex.ToString());
            }
        }

        #endregion

        #region [ Function Definition ]
        private void InitSpdColumnHeader()
        {
            spdList.ActiveSheet.RowHeader.ColumnCount = 0;
            spdList.RPT_ColumnInit();

            //spdList.RPT_AddBasicColumn("Customer", 0, 0, Visibles.True, Frozen.True, Align.Center, Merge.True, Formatter.String, 70);
            //spdList.RPT_AddBasicColumn("Bumping Type", 0, 1, Visibles.True, Frozen.True, Align.Center, Merge.True, Formatter.String, 70);
            //spdList.RPT_AddBasicColumn("Operation Flow", 0, 2, Visibles.False, Frozen.True, Align.Left, Merge.True, Formatter.String, 70);
            //spdList.RPT_AddBasicColumn("Layer classification", 0, 3, Visibles.False, Frozen.True, Align.Center, Merge.True, Formatter.String, 70);
            //spdList.RPT_AddBasicColumn("PKG Type", 0, 4, Visibles.False, Frozen.True, Align.Center, Merge.True, Formatter.String, 50);
            //spdList.RPT_AddBasicColumn("RDL Plating", 0, 5, Visibles.False, Frozen.True, Align.Center, Merge.True, Formatter.String, 50);
            //spdList.RPT_AddBasicColumn("Final Bump", 0, 6, Visibles.False, Frozen.True, Align.Center, Merge.True, Formatter.String, 50);
            //spdList.RPT_AddBasicColumn("Sub. Material", 0, 7, Visibles.False, Frozen.True, Align.Center, Merge.True, Formatter.String, 50);
            //spdList.RPT_AddBasicColumn("Size", 0, 8, Visibles.False, Frozen.True, Align.Center, Merge.True, Formatter.String, 50);
            //spdList.RPT_AddBasicColumn("Thickness", 0, 9, Visibles.False, Frozen.True, Align.Center, Merge.True, Formatter.String, 50);
            //spdList.RPT_AddBasicColumn("Flat Type", 0, 10, Visibles.False, Frozen.True, Align.Center, Merge.True, Formatter.String, 50);
            //spdList.RPT_AddBasicColumn("Wafer Orientation", 0, 11, Visibles.False, Frozen.True, Align.Center, Merge.True, Formatter.String, 50);
            
            //spdList.RPT_AddBasicColumn("Lot ID", 0, 12, Visibles.True, Frozen.False, Align.Left, Merge.False, Formatter.String, 120);
            //spdList.RPT_AddBasicColumn("Mother Lot ID", 0, 13, Visibles.True, Frozen.False, Align.Left, Merge.False, Formatter.String, 120);
            //spdList.RPT_AddBasicColumn("Test Area", 0, 14, Visibles.True, Frozen.False, Align.Left, Merge.False, Formatter.String, 120);
            //spdList.RPT_AddBasicColumn("Product", 0, 15, Visibles.True, Frozen.False, Align.Left, Merge.False, Formatter.String, 120);            
            //spdList.RPT_AddBasicColumn("Start Time", 0, 16, Visibles.True, Frozen.False, Align.Left, Merge.False, Formatter.String, 130);
            //spdList.RPT_AddBasicColumn("End Time", 0, 17, Visibles.True, Frozen.False, Align.Left, Merge.False, Formatter.String, 130);
            //spdList.RPT_AddBasicColumn("Yield", 0, 18, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Double1, 70);
            //spdList.RPT_AddBasicColumn("Wafer Cat", 0, 19, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.String, 120);
            //spdList.RPT_AddBasicColumn("Loss Die", 0, 20, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.String, 120);
            //spdList.RPT_AddBasicColumn("Tested Die", 0, 21, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.String, 120);
            //spdList.RPT_AddBasicColumn("Net Die", 0, 22, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.String, 120);
            //spdList.RPT_AddBasicColumn("GEC", 0, 23, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.String, 120);

            // Group항목이 있을경우 반드시 선언해줄것.
            //spdList.RPT_ColumnConfigFromTable(btnSort);

            spdList.RPT_AddBasicColumn("Lot ID", 0, (int)COL_LIST.LOT_ID, Visibles.True, Frozen.True, Align.Left, Merge.False, Formatter.String, 120);
            spdList.RPT_AddBasicColumn("Mother Lot ID", 0, (int)COL_LIST.MOTHER_LOT_ID, Visibles.True, Frozen.False, Align.Left, Merge.False, Formatter.String, 120);
            spdList.RPT_AddBasicColumn("Test Area", 0, (int)COL_LIST.TESTAREA, Visibles.True, Frozen.False, Align.Left, Merge.False, Formatter.String, 120);
            spdList.RPT_AddBasicColumn("Product", 0, (int)COL_LIST.PRODUCT, Visibles.True, Frozen.False, Align.Left, Merge.False, Formatter.String, 120);
            spdList.RPT_AddBasicColumn("Start Time", 0, (int)COL_LIST.START_TIME, Visibles.True, Frozen.False, Align.Left, Merge.False, Formatter.String, 130);
            spdList.RPT_AddBasicColumn("End Time", 0, (int)COL_LIST.END_TIME, Visibles.True, Frozen.False, Align.Left, Merge.False, Formatter.String, 130);
            spdList.RPT_AddBasicColumn("Yield", 0, (int)COL_LIST.YIELD, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.String, 120);
            spdList.RPT_AddBasicColumn("Wafer Cat", 0, (int)COL_LIST.WAFER_CAT, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.String, 120);
            spdList.RPT_AddBasicColumn("Loss Die", 0, (int)COL_LIST.LOSS_DIE, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.String, 120);
            spdList.RPT_AddBasicColumn("Tested Die", 0, (int)COL_LIST.TESTED_DIE, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.String, 120);
            spdList.RPT_AddBasicColumn("Net Die", 0, (int)COL_LIST.NETDIE, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.String, 120);
            spdList.RPT_AddBasicColumn("GEC", 0, (int)COL_LIST.GEC, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.String, 120);                       
        }

        private void InitSpdColumnHeaderDetail()
        {
            spdData.ActiveSheet.RowHeader.ColumnCount = 0;
            spdData.RPT_ColumnInit();

            spdData.RPT_AddBasicColumn("Lot ID", 0, (int)COL_BIN.LOT_ID, Visibles.True, Frozen.True, Align.Left, Merge.False, Formatter.String, 120);
            spdData.RPT_AddBasicColumn("Mother Lot ID", 0, (int)COL_BIN.MOTHER_LOT_ID, Visibles.True, Frozen.False, Align.Left, Merge.False, Formatter.String, 120);
            spdData.RPT_AddBasicColumn("Test Area", 0, (int)COL_BIN.TESTAREA, Visibles.True, Frozen.False, Align.Left, Merge.False, Formatter.String, 120);
            spdData.RPT_AddBasicColumn("Product", 0, (int)COL_BIN.PRODUCT, Visibles.True, Frozen.False, Align.Left, Merge.False, Formatter.String, 120);
            spdData.RPT_AddBasicColumn("Wafer ID", 0, (int)COL_BIN.WAFER_ID, Visibles.True, Frozen.False, Align.Left, Merge.False, Formatter.String, 120);
            spdData.RPT_AddBasicColumn("Wafer Seq", 0, (int)COL_BIN.WAFER_SEQ, Visibles.True, Frozen.False, Align.Left, Merge.False, Formatter.String, 120);
            spdData.RPT_AddBasicColumn("Start Time", 0, (int)COL_BIN.START_TIME, Visibles.True, Frozen.False, Align.Left, Merge.False, Formatter.String, 130);
            spdData.RPT_AddBasicColumn("End Time", 0, (int)COL_BIN.END_TIME, Visibles.True, Frozen.False, Align.Left, Merge.False, Formatter.String, 130);
            spdData.RPT_AddBasicColumn("Yield", 0, (int)COL_BIN.YIELD, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.String, 120);
            spdData.RPT_AddBasicColumn("Wafer Cat", 0, (int)COL_BIN.WAFER_CAT, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.String, 120);
            spdData.RPT_AddBasicColumn("Loss Die", 0, (int)COL_BIN.LOSS_DIE, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.String, 120);
            spdData.RPT_AddBasicColumn("Tasted Die", 0, (int)COL_BIN.TESTED_DIE, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.String, 120);
            spdData.RPT_AddBasicColumn("Net Die", 0, (int)COL_BIN.NETDIE, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.String, 120);
            spdData.RPT_AddBasicColumn("GEC", 0, (int)COL_BIN.GEC, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.String, 120);

        }

        #region " SortInit : Group By 설정 "

        /// <summary>
        /// 3. Group By 정의
        /// </summary>
        private void SortInit()
        {
            if (cdvFactory.Text.Trim() == "HMKB1")
            {
                ((udcTableForm)(this.btnSort.BindingForm)).Clear();
                ((udcTableForm)(this.btnSort.BindingForm)).AddRow("CUSTOMER", "MAT.MAT_GRP_1", "NVL((SELECT DATA_1 FROM MGCMTBLDAT WHERE TABLE_NAME = 'H_CUSTOMER' AND KEY_1 = MAT.MAT_GRP_1 AND ROWNUM=1), '-') AS CUSTOMER", true);
                ((udcTableForm)(this.btnSort.BindingForm)).AddRow("BUMPING TYPE", "MAT.MAT_GRP_2", "MAT.MAT_GRP_2 AS BUMPING_TYPE", true);
                ((udcTableForm)(this.btnSort.BindingForm)).AddRow("PROCESS FLOW", "MAT.MAT_GRP_3", "MAT.MAT_GRP_3 AS PROCESS_FLOW", false);
                ((udcTableForm)(this.btnSort.BindingForm)).AddRow("Layer classification", "MAT.MAT_GRP_4", "MAT.MAT_GRP_4 AS LAYER", false);
                ((udcTableForm)(this.btnSort.BindingForm)).AddRow("PKG TYPE", "MAT.MAT_GRP_5", "MAT.MAT_GRP_5 AS PKG_TYPE", false);
                ((udcTableForm)(this.btnSort.BindingForm)).AddRow("RDL PLATING", "MAT.MAT_GRP_6", "MAT.MAT_GRP_6 AS RDL_PLATING", false);
                ((udcTableForm)(this.btnSort.BindingForm)).AddRow("FINAL BUMP", "MAT.MAT_GRP_7", "MAT.MAT_GRP_7 AS FINAL_BUMP", false);
                ((udcTableForm)(this.btnSort.BindingForm)).AddRow("SUB. MATERIAL", "MAT.MAT_GRP_8", "MAT.MAT_GRP_8 AS SUB_MATERIAL", false);
                ((udcTableForm)(this.btnSort.BindingForm)).AddRow("SIZE", "MAT.MAT_CMF_14", "MAT.MAT_CMF_14 AS WF_SIZE", false);
                ((udcTableForm)(this.btnSort.BindingForm)).AddRow("THICKNESS", "MAT.MAT_CMF_2", "MAT.MAT_CMF_2 AS THICKNESS", false);
                ((udcTableForm)(this.btnSort.BindingForm)).AddRow("FLAT TYPE", "MAT.MAT_CMF_3", "MAT.MAT_CMF_3 AS FLAT_TYPE", false);
                ((udcTableForm)(this.btnSort.BindingForm)).AddRow("WAFER ORIENTATION", "MAT.MAT_CMF_4", "MAT.MAT_CMF_4 AS WAFER_ORIENTATION", false);
            }            
        }

        #endregion

        private void InitValueByRegister()
        {
            //txtFactory.Text = StdFunction.GetRegister(this.Name, "FACTORY", StdGlobalVariable.gsReportFactoryPT);
        }

        public string MakeSqlString()
        {
            try
            {                
                StringBuilder sQuery = new StringBuilder();
                
                sQuery.Append(" SELECT               \n");
                sQuery.Append("      LOT_ID          \n");
                sQuery.Append("     ,MOTHER_LOT_ID          \n");
                sQuery.Append("     ,TESTAREA          \n");
                sQuery.Append("     ,PRODUCT          \n");
                sQuery.Append("     ,START_TIME          \n");
                sQuery.Append("     ,END_TIME          \n");
                sQuery.Append("     ,YIELD          \n");
                sQuery.Append("     ,WAFER_CAT          \n");
                sQuery.Append("     ,LOSS_DIE          \n");
                sQuery.Append("     ,TESTED_DIE          \n");
                sQuery.Append("     ,NETDIE          \n");
                sQuery.Append("     ,GEC          \n");
                sQuery.Append(" FROM VYMSWFRYLD@RPTTOMES YLD, MWIPMATDEF MAT      \n");
                sQuery.Append(" WHERE 1=1          \n");
                sQuery.Append(" AND YLD.PRODUCT = MAT.MAT_ID " + "\n");

                if (string.IsNullOrEmpty(txtLotID.Text) == false)
                    sQuery.Append(" AND LOT_ID LIKE '%' || :LOT_ID || '%'         \n");
                
                if (string.IsNullOrEmpty(txtMatID.Text) == false)
                    sQuery.Append(" AND PRODUCT LIKE '%' || :PRODUCT || '%'       \n");

                if (rdoMMG.Checked == false) //if (string.IsNullOrEmpty(cdvOper.Text) == false)
                {
                    if (string.IsNullOrEmpty(cdvOper.Text) == false)
                        sQuery.Append(" AND TESTAREA = :OPER          \n");
                }

                sQuery.Append(" AND END_TIME BETWEEN TO_DATE(:FROMDATE,'YYYYMMDDHH24MISS') AND TO_DATE(:TODATE,'YYYYMMDDHH24MISS')          \n");
                sQuery.Append(" AND MAT.FACTORY = :FACTORY   \n");
                sQuery.Append(" AND MAT.MAT_VER = 1 " + "\n");

                #region 상세 조회에 따른 SQL문 생성
                if (udcBUMPCondition1.Text != "ALL" && udcBUMPCondition1.Text != "")
                    sQuery.AppendFormat("   AND MAT.MAT_GRP_1 {0} " + "\n", udcBUMPCondition1.SelectedValueToQueryString);

                if (udcBUMPCondition2.Text != "ALL" && udcBUMPCondition2.Text != "")
                    sQuery.AppendFormat("   AND MAT.MAT_GRP_2 {0} " + "\n", udcBUMPCondition2.SelectedValueToQueryString);

                if (udcBUMPCondition3.Text != "ALL" && udcBUMPCondition3.Text != "")
                    sQuery.AppendFormat("   AND MAT.MAT_GRP_3 {0} " + "\n", udcBUMPCondition3.SelectedValueToQueryString);

                if (udcBUMPCondition4.Text != "ALL" && udcBUMPCondition4.Text != "")
                    sQuery.AppendFormat("   AND MAT.MAT_GRP_4 {0} " + "\n", udcBUMPCondition4.SelectedValueToQueryString);

                if (udcBUMPCondition5.Text != "ALL" && udcBUMPCondition5.Text != "")
                    sQuery.AppendFormat("   AND MAT.MAT_GRP_5 {0} " + "\n", udcBUMPCondition5.SelectedValueToQueryString);

                if (udcBUMPCondition6.Text != "ALL" && udcBUMPCondition6.Text != "")
                    sQuery.AppendFormat("   AND MAT.MAT_GRP_6 {0} " + "\n", udcBUMPCondition6.SelectedValueToQueryString);

                if (udcBUMPCondition7.Text != "ALL" && udcBUMPCondition7.Text != "")
                    sQuery.AppendFormat("   AND MAT.MAT_GRP_7 {0} " + "\n", udcBUMPCondition7.SelectedValueToQueryString);

                if (udcBUMPCondition8.Text != "ALL" && udcBUMPCondition8.Text != "")
                    sQuery.AppendFormat("   AND MAT.MAT_GRP_8 {0} " + "\n", udcBUMPCondition8.SelectedValueToQueryString);

                if (udcBUMPCondition9.Text != "ALL" && udcBUMPCondition9.Text != "")
                    sQuery.AppendFormat("   AND MAT.MAT_CMF_14 {0} " + "\n", udcBUMPCondition9.SelectedValueToQueryString);

                if (udcBUMPCondition10.Text != "ALL" && udcBUMPCondition10.Text != "")
                    sQuery.AppendFormat("   AND MAT.MAT_CMF_2 {0} " + "\n", udcBUMPCondition10.SelectedValueToQueryString);

                if (udcBUMPCondition11.Text != "ALL" && udcBUMPCondition11.Text != "")
                    sQuery.AppendFormat("   AND MAT.MAT_CMF_3 {0} " + "\n", udcBUMPCondition11.SelectedValueToQueryString);

                if (udcBUMPCondition12.Text != "ALL" && udcBUMPCondition12.Text != "")
                    sQuery.AppendFormat("   AND MAT.MAT_CMF_4 {0} " + "\n", udcBUMPCondition12.SelectedValueToQueryString);
                #endregion

                sQuery.Append(" ORDER BY  LOT_ID         \n");                
                
                                
                sQuery.Replace(":FACTORY", "'" + cdvFactory.Text.Trim()+ "'");
                sQuery.Replace(":FROMDATE", "'" + sFromDateTime.Trim() + "'");
                sQuery.Replace(":TODATE", "'" + sToDateTime.Trim() + "'");
                sQuery.Replace(":PRODUCT", "'" + txtMatID.Text.Trim() + "'");
                sQuery.Replace(":LOT_ID", "'" + txtLotID.Text.Trim() + "'");

                if (string.IsNullOrEmpty(cdvOper.Text) == false)
                {
                    sQuery.Replace(":OPER", "'" + cdvOper.ValueGetListView.FocusedItem.SubItems[1].Text + "'");   //cdvOper.Text.Trim()
                    strOper = cdvOper.ValueGetListView.FocusedItem.SubItems[1].Text.Trim();  //cdvOperation.Text.Trim();
                }

                
                return sQuery.ToString();

                
                //sQuery.Append(" SELECT            \n");
                //sQuery.Append("     TEST_INFO.L_LOT_ID AS LOT_ID          \n");
                //sQuery.Append("     ,STS.LOT_CMF_2 AS CUST_LOT_ID          \n");
                //sQuery.Append("     ,STS.LOT_CMF_3 AS LOT_SUB          \n");
                //sQuery.Append("     ,DEVICE          \n");
                //sQuery.Append("     ,CUST_DEVICE          \n");
                //sQuery.Append("     ,MIN_START_TIME AS START_TIME         \n");
                //sQuery.Append("     ,MAX_END_TIME AS END_TIME          \n");
                //sQuery.Append("     ,TESTER_ID          \n");
                //sQuery.Append("     ,PROBE_CARD          \n");
                //sQuery.Append("     ,DECODE(MAT.WEIGHT_NET,' ', 0, ROUND(GOOD_DIE/(TO_NUMBER(MAT.WEIGHT_NET)*WAFER_CNT) * 100, 2)) AS YIELD          \n");
                //sQuery.Append("     ,WAFER_CNT          \n");
                //sQuery.Append("     ,DECODE(MAT.WEIGHT_NET,' ',0,TO_NUMBER(MAT.WEIGHT_NET)*WAFER_CNT) AS NET_DIE          \n");
                //sQuery.Append("     ,TOTAL_DIE          \n");
                //sQuery.Append("     ,GOOD_DIE          \n");
                //sQuery.Append("     ,BIN          \n");
                
                //if (rdoQty.Checked == true)
                //    sQuery.Append("     ,BIN_QTY          \n");
                //else
                //    sQuery.Append("     ,DECODE(MAT.WEIGHT_NET,' ', 0, ROUND(BIN_QTY/(TO_NUMBER(MAT.WEIGHT_NET)*WAFER_CNT) * 100, 2)) AS BIN_QTY \n");

                //sQuery.Append(" FROM          \n");
                //sQuery.Append(" (          \n");
                //sQuery.Append("     SELECT           \n");
                //sQuery.Append("         L_LOT_ID          \n");
                //sQuery.Append("         ,MAX(DEVICE) DEVICE          \n");
                //sQuery.Append("         ,MAX(CUST_DEVICE) CUST_DEVICE          \n");
                //sQuery.Append("         ,MIN(START_TIME) MIN_START_TIME          \n");
                //sQuery.Append("         ,MAX(END_TIME) MAX_END_TIME          \n");
                //sQuery.Append("         ,MAX(TESTER_ID) TESTER_ID          \n");
                //sQuery.Append("         ,MAX(PROBE_CARD_ID) PROBE_CARD          \n");
                //sQuery.Append("         ,COUNT(*) WAFER_CNT          \n");
                //sQuery.Append("         ,SUM(TOTAL_DIE) TOTAL_DIE          \n");
                //sQuery.Append("         ,SUM(GOOD_DIE) GOOD_DIE          \n");
                //sQuery.Append("     FROM           \n");
                //sQuery.Append("         (          \n");
                //sQuery.Append("             SELECT           \n");
                //sQuery.Append("                 WAFER.LOT_ID L_LOT_ID          \n");
                //sQuery.Append("                 ,WAFER.*           \n");
                //if (rdoMMG.Checked == true)
                //    sQuery.Append("             FROM TQC_M_WAFER@MES2YMS WAFER          \n");
                //else 
                //    sQuery.Append("             FROM TQC_WAFER@MES2YMS WAFER          \n");
                //sQuery.Append("                 WHERE 1=1          \n");
                //sQuery.Append("                     AND WAFER.RETEST_IDX_REV = 0           \n");
                //if (string.IsNullOrEmpty(txtLotID.Text) == false)
                //    sQuery.Append("                     AND WAFER.LOT_ID LIKE '%' || :LOT_ID || '%'\n");
                //else 
                //    sQuery.Append("                     AND WAFER.LOT_ID IN (SELECT DISTINCT LOT_ID FROM TQC_WAFER@MES2YMS WHERE WAFER.END_TIME BETWEEN TO_DATE(:FROMDATE,'YYYYMMDDHH24MISS') AND TO_DATE(:TODATE,'YYYYMMDDHH24MISS'))          \n");
                //if (rdoMMG.Checked == false)
                //    sQuery.Append("                     AND TESTAREA= :OPER          \n");
                //sQuery.Append("         ) GROUP BY  L_LOT_ID          \n");
                //sQuery.Append(" ) TEST_INFO,          \n");
                //sQuery.Append(" (          \n");
                //sQuery.Append("     SELECT           \n");
                //sQuery.Append("         L_LOT_ID          \n");
                //sQuery.Append("         ,BIN          \n");
                //sQuery.Append("         ,SUM(BIN_QTY) BIN_QTY          \n");
                //sQuery.Append("     FROM           \n");
                //sQuery.Append("         (          \n");
                //sQuery.Append("             SELECT           \n");
                //sQuery.Append("                 WAFER.LOT_ID L_LOT_ID          \n");
                //sQuery.Append("                 ,BIN.BIN AS BIN          \n");
                //sQuery.Append("                 ,BIN.VALUE AS BIN_QTY          \n");
                
                //if (rdoMMG.Checked == true)
                //    sQuery.Append("             FROM TQC_M_WAFER@MES2YMS WAFER, TQP_M_WAFER_SUM_PIVOT@MES2YMS BIN          \n");
                //else
                //    sQuery.Append("             FROM TQC_WAFER@MES2YMS WAFER, TQP_WAFER_SUM_PIVOT@MES2YMS BIN          \n");
                
                //sQuery.Append("             WHERE 1=1          \n");
                //sQuery.Append("                 AND WAFER.RETEST_IDX_REV = 0           \n");
                
                //if (rdoMMG.Checked == true)
                //    sQuery.Append("                 AND BIN.WAFER_ID = WAFER.WAFER_ID           \n");
                //else 
                //    sQuery.Append("                 AND BIN.WAFER_SEQ = WAFER.WAFER_SEQ           \n");

                //if (string.IsNullOrEmpty(txtLotID.Text) == false)
                //    sQuery.Append("                     AND WAFER.LOT_ID LIKE '%' || :LOT_ID || '%'\n");
                //else 
                //    sQuery.Append("                 AND WAFER.LOT_ID IN (SELECT DISTINCT LOT_ID FROM TQC_WAFER@MES2YMS WHERE WAFER.END_TIME BETWEEN TO_DATE(:FROMDATE,'YYYYMMDDHH24MISS') AND TO_DATE(:TODATE,'YYYYMMDDHH24MISS'))          \n");
                
                //if (rdoMMG.Checked == false)
                //    sQuery.Append("                 AND WAFER.TESTAREA= :OPER          \n");
                //sQuery.Append("         ) GROUP BY L_LOT_ID, BIN          \n");
                //sQuery.Append(" ) BIN_INFO          \n");
                //sQuery.Append(" ,MWIPLOTSTS STS          \n");
                //sQuery.Append(" ,MWIPMATDEF MAT          \n");
                //sQuery.Append(" WHERE 1=1          \n");
                //sQuery.Append("     AND TEST_INFO.L_LOT_ID = BIN_INFO.L_LOT_ID          \n");
                //sQuery.Append("     AND  TEST_INFO.L_LOT_ID=STS.LOT_ID          \n");
                //sQuery.Append("     AND TEST_INFO.DEVICE=MAT.MAT_ID          \n");
                //sQuery.Append("     AND MAT.FACTORY= :FACTORY          \n");
                //if (string.IsNullOrEmpty(cdvDevice.Text) == false)
                //    sQuery.Append("         AND TEST_INFO.CUST_DEVICE = :DEVICE          \n");
                //if (string.IsNullOrEmpty(cdvCustomer.Text) == false)
                //    sQuery.Append("         AND MAT.CUSTOMER_ID= :CUSTOMER          \n");
                //sQuery.Append(" ORDER BY           \n");
                //sQuery.Append("     LOT_ID          \n");
                //sQuery.Append("     ,BIN          \n");

                //sQuery.Replace(":FACTORY", "'" + StdGlobalVariable.gsReportFactoryPT + "'");
                //sQuery.Replace(":FROMDATE", "'" + sFromDateTime.Trim() + "'");
                //sQuery.Replace(":TODATE", "'" + sToDateTime.Trim() + "'");
                //sQuery.Replace(":CUSTOMER", "'" + cdvCustomer.Text.Trim() + "'");
                //sQuery.Replace(":DEVICE", "'" + cdvDevice.Text.Trim() + "'");
                //sQuery.Replace(":OPER", "'" + cdvOperation.Text.Trim() + "'");
                //sQuery.Replace(":LOT_ID", "'" + txtLotID.Text.Trim() + "'");
                                
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public string MakeSqlString1(string lot_id)
        {
            try
            {
                StringBuilder sQuery = new StringBuilder();
                // VYMSWFRYLD@TESTMES
                sQuery.Append(" SELECT            \n");
                sQuery.Append("     LOT_ID          \n");
                sQuery.Append("     ,MOTHER_LOT_ID          \n");
                sQuery.Append("     ,TESTAREA          \n");
                sQuery.Append("     ,PRODUCT          \n");
                sQuery.Append("     ,WAFER_ID          \n");
                sQuery.Append("     ,WAFER_SEQ          \n");
                sQuery.Append("     ,START_TIME          \n");
                sQuery.Append("     ,END_TIME          \n");
                sQuery.Append("     ,YIELD          \n");
                sQuery.Append("     ,WAFER_CAT          \n");
                sQuery.Append("     ,LOSS_DIE          \n");
                sQuery.Append("     ,TESTED_DIE          \n");
                sQuery.Append("     ,NETDIE          \n");
                sQuery.Append("     ,GEC          \n");
                sQuery.Append(" FROM VYMSWFRYLD@RPTTOMES       \n");
                sQuery.Append(" WHERE 1=1          \n");
                sQuery.Append(" AND LOT_ID = :LOT_ID          \n");
                //sQuery.Append(" AND FACILITY = :FACILITY          \n");

                if (string.IsNullOrEmpty(txtMatID.Text) == false)
                    sQuery.Append(" AND PRODUCT LIKE '%' || :PRODUCT || '%'       \n");

                if (rdoMMG.Checked == false) //if (string.IsNullOrEmpty(cdvOper.Text) == false)
                {
                    sQuery.Append(" AND TESTAREA = :OPER          \n");
                }
                                
                sQuery.Append(" ORDER BY           \n");
                sQuery.Append("     LOT_ID          \n");

                //sQuery.Replace(":FACILITY", "'" + txtFactory.Text.Trim() + "'");
                sQuery.Replace(":LOT_ID", "'" + lot_id.Trim() + "'");
                sQuery.Replace(":PRODUCT", "'" + txtMatID.Text.Trim() + "'");
                
                if (string.IsNullOrEmpty(strOper) == false)
                    sQuery.Replace(":OPER", "'" + strOper.Trim() + "'");


                return sQuery.ToString();


                //sQuery.Append(" SELECT            \n");
                //sQuery.Append("     TEST_INFO.L_LOT_ID AS LOT_ID          \n");
                //sQuery.Append("     ,STS.LOT_CMF_2 AS CUST_LOT_ID          \n");
                //sQuery.Append("     ,STS.LOT_CMF_3 AS LOT_SUB          \n");
                //sQuery.Append("     ,TEST_INFO.WAFER_NO          \n");
                //sQuery.Append("     ,DEVICE          \n");
                //sQuery.Append("     ,CUST_DEVICE          \n");
                //sQuery.Append("     ,MIN_START_TIME AS START_TIME         \n");
                //sQuery.Append("     ,MAX_END_TIME AS END_TIME          \n");
                //sQuery.Append("     ,TESTER_ID          \n");
                //sQuery.Append("     ,PROBE_CARD          \n");
                //sQuery.Append("     ,DECODE(MAT.WEIGHT_NET,' ', 0, ROUND(GOOD_DIE/TO_NUMBER(MAT.WEIGHT_NET) * 100, 2)) AS YIELD          \n");
                //sQuery.Append("     ,DECODE(MAT.WEIGHT_NET,' ',0,TO_NUMBER(MAT.WEIGHT_NET)) AS NET_DIE          \n");
                //sQuery.Append("     ,TOTAL_DIE          \n");
                //sQuery.Append("     ,GOOD_DIE          \n");
                //sQuery.Append("     ,BIN          \n");
                //if (rdoQty.Checked == true)
                //    sQuery.Append("     ,BIN_QTY          \n");
                //else
                //    sQuery.Append("     ,DECODE(MAT.WEIGHT_NET,' ', 0, ROUND(BIN_QTY/TO_NUMBER(MAT.WEIGHT_NET) * 100, 2)) AS BIN_QTY          \n");
                //sQuery.Append(" FROM          \n");
                //sQuery.Append(" (          \n");
                //sQuery.Append("     SELECT           \n");
                //sQuery.Append("         L_LOT_ID          \n");
                //sQuery.Append("         ,WAFER_NO          \n");
                //sQuery.Append("         ,DEVICE DEVICE          \n");
                //sQuery.Append("         ,CUST_DEVICE CUST_DEVICE          \n");
                //sQuery.Append("         ,START_TIME MIN_START_TIME          \n");
                //sQuery.Append("         ,END_TIME MAX_END_TIME          \n");
                //sQuery.Append("         ,TESTER_ID TESTER_ID          \n");
                //sQuery.Append("         ,PROBE_CARD_ID PROBE_CARD          \n");
                //sQuery.Append("         ,TOTAL_DIE TOTAL_DIE          \n");
                //sQuery.Append("         ,GOOD_DIE GOOD_DIE          \n");
                //sQuery.Append("     FROM           \n");
                //sQuery.Append("         (          \n");
                //sQuery.Append("             SELECT           \n");
                //sQuery.Append("                 WAFER.LOT_ID L_LOT_ID          \n");
                //sQuery.Append("                 ,WAFER.*           \n");
                
                //if (rdoMMG.Checked == true)
                //    sQuery.Append("             FROM TQC_M_WAFER@MES2YMS WAFER          \n");
                //else 
                //    sQuery.Append("             FROM TQC_WAFER@MES2YMS WAFER          \n");
                
                //sQuery.Append("                 WHERE 1=1          \n");
                //sQuery.Append("                     AND WAFER.RETEST_IDX_REV = 0           \n");
                //sQuery.Append("                     AND WAFER.LOT_ID =:LOT_ID          \n");
                
                //if (rdoMMG.Checked == false)
                //    sQuery.Append("                     AND TESTAREA= :OPER          \n");

                //sQuery.Append("         )           \n");
                //sQuery.Append(" ) TEST_INFO,          \n");
                //sQuery.Append(" (          \n");
                //sQuery.Append("     SELECT           \n");
                //sQuery.Append("         L_LOT_ID          \n");
                //sQuery.Append("         ,WAFER_NO          \n");
                //sQuery.Append("         ,BIN          \n");
                //sQuery.Append("         ,BIN_QTY BIN_QTY          \n");
                //sQuery.Append("     FROM           \n");
                //sQuery.Append("         (          \n");
                //sQuery.Append("             SELECT           \n");
                //sQuery.Append("                 WAFER.LOT_ID L_LOT_ID          \n");
                //sQuery.Append("                 ,WAFER.WAFER_NO          \n");
                //sQuery.Append("                 ,BIN.BIN AS BIN          \n");
                //sQuery.Append("                 ,BIN.VALUE AS BIN_QTY          \n");
                //if (rdoMMG.Checked == true)
                //    sQuery.Append("             FROM TQC_M_WAFER@MES2YMS WAFER, TQP_M_WAFER_SUM_PIVOT@MES2YMS BIN          \n");
                //else 
                //    sQuery.Append("             FROM TQC_WAFER@MES2YMS WAFER, TQP_WAFER_SUM_PIVOT@MES2YMS BIN          \n");

                //sQuery.Append("             WHERE 1=1          \n");
                //sQuery.Append("                 AND WAFER.RETEST_IDX_REV = 0           \n");
                //if (rdoMMG.Checked == true)
                //    sQuery.Append("                 AND BIN.WAFER_ID = WAFER.WAFER_ID           \n");
                //else 
                //    sQuery.Append("                 AND BIN.WAFER_SEQ = WAFER.WAFER_SEQ           \n");
                //sQuery.Append("                 AND WAFER.LOT_ID= :LOT_ID          \n");
                //if (rdoMMG.Checked == false)
                //    sQuery.Append("                 AND WAFER.TESTAREA= :OPER         \n");
                //sQuery.Append("         )          \n");
                //sQuery.Append(" ) BIN_INFO          \n");
                //sQuery.Append(" ,MWIPLOTSTS STS          \n");
                //sQuery.Append(" ,MWIPMATDEF MAT          \n");
                //sQuery.Append(" WHERE 1=1          \n");
                //sQuery.Append("     AND TEST_INFO.L_LOT_ID = BIN_INFO.L_LOT_ID          \n");
                //sQuery.Append("     AND TEST_INFO.WAFER_NO=BIN_INFO.WAFER_NO          \n");
                //sQuery.Append("     AND  TEST_INFO.L_LOT_ID=STS.LOT_ID          \n");
                //sQuery.Append("     AND TEST_INFO.DEVICE=MAT.MAT_ID          \n");
                //sQuery.Append("     AND MAT.FACTORY= :FACTORY          \n");
                //sQuery.Append(" ORDER BY           \n");
                //sQuery.Append("     LOT_ID          \n");
                //sQuery.Append("     ,WAFER_NO          \n");
                //sQuery.Append("     ,BIN          \n");

                //sQuery.Replace(":FACTORY", "'" + StdGlobalVariable.gsReportFactoryPT + "'");
                //sQuery.Replace(":LOT_ID", "'" + lot_id.Trim() + "'");
                //sQuery.Replace(":OPER", "'" + strOper.Trim() + "'");                
                                
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void SpreadDataZeroClear()
        {
            //if (spdList.Sheets[0].Rows.Count > 0)
            //{
            //    //for (int i = 0; i < spdHistory.Sheets[0].Rows.Count; i++)
            //    //{
            //    //    for (int k = (int)COL_LIST.GROUP_DESC + 1; k < spdHistory.Sheets[0].ColumnCount; k++)
            //    //    {
            //    //        if (Convert.ToDouble(spdHistory.Sheets[0].Cells[i, k].Value) == 0)
            //    //            spdHistory.Sheets[0].Cells[i, k].Value = null;
            //    //    }
            //    //}
            //}
        }

        private void SpreadSetSperator()
        {
            FarPoint.Win.Spread.CellType.NumberCellType numCellType = new FarPoint.Win.Spread.CellType.NumberCellType();
            numCellType.Separator = ",";
            numCellType.DecimalPlaces = 0;
            numCellType.ShowSeparator = true;

            FarPoint.Win.Spread.CellType.NumberCellType doubleCellType = new FarPoint.Win.Spread.CellType.NumberCellType();
            doubleCellType.ButtonAlign = FarPoint.Win.ButtonAlign.Right;
            doubleCellType.DecimalPlaces = 1;
            doubleCellType.DropDownButton = false;
            doubleCellType.MaximumValue = 99999999999.9;
            doubleCellType.MinimumValue = -99999999999.9;
            doubleCellType.Separator = ",";
            doubleCellType.ShowSeparator = true;
            doubleCellType.NegativeRed = true;
            
        }

        #endregion

        #region [ Form Event ]
        private void YLD041001_Load(object sender, EventArgs e)
        {
            InitValueByRegister();
            dtpDateFrom.Value = DateTime.Now.AddDays(-7);
            dtpDateTo.Value = DateTime.Now;
            txtFactory.Text = "HMKB1";
            //txtFactory.Text = StdGlobalVariable.gsReportFactoryPT;
            
            InitSpdColumnHeader();
            InitSpdColumnHeaderDetail();

            //if (string.IsNullOrEmpty(StdGlobalVariable.gsCustomer.Trim()) != true)
            //{
            //    cdvCustomer.Text = StdGlobalVariable.gsCustomer.Trim();
            //    cdvCustomer.Enabled = false;
            //}

            ////StdLangFunction.ToClientLanguage(this);
        }
        #endregion

        private void spdList_CellClick(object sender, FarPoint.Win.Spread.CellClickEventArgs e)
        {
            try
            {
                string QueryCond = null;
                DataTable dt = null;
                string sLotID = null;

                string strWaferNo = string.Empty;
                //int binNo = 0;
                //int colcnt = 0;
                //int maxbinno = 0;
                //int prebinNo = 0;
                int iRow = 0;

                //spdData.Sheets[0].RowCount = 0;
                spdData.ActiveSheet.RowCount = 0;

                InitSpdColumnHeaderDetail();

                // Factory
                //QueryCond = FwxCmnFunction.PackCondition(QueryCond, txtFactory.Text.Trim());
                QueryCond = FwxCmnFunction.PackCondition(QueryCond, cdvFactory.Text.Trim());

                this.Refresh();
                LoadingPopUp.LoadIngPopUpShow(this);

                sLotID = spdList.ActiveSheet.Cells[e.Row, (int)COL_LIST.LOT_ID].Text;

                dt = CmnFunction.oComm.GetFuncDataTable("DYNAMIC", MakeSqlString1(sLotID));

                if (dt == null || dt.Rows.Count <= 0)
                {
                    //MessageBox.Show(StdLangFunction.FindMessageLanguage("STD002"), "BUM010101");
                    //spdData.Sheets[0].RowCount = 0;
                    spdData.ActiveSheet.RowCount = 0;
                    return;
                }

                if (dt != null)
                {
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        iRow = spdData.ActiveSheet.RowCount;
                        spdData.ActiveSheet.RowCount++;

                        spdData.ActiveSheet.Cells[iRow, (int)COL_BIN.LOT_ID].Value = dt.Rows[i]["LOT_ID"].ToString();
                        spdData.ActiveSheet.Cells[iRow, (int)COL_BIN.MOTHER_LOT_ID].Value = dt.Rows[i]["MOTHER_LOT_ID"].ToString();
                        spdData.ActiveSheet.Cells[iRow, (int)COL_BIN.TESTAREA].Value = dt.Rows[i]["TESTAREA"].ToString();
                        spdData.ActiveSheet.Cells[iRow, (int)COL_BIN.PRODUCT].Value = dt.Rows[i]["PRODUCT"].ToString();
                        spdData.ActiveSheet.Cells[iRow, (int)COL_BIN.WAFER_ID].Value = dt.Rows[i]["WAFER_ID"].ToString();
                        spdData.ActiveSheet.Cells[iRow, (int)COL_BIN.WAFER_SEQ].Value = dt.Rows[i]["WAFER_SEQ"].ToString();
                        spdData.ActiveSheet.Cells[iRow, (int)COL_BIN.START_TIME].Value = dt.Rows[i]["START_TIME"].ToString();
                        spdData.ActiveSheet.Cells[iRow, (int)COL_BIN.END_TIME].Value = dt.Rows[i]["END_TIME"].ToString();
                        spdData.ActiveSheet.Cells[iRow, (int)COL_BIN.YIELD].Value = dt.Rows[i]["YIELD"].ToString();
                        spdData.ActiveSheet.Cells[iRow, (int)COL_BIN.WAFER_CAT].Value = dt.Rows[i]["WAFER_CAT"].ToString();
                        spdData.ActiveSheet.Cells[iRow, (int)COL_BIN.LOSS_DIE].Value = dt.Rows[i]["LOSS_DIE"].ToString();
                        spdData.ActiveSheet.Cells[iRow, (int)COL_BIN.TESTED_DIE].Value = dt.Rows[i]["TESTED_DIE"].ToString();
                        spdData.ActiveSheet.Cells[iRow, (int)COL_BIN.NETDIE].Value = dt.Rows[i]["NETDIE"].ToString();
                        spdData.ActiveSheet.Cells[iRow, (int)COL_BIN.GEC].Value = dt.Rows[i]["GEC"].ToString();

                    }
                }


        //        if (dt != null)
        //        {
        //            for (int i = 0; i < dt.Rows.Count; i++)
        //            {
        //                binNo = Convert.ToInt32(dt.Rows[i]["BIN"].ToString());
        //                colcnt = spdData.ActiveSheet.ColumnCount;

        //                if (i == 0)
        //                    spdData.ActiveSheet.RowCount++;
        //                else if (i > 0)
        //                {
        //                    if (strWaferNo == dt.Rows[i]["WAFER_NO"].ToString())
        //                    {
        //                        if (binNo > maxbinno)
        //                        {
        //                            for (int k = 0; k < binNo - maxbinno; k++)
        //                            {
        //                                spdData.RPT_AddBasicColumn("BIN" + (maxbinno + k + 1).ToString("00"), 0, colcnt + k, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.String, 120);
        //                            }
        //                        }

        //                        spdData.ActiveSheet.Cells[spdData.ActiveSheet.RowCount - 1, (int)COL_BIN.GOOD_DIE + binNo].Text = dt.Rows[i]["BIN_QTY"].ToString();

        //                        prebinNo = Convert.ToInt32(dt.Rows[i]["BIN"].ToString());

        //                        if (prebinNo > maxbinno)
        //                            maxbinno = prebinNo;

        //                        continue;
        //                    }
        //                    else if (strWaferNo != dt.Rows[i]["WAFER_NO"].ToString())
        //                    {
        //                        spdData.ActiveSheet.RowCount++;

        //                        if (binNo > maxbinno)
        //                        {
        //                            for (int k = 0; k < Convert.ToInt32(dt.Rows[i]["BIN"].ToString()) - maxbinno; k++)
        //                            {
        //                                spdData.RPT_AddBasicColumn("BIN" + (maxbinno + k + 1).ToString("00"), 0, colcnt + k, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.String, 120);
        //                            }

        //                            maxbinno = binNo;
        //                        }
        //                    }
        //                }                        

        //                spdData.ActiveSheet.Cells[spdData.ActiveSheet.RowCount - 1, (int)COL_BIN.LOT_ID].Text = dt.Rows[i]["LOT_ID"].ToString();
        //                spdData.ActiveSheet.Cells[spdData.ActiveSheet.RowCount - 1, (int)COL_BIN.CUST_LOT_ID].Text = dt.Rows[i]["CUST_LOT_ID"].ToString();
        //                spdData.ActiveSheet.Cells[spdData.ActiveSheet.RowCount - 1, (int)COL_BIN.LOT_SUB].Text = dt.Rows[i]["LOT_SUB"].ToString();
        //                spdData.ActiveSheet.Cells[spdData.ActiveSheet.RowCount - 1, (int)COL_BIN.WAFER_NO].Text = dt.Rows[i]["WAFER_NO"].ToString();
        //                spdData.ActiveSheet.Cells[spdData.ActiveSheet.RowCount - 1, (int)COL_BIN.DEVICE].Text = dt.Rows[i]["DEVICE"].ToString();
        //                spdData.ActiveSheet.Cells[spdData.ActiveSheet.RowCount - 1, (int)COL_BIN.CUST_DEVICE].Text = dt.Rows[i]["CUST_DEVICE"].ToString();
        //                spdData.ActiveSheet.Cells[spdData.ActiveSheet.RowCount - 1, (int)COL_BIN.END_TIME].Text = dt.Rows[i]["END_TIME"].ToString();
        //                spdData.ActiveSheet.Cells[spdData.ActiveSheet.RowCount - 1, (int)COL_BIN.TESTER_ID].Text = dt.Rows[i]["TESTER_ID"].ToString();
        //                spdData.ActiveSheet.Cells[spdData.ActiveSheet.RowCount - 1, (int)COL_BIN.PROBE_CARD].Text = dt.Rows[i]["PROBE_CARD"].ToString();
        //                spdData.ActiveSheet.Cells[spdData.ActiveSheet.RowCount - 1, (int)COL_BIN.YIELD].Text = dt.Rows[i]["YIELD"].ToString();
        //                spdData.ActiveSheet.Cells[spdData.ActiveSheet.RowCount - 1, (int)COL_BIN.NET_DIE].Text = dt.Rows[i]["NET_DIE"].ToString();
        //                spdData.ActiveSheet.Cells[spdData.ActiveSheet.RowCount - 1, (int)COL_BIN.TOTAL_DIE].Text = dt.Rows[i]["TOTAL_DIE"].ToString();
        //                spdData.ActiveSheet.Cells[spdData.ActiveSheet.RowCount - 1, (int)COL_BIN.GOOD_DIE].Text = dt.Rows[i]["GOOD_DIE"].ToString();

        //                if (i == 0)
        //                {
        //                    for (int k = 0; k < Convert.ToInt32(dt.Rows[i]["BIN"].ToString()); k++)
        //                    {
        //                        spdData.RPT_AddBasicColumn("BIN" + (k + 1).ToString("00"), 0, (int)COL_BIN.GOOD_DIE + k + 1, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.String, 120);
        //                    }

        //                    maxbinno = binNo;
        //                }
        //                else
        //                {
        //                    if (prebinNo > maxbinno)
        //                        maxbinno = prebinNo;
        //                }

        //                spdData.ActiveSheet.Cells[spdData.ActiveSheet.RowCount - 1, (int)COL_BIN.GOOD_DIE + binNo].Text = dt.Rows[i]["BIN_QTY"].ToString();

        //                prebinNo = Convert.ToInt32(dt.Rows[i]["BIN"].ToString());
        //                strWaferNo = dt.Rows[i]["WAFER_NO"].ToString();
        //            }
        //        }

        //        //StdFunction.FitColumnHeader(spdData);
        //        CmnFunction.FitColumnHeader(spdData);
        //        spdData.RPT_AlternatingRows();

        //        SpreadSetSperator();
        //        SpreadDataZeroClear();                
            }
            catch (Exception ex)
            {
                CmnFunction.ShowMsgBox("BUM010101.spdList_CellClick() : " + ex.ToString());
                return;
            }
            finally
            {
                LoadingPopUp.LoadingPopUpHidden();
            }
        }

        //private void cdvDevice_ButtonPress(object sender, EventArgs e)
        //{
        //    try
        //    {
        //        cdvDevice.Init();
        //        StdInitFunction.InitListView(cdvDevice.GetListView);
        //        cdvDevice.Columns.Add("DEVICE", 50, HorizontalAlignment.Left);
        //        cdvDevice.Columns.Add("DESC", 50, HorizontalAlignment.Left);
        //        cdvDevice.SelectedSubItemIndex = 0;

        //        cdvDevice.AddEmptyRow(1);
        //        StdListFunction.ViewMaterialList(cdvDevice.GetListView, txtFactory.Text.Trim(), "DEVICE");
        //        CusListFunction.GetDeviceList(cdvDevice.GetListView, "CUST_DEVICE", cdvCustomer.Text, StdGlobalVariable.gsReportFactoryPT);
        //    }
        //    catch (Exception ex)
        //    {
        //        CmnFunction.ShowMsgBox("BUM010101.cdvDevice_ButtonPress() : " + ex.ToString());
        //    }
        //}

        //private void cdvCustomer_ButtonPress(object sender, EventArgs e)
        //{
        //    try
        //    {
        //        cdvCustomer.Init();
        //        StdInitFunction.InitListView(cdvCustomer.GetListView);
        //        cdvCustomer.Columns.Add("Customer", 50, HorizontalAlignment.Left);
        //        cdvCustomer.Columns.Add("DESC", 50, HorizontalAlignment.Left);
        //        cdvCustomer.SelectedSubItemIndex = 0;

        //        cdvCustomer.AddEmptyRow(1);
        //        CusListFunction.CustomerList(cdvCustomer.GetListView, txtFactory.Text.Trim());
        //    }
        //    catch (Exception ex)
        //    {
        //        CmnFunction.ShowMsgBox("BUM010101.cdvCustomer_ButtonPress() : " + ex.ToString());
        //    }
        //}

        //private void cdvOperation_ButtonPress(object sender, EventArgs e)
        //{
        //    cdvOperation.Init();
        //    StdInitFunction.InitListView(cdvOperation.GetListView);
        //    cdvOperation.Columns.Add("KEY_1", 30, HorizontalAlignment.Left);
        //    cdvOperation.Columns.Add("DATA_1", 70, HorizontalAlignment.Left);
        //    cdvOperation.SelectedSubItemIndex = 0;
        //    cdvOperation.DisplaySubItemIndex = 1;

        //    StdListFunction.ViewOperList(cdvOperation.GetListView, txtFactory.Text, "");
        //}

        private void btnLotListExcel_Click(object sender, EventArgs e)
        {
            try
            {
                if (saveExcel.ShowDialog(this) == DialogResult.Cancel) return;

                //spdData.ActiveSheet.Protect = false;
                //spdData.SaveExcel(saveExcel.FileName, FarPoint.Excel.ExcelSaveFlags.SaveBothCustomRowAndColumnHeaders);
            }
            catch (Exception ex)
            {
                CmnFunction.ShowMsgBox(ex.ToString());
            }
        }

        private void cdvFactory_ValueSelectedItemChanged(object sender, MCCodeViewSelChanged_EventArgs e)
        {
            cdvOper.sFactory = cdvFactory.txtValue;
        }

        private void rdoMMG_CheckedChanged(object sender, EventArgs e)
        {
            if (rdoMMG.Checked == true)
                cdvOper.Text = "";  //cdvOper.Enabled = false;
            //else
                //cdvOper.Enabled = true;
        }
    }
}
