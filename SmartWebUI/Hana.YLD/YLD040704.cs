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

namespace Hana.YLD
{
    public partial class YLD040704 : Miracom.SmartWeb.UI.Controls.udcCUSReport002
    {
        /// <summary>
        /// 클  래  스: YLD040704<br/>
        /// 클래스요약: DC TREND(Scatter) <br/>
        /// 작  성  자: 김민우<br/>
        /// 최초작성일: 2010-10-04<br/>
        /// 상세  설명: DC TREND(Scatter)<br/>
        /// 변경  내용: <br/> 
        
        /// </summary>

        string minYld;

        public YLD040704()
        {
            InitializeComponent();
            SortInit();
            GridColumnInit();
            cdvFromToDate.AutoBinding();
            cdvFromToDate.DaySelector.SelectedValue = "DAY";

            this.cdvOper.sFactory = GlobalVariable.gsAssyDefaultFactory;
            this.cdvMat.sFactory = GlobalVariable.gsAssyDefaultFactory;
            //this.SetFactory(GlobalVariable.gsAssyDefaultFactory);
            //cdvFactory.Text = GlobalVariable.gsAssyDefaultFactory;
        }

        #region 초기화 및 유효성 검사
        /// <summary 1. 유효성 검사>
        /// <remarks></remarks>
        /// </summary>
        /// <returns></returns>
        private Boolean CheckField()
        {
            if (cbColor.Checked == true)
            {
                if (cdvOper.Text == "ALL" || cdvOper.Text == "")
                {
                    //Combo Box가 아닌 TextBox에 직접 입력해야 할 수도 있기 때문에 공정을 선택하는 Message이지만 공정을 입력하는 Message로 수정. 
                    CmnFunction.ShowMsgBox(RptMessages.GetMessage("STD064", GlobalVariable.gcLanguage));
                    return false;
                }
            }
            
            if (cdvCustomer.Text.Equals("") || cdvCustomer.Text.Equals("ALL"))
            {
                CmnFunction.ShowMsgBox(RptMessages.GetMessage("STD038", GlobalVariable.gcLanguage));
                return false;
            }

            if (txtYield.Text.Equals("") || CmnFunction.CheckNumeric(txtYield.Text) == false)
            {
                CmnFunction.ShowMsgBox(RptMessages.GetMessage("STD078", GlobalVariable.gcLanguage));
                return false;
            }

            if (cdvFromToDate.DaySelector.SelectedValue.ToString() == "MONTH")
            {
                if (txtSearchProduct.Text.Length < 3 && txtLotID.Text.Length < 3)
                {

                    CmnFunction.ShowMsgBox(RptMessages.GetMessage("STD077", GlobalVariable.gcLanguage));
                    return false;

                }
            }
            else if (cdvFromToDate.DaySelector.SelectedValue.ToString() == "DAY")
            {

            }
            else
            {
                if (txtSearchProduct.Text.Length < 3 && txtLotID.Text.Length < 3)
                {
                    CmnFunction.ShowMsgBox(RptMessages.GetMessage("STD077", GlobalVariable.gcLanguage));
                    return false;
                }


            }
            
            return true;
        }

        /// <summary>
        /// 2. 헤더 생성
        /// </summary>
        private void GridColumnInit()
        {
            spdData.RPT_ColumnInit();

            try
            {
                spdData.RPT_AddBasicColumn("Customer", 0, 0, Visibles.False, Frozen.True, Align.Center, Merge.False, Formatter.String, 80);
                spdData.RPT_AddBasicColumn("Family", 0, 1, Visibles.False, Frozen.True, Align.Center, Merge.False, Formatter.String, 80);
                spdData.RPT_AddBasicColumn("LD Count", 0, 2, Visibles.False, Frozen.True, Align.Center, Merge.False, Formatter.String, 80);
                spdData.RPT_AddBasicColumn("PKG", 0, 3, Visibles.False, Frozen.True, Align.Center, Merge.False, Formatter.String, 80);
                spdData.RPT_AddBasicColumn("Type1", 0, 4, Visibles.False, Frozen.True, Align.Center, Merge.False, Formatter.String, 80);
                spdData.RPT_AddBasicColumn("Type2", 0, 5, Visibles.False, Frozen.True, Align.Center, Merge.False, Formatter.String, 80);
                spdData.RPT_AddBasicColumn("Density", 0, 6, Visibles.False, Frozen.True, Align.Center, Merge.False, Formatter.String, 80);
                spdData.RPT_AddBasicColumn("Generation", 0, 7, Visibles.False, Frozen.True, Align.Center, Merge.False, Formatter.String, 80);
                spdData.RPT_AddBasicColumn("Product", 0, 8, Visibles.False, Frozen.True, Align.Left, Merge.False, Formatter.String, 80);
                spdData.RPT_AddBasicColumn("RECEIVE_DATE", 0, 9, Visibles.True, Frozen.False, Align.Left, Merge.False, Formatter.String, 80);
                spdData.RPT_AddBasicColumn("LotID", 0, 10, Visibles.True, Frozen.False, Align.Left, Merge.False, Formatter.String, 80);

                spdData.RPT_AddBasicColumn("LotID(HANA)", 0, 11, Visibles.True, Frozen.False, Align.Left, Merge.False, Formatter.String, 80);

                spdData.RPT_AddBasicColumn("B/G Start Time", 0, 12, Visibles.True, Frozen.False, Align.Left, Merge.False, Formatter.String, 80);
                spdData.RPT_AddBasicColumn("B/G End Time", 0, 13, Visibles.True, Frozen.False, Align.Left, Merge.False, Formatter.String, 80);
                spdData.RPT_AddBasicColumn("B/G Machine", 0, 14, Visibles.True, Frozen.False, Align.Left, Merge.False, Formatter.String, 80);
                spdData.RPT_AddBasicColumn("B/G Start Q'ty", 0, 15, Visibles.True, Frozen.False, Align.Left, Merge.False, Formatter.Number, 80);
                spdData.RPT_AddBasicColumn("B/G End Q'ty", 0, 16, Visibles.True, Frozen.False, Align.Left, Merge.False, Formatter.Number, 80);
                spdData.RPT_AddBasicColumn("B/G Loss Q'ty", 0, 17, Visibles.True, Frozen.False, Align.Left, Merge.False, Formatter.Number, 80);

                spdData.RPT_AddBasicColumn("Saw Start Time", 0, 18, Visibles.True, Frozen.False, Align.Left, Merge.False, Formatter.String, 80);
                spdData.RPT_AddBasicColumn("Saw End Time", 0, 19, Visibles.True, Frozen.False, Align.Left, Merge.False, Formatter.String, 80);
                spdData.RPT_AddBasicColumn("Saw Machine", 0, 20, Visibles.True, Frozen.False, Align.Left, Merge.False, Formatter.String, 80);
                spdData.RPT_AddBasicColumn("Saw Start Q'ty", 0, 21, Visibles.True, Frozen.False, Align.Left, Merge.False, Formatter.Number, 80);
                spdData.RPT_AddBasicColumn("Saw End Q'ty", 0, 22, Visibles.True, Frozen.False, Align.Left, Merge.False, Formatter.Number, 80);
                spdData.RPT_AddBasicColumn("Saw Loss Q'ty", 0, 23, Visibles.True, Frozen.False, Align.Left, Merge.False, Formatter.Number, 80);

                spdData.RPT_AddBasicColumn("D/A1 Start Time", 0, 24, Visibles.True, Frozen.False, Align.Left, Merge.False, Formatter.String, 80);
                spdData.RPT_AddBasicColumn("D/A1 End Time", 0, 25, Visibles.True, Frozen.False, Align.Left, Merge.False, Formatter.String, 80);
                spdData.RPT_AddBasicColumn("D/A1 Machine", 0, 26, Visibles.True, Frozen.False, Align.Left, Merge.False, Formatter.String, 80);
                spdData.RPT_AddBasicColumn("D/A1 Start Q'ty", 0, 27, Visibles.True, Frozen.False, Align.Left, Merge.False, Formatter.Number, 80);
                spdData.RPT_AddBasicColumn("D/A1 End Q'ty", 0, 28, Visibles.True, Frozen.False, Align.Left, Merge.False, Formatter.Number, 80);
                spdData.RPT_AddBasicColumn("D/A1 Loss Q'ty", 0, 29, Visibles.True, Frozen.False, Align.Left, Merge.False, Formatter.Number, 80);


                spdData.RPT_AddBasicColumn("D/A2 Start Time", 0, 30, Visibles.True, Frozen.False, Align.Left, Merge.False, Formatter.String, 80);
                spdData.RPT_AddBasicColumn("D/A2 End Time", 0, 31, Visibles.True, Frozen.False, Align.Left, Merge.False, Formatter.String, 80);
                spdData.RPT_AddBasicColumn("D/A2 Machine", 0, 32, Visibles.True, Frozen.False, Align.Left, Merge.False, Formatter.String, 80);
                spdData.RPT_AddBasicColumn("D/A2 Start Q'ty", 0, 33, Visibles.True, Frozen.False, Align.Left, Merge.False, Formatter.Number, 80);
                spdData.RPT_AddBasicColumn("D/A2 End Q'ty", 0, 34, Visibles.True, Frozen.False, Align.Left, Merge.False, Formatter.Number, 80);
                spdData.RPT_AddBasicColumn("D/A2 Loss Q'ty", 0, 35, Visibles.True, Frozen.False, Align.Left, Merge.False, Formatter.Number, 80);

                spdData.RPT_AddBasicColumn("D/A3 Start Time", 0, 36, Visibles.True, Frozen.False, Align.Left, Merge.False, Formatter.String, 80);
                spdData.RPT_AddBasicColumn("D/A3 End Time", 0, 37, Visibles.True, Frozen.False, Align.Left, Merge.False, Formatter.String, 80);
                spdData.RPT_AddBasicColumn("D/A3 Machine", 0, 38, Visibles.True, Frozen.False, Align.Left, Merge.False, Formatter.String, 80);
                spdData.RPT_AddBasicColumn("D/A3 Start Q'ty", 0, 39, Visibles.True, Frozen.False, Align.Left, Merge.False, Formatter.Number, 80);
                spdData.RPT_AddBasicColumn("D/A3 End Q'ty", 0, 40, Visibles.True, Frozen.False, Align.Left, Merge.False, Formatter.Number, 80);
                spdData.RPT_AddBasicColumn("D/A3 Loss Q'ty", 0, 41, Visibles.True, Frozen.False, Align.Left, Merge.False, Formatter.Number, 80);

                spdData.RPT_AddBasicColumn("W/B1 Start Time", 0, 42, Visibles.True, Frozen.False, Align.Left, Merge.False, Formatter.String, 80);
                spdData.RPT_AddBasicColumn("W/B1 End Time", 0, 43, Visibles.True, Frozen.False, Align.Left, Merge.False, Formatter.String, 80);
                spdData.RPT_AddBasicColumn("W/B1 Machine", 0, 44, Visibles.True, Frozen.False, Align.Left, Merge.False, Formatter.String, 80);
                spdData.RPT_AddBasicColumn("W/B1 Start Q'ty", 0, 45, Visibles.True, Frozen.False, Align.Left, Merge.False, Formatter.Number, 80);
                spdData.RPT_AddBasicColumn("W/B1 End Q'ty", 0, 46, Visibles.True, Frozen.False, Align.Left, Merge.False, Formatter.Number, 80);
                spdData.RPT_AddBasicColumn("W/B1 Loss Q'ty", 0, 47, Visibles.True, Frozen.False, Align.Left, Merge.False, Formatter.Number, 80);

                spdData.RPT_AddBasicColumn("W/B2 Start Time", 0, 48, Visibles.True, Frozen.False, Align.Left, Merge.False, Formatter.String, 80);
                spdData.RPT_AddBasicColumn("W/B2 End Time", 0, 49, Visibles.True, Frozen.False, Align.Left, Merge.False, Formatter.String, 80);
                spdData.RPT_AddBasicColumn("W/B2 Machine", 0, 50, Visibles.True, Frozen.False, Align.Left, Merge.False, Formatter.String, 80);
                spdData.RPT_AddBasicColumn("W/B2 Start Q'ty", 0, 51, Visibles.True, Frozen.False, Align.Left, Merge.False, Formatter.Number, 80);
                spdData.RPT_AddBasicColumn("W/B2 End Q'ty", 0, 52, Visibles.True, Frozen.False, Align.Left, Merge.False, Formatter.Number, 80);
                spdData.RPT_AddBasicColumn("W/B2 Loss Q'ty", 0, 53, Visibles.True, Frozen.False, Align.Left, Merge.False, Formatter.Number, 80);

                spdData.RPT_AddBasicColumn("W/B3 Start Time", 0, 54, Visibles.True, Frozen.False, Align.Left, Merge.False, Formatter.String, 80);
                spdData.RPT_AddBasicColumn("W/B3 End Time", 0, 55, Visibles.True, Frozen.False, Align.Left, Merge.False, Formatter.String, 80);
                spdData.RPT_AddBasicColumn("W/B3 Machine", 0, 56, Visibles.True, Frozen.False, Align.Left, Merge.False, Formatter.String, 80);
                spdData.RPT_AddBasicColumn("W/B3 Start Q'ty", 0, 57, Visibles.True, Frozen.False, Align.Left, Merge.False, Formatter.Number, 80);
                spdData.RPT_AddBasicColumn("W/B3 End Q'ty", 0, 58, Visibles.True, Frozen.False, Align.Left, Merge.False, Formatter.Number, 80);
                spdData.RPT_AddBasicColumn("W/B3 Loss Q'ty", 0, 59, Visibles.True, Frozen.False, Align.Left, Merge.False, Formatter.Number, 80);

                spdData.RPT_AddBasicColumn("MOLD Start Time", 0, 60, Visibles.True, Frozen.False, Align.Left, Merge.False, Formatter.String, 80);
                spdData.RPT_AddBasicColumn("MOLD End Time", 0, 61, Visibles.True, Frozen.False, Align.Left, Merge.False, Formatter.String, 80);
                spdData.RPT_AddBasicColumn("MOLD Machine", 0, 62, Visibles.True, Frozen.False, Align.Left, Merge.False, Formatter.String, 80);
                spdData.RPT_AddBasicColumn("MOLD Start Q'ty", 0, 63, Visibles.True, Frozen.False, Align.Left, Merge.False, Formatter.Number, 80);
                spdData.RPT_AddBasicColumn("MOLD End Q'ty", 0, 64, Visibles.True, Frozen.False, Align.Left, Merge.False, Formatter.Number, 80);
                spdData.RPT_AddBasicColumn("MOLD Loss Q'ty", 0, 65, Visibles.True, Frozen.False, Align.Left, Merge.False, Formatter.Number, 80);

                spdData.RPT_AddBasicColumn("MARKING Start Time", 0, 66, Visibles.True, Frozen.False, Align.Left, Merge.False, Formatter.String, 80);
                spdData.RPT_AddBasicColumn("MARKING End Time", 0, 67, Visibles.True, Frozen.False, Align.Left, Merge.False, Formatter.String, 80);
                spdData.RPT_AddBasicColumn("MARKING Machine", 0, 68, Visibles.True, Frozen.False, Align.Left, Merge.False, Formatter.String, 80);
                spdData.RPT_AddBasicColumn("MARKING Start Q'ty", 0, 69, Visibles.True, Frozen.False, Align.Left, Merge.False, Formatter.Number, 80);
                spdData.RPT_AddBasicColumn("MARKING End Q'ty", 0, 70, Visibles.True, Frozen.False, Align.Left, Merge.False, Formatter.Number, 80);
                spdData.RPT_AddBasicColumn("MARKING Loss Q'ty", 0, 71, Visibles.True, Frozen.False, Align.Left, Merge.False, Formatter.Number, 80);

                spdData.RPT_AddBasicColumn("BALL MOUNT Start Time", 0, 72, Visibles.True, Frozen.False, Align.Left, Merge.False, Formatter.String, 80);
                spdData.RPT_AddBasicColumn("BALL MOUNT End Time", 0, 73, Visibles.True, Frozen.False, Align.Left, Merge.False, Formatter.String, 80);
                spdData.RPT_AddBasicColumn("BALL MOUNT Machine", 0, 74, Visibles.True, Frozen.False, Align.Left, Merge.False, Formatter.String, 80);
                spdData.RPT_AddBasicColumn("BALL MOUNT Start Q'ty", 0, 75, Visibles.True, Frozen.False, Align.Left, Merge.False, Formatter.Number, 80);
                spdData.RPT_AddBasicColumn("BALL MOUNT End Q'ty", 0, 76, Visibles.True, Frozen.False, Align.Left, Merge.False, Formatter.Number, 80);
                spdData.RPT_AddBasicColumn("BALL MOUNT Loss Q'ty", 0, 77, Visibles.True, Frozen.False, Align.Left, Merge.False, Formatter.Number, 80);

                spdData.RPT_AddBasicColumn("PKG SAWING Start Time", 0, 78, Visibles.True, Frozen.False, Align.Left, Merge.False, Formatter.String, 80);
                spdData.RPT_AddBasicColumn("PKG SAWING End Time", 0, 79, Visibles.True, Frozen.False, Align.Left, Merge.False, Formatter.String, 80);
                spdData.RPT_AddBasicColumn("PKG SAWING Machine", 0, 80, Visibles.True, Frozen.False, Align.Left, Merge.False, Formatter.String, 80);
                spdData.RPT_AddBasicColumn("PKG SAWING Start Q'ty", 0, 81, Visibles.True, Frozen.False, Align.Left, Merge.False, Formatter.Number, 80);
                spdData.RPT_AddBasicColumn("PKG SAWING End Q'ty", 0, 82, Visibles.True, Frozen.False, Align.Left, Merge.False, Formatter.Number, 80);
                spdData.RPT_AddBasicColumn("PKG SAWING Loss Q'ty", 0, 83, Visibles.True, Frozen.False, Align.Left, Merge.False, Formatter.Number, 80);

                spdData.RPT_AddBasicColumn("TRIM Start Time", 0, 84, Visibles.True, Frozen.False, Align.Left, Merge.False, Formatter.String, 80);
                spdData.RPT_AddBasicColumn("TRIM End Time", 0, 85, Visibles.True, Frozen.False, Align.Left, Merge.False, Formatter.String, 80);
                spdData.RPT_AddBasicColumn("TRIM Machine", 0, 86, Visibles.True, Frozen.False, Align.Left, Merge.False, Formatter.String, 80);
                spdData.RPT_AddBasicColumn("TRIM Start Q'ty", 0, 87, Visibles.True, Frozen.False, Align.Left, Merge.False, Formatter.Number, 80);
                spdData.RPT_AddBasicColumn("TRIM End Q'ty", 0, 88, Visibles.True, Frozen.False, Align.Left, Merge.False, Formatter.Number, 80);
                spdData.RPT_AddBasicColumn("TRIM Loss Q'ty", 0, 89, Visibles.True, Frozen.False, Align.Left, Merge.False, Formatter.Number, 80);

                spdData.RPT_AddBasicColumn("FORM Start Time", 0, 90, Visibles.True, Frozen.False, Align.Left, Merge.False, Formatter.String, 80);
                spdData.RPT_AddBasicColumn("FORM End Time", 0, 91, Visibles.True, Frozen.False, Align.Left, Merge.False, Formatter.String, 80);
                spdData.RPT_AddBasicColumn("FORM Machine", 0, 92, Visibles.True, Frozen.False, Align.Left, Merge.False, Formatter.String, 80);
                spdData.RPT_AddBasicColumn("FORM Start Q'ty", 0, 93, Visibles.True, Frozen.False, Align.Left, Merge.False, Formatter.Number, 80);
                spdData.RPT_AddBasicColumn("FORM End Q'ty", 0, 94, Visibles.True, Frozen.False, Align.Left, Merge.False, Formatter.Number, 80);
                spdData.RPT_AddBasicColumn("FORM Loss Q'ty", 0, 95, Visibles.True, Frozen.False, Align.Left, Merge.False, Formatter.Number, 80);

                spdData.RPT_AddBasicColumn("AUTO VISUAL Start Time", 0, 96, Visibles.True, Frozen.False, Align.Left, Merge.False, Formatter.String, 80);
                spdData.RPT_AddBasicColumn("AUTO VISUAL End Time", 0, 97, Visibles.True, Frozen.False, Align.Left, Merge.False, Formatter.String, 80);
                spdData.RPT_AddBasicColumn("AUTO VISUAL Machine", 0, 98, Visibles.True, Frozen.False, Align.Left, Merge.False, Formatter.String, 80);
                spdData.RPT_AddBasicColumn("AUTO VISUAL Start Q'ty", 0, 99, Visibles.True, Frozen.False, Align.Left, Merge.False, Formatter.Number, 80);
                spdData.RPT_AddBasicColumn("AUTO VISUAL End Q'ty", 0, 100, Visibles.True, Frozen.False, Align.Left, Merge.False, Formatter.Number, 80);
                spdData.RPT_AddBasicColumn("AUTO VISUAL Loss Q'ty", 0, 101, Visibles.True, Frozen.False, Align.Left, Merge.False, Formatter.Number, 80);

                spdData.RPT_AddBasicColumn("IN", 0, 102, Visibles.True, Frozen.False, Align.Left, Merge.False, Formatter.Number, 80);
                spdData.RPT_AddBasicColumn("OUT", 0, 103, Visibles.True, Frozen.False, Align.Left, Merge.False, Formatter.Number, 80);
                spdData.RPT_AddBasicColumn("YLD", 0, 104, Visibles.True, Frozen.False, Align.Left, Merge.False, Formatter.Double2, 80);
                spdData.RPT_AddBasicColumn("LOSS", 0, 105, Visibles.True, Frozen.False, Align.Left, Merge.False, Formatter.Number, 80);

                spdData.RPT_AddBasicColumn("DC_IN_QTY", 0, 106, Visibles.True, Frozen.False, Align.Left, Merge.False, Formatter.Number, 80);
                spdData.RPT_AddBasicColumn("DC_OUT_QTY", 0, 107, Visibles.True, Frozen.False, Align.Left, Merge.False, Formatter.Number, 80);
                
                spdData.RPT_AddBasicColumn("DC_BIN_QTY_6", 0, 108, Visibles.True, Frozen.False, Align.Left, Merge.False, Formatter.Number, 80);
                spdData.RPT_AddBasicColumn("DC_BIN_QTY_7", 0, 109, Visibles.True, Frozen.False, Align.Left, Merge.False, Formatter.Number, 80);
                spdData.RPT_AddBasicColumn("DC_BIN_QTY_8", 0, 110, Visibles.True, Frozen.False, Align.Left, Merge.False, Formatter.Number, 80);
                spdData.RPT_AddBasicColumn("DC_YLD", 0, 111, Visibles.True, Frozen.False, Align.Left, Merge.False, Formatter.Double2, 80);
                spdData.RPT_AddBasicColumn("DC_YLD", 0, 112, Visibles.False, Frozen.False, Align.Left, Merge.False, Formatter.String, 80);

                spdData.RPT_AddBasicColumn("DCFAIL_RATE", 0, 113, Visibles.True, Frozen.False, Align.Left, Merge.False, Formatter.Double2, 80);
                spdData.RPT_AddBasicColumn("IDD_RATE", 0, 114, Visibles.True, Frozen.False, Align.Left, Merge.False, Formatter.Double2, 80);
                spdData.RPT_AddBasicColumn("LEAK_RATE", 0, 115, Visibles.True, Frozen.False, Align.Left, Merge.False, Formatter.Double2, 80);
                spdData.RPT_AddBasicColumn("OS_RATE", 0, 116, Visibles.True, Frozen.False, Align.Left, Merge.False, Formatter.Double2, 80);

                spdData.RPT_AddBasicColumn("PCB Vendor Name", 0, 117, Visibles.True, Frozen.False, Align.Left, Merge.False, Formatter.String, 80);
                spdData.RPT_AddBasicColumn("PCB Vendor LOT", 0, 118, Visibles.True, Frozen.False, Align.Left, Merge.False, Formatter.String, 80);
                spdData.RPT_AddBasicColumn("PCB CNT", 0, 119, Visibles.True, Frozen.False, Align.Left, Merge.False, Formatter.Number, 80);

                spdData.RPT_AddBasicColumn("LF Vendor Name", 0, 120, Visibles.True, Frozen.False, Align.Left, Merge.False, Formatter.String, 80);
                spdData.RPT_AddBasicColumn("LF Vendor LOT", 0, 121, Visibles.True, Frozen.False, Align.Left, Merge.False, Formatter.String, 80);
                spdData.RPT_AddBasicColumn("LF CNT", 0, 122, Visibles.True, Frozen.False, Align.Left, Merge.False, Formatter.Number, 80);

                spdData.RPT_AddBasicColumn("Epoxy Vendor Name", 0, 123, Visibles.True, Frozen.False, Align.Left, Merge.False, Formatter.String, 80);
                spdData.RPT_AddBasicColumn("Epoxy Vendor LOT", 0, 124, Visibles.True, Frozen.False, Align.Left, Merge.False, Formatter.String, 80);
                spdData.RPT_AddBasicColumn("Epoxy CNT", 0, 125, Visibles.True, Frozen.False, Align.Left, Merge.False, Formatter.Number, 80);

                spdData.RPT_AddBasicColumn("GW Vendor Name", 0, 126, Visibles.True, Frozen.False, Align.Left, Merge.False, Formatter.String, 80);
                spdData.RPT_AddBasicColumn("GW Vendor LOT", 0, 127, Visibles.True, Frozen.False, Align.Left, Merge.False, Formatter.String, 80);
                spdData.RPT_AddBasicColumn("GW CNT", 0, 128, Visibles.True, Frozen.False, Align.Left, Merge.False, Formatter.Number, 80);

                spdData.RPT_AddBasicColumn("EMC Vendor Name", 0, 129, Visibles.True, Frozen.False, Align.Left, Merge.False, Formatter.String, 80);
                spdData.RPT_AddBasicColumn("EMC Vendor LOT", 0, 130, Visibles.True, Frozen.False, Align.Left, Merge.False, Formatter.String, 80);
                spdData.RPT_AddBasicColumn("EMC CNT", 0, 131, Visibles.True, Frozen.False, Align.Left, Merge.False, Formatter.Number, 80);

                spdData.RPT_AddBasicColumn("SB Vendor Name", 0, 132, Visibles.True, Frozen.False, Align.Left, Merge.False, Formatter.String, 80);
                spdData.RPT_AddBasicColumn("SB Vendor LOT", 0, 133, Visibles.True, Frozen.False, Align.Left, Merge.False, Formatter.String, 80);
                spdData.RPT_AddBasicColumn("SB CNT", 0, 134, Visibles.True, Frozen.False, Align.Left, Merge.False, Formatter.Number, 80);
                
                spdData.RPT_ColumnConfigFromTable(btnSort); //Group항목이 있을경우 반드시 선업해줄것
            }
            catch (Exception ex)
            {
                CmnFunction.ShowMsgBox(ex.Message);
            }
        }

