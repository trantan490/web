//-----------------------------------------------------------------------------
//
//   System      : MES Report
//   File Name   : 
//   Description : Client Common function Module 
//
//   MES Version : 4.x.x.x
//
//   History
//       - **** Do Not Modify in Site!!! ****
//       - 2008-10-01 : Created by John Seo
//
//
//   Copyright(C) 1998-2005 MIRACOM,INC.
//   All rights reserved.
//
//-----------------------------------------------------------------------------
using System;
using System.Data;
using System.Windows.Forms;
using System.IO;
using System.Reflection;
using System.Collections;
using FarPoint.Win.Spread;
using FarPoint.Excel;
using SoftwareFX.ChartFX;
using Microsoft.Office.Core;
using Microsoft.VisualBasic;

//using System.Windows.Forms.DataVisualization.Charting;
//using Excel = Microsoft.Office.Interop.Excel;

namespace Miracom.SmartWeb.UI.Controls
{
	/// <summary>
	/// ExcelHelper에 대한 요약 설명입니다.
	/// </summary>
	public class ExcelHelper
	{
		private static ExcelHelper _instance = null;
        Excel.Application xlApp;
        Excel.Workbooks xlBooks;
        Excel.Workbook xlBook;
        Excel.Worksheet xlSheet;        
        int iMrgECol = 0;
        int ColSize = 0;
        
		private ExcelHelper()
		{

		}

		public static ExcelHelper Instance
		{
			get
			{
				if( _instance == null)
				{
					_instance = new ExcelHelper();
				}
				return _instance;
			}
        }
        
        #region Spread 및 Chart의 내용을 엑셀로 Export

        /// <summary>
        /// Spread의 내용을 엑셀로 Export함.시작위치는 내부에서 자동 계산.
        /// </summary>
        /// <param name="oSpread"> Spread컨트롤 명 </param>
        /// <param name="sFileTitle"> 화면 명 </param>
        /// <param name="sHeadL"> 엑셀 머릿말(왼쪽) </param>
        /// <param name="sHeadR"> 엑셀 머릿말(오른쪽) </param>
        public void subMakeExcel(FpSpread oSpread, string sFileTitle,
                                 string sHeadL, string sHeadR)
        {
            this.subMakeExcel(oSpread, null, sFileTitle, sHeadL, sHeadR, true);
        }

        public void subMakeExcel(FpSpread oSpread, string sFileTitle,
                                 string sHeadL, string sHeadR, bool bAutoFit)
        {
            this.subMakeExcel(oSpread, null, sFileTitle, sHeadL, sHeadR, bAutoFit);
        }

        /// <summary>
        /// Spread와 Chart의 내용을 엑셀로 Export함.시작위치는 내부에서 자동 계산.
        /// </summary>
        /// <param name="oSpread"> Spread컨트롤 명 </param>
        /// <param name="oChartFx"> ChartFX컨트롤 명 명 </param>
        /// <param name="sFileTitle"> 화면 명 </param>
        /// <param name="sHeadL"> 엑셀 머릿말(왼쪽) </param>
        /// <param name="sHeadR"> 엑셀 머릿말(오른쪽) </param>        
        public void subMakeExcel(FpSpread oSpread, SoftwareFX.ChartFX.Chart oChartFx,
                                 string sFileTitle, string sHeadL, string sHeadR)
        {
            this.subMakeExcel(oSpread, oChartFx, sFileTitle, sHeadL, sHeadR, true);
        }
        
        /// <summary>
        /// Spread와 Chart의 내용을 엑셀로 Export함.시작위치는 내부에서 자동 계산.
        /// </summary>
        /// <param name="oSpread"> Spread컨트롤 명 </param>
        /// <param name="oChartFx"> ChartFX컨트롤 명 명 </param>
        /// <param name="sFileTitle"> 화면 명 </param>
        /// <param name="sHeadL"> 엑셀 머릿말(왼쪽) </param>
        /// <param name="sHeadR"> 엑셀 머릿말(오른쪽) </param>
        /// <param name="autofit">오토피트(자동너비계산)</param>
        public void subMakeExcel(FpSpread oSpread, SoftwareFX.ChartFX.Chart oChartFx,
                                 string sFileTitle, string sHeadL, string sHeadR, bool bAutoFit)
        {
            DialogResult dlg;
            bool IsMerge = true;
            bool bResult = true;
            int iSCol = 1;
            int iSRow = 0;
            int iMergeColSize = 0;
            int iChartRow = 0;

            int iTmp = 0;
            int jTmp = 0;

            try
            {
                if (oSpread.ActiveSheet.Rows.Count < 1)
                {
                    MessageBox.Show("저장할 Data가 없습니다.", "Excel");
                }

                if (oSpread.ActiveSheet.Rows.Count > 600)
                {
                    dlg = MessageBox.Show("데이타 건수가 많아 셀병합 작업시 속도가 느려집니다. 셀병합후 엑셀로 저장하시겠습니까?", "Excel Export", MessageBoxButtons.YesNo);
                    if (dlg == DialogResult.No)
                    {
                        IsMerge = false;
                    }
                }

                int iECol = 0;
                int iERow = 0;
                int iGridHeadCnt = 0;

                if (sFileTitle == null) sFileTitle = "";
                if (sHeadL == null) sHeadL = "";
                if (sHeadR == null) sHeadR = "";

                xlApp = new Excel.Application();                
                xlBooks = xlApp.Workbooks;
                xlBook = xlBooks.Add(Excel.XlWBATemplate.xlWBATWorksheet);
                xlSheet = (Excel.Worksheet)xlBook.ActiveSheet;
                                
                xlApp.Visible = false;
                //xlApp.Visible = true;

                xlApp.Cells.ClearContents();
                xlApp.Cells.ClearFormats();

                iGridHeadCnt = oSpread.ActiveSheet.ColumnHeaderRowCount;
                 
                DataTable dt = null;
                String strSql = null;

                strSql = " SELECT USER_DESC FROM RWEBUSRDEF WHERE USER_ID = '" + GlobalVariable.gsUserID + "' ";
                dt = CmnFunction.oComm.GetFuncDataTable("DYNAMIC", strSql);

                if (sHeadR == "")
                    sHeadR = "사 용 자 : " + dt.Rows[0][0].ToString() + " (" + GlobalVariable.gsUserID + ")";
                else
                    sHeadR = sHeadR + "^사 용 자 : " + dt.Rows[0][0].ToString() + " (" + GlobalVariable.gsUserID + ")";

                if (oChartFx == null)
                {
                    //iSRow값을 내부에서 계산하게 수정.
                    if (sHeadL != "")
                        iTmp = sHeadL.Split('^').Length;

                    if (sHeadR != "")
                        jTmp = sHeadR.Split('^').Length;

                    iSRow = (iTmp > jTmp ? iTmp : jTmp) + 5 + 1;

                    //페이지 여백설정
                    subPageSetup(iSRow + iGridHeadCnt - 1, true);
                }
                else
                {
                    //iSRow값을 내부에서 계산하게 수정.
                    if (sHeadL != "")
                        iTmp = sHeadL.Split('^').Length;

                    if (sHeadR != "")
                        jTmp = sHeadR.Split('^').Length;

                    // 머릿말 Row수 + 타이틀이 차지하는 Row수(5)
                    iChartRow = (iTmp > jTmp ? iTmp : jTmp) + 5 + 1;

                    //Chart복사
                    iSRow = CopyChart(oChartFx, iChartRow);

                    //페이지 여백설정
                    subPageSetup(iSRow + iGridHeadCnt - 1, false);
                }

                //Header항목이 2라인일 경우
                iECol = CopySpView(oSpread, iGridHeadCnt, iSCol, iSRow, ref iMergeColSize); //Excel에서 마지막 Col의 위치값

                ColSize = iECol;

                iERow = oSpread.ActiveSheet.Rows.Count + iSRow + iGridHeadCnt - 1; //Excel에서 마지막 Row의 위치값

                //Header의 라인 작성(Head부분의 색적용, Data라인 작성)
                HeaderLine(iGridHeadCnt, iSCol, iSRow, iECol, iERow, iMergeColSize);

                ////각각의 머리글을 작성
                setHeader(iSCol, iECol, sHeadL, sHeadR);

                //파일의 타이틀을 작성
                setTitle(iSCol, iECol, sFileTitle);

                //Data부분 셀 Merge (600건 넘어가면 속도 작살임..)
                if (IsMerge == true)
                {
                    string[] tmpData = null;
                    int iRepeatRow = 0;

                    if (oSpread.ActiveSheet.Tag != null)
                    {
                        tmpData = oSpread.ActiveSheet.Tag.ToString().Split('^');
                        iRepeatRow = Convert.ToInt16(tmpData[1].ToString()) - 1;
                    }

                    DataMerge(iSCol, iSRow + iGridHeadCnt, iMrgECol, iERow, iMergeColSize, iRepeatRow);
                }

                xlSheet.get_Range(xlSheet.Cells[iSRow, iSCol], xlSheet.Cells[iSRow, iSCol]).Select();

                oSpread.ActiveSheet.SetActiveCell(iSRow, iSCol);

                if (bAutoFit == true)
                {
                    //왼쪽 머릿말과 오른쪽 머리말 부분을 뺀 나머지만 AutoFit을 한다.
                    if (sHeadL == "" && sHeadR == "")                    
                        xlSheet.get_Range(xlSheet.Cells[iSRow, 1], xlSheet.Cells[iSRow, iECol]).EntireColumn.AutoFit();
                    else if (sHeadL == "" && sHeadR != "")
                        xlSheet.get_Range(xlSheet.Cells[iSRow, 1], xlSheet.Cells[iSRow, iECol-1]).EntireColumn.AutoFit();
                    else if (sHeadL != "" && sHeadR == "")
                        xlSheet.get_Range(xlSheet.Cells[iSRow, 2], xlSheet.Cells[iSRow, iECol]).EntireColumn.AutoFit();
                    else
                        xlSheet.get_Range(xlSheet.Cells[iSRow, 2], xlSheet.Cells[iSRow, iECol - 1]).EntireColumn.AutoFit();
                }                
                
                bResult = true;

            }
            catch (Exception ex)
            {
                String errorMessage = "";
                errorMessage = String.Concat(errorMessage, ex.Message);
                errorMessage = String.Concat(errorMessage, " Line: ");
                errorMessage = String.Concat(errorMessage, ex.Source);

                CmnFunction.ShowMsgBox(errorMessage, "Error [" + ex.Source + "]", MessageBoxButtons.OK, 1);
                bResult = false;
            }
            finally
            {
                // 사용자에게 저장 여부를 묻는다.
                xlBook.Saved = false;

                System.Runtime.InteropServices.Marshal.ReleaseComObject(xlSheet);
                System.Runtime.InteropServices.Marshal.ReleaseComObject(xlBook);
                System.Runtime.InteropServices.Marshal.ReleaseComObject(xlBooks);

                if (bResult == false)
                {
                    //Make Excel application close.
                    xlApp.DisplayAlerts = false;
                    //xlBook.Saved = true;                    
                    xlBooks.Close();  // ***** 이 함수를 호출 하지 않으면 작업프로세스에 EXCEL.EXE가 죽지 않고 계속 남아 있음 *****
                    xlApp.Quit();
                }
                else
                {
                    // 모든 엑셀의 경고메시지가 나타나도록 한다.
                    //xlApp.DisplayAlerts = true;
                    //Make Excel visible and give the user control.
                    xlApp.Visible = true;
                    xlApp.UserControl = true;
                }

                System.Runtime.InteropServices.Marshal.ReleaseComObject(xlApp);
                GC.Collect();
                System.Windows.Forms.Cursor.Current = Cursors.Default;
            }

        } //public void subMakeExcel(FpSpread oSpread, Chart oChartFx, string sFileTitle, string sHeadL, string sHeadR)


        /// <summary>
        /// Spread와 Chart의 내용을 엑셀로 Export함.시작위치는 내부에서 자동 계산.
        /// </summary>
        /// <param name="oSpread"> Spread컨트롤 명 </param>
        /// <param name="oChartFx"> MSChart컨트롤 명 명 </param>
        /// <param name="sFileTitle"> 화면 명 </param>
        /// <param name="sHeadL"> 엑셀 머릿말(왼쪽) </param>
        /// <param name="sHeadR"> 엑셀 머릿말(오른쪽) </param>        
        public void subMakeMsChartExcel(FpSpread oSpread, System.Windows.Forms.DataVisualization.Charting.Chart oMSChart,
                                 string sFileTitle, string sHeadL, string sHeadR)
        {
            this.subMakeMsChartExcel(oSpread, oMSChart, sFileTitle, sHeadL, sHeadR, true);
        }

        /// <summary>
        /// Spread와 Chart의 내용을 엑셀로 Export함.시작위치는 내부에서 자동 계산.
        /// </summary>
        /// <param name="oSpread"> Spread컨트롤 명 </param>
        /// <param name="oChartFx"> MSChart컨트롤 명 명 </param>
        /// <param name="sFileTitle"> 화면 명 </param>
        /// <param name="sHeadL"> 엑셀 머릿말(왼쪽) </param>
        /// <param name="sHeadR"> 엑셀 머릿말(오른쪽) </param>
        /// <param name="autofit">오토피트(자동너비계산)</param>
        public void subMakeMsChartExcel(FpSpread oSpread, System.Windows.Forms.DataVisualization.Charting.Chart oMSChart,
                                 string sFileTitle, string sHeadL, string sHeadR, bool bAutoFit)
        {
            DialogResult dlg;
            bool IsMerge = true;
            bool bResult = true;
            int iSCol = 1;
            int iSRow = 0;
            int iMergeColSize = 0;
            int iChartRow = 0;

            int iTmp = 0;
            int jTmp = 0;

            try
            {
                if (oSpread.ActiveSheet.Rows.Count < 1)
                {
                    MessageBox.Show("저장할 Data가 없습니다.", "Excel");
                }

                if (oSpread.ActiveSheet.Rows.Count > 600)
                {
                    dlg = MessageBox.Show("데이타 건수가 많아 셀병합 작업시 속도가 느려집니다. 셀병합후 엑셀로 저장하시겠습니까?", "Excel Export", MessageBoxButtons.YesNo);
                    if (dlg == DialogResult.No)
                    {
                        IsMerge = false;
                    }
                }

                int iECol = 0;
                int iERow = 0;
                int iGridHeadCnt = 0;

                if (sFileTitle == null) sFileTitle = "";
                if (sHeadL == null) sHeadL = "";
                if (sHeadR == null) sHeadR = "";

                xlApp = new Excel.Application();
                xlBooks = xlApp.Workbooks;
                xlBook = xlBooks.Add(Excel.XlWBATemplate.xlWBATWorksheet);
                xlSheet = (Excel.Worksheet)xlBook.ActiveSheet;

                xlApp.Visible = false;
                //xlApp.Visible = true;

                xlApp.Cells.ClearContents();
                xlApp.Cells.ClearFormats();

                iGridHeadCnt = oSpread.ActiveSheet.ColumnHeaderRowCount;

                DataTable dt = null;
                String strSql = null;

                strSql = " SELECT USER_DESC FROM RWEBUSRDEF WHERE USER_ID = '" + GlobalVariable.gsUserID + "' ";
                dt = CmnFunction.oComm.GetFuncDataTable("DYNAMIC", strSql);

                if (sHeadR == "")
                    sHeadR = "사 용 자 : " + dt.Rows[0][0].ToString() + " (" + GlobalVariable.gsUserID + ")";
                else
                    sHeadR = sHeadR + "^사 용 자 : " + dt.Rows[0][0].ToString() + " (" + GlobalVariable.gsUserID + ")";

                if (oMSChart == null)
                {
                    //iSRow값을 내부에서 계산하게 수정.
                    if (sHeadL != "")
                        iTmp = sHeadL.Split('^').Length;

                    if (sHeadR != "")
                        jTmp = sHeadR.Split('^').Length;

                    iSRow = (iTmp > jTmp ? iTmp : jTmp) + 5 + 1;

                    //페이지 여백설정
                    subPageSetup(iSRow + iGridHeadCnt - 1, true);
                }
                else
                {
                    //iSRow값을 내부에서 계산하게 수정.
                    if (sHeadL != "")
                        iTmp = sHeadL.Split('^').Length;

                    if (sHeadR != "")
                        jTmp = sHeadR.Split('^').Length;

                    // 머릿말 Row수 + 타이틀이 차지하는 Row수(5)
                    iChartRow = (iTmp > jTmp ? iTmp : jTmp) + 5 + 1;

                    //Chart복사
                    iSRow = CopyMSChart(oMSChart, iChartRow);

                    //페이지 여백설정
                    subPageSetup(iSRow + iGridHeadCnt - 1, false);
                }

                //Header항목이 2라인일 경우
                iECol = CopySpView(oSpread, iGridHeadCnt, iSCol, iSRow, ref iMergeColSize); //Excel에서 마지막 Col의 위치값

                ColSize = iECol;

                iERow = oSpread.ActiveSheet.Rows.Count + iSRow + iGridHeadCnt - 1; //Excel에서 마지막 Row의 위치값

                //Header의 라인 작성(Head부분의 색적용, Data라인 작성)
                HeaderLine(iGridHeadCnt, iSCol, iSRow, iECol, iERow, iMergeColSize);
                                
                ////각각의 머리글을 작성
                setHeader(iSCol, iECol, sHeadL, sHeadR);
                                
                //파일의 타이틀을 작성
                setTitle(iSCol, iECol, sFileTitle);

                //Data부분 셀 Merge (600건 넘어가면 속도 작살임..)
                if (IsMerge == true)
                {
                    string[] tmpData = null;
                    int iRepeatRow = 0;

                    if (oSpread.ActiveSheet.Tag != null)
                    {
                        tmpData = oSpread.ActiveSheet.Tag.ToString().Split('^');
                        iRepeatRow = Convert.ToInt16(tmpData[1].ToString()) - 1;
                    }

                    DataMerge(iSCol, iSRow + iGridHeadCnt, iMrgECol, iERow, iMergeColSize, iRepeatRow);
                }

                xlSheet.get_Range(xlSheet.Cells[iSRow, iSCol], xlSheet.Cells[iSRow, iSCol]).Select();

                oSpread.ActiveSheet.SetActiveCell(iSRow, iSCol);

                if (bAutoFit == true)
                {
                    //왼쪽 머릿말과 오른쪽 머리말 부분을 뺀 나머지만 AutoFit을 한다.
                    if (sHeadL == "" && sHeadR == "")
                        xlSheet.get_Range(xlSheet.Cells[iSRow, 1], xlSheet.Cells[iSRow, iECol]).EntireColumn.AutoFit();
                    else if (sHeadL == "" && sHeadR != "")
                        xlSheet.get_Range(xlSheet.Cells[iSRow, 1], xlSheet.Cells[iSRow, iECol - 1]).EntireColumn.AutoFit();
                    else if (sHeadL != "" && sHeadR == "")
                        xlSheet.get_Range(xlSheet.Cells[iSRow, 2], xlSheet.Cells[iSRow, iECol]).EntireColumn.AutoFit();
                    else
                        xlSheet.get_Range(xlSheet.Cells[iSRow, 2], xlSheet.Cells[iSRow, iECol - 1]).EntireColumn.AutoFit();
                }

                bResult = true;

            }
            catch (Exception ex)
            {
                String errorMessage = "";
                errorMessage = String.Concat(errorMessage, ex.Message);
                errorMessage = String.Concat(errorMessage, " Line: ");
                errorMessage = String.Concat(errorMessage, ex.Source);

                CmnFunction.ShowMsgBox(errorMessage, "Error [" + ex.Source + "]", MessageBoxButtons.OK, 1);
                bResult = false;
            }
            finally
            {
                // 사용자에게 저장 여부를 묻는다.
                xlBook.Saved = false;

                System.Runtime.InteropServices.Marshal.ReleaseComObject(xlSheet);
                System.Runtime.InteropServices.Marshal.ReleaseComObject(xlBook);
                System.Runtime.InteropServices.Marshal.ReleaseComObject(xlBooks);

                if (bResult == false)
                {
                    //Make Excel application close.
                    xlApp.DisplayAlerts = false;
                    //xlBook.Saved = true;                    
                    xlBooks.Close();  // ***** 이 함수를 호출 하지 않으면 작업프로세스에 EXCEL.EXE가 죽지 않고 계속 남아 있음 *****
                    xlApp.Quit();
                }
                else
                {
                    // 모든 엑셀의 경고메시지가 나타나도록 한다.
                    //xlApp.DisplayAlerts = true;
                    //Make Excel visible and give the user control.
                    xlApp.Visible = true;
                    xlApp.UserControl = true;
                }

                System.Runtime.InteropServices.Marshal.ReleaseComObject(xlApp);
                GC.Collect();
                System.Windows.Forms.Cursor.Current = Cursors.Default;
            }

        } //public void subMakeExcel(FpSpread oSpread, Chart oChartFx, string sFileTitle, string sHeadL, string sHeadR)




        private void subPageSetup(int iPrtTitleRow, bool IsPrintTitleRows)
        {
            string sPrnDate = "조회일자 : " + DateTime.Now.ToString("yyyy-MM-dd");
            
            if(IsPrintTitleRows ==true)
                xlSheet.PageSetup.PrintTitleRows = "$1:$" + iPrtTitleRow;

            xlSheet.PageSetup.LeftFooter = "&10하나마이크론";
            xlSheet.PageSetup.CenterFooter = "&10페이지 : &P/&N";
            xlSheet.PageSetup.RightFooter = "&10" + sPrnDate;
            //xlSheet.PageSetup.LeftMargin = xlApp.InchesToPoints(0.4);            
            //xlSheet.PageSetup.RightMargin = xlApp.InchesToPoints(0.4);
            //xlSheet.PageSetup.TopMargin = xlApp.InchesToPoints(0.4);
            //xlSheet.PageSetup.BottomMargin = xlApp.InchesToPoints(0.67);
            //xlSheet.PageSetup.HeaderMargin = xlApp.InchesToPoints(0.4);
            //xlSheet.PageSetup.FooterMargin = xlApp.InchesToPoints(0.4);
            xlSheet.PageSetup.LeftMargin = xlApp.InchesToPoints(0.0);
            xlSheet.PageSetup.RightMargin = xlApp.InchesToPoints(0.0);
            xlSheet.PageSetup.TopMargin = xlApp.InchesToPoints(0.0);

            // 2010-09-07-임종우 : 엑셀 출력 시 하단에 꼬리말이 데이터와 곁쳐 여백 설정함.
            xlSheet.PageSetup.BottomMargin = xlApp.InchesToPoints(0.5);

            xlSheet.PageSetup.HeaderMargin = xlApp.InchesToPoints(0.0);
            xlSheet.PageSetup.FooterMargin = xlApp.InchesToPoints(0.0);
            xlSheet.PageSetup.CenterHorizontally=true ;
            xlSheet.PageSetup.Orientation =  Excel.XlPageOrientation.xlLandscape; //페이지 설정 가로
            xlSheet.PageSetup.PaperSize =  Excel.XlPaperSize.xlPaperA4; //xlPaperA4                        
            xlSheet.PageSetup.Zoom = false ;
            xlSheet.PageSetup.FitToPagesWide = 1;
            xlSheet.PageSetup.FitToPagesTall = false ;            
        }

        private int CopySpView(FpSpread oSpread, int iGridHeadCnt,
                               int iSCol, int iSRow, ref int iMergeColSize)
        {
            int iMrgDelCnt = 0;
            int iFrozenColumn = 0;  //틀고정 칼럼(칼럼 Merge시 1부터 여기까지만 작업한다. - 속도 때문에)

            string[] tmpData = null;

            iFrozenColumn = oSpread.ActiveSheet.FrozenColumnCount - 1;

            if (iFrozenColumn <= 0) iFrozenColumn = 8;
            
            FarPoint.Win.Spread.Model.BaseSheetSelectionModel selModel;
            selModel = (FarPoint.Win.Spread.Model.BaseSheetSelectionModel)oSpread.ActiveSheet.Models.Selection;            
            FarPoint.Win.Spread.OperationMode iMyMode;                       
            
            iMyMode =   oSpread.ActiveSheet.OperationMode ;
            oSpread.ActiveSheet.OperationMode = FarPoint.Win.Spread.OperationMode.Normal;

            int iDelCnt = 0;
            
            oSpread.ClipboardOptions = FarPoint.Win.Spread.ClipboardOptions.ColumnHeaders;
            
            oSpread.ActiveSheet.AddSelection(-1, -1, oSpread.ActiveSheet.Rows.Count, oSpread.ActiveSheet.Columns.Count);

            oSpread.ActiveSheet.ClipboardCopy();
            oSpread.ActiveSheet.OperationMode = iMyMode;
            oSpread.ActiveSheet.SetActiveCell(iSRow, iSCol);

            ((Excel.Range)xlSheet.Cells[iSRow, iSCol]).Select();
            xlSheet._PasteSpecial(Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing);

            xlSheet.Cells.VerticalAlignment = Excel.XlVAlign.xlVAlignCenter;
            xlSheet.Cells.Font.Name = "Tahoma";
            xlSheet.Cells.Font.Size = 10;
            xlSheet.Cells.Orientation = 0;
            xlSheet.Cells.AddIndent = false;
            xlSheet.Cells.ShrinkToFit = false;

            //숨겨진 컬럼처리 루틴
            for (int i = oSpread.ActiveSheet.ColumnCount-1; i >= 0 ; i--)
            {
                if (oSpread.ActiveSheet.Columns[i].Visible == false)
                {
                    xlSheet.get_Range(xlSheet.Cells[1, i + iSCol], xlSheet.Cells[oSpread.ActiveSheet.RowCount + iSRow + iGridHeadCnt-1, i + iSCol]).Delete(Excel.XlDirection.xlToLeft);
                    iDelCnt++;

                    //칼럼 Merge는 틀고정을 한 위치부터 실시해야됨.
                    if (iFrozenColumn > i)
                        iMrgDelCnt++;
                }
            }
             
            iMrgECol = iFrozenColumn - iMrgDelCnt;

            if (oSpread.ActiveSheet.Tag == null)
            {
                iMergeColSize = iMrgECol + 1;
            }
            else
            {
                tmpData = oSpread.ActiveSheet.Tag.ToString().Split('^');
                iMergeColSize = Convert.ToInt16(tmpData[0].ToString()) ;
            }

            return oSpread.ActiveSheet.ColumnCount + iSCol - iDelCnt - 1;
             
        }

        private void HeaderLine(int iGridHeadCnt,
                                int iSCol, int iSRow,
                                int iECol, int iERow, int iMergeColSize)
        {
            bool isV = false;
            bool isH = false;
            string[] tmpHeadMrg = null;
            string tmpHeadInx = null;
            string sValue = null;
            int i = 0;
            int iCSRow = 0;
            int iCERow = 0;

            if (iSCol < iECol) isV = true;
            if (iSRow < iERow) isH = true;

            LineDraw(iSCol, iSRow, iECol, iERow, isV, isH);

            if (iGridHeadCnt == 2 || iGridHeadCnt == 3)
            {
                //각 Merge할 셀의 위치정보파악
                tmpHeadInx = "";
                for (i = 0; i <= iECol-2; i++)
                {
                    if (xlSheet.get_Range(xlSheet.Cells[iSRow, iSCol + i], xlSheet.Cells[iSRow, iSCol + i]).Text.ToString() != "")
                        tmpHeadInx += Convert.ToString(iSCol + i) + ",";

                }

                if (xlSheet.get_Range(xlSheet.Cells[iSRow, iECol], xlSheet.Cells[iSRow, iECol]).Text.ToString() != "")
                    tmpHeadInx += Convert.ToString(iECol);
                else
                    tmpHeadInx = tmpHeadInx.Substring(0, tmpHeadInx.Length - 1);

                tmpHeadMrg = tmpHeadInx.Split(',');

                //가로 데이타 머지.
                for (i = 0; i < tmpHeadMrg.Length-1; i++)
                {
                    xlSheet.get_Range(xlSheet.Cells[iSRow, Convert.ToInt16(tmpHeadMrg[i])], xlSheet.Cells[iSRow, Convert.ToInt16(tmpHeadMrg[i + 1]) - 1]).MergeCells = true;
                    
                    if (xlSheet.get_Range(xlSheet.Cells[iSRow + 1, Convert.ToInt16(tmpHeadMrg[i])], xlSheet.Cells[iSRow + 1, Convert.ToInt16(tmpHeadMrg[i])]).Text.ToString() == "")
                    {
                        xlSheet.get_Range(xlSheet.Cells[iSRow, Convert.ToInt16(tmpHeadMrg[i])], xlSheet.Cells[iSRow + 1, Convert.ToInt16(tmpHeadMrg[i])]).MergeCells = true;
                        
                    }
                }

                xlSheet.get_Range(xlSheet.Cells[iSRow, Convert.ToInt16(tmpHeadMrg[i])], xlSheet.Cells[iSRow, iECol]).MergeCells = true;

                if (xlSheet.get_Range(xlSheet.Cells[iSRow + 1, Convert.ToInt16(tmpHeadMrg[i])], xlSheet.Cells[iSRow + 1, Convert.ToInt16(tmpHeadMrg[i])]).Text.ToString() == "")
                {
                    xlSheet.get_Range(xlSheet.Cells[iSRow, Convert.ToInt16(tmpHeadMrg[i])], xlSheet.Cells[iSRow + 1, Convert.ToInt16(tmpHeadMrg[i])]).MergeCells = true;

                }

                //세로 데이타 머지
                for (int iC = iSCol; iC <= iMergeColSize; iC++)
                {
                    //비교 대상 초기값
                    sValue = xlSheet.get_Range(xlSheet.Cells[iSRow, iC], xlSheet.Cells[iSRow, iC]).Text.ToString();
                    iCSRow = iSRow;

                    for (int iR = iSRow + 1; iR <= iSRow +iGridHeadCnt-1; iR++)
                    {
                        if (sValue == xlSheet.get_Range(xlSheet.Cells[iR, iC], xlSheet.Cells[iR, iC]).Text.ToString())
                        {                            
                            iCERow = iR ;
                            Excel.Range oRangeMgr = xlSheet.get_Range(xlSheet.Cells[iCSRow, iC], xlSheet.Cells[iCERow, iC]);

                            oRangeMgr.ClearContents();
                            oRangeMgr.MergeCells = true;
                            oRangeMgr.Value2 = sValue;
                            oRangeMgr.HorizontalAlignment = Excel.XlHAlign.xlHAlignLeft;
                            oRangeMgr.VerticalAlignment = Excel.XlVAlign.xlVAlignCenter;
                            
                            iCSRow = iR;
                            sValue = xlSheet.get_Range(xlSheet.Cells[iR, iC], xlSheet.Cells[iR, iC]).Text.ToString();
                        }
                    }
                }
            }

            //if (iGridHeadCnt == 3)
            //{
            //    //각 Merge할 셀의 위치정보파악
            //    tmpHeadInx = "";

            //    for (int x = 0; x < 2; x++)
            //    {
            //        tmpHeadInx = "";
            //        tmpHeadMrg = null;

            //        for (i = 0; i <= iECol - 2; i++)
            //        {
            //            if (xlSheet.get_Range(xlSheet.Cells[iSRow + x, iSCol + i], xlSheet.Cells[iSRow + x, iSCol + i]).Text.ToString() != "")
            //                tmpHeadInx += Convert.ToString(iSCol + i) + ",";

            //        }

            //        if (xlSheet.get_Range(xlSheet.Cells[iSRow + x, iECol], xlSheet.Cells[iSRow + x, iECol]).Text.ToString() != "")
            //            tmpHeadInx += Convert.ToString(iECol);
            //        else
            //            tmpHeadInx = tmpHeadInx.Substring(0, tmpHeadInx.Length - 1);

            //        tmpHeadMrg = tmpHeadInx.Split(',');

            //        //가로 데이타 머지.
            //        for (i = 0; i < tmpHeadMrg.Length - 1; i++)
            //        {
            //            xlSheet.get_Range(xlSheet.Cells[iSRow + x, Convert.ToInt16(tmpHeadMrg[i])], xlSheet.Cells[iSRow + x, Convert.ToInt16(tmpHeadMrg[i + 1]) - 1]).MergeCells = true;

            //            if (xlSheet.get_Range(xlSheet.Cells[iSRow + 1 + x, Convert.ToInt16(tmpHeadMrg[i])], xlSheet.Cells[iSRow + 1 + x, Convert.ToInt16(tmpHeadMrg[i])]).Text.ToString() == "")
            //            {
            //                xlSheet.get_Range(xlSheet.Cells[iSRow + x, Convert.ToInt16(tmpHeadMrg[i])], xlSheet.Cells[iSRow + 1 + x, Convert.ToInt16(tmpHeadMrg[i])]).MergeCells = true;

            //            }
            //        }

            //        xlSheet.get_Range(xlSheet.Cells[iSRow + x, Convert.ToInt16(tmpHeadMrg[i])], xlSheet.Cells[iSRow + x, iECol]).MergeCells = true;

            //        if (xlSheet.get_Range(xlSheet.Cells[iSRow + 1 + x, Convert.ToInt16(tmpHeadMrg[i])], xlSheet.Cells[iSRow + 1 + x, Convert.ToInt16(tmpHeadMrg[i])]).Text.ToString() == "")
            //        {
            //            xlSheet.get_Range(xlSheet.Cells[iSRow + x, Convert.ToInt16(tmpHeadMrg[i])], xlSheet.Cells[iSRow + 1 + x, Convert.ToInt16(tmpHeadMrg[i])]).MergeCells = true;

            //        }


            //        //세로 데이타 머지
            //        for (int iC = iSCol; iC <= iMergeColSize; iC++)
            //        {
            //            //비교 대상 초기값
            //            sValue = xlSheet.get_Range(xlSheet.Cells[iSRow, iC], xlSheet.Cells[iSRow, iC]).Text.ToString();
            //            iCSRow = iSRow;

            //            for (int iR = iSRow + 1; iR <= iSRow + iGridHeadCnt - 1; iR++)
            //            {
            //                if (sValue == xlSheet.get_Range(xlSheet.Cells[iR, iC], xlSheet.Cells[iR, iC]).Text.ToString())
            //                {
            //                    iCERow = iR;
            //                    Excel.Range oRangeMgr = xlSheet.get_Range(xlSheet.Cells[iCSRow, iC], xlSheet.Cells[iCERow, iC]);

            //                    oRangeMgr.ClearContents();
            //                    oRangeMgr.MergeCells = true;
            //                    oRangeMgr.Value2 = sValue;
            //                    oRangeMgr.HorizontalAlignment = Excel.XlHAlign.xlHAlignLeft;
            //                    oRangeMgr.VerticalAlignment = Excel.XlVAlign.xlVAlignCenter;

            //                    iCSRow = iR;
            //                    sValue = xlSheet.get_Range(xlSheet.Cells[iR, iC], xlSheet.Cells[iR, iC]).Text.ToString();
            //                }
            //            }
            //        }
            //    }
            //}

            Excel.Range oRange = (Excel.Range)xlSheet.get_Range(xlSheet.Cells[iSRow, iSCol], xlSheet.Cells[iSRow + iGridHeadCnt - 1, iECol]);

            oRange.HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
            oRange.Font.Size = 10;
            oRange.Font.Bold = true;
            oRange.RowHeight = 17;
            oRange.Interior.ColorIndex = 41;
            oRange.Font.ColorIndex = 2;
            
            //헤더 아래의 선 
            //oRange.Borders[Excel.XlBordersIndex.xlEdgeBottom].LineStyle = Excel.XlLineStyle.xlContinuous;
            //oRange.Borders[Excel.XlBordersIndex.xlEdgeBottom].Weight = Excel.XlBorderWeight.xlMedium;
            //oRange.Borders[Excel.XlBordersIndex.xlEdgeBottom].ColorIndex = Excel.XlColorIndex.xlColorIndexAutomatic;          

        }

        private void LineDraw(int iSCol, int iSRow,int iECol, int iERow, bool isVertical, bool isHorizental)
        {
            Excel.Range oRange = (Excel.Range)xlSheet.get_Range(xlSheet.Cells[iSRow, iSCol], xlSheet.Cells[iERow, iECol]);

            //해당 Range에 라인을 그린다.
            oRange.Borders[Excel.XlBordersIndex.xlDiagonalDown].LineStyle = Excel.XlLineStyle.xlLineStyleNone;
            oRange.Borders[Excel.XlBordersIndex.xlDiagonalUp].LineStyle = Excel.XlLineStyle.xlLineStyleNone;

            oRange.Borders[Excel.XlBordersIndex.xlEdgeLeft].LineStyle = Excel.XlLineStyle.xlContinuous;
            oRange.Borders[Excel.XlBordersIndex.xlEdgeLeft].Weight = Excel.XlBorderWeight.xlMedium;
            oRange.Borders[Excel.XlBordersIndex.xlEdgeLeft].ColorIndex = Excel.XlColorIndex.xlColorIndexAutomatic;

            oRange.Borders[Excel.XlBordersIndex.xlEdgeTop].LineStyle = Excel.XlLineStyle.xlContinuous;
            oRange.Borders[Excel.XlBordersIndex.xlEdgeTop].Weight = Excel.XlBorderWeight.xlMedium;
            oRange.Borders[Excel.XlBordersIndex.xlEdgeTop].ColorIndex = Excel.XlColorIndex.xlColorIndexAutomatic;

            oRange.Borders[Excel.XlBordersIndex.xlEdgeBottom].LineStyle = Excel.XlLineStyle.xlContinuous;
            oRange.Borders[Excel.XlBordersIndex.xlEdgeBottom].Weight = Excel.XlBorderWeight.xlMedium;
            oRange.Borders[Excel.XlBordersIndex.xlEdgeBottom].ColorIndex = Excel.XlColorIndex.xlColorIndexAutomatic;

            oRange.Borders[Excel.XlBordersIndex.xlEdgeRight].LineStyle = Excel.XlLineStyle.xlContinuous;
            oRange.Borders[Excel.XlBordersIndex.xlEdgeRight].Weight = Excel.XlBorderWeight.xlMedium;
            oRange.Borders[Excel.XlBordersIndex.xlEdgeRight].ColorIndex = Excel.XlColorIndex.xlColorIndexAutomatic;

            if (isVertical == true)
            {
                oRange.Borders[Excel.XlBordersIndex.xlInsideVertical].LineStyle = Excel.XlLineStyle.xlContinuous;
                oRange.Borders[Excel.XlBordersIndex.xlInsideVertical].Weight = Excel.XlBorderWeight.xlThin;
                oRange.Borders[Excel.XlBordersIndex.xlInsideVertical].ColorIndex = Excel.XlColorIndex.xlColorIndexAutomatic;
            }

            if (isHorizental == true)
            {
                oRange.Borders[Excel.XlBordersIndex.xlInsideHorizontal].LineStyle = Excel.XlLineStyle.xlContinuous;
                oRange.Borders[Excel.XlBordersIndex.xlInsideHorizontal].Weight = Excel.XlBorderWeight.xlThin;
                oRange.Borders[Excel.XlBordersIndex.xlInsideHorizontal].ColorIndex = Excel.XlColorIndex.xlColorIndexAutomatic;
            }
        }

        private void setHeader(int iSCol, int iECol, string sHeadL, string sHeadR)
        {
            string[] tmpHeadL = null;
            string[] tmpHeadR = null;

            tmpHeadL = sHeadL.Split('^');
            tmpHeadR = sHeadR.Split('^');

            for (int i = 0; i < tmpHeadL.Length; i++)
            {
                Excel.Range oRange = xlSheet.get_Range(xlSheet.Cells[5 + i, iSCol], xlSheet.Cells[5 + i, iSCol]);
                
                oRange.Value2= tmpHeadL[i];
                oRange.HorizontalAlignment = Excel.XlHAlign.xlHAlignLeft;
                oRange.WrapText = false;
            }

            for (int i = 0; i < tmpHeadR.Length; i++)
            {
                Excel.Range oRange = xlSheet.get_Range(xlSheet.Cells[5 + i, iECol], xlSheet.Cells[5 + i, iECol]);

                oRange.Value2 = tmpHeadR[i];
                oRange.HorizontalAlignment = Excel.XlHAlign.xlHAlignRight;
                oRange.WrapText = false;
            }

        }

        private void setTitle(int iSCol, int iECol, string sFileTitle)
        {
            try
            {
                int iBoxWidth = 0;
                double dWithSum = 0.0;

                for ( int i = iSCol; i <= iECol; i++)
                    dWithSum += Convert.ToDouble(xlSheet.get_Range(xlSheet.Cells[1, i], xlSheet.Cells[1, i]).ColumnWidth);

                //Column항목이 적을 경우 전체 컬럼크기를 키운다.
                double dTmpVal = 0.0;
                int iIncVal = 0;
                int iTmpColCnt = 0;

                iTmpColCnt = iECol - iSCol + 1;

                if (dWithSum < 112)
                {
                    dTmpVal = (112 - dWithSum) / iTmpColCnt;
                    iIncVal = Convert.ToInt16(dTmpVal);

                    for (int i = iSCol; i <= iECol; i++)
                        xlSheet.get_Range(xlSheet.Cells[1, i], xlSheet.Cells[1, i]).ColumnWidth =
                              Convert.ToDouble(xlSheet.get_Range(xlSheet.Cells[1, i], xlSheet.Cells[1, i]).ColumnWidth) + Convert.ToDouble(iIncVal);
                }

                dWithSum = 0;

                for (int i = iSCol; i <= iECol; i++)
                    dWithSum += Convert.ToDouble(xlSheet.get_Range(xlSheet.Cells[1, i], xlSheet.Cells[1, i]).Width);


                //iBoxWidth = Convert.ToInt16(dWithSum / 4);

                if (iBoxWidth < 175) iBoxWidth = 175;

                if (iBoxWidth > 250) iBoxWidth = 200;
                
                Excel.Shape oTitle = xlSheet.Shapes.AddTextbox(MsoTextOrientation.msoTextOrientationHorizontal, iBoxWidth, 5, iBoxWidth * 2, 30);
                oTitle.Line.DashStyle = Microsoft.Office.Core.MsoLineDashStyle.msoLineSolid;
                oTitle.Line.Style = Microsoft.Office.Core.MsoLineStyle.msoLineThinThick;                
                oTitle.Line.ForeColor.SchemeColor = 64;
                oTitle.Shadow.Type = Microsoft.Office.Core.MsoShadowType.msoShadow6;
                oTitle.Placement = Excel.XlPlacement.xlFreeFloating;
                
                //oTitle.TextFrame.Characters(1, sFileTitle.Length).Text = sFileTitle;
                oTitle.TextFrame.Characters(0,1).Text = sFileTitle;
                oTitle.TextFrame.HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
                oTitle.TextFrame.VerticalAlignment = Excel.XlVAlign.xlVAlignCenter;

                oTitle.TextFrame.Characters(1, sFileTitle.Length).Font.Size = 18;
                oTitle.TextFrame.Characters(1, sFileTitle.Length).Font.FontStyle = "bold";
                oTitle.TextFrame.Characters(1, sFileTitle.Length).Font.Strikethrough = false;
            }
            catch (Exception)
            {
                //DO Nothing
            }
        }

        private void DataMerge(int iSCol, int iSRow, int iECol, int iERow, int iMergeColSize, int iRepeatRow)
        {
            int iCSRow = 0;
            int iCERow = 0;
            string sValue = "";
            string sNextValue = "";

            for (int iC = iSCol; iC <= iMergeColSize; iC++)
            {
                //비교 대상 초기값
                sValue = xlSheet.get_Range(xlSheet.Cells[iSRow, iC], xlSheet.Cells[iSRow, iC]).Text.ToString();
                iCSRow = iSRow;

                for (int iR = iSRow + 1; iR <= iERow + 1; iR++)
                {
                    if (iR < iERow + 1)
                        sNextValue = xlSheet.get_Range(xlSheet.Cells[iR, iC], xlSheet.Cells[iR, iC]).Text.ToString();
                    else
                        sNextValue = "!@#$%^&*()";  //Dummy

                    //Sub Total Row의 Column병합을막기위해서.
                    if (sValue.IndexOf("Sub Total") > 0 || sValue == "Total")
                    {
                        //xlSheet.get_Range(xlSheet.Cells[iR, iC], xlSheet.Cells[iR + iRepeatRow, iMergeColSize]);
                        Excel.Range oRange = xlSheet.get_Range(xlSheet.Cells[iCSRow, iC], xlSheet.Cells[iCSRow + iRepeatRow, iMergeColSize]);
                        oRange.Value2 = sValue;
                    }

                    if (sValue != sNextValue)
                    {
                        //두칸이상인 경우만 Merge해줌(세로 데이타 머지)
                        if ((iR - iCSRow) > 1)
                        {
                            iCERow = iR - 1;

                            Excel.Range oRange = xlSheet.get_Range(xlSheet.Cells[iCSRow, iC], xlSheet.Cells[iCERow, iC]);

                            oRange.ClearContents();
                            oRange.MergeCells = true;
                            oRange.Value2 = sValue;
                            oRange.HorizontalAlignment = Excel.XlHAlign.xlHAlignLeft;
                            oRange.VerticalAlignment = Excel.XlVAlign.xlVAlignTop;
                        }

                        iCSRow = iR;
                        sValue = xlSheet.get_Range(xlSheet.Cells[iR, iC], xlSheet.Cells[iR, iC]).Text.ToString();
                    } 
                }
            } 

            //Total이나 Sub Total부분 셀 Merge
            for (int iR = iSRow; iR <= iERow; iR = iR + iRepeatRow+1)
            {
                for (int iC = iMergeColSize - 1; iC >= iSCol; iC--)
                {
                    //비교 대상 초기값
                    sValue = xlSheet.get_Range(xlSheet.Cells[iR, iC], xlSheet.Cells[iR, iC]).Text.ToString();

                    //- Sub Total인 경우 데이타가 엑셀에서 깨어짐. 이값을 보증하기 위한 루틴.
                    if (sValue == "#NAME?")
                    {
                        sValue = xlSheet.get_Range(xlSheet.Cells[iR, iC], xlSheet.Cells[iR, iC]).Formula.ToString();
                        sValue = sValue.Replace("=", " ");
                    }

                    if (sValue.IndexOf("Sub Total") > 0 || sValue == "Total")
                    {
                        Excel.Range oRangeMgr = xlSheet.get_Range(xlSheet.Cells[iR, iC], xlSheet.Cells[iR + iRepeatRow, iMergeColSize]);
                        
                        oRangeMgr.ClearContents();                        
                        oRangeMgr.MergeCells = true;
                        oRangeMgr.Value2 = sValue;


                        // SubTotal 과 Total 부분 Row에 BackColor 
                        Excel.Range oRange2 = xlSheet.get_Range(xlSheet.Cells[iR, iC], xlSheet.Cells[iR + iRepeatRow, ColSize]);
                        oRange2.Interior.ColorIndex = 38;
                        oRange2.Font.ColorIndex = 1;

                        if (sValue =="Total")
                            oRangeMgr.HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
                        else
                            oRangeMgr.HorizontalAlignment = Excel.XlHAlign.xlHAlignLeft;

                        oRangeMgr.VerticalAlignment = Excel.XlVAlign.xlVAlignCenter;
                    }                    
                }
            }

        }

        private int CopyChart(SoftwareFX.ChartFX.Chart oChartFx, int iChartRow)
        {
            string sImageFileName = null;
            int iRowCnt = 0;
            double dRowHeight = 0;
            double dPicHeight = 0;

            Excel.Pictures oPic;           

            sImageFileName = Environment.GetFolderPath(Environment.SpecialFolder.MyPictures) 
                           + @"\Tmp_Chart" 
                           + DateTime.Now.Year.ToString() 
                           + DateTime.Now.Month.ToString()
                           + DateTime.Now.Day.ToString()
                           + DateTime.Now.Hour.ToString()
                           + DateTime.Now.Minute.ToString()
                           + DateTime.Now.Second.ToString()
                           + ".bmp";

            oChartFx.Export(SoftwareFX.ChartFX.FileFormat.Bitmap, sImageFileName);

            xlSheet.get_Range(xlSheet.Cells[iChartRow, 1], xlSheet.Cells[iChartRow, 1]).Select();
                        
            oPic = (Excel.Pictures)xlSheet.Pictures(Missing.Value);
            oPic.Insert(sImageFileName, Missing.Value).Select(true);
            dPicHeight = oPic.Insert(sImageFileName, Missing.Value).Height;                     

            //Row의 폭
            dRowHeight = Convert.ToDouble(xlSheet.get_Range(xlSheet.Cells[iChartRow, 1], xlSheet.Cells[iChartRow, 1]).Height);
            
            //그림의 폭           
            iRowCnt = Convert.ToInt16((dPicHeight / dRowHeight)) + 2;            
                    
            System.IO.File.Delete(sImageFileName);

            return iChartRow+iRowCnt;
        }

        private int CopyMSChart(System.Windows.Forms.DataVisualization.Charting.Chart oMSChart, int iChartRow)
        {
            //MS Chart 사용 로직 추가

            string sImageFileName = null;
            int iRowCnt = 0;
            double dRowHeight = 0;
            double dPicHeight = 0;

            Excel.Pictures oPic;

            sImageFileName = Environment.GetFolderPath(Environment.SpecialFolder.MyPictures)
                           + @"\Tmp_Chart"
                           + DateTime.Now.Year.ToString()
                           + DateTime.Now.Month.ToString()
                           + DateTime.Now.Day.ToString()
                           + DateTime.Now.Hour.ToString()
                           + DateTime.Now.Minute.ToString()
                           + DateTime.Now.Second.ToString()
                           + ".bmp";

            //oChartFx.Export(SoftwareFX.ChartFX.FileFormat.Bitmap, sImageFileName);
            oMSChart.SaveImage(sImageFileName, System.Windows.Forms.DataVisualization.Charting.ChartImageFormat.Bmp);

            xlSheet.get_Range(xlSheet.Cells[iChartRow, 1], xlSheet.Cells[iChartRow, 1]).Select();

            oPic = (Excel.Pictures)xlSheet.Pictures(Missing.Value);
            oPic.Insert(sImageFileName, Missing.Value).Select(true);
            dPicHeight = oPic.Insert(sImageFileName, Missing.Value).Height;

            //Row의 폭
            dRowHeight = Convert.ToDouble(xlSheet.get_Range(xlSheet.Cells[iChartRow, 1], xlSheet.Cells[iChartRow, 1]).Height);

            //그림의 폭           
            iRowCnt = Convert.ToInt16((dPicHeight / dRowHeight)) + 2;

            System.IO.File.Delete(sImageFileName);

            return iChartRow + iRowCnt;
        }


        #endregion		
	
	}
}
