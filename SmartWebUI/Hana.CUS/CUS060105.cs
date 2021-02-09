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
    public partial class CUS060105 : Miracom.SmartWeb.UI.Controls.udcCUSReport002
    {
        /// <summary>
        /// 클  래  스: CUS060105<br/>
        /// 클래스요약: 고객사 YIELD<br/>
        /// 작  성  자: 미라콤 김민규<br/>
        /// 최초작성일: 2008-12-05<br/>
        /// 상세  설명: 고객사 YIELD<br/>
        /// 변경  내용: <br/>
        /// </summary>
        /// 

        static DataTable global_oper_Assy = null;
        static DataTable global_oper_Test = null;

        public CUS060105()
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

            int j = 10;
            try
            {
                if (CheckFactory("ALL") == true)
                {
                    for (int i = 0; i < global_oper_Assy.Rows.Count; i++)
                    {
                        spdData.RPT_AddBasicColumn(global_oper_Assy.Rows[i][0].ToString(), 0, j, Visibles.True, Frozen.False, Align.Center, Merge.False, Formatter.String, 80);

                        spdData.RPT_AddBasicColumn("IN", 1, j, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 80);
                        j++;
                        spdData.RPT_AddBasicColumn("OUT", 1, j, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 80);
                        j++;
                        spdData.RPT_AddBasicColumn("YIELD", 1, j, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Double3, 70);
                        j++;
                        spdData.RPT_MerageHeaderColumnSpan(0, j-3, 3);
                    }
                    spdData.RPT_AddBasicColumn("ASSY TOTAL", 1, j, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.String, 70);

                    spdData.RPT_AddBasicColumn("IN", 1, j, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 70);
                    j++;
                    spdData.RPT_AddBasicColumn("OUT", 1, j, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 70);
                    j++;
                    spdData.RPT_AddBasicColumn("YIELD", 1, j, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Double3, 70);                    
                    j++;
                    spdData.RPT_MerageHeaderColumnSpan(0, j - 3, 3);

                   
                    for (int i = 0; i < global_oper_Test.Rows.Count; i++)
                    {
                        spdData.RPT_AddBasicColumn(global_oper_Test.Rows[i][0].ToString(), 0, j, Visibles.True, Frozen.False, Align.Center, Merge.False, Formatter.String, 80);

                        spdData.RPT_AddBasicColumn("IN", 1, j, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 80);
                        j++;
                        spdData.RPT_AddBasicColumn("OUT", 1, j, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 80);
                        j++;
                        spdData.RPT_AddBasicColumn("YIELD", 1, j, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Double3, 70);
                        j++;
                        spdData.RPT_MerageHeaderColumnSpan(0, j - 3, 3);
                    }

                    spdData.RPT_AddBasicColumn("TEST TOTAL", 0, j, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.String, 70);
                    
                    spdData.RPT_AddBasicColumn("IN", 1, j, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Double3, 70);
                    j++;
                    spdData.RPT_AddBasicColumn("OUT", 1, j, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Double3, 70);
                    j++;
                    spdData.RPT_AddBasicColumn("YIELD", 1, j, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Double3, 70);                    
                    j++;
                    spdData.RPT_MerageHeaderColumnSpan(0, j - 3, 3);


                    spdData.RPT_AddBasicColumn("GRAND TOTAL", 0, j, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.String, 70);

                    spdData.RPT_AddBasicColumn("IN", 1, j, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Double3, 70);
                    j++;
                    spdData.RPT_AddBasicColumn("OUT", 1, j, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Double3, 70);
                    j++;
                    spdData.RPT_AddBasicColumn("YIELD", 1, j, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Double3, 70);
                    j++;
                    spdData.RPT_MerageHeaderColumnSpan(0, j-3, 3);
                    j = 0;                    
                }
                
                if (CheckFactory(GlobalVariable.gsAssyDefaultFactory) == true)
                {                    
                    for (int i = 0; i < global_oper_Assy.Rows.Count; i++)
                    {
                        spdData.RPT_AddBasicColumn(global_oper_Assy.Rows[i][0].ToString(), 0, spdData.ActiveSheet.ColumnHeader.Columns.Count, Visibles.True, Frozen.False, Align.Center, Merge.False, Formatter.String, 80);
                        spdData.RPT_AddBasicColumn("IN", 1, spdData.ActiveSheet.ColumnHeader.Columns.Count-1, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 80);                        
                        spdData.RPT_AddBasicColumn("OUT", 1, spdData.ActiveSheet.ColumnHeader.Columns.Count, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 80);
                        spdData.RPT_AddBasicColumn("YIELD", 1, spdData.ActiveSheet.ColumnHeader.Columns.Count, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Double3, 70);                    
                        spdData.RPT_MerageHeaderColumnSpan(0, spdData.ActiveSheet.ColumnHeader.Columns.Count - 3, 3);
                    }
                    spdData.RPT_AddBasicColumn("ASSY TOTAL", 0, spdData.ActiveSheet.ColumnHeader.Columns.Count, Visibles.True, Frozen.False, Align.Left, Merge.False, Formatter.String, 70);
                    spdData.RPT_AddBasicColumn("IN", 1, spdData.ActiveSheet.ColumnHeader.Columns.Count-1, Visibles.True, Frozen.False, Align.Left, Merge.False, Formatter.Number, 70);
                    spdData.RPT_AddBasicColumn("OUT", 1, spdData.ActiveSheet.ColumnHeader.Columns.Count, Visibles.True, Frozen.False, Align.Left, Merge.False, Formatter.Number, 70);
                    spdData.RPT_AddBasicColumn("YIELD", 1, spdData.ActiveSheet.ColumnHeader.Columns.Count, Visibles.True, Frozen.False, Align.Left, Merge.False, Formatter.Double3, 70);
                    spdData.RPT_MerageHeaderColumnSpan(0, spdData.ActiveSheet.ColumnHeader.Columns.Count - 3, 3);                    
                }
                
                if (CheckFactory(GlobalVariable.gsTestDefaultFactory) == true)
                {
                    for (int i = 0; i < global_oper_Test.Rows.Count; i++)
                    {
                        spdData.RPT_AddBasicColumn(global_oper_Test.Rows[i][0].ToString(), 0, spdData.ActiveSheet.ColumnHeader.Columns.Count, Visibles.True, Frozen.False, Align.Center, Merge.False, Formatter.String, 80);
                        spdData.RPT_AddBasicColumn("IN", 1, spdData.ActiveSheet.ColumnHeader.Columns.Count-1, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 80);
                        spdData.RPT_AddBasicColumn("OUT", 1, spdData.ActiveSheet.ColumnHeader.Columns.Count, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 80);                        j++;
                        spdData.RPT_AddBasicColumn("YIELD", 1, spdData.ActiveSheet.ColumnHeader.Columns.Count, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Double3, 70);                        
                        spdData.RPT_MerageHeaderColumnSpan(0, spdData.ActiveSheet.ColumnHeader.Columns.Count - 3, 3);
                    }

                    spdData.RPT_AddBasicColumn("Test TOTAL", 0, spdData.ActiveSheet.ColumnHeader.Columns.Count, Visibles.True, Frozen.False, Align.Left, Merge.False, Formatter.String, 70);
                    spdData.RPT_AddBasicColumn("IN", 1, spdData.ActiveSheet.ColumnHeader.Columns.Count-1, Visibles.True, Frozen.False, Align.Left, Merge.False, Formatter.Number, 70);
                    spdData.RPT_AddBasicColumn("OUT", 1, spdData.ActiveSheet.ColumnHeader.Columns.Count, Visibles.True, Frozen.False, Align.Left, Merge.False, Formatter.Number, 70);
                    spdData.RPT_AddBasicColumn("YIELD", 1, spdData.ActiveSheet.ColumnHeader.Columns.Count, Visibles.True, Frozen.False, Align.Left, Merge.False, Formatter.Double3, 70);
                    spdData.RPT_MerageHeaderColumnSpan(0, spdData.ActiveSheet.ColumnHeader.Columns.Count - 3, 3);                    
                }

                spdData.RPT_MerageHeaderRowSpan(0, 0, 2);
                spdData.RPT_MerageHeaderRowSpan(0, 1, 2);
                spdData.RPT_MerageHeaderRowSpan(0, 2, 2);
                spdData.RPT_MerageHeaderRowSpan(0, 3, 2);
                spdData.RPT_MerageHeaderRowSpan(0, 4, 2);
                spdData.RPT_MerageHeaderRowSpan(0, 5, 2);
                spdData.RPT_MerageHeaderRowSpan(0, 6, 2);
                spdData.RPT_MerageHeaderRowSpan(0, 7, 2);
                spdData.RPT_MerageHeaderRowSpan(0, 8, 2);
                spdData.RPT_MerageHeaderRowSpan(0, 9, 2);                
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
                return;
            }
            spdData.RPT_ColumnConfigFromTable(btnSort); //Group항목이 있을경우 반드시 선언해줄것.
        }

        /// <summary>
        /// 3. Group By 정의
        /// </summary>
        private void SortInit()
        {
            ((udcTableForm)(this.btnSort.BindingForm)).AddRow("Customer", "MAT_GRP_1", "MAT_GRP_1", "NVL(MAT_GRP_1,0)", true);
            ((udcTableForm)(this.btnSort.BindingForm)).AddRow("Family", "MAT_GRP_2", "MAT_GRP_2", "MAT_GRP_2", false);
            ((udcTableForm)(this.btnSort.BindingForm)).AddRow("Package", "MAT_GRP_3", "MAT_GRP_3", "NVL(MAT_GRP_3,0)", true);
            ((udcTableForm)(this.btnSort.BindingForm)).AddRow("Type1", "MAT_GRP_4", "MAT_GRP_4", "MAT_GRP_4", false);
            ((udcTableForm)(this.btnSort.BindingForm)).AddRow("Type2", "MAT_GRP_5", "MAT_GRP_5", "MAT_GRP_5", false);
            ((udcTableForm)(this.btnSort.BindingForm)).AddRow("LD Count", "MAT_GRP_6", "MAT_GRP_6", "MAT_GRP_6", false);
            ((udcTableForm)(this.btnSort.BindingForm)).AddRow("Density", "MAT_GRP_7", "MAT_GRP_7", "MAT_GRP_7", false);
            ((udcTableForm)(this.btnSort.BindingForm)).AddRow("Generation", "MAT_GRP_8", "MAT_GRP_8", "MAT_GRP_8", false);
            ((udcTableForm)(this.btnSort.BindingForm)).AddRow("Product", "MAT_CMF_7", "A.MAT_CMF_7", "MAT_CMF_7", true);
            ((udcTableForm)(this.btnSort.BindingForm)).AddRow("Lot ID", "LOT_ID", "B.LOT_ID", "LOT_ID", true);     
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
            string sStart_Tran_Time = cdvFromToDate.Start_Tran_Time.ToString();
            string sEnd_Tran_Time = cdvFromToDate.End_Tran_Time.ToString();
            
            string bbbb = null;
            bbbb = string.Empty;
                        
            udcTableForm tableForm = (udcTableForm)btnSort.BindingForm;
            QueryCond1 = tableForm.SelectedValueToQueryContainNull;            
            QueryCond2 = tableForm.SelectedValue2ToQueryContainNull;
            QueryCond3 = tableForm.SelectedValue3ToQueryContainNull;


            strSqlString.AppendFormat("       SELECT {0} " + "\n", QueryCond3);

            if(CheckFactory("ALL") == true)
            {
                for (int i = 0; i < global_oper_Assy.Rows.Count; i++)
                {
                    strSqlString.Append("            , SUM(NVL(IN_A" + i + ",0)) AS IN_QTY_A ");
                    strSqlString.Append("            , SUM(NVL(OUT_A" + i + ",0)) AS OUT_QTY_A ");
                    strSqlString.Append("            , DECODE(SUM(NVL(IN_A" + i + ",0)), 0, 0, ROUND(SUM(NVL(OUT_A" + i + ",0))/SUM(NVL(IN_A" + i + ",0))*100,3)) AS YIELD " + "\n");
                }

                strSqlString.Append("            , SUM(NVL(HMKA1,0)) AS IN_A " + "\n");
                strSqlString.Append("            , SUM(NVL(HMKA2,0)) AS OUT_A " + "\n");
                strSqlString.Append("            , DECODE(SUM(NVL(HMKA1,0)), 0, 0, ROUND(SUM(NVL(HMKA2,0))/SUM(NVL(HMKA1,0))*100,3)) AS YIELD " + "\n");

                for (int i = 0; i < global_oper_Test.Rows.Count; i++)
                {
                    strSqlString.Append("            , SUM(NVL(IN_T" + i + ",0)) AS IN_QTY_T ");
                    strSqlString.Append("            , SUM(NVL(OUT_T" + i + ",0)) AS OUT_QTY_T ");
                    strSqlString.Append("            , DECODE(SUM(NVL(IN_T" + i + ",0)), 0, 0, ROUND(SUM(NVL(OUT_T" + i + ",0))/SUM(NVL(IN_T" + i + ",0))*100,3)) AS YIELD " + "\n");
                }

                strSqlString.Append("            , SUM(NVL(HMKT1,0)) AS IN_T " + "\n");
                strSqlString.Append("            , SUM(NVL(HMKT2,0)) AS OUT_T " + "\n");
                strSqlString.Append("            , DECODE(SUM(NVL(HMKT1,0)), 0, 0, ROUND(SUM(NVL(HMKT2,0))/SUM(NVL(HMKT1,0))*100,3)) AS YIELD " + "\n");

                strSqlString.Append("            , SUM(NVL(TOTAL_IN,0)) AS IN_TOTAL " + "\n");
                strSqlString.Append("            , SUM(NVL(TOTAL_OUT,0)) AS OUT_TOTAL " + "\n");
                strSqlString.Append("            , DECODE(SUM(NVL(TOTAL_IN,0)), 0, 0, ROUND(SUM(NVL(TOTAL_OUT,0))/SUM(NVL(TOTAL_IN,0))*100,3)) AS TOTAL_YIELD" + "\n");
            }
           
            if (CheckFactory(GlobalVariable.gsAssyDefaultFactory) == true)
            {
                for (int i = 0; i < global_oper_Assy.Rows.Count; i++)
                {
                    strSqlString.Append("            , SUM(NVL(IN_A" + i + ",0)) AS IN_QTY_A ");
                    strSqlString.Append("            , SUM(NVL(OUT_A" + i + ",0)) AS OUT_QTY_A ");
                    strSqlString.Append("            , DECODE(SUM(NVL(IN_A" + i + ",0)), 0, 0, ROUND(SUM(NVL(OUT_A" + i + ",0))/SUM(NVL(IN_A" + i + ",0))*100,3)) AS YIELD " + "\n");                    
                }

                strSqlString.Append("            , SUM(NVL(HMKA1,0)) AS IN_A " + "\n");
                strSqlString.Append("            , SUM(NVL(HMKA2,0)) AS OUT_A " + "\n");
                strSqlString.Append("            , DECODE(SUM(NVL(HMKA1,0)), 0, 0, ROUND(SUM(NVL(HMKA2,0))/SUM(NVL(HMKA1,0))*100,3)) AS YIELD " + "\n");
            }
            
            if (CheckFactory(GlobalVariable.gsTestDefaultFactory) == true)
            {
                for (int i = 0; i < global_oper_Test.Rows.Count; i++)
                {
                    strSqlString.Append("            , SUM(NVL(IN_T" + i + ",0)) AS IN_QTY_T ");
                    strSqlString.Append("            , SUM(NVL(OUT_T" + i + ",0)) AS OUT_QTY_T ");
                    strSqlString.Append("            , DECODE(SUM(NVL(IN_T" + i + ",0)), 0, 0, ROUND(SUM(NVL(OUT_T" + i + ",0))/SUM(NVL(IN_T" + i + ",0))*100,3)) AS YIELD " + "\n");                    
                }

                strSqlString.Append("            , SUM(NVL(HMKT1,0)) AS IN_T " + "\n");
                strSqlString.Append("            , SUM(NVL(HMKT2,0)) AS OUT_T " + "\n");
                strSqlString.Append("            , DECODE(SUM(NVL(HMKT1,0)), 0, 0, ROUND(SUM(NVL(HMKT2,0))/SUM(NVL(HMKT1,0))*100,3)) AS YIELD " + "\n");
            }            

            strSqlString.Append("         FROM (  " + "\n");
            strSqlString.AppendFormat("              SELECT {0} " + "\n", QueryCond1);

            if(CheckFactory("ALL") == true)
            {
                for (int i = 0; i < global_oper_Assy.Rows.Count; i++)
                {
                    strSqlString.Append("                   , SUM(DECODE(OPER_GRP_1, '" + global_oper_Assy.Rows[i][0] + "', OPER_IN_QTY_1, 0)) AS IN_A" + i + "\n");
                    strSqlString.Append("                   , SUM(DECODE(OPER_GRP_1, '" + global_oper_Assy.Rows[i][0] + "', END_QTY_1, 0)) AS OUT_A" + i + "\n");
                }
                strSqlString.Append("                   , DECODE(FACTORY, '" + GlobalVariable.gsAssyDefaultFactory + "', OPER_IN_QTY_1, 0) AS HMKA1 " + "\n");
                strSqlString.Append("                   , DECODE(FACTORY, '" + GlobalVariable.gsAssyDefaultFactory + "', END_QTY_1, 0) AS HMKA2 " + "\n");

                for (int i = 0; i < global_oper_Test.Rows.Count; i++)
                {
                    strSqlString.Append("                   , SUM(DECODE(OPER_GRP_1, '" + global_oper_Test.Rows[i][0] + "', OPER_IN_QTY_1, 0)) AS IN_T" + (i) + "\n");
                    strSqlString.Append("                   , SUM(DECODE(OPER_GRP_1, '" + global_oper_Test.Rows[i][0] + "', END_QTY_1, 0)) AS OUT_T" + (i) + "\n");
                }
                strSqlString.Append("                   , DECODE(FACTORY, '" + GlobalVariable.gsTestDefaultFactory + "', OPER_IN_QTY_1, 0) HMKT1 " + "\n");
                strSqlString.Append("                   , DECODE(FACTORY, '" + GlobalVariable.gsTestDefaultFactory + "', END_QTY_1, 0) HMKT2 " + "\n");

                strSqlString.Append("                   ,SUM(CASE WHEN FACTORY " + cdvFactory.SelectedValueToQueryString + "\n");
                strSqlString.Append("                             THEN NVL(OPER_IN_QTY_1,0) " + "\n");
                strSqlString.Append("                             ELSE 0 END) AS TOTAL_IN " + "\n");
                strSqlString.Append("                   ,SUM(CASE WHEN FACTORY " + cdvFactory.SelectedValueToQueryString + "\n");
                strSqlString.Append("                             THEN NVL(END_QTY_1,0) " + "\n");
                strSqlString.Append("                             ELSE 0 END) TOTAL_OUT " + "\n");

                }
            
            if (CheckFactory(GlobalVariable.gsAssyDefaultFactory) == true)
            {
                for (int i = 0; i < global_oper_Assy.Rows.Count; i++)
                {
                    strSqlString.Append("                   , SUM(DECODE(OPER_GRP_1, '" + global_oper_Assy.Rows[i][0] + "', OPER_IN_QTY_1, 0)) AS IN_A" + i + "\n");
                    strSqlString.Append("                   , SUM(DECODE(OPER_GRP_1, '" + global_oper_Assy.Rows[i][0] + "', END_QTY_1, 0)) AS OUT_A" + i + "\n");
                }
                //strSqlString.Append("                   , DECODE(FACTORY, '" + GlobalVariable.gsAssyDefaultFactory + "', OPER_IN_QTY_1, 0) AS HMKA1 " + "\n");
                //strSqlString.Append("                   , DECODE(FACTORY, '" + GlobalVariable.gsAssyDefaultFactory + "', END_QTY_1, 0) AS HMKA2 " + "\n");

                strSqlString.Append("                   , CASE WHEN FACTORY = '" + GlobalVariable.gsAssyDefaultFactory + "' AND OPER_GRP_1 NOT IN ('-') " + "\n");
                strSqlString.Append("                          THEN OPER_IN_QTY_1" + "\n");
                strSqlString.Append("                          ELSE 0" + "\n");
                strSqlString.Append("                     END AS HMKA1" + "\n");
                strSqlString.Append("                   , CASE WHEN FACTORY = '" + GlobalVariable.gsAssyDefaultFactory + "' AND OPER_GRP_1 NOT IN ('-') " + "\n");
                strSqlString.Append("                          THEN END_QTY_1" + "\n");
                strSqlString.Append("                          ELSE 0" + "\n");
                strSqlString.Append("                     END AS HMKA2" + "\n");

            }
            
            
            if (CheckFactory(GlobalVariable.gsTestDefaultFactory) == true)
            {
                for (int i = 0; i < global_oper_Test.Rows.Count; i++)
                {
                    strSqlString.Append("                   , DECODE(OPER_GRP_1, '" + global_oper_Test.Rows[i][0] + "', OPER_IN_QTY_1, 0) AS IN_T" + i + "\n");
                    strSqlString.Append("                   , DECODE(OPER_GRP_1, '" + global_oper_Test.Rows[i][0] + "', END_QTY_1, 0) AS OUT_T" + i + "\n");
                }
                strSqlString.Append("                   , DECODE(FACTORY, '" + GlobalVariable.gsTestDefaultFactory + "', OPER_IN_QTY_1, 0) AS HMKT1 " + "\n");
                strSqlString.Append("                   , DECODE(FACTORY, '" + GlobalVariable.gsTestDefaultFactory + "', END_QTY_1, 0) AS HMKT2 " + "\n");
            }           


            strSqlString.Append("                FROM (  " + "\n");
            strSqlString.AppendFormat("                      SELECT {0}, C.OPER_GRP_1  " + "\n", QueryCond2);
            strSqlString.Append("                           , OPER_IN_QTY_1, END_QTY_1, B.FACTORY        " + "\n");
            strSqlString.Append("                        FROM MWIPMATDEF A, RSUMWIPLTH B, MWIPOPRDEF C       " + "\n");
            strSqlString.Append("                       WHERE B.FACTORY " + cdvFactory.SelectedValueToQueryString + "\n");
            strSqlString.Append("                         AND B.FACTORY = A.FACTORY(+)  " + "\n");
            strSqlString.Append("                         AND B.FACTORY = C.FACTORY(+)  " + "\n");
            strSqlString.Append("                         AND B.MAT_ID = A.MAT_ID(+)  " + "\n");
            strSqlString.Append("                         AND B.OPER = C.OPER  " + "\n");            
            strSqlString.Append("                         AND C.OPER_GRP_1(+) NOT IN ('-')  " + "\n");
            strSqlString.Append("                         AND A.MAT_VER(+) = 1  " + "\n");
            strSqlString.Append("                         AND B.LOT_ID IN (" + "\n");
            strSqlString.Append("                                         SELECT DISTINCT LOT_ID " + "\n");
            strSqlString.Append("                                           FROM VWIPSHPLOT" + "\n");
            strSqlString.Append("                                          WHERE 1=1" + "\n");
            strSqlString.AppendFormat("                                            AND TRAN_TIME BETWEEN '{0}' AND '{1}'" + "\n", sStart_Tran_Time, sEnd_Tran_Time);
            strSqlString.Append("                                            AND FROM_FACTORY " + cdvFactory.SelectedValueToQueryString + "\n");
            strSqlString.Append("                                            AND FROM_OPER IN ('AZ010','AZ009','TZ010','EZ010','F0000')" + "\n");
            strSqlString.Append("                                            AND TO_OPER = ' ' " + "\n");
            strSqlString.Append("                                         )" + "\n");

            if (GlobalVariable.gsUserGroup == "ADMIN_GROUP" || GlobalVariable.gsUserGroup == "HANA_ADMIN_GROUP")
            {
                udcWIPCondition1.Enabled = true;
                
                if (udcWIPCondition1.Text != "ALL" && udcWIPCondition1.Text != "")
                    strSqlString.AppendFormat("                         AND A.MAT_GRP_1 {0}" + "\n", udcWIPCondition1.SelectedValueToQueryString);
            }
            else
            {
                strSqlString.Append("                         AND A.MAT_GRP_1 = '" + GlobalVariable.gsCustomer + "'" + "\n");
            }

            //상세 조회에 따른 SQL문 생성                        
            //if (udcWIPCondition1.Text != "ALL" && udcWIPCondition1.Text != "")
            //    strSqlString.AppendFormat("                             AND A.MAT_GRP_1 {0}" + "\n", udcWIPCondition2.SelectedValueToQueryString);

            if (udcWIPCondition2.Text != "ALL" && udcWIPCondition2.Text != "")
                strSqlString.AppendFormat("                         AND A.MAT_GRP_2 {0}" + "\n", udcWIPCondition2.SelectedValueToQueryString);

            if (udcWIPCondition3.Text != "ALL" && udcWIPCondition3.Text != "")
                strSqlString.AppendFormat("                         AND A.MAT_GRP_3 {0} " + "\n", udcWIPCondition3.SelectedValueToQueryString);

            if (udcWIPCondition4.Text != "ALL" && udcWIPCondition4.Text != "")
                strSqlString.AppendFormat("                         AND A.MAT_GRP_4 {0} " + "\n", udcWIPCondition4.SelectedValueToQueryString);

            if (udcWIPCondition5.Text != "ALL" && udcWIPCondition5.Text != "")
                strSqlString.AppendFormat("                         AND A.MAT_GRP_5 {0} " + "\n", udcWIPCondition5.SelectedValueToQueryString);

            if (udcWIPCondition6.Text != "ALL" && udcWIPCondition6.Text != "")
                strSqlString.AppendFormat("                         AND A.MAT_GRP_6 {0} " + "\n", udcWIPCondition6.SelectedValueToQueryString);

            if (udcWIPCondition7.Text != "ALL" && udcWIPCondition7.Text != "")
                strSqlString.AppendFormat("                         AND A.MAT_GRP_7 {0} " + "\n", udcWIPCondition7.SelectedValueToQueryString);

            if (udcWIPCondition8.Text != "ALL" && udcWIPCondition8.Text != "")
                strSqlString.AppendFormat("                         AND A.MAT_GRP_8 {0} " + "\n", udcWIPCondition8.SelectedValueToQueryString);

            if (udcWIPCondition9.Text != "ALL" && udcWIPCondition9.Text != "")
                strSqlString.AppendFormat("                         AND A.MAT_GRP_9 {0} " + "\n", udcWIPCondition9.SelectedValueToQueryString);

            strSqlString.Append("                     )       " + "\n");
            strSqlString.AppendFormat("            GROUP BY {0}, OPER_GRP_1, END_QTY_1 , OPER_IN_QTY_1, FACTORY         " + "\n", QueryCond1);
            strSqlString.Append("              )          " + "\n");
            strSqlString.AppendFormat("     GROUP BY {0} " + "\n", QueryCond1);
            strSqlString.AppendFormat("     ORDER BY {0} " + "\n", QueryCond1);

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
            strSqlString.Append("                 AND OPER_GRP_1 NOT IN ('-') " + "\n");
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
            strSqlString.Append("                 AND OPER_GRP_1 NOT IN ('-') " + "\n");
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

                //그루핑 순서 1순위만 SubTotal을 사용하기 위해서 첫번째 값을 가져옴
                int sub = ((udcTableForm)(this.btnSort.BindingForm)).GetCheckedValue(2); 
                
                //by John
                //1.Griid 합계 표시
                int[] rowType = spdData.RPT_DataBindingWithSubTotal(dt, 0, sub, 9, null, null, btnSort);
                //                  토탈 시작할 셀넘버, 시작 부터 서브토탈 몇개 쓸건지, 첫셀부터 몇번째 셀까지 TOTAL 적용안함

                //3. Total부분 셀머지
                spdData.RPT_FillDataSelectiveCells("Total", 0, 10, 0, 1, true, Align.Center, VerticalAlign.Center);
                
                //4. Column Auto Fit
                spdData.RPT_AutoFit(false);

                //YIELD 부분의  TOTAL값 및 SUB TOTAL을 계산하지 않고 직접 계산 

                string subtotal = null;

                for (int i = 0; i < spdData.ActiveSheet.Columns.Count; i++)
                {
                    if (spdData.ActiveSheet.ColumnHeader.Cells[1, i].Text == "YIELD")
                    {
                        if (Convert.ToInt32(spdData.ActiveSheet.Cells[0, i - 1].Value) == 0 || Convert.ToInt32(spdData.ActiveSheet.Cells[0, i - 2].Value) == 0)
                        {
                            spdData.ActiveSheet.Cells[0, i].Value = 0;
                        }
                        else
                        {
                            spdData.ActiveSheet.Cells[0, i].Value = ((Convert.ToDouble(spdData.ActiveSheet.Cells[0, i - 1].Value) / Convert.ToDouble(spdData.ActiveSheet.Cells[0, i - 2].Value)) * 100).ToString();
                        }
                        

                        for (int j = 0; j < spdData.ActiveSheet.Rows.Count; j++)
                        {
                            for (int k = 0; k < sub; k++)
                            {
                                if (spdData.ActiveSheet.Cells[j, k].Value != null)
                                    subtotal = spdData.ActiveSheet.Cells[j, k].Value.ToString();

                                subtotal.Trim();
                                if (subtotal.Length > 5)
                                {
                                    if (subtotal.Substring(subtotal.Length - 5, 5) == "Total")
                                    {
                                        if (Convert.ToInt32(spdData.ActiveSheet.Cells[j, i - 2].Value) == 0 || Convert.ToInt32(spdData.ActiveSheet.Cells[j, i - 1].Value) == 0)
                                        {
                                            spdData.ActiveSheet.Cells[j, i].Value = 0;
                                        }
                                        else
                                        {
                                            spdData.ActiveSheet.Cells[j, i].Value = ((Convert.ToDouble(spdData.ActiveSheet.Cells[j, i - 1].Value) / Convert.ToDouble(spdData.ActiveSheet.Cells[j, i - 2].Value)) * 100).ToString();
                                        }
                                    }
                                } // if (subtotal.Length > 5)
                            } //for (int k = sub + 1; k > 0; k--)
                        } //for (int j = 0; j < spdData.ActiveSheet.Rows.Count; j++)
                    } // if (spdData.ActiveSheet.Columns[i].Label == "Progress rate")
                }  

                
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

        //private void spdData_CellClick(object sender, FarPoint.Win.Spread.CellClickEventArgs e)
        //{
        //    if (e.ColumnHeader == true && ( e.Column > 9 ) && (e.Column < spdData.ActiveSheet.ColumnHeader.Columns.Count) )
        //    {

        //        string Query = "SELECT OPER_DESC FROM MWIPOPRDEF WHERE OPER = '" + spdData.ActiveSheet.ColumnHeader.Cells[0, e.Column].Text + "' AND ROWNUM=1";
        //        DataTable dt = CmnFunction.oComm.GetFuncDataTable("DYNAMIC", Query);

        //        ToolTip desc = new ToolTip();
        //        desc.Show(dt.Rows[0][0].ToString(), spdData, e.X + 10, e.Y, 1000);
        //    }
        //}
        
    }
}
