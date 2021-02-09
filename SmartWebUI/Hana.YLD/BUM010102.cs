using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Collections;
using System.Windows.Forms;

using Miracom.UI;
using Miracom.SmartWeb;
using Miracom.SmartWeb.UI;
using Miracom.SmartWeb.FWX;
using Miracom.SmartWeb.UI.Controls;
//using System.Windows.Forms.DataVisualization;
//using System.Windows.Forms.DataVisualization.Charting;
using Microsoft.Win32;

namespace Hana.YLD
{
    public partial class BUM010102 : Miracom.SmartWeb.UI.Controls.udcCUSReport002
    {
        private bool b_load_flag;        
        DACrux.TEST.Control.TPUCGallery oro = null;

        public bool LoadFlag
        {
            get
            {
                return b_load_flag;
            }
            set
            {
                b_load_flag = value;
            }
        }

        public BUM010102()
        {
            InitializeComponent();

            this.SetFactory("HMKB1");
            cdvFactory.Text = "HMKB1";
            cdvOper.sFactory = cdvFactory.txtValue;
            //SortInit();
        }

        #region [ Enum Definition ]
        #region " Constant Definition"

        //DACrux.YMS.Admin.TPUCGallery oro = null;
        //private bool draw_flag;

        private enum COL_WAFER
        {
            SEL,
            WAFER_NO,
            DEVICE_ID,
            WAFER_ID,
            TESTED_DIE,
            GQTY,
            SQTY,
            YIELD,
            BIN

            //SEL,
            //WAFER_NO,
            //WAFER_ID,
            //NET_DIE,
            //GOOD_QTY,
            //YIELD,
            //LOW_YIELD_HOLD,
            //LOW_YIELD_LOSS,
            //BIN_LOW_YIELD,
            //PT2_HYN_LOW_YIELD,
            //WAFER_SEQ,
            //BIN
        }

        #endregion

        #region " Variable Definition "

        private string m_oper_desc = string.Empty;
        private string s_mother_lot = string.Empty;

        private string map_server_ip = string.Empty;
        int map_server_port = 0;

        int Tempindex = -1;
        Color TempColor = Color.White;
        string[] waferList = null;

        #endregion

        #endregion

        #region [ Control Event ]

        #endregion

        #region [ Button Event ]
        private void btnView_Click(object sender, EventArgs e)
        {
            //int i = 0;
            //long tot_wf = 0;
            //long tot_net = 0;
            //long tot_good = 0;

            try
            {
                this.Refresh();
                LoadingPopUp.LoadIngPopUpShow(this);

                if (string.IsNullOrEmpty(txtLotID.Text.Trim()) == true)
                {
                    CmnFunction.ShowMsgBox(RptMessages.GetMessage("STD009", GlobalVariable.gcLanguage));
                    return;
                }

                if (rdoOper.Checked == true && string.IsNullOrEmpty(cdvOper.Text.Trim()) == true)  //cdvOperation.Text.Trim()
                {
                    //Combo Box가 아닌 TextBox에 직접 입력해야 할 수도 있기 때문에 공정을 선택하는 Message이지만 공정을 입력하는 Message로 수정. 
                    CmnFunction.ShowMsgBox(RptMessages.GetMessage("STD064", GlobalVariable.gcLanguage));
                    return;
                }

                spdWafBin.ActiveSheet.RowCount = 0;

                ViewWaferResultYMS();

                Tempindex = -1;
                TempColor = Color.White;


                //View_Wafer_Result_YMS();

                //if (spdWafBin.ActiveSheet.RowCount == 0)
                //{
                //    for (i = 0; i < 25; i++)
                //    {
                //        spdWafBin.ActiveSheet.RowCount++;
                //        spdWafBin.ActiveSheet.Cells[i, (int)COL_WAFER.WAFER_NO].Value = (i + 1).ToString().PadLeft(2, '0');
                //    }
                //}

                //tot_net = 0;
                //tot_good = 0;
                //for (i = 0; i < 25; i++)
                //{
                //    if (string.IsNullOrEmpty(spdWafBin.ActiveSheet.Cells[i, (int)COL_WAFER.NET_DIE].Value.ToString()) == false && spdWafBin.ActiveSheet.Cells[i, (int)COL_WAFER.NET_DIE].Value.ToString() != " ")
                //    {
                //        if (string.IsNullOrEmpty(spdWafBin.ActiveSheet.Cells[i, (int)COL_WAFER.NET_DIE].Value.ToString()) == false)
                //        {
                //            tot_wf++;
                //            tot_net = tot_net + Convert.ToInt32(spdWafBin.ActiveSheet.Cells[i, (int)COL_WAFER.NET_DIE].Value);
                //        }
                //    }

                //    if (string.IsNullOrEmpty(spdWafBin.ActiveSheet.Cells[i, (int)COL_WAFER.GOOD_QTY].Value.ToString()) == false && spdWafBin.ActiveSheet.Cells[i, (int)COL_WAFER.GOOD_QTY].Value.ToString() != " ")
                //    {
                //        if (string.IsNullOrEmpty(spdWafBin.ActiveSheet.Cells[i, (int)COL_WAFER.GOOD_QTY].Value.ToString()) == false)
                //        {
                //            tot_good = tot_good + Convert.ToInt32(spdWafBin.ActiveSheet.Cells[i, (int)COL_WAFER.GOOD_QTY].Value);
                //        }
                //    }
                //}
                //spdWafBin.ActiveSheet.RowCount++;
                //spdWafBin.ActiveSheet.Cells[spdWafBin.ActiveSheet.RowCount - 1, (int)COL_WAFER.WAFER_NO].Value = tot_wf.ToString();
                //spdWafBin.ActiveSheet.Cells[spdWafBin.ActiveSheet.RowCount - 1, (int)COL_WAFER.WAFER_NO].Tag = "LOT";
                //spdWafBin.ActiveSheet.Cells[spdWafBin.ActiveSheet.RowCount - 1, (int)COL_WAFER.NET_DIE].Text = tot_net.ToString();
                //spdWafBin.ActiveSheet.Cells[spdWafBin.ActiveSheet.RowCount - 1, (int)COL_WAFER.GOOD_QTY].Text = tot_good.ToString();
                //spdWafBin.ActiveSheet.Cells[spdWafBin.ActiveSheet.RowCount - 1, (int)COL_WAFER.YIELD].Text = Math.Round(((double)tot_good / (double)tot_net) * 100, 2).ToString();

                //spdWafBin.ActiveSheet.Rows[spdWafBin.ActiveSheet.RowCount - 1].BackColor = Color.LightYellow;

                ////spdWafBin.ActiveSheet.OperationMode = FarPoint.Win.Spread.OperationMode.SingleSelect;
                ////StdFunction.FitColumnHeader(spdWafBin);
                //CmnFunction.FitColumnHeader(spdWafBin);

            }
            catch (Exception ex)
            {
                CmnFunction.ShowMsgBox("YLD041002.btnView_Click() : " + ex.ToString());
            }
            finally
            {
                LoadingPopUp.LoadingPopUpHidden();
            }
        }

