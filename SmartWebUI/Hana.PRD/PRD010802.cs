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
    public partial class PRD010802 : Miracom.SmartWeb.UI.Controls.udcCUSReport002
    {
        DataTable columnList = null;

        /// <summary>
        /// 클  래  스: PRD010802<br/>
        /// 클래스요약: GCM DATA 조회<br/>
        /// 작  성  자: 하나마이크론 임종우<br/>
        /// 최초작성일: 2012-05-21<br/>
        /// 상세  설명: GCM DATA 조회 화면<br/>
        /// 변경  내용: <br/>     

        /// </summary>
        /// 
        public PRD010802()
        {
            InitializeComponent();                       
            
            SortInit();
            GridColumnInit();            
        }

        #region 초기화 및 유효성 검사
        /// <summary 1. 유효성 검사>
        ///
        /// </summary>        
        private Boolean CheckField()
        {
            if (cdvFactory.Text.TrimEnd() == "")
            {
                CmnFunction.ShowMsgBox(RptMessages.GetMessage("STD001", GlobalVariable.gcLanguage));
                return false;
            }
            
            if (cdvGcmList.Text.TrimEnd() == "")
            {
                CmnFunction.ShowMsgBox(RptMessages.GetMessage("STD999", GlobalVariable.gcLanguage));
                return false;
            }

            return true;
        }

        /// <summary 2. 헤더 생성> 
        ///
        /// </summary>
        private void GridColumnInit()
        {
            spdData.ActiveSheet.ColumnHeader.Rows.Count = 1;
            
            try
            {
                spdData.RPT_ColumnInit();

                if (cdvFactory.Text != "")
                {
                    for (int i = 0; i < 20; i++)
                    {
                        if (columnList.Rows[0][i].ToString() == " ")
                        {
                            spdData.RPT_AddBasicColumn(columnList.Rows[0][i].ToString(), 0, i, Visibles.False, Frozen.True, Align.Left, Merge.False, Formatter.String, 60);
                        }
                        else
                        {
                            spdData.RPT_AddBasicColumn(columnList.Rows[0][i].ToString(), 0, i, Visibles.True, Frozen.True, Align.Left, Merge.False, Formatter.String, 60);
                        }

                        // Key 데이타는 색상 표시 함.
                        if (i < 10)
                        {
                            spdData.ActiveSheet.Columns[i].BackColor = Color.LightYellow;
                        }
                    }
                }                

                spdData.RPT_ColumnConfigFromTable(btnSort); //Group항목이 있을경우 반드시 선언해줄것.
            }

            catch (Exception ex)
            {
                CmnFunction.ShowMsgBox(ex.Message);
            }
        }

        /// <summary 3. SortInit>
        /// 
        /// </summary>
        private void SortInit()
        {

        }
        #endregion


        #region SQL 쿼리 Build
        /// <summary 4. SQL 쿼리 Build>
        /// 
        /// </summary>
        /// <returns> strSqlString </returns>

        private string MakeSqlString()
        {
            StringBuilder strSqlString = new StringBuilder();

            strSqlString.Append("SELECT KEY_1, KEY_2, KEY_3, KEY_4, KEY_5, KEY_6, KEY_7, KEY_8, KEY_9, KEY_10 " + "\n");
            strSqlString.Append("     , DATA_1, DATA_2, DATA_3, DATA_4, DATA_5, DATA_6, DATA_7, DATA_8, DATA_9, DATA_10 " + "\n");
            strSqlString.Append("  FROM MGCMTBLDAT " + "\n");
            strSqlString.Append(" WHERE 1=1 " + "\n");
            strSqlString.Append("   AND FACTORY = '" + cdvFactory.Text + "'" + "\n");
            strSqlString.Append("   AND TABLE_NAME = '" + cdvGcmList.Text + "' " + "\n");
                        

            if (GlobalVariable.gsUserGroup == "ADMIN_GROUP" || GlobalVariable.gsUserGroup == "HANA_ADMIN_GROUP")
            {
                System.Windows.Forms.Clipboard.SetText(strSqlString.ToString());
            }

            return strSqlString.ToString();
        }

        // GCM 테이블의 헤더 가져오기
        private string MakeSqlString2()
        {
            StringBuilder strSqlString = new StringBuilder();

            strSqlString.Append("SELECT KEY_1_PRT, KEY_2_PRT, KEY_3_PRT, KEY_4_PRT, KEY_5_PRT, KEY_6_PRT, KEY_7_PRT, KEY_8_PRT, KEY_9_PRT, KEY_10_PRT" + "\n");
            strSqlString.Append("     , DATA_1_PRT, DATA_2_PRT, DATA_3_PRT, DATA_4_PRT, DATA_5_PRT, DATA_6_PRT, DATA_7_PRT, DATA_8_PRT, DATA_9_PRT, DATA_10_PRT " + "\n");
            strSqlString.Append("  FROM MGCMTBLDEF " + "\n");
            strSqlString.Append(" WHERE 1=1 " + "\n");
            strSqlString.Append("   AND FACTORY = '" + cdvFactory.Text + "'" + "\n");
            strSqlString.Append("   AND TABLE_NAME = '" + cdvGcmList.Text + "'" + "\n");

            return strSqlString.ToString();
        }
        #endregion


        #region EVENT 처리
        /// <summary 5. View>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnView_Click(object sender, EventArgs e)
        {
            DataTable dt = null;
            if (CheckField() == false) return;

            try
            {
                LoadingPopUp.LoadIngPopUpShow(this);
                this.Refresh();

                columnList = CmnFunction.oComm.GetFuncDataTable("DYNAMIC", MakeSqlString2());

                GridColumnInit();
                dt = CmnFunction.oComm.GetFuncDataTable("DYNAMIC", MakeSqlString());

                if (dt.Rows.Count == 0)
                {
                    dt.Dispose();
                    LoadingPopUp.LoadingPopUpHidden();
                    CmnFunction.ShowMsgBox(RptMessages.GetMessage("STD010", GlobalVariable.gcLanguage));
                    return;
                }

                spdData.DataSource = dt;

                //4. Column Auto Fit
                spdData.RPT_AutoFit(false);
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

        private void button_Search(object sender, EventArgs e)
        {
            String key = this.textBox1.Text;
            if (key != "")
            {
                for (int i = 0; i < this.spdData.ActiveSheet.RowCount; i++)
                {
                    bool containRow = false;
                    this.spdData.ActiveSheet.Rows[i].Visible = true;
                    for (int j = 0; j < this.spdData.ActiveSheet.ColumnCount; j++)
                    {
                        if (this.spdData.ActiveSheet.Cells[i, j].Value.ToString().IndexOf(key) != -1)
                        {
                            containRow = true;
                        }
                    }
                    if (!containRow)
                    {
                        this.spdData.ActiveSheet.Rows[i].Visible = false;
                    }
                }
            }
        }

        /// <summary 6. Excel Export>
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
        #endregion

        private void cdvGcmList_ValueButtonPress(object sender, EventArgs e)
        {
            string strQuery = string.Empty;
                        
            strQuery += "SELECT KEY_1 AS CODE, DATA_1 AS DATA " + "\n";
            strQuery += "  FROM MGCMTBLDAT " + "\n";
            strQuery += " WHERE 1=1 " + "\n";
            strQuery += "   AND FACTORY = '" + GlobalVariable.gsAssyDefaultFactory + "'" + "\n";
            strQuery += "   AND TABLE_NAME = 'H_RPT_GCM_DISP_ITEM' " + "\n";
            strQuery += " ORDER BY KEY_1 " + "\n";

            cdvGcmList.sDynamicQuery = strQuery;            
        }

        private void udcCUSCondition1_Load(object sender, EventArgs e)
        {

        }

        private void cdvGcmList_Load(object sender, EventArgs e)
        {

        }

    }
}
