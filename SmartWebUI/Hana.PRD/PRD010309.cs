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
using System.Collections;

namespace Hana.PRD
{
    public partial class PRD010309 : Miracom.SmartWeb.UI.Controls.udcCUSReport002
    {
        /// <summary>
        /// Ŭ  ��  ��: PRD010309<br/>
        /// Ŭ�������: ��ü HOLD LOT ��ȸ<br/>
        /// ��  ��  ��: �ϳ�����ũ�� ������<br/>
        /// �����ۼ���: 2013-10-08<br/>
        /// ��  ����: ��ü HOLD LOT ��ȸ.<br/>
        /// ����  ����: <br/>   
        /// 2013-10-14-������ : Sub Total, Grand Total Pop up ��� �߰� & Target TAT Day �� ���� (��Ǽ� ��û)
        /// 2013-10-15-������ : �⺻ : �����ϱ���, ��ü�� 0���� ���� (��Ǽ� ��û)
        ///                     GRAND_TOTAL, SUB_TOTAL �� HOLD RATE �� ��� ���� �ƴ� ���� ���ϴ� �������� ���� (��Ǽ� ��û)
        /// 2013-10-23-��ο� : 1.     1�� �̸� �׸� �߰� -> ��ü���� 1�� �̸��� ��ǰ ǥ��  (��Ǽ� ��û)
        ///                     2.     FGS ����� ��ȸ �� �� �ֵ��� ����(FGS �� �����׷��� STOCK �� ǥ���ϸ�, F0000 ����� ǥ��)
        /// 2013-12-24-������ : �� �˾� ���� ���� : ��ġ�μ�/��ü/����/PIN TYPE/PRODUCT/LOT_ID (��Ǽ� ��û)
        /// 2014-01-02-������ : COB ���� üũ �ڽ� �߰� (��Ǽ�D ��û)
        /// 2015-03-12-������ : HMKE �߰� (�ڹ���D ��û
        /// </summary>
        public PRD010309()
        {
            InitializeComponent();
            //udcDurationDate1.AutoBinding();

            SortInit();
            GridColumnInit(); //��� ����¥��

            cdvHoldKind.SelectedIndex = 0;
            cdvDate.Value = DateTime.Today;
            txtHoldDate.Text = "0";
        }

        #region SortInit

        /// <summary>
        /// SortInit
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SortInit()
        {
            ((udcTableForm)(this.btnSort.BindingForm)).Clear();

            if (cdvFactory.Text.Trim() != "HMKB1")
            {
                ((udcTableForm)(this.btnSort.BindingForm)).AddRow("Step", "A.OPER_GRP", "DECODE(A.OPER_GRP, 'STOCK', 1, 'DP', 2, 'DA', 3, 'WB', 4, 'GATE', 5, 'MOLD', 6, 'TEST', 7, 9)", "A.OPER_GRP", true);
                ((udcTableForm)(this.btnSort.BindingForm)).AddRow("Cust Type", "A.CUST_TYPE", "DECODE(A.CUST_TYPE, '�Ｚ�޸�', 1, 'S-LSI', 2, 'HYNIX', 3, 'IML', 4, 'FCI', 5, 9)", "A.CUST_TYPE", true);
                ((udcTableForm)(this.btnSort.BindingForm)).AddRow("Customer", "C.MAT_GRP_1", "C.MAT_GRP_1", "A.MAT_GRP_1", false);
                ((udcTableForm)(this.btnSort.BindingForm)).AddRow("Family", "C.MAT_GRP_2", "C.MAT_GRP_2", "A.MAT_GRP_2", false);
                ((udcTableForm)(this.btnSort.BindingForm)).AddRow("Package", "C.MAT_GRP_3", "C.MAT_GRP_3", "A.MAT_GRP_3", false);
                ((udcTableForm)(this.btnSort.BindingForm)).AddRow("Type1", "C.MAT_GRP_4", "C.MAT_GRP_4", "A.MAT_GRP_4", false);
                ((udcTableForm)(this.btnSort.BindingForm)).AddRow("Type2", "C.MAT_GRP_5", "C.MAT_GRP_5", "A.MAT_GRP_5", false);
                ((udcTableForm)(this.btnSort.BindingForm)).AddRow("LD Count", "C.MAT_GRP_6", "C.MAT_GRP_6", "A.MAT_GRP_6", false);
                ((udcTableForm)(this.btnSort.BindingForm)).AddRow("Density", "C.MAT_GRP_7", "C.MAT_GRP_7", "A.MAT_GRP_7", false);
                ((udcTableForm)(this.btnSort.BindingForm)).AddRow("Generation", "C.MAT_GRP_8", "C.MAT_GRP_8", "A.MAT_GRP_8", false);
                ((udcTableForm)(this.btnSort.BindingForm)).AddRow("Pin Type", "C.MAT_CMF_10", "C.MAT_CMF_10", "A.MAT_CMF_10", false);
                ((udcTableForm)(this.btnSort.BindingForm)).AddRow("Cust Device", "C.MAT_CMF_7", "C.MAT_CMF_7", "A.MAT_CMF_7", false);
                ((udcTableForm)(this.btnSort.BindingForm)).AddRow("Product", "A.MAT_ID", "A.MAT_ID", "A.MAT_ID", false);
            }
            else
            {
                ((udcTableForm)(this.btnSort.BindingForm)).AddRow("Step", "A.OPER_GRP", "DECODE(A.OPER_GRP, 'Incomming', 1, 'RCF', 2, 'RDL1', 3, 'PSV1', 4, 'RDL2', 5, 'PSV2', 6, 'RDL3', 7, 'PSV3', 8, 'BUMP', 9, 'AVI', 10, 'OQC', 11, 'PACKING', 12, 14)", "A.OPER_GRP", true);
                ((udcTableForm)(this.btnSort.BindingForm)).AddRow("Cust Type", "A.CUST_TYPE", "DECODE(A.CUST_TYPE, '�Ｚ�޸�', 1, 'S-LSI', 2, 'HYNIX', 3, 'IML', 4, 'FCI', 5, 9)", "A.CUST_TYPE", true);
                ((udcTableForm)(this.btnSort.BindingForm)).AddRow("Customer", "C.MAT_GRP_1", "C.MAT_GRP_1", "A.MAT_GRP_1", false);
                ((udcTableForm)(this.btnSort.BindingForm)).AddRow("BUMPING TYPE", "C.MAT_GRP_2", "C.MAT_GRP_2", "A.MAT_GRP_2", true);
                ((udcTableForm)(this.btnSort.BindingForm)).AddRow("PROCESS FLOW", "C.MAT_GRP_3", "C.MAT_GRP_3", "A.MAT_GRP_3", true);
                ((udcTableForm)(this.btnSort.BindingForm)).AddRow("Layer classification", "C.MAT_GRP_4", "C.MAT_GRP_4", "A.MAT_GRP_4", true);
                ((udcTableForm)(this.btnSort.BindingForm)).AddRow("RDL PLATING", "C.MAT_GRP_6", "C.MAT_GRP_6", "A.MAT_GRP_6", false);
                ((udcTableForm)(this.btnSort.BindingForm)).AddRow("FINAL BUMP", "C.MAT_GRP_7", "C.MAT_GRP_7", "A.MAT_GRP_7", false);
                ((udcTableForm)(this.btnSort.BindingForm)).AddRow("SUB. MATERIAL", "C.MAT_GRP_8", "C.MAT_GRP_8", "A.MAT_GRP_8", false);
                ((udcTableForm)(this.btnSort.BindingForm)).AddRow("SIZE", "C.MAT_CMF_14", "C.MAT_CMF_14", "A.MAT_CMF_14", false);
                ((udcTableForm)(this.btnSort.BindingForm)).AddRow("THICKNESS", "C.MAT_CMF_2", "C.MAT_CMF_2", "A.MAT_CMF_2", false);
                ((udcTableForm)(this.btnSort.BindingForm)).AddRow("FLAT TYPE", "C.MAT_CMF_3", "C.MAT_CMF_3", "A.MAT_CMF_3", false);
                ((udcTableForm)(this.btnSort.BindingForm)).AddRow("Product", "A.MAT_ID", "A.MAT_ID", "A.MAT_ID", false);
            }
        }

        #endregion


        #region �����������

