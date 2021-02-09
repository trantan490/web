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


namespace Hana.CUS
{
    public partial class CUS060104 : Miracom.SmartWeb.UI.Controls.udcCUSReport002
    {
        /// <summary>
        /// 클  래  스: CUS060104<br/>
        /// 클래스요약: 고객사 TAT<br/>
        /// 작  성  자: 미라콤 김민규<br/>
        /// 최초작성일: 2008-12-05<br/>
        /// 상세  설명: 고객사 YIELD<br/>
        /// 변경  내용: <br/>
        /// </summary>

        static DataTable global_oper_Assy = null;
        static DataTable global_oper_Test = null;
        

        public CUS060104()
        {
            InitializeComponent();
            cdvFromToDate.AutoBinding();
            SortInit();           
            GridColumnInit();
        }


        #region 유효성 검사 및 초기화
        /// <summary 1. 유효성 검사>
        /// <remarks></remarks>
        /// </summary>
        /// <returns></returns>
        private Boolean CheckField()
        {
            if (cdvFactory.txtValue.Trim() == "")
            {
                CmnFunction.ShowMsgBox(RptMessages.GetMessage("STD001", GlobalVariable.gcLanguage));
                return false;
            }

            if (CheckFactory("HMKE1") == true || CheckFactory("FGS") == true)
            {
                CmnFunction.ShowMsgBox(RptMessages.GetMessage("STD031", GlobalVariable.gcLanguage));
                return false;
            }

            return true;
        }

        private bool CheckFactory(string SelFactory)
        {
            string[] factory = cdvFactory.txtValue.Split(',');

            for (int i = 0; i < factory.Length; i++)
            {
                if (factory[i].Trim().ToString() == SelFactory)
                    return true;
            }

            return false;
        }

