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
    public partial class PQC031109 : Miracom.SmartWeb.UI.Controls.udcCUSReport002
    {
        private string strSelect;
        private string strDecode;

        /// <summary>
        /// 클  래  스: PQC031109<br/>
        /// 클래스요약: APQP Data Interface 조회<br/>
        /// 작  성  자: 배수민<br/>
        /// 최초작성일: 2010-12-29<br/>
        /// 상세  설명: APQP Data Interface 조회<br/>
        /// 변경  내용: <br/>

        /// </summary>
        public PQC031109()
        {
            InitializeComponent();
            SortInit();
            GridColumnInit();

            strSelect = string.Empty;
            strDecode = string.Empty;

        }

        #region " Constant Definition "

        #endregion

        #region " Function Definition "

        /// <summary 1. 유효성 검사>
        /// <remarks></remarks>
        /// </summary>
        /// <returns></returns>
        private Boolean CheckField()
        {

            return true;

        }

        /// <summary>
        /// 2. 헤더 생성
        /// </summary>
        private void GridColumnInit()
        {

            spdData.RPT_ColumnInit();

            spdData.RPT_AddBasicColumn("MAT_ID", 0, 0, Visibles.True, Frozen.True, Align.Left, Merge.True, Formatter.String, 100);
            spdData.RPT_AddBasicColumn("IQC_NO", 0, 1, Visibles.True, Frozen.True, Align.Left, Merge.True, Formatter.String, 150);
            spdData.RPT_AddBasicColumn("APQP_NO", 0, 2, Visibles.True, Frozen.True, Align.Left, Merge.True, Formatter.String, 130);
            spdData.RPT_AddBasicColumn("FINAL_DATA", 0, 3, Visibles.True, Frozen.True, Align.Left, Merge.True, Formatter.String, 80);
            spdData.RPT_AddBasicColumn("PROGRESS", 0, 4, Visibles.True, Frozen.True, Align.Left, Merge.True, Formatter.String, 80);
            spdData.RPT_AddBasicColumn("PHASE1", 0, 5, Visibles.True, Frozen.True, Align.Center, Merge.True, Formatter.String, 50);
            spdData.RPT_AddBasicColumn("PHASE2", 0, 6, Visibles.True, Frozen.True, Align.Center, Merge.True, Formatter.String, 50);
            spdData.RPT_AddBasicColumn("PHASE3", 0, 7, Visibles.True, Frozen.True, Align.Center, Merge.True, Formatter.String, 50);
            spdData.RPT_AddBasicColumn("PHASE4", 0, 8, Visibles.True, Frozen.True, Align.Center, Merge.True, Formatter.String, 50);
            spdData.RPT_AddBasicColumn("PHASE5", 0, 9, Visibles.True, Frozen.True, Align.Center, Merge.True, Formatter.String, 50);
            spdData.RPT_AddBasicColumn("PHASE6", 0, 10, Visibles.True, Frozen.True, Align.Center, Merge.True, Formatter.String, 50);
            spdData.RPT_AddBasicColumn("PHASE7", 0, 11, Visibles.True, Frozen.False, Align.Center, Merge.True, Formatter.String, 50);
            spdData.RPT_AddBasicColumn("PHASE8", 0, 12, Visibles.True, Frozen.False, Align.Center, Merge.True, Formatter.String, 50);
            spdData.RPT_AddBasicColumn("PHASE9", 0, 13, Visibles.True, Frozen.False, Align.Center, Merge.True, Formatter.String, 50);
            spdData.RPT_AddBasicColumn("PHASE10", 0, 14, Visibles.True, Frozen.False, Align.Center, Merge.True, Formatter.String, 50);
          
           
        }

        /// <summary>
        /// 3. Group By 정의
        /// </summary>
        private void SortInit()
        {

        }

        /// <summary>
        /// 4. 쿼리 생성
        /// </summary>
        /// <returns></returns>
        private string MakeSqlString()
        {
            StringBuilder strSqlString = new StringBuilder();

            string QueryCond1;
            string QueryCond2;
            string QueryCond3;

            udcTableForm tableForm = (udcTableForm)btnSort.BindingForm;

            QueryCond1 = tableForm.SelectedValueToQueryContainNull;
            QueryCond2 = tableForm.SelectedValue2ToQueryContainNull;
            QueryCond3 = tableForm.SelectedValue3ToQueryContainNull;

            strSqlString.Append("  SELECT MAT_ID, IQC_NO, APQP_NO, FINAL_DATA, PROGRESS " + QueryCond1 + "\n");
            strSqlString.Append("       , PHASE1, PHASE2, PHASE3, PHASE4, PHASE5, PHASE6, PHASE7, PHASE8, PHASE9, PHASE10 " + "\n");
            strSqlString.Append("    FROM CIQCAPQDAT@RPTTOMES " + "\n");
            strSqlString.Append("   WHERE DEL_FLAG = ' ' " + "\n");
        

          

            if (GlobalVariable.gsUserGroup == "ADMIN_GROUP" || GlobalVariable.gsUserGroup == "HANA_ADMIN_GROUP")
            {
                System.Windows.Forms.Clipboard.SetText(strSqlString.ToString());
            }

            return strSqlString.ToString();
        }


        /// <summary>
        /// 5. Chart 생성
        /// </summary>
        /// <param name="DT">Chart를 생성할 데이터 테이블</param>
        private void MakeChart(DataTable DT)
        {

        }

        #endregion


        private void btnView_Click(object sender, EventArgs e)
        {

            DataTable dt = null;

            if (CheckField() == true)
            {
                GridColumnInit(); 
                spdData_Sheet1.RowCount = 0;

                spdData.Visible = true;

            }

            try
            {
                LoadingPopUp.LoadIngPopUpShow(this);

                this.Refresh();

                dt = CmnFunction.oComm.GetFuncDataTable("DYNAMIC", MakeSqlString()); //데이터 저장

                if (dt.Rows.Count == 0)
                {
                    dt.Dispose();
                    LoadingPopUp.LoadingPopUpHidden();
                    CmnFunction.ShowMsgBox(RptMessages.GetMessage("STD010", GlobalVariable.gcLanguage));
                    return;
                }

                spdData.DataSource = dt; //데이터 뿌려줌

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
            // Excel 바로 보이기
            //ExcelHelper.Instance.subMakeExcel(spdData, this.lblTitle.Text, " ^ ", " ^ ", true);
            spdData.ExportExcel();
        }

       

    }
}