        /// <summary>
        /// 3. Group By 정의
        /// </summary>
        private void SortInit()
        {
            ((udcTableForm)(this.btnSort.BindingForm)).AddRow("Customer", "CUSTOMER", "CUSTOMER", true);
            ((udcTableForm)(this.btnSort.BindingForm)).AddRow("Family", "FAMILY", "FAMILY", false);
            ((udcTableForm)(this.btnSort.BindingForm)).AddRow("LD Count", "LD_COUNT", "LD_COUNT", true);
            ((udcTableForm)(this.btnSort.BindingForm)).AddRow("Package", "PKG", "PKG", true);
            ((udcTableForm)(this.btnSort.BindingForm)).AddRow("Type1", "TYPE1", "TYPE1", false);
            ((udcTableForm)(this.btnSort.BindingForm)).AddRow("Type2", "TYPE2", "TYPE2", false);
            ((udcTableForm)(this.btnSort.BindingForm)).AddRow("Density", "DENSITY", "DENSITY", true);
            ((udcTableForm)(this.btnSort.BindingForm)).AddRow("Generation", "GENERATION", "GENERATION", true);
            ((udcTableForm)(this.btnSort.BindingForm)).AddRow("Product", "MAT_ID", "MAT_ID", true);

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

            udcTableForm tableForm = (udcTableForm)btnSort.BindingForm;

            QueryCond1 = tableForm.SelectedValueToQueryContainNull;
            QueryCond2 = tableForm.SelectedValue2ToQueryContainNull;
            
            string[] strDate = cdvFromToDate.getSelectDate();
            int LastDate = strDate.Length;

            strSqlString.Append("        SELECT " + QueryCond1 + " ,WORK_DATE, LOT_ID, DC_CMF_1 " + "\n");
            strSqlString.Append("        , BG_START_TIME, BG_END_TIME, BG_RES_ID, BG_IN_QTY, BG_OUT_QTY, BG_LOSS_QTY " + "\n");
            strSqlString.Append("        , SAW_START_TIME, SAW_END_TIME, SAW_RES_ID, SAW_IN_QTY, SAW_OUT_QTY, SAW_LOSS_QTY " + "\n");
            strSqlString.Append("        , DA1_START_TIME, DA1_END_TIME, DA1_RES_ID, DA1_IN_QTY, DA1_OUT_QTY, DA1_LOSS_QTY " + "\n");
            strSqlString.Append("        , DA2_START_TIME, DA2_END_TIME, DA2_RES_ID, DA2_IN_QTY, DA2_OUT_QTY, DA2_LOSS_QTY " + "\n");
            strSqlString.Append("        , DA3_START_TIME, DA3_END_TIME, DA3_RES_ID, DA3_IN_QTY, DA3_OUT_QTY, DA3_LOSS_QTY " + "\n");
            strSqlString.Append("        , WB1_START_TIME, WB1_END_TIME, WB1_RES_ID, WB1_IN_QTY, WB1_OUT_QTY, WB1_LOSS_QTY " + "\n");
            strSqlString.Append("        , WB2_START_TIME, WB2_END_TIME, WB2_RES_ID, WB2_IN_QTY, WB2_OUT_QTY, WB2_LOSS_QTY " + "\n");
            strSqlString.Append("        , WB3_START_TIME, WB3_END_TIME, WB3_RES_ID, WB3_IN_QTY, WB3_OUT_QTY, WB3_LOSS_QTY " + "\n");
            strSqlString.Append("        , MD_START_TIME, MD_END_TIME, MD_RES_ID, MD_IN_QTY, MD_OUT_QTY, MD_LOSS_QTY " + "\n");
            strSqlString.Append("        , MK_START_TIME, MK_END_TIME, MK_RES_ID, MK_IN_QTY, MK_OUT_QTY, MK_LOSS_QTY " + "\n");
            strSqlString.Append("        , BM_START_TIME, BM_END_TIME, BM_RES_ID, BM_IN_QTY, BM_OUT_QTY, BM_LOSS_QTY " + "\n");
            strSqlString.Append("        , PS_START_TIME, PS_END_TIME, PS_RES_ID, PS_IN_QTY, PS_OUT_QTY, PS_LOSS_QTY " + "\n");
            strSqlString.Append("        , TR_START_TIME, TR_END_TIME, TR_RES_ID, TR_IN_QTY, TR_OUT_QTY, TR_LOSS_QTY " + "\n");
            strSqlString.Append("        , FORM_START_TIME, FORM_END_TIME, FORM_RES_ID, FORM_IN_QTY, FORM_OUT_QTY, FORM_LOSS_QTY " + "\n");
            strSqlString.Append("        , AV_START_TIME, AV_END_TIME, AV_RES_ID, AV_IN_QTY, AV_OUT_QTY, AV_LOSS_QTY " + "\n");
            strSqlString.Append("        , TOTAL_IN_QTY, TOTAL_OUT_QTY, TOTAL_YLD, TOTAL_LOSS " + "\n");
            strSqlString.Append("        , DC_IN_QTY, DC_OUT_QTY, DC_BIN_QTY_6, DC_BIN_QTY_7, DC_BIN_QTY_8, DC_YLD, WORK_DATE||' - '||LOT_ID " + "\n");
            strSqlString.Append("        , DC_CMF_QTY_1,DC_CMF_QTY_2,DC_CMF_QTY_3,DC_CMF_QTY_4 " + "\n");

            strSqlString.Append("        , PCB_VEN_NM,PCB_VEN_LOT,PCB_CNT " + "\n");
            strSqlString.Append("        , LF_VEN_NM, LF_VEN_LOT, LF_CNT " + "\n");
            strSqlString.Append("        , EPX_VEN_NM, EPX_VEN_LOT, EPX_CNT " + "\n");
            strSqlString.Append("        , GW_VEN_NM, GW_VEN_LOT, GW_CNT " + "\n");
            strSqlString.Append("        , EMC_VEN_NM, EMC_VEN_LOT, EMC_CNT " + "\n");
            strSqlString.Append("        , SB_VEN_NM, SB_VEN_LOT, SB_CNT " + "\n");

            strSqlString.Append("        FROM RSUMDCLHIS A, MWIPCALDEF B" + "\n");
            strSqlString.Append("       WHERE 1=1 " + "\n");

            #region 상세 조회에 따른 SQL문 생성
            if (udcWIPCondition1.Text != "ALL" && udcWIPCondition1.Text != "")
                //strSqlString.AppendFormat("   AND CUSTOMER {0} " + "\n", udcWIPCondition1.SelectedValueToQueryString);

            if (udcWIPCondition2.Text != "ALL" && udcWIPCondition2.Text != "")
                strSqlString.AppendFormat("   AND FAMILY {0} " + "\n", udcWIPCondition2.SelectedValueToQueryString);

            if (udcWIPCondition3.Text != "ALL" && udcWIPCondition3.Text != "")
                strSqlString.AppendFormat("   AND PKG {0} " + "\n", udcWIPCondition3.SelectedValueToQueryString);

            if (udcWIPCondition4.Text != "ALL" && udcWIPCondition4.Text != "")
                strSqlString.AppendFormat("   AND TYPE1 {0} " + "\n", udcWIPCondition4.SelectedValueToQueryString);

            if (udcWIPCondition5.Text != "ALL" && udcWIPCondition5.Text != "")
                strSqlString.AppendFormat("   AND TYPE2 {0} " + "\n", udcWIPCondition5.SelectedValueToQueryString);

            if (udcWIPCondition6.Text != "ALL" && udcWIPCondition6.Text != "")
                strSqlString.AppendFormat("   AND LD_COUNT {0} " + "\n", udcWIPCondition6.SelectedValueToQueryString);

            if (udcWIPCondition7.Text != "ALL" && udcWIPCondition7.Text != "")
                strSqlString.AppendFormat("   AND DENSITY {0} " + "\n", udcWIPCondition7.SelectedValueToQueryString);

            if (udcWIPCondition8.Text != "ALL" && udcWIPCondition8.Text != "")
                strSqlString.AppendFormat("   AND GENERATION {0} " + "\n", udcWIPCondition8.SelectedValueToQueryString);

            if (udcRASCondition6.Text != "ALL" && udcRASCondition6.Text != "")
            {
                strSqlString.AppendFormat("   AND (BG_RES_ID {0} " + "\n", udcRASCondition6.SelectedValueToQueryString);
                strSqlString.AppendFormat("    OR SAW_RES_ID {0} " + "\n", udcRASCondition6.SelectedValueToQueryString);
                strSqlString.AppendFormat("    OR DA1_RES_ID {0} " + "\n", udcRASCondition6.SelectedValueToQueryString);
                strSqlString.AppendFormat("    OR DA2_RES_ID {0} " + "\n", udcRASCondition6.SelectedValueToQueryString);
                strSqlString.AppendFormat("    OR DA3_RES_ID {0} " + "\n", udcRASCondition6.SelectedValueToQueryString);
                strSqlString.AppendFormat("    OR WB1_RES_ID {0} " + "\n", udcRASCondition6.SelectedValueToQueryString);
                strSqlString.AppendFormat("    OR WB2_RES_ID {0} " + "\n", udcRASCondition6.SelectedValueToQueryString);
                strSqlString.AppendFormat("    OR WB3_RES_ID {0} " + "\n", udcRASCondition6.SelectedValueToQueryString);
                strSqlString.AppendFormat("    OR MD_RES_ID {0} )" + "\n", udcRASCondition6.SelectedValueToQueryString);
                strSqlString.AppendFormat("    OR MK_RES_ID {0} " + "\n", udcRASCondition6.SelectedValueToQueryString);
                strSqlString.AppendFormat("    OR BM_RES_ID {0} " + "\n", udcRASCondition6.SelectedValueToQueryString);
                strSqlString.AppendFormat("    OR PS_RES_ID {0} " + "\n", udcRASCondition6.SelectedValueToQueryString);
                strSqlString.AppendFormat("    OR TR_RES_ID {0} " + "\n", udcRASCondition6.SelectedValueToQueryString);
                strSqlString.AppendFormat("    OR FORM_RES_ID {0} " + "\n", udcRASCondition6.SelectedValueToQueryString);
                strSqlString.AppendFormat("    OR AV_RES_ID {0} " + "\n", udcRASCondition6.SelectedValueToQueryString);
            }

            #endregion

            if (cdvCustomer.Text != "" && cdvCustomer.Text != "ALL")
            {
                strSqlString.Append("         AND CUSTOMER = '" + cdvCustomer.Text + "' " + "\n");
            }
            
            strSqlString.Append("         AND LOT_ID LIKE '" + txtLotID.Text + "' " + "\n");
            strSqlString.Append("         AND MAT_ID LIKE '" + txtSearchProduct.Text + "' " + "\n");
            strSqlString.Append("         AND A.WORK_DATE = B.SYS_DATE " + "\n");
            strSqlString.Append("         AND B.CALENDAR_ID = 'HM' " + "\n");

            if (cdvFromToDate.DaySelector.SelectedValue.ToString() == "MONTH")
            {
                strSqlString.Append("           AND WORK_MONTH BETWEEN '" + cdvFromToDate.FromYearMonth.Value.ToString("yyyyMM") + "' AND '" + cdvFromToDate.ToYearMonth.Value.ToString("yyyyMM") + "'" + "\n");
            }
            else if (cdvFromToDate.DaySelector.SelectedValue.ToString() == "DAY")
            {
                strSqlString.Append("         AND WORK_DATE >='" + cdvFromToDate.FromDate.Text.Replace("-", "") + "' " + "\n");
                strSqlString.Append("         AND WORK_DATE <='" + cdvFromToDate.ToDate.Text.Replace("-", "") + "' " + "\n");
            }
            else
            {
                strSqlString.Append("           AND B.SYS_YEAR||B.PLAN_WEEK BETWEEN '" + cdvFromToDate.HmFromWeek + "' AND '" + cdvFromToDate.HmToWeek + "'" + "\n");
            }

            strSqlString.Append("   AND DC_YLD <= " + txtYield.Text + "\n");
            
            if (cdvOper.Text != "ALL" && cdvOper.Text != "")
            {
                if (cdvOper.Text.Contains("A0040"))
                {
                    strSqlString.Append("         AND BG_START_TIME <> ' ' " + "\n");
                    if (ckRes.Checked == true)
                    {
                        strSqlString.Append("        ORDER BY BG_RES_ID, BG_START_TIME" + "\n");
                    }
                    else
                    {
                        strSqlString.Append("        ORDER BY BG_START_TIME" + "\n");
                    }
                  

                }
                if (cdvOper.Text.Contains("A0200"))
                {
                    strSqlString.Append("         AND SAW_START_TIME <> ' ' " + "\n");
                    if (ckRes.Checked == true)
                    {
                        strSqlString.Append("        ORDER BY SAW_RES_ID, SAW_START_TIME" + "\n");
                    }
                    else
                    {
                        strSqlString.Append("        ORDER BY SAW_START_TIME" + "\n");
                    }
                }
                if (cdvOper.Text.Contains("A0400"))
                {
                    strSqlString.Append("         AND DA1_START_TIME <> ' ' " + "\n");
                    if (ckRes.Checked == true)
                    {
                        strSqlString.Append("        ORDER BY DA1_RES_ID, DA1_START_TIME" + "\n");
                    }
                    else
                    {
                        strSqlString.Append("        ORDER BY DA1_START_TIME" + "\n");
                    }
                }
                if (cdvOper.Text.Contains("A0401"))
                {
                    strSqlString.Append("         AND DA2_START_TIME <> ' ' " + "\n");
                    if (ckRes.Checked == true)
                    {
                        strSqlString.Append("        ORDER BY DA2_RES_ID, DA2_START_TIME" + "\n");
                    }
                    else
                    {
                        strSqlString.Append("        ORDER BY DA2_START_TIME" + "\n");
                    }
                }
                if (cdvOper.Text.Contains("A0402"))
                {
                    strSqlString.Append("         AND DA3_START_TIME <> ' ' " + "\n");
                    if (ckRes.Checked == true)
                    {
                        strSqlString.Append("        ORDER BY DA3_RES_ID, DA3_START_TIME" + "\n");
                    }
                    else
                    {
                        strSqlString.Append("        ORDER BY DA3_START_TIME" + "\n");
                    }
                }
                if (cdvOper.Text.Contains("A0600"))
                {
                    strSqlString.Append("         AND WB1_START_TIME <> ' ' " + "\n");
                    if (ckRes.Checked == true)
                    {
                        strSqlString.Append("        ORDER BY WB1_RES_ID, WB1_START_TIME" + "\n");
                    }
                    else
                    {
                        strSqlString.Append("        ORDER BY WB1_START_TIME" + "\n");
                    }
                }
                if (cdvOper.Text.Contains("A0601"))
                {
                    strSqlString.Append("         AND WB2_START_TIME <> ' ' " + "\n");
                    if (ckRes.Checked == true)
                    {
                        strSqlString.Append("        ORDER BY WB2_RES_ID, WB2_START_TIME" + "\n");
                    }
                    else
                    {
                        strSqlString.Append("        ORDER BY WB2_START_TIME" + "\n");
                    }
                }
                if (cdvOper.Text.Contains("A0602"))
                {
                    strSqlString.Append("         AND WB3_START_TIME <> ' ' " + "\n");
                    if (ckRes.Checked == true)
                    {
                        strSqlString.Append("        ORDER BY WB3_RES_ID, WB3_START_TIME" + "\n");
                    }
                    else
                    {
                        strSqlString.Append("        ORDER BY WB3_START_TIME" + "\n");
                    }
                }
                if (cdvOper.Text.Contains("A1000"))
                {
                    strSqlString.Append("         AND MD_START_TIME <> ' ' " + "\n");
                    if (ckRes.Checked == true)
                    {
                        strSqlString.Append("        ORDER BY MD_RES_ID, MD_START_TIME" + "\n");
                    }
                    else
                    {
                        strSqlString.Append("        ORDER BY MD_START_TIME" + "\n");
                    }
                }
                if (cdvOper.Text.Contains("A1150"))
                {
                    strSqlString.Append("         AND MK_START_TIME <> ' ' " + "\n");
                    if (ckRes.Checked == true)
                    {
                        strSqlString.Append("        ORDER BY MK_RES_ID, MK_START_TIME" + "\n");
                    }
                    else
                    {
                        strSqlString.Append("        ORDER BY MK_START_TIME" + "\n");
                    }
                }
                if (cdvOper.Text.Contains("A1300"))
                {
                    strSqlString.Append("         AND BM_START_TIME <> ' ' " + "\n");
                    if (ckRes.Checked == true)
                    {
                        strSqlString.Append("        ORDER BY BM_RES_ID, BM_START_TIME" + "\n");
                    }
                    else
                    {
                        strSqlString.Append("        ORDER BY BM_START_TIME" + "\n");
                    }
                }
                if (cdvOper.Text.Contains("A1750"))
                {
                    strSqlString.Append("         AND PS_START_TIME <> ' ' " + "\n");
                    if (ckRes.Checked == true)
                    {
                        strSqlString.Append("        ORDER BY PS_RES_ID, PS_START_TIME" + "\n");
                    }
                    else
                    {
                        strSqlString.Append("        ORDER BY PS_START_TIME" + "\n");
                    }
                }
                if (cdvOper.Text.Contains("A1200"))
                {
                    strSqlString.Append("         AND TR_START_TIME <> ' ' " + "\n");
                    if (ckRes.Checked == true)
                    {
                        strSqlString.Append("        ORDER BY TR_RES_ID, TR_START_TIME" + "\n");
                    }
                    else
                    {
                        strSqlString.Append("        ORDER BY TR_START_TIME" + "\n");
                    }
                }
                if (cdvOper.Text.Contains("A1800"))
                {
                    strSqlString.Append("         AND FORM_START_TIME <> ' ' " + "\n");
                    if (ckRes.Checked == true)
                    {
                        strSqlString.Append("        ORDER BY FORM_RES_ID, FORM_START_TIME" + "\n");
                    }
                    else
                    {
                        strSqlString.Append("        ORDER BY FORM_START_TIME" + "\n");
                    }
                }
                if (cdvOper.Text.Contains("A2000"))
                {
                    strSqlString.Append("         AND AV_START_TIME <> ' ' " + "\n");
                    if (ckRes.Checked == true)
                    {
                        strSqlString.Append("        ORDER BY AV_RES_ID, AV_START_TIME" + "\n");
                    }
                    else
                    {
                        strSqlString.Append("        ORDER BY AV_START_TIME" + "\n");
                    }
                }
            }
            
            if (GlobalVariable.gsUserGroup == "ADMIN_GROUP" || GlobalVariable.gsUserGroup == "HANA_ADMIN_GROUP")
            {
                System.Windows.Forms.Clipboard.SetText(strSqlString.ToString());
            }

            return strSqlString.ToString();
        }


        
        /// 설비별 DC YIELD
        private string MakeSqlStringRes()
        {
            StringBuilder strSqlString = new StringBuilder();

            if (cdvOper.Text != "ALL" && cdvOper.Text != "")
            {
                if (cdvOper.Text.Contains("A0040"))
                {
                    strSqlString.Append("SELECT WORK_DATE,LOT_ID,BG_RES_ID,DC_YLD,WORK_DATE||'-'||LOT_ID||'('||BG_RES_ID||')'" + "\n");
                    strSqlString.Append("  FROM RSUMDCLHIS A, MWIPCALDEF B" + "\n");
                    strSqlString.Append(" WHERE 1=1 " + "\n");
                    if (cdvCustomer.Text != "" && cdvCustomer.Text != "ALL")
                    {
                        strSqlString.Append("   AND CUSTOMER = '" + cdvCustomer.Text + "' " + "\n");
                    }

                    strSqlString.Append("   AND LOT_ID LIKE '" + txtLotID.Text + "' " + "\n");
                    strSqlString.Append("   AND MAT_ID LIKE '" + txtSearchProduct.Text + "' " + "\n");

                    #region 상세 조회에 따른 SQL문 생성
                    if (udcWIPCondition1.Text != "ALL" && udcWIPCondition1.Text != "")
                        //strSqlString.AppendFormat("   AND CUSTOMER {0} " + "\n", udcWIPCondition1.SelectedValueToQueryString);

                    if (udcWIPCondition2.Text != "ALL" && udcWIPCondition2.Text != "")
                            strSqlString.AppendFormat("   AND FAMILY {0} " + "\n", udcWIPCondition2.SelectedValueToQueryString);

                    if (udcWIPCondition3.Text != "ALL" && udcWIPCondition3.Text != "")
                        strSqlString.AppendFormat("   AND PKG {0} " + "\n", udcWIPCondition3.SelectedValueToQueryString);

                    if (udcWIPCondition4.Text != "ALL" && udcWIPCondition4.Text != "")
                        strSqlString.AppendFormat("   AND TYPE1 {0} " + "\n", udcWIPCondition4.SelectedValueToQueryString);

                    if (udcWIPCondition5.Text != "ALL" && udcWIPCondition5.Text != "")
                        strSqlString.AppendFormat("   AND TYPE2 {0} " + "\n", udcWIPCondition5.SelectedValueToQueryString);

                    if (udcWIPCondition6.Text != "ALL" && udcWIPCondition6.Text != "")
                        strSqlString.AppendFormat("   AND LD_COUNT {0} " + "\n", udcWIPCondition6.SelectedValueToQueryString);

                    if (udcWIPCondition7.Text != "ALL" && udcWIPCondition7.Text != "")
                        strSqlString.AppendFormat("   AND DENSITY {0} " + "\n", udcWIPCondition7.SelectedValueToQueryString);

                    if (udcWIPCondition8.Text != "ALL" && udcWIPCondition8.Text != "")
                        strSqlString.AppendFormat("   AND GENERATION {0} " + "\n", udcWIPCondition8.SelectedValueToQueryString);

                    if (udcRASCondition6.Text != "ALL" && udcRASCondition6.Text != "")
                    {
                        strSqlString.AppendFormat("   AND (BG_RES_ID {0} " + "\n", udcRASCondition6.SelectedValueToQueryString);
                        strSqlString.AppendFormat("    OR SAW_RES_ID {0} " + "\n", udcRASCondition6.SelectedValueToQueryString);
                        strSqlString.AppendFormat("    OR DA1_RES_ID {0} " + "\n", udcRASCondition6.SelectedValueToQueryString);
                        strSqlString.AppendFormat("    OR DA2_RES_ID {0} " + "\n", udcRASCondition6.SelectedValueToQueryString);
                        strSqlString.AppendFormat("    OR DA3_RES_ID {0} " + "\n", udcRASCondition6.SelectedValueToQueryString);
                        strSqlString.AppendFormat("    OR WB1_RES_ID {0} " + "\n", udcRASCondition6.SelectedValueToQueryString);
                        strSqlString.AppendFormat("    OR WB2_RES_ID {0} " + "\n", udcRASCondition6.SelectedValueToQueryString);
                        strSqlString.AppendFormat("    OR WB3_RES_ID {0} " + "\n", udcRASCondition6.SelectedValueToQueryString);
                        strSqlString.AppendFormat("    OR MD_RES_ID {0} )" + "\n", udcRASCondition6.SelectedValueToQueryString);
                        strSqlString.AppendFormat("    OR MK_RES_ID {0} " + "\n", udcRASCondition6.SelectedValueToQueryString);
                        strSqlString.AppendFormat("    OR BM_RES_ID {0} " + "\n", udcRASCondition6.SelectedValueToQueryString);
                        strSqlString.AppendFormat("    OR PS_RES_ID {0} " + "\n", udcRASCondition6.SelectedValueToQueryString);
                        strSqlString.AppendFormat("    OR TR_RES_ID {0} " + "\n", udcRASCondition6.SelectedValueToQueryString);
                        strSqlString.AppendFormat("    OR FORM_RES_ID {0} " + "\n", udcRASCondition6.SelectedValueToQueryString);
                        strSqlString.AppendFormat("    OR AV_RES_ID {0} " + "\n", udcRASCondition6.SelectedValueToQueryString);
                    }

                    #endregion

                    strSqlString.Append("   AND A.WORK_DATE = B.SYS_DATE " + "\n");
                    strSqlString.Append("   AND B.CALENDAR_ID = 'HM' " + "\n");
                    strSqlString.Append("   AND WORK_DATE >='" + cdvFromToDate.FromDate.Text.Replace("-", "") + "' " + "\n");
                    strSqlString.Append("   AND WORK_DATE <='" + cdvFromToDate.ToDate.Text.Replace("-", "") + "' " + "\n");
                    strSqlString.Append("   AND DC_YLD <= " + txtYield.Text + "\n");
                    strSqlString.Append("   AND BG_START_TIME <> ' ' " + "\n");
                    strSqlString.Append(" ORDER BY WORK_DATE,BG_RES_ID" + "\n");
                }
                if (cdvOper.Text.Contains("A0200"))
                {
                    strSqlString.Append("SELECT WORK_DATE,LOT_ID,SAW_RES_ID,DC_YLD,WORK_DATE||'-'||LOT_ID||'('||SAW_RES_ID||')'" + "\n");
                    strSqlString.Append("  FROM RSUMDCLHIS A, MWIPCALDEF B" + "\n");
                    strSqlString.Append(" WHERE 1=1 " + "\n");
                    if (cdvCustomer.Text != "" && cdvCustomer.Text != "ALL")
                    {
                        strSqlString.Append("   AND CUSTOMER = '" + cdvCustomer.Text + "' " + "\n");
                    }

                    strSqlString.Append("   AND LOT_ID LIKE '" + txtLotID.Text + "' " + "\n");
                    strSqlString.Append("   AND MAT_ID LIKE '" + txtSearchProduct.Text + "' " + "\n");

                    #region 상세 조회에 따른 SQL문 생성
                    if (udcWIPCondition1.Text != "ALL" && udcWIPCondition1.Text != "")
                        //strSqlString.AppendFormat("   AND CUSTOMER {0} " + "\n", udcWIPCondition1.SelectedValueToQueryString);

                    if (udcWIPCondition2.Text != "ALL" && udcWIPCondition2.Text != "")
                            strSqlString.AppendFormat("   AND FAMILY {0} " + "\n", udcWIPCondition2.SelectedValueToQueryString);

                    if (udcWIPCondition3.Text != "ALL" && udcWIPCondition3.Text != "")
                        strSqlString.AppendFormat("   AND PKG {0} " + "\n", udcWIPCondition3.SelectedValueToQueryString);

                    if (udcWIPCondition4.Text != "ALL" && udcWIPCondition4.Text != "")
                        strSqlString.AppendFormat("   AND TYPE1 {0} " + "\n", udcWIPCondition4.SelectedValueToQueryString);

                    if (udcWIPCondition5.Text != "ALL" && udcWIPCondition5.Text != "")
                        strSqlString.AppendFormat("   AND TYPE2 {0} " + "\n", udcWIPCondition5.SelectedValueToQueryString);

                    if (udcWIPCondition6.Text != "ALL" && udcWIPCondition6.Text != "")
                        strSqlString.AppendFormat("   AND LD_COUNT {0} " + "\n", udcWIPCondition6.SelectedValueToQueryString);

                    if (udcWIPCondition7.Text != "ALL" && udcWIPCondition7.Text != "")
                        strSqlString.AppendFormat("   AND DENSITY {0} " + "\n", udcWIPCondition7.SelectedValueToQueryString);

                    if (udcWIPCondition8.Text != "ALL" && udcWIPCondition8.Text != "")
                        strSqlString.AppendFormat("   AND GENERATION {0} " + "\n", udcWIPCondition8.SelectedValueToQueryString);

                    if (udcRASCondition6.Text != "ALL" && udcRASCondition6.Text != "")
                    {
                        strSqlString.AppendFormat("   AND (BG_RES_ID {0} " + "\n", udcRASCondition6.SelectedValueToQueryString);
                        strSqlString.AppendFormat("    OR SAW_RES_ID {0} " + "\n", udcRASCondition6.SelectedValueToQueryString);
                        strSqlString.AppendFormat("    OR DA1_RES_ID {0} " + "\n", udcRASCondition6.SelectedValueToQueryString);
                        strSqlString.AppendFormat("    OR DA2_RES_ID {0} " + "\n", udcRASCondition6.SelectedValueToQueryString);
                        strSqlString.AppendFormat("    OR DA3_RES_ID {0} " + "\n", udcRASCondition6.SelectedValueToQueryString);
                        strSqlString.AppendFormat("    OR WB1_RES_ID {0} " + "\n", udcRASCondition6.SelectedValueToQueryString);
                        strSqlString.AppendFormat("    OR WB2_RES_ID {0} " + "\n", udcRASCondition6.SelectedValueToQueryString);
                        strSqlString.AppendFormat("    OR WB3_RES_ID {0} " + "\n", udcRASCondition6.SelectedValueToQueryString);
                        strSqlString.AppendFormat("    OR MD_RES_ID {0} )" + "\n", udcRASCondition6.SelectedValueToQueryString);
                        strSqlString.AppendFormat("    OR MK_RES_ID {0} " + "\n", udcRASCondition6.SelectedValueToQueryString);
                        strSqlString.AppendFormat("    OR BM_RES_ID {0} " + "\n", udcRASCondition6.SelectedValueToQueryString);
                        strSqlString.AppendFormat("    OR PS_RES_ID {0} " + "\n", udcRASCondition6.SelectedValueToQueryString);
                        strSqlString.AppendFormat("    OR TR_RES_ID {0} " + "\n", udcRASCondition6.SelectedValueToQueryString);
                        strSqlString.AppendFormat("    OR FORM_RES_ID {0} " + "\n", udcRASCondition6.SelectedValueToQueryString);
                        strSqlString.AppendFormat("    OR AV_RES_ID {0} " + "\n", udcRASCondition6.SelectedValueToQueryString);
                    }

                    #endregion
                    strSqlString.Append("   AND A.WORK_DATE = B.SYS_DATE " + "\n");
                    strSqlString.Append("   AND B.CALENDAR_ID = 'HM' " + "\n");
                    strSqlString.Append("   AND WORK_DATE >='" + cdvFromToDate.FromDate.Text.Replace("-", "") + "' " + "\n");
                    strSqlString.Append("   AND WORK_DATE <='" + cdvFromToDate.ToDate.Text.Replace("-", "") + "' " + "\n");
                    strSqlString.Append("   AND DC_YLD <= " + txtYield.Text + "\n");
                    strSqlString.Append("   AND SAW_START_TIME <> ' ' " + "\n");
                    strSqlString.Append(" ORDER BY WORK_DATE,SAW_RES_ID" + "\n");
                   
                }
                if (cdvOper.Text.Contains("A0400"))
                {
                    strSqlString.Append("SELECT WORK_DATE,LOT_ID,DA1_RES_ID,DC_YLD,WORK_DATE||'-'||LOT_ID||'('||DA1_RES_ID||')'" + "\n");
                    strSqlString.Append("  FROM RSUMDCLHIS A, MWIPCALDEF B" + "\n");
                    strSqlString.Append(" WHERE 1=1 " + "\n");
                    if (cdvCustomer.Text != "" && cdvCustomer.Text != "ALL")
                    {
                        strSqlString.Append("   AND CUSTOMER = '" + cdvCustomer.Text + "' " + "\n");
                    }

                    strSqlString.Append("   AND LOT_ID LIKE '" + txtLotID.Text + "' " + "\n");
                    strSqlString.Append("   AND MAT_ID LIKE '" + txtSearchProduct.Text + "' " + "\n");

                    #region 상세 조회에 따른 SQL문 생성
                    if (udcWIPCondition1.Text != "ALL" && udcWIPCondition1.Text != "")
                        //strSqlString.AppendFormat("   AND CUSTOMER {0} " + "\n", udcWIPCondition1.SelectedValueToQueryString);

                    if (udcWIPCondition2.Text != "ALL" && udcWIPCondition2.Text != "")
                            strSqlString.AppendFormat("   AND FAMILY {0} " + "\n", udcWIPCondition2.SelectedValueToQueryString);

                    if (udcWIPCondition3.Text != "ALL" && udcWIPCondition3.Text != "")
                        strSqlString.AppendFormat("   AND PKG {0} " + "\n", udcWIPCondition3.SelectedValueToQueryString);

                    if (udcWIPCondition4.Text != "ALL" && udcWIPCondition4.Text != "")
                        strSqlString.AppendFormat("   AND TYPE1 {0} " + "\n", udcWIPCondition4.SelectedValueToQueryString);

                    if (udcWIPCondition5.Text != "ALL" && udcWIPCondition5.Text != "")
                        strSqlString.AppendFormat("   AND TYPE2 {0} " + "\n", udcWIPCondition5.SelectedValueToQueryString);

                    if (udcWIPCondition6.Text != "ALL" && udcWIPCondition6.Text != "")
                        strSqlString.AppendFormat("   AND LD_COUNT {0} " + "\n", udcWIPCondition6.SelectedValueToQueryString);

                    if (udcWIPCondition7.Text != "ALL" && udcWIPCondition7.Text != "")
                        strSqlString.AppendFormat("   AND DENSITY {0} " + "\n", udcWIPCondition7.SelectedValueToQueryString);

                    if (udcWIPCondition8.Text != "ALL" && udcWIPCondition8.Text != "")
                        strSqlString.AppendFormat("   AND GENERATION {0} " + "\n", udcWIPCondition8.SelectedValueToQueryString);

                    if (udcRASCondition6.Text != "ALL" && udcRASCondition6.Text != "")
                    {
                        strSqlString.AppendFormat("   AND (BG_RES_ID {0} " + "\n", udcRASCondition6.SelectedValueToQueryString);
                        strSqlString.AppendFormat("    OR SAW_RES_ID {0} " + "\n", udcRASCondition6.SelectedValueToQueryString);
                        strSqlString.AppendFormat("    OR DA1_RES_ID {0} " + "\n", udcRASCondition6.SelectedValueToQueryString);
                        strSqlString.AppendFormat("    OR DA2_RES_ID {0} " + "\n", udcRASCondition6.SelectedValueToQueryString);
                        strSqlString.AppendFormat("    OR DA3_RES_ID {0} " + "\n", udcRASCondition6.SelectedValueToQueryString);
                        strSqlString.AppendFormat("    OR WB1_RES_ID {0} " + "\n", udcRASCondition6.SelectedValueToQueryString);
                        strSqlString.AppendFormat("    OR WB2_RES_ID {0} " + "\n", udcRASCondition6.SelectedValueToQueryString);
                        strSqlString.AppendFormat("    OR WB3_RES_ID {0} " + "\n", udcRASCondition6.SelectedValueToQueryString);
                        strSqlString.AppendFormat("    OR MD_RES_ID {0} )" + "\n", udcRASCondition6.SelectedValueToQueryString);
                        strSqlString.AppendFormat("    OR MK_RES_ID {0} " + "\n", udcRASCondition6.SelectedValueToQueryString);
                        strSqlString.AppendFormat("    OR BM_RES_ID {0} " + "\n", udcRASCondition6.SelectedValueToQueryString);
                        strSqlString.AppendFormat("    OR PS_RES_ID {0} " + "\n", udcRASCondition6.SelectedValueToQueryString);
                        strSqlString.AppendFormat("    OR TR_RES_ID {0} " + "\n", udcRASCondition6.SelectedValueToQueryString);
                        strSqlString.AppendFormat("    OR FORM_RES_ID {0} " + "\n", udcRASCondition6.SelectedValueToQueryString);
                        strSqlString.AppendFormat("    OR AV_RES_ID {0} " + "\n", udcRASCondition6.SelectedValueToQueryString);
                    }

                    #endregion
                    strSqlString.Append("   AND A.WORK_DATE = B.SYS_DATE " + "\n");
                    strSqlString.Append("   AND B.CALENDAR_ID = 'HM' " + "\n");
                    strSqlString.Append("   AND WORK_DATE >='" + cdvFromToDate.FromDate.Text.Replace("-", "") + "' " + "\n");
                    strSqlString.Append("   AND WORK_DATE <='" + cdvFromToDate.ToDate.Text.Replace("-", "") + "' " + "\n");
                    strSqlString.Append("   AND DC_YLD <= " + txtYield.Text + "\n");
                    strSqlString.Append("   AND DA1_START_TIME <> ' ' " + "\n");
                    strSqlString.Append(" ORDER BY WORK_DATE,DA1_RES_ID" + "\n");
                   
                }
                if (cdvOper.Text.Contains("A0401"))
                {
                    strSqlString.Append("SELECT WORK_DATE,LOT_ID,DA2_RES_ID,DC_YLD,WORK_DATE||'-'||LOT_ID||'('||DA2_RES_ID||')'" + "\n");
                    strSqlString.Append("  FROM RSUMDCLHIS A, MWIPCALDEF B" + "\n");
                    strSqlString.Append(" WHERE 1=1 " + "\n");
                    if (cdvCustomer.Text != "" && cdvCustomer.Text != "ALL")
                    {
                        strSqlString.Append("   AND CUSTOMER = '" + cdvCustomer.Text + "' " + "\n");
                    }

                    strSqlString.Append("   AND LOT_ID LIKE '" + txtLotID.Text + "' " + "\n");
                    strSqlString.Append("   AND MAT_ID LIKE '" + txtSearchProduct.Text + "' " + "\n");

                    #region 상세 조회에 따른 SQL문 생성
                    if (udcWIPCondition1.Text != "ALL" && udcWIPCondition1.Text != "")
                        //strSqlString.AppendFormat("   AND CUSTOMER {0} " + "\n", udcWIPCondition1.SelectedValueToQueryString);

                     if (udcWIPCondition2.Text != "ALL" && udcWIPCondition2.Text != "")
                            strSqlString.AppendFormat("   AND FAMILY {0} " + "\n", udcWIPCondition2.SelectedValueToQueryString);

                    if (udcWIPCondition3.Text != "ALL" && udcWIPCondition3.Text != "")
                        strSqlString.AppendFormat("   AND PKG {0} " + "\n", udcWIPCondition3.SelectedValueToQueryString);

                    if (udcWIPCondition4.Text != "ALL" && udcWIPCondition4.Text != "")
                        strSqlString.AppendFormat("   AND TYPE1 {0} " + "\n", udcWIPCondition4.SelectedValueToQueryString);

                    if (udcWIPCondition5.Text != "ALL" && udcWIPCondition5.Text != "")
                        strSqlString.AppendFormat("   AND TYPE2 {0} " + "\n", udcWIPCondition5.SelectedValueToQueryString);

                    if (udcWIPCondition6.Text != "ALL" && udcWIPCondition6.Text != "")
                        strSqlString.AppendFormat("   AND LD_COUNT {0} " + "\n", udcWIPCondition6.SelectedValueToQueryString);

                    if (udcWIPCondition7.Text != "ALL" && udcWIPCondition7.Text != "")
                        strSqlString.AppendFormat("   AND DENSITY {0} " + "\n", udcWIPCondition7.SelectedValueToQueryString);

                    if (udcWIPCondition8.Text != "ALL" && udcWIPCondition8.Text != "")
                        strSqlString.AppendFormat("   AND GENERATION {0} " + "\n", udcWIPCondition8.SelectedValueToQueryString);

                    if (udcRASCondition6.Text != "ALL" && udcRASCondition6.Text != "")
                    {
                        strSqlString.AppendFormat("   AND (BG_RES_ID {0} " + "\n", udcRASCondition6.SelectedValueToQueryString);
                        strSqlString.AppendFormat("    OR SAW_RES_ID {0} " + "\n", udcRASCondition6.SelectedValueToQueryString);
                        strSqlString.AppendFormat("    OR DA1_RES_ID {0} " + "\n", udcRASCondition6.SelectedValueToQueryString);
                        strSqlString.AppendFormat("    OR DA2_RES_ID {0} " + "\n", udcRASCondition6.SelectedValueToQueryString);
                        strSqlString.AppendFormat("    OR DA3_RES_ID {0} " + "\n", udcRASCondition6.SelectedValueToQueryString);
                        strSqlString.AppendFormat("    OR WB1_RES_ID {0} " + "\n", udcRASCondition6.SelectedValueToQueryString);
                        strSqlString.AppendFormat("    OR WB2_RES_ID {0} " + "\n", udcRASCondition6.SelectedValueToQueryString);
                        strSqlString.AppendFormat("    OR WB3_RES_ID {0} " + "\n", udcRASCondition6.SelectedValueToQueryString);
                        strSqlString.AppendFormat("    OR MD_RES_ID {0} )" + "\n", udcRASCondition6.SelectedValueToQueryString);
                        strSqlString.AppendFormat("    OR MK_RES_ID {0} " + "\n", udcRASCondition6.SelectedValueToQueryString);
                        strSqlString.AppendFormat("    OR BM_RES_ID {0} " + "\n", udcRASCondition6.SelectedValueToQueryString);
                        strSqlString.AppendFormat("    OR PS_RES_ID {0} " + "\n", udcRASCondition6.SelectedValueToQueryString);
                        strSqlString.AppendFormat("    OR TR_RES_ID {0} " + "\n", udcRASCondition6.SelectedValueToQueryString);
                        strSqlString.AppendFormat("    OR FORM_RES_ID {0} " + "\n", udcRASCondition6.SelectedValueToQueryString);
                        strSqlString.AppendFormat("    OR AV_RES_ID {0} " + "\n", udcRASCondition6.SelectedValueToQueryString);
                    }

                    #endregion
                    strSqlString.Append("   AND A.WORK_DATE = B.SYS_DATE " + "\n");
                    strSqlString.Append("   AND B.CALENDAR_ID = 'HM' " + "\n");
                    strSqlString.Append("   AND WORK_DATE >='" + cdvFromToDate.FromDate.Text.Replace("-", "") + "' " + "\n");
                    strSqlString.Append("   AND WORK_DATE <='" + cdvFromToDate.ToDate.Text.Replace("-", "") + "' " + "\n");
                    strSqlString.Append("   AND DC_YLD <= " + txtYield.Text + "\n");
                    strSqlString.Append("   AND DA2_START_TIME <> ' ' " + "\n");
                    strSqlString.Append(" ORDER BY WORK_DATE,DA2_RES_ID" + "\n");
                   
                }
                if (cdvOper.Text.Contains("A0402"))
                {
                    strSqlString.Append("SELECT WORK_DATE,LOT_ID,DA3_RES_ID,DC_YLD,WORK_DATE||'-'||LOT_ID||'('||DA3_RES_ID||')'" + "\n");
                    strSqlString.Append("  FROM RSUMDCLHIS A, MWIPCALDEF B" + "\n");
                    strSqlString.Append(" WHERE 1=1 " + "\n");
                    if (cdvCustomer.Text != "" && cdvCustomer.Text != "ALL")
                    {
                        strSqlString.Append("   AND CUSTOMER = '" + cdvCustomer.Text + "' " + "\n");
                    }

                    strSqlString.Append("   AND LOT_ID LIKE '" + txtLotID.Text + "' " + "\n");
                    strSqlString.Append("   AND MAT_ID LIKE '" + txtSearchProduct.Text + "' " + "\n");

                    #region 상세 조회에 따른 SQL문 생성
                    if (udcWIPCondition1.Text != "ALL" && udcWIPCondition1.Text != "")
                        //strSqlString.AppendFormat("   AND CUSTOMER {0} " + "\n", udcWIPCondition1.SelectedValueToQueryString);

                    if (udcWIPCondition2.Text != "ALL" && udcWIPCondition2.Text != "")
                            strSqlString.AppendFormat("   AND FAMILY {0} " + "\n", udcWIPCondition2.SelectedValueToQueryString);

                    if (udcWIPCondition3.Text != "ALL" && udcWIPCondition3.Text != "")
                        strSqlString.AppendFormat("   AND PKG {0} " + "\n", udcWIPCondition3.SelectedValueToQueryString);

                    if (udcWIPCondition4.Text != "ALL" && udcWIPCondition4.Text != "")
                        strSqlString.AppendFormat("   AND TYPE1 {0} " + "\n", udcWIPCondition4.SelectedValueToQueryString);

                    if (udcWIPCondition5.Text != "ALL" && udcWIPCondition5.Text != "")
                        strSqlString.AppendFormat("   AND TYPE2 {0} " + "\n", udcWIPCondition5.SelectedValueToQueryString);

                    if (udcWIPCondition6.Text != "ALL" && udcWIPCondition6.Text != "")
                        strSqlString.AppendFormat("   AND LD_COUNT {0} " + "\n", udcWIPCondition6.SelectedValueToQueryString);

                    if (udcWIPCondition7.Text != "ALL" && udcWIPCondition7.Text != "")
                        strSqlString.AppendFormat("   AND DENSITY {0} " + "\n", udcWIPCondition7.SelectedValueToQueryString);

                    if (udcWIPCondition8.Text != "ALL" && udcWIPCondition8.Text != "")
                        strSqlString.AppendFormat("   AND GENERATION {0} " + "\n", udcWIPCondition8.SelectedValueToQueryString);

                    if (udcRASCondition6.Text != "ALL" && udcRASCondition6.Text != "")
                    {
                        strSqlString.AppendFormat("   AND (BG_RES_ID {0} " + "\n", udcRASCondition6.SelectedValueToQueryString);
                        strSqlString.AppendFormat("    OR SAW_RES_ID {0} " + "\n", udcRASCondition6.SelectedValueToQueryString);
                        strSqlString.AppendFormat("    OR DA1_RES_ID {0} " + "\n", udcRASCondition6.SelectedValueToQueryString);
                        strSqlString.AppendFormat("    OR DA2_RES_ID {0} " + "\n", udcRASCondition6.SelectedValueToQueryString);
                        strSqlString.AppendFormat("    OR DA3_RES_ID {0} " + "\n", udcRASCondition6.SelectedValueToQueryString);
                        strSqlString.AppendFormat("    OR WB1_RES_ID {0} " + "\n", udcRASCondition6.SelectedValueToQueryString);
                        strSqlString.AppendFormat("    OR WB2_RES_ID {0} " + "\n", udcRASCondition6.SelectedValueToQueryString);
                        strSqlString.AppendFormat("    OR WB3_RES_ID {0} " + "\n", udcRASCondition6.SelectedValueToQueryString);
                        strSqlString.AppendFormat("    OR MD_RES_ID {0} )" + "\n", udcRASCondition6.SelectedValueToQueryString);
                        strSqlString.AppendFormat("    OR MK_RES_ID {0} " + "\n", udcRASCondition6.SelectedValueToQueryString);
                        strSqlString.AppendFormat("    OR BM_RES_ID {0} " + "\n", udcRASCondition6.SelectedValueToQueryString);
                        strSqlString.AppendFormat("    OR PS_RES_ID {0} " + "\n", udcRASCondition6.SelectedValueToQueryString);
                        strSqlString.AppendFormat("    OR TR_RES_ID {0} " + "\n", udcRASCondition6.SelectedValueToQueryString);
                        strSqlString.AppendFormat("    OR FORM_RES_ID {0} " + "\n", udcRASCondition6.SelectedValueToQueryString);
                        strSqlString.AppendFormat("    OR AV_RES_ID {0} " + "\n", udcRASCondition6.SelectedValueToQueryString);
                    }

                    #endregion
                    strSqlString.Append("   AND A.WORK_DATE = B.SYS_DATE " + "\n");
                    strSqlString.Append("   AND B.CALENDAR_ID = 'HM' " + "\n");
                    strSqlString.Append("   AND WORK_DATE >='" + cdvFromToDate.FromDate.Text.Replace("-", "") + "' " + "\n");
                    strSqlString.Append("   AND WORK_DATE <='" + cdvFromToDate.ToDate.Text.Replace("-", "") + "' " + "\n");
                    strSqlString.Append("   AND DC_YLD <= " + txtYield.Text + "\n");
                    strSqlString.Append("   AND DA3_START_TIME <> ' ' " + "\n");
                    strSqlString.Append(" ORDER BY WORK_DATE,DA3_RES_ID" + "\n");
                   
                }
                if (cdvOper.Text.Contains("A0600"))
                {
                    strSqlString.Append("SELECT WORK_DATE,LOT_ID,WB1_RES_ID,DC_YLD,WORK_DATE||'-'||LOT_ID||'('||WB1_RES_ID||')'" + "\n");
                    strSqlString.Append("  FROM RSUMDCLHIS A, MWIPCALDEF B" + "\n");
                    strSqlString.Append(" WHERE 1=1 " + "\n");
                    if (cdvCustomer.Text != "" && cdvCustomer.Text != "ALL")
                    {
                        strSqlString.Append("   AND CUSTOMER = '" + cdvCustomer.Text + "' " + "\n");
                    }

                    strSqlString.Append("   AND LOT_ID LIKE '" + txtLotID.Text + "' " + "\n");
                    strSqlString.Append("   AND MAT_ID LIKE '" + txtSearchProduct.Text + "' " + "\n");

                    #region 상세 조회에 따른 SQL문 생성
                    if (udcWIPCondition1.Text != "ALL" && udcWIPCondition1.Text != "")
                        //strSqlString.AppendFormat("   AND CUSTOMER {0} " + "\n", udcWIPCondition1.SelectedValueToQueryString);

                    if (udcWIPCondition2.Text != "ALL" && udcWIPCondition2.Text != "")
                            strSqlString.AppendFormat("   AND FAMILY {0} " + "\n", udcWIPCondition2.SelectedValueToQueryString);

                    if (udcWIPCondition3.Text != "ALL" && udcWIPCondition3.Text != "")
                        strSqlString.AppendFormat("   AND PKG {0} " + "\n", udcWIPCondition3.SelectedValueToQueryString);

                    if (udcWIPCondition4.Text != "ALL" && udcWIPCondition4.Text != "")
                        strSqlString.AppendFormat("   AND TYPE1 {0} " + "\n", udcWIPCondition4.SelectedValueToQueryString);

                    if (udcWIPCondition5.Text != "ALL" && udcWIPCondition5.Text != "")
                        strSqlString.AppendFormat("   AND TYPE2 {0} " + "\n", udcWIPCondition5.SelectedValueToQueryString);

                    if (udcWIPCondition6.Text != "ALL" && udcWIPCondition6.Text != "")
                        strSqlString.AppendFormat("   AND LD_COUNT {0} " + "\n", udcWIPCondition6.SelectedValueToQueryString);

                    if (udcWIPCondition7.Text != "ALL" && udcWIPCondition7.Text != "")
                        strSqlString.AppendFormat("   AND DENSITY {0} " + "\n", udcWIPCondition7.SelectedValueToQueryString);

                    if (udcWIPCondition8.Text != "ALL" && udcWIPCondition8.Text != "")
                        strSqlString.AppendFormat("   AND GENERATION {0} " + "\n", udcWIPCondition8.SelectedValueToQueryString);

                    if (udcRASCondition6.Text != "ALL" && udcRASCondition6.Text != "")
                    {
                        strSqlString.AppendFormat("   AND (BG_RES_ID {0} " + "\n", udcRASCondition6.SelectedValueToQueryString);
                        strSqlString.AppendFormat("    OR SAW_RES_ID {0} " + "\n", udcRASCondition6.SelectedValueToQueryString);
                        strSqlString.AppendFormat("    OR DA1_RES_ID {0} " + "\n", udcRASCondition6.SelectedValueToQueryString);
                        strSqlString.AppendFormat("    OR DA2_RES_ID {0} " + "\n", udcRASCondition6.SelectedValueToQueryString);
                        strSqlString.AppendFormat("    OR DA3_RES_ID {0} " + "\n", udcRASCondition6.SelectedValueToQueryString);
                        strSqlString.AppendFormat("    OR WB1_RES_ID {0} " + "\n", udcRASCondition6.SelectedValueToQueryString);
                        strSqlString.AppendFormat("    OR WB2_RES_ID {0} " + "\n", udcRASCondition6.SelectedValueToQueryString);
                        strSqlString.AppendFormat("    OR WB3_RES_ID {0} " + "\n", udcRASCondition6.SelectedValueToQueryString);
                        strSqlString.AppendFormat("    OR MD_RES_ID {0} )" + "\n", udcRASCondition6.SelectedValueToQueryString);
                        strSqlString.AppendFormat("    OR MK_RES_ID {0} " + "\n", udcRASCondition6.SelectedValueToQueryString);
                        strSqlString.AppendFormat("    OR BM_RES_ID {0} " + "\n", udcRASCondition6.SelectedValueToQueryString);
                        strSqlString.AppendFormat("    OR PS_RES_ID {0} " + "\n", udcRASCondition6.SelectedValueToQueryString);
                        strSqlString.AppendFormat("    OR TR_RES_ID {0} " + "\n", udcRASCondition6.SelectedValueToQueryString);
                        strSqlString.AppendFormat("    OR FORM_RES_ID {0} " + "\n", udcRASCondition6.SelectedValueToQueryString);
                        strSqlString.AppendFormat("    OR AV_RES_ID {0} " + "\n", udcRASCondition6.SelectedValueToQueryString);
                    }

                    #endregion
                    strSqlString.Append("   AND A.WORK_DATE = B.SYS_DATE " + "\n");
                    strSqlString.Append("   AND B.CALENDAR_ID = 'HM' " + "\n");
                    strSqlString.Append("   AND WORK_DATE >='" + cdvFromToDate.FromDate.Text.Replace("-", "") + "' " + "\n");
                    strSqlString.Append("   AND WORK_DATE <='" + cdvFromToDate.ToDate.Text.Replace("-", "") + "' " + "\n");
                    strSqlString.Append("   AND DC_YLD <= " + txtYield.Text + "\n");
                    strSqlString.Append("   AND WB1_START_TIME <> ' ' " + "\n");
                    strSqlString.Append(" ORDER BY WORK_DATE,WB1_RES_ID" + "\n");
                   
                }
                if (cdvOper.Text.Contains("A0601"))
                {
                    strSqlString.Append("SELECT WORK_DATE,LOT_ID,WB2_RES_ID,DC_YLD,WORK_DATE||'-'||LOT_ID||'('||WB2_RES_ID||')'" + "\n");
                    strSqlString.Append("  FROM RSUMDCLHIS A, MWIPCALDEF B" + "\n");
                    strSqlString.Append(" WHERE 1=1 " + "\n");
                    if (cdvCustomer.Text != "" && cdvCustomer.Text != "ALL")
                    {
                        strSqlString.Append("   AND CUSTOMER = '" + cdvCustomer.Text + "' " + "\n");
                    }

                    strSqlString.Append("   AND LOT_ID LIKE '" + txtLotID.Text + "' " + "\n");
                    strSqlString.Append("   AND MAT_ID LIKE '" + txtSearchProduct.Text + "' " + "\n");

                    #region 상세 조회에 따른 SQL문 생성
                    if (udcWIPCondition1.Text != "ALL" && udcWIPCondition1.Text != "")
                        //strSqlString.AppendFormat("   AND CUSTOMER {0} " + "\n", udcWIPCondition1.SelectedValueToQueryString);

                    if (udcWIPCondition2.Text != "ALL" && udcWIPCondition2.Text != "")
                            strSqlString.AppendFormat("   AND FAMILY {0} " + "\n", udcWIPCondition2.SelectedValueToQueryString);

                    if (udcWIPCondition3.Text != "ALL" && udcWIPCondition3.Text != "")
                        strSqlString.AppendFormat("   AND PKG {0} " + "\n", udcWIPCondition3.SelectedValueToQueryString);

                    if (udcWIPCondition4.Text != "ALL" && udcWIPCondition4.Text != "")
                        strSqlString.AppendFormat("   AND TYPE1 {0} " + "\n", udcWIPCondition4.SelectedValueToQueryString);

                    if (udcWIPCondition5.Text != "ALL" && udcWIPCondition5.Text != "")
                        strSqlString.AppendFormat("   AND TYPE2 {0} " + "\n", udcWIPCondition5.SelectedValueToQueryString);

                    if (udcWIPCondition6.Text != "ALL" && udcWIPCondition6.Text != "")
                        strSqlString.AppendFormat("   AND LD_COUNT {0} " + "\n", udcWIPCondition6.SelectedValueToQueryString);

                    if (udcWIPCondition7.Text != "ALL" && udcWIPCondition7.Text != "")
                        strSqlString.AppendFormat("   AND DENSITY {0} " + "\n", udcWIPCondition7.SelectedValueToQueryString);

                    if (udcWIPCondition8.Text != "ALL" && udcWIPCondition8.Text != "")
                        strSqlString.AppendFormat("   AND GENERATION {0} " + "\n", udcWIPCondition8.SelectedValueToQueryString);

                    if (udcRASCondition6.Text != "ALL" && udcRASCondition6.Text != "")
                    {
                        strSqlString.AppendFormat("   AND (BG_RES_ID {0} " + "\n", udcRASCondition6.SelectedValueToQueryString);
                        strSqlString.AppendFormat("    OR SAW_RES_ID {0} " + "\n", udcRASCondition6.SelectedValueToQueryString);
                        strSqlString.AppendFormat("    OR DA1_RES_ID {0} " + "\n", udcRASCondition6.SelectedValueToQueryString);
                        strSqlString.AppendFormat("    OR DA2_RES_ID {0} " + "\n", udcRASCondition6.SelectedValueToQueryString);
                        strSqlString.AppendFormat("    OR DA3_RES_ID {0} " + "\n", udcRASCondition6.SelectedValueToQueryString);
                        strSqlString.AppendFormat("    OR WB1_RES_ID {0} " + "\n", udcRASCondition6.SelectedValueToQueryString);
                        strSqlString.AppendFormat("    OR WB2_RES_ID {0} " + "\n", udcRASCondition6.SelectedValueToQueryString);
                        strSqlString.AppendFormat("    OR WB3_RES_ID {0} " + "\n", udcRASCondition6.SelectedValueToQueryString);
                        strSqlString.AppendFormat("    OR MD_RES_ID {0} )" + "\n", udcRASCondition6.SelectedValueToQueryString);
                        strSqlString.AppendFormat("    OR MK_RES_ID {0} " + "\n", udcRASCondition6.SelectedValueToQueryString);
                        strSqlString.AppendFormat("    OR BM_RES_ID {0} " + "\n", udcRASCondition6.SelectedValueToQueryString);
                        strSqlString.AppendFormat("    OR PS_RES_ID {0} " + "\n", udcRASCondition6.SelectedValueToQueryString);
                        strSqlString.AppendFormat("    OR TR_RES_ID {0} " + "\n", udcRASCondition6.SelectedValueToQueryString);
                        strSqlString.AppendFormat("    OR FORM_RES_ID {0} " + "\n", udcRASCondition6.SelectedValueToQueryString);
                        strSqlString.AppendFormat("    OR AV_RES_ID {0} " + "\n", udcRASCondition6.SelectedValueToQueryString);
                    }

                    #endregion
                    strSqlString.Append("   AND A.WORK_DATE = B.SYS_DATE " + "\n");
                    strSqlString.Append("   AND B.CALENDAR_ID = 'HM' " + "\n");
                    strSqlString.Append("   AND WORK_DATE >='" + cdvFromToDate.FromDate.Text.Replace("-", "") + "' " + "\n");
                    strSqlString.Append("   AND WORK_DATE <='" + cdvFromToDate.ToDate.Text.Replace("-", "") + "' " + "\n");
                    strSqlString.Append("   AND DC_YLD <= " + txtYield.Text + "\n");
                    strSqlString.Append("   AND WB2_START_TIME <> ' ' " + "\n");
                    strSqlString.Append(" ORDER BY WORK_DATE,WB2_RES_ID" + "\n");
                  
                }
                if (cdvOper.Text.Contains("A0602"))
                {
                    strSqlString.Append("SELECT WORK_DATE,LOT_ID,WB3_RES_ID,DC_YLD,WORK_DATE||'-'||LOT_ID||'('||WB3_RES_ID||')'" + "\n");
                    strSqlString.Append("  FROM RSUMDCLHIS A, MWIPCALDEF B" + "\n");
                    strSqlString.Append(" WHERE 1=1 " + "\n");
                    if (cdvCustomer.Text != "" && cdvCustomer.Text != "ALL")
                    {
                        strSqlString.Append("   AND CUSTOMER = '" + cdvCustomer.Text + "' " + "\n");
                    }

                    strSqlString.Append("   AND LOT_ID LIKE '" + txtLotID.Text + "' " + "\n");
                    strSqlString.Append("   AND MAT_ID LIKE '" + txtSearchProduct.Text + "' " + "\n");

                    #region 상세 조회에 따른 SQL문 생성
                    if (udcWIPCondition1.Text != "ALL" && udcWIPCondition1.Text != "")
                        //strSqlString.AppendFormat("   AND CUSTOMER {0} " + "\n", udcWIPCondition1.SelectedValueToQueryString);

                    if (udcWIPCondition2.Text != "ALL" && udcWIPCondition2.Text != "")
                            strSqlString.AppendFormat("   AND FAMILY {0} " + "\n", udcWIPCondition2.SelectedValueToQueryString);

                    if (udcWIPCondition3.Text != "ALL" && udcWIPCondition3.Text != "")
                        strSqlString.AppendFormat("   AND PKG {0} " + "\n", udcWIPCondition3.SelectedValueToQueryString);

                    if (udcWIPCondition4.Text != "ALL" && udcWIPCondition4.Text != "")
                        strSqlString.AppendFormat("   AND TYPE1 {0} " + "\n", udcWIPCondition4.SelectedValueToQueryString);

                    if (udcWIPCondition5.Text != "ALL" && udcWIPCondition5.Text != "")
                        strSqlString.AppendFormat("   AND TYPE2 {0} " + "\n", udcWIPCondition5.SelectedValueToQueryString);

                    if (udcWIPCondition6.Text != "ALL" && udcWIPCondition6.Text != "")
                        strSqlString.AppendFormat("   AND LD_COUNT {0} " + "\n", udcWIPCondition6.SelectedValueToQueryString);

                    if (udcWIPCondition7.Text != "ALL" && udcWIPCondition7.Text != "")
                        strSqlString.AppendFormat("   AND DENSITY {0} " + "\n", udcWIPCondition7.SelectedValueToQueryString);

                    if (udcWIPCondition8.Text != "ALL" && udcWIPCondition8.Text != "")
                        strSqlString.AppendFormat("   AND GENERATION {0} " + "\n", udcWIPCondition8.SelectedValueToQueryString);

                    if (udcRASCondition6.Text != "ALL" && udcRASCondition6.Text != "")
                    {
                        strSqlString.AppendFormat("   AND (BG_RES_ID {0} " + "\n", udcRASCondition6.SelectedValueToQueryString);
                        strSqlString.AppendFormat("    OR SAW_RES_ID {0} " + "\n", udcRASCondition6.SelectedValueToQueryString);
                        strSqlString.AppendFormat("    OR DA1_RES_ID {0} " + "\n", udcRASCondition6.SelectedValueToQueryString);
                        strSqlString.AppendFormat("    OR DA2_RES_ID {0} " + "\n", udcRASCondition6.SelectedValueToQueryString);
                        strSqlString.AppendFormat("    OR DA3_RES_ID {0} " + "\n", udcRASCondition6.SelectedValueToQueryString);
                        strSqlString.AppendFormat("    OR WB1_RES_ID {0} " + "\n", udcRASCondition6.SelectedValueToQueryString);
                        strSqlString.AppendFormat("    OR WB2_RES_ID {0} " + "\n", udcRASCondition6.SelectedValueToQueryString);
                        strSqlString.AppendFormat("    OR WB3_RES_ID {0} " + "\n", udcRASCondition6.SelectedValueToQueryString);
                        strSqlString.AppendFormat("    OR MD_RES_ID {0} )" + "\n", udcRASCondition6.SelectedValueToQueryString);
                        strSqlString.AppendFormat("    OR MK_RES_ID {0} " + "\n", udcRASCondition6.SelectedValueToQueryString);
                        strSqlString.AppendFormat("    OR BM_RES_ID {0} " + "\n", udcRASCondition6.SelectedValueToQueryString);
                        strSqlString.AppendFormat("    OR PS_RES_ID {0} " + "\n", udcRASCondition6.SelectedValueToQueryString);
                        strSqlString.AppendFormat("    OR TR_RES_ID {0} " + "\n", udcRASCondition6.SelectedValueToQueryString);
                        strSqlString.AppendFormat("    OR FORM_RES_ID {0} " + "\n", udcRASCondition6.SelectedValueToQueryString);
                        strSqlString.AppendFormat("    OR AV_RES_ID {0} " + "\n", udcRASCondition6.SelectedValueToQueryString);
                    }

                    #endregion
                    strSqlString.Append("   AND A.WORK_DATE = B.SYS_DATE " + "\n");
                    strSqlString.Append("   AND B.CALENDAR_ID = 'HM' " + "\n");
                    strSqlString.Append("   AND WORK_DATE >='" + cdvFromToDate.FromDate.Text.Replace("-", "") + "' " + "\n");
                    strSqlString.Append("   AND WORK_DATE <='" + cdvFromToDate.ToDate.Text.Replace("-", "") + "' " + "\n");
                    strSqlString.Append("   AND DC_YLD <= " + txtYield.Text + "\n");
                    strSqlString.Append("   AND WB3_START_TIME <> ' ' " + "\n");
                    strSqlString.Append(" ORDER BY WORK_DATE,WB3_RES_ID" + "\n");
                  
                }
                if (cdvOper.Text.Contains("A1000"))
                {
                    strSqlString.Append("SELECT WORK_DATE,LOT_ID,MD_RES_ID,DC_YLD,WORK_DATE||'-'||LOT_ID||'('||MD_RES_ID||')'" + "\n");
                    strSqlString.Append("  FROM RSUMDCLHIS A, MWIPCALDEF B" + "\n");
                    strSqlString.Append(" WHERE 1=1 " + "\n");
                    if (cdvCustomer.Text != "" && cdvCustomer.Text != "ALL")
                    {
                        strSqlString.Append("   AND CUSTOMER = '" + cdvCustomer.Text + "' " + "\n");
                    }

                    strSqlString.Append("   AND LOT_ID LIKE '" + txtLotID.Text + "' " + "\n");
                    strSqlString.Append("   AND MAT_ID LIKE '" + txtSearchProduct.Text + "' " + "\n");

                    #region 상세 조회에 따른 SQL문 생성
                    if (udcWIPCondition1.Text != "ALL" && udcWIPCondition1.Text != "")
                        //strSqlString.AppendFormat("   AND CUSTOMER {0} " + "\n", udcWIPCondition1.SelectedValueToQueryString);

                    if (udcWIPCondition2.Text != "ALL" && udcWIPCondition2.Text != "")
                            strSqlString.AppendFormat("   AND FAMILY {0} " + "\n", udcWIPCondition2.SelectedValueToQueryString);

                    if (udcWIPCondition3.Text != "ALL" && udcWIPCondition3.Text != "")
                        strSqlString.AppendFormat("   AND PKG {0} " + "\n", udcWIPCondition3.SelectedValueToQueryString);

                    if (udcWIPCondition4.Text != "ALL" && udcWIPCondition4.Text != "")
                        strSqlString.AppendFormat("   AND TYPE1 {0} " + "\n", udcWIPCondition4.SelectedValueToQueryString);

                    if (udcWIPCondition5.Text != "ALL" && udcWIPCondition5.Text != "")
                        strSqlString.AppendFormat("   AND TYPE2 {0} " + "\n", udcWIPCondition5.SelectedValueToQueryString);

                    if (udcWIPCondition6.Text != "ALL" && udcWIPCondition6.Text != "")
                        strSqlString.AppendFormat("   AND LD_COUNT {0} " + "\n", udcWIPCondition6.SelectedValueToQueryString);

                    if (udcWIPCondition7.Text != "ALL" && udcWIPCondition7.Text != "")
                        strSqlString.AppendFormat("   AND DENSITY {0} " + "\n", udcWIPCondition7.SelectedValueToQueryString);

                    if (udcWIPCondition8.Text != "ALL" && udcWIPCondition8.Text != "")
                        strSqlString.AppendFormat("   AND GENERATION {0} " + "\n", udcWIPCondition8.SelectedValueToQueryString);

                    if (udcRASCondition6.Text != "ALL" && udcRASCondition6.Text != "")
                    {
                        strSqlString.AppendFormat("   AND (BG_RES_ID {0} " + "\n", udcRASCondition6.SelectedValueToQueryString);
                        strSqlString.AppendFormat("    OR SAW_RES_ID {0} " + "\n", udcRASCondition6.SelectedValueToQueryString);
                        strSqlString.AppendFormat("    OR DA1_RES_ID {0} " + "\n", udcRASCondition6.SelectedValueToQueryString);
                        strSqlString.AppendFormat("    OR DA2_RES_ID {0} " + "\n", udcRASCondition6.SelectedValueToQueryString);
                        strSqlString.AppendFormat("    OR DA3_RES_ID {0} " + "\n", udcRASCondition6.SelectedValueToQueryString);
                        strSqlString.AppendFormat("    OR WB1_RES_ID {0} " + "\n", udcRASCondition6.SelectedValueToQueryString);
                        strSqlString.AppendFormat("    OR WB2_RES_ID {0} " + "\n", udcRASCondition6.SelectedValueToQueryString);
                        strSqlString.AppendFormat("    OR WB3_RES_ID {0} " + "\n", udcRASCondition6.SelectedValueToQueryString);
                        strSqlString.AppendFormat("    OR MD_RES_ID {0} )" + "\n", udcRASCondition6.SelectedValueToQueryString);
                        strSqlString.AppendFormat("    OR MK_RES_ID {0} " + "\n", udcRASCondition6.SelectedValueToQueryString);
                        strSqlString.AppendFormat("    OR BM_RES_ID {0} " + "\n", udcRASCondition6.SelectedValueToQueryString);
                        strSqlString.AppendFormat("    OR PS_RES_ID {0} " + "\n", udcRASCondition6.SelectedValueToQueryString);
                        strSqlString.AppendFormat("    OR TR_RES_ID {0} " + "\n", udcRASCondition6.SelectedValueToQueryString);
                        strSqlString.AppendFormat("    OR FORM_RES_ID {0} " + "\n", udcRASCondition6.SelectedValueToQueryString);
                        strSqlString.AppendFormat("    OR AV_RES_ID {0} " + "\n", udcRASCondition6.SelectedValueToQueryString);
                    }

                    #endregion
                    strSqlString.Append("   AND A.WORK_DATE = B.SYS_DATE " + "\n");
                    strSqlString.Append("   AND B.CALENDAR_ID = 'HM' " + "\n");
                    strSqlString.Append("   AND WORK_DATE >='" + cdvFromToDate.FromDate.Text.Replace("-", "") + "' " + "\n");
                    strSqlString.Append("   AND WORK_DATE <='" + cdvFromToDate.ToDate.Text.Replace("-", "") + "' " + "\n");
                    strSqlString.Append("   AND DC_YLD <= " + txtYield.Text + "\n");
                    strSqlString.Append("   AND MD_START_TIME <> ' ' " + "\n");
                    strSqlString.Append(" ORDER BY WORK_DATE,MD_RES_ID" + "\n");
                  
                }
                if (cdvOper.Text.Contains("A1150"))
                {
                    strSqlString.Append("SELECT WORK_DATE,LOT_ID,MK_RES_ID,DC_YLD,WORK_DATE||'-'||LOT_ID||'('||MK_RES_ID||')'" + "\n");
                    strSqlString.Append("  FROM RSUMDCLHIS A, MWIPCALDEF B" + "\n");
                    strSqlString.Append(" WHERE 1=1 " + "\n");
                    if (cdvCustomer.Text != "" && cdvCustomer.Text != "ALL")
                    {
                        strSqlString.Append("   AND CUSTOMER = '" + cdvCustomer.Text + "' " + "\n");
                    }

                    strSqlString.Append("   AND LOT_ID LIKE '" + txtLotID.Text + "' " + "\n");
                    strSqlString.Append("   AND MAT_ID LIKE '" + txtSearchProduct.Text + "' " + "\n");

                    #region 상세 조회에 따른 SQL문 생성
                    if (udcWIPCondition1.Text != "ALL" && udcWIPCondition1.Text != "")
                        //strSqlString.AppendFormat("   AND CUSTOMER {0} " + "\n", udcWIPCondition1.SelectedValueToQueryString);

                    if (udcWIPCondition2.Text != "ALL" && udcWIPCondition2.Text != "")
                            strSqlString.AppendFormat("   AND FAMILY {0} " + "\n", udcWIPCondition2.SelectedValueToQueryString);

                    if (udcWIPCondition3.Text != "ALL" && udcWIPCondition3.Text != "")
                        strSqlString.AppendFormat("   AND PKG {0} " + "\n", udcWIPCondition3.SelectedValueToQueryString);

                    if (udcWIPCondition4.Text != "ALL" && udcWIPCondition4.Text != "")
                        strSqlString.AppendFormat("   AND TYPE1 {0} " + "\n", udcWIPCondition4.SelectedValueToQueryString);

                    if (udcWIPCondition5.Text != "ALL" && udcWIPCondition5.Text != "")
                        strSqlString.AppendFormat("   AND TYPE2 {0} " + "\n", udcWIPCondition5.SelectedValueToQueryString);

                    if (udcWIPCondition6.Text != "ALL" && udcWIPCondition6.Text != "")
                        strSqlString.AppendFormat("   AND LD_COUNT {0} " + "\n", udcWIPCondition6.SelectedValueToQueryString);

                    if (udcWIPCondition7.Text != "ALL" && udcWIPCondition7.Text != "")
                        strSqlString.AppendFormat("   AND DENSITY {0} " + "\n", udcWIPCondition7.SelectedValueToQueryString);

                    if (udcWIPCondition8.Text != "ALL" && udcWIPCondition8.Text != "")
                        strSqlString.AppendFormat("   AND GENERATION {0} " + "\n", udcWIPCondition8.SelectedValueToQueryString);

                    if (udcRASCondition6.Text != "ALL" && udcRASCondition6.Text != "")
                    {
                        strSqlString.AppendFormat("   AND (BG_RES_ID {0} " + "\n", udcRASCondition6.SelectedValueToQueryString);
                        strSqlString.AppendFormat("    OR SAW_RES_ID {0} " + "\n", udcRASCondition6.SelectedValueToQueryString);
                        strSqlString.AppendFormat("    OR DA1_RES_ID {0} " + "\n", udcRASCondition6.SelectedValueToQueryString);
                        strSqlString.AppendFormat("    OR DA2_RES_ID {0} " + "\n", udcRASCondition6.SelectedValueToQueryString);
                        strSqlString.AppendFormat("    OR DA3_RES_ID {0} " + "\n", udcRASCondition6.SelectedValueToQueryString);
                        strSqlString.AppendFormat("    OR WB1_RES_ID {0} " + "\n", udcRASCondition6.SelectedValueToQueryString);
                        strSqlString.AppendFormat("    OR WB2_RES_ID {0} " + "\n", udcRASCondition6.SelectedValueToQueryString);
                        strSqlString.AppendFormat("    OR WB3_RES_ID {0} " + "\n", udcRASCondition6.SelectedValueToQueryString);
                        strSqlString.AppendFormat("    OR MD_RES_ID {0} )" + "\n", udcRASCondition6.SelectedValueToQueryString);
                        strSqlString.AppendFormat("    OR MK_RES_ID {0} " + "\n", udcRASCondition6.SelectedValueToQueryString);
                        strSqlString.AppendFormat("    OR BM_RES_ID {0} " + "\n", udcRASCondition6.SelectedValueToQueryString);
                        strSqlString.AppendFormat("    OR PS_RES_ID {0} " + "\n", udcRASCondition6.SelectedValueToQueryString);
                        strSqlString.AppendFormat("    OR TR_RES_ID {0} " + "\n", udcRASCondition6.SelectedValueToQueryString);
                        strSqlString.AppendFormat("    OR FORM_RES_ID {0} " + "\n", udcRASCondition6.SelectedValueToQueryString);
                        strSqlString.AppendFormat("    OR AV_RES_ID {0} " + "\n", udcRASCondition6.SelectedValueToQueryString);
                    }

                    #endregion
                    strSqlString.Append("   AND A.WORK_DATE = B.SYS_DATE " + "\n");
                    strSqlString.Append("   AND B.CALENDAR_ID = 'HM' " + "\n");
                    strSqlString.Append("   AND WORK_DATE >='" + cdvFromToDate.FromDate.Text.Replace("-", "") + "' " + "\n");
                    strSqlString.Append("   AND WORK_DATE <='" + cdvFromToDate.ToDate.Text.Replace("-", "") + "' " + "\n");
                    strSqlString.Append("   AND DC_YLD <= " + txtYield.Text + "\n");
                    strSqlString.Append("   AND MK_START_TIME <> ' ' " + "\n");
                    strSqlString.Append(" ORDER BY WORK_DATE,MK_RES_ID" + "\n");
                  
                }
                if (cdvOper.Text.Contains("A1300"))
                {
                    strSqlString.Append("SELECT WORK_DATE,LOT_ID,BM_RES_ID,DC_YLD,WORK_DATE||'-'||LOT_ID||'('||BM_RES_ID||')'" + "\n");
                    strSqlString.Append("  FROM RSUMDCLHIS A, MWIPCALDEF B" + "\n");
                    strSqlString.Append(" WHERE 1=1 " + "\n");
                    if (cdvCustomer.Text != "" && cdvCustomer.Text != "ALL")
                    {
                        strSqlString.Append("   AND CUSTOMER = '" + cdvCustomer.Text + "' " + "\n");
                    }

                    strSqlString.Append("   AND LOT_ID LIKE '" + txtLotID.Text + "' " + "\n");
                    strSqlString.Append("   AND MAT_ID LIKE '" + txtSearchProduct.Text + "' " + "\n");

                    #region 상세 조회에 따른 SQL문 생성
                    if (udcWIPCondition1.Text != "ALL" && udcWIPCondition1.Text != "")
                        //strSqlString.AppendFormat("   AND CUSTOMER {0} " + "\n", udcWIPCondition1.SelectedValueToQueryString);

                    if (udcWIPCondition2.Text != "ALL" && udcWIPCondition2.Text != "")
                            strSqlString.AppendFormat("   AND FAMILY {0} " + "\n", udcWIPCondition2.SelectedValueToQueryString);

                    if (udcWIPCondition3.Text != "ALL" && udcWIPCondition3.Text != "")
                        strSqlString.AppendFormat("   AND PKG {0} " + "\n", udcWIPCondition3.SelectedValueToQueryString);

                    if (udcWIPCondition4.Text != "ALL" && udcWIPCondition4.Text != "")
                        strSqlString.AppendFormat("   AND TYPE1 {0} " + "\n", udcWIPCondition4.SelectedValueToQueryString);

                    if (udcWIPCondition5.Text != "ALL" && udcWIPCondition5.Text != "")
                        strSqlString.AppendFormat("   AND TYPE2 {0} " + "\n", udcWIPCondition5.SelectedValueToQueryString);

                    if (udcWIPCondition6.Text != "ALL" && udcWIPCondition6.Text != "")
                        strSqlString.AppendFormat("   AND LD_COUNT {0} " + "\n", udcWIPCondition6.SelectedValueToQueryString);

                    if (udcWIPCondition7.Text != "ALL" && udcWIPCondition7.Text != "")
                        strSqlString.AppendFormat("   AND DENSITY {0} " + "\n", udcWIPCondition7.SelectedValueToQueryString);

                    if (udcWIPCondition8.Text != "ALL" && udcWIPCondition8.Text != "")
                        strSqlString.AppendFormat("   AND GENERATION {0} " + "\n", udcWIPCondition8.SelectedValueToQueryString);

                    if (udcRASCondition6.Text != "ALL" && udcRASCondition6.Text != "")
                    {
                        strSqlString.AppendFormat("   AND (BG_RES_ID {0} " + "\n", udcRASCondition6.SelectedValueToQueryString);
                        strSqlString.AppendFormat("    OR SAW_RES_ID {0} " + "\n", udcRASCondition6.SelectedValueToQueryString);
                        strSqlString.AppendFormat("    OR DA1_RES_ID {0} " + "\n", udcRASCondition6.SelectedValueToQueryString);
                        strSqlString.AppendFormat("    OR DA2_RES_ID {0} " + "\n", udcRASCondition6.SelectedValueToQueryString);
                        strSqlString.AppendFormat("    OR DA3_RES_ID {0} " + "\n", udcRASCondition6.SelectedValueToQueryString);
                        strSqlString.AppendFormat("    OR WB1_RES_ID {0} " + "\n", udcRASCondition6.SelectedValueToQueryString);
                        strSqlString.AppendFormat("    OR WB2_RES_ID {0} " + "\n", udcRASCondition6.SelectedValueToQueryString);
                        strSqlString.AppendFormat("    OR WB3_RES_ID {0} " + "\n", udcRASCondition6.SelectedValueToQueryString);
                        strSqlString.AppendFormat("    OR MD_RES_ID {0} )" + "\n", udcRASCondition6.SelectedValueToQueryString);
                        strSqlString.AppendFormat("    OR MK_RES_ID {0} " + "\n", udcRASCondition6.SelectedValueToQueryString);
                        strSqlString.AppendFormat("    OR BM_RES_ID {0} " + "\n", udcRASCondition6.SelectedValueToQueryString);
                        strSqlString.AppendFormat("    OR PS_RES_ID {0} " + "\n", udcRASCondition6.SelectedValueToQueryString);
                        strSqlString.AppendFormat("    OR TR_RES_ID {0} " + "\n", udcRASCondition6.SelectedValueToQueryString);
                        strSqlString.AppendFormat("    OR FORM_RES_ID {0} " + "\n", udcRASCondition6.SelectedValueToQueryString);
                        strSqlString.AppendFormat("    OR AV_RES_ID {0} " + "\n", udcRASCondition6.SelectedValueToQueryString);
                    }

                    #endregion
                    strSqlString.Append("   AND A.WORK_DATE = B.SYS_DATE " + "\n");
                    strSqlString.Append("   AND B.CALENDAR_ID = 'HM' " + "\n");
                    strSqlString.Append("   AND WORK_DATE >='" + cdvFromToDate.FromDate.Text.Replace("-", "") + "' " + "\n");
                    strSqlString.Append("   AND WORK_DATE <='" + cdvFromToDate.ToDate.Text.Replace("-", "") + "' " + "\n");
                    strSqlString.Append("   AND DC_YLD <= " + txtYield.Text + "\n");
                    strSqlString.Append("   AND BM_START_TIME <> ' ' " + "\n");
                    strSqlString.Append(" ORDER BY WORK_DATE,BM_RES_ID" + "\n");
                  
                }
                if (cdvOper.Text.Contains("A1750"))
                {
                    strSqlString.Append("SELECT WORK_DATE,LOT_ID,PS_RES_ID,DC_YLD,WORK_DATE||'-'||LOT_ID||'('||PS_RES_ID||')'" + "\n");
                    strSqlString.Append("  FROM RSUMDCLHIS A, MWIPCALDEF B" + "\n");
                    strSqlString.Append(" WHERE 1=1 " + "\n");
                    if (cdvCustomer.Text != "" && cdvCustomer.Text != "ALL")
                    {
                        strSqlString.Append("   AND CUSTOMER = '" + cdvCustomer.Text + "' " + "\n");
                    }

                    strSqlString.Append("   AND LOT_ID LIKE '" + txtLotID.Text + "' " + "\n");
                    strSqlString.Append("   AND MAT_ID LIKE '" + txtSearchProduct.Text + "' " + "\n");

                    #region 상세 조회에 따른 SQL문 생성
                    if (udcWIPCondition1.Text != "ALL" && udcWIPCondition1.Text != "")
                        //strSqlString.AppendFormat("   AND CUSTOMER {0} " + "\n", udcWIPCondition1.SelectedValueToQueryString);

                    if (udcWIPCondition2.Text != "ALL" && udcWIPCondition2.Text != "")
                            strSqlString.AppendFormat("   AND FAMILY {0} " + "\n", udcWIPCondition2.SelectedValueToQueryString);

                    if (udcWIPCondition3.Text != "ALL" && udcWIPCondition3.Text != "")
                        strSqlString.AppendFormat("   AND PKG {0} " + "\n", udcWIPCondition3.SelectedValueToQueryString);

                    if (udcWIPCondition4.Text != "ALL" && udcWIPCondition4.Text != "")
                        strSqlString.AppendFormat("   AND TYPE1 {0} " + "\n", udcWIPCondition4.SelectedValueToQueryString);

                    if (udcWIPCondition5.Text != "ALL" && udcWIPCondition5.Text != "")
                        strSqlString.AppendFormat("   AND TYPE2 {0} " + "\n", udcWIPCondition5.SelectedValueToQueryString);

                    if (udcWIPCondition6.Text != "ALL" && udcWIPCondition6.Text != "")
                        strSqlString.AppendFormat("   AND LD_COUNT {0} " + "\n", udcWIPCondition6.SelectedValueToQueryString);

                    if (udcWIPCondition7.Text != "ALL" && udcWIPCondition7.Text != "")
                        strSqlString.AppendFormat("   AND DENSITY {0} " + "\n", udcWIPCondition7.SelectedValueToQueryString);

                    if (udcWIPCondition8.Text != "ALL" && udcWIPCondition8.Text != "")
                        strSqlString.AppendFormat("   AND GENERATION {0} " + "\n", udcWIPCondition8.SelectedValueToQueryString);

                    if (udcRASCondition6.Text != "ALL" && udcRASCondition6.Text != "")
                    {
                        strSqlString.AppendFormat("   AND (BG_RES_ID {0} " + "\n", udcRASCondition6.SelectedValueToQueryString);
                        strSqlString.AppendFormat("    OR SAW_RES_ID {0} " + "\n", udcRASCondition6.SelectedValueToQueryString);
                        strSqlString.AppendFormat("    OR DA1_RES_ID {0} " + "\n", udcRASCondition6.SelectedValueToQueryString);
                        strSqlString.AppendFormat("    OR DA2_RES_ID {0} " + "\n", udcRASCondition6.SelectedValueToQueryString);
                        strSqlString.AppendFormat("    OR DA3_RES_ID {0} " + "\n", udcRASCondition6.SelectedValueToQueryString);
                        strSqlString.AppendFormat("    OR WB1_RES_ID {0} " + "\n", udcRASCondition6.SelectedValueToQueryString);
                        strSqlString.AppendFormat("    OR WB2_RES_ID {0} " + "\n", udcRASCondition6.SelectedValueToQueryString);
                        strSqlString.AppendFormat("    OR WB3_RES_ID {0} " + "\n", udcRASCondition6.SelectedValueToQueryString);
                        strSqlString.AppendFormat("    OR MD_RES_ID {0} )" + "\n", udcRASCondition6.SelectedValueToQueryString);
                        strSqlString.AppendFormat("    OR MK_RES_ID {0} " + "\n", udcRASCondition6.SelectedValueToQueryString);
                        strSqlString.AppendFormat("    OR BM_RES_ID {0} " + "\n", udcRASCondition6.SelectedValueToQueryString);
                        strSqlString.AppendFormat("    OR PS_RES_ID {0} " + "\n", udcRASCondition6.SelectedValueToQueryString);
                        strSqlString.AppendFormat("    OR TR_RES_ID {0} " + "\n", udcRASCondition6.SelectedValueToQueryString);
                        strSqlString.AppendFormat("    OR FORM_RES_ID {0} " + "\n", udcRASCondition6.SelectedValueToQueryString);
                        strSqlString.AppendFormat("    OR AV_RES_ID {0} " + "\n", udcRASCondition6.SelectedValueToQueryString);
                    }

                    #endregion
                    strSqlString.Append("   AND A.WORK_DATE = B.SYS_DATE " + "\n");
                    strSqlString.Append("   AND B.CALENDAR_ID = 'HM' " + "\n");
                    strSqlString.Append("   AND WORK_DATE >='" + cdvFromToDate.FromDate.Text.Replace("-", "") + "' " + "\n");
                    strSqlString.Append("   AND WORK_DATE <='" + cdvFromToDate.ToDate.Text.Replace("-", "") + "' " + "\n");
                    strSqlString.Append("   AND DC_YLD <= " + txtYield.Text + "\n");
                    strSqlString.Append("   AND PS_START_TIME <> ' ' " + "\n");
                    strSqlString.Append(" ORDER BY WORK_DATE,PS_RES_ID" + "\n");
                  
                }
                if (cdvOper.Text.Contains("A1200"))
                {
                    strSqlString.Append("SELECT WORK_DATE,LOT_ID,TR_RES_ID,DC_YLD,WORK_DATE||'-'||LOT_ID||'('||TR_RES_ID||')'" + "\n");
                    strSqlString.Append("  FROM RSUMDCLHIS A, MWIPCALDEF B" + "\n");
                    strSqlString.Append(" WHERE 1=1 " + "\n");
                    if (cdvCustomer.Text != "" && cdvCustomer.Text != "ALL")
                    {
                        strSqlString.Append("   AND CUSTOMER = '" + cdvCustomer.Text + "' " + "\n");
                    }

                    strSqlString.Append("   AND LOT_ID LIKE '" + txtLotID.Text + "' " + "\n");
                    strSqlString.Append("   AND MAT_ID LIKE '" + txtSearchProduct.Text + "' " + "\n");

                    #region 상세 조회에 따른 SQL문 생성
                    if (udcWIPCondition1.Text != "ALL" && udcWIPCondition1.Text != "")
                        //strSqlString.AppendFormat("   AND CUSTOMER {0} " + "\n", udcWIPCondition1.SelectedValueToQueryString);

                    if (udcWIPCondition2.Text != "ALL" && udcWIPCondition2.Text != "")
                            strSqlString.AppendFormat("   AND FAMILY {0} " + "\n", udcWIPCondition2.SelectedValueToQueryString);

                    if (udcWIPCondition3.Text != "ALL" && udcWIPCondition3.Text != "")
                        strSqlString.AppendFormat("   AND PKG {0} " + "\n", udcWIPCondition3.SelectedValueToQueryString);

                    if (udcWIPCondition4.Text != "ALL" && udcWIPCondition4.Text != "")
                        strSqlString.AppendFormat("   AND TYPE1 {0} " + "\n", udcWIPCondition4.SelectedValueToQueryString);

                    if (udcWIPCondition5.Text != "ALL" && udcWIPCondition5.Text != "")
                        strSqlString.AppendFormat("   AND TYPE2 {0} " + "\n", udcWIPCondition5.SelectedValueToQueryString);

                    if (udcWIPCondition6.Text != "ALL" && udcWIPCondition6.Text != "")
                        strSqlString.AppendFormat("   AND LD_COUNT {0} " + "\n", udcWIPCondition6.SelectedValueToQueryString);

                    if (udcWIPCondition7.Text != "ALL" && udcWIPCondition7.Text != "")
                        strSqlString.AppendFormat("   AND DENSITY {0} " + "\n", udcWIPCondition7.SelectedValueToQueryString);

                    if (udcWIPCondition8.Text != "ALL" && udcWIPCondition8.Text != "")
                        strSqlString.AppendFormat("   AND GENERATION {0} " + "\n", udcWIPCondition8.SelectedValueToQueryString);

                    if (udcRASCondition6.Text != "ALL" && udcRASCondition6.Text != "")
                    {
                        strSqlString.AppendFormat("   AND (BG_RES_ID {0} " + "\n", udcRASCondition6.SelectedValueToQueryString);
                        strSqlString.AppendFormat("    OR SAW_RES_ID {0} " + "\n", udcRASCondition6.SelectedValueToQueryString);
                        strSqlString.AppendFormat("    OR DA1_RES_ID {0} " + "\n", udcRASCondition6.SelectedValueToQueryString);
                        strSqlString.AppendFormat("    OR DA2_RES_ID {0} " + "\n", udcRASCondition6.SelectedValueToQueryString);
                        strSqlString.AppendFormat("    OR DA3_RES_ID {0} " + "\n", udcRASCondition6.SelectedValueToQueryString);
                        strSqlString.AppendFormat("    OR WB1_RES_ID {0} " + "\n", udcRASCondition6.SelectedValueToQueryString);
                        strSqlString.AppendFormat("    OR WB2_RES_ID {0} " + "\n", udcRASCondition6.SelectedValueToQueryString);
                        strSqlString.AppendFormat("    OR WB3_RES_ID {0} " + "\n", udcRASCondition6.SelectedValueToQueryString);
                        strSqlString.AppendFormat("    OR MD_RES_ID {0} )" + "\n", udcRASCondition6.SelectedValueToQueryString);
                        strSqlString.AppendFormat("    OR MK_RES_ID {0} " + "\n", udcRASCondition6.SelectedValueToQueryString);
                        strSqlString.AppendFormat("    OR BM_RES_ID {0} " + "\n", udcRASCondition6.SelectedValueToQueryString);
                        strSqlString.AppendFormat("    OR PS_RES_ID {0} " + "\n", udcRASCondition6.SelectedValueToQueryString);
                        strSqlString.AppendFormat("    OR TR_RES_ID {0} " + "\n", udcRASCondition6.SelectedValueToQueryString);
                        strSqlString.AppendFormat("    OR FORM_RES_ID {0} " + "\n", udcRASCondition6.SelectedValueToQueryString);
                        strSqlString.AppendFormat("    OR AV_RES_ID {0} " + "\n", udcRASCondition6.SelectedValueToQueryString);
                    }

                    #endregion
                    strSqlString.Append("   AND A.WORK_DATE = B.SYS_DATE " + "\n");
                    strSqlString.Append("   AND B.CALENDAR_ID = 'HM' " + "\n");
                    strSqlString.Append("   AND WORK_DATE >='" + cdvFromToDate.FromDate.Text.Replace("-", "") + "' " + "\n");
                    strSqlString.Append("   AND WORK_DATE <='" + cdvFromToDate.ToDate.Text.Replace("-", "") + "' " + "\n");
                    strSqlString.Append("   AND DC_YLD <= " + txtYield.Text + "\n");
                    strSqlString.Append("   AND TR_START_TIME <> ' ' " + "\n");
                    strSqlString.Append(" ORDER BY WORK_DATE,TR_RES_ID" + "\n");
                  
                }
                if (cdvOper.Text.Contains("A1800"))
                {
                    strSqlString.Append("SELECT WORK_DATE,LOT_ID,FORM_RES_ID,DC_YLD,WORK_DATE||'-'||LOT_ID||'('||FORM_RES_ID||')'" + "\n");
                    strSqlString.Append("  FROM RSUMDCLHIS A, MWIPCALDEF B" + "\n");
                    strSqlString.Append(" WHERE 1=1 " + "\n");
                    if (cdvCustomer.Text != "" && cdvCustomer.Text != "ALL")
                    {
                        strSqlString.Append("   AND CUSTOMER = '" + cdvCustomer.Text + "' " + "\n");
                    }

                    strSqlString.Append("   AND LOT_ID LIKE '" + txtLotID.Text + "' " + "\n");
                    strSqlString.Append("   AND MAT_ID LIKE '" + txtSearchProduct.Text + "' " + "\n");

                    #region 상세 조회에 따른 SQL문 생성
                    if (udcWIPCondition1.Text != "ALL" && udcWIPCondition1.Text != "")
                        //strSqlString.AppendFormat("   AND CUSTOMER {0} " + "\n", udcWIPCondition1.SelectedValueToQueryString);

                    if (udcWIPCondition2.Text != "ALL" && udcWIPCondition2.Text != "")
                            strSqlString.AppendFormat("   AND FAMILY {0} " + "\n", udcWIPCondition2.SelectedValueToQueryString);

                    if (udcWIPCondition3.Text != "ALL" && udcWIPCondition3.Text != "")
                        strSqlString.AppendFormat("   AND PKG {0} " + "\n", udcWIPCondition3.SelectedValueToQueryString);

                    if (udcWIPCondition4.Text != "ALL" && udcWIPCondition4.Text != "")
                        strSqlString.AppendFormat("   AND TYPE1 {0} " + "\n", udcWIPCondition4.SelectedValueToQueryString);

                    if (udcWIPCondition5.Text != "ALL" && udcWIPCondition5.Text != "")
                        strSqlString.AppendFormat("   AND TYPE2 {0} " + "\n", udcWIPCondition5.SelectedValueToQueryString);

                    if (udcWIPCondition6.Text != "ALL" && udcWIPCondition6.Text != "")
                        strSqlString.AppendFormat("   AND LD_COUNT {0} " + "\n", udcWIPCondition6.SelectedValueToQueryString);

                    if (udcWIPCondition7.Text != "ALL" && udcWIPCondition7.Text != "")
                        strSqlString.AppendFormat("   AND DENSITY {0} " + "\n", udcWIPCondition7.SelectedValueToQueryString);

                    if (udcWIPCondition8.Text != "ALL" && udcWIPCondition8.Text != "")
                        strSqlString.AppendFormat("   AND GENERATION {0} " + "\n", udcWIPCondition8.SelectedValueToQueryString);

                    if (udcRASCondition6.Text != "ALL" && udcRASCondition6.Text != "")
                    {
                        strSqlString.AppendFormat("   AND (BG_RES_ID {0} " + "\n", udcRASCondition6.SelectedValueToQueryString);
                        strSqlString.AppendFormat("    OR SAW_RES_ID {0} " + "\n", udcRASCondition6.SelectedValueToQueryString);
                        strSqlString.AppendFormat("    OR DA1_RES_ID {0} " + "\n", udcRASCondition6.SelectedValueToQueryString);
                        strSqlString.AppendFormat("    OR DA2_RES_ID {0} " + "\n", udcRASCondition6.SelectedValueToQueryString);
                        strSqlString.AppendFormat("    OR DA3_RES_ID {0} " + "\n", udcRASCondition6.SelectedValueToQueryString);
                        strSqlString.AppendFormat("    OR WB1_RES_ID {0} " + "\n", udcRASCondition6.SelectedValueToQueryString);
                        strSqlString.AppendFormat("    OR WB2_RES_ID {0} " + "\n", udcRASCondition6.SelectedValueToQueryString);
                        strSqlString.AppendFormat("    OR WB3_RES_ID {0} " + "\n", udcRASCondition6.SelectedValueToQueryString);
                        strSqlString.AppendFormat("    OR MD_RES_ID {0} )" + "\n", udcRASCondition6.SelectedValueToQueryString);
                        strSqlString.AppendFormat("    OR MK_RES_ID {0} " + "\n", udcRASCondition6.SelectedValueToQueryString);
                        strSqlString.AppendFormat("    OR BM_RES_ID {0} " + "\n", udcRASCondition6.SelectedValueToQueryString);
                        strSqlString.AppendFormat("    OR PS_RES_ID {0} " + "\n", udcRASCondition6.SelectedValueToQueryString);
                        strSqlString.AppendFormat("    OR TR_RES_ID {0} " + "\n", udcRASCondition6.SelectedValueToQueryString);
                        strSqlString.AppendFormat("    OR FORM_RES_ID {0} " + "\n", udcRASCondition6.SelectedValueToQueryString);
                        strSqlString.AppendFormat("    OR AV_RES_ID {0} " + "\n", udcRASCondition6.SelectedValueToQueryString);
                    }

                    #endregion
                    strSqlString.Append("   AND A.WORK_DATE = B.SYS_DATE " + "\n");
                    strSqlString.Append("   AND B.CALENDAR_ID = 'HM' " + "\n");
                    strSqlString.Append("   AND WORK_DATE >='" + cdvFromToDate.FromDate.Text.Replace("-", "") + "' " + "\n");
                    strSqlString.Append("   AND WORK_DATE <='" + cdvFromToDate.ToDate.Text.Replace("-", "") + "' " + "\n");
                    strSqlString.Append("   AND DC_YLD <= " + txtYield.Text + "\n");
                    strSqlString.Append("   AND FORM_START_TIME <> ' ' " + "\n");
                    strSqlString.Append(" ORDER BY WORK_DATE,FORM_RES_ID" + "\n");
                  
                }
                if (cdvOper.Text.Contains("A2000"))
                {
                    strSqlString.Append("SELECT WORK_DATE,LOT_ID,AV_RES_ID,DC_YLD,WORK_DATE||'-'||LOT_ID||'('||AV_RES_ID||')'" + "\n");
                    strSqlString.Append("  FROM RSUMDCLHIS A, MWIPCALDEF B" + "\n");
                    strSqlString.Append(" WHERE 1=1 " + "\n");
                    if (cdvCustomer.Text != "" && cdvCustomer.Text != "ALL")
                    {
                        strSqlString.Append("   AND CUSTOMER = '" + cdvCustomer.Text + "' " + "\n");
                    }

                    strSqlString.Append("   AND LOT_ID LIKE '" + txtLotID.Text + "' " + "\n");
                    strSqlString.Append("   AND MAT_ID LIKE '" + txtSearchProduct.Text + "' " + "\n");

                    #region 상세 조회에 따른 SQL문 생성
                    if (udcWIPCondition1.Text != "ALL" && udcWIPCondition1.Text != "")
                        //strSqlString.AppendFormat("   AND CUSTOMER {0} " + "\n", udcWIPCondition1.SelectedValueToQueryString);

                    if (udcWIPCondition2.Text != "ALL" && udcWIPCondition2.Text != "")
                            strSqlString.AppendFormat("   AND FAMILY {0} " + "\n", udcWIPCondition2.SelectedValueToQueryString);

                    if (udcWIPCondition3.Text != "ALL" && udcWIPCondition3.Text != "")
                        strSqlString.AppendFormat("   AND PKG {0} " + "\n", udcWIPCondition3.SelectedValueToQueryString);

                    if (udcWIPCondition4.Text != "ALL" && udcWIPCondition4.Text != "")
                        strSqlString.AppendFormat("   AND TYPE1 {0} " + "\n", udcWIPCondition4.SelectedValueToQueryString);

                    if (udcWIPCondition5.Text != "ALL" && udcWIPCondition5.Text != "")
                        strSqlString.AppendFormat("   AND TYPE2 {0} " + "\n", udcWIPCondition5.SelectedValueToQueryString);

                    if (udcWIPCondition6.Text != "ALL" && udcWIPCondition6.Text != "")
                        strSqlString.AppendFormat("   AND LD_COUNT {0} " + "\n", udcWIPCondition6.SelectedValueToQueryString);

                    if (udcWIPCondition7.Text != "ALL" && udcWIPCondition7.Text != "")
                        strSqlString.AppendFormat("   AND DENSITY {0} " + "\n", udcWIPCondition7.SelectedValueToQueryString);

                    if (udcWIPCondition8.Text != "ALL" && udcWIPCondition8.Text != "")
                        strSqlString.AppendFormat("   AND GENERATION {0} " + "\n", udcWIPCondition8.SelectedValueToQueryString);

                    if (udcRASCondition6.Text != "ALL" && udcRASCondition6.Text != "")
                    {
                        strSqlString.AppendFormat("   AND (BG_RES_ID {0} " + "\n", udcRASCondition6.SelectedValueToQueryString);
                        strSqlString.AppendFormat("    OR SAW_RES_ID {0} " + "\n", udcRASCondition6.SelectedValueToQueryString);
                        strSqlString.AppendFormat("    OR DA1_RES_ID {0} " + "\n", udcRASCondition6.SelectedValueToQueryString);
                        strSqlString.AppendFormat("    OR DA2_RES_ID {0} " + "\n", udcRASCondition6.SelectedValueToQueryString);
                        strSqlString.AppendFormat("    OR DA3_RES_ID {0} " + "\n", udcRASCondition6.SelectedValueToQueryString);
                        strSqlString.AppendFormat("    OR WB1_RES_ID {0} " + "\n", udcRASCondition6.SelectedValueToQueryString);
                        strSqlString.AppendFormat("    OR WB2_RES_ID {0} " + "\n", udcRASCondition6.SelectedValueToQueryString);
                        strSqlString.AppendFormat("    OR WB3_RES_ID {0} " + "\n", udcRASCondition6.SelectedValueToQueryString);
                        strSqlString.AppendFormat("    OR MD_RES_ID {0} )" + "\n", udcRASCondition6.SelectedValueToQueryString);
                        strSqlString.AppendFormat("    OR MK_RES_ID {0} " + "\n", udcRASCondition6.SelectedValueToQueryString);
                        strSqlString.AppendFormat("    OR BM_RES_ID {0} " + "\n", udcRASCondition6.SelectedValueToQueryString);
                        strSqlString.AppendFormat("    OR PS_RES_ID {0} " + "\n", udcRASCondition6.SelectedValueToQueryString);
                        strSqlString.AppendFormat("    OR TR_RES_ID {0} " + "\n", udcRASCondition6.SelectedValueToQueryString);
                        strSqlString.AppendFormat("    OR FORM_RES_ID {0} " + "\n", udcRASCondition6.SelectedValueToQueryString);
                        strSqlString.AppendFormat("    OR AV_RES_ID {0} " + "\n", udcRASCondition6.SelectedValueToQueryString);
                    }

                    #endregion
                    strSqlString.Append("   AND A.WORK_DATE = B.SYS_DATE " + "\n");
                    strSqlString.Append("   AND B.CALENDAR_ID = 'HM' " + "\n");
                    strSqlString.Append("   AND WORK_DATE >='" + cdvFromToDate.FromDate.Text.Replace("-", "") + "' " + "\n");
                    strSqlString.Append("   AND WORK_DATE <='" + cdvFromToDate.ToDate.Text.Replace("-", "") + "' " + "\n");
                    strSqlString.Append("   AND DC_YLD <= " + txtYield.Text + "\n");
                    strSqlString.Append("   AND AV_START_TIME <> ' ' " + "\n");
                    strSqlString.Append(" ORDER BY WORK_DATE,AV_RES_ID" + "\n");
                }
            }

            if (GlobalVariable.gsUserGroup == "ADMIN_GROUP" || GlobalVariable.gsUserGroup == "HANA_ADMIN_GROUP")
            {
                System.Windows.Forms.Clipboard.SetText(strSqlString.ToString());
            }

            return strSqlString.ToString();
        }

        
        /// 해당 조건에 맞는 설비명
        private string MakeCount()
        {
            StringBuilder strSqlString = new StringBuilder();

            if (cdvOper.Text != "ALL" && cdvOper.Text != "")
            {
                if (cdvOper.Text.Contains("A0040"))
                {
                    strSqlString.Append("SELECT ROWNUM-1, BG_RES_ID FROM(" + "\n");
                    strSqlString.Append("SELECT DISTINCT BG_RES_ID" + "\n");
                    strSqlString.Append("  FROM RSUMDCLHIS A, MWIPCALDEF B" + "\n");
                    strSqlString.Append(" WHERE 1=1 " + "\n");
                    if (cdvCustomer.Text != "" && cdvCustomer.Text != "ALL")
                    {
                        strSqlString.Append("   AND CUSTOMER = '" + cdvCustomer.Text + "' " + "\n");
                    }
                    strSqlString.Append("   AND LOT_ID LIKE '" + txtLotID.Text + "' " + "\n");
                    strSqlString.Append("   AND MAT_ID LIKE '" + txtSearchProduct.Text + "' " + "\n");

                    #region 상세 조회에 따른 SQL문 생성
                    if (udcWIPCondition1.Text != "ALL" && udcWIPCondition1.Text != "")
                        //strSqlString.AppendFormat("   AND CUSTOMER {0} " + "\n", udcWIPCondition1.SelectedValueToQueryString);

                    if (udcWIPCondition2.Text != "ALL" && udcWIPCondition2.Text != "")
                            strSqlString.AppendFormat("   AND FAMILY {0} " + "\n", udcWIPCondition2.SelectedValueToQueryString);

                    if (udcWIPCondition3.Text != "ALL" && udcWIPCondition3.Text != "")
                        strSqlString.AppendFormat("   AND PKG {0} " + "\n", udcWIPCondition3.SelectedValueToQueryString);

                    if (udcWIPCondition4.Text != "ALL" && udcWIPCondition4.Text != "")
                        strSqlString.AppendFormat("   AND TYPE1 {0} " + "\n", udcWIPCondition4.SelectedValueToQueryString);

                    if (udcWIPCondition5.Text != "ALL" && udcWIPCondition5.Text != "")
                        strSqlString.AppendFormat("   AND TYPE2 {0} " + "\n", udcWIPCondition5.SelectedValueToQueryString);

                    if (udcWIPCondition6.Text != "ALL" && udcWIPCondition6.Text != "")
                        strSqlString.AppendFormat("   AND LD_COUNT {0} " + "\n", udcWIPCondition6.SelectedValueToQueryString);

                    if (udcWIPCondition7.Text != "ALL" && udcWIPCondition7.Text != "")
                        strSqlString.AppendFormat("   AND DENSITY {0} " + "\n", udcWIPCondition7.SelectedValueToQueryString);

                    if (udcWIPCondition8.Text != "ALL" && udcWIPCondition8.Text != "")
                        strSqlString.AppendFormat("   AND GENERATION {0} " + "\n", udcWIPCondition8.SelectedValueToQueryString);

                    if (udcRASCondition6.Text != "ALL" && udcRASCondition6.Text != "")
                    {
                        strSqlString.AppendFormat("   AND (BG_RES_ID {0} " + "\n", udcRASCondition6.SelectedValueToQueryString);
                        strSqlString.AppendFormat("    OR SAW_RES_ID {0} " + "\n", udcRASCondition6.SelectedValueToQueryString);
                        strSqlString.AppendFormat("    OR DA1_RES_ID {0} " + "\n", udcRASCondition6.SelectedValueToQueryString);
                        strSqlString.AppendFormat("    OR DA2_RES_ID {0} " + "\n", udcRASCondition6.SelectedValueToQueryString);
                        strSqlString.AppendFormat("    OR DA3_RES_ID {0} " + "\n", udcRASCondition6.SelectedValueToQueryString);
                        strSqlString.AppendFormat("    OR WB1_RES_ID {0} " + "\n", udcRASCondition6.SelectedValueToQueryString);
                        strSqlString.AppendFormat("    OR WB2_RES_ID {0} " + "\n", udcRASCondition6.SelectedValueToQueryString);
                        strSqlString.AppendFormat("    OR WB3_RES_ID {0} " + "\n", udcRASCondition6.SelectedValueToQueryString);
                        strSqlString.AppendFormat("    OR MD_RES_ID {0} )" + "\n", udcRASCondition6.SelectedValueToQueryString);
                        strSqlString.AppendFormat("    OR MK_RES_ID {0} " + "\n", udcRASCondition6.SelectedValueToQueryString);
                        strSqlString.AppendFormat("    OR BM_RES_ID {0} " + "\n", udcRASCondition6.SelectedValueToQueryString);
                        strSqlString.AppendFormat("    OR PS_RES_ID {0} " + "\n", udcRASCondition6.SelectedValueToQueryString);
                        strSqlString.AppendFormat("    OR TR_RES_ID {0} " + "\n", udcRASCondition6.SelectedValueToQueryString);
                        strSqlString.AppendFormat("    OR FORM_RES_ID {0} " + "\n", udcRASCondition6.SelectedValueToQueryString);
                        strSqlString.AppendFormat("    OR AV_RES_ID {0} " + "\n", udcRASCondition6.SelectedValueToQueryString);
                    }

                    #endregion
                    strSqlString.Append("   AND A.WORK_DATE = B.SYS_DATE " + "\n");
                    strSqlString.Append("   AND B.CALENDAR_ID = 'HM' " + "\n");
                    strSqlString.Append("   AND WORK_DATE >='" + cdvFromToDate.FromDate.Text.Replace("-", "") + "' " + "\n");
                    strSqlString.Append("   AND WORK_DATE <='" + cdvFromToDate.ToDate.Text.Replace("-", "") + "' " + "\n");
                    strSqlString.Append("   AND DC_YLD <= " + txtYield.Text + "\n");
                    strSqlString.Append("   AND BG_START_TIME <> ' '" + "\n");
                    strSqlString.Append(" ORDER BY BG_RES_ID)" + "\n");
                }
                if (cdvOper.Text.Contains("A0200"))
                {
                    strSqlString.Append("SELECT ROWNUM-1, SAW_RES_ID FROM(" + "\n");
                    strSqlString.Append("SELECT DISTINCT SAW_RES_ID" + "\n");
                    strSqlString.Append("  FROM RSUMDCLHIS A, MWIPCALDEF B" + "\n");
                    strSqlString.Append(" WHERE 1=1 " + "\n");
                    if (cdvCustomer.Text != "" && cdvCustomer.Text != "ALL")
                    {
                        strSqlString.Append("   AND CUSTOMER = '" + cdvCustomer.Text + "' " + "\n");
                    }

                    strSqlString.Append("   AND LOT_ID LIKE '" + txtLotID.Text + "' " + "\n");
                    strSqlString.Append("   AND MAT_ID LIKE '" + txtSearchProduct.Text + "' " + "\n");

                    #region 상세 조회에 따른 SQL문 생성
                    if (udcWIPCondition1.Text != "ALL" && udcWIPCondition1.Text != "")
                        //strSqlString.AppendFormat("   AND CUSTOMER {0} " + "\n", udcWIPCondition1.SelectedValueToQueryString);

                    if (udcWIPCondition2.Text != "ALL" && udcWIPCondition2.Text != "")
                            strSqlString.AppendFormat("   AND FAMILY {0} " + "\n", udcWIPCondition2.SelectedValueToQueryString);

                    if (udcWIPCondition3.Text != "ALL" && udcWIPCondition3.Text != "")
                        strSqlString.AppendFormat("   AND PKG {0} " + "\n", udcWIPCondition3.SelectedValueToQueryString);

                    if (udcWIPCondition4.Text != "ALL" && udcWIPCondition4.Text != "")
                        strSqlString.AppendFormat("   AND TYPE1 {0} " + "\n", udcWIPCondition4.SelectedValueToQueryString);

                    if (udcWIPCondition5.Text != "ALL" && udcWIPCondition5.Text != "")
                        strSqlString.AppendFormat("   AND TYPE2 {0} " + "\n", udcWIPCondition5.SelectedValueToQueryString);

                    if (udcWIPCondition6.Text != "ALL" && udcWIPCondition6.Text != "")
                        strSqlString.AppendFormat("   AND LD_COUNT {0} " + "\n", udcWIPCondition6.SelectedValueToQueryString);

                    if (udcWIPCondition7.Text != "ALL" && udcWIPCondition7.Text != "")
                        strSqlString.AppendFormat("   AND DENSITY {0} " + "\n", udcWIPCondition7.SelectedValueToQueryString);

                    if (udcWIPCondition8.Text != "ALL" && udcWIPCondition8.Text != "")
                        strSqlString.AppendFormat("   AND GENERATION {0} " + "\n", udcWIPCondition8.SelectedValueToQueryString);

                    if (udcRASCondition6.Text != "ALL" && udcRASCondition6.Text != "")
                    {
                        strSqlString.AppendFormat("   AND (BG_RES_ID {0} " + "\n", udcRASCondition6.SelectedValueToQueryString);
                        strSqlString.AppendFormat("    OR SAW_RES_ID {0} " + "\n", udcRASCondition6.SelectedValueToQueryString);
                        strSqlString.AppendFormat("    OR DA1_RES_ID {0} " + "\n", udcRASCondition6.SelectedValueToQueryString);
                        strSqlString.AppendFormat("    OR DA2_RES_ID {0} " + "\n", udcRASCondition6.SelectedValueToQueryString);
                        strSqlString.AppendFormat("    OR DA3_RES_ID {0} " + "\n", udcRASCondition6.SelectedValueToQueryString);
                        strSqlString.AppendFormat("    OR WB1_RES_ID {0} " + "\n", udcRASCondition6.SelectedValueToQueryString);
                        strSqlString.AppendFormat("    OR WB2_RES_ID {0} " + "\n", udcRASCondition6.SelectedValueToQueryString);
                        strSqlString.AppendFormat("    OR WB3_RES_ID {0} " + "\n", udcRASCondition6.SelectedValueToQueryString);
                        strSqlString.AppendFormat("    OR MD_RES_ID {0} )" + "\n", udcRASCondition6.SelectedValueToQueryString);
                        strSqlString.AppendFormat("    OR MK_RES_ID {0} " + "\n", udcRASCondition6.SelectedValueToQueryString);
                        strSqlString.AppendFormat("    OR BM_RES_ID {0} " + "\n", udcRASCondition6.SelectedValueToQueryString);
                        strSqlString.AppendFormat("    OR PS_RES_ID {0} " + "\n", udcRASCondition6.SelectedValueToQueryString);
                        strSqlString.AppendFormat("    OR TR_RES_ID {0} " + "\n", udcRASCondition6.SelectedValueToQueryString);
                        strSqlString.AppendFormat("    OR FORM_RES_ID {0} " + "\n", udcRASCondition6.SelectedValueToQueryString);
                        strSqlString.AppendFormat("    OR AV_RES_ID {0} " + "\n", udcRASCondition6.SelectedValueToQueryString);
                    }

                    #endregion
                    strSqlString.Append("   AND A.WORK_DATE = B.SYS_DATE " + "\n");
                    strSqlString.Append("   AND B.CALENDAR_ID = 'HM' " + "\n");
                    strSqlString.Append("   AND WORK_DATE >='" + cdvFromToDate.FromDate.Text.Replace("-", "") + "' " + "\n");
                    strSqlString.Append("   AND WORK_DATE <='" + cdvFromToDate.ToDate.Text.Replace("-", "") + "' " + "\n");
                    strSqlString.Append("   AND DC_YLD <= " + txtYield.Text + "\n");
                    strSqlString.Append("   AND SAW_START_TIME <> ' '" + "\n");
                    strSqlString.Append(" ORDER BY SAW_RES_ID)" + "\n");
                }
                if (cdvOper.Text.Contains("A0400"))
                {
                    strSqlString.Append("SELECT ROWNUM-1, DA1_RES_ID FROM(" + "\n");
                    strSqlString.Append("SELECT DISTINCT DA1_RES_ID" + "\n");
                    strSqlString.Append("  FROM RSUMDCLHIS A, MWIPCALDEF B" + "\n");
                    strSqlString.Append(" WHERE 1=1 " + "\n");
                    if (cdvCustomer.Text != "" && cdvCustomer.Text != "ALL")
                    {
                        strSqlString.Append("   AND CUSTOMER = '" + cdvCustomer.Text + "' " + "\n");
                    }

                    strSqlString.Append("   AND LOT_ID LIKE '" + txtLotID.Text + "' " + "\n");
                    strSqlString.Append("   AND MAT_ID LIKE '" + txtSearchProduct.Text + "' " + "\n");

                    #region 상세 조회에 따른 SQL문 생성
                    if (udcWIPCondition1.Text != "ALL" && udcWIPCondition1.Text != "")
                        //strSqlString.AppendFormat("   AND CUSTOMER {0} " + "\n", udcWIPCondition1.SelectedValueToQueryString);

                    if (udcWIPCondition2.Text != "ALL" && udcWIPCondition2.Text != "")
                            strSqlString.AppendFormat("   AND FAMILY {0} " + "\n", udcWIPCondition2.SelectedValueToQueryString);

                    if (udcWIPCondition3.Text != "ALL" && udcWIPCondition3.Text != "")
                        strSqlString.AppendFormat("   AND PKG {0} " + "\n", udcWIPCondition3.SelectedValueToQueryString);

                    if (udcWIPCondition4.Text != "ALL" && udcWIPCondition4.Text != "")
                        strSqlString.AppendFormat("   AND TYPE1 {0} " + "\n", udcWIPCondition4.SelectedValueToQueryString);

                    if (udcWIPCondition5.Text != "ALL" && udcWIPCondition5.Text != "")
                        strSqlString.AppendFormat("   AND TYPE2 {0} " + "\n", udcWIPCondition5.SelectedValueToQueryString);

                    if (udcWIPCondition6.Text != "ALL" && udcWIPCondition6.Text != "")
                        strSqlString.AppendFormat("   AND LD_COUNT {0} " + "\n", udcWIPCondition6.SelectedValueToQueryString);

                    if (udcWIPCondition7.Text != "ALL" && udcWIPCondition7.Text != "")
                        strSqlString.AppendFormat("   AND DENSITY {0} " + "\n", udcWIPCondition7.SelectedValueToQueryString);

                    if (udcWIPCondition8.Text != "ALL" && udcWIPCondition8.Text != "")
                        strSqlString.AppendFormat("   AND GENERATION {0} " + "\n", udcWIPCondition8.SelectedValueToQueryString);

                    if (udcRASCondition6.Text != "ALL" && udcRASCondition6.Text != "")
                    {
                        strSqlString.AppendFormat("   AND (BG_RES_ID {0} " + "\n", udcRASCondition6.SelectedValueToQueryString);
                        strSqlString.AppendFormat("    OR SAW_RES_ID {0} " + "\n", udcRASCondition6.SelectedValueToQueryString);
                        strSqlString.AppendFormat("    OR DA1_RES_ID {0} " + "\n", udcRASCondition6.SelectedValueToQueryString);
                        strSqlString.AppendFormat("    OR DA2_RES_ID {0} " + "\n", udcRASCondition6.SelectedValueToQueryString);
                        strSqlString.AppendFormat("    OR DA3_RES_ID {0} " + "\n", udcRASCondition6.SelectedValueToQueryString);
                        strSqlString.AppendFormat("    OR WB1_RES_ID {0} " + "\n", udcRASCondition6.SelectedValueToQueryString);
                        strSqlString.AppendFormat("    OR WB2_RES_ID {0} " + "\n", udcRASCondition6.SelectedValueToQueryString);
                        strSqlString.AppendFormat("    OR WB3_RES_ID {0} " + "\n", udcRASCondition6.SelectedValueToQueryString);
                        strSqlString.AppendFormat("    OR MD_RES_ID {0} )" + "\n", udcRASCondition6.SelectedValueToQueryString);
                        strSqlString.AppendFormat("    OR MK_RES_ID {0} " + "\n", udcRASCondition6.SelectedValueToQueryString);
                        strSqlString.AppendFormat("    OR BM_RES_ID {0} " + "\n", udcRASCondition6.SelectedValueToQueryString);
                        strSqlString.AppendFormat("    OR PS_RES_ID {0} " + "\n", udcRASCondition6.SelectedValueToQueryString);
                        strSqlString.AppendFormat("    OR TR_RES_ID {0} " + "\n", udcRASCondition6.SelectedValueToQueryString);
                        strSqlString.AppendFormat("    OR FORM_RES_ID {0} " + "\n", udcRASCondition6.SelectedValueToQueryString);
                        strSqlString.AppendFormat("    OR AV_RES_ID {0} " + "\n", udcRASCondition6.SelectedValueToQueryString);
                    }

                    #endregion
                    strSqlString.Append("   AND A.WORK_DATE = B.SYS_DATE " + "\n");
                    strSqlString.Append("   AND B.CALENDAR_ID = 'HM' " + "\n");
                    strSqlString.Append("   AND WORK_DATE >='" + cdvFromToDate.FromDate.Text.Replace("-", "") + "' " + "\n");
                    strSqlString.Append("   AND WORK_DATE <='" + cdvFromToDate.ToDate.Text.Replace("-", "") + "' " + "\n");
                    strSqlString.Append("   AND DC_YLD <= " + txtYield.Text + "\n");
                    strSqlString.Append("   AND DA1_START_TIME <> ' '" + "\n");
                    strSqlString.Append(" ORDER BY DA1_RES_ID)" + "\n");
                }
                if (cdvOper.Text.Contains("A0401"))
                {
                    strSqlString.Append("SELECT ROWNUM-1, DA2_RES_ID FROM(" + "\n");
                    strSqlString.Append("SELECT DISTINCT DA2_RES_ID" + "\n");
                    strSqlString.Append("  FROM RSUMDCLHIS A, MWIPCALDEF B" + "\n");
                    strSqlString.Append(" WHERE 1=1 " + "\n");
                    if (cdvCustomer.Text != "" && cdvCustomer.Text != "ALL")
                    {
                        strSqlString.Append("   AND CUSTOMER = '" + cdvCustomer.Text + "' " + "\n");
                    }

                    strSqlString.Append("   AND LOT_ID LIKE '" + txtLotID.Text + "' " + "\n");
                    strSqlString.Append("   AND MAT_ID LIKE '" + txtSearchProduct.Text + "' " + "\n");

                    #region 상세 조회에 따른 SQL문 생성
                    if (udcWIPCondition1.Text != "ALL" && udcWIPCondition1.Text != "")
                        //strSqlString.AppendFormat("   AND CUSTOMER {0} " + "\n", udcWIPCondition1.SelectedValueToQueryString);

                    if (udcWIPCondition2.Text != "ALL" && udcWIPCondition2.Text != "")
                            strSqlString.AppendFormat("   AND FAMILY {0} " + "\n", udcWIPCondition2.SelectedValueToQueryString);

                    if (udcWIPCondition3.Text != "ALL" && udcWIPCondition3.Text != "")
                        strSqlString.AppendFormat("   AND PKG {0} " + "\n", udcWIPCondition3.SelectedValueToQueryString);

                    if (udcWIPCondition4.Text != "ALL" && udcWIPCondition4.Text != "")
                        strSqlString.AppendFormat("   AND TYPE1 {0} " + "\n", udcWIPCondition4.SelectedValueToQueryString);

                    if (udcWIPCondition5.Text != "ALL" && udcWIPCondition5.Text != "")
                        strSqlString.AppendFormat("   AND TYPE2 {0} " + "\n", udcWIPCondition5.SelectedValueToQueryString);

                    if (udcWIPCondition6.Text != "ALL" && udcWIPCondition6.Text != "")
                        strSqlString.AppendFormat("   AND LD_COUNT {0} " + "\n", udcWIPCondition6.SelectedValueToQueryString);

                    if (udcWIPCondition7.Text != "ALL" && udcWIPCondition7.Text != "")
                        strSqlString.AppendFormat("   AND DENSITY {0} " + "\n", udcWIPCondition7.SelectedValueToQueryString);

                    if (udcWIPCondition8.Text != "ALL" && udcWIPCondition8.Text != "")
                        strSqlString.AppendFormat("   AND GENERATION {0} " + "\n", udcWIPCondition8.SelectedValueToQueryString);

                    if (udcRASCondition6.Text != "ALL" && udcRASCondition6.Text != "")
                    {
                        strSqlString.AppendFormat("   AND (BG_RES_ID {0} " + "\n", udcRASCondition6.SelectedValueToQueryString);
                        strSqlString.AppendFormat("    OR SAW_RES_ID {0} " + "\n", udcRASCondition6.SelectedValueToQueryString);
                        strSqlString.AppendFormat("    OR DA1_RES_ID {0} " + "\n", udcRASCondition6.SelectedValueToQueryString);
                        strSqlString.AppendFormat("    OR DA2_RES_ID {0} " + "\n", udcRASCondition6.SelectedValueToQueryString);
                        strSqlString.AppendFormat("    OR DA3_RES_ID {0} " + "\n", udcRASCondition6.SelectedValueToQueryString);
                        strSqlString.AppendFormat("    OR WB1_RES_ID {0} " + "\n", udcRASCondition6.SelectedValueToQueryString);
                        strSqlString.AppendFormat("    OR WB2_RES_ID {0} " + "\n", udcRASCondition6.SelectedValueToQueryString);
                        strSqlString.AppendFormat("    OR WB3_RES_ID {0} " + "\n", udcRASCondition6.SelectedValueToQueryString);
                        strSqlString.AppendFormat("    OR MD_RES_ID {0} )" + "\n", udcRASCondition6.SelectedValueToQueryString);
                        strSqlString.AppendFormat("    OR MK_RES_ID {0} " + "\n", udcRASCondition6.SelectedValueToQueryString);
                        strSqlString.AppendFormat("    OR BM_RES_ID {0} " + "\n", udcRASCondition6.SelectedValueToQueryString);
                        strSqlString.AppendFormat("    OR PS_RES_ID {0} " + "\n", udcRASCondition6.SelectedValueToQueryString);
                        strSqlString.AppendFormat("    OR TR_RES_ID {0} " + "\n", udcRASCondition6.SelectedValueToQueryString);
                        strSqlString.AppendFormat("    OR FORM_RES_ID {0} " + "\n", udcRASCondition6.SelectedValueToQueryString);
                        strSqlString.AppendFormat("    OR AV_RES_ID {0} " + "\n", udcRASCondition6.SelectedValueToQueryString);
                    }

                    #endregion
                    strSqlString.Append("   AND A.WORK_DATE = B.SYS_DATE " + "\n");
                    strSqlString.Append("   AND B.CALENDAR_ID = 'HM' " + "\n");
                    strSqlString.Append("   AND WORK_DATE >='" + cdvFromToDate.FromDate.Text.Replace("-", "") + "' " + "\n");
                    strSqlString.Append("   AND WORK_DATE <='" + cdvFromToDate.ToDate.Text.Replace("-", "") + "' " + "\n");
                    strSqlString.Append("   AND DC_YLD <= " + txtYield.Text + "\n");
                    strSqlString.Append("   AND DA2_START_TIME <> ' '" + "\n");
                    strSqlString.Append(" ORDER BY DA2_RES_ID)" + "\n");
                }
                if (cdvOper.Text.Contains("A0402"))
                {
                    strSqlString.Append("SELECT ROWNUM-1, DA3_RES_ID FROM(" + "\n");
                    strSqlString.Append("SELECT DISTINCT DA3_RES_ID" + "\n");
                    strSqlString.Append("  FROM RSUMDCLHIS A, MWIPCALDEF B" + "\n");
                    strSqlString.Append(" WHERE 1=1 " + "\n");
                    if (cdvCustomer.Text != "" && cdvCustomer.Text != "ALL")
                    {
                        strSqlString.Append("   AND CUSTOMER = '" + cdvCustomer.Text + "' " + "\n");
                    }

                    strSqlString.Append("   AND LOT_ID LIKE '" + txtLotID.Text + "' " + "\n");
                    strSqlString.Append("   AND MAT_ID LIKE '" + txtSearchProduct.Text + "' " + "\n");

                    #region 상세 조회에 따른 SQL문 생성
                    if (udcWIPCondition1.Text != "ALL" && udcWIPCondition1.Text != "")
                        //strSqlString.AppendFormat("   AND CUSTOMER {0} " + "\n", udcWIPCondition1.SelectedValueToQueryString);

                    if (udcWIPCondition2.Text != "ALL" && udcWIPCondition2.Text != "")
                            strSqlString.AppendFormat("   AND FAMILY {0} " + "\n", udcWIPCondition2.SelectedValueToQueryString);

                    if (udcWIPCondition3.Text != "ALL" && udcWIPCondition3.Text != "")
                        strSqlString.AppendFormat("   AND PKG {0} " + "\n", udcWIPCondition3.SelectedValueToQueryString);

                    if (udcWIPCondition4.Text != "ALL" && udcWIPCondition4.Text != "")
                        strSqlString.AppendFormat("   AND TYPE1 {0} " + "\n", udcWIPCondition4.SelectedValueToQueryString);

                    if (udcWIPCondition5.Text != "ALL" && udcWIPCondition5.Text != "")
                        strSqlString.AppendFormat("   AND TYPE2 {0} " + "\n", udcWIPCondition5.SelectedValueToQueryString);

                    if (udcWIPCondition6.Text != "ALL" && udcWIPCondition6.Text != "")
                        strSqlString.AppendFormat("   AND LD_COUNT {0} " + "\n", udcWIPCondition6.SelectedValueToQueryString);

                    if (udcWIPCondition7.Text != "ALL" && udcWIPCondition7.Text != "")
                        strSqlString.AppendFormat("   AND DENSITY {0} " + "\n", udcWIPCondition7.SelectedValueToQueryString);

                    if (udcWIPCondition8.Text != "ALL" && udcWIPCondition8.Text != "")
                        strSqlString.AppendFormat("   AND GENERATION {0} " + "\n", udcWIPCondition8.SelectedValueToQueryString);

                    if (udcRASCondition6.Text != "ALL" && udcRASCondition6.Text != "")
                    {
                        strSqlString.AppendFormat("   AND (BG_RES_ID {0} " + "\n", udcRASCondition6.SelectedValueToQueryString);
                        strSqlString.AppendFormat("    OR SAW_RES_ID {0} " + "\n", udcRASCondition6.SelectedValueToQueryString);
                        strSqlString.AppendFormat("    OR DA1_RES_ID {0} " + "\n", udcRASCondition6.SelectedValueToQueryString);
                        strSqlString.AppendFormat("    OR DA2_RES_ID {0} " + "\n", udcRASCondition6.SelectedValueToQueryString);
                        strSqlString.AppendFormat("    OR DA3_RES_ID {0} " + "\n", udcRASCondition6.SelectedValueToQueryString);
                        strSqlString.AppendFormat("    OR WB1_RES_ID {0} " + "\n", udcRASCondition6.SelectedValueToQueryString);
                        strSqlString.AppendFormat("    OR WB2_RES_ID {0} " + "\n", udcRASCondition6.SelectedValueToQueryString);
                        strSqlString.AppendFormat("    OR WB3_RES_ID {0} " + "\n", udcRASCondition6.SelectedValueToQueryString);
                        strSqlString.AppendFormat("    OR MD_RES_ID {0} )" + "\n", udcRASCondition6.SelectedValueToQueryString);
                        strSqlString.AppendFormat("    OR MK_RES_ID {0} " + "\n", udcRASCondition6.SelectedValueToQueryString);
                        strSqlString.AppendFormat("    OR BM_RES_ID {0} " + "\n", udcRASCondition6.SelectedValueToQueryString);
                        strSqlString.AppendFormat("    OR PS_RES_ID {0} " + "\n", udcRASCondition6.SelectedValueToQueryString);
                        strSqlString.AppendFormat("    OR TR_RES_ID {0} " + "\n", udcRASCondition6.SelectedValueToQueryString);
                        strSqlString.AppendFormat("    OR FORM_RES_ID {0} " + "\n", udcRASCondition6.SelectedValueToQueryString);
                        strSqlString.AppendFormat("    OR AV_RES_ID {0} " + "\n", udcRASCondition6.SelectedValueToQueryString);
                    }

                    #endregion
                    strSqlString.Append("   AND A.WORK_DATE = B.SYS_DATE " + "\n");
                    strSqlString.Append("   AND B.CALENDAR_ID = 'HM' " + "\n");
                    strSqlString.Append("   AND WORK_DATE >='" + cdvFromToDate.FromDate.Text.Replace("-", "") + "' " + "\n");
                    strSqlString.Append("   AND WORK_DATE <='" + cdvFromToDate.ToDate.Text.Replace("-", "") + "' " + "\n");
                    strSqlString.Append("   AND DC_YLD <= " + txtYield.Text + "\n");
                    strSqlString.Append("   AND DA3_START_TIME <> ' '" + "\n");
                    strSqlString.Append(" ORDER BY DA3_RES_ID)" + "\n");
                }
                if (cdvOper.Text.Contains("A0600"))
                {
                    strSqlString.Append("SELECT ROWNUM-1, WB1_RES_ID FROM(" + "\n");
                    strSqlString.Append("SELECT DISTINCT WB1_RES_ID" + "\n");
                    strSqlString.Append("  FROM RSUMDCLHIS A, MWIPCALDEF B" + "\n");
                    strSqlString.Append(" WHERE 1=1 " + "\n");
                    if (cdvCustomer.Text != "" && cdvCustomer.Text != "ALL")
                    {
                        strSqlString.Append("   AND CUSTOMER = '" + cdvCustomer.Text + "' " + "\n");
                    }

                    strSqlString.Append("   AND LOT_ID LIKE '" + txtLotID.Text + "' " + "\n");
                    strSqlString.Append("   AND MAT_ID LIKE '" + txtSearchProduct.Text + "' " + "\n");

                    #region 상세 조회에 따른 SQL문 생성
                    if (udcWIPCondition1.Text != "ALL" && udcWIPCondition1.Text != "")
                        //strSqlString.AppendFormat("   AND CUSTOMER {0} " + "\n", udcWIPCondition1.SelectedValueToQueryString);

                    if (udcWIPCondition2.Text != "ALL" && udcWIPCondition2.Text != "")
                            strSqlString.AppendFormat("   AND FAMILY {0} " + "\n", udcWIPCondition2.SelectedValueToQueryString);

                    if (udcWIPCondition3.Text != "ALL" && udcWIPCondition3.Text != "")
                        strSqlString.AppendFormat("   AND PKG {0} " + "\n", udcWIPCondition3.SelectedValueToQueryString);

                    if (udcWIPCondition4.Text != "ALL" && udcWIPCondition4.Text != "")
                        strSqlString.AppendFormat("   AND TYPE1 {0} " + "\n", udcWIPCondition4.SelectedValueToQueryString);

                    if (udcWIPCondition5.Text != "ALL" && udcWIPCondition5.Text != "")
                        strSqlString.AppendFormat("   AND TYPE2 {0} " + "\n", udcWIPCondition5.SelectedValueToQueryString);

                    if (udcWIPCondition6.Text != "ALL" && udcWIPCondition6.Text != "")
                        strSqlString.AppendFormat("   AND LD_COUNT {0} " + "\n", udcWIPCondition6.SelectedValueToQueryString);

                    if (udcWIPCondition7.Text != "ALL" && udcWIPCondition7.Text != "")
                        strSqlString.AppendFormat("   AND DENSITY {0} " + "\n", udcWIPCondition7.SelectedValueToQueryString);

                    if (udcWIPCondition8.Text != "ALL" && udcWIPCondition8.Text != "")
                        strSqlString.AppendFormat("   AND GENERATION {0} " + "\n", udcWIPCondition8.SelectedValueToQueryString);

                    if (udcRASCondition6.Text != "ALL" && udcRASCondition6.Text != "")
                    {
                        strSqlString.AppendFormat("   AND (BG_RES_ID {0} " + "\n", udcRASCondition6.SelectedValueToQueryString);
                        strSqlString.AppendFormat("    OR SAW_RES_ID {0} " + "\n", udcRASCondition6.SelectedValueToQueryString);
                        strSqlString.AppendFormat("    OR DA1_RES_ID {0} " + "\n", udcRASCondition6.SelectedValueToQueryString);
                        strSqlString.AppendFormat("    OR DA2_RES_ID {0} " + "\n", udcRASCondition6.SelectedValueToQueryString);
                        strSqlString.AppendFormat("    OR DA3_RES_ID {0} " + "\n", udcRASCondition6.SelectedValueToQueryString);
                        strSqlString.AppendFormat("    OR WB1_RES_ID {0} " + "\n", udcRASCondition6.SelectedValueToQueryString);
                        strSqlString.AppendFormat("    OR WB2_RES_ID {0} " + "\n", udcRASCondition6.SelectedValueToQueryString);
                        strSqlString.AppendFormat("    OR WB3_RES_ID {0} " + "\n", udcRASCondition6.SelectedValueToQueryString);
                        strSqlString.AppendFormat("    OR MD_RES_ID {0} )" + "\n", udcRASCondition6.SelectedValueToQueryString);
                        strSqlString.AppendFormat("    OR MK_RES_ID {0} " + "\n", udcRASCondition6.SelectedValueToQueryString);
                        strSqlString.AppendFormat("    OR BM_RES_ID {0} " + "\n", udcRASCondition6.SelectedValueToQueryString);
                        strSqlString.AppendFormat("    OR PS_RES_ID {0} " + "\n", udcRASCondition6.SelectedValueToQueryString);
                        strSqlString.AppendFormat("    OR TR_RES_ID {0} " + "\n", udcRASCondition6.SelectedValueToQueryString);
                        strSqlString.AppendFormat("    OR FORM_RES_ID {0} " + "\n", udcRASCondition6.SelectedValueToQueryString);
                        strSqlString.AppendFormat("    OR AV_RES_ID {0} " + "\n", udcRASCondition6.SelectedValueToQueryString);
                    }

                    #endregion
                    strSqlString.Append("   AND A.WORK_DATE = B.SYS_DATE " + "\n");
                    strSqlString.Append("   AND B.CALENDAR_ID = 'HM' " + "\n");
                    strSqlString.Append("   AND WORK_DATE >='" + cdvFromToDate.FromDate.Text.Replace("-", "") + "' " + "\n");
                    strSqlString.Append("   AND WORK_DATE <='" + cdvFromToDate.ToDate.Text.Replace("-", "") + "' " + "\n");
                    strSqlString.Append("   AND DC_YLD <= " + txtYield.Text + "\n");
                    strSqlString.Append("   AND WB1_START_TIME <> ' '" + "\n");
                    strSqlString.Append(" ORDER BY WB1_RES_ID)" + "\n");
                }
                if (cdvOper.Text.Contains("A0601"))
                {
                    strSqlString.Append("SELECT ROWNUM-1, WB2_RES_ID FROM(" + "\n");
                    strSqlString.Append("SELECT DISTINCT WB2_RES_ID" + "\n");
                    strSqlString.Append("  FROM RSUMDCLHIS A, MWIPCALDEF B" + "\n");
                    strSqlString.Append(" WHERE 1=1 " + "\n");
                    if (cdvCustomer.Text != "" && cdvCustomer.Text != "ALL")
                    {
                        strSqlString.Append("   AND CUSTOMER = '" + cdvCustomer.Text + "' " + "\n");
                    }

                    strSqlString.Append("   AND LOT_ID LIKE '" + txtLotID.Text + "' " + "\n");
                    strSqlString.Append("   AND MAT_ID LIKE '" + txtSearchProduct.Text + "' " + "\n");

                    #region 상세 조회에 따른 SQL문 생성
                    if (udcWIPCondition1.Text != "ALL" && udcWIPCondition1.Text != "")
                        //strSqlString.AppendFormat("   AND CUSTOMER {0} " + "\n", udcWIPCondition1.SelectedValueToQueryString);

                    if (udcWIPCondition2.Text != "ALL" && udcWIPCondition2.Text != "")
                            strSqlString.AppendFormat("   AND FAMILY {0} " + "\n", udcWIPCondition2.SelectedValueToQueryString);

                    if (udcWIPCondition3.Text != "ALL" && udcWIPCondition3.Text != "")
                        strSqlString.AppendFormat("   AND PKG {0} " + "\n", udcWIPCondition3.SelectedValueToQueryString);

                    if (udcWIPCondition4.Text != "ALL" && udcWIPCondition4.Text != "")
                        strSqlString.AppendFormat("   AND TYPE1 {0} " + "\n", udcWIPCondition4.SelectedValueToQueryString);

                    if (udcWIPCondition5.Text != "ALL" && udcWIPCondition5.Text != "")
                        strSqlString.AppendFormat("   AND TYPE2 {0} " + "\n", udcWIPCondition5.SelectedValueToQueryString);

                    if (udcWIPCondition6.Text != "ALL" && udcWIPCondition6.Text != "")
                        strSqlString.AppendFormat("   AND LD_COUNT {0} " + "\n", udcWIPCondition6.SelectedValueToQueryString);

                    if (udcWIPCondition7.Text != "ALL" && udcWIPCondition7.Text != "")
                        strSqlString.AppendFormat("   AND DENSITY {0} " + "\n", udcWIPCondition7.SelectedValueToQueryString);

                    if (udcWIPCondition8.Text != "ALL" && udcWIPCondition8.Text != "")
                        strSqlString.AppendFormat("   AND GENERATION {0} " + "\n", udcWIPCondition8.SelectedValueToQueryString);

                    if (udcRASCondition6.Text != "ALL" && udcRASCondition6.Text != "")
                    {
                        strSqlString.AppendFormat("   AND (BG_RES_ID {0} " + "\n", udcRASCondition6.SelectedValueToQueryString);
                        strSqlString.AppendFormat("    OR SAW_RES_ID {0} " + "\n", udcRASCondition6.SelectedValueToQueryString);
                        strSqlString.AppendFormat("    OR DA1_RES_ID {0} " + "\n", udcRASCondition6.SelectedValueToQueryString);
                        strSqlString.AppendFormat("    OR DA2_RES_ID {0} " + "\n", udcRASCondition6.SelectedValueToQueryString);
                        strSqlString.AppendFormat("    OR DA3_RES_ID {0} " + "\n", udcRASCondition6.SelectedValueToQueryString);
                        strSqlString.AppendFormat("    OR WB1_RES_ID {0} " + "\n", udcRASCondition6.SelectedValueToQueryString);
                        strSqlString.AppendFormat("    OR WB2_RES_ID {0} " + "\n", udcRASCondition6.SelectedValueToQueryString);
                        strSqlString.AppendFormat("    OR WB3_RES_ID {0} " + "\n", udcRASCondition6.SelectedValueToQueryString);
                        strSqlString.AppendFormat("    OR MD_RES_ID {0} )" + "\n", udcRASCondition6.SelectedValueToQueryString);
                        strSqlString.AppendFormat("    OR MK_RES_ID {0} " + "\n", udcRASCondition6.SelectedValueToQueryString);
                        strSqlString.AppendFormat("    OR BM_RES_ID {0} " + "\n", udcRASCondition6.SelectedValueToQueryString);
                        strSqlString.AppendFormat("    OR PS_RES_ID {0} " + "\n", udcRASCondition6.SelectedValueToQueryString);
                        strSqlString.AppendFormat("    OR TR_RES_ID {0} " + "\n", udcRASCondition6.SelectedValueToQueryString);
                        strSqlString.AppendFormat("    OR FORM_RES_ID {0} " + "\n", udcRASCondition6.SelectedValueToQueryString);
                        strSqlString.AppendFormat("    OR AV_RES_ID {0} " + "\n", udcRASCondition6.SelectedValueToQueryString);
                    }

                    #endregion
                    strSqlString.Append("   AND A.WORK_DATE = B.SYS_DATE " + "\n");
                    strSqlString.Append("   AND B.CALENDAR_ID = 'HM' " + "\n");
                    strSqlString.Append("   AND WORK_DATE >='" + cdvFromToDate.FromDate.Text.Replace("-", "") + "' " + "\n");
                    strSqlString.Append("   AND WORK_DATE <='" + cdvFromToDate.ToDate.Text.Replace("-", "") + "' " + "\n");
                    strSqlString.Append("   AND DC_YLD <= " + txtYield.Text + "\n");
                    strSqlString.Append("   AND WB2_START_TIME <> ' '" + "\n");
                    strSqlString.Append(" ORDER BY WB2_RES_ID)" + "\n");
                }
                if (cdvOper.Text.Contains("A0602"))
                {
                    strSqlString.Append("SELECT ROWNUM-1, WB3_RES_ID FROM(" + "\n");
                    strSqlString.Append("SELECT DISTINCT WB3_RES_ID" + "\n");
                    strSqlString.Append("  FROM RSUMDCLHIS A, MWIPCALDEF B" + "\n");
                    strSqlString.Append(" WHERE 1=1 " + "\n");
                    if (cdvCustomer.Text != "" && cdvCustomer.Text != "ALL")
                    {
                        strSqlString.Append("   AND CUSTOMER = '" + cdvCustomer.Text + "' " + "\n");
                    }

                    strSqlString.Append("   AND LOT_ID LIKE '" + txtLotID.Text + "' " + "\n");
                    strSqlString.Append("   AND MAT_ID LIKE '" + txtSearchProduct.Text + "' " + "\n");

                    #region 상세 조회에 따른 SQL문 생성
                    if (udcWIPCondition1.Text != "ALL" && udcWIPCondition1.Text != "")
                        //strSqlString.AppendFormat("   AND CUSTOMER {0} " + "\n", udcWIPCondition1.SelectedValueToQueryString);

                    if (udcWIPCondition2.Text != "ALL" && udcWIPCondition2.Text != "")
                            strSqlString.AppendFormat("   AND FAMILY {0} " + "\n", udcWIPCondition2.SelectedValueToQueryString);

                    if (udcWIPCondition3.Text != "ALL" && udcWIPCondition3.Text != "")
                        strSqlString.AppendFormat("   AND PKG {0} " + "\n", udcWIPCondition3.SelectedValueToQueryString);

                    if (udcWIPCondition4.Text != "ALL" && udcWIPCondition4.Text != "")
                        strSqlString.AppendFormat("   AND TYPE1 {0} " + "\n", udcWIPCondition4.SelectedValueToQueryString);

                    if (udcWIPCondition5.Text != "ALL" && udcWIPCondition5.Text != "")
                        strSqlString.AppendFormat("   AND TYPE2 {0} " + "\n", udcWIPCondition5.SelectedValueToQueryString);

                    if (udcWIPCondition6.Text != "ALL" && udcWIPCondition6.Text != "")
                        strSqlString.AppendFormat("   AND LD_COUNT {0} " + "\n", udcWIPCondition6.SelectedValueToQueryString);

                    if (udcWIPCondition7.Text != "ALL" && udcWIPCondition7.Text != "")
                        strSqlString.AppendFormat("   AND DENSITY {0} " + "\n", udcWIPCondition7.SelectedValueToQueryString);

                    if (udcWIPCondition8.Text != "ALL" && udcWIPCondition8.Text != "")
                        strSqlString.AppendFormat("   AND GENERATION {0} " + "\n", udcWIPCondition8.SelectedValueToQueryString);

                    if (udcRASCondition6.Text != "ALL" && udcRASCondition6.Text != "")
                    {
                        strSqlString.AppendFormat("   AND (BG_RES_ID {0} " + "\n", udcRASCondition6.SelectedValueToQueryString);
                        strSqlString.AppendFormat("    OR SAW_RES_ID {0} " + "\n", udcRASCondition6.SelectedValueToQueryString);
                        strSqlString.AppendFormat("    OR DA1_RES_ID {0} " + "\n", udcRASCondition6.SelectedValueToQueryString);
                        strSqlString.AppendFormat("    OR DA2_RES_ID {0} " + "\n", udcRASCondition6.SelectedValueToQueryString);
                        strSqlString.AppendFormat("    OR DA3_RES_ID {0} " + "\n", udcRASCondition6.SelectedValueToQueryString);
                        strSqlString.AppendFormat("    OR WB1_RES_ID {0} " + "\n", udcRASCondition6.SelectedValueToQueryString);
                        strSqlString.AppendFormat("    OR WB2_RES_ID {0} " + "\n", udcRASCondition6.SelectedValueToQueryString);
                        strSqlString.AppendFormat("    OR WB3_RES_ID {0} " + "\n", udcRASCondition6.SelectedValueToQueryString);
                        strSqlString.AppendFormat("    OR MD_RES_ID {0} )" + "\n", udcRASCondition6.SelectedValueToQueryString);
                        strSqlString.AppendFormat("    OR MK_RES_ID {0} " + "\n", udcRASCondition6.SelectedValueToQueryString);
                        strSqlString.AppendFormat("    OR BM_RES_ID {0} " + "\n", udcRASCondition6.SelectedValueToQueryString);
                        strSqlString.AppendFormat("    OR PS_RES_ID {0} " + "\n", udcRASCondition6.SelectedValueToQueryString);
                        strSqlString.AppendFormat("    OR TR_RES_ID {0} " + "\n", udcRASCondition6.SelectedValueToQueryString);
                        strSqlString.AppendFormat("    OR FORM_RES_ID {0} " + "\n", udcRASCondition6.SelectedValueToQueryString);
                        strSqlString.AppendFormat("    OR AV_RES_ID {0} " + "\n", udcRASCondition6.SelectedValueToQueryString);
                    }

                    #endregion
                    strSqlString.Append("   AND A.WORK_DATE = B.SYS_DATE " + "\n");
                    strSqlString.Append("   AND B.CALENDAR_ID = 'HM' " + "\n");
                    strSqlString.Append("   AND WORK_DATE >='" + cdvFromToDate.FromDate.Text.Replace("-", "") + "' " + "\n");
                    strSqlString.Append("   AND WORK_DATE <='" + cdvFromToDate.ToDate.Text.Replace("-", "") + "' " + "\n");
                    strSqlString.Append("   AND DC_YLD <= " + txtYield.Text + "\n");
                    strSqlString.Append("   AND WB3_START_TIME <> ' '" + "\n");
                    strSqlString.Append(" ORDER BY WB3_RES_ID)" + "\n");
                }
                if (cdvOper.Text.Contains("A1000"))
                {
                    strSqlString.Append("SELECT ROWNUM-1, MD_RES_ID FROM(" + "\n");
                    strSqlString.Append("SELECT DISTINCT MD_RES_ID" + "\n");
                    strSqlString.Append("  FROM RSUMDCLHIS A, MWIPCALDEF B" + "\n");
                    strSqlString.Append(" WHERE 1=1 " + "\n");
                    if (cdvCustomer.Text != "" && cdvCustomer.Text != "ALL")
                    {
                        strSqlString.Append("   AND CUSTOMER = '" + cdvCustomer.Text + "' " + "\n");
                    }

                    strSqlString.Append("   AND LOT_ID LIKE '" + txtLotID.Text + "' " + "\n");
                    strSqlString.Append("   AND MAT_ID LIKE '" + txtSearchProduct.Text + "' " + "\n");

                    #region 상세 조회에 따른 SQL문 생성
                    if (udcWIPCondition1.Text != "ALL" && udcWIPCondition1.Text != "")
                        //strSqlString.AppendFormat("   AND CUSTOMER {0} " + "\n", udcWIPCondition1.SelectedValueToQueryString);

                    if (udcWIPCondition2.Text != "ALL" && udcWIPCondition2.Text != "")
                            strSqlString.AppendFormat("   AND FAMILY {0} " + "\n", udcWIPCondition2.SelectedValueToQueryString);

                    if (udcWIPCondition3.Text != "ALL" && udcWIPCondition3.Text != "")
                        strSqlString.AppendFormat("   AND PKG {0} " + "\n", udcWIPCondition3.SelectedValueToQueryString);

                    if (udcWIPCondition4.Text != "ALL" && udcWIPCondition4.Text != "")
                        strSqlString.AppendFormat("   AND TYPE1 {0} " + "\n", udcWIPCondition4.SelectedValueToQueryString);

                    if (udcWIPCondition5.Text != "ALL" && udcWIPCondition5.Text != "")
                        strSqlString.AppendFormat("   AND TYPE2 {0} " + "\n", udcWIPCondition5.SelectedValueToQueryString);

                    if (udcWIPCondition6.Text != "ALL" && udcWIPCondition6.Text != "")
                        strSqlString.AppendFormat("   AND LD_COUNT {0} " + "\n", udcWIPCondition6.SelectedValueToQueryString);

                    if (udcWIPCondition7.Text != "ALL" && udcWIPCondition7.Text != "")
                        strSqlString.AppendFormat("   AND DENSITY {0} " + "\n", udcWIPCondition7.SelectedValueToQueryString);

                    if (udcWIPCondition8.Text != "ALL" && udcWIPCondition8.Text != "")
                        strSqlString.AppendFormat("   AND GENERATION {0} " + "\n", udcWIPCondition8.SelectedValueToQueryString);

                    if (udcRASCondition6.Text != "ALL" && udcRASCondition6.Text != "")
                    {
                        strSqlString.AppendFormat("   AND (BG_RES_ID {0} " + "\n", udcRASCondition6.SelectedValueToQueryString);
                        strSqlString.AppendFormat("    OR SAW_RES_ID {0} " + "\n", udcRASCondition6.SelectedValueToQueryString);
                        strSqlString.AppendFormat("    OR DA1_RES_ID {0} " + "\n", udcRASCondition6.SelectedValueToQueryString);
                        strSqlString.AppendFormat("    OR DA2_RES_ID {0} " + "\n", udcRASCondition6.SelectedValueToQueryString);
                        strSqlString.AppendFormat("    OR DA3_RES_ID {0} " + "\n", udcRASCondition6.SelectedValueToQueryString);
                        strSqlString.AppendFormat("    OR WB1_RES_ID {0} " + "\n", udcRASCondition6.SelectedValueToQueryString);
                        strSqlString.AppendFormat("    OR WB2_RES_ID {0} " + "\n", udcRASCondition6.SelectedValueToQueryString);
                        strSqlString.AppendFormat("    OR WB3_RES_ID {0} " + "\n", udcRASCondition6.SelectedValueToQueryString);
                        strSqlString.AppendFormat("    OR MD_RES_ID {0} )" + "\n", udcRASCondition6.SelectedValueToQueryString);
                        strSqlString.AppendFormat("    OR MK_RES_ID {0} " + "\n", udcRASCondition6.SelectedValueToQueryString);
                        strSqlString.AppendFormat("    OR BM_RES_ID {0} " + "\n", udcRASCondition6.SelectedValueToQueryString);
                        strSqlString.AppendFormat("    OR PS_RES_ID {0} " + "\n", udcRASCondition6.SelectedValueToQueryString);
                        strSqlString.AppendFormat("    OR TR_RES_ID {0} " + "\n", udcRASCondition6.SelectedValueToQueryString);
                        strSqlString.AppendFormat("    OR FORM_RES_ID {0} " + "\n", udcRASCondition6.SelectedValueToQueryString);
                        strSqlString.AppendFormat("    OR AV_RES_ID {0} " + "\n", udcRASCondition6.SelectedValueToQueryString);
                    }

                    #endregion
                    strSqlString.Append("   AND A.WORK_DATE = B.SYS_DATE " + "\n");
                    strSqlString.Append("   AND B.CALENDAR_ID = 'HM' " + "\n");
                    strSqlString.Append("   AND WORK_DATE >='" + cdvFromToDate.FromDate.Text.Replace("-", "") + "' " + "\n");
                    strSqlString.Append("   AND WORK_DATE <='" + cdvFromToDate.ToDate.Text.Replace("-", "") + "' " + "\n");
                    strSqlString.Append("   AND DC_YLD <= " + txtYield.Text + "\n");
                    strSqlString.Append("   AND MD_START_TIME <> ' '" + "\n");
                    strSqlString.Append(" ORDER BY MD_RES_ID)" + "\n");
                }
                if (cdvOper.Text.Contains("A1150"))
                {
                    strSqlString.Append("SELECT ROWNUM-1, MK_RES_ID FROM(" + "\n");
                    strSqlString.Append("SELECT DISTINCT MK_RES_ID" + "\n");
                    strSqlString.Append("  FROM RSUMDCLHIS A, MWIPCALDEF B" + "\n");
                    strSqlString.Append(" WHERE 1=1 " + "\n");
                    if (cdvCustomer.Text != "" && cdvCustomer.Text != "ALL")
                    {
                        strSqlString.Append("   AND CUSTOMER = '" + cdvCustomer.Text + "' " + "\n");
                    }

                    strSqlString.Append("   AND LOT_ID LIKE '" + txtLotID.Text + "' " + "\n");
                    strSqlString.Append("   AND MAT_ID LIKE '" + txtSearchProduct.Text + "' " + "\n");

                    #region 상세 조회에 따른 SQL문 생성
                    if (udcWIPCondition1.Text != "ALL" && udcWIPCondition1.Text != "")
                        //strSqlString.AppendFormat("   AND CUSTOMER {0} " + "\n", udcWIPCondition1.SelectedValueToQueryString);

                    if (udcWIPCondition2.Text != "ALL" && udcWIPCondition2.Text != "")
                            strSqlString.AppendFormat("   AND FAMILY {0} " + "\n", udcWIPCondition2.SelectedValueToQueryString);

                    if (udcWIPCondition3.Text != "ALL" && udcWIPCondition3.Text != "")
                        strSqlString.AppendFormat("   AND PKG {0} " + "\n", udcWIPCondition3.SelectedValueToQueryString);

                    if (udcWIPCondition4.Text != "ALL" && udcWIPCondition4.Text != "")
                        strSqlString.AppendFormat("   AND TYPE1 {0} " + "\n", udcWIPCondition4.SelectedValueToQueryString);

                    if (udcWIPCondition5.Text != "ALL" && udcWIPCondition5.Text != "")
                        strSqlString.AppendFormat("   AND TYPE2 {0} " + "\n", udcWIPCondition5.SelectedValueToQueryString);

                    if (udcWIPCondition6.Text != "ALL" && udcWIPCondition6.Text != "")
                        strSqlString.AppendFormat("   AND LD_COUNT {0} " + "\n", udcWIPCondition6.SelectedValueToQueryString);

                    if (udcWIPCondition7.Text != "ALL" && udcWIPCondition7.Text != "")
                        strSqlString.AppendFormat("   AND DENSITY {0} " + "\n", udcWIPCondition7.SelectedValueToQueryString);

                    if (udcWIPCondition8.Text != "ALL" && udcWIPCondition8.Text != "")
                        strSqlString.AppendFormat("   AND GENERATION {0} " + "\n", udcWIPCondition8.SelectedValueToQueryString);

                    if (udcRASCondition6.Text != "ALL" && udcRASCondition6.Text != "")
                    {
                        strSqlString.AppendFormat("   AND (BG_RES_ID {0} " + "\n", udcRASCondition6.SelectedValueToQueryString);
                        strSqlString.AppendFormat("    OR SAW_RES_ID {0} " + "\n", udcRASCondition6.SelectedValueToQueryString);
                        strSqlString.AppendFormat("    OR DA1_RES_ID {0} " + "\n", udcRASCondition6.SelectedValueToQueryString);
                        strSqlString.AppendFormat("    OR DA2_RES_ID {0} " + "\n", udcRASCondition6.SelectedValueToQueryString);
                        strSqlString.AppendFormat("    OR DA3_RES_ID {0} " + "\n", udcRASCondition6.SelectedValueToQueryString);
                        strSqlString.AppendFormat("    OR WB1_RES_ID {0} " + "\n", udcRASCondition6.SelectedValueToQueryString);
                        strSqlString.AppendFormat("    OR WB2_RES_ID {0} " + "\n", udcRASCondition6.SelectedValueToQueryString);
                        strSqlString.AppendFormat("    OR WB3_RES_ID {0} " + "\n", udcRASCondition6.SelectedValueToQueryString);
                        strSqlString.AppendFormat("    OR MD_RES_ID {0} )" + "\n", udcRASCondition6.SelectedValueToQueryString);
                        strSqlString.AppendFormat("    OR MK_RES_ID {0} " + "\n", udcRASCondition6.SelectedValueToQueryString);
                        strSqlString.AppendFormat("    OR BM_RES_ID {0} " + "\n", udcRASCondition6.SelectedValueToQueryString);
                        strSqlString.AppendFormat("    OR PS_RES_ID {0} " + "\n", udcRASCondition6.SelectedValueToQueryString);
                        strSqlString.AppendFormat("    OR TR_RES_ID {0} " + "\n", udcRASCondition6.SelectedValueToQueryString);
                        strSqlString.AppendFormat("    OR FORM_RES_ID {0} " + "\n", udcRASCondition6.SelectedValueToQueryString);
                        strSqlString.AppendFormat("    OR AV_RES_ID {0} " + "\n", udcRASCondition6.SelectedValueToQueryString);
                    }

                    #endregion
                    strSqlString.Append("   AND A.WORK_DATE = B.SYS_DATE " + "\n");
                    strSqlString.Append("   AND B.CALENDAR_ID = 'HM' " + "\n");
                    strSqlString.Append("   AND WORK_DATE >='" + cdvFromToDate.FromDate.Text.Replace("-", "") + "' " + "\n");
                    strSqlString.Append("   AND WORK_DATE <='" + cdvFromToDate.ToDate.Text.Replace("-", "") + "' " + "\n");
                    strSqlString.Append("   AND DC_YLD <= " + txtYield.Text + "\n");
                    strSqlString.Append("   AND MK_START_TIME <> ' '" + "\n");
                    strSqlString.Append(" ORDER BY MK_RES_ID)" + "\n");
                }
                if (cdvOper.Text.Contains("A1300"))
                {
                    strSqlString.Append("SELECT ROWNUM-1, BM_RES_ID FROM(" + "\n");
                    strSqlString.Append("SELECT DISTINCT BM_RES_ID" + "\n");
                    strSqlString.Append("  FROM RSUMDCLHIS A, MWIPCALDEF B" + "\n");
                    strSqlString.Append(" WHERE 1=1 " + "\n");
                    if (cdvCustomer.Text != "" && cdvCustomer.Text != "ALL")
                    {
                        strSqlString.Append("   AND CUSTOMER = '" + cdvCustomer.Text + "' " + "\n");
                    }

                    strSqlString.Append("   AND LOT_ID LIKE '" + txtLotID.Text + "' " + "\n");
                    strSqlString.Append("   AND MAT_ID LIKE '" + txtSearchProduct.Text + "' " + "\n");

                    #region 상세 조회에 따른 SQL문 생성
                    if (udcWIPCondition1.Text != "ALL" && udcWIPCondition1.Text != "")
                        //strSqlString.AppendFormat("   AND CUSTOMER {0} " + "\n", udcWIPCondition1.SelectedValueToQueryString);

                    if (udcWIPCondition2.Text != "ALL" && udcWIPCondition2.Text != "")
                            strSqlString.AppendFormat("   AND FAMILY {0} " + "\n", udcWIPCondition2.SelectedValueToQueryString);

                    if (udcWIPCondition3.Text != "ALL" && udcWIPCondition3.Text != "")
                        strSqlString.AppendFormat("   AND PKG {0} " + "\n", udcWIPCondition3.SelectedValueToQueryString);

                    if (udcWIPCondition4.Text != "ALL" && udcWIPCondition4.Text != "")
                        strSqlString.AppendFormat("   AND TYPE1 {0} " + "\n", udcWIPCondition4.SelectedValueToQueryString);

                    if (udcWIPCondition5.Text != "ALL" && udcWIPCondition5.Text != "")
                        strSqlString.AppendFormat("   AND TYPE2 {0} " + "\n", udcWIPCondition5.SelectedValueToQueryString);

                    if (udcWIPCondition6.Text != "ALL" && udcWIPCondition6.Text != "")
                        strSqlString.AppendFormat("   AND LD_COUNT {0} " + "\n", udcWIPCondition6.SelectedValueToQueryString);

                    if (udcWIPCondition7.Text != "ALL" && udcWIPCondition7.Text != "")
                        strSqlString.AppendFormat("   AND DENSITY {0} " + "\n", udcWIPCondition7.SelectedValueToQueryString);

                    if (udcWIPCondition8.Text != "ALL" && udcWIPCondition8.Text != "")
                        strSqlString.AppendFormat("   AND GENERATION {0} " + "\n", udcWIPCondition8.SelectedValueToQueryString);

                    if (udcRASCondition6.Text != "ALL" && udcRASCondition6.Text != "")
                    {
                        strSqlString.AppendFormat("   AND (BG_RES_ID {0} " + "\n", udcRASCondition6.SelectedValueToQueryString);
                        strSqlString.AppendFormat("    OR SAW_RES_ID {0} " + "\n", udcRASCondition6.SelectedValueToQueryString);
                        strSqlString.AppendFormat("    OR DA1_RES_ID {0} " + "\n", udcRASCondition6.SelectedValueToQueryString);
                        strSqlString.AppendFormat("    OR DA2_RES_ID {0} " + "\n", udcRASCondition6.SelectedValueToQueryString);
                        strSqlString.AppendFormat("    OR DA3_RES_ID {0} " + "\n", udcRASCondition6.SelectedValueToQueryString);
                        strSqlString.AppendFormat("    OR WB1_RES_ID {0} " + "\n", udcRASCondition6.SelectedValueToQueryString);
                        strSqlString.AppendFormat("    OR WB2_RES_ID {0} " + "\n", udcRASCondition6.SelectedValueToQueryString);
                        strSqlString.AppendFormat("    OR WB3_RES_ID {0} " + "\n", udcRASCondition6.SelectedValueToQueryString);
                        strSqlString.AppendFormat("    OR MD_RES_ID {0} )" + "\n", udcRASCondition6.SelectedValueToQueryString);
                        strSqlString.AppendFormat("    OR MK_RES_ID {0} " + "\n", udcRASCondition6.SelectedValueToQueryString);
                        strSqlString.AppendFormat("    OR BM_RES_ID {0} " + "\n", udcRASCondition6.SelectedValueToQueryString);
                        strSqlString.AppendFormat("    OR PS_RES_ID {0} " + "\n", udcRASCondition6.SelectedValueToQueryString);
                        strSqlString.AppendFormat("    OR TR_RES_ID {0} " + "\n", udcRASCondition6.SelectedValueToQueryString);
                        strSqlString.AppendFormat("    OR FORM_RES_ID {0} " + "\n", udcRASCondition6.SelectedValueToQueryString);
                        strSqlString.AppendFormat("    OR AV_RES_ID {0} " + "\n", udcRASCondition6.SelectedValueToQueryString);
                    }

                    #endregion
                    strSqlString.Append("   AND A.WORK_DATE = B.SYS_DATE " + "\n");
                    strSqlString.Append("   AND B.CALENDAR_ID = 'HM' " + "\n");
                    strSqlString.Append("   AND WORK_DATE >='" + cdvFromToDate.FromDate.Text.Replace("-", "") + "' " + "\n");
                    strSqlString.Append("   AND WORK_DATE <='" + cdvFromToDate.ToDate.Text.Replace("-", "") + "' " + "\n");
                    strSqlString.Append("   AND DC_YLD <= " + txtYield.Text + "\n");
                    strSqlString.Append("   AND BM_START_TIME <> ' '" + "\n");
                    strSqlString.Append(" ORDER BY BM_RES_ID)" + "\n");
                }
                if (cdvOper.Text.Contains("A1750"))
                {
                    strSqlString.Append("SELECT ROWNUM-1, PS_RES_ID FROM(" + "\n");
                    strSqlString.Append("SELECT DISTINCT PS_RES_ID" + "\n");
                    strSqlString.Append("  FROM RSUMDCLHIS A, MWIPCALDEF B" + "\n");
                    strSqlString.Append(" WHERE 1=1 " + "\n");
                    if (cdvCustomer.Text != "" && cdvCustomer.Text != "ALL")
                    {
                        strSqlString.Append("   AND CUSTOMER = '" + cdvCustomer.Text + "' " + "\n");
                    }

                    strSqlString.Append("   AND LOT_ID LIKE '" + txtLotID.Text + "' " + "\n");
                    strSqlString.Append("   AND MAT_ID LIKE '" + txtSearchProduct.Text + "' " + "\n");

                    #region 상세 조회에 따른 SQL문 생성
                    if (udcWIPCondition1.Text != "ALL" && udcWIPCondition1.Text != "")
                        //strSqlString.AppendFormat("   AND CUSTOMER {0} " + "\n", udcWIPCondition1.SelectedValueToQueryString);

                    if (udcWIPCondition2.Text != "ALL" && udcWIPCondition2.Text != "")
                            strSqlString.AppendFormat("   AND FAMILY {0} " + "\n", udcWIPCondition2.SelectedValueToQueryString);

                    if (udcWIPCondition3.Text != "ALL" && udcWIPCondition3.Text != "")
                        strSqlString.AppendFormat("   AND PKG {0} " + "\n", udcWIPCondition3.SelectedValueToQueryString);

                    if (udcWIPCondition4.Text != "ALL" && udcWIPCondition4.Text != "")
                        strSqlString.AppendFormat("   AND TYPE1 {0} " + "\n", udcWIPCondition4.SelectedValueToQueryString);

                    if (udcWIPCondition5.Text != "ALL" && udcWIPCondition5.Text != "")
                        strSqlString.AppendFormat("   AND TYPE2 {0} " + "\n", udcWIPCondition5.SelectedValueToQueryString);

                    if (udcWIPCondition6.Text != "ALL" && udcWIPCondition6.Text != "")
                        strSqlString.AppendFormat("   AND LD_COUNT {0} " + "\n", udcWIPCondition6.SelectedValueToQueryString);

                    if (udcWIPCondition7.Text != "ALL" && udcWIPCondition7.Text != "")
                        strSqlString.AppendFormat("   AND DENSITY {0} " + "\n", udcWIPCondition7.SelectedValueToQueryString);

                    if (udcWIPCondition8.Text != "ALL" && udcWIPCondition8.Text != "")
                        strSqlString.AppendFormat("   AND GENERATION {0} " + "\n", udcWIPCondition8.SelectedValueToQueryString);

                    if (udcRASCondition6.Text != "ALL" && udcRASCondition6.Text != "")
                    {
                        strSqlString.AppendFormat("   AND (BG_RES_ID {0} " + "\n", udcRASCondition6.SelectedValueToQueryString);
                        strSqlString.AppendFormat("    OR SAW_RES_ID {0} " + "\n", udcRASCondition6.SelectedValueToQueryString);
                        strSqlString.AppendFormat("    OR DA1_RES_ID {0} " + "\n", udcRASCondition6.SelectedValueToQueryString);
                        strSqlString.AppendFormat("    OR DA2_RES_ID {0} " + "\n", udcRASCondition6.SelectedValueToQueryString);
                        strSqlString.AppendFormat("    OR DA3_RES_ID {0} " + "\n", udcRASCondition6.SelectedValueToQueryString);
                        strSqlString.AppendFormat("    OR WB1_RES_ID {0} " + "\n", udcRASCondition6.SelectedValueToQueryString);
                        strSqlString.AppendFormat("    OR WB2_RES_ID {0} " + "\n", udcRASCondition6.SelectedValueToQueryString);
                        strSqlString.AppendFormat("    OR WB3_RES_ID {0} " + "\n", udcRASCondition6.SelectedValueToQueryString);
                        strSqlString.AppendFormat("    OR MD_RES_ID {0} )" + "\n", udcRASCondition6.SelectedValueToQueryString);
                        strSqlString.AppendFormat("    OR MK_RES_ID {0} " + "\n", udcRASCondition6.SelectedValueToQueryString);
                        strSqlString.AppendFormat("    OR BM_RES_ID {0} " + "\n", udcRASCondition6.SelectedValueToQueryString);
                        strSqlString.AppendFormat("    OR PS_RES_ID {0} " + "\n", udcRASCondition6.SelectedValueToQueryString);
                        strSqlString.AppendFormat("    OR TR_RES_ID {0} " + "\n", udcRASCondition6.SelectedValueToQueryString);
                        strSqlString.AppendFormat("    OR FORM_RES_ID {0} " + "\n", udcRASCondition6.SelectedValueToQueryString);
                        strSqlString.AppendFormat("    OR AV_RES_ID {0} " + "\n", udcRASCondition6.SelectedValueToQueryString);
                    }

                    #endregion
                    strSqlString.Append("   AND A.WORK_DATE = B.SYS_DATE " + "\n");
                    strSqlString.Append("   AND B.CALENDAR_ID = 'HM' " + "\n");
                    strSqlString.Append("   AND WORK_DATE >='" + cdvFromToDate.FromDate.Text.Replace("-", "") + "' " + "\n");
                    strSqlString.Append("   AND WORK_DATE <='" + cdvFromToDate.ToDate.Text.Replace("-", "") + "' " + "\n");
                    strSqlString.Append("   AND DC_YLD <= " + txtYield.Text + "\n");
                    strSqlString.Append("   AND PS_START_TIME <> ' '" + "\n");
                    strSqlString.Append(" ORDER BY PS_RES_ID)" + "\n");
                }
                if (cdvOper.Text.Contains("A1200"))
                {
                    strSqlString.Append("SELECT ROWNUM-1, TR_RES_ID FROM(" + "\n");
                    strSqlString.Append("SELECT DISTINCT TR_RES_ID" + "\n");
                    strSqlString.Append("  FROM RSUMDCLHIS A, MWIPCALDEF B" + "\n");
                    strSqlString.Append(" WHERE 1=1 " + "\n");
                    if (cdvCustomer.Text != "" && cdvCustomer.Text != "ALL")
                    {
                        strSqlString.Append("   AND CUSTOMER = '" + cdvCustomer.Text + "' " + "\n");
                    }

                    strSqlString.Append("   AND LOT_ID LIKE '" + txtLotID.Text + "' " + "\n");
                    strSqlString.Append("   AND MAT_ID LIKE '" + txtSearchProduct.Text + "' " + "\n");

                    #region 상세 조회에 따른 SQL문 생성
                    if (udcWIPCondition1.Text != "ALL" && udcWIPCondition1.Text != "")
                        //strSqlString.AppendFormat("   AND CUSTOMER {0} " + "\n", udcWIPCondition1.SelectedValueToQueryString);

                    if (udcWIPCondition2.Text != "ALL" && udcWIPCondition2.Text != "")
                            strSqlString.AppendFormat("   AND FAMILY {0} " + "\n", udcWIPCondition2.SelectedValueToQueryString);

                    if (udcWIPCondition3.Text != "ALL" && udcWIPCondition3.Text != "")
                        strSqlString.AppendFormat("   AND PKG {0} " + "\n", udcWIPCondition3.SelectedValueToQueryString);

                    if (udcWIPCondition4.Text != "ALL" && udcWIPCondition4.Text != "")
                        strSqlString.AppendFormat("   AND TYPE1 {0} " + "\n", udcWIPCondition4.SelectedValueToQueryString);

                    if (udcWIPCondition5.Text != "ALL" && udcWIPCondition5.Text != "")
                        strSqlString.AppendFormat("   AND TYPE2 {0} " + "\n", udcWIPCondition5.SelectedValueToQueryString);

                    if (udcWIPCondition6.Text != "ALL" && udcWIPCondition6.Text != "")
                        strSqlString.AppendFormat("   AND LD_COUNT {0} " + "\n", udcWIPCondition6.SelectedValueToQueryString);

                    if (udcWIPCondition7.Text != "ALL" && udcWIPCondition7.Text != "")
                        strSqlString.AppendFormat("   AND DENSITY {0} " + "\n", udcWIPCondition7.SelectedValueToQueryString);

                    if (udcWIPCondition8.Text != "ALL" && udcWIPCondition8.Text != "")
                        strSqlString.AppendFormat("   AND GENERATION {0} " + "\n", udcWIPCondition8.SelectedValueToQueryString);

                    if (udcRASCondition6.Text != "ALL" && udcRASCondition6.Text != "")
                    {
                        strSqlString.AppendFormat("   AND (BG_RES_ID {0} " + "\n", udcRASCondition6.SelectedValueToQueryString);
                        strSqlString.AppendFormat("    OR SAW_RES_ID {0} " + "\n", udcRASCondition6.SelectedValueToQueryString);
                        strSqlString.AppendFormat("    OR DA1_RES_ID {0} " + "\n", udcRASCondition6.SelectedValueToQueryString);
                        strSqlString.AppendFormat("    OR DA2_RES_ID {0} " + "\n", udcRASCondition6.SelectedValueToQueryString);
                        strSqlString.AppendFormat("    OR DA3_RES_ID {0} " + "\n", udcRASCondition6.SelectedValueToQueryString);
                        strSqlString.AppendFormat("    OR WB1_RES_ID {0} " + "\n", udcRASCondition6.SelectedValueToQueryString);
                        strSqlString.AppendFormat("    OR WB2_RES_ID {0} " + "\n", udcRASCondition6.SelectedValueToQueryString);
                        strSqlString.AppendFormat("    OR WB3_RES_ID {0} " + "\n", udcRASCondition6.SelectedValueToQueryString);
                        strSqlString.AppendFormat("    OR MD_RES_ID {0} )" + "\n", udcRASCondition6.SelectedValueToQueryString);
                        strSqlString.AppendFormat("    OR MK_RES_ID {0} " + "\n", udcRASCondition6.SelectedValueToQueryString);
                        strSqlString.AppendFormat("    OR BM_RES_ID {0} " + "\n", udcRASCondition6.SelectedValueToQueryString);
                        strSqlString.AppendFormat("    OR PS_RES_ID {0} " + "\n", udcRASCondition6.SelectedValueToQueryString);
                        strSqlString.AppendFormat("    OR TR_RES_ID {0} " + "\n", udcRASCondition6.SelectedValueToQueryString);
                        strSqlString.AppendFormat("    OR FORM_RES_ID {0} " + "\n", udcRASCondition6.SelectedValueToQueryString);
                        strSqlString.AppendFormat("    OR AV_RES_ID {0} " + "\n", udcRASCondition6.SelectedValueToQueryString);
                    }

                    #endregion
                    strSqlString.Append("   AND A.WORK_DATE = B.SYS_DATE " + "\n");
                    strSqlString.Append("   AND B.CALENDAR_ID = 'HM' " + "\n");
                    strSqlString.Append("   AND WORK_DATE >='" + cdvFromToDate.FromDate.Text.Replace("-", "") + "' " + "\n");
                    strSqlString.Append("   AND WORK_DATE <='" + cdvFromToDate.ToDate.Text.Replace("-", "") + "' " + "\n");
                    strSqlString.Append("   AND DC_YLD <= " + txtYield.Text + "\n");
                    strSqlString.Append("   AND TR_START_TIME <> ' '" + "\n");
                    strSqlString.Append(" ORDER BY TR_RES_ID)" + "\n");
                }
                if (cdvOper.Text.Contains("A1800"))
                {
                    strSqlString.Append("SELECT ROWNUM-1, FORM_RES_ID FROM(" + "\n");
                    strSqlString.Append("SELECT DISTINCT FORM_RES_ID" + "\n");
                    strSqlString.Append("  FROM RSUMDCLHIS A, MWIPCALDEF B" + "\n");
                    strSqlString.Append(" WHERE 1=1 " + "\n");
                    if (cdvCustomer.Text != "" && cdvCustomer.Text != "ALL")
                    {
                        strSqlString.Append("   AND CUSTOMER = '" + cdvCustomer.Text + "' " + "\n");
                    }

                    strSqlString.Append("   AND LOT_ID LIKE '" + txtLotID.Text + "' " + "\n");
                    strSqlString.Append("   AND MAT_ID LIKE '" + txtSearchProduct.Text + "' " + "\n");

                    #region 상세 조회에 따른 SQL문 생성
                    if (udcWIPCondition1.Text != "ALL" && udcWIPCondition1.Text != "")
                        //strSqlString.AppendFormat("   AND CUSTOMER {0} " + "\n", udcWIPCondition1.SelectedValueToQueryString);

                    if (udcWIPCondition2.Text != "ALL" && udcWIPCondition2.Text != "")
                            strSqlString.AppendFormat("   AND FAMILY {0} " + "\n", udcWIPCondition2.SelectedValueToQueryString);

                    if (udcWIPCondition3.Text != "ALL" && udcWIPCondition3.Text != "")
                        strSqlString.AppendFormat("   AND PKG {0} " + "\n", udcWIPCondition3.SelectedValueToQueryString);

                    if (udcWIPCondition4.Text != "ALL" && udcWIPCondition4.Text != "")
                        strSqlString.AppendFormat("   AND TYPE1 {0} " + "\n", udcWIPCondition4.SelectedValueToQueryString);

                    if (udcWIPCondition5.Text != "ALL" && udcWIPCondition5.Text != "")
                        strSqlString.AppendFormat("   AND TYPE2 {0} " + "\n", udcWIPCondition5.SelectedValueToQueryString);

                    if (udcWIPCondition6.Text != "ALL" && udcWIPCondition6.Text != "")
                        strSqlString.AppendFormat("   AND LD_COUNT {0} " + "\n", udcWIPCondition6.SelectedValueToQueryString);

                    if (udcWIPCondition7.Text != "ALL" && udcWIPCondition7.Text != "")
                        strSqlString.AppendFormat("   AND DENSITY {0} " + "\n", udcWIPCondition7.SelectedValueToQueryString);

                    if (udcWIPCondition8.Text != "ALL" && udcWIPCondition8.Text != "")
                        strSqlString.AppendFormat("   AND GENERATION {0} " + "\n", udcWIPCondition8.SelectedValueToQueryString);

                    if (udcRASCondition6.Text != "ALL" && udcRASCondition6.Text != "")
                    {
                        strSqlString.AppendFormat("   AND (BG_RES_ID {0} " + "\n", udcRASCondition6.SelectedValueToQueryString);
                        strSqlString.AppendFormat("    OR SAW_RES_ID {0} " + "\n", udcRASCondition6.SelectedValueToQueryString);
                        strSqlString.AppendFormat("    OR DA1_RES_ID {0} " + "\n", udcRASCondition6.SelectedValueToQueryString);
                        strSqlString.AppendFormat("    OR DA2_RES_ID {0} " + "\n", udcRASCondition6.SelectedValueToQueryString);
                        strSqlString.AppendFormat("    OR DA3_RES_ID {0} " + "\n", udcRASCondition6.SelectedValueToQueryString);
                        strSqlString.AppendFormat("    OR WB1_RES_ID {0} " + "\n", udcRASCondition6.SelectedValueToQueryString);
                        strSqlString.AppendFormat("    OR WB2_RES_ID {0} " + "\n", udcRASCondition6.SelectedValueToQueryString);
                        strSqlString.AppendFormat("    OR WB3_RES_ID {0} " + "\n", udcRASCondition6.SelectedValueToQueryString);
                        strSqlString.AppendFormat("    OR MD_RES_ID {0} )" + "\n", udcRASCondition6.SelectedValueToQueryString);
                        strSqlString.AppendFormat("    OR MK_RES_ID {0} " + "\n", udcRASCondition6.SelectedValueToQueryString);
                        strSqlString.AppendFormat("    OR BM_RES_ID {0} " + "\n", udcRASCondition6.SelectedValueToQueryString);
                        strSqlString.AppendFormat("    OR PS_RES_ID {0} " + "\n", udcRASCondition6.SelectedValueToQueryString);
                        strSqlString.AppendFormat("    OR TR_RES_ID {0} " + "\n", udcRASCondition6.SelectedValueToQueryString);
                        strSqlString.AppendFormat("    OR FORM_RES_ID {0} " + "\n", udcRASCondition6.SelectedValueToQueryString);
                        strSqlString.AppendFormat("    OR AV_RES_ID {0} " + "\n", udcRASCondition6.SelectedValueToQueryString);
                    }

                    #endregion
                    strSqlString.Append("   AND A.WORK_DATE = B.SYS_DATE " + "\n");
                    strSqlString.Append("   AND B.CALENDAR_ID = 'HM' " + "\n");
                    strSqlString.Append("   AND WORK_DATE >='" + cdvFromToDate.FromDate.Text.Replace("-", "") + "' " + "\n");
                    strSqlString.Append("   AND WORK_DATE <='" + cdvFromToDate.ToDate.Text.Replace("-", "") + "' " + "\n");
                    strSqlString.Append("   AND DC_YLD <= " + txtYield.Text + "\n");
                    strSqlString.Append("   AND FORM_START_TIME <> ' '" + "\n");
                    strSqlString.Append(" ORDER BY FORM_RES_ID)" + "\n");
                }
                if (cdvOper.Text.Contains("A2000"))
                {
                    strSqlString.Append("SELECT ROWNUM-1, AV_RES_ID FROM(" + "\n");
                    strSqlString.Append("SELECT DISTINCT AV_RES_ID" + "\n");
                    strSqlString.Append("  FROM RSUMDCLHIS A, MWIPCALDEF B" + "\n");
                    strSqlString.Append(" WHERE 1=1 " + "\n");
                    if (cdvCustomer.Text != "" && cdvCustomer.Text != "ALL")
                    {
                        strSqlString.Append("   AND CUSTOMER = '" + cdvCustomer.Text + "' " + "\n");
                    }

                    strSqlString.Append("   AND LOT_ID LIKE '" + txtLotID.Text + "' " + "\n");
                    strSqlString.Append("   AND MAT_ID LIKE '" + txtSearchProduct.Text + "' " + "\n");

                    #region 상세 조회에 따른 SQL문 생성
                    if (udcWIPCondition1.Text != "ALL" && udcWIPCondition1.Text != "")
                        //strSqlString.AppendFormat("   AND CUSTOMER {0} " + "\n", udcWIPCondition1.SelectedValueToQueryString);

                    if (udcWIPCondition2.Text != "ALL" && udcWIPCondition2.Text != "")
                            strSqlString.AppendFormat("   AND FAMILY {0} " + "\n", udcWIPCondition2.SelectedValueToQueryString);

                    if (udcWIPCondition3.Text != "ALL" && udcWIPCondition3.Text != "")
                        strSqlString.AppendFormat("   AND PKG {0} " + "\n", udcWIPCondition3.SelectedValueToQueryString);

                    if (udcWIPCondition4.Text != "ALL" && udcWIPCondition4.Text != "")
                        strSqlString.AppendFormat("   AND TYPE1 {0} " + "\n", udcWIPCondition4.SelectedValueToQueryString);

                    if (udcWIPCondition5.Text != "ALL" && udcWIPCondition5.Text != "")
                        strSqlString.AppendFormat("   AND TYPE2 {0} " + "\n", udcWIPCondition5.SelectedValueToQueryString);

                    if (udcWIPCondition6.Text != "ALL" && udcWIPCondition6.Text != "")
                        strSqlString.AppendFormat("   AND LD_COUNT {0} " + "\n", udcWIPCondition6.SelectedValueToQueryString);

                    if (udcWIPCondition7.Text != "ALL" && udcWIPCondition7.Text != "")
                        strSqlString.AppendFormat("   AND DENSITY {0} " + "\n", udcWIPCondition7.SelectedValueToQueryString);

                    if (udcWIPCondition8.Text != "ALL" && udcWIPCondition8.Text != "")
                        strSqlString.AppendFormat("   AND GENERATION {0} " + "\n", udcWIPCondition8.SelectedValueToQueryString);

                    if (udcRASCondition6.Text != "ALL" && udcRASCondition6.Text != "")
                    {
                        strSqlString.AppendFormat("   AND (BG_RES_ID {0} " + "\n", udcRASCondition6.SelectedValueToQueryString);
                        strSqlString.AppendFormat("    OR SAW_RES_ID {0} " + "\n", udcRASCondition6.SelectedValueToQueryString);
                        strSqlString.AppendFormat("    OR DA1_RES_ID {0} " + "\n", udcRASCondition6.SelectedValueToQueryString);
                        strSqlString.AppendFormat("    OR DA2_RES_ID {0} " + "\n", udcRASCondition6.SelectedValueToQueryString);
                        strSqlString.AppendFormat("    OR DA3_RES_ID {0} " + "\n", udcRASCondition6.SelectedValueToQueryString);
                        strSqlString.AppendFormat("    OR WB1_RES_ID {0} " + "\n", udcRASCondition6.SelectedValueToQueryString);
                        strSqlString.AppendFormat("    OR WB2_RES_ID {0} " + "\n", udcRASCondition6.SelectedValueToQueryString);
                        strSqlString.AppendFormat("    OR WB3_RES_ID {0} " + "\n", udcRASCondition6.SelectedValueToQueryString);
                        strSqlString.AppendFormat("    OR MD_RES_ID {0} )" + "\n", udcRASCondition6.SelectedValueToQueryString);
                        strSqlString.AppendFormat("    OR MK_RES_ID {0} " + "\n", udcRASCondition6.SelectedValueToQueryString);
                        strSqlString.AppendFormat("    OR BM_RES_ID {0} " + "\n", udcRASCondition6.SelectedValueToQueryString);
                        strSqlString.AppendFormat("    OR PS_RES_ID {0} " + "\n", udcRASCondition6.SelectedValueToQueryString);
                        strSqlString.AppendFormat("    OR TR_RES_ID {0} " + "\n", udcRASCondition6.SelectedValueToQueryString);
                        strSqlString.AppendFormat("    OR FORM_RES_ID {0} " + "\n", udcRASCondition6.SelectedValueToQueryString);
                        strSqlString.AppendFormat("    OR AV_RES_ID {0} " + "\n", udcRASCondition6.SelectedValueToQueryString);
                    }

                    #endregion
                    strSqlString.Append("   AND A.WORK_DATE = B.SYS_DATE " + "\n");
                    strSqlString.Append("   AND B.CALENDAR_ID = 'HM' " + "\n");
                    strSqlString.Append("   AND WORK_DATE >='" + cdvFromToDate.FromDate.Text.Replace("-", "") + "' " + "\n");
                    strSqlString.Append("   AND WORK_DATE <='" + cdvFromToDate.ToDate.Text.Replace("-", "") + "' " + "\n");
                    strSqlString.Append("   AND DC_YLD <= " + txtYield.Text + "\n");
                    strSqlString.Append("   AND AV_START_TIME <> ' '" + "\n");
                    strSqlString.Append(" ORDER BY AV_RES_ID)" + "\n");
                }
            }
            if (GlobalVariable.gsUserGroup == "ADMIN_GROUP" || GlobalVariable.gsUserGroup == "HANA_ADMIN_GROUP")
            {
                System.Windows.Forms.Clipboard.SetText(strSqlString.ToString());
            }

            return strSqlString.ToString();
        }