        /// <summary>
        /// �����������
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void GridColumnInit()
        {
            try
            {
                spdData.ActiveSheet.ColumnHeader.Rows.Count = 1;
                spdData.RPT_ColumnInit();

                if (cdvFactory.Text.Trim() != "HMKB1")
                {
                    spdData.RPT_AddBasicColumn("Step", 0, 0, Visibles.True, Frozen.True, Align.Center, Merge.True, Formatter.String, 80);
                    spdData.RPT_AddBasicColumn("Cust Type", 0, 1, Visibles.True, Frozen.True, Align.Center, Merge.True, Formatter.String, 80);
                    spdData.RPT_AddBasicColumn("Customer", 0, 2, Visibles.False, Frozen.True, Align.Center, Merge.True, Formatter.String, 80);
                    spdData.RPT_AddBasicColumn("Family", 0, 3, Visibles.False, Frozen.True, Align.Center, Merge.True, Formatter.String, 80);
                    spdData.RPT_AddBasicColumn("Package", 0, 4, Visibles.False, Frozen.True, Align.Center, Merge.True, Formatter.String, 80);
                    spdData.RPT_AddBasicColumn("Type1", 0, 5, Visibles.False, Frozen.True, Align.Center, Merge.True, Formatter.String, 80);
                    spdData.RPT_AddBasicColumn("Type2", 0, 6, Visibles.False, Frozen.True, Align.Center, Merge.True, Formatter.String, 80);
                    spdData.RPT_AddBasicColumn("LD Count", 0, 7, Visibles.False, Frozen.True, Align.Center, Merge.True, Formatter.String, 80);
                    spdData.RPT_AddBasicColumn("Density", 0, 8, Visibles.False, Frozen.True, Align.Center, Merge.True, Formatter.String, 80);
                    spdData.RPT_AddBasicColumn("Generation", 0, 9, Visibles.False, Frozen.True, Align.Center, Merge.True, Formatter.String, 80);
                    spdData.RPT_AddBasicColumn("Pin Type", 0, 10, Visibles.False, Frozen.True, Align.Left, Merge.True, Formatter.String, 80);
                    spdData.RPT_AddBasicColumn("Cust Device", 0, 11, Visibles.False, Frozen.True, Align.Left, Merge.True, Formatter.String, 80);
                    spdData.RPT_AddBasicColumn("Product", 0, 12, Visibles.False, Frozen.True, Align.Left, Merge.True, Formatter.String, 80);
                }
                else
                {
                    spdData.RPT_AddBasicColumn("Step", 0, 0, Visibles.True, Frozen.True, Align.Center, Merge.True, Formatter.String, 80);
                    spdData.RPT_AddBasicColumn("Cust Type", 0, 1, Visibles.True, Frozen.True, Align.Center, Merge.True, Formatter.String, 80);
                    spdData.RPT_AddBasicColumn("Customer", 0, 2, Visibles.False, Frozen.True, Align.Center, Merge.True, Formatter.String, 80);
                    spdData.RPT_AddBasicColumn("Bumping Type", 0, 3, Visibles.True, Frozen.True, Align.Left, Merge.True, Formatter.String, 70);
                    spdData.RPT_AddBasicColumn("Operation Flow", 0, 4, Visibles.True, Frozen.True, Align.Center, Merge.True, Formatter.String, 80);
                    spdData.RPT_AddBasicColumn("Layer classification", 0, 5, Visibles.True, Frozen.True, Align.Center, Merge.True, Formatter.String, 80);
                    spdData.RPT_AddBasicColumn("RDL Plating", 0, 6, Visibles.False, Frozen.True, Align.Center, Merge.True, Formatter.String, 70);
                    spdData.RPT_AddBasicColumn("Final Bump", 0, 7, Visibles.False, Frozen.True, Align.Center, Merge.True, Formatter.String, 70);
                    spdData.RPT_AddBasicColumn("Sub. Material", 0, 8, Visibles.False, Frozen.True, Align.Center, Merge.True, Formatter.String, 70);
                    spdData.RPT_AddBasicColumn("Size", 0, 9, Visibles.False, Frozen.True, Align.Center, Merge.True, Formatter.String, 70);
                    spdData.RPT_AddBasicColumn("Thickness", 0, 10, Visibles.False, Frozen.True, Align.Left, Merge.True, Formatter.String, 70);
                    spdData.RPT_AddBasicColumn("Flat Type", 0, 11, Visibles.False, Frozen.True, Align.Left, Merge.True, Formatter.String, 70);
                    spdData.RPT_AddBasicColumn("Product", 0, 12, Visibles.False, Frozen.True, Align.Left, Merge.True, Formatter.String, 80);
                }

                spdData.RPT_AddBasicColumn("Less than 1 day", 0, 13, Visibles.True, Frozen.False, Align.Center, Merge.False, Formatter.String, 80);
                spdData.RPT_AddBasicColumn("LOT", 1, 13, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 80);
                spdData.RPT_AddBasicColumn("QTY", 1, 14, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 80);
                spdData.RPT_MerageHeaderColumnSpan(0, 13, 2);

                spdData.RPT_AddBasicColumn("More than 1 day", 0, 15, Visibles.True, Frozen.False, Align.Center, Merge.False, Formatter.String, 80);
                spdData.RPT_AddBasicColumn("LOT", 1, 15, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 80);
                spdData.RPT_AddBasicColumn("QTY", 1, 16, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 80);
                spdData.RPT_MerageHeaderColumnSpan(0, 15, 2);

                spdData.RPT_AddBasicColumn("More than 2 days", 0, 17, Visibles.True, Frozen.False, Align.Center, Merge.False, Formatter.String, 80);
                spdData.RPT_AddBasicColumn("LOT", 1, 17, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 80);
                spdData.RPT_AddBasicColumn("QTY", 1, 18, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 80);
                spdData.RPT_MerageHeaderColumnSpan(0, 17, 2);

                spdData.RPT_AddBasicColumn("More than 3 days", 0, 19, Visibles.True, Frozen.False, Align.Center, Merge.False, Formatter.String, 80);
                spdData.RPT_AddBasicColumn("LOT", 1, 19, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 80);
                spdData.RPT_AddBasicColumn("QTY", 1, 20, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 80);
                spdData.RPT_MerageHeaderColumnSpan(0, 19, 2);

                spdData.RPT_AddBasicColumn("More than 7 days", 0, 21, Visibles.True, Frozen.False, Align.Center, Merge.False, Formatter.String, 80);
                spdData.RPT_AddBasicColumn("LOT", 1, 21, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 80);
                spdData.RPT_AddBasicColumn("QTY", 1, 22, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 80);
                spdData.RPT_MerageHeaderColumnSpan(0, 21, 2);

                spdData.RPT_AddBasicColumn("TOTAL", 0, 23, Visibles.True, Frozen.False, Align.Center, Merge.False, Formatter.String, 80);
                spdData.RPT_AddBasicColumn("LOT", 1, 23, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 80);
                spdData.RPT_AddBasicColumn("QTY", 1, 24, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 80);
                spdData.RPT_MerageHeaderColumnSpan(0, 23, 2);

                spdData.RPT_AddBasicColumn("HOLD RATE", 0, 25, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Double1, 80);
                spdData.RPT_AddBasicColumn("HOLD QTY", 0, 26, Visibles.False, Frozen.False, Align.Right, Merge.False, Formatter.Number, 80);
                spdData.RPT_AddBasicColumn("WIP TTL", 0, 27, Visibles.False, Frozen.False, Align.Right, Merge.False, Formatter.Number, 80);

                for (int i = 0; i <= 12; i++)
                {
                    spdData.RPT_MerageHeaderRowSpan(0, i, 2);
                }

                spdData.RPT_MerageHeaderRowSpan(0, 25, 2);
                spdData.RPT_MerageHeaderRowSpan(0, 26, 2);
                spdData.RPT_MerageHeaderRowSpan(0, 27, 2);

                spdData.RPT_ColumnConfigFromTable_New(btnSort); //Group�׸��� ������� �ݵ�� �������ٰ�.
                //spdData.RPT_ColumnConfigFromTable(btnSort); //Group�׸��� ������� �ݵ�� �������ٰ�.
            }
            catch (Exception ex)
            {
                CmnFunction.ShowMsgBox(ex.Message);
            }
        }

        #endregion

        #region ��ȸ

        /// <summary>
        /// ��ȸ
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

                //�׷��� ���� 1������ SubTotal�� ����ϱ� ���ؼ� ù��° ���� ������
                int sub = ((udcTableForm)(this.btnSort.BindingForm)).GetCheckedValue(1);
                int nGroupCount = ((udcTableForm)(this.btnSort.BindingForm)).GetSelectedCount();

                ////by John (����¥��)
                ////1.Griid �հ� ǥ��
                int[] rowType = spdData.RPT_DataBindingWithSubTotal(dt, 0, sub + 1, 13, null, null, btnSort);
                //spdData.DataSource = dt;

                ////2. Į�� ����(�ʿ��ϴٸ�..)
                //spdData.Sheets[0].FrozenColumnCount = 9;

                ////3. Total�κ� ������
                spdData.RPT_FillDataSelectiveCells("Total", 0, 13, 0, 1, true, Align.Center, VerticalAlign.Center);

                //4. Column Auto Fit
                spdData.RPT_AutoFit(false);
                                                
                SetAvgVertical(1, 25, false);                

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

        public void SetAvgVertical(int nSampleNormalRowPos, int nColPos, bool bWithNull)
        {                        
            Color color = spdData.ActiveSheet.Cells[nSampleNormalRowPos, nColPos].BackColor;
            double ibunja = 0;
            double ibunmo = 0;
            double irate = 0;

            ibunja = Convert.ToDouble(spdData.ActiveSheet.Cells[0, 26].Value);
            ibunmo = Convert.ToDouble(spdData.ActiveSheet.Cells[0, 27].Value);

            spdData.ActiveSheet.Cells[0, nColPos].Value = (ibunja / ibunmo) * 100;
            
            for (int i = 1; i < spdData.ActiveSheet.Rows.Count; i++)
            {
                if (spdData.ActiveSheet.Cells[i, nColPos].BackColor != color)
                {
                    ibunja = Convert.ToDouble(spdData.ActiveSheet.Cells[i, 26].Value);
                    ibunmo = Convert.ToDouble(spdData.ActiveSheet.Cells[i, 27].Value);

                    if (ibunmo != 0)
                    {
                        irate = (ibunja / ibunmo) * 100;
                        spdData.ActiveSheet.Cells[i, nColPos].Value = irate;
                    }
                }
            }

            return;
        }

