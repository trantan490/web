using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;

//using Miracom.SmartWeb.UI;
//using Miracom.SmartWeb.FWX;

using Miracom.UI;
using Miracom.SmartWeb;
using Miracom.SmartWeb.UI;
using Miracom.SmartWeb.FWX;
using Miracom.SmartWeb.UI.Controls;

//using System.Windows.Forms.DataVisualization;
//using System.Windows.Forms.DataVisualization.Charting;


namespace Hana.YLD
{
    public partial class YLD041001 : Miracom.SmartWeb.UI.Controls.udcCUSReportOven002
    {
        public YLD041001()
        {
            InitializeComponent();
        }

        #region " Constant Definition"

        private enum COL_LIST
        {
             LOT_ID
            ,CUST_LOT_ID
            ,LOT_SUB
            ,DEVICE
            ,CUST_DEVICE
            ,START_TIME
            ,END_TIME
            ,TESTER_ID
            ,PROBE_CARD
            ,YIELD
            ,WAFER_CNT
            ,NET_DIE
            ,TOTAL_DIE
            ,GOOD_DIE
        }
        private enum COL_BIN
        {
             LOT_ID
            ,CUST_LOT_ID
            ,LOT_SUB
            ,WAFER_NO
            ,DEVICE
            ,CUST_DEVICE
            ,START_TIME
            ,END_TIME
            ,TESTER_ID
            ,PROBE_CARD
            ,YIELD
            ,NET_DIE
            ,TOTAL_DIE
            ,GOOD_DIE
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
            int binNo = 0;
            int prebinNo = 0;
            //int colcnt = 0;            
            int maxbinno = 0;

            try
            {
                //spdList.Sheets[0].RowCount = 0;
                //spdData.Sheets[0].RowCount = 0;

                InitSpdColumnHeader();

                if (rdoOper.Checked == true && string.IsNullOrEmpty(cdvOperation.Text.Trim()) == true)
                {
                    //Combo Box가 아닌 TextBox에 직접 입력해야 할 수도 있기 때문에 공정을 선택하는 Message이지만 공정을 입력하는 Message로 수정. 
                    CmnFunction.ShowMsgBox(RptMessages.GetMessage("STD064", GlobalVariable.gcLanguage));
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
                    CmnFunction.ShowMsgBox(RptMessages.GetMessage("STD076", GlobalVariable.gcLanguage));
                    dtpDateFrom.Focus();
                    return;
                }

                //StdFunction.SetRegister(this.Name, "FACTORY", txtFactory.Text.Trim());

                // Factory
                QueryCond = FwxCmnFunction.PackCondition(QueryCond, txtFactory.Text.Trim());

                this.Refresh();
                LoadingPopUp.LoadIngPopUpShow(this);

                dt = CmnFunction.oComm.GetFuncDataTable("DYNAMIC", MakeSqlString());

                if (dt == null || dt.Rows.Count <= 0)
                {
                    //MessageBox.Show(StdLangFunction.FindMessageLanguage("STD002"), "YLD041001");
                    //spdList.Sheets[0].RowCount = 0;                   
                    return;
                }

                if (dt != null)
                {
                    for (int i = 0; i < dt.Rows.Count; i++) 
                    {
                        binNo = Convert.ToInt32(dt.Rows[i]["BIN"].ToString());

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

                        if (i == 0)
                        {
                            for (int k = 0; k < Convert.ToInt32(dt.Rows[i]["BIN"].ToString()); k++)
                            {
                                //spdList.RPT_AddBasicColumn("BIN" + (k + 1).ToString("00"), 0, (int)COL_LIST.GOOD_DIE + k + 1, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.String, 120);
                            }

                            maxbinno = binNo; 
                        }
                        else
                        {
                            if(prebinNo > maxbinno)
                                maxbinno = prebinNo; 
                        }

                        //spdList.ActiveSheet.Cells[spdList.ActiveSheet.RowCount - 1, (int)COL_LIST.GOOD_DIE + binNo].Text = dt.Rows[i]["BIN_QTY"].ToString();

                        prebinNo = Convert.ToInt32(dt.Rows[i]["BIN"].ToString());
                        strlotId = dt.Rows[i]["LOT_ID"].ToString();
                    }
                }

                //StdFunction.FitColumnHeader(spdList);
                //CmnFunction.FitColumnHeader(spdList);
                //spdList.RPT_AlternatingRows();

                SpreadSetSperator();
                SpreadDataZeroClear();
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
            //spdList.RPT_InitSpread(true, true, FarPoint.Win.Spread.OperationMode.SingleSelect);
            //spdList.Sheets[0].RowHeader.ColumnCount = 1;
            //spdList.RPT_InitColumn();
            //spdList.RPT_AddBasicColumn("Lot ID", 0, (int)COL_LIST.LOT_ID, Visibles.True, Frozen.True, Align.Left, Merge.False, Formatter.String, 120);
            //spdList.RPT_AddBasicColumn("Cust Lot ID", 0, (int)COL_LIST.CUST_LOT_ID, Visibles.True, Frozen.False, Align.Left, Merge.False, Formatter.String, 120);
            //spdList.RPT_AddBasicColumn("Lot Sub", 0, (int)COL_LIST.LOT_SUB, Visibles.True, Frozen.False, Align.Left, Merge.False, Formatter.String, 120);            
            //spdList.RPT_AddBasicColumn("Device", 0, (int)COL_LIST.DEVICE, Visibles.True, Frozen.False, Align.Left, Merge.False, Formatter.String, 120);            
            //spdList.RPT_AddBasicColumn("Cust Device", 0, (int)COL_LIST.CUST_DEVICE, Visibles.True, Frozen.False, Align.Left, Merge.False, Formatter.String, 120);  
            //spdList.RPT_AddBasicColumn("Start Time", 0, (int)COL_LIST.START_TIME, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.String, 120);
            //spdList.RPT_AddBasicColumn("End Time", 0, (int)COL_LIST.END_TIME, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.String, 120);
            //spdList.RPT_AddBasicColumn("Tester ID", 0, (int)COL_LIST.TESTER_ID, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.String, 120);
            //spdList.RPT_AddBasicColumn("Probe Card", 0, (int)COL_LIST.PROBE_CARD, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.String, 120);
            //spdList.RPT_AddBasicColumn("Yield", 0, (int)COL_LIST.YIELD, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.String, 120);
            //spdList.RPT_AddBasicColumn("Wafer Cnt", 0, (int)COL_LIST.WAFER_CNT, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.String, 120);
            //spdList.RPT_AddBasicColumn("Net Die", 0, (int)COL_LIST.NET_DIE, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.String, 120);
            //spdList.RPT_AddBasicColumn("Total DIe", 0, (int)COL_LIST.TOTAL_DIE, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.String, 120);
            //spdList.RPT_AddBasicColumn("Good Die", 0, (int)COL_LIST.GOOD_DIE, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.String, 120);
            ////spdList.RPT_SetCellsType();                          
        }

