//-----------------------------------------------------------------------------
//
//   System      : MES
//   File Name   : CommonFunction.cs
//   Description : Client Common function Module
//
//   MES Version : 4.x.x.x
//
//   History
//       - **** Do Not Modify in Site!!! ****
//       - 2004-06-07 : Created by SKJIN
//
//
//   Copyright(C) 1998-2005 MIRACOM,INC.
//   All rights reserved.
//
//-----------------------------------------------------------------------------

using System;
using System.IO;
using System.Text;
using System.Data;
using System.Collections;
using System.Diagnostics;
using System.Drawing;
using System.Windows.Forms;
//using Excel = Microsoft.Office.Interop.Excel;
//using Microsoft.Office.Interop.Excel;
using Excel;

namespace Miracom.SmartWeb.UI
{
    public sealed partial class CmnExcelFunction
    {
        private const int EXCEL_FIRST_ROWCOL = 1;
        private const int EXCEL_START_ROW = 3;
        private const int EXCEL_START_COL = 1;

        /// <summary>
        /// Export Data of Control to Excel Application Data
        /// </summary>
        /// <param name="ctlObject">Excel export target object(ListView, FpSpread)</param>
        /// <param name="sTitle">Excel export title string.</param>
        /// <param name="sCondition">Excel export condition string.</param>
        /// <param name="bColorFlag">Cell Fore/Backgroup color set(True/False)</param>
        /// <param name="bTextBase">Cell format only text(True/False)</param>
        /// <param name="bShowDate">Current Date/Time visible(True/False)</param>
        /// <param name="iStartCol">Start column(-1: All column)</param>
        /// <param name="iEndCol">Last column(-1: All column)</param>
        /// <returns>True or False, Otherwise Exception pass the throw </returns>
        public static bool ExportToExcel(System.Windows.Forms.Control ctlObject,
                            string sTitle, string sCondition,
                            bool bColorFlag, bool bTextBase, bool bShowDate,
                            int iStartCol, int iEndCol)
        {
            return ExportToExcel(ctlObject, sTitle, sCondition,
                            bColorFlag, bTextBase, bShowDate, iStartCol, iEndCol, 0, false);
        }

        /// <summary>
        /// Export Data of Control to Excel Application Data
        /// </summary>
        /// <param name="ctlObject">Excel export target object(ListView, FpSpread)</param>
        /// <param name="sTitle">Excel export title string.</param>
        /// <param name="sCondition">Excel export condition string.</param>
        /// <param name="bColorFlag">Cell Fore/Backgroup color set(True/False)</param>
        /// <param name="bTextBase">Cell format only text(True/False)</param>
        /// <param name="bShowDate">Current Date/Time visible(True/False)</param>
        /// <param name="iStartCol">Start column(-1: All column)</param>
        /// <param name="iEndCol">Last column(-1: All column)</param>
        /// <param name="iRowHeight">Row Height(0: default RowHeight)</param>
        /// <returns>True or False, Otherwise Exception pass the throw </returns>
        public static bool ExportToExcel(System.Windows.Forms.Control ctlObject,
                            string sTitle, string sCondition,
                            bool bColorFlag, bool bTextBase, bool bShowDate,
                            int iStartCol, int iEndCol, int iRowHeight)
        {
            return ExportToExcel(ctlObject, sTitle, sCondition,
                            bColorFlag, bTextBase, bShowDate, iStartCol, iEndCol, iRowHeight, false);
        }

        /// <summary>
        /// Export Data of Control to Excel Application Data
        /// </summary>
        /// <param name="ctlObject">Excel export target object(ListView, FpSpread)</param>
        /// <param name="sTitle">Excel export title string.</param>
        /// <param name="sCondition">Excel export condition string.</param>
        /// <param name="bColorFlag">Cell Fore/Backgroup color set(True/False)</param>
        /// <param name="bTextBase">Cell format only text(True/False)</param>
        /// <param name="bShowDate">Current Date/Time visible(True/False)</param>
        /// <param name="iStartCol">Start column(-1: All column)</param>
        /// <param name="iEndCol">Last column(-1: All column)</param>
        /// <param name="iRowHeight">Row Height(0: default RowHeight)</param>
        /// <param name="bOnlyData">Only Data export</param>
        /// <returns>True or False, Otherwise Exception pass the throw </returns>
        public static bool ExportToExcel(System.Windows.Forms.Control ctlObject,
                            string sTitle, string sCondition,
                            bool bColorFlag, bool bTextBase, bool bShowDate,
                            int iStartCol, int iEndCol, int iRowHeight, bool bOnlyData)
        {
            try
            {
                // Export excel title.
                if (sTitle == null)
                {
                    sTitle = "##";
                }
                else
                {
                    // Invalide Sheet name replace(/,?,*,[,])
                    sTitle = sTitle.Trim().Replace("/", "").Replace("?", "").Replace("*", "").Replace("[", "").Replace("]", "");
                    if (sTitle.Length > 30)
                    {
                        sTitle = sTitle.Substring(0, 30);
                    }
                }

                // Export excel condition.
                if (sCondition == null)
                {
                    sCondition = " ";
                }

                // Export excel data.
                if (ctlObject is ListView)
                {
                    if (((ListView)ctlObject).Items.Count > 0)
                    {
                        return ListviewToExcel(new ListView[] { (ListView)ctlObject },
                                null, 0,
                                sTitle, sCondition,
                                bColorFlag, bTextBase, bShowDate);
                    }
                }
                else if (ctlObject is FarPoint.Win.Spread.FpSpread)
                {
                    if (((FarPoint.Win.Spread.FpSpread)ctlObject).ActiveSheet.RowCount > 0)
                    {
                        return SpreadSheetToExcel(new FarPoint.Win.Spread.SheetView[] { ((FarPoint.Win.Spread.FpSpread)ctlObject).ActiveSheet },
                                null, 0,
                                sTitle, sCondition,
                                bColorFlag, bTextBase, bShowDate,
                                iStartCol, iEndCol, iRowHeight,
                                bOnlyData, null);
                    }
                }
                else
                {
                    CmnFunction.ShowMsgBox(LanguageFunction.GetMessage(11), "Error", MessageBoxButtons.OK, 1);
                    return false;
                }

            }
            catch (Exception ex)
            {
                CmnFunction.ShowMsgBox(ex.Message, "Error", MessageBoxButtons.OK, 1);
            }

            return false;

        }


        /// <summary>
        /// Export Data of Array Control to Excel Application Data
        /// </summary>
        /// <param name="ctlObject">Excel export target array object(ListView, FpSpread)</param>
        /// <param name="sImageFileName">Excel export image file full path(Jpg, bmp, etc)</param>
        /// <param name="iImagePostion">Image posiont(1: Top, 2: Bottom)</param>
        /// <param name="sTitle">Excel export title string.</param>
        /// <param name="sCondition">Excel export condition string.</param>
        /// <param name="bColorFlag">Cell Fore/Backgroup color set(True/False)</param>
        /// <param name="bTextBase">Cell format only text(True/False)</param>
        /// <param name="bShowDate">Current Date/Time visible(True/False)</param>
        /// <param name="iStartCol">Start column(-1: All column)</param>
        /// <param name="iEndCol">Last column(-1: All column)</param>
        /// <returns>True or False, Otherwise Exception pass the throw </returns>
        public static bool ExportToExcelEx(System.Windows.Forms.Control[] ctlObject,
                            string sImageFileName, int iImagePostion,
                            string sTitle, string sCondition,
                            bool bColorFlag, bool bTextBase, bool bShowDate,
                            int iStartCol, int iEndCol)
        {
            return ExportToExcelEx(ctlObject, sImageFileName, iImagePostion,
                            sTitle, sCondition, bColorFlag, bTextBase, bShowDate,
                            iStartCol, iEndCol, 0, false, null);
        }

        /// <summary>
        /// Export Data of Array Control to Excel Application Data
        /// </summary>
        /// <param name="ctlObject">Excel export target array object(ListView, FpSpread)</param>
        /// <param name="sImageFileName">Excel export image file full path(Jpg, bmp, etc)</param>
        /// <param name="iImagePostion">Image posiont(1: Top, 2: Bottom)</param>
        /// <param name="sTitle">Excel export title string.</param>
        /// <param name="sCondition">Excel export condition string.</param>
        /// <param name="bColorFlag">Cell Fore/Backgroup color set(True/False)</param>
        /// <param name="bTextBase">Cell format only text(True/False)</param>
        /// <param name="bShowDate">Current Date/Time visible(True/False)</param>
        /// <param name="iStartCol">Start column(-1: All column)</param>
        /// <param name="iEndCol">Last column(-1: All column)</param>
        /// <param name="iRowHeight">Row Height(0: default RowHeight)</param>
        /// <param name="bOnlyData">Only Data export</param>
        /// <returns>True or False, Otherwise Exception pass the throw </returns>
        public static bool ExportToExcelEx(System.Windows.Forms.Control[] ctlObject,
                            string sImageFileName, int iImagePostion,
                            string sTitle, string sCondition,
                            bool bColorFlag, bool bTextBase, bool bShowDate,
                            int iStartCol, int iEndCol, int iRowHeight, bool bOnlyData)
        {
            return ExportToExcelEx(ctlObject, sImageFileName, iImagePostion,
                            sTitle, sCondition, bColorFlag, bTextBase, bShowDate,
                            iStartCol, iEndCol, iRowHeight, bOnlyData, null);
        }

        /// <summary>
        /// Export Data of Array Control to Excel Application Data
        /// </summary>
        /// <param name="ctlObject">Excel export target array object(ListView, FpSpread)</param>
        /// <param name="sImageFileName">Excel export image file full path(Jpg, bmp, etc)</param>
        /// <param name="iImagePostion">Image posiont(1: Top, 2: Bottom)</param>
        /// <param name="sTitle">Excel export title string.</param>
        /// <param name="sCondition">Excel export condition string.</param>
        /// <param name="bColorFlag">Cell Fore/Backgroup color set(True/False)</param>
        /// <param name="bTextBase">Cell format only text(True/False)</param>
        /// <param name="bShowDate">Current Date/Time visible(True/False)</param>
        /// <param name="iStartCol">Start column(-1: All column)</param>
        /// <param name="iEndCol">Last column(-1: All column)</param>
        /// <param name="iRowHeight">Row Height(0: default RowHeight)</param>
        /// <param name="bOnlyData">Only Data export</param>
        /// <param name="pageOptions">Set Print PageSetup Array[12] values(Set defualt use = -1)</param>
        /// <returns>True or False, Otherwise Exception pass the throw </returns>
        public static bool ExportToExcelEx(System.Windows.Forms.Control[] ctlObject,
                            string sImageFileName, int iImagePostion,
                            string sTitle, string sCondition,
                            bool bColorFlag, bool bTextBase, bool bShowDate,
                            int iStartCol, int iEndCol, int iRowHeight,
                            bool bOnlyData, float[] pageOptions)
        {
            ListView[] xList = null;    
            FarPoint.Win.Spread.SheetView[] xSheet = null;
            Boolean bReturn = false;

            try
            {
                // Export excel title.
                if (sTitle == null)
                {
                    sTitle = "##";
                }
                else
                {
                    // Invalide Sheet name replace(/,?,*,[,])
                    sTitle = sTitle.Trim().Replace("/", "").Replace("?", "").Replace("*", "").Replace("[", "").Replace("]", "");
                    if (sTitle.Length > 30)
                    {
                        sTitle = sTitle.Substring(0, 30);
                    }
                }

                // Export excel condition.
                if (sCondition == null)
                {
                    sCondition = " ";
                }

                foreach (System.Windows.Forms.Control objList in ctlObject)
                {
                    if (objList is ListView)
                    {
                        ListView[] tempView = null;

                        if (((ListView)objList).Items.Count <= 0)
                        {
                            continue;
                        }

                        if (xList == null || xList.Length <= 0)
                        {
                            tempView = new ListView[1];
                            tempView[0] = (ListView)objList;
                        }
                        else
                        {
                            tempView = new ListView[xList.Length + 1];
                            Array.Copy(xList, tempView, xList.Length);
                            tempView[xList.Length] = (ListView)objList;
                        }

                        xList = new ListView[tempView.Length];
                        Array.Copy(tempView, xList, tempView.Length);
                    }
                    else if (objList is FarPoint.Win.Spread.FpSpread)
                    {
                        FarPoint.Win.Spread.SheetView[] tempView = null;

                        if (((FarPoint.Win.Spread.FpSpread)objList).ActiveSheet.RowCount <= 0)
                        {
                            continue;
                        }

                        if (xSheet == null || xSheet.Length <= 0)
                        {
                            tempView = new FarPoint.Win.Spread.SheetView[1];
                            tempView[0] = ((FarPoint.Win.Spread.FpSpread)objList).ActiveSheet;
                        }
                        else
                        {
                            tempView = new FarPoint.Win.Spread.SheetView[xSheet.Length + 1];
                            Array.Copy(xSheet, tempView, xSheet.Length);
                            tempView[xSheet.Length] = ((FarPoint.Win.Spread.FpSpread)objList).ActiveSheet;
                        }

                        xSheet = new FarPoint.Win.Spread.SheetView[tempView.Length];
                        Array.Copy(tempView, xSheet, tempView.Length);
                    }
                }

                if (xList != null && xList.Length > 0)
                {
                    bReturn = ListviewToExcel(xList,
                                sImageFileName, iImagePostion,
                                sTitle, sCondition, bColorFlag, bTextBase, bShowDate);
                }

                if (xSheet != null && xSheet.Length > 0)
                {
                    bReturn = SpreadSheetToExcel(xSheet,
                                sImageFileName, iImagePostion,
                                sTitle, sCondition,
                                bColorFlag, bTextBase, bShowDate,
                                iStartCol, iEndCol, iRowHeight, bOnlyData, pageOptions);
                }
                else
                {
                    bReturn = SpreadSheetToExcel(xSheet,
                                sImageFileName, iImagePostion,
                                sTitle, sCondition,
                                bColorFlag, bTextBase, bShowDate,
                                iStartCol, iEndCol, iRowHeight, bOnlyData, pageOptions);
                }

                return bReturn;
            }
            catch (Exception ex)
            {
                CmnFunction.ShowMsgBox(ex.Message, "Error", MessageBoxButtons.OK, 1);
            }

            return false;

        }