        //2013-03-15-김민우: VENDOR별 검색
        private string MakeSqlStringMat()
        {
            StringBuilder strSqlString = new StringBuilder();

            if (cdvMat.Text != "ALL" || cdvMat.Text != "")
            {
                strSqlString.Append("SELECT WORK_DATE,LOT_ID" + "\n");
                
                if (cdvMat.Text.Contains("PB"))
                {
                        strSqlString.Append(", PCB_VEN_NM");
                }
                if (cdvMat.Text.Contains("LF"))
                {
                    if (cdvMat.Text.Substring(0, 2).Equals("LF"))
                    {
                        strSqlString.Append(", LF_VEN_NM");
                    }
                    else
                    {
                        strSqlString.Append("||'-'||LF_VEN_NM");
                    }
                }
                if (cdvMat.Text.Contains("MC"))
                {
                    if (cdvMat.Text.Substring(0, 2).Equals("MC"))
                    {
                        strSqlString.Append(", EMC_VEN_NM");
                    }
                    else
                    {
                        strSqlString.Append("||'-'||EMC_VEN_NM");
                    }
                }
                if (cdvMat.Text.Contains("AE"))
                {
                    if (cdvMat.Text.Substring(0, 2).Equals("AE"))
                    {
                        strSqlString.Append(", EPX_VEN_NM");
                    }
                    else
                    {
                        strSqlString.Append("||'-'||EPX_VEN_NM");
                    }
                }
                if (cdvMat.Text.Contains("GW"))
                {
                    if (cdvMat.Text.Substring(0, 2).Equals("GW"))
                    {
                        strSqlString.Append(", GW_VEN_NM");
                    }
                    else
                    {
                        strSqlString.Append("||'-'||GW_VEN_NM");
                    }
                }
                if (cdvMat.Text.Contains("SB"))
                {
                    if (cdvMat.Text.Substring(0, 2).Equals("SB"))
                    {
                        strSqlString.Append(", SB_VEN_NM");
                    }
                    else
                    {
                        strSqlString.Append("||'-'||SB_VEN_NM");
                    }
                }
              
                strSqlString.Append("\n");
                strSqlString.Append(", DC_YLD,WORK_DATE||'-'||LOT_ID" + "\n");

    
                strSqlString.Append("  FROM RSUMDCLHIS A, MWIPCALDEF B" + "\n");
                strSqlString.Append(" WHERE 1=1 " + "\n");
                if (cdvCustomer.Text != "" && cdvCustomer.Text != "ALL")
                {
                    strSqlString.Append("   AND CUSTOMER = '" + cdvCustomer.Text + "' " + "\n");
                }
                strSqlString.Append("   AND LOT_ID LIKE '" + txtLotID.Text + "' " + "\n");
                strSqlString.Append("   AND MAT_ID LIKE '" + txtSearchProduct.Text + "' " + "\n");
                #region 상세 조회에 따른 SQL문 생성
                if (udcWIPCondition1.Text != "ALL" && udcWIPCondition1.Text != "")
                    //strSqlString.AppendFormat("   AND CUSTOMER {0} " + "\n", udcWIPCondition1.SelectedValueToQueryString);

                    if (udcWIPCondition2.Text != "ALL" && udcWIPCondition2.Text != "")
                        strSqlString.AppendFormat("   AND FAMILY {0} " + "\n", udcWIPCondition2.SelectedValueToQueryString);

                if (udcWIPCondition3.Text != "ALL" && udcWIPCondition3.Text != "")
                    strSqlString.AppendFormat("   AND PKG {0} " + "\n", udcWIPCondition3.SelectedValueToQueryString);

                if (udcWIPCondition4.Text != "ALL" && udcWIPCondition4.Text != "")
                    strSqlString.AppendFormat("   AND TYPE1 {0} " + "\n", udcWIPCondition4.SelectedValueToQueryString);

                if (udcWIPCondition5.Text != "ALL" && udcWIPCondition5.Text != "")
                    strSqlString.AppendFormat("   AND TYPE2 {0} " + "\n", udcWIPCondition5.SelectedValueToQueryString);

                if (udcWIPCondition6.Text != "ALL" && udcWIPCondition6.Text != "")
                    strSqlString.AppendFormat("   AND LD_COUNT {0} " + "\n", udcWIPCondition6.SelectedValueToQueryString);

                if (udcWIPCondition7.Text != "ALL" && udcWIPCondition7.Text != "")
                    strSqlString.AppendFormat("   AND DENSITY {0} " + "\n", udcWIPCondition7.SelectedValueToQueryString);

                if (udcWIPCondition8.Text != "ALL" && udcWIPCondition8.Text != "")
                    strSqlString.AppendFormat("   AND GENERATION {0} " + "\n", udcWIPCondition8.SelectedValueToQueryString);

                if (udcRASCondition6.Text != "ALL" && udcRASCondition6.Text != "")
                {
                    strSqlString.AppendFormat("   AND (BG_RES_ID {0} " + "\n", udcRASCondition6.SelectedValueToQueryString);
                    strSqlString.AppendFormat("    OR SAW_RES_ID {0} " + "\n", udcRASCondition6.SelectedValueToQueryString);
                    strSqlString.AppendFormat("    OR DA1_RES_ID {0} " + "\n", udcRASCondition6.SelectedValueToQueryString);
                    strSqlString.AppendFormat("    OR DA2_RES_ID {0} " + "\n", udcRASCondition6.SelectedValueToQueryString);
                    strSqlString.AppendFormat("    OR DA3_RES_ID {0} " + "\n", udcRASCondition6.SelectedValueToQueryString);
                    strSqlString.AppendFormat("    OR WB1_RES_ID {0} " + "\n", udcRASCondition6.SelectedValueToQueryString);
                    strSqlString.AppendFormat("    OR WB2_RES_ID {0} " + "\n", udcRASCondition6.SelectedValueToQueryString);
                    strSqlString.AppendFormat("    OR WB3_RES_ID {0} " + "\n", udcRASCondition6.SelectedValueToQueryString);
                    strSqlString.AppendFormat("    OR MD_RES_ID {0} )" + "\n", udcRASCondition6.SelectedValueToQueryString);
                    strSqlString.AppendFormat("    OR MK_RES_ID {0} " + "\n", udcRASCondition6.SelectedValueToQueryString);
                    strSqlString.AppendFormat("    OR BM_RES_ID {0} " + "\n", udcRASCondition6.SelectedValueToQueryString);
                    strSqlString.AppendFormat("    OR PS_RES_ID {0} " + "\n", udcRASCondition6.SelectedValueToQueryString);
                    strSqlString.AppendFormat("    OR TR_RES_ID {0} " + "\n", udcRASCondition6.SelectedValueToQueryString);
                    strSqlString.AppendFormat("    OR FORM_RES_ID {0} " + "\n", udcRASCondition6.SelectedValueToQueryString);
                    strSqlString.AppendFormat("    OR AV_RES_ID {0} " + "\n", udcRASCondition6.SelectedValueToQueryString);
                }

                #endregion
                strSqlString.Append("   AND A.WORK_DATE = B.SYS_DATE " + "\n");
                strSqlString.Append("   AND B.CALENDAR_ID = 'HM' " + "\n");
                strSqlString.Append("   AND WORK_DATE >='" + cdvFromToDate.FromDate.Text.Replace("-", "") + "' " + "\n");
                strSqlString.Append("   AND WORK_DATE <='" + cdvFromToDate.ToDate.Text.Replace("-", "") + "' " + "\n");
                strSqlString.Append("   AND DC_YLD <= " + txtYield.Text + "\n");

                if (cdvMat.Text.Contains("PB"))
                {
                    strSqlString.Append("   AND PCB_VEN_NM <> ' ' " + "\n");
                }
                if (cdvMat.Text.Contains("LF"))
                {
                    strSqlString.Append("   AND LF_VEN_NM <> ' ' " + "\n");
                }
                if (cdvMat.Text.Contains("MC"))
                {
                    strSqlString.Append("   AND EMC_VEN_NM <> ' ' " + "\n");
                }
                if (cdvMat.Text.Contains("AE"))
                {
                    strSqlString.Append("   AND EPX_VEN_NM <> ' ' " + "\n");
                }
                if (cdvMat.Text.Contains("GW"))
                {
                    strSqlString.Append("   AND GW_VEN_NM <> ' ' " + "\n");
                }
                if (cdvMat.Text.Contains("SB"))
                {
                    strSqlString.Append("   AND SB_VEN_NM <> ' ' " + "\n");
                }

                strSqlString.Append(" ORDER BY WORK_DATE, ");

                if (cdvMat.Text.Contains("PB"))
                {
                    strSqlString.Append("PCB_VEN_NM");
                }
                if (cdvMat.Text.Contains("LF"))
                {
                    if (cdvMat.Text.Substring(0, 2).Equals("LF"))
                    {
                        strSqlString.Append("LF_VEN_NM");
                    }
                    else
                    {
                        strSqlString.Append("||'-'||LF_VEN_NM");
                    }
                }
                if (cdvMat.Text.Contains("MC"))
                {
                    if (cdvMat.Text.Substring(0, 2).Equals("MC"))
                    {
                        strSqlString.Append("EMC_VEN_NM");
                    }
                    else
                    {
                        strSqlString.Append("||'-'||EMC_VEN_NM");
                    }
                }
                if (cdvMat.Text.Contains("AE"))
                {
                    if (cdvMat.Text.Substring(0, 2).Equals("AE"))
                    {
                        strSqlString.Append("EPX_VEN_NM");
                    }
                    else
                    {
                        strSqlString.Append("||'-'||EPX_VEN_NM");
                    }
                }
                if (cdvMat.Text.Contains("GW"))
                {
                    if (cdvMat.Text.Substring(0, 2).Equals("GW"))
                    {
                        strSqlString.Append("GW_VEN_NM");
                    }
                    else
                    {
                        strSqlString.Append("||'-'||GW_VEN_NM");
                    }
                }
                if (cdvMat.Text.Contains("SB"))
                {
                    if (cdvMat.Text.Substring(0, 2).Equals("SB"))
                    {
                        strSqlString.Append("SB_VEN_NM");
                    }
                    else
                    {
                        strSqlString.Append("||'-'||SB_VEN_NM");
                    }
                }



            }
            if (GlobalVariable.gsUserGroup == "ADMIN_GROUP" || GlobalVariable.gsUserGroup == "HANA_ADMIN_GROUP")
            {
                System.Windows.Forms.Clipboard.SetText(strSqlString.ToString());
            }

            return strSqlString.ToString();
        }