        private void InitSpdColumnHeaderDetail()
        {
            //spdData.RPT_InitSpread(true, true, FarPoint.Win.Spread.OperationMode.SingleSelect);
            //spdData.Sheets[0].RowHeader.ColumnCount = 1;
            //spdData.RPT_InitColumn();
            //spdData.RPT_AddBasicColumn("Lot ID", 0, (int)COL_BIN.LOT_ID, Visibles.True, Frozen.False, Align.Left, Merge.False, Formatter.String, 120);
            //spdData.RPT_AddBasicColumn("Cust Lot ID", 0, (int)COL_BIN.CUST_LOT_ID, Visibles.True, Frozen.False, Align.Left, Merge.False, Formatter.String, 120);
            //spdData.RPT_AddBasicColumn("Lot Sub", 0, (int)COL_BIN.LOT_SUB, Visibles.True, Frozen.False, Align.Left, Merge.False, Formatter.String, 120);
            //spdData.RPT_AddBasicColumn("Wafer No", 0, (int)COL_BIN.WAFER_NO, Visibles.True, Frozen.False, Align.Left, Merge.False, Formatter.String, 120);
            //spdData.RPT_AddBasicColumn("Device", 0, (int)COL_BIN.DEVICE, Visibles.True, Frozen.False, Align.Left, Merge.False, Formatter.String, 120);
            //spdData.RPT_AddBasicColumn("Cust Device", 0, (int)COL_BIN.CUST_DEVICE, Visibles.True, Frozen.False, Align.Left, Merge.False, Formatter.String, 120);
            //spdData.RPT_AddBasicColumn("Start Time", 0, (int)COL_BIN.START_TIME, Visibles.True, Frozen.False, Align.Left, Merge.False, Formatter.String, 120);
            //spdData.RPT_AddBasicColumn("End Time", 0, (int)COL_BIN.END_TIME, Visibles.True, Frozen.False, Align.Left, Merge.False, Formatter.String, 120);
            //spdData.RPT_AddBasicColumn("Tester ID", 0, (int)COL_BIN.TESTER_ID, Visibles.True, Frozen.False, Align.Left, Merge.False, Formatter.String, 120);
            //spdData.RPT_AddBasicColumn("Probe Card", 0, (int)COL_BIN.PROBE_CARD, Visibles.True, Frozen.False, Align.Left, Merge.False, Formatter.String, 120);
            //spdData.RPT_AddBasicColumn("Yield", 0, (int)COL_BIN.YIELD, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.String, 120);
            //spdData.RPT_AddBasicColumn("Net Die", 0, (int)COL_BIN.NET_DIE, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.String, 120);
            //spdData.RPT_AddBasicColumn("Total Die", 0, (int)COL_BIN.TOTAL_DIE, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.String, 120);
            //spdData.RPT_AddBasicColumn("Good Die", 0, (int)COL_BIN.GOOD_DIE, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.String, 120);
            ////spdData.RPT_SetCellsType();
        }

        private void InitValueByRegister()
        {
            //txtFactory.Text = StdFunction.GetRegister(this.Name, "FACTORY", StdGlobalVariable.gsReportFactoryPT);
        }

        public string MakeSqlString()
        {
            try
            {
                StringBuilder sQuery = new StringBuilder();

                sQuery.Append(" SELECT            \n");
                sQuery.Append("     TEST_INFO.L_LOT_ID AS LOT_ID          \n");
                sQuery.Append("     ,STS.LOT_CMF_2 AS CUST_LOT_ID          \n");
                sQuery.Append("     ,STS.LOT_CMF_3 AS LOT_SUB          \n");
                sQuery.Append("     ,DEVICE          \n");
                sQuery.Append("     ,CUST_DEVICE          \n");
                sQuery.Append("     ,MIN_START_TIME AS START_TIME         \n");
                sQuery.Append("     ,MAX_END_TIME AS END_TIME          \n");
                sQuery.Append("     ,TESTER_ID          \n");
                sQuery.Append("     ,PROBE_CARD          \n");
                sQuery.Append("     ,DECODE(MAT.WEIGHT_NET,' ', 0, ROUND(GOOD_DIE/(TO_NUMBER(MAT.WEIGHT_NET)*WAFER_CNT) * 100, 2)) AS YIELD          \n");
                sQuery.Append("     ,WAFER_CNT          \n");
                sQuery.Append("     ,DECODE(MAT.WEIGHT_NET,' ',0,TO_NUMBER(MAT.WEIGHT_NET)*WAFER_CNT) AS NET_DIE          \n");
                sQuery.Append("     ,TOTAL_DIE          \n");
                sQuery.Append("     ,GOOD_DIE          \n");
                sQuery.Append("     ,BIN          \n");
                
                if (rdoQty.Checked == true)
                    sQuery.Append("     ,BIN_QTY          \n");
                else
                    sQuery.Append("     ,DECODE(MAT.WEIGHT_NET,' ', 0, ROUND(BIN_QTY/(TO_NUMBER(MAT.WEIGHT_NET)*WAFER_CNT) * 100, 2)) AS BIN_QTY \n");

                sQuery.Append(" FROM          \n");
                sQuery.Append(" (          \n");
                sQuery.Append("     SELECT           \n");
                sQuery.Append("         L_LOT_ID          \n");
                sQuery.Append("         ,MAX(DEVICE) DEVICE          \n");
                sQuery.Append("         ,MAX(CUST_DEVICE) CUST_DEVICE          \n");
                sQuery.Append("         ,MIN(START_TIME) MIN_START_TIME          \n");
                sQuery.Append("         ,MAX(END_TIME) MAX_END_TIME          \n");
                sQuery.Append("         ,MAX(TESTER_ID) TESTER_ID          \n");
                sQuery.Append("         ,MAX(PROBE_CARD_ID) PROBE_CARD          \n");
                sQuery.Append("         ,COUNT(*) WAFER_CNT          \n");
                sQuery.Append("         ,SUM(TOTAL_DIE) TOTAL_DIE          \n");
                sQuery.Append("         ,SUM(GOOD_DIE) GOOD_DIE          \n");
                sQuery.Append("     FROM           \n");
                sQuery.Append("         (          \n");
                sQuery.Append("             SELECT           \n");
                sQuery.Append("                 WAFER.LOT_ID L_LOT_ID          \n");
                sQuery.Append("                 ,WAFER.*           \n");
                if (rdoMMG.Checked == true)
                    sQuery.Append("             FROM TQC_M_WAFER@MES2YMS WAFER          \n");
                else 
                    sQuery.Append("             FROM TQC_WAFER@MES2YMS WAFER          \n");
                sQuery.Append("                 WHERE 1=1          \n");
                sQuery.Append("                     AND WAFER.RETEST_IDX_REV = 0           \n");
                if (string.IsNullOrEmpty(txtLotID.Text) == false)
                    sQuery.Append("                     AND WAFER.LOT_ID LIKE '%' || :LOT_ID || '%'\n");
                else 
                    sQuery.Append("                     AND WAFER.LOT_ID IN (SELECT DISTINCT LOT_ID FROM TQC_WAFER@MES2YMS WHERE WAFER.END_TIME BETWEEN TO_DATE(:FROMDATE,'YYYYMMDDHH24MISS') AND TO_DATE(:TODATE,'YYYYMMDDHH24MISS'))          \n");
                if (rdoMMG.Checked == false)
                    sQuery.Append("                     AND TESTAREA= :OPER          \n");
                sQuery.Append("         ) GROUP BY  L_LOT_ID          \n");
                sQuery.Append(" ) TEST_INFO,          \n");
                sQuery.Append(" (          \n");
                sQuery.Append("     SELECT           \n");
                sQuery.Append("         L_LOT_ID          \n");
                sQuery.Append("         ,BIN          \n");
                sQuery.Append("         ,SUM(BIN_QTY) BIN_QTY          \n");
                sQuery.Append("     FROM           \n");
                sQuery.Append("         (          \n");
                sQuery.Append("             SELECT           \n");
                sQuery.Append("                 WAFER.LOT_ID L_LOT_ID          \n");
                sQuery.Append("                 ,BIN.BIN AS BIN          \n");
                sQuery.Append("                 ,BIN.VALUE AS BIN_QTY          \n");
                
                if (rdoMMG.Checked == true)
                    sQuery.Append("             FROM TQC_M_WAFER@MES2YMS WAFER, TQP_M_WAFER_SUM_PIVOT@MES2YMS BIN          \n");
                else
                    sQuery.Append("             FROM TQC_WAFER@MES2YMS WAFER, TQP_WAFER_SUM_PIVOT@MES2YMS BIN          \n");
                
                sQuery.Append("             WHERE 1=1          \n");
                sQuery.Append("                 AND WAFER.RETEST_IDX_REV = 0           \n");
                
                if (rdoMMG.Checked == true)
                    sQuery.Append("                 AND BIN.WAFER_ID = WAFER.WAFER_ID           \n");
                else 
                    sQuery.Append("                 AND BIN.WAFER_SEQ = WAFER.WAFER_SEQ           \n");

                if (string.IsNullOrEmpty(txtLotID.Text) == false)
                    sQuery.Append("                     AND WAFER.LOT_ID LIKE '%' || :LOT_ID || '%'\n");
                else 
                    sQuery.Append("                 AND WAFER.LOT_ID IN (SELECT DISTINCT LOT_ID FROM TQC_WAFER@MES2YMS WHERE WAFER.END_TIME BETWEEN TO_DATE(:FROMDATE,'YYYYMMDDHH24MISS') AND TO_DATE(:TODATE,'YYYYMMDDHH24MISS'))          \n");
                
                if (rdoMMG.Checked == false)
                    sQuery.Append("                 AND WAFER.TESTAREA= :OPER          \n");
                sQuery.Append("         ) GROUP BY L_LOT_ID, BIN          \n");
                sQuery.Append(" ) BIN_INFO          \n");
                sQuery.Append(" ,MWIPLOTSTS STS          \n");
                sQuery.Append(" ,MWIPMATDEF MAT          \n");
                sQuery.Append(" WHERE 1=1          \n");
                sQuery.Append("     AND TEST_INFO.L_LOT_ID = BIN_INFO.L_LOT_ID          \n");
                sQuery.Append("     AND  TEST_INFO.L_LOT_ID=STS.LOT_ID          \n");
                sQuery.Append("     AND TEST_INFO.DEVICE=MAT.MAT_ID          \n");
                sQuery.Append("     AND MAT.FACTORY= :FACTORY          \n");
                if (string.IsNullOrEmpty(cdvDevice.Text) == false)
                    sQuery.Append("         AND TEST_INFO.CUST_DEVICE = :DEVICE          \n");
                if (string.IsNullOrEmpty(cdvCustomer.Text) == false)
                    sQuery.Append("         AND MAT.CUSTOMER_ID= :CUSTOMER          \n");
                sQuery.Append(" ORDER BY           \n");
                sQuery.Append("     LOT_ID          \n");
                sQuery.Append("     ,BIN          \n");

                //sQuery.Replace(":FACTORY", "'" + StdGlobalVariable.gsReportFactoryPT + "'");
                sQuery.Replace(":FROMDATE", "'" + sFromDateTime.Trim() + "'");
                sQuery.Replace(":TODATE", "'" + sToDateTime.Trim() + "'");
                sQuery.Replace(":CUSTOMER", "'" + cdvCustomer.Text.Trim() + "'");
                sQuery.Replace(":DEVICE", "'" + cdvDevice.Text.Trim() + "'");
                sQuery.Replace(":OPER", "'" + cdvOperation.Text.Trim() + "'");
                sQuery.Replace(":LOT_ID", "'" + txtLotID.Text.Trim() + "'");

                strOper = cdvOperation.Text.Trim();

                return sQuery.ToString();
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

                sQuery.Append(" SELECT            \n");
                sQuery.Append("     TEST_INFO.L_LOT_ID AS LOT_ID          \n");
                sQuery.Append("     ,STS.LOT_CMF_2 AS CUST_LOT_ID          \n");
                sQuery.Append("     ,STS.LOT_CMF_3 AS LOT_SUB          \n");
                sQuery.Append("     ,TEST_INFO.WAFER_NO          \n");
                sQuery.Append("     ,DEVICE          \n");
                sQuery.Append("     ,CUST_DEVICE          \n");
                sQuery.Append("     ,MIN_START_TIME AS START_TIME         \n");
                sQuery.Append("     ,MAX_END_TIME AS END_TIME          \n");
                sQuery.Append("     ,TESTER_ID          \n");
                sQuery.Append("     ,PROBE_CARD          \n");
                sQuery.Append("     ,DECODE(MAT.WEIGHT_NET,' ', 0, ROUND(GOOD_DIE/TO_NUMBER(MAT.WEIGHT_NET) * 100, 2)) AS YIELD          \n");
                sQuery.Append("     ,DECODE(MAT.WEIGHT_NET,' ',0,TO_NUMBER(MAT.WEIGHT_NET)) AS NET_DIE          \n");
                sQuery.Append("     ,TOTAL_DIE          \n");
                sQuery.Append("     ,GOOD_DIE          \n");
                sQuery.Append("     ,BIN          \n");
                if (rdoQty.Checked == true)
                    sQuery.Append("     ,BIN_QTY          \n");
                else
                    sQuery.Append("     ,DECODE(MAT.WEIGHT_NET,' ', 0, ROUND(BIN_QTY/TO_NUMBER(MAT.WEIGHT_NET) * 100, 2)) AS BIN_QTY          \n");
                sQuery.Append(" FROM          \n");
                sQuery.Append(" (          \n");
                sQuery.Append("     SELECT           \n");
                sQuery.Append("         L_LOT_ID          \n");
                sQuery.Append("         ,WAFER_NO          \n");
                sQuery.Append("         ,DEVICE DEVICE          \n");
                sQuery.Append("         ,CUST_DEVICE CUST_DEVICE          \n");
                sQuery.Append("         ,START_TIME MIN_START_TIME          \n");
                sQuery.Append("         ,END_TIME MAX_END_TIME          \n");
                sQuery.Append("         ,TESTER_ID TESTER_ID          \n");
                sQuery.Append("         ,PROBE_CARD_ID PROBE_CARD          \n");
                sQuery.Append("         ,TOTAL_DIE TOTAL_DIE          \n");
                sQuery.Append("         ,GOOD_DIE GOOD_DIE          \n");
                sQuery.Append("     FROM           \n");
                sQuery.Append("         (          \n");
                sQuery.Append("             SELECT           \n");
                sQuery.Append("                 WAFER.LOT_ID L_LOT_ID          \n");
                sQuery.Append("                 ,WAFER.*           \n");
                
                if (rdoMMG.Checked == true)
                    sQuery.Append("             FROM TQC_M_WAFER@MES2YMS WAFER          \n");
                else 
                    sQuery.Append("             FROM TQC_WAFER@MES2YMS WAFER          \n");
                
                sQuery.Append("                 WHERE 1=1          \n");
                sQuery.Append("                     AND WAFER.RETEST_IDX_REV = 0           \n");
                sQuery.Append("                     AND WAFER.LOT_ID =:LOT_ID          \n");
                
                if (rdoMMG.Checked == false)
                    sQuery.Append("                     AND TESTAREA= :OPER          \n");

                sQuery.Append("         )           \n");
                sQuery.Append(" ) TEST_INFO,          \n");
                sQuery.Append(" (          \n");
                sQuery.Append("     SELECT           \n");
                sQuery.Append("         L_LOT_ID          \n");
                sQuery.Append("         ,WAFER_NO          \n");
                sQuery.Append("         ,BIN          \n");
                sQuery.Append("         ,BIN_QTY BIN_QTY          \n");
                sQuery.Append("     FROM           \n");
                sQuery.Append("         (          \n");
                sQuery.Append("             SELECT           \n");
                sQuery.Append("                 WAFER.LOT_ID L_LOT_ID          \n");
                sQuery.Append("                 ,WAFER.WAFER_NO          \n");
                sQuery.Append("                 ,BIN.BIN AS BIN          \n");
                sQuery.Append("                 ,BIN.VALUE AS BIN_QTY          \n");
                if (rdoMMG.Checked == true)
                    sQuery.Append("             FROM TQC_M_WAFER@MES2YMS WAFER, TQP_M_WAFER_SUM_PIVOT@MES2YMS BIN          \n");
                else 
                    sQuery.Append("             FROM TQC_WAFER@MES2YMS WAFER, TQP_WAFER_SUM_PIVOT@MES2YMS BIN          \n");

                sQuery.Append("             WHERE 1=1          \n");
                sQuery.Append("                 AND WAFER.RETEST_IDX_REV = 0           \n");
                if (rdoMMG.Checked == true)
                    sQuery.Append("                 AND BIN.WAFER_ID = WAFER.WAFER_ID           \n");
                else 
                    sQuery.Append("                 AND BIN.WAFER_SEQ = WAFER.WAFER_SEQ           \n");
                sQuery.Append("                 AND WAFER.LOT_ID= :LOT_ID          \n");
                if (rdoMMG.Checked == false)
                    sQuery.Append("                 AND WAFER.TESTAREA= :OPER         \n");
                sQuery.Append("         )          \n");
                sQuery.Append(" ) BIN_INFO          \n");
                sQuery.Append(" ,MWIPLOTSTS STS          \n");
                sQuery.Append(" ,MWIPMATDEF MAT          \n");
                sQuery.Append(" WHERE 1=1          \n");
                sQuery.Append("     AND TEST_INFO.L_LOT_ID = BIN_INFO.L_LOT_ID          \n");
                sQuery.Append("     AND TEST_INFO.WAFER_NO=BIN_INFO.WAFER_NO          \n");
                sQuery.Append("     AND  TEST_INFO.L_LOT_ID=STS.LOT_ID          \n");
                sQuery.Append("     AND TEST_INFO.DEVICE=MAT.MAT_ID          \n");
                sQuery.Append("     AND MAT.FACTORY= :FACTORY          \n");
                sQuery.Append(" ORDER BY           \n");
                sQuery.Append("     LOT_ID          \n");
                sQuery.Append("     ,WAFER_NO          \n");
                sQuery.Append("     ,BIN          \n");

                //sQuery.Replace(":FACTORY", "'" + StdGlobalVariable.gsReportFactoryPT + "'");
                sQuery.Replace(":LOT_ID", "'" + lot_id.Trim() + "'");
                sQuery.Replace(":OPER", "'" + strOper.Trim() + "'");                

                return sQuery.ToString();
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

            //for (int i = 0; i < spdList.Sheets[0].Rows.Count; i++)
            //{

            //    //for (int k = (int)COL_LIST.GROUP_DESC + 1; k < spdHistory.Sheets[0].ColumnHeader.Columns.Count; k++)
            //    //{
            //    //    if (spdHistory.Sheets[0].Cells[i, (int)COL_LIST.GROUP_DESC].Text.Trim() == "인당수/H")
            //    //        spdHistory.Sheets[0].Cells[i, k].CellType = doubleCellType;
            //    //    else
            //    //        spdHistory.Sheets[0].Cells[i, k].CellType = numCellType;
            //    //}
            //}
        }

        #endregion

        #region [ Form Event ]
        private void YLD041001_Load(object sender, EventArgs e)
        {
            InitValueByRegister();
            dtpDateFrom.Value = DateTime.Now.AddDays(-7);
            dtpDateTo.Value = DateTime.Now;
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

        //private void spdList_CellClick(object sender, FarPoint.Win.Spread.CellClickEventArgs e)
        //{
        //    try
        //    {
        //        string QueryCond = null;
        //        DataTable dt = null;
        //        string sLotID = null;
                
        //        string strWaferNo = string.Empty;
        //        int binNo = 0;
        //        int colcnt = 0;
        //        int maxbinno = 0;
        //        int prebinNo = 0;

        //        spdData.Sheets[0].RowCount = 0;

        //        InitSpdColumnHeaderDetail();

        //        // Factory
        //        QueryCond = FwxCmnFunction.PackCondition(QueryCond, txtFactory.Text.Trim());

        //        this.Refresh();
        //        LoadingPopUp.LoadIngPopUpShow(this);

        //        sLotID = spdList.ActiveSheet.Cells[e.Row, (int)COL_LIST.LOT_ID].Text;

        //        dt = CmnFunction.oComm.GetFuncDataTable("DYNAMIC", MakeSqlString1(sLotID));

        //        if (dt == null || dt.Rows.Count <= 0)
        //        {
        //            MessageBox.Show(StdLangFunction.FindMessageLanguage("STD002"), "YLD041001");
        //            spdData.Sheets[0].RowCount = 0;
        //            return;
        //        }

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
        //    }
        //    catch (Exception ex)
        //    {
        //        CmnFunction.ShowMsgBox("YLD041001.spdList_CellClick() : " + ex.ToString());
        //        return;
        //    }
        //    finally
        //    {
        //        LoadingPopUp.LoadingPopUpHidden();
        //    }
        //}

        private void cdvDevice_ButtonPress(object sender, EventArgs e)
        {
            try
            {
                cdvDevice.Init();
                //StdInitFunction.InitListView(cdvDevice.GetListView);
                cdvDevice.Columns.Add("DEVICE", 50, HorizontalAlignment.Left);
                cdvDevice.Columns.Add("DESC", 50, HorizontalAlignment.Left);
                cdvDevice.SelectedSubItemIndex = 0;

                cdvDevice.AddEmptyRow(1);
                //StdListFunction.ViewMaterialList(cdvDevice.GetListView, txtFactory.Text.Trim(), "DEVICE");
                //CusListFunction.GetDeviceList(cdvDevice.GetListView, "CUST_DEVICE", cdvCustomer.Text, StdGlobalVariable.gsReportFactoryPT);

            }
            catch (Exception ex)
            {
                CmnFunction.ShowMsgBox("YLD041001.cdvDevice_ButtonPress() : " + ex.ToString());
            }
        }

        private void cdvCustomer_ButtonPress(object sender, EventArgs e)
        {
            try
            {
                cdvCustomer.Init();
                //StdInitFunction.InitListView(cdvCustomer.GetListView);
                cdvCustomer.Columns.Add("Customer", 50, HorizontalAlignment.Left);
                cdvCustomer.Columns.Add("DESC", 50, HorizontalAlignment.Left);
                cdvCustomer.SelectedSubItemIndex = 0;

                cdvCustomer.AddEmptyRow(1);
                //CusListFunction.CustomerList(cdvCustomer.GetListView, txtFactory.Text.Trim());
            }
            catch (Exception ex)
            {
                CmnFunction.ShowMsgBox("YLD041001.cdvCustomer_ButtonPress() : " + ex.ToString());
            }
        }

        private void cdvOperation_ButtonPress(object sender, EventArgs e)
        {
            cdvOperation.Init();
            //StdInitFunction.InitListView(cdvOperation.GetListView);
            cdvOperation.Columns.Add("KEY_1", 30, HorizontalAlignment.Left);
            cdvOperation.Columns.Add("DATA_1", 70, HorizontalAlignment.Left);
            cdvOperation.SelectedSubItemIndex = 0;
            cdvOperation.DisplaySubItemIndex = 1;

            //StdListFunction.ViewOperList(cdvOperation.GetListView, txtFactory.Text, "");
        }

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
    }
}