        /// <summary>
        /// 2. 헤더 생성
        /// </summary>
        private void GridColumnInit()
        {
            spdData.ActiveSheet.ColumnHeader.Rows.Count = 1;
            spdData.RPT_ColumnInit();
            spdData.RPT_AddBasicColumn("Customer", 0, 0, Visibles.False, Frozen.True, Align.Center, Merge.True, Formatter.String, 60);
            spdData.RPT_AddBasicColumn("Family", 0, 1, Visibles.False, Frozen.True, Align.Center, Merge.True, Formatter.String, 60);
            spdData.RPT_AddBasicColumn("Package", 0, 2, Visibles.True, Frozen.True, Align.Center, Merge.True, Formatter.String, 60);
            spdData.RPT_AddBasicColumn("Type1", 0, 3, Visibles.False, Frozen.True, Align.Center, Merge.True, Formatter.String, 60);
            spdData.RPT_AddBasicColumn("Type2", 0, 4, Visibles.False, Frozen.True, Align.Center, Merge.True, Formatter.String, 60);
            spdData.RPT_AddBasicColumn("LD Count", 0, 5, Visibles.False, Frozen.True, Align.Center, Merge.True, Formatter.String, 60);
            spdData.RPT_AddBasicColumn("Density", 0, 6, Visibles.False, Frozen.True, Align.Center, Merge.True, Formatter.String, 60);
            spdData.RPT_AddBasicColumn("Generation", 0, 7, Visibles.False, Frozen.True, Align.Center, Merge.True, Formatter.String, 70);
            spdData.RPT_AddBasicColumn("Product", 0, 8, Visibles.True, Frozen.True, Align.Left, Merge.True, Formatter.String, 80);
            spdData.RPT_AddBasicColumn("LOTID", 0, 9, Visibles.True, Frozen.True, Align.Left, Merge.True, Formatter.String, 80);

            global_oper_Test = CmnFunction.oComm.GetFuncDataTable("DYNAMIC", MakeSqlString1());
            global_oper_Assy = CmnFunction.oComm.GetFuncDataTable("DYNAMIC", MakeSqlString2());

            if (CheckFactory("ALL") == true)
            {
                global_oper_Test = CmnFunction.oComm.GetFuncDataTable("DYNAMIC", MakeSqlString1());
                global_oper_Assy = CmnFunction.oComm.GetFuncDataTable("DYNAMIC", MakeSqlString2());

                for (int i = 0; i < global_oper_Assy.Rows.Count; i++)
                {
                    spdData.RPT_AddBasicColumn(global_oper_Assy.Rows[i][0].ToString(), 0, (10 + i), Visibles.True, Frozen.False, Align.Center, Merge.False, Formatter.Double1, 80);
                }
                spdData.RPT_AddBasicColumn("Assy Total", 0, 10 + global_oper_Assy.Rows.Count, Visibles.True, Frozen.False, Align.Center, Merge.False, Formatter.Double1, 70);

                for (int i = 0; i < global_oper_Test.Rows.Count; i++)
                {
                    spdData.RPT_AddBasicColumn(global_oper_Test.Rows[i][0].ToString(), 0, (11 + global_oper_Assy.Rows.Count + i), Visibles.True, Frozen.False, Align.Center, Merge.False, Formatter.Double1, 80);
                }
                spdData.RPT_AddBasicColumn("Test Total", 0, (global_oper_Assy.Rows.Count + global_oper_Test.Rows.Count + 11), Visibles.True, Frozen.False, Align.Center, Merge.False, Formatter.Double1, 70);
                spdData.RPT_AddBasicColumn("Total", 0, (global_oper_Assy.Rows.Count + global_oper_Test.Rows.Count + 12), Visibles.True, Frozen.False, Align.Center, Merge.False, Formatter.Double1, 70);

            }
            
            if (CheckFactory(GlobalVariable.gsAssyDefaultFactory) == true)
            {
                global_oper_Assy = CmnFunction.oComm.GetFuncDataTable("DYNAMIC", MakeSqlString2());

                for (int i = 0; i < global_oper_Assy.Rows.Count; i++)
                {
                    spdData.RPT_AddBasicColumn(global_oper_Assy.Rows[i][0].ToString(), 0, (10 + i), Visibles.True, Frozen.False, Align.Center, Merge.False, Formatter.Double1, 80);
                }
                spdData.RPT_AddBasicColumn("Assy Total", 0, 10 + global_oper_Assy.Rows.Count, Visibles.True, Frozen.False, Align.Center, Merge.False, Formatter.Double1, 70);
                spdData.RPT_AddBasicColumn("Total", 0, (global_oper_Assy.Rows.Count + 11), Visibles.True, Frozen.False, Align.Center, Merge.False, Formatter.Double1, 70);
            }
            
            if (CheckFactory(GlobalVariable.gsTestDefaultFactory) == true)
            {
                global_oper_Test = CmnFunction.oComm.GetFuncDataTable("DYNAMIC", MakeSqlString1());


                for (int i = 0; i < global_oper_Test.Rows.Count; i++)
                {
                    spdData.RPT_AddBasicColumn(global_oper_Test.Rows[i][0].ToString(), 0, (10 + i), Visibles.True, Frozen.False, Align.Center, Merge.False, Formatter.Double1, 80);
                }
                spdData.RPT_AddBasicColumn("Test Total", 0, (global_oper_Test.Rows.Count + 10), Visibles.True, Frozen.False, Align.Center, Merge.False, Formatter.Double1, 70);
                spdData.RPT_AddBasicColumn("Total", 0, (global_oper_Test.Rows.Count + 11), Visibles.True, Frozen.False, Align.Center, Merge.False, Formatter.Double1, 70);
            }
                spdData.RPT_ColumnConfigFromTable(btnSort); //Group항목이 있을경우 반드시 선언해줄것.
        }

        /// <summary>
        /// 3. Group By 정의
        /// </summary>
        private void SortInit()
        {
            ((udcTableForm)(this.btnSort.BindingForm)).AddRow("Customer", "MAT_GRP_1", "B.MAT_GRP_1", "MAT_GRP_1", true);
            ((udcTableForm)(this.btnSort.BindingForm)).AddRow("Family", "MAT_GRP_2", "B.MAT_GRP_2", "MAT_GRP_2", false);
            ((udcTableForm)(this.btnSort.BindingForm)).AddRow("Package", "MAT_GRP_3", "B.MAT_GRP_3", "MAT_GRP_3", true);
            ((udcTableForm)(this.btnSort.BindingForm)).AddRow("Type1", "MAT_GRP_4", "B.MAT_GRP_4", "MAT_GRP_4", false);
            ((udcTableForm)(this.btnSort.BindingForm)).AddRow("Type2", "MAT_GRP_5", "B.MAT_GRP_5", "MAT_GRP_5", false);
            ((udcTableForm)(this.btnSort.BindingForm)).AddRow("LD Count", "MAT_GRP_6", "B.MAT_GRP_6", "MAT_GRP_6", false);
            ((udcTableForm)(this.btnSort.BindingForm)).AddRow("Density", "MAT_GRP_7", "B.MAT_GRP_7", "MAT_GRP_7", false);
            ((udcTableForm)(this.btnSort.BindingForm)).AddRow("Generation", "MAT_GRP_8", "B.MAT_GRP_8", "MAT_GRP_8", false);
            ((udcTableForm)(this.btnSort.BindingForm)).AddRow("Product", "MAT_CMF_7", "B.MAT_CMF_7", "MAT_CMF_7", true);
            ((udcTableForm)(this.btnSort.BindingForm)).AddRow("Lot ID", "LOT_ID", "A.LOT_ID", "LOT_ID", true);     
        } 
        #endregion


