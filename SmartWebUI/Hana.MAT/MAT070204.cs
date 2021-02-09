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

namespace Hana.MAT
{
    public partial class MAT070204 : Miracom.SmartWeb.UI.Controls.udcCUSReport002
    {
        /// <summary>
        /// Ŭ  ��  ��: MAT070204<br/>
        /// Ŭ�������: ������ ���纰 ���� ��ȸ (WMS)<br/>
        /// ��  ��  ��: �ϳ�����ũ�� �����<br/>
        /// �����ۼ���: 2012-03-09<br/>
        /// ��  ����: WMS - ������ ���� ���� ��ȸ<br/>
        /// ����  ����: <br/>

        public MAT070204()
        {
            InitializeComponent();

            this.cdvMatType.sFactory = GlobalVariable.gsAssyDefaultFactory;

            cboYear.Text = DateTime.Now.Year.ToString();

            if (DateTime.Now.Month.ToString().Length == 1)
            {
                cboMonth.Text = "0" + DateTime.Now.Month.ToString();
            }
            else
            {
                cboMonth.Text = DateTime.Now.Month.ToString();
            }            
            
            //cboBy.Enabled = false;            

            SortInit();
            GridColumnInit();

            // ���� Ÿ�� GCM���� ���� ��������
            //string strquery = string.Empty;
            //if (cboBy.Text == "WMS Management")
            //{
            //    strquery = "SELECT KEY_1, DATA_1 FROM MGCMTBLDAT WHERE FACTORY='" + GlobalVariable.gsAssyDefaultFactory + "' AND TABLE_NAME='MAT_TYPE'";
            //}
            //cdvMatType.sDynamicQuery = strquery;

            for (int i = DateTime.Now.Year - 3; i <= DateTime.Now.Year + 3; i++)
            {
                cboYear.Items.Add(i);
            }

        }

        #region ��ȿ�� �˻� �� �ʱ�ȭ
        /// <summary 1. ��ȿ�� �˻�>
        /// <remarks></remarks>
        /// </summary>
        /// <returns></returns>
        private Boolean CheckField()
        {
            if (cboMonth.Text == "" || cboBy.Text == "")
            {
                CmnFunction.ShowMsgBox(RptMessages.GetMessage("STD001", GlobalVariable.gcLanguage));
                return false;
            }

            return true;
        }