        #region CheckField

        /// <summary>
        /// CheckField
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private Boolean CheckField()
        {
            if (cdvFactory.Text.Trim().Length == 0)
            {
                CmnFunction.ShowMsgBox(RptMessages.GetMessage("STD001", GlobalVariable.gcLanguage));
                return false;
            }

            if (cdvStep.Text.Trim().Length == 0)
            {
                CmnFunction.ShowMsgBox(RptMessages.GetMessage("STD005", GlobalVariable.gcLanguage));
                return false;
            }

            return true;
        }

        #endregion

        #region MakeSqlString
        private string MakeSqlString()
        {
            StringBuilder strSqlString = new StringBuilder();

            bool isToday = false;
            string QueryCond1 = null;
            string QueryCond2 = null;

            udcTableForm tableForm = (udcTableForm)btnSort.BindingForm;                        
            QueryCond1 = tableForm.SelectedValueToQueryContainNull;
            QueryCond2 = tableForm.SelectedValue2ToQueryContainNull;
                       
            string strDate = cdvDate.Value.ToString("yyyyMMdd");

            if (DateTime.Now.ToString("yyyyMMdd").Equals(strDate))
            {
                strDate = DateTime.Now.ToString("yyyyMMddHHmmss");
                isToday = true;
            }
            else
            {
                strDate = strDate + "220000";
            }


            strSqlString.Append("WITH DT AS" + "\n");
            strSqlString.Append("(" + "\n");

            if (rbtCreate.Checked == true)
                strSqlString.Append("    SELECT TRUNC(TO_DATE('" + strDate + "','YYYYMMDDHH24MISS') - TO_DATE(A.LOT_CMF_14,'YYYYMMDDHH24MISS'), 2) AS TAT" + "\n");
            else if(rbtIssue.Checked == true)
                strSqlString.Append("    SELECT TRUNC(TO_DATE('" + strDate + "','YYYYMMDDHH24MISS') - DECODE(A.RESV_FIELD_1, ' ', SYSDATE, TO_DATE(A.RESV_FIELD_1,'YYYYMMDDHH24MISS')), 2) AS TAT " + "\n");
            else
                strSqlString.Append("    SELECT TRUNC(TO_DATE('" + strDate + "','YYYYMMDDHH24MISS') - TO_DATE(A.OPER_IN_TIME,'YYYYMMDDHH24MISS'), 2) AS TAT " + "\n");
                        
            strSqlString.Append("         , CASE WHEN A.FACTORY = '" + GlobalVariable.gsAssyDefaultFactory + "' AND B.OPER_GRP_1 = 'HMK2A' THEN 'STOCK'" + "\n");
            strSqlString.Append("                WHEN A.FACTORY = '" + GlobalVariable.gsAssyDefaultFactory + "' AND B.OPER_GRP_1 IN ('B/G','SAW', 'Back Side Coating') THEN 'DP'" + "\n");
            strSqlString.Append("                WHEN A.FACTORY = '" + GlobalVariable.gsAssyDefaultFactory + "' AND B.OPER_GRP_1 IN ('D/A','SMT', 'S/P') THEN 'DA'" + "\n");
            strSqlString.Append("                WHEN A.FACTORY = '" + GlobalVariable.gsAssyDefaultFactory + "' AND B.OPER_GRP_1 IN ('W/B','GATE') THEN 'WB'" + "\n");
            strSqlString.Append("                WHEN A.FACTORY = '" + GlobalVariable.gsAssyDefaultFactory + "' AND B.OPER_GRP_1 IN ('MOLD','CURE') THEN 'MOLD'" + "\n");
            strSqlString.Append("                WHEN A.FACTORY = '" + GlobalVariable.gsAssyDefaultFactory + "' THEN 'FINISH'" + "\n");
            strSqlString.Append("                WHEN A.FACTORY = '" + GlobalVariable.gsTestDefaultFactory + "' AND B.OPER_GRP_1 = 'HMK3T' THEN 'STOCK'" + "\n");
            strSqlString.Append("                WHEN A.FACTORY = '" + GlobalVariable.gsTestDefaultFactory + "' AND B.OPER_GRP_1 IN ('TEST', 'CAS', 'OS') THEN 'TEST'" + "\n");
            strSqlString.Append("                WHEN A.FACTORY = '" + GlobalVariable.gsTestDefaultFactory + "' THEN 'MVP'" + "\n");
            strSqlString.Append("                WHEN A.FACTORY = 'FGS' AND B.OPER = 'F0000' THEN 'STOCK'" + "\n");
            strSqlString.Append("                WHEN A.FACTORY = 'HMKE1' AND B.OPER_GRP_1 = 'HMKE1' THEN 'STOCK'" + "\n");
            strSqlString.Append("                WHEN A.FACTORY = 'HMKE1' AND B.OPER_GRP_1 = 'TEST' THEN 'TEST'" + "\n");
            strSqlString.Append("                WHEN A.FACTORY = 'HMKE1' AND B.OPER_GRP_1 IN ('BAKE', 'QA', 'P/K', 'HMKE2') THEN 'MVP'" + "\n");
            strSqlString.Append("                WHEN A.FACTORY = 'HMKB1' AND B.OPER_GRP_1 IN ('HMK2B','IQC', 'I-STOCK') THEN 'Incoming'" + "\n");
            strSqlString.Append("                WHEN A.FACTORY = 'HMKB1' AND B.OPER_GRP_1 = 'RCF_PHOTO' THEN 'RCF'" + "\n");
            strSqlString.Append("                WHEN A.FACTORY = 'HMKB1' AND B.OPER_GRP_1 IN ('RDL1_SPUTTER','RDL1_PHOTO', 'RDL1_PLAT', 'RDL1_ETCH') THEN 'RDL1'" + "\n");
            strSqlString.Append("                WHEN A.FACTORY = 'HMKB1' AND B.OPER_GRP_1 = 'PSV1_PHOTO' THEN 'PSV1'" + "\n");
            strSqlString.Append("                WHEN A.FACTORY = 'HMKB1' AND B.OPER_GRP_1 IN ('RDL2_SPUTTER','RDL2_PHOTO', 'RDL2_PLAT', 'RDL2_ETCH') THEN 'RDL2'" + "\n");
            strSqlString.Append("                WHEN A.FACTORY = 'HMKB1' AND B.OPER_GRP_1 = 'PSV2_PHOTO' THEN 'PSV2'" + "\n");
            strSqlString.Append("                WHEN A.FACTORY = 'HMKB1' AND B.OPER_GRP_1 IN ('RDL3_SPUTTER','RDL3_PHOTO', 'RDL3_PLAT', 'RDL3_ETCH') THEN 'RDL3'" + "\n");
            strSqlString.Append("                WHEN A.FACTORY = 'HMKB1' AND B.OPER_GRP_1 = 'PSV3_PHOTO' THEN 'PSV3'" + "\n");
            strSqlString.Append("                WHEN A.FACTORY = 'HMKB1' AND B.OPER_GRP_1 IN ('BUMP_SPUTTER','BUMP_PHOTO','BUMP_CU_PLAT','BUMP_SNAG_PLAT','BUMP_ETCH','BUMP_BALL_MOUNT','BUMP_REFLOW','FINAL_INSP') THEN 'BUMP'" + "\n");
            strSqlString.Append("                WHEN A.FACTORY = 'HMKB1' AND B.OPER_GRP_1 IN ('SORT','AVI') THEN 'AVI'" + "\n");
            strSqlString.Append("                WHEN A.FACTORY = 'HMKB1' AND B.OPER_GRP_1 = 'OGI' THEN 'OQC'" + "\n");
            strSqlString.Append("                WHEN A.FACTORY = 'HMKB1' AND B.OPER_GRP_1 IN ('PACKING','HMK3B') THEN 'PACKING'" + "\n");
            strSqlString.Append("                ELSE ''" + "\n");
            strSqlString.Append("           END AS OPER_GRP " + "\n");
            strSqlString.Append("         , CASE WHEN A.FACTORY = '" + GlobalVariable.gsAssyDefaultFactory + "' AND A.MAT_ID LIKE 'SEK%' THEN '�Ｚ�޸�'" + "\n");
            strSqlString.Append("                WHEN A.FACTORY = '" + GlobalVariable.gsAssyDefaultFactory + "' AND A.MAT_ID LIKE 'SES%' THEN 'S-LSI'" + "\n");
            strSqlString.Append("                WHEN A.FACTORY = '" + GlobalVariable.gsAssyDefaultFactory + "' AND A.MAT_ID LIKE 'SEL%' THEN 'S-LSI'" + "\n");
            strSqlString.Append("                WHEN A.FACTORY = '" + GlobalVariable.gsAssyDefaultFactory + "' AND A.MAT_ID LIKE 'HX%' THEN 'HYNIX'" + "\n");
            strSqlString.Append("                WHEN A.FACTORY = '" + GlobalVariable.gsAssyDefaultFactory + "' THEN 'FABLESS'" + "\n");
            strSqlString.Append("                WHEN A.FACTORY = '" + GlobalVariable.gsTestDefaultFactory + "' AND A.MAT_ID LIKE 'SES%' THEN 'S-LSI'" + "\n");
            strSqlString.Append("                WHEN A.FACTORY = '" + GlobalVariable.gsTestDefaultFactory + "' AND A.MAT_ID LIKE 'IM%' THEN 'IML'" + "\n");
            strSqlString.Append("                WHEN A.FACTORY = '" + GlobalVariable.gsTestDefaultFactory + "' AND A.MAT_ID LIKE 'FC%' THEN 'FCI'" + "\n");
            strSqlString.Append("                WHEN A.FACTORY = '" + GlobalVariable.gsTestDefaultFactory + "' THEN '��Ÿ��ü'" + "\n");
            strSqlString.Append("                WHEN A.FACTORY IN ('FGS', 'HMKE1') THEN 'FABLESS'" + "\n");
            strSqlString.Append("                WHEN A.FACTORY = 'HMKB1' AND A.MAT_ID LIKE 'SEK%' THEN '�Ｚ�޸�'" + "\n");
            strSqlString.Append("                WHEN A.FACTORY = 'HMKB1' AND A.MAT_ID LIKE 'SES%' THEN 'S-LSI'" + "\n");
            strSqlString.Append("                WHEN A.FACTORY = 'HMKB1' AND A.MAT_ID LIKE 'HX%' THEN 'HYNIX'" + "\n");
            strSqlString.Append("                WHEN A.FACTORY = 'HMKB1' THEN 'FABLESS'" + "\n");
            strSqlString.Append("                ELSE ''" + "\n");
            strSqlString.Append("           END AS CUST_TYPE" + "\n");
            strSqlString.Append("         , A.* " + "\n");
            strSqlString.Append("         , C.MAT_GRP_1, C.MAT_GRP_2, C.MAT_GRP_3, C.MAT_GRP_4, C.MAT_GRP_5" + "\n");
            strSqlString.Append("         , C.MAT_GRP_6, C.MAT_GRP_7, C.MAT_GRP_8, C.MAT_GRP_9, C.MAT_GRP_10" + "\n");
            strSqlString.Append("         , C.MAT_CMF_10, C.MAT_CMF_11, C.MAT_CMF_13" + "\n");

            if (isToday)
            {
                strSqlString.Append("      FROM RWIPLOTSTS A" + "\n");
                strSqlString.Append("         , MWIPOPRDEF B" + "\n");
                strSqlString.Append("         , MWIPMATDEF C" + "\n");
                strSqlString.Append("     WHERE 1=1" + "\n");
            }
            else
            {
                strSqlString.Append("      FROM RWIPLOTSTS_BOH A" + "\n");
                strSqlString.Append("         , MWIPOPRDEF B" + "\n");
                strSqlString.Append("         , MWIPMATDEF C" + "\n");                
                strSqlString.Append("     WHERE A.CUTOFF_DT = '" + cdvDate.SelectedValue() + "22' " + "\n");

            }

            
            strSqlString.Append("       AND A.FACTORY = B.FACTORY" + "\n");
            strSqlString.Append("       AND A.FACTORY = C.FACTORY" + "\n");
            strSqlString.Append("       AND A.OPER = B.OPER" + "\n");
            strSqlString.Append("       AND A.MAT_ID = C.MAT_ID" + "\n");
            strSqlString.Append("       AND A.FACTORY = '" + cdvFactory.Text + "'" + "\n");
            strSqlString.Append("       AND A.LOT_DEL_FLAG = ' '" + "\n");
            strSqlString.Append("       AND A.LOT_TYPE = 'W'" + "\n");
            strSqlString.Append("       AND A.MAT_ID LIKE '" + txtSearchProduct.Text + "'" + "\n");

            if (cdvLotType.Text != "ALL")
            {
                strSqlString.Append("       AND A.LOT_CMF_5 LIKE '" + cdvLotType.Text + "'" + "\n");
            }

            if (ckbCOB.Checked == true)
            {
                strSqlString.Append("       AND C.MAT_GRP_3 <> 'COB'" + "\n");
            }

            strSqlString.Append(")" + "\n");
            strSqlString.AppendFormat("SELECT {0} " + "\n", QueryCond1);
            strSqlString.Append("     , SUM(LOT_OVER_0) AS LOT_OVER_0" + "\n");
            strSqlString.Append("     , SUM(QTY_OVER_0) AS QTY_OVER_0" + "\n");
            strSqlString.Append("     , SUM(LOT_OVER_1) AS LOT_OVER_1" + "\n");
            strSqlString.Append("     , SUM(QTY_OVER_1) AS QTY_OVER_1" + "\n");
            strSqlString.Append("     , SUM(LOT_OVER_2) AS LOT_OVER_2" + "\n");
            strSqlString.Append("     , SUM(QTY_OVER_2) AS QTY_OVER_2" + "\n");
            strSqlString.Append("     , SUM(LOT_OVER_3) AS LOT_OVER_3" + "\n");
            strSqlString.Append("     , SUM(QTY_OVER_3) AS QTY_OVER_3" + "\n");
            strSqlString.Append("     , SUM(LOT_OVER_7) AS LOT_OVER_7" + "\n");
            strSqlString.Append("     , SUM(QTY_OVER_7) AS QTY_OVER_7" + "\n");
            strSqlString.Append("     , SUM(LOT_TTL) AS LOT_TTL" + "\n");
            strSqlString.Append("     , SUM(QTY_TTL) AS QTY_TTL" + "\n");
            strSqlString.Append("     , DECODE(SUM(WIP_TTL), 0, 0, ROUND(SUM(QTY_HOLD) / SUM(WIP_TTL) * 100, 1)) AS HOLD_RATE" + "\n");
            strSqlString.Append("     , SUM(QTY_HOLD) AS QTY_HOLD" + "\n");
            strSqlString.Append("     , SUM(WIP_TTL) AS WIP_TTL" + "\n");
            strSqlString.Append("  FROM (" + "\n");
            strSqlString.Append("        SELECT OPER_GRP, CUST_TYPE, MAT_ID" + "\n");
            strSqlString.Append("             , SUM(CASE WHEN TAT < 1 THEN 1 ELSE 0 END) AS LOT_OVER_0" + "\n");
            strSqlString.Append("             , SUM(CASE WHEN TAT < 1 THEN QTY_1 ELSE 0 END) AS QTY_OVER_0" + "\n");
            strSqlString.Append("             , SUM(CASE WHEN 1 <= TAT AND TAT < 2 THEN 1 ELSE 0 END) AS LOT_OVER_1" + "\n");
            strSqlString.Append("             , SUM(CASE WHEN 1 <= TAT AND TAT < 2 THEN QTY_1 ELSE 0 END) AS QTY_OVER_1" + "\n");
            strSqlString.Append("             , SUM(CASE WHEN 2 <= TAT AND TAT < 3 THEN 1 ELSE 0 END) AS LOT_OVER_2" + "\n");
            strSqlString.Append("             , SUM(CASE WHEN 2 <= TAT AND TAT < 3 THEN QTY_1 ELSE 0 END) AS QTY_OVER_2" + "\n");
            strSqlString.Append("             , SUM(CASE WHEN 3 <= TAT AND TAT < 7 THEN 1 ELSE 0 END) AS LOT_OVER_3" + "\n");
            strSqlString.Append("             , SUM(CASE WHEN 3 <= TAT AND TAT < 7 THEN QTY_1 ELSE 0 END) AS QTY_OVER_3" + "\n");
            strSqlString.Append("             , SUM(CASE WHEN 7 <= TAT THEN 1 ELSE 0 END) AS LOT_OVER_7" + "\n");
            strSqlString.Append("             , SUM(CASE WHEN 7 <= TAT THEN QTY_1 ELSE 0 END) AS QTY_OVER_7" + "\n");
            strSqlString.Append("             , SUM(CASE WHEN 0 <= TAT THEN 1 ELSE 0 END) AS LOT_TTL" + "\n");
            strSqlString.Append("             , SUM(CASE WHEN 0 <= TAT THEN QTY_1 ELSE 0 END) AS QTY_TTL" + "\n");
            strSqlString.Append("             , SUM(CASE WHEN HOLD_FLAG = 'Y' THEN QTY_1 ELSE 0 END) AS QTY_HOLD " + "\n");
            strSqlString.Append("          FROM DT" + "\n");
            strSqlString.Append("         WHERE 1=1" + "\n");
            strSqlString.Append("           AND TAT >= " + txtHoldDate.Text + "\n");
            

            // Hold (Hold Flag)
            if (cdvHoldKind.Text == "Hold")
                strSqlString.Append("           AND HOLD_FLAG = 'Y' " + "\n");
            else if (cdvHoldKind.Text == "Non Hold")
                strSqlString.Append("           AND HOLD_FLAG = ' ' " + "\n");

            // 2011-05-31-������ : HOLD_CODE �˻� ��� �߰� (������ ��û)
            if (cdvHoldCode.Text != "ALL" && cdvHoldCode.Text != "")
            {
                strSqlString.AppendFormat("           AND HOLD_CODE {0}" + "\n", cdvHoldCode.SelectedValueToQueryString);
            }

            strSqlString.Append("         GROUP BY OPER_GRP, CUST_TYPE, MAT_ID" + "\n");
            strSqlString.Append("       ) A" + "\n");
            strSqlString.Append("     , (" + "\n");
            strSqlString.Append("        SELECT OPER_GRP, CUST_TYPE, MAT_ID, SUM(QTY_1) AS WIP_TTL FROM DT GROUP BY OPER_GRP, CUST_TYPE, MAT_ID" + "\n");
            strSqlString.Append("       ) B" + "\n");
            strSqlString.Append("     , MWIPMATDEF C" + "\n");
            strSqlString.Append(" WHERE A.OPER_GRP = B.OPER_GRP(+)" + "\n");
            strSqlString.Append("   AND A.CUST_TYPE = B.CUST_TYPE(+)" + "\n");
            strSqlString.Append("   AND A.MAT_ID = B.MAT_ID(+)" + "\n");
            strSqlString.Append("   AND A.MAT_ID = C.MAT_ID" + "\n");
            strSqlString.Append("   AND C.FACTORY = '" + cdvFactory.Text + "'" + "\n");

            if (cdvStep.Text != "ALL")
            {
                strSqlString.Append("   AND A.OPER_GRP " + cdvStep.SelectedValueToQueryString + "\n");
            }

            //�� ��ȸ�� ���� SQL�� ����                        
            #region �� ��ȸ�� ���� SQL�� ����
            if (cdvFactory.Text != "HMKB1")
            {
                if (udcWIPCondition1.Text != "ALL" && udcWIPCondition1.Text != "")
                    strSqlString.AppendFormat("   AND C.MAT_GRP_1 {0}" + "\n", udcWIPCondition1.SelectedValueToQueryString);

                if (udcWIPCondition2.Text != "ALL" && udcWIPCondition2.Text != "")
                    strSqlString.AppendFormat("   AND C.MAT_GRP_2 {0} " + "\n", udcWIPCondition2.SelectedValueToQueryString);

                if (udcWIPCondition3.Text != "ALL" && udcWIPCondition3.Text != "")
                    strSqlString.AppendFormat("   AND C.MAT_GRP_3 {0} " + "\n", udcWIPCondition3.SelectedValueToQueryString);

                if (udcWIPCondition4.Text != "ALL" && udcWIPCondition4.Text != "")
                    strSqlString.AppendFormat("   AND C.MAT_GRP_4 {0} " + "\n", udcWIPCondition4.SelectedValueToQueryString);

                if (udcWIPCondition5.Text != "ALL" && udcWIPCondition5.Text != "")
                    strSqlString.AppendFormat("   AND C.MAT_GRP_5 {0} " + "\n", udcWIPCondition5.SelectedValueToQueryString);

                if (udcWIPCondition6.Text != "ALL" && udcWIPCondition6.Text != "")
                    strSqlString.AppendFormat("   AND C.MAT_GRP_6 {0} " + "\n", udcWIPCondition6.SelectedValueToQueryString);

                if (udcWIPCondition7.Text != "ALL" && udcWIPCondition7.Text != "")
                    strSqlString.AppendFormat("   AND C.MAT_GRP_7 {0} " + "\n", udcWIPCondition7.SelectedValueToQueryString);

                if (udcWIPCondition8.Text != "ALL" && udcWIPCondition8.Text != "")
                    strSqlString.AppendFormat("   AND C.MAT_GRP_8 {0} " + "\n", udcWIPCondition8.SelectedValueToQueryString);

                if (udcWIPCondition9.Text != "ALL" && udcWIPCondition9.Text != "")
                    strSqlString.AppendFormat("   AND C.MAT_GRP_9 {0} " + "\n", udcWIPCondition9.SelectedValueToQueryString);
            }
            else
            {
                if (udcBUMPCondition1.Text != "ALL" && udcBUMPCondition1.Text != "")
                    strSqlString.AppendFormat("   AND C.MAT_GRP_1 {0} " + "\n", udcBUMPCondition1.SelectedValueToQueryString);

                if (udcBUMPCondition2.Text != "ALL" && udcBUMPCondition2.Text != "")
                    strSqlString.AppendFormat("   AND C.MAT_GRP_2 {0} " + "\n", udcBUMPCondition2.SelectedValueToQueryString);

                if (udcBUMPCondition3.Text != "ALL" && udcBUMPCondition3.Text != "")
                    strSqlString.AppendFormat("   AND C.MAT_GRP_3 {0} " + "\n", udcBUMPCondition3.SelectedValueToQueryString);

                if (udcBUMPCondition4.Text != "ALL" && udcBUMPCondition4.Text != "")
                    strSqlString.AppendFormat("   AND C.MAT_GRP_4 {0} " + "\n", udcBUMPCondition4.SelectedValueToQueryString);

                if (udcBUMPCondition5.Text != "ALL" && udcBUMPCondition5.Text != "")
                    strSqlString.AppendFormat("   AND C.MAT_GRP_5 {0} " + "\n", udcBUMPCondition5.SelectedValueToQueryString);

                if (udcBUMPCondition6.Text != "ALL" && udcBUMPCondition6.Text != "")
                    strSqlString.AppendFormat("   AND C.MAT_GRP_6 {0} " + "\n", udcBUMPCondition6.SelectedValueToQueryString);

                if (udcBUMPCondition7.Text != "ALL" && udcBUMPCondition7.Text != "")
                    strSqlString.AppendFormat("   AND C.MAT_GRP_7 {0} " + "\n", udcBUMPCondition7.SelectedValueToQueryString);

                if (udcBUMPCondition8.Text != "ALL" && udcBUMPCondition8.Text != "")
                    strSqlString.AppendFormat("   AND C.MAT_GRP_8 {0} " + "\n", udcBUMPCondition8.SelectedValueToQueryString);

                if (udcBUMPCondition9.Text != "ALL" && udcBUMPCondition9.Text != "")
                    strSqlString.AppendFormat("   AND C.MAT_CMF_14 {0} " + "\n", udcBUMPCondition9.SelectedValueToQueryString);

                if (udcBUMPCondition10.Text != "ALL" && udcBUMPCondition10.Text != "")
                    strSqlString.AppendFormat("   AND C.MAT_CMF_2 {0} " + "\n", udcBUMPCondition10.SelectedValueToQueryString);

                if (udcBUMPCondition11.Text != "ALL" && udcBUMPCondition11.Text != "")
                    strSqlString.AppendFormat("   AND C.MAT_CMF_3 {0} " + "\n", udcBUMPCondition11.SelectedValueToQueryString);

                if (udcBUMPCondition12.Text != "ALL" && udcBUMPCondition12.Text != "")
                    strSqlString.AppendFormat("   AND C.MAT_CMF_4 {0} " + "\n", udcBUMPCondition12.SelectedValueToQueryString);
            }
            #endregion

            strSqlString.AppendFormat(" GROUP BY {0} " + "\n", QueryCond1);
            strSqlString.AppendFormat(" ORDER BY {0} " + "\n", QueryCond2);

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
            // Excel �ٷ� ���̱�
            //ExcelHelper.Instance.subMakeExcel(spdData, this.lblTitle.Text, " ^ ", " ^ ", true);
            spdData.ExportExcel();            
        }
        #endregion

