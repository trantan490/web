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
	/// ExcelHelper�� ���� ��� �����Դϴ�.
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
        
        #region Spread �� Chart�� ������ ������ Export

        /// <summary>
        /// Spread�� ������ ������ Export��.������ġ�� ���ο��� �ڵ� ���.
        /// </summary>
        /// <param name="oSpread"> Spread��Ʈ�� �� </param>
        /// <param name="sFileTitle"> ȭ�� �� </param>
        /// <param name="sHeadL"> ���� �Ӹ���(����) </param>
        /// <param name="sHeadR"> ���� �Ӹ���(������) </param>
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
        /// Spread�� Chart�� ������ ������ Export��.������ġ�� ���ο��� �ڵ� ���.
        /// </summary>
        /// <param name="oSpread"> Spread��Ʈ�� �� </param>
        /// <param name="oChartFx"> ChartFX��Ʈ�� �� �� </param>
        /// <param name="sFileTitle"> ȭ�� �� </param>
        /// <param name="sHeadL"> ���� �Ӹ���(����) </param>
        /// <param name="sHeadR"> ���� �Ӹ���(������) </param>        
        public void subMakeExcel(FpSpread oSpread, SoftwareFX.ChartFX.Chart oChartFx,
                                 string sFileTitle, string sHeadL, string sHeadR)
        {
            this.subMakeExcel(oSpread, oChartFx, sFileTitle, sHeadL, sHeadR, true);
        }
        
        /// <summary>
        /// Spread�� Chart�� ������ ������ Export��.������ġ�� ���ο��� �ڵ� ���.
        /// </summary>
        /// <param name="oSpread"> Spread��Ʈ�� �� </param>
        /// <param name="oChartFx"> ChartFX��Ʈ�� �� �� </param>
        /// <param name="sFileTitle"> ȭ�� �� </param>
        /// <param name="sHeadL"> ���� �Ӹ���(����) </param>
        /// <param name="sHeadR"> ���� �Ӹ���(������) </param>
        /// <param name="autofit">������Ʈ(�ڵ��ʺ���)</param>
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
                    MessageBox.Show("������ Data�� �����ϴ�.", "Excel");
                }

                if (oSpread.ActiveSheet.Rows.Count > 600)
                {
                    dlg = MessageBox.Show("����Ÿ �Ǽ��� ���� ������ �۾��� �ӵ��� �������ϴ�. �������� ������ �����Ͻðڽ��ϱ�?", "Excel Export", MessageBoxButtons.YesNo);
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
                    sHeadR = "�� �� �� : " + dt.Rows[0][0].ToString() + " (" + GlobalVariable.gsUserID + ")";
                else
                    sHeadR = sHeadR + "^�� �� �� : " + dt.Rows[0][0].ToString() + " (" + GlobalVariable.gsUserID + ")";

                if (oChartFx == null)
                {
                    //iSRow���� ���ο��� ����ϰ� ����.
                    if (sHeadL != "")
                        iTmp = sHeadL.Split('^').Length;

                    if (sHeadR != "")
                        jTmp = sHeadR.Split('^').Length;

                    iSRow = (iTmp > jTmp ? iTmp : jTmp) + 5 + 1;

                    //������ ���鼳��
                    subPageSetup(iSRow + iGridHeadCnt - 1, true);
                }
                else
                {
                    //iSRow���� ���ο��� ����ϰ� ����.
                    if (sHeadL != "")
                        iTmp = sHeadL.Split('^').Length;

                    if (sHeadR != "")
                        jTmp = sHeadR.Split('^').Length;

                    // �Ӹ��� Row�� + Ÿ��Ʋ�� �����ϴ� Row��(5)
                    iChartRow = (iTmp > jTmp ? iTmp : jTmp) + 5 + 1;

                    //Chart����
                    iSRow = CopyChart(oChartFx, iChartRow);

                    //������ ���鼳��
                    subPageSetup(iSRow + iGridHeadCnt - 1, false);
                }

                //Header�׸��� 2������ ���
                iECol = CopySpView(oSpread, iGridHeadCnt, iSCol, iSRow, ref iMergeColSize); //Excel���� ������ Col�� ��ġ��

                ColSize = iECol;

                iERow = oSpread.ActiveSheet.Rows.Count + iSRow + iGridHeadCnt - 1; //Excel���� ������ Row�� ��ġ��

                //Header�� ���� �ۼ�(Head�κ��� ������, Data���� �ۼ�)
                HeaderLine(iGridHeadCnt, iSCol, iSRow, iECol, iERow, iMergeColSize);

                ////������ �Ӹ����� �ۼ�
                setHeader(iSCol, iECol, sHeadL, sHeadR);

                //������ Ÿ��Ʋ�� �ۼ�
                setTitle(iSCol, iECol, sFileTitle);

                //Data�κ� �� Merge (600�� �Ѿ�� �ӵ� �ۻ���..)
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
                    //���� �Ӹ����� ������ �Ӹ��� �κ��� �� �������� AutoFit�� �Ѵ�.
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
                // ����ڿ��� ���� ���θ� ���´�.
                xlBook.Saved = false;

                System.Runtime.InteropServices.Marshal.ReleaseComObject(xlSheet);
                System.Runtime.InteropServices.Marshal.ReleaseComObject(xlBook);
                System.Runtime.InteropServices.Marshal.ReleaseComObject(xlBooks);

                if (bResult == false)
                {
                    //Make Excel application close.
                    xlApp.DisplayAlerts = false;
                    //xlBook.Saved = true;                    
                    xlBooks.Close();  // ***** �� �Լ��� ȣ�� ���� ������ �۾����μ����� EXCEL.EXE�� ���� �ʰ� ��� ���� ���� *****
                    xlApp.Quit();
                }
                else
                {
                    // ��� ������ ���޽����� ��Ÿ������ �Ѵ�.
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
        /// Spread�� Chart�� ������ ������ Export��.������ġ�� ���ο��� �ڵ� ���.
        /// </summary>
        /// <param name="oSpread"> Spread��Ʈ�� �� </param>
        /// <param name="oChartFx"> MSChart��Ʈ�� �� �� </param>
        /// <param name="sFileTitle"> ȭ�� �� </param>
        /// <param name="sHeadL"> ���� �Ӹ���(����) </param>
        /// <param name="sHeadR"> ���� �Ӹ���(������) </param>        
        public void subMakeMsChartExcel(FpSpread oSpread, System.Windows.Forms.DataVisualization.Charting.Chart oMSChart,
                                 string sFileTitle, string sHeadL, string sHeadR)
        {
            this.subMakeMsChartExcel(oSpread, oMSChart, sFileTitle, sHeadL, sHeadR, true);
        }

        /// <summary>
        /// Spread�� Chart�� ������ ������ Export��.������ġ�� ���ο��� �ڵ� ���.
        /// </summary>
        /// <param name="oSpread"> Spread��Ʈ�� �� </param>
        /// <param name="oChartFx"> MSChart��Ʈ�� �� �� </param>
        /// <param name="sFileTitle"> ȭ�� �� </param>
        /// <param name="sHeadL"> ���� �Ӹ���(����) </param>
        /// <param name="sHeadR"> ���� �Ӹ���(������) </param>
        /// <param name="autofit">������Ʈ(�ڵ��ʺ���)</param>
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
                    MessageBox.Show("������ Data�� �����ϴ�.", "Excel");
                }

                if (oSpread.ActiveSheet.Rows.Count > 600)
                {
                    dlg = MessageBox.Show("����Ÿ �Ǽ��� ���� ������ �۾��� �ӵ��� �������ϴ�. �������� ������ �����Ͻðڽ��ϱ�?", "Excel Export", MessageBoxButtons.YesNo);
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
                    sHeadR = "�� �� �� : " + dt.Rows[0][0].ToString() + " (" + GlobalVariable.gsUserID + ")";
                else
                    sHeadR = sHeadR + "^�� �� �� : " + dt.Rows[0][0].ToString() + " (" + GlobalVariable.gsUserID + ")";

                if (oMSChart == null)
                {
                    //iSRow���� ���ο��� ����ϰ� ����.
                    if (sHeadL != "")
                        iTmp = sHeadL.Split('^').Length;

                    if (sHeadR != "")
                        jTmp = sHeadR.Split('^').Length;

                    iSRow = (iTmp > jTmp ? iTmp : jTmp) + 5 + 1;

                    //������ ���鼳��
                    subPageSetup(iSRow + iGridHeadCnt - 1, true);
                }
                else
                {
                    //iSRow���� ���ο��� ����ϰ� ����.
                    if (sHeadL != "")
                        iTmp = sHeadL.Split('^').Length;

                    if (sHeadR != "")
                        jTmp = sHeadR.Split('^').Length;

                    // �Ӹ��� Row�� + Ÿ��Ʋ�� �����ϴ� Row��(5)
                    iChartRow = (iTmp > jTmp ? iTmp : jTmp) + 5 + 1;

                    //Chart����
                    iSRow = CopyMSChart(oMSChart, iChartRow);

                    //������ ���鼳��
                    subPageSetup(iSRow + iGridHeadCnt - 1, false);
                }

                //Header�׸��� 2������ ���
                iECol = CopySpView(oSpread, iGridHeadCnt, iSCol, iSRow, ref iMergeColSize); //Excel���� ������ Col�� ��ġ��

                ColSize = iECol;

                iERow = oSpread.ActiveSheet.Rows.Count + iSRow + iGridHeadCnt - 1; //Excel���� ������ Row�� ��ġ��

                //Header�� ���� �ۼ�(Head�κ��� ������, Data���� �ۼ�)
                HeaderLine(iGridHeadCnt, iSCol, iSRow, iECol, iERow, iMergeColSize);
                                
                ////������ �Ӹ����� �ۼ�
                setHeader(iSCol, iECol, sHeadL, sHeadR);
                                
                //������ Ÿ��Ʋ�� �ۼ�
                setTitle(iSCol, iECol, sFileTitle);

                //Data�κ� �� Merge (600�� �Ѿ�� �ӵ� �ۻ���..)
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
                    //���� �Ӹ����� ������ �Ӹ��� �κ��� �� �������� AutoFit�� �Ѵ�.
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
                // ����ڿ��� ���� ���θ� ���´�.
                xlBook.Saved = false;

                System.Runtime.InteropServices.Marshal.ReleaseComObject(xlSheet);
                System.Runtime.InteropServices.Marshal.ReleaseComObject(xlBook);
                System.Runtime.InteropServices.Marshal.ReleaseComObject(xlBooks);

                if (bResult == false)
                {
                    //Make Excel application close.
                    xlApp.DisplayAlerts = false;
                    //xlBook.Saved = true;                    
                    xlBooks.Close();  // ***** �� �Լ��� ȣ�� ���� ������ �۾����μ����� EXCEL.EXE�� ���� �ʰ� ��� ���� ���� *****
                    xlApp.Quit();
                }
                else
                {
                    // ��� ������ ���޽����� ��Ÿ������ �Ѵ�.
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
            string sPrnDate = "��ȸ���� : " + DateTime.Now.ToString("yyyy-MM-dd");
            
            if(IsPrintTitleRows ==true)
                xlSheet.PageSetup.PrintTitleRows = "$1:$" + iPrtTitleRow;

            xlSheet.PageSetup.LeftFooter = "&10�ϳ�����ũ��";
            xlSheet.PageSetup.CenterFooter = "&10������ : &P/&N";
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

            // 2010-09-07-������ : ���� ��� �� �ϴܿ� �������� �����Ϳ� ���� ���� ������.
            xlSheet.PageSetup.BottomMargin = xlApp.InchesToPoints(0.5);

            xlSheet.PageSetup.HeaderMargin = xlApp.InchesToPoints(0.0);
            xlSheet.PageSetup.FooterMargin = xlApp.InchesToPoints(0.0);
            xlSheet.PageSetup.CenterHorizontally=true ;
            xlSheet.PageSetup.Orientation =  Excel.XlPageOrientation.xlLandscape; //������ ���� ����
            xlSheet.PageSetup.PaperSize =  Excel.XlPaperSize.xlPaperA4; //xlPaperA4                        
            xlSheet.PageSetup.Zoom = false ;
            xlSheet.PageSetup.FitToPagesWide = 1;
            xlSheet.PageSetup.FitToPagesTall = false ;            
        }

        private int CopySpView(FpSpread oSpread, int iGridHeadCnt,
                               int iSCol, int iSRow, ref int iMergeColSize)
        {
            int iMrgDelCnt = 0;
            int iFrozenColumn = 0;  //Ʋ���� Į��(Į�� Merge�� 1���� ��������� �۾��Ѵ�. - �ӵ� ������)

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

            //������ �÷�ó�� ��ƾ
            for (int i = oSpread.ActiveSheet.ColumnCount-1; i >= 0 ; i--)
            {
                if (oSpread.ActiveSheet.Columns[i].Visible == false)
                {
                    xlSheet.get_Range(xlSheet.Cells[1, i + iSCol], xlSheet.Cells[oSpread.ActiveSheet.RowCount + iSRow + iGridHeadCnt-1, i + iSCol]).Delete(Excel.XlDirection.xlToLeft);
                    iDelCnt++;

                    //Į�� Merge�� Ʋ������ �� ��ġ���� �ǽ��ؾߵ�.
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
                //�� Merge�� ���� ��ġ�����ľ�
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

                //���� ����Ÿ ����.
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

                //���� ����Ÿ ����
                for (int iC = iSCol; iC <= iMergeColSize; iC++)
                {
                    //�� ��� �ʱⰪ
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
            //    //�� Merge�� ���� ��ġ�����ľ�
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

            //        //���� ����Ÿ ����.
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


            //        //���� ����Ÿ ����
            //        for (int iC = iSCol; iC <= iMergeColSize; iC++)
            //        {
            //            //�� ��� �ʱⰪ
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
            
            //��� �Ʒ��� �� 
            //oRange.Borders[Excel.XlBordersIndex.xlEdgeBottom].LineStyle = Excel.XlLineStyle.xlContinuous;
            //oRange.Borders[Excel.XlBordersIndex.xlEdgeBottom].Weight = Excel.XlBorderWeight.xlMedium;
            //oRange.Borders[Excel.XlBordersIndex.xlEdgeBottom].ColorIndex = Excel.XlColorIndex.xlColorIndexAutomatic;          

        }

        private void LineDraw(int iSCol, int iSRow,int iECol, int iERow, bool isVertical, bool isHorizental)
        {
            Excel.Range oRange = (Excel.Range)xlSheet.get_Range(xlSheet.Cells[iSRow, iSCol], xlSheet.Cells[iERow, iECol]);

            //�ش� Range�� ������ �׸���.
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

                //Column�׸��� ���� ��� ��ü �÷�ũ�⸦ Ű���.
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
                //�� ��� �ʱⰪ
                sValue = xlSheet.get_Range(xlSheet.Cells[iSRow, iC], xlSheet.Cells[iSRow, iC]).Text.ToString();
                iCSRow = iSRow;

                for (int iR = iSRow + 1; iR <= iERow + 1; iR++)
                {
                    if (iR < iERow + 1)
                        sNextValue = xlSheet.get_Range(xlSheet.Cells[iR, iC], xlSheet.Cells[iR, iC]).Text.ToString();
                    else
                        sNextValue = "!@#$%^&*()";  //Dummy

                    //Sub Total Row�� Column�������������ؼ�.
                    if (sValue.IndexOf("Sub Total") > 0 || sValue == "Total")
                    {
                        //xlSheet.get_Range(xlSheet.Cells[iR, iC], xlSheet.Cells[iR + iRepeatRow, iMergeColSize]);
                        Excel.Range oRange = xlSheet.get_Range(xlSheet.Cells[iCSRow, iC], xlSheet.Cells[iCSRow + iRepeatRow, iMergeColSize]);
                        oRange.Value2 = sValue;
                    }

                    if (sValue != sNextValue)
                    {
                        //��ĭ�̻��� ��츸 Merge����(���� ����Ÿ ����)
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

            //Total�̳� Sub Total�κ� �� Merge
            for (int iR = iSRow; iR <= iERow; iR = iR + iRepeatRow+1)
            {
                for (int iC = iMergeColSize - 1; iC >= iSCol; iC--)
                {
                    //�� ��� �ʱⰪ
                    sValue = xlSheet.get_Range(xlSheet.Cells[iR, iC], xlSheet.Cells[iR, iC]).Text.ToString();

                    //- Sub Total�� ��� ����Ÿ�� �������� ������. �̰��� �����ϱ� ���� ��ƾ.
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


                        // SubTotal �� Total �κ� Row�� BackColor 
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

            //Row�� ��
            dRowHeight = Convert.ToDouble(xlSheet.get_Range(xlSheet.Cells[iChartRow, 1], xlSheet.Cells[iChartRow, 1]).Height);
            
            //�׸��� ��           
            iRowCnt = Convert.ToInt16((dPicHeight / dRowHeight)) + 2;            
                    
            System.IO.File.Delete(sImageFileName);

            return iChartRow+iRowCnt;
        }

        private int CopyMSChart(System.Windows.Forms.DataVisualization.Charting.Chart oMSChart, int iChartRow)
        {
            //MS Chart ��� ���� �߰�

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

            //Row�� ��
            dRowHeight = Convert.ToDouble(xlSheet.get_Range(xlSheet.Cells[iChartRow, 1], xlSheet.Cells[iChartRow, 1]).Height);

            //�׸��� ��           
            iRowCnt = Convert.ToInt16((dPicHeight / dRowHeight)) + 2;

            System.IO.File.Delete(sImageFileName);

            return iChartRow + iRowCnt;
        }


        #endregion		
	
	}
}