        /// <summary>
        /// 2. ��� ����
        /// </summary>
        private void GridColumnInit()
        {
            spdData.RPT_ColumnInit();            
            string lastDayOfMonth = System.DateTime.Parse((cboYear.Text + "-" + cboMonth.Text + "-01")).AddMonths(1).AddDays(-1).ToString("dd");
            //string lastDayOfMonth = firstDayOfMonth.AddMonths(1).AddDays(-1);
            //DateTime firstDayOfMonth = System.DateTime.Parse(cboYear.Text+cboMonth.Text+"010000"); 

            //string sToDate = DateTime.Parse(sFromDate).ToString("YYYYMMDD");
            //string sToDate = DateTime.Parse(sFromDate).AddMonths(1).AddDays(-1).ToString("yyyymmdd");
            
            spdData.RPT_AddBasicColumn("Type", 0, 0, Visibles.True, Frozen.True, Align.Left, Merge.True, Formatter.String, 35);
            spdData.RPT_AddBasicColumn("Item name", 0, 1, Visibles.True, Frozen.True, Align.Left, Merge.True, Formatter.String, 70);
            spdData.RPT_AddBasicColumn("Item Code", 0, 2, Visibles.True, Frozen.True, Align.Left, Merge.True, Formatter.String, 50);
            spdData.RPT_AddBasicColumn("warehousing", 0, 3, Visibles.True, Frozen.True, Align.Right, Merge.True, Formatter.String, 35);

            for (int i = 1; i <= int.Parse(lastDayOfMonth); i++)
            {
                spdData.RPT_AddBasicColumn(i + "��", 0, i+3, Visibles.True, Frozen.False, Align.Right, Merge.True, Formatter.Number, 35);
            }
            //spdData.RPT_AddBasicColumn("2��", 0, 5, Visibles.True, Frozen.False, Align.Right, Merge.True, Formatter.Number, 35);
            //spdData.RPT_AddBasicColumn("3��", 0, 6, Visibles.True, Frozen.False, Align.Right, Merge.True, Formatter.Number, 35);
            //spdData.RPT_AddBasicColumn("4��", 0, 7, Visibles.True, Frozen.False, Align.Right, Merge.True, Formatter.Number, 35);
            //spdData.RPT_AddBasicColumn("5��", 0, 8, Visibles.True, Frozen.False, Align.Right, Merge.True, Formatter.Number, 35);
            //spdData.RPT_AddBasicColumn("6��", 0, 9, Visibles.True, Frozen.False, Align.Right, Merge.True, Formatter.Number, 35);
            //spdData.RPT_AddBasicColumn("7��", 0, 10, Visibles.True, Frozen.False, Align.Right, Merge.True, Formatter.Number, 35);
            //spdData.RPT_AddBasicColumn("8��", 0, 11, Visibles.True, Frozen.False, Align.Right, Merge.True, Formatter.Number, 35);
            //spdData.RPT_AddBasicColumn("9��", 0, 12, Visibles.True, Frozen.False, Align.Right, Merge.True, Formatter.Number, 35);
            //spdData.RPT_AddBasicColumn("10��", 0, 13, Visibles.True, Frozen.False, Align.Right, Merge.True, Formatter.Number, 35);
            //spdData.RPT_AddBasicColumn("11��", 0, 14, Visibles.True, Frozen.False, Align.Right, Merge.True, Formatter.Number, 35);
            //spdData.RPT_AddBasicColumn("12��", 0, 15, Visibles.True, Frozen.False, Align.Right, Merge.True, Formatter.Number, 35);
            //spdData.RPT_AddBasicColumn("13��", 0, 16, Visibles.True, Frozen.False, Align.Right, Merge.True, Formatter.Number, 35);
            //spdData.RPT_AddBasicColumn("14��", 0, 17, Visibles.True, Frozen.False, Align.Right, Merge.True, Formatter.Number, 35);
            //spdData.RPT_AddBasicColumn("15��", 0, 18, Visibles.True, Frozen.False, Align.Right, Merge.True, Formatter.Number, 35);
            //spdData.RPT_AddBasicColumn("16��", 0, 19, Visibles.True, Frozen.False, Align.Right, Merge.True, Formatter.Number, 35);
            //spdData.RPT_AddBasicColumn("17��", 0, 20, Visibles.True, Frozen.False, Align.Right, Merge.True, Formatter.Number, 35);
            //spdData.RPT_AddBasicColumn("18��", 0, 21, Visibles.True, Frozen.False, Align.Right, Merge.True, Formatter.Number, 35);
            //spdData.RPT_AddBasicColumn("19��", 0, 22, Visibles.True, Frozen.False, Align.Right, Merge.True, Formatter.Number, 35);
            //spdData.RPT_AddBasicColumn("20��", 0, 23, Visibles.True, Frozen.False, Align.Right, Merge.True, Formatter.Number, 35);
            //spdData.RPT_AddBasicColumn("21��", 0, 24, Visibles.True, Frozen.False, Align.Right, Merge.True, Formatter.Number, 35);
            //spdData.RPT_AddBasicColumn("22��", 0, 25, Visibles.True, Frozen.False, Align.Right, Merge.True, Formatter.Number, 35);
            //spdData.RPT_AddBasicColumn("23��", 0, 26, Visibles.True, Frozen.False, Align.Right, Merge.True, Formatter.Number, 35);
            //spdData.RPT_AddBasicColumn("24��", 0, 27, Visibles.True, Frozen.False, Align.Right, Merge.True, Formatter.Number, 35);
            //spdData.RPT_AddBasicColumn("25��", 0, 28, Visibles.True, Frozen.False, Align.Right, Merge.True, Formatter.Number, 35);
            //spdData.RPT_AddBasicColumn("26��", 0, 29, Visibles.True, Frozen.False, Align.Right, Merge.True, Formatter.Number, 35);
            //spdData.RPT_AddBasicColumn("27��", 0, 30, Visibles.True, Frozen.False, Align.Right, Merge.True, Formatter.Number, 35);
            //spdData.RPT_AddBasicColumn("28��", 0, 31, Visibles.True, Frozen.False, Align.Right, Merge.True, Formatter.Number, 35);
            //spdData.RPT_AddBasicColumn("29��", 0, 32, Visibles.True, Frozen.False, Align.Right, Merge.True, Formatter.Number, 35);
            //spdData.RPT_AddBasicColumn("30��", 0, 33, Visibles.True, Frozen.False, Align.Right, Merge.True, Formatter.Number, 35);
            //spdData.RPT_AddBasicColumn("31��", 0, 34, Visibles.True, Frozen.False, Align.Right, Merge.True, Formatter.Number, 35);

            //spdData.RPT_ColumnConfigFromTable(btnSort); //Group�׸��� ������� �ݵ�� �������ٰ�.        
        }