        private void cdvFactory_ValueSelectedItemChanged(object sender, MCCodeViewSelChanged_EventArgs e)
        {
            this.SetFactory(cdvFactory.txtValue);
            cdvStep.sFactory = cdvFactory.txtValue;

            if (cdvFactory.Text.Trim() == "HMKB1")
            {
                BaseFormType = eBaseFormType.BUMP_BASE;
                pnlBUMPDetail.Visible = false;
            }
            else
            {
                BaseFormType = eBaseFormType.WIP_BASE;
                pnlWIPDetail.Visible = false;
            }

            SortInit();
            GridColumnInit();
        }

        private void cdvStep_ValueButtonPress(object sender, EventArgs e)
        {
            string strQuery = string.Empty;

            if (cdvFactory.Text == GlobalVariable.gsAssyDefaultFactory)
            {
                strQuery += "SELECT DECODE(ROWNUM, 1, A, 2, B, 3, C, 4, D, 5, E, 6, F, 7, G) AS Code, '' AS Data" + "\n";
                strQuery += "  FROM (SELECT 1 FROM DUAL CONNECT BY LEVEL <= 7) " + "\n";
                strQuery += "     , ( " + "\n";
                strQuery += "        SELECT 'STOCK' AS A" + "\n";
                strQuery += "             , 'DP' AS B, 'DA' AS C " + "\n";
                strQuery += "             , 'WB' AS D, 'GATE' AS E " + "\n";
                strQuery += "             , 'MOLD' AS F, 'FINISH' AS G " + "\n";
                strQuery += "           FROM DUAL " + "\n";
                strQuery += "       ) " + "\n";
            }
            else if (cdvFactory.Text == "HMKB1")
            {
                strQuery += "SELECT DECODE(ROWNUM, 1, A, 2, B, 3, C, 4, D, 5, E, 6, F, 7, G, 8, H, 9, I, 10, J, 11, K, 12, L) AS Code, '' AS Data" + "\n";
                strQuery += "  FROM (SELECT 1 FROM DUAL CONNECT BY LEVEL <= 12) " + "\n";
                strQuery += "     , ( " + "\n";
                strQuery += "        SELECT 'Incomming' AS A" + "\n";
                strQuery += "             , 'RCF' AS B, 'RDL1' AS C " + "\n";
                strQuery += "             , 'PSV1' AS D, 'RDL2' AS E " + "\n";
                strQuery += "             , 'PSV2' AS F, 'RDL3' AS G " + "\n";
                strQuery += "             , 'PSV3' AS H, 'BUMP' AS I " + "\n";
                strQuery += "             , 'AVI' AS J, 'OQC' AS K " + "\n";
                strQuery += "             , 'PACKING' AS L " + "\n";
                strQuery += "           FROM DUAL " + "\n";
                strQuery += "       ) " + "\n";
            }
            else
            {
                strQuery += "SELECT DECODE(ROWNUM, 1, A, 2, B, 3, C) AS Code, '' AS Data" + "\n";
                strQuery += "  FROM (SELECT 1 FROM DUAL CONNECT BY LEVEL <= 3) " + "\n";
                strQuery += "     , ( " + "\n";
                strQuery += "        SELECT 'STOCK' AS A" + "\n";
                strQuery += "             , 'TEST' AS B, 'MVP' AS C " + "\n";                
                strQuery += "           FROM DUAL " + "\n";
                strQuery += "       ) " + "\n";
            }

            cdvStep.sDynamicQuery = strQuery;
        }

        private void spdData_CellClick(object sender, FarPoint.Win.Spread.CellClickEventArgs e)
        {
            string sHoldDay = null;
            string[] dataArry = new string[13];

            // �׷������� �ƴϰ� HOLD RATE �� �ƴϸ鼭 Null ���� �ƴѰ�� Ŭ�� �� �˾� â ���.
            if (e.Column >= 13 && e.Column != 25 && spdData.ActiveSheet.Cells[e.Row, e.Column].Text != "")
            {
                Color BackColor = spdData.ActiveSheet.Cells[1, 13].BackColor;

                // GrandTotal �� RAW Data
                if (e.Row == 0 || spdData.ActiveSheet.Cells[e.Row, 13].BackColor == BackColor)
                {

                    // Group ���� �����ͼ� ���... �� �˾����� ���ǰ����� ����ϱ� ����
                    for (int i = 0; i < 13; i++)
                    {
                        dataArry[i] = spdData.ActiveSheet.Cells[e.Row, i].Value.ToString();
                    }
                }
                else // SubTotal
                {
                    dataArry[0] = spdData.ActiveSheet.Cells[e.Row, 0].Value.ToString();
                }

                //// ���� ���� ���� �ڵ�� �����ϱ� ����..
                //if (dataArry[0] != " ")
                //{
                //    DataTable dtCustomer = CmnFunction.oComm.GetFuncDataTable("DYNAMIC", MakeSqlCustomer(dataArry[0].ToString()));

                //    dataArry[0] = dtCustomer.Rows[0][0].ToString();
                //}

                if (e.Column == 13 || e.Column == 14)
                    sHoldDay = "0";
                else if (e.Column == 15 || e.Column == 16)
                    sHoldDay = "1";
                else if (e.Column == 17 || e.Column == 18)
                    sHoldDay = "2";
                else if (e.Column == 19 || e.Column == 20)
                    sHoldDay = "3";
                else if (e.Column == 21 || e.Column == 22)
                    sHoldDay = "7";
                else sHoldDay = "TTL";
                

                DataTable dt = CmnFunction.oComm.GetFuncDataTable("DYNAMIC", MakeSqlDetail(dataArry, sHoldDay));

                if (dt != null && dt.Rows.Count > 0)
                {
                    System.Windows.Forms.Form frm = new PRD010309_P1("", dt, cdvFactory.Text);
                    frm.ShowDialog();
                }                
            }
            else
            {
                return;
            }
        }

        private string MakeSqlDetail(string[] dataArry, string sHoldDay)
        {
            StringBuilder strSqlString = new StringBuilder();

            udcTableForm tableForm = (udcTableForm)btnSort.BindingForm;
            //ArrayList list = tableForm.SelectedValueToArrayListToValuedTreeNode; 
            string[] list = tableForm.SelectedValue3ToQueryContainNull.Split(',');
            bool isToday = false;

            string strDate = cdvDate.Value.ToString("yyyyMMdd");

            if (DateTime.Now.ToString("yyyyMMdd").Equals(strDate))
            {
                strDate = DateTime.Now.ToString("yyyyMMddHHmmss");
                isToday = true;
            }
            else
            {
                strDate = strDate + "220000";
            }


            strSqlString.Append("WITH DT AS" + "\n");
            strSqlString.Append("(" + "\n");

            if (cdvFactory.Text != "HMKB1")
            {
                if (rbtCreate.Checked == true)
                    strSqlString.Append("    SELECT TRUNC(TO_DATE('" + strDate + "','YYYYMMDDHH24MISS') - TO_DATE(A.LOT_CMF_14,'YYYYMMDDHH24MISS'), 2) AS TAT" + "\n");
                else if (rbtIssue.Checked == true)
                    strSqlString.Append("    SELECT TRUNC(TO_DATE('" + strDate + "','YYYYMMDDHH24MISS') - DECODE(A.RESV_FIELD_1, ' ', SYSDATE, TO_DATE(A.RESV_FIELD_1,'YYYYMMDDHH24MISS')), 2) AS TAT " + "\n");
                else
                    strSqlString.Append("    SELECT TRUNC(TO_DATE('" + strDate + "','YYYYMMDDHH24MISS') - TO_DATE(A.OPER_IN_TIME,'YYYYMMDDHH24MISS'), 2) AS TAT " + "\n");
            } else
            {
                strSqlString.Append("    SELECT TRUNC(TO_DATE('" + strDate + "','YYYYMMDDHH24MISS') - TO_DATE(A.LOT_CMF_14,'YYYYMMDDHH24MISS'), 2) AS FAC_TAT" + "\n");
                strSqlString.Append("         , TRUNC(TO_DATE('" + strDate + "','YYYYMMDDHH24MISS') - DECODE(A.RESV_FIELD_1, ' ', SYSDATE, TO_DATE(A.RESV_FIELD_1,'YYYYMMDDHH24MISS')), 2) AS ISSUE_TAT " + "\n");
                strSqlString.Append("         , TRUNC(TO_DATE('" + strDate + "','YYYYMMDDHH24MISS') - TO_DATE(A.OPER_IN_TIME,'YYYYMMDDHH24MISS'), 2) AS TAT " + "\n");
            }

            strSqlString.Append("         , CASE WHEN A.FACTORY = '" + GlobalVariable.gsAssyDefaultFactory + "' AND B.OPER_GRP_1 = 'HMK2A' THEN 'STOCK'" + "\n");
            strSqlString.Append("                WHEN A.FACTORY = '" + GlobalVariable.gsAssyDefaultFactory + "' AND B.OPER_GRP_1 IN ('B/G','SAW', 'Back Side Coating') THEN 'DP'" + "\n");
            strSqlString.Append("                WHEN A.FACTORY = '" + GlobalVariable.gsAssyDefaultFactory + "' AND B.OPER_GRP_1 IN ('D/A','SMT', 'S/P') THEN 'DA'" + "\n");
            strSqlString.Append("                WHEN A.FACTORY = '" + GlobalVariable.gsAssyDefaultFactory + "' AND B.OPER_GRP_1 IN ('W/B','GATE') THEN 'WB'" + "\n");
            strSqlString.Append("                WHEN A.FACTORY = '" + GlobalVariable.gsAssyDefaultFactory + "' AND B.OPER_GRP_1 IN ('MOLD','CURE') THEN 'MOLD'" + "\n");
            strSqlString.Append("                WHEN A.FACTORY = '" + GlobalVariable.gsAssyDefaultFactory + "' THEN 'FINISH'" + "\n");
            strSqlString.Append("                WHEN A.FACTORY = '" + GlobalVariable.gsTestDefaultFactory + "' AND B.OPER_GRP_1 = 'HMK3T' THEN 'STOCK'" + "\n");
            strSqlString.Append("                WHEN A.FACTORY = '" + GlobalVariable.gsTestDefaultFactory + "' AND B.OPER_GRP_1 IN ('TEST', 'CAS', 'OS') THEN 'TEST'" + "\n");
            strSqlString.Append("                WHEN A.FACTORY = '" + GlobalVariable.gsTestDefaultFactory + "' THEN 'MVP'" + "\n");
            strSqlString.Append("                WHEN A.FACTORY = 'FGS' AND B.OPER = 'F0000' THEN 'STOCK'" + "\n");
            strSqlString.Append("                WHEN A.FACTORY = 'HMKE1' AND B.OPER_GRP_1 = 'HMKE1' THEN 'STOCK'" + "\n");
            strSqlString.Append("                WHEN A.FACTORY = 'HMKE1' AND B.OPER_GRP_1 = 'TEST' THEN 'TEST'" + "\n");
            strSqlString.Append("                WHEN A.FACTORY = 'HMKE1' AND B.OPER_GRP_1 IN ('BAKE', 'QA', 'P/K', 'HMKE2') THEN 'MVP'" + "\n");
            strSqlString.Append("                WHEN A.FACTORY = 'HMKB1' AND B.OPER_GRP_1 IN ('HMK2B','IQC', 'I-STOCK') THEN 'Incoming'" + "\n");
            strSqlString.Append("                WHEN A.FACTORY = 'HMKB1' AND B.OPER_GRP_1 = 'RCF_PHOTO' THEN 'RCF'" + "\n");
            strSqlString.Append("                WHEN A.FACTORY = 'HMKB1' AND B.OPER_GRP_1 IN ('RDL1_SPUTTER','RDL1_PHOTO', 'RDL1_PLAT', 'RDL1_ETCH') THEN 'RDL1'" + "\n");
            strSqlString.Append("                WHEN A.FACTORY = 'HMKB1' AND B.OPER_GRP_1 = 'PSV1_PHOTO' THEN 'PSV1'" + "\n");
            strSqlString.Append("                WHEN A.FACTORY = 'HMKB1' AND B.OPER_GRP_1 IN ('RDL2_SPUTTER','RDL2_PHOTO', 'RDL2_PLAT', 'RDL2_ETCH') THEN 'RDL2'" + "\n");
            strSqlString.Append("                WHEN A.FACTORY = 'HMKB1' AND B.OPER_GRP_1 = 'PSV2_PHOTO' THEN 'PSV2'" + "\n");
            strSqlString.Append("                WHEN A.FACTORY = 'HMKB1' AND B.OPER_GRP_1 IN ('RDL3_SPUTTER','RDL3_PHOTO', 'RDL3_PLAT', 'RDL3_ETCH') THEN 'RDL3'" + "\n");
            strSqlString.Append("                WHEN A.FACTORY = 'HMKB1' AND B.OPER_GRP_1 = 'PSV3_PHOTO' THEN 'PSV3'" + "\n");
            strSqlString.Append("                WHEN A.FACTORY = 'HMKB1' AND B.OPER_GRP_1 IN ('BUMP_SPUTTER','BUMP_PHOTO','BUMP_CU_PLAT','BUMP_SNAG_PLAT','BUMP_ETCH','BUMP_BALL_MOUNT','BUMP_REFLOW','FINAL_INSP') THEN 'BUMP'" + "\n");
            strSqlString.Append("                WHEN A.FACTORY = 'HMKB1' AND B.OPER_GRP_1 IN ('SORT','AVI') THEN 'AVI'" + "\n");
            strSqlString.Append("                WHEN A.FACTORY = 'HMKB1' AND B.OPER_GRP_1 = 'OGI' THEN 'OQC'" + "\n");
            strSqlString.Append("                WHEN A.FACTORY = 'HMKB1' AND B.OPER_GRP_1 IN ('PACKING','HMK3B') THEN 'PACKING'" + "\n");
            strSqlString.Append("                ELSE ''" + "\n");
            strSqlString.Append("           END AS OPER_GRP " + "\n");
            strSqlString.Append("         , CASE WHEN A.FACTORY = '" + GlobalVariable.gsAssyDefaultFactory + "' AND A.MAT_ID LIKE 'SEK%' THEN '�Ｚ�޸�'" + "\n");
            strSqlString.Append("                WHEN A.FACTORY = '" + GlobalVariable.gsAssyDefaultFactory + "' AND A.MAT_ID LIKE 'SES%' THEN 'S-LSI'" + "\n");
            strSqlString.Append("                WHEN A.FACTORY = '" + GlobalVariable.gsAssyDefaultFactory + "' AND A.MAT_ID LIKE 'HX%' THEN 'HYNIX'" + "\n");
            strSqlString.Append("                WHEN A.FACTORY = '" + GlobalVariable.gsAssyDefaultFactory + "' THEN 'FABLESS'" + "\n");
            strSqlString.Append("                WHEN A.FACTORY = '" + GlobalVariable.gsTestDefaultFactory + "' AND A.MAT_ID LIKE 'SES%' THEN 'S-LSI'" + "\n");
            strSqlString.Append("                WHEN A.FACTORY = '" + GlobalVariable.gsTestDefaultFactory + "' AND A.MAT_ID LIKE 'IM%' THEN 'IML'" + "\n");
            strSqlString.Append("                WHEN A.FACTORY = '" + GlobalVariable.gsTestDefaultFactory + "' AND A.MAT_ID LIKE 'FC%' THEN 'FCI'" + "\n");
            strSqlString.Append("                WHEN A.FACTORY = '" + GlobalVariable.gsTestDefaultFactory + "' THEN '��Ÿ��ü'" + "\n");
            strSqlString.Append("                WHEN A.FACTORY IN ('FGS', 'HMKE1') THEN 'FABLESS'" + "\n");
            strSqlString.Append("                WHEN A.FACTORY = 'HMKB1' AND A.MAT_ID LIKE 'SEK%' THEN '�Ｚ�޸�'" + "\n");
            strSqlString.Append("                WHEN A.FACTORY = 'HMKB1' AND A.MAT_ID LIKE 'SES%' THEN 'S-LSI'" + "\n");
            strSqlString.Append("                WHEN A.FACTORY = 'HMKB1' AND A.MAT_ID LIKE 'HX%' THEN 'HYNIX'" + "\n");
            strSqlString.Append("                WHEN A.FACTORY = 'HMKB1' THEN 'FABLESS'" + "\n");
            strSqlString.Append("                ELSE ''" + "\n");
            strSqlString.Append("           END AS CUST_TYPE" + "\n");
            strSqlString.Append("         , A.* " + "\n");
            strSqlString.Append("         , C.MAT_GRP_1, C.MAT_GRP_2, C.MAT_GRP_3, C.MAT_GRP_4, C.MAT_GRP_5" + "\n");
            strSqlString.Append("         , C.MAT_GRP_6, C.MAT_GRP_7, C.MAT_GRP_8, C.MAT_GRP_9, C.MAT_GRP_10" + "\n");
            strSqlString.Append("         , C.MAT_CMF_10, C.MAT_CMF_11, C.MAT_CMF_13" + "\n");

            if (isToday)
            {
                strSqlString.Append("      FROM RWIPLOTSTS A" + "\n");
                strSqlString.Append("         , MWIPOPRDEF B" + "\n");
                strSqlString.Append("         , MWIPMATDEF C" + "\n");
                strSqlString.Append("     WHERE 1=1" + "\n");
            }
            else
            {
                strSqlString.Append("      FROM RWIPLOTSTS_BOH A" + "\n");
                strSqlString.Append("         , MWIPOPRDEF B" + "\n");
                strSqlString.Append("         , MWIPMATDEF C" + "\n");
                strSqlString.Append("     WHERE A.CUTOFF_DT = '" + cdvDate.SelectedValue() + "22' " + "\n");

            }

            strSqlString.Append("       AND A.FACTORY = B.FACTORY" + "\n");
            strSqlString.Append("       AND A.FACTORY = C.FACTORY" + "\n");
            strSqlString.Append("       AND A.OPER = B.OPER" + "\n");
            strSqlString.Append("       AND A.MAT_ID = C.MAT_ID" + "\n");
            strSqlString.Append("       AND A.FACTORY = '" + cdvFactory.Text + "'" + "\n");
            strSqlString.Append("       AND A.LOT_DEL_FLAG = ' '" + "\n");
            strSqlString.Append("       AND A.LOT_TYPE = 'W'" + "\n");
            strSqlString.Append("       AND A.MAT_ID LIKE '" + txtSearchProduct.Text + "'" + "\n");

            if (cdvLotType.Text != "ALL")
            {
                strSqlString.Append("       AND A.LOT_CMF_5 LIKE '" + cdvLotType.Text + "'" + "\n");
            }

            strSqlString.Append(")" + "\n");

            if (cdvFactory.Text != "HMKB1")
            {
                strSqlString.Append("SELECT NVL(B.DATA_6, ' ') AS ACTION_DEPT" + "\n");
                strSqlString.Append("     , MAT_GRP_1 AS CUSTOMER" + "\n");
                strSqlString.Append("     , OPER, MAT_CMF_10 AS PIN_TYPE" + "\n");
                strSqlString.Append("     , MAT_ID AS PRODUCT" + "\n");
                strSqlString.Append("     , LOT_ID" + "\n");
                strSqlString.Append("     , LOT_CMF_5" + "\n");
                strSqlString.Append("     , CASE WHEN HOLD_FLAG = 'Y' THEN 'HOLD'" + "\n");
                strSqlString.Append("            ELSE DECODE(LOT_STATUS, 'PROC', 'RUN', LOT_STATUS)" + "\n");
                strSqlString.Append("       END AS STATUS" + "\n");
                strSqlString.Append("     , HOLD_CODE" + "\n");
                strSqlString.Append("     , NVL(B.DATA_1, ' ') AS HOLD_DESC" + "\n");
                strSqlString.Append("     , NVL(B.DATA_7, ' ') AS RELEASE_DEPT" + "\n");
                strSqlString.Append("     , TAT" + "\n");
                strSqlString.Append("     , B.DATA_5 / 24 AS TAT_TARGET" + "\n");
                strSqlString.Append("     , (B.DATA_5 / 24) - TAT AS TAT_DEF" + "\n");
                strSqlString.Append("     , QTY_1" + "\n");
                strSqlString.Append("     , LAST_COMMENT" + "\n");
                strSqlString.Append("  FROM DT A" + "\n");
                strSqlString.Append("     , MGCMTBLDAT B" + "\n");
                //strSqlString.Append(" WHERE A.TAT >= 1" + "\n");
                strSqlString.Append(" WHERE A.TAT >= " + txtHoldDate.Text + "\n");

                strSqlString.Append("   AND A.FACTORY = B.FACTORY(+) " + "\n");
                strSqlString.Append("   AND A.HOLD_CODE = B.KEY_1(+) " + "\n");
                strSqlString.Append("   AND B.TABLE_NAME(+) = 'HOLD_CODE' " + "\n");


                if (sHoldDay == "0")
                    strSqlString.Append("   AND A.TAT >= 0 AND A.TAT < 1 " + "\n");
                else if (sHoldDay == "1")
                    strSqlString.Append("   AND A.TAT >= 1 AND A.TAT < 2 " + "\n");
                else if (sHoldDay == "2")
                    strSqlString.Append("   AND A.TAT >= 2 AND A.TAT < 3 " + "\n");
                else if (sHoldDay == "3")
                    strSqlString.Append("   AND A.TAT >= 3 AND A.TAT < 7 " + "\n");
                else if (sHoldDay == "7")
                    strSqlString.Append("   AND A.TAT >= 7 " + "\n");

                // Hold (Hold Flag)
                if (cdvHoldKind.Text == "Hold")
                    strSqlString.Append("   AND A.HOLD_FLAG = 'Y' " + "\n");
                else if (cdvHoldKind.Text == "Non Hold")
                    strSqlString.Append("   AND A.HOLD_FLAG = ' ' " + "\n");

                if (cdvHoldCode.Text != "ALL" && cdvHoldCode.Text != "")
                {
                    strSqlString.AppendFormat("   AND A.HOLD_CODE {0}" + "\n", cdvHoldCode.SelectedValueToQueryString);
                }

                if (dataArry[0].Contains(" Sub Total"))
                {
                    strSqlString.AppendFormat("   AND {0} = '{1}'" + "\n", list[0].Trim(), dataArry[0].Replace(" Sub Total", ""));
                }
                else if (dataArry[0] != "Total")
                {
                    for (int i = 0; i < list.Length; i++)
                    {
                        if (list[i].Trim() != "' '")
                        {
                            strSqlString.AppendFormat("   AND {0} = '{1}'" + "\n", list[i].Trim(), dataArry[i]);
                        }
                    }
                }

                strSqlString.Append(" ORDER BY ACTION_DEPT, CUSTOMER, OPER, PIN_TYPE, PRODUCT, LOT_ID " + "\n");
            }
            else
            {
                strSqlString.Append("SELECT OPER, MAT_GRP_3 AS PROCESS_TYPE, MAT_ID AS PRODUCT " + "\n");
                strSqlString.Append("     , LOT_ID" + "\n");
                strSqlString.Append("     , LOT_CMF_5" + "\n");
                strSqlString.Append("     , CASE WHEN HOLD_FLAG = 'Y' THEN 'HOLD'" + "\n");
                strSqlString.Append("            ELSE DECODE(LOT_STATUS, 'PROC', 'RUN', LOT_STATUS)" + "\n");
                strSqlString.Append("       END AS STATUS" + "\n");
                strSqlString.Append("     , HOLD_CODE" + "\n");
                strSqlString.Append("     , NVL(B.DATA_1, ' ') AS HOLD_DESC" + "\n");
                strSqlString.Append("     , NVL(B.DATA_6, ' ') AS ACTION_DEPT" + "\n");
                strSqlString.Append("     , FAC_TAT" + "\n");       // ��� (��)
                strSqlString.Append("     , ISSUE_TAT" + "\n");     // ISSUE (��)
                strSqlString.Append("     , TAT" + "\n");           // ���� (��)
                strSqlString.Append("     , TAT" + "\n");           // ���� ��ü �ϼ�
                strSqlString.Append("     , NVL(B.DATA_7, ' ') AS RELEASE_DEPT" + "\n");
                strSqlString.Append("     , DECODE(B.DATA_5, ' ', 0,B.DATA_5)  / 24 AS TAT_TARGET" + "\n");
                strSqlString.Append("     , (DECODE(B.DATA_5, ' ', 0,B.DATA_5) / 24) - TAT AS TAT_DEF" + "\n");
                strSqlString.Append("     , QTY_1" + "\n");
                strSqlString.Append("     , LAST_COMMENT" + "\n");
                strSqlString.Append("  FROM DT A" + "\n");
                strSqlString.Append("     , MGCMTBLDAT B" + "\n");
                strSqlString.Append(" WHERE A.TAT >= " + txtHoldDate.Text + "\n");

                strSqlString.Append("   AND A.FACTORY = B.FACTORY(+) " + "\n");
                strSqlString.Append("   AND A.HOLD_CODE = B.KEY_1(+) " + "\n");
                strSqlString.Append("   AND B.TABLE_NAME(+) = 'HOLD_CODE' " + "\n");


                if (rbtCreate.Checked == true)
                {
                    if (sHoldDay == "0")
                        strSqlString.Append("   AND A.FAC_TAT >= 0 AND A.FAC_TAT < 1 " + "\n");
                    else if (sHoldDay == "1")
                        strSqlString.Append("   AND A.FAC_TAT >= 1 AND A.FAC_TAT < 2 " + "\n");
                    else if (sHoldDay == "2")
                        strSqlString.Append("   AND A.FAC_TAT >= 2 AND A.FAC_TAT < 3 " + "\n");
                    else if (sHoldDay == "3")
                        strSqlString.Append("   AND A.FAC_TAT >= 3 AND A.FAC_TAT < 7 " + "\n");
                    else if (sHoldDay == "7")
                        strSqlString.Append("   AND A.FAC_TAT >= 7 " + "\n");
                }
                else if (rbtIssue.Checked == true)
                {
                    if (sHoldDay == "0")
                        strSqlString.Append("   AND A.ISSUE_TAT >= 0 AND A.ISSUE_TAT < 1 " + "\n");
                    else if (sHoldDay == "1")
                        strSqlString.Append("   AND A.ISSUE_TAT >= 1 AND A.ISSUE_TAT < 2 " + "\n");
                    else if (sHoldDay == "2")
                        strSqlString.Append("   AND A.ISSUE_TAT >= 2 AND A.ISSUE_TAT < 3 " + "\n");
                    else if (sHoldDay == "3")
                        strSqlString.Append("   AND A.ISSUE_TAT >= 3 AND A.ISSUE_TAT < 7 " + "\n");
                    else if (sHoldDay == "7")
                        strSqlString.Append("   AND A.ISSUE_TAT >= 7 " + "\n");
                }
                else
                {
                    if (sHoldDay == "0")
                        strSqlString.Append("   AND A.TAT >= 0 AND A.TAT < 1 " + "\n");
                    else if (sHoldDay == "1")
                        strSqlString.Append("   AND A.TAT >= 1 AND A.TAT < 2 " + "\n");
                    else if (sHoldDay == "2")
                        strSqlString.Append("   AND A.TAT >= 2 AND A.TAT < 3 " + "\n");
                    else if (sHoldDay == "3")
                        strSqlString.Append("   AND A.TAT >= 3 AND A.TAT < 7 " + "\n");
                    else if (sHoldDay == "7")
                        strSqlString.Append("   AND A.TAT >= 7 " + "\n");
                }

                // Hold (Hold Flag)
                if (cdvHoldKind.Text == "Hold")
                    strSqlString.Append("   AND A.HOLD_FLAG = 'Y' " + "\n");
                else if (cdvHoldKind.Text == "Non Hold")
                    strSqlString.Append("   AND A.HOLD_FLAG = ' ' " + "\n");

                if (cdvHoldCode.Text != "ALL" && cdvHoldCode.Text != "")
                {
                    strSqlString.AppendFormat("   AND A.HOLD_CODE {0}" + "\n", cdvHoldCode.SelectedValueToQueryString);
                }

                if (dataArry[0].Contains(" Sub Total"))
                {
                    strSqlString.AppendFormat("   AND {0} = '{1}'" + "\n", list[0].Trim(), dataArry[0].Replace(" Sub Total", ""));
                }
                else if (dataArry[0] != "Total")
                {
                    for (int i = 0; i < list.Length; i++)
                    {
                        if (list[i].Trim() != "' '")
                        {
                            strSqlString.AppendFormat("   AND {0} = '{1}'" + "\n", list[i].Trim(), dataArry[i]);
                        }
                    }
                }

                strSqlString.Append(" ORDER BY ACTION_DEPT, OPER, PRODUCT, LOT_ID " + "\n");
            }

            return strSqlString.ToString();
        }
    }
}

