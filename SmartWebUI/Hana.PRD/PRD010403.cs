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
    public partial class PRD010403 : Miracom.SmartWeb.UI.Controls.udcCUSReport002
    {

        #region " PRD010403 : Program Initial "

        public PRD010403()
        {
            InitializeComponent();
            cdvFromToDate.AutoBinding();
            SortInit();
            GridColumnInit();
            cdvFactory.Text = "HMKA1, HMKT1";
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
            int iIdx;
            try
            {
                spdData.RPT_ColumnInit();

                //if (cdvType.Text == "Standard")
                if (cdvType.SelectedIndex == 0)
                {
                    // 2009-03-23-정비재 : 임태성대리 요청으로 표시되는 순서를 변경함
                    iIdx = 0;
                    spdData.RPT_AddBasicColumn("CUSTOMER", 0, iIdx, Visibles.True, Frozen.False, Align.Center, Merge.True, Formatter.String, 50);
                    iIdx += 1;
                    spdData.RPT_AddBasicColumn("PIN_TYPE", 0, iIdx, Visibles.True, Frozen.False, Align.Center, Merge.True, Formatter.String, 40);
                    // FAMILY
                    if (udcWIPCondition2.Text != "ALL" && udcWIPCondition2.Text != "")
                    {
                        iIdx += 1;
                        spdData.RPT_AddBasicColumn("FAMILY", 0, iIdx, Visibles.True, Frozen.False, Align.Center, Merge.True, Formatter.String, 40);
                    }
                    // PACKAGE
                    if (udcWIPCondition3.Text != "ALL" && udcWIPCondition3.Text != "")
                    {
                        iIdx += 1;
                        spdData.RPT_AddBasicColumn("PACKAGE", 0, iIdx, Visibles.True, Frozen.False, Align.Center, Merge.True, Formatter.String, 40);
                    }
                    // TYPE1
                    if (udcWIPCondition4.Text != "ALL" && udcWIPCondition4.Text != "")
                    {
                        iIdx += 1;
                        spdData.RPT_AddBasicColumn("TYPE1", 0, iIdx, Visibles.True, Frozen.False, Align.Center, Merge.True, Formatter.String, 40);
                    }
                    // TYPE2
                    if (udcWIPCondition5.Text != "ALL" && udcWIPCondition5.Text != "")
                    {
                        iIdx += 1;
                        spdData.RPT_AddBasicColumn("TYPE2", 0, iIdx, Visibles.True, Frozen.False, Align.Center, Merge.True, Formatter.String, 40);
                    }
                    // LEAD COUNT
                    if (udcWIPCondition6.Text != "ALL" && udcWIPCondition6.Text != "")
                    {
                        iIdx += 1;
                        spdData.RPT_AddBasicColumn("LEAD_COUNT", 0, iIdx, Visibles.True, Frozen.False, Align.Center, Merge.True, Formatter.String, 40);
                    }
                    // DENSITY
                    if (udcWIPCondition7.Text != "ALL" && udcWIPCondition7.Text != "")
                    {
                        iIdx += 1;
                        spdData.RPT_AddBasicColumn("DENSITY", 0, iIdx, Visibles.True, Frozen.False, Align.Center, Merge.True, Formatter.String, 40);
                    }
                    // GENERATION
                    if (udcWIPCondition8.Text != "ALL" && udcWIPCondition8.Text != "")
                    {
                        iIdx += 1;
                        spdData.RPT_AddBasicColumn("GENERATION", 0, iIdx, Visibles.True, Frozen.False, Align.Center, Merge.True, Formatter.String, 40);
                    }
                    iIdx += 1;
                    spdData.RPT_AddBasicColumn("PRODUCT", 0, iIdx, Visibles.True, Frozen.False, Align.Center, Merge.True, Formatter.String, 80);
                    spdData.RPT_AddBasicColumn("RETURN_TYPE", 0, iIdx + 1, Visibles.True, Frozen.False, Align.Center, Merge.True, Formatter.String, 50);

                    spdData.RPT_AddBasicColumn("BOH", 0, iIdx + 2, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 50);

                    if (GlobalVariable.gsGlovalSite != "K1")
                    {
                        spdData.RPT_AddBasicColumn("PARTIN", 0, iIdx + 3, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 50);
                        spdData.RPT_AddBasicColumn("PARTOUT", 0, iIdx + 4, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 60);
                        iIdx += 2;
                    }
                    spdData.RPT_AddBasicColumn("A-IN(RECV)", 0, iIdx + 3, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 50);
                    spdData.RPT_AddBasicColumn("A-OUT", 0, iIdx + 4, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 50);
                    spdData.RPT_AddBasicColumn("A-RIN", 0, iIdx + 5, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 50);
                    spdData.RPT_AddBasicColumn("A-ROUT", 0, iIdx + 6, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 50);
                    spdData.RPT_AddBasicColumn("T-IN", 0, iIdx + 7, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 50);
                    spdData.RPT_AddBasicColumn("T-OUT", 0, iIdx + 8, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 50);
                    spdData.RPT_AddBasicColumn("OUT-TTL", 0, iIdx + 9, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 50);
                    spdData.RPT_AddBasicColumn("T-ROUT", 0, iIdx + 10, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 50);
                    spdData.RPT_AddBasicColumn("ROUT-TTL", 0, iIdx + 11, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 50);
                    spdData.RPT_AddBasicColumn("LOSS_LOGIC", 0, iIdx + 12, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 70);
                    spdData.RPT_AddBasicColumn("LOSS_REAL", 0, iIdx + 13, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 70);
                    spdData.RPT_AddBasicColumn("EOH", 0, iIdx + 14, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 50);
                    spdData.RPT_AddBasicColumn("BONUS", 0, iIdx + 15, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 50);
                    spdData.RPT_AddBasicColumn("TRANS", 0, iIdx + 16, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 50);
                    spdData.RPT_AddBasicColumn("YIELD", 0, iIdx + 17, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Double2, 50);
                    spdData.RPT_AddBasicColumn("K3-SHIP", 0, iIdx + 18, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 50);
                }
                //if (cdvType.Text == "Partchange")
                else if (cdvType.SelectedIndex == 1)
                {
                    spdData.RPT_AddBasicColumn("OUT_PART_NO", 0, 0, Visibles.True, Frozen.True, Align.Left, Merge.False, Formatter.String, 150);
                    spdData.RPT_AddBasicColumn("OUT_QTY", 0, 1, Visibles.True, Frozen.True, Align.Right, Merge.False, Formatter.Number, 80);
                    spdData.RPT_AddBasicColumn("IN_PART_NO", 0, 2, Visibles.True, Frozen.True, Align.Left, Merge.False, Formatter.String, 150);
                    spdData.RPT_AddBasicColumn("IN_QTY", 0, 3, Visibles.True, Frozen.True, Align.Right, Merge.False, Formatter.Number, 80);        
                }
                // 2009-03-24-정비재 : 사용않함, 필요없는 것 같음
                // Group항목이 있을경우 반드시 선언해줄것.
                //spdData.RPT_ColumnConfigFromTable(btnSort);
            }
            catch (Exception ex)
            {
                CmnFunction.ShowMsgBox(ex.Message);
                LoadingPopUp.LoadingPopUpHidden();
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
            /************************************************/
            /* COMMENT BY  : MES에서 사용할 결산용 QUERY문	*/
            /* 												*/
            /* CREATED BY  : BEE-JAE JUNG (2009-01-22)		*/
            /* 												*/
            /* MODIFIED BY : BEE-JAE JUNG (2009-01-22)		*/
            /************************************************/
            StringBuilder strSqlString = new StringBuilder();

            string QueryCond1 = null, QueryCond2 = null, QueryCond3 = null;
            string strFromDate = null, strToDate = null, strFromCutOffDate = null, strToCutOffDate = null;

            udcTableForm tableForm = (udcTableForm)btnSort.BindingForm;
            DateTime dtDate;

            QueryCond1 = tableForm.SelectedValueToQueryContainNull;
            QueryCond2 = tableForm.SelectedValue2ToQueryContainNull;
            QueryCond3 = tableForm.SelectedValue3ToQueryContainNull;
 
            // 결산기간
            switch (cdvFromToDate.DaySelector.SelectedValue.ToString())
            {
                case "DAY":
                    //if (udcWIPCondition1.Text == "HX" || (txtSearchProduct.Text.Trim().Length >= 2 && txtSearchProduct.Text.Trim().Substring(0, 2).ToUpper() == "HX"))
                    //{
                    //    // 시작일자
                    //    strFromDate = cdvFromToDate.FromDate.Text.Replace("-", "") + "060001";
                    //    // 정상
                    //    if (rbtEoh01.Checked == true)
                    //    {
                    //        strToDate = cdvFromToDate.ToDate.Text.Replace("-", "") + "060000";
                    //    }
                    //    // 금일
                    //    else
                    //    {
                    //        // 종료일자
                    //        QRY = "(SELECT TO_CHAR(SYSDATE, 'YYYYMMDDHH24MISS') AS NOW_DATE FROM DUAL)";
                    //        dt1 = CmnFunction.oComm.GetFuncDataTable("DYNAMIC", QRY);
                    //        strToDate = dt1.Rows[0][0].ToString();
                    //    }
                    //}
                    //else
                    //{
                    //    // 시작일자
                    //    dtDate = DateTime.Parse(cdvFromToDate.FromDate.Text);
                    //    dtDate = dtDate.AddDays(-1);
                    //    strFromDate = dtDate.ToString("yyyyMMdd") + "220001";
                    //    // 정상
                    //    if (rbtEoh01.Checked == true)
                    //    {
                    //        // 종료일자
                    //        dtDate = DateTime.Parse(cdvFromToDate.ToDate.Text);
                    //        dtDate = dtDate.AddDays(-1);
                    //        strToDate = dtDate.ToString("yyyyMMdd") + "220000";
                    //    }
                    //    // 금일
                    //    else
                    //    {
                    //        // 종료일자
                    //        QRY = "(SELECT TO_CHAR(SYSDATE, 'YYYYMMDDHH24MISS') AS NOW_DATE FROM DUAL)";
                    //        dt1 = CmnFunction.oComm.GetFuncDataTable("DYNAMIC", QRY);
                    //        strToDate = dt1.Rows[0][0].ToString();
                    //    }
                    //}

                    // 2009-03-24-정비재 : 기준정보 시간을 선택한다.
                    if (rbTime01.Checked == true)
                    {
                        dtDate = DateTime.Parse(cdvFromToDate.FromDate.Text);
                        dtDate = dtDate.AddDays(-1);
                        strFromDate = dtDate.ToString("yyyyMMdd") + "220001";
                        strToDate = cdvFromToDate.ToDate.Text.Replace("-", "") + "220000";
                    }
                    else if (rbTime02.Checked == true)
                    {
                        strFromDate = cdvFromToDate.FromDate.Text.Replace("-", "") + "060001";
                        strToDate = cdvFromToDate.ToDate.Text.Replace("-", "") + "060000";
                    }
                    strFromCutOffDate = strFromDate.Substring(0, 10);
                    strToCutOffDate = strToDate.Substring(0, 10);
                    break;

                default:
                    MessageBox.Show("결산기간은 일(Day) 단위로만 할 수 있습니다!" + "\r\n\r\n"
                                  + "일(Day) 단위를 선택하여 주십시오!", this.Title, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    return "";
            }
            //Type = Basics
            if (cdvType.SelectedIndex == 0)
            {
                #region 1.HMK
                if (GlobalVariable.gsGlovalSite == "K1")
                {
                    strSqlString.AppendFormat("SELECT {0} " + "\n", QueryCond3);
                    /* 결산 : 월 결산을 하기 위한 QUERY문	*/
                    strSqlString.Append("     , SUM(A.BOH) AS BOH " + "\n");
                    strSqlString.Append("     , SUM(A.INASSY) AS INASSY  " + "\n");
                    strSqlString.Append("     , SUM(A.OUTASSY) AS OUTASSY " + "\n");
                    strSqlString.Append("     , SUM(A.RINASSY) AS RINASSY " + "\n");
                    strSqlString.Append("     , SUM(A.ROUTASSY) AS ROUTASSY " + "\n");
                    strSqlString.Append("     , SUM(A.INTEST) AS INTEST " + "\n");
                    strSqlString.Append("     , SUM(A.OUTTEST) AS OUTTEST " + "\n");
                    strSqlString.Append("     , SUM(A.OUTASSY + A.OUTTEST) AS OUT_TTL " + "\n");
                    strSqlString.Append("     , SUM(A.ROUTTEST) AS T_ROUT " + "\n");
                    strSqlString.Append("     , SUM(A.ROUTASSY + A.ROUTTEST) AS ROUT_TTL " + "\n");

                    // 조립Site(HM = 자사조립) 
                    if (rbtSite01.Checked == true)
                    {
                        strSqlString.Append("     , SUM((A.BOH + A.INASSY) - (A.EOH + A.OUTASSY + A.OUTTEST - A.ROUTASSY - A.ROUTTEST + A.RINASSY)) AS LOSS " + "\n");
                    }
                    else
                    {
                        strSqlString.Append("     , SUM((A.BOH + A.INTEST) - (A.EOH + A.OUTTEST - A.ROUTTEST + A.RINASSY)) AS LOSS " + "\n");
                    }

                    strSqlString.Append("     , SUM(NVL(D.SUM_QTY,0)) AS LOSS_REAL" + "\n");
                    strSqlString.Append("     , SUM(A.EOH) AS EOH " + "\n");
                    strSqlString.Append("     , SUM(A.BONUS) AS BONUS " + "\n");
                    strSqlString.Append("     , SUM(A.TRANS) AS TRANS " + "\n");

                    // 조립Site(HM = 자사조립) 
                    if (rbtSite01.Checked == true)
                    {
                        strSqlString.Append("     , CASE SUM(A.BOH + A.INASSY - A.EOH - A.RINASSY) WHEN 0 THEN 0 " + "\n");
                        strSqlString.Append("                                                      ELSE ROUND(SUM(A.OUTASSY + A.OUTTEST - A.ROUTASSY - A.ROUTTEST) / SUM(A.BOH + A.INASSY - A.EOH - A.RINASSY) * 100, 3) " + "\n");
                        strSqlString.Append("       END AS YIELD" + "\n");
                    }
                    else
                    {
                        strSqlString.Append("     , CASE SUM(A.BOH + A.INTEST - A.EOH - A.RINASSY) WHEN 0 THEN 0 " + "\n");
                        strSqlString.Append("                                                      ELSE ROUND(SUM(A.OUTTEST - A.ROUTTEST) / SUM(A.BOH + A.INTEST - A.EOH - A.RINASSY) * 100, 3) " + "\n");
                        strSqlString.Append("       END AS YIELD" + "\n");
                    }

                    strSqlString.Append("     , SUM(A.OUTASSY - A.ROUTASSY) AS K3_SHIP " + "\n");
                    strSqlString.Append("  FROM ( " + "\n");
                    /************************************************************************************************************/
                    /* BOH : 각 공정별 BOH수량을 조회한다.                                                                      */
                    strSqlString.Append("        SELECT A.MAT_ID AS MAT_ID " + "\n");
                    strSqlString.Append("             , A.RETURN_TYPE AS RETURN_TYPE " + "\n");
                    strSqlString.Append("             , SUM(A.BOH) AS BOH " + "\n");
                    strSqlString.Append("             , 0 AS INASSY " + "\n");
                    strSqlString.Append("             , 0 AS INTEST " + "\n");
                    strSqlString.Append("             , 0 AS ASYTST " + "\n");
                    strSqlString.Append("             , 0 AS OUTASSY " + "\n");
                    strSqlString.Append("             , 0 AS OUTTEST " + "\n");
                    strSqlString.Append("             , 0 AS RINASSY " + "\n");
                    strSqlString.Append("             , 0 AS ROUTASSY " + "\n");
                    strSqlString.Append("             , 0 AS ROUTTEST " + "\n");
                    strSqlString.Append("             , 0 AS BONUS " + "\n");
                    strSqlString.Append("             , 0 AS TRANS " + "\n");
                    strSqlString.Append("             , 0 AS EOH " + "\n");
                    strSqlString.Append("             , 0 AS K4EOH " + "\n");
                    strSqlString.Append("          FROM ( " + "\n");

                    // 조립Site(HM = 자사조립) 
                    if (rbtSite01.Checked == true)
                    {
                        /* BOH : HMK2의 ASSY SITE가 않맞는 문제로 HMK2 BOH를 별도로 계산한다.(HMK2는 ASSY SITE를 구분하지 않는다.)  */
                        strSqlString.Append("                SELECT A.MAT_ID AS MAT_ID " + "\n");
                        strSqlString.Append("                     , CASE A.LOT_CMF_15 WHEN ' ' THEN '**' " + "\n");
                        strSqlString.Append("                                         ELSE A.LOT_CMF_15 " + "\n");
                        strSqlString.Append("                       END AS RETURN_TYPE " + "\n");
                        strSqlString.Append("                     , SUM(A.QTY_1) AS BOH " + "\n");
                        strSqlString.Append("                  FROM RWIPLOTSTS_BOH A " + "\n");
                        strSqlString.Append("                 WHERE A.FACTORY = '" + GlobalVariable.gsAssyDefaultFactory + "' " + "\n");
                        strSqlString.Append("                   AND A.OPER = 'A0000' " + "\n");
                        strSqlString.Append("                   AND A.LOT_TYPE = 'W' " + "\n");
                        strSqlString.Append("                   AND A.OWNER_CODE = 'PROD' " + "\n");
                        strSqlString.AppendFormat("                   AND A.CUTOFF_DT = '{0}' " + "\n", strFromCutOffDate);
                        strSqlString.AppendFormat("                   AND A.LOT_CMF_5 LIKE '{0}%' " + "\n", txtLotType.Text.Trim());
                        strSqlString.AppendFormat("                   AND A.MAT_ID LIKE '{0}%' " + "\n", txtSearchProduct.Text.Trim());
                        strSqlString.Append("                 GROUP BY A.MAT_ID, A.LOT_CMF_15 " + "\n");
                        strSqlString.Append("                UNION " + "\n");
                        /************************************************************************************************************/
                        /* BOH : HMK2를 제외한 공정의 BOH를 계산한다.                                                               */
                        strSqlString.Append("                SELECT A.MAT_ID AS MAT_ID " + "\n");
                        strSqlString.Append("                     , CASE A.LOT_CMF_15 WHEN ' ' THEN '**' " + "\n");
                        strSqlString.Append("                                         ELSE A.LOT_CMF_15 " + "\n");
                        strSqlString.Append("                       END AS RETURN_TYPE " + "\n");
                        strSqlString.Append("                     , SUM(A.QTY_1) AS BOH " + "\n");
                        strSqlString.Append("                  FROM RWIPLOTSTS_BOH A " + "\n");
                        strSqlString.Append("                 WHERE A.FACTORY IN ('" + GlobalVariable.gsAssyDefaultFactory + "', '" + GlobalVariable.gsTestDefaultFactory + "') " + "\n");
                        strSqlString.Append("                   AND A.OPER > 'A0000' " + "\n");
                        strSqlString.Append("                   AND A.LOT_TYPE = 'W' " + "\n");
                        strSqlString.Append("                   AND A.OWNER_CODE = 'PROD' " + "\n");
                        strSqlString.AppendFormat("                   AND A.CUTOFF_DT = '{0}' " + "\n", strFromCutOffDate);
                        strSqlString.AppendFormat("                   AND A.LOT_CMF_5 LIKE '{0}%' " + "\n", txtLotType.Text.Trim());
                        strSqlString.AppendFormat("                   AND A.MAT_ID LIKE '{0}%' " + "\n", txtSearchProduct.Text.Trim());
                        strSqlString.AppendFormat("                   AND TRIM(A.LOT_CMF_7) = 'HM' " + "\n");
                        strSqlString.Append("                 GROUP BY A.MAT_ID, A.LOT_CMF_15 " + "\n");
                        strSqlString.Append("                ) A " + "\n");
                        strSqlString.Append("         GROUP BY A.MAT_ID, A.RETURN_TYPE " + "\n");
                    }
                    else
                    {
                        /************************************************************************************************************/
                        /* BOH : HMK2를 제외한 공정의 BOH를 계산한다.                                                               */
                        strSqlString.Append("                SELECT A.MAT_ID AS MAT_ID " + "\n");
                        strSqlString.Append("                     , CASE A.LOT_CMF_15 WHEN ' ' THEN '**' " + "\n");
                        strSqlString.Append("                                         ELSE A.LOT_CMF_15 " + "\n");
                        strSqlString.Append("                       END AS RETURN_TYPE " + "\n");
                        strSqlString.Append("                     , SUM(A.QTY_1) AS BOH " + "\n");
                        strSqlString.Append("                  FROM RWIPLOTSTS_BOH A " + "\n");
                        strSqlString.Append("                 WHERE A.FACTORY = '" + GlobalVariable.gsTestDefaultFactory + "' " + "\n");
                        strSqlString.Append("                   AND A.OPER >= 'T0000' " + "\n");
                        strSqlString.Append("                   AND A.LOT_TYPE = 'W' " + "\n");
                        strSqlString.Append("                   AND A.OWNER_CODE = 'PROD' " + "\n");
                        strSqlString.AppendFormat("                   AND A.CUTOFF_DT = '{0}' " + "\n", strFromCutOffDate);
                        strSqlString.AppendFormat("                   AND A.LOT_CMF_5 LIKE '{0}%' " + "\n", txtLotType.Text.Trim());
                        strSqlString.AppendFormat("                   AND A.MAT_ID LIKE '{0}%' " + "\n", txtSearchProduct.Text.Trim());
                        strSqlString.AppendFormat("                   AND TRIM(A.LOT_CMF_7) <> 'HM' " + "\n");
                        strSqlString.Append("                 GROUP BY A.MAT_ID, A.LOT_CMF_15 " + "\n");
                        strSqlString.Append("                ) A " + "\n");
                        strSqlString.Append("         GROUP BY A.MAT_ID, A.RETURN_TYPE " + "\n");
                    }
                    strSqlString.Append("         UNION " + "\n");
                    // 조립Site(HM = 자사조립) 
                    if (rbtSite01.Checked == true)
                    {
                        /************************************************************************************************************/
                        /* INASSY(RECV_XX_HMK2) : HMK2 RECIVE(MES의 A0000공정의 IN 수량) 수량을 조회한다.                           */
                        strSqlString.Append("        SELECT A.MAT_ID AS MAT_ID " + "\n");
                        strSqlString.Append("             , CASE A.LOT_CMF_15 WHEN ' ' THEN '**' " + "\n");
                        strSqlString.Append("                                 ELSE A.LOT_CMF_15 " + "\n");
                        strSqlString.Append("               END AS RETURN_TYPE " + "\n");
                        strSqlString.Append("             , 0 AS BOH " + "\n");
                        strSqlString.Append("             , SUM(A.QTY_1) AS INASSY " + "\n");
                        strSqlString.Append("             , 0 AS INTEST " + "\n");
                        strSqlString.Append("             , 0 AS ASYTST " + "\n");
                        strSqlString.Append("             , 0 AS OUTASSY " + "\n");
                        strSqlString.Append("             , 0 AS OUTTEST " + "\n");
                        strSqlString.Append("             , 0 AS RINASSY " + "\n");
                        strSqlString.Append("             , 0 AS ROUTASSY " + "\n");
                        strSqlString.Append("             , 0 AS ROUTTEST " + "\n");
                        strSqlString.Append("             , 0 AS BONUS " + "\n");
                        strSqlString.Append("             , 0 AS TRANS " + "\n");
                        strSqlString.Append("             , 0 AS EOH " + "\n");
                        strSqlString.Append("             , 0 AS K4EOH " + "\n");
                        strSqlString.Append("          FROM RWIPLOTHIS A " + "\n");
                        strSqlString.Append("         WHERE A.FACTORY = '" + GlobalVariable.gsAssyDefaultFactory + "' " + "\n");
                        strSqlString.Append("           AND A.OPER = 'A0000' " + "\n");
                        strSqlString.Append("           AND A.TRAN_CODE = 'CREATE' " + "\n");
                        strSqlString.Append("           AND A.LOT_TYPE = 'W' " + "\n");
                        strSqlString.Append("           AND A.OWNER_CODE = 'PROD' " + "\n");
                        strSqlString.AppendFormat("           AND A.LOT_CMF_5 LIKE '{0}%' " + "\n", txtLotType.Text.Trim());
                        strSqlString.AppendFormat("           AND A.TRAN_TIME BETWEEN '{0}' AND '{1}' " + "\n", strFromDate, strToDate);
                        strSqlString.AppendFormat("           AND A.MAT_ID LIKE '{0}%' " + "\n", txtSearchProduct.Text.Trim());
                        strSqlString.Append("           AND A.HIST_DEL_FLAG <> 'Y' " + "\n");
                        strSqlString.Append("         GROUP BY A.MAT_ID, A.LOT_CMF_15 " + "\n");
                    }
                    else
                    {
                        strSqlString.Append("        SELECT ' ' AS MAT_ID " + "\n");
                        strSqlString.Append("             , ' ' AS RETURN_TYPE " + "\n");
                        strSqlString.Append("             , 0 AS BOH " + "\n");
                        strSqlString.Append("             , 0 AS INASSY " + "\n");
                        strSqlString.Append("             , 0 AS INTEST " + "\n");
                        strSqlString.Append("             , 0 AS ASYTST " + "\n");
                        strSqlString.Append("             , 0 AS OUTASSY " + "\n");
                        strSqlString.Append("             , 0 AS OUTTEST " + "\n");
                        strSqlString.Append("             , 0 AS RINASSY " + "\n");
                        strSqlString.Append("             , 0 AS ROUTASSY " + "\n");
                        strSqlString.Append("             , 0 AS ROUTTEST " + "\n");
                        strSqlString.Append("             , 0 AS BONUS " + "\n");
                        strSqlString.Append("             , 0 AS TRANS " + "\n");
                        strSqlString.Append("             , 0 AS EOH " + "\n");
                        strSqlString.Append("             , 0 AS K4EOH " + "\n");
                        strSqlString.Append("          FROM DUAL A " + "\n");
                    }
                    strSqlString.Append("        UNION " + "\n");
                    /************************************************************************************************************/
                    /* INTEST(ISSUE_HMK3_HMKT) : HMK3 ISSUE(MES의 T0000공정의 END 수량) 수량을 조회한다.                        */
                    strSqlString.Append("        SELECT A.MAT_ID AS MAT_ID " + "\n");
                    strSqlString.Append("             , CASE A.LOT_CMF_15 WHEN ' ' THEN '**' " + "\n");
                    strSqlString.Append("                                 ELSE A.LOT_CMF_15 " + "\n");
                    strSqlString.Append("               END AS RETURN_TYPE " + "\n");
                    strSqlString.Append("             , 0 AS BOH " + "\n");
                    strSqlString.Append("             , 0 AS INASSY " + "\n");
                    strSqlString.Append("             , SUM(A.QTY_1) AS INTEST " + "\n");
                    strSqlString.Append("             , 0 AS ASYTST " + "\n");
                    strSqlString.Append("             , 0 AS OUTASSY " + "\n");
                    strSqlString.Append("             , 0 AS OUTTEST " + "\n");
                    strSqlString.Append("             , 0 AS RINASSY " + "\n");
                    strSqlString.Append("             , 0 AS ROUTASSY " + "\n");
                    strSqlString.Append("             , 0 AS ROUTTEST " + "\n");
                    strSqlString.Append("             , 0 AS BONUS " + "\n");
                    strSqlString.Append("             , 0 AS TRANS " + "\n");
                    strSqlString.Append("             , 0 AS EOH " + "\n");
                    strSqlString.Append("             , 0 AS K4EOH " + "\n");
                    strSqlString.Append("          FROM RWIPLOTHIS A " + "\n");
                    strSqlString.Append("         WHERE A.FACTORY = '" + GlobalVariable.gsTestDefaultFactory + "' " + "\n");
                    strSqlString.Append("           AND A.OLD_OPER = 'T0000' " + "\n");
                    strSqlString.Append("           AND A.TRAN_CMF_1 = 'ISSUE' " + "\n");
                    strSqlString.Append("           AND A.LOT_TYPE = 'W' " + "\n");
                    strSqlString.Append("           AND A.OWNER_CODE = 'PROD' " + "\n");
                    strSqlString.AppendFormat("           AND A.LOT_CMF_5 LIKE '{0}%' " + "\n", txtLotType.Text.Trim());
                    strSqlString.AppendFormat("           AND A.TRAN_TIME BETWEEN '{0}' AND '{1}'" + "\n", strFromDate, strToDate);
                    strSqlString.AppendFormat("           AND A.MAT_ID LIKE '{0}%' " + "\n", txtSearchProduct.Text.Trim());
                    // 조립Site(HM = 자사조립) 
                    if (rbtSite01.Checked == true)
                    {
                        strSqlString.AppendFormat("           AND TRIM(A.LOT_CMF_7) = 'HM' " + "\n");
                    }
                    else
                    {
                        strSqlString.AppendFormat("           AND TRIM(A.LOT_CMF_7) <> 'HM' " + "\n");
                    }
                    strSqlString.Append("           AND A.HIST_DEL_FLAG <> 'Y' " + "\n");
                    strSqlString.Append("         GROUP BY A.MAT_ID, A.LOT_CMF_15 " + "\n");
                    strSqlString.Append("        UNION " + "\n");
                    // 조립Site(HM = 자사조립) 
                    if (rbtSite01.Checked == true)
                    {
                        /************************************************************************************************************/
                        /* OUTASSY(SHIP_HMK3_XX) : HMK3 STOCKS에서 출하된 수량을 조회한다.(SHIP_HMKA ACTIVITY는 존재하지 않는다.)   */
                        strSqlString.Append("        SELECT A.MAT_ID AS MAT_ID " + "\n");
                        strSqlString.Append("             , CASE A.LOT_CMF_15 WHEN ' ' THEN '**' " + "\n");
                        strSqlString.Append("                                 ELSE A.LOT_CMF_15 " + "\n");
                        strSqlString.Append("               END AS RETURN_TYPE " + "\n");
                        strSqlString.Append("             , 0 AS BOH " + "\n");
                        strSqlString.Append("             , 0 AS INASSY " + "\n");
                        strSqlString.Append("             , 0 AS INTEST " + "\n");
                        strSqlString.Append("             , 0 AS ASYTST " + "\n");
                        strSqlString.Append("             , SUM(A.QTY_1) AS OUTASSY " + "\n");
                        strSqlString.Append("             , 0 AS OUTTEST " + "\n");
                        strSqlString.Append("             , 0 AS RINASSY " + "\n");
                        strSqlString.Append("             , 0 AS ROUTASSY " + "\n");
                        strSqlString.Append("             , 0 AS ROUTTEST " + "\n");
                        strSqlString.Append("             , 0 AS BONUS " + "\n");
                        strSqlString.Append("             , 0 AS TRANS " + "\n");
                        strSqlString.Append("             , 0 AS EOH " + "\n");
                        strSqlString.Append("             , 0 AS K4EOH " + "\n");
                        strSqlString.Append("          FROM RWIPLOTHIS A " + "\n");
                        strSqlString.Append("         WHERE A.FACTORY IN ('" + GlobalVariable.gsTestDefaultFactory + "', 'CUSTOMER') " + "\n");
                        strSqlString.Append("           AND A.OLD_FACTORY = '" + GlobalVariable.gsAssyDefaultFactory + "' " + "\n");
                        strSqlString.Append("           AND A.OPER = ' ' " + "\n");
                        strSqlString.Append("           AND A.OLD_OPER = 'AZ010' " + "\n");
                        strSqlString.Append("           AND A.TRAN_CODE = 'SHIP' " + "\n");
                        strSqlString.Append("           AND A.LOT_TYPE = 'W' " + "\n");
                        strSqlString.Append("           AND A.OWNER_CODE = 'PROD' " + "\n");
                        strSqlString.AppendFormat("           AND A.LOT_CMF_5 LIKE '{0}%' " + "\n", txtLotType.Text.Trim());
                        strSqlString.AppendFormat("           AND A.TRAN_TIME BETWEEN '{0}' AND '{1}'" + "\n", strFromDate, strToDate);
                        strSqlString.AppendFormat("           AND A.MAT_ID LIKE '{0}%' " + "\n", txtSearchProduct.Text.Trim());
                        strSqlString.Append("           AND A.HIST_DEL_FLAG <> 'Y' " + "\n");
                        strSqlString.Append("         GROUP BY A.MAT_ID, A.LOT_CMF_15 " + "\n");
                    }
                    else
                    {
                        strSqlString.Append("        SELECT ' ' AS MAT_ID " + "\n");
                        strSqlString.Append("             , ' ' AS RETURN_TYPE " + "\n");
                        strSqlString.Append("             , 0 AS BOH " + "\n");
                        strSqlString.Append("             , 0 AS INASSY " + "\n");
                        strSqlString.Append("             , 0 AS INTEST " + "\n");
                        strSqlString.Append("             , 0 AS ASYTST " + "\n");
                        strSqlString.Append("             , 0 AS OUTASSY " + "\n");
                        strSqlString.Append("             , 0 AS OUTTEST " + "\n");
                        strSqlString.Append("             , 0 AS RINASSY " + "\n");
                        strSqlString.Append("             , 0 AS ROUTASSY " + "\n");
                        strSqlString.Append("             , 0 AS ROUTTEST " + "\n");
                        strSqlString.Append("             , 0 AS BONUS " + "\n");
                        strSqlString.Append("             , 0 AS TRANS " + "\n");
                        strSqlString.Append("             , 0 AS EOH " + "\n");
                        strSqlString.Append("             , 0 AS K4EOH " + "\n");
                        strSqlString.Append("          FROM DUAL A " + "\n");
                    }
                    strSqlString.Append("        UNION " + "\n");
                    /************************************************************************************************************/
                    /* OUTTEST(SHIP_HMK4_XX) : HMK4 STOCKS에서 출하된 수량을 조회한다.                                          */
                    strSqlString.Append("        SELECT A.MAT_ID AS MAT_ID " + "\n");
                    strSqlString.Append("             , A.RETURN_TYPE AS RETURN_TYPE " + "\n");
                    strSqlString.Append("             , 0 AS BOH " + "\n");
                    strSqlString.Append("             , 0 AS INASSY " + "\n");
                    strSqlString.Append("             , 0 AS INTEST " + "\n");
                    strSqlString.Append("             , 0 AS ASYTST " + "\n");
                    strSqlString.Append("             , 0 AS OUTASSY " + "\n");
                    strSqlString.Append("             , SUM(A.OUTTEST) AS OUTTEST " + "\n");
                    strSqlString.Append("             , 0 AS RINASSY " + "\n");
                    strSqlString.Append("             , 0 AS ROUTASSY " + "\n");
                    strSqlString.Append("             , 0 AS ROUTTEST " + "\n");
                    strSqlString.Append("             , 0 AS BONUS " + "\n");
                    strSqlString.Append("             , 0 AS TRANS " + "\n");
                    strSqlString.Append("             , 0 AS EOH " + "\n");
                    strSqlString.Append("             , 0 AS K4EOH " + "\n");
                    strSqlString.Append("          FROM ( " + "\n");
                    /* CUSTOMER : LOT이 최종으로 출하된 수량을 조회한다.                                                        */
                    strSqlString.Append("                SELECT A.MAT_ID AS MAT_ID " + "\n");
                    strSqlString.Append("                     , CASE A.LOT_CMF_15 WHEN ' ' THEN '**' " + "\n");
                    strSqlString.Append("                                         ELSE A.LOT_CMF_15 " + "\n");
                    strSqlString.Append("                       END AS RETURN_TYPE " + "\n");
                    strSqlString.Append("                     , SUM(A.QTY_1) AS OUTTEST " + "\n");
                    strSqlString.Append("                  FROM RWIPLOTHIS A " + "\n");
                    strSqlString.Append("                 WHERE A.FACTORY = 'CUSTOMER' " + "\n");
                    strSqlString.Append("                   AND A.OLD_FACTORY = '" + GlobalVariable.gsTestDefaultFactory + "' " + "\n");
                    strSqlString.Append("                   AND A.OPER = ' ' " + "\n");
                    strSqlString.Append("                   AND A.OLD_OPER = 'TZ010' " + "\n");
                    strSqlString.Append("                   AND A.TRAN_CODE = 'SHIP' " + "\n");
                    strSqlString.Append("                   AND A.LOT_TYPE = 'W' " + "\n");
                    strSqlString.Append("                   AND A.OWNER_CODE = 'PROD' " + "\n");
                    strSqlString.AppendFormat("                   AND A.LOT_CMF_5 LIKE '{0}%' " + "\n", txtLotType.Text.Trim());
                    strSqlString.AppendFormat("                   AND A.TRAN_TIME BETWEEN '{0}' AND '{1}'" + "\n", strFromDate, strToDate);
                    strSqlString.AppendFormat("                   AND A.MAT_ID LIKE '{0}%' " + "\n", txtSearchProduct.Text.Trim());
                    // 조립Site(HM = 자사조립)
                    if (rbtSite01.Checked == true)
                    {
                        strSqlString.AppendFormat("                   AND TRIM(A.LOT_CMF_7) = 'HM' " + "\n");
                    }
                    else
                    {
                        strSqlString.AppendFormat("                   AND TRIM(A.LOT_CMF_7) <> 'HM' " + "\n");
                    }
                    strSqlString.Append("                   AND A.HIST_DEL_FLAG <> 'Y' " + "\n");
                    strSqlString.Append("                 GROUP BY A.MAT_ID, A.LOT_CMF_15 " + "\n");
                    strSqlString.Append("                UNION " + "\n");
                    /* HMKT1 : 파샬출하된 LOT의 수량을 조회한다.                                                                */
                    strSqlString.Append("                SELECT A.MAT_ID AS MAT_ID " + "\n");
                    strSqlString.Append("                     , CASE A.LOT_CMF_15 WHEN ' ' THEN '**' " + "\n");
                    strSqlString.Append("                                         ELSE A.LOT_CMF_15 " + "\n");
                    strSqlString.Append("                       END AS RETURN_TYPE " + "\n");
                    strSqlString.Append("                     , SUM(A.OLD_QTY_1 - A.QTY_1) AS OUTTEST " + "\n");
                    strSqlString.Append("                  FROM RWIPLOTHIS A " + "\n");
                    strSqlString.Append("                 WHERE A.FACTORY = '" + GlobalVariable.gsTestDefaultFactory + "' " + "\n");
                    strSqlString.Append("                   AND A.OLD_FACTORY = '" + GlobalVariable.gsTestDefaultFactory + "' " + "\n");
                    strSqlString.Append("                   AND A.OPER = 'TZ010' " + "\n");
                    strSqlString.Append("                   AND A.OLD_OPER = 'TZ010' " + "\n");
                    strSqlString.Append("                   AND A.TRAN_CODE = 'SHIP' " + "\n");
                    strSqlString.Append("                   AND A.LOT_TYPE = 'W' " + "\n");
                    strSqlString.Append("                   AND A.OWNER_CODE = 'PROD' " + "\n");
                    strSqlString.AppendFormat("                   AND A.LOT_CMF_5 LIKE '{0}%' " + "\n", txtLotType.Text.Trim());
                    strSqlString.AppendFormat("                   AND A.TRAN_TIME BETWEEN '{0}' AND '{1}'" + "\n", strFromDate, strToDate);
                    strSqlString.AppendFormat("                   AND A.MAT_ID LIKE '{0}%' " + "\n", txtSearchProduct.Text.Trim());
                    // 조립Site(HM = 자사조립)
                    if (rbtSite01.Checked == true)
                    {
                        strSqlString.AppendFormat("                   AND TRIM(A.LOT_CMF_7) = 'HM' " + "\n");
                    }
                    else
                    {
                        strSqlString.AppendFormat("                   AND TRIM(A.LOT_CMF_7) <> 'HM' " + "\n");
                    }
                    strSqlString.Append("                   AND A.HIST_DEL_FLAG <> 'Y' " + "\n");
                    strSqlString.Append("                 GROUP BY A.MAT_ID, A.LOT_CMF_15 " + "\n");
                    strSqlString.Append("             ) A " + "\n");
                    strSqlString.Append("         GROUP BY A.MAT_ID, A.RETURN_TYPE " + "\n");
                    strSqlString.Append("        UNION " + "\n");
                    /************************************************************************************************************/
                    /* RINASSY(RETURN_HMK2_XX) : HMK2 STOCKS에서 반품된 수량을 조회한다.                                        */
                    strSqlString.Append("        SELECT A.MAT_ID AS MAT_ID " + "\n");
                    strSqlString.Append("             , CASE A.LOT_CMF_15 WHEN ' ' THEN '**' " + "\n");
                    strSqlString.Append("                                 ELSE A.LOT_CMF_15 " + "\n");
                    strSqlString.Append("               END AS RETURN_TYPE " + "\n");
                    strSqlString.Append("             , 0 AS BOH " + "\n");
                    strSqlString.Append("             , 0 AS INASSY " + "\n");
                    strSqlString.Append("             , 0 AS INTEST " + "\n");
                    strSqlString.Append("             , 0 AS ASYTST " + "\n");
                    strSqlString.Append("             , 0 AS OUTASSY " + "\n");
                    strSqlString.Append("             , 0 AS OUTTEST " + "\n");
                    strSqlString.Append("             , SUM(A.QTY_1) AS RINASSY " + "\n");
                    strSqlString.Append("             , 0 AS ROUTASSY " + "\n");
                    strSqlString.Append("             , 0 AS ROUTTEST " + "\n");
                    strSqlString.Append("             , 0 AS BONUS " + "\n");
                    strSqlString.Append("             , 0 AS TRANS " + "\n");
                    strSqlString.Append("             , 0 AS EOH " + "\n");
                    strSqlString.Append("             , 0 AS K4EOH " + "\n");
                    strSqlString.Append("          FROM RWIPLOTHIS A " + "\n");
                    strSqlString.Append("         WHERE A.FACTORY = 'RETURN' " + "\n");
                    strSqlString.Append("           AND A.OLD_FACTORY = '" + GlobalVariable.gsAssyDefaultFactory + "' " + "\n");
                    strSqlString.Append("           AND A.OPER = ' ' " + "\n");
                    strSqlString.Append("           AND A.OLD_OPER = 'A0000' " + "\n");
                    strSqlString.Append("           AND A.LOT_TYPE = 'W' " + "\n");
                    strSqlString.Append("           AND A.OWNER_CODE = 'PROD' " + "\n");
                    strSqlString.AppendFormat("           AND A.LOT_CMF_5 LIKE '{0}%' " + "\n", txtLotType.Text.Trim());
                    strSqlString.AppendFormat("           AND A.TRAN_TIME BETWEEN '{0}' AND '{1}'" + "\n", strFromDate, strToDate);
                    strSqlString.AppendFormat("           AND A.MAT_ID LIKE '{0}%' " + "\n", txtSearchProduct.Text.Trim());
                    //// 2009-03-23-정비재 : RETURN된 자재는 ASSY SITE를 검색조건에 넣지 않는다.
                    // 2009-05-08-정비재 : RETURN된 자재도 ASSY SITE를 구분한다.(이진영 대리 요청)
                    //// 조립Site(HM = 자사조립)
                    //if (rbtSite01.Checked == true)
                    //{
                    //    strSqlString.AppendFormat("           AND TRIM(A.LOT_CMF_7) = 'HM' " + "\n");
                    //}
                    //else
                    //{
                    //    strSqlString.AppendFormat("           AND TRIM(A.LOT_CMF_7) <> 'HM' " + "\n");
                    //}
                    strSqlString.Append("           AND A.HIST_DEL_FLAG <> 'Y' " + "\n");
                    strSqlString.Append("         GROUP BY A.MAT_ID, A.LOT_CMF_15 " + "\n");
                    strSqlString.Append("        UNION " + "\n");
                    /************************************************************************************************************/
                    /* ROUTASSY(RETURN_XX_HMK3 - RETURN_HMK4_HMK3) : HMK3 STOCKS에 반품되어 들어온 수량을 조회한다.             */
                    strSqlString.Append("        SELECT A.MAT_ID AS MAT_ID " + "\n");
                    strSqlString.Append("             , A.RETURN_TYPE AS RETURN_TYPE " + "\n");
                    strSqlString.Append("             , 0 AS BOH " + "\n");
                    strSqlString.Append("             , 0 AS INASSY " + "\n");
                    strSqlString.Append("             , 0 AS INTEST " + "\n");
                    strSqlString.Append("             , 0 AS ASYTST " + "\n");
                    strSqlString.Append("             , 0 AS OUTASSY " + "\n");
                    strSqlString.Append("             , 0 AS OUTTEST " + "\n");
                    strSqlString.Append("             , 0 AS ROUTASSY " + "\n");
                    strSqlString.Append("             , SUM(A.ROUTASSY) AS ROUTASSY " + "\n");
                    strSqlString.Append("             , 0 AS ROUTTEST " + "\n");
                    strSqlString.Append("             , 0 AS BONUS " + "\n");
                    strSqlString.Append("             , 0 AS TRANS " + "\n");
                    strSqlString.Append("             , 0 AS EOH " + "\n");
                    strSqlString.Append("             , 0 AS K4EOH " + "\n");
                    strSqlString.Append("          FROM ( " + "\n");
                    strSqlString.Append("                SELECT A.MAT_ID AS MAT_ID " + "\n");
                    strSqlString.Append("                     , CASE A.LOT_CMF_15 WHEN ' ' THEN '**' " + "\n");
                    strSqlString.Append("                                         ELSE A.LOT_CMF_15 " + "\n");
                    strSqlString.Append("                       END AS RETURN_TYPE " + "\n");
                    strSqlString.Append("                     , SUM(A.QTY_1) AS ROUTASSY " + "\n");
                    strSqlString.Append("                  FROM RWIPLOTHIS A " + "\n");
                    strSqlString.Append("                 WHERE A.FACTORY = '" + GlobalVariable.gsAssyDefaultFactory + "' " + "\n");
                    strSqlString.Append("                   AND A.OLD_FACTORY = '" + GlobalVariable.gsAssyDefaultFactory + "' " + "\n");
                    strSqlString.Append("                   AND A.OPER = 'AZ010' " + "\n");
                    strSqlString.Append("                   AND A.OLD_OPER = 'AZ010' " + "\n");
                    strSqlString.Append("                   AND A.TRAN_CODE = 'CREATE' " + "\n");
                    strSqlString.Append("                   AND A.LOT_TYPE = 'W' " + "\n");
                    strSqlString.Append("                   AND A.OWNER_CODE = 'PROD' " + "\n");
                    strSqlString.AppendFormat("                   AND A.LOT_CMF_5 LIKE '{0}%' " + "\n", txtLotType.Text.Trim());
                    strSqlString.AppendFormat("                   AND A.TRAN_TIME BETWEEN '{0}' AND '{1}'" + "\n", strFromDate, strToDate);
                    strSqlString.AppendFormat("                   AND A.MAT_ID LIKE '{0}%' " + "\n", txtSearchProduct.Text.Trim());
                    // 조립Site(HM = 자사조립)
                    // 2009-05-08-정비재 : RETURN된 자재도 ASSY SITE를 구분한다.(이진영 대리 요청)
                    //if (rbtSite01.Checked == true)
                    //{
                    //    strSqlString.AppendFormat("                   AND TRIM(A.LOT_CMF_7) = 'HM' " + "\n");
                    //}
                    //else
                    //{
                    //    strSqlString.AppendFormat("                   AND TRIM(A.LOT_CMF_7) <> 'HM' " + "\n");
                    //}
                    strSqlString.Append("                   AND A.HIST_DEL_FLAG <> 'Y' " + "\n");
                    strSqlString.Append("                 GROUP BY A.MAT_ID, A.LOT_CMF_15 " + "\n");
                    strSqlString.Append("                UNION " + "\n");
                    strSqlString.Append("                SELECT A.MAT_ID AS MAT_ID " + "\n");
                    strSqlString.Append("                     , CASE A.LOT_CMF_15 WHEN ' ' THEN '**' " + "\n");
                    strSqlString.Append("                                         ELSE A.LOT_CMF_15 " + "\n");
                    strSqlString.Append("                       END AS RETURN_TYPE " + "\n");
                    strSqlString.Append("                     , -SUM(A.QTY_1) AS ROUTASSY " + "\n");
                    strSqlString.Append("                  FROM RWIPLOTHIS A " + "\n");
                    strSqlString.Append("                 WHERE A.FACTORY = '" + GlobalVariable.gsAssyDefaultFactory + "' " + "\n");
                    strSqlString.Append("                   AND A.OPER IN ('AZ010', 'AZ009', 'T0000') " + "\n");
                    strSqlString.Append("                   AND A.OLD_FACTORY = '" + GlobalVariable.gsTestDefaultFactory + "' " + "\n");
                    strSqlString.Append("                   AND A.OLD_OPER = 'TZ010' " + "\n");
                    strSqlString.Append("                   AND A.LOT_TYPE = 'W' " + "\n");
                    strSqlString.Append("                   AND A.OWNER_CODE = 'PROD' " + "\n");
                    strSqlString.AppendFormat("                   AND A.LOT_CMF_5 LIKE '{0}%' " + "\n", txtLotType.Text.Trim());
                    strSqlString.AppendFormat("                   AND A.TRAN_TIME BETWEEN '{0}' AND '{1}'" + "\n", strFromDate, strToDate);
                    strSqlString.AppendFormat("                   AND A.MAT_ID LIKE '{0}%' " + "\n", txtSearchProduct.Text.Trim());
                    //// 조립Site(HM = 자사조립)
                    //// 2009-03-23-정비재 : RETURN된 자재는 ASSY SITE를 검색조건에 넣지 않는다.
                    // 2009-05-08-정비재 : RETURN된 자재도 ASSY SITE를 구분한다.(이진영 대리 요청)
                    //if (rbtSite01.Checked == true)
                    //{
                    //    strSqlString.AppendFormat("                   AND TRIM(A.LOT_CMF_7) = 'HM' " + "\n");
                    //}
                    //else
                    //{
                    //    strSqlString.AppendFormat("                   AND TRIM(A.LOT_CMF_7) <> 'HM' " + "\n");
                    //}
                    strSqlString.Append("                   AND A.HIST_DEL_FLAG <> 'Y' " + "\n");
                    strSqlString.Append("                 GROUP BY A.MAT_ID, A.LOT_CMF_15 " + "\n");
                    strSqlString.Append("               ) A" + "\n");
                    strSqlString.Append("         GROUP BY A.MAT_ID, A.RETURN_TYPE " + "\n");
                    strSqlString.Append("        UNION " + "\n");
                    /************************************************************************************************************/
                    /* ROUTTEST(RETURN_XX_HMK4) : HMK4 STOCKS에 반품되어 들어온 수량을 조회한다.                                */
                    strSqlString.Append("        SELECT A.MAT_ID AS MAT_ID " + "\n");
                    strSqlString.Append("             , CASE A.LOT_CMF_15 WHEN ' ' THEN '**' " + "\n");
                    strSqlString.Append("                                 ELSE A.LOT_CMF_15 " + "\n");
                    strSqlString.Append("               END AS RETURN_TYPE " + "\n");
                    strSqlString.Append("             , 0 AS BOH " + "\n");
                    strSqlString.Append("             , 0 AS INASSY " + "\n");
                    strSqlString.Append("             , 0 AS INTEST " + "\n");
                    strSqlString.Append("             , 0 AS ASYTST " + "\n");
                    strSqlString.Append("             , 0 AS OUTASSY " + "\n");
                    strSqlString.Append("             , 0 AS OUTTEST " + "\n");
                    strSqlString.Append("             , 0 AS RINASSY " + "\n");
                    strSqlString.Append("             , 0 AS ROUTASSY " + "\n");
                    strSqlString.Append("             , SUM(A.QTY_1) AS ROUTTEST " + "\n");
                    strSqlString.Append("             , 0 AS BONUS " + "\n");
                    strSqlString.Append("             , 0 AS TRANS " + "\n");
                    strSqlString.Append("             , 0 AS EOH " + "\n");
                    strSqlString.Append("             , 0 AS K4EOH " + "\n");
                    strSqlString.Append("          FROM RWIPLOTHIS A " + "\n");
                    strSqlString.Append("         WHERE A.FACTORY = '" + GlobalVariable.gsTestDefaultFactory + "' " + "\n");
                    strSqlString.Append("           AND A.OLD_FACTORY = '" + GlobalVariable.gsTestDefaultFactory + "' " + "\n");
                    strSqlString.Append("           AND A.OPER = 'TZ010' " + "\n");
                    strSqlString.Append("           AND A.OLD_OPER = 'TZ010' " + "\n");
                    strSqlString.Append("           AND A.TRAN_CODE = 'CREATE' " + "\n");
                    strSqlString.Append("           AND A.LOT_TYPE = 'W' " + "\n");
                    strSqlString.Append("           AND A.OWNER_CODE = 'PROD' " + "\n");
                    strSqlString.AppendFormat("           AND A.LOT_CMF_5 LIKE '{0}%' " + "\n", txtLotType.Text.Trim());
                    strSqlString.AppendFormat("           AND A.TRAN_TIME BETWEEN '{0}' AND '{1}'" + "\n", strFromDate, strToDate);
                    strSqlString.AppendFormat("           AND A.MAT_ID LIKE '{0}%' " + "\n", txtSearchProduct.Text.Trim());
                    // 조립Site(HM = 자사조립)
                    if (rbtSite01.Checked == true)
                    {
                        strSqlString.Append("           AND TRIM(A.LOT_CMF_7) = 'HM' " + "\n");
                    }
                    else
                    {
                        strSqlString.Append("           AND TRIM(A.LOT_CMF_7) <> 'HM' " + "\n");
                    }
                    strSqlString.Append("           AND A.HIST_DEL_FLAG <> 'Y' " + "\n");
                    strSqlString.Append("         GROUP BY A.MAT_ID, A.LOT_CMF_15 " + "\n");
                    strSqlString.Append("        UNION " + "\n");
                    /************************************************************************************************************/
                    /* BONUS(BONUS + ABNORMALSTART) : 전체공정에서 BONUS + ABNORMALSTART EVENT가 발생된 수량을 조회한다.        */
                    /* BONUS : 실제 BONUS 수량을 조회한다.(HALF CHIP 는 실제 BONUS, REWORK에서 제외한다)                        */
                    strSqlString.Append("        SELECT A.MAT_ID AS MAT_ID " + "\n");
                    strSqlString.Append("             , CASE B.LOT_CMF_15 WHEN ' ' THEN '**' " + "\n");
                    strSqlString.Append("                                 ELSE B.LOT_CMF_15 " + "\n");
                    strSqlString.Append("               END AS RETURN_TYPE " + "\n");
                    strSqlString.Append("             , 0 AS BOH " + "\n");
                    strSqlString.Append("             , 0 AS INASSY " + "\n");
                    strSqlString.Append("             , 0 AS INTEST " + "\n");
                    strSqlString.Append("             , 0 AS ASYTST " + "\n");
                    strSqlString.Append("             , 0 AS OUTASSY " + "\n");
                    strSqlString.Append("             , 0 AS OUTTEST " + "\n");
                    strSqlString.Append("             , 0 AS ROUTASSY " + "\n");
                    strSqlString.Append("             , 0 AS ROUTASSY " + "\n");
                    strSqlString.Append("             , 0 AS ROUTTEST " + "\n");
                    strSqlString.Append("             , SUM(A.TOTAL_BONUS_QTY) AS BONUS " + "\n");
                    strSqlString.Append("             , 0 AS TRANS " + "\n");
                    strSqlString.Append("             , 0 AS EOH " + "\n");
                    strSqlString.Append("             , 0 AS K4EOH " + "\n");
                    strSqlString.Append("          FROM RWIPLOTBNS A, RWIPLOTSTS B " + "\n");
                    strSqlString.Append("         WHERE A.FACTORY = B.FACTORY " + "\n");
                    strSqlString.Append("           AND A.MAT_ID = B.MAT_ID " + "\n");
                    strSqlString.Append("           AND A.LOT_ID = B.LOT_ID " + "\n");
                    strSqlString.Append("           AND B.LOT_TYPE = 'W' " + "\n");
                    strSqlString.Append("           AND B.OWNER_CODE = 'PROD' " + "\n");
                    strSqlString.Append("           AND B.LOT_CMF_5 LIKE 'P%' " + "\n");
                    strSqlString.Append("           AND A.FACTORY IN ('" + GlobalVariable.gsAssyDefaultFactory + "', '" + GlobalVariable.gsTestDefaultFactory + "') " + "\n");
                    strSqlString.AppendFormat("           AND A.TRAN_TIME BETWEEN '{0}' AND '{1}'" + "\n", strFromDate, strToDate);
                    strSqlString.AppendFormat("           AND A.MAT_ID LIKE '{0}%' " + "\n", txtSearchProduct.Text.Trim());
                    // 조립Site(HM = 자사조립)
                    if (rbtSite01.Checked == true)
                    {
                        strSqlString.Append("           AND B.LOT_CMF_7 = 'HM' " + "\n");
                    }
                    else
                    {
                        strSqlString.Append("           AND B.LOT_CMF_7 <> 'HM' " + "\n");
                    }
                    strSqlString.Append("         GROUP BY A.MAT_ID, B.LOT_CMF_15 " + "\n");
                    strSqlString.Append("        UNION " + "\n");
                    /************************************************************************************************************/
                    /* TRANSFER : HMK2, HMK3, HMK4 에서 TERMINATE된 수량을 조회한다.                                            */
                    strSqlString.Append("        SELECT A.MAT_ID AS MAT_ID " + "\n");
                    strSqlString.Append("             , CASE A.LOT_CMF_15 WHEN ' ' THEN '**' " + "\n");
                    strSqlString.Append("                                 ELSE A.LOT_CMF_15 " + "\n");
                    strSqlString.Append("               END AS RETURN_TYPE " + "\n");
                    strSqlString.Append("             , 0 AS BOH " + "\n");
                    strSqlString.Append("             , 0 AS INASSY " + "\n");
                    strSqlString.Append("             , 0 AS INTEST " + "\n");
                    strSqlString.Append("             , 0 AS ASYTST " + "\n");
                    strSqlString.Append("             , 0 AS OUTASSY " + "\n");
                    strSqlString.Append("             , 0 AS OUTTEST " + "\n");
                    strSqlString.Append("             , 0 AS RINASSY " + "\n");
                    strSqlString.Append("             , 0 AS ROUTASSY " + "\n");
                    strSqlString.Append("             , 0 AS ROUTTEST " + "\n");
                    strSqlString.Append("             , 0 AS BONUS " + "\n");
                    strSqlString.Append("             , SUM(A.QTY_1) AS TRANS " + "\n");
                    strSqlString.Append("             , 0 AS EOH " + "\n");
                    strSqlString.Append("             , 0 AS K4EOH " + "\n");
                    strSqlString.Append("          FROM RWIPLOTHIS A " + "\n");
                    strSqlString.Append("         WHERE A.FACTORY = A.OLD_FACTORY " + "\n");
                    strSqlString.Append("           AND A.OPER = A.OLD_OPER " + "\n");
                    strSqlString.Append("           AND A.FACTORY IN ('" + GlobalVariable.gsAssyDefaultFactory + "', '" + GlobalVariable.gsTestDefaultFactory + "') " + "\n");
                    strSqlString.Append("           AND A.TRAN_CODE = 'TERMINATE' " + "\n");
                    strSqlString.Append("           AND A.LOT_TYPE = 'W' " + "\n");
                    strSqlString.Append("           AND A.OWNER_CODE = 'PROD' " + "\n");
                    strSqlString.AppendFormat("           AND A.LOT_CMF_5 LIKE '{0}%' " + "\n", txtLotType.Text.Trim());
                    strSqlString.AppendFormat("           AND A.TRAN_TIME BETWEEN '{0}' AND '{1}'" + "\n", strFromDate, strToDate);
                    strSqlString.AppendFormat("           AND A.MAT_ID LIKE '{0}%' " + "\n", txtSearchProduct.Text.Trim());
                    // 조립Site(HM = 자사조립)
                    if (rbtSite01.Checked == true)
                    {
                        strSqlString.Append("           AND A.LOT_CMF_7 = 'HM' " + "\n");
                    }
                    else
                    {
                        strSqlString.Append("           AND A.LOT_CMF_7 <> 'HM' " + "\n");
                    }
                    strSqlString.Append("           AND A.HIST_DEL_FLAG <> 'Y' " + "\n");
                    strSqlString.Append("         GROUP BY A.MAT_ID, A.LOT_CMF_15 " + "\n");
                    strSqlString.Append("        UNION " + "\n");
                    /********************************************************************************************************/
                    /* EOH : 각 공정별 EOH수량을 조회한다.                                                                  */
                    strSqlString.Append("        SELECT A.MAT_ID AS MAT_ID " + "\n");
                    strSqlString.Append("             , A.RETURN_TYPE AS RETURN_TYPE " + "\n");
                    strSqlString.Append("             , 0 AS BOH " + "\n");
                    strSqlString.Append("             , 0 AS INASSY " + "\n");
                    strSqlString.Append("             , 0 AS INTEST " + "\n");
                    strSqlString.Append("             , 0 AS ASYTST " + "\n");
                    strSqlString.Append("             , 0 AS OUTASSY " + "\n");
                    strSqlString.Append("             , 0 AS OUTTEST " + "\n");
                    strSqlString.Append("             , 0 AS RINASSY " + "\n");
                    strSqlString.Append("             , 0 AS ROUTASSY " + "\n");
                    strSqlString.Append("             , 0 AS ROUTTEST " + "\n");
                    strSqlString.Append("             , 0 AS BONUS " + "\n");
                    strSqlString.Append("             , 0 AS TRANS " + "\n");
                    strSqlString.Append("             , SUM(A.EOH) AS EOH " + "\n");
                    strSqlString.Append("             , 0 AS K4EOH " + "\n");
                    strSqlString.Append("          FROM ( " + "\n");
                    /* EOH : HMK2의 ASSY SITE가 않맞는 문제로 HMK2 EOH를 별도로 계산한다.(HMK2는 ASSY SITE를 구분하지 않는다.)  */
                    strSqlString.Append("                SELECT A.MAT_ID AS MAT_ID " + "\n");
                    strSqlString.Append("                     , CASE A.LOT_CMF_15 WHEN ' ' THEN '**' " + "\n");
                    strSqlString.Append("                                         ELSE A.LOT_CMF_15 " + "\n");
                    strSqlString.Append("                       END AS RETURN_TYPE " + "\n");
                    strSqlString.Append("                     , SUM(A.QTY_1) AS EOH " + "\n");
                    // EOH구분(정상 = RWIPLOTSTS_BOH, 금일 = RWIPLOTSTS)
                    if (rbtEoh01.Checked == true)
                    {
                        strSqlString.Append("                  FROM RWIPLOTSTS_BOH A " + "\n");
                    }
                    else
                    {
                        strSqlString.Append("                  FROM RWIPLOTSTS A " + "\n");
                    }
                    strSqlString.Append("                 WHERE A.FACTORY = '" + GlobalVariable.gsAssyDefaultFactory + "' " + "\n");
                    strSqlString.Append("                   AND A.OPER = 'A0000' " + "\n");
                    strSqlString.Append("                   AND A.LOT_TYPE = 'W' " + "\n");
                    strSqlString.Append("                   AND A.OWNER_CODE = 'PROD' " + "\n");
                    // EOH구분(정상 = RWIPLOTSTS_BOH, 금일 = RWIPLOTSTS)
                    if (rbtEoh01.Checked == true)
                    {
                        strSqlString.AppendFormat("                   AND A.CUTOFF_DT = '{0}' " + "\n", strToCutOffDate);
                    }
                    else
                    {
                        // 금일의 EOH는 RWIPLOTSTS Table의 값을 조회한다.
                        strSqlString.AppendFormat("                   AND A.LOT_DEL_FLAG <> 'Y' " + "\n");
                    }
                    strSqlString.AppendFormat("                   AND A.LOT_CMF_5 LIKE '{0}%' " + "\n", txtLotType.Text.Trim());
                    strSqlString.AppendFormat("                   AND A.MAT_ID LIKE '{0}%' " + "\n", txtSearchProduct.Text.Trim());
                    //// 조립Site(HM = 자사조립)
                    // 2009-05-08-정비재 : RETURN된 자재도 ASSY SITE를 구분한다.(이진영 대리 요청)
                    //if (rbtSite01.Checked == true)
                    //{
                    //    strSqlString.Append("                   AND TRIM(A.LOT_CMF_7) = 'HM' " + "\n");
                    //}
                    //else
                    //{
                    //    strSqlString.Append("                   AND TRIM(A.LOT_CMF_7) <> 'HM' " + "\n");
                    //}
                    strSqlString.Append("                 GROUP BY A.MAT_ID, A.LOT_CMF_15 " + "\n");
                    strSqlString.Append("                UNION " + "\n");
                    /************************************************************************************************************/
                    /* BOH : HMK2를 제외한 공정의 BOH를 계산한다.                                                               */
                    strSqlString.Append("                SELECT A.MAT_ID AS MAT_ID " + "\n");
                    strSqlString.Append("                     , CASE A.LOT_CMF_15 WHEN ' ' THEN '**' " + "\n");
                    strSqlString.Append("                                         ELSE A.LOT_CMF_15 " + "\n");
                    strSqlString.Append("                       END AS RETURN_TYPE " + "\n");
                    strSqlString.Append("                     , SUM(A.QTY_1) AS EOH " + "\n");
                    // EOH구분(정상 = RWIPLOTSTS_BOH, 금일 = RWIPLOTSTS)
                    if (rbtEoh01.Checked == true)
                    {
                        strSqlString.Append("                  FROM RWIPLOTSTS_BOH A " + "\n");
                    }
                    else
                    {
                        strSqlString.Append("                  FROM RWIPLOTSTS A " + "\n");
                    }
                    strSqlString.Append("                 WHERE A.FACTORY IN ('" + GlobalVariable.gsAssyDefaultFactory + "', '" + GlobalVariable.gsTestDefaultFactory + "') " + "\n");
                    strSqlString.Append("                   AND A.OPER > 'A0000' " + "\n");
                    strSqlString.Append("                   AND A.LOT_TYPE = 'W' " + "\n");
                    strSqlString.Append("                   AND A.OWNER_CODE = 'PROD' " + "\n");
                    // EOH구분(정상 = RWIPLOTSTS_BOH, 금일 = RWIPLOTSTS)
                    if (rbtEoh01.Checked == true)
                    {
                        strSqlString.AppendFormat("                   AND A.CUTOFF_DT = '{0}' " + "\n", strToCutOffDate);
                    }
                    else
                    {
                        // 금일의 EOH는 RWIPLOTSTS Table의 값을 조회한다.
                        strSqlString.AppendFormat("                   AND A.LOT_DEL_FLAG <> 'Y' " + "\n");
                    }
                    strSqlString.AppendFormat("                   AND A.LOT_CMF_5 LIKE '{0}%' " + "\n", txtLotType.Text.Trim());
                    strSqlString.AppendFormat("                   AND A.MAT_ID LIKE '{0}%' " + "\n", txtSearchProduct.Text.Trim());
                    // 조립Site(HM = 자사조립) 
                    // 2009-05-08-정비재 : RETURN된 자재도 ASSY SITE를 구분한다.(이진영 대리 요청)
                    //if (rbtSite01.Checked == true)
                    //{
                    //    strSqlString.AppendFormat("                   AND A.LOT_CMF_7 = 'HM' " + "\n");
                    //}
                    //else
                    //{
                    //    strSqlString.AppendFormat("                   AND A.LOT_CMF_7 <> 'HM' " + "\n");
                    //}
                    strSqlString.Append("                 GROUP BY A.MAT_ID, A.LOT_CMF_15 " + "\n");
                    strSqlString.Append("                ) A " + "\n");
                    strSqlString.Append("         GROUP BY A.MAT_ID, A.RETURN_TYPE " + "\n");
                    strSqlString.Append("     ) A , (SELECT DISTINCT MAT_ID AS MAT_ID " + "\n");
                    strSqlString.Append("                , MAT_TYPE AS MAT_TYPE " + "\n");
                    strSqlString.Append("                , DELETE_FLAG AS DELETE_FLAG " + "\n");
                    strSqlString.Append("                , DECODE(MAT_GRP_1, ' ', '-', MAT_GRP_1) AS MAT_GRP_1 " + "\n");
                    strSqlString.Append("                , DECODE(MAT_GRP_2, ' ', '-', MAT_GRP_2) AS MAT_GRP_2 " + "\n");
                    strSqlString.Append("                , DECODE(MAT_GRP_3, ' ', '-', MAT_GRP_3) AS MAT_GRP_3 " + "\n");
                    strSqlString.Append("                , DECODE(MAT_GRP_4, ' ', '-', MAT_GRP_4) AS MAT_GRP_4 " + "\n");
                    strSqlString.Append("                , DECODE(MAT_GRP_5, ' ', '-', MAT_GRP_5) AS MAT_GRP_5 " + "\n");
                    strSqlString.Append("                , DECODE(MAT_GRP_6, ' ', '-', MAT_GRP_6) AS MAT_GRP_6 " + "\n");
                    strSqlString.Append("                , DECODE(MAT_GRP_7, ' ', '-', MAT_GRP_7) AS MAT_GRP_7 " + "\n");
                    strSqlString.Append("                , DECODE(MAT_GRP_8, ' ', '-', MAT_GRP_8) AS MAT_GRP_8 " + "\n");
                    strSqlString.Append("                , DECODE(MAT_CMF_10, ' ', '-', MAT_CMF_10) AS MAT_CMF_10 " + "\n");
                    strSqlString.Append("             FROM MWIPMATDEF " + "\n");
                    strSqlString.Append("            WHERE MAT_TYPE = 'FG' " + "\n");
                    strSqlString.Append("              AND DELETE_FLAG <> 'Y') B " + "\n");
                    strSqlString.Append("                 ,(" + "\n");
                    strSqlString.Append("                         SELECT MAT_ID , SUM(TRANS_QTY + LOSS_QTY) AS SUM_QTY " + "\n");
                    strSqlString.Append("                          FROM (" + "\n");
                    strSqlString.Append("                                 SELECT A.MAT_ID                         " + "\n");
                    strSqlString.Append("                                      , CASE WHEN A.OPER <= 'A0395' THEN A.TRANS_QTY " + "\n");
                    strSqlString.Append("                                             WHEN B.MAT_GRP_5 = 'Merge' THEN A.TRANS_QTY" + "\n");
                    strSqlString.Append("                                             ELSE A.TRANS_QTY * NVL(B.COMP_CNT,1) " + "\n");
                    strSqlString.Append("                                        END TRANS_QTY " + "\n");
                    strSqlString.Append("                                      , CASE WHEN A.OPER <= 'A0395' THEN A.LOSS_QTY " + "\n");
                    strSqlString.Append("                                             WHEN B.MAT_GRP_5 = 'Merge' THEN A.LOSS_QTY" + "\n");
                    strSqlString.Append("                                             ELSE A.LOSS_QTY * NVL(B.COMP_CNT,1) " + "\n");
                    strSqlString.Append("                                        END LOSS_QTY                          " + "\n");
                    strSqlString.Append("                                   FROM (" + "\n");
                    strSqlString.Append("                                         SELECT MAT_ID" + "\n");
                    strSqlString.Append("                                              , OPER" + "\n");
                    strSqlString.Append("                                              , SUM(QTY_1) AS TRANS_QTY  " + "\n");
                    strSqlString.Append("                                              , 0 AS LOSS_QTY" + "\n");
                    strSqlString.Append("                                           FROM RWIPLOTHIS  " + "\n");
                    strSqlString.Append("                                          WHERE 1=1  " + "\n");
                    strSqlString.Append("                                            AND OPER = OLD_OPER  " + "\n");
                    strSqlString.Append("                                            AND TRAN_TIME BETWEEN '20200921220001' AND '20200923220000' " + "\n");
                    strSqlString.Append("                                            AND FACTORY = 'HMKA1'  " + "\n");
                    strSqlString.Append("                                            AND TRAN_CODE = 'TERMINATE'  " + "\n");
                    strSqlString.Append("                                            AND LOT_TYPE = 'W'  " + "\n");
                    strSqlString.Append("                                            AND HIST_DEL_FLAG = ' ' " + "\n");
                    strSqlString.Append("                                            AND MAT_ID LIKE 'SE%' " + "\n");
                    strSqlString.Append("                                            AND LOT_CMF_5 LIKE 'P%'" + "\n");
                    strSqlString.Append("                                          GROUP BY MAT_ID, OPER  " + "\n");
                    strSqlString.Append("                                         UNION ALL  " + "\n");
                    strSqlString.Append("                                         SELECT LSM.MAT_ID " + "\n");
                    strSqlString.Append("                                              , LSM.OPER" + "\n");
                    strSqlString.Append("                                              , 0 AS TRANS_QTY  " + "\n");
                    strSqlString.Append("                                              , SUM(DECODE(LOSS_CODE, 'N01', 0, LOSS_QTY)) AS LOSS_QTY    " + "\n");
                    strSqlString.Append("                                           FROM RWIPLOTLSM LSM  " + "\n");
                    strSqlString.Append("                                              , RWIPLOTSTS LOT  " + "\n");
                    strSqlString.Append("                                          WHERE 1=1  " + "\n");
                    strSqlString.Append("                                            AND LOT.FACTORY IN ('HMKA1', 'CUSTOMER')" + "\n");
                    strSqlString.Append("                                            AND LSM.LOT_ID = LOT.LOT_ID             " + "\n");
                    strSqlString.Append("                                            AND LSM.FACTORY = 'HMKA1'             " + "\n");
                    strSqlString.Append("                                            AND LSM.TRAN_TIME BETWEEN '20200921220001' AND '20200923220000'  " + "\n");
                    strSqlString.Append("                                            AND LSM.MAT_ID LIKE 'SE%'  " + "\n");
                    strSqlString.Append("                                            AND LOT.LOT_TYPE = 'W' " + "\n");
                    strSqlString.Append("                                            AND LOT.LOT_CMF_5 LIKE 'P%'" + "\n");
                    strSqlString.Append("                                          GROUP BY LSM.MAT_ID, LSM.OPER" + "\n");
                    strSqlString.Append("                                        ) A" + "\n");
                    strSqlString.Append("                                      , VWIPMATDEF B" + "\n");
                    strSqlString.Append("                                  WHERE 1=1" + "\n");
                    strSqlString.Append("                                    AND A.MAT_ID = B.MAT_ID" + "\n");
                    strSqlString.Append("                                    AND B.FACTORY = 'HMKA1'" + "\n");
                    strSqlString.Append("                                    AND B.DELETE_FLAG = ' ' " + "\n");
                    strSqlString.Append("                                ) GROUP BY MAT_ID " + "\n");
                    strSqlString.Append("                  ) D " + "\n");
                    strSqlString.Append(" WHERE A.MAT_ID = B.MAT_ID " + "\n");
                    strSqlString.Append("   AND A.MAT_ID = D.MAT_ID(+) " + "\n");
                    strSqlString.Append("   AND B.MAT_TYPE = 'FG' " + "\n");
                    strSqlString.Append("   AND B.DELETE_FLAG <> 'Y' " + "\n");
                    //상세 조회에 따른 SQL문 생성
                    /********************************************************************************************************************/
                    if (udcWIPCondition1.Text != "ALL" && udcWIPCondition1.Text != "")
                        strSqlString.AppendFormat("   AND B.MAT_GRP_1 {0} " + "\n", udcWIPCondition1.SelectedValueToQueryString);
                    if (udcWIPCondition2.Text != "ALL" && udcWIPCondition2.Text != "")
                        strSqlString.AppendFormat("   AND B.MAT_GRP_2 {0} " + "\n", udcWIPCondition2.SelectedValueToQueryString);
                    if (udcWIPCondition3.Text != "ALL" && udcWIPCondition3.Text != "")
                        strSqlString.AppendFormat("   AND B.MAT_GRP_3 {0} " + "\n", udcWIPCondition3.SelectedValueToQueryString);
                    if (udcWIPCondition4.Text != "ALL" && udcWIPCondition4.Text != "")
                        strSqlString.AppendFormat("   AND B.MAT_GRP_4 {0} " + "\n", udcWIPCondition4.SelectedValueToQueryString);
                    if (udcWIPCondition5.Text != "ALL" && udcWIPCondition5.Text != "")
                        strSqlString.AppendFormat("   AND B.MAT_GRP_5 {0} " + "\n", udcWIPCondition5.SelectedValueToQueryString);
                    if (udcWIPCondition6.Text != "ALL" && udcWIPCondition6.Text != "")
                        strSqlString.AppendFormat("   AND B.MAT_GRP_6 {0} " + "\n", udcWIPCondition6.SelectedValueToQueryString);
                    if (udcWIPCondition7.Text != "ALL" && udcWIPCondition7.Text != "")
                        strSqlString.AppendFormat("   AND B.MAT_GRP_7 {0} " + "\n", udcWIPCondition7.SelectedValueToQueryString);
                    if (udcWIPCondition8.Text != "ALL" && udcWIPCondition8.Text != "")
                        strSqlString.AppendFormat("   AND B.MAT_GRP_8 {0} " + "\n", udcWIPCondition8.SelectedValueToQueryString);
                    if (udcWIPCondition9.Text != "ALL" && udcWIPCondition9.Text != "")
                        strSqlString.AppendFormat("   AND B.MAT_GRP_9 {0} " + "\n", udcWIPCondition9.SelectedValueToQueryString);
                    /********************************************************************************************************************/
                    strSqlString.AppendFormat(" GROUP BY {0} " + "\n", QueryCond1);
                    strSqlString.AppendFormat(" ORDER BY {0} " + "\n", QueryCond1);
                }
                #endregion

                #region 2.HMV2
                else
                {
                    strSqlString.Append("WITH DT AS" + "\n");
                    strSqlString.Append("( " + "\n");
                    strSqlString.Append("SELECT LOT_ID, HIST_SEQ, OLD_HIST_SEQ, MAT_ID, OLD_MAT_ID, OPER, QTY_1" + "\n");
                    strSqlString.Append("  FROM MESMGR.MWIPADTHIS@RPTTOMES " + "\n");
                    strSqlString.Append(" WHERE LOT_TYPE = 'W' " + "\n");
                    strSqlString.Append("   AND TRAN_CODE = 'ADAPT' " + "\n");
                    strSqlString.Append("   AND TRAN_TIME BETWEEN  '" + strFromDate + "' AND '" + strToDate + "' \n");
                    strSqlString.Append("   AND MAT_ID LIKE '%'" + "\n");
                    strSqlString.Append("   AND LOT_CMF_5 LIKE 'P%'" + "\n");
                    strSqlString.Append("   AND TRAN_CMF_5 = 'ADAPT-PART_CHANGE'" + "\n");
                    strSqlString.Append("   AND HIST_DEL_FLAG = ' '" + "\n");
                    strSqlString.Append("   AND OPER LIKE 'A%'" + "\n");
                    strSqlString.Append(")" + "\n");
                    strSqlString.Append(", DT2 AS" + "\n");
                    strSqlString.Append("( " + "\n");
                    strSqlString.Append("SELECT OLD_MAT_ID AS ORIGINAL_PART, MAT_ID AS CHANGE_PART, SUM(QTY_1) AS QTY_1" + "\n");
                    strSqlString.Append("  FROM ( " + "\n");
                    strSqlString.Append("        SELECT A.*, B.LOT_ID AS B_LOT_ID " + "\n");
                    strSqlString.Append("          FROM DT A, MESMGR.MWIPLOTCMB@RPTTOMES B" + "\n");
                    strSqlString.Append("         WHERE 1=1 " + " \n");
                    strSqlString.Append("           AND A.LOT_ID = B.LOT_ID(+)" + "\n");
                    strSqlString.Append("           AND A.OLD_HIST_SEQ = B.HIST_SEQ(+)" + "\n");
                    strSqlString.Append("       )" + "\n");
                    strSqlString.Append(" WHERE 1=1" + "\n");
                    strSqlString.Append("   AND B_LOT_ID IS NULL" + "\n");
                    strSqlString.Append(" GROUP BY OLD_MAT_ID, MAT_ID" + "\n");
                    strSqlString.Append(")" + "\n");
                    strSqlString.AppendFormat("SELECT {0} " + "\n", QueryCond3);
                    /* 결산 : 월 결산을 하기 위한 QUERY문	*/
                    strSqlString.Append("     , SUM(A.BOH) AS BOH " + "\n");
                    strSqlString.Append("     , MAX(NVL(C.PART_IN,0)) AS PART_IN  " + "\n");
                    strSqlString.Append("     , MAX(NVL(C.PART_OUT,0)) AS PART_OUT  " + "\n");
                    strSqlString.Append("     , SUM(A.INASSY) AS INASSY  " + "\n");
                    strSqlString.Append("     , SUM(A.OUTASSY) AS OUTASSY " + "\n");
                    strSqlString.Append("     , SUM(A.RINASSY) AS RINASSY " + "\n");
                    strSqlString.Append("     , SUM(A.ROUTASSY) AS ROUTASSY " + "\n");
                    strSqlString.Append("     , SUM(A.INTEST) AS INTEST " + "\n");
                    strSqlString.Append("     , SUM(A.OUTTEST) AS OUTTEST " + "\n");
                    strSqlString.Append("     , SUM(A.OUTASSY + A.OUTTEST) AS OUT_TTL " + "\n");
                    strSqlString.Append("     , SUM(A.ROUTTEST) AS T_ROUT " + "\n");
                    strSqlString.Append("     , SUM(A.ROUTASSY + A.ROUTTEST) AS ROUT_TTL " + "\n");
                    strSqlString.Append("     , -((SUM(A.OUTASSY) + SUM(A.EOH) + SUM(A.RINASSY)) - (SUM(A.BOH) + MAX(NVL(C.PART_IN+C.PART_OUT,0))+ SUM(A.INASSY))) AS LOSS_V1   " + "\n");
                    
                    // 2020-10-15-임종우 : 사용하지도 않고 혼선에 여지가 있어서 Real Loss 표시 안 함.
                    strSqlString.Append("     , 0 AS LOSS_REAL" + "\n");
                    //strSqlString.Append("     , SUM(NVL(D.SUM_QTY,0)) AS LOSS_REAL" + "\n");
                    strSqlString.Append("     , SUM(A.EOH) AS EOH " + "\n");
                    strSqlString.Append("     , SUM(A.BONUS) AS BONUS " + "\n");
                    strSqlString.Append("     , SUM(A.TRANS) AS TRANS " + "\n");
                    strSqlString.Append("     , ROUND((SUM(A.OUTASSY) + SUM(A.EOH) + SUM(A.RINASSY)) / (SUM(A.BOH) + MAX(NVL(C.PART_IN+C.PART_OUT,0))+ SUM(A.INASSY))*100,2) AS YIELD" + "\n");
                    strSqlString.Append("     , SUM(A.OUTASSY - A.ROUTASSY) AS K3_SHIP " + "\n");
                    strSqlString.Append("  FROM ( " + "\n");
                    /************************************************************************************************************/
                    /* BOH : 각 공정별 BOH수량을 조회한다.                                                                      */
                    strSqlString.Append("        SELECT A.MAT_ID AS MAT_ID " + "\n");
                    strSqlString.Append("             , A.RETURN_TYPE AS RETURN_TYPE " + "\n");
                    strSqlString.Append("             , SUM(A.BOH) AS BOH " + "\n");
                    strSqlString.Append("             , 0 AS INASSY " + "\n");
                    strSqlString.Append("             , 0 AS INTEST " + "\n");
                    strSqlString.Append("             , 0 AS ASYTST " + "\n");
                    strSqlString.Append("             , 0 AS OUTASSY " + "\n");
                    strSqlString.Append("             , 0 AS OUTTEST " + "\n");
                    strSqlString.Append("             , 0 AS RINASSY " + "\n");
                    strSqlString.Append("             , 0 AS ROUTASSY " + "\n");
                    strSqlString.Append("             , 0 AS ROUTTEST " + "\n");
                    strSqlString.Append("             , 0 AS BONUS " + "\n");
                    strSqlString.Append("             , 0 AS TRANS " + "\n");
                    strSqlString.Append("             , 0 AS EOH " + "\n");
                    strSqlString.Append("             , 0 AS K4EOH " + "\n");
                    strSqlString.Append("          FROM ( " + "\n");
                    // 조립Site(HM = 자사조립) 
                    if (rbtSite01.Checked == true)
                    {                        
                        strSqlString.Append("                SELECT A.MAT_ID AS MAT_ID " + "\n");
                        strSqlString.Append("                     , CASE A.LOT_CMF_15 WHEN ' ' THEN '**' " + "\n");
                        strSqlString.Append("                                         ELSE A.LOT_CMF_15 " + "\n");
                        strSqlString.Append("                       END AS RETURN_TYPE " + "\n");
                        strSqlString.Append("                     , SUM(A.QTY_1) AS BOH " + "\n");
                        strSqlString.Append("                  FROM RWIPLOTSTS_BOH A " + "\n");
                        strSqlString.Append("                 WHERE A.FACTORY = '" + GlobalVariable.gsAssyDefaultFactory + "' " + "\n");                        
                        strSqlString.Append("                   AND A.LOT_TYPE = 'W' " + "\n");
                        strSqlString.Append("                   AND A.OWNER_CODE = 'PROD' " + "\n");
                        strSqlString.AppendFormat("                   AND A.CUTOFF_DT = '{0}' " + "\n", strFromCutOffDate);
                        strSqlString.AppendFormat("                   AND A.LOT_CMF_5 LIKE '{0}%' " + "\n", txtLotType.Text.Trim());
                        strSqlString.AppendFormat("                   AND A.MAT_ID LIKE '{0}%' " + "\n", txtSearchProduct.Text.Trim());
                        strSqlString.Append("                 GROUP BY A.MAT_ID, A.LOT_CMF_15 " + "\n");
                        strSqlString.Append("                ) A " + "\n");
                        strSqlString.Append("         GROUP BY A.MAT_ID, A.RETURN_TYPE " + "\n");
                    }
                    else
                    {
                        /************************************************************************************************************/
                        /* BOH : HMK2를 제외한 공정의 BOH를 계산한다.                                                               */
                        strSqlString.Append("                SELECT A.MAT_ID AS MAT_ID " + "\n");
                        strSqlString.Append("                     , CASE A.LOT_CMF_15 WHEN ' ' THEN '**' " + "\n");
                        strSqlString.Append("                                         ELSE A.LOT_CMF_15 " + "\n");
                        strSqlString.Append("                       END AS RETURN_TYPE " + "\n");
                        strSqlString.Append("                     , SUM(A.QTY_1) AS BOH " + "\n");
                        strSqlString.Append("                  FROM RWIPLOTSTS_BOH A " + "\n");
                        strSqlString.Append("                 WHERE A.FACTORY = '" + GlobalVariable.gsTestDefaultFactory + "' " + "\n");
                        strSqlString.Append("                   AND A.OPER >= 'T0000' " + "\n");
                        strSqlString.Append("                   AND A.LOT_TYPE = 'W' " + "\n");
                        strSqlString.Append("                   AND A.OWNER_CODE = 'PROD' " + "\n");
                        strSqlString.AppendFormat("                   AND A.CUTOFF_DT = '{0}' " + "\n", strFromCutOffDate);
                        strSqlString.AppendFormat("                   AND A.LOT_CMF_5 LIKE '{0}%' " + "\n", txtLotType.Text.Trim());
                        strSqlString.AppendFormat("                   AND A.MAT_ID LIKE '{0}%' " + "\n", txtSearchProduct.Text.Trim());
                        strSqlString.AppendFormat("                   AND TRIM(A.LOT_CMF_7) <> 'HM' " + "\n");
                        strSqlString.Append("                 GROUP BY A.MAT_ID, A.LOT_CMF_15 " + "\n");
                        strSqlString.Append("                ) A " + "\n");
                        strSqlString.Append("         GROUP BY A.MAT_ID, A.RETURN_TYPE " + "\n");
                    }
                    strSqlString.Append("         UNION " + "\n");
                    // 조립Site(HM = 자사조립) 
                    if (rbtSite01.Checked == true)
                    {
                        /************************************************************************************************************/
                        /* INASSY(RECV_XX_HMK2) : HMK2 RECIVE(MES의 A0000공정의 IN 수량) 수량을 조회한다.                           */
                        strSqlString.Append("        SELECT A.MAT_ID AS MAT_ID " + "\n");
                        strSqlString.Append("             , CASE A.LOT_CMF_15 WHEN ' ' THEN '**' " + "\n");
                        strSqlString.Append("                                 ELSE A.LOT_CMF_15 " + "\n");
                        strSqlString.Append("               END AS RETURN_TYPE " + "\n");
                        strSqlString.Append("             , 0 AS BOH " + "\n");
                        strSqlString.Append("             , SUM(A.QTY_1) AS INASSY " + "\n");
                        strSqlString.Append("             , 0 AS INTEST " + "\n");
                        strSqlString.Append("             , 0 AS ASYTST " + "\n");
                        strSqlString.Append("             , 0 AS OUTASSY " + "\n");
                        strSqlString.Append("             , 0 AS OUTTEST " + "\n");
                        strSqlString.Append("             , 0 AS RINASSY " + "\n");
                        strSqlString.Append("             , 0 AS ROUTASSY " + "\n");
                        strSqlString.Append("             , 0 AS ROUTTEST " + "\n");
                        strSqlString.Append("             , 0 AS BONUS " + "\n");
                        strSqlString.Append("             , 0 AS TRANS " + "\n");
                        strSqlString.Append("             , 0 AS EOH " + "\n");
                        strSqlString.Append("             , 0 AS K4EOH " + "\n");
                        strSqlString.Append("          FROM RWIPLOTHIS A " + "\n");
                        strSqlString.Append("         WHERE A.FACTORY = '" + GlobalVariable.gsAssyDefaultFactory + "' " + "\n");
                        strSqlString.Append("           AND A.OPER = 'A0000' " + "\n");
                        strSqlString.Append("           AND A.TRAN_CODE = 'CREATE' " + "\n");
                        strSqlString.Append("           AND A.LOT_TYPE = 'W' " + "\n");
                        strSqlString.Append("           AND A.OWNER_CODE = 'PROD' " + "\n");
                        strSqlString.AppendFormat("           AND A.LOT_CMF_5 LIKE '{0}%' " + "\n", txtLotType.Text.Trim());
                        strSqlString.AppendFormat("           AND A.TRAN_TIME BETWEEN '{0}' AND '{1}' " + "\n", strFromDate, strToDate);
                        strSqlString.AppendFormat("           AND A.MAT_ID LIKE '{0}%' " + "\n", txtSearchProduct.Text.Trim());
                        strSqlString.Append("           AND A.HIST_DEL_FLAG <> 'Y' " + "\n");
                        strSqlString.Append("         GROUP BY A.MAT_ID, A.LOT_CMF_15 " + "\n");
                    }
                    else
                    {
                        strSqlString.Append("        SELECT ' ' AS MAT_ID " + "\n");
                        strSqlString.Append("             , ' ' AS RETURN_TYPE " + "\n");
                        strSqlString.Append("             , 0 AS BOH " + "\n");
                        strSqlString.Append("             , 0 AS INASSY " + "\n");
                        strSqlString.Append("             , 0 AS INTEST " + "\n");
                        strSqlString.Append("             , 0 AS ASYTST " + "\n");
                        strSqlString.Append("             , 0 AS OUTASSY " + "\n");
                        strSqlString.Append("             , 0 AS OUTTEST " + "\n");
                        strSqlString.Append("             , 0 AS RINASSY " + "\n");
                        strSqlString.Append("             , 0 AS ROUTASSY " + "\n");
                        strSqlString.Append("             , 0 AS ROUTTEST " + "\n");
                        strSqlString.Append("             , 0 AS BONUS " + "\n");
                        strSqlString.Append("             , 0 AS TRANS " + "\n");
                        strSqlString.Append("             , 0 AS EOH " + "\n");
                        strSqlString.Append("             , 0 AS K4EOH " + "\n");
                        strSqlString.Append("          FROM DUAL A " + "\n");
                    }
                    strSqlString.Append("        UNION " + "\n");
                    /************************************************************************************************************/
                    /* INTEST(ISSUE_HMK3_HMKT) : HMK3 ISSUE(MES의 T0000공정의 END 수량) 수량을 조회한다.                        */
                    strSqlString.Append("        SELECT A.MAT_ID AS MAT_ID " + "\n");
                    strSqlString.Append("             , CASE A.LOT_CMF_15 WHEN ' ' THEN '**' " + "\n");
                    strSqlString.Append("                                 ELSE A.LOT_CMF_15 " + "\n");
                    strSqlString.Append("               END AS RETURN_TYPE " + "\n");
                    strSqlString.Append("             , 0 AS BOH " + "\n");
                    strSqlString.Append("             , 0 AS INASSY " + "\n");
                    strSqlString.Append("             , SUM(A.QTY_1) AS INTEST " + "\n");
                    strSqlString.Append("             , 0 AS ASYTST " + "\n");
                    strSqlString.Append("             , 0 AS OUTASSY " + "\n");
                    strSqlString.Append("             , 0 AS OUTTEST " + "\n");
                    strSqlString.Append("             , 0 AS RINASSY " + "\n");
                    strSqlString.Append("             , 0 AS ROUTASSY " + "\n");
                    strSqlString.Append("             , 0 AS ROUTTEST " + "\n");
                    strSqlString.Append("             , 0 AS BONUS " + "\n");
                    strSqlString.Append("             , 0 AS TRANS " + "\n");
                    strSqlString.Append("             , 0 AS EOH " + "\n");
                    strSqlString.Append("             , 0 AS K4EOH " + "\n");
                    strSqlString.Append("          FROM RWIPLOTHIS A " + "\n");
                    strSqlString.Append("         WHERE A.FACTORY = '" + GlobalVariable.gsTestDefaultFactory + "' " + "\n");
                    strSqlString.Append("           AND A.OLD_OPER = 'T0000' " + "\n");
                    strSqlString.Append("           AND A.TRAN_CMF_1 = 'ISSUE' " + "\n");
                    strSqlString.Append("           AND A.LOT_TYPE = 'W' " + "\n");
                    strSqlString.Append("           AND A.OWNER_CODE = 'PROD' " + "\n");
                    strSqlString.AppendFormat("           AND A.LOT_CMF_5 LIKE '{0}%' " + "\n", txtLotType.Text.Trim());
                    strSqlString.AppendFormat("           AND A.TRAN_TIME BETWEEN '{0}' AND '{1}'" + "\n", strFromDate, strToDate);
                    strSqlString.AppendFormat("           AND A.MAT_ID LIKE '{0}%' " + "\n", txtSearchProduct.Text.Trim());
                    // 조립Site(HM = 자사조립) 
                    if (rbtSite01.Checked == true)
                    {
                        strSqlString.AppendFormat("           AND TRIM(A.LOT_CMF_7) = 'HM' " + "\n");
                    }
                    else
                    {
                        strSqlString.AppendFormat("           AND TRIM(A.LOT_CMF_7) <> 'HM' " + "\n");
                    }
                    strSqlString.Append("           AND A.HIST_DEL_FLAG <> 'Y' " + "\n");
                    strSqlString.Append("         GROUP BY A.MAT_ID, A.LOT_CMF_15 " + "\n");
                    strSqlString.Append("        UNION " + "\n");
                    // 조립Site(HM = 자사조립) 
                    if (rbtSite01.Checked == true)
                    {
                        /************************************************************************************************************/
                        /* OUTASSY(SHIP_HMK3_XX) : HMK3 STOCKS에서 출하된 수량을 조회한다.(SHIP_HMKA ACTIVITY는 존재하지 않는다.)   */
                        strSqlString.Append("        SELECT A.MAT_ID AS MAT_ID " + "\n");
                        strSqlString.Append("             , CASE A.LOT_CMF_15 WHEN ' ' THEN '**' " + "\n");
                        strSqlString.Append("                                 ELSE A.LOT_CMF_15 " + "\n");
                        strSqlString.Append("               END AS RETURN_TYPE " + "\n");
                        strSqlString.Append("             , 0 AS BOH " + "\n");
                        strSqlString.Append("             , 0 AS INASSY " + "\n");
                        strSqlString.Append("             , 0 AS INTEST " + "\n");
                        strSqlString.Append("             , 0 AS ASYTST " + "\n");
                        strSqlString.Append("             , SUM(A.QTY_1) AS OUTASSY " + "\n");
                        strSqlString.Append("             , 0 AS OUTTEST " + "\n");
                        strSqlString.Append("             , 0 AS RINASSY " + "\n");
                        strSqlString.Append("             , 0 AS ROUTASSY " + "\n");
                        strSqlString.Append("             , 0 AS ROUTTEST " + "\n");
                        strSqlString.Append("             , 0 AS BONUS " + "\n");
                        strSqlString.Append("             , 0 AS TRANS " + "\n");
                        strSqlString.Append("             , 0 AS EOH " + "\n");
                        strSqlString.Append("             , 0 AS K4EOH " + "\n");
                        strSqlString.Append("          FROM RWIPLOTHIS A " + "\n");
                        strSqlString.Append("         WHERE A.FACTORY IN ('" + GlobalVariable.gsTestDefaultFactory + "', 'CUSTOMER') " + "\n");
                        strSqlString.Append("           AND A.OLD_FACTORY = '" + GlobalVariable.gsAssyDefaultFactory + "' " + "\n");
                        strSqlString.Append("           AND A.OPER = ' ' " + "\n");
                        strSqlString.Append("           AND A.OLD_OPER = 'AZ010' " + "\n");
                        strSqlString.Append("           AND A.TRAN_CODE = 'SHIP' " + "\n");
                        strSqlString.Append("           AND A.LOT_TYPE = 'W' " + "\n");
                        strSqlString.Append("           AND A.OWNER_CODE = 'PROD' " + "\n");
                        strSqlString.AppendFormat("           AND A.LOT_CMF_5 LIKE '{0}%' " + "\n", txtLotType.Text.Trim());
                        strSqlString.AppendFormat("           AND A.TRAN_TIME BETWEEN '{0}' AND '{1}'" + "\n", strFromDate, strToDate);
                        strSqlString.AppendFormat("           AND A.MAT_ID LIKE '{0}%' " + "\n", txtSearchProduct.Text.Trim());
                        strSqlString.Append("           AND A.HIST_DEL_FLAG <> 'Y' " + "\n");
                        strSqlString.Append("         GROUP BY A.MAT_ID, A.LOT_CMF_15 " + "\n");
                    }
                    else
                    {
                        strSqlString.Append("        SELECT ' ' AS MAT_ID " + "\n");
                        strSqlString.Append("             , ' ' AS RETURN_TYPE " + "\n");
                        strSqlString.Append("             , 0 AS BOH " + "\n");
                        strSqlString.Append("             , 0 AS INASSY " + "\n");
                        strSqlString.Append("             , 0 AS INTEST " + "\n");
                        strSqlString.Append("             , 0 AS ASYTST " + "\n");
                        strSqlString.Append("             , 0 AS OUTASSY " + "\n");
                        strSqlString.Append("             , 0 AS OUTTEST " + "\n");
                        strSqlString.Append("             , 0 AS RINASSY " + "\n");
                        strSqlString.Append("             , 0 AS ROUTASSY " + "\n");
                        strSqlString.Append("             , 0 AS ROUTTEST " + "\n");
                        strSqlString.Append("             , 0 AS BONUS " + "\n");
                        strSqlString.Append("             , 0 AS TRANS " + "\n");
                        strSqlString.Append("             , 0 AS EOH " + "\n");
                        strSqlString.Append("             , 0 AS K4EOH " + "\n");
                        strSqlString.Append("          FROM DUAL A " + "\n");
                    }
                    strSqlString.Append("        UNION " + "\n");
                    /************************************************************************************************************/
                    /* OUTTEST(SHIP_HMK4_XX) : HMK4 STOCKS에서 출하된 수량을 조회한다.                                          */
                    strSqlString.Append("        SELECT A.MAT_ID AS MAT_ID " + "\n");
                    strSqlString.Append("             , A.RETURN_TYPE AS RETURN_TYPE " + "\n");
                    strSqlString.Append("             , 0 AS BOH " + "\n");
                    strSqlString.Append("             , 0 AS INASSY " + "\n");
                    strSqlString.Append("             , 0 AS INTEST " + "\n");
                    strSqlString.Append("             , 0 AS ASYTST " + "\n");
                    strSqlString.Append("             , 0 AS OUTASSY " + "\n");
                    strSqlString.Append("             , SUM(A.OUTTEST) AS OUTTEST " + "\n");
                    strSqlString.Append("             , 0 AS RINASSY " + "\n");
                    strSqlString.Append("             , 0 AS ROUTASSY " + "\n");
                    strSqlString.Append("             , 0 AS ROUTTEST " + "\n");
                    strSqlString.Append("             , 0 AS BONUS " + "\n");
                    strSqlString.Append("             , 0 AS TRANS " + "\n");
                    strSqlString.Append("             , 0 AS EOH " + "\n");
                    strSqlString.Append("             , 0 AS K4EOH " + "\n");
                    strSqlString.Append("          FROM ( " + "\n");
                    /* CUSTOMER : LOT이 최종으로 출하된 수량을 조회한다.                                                        */
                    strSqlString.Append("                SELECT A.MAT_ID AS MAT_ID " + "\n");
                    strSqlString.Append("                     , CASE A.LOT_CMF_15 WHEN ' ' THEN '**' " + "\n");
                    strSqlString.Append("                                         ELSE A.LOT_CMF_15 " + "\n");
                    strSqlString.Append("                       END AS RETURN_TYPE " + "\n");
                    strSqlString.Append("                     , SUM(A.QTY_1) AS OUTTEST " + "\n");
                    strSqlString.Append("                  FROM RWIPLOTHIS A " + "\n");
                    strSqlString.Append("                 WHERE A.FACTORY = 'CUSTOMER' " + "\n");
                    strSqlString.Append("                   AND A.OLD_FACTORY = '" + GlobalVariable.gsTestDefaultFactory + "' " + "\n");
                    strSqlString.Append("                   AND A.OPER = ' ' " + "\n");
                    strSqlString.Append("                   AND A.OLD_OPER = 'TZ010' " + "\n");
                    strSqlString.Append("                   AND A.TRAN_CODE = 'SHIP' " + "\n");
                    strSqlString.Append("                   AND A.LOT_TYPE = 'W' " + "\n");
                    strSqlString.Append("                   AND A.OWNER_CODE = 'PROD' " + "\n");
                    strSqlString.AppendFormat("                   AND A.LOT_CMF_5 LIKE '{0}%' " + "\n", txtLotType.Text.Trim());
                    strSqlString.AppendFormat("                   AND A.TRAN_TIME BETWEEN '{0}' AND '{1}'" + "\n", strFromDate, strToDate);
                    strSqlString.AppendFormat("                   AND A.MAT_ID LIKE '{0}%' " + "\n", txtSearchProduct.Text.Trim());
                    // 조립Site(HM = 자사조립)
                    if (rbtSite01.Checked == true)
                    {
                        strSqlString.AppendFormat("                   AND TRIM(A.LOT_CMF_7) = 'HM' " + "\n");
                    }
                    else
                    {
                        strSqlString.AppendFormat("                   AND TRIM(A.LOT_CMF_7) <> 'HM' " + "\n");
                    }
                    strSqlString.Append("                   AND A.HIST_DEL_FLAG <> 'Y' " + "\n");
                    strSqlString.Append("                 GROUP BY A.MAT_ID, A.LOT_CMF_15 " + "\n");
                    strSqlString.Append("                UNION " + "\n");
                    /* HMKT1 : 파샬출하된 LOT의 수량을 조회한다.                                                                */
                    strSqlString.Append("                SELECT A.MAT_ID AS MAT_ID " + "\n");
                    strSqlString.Append("                     , CASE A.LOT_CMF_15 WHEN ' ' THEN '**' " + "\n");
                    strSqlString.Append("                                         ELSE A.LOT_CMF_15 " + "\n");
                    strSqlString.Append("                       END AS RETURN_TYPE " + "\n");
                    strSqlString.Append("                     , SUM(A.OLD_QTY_1 - A.QTY_1) AS OUTTEST " + "\n");
                    strSqlString.Append("                  FROM RWIPLOTHIS A " + "\n");
                    strSqlString.Append("                 WHERE A.FACTORY = '" + GlobalVariable.gsTestDefaultFactory + "' " + "\n");
                    strSqlString.Append("                   AND A.OLD_FACTORY = '" + GlobalVariable.gsTestDefaultFactory + "' " + "\n");
                    strSqlString.Append("                   AND A.OPER = 'TZ010' " + "\n");
                    strSqlString.Append("                   AND A.OLD_OPER = 'TZ010' " + "\n");
                    strSqlString.Append("                   AND A.TRAN_CODE = 'SHIP' " + "\n");
                    strSqlString.Append("                   AND A.LOT_TYPE = 'W' " + "\n");
                    strSqlString.Append("                   AND A.OWNER_CODE = 'PROD' " + "\n");
                    strSqlString.AppendFormat("                   AND A.LOT_CMF_5 LIKE '{0}%' " + "\n", txtLotType.Text.Trim());
                    strSqlString.AppendFormat("                   AND A.TRAN_TIME BETWEEN '{0}' AND '{1}'" + "\n", strFromDate, strToDate);
                    strSqlString.AppendFormat("                   AND A.MAT_ID LIKE '{0}%' " + "\n", txtSearchProduct.Text.Trim());
                    // 조립Site(HM = 자사조립)
                    if (rbtSite01.Checked == true)
                    {
                        strSqlString.AppendFormat("                   AND TRIM(A.LOT_CMF_7) = 'HM' " + "\n");
                    }
                    else
                    {
                        strSqlString.AppendFormat("                   AND TRIM(A.LOT_CMF_7) <> 'HM' " + "\n");
                    }
                    strSqlString.Append("                   AND A.HIST_DEL_FLAG <> 'Y' " + "\n");
                    strSqlString.Append("                 GROUP BY A.MAT_ID, A.LOT_CMF_15 " + "\n");
                    strSqlString.Append("             ) A " + "\n");
                    strSqlString.Append("         GROUP BY A.MAT_ID, A.RETURN_TYPE " + "\n");
                    strSqlString.Append("        UNION " + "\n");
                    /************************************************************************************************************/
                    /* RINASSY(RETURN_HMK2_XX) : HMK2 STOCKS에서 반품된 수량을 조회한다.                                        */
                    strSqlString.Append("        SELECT A.MAT_ID AS MAT_ID " + "\n");
                    strSqlString.Append("             , CASE A.LOT_CMF_15 WHEN ' ' THEN '**' " + "\n");
                    strSqlString.Append("                                 ELSE A.LOT_CMF_15 " + "\n");
                    strSqlString.Append("               END AS RETURN_TYPE " + "\n");
                    strSqlString.Append("             , 0 AS BOH " + "\n");
                    strSqlString.Append("             , 0 AS INASSY " + "\n");
                    strSqlString.Append("             , 0 AS INTEST " + "\n");
                    strSqlString.Append("             , 0 AS ASYTST " + "\n");
                    strSqlString.Append("             , 0 AS OUTASSY " + "\n");
                    strSqlString.Append("             , 0 AS OUTTEST " + "\n");
                    strSqlString.Append("             , SUM(A.QTY_1) AS RINASSY " + "\n");
                    strSqlString.Append("             , 0 AS ROUTASSY " + "\n");
                    strSqlString.Append("             , 0 AS ROUTTEST " + "\n");
                    strSqlString.Append("             , 0 AS BONUS " + "\n");
                    strSqlString.Append("             , 0 AS TRANS " + "\n");
                    strSqlString.Append("             , 0 AS EOH " + "\n");
                    strSqlString.Append("             , 0 AS K4EOH " + "\n");
                    strSqlString.Append("          FROM RWIPLOTHIS A " + "\n");
                    strSqlString.Append("         WHERE A.FACTORY = 'RETURN' " + "\n");
                    strSqlString.Append("           AND A.OLD_FACTORY = '" + GlobalVariable.gsAssyDefaultFactory + "' " + "\n");
                    strSqlString.Append("           AND A.OPER = ' ' " + "\n");
                    strSqlString.Append("           AND A.OLD_OPER = 'A0000' " + "\n");
                    strSqlString.Append("           AND A.LOT_TYPE = 'W' " + "\n");
                    strSqlString.Append("           AND A.OWNER_CODE = 'PROD' " + "\n");
                    strSqlString.AppendFormat("           AND A.LOT_CMF_5 LIKE '{0}%' " + "\n", txtLotType.Text.Trim());
                    strSqlString.AppendFormat("           AND A.TRAN_TIME BETWEEN '{0}' AND '{1}'" + "\n", strFromDate, strToDate);
                    strSqlString.AppendFormat("           AND A.MAT_ID LIKE '{0}%' " + "\n", txtSearchProduct.Text.Trim());
                    //// 2009-03-23-정비재 : RETURN된 자재는 ASSY SITE를 검색조건에 넣지 않는다.
                    // 2009-05-08-정비재 : RETURN된 자재도 ASSY SITE를 구분한다.(이진영 대리 요청)
                    //// 조립Site(HM = 자사조립)
                    //if (rbtSite01.Checked == true)
                    //{
                    //    strSqlString.AppendFormat("           AND TRIM(A.LOT_CMF_7) = 'HM' " + "\n");
                    //}
                    //else
                    //{
                    //    strSqlString.AppendFormat("           AND TRIM(A.LOT_CMF_7) <> 'HM' " + "\n");
                    //}
                    strSqlString.Append("           AND A.HIST_DEL_FLAG <> 'Y' " + "\n");
                    strSqlString.Append("         GROUP BY A.MAT_ID, A.LOT_CMF_15 " + "\n");
                    strSqlString.Append("        UNION " + "\n");
                    /************************************************************************************************************/
                    /* ROUTASSY(RETURN_XX_HMK3 - RETURN_HMK4_HMK3) : HMK3 STOCKS에 반품되어 들어온 수량을 조회한다.             */
                    strSqlString.Append("        SELECT A.MAT_ID AS MAT_ID " + "\n");
                    strSqlString.Append("             , A.RETURN_TYPE AS RETURN_TYPE " + "\n");
                    strSqlString.Append("             , 0 AS BOH " + "\n");
                    strSqlString.Append("             , 0 AS INASSY " + "\n");
                    strSqlString.Append("             , 0 AS INTEST " + "\n");
                    strSqlString.Append("             , 0 AS ASYTST " + "\n");
                    strSqlString.Append("             , 0 AS OUTASSY " + "\n");
                    strSqlString.Append("             , 0 AS OUTTEST " + "\n");
                    strSqlString.Append("             , 0 AS ROUTASSY " + "\n");
                    strSqlString.Append("             , SUM(A.ROUTASSY) AS ROUTASSY " + "\n");
                    strSqlString.Append("             , 0 AS ROUTTEST " + "\n");
                    strSqlString.Append("             , 0 AS BONUS " + "\n");
                    strSqlString.Append("             , 0 AS TRANS " + "\n");
                    strSqlString.Append("             , 0 AS EOH " + "\n");
                    strSqlString.Append("             , 0 AS K4EOH " + "\n");
                    strSqlString.Append("          FROM ( " + "\n");
                    strSqlString.Append("                SELECT A.MAT_ID AS MAT_ID " + "\n");
                    strSqlString.Append("                     , CASE A.LOT_CMF_15 WHEN ' ' THEN '**' " + "\n");
                    strSqlString.Append("                                         ELSE A.LOT_CMF_15 " + "\n");
                    strSqlString.Append("                       END AS RETURN_TYPE " + "\n");
                    strSqlString.Append("                     , SUM(A.QTY_1) AS ROUTASSY " + "\n");
                    strSqlString.Append("                  FROM RWIPLOTHIS A " + "\n");
                    strSqlString.Append("                 WHERE A.FACTORY = '" + GlobalVariable.gsAssyDefaultFactory + "' " + "\n");
                    strSqlString.Append("                   AND A.OLD_FACTORY = '" + GlobalVariable.gsAssyDefaultFactory + "' " + "\n");
                    strSqlString.Append("                   AND A.OPER = 'AZ010' " + "\n");
                    strSqlString.Append("                   AND A.OLD_OPER = 'AZ010' " + "\n");
                    strSqlString.Append("                   AND A.TRAN_CODE = 'CREATE' " + "\n");
                    strSqlString.Append("                   AND A.LOT_TYPE = 'W' " + "\n");
                    strSqlString.Append("                   AND A.OWNER_CODE = 'PROD' " + "\n");
                    strSqlString.AppendFormat("                   AND A.LOT_CMF_5 LIKE '{0}%' " + "\n", txtLotType.Text.Trim());
                    strSqlString.AppendFormat("                   AND A.TRAN_TIME BETWEEN '{0}' AND '{1}'" + "\n", strFromDate, strToDate);
                    strSqlString.AppendFormat("                   AND A.MAT_ID LIKE '{0}%' " + "\n", txtSearchProduct.Text.Trim());
                    // 조립Site(HM = 자사조립)
                    // 2009-05-08-정비재 : RETURN된 자재도 ASSY SITE를 구분한다.(이진영 대리 요청)
                    //if (rbtSite01.Checked == true)
                    //{
                    //    strSqlString.AppendFormat("                   AND TRIM(A.LOT_CMF_7) = 'HM' " + "\n");
                    //}
                    //else
                    //{
                    //    strSqlString.AppendFormat("                   AND TRIM(A.LOT_CMF_7) <> 'HM' " + "\n");
                    //}
                    strSqlString.Append("                   AND A.HIST_DEL_FLAG <> 'Y' " + "\n");
                    strSqlString.Append("                 GROUP BY A.MAT_ID, A.LOT_CMF_15 " + "\n");
                    strSqlString.Append("                UNION " + "\n");
                    strSqlString.Append("                SELECT A.MAT_ID AS MAT_ID " + "\n");
                    strSqlString.Append("                     , CASE A.LOT_CMF_15 WHEN ' ' THEN '**' " + "\n");
                    strSqlString.Append("                                         ELSE A.LOT_CMF_15 " + "\n");
                    strSqlString.Append("                       END AS RETURN_TYPE " + "\n");
                    strSqlString.Append("                     , -SUM(A.QTY_1) AS ROUTASSY " + "\n");
                    strSqlString.Append("                  FROM RWIPLOTHIS A " + "\n");
                    strSqlString.Append("                 WHERE A.FACTORY = '" + GlobalVariable.gsAssyDefaultFactory + "' " + "\n");
                    strSqlString.Append("                   AND A.OPER IN ('AZ010', 'AZ009', 'T0000') " + "\n");
                    strSqlString.Append("                   AND A.OLD_FACTORY = '" + GlobalVariable.gsTestDefaultFactory + "' " + "\n");
                    strSqlString.Append("                   AND A.OLD_OPER = 'TZ010' " + "\n");
                    strSqlString.Append("                   AND A.LOT_TYPE = 'W' " + "\n");
                    strSqlString.Append("                   AND A.OWNER_CODE = 'PROD' " + "\n");
                    strSqlString.AppendFormat("                   AND A.LOT_CMF_5 LIKE '{0}%' " + "\n", txtLotType.Text.Trim());
                    strSqlString.AppendFormat("                   AND A.TRAN_TIME BETWEEN '{0}' AND '{1}'" + "\n", strFromDate, strToDate);
                    strSqlString.AppendFormat("                   AND A.MAT_ID LIKE '{0}%' " + "\n", txtSearchProduct.Text.Trim());
                    //// 조립Site(HM = 자사조립)
                    //// 2009-03-23-정비재 : RETURN된 자재는 ASSY SITE를 검색조건에 넣지 않는다.
                    // 2009-05-08-정비재 : RETURN된 자재도 ASSY SITE를 구분한다.(이진영 대리 요청)
                    //if (rbtSite01.Checked == true)
                    //{
                    //    strSqlString.AppendFormat("                   AND TRIM(A.LOT_CMF_7) = 'HM' " + "\n");
                    //}
                    //else
                    //{
                    //    strSqlString.AppendFormat("                   AND TRIM(A.LOT_CMF_7) <> 'HM' " + "\n");
                    //}
                    strSqlString.Append("                   AND A.HIST_DEL_FLAG <> 'Y' " + "\n");
                    strSqlString.Append("                 GROUP BY A.MAT_ID, A.LOT_CMF_15 " + "\n");
                    strSqlString.Append("               ) A" + "\n");
                    strSqlString.Append("         GROUP BY A.MAT_ID, A.RETURN_TYPE " + "\n");
                    strSqlString.Append("        UNION " + "\n");
                    /************************************************************************************************************/
                    /* ROUTTEST(RETURN_XX_HMK4) : HMK4 STOCKS에 반품되어 들어온 수량을 조회한다.                                */
                    strSqlString.Append("        SELECT A.MAT_ID AS MAT_ID " + "\n");
                    strSqlString.Append("             , CASE A.LOT_CMF_15 WHEN ' ' THEN '**' " + "\n");
                    strSqlString.Append("                                 ELSE A.LOT_CMF_15 " + "\n");
                    strSqlString.Append("               END AS RETURN_TYPE " + "\n");
                    strSqlString.Append("             , 0 AS BOH " + "\n");
                    strSqlString.Append("             , 0 AS INASSY " + "\n");
                    strSqlString.Append("             , 0 AS INTEST " + "\n");
                    strSqlString.Append("             , 0 AS ASYTST " + "\n");
                    strSqlString.Append("             , 0 AS OUTASSY " + "\n");
                    strSqlString.Append("             , 0 AS OUTTEST " + "\n");
                    strSqlString.Append("             , 0 AS RINASSY " + "\n");
                    strSqlString.Append("             , 0 AS ROUTASSY " + "\n");
                    strSqlString.Append("             , SUM(A.QTY_1) AS ROUTTEST " + "\n");
                    strSqlString.Append("             , 0 AS BONUS " + "\n");
                    strSqlString.Append("             , 0 AS TRANS " + "\n");
                    strSqlString.Append("             , 0 AS EOH " + "\n");
                    strSqlString.Append("             , 0 AS K4EOH " + "\n");
                    strSqlString.Append("          FROM RWIPLOTHIS A " + "\n");
                    strSqlString.Append("         WHERE A.FACTORY = '" + GlobalVariable.gsTestDefaultFactory + "' " + "\n");
                    strSqlString.Append("           AND A.OLD_FACTORY = '" + GlobalVariable.gsTestDefaultFactory + "' " + "\n");
                    strSqlString.Append("           AND A.OPER = 'TZ010' " + "\n");
                    strSqlString.Append("           AND A.OLD_OPER = 'TZ010' " + "\n");
                    strSqlString.Append("           AND A.TRAN_CODE = 'CREATE' " + "\n");
                    strSqlString.Append("           AND A.LOT_TYPE = 'W' " + "\n");
                    strSqlString.Append("           AND A.OWNER_CODE = 'PROD' " + "\n");
                    strSqlString.AppendFormat("           AND A.LOT_CMF_5 LIKE '{0}%' " + "\n", txtLotType.Text.Trim());
                    strSqlString.AppendFormat("           AND A.TRAN_TIME BETWEEN '{0}' AND '{1}'" + "\n", strFromDate, strToDate);
                    strSqlString.AppendFormat("           AND A.MAT_ID LIKE '{0}%' " + "\n", txtSearchProduct.Text.Trim());
                    // 조립Site(HM = 자사조립)
                    if (rbtSite01.Checked == true)
                    {
                        strSqlString.Append("           AND TRIM(A.LOT_CMF_7) = 'HM' " + "\n");
                    }
                    else
                    {
                        strSqlString.Append("           AND TRIM(A.LOT_CMF_7) <> 'HM' " + "\n");
                    }
                    strSqlString.Append("           AND A.HIST_DEL_FLAG <> 'Y' " + "\n");
                    strSqlString.Append("         GROUP BY A.MAT_ID, A.LOT_CMF_15 " + "\n");
                    strSqlString.Append("        UNION " + "\n");
                    /************************************************************************************************************/
                    /* BONUS(BONUS + ABNORMALSTART) : 전체공정에서 BONUS + ABNORMALSTART EVENT가 발생된 수량을 조회한다.        */
                    /* BONUS : 실제 BONUS 수량을 조회한다.(HALF CHIP 는 실제 BONUS, REWORK에서 제외한다)                        */
                    strSqlString.Append("        SELECT A.MAT_ID AS MAT_ID " + "\n");
                    strSqlString.Append("             , CASE B.LOT_CMF_15 WHEN ' ' THEN '**' " + "\n");
                    strSqlString.Append("                                 ELSE B.LOT_CMF_15 " + "\n");
                    strSqlString.Append("               END AS RETURN_TYPE " + "\n");
                    strSqlString.Append("             , 0 AS BOH " + "\n");
                    strSqlString.Append("             , 0 AS INASSY " + "\n");
                    strSqlString.Append("             , 0 AS INTEST " + "\n");
                    strSqlString.Append("             , 0 AS ASYTST " + "\n");
                    strSqlString.Append("             , 0 AS OUTASSY " + "\n");
                    strSqlString.Append("             , 0 AS OUTTEST " + "\n");
                    strSqlString.Append("             , 0 AS ROUTASSY " + "\n");
                    strSqlString.Append("             , 0 AS ROUTASSY " + "\n");
                    strSqlString.Append("             , 0 AS ROUTTEST " + "\n");
                    strSqlString.Append("             , SUM(A.TOTAL_BONUS_QTY) AS BONUS " + "\n");
                    strSqlString.Append("             , 0 AS TRANS " + "\n");
                    strSqlString.Append("             , 0 AS EOH " + "\n");
                    strSqlString.Append("             , 0 AS K4EOH " + "\n");
                    strSqlString.Append("          FROM RWIPLOTBNS A, RWIPLOTSTS B " + "\n");
                    strSqlString.Append("         WHERE A.FACTORY = B.FACTORY " + "\n");
                    strSqlString.Append("           AND A.MAT_ID = B.MAT_ID " + "\n");
                    strSqlString.Append("           AND A.LOT_ID = B.LOT_ID " + "\n");
                    strSqlString.Append("           AND B.LOT_TYPE = 'W' " + "\n");
                    strSqlString.Append("           AND B.OWNER_CODE = 'PROD' " + "\n");
                    strSqlString.Append("           AND B.LOT_CMF_5 LIKE 'P%' " + "\n");
                    strSqlString.Append("           AND A.FACTORY IN ('" + GlobalVariable.gsAssyDefaultFactory + "', '" + GlobalVariable.gsTestDefaultFactory + "') " + "\n");
                    strSqlString.AppendFormat("           AND A.TRAN_TIME BETWEEN '{0}' AND '{1}'" + "\n", strFromDate, strToDate);
                    strSqlString.AppendFormat("           AND A.MAT_ID LIKE '{0}%' " + "\n", txtSearchProduct.Text.Trim());
                    // 조립Site(HM = 자사조립)
                    if (rbtSite01.Checked == true)
                    {
                        strSqlString.Append("           AND B.LOT_CMF_7 = 'HM' " + "\n");
                    }
                    else
                    {
                        strSqlString.Append("           AND B.LOT_CMF_7 <> 'HM' " + "\n");
                    }
                    strSqlString.Append("         GROUP BY A.MAT_ID, B.LOT_CMF_15 " + "\n");
                    strSqlString.Append("        UNION " + "\n");
                    /************************************************************************************************************/
                    /* TRANSFER : HMK2, HMK3, HMK4 에서 TERMINATE된 수량을 조회한다.                                            */
                    strSqlString.Append("        SELECT A.MAT_ID AS MAT_ID " + "\n");
                    strSqlString.Append("             , CASE A.LOT_CMF_15 WHEN ' ' THEN '**' " + "\n");
                    strSqlString.Append("                                 ELSE A.LOT_CMF_15 " + "\n");
                    strSqlString.Append("               END AS RETURN_TYPE " + "\n");
                    strSqlString.Append("             , 0 AS BOH " + "\n");
                    strSqlString.Append("             , 0 AS INASSY " + "\n");
                    strSqlString.Append("             , 0 AS INTEST " + "\n");
                    strSqlString.Append("             , 0 AS ASYTST " + "\n");
                    strSqlString.Append("             , 0 AS OUTASSY " + "\n");
                    strSqlString.Append("             , 0 AS OUTTEST " + "\n");
                    strSqlString.Append("             , 0 AS RINASSY " + "\n");
                    strSqlString.Append("             , 0 AS ROUTASSY " + "\n");
                    strSqlString.Append("             , 0 AS ROUTTEST " + "\n");
                    strSqlString.Append("             , 0 AS BONUS " + "\n");
                    strSqlString.Append("             , SUM(A.QTY_1) AS TRANS " + "\n");
                    strSqlString.Append("             , 0 AS EOH " + "\n");
                    strSqlString.Append("             , 0 AS K4EOH " + "\n");
                    strSqlString.Append("          FROM RWIPLOTHIS A " + "\n");
                    strSqlString.Append("         WHERE A.FACTORY = A.OLD_FACTORY " + "\n");
                    strSqlString.Append("           AND A.OPER = A.OLD_OPER " + "\n");
                    strSqlString.Append("           AND A.FACTORY IN ('" + GlobalVariable.gsAssyDefaultFactory + "', '" + GlobalVariable.gsTestDefaultFactory + "') " + "\n");
                    strSqlString.Append("           AND A.TRAN_CODE = 'TERMINATE' " + "\n");
                    strSqlString.Append("           AND A.LOT_TYPE = 'W' " + "\n");
                    strSqlString.Append("           AND A.OWNER_CODE = 'PROD' " + "\n");
                    strSqlString.AppendFormat("           AND A.LOT_CMF_5 LIKE '{0}%' " + "\n", txtLotType.Text.Trim());
                    strSqlString.AppendFormat("           AND A.TRAN_TIME BETWEEN '{0}' AND '{1}'" + "\n", strFromDate, strToDate);
                    strSqlString.AppendFormat("           AND A.MAT_ID LIKE '{0}%' " + "\n", txtSearchProduct.Text.Trim());
                    // 조립Site(HM = 자사조립)
                    if (rbtSite01.Checked == true)
                    {
                        strSqlString.Append("           AND A.LOT_CMF_7 = 'HM' " + "\n");
                    }
                    else
                    {
                        strSqlString.Append("           AND A.LOT_CMF_7 <> 'HM' " + "\n");
                    }
                    strSqlString.Append("           AND A.HIST_DEL_FLAG <> 'Y' " + "\n");
                    strSqlString.Append("         GROUP BY A.MAT_ID, A.LOT_CMF_15 " + "\n");
                    strSqlString.Append("        UNION " + "\n");
                    /********************************************************************************************************/
                    /* EOH : 각 공정별 EOH수량을 조회한다.                                                                  */
                    strSqlString.Append("        SELECT A.MAT_ID AS MAT_ID " + "\n");
                    strSqlString.Append("             , A.RETURN_TYPE AS RETURN_TYPE " + "\n");
                    strSqlString.Append("             , 0 AS BOH " + "\n");
                    strSqlString.Append("             , 0 AS INASSY " + "\n");
                    strSqlString.Append("             , 0 AS INTEST " + "\n");
                    strSqlString.Append("             , 0 AS ASYTST " + "\n");
                    strSqlString.Append("             , 0 AS OUTASSY " + "\n");
                    strSqlString.Append("             , 0 AS OUTTEST " + "\n");
                    strSqlString.Append("             , 0 AS RINASSY " + "\n");
                    strSqlString.Append("             , 0 AS ROUTASSY " + "\n");
                    strSqlString.Append("             , 0 AS ROUTTEST " + "\n");
                    strSqlString.Append("             , 0 AS BONUS " + "\n");
                    strSqlString.Append("             , 0 AS TRANS " + "\n");
                    strSqlString.Append("             , SUM(A.EOH) AS EOH " + "\n");
                    strSqlString.Append("             , 0 AS K4EOH " + "\n");
                    strSqlString.Append("          FROM ( " + "\n");
                    /* EOH : HMK2의 ASSY SITE가 않맞는 문제로 HMK2 EOH를 별도로 계산한다.(HMK2는 ASSY SITE를 구분하지 않는다.)  */
                    strSqlString.Append("                SELECT A.MAT_ID AS MAT_ID " + "\n");
                    strSqlString.Append("                     , CASE A.LOT_CMF_15 WHEN ' ' THEN '**' " + "\n");
                    strSqlString.Append("                                         ELSE A.LOT_CMF_15 " + "\n");
                    strSqlString.Append("                       END AS RETURN_TYPE " + "\n");
                    strSqlString.Append("                     , SUM(A.QTY_1) AS EOH " + "\n");
                    // EOH구분(정상 = RWIPLOTSTS_BOH, 금일 = RWIPLOTSTS)
                    if (rbtEoh01.Checked == true)
                    {
                        strSqlString.Append("                  FROM RWIPLOTSTS_BOH A " + "\n");
                    }
                    else
                    {
                        strSqlString.Append("                  FROM RWIPLOTSTS A " + "\n");
                    }
                    strSqlString.Append("                 WHERE A.FACTORY = '" + GlobalVariable.gsAssyDefaultFactory + "' " + "\n");
                    strSqlString.Append("                   AND A.OPER = 'A0000' " + "\n");
                    strSqlString.Append("                   AND A.LOT_TYPE = 'W' " + "\n");
                    strSqlString.Append("                   AND A.OWNER_CODE = 'PROD' " + "\n");
                    // EOH구분(정상 = RWIPLOTSTS_BOH, 금일 = RWIPLOTSTS)
                    if (rbtEoh01.Checked == true)
                    {
                        strSqlString.AppendFormat("                   AND A.CUTOFF_DT = '{0}' " + "\n", strToCutOffDate);
                    }
                    else
                    {
                        // 금일의 EOH는 RWIPLOTSTS Table의 값을 조회한다.
                        strSqlString.AppendFormat("                   AND A.LOT_DEL_FLAG <> 'Y' " + "\n");
                    }
                    strSqlString.AppendFormat("                   AND A.LOT_CMF_5 LIKE '{0}%' " + "\n", txtLotType.Text.Trim());
                    strSqlString.AppendFormat("                   AND A.MAT_ID LIKE '{0}%' " + "\n", txtSearchProduct.Text.Trim());
                    //// 조립Site(HM = 자사조립)
                    // 2009-05-08-정비재 : RETURN된 자재도 ASSY SITE를 구분한다.(이진영 대리 요청)
                    //if (rbtSite01.Checked == true)
                    //{
                    //    strSqlString.Append("                   AND TRIM(A.LOT_CMF_7) = 'HM' " + "\n");
                    //}
                    //else
                    //{
                    //    strSqlString.Append("                   AND TRIM(A.LOT_CMF_7) <> 'HM' " + "\n");
                    //}
                    strSqlString.Append("                 GROUP BY A.MAT_ID, A.LOT_CMF_15 " + "\n");
                    strSqlString.Append("                UNION " + "\n");
                    /************************************************************************************************************/
                    /* BOH : HMK2를 제외한 공정의 BOH를 계산한다.                                                               */
                    strSqlString.Append("                SELECT A.MAT_ID AS MAT_ID " + "\n");
                    strSqlString.Append("                     , CASE A.LOT_CMF_15 WHEN ' ' THEN '**' " + "\n");
                    strSqlString.Append("                                         ELSE A.LOT_CMF_15 " + "\n");
                    strSqlString.Append("                       END AS RETURN_TYPE " + "\n");
                    strSqlString.Append("                     , SUM(A.QTY_1) AS EOH " + "\n");
                    // EOH구분(정상 = RWIPLOTSTS_BOH, 금일 = RWIPLOTSTS)
                    if (rbtEoh01.Checked == true)
                    {
                        strSqlString.Append("                  FROM RWIPLOTSTS_BOH A " + "\n");
                    }
                    else
                    {
                        strSqlString.Append("                  FROM RWIPLOTSTS A " + "\n");
                    }
                    strSqlString.Append("                 WHERE A.FACTORY IN ('" + GlobalVariable.gsAssyDefaultFactory + "', '" + GlobalVariable.gsTestDefaultFactory + "') " + "\n");
                    strSqlString.Append("                   AND A.OPER > 'A0000' " + "\n");
                    strSqlString.Append("                   AND A.LOT_TYPE = 'W' " + "\n");
                    strSqlString.Append("                   AND A.OWNER_CODE = 'PROD' " + "\n");
                    // EOH구분(정상 = RWIPLOTSTS_BOH, 금일 = RWIPLOTSTS)
                    if (rbtEoh01.Checked == true)
                    {
                        strSqlString.AppendFormat("                   AND A.CUTOFF_DT = '{0}' " + "\n", strToCutOffDate);
                    }
                    else
                    {
                        // 금일의 EOH는 RWIPLOTSTS Table의 값을 조회한다.
                        strSqlString.AppendFormat("                   AND A.LOT_DEL_FLAG <> 'Y' " + "\n");
                    }
                    strSqlString.AppendFormat("                   AND A.LOT_CMF_5 LIKE '{0}%' " + "\n", txtLotType.Text.Trim());
                    strSqlString.AppendFormat("                   AND A.MAT_ID LIKE '{0}%' " + "\n", txtSearchProduct.Text.Trim());
                    // 조립Site(HM = 자사조립) 
                    // 2009-05-08-정비재 : RETURN된 자재도 ASSY SITE를 구분한다.(이진영 대리 요청)
                    //if (rbtSite01.Checked == true)
                    //{
                    //    strSqlString.AppendFormat("                   AND A.LOT_CMF_7 = 'HM' " + "\n");
                    //}
                    //else
                    //{
                    //    strSqlString.AppendFormat("                   AND A.LOT_CMF_7 <> 'HM' " + "\n");
                    //}
                    strSqlString.Append("                 GROUP BY A.MAT_ID, A.LOT_CMF_15 " + "\n");
                    strSqlString.Append("                ) A " + "\n");
                    strSqlString.Append("         GROUP BY A.MAT_ID, A.RETURN_TYPE " + "\n");                    
                    strSqlString.Append("       ) A " + "\n");
                    strSqlString.Append("     , ( " + "\n");
                    strSqlString.Append("        SELECT DISTINCT MAT_ID AS MAT_ID " + "\n");
                    strSqlString.Append("             , MAT_TYPE AS MAT_TYPE " + "\n");
                    strSqlString.Append("             , DELETE_FLAG AS DELETE_FLAG " + "\n");
                    strSqlString.Append("             , DECODE(MAT_GRP_1, ' ', '-', MAT_GRP_1) AS MAT_GRP_1 " + "\n");
                    strSqlString.Append("             , DECODE(MAT_GRP_2, ' ', '-', MAT_GRP_2) AS MAT_GRP_2 " + "\n");
                    strSqlString.Append("             , DECODE(MAT_GRP_3, ' ', '-', MAT_GRP_3) AS MAT_GRP_3 " + "\n");
                    strSqlString.Append("             , DECODE(MAT_GRP_4, ' ', '-', MAT_GRP_4) AS MAT_GRP_4 " + "\n");
                    strSqlString.Append("             , DECODE(MAT_GRP_5, ' ', '-', MAT_GRP_5) AS MAT_GRP_5 " + "\n");
                    strSqlString.Append("             , DECODE(MAT_GRP_6, ' ', '-', MAT_GRP_6) AS MAT_GRP_6 " + "\n");
                    strSqlString.Append("             , DECODE(MAT_GRP_7, ' ', '-', MAT_GRP_7) AS MAT_GRP_7 " + "\n");
                    strSqlString.Append("             , DECODE(MAT_GRP_8, ' ', '-', MAT_GRP_8) AS MAT_GRP_8 " + "\n");
                    strSqlString.Append("             , DECODE(MAT_CMF_10, ' ', '-', MAT_CMF_10) AS MAT_CMF_10 " + "\n");
                    strSqlString.Append("          FROM MWIPMATDEF " + "\n");
                    strSqlString.Append("         WHERE MAT_TYPE = 'FG' " + "\n");
                    strSqlString.Append("           AND DELETE_FLAG <> 'Y' " + "\n");
                    strSqlString.Append("       ) B " + "\n");
                    strSqlString.Append("     , ( " + "\n");
                    strSqlString.Append("        SELECT MAT_ID, SUM(PART_OUT) AS PART_OUT, SUM(PART_IN) AS PART_IN" + "\n");
                    strSqlString.Append("          FROM (" + "\n");
                    strSqlString.Append("                SELECT ORIGINAL_PART AS MAT_ID " + "\n");
                    strSqlString.Append("                     , - QTY_1 AS PART_OUT " + "\n");
                    strSqlString.Append("                     , 0 AS PART_IN " + "\n");
                    strSqlString.Append("                  FROM DT2 " + "\n");
                    strSqlString.Append("                 UNION ALL" + "\n");
                    strSqlString.Append("                SELECT CHANGE_PART AS MAT_ID " + "\n");
                    strSqlString.Append("                     , 0 AS PATR_OUT " + "\n");
                    strSqlString.Append("                     , QTY_1 AS PART_IN " + "\n");
                    strSqlString.Append("                  FROM DT2 " + "\n");
                    strSqlString.Append("               )" + "\n");
                    strSqlString.Append("         GROUP BY MAT_ID " + "\n");
                    strSqlString.Append("       ) C " + "\n");
                    strSqlString.Append("     , ( " + "\n");
                    strSqlString.Append("        SELECT MAT_ID , SUM(TRANS_QTY + LOSS_QTY) AS SUM_QTY " + "\n");
                    strSqlString.Append("          FROM (" + "\n");
                    strSqlString.Append("                SELECT A.MAT_ID " + "\n");
                    strSqlString.Append("                     , CASE WHEN A.OPER <= 'A0395' THEN A.TRANS_QTY " + "\n");
                    strSqlString.Append("                            WHEN B.MAT_GRP_5 = 'Merge' THEN A.TRANS_QTY" + "\n");
                    strSqlString.Append("                            ELSE A.TRANS_QTY * NVL(B.COMP_CNT,1) " + "\n");
                    strSqlString.Append("                       END TRANS_QTY " + "\n");
                    strSqlString.Append("                     , CASE WHEN A.OPER <= 'A0395' THEN A.LOSS_QTY " + "\n");
                    strSqlString.Append("                            WHEN B.MAT_GRP_5 = 'Merge' THEN A.LOSS_QTY" + "\n");
                    strSqlString.Append("                            ELSE A.LOSS_QTY * NVL(B.COMP_CNT,1) " + "\n");
                    strSqlString.Append("                       END LOSS_QTY  " + "\n");
                    strSqlString.Append("                  FROM (" + "\n");
                    strSqlString.Append("                        SELECT MAT_ID" + "\n");
                    strSqlString.Append("                             , OPER" + "\n");
                    strSqlString.Append("                             , SUM(QTY_1) AS TRANS_QTY  " + "\n");
                    strSqlString.Append("                             , 0 AS LOSS_QTY" + "\n");
                    strSqlString.Append("                          FROM RWIPLOTHIS  " + "\n");
                    strSqlString.Append("                         WHERE 1=1  " + "\n");
                    strSqlString.Append("                           AND OPER = OLD_OPER  " + "\n");                    
                    strSqlString.Append("                           AND TRAN_TIME BETWEEN '" + strFromDate + "' AND '" + strToDate + "' " + "\n");
                    strSqlString.Append("                           AND FACTORY = '" + GlobalVariable.gsAssyDefaultFactory + "'  " + "\n");
                    strSqlString.Append("                           AND TRAN_CODE = 'TERMINATE'  " + "\n");
                    strSqlString.Append("                           AND LOT_TYPE = 'W'  " + "\n");
                    strSqlString.Append("                           AND HIST_DEL_FLAG = ' ' " + "\n");
                   // strSqlString.Append("                           AND MAT_ID LIKE 'SE%' " + "\n");
                    strSqlString.Append("                           AND LOT_CMF_5 LIKE 'P%'" + "\n");
                    strSqlString.Append("                         GROUP BY MAT_ID, OPER  " + "\n");
                    strSqlString.Append("                         UNION ALL  " + "\n");
                    strSqlString.Append("                        SELECT LSM.MAT_ID " + "\n");
                    strSqlString.Append("                             , LSM.OPER" + "\n");
                    strSqlString.Append("                             , 0 AS TRANS_QTY  " + "\n");
                    strSqlString.Append("                             , SUM(DECODE(LOSS_CODE, 'N01', 0, LOSS_QTY)) AS LOSS_QTY    " + "\n");
                    strSqlString.Append("                          FROM RWIPLOTLSM LSM  " + "\n");
                    strSqlString.Append("                             , RWIPLOTSTS LOT  " + "\n");
                    strSqlString.Append("                         WHERE 1=1  " + "\n");
                    strSqlString.Append("                           AND LOT.FACTORY IN ('" + GlobalVariable.gsAssyDefaultFactory + "', 'CUSTOMER')" + "\n");
                    strSqlString.Append("                           AND LSM.LOT_ID = LOT.LOT_ID " + "\n");
                    strSqlString.Append("                           AND LSM.FACTORY = '" + GlobalVariable.gsAssyDefaultFactory + "'" + "\n");                    
                    strSqlString.Append("                           AND LSM.TRAN_TIME BETWEEN '" + strFromDate + "' AND '" + strToDate + "'  " + "\n");
                    //strSqlString.Append("                           AND LSM.MAT_ID LIKE 'SE%'  " + "\n");
                    strSqlString.Append("                           AND LOT.LOT_TYPE = 'W' " + "\n");
                    strSqlString.Append("                           AND LOT.LOT_CMF_5 LIKE 'P%'" + "\n");
                    strSqlString.Append("                         GROUP BY LSM.MAT_ID, LSM.OPER" + "\n");
                    strSqlString.Append("                       ) A" + "\n");                    
                    strSqlString.Append("                     , VWIPMATDEF B" + "\n");
                    strSqlString.Append("                 WHERE 1=1" + "\n");
                    strSqlString.Append("                   AND A.MAT_ID = B.MAT_ID" + "\n");
                    strSqlString.Append("                   AND B.FACTORY = '" + GlobalVariable.gsAssyDefaultFactory + "'" + "\n");
                    strSqlString.Append("                   AND B.DELETE_FLAG = ' ' " + "\n");                    
                    strSqlString.Append("               ) " + "\n");
                    strSqlString.Append("         GROUP BY MAT_ID " + "\n");
                    strSqlString.Append("       ) D " + "\n");
                    strSqlString.Append(" WHERE A.MAT_ID = B.MAT_ID " + "\n");
                    strSqlString.Append("   AND A.MAT_ID = C.MAT_ID(+) " + "\n");
                    strSqlString.Append("   AND A.MAT_ID = D.MAT_ID(+) " + "\n");
                    strSqlString.Append("   AND B.MAT_TYPE = 'FG' " + "\n");
                    strSqlString.Append("   AND B.DELETE_FLAG <> 'Y' " + "\n");
                    //상세 조회에 따른 SQL문 생성
                    /********************************************************************************************************************/
                    if (udcWIPCondition1.Text != "ALL" && udcWIPCondition1.Text != "")
                        strSqlString.AppendFormat("   AND B.MAT_GRP_1 {0} " + "\n", udcWIPCondition1.SelectedValueToQueryString);
                    if (udcWIPCondition2.Text != "ALL" && udcWIPCondition2.Text != "")
                        strSqlString.AppendFormat("   AND B.MAT_GRP_2 {0} " + "\n", udcWIPCondition2.SelectedValueToQueryString);
                    if (udcWIPCondition3.Text != "ALL" && udcWIPCondition3.Text != "")
                        strSqlString.AppendFormat("   AND B.MAT_GRP_3 {0} " + "\n", udcWIPCondition3.SelectedValueToQueryString);
                    if (udcWIPCondition4.Text != "ALL" && udcWIPCondition4.Text != "")
                        strSqlString.AppendFormat("   AND B.MAT_GRP_4 {0} " + "\n", udcWIPCondition4.SelectedValueToQueryString);
                    if (udcWIPCondition5.Text != "ALL" && udcWIPCondition5.Text != "")
                        strSqlString.AppendFormat("   AND B.MAT_GRP_5 {0} " + "\n", udcWIPCondition5.SelectedValueToQueryString);
                    if (udcWIPCondition6.Text != "ALL" && udcWIPCondition6.Text != "")
                        strSqlString.AppendFormat("   AND B.MAT_GRP_6 {0} " + "\n", udcWIPCondition6.SelectedValueToQueryString);
                    if (udcWIPCondition7.Text != "ALL" && udcWIPCondition7.Text != "")
                        strSqlString.AppendFormat("   AND B.MAT_GRP_7 {0} " + "\n", udcWIPCondition7.SelectedValueToQueryString);
                    if (udcWIPCondition8.Text != "ALL" && udcWIPCondition8.Text != "")
                        strSqlString.AppendFormat("   AND B.MAT_GRP_8 {0} " + "\n", udcWIPCondition8.SelectedValueToQueryString);
                    if (udcWIPCondition9.Text != "ALL" && udcWIPCondition9.Text != "")
                        strSqlString.AppendFormat("   AND B.MAT_GRP_9 {0} " + "\n", udcWIPCondition9.SelectedValueToQueryString);
                    /********************************************************************************************************************/
                    strSqlString.AppendFormat(" GROUP BY {0} " + "\n", QueryCond1);
                    strSqlString.AppendFormat(" ORDER BY {0} " + "\n", QueryCond1);
                }
                #endregion





                
                
            }
            //Type Part Change
            else if (cdvType.SelectedIndex == 1)
            {
                strSqlString.Append("   WITH DT AS " + "\n");
                strSqlString.Append("   ( " + "\n");
                strSqlString.Append("   SELECT LOT_ID, HIST_SEQ, OLD_HIST_SEQ, MAT_ID, OLD_MAT_ID, SUBSTR(LOT_CMF_5, 1, 1) AS LOT_CMF_5, OPER, QTY_1" + "\n");
                strSqlString.Append("     FROM MWIPADTHIS@RPTTOMES" + "\n");
                strSqlString.Append("    WHERE LOT_TYPE = 'W'" + "\n");
                strSqlString.Append("      AND TRAN_CODE = 'ADAPT'" + "\n");
                strSqlString.AppendFormat("      AND TRAN_TIME BETWEEN '{0}' AND '{1}'" + "\n", strFromDate, strToDate);
                strSqlString.AppendFormat("      AND MAT_ID LIKE '{0}%' " + "\n", txtSearchProduct.Text.Trim());      
                strSqlString.Append("      AND TRAN_CMF_5 = 'ADAPT-PART_CHANGE'" + "\n");
                strSqlString.Append("      AND HIST_DEL_FLAG = ' '" + "\n");
                strSqlString.Append("      AND OPER LIKE 'A%'" + "\n");
                strSqlString.Append("   )" + "\n");
                strSqlString.Append("   SELECT SUBSTR(OLD_MAT_ID, 3) AS OUT_PART_NO, SUM(QTY_1) AS OUT_QTY" + "\n");
                strSqlString.Append("        , SUBSTR(MAT_ID, 3) AS IN_PART_NO, SUM(QTY_1) AS IN_QTY" + "\n");
                strSqlString.Append("     FROM (" + "\n");
                strSqlString.Append("           SELECT A.*, B.LOT_ID AS B_LOT_ID" + "\n");
                strSqlString.Append("             FROM DT A" + "\n");
                strSqlString.Append("                , MWIPLOTCMB@RPTTOMES B" + "\n");
                strSqlString.Append("            WHERE 1=1" + "\n");
                strSqlString.Append("              AND A.LOT_ID = B.LOT_ID(+)" + "\n");
                strSqlString.Append("              AND A.OLD_HIST_SEQ = B.HIST_SEQ(+)" + "\n");
                strSqlString.Append("          )" + "\n");
                strSqlString.Append("    WHERE 1=1" + "\n");
                strSqlString.Append("      AND B_LOT_ID IS NULL" + "\n");
                strSqlString.Append("      AND LOT_CMF_5 LIKE 'P%' " + "\n");
                strSqlString.Append("    GROUP BY OLD_MAT_ID, MAT_ID, LOT_CMF_5" + "\n");
                strSqlString.Append("    ORDER BY OLD_MAT_ID, MAT_ID, LOT_CMF_5" + "\n");

            }

            if (GlobalVariable.gsUserGroup == "ADMIN_GROUP" || GlobalVariable.gsUserGroup == "HANA_ADMIN_GROUP")
            {
                System.Windows.Forms.Clipboard.SetText(strSqlString.ToString());
            }

            return strSqlString.ToString();
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
            int iSubTotalCount = 2;
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
                //Type Basics
                if (cdvType.SelectedIndex == 0)
                {
                    //1.Griid 합계 표시
                    // FAMILY
                    if (udcWIPCondition2.Text != "ALL" && udcWIPCondition2.Text != "")
                    {
                        iSubTotalCount += 1;
                    }
                    // PACKAGE
                    if (udcWIPCondition3.Text != "ALL" && udcWIPCondition3.Text != "")
                    {
                        iSubTotalCount += 1;
                    }
                    // TYPE1
                    if (udcWIPCondition4.Text != "ALL" && udcWIPCondition4.Text != "")
                    {
                        iSubTotalCount += 1;
                    }
                    // TYPE2
                    if (udcWIPCondition5.Text != "ALL" && udcWIPCondition5.Text != "")
                    {
                        iSubTotalCount += 1;
                    }
                    // LEAD COUNT
                    if (udcWIPCondition6.Text != "ALL" && udcWIPCondition6.Text != "")
                    {
                        iSubTotalCount += 1;
                    }
                    // DENSITY
                    if (udcWIPCondition7.Text != "ALL" && udcWIPCondition7.Text != "")
                    {
                        iSubTotalCount += 1;
                    }
                    // GENERATION
                    if (udcWIPCondition8.Text != "ALL" && udcWIPCondition8.Text != "")
                    {
                        iSubTotalCount += 1;
                    }

                    // Sub Total
                    int[] rowType = spdData.RPT_DataBindingWithSubTotal(dt, 0, iSubTotalCount, 1, null, null);
                    // Total
                    spdData.RPT_FillDataSelectiveCells("Total", 0, iSubTotalCount, 0, 1, true, Align.Center, VerticalAlign.Center);

                    //4. Column Auto Fit
                    spdData.RPT_AutoFit(false);

                    if (GlobalVariable.gsGlovalSite == "K1")
                    {
                        // 2009-03-26-정비재 : Yield는 Sum이 아닌 평균을 계산한다.
                        SetAvgVertical(1, iSubTotalCount + 16, false);

                        // Sub Total에 대한 평균을 계산한다.
                        for (int iRow = 2; iRow <= iSubTotalCount; iRow++)
                        {
                            SetTotalAvgVertical(iRow, iSubTotalCount + 16, false);
                        }
                    }
                    else
                    {                        
                        SetAvgVertical(1, iSubTotalCount + 19, false);
                                                
                        for (int iRow = 2; iRow <= iSubTotalCount; iRow++)
                        {
                            SetTotalAvgVertical(iRow, iSubTotalCount + 19, false);
                        }
                    }
                }
                else if (cdvType.SelectedIndex == 1)
                {
                    //1.Griid 합계 표시
                    int[] rowType = spdData.RPT_DataBindingWithSubTotal(dt, 0, 0, 4, null, null, btnSort);
                    
                    spdData.RPT_AutoFit(false);
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


        #endregion

        
    }
}