        /// <summary>
        /// 3. Group By ����
        /// </summary>
        private void SortInit()
        {
            //if (cboBy.Text == "WMS ����")
            if (cboBy.SelectedIndex == 0)
            {
                ((udcTableForm)(this.btnSort.BindingForm)).AddRow("Type", "B.DATA_6", "B.DATA_6", true);
                ((udcTableForm)(this.btnSort.BindingForm)).AddRow("Item name", "B.DATA_1", "B.DATA_1", true);
                ((udcTableForm)(this.btnSort.BindingForm)).AddRow("Item Code", "A.MAT_ID", "A.MAT_ID", true);
            }
            else
            {
                ((udcTableForm)(this.btnSort.BindingForm)).AddRow("Type", "B.MAT_TYPE", "B.MAT_TYPE", true);
                ((udcTableForm)(this.btnSort.BindingForm)).AddRow("Item name", "B.MAT_DESC", "B.MAT_DESC", true);
                ((udcTableForm)(this.btnSort.BindingForm)).AddRow("Item Code", "A.MAT_ID", "A.MAT_ID", true);
            }
        }
        #endregion

        #region SQL ���� Build
        /// <summary>
        /// 4. ���� ����
        /// </summary>
        /// <returns></returns>

        private string MakeSqlString()
        {
            StringBuilder strSqlString = new StringBuilder();

            string QueryCond1 = null;
            string QueryCond2 = "B.MAT_TYPE, B.MAT_DESC, A.MAT_ID";

            string lastDayOfMonth = System.DateTime.Parse((cboYear.Text + "-" + cboMonth.Text + "-01")).AddMonths(1).AddDays(-1).ToString("dd");

            string sFromDate = cboYear.Text + cboMonth.Text + "010000";
            string sToDate = cboYear.Text + cboMonth.Text + lastDayOfMonth + "2359";

            udcTableForm tableForm = (udcTableForm)btnSort.BindingForm;

            QueryCond1 = tableForm.SelectedValueToQueryContainNull;
            //QueryCond2 = tableForm.SelectedValue2ToQueryContainNull;

            //if (cboBy.Text == "WMS ����")
            if (cboBy.SelectedIndex == 0)
            {
                strSqlString.AppendFormat("SELECT {0} " + "\n", QueryCond1);
                if (cboLotQty.Text == "LOT")
                {
                    strSqlString.Append("      , SUM(1) AS ISSUE_CNT " + "\n");
                }
                else
                {
                    strSqlString.Append("      , SUM(ISSUE_QUANTITY) AS ISSUE_CNT " + "\n");
                }
                for (int i = 1; i <= 9; i++)
                {
                    if (cboLotQty.Text == "LOT")
                    {
                        strSqlString.Append("      , SUM(DECODE(SUBSTR(A.TRAN_TIME,7,2),'0" + i + "',1,0)) AS A " + "\n");
                    }
                    else
                    {
                        strSqlString.Append("      , SUM(DECODE(SUBSTR(A.TRAN_TIME,7,2),'0" + i + "',ISSUE_QUANTITY,0)) AS A " + "\n");
                    }
                }
                for (int i = 10; i <= int.Parse(lastDayOfMonth); i++)
                {
                    if (cboLotQty.Text == "LOT")
                    {
                        strSqlString.Append("      , SUM(DECODE(SUBSTR(A.TRAN_TIME,7,2),'" + i + "',1,0)) AS A " + "\n");
                    }
                    else
                    {
                        strSqlString.Append("      , SUM(DECODE(SUBSTR(A.TRAN_TIME,7,2),'" + i + "',ISSUE_QUANTITY,0)) AS A " + "\n");
                    }
                }
                strSqlString.Append("  FROM CWMSLOTHIS@RPTTOMES A, (SELECT KEY_1, DATA_1, FACTORY, DATA_6 FROM MGCMTBLDAT WHERE FACTORY = '" + GlobalVariable.gsAssyDefaultFactory + "' AND TABLE_NAME = 'MAT_MASTER' ) B" + "\n");
                strSqlString.Append(" WHERE B.FACTORY = '" + GlobalVariable.gsAssyDefaultFactory + "'" + "\n");
                strSqlString.Append("   AND A.MAT_ID = B.KEY_1" + "\n");
                strSqlString.Append("   AND A.CLIENTID = '100'" + "\n");
                strSqlString.Append("   AND A.TRAN_TIME >='" + sFromDate + "'\n");
                strSqlString.Append("   AND A.TRAN_TIME <='" + sToDate + "'\n");

                //#region ��ȸ����(FACTORY, STEP, LOT_TYPE, PRODUCT, DATE)
                if (cdvMatType.Text.TrimEnd() != "")
                {
                    strSqlString.Append("      AND B.DATA_6='" + cdvMatType.txtValue + "'\n");
                }
                strSqlString.Append("   AND A.STORAGE_LOCATION = '2000'" + "\n");
                //#endregion

                strSqlString.Append("  GROUP BY B.DATA_6, A.MAT_ID, B.DATA_1" + "\n");
                strSqlString.Append("  ORDER BY A.MAT_ID" + "\n");
            }
            else // WMS �̰��� (MES������ ���� - ���̵�, ������)
            {
                strSqlString.AppendFormat("SELECT {0} " + "\n", QueryCond2);
                if (cboLotQty.Text == "LOT")
                {
                    strSqlString.Append("      , SUM(1) AS ISSUE_CNT " + "\n");
                }
                else
                {
                    strSqlString.Append("      , SUM(QTY_1) AS ISSUE_CNT " + "\n");
                }
                for (int i = 1; i <= 9; i++)
                {
                    if (cboLotQty.Text == "LOT")
                    {
                        strSqlString.Append("      , SUM(DECODE(SUBSTR(A.RECV_TIME,7,2),'0" + i + "',1,0)) AS A " + "\n");
                    }
                    else
                    {
                        strSqlString.Append("      , SUM(DECODE(SUBSTR(A.RECV_TIME,7,2),'0" + i + "',QTY_1,0)) AS A " + "\n");
                    }
                }
                for (int i = 10; i <= int.Parse(lastDayOfMonth); i++)
                {
                    if (cboLotQty.Text == "LOT")
                    {
                        strSqlString.Append("      , SUM(DECODE(SUBSTR(A.RECV_TIME,7,2),'" + i + "',1,0)) AS A " + "\n");
                    }
                    else
                    {
                        strSqlString.Append("      , SUM(DECODE(SUBSTR(A.RECV_TIME,7,2),'" + i + "',QTY_1,0)) AS A " + "\n");
                    }
                }
                strSqlString.Append("  FROM CWIPMATSLP@RPTTOMES A, MWIPMATDEF B" + "\n");
                strSqlString.Append(" WHERE B.FACTORY = '" + GlobalVariable.gsAssyDefaultFactory + "'" + "\n");
                strSqlString.Append("   AND A.MAT_ID = B.MAT_ID" + "\n");
                strSqlString.Append("   AND B.DELETE_FLAG = ' '" + "\n");
                strSqlString.Append("   AND A.RECV_TIME >='" + sFromDate + "'\n");
                strSqlString.Append("   AND A.RECV_TIME <='" + sToDate + "'\n");

                //#region ��ȸ����(FACTORY., STEP, LOT_TYPE, PRODUCT, DATE)
                if (cdvMatType.Text.TrimEnd() != "")
                {
                    strSqlString.Append("      AND B.MAT_TYPE='" + cdvMatType.txtValue + "'\n");
                }
                strSqlString.Append("   AND A.RESV_FIELD_3 = B.MAT_TYPE" + "\n");
                //#endregion

                strSqlString.Append("  GROUP BY B.MAT_TYPE,A.MAT_ID, B.MAT_DESC" + "\n");
                strSqlString.Append("  ORDER BY A.MAT_ID" + "\n");    
            }

            if (GlobalVariable.gsUserGroup == "ADMIN_GROUP" || GlobalVariable.gsUserGroup == "HANA_ADMIN_GROUP")
            {
                System.Windows.Forms.Clipboard.SetText(strSqlString.ToString());
            }

            return strSqlString.ToString();
        }
        #endregion
        