        #region SQL 쿼리 Build 
        /// <summary>
        /// 4. 쿼리 생성
        /// </summary>
        /// <returns></returns>
          
        private string MakeSqlString()
        {
            StringBuilder strSqlString = new StringBuilder();

            string QueryCond1 = null;
            string QueryCond2 = null;
            string QueryCond3 = null;
            string sStart_Tran_Time = null;
            sStart_Tran_Time = string.Empty;
            string sEnd_Tran_Time = null;
            sEnd_Tran_Time = string.Empty;
                                               
            udcTableForm tableForm = (udcTableForm)btnSort.BindingForm;
            QueryCond1 = tableForm.SelectedValueToQueryContainNull;            
            QueryCond2 = tableForm.SelectedValue2ToQueryContainNull;
            QueryCond3 = tableForm.SelectedValue3ToQueryContainNull;


            strSqlString.AppendFormat("SELECT {0} " + "\n", QueryCond1);

            if (CheckFactory("ALL") == true)
            {
                for (int i = 0; i < global_oper_Assy.Rows.Count; i++)
                {
                    strSqlString.Append("              , ROUND(SUM(DECODE(OPER_GRP_1, '" + global_oper_Assy.Rows[i][0] + "', PROC_TIME, 0))/60/60/24,3) AS A" + i + "\n");
                }
                strSqlString.Append("              , ROUND(SUM(DECODE(FACTORY, '" + GlobalVariable.gsAssyDefaultFactory + "', PROC_TIME, 0))/60/60/24,3) AS HMKA1 " + "\n");

                for (int i = 0; i < global_oper_Test.Rows.Count; i++)
                {
                    strSqlString.Append("              , ROUND(SUM(DECODE(OPER_GRP_1, '" + global_oper_Test.Rows[i][0]+ "', PROC_TIME, 0))/60/60/24,3) AS T" + i + "\n");
                }
                strSqlString.Append("              , ROUND(SUM(DECODE(FACTORY, '" + GlobalVariable.gsTestDefaultFactory + "', PROC_TIME, 0))/60/60/24,3) AS HMKT1 " + "\n");
                strSqlString.Append("              , ROUND(SUM(PROC_TIME)/60/60/24,3) AS TOTAL_TAT " + "\n");
            }
            
            if (CheckFactory(GlobalVariable.gsAssyDefaultFactory) == true)
            {
                for (int i = 0; i < global_oper_Assy.Rows.Count; i++)
                {
                    strSqlString.Append("              , ROUND(SUM(DECODE(OPER_GRP_1, '" + global_oper_Assy.Rows[i][0] + "', PROC_TIME, 0))/60/60/24,3) AS A" + i + "\n");
                }
                strSqlString.Append("              , ROUND(SUM(DECODE(FACTORY, '" + GlobalVariable.gsAssyDefaultFactory + "', PROC_TIME, 0))/60/60/24,3) AS HMKA1 " + "\n");
            }
            
            if (CheckFactory(GlobalVariable.gsTestDefaultFactory) == true)
            {
                for (int i = 0; i < global_oper_Test.Rows.Count; i++)
                {
                    strSqlString.Append("              , ROUND(SUM(DECODE(OPER_GRP_1, '" + global_oper_Test.Rows[i][0] + "', PROC_TIME, 0))/60/60/24,3) AS T" + i + "\n");
                }
                strSqlString.Append("              , ROUND(SUM(DECODE(FACTORY, '" + GlobalVariable.gsTestDefaultFactory + "', PROC_TIME, 0))/60/60/24,3) AS HMKT1 " + "\n");
            }
                        
            strSqlString.Append("           FROM (      " + "\n");
            strSqlString.AppendFormat("                SELECT {0}, A.OPER, C.OPER_GRP_1, A.PROC_TIME, A.FACTORY  " + "\n",QueryCond2);
            strSqlString.Append("                  FROM RSUMOUTLTH A " + "\n");
            strSqlString.Append("                     , MWIPMATDEF B " + "\n");
            strSqlString.Append("                     , MWIPOPRDEF C " + "\n");                       
            strSqlString.Append("                 WHERE A.FACTORY " + cdvFactory.SelectedValueToQueryString + "\n");            
            strSqlString.Append("                   AND A.FACTORY = B.FACTORY " + "\n");
            strSqlString.Append("                   AND A.FACTORY = C.FACTORY " + "\n");
            strSqlString.Append("                   AND A.MAT_ID = B.MAT_ID " + "\n");
            strSqlString.Append("                   AND A.OPER = C.OPER " + "\n");
            strSqlString.Append("                   AND A.MAT_VER = 1 " + "\n");
            strSqlString.Append("                   AND A.MAT_VER = B.MAT_VER  " + "\n");

            //관리자만 고객사 화면에서 CUSTOMER 별로 조회 가능
            if (GlobalVariable.gsUserGroup == "ADMIN_GROUP" || GlobalVariable.gsUserGroup == "HANA_ADMIN_GROUP")
            {
                udcWIPCondition1.Enabled = true;
                
                if (udcWIPCondition1.Text != "ALL" && udcWIPCondition1.Text != "")
                    strSqlString.AppendFormat("       AND B.MAT_GRP_1 {0} " + "\n", udcWIPCondition1.SelectedValueToQueryString);
            }
            else
            {
                strSqlString.Append("                   AND B.MAT_GRP_1 = '" + GlobalVariable.gsCustomer +"'" + "\n");
            }

            if (cdvFromToDate.DaySelector.SelectedValue.ToString() == "DAY")
            {
                sStart_Tran_Time = cdvFromToDate.Start_Tran_Time.ToString();
                sEnd_Tran_Time = cdvFromToDate.End_Tran_Time.ToString();

                strSqlString.AppendFormat(" AND END_TIME BETWEEN '{0}' AND '{1}' " + "\n", sStart_Tran_Time, sEnd_Tran_Time);
            }

            //상세 조회에 따른 SQL문 생성                                        
            //if (udcWIPCondition1.Text != "ALL" && udcWIPCondition1.Text != "")
            //    strSqlString.AppendFormat("       AND B.MAT_GRP_1 {0} " + "\n", udcWIPCondition2.SelectedValueToQueryString);

            if (udcWIPCondition2.Text != "ALL" && udcWIPCondition2.Text != "")
                strSqlString.AppendFormat("       AND B.MAT_GRP_2 {0} " + "\n", udcWIPCondition2.SelectedValueToQueryString);

            if (udcWIPCondition3.Text != "ALL" && udcWIPCondition3.Text != "")
                strSqlString.AppendFormat("       AND B.MAT_GRP_3 {0} " + "\n", udcWIPCondition3.SelectedValueToQueryString);

            if (udcWIPCondition4.Text != "ALL" && udcWIPCondition4.Text != "")
                strSqlString.AppendFormat("       AND B.MAT_GRP_4 {0} " + "\n", udcWIPCondition4.SelectedValueToQueryString);

            if (udcWIPCondition5.Text != "ALL" && udcWIPCondition5.Text != "")
                strSqlString.AppendFormat("       AND B.MAT_GRP_5 {0} " + "\n", udcWIPCondition5.SelectedValueToQueryString);

            if (udcWIPCondition6.Text != "ALL" && udcWIPCondition6.Text != "")
                strSqlString.AppendFormat("       AND B.MAT_GRP_6 {0} " + "\n", udcWIPCondition6.SelectedValueToQueryString);

            if (udcWIPCondition7.Text != "ALL" && udcWIPCondition7.Text != "")
                strSqlString.AppendFormat("       AND B.MAT_GRP_7 {0} " + "\n", udcWIPCondition7.SelectedValueToQueryString);

            if (udcWIPCondition8.Text != "ALL" && udcWIPCondition8.Text != "")
                strSqlString.AppendFormat("       AND B.MAT_GRP_8 {0} " + "\n", udcWIPCondition8.SelectedValueToQueryString);

            if (udcWIPCondition9.Text != "ALL" && udcWIPCondition9.Text != "")
                strSqlString.AppendFormat("       AND B.MAT_GRP_9 {0} " + "\n", udcWIPCondition9.SelectedValueToQueryString);

            strSqlString.Append("                )                     " + "\n");
            strSqlString.AppendFormat("       GROUP BY {0}     " + "\n", QueryCond1);

            if (GlobalVariable.gsUserGroup == "ADMIN_GROUP" || GlobalVariable.gsUserGroup == "HANA_ADMIN_GROUP")
            {
                System.Windows.Forms.Clipboard.SetText(strSqlString.ToString());
            }

            return strSqlString.ToString();            
        }

        private string MakeSqlString1()
        {
            StringBuilder strSqlString = new StringBuilder();

            string QueryCond1 = null;

            udcTableForm tableForm = (udcTableForm)btnSort.BindingForm;

            QueryCond1 = tableForm.SelectedValueToQueryContainNull;

            strSqlString.Append("     SELECT OPER_GRP_1, MIN(OPER)  " + "\n");
            strSqlString.Append("                FROM MWIPOPRDEF  " + "\n");
            strSqlString.Append("               WHERE FACTORY = '" + GlobalVariable.gsTestDefaultFactory + "' " + "\n");
            strSqlString.Append("           GROUP BY OPER_GRP_1  " + "\n");
            strSqlString.Append("           ORDER BY MIN(OPER) ASC " + "\n");

            return strSqlString.ToString();
        }


        private string MakeSqlString2()
        {
            StringBuilder strSqlString = new StringBuilder();

            string QueryCond1 = null;

            udcTableForm tableForm = (udcTableForm)btnSort.BindingForm;

            QueryCond1 = tableForm.SelectedValueToQueryContainNull;

            strSqlString.Append("              SELECT OPER_GRP_1, MIN(OPER) " + "\n");
            strSqlString.Append("                FROM MWIPOPRDEF  " + "\n");
            strSqlString.Append("               WHERE FACTORY = '" + GlobalVariable.gsAssyDefaultFactory + "' " + "\n");
            strSqlString.Append("                 AND OPER NOT IN ('00001', '00002')  " + "\n");
            strSqlString.Append("                 AND OPER_GRP_1 NOT IN ('-')  " + "\n");
            strSqlString.Append("            GROUP BY OPER_GRP_1 " + "\n");
            strSqlString.Append("            ORDER BY MIN(OPER) " + "\n");

            return strSqlString.ToString();
        }

        #endregion


        #region EVENT 처리
        private void btnView_Click(object sender, EventArgs e)
        {
            //DurationTime_Check();

            DataTable dt = null;
            if (CheckField() == false) return;
            
            spdData_Sheet1.RowCount = 0;
            
            try
            {                
                this.Refresh();
                LoadingPopUp.LoadIngPopUpShow(this);
                dt = CmnFunction.oComm.GetFuncDataTable("DYNAMIC", MakeSqlString());
                
                GridColumnInit();

                if (dt.Rows.Count == 0)
                {
                    dt.Dispose();
                    LoadingPopUp.LoadingPopUpHidden();
                    CmnFunction.ShowMsgBox(RptMessages.GetMessage("STD010", GlobalVariable.gcLanguage));
                    return;
                }

                //spdData.DataSource = dt;
                
                //by John
                //1.Griid 합계 표시
                int[] rowType = spdData.RPT_DataBindingWithSubTotal(dt, 0, 1, 9, null, null, btnSort);
                //                  토탈 시작할 셀넘버, 시작 부터 서브토탈 몇개 쓸건지, 첫셀부터 몇번째 셀까지 TOTAL 적용안함

                //2. 칼럼 고정(필요하다면..)
                //spdData.Sheets[0].FrozenColumnCount = 11;

                //by John (두줄짜리이상 + 부분합계.)
                //1.Griid 합계 표시
                //int[] rowType = spdData.RPT_DataBindingWithSubTotalAndDivideRows(dt, 0, 1, 9, 10, 4, udcCUSFromToCondition1.CountFromToValue, btnSort);
                //int[] rowType = spdData.RPT_DataBindingWithSubTotalAndDivideRows(dt, 0, 1, 9, 10, 4, udcCUSCondition1.SelectCount, btnSort);
                //// 서브토탈시작할 컬럼, 서브토탈 몇개쓸껀지, 몇 컬럼까지 서브토탈 ~ 까지 안함, 자동채움시작컬럼 전컬럼, 디바이드 로우수                                                                                        
                
                //구분항목 값 생성(구분이 들어가는 위치임. 0부터 시작)                

                //3. Total부분 셀머지
                spdData.RPT_FillDataSelectiveCells("Total", 0, 10, 0, 1, true, Align.Center, VerticalAlign.Center);

                //spdData.RPT_FillColumnData(9, new string[] {"IN", "OUT", "EOH", "BOH" });

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

        private void btnExcelExport_Click(object sender, EventArgs e)
        {
            //ExcelHelper.Instance.subMakeExcel(spdData, this.lblTitle.Text, null, null);
            spdData.ExportExcel();
        }
        #endregion
    }
}
