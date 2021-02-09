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
    public partial class PRD010703 : Miracom.SmartWeb.UI.Controls.udcCUSReport002
    {
        string[] dayArray = new string[4];
        string[] dayArray2 = new string[3]; //�÷� ����� (��.�� �� ǥ�� - 04.19)

        /// <summary>
        /// Ŭ  ��  ��: PRD010703<br/>
        /// Ŭ�������: ����(����) ���ֺ� ����<br/>
        /// ��  ��  ��: �ϳ�����ũ�� ������<br/>
        /// �����ۼ���: 2010-04-23<br/>
        /// ��  ����: ���� ���� ���ֺ���� ��ȸ<br/>
        /// ����  ����: <br/>       
        /// 2012-08-21-������ : ���� ��ü ���� ��� ���� �߻� ���� �ʵ��� ����
        /// 2013-10-14-��ο�: LOT TYPE ALL, P%, E% �������κ���
        
        /// </summary>
        public PRD010703()
        {
            InitializeComponent();            
            SortInit();
            
            cdvDate.Text = DateTime.Now.AddDays(-1).ToString("yyyy-MM-dd");

            GridColumnInit(); //��� ����¥�� 
            cdvFactory.Text = GlobalVariable.gsAssyDefaultFactory;
            this.SetFactory(cdvFactory.txtValue);
            //cdvType.sFactory = cdvFactory.txtValue;
            cdvPSite.sFactory = cdvFactory.txtValue;
            cdvFactory.Enabled = false;
            cdvPSite.Text = "ALL";
        }

        #region SortInit

        /// <summary>
        /// SortInit
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SortInit()
        {
            ((udcTableForm)(this.btnSort.BindingForm)).AddRow("Outsourcing company", "NVL((SELECT DATA_1 FROM MGCMTBLDAT WHERE TABLE_NAME = 'H_VENDOR_PLATING' AND KEY_1 = A.SHP_FAC AND ROWNUM=1), '-') AS SHP_FAC", "A.SHP_FAC", true);
            ((udcTableForm)(this.btnSort.BindingForm)).AddRow("Customer", "(SELECT DATA_1 FROM MGCMTBLDAT WHERE TABLE_NAME = 'H_CUSTOMER' AND KEY_1 = A.CUSTOMER AND ROWNUM=1) AS CUSTOMER", "A.CUSTOMER", true);
            ((udcTableForm)(this.btnSort.BindingForm)).AddRow("LD Count", "A.LEAD_COUNT", "A.LEAD_COUNT", true);
            ((udcTableForm)(this.btnSort.BindingForm)).AddRow("Package", "A.PKG", "A.PKG", true);
            ((udcTableForm)(this.btnSort.BindingForm)).AddRow("Plating method", "A.PLATING_TYPE", "A.PLATING_TYPE", true);            
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
            GetDate();
            spdData.RPT_ColumnInit();

            spdData.RPT_AddBasicColumn("Outsourcing company", 0, 0, Visibles.True, Frozen.True, Align.Center, Merge.True, Formatter.String, 60);
            spdData.RPT_AddBasicColumn("Customer", 0, 1, Visibles.True, Frozen.True, Align.Center, Merge.True, Formatter.String, 70);
            spdData.RPT_AddBasicColumn("LD Count", 0, 2, Visibles.True, Frozen.True, Align.Center, Merge.True, Formatter.String, 60);
            spdData.RPT_AddBasicColumn("Package", 0, 3, Visibles.True, Frozen.True, Align.Center, Merge.True, Formatter.String, 60);            
            spdData.RPT_AddBasicColumn("Plating method", 0, 4, Visibles.True, Frozen.True, Align.Center, Merge.True, Formatter.String, 60);
            spdData.RPT_AddBasicColumn("plan", 0, 5, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 70);
            spdData.RPT_AddBasicColumn("actual", 0, 6, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 70);
            spdData.RPT_AddBasicColumn("Progress rate", 0, 7, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Double1, 50);                
            spdData.RPT_AddBasicColumn("3day Performance", 0, 8, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.String, 80);
            spdData.RPT_AddBasicColumn(dayArray2[0], 1, 8, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 70);
            spdData.RPT_AddBasicColumn(dayArray2[1], 1, 9, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 70);
            spdData.RPT_AddBasicColumn(dayArray2[2], 1, 10, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 70);
            spdData.RPT_MerageHeaderColumnSpan(0, 8 , 3);

            spdData.RPT_AddBasicColumn("Ratio", 0, 11, Visibles.True, Frozen.False, Align.Center, Merge.False, Formatter.Double2, 60);

            spdData.RPT_MerageHeaderRowSpan(0, 0, 2);
            spdData.RPT_MerageHeaderRowSpan(0, 1, 2);
            spdData.RPT_MerageHeaderRowSpan(0, 2, 2);
            spdData.RPT_MerageHeaderRowSpan(0, 3, 2);
            spdData.RPT_MerageHeaderRowSpan(0, 4, 2);
            spdData.RPT_MerageHeaderRowSpan(0, 5, 2);
            spdData.RPT_MerageHeaderRowSpan(0, 6, 2);
            spdData.RPT_MerageHeaderRowSpan(0, 7, 2);
            spdData.RPT_MerageHeaderRowSpan(0, 11, 2);
            
            spdData.RPT_ColumnConfigFromTable(btnSort); //Group�׸��� ������� �ݵ�� �������ٰ�.
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
                //int sub = ((udcTableForm)(this.btnSort.BindingForm)).GetCheckedValue(2);

                ////by John (����¥��)
                ////1.Griid �հ� ǥ��
                int[] rowType = spdData.RPT_DataBindingWithSubTotal(dt, 0, 1, 5, null, null, btnSort);

                ////2. Į�� ����(�ʿ��ϴٸ�..)
                //spdData.Sheets[0].FrozenColumnCount = 11;

                ////3. Total�κ� ������
                spdData.RPT_FillDataSelectiveCells("Total", 0, 5, 0, 1, true, Align.Center, VerticalAlign.Center);

                //4. Column Auto Fit
                spdData.RPT_AutoFit(false);

                //5. ������ �� ��հ� ���ϱ�(GrandTotal�κ� & SubTotal Ư�� �÷� �̿� ���� ���ϱ�)
                SetAvgVertical(1, 7);

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
            if (cdvFactory.Text.TrimEnd() == "")
            {
                CmnFunction.ShowMsgBox(RptMessages.GetMessage("STD001", GlobalVariable.gcLanguage));
                return false;
            }

            return true;
        }

        #endregion


        /// <summary>
        /// 2010-04-21-������ : AVG ���ϱ�.. SubTotal, GrandTotal ���Ҷ� Ư�� �÷� ������ ���� ���Ҷ�..        
        /// 
        /// </summary>
        /// <param name="nSampleNormalRowPos"></param>
        /// <param name="nColPos"></param>
        public void SetAvgVertical(int nSampleNormalRowPos, int nColPos)
        {
            Color color = spdData.ActiveSheet.Cells[nSampleNormalRowPos, nColPos].BackColor;
            double iassyMon = 0;
            double itargetMon = 0;

            iassyMon = Convert.ToDouble(spdData.ActiveSheet.Cells[0, 6].Value);
            itargetMon = Convert.ToDouble(spdData.ActiveSheet.Cells[0, 5].Value);

            // �и��� 0�� �ƴҰ�쿡�� ����Ѵ�..
            if (itargetMon != 0)
            {
                spdData.ActiveSheet.Cells[0, nColPos].Value = (iassyMon / itargetMon) * 100;

                for (int i = 1; i < spdData.ActiveSheet.Rows.Count; i++)
                {
                    if (spdData.ActiveSheet.Cells[i, nColPos].BackColor != color)
                    {
                        iassyMon = Convert.ToDouble(spdData.ActiveSheet.Cells[i, 6].Value);
                        itargetMon = Convert.ToDouble(spdData.ActiveSheet.Cells[i, 5].Value);

                        if (itargetMon != 0)
                        {
                            spdData.ActiveSheet.Cells[i, nColPos].Value = iassyMon / itargetMon * 100;
                        }
                    }
                }
            }
        }

        #region MakeSqlString
        private string MakeSqlString()
        {
            StringBuilder strSqlString = new StringBuilder();

            string QueryCond1 = null;
            string QueryCond2 = null;
            
            DateTime selectDate = Convert.ToDateTime(cdvDate.Value.ToString("yyyy-MM") + "-01");
            string startDate = selectDate.AddDays(-1).ToString("yyyyMMdd")  + "220000";
            string endDate = cdvDate.Value.ToString("yyyyMMdd") + "215959";

            udcTableForm tableForm = (udcTableForm)btnSort.BindingForm;

            QueryCond1 = tableForm.SelectedValueToQueryContainNull;
            QueryCond2 = tableForm.SelectedValue2ToQueryContainNull;


            strSqlString.AppendFormat("SELECT {0}" + "\n", QueryCond1);
            strSqlString.Append("     , SUM(A.PLAN_QTY * B.PLATING_PRICE)  AS PLAN_QTY " + "\n");
            strSqlString.Append("     , SUM(A.MON_RECV_QTY * B.PLATING_PRICE) AS MON_RECV_QTY " + "\n");
            strSqlString.Append("     , DECODE(SUM(A.PLAN_QTY * B.PLATING_PRICE), 0, 0, ROUND((SUM(MON_RECV_QTY * B.PLATING_PRICE)/SUM(PLAN_QTY * B.PLATING_PRICE))*100,1)) AS JINDO " + "\n");
            strSqlString.Append("     , SUM(A.DAY_RECV_QTY_1 * B.PLATING_PRICE) AS DAY_RECV_QTY_1 " + "\n");
            strSqlString.Append("     , SUM(A.DAY_RECV_QTY_2 * B.PLATING_PRICE) AS DAY_RECV_QTY_2 " + "\n");
            strSqlString.Append("     , SUM(A.DAY_RECV_QTY_3 * B.PLATING_PRICE) AS DAY_RECV_QTY_3 " + "\n");
            strSqlString.Append("     , ROUND(RATIO_TO_REPORT(SUM(A.MON_RECV_QTY * B.PLATING_PRICE)) OVER () * 100, 2) AS PER " + "\n");            
            strSqlString.Append("  FROM ( " + "\n");
            strSqlString.Append("        SELECT A.SHP_FAC " + "\n");
            strSqlString.Append("             , B.MAT_GRP_1 AS CUSTOMER " + "\n");
            strSqlString.Append("             , B.MAT_GRP_6 AS LEAD_COUNT " + "\n");
            strSqlString.Append("             , B.MAT_GRP_3 AS PKG " + "\n");
            strSqlString.Append("             , B.MAT_CMF_1 AS PLATING_TYPE" + "\n");
            strSqlString.Append("             , SUM(MON_RECV_QTY) AS MON_RECV_QTY" + "\n");            
            strSqlString.Append("             , SUM(DAY_RECV_QTY_1) AS DAY_RECV_QTY_1" + "\n");
            strSqlString.Append("             , SUM(DAY_RECV_QTY_2) AS DAY_RECV_QTY_2 " + "\n");
            strSqlString.Append("             , SUM(DAY_RECV_QTY_3) AS DAY_RECV_QTY_3   " + "\n");                       
            strSqlString.Append("             , 0 AS PLAN_QTY" + "\n");
            strSqlString.Append("          FROM ( " + "\n");
            strSqlString.Append("                 SELECT HLD.FACTORY " + "\n");
            strSqlString.Append("                      , HLD.MAT_ID " + "\n");
            strSqlString.Append("                      , HLD.LOT_ID   " + "\n");
            strSqlString.Append("                      , HLD.QTY_1 AS SHIP_QTY " + "\n");
            strSqlString.Append("                      , HLD.HOLD_TRAN_TIME " + "\n");
            strSqlString.Append("                      , HLD.RELEASE_TRAN_TIME " + "\n");
            strSqlString.Append("                      , HIS.LOT_CMF_5 AS LOT_TYPE " + "\n");
            strSqlString.Append("                      , HIS.TRAN_CMF_2 AS SHP_FAC " + "\n");
            strSqlString.Append("                      , CASE WHEN HLD.HOLD_TRAN_TIME BETWEEN '" + startDate + "' AND '" + endDate + "' " + "\n");
            strSqlString.Append("                             THEN DECODE(TRIM(HLD.RELEASE_TRAN_TIME), NULL, 0, HLD.QTY_1) " + "\n");
            strSqlString.Append("                             ELSE 0 " + "\n");
            strSqlString.Append("                         END AS MON_RECV_QTY        " + "\n");
            strSqlString.Append("                      , CASE WHEN HLD.HOLD_TRAN_TIME BETWEEN '" + dayArray[3] + "220000' AND '" + dayArray[0] + "215959' " + "\n");
            strSqlString.Append("                             THEN DECODE(TRIM(HLD.RELEASE_TRAN_TIME), NULL, 0, HLD.QTY_1) " + "\n");
            strSqlString.Append("                             ELSE 0 " + "\n");
            strSqlString.Append("                         END AS DAY_RECV_QTY_1 " + "\n");
            strSqlString.Append("                      , CASE WHEN HLD.HOLD_TRAN_TIME BETWEEN '" + dayArray[0] + "220000' AND '" + dayArray[1] + "215959' " + "\n");
            strSqlString.Append("                             THEN DECODE(TRIM(HLD.RELEASE_TRAN_TIME), NULL, 0, HLD.QTY_1) " + "\n");
            strSqlString.Append("                             ELSE 0 " + "\n");
            strSqlString.Append("                         END AS DAY_RECV_QTY_2 " + "\n");
            strSqlString.Append("                      , CASE WHEN HLD.HOLD_TRAN_TIME BETWEEN '" + dayArray[1] + "220000' AND '" + endDate + "' " + "\n");
            strSqlString.Append("                             THEN DECODE(TRIM(HLD.RELEASE_TRAN_TIME), NULL, 0, HLD.QTY_1) " + "\n");
            strSqlString.Append("                             ELSE 0 " + "\n");
            strSqlString.Append("                         END AS DAY_RECV_QTY_3  " + "\n");
            strSqlString.Append("                      , CASE WHEN HLD.HOLD_TRAN_TIME BETWEEN '" + dayArray[1] + "220000' AND '" + endDate + "' " + "\n");
            strSqlString.Append("                             THEN TRUNC(DECODE(TRIM(HLD.RELEASE_TRAN_TIME), NULL, SYSDATE,TO_DATE(HLD.RELEASE_TRAN_TIME,'YYYYMMDDHH24MISS')) - TO_DATE(HLD.HOLD_TRAN_TIME,'YYYYMMDDHH24MISS'),2) " + "\n");
            strSqlString.Append("                             ELSE 0 " + "\n");
            strSqlString.Append("                         END AS TAT  " + "\n");
            strSqlString.Append("                   FROM RWIPLOTHLD HLD " + "\n");
            strSqlString.Append("                      , RWIPLOTHIS HIS " + "\n");
            strSqlString.Append("                  WHERE 1=1 " + "\n");
            strSqlString.Append("                    AND HLD.FACTORY = '" + GlobalVariable.gsAssyDefaultFactory + "' " + "\n");
            strSqlString.Append("                    AND HLD.FACTORY = HIS.FACTORY(+) " + "\n");
            strSqlString.Append("                    AND HLD.LOT_ID = HIS.LOT_ID(+) " + "\n");
            strSqlString.Append("                    AND HLD.HIST_SEQ = HIS.HIST_SEQ(+) " + "\n");
            strSqlString.Append("                    AND HLD.OPER = 'A1450'  " + "\n");

            // �ſ� 1��, 2�� ��� 3���� ������ �����ͷ� WORK_DATE ����Ѵ�.
            if (endDate.Substring(6, 2).Equals("01") || endDate.Substring(6, 2).Equals("02"))
            {
                strSqlString.Append("                    AND HLD.HOLD_TRAN_TIME BETWEEN '" + dayArray[0] + "220000' AND '" + endDate + "' " + "\n");
            }
            else
            {
                strSqlString.Append("                    AND HLD.HOLD_TRAN_TIME BETWEEN '" + startDate + "' AND '" + endDate + "' " + "\n");                
            }
                        
            strSqlString.Append("                    AND HIS.HOLD_CODE = 'S0' " + "\n");
            strSqlString.Append("                    AND HLD.HIST_DEL_FLAG = ' ' " + "\n");
            strSqlString.Append("                    AND HIS.HIST_DEL_FLAG = ' ' " + "\n");
            strSqlString.Append("                    AND HIS.TRAN_CODE = 'HOLD' " + "\n");
            strSqlString.Append("                    AND HIS.HOLD_CODE = 'S0' " + "\n");

            /*
            if (cdvType.txtValue != "" && cdvType.txtValue != "ALL")
            {
                strSqlString.Append("                    AND HIS.LOT_CMF_5 " + cdvType.SelectedValueToQueryString + "\n");
            }
            */

            if (cdvLotType.Text != "ALL")
            {
                strSqlString.Append("                    AND HIS.LOT_CMF_5 LIKE '" + cdvLotType.Text + "'" + "\n");
            }

            strSqlString.Append("               ) A " + "\n");
            strSqlString.Append("             , MWIPMATDEF B " + "\n");
            strSqlString.Append("         WHERE 1 = 1            " + "\n");
            strSqlString.Append("           AND A.FACTORY = B.FACTORY " + "\n");
            strSqlString.Append("           AND A.MAT_ID = B.MAT_ID            " + "\n");
            strSqlString.Append("           AND B.DELETE_FLAG <> 'Y'" + "\n");
            strSqlString.Append("           AND B.MAT_TYPE = 'FG'" + "\n");
            strSqlString.Append("           AND B.MAT_CMF_1 IN ('Sn-Bi','Sn 100')" + "\n");
            strSqlString.Append("         GROUP BY A.SHP_FAC, B.MAT_GRP_1, B.MAT_GRP_6, B.MAT_GRP_3 , B.MAT_CMF_1 " + "\n");
            strSqlString.Append("        UNION ALL" + "\n");
            strSqlString.Append("        SELECT SHP_FAC, CUSTOMER, LEAD_COUNT, PKG, PLATING_TYPE" + "\n");
            strSqlString.Append("             , 0 AS MON_RECV_QTY" + "\n");            
            strSqlString.Append("             , 0 AS DAY_RECV_QTY_1" + "\n");
            strSqlString.Append("             , 0 AS DAY_RECV_QTY_2 " + "\n");
            strSqlString.Append("             , 0 AS DAY_RECV_QTY_3 " + "\n");               
            strSqlString.Append("             , SUM(PLAN_QTY) AS PLAN_QTY" + "\n");
            strSqlString.Append("          FROM CWIPTINPLN@RPTTOMES" + "\n");
            strSqlString.Append("         WHERE FACTORY = '" + GlobalVariable.gsAssyDefaultFactory + "' " + "\n");
            strSqlString.Append("           AND PLAN_MONTH = '" + cdvDate.Value.ToString("yyyyMM") + "'" + "\n");
            strSqlString.Append("         GROUP BY SHP_FAC, CUSTOMER, LEAD_COUNT, PKG, PLATING_TYPE " + "\n");
            strSqlString.Append("       ) A " + "\n");
            strSqlString.Append("     , ( " + "\n");
            strSqlString.Append("        SELECT SHP_FAC, CUSTOMER, LEAD_COUNT, PKG, PLATING_TYPE, PLATING_PRICE " + "\n");
            strSqlString.Append("          FROM CWIPTINPRI@RPTTOMES " + "\n");
            strSqlString.Append("         WHERE FACTORY = '" + GlobalVariable.gsAssyDefaultFactory + "' " + "\n");
            strSqlString.Append("           AND HIST_DEL_FLAG = ' ' " + "\n");
            strSqlString.Append("           AND START_DAY <= '" + cdvDate.Value.ToString("yyyyMMdd") + "' " + "\n");
            strSqlString.Append("           AND END_DAY >= '" + cdvDate.Value.ToString("yyyyMMdd") + "' " + "\n");            
            strSqlString.Append("       ) B " + "\n");
            strSqlString.Append(" WHERE 1 = 1 " + "\n");
            strSqlString.Append("   AND A.SHP_FAC = B.SHP_FAC(+) " + "\n");
            strSqlString.Append("   AND A.CUSTOMER = B.CUSTOMER(+) " + "\n");
            strSqlString.Append("   AND A.LEAD_COUNT = B.LEAD_COUNT(+)" + "\n");
            strSqlString.Append("   AND A.PKG = B.PKG(+) " + "\n");
            strSqlString.Append("   AND A.PLATING_TYPE = B.PLATING_TYPE(+) " + "\n");

            // ���� ��ü ���ÿ� ���� SQL�� ����
            if (cdvPSite.Text != "ALL" && cdvPSite.Text != "")
                strSqlString.AppendFormat("   AND A.SHP_FAC {0} " + "\n", cdvPSite.SelectedValueToQueryString);

            //�� ��ȸ�� ���� SQL�� ����                        
            if (udcWIPCondition1.Text != "ALL" && udcWIPCondition1.Text != "")                
                strSqlString.AppendFormat("   AND A.CUSTOMER {0} " + "\n", udcWIPCondition1.SelectedValueToQueryString);

            //if (udcWIPCondition2.Text != "ALL" && udcWIPCondition2.Text != "")
            //    strSqlString.AppendFormat("   AND MAT.MAT_GRP_2 {0} " + "\n", udcWIPCondition2.SelectedValueToQueryString);

            if (udcWIPCondition3.Text != "ALL" && udcWIPCondition3.Text != "")
                strSqlString.AppendFormat("   AND A.PKG {0} " + "\n", udcWIPCondition3.SelectedValueToQueryString);

            //if (udcWIPCondition4.Text != "ALL" && udcWIPCondition4.Text != "")
            //    strSqlString.AppendFormat("   AND MAT.MAT_GRP_4 {0} " + "\n", udcWIPCondition4.SelectedValueToQueryString);

            //if (udcWIPCondition5.Text != "ALL" && udcWIPCondition5.Text != "")
            //    strSqlString.AppendFormat("   AND MAT.MAT_GRP_5 {0} " + "\n", udcWIPCondition5.SelectedValueToQueryString);

            if (udcWIPCondition6.Text != "ALL" && udcWIPCondition6.Text != "")
                strSqlString.AppendFormat("   AND A.LEAD_COUNT {0} " + "\n", udcWIPCondition6.SelectedValueToQueryString);

            //if (udcWIPCondition7.Text != "ALL" && udcWIPCondition7.Text != "")
            //    strSqlString.AppendFormat("   AND MAT.MAT_GRP_7 {0} " + "\n", udcWIPCondition7.SelectedValueToQueryString);

            //if (udcWIPCondition8.Text != "ALL" && udcWIPCondition8.Text != "")
            //    strSqlString.AppendFormat("   AND MAT.MAT_GRP_8 {0} " + "\n", udcWIPCondition8.SelectedValueToQueryString);                        

            strSqlString.AppendFormat(" GROUP BY {0}" + "\n", QueryCond2);
            strSqlString.AppendFormat(" ORDER BY {0}" + "\n", QueryCond2);
            
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
        #endregion

        #region FactorySelectChanged

        /// <summary>
        /// FactorySelectChanged
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cdvFactory_ValueSelectedItemChanged(object sender, MCCodeViewSelChanged_EventArgs e)
        {
            this.SetFactory(cdvFactory.txtValue);
            //cdvType.sFactory = cdvFactory.txtValue;
            cdvPSite.sFactory = cdvFactory.txtValue;
        }
        #endregion

        private void GetDate()
        {
            dayArray[0] = cdvDate.Value.AddDays(-2).ToString("yyyyMMdd"); // 3day(������)
            dayArray[1] = cdvDate.Value.AddDays(-1).ToString("yyyyMMdd"); // 3day(����)
            dayArray[2] = cdvDate.Value.ToString("yyyyMMdd");             // 3day(��ȸ��)
            dayArray[3] = cdvDate.Value.AddDays(-3).ToString("yyyyMMdd"); // ��������

            dayArray2[0] = cdvDate.Value.AddDays(-2).ToString("MM.dd"); // 3day(������)
            dayArray2[1] = cdvDate.Value.AddDays(-1).ToString("MM.dd"); // 3day(����)
            dayArray2[2] = cdvDate.Value.ToString("MM.dd");             // 3day(��ȸ��)
        }
    }
}

