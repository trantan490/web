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
using System.Windows.Forms.DataVisualization.Charting;

namespace Hana.RAS
{
    public partial class RAS020107 : Miracom.SmartWeb.UI.Controls.udcCUSReport002
    {           
        /// <summary>
        /// Ŭ  ��  ��: RAS020107<br/>
        /// Ŭ�������: ���� ���� ���귮 ��� ���귮(����)<br/>
        /// ��  ��  ��: �ϳ�����ũ�� ������<br/>
        /// �����ۼ���: 2016-04-11<br/>
        /// ��  ����: ���� ���� ���귮 ��� ���귮(������C ��û)<br/>
        /// ����  ����: <br/> 
        /// </summary>
        public RAS020107()
        {
            InitializeComponent();

            cdvFromToDate.AutoBinding();
            SortInit();
            GridColumnInit();

            cdvFromToDate.DaySelector.SelectedValue = "WEEK";
            cdvFromToDate.DaySelector.Enabled = false;
            cdvFromToDate.FromWeek.Text = "1";
                        
            this.SetFactory(GlobalVariable.gsAssyDefaultFactory);
            this.cdvFactory.sFactory = GlobalVariable.gsAssyDefaultFactory;
            cdvFactory.Text = GlobalVariable.gsAssyDefaultFactory;            
        }

        #region �ʱ�ȭ �� ��ȿ�� �˻�
        /// <summary 1. ��ȿ�� �˻�>
        /// <remarks></remarks>
        /// </summary>
        /// <returns></returns>
        private Boolean CheckField()
        {    
            return true;
        }

        /// <summary>
        /// 2. ��� ����
        /// </summary>
        private void GridColumnInit()
        {            
            spdData.RPT_ColumnInit();

            string[] strDate = cdvFromToDate.getSelectDate();

            try
            {
                spdData.RPT_ColumnInit();

                spdData.RPT_AddBasicColumn("Team in charge", 0, 0, Visibles.False, Frozen.True, Align.Center, Merge.True, Formatter.String, 70);
                spdData.RPT_AddBasicColumn("��Ʈ", 0, 1, Visibles.False, Frozen.True, Align.Center, Merge.True, Formatter.String, 70);
                spdData.RPT_AddBasicColumn("EQP Type", 0, 2, Visibles.True, Frozen.True, Align.Center, Merge.True, Formatter.String, 100);
                spdData.RPT_AddBasicColumn("Maker", 0, 3, Visibles.False, Frozen.True, Align.Center, Merge.True, Formatter.String, 70);
                spdData.RPT_AddBasicColumn("Model", 0, 4, Visibles.False, Frozen.True, Align.Center, Merge.True, Formatter.String, 70);
                spdData.RPT_AddBasicColumn("Equipment name", 0, 5, Visibles.True, Frozen.True, Align.Center, Merge.True, Formatter.String, 50);
                spdData.RPT_AddBasicColumn("CAPA (UPEH average)", 0, 6, Visibles.True, Frozen.True, Align.Right, Merge.False, Formatter.Number, 80);

                for (int i = 0; i < cdvFromToDate.SubtractBetweenFromToDate + 1; i++)
                {
                    spdData.RPT_AddBasicColumn(strDate[i], 0, 7+i, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 70);                    
                }
                
                spdData.RPT_ColumnConfigFromTable(btnSort);                
            }
            catch (Exception ex)
            {
                CmnFunction.ShowMsgBox(ex.Message);
            }
        }