        private void btnExcelExport_Click(object sender, EventArgs e)
        {

        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            string dpLotStatus = string.Empty;
            string dpQtyUnit = string.Empty;

            try
            {
                //StdGlobalVariable.gaSelectLot_ID.Clear();
                //StdGlobalVariable.gsCurrentLot_ID = "";

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
        /// <summary>
        /// Spread Column을 초기화합니다.
        /// </summary>
        private void InitSpdColumnHeader()
        {
            ////spdWafBin.ActiveSheet.ColumnHeader.Rows.Count = 0;            
            ////spdWafBin.RPT_InitSpread(true, true, FarPoint.Win.Spread.OperationMode.Normal);
            ////spdWafBin.Sheets[0].RowHeader.ColumnCount = 0;
            ////spdWafBin.RPT_InitColumn();


            //spdWafBin.ActiveSheet.RowHeader.ColumnCount = 0;
            //spdWafBin.RPT_ColumnInit();
            // Group 정보 추가할 경우 로직 수정 필요
            //spdWafBin.RPT_AddBasicColumn("Customer", 0, 0, Visibles.True, Frozen.True, Align.Center, Merge.True, Formatter.String, 70);
            //spdWafBin.RPT_AddBasicColumn("Bumping Type", 0, 1, Visibles.True, Frozen.True, Align.Center, Merge.True, Formatter.String, 70);
            //spdWafBin.RPT_AddBasicColumn("Operation Flow", 0, 2, Visibles.False, Frozen.True, Align.Left, Merge.True, Formatter.String, 70);
            //spdWafBin.RPT_AddBasicColumn("Layer classification", 0, 3, Visibles.False, Frozen.True, Align.Center, Merge.True, Formatter.String, 70);
            //spdWafBin.RPT_AddBasicColumn("PKG Type", 0, 4, Visibles.False, Frozen.True, Align.Center, Merge.True, Formatter.String, 50);
            //spdWafBin.RPT_AddBasicColumn("RDL Plating", 0, 5, Visibles.False, Frozen.True, Align.Center, Merge.True, Formatter.String, 50);
            //spdWafBin.RPT_AddBasicColumn("Final Bump", 0, 6, Visibles.False, Frozen.True, Align.Center, Merge.True, Formatter.String, 50);
            //spdWafBin.RPT_AddBasicColumn("Sub. Material", 0, 7, Visibles.False, Frozen.True, Align.Center, Merge.True, Formatter.String, 50);
            //spdWafBin.RPT_AddBasicColumn("Size", 0, 8, Visibles.False, Frozen.True, Align.Center, Merge.True, Formatter.String, 50);
            //spdWafBin.RPT_AddBasicColumn("Thickness", 0, 9, Visibles.False, Frozen.True, Align.Center, Merge.True, Formatter.String, 50);
            //spdWafBin.RPT_AddBasicColumn("Flat Type", 0, 10, Visibles.False, Frozen.True, Align.Center, Merge.True, Formatter.String, 50);
            //spdWafBin.RPT_AddBasicColumn("Wafer Orientation", 0, 11, Visibles.False, Frozen.True, Align.Center, Merge.True, Formatter.String, 50);
            
            //Sel
            //spdWafBin.RPT_AddBasicColumn("No", 0, (int)COL_WAFER.WAFER_NO, Visibles.True, Frozen.False, Align.Left, Merge.False, Formatter.Number, 50);
            //spdWafBin.RPT_AddBasicColumn("Device", 0, (int)COL_WAFER.DEVICE_ID, Visibles.True, Frozen.False, Align.Left, Merge.False, Formatter.String, 80);
            //spdWafBin.RPT_AddBasicColumn("Wafer ID", 0, (int)COL_WAFER.WAFER_ID, Visibles.True, Frozen.False, Align.Left, Merge.False, Formatter.String, 100);
            //spdWafBin.RPT_AddBasicColumn("T Qty", 0, (int)COL_WAFER.TESTED_DIE, Visibles.True, Frozen.False, Align.Left, Merge.False, Formatter.String, 60);
            //spdWafBin.RPT_AddBasicColumn("G Qty", 0, (int)COL_WAFER.GQTY, Visibles.True, Frozen.False, Align.Left, Merge.False, Formatter.String, 60);
            //spdWafBin.RPT_AddBasicColumn("F Qty", 0, (int)COL_WAFER.SQTY, Visibles.True, Frozen.False, Align.Left, Merge.False, Formatter.String, 60);
            //spdWafBin.RPT_AddBasicColumn("Yield", 0, (int)COL_WAFER.YIELD, Visibles.True, Frozen.False, Align.Left, Merge.False, Formatter.String, 60);


            //기존
            //spdWafBin.RPT_AddBasicColumn("Sel.", 0, (int)COL_WAFER.SEL, Visibles.True, Frozen.False, Align.Center, Merge.False, Formatter.ChkType, 50);
            //spdWafBin.RPT_AddBasicColumn("Wf No.", 0, (int)COL_WAFER.WAFER_NO, Visibles.True, Frozen.False, Align.Left, Merge.False, Formatter.Number, 50);
            //spdWafBin.RPT_AddBasicColumn("Wf ID.", 0, (int)COL_WAFER.WAFER_ID, Visibles.False, Frozen.False, Align.Left, Merge.False, Formatter.String, 100);
            //spdWafBin.RPT_AddBasicColumn("Net Die", 0, (int)COL_WAFER.NET_DIE, Visibles.True, Frozen.False, Align.Left, Merge.False, Formatter.String, 80);
            //spdWafBin.RPT_AddBasicColumn("Good Qty", 0, (int)COL_WAFER.GOOD_QTY, Visibles.True, Frozen.False, Align.Left, Merge.False, Formatter.String, 60);
            //spdWafBin.RPT_AddBasicColumn("Yield", 0, (int)COL_WAFER.YIELD, Visibles.True, Frozen.False, Align.Left, Merge.False, Formatter.String, 80);
            //spdWafBin.RPT_AddBasicColumn("Low Yield(Hold)", 0, (int)COL_WAFER.LOW_YIELD_HOLD, Visibles.False, Frozen.False, Align.Left, Merge.False, Formatter.String, 80);
            //spdWafBin.RPT_AddBasicColumn("Low Yield(Scrap)", 0, (int)COL_WAFER.LOW_YIELD_LOSS, Visibles.False, Frozen.False, Align.Left, Merge.False, Formatter.String, 60);
            //spdWafBin.RPT_AddBasicColumn("Low Yield(BIN)", 0, (int)COL_WAFER.BIN_LOW_YIELD, Visibles.False, Frozen.False, Align.Left, Merge.False, Formatter.String, 60);
            //spdWafBin.RPT_AddBasicColumn("Low Yield(HYN PT2)", 0, (int)COL_WAFER.PT2_HYN_LOW_YIELD, Visibles.False, Frozen.False, Align.Left, Merge.False, Formatter.String, 60);
            //spdWafBin.RPT_AddBasicColumn("Wafer Seq.", 0, (int)COL_WAFER.WAFER_SEQ, Visibles.True, Frozen.False, Align.Left, Merge.False, Formatter.String, 60);
            //spdWafBin.RPT_SetCellsType();

            //for (int i = 1; i < spdWafBin.ActiveSheet.ColumnCount; i++)
            //{
            //    spdWafBin.Sheets[0].Columns[i].Locked = true;
            //}
        }

        // Map Sever Info
        private bool ViewMapServerInfo(string table, string key1)
        {
            DataTable dtGCMTBL = null;
            DataTable dtGCMDAT = null;
            StringBuilder sQuery = new StringBuilder();
            StringBuilder sQuery2 = new StringBuilder();

            try
            {
                sQuery = new StringBuilder();
                sQuery.AppendLine("SELECT *  ");                
                sQuery.AppendLine("FROM MGCMTBLDEF  ");
                sQuery.AppendLine("WHERE FACTORY = :FACTORY ");
                sQuery.AppendLine("AND TABLE_NAME = :TABLE_NAME ");

                //sQuery.Replace(":FACTORY", "'HMKB1'");
                sQuery.Replace(":FACTORY", "'" + cdvFactory.Text + "'");
                sQuery.Replace(":TABLE_NAME", "'" + table + "'");
                dtGCMTBL = CmnFunction.oComm.GetFuncDataTable("DYNAMIC", sQuery.ToString());

                if (dtGCMTBL == null || dtGCMTBL.Rows.Count == 0)
                {
                    return false;
                }

                if (dtGCMTBL.Rows.Count > 0)
                {                    
                    sQuery2 = new StringBuilder();
                    sQuery2.AppendLine("SELECT  *  ");                    
                    sQuery2.AppendLine("FROM MGCMTBLDAT  ");
                    sQuery2.AppendLine("WHERE FACTORY = :FACTORY ");
                    sQuery2.AppendLine("AND TABLE_NAME = :TABLE_NAME ");
                    sQuery2.AppendLine("AND KEY_1 = :KEY_1 ");

                    sQuery2.Replace(":FACTORY", "'" + cdvFactory.Text + "'");
                    sQuery2.Replace(":TABLE_NAME", "'" + table + "'");
                    sQuery2.Replace(":KEY_1", "'" + key1 + "'");
                    dtGCMDAT = CmnFunction.oComm.GetFuncDataTable("DYNAMIC", sQuery2.ToString());

                    if (dtGCMDAT == null)
                    {
                        return false;
                    }

                    if (dtGCMDAT.Rows.Count > 0)
                    {
                        map_server_ip = dtGCMDAT.Rows[0]["DATA_1"].ToString();
                        map_server_port = Convert.ToInt32(dtGCMDAT.Rows[0]["DATA_2"]);
                    }
                }

                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void ViewWaferResultYMS()
        {
            DataTable dtLotSTS = null;
            DataTable dtOprDEF = null;
            DataTable dtMatDEF = null;
            DataTable dtSltSTS = null;
            DataTable dtWfrYLD = null;
            StringBuilder sLotStsQuery = new StringBuilder();
            StringBuilder sOprDefQuery = new StringBuilder();
            StringBuilder sMatDefQuery = new StringBuilder();
            StringBuilder sQuery = new StringBuilder();
            StringBuilder sQuery2 = new StringBuilder();
            int iRow = 0;
            double tmp = 0;

            try
            {
                spdWafBin.ActiveSheet.Rows.Count = 0;

                // MWIPLOTSTS@TESTMES   //RWIPLOTSTS?
                sLotStsQuery = new StringBuilder();
                //sLotStsQuery.AppendLine("SELECT  * ");
                //sLotStsQuery.AppendLine("FROM MWIPLOTSTS@TESTMES ");
                //sLotStsQuery.AppendLine("WHERE LOT_ID = :LOT_ID ");

                sLotStsQuery.AppendLine("SELECT  * ");
                sLotStsQuery.AppendLine("FROM MWIPLOTSTS@RPTTOMES LOT, MWIPMATDEF MAT ");
                sLotStsQuery.AppendLine("WHERE 1=1 ");
                sLotStsQuery.AppendLine("AND LOT.FACTORY = MAT.FACTORY  ");
                sLotStsQuery.AppendLine("AND LOT.MAT_ID = MAT.MAT_ID  ");
                sLotStsQuery.AppendLine("AND LOT_ID = :LOT_ID ");
                #region 상세 조회에 따른 SQL문 생성
                if (udcBUMPCondition1.Text != "ALL" && udcBUMPCondition1.Text != "")
                    sLotStsQuery.AppendFormat("   AND MAT.MAT_GRP_1 {0} " + "\n", udcBUMPCondition1.SelectedValueToQueryString);

                if (udcBUMPCondition2.Text != "ALL" && udcBUMPCondition2.Text != "")
                    sLotStsQuery.AppendFormat("   AND MAT.MAT_GRP_2 {0} " + "\n", udcBUMPCondition2.SelectedValueToQueryString);

                if (udcBUMPCondition3.Text != "ALL" && udcBUMPCondition3.Text != "")
                    sLotStsQuery.AppendFormat("   AND MAT.MAT_GRP_3 {0} " + "\n", udcBUMPCondition3.SelectedValueToQueryString);

                if (udcBUMPCondition4.Text != "ALL" && udcBUMPCondition4.Text != "")
                    sLotStsQuery.AppendFormat("   AND MAT.MAT_GRP_4 {0} " + "\n", udcBUMPCondition4.SelectedValueToQueryString);

                if (udcBUMPCondition5.Text != "ALL" && udcBUMPCondition5.Text != "")
                    sLotStsQuery.AppendFormat("   AND MAT.MAT_GRP_5 {0} " + "\n", udcBUMPCondition5.SelectedValueToQueryString);

                if (udcBUMPCondition6.Text != "ALL" && udcBUMPCondition6.Text != "")
                    sLotStsQuery.AppendFormat("   AND MAT.MAT_GRP_6 {0} " + "\n", udcBUMPCondition6.SelectedValueToQueryString);

                if (udcBUMPCondition7.Text != "ALL" && udcBUMPCondition7.Text != "")
                    sLotStsQuery.AppendFormat("   AND MAT.MAT_GRP_7 {0} " + "\n", udcBUMPCondition7.SelectedValueToQueryString);

                if (udcBUMPCondition8.Text != "ALL" && udcBUMPCondition8.Text != "")
                    sLotStsQuery.AppendFormat("   AND MAT.MAT_GRP_8 {0} " + "\n", udcBUMPCondition8.SelectedValueToQueryString);

                if (udcBUMPCondition9.Text != "ALL" && udcBUMPCondition9.Text != "")
                    sLotStsQuery.AppendFormat("   AND MAT.MAT_CMF_14 {0} " + "\n", udcBUMPCondition9.SelectedValueToQueryString);

                if (udcBUMPCondition10.Text != "ALL" && udcBUMPCondition10.Text != "")
                    sLotStsQuery.AppendFormat("   AND MAT.MAT_CMF_2 {0} " + "\n", udcBUMPCondition10.SelectedValueToQueryString);

                if (udcBUMPCondition11.Text != "ALL" && udcBUMPCondition11.Text != "")
                    sLotStsQuery.AppendFormat("   AND MAT.MAT_CMF_3 {0} " + "\n", udcBUMPCondition11.SelectedValueToQueryString);

                if (udcBUMPCondition12.Text != "ALL" && udcBUMPCondition12.Text != "")
                    sLotStsQuery.AppendFormat("   AND MAT.MAT_CMF_4 {0} " + "\n", udcBUMPCondition12.SelectedValueToQueryString);
                #endregion

                sLotStsQuery.Replace(":LOT_ID", "'" + txtLotID.Text + "'");
                dtLotSTS = CmnFunction.oComm.GetFuncDataTable("DYNAMIC", sLotStsQuery.ToString());
                                
                // LOT 정보 있을 경우...
                if (dtLotSTS.Rows.Count > 0)
                {
                    // MWIPOPRDEF
                    sOprDefQuery = new StringBuilder();
                    sOprDefQuery.AppendLine("SELECT  *  ");                    
                    sOprDefQuery.AppendLine("FROM MWIPOPRDEF   ");
                    sOprDefQuery.AppendLine("WHERE 1=1 ");
                    sOprDefQuery.AppendLine("AND FACTORY = :FACTORY ");
                    sOprDefQuery.AppendLine("AND OPER = :OPER ");

                    sOprDefQuery.Replace(":FACTORY", "'" + dtLotSTS.Rows[0]["FACTORY"] + "'");
                    sOprDefQuery.Replace(":OPER", "'" + dtLotSTS.Rows[0]["OPER"] + "'");
                    dtOprDEF = CmnFunction.oComm.GetFuncDataTable("DYNAMIC", sOprDefQuery.ToString());

                    if (dtOprDEF.Rows.Count > 0)
                    {
                        m_oper_desc = dtOprDEF.Rows[0]["OPER_DESC"].ToString();
                    }

                    // MWIPMATDEF
                    sMatDefQuery = new StringBuilder();
                    sMatDefQuery.AppendLine("SELECT  *  ");                    
                    sMatDefQuery.AppendLine("FROM MWIPMATDEF ");
                    sMatDefQuery.AppendLine("WHERE 1=1 ");
                    sMatDefQuery.AppendLine("AND FACTORY = :FACTORY ");
                    sMatDefQuery.AppendLine("AND MAT_ID = :MAT_ID ");
                    sMatDefQuery.AppendLine("AND MAT_VER = " + dtLotSTS.Rows[0]["MAT_VER"]);

                    sMatDefQuery.Replace(":FACTORY", "'" + dtLotSTS.Rows[0]["FACTORY"] + "'");
                    sMatDefQuery.Replace(":MAT_ID", "'" + dtLotSTS.Rows[0]["MAT_ID"] + "'");
                    dtMatDEF = CmnFunction.oComm.GetFuncDataTable("DYNAMIC", sMatDefQuery.ToString());

                    // MWIPSLTSTS@TESTMES
                    sQuery = new StringBuilder();
                    sQuery.AppendLine("SELECT ");
                    sQuery.AppendLine("    *  ");
                    sQuery.AppendLine("FROM MWIPSLTSTS@RPTTOMES ");
                    sQuery.AppendLine("WHERE 1=1 ");
                    //sQuery.AppendLine("AND FACTORY = :FACTORY ");
                    sQuery.AppendLine("AND LOT_ID = :LOT_ID ");
                    sQuery.AppendLine("AND SUBLOT_DEL_FLAG = ' ' ");
                    sQuery.AppendLine("ORDER BY SLOT_NO ");

                    //sQuery.Replace(":FACTORY", "'" + cdvFactory.Text + "'");
                    sQuery.Replace(":LOT_ID", "'" + txtLotID.Text + "'");
                    dtSltSTS = CmnFunction.oComm.GetFuncDataTable("DYNAMIC", sQuery.ToString());

                    if (dtSltSTS == null)
                    {
                        return;
                    }

                    if (dtSltSTS.Rows.Count > 0)
                    {
                        for (int i = 0; i < dtSltSTS.Rows.Count; i++)
                        {
                            // VYMSWFRYLD@TESTMES
                            sQuery2 = new StringBuilder();
                            //sQuery2.AppendLine("SELECT  *  ");                            
                            //sQuery2.AppendLine("FROM VYMSWFRYLD@TESTMES ");
                            //sQuery2.AppendLine("WHERE 1=1 ");
                            //sQuery2.AppendLine("AND WAFER_ID = '" + dtSltSTS.Rows[i]["SUBLOT_ID"] + "'");
                            //sQuery2.AppendLine("AND WAFER_SEQ = (SELECT MAX(WAFER_SEQ) FROM VYMSWFRYLD@TESTMES WHERE WAFER_ID = '" + dtSltSTS.Rows[i]["SUBLOT_ID"] + "')");

                            sQuery2.AppendLine("SELECT ");
                            sQuery2.AppendLine("    YLD.*  ");
                            sQuery2.AppendLine("FROM VYMSWFRYLD@RPTTOMES YLD, MWIPMATDEF MAT ");
                            sQuery2.AppendLine("WHERE 1=1 ");
                            sQuery2.AppendLine("AND YLD.PRODUCT = MAT.MAT_ID  ");
                            sQuery2.AppendLine("AND YLD.WAFER_ID = '" + dtSltSTS.Rows[i]["SUBLOT_ID"] + "'");
                            sQuery2.AppendLine("AND YLD.WAFER_SEQ = (SELECT MAX(WAFER_SEQ) FROM VYMSWFRYLD@RPTTOMES WHERE WAFER_ID = '" + dtSltSTS.Rows[i]["SUBLOT_ID"] + "')");
                            sQuery2.AppendLine("AND MAT.FACTORY = :FACTORY   ");
                            sQuery2.AppendLine("AND MAT.MAT_VER = 1  ");

                            #region 상세 조회에 따른 SQL문 생성
                            if (udcBUMPCondition1.Text != "ALL" && udcBUMPCondition1.Text != "")
                                sQuery2.AppendFormat("   AND MAT.MAT_GRP_1 {0} " + "\n", udcBUMPCondition1.SelectedValueToQueryString);

                            if (udcBUMPCondition2.Text != "ALL" && udcBUMPCondition2.Text != "")
                                sQuery2.AppendFormat("   AND MAT.MAT_GRP_2 {0} " + "\n", udcBUMPCondition2.SelectedValueToQueryString);

                            if (udcBUMPCondition3.Text != "ALL" && udcBUMPCondition3.Text != "")
                                sQuery2.AppendFormat("   AND MAT.MAT_GRP_3 {0} " + "\n", udcBUMPCondition3.SelectedValueToQueryString);

                            if (udcBUMPCondition4.Text != "ALL" && udcBUMPCondition4.Text != "")
                                sQuery2.AppendFormat("   AND MAT.MAT_GRP_4 {0} " + "\n", udcBUMPCondition4.SelectedValueToQueryString);

                            if (udcBUMPCondition5.Text != "ALL" && udcBUMPCondition5.Text != "")
                                sQuery2.AppendFormat("   AND MAT.MAT_GRP_5 {0} " + "\n", udcBUMPCondition5.SelectedValueToQueryString);

                            if (udcBUMPCondition6.Text != "ALL" && udcBUMPCondition6.Text != "")
                                sQuery2.AppendFormat("   AND MAT.MAT_GRP_6 {0} " + "\n", udcBUMPCondition6.SelectedValueToQueryString);

                            if (udcBUMPCondition7.Text != "ALL" && udcBUMPCondition7.Text != "")
                                sQuery2.AppendFormat("   AND MAT.MAT_GRP_7 {0} " + "\n", udcBUMPCondition7.SelectedValueToQueryString);

                            if (udcBUMPCondition8.Text != "ALL" && udcBUMPCondition8.Text != "")
                                sQuery2.AppendFormat("   AND MAT.MAT_GRP_8 {0} " + "\n", udcBUMPCondition8.SelectedValueToQueryString);

                            if (udcBUMPCondition9.Text != "ALL" && udcBUMPCondition9.Text != "")
                                sQuery2.AppendFormat("   AND MAT.MAT_CMF_14 {0} " + "\n", udcBUMPCondition9.SelectedValueToQueryString);

                            if (udcBUMPCondition10.Text != "ALL" && udcBUMPCondition10.Text != "")
                                sQuery2.AppendFormat("   AND MAT.MAT_CMF_2 {0} " + "\n", udcBUMPCondition10.SelectedValueToQueryString);

                            if (udcBUMPCondition11.Text != "ALL" && udcBUMPCondition11.Text != "")
                                sQuery2.AppendFormat("   AND MAT.MAT_CMF_3 {0} " + "\n", udcBUMPCondition11.SelectedValueToQueryString);

                            if (udcBUMPCondition12.Text != "ALL" && udcBUMPCondition12.Text != "")
                                sQuery2.AppendFormat("   AND MAT.MAT_CMF_4 {0} " + "\n", udcBUMPCondition12.SelectedValueToQueryString);
                            #endregion

                            sQuery2.Replace(":FACTORY", "'" + cdvFactory.Text + "'");
                            dtWfrYLD = CmnFunction.oComm.GetFuncDataTable("DYNAMIC", sQuery2.ToString());


                            iRow = spdWafBin.ActiveSheet.RowCount;
                            spdWafBin.ActiveSheet.RowCount++;

                            spdWafBin.ActiveSheet.Cells[iRow, (int)COL_WAFER.WAFER_NO].Value = dtSltSTS.Rows[i]["SLOT_NO"].ToString();
                            spdWafBin.ActiveSheet.Cells[iRow, (int)COL_WAFER.DEVICE_ID].Value = dtSltSTS.Rows[i]["MAT_ID"];
                            

                            if (dtWfrYLD.Rows.Count > 0)
                            {//"DATA_TYPE" == "YMS"

                                spdWafBin.ActiveSheet.Cells[iRow, (int)COL_WAFER.WAFER_ID].Value = dtWfrYLD.Rows[0]["WAFER_ID"];
                                spdWafBin.ActiveSheet.Cells[iRow, (int)COL_WAFER.TESTED_DIE].Value = dtWfrYLD.Rows[0]["NETDIE"].ToString();
                                spdWafBin.ActiveSheet.Cells[iRow, (int)COL_WAFER.GQTY].Value = (Convert.ToInt32(dtWfrYLD.Rows[0]["NETDIE"]) - Convert.ToInt32(dtWfrYLD.Rows[0]["LOSS_DIE"])).ToString();
                                //spdWafBin.ActiveSheet.Cells[iRow, (int)COL_WAFER.TESTED_DIE].Value = dtWfrYLD.Rows[0]["TESTED_DIE"].ToString();
                                //spdWafBin.ActiveSheet.Cells[iRow, (int)COL_WAFER.GQTY].Value = dtWfrYLD.Rows[0]["GEC"].ToString();
                                
                                spdWafBin.ActiveSheet.Cells[iRow, (int)COL_WAFER.SQTY].Value = dtWfrYLD.Rows[0]["LOSS_DIE"].ToString();
                                spdWafBin.ActiveSheet.Cells[iRow, (int)COL_WAFER.YIELD].Value = dtWfrYLD.Rows[0]["YIELD"].ToString();
                            }
                            else
                            {//"DATA_TYPE" == "MES"

                                spdWafBin.ActiveSheet.Cells[iRow, (int)COL_WAFER.WAFER_ID].Value = dtSltSTS.Rows[i]["SUBLOT_ID"];
                                
                                if (dtMatDEF.Rows[0]["MAT_CMF_7"].ToString() != "")
                                {
                                    if (int.Parse(dtMatDEF.Rows[0]["MAT_CMF_7"].ToString()) > 0)
                                    {
                                        spdWafBin.ActiveSheet.Cells[iRow, (int)COL_WAFER.GQTY].Value = dtSltSTS.Rows[i]["QTY_2"];
                                        tmp = Convert.ToDouble(dtSltSTS.Rows[i]["QTY_2"]) / Convert.ToDouble(dtMatDEF.Rows[0]["MAT_CMF_7"]);
                                        spdWafBin.ActiveSheet.Cells[iRow, (int)COL_WAFER.YIELD].Value = tmp * 100;
                                    }
                                }

                                spdWafBin.ActiveSheet.Cells[iRow, (int)COL_WAFER.SQTY].Value = 0;
                            }

                        }
                        //WaferList(dtSLTSTS, dtWFRYLD);
                    }

                }

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        

        //private bool View_Wafer_Result_YMS()
        //{
        //    DataTable dt = null;
        //    StringBuilder sQuery = new StringBuilder();
        //    StringBuilder sBinList = new StringBuilder();
        //    StringBuilder sBinList2 = new StringBuilder();
        //    StringBuilder sBinList3 = new StringBuilder();
        //    StringBuilder sBinList4 = new StringBuilder();
        //    //TRSNode in_node = new TRSNode("IN");
        //    //TRSNode out_node = new TRSNode("OUT");
        //    int i = 0;
        //    int j = 0;

        //    try
        //    {
        //        spdWafBin.ActiveSheet.Rows.Count = 0;

        //        //Wafer List Result
        //        if (rdoMMG.Checked == false)
        //        {
        //            sQuery = new StringBuilder();
        //            sQuery.AppendLine("SELECT");
        //            sQuery.AppendLine("    ' ' SEL");
        //            sQuery.AppendLine("    ,LST.WAFER_NO");
        //            sQuery.AppendLine("    ,WAF.WAFER_ID");
        //            sQuery.AppendLine("    ,NVL(WAF.NETDIE,' ') ");
        //            sQuery.AppendLine("    ,NVL(WAF.GOOD_QTY,0) AS GOOD_QTY");
        //            sQuery.AppendLine("    ,NVL(YIELD, 0)");
        //            sQuery.AppendLine("    ,' ' AS LOW_YIELD_HOLD");
        //            sQuery.AppendLine("    ,' ' AS LOW_YIELD_LOSS");
        //            sQuery.AppendLine("    ,' ' AS PT2_HYN_LOW_YIELD");
        //            sQuery.AppendLine("    ,' ' AS BIN_LOW_YIELD");
        //            sQuery.AppendLine("    ,NVL(WAF.WAFER_SEQ,0) AS WAFER_SEQ");
        //            sQuery.AppendLine("FROM");
        //            sQuery.AppendLine("(");
        //            sQuery.AppendLine("    SELECT");
        //            sQuery.AppendLine("        WAFHIS.WAFER_NO");
        //            sQuery.AppendLine("        ,WAFHIS.WAFER_ID");
        //            sQuery.AppendLine("        ,LOTSTS.LOT_CMF_8 AS NETDIE");
        //            sQuery.AppendLine("        ,ROUND(WAFHIS.GOOD_QTY/TO_NUMBER(LOTSTS.LOT_CMF_8)*100, 2) AS YIELD");
        //            sQuery.AppendLine("        ,' '");
        //            sQuery.AppendLine("        ,' '");
        //            sQuery.AppendLine("        ,' '");
        //            sQuery.AppendLine("        ,' '");
        //            sQuery.AppendLine("        ,NVL(WAFHIS.WAFER_SEQ,0) AS WAFER_SEQ");
        //            sQuery.AppendLine("        ,NVL(WAFHIS.GOOD_QTY,0) AS GOOD_QTY");
        //            sQuery.AppendLine("    FROM VYMSWAFHIS WAFHIS, MWIPLOTSTS LOTSTS");
        //            sQuery.AppendLine("    WHERE 1=1");
        //            sQuery.AppendLine("        AND WAFHIS.WAFER_ID IN (SELECT WAFER_ID FROM CWIPLOTWAF WHERE LOT_ID=:LOT_ID)");
        //            sQuery.AppendLine("        AND WAFHIS.OPER=:OPER");
        //            sQuery.AppendLine("        AND WAFHIS.RETEST_IDX_REV=0");
        //            sQuery.AppendLine("        AND WAFHIS.LOT_ID=LOTSTS.LOT_ID");
        //            sQuery.AppendLine(")WAF, VTOTWAFLST LST");
        //            sQuery.AppendLine("WHERE 1=1");
        //            sQuery.AppendLine("    AND LST.WAFER_NO=WAF.WAFER_NO(+)");
        //            sQuery.AppendLine("ORDER BY LST.WAFER_NO");
        //        }
        //        else
        //        {
        //            sQuery = new StringBuilder();
        //            sQuery.AppendLine("SELECT");
        //            sQuery.AppendLine("    ' ' SEL");
        //            sQuery.AppendLine("    ,LST.WAFER_NO");
        //            sQuery.AppendLine("    ,WAF.WAFER_ID");
        //            sQuery.AppendLine("    ,NVL(WAF.NETDIE,' ') ");
        //            sQuery.AppendLine("    ,NVL(WAF.GOOD_QTY,0) AS GOOD_QTY");
        //            sQuery.AppendLine("    ,NVL(YIELD, 0)");
        //            sQuery.AppendLine("    ,' ' AS LOW_YIELD_HOLD");
        //            sQuery.AppendLine("    ,' ' AS LOW_YIELD_LOSS");
        //            sQuery.AppendLine("    ,' ' AS PT2_HYN_LOW_YIELD");
        //            sQuery.AppendLine("    ,' ' AS BIN_LOW_YIELD");
        //            sQuery.AppendLine("    ,NVL(WAF.WAFER_SEQ,0) AS WAFER_SEQ");
        //            sQuery.AppendLine("FROM");
        //            sQuery.AppendLine("(");
        //            sQuery.AppendLine("    SELECT");
        //            sQuery.AppendLine("        WAFHIS.WAFER_NO");
        //            sQuery.AppendLine("        ,WAFHIS.WAFER_ID");
        //            sQuery.AppendLine("        ,LOTSTS.LOT_CMF_8 AS NETDIE");
        //            sQuery.AppendLine("        ,ROUND(WAFHIS.GOOD_QTY/TO_NUMBER(LOTSTS.LOT_CMF_8)*100, 2) AS YIELD");
        //            sQuery.AppendLine("        ,' '");
        //            sQuery.AppendLine("        ,' '");
        //            sQuery.AppendLine("        ,' '");
        //            sQuery.AppendLine("        ,' '");
        //            sQuery.AppendLine("        ,NVL(WAFHIS.WAFER_SEQ,0) AS WAFER_SEQ");
        //            sQuery.AppendLine("        ,NVL(WAFHIS.GOOD_QTY,0) AS GOOD_QTY");
        //            sQuery.AppendLine("    FROM VYMSWAFMMG WAFHIS, MWIPLOTSTS LOTSTS");
        //            sQuery.AppendLine("    WHERE 1=1");
        //            sQuery.AppendLine("        AND WAFHIS.WAFER_ID IN (SELECT WAFER_ID FROM CWIPLOTWAF WHERE LOT_ID=:LOT_ID)");
        //            sQuery.AppendLine("        AND WAFHIS.LOT_ID=LOTSTS.LOT_ID");
        //            sQuery.AppendLine(")WAF, VTOTWAFLST LST");
        //            sQuery.AppendLine("WHERE 1=1");
        //            sQuery.AppendLine("    AND LST.WAFER_NO=WAF.WAFER_NO(+)");
        //            sQuery.AppendLine("ORDER BY LST.WAFER_NO");
        //        }

        //        sQuery.Replace(":LOT_ID", "'" + txtLotID.Text + "'");
        //        sQuery.Replace(":OPER", "'" + cdvOperation.Text + "'");

        //        dt = CmnFunction.oComm.GetFuncDataTable("DYNAMIC", sQuery.ToString());

        //        if (dt == null)
        //        {
        //            return false;
        //        }

        //        if (dt.Rows.Count > 0)
        //        {
        //            for (i = 0; i < dt.Rows.Count; i++)
        //            {
        //                spdWafBin.ActiveSheet.RowCount++;

        //                //if (dt.Rows[i][(int)COL_WAFER.LOW_YIELD_HOLD].ToString() == "Y" ||
        //                //    dt.Rows[i][(int)COL_WAFER.LOW_YIELD_LOSS].ToString() == "Y" ||
        //                //    dt.Rows[i][(int)COL_WAFER.PT2_HYN_LOW_YIELD].ToString() == "Y" ||
        //                //    dt.Rows[i][(int)COL_WAFER.BIN_LOW_YIELD].ToString() == "Y")
        //                //{
        //                //    spdWafBin.ActiveSheet.Rows[spdWafBin.ActiveSheet.RowCount - 1].BackColor = Color.LavenderBlush;
        //                //}

        //                for (j = 0; j < spdWafBin.ActiveSheet.ColumnCount; j++)
        //                {
        //                    spdWafBin.ActiveSheet.Cells[spdWafBin.ActiveSheet.RowCount - 1, j].Value = dt.Rows[i][j].ToString();

        //                    if (j >= (int)COL_WAFER.BIN && j < spdWafBin.ActiveSheet.ColumnCount - 1)
        //                    {
        //                        if (dt.Rows[i][j + 1].ToString() == "Y")
        //                        {
        //                            spdWafBin.ActiveSheet.Cells[spdWafBin.ActiveSheet.RowCount - 1, j].BackColor = Color.Pink;
        //                        }
        //                    }
        //                }
        //            }
        //        }

        //        if (spdWafBin.ActiveSheet.RowCount == 0)
        //        {
        //            for (i = 0; i < 25; i++)
        //            {
        //                spdWafBin.ActiveSheet.RowCount++;
        //                spdWafBin.ActiveSheet.Cells[i, (int)COL_WAFER.WAFER_NO].Value = (i + 1).ToString().PadLeft(2, '0');
        //                spdWafBin.ActiveSheet.Rows[i].BackColor = Color.LightGray;
        //            }
        //        }
        //        else
        //        {
        //            for (i = 0; i < 25; i++)
        //            {
        //                //if (spdWafBin.ActiveSheet.Cells[i, (int)COL_WAFER.WAFER_SEQ].Value.ToString() == "0")
        //                //    spdWafBin.ActiveSheet.Rows[i].BackColor = Color.LightGray;
        //            }
        //        }

        //        ////spdWafBin.ActiveSheet.OperationMode = FarPoint.Win.Spread.OperationMode.SingleSelect;
        //        ////spdWafBin.RPT_ColumnAutoFit(true);
        //        spdWafBin.RPT_AutoFit(true);
        //    }
        //    catch (Exception ex)
        //    {
        //        //MPCF.ShowMsgBox(ex.Message);
        //        return false;
        //    }

        //    return true;
        //}

        /// <summary>
        /// 초기화 함수입니다.
        /// </summary>
        private void InitValueByRegister()
        {

        }

        private void ClearData(Miracom.SmartWeb.UI.Controls.udcFarPoint spread, char ProcStep)
        {
            try
            {

            }
            catch (Exception ex)
            {
                CmnFunction.ShowMsgBox("YLD041002() : " + ex.ToString());
            }
        }

        public bool ViewLotList(int statusRowIndex, int statusColIndex, string lotId)
        {
            StringBuilder sQuery = new StringBuilder();
            string lotStatusType = string.Empty;

            try
            {

                return true;
            }
            catch
            {                
                return false;
            }
        }

        public string MakeSqlString()
        {
            try
            {
                StringBuilder sQuery = new StringBuilder();

                sQuery.Append(" SELECT OPER, OPER_SHORT_DESC FROM MWIPOPRDEF           \n");
                sQuery.Append(" WHERE FACTORY = :p_FACTORY          \n");
                sQuery.Append(" ORDER BY OPER          \n");

                //sQuery.Replace(":p_FACTORY", "'" + StdGlobalVariable.gsReportFactoryPT + "'");

                return sQuery.ToString();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        #endregion

        #region [ Form Event ]
        private void YLD041002_Load(object sender, EventArgs e)
        {
            try
            {
                // GCM Select 로직 추가할것
                if (ViewMapServerInfo("H_DACRUX_IF", "DACRUX_DMS") == false)
                    return;

                oro = new DACrux.TEST.Control.TPUCGallery();
                oro.Dock = DockStyle.Fill;
                oro.MAP_SERVER_IP = map_server_ip; //"12.230.55.151";
                oro.MAP_SERVER_PORT = map_server_port; //7513;

                Panel p = new Panel();
                p.Controls.Add((UserControl)oro);
                p.Dock = DockStyle.Fill;

                this.splitContainer1.Panel2.Controls.Add(p);

                InitSpdColumnHeader();
                InitValueByRegister();

                //StdLangFunction.ToClientLanguage(this);
            }
            catch (Exception ex)
            {
                CmnFunction.ShowMsgBox(ex.ToString());
            }
        }
        #endregion

        //private void cdvOperation_ButtonPress(object sender, EventArgs e)
        //{
        //    try
        //    {
        //        DataTable dt = null;
        //        ListViewItem itmX = null;

        //        cdvOperation.Init();
        //        //StdInitFunction.InitListView(cdvOperation.GetListView);
        //        cdvOperation.Columns.Add("KEY_1", 30, HorizontalAlignment.Left);
        //        cdvOperation.Columns.Add("DATA_1", 70, HorizontalAlignment.Left);
        //        cdvOperation.SelectedSubItemIndex = 0;
        //        cdvOperation.DisplaySubItemIndex = 1;

        //        StringBuilder sQuery = new StringBuilder();

        //        sQuery.Append(" SELECT OPER, OPER_SHORT_DESC FROM MWIPOPRDEF           \n");
        //        sQuery.Append(" WHERE FACTORY = :p_FACTORY          \n");
        //        sQuery.Append(" AND OPER_CMF_2 = 'Y'          \n");
        //        if (string.IsNullOrEmpty(GlobalVariable.gsCustomer.Trim()) != true)
        //        {
        //            if (GlobalVariable.gsCustomer.Trim() != "HYN")
        //                sQuery.Append(" AND OPER NOT IN ('P1300','P1600')          \n");
        //        }
        //        sQuery.Append(" ORDER BY OPER          \n");

        //        //sQuery.Replace(":p_FACTORY", "'" + StdGlobalVariable.gsReportFactoryPT + "'");

        //        dt = CmnFunction.oComm.GetFuncDataTable("DYNAMIC", sQuery.ToString());

        //        if (dt == null || dt.Rows.Count == 0)
        //        {
        //            return;
        //        }

        //        for (int iCnt = 0; iCnt < dt.Rows.Count; iCnt++)
        //        {
        //            //itmX = new ListViewItem(dt.Rows[iCnt]["OPER"].ToString().TrimEnd(), (int)StdGlobalConstant.SMALLICON_INDEX.IDX_MATERIAL);
        //            if (((ListView)cdvOperation.GetListView).Columns.Count > 1)
        //            {
        //                itmX.SubItems.Add(dt.Rows[iCnt]["OPER_SHORT_DESC"].ToString().TrimEnd());
        //            }
        //            ((ListView)cdvOperation.GetListView).Items.Add(itmX);

        //        }
        //        //StdListFunction.ViewMaterialList(cdvDevice.GetListView, StdGlobalVariable.gsReportFactoryPT, "DEVICE");

        //    }
        //    catch (Exception ex)
        //    {
        //        CmnFunction.ShowMsgBox("WIP0001.cdvResource_ButtonPress() : " + ex.ToString());
        //    }

        //}

        private void txtLotID_KeyPress(object sender, KeyPressEventArgs e)
        {
            try
            {
                if (e.KeyChar == (char)13)
                {

                    //StdGlobalVariable.gsCurrentLot_ID = txtLotID.Text.Trim();

                    //if (string.IsNullOrEmpty(StdGlobalVariable.gsCurrentLot_ID) == false)
                    //{
                    //    txtLotID.Text = StdGlobalVariable.gsCurrentLot_ID;
                    //    btnView.PerformClick();
                    //}
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void rdoMMG_CheckedChanged(object sender, EventArgs e)
        {
            btnMapView.PerformClick();

            if (rdoMMG.Checked == true)
                cdvOper.Enabled = false;    //cdvOperation.Enabled = false;
            else
                cdvOper.Enabled = true;     //cdvOperation.Enabled = true;
        }

        //private void spdWafBin_CellClick(object sender, FarPoint.Win.Spread.CellClickEventArgs e)
        //{
        //    string saveResult = string.Empty;
        //    //string[] waferList = new string[spdWafBin.ActiveSheet.Rows.Count];

        //    try
        //    {
        //        Cursor.Current = Cursors.WaitCursor;

        //        if (e.ColumnHeader == true)
        //        {
        //            return;
        //        }

        //        for (int i = 0; i < spdWafBin.ActiveSheet.Rows.Count; i++)
        //        {
        //            if (i == Tempindex)
        //                spdWafBin_Sheet1.Rows[i].BackColor = TempColor;
        //        }

        //        if (rdoOper.Checked == true)
        //        {                    
        //            if (cdvOper.ValueGetListView.FocusedItem.SubItems[1].Text == "IQC")
        //            {
        //                //tpucBinMapView1.DrawWaferMap("IQC", spdSpread.Sheets[0].Cells[0, 1].Value.ToString(), s_mother_lot, spdWafBin.ActiveSheet.Cells[e.Row, (int)COL_WAFER.WAFER_ID].Text, "", 0, false);
        //            }
        //            else if (cdvOper.ValueGetListView.FocusedItem.SubItems[1].Text == "AVI")
        //            {
        //                //tpucBinMapView1.DrawWaferMap("AVI", spdSpread.Sheets[0].Cells[0, 1].Value.ToString(), s_mother_lot, spdWafBin.ActiveSheet.Cells[e.Row, (int)COL_WAFER.WAFER_ID].Text, "", 0, false);
        //            }
        //            else
        //            {
        //                //tpucBinMapView1.DrawWaferMap(cdvOper.ValueGetListView.FocusedItem.SubItems[1].Text, spdSpread.Sheets[0].Cells[0, 1].Value.ToString(), s_mother_lot, spdWafBin.ActiveSheet.Cells[e.Row, (int)COL_WAFER.WAFER_ID].Text, "", 0, false);
        //            }
        //        }
        //        else
        //        {
        //            if (m_oper_desc == "IQC")
        //            {
        //                //tpucBinMapView1.DrawWaferMap("IQC", spdSpread.Sheets[0].Cells[0, 1].Value.ToString(), s_mother_lot, spdWafBin.ActiveSheet.Cells[e.Row, (int)COL_WAFER.WAFER_ID].Text, "", 0, true);
        //            }
        //            else if (m_oper_desc == "AVI")
        //            {
        //                //tpucBinMapView1.DrawWaferMap("AVI", spdSpread.Sheets[0].Cells[0, 1].Value.ToString(), s_mother_lot, spdWafBin.ActiveSheet.Cells[e.Row, (int)COL_WAFER.WAFER_ID].Text, "", 0, true);
        //            }
        //            else
        //            {
        //                //tpucBinMapView1.DrawWaferMap(spdSpread.Sheets[0].Cells[2, 1].Value.ToString(), spdSpread.Sheets[0].Cells[0, 1].Value.ToString(), s_mother_lot, spdWafBin.ActiveSheet.Cells[e.Row, (int)COL_WAFER.WAFER_ID].Text, "", 0, true);

        //                //oro.DrawWaferGallery(waferList);

        //                oro.DrawWaferGallery("", waferList);                     
        //            }
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        //MPCF.ShowMsgBox(ex.Message);
        //        CmnFunction.ShowMsgBox(ex.ToString());
        //    }
        //    finally
        //    {
        //        Cursor.Current = Cursors.Default;
        //    }
        //}

        private void btnMapView_Click(object sender, EventArgs e)
        {
            try
            {
                int seq = 0;
                int cnt = 0;
                string gubun = "";

                if (spdWafBin.ActiveSheet.RowCount == 0) return;

                for (int k = 0; k < spdWafBin.ActiveSheet.RowCount; k++)
                {
                    if (k == Tempindex)
                        spdWafBin_Sheet1.Rows[k].BackColor = TempColor;

                    if (spdWafBin.ActiveSheet.Cells[k, (int)COL_WAFER.SEL].Text == "True")
                    {
                        cnt++;
                    }
                }

                waferList = new string[cnt];

                for (int k = 0; k < spdWafBin.ActiveSheet.RowCount; k++)
                {
                    if (spdWafBin.ActiveSheet.Cells[k, (int)COL_WAFER.SEL].Text == "True")
                    {
                        if (rdoOper.Checked == true)
                        {
                            //if (cdvOper.Text == "IQC")  //cdvOperation.DescText
                            if (cdvOper.ValueGetListView.FocusedItem.SubItems[1].Text == "IQC")
                            {
                                gubun = "IQC";
                                //tpucBinMapView1.DrawWaferMap("IQC", spdSpread.Sheets[0].Cells[0, 1].Value.ToString(), s_mother_lot, spdWafBin.ActiveSheet.Cells[e.Row, (int)COL_WAFER.WAFER_ID].Text, "", 0, false);
                            }
                            else if (cdvOper.ValueGetListView.FocusedItem.SubItems[1].Text == "AVI")
                            {
                                gubun = "AVI";
                                //tpucBinMapView1.DrawWaferMap("AVI", spdSpread.Sheets[0].Cells[0, 1].Value.ToString(), s_mother_lot, spdWafBin.ActiveSheet.Cells[e.Row, (int)COL_WAFER.WAFER_ID].Text, "", 0, false);
                            }
                            else
                            {
                                gubun = "MULTIPROBE";
                                //tpucBinMapView1.DrawWaferMap(cdvOper.ValueGetListView.FocusedItem.SubItems[1].Text, spdSpread.Sheets[0].Cells[0, 1].Value.ToString(), s_mother_lot, spdWafBin.ActiveSheet.Cells[e.Row, (int)COL_WAFER.WAFER_ID].Text, "", 0, false);
                            }
                        }
                        else
                        {
                            if (m_oper_desc == "IQC")
                            {
                                gubun = "IQC";
                            }
                            else if (m_oper_desc == "AVI")
                            {
                                gubun = "AVI";
                            }
                            else
                            {
                                gubun = "MULTIPROBE";
                            }
                        }

                        waferList[seq] = spdWafBin.ActiveSheet.Cells[k, (int)COL_WAFER.WAFER_ID].Value.ToString();

                        seq++;
                    }
                }

                oro.DrawWaferGallery(gubun, waferList);


                ////merge map
                //if (rdoMMG.Checked)
                //{
                //    wafer_data[seq] = spdWafBin.ActiveSheet.Cells[k, (int)COL_WAFER.WAFER_ID].Text;
                //    draw_flag = true;
                //}
                //else  // 공정맵
                //{
                //    wafer_data[seq] = spdWafBin.ActiveSheet.Cells[k, (int)COL_WAFER.WAFER_SEQ].Text;
                //    draw_flag = false;
                //}
            }
            catch (Exception ex)
            {
                //MPCF.ShowMsgBox(ex.Message);
                CmnFunction.ShowMsgBox(ex.ToString());
            }
        }

        private void cdvFactory_ValueSelectedItemChanged(object sender, MCCodeViewSelChanged_EventArgs e)
        {
            cdvOper.sFactory = cdvFactory.txtValue;
        }

       

        //private void btnMapView_Click(object sender, EventArgs e)
        //{
        //string[] wafer_data = null;

        //try
        //{
        //    int seq = 0;
        //    int cnt = 0;

        //    if (spdWafBin.ActiveSheet.RowCount == 0) return;

        //    for (int k = 0; k < spdWafBin.ActiveSheet.RowCount; k++)
        //    {
        //        if (spdWafBin.ActiveSheet.Cells[k, (int)COL_WAFER.SEL].Text == "True")
        //        {
        //            cnt++;
        //        }
        //    }

        //    wafer_data = new string[cnt];

        //    for (int k = 0; k < spdWafBin.ActiveSheet.RowCount; k++)
        //    {
        //        if (spdWafBin.ActiveSheet.Cells[k, (int)COL_WAFER.SEL].Text == "True")
        //        {
        //            //merge map
        //            if (rdoMMG.Checked)
        //            {
        //                wafer_data[seq] = spdWafBin.ActiveSheet.Cells[k, (int)COL_WAFER.WAFER_ID].Text;
        //                draw_flag = true;
        //            }
        //            else  // 공정맵
        //            {
        //                wafer_data[seq] = spdWafBin.ActiveSheet.Cells[k, (int)COL_WAFER.WAFER_SEQ].Text;
        //                draw_flag = false;
        //            }
        //            seq++;
        //        }
        //    }

        //    //oro.Draw(wafer_data, draw_flag);
        //}
        //catch (Exception ex)
        //{
        //    //throw ex;
        //    return;
        //}            
        //}
    }
}