        // ExportToText()
        //       - Export Data of Control to Tab Text Data
        // Return Value
        //       - True or False
        // Arguments
        //       - ByRef l_control as listview
        //       - ByVal title as string : Form title
        //       - Optional ByVal Condition as string
        //
        private static bool ExportToText(System.Windows.Forms.Control ctlObject, string title, string condition)
        {
            string sClipData;
            string sFileName;
            int i, j;

            StreamWriter TXTStream = null;
            SaveFileDialog SaveDialog;
            ListView lisCtl;
            ListViewItem lisItem;

            try
            {
                System.Windows.Forms.Application.DoEvents();
                Cursor.Current = Cursors.WaitCursor;

                sFileName = title;
                sFileName += "_" + DateTime.Now.ToString("yyyyMMddHHmmss") + ".xls";


                SaveDialog = new SaveFileDialog();
                SaveDialog.FileName = sFileName;
                SaveDialog.Filter = "Excel files (*.xls)|*.xls|All files (*.*)|*.*";
                if (SaveDialog.ShowDialog() == System.Windows.Forms.DialogResult.Cancel)
                {
                    return false;
                }

                sFileName = SaveDialog.FileName;
                if (sFileName.IndexOf(".") <= 0)
                {
                    sFileName += ".xls";
                }

                TXTStream = File.CreateText(sFileName);

                sClipData = "";
                sClipData = title + "\r";
                if (condition.Trim() != "")
                {
                    sClipData += "\r" + "\r";
                    sClipData += condition + "\r";
                    sClipData += "\r";
                }
                sClipData = sClipData + "\r";

                if (ctlObject is ListView)
                {
                    lisCtl = (ListView)ctlObject;
                    for (i = 0; i < lisCtl.Columns.Count; i++)
                    {
                        if (lisCtl.Columns[i].Width > 0)
                        {
                            //sClipData = sClipData + Strings.Replace(lisCtl.Columns[i].Text, "\r", "   ", 1, -1, 0).Replace("\n", " ") + "\t";
                            sClipData += lisCtl.Columns[i].Text.Replace("\r", "   ").Replace("\n", " ") + "\t";
                        }
                    }
                    sClipData += "\r";

                    for (i = 0; i < lisCtl.Items.Count; i++)
                    {
                        for (j = 0; j < lisCtl.Columns.Count; j++)
                        {
                            if (lisCtl.Columns[j].Width > 0)
                            {
                                lisItem = lisCtl.Items[i];
                                try
                                {
                                    if (lisItem.SubItems[j].Text.IndexOf("<") > 0 ||
                                        lisItem.SubItems[j].Text.IndexOf(">") > 0)
                                    {
                                        sClipData += System.Convert.ToString(lisItem.SubItems[j].Text).Trim().Replace(" ", "").Replace("\r", "   ").Replace("\n", "  ").Replace("\t", " ") + "\t";
                                    }
                                    else
                                    {
                                        sClipData += System.Convert.ToString(lisItem.SubItems[j].Text).Replace("\r", "   ").Replace("\n", "  ").Replace("\t", " ") + "\t";
                                    }
                                }
                                catch
                                {
                                    sClipData += "\t";
                                }
                            }
                        }
                        sClipData += "\r";
                    }
                }
                else
                {
                    CmnFunction.ShowMsgBox("Invalid Control Type.", "Warring", MessageBoxButtons.OK, 1);
                    return false;
                }

                Clipboard.SetDataObject((object)sClipData, false, 5, 10);

                TXTStream.Write(sClipData);
                return true;

            }
            catch (Exception ex)
            {
                CmnFunction.ShowMsgBox(ex.Message, "Error", MessageBoxButtons.OK, 1);
                return false;
            }
            finally
            {
                if (TXTStream != null)
                {
                    TXTStream.Close();
                    TXTStream = null;
                }

                Cursor.Current = Cursors.Default;
            }

        }

        // ListViewToExcel()
        //       - Export Data of Listview Control to Excel Application Data
        // Return Value
        //       - True or False
        private static bool ListviewToExcel(ListView[] xList,
                string imageFile, int imagePostion,
                string title, string condition,
                bool colorFlag, bool bTextBase, bool bShowDate)
        {
            Excel.Application xlApp = new Excel.Application();
            Excel.Workbooks xlBooks = xlApp.Workbooks;
            Excel.Workbook xlBook = xlBooks.Add(Excel.XlWBATemplate.xlWBATWorksheet);
            Excel.Worksheet xlSheet = (Excel.Worksheet)xlBook.ActiveSheet;
            Excel.Range xlWiths;

            Int32 i, j;
            int iRestRows, iRestCols, iColumnCnt;
            string clipboardData;
            ListViewItem lisItem;
            bool bReturn = true;

            try
            {
                System.Windows.Forms.Application.DoEvents();
                Cursor.Current = Cursors.WaitCursor;

                if (title.Trim().Length > 30)
                {
                    xlSheet.Name = title.TrimEnd().Substring(0, 30);
                }
                else
                {
                    xlSheet.Name = title.TrimEnd();
                }

                // Start Row/Column reset.
                iRestRows = EXCEL_START_ROW;
                iRestCols = EXCEL_START_COL;

                // 데이터 조회조건에 대한 설명 또는 추가정보 설정.
                condition = condition.TrimEnd(new char[] { ' ', '\r', '\n' });
                if (condition.Length > 0)
                {
                    string[] sTempStr = condition.Split(new char[] { '\r', '\n' });
                    iRestRows += (sTempStr.Length + 1);
                }

                //First Column Width
                xlSheet.Cells.Select();
                xlSheet.Cells.EntireColumn.ColumnWidth = 1;
                xlSheet.Cells.Font.Name = "Tahoma";
                xlSheet.Cells.Font.Size = 10;

                // 이미지가 상단에 위치할 경우(만약, imagePostion == 0이면 이미지를 표시하지 않음)
                if (imageFile != null && imagePostion == 1)
                {
                    int importRows = iRestRows;

                    importRows = InsertImageToExcel((Excel._Worksheet)xlSheet, imageFile, importRows, iRestCols);
                    if (importRows > 0) // Image is Top position.
                    {
                        iRestRows += importRows + 2;
                    }
                }

                // 각종 엑셀의 경고메시지를 보이지 않도록 합니다.
                xlApp.DisplayAlerts = false;

                //'셀서식을 TEXT로 인식하도록 한다.
                //'단, 셀 서식이 Text일 경우에는 셀 병합할 때 확인 창이 뜬다.
                if (bTextBase == true)
                {
                    xlSheet.Cells.NumberFormat = "@";
                }

                // 여러개의 ListView를 하나의 Excel에 붙여넣음.
                foreach (ListView activeList in xList)
                {

                    if (activeList.Items.Count <= 0)
                    {
                        continue;
                    }



                    iColumnCnt = 0;
                    for (i = 0; i < activeList.Columns.Count; i++)
                    {
                        if (activeList.Columns[i].Width > 0)
                        {
                            iColumnCnt++;
                        }
                    }

                    //'Column Header에 대한 셀서식
                    xlWiths = xlSheet.get_Range(xlSheet.Cells[iRestRows, iRestCols], xlSheet.Cells[iRestRows, iRestCols + iColumnCnt - 1]);
                    xlWiths.HorizontalAlignment = ExcelComponent.XlHAlign.xlHAlignCenter; //-4108
                    xlWiths.VerticalAlignment = ExcelComponent.XlVAlign.xlValignCenter; //-4108
                    xlWiths.Interior.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.DodgerBlue);
                    xlWiths.Font.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.White);
                    xlWiths.Font.Bold = true;
                    xlWiths.Borders[Excel.XlBordersIndex.xlEdgeLeft].LineStyle = ExcelComponent.XlLineStyle.xlContinuous;
                    xlWiths.Borders[Excel.XlBordersIndex.xlEdgeRight].LineStyle = ExcelComponent.XlLineStyle.xlContinuous;
                    xlWiths.Borders[Excel.XlBordersIndex.xlEdgeTop].LineStyle = ExcelComponent.XlLineStyle.xlContinuous;
                    xlWiths.Borders[Excel.XlBordersIndex.xlEdgeBottom].LineStyle = ExcelComponent.XlLineStyle.xlContinuous;
                    if (xlWiths.Rows.Count > 1)
                    {
                        xlWiths.Borders[Excel.XlBordersIndex.xlInsideHorizontal].LineStyle = ExcelComponent.XlLineStyle.xlContinuous;
                    }
                    if (xlWiths.Columns.Count > 1)
                    {
                        xlWiths.Borders[Excel.XlBordersIndex.xlInsideVertical].LineStyle = ExcelComponent.XlLineStyle.xlContinuous;
                    }

                    //' List에 대한 셀서식
                    if (activeList.Items.Count > 0)
                    {
                        xlWiths = xlSheet.get_Range(xlSheet.Cells[iRestRows + 1, iRestCols], xlSheet.Cells[iRestRows + activeList.Items.Count, iRestCols + iColumnCnt - 1]);
                        xlWiths.Borders[Excel.XlBordersIndex.xlEdgeLeft].LineStyle = ExcelComponent.XlLineStyle.xlContinuous;
                        xlWiths.Borders[Excel.XlBordersIndex.xlEdgeRight].LineStyle = ExcelComponent.XlLineStyle.xlContinuous;
                        xlWiths.Borders[Excel.XlBordersIndex.xlEdgeBottom].LineStyle = ExcelComponent.XlLineStyle.xlContinuous;
                        if (xlWiths.Columns.Count > 1)
                        {
                            xlWiths.Borders[Excel.XlBordersIndex.xlInsideVertical].LineStyle = ExcelComponent.XlLineStyle.xlContinuous;
                            xlWiths.Borders[Excel.XlBordersIndex.xlInsideVertical].Weight = ExcelComponent.XlBorderWeight.xlHairline;
                        }

                        if (xlWiths.Rows.Count > 1)
                        {
                            xlWiths.Borders[Excel.XlBordersIndex.xlInsideHorizontal].LineStyle = ExcelComponent.XlLineStyle.xlContinuous;
                            xlWiths.Borders[Excel.XlBordersIndex.xlInsideHorizontal].Weight = ExcelComponent.XlBorderWeight.xlHairline;
                        }

                        if (colorFlag == true)
                        {
                            xlWiths.Font.Color = System.Drawing.ColorTranslator.ToOle(activeList.ForeColor);
                        }
                    }

                    clipboardData = "";
                    for (i = 0; i < activeList.Columns.Count; i++)
                    {
                        if (activeList.Columns[i].Width > 0)
                        {
                            clipboardData += activeList.Columns[i].Text + "\t";
                        }
                    }
                    clipboardData += "\r";
                    Clipboard.SetDataObject((object)clipboardData, false, 5, 10);
                    ((Excel.Range)xlSheet.Cells[iRestRows, iRestCols]).Select();
                    //xlSheet.PasteSpecial(Type.Missing, false, false, Type.Missing, Type.Missing, Type.Missing, Type.Missing);
                    xlSheet._PasteSpecial(Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing);

                    clipboardData = "";
                    for (i = 0; i < activeList.Items.Count; i++)
                    {
                        for (j = 0; j < activeList.Columns.Count; j++)
                        {
                            if (activeList.Columns[j].Width > 0)
                            {
                                try
                                {
                                    if (activeList.Items[i].SubItems[j].Text.IndexOf("<") > 0 ||
                                        activeList.Items[i].SubItems[j].Text.IndexOf(">") > 0)
                                    {
                                        clipboardData += System.Convert.ToString(activeList.Items[i].SubItems[j].Text).Trim().Replace(" ", "").Replace("\r", "   ").Replace("\n", "  ").Replace("\t", " ") + "\t";
                                    }
                                    else
                                    {
                                        clipboardData += System.Convert.ToString(activeList.Items[i].SubItems[j].Text).Replace("\r", "   ").Replace("\n", "  ").Replace("\t", " ") + "\t";
                                    }
                                }
                                catch
                                {
                                    clipboardData += "\t";
                                }
                            }
                        }

                        clipboardData = clipboardData + "\r";
                        if ((i + 1) % 100 == 0 || i == activeList.Items.Count - 1)
                        {
                            if ((i + 1) >= 100)
                            {
                                ((Excel.Range)xlSheet.Cells[(iRestRows + 1 + 100 * ((i) / 100)), iRestCols]).Select();
                                //xlSheet.get_Range("B" + System.Convert.ToString(iRestRows + 1 + 100 * ((i) / 100)), Type.Missing).Select();
                            }
                            else
                            {
                                ((Excel.Range)xlSheet.Cells[(iRestRows + 1), iRestCols]).Select();
                                //xlSheet.get_Range("B" + System.Convert.ToString(iRestRows + 1), Type.Missing).Select();
                            }

                            Clipboard.SetDataObject((object)clipboardData, false, 5, 10);
                            //xlSheet.PasteSpecial(Type.Missing, false, false, Type.Missing, Type.Missing, Type.Missing, Type.Missing);
                            xlSheet._PasteSpecial(Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing);
                            clipboardData = "";
                        }
                    }

                    // Column크기를 자동으로 조절.
                    xlWiths = xlSheet.get_Range(xlSheet.Cells[iRestRows + 1, iRestCols], xlSheet.Cells[iRestRows + activeList.Items.Count, iRestCols + iColumnCnt - 1]);
                    xlWiths.CurrentRegion.EntireColumn.AutoFit();

                    if (colorFlag == true)
                    {
                        for (i = 0; i < activeList.Items.Count; i++)
                        {
                            lisItem = activeList.Items[i];
                            if (lisItem.SubItems[0].ForeColor.IsEmpty == false)
                            {
                                if (lisItem.SubItems[0].ForeColor.ToArgb() != System.Drawing.Color.Black.ToArgb())
                                {
                                    xlSheet.get_Range(xlSheet.Cells[i + iRestRows + 1, iRestCols], xlSheet.Cells[i + iRestRows + 1, iRestCols + iColumnCnt - 1]).Font.Color =
                                    System.Drawing.ColorTranslator.ToOle(lisItem.SubItems[0].ForeColor);
                                }
                            }
                        }
                    }

                    iRestRows += activeList.Items.Count + 1;
                }

                //'Title 에 대한 셀 서식 지정
                xlWiths = xlSheet.get_Range(xlSheet.Cells[1, 2], xlSheet.Cells[1, 2]);
                xlWiths.VerticalAlignment = ExcelComponent.XlVAlign.xlValignCenter;
                xlWiths.RowHeight = 30;
                xlWiths.Font.Size = 12;
                xlWiths.Font.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.Black);
                xlWiths.Font.Bold = true;
                //xlWiths.Font.Name = "Tahoma";
                ((Excel.Range)xlSheet.Cells[1, iRestCols]).Value2 = title;

                if (bShowDate == true)
                {
                    string sNowDate;
                    sNowDate = string.Format(Convert.ToString(DateTime.Now), "yyyy-MM-dd") + " " + string.Format(Convert.ToString(DateTime.Now), "HH:mm:ss");
                    ((Excel.Range)xlSheet.Cells[2, iRestCols]).Value2 = "Today: " + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                }

                if (condition.Length > 0)
                {
                    Clipboard.SetDataObject((object)condition, false, 5, 10);
                    ((Excel.Range)xlSheet.Cells[3, iRestCols]).Select();
                    //xlSheet.PasteSpecial(Type.Missing, false, false, Type.Missing, Type.Missing, Type.Missing, Type.Missing);
                    xlSheet._PasteSpecial(Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing);
                }

                ((Excel.Range)xlSheet.Cells[1, 1]).Select();
                //System.Runtime.InteropServices.Marshal.ReleaseComObject(xlWiths);
                return bReturn;

            }
            catch (Exception ex)
            {
                String errorMessage = "";
                errorMessage = String.Concat(errorMessage, ex.Message);
                errorMessage = String.Concat(errorMessage, " Line: ");
                errorMessage = String.Concat(errorMessage, ex.Source);

                CmnFunction.ShowMsgBox(errorMessage, "Error [" + ex.Source + "]", MessageBoxButtons.OK, 1);
                bReturn = false;
                return bReturn;
            }
            finally
            {
                // 사용자에게 저장 여부를 묻는다.
                xlBook.Saved = false;

                System.Runtime.InteropServices.Marshal.ReleaseComObject(xlSheet);
                System.Runtime.InteropServices.Marshal.ReleaseComObject(xlBook);
                System.Runtime.InteropServices.Marshal.ReleaseComObject(xlBooks);

                if (bReturn == false)
                {
                    //Make Excel application close.
                    xlApp.DisplayAlerts = false;
                    //xlBook.Saved = true;
                    xlApp.Quit();
                }
                else
                {
                    // 모든 엑셀의 경고메시지가 나타나도록 한다.
                    xlApp.DisplayAlerts = true;
                    //Make Excel visible and give the user control.
                    xlApp.Visible = true;
                    xlApp.UserControl = true;
                }

                System.Runtime.InteropServices.Marshal.ReleaseComObject(xlApp);
                GC.Collect();
                Cursor.Current = Cursors.Default;
            }

        }


        // Print()
        //       - Export Data of Control to Print
        // Return Value
        //       - True or False
        public static bool Print(System.Windows.Forms.Control ctlObject,
                            string sTitle, string sCondition,
                            bool bColorFlag, bool bTextBase, bool bShowDate,
                            int iStartCol, int iEndCol, int iRowHeight,
                            bool bOnlyData, float[] pageOptions)
        {
            string TempFile = "";

            try
            {
                // Export excel title.
                if (sTitle == null)
                {
                    sTitle = "##";
                }
                else
                {
                    // Invalide Sheet name replace(/,?,*,[,])
                    sTitle = sTitle.Trim().Replace("/", "").Replace("?", "").Replace("*", "").Replace("[", "").Replace("]", "");
                    if (sTitle.Length > 30)
                    {
                        sTitle = sTitle.Substring(0, 30);
                    }
                }

                // Export excel condition.
                if (sCondition == null)
                {
                    sCondition = " ";
                }

                // Export excel data.
                if (ctlObject is ListView)
                {
                    // ListView를 기준으로 그래프가 상위/하위에 있는지를 구분함.
                    //imagePosition = 1; // 1:Top  2:Bottom

                    if (((ListView)ctlObject).Items.Count > 0)
                    {
                        return ListviewToExcel(new ListView[] { (ListView)ctlObject },
                                TempFile, 0,
                                sTitle, sCondition,
                                bColorFlag, bTextBase, bShowDate);
                    }
                }
                else if (ctlObject is FarPoint.Win.Spread.FpSpread)
                {
                    // FpSpread를 기준으로 그래프가 상위/하위에 있는지를 구분함.
                    //imagePosition = 1; // 1:Top  2:Bottom

                    if (((FarPoint.Win.Spread.FpSpread)ctlObject).ActiveSheet.RowCount > 0)
                    {
                        return SpreadSheetToPrint(new FarPoint.Win.Spread.SheetView[] { ((FarPoint.Win.Spread.FpSpread)ctlObject).ActiveSheet },
                               TempFile, 0,
                               sTitle, sCondition,
                               bColorFlag, bTextBase, bShowDate,
                               iStartCol, iEndCol, iRowHeight, false, pageOptions);
                    }
                }
                else
                {
                    CmnFunction.ShowMsgBox(LanguageFunction.GetMessage(11), "Error", MessageBoxButtons.OK, 1);
                    return false;
                }

            }
            catch (Exception ex)
            {
                CmnFunction.ShowMsgBox(ex.Message, "Error", MessageBoxButtons.OK, 1);
            }
            finally
            {

            }


            return false;

        }

        // SpreadSheetToExcel()
        //       - Export Data of Spread Sheet Control to Excel Application Data
        // Return Value
        //       - True or False
        // Arguments
        //       - ByRef l_control As Spread Sheet
        //       - ByVal title As string
        //       - Optional ByVal Condition As string
        //       - Optional ByVal ColorFlag As Boolean
        //       - Optional ByVal bTextBase As Boolean    '
        //
        public static bool SpreadSheetToExcel(FarPoint.Win.Spread.SheetView actSheet, string imageFile, int imagePostion,
                string title, string condition, bool colorFlag, bool bTextBase, bool bShowDate, int iStartCol, int iEndCol)
        {

            Excel.Application XApp = new Excel.Application();
            Excel.Workbooks XBooks = XApp.Workbooks;
            Excel.Workbook XBook = XBooks.Add(XlWBATemplate.xlWBATWorksheet);
            Excel.Worksheet XSheet = (Excel.Worksheet)XBook.ActiveSheet;
            Excel.Range xWiths;

            //Excel.Application XApp;
            //Excel._Workbook XBook;
            //Excel._Worksheet XSheet;
            //Excel.Range xWiths;

            Int32 i, j, k;
            int[] aColumns = null;
            int iRestRows, iRestCols, iColumnCnt;
            string clipboardData;
            bool bAddColumn;

            try
            {
                int iRowHeaderCount, iColHeaderCount;

                //XApp = new Excel.Application();
                //XBook = (Excel._Workbook)(XApp.Workbooks.Add(XlWBATemplate.xlWBATWorksheet));
                //XSheet = (Excel._Worksheet)XBook.ActiveSheet;

                //XApp.Visible = false;
                //XApp.Visible = true;
                System.Windows.Forms.Application.DoEvents();
                Cursor.Current = Cursors.WaitCursor;

                if (title.Trim().Length > 30)
                {
                    XSheet.Name = title.TrimEnd().Substring(0, 30);
                }
                else
                {
                    XSheet.Name = title.TrimEnd();
                }

                // Start Row/Column reset.
                iRestRows = 3;
                iRestCols = 2;
                iColumnCnt = 0;

                if (actSheet.RowHeader.Visible == false || actSheet.RowHeader.ColumnCount <= 0)
                {
                    iRowHeaderCount = 0;
                }
                else
                {
                    iRowHeaderCount = actSheet.RowHeader.ColumnCount;
                }
                iColumnCnt = iRowHeaderCount;

                if (actSheet.ColumnHeader.Visible == false || actSheet.ColumnHeader.RowCount <= 0)
                {
                    iColHeaderCount = 0;
                }
                else
                {
                    iColHeaderCount = actSheet.ColumnHeader.RowCount;
                }


                //'지정한 컬럼만 Export 할 경우가 아닌 경우
                if (iStartCol < 0)
                {
                    iStartCol = 0;
                }

                if (iEndCol < 0)
                {
                    iEndCol = actSheet.ColumnCount;
                    if (iEndCol > XSheet.Columns.Count)
                    {
                        // Excel 최대 Column이면 시작 Column을 제외한다.
                        iEndCol = XSheet.Columns.Count - iRestCols;
                    }
                    iEndCol--;
                }

                // 데이터 조회조건에 대한 설명 또는 추가정보 설정.
                condition = condition.TrimEnd(new char[] { ' ', '\r', '\n' });
                if (condition.Length > 0)
                {
                    string[] sTempStr = condition.Split(new char[] { '\r', '\n' });
                    iRestRows += (sTempStr.Length + 1);
                }

                //lImage, int imagePostion,
                if (imageFile != null)
                {
                    int importRows = iRestRows;

                    if (imagePostion == 2) // Image is bottom.(이미지가 아래에 있으면,...)
                    {
                        importRows += actSheet.RowCount + iColHeaderCount + 1;
                    }

                    importRows = InsertImageToExcel((Excel._Worksheet)XSheet, imageFile, importRows, iRestCols);
                    if (imagePostion == 1 && importRows > 0) // Image is Top position.(이미지가 위에 있으면,...)
                    {
                        iRestRows += importRows + 1;
                    }
                }


                //First Column Width
                XSheet.get_Range(XSheet.Cells[1, 1], XSheet.Cells[XSheet.Rows.Count, 1]).EntireColumn.ColumnWidth = 0.5;
                XSheet.get_Range(XSheet.Cells[1, 1], XSheet.Cells[XSheet.Rows.Count, ExcelComponent.EXCEL_MAX_COL]).Font.Size = 10;

                //'셀서식을 TEXT로 인식하도록 한다.
                //'단, 셀 서식이 Text일 경우에는 셀 병합할 때 확인 창이 뜬다.
                if (bTextBase == true)
                {
                    XSheet.get_Range(XSheet.Cells[1, 1], XSheet.Cells[XSheet.Rows.Count, ExcelComponent.EXCEL_MAX_COL]).NumberFormat = "@";
                }


                for (i = iStartCol; i <= iEndCol; i++)
                {
                    if (actSheet.Columns[i].Width > 0 && actSheet.Columns[i].Visible == true)
                    {
                        bAddColumn = true;
                        for (j = 0; j < iColHeaderCount; j++)
                        {
                            if (actSheet.ColumnHeader.Cells[j, i].CellType is FarPoint.Win.Spread.CellType.ButtonCellType)
                            {
                                bAddColumn = false;
                                break;
                            }
                            else if (actSheet.ColumnHeader.Cells[j, i].CellType is FarPoint.Win.Spread.CellType.EmptyCellType)
                            {
                                bAddColumn = false;
                                break;
                            }
                            else if (actSheet.ColumnHeader.Cells[j, i].CellType is FarPoint.Win.Spread.CellType.ProgressCellType)
                            {
                                bAddColumn = false;
                                break;
                            }
                            else if (actSheet.ColumnHeader.Cells[j, i].CellType is FarPoint.Win.Spread.CellType.SliderCellType)
                            {
                                bAddColumn = false;
                                break;
                            }
                        }

                        if (bAddColumn == true)
                        {
                            if (aColumns == null)
                            {
                                aColumns = new int[iColumnCnt + 1];
                            }
                            else
                            {
                                int[] tempArray = new int[iColumnCnt];

                                Array.Copy(aColumns, tempArray, iColumnCnt);
                                aColumns = new int[iColumnCnt + 1];
                                Array.Copy(tempArray, aColumns, iColumnCnt);
                            }
                            aColumns[iColumnCnt] = i;
                            iColumnCnt++;
                        }
                    }
                }


                if (iColHeaderCount > 0)
                {
                    // Column Header name 및 Cell 서식 설정.
                    for (j = 0; j < iColHeaderCount; j++)
                    {
                        string thisHeader;
                        string[] sColumnHeader;

                        thisHeader = actSheet.ColumnHeader.GetClip(j, 0, 1, -1);
                        sColumnHeader = thisHeader.TrimEnd(new char[] { '\n', '\r' }).Split('\t');

                        clipboardData = "";
                        for (i = 0; i < iColumnCnt; i++)
                        {
                            if (iRowHeaderCount > i)
                            {
                                clipboardData += " " + "\t";
                            }
                            else
                            {
                                thisHeader = "";
                                if (aColumns[i] < sColumnHeader.Length)
                                {
                                    thisHeader = sColumnHeader[aColumns[i]];
                                }

                                clipboardData += thisHeader + "\t";
                                if (actSheet.ColumnHeader.Cells[j, aColumns[i]].ColumnSpan > 1)
                                {
                                    for (k = 0; k <= actSheet.ColumnHeader.Cells[j, aColumns[i]].ColumnSpan - 2; k++)
                                    {
                                        clipboardData += " " + "\t";
                                    }
                                    i += actSheet.ColumnHeader.Cells[j, aColumns[i]].ColumnSpan - 1;
                                }
                            }
                        }

                        clipboardData = clipboardData + "\n";
                        Clipboard.SetDataObject(clipboardData);
                        XSheet.get_Range("B" + System.Convert.ToString(iRestRows + j), Type.Missing).Select();
                        XSheet._PasteSpecial(Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing);
                    }

                    XApp.DisplayAlerts = false;

                    // Column Heaer의 Merge를 설정.
                    for (j = 0; j < iColHeaderCount; j++)
                    {
                        for (k = 0; k < iColumnCnt; k++)
                        {
                            i = aColumns[k];
                            if (iRowHeaderCount > k)
                            {
                                k += iRowHeaderCount - 1;
                            }
                            else
                            {
                                if (actSheet.ColumnHeader.Cells[j, i].ColumnSpan > 1)
                                {
                                    xWiths = XSheet.get_Range(XSheet.Cells[j + iRestRows, k + iRestCols],
                                            XSheet.Cells[j + iRestRows, k + iRestCols + actSheet.ColumnHeader.Cells[j, i].ColumnSpan - 1]);
                                    //xWiths.Borders[XlBordersIndex.xlEdgeRight].LineStyle = ExcelComponent.XlLineStyle.xlContinuous;
                                    xWiths.Select();
                                    xWiths.Merge(Type.Missing);

                                    k += actSheet.ColumnHeader.Cells[j, i].ColumnSpan - 1;
                                }

                                if (actSheet.ColumnHeader.Cells[j, i].RowSpan > 1)
                                {
                                    xWiths = XSheet.get_Range(XSheet.Cells[j + iRestRows, k + iRestCols],
                                            XSheet.Cells[j + iRestRows + actSheet.ColumnHeader.Cells[j, i].RowSpan - 1, k + iRestCols]);
                                    //xWiths.Borders[XlBordersIndex.xlEdgeRight].LineStyle = ExcelComponent.XlLineStyle.xlContinuous;
                                    xWiths.Select();
                                    xWiths.Merge(Type.Missing);
                                }
                            }
                        }
                    }


                    // Column Header 에 대한 셀서식설정.
                    xWiths = XSheet.get_Range(XSheet.Cells[iRestRows, iRestCols], XSheet.Cells[iRestRows + iColHeaderCount - 1, iRestCols + iColumnCnt - 1]);
                    xWiths.HorizontalAlignment = ExcelComponent.XlHAlign.xlHAlignCenter; //-4108
                    xWiths.VerticalAlignment = ExcelComponent.XlVAlign.xlValignCenter; //-4108
                    xWiths.Interior.ColorIndex = 15;
                    xWiths.Font.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.Black);
                    xWiths.Font.Bold = true;
                    xWiths.Borders[XlBordersIndex.xlEdgeLeft].LineStyle = ExcelComponent.XlLineStyle.xlContinuous;
                    xWiths.Borders[XlBordersIndex.xlEdgeRight].LineStyle = ExcelComponent.XlLineStyle.xlContinuous;
                    xWiths.Borders[XlBordersIndex.xlEdgeTop].LineStyle = ExcelComponent.XlLineStyle.xlContinuous;
                    xWiths.Borders[XlBordersIndex.xlEdgeBottom].LineStyle = ExcelComponent.XlLineStyle.xlContinuous;
                    if (xWiths.Rows.Count > 1)
                    {
                        xWiths.Borders[XlBordersIndex.xlInsideHorizontal].LineStyle = ExcelComponent.XlLineStyle.xlContinuous;
                    }

                    if (xWiths.Columns.Count > 1)
                    {
                        xWiths.Borders[XlBordersIndex.xlInsideVertical].LineStyle = ExcelComponent.XlLineStyle.xlContinuous;
                    }

                    // Data 시작 위치 재설정.
                    iRestRows += iColHeaderCount;
                }

                //if (sheet.RowHeader.Visible == true && sheet.RowHeader.ColumnCount > 0)
                if (iRowHeaderCount > 0)
                {
                    //'Row Header 셀서식
                    xWiths = XSheet.get_Range(XSheet.Cells[iRestRows, iRestCols], XSheet.Cells[iRestRows + actSheet.RowCount - 1, iRestCols + iRowHeaderCount - 1]);
                    xWiths.HorizontalAlignment = ExcelComponent.XlHAlign.xlHAlignCenter; //-4108
                    xWiths.VerticalAlignment = ExcelComponent.XlVAlign.xlValignCenter; //-4108
                    xWiths.Interior.ColorIndex = 15;
                    xWiths.Font.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.Black);
                    xWiths.Font.Bold = true;
                    xWiths.Borders[XlBordersIndex.xlEdgeLeft].LineStyle = ExcelComponent.XlLineStyle.xlContinuous;
                    xWiths.Borders[XlBordersIndex.xlEdgeRight].LineStyle = ExcelComponent.XlLineStyle.xlContinuous;
                    xWiths.Borders[XlBordersIndex.xlEdgeTop].LineStyle = ExcelComponent.XlLineStyle.xlContinuous;
                    xWiths.Borders[XlBordersIndex.xlEdgeBottom].LineStyle = ExcelComponent.XlLineStyle.xlContinuous;
                    if (xWiths.Rows.Count > 1)
                    {
                        xWiths.Borders[XlBordersIndex.xlInsideHorizontal].LineStyle = ExcelComponent.XlLineStyle.xlContinuous;
                    }
                }

                // Spread 에 대한 셀서식
                if (actSheet.RowCount > 0)
                {
                    xWiths = XSheet.get_Range(XSheet.Cells[iRestRows, iRestCols + iRowHeaderCount], XSheet.Cells[iRestRows + actSheet.RowCount - 1, iRestCols + iColumnCnt - 1]);
                    xWiths.Borders[XlBordersIndex.xlEdgeLeft].LineStyle = ExcelComponent.XlLineStyle.xlContinuous;
                    xWiths.Borders[XlBordersIndex.xlEdgeRight].LineStyle = ExcelComponent.XlLineStyle.xlContinuous;
                    xWiths.Borders[XlBordersIndex.xlEdgeBottom].LineStyle = ExcelComponent.XlLineStyle.xlContinuous;
                    if (xWiths.Columns.Count > 1)
                    {
                        xWiths.Borders[XlBordersIndex.xlInsideVertical].LineStyle = ExcelComponent.XlLineStyle.xlContinuous;
                        xWiths.Borders[XlBordersIndex.xlInsideVertical].Weight = ExcelComponent.XlBorderWeight.xlHairline;
                    }

                    if (xWiths.Rows.Count > 1)
                    {
                        xWiths.Borders[XlBordersIndex.xlInsideHorizontal].LineStyle = ExcelComponent.XlLineStyle.xlContinuous;
                        xWiths.Borders[XlBordersIndex.xlInsideHorizontal].Weight = ExcelComponent.XlBorderWeight.xlHairline;
                    }
                }

                for (i = 0; i < actSheet.RowCount; i++)
                {
                    clipboardData = "";
                    for (j = 0; j < iColumnCnt; j++)
                    {
                        try
                        {
                            if (iRowHeaderCount > j)
                            {
                                clipboardData += actSheet.RowHeader.Cells[i, j].Text.Trim().Replace(" ", "").Replace("\r", "   ").Replace("\n", "  ").Replace("\t", " ") + "\t";
                            }
                            else
                            {
                                if (actSheet.Cells[i, aColumns[j]].Text.IndexOf("<") > 0 || actSheet.Cells[i, aColumns[j]].Text.IndexOf(">") > 0)
                                {
                                    clipboardData += actSheet.Cells[i, aColumns[j]].Text.Trim().Replace(" ", "").Replace("\r", "   ").Replace("\n", "  ").Replace("\t", " ") + "\t";
                                }
                                else
                                {
                                    clipboardData += actSheet.Cells[i, aColumns[j]].Text.Replace("\r", "   ").Replace("\n", "  ").Replace("\t", " ") + "\t";
                                }
                            }
                        }
                        catch
                        {
                            clipboardData += "\t";
                        }
                    }

                    clipboardData = clipboardData + "\n";
                    Clipboard.SetDataObject(clipboardData);
                    XSheet.get_Range("B" + System.Convert.ToString(iRestRows + i), Type.Missing).Select();
                    //XSheet._PasteSpecial(Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing);
                    XSheet._PasteSpecial(Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing);
                }

                // Column크기를 자동으로 조절.
                xWiths = XSheet.get_Range(XSheet.Cells[iRestRows - iColHeaderCount, iRestCols], XSheet.Cells[iRestRows + actSheet.RowCount - 1, iRestCols + iColumnCnt - 1]);
                xWiths.CurrentRegion.EntireColumn.AutoFit();

                if (colorFlag == true)
                {
                    for (i = 0; i < actSheet.RowCount; i++)
                    {
                        for (j = iStartCol; j <= iEndCol; j++)
                        {
                            if (actSheet.Cells[i, j].ForeColor.IsEmpty == false)
                            {
                                if (actSheet.Cells[i, j].ForeColor.ToArgb() != System.Drawing.Color.Black.ToArgb())
                                {
                                    XSheet.get_Range(XSheet.Cells[i + iRestRows, j + iRestCols + iRowHeaderCount], XSheet.Cells[i + iRestRows, j + iRestCols + iRowHeaderCount]).Font.Color =
                                        System.Drawing.ColorTranslator.ToOle(actSheet.Cells[i, j].ForeColor);
                                }
                            }

                            if (actSheet.Cells[i, j].BackColor.IsEmpty == false)
                            {
                                if (actSheet.Cells[i, j].BackColor.ToArgb() != System.Drawing.Color.White.ToArgb())
                                {
                                    XSheet.get_Range(XSheet.Cells[i + iRestRows, j + iRestCols + iRowHeaderCount], XSheet.Cells[i + iRestRows, j + iRestCols + iRowHeaderCount]).Interior.Color =
                                        System.Drawing.ColorTranslator.ToOle(actSheet.Cells[i, j].BackColor);
                                }
                            }
                        }
                    }

                }

                //'Title 에 대한 셀 서식 지정
                xWiths = XSheet.get_Range(XSheet.Cells[1, 2], XSheet.Cells[1, 2]);
                xWiths.VerticalAlignment = ExcelComponent.XlVAlign.xlValignCenter;
                xWiths.RowHeight = 30;
                xWiths.Font.Size = 12;
                xWiths.Font.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.Black);
                xWiths.Font.Bold = true;
                xWiths.Font.Name = "Arail";
                ((Range)XSheet.Cells[1, 2]).Value2 = title;

                if (bShowDate == true)
                {
                    string sNowDate;
                    sNowDate = string.Format(Convert.ToString(DateTime.Now), "yyyy-MM-dd") + " " + string.Format(Convert.ToString(DateTime.Now), "HH:mm:ss");
                    ((Range)XSheet.Cells[2, 2]).Value2 = "\'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                }

                if (condition.Length > 0)
                {
                    Clipboard.SetDataObject(condition);
                    ((Range)XSheet.Cells[3, 2]).Select();
                    XSheet._PasteSpecial(Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing);
                }

                XApp.DisplayAlerts = true;
                ((Range)XSheet.Cells[2, 2]).Select();
                XApp.Visible = true;

                return true;

            }
            catch (Exception ex)
            {
                //FwxCmnFunction.ShowMsgBox(ex.Message, "Error", MessageBoxButtons.OK, 1);
                CmnFunction.ShowMsgBox(ex.ToString(), "Error", MessageBoxButtons.OK, 1);
                return false;
            }
            finally
            {

                System.Runtime.InteropServices.Marshal.ReleaseComObject(XSheet);
                System.Runtime.InteropServices.Marshal.ReleaseComObject(XBook);
                System.Runtime.InteropServices.Marshal.ReleaseComObject(XBooks);
                System.Runtime.InteropServices.Marshal.ReleaseComObject(XApp);
                GC.Collect();
                Cursor.Current = Cursors.Default;

            }

        }

        // SpreadSheetToExcel()
        //       - Export Data of Spread Sheet Control to Excel Application Data
        // Return Value
        //       - True or False
        private static bool SpreadSheetToExcel(FarPoint.Win.Spread.SheetView[] xSheet,
                string imageFile, int imagePostion,
                string title, string condition,
                bool colorFlag, bool bTextBase, bool bShowDate,
                int iStartCol, int iEndCol, int iRowHeight,
                bool bOnlyData, float[] pageOptions)
        {

            Excel.Application xlApp = new Excel.Application();
            Excel.Workbooks xlBooks = xlApp.Workbooks;
            Excel.Workbook xlBook = xlBooks.Add(Excel.XlWBATemplate.xlWBATWorksheet);
            Excel.Worksheet xlSheet = (Excel.Worksheet)xlBook.ActiveSheet;
            Excel.Range xlWiths;


            Int32 i, j, k;
            int startColumn, endColumn;
            int[] aColumns = null;
            int iRestRows, iRestCols, iColumnCnt;
            int pastColumn, pastColumnCount, pastColumnIdx;
            string clipboardData;
            bool bAddColumn, bReturn = true;


            try
            {
                int iRowHeaderCount, iColHeaderCount;

                System.Windows.Forms.Application.DoEvents();
                Cursor.Current = Cursors.WaitCursor;

                // Sheet Naet init.
                if (title.Length > 0)
                {
                    string sheetTile = "";
                    sheetTile = title.Replace(" ", "").Replace(":", ".").Replace("?", ".").Replace("*", ".").Replace("[", ".").Replace("]", ".").Replace("<", ".").Replace(">", ".");
                    xlSheet.Name = sheetTile;
                }

                // Start Row/Column reset init.
                iColumnCnt = 0;
                iRowHeaderCount = 0;
                iColHeaderCount = 0;
                startColumn = 0;
                endColumn = 0;
                // Spread Data만 Export할 경우 Row/Column Header를 표시하지 않음.
                if (bOnlyData)
                {
                    iRestRows = EXCEL_FIRST_ROWCOL;
                    iRestCols = EXCEL_FIRST_ROWCOL;
                }
                else
                {
                    iRestRows = EXCEL_START_ROW;
                    iRestCols = EXCEL_START_COL;
                }

                // Spread Data만 Export할 경우 Image를 표시하지 않음.
                if (bOnlyData == false)
                {
                    // 데이터 조회조건에 대한 설명 또는 추가정보 설정.
                    condition = condition.TrimEnd(new char[] { ' ', '\r', '\n' });
                    if (condition.Length > 0)
                    {
                        string[] sTempStr = condition.Split(new char[] { '\r', '\n' });
                        iRestRows += (sTempStr.Length + 1);
                    }
                }

                //First Column Font/Width init.
                xlSheet.Cells.Select();
                xlSheet.Cells.EntireColumn.ColumnWidth = 1;
                xlSheet.Cells.Font.Name = "Tahoma";
                xlSheet.Cells.Font.Size = 10;
                ////  텍스트가 사용 가능한 열 너비에 맞게 자동으로 축소 여부.
                //xlSheet.Cells.ShrinkToFit = true;

                // 이미지가 상단에 위치할 경우(만약, imagePostion == 0이면 이미지를 표시하지 않음)
                // Spread Data만 Export할 경우 Image를 표시하지 않음.
                if (bOnlyData == false && imageFile != null && imagePostion == 1)
                {
                    int importRows = iRestRows;

                    importRows = InsertImageToExcel((Excel._Worksheet)xlSheet, imageFile, importRows, iRestCols);
                    if (importRows > 0) // Image is Top position.
                    {
                        iRestRows += importRows + 2;
                    }
                }

                // 각종 엑셀의 경고메시지를 보이지 않도록 합니다.
                xlApp.DisplayAlerts = false;

                // 셀서식을 TEXT로 인식하도록 한다.
                // 단, 셀 서식이 Text일 경우에는 셀 병합할 때 확인 창이 뜬다.
                if (bTextBase == true)
                {
                    xlSheet.Cells.NumberFormat = "@";
                }


                // 여러장의 분할된 Sread Sheet를 하나의 Excel에 붙여넣음.
                foreach (FarPoint.Win.Spread.SheetView activeSheet in xSheet)
                {
                    if (activeSheet.RowCount <= 0)
                    {
                        continue;
                    }

                    // Row Header count.
                    if (activeSheet.RowHeader.Visible == false || activeSheet.RowHeader.ColumnCount <= 0)
                    {
                        iRowHeaderCount = 0;
                    }
                    else
                    {
                        iRowHeaderCount = activeSheet.RowHeader.ColumnCount;
                    }

                    // Column Header count.
                    if (activeSheet.ColumnHeader.Visible == false || activeSheet.ColumnHeader.RowCount <= 0)
                    {
                        iColHeaderCount = 0;
                    }
                    else
                    {
                        iColHeaderCount = activeSheet.ColumnHeader.RowCount;
                    }

                    // 지정한 컬럼만 Export 할 경우가 아닌 경우
                    if (iStartCol < 0)
                    {
                        startColumn = 0;
                    }

                    if (iEndCol < 0)
                    {
                        endColumn = activeSheet.ColumnCount;
                        if (endColumn > xlSheet.Columns.Count)
                        {
                            // Excel 최대 Column이면 시작 Column을 제외한다.
                            endColumn = xlSheet.Columns.Count - iRestCols;
                        }
                    }

                    // Export target cell column number.(저장 대상 셀을 찾는다.)
                    iColumnCnt = 0;
                    for (i = startColumn; i < endColumn; i++)
                    {
                        if (activeSheet.Columns[i].Width > 0 && activeSheet.Columns[i].Visible == true)
                        {
                            bAddColumn = true;
                            for (j = 0; j < iColHeaderCount; j++)
                            {
                                if (activeSheet.ColumnHeader.Cells[j, i].CellType is FarPoint.Win.Spread.CellType.ButtonCellType)
                                {
                                    bAddColumn = false;
                                    break;
                                }
                                else if (activeSheet.ColumnHeader.Cells[j, i].CellType is FarPoint.Win.Spread.CellType.EmptyCellType)
                                {
                                    bAddColumn = false;
                                    break;
                                }
                                else if (activeSheet.ColumnHeader.Cells[j, i].CellType is FarPoint.Win.Spread.CellType.ProgressCellType)
                                {
                                    bAddColumn = false;
                                    break;
                                }
                                else if (activeSheet.ColumnHeader.Cells[j, i].CellType is FarPoint.Win.Spread.CellType.SliderCellType)
                                {
                                    bAddColumn = false;
                                    break;
                                }
                            }

                            if (bAddColumn == true)
                            {
                                if (aColumns == null)
                                {
                                    aColumns = new int[iColumnCnt + 1];
                                }
                                else
                                {
                                    int[] tempArray = new int[iColumnCnt];

                                    Array.Copy(aColumns, tempArray, iColumnCnt);
                                    aColumns = new int[iColumnCnt + 1];
                                    Array.Copy(tempArray, aColumns, iColumnCnt);
                                }
                                aColumns[iColumnCnt] = i;
                                iColumnCnt++;
                            }
                        }
                    }

                    // Spread Data만 Export할 경우 Row/Column Header를 표시하지 않음.
                    if (bOnlyData == false && iColHeaderCount > 0)
                    {
                        int spanCount; // Row/Column span count.

                        // Column Header name 및 Cell 서식 설정.
                        for (j = 0; j < iColHeaderCount; j++)
                        {
                            string thisHeader;
                            string[] sColumnHeader;

                            thisHeader = activeSheet.ColumnHeader.GetClip(j, 0, 1, -1);
                            sColumnHeader = thisHeader.TrimEnd(new char[] { '\n', '\r' }).Split('\t');

                            clipboardData = "";
                            for (i = 0; i < iColumnCnt; i++)
                            {
                                thisHeader = "";
                                if (aColumns[i] < sColumnHeader.Length)
                                {
                                    thisHeader = sColumnHeader[aColumns[i]];
                                }

                                clipboardData += thisHeader + "\t";
                                spanCount = activeSheet.ColumnHeader.Cells[j, aColumns[i]].ColumnSpan;
                                if (spanCount > 1)
                                {
                                    for (k = 0; k <= (spanCount - 2); k++)
                                    {
                                        clipboardData += " " + "\t";
                                    }
                                    i += spanCount - 1;
                                }
                            }

                            clipboardData = clipboardData + "\n";
                            Clipboard.SetDataObject((object)clipboardData, false, 5, 10);
                            ((Excel.Range)xlSheet.Cells[(iRestRows + j), (iRestCols + iRowHeaderCount)]).Select();
                            //xlSheet.PasteSpecial(Type.Missing, false, false, Type.Missing, Type.Missing, Type.Missing, Type.Missing);
                            xlSheet._PasteSpecial(Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing);
                        }


                        // Column Heaer의 Merge를 설정.
                        for (j = 0; j < iColHeaderCount; j++)
                        {
                            for (k = 0; k < iColumnCnt; k++)
                            {
                                i = aColumns[k];
                                if (iRowHeaderCount > k)
                                {
                                    k += iRowHeaderCount - 1;
                                }
                                else
                                {
                                    spanCount = activeSheet.ColumnHeader.Cells[j, i].ColumnSpan;
                                    if (spanCount > 1)
                                    {
                                        xlWiths = xlSheet.get_Range(xlSheet.Cells[j + iRestRows, k + iRestCols],
                                                xlSheet.Cells[j + iRestRows, k + iRestCols + spanCount - 1]);
                                        //xlWiths.Borders[Excel.XlBordersIndex.xlEdgeRight].LineStyle = ExcelComponent.XlLineStyle.xlContinuous;
                                        xlWiths.Select();
                                        xlWiths.Merge(Type.Missing);

                                        k += spanCount - 1;
                                    }

                                    spanCount = activeSheet.ColumnHeader.Cells[j, i].RowSpan;
                                    if (spanCount > 1 && spanCount <= (iColHeaderCount - j))
                                    {
                                        xlWiths = xlSheet.get_Range(xlSheet.Cells[j + iRestRows, k + iRestCols],
                                                xlSheet.Cells[j + iRestRows + spanCount - 1, k + iRestCols]);
                                        //xlWiths.Borders[Excel.XlBordersIndex.xlEdgeRight].LineStyle = ExcelComponent.XlLineStyle.xlContinuous;
                                        xlWiths.Select();
                                        xlWiths.Merge(Type.Missing);
                                    }
                                }
                            }
                        }


                        // Column Header 에 대한 셀서식설정.
                        xlWiths = xlSheet.get_Range(xlSheet.Cells[iRestRows, iRestCols], xlSheet.Cells[iRestRows + iColHeaderCount - 1, iRestCols + iRowHeaderCount + iColumnCnt - 1]);
                        xlWiths.HorizontalAlignment = ExcelComponent.XlHAlign.xlHAlignCenter; //-4108
                        xlWiths.VerticalAlignment = ExcelComponent.XlVAlign.xlValignCenter; //-4108
                        xlWiths.Interior.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.DodgerBlue);
                        xlWiths.Font.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.White);
                        xlWiths.Font.Bold = true;
                        xlWiths.Borders[Excel.XlBordersIndex.xlEdgeLeft].LineStyle = ExcelComponent.XlLineStyle.xlContinuous;
                        xlWiths.Borders[Excel.XlBordersIndex.xlEdgeRight].LineStyle = ExcelComponent.XlLineStyle.xlContinuous;
                        xlWiths.Borders[Excel.XlBordersIndex.xlEdgeTop].LineStyle = ExcelComponent.XlLineStyle.xlContinuous;
                        xlWiths.Borders[Excel.XlBordersIndex.xlEdgeBottom].LineStyle = ExcelComponent.XlLineStyle.xlContinuous;
                        if (xlWiths.Rows.Count > 1)
                        {
                            xlWiths.Borders[Excel.XlBordersIndex.xlInsideHorizontal].LineStyle = ExcelComponent.XlLineStyle.xlContinuous;
                        }

                        if (xlWiths.Columns.Count > 1)
                        {
                            xlWiths.Borders[Excel.XlBordersIndex.xlInsideVertical].LineStyle = ExcelComponent.XlLineStyle.xlContinuous;
                        }

                        // Data 시작 위치 재설정.
                        iRestRows += iColHeaderCount;
                    }

                    #region Set RowHeader/Rows cell formula.
                    // Set RowHeaders cell formula.
                    if (iRowHeaderCount > 0)
                    {
                        int spanCount, skipColumn; // Row/Column span count.
                        string rowLavel;

                        // Row Heaer의 Merge를 설정.
                        for (j = 0; j < activeSheet.RowCount; j++)
                        {
                            for (k = 0; k < iRowHeaderCount; k++)
                            {
                                skipColumn = 0;
                                // Set Row Header Text.
                                rowLavel = activeSheet.GetRowLabel(j, k).ToString().Trim();
                                ((Excel.Range)xlSheet.Cells[j + iRestRows, k + iRestCols]).Value2 = rowLavel;

                                spanCount = activeSheet.RowHeader.Cells[j, k].ColumnSpan;
                                if (spanCount > 1 && spanCount <= (iRowHeaderCount - k))
                                {
                                    xlWiths = xlSheet.get_Range(xlSheet.Cells[j + iRestRows, k + iRestCols],
                                            xlSheet.Cells[j + iRestRows, k + iRestCols + spanCount - 1]);
                                    //xlWiths.Borders[Excel.XlBordersIndex.xlEdgeRight].LineStyle = ExcelComponent.XlLineStyle.xlContinuous;
                                    xlWiths.Select();
                                    xlWiths.Merge(Type.Missing);

                                    skipColumn = spanCount - 1;
                                }

                                spanCount = activeSheet.RowHeader.Cells[j, k].RowSpan;
                                if (spanCount > 1)
                                {
                                    xlWiths = xlSheet.get_Range(xlSheet.Cells[j + iRestRows, k + iRestCols],
                                            xlSheet.Cells[j + iRestRows + spanCount - 1, k + iRestCols]);
                                    //xlWiths.Borders[Excel.XlBordersIndex.xlEdgeRight].LineStyle = ExcelComponent.XlLineStyle.xlContinuous;
                                    xlWiths.Select();
                                    xlWiths.Merge(Type.Missing);
                                }

                                k += skipColumn;
                            }
                        }

                        //'Row Header 셀서식
                        xlWiths = xlSheet.get_Range(xlSheet.Cells[iRestRows, iRestCols], xlSheet.Cells[iRestRows + activeSheet.RowCount - 1, iRestCols + iRowHeaderCount - 1]);
                        xlWiths.HorizontalAlignment = ExcelComponent.XlHAlign.xlHAlignCenter; //-4108
                        xlWiths.VerticalAlignment = ExcelComponent.XlVAlign.xlValignCenter; //-4108
                        xlWiths.Interior.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.DodgerBlue);
                        xlWiths.Font.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.White);
                        xlWiths.Font.Bold = true;
                        xlWiths.Borders[Excel.XlBordersIndex.xlEdgeLeft].LineStyle = ExcelComponent.XlLineStyle.xlContinuous;
                        xlWiths.Borders[Excel.XlBordersIndex.xlEdgeRight].LineStyle = ExcelComponent.XlLineStyle.xlContinuous;
                        xlWiths.Borders[Excel.XlBordersIndex.xlEdgeTop].LineStyle = ExcelComponent.XlLineStyle.xlContinuous;
                        xlWiths.Borders[Excel.XlBordersIndex.xlEdgeBottom].LineStyle = ExcelComponent.XlLineStyle.xlContinuous;
                        if (xlWiths.Rows.Count > 1)
                        {
                            xlWiths.Borders[Excel.XlBordersIndex.xlInsideHorizontal].LineStyle = ExcelComponent.XlLineStyle.xlContinuous;
                        }

                        if (xlWiths.Columns.Count > 1)
                        {
                            xlWiths.Borders[Excel.XlBordersIndex.xlInsideVertical].LineStyle = ExcelComponent.XlLineStyle.xlContinuous;
                        }

                        // Data Start Column position reset.
                        //iRestCols += iRowHeaderCount;
                    }

                    // Set Rows cell formula.
                    if (activeSheet.RowCount > 0)
                    {
                        xlWiths = xlSheet.get_Range(xlSheet.Cells[iRestRows, iRestCols + iRowHeaderCount], xlSheet.Cells[iRestRows + activeSheet.RowCount - 1, iRestCols + iRowHeaderCount + iColumnCnt - 1]);
                        xlWiths.Borders[Excel.XlBordersIndex.xlEdgeLeft].LineStyle = ExcelComponent.XlLineStyle.xlContinuous;
                        xlWiths.Borders[Excel.XlBordersIndex.xlEdgeRight].LineStyle = ExcelComponent.XlLineStyle.xlContinuous;
                        xlWiths.Borders[Excel.XlBordersIndex.xlEdgeBottom].LineStyle = ExcelComponent.XlLineStyle.xlContinuous;
                        if (xlWiths.Columns.Count > 1)
                        {
                            xlWiths.Borders[Excel.XlBordersIndex.xlInsideVertical].LineStyle = ExcelComponent.XlLineStyle.xlContinuous;
                            xlWiths.Borders[Excel.XlBordersIndex.xlInsideVertical].Weight = ExcelComponent.XlBorderWeight.xlHairline;
                        }

                        if (xlWiths.Rows.Count > 1)
                        {
                            xlWiths.Borders[Excel.XlBordersIndex.xlInsideHorizontal].LineStyle = ExcelComponent.XlLineStyle.xlContinuous;
                            xlWiths.Borders[Excel.XlBordersIndex.xlInsideHorizontal].Weight = ExcelComponent.XlBorderWeight.xlHairline;
                        }
                    }
                    #endregion

                    #region Get Spread all values.(Hidden column skip).
                    pastColumn = 0;
                    pastColumnCount = 0;
                    pastColumnIdx = iRestCols + iRowHeaderCount;
                    for (i = 0; i < iColumnCnt; i++)
                    {
                        if (aColumns[i] == (pastColumn + pastColumnCount))
                        {
                            pastColumnCount++;
                            continue;
                        }
                        else
                        {
                            if (pastColumnCount > 0)
                            {
                                clipboardData = activeSheet.GetClip(0, pastColumn, -1, pastColumnCount);
                                Clipboard.SetDataObject((object)clipboardData, false, 5, 10);
                                ((Excel.Range)xlSheet.Cells[iRestRows, pastColumnIdx]).Select();
                                //xlSheet.PasteSpecial(Type.Missing, false, false, Type.Missing, Type.Missing, Type.Missing, Type.Missing);
                                xlSheet._PasteSpecial(Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing);

                                pastColumnIdx += pastColumnCount;
                            }

                            pastColumn = aColumns[i];
                            pastColumnCount = 1;
                        }
                    }

                    if (pastColumnCount > 0)
                    {
                        clipboardData = activeSheet.GetClip(0, pastColumn, -1, pastColumnCount);
                        Clipboard.SetDataObject((object)clipboardData, false, 5, 10);
                        ((Excel.Range)xlSheet.Cells[iRestRows, pastColumnIdx]).Select();
                        //xlSheet.PasteSpecial(Type.Missing, false, false, Type.Missing, Type.Missing, Type.Missing, Type.Missing);
                        xlSheet._PasteSpecial(Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing);
                    }

                    // Column크기를 자동으로 조절(Row and Column header).
                    xlWiths = xlSheet.get_Range(xlSheet.Cells[(iRestRows - iColHeaderCount > 0 ? iRestRows - iColHeaderCount : iRestRows), iRestCols],
                                xlSheet.Cells[iRestRows + activeSheet.RowCount - 1, iRestCols + iRowHeaderCount + iColumnCnt - 1]);
                    xlWiths.EntireColumn.AutoFit();
                    // Row 크기를 조절.
                    if (iRowHeight > 0)
                    {
                        //Keep the minimum row height to 13.
                        if (iRowHeight < 13)
                        {
                            iRowHeight = 13;
                        }
                        xlWiths.EntireRow.RowHeight = iRowHeight;
                    }
                    #endregion

                    // 각 Column의 Background/ForeColor를 설정함.
                    if (colorFlag == true)
                    {
                        for (i = 0; i < activeSheet.RowCount; i++)
                        {
                            for (j = startColumn; j < endColumn; j++)
                            {
                                if (activeSheet.Cells[i, j].ForeColor.IsEmpty == false)
                                {
                                    if (activeSheet.Cells[i, j].ForeColor.ToArgb() != System.Drawing.Color.Black.ToArgb())
                                    {
                                        ((Excel.Range)xlSheet.Cells[i + iRestRows, j + iRestCols + iRowHeaderCount]).Font.Color =
                                            System.Drawing.ColorTranslator.ToOle(activeSheet.Cells[i, j].ForeColor);
                                    }
                                }

                                if (activeSheet.Cells[i, j].BackColor.IsEmpty == false)
                                {
                                    if (activeSheet.Cells[i, j].BackColor.ToArgb() != System.Drawing.Color.White.ToArgb())
                                    {
                                        ((Excel.Range)xlSheet.Cells[i + iRestRows, j + iRestCols + iRowHeaderCount]).Interior.Color =
                                            System.Drawing.ColorTranslator.ToOle(activeSheet.Cells[i, j].BackColor);
                                    }
                                }
                            }
                        }
                    }

                    iRestRows += activeSheet.RowCount;
                    // 데이터만 export(bOnlyData == true)할 경우엔 Spread별로 구분하는 Row를 넣지 않음.
                    if (bOnlyData == false)
                    {
                        iRestRows++;
                    }
                }

                // Spread Data만 Export할 경우 Page Setup.
                if (pageOptions != null && pageOptions.Length == 12)
                {
                    InsertPrintOption(xlSheet, pageOptions);
                }

                // Spread Data만 Export할 경우 Title, Row/Column Header, Conditions, Current Date를 표시하지 않음.
                if (bOnlyData == false)
                {
                    // Title 에 대한 셀 서식 지정
                    xlWiths = xlSheet.get_Range(xlSheet.Cells[1, iRestCols], xlSheet.Cells[1, iRestCols]);
                    xlWiths.VerticalAlignment = ExcelComponent.XlVAlign.xlValignCenter;
                    xlWiths.RowHeight = 30;
                    xlWiths.Font.Size = 18;
                    xlWiths.Font.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.Black);
                    xlWiths.Font.Bold = true;
                    ((Excel.Range)xlSheet.Cells[1, iRestCols]).Value2 = title;

                    // Current Date/Time표시 유무.
                    if (bShowDate == true)
                    {
                        string sNowDate;
                        sNowDate = string.Format(Convert.ToString(DateTime.Now), "yyyy-MM-dd") + " " + string.Format(Convert.ToString(DateTime.Now), "HH:mm:ss");
                        ((Excel.Range)xlSheet.Cells[2, iRestCols]).Value2 = "Today: " + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                    }

                    //'조회조건에 값
                    if (condition.Length > 0)
                    {
                        Clipboard.SetDataObject((object)condition, false, 5, 10);
                        ((Excel.Range)xlSheet.Cells[3, iRestCols]).Select();
                        //xlSheet.PasteSpecial(Type.Missing, false, false, Type.Missing, Type.Missing, Type.Missing, Type.Missing);
                        xlSheet._PasteSpecial(Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing);
                    }

                    // 이미지가 하단에 위치할 경우,..
                    if (imageFile != null && imagePostion > 1)
                    {
                        int importRows = iRestRows + 1;

                        importRows = InsertImageToExcel((Excel._Worksheet)xlSheet, imageFile, importRows, iRestCols);
                    }
                }
                else
                {
                    // Set Excel file name.
                    xlBook.Saved = true;
                    xlBook.SaveAs(xlSheet.Name, Excel.XlFileFormat.xlWorkbookNormal,
                            Type.Missing, Type.Missing, true, Type.Missing, Excel.XlSaveAsAccessMode.xlNoChange,
                            Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing);
                }

                // Set cursor position.
                ((Excel.Range)xlSheet.Cells[1, 1]).Select();
                //System.Runtime.InteropServices.Marshal.ReleaseComObject(xlWiths);
                return bReturn;

            }
            catch (Exception ex)
            {
                String errorMessage = "";
                errorMessage = String.Concat(errorMessage, ex.Message);
                errorMessage = String.Concat(errorMessage, " Line: ");
                errorMessage = String.Concat(errorMessage, ex.Source);

                CmnFunction.ShowMsgBox(errorMessage, "Error [" + ex.Source + "]", MessageBoxButtons.OK, 1);
                bReturn = false;
                return bReturn;
            }
            finally
            {
                // 사용자에게 저장 여부를 묻는다.
                xlBook.Saved = false;

                System.Runtime.InteropServices.Marshal.ReleaseComObject(xlSheet);
                System.Runtime.InteropServices.Marshal.ReleaseComObject(xlBook);
                System.Runtime.InteropServices.Marshal.ReleaseComObject(xlBooks);

                if (bReturn == false)
                {
                    //Make Excel application close.
                    xlApp.DisplayAlerts = false;
                    //xlBook.Saved = true;
                    xlApp.Quit();
                }
                else
                {
                    // 모든 엑셀의 경고메시지가 나타나도록 한다.
                    xlApp.DisplayAlerts = true;
                    //Make Excel visible and give the user control.
                    xlApp.Visible = true;
                    xlApp.UserControl = true;
                }

                System.Runtime.InteropServices.Marshal.ReleaseComObject(xlApp);
                GC.Collect();
                Cursor.Current = Cursors.Default;
            }

        }

        /// <summary>
        /// Excel에 이미지(그래프)를 넣어서 export한다.
        /// </summary>
        /// <param name="xSheet">Microsoft.Office.Interop.Excel.Worksheet </param>
        /// <param name="imagefile">Image file name</param>
        /// <param name="row">Excel Sheet image row number</param>
        /// <param name="column">Excel Sheet image column number</param>
        /// <returns>이미지 크기에 따른 이미지의 마지막 Row number</returns>
        private static int InsertImageToExcel(Excel._Worksheet xSheet, string imagefile, int row, int column)
        {
            Excel.Range xlWiths;
            Excel.Pictures xPics;
            Excel.Picture xPicture;
            int nHightRow;

            try
            {
                //create some pictures
                xPics = (Excel.Pictures)xSheet.Pictures(Type.Missing);
                xPics.Insert(imagefile, Type.Missing);
                xPicture = (Excel.Picture)xPics.Item(1);

                //set the position of the pics
                xlWiths = (Excel.Range)xSheet.Cells[row, column];
                xPicture.Left = (double)xlWiths.Left;
                xPicture.Top = (double)xlWiths.Top;
                // 이미지의 폭/높이는 변경하지 않음.
                //xPic01.Height = (double)xlWiths.Height;
                //xPic01.Width = (double)xlWiths.Width;

                // 이미즈의 폭에 따른 컬럼(Column)의 계산이 컬럼의 크기 기준값이 달라 계산되지 않으므로,
                // 계산하지 않고, 이미지의 높이에 따른 로우(Row)의 크기만 계산.
                if (xPicture.Height <= (double)xlWiths.Height)
                {
                    nHightRow = row + 1;
                }
                else if ((double)xlWiths.Height <= 0)
                {
                    nHightRow = (int)xPicture.Height;
                }
                else
                {
                    nHightRow = (int)(xPicture.Height / (double)xlWiths.Height) + 1;
                    if (nHightRow > xSheet.Rows.Count)
                    {
                        nHightRow = (int)xPicture.Height;
                    }
                }

                return nHightRow;
            }
            catch (Exception ex)
            {
                CmnFunction.ShowMsgBox(ex.Message, "Error", MessageBoxButtons.OK, 1);
                return -1;
            }
        }

        // SpreadSheetToPrint()
        //       - Export Data of Spread Sheet Control to Printer
        // Return Value
        //       - True or False
        //
        private static bool SpreadSheetToPrint(FarPoint.Win.Spread.SheetView[] xSheet,
               string imageFile, int imagePostion,
                string title, string condition,
                bool colorFlag, bool bTextBase, bool bShowDate,
                int iStartCol, int iEndCol, int iRowHeight,
                bool bOnlyData, float[] pageOptions)
        {

            Excel.Application xlApp = new Excel.Application();
            Excel.Workbooks xlBooks = xlApp.Workbooks;
            Excel.Workbook xlBook = xlBooks.Add(Excel.XlWBATemplate.xlWBATWorksheet);
            Excel.Worksheet xlSheet = (Excel.Worksheet)xlBook.ActiveSheet;
            Excel.Range xlWiths;


            Int32 i, j, k;
            int startColumn, endColumn;
            int[] aColumns = null;
            int iRestRows, iRestCols, iColumnCnt;
            int pastColumn, pastColumnCount, pastColumnIdx;
            string clipboardData;
            bool bAddColumn, bReturn = true;


            try
            {
                int iRowHeaderCount, iColHeaderCount;

                System.Windows.Forms.Application.DoEvents();
                Cursor.Current = Cursors.WaitCursor;

                xlApp.Visible = false;
                // Sheet Naet init.
                if (title.Length > 0)
                {
                    string sheetTile = "";
                    sheetTile = title.Replace(" ", "").Replace(":", ".").Replace("?", ".").Replace("*", ".").Replace("[", ".").Replace("]", ".").Replace("<", ".").Replace(">", ".");
                    xlSheet.Name = sheetTile;
                }

                // Start Row/Column reset init.
                iColumnCnt = 0;
                iRowHeaderCount = 0;
                iColHeaderCount = 0;
                startColumn = 0;
                endColumn = 0;
                // Spread Data만 Export할 경우 Row/Column Header를 표시하지 않음.
                if (bOnlyData)
                {
                    iRestRows = EXCEL_FIRST_ROWCOL;
                    iRestCols = EXCEL_FIRST_ROWCOL;
                }
                else
                {
                    iRestRows = EXCEL_START_ROW;
                    iRestCols = EXCEL_START_COL;
                }

                // Spread Data만 Export할 경우 Image를 표시하지 않음.
                if (bOnlyData == false)
                {
                    // 데이터 조회조건에 대한 설명 또는 추가정보 설정.
                    condition = condition.TrimEnd(new char[] { ' ', '\r', '\n' });
                    if (condition.Length > 0)
                    {
                        string[] sTempStr = condition.Split(new char[] { '\r', '\n' });
                        iRestRows += (sTempStr.Length + 1);
                    }
                }

                //First Column Font/Width init.
                xlSheet.Cells.Select();
                xlSheet.Cells.EntireColumn.ColumnWidth = 1;
                xlSheet.Cells.Font.Name = "Tahoma";
                xlSheet.Cells.Font.Size = 10;
                ////  텍스트가 사용 가능한 열 너비에 맞게 자동으로 축소 여부.
                //xlSheet.Cells.ShrinkToFit = true;

                // 이미지가 상단에 위치할 경우(만약, imagePostion == 0이면 이미지를 표시하지 않음)
                // Spread Data만 Export할 경우 Image를 표시하지 않음.
                if (bOnlyData == false && imageFile != null && imagePostion == 1)
                {
                    int importRows = iRestRows;

                    importRows = InsertImageToExcel((Excel._Worksheet)xlSheet, imageFile, importRows, iRestCols);
                    if (importRows > 0) // Image is Top position.
                    {
                        iRestRows += importRows + 2;
                    }
                }

                // 각종 엑셀의 경고메시지를 보이지 않도록 합니다.
                xlApp.DisplayAlerts = false;

                // 셀서식을 TEXT로 인식하도록 한다.
                // 단, 셀 서식이 Text일 경우에는 셀 병합할 때 확인 창이 뜬다.
                if (bTextBase == true)
                {
                    xlSheet.Cells.NumberFormat = "@";
                }


                // 여러장의 분할된 Sread Sheet를 하나의 Excel에 붙여넣음.
                foreach (FarPoint.Win.Spread.SheetView activeSheet in xSheet)
                {
                    if (activeSheet.RowCount <= 0)
                    {
                        continue;
                    }

                    // Row Header count.
                    if (activeSheet.RowHeader.Visible == false || activeSheet.RowHeader.ColumnCount <= 0)
                    {
                        iRowHeaderCount = 0;
                    }
                    else
                    {
                        iRowHeaderCount = activeSheet.RowHeader.ColumnCount;
                    }

                    // Column Header count.
                    if (activeSheet.ColumnHeader.Visible == false || activeSheet.ColumnHeader.RowCount <= 0)
                    {
                        iColHeaderCount = 0;
                    }
                    else
                    {
                        iColHeaderCount = activeSheet.ColumnHeader.RowCount;
                    }

                    // 지정한 컬럼만 Export 할 경우가 아닌 경우
                    if (iStartCol < 0)
                    {
                        startColumn = 0;
                    }

                    if (iEndCol < 0)
                    {
                        endColumn = activeSheet.ColumnCount;
                        if (endColumn > xlSheet.Columns.Count)
                        {
                            // Excel 최대 Column이면 시작 Column을 제외한다.
                            endColumn = xlSheet.Columns.Count - iRestCols;
                        }
                    }

                    // Export target cell column number.(저장 대상 셀을 찾는다.)
                    iColumnCnt = 0;
                    for (i = startColumn; i < endColumn; i++)
                    {
                        if (activeSheet.Columns[i].Width > 0 && activeSheet.Columns[i].Visible == true)
                        {
                            bAddColumn = true;
                            for (j = 0; j < iColHeaderCount; j++)
                            {
                                if (activeSheet.ColumnHeader.Cells[j, i].CellType is FarPoint.Win.Spread.CellType.ButtonCellType)
                                {
                                    bAddColumn = false;
                                    break;
                                }
                                else if (activeSheet.ColumnHeader.Cells[j, i].CellType is FarPoint.Win.Spread.CellType.EmptyCellType)
                                {
                                    bAddColumn = false;
                                    break;
                                }
                                else if (activeSheet.ColumnHeader.Cells[j, i].CellType is FarPoint.Win.Spread.CellType.ProgressCellType)
                                {
                                    bAddColumn = false;
                                    break;
                                }
                                else if (activeSheet.ColumnHeader.Cells[j, i].CellType is FarPoint.Win.Spread.CellType.SliderCellType)
                                {
                                    bAddColumn = false;
                                    break;
                                }
                            }

                            if (bAddColumn == true)
                            {
                                if (aColumns == null)
                                {
                                    aColumns = new int[iColumnCnt + 1];
                                }
                                else
                                {
                                    int[] tempArray = new int[iColumnCnt];

                                    Array.Copy(aColumns, tempArray, iColumnCnt);
                                    aColumns = new int[iColumnCnt + 1];
                                    Array.Copy(tempArray, aColumns, iColumnCnt);
                                }
                                aColumns[iColumnCnt] = i;
                                iColumnCnt++;
                            }
                        }
                    }

                    // Spread Data만 Export할 경우 Row/Column Header를 표시하지 않음.
                    if (bOnlyData == false && iColHeaderCount > 0)
                    {
                        int spanCount; // Row/Column span count.

                        // Column Header name 및 Cell 서식 설정.
                        for (j = 0; j < iColHeaderCount; j++)
                        {
                            string thisHeader;
                            string[] sColumnHeader;

                            thisHeader = activeSheet.ColumnHeader.GetClip(j, 0, 1, -1);
                            sColumnHeader = thisHeader.TrimEnd(new char[] { '\n', '\r' }).Split('\t');

                            clipboardData = "";
                            for (i = 0; i < iColumnCnt; i++)
                            {
                                thisHeader = "";
                                if (aColumns[i] < sColumnHeader.Length)
                                {
                                    thisHeader = sColumnHeader[aColumns[i]];
                                }

                                clipboardData += thisHeader + "\t";
                                spanCount = activeSheet.ColumnHeader.Cells[j, aColumns[i]].ColumnSpan;
                                if (spanCount > 1)
                                {
                                    for (k = 0; k <= (spanCount - 2); k++)
                                    {
                                        clipboardData += " " + "\t";
                                    }
                                    i += spanCount - 1;
                                }
                            }

                            clipboardData = clipboardData + "\n";
                            Clipboard.SetDataObject((object)clipboardData, false, 5, 10);
                            ((Excel.Range)xlSheet.Cells[(iRestRows + j), (iRestCols + iRowHeaderCount)]).Select();
                            //xlSheet.PasteSpecial(Type.Missing, false, false, Type.Missing, Type.Missing, Type.Missing, Type.Missing);
                            xlSheet._PasteSpecial(Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing);
                        }


                        // Column Heaer의 Merge를 설정.
                        for (j = 0; j < iColHeaderCount; j++)
                        {
                            for (k = 0; k < iColumnCnt; k++)
                            {
                                i = aColumns[k];
                                if (iRowHeaderCount > k)
                                {
                                    k += iRowHeaderCount - 1;
                                }
                                else
                                {
                                    spanCount = activeSheet.ColumnHeader.Cells[j, i].ColumnSpan;
                                    if (spanCount > 1)
                                    {
                                        xlWiths = xlSheet.get_Range(xlSheet.Cells[j + iRestRows, k + iRestCols],
                                                xlSheet.Cells[j + iRestRows, k + iRestCols + spanCount - 1]);
                                        //xlWiths.Borders[Excel.XlBordersIndex.xlEdgeRight].LineStyle = ExcelComponent.XlLineStyle.xlContinuous;
                                        xlWiths.Select();
                                        xlWiths.Merge(Type.Missing);

                                        k += spanCount - 1;
                                    }

                                    spanCount = activeSheet.ColumnHeader.Cells[j, i].RowSpan;
                                    if (spanCount > 1 && spanCount <= (iColHeaderCount - j))
                                    {
                                        xlWiths = xlSheet.get_Range(xlSheet.Cells[j + iRestRows, k + iRestCols],
                                                xlSheet.Cells[j + iRestRows + spanCount - 1, k + iRestCols]);
                                        //xlWiths.Borders[Excel.XlBordersIndex.xlEdgeRight].LineStyle = ExcelComponent.XlLineStyle.xlContinuous;
                                        xlWiths.Select();
                                        xlWiths.Merge(Type.Missing);
                                    }
                                }
                            }
                        }


                        // Column Header 에 대한 셀서식설정.
                        xlWiths = xlSheet.get_Range(xlSheet.Cells[iRestRows, iRestCols], xlSheet.Cells[iRestRows + iColHeaderCount - 1, iRestCols + iRowHeaderCount + iColumnCnt - 1]);
                        xlWiths.HorizontalAlignment = ExcelComponent.XlHAlign.xlHAlignCenter; //-4108
                        xlWiths.VerticalAlignment = ExcelComponent.XlVAlign.xlValignCenter; //-4108
                        xlWiths.Interior.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.DodgerBlue);
                        xlWiths.Font.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.White);
                        xlWiths.Font.Bold = true;
                        xlWiths.Borders[Excel.XlBordersIndex.xlEdgeLeft].LineStyle = ExcelComponent.XlLineStyle.xlContinuous;
                        xlWiths.Borders[Excel.XlBordersIndex.xlEdgeRight].LineStyle = ExcelComponent.XlLineStyle.xlContinuous;
                        xlWiths.Borders[Excel.XlBordersIndex.xlEdgeTop].LineStyle = ExcelComponent.XlLineStyle.xlContinuous;
                        xlWiths.Borders[Excel.XlBordersIndex.xlEdgeBottom].LineStyle = ExcelComponent.XlLineStyle.xlContinuous;
                        if (xlWiths.Rows.Count > 1)
                        {
                            xlWiths.Borders[Excel.XlBordersIndex.xlInsideHorizontal].LineStyle = ExcelComponent.XlLineStyle.xlContinuous;
                        }

                        if (xlWiths.Columns.Count > 1)
                        {
                            xlWiths.Borders[Excel.XlBordersIndex.xlInsideVertical].LineStyle = ExcelComponent.XlLineStyle.xlContinuous;
                        }

                        // Data 시작 위치 재설정.
                        iRestRows += iColHeaderCount;
                    }

                    #region Set RowHeader/Rows cell formula.
                    // Set RowHeaders cell formula.
                    if (iRowHeaderCount > 0)
                    {
                        int spanCount, skipColumn; // Row/Column span count.
                        string rowLavel;

                        // Row Heaer의 Merge를 설정.
                        for (j = 0; j < activeSheet.RowCount; j++)
                        {
                            for (k = 0; k < iRowHeaderCount; k++)
                            {
                                skipColumn = 0;
                                // Set Row Header Text.
                                rowLavel = activeSheet.GetRowLabel(j, k).ToString().Trim();
                                ((Excel.Range)xlSheet.Cells[j + iRestRows, k + iRestCols]).Value2 = rowLavel;

                                spanCount = activeSheet.RowHeader.Cells[j, k].ColumnSpan;
                                if (spanCount > 1 && spanCount <= (iRowHeaderCount - k))
                                {
                                    xlWiths = xlSheet.get_Range(xlSheet.Cells[j + iRestRows, k + iRestCols],
                                            xlSheet.Cells[j + iRestRows, k + iRestCols + spanCount - 1]);
                                    //xlWiths.Borders[Excel.XlBordersIndex.xlEdgeRight].LineStyle = ExcelComponent.XlLineStyle.xlContinuous;
                                    xlWiths.Select();
                                    xlWiths.Merge(Type.Missing);

                                    skipColumn = spanCount - 1;
                                }

                                spanCount = activeSheet.RowHeader.Cells[j, k].RowSpan;
                                if (spanCount > 1)
                                {
                                    xlWiths = xlSheet.get_Range(xlSheet.Cells[j + iRestRows, k + iRestCols],
                                            xlSheet.Cells[j + iRestRows + spanCount - 1, k + iRestCols]);
                                    //xlWiths.Borders[Excel.XlBordersIndex.xlEdgeRight].LineStyle = ExcelComponent.XlLineStyle.xlContinuous;
                                    xlWiths.Select();
                                    xlWiths.Merge(Type.Missing);
                                }

                                k += skipColumn;
                            }
                        }

                        //'Row Header 셀서식
                        xlWiths = xlSheet.get_Range(xlSheet.Cells[iRestRows, iRestCols], xlSheet.Cells[iRestRows + activeSheet.RowCount - 1, iRestCols + iRowHeaderCount - 1]);
                        xlWiths.HorizontalAlignment = ExcelComponent.XlHAlign.xlHAlignCenter; //-4108
                        xlWiths.VerticalAlignment = ExcelComponent.XlVAlign.xlValignCenter; //-4108
                        xlWiths.Interior.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.DodgerBlue);
                        xlWiths.Font.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.White);
                        xlWiths.Font.Bold = true;
                        xlWiths.Borders[Excel.XlBordersIndex.xlEdgeLeft].LineStyle = ExcelComponent.XlLineStyle.xlContinuous;
                        xlWiths.Borders[Excel.XlBordersIndex.xlEdgeRight].LineStyle = ExcelComponent.XlLineStyle.xlContinuous;
                        xlWiths.Borders[Excel.XlBordersIndex.xlEdgeTop].LineStyle = ExcelComponent.XlLineStyle.xlContinuous;
                        xlWiths.Borders[Excel.XlBordersIndex.xlEdgeBottom].LineStyle = ExcelComponent.XlLineStyle.xlContinuous;
                        if (xlWiths.Rows.Count > 1)
                        {
                            xlWiths.Borders[Excel.XlBordersIndex.xlInsideHorizontal].LineStyle = ExcelComponent.XlLineStyle.xlContinuous;
                        }

                        if (xlWiths.Columns.Count > 1)
                        {
                            xlWiths.Borders[Excel.XlBordersIndex.xlInsideVertical].LineStyle = ExcelComponent.XlLineStyle.xlContinuous;
                        }

                        // Data Start Column position reset.
                        //iRestCols += iRowHeaderCount;
                    }

                    // Set Rows cell formula.
                    if (activeSheet.RowCount > 0)
                    {
                        xlWiths = xlSheet.get_Range(xlSheet.Cells[iRestRows, iRestCols + iRowHeaderCount], xlSheet.Cells[iRestRows + activeSheet.RowCount - 1, iRestCols + iRowHeaderCount + iColumnCnt - 1]);
                        xlWiths.Borders[Excel.XlBordersIndex.xlEdgeLeft].LineStyle = ExcelComponent.XlLineStyle.xlContinuous;
                        xlWiths.Borders[Excel.XlBordersIndex.xlEdgeRight].LineStyle = ExcelComponent.XlLineStyle.xlContinuous;
                        xlWiths.Borders[Excel.XlBordersIndex.xlEdgeBottom].LineStyle = ExcelComponent.XlLineStyle.xlContinuous;
                        if (xlWiths.Columns.Count > 1)
                        {
                            xlWiths.Borders[Excel.XlBordersIndex.xlInsideVertical].LineStyle = ExcelComponent.XlLineStyle.xlContinuous;
                            xlWiths.Borders[Excel.XlBordersIndex.xlInsideVertical].Weight = ExcelComponent.XlBorderWeight.xlHairline;
                        }

                        if (xlWiths.Rows.Count > 1)
                        {
                            xlWiths.Borders[Excel.XlBordersIndex.xlInsideHorizontal].LineStyle = ExcelComponent.XlLineStyle.xlContinuous;
                            xlWiths.Borders[Excel.XlBordersIndex.xlInsideHorizontal].Weight = ExcelComponent.XlBorderWeight.xlHairline;
                        }
                    }
                    #endregion

                    #region Get Spread all values.(Hidden column skip).
                    pastColumn = 0;
                    pastColumnCount = 0;
                    pastColumnIdx = iRestCols + iRowHeaderCount;
                    for (i = 0; i < iColumnCnt; i++)
                    {
                        if (aColumns[i] == (pastColumn + pastColumnCount))
                        {
                            pastColumnCount++;
                            continue;
                        }
                        else
                        {
                            if (pastColumnCount > 0)
                            {
                                clipboardData = activeSheet.GetClip(0, pastColumn, -1, pastColumnCount);
                                Clipboard.SetDataObject((object)clipboardData, false, 5, 10);
                                ((Excel.Range)xlSheet.Cells[iRestRows, pastColumnIdx]).Select();
                                //xlSheet.PasteSpecial(Type.Missing, false, false, Type.Missing, Type.Missing, Type.Missing, Type.Missing);
                                xlSheet._PasteSpecial(Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing);

                                pastColumnIdx += pastColumnCount;
                            }

                            pastColumn = aColumns[i];
                            pastColumnCount = 1;
                        }
                    }

                    if (pastColumnCount > 0)
                    {
                        clipboardData = activeSheet.GetClip(0, pastColumn, -1, pastColumnCount);
                        Clipboard.SetDataObject((object)clipboardData, false, 5, 10);
                        ((Excel.Range)xlSheet.Cells[iRestRows, pastColumnIdx]).Select();
                        //xlSheet.PasteSpecial(Type.Missing, false, false, Type.Missing, Type.Missing, Type.Missing, Type.Missing);
                        xlSheet._PasteSpecial(Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing);
                    }

                    // Column크기를 자동으로 조절(Row and Column header).
                    xlWiths = xlSheet.get_Range(xlSheet.Cells[(iRestRows - iColHeaderCount > 0 ? iRestRows - iColHeaderCount : iRestRows), iRestCols],
                                xlSheet.Cells[iRestRows + activeSheet.RowCount - 1, iRestCols + iRowHeaderCount + iColumnCnt - 1]);
                    xlWiths.EntireColumn.AutoFit();
                    // Row 크기를 조절.
                    if (iRowHeight > 0)
                    {
                        //Keep the minimum row height to 13.
                        if (iRowHeight < 13)
                        {
                            iRowHeight = 13;
                        }
                        xlWiths.EntireRow.RowHeight = iRowHeight;
                    }
                    #endregion

                    // 각 Column의 Background/ForeColor를 설정함.
                    if (colorFlag == true)
                    {
                        for (i = 0; i < activeSheet.RowCount; i++)
                        {
                            for (j = startColumn; j < endColumn; j++)
                            {
                                if (activeSheet.Cells[i, j].ForeColor.IsEmpty == false)
                                {
                                    if (activeSheet.Cells[i, j].ForeColor.ToArgb() != System.Drawing.Color.Black.ToArgb())
                                    {
                                        ((Excel.Range)xlSheet.Cells[i + iRestRows, j + iRestCols + iRowHeaderCount]).Font.Color =
                                            System.Drawing.ColorTranslator.ToOle(activeSheet.Cells[i, j].ForeColor);
                                    }
                                }

                                if (activeSheet.Cells[i, j].BackColor.IsEmpty == false)
                                {
                                    if (activeSheet.Cells[i, j].BackColor.ToArgb() != System.Drawing.Color.White.ToArgb())
                                    {
                                        ((Excel.Range)xlSheet.Cells[i + iRestRows, j + iRestCols + iRowHeaderCount]).Interior.Color =
                                            System.Drawing.ColorTranslator.ToOle(activeSheet.Cells[i, j].BackColor);
                                    }
                                }
                            }
                        }
                    }

                    iRestRows += activeSheet.RowCount;
                    // 데이터만 export(bOnlyData == true)할 경우엔 Spread별로 구분하는 Row를 넣지 않음.
                    if (bOnlyData == false)
                    {
                        iRestRows++;
                    }
                }

                // Spread Data만 Export할 경우 Page Setup.
                if (pageOptions != null && pageOptions.Length == 12)
                {
                    InsertPrintOption(xlSheet, pageOptions);
                }

                // Spread Data만 Export할 경우 Title, Row/Column Header, Conditions, Current Date를 표시하지 않음.
                if (bOnlyData == false)
                {
                    // Title 에 대한 셀 서식 지정
                    xlWiths = xlSheet.get_Range(xlSheet.Cells[1, iRestCols], xlSheet.Cells[1, iRestCols]);
                    xlWiths.VerticalAlignment = ExcelComponent.XlVAlign.xlValignCenter;
                    xlWiths.RowHeight = 30;
                    xlWiths.Font.Size = 18;
                    xlWiths.Font.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.Black);
                    xlWiths.Font.Bold = true;
                    ((Excel.Range)xlSheet.Cells[1, iRestCols]).Value2 = title;

                    // Current Date/Time표시 유무.
                    if (bShowDate == true)
                    {
                        string sNowDate;
                        sNowDate = string.Format(Convert.ToString(DateTime.Now), "yyyy-MM-dd") + " " + string.Format(Convert.ToString(DateTime.Now), "HH:mm:ss");
                        ((Excel.Range)xlSheet.Cells[2, iRestCols]).Value2 = "Today: " + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                    }

                    //'조회조건에 값
                    if (condition.Length > 0)
                    {
                        Clipboard.SetDataObject((object)condition, false, 5, 10);
                        ((Excel.Range)xlSheet.Cells[3, iRestCols]).Select();
                        //xlSheet.PasteSpecial(Type.Missing, false, false, Type.Missing, Type.Missing, Type.Missing, Type.Missing);
                        xlSheet._PasteSpecial(Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing);
                    }

                    // 이미지가 하단에 위치할 경우,..
                    if (imageFile != null && imagePostion > 1)
                    {
                        int importRows = iRestRows + 1;

                        importRows = InsertImageToExcel((Excel._Worksheet)xlSheet, imageFile, importRows, iRestCols);
                    }

                    // Page Setup
                    xlSheet.PageSetup.LeftMargin = 0;
                    xlSheet.PageSetup.RightMargin = 0;

                    if (true)
                    {
                        xlSheet.PageSetup.Orientation = XlPageOrientation.xlLandscape;

                        xlWiths = xlSheet.get_Range(xlSheet.Cells[1, 1],
                                                xlSheet.Cells[iRestRows, endColumn]);

                        xlSheet.PageSetup.Zoom = ((72 * 11.69 - (10 * 2) - xlSheet.PageSetup.LeftMargin - xlSheet.PageSetup.RightMargin) / (double)xlWiths.Width) * 100;

                    //}
                    //else
                    //{
                    //    xlSheet.PageSetup.Orientation = XlPageOrientation.xlPortrait;
                    //    xlSheet.PageSetup.Zoom = ((72 * 8.27 - (10 * 2) - xlSheet.PageSetup.LeftMargin - xlSheet.PageSetup.RightMargin) / (double)xlWiths.Width) * 100;
                    }
                }

                // Set cursor position.
                ((Excel.Range)xlSheet.Cells[1, 1]).Select();
                //System.Runtime.InteropServices.Marshal.ReleaseComObject(xlWiths);
                xlSheet.PrintOut(1, 1, 1, false, null, false, true, null);

                return bReturn;

            }
            catch (Exception ex)
            {
                String errorMessage = "";
                errorMessage = String.Concat(errorMessage, ex.Message);
                errorMessage = String.Concat(errorMessage, " Line: ");
                errorMessage = String.Concat(errorMessage, ex.Source);

                CmnFunction.ShowMsgBox(errorMessage, "Error [" + ex.Source + "]", MessageBoxButtons.OK, 1);
                bReturn = false;
                return bReturn;
            }
            finally
            {
                // 사용자에게 저장 여부를 묻는다.
                xlBook.Saved = false;

                System.Runtime.InteropServices.Marshal.ReleaseComObject(xlSheet);
                System.Runtime.InteropServices.Marshal.ReleaseComObject(xlBook);
                System.Runtime.InteropServices.Marshal.ReleaseComObject(xlBooks);

                if (bReturn == true)
                {
                    //Make Excel application close.
                    xlApp.DisplayAlerts = false;
                    //xlBook.Saved = true;
                    xlApp.Quit();
                }
                else
                {
                    // 모든 엑셀의 경고메시지가 나타나도록 한다.
                    xlApp.DisplayAlerts = true;
                    //Make Excel visible and give the user control.
                    xlApp.Visible = true;
                    xlApp.UserControl = true;
                }

                System.Runtime.InteropServices.Marshal.ReleaseComObject(xlApp);
                GC.Collect();
                Cursor.Current = Cursors.Default;
            }

        }

        /// <summary>
        /// Export excel Page Setup.
        /// </summary>
        /// <param name="xSheet">Microsoft.Office.Interop.Excel.Worksheet </param>
        /// <param name="prtOptions">pageOption Array[12] values(Set defualt use = -1): 
        /// Array 1.Sets the size of the left margin, in points.
        /// Array 2.Sets the size of the right margin, in points.
        /// Array 3.Sets the size of the top margin, in points.
        /// Array 4.Sets the size of the bottom margin, in points.
        /// Array 5.Sets the size of the header margin, in points.
        /// Array 6.Sets the size of the footer margin, in points.
        /// Array 7.True(1) if row and column headings are printed with this page.(Default:False)
        /// Array 8.True(1) if cell gridlines are printed on the page.(Default:True)
        /// Array 9.True(1) if the sheet is centered horizontally on the page when it's printed.(Default:False)
        /// Array 10.True(1) if the sheet is centered vertically on the page when it's printed.(Default:False)
        /// Array 11.Sets a XlPageOrientation value that represents the portrait or landscape printing mode.
        /// Array 12.Sets a percentage (between 10 and 400 percent) by which Microsoft Excel will scale the worksheet for printing.</param>
        /// <returns></returns>
        private static bool InsertPrintOption(Excel._Worksheet xSheet, float[] pageOptions)
        {
            try
            {
                if (pageOptions.Length < 12)
                {
                    return false;
                }

                // Sets the size of the left margin, in points. Read/write Double.
                if (pageOptions[0] >= 0)
                {
                    xSheet.PageSetup.LeftMargin = xSheet.Application.CentimetersToPoints((double)pageOptions[0]);
                }

                // Sets the size of the right margin, in points. Read/write Double.
                if (pageOptions[1] >= 0)
                {
                    xSheet.PageSetup.RightMargin = xSheet.Application.CentimetersToPoints((double)pageOptions[1]);
                }

                // Sets the size of the top margin, in points. Read/write Double.
                if (pageOptions[2] >= 0)
                {
                    xSheet.PageSetup.TopMargin = xSheet.Application.CentimetersToPoints((double)pageOptions[2]);
                }

                // Sets the size of the bottom margin, in points. Read/write Double.
                if (pageOptions[3] >= 0)
                {
                    xSheet.PageSetup.BottomMargin = xSheet.Application.CentimetersToPoints((double)pageOptions[3]);
                }

                // Sets the size of the header margin, in points. Read/write Double.
                if (pageOptions[4] >= 0)
                {
                    xSheet.PageSetup.HeaderMargin = xSheet.Application.CentimetersToPoints((double)pageOptions[4]);
                }

                // Sets the size of the footer margin, in points. Read/write Double.
                if (pageOptions[5] >= 0)
                {
                    xSheet.PageSetup.FooterMargin = xSheet.Application.CentimetersToPoints((double)pageOptions[5]);
                }

                //True if row and column headings are printed with this page.(Default:False)
                if (pageOptions[6] >= 0)
                {
                    xSheet.PageSetup.PrintHeadings = (pageOptions[6] <= 0F ? true : false);
                }

                //True if cell gridlines are printed on the page.(Default:True)
                if (pageOptions[7] >= 0)
                {
                    xSheet.PageSetup.PrintGridlines = (pageOptions[7] <= 0F ? true : false);
                }

                //True if the sheet is centered horizontally on the page when it's printed.(Default:False)
                if (pageOptions[8] >= 0)
                {
                    xSheet.PageSetup.CenterHorizontally = (pageOptions[8] <= 0F ? true : false);
                }

                //True if the sheet is centered vertically on the page when it's printed.(Default:False)
                if (pageOptions[9] >= 0)
                {
                    xSheet.PageSetup.CenterVertically = (pageOptions[9] <= 0F ? true : false);
                }

                // Sets a XlPageOrientation value that represents the portrait or landscape printing mode.
                if (pageOptions[10] >= 0)
                {
                    xSheet.PageSetup.Orientation =
                        (pageOptions[10] <= 0F ? Excel.XlPageOrientation.xlPortrait : Excel.XlPageOrientation.xlLandscape);
                }

                //Sets a percentage (between 10 and 400 percent) by which Microsoft Excel will scale the worksheet for printing.
                if (pageOptions[11] > 0)
                {
                    xSheet.PageSetup.Zoom = (pageOptions[11] <= 10F ? 10 : (int)pageOptions[11]);
                }
                if (pageOptions[11] == 0)
                {
                    xSheet.PageSetup.Zoom = false;
                }

                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}