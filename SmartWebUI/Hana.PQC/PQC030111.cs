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

namespace Hana.RAS
{

    /// <summary>
    /// 클  래  스: PQC030111<br/>
    /// 클래스요약: ASSY  공정능력조회현황<br/>
    /// 작  성  자: 장은희 <br/>
    /// 최초작성일: 2009-08-<br/>
    /// 상세  설명: ASSY  공정능력조회현황 <br/>
    /// 변경  내용:   <br/>
    /// </summary>
    public partial class PQC030111 : Miracom.SmartWeb.UI.Controls.udcCUSReport002
    {

        public PQC030111()
        {

            InitializeComponent();
            udcFromToDate.AutoBinding();
            SortInit();
            GridColumnInit();
            udcChartFX1.RPT_1_ChartInit();  //차트 초기화. 

            this.SetFactory(GlobalVariable.gsAssyDefaultFactory);
            cdvFactory.Text = GlobalVariable.gsAssyDefaultFactory;
            this.udcPkgType.sFactory = GlobalVariable.gsAssyDefaultFactory;

            cboChart.SelectedIndex = 0;

        }

        #region " Function Definition "

        /// <summary 1. 유효성 검사>
        /// <remarks></remarks>
        /// </summary>
        /// <returns></returns>
        private Boolean CheckField()
        {
            if (cdvFactory.Text.TrimEnd() == "")
            {
                CmnFunction.ShowMsgBox(RptMessages.GetMessage("STD001", GlobalVariable.gcLanguage));
                return false;
            }

            return true;
        }

        #region  한줄헤더생성
        /// <summary>
        /// 2. 헤더 생성
        /// </summary>
        private void GridColumnInit()
        {
            //spdData.ActiveSheet.ColumnHeader.Rows.Count = 1;
            //spdData.ActiveSheet.RowHeader.ColumnCount = 0 ;

            spdData.RPT_ColumnInit();

            spdData.RPT_AddBasicColumn("CUSTOMER", 0, 0, Visibles.False, Frozen.False, Align.Center, Merge.True, Formatter.String, 70);
            spdData.RPT_AddBasicColumn("FAMILY", 0, 1, Visibles.False, Frozen.False, Align.Center, Merge.True, Formatter.String, 60);
            spdData.RPT_AddBasicColumn("PACKAGE", 0, 2, Visibles.False, Frozen.False, Align.Center, Merge.True, Formatter.String, 60);
            spdData.RPT_AddBasicColumn("TYPE1", 0, 3, Visibles.False, Frozen.False, Align.Center, Merge.True, Formatter.String, 50);
            spdData.RPT_AddBasicColumn("TYPE2", 0, 4, Visibles.False, Frozen.False, Align.Center, Merge.True, Formatter.String, 50);
            spdData.RPT_AddBasicColumn("LD COUNT", 0, 5, Visibles.False, Frozen.False, Align.Center, Merge.True, Formatter.String, 60);
            spdData.RPT_AddBasicColumn("DENSITY", 0, 6, Visibles.False, Frozen.False, Align.Center, Merge.True, Formatter.String, 60);
            spdData.RPT_AddBasicColumn("GENERATION", 0, 7, Visibles.False, Frozen.False, Align.Center, Merge.True, Formatter.String, 80);
            spdData.RPT_AddBasicColumn("PIN TYPE", 0, 8, Visibles.True, Frozen.False, Align.Center, Merge.True, Formatter.String, 130);

            spdData.RPT_AddBasicColumn("Operation", 0, 9, Visibles.True, Frozen.False, Align.Center, Merge.True, Formatter.String, 80);
            spdData.RPT_AddBasicColumn("Classification", 0, 10, Visibles.True, Frozen.False, Align.Center, Merge.True, Formatter.String, 80);
            spdData.RPT_AddBasicColumn("Customer", 0, 11, Visibles.True, Frozen.False, Align.Center, Merge.False, Formatter.String, 80);
            spdData.RPT_AddBasicColumn("PKG Type", 0, 12, Visibles.True, Frozen.False, Align.Center, Merge.False, Formatter.String, 80);
            spdData.RPT_AddBasicColumn("Item", 0, 13, Visibles.True, Frozen.False, Align.Center, Merge.False, Formatter.String, 80);

            
           
            spdData.RPT_ColumnConfigFromTable(btnSort); //Group항목이 있을경우 반드시 선언해줄것.
        }
        #endregion


        /// <summary>
        /// 3. Group By 정의
        /// </summary>

        #region   GROUP BY
        private void SortInit()
        {
            ((udcTableForm)(this.btnSort.BindingForm)).Clear();
            ((udcTableForm)(this.btnSort.BindingForm)).AddRow("CUSTOMER", "MAT.MAT_GRP_1", "MAT_GRP_1", "NVL((SELECT DATA_1 FROM MGCMTBLDAT WHERE TABLE_NAME = 'H_CUSTOMER' AND KEY_1 = MAT_GRP_1 AND ROWNUM=1), '-') AS CUSTOMER", false);
            ((udcTableForm)(this.btnSort.BindingForm)).AddRow("FAMILY", "MAT.MAT_GRP_2", "MAT.MAT_GRP_2 AS FAMILY", "MIN(FAB_SEQ) AS FAMILY", false);
            ((udcTableForm)(this.btnSort.BindingForm)).AddRow("PACKAGE", "MAT.MAT_GRP_3", "MAT.MAT_GRP_3 AS PACKAGE", false);
            ((udcTableForm)(this.btnSort.BindingForm)).AddRow("TYPE1", "MAT.MAT_GRP_4", "MAT.MAT_GRP_4 AS TYPE1", false);
            ((udcTableForm)(this.btnSort.BindingForm)).AddRow("TYPE2", "MAT.MAT_GRP_5", "MAT.MAT_GRP_5 AS TYPE2", false);
            ((udcTableForm)(this.btnSort.BindingForm)).AddRow("LD COUNT", "MAT.MAT_GRP_6", "MAT.MAT_GRP_6 AS \"LD COUNT\"", false);
            ((udcTableForm)(this.btnSort.BindingForm)).AddRow("DENSITY", "MAT.MAT_GRP_7", "MAT.MAT_GRP_7 AS DENSITY", false);
            ((udcTableForm)(this.btnSort.BindingForm)).AddRow("GENERATION", "MAT.MAT_GRP_8", "MAT.MAT_GRP_8 AS GENERATION", false);
            ((udcTableForm)(this.btnSort.BindingForm)).AddRow("PIN TYPE", "MAT.MAT_CMF_10", "MAT.MAT_CMF_10 AS PIN_TYPE", false);

        }
        #endregion

        /// <summary>
        /// 4. 쿼리 생성
        /// </summary>
        /// <returns></returns>
        /// 

       
        private string MakeSqlString(int nIndex)
        {

            StringBuilder strSqlString = new StringBuilder();

            string[] selectDate1 = new string[udcFromToDate.SubtractBetweenFromToDate + 1];
            string strFromDate = udcFromToDate.ExactFromDate;
            string strToDate = udcFromToDate.ExactToDate;
            string strDate = string.Empty;
            string QueryCond1 = null;

            //추가
            udcTableForm tableForm = (udcTableForm)btnSort.BindingForm;
            QueryCond1 = tableForm.SelectedValueToQueryContainNull;

            int Between = udcFromToDate.SubtractBetweenFromToDate + 1;

            selectDate1 = udcFromToDate.getSelectDate();

            switch (udcFromToDate.DaySelector.SelectedValue.ToString())
            {
                case "DAY":
                    strDate = "WORK_DATE";
                    break;
                case "WEEK":
                    strDate = "WORK_WEEK";
                    break;
                case "MONTH":
                    strDate = "WORK_MONTH";
                    break;
                default:
                    strDate = "WORK_DATE";
                    break;
            }

            switch (nIndex)
            {
                case 0:  
                    {
                        #region " SPREAD"

                        #endregion
                    }
                    break;
                case 1:  
                    {
                        #region " 1번 그래프 "

                        #endregion
                    }
                    break;
                case 2:
                    {
                        #region " 2번 그래프 "
                        #endregion
                    }
                    break;
            }

            if (GlobalVariable.gsUserGroup == "ADMIN_GROUP" || GlobalVariable.gsUserGroup == "HANA_ADMIN_GROUP")
            {
                System.Windows.Forms.Clipboard.SetText(strSqlString.ToString());
            }

            return strSqlString.ToString();
        }


