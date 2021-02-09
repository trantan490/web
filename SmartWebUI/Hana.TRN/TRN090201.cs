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

namespace Hana.TRN
{
    public partial class TRN090201 : Miracom.SmartWeb.UI.Controls.udcCUSReport002
    {

        #region " TRN010102 : Program Initial "

        public TRN090201()
        {
            InitializeComponent();
            this.SetFactory("HMKA1, HMKT1");
            cdvFactory.Text = GlobalVariable.gsAssyDefaultFactory;
            //cdvBaseDate.Value = DateTime.Today;
            cdvBaseDate.Value = DateTime.Now.AddDays(-1);
            fnSSInitial(SS01);
            fnSSSortInit();
        }

        DataTable dt = null;

        #endregion


        #region " Common Function "

        private void fnSSInitial(Miracom.SmartWeb.UI.Controls.udcFarPoint SS)
        {
            /****************************************************
             * comment : ss의 header를 설정한다.
             * 
             * created by : bee-jae jung(2010-07-23-금요일)
             * 
             * modified by : bee-jae jung(2010-07-23-금요일)
             ****************************************************/
            string qry = "";
            int iindex = 0;
            Formatter nf;
            try
            {
                Cursor.Current = Cursors.WaitCursor;

                SS.RPT_ColumnInit();
                // CUSTOMER
                if (udcWIPCondition1.Text != "ALL" && udcWIPCondition1.Text != "")
                {
                    iindex = 0;
                    SS.RPT_AddBasicColumn("CUSTOMER", 0, iindex, Visibles.True, Frozen.False, Align.Center, Merge.True, Formatter.String, 40);
                }
                // FAMILY
                if (udcWIPCondition2.Text != "ALL" && udcWIPCondition2.Text != "")
                {
                    iindex++;
                    SS.RPT_AddBasicColumn("FAMILY", 0, iindex, Visibles.True, Frozen.False, Align.Center, Merge.True, Formatter.String, 40);
                }
                // PACKAGE
                if (udcWIPCondition3.Text != "ALL" && udcWIPCondition3.Text != "")
                {
                    iindex++;
                    SS.RPT_AddBasicColumn("PACKAGE", 0, iindex, Visibles.True, Frozen.False, Align.Center, Merge.True, Formatter.String, 40);
                }
                // TYPE1
                if (udcWIPCondition4.Text != "ALL" && udcWIPCondition4.Text != "")
                { 
                    iindex++;
                    SS.RPT_AddBasicColumn("TYPE1", 0, iindex, Visibles.True, Frozen.False, Align.Center, Merge.True, Formatter.String, 40);
                }
                // TYPE2
                if (udcWIPCondition5.Text != "ALL" && udcWIPCondition5.Text != "")
                {
                    iindex++;
                    SS.RPT_AddBasicColumn("TYPE2", 0, iindex, Visibles.True, Frozen.False, Align.Center, Merge.True, Formatter.String, 40);
                }
                // LEAD COUNT
                if (udcWIPCondition6.Text != "ALL" && udcWIPCondition6.Text != "")
                {
                    iindex++;
                    SS.RPT_AddBasicColumn("LEAD_COUNT", 0, iindex, Visibles.True, Frozen.False, Align.Center, Merge.True, Formatter.String, 40);
                }
                // DENSITY
                if (udcWIPCondition7.Text != "ALL" && udcWIPCondition7.Text != "")
                {
                    iindex++;
                    SS.RPT_AddBasicColumn("DENSITY", 0, iindex, Visibles.True, Frozen.False, Align.Center, Merge.True, Formatter.String, 40);
                }
                // GENERATION
                if (udcWIPCondition8.Text != "ALL" && udcWIPCondition8.Text != "")
                {
                    iindex++;
                    SS.RPT_AddBasicColumn("GENERATION", 0, iindex, Visibles.True, Frozen.False, Align.Center, Merge.True, Formatter.String, 40);
                }

                // 2010-07-23-정비재 : 검색구분에 따라서 숫자 format을 설정한다.
                if (rb01.Checked == true)
                {
                    nf = Formatter.Double3;
                }
                else
                {
                    nf = Formatter.Number;
                }

                // 2010-07-20-정비재 : sheet의 headeer를 설정한다.
                SS.RPT_AddBasicColumn("Classification", 0, iindex++, Visibles.True, Frozen.False, Align.Center, Merge.True, Formatter.String, 100);
                SS.RPT_AddBasicColumn("Period average", 0, iindex++, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Double3, 100);
                // 2010-07-23-정비재 : 기준일자(basedate) 기준으로 일자를 설정한다.
                qry = "SELECT SUBSTR(TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 7, 'YYYYMMDD'), -4) AS DD_7\n"
                    + "     , SUBSTR(TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 6, 'YYYYMMDD'), -4) AS DD_6\n"
                    + "     , SUBSTR(TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 5, 'YYYYMMDD'), -4) AS DD_5\n"
                    + "     , SUBSTR(TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 4, 'YYYYMMDD'), -4) AS DD_4\n"
                    + "     , SUBSTR(TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 3, 'YYYYMMDD'), -4) AS DD_3\n"
                    + "     , SUBSTR(TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 2, 'YYYYMMDD'), -4) AS DD_2\n"
                    + "     , SUBSTR(TO_CHAR(TO_DATE('" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', 'YYYYMMDD') - 1, 'YYYYMMDD'), -4) AS DD_1\n"
                    + "  FROM DUAL";

                if (GlobalVariable.gsUserGroup == "ADMIN_GROUP" || GlobalVariable.gsUserGroup == "HANA_ADMIN_GROUP")
                {
                    Clipboard.SetText(qry.Replace((char)Keys.Tab, ' '));
                }

                dt = CmnFunction.oComm.GetFuncDataTable("DYNAMIC", qry.Replace((char)Keys.Tab, ' '));
                if (dt.Rows.Count <= 0)
                {
                    SS.RPT_AddBasicColumn("DD-7", 0, iindex++, Visibles.True, Frozen.False, Align.Right, Merge.False, nf, 100);
                    SS.RPT_AddBasicColumn("DD-6", 0, iindex++, Visibles.True, Frozen.False, Align.Right, Merge.False, nf, 100);
                    SS.RPT_AddBasicColumn("DD-5", 0, iindex++, Visibles.True, Frozen.False, Align.Right, Merge.False, nf, 100);
                    SS.RPT_AddBasicColumn("DD-4", 0, iindex++, Visibles.True, Frozen.False, Align.Right, Merge.False, nf, 100);
                    SS.RPT_AddBasicColumn("DD-3", 0, iindex++, Visibles.True, Frozen.False, Align.Right, Merge.False, nf, 100);
                    SS.RPT_AddBasicColumn("DD-2", 0, iindex++, Visibles.True, Frozen.False, Align.Right, Merge.False, nf, 100);
                    SS.RPT_AddBasicColumn("DD-1", 0, iindex++, Visibles.True, Frozen.False, Align.Right, Merge.False, nf, 100);
                }
                else
                {
                    SS.RPT_AddBasicColumn(dt.Rows[0][0].ToString(), 0, iindex++, Visibles.True, Frozen.False, Align.Right, Merge.False, nf, 100);
                    SS.RPT_AddBasicColumn(dt.Rows[0][1].ToString(), 0, iindex++, Visibles.True, Frozen.False, Align.Right, Merge.False, nf, 100);
                    SS.RPT_AddBasicColumn(dt.Rows[0][2].ToString(), 0, iindex++, Visibles.True, Frozen.False, Align.Right, Merge.False, nf, 100);
                    SS.RPT_AddBasicColumn(dt.Rows[0][3].ToString(), 0, iindex++, Visibles.True, Frozen.False, Align.Right, Merge.False, nf, 100);
                    SS.RPT_AddBasicColumn(dt.Rows[0][4].ToString(), 0, iindex++, Visibles.True, Frozen.False, Align.Right, Merge.False, nf, 100);
                    SS.RPT_AddBasicColumn(dt.Rows[0][5].ToString(), 0, iindex++, Visibles.True, Frozen.False, Align.Right, Merge.False, nf, 100);
                    SS.RPT_AddBasicColumn(dt.Rows[0][6].ToString(), 0, iindex++, Visibles.True, Frozen.False, Align.Right, Merge.False, nf, 100);
                }
                dt.Dispose();
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
             * created by : bee-jae jung(2010-07-23-금요일)
             * 
             * modified by : bee-jae jung(2010-07-23-금요일)
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
             * created by : bee-jae jung(2010-07-23-금요일)
             * 
             * modified by : bee-jae jung(2010-07-23-금요일)
             ****************************************************/
            string qry = "";
            string oper = "";
            try
            {
                Cursor.Current = Cursors.WaitCursor;
                LoadingPopUp.LoadIngPopUpShow(this);

                // 2010-07-23-정비재 : Sheet / Listview를 초기화 한다.
                CmnInitFunction.ClearList(SS01, true);

                if (rb01.Checked == true)
                {

                    if (cdvres_grp_3.Text.Trim() == "")
                    {
                        oper = "ALL";
                    }
                    else
                    {
                        oper = cdvres_grp_3.Text.Trim();
                    }
                    
                    // 2010-08-03-김민우 : Summary Table에서 설비 가동율을 조회한다. 
                    qry = "SELECT FLAG, MONTH_AVERAGE, DD_7, DD_6, DD_5, DD_4, DD_3, DD_2, DD_1 \n"
                       + "   FROM RSUMTRNOPR \n"
                       + "  WHERE FACTORY = '" + cdvFactory.Text + "' \n"
                       + "    AND WORK_DATE = '" + cdvBaseDate.Value.ToString("yyyyMMdd") + "' \n"
                       + "    AND OPER =  '" + oper + "' \n"
                       + " ORDER BY DECODE(FLAG, '종합가동율', 0, '성능가동율', 1, '시간가동율', 2, 99) ASC";
                }
                else if (rb02.Checked == true)
                {

                    if (cdvres_grp_3.Text.Trim() == "")
                    {
                        oper = "ALL";
                    }
                    else
                    {
                        oper = cdvres_grp_3.Text.Trim();
                    }
                    // 2010-08-03-김민우 : Summary Table에서 설비 가동율을 조회한다. 
                    qry = "SELECT MAINT_CODE, MONTH_AVERAGE, DD_7, DD_6, DD_5, DD_4, DD_3, DD_2, DD_1 \n"
                       + "   FROM RSUMTRNDWH \n"
                       + "  WHERE FACTORY = '" + cdvFactory.Text + "' \n"
                       + "    AND WORK_DATE = '" + cdvBaseDate.Value.ToString("yyyyMMdd") + "' \n"
                       + "    AND OPER =  '" + oper + "' \n"
                       + " ORDER BY MAINT_CODE ASC";
                }
                                
                if (GlobalVariable.gsUserGroup == "ADMIN_GROUP" || GlobalVariable.gsUserGroup == "HANA_ADMIN_GROUP")
                {
                    System.Windows.Forms.Clipboard.SetText(qry.Replace((char)Keys.Tab, ' '));
                }

                dt = CmnFunction.oComm.GetFuncDataTable("DYNAMIC", qry.Replace((char)Keys.Tab, ' '));

                if (dt.Rows.Count == 0)
                {
                    dt.Dispose();
                    LoadingPopUp.LoadingPopUpHidden();
                    CmnFunction.ShowMsgBox(RptMessages.GetMessage("STD010", GlobalVariable.gcLanguage));
                    return false;
                }
              
                SS01.DataSource = dt;

                fnMakeChart(SS01, 0, 2);

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

        private void fnMakeChart(Miracom.SmartWeb.UI.Controls.udcFarPoint SS, int iselrow, int iselcol)
        {
            /****************************************************
             * Comment : SS의 데이터를 Chart에 표시한다.
             * 
             * Created By : bee-jae jung(2010-07-23-금요일)
             * 
             * Modified By : bee-jae jung(2010-07-23-금요일)
             ****************************************************/
            int[] ich_dd; int[] icols_dd; int[] irows_dd; string[] stitle_dd;
            int icol = 0, irow = 0;
            //int  idecimalposition = 0;
            //string sxtitle = "", sytitle = "";
            try
            {
                Cursor.Current = Cursors.WaitCursor;

                // 2010-07-23-정비재 : SS에 데이터가 없으면 종료한다.
                if (SS.Sheets[0].RowCount <= 0)
                {
                    return;
                }

                // 2010-07-23-정비재 : 검색조건에 따라서 Chart에 Display하는 숫자의 Format을 달리한다.
                //if (rb01.Checked == true)
                //{
                //    idecimalposition = 3;
                //    sxtitle = "가동율(%)";
                //    sytitle = "설비 가동율";
                //}
                //else
                //{
                //    idecimalposition = 0;
                //    sxtitle = "Down Time(초)";
                //    sytitle = "설비 Down Time 현황";
                //}

                // 2010-07-23-정비재 : 검색된 데이터 건 수 만큼 배열을 생성한다.
                ich_dd = new int[SS.Sheets[0].ColumnCount - 2];
                icols_dd = new int[SS.Sheets[0].ColumnCount - 2];
                irows_dd = new int[SS.Sheets[0].RowCount];
                stitle_dd = new string[SS.Sheets[0].RowCount];

                for (icol = 0; icol < ich_dd.Length; icol++)
                {
                    ich_dd[icol] = iselcol + icol;
                    icols_dd[icol] = iselcol + icol;
                }
                for (irow = 0; irow < SS.Sheets[0].RowCount; irow++)
                {
                    irows_dd[irow] = iselrow + irow;
                    stitle_dd[irow] = SS.Sheets[0].Cells[iselrow + irow, 0].Text;
                }

                // 2010-07-23-정비재 : Display할 Chart를 초기화 한다.
                udcMSChart1.RPT_1_ChartInit();
                udcMSChart1.RPT_2_ClearData();
                udcMSChart1.RPT_3_OpenData(irows_dd.Length, icols_dd.Length);
                udcMSChart1.RPT_4_AddData(SS, irows_dd, icols_dd, SeriseType.Rows);
                udcMSChart1.RPT_5_CloseData();
                udcMSChart1.RPT_7_SetXAsixTitleUsingSpreadHeader(SS, 0, stitle_dd.Length);
                udcMSChart1.RPT_8_SetSeriseLegend(stitle_dd, System.Windows.Forms.DataVisualization.Charting.Docking.Top);
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
