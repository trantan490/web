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
    public partial class YLD041002 : Miracom.SmartWeb.UI.Controls.udcCUSReportOven002
    {
        private bool b_load_flag;
        //DACrux.YMS.Admin.TPUCGallery oro = null;

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

        public YLD041002()
        {            
            InitializeComponent();
        }

        #region [ Enum Definition ]
        #region " Constant Definition"

        //DACrux.YMS.Admin.TPUCGallery oro = null;
        //private bool draw_flag;

        private enum COL_WAFER
        {
            SEL,
            WAFER_NO,
            WAFER_ID,
            NET_DIE,
            GOOD_QTY,
            YIELD,
            LOW_YIELD_HOLD,
            LOW_YIELD_LOSS,
            BIN_LOW_YIELD,
            PT2_HYN_LOW_YIELD,
            WAFER_SEQ,
            BIN
        }

        #endregion
        #endregion

        #region [ Control Event ]

        #endregion

        #region [ Button Event ]
        private void btnView_Click(object sender, EventArgs e)
        {
            int i = 0;
            long tot_wf = 0;
            long tot_net = 0;
            long tot_good = 0;

            try
            {
                this.Refresh();
                LoadingPopUp.LoadIngPopUpShow(this);

                if (string.IsNullOrEmpty(txtLotID.Text.Trim()) == true)
                {
                    CmnFunction.ShowMsgBox(RptMessages.GetMessage("STD009", GlobalVariable.gcLanguage));
                    return;
                }

                if (rdoOper.Checked == true && string.IsNullOrEmpty(cdvOperation.Text.Trim()) == true)
                {
                    //Combo Box가 아닌 TextBox에 직접 입력해야 할 수도 있기 때문에 공정을 선택하는 Message이지만 공정을 입력하는 Message로 수정. 
                    CmnFunction.ShowMsgBox(RptMessages.GetMessage("STD064", GlobalVariable.gcLanguage));
                    return;
                }

                spdWafBin.ActiveSheet.RowCount = 0;

                View_Wafer_Result_YMS();

                if (spdWafBin.ActiveSheet.RowCount == 0)
                {
                    for (i = 0; i < 25; i++)
                    {
                        spdWafBin.ActiveSheet.RowCount++;
                        spdWafBin.ActiveSheet.Cells[i, (int)COL_WAFER.WAFER_NO].Value = (i + 1).ToString().PadLeft(2, '0');
                    }
                }

                tot_net = 0;
                tot_good = 0;
                for (i = 0; i < 25; i++)
                {
                    if (string.IsNullOrEmpty(spdWafBin.ActiveSheet.Cells[i, (int)COL_WAFER.NET_DIE].Value.ToString()) == false && spdWafBin.ActiveSheet.Cells[i, (int)COL_WAFER.NET_DIE].Value.ToString() != " ")
                    {
                        if (string.IsNullOrEmpty(spdWafBin.ActiveSheet.Cells[i, (int)COL_WAFER.NET_DIE].Value.ToString()) == false)
                        {
                            tot_wf++;
                            tot_net = tot_net + Convert.ToInt32(spdWafBin.ActiveSheet.Cells[i, (int)COL_WAFER.NET_DIE].Value);
                        }
                    }

                    if (string.IsNullOrEmpty(spdWafBin.ActiveSheet.Cells[i, (int)COL_WAFER.GOOD_QTY].Value.ToString()) == false && spdWafBin.ActiveSheet.Cells[i, (int)COL_WAFER.GOOD_QTY].Value.ToString() != " ")
                    {
                        if (string.IsNullOrEmpty(spdWafBin.ActiveSheet.Cells[i, (int)COL_WAFER.GOOD_QTY].Value.ToString()) == false)
                        {
                            tot_good = tot_good + Convert.ToInt32(spdWafBin.ActiveSheet.Cells[i, (int)COL_WAFER.GOOD_QTY].Value);
                        }
                    }
                }
                spdWafBin.ActiveSheet.RowCount++;
                spdWafBin.ActiveSheet.Cells[spdWafBin.ActiveSheet.RowCount - 1, (int)COL_WAFER.WAFER_NO].Value = tot_wf.ToString();
                spdWafBin.ActiveSheet.Cells[spdWafBin.ActiveSheet.RowCount - 1, (int)COL_WAFER.WAFER_NO].Tag = "LOT";
                spdWafBin.ActiveSheet.Cells[spdWafBin.ActiveSheet.RowCount - 1, (int)COL_WAFER.NET_DIE].Text = tot_net.ToString();
                spdWafBin.ActiveSheet.Cells[spdWafBin.ActiveSheet.RowCount - 1, (int)COL_WAFER.GOOD_QTY].Text = tot_good.ToString();
                spdWafBin.ActiveSheet.Cells[spdWafBin.ActiveSheet.RowCount - 1, (int)COL_WAFER.YIELD].Text = Math.Round(((double)tot_good / (double)tot_net) * 100, 2).ToString();

                spdWafBin.ActiveSheet.Rows[spdWafBin.ActiveSheet.RowCount - 1].BackColor = Color.LightYellow;

                //spdWafBin.ActiveSheet.OperationMode = FarPoint.Win.Spread.OperationMode.SingleSelect;
                //StdFunction.FitColumnHeader(spdWafBin);
                CmnFunction.FitColumnHeader(spdWafBin);

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
            //spdWafBin.ActiveSheet.ColumnHeader.Rows.Count = 0;
            spdWafBin.ActiveSheet.RowHeader.ColumnCount = 0;
            spdWafBin.RPT_ColumnInit();
            //spdWafBin.RPT_InitSpread(true, true, FarPoint.Win.Spread.OperationMode.Normal);
            //spdWafBin.Sheets[0].RowHeader.ColumnCount = 0;
            //spdWafBin.RPT_InitColumn();


            //spdWafBin.RPT_AddBasicColumn("Sel.", 0, (int)COL_WAFER.SEL, Visibles.True, Frozen.False, Align.Center, Merge.False, Formatter.ChkType, 50);
            spdWafBin.RPT_AddBasicColumn("Wf No.", 0, (int)COL_WAFER.WAFER_NO, Visibles.True, Frozen.False, Align.Left, Merge.False, Formatter.Number, 50);
            spdWafBin.RPT_AddBasicColumn("Wf ID.", 0, (int)COL_WAFER.WAFER_ID, Visibles.False, Frozen.False, Align.Left, Merge.False, Formatter.String, 100);
            spdWafBin.RPT_AddBasicColumn("Net Die", 0, (int)COL_WAFER.NET_DIE, Visibles.True, Frozen.False, Align.Left, Merge.False, Formatter.String, 80);
            spdWafBin.RPT_AddBasicColumn("Good Qty", 0, (int)COL_WAFER.GOOD_QTY, Visibles.True, Frozen.False, Align.Left, Merge.False, Formatter.String, 60);
            spdWafBin.RPT_AddBasicColumn("Yield", 0, (int)COL_WAFER.YIELD, Visibles.True, Frozen.False, Align.Left, Merge.False, Formatter.String, 80);
            spdWafBin.RPT_AddBasicColumn("Low Yield(Hold)", 0, (int)COL_WAFER.LOW_YIELD_HOLD, Visibles.False, Frozen.False, Align.Left, Merge.False, Formatter.String, 80);
            spdWafBin.RPT_AddBasicColumn("Low Yield(Scrap)", 0, (int)COL_WAFER.LOW_YIELD_LOSS, Visibles.False, Frozen.False, Align.Left, Merge.False, Formatter.String, 60);
            spdWafBin.RPT_AddBasicColumn("Low Yield(BIN)", 0, (int)COL_WAFER.BIN_LOW_YIELD, Visibles.False, Frozen.False, Align.Left, Merge.False, Formatter.String, 60);
            spdWafBin.RPT_AddBasicColumn("Low Yield(HYN PT2)", 0, (int)COL_WAFER.PT2_HYN_LOW_YIELD, Visibles.False, Frozen.False, Align.Left, Merge.False, Formatter.String, 60);
            spdWafBin.RPT_AddBasicColumn("Wafer Seq.", 0, (int)COL_WAFER.WAFER_SEQ, Visibles.True, Frozen.False, Align.Left, Merge.False, Formatter.String, 60);
            spdWafBin.RPT_SetCellsType();

            for (int i = 1; i < spdWafBin.ActiveSheet.ColumnCount; i++)
            {
                spdWafBin.Sheets[0].Columns[i].Locked = true;
            }
        }

        private bool View_Wafer_Result_YMS()
        {
            DataTable dt = null;
            StringBuilder sQuery = new StringBuilder();
            StringBuilder sBinList = new StringBuilder();
            StringBuilder sBinList2 = new StringBuilder();
            StringBuilder sBinList3 = new StringBuilder();
            StringBuilder sBinList4 = new StringBuilder();
            //TRSNode in_node = new TRSNode("IN");
            //TRSNode out_node = new TRSNode("OUT");
            int i = 0;
            int j = 0;

            try
            {
                spdWafBin.ActiveSheet.Rows.Count = 0;

                //Wafer List Result
                if (rdoMMG.Checked == false)
                {
                    sQuery = new StringBuilder();
                    sQuery.AppendLine("SELECT");
                    sQuery.AppendLine("    ' ' SEL");
                    sQuery.AppendLine("    ,LST.WAFER_NO");
                    sQuery.AppendLine("    ,WAF.WAFER_ID");
                    sQuery.AppendLine("    ,NVL(WAF.NETDIE,' ') ");
                    sQuery.AppendLine("    ,NVL(WAF.GOOD_QTY,0) AS GOOD_QTY");
                    sQuery.AppendLine("    ,NVL(YIELD, 0)");
                    sQuery.AppendLine("    ,' ' AS LOW_YIELD_HOLD");
                    sQuery.AppendLine("    ,' ' AS LOW_YIELD_LOSS");
                    sQuery.AppendLine("    ,' ' AS PT2_HYN_LOW_YIELD");
                    sQuery.AppendLine("    ,' ' AS BIN_LOW_YIELD");
                    sQuery.AppendLine("    ,NVL(WAF.WAFER_SEQ,0) AS WAFER_SEQ");
                    sQuery.AppendLine("FROM");
                    sQuery.AppendLine("(");
                    sQuery.AppendLine("    SELECT");
                    sQuery.AppendLine("        WAFHIS.WAFER_NO");
                    sQuery.AppendLine("        ,WAFHIS.WAFER_ID");
                    sQuery.AppendLine("        ,LOTSTS.LOT_CMF_8 AS NETDIE");
                    sQuery.AppendLine("        ,ROUND(WAFHIS.GOOD_QTY/TO_NUMBER(LOTSTS.LOT_CMF_8)*100, 2) AS YIELD");
                    sQuery.AppendLine("        ,' '");
                    sQuery.AppendLine("        ,' '");
                    sQuery.AppendLine("        ,' '");
                    sQuery.AppendLine("        ,' '");
                    sQuery.AppendLine("        ,NVL(WAFHIS.WAFER_SEQ,0) AS WAFER_SEQ");
                    sQuery.AppendLine("        ,NVL(WAFHIS.GOOD_QTY,0) AS GOOD_QTY");
                    sQuery.AppendLine("    FROM VYMSWAFHIS WAFHIS, MWIPLOTSTS LOTSTS");
                    sQuery.AppendLine("    WHERE 1=1");
                    sQuery.AppendLine("        AND WAFHIS.WAFER_ID IN (SELECT WAFER_ID FROM CWIPLOTWAF WHERE LOT_ID=:LOT_ID)");
                    sQuery.AppendLine("        AND WAFHIS.OPER=:OPER");
                    sQuery.AppendLine("        AND WAFHIS.RETEST_IDX_REV=0");
                    sQuery.AppendLine("        AND WAFHIS.LOT_ID=LOTSTS.LOT_ID");
                    sQuery.AppendLine(")WAF, VTOTWAFLST LST");
                    sQuery.AppendLine("WHERE 1=1");
                    sQuery.AppendLine("    AND LST.WAFER_NO=WAF.WAFER_NO(+)");
                    sQuery.AppendLine("ORDER BY LST.WAFER_NO");
                }
                else
                {
                    sQuery = new StringBuilder();
                    sQuery.AppendLine("SELECT");
                    sQuery.AppendLine("    ' ' SEL");
                    sQuery.AppendLine("    ,LST.WAFER_NO");
                    sQuery.AppendLine("    ,WAF.WAFER_ID");
                    sQuery.AppendLine("    ,NVL(WAF.NETDIE,' ') ");
                    sQuery.AppendLine("    ,NVL(WAF.GOOD_QTY,0) AS GOOD_QTY");
                    sQuery.AppendLine("    ,NVL(YIELD, 0)");
                    sQuery.AppendLine("    ,' ' AS LOW_YIELD_HOLD");
                    sQuery.AppendLine("    ,' ' AS LOW_YIELD_LOSS");
                    sQuery.AppendLine("    ,' ' AS PT2_HYN_LOW_YIELD");
                    sQuery.AppendLine("    ,' ' AS BIN_LOW_YIELD");
                    sQuery.AppendLine("    ,NVL(WAF.WAFER_SEQ,0) AS WAFER_SEQ");
                    sQuery.AppendLine("FROM");
                    sQuery.AppendLine("(");
                    sQuery.AppendLine("    SELECT");
                    sQuery.AppendLine("        WAFHIS.WAFER_NO");
                    sQuery.AppendLine("        ,WAFHIS.WAFER_ID");
                    sQuery.AppendLine("        ,LOTSTS.LOT_CMF_8 AS NETDIE");
                    sQuery.AppendLine("        ,ROUND(WAFHIS.GOOD_QTY/TO_NUMBER(LOTSTS.LOT_CMF_8)*100, 2) AS YIELD");
                    sQuery.AppendLine("        ,' '");
                    sQuery.AppendLine("        ,' '");
                    sQuery.AppendLine("        ,' '");
                    sQuery.AppendLine("        ,' '");
                    sQuery.AppendLine("        ,NVL(WAFHIS.WAFER_SEQ,0) AS WAFER_SEQ");
                    sQuery.AppendLine("        ,NVL(WAFHIS.GOOD_QTY,0) AS GOOD_QTY");
                    sQuery.AppendLine("    FROM VYMSWAFMMG WAFHIS, MWIPLOTSTS LOTSTS");
                    sQuery.AppendLine("    WHERE 1=1");
                    sQuery.AppendLine("        AND WAFHIS.WAFER_ID IN (SELECT WAFER_ID FROM CWIPLOTWAF WHERE LOT_ID=:LOT_ID)");
                    sQuery.AppendLine("        AND WAFHIS.LOT_ID=LOTSTS.LOT_ID");
                    sQuery.AppendLine(")WAF, VTOTWAFLST LST");
                    sQuery.AppendLine("WHERE 1=1");
                    sQuery.AppendLine("    AND LST.WAFER_NO=WAF.WAFER_NO(+)");
                    sQuery.AppendLine("ORDER BY LST.WAFER_NO");
                }

                sQuery.Replace(":LOT_ID", "'" + txtLotID.Text + "'");
                sQuery.Replace(":OPER", "'" + cdvOperation.Text + "'");

                dt = CmnFunction.oComm.GetFuncDataTable("DYNAMIC", sQuery.ToString());

                if (dt == null)
                {
                    return false;
                }

                if (dt.Rows.Count > 0)
                {
                    for (i = 0; i < dt.Rows.Count; i++)
                    {
                        spdWafBin.ActiveSheet.RowCount++;

                        if (dt.Rows[i][(int)COL_WAFER.LOW_YIELD_HOLD].ToString() == "Y" ||
                            dt.Rows[i][(int)COL_WAFER.LOW_YIELD_LOSS].ToString() == "Y" ||
                            dt.Rows[i][(int)COL_WAFER.PT2_HYN_LOW_YIELD].ToString() == "Y" ||
                            dt.Rows[i][(int)COL_WAFER.BIN_LOW_YIELD].ToString() == "Y")
                        {
                            spdWafBin.ActiveSheet.Rows[spdWafBin.ActiveSheet.RowCount - 1].BackColor = Color.LavenderBlush;
                        }

                        for (j = 0; j < spdWafBin.ActiveSheet.ColumnCount; j++)
                        {
                            spdWafBin.ActiveSheet.Cells[spdWafBin.ActiveSheet.RowCount - 1, j].Value = dt.Rows[i][j].ToString();

                            if (j >= (int)COL_WAFER.BIN && j < spdWafBin.ActiveSheet.ColumnCount - 1)
                            {
                                if (dt.Rows[i][j + 1].ToString() == "Y")
                                {
                                    spdWafBin.ActiveSheet.Cells[spdWafBin.ActiveSheet.RowCount - 1, j].BackColor = Color.Pink;
                                }
                            }
                        }
                    }
                }

                if (spdWafBin.ActiveSheet.RowCount == 0)
                {
                    for (i = 0; i < 25; i++)
                    {
                        spdWafBin.ActiveSheet.RowCount++;
                        spdWafBin.ActiveSheet.Cells[i, (int)COL_WAFER.WAFER_NO].Value = (i + 1).ToString().PadLeft(2, '0');
                        spdWafBin.ActiveSheet.Rows[i].BackColor = Color.LightGray;
                    }
                }
                else
                {
                    for (i = 0; i < 25; i++)
                    {
                        if (spdWafBin.ActiveSheet.Cells[i, (int)COL_WAFER.WAFER_SEQ].Value.ToString() == "0")
                            spdWafBin.ActiveSheet.Rows[i].BackColor = Color.LightGray;
                    }
                }

                //spdWafBin.ActiveSheet.OperationMode = FarPoint.Win.Spread.OperationMode.SingleSelect;
                //spdWafBin.RPT_ColumnAutoFit(true);
                spdWafBin.RPT_AutoFit(true);
            }
            catch 
            {                
                return false;
            }

            return true;
        }

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
                RegistryKey rk1 = Registry.CurrentUser.CreateSubKey("SOFTWARE\\Miracom\\DACrux\\UserOption", RegistryKeyPermissionCheck.ReadWriteSubTree);
                rk1.SetValue("Server", "115.89.54.152");

                InitSpdColumnHeader();
                InitValueByRegister();
                //txtFactory.Text = StdGlobalVariable.gsReportFactoryPT;

                //oro = new DACrux.YMS.Admin.TPUCGallery();
                //oro.Dock = System.Windows.Forms.DockStyle.Fill; 
                Panel p = new Panel(); 
                //p.Controls.Add((UserControl)oro);
                p.Dock = System.Windows.Forms.DockStyle.Fill;

                this.splitContainer1.Panel2.Controls.Add(p);

                //StdLangFunction.ToClientLanguage(this);
            }
            catch (Exception ex)
            {
                CmnFunction.ShowMsgBox(ex.ToString());
            }
        }
        #endregion

        private void cdvOperation_ButtonPress(object sender, EventArgs e)
        {
            try
            {
                DataTable dt = null;
                ListViewItem itmX = null;

                cdvOperation.Init();
                //StdInitFunction.InitListView(cdvOperation.GetListView);
                cdvOperation.Columns.Add("KEY_1", 30, HorizontalAlignment.Left);
                cdvOperation.Columns.Add("DATA_1", 70, HorizontalAlignment.Left);
                cdvOperation.SelectedSubItemIndex = 0;
                cdvOperation.DisplaySubItemIndex = 1;

                StringBuilder sQuery = new StringBuilder();

                sQuery.Append(" SELECT OPER, OPER_SHORT_DESC FROM MWIPOPRDEF           \n");
                sQuery.Append(" WHERE FACTORY = :p_FACTORY          \n");
                sQuery.Append(" AND OPER_CMF_2 = 'Y'          \n");
                if (string.IsNullOrEmpty(GlobalVariable.gsCustomer.Trim()) != true)
                {
                    if (GlobalVariable.gsCustomer.Trim() != "HYN")
                        sQuery.Append(" AND OPER NOT IN ('P1300','P1600')          \n");
                }
                sQuery.Append(" ORDER BY OPER          \n");

                //sQuery.Replace(":p_FACTORY", "'" + StdGlobalVariable.gsReportFactoryPT + "'");

                dt = CmnFunction.oComm.GetFuncDataTable("DYNAMIC", sQuery.ToString());

                if (dt == null || dt.Rows.Count == 0)
                {
                    return;
                }

                for (int iCnt = 0; iCnt < dt.Rows.Count; iCnt++)
                {
                    //itmX = new ListViewItem(dt.Rows[iCnt]["OPER"].ToString().TrimEnd(), (int)StdGlobalConstant.SMALLICON_INDEX.IDX_MATERIAL);
                    if (((ListView)cdvOperation.GetListView).Columns.Count > 1)
                    {
                        itmX.SubItems.Add(dt.Rows[iCnt]["OPER_SHORT_DESC"].ToString().TrimEnd());
                    }
                    ((ListView)cdvOperation.GetListView).Items.Add(itmX);

                }
                //StdListFunction.ViewMaterialList(cdvDevice.GetListView, StdGlobalVariable.gsReportFactoryPT, "DEVICE");

            }
            catch (Exception ex)
            {
                CmnFunction.ShowMsgBox("WIP0001.cdvResource_ButtonPress() : " + ex.ToString());
            }
            
        }

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
                cdvOperation.Enabled = false;
            else
                cdvOperation.Enabled = true;
        }

        private void btnMapView_Click(object sender, EventArgs e)
        {
            string[] wafer_data = null;

            try
            {
                int seq = 0;
                int cnt = 0;

                if (spdWafBin.ActiveSheet.RowCount == 0) return;

                for (int k = 0; k < spdWafBin.ActiveSheet.RowCount; k++)
                {
                    if (spdWafBin.ActiveSheet.Cells[k, (int)COL_WAFER.SEL].Text == "True")
                    {
                        cnt++;
                    }
                }

                wafer_data = new string[cnt];

                for (int k = 0; k < spdWafBin.ActiveSheet.RowCount; k++)
                {
                    if (spdWafBin.ActiveSheet.Cells[k, (int)COL_WAFER.SEL].Text == "True")
                    {
                        //merge map
                        if (rdoMMG.Checked)
                        {
                            wafer_data[seq] = spdWafBin.ActiveSheet.Cells[k, (int)COL_WAFER.WAFER_ID].Text;
                            //draw_flag = true;
                        }
                        else  // 공정맵
                        {
                            wafer_data[seq] = spdWafBin.ActiveSheet.Cells[k, (int)COL_WAFER.WAFER_SEQ].Text;
                            //draw_flag = false;
                        }
                        seq++;
                    }
                }

                //oro.Draw(wafer_data, draw_flag);
            }
            catch
            {                
                return;
            }            
        }
    }
}