        private void ShowChart()
        {
            // 차트 설정

            #region " Chart 기본설정"           

            udcChartFX1.RPT_1_ChartInit();
            udcChartFX1.RPT_2_ClearData();

            // 그래프 설정 //                      
            udcChartFX1.AxisY.Font = new System.Drawing.Font("굴림체", 8, System.Drawing.FontStyle.Regular);
            udcChartFX1.AxisY2.Font = new System.Drawing.Font("굴림체", 8, System.Drawing.FontStyle.Regular);
            udcChartFX1.AxisX.Font = new System.Drawing.Font("굴림체", 8, System.Drawing.FontStyle.Regular);
            udcChartFX1.PointLabels = true;
            // ToolBar 보여주기
            udcChartFX1.ToolBar = true;

            //3D 
            udcChartFX1.Chart3D = true;

            // contion attribute 를 이용한 0 point label hidden            
            SoftwareFX.ChartFX.ConditionalAttributes contion = udcChartFX1.ConditionalAttributes[0];
            contion.Condition.From = 0;
            contion.Condition.To = 0;
            contion.PointLabels = false;

            #endregion

            switch (cboChart.SelectedIndex)
            {

                case 0:   // 전체보기
                    {
                        #region 전체보기

                        #endregion
                    }
                    break;

                case 1:  // 
                    {
                        #region Chart No.1
                        #endregion
                    }
                    break;
                case 2:   // 
                    {
                        #region Chart No.2
                        #endregion
                    }
                    break;
            }            
        }

        #endregion

        #region " EVENT HANDLER "

        private void btnView_Click(object sender, EventArgs e)
        {
            DataTable dt = null;

            if (CheckField() == false) return;

            //GridColumnInit();
            //spdData_Sheet1.RowCount = 0;

            try
            {
                LoadingPopUp.LoadIngPopUpShow(this);
                this.Refresh();

                dt = CmnFunction.oComm.GetFuncDataTable("DYNAMIC", MakeSqlString(0));
                GridColumnInit();
                spdData_Sheet1.RowCount = 0;
                //    spdData_Sheet1.ColumnCount = 0;

                if (dt.Rows.Count == 0)
                {
                    dt.Dispose();
                    LoadingPopUp.LoadingPopUpHidden();
                    CmnFunction.ShowMsgBox(RptMessages.GetMessage("STD010", GlobalVariable.gcLanguage));
                    return;
                }

                //by John
                //1.Griid 합계 표시

                spdData.DataSource = dt;
                spdData.RPT_ColumnConfigFromTable(btnSort);


                //2. 칼럼 고정(필요하다면..)
                //spdData.Sheets[0].FrozenColumnCount = 10;

                ////3. Total부분 셀머지
                //spdData.RPT_FillDataSelectiveCells("Total", 0, 4, 0, 9, true, Align.Center, VerticalAlign.Center);

                //4. Column Auto Fit4
                spdData.RPT_AutoFit(false);

                //// 1번 그래프 그리도록 이벤트 발생
                //cboChart.SelectedIndex = 0;
                ShowChart();

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

        #region DataTable Pivot Function
        public DataTable GetRotatedDataTable(ref DataTable dt)
        {
            int nColToRow = 0;  // Column Position of dt => Legend Column of dtNew

            DataTable dtNew = new DataTable();
            Object[] dr = null;

            // Get Series Type
            Type type = dt.Columns[1].DataType;

            // Adding Columns
            dtNew.Columns.Add("GUBUN", typeof(String));
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                dtNew.Columns.Add(dt.Rows[i][0].ToString(), type);
            }

            // Filling Data
            for (int j = nColToRow + 1; j < dt.Columns.Count; j++)
            {
                dr = dtNew.NewRow().ItemArray;
                dr[0] = dt.Columns[j].Caption;
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    dr[i + 1] = dt.Rows[i][j];
                }
                dtNew.Rows.Add(dr);
            }
            return dtNew;
        }
        #endregion

        private void cdvFactory_ValueSelectedItemChanged(object sender, Miracom.UI.MCCodeViewSelChanged_EventArgs e)
        {
            this.SetFactory(cdvFactory.txtValue);
        }

        private void spdData_CellClick(object sender, FarPoint.Win.Spread.CellClickEventArgs e)
        {
            if (e.Column > 10)
            {
                //mnuPopup.Show();
            }

        }

    

        private void cboChart_SelectedIndexChanged_1(object sender, EventArgs e)
        {
            spdData.ActiveSheet.ColumnHeader.Rows.Count = 1;
            spdData.RPT_ColumnInit();
            GridColumnInit();
            SortInit();
        }

        #region POPUP MENU

        #endregion



        #endregion





    }
}