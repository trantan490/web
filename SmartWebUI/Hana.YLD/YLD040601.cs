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

namespace Hana.YLD
{
    public partial class YLD040601 : Miracom.SmartWeb.UI.Controls.udcCUSReport002
    {
        private DataTable dtLoss = null;

        /// <summary>
        /// Ŭ  ��  ��: YLD040601<br/>
        /// Ŭ�������: LOT �� ���� ��ȸ<br/>
        /// ��  ��  ��: �ϳ�����ũ�� ���ؿ�<br/>
        /// �����ۼ���: 2009-02-04<br/>
        /// ��  ����: LOT �� ���� ��ȸ.<br/>
        /// MES Report ���� �� �Ϲ����� ������ ȭ���� �ƴ� ����ڵ��� ������� Excel Raw Data ������ ���� ��Ŀ� ���߾� ���ߵ�<br />
        /// MES Client > General Code Setup �� ���ؼ� LOSS_CODE �� Display Seq �� Display Flag  �� �����Ͽ� ȭ��ǥ�� ���� �� ǥ�� ���θ� ������ �� ���� <br />
        /// 
        /// ����  ����: <br/>
        /// 2010-04-16-��ο� : �����κ� ���� (������ ��û)
        /// 2010-11-01-��ο� : �˻� ���ǿ� ���� �߰� (������ ��û)
        /// 2010-11-05-��ο� : '" + GlobalVariable.gsTestDefaultFactory + "'�� YIELD ��ȸ �߰�
        /// 2011-05-03-��ο� : ������ 0 ���� ����
        /// 2011-07-18-����� : A2200����(I.R Inspection)������ Comment �߰� (�̱���S ��û)
        /// 2011-09-14-����� : I.R Inspection üũ ��, A2050�������� ���� (�̱���S ��û)
        /// 2011-10-12-������ : W/B Multi �۾��� �ڽ� Lot �� Loss ���� �� Lot�� ���� ��Ű���� ���� (��âȣ D ��û)
        /// 2013-05-23-������ : 1Lot 1�Ű������� ���� ASSY Data ���� ���� : SHIP -> CLOSE (���̼�, ���¼� ��û)
        /// 2013-10-17-��ο� : LOT TYPE ALL, P%, E% �������κ���
        /// 2019-04-02-������ : Bump �߰�
        /// 2019-04-23-������ : Bump YMS �����͸� �̿��� �ű� ������� ���� (�Ѽ�ȣ���� ��û)
        /// 2019-04-26-������ : Assy ���� ����. RWIPLOTHIS -> CWIPLOTEND �� ����
        /// 2019-06-03-������ : Bump Merge �� Lot �����͵� ǥ�õǵ��� ���� (�Ѽ�ȣ���� ��û)
        /// 2020-06-24-��̰� : slot_no ������ �־� ���� (���缺 ���ӿ�û)
        /// </summary>
        public YLD040601()
        {
            InitializeComponent();
            OptionInit(); // �ʱ�ȭ
            SortInit(); // group option �ʱ�ȭ
            GridColumnInit(); //��� ����¥��
        }

        #region 0.�ʱ�ȭ
        /// <summary>
        /// 0.�ʱ�ȭ
        /// </summary>
        private void OptionInit()
        {
            dtLoss = new DataTable(); // Loss Code Table 
            GetLossDT(); // Loss Table ��������

            //this.SetFactory(GlobalVariable.gsAssyDefaultFactory); // Assembly �� ����
            //cdvFactory.Text = GlobalVariable.gsAssyDefaultFactory;
            //cdvStartDate.Value = DateTime.Today.AddDays(-2);
            //cdvEndDate.Value = DateTime.Today.AddDays(-1);

            //udcDate.= DateTime.Today.AddDays(-2).ToString() + "220000";
            //udcDate.End_Tran_Time = DateTime.Today.AddDays(-1).ToString() + "215959";
            udcDate.AutoBinding(DateTime.Now.AddDays(-2).ToString(), DateTime.Now.AddDays(-1).ToString());

        }
        #endregion

        #region 1.SortInit

        /// <summary>
        /// 1.SortInit
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SortInit()
        {
            ((udcTableForm)(this.btnSort.BindingForm)).AddRow("Customer", "SUMMERY.MAT_GRP_1", "MAT.MAT_GRP_1", false);
            ((udcTableForm)(this.btnSort.BindingForm)).AddRow("Family", "SUMMERY.MAT_GRP_2", "MAT.MAT_GRP_2", false);
            ((udcTableForm)(this.btnSort.BindingForm)).AddRow("LD Count", "SUMMERY.MAT_GRP_6", "MAT.MAT_GRP_6", true);
            ((udcTableForm)(this.btnSort.BindingForm)).AddRow("Package", "SUMMERY.MAT_GRP_3", "MAT.MAT_GRP_3", true);
            ((udcTableForm)(this.btnSort.BindingForm)).AddRow("Type1", "SUMMERY.MAT_GRP_4", "MAT.MAT_GRP_4", false);
            ((udcTableForm)(this.btnSort.BindingForm)).AddRow("Type2", "SUMMERY.MAT_GRP_5", "MAT.MAT_GRP_5", false);
            ((udcTableForm)(this.btnSort.BindingForm)).AddRow("Density", "SUMMERY.MAT_GRP_7", "MAT.MAT_GRP_7", false);
            ((udcTableForm)(this.btnSort.BindingForm)).AddRow("Generation", "SUMMERY.MAT_GRP_8", "MAT.MAT_GRP_8", false);
            ((udcTableForm)(this.btnSort.BindingForm)).AddRow("Product", "SUMMERY.MAT_ID", "A.MAT_ID", true);
        }

        #endregion

        #region 2.�����������

        /// <summary>
        /// 2.�����������
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void GridColumnInit()
        {
            //spdData.ActiveSheet.ColumnHeader.Rows.Count = 1;
            //spdData.ActiveSheet.Columns.Remove(0, spdData.ActiveSheet.Columns.Count);

            spdData.RPT_ColumnInit();

            if (cdvFactory.Text == "HMKB1")
            {
                spdData.RPT_AddBasicColumn("Cust Code", 0, 0, Visibles.True, Frozen.True, Align.Center, Merge.True, Formatter.String, 60);
                spdData.RPT_AddBasicColumn("Device", 0, 1, Visibles.True, Frozen.True, Align.Left, Merge.True, Formatter.String, 80);
                spdData.RPT_AddBasicColumn("Run ID", 0, 2, Visibles.True, Frozen.True, Align.Left, Merge.True, Formatter.String, 80);
                spdData.RPT_AddBasicColumn("Lot ID", 0, 3, Visibles.True, Frozen.True, Align.Left, Merge.True, Formatter.String, 80);                
                spdData.RPT_AddBasicColumn("IN TIME", 0, 4, Visibles.True, Frozen.True, Align.Left, Merge.True, Formatter.String, 120);
                spdData.RPT_AddBasicColumn("SHIP TIME", 0, 5, Visibles.True, Frozen.True, Align.Left, Merge.True, Formatter.String, 120);
                spdData.RPT_AddBasicColumn("Wafer No.", 0, 6, Visibles.True, Frozen.True, Align.Center, Merge.False, Formatter.String, 50);
                spdData.RPT_AddBasicColumn("Net Die", 0, 7, Visibles.True, Frozen.False, Align.Left, Merge.False, Formatter.Number, 80);
                spdData.RPT_AddBasicColumn("Good Die", 0, 8, Visibles.True, Frozen.False, Align.Left, Merge.False, Formatter.Number, 80);
                spdData.RPT_AddBasicColumn("Fail Die", 0, 9, Visibles.True, Frozen.False, Align.Left, Merge.False, Formatter.Number, 80);
                spdData.RPT_AddBasicColumn("Yield", 0, 10, Visibles.True, Frozen.False, Align.Left, Merge.False, Formatter.Double2, 80);
                spdData.RPT_AddBasicColumn("Bump Yield", 0, 11, Visibles.True, Frozen.False, Align.Left, Merge.False, Formatter.Double2, 80);
                spdData.RPT_AddBasicColumn("Raw Material", 0, 12, Visibles.True, Frozen.False, Align.Left, Merge.False, Formatter.String, 80);
                spdData.RPT_AddBasicColumn("Foreign Material", 0, 13, Visibles.True, Frozen.False, Align.Left, Merge.False, Formatter.String, 80);
                spdData.RPT_AddBasicColumn("Big/Small Bump", 0, 14, Visibles.True, Frozen.False, Align.Left, Merge.False, Formatter.String, 80);
                spdData.RPT_AddBasicColumn("Missing Bump", 0, 15, Visibles.True, Frozen.False, Align.Left, Merge.False, Formatter.String, 80);
                spdData.RPT_AddBasicColumn("Bridged Bump", 0, 16, Visibles.True, Frozen.False, Align.Left, Merge.False, Formatter.String, 80);
                spdData.RPT_AddBasicColumn("Lump", 0, 17, Visibles.True, Frozen.False, Align.Left, Merge.False, Formatter.String, 80);
                spdData.RPT_AddBasicColumn("Metal Unetch", 0, 18, Visibles.True, Frozen.False, Align.Left, Merge.False, Formatter.String, 80);
                spdData.RPT_AddBasicColumn("Scratch", 0, 19, Visibles.True, Frozen.False, Align.Left, Merge.False, Formatter.String, 80);
                spdData.RPT_AddBasicColumn("Parasitic Bump", 0, 20, Visibles.True, Frozen.False, Align.Left, Merge.False, Formatter.String, 80);
                spdData.RPT_AddBasicColumn("Irregular Bump", 0, 21, Visibles.True, Frozen.False, Align.Left, Merge.False, Formatter.String, 80);
                spdData.RPT_AddBasicColumn("Miss Align", 0, 22, Visibles.True, Frozen.False, Align.Left, Merge.False, Formatter.String, 80);
                spdData.RPT_AddBasicColumn("(RDL/UBM) Discolor", 0, 23, Visibles.True, Frozen.False, Align.Left, Merge.False, Formatter.String, 80);
                spdData.RPT_AddBasicColumn("(RDL/UBM) Irregular", 0, 24, Visibles.True, Frozen.False, Align.Left, Merge.False, Formatter.String, 80);
                spdData.RPT_AddBasicColumn("(RDL/UBM) Short", 0, 25, Visibles.True, Frozen.False, Align.Left, Merge.False, Formatter.String, 80);
                spdData.RPT_AddBasicColumn("(RDL/UBM) Damage", 0, 26, Visibles.True, Frozen.False, Align.Left, Merge.False, Formatter.String, 80);
                spdData.RPT_AddBasicColumn("PSV Discolor", 0, 27, Visibles.True, Frozen.False, Align.Left, Merge.False, Formatter.String, 80);
                spdData.RPT_AddBasicColumn("PSV Damage", 0, 28, Visibles.True, Frozen.False, Align.Left, Merge.False, Formatter.String, 80);
                spdData.RPT_AddBasicColumn("PSV Wrinkle", 0, 29, Visibles.True, Frozen.False, Align.Left, Merge.False, Formatter.String, 80);
                spdData.RPT_AddBasicColumn("Flux Residue", 0, 30, Visibles.True, Frozen.False, Align.Left, Merge.False, Formatter.String, 80);
                spdData.RPT_AddBasicColumn("Customer Request", 0, 31, Visibles.True, Frozen.False, Align.Left, Merge.False, Formatter.String, 80);
                spdData.RPT_AddBasicColumn("Others", 0, 32, Visibles.True, Frozen.False, Align.Left, Merge.False, Formatter.String, 80);
            }
            else
            {
                spdData.RPT_AddBasicColumn("Customer", 0, 0, Visibles.False, Frozen.True, Align.Center, Merge.False, Formatter.String, 80);
                spdData.RPT_AddBasicColumn("Family", 0, 1, Visibles.False, Frozen.True, Align.Center, Merge.False, Formatter.String, 80);
                spdData.RPT_AddBasicColumn("LD Count", 0, 2, Visibles.True, Frozen.True, Align.Center, Merge.False, Formatter.String, 80);
                spdData.RPT_AddBasicColumn("PKG", 0, 3, Visibles.True, Frozen.True, Align.Center, Merge.False, Formatter.String, 80);
                spdData.RPT_AddBasicColumn("Type1", 0, 4, Visibles.False, Frozen.True, Align.Center, Merge.False, Formatter.String, 80);
                spdData.RPT_AddBasicColumn("Type2", 0, 5, Visibles.False, Frozen.True, Align.Center, Merge.False, Formatter.String, 80);
                spdData.RPT_AddBasicColumn("Density", 0, 6, Visibles.False, Frozen.True, Align.Center, Merge.False, Formatter.String, 80);
                spdData.RPT_AddBasicColumn("Generation", 0, 7, Visibles.False, Frozen.True, Align.Center, Merge.False, Formatter.String, 80);
                spdData.RPT_AddBasicColumn("Product", 0, 8, Visibles.True, Frozen.True, Align.Left, Merge.False, Formatter.String, 80);


                spdData.RPT_AddBasicColumn("LotID", 0, 9, Visibles.True, Frozen.True, Align.Left, Merge.False, Formatter.String, 80);
                spdData.RPT_AddBasicColumn("IN TIME", 0, 10, Visibles.True, Frozen.False, Align.Left, Merge.False, Formatter.String, 80);
                spdData.RPT_AddBasicColumn("SHIP TIME", 0, 11, Visibles.True, Frozen.False, Align.Left, Merge.False, Formatter.String, 80);
                spdData.RPT_AddBasicColumn("TAT", 0, 12, Visibles.True, Frozen.False, Align.Left, Merge.False, Formatter.String, 80);

                // 2010-11-05-��ο�
                if (cdvFactory.Text.Equals(GlobalVariable.gsAssyDefaultFactory))
                {
                    spdData.RPT_AddBasicColumn("I/L IN", 0, 13, Visibles.True, Frozen.False, Align.Left, Merge.False, Formatter.Number, 80);
                    spdData.RPT_AddBasicColumn("I/L OUT", 0, 14, Visibles.True, Frozen.False, Align.Left, Merge.False, Formatter.Number, 80);
                    spdData.RPT_AddBasicColumn("I/L YLD", 0, 15, Visibles.True, Frozen.False, Align.Left, Merge.False, Formatter.String, 80);
                    spdData.RPT_AddBasicColumn("B/E IN", 0, 16, Visibles.True, Frozen.False, Align.Left, Merge.False, Formatter.Number, 80);
                    spdData.RPT_AddBasicColumn("B/E OUT", 0, 17, Visibles.True, Frozen.False, Align.Left, Merge.False, Formatter.Number, 80);
                    spdData.RPT_AddBasicColumn("B/E YLD", 0, 18, Visibles.True, Frozen.False, Align.Left, Merge.False, Formatter.String, 80);
                }
                else
                {
                    spdData.RPT_AddBasicColumn("I/L IN", 0, 13, Visibles.False, Frozen.False, Align.Left, Merge.False, Formatter.Number, 80);
                    spdData.RPT_AddBasicColumn("I/L OUT", 0, 14, Visibles.False, Frozen.False, Align.Left, Merge.False, Formatter.Number, 80);
                    spdData.RPT_AddBasicColumn("I/L YLD", 0, 15, Visibles.False, Frozen.False, Align.Left, Merge.False, Formatter.String, 80);
                    spdData.RPT_AddBasicColumn("B/E IN", 0, 16, Visibles.False, Frozen.False, Align.Left, Merge.False, Formatter.Number, 80);
                    spdData.RPT_AddBasicColumn("B/E OUT", 0, 17, Visibles.False, Frozen.False, Align.Left, Merge.False, Formatter.Number, 80);
                    spdData.RPT_AddBasicColumn("B/E YLD", 0, 18, Visibles.False, Frozen.False, Align.Left, Merge.False, Formatter.String, 80);
                }

                // 2011-07-18-�����
                if (ckbIRInspection.Checked == true)
                {
                    spdData.RPT_AddBasicColumn("IN", 0, 19, Visibles.True, Frozen.False, Align.Left, Merge.False, Formatter.Number, 80);
                    spdData.RPT_AddBasicColumn("OUT", 0, 20, Visibles.True, Frozen.False, Align.Left, Merge.False, Formatter.Number, 80);
                    spdData.RPT_AddBasicColumn("YLD", 0, 21, Visibles.True, Frozen.False, Align.Left, Merge.False, Formatter.String, 80);
                    spdData.RPT_AddBasicColumn("BONUS", 0, 22, Visibles.True, Frozen.False, Align.Left, Merge.False, Formatter.Number, 80);
                    spdData.RPT_AddBasicColumn("LOSS", 0, 23, Visibles.True, Frozen.False, Align.Left, Merge.False, Formatter.Number, 80);
                    //spdData.RPT_AddBasicColumn("IR_INSPECTION", 0, 24, Visibles.True, Frozen.False, Align.Left, Merge.False, Formatter.Number, 100);
                }
                else
                {
                    spdData.RPT_AddBasicColumn("IN", 0, 19, Visibles.True, Frozen.False, Align.Left, Merge.False, Formatter.Number, 80);
                    spdData.RPT_AddBasicColumn("OUT", 0, 20, Visibles.True, Frozen.False, Align.Left, Merge.False, Formatter.Number, 80);
                    spdData.RPT_AddBasicColumn("YLD", 0, 21, Visibles.True, Frozen.False, Align.Left, Merge.False, Formatter.String, 80);
                    spdData.RPT_AddBasicColumn("BONUS", 0, 22, Visibles.True, Frozen.False, Align.Left, Merge.False, Formatter.Number, 80);
                    spdData.RPT_AddBasicColumn("LOSS", 0, 23, Visibles.True, Frozen.False, Align.Left, Merge.False, Formatter.Number, 80);
                }

                for (int i = 0; i < dtLoss.Rows.Count; i++)
                {
                    spdData.RPT_AddBasicColumn(dtLoss.Rows[i]["KEY_1"].ToString(), 0, spdData.ActiveSheet.Columns.Count, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 60);
                }

                spdData.RPT_ColumnConfigFromTable(btnSort); //Group�׸��� ������� �ݵ�� �������ٰ�.
            }            
        }

        #endregion

        #region 3.��ȸ

        /// <summary>
        /// 3.��ȸ
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnView_Click(object sender, EventArgs e)
        {
            if (!CheckField()) return; 
            
            DataTable dt = null;
            GetLossDT();
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

                if (cdvFactory.Text == "HMKB1")
                {
                    //spdData.DataSource = dt;
                    //1.Griid �հ� ǥ��
                    int[] rowType = spdData.RPT_DataBindingWithSubTotal(dt, 0, 3, 7, null, null, btnSort);

                    ////3. Total�κ� ������
                    spdData.RPT_FillDataSelectiveCells("Total", 0, 7, 0, 1, true, Align.Center, VerticalAlign.Center);

                    //GrandTotal �� SubTotal �κ� Yield �� ���ϱ�
                    spdData.RPT_SetAvgSubTotalAndGrandTotal(1, 10, 1, true);
                    spdData.RPT_SetAvgSubTotalAndGrandTotal(1, 11, 1, true);
                }
                else
                {
                    //�׷��� ���� 1������ SubTotal�� ����ϱ� ���ؼ� ù��° ���� ������
                    int sub = ((udcTableForm)(this.btnSort.BindingForm)).GetCheckedValue(2);

                    //spdData.DataSource = dt;


                    //1.Griid �հ� ǥ��
                    int[] rowType = spdData.RPT_DataBindingWithSubTotal(dt, 0, sub + 1, 11, null, null, btnSort);

                    ////2. Į�� ����(�ʿ��ϴٸ�..)
                    //spdData.Sheets[0].FrozenColumnCount = 9;

                    ////3. Total�κ� ������
                    spdData.RPT_FillDataSelectiveCells("Total", 0, 9, 0, 1, true, Align.Center, VerticalAlign.Center);

                    //4. Column Auto Fit
                    //spdData.RPT_AutoFit(true);

                    //5. ����Ÿ�� 0�� �κ��� ����(Add by John. 2009.1.13)
                    //spdData.RPT_RemoveZeroColumn(13, 1);'


                    //2009.12.04(������) GrandTotal �� SubTotal �κ� Yield �� ���ϱ�
                    SetGrandYield(1, 15);
                    SetGrandYield(1, 18);
                    SetGrandYield(1, 21);
                }

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

        /// <summary>
        /// Make Sql
        /// </summary>
        /// <returns></returns>
        private string MakeSqlString()
        {
            StringBuilder strSqlString = new StringBuilder();

            string QueryCond1 = null;
            string QueryCond2 = null;
            string sOper = null;

            udcTableForm tableForm = (udcTableForm)btnSort.BindingForm;

            QueryCond1 = tableForm.SelectedValueToQueryContainNull;
            QueryCond2 = tableForm.SelectedValue2ToQueryContainNull;

            GetLossDT(); // Loss Table ��������
                        
            // Decode �ݺ��� ����
            string strDecode = string.Empty;

            for (int i = 0; i < dtLoss.Rows.Count; i++)
            {
                strDecode += "     , SUM(DECODE(LOSS.LOSS_CODE,'" + dtLoss.Rows[i]["KEY_1"].ToString() + "',LOSS.LOSS_QTY,0))  LOSS" + i.ToString() + "\n";
            }

            // Factory �� ���� ���� �¾�
            if (cdvFactory.Text == GlobalVariable.gsAssyDefaultFactory)
                sOper = "AZ010";
            else if (cdvFactory.Text == GlobalVariable.gsTestDefaultFactory)
                sOper = "TZ010";
            else if (cdvFactory.Text == "HMKB1")
                sOper = "BZ010";
            else if (cdvFactory.Text == "HMKE1")
                sOper = "EZ010";
            else
                sOper = "";

            String strStartDate = udcDate.Start_Tran_Time;
            String strEndDate = udcDate.End_Tran_Time;
                        
            #region ASSY Factory ��ȸ ��
            // 2011-10-12-������ : ���� ������ �����Ͽ� HMKA1 �и��Ͽ� ���� ���� ��.
            if (cdvFactory.Text == GlobalVariable.gsAssyDefaultFactory)
            {
                // ����
                strSqlString.Append("WITH DT AS" + "\n");
                strSqlString.Append("(" + "\n");
                strSqlString.Append(" SELECT * " + "\n");
                //strSqlString.Append("   FROM RWIPLOTHIS " + "\n");
                strSqlString.Append("   FROM CWIPLOTEND " + "\n");
                strSqlString.Append("  WHERE 1=1 " + "\n");
                strSqlString.Append("    AND TRAN_TIME BETWEEN '" + strStartDate + "' AND '" + strEndDate + "' " + "\n");
                strSqlString.Append("    AND TRAN_CODE = 'END' " + "\n");
                strSqlString.Append("    AND LOT_TYPE = 'W' " + "\n");
                strSqlString.Append("    AND FACTORY = '" + cdvFactory.Text + "' " + "\n");                
                strSqlString.Append("    AND HIST_DEL_FLAG = ' ' " + "\n");
                strSqlString.Append("    AND TRAN_CMF_1 = 'CLOSE' " + "\n");    

                if (!txtSearchProduct.Text.Equals("%") && !String.IsNullOrEmpty(txtSearchProduct.Text))
                    strSqlString.Append("    AND MAT_ID LIKE '" + txtSearchProduct.Text + "'" + "\n");

                if (!String.IsNullOrEmpty(txtLotID.Text))
                    strSqlString.Append("    AND LOT_ID = '" + txtLotID.Text + "'" + "\n");
                
                //strSqlString.Append("    AND LOT_CMF_5 " + cdvLotType.SelectedValueToQueryString + "\n");
                if (cdvLotType.Text != "ALL")
                {
                    strSqlString.Append("    AND LOT_CMF_5 LIKE '" + cdvLotType.Text + "'" + "\n");
                }
                
                strSqlString.Append(")" + "\n");
                strSqlString.Append("SELECT " + QueryCond1 + "\n");
                strSqlString.Append("     , SUMMERY.LOT_ID,SUMMERY.IN_TIME,SUMMERY.SHIP_TIME " + "\n");
                strSqlString.Append("     , SUMMERY.TAT AS TAT " + "\n");
                strSqlString.Append("     , SUMMERY.BE_OUT + NVL(SUM(LOSS.BE_LOSS_QTY),0) + NVL(SUM(LOSS.IL_LOSS_QTY),0) AS IL_IN " + "\n");
                strSqlString.Append("     , SUMMERY.BE_OUT + NVL(SUM(LOSS.BE_LOSS_QTY),0) AS IL_OUT " + "\n");
                strSqlString.Append("     , ROUND(((SUMMERY.BE_OUT + NVL(SUM(LOSS.BE_LOSS_QTY),0))/(SUMMERY.BE_OUT + NVL(SUM(LOSS.BE_LOSS_QTY),0) + NVL(SUM(LOSS.IL_LOSS_QTY),0)))*100,3)||'%' AS IL_YIELD " + "\n");
                strSqlString.Append("     , SUMMERY.BE_OUT + NVL(SUM(LOSS.BE_LOSS_QTY),0) AS BE_IN " + "\n");
                strSqlString.Append("     , SUMMERY.BE_OUT " + "\n");
                strSqlString.Append("     , ROUND((SUMMERY.BE_OUT/(SUMMERY.BE_OUT + NVL(SUM(LOSS.BE_LOSS_QTY),0)))*100,3)||'%' AS BE_YIELD " + "\n");
                strSqlString.Append("     , SUMMERY.OUT_QTY + NVL(SUM(LOSS.LOSS_QTY),0) AS IN_QTY  " + "\n");
                strSqlString.Append("     , SUMMERY.OUT_QTY " + "\n");

                // 2010-04-16-��ο� : �����κ� ���� (������ ��û)
                //strSqlString.Append("     , ROUND(SUMMERY.BE_OUT/(SUMMERY.IL_OUT + SUMMERY.IL_LOSS_QTY)*100,3)||'%' AS YIELD " + "\n");
                strSqlString.Append("     , ROUND(SUMMERY.OUT_QTY/(SUMMERY.OUT_QTY + NVL(SUM(LOSS.LOSS_QTY),0))*100,3)||'%' AS YIELD " + "\n");

                strSqlString.Append("     , '' AS BONUS " + "\n");
                strSqlString.Append("     , NVL(SUM(LOSS.LOSS_QTY),0) AS LOSS_QTY " + "\n");
                strSqlString.Append(strDecode + "\n");

                if (ckbIRInspection.Checked == true)
                {
                    strSqlString.Append("     , SUMMERY.LAST_COMMENT AS �۾��� " + "\n");
                }

                strSqlString.Append("  FROM ( " + "\n");
                strSqlString.Append("        SELECT " + QueryCond2 + "\n");
                strSqlString.Append("             , A.LOT_ID AS LOT_ID " + "\n");

                if (ckbIRInspection.Checked == true)
                {
                    strSqlString.Append("             , B.LAST_COMMENT " + "\n");
                }

                strSqlString.Append("             , TO_CHAR(TO_DATE(C.CREATE_TIME,'yyyymmddhh24miss')) AS IN_TIME " + "\n");
                strSqlString.Append("             , TO_CHAR(TO_DATE(A.TRAN_TIME,'yyyymmddhh24miss')) AS SHIP_TIME " + "\n");
                strSqlString.Append("             , ROUND((TO_DATE(A.TRAN_TIME,'yyyymmddhh24miss') - TO_DATE(C.CREATE_TIME,'yyyymmddhh24miss')),2) AS TAT " + "\n");
                strSqlString.Append("             , (  " + "\n");
                strSqlString.Append("                SELECT MIN(QTY_1)   " + "\n");
                strSqlString.Append("                  FROM RWIPLOTHIS  " + "\n");
                strSqlString.Append("                 WHERE FACTORY = '" + cdvFactory.Text + "'  " + "\n");
                strSqlString.Append("                   AND OLD_OPER >= 'A1000'  " + "\n");
                strSqlString.Append("                   AND LOT_ID=A.LOT_ID  " + "\n");
                strSqlString.Append("                   AND TRAN_CODE IN ('END','SHIP')    " + "\n");
                strSqlString.Append("                   AND HIST_DEL_FLAG = ' '               " + "\n");
                strSqlString.Append("               ) AS BE_OUT " + "\n");
                strSqlString.Append("             , A.QTY_1 AS OUT_QTY " + "\n");                
                strSqlString.Append("          FROM DT A" + "\n");

                // 2011-07-18-����� : A2200����(I.R Inspection)������ Comment ������.
                // 2011-09-14-����� : A2050�������� ���� 
                if (ckbIRInspection.Checked == true)
                {                    
                    strSqlString.Append("             , ( " + "\n");
                    strSqlString.Append("                SELECT LOT_ID, LAST_COMMENT " + "\n");
                    strSqlString.Append("                  FROM RWIPLOTHIS " + "\n");
                    strSqlString.Append("                 WHERE 1=1 " + "\n");
                    strSqlString.Append("                   AND FACTORY = '" + cdvFactory.Text + "' " + "\n");
                    strSqlString.Append("                   AND OLD_OPER = 'A2050' " + "\n");
                    strSqlString.Append("                   AND TRAN_CODE = 'LOSS' " + "\n");
                    strSqlString.Append("                   AND HIST_DEL_FLAG = ' ' " + "\n");
                    strSqlString.Append("               ) B " + "\n");
                }

                strSqlString.Append("             , MWIPMATDEF MAT" + "\n");
                strSqlString.Append("             , RWIPLOTSTS C " + "\n");
                strSqlString.Append("         WHERE 1=1" + "\n");
                strSqlString.Append("           AND A.MAT_ID=MAT.MAT_ID " + "\n");
                strSqlString.Append("           AND A.LOT_ID=C.LOT_ID " + "\n");

                if (ckbIRInspection.Checked == true)
                {
                    strSqlString.Append("           AND A.LOT_ID=B.LOT_ID(+) " + "\n");
                }

                strSqlString.Append("           AND MAT.FACTORY = '" + cdvFactory.Text + "' " + "\n");

                #region �� ��ȸ�� ���� SQL�� ����
                if (udcWIPCondition1.Text != "ALL" && udcWIPCondition1.Text != "")
                    strSqlString.AppendFormat("           AND MAT.MAT_GRP_1 {0} " + "\n", udcWIPCondition1.SelectedValueToQueryString);

                if (udcWIPCondition2.Text != "ALL" && udcWIPCondition2.Text != "")
                    strSqlString.AppendFormat("           AND MAT.MAT_GRP_2 {0} " + "\n", udcWIPCondition2.SelectedValueToQueryString);

                if (udcWIPCondition3.Text != "ALL" && udcWIPCondition3.Text != "")
                    strSqlString.AppendFormat("           AND MAT.MAT_GRP_3 {0} " + "\n", udcWIPCondition3.SelectedValueToQueryString);

                if (udcWIPCondition4.Text != "ALL" && udcWIPCondition4.Text != "")
                    strSqlString.AppendFormat("           AND MAT.MAT_GRP_4 {0} " + "\n", udcWIPCondition4.SelectedValueToQueryString);

                if (udcWIPCondition5.Text != "ALL" && udcWIPCondition5.Text != "")
                    strSqlString.AppendFormat("           AND MAT.MAT_GRP_5 {0} " + "\n", udcWIPCondition5.SelectedValueToQueryString);

                if (udcWIPCondition6.Text != "ALL" && udcWIPCondition6.Text != "")
                    strSqlString.AppendFormat("           AND MAT.MAT_GRP_6 {0} " + "\n", udcWIPCondition6.SelectedValueToQueryString);

                if (udcWIPCondition7.Text != "ALL" && udcWIPCondition7.Text != "")
                    strSqlString.AppendFormat("           AND MAT.MAT_GRP_7 {0} " + "\n", udcWIPCondition7.SelectedValueToQueryString);

                if (udcWIPCondition8.Text != "ALL" && udcWIPCondition8.Text != "")
                    strSqlString.AppendFormat("           AND MAT.MAT_GRP_8 {0} " + "\n", udcWIPCondition8.SelectedValueToQueryString);
                #endregion
                
                strSqlString.Append("       ) SUMMERY" + "\n");
                strSqlString.Append("     , (" + "\n");
                strSqlString.Append("        SELECT LOT_ID" + "\n");
                strSqlString.Append("             , LOSS_CODE" + "\n");
                strSqlString.Append("             , SUM(LOSS_QTY) AS LOSS_QTY " + "\n");
                strSqlString.Append("             , SUM(CASE WHEN OPER < 'A1000' THEN LOSS_QTY " + "\n");
                strSqlString.Append("                        ELSE 0 " + "\n");
                strSqlString.Append("                   END) IL_LOSS_QTY " + "\n");
                strSqlString.Append("             , SUM(CASE WHEN OPER >= 'A1000' THEN LOSS_QTY " + "\n");
                strSqlString.Append("                        ELSE 0 " + "\n");
                strSqlString.Append("                   END) BE_LOSS_QTY " + "\n");
                strSqlString.Append("          FROM (" + "\n");
                strSqlString.Append("                SELECT LOT_ID, OPER, LOSS_CODE, LOSS_QTY" + "\n");
                strSqlString.Append("                  FROM RWIPLOTLSM" + "\n");
                strSqlString.Append("                 WHERE 1=1" + "\n");
                strSqlString.Append("                   AND FACTORY = '" + cdvFactory.Text + "'  " + "\n");
                strSqlString.Append("                   AND HIST_DEL_FLAG = ' '" + "\n");
                strSqlString.Append("                   AND LOT_ID IN ( " + "\n");                
                strSqlString.Append("                                  SELECT LOT_ID " + "\n");
                strSqlString.Append("                                    FROM DT " + "\n");
                strSqlString.Append("                                 ) " + "\n");
                strSqlString.Append("                UNION ALL " + "\n");
                strSqlString.Append("                SELECT /*+ INDEX (LSM, RWIPLOTLSM_IDX02) */SPL.FROM_TO_LOT_ID AS LOT_ID, OPER, LOSS_CODE, LOSS_QTY" + "\n");
                strSqlString.Append("                  FROM RWIPLOTLSM LSM" + "\n");
                strSqlString.Append("                     , (" + "\n");
                strSqlString.Append("                        SELECT LOT_ID, FROM_TO_LOT_ID " + "\n");
                strSqlString.Append("                          FROM RWIPLOTSTS " + "\n");
                strSqlString.Append("                         WHERE 1=1" + "\n");
                strSqlString.Append("                           AND FACTORY = '" + cdvFactory.Text + "'  " + "\n");
                strSqlString.Append("                           AND LOT_TYPE = 'W' " + "\n");
                strSqlString.Append("                           AND ADD_ORDER_ID_2 = 'MULTIEQP' " + "\n");
                strSqlString.Append("                           AND FROM_TO_LOT_ID IN ( " + "\n");
                strSqlString.Append("                                                  SELECT LOT_ID " + "\n");
                strSqlString.Append("                                                    FROM DT " + "\n");
               strSqlString.Append("                                                 ) " + "\n");
                strSqlString.Append("                       ) SPL" + "\n");
                strSqlString.Append("                 WHERE 1=1" + "\n");
                strSqlString.Append("                   AND LSM.LOT_ID = SPL.LOT_ID" + "\n");
                strSqlString.Append("                   AND LSM.FACTORY = '" + cdvFactory.Text + "'  " + "\n");
                strSqlString.Append("                   AND HIST_DEL_FLAG = ' '" + "\n");                
                strSqlString.Append("               )" + "\n");
                strSqlString.Append("         GROUP BY LOT_ID,LOSS_CODE,LOSS_QTY" + "\n");
                strSqlString.Append("       ) LOSS" + "\n");
                strSqlString.Append(" WHERE SUMMERY.LOT_ID=LOSS.LOT_ID(+)" + "\n");

                /// 2010-11-01-��ο� : �˻� ���ǿ� ���� �߰� - �˻� �� ������ ���� �̸��� ��ȸ (������ ��û)
                if (txtYield.Text != "100" && txtYield.Text != "")
                {
                    strSqlString.Append("   AND ROUND(SUMMERY.OUT_QTY/(SUMMERY.OUT_QTY + NVL(SUM(LOSS.LOSS_QTY),0))*100,3) <= " + txtYield.Text + "\n");
                }

                strSqlString.Append(" GROUP BY " + QueryCond1 + "\n");
                strSqlString.Append("        , SUMMERY.LOT_ID,SUMMERY.IN_TIME,SUMMERY.SHIP_TIME, SUMMERY.TAT " + "\n");

                if (ckbIRInspection.Checked == true)
                {
                    strSqlString.Append("        , SUMMERY.LAST_COMMENT " + "\n");
                }

                strSqlString.Append("        , SUMMERY.BE_OUT, SUMMERY.OUT_QTY " + "\n");           
                strSqlString.Append(" ORDER BY  " + QueryCond1 + ",SUMMERY.LOT_ID" + "\n");
            }
            #endregion

            #region Bump Factory ��ȸ ��
            else if (cdvFactory.Text == "HMKB1")
            {
                strSqlString.Append("WITH DT AS" + "\n");
                strSqlString.Append("(" + "\n");
                strSqlString.Append(" SELECT *" + "\n");
                strSqlString.Append("   FROM VWIPSHPLOT" + "\n");
                strSqlString.Append("  WHERE 1=1" + "\n");
                strSqlString.Append("    AND FROM_FACTORY = 'HMKB1' " + "\n");
                strSqlString.Append("    AND FROM_OPER = 'BZ010'" + "\n");
                strSqlString.Append("    AND TRAN_TIME BETWEEN '" + strStartDate + "' AND '" + strEndDate + "' " + "\n");

                if (!txtSearchProduct.Text.Equals("%") && !String.IsNullOrEmpty(txtSearchProduct.Text))
                    strSqlString.Append("    AND MAT_ID LIKE '" + txtSearchProduct.Text + "'" + "\n");

                if (!String.IsNullOrEmpty(txtLotID.Text))
                    strSqlString.Append("    AND LOT_ID = '" + txtLotID.Text + "'" + "\n");

                if (cdvLotType.Text != "ALL")
                {
                    strSqlString.Append("    AND LOT_CMF_5 LIKE '" + cdvLotType.Text + "'" + "\n");
                }

                strSqlString.Append(")" + "\n");
                strSqlString.Append("SELECT A.CUSTOMER" + "\n");
                strSqlString.Append("     , B.MAT_CMF_7 AS DEVICE" + "\n");
                strSqlString.Append("     , A.MOTHER_LOT_ID AS RUN_ID" + "\n");
                strSqlString.Append("     , A.LOT_ID" + "\n");
                //strSqlString.Append("     , (SELECT DECODE(RESV_FIELD_1, ' ', ' ', TO_CHAR(TO_DATE(RESV_FIELD_1,'YYYYMMDDHH24MISS'))) FROM DT WHERE LOT_ID = A.LOT_ID) AS IN_DATE" + "\n");
                strSqlString.Append("     , NVL((" + "\n");
                strSqlString.Append("            SELECT TO_CHAR(TO_DATE(TRAN_TIME,'YYYYMMDDHH24MISS'))" + "\n");
                strSqlString.Append("              FROM CWIPLOTEND" + "\n");
                strSqlString.Append("             WHERE FACTORY = 'HMKB1' " + "\n");
                strSqlString.Append("               AND OLD_OPER = 'B0000'" + "\n");
                strSqlString.Append("               AND HIST_DEL_FLAG = ' ' " + "\n");
                strSqlString.Append("               AND LOT_ID = A.LOT_ID" + "\n");
                strSqlString.Append("       ), ' ') AS IN_DATE" + "\n");
                strSqlString.Append("     , (SELECT TO_CHAR(TO_DATE(TRAN_TIME,'YYYYMMDDHH24MISS')) FROM DT WHERE LOT_ID = A.LOT_ID) AS OUT_DATE" + "\n");
                // 2020-06-24-��̰� : WAFER SLOT_NO ���� ���� ������ ������ WAFER_ID �ڿ��� 2�ڸ� �������� ������ �ӽ� ��ġ
                //strSqlString.Append("     , A.SLOT_ID AS WAFER_NO" + "\n");
                strSqlString.Append("     , SUBSTR(A.WAFER_ID, -2) AS WAFER_NO" + "\n");
                strSqlString.Append("     , A.NETDIE" + "\n");
                strSqlString.Append("     , A.BIN0 AS GOOD_DIE" + "\n");
                strSqlString.Append("     , A.LOSS_DIE AS FAIL_DIE " + "\n");
                strSqlString.Append("     , A.YIELD" + "\n");
                strSqlString.Append("     , ROUND((A.BIN0 + A.BIN2 + A.BIN21) / A.NETDIE * 100, 2) AS BUMP_YIELD" + "\n");
                strSqlString.Append("     , A.BIN2, A.BIN3, A.BIN4, A.BIN5, A.BIN6, A.BIN7, A.BIN8, A.BIN9, A.BIN10" + "\n");
                strSqlString.Append("     , A.BIN11, A.BIN12, A.BIN13, A.BIN14, A.BIN15, A.BIN16, A.BIN17, A.BIN18, A.BIN19, A.BIN20" + "\n");
                strSqlString.Append("     , A.BIN21, A.BIN22" + "\n");
                strSqlString.Append("  FROM VYMSWFRYLD@RPTTOMES A" + "\n");
                strSqlString.Append("     , MWIPMATDEF B " + "\n");
                strSqlString.Append(" WHERE 1=1" + "\n");
                strSqlString.Append("   AND A.FACILITY = B.FACTORY" + "\n");
                strSqlString.Append("   AND A.PRODUCT = B.MAT_ID" + "\n");
                //strSqlString.Append("   AND A.LOT_ID IN (SELECT LOT_ID FROM DT)" + "\n"); 
                strSqlString.Append("   AND A.MOTHER_LOT_ID IN (SELECT LOT_CMF_4 FROM DT)" + "\n"); // 2019-06-03-������ : Merge Lot �̷��� ǥ�� �ϱ� ���� ����.
                strSqlString.Append("   AND A.FACILITY = 'HMKB1'" + "\n");                
                strSqlString.Append("   AND A.TESTAREA = 'B7950'" + "\n"); // ���� ������
                strSqlString.Append("   AND A.PROBE_CNT = 0" + "\n");  // ������ �̷��� 0

                #region �� ��ȸ�� ���� SQL�� ����
                if (udcWIPCondition1.Text != "ALL" && udcWIPCondition1.Text != "")
                    strSqlString.AppendFormat("   AND B.MAT_GRP_1 {0} " + "\n", udcWIPCondition1.SelectedValueToQueryString);

                if (udcWIPCondition2.Text != "ALL" && udcWIPCondition2.Text != "")
                    strSqlString.AppendFormat("   AND B.MAT_GRP_2 {0} " + "\n", udcWIPCondition2.SelectedValueToQueryString);

                if (udcWIPCondition3.Text != "ALL" && udcWIPCondition3.Text != "")
                    strSqlString.AppendFormat("   AND B.MAT_GRP_3 {0} " + "\n", udcWIPCondition3.SelectedValueToQueryString);

                if (udcWIPCondition4.Text != "ALL" && udcWIPCondition4.Text != "")
                    strSqlString.AppendFormat("   AND B.MAT_GRP_4 {0} " + "\n", udcWIPCondition4.SelectedValueToQueryString);

                if (udcWIPCondition5.Text != "ALL" && udcWIPCondition5.Text != "")
                    strSqlString.AppendFormat("   AND B.MAT_GRP_5 {0} " + "\n", udcWIPCondition5.SelectedValueToQueryString);

                if (udcWIPCondition6.Text != "ALL" && udcWIPCondition6.Text != "")
                    strSqlString.AppendFormat("   AND B.MAT_GRP_6 {0} " + "\n", udcWIPCondition6.SelectedValueToQueryString);

                if (udcWIPCondition7.Text != "ALL" && udcWIPCondition7.Text != "")
                    strSqlString.AppendFormat("   AND B.MAT_GRP_7 {0} " + "\n", udcWIPCondition7.SelectedValueToQueryString);

                if (udcWIPCondition8.Text != "ALL" && udcWIPCondition8.Text != "")
                    strSqlString.AppendFormat("   AND B.MAT_GRP_8 {0} " + "\n", udcWIPCondition8.SelectedValueToQueryString);
                #endregion

                if (txtYield.Text != "100" && txtYield.Text != "")
                {
                    strSqlString.Append("   AND A.YIELD <= " + txtYield.Text + "\n");
                }

                strSqlString.Append(" ORDER BY CUSTOMER, DEVICE, RUN_ID, LOT_ID, WAFER_NO, IN_DATE, OUT_DATE" + "\n");
            }
            #endregion

            #region �׿� Factory ��ȸ ��
            else
            {
                // ����
                strSqlString.Append("SELECT " + QueryCond1 + "\n");
                strSqlString.Append("     , SUMMERY.LOT_ID,SUMMERY.IN_TIME,SUMMERY.SHIP_TIME " + "\n");
                strSqlString.Append("     , TO_CHAR(SUMMERY.TAT) AS TAT " + "\n");
                strSqlString.Append("     , 0 AS IL_IN  " + "\n");
                strSqlString.Append("     , 0 AS IL_OUT " + "\n");
                strSqlString.Append("     , '' AS IL_YIELD " + "\n");
                strSqlString.Append("     , 0 AS BE_IN " + "\n");
                strSqlString.Append("     , 0 AS BE_OUT " + "\n");
                strSqlString.Append("     , '' AS BE_YIELD " + "\n");
                strSqlString.Append("     , SUMMERY.OUT_QTY + SUMMERY.LOSS_QTY AS IN_QTY " + "\n");
                strSqlString.Append("     , SUMMERY.OUT_QTY " + "\n");
                strSqlString.Append("     , ROUND(SUMMERY.OUT_QTY/(SUMMERY.OUT_QTY + SUMMERY.LOSS_QTY)*100,3)||'%' AS YIELD " + "\n");
                strSqlString.Append("     , '' AS BONUS " + "\n");
                strSqlString.Append("     , SUMMERY.LOSS_QTY " + "\n");
                strSqlString.Append("             " + strDecode + "\n");
                strSqlString.Append("  FROM ( " + "\n");
                strSqlString.Append("        SELECT " + QueryCond2 + "\n");
                strSqlString.Append("             , A.LOT_ID AS LOT_ID " + "\n");
                strSqlString.Append("             , TO_CHAR(TO_DATE(C.CREATE_TIME,'yyyymmddhh24miss')) AS IN_TIME " + "\n");
                strSqlString.Append("             , TO_CHAR(TO_DATE(A.TRAN_TIME,'yyyymmddhh24miss')) AS SHIP_TIME " + "\n");
                strSqlString.Append("             , ROUND((TO_DATE(A.TRAN_TIME,'yyyymmddhh24miss') - TO_DATE(C.CREATE_TIME,'yyyymmddhh24miss')),2) AS TAT " + "\n");
                strSqlString.Append("             , A.QTY_1 AS OUT_QTY " + "\n");
                strSqlString.Append("             , ( " + "\n");
                strSqlString.Append("                SELECT NVL(SUM(TOTAL_LOSS_QTY),0) " + "\n");
                strSqlString.Append("                  FROM RWIPLOTLOS  " + "\n");
                strSqlString.Append("                 WHERE LOT_ID=A.LOT_ID  " + "\n");
                strSqlString.Append("                   AND FACTORY = '" + cdvFactory.Text + "'  " + "\n");
                strSqlString.Append("                   AND HIST_DEL_FLAG = ' '  " + "\n");
                strSqlString.Append("               ) AS LOSS_QTY  " + "\n");
                strSqlString.Append("          FROM (" + "\n");
                strSqlString.Append("                SELECT MAT_ID " + "\n");
                strSqlString.Append("                     , LOT_ID " + "\n");
                strSqlString.Append("                     , SHIP_QTY_1 AS QTY_1  " + "\n");
                strSqlString.Append("                     , TRAN_TIME " + "\n");
                strSqlString.Append("                  FROM VWIPSHPLOT    " + "\n");
                strSqlString.Append("                 WHERE 1=1    " + "\n");
                strSqlString.Append("                   AND FROM_FACTORY = '" + cdvFactory.Text + "'  " + "\n");
                strSqlString.Append("                   AND OWNER_CODE = 'PROD'  " + "\n");
                strSqlString.Append("                   AND LOT_TYPE = 'W' " + "\n");
                strSqlString.Append("                   AND FROM_OPER = '" + sOper + "'" + "\n");
                strSqlString.Append("                   AND TRAN_TIME BETWEEN '" + strStartDate + "' AND '" + strEndDate + "' " + "\n");

                if (!txtSearchProduct.Text.Equals("%") && !String.IsNullOrEmpty(txtSearchProduct.Text))
                    strSqlString.Append("                   AND MAT_ID LIKE '" + txtSearchProduct.Text + "'" + "\n");

                if (!String.IsNullOrEmpty(txtLotID.Text))
                    strSqlString.Append("                   AND LOT_ID = '" + txtLotID.Text + "'" + "\n");

                if (ckbCustomerShip.Checked == true)
                    strSqlString.Append("                   AND TO_FACTORY = 'CUSTOMER'" + "\n");

                if (cdvLotType.Text != "ALL")
                {
                    strSqlString.Append("                   AND LOT_CMF_5 LIKE '" + cdvLotType.Text + "'" + "\n");
                }

                strSqlString.Append("               ) A" + "\n");
                strSqlString.Append("             , MWIPMATDEF MAT" + "\n");
                strSqlString.Append("             , RWIPLOTSTS C " + "\n");
                strSqlString.Append("         WHERE 1=1" + "\n");
                strSqlString.Append("           AND A.MAT_ID=MAT.MAT_ID " + "\n");
                strSqlString.Append("           AND A.LOT_ID=C.LOT_ID " + "\n");
                strSqlString.Append("           AND MAT.FACTORY = '" + cdvFactory.Text + "' " + "\n");

                /* RWIPLOTSTS Table �� Create Code �� Return �� ���� �ϳ� 2009.03/26 ������ �����ʹ� ���� PROD ��
                 * �̷��� ������ �Ʒ��� �ڵ带 �ּ� ó�� �ϰ� �̷��� �˻��Ͽ� ��ǰ�� Lot �� ����
                 */
                strSqlString.Append("           AND C.CREATE_CODE != 'RETN' " + "\n");
                strSqlString.Append("           AND A.LOT_ID NOT IN " + "\n");
                strSqlString.Append("           ( " + "\n");
                strSqlString.Append("           SELECT  LOT_ID " + "\n");
                strSqlString.Append("           FROM    RWIPLOTHIS " + "\n");
                strSqlString.Append("           WHERE   1=1 " + "\n");
                strSqlString.Append("                   AND FACTORY = '" + cdvFactory.txtValue + "' " + "\n");
                strSqlString.Append("                   AND TRAN_CODE = 'CREATE' " + "\n");
                strSqlString.Append("                   AND OPER = '" + sOper + "'" + "\n");
                strSqlString.Append("                   AND HIST_DEL_FLAG = ' '" + "\n");
                strSqlString.Append("           ) " + "\n");

                #region �� ��ȸ�� ���� SQL�� ����
                if (udcWIPCondition1.Text != "ALL" && udcWIPCondition1.Text != "")
                    strSqlString.AppendFormat("           AND MAT.MAT_GRP_1 {0} " + "\n", udcWIPCondition1.SelectedValueToQueryString);

                if (udcWIPCondition2.Text != "ALL" && udcWIPCondition2.Text != "")
                    strSqlString.AppendFormat("           AND MAT.MAT_GRP_2 {0} " + "\n", udcWIPCondition2.SelectedValueToQueryString);

                if (udcWIPCondition3.Text != "ALL" && udcWIPCondition3.Text != "")
                    strSqlString.AppendFormat("           AND MAT.MAT_GRP_3 {0} " + "\n", udcWIPCondition3.SelectedValueToQueryString);

                if (udcWIPCondition4.Text != "ALL" && udcWIPCondition4.Text != "")
                    strSqlString.AppendFormat("           AND MAT.MAT_GRP_4 {0} " + "\n", udcWIPCondition4.SelectedValueToQueryString);

                if (udcWIPCondition5.Text != "ALL" && udcWIPCondition5.Text != "")
                    strSqlString.AppendFormat("           AND MAT.MAT_GRP_5 {0} " + "\n", udcWIPCondition5.SelectedValueToQueryString);

                if (udcWIPCondition6.Text != "ALL" && udcWIPCondition6.Text != "")
                    strSqlString.AppendFormat("           AND MAT.MAT_GRP_6 {0} " + "\n", udcWIPCondition6.SelectedValueToQueryString);

                if (udcWIPCondition7.Text != "ALL" && udcWIPCondition7.Text != "")
                    strSqlString.AppendFormat("           AND MAT.MAT_GRP_7 {0} " + "\n", udcWIPCondition7.SelectedValueToQueryString);

                if (udcWIPCondition8.Text != "ALL" && udcWIPCondition8.Text != "")
                    strSqlString.AppendFormat("           AND MAT.MAT_GRP_8 {0} " + "\n", udcWIPCondition8.SelectedValueToQueryString);
                #endregion

                strSqlString.Append("     ) SUMMERY" + "\n");
                strSqlString.Append("   , (" + "\n");
                strSqlString.Append("     SELECT LOT_ID" + "\n");
                strSqlString.Append("          , LOSS_CODE" + "\n");
                strSqlString.Append("          , SUM(LOSS_QTY) AS LOSS_QTY " + "\n");
                strSqlString.Append("       FROM RWIPLOTLSM" + "\n");
                strSqlString.Append("      WHERE 1=1" + "\n");
                strSqlString.Append("        AND FACTORY = '" + cdvFactory.Text + "'  " + "\n");
                strSqlString.Append("        AND HIST_DEL_FLAG = ' '" + "\n");
                strSqlString.Append("   GROUP BY LOT_ID,LOSS_CODE,LOSS_QTY" + "\n");
                strSqlString.Append("      ) LOSS" + "\n");
                strSqlString.Append(" WHERE SUMMERY.LOT_ID=LOSS.LOT_ID(+)" + "\n");

                /// 2010-11-01-��ο� : �˻� ���ǿ� ���� �߰� - �˻� �� ������ ���� �̸��� ��ȸ (������ ��û)
                if (txtYield.Text != "100" && txtYield.Text != "")
                {
                    strSqlString.Append("   AND ROUND(SUMMERY.OUT_QTY/(SUMMERY.OUT_QTY + SUMMERY.LOSS_QTY)*100,3) <= " + txtYield.Text + "\n");
                }
                strSqlString.Append(" GROUP BY " + QueryCond1 + ",SUMMERY.LOT_ID,SUMMERY.IN_TIME,SUMMERY.SHIP_TIME" + "\n");
                strSqlString.Append("            , SUMMERY.TAT " + "\n");
                strSqlString.Append("            , SUMMERY.OUT_QTY " + "\n");
                strSqlString.Append("            , SUMMERY.LOSS_QTY " + "\n");
                strSqlString.Append("            , SUMMERY.LOSS_QTY " + "\n");
                strSqlString.Append(" ORDER BY  " + QueryCond1 + ",SUMMERY.LOT_ID" + "\n");
            }
            #endregion

            if (GlobalVariable.gsUserGroup == "ADMIN_GROUP" || GlobalVariable.gsUserGroup == "HANA_ADMIN_GROUP")
            {
                System.Windows.Forms.Clipboard.SetText(strSqlString.ToString());
            }

            return strSqlString.ToString();
        }
        #endregion

        #region " User Define Function "
        /// <summary>
        /// Loss Code ��������
        /// </summary>
        private void GetLossDT()
        {
            StringBuilder strSqlString = new StringBuilder();

            strSqlString.Append("SELECT  KEY_1,DATA_3,DATA_4  " + "\n");
            strSqlString.Append("FROM    MGCMTBLDAT  " + "\n");
            strSqlString.Append("WHERE   1=1 " + "\n");
            strSqlString.Append("        AND FACTORY = '" + cdvFactory.txtValue + "' " + "\n");
            strSqlString.Append("        AND TABLE_NAME='LOSS_CODE' " + "\n");
            if (cdvFactory.Text.Equals(GlobalVariable.gsAssyDefaultFactory))
            {
                strSqlString.Append("        AND DATA_3='Y'  " + "\n");
                strSqlString.Append("ORDER BY TO_NUMBER(DATA_4) " + "\n");
            }

            dtLoss = CmnFunction.oComm.GetFuncDataTable("DYNAMIC", strSqlString.ToString());
        }
        
        /// <summary>
        /// 2009.12.04(������) GrandTotal �� SubTotal �κ� Yield �� ���ϱ�
        /// </summary>
        /// <param name="nSampleNormalRowPos"></param>
        /// <param name="nColPos"></param>
        /// <param name="bWithNull"></param>
        public void SetGrandYield(int nSampleNormalRowPos, int nColPos)
        {
            double din = 0;
            double dout = 0;
            double dyield = 0;

            Color color = spdData.ActiveSheet.Cells[nSampleNormalRowPos, nColPos].BackColor;

            for (int i = 0; i < spdData.ActiveSheet.Rows.Count; i++)
            {
                din = Convert.ToDouble(spdData.ActiveSheet.Cells[i, nColPos - 2].Value);
                dout = Convert.ToDouble(spdData.ActiveSheet.Cells[i, nColPos - 1].Value);
                dyield = (dout / din) * 100;

                // GrandTotal Yield �� ���ϱ�
                if (i == 0)
                {
                    // 5�ڸ� �����̸� (99.99) �״�� ǥ�� -> 99.99 + %
                    if (dyield.ToString().Length < 7)
                    {
                        spdData.ActiveSheet.Cells[0, nColPos].Value = dyield.ToString() + "%";
                    }
                    else // 5�ڸ� �ʰ��ϸ� (99.999) 5�ڸ������� ǥ�� -> 99.99 + %
                    {
                        spdData.ActiveSheet.Cells[0, nColPos].Value = dyield.ToString().Substring(0, 6) + "%";
                    }
                }
                // subTotal Yield �� ���ϱ�                
                else if (spdData.ActiveSheet.Cells[i, nColPos].BackColor != color)
                {
                    if (dyield.ToString().Length < 7)
                    {
                        spdData.ActiveSheet.Cells[i, nColPos].Value = dyield.ToString() + "%";
                    }
                    else
                    {
                        spdData.ActiveSheet.Cells[i, nColPos].Value = dyield.ToString().Substring(0, 6) + "%";
                    }
                }
            }

            return;
        }

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

            //if (rbtDetail.Checked)
            //{
            //    if (cdvStep.Text.Trim().Length == 0)
            //    {
            //        CmnFunction.ShowMsgBox(RptMessages.GetMessage("STD005", GlobalVariable.gcLanguage));
            //        return false;
            //    }
            //}

            //if (rbtBrief.Checked)
            //{
            //    if (cdvStepGrp.Text.Trim().Length == 0)
            //    {
            //        CmnFunction.ShowMsgBox(RptMessages.GetMessage("STD005", GlobalVariable.gcLanguage));
            //        return false;
            //    }
            //}

            return true;
        }

        
        #endregion

        #region " Event Handler "

        /// <summary>
        /// ToExcel
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnExcelExport_Click(object sender, EventArgs e)
        {
            // Excel �ٷ� ���̱�
            //ExcelHelper.Instance.subMakeExcel(spdData, this.lblTitle.Text, " ^ ", " ^ ", true);
            spdData.ExportExcel();            
        }

        private void rbtBrief_CheckedChanged(object sender, EventArgs e)
        {
            //cdvStep.Visible = !(rbtBrief.Checked);
        }

        private void cdvFactory_ValueSelectedItemChanged(object sender, MCCodeViewSelChanged_EventArgs e)
        {
            this.SetFactory(cdvFactory.txtValue);
            //cdvLotType.sFactory = cdvFactory.txtValue;

            // 2011-07-18-����� : HMKA1�� A2200 ����(I.R Inspection)���� Comment ��ȸ �� �� �ְ�
            if (cdvFactory.Text == GlobalVariable.gsAssyDefaultFactory)
            {
                ckbIRInspection.Enabled = true;
            }
            else
            {
                ckbIRInspection.Enabled = false;
            }
        }

        #endregion

        private void txtSearchProduct_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                btnView_Click(sender, e);
            }
        }
    }
}