        /// <summary>
        /// 3. Group By ����
        /// </summary>
        private void SortInit()
        {
            ((udcTableForm)(this.btnSort.BindingForm)).Clear();
            ((udcTableForm)(this.btnSort.BindingForm)).AddRow("Team in charge", "TEAM", "RES.RES_GRP_1", "RES_GRP_1", false);
            ((udcTableForm)(this.btnSort.BindingForm)).AddRow("Part", "PART", "RES.RES_GRP_2", "RES_GRP_2", false);
            ((udcTableForm)(this.btnSort.BindingForm)).AddRow("EQP Type", "EQP_TYPE", "RES.RES_GRP_3", "RES_GRP_3", true);
            ((udcTableForm)(this.btnSort.BindingForm)).AddRow("Maker", "MAKER", "RES.RES_GRP_5", "RES_GRP_5", false);
            ((udcTableForm)(this.btnSort.BindingForm)).AddRow("Model", "MODEL", "RES.RES_GRP_6", "RES_GRP_6", false);
            ((udcTableForm)(this.btnSort.BindingForm)).AddRow("Equipment name", "RES_ID", "RES.RES_ID", "RES_ID", true);

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

            string QueryCond1;
            string QueryCond2;
            string QueryCond3;                      
            string strDecodeQuery = null;            
            string sKpcsValue;         // Kpcs ���п� ���� ������ �и� ��
            
            udcTableForm tableForm = (udcTableForm)btnSort.BindingForm;
            QueryCond1 = tableForm.SelectedValueToQueryContainNull;
            QueryCond2 = tableForm.SelectedValue2ToQueryContainNull;
            QueryCond3 = tableForm.SelectedValue3ToQueryContainNull;
            
            // kpcs ���ÿ� ���� �и� �� ���� �Ѵ�.
            if (ckbKpcs.Checked == true)
            {         
                sKpcsValue = "1000";         
            }
            else
            {
                sKpcsValue = "1";
            }
                        
            #region �ְ� ��ȸ

            string sFrom = cdvFromToDate.FromWeek.SelectedValue.ToString();
            string sTo = cdvFromToDate.ToWeek.SelectedValue.ToString();
            string[] selectDate = cdvFromToDate.getSelectDate();            

            for (int i = 0; i < cdvFromToDate.SubtractBetweenFromToDate + 1; i++)
            {
                strDecodeQuery += "     , ROUND(SUM(DECODE(WEEK, '" + selectDate[i].ToString() + "', QTY, 0)) / " + sKpcsValue + ",0) AS W" + i + "\n";
            }

            strSqlString.AppendFormat("SELECT {0} " + "\n", QueryCond2);
            strSqlString.Append("     , ROUND(AVG(UPH.UPEH) * 24 * 7 * 0.7 / " + sKpcsValue + ", 0) AS CAPA" + "\n");
            strSqlString.Append(strDecodeQuery);
            strSqlString.Append("  FROM (" + "\n");
            strSqlString.Append("        SELECT B.PLAN_YEAR || LPAD(B.PLAN_WEEK, 2, 0) AS WEEK" + "\n");
            strSqlString.Append("             , A.RES_ID " + "\n");
            strSqlString.Append("             , A.MAT_ID" + "\n");
            strSqlString.Append("             , A.QTY" + "\n");
            strSqlString.Append("          FROM CSUMRASMOV@RPTTOMES A" + "\n");
            strSqlString.Append("             , MWIPCALDEF B" + "\n");
            strSqlString.Append("         WHERE 1=1" + "\n");
            strSqlString.Append("           AND A.WORK_DAY = B.SYS_DATE   " + "\n");
            strSqlString.Append("           AND A.FACTORY = '" + cdvFactory.Text + "'" + "\n");
            strSqlString.Append("           AND B.PLAN_YEAR || LPAD(B.PLAN_WEEK, 2, 0) BETWEEN '" + sFrom + "' AND '" + sTo + "'" + "\n");
            strSqlString.Append("           AND B.CALENDAR_ID = 'OTD' " + "\n");
            strSqlString.Append("       ) DAT" + "\n");
            strSqlString.Append("     , MRASRESDEF RES" + "\n");
            strSqlString.Append("     , CRASUPHDEF UPH" + "\n");
            strSqlString.Append(" WHERE 1=1  " + "\n");
            strSqlString.Append("   AND DAT.RES_ID = RES.RES_ID " + "\n");
            strSqlString.Append("   AND RES.FACTORY = UPH.FACTORY(+) " + "\n");
            strSqlString.Append("   AND RES.RES_STS_8 = UPH.OPER(+) " + "\n");
            strSqlString.Append("   AND RES.RES_GRP_6 = UPH.RES_MODEL(+) " + "\n");
            strSqlString.Append("   AND RES.RES_GRP_7 = UPH.UPEH_GRP(+) " + "\n");
            strSqlString.Append("   AND RES.RES_STS_2 = UPH.MAT_ID(+) " + "\n");
            strSqlString.Append("   AND RES.FACTORY = '" + cdvFactory.Text + "' " + "\n");
            strSqlString.Append("   AND RES.RES_TYPE IN ('EQUIPMENT', 'LINE')" + "\n");
            // 2017-05-29-������ : USE STATE�� "N"�� ���� ��ȸ�ϱ� ���Ͽ� �߰���.
            strSqlString.Append("   AND RES.RES_CMF_9 = '" + (chkUseStateN.Checked == true ? "N" : "Y") + "'\n");
            strSqlString.Append("   AND RES.DELETE_FLAG = ' '" + "\n");
            strSqlString.Append("   AND RES.RES_ID NOT LIKE '%$$$%'" + "\n");

            #region " RAS �� ��ȸ "
            if (udcRASCondition1.Text != "ALL" && udcRASCondition1.Text != "")
                strSqlString.AppendFormat("   AND RES.RES_GRP_1 {0} " + "\n", udcRASCondition1.SelectedValueToQueryString);

            if (udcRASCondition2.Text != "ALL" && udcRASCondition2.Text != "")
                strSqlString.AppendFormat("   AND RES.RES_GRP_2 {0} " + "\n", udcRASCondition2.SelectedValueToQueryString);

            if (udcRASCondition3.Text != "ALL" && udcRASCondition3.Text != "")
                strSqlString.AppendFormat("   AND RES.RES_GRP_3 {0} " + "\n", udcRASCondition3.SelectedValueToQueryString);

            if (udcRASCondition4.Text != "ALL" && udcRASCondition4.Text != "")
                strSqlString.AppendFormat("   AND RES.RES_GRP_5 {0} " + "\n", udcRASCondition4.SelectedValueToQueryString);

            if (udcRASCondition5.Text != "ALL" && udcRASCondition5.Text != "")
                strSqlString.AppendFormat("   AND RES.RES_GRP_6 {0} " + "\n", udcRASCondition5.SelectedValueToQueryString);

            if (udcRASCondition6.Text != "ALL" && udcRASCondition6.Text != "")
                strSqlString.AppendFormat("   AND RES.RES_ID {0} " + "\n", udcRASCondition6.SelectedValueToQueryString);

            if (udcRASCondition7.Text != "ALL" && udcRASCondition7.Text != "")
                strSqlString.AppendFormat("   AND RES.RES_ID IN ( SELECT UNIQUE RES_ID FROM CRASRESUSR WHERE USER_DESC {0} ) " + "\n", udcRASCondition7.SelectedValueToQueryString);
            #endregion

            strSqlString.AppendFormat(" GROUP BY {0} " + "\n", QueryCond2);
            strSqlString.AppendFormat(" ORDER BY {0} " + "\n", QueryCond3);

            //strSqlString.Append("           AND MAT.MAT_ID LIKE '" + txtProduct.Text + "'" + "\n");

            //#region �� ��ȸ�� ���� SQL�� ����

            //if (udcBUMPCondition1.Text != "ALL" && udcBUMPCondition1.Text != "")
            //    strSqlString.AppendFormat("           AND MAT.MAT_GRP_1 {0} " + "\n", udcBUMPCondition1.SelectedValueToQueryString);

            //if (udcBUMPCondition2.Text != "ALL" && udcBUMPCondition2.Text != "")
            //    strSqlString.AppendFormat("           AND MAT.MAT_GRP_2 {0} " + "\n", udcBUMPCondition2.SelectedValueToQueryString);

            //if (udcBUMPCondition3.Text != "ALL" && udcBUMPCondition3.Text != "")
            //    strSqlString.AppendFormat("           AND MAT.MAT_GRP_3 {0} " + "\n", udcBUMPCondition3.SelectedValueToQueryString);

            //if (udcBUMPCondition4.Text != "ALL" && udcBUMPCondition4.Text != "")
            //    strSqlString.AppendFormat("           AND MAT.MAT_GRP_4 {0} " + "\n", udcBUMPCondition4.SelectedValueToQueryString);

            //if (udcBUMPCondition5.Text != "ALL" && udcBUMPCondition5.Text != "")
            //    strSqlString.AppendFormat("           AND MAT.MAT_GRP_5 {0} " + "\n", udcBUMPCondition5.SelectedValueToQueryString);

            //if (udcBUMPCondition6.Text != "ALL" && udcBUMPCondition6.Text != "")
            //    strSqlString.AppendFormat("           AND MAT.MAT_GRP_6 {0} " + "\n", udcBUMPCondition6.SelectedValueToQueryString);

            //if (udcBUMPCondition7.Text != "ALL" && udcBUMPCondition7.Text != "")
            //    strSqlString.AppendFormat("           AND MAT.MAT_GRP_7 {0} " + "\n", udcBUMPCondition7.SelectedValueToQueryString);

            //if (udcBUMPCondition8.Text != "ALL" && udcBUMPCondition8.Text != "")
            //    strSqlString.AppendFormat("           AND MAT.MAT_GRP_8 {0} " + "\n", udcBUMPCondition8.SelectedValueToQueryString);

            //if (udcBUMPCondition9.Text != "ALL" && udcBUMPCondition9.Text != "")
            //    strSqlString.AppendFormat("           AND MAT.MAT_CMF_14 {0} " + "\n", udcBUMPCondition9.SelectedValueToQueryString);

            //if (udcBUMPCondition10.Text != "ALL" && udcBUMPCondition10.Text != "")
            //    strSqlString.AppendFormat("           AND MAT.MAT_CMF_2 {0} " + "\n", udcBUMPCondition10.SelectedValueToQueryString);

            //if (udcBUMPCondition11.Text != "ALL" && udcBUMPCondition11.Text != "")
            //    strSqlString.AppendFormat("           AND MAT.MAT_CMF_3 {0} " + "\n", udcBUMPCondition11.SelectedValueToQueryString);

            //if (udcBUMPCondition12.Text != "ALL" && udcBUMPCondition12.Text != "")
            //    strSqlString.AppendFormat("           AND MAT.MAT_CMF_4 {0} " + "\n", udcBUMPCondition12.SelectedValueToQueryString);
         
            //#endregion
                        
            
            #endregion
            

            if (GlobalVariable.gsUserGroup == "ADMIN_GROUP" || GlobalVariable.gsUserGroup == "HANA_ADMIN_GROUP")
            {
                System.Windows.Forms.Clipboard.SetText(strSqlString.ToString());
            }

            return strSqlString.ToString();
        }

