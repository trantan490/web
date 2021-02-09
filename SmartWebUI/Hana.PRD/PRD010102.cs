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
    public partial class PRD010102 : Miracom.SmartWeb.UI.Controls.udcCUSReport002
    {

        private int intTime = 60;

        #region " PRD010102 : Program Initial "

        public PRD010102()
        {
            InitializeComponent();
            SortInit();
            GridColumnInit();
            cdvFactory.Text = "HMKA1, HMKT1, FGS";
        }

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

        #endregion


        #region " GridColumnInit : Sheet Title 설정 "

        /// <summary>
        /// 2. 헤더 생성
        /// </summary>
        private void GridColumnInit()
        {
            StringBuilder strSqlString = new StringBuilder();
            DataTable dt = null;
            int iIdx;
            
            try
            {
                Cursor.Current = Cursors.WaitCursor;

                spdData.ActiveSheet.ColumnHeader.Rows.Count = 1;
                spdData.RPT_ColumnInit();

                iIdx = 0;
                spdData.RPT_AddBasicColumn("PIN_TYPE", 0, iIdx, Visibles.True, Frozen.False, Align.Center, Merge.True, Formatter.String, 80);

                // 2009-05-27-정비재 : 공정을 조회하여, Sheet에 공정을 설정한다.
                strSqlString.Append("SELECT A.OPER " + "\n");
                strSqlString.Append("  FROM ( " + "\n");
                strSqlString.Append("        SELECT CASE SUBSTR(A.OPER, 1, 1) WHEN 'A' THEN 1000 + ROWNUM " + "\n");
                strSqlString.Append("                                         WHEN 'F' THEN 2000 + ROWNUM " + "\n");
                strSqlString.Append("                                         WHEN 'T' THEN 3000 + ROWNUM " + "\n");
                strSqlString.Append("               END AS SEQ " + "\n");
                strSqlString.Append("             , A.OPER AS OPER " + "\n");
                strSqlString.Append("          FROM MWIPOPRDEF A " + "\n");
                strSqlString.Append("         WHERE A.FACTORY IN ('" + GlobalVariable.gsAssyDefaultFactory + "', '" + GlobalVariable.gsTestDefaultFactory + "', 'FGS') " + "\n");
                strSqlString.Append("           AND SUBSTR(A.OPER, 1, 1) IN ('A', 'T', 'F') " + "\n");
                strSqlString.Append("           AND A.OPER NOT IN ('A000N', 'T000N', 'F000N') " + "\n");
                strSqlString.Append("       ) A " + "\n");
                strSqlString.Append(" ORDER BY A.SEQ ASC " + "\n");

                dt = CmnFunction.oComm.GetFuncDataTable("DYNAMIC", strSqlString.ToString());

                if (dt.Rows.Count == 0)
                {
                    dt.Dispose();
                }
                else
                {
                    for (int iRow = 0; iRow < dt.Rows.Count; iRow++)
                    {
                        iIdx += 1;
                        spdData.RPT_AddBasicColumn(dt.Rows[iRow][0].ToString(), 0, iIdx, Visibles.True, Frozen.False, Align.Center, Merge.True, Formatter.String, 80);
                        spdData.RPT_AddBasicColumn("WAIT", 1, iIdx, Visibles.True, Frozen.False, Align.Right, Merge.True, Formatter.Number, 40);
                        iIdx += 1;
                        spdData.RPT_AddBasicColumn("PROC", 1, iIdx, Visibles.True, Frozen.False, Align.Right, Merge.True, Formatter.Number, 40);

                        spdData.RPT_MerageHeaderColumnSpan(0, iIdx - 1, 2);
                    }
                }
                spdData.RPT_MerageHeaderRowSpan(0, 0, 2);
            }
            catch (Exception ex)
            {
                CmnFunction.ShowMsgBox(ex.Message);
                LoadingPopUp.LoadingPopUpHidden();
            }
            finally
            {
                Cursor.Current = Cursors.Default;
            }
        }

        #endregion


        #region " SortInit : Group By 설정 "

        /// <summary>
        /// 3. Group By 정의
        /// </summary>
        private void SortInit()
        {
            try
            {
                ((udcTableForm)(this.btnSort.BindingForm)).Clear();
                ((udcTableForm)(this.btnSort.BindingForm)).AddRow("CUSTOMER", "B.MAT_GRP_1", "MAT_GRP_1", "NVL((SELECT DATA_1 FROM MGCMTBLDAT WHERE TABLE_NAME = 'H_CUSTOMER' AND KEY_1 = MAT_GRP_1 AND ROWNUM=1), '-') AS CUSTOMER", true);
                ((udcTableForm)(this.btnSort.BindingForm)).AddRow("PIN_TYPE", "B.MAT_CMF_10", "PIN_TYPE", "B.MAT_CMF_10", true);
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
                LoadingPopUp.LoadingPopUpHidden();
            }
        }

        #endregion

        
        #region " SetAvgVertical : Yield의 평균값을 계산하는 함수 "

        public void SetAvgVertical(int nSampleNormalRowPos, int nColPos, bool bWithNull)
        {
            double sum = 0;
            double totalSum = 0;

            double divide = 0;
            double totalDivide = 0;

            Color color = spdData.ActiveSheet.Cells[nSampleNormalRowPos, nColPos].BackColor;

            for (int i = 1; i < spdData.ActiveSheet.Rows.Count; i++)
            {
                if (spdData.ActiveSheet.Cells[i, nColPos].BackColor == color)
                {
                    sum += Convert.ToDouble(spdData.ActiveSheet.Cells[i, nColPos].Value);

                    if (!bWithNull && (spdData.ActiveSheet.Cells[i, nColPos].Value == null || spdData.ActiveSheet.Cells[i, nColPos].Value.ToString().Trim() == ""))
                    {
                        continue;
                    }
                    divide += 1;
                }
                else
                {
                    if (divide != 0)
                    {
                        spdData.ActiveSheet.Cells[i, nColPos].Value = sum / divide;
                    }
                    totalSum += sum;
                    totalDivide += divide;

                    sum = 0;
                    divide = 0;
                }
            }
            if (totalDivide != 0)
            {
                spdData.ActiveSheet.Cells[0, nColPos].Value = totalSum / totalDivide;
            }
            spdData.ActiveSheet.Cells[spdData.ActiveSheet.Rows.Count - 1, nColPos].Value = totalSum / totalDivide;

            return;
        }

        public void SetTotalAvgVertical(int nBasisCount, int nColPos, bool bWithNull)
        {
            double dSubTotalSum2 = 0;
            double dSubTotalDivide2 = 0;
            double dSubTotalSum3 = 0;
            double dSubTotalDivide3 = 0;
            double dSubTotalSum4 = 0;
            double dSubTotalDivide4 = 0;
            double dSubTotalSum5 = 0;
            double dSubTotalDivide5 = 0;
            double dSubTotalSum6 = 0;
            double dSubTotalDivide6 = 0;
            double dSubTotalSum7 = 0;
            double dSubTotalDivide7 = 0;
            double dSubTotalSum8 = 0;
            double dSubTotalDivide8 = 0;
            
            // 2009-03-27-정비재 : sub total의 색상을 설정
            Color color = Color.FromArgb(((System.Byte)(255)), ((System.Byte)(222)), ((System.Byte)(236)), ((System.Byte)(242)));
                        
            // Row Loop
            for (int iRow = 1; iRow < spdData.ActiveSheet.Rows.Count; iRow++)
            {
                if (spdData.ActiveSheet.Cells[iRow, nColPos].BackColor == color)
                {
                    if(nBasisCount == 2)
                    {
                        // Cell Group 2
                        if (spdData.ActiveSheet.Cells[iRow, 0].Value != null
                            && spdData.ActiveSheet.Cells[iRow, 1].Value != null
                            && spdData.ActiveSheet.Cells[iRow, 2].Value == null)
                        {
                            if (!bWithNull && (spdData.ActiveSheet.Cells[iRow, nColPos].Value == null || spdData.ActiveSheet.Cells[iRow, nColPos].Text.Trim() == ""))
                            {

                            }
                            else
                            {
                                dSubTotalDivide2 += 1;
                                dSubTotalSum2 += Convert.ToDouble(spdData.ActiveSheet.Cells[iRow, nColPos].Value);
                            }
                        }
                        else if (spdData.ActiveSheet.Cells[iRow, 0].Value != null
                            && spdData.ActiveSheet.Cells[iRow, 1].Value == null
                            && spdData.ActiveSheet.Cells[iRow, 2].Value == null)
                        {
                            if (dSubTotalDivide2 != 0)
                            {
                                spdData.ActiveSheet.Cells[iRow, nColPos].Value = dSubTotalSum2 / dSubTotalDivide2;
                            }
                            dSubTotalDivide2 = 0;
                            dSubTotalSum2 = 0;
                        }
                    }
                    else if (nBasisCount == 3)
                    {
                        // Cell Group 2
                        if (spdData.ActiveSheet.Cells[iRow, 0].Value != null
                            && spdData.ActiveSheet.Cells[iRow, 1].Value != null
                            && spdData.ActiveSheet.Cells[iRow, 2].Value != null
                            && spdData.ActiveSheet.Cells[iRow, 3].Value == null)
                        {
                            if (!bWithNull && (spdData.ActiveSheet.Cells[iRow, nColPos].Value == null || spdData.ActiveSheet.Cells[iRow, nColPos].Text.Trim() == ""))
                            {

                            }
                            else
                            {
                                dSubTotalDivide2 += 1;
                                dSubTotalSum2 += Convert.ToDouble(spdData.ActiveSheet.Cells[iRow, nColPos].Value);
                            }
                        }
                        else if (spdData.ActiveSheet.Cells[iRow, 0].Value != null
                            && spdData.ActiveSheet.Cells[iRow, 1].Value != null
                            && spdData.ActiveSheet.Cells[iRow, 2].Value == null
                            && spdData.ActiveSheet.Cells[iRow, 3].Value == null)
                        {
                            if (dSubTotalDivide2 != 0)
                            {
                                spdData.ActiveSheet.Cells[iRow, nColPos].Value = dSubTotalSum2 / dSubTotalDivide2;
                            }
                            dSubTotalDivide2 = 0;
                            dSubTotalSum2 = 0;
                        }

                        // Cell Group 3
                        if (spdData.ActiveSheet.Cells[iRow, 0].Value != null
                            && spdData.ActiveSheet.Cells[iRow, 1].Value != null
                            && spdData.ActiveSheet.Cells[iRow, 2].Value == null
                            && spdData.ActiveSheet.Cells[iRow, 3].Value == null)
                        {
                            if (!bWithNull && (spdData.ActiveSheet.Cells[iRow, nColPos].Value == null || spdData.ActiveSheet.Cells[iRow, nColPos].Text.Trim() == ""))
                            {

                            }
                            else
                            {
                                dSubTotalDivide3 += 1;
                                dSubTotalSum3 += Convert.ToDouble(spdData.ActiveSheet.Cells[iRow, nColPos].Value);
                            }
                        }
                        else if (spdData.ActiveSheet.Cells[iRow, 0].Value != null
                            && spdData.ActiveSheet.Cells[iRow, 1].Value == null
                            && spdData.ActiveSheet.Cells[iRow, 2].Value == null
                            && spdData.ActiveSheet.Cells[iRow, 3].Value == null)
                        {
                            if (dSubTotalDivide3 != 0)
                            {
                                spdData.ActiveSheet.Cells[iRow, nColPos].Value = dSubTotalSum3 / dSubTotalDivide3;
                            }
                            dSubTotalDivide3 = 0;
                            dSubTotalSum3 = 0;
                        }
                    }
                    else if (nBasisCount == 4)
                    {
                        // Cell Group 2
                        if (spdData.ActiveSheet.Cells[iRow, 0].Value != null
                            && spdData.ActiveSheet.Cells[iRow, 1].Value != null
                            && spdData.ActiveSheet.Cells[iRow, 2].Value != null
                            && spdData.ActiveSheet.Cells[iRow, 3].Value != null
                            && spdData.ActiveSheet.Cells[iRow, 4].Value == null)
                        {
                            if (!bWithNull && (spdData.ActiveSheet.Cells[iRow, nColPos].Value == null || spdData.ActiveSheet.Cells[iRow, nColPos].Text.Trim() == ""))
                            {

                            }
                            else
                            {
                                dSubTotalDivide2 += 1;
                                dSubTotalSum2 += Convert.ToDouble(spdData.ActiveSheet.Cells[iRow, nColPos].Value);
                            }
                        }
                        else if (spdData.ActiveSheet.Cells[iRow, 0].Value != null
                            && spdData.ActiveSheet.Cells[iRow, 1].Value != null
                            && spdData.ActiveSheet.Cells[iRow, 2].Value != null
                            && spdData.ActiveSheet.Cells[iRow, 3].Value == null
                            && spdData.ActiveSheet.Cells[iRow, 4].Value == null)
                        {
                            if (dSubTotalDivide2 != 0)
                            {
                                spdData.ActiveSheet.Cells[iRow, nColPos].Value = dSubTotalSum2 / dSubTotalDivide2;
                            }
                            dSubTotalDivide2 = 0;
                            dSubTotalSum2 = 0;
                        }

                        // Cell Group 3
                        if (spdData.ActiveSheet.Cells[iRow, 0].Value != null
                            && spdData.ActiveSheet.Cells[iRow, 1].Value != null
                            && spdData.ActiveSheet.Cells[iRow, 2].Value != null
                            && spdData.ActiveSheet.Cells[iRow, 3].Value == null
                            && spdData.ActiveSheet.Cells[iRow, 4].Value == null)
                        {
                            if (!bWithNull && (spdData.ActiveSheet.Cells[iRow, nColPos].Value == null || spdData.ActiveSheet.Cells[iRow, nColPos].Text.Trim() == ""))
                            {

                            }
                            else
                            {
                                dSubTotalDivide3 += 1;
                                dSubTotalSum3 += Convert.ToDouble(spdData.ActiveSheet.Cells[iRow, nColPos].Value);
                            }
                        }
                        else if (spdData.ActiveSheet.Cells[iRow, 0].Value != null
                            && spdData.ActiveSheet.Cells[iRow, 1].Value != null
                            && spdData.ActiveSheet.Cells[iRow, 2].Value == null
                            && spdData.ActiveSheet.Cells[iRow, 3].Value == null
                            && spdData.ActiveSheet.Cells[iRow, 4].Value == null)
                        {
                            if (dSubTotalDivide3 != 0)
                            {
                                spdData.ActiveSheet.Cells[iRow, nColPos].Value = dSubTotalSum3 / dSubTotalDivide3;
                            }
                            dSubTotalDivide3 = 0;
                            dSubTotalSum3 = 0;
                        }

                        // Cell Group 4
                        if (spdData.ActiveSheet.Cells[iRow, 0].Value != null
                            && spdData.ActiveSheet.Cells[iRow, 1].Value != null
                            && spdData.ActiveSheet.Cells[iRow, 2].Value == null
                            && spdData.ActiveSheet.Cells[iRow, 3].Value == null
                            && spdData.ActiveSheet.Cells[iRow, 4].Value == null)
                        {
                            if (!bWithNull && (spdData.ActiveSheet.Cells[iRow, nColPos].Value == null || spdData.ActiveSheet.Cells[iRow, nColPos].Text.Trim() == ""))
                            {

                            }
                            else
                            {
                                dSubTotalDivide4 += 1;
                                dSubTotalSum4 += Convert.ToDouble(spdData.ActiveSheet.Cells[iRow, nColPos].Value);
                            }
                        }
                        else if (spdData.ActiveSheet.Cells[iRow, 0].Value != null
                            && spdData.ActiveSheet.Cells[iRow, 1].Value == null
                            && spdData.ActiveSheet.Cells[iRow, 2].Value == null
                            && spdData.ActiveSheet.Cells[iRow, 3].Value == null
                            && spdData.ActiveSheet.Cells[iRow, 4].Value == null)
                        {
                            if (dSubTotalDivide4 != 0)
                            {
                                spdData.ActiveSheet.Cells[iRow, nColPos].Value = dSubTotalSum4 / dSubTotalDivide4;
                            }
                            dSubTotalDivide4 = 0;
                            dSubTotalSum4 = 0;
                        }
                    }
                    else if (nBasisCount == 5)
                    {
                        // Cell Group 2
                        if (spdData.ActiveSheet.Cells[iRow, 0].Value != null
                            && spdData.ActiveSheet.Cells[iRow, 1].Value != null
                            && spdData.ActiveSheet.Cells[iRow, 2].Value != null
                            && spdData.ActiveSheet.Cells[iRow, 3].Value != null
                            && spdData.ActiveSheet.Cells[iRow, 4].Value != null
                            && spdData.ActiveSheet.Cells[iRow, 5].Value == null)
                        {
                            if (!bWithNull && (spdData.ActiveSheet.Cells[iRow, nColPos].Value == null || spdData.ActiveSheet.Cells[iRow, nColPos].Text.Trim() == ""))
                            {

                            }
                            else
                            {
                                dSubTotalDivide2 += 1;
                                dSubTotalSum2 += Convert.ToDouble(spdData.ActiveSheet.Cells[iRow, nColPos].Value);
                            }
                        }
                        else if (spdData.ActiveSheet.Cells[iRow, 0].Value != null
                            && spdData.ActiveSheet.Cells[iRow, 1].Value != null
                            && spdData.ActiveSheet.Cells[iRow, 2].Value != null
                            && spdData.ActiveSheet.Cells[iRow, 3].Value != null
                            && spdData.ActiveSheet.Cells[iRow, 4].Value == null
                            && spdData.ActiveSheet.Cells[iRow, 5].Value == null)
                        {
                            if (dSubTotalDivide2 != 0)
                            {
                                spdData.ActiveSheet.Cells[iRow, nColPos].Value = dSubTotalSum2 / dSubTotalDivide2;
                            }
                            dSubTotalDivide2 = 0;
                            dSubTotalSum2 = 0;
                        }

                        // Cell Group 3
                        if (spdData.ActiveSheet.Cells[iRow, 0].Value != null
                            && spdData.ActiveSheet.Cells[iRow, 1].Value != null
                            && spdData.ActiveSheet.Cells[iRow, 2].Value != null
                            && spdData.ActiveSheet.Cells[iRow, 3].Value != null
                            && spdData.ActiveSheet.Cells[iRow, 4].Value == null
                            && spdData.ActiveSheet.Cells[iRow, 5].Value == null)
                        {
                            if (!bWithNull && (spdData.ActiveSheet.Cells[iRow, nColPos].Value == null || spdData.ActiveSheet.Cells[iRow, nColPos].Text.Trim() == ""))
                            {

                            }
                            else
                            {
                                dSubTotalDivide3 += 1;
                                dSubTotalSum3 += Convert.ToDouble(spdData.ActiveSheet.Cells[iRow, nColPos].Value);
                            }
                        }
                        else if (spdData.ActiveSheet.Cells[iRow, 0].Value != null
                            && spdData.ActiveSheet.Cells[iRow, 1].Value != null
                            && spdData.ActiveSheet.Cells[iRow, 2].Value != null
                            && spdData.ActiveSheet.Cells[iRow, 3].Value == null
                            && spdData.ActiveSheet.Cells[iRow, 4].Value == null
                            && spdData.ActiveSheet.Cells[iRow, 5].Value == null)
                        {
                            if (dSubTotalDivide3 != 0)
                            {
                                spdData.ActiveSheet.Cells[iRow, nColPos].Value = dSubTotalSum3 / dSubTotalDivide3;
                            }
                            dSubTotalDivide3 = 0;
                            dSubTotalSum3 = 0;
                        }

                        // Cell Group 4
                        if (spdData.ActiveSheet.Cells[iRow, 0].Value != null
                            && spdData.ActiveSheet.Cells[iRow, 1].Value != null
                            && spdData.ActiveSheet.Cells[iRow, 2].Value != null
                            && spdData.ActiveSheet.Cells[iRow, 3].Value == null
                            && spdData.ActiveSheet.Cells[iRow, 4].Value == null
                            && spdData.ActiveSheet.Cells[iRow, 5].Value == null)
                        {
                            if (!bWithNull && (spdData.ActiveSheet.Cells[iRow, nColPos].Value == null || spdData.ActiveSheet.Cells[iRow, nColPos].Text.Trim() == ""))
                            {

                            }
                            else
                            {
                                dSubTotalDivide4 += 1;
                                dSubTotalSum4 += Convert.ToDouble(spdData.ActiveSheet.Cells[iRow, nColPos].Value);
                            }
                        }
                        else if (spdData.ActiveSheet.Cells[iRow, 0].Value != null
                            && spdData.ActiveSheet.Cells[iRow, 1].Value != null
                            && spdData.ActiveSheet.Cells[iRow, 2].Value == null
                            && spdData.ActiveSheet.Cells[iRow, 3].Value == null
                            && spdData.ActiveSheet.Cells[iRow, 4].Value == null
                            && spdData.ActiveSheet.Cells[iRow, 5].Value == null)
                        {
                            if (dSubTotalDivide4 != 0)
                            {
                                spdData.ActiveSheet.Cells[iRow, nColPos].Value = dSubTotalSum4 / dSubTotalDivide4;
                            }
                            dSubTotalDivide4 = 0;
                            dSubTotalSum4 = 0;
                        }

                        // Cell Group 5
                        if (spdData.ActiveSheet.Cells[iRow, 0].Value != null
                            && spdData.ActiveSheet.Cells[iRow, 1].Value != null
                            && spdData.ActiveSheet.Cells[iRow, 2].Value == null
                            && spdData.ActiveSheet.Cells[iRow, 3].Value == null
                            && spdData.ActiveSheet.Cells[iRow, 4].Value == null
                            && spdData.ActiveSheet.Cells[iRow, 5].Value == null)
                        {
                            if (!bWithNull && (spdData.ActiveSheet.Cells[iRow, nColPos].Value == null || spdData.ActiveSheet.Cells[iRow, nColPos].Text.Trim() == ""))
                            {

                            }
                            else
                            {
                                dSubTotalDivide5 += 1;
                                dSubTotalSum5 += Convert.ToDouble(spdData.ActiveSheet.Cells[iRow, nColPos].Value);
                            }
                        }
                        else if (spdData.ActiveSheet.Cells[iRow, 0].Value != null
                            && spdData.ActiveSheet.Cells[iRow, 1].Value == null
                            && spdData.ActiveSheet.Cells[iRow, 2].Value == null
                            && spdData.ActiveSheet.Cells[iRow, 3].Value == null
                            && spdData.ActiveSheet.Cells[iRow, 4].Value == null
                            && spdData.ActiveSheet.Cells[iRow, 5].Value == null)
                        {
                            if (dSubTotalDivide5 != 0)
                            {
                                spdData.ActiveSheet.Cells[iRow, nColPos].Value = dSubTotalSum5 / dSubTotalDivide5;
                            }
                            dSubTotalDivide5 = 0;
                            dSubTotalSum5 = 0;
                        }
                    }
                    else if (nBasisCount == 6)
                    {
                        // Cell Group 2
                        if (spdData.ActiveSheet.Cells[iRow, 0].Value != null
                            && spdData.ActiveSheet.Cells[iRow, 1].Value != null
                            && spdData.ActiveSheet.Cells[iRow, 2].Value != null
                            && spdData.ActiveSheet.Cells[iRow, 3].Value != null
                            && spdData.ActiveSheet.Cells[iRow, 4].Value != null
                            && spdData.ActiveSheet.Cells[iRow, 5].Value != null
                            && spdData.ActiveSheet.Cells[iRow, 6].Value == null)
                        {
                            if (!bWithNull && (spdData.ActiveSheet.Cells[iRow, nColPos].Value == null || spdData.ActiveSheet.Cells[iRow, nColPos].Text.Trim() == ""))
                            {

                            }
                            else
                            {
                                dSubTotalDivide2 += 1;
                                dSubTotalSum2 += Convert.ToDouble(spdData.ActiveSheet.Cells[iRow, nColPos].Value);
                            }
                        }
                        else if (spdData.ActiveSheet.Cells[iRow, 0].Value != null
                            && spdData.ActiveSheet.Cells[iRow, 1].Value != null
                            && spdData.ActiveSheet.Cells[iRow, 2].Value != null
                            && spdData.ActiveSheet.Cells[iRow, 3].Value != null
                            && spdData.ActiveSheet.Cells[iRow, 4].Value != null
                            && spdData.ActiveSheet.Cells[iRow, 5].Value == null
                            && spdData.ActiveSheet.Cells[iRow, 6].Value == null)
                        {
                            if (dSubTotalDivide2 != 0)
                            {
                                spdData.ActiveSheet.Cells[iRow, nColPos].Value = dSubTotalSum2 / dSubTotalDivide2;
                            }
                            dSubTotalDivide2 = 0;
                            dSubTotalSum2 = 0;
                        }

                        // Cell Group 3
                        if (spdData.ActiveSheet.Cells[iRow, 0].Value != null
                            && spdData.ActiveSheet.Cells[iRow, 1].Value != null
                            && spdData.ActiveSheet.Cells[iRow, 2].Value != null
                            && spdData.ActiveSheet.Cells[iRow, 3].Value != null
                            && spdData.ActiveSheet.Cells[iRow, 4].Value != null
                            && spdData.ActiveSheet.Cells[iRow, 5].Value == null
                            && spdData.ActiveSheet.Cells[iRow, 6].Value == null)
                        {
                            if (!bWithNull && (spdData.ActiveSheet.Cells[iRow, nColPos].Value == null || spdData.ActiveSheet.Cells[iRow, nColPos].Text.Trim() == ""))
                            {

                            }
                            else
                            {
                                dSubTotalDivide3 += 1;
                                dSubTotalSum3 += Convert.ToDouble(spdData.ActiveSheet.Cells[iRow, nColPos].Value);
                            }
                        }
                        else if (spdData.ActiveSheet.Cells[iRow, 0].Value != null
                            && spdData.ActiveSheet.Cells[iRow, 1].Value != null
                            && spdData.ActiveSheet.Cells[iRow, 2].Value != null
                            && spdData.ActiveSheet.Cells[iRow, 3].Value != null
                            && spdData.ActiveSheet.Cells[iRow, 4].Value == null
                            && spdData.ActiveSheet.Cells[iRow, 5].Value == null
                            && spdData.ActiveSheet.Cells[iRow, 6].Value == null)
                        {
                            if (dSubTotalDivide3 != 0)
                            {
                                spdData.ActiveSheet.Cells[iRow, nColPos].Value = dSubTotalSum3 / dSubTotalDivide3;
                            }
                            dSubTotalDivide3 = 0;
                            dSubTotalSum3 = 0;
                        }

                        // Cell Group 4
                        if (spdData.ActiveSheet.Cells[iRow, 0].Value != null
                            && spdData.ActiveSheet.Cells[iRow, 1].Value != null
                            && spdData.ActiveSheet.Cells[iRow, 2].Value != null
                            && spdData.ActiveSheet.Cells[iRow, 3].Value != null
                            && spdData.ActiveSheet.Cells[iRow, 4].Value == null
                            && spdData.ActiveSheet.Cells[iRow, 5].Value == null
                            && spdData.ActiveSheet.Cells[iRow, 6].Value == null)
                        {
                            if (!bWithNull && (spdData.ActiveSheet.Cells[iRow, nColPos].Value == null || spdData.ActiveSheet.Cells[iRow, nColPos].Text.Trim() == ""))
                            {

                            }
                            else
                            {
                                dSubTotalDivide4 += 1;
                                dSubTotalSum4 += Convert.ToDouble(spdData.ActiveSheet.Cells[iRow, nColPos].Value);
                            }
                        }
                        else if (spdData.ActiveSheet.Cells[iRow, 0].Value != null
                            && spdData.ActiveSheet.Cells[iRow, 1].Value != null
                            && spdData.ActiveSheet.Cells[iRow, 2].Value != null
                            && spdData.ActiveSheet.Cells[iRow, 3].Value == null
                            && spdData.ActiveSheet.Cells[iRow, 4].Value == null
                            && spdData.ActiveSheet.Cells[iRow, 5].Value == null
                            && spdData.ActiveSheet.Cells[iRow, 6].Value == null)
                        {
                            if (dSubTotalDivide4 != 0)
                            {
                                spdData.ActiveSheet.Cells[iRow, nColPos].Value = dSubTotalSum4 / dSubTotalDivide4;
                            }
                            dSubTotalDivide4 = 0;
                            dSubTotalSum4 = 0;
                        }

                        // Cell Group 5
                        if (spdData.ActiveSheet.Cells[iRow, 0].Value != null
                            && spdData.ActiveSheet.Cells[iRow, 1].Value != null
                            && spdData.ActiveSheet.Cells[iRow, 2].Value != null
                            && spdData.ActiveSheet.Cells[iRow, 3].Value == null
                            && spdData.ActiveSheet.Cells[iRow, 4].Value == null
                            && spdData.ActiveSheet.Cells[iRow, 5].Value == null
                            && spdData.ActiveSheet.Cells[iRow, 6].Value == null)
                        {
                            if (!bWithNull && (spdData.ActiveSheet.Cells[iRow, nColPos].Value == null || spdData.ActiveSheet.Cells[iRow, nColPos].Text.Trim() == ""))
                            {

                            }
                            else
                            {
                                dSubTotalDivide5 += 1;
                                dSubTotalSum5 += Convert.ToDouble(spdData.ActiveSheet.Cells[iRow, nColPos].Value);
                            }
                        }
                        else if (spdData.ActiveSheet.Cells[iRow, 0].Value != null
                            && spdData.ActiveSheet.Cells[iRow, 1].Value != null
                            && spdData.ActiveSheet.Cells[iRow, 2].Value == null
                            && spdData.ActiveSheet.Cells[iRow, 3].Value == null
                            && spdData.ActiveSheet.Cells[iRow, 4].Value == null
                            && spdData.ActiveSheet.Cells[iRow, 5].Value == null
                            && spdData.ActiveSheet.Cells[iRow, 6].Value == null)
                        {
                            if (dSubTotalDivide5 != 0)
                            {
                                spdData.ActiveSheet.Cells[iRow, nColPos].Value = dSubTotalSum5 / dSubTotalDivide5;
                            }
                            dSubTotalDivide5 = 0;
                            dSubTotalSum5 = 0;
                        }

                        // Cell Group 6
                        if (spdData.ActiveSheet.Cells[iRow, 0].Value != null
                            && spdData.ActiveSheet.Cells[iRow, 1].Value != null
                            && spdData.ActiveSheet.Cells[iRow, 2].Value == null
                            && spdData.ActiveSheet.Cells[iRow, 3].Value == null
                            && spdData.ActiveSheet.Cells[iRow, 4].Value == null
                            && spdData.ActiveSheet.Cells[iRow, 5].Value == null
                            && spdData.ActiveSheet.Cells[iRow, 6].Value == null)
                        {
                            if (!bWithNull && (spdData.ActiveSheet.Cells[iRow, nColPos].Value == null || spdData.ActiveSheet.Cells[iRow, nColPos].Text.Trim() == ""))
                            {

                            }
                            else
                            {
                                dSubTotalDivide6 += 1;
                                dSubTotalSum6 += Convert.ToDouble(spdData.ActiveSheet.Cells[iRow, nColPos].Value);
                            }
                        }
                        else if (spdData.ActiveSheet.Cells[iRow, 0].Value != null
                            && spdData.ActiveSheet.Cells[iRow, 1].Value == null
                            && spdData.ActiveSheet.Cells[iRow, 2].Value == null
                            && spdData.ActiveSheet.Cells[iRow, 3].Value == null
                            && spdData.ActiveSheet.Cells[iRow, 4].Value == null
                            && spdData.ActiveSheet.Cells[iRow, 5].Value == null
                            && spdData.ActiveSheet.Cells[iRow, 6].Value == null)
                        {
                            if (dSubTotalDivide6 != 0)
                            {
                                spdData.ActiveSheet.Cells[iRow, nColPos].Value = dSubTotalSum6 / dSubTotalDivide6;
                            }
                            dSubTotalDivide6 = 0;
                            dSubTotalSum6 = 0;
                        }
                    }
                    else if (nBasisCount == 7)
                    {
                        // Cell Group 2
                        if (spdData.ActiveSheet.Cells[iRow, 0].Value != null
                            && spdData.ActiveSheet.Cells[iRow, 1].Value != null
                            && spdData.ActiveSheet.Cells[iRow, 2].Value != null
                            && spdData.ActiveSheet.Cells[iRow, 3].Value != null
                            && spdData.ActiveSheet.Cells[iRow, 4].Value != null
                            && spdData.ActiveSheet.Cells[iRow, 5].Value != null
                            && spdData.ActiveSheet.Cells[iRow, 6].Value != null
                            && spdData.ActiveSheet.Cells[iRow, 7].Value == null)
                        {
                            if (!bWithNull && (spdData.ActiveSheet.Cells[iRow, nColPos].Value == null || spdData.ActiveSheet.Cells[iRow, nColPos].Text.Trim() == ""))
                            {

                            }
                            else
                            {
                                dSubTotalDivide2 += 1;
                                dSubTotalSum2 += Convert.ToDouble(spdData.ActiveSheet.Cells[iRow, nColPos].Value);
                            }
                        }
                        else if (spdData.ActiveSheet.Cells[iRow, 0].Value != null
                            && spdData.ActiveSheet.Cells[iRow, 1].Value != null
                            && spdData.ActiveSheet.Cells[iRow, 2].Value != null
                            && spdData.ActiveSheet.Cells[iRow, 3].Value != null
                            && spdData.ActiveSheet.Cells[iRow, 4].Value != null
                            && spdData.ActiveSheet.Cells[iRow, 5].Value != null
                            && spdData.ActiveSheet.Cells[iRow, 6].Value == null
                            && spdData.ActiveSheet.Cells[iRow, 7].Value == null)
                        {
                            if (dSubTotalDivide2 != 0)
                            {
                                spdData.ActiveSheet.Cells[iRow, nColPos].Value = dSubTotalSum2 / dSubTotalDivide2;
                            }
                            dSubTotalDivide2 = 0;
                            dSubTotalSum2 = 0;
                        }

                        // Cell Group 3
                        if (spdData.ActiveSheet.Cells[iRow, 0].Value != null
                            && spdData.ActiveSheet.Cells[iRow, 1].Value != null
                            && spdData.ActiveSheet.Cells[iRow, 2].Value != null
                            && spdData.ActiveSheet.Cells[iRow, 3].Value != null
                            && spdData.ActiveSheet.Cells[iRow, 4].Value != null
                            && spdData.ActiveSheet.Cells[iRow, 5].Value != null
                            && spdData.ActiveSheet.Cells[iRow, 6].Value == null
                            && spdData.ActiveSheet.Cells[iRow, 7].Value == null)
                        {
                            if (!bWithNull && (spdData.ActiveSheet.Cells[iRow, nColPos].Value == null || spdData.ActiveSheet.Cells[iRow, nColPos].Text.Trim() == ""))
                            {

                            }
                            else
                            {
                                dSubTotalDivide3 += 1;
                                dSubTotalSum3 += Convert.ToDouble(spdData.ActiveSheet.Cells[iRow, nColPos].Value);
                            }
                        }
                        else if (spdData.ActiveSheet.Cells[iRow, 0].Value != null
                            && spdData.ActiveSheet.Cells[iRow, 1].Value != null
                            && spdData.ActiveSheet.Cells[iRow, 2].Value != null
                            && spdData.ActiveSheet.Cells[iRow, 3].Value != null
                            && spdData.ActiveSheet.Cells[iRow, 4].Value != null
                            && spdData.ActiveSheet.Cells[iRow, 5].Value == null
                            && spdData.ActiveSheet.Cells[iRow, 6].Value == null
                            && spdData.ActiveSheet.Cells[iRow, 7].Value == null)
                        {
                            if (dSubTotalDivide3 != 0)
                            {
                                spdData.ActiveSheet.Cells[iRow, nColPos].Value = dSubTotalSum3 / dSubTotalDivide3;
                            }
                            dSubTotalDivide3 = 0;
                            dSubTotalSum3 = 0;
                        }

                        // Cell Group 4
                        if (spdData.ActiveSheet.Cells[iRow, 0].Value != null
                            && spdData.ActiveSheet.Cells[iRow, 1].Value != null
                            && spdData.ActiveSheet.Cells[iRow, 2].Value != null
                            && spdData.ActiveSheet.Cells[iRow, 3].Value != null
                            && spdData.ActiveSheet.Cells[iRow, 4].Value != null
                            && spdData.ActiveSheet.Cells[iRow, 5].Value == null
                            && spdData.ActiveSheet.Cells[iRow, 6].Value == null
                            && spdData.ActiveSheet.Cells[iRow, 7].Value == null)
                        {
                            if (!bWithNull && (spdData.ActiveSheet.Cells[iRow, nColPos].Value == null || spdData.ActiveSheet.Cells[iRow, nColPos].Text.Trim() == ""))
                            {

                            }
                            else
                            {
                                dSubTotalDivide4 += 1;
                                dSubTotalSum4 += Convert.ToDouble(spdData.ActiveSheet.Cells[iRow, nColPos].Value);
                            }
                        }
                        else if (spdData.ActiveSheet.Cells[iRow, 0].Value != null
                            && spdData.ActiveSheet.Cells[iRow, 1].Value != null
                            && spdData.ActiveSheet.Cells[iRow, 2].Value != null
                            && spdData.ActiveSheet.Cells[iRow, 3].Value != null
                            && spdData.ActiveSheet.Cells[iRow, 4].Value == null
                            && spdData.ActiveSheet.Cells[iRow, 5].Value == null
                            && spdData.ActiveSheet.Cells[iRow, 6].Value == null
                            && spdData.ActiveSheet.Cells[iRow, 7].Value == null)
                        {
                            if (dSubTotalDivide4 != 0)
                            {
                                spdData.ActiveSheet.Cells[iRow, nColPos].Value = dSubTotalSum4 / dSubTotalDivide4;
                            }
                            dSubTotalDivide4 = 0;
                            dSubTotalSum4 = 0;
                        }

                        // Cell Group 5
                        if (spdData.ActiveSheet.Cells[iRow, 0].Value != null
                            && spdData.ActiveSheet.Cells[iRow, 1].Value != null
                            && spdData.ActiveSheet.Cells[iRow, 2].Value != null
                            && spdData.ActiveSheet.Cells[iRow, 3].Value != null
                            && spdData.ActiveSheet.Cells[iRow, 4].Value == null
                            && spdData.ActiveSheet.Cells[iRow, 5].Value == null
                            && spdData.ActiveSheet.Cells[iRow, 6].Value == null
                            && spdData.ActiveSheet.Cells[iRow, 7].Value == null)
                        {
                            if (!bWithNull && (spdData.ActiveSheet.Cells[iRow, nColPos].Value == null || spdData.ActiveSheet.Cells[iRow, nColPos].Text.Trim() == ""))
                            {

                            }
                            else
                            {
                                dSubTotalDivide5 += 1;
                                dSubTotalSum5 += Convert.ToDouble(spdData.ActiveSheet.Cells[iRow, nColPos].Value);
                            }
                        }
                        else if (spdData.ActiveSheet.Cells[iRow, 0].Value != null
                            && spdData.ActiveSheet.Cells[iRow, 1].Value != null
                            && spdData.ActiveSheet.Cells[iRow, 2].Value != null
                            && spdData.ActiveSheet.Cells[iRow, 3].Value == null
                            && spdData.ActiveSheet.Cells[iRow, 4].Value == null
                            && spdData.ActiveSheet.Cells[iRow, 5].Value == null
                            && spdData.ActiveSheet.Cells[iRow, 6].Value == null
                            && spdData.ActiveSheet.Cells[iRow, 7].Value == null)
                        {
                            if (dSubTotalDivide5 != 0)
                            {
                                spdData.ActiveSheet.Cells[iRow, nColPos].Value = dSubTotalSum5 / dSubTotalDivide5;
                            }
                            dSubTotalDivide5 = 0;
                            dSubTotalSum5 = 0;
                        }

                        // Cell Group 6
                        if (spdData.ActiveSheet.Cells[iRow, 0].Value != null
                            && spdData.ActiveSheet.Cells[iRow, 1].Value != null
                            && spdData.ActiveSheet.Cells[iRow, 2].Value != null
                            && spdData.ActiveSheet.Cells[iRow, 3].Value == null
                            && spdData.ActiveSheet.Cells[iRow, 4].Value == null
                            && spdData.ActiveSheet.Cells[iRow, 5].Value == null
                            && spdData.ActiveSheet.Cells[iRow, 6].Value == null
                            && spdData.ActiveSheet.Cells[iRow, 7].Value == null)
                        {
                            if (!bWithNull && (spdData.ActiveSheet.Cells[iRow, nColPos].Value == null || spdData.ActiveSheet.Cells[iRow, nColPos].Text.Trim() == ""))
                            {

                            }
                            else
                            {
                                dSubTotalDivide6 += 1;
                                dSubTotalSum6 += Convert.ToDouble(spdData.ActiveSheet.Cells[iRow, nColPos].Value);
                            }
                        }
                        else if (spdData.ActiveSheet.Cells[iRow, 0].Value != null
                            && spdData.ActiveSheet.Cells[iRow, 1].Value != null
                            && spdData.ActiveSheet.Cells[iRow, 2].Value == null
                            && spdData.ActiveSheet.Cells[iRow, 3].Value == null
                            && spdData.ActiveSheet.Cells[iRow, 4].Value == null
                            && spdData.ActiveSheet.Cells[iRow, 5].Value == null
                            && spdData.ActiveSheet.Cells[iRow, 6].Value == null
                            && spdData.ActiveSheet.Cells[iRow, 7].Value == null)
                        {
                            if (dSubTotalDivide6 != 0)
                            {
                                spdData.ActiveSheet.Cells[iRow, nColPos].Value = dSubTotalSum6 / dSubTotalDivide6;
                            }
                            dSubTotalDivide6 = 0;
                            dSubTotalSum6 = 0;
                        }

                        // Cell Group 7
                        if (spdData.ActiveSheet.Cells[iRow, 0].Value != null
                            && spdData.ActiveSheet.Cells[iRow, 1].Value != null
                            && spdData.ActiveSheet.Cells[iRow, 2].Value == null
                            && spdData.ActiveSheet.Cells[iRow, 3].Value == null
                            && spdData.ActiveSheet.Cells[iRow, 4].Value == null
                            && spdData.ActiveSheet.Cells[iRow, 5].Value == null
                            && spdData.ActiveSheet.Cells[iRow, 6].Value == null
                            && spdData.ActiveSheet.Cells[iRow, 7].Value == null)
                        {
                            if (!bWithNull && (spdData.ActiveSheet.Cells[iRow, nColPos].Value == null || spdData.ActiveSheet.Cells[iRow, nColPos].Text.Trim() == ""))
                            {

                            }
                            else
                            {
                                dSubTotalDivide7 += 1;
                                dSubTotalSum7 += Convert.ToDouble(spdData.ActiveSheet.Cells[iRow, nColPos].Value);
                            }
                        }
                        else if (spdData.ActiveSheet.Cells[iRow, 0].Value != null
                            && spdData.ActiveSheet.Cells[iRow, 1].Value == null
                            && spdData.ActiveSheet.Cells[iRow, 2].Value == null
                            && spdData.ActiveSheet.Cells[iRow, 3].Value == null
                            && spdData.ActiveSheet.Cells[iRow, 4].Value == null
                            && spdData.ActiveSheet.Cells[iRow, 5].Value == null
                            && spdData.ActiveSheet.Cells[iRow, 6].Value == null
                            && spdData.ActiveSheet.Cells[iRow, 7].Value == null)
                        {
                            if (dSubTotalDivide7 != 0)
                            {
                                spdData.ActiveSheet.Cells[iRow, nColPos].Value = dSubTotalSum7 / dSubTotalDivide7;
                            }
                            dSubTotalDivide7 = 0;
                            dSubTotalSum7 = 0;
                        }
                    }
                    else if (nBasisCount == 8)
                    {
                        // Cell Group 2
                        if (spdData.ActiveSheet.Cells[iRow, 0].Value != null
                            && spdData.ActiveSheet.Cells[iRow, 1].Value != null
                            && spdData.ActiveSheet.Cells[iRow, 2].Value != null
                            && spdData.ActiveSheet.Cells[iRow, 3].Value != null
                            && spdData.ActiveSheet.Cells[iRow, 4].Value != null
                            && spdData.ActiveSheet.Cells[iRow, 5].Value != null
                            && spdData.ActiveSheet.Cells[iRow, 6].Value != null
                            && spdData.ActiveSheet.Cells[iRow, 7].Value != null
                            && spdData.ActiveSheet.Cells[iRow, 8].Value == null)
                        {
                            if (!bWithNull && (spdData.ActiveSheet.Cells[iRow, nColPos].Value == null || spdData.ActiveSheet.Cells[iRow, nColPos].Text.Trim() == ""))
                            {

                            }
                            else
                            {
                                dSubTotalDivide2 += 1;
                                dSubTotalSum2 += Convert.ToDouble(spdData.ActiveSheet.Cells[iRow, nColPos].Value);
                            }
                        }
                        else if (spdData.ActiveSheet.Cells[iRow, 0].Value != null
                            && spdData.ActiveSheet.Cells[iRow, 1].Value != null
                            && spdData.ActiveSheet.Cells[iRow, 2].Value != null
                            && spdData.ActiveSheet.Cells[iRow, 3].Value != null
                            && spdData.ActiveSheet.Cells[iRow, 4].Value != null
                            && spdData.ActiveSheet.Cells[iRow, 5].Value != null
                            && spdData.ActiveSheet.Cells[iRow, 6].Value != null
                            && spdData.ActiveSheet.Cells[iRow, 7].Value == null
                            && spdData.ActiveSheet.Cells[iRow, 8].Value == null)
                        {
                            if (dSubTotalDivide2 != 0)
                            {
                                spdData.ActiveSheet.Cells[iRow, nColPos].Value = dSubTotalSum2 / dSubTotalDivide2;
                            }
                            dSubTotalDivide2 = 0;
                            dSubTotalSum2 = 0;
                        }

                        // Cell Group 3
                        if (spdData.ActiveSheet.Cells[iRow, 0].Value != null
                            && spdData.ActiveSheet.Cells[iRow, 1].Value != null
                            && spdData.ActiveSheet.Cells[iRow, 2].Value != null
                            && spdData.ActiveSheet.Cells[iRow, 3].Value != null
                            && spdData.ActiveSheet.Cells[iRow, 4].Value != null
                            && spdData.ActiveSheet.Cells[iRow, 5].Value != null
                            && spdData.ActiveSheet.Cells[iRow, 6].Value != null
                            && spdData.ActiveSheet.Cells[iRow, 7].Value == null
                            && spdData.ActiveSheet.Cells[iRow, 8].Value == null)
                        {
                            if (!bWithNull && (spdData.ActiveSheet.Cells[iRow, nColPos].Value == null || spdData.ActiveSheet.Cells[iRow, nColPos].Text.Trim() == ""))
                            {

                            }
                            else
                            {
                                dSubTotalDivide3 += 1;
                                dSubTotalSum3 += Convert.ToDouble(spdData.ActiveSheet.Cells[iRow, nColPos].Value);
                            }
                        }
                        else if (spdData.ActiveSheet.Cells[iRow, 0].Value != null
                            && spdData.ActiveSheet.Cells[iRow, 1].Value != null
                            && spdData.ActiveSheet.Cells[iRow, 2].Value != null
                            && spdData.ActiveSheet.Cells[iRow, 3].Value != null
                            && spdData.ActiveSheet.Cells[iRow, 4].Value != null
                            && spdData.ActiveSheet.Cells[iRow, 5].Value != null
                            && spdData.ActiveSheet.Cells[iRow, 6].Value == null
                            && spdData.ActiveSheet.Cells[iRow, 7].Value == null
                            && spdData.ActiveSheet.Cells[iRow, 8].Value == null)
                        {
                            if (dSubTotalDivide3 != 0)
                            {
                                spdData.ActiveSheet.Cells[iRow, nColPos].Value = dSubTotalSum3 / dSubTotalDivide3;
                            }
                            dSubTotalDivide3 = 0;
                            dSubTotalSum3 = 0;
                        }

                        // Cell Group 4
                        if (spdData.ActiveSheet.Cells[iRow, 0].Value != null
                            && spdData.ActiveSheet.Cells[iRow, 1].Value != null
                            && spdData.ActiveSheet.Cells[iRow, 2].Value != null
                            && spdData.ActiveSheet.Cells[iRow, 3].Value != null
                            && spdData.ActiveSheet.Cells[iRow, 4].Value != null
                            && spdData.ActiveSheet.Cells[iRow, 5].Value != null
                            && spdData.ActiveSheet.Cells[iRow, 6].Value == null
                            && spdData.ActiveSheet.Cells[iRow, 7].Value == null
                            && spdData.ActiveSheet.Cells[iRow, 8].Value == null)
                        {
                            if (!bWithNull && (spdData.ActiveSheet.Cells[iRow, nColPos].Value == null || spdData.ActiveSheet.Cells[iRow, nColPos].Text.Trim() == ""))
                            {

                            }
                            else
                            {
                                dSubTotalDivide4 += 1;
                                dSubTotalSum4 += Convert.ToDouble(spdData.ActiveSheet.Cells[iRow, nColPos].Value);
                            }
                        }
                        else if (spdData.ActiveSheet.Cells[iRow, 0].Value != null
                            && spdData.ActiveSheet.Cells[iRow, 1].Value != null
                            && spdData.ActiveSheet.Cells[iRow, 2].Value != null
                            && spdData.ActiveSheet.Cells[iRow, 3].Value != null
                            && spdData.ActiveSheet.Cells[iRow, 4].Value != null
                            && spdData.ActiveSheet.Cells[iRow, 5].Value == null
                            && spdData.ActiveSheet.Cells[iRow, 6].Value == null
                            && spdData.ActiveSheet.Cells[iRow, 7].Value == null
                            && spdData.ActiveSheet.Cells[iRow, 8].Value == null)
                        {
                            if (dSubTotalDivide4 != 0)
                            {
                                spdData.ActiveSheet.Cells[iRow, nColPos].Value = dSubTotalSum4 / dSubTotalDivide4;
                            }
                            dSubTotalDivide4 = 0;
                            dSubTotalSum4 = 0;
                        }

                        // Cell Group 5
                        if (spdData.ActiveSheet.Cells[iRow, 0].Value != null
                            && spdData.ActiveSheet.Cells[iRow, 1].Value != null
                            && spdData.ActiveSheet.Cells[iRow, 2].Value != null
                            && spdData.ActiveSheet.Cells[iRow, 3].Value != null
                            && spdData.ActiveSheet.Cells[iRow, 4].Value != null
                            && spdData.ActiveSheet.Cells[iRow, 5].Value == null
                            && spdData.ActiveSheet.Cells[iRow, 6].Value == null
                            && spdData.ActiveSheet.Cells[iRow, 7].Value == null
                            && spdData.ActiveSheet.Cells[iRow, 8].Value == null)
                        {
                            if (!bWithNull && (spdData.ActiveSheet.Cells[iRow, nColPos].Value == null || spdData.ActiveSheet.Cells[iRow, nColPos].Text.Trim() == ""))
                            {

                            }
                            else
                            {
                                dSubTotalDivide5 += 1;
                                dSubTotalSum5 += Convert.ToDouble(spdData.ActiveSheet.Cells[iRow, nColPos].Value);
                            }
                        }
                        else if (spdData.ActiveSheet.Cells[iRow, 0].Value != null
                            && spdData.ActiveSheet.Cells[iRow, 1].Value != null
                            && spdData.ActiveSheet.Cells[iRow, 2].Value != null
                            && spdData.ActiveSheet.Cells[iRow, 3].Value != null
                            && spdData.ActiveSheet.Cells[iRow, 4].Value == null
                            && spdData.ActiveSheet.Cells[iRow, 5].Value == null
                            && spdData.ActiveSheet.Cells[iRow, 6].Value == null
                            && spdData.ActiveSheet.Cells[iRow, 7].Value == null
                            && spdData.ActiveSheet.Cells[iRow, 8].Value == null)
                        {
                            if (dSubTotalDivide5 != 0)
                            {
                                spdData.ActiveSheet.Cells[iRow, nColPos].Value = dSubTotalSum5 / dSubTotalDivide5;
                            }
                            dSubTotalDivide5 = 0;
                            dSubTotalSum5 = 0;
                        }

                        // Cell Group 6
                        if (spdData.ActiveSheet.Cells[iRow, 0].Value != null
                            && spdData.ActiveSheet.Cells[iRow, 1].Value != null
                            && spdData.ActiveSheet.Cells[iRow, 2].Value != null
                            && spdData.ActiveSheet.Cells[iRow, 3].Value != null
                            && spdData.ActiveSheet.Cells[iRow, 4].Value == null
                            && spdData.ActiveSheet.Cells[iRow, 5].Value == null
                            && spdData.ActiveSheet.Cells[iRow, 6].Value == null
                            && spdData.ActiveSheet.Cells[iRow, 7].Value == null
                            && spdData.ActiveSheet.Cells[iRow, 8].Value == null)
                        {
                            if (!bWithNull && (spdData.ActiveSheet.Cells[iRow, nColPos].Value == null || spdData.ActiveSheet.Cells[iRow, nColPos].Text.Trim() == ""))
                            {

                            }
                            else
                            {
                                dSubTotalDivide6 += 1;
                                dSubTotalSum6 += Convert.ToDouble(spdData.ActiveSheet.Cells[iRow, nColPos].Value);
                            }
                        }
                        else if (spdData.ActiveSheet.Cells[iRow, 0].Value != null
                            && spdData.ActiveSheet.Cells[iRow, 1].Value != null
                            && spdData.ActiveSheet.Cells[iRow, 2].Value != null
                            && spdData.ActiveSheet.Cells[iRow, 3].Value == null
                            && spdData.ActiveSheet.Cells[iRow, 4].Value == null
                            && spdData.ActiveSheet.Cells[iRow, 5].Value == null
                            && spdData.ActiveSheet.Cells[iRow, 6].Value == null
                            && spdData.ActiveSheet.Cells[iRow, 7].Value == null
                            && spdData.ActiveSheet.Cells[iRow, 8].Value == null)
                        {
                            if (dSubTotalDivide6 != 0)
                            {
                                spdData.ActiveSheet.Cells[iRow, nColPos].Value = dSubTotalSum6 / dSubTotalDivide6;
                            }
                            dSubTotalDivide6 = 0;
                            dSubTotalSum6 = 0;
                        }

                        // Cell Group 7
                        if (spdData.ActiveSheet.Cells[iRow, 0].Value != null
                            && spdData.ActiveSheet.Cells[iRow, 1].Value != null
                            && spdData.ActiveSheet.Cells[iRow, 2].Value != null
                            && spdData.ActiveSheet.Cells[iRow, 3].Value == null
                            && spdData.ActiveSheet.Cells[iRow, 4].Value == null
                            && spdData.ActiveSheet.Cells[iRow, 5].Value == null
                            && spdData.ActiveSheet.Cells[iRow, 6].Value == null
                            && spdData.ActiveSheet.Cells[iRow, 7].Value == null
                            && spdData.ActiveSheet.Cells[iRow, 8].Value == null)
                        {
                            if (!bWithNull && (spdData.ActiveSheet.Cells[iRow, nColPos].Value == null || spdData.ActiveSheet.Cells[iRow, nColPos].Text.Trim() == ""))
                            {

                            }
                            else
                            {
                                dSubTotalDivide7 += 1;
                                dSubTotalSum7 += Convert.ToDouble(spdData.ActiveSheet.Cells[iRow, nColPos].Value);
                            }
                        }
                        else if (spdData.ActiveSheet.Cells[iRow, 0].Value != null
                            && spdData.ActiveSheet.Cells[iRow, 1].Value != null
                            && spdData.ActiveSheet.Cells[iRow, 2].Value == null
                            && spdData.ActiveSheet.Cells[iRow, 3].Value == null
                            && spdData.ActiveSheet.Cells[iRow, 4].Value == null
                            && spdData.ActiveSheet.Cells[iRow, 5].Value == null
                            && spdData.ActiveSheet.Cells[iRow, 6].Value == null
                            && spdData.ActiveSheet.Cells[iRow, 7].Value == null
                            && spdData.ActiveSheet.Cells[iRow, 8].Value == null)
                        {
                            if (dSubTotalDivide7 != 0)
                            {
                                spdData.ActiveSheet.Cells[iRow, nColPos].Value = dSubTotalSum7 / dSubTotalDivide7;
                            }
                            dSubTotalDivide7 = 0;
                            dSubTotalSum7 = 0;
                        }

                        // Cell Group 8
                        if (spdData.ActiveSheet.Cells[iRow, 0].Value != null
                            && spdData.ActiveSheet.Cells[iRow, 1].Value != null
                            && spdData.ActiveSheet.Cells[iRow, 2].Value == null
                            && spdData.ActiveSheet.Cells[iRow, 3].Value == null
                            && spdData.ActiveSheet.Cells[iRow, 4].Value == null
                            && spdData.ActiveSheet.Cells[iRow, 5].Value == null
                            && spdData.ActiveSheet.Cells[iRow, 6].Value == null
                            && spdData.ActiveSheet.Cells[iRow, 7].Value == null
                            && spdData.ActiveSheet.Cells[iRow, 8].Value == null)
                        {
                            if (!bWithNull && (spdData.ActiveSheet.Cells[iRow, nColPos].Value == null || spdData.ActiveSheet.Cells[iRow, nColPos].Text.Trim() == ""))
                            {

                            }
                            else
                            {
                                dSubTotalDivide8 += 1;
                                dSubTotalSum8 += Convert.ToDouble(spdData.ActiveSheet.Cells[iRow, nColPos].Value);
                            }
                        }
                        else if (spdData.ActiveSheet.Cells[iRow, 0].Value != null
                            && spdData.ActiveSheet.Cells[iRow, 1].Value == null
                            && spdData.ActiveSheet.Cells[iRow, 2].Value == null
                            && spdData.ActiveSheet.Cells[iRow, 3].Value == null
                            && spdData.ActiveSheet.Cells[iRow, 4].Value == null
                            && spdData.ActiveSheet.Cells[iRow, 5].Value == null
                            && spdData.ActiveSheet.Cells[iRow, 6].Value == null
                            && spdData.ActiveSheet.Cells[iRow, 7].Value == null
                            && spdData.ActiveSheet.Cells[iRow, 8].Value == null)
                        {
                            if (dSubTotalDivide8 != 0)
                            {
                                spdData.ActiveSheet.Cells[iRow, nColPos].Value = dSubTotalSum8 / dSubTotalDivide8;
                            }
                            dSubTotalDivide8 = 0;
                            dSubTotalSum8 = 0;
                        }
                    }
                    
                }
                

                //if (spdData.ActiveSheet.Cells[iRow, nColPos].BackColor == color)
                //{
                //    iSubTotalCount += 1;
                //    if (iSubTotalCount == nBasisCount)
                //    {
                //        if (dSubTotalDivide != 0)
                //        {
                //            spdData.ActiveSheet.Cells[iRow, nColPos].Value = dSubTotalSum / dSubTotalDivide;
                //        }
                //        dSubTotalDivide = 0;
                //        dSubTotalSum = 0;
                //        iSubTotalCount = 0;
                //    }
                //    else
                //    {
                //        if (!bWithNull && (spdData.ActiveSheet.Cells[iRow, nColPos].Value == null || spdData.ActiveSheet.Cells[iRow, nColPos].Text.Trim() == ""))
                //        {

                //        }
                //        else
                //        {
                //            if (iSubTotalCount == nBasisCount - 1)
                //            {
                //                dSubTotalSum += Convert.ToDouble(spdData.ActiveSheet.Cells[iRow, nColPos].Value);
                //                dSubTotalDivide += 1;
                //            }
                //        }
                //    }
                //}
                //else
                //{
                //    iSubTotalCount = 0;
                //}
            }
            return;
        }

        #endregion


        #region " MakeSqlString : Sql Query문 "

        /// <summary>
        /// 4. 쿼리 생성
        /// </summary>
        /// <returns></returns>
        private string MakeSqlString()
        {
            /****************************************************/
            /*     Comment : 재공이동현황을 조회하는 QUERY문	*/
            /* 											       */
            /*   Create By : bee-jae jung(2009-05-27-수요일)    */
            /* 											       */
            /* Modified By : bee-jae jung(2009-05-27-수요일)    */
            /****************************************************/
            StringBuilder strSqlString = new StringBuilder();
            string QueryCond1 = null, QueryCond2 = null, QueryCond3 = null;
            udcTableForm tableForm = (udcTableForm)btnSort.BindingForm;
            
            QueryCond1 = tableForm.SelectedValueToQueryContainNull;
            QueryCond2 = tableForm.SelectedValue2ToQueryContainNull;
            QueryCond3 = tableForm.SelectedValue3ToQueryContainNull;

            try
            {
                Cursor.Current = Cursors.WaitCursor;

                //// 결산기간
                //switch (cdvFromToDate.DaySelector.SelectedValue.ToString())
                //{
                //    case "DAY":
                //        //if (udcWIPCondition1.Text == "HX" || (txtSearchProduct.Text.Trim().Length >= 2 && txtSearchProduct.Text.Trim().Substring(0, 2).ToUpper() == "HX"))
                //        //{
                //        //    // 시작일자
                //        //    strFromDate = cdvFromToDate.FromDate.Text.Replace("-", "") + "060001";
                //        //    // 정상
                //        //    if (rbtEoh01.Checked == true)
                //        //    {
                //        //        strToDate = cdvFromToDate.ToDate.Text.Replace("-", "") + "060000";
                //        //    }
                //        //    // 금일
                //        //    else
                //        //    {
                //        //        // 종료일자
                //        //        QRY = "(SELECT TO_CHAR(SYSDATE, 'YYYYMMDDHH24MISS') AS NOW_DATE FROM DUAL)";
                //        //        dt1 = CmnFunction.oComm.GetFuncDataTable("DYNAMIC", QRY);
                //        //        strToDate = dt1.Rows[0][0].ToString();
                //        //    }
                //        //}
                //        //else
                //        //{
                //        //    // 시작일자
                //        //    dtDate = DateTime.Parse(cdvFromToDate.FromDate.Text);
                //        //    dtDate = dtDate.AddDays(-1);
                //        //    strFromDate = dtDate.ToString("yyyyMMdd") + "220001";
                //        //    // 정상
                //        //    if (rbtEoh01.Checked == true)
                //        //    {
                //        //        // 종료일자
                //        //        dtDate = DateTime.Parse(cdvFromToDate.ToDate.Text);
                //        //        dtDate = dtDate.AddDays(-1);
                //        //        strToDate = dtDate.ToString("yyyyMMdd") + "220000";
                //        //    }
                //        //    // 금일
                //        //    else
                //        //    {
                //        //        // 종료일자
                //        //        QRY = "(SELECT TO_CHAR(SYSDATE, 'YYYYMMDDHH24MISS') AS NOW_DATE FROM DUAL)";
                //        //        dt1 = CmnFunction.oComm.GetFuncDataTable("DYNAMIC", QRY);
                //        //        strToDate = dt1.Rows[0][0].ToString();
                //        //    }
                //        //}

                //        // 2009-03-24-정비재 : 기준정보 시간을 선택한다.
                //        if (rbTime01.Checked == true)
                //        {
                //            dtDate = DateTime.Parse(cdvFromToDate.FromDate.Text);
                //            dtDate = dtDate.AddDays(-1);
                //            strFromDate = dtDate.ToString("yyyyMMdd") + "220001";
                //            strToDate = cdvFromToDate.ToDate.Text.Replace("-", "") + "220000";
                //        }
                //        else if (rbTime02.Checked == true)
                //        {
                //            strFromDate = cdvFromToDate.FromDate.Text.Replace("-", "") + "060001";
                //            strToDate = cdvFromToDate.ToDate.Text.Replace("-", "") + "060000";
                //        }
                //        strFromCutOffDate = strFromDate.Substring(0, 10);
                //        strToCutOffDate = strToDate.Substring(0, 10);
                //        break;

                //    default:
                //        MessageBox.Show("결산기간은 일(Day) 단위로만 할 수 있습니다!" + "\r\n\r\n"
                //                      + "일(Day) 단위를 선택하여 주십시오!", this.Title, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                //        return "";
                //}


                strSqlString.Append("SELECT A.PIN_TYPE " + "\n");
                strSqlString.Append("     , SUM(A.A0000_WAIT) AS A0000_WAIT, SUM(A.A0000_PROC) AS A0000_PROC, SUM(A.A0020_WAIT) AS A0020_WAIT, SUM(A.A0020_PROC) AS A0020_PROC " + "\n");
                strSqlString.Append("     , SUM(A.A0040_WAIT) AS A0040_WAIT, SUM(A.A0040_PROC) AS A0040_PROC, SUM(A.A0100_WAIT) AS A0100_WAIT, SUM(A.A0100_PROC) AS A0100_PROC " + "\n");
                strSqlString.Append("     , SUM(A.A0150_WAIT) AS A0150_WAIT, SUM(A.A0150_PROC) AS A0150_PROC, SUM(A.A0200_WAIT) AS A0200_WAIT, SUM(A.A0200_PROC) AS A0200_PROC " + "\n");
                strSqlString.Append("     , SUM(A.A0250_WAIT) AS A0250_WAIT, SUM(A.A0250_PROC) AS A0250_PROC, SUM(A.A0300_WAIT) AS A0300_WAIT, SUM(A.A0300_PROC) AS A0300_PROC " + "\n");
                strSqlString.Append("     , SUM(A.A0320_WAIT) AS A0320_WAIT, SUM(A.A0320_PROC) AS A0320_PROC, SUM(A.A0330_WAIT) AS A0330_WAIT, SUM(A.A0330_PROC) AS A0330_PROC " + "\n");
                strSqlString.Append("     , SUM(A.A0340_WAIT) AS A0340_WAIT, SUM(A.A0340_PROC) AS A0340_PROC, SUM(A.A0350_WAIT) AS A0350_WAIT, SUM(A.A0350_PROC) AS A0350_PROC " + "\n");
                strSqlString.Append("     , SUM(A.A0360_WAIT) AS A0360_WAIT, SUM(A.A0360_PROC) AS A0360_PROC, SUM(A.A0370_WAIT) AS A0370_WAIT, SUM(A.A0370_PROC) AS A0370_PROC " + "\n");
                strSqlString.Append("     , SUM(A.A0380_WAIT) AS A0380_WAIT, SUM(A.A0380_PROC) AS A0380_PROC, SUM(A.A0390_WAIT) AS A0390_WAIT, SUM(A.A0390_PROC) AS A0390_PROC " + "\n");
                strSqlString.Append("     , SUM(A.A0400_WAIT) AS A0400_WAIT, SUM(A.A0400_PROC) AS A0400_PROC, SUM(A.A0401_WAIT) AS A0401_WAIT, SUM(A.A0401_PROC) AS A0401_PROC " + "\n");
                strSqlString.Append("     , SUM(A.A0402_WAIT) AS A0402_WAIT, SUM(A.A0402_PROC) AS A0402_PROC, SUM(A.A0403_WAIT) AS A0403_WAIT, SUM(A.A0403_PROC) AS A0403_PROC " + "\n");
                strSqlString.Append("     , SUM(A.A0404_WAIT) AS A0404_WAIT, SUM(A.A0404_PROC) AS A0404_PROC, SUM(A.A0405_WAIT) AS A0405_WAIT, SUM(A.A0405_PROC) AS A0405_PROC " + "\n");
                strSqlString.Append("     , SUM(A.A0406_WAIT) AS A0406_WAIT, SUM(A.A0406_PROC) AS A0406_PROC, SUM(A.A0407_WAIT) AS A0407_WAIT, SUM(A.A0407_PROC) AS A0407_PROC " + "\n");
                strSqlString.Append("     , SUM(A.A0408_WAIT) AS A0408_WAIT, SUM(A.A0408_PROC) AS A0408_PROC, SUM(A.A0409_WAIT) AS A0409_WAIT, SUM(A.A0409_PROC) AS A0409_PROC " + "\n");
                strSqlString.Append("     , SUM(A.A0500_WAIT) AS A0500_WAIT, SUM(A.A0500_PROC) AS A0500_PROC, SUM(A.A0501_WAIT) AS A0501_WAIT, SUM(A.A0501_PROC) AS A0501_PROC " + "\n");
                strSqlString.Append("     , SUM(A.A0502_WAIT) AS A0502_WAIT, SUM(A.A0502_PROC) AS A0502_PROC, SUM(A.A0503_WAIT) AS A0503_WAIT, SUM(A.A0503_PROC) AS A0503_PROC " + "\n");
                strSqlString.Append("     , SUM(A.A0504_WAIT) AS A0504_WAIT, SUM(A.A0504_PROC) AS A0504_PROC, SUM(A.A0505_WAIT) AS A0505_WAIT, SUM(A.A0505_PROC) AS A0505_PROC " + "\n");
                strSqlString.Append("     , SUM(A.A0506_WAIT) AS A0506_WAIT, SUM(A.A0506_PROC) AS A0506_PROC, SUM(A.A0507_WAIT) AS A0507_WAIT, SUM(A.A0507_PROC) AS A0507_PROC " + "\n");
                strSqlString.Append("     , SUM(A.A0508_WAIT) AS A0508_WAIT, SUM(A.A0508_PROC) AS A0508_PROC, SUM(A.A0509_WAIT) AS A0509_WAIT, SUM(A.A0509_PROC) AS A0509_PROC " + "\n");
                strSqlString.Append("     , SUM(A.A0540_WAIT) AS A0540_WAIT, SUM(A.A0540_PROC) AS A0540_PROC, SUM(A.A0550_WAIT) AS A0550_WAIT, SUM(A.A0550_PROC) AS A0550_PROC " + "\n");
                strSqlString.Append("     , SUM(A.A0551_WAIT) AS A0551_WAIT, SUM(A.A0551_PROC) AS A0551_PROC, SUM(A.A0552_WAIT) AS A0552_WAIT, SUM(A.A0552_PROC) AS A0552_PROC " + "\n");
                strSqlString.Append("     , SUM(A.A0553_WAIT) AS A0553_WAIT, SUM(A.A0553_PROC) AS A0553_PROC, SUM(A.A0554_WAIT) AS A0554_WAIT, SUM(A.A0554_PROC) AS A0554_PROC " + "\n");
                strSqlString.Append("     , SUM(A.A0555_WAIT) AS A0555_WAIT, SUM(A.A0555_PROC) AS A0555_PROC, SUM(A.A0556_WAIT) AS A0556_WAIT, SUM(A.A0556_PROC) AS A0556_PROC " + "\n");
                strSqlString.Append("     , SUM(A.A0557_WAIT) AS A0557_WAIT, SUM(A.A0557_PROC) AS A0557_PROC, SUM(A.A0558_WAIT) AS A0558_WAIT, SUM(A.A0558_PROC) AS A0558_PROC " + "\n");
                strSqlString.Append("     , SUM(A.A0559_WAIT) AS A0559_WAIT, SUM(A.A0559_PROC) AS A0559_PROC, SUM(A.A0600_WAIT) AS A0600_WAIT, SUM(A.A0600_PROC) AS A0600_PROC " + "\n");
                strSqlString.Append("     , SUM(A.A0601_WAIT) AS A0601_WAIT, SUM(A.A0601_PROC) AS A0601_PROC, SUM(A.A0602_WAIT) AS A0602_WAIT, SUM(A.A0602_PROC) AS A0602_PROC " + "\n");
                strSqlString.Append("     , SUM(A.A0603_WAIT) AS A0603_WAIT, SUM(A.A0603_PROC) AS A0603_PROC, SUM(A.A0604_WAIT) AS A0604_WAIT, SUM(A.A0604_PROC) AS A0604_PROC " + "\n");
                strSqlString.Append("     , SUM(A.A0605_WAIT) AS A0605_WAIT, SUM(A.A0605_PROC) AS A0605_PROC, SUM(A.A0606_WAIT) AS A0606_WAIT, SUM(A.A0606_PROC) AS A0606_PROC " + "\n");
                strSqlString.Append("     , SUM(A.A0607_WAIT) AS A0607_WAIT, SUM(A.A0607_PROC) AS A0607_PROC, SUM(A.A0608_WAIT) AS A0608_WAIT, SUM(A.A0608_PROC) AS A0608_PROC " + "\n");
                strSqlString.Append("     , SUM(A.A0609_WAIT) AS A0609_WAIT, SUM(A.A0609_PROC) AS A0609_PROC, SUM(A.A0620_WAIT) AS A0620_WAIT, SUM(A.A0620_PROC) AS A0620_PROC " + "\n");
                strSqlString.Append("     , SUM(A.A0630_WAIT) AS A0630_WAIT, SUM(A.A0630_PROC) AS A0630_PROC, SUM(A.A0650_WAIT) AS A0650_WAIT, SUM(A.A0650_PROC) AS A0650_PROC " + "\n");
                strSqlString.Append("     , SUM(A.A0670_WAIT) AS A0670_WAIT, SUM(A.A0670_PROC) AS A0670_PROC, SUM(A.A0800_WAIT) AS A0800_WAIT, SUM(A.A0800_PROC) AS A0800_PROC " + "\n");
                strSqlString.Append("     , SUM(A.A0801_WAIT) AS A0801_WAIT, SUM(A.A0801_PROC) AS A0801_PROC, SUM(A.A0802_WAIT) AS A0802_WAIT, SUM(A.A0802_PROC) AS A0802_PROC " + "\n");
                strSqlString.Append("     , SUM(A.A0803_WAIT) AS A0803_WAIT, SUM(A.A0803_PROC) AS A0803_PROC, SUM(A.A0804_WAIT) AS A0804_WAIT, SUM(A.A0804_PROC) AS A0804_PROC " + "\n");
                strSqlString.Append("     , SUM(A.A0805_WAIT) AS A0805_WAIT, SUM(A.A0805_PROC) AS A0805_PROC, SUM(A.A0806_WAIT) AS A0806_WAIT, SUM(A.A0806_PROC) AS A0806_PROC " + "\n");
                strSqlString.Append("     , SUM(A.A0807_WAIT) AS A0807_WAIT, SUM(A.A0807_PROC) AS A0807_PROC, SUM(A.A0808_WAIT) AS A0808_WAIT, SUM(A.A0808_PROC) AS A0808_PROC " + "\n");
                strSqlString.Append("     , SUM(A.A0809_WAIT) AS A0809_WAIT, SUM(A.A0809_PROC) AS A0809_PROC, SUM(A.A0950_WAIT) AS A0950_WAIT, SUM(A.A0950_PROC) AS A0950_PROC " + "\n");
                strSqlString.Append("     , SUM(A.A0970_WAIT) AS A0970_WAIT, SUM(A.A0970_PROC) AS A0970_PROC, SUM(A.A1000_WAIT) AS A1000_WAIT, SUM(A.A1000_PROC) AS A1000_PROC " + "\n");
                strSqlString.Append("     , SUM(A.A1070_WAIT) AS A1070_WAIT, SUM(A.A1070_PROC) AS A1070_PROC, SUM(A.A1100_WAIT) AS A1100_WAIT, SUM(A.A1100_PROC) AS A1100_PROC " + "\n");
                strSqlString.Append("     , SUM(A.A1110_WAIT) AS A1110_WAIT, SUM(A.A1110_PROC) AS A1110_PROC, SUM(A.A1120_WAIT) AS A1120_WAIT, SUM(A.A1120_PROC) AS A1120_PROC " + "\n");
                strSqlString.Append("     , SUM(A.A1130_WAIT) AS A1130_WAIT, SUM(A.A1130_PROC) AS A1130_PROC, SUM(A.A1150_WAIT) AS A1150_WAIT, SUM(A.A1150_PROC) AS A1150_PROC " + "\n");
                strSqlString.Append("     , SUM(A.A1170_WAIT) AS A1170_WAIT, SUM(A.A1170_PROC) AS A1170_PROC, SUM(A.A1180_WAIT) AS A1180_WAIT, SUM(A.A1180_PROC) AS A1180_PROC " + "\n");
                strSqlString.Append("     , SUM(A.A1200_WAIT) AS A1200_WAIT, SUM(A.A1200_PROC) AS A1200_PROC, SUM(A.A1230_WAIT) AS A1230_WAIT, SUM(A.A1230_PROC) AS A1230_PROC " + "\n");
                strSqlString.Append("     , SUM(A.A1250_WAIT) AS A1250_WAIT, SUM(A.A1250_PROC) AS A1250_PROC, SUM(A.A1300_WAIT) AS A1300_WAIT, SUM(A.A1300_PROC) AS A1300_PROC " + "\n");
                strSqlString.Append("     , SUM(A.A1450_WAIT) AS A1450_WAIT, SUM(A.A1450_PROC) AS A1450_PROC, SUM(A.A1470_WAIT) AS A1470_WAIT, SUM(A.A1470_PROC) AS A1470_PROC " + "\n");
                strSqlString.Append("     , SUM(A.A1500_WAIT) AS A1500_WAIT, SUM(A.A1500_PROC) AS A1500_PROC, SUM(A.A1720_WAIT) AS A1720_WAIT, SUM(A.A1720_PROC) AS A1720_PROC " + "\n");
                strSqlString.Append("     , SUM(A.A1750_WAIT) AS A1750_WAIT, SUM(A.A1750_PROC) AS A1750_PROC, SUM(A.A1770_WAIT) AS A1770_WAIT, SUM(A.A1770_PROC) AS A1770_PROC " + "\n");
                strSqlString.Append("     , SUM(A.A1800_WAIT) AS A1800_WAIT, SUM(A.A1800_PROC) AS A1800_PROC, SUM(A.A1900_WAIT) AS A1900_WAIT, SUM(A.A1900_PROC) AS A1900_PROC " + "\n");
                strSqlString.Append("     , SUM(A.A1950_WAIT) AS A1950_WAIT, SUM(A.A1950_PROC) AS A1950_PROC, SUM(A.A2000_WAIT) AS A2000_WAIT, SUM(A.A2000_PROC) AS A2000_PROC " + "\n");
                strSqlString.Append("     , SUM(A.A2100_WAIT) AS A2100_WAIT, SUM(A.A2100_PROC) AS A2100_PROC, SUM(A.A2300_WAIT) AS A2300_WAIT, SUM(A.A2300_PROC) AS A2300_PROC " + "\n");
                strSqlString.Append("     , SUM(A.AZ009_WAIT) AS AZ009_WAIT, SUM(A.AZ009_PROC) AS AZ009_PROC, SUM(A.AZ010_WAIT) AS AZ010_WAIT, SUM(A.AZ010_PROC) AS AZ010_PROC " + "\n");
                strSqlString.Append("     , SUM(A.T0000_WAIT) AS T0000_WAIT, SUM(A.T0000_PROC) AS T0000_PROC, SUM(A.T0100_WAIT) AS T0100_WAIT, SUM(A.T0100_PROC) AS T0100_PROC " + "\n");
                strSqlString.Append("     , SUM(A.T0300_WAIT) AS T0300_WAIT, SUM(A.T0300_PROC) AS T0300_PROC, SUM(A.T0500_WAIT) AS T0500_WAIT, SUM(A.T0500_PROC) AS T0500_PROC " + "\n");
                strSqlString.Append("     , SUM(A.T0540_WAIT) AS T0540_WAIT, SUM(A.T0540_PROC) AS T0540_PROC, SUM(A.T0550_WAIT) AS T0550_WAIT, SUM(A.T0550_PROC) AS T0550_PROC " + "\n");
                strSqlString.Append("     , SUM(A.T0560_WAIT) AS T0560_WAIT, SUM(A.T0560_PROC) AS T0560_PROC, SUM(A.T0600_WAIT) AS T0600_WAIT, SUM(A.T0600_PROC) AS T0600_PROC " + "\n");
                strSqlString.Append("     , SUM(A.T0650_WAIT) AS T0650_WAIT, SUM(A.T0650_PROC) AS T0650_PROC, SUM(A.T0670_WAIT) AS T0670_WAIT, SUM(A.T0670_PROC) AS T0670_PROC " + "\n");
                strSqlString.Append("     , SUM(A.T0700_WAIT) AS T0700_WAIT, SUM(A.T0700_PROC) AS T0700_PROC, SUM(A.T0800_WAIT) AS T0800_WAIT, SUM(A.T0800_PROC) AS T0800_PROC " + "\n");
                strSqlString.Append("     , SUM(A.T0900_WAIT) AS T0900_WAIT, SUM(A.T0900_PROC) AS T0900_PROC, SUM(A.T1040_WAIT) AS T1040_WAIT, SUM(A.T1040_PROC) AS T1040_PROC " + "\n");
                strSqlString.Append("     , SUM(A.T1080_WAIT) AS T1080_WAIT, SUM(A.T1080_PROC) AS T1080_PROC, SUM(A.T1100_WAIT) AS T1100_WAIT, SUM(A.T1100_PROC) AS T1100_PROC " + "\n");
                strSqlString.Append("     , SUM(A.T1200_WAIT) AS T1200_WAIT, SUM(A.T1200_PROC) AS T1200_PROC, SUM(A.T1300_WAIT) AS T1300_WAIT, SUM(A.T1300_PROC) AS T1300_PROC " + "\n");
                strSqlString.Append("     , SUM(A.TZ010_WAIT) AS TZ010_WAIT, SUM(A.TZ010_PROC) AS TZ010_PROC " + "\n");
                strSqlString.Append("     , SUM(A.F0000_WAIT) AS F0000_WAIT, SUM(A.F0000_PROC) AS F0000_PROC " + "\n");
                strSqlString.Append("  FROM ( " + "\n");
                strSqlString.Append("        SELECT B.MAT_CMF_10 AS PIN_TYPE " + "\n");
                strSqlString.Append("             , SUM(CASE A.OPER WHEN 'A0000' THEN DECODE(A.LOT_STATUS, 'WAIT', A.QTY_1, 0) ELSE 0 END) AS A0000_WAIT " + "\n");
                strSqlString.Append("             , SUM(CASE A.OPER WHEN 'A0000' THEN DECODE(A.LOT_STATUS, 'PROC', A.QTY_1, 0) ELSE 0 END) AS A0000_PROC " + "\n");
                strSqlString.Append("             , SUM(CASE A.OPER WHEN 'A0020' THEN DECODE(A.LOT_STATUS, 'WAIT', A.QTY_1, 0) ELSE 0 END) AS A0020_WAIT " + "\n");
                strSqlString.Append("             , SUM(CASE A.OPER WHEN 'A0020' THEN DECODE(A.LOT_STATUS, 'PROC', A.QTY_1, 0) ELSE 0 END) AS A0020_PROC " + "\n");
                strSqlString.Append("             , SUM(CASE A.OPER WHEN 'A0040' THEN DECODE(A.LOT_STATUS, 'WAIT', A.QTY_1, 0) ELSE 0 END) AS A0040_WAIT " + "\n");
                strSqlString.Append("             , SUM(CASE A.OPER WHEN 'A0040' THEN DECODE(A.LOT_STATUS, 'PROC', A.QTY_1, 0) ELSE 0 END) AS A0040_PROC " + "\n");
                strSqlString.Append("             , SUM(CASE A.OPER WHEN 'A0100' THEN DECODE(A.LOT_STATUS, 'WAIT', A.QTY_1, 0) ELSE 0 END) AS A0100_WAIT " + "\n");
                strSqlString.Append("             , SUM(CASE A.OPER WHEN 'A0100' THEN DECODE(A.LOT_STATUS, 'PROC', A.QTY_1, 0) ELSE 0 END) AS A0100_PROC " + "\n");
                strSqlString.Append("             , SUM(CASE A.OPER WHEN 'A0150' THEN DECODE(A.LOT_STATUS, 'WAIT', A.QTY_1, 0) ELSE 0 END) AS A0150_WAIT " + "\n");
                strSqlString.Append("             , SUM(CASE A.OPER WHEN 'A0150' THEN DECODE(A.LOT_STATUS, 'PROC', A.QTY_1, 0) ELSE 0 END) AS A0150_PROC " + "\n");
                strSqlString.Append("             , SUM(CASE A.OPER WHEN 'A0200' THEN DECODE(A.LOT_STATUS, 'WAIT', A.QTY_1, 0) ELSE 0 END) AS A0200_WAIT " + "\n");
                strSqlString.Append("             , SUM(CASE A.OPER WHEN 'A0200' THEN DECODE(A.LOT_STATUS, 'PROC', A.QTY_1, 0) ELSE 0 END) AS A0200_PROC " + "\n");
                strSqlString.Append("             , SUM(CASE A.OPER WHEN 'A0250' THEN DECODE(A.LOT_STATUS, 'WAIT', A.QTY_1, 0) ELSE 0 END) AS A0250_WAIT " + "\n");
                strSqlString.Append("             , SUM(CASE A.OPER WHEN 'A0250' THEN DECODE(A.LOT_STATUS, 'PROC', A.QTY_1, 0) ELSE 0 END) AS A0250_PROC " + "\n");
                strSqlString.Append("             , SUM(CASE A.OPER WHEN 'A0300' THEN DECODE(A.LOT_STATUS, 'WAIT', A.QTY_1, 0) ELSE 0 END) AS A0300_WAIT " + "\n");
                strSqlString.Append("             , SUM(CASE A.OPER WHEN 'A0300' THEN DECODE(A.LOT_STATUS, 'PROC', A.QTY_1, 0) ELSE 0 END) AS A0300_PROC " + "\n");
                strSqlString.Append("             , SUM(CASE A.OPER WHEN 'A0320' THEN DECODE(A.LOT_STATUS, 'WAIT', A.QTY_1, 0) ELSE 0 END) AS A0320_WAIT " + "\n");
                strSqlString.Append("             , SUM(CASE A.OPER WHEN 'A0320' THEN DECODE(A.LOT_STATUS, 'PROC', A.QTY_1, 0) ELSE 0 END) AS A0320_PROC " + "\n");
                strSqlString.Append("             , SUM(CASE A.OPER WHEN 'A0330' THEN DECODE(A.LOT_STATUS, 'WAIT', A.QTY_1, 0) ELSE 0 END) AS A0330_WAIT " + "\n");
                strSqlString.Append("             , SUM(CASE A.OPER WHEN 'A0330' THEN DECODE(A.LOT_STATUS, 'PROC', A.QTY_1, 0) ELSE 0 END) AS A0330_PROC " + "\n");
                strSqlString.Append("             , SUM(CASE A.OPER WHEN 'A0340' THEN DECODE(A.LOT_STATUS, 'WAIT', A.QTY_1, 0) ELSE 0 END) AS A0340_WAIT " + "\n");
                strSqlString.Append("             , SUM(CASE A.OPER WHEN 'A0340' THEN DECODE(A.LOT_STATUS, 'PROC', A.QTY_1, 0) ELSE 0 END) AS A0340_PROC " + "\n");
                strSqlString.Append("             , SUM(CASE A.OPER WHEN 'A0350' THEN DECODE(A.LOT_STATUS, 'WAIT', A.QTY_1, 0) ELSE 0 END) AS A0350_WAIT " + "\n");
                strSqlString.Append("             , SUM(CASE A.OPER WHEN 'A0350' THEN DECODE(A.LOT_STATUS, 'PROC', A.QTY_1, 0) ELSE 0 END) AS A0350_PROC " + "\n");
                strSqlString.Append("             , SUM(CASE A.OPER WHEN 'A0360' THEN DECODE(A.LOT_STATUS, 'WAIT', A.QTY_1, 0) ELSE 0 END) AS A0360_WAIT " + "\n");
                strSqlString.Append("             , SUM(CASE A.OPER WHEN 'A0360' THEN DECODE(A.LOT_STATUS, 'PROC', A.QTY_1, 0) ELSE 0 END) AS A0360_PROC " + "\n");
                strSqlString.Append("             , SUM(CASE A.OPER WHEN 'A0370' THEN DECODE(A.LOT_STATUS, 'WAIT', A.QTY_1, 0) ELSE 0 END) AS A0370_WAIT " + "\n");
                strSqlString.Append("             , SUM(CASE A.OPER WHEN 'A0370' THEN DECODE(A.LOT_STATUS, 'PROC', A.QTY_1, 0) ELSE 0 END) AS A0370_PROC " + "\n");
                strSqlString.Append("             , SUM(CASE A.OPER WHEN 'A0380' THEN DECODE(A.LOT_STATUS, 'WAIT', A.QTY_1, 0) ELSE 0 END) AS A0380_WAIT " + "\n");
                strSqlString.Append("             , SUM(CASE A.OPER WHEN 'A0380' THEN DECODE(A.LOT_STATUS, 'PROC', A.QTY_1, 0) ELSE 0 END) AS A0380_PROC " + "\n");
                strSqlString.Append("             , SUM(CASE A.OPER WHEN 'A0390' THEN DECODE(A.LOT_STATUS, 'WAIT', A.QTY_1, 0) ELSE 0 END) AS A0390_WAIT " + "\n");
                strSqlString.Append("             , SUM(CASE A.OPER WHEN 'A0390' THEN DECODE(A.LOT_STATUS, 'PROC', A.QTY_1, 0) ELSE 0 END) AS A0390_PROC " + "\n");
                strSqlString.Append("             , SUM(CASE A.OPER WHEN 'A0400' THEN DECODE(A.LOT_STATUS, 'WAIT', A.QTY_1, 0) ELSE 0 END) AS A0400_WAIT " + "\n");
                strSqlString.Append("             , SUM(CASE A.OPER WHEN 'A0400' THEN DECODE(A.LOT_STATUS, 'PROC', A.QTY_1, 0) ELSE 0 END) AS A0400_PROC " + "\n");
                strSqlString.Append("             , SUM(CASE A.OPER WHEN 'A0401' THEN DECODE(A.LOT_STATUS, 'WAIT', A.QTY_1, 0) ELSE 0 END) AS A0401_WAIT " + "\n");
                strSqlString.Append("             , SUM(CASE A.OPER WHEN 'A0401' THEN DECODE(A.LOT_STATUS, 'PROC', A.QTY_1, 0) ELSE 0 END) AS A0401_PROC " + "\n");
                strSqlString.Append("             , SUM(CASE A.OPER WHEN 'A0402' THEN DECODE(A.LOT_STATUS, 'WAIT', A.QTY_1, 0) ELSE 0 END) AS A0402_WAIT " + "\n");
                strSqlString.Append("             , SUM(CASE A.OPER WHEN 'A0402' THEN DECODE(A.LOT_STATUS, 'PROC', A.QTY_1, 0) ELSE 0 END) AS A0402_PROC " + "\n");
                strSqlString.Append("             , SUM(CASE A.OPER WHEN 'A0403' THEN DECODE(A.LOT_STATUS, 'WAIT', A.QTY_1, 0) ELSE 0 END) AS A0403_WAIT " + "\n");
                strSqlString.Append("             , SUM(CASE A.OPER WHEN 'A0403' THEN DECODE(A.LOT_STATUS, 'PROC', A.QTY_1, 0) ELSE 0 END) AS A0403_PROC " + "\n");
                strSqlString.Append("             , SUM(CASE A.OPER WHEN 'A0404' THEN DECODE(A.LOT_STATUS, 'WAIT', A.QTY_1, 0) ELSE 0 END) AS A0404_WAIT " + "\n");
                strSqlString.Append("             , SUM(CASE A.OPER WHEN 'A0404' THEN DECODE(A.LOT_STATUS, 'PROC', A.QTY_1, 0) ELSE 0 END) AS A0404_PROC " + "\n");
                strSqlString.Append("             , SUM(CASE A.OPER WHEN 'A0405' THEN DECODE(A.LOT_STATUS, 'WAIT', A.QTY_1, 0) ELSE 0 END) AS A0405_WAIT " + "\n");
                strSqlString.Append("             , SUM(CASE A.OPER WHEN 'A0405' THEN DECODE(A.LOT_STATUS, 'PROC', A.QTY_1, 0) ELSE 0 END) AS A0405_PROC " + "\n");
                strSqlString.Append("             , SUM(CASE A.OPER WHEN 'A0406' THEN DECODE(A.LOT_STATUS, 'WAIT', A.QTY_1, 0) ELSE 0 END) AS A0406_WAIT " + "\n");
                strSqlString.Append("             , SUM(CASE A.OPER WHEN 'A0406' THEN DECODE(A.LOT_STATUS, 'PROC', A.QTY_1, 0) ELSE 0 END) AS A0406_PROC " + "\n");
                strSqlString.Append("             , SUM(CASE A.OPER WHEN 'A0407' THEN DECODE(A.LOT_STATUS, 'WAIT', A.QTY_1, 0) ELSE 0 END) AS A0407_WAIT " + "\n");
                strSqlString.Append("             , SUM(CASE A.OPER WHEN 'A0407' THEN DECODE(A.LOT_STATUS, 'PROC', A.QTY_1, 0) ELSE 0 END) AS A0407_PROC " + "\n");
                strSqlString.Append("             , SUM(CASE A.OPER WHEN 'A0408' THEN DECODE(A.LOT_STATUS, 'WAIT', A.QTY_1, 0) ELSE 0 END) AS A0408_WAIT " + "\n");
                strSqlString.Append("             , SUM(CASE A.OPER WHEN 'A0408' THEN DECODE(A.LOT_STATUS, 'PROC', A.QTY_1, 0) ELSE 0 END) AS A0408_PROC " + "\n");
                strSqlString.Append("             , SUM(CASE A.OPER WHEN 'A0409' THEN DECODE(A.LOT_STATUS, 'WAIT', A.QTY_1, 0) ELSE 0 END) AS A0409_WAIT " + "\n");
                strSqlString.Append("             , SUM(CASE A.OPER WHEN 'A0409' THEN DECODE(A.LOT_STATUS, 'PROC', A.QTY_1, 0) ELSE 0 END) AS A0409_PROC " + "\n");
                strSqlString.Append("             , SUM(CASE A.OPER WHEN 'A0500' THEN DECODE(A.LOT_STATUS, 'WAIT', A.QTY_1, 0) ELSE 0 END) AS A0500_WAIT " + "\n");
                strSqlString.Append("             , SUM(CASE A.OPER WHEN 'A0500' THEN DECODE(A.LOT_STATUS, 'PROC', A.QTY_1, 0) ELSE 0 END) AS A0500_PROC " + "\n");
                strSqlString.Append("             , SUM(CASE A.OPER WHEN 'A0501' THEN DECODE(A.LOT_STATUS, 'WAIT', A.QTY_1, 0) ELSE 0 END) AS A0501_WAIT " + "\n");
                strSqlString.Append("             , SUM(CASE A.OPER WHEN 'A0501' THEN DECODE(A.LOT_STATUS, 'PROC', A.QTY_1, 0) ELSE 0 END) AS A0501_PROC " + "\n");
                strSqlString.Append("             , SUM(CASE A.OPER WHEN 'A0502' THEN DECODE(A.LOT_STATUS, 'WAIT', A.QTY_1, 0) ELSE 0 END) AS A0502_WAIT " + "\n");
                strSqlString.Append("             , SUM(CASE A.OPER WHEN 'A0502' THEN DECODE(A.LOT_STATUS, 'PROC', A.QTY_1, 0) ELSE 0 END) AS A0502_PROC " + "\n");
                strSqlString.Append("             , SUM(CASE A.OPER WHEN 'A0503' THEN DECODE(A.LOT_STATUS, 'WAIT', A.QTY_1, 0) ELSE 0 END) AS A0503_WAIT " + "\n");
                strSqlString.Append("             , SUM(CASE A.OPER WHEN 'A0503' THEN DECODE(A.LOT_STATUS, 'PROC', A.QTY_1, 0) ELSE 0 END) AS A0503_PROC " + "\n");
                strSqlString.Append("             , SUM(CASE A.OPER WHEN 'A0504' THEN DECODE(A.LOT_STATUS, 'WAIT', A.QTY_1, 0) ELSE 0 END) AS A0504_WAIT " + "\n");
                strSqlString.Append("             , SUM(CASE A.OPER WHEN 'A0504' THEN DECODE(A.LOT_STATUS, 'PROC', A.QTY_1, 0) ELSE 0 END) AS A0504_PROC " + "\n");
                strSqlString.Append("             , SUM(CASE A.OPER WHEN 'A0505' THEN DECODE(A.LOT_STATUS, 'WAIT', A.QTY_1, 0) ELSE 0 END) AS A0505_WAIT " + "\n");
                strSqlString.Append("             , SUM(CASE A.OPER WHEN 'A0505' THEN DECODE(A.LOT_STATUS, 'PROC', A.QTY_1, 0) ELSE 0 END) AS A0505_PROC " + "\n");
                strSqlString.Append("             , SUM(CASE A.OPER WHEN 'A0506' THEN DECODE(A.LOT_STATUS, 'WAIT', A.QTY_1, 0) ELSE 0 END) AS A0506_WAIT " + "\n");
                strSqlString.Append("             , SUM(CASE A.OPER WHEN 'A0506' THEN DECODE(A.LOT_STATUS, 'PROC', A.QTY_1, 0) ELSE 0 END) AS A0506_PROC " + "\n");
                strSqlString.Append("             , SUM(CASE A.OPER WHEN 'A0507' THEN DECODE(A.LOT_STATUS, 'WAIT', A.QTY_1, 0) ELSE 0 END) AS A0507_WAIT " + "\n");
                strSqlString.Append("             , SUM(CASE A.OPER WHEN 'A0507' THEN DECODE(A.LOT_STATUS, 'PROC', A.QTY_1, 0) ELSE 0 END) AS A0507_PROC " + "\n");
                strSqlString.Append("             , SUM(CASE A.OPER WHEN 'A0508' THEN DECODE(A.LOT_STATUS, 'WAIT', A.QTY_1, 0) ELSE 0 END) AS A0508_WAIT " + "\n");
                strSqlString.Append("             , SUM(CASE A.OPER WHEN 'A0508' THEN DECODE(A.LOT_STATUS, 'PROC', A.QTY_1, 0) ELSE 0 END) AS A0508_PROC " + "\n");
                strSqlString.Append("             , SUM(CASE A.OPER WHEN 'A0509' THEN DECODE(A.LOT_STATUS, 'WAIT', A.QTY_1, 0) ELSE 0 END) AS A0509_WAIT " + "\n");
                strSqlString.Append("             , SUM(CASE A.OPER WHEN 'A0509' THEN DECODE(A.LOT_STATUS, 'PROC', A.QTY_1, 0) ELSE 0 END) AS A0509_PROC " + "\n");
                strSqlString.Append("             , SUM(CASE A.OPER WHEN 'A0540' THEN DECODE(A.LOT_STATUS, 'WAIT', A.QTY_1, 0) ELSE 0 END) AS A0540_WAIT " + "\n");
                strSqlString.Append("             , SUM(CASE A.OPER WHEN 'A0540' THEN DECODE(A.LOT_STATUS, 'PROC', A.QTY_1, 0) ELSE 0 END) AS A0540_PROC " + "\n");
                strSqlString.Append("             , SUM(CASE A.OPER WHEN 'A0550' THEN DECODE(A.LOT_STATUS, 'WAIT', A.QTY_1, 0) ELSE 0 END) AS A0550_WAIT " + "\n");
                strSqlString.Append("             , SUM(CASE A.OPER WHEN 'A0550' THEN DECODE(A.LOT_STATUS, 'PROC', A.QTY_1, 0) ELSE 0 END) AS A0550_PROC " + "\n");
                strSqlString.Append("             , SUM(CASE A.OPER WHEN 'A0551' THEN DECODE(A.LOT_STATUS, 'WAIT', A.QTY_1, 0) ELSE 0 END) AS A0551_WAIT " + "\n");
                strSqlString.Append("             , SUM(CASE A.OPER WHEN 'A0551' THEN DECODE(A.LOT_STATUS, 'PROC', A.QTY_1, 0) ELSE 0 END) AS A0551_PROC " + "\n");
                strSqlString.Append("             , SUM(CASE A.OPER WHEN 'A0552' THEN DECODE(A.LOT_STATUS, 'WAIT', A.QTY_1, 0) ELSE 0 END) AS A0552_WAIT " + "\n");
                strSqlString.Append("             , SUM(CASE A.OPER WHEN 'A0552' THEN DECODE(A.LOT_STATUS, 'PROC', A.QTY_1, 0) ELSE 0 END) AS A0552_PROC " + "\n");
                strSqlString.Append("             , SUM(CASE A.OPER WHEN 'A0553' THEN DECODE(A.LOT_STATUS, 'WAIT', A.QTY_1, 0) ELSE 0 END) AS A0553_WAIT " + "\n");
                strSqlString.Append("             , SUM(CASE A.OPER WHEN 'A0553' THEN DECODE(A.LOT_STATUS, 'PROC', A.QTY_1, 0) ELSE 0 END) AS A0553_PROC " + "\n");
                strSqlString.Append("             , SUM(CASE A.OPER WHEN 'A0554' THEN DECODE(A.LOT_STATUS, 'WAIT', A.QTY_1, 0) ELSE 0 END) AS A0554_WAIT " + "\n");
                strSqlString.Append("             , SUM(CASE A.OPER WHEN 'A0554' THEN DECODE(A.LOT_STATUS, 'PROC', A.QTY_1, 0) ELSE 0 END) AS A0554_PROC " + "\n");
                strSqlString.Append("             , SUM(CASE A.OPER WHEN 'A0555' THEN DECODE(A.LOT_STATUS, 'WAIT', A.QTY_1, 0) ELSE 0 END) AS A0555_WAIT " + "\n");
                strSqlString.Append("             , SUM(CASE A.OPER WHEN 'A0555' THEN DECODE(A.LOT_STATUS, 'PROC', A.QTY_1, 0) ELSE 0 END) AS A0555_PROC " + "\n");
                strSqlString.Append("             , SUM(CASE A.OPER WHEN 'A0556' THEN DECODE(A.LOT_STATUS, 'WAIT', A.QTY_1, 0) ELSE 0 END) AS A0556_WAIT " + "\n");
                strSqlString.Append("             , SUM(CASE A.OPER WHEN 'A0556' THEN DECODE(A.LOT_STATUS, 'PROC', A.QTY_1, 0) ELSE 0 END) AS A0556_PROC " + "\n");
                strSqlString.Append("             , SUM(CASE A.OPER WHEN 'A0557' THEN DECODE(A.LOT_STATUS, 'WAIT', A.QTY_1, 0) ELSE 0 END) AS A0557_WAIT " + "\n");
                strSqlString.Append("             , SUM(CASE A.OPER WHEN 'A0557' THEN DECODE(A.LOT_STATUS, 'PROC', A.QTY_1, 0) ELSE 0 END) AS A0557_PROC " + "\n");
                strSqlString.Append("             , SUM(CASE A.OPER WHEN 'A0558' THEN DECODE(A.LOT_STATUS, 'WAIT', A.QTY_1, 0) ELSE 0 END) AS A0558_WAIT " + "\n");
                strSqlString.Append("             , SUM(CASE A.OPER WHEN 'A0558' THEN DECODE(A.LOT_STATUS, 'PROC', A.QTY_1, 0) ELSE 0 END) AS A0558_PROC " + "\n");
                strSqlString.Append("             , SUM(CASE A.OPER WHEN 'A0559' THEN DECODE(A.LOT_STATUS, 'WAIT', A.QTY_1, 0) ELSE 0 END) AS A0559_WAIT " + "\n");
                strSqlString.Append("             , SUM(CASE A.OPER WHEN 'A0559' THEN DECODE(A.LOT_STATUS, 'PROC', A.QTY_1, 0) ELSE 0 END) AS A0559_PROC " + "\n");
                strSqlString.Append("             , SUM(CASE A.OPER WHEN 'A0600' THEN DECODE(A.LOT_STATUS, 'WAIT', A.QTY_1, 0) ELSE 0 END) AS A0600_WAIT " + "\n");
                strSqlString.Append("             , SUM(CASE A.OPER WHEN 'A0600' THEN DECODE(A.LOT_STATUS, 'PROC', A.QTY_1, 0) ELSE 0 END) AS A0600_PROC " + "\n");
                strSqlString.Append("             , SUM(CASE A.OPER WHEN 'A0601' THEN DECODE(A.LOT_STATUS, 'WAIT', A.QTY_1, 0) ELSE 0 END) AS A0601_WAIT " + "\n");
                strSqlString.Append("             , SUM(CASE A.OPER WHEN 'A0601' THEN DECODE(A.LOT_STATUS, 'PROC', A.QTY_1, 0) ELSE 0 END) AS A0601_PROC " + "\n");
                strSqlString.Append("             , SUM(CASE A.OPER WHEN 'A0602' THEN DECODE(A.LOT_STATUS, 'WAIT', A.QTY_1, 0) ELSE 0 END) AS A0602_WAIT " + "\n");
                strSqlString.Append("             , SUM(CASE A.OPER WHEN 'A0602' THEN DECODE(A.LOT_STATUS, 'PROC', A.QTY_1, 0) ELSE 0 END) AS A0602_PROC " + "\n");
                strSqlString.Append("             , SUM(CASE A.OPER WHEN 'A0603' THEN DECODE(A.LOT_STATUS, 'WAIT', A.QTY_1, 0) ELSE 0 END) AS A0603_WAIT " + "\n");
                strSqlString.Append("             , SUM(CASE A.OPER WHEN 'A0603' THEN DECODE(A.LOT_STATUS, 'PROC', A.QTY_1, 0) ELSE 0 END) AS A0603_PROC " + "\n");
                strSqlString.Append("             , SUM(CASE A.OPER WHEN 'A0604' THEN DECODE(A.LOT_STATUS, 'WAIT', A.QTY_1, 0) ELSE 0 END) AS A0604_WAIT " + "\n");
                strSqlString.Append("             , SUM(CASE A.OPER WHEN 'A0604' THEN DECODE(A.LOT_STATUS, 'PROC', A.QTY_1, 0) ELSE 0 END) AS A0604_PROC " + "\n");
                strSqlString.Append("             , SUM(CASE A.OPER WHEN 'A0605' THEN DECODE(A.LOT_STATUS, 'WAIT', A.QTY_1, 0) ELSE 0 END) AS A0605_WAIT " + "\n");
                strSqlString.Append("             , SUM(CASE A.OPER WHEN 'A0605' THEN DECODE(A.LOT_STATUS, 'PROC', A.QTY_1, 0) ELSE 0 END) AS A0605_PROC " + "\n");
                strSqlString.Append("             , SUM(CASE A.OPER WHEN 'A0606' THEN DECODE(A.LOT_STATUS, 'WAIT', A.QTY_1, 0) ELSE 0 END) AS A0606_WAIT " + "\n");
                strSqlString.Append("             , SUM(CASE A.OPER WHEN 'A0606' THEN DECODE(A.LOT_STATUS, 'PROC', A.QTY_1, 0) ELSE 0 END) AS A0606_PROC " + "\n");
                strSqlString.Append("             , SUM(CASE A.OPER WHEN 'A0607' THEN DECODE(A.LOT_STATUS, 'WAIT', A.QTY_1, 0) ELSE 0 END) AS A0607_WAIT " + "\n");
                strSqlString.Append("             , SUM(CASE A.OPER WHEN 'A0607' THEN DECODE(A.LOT_STATUS, 'PROC', A.QTY_1, 0) ELSE 0 END) AS A0607_PROC " + "\n");
                strSqlString.Append("             , SUM(CASE A.OPER WHEN 'A0608' THEN DECODE(A.LOT_STATUS, 'WAIT', A.QTY_1, 0) ELSE 0 END) AS A0608_WAIT " + "\n");
                strSqlString.Append("             , SUM(CASE A.OPER WHEN 'A0608' THEN DECODE(A.LOT_STATUS, 'PROC', A.QTY_1, 0) ELSE 0 END) AS A0608_PROC " + "\n");
                strSqlString.Append("             , SUM(CASE A.OPER WHEN 'A0609' THEN DECODE(A.LOT_STATUS, 'WAIT', A.QTY_1, 0) ELSE 0 END) AS A0609_WAIT " + "\n");
                strSqlString.Append("             , SUM(CASE A.OPER WHEN 'A0609' THEN DECODE(A.LOT_STATUS, 'PROC', A.QTY_1, 0) ELSE 0 END) AS A0609_PROC " + "\n");
                strSqlString.Append("             , SUM(CASE A.OPER WHEN 'A0620' THEN DECODE(A.LOT_STATUS, 'WAIT', A.QTY_1, 0) ELSE 0 END) AS A0620_WAIT " + "\n");
                strSqlString.Append("             , SUM(CASE A.OPER WHEN 'A0620' THEN DECODE(A.LOT_STATUS, 'PROC', A.QTY_1, 0) ELSE 0 END) AS A0620_PROC " + "\n");
                strSqlString.Append("             , SUM(CASE A.OPER WHEN 'A0630' THEN DECODE(A.LOT_STATUS, 'WAIT', A.QTY_1, 0) ELSE 0 END) AS A0630_WAIT " + "\n");
                strSqlString.Append("             , SUM(CASE A.OPER WHEN 'A0630' THEN DECODE(A.LOT_STATUS, 'PROC', A.QTY_1, 0) ELSE 0 END) AS A0630_PROC " + "\n");
                strSqlString.Append("             , SUM(CASE A.OPER WHEN 'A0650' THEN DECODE(A.LOT_STATUS, 'WAIT', A.QTY_1, 0) ELSE 0 END) AS A0650_WAIT " + "\n");
                strSqlString.Append("             , SUM(CASE A.OPER WHEN 'A0650' THEN DECODE(A.LOT_STATUS, 'PROC', A.QTY_1, 0) ELSE 0 END) AS A0650_PROC " + "\n");
                strSqlString.Append("             , SUM(CASE A.OPER WHEN 'A0670' THEN DECODE(A.LOT_STATUS, 'WAIT', A.QTY_1, 0) ELSE 0 END) AS A0670_WAIT " + "\n");
                strSqlString.Append("             , SUM(CASE A.OPER WHEN 'A0670' THEN DECODE(A.LOT_STATUS, 'PROC', A.QTY_1, 0) ELSE 0 END) AS A0670_PROC " + "\n");
                strSqlString.Append("             , SUM(CASE A.OPER WHEN 'A0800' THEN DECODE(A.LOT_STATUS, 'WAIT', A.QTY_1, 0) ELSE 0 END) AS A0800_WAIT " + "\n");
                strSqlString.Append("             , SUM(CASE A.OPER WHEN 'A0800' THEN DECODE(A.LOT_STATUS, 'PROC', A.QTY_1, 0) ELSE 0 END) AS A0800_PROC " + "\n");
                strSqlString.Append("             , SUM(CASE A.OPER WHEN 'A0801' THEN DECODE(A.LOT_STATUS, 'WAIT', A.QTY_1, 0) ELSE 0 END) AS A0801_WAIT " + "\n");
                strSqlString.Append("             , SUM(CASE A.OPER WHEN 'A0801' THEN DECODE(A.LOT_STATUS, 'PROC', A.QTY_1, 0) ELSE 0 END) AS A0801_PROC " + "\n");
                strSqlString.Append("             , SUM(CASE A.OPER WHEN 'A0802' THEN DECODE(A.LOT_STATUS, 'WAIT', A.QTY_1, 0) ELSE 0 END) AS A0802_WAIT " + "\n");
                strSqlString.Append("             , SUM(CASE A.OPER WHEN 'A0802' THEN DECODE(A.LOT_STATUS, 'PROC', A.QTY_1, 0) ELSE 0 END) AS A0802_PROC " + "\n");
                strSqlString.Append("             , SUM(CASE A.OPER WHEN 'A0803' THEN DECODE(A.LOT_STATUS, 'WAIT', A.QTY_1, 0) ELSE 0 END) AS A0803_WAIT " + "\n");
                strSqlString.Append("             , SUM(CASE A.OPER WHEN 'A0803' THEN DECODE(A.LOT_STATUS, 'PROC', A.QTY_1, 0) ELSE 0 END) AS A0803_PROC " + "\n");
                strSqlString.Append("             , SUM(CASE A.OPER WHEN 'A0804' THEN DECODE(A.LOT_STATUS, 'WAIT', A.QTY_1, 0) ELSE 0 END) AS A0804_WAIT " + "\n");
                strSqlString.Append("             , SUM(CASE A.OPER WHEN 'A0804' THEN DECODE(A.LOT_STATUS, 'PROC', A.QTY_1, 0) ELSE 0 END) AS A0804_PROC " + "\n");
                strSqlString.Append("             , SUM(CASE A.OPER WHEN 'A0805' THEN DECODE(A.LOT_STATUS, 'WAIT', A.QTY_1, 0) ELSE 0 END) AS A0805_WAIT " + "\n");
                strSqlString.Append("             , SUM(CASE A.OPER WHEN 'A0805' THEN DECODE(A.LOT_STATUS, 'PROC', A.QTY_1, 0) ELSE 0 END) AS A0805_PROC " + "\n");
                strSqlString.Append("             , SUM(CASE A.OPER WHEN 'A0806' THEN DECODE(A.LOT_STATUS, 'WAIT', A.QTY_1, 0) ELSE 0 END) AS A0806_WAIT " + "\n");
                strSqlString.Append("             , SUM(CASE A.OPER WHEN 'A0806' THEN DECODE(A.LOT_STATUS, 'PROC', A.QTY_1, 0) ELSE 0 END) AS A0806_PROC " + "\n");
                strSqlString.Append("             , SUM(CASE A.OPER WHEN 'A0807' THEN DECODE(A.LOT_STATUS, 'WAIT', A.QTY_1, 0) ELSE 0 END) AS A0807_WAIT " + "\n");
                strSqlString.Append("             , SUM(CASE A.OPER WHEN 'A0807' THEN DECODE(A.LOT_STATUS, 'PROC', A.QTY_1, 0) ELSE 0 END) AS A0807_PROC " + "\n");
                strSqlString.Append("             , SUM(CASE A.OPER WHEN 'A0808' THEN DECODE(A.LOT_STATUS, 'WAIT', A.QTY_1, 0) ELSE 0 END) AS A0808_WAIT " + "\n");
                strSqlString.Append("             , SUM(CASE A.OPER WHEN 'A0808' THEN DECODE(A.LOT_STATUS, 'PROC', A.QTY_1, 0) ELSE 0 END) AS A0808_PROC " + "\n");
                strSqlString.Append("             , SUM(CASE A.OPER WHEN 'A0809' THEN DECODE(A.LOT_STATUS, 'WAIT', A.QTY_1, 0) ELSE 0 END) AS A0809_WAIT " + "\n");
                strSqlString.Append("             , SUM(CASE A.OPER WHEN 'A0809' THEN DECODE(A.LOT_STATUS, 'PROC', A.QTY_1, 0) ELSE 0 END) AS A0809_PROC " + "\n");
                strSqlString.Append("             , SUM(CASE A.OPER WHEN 'A0950' THEN DECODE(A.LOT_STATUS, 'WAIT', A.QTY_1, 0) ELSE 0 END) AS A0950_WAIT " + "\n");
                strSqlString.Append("             , SUM(CASE A.OPER WHEN 'A0950' THEN DECODE(A.LOT_STATUS, 'PROC', A.QTY_1, 0) ELSE 0 END) AS A0950_PROC " + "\n");
                strSqlString.Append("             , SUM(CASE A.OPER WHEN 'A0970' THEN DECODE(A.LOT_STATUS, 'WAIT', A.QTY_1, 0) ELSE 0 END) AS A0970_WAIT " + "\n");
                strSqlString.Append("             , SUM(CASE A.OPER WHEN 'A0970' THEN DECODE(A.LOT_STATUS, 'PROC', A.QTY_1, 0) ELSE 0 END) AS A0970_PROC " + "\n");
                strSqlString.Append("             , SUM(CASE A.OPER WHEN 'A1000' THEN DECODE(A.LOT_STATUS, 'WAIT', A.QTY_1, 0) ELSE 0 END) AS A1000_WAIT " + "\n");
                strSqlString.Append("             , SUM(CASE A.OPER WHEN 'A1000' THEN DECODE(A.LOT_STATUS, 'PROC', A.QTY_1, 0) ELSE 0 END) AS A1000_PROC " + "\n");
                strSqlString.Append("             , SUM(CASE A.OPER WHEN 'A1070' THEN DECODE(A.LOT_STATUS, 'WAIT', A.QTY_1, 0) ELSE 0 END) AS A1070_WAIT " + "\n");
                strSqlString.Append("             , SUM(CASE A.OPER WHEN 'A1070' THEN DECODE(A.LOT_STATUS, 'PROC', A.QTY_1, 0) ELSE 0 END) AS A1070_PROC " + "\n");
                strSqlString.Append("             , SUM(CASE A.OPER WHEN 'A1100' THEN DECODE(A.LOT_STATUS, 'WAIT', A.QTY_1, 0) ELSE 0 END) AS A1100_WAIT " + "\n");
                strSqlString.Append("             , SUM(CASE A.OPER WHEN 'A1100' THEN DECODE(A.LOT_STATUS, 'PROC', A.QTY_1, 0) ELSE 0 END) AS A1100_PROC " + "\n");
                strSqlString.Append("             , SUM(CASE A.OPER WHEN 'A1110' THEN DECODE(A.LOT_STATUS, 'WAIT', A.QTY_1, 0) ELSE 0 END) AS A1110_WAIT " + "\n");
                strSqlString.Append("             , SUM(CASE A.OPER WHEN 'A1110' THEN DECODE(A.LOT_STATUS, 'PROC', A.QTY_1, 0) ELSE 0 END) AS A1110_PROC " + "\n");
                strSqlString.Append("             , SUM(CASE A.OPER WHEN 'A1120' THEN DECODE(A.LOT_STATUS, 'WAIT', A.QTY_1, 0) ELSE 0 END) AS A1120_WAIT " + "\n");
                strSqlString.Append("             , SUM(CASE A.OPER WHEN 'A1120' THEN DECODE(A.LOT_STATUS, 'PROC', A.QTY_1, 0) ELSE 0 END) AS A1120_PROC " + "\n");
                strSqlString.Append("             , SUM(CASE A.OPER WHEN 'A1130' THEN DECODE(A.LOT_STATUS, 'WAIT', A.QTY_1, 0) ELSE 0 END) AS A1130_WAIT " + "\n");
                strSqlString.Append("             , SUM(CASE A.OPER WHEN 'A1130' THEN DECODE(A.LOT_STATUS, 'PROC', A.QTY_1, 0) ELSE 0 END) AS A1130_PROC " + "\n");
                strSqlString.Append("             , SUM(CASE A.OPER WHEN 'A1150' THEN DECODE(A.LOT_STATUS, 'WAIT', A.QTY_1, 0) ELSE 0 END) AS A1150_WAIT " + "\n");
                strSqlString.Append("             , SUM(CASE A.OPER WHEN 'A1150' THEN DECODE(A.LOT_STATUS, 'PROC', A.QTY_1, 0) ELSE 0 END) AS A1150_PROC " + "\n");
                strSqlString.Append("             , SUM(CASE A.OPER WHEN 'A1170' THEN DECODE(A.LOT_STATUS, 'WAIT', A.QTY_1, 0) ELSE 0 END) AS A1170_WAIT " + "\n");
                strSqlString.Append("             , SUM(CASE A.OPER WHEN 'A1170' THEN DECODE(A.LOT_STATUS, 'PROC', A.QTY_1, 0) ELSE 0 END) AS A1170_PROC " + "\n");
                strSqlString.Append("             , SUM(CASE A.OPER WHEN 'A1180' THEN DECODE(A.LOT_STATUS, 'WAIT', A.QTY_1, 0) ELSE 0 END) AS A1180_WAIT " + "\n");
                strSqlString.Append("             , SUM(CASE A.OPER WHEN 'A1180' THEN DECODE(A.LOT_STATUS, 'PROC', A.QTY_1, 0) ELSE 0 END) AS A1180_PROC " + "\n");
                strSqlString.Append("             , SUM(CASE A.OPER WHEN 'A1200' THEN DECODE(A.LOT_STATUS, 'WAIT', A.QTY_1, 0) ELSE 0 END) AS A1200_WAIT " + "\n");
                strSqlString.Append("             , SUM(CASE A.OPER WHEN 'A1200' THEN DECODE(A.LOT_STATUS, 'PROC', A.QTY_1, 0) ELSE 0 END) AS A1200_PROC " + "\n");
                strSqlString.Append("             , SUM(CASE A.OPER WHEN 'A1230' THEN DECODE(A.LOT_STATUS, 'WAIT', A.QTY_1, 0) ELSE 0 END) AS A1230_WAIT " + "\n");
                strSqlString.Append("             , SUM(CASE A.OPER WHEN 'A1230' THEN DECODE(A.LOT_STATUS, 'PROC', A.QTY_1, 0) ELSE 0 END) AS A1230_PROC " + "\n");
                strSqlString.Append("             , SUM(CASE A.OPER WHEN 'A1250' THEN DECODE(A.LOT_STATUS, 'WAIT', A.QTY_1, 0) ELSE 0 END) AS A1250_WAIT " + "\n");
                strSqlString.Append("             , SUM(CASE A.OPER WHEN 'A1250' THEN DECODE(A.LOT_STATUS, 'PROC', A.QTY_1, 0) ELSE 0 END) AS A1250_PROC " + "\n");
                strSqlString.Append("             , SUM(CASE A.OPER WHEN 'A1300' THEN DECODE(A.LOT_STATUS, 'WAIT', A.QTY_1, 0) ELSE 0 END) AS A1300_WAIT " + "\n");
                strSqlString.Append("             , SUM(CASE A.OPER WHEN 'A1300' THEN DECODE(A.LOT_STATUS, 'PROC', A.QTY_1, 0) ELSE 0 END) AS A1300_PROC " + "\n");
                strSqlString.Append("             , SUM(CASE A.OPER WHEN 'A1450' THEN DECODE(A.LOT_STATUS, 'WAIT', A.QTY_1, 0) ELSE 0 END) AS A1450_WAIT " + "\n");
                strSqlString.Append("             , SUM(CASE A.OPER WHEN 'A1450' THEN DECODE(A.LOT_STATUS, 'PROC', A.QTY_1, 0) ELSE 0 END) AS A1450_PROC " + "\n");
                strSqlString.Append("             , SUM(CASE A.OPER WHEN 'A1470' THEN DECODE(A.LOT_STATUS, 'WAIT', A.QTY_1, 0) ELSE 0 END) AS A1470_WAIT " + "\n");
                strSqlString.Append("             , SUM(CASE A.OPER WHEN 'A1470' THEN DECODE(A.LOT_STATUS, 'PROC', A.QTY_1, 0) ELSE 0 END) AS A1470_PROC " + "\n");
                strSqlString.Append("             , SUM(CASE A.OPER WHEN 'A1500' THEN DECODE(A.LOT_STATUS, 'WAIT', A.QTY_1, 0) ELSE 0 END) AS A1500_WAIT " + "\n");
                strSqlString.Append("             , SUM(CASE A.OPER WHEN 'A1500' THEN DECODE(A.LOT_STATUS, 'PROC', A.QTY_1, 0) ELSE 0 END) AS A1500_PROC " + "\n");
                strSqlString.Append("             , SUM(CASE A.OPER WHEN 'A1720' THEN DECODE(A.LOT_STATUS, 'WAIT', A.QTY_1, 0) ELSE 0 END) AS A1720_WAIT " + "\n");
                strSqlString.Append("             , SUM(CASE A.OPER WHEN 'A1720' THEN DECODE(A.LOT_STATUS, 'PROC', A.QTY_1, 0) ELSE 0 END) AS A1720_PROC " + "\n");
                strSqlString.Append("             , SUM(CASE A.OPER WHEN 'A1750' THEN DECODE(A.LOT_STATUS, 'WAIT', A.QTY_1, 0) ELSE 0 END) AS A1750_WAIT " + "\n");
                strSqlString.Append("             , SUM(CASE A.OPER WHEN 'A1750' THEN DECODE(A.LOT_STATUS, 'PROC', A.QTY_1, 0) ELSE 0 END) AS A1750_PROC " + "\n");
                strSqlString.Append("             , SUM(CASE A.OPER WHEN 'A1770' THEN DECODE(A.LOT_STATUS, 'WAIT', A.QTY_1, 0) ELSE 0 END) AS A1770_WAIT " + "\n");
                strSqlString.Append("             , SUM(CASE A.OPER WHEN 'A1770' THEN DECODE(A.LOT_STATUS, 'PROC', A.QTY_1, 0) ELSE 0 END) AS A1770_PROC " + "\n");
                strSqlString.Append("             , SUM(CASE A.OPER WHEN 'A1800' THEN DECODE(A.LOT_STATUS, 'WAIT', A.QTY_1, 0) ELSE 0 END) AS A1800_WAIT " + "\n");
                strSqlString.Append("             , SUM(CASE A.OPER WHEN 'A1800' THEN DECODE(A.LOT_STATUS, 'PROC', A.QTY_1, 0) ELSE 0 END) AS A1800_PROC " + "\n");
                strSqlString.Append("             , SUM(CASE A.OPER WHEN 'A1900' THEN DECODE(A.LOT_STATUS, 'WAIT', A.QTY_1, 0) ELSE 0 END) AS A1900_WAIT " + "\n");
                strSqlString.Append("             , SUM(CASE A.OPER WHEN 'A1900' THEN DECODE(A.LOT_STATUS, 'PROC', A.QTY_1, 0) ELSE 0 END) AS A1900_PROC " + "\n");
                strSqlString.Append("             , SUM(CASE A.OPER WHEN 'A1950' THEN DECODE(A.LOT_STATUS, 'WAIT', A.QTY_1, 0) ELSE 0 END) AS A1950_WAIT " + "\n");
                strSqlString.Append("             , SUM(CASE A.OPER WHEN 'A1950' THEN DECODE(A.LOT_STATUS, 'PROC', A.QTY_1, 0) ELSE 0 END) AS A1950_PROC " + "\n");
                strSqlString.Append("             , SUM(CASE A.OPER WHEN 'A2000' THEN DECODE(A.LOT_STATUS, 'WAIT', A.QTY_1, 0) ELSE 0 END) AS A2000_WAIT " + "\n");
                strSqlString.Append("             , SUM(CASE A.OPER WHEN 'A2000' THEN DECODE(A.LOT_STATUS, 'PROC', A.QTY_1, 0) ELSE 0 END) AS A2000_PROC " + "\n");
                strSqlString.Append("             , SUM(CASE A.OPER WHEN 'A2100' THEN DECODE(A.LOT_STATUS, 'WAIT', A.QTY_1, 0) ELSE 0 END) AS A2100_WAIT " + "\n");
                strSqlString.Append("             , SUM(CASE A.OPER WHEN 'A2100' THEN DECODE(A.LOT_STATUS, 'PROC', A.QTY_1, 0) ELSE 0 END) AS A2100_PROC " + "\n");
                strSqlString.Append("             , SUM(CASE A.OPER WHEN 'A2300' THEN DECODE(A.LOT_STATUS, 'WAIT', A.QTY_1, 0) ELSE 0 END) AS A2300_WAIT " + "\n");
                strSqlString.Append("             , SUM(CASE A.OPER WHEN 'A2300' THEN DECODE(A.LOT_STATUS, 'PROC', A.QTY_1, 0) ELSE 0 END) AS A2300_PROC " + "\n");
                strSqlString.Append("             , SUM(CASE A.OPER WHEN 'AZ009' THEN DECODE(A.LOT_STATUS, 'WAIT', A.QTY_1, 0) ELSE 0 END) AS AZ009_WAIT " + "\n");
                strSqlString.Append("             , SUM(CASE A.OPER WHEN 'AZ009' THEN DECODE(A.LOT_STATUS, 'PROC', A.QTY_1, 0) ELSE 0 END) AS AZ009_PROC " + "\n");
                strSqlString.Append("             , SUM(CASE A.OPER WHEN 'AZ010' THEN DECODE(A.LOT_STATUS, 'WAIT', A.QTY_1, 0) ELSE 0 END) AS AZ010_WAIT " + "\n");
                strSqlString.Append("             , SUM(CASE A.OPER WHEN 'AZ010' THEN DECODE(A.LOT_STATUS, 'PROC', A.QTY_1, 0) ELSE 0 END) AS AZ010_PROC " + "\n");
                strSqlString.Append("             , SUM(CASE A.OPER WHEN 'T0000' THEN DECODE(A.LOT_STATUS, 'WAIT', A.QTY_1, 0) ELSE 0 END) AS T0000_WAIT " + "\n");
                strSqlString.Append("             , SUM(CASE A.OPER WHEN 'T0000' THEN DECODE(A.LOT_STATUS, 'PROC', A.QTY_1, 0) ELSE 0 END) AS T0000_PROC " + "\n");
                strSqlString.Append("             , SUM(CASE A.OPER WHEN 'T0100' THEN DECODE(A.LOT_STATUS, 'WAIT', A.QTY_1, 0) ELSE 0 END) AS T0100_WAIT " + "\n");
                strSqlString.Append("             , SUM(CASE A.OPER WHEN 'T0100' THEN DECODE(A.LOT_STATUS, 'PROC', A.QTY_1, 0) ELSE 0 END) AS T0100_PROC " + "\n");
                strSqlString.Append("             , SUM(CASE A.OPER WHEN 'T0300' THEN DECODE(A.LOT_STATUS, 'WAIT', A.QTY_1, 0) ELSE 0 END) AS T0300_WAIT " + "\n");
                strSqlString.Append("             , SUM(CASE A.OPER WHEN 'T0300' THEN DECODE(A.LOT_STATUS, 'PROC', A.QTY_1, 0) ELSE 0 END) AS T0300_PROC " + "\n");
                strSqlString.Append("             , SUM(CASE A.OPER WHEN 'T0500' THEN DECODE(A.LOT_STATUS, 'WAIT', A.QTY_1, 0) ELSE 0 END) AS T0500_WAIT " + "\n");
                strSqlString.Append("             , SUM(CASE A.OPER WHEN 'T0500' THEN DECODE(A.LOT_STATUS, 'PROC', A.QTY_1, 0) ELSE 0 END) AS T0500_PROC " + "\n");
                strSqlString.Append("             , SUM(CASE A.OPER WHEN 'T0540' THEN DECODE(A.LOT_STATUS, 'WAIT', A.QTY_1, 0) ELSE 0 END) AS T0540_WAIT " + "\n");
                strSqlString.Append("             , SUM(CASE A.OPER WHEN 'T0540' THEN DECODE(A.LOT_STATUS, 'PROC', A.QTY_1, 0) ELSE 0 END) AS T0540_PROC " + "\n");
                strSqlString.Append("             , SUM(CASE A.OPER WHEN 'T0550' THEN DECODE(A.LOT_STATUS, 'WAIT', A.QTY_1, 0) ELSE 0 END) AS T0550_WAIT " + "\n");
                strSqlString.Append("             , SUM(CASE A.OPER WHEN 'T0550' THEN DECODE(A.LOT_STATUS, 'PROC', A.QTY_1, 0) ELSE 0 END) AS T0550_PROC " + "\n");
                strSqlString.Append("             , SUM(CASE A.OPER WHEN 'T0560' THEN DECODE(A.LOT_STATUS, 'WAIT', A.QTY_1, 0) ELSE 0 END) AS T0560_WAIT " + "\n");
                strSqlString.Append("             , SUM(CASE A.OPER WHEN 'T0560' THEN DECODE(A.LOT_STATUS, 'PROC', A.QTY_1, 0) ELSE 0 END) AS T0560_PROC " + "\n");
                strSqlString.Append("             , SUM(CASE A.OPER WHEN 'T0600' THEN DECODE(A.LOT_STATUS, 'WAIT', A.QTY_1, 0) ELSE 0 END) AS T0600_WAIT " + "\n");
                strSqlString.Append("             , SUM(CASE A.OPER WHEN 'T0600' THEN DECODE(A.LOT_STATUS, 'PROC', A.QTY_1, 0) ELSE 0 END) AS T0600_PROC " + "\n");
                strSqlString.Append("             , SUM(CASE A.OPER WHEN 'T0650' THEN DECODE(A.LOT_STATUS, 'WAIT', A.QTY_1, 0) ELSE 0 END) AS T0650_WAIT " + "\n");
                strSqlString.Append("             , SUM(CASE A.OPER WHEN 'T0650' THEN DECODE(A.LOT_STATUS, 'PROC', A.QTY_1, 0) ELSE 0 END) AS T0650_PROC " + "\n");
                strSqlString.Append("             , SUM(CASE A.OPER WHEN 'T0670' THEN DECODE(A.LOT_STATUS, 'WAIT', A.QTY_1, 0) ELSE 0 END) AS T0670_WAIT " + "\n");
                strSqlString.Append("             , SUM(CASE A.OPER WHEN 'T0670' THEN DECODE(A.LOT_STATUS, 'PROC', A.QTY_1, 0) ELSE 0 END) AS T0670_PROC " + "\n");
                strSqlString.Append("             , SUM(CASE A.OPER WHEN 'T0700' THEN DECODE(A.LOT_STATUS, 'WAIT', A.QTY_1, 0) ELSE 0 END) AS T0700_WAIT " + "\n");
                strSqlString.Append("             , SUM(CASE A.OPER WHEN 'T0700' THEN DECODE(A.LOT_STATUS, 'PROC', A.QTY_1, 0) ELSE 0 END) AS T0700_PROC " + "\n");
                strSqlString.Append("             , SUM(CASE A.OPER WHEN 'T0800' THEN DECODE(A.LOT_STATUS, 'WAIT', A.QTY_1, 0) ELSE 0 END) AS T0800_WAIT " + "\n");
                strSqlString.Append("             , SUM(CASE A.OPER WHEN 'T0800' THEN DECODE(A.LOT_STATUS, 'PROC', A.QTY_1, 0) ELSE 0 END) AS T0800_PROC " + "\n");
                strSqlString.Append("             , SUM(CASE A.OPER WHEN 'T0900' THEN DECODE(A.LOT_STATUS, 'WAIT', A.QTY_1, 0) ELSE 0 END) AS T0900_WAIT " + "\n");
                strSqlString.Append("             , SUM(CASE A.OPER WHEN 'T0900' THEN DECODE(A.LOT_STATUS, 'PROC', A.QTY_1, 0) ELSE 0 END) AS T0900_PROC " + "\n");
                strSqlString.Append("             , SUM(CASE A.OPER WHEN 'T1040' THEN DECODE(A.LOT_STATUS, 'WAIT', A.QTY_1, 0) ELSE 0 END) AS T1040_WAIT " + "\n");
                strSqlString.Append("             , SUM(CASE A.OPER WHEN 'T1040' THEN DECODE(A.LOT_STATUS, 'PROC', A.QTY_1, 0) ELSE 0 END) AS T1040_PROC " + "\n");
                strSqlString.Append("             , SUM(CASE A.OPER WHEN 'T1080' THEN DECODE(A.LOT_STATUS, 'WAIT', A.QTY_1, 0) ELSE 0 END) AS T1080_WAIT " + "\n");
                strSqlString.Append("             , SUM(CASE A.OPER WHEN 'T1080' THEN DECODE(A.LOT_STATUS, 'PROC', A.QTY_1, 0) ELSE 0 END) AS T1080_PROC " + "\n");
                strSqlString.Append("             , SUM(CASE A.OPER WHEN 'T1100' THEN DECODE(A.LOT_STATUS, 'WAIT', A.QTY_1, 0) ELSE 0 END) AS T1100_WAIT " + "\n");
                strSqlString.Append("             , SUM(CASE A.OPER WHEN 'T1100' THEN DECODE(A.LOT_STATUS, 'PROC', A.QTY_1, 0) ELSE 0 END) AS T1100_PROC " + "\n");
                strSqlString.Append("             , SUM(CASE A.OPER WHEN 'T1200' THEN DECODE(A.LOT_STATUS, 'WAIT', A.QTY_1, 0) ELSE 0 END) AS T1200_WAIT " + "\n");
                strSqlString.Append("             , SUM(CASE A.OPER WHEN 'T1200' THEN DECODE(A.LOT_STATUS, 'PROC', A.QTY_1, 0) ELSE 0 END) AS T1200_PROC " + "\n");
                strSqlString.Append("             , SUM(CASE A.OPER WHEN 'T1300' THEN DECODE(A.LOT_STATUS, 'WAIT', A.QTY_1, 0) ELSE 0 END) AS T1300_WAIT " + "\n");
                strSqlString.Append("             , SUM(CASE A.OPER WHEN 'T1300' THEN DECODE(A.LOT_STATUS, 'PROC', A.QTY_1, 0) ELSE 0 END) AS T1300_PROC " + "\n");
                strSqlString.Append("             , SUM(CASE A.OPER WHEN 'TZ010' THEN DECODE(A.LOT_STATUS, 'WAIT', A.QTY_1, 0) ELSE 0 END) AS TZ010_WAIT " + "\n");
                strSqlString.Append("             , SUM(CASE A.OPER WHEN 'TZ010' THEN DECODE(A.LOT_STATUS, 'PROC', A.QTY_1, 0) ELSE 0 END) AS TZ010_PROC " + "\n");
                strSqlString.Append("             , SUM(CASE A.OPER WHEN 'F0000' THEN DECODE(A.LOT_STATUS, 'WAIT', A.QTY_1, 0) ELSE 0 END) AS F0000_WAIT " + "\n");
                strSqlString.Append("             , SUM(CASE A.OPER WHEN 'F0000' THEN DECODE(A.LOT_STATUS, 'PROC', A.QTY_1, 0) ELSE 0 END) AS F0000_PROC " + "\n");
                strSqlString.Append("          FROM MWIPLOTSTS A, MWIPMATDEF B, MWIPOPRDEF C " + "\n");
                strSqlString.Append("         WHERE A.FACTORY = B.FACTORY " + "\n");
                strSqlString.Append("           AND A.MAT_ID = B.MAT_ID " + "\n");
                strSqlString.Append("           AND A.FACTORY = C.FACTORY " + "\n");
                strSqlString.Append("           AND A.OPER = C.OPER " + "\n");
                strSqlString.Append("           AND A.FACTORY IN ('" + GlobalVariable.gsAssyDefaultFactory + "', '" + GlobalVariable.gsTestDefaultFactory + "', 'FGS') " + "\n");
                strSqlString.Append("           AND SUBSTR(A.OPER, 1, 1) IN ('A', 'T', 'F') " + "\n");
                strSqlString.Append("           AND A.OPER NOT IN ('A000N', 'T000N', 'F000N') " + "\n");
                strSqlString.Append("           AND A.LOT_CMF_5 LIKE 'P%' " + "\n");
                strSqlString.Append("           AND A.OWNER_CODE = 'PROD' " + "\n");
                strSqlString.Append("           AND A.LOT_DEL_FLAG <> 'Y' " + "\n");
                strSqlString.Append("         GROUP BY B.MAT_CMF_10, A.OPER, A.LOT_STATUS " + "\n");
                strSqlString.Append("         ORDER BY B.MAT_CMF_10, A.OPER, A.LOT_STATUS " + "\n");
                strSqlString.Append("     ) A " + "\n");
                strSqlString.Append(" GROUP BY A.PIN_TYPE " + "\n");
                strSqlString.Append(" ORDER BY A.PIN_TYPE " + "\n");

                Clipboard.SetText(strSqlString.ToString());

                return strSqlString.ToString();
            }
            catch (Exception ex)
            {
                CmnFunction.ShowMsgBox(ex.Message);
                return "";
            }
            finally
            {
                Cursor.Current = Cursors.Default;
            }
        }

        #endregion


        #region " MakeChart : Chart 처리 "

        /// <summary>
        /// 5. Chart 생성
        /// </summary>
        /// <param name="DT">Chart를 생성할 데이터 테이블</param>
        private void MakeChart(DataTable DT)
        {

        }

        #endregion


        #region " Event 처리 "


        #region " btnView_Click : View버튼을 선택했을 때 "

        private void btnView_Click(object sender, EventArgs e)
        {
            string strSqlString = "";
            DataTable dt = null;
            //int iSubTotalCount = 1;     // 2009-05-27-정비재 : Sub Total의 시작위치(Columns)를 지정한다.
            if (CheckField() == false) return;

            try
            {
                // 검색중 화면 표시
                LoadingPopUp.LoadIngPopUpShow(this);
                this.Refresh();

                // 2009-03-24-정비재 : Sheet의 Title를 동적으로 표시하기 위한 함수
                SortInit();
                GridColumnInit();

                // Query문으로 데이터를 검색한다.
                strSqlString = MakeSqlString();
                if (strSqlString.Trim() == "")
                {
                    return;
                }
                dt = CmnFunction.oComm.GetFuncDataTable("DYNAMIC", strSqlString);

                if (dt.Rows.Count == 0)
                {
                    dt.Dispose();
                    LoadingPopUp.LoadingPopUpHidden();
                    CmnFunction.ShowMsgBox(RptMessages.GetMessage("STD010", GlobalVariable.gcLanguage));
                    return;
                }

                spdData.DataSource = dt;

                ////1.Griid 합계 표시
                //// FAMILY
                //if (udcWIPCondition2.Text != "ALL" && udcWIPCondition2.Text != "")
                //{
                //    iSubTotalCount += 1;
                //}
                //// PACKAGE
                //if (udcWIPCondition3.Text != "ALL" && udcWIPCondition3.Text != "")
                //{
                //    iSubTotalCount += 1;
                //}
                //// TYPE1
                //if (udcWIPCondition4.Text != "ALL" && udcWIPCondition4.Text != "")
                //{
                //    iSubTotalCount += 1;
                //}
                //// TYPE2
                //if (udcWIPCondition5.Text != "ALL" && udcWIPCondition5.Text != "")
                //{
                //    iSubTotalCount += 1;
                //}
                //// LEAD COUNT
                //if (udcWIPCondition6.Text != "ALL" && udcWIPCondition6.Text != "")
                //{
                //    iSubTotalCount += 1;
                //}
                //// DENSITY
                //if (udcWIPCondition7.Text != "ALL" && udcWIPCondition7.Text != "")
                //{
                //    iSubTotalCount += 1;
                //}
                //// GENERATION
                //if (udcWIPCondition8.Text != "ALL" && udcWIPCondition8.Text != "")
                //{
                //    iSubTotalCount += 1;
                //}

                //// Sub Total
                //int[] rowType = spdData.RPT_DataBindingWithSubTotal(dt, 0, iSubTotalCount, 1, null, null);
                // Total
                //spdData.RPT_FillDataSelectiveCells("Total", 0, iSubTotalCount, 0, 1, true, Align.Center, VerticalAlign.Center);
                
                //4. Column Auto Fit
                spdData.RPT_AutoFit(false);

                ////// 2009-03-26-정비재 : Yield는 Sum이 아닌 평균을 계산한다.
                ////SetAvgVertical(1, iSubTotalCount + 16, false);

                ////// Sub Total에 대한 평균을 계산한다.
                ////for (int iRow = 2; iRow <= iSubTotalCount; iRow++)
                ////{
                ////    SetTotalAvgVertical(iRow, iSubTotalCount + 16, false);
                ////}

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

        #endregion


        #region " btnExcelExport_Click : Excel로 내보내기 "

        /// <summary>
        /// Excel Export
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnExcelExport_Click(object sender, EventArgs e)
        {
            //ExcelHelper.Instance.subMakeExcel(spdData, this.lblTitle.Text, null, null, true);
            spdData.ExportExcel();
        }

        #endregion


        #region " txtSearchProduct : Leave Event "

        private void txtSearchProduct_Leave(object sender, EventArgs e)
        {
            /****************************************************/
            /* Comment : 검색할 제품코드를 대분자로 변경한다.   */
            /*                                                  */
            /* Created By : bee-jae jung (2009-02-24)           */
            /*                                                  */
            /* Modified By : bee-jae jung (2009-02-24)          */
            /****************************************************/
            txtSearchProduct.Text = txtSearchProduct.Text.ToUpper();
        }

        #endregion

        private void tmrRefresh_Tick(object sender, EventArgs e)
        {
            /****************************************************************/
            /* Comment : Timer를 이용하여 30초에 한 번식 데이터를 조회한다. */
            /*                                                              */
            /* Created By : bee-jae jung(2009-05-28-목요일)                 */
            /*                                                              */
            /* Modified By : bee-jae jung(2009-05-28-목요일)                */
            /****************************************************************/
            try
            {
                Cursor.Current = Cursors.WaitCursor;

                if (intTime > 0)
                {
                    intTime -= 1;
                }
                else
                {
                    intTime = 60;
                    btnView_Click(null, null);
                }
                lblRefresh.Text = "Refresh 남은 시간 : " + intTime.ToString() + " 초";
            }
            catch (Exception ex)
            {
                CmnFunction.ShowMsgBox(ex.Message);
            }
            finally
            {
                Cursor.Current = Cursors.Default;
            }
        }


        #endregion

        private void PRD010102_Load(object sender, EventArgs e)
        {
            /****************************************************/
            /* Comment : Form Load시 처리할 Logic을 선언한다.   */
            /*                                                  */
            /* Created By : bee-jae jung(2009-05-28-목요일)     */
            /*                                                  */
            /* Modified By : bee-jae jung(2009-05-28-목요일)    */
            /****************************************************/
            try
            {
                Cursor.Current = Cursors.WaitCursor;
                // Refresh 시간을 60초로 한다.
                tmrRefresh.Interval = 1000;
                tmrRefresh.Enabled = true;
            }
            catch (Exception ex)
            {
                CmnFunction.ShowMsgBox(ex.Message);
            }
            finally
            {
                Cursor.Current = Cursors.Default;
            }
        }

        
    }
}
