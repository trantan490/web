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

using System.Data.OleDb;


namespace Hana.PQC
{
    public partial class YLD040702 : Miracom.SmartWeb.UI.Controls.udcCUSReport002
    {

        #region " YLD040702 : Program Initial "
        /// 클  래  스: YLD040702<br/>
        /// 클래스요약: DC TREND <br/>
        /// 작  성  자: 김민우<br/>
        /// 최초작성일: 2010-10-04<br/>
        /// 상세  설명: DC TREND(Scatter)<br/>
        /// 변경  내용: <br/> 
        /// 
        public YLD040702()
        {
            InitializeComponent();
            fnSSInitial(SS01);
            fnSSSortInit();
            this.SetFactory(GlobalVariable.gsAssyDefaultFactory);
            cdvBaseDate.Value = DateTime.Today;
            cdvFactory.Text = GlobalVariable.gsAssyDefaultFactory;

        }

        #endregion


        #region " Common Function "

        private void fnSSInitial(Miracom.SmartWeb.UI.Controls.udcFarPoint SS)
        {
            /****************************************************
             * Comment : SS의 Header를 설정한다.
             * 
             * Created By : KIM-MINWOO(2010-10-04-월요일)
             * 
             * Modified By :KIM-MINWOO(2010-10-04-월요일)
             ****************************************************/

            try
            {
                Cursor.Current = Cursors.WaitCursor;

                SS.RPT_ColumnInit();
                // CUSTOMER

                SS.RPT_AddBasicColumn("CUSTOMER", 0, 0, Visibles.True, Frozen.False, Align.Center, Merge.True, Formatter.String, 80);
                SS.RPT_AddBasicColumn("MM-2", 0, 1, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Double2, 80);
                SS.RPT_AddBasicColumn("MM-1", 0, 2, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Double2, 80);
                SS.RPT_AddBasicColumn("MM", 0, 3, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Double2, 80);
                SS.RPT_AddBasicColumn("WW-3", 0, 4, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Double2, 80);
                SS.RPT_AddBasicColumn("WW-2", 0, 5, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Double2, 80);
                SS.RPT_AddBasicColumn("WW-1", 0, 6, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Double2, 80);
                SS.RPT_AddBasicColumn("WW", 0, 7, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Double2, 80);
                SS.RPT_AddBasicColumn("DD-6", 0, 8, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Double2, 80);
                SS.RPT_AddBasicColumn("DD-5", 0, 9, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Double2, 80);
                SS.RPT_AddBasicColumn("DD-4", 0, 10, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Double2, 80);
                SS.RPT_AddBasicColumn("DD-3", 0, 11, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Double2, 80);
                SS.RPT_AddBasicColumn("DD-2", 0, 12, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Double2, 80);
                SS.RPT_AddBasicColumn("DD-1", 0, 13, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Double2, 80);
                SS.RPT_AddBasicColumn("DD", 0, 14, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Double2, 80);


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
             * Comment : SS의 데이터의 정렬규칙을 설정하다.
             * 
             * Created By : KIM-MINWOO(2010-10-04-월요일)
             * 
             * Modified By :KIM-MINWOO(2010-10-04-월요일)
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
                ((udcTableForm)(this.btnSort.BindingForm)).AddRow("PRODUCT", "A.MAT_ID", "MAT_ID", "A.MAT_ID", true);
                ((udcTableForm)(this.btnSort.BindingForm)).AddRow("RETURN_TYPE", "A.RETURN_TYPE", "RETURN_TYPE", "A.RETURN_TYPE", true);
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
             * Comment : DataBase에 저장된 데이터를 조회한다.
             * 
              * Created By : KIM-MINWOO(2010-10-04-월요일)
             * 
             * Modified By :KIM-MINWOO(2010-10-04-월요일)
             ****************************************************/
            DataTable dt = null;
            Miracom.SmartWeb.UI.Controls.udcFarPoint SS = null;
            string QRY = "";
            try
            {
                Cursor.Current = Cursors.WaitCursor;
                LoadingPopUp.LoadIngPopUpShow(this);

                CmnInitFunction.ClearList(SS01, true);

                // 검색 날짜의 주차 가져오기
                DataTable dt1 = null;
                dt1 = null;
                dt1 = CmnFunction.oComm.GetFuncDataTable("DYNAMIC", MakeSqlString2(cdvBaseDate.Value.ToString("yyyyMMdd")));
                string week = dt1.Rows[0][0].ToString();
                // 검색 날짜의 주차 가져오기(-1)
                dt1 = null;
                dt1 = CmnFunction.oComm.GetFuncDataTable("DYNAMIC", MakeSqlString2(cdvBaseDate.Value.AddDays(-7).ToString("yyyyMMdd")));
                string week_1 = dt1.Rows[0][0].ToString();
                // 검색 날짜의 주차 가져오기(-2)
                dt1 = null;
                dt1 = CmnFunction.oComm.GetFuncDataTable("DYNAMIC", MakeSqlString2(cdvBaseDate.Value.AddDays(-14).ToString("yyyyMMdd")));
                string week_2 = dt1.Rows[0][0].ToString();
                // 검색 날짜의 주차 가져오기(-3)
                dt1 = null;
                dt1 = CmnFunction.oComm.GetFuncDataTable("DYNAMIC", MakeSqlString2(cdvBaseDate.Value.AddDays(-21).ToString("yyyyMMdd")));
                string week_3 = dt1.Rows[0][0].ToString();


                QRY = "SELECT  CUSTOMER \n"

                    + "		 , NVL(ROUND(DC_OUT_QTY_MM_2/DECODE(DC_IN_QTY_MM_2,0,1,DC_IN_QTY_MM_2) * 100,2),0) AS MM_2 \n"
                    + "		 , NVL(ROUND(DC_OUT_QTY_MM_1/DECODE(DC_IN_QTY_MM_1,0,1,DC_IN_QTY_MM_1) * 100,2),0) AS MM_1 \n"
                    + "		 , NVL(ROUND(DC_OUT_QTY_MM/DECODE(DC_IN_QTY_MM,0,1,DC_IN_QTY_MM) * 100,2),0) AS MM \n"
                    + "		 , ROUND(DC_OUT_QTY_WW_3/DECODE(DC_IN_QTY_WW_3,0,1,DC_IN_QTY_WW_3) * 100,2) AS WW_3 \n"
                    + "		 , ROUND(DC_OUT_QTY_WW_2/DECODE(DC_IN_QTY_WW_2,0,1,DC_IN_QTY_WW_2) * 100,2) AS WW_2 \n"
                    + "		 , ROUND(DC_OUT_QTY_WW_1/DECODE(DC_IN_QTY_WW_1,0,1,DC_IN_QTY_WW_1) * 100,2) AS WW_1 \n"
                    + "		 , ROUND(DC_OUT_QTY_WW/DECODE(DC_IN_QTY_WW,0,1,DC_IN_QTY_WW) * 100,2) AS WW \n"
                    + "		 , ROUND(DC_OUT_QTY_DD_6/DECODE(DC_IN_QTY_DD_6,0,1,DC_IN_QTY_DD_6) * 100,2) AS DD_6 \n"
                    + "		 , ROUND(DC_OUT_QTY_DD_5/DECODE(DC_IN_QTY_DD_5,0,1,DC_IN_QTY_DD_5) * 100,2) AS DD_5 \n"
                    + "		 , ROUND(DC_OUT_QTY_DD_4/DECODE(DC_IN_QTY_DD_4,0,1,DC_IN_QTY_DD_4) * 100,2) AS DD_4 \n"
                    + "		 , ROUND(DC_OUT_QTY_DD_3/DECODE(DC_IN_QTY_DD_3,0,1,DC_IN_QTY_DD_3) * 100,2) AS DD_3 \n"
                    + "		 , ROUND(DC_OUT_QTY_DD_2/DECODE(DC_IN_QTY_DD_2,0,1,DC_IN_QTY_DD_2) * 100,2) AS DD_2 \n"
                    + "		 , ROUND(DC_OUT_QTY_DD_1/DECODE(DC_IN_QTY_DD_1,0,1,DC_IN_QTY_DD_1) * 100,2) AS DD_1 \n"
                    + "		 , ROUND(DC_OUT_QTY_DD/DECODE(DC_IN_QTY_DD,0,1,DC_IN_QTY_DD) * 100,2) AS DD \n"
                    + "		   FROM( \n"




                    + "SELECT  CUSTOMER AS CUSTOMER \n"
                    + "		 , SUM(DECODE(WORK_MONTH,'" + cdvBaseDate.Value.AddMonths(-2).ToString("yyyyMM") + "', DC_IN_QTY,0)) AS DC_IN_QTY_MM_2 \n"
                    + "		 , SUM(DECODE(WORK_MONTH,'" + cdvBaseDate.Value.AddMonths(-1).ToString("yyyyMM") + "', DC_IN_QTY,0)) AS DC_IN_QTY_MM_1 \n"
                    + "		 , SUM(DECODE(WORK_MONTH,'" + cdvBaseDate.Value.ToString("yyyyMM") + "', DC_IN_QTY,0)) AS DC_IN_QTY_MM \n"
                    + "		 , SUM(DECODE(WORK_MONTH,'" + cdvBaseDate.Value.AddMonths(-2).ToString("yyyyMM") + "', DC_OUT_QTY,0)) AS DC_OUT_QTY_MM_2 \n"
                    + "		 , SUM(DECODE(WORK_MONTH,'" + cdvBaseDate.Value.AddMonths(-1).ToString("yyyyMM") + "', DC_OUT_QTY,0)) AS DC_OUT_QTY_MM_1 \n"
                    + "		 , SUM(DECODE(WORK_MONTH,'" + cdvBaseDate.Value.ToString("yyyyMM") + "', DC_OUT_QTY,0)) AS DC_OUT_QTY_MM \n"
                    + "		 , SUM(DECODE(PLAN_WEEK,'" + week_3 + "', DC_IN_QTY,0)) AS DC_IN_QTY_WW_3 \n"
                    + "		 , SUM(DECODE(PLAN_WEEK,'" + week_2 + "', DC_IN_QTY,0)) AS DC_IN_QTY_WW_2 \n"
                    + "		 , SUM(DECODE(PLAN_WEEK,'" + week_1 + "', DC_IN_QTY,0)) AS DC_IN_QTY_WW_1 \n"
                    + "		 , SUM(DECODE(PLAN_WEEK,'" + week + "', DC_IN_QTY,0)) AS DC_IN_QTY_WW \n"
                    + "		 , SUM(DECODE(PLAN_WEEK,'" + week_3 + "', DC_OUT_QTY,0)) AS DC_OUT_QTY_WW_3 \n"
                    + "		 , SUM(DECODE(PLAN_WEEK,'" + week_2 + "', DC_OUT_QTY,0)) AS DC_OUT_QTY_WW_2 \n"
                    + "		 , SUM(DECODE(PLAN_WEEK,'" + week_1 + "', DC_OUT_QTY,0)) AS DC_OUT_QTY_WW_1 \n"
                    + "		 , SUM(DECODE(PLAN_WEEK,'" + week + "', DC_OUT_QTY,0)) AS DC_OUT_QTY_WW \n"
                    + "		 , SUM(DECODE(WORK_DATE,'" + cdvBaseDate.Value.AddDays(-6).ToString("yyyyMMdd") + "', DC_IN_QTY,0)) AS DC_IN_QTY_DD_6 \n"
                    + "		 , SUM(DECODE(WORK_DATE,'" + cdvBaseDate.Value.AddDays(-5).ToString("yyyyMMdd") + "', DC_IN_QTY,0)) AS DC_IN_QTY_DD_5 \n"
                    + "		 , SUM(DECODE(WORK_DATE,'" + cdvBaseDate.Value.AddDays(-4).ToString("yyyyMMdd") + "', DC_IN_QTY,0)) AS DC_IN_QTY_DD_4 \n"
                    + "		 , SUM(DECODE(WORK_DATE,'" + cdvBaseDate.Value.AddDays(-3).ToString("yyyyMMdd") + "', DC_IN_QTY,0)) AS DC_IN_QTY_DD_3 \n"
                    + "		 , SUM(DECODE(WORK_DATE,'" + cdvBaseDate.Value.AddDays(-2).ToString("yyyyMMdd") + "', DC_IN_QTY,0)) AS DC_IN_QTY_DD_2 \n"
                    + "		 , SUM(DECODE(WORK_DATE,'" + cdvBaseDate.Value.AddDays(-1).ToString("yyyyMMdd") + "', DC_IN_QTY,0)) AS DC_IN_QTY_DD_1 \n"
                    + "		 , SUM(DECODE(WORK_DATE,'" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', DC_IN_QTY,0)) AS DC_IN_QTY_DD \n"
                    + "		 , SUM(DECODE(WORK_DATE,'" + cdvBaseDate.Value.AddDays(-6).ToString("yyyyMMdd") + "', DC_OUT_QTY,0)) AS DC_OUT_QTY_DD_6 \n"
                    + "		 , SUM(DECODE(WORK_DATE,'" + cdvBaseDate.Value.AddDays(-5).ToString("yyyyMMdd") + "', DC_OUT_QTY,0)) AS DC_OUT_QTY_DD_5 \n"
                    + "		 , SUM(DECODE(WORK_DATE,'" + cdvBaseDate.Value.AddDays(-4).ToString("yyyyMMdd") + "', DC_OUT_QTY,0)) AS DC_OUT_QTY_DD_4 \n"
                    + "		 , SUM(DECODE(WORK_DATE,'" + cdvBaseDate.Value.AddDays(-3).ToString("yyyyMMdd") + "', DC_OUT_QTY,0)) AS DC_OUT_QTY_DD_3 \n"
                    + "		 , SUM(DECODE(WORK_DATE,'" + cdvBaseDate.Value.AddDays(-2).ToString("yyyyMMdd") + "', DC_OUT_QTY,0)) AS DC_OUT_QTY_DD_2 \n"
                    + "		 , SUM(DECODE(WORK_DATE,'" + cdvBaseDate.Value.AddDays(-1).ToString("yyyyMMdd") + "', DC_OUT_QTY,0)) AS DC_OUT_QTY_DD_1 \n"
                    + "		 , SUM(DECODE(WORK_DATE,'" + cdvBaseDate.Value.ToString("yyyyMMdd") + "', DC_OUT_QTY,0)) AS DC_OUT_QTY_DD \n"

                    + "	  FROM ("
                    + "	        SELECT A.*, B.PLAN_WEEK FROM RSUMDCLHIS A, MWIPCALDEF B  \n"
                    + "	         WHERE A.WORK_DATE = B.SYS_DATE \n"
                    + "	           ANd B.CALENDAR_ID = 'SE' \n"
                    + "            AND CUSTOMER = '" + cdvCustomer.Text + "' \n"
                    + "            AND LOT_ID LIKE '" + txtLotID.Text + "' \n"
                    + "            AND MAT_ID LIKE '" + txtSearchProduct.Text + "' \n";
                
                #region 상세 조회에 따른 SQL문 생성
                if (udcWIPCondition1.Text != "ALL" && udcWIPCondition1.Text != "")
                    //QRY += "	           AND CUSTOMER " + udcWIPCondition1.SelectedValueToQueryString + "\n";

                if (udcWIPCondition2.Text != "ALL" && udcWIPCondition2.Text != "")
                    QRY += "	           AND FAMILY " + udcWIPCondition2.SelectedValueToQueryString + "\n";

                if (udcWIPCondition3.Text != "ALL" && udcWIPCondition3.Text != "")

                    QRY += "	           AND PKG " + udcWIPCondition3.SelectedValueToQueryString + "\n";

                if (udcWIPCondition4.Text != "ALL" && udcWIPCondition4.Text != "")

                    QRY += "	           AND TYPE1 " + udcWIPCondition4.SelectedValueToQueryString + "\n";

                if (udcWIPCondition5.Text != "ALL" && udcWIPCondition5.Text != "")

                    QRY += "	           AND TYPE2 " + udcWIPCondition5.SelectedValueToQueryString + "\n";

                if (udcWIPCondition6.Text != "ALL" && udcWIPCondition6.Text != "")

                    QRY += "	           AND LD_COUNT " + udcWIPCondition6.SelectedValueToQueryString + "\n";

                if (udcWIPCondition7.Text != "ALL" && udcWIPCondition7.Text != "")

                    QRY += "	           AND DENSITY " + udcWIPCondition7.SelectedValueToQueryString + "\n";

                if (udcWIPCondition8.Text != "ALL" && udcWIPCondition8.Text != "")

                    QRY += "	           AND GENERATION " + udcWIPCondition8.SelectedValueToQueryString + "\n";

                if (udcRASCondition6.Text != "ALL" && udcRASCondition6.Text != "")
                {
                    QRY += "	           AND (BG_RES_ID " + udcRASCondition6.SelectedValueToQueryString + "\n";
                    QRY += "	           OR   SAW_RES_ID " + udcRASCondition6.SelectedValueToQueryString + "\n";
                    QRY += "	           OR   DA1_RES_ID " + udcRASCondition6.SelectedValueToQueryString + "\n";
                    QRY += "	           OR   DA2_RES_ID " + udcRASCondition6.SelectedValueToQueryString + "\n";
                    QRY += "	           OR   DA3_RES_ID " + udcRASCondition6.SelectedValueToQueryString + "\n";
                    QRY += "	           OR   WB1_RES_ID " + udcRASCondition6.SelectedValueToQueryString + "\n";
                    QRY += "	           OR   WB2_RES_ID " + udcRASCondition6.SelectedValueToQueryString + "\n";
                    QRY += "	           OR   WB3_RES_ID " + udcRASCondition6.SelectedValueToQueryString + "\n";
                    QRY += "	           OR   MD_RES_ID " + udcRASCondition6.SelectedValueToQueryString + ")\n";

                }
                #endregion
                

                   QRY += "	  )GROUP BY CUSTOMER)";


                if (GlobalVariable.gsUserGroup == "ADMIN_GROUP" || GlobalVariable.gsUserGroup == "HANA_ADMIN_GROUP")
                {
                    System.Windows.Forms.Clipboard.SetText(QRY.Replace((char)Keys.Tab, ' '));
                }

                dt = CmnFunction.oComm.GetFuncDataTable("DYNAMIC", QRY.Replace((char)Keys.Tab, ' '));

                if (dt.Rows.Count == 0)
                {
                    dt.Dispose();
                    LoadingPopUp.LoadingPopUpHidden();
                    CmnFunction.ShowMsgBox(RptMessages.GetMessage("STD010", GlobalVariable.gcLanguage));
                    return false;
                }


                SS = SS01;

                SS.DataSource = dt;
                SS.RPT_AutoFit(false);

                fnMakeChart(SS, 0);

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

        // 검색 날짜의 주차 가져오기
        private string MakeSqlString2(string date)
        {
            StringBuilder sqlString = new StringBuilder();

            sqlString.Append("SELECT PLAN_WEEK " + "\n");
            sqlString.Append("  FROM MWIPCALDEF " + "\n");
            sqlString.Append(" WHERE 1=1" + "\n");
            sqlString.Append("   AND SYS_DATE='" + date + "'" + "\n");
            sqlString.Append("   AND CALENDAR_ID='SE'" + "\n");
            return sqlString.ToString();
        }

        private void fnMakeChart(Miracom.SmartWeb.UI.Controls.udcFarPoint SS, int iselrow)
        {
            /****************************************************
             * Comment : SS의 데이터를 Chart에 표시한다.
             * 
             * Created By : KIM-MINWOO(2010-10-04-월요일)
             * 
             * Modified By :KIM-MINWOO(2010-10-04-월요일)
             ****************************************************/
            int[] ich_mm = new int[3]; int[] icols_mm = new int[3]; int[] irows_mm = new int[1]; string[] stitle_mm = new string[1];
            int[] ich_ww = new int[4]; int[] icols_ww = new int[4]; int[] irows_ww = new int[1]; string[] stitle_ww = new string[1];
            int[] ich_dd = new int[7]; int[] icols_dd = new int[7]; int[] irows_dd = new int[1]; string[] stitle_dd = new string[1];
            int[] ich_md = new int[31]; int[] icols_md = new int[31]; int[] irows_md = new int[5]; string[] stitle_md = new string[5];
            int icol = 0, irow = 0;
            try
            {
                Cursor.Current = Cursors.WaitCursor;

                if (SS.Sheets[0].RowCount <= 0)
                {
                    return;
                }

                cf01.RPT_1_ChartInit(); cf02.RPT_1_ChartInit(); cf03.RPT_1_ChartInit();
                cf01.RPT_2_ClearData(); cf02.RPT_2_ClearData(); cf03.RPT_2_ClearData();

                cf01.AxisX.Title.Text = "Monthly Status";
                cf01.AxisY.Title.Text = "DC YIELD";
                cf01.Gallery = SoftwareFX.ChartFX.Gallery.Lines;
                cf01.AxisY.DataFormat.Format = 0;
                cf01.AxisY.DataFormat.Decimals = 2;
                cf01.AxisX.Staggered = false;
                cf01.PointLabels = true;

                for (icol = 0; icol < ich_mm.Length; icol++)
                {
                    ich_mm[icol] = icol + 1;
                    icols_mm[icol] = icol + 1;
                }
                for (irow = 0; irow < SS.Sheets[0].RowCount; irow++)
                {
                    irows_mm[irow] = iselrow + irow;
                    stitle_mm[irow] = SS.Sheets[0].Cells[iselrow + irow, 0].Text;
                }
                cf01.RPT_3_OpenData(irows_mm.Length, icols_mm.Length);
                cf01.RPT_4_AddData(SS, irows_mm, icols_mm, SeriseType.Rows);
                cf01.RPT_5_CloseData();
                cf01.RPT_7_SetXAsixTitleUsingSpreadHeader(SS, 0, 1);
                cf01.RPT_8_SetSeriseLegend(stitle_mm, SoftwareFX.ChartFX.Docked.Top);

                // 주별 Chart
                cf02.AxisX.Title.Text = "Weekly Status";

                cf02.AxisY.Title.Text = "DC YIELD";

                cf02.Gallery = SoftwareFX.ChartFX.Gallery.Curve;
                cf02.AxisY.DataFormat.Format = 0;
                cf02.AxisY.DataFormat.Decimals = 2;
                cf02.AxisX.Staggered = false;
                cf02.PointLabels = true;
                for (icol = 0; icol < ich_ww.Length; icol++)
                {
                    ich_ww[icol] = icol + 4;
                    icols_ww[icol] = icol + 4;
                }
                for (irow = 0; irow < SS.Sheets[0].RowCount; irow++)
                {
                    irows_ww[irow] = iselrow + irow;
                    stitle_ww[irow] = SS.Sheets[0].Cells[iselrow + irow, 0].Text;
                }
                cf02.RPT_3_OpenData(irows_ww.Length, icols_ww.Length);
                cf02.RPT_4_AddData(SS, irows_ww, icols_ww, SeriseType.Rows);
                cf02.RPT_5_CloseData();
                cf02.RPT_7_SetXAsixTitleUsingSpreadHeader(SS, 0, 4);
                cf02.RPT_8_SetSeriseLegend(stitle_ww, SoftwareFX.ChartFX.Docked.Top);

                // 일별 Chart
                cf03.AxisX.Title.Text = "Daily Status";

                cf03.AxisY.Title.Text = "DC YIELD";

                cf03.Gallery = SoftwareFX.ChartFX.Gallery.Curve;
                cf03.AxisY.DataFormat.Format = 0;
                cf03.AxisY.DataFormat.Decimals = 2;
                cf03.AxisX.Staggered = false;
                cf03.PointLabels = true;
                for (icol = 0; icol < ich_dd.Length; icol++)
                {
                    ich_dd[icol] = icol + 8;
                    icols_dd[icol] = icol + 8;
                }
                for (irow = 0; irow < SS.Sheets[0].RowCount; irow++)
                {
                    irows_dd[irow] = iselrow + irow;
                    stitle_dd[irow] = SS.Sheets[0].Cells[iselrow + irow, 0].Text;
                }
                cf03.RPT_3_OpenData(irows_dd.Length, icols_dd.Length);
                cf03.RPT_4_AddData(SS, irows_dd, icols_dd, SeriseType.Rows);
                cf03.RPT_5_CloseData();
                cf03.RPT_7_SetXAsixTitleUsingSpreadHeader(SS, 0, 8);
                cf03.RPT_8_SetSeriseLegend(stitle_dd, SoftwareFX.ChartFX.Docked.Top);

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
             * Created By : KIM-MINWOO(2010-10-04-월요일)
             * 
             * Modified By :KIM-MINWOO(2010-10-04-월요일)
             ****************************************************/
            try
            {
                if (cdvCustomer.Text.Equals("") || cdvCustomer.Text.Equals("ALL"))
                {
                    CmnFunction.ShowMsgBox(RptMessages.GetMessage("STD038", GlobalVariable.gcLanguage));
                    return;
                }

                Cursor.Current = Cursors.WaitCursor;

                cf01.RPT_1_ChartInit(); cf02.RPT_1_ChartInit(); cf03.RPT_1_ChartInit();
                cf01.RPT_2_ClearData(); cf02.RPT_2_ClearData(); cf03.RPT_2_ClearData();


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
              * Created By : KIM-MINWOO(2010-10-04-월요일)
             * 
             * Modified By :KIM-MINWOO(2010-10-04-월요일)
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