        #endregion


        #region EVENT ó��
        /// <summary>
        /// 6. View ��ư Action
        /// </summary>        ///         
        private void btnView_Click(object sender, EventArgs e)
        {
            DataTable dt = null;

            if (CheckField() == false) return;

            GridColumnInit();
            spdData_Sheet1.RowCount = 0;

            try
            {
                LoadingPopUp.LoadIngPopUpShow(this);
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

                //int[] rowType = spdData.RPT_DataBindingWithSubTotal(dt, 0, sub+1, 14, null, null, btnSort);
                //����Ÿ���̺�, ��Ż ������ ���ѹ�, ���� ���� ������Ż � ������, ù������ ���° ������ TOTAL �������

                //Total�κ� ������
                //spdData.RPT_FillDataSelectiveCells("Total", 0, 14, 0, 1, true, Align.Center, VerticalAlign.Center);

                //spdData.RPT_AutoFit(false);                

                spdData.DataSource = dt;

                for (int i = 0; i <= 5; i++)
                {
                    spdData.ActiveSheet.Columns[i].BackColor = Color.LemonChiffon;
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
        /// Excel Export
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnExcelExport_Click(object sender, EventArgs e)
        {
            if (spdData.ActiveSheet.Rows.Count > 0)
            {
                //ExcelHelper.Instance.subMakeExcel(spdData, this.lblTitle.Text, null, null, true);
                spdData.ExportExcel();            
            }
        }

        /// <summary>
        /// Factory ����
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cdvFactory_ValueSelectedItemChanged(object sender, Miracom.UI.MCCodeViewSelChanged_EventArgs e)
        {
            this.SetFactory(cdvFactory.txtValue);
        }

        #endregion

		private void spdData_EnterCell(object sender, FarPoint.Win.Spread.EnterCellEventArgs e)
		{
			/****************************************************
			 * comment : spread sheet�� cell�� ����Ǹ�
			 * 
			 * created by : bee-jae jung(2016-09-22-�����)
			 * 
			 * modified by : bee-jae jung(2016-09-20-�����)
			 ****************************************************/
			try
			{
				System.Windows.Forms.Cursor.Current = Cursors.WaitCursor;

				ShowChart(e.Row);
			}
			catch (Exception ex)
			{
				CmnFunction.ShowMsgBox(ex.Message);
			}
			finally
			{
				System.Windows.Forms.Cursor.Current = Cursors.Default;
			}
		}

		private void ShowChart(int irow)
        {
            int chcnt = 7;  // �� ��° �÷����� ������ ǥ���ϴ���
            int iColumnsCount = cdvFromToDate.SubtractBetweenFromToDate + 1;            

            // ��Ʈ ����
            udcMSChart1.RPT_2_ClearData();
            udcMSChart1.RPT_3_OpenData(2, iColumnsCount);
            int[] columnsUpeh = new Int32[iColumnsCount];
            int[] columns = new Int32[iColumnsCount];
            int[] columnsHeader = new Int32[iColumnsCount];

            // íƮ 1 - UPEH��հ�.. �÷� �Ѱ��� ���̱� ������ �÷� �迭�� ������ ���� �ִ´�.
            for (int i = 0; i < columns.Length; i++)
            {
                columnsUpeh[i] = 6;                
            }

            // íƮ 2 - ������
            for (int i = 0; i < columns.Length; i++)
            {
                columns[i] = chcnt;
                columnsHeader[i] = chcnt;
                chcnt++;
            }

            double max1 = udcMSChart1.RPT_4_AddData(spdData, new int[] { irow }, columnsUpeh, SeriseType.Rows);
            double max2 = udcMSChart1.RPT_4_AddData(spdData, new int[] { irow }, columns, SeriseType.Rows);

            if (max1 < max2)
            {
                max1 = max2;
            }

            udcMSChart1.RPT_5_CloseData();

            udcMSChart1.RPT_6_SetGallery(SeriesChartType.Line, 0, 1, "", AsixType.Y, DataTypes.Initeger, max1 * 1.1);
            udcMSChart1.RPT_6_SetGallery(SeriesChartType.Bar, 1, 1, "", AsixType.Y, DataTypes.Initeger, max1 * 1.1);

            udcMSChart1.RPT_7_SetXAsixTitleUsingSpreadHeader(spdData, 0, columnsHeader);            
            udcMSChart1.RPT_8_SetSeriseLegend(new string[] { "CAPA (UPEH average)", "OUT" }, System.Windows.Forms.DataVisualization.Charting.Docking.Top);

            udcMSChart1.Legends[0].Alignment = StringAlignment.Center;
            
            //udcMSChart1.PointLabels = true;
            //udcMSChart1.AxisY.Gridlines = false;
            //udcMSChart1.AxisY.DataFormat.Decimals = 0;

            string[] strDate = cdvFromToDate.getSelectDate();
            // x�� Label ǥ�� �κ�
            for (int i = 0; i <= cdvFromToDate.SubtractBetweenFromToDate; i++)
            {                
                udcMSChart1.Series[0].Points[i].AxisLabel = strDate[i];                
            }
                        
            //udcMSChart1.SerLegBox = true;
            //udcMSChart1.SerLegBoxObj.Docked = SoftwareFX.ChartFX.Docked.Top;
            //udcMSChart1.SerLeg[0] = sVender;
        }


    }
}