        /// 해당 조건에 맞는 원부자재명
        private string MakeMatCount()
        {
            StringBuilder strSqlString = new StringBuilder();
            
            strSqlString.Append("SELECT ROWNUM-1, MAT_INFO " + "\n");
            
            strSqlString.Append("FROM( SELECT DISTINCT " + "\n");
            if (cdvMat.Text.Contains("PB"))
            {
                strSqlString.Append("PCB_VEN_NM");
            }
            if (cdvMat.Text.Contains("LF"))
            {
                if (cdvMat.Text.Substring(0, 2).Equals("LF"))
                {
                    strSqlString.Append("LF_VEN_NM");
                }
                else
                {
                    strSqlString.Append("||'-'||LF_VEN_NM");
                }
            }
            if (cdvMat.Text.Contains("MC"))
            {
                if (cdvMat.Text.Substring(0, 2).Equals("MC"))
                {
                    strSqlString.Append("EMC_VEN_NM");
                }
                else
                {
                    strSqlString.Append("||'-'||EMC_VEN_NM");
                }
            }
            if (cdvMat.Text.Contains("AE"))
            {
                if (cdvMat.Text.Substring(0, 2).Equals("AE"))
                {
                    strSqlString.Append("EPX_VEN_NM");
                }
                else
                {
                    strSqlString.Append("||'-'||EPX_VEN_NM");
                }
            }
            if (cdvMat.Text.Contains("GW"))
            {
                if (cdvMat.Text.Substring(0, 2).Equals("GW"))
                {
                    strSqlString.Append("GW_VEN_NM");
                }
                else
                {
                    strSqlString.Append("||'-'||GW_VEN_NM");
                }
            }
            if (cdvMat.Text.Contains("SB"))
            {
                if (cdvMat.Text.Substring(0, 2).Equals("SB"))
                {
                    strSqlString.Append("SB_VEN_NM");
                }
                else
                {
                    strSqlString.Append("||'-'||SB_VEN_NM");
                }
            }
            strSqlString.Append(" AS MAT_INFO");
            strSqlString.Append("  FROM RSUMDCLHIS A, MWIPCALDEF B" + "\n");
            strSqlString.Append(" WHERE 1=1 " + "\n");
            if (cdvCustomer.Text != "" && cdvCustomer.Text != "ALL")
            {
                strSqlString.Append("   AND CUSTOMER = '" + cdvCustomer.Text + "' " + "\n");
            }
            strSqlString.Append("   AND LOT_ID LIKE '" + txtLotID.Text + "' " + "\n");
            strSqlString.Append("   AND MAT_ID LIKE '" + txtSearchProduct.Text + "' " + "\n");

            #region 상세 조회에 따른 SQL문 생성
            if (udcWIPCondition1.Text != "ALL" && udcWIPCondition1.Text != "")
                //strSqlString.AppendFormat("   AND CUSTOMER {0} " + "\n", udcWIPCondition1.SelectedValueToQueryString);

                if (udcWIPCondition2.Text != "ALL" && udcWIPCondition2.Text != "")
                    strSqlString.AppendFormat("   AND FAMILY {0} " + "\n", udcWIPCondition2.SelectedValueToQueryString);

            if (udcWIPCondition3.Text != "ALL" && udcWIPCondition3.Text != "")
                strSqlString.AppendFormat("   AND PKG {0} " + "\n", udcWIPCondition3.SelectedValueToQueryString);

            if (udcWIPCondition4.Text != "ALL" && udcWIPCondition4.Text != "")
                strSqlString.AppendFormat("   AND TYPE1 {0} " + "\n", udcWIPCondition4.SelectedValueToQueryString);

            if (udcWIPCondition5.Text != "ALL" && udcWIPCondition5.Text != "")
                strSqlString.AppendFormat("   AND TYPE2 {0} " + "\n", udcWIPCondition5.SelectedValueToQueryString);

            if (udcWIPCondition6.Text != "ALL" && udcWIPCondition6.Text != "")
                strSqlString.AppendFormat("   AND LD_COUNT {0} " + "\n", udcWIPCondition6.SelectedValueToQueryString);

            if (udcWIPCondition7.Text != "ALL" && udcWIPCondition7.Text != "")
                strSqlString.AppendFormat("   AND DENSITY {0} " + "\n", udcWIPCondition7.SelectedValueToQueryString);

            if (udcWIPCondition8.Text != "ALL" && udcWIPCondition8.Text != "")
                strSqlString.AppendFormat("   AND GENERATION {0} " + "\n", udcWIPCondition8.SelectedValueToQueryString);

            if (udcRASCondition6.Text != "ALL" && udcRASCondition6.Text != "")
            {
                strSqlString.AppendFormat("   AND (BG_RES_ID {0} " + "\n", udcRASCondition6.SelectedValueToQueryString);
                strSqlString.AppendFormat("    OR SAW_RES_ID {0} " + "\n", udcRASCondition6.SelectedValueToQueryString);
                strSqlString.AppendFormat("    OR DA1_RES_ID {0} " + "\n", udcRASCondition6.SelectedValueToQueryString);
                strSqlString.AppendFormat("    OR DA2_RES_ID {0} " + "\n", udcRASCondition6.SelectedValueToQueryString);
                strSqlString.AppendFormat("    OR DA3_RES_ID {0} " + "\n", udcRASCondition6.SelectedValueToQueryString);
                strSqlString.AppendFormat("    OR WB1_RES_ID {0} " + "\n", udcRASCondition6.SelectedValueToQueryString);
                strSqlString.AppendFormat("    OR WB2_RES_ID {0} " + "\n", udcRASCondition6.SelectedValueToQueryString);
                strSqlString.AppendFormat("    OR WB3_RES_ID {0} " + "\n", udcRASCondition6.SelectedValueToQueryString);
                strSqlString.AppendFormat("    OR MD_RES_ID {0} )" + "\n", udcRASCondition6.SelectedValueToQueryString);
                strSqlString.AppendFormat("    OR MK_RES_ID {0} " + "\n", udcRASCondition6.SelectedValueToQueryString);
                strSqlString.AppendFormat("    OR BM_RES_ID {0} " + "\n", udcRASCondition6.SelectedValueToQueryString);
                strSqlString.AppendFormat("    OR PS_RES_ID {0} " + "\n", udcRASCondition6.SelectedValueToQueryString);
                strSqlString.AppendFormat("    OR TR_RES_ID {0} " + "\n", udcRASCondition6.SelectedValueToQueryString);
                strSqlString.AppendFormat("    OR FORM_RES_ID {0} " + "\n", udcRASCondition6.SelectedValueToQueryString);
                strSqlString.AppendFormat("    OR AV_RES_ID {0} " + "\n", udcRASCondition6.SelectedValueToQueryString);
            }

            #endregion
            strSqlString.Append("   AND A.WORK_DATE = B.SYS_DATE " + "\n");
            strSqlString.Append("   AND B.CALENDAR_ID = 'HM' " + "\n");
            strSqlString.Append("   AND WORK_DATE >='" + cdvFromToDate.FromDate.Text.Replace("-", "") + "' " + "\n");
            strSqlString.Append("   AND WORK_DATE <='" + cdvFromToDate.ToDate.Text.Replace("-", "") + "' " + "\n");
            strSqlString.Append("   AND DC_YLD <= " + txtYield.Text + "\n");
           
            if (cdvMat.Text.Contains("PB"))
            {
                strSqlString.Append("   AND PCB_VEN_NM <> ' ' " + "\n");
            }
            if (cdvMat.Text.Contains("LF"))
            {
                strSqlString.Append("   AND LF_VEN_NM <> ' ' " + "\n");
            }
            if (cdvMat.Text.Contains("MC"))
            {
                strSqlString.Append("   AND EMC_VEN_NM <> ' ' " + "\n");
            }
            if (cdvMat.Text.Contains("AE"))
            {
                strSqlString.Append("   AND EPX_VEN_NM <> ' ' " + "\n");
            }
            if (cdvMat.Text.Contains("GW"))
            {
                strSqlString.Append("   AND GW_VEN_NM <> ' ' " + "\n");
            }
            if (cdvMat.Text.Contains("SB"))
            {
                strSqlString.Append("   AND SB_VEN_NM <> ' ' " + "\n");
            }

            strSqlString.Append(" ORDER BY ");

            if (cdvMat.Text.Contains("PB"))
            {
                strSqlString.Append("PCB_VEN_NM");
            }
            if (cdvMat.Text.Contains("LF"))
            {
                if (cdvMat.Text.Substring(0, 2).Equals("LF"))
                {
                    strSqlString.Append("LF_VEN_NM");
                }
                else
                {
                    strSqlString.Append("||'-'||LF_VEN_NM");
                }
            }
            if (cdvMat.Text.Contains("MC"))
            {
                if (cdvMat.Text.Substring(0, 2).Equals("MC"))
                {
                    strSqlString.Append("EMC_VEN_NM");
                }
                else
                {
                    strSqlString.Append("||'-'||EMC_VEN_NM");
                }
            }
            if (cdvMat.Text.Contains("AE"))
            {
                if (cdvMat.Text.Substring(0, 2).Equals("AE"))
                {
                    strSqlString.Append("EPX_VEN_NM");
                }
                else
                {
                    strSqlString.Append("||'-'||EPX_VEN_NM");
                }
            }
            if (cdvMat.Text.Contains("GW"))
            {
                if (cdvMat.Text.Substring(0, 2).Equals("GW"))
                {
                    strSqlString.Append("GW_VEN_NM");
                }
                else
                {
                    strSqlString.Append("||'-'||GW_VEN_NM");
                }
            }
            if (cdvMat.Text.Contains("SB"))
            {
                if (cdvMat.Text.Substring(0, 2).Equals("SB"))
                {
                    strSqlString.Append("SB_VEN_NM");
                }
                else
                {
                    strSqlString.Append("||'-'||SB_VEN_NM");
                }
            }
            strSqlString.Append(")" + "\n");
          
            if (GlobalVariable.gsUserGroup == "ADMIN_GROUP" || GlobalVariable.gsUserGroup == "HANA_ADMIN_GROUP")
            {
                System.Windows.Forms.Clipboard.SetText(strSqlString.ToString());
            }

            return strSqlString.ToString();
        }