        #region EVENT ó��
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
                int sub = ((udcTableForm)(this.btnSort.BindingForm)).GetCheckedValue(2);

                ////by John (����¥��)
                ////1.Griid �հ� ǥ��
                //int[] rowType = spdData.RPT_DataBindingWithSubTotal(dt, 0, sub + 1, 4, null, null, btnSort);
                int[] rowType = spdData.RPT_DataBindingWithSubTotal(dt, 0, 1, 3, null, null, btnSort);

                //spdData.DataSource = dt;
                spdData.ActiveSheet.Columns[0].BackColor = Color.Turquoise;
                spdData.ActiveSheet.Columns[1].BackColor = Color.WhiteSmoke;
                spdData.ActiveSheet.Columns[2].BackColor = Color.WhiteSmoke;
                spdData.ActiveSheet.Columns[3].BackColor = Color.LightYellow;
                
                
                //spdData.RPT_FillDataSelectiveCells("Total", 0, 4, 0, 1, true, Align.Center, VerticalAlign.Center);

                ////2. Į�� ����(�ʿ��ϴٸ�..)
                //spdData.Sheets[0].FrozenColumnCount = 9;x

                ////3. Total�κ� ������
                

                //4. Column Auto Fit
                spdData.RPT_AutoFit(false);

                dt.Dispose();

                //cdvMatType.Text = "";
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

        private void btnExcelExport_Click(object sender, EventArgs e)
        {
            ExcelHelper.Instance.subMakeExcel(spdData, this.lblTitle.Text, " ^ ", " ^ ");
        }

       

        private void cdvMatType_ValueButtonPress(object sender, EventArgs e)
        {
            string strQuery = string.Empty;

            //if (cboBy.Text == "WMS ����")
            if (cboBy.SelectedIndex == 0)
            {
                strQuery = "SELECT KEY_1, DATA_1 FROM MGCMTBLDAT WHERE FACTORY='" + GlobalVariable.gsAssyDefaultFactory + "' AND TABLE_NAME='MAT_TYPE'";
            }
            else // WMS �̰��� - MES �״�� (���̵�, ������)
            {
                strQuery = "SELECT KEY_1, DATA_1 FROM MGCMTBLDAT WHERE FACTORY='" + GlobalVariable.gsAssyDefaultFactory + "' AND TABLE_NAME='MATERIAL_TYPE' AND KEY_1 IN ('BD', 'TE')";
            }

            cdvMatType.sDynamicQuery = strQuery;

            //// 2011-09-21-������ : ����Ʈ �ʱ�ȭ �� �ٽ� ��� ����...�ణ �̻������� �������� �����ʹ� �ش� �̺�Ʈ ��������� ���� �� ����.
            //// ���� ����Ʈ�� �Ѱ��� �����ϸ� ����, �Ѱ��� �������� ������ "ALL" ó��
            //if (cdvVendor.ValueItems.Count > 0)
            //{
            //    cdvVendor.ResetText();
            //}
            //else
            //{
            //    // �� �κ� ó�� ���ϸ� ������ ���� ������ �� ��.
            //    cdvVendor.Text = "ALL";
            //}
        }

        private void cboBy_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                cdvMatType.Text = "";
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
    }
}