        #endregion


        #region EVENT 처리
        /// <summary>
        /// 6. View 버튼 Action
        /// </summary>        ///         
        private void btnView_Click(object sender, EventArgs e)
        {
            DataTable dt = null;

            if (CheckField() == false) return;

            GridColumnInit();
            spdData_Sheet1.RowCount = 0;

            try
            {
                LoadingPopUp.LoadIngPopUpShow(this);

                dt = CmnFunction.oComm.GetFuncDataTable("DYNAMIC", MakeSqlString());

                if (dt.Rows.Count == 0)
                {
                    dt.Dispose();
                    udcChartFX1.RPT_1_ChartInit();
                    udcChartFX1.RPT_2_ClearData();
                    LoadingPopUp.LoadingPopUpHidden();
                    CmnFunction.ShowMsgBox(RptMessages.GetMessage("STD010", GlobalVariable.gcLanguage));
                    return;
                }

                //그루핑 순서 1순위만 SubTotal을 사용하기 위해서 첫번째 값을 가져옴
                int sub = ((udcTableForm)(this.btnSort.BindingForm)).GetCheckedValue(2);
                //1.Griid 합계 표시
                int[] rowType = spdData.RPT_DataBindingWithSubTotal(dt, 0, 0, 9, null, null, btnSort);

                //2. 칼럼 고정(필요하다면..)
                spdData.Sheets[0].FrozenColumnCount = 11;

                //3. Total부분 셀머지
                spdData.RPT_FillDataSelectiveCells("Total", 0, 11, 0, 1, true, Align.Center, VerticalAlign.Center);

                spdData.ActiveSheet.Cells[0, 104].Value = (Convert.ToDouble(spdData.ActiveSheet.Cells[0, 103].Value) / Convert.ToDouble(spdData.ActiveSheet.Cells[0, 102].Value)) * 100;
                spdData.ActiveSheet.Cells[0, 111].Value = (Convert.ToDouble(spdData.ActiveSheet.Cells[0, 107].Value) / Convert.ToDouble(spdData.ActiveSheet.Cells[0, 106].Value)) * 100;
                
                spdData.RPT_AutoFit(false);

                dt.Dispose();
                
                // DC YIELD 최소값 가져오기
                MakeSqlString2();
                
                //Chart 생성
                if (spdData.ActiveSheet.RowCount > 0)
                {
                    if (cdvMat.Text == "ALL" || cdvMat.Text == "")
                    {
                        if (cbColor.Checked == false)
                        {
                            fnMakeChart(spdData, spdData.ActiveSheet.RowCount);
                        }
                        else
                        {
                            fnMakeChart2();
                        }
                    }
                    else //원부자재 차트
                    {
                        fnMakeChart3();
                    }
                        
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

        private string MakeSqlString2()
        {
            StringBuilder strSqlString = new StringBuilder();
           

            strSqlString.Append("      SELECT TRUNC(MIN(DC_YLD))" + "\n");
            strSqlString.Append("        FROM RSUMDCLHIS A, MWIPCALDEF B" + "\n");
            strSqlString.Append("       WHERE 1=1 " + "\n");

            #region 상세 조회에 따른 SQL문 생성
            if (udcWIPCondition1.Text != "ALL" && udcWIPCondition1.Text != "")
                //strSqlString.AppendFormat("   AND CUSTOMER {0} " + "\n", udcWIPCondition1.SelectedValueToQueryString);

            if (udcWIPCondition2.Text != "ALL" && udcWIPCondition2.Text != "")
                    strSqlString.AppendFormat("   AND FAMILY {0} " + "\n", udcWIPCondition2.SelectedValueToQueryString);

            if (udcWIPCondition3.Text != "ALL" && udcWIPCondition3.Text != "")
                strSqlString.AppendFormat("   AND PKG {0} " + "\n", udcWIPCondition3.SelectedValueToQueryString);

            if (udcWIPCondition4.Text != "ALL" && udcWIPCondition4.Text != "")
                strSqlString.AppendFormat("   AND TYPE1 {0} " + "\n", udcWIPCondition4.SelectedValueToQueryString);

            if (udcWIPCondition5.Text != "ALL" && udcWIPCondition5.Text != "")
                strSqlString.AppendFormat("   AND TYPE2 {0} " + "\n", udcWIPCondition5.SelectedValueToQueryString);

            if (udcWIPCondition6.Text != "ALL" && udcWIPCondition6.Text != "")
                strSqlString.AppendFormat("   AND LD_COUNT {0} " + "\n", udcWIPCondition6.SelectedValueToQueryString);

            if (udcWIPCondition7.Text != "ALL" && udcWIPCondition7.Text != "")
                strSqlString.AppendFormat("   AND DENSITY {0} " + "\n", udcWIPCondition7.SelectedValueToQueryString);

            if (udcWIPCondition8.Text != "ALL" && udcWIPCondition8.Text != "")
                strSqlString.AppendFormat("   AND GENERATION {0} " + "\n", udcWIPCondition8.SelectedValueToQueryString);

            if (udcRASCondition6.Text != "ALL" && udcRASCondition6.Text != "")
            {
                strSqlString.AppendFormat("   AND (BG_RES_ID {0} " + "\n", udcRASCondition6.SelectedValueToQueryString);
                strSqlString.AppendFormat("    OR SAW_RES_ID {0} " + "\n", udcRASCondition6.SelectedValueToQueryString);
                strSqlString.AppendFormat("    OR DA1_RES_ID {0} " + "\n", udcRASCondition6.SelectedValueToQueryString);
                strSqlString.AppendFormat("    OR DA2_RES_ID {0} " + "\n", udcRASCondition6.SelectedValueToQueryString);
                strSqlString.AppendFormat("    OR DA3_RES_ID {0} " + "\n", udcRASCondition6.SelectedValueToQueryString);
                strSqlString.AppendFormat("    OR WB1_RES_ID {0} " + "\n", udcRASCondition6.SelectedValueToQueryString);
                strSqlString.AppendFormat("    OR WB2_RES_ID {0} " + "\n", udcRASCondition6.SelectedValueToQueryString);
                strSqlString.AppendFormat("    OR WB3_RES_ID {0} " + "\n", udcRASCondition6.SelectedValueToQueryString);
                strSqlString.AppendFormat("    OR MD_RES_ID {0} )" + "\n", udcRASCondition6.SelectedValueToQueryString);
                strSqlString.AppendFormat("    OR MK_RES_ID {0} " + "\n", udcRASCondition6.SelectedValueToQueryString);
                strSqlString.AppendFormat("    OR BM_RES_ID {0} " + "\n", udcRASCondition6.SelectedValueToQueryString);
                strSqlString.AppendFormat("    OR PS_RES_ID {0} " + "\n", udcRASCondition6.SelectedValueToQueryString);
                strSqlString.AppendFormat("    OR TR_RES_ID {0} " + "\n", udcRASCondition6.SelectedValueToQueryString);
                strSqlString.AppendFormat("    OR FORM_RES_ID {0} " + "\n", udcRASCondition6.SelectedValueToQueryString);
                strSqlString.AppendFormat("    OR AV_RES_ID {0} " + "\n", udcRASCondition6.SelectedValueToQueryString);
            }

            #endregion

            if (cdvCustomer.Text != "" && cdvCustomer.Text != "ALL")
            {
                strSqlString.Append("         AND CUSTOMER = '" + cdvCustomer.Text + "' " + "\n");
            }
            strSqlString.Append("         AND LOT_ID LIKE '" + txtLotID.Text + "' " + "\n");
            strSqlString.Append("         AND MAT_ID LIKE '" + txtSearchProduct.Text + "' " + "\n");
            strSqlString.Append("         AND A.WORK_DATE = B.SYS_DATE " + "\n");
            strSqlString.Append("         AND B.CALENDAR_ID = 'HM' " + "\n");

            if (cdvFromToDate.DaySelector.SelectedValue.ToString() == "MONTH")
            {
                strSqlString.Append("           AND WORK_MONTH BETWEEN '" + cdvFromToDate.FromYearMonth.Value.ToString("yyyyMM") + "' AND '" + cdvFromToDate.ToYearMonth.Value.ToString("yyyyMM") + "'" + "\n");
            }
            else if (cdvFromToDate.DaySelector.SelectedValue.ToString() == "DAY")
            {
                strSqlString.Append("         AND WORK_DATE >='" + cdvFromToDate.FromDate.Text.Replace("-", "") + "' " + "\n");
                strSqlString.Append("         AND WORK_DATE <='" + cdvFromToDate.ToDate.Text.Replace("-", "") + "' " + "\n");
            }
            else
            {
                strSqlString.Append("           AND B.SYS_YEAR||B.PLAN_WEEK BETWEEN '" + cdvFromToDate.HmFromWeek + "' AND '" + cdvFromToDate.HmToWeek + "'" + "\n");
            }
            strSqlString.Append("   AND DC_YLD <= " + txtYield.Text + "\n");

            DataTable dt = null;
            dt = CmnFunction.oComm.GetFuncDataTable("DYNAMIC", strSqlString.ToString());

            minYld = dt.Rows[0][0].ToString();
            return minYld;
        }

        private void fnMakeChart(Miracom.SmartWeb.UI.Controls.udcFarPoint SS, int iselrow)
        {
            /****************************************************
             * Comment : SS의 데이터를 Chart에 표시한다.
             * 
             * Created By : min-woo kim(2010-10-05-화요일)
             * 
             * Modified By : min-woo kim(2010-10-05-화요일)
             ****************************************************/
            int[] ich_mm = new int[iselrow]; int[] icols_mm = new int[iselrow]; int[] irows_mm = new int[iselrow-1]; string[] stitle_mm = new string[iselrow];

            int icol = 0, irow = 0;
            try
            {
                Cursor.Current = Cursors.WaitCursor;

                if (SS.Sheets[0].RowCount <= 0)
                {
                    return;
                }

                udcChartFX1.RPT_1_ChartInit();
                udcChartFX1.RPT_2_ClearData();
                udcChartFX1.AxisY.Font = new System.Drawing.Font("Tahoma", 8.25F);
                udcChartFX1.AxisX.Font = new System.Drawing.Font("Tahoma", 8.25F);
                udcChartFX1.AxisX.Title.Text = "DC TREND";
                udcChartFX1.AxisX.Title.Font = new System.Drawing.Font("Tahoma", 13.25F);
                udcChartFX1.AxisY.Title.Text = "yield";

                udcChartFX1.AxisX.Staggered = false;
                for (icol = 0; icol < ich_mm.Length; icol++)
                {
                    ich_mm[icol] = icol +1;
                    icols_mm[icol] = icol +1;
                }
                for (irow = 0; irow < irows_mm.Length; irow++)
                {
                    irows_mm[irow] = irow +1;
                    //stitle_mm[irow] = SS.Sheets[0].Cells[irow, 0].Text;
                }

                udcChartFX1.RPT_3_OpenData(1, icols_mm.Length-1);
                double max1 = udcChartFX1.RPT_4_AddData(SS, irows_mm, new int[] { 111 }, SeriseType.Column);
                //double max1 = udcChartFX1.RPT_4_AddData(spdData, new int[] { 2 }, icols_mm, SeriseType.Rows);
                udcChartFX1.RPT_5_CloseData();
                //udcChartFX1.RPT_6_SetGallery(ChartType.Line, 0, 1, "", AsixType.Y, DataTypes.Double2, 100);
                //udcChartFX1.RPT_7_SetXAsixTitleUsingSpreadHeader(SS, 0, 1);
                udcChartFX1.RPT_7_SetXAsixTitleUsingSpread(SS, irows_mm, 112);
                //udcChartFX1.RPT_8_SetSeriseLegend(stitle_mm, SoftwareFX.ChartFX.Docked.Bottom);
              
                udcChartFX1.Gallery = SoftwareFX.ChartFX.Gallery.Scatter;
                udcChartFX1.MarkerShape = SoftwareFX.ChartFX.MarkerShape.Circle;
                udcChartFX1.MarkerSize = 1;
                udcChartFX1.AxisY.Max = Convert.ToDouble(txtYield.Text);
                udcChartFX1.AxisY.Min = Convert.ToDouble(minYld); //Convert.ToDouble(txtYield.Text) - 1;
                udcChartFX1.AxisY.Step = 0.1;
                udcChartFX1.AxisY.LabelsFormat.Decimals = 2;
            }
            catch (Exception ex)
            {
                CmnFunction.ShowMsgBox(ex.Message);
                return;
            }
            finally
            {
                Cursor.Current = Cursors.Default;
            }
        }
        //설비별로 구분지어 표시되는 차트
        private void fnMakeChart2()
        {
            /****************************************************
             * Comment : DC YIELD(Scatter 차트 이용)
             * 
             * Created By : min-woo kim(2012-05-07)
             * 
             * Modified By : min-woo kim(2012-05-07)
             ****************************************************/

            try
            {

                udcChartFX1.RPT_1_ChartInit();
                udcChartFX1.RPT_2_ClearData();
                udcChartFX1.AxisY.Font = new System.Drawing.Font("Tahoma", 8.25F);
                udcChartFX1.AxisX.Font = new System.Drawing.Font("Tahoma", 5.25F);
                udcChartFX1.AxisX.Title.Text = "DC TREND";
                udcChartFX1.AxisX.Title.Font = new System.Drawing.Font("Tahoma", 13.25F);
                udcChartFX1.AxisY.Title.Text = "yield";
                DataTable dtChart = CmnFunction.oComm.GetFuncDataTable("DYNAMIC", MakeSqlStringRes());
                DataTable dtCountByRes = CmnFunction.oComm.GetFuncDataTable("DYNAMIC", MakeCount());
                int cnt = dtChart.Rows.Count;
                udcChartFX1.Gallery = SoftwareFX.ChartFX.Gallery.Scatter;
                udcChartFX1.OpenData(SoftwareFX.ChartFX.COD.Values, 1, 1);
                //쿼리 돌려보면 이해하기 빠름..                
                for (int k = 0; k < dtCountByRes.Rows.Count; k++)
                {
                    udcChartFX1.SerLeg[k] = dtCountByRes.Rows[k][1].ToString();
                    for (int i = 0; i < dtChart.Rows.Count; i++)
                    {
                        udcChartFX1.Legend[i] = dtChart.Rows[i][4].ToString();
                        //해당 설비면 YIELD 값을 넣고
                        if (dtChart.Rows[i][2].ToString().Equals(dtCountByRes.Rows[k][1].ToString()))
                        {
                            udcChartFX1.Value[Convert.ToInt16(dtCountByRes.Rows[k][0].ToString()), i] = Convert.ToDouble(dtChart.Rows[i][3].ToString());
                        }
                        // 아니면 0을 넣어 시리즈별 컬럼 수 맞춤(0을 넣어 차트상에는 안보이게 함)
                        else
                        {
                            udcChartFX1.Value[Convert.ToInt16(dtCountByRes.Rows[k][0].ToString()), i] = 0;
                        }
                    }
                }

                udcChartFX1.Gallery = SoftwareFX.ChartFX.Gallery.Scatter;
                udcChartFX1.MarkerShape = SoftwareFX.ChartFX.MarkerShape.Circle;
                udcChartFX1.MarkerSize = 2;
                udcChartFX1.AxisY.Max = Convert.ToDouble(txtYield.Text);
                udcChartFX1.AxisY.Min = Convert.ToDouble(minYld); 
                udcChartFX1.AxisY.Step = 0.1;
                udcChartFX1.AxisY.LabelsFormat.Decimals = 2;
                udcChartFX1.SerLegBox = true;
                udcChartFX1.SerLegBoxObj.SizeToFit();
            }
            catch (Exception ex)
            {
                CmnFunction.ShowMsgBox(ex.Message);
                return;
            }
            finally
            {
                Cursor.Current = Cursors.Default;
            }
        }

        //원부자재별로 구분지어 표시되는 차트
        private void fnMakeChart3()
        {
            /****************************************************
             * Comment : DC YIELD(Scatter 차트 이용)
             * 
             * Created By : min-woo kim(2013-03-18)
             * 
             * Modified By : min-woo kim(2013-03-18)
             ****************************************************/

            try
            {

                udcChartFX1.RPT_1_ChartInit();
                udcChartFX1.RPT_2_ClearData();
                udcChartFX1.AxisY.Font = new System.Drawing.Font("Tahoma", 8.25F);
                udcChartFX1.AxisX.Font = new System.Drawing.Font("Tahoma", 5.25F);
                udcChartFX1.AxisX.Title.Text = "DC TREND";
                udcChartFX1.AxisX.Title.Font = new System.Drawing.Font("Tahoma", 13.25F);
                udcChartFX1.AxisY.Title.Text = "yield";
                DataTable dtChart = CmnFunction.oComm.GetFuncDataTable("DYNAMIC", MakeSqlStringMat());
                DataTable dtCountByMat = CmnFunction.oComm.GetFuncDataTable("DYNAMIC", MakeMatCount());
                int cnt = dtChart.Rows.Count;
                udcChartFX1.Gallery = SoftwareFX.ChartFX.Gallery.Scatter;
                udcChartFX1.OpenData(SoftwareFX.ChartFX.COD.Values, 1, 1);
                //쿼리 돌려보면 이해하기 빠름..                
                for (int k = 0; k < dtCountByMat.Rows.Count; k++)
                {
                    udcChartFX1.SerLeg[k] = dtCountByMat.Rows[k][1].ToString();
                    for (int i = 0; i < dtChart.Rows.Count; i++)
                    {
                        udcChartFX1.Legend[i] = dtChart.Rows[i][4].ToString();
                        //해당 설비면 YIELD 값을 넣고
                        if (dtChart.Rows[i][2].ToString().Equals(dtCountByMat.Rows[k][1].ToString()))
                        {
                            udcChartFX1.Value[Convert.ToInt16(dtCountByMat.Rows[k][0].ToString()), i] = Convert.ToDouble(dtChart.Rows[i][3].ToString());
                        }
                        // 아니면 0을 넣어 시리즈별 컬럼 수 맞춤(0을 넣어 차트상에는 안보이게 함)
                        else
                        {
                            udcChartFX1.Value[Convert.ToInt16(dtCountByMat.Rows[k][0].ToString()), i] = 0;
                        }
                    }
                }

                udcChartFX1.Gallery = SoftwareFX.ChartFX.Gallery.Scatter;
                udcChartFX1.MarkerShape = SoftwareFX.ChartFX.MarkerShape.Circle;
                udcChartFX1.MarkerSize = 2;
                udcChartFX1.AxisY.Max = Convert.ToDouble(txtYield.Text);
                udcChartFX1.AxisY.Min = Convert.ToDouble(minYld);
                udcChartFX1.AxisY.Step = 0.1;
                udcChartFX1.AxisY.LabelsFormat.Decimals = 2;
                udcChartFX1.SerLegBox = true;
                udcChartFX1.SerLegBoxObj.SizeToFit();
            }
            catch (Exception ex)
            {
                CmnFunction.ShowMsgBox(ex.Message);
                return;
            }
            finally
            {
                Cursor.Current = Cursors.Default;
            }
        }

        /// <summary>
        /// Excel Export
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnExcelExport_Click(object sender, EventArgs e)
        {
            if (spdData.ActiveSheet.Rows.Count > 0)
            {
                StringBuilder Condition = new StringBuilder();
                ExcelHelper.Instance.subMakeExcel(spdData, this.lblTitle.Text, Condition.ToString(), null, true);
                //spdData.ExportExcel();            
            }
        }

        // 공정 가져오기
        private void cdvOper_ButtonPress(object sender, EventArgs e)
        {
            string strQuery = string.Empty;

            strQuery += "SELECT CODE, DATA FROM (" + "\n";
            strQuery += "                        SELECT 'ALL' AS CODE , 'ALL' AS DATA FROM DUAL" + "\n";
            strQuery += "                        UNION ALL" + "\n";
            strQuery += "                        SELECT OPER AS Code, OPER_DESC AS Data FROM MWIPOPRDEF@RPTTOMES" + "\n";
            strQuery += "                         WHERE OPER IN ('A0040','A0200','A0400','A0401','A0402','A0600','A0601','A0602','A1000','A1150','A1300','A1750','A1200','A1800','A2000') " + "\n";
            strQuery += "                       ) " + "\n";
            strQuery += " ORDER BY DECODE(CODE,'ALL',1,2), CODE " + "\n";
            cdvOper.sDynamicQuery = strQuery;
        }

        /// <summary>
        /// Factory 설정
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cdvFactory_ValueSelectedItemChanged(object sender, Miracom.UI.MCCodeViewSelChanged_EventArgs e)
        {
            this.SetFactory(cdvFactory.txtValue);
            cdvOper.sFactory = cdvFactory.txtValue;
        }

        // 2013-03-13 김민우: 원자재 조회

        private void cdvMat_ValueButtonPress(object sender, EventArgs e)
        {
            string strQuery = string.Empty;
         
            strQuery += "SELECT 'PB' AS Code, 'PCB' AS Data FROM DUAL" + "\n";
            strQuery += "UNION ALL" + "\n";
            strQuery += "SELECT 'LF' AS Code, 'Lead Frame' AS Data FROM DUAL" + "\n";
            strQuery += "UNION ALL" + "\n";
            strQuery += "SELECT 'MC' AS Code, 'EMC' AS Data FROM DUAL" + "\n";
            strQuery += "UNION ALL" + "\n";
            strQuery += "SELECT 'AE' AS Code, 'Epoxy' AS Data FROM DUAL" + "\n";
            strQuery += "UNION ALL" + "\n";
            strQuery += "SELECT 'GW' AS Code, 'Gold Wire' AS Data FROM DUAL" + "\n";
            strQuery += "UNION ALL" + "\n";
            strQuery += "SELECT 'SB' AS Code, 'Soler Ball' AS Data FROM DUAL" + "\n";
      
            cdvMat.sDynamicQuery = strQuery;
        }

        #endregion
    }
}
        