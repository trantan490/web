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
    public partial class PRD010205 : Miracom.SmartWeb.UI.Controls.udcCUSReport002
    {
        GlobalVariable.FindWeek FindWeek = new GlobalVariable.FindWeek();

        string[] dayArry = new string[3];
        string[] dayArry2 = new string[3];
        decimal jindoPer;

        /// <summary>
        /// Ŭ  ��  ��: PRD010205<br/>
        /// Ŭ�������: TEST ���� ���� ��Ȳ<br/>
        /// ��  ��  ��: ������<br/>
        /// �����ۼ���: 2009-10-07<br/>
        /// ��  ����: TEST ���� ���� ��Ȳ<br/>
        /// ����  ����: <br/>    
        /// 2011-10-15-��ο� : ����ȹ Rev�߰�. (���¼� ��û)
        /// 2011-11-02-��ο� : ��ü �� Other�� ���� ��ü���� ��� �ɰ��� ���� �� ��(���¼� ��û)
        /// 2011-11-02-��ο� : ���� ���̺а� ���� �����ǥ ������ �ʰ� �ϱ�(���¼� ��û)
        /// 2011-11-02-��ο� : �޼��� (LIGIC: �������/�����ǥ)--> ������ ����(LIGIC: �������/����ȹRev) (���¼� ��û)
        /// 2011-11-02-��ο� : �����Ȳ���� Stock ����� Part �� ��ǰ�׷�(1~10) Family ������ '-' ���� �� �ִ� ���� ���ܽ��� ��� (���¼� ��û)
        /// 2011-11-09-��ο� : ���� ���� �ڵ尡 �ƴ϶� ��Ī���� ǥ�� (���¼� ��û)
        /// 2011-11-21-������ : �� ��ȹ ��ǰ �ߺ� ���� ����
        /// 2011-12-26-������ : MWIPCALDEF �� �۳�,���� ������ ���� ��ġ�� ���� �߻����� SYS_YEAR -> PLAN_YEAR ���� ����
        /// 2012-01-17-��ο� : TEST IN/OUT ���� Logic�� ���� (TEST IN / OUT ���� T0100 ���θ� ���� --> T0100,T0400 �հ�� ����) (���¼� ��û)
        /// 2013-06-07-������ : SE ��ǰ �� ��ȹ REV ���� ���� OMS �����Ϳ��� �� ��ȹ UPLOAD �����ͷ� ���� �� (������ ��û)
        /// 2013-06-10-������ : HAVING ���� ����κ� NVL ó�� �ȵ� �κ� ����.
        /// 2013-07-31-��ο� : DETAIL ���� ��� Ȱ��ȭ �� Part no �� ��ȸ�� �� �ִ� ��� �߰�
        /// 2013-09-02-������ : HMKE ��ȹ, ���� ǥ�� �ǵ��� ���� (�ڹ��� ��û)
        /// 2013-10-14-��ο� : LOT TYPE ALL, P%, E% �������� ����
        /// 2014-03-01-������ : Kpcs ���� ��ȸ�� Remain Day�� 0���ΰ�� �����κ� ����
        /// 2014-04-21-������ : ��ǰ�԰� �κ� �� ���� �������� ���� VSUMWIPOUT ���̺� ��� (���¼� ��û)
        /// 2014-07-15-������ : �ְ���ȹ / �ְ����� / �ְ��ܷ� / �ְ� A/O (���¼� ��û)
        /// 2014-10-01-������ : TEST ����, �ְ� ���� �߰� (��Ǽ�D ��û)
        ///                   : 3 Day ������ ���� �����Ͽ� ǥ�� �� �� (���¼�K ��û)
        /// 2015-03-18-������ : D0, D1 ��ȹ ǥ�� (�ֿ���D ��û)
        /// 2015-09-21-������ : ���� �� �ϵ� �ڵ� �Ǿ� �ִ°��� ���������� ���� (���¼�K ��û)
        /// 2015-12-03-������ : Group ���ǿ� Product �߰� (���¼�K ��û)
        /// 2016-09-06-������ : HMKT, HMKE Factory �и� (���¼�K ��û)
        /// 2016-09-22-������ : FGS ��� ���� ������ ��ȸ ���� ����
        /// 2016-09-26-������ : TEST �Ϸ� ���� ��� ��Ȳ �߰� (���γ��� ��û)
        /// 2016-09-27-������ : TR 3�� ����, FGS SHIP ������ �߰� (���γ��� ��û)
        /// 2018-01-22-������ : SubTotal, GrandTotal ����� ���ϱ� Function ����
        /// 2020-03-03-��̰� : �Ｚ S-LSI ���� �Ʒ��� ���� ����. (�̽��� D)
        ///  [TEST IN ����]     Case 1. T0100, T0400, T0500, T0960, S0960 IN��
        ///  [TEST IN ����]     Case 2. T0100+T0960 ������ ���� ��� T0100 IN��
        ///  [TEST IN ����]     Case 3. T0500+T0960 ������ ���� ��� T0500 IN�� 
        ///  [TEST OUT ����]    Case 1. T0100, T0400, T0500, T0960, S0960 OUT��
        ///  [TEST OUT ����]    Case 2. T0100+T0960 ������ ���� ��� T0960 OUT��
        ///  [TEST OUT ����]    Case 3. T0500+T0960 ������ ���� ��� T0960 OUT�� 
        ///  2020-04-08-��̰� : HMKE1 ����ȹ������ �ʴ� �κ� ���� (�̵��� D)
        /// </summary>
        public PRD010205()
        {
            InitializeComponent();                                   
            SortInit();
            cdvDate.Value = DateTime.Now;
            GridColumnInit();            
            this.SetFactory(GlobalVariable.gsTestDefaultFactory);
            this.cdvFactory.sFactory = GlobalVariable.gsAssyDefaultFactory;
            cdvFactory.Text = GlobalVariable.gsTestDefaultFactory;    
        }

        #region �ʱ�ȭ �� ��ȿ�� �˻�
        /// <summary 1. ��ȿ�� �˻�>
        /// <remarks></remarks>
        /// </summary>
        /// <returns></returns>
        private Boolean CheckField()
        {
            //if (cdvFactory.Text.TrimEnd() == "")
            //{
            //    CmnFunction.ShowMsgBox(RptMessages.GetMessage("STD001", GlobalVariable.gcLanguage));
            //    return false;
            //}

            return true;
        }

        /// <summary>
        /// 2. ��� ����
        /// </summary>
        private void GridColumnInit()
        {
            spdData.RPT_ColumnInit();

            LabelTextChange();

            try
            {
                if (ckbKpcs.Checked == false)
                {
                    spdData.RPT_AddBasicColumn("CUSTOMER", 0, 0, Visibles.True, Frozen.True, Align.Center, Merge.True, Formatter.String, 70);
                    spdData.RPT_AddBasicColumn("MAJOR CODE", 0, 1, Visibles.True, Frozen.True, Align.Center, Merge.True, Formatter.String, 70);
                    spdData.RPT_AddBasicColumn("PACKAGE", 0, 2, Visibles.True, Frozen.True, Align.Center, Merge.True, Formatter.String, 70);
                    spdData.RPT_AddBasicColumn("TYPE1", 0, 3, Visibles.False, Frozen.True, Align.Center, Merge.True, Formatter.String, 50);
                    spdData.RPT_AddBasicColumn("LD COUNT", 0, 4, Visibles.False, Frozen.True, Align.Center, Merge.True, Formatter.String, 50);
                    spdData.RPT_AddBasicColumn("PKG CODE", 0, 5, Visibles.False, Frozen.True, Align.Center, Merge.True, Formatter.String, 50);
                    spdData.RPT_AddBasicColumn("PRODUCT", 0, 6, Visibles.False, Frozen.True, Align.Left, Merge.True, Formatter.String, 180);
                    spdData.RPT_AddBasicColumn(" ", 0, 7, Visibles.False, Frozen.True, Align.Center, Merge.True, Formatter.String, 50);
                    spdData.RPT_AddBasicColumn(" ", 0, 8, Visibles.False, Frozen.True, Align.Left, Merge.True, Formatter.String, 70);
                    spdData.RPT_AddBasicColumn(" ", 0, 9, Visibles.False, Frozen.True, Align.Left, Merge.True, Formatter.String, 70);
                    spdData.RPT_AddBasicColumn(" ", 0, 10, Visibles.False, Frozen.True, Align.Left, Merge.True, Formatter.String, 70);

                    if (ckbRev.Checked == false)
                    {
                        spdData.RPT_AddBasicColumn("Monthly plan", 0, 11, Visibles.True, Frozen.True, Align.Right, Merge.False, Formatter.Number, 70);
                        spdData.RPT_AddBasicColumn("Monthly Plan Rev", 0, 12, Visibles.False, Frozen.False, Align.Right, Merge.False, Formatter.Number, 70);
                        spdData.RPT_AddBasicColumn("Monthly plan difference", 0, 13, Visibles.False, Frozen.False, Align.Right, Merge.False, Formatter.Number, 70);
                    }
                    else
                    {
                        spdData.RPT_AddBasicColumn("Monthly plan", 0, 11, Visibles.True, Frozen.True, Align.Right, Merge.False, Formatter.Number, 70);
                        spdData.RPT_AddBasicColumn("Monthly Plan Rev", 0, 12, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 70);
                        spdData.RPT_AddBasicColumn("Monthly plan difference", 0, 13, Visibles.False, Frozen.False, Align.Right, Merge.False, Formatter.Number, 70);
                    }

                    spdData.RPT_AddBasicColumn("goal", 0, 14, Visibles.False, Frozen.False, Align.Right, Merge.False, Formatter.Number, 70);
                    
                    spdData.RPT_AddBasicColumn("Production Status", 0, 15, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.String, 70);
                    spdData.RPT_AddBasicColumn("cumulative total", 1, 15, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.String, 70);
                    spdData.RPT_AddBasicColumn("actual", 2, 15, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 70);
                    spdData.RPT_AddBasicColumn("Progress rate (%)", 2, 16, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Double1, 90);
                    spdData.RPT_AddBasicColumn("Difference", 2, 17, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 70);
                    spdData.RPT_MerageHeaderColumnSpan(1, 15, 3);
                    spdData.RPT_MerageHeaderColumnSpan(0, 15, 3);

                    spdData.RPT_AddBasicColumn("a daily goal", 0, 18, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 70);

                    spdData.RPT_AddBasicColumn("Performance by Major Operation", 0, 19, Visibles.True, Frozen.False, Align.Center, Merge.False, Formatter.String, 70);
                    spdData.RPT_AddBasicColumn("Semi-manufactures", 1, 19, Visibles.True, Frozen.False, Align.Center, Merge.False, Formatter.Number, 70);
                    spdData.RPT_AddBasicColumn(dayArry[0], 2, 19, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 70);
                    spdData.RPT_AddBasicColumn(dayArry[1], 2, 20, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 70);
                    spdData.RPT_AddBasicColumn(dayArry[2], 2, 21, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 70);
                    spdData.RPT_MerageHeaderColumnSpan(1, 19, 3);

                    spdData.RPT_AddBasicColumn("Test-In", 1, 22, Visibles.True, Frozen.False, Align.Center, Merge.False, Formatter.String, 70);
                    spdData.RPT_AddBasicColumn(dayArry[0], 2, 22, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 70);
                    spdData.RPT_AddBasicColumn(dayArry[1], 2, 23, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 70);
                    spdData.RPT_AddBasicColumn(dayArry[2], 2, 24, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 70);
                    spdData.RPT_MerageHeaderColumnSpan(1, 22, 3);

                    spdData.RPT_AddBasicColumn("Test-Out", 1, 25, Visibles.True, Frozen.False, Align.Center, Merge.False, Formatter.String, 70);
                    spdData.RPT_AddBasicColumn(dayArry[0], 2, 25, Visibles.True, Frozen.False, Align.Center, Merge.False, Formatter.Number, 70);
                    spdData.RPT_AddBasicColumn(dayArry[1], 2, 26, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 70);
                    spdData.RPT_AddBasicColumn(dayArry[2], 2, 27, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 70);
                    spdData.RPT_MerageHeaderColumnSpan(1, 25, 3);

                    spdData.RPT_AddBasicColumn("Ship", 1, 28, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.String, 70);
                    spdData.RPT_AddBasicColumn(dayArry[0], 2, 28, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 70);
                    spdData.RPT_AddBasicColumn(dayArry[1], 2, 29, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 70);
                    spdData.RPT_AddBasicColumn(dayArry[2], 2, 30, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 70);
                    spdData.RPT_MerageHeaderColumnSpan(1, 28, 3);

                    spdData.RPT_MerageHeaderColumnSpan(0, 19, 12);

                    if (DateTime.Now.ToString("yyyyMMdd") == cdvDate.SelectedValue())
                    {
                        spdData.RPT_AddBasicColumn("�����Ȳ (" + DateTime.Now.ToString("yy.MM.dd HH:mm") + " ����)", 0, 31, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.String, 70);
                    }
                    else
                    {
                        spdData.RPT_AddBasicColumn("�����Ȳ (" + cdvDate.Value.ToString("yy.MM.dd") + " 22:00 ����)", 0, 31, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.String, 70);
                    }

                    spdData.RPT_AddBasicColumn("STOCK", 1, 31, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 70);
                    spdData.RPT_AddBasicColumn("TEST", 1, 32, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 70);
                    spdData.RPT_AddBasicColumn("HOLD", 1, 33, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 70);
                    spdData.RPT_AddBasicColumn("TEST(��)", 1, 34, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 70);
                    spdData.RPT_AddBasicColumn("HMK4T", 1, 35, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 70);
                    spdData.RPT_AddBasicColumn("TTL", 1, 36, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 70);
                    spdData.RPT_AddBasicColumn("FGS", 1, 37, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 70);
                    spdData.RPT_MerageHeaderColumnSpan(0, 31, 7);

                    spdData.RPT_AddBasicColumn("WW" + FindWeek.ThisWeek.Substring(4), 0, 38, Visibles.True, Frozen.False, Align.Center, Merge.False, Formatter.String, 70);
                    spdData.RPT_AddBasicColumn("plan", 1, 38, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 70);
                    spdData.RPT_AddBasicColumn("actual", 1, 39, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 70);
                    spdData.RPT_AddBasicColumn("residual quantity", 1, 40, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 70);
                    spdData.RPT_AddBasicColumn("A/O", 1, 41, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 70);
                    spdData.RPT_MerageHeaderColumnSpan(0, 38, 4);

                    spdData.RPT_AddBasicColumn("TEST cumulative performance", 0, 42, Visibles.True, Frozen.False, Align.Center, Merge.False, Formatter.String, 70);
                    spdData.RPT_AddBasicColumn("monthly", 1, 42, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 70);
                    spdData.RPT_AddBasicColumn("weekly", 1, 43, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 70);
                    spdData.RPT_MerageHeaderColumnSpan(0, 42, 2); 

                    spdData.RPT_AddBasicColumn("Daily plan", 0, 44, Visibles.True, Frozen.False, Align.Center, Merge.False, Formatter.String, 70);
                    spdData.RPT_AddBasicColumn("D0", 1, 44, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 70);
                    spdData.RPT_AddBasicColumn("D1", 1, 45, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 70);
                    spdData.RPT_MerageHeaderColumnSpan(0, 44, 2);

                    spdData.RPT_AddBasicColumn("TEST(completion) Details WIP", 0, 46, Visibles.True, Frozen.False, Align.Center, Merge.False, Formatter.String, 70);
                    spdData.RPT_AddBasicColumn("TR wait", 1, 46, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 70);
                    spdData.RPT_AddBasicColumn("TR ��", 1, 47, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 70);
                    spdData.RPT_AddBasicColumn("TR completed", 1, 48, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 70);
                    spdData.RPT_MerageHeaderColumnSpan(0, 46, 3);

                    spdData.RPT_AddBasicColumn("TR Performance", 0, 49, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.String, 70);
                    spdData.RPT_AddBasicColumn(dayArry[0], 1, 49, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 70);
                    spdData.RPT_AddBasicColumn(dayArry[1], 1, 50, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 70);
                    spdData.RPT_AddBasicColumn(dayArry[2], 1, 51, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 70);
                    spdData.RPT_MerageHeaderColumnSpan(0, 49, 3);

                    spdData.RPT_AddBasicColumn("FGS performance", 0, 52, Visibles.True, Frozen.False, Align.Center, Merge.False, Formatter.String, 70);
                    spdData.RPT_AddBasicColumn("monthly", 1, 52, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 70);
                    spdData.RPT_AddBasicColumn("daily", 1, 53, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 70);                    
                    spdData.RPT_MerageHeaderColumnSpan(0, 52, 2);
                }
                else
                {
                    spdData.RPT_AddBasicColumn("CUSTOMER", 0, 0, Visibles.True, Frozen.True, Align.Center, Merge.True, Formatter.String, 70);
                    spdData.RPT_AddBasicColumn("MAJOR CODE", 0, 1, Visibles.False, Frozen.True, Align.Center, Merge.True, Formatter.String, 70);
                    spdData.RPT_AddBasicColumn("PACKAGE", 0, 2, Visibles.True, Frozen.True, Align.Center, Merge.True, Formatter.String, 70);
                    spdData.RPT_AddBasicColumn("TYPE1", 0, 3, Visibles.False, Frozen.True, Align.Center, Merge.True, Formatter.String, 50);
                    spdData.RPT_AddBasicColumn("LD COUNT", 0, 4, Visibles.False, Frozen.True, Align.Center, Merge.True, Formatter.String, 50);
                    spdData.RPT_AddBasicColumn("PKG CODE", 0, 5, Visibles.False, Frozen.True, Align.Center, Merge.True, Formatter.String, 50);
                    spdData.RPT_AddBasicColumn("PRODUCT", 0, 6, Visibles.False, Frozen.True, Align.Left, Merge.True, Formatter.String, 150);
                    spdData.RPT_AddBasicColumn(" ", 0, 7, Visibles.False, Frozen.True, Align.Center, Merge.True, Formatter.String, 50);
                    spdData.RPT_AddBasicColumn(" ", 0, 8, Visibles.False, Frozen.True, Align.Left, Merge.True, Formatter.String, 70);
                    spdData.RPT_AddBasicColumn(" ", 0, 9, Visibles.False, Frozen.True, Align.Left, Merge.True, Formatter.String, 70);
                    spdData.RPT_AddBasicColumn(" ", 0, 10, Visibles.False, Frozen.True, Align.Left, Merge.True, Formatter.String, 70);

                    if (ckbRev.Checked == false)
                    {
                        spdData.RPT_AddBasicColumn("Monthly plan", 0, 11, Visibles.True, Frozen.True, Align.Right, Merge.False, Formatter.Double1, 70);
                        spdData.RPT_AddBasicColumn("Monthly Plan Rev", 0, 12, Visibles.False, Frozen.False, Align.Right, Merge.False, Formatter.Number, 70);
                        spdData.RPT_AddBasicColumn("Monthly plan difference", 0, 13, Visibles.False, Frozen.False, Align.Right, Merge.False, Formatter.Number, 70);
                    }
                    else
                    {
                        spdData.RPT_AddBasicColumn("Monthly plan", 0, 11, Visibles.True, Frozen.True, Align.Right, Merge.False, Formatter.Double1, 70);
                        spdData.RPT_AddBasicColumn("Monthly Plan Rev", 0, 12, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 70);
                        spdData.RPT_AddBasicColumn("Monthly plan difference", 0, 13, Visibles.False, Frozen.False, Align.Right, Merge.False, Formatter.Number, 70);
                    }
                   
                    spdData.RPT_AddBasicColumn("goal", 0, 14, Visibles.False, Frozen.False, Align.Right, Merge.False, Formatter.Double1, 70);

                    spdData.RPT_AddBasicColumn("Production Status", 0, 15, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.String, 70);
                    spdData.RPT_AddBasicColumn("cumulative total", 1, 15, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.String, 70);
                    spdData.RPT_AddBasicColumn("actual", 2, 15, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Double1, 70);
                    spdData.RPT_AddBasicColumn("Progress rate (%)", 2, 16, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Double1, 90);
                    spdData.RPT_AddBasicColumn("Difference", 2, 17, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Double1, 70);
                    spdData.RPT_MerageHeaderColumnSpan(1, 15, 3);
                    spdData.RPT_MerageHeaderColumnSpan(0, 15, 3);


                    spdData.RPT_AddBasicColumn("a daily goal", 0, 18, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Double1, 70);

                    spdData.RPT_AddBasicColumn("Performance by Major Operation", 0, 19, Visibles.True, Frozen.False, Align.Center, Merge.False, Formatter.String, 70);
                    spdData.RPT_AddBasicColumn("Semi-manufactures", 1, 19, Visibles.True, Frozen.False, Align.Center, Merge.False, Formatter.Number, 70);
                    spdData.RPT_AddBasicColumn(dayArry[0], 2, 19, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Double1, 70);
                    spdData.RPT_AddBasicColumn(dayArry[1], 2, 20, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Double1, 70);
                    spdData.RPT_AddBasicColumn(dayArry[2], 2, 21, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Double1, 70);
                    spdData.RPT_MerageHeaderColumnSpan(1, 19, 3);

                    spdData.RPT_AddBasicColumn("Test-In", 1, 22, Visibles.True, Frozen.False, Align.Center, Merge.False, Formatter.String, 70);
                    spdData.RPT_AddBasicColumn(dayArry[0], 2, 22, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Double1, 70);
                    spdData.RPT_AddBasicColumn(dayArry[1], 2, 23, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Double1, 70);
                    spdData.RPT_AddBasicColumn(dayArry[2], 2, 24, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Double1, 70);
                    spdData.RPT_MerageHeaderColumnSpan(1, 22, 3);

                    spdData.RPT_AddBasicColumn("Test-Out", 1, 25, Visibles.True, Frozen.False, Align.Center, Merge.False, Formatter.String, 70);
                    spdData.RPT_AddBasicColumn(dayArry[0], 2, 25, Visibles.True, Frozen.False, Align.Center, Merge.False, Formatter.Double1, 70);
                    spdData.RPT_AddBasicColumn(dayArry[1], 2, 26, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Double1, 70);
                    spdData.RPT_AddBasicColumn(dayArry[2], 2, 27, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Double1, 70);
                    spdData.RPT_MerageHeaderColumnSpan(1, 25, 3);

                    spdData.RPT_AddBasicColumn("Ship", 1, 28, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.String, 70);
                    spdData.RPT_AddBasicColumn(dayArry[0], 2, 28, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Double1, 70);
                    spdData.RPT_AddBasicColumn(dayArry[1], 2, 29, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Double1, 70);
                    spdData.RPT_AddBasicColumn(dayArry[2], 2, 30, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Double1, 70);
                    spdData.RPT_MerageHeaderColumnSpan(1, 28, 3);

                    spdData.RPT_MerageHeaderColumnSpan(0, 19, 12);

                    if (DateTime.Now.ToString("yyyyMMdd") == cdvDate.SelectedValue())
                    {
                        spdData.RPT_AddBasicColumn("�����Ȳ (" + DateTime.Now.ToString("yy.MM.dd HH:mm") + " ����)", 0, 31, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.String, 70);
                    }
                    else
                    {
                        spdData.RPT_AddBasicColumn("�����Ȳ (" + cdvDate.Value.ToString("yy.MM.dd") + " 22:00 ����)", 0, 31, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.String, 70);
                    }

                    spdData.RPT_AddBasicColumn("STOCK", 1, 31, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Double1, 70);
                    spdData.RPT_AddBasicColumn("TEST", 1, 32, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Double1, 70);
                    spdData.RPT_AddBasicColumn("HOLD", 1, 33, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Double1, 70);
                    spdData.RPT_AddBasicColumn("TEST(��)", 1, 34, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Double1, 70);
                    spdData.RPT_AddBasicColumn("HMK4T", 1, 35, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 70);
                    spdData.RPT_AddBasicColumn("TTL", 1, 36, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Double1, 70);
                    spdData.RPT_AddBasicColumn("FGS", 1, 37, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Double1, 70);
                    spdData.RPT_MerageHeaderColumnSpan(0, 31, 7);

                    spdData.RPT_AddBasicColumn("WW" + FindWeek.ThisWeek.Substring(4), 0, 38, Visibles.True, Frozen.False, Align.Center, Merge.False, Formatter.String, 70);
                    spdData.RPT_AddBasicColumn("plan", 1, 38, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Double1, 70);
                    spdData.RPT_AddBasicColumn("actual", 1, 39, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Double1, 70);
                    spdData.RPT_AddBasicColumn("residual quantity", 1, 40, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Double1, 70);
                    spdData.RPT_AddBasicColumn("A/O", 1, 41, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Double1, 70);
                    spdData.RPT_MerageHeaderColumnSpan(0, 38, 4);

                    spdData.RPT_AddBasicColumn("TEST cumulative performance", 0, 42, Visibles.True, Frozen.False, Align.Center, Merge.False, Formatter.String, 70);
                    spdData.RPT_AddBasicColumn("monthly", 1, 42, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Double1, 70);
                    spdData.RPT_AddBasicColumn("weekly", 1, 43, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Double1, 70);
                    spdData.RPT_MerageHeaderColumnSpan(0, 42, 2);                    

                    spdData.RPT_AddBasicColumn("Daily plan", 0, 44, Visibles.True, Frozen.False, Align.Center, Merge.False, Formatter.String, 70);
                    spdData.RPT_AddBasicColumn("D0", 1, 44, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Double1, 70);
                    spdData.RPT_AddBasicColumn("D1", 1, 45, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Double1, 70);
                    spdData.RPT_MerageHeaderColumnSpan(0, 44, 2);

                    spdData.RPT_AddBasicColumn("TEST(completion) Details WIP", 0, 46, Visibles.True, Frozen.False, Align.Center, Merge.False, Formatter.String, 70);
                    spdData.RPT_AddBasicColumn("TR wait", 1, 46, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Double1, 70);
                    spdData.RPT_AddBasicColumn("TR ing", 1, 47, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Double1, 70);
                    spdData.RPT_AddBasicColumn("TR completed", 1, 48, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Double1, 70);
                    spdData.RPT_MerageHeaderColumnSpan(0, 46, 3);

                    spdData.RPT_AddBasicColumn("TR Performance", 0, 49, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.String, 70);
                    spdData.RPT_AddBasicColumn(dayArry[0], 1, 49, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Double1, 70);
                    spdData.RPT_AddBasicColumn(dayArry[1], 1, 50, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Double1, 70);
                    spdData.RPT_AddBasicColumn(dayArry[2], 1, 51, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Double1, 70);
                    spdData.RPT_MerageHeaderColumnSpan(0, 49, 3);

                    spdData.RPT_AddBasicColumn("FGS performance", 0, 52, Visibles.True, Frozen.False, Align.Center, Merge.False, Formatter.String, 70);
                    spdData.RPT_AddBasicColumn("monthly", 1, 52, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Double1, 70);
                    spdData.RPT_AddBasicColumn("daily", 1, 53, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Double1, 70);
                    spdData.RPT_MerageHeaderColumnSpan(0, 52, 2);
                }

                spdData.RPT_MerageHeaderRowSpan(0, 0, 3);
                spdData.RPT_MerageHeaderRowSpan(0, 1, 3);
                spdData.RPT_MerageHeaderRowSpan(0, 2, 3);
                spdData.RPT_MerageHeaderRowSpan(0, 3, 3);
                spdData.RPT_MerageHeaderRowSpan(0, 4, 3);
                spdData.RPT_MerageHeaderRowSpan(0, 5, 3);
                spdData.RPT_MerageHeaderRowSpan(0, 6, 3);
                spdData.RPT_MerageHeaderRowSpan(0, 7, 3);
                spdData.RPT_MerageHeaderRowSpan(0, 8, 3);
                spdData.RPT_MerageHeaderRowSpan(0, 9, 3);
                spdData.RPT_MerageHeaderRowSpan(0, 10, 3);
                spdData.RPT_MerageHeaderRowSpan(0, 11, 3);
                spdData.RPT_MerageHeaderRowSpan(0, 12, 3);
                spdData.RPT_MerageHeaderRowSpan(0, 13, 3);
                spdData.RPT_MerageHeaderRowSpan(0, 18, 3);

                for (int i = 31; i <= 53; i++)
                {
                    spdData.RPT_MerageHeaderRowSpan(1, i, 2);
                }

                spdData.RPT_ColumnConfigFromTable(btnSort);
            }
            catch (Exception ex)
            {
                CmnFunction.ShowMsgBox(ex.Message);
            }
        }

        /// <summary>
        /// 3. Group By ����
        /// </summary>
        private void SortInit()
        {

            ((udcTableForm)(this.btnSort.BindingForm)).AddRow("CUSTOMER", "(SELECT DATA_1 FROM MGCMTBLDAT@RPTTOMES WHERE FACTORY = '" + GlobalVariable.gsAssyDefaultFactory + "' AND TABLE_NAME = 'H_CUSTOMER' AND KEY_1 = C.MAT_GRP_1) AS CUSTOMER", "C.MAT_GRP_1","CUSTOMER",true);
            ((udcTableForm)(this.btnSort.BindingForm)).AddRow("MAJOR CODE", "C.MAT_GRP_9", "C.MAT_GRP_9", "C.MAT_GRP_9", true);
            ((udcTableForm)(this.btnSort.BindingForm)).AddRow("PACKAGE", "C.MAT_GRP_3", "C.MAT_GRP_3", "C.MAT_GRP_3", true);
            ((udcTableForm)(this.btnSort.BindingForm)).AddRow("TYPE1", "C.MAT_GRP_4", "C.MAT_GRP_4", "C.MAT_GRP_4", false);
            ((udcTableForm)(this.btnSort.BindingForm)).AddRow("LD COUNT", "C.MAT_GRP_6", "C.MAT_GRP_6", "C.MAT_GRP_6", false);
            ((udcTableForm)(this.btnSort.BindingForm)).AddRow("PKG CODE", "C.MAT_CMF_11", "C.MAT_CMF_11","C.MAT_CMF_11", false);
            ((udcTableForm)(this.btnSort.BindingForm)).AddRow("PRODUCT", "C.MAT_ID", "C.MAT_ID", "C.MAT_ID", false);
            ((udcTableForm)(this.btnSort.BindingForm)).AddRow("", "", "", false);
            ((udcTableForm)(this.btnSort.BindingForm)).AddRow("", "", "", false);
            ((udcTableForm)(this.btnSort.BindingForm)).AddRow("", "", "", false);
            ((udcTableForm)(this.btnSort.BindingForm)).AddRow("", "", "", false);
        }
        #endregion


        #region SQL ���� Build
        /// <summary>
        /// 4. ���� ����
        /// </summary>
        /// <returns></returns>
        private string MakeSqlString()
        {
            StringBuilder strSqlString = new StringBuilder();

            string QueryCond1;
            string QueryCond2;
            string QueryCond3;
            string start_date;
            string yesterday;
            string date;
            string month;
            string year;
            string lastMonth;

            udcTableForm tableForm = (udcTableForm)btnSort.BindingForm;
            QueryCond1 = tableForm.SelectedValueToQueryContainNull;
            QueryCond2 = tableForm.SelectedValue2ToQueryContainNull;
            QueryCond3 = tableForm.SelectedValue3ToQueryContainNull;

            int remain = Convert.ToInt32(lblLastDay.Text.Substring(0,2)) - Convert.ToInt32(lblToday.Text.Substring(0,2)) + 1;

            date = cdvDate.SelectedValue();

            DateTime Select_date;
            Select_date = DateTime.Parse(cdvDate.Text.ToString());

            year = Select_date.ToString("yyyy");
            month = Select_date.ToString("yyyyMM");
            start_date = month + "01";
            yesterday = Select_date.AddDays(-1).ToString("yyyyMMdd");
            lastMonth = Select_date.AddMonths(-1).ToString("yyyyMM"); // ������

            // ������ �������� ���ϱ�
            DataTable dt1 = null;
            string Last_Month_Last_day = "(SELECT TO_CHAR(LAST_DAY(TO_DATE('" + lastMonth + "', 'YYYYMM')),'YYYYMMDD') FROM DUAL)";
            dt1 = CmnFunction.oComm.GetFuncDataTable("DYNAMIC", Last_Month_Last_day);
            Last_Month_Last_day = dt1.Rows[0][0].ToString();

            // ���� �������� ���ϱ�
            DataTable dt2 = null;
            string last_day = "(SELECT TO_CHAR(LAST_DAY(TO_DATE('" + month + "', 'YYYYMM')),'YYYYMMDD') FROM DUAL)";
            dt2 = CmnFunction.oComm.GetFuncDataTable("DYNAMIC", last_day);
            last_day = dt2.Rows[0][0].ToString();

            // ���������� �������� ��������
            dt1 = null;
            dt1 = CmnFunction.oComm.GetFuncDataTable("DYNAMIC", MakeSqlString2(year,Select_date.ToString("yyyyMMdd")));
            string Lastweek_lastday = dt1.Rows[0][0].ToString();
                       
            //strSqlString.AppendFormat("SELECT {0} " + "\n", QueryCond2);
            strSqlString.Append("SELECT " + QueryCond1 + "\n");
        

            if (ckbKpcs.Checked == false)
            {
                
                //��ο�
                if (ckbRev.Checked == false)
                {
                    strSqlString.Append("     , SUM(NVL(A.MON_PLAN,0)) AS \"MON_PLAN\"" + "\n");
                    strSqlString.Append("     , 0 AS \"����ȹRev\"" + "\n");
                    strSqlString.Append("     , 0 AS \"����ȹ ����\"" + "\n");
                }
                else
                {
                    strSqlString.Append("     , SUM(NVL(A.ORI_PLAN,0)) AS \"MON_PLAN\"" + "\n");
                    strSqlString.Append("     , SUM(NVL(A.MON_PLAN,0)) AS \"MON_PLAN_REV\"" + "\n"); ;
                    strSqlString.Append("     , SUM(NVL(A.MON_PLAN,0)) -SUM(NVL(A.ORI_PLAN,0)) AS \"MON_PLAN_DIFF\"" + "\n");
                }

                strSqlString.Append("     , ROUND(((SUM(NVL(A.MON_PLAN,0)) * " + jindoPer + ") / 100),0) AS TARGET_MON " + "\n");
                strSqlString.Append("     , SUM(NVL(A.SHIP_MON,0)) " + "\n");                
                strSqlString.Append("     , DECODE(SUM(NVL(A.MON_PLAN,0)),0,0, ROUND((SUM(NVL(A.SHIP_MON,0))/SUM(NVL(A.MON_PLAN,0)))*100,1)) AS JINDO" + "\n");
                strSqlString.Append("     , SUM(NVL(A.SHIP_MON,0)) - ROUND(((SUM(NVL(A.MON_PLAN,0)) * " + jindoPer + ") / 100),1) AS DEF   " + "\n");

                if (lblRemain.Text != "0 day")  // �ܿ����� 0 ���� �ƴҶ�
                {
                    strSqlString.Append("     , ROUND(((SUM(NVL(A.MON_PLAN,0)) - SUM(NVL(A.SHIP_MON,0))) /" + Convert.ToInt32(lblRemain.Text.Substring(0, 2)) + "), 1) AS TARGET_DAY " + "\n");
                }
                else  // �ܿ����� 0 �� �ϴ�
                {
                    strSqlString.Append("         , 0 AS TARGET_DAY " + "\n");
                }

                strSqlString.Append("     , SUM(NVL(A.RCV0,0)), SUM(NVL(A.RCV1,0)), SUM(NVL(A.RCV2,0)) " + "\n");
                strSqlString.Append("     , SUM(NVL(A.T_IN0,0)), SUM(NVL(A.T_IN1,0)), SUM(NVL(A.T_IN2,0)) " + "\n");
                strSqlString.Append("     , SUM(NVL(A.T_OUT0,0)), SUM(NVL(A.T_OUT1,0)), SUM(NVL(A.T_OUT2,0)) " + "\n");
                strSqlString.Append("     , SUM(NVL(A.SHP0,0)), SUM(NVL(A.SHP1,0)), SUM(NVL(A.SHP2,0)) " + "\n");
                strSqlString.Append("     , SUM(NVL(B.V0,0)) AS STOCK" + "\n");
                strSqlString.Append("     , SUM(NVL(B.V1+B.V2+B.V4+B.V5+B.V6,0)) AS TEST " + "\n");
                strSqlString.Append("     , SUM(NVL(B.V3,0)) AS HOLD " + "\n");
                strSqlString.Append("     , SUM(NVL(B.V7+B.V8+B.V9+B.V10+B.V11,0)) AS TEST_END " + "\n");
                strSqlString.Append("     , SUM(NVL(B.V12,0)) AS HMK4T " + "\n");                
                strSqlString.Append("     , SUM(NVL(B.V0,0)) + SUM(NVL(B.V1+B.V2+B.V4+B.V5+B.V6,0)) + SUM(NVL(B.V3,0)) + SUM(NVL(B.V7+B.V8+B.V9+B.V10+B.V11+B.V12,0)) AS TTL " + "\n");
                strSqlString.Append("     , SUM(NVL(A.FGS,0)) AS FGS " + "\n");
                strSqlString.Append("     , SUM(NVL(A.WEEK_PLAN,0)) AS WEEK_PLAN " + "\n");
                strSqlString.Append("     , SUM(NVL(A.SHIP_WEEK,0)) AS SHIP_WEEK " + "\n");
                strSqlString.Append("     , SUM(NVL(A.WEEK_PLAN,0) - NVL(A.SHIP_WEEK,0)) AS WEEK_DEF " + "\n");
                strSqlString.Append("     , SUM(NVL(A.WEEK_PLAN,0) - NVL(A.SHIP_WEEK,0)) - SUM(NVL(B.V0,0) + NVL(B.V1+B.V2+B.V4+B.V5+B.V6,0) + NVL(B.V3,0) + NVL(B.V7+B.V8+B.V9+B.V10+B.V11+B.V12,0)) AS AO_DEF " + "\n");
                strSqlString.Append("     , SUM(NVL(A.T_OUT_MONTH,0)) AS T_OUT_MONTH" + "\n");
                strSqlString.Append("     , SUM(NVL(A.T_OUT_WEEK,0)) AS T_OUT_WEEK" + "\n");
                strSqlString.Append("     , SUM(NVL(A.PLAN1,0)) AS PLAN1" + "\n");
                strSqlString.Append("     , SUM(NVL(A.PLAN2,0)) AS PLAN2" + "\n");
                strSqlString.Append("     , SUM(NVL(B.V7+B.V8+B.V9,0)) AS TR_WAIT " + "\n");
                strSqlString.Append("     , SUM(NVL(B.V10,0)) AS TR_ING " + "\n");
                strSqlString.Append("     , SUM(NVL(B.V11,0)) AS TR_END " + "\n");
                strSqlString.Append("     , SUM(NVL(A.TR_OUT0,0)) AS TR_OUT0, SUM(NVL(A.TR_OUT1,0)) AS TR_OUT1, SUM(NVL(A.TR_OUT2,0)) AS TR_OUT2 " + "\n");
                strSqlString.Append("     , SUM(NVL(A.SHIP_MON_FGS,0)) AS SHIP_MON_FGS " + "\n");
                strSqlString.Append("     , SUM(NVL(A.SHIP_DAY_FGS,0)) AS SHIP_DAY_FGS " + "\n");
            }
            else
            {
                if (ckbRev.Checked == false)
                {
                    strSqlString.Append("     , ROUND(SUM(NVL(A.MON_PLAN,0))/1000,1) AS \"MON_PLAN\"" + "\n");
                    strSqlString.Append("     , 0 AS \"����ȹRev\"" + "\n");
                    strSqlString.Append("     , 0 AS \"����ȹ ����\"" + "\n");
                }
                else
                {
                    strSqlString.Append("     , ROUND(SUM(NVL(A.ORI_PLAN,0))/1000,1) AS \"MON_PLAN\"" + "\n");
                    strSqlString.Append("     , ROUND(SUM(NVL(A.MON_PLAN,0))/1000,1) AS \"MON_PLAN_REV\"" + "\n");
                    strSqlString.Append("     , ROUND((SUM(NVL(A.MON_PLAN,0)) -SUM(NVL(A.ORI_PLAN,0)))/1000,1) AS \"MON_PLAN_DIFF\"" + "\n");
                }
               
                strSqlString.Append("     , ROUND(((SUM(NVL(A.MON_PLAN,0)) * " + jindoPer + ") / 100)/1000,1) AS TARGET_MON " + "\n");
                strSqlString.Append("     , ROUND(SUM(NVL(A.SHIP_MON,0))/1000,1) " + "\n");                
                strSqlString.Append("     , DECODE(SUM(NVL(A.MON_PLAN,0)),0,0, ROUND((SUM(NVL(A.SHIP_MON,0))/SUM(NVL(A.MON_PLAN,0)))*100,1)) AS JINDO" + "\n");                
                strSqlString.Append("     , ROUND((SUM(NVL(A.SHIP_MON,0)) - ROUND(((SUM(NVL(A.MON_PLAN,0)) * " + jindoPer + ") / 100),1))/1000,1) AS DEF   " + "\n");

                if (lblRemain.Text != "0 day")  // �ܿ����� 0 ���� �ƴҶ�
                {
                    strSqlString.Append("     , ROUND(((SUM(NVL(A.MON_PLAN,0)) - SUM(NVL(A.SHIP_MON,0))) /" + Convert.ToInt32(lblRemain.Text.Substring(0, 2)) + ")/1000, 1) AS TARGET_DAY " + "\n");
                }
                else  // �ܿ����� 0 �� �ϴ�
                {
                    strSqlString.Append("         , 0 AS TARGET_DAY " + "\n");
                }
                
                strSqlString.Append("     , ROUND(SUM(NVL(A.RCV0,0))/1000,1), ROUND(SUM(NVL(A.RCV1,0))/1000,1), ROUND(SUM(NVL(A.RCV2,0))/1000,1) " + "\n");
                strSqlString.Append("     , ROUND(SUM(NVL(A.T_IN0,0))/1000,1), ROUND(SUM(NVL(A.T_IN1,0))/1000,1), ROUND(SUM(NVL(A.T_IN2,0))/1000,1) " + "\n");
                strSqlString.Append("     , ROUND(SUM(NVL(A.T_OUT0,0))/1000,1), ROUND(SUM(NVL(A.T_OUT1,0))/1000,1), ROUND(SUM(NVL(A.T_OUT2,0))/1000,1) " + "\n");
                strSqlString.Append("     , ROUND(SUM(NVL(A.SHP0,0))/1000,1), ROUND(SUM(NVL(A.SHP1,0))/1000,1), ROUND(SUM(NVL(A.SHP2,0))/1000,1) " + "\n");
                strSqlString.Append("     , ROUND(SUM(NVL(B.V0,0))/1000,1) AS STOCK" + "\n");
                strSqlString.Append("     , ROUND(SUM(NVL(B.V1+B.V2+B.V4+B.V5+B.V6,0))/1000,1) AS TEST " + "\n");
                strSqlString.Append("     , ROUND(SUM(NVL(B.V3,0))/1000,1) AS HOLD " + "\n");
                strSqlString.Append("     , ROUND(SUM(NVL(B.V7+B.V8+B.V9+B.V10+B.V11,0))/1000,1) AS TEST_END " + "\n");
                strSqlString.Append("     , ROUND(SUM(NVL(B.V12,0))/1000,1) AS HMK4T " + "\n");
                strSqlString.Append("     , ROUND((SUM(NVL(B.V0,0)) + SUM(NVL(B.V1+B.V2+B.V4+B.V5+B.V6,0)) + SUM(NVL(B.V3,0)) + SUM(NVL(B.V7+B.V8+B.V9+B.V10+B.V11+B.V12,0)))/1000,1) AS TTL " + "\n");
                strSqlString.Append("     , ROUND(SUM(NVL(A.FGS,0))/1000,1) AS FGS " + "\n");
                strSqlString.Append("     , ROUND(SUM(NVL(A.WEEK_PLAN,0))/1000,1) AS WEEK_PLAN " + "\n");
                strSqlString.Append("     , ROUND(SUM(NVL(A.SHIP_WEEK,0))/1000,1) AS SHIP_WEEK " + "\n");
                strSqlString.Append("     , ROUND((SUM(NVL(A.WEEK_PLAN,0) - NVL(A.SHIP_WEEK,0)))/1000,1) AS WEEK_DEF " + "\n");
                strSqlString.Append("     , ROUND((SUM(NVL(A.WEEK_PLAN,0) - NVL(A.SHIP_WEEK,0)) - SUM(NVL(B.V0,0) + NVL(B.V1+B.V2+B.V4+B.V5+B.V6,0) + NVL(B.V3,0) + NVL(B.V7+B.V8+B.V9+B.V10+B.V11+B.V12,0)))/1000,1) AS AO_DEF " + "\n");
                strSqlString.Append("     , ROUND(SUM(NVL(A.T_OUT_MONTH,0))/1000,1) AS T_OUT_MONTH " + "\n");
                strSqlString.Append("     , ROUND(SUM(NVL(A.T_OUT_WEEK,0))/1000,1) AS T_OUT_WEEK" + "\n");
                strSqlString.Append("     , ROUND(SUM(NVL(A.PLAN1,0))/1000,1) AS PLAN1" + "\n");
                strSqlString.Append("     , ROUND(SUM(NVL(A.PLAN2,0))/1000,1) AS PLAN2" + "\n");
                strSqlString.Append("     , ROUND(SUM(NVL(B.V7+B.V8+B.V9,0))/1000,1) AS TR_WAIT " + "\n");
                strSqlString.Append("     , ROUND(SUM(NVL(B.V10,0))/1000,1) AS TR_ING " + "\n");
                strSqlString.Append("     , ROUND(SUM(NVL(B.V11,0))/1000,1) AS TR_END " + "\n");
                strSqlString.Append("     , ROUND(SUM(NVL(A.TR_OUT0,0))/1000,1) AS TR_OUT0, ROUND(SUM(NVL(A.TR_OUT1,0))/1000,1) AS TR_OUT1, ROUND(SUM(NVL(A.TR_OUT2,0))/1000,1) AS TR_OUT2" + "\n");
                strSqlString.Append("     , ROUND(SUM(NVL(A.SHIP_MON_FGS,0))/1000,1) AS SHIP_MON_FGS " + "\n");
                strSqlString.Append("     , ROUND(SUM(NVL(A.SHIP_DAY_FGS,0))/1000,1) AS SHIP_DAY_FGS " + "\n");
            }

            strSqlString.Append("  FROM ( " + "\n");
            strSqlString.Append("       SELECT MAT.MAT_GRP_1, MAT.MAT_GRP_2, MAT.MAT_GRP_3, MAT.MAT_GRP_4, MAT.MAT_GRP_5, MAT.MAT_GRP_6, MAT.MAT_GRP_7, MAT.MAT_GRP_8, MAT.MAT_CMF_10, MAT.MAT_ID, MAT.MAT_CMF_7 " + "\n");
            if (ckbRev.Checked == false)
            {
                strSqlString.Append("            , DECODE(MAT.MAT_GRP_3,'COB',ROUND(SUM(PLAN.PLAN_QTY_TEST)/MAT.MAT_CMF_13,0), SUM(PLAN.PLAN_QTY_TEST)) AS MON_PLAN" + "\n");
                strSqlString.Append("             , 0 AS ORI_PLAN" + "\n");
            }
            else
            {
                strSqlString.Append("            , DECODE(MAT.MAT_GRP_3,'COB',ROUND(SUM(PLAN.RESV_FIELD2)/MAT.MAT_CMF_13,0), SUM(PLAN.RESV_FIELD2)) AS MON_PLAN" + "\n");
                strSqlString.Append("            , DECODE(MAT.MAT_GRP_3,'COB',ROUND(SUM(PLAN.PLAN_QTY_TEST)/MAT.MAT_CMF_13,0), SUM(PLAN.PLAN_QTY_TEST)) AS ORI_PLAN" + "\n");
            }
            
            strSqlString.Append("            , DECODE(MAT.MAT_GRP_3,'COB',ROUND(SUM(SHP.SHIP_MON)/MAT.MAT_CMF_13,0), SUM(SHP.SHIP_MON)) AS SHIP_MON" + "\n");
            strSqlString.Append("            , DECODE(MAT.MAT_GRP_3,'COB',ROUND(SUM(SHP.SHP0)/MAT.MAT_CMF_13,0), SUM(SHP.SHP0)) AS SHP0" + "\n");
            strSqlString.Append("            , DECODE(MAT.MAT_GRP_3,'COB',ROUND(SUM(SHP.SHP1)/MAT.MAT_CMF_13,0), SUM(SHP.SHP1)) AS SHP1" + "\n");
            strSqlString.Append("            , DECODE(MAT.MAT_GRP_3,'COB',ROUND(SUM(SHP.SHP2)/MAT.MAT_CMF_13,0), SUM(SHP.SHP2)) AS SHP2" + "\n");
            strSqlString.Append("            , DECODE(MAT.MAT_GRP_3,'COB',ROUND(SUM(T_INOUT.RCV0)/MAT.MAT_CMF_13,0), SUM(T_INOUT.RCV0)) AS RCV0" + "\n");
            strSqlString.Append("            , DECODE(MAT.MAT_GRP_3,'COB',ROUND(SUM(T_INOUT.RCV1)/MAT.MAT_CMF_13,0), SUM(T_INOUT.RCV1)) AS RCV1" + "\n");
            strSqlString.Append("            , DECODE(MAT.MAT_GRP_3,'COB',ROUND(SUM(T_INOUT.RCV2)/MAT.MAT_CMF_13,0), SUM(T_INOUT.RCV2)) AS RCV2" + "\n");
            strSqlString.Append("            , DECODE(MAT.MAT_GRP_3,'COB',ROUND(SUM(T_INOUT.T_IN0)/MAT.MAT_CMF_13,0), SUM(T_INOUT.T_IN0)) AS T_IN0" + "\n");
            strSqlString.Append("            , DECODE(MAT.MAT_GRP_3,'COB',ROUND(SUM(T_INOUT.T_IN1)/MAT.MAT_CMF_13,0), SUM(T_INOUT.T_IN1)) AS T_IN1" + "\n");
            strSqlString.Append("            , DECODE(MAT.MAT_GRP_3,'COB',ROUND(SUM(T_INOUT.T_IN2)/MAT.MAT_CMF_13,0), SUM(T_INOUT.T_IN2)) AS T_IN2" + "\n");
            strSqlString.Append("            , DECODE(MAT.MAT_GRP_3,'COB',ROUND(SUM(T_INOUT.T_OUT0)/MAT.MAT_CMF_13,0), SUM(T_INOUT.T_OUT0)) AS T_OUT0" + "\n");
            strSqlString.Append("            , DECODE(MAT.MAT_GRP_3,'COB',ROUND(SUM(T_INOUT.T_OUT1)/MAT.MAT_CMF_13,0), SUM(T_INOUT.T_OUT1)) AS T_OUT1" + "\n");
            strSqlString.Append("            , DECODE(MAT.MAT_GRP_3,'COB',ROUND(SUM(T_INOUT.T_OUT2)/MAT.MAT_CMF_13,0), SUM(T_INOUT.T_OUT2)) AS T_OUT2" + "\n");
            strSqlString.Append("            , DECODE(MAT.MAT_GRP_3,'COB',ROUND(SUM(T_INOUT.T_OUT_MONTH)/MAT.MAT_CMF_13,0), SUM(T_INOUT.T_OUT_MONTH)) AS T_OUT_MONTH" + "\n");
            strSqlString.Append("            , DECODE(MAT.MAT_GRP_3,'COB',ROUND(SUM(T_INOUT.T_OUT_WEEK)/MAT.MAT_CMF_13,0), SUM(T_INOUT.T_OUT_WEEK)) AS T_OUT_WEEK" + "\n");
            strSqlString.Append("            , DECODE(MAT.MAT_GRP_3,'COB',ROUND(SUM(FGS.FGS)/MAT.MAT_CMF_13,0), SUM(FGS.FGS)) AS FGS" + "\n");
            strSqlString.Append("            , CASE WHEN MAT.MAT_GRP_1 = 'SE' AND MAT.MAT_GRP_3 = 'COB' THEN ROUND(SUM(NVL(W_PLAN.WEEK_PLAN,0) + NVL(SHP.T_SHIP_WEEK,0))/MAT.MAT_CMF_13,0) " + "\n");
            strSqlString.Append("                   WHEN MAT.MAT_GRP_1 = 'SE' THEN SUM(NVL(W_PLAN.WEEK_PLAN,0) + NVL(SHP.T_SHIP_WEEK,0))" + "\n");
            strSqlString.Append("                   ELSE SUM(NVL(W_PLAN.WEEK_PLAN,0))" + "\n");
            strSqlString.Append("              END AS WEEK_PLAN" + "\n");
            strSqlString.Append("            , DECODE(MAT.MAT_GRP_3,'COB',ROUND(SUM(SHP.SHIP_WEEK)/MAT.MAT_CMF_13,0), SUM(SHP.SHIP_WEEK)) AS SHIP_WEEK " + "\n");
            strSqlString.Append("            , DECODE(MAT.MAT_GRP_3,'COB',ROUND(SUM(W_PLAN.PLAN1)/MAT.MAT_CMF_13,0), SUM(W_PLAN.PLAN1)) AS PLAN1 " + "\n");
            strSqlString.Append("            , DECODE(MAT.MAT_GRP_3,'COB',ROUND(SUM(W_PLAN.PLAN2)/MAT.MAT_CMF_13,0), SUM(W_PLAN.PLAN2)) AS PLAN2 " + "\n");
            strSqlString.Append("            , DECODE(MAT.MAT_GRP_3,'COB',ROUND(SUM(T_INOUT.TR_OUT0)/MAT.MAT_CMF_13,0), SUM(T_INOUT.TR_OUT0)) AS TR_OUT0" + "\n");
            strSqlString.Append("            , DECODE(MAT.MAT_GRP_3,'COB',ROUND(SUM(T_INOUT.TR_OUT1)/MAT.MAT_CMF_13,0), SUM(T_INOUT.TR_OUT1)) AS TR_OUT1" + "\n");
            strSqlString.Append("            , DECODE(MAT.MAT_GRP_3,'COB',ROUND(SUM(T_INOUT.TR_OUT2)/MAT.MAT_CMF_13,0), SUM(T_INOUT.TR_OUT2)) AS TR_OUT2" + "\n");
            strSqlString.Append("            , DECODE(MAT.MAT_GRP_3,'COB',ROUND(SUM(SHP.SHIP_MON_FGS)/MAT.MAT_CMF_13,0), SUM(SHP.SHIP_MON_FGS)) AS SHIP_MON_FGS " + "\n");
            strSqlString.Append("            , DECODE(MAT.MAT_GRP_3,'COB',ROUND(SUM(SHP.SHIP_DAY_FGS)/MAT.MAT_CMF_13,0), SUM(SHP.SHIP_DAY_FGS)) AS SHIP_DAY_FGS " + "\n");
            strSqlString.Append("         FROM ( " + "\n");
            strSqlString.Append("               SELECT DISTINCT MAT_GRP_1, MAT_GRP_2, MAT_GRP_3, MAT_GRP_4, MAT_GRP_5, MAT_GRP_6, MAT_GRP_7, MAT_GRP_8, MAT_CMF_10, MAT_ID, MAT_CMF_7, TO_NUMBER(DECODE(MAT_CMF_13, ' ', 1, '-', 1, MAT_CMF_13)) AS MAT_CMF_13 " + "\n");
            strSqlString.Append("                 FROM MWIPMATDEF " + "\n");
            strSqlString.Append("                WHERE 1=1 " + "\n");
            strSqlString.Append("                  AND FACTORY  = '" + cdvFactory.Text + "' " + "\n");
            //strSqlString.Append("                  AND FACTORY IN ('" + GlobalVariable.gsTestDefaultFactory + "', 'HMKE1') " + "\n");
            strSqlString.Append("                  AND MAT_TYPE= 'FG' " + "\n");
            strSqlString.Append("                  AND DELETE_FLAG <> 'Y' " + "\n");
            strSqlString.Append("              ) MAT " + "\n");
            strSqlString.Append("            , ( " + "\n");
            strSqlString.Append("               SELECT MAT_ID,PLAN_QTY_TEST,PLAN_MONTH, RESV_FIELD2  " + "\n");
            strSqlString.Append("                 FROM (  " + "\n");
            strSqlString.Append("                       SELECT MAT_ID,SUM(PLAN_QTY_TEST) AS PLAN_QTY_TEST,PLAN_MONTH, SUM(RESV_FIELD2) AS RESV_FIELD2  " + "\n");
            strSqlString.Append("                         FROM ( " + "\n");

            if (cdvFactory.Text == GlobalVariable.gsTestDefaultFactory)
                strSqlString.Append("                                SELECT MAT_ID, SUM(PLAN_QTY_TEST) AS PLAN_QTY_TEST, PLAN_MONTH, SUM(TO_NUMBER(DECODE(RESV_FIELD2,' ',0,RESV_FIELD2))) AS RESV_FIELD2  " + "\n");
            else
                strSqlString.Append("                                SELECT MAT_ID, SUM(TO_NUMBER(DECODE(RESV_FIELD5,' ',0,RESV_FIELD5))) AS PLAN_QTY_TEST, PLAN_MONTH, SUM(TO_NUMBER(DECODE(RESV_FIELD6,' ',0,RESV_FIELD6))) AS RESV_FIELD2  " + "\n");

            strSqlString.Append("                                 FROM CWIPPLNMON " + "\n");
            strSqlString.Append("                                WHERE 1=1 " + "\n");
            strSqlString.Append("                                  AND FACTORY  = '" + GlobalVariable.gsTestDefaultFactory + "' " + "\n");      // ������ ASSY�����͸� TEST�� �����ϰ� �����ϱ⿡ �� factory ������ �׳� �÷����� �Ѵ�.       
            //strSqlString.Append("                                  AND FACTORY IN ('" + GlobalVariable.gsTestDefaultFactory + "', 'HMKE1') " + "\n");
            strSqlString.Append("                                  AND PLAN_MONTH = '" + month + "' " + "\n");
            strSqlString.Append("                                GROUP BY MAT_ID, PLAN_MONTH " + "\n");
            strSqlString.Append("                              ) " + "\n");
            strSqlString.Append("                        GROUP BY MAT_ID,PLAN_MONTH  " + "\n");
            strSqlString.Append("                      ) " + "\n");
            strSqlString.Append("              ) PLAN " + "\n");
            strSqlString.Append("            , ( " + "\n");
            strSqlString.Append("               SELECT MAT_ID " + "\n");
            strSqlString.Append("                    , SUM(WW_QTY) AS WEEK_PLAN " + "\n");
            strSqlString.Append("                    , 0 AS PLAN1 " + "\n");
            strSqlString.Append("                    , 0 AS PLAN2 " + "\n");
            strSqlString.Append("                 FROM RWIPPLNWEK " + "\n");
            strSqlString.Append("                WHERE 1=1 " + "\n");
            strSqlString.Append("                  AND FACTORY = '" + cdvFactory.Text + "' " + "\n");
            //strSqlString.Append("                  AND FACTORY = '" + GlobalVariable.gsTestDefaultFactory + "' " + "\n");
            strSqlString.Append("                  AND GUBUN = '3' " + "\n");
            strSqlString.Append("                  AND MAT_ID NOT LIKE 'SE%' " + "\n");
            strSqlString.Append("                  AND PLAN_WEEK = '" + FindWeek.ThisWeek + "' " + "\n");
            strSqlString.Append("                GROUP BY MAT_ID " + "\n");
            strSqlString.Append("                UNION ALL " + "\n");

            strSqlString.Append("               SELECT MAT_ID " + "\n");
            strSqlString.Append("                    , SUM(CASE WHEN PLAN_DAY BETWEEN '" + FindWeek.StartDay_ThisWeek + "' AND '" + FindWeek.EndDay_ThisWeek + "' THEN PLAN_QTY ELSE 0 END) AS WEEN_PLAN" + "\n");            
            strSqlString.Append("                    , SUM(DECODE(PLAN_DAY, '" + date + "', PLAN_QTY, 0)) AS PLAN1" + "\n");
            strSqlString.Append("                    , SUM(DECODE(PLAN_DAY, '" + cdvDate.Value.AddDays(1).ToString("yyyyMMdd") + "', PLAN_QTY, 0)) AS PLAN2" + "\n");

            // ����ȹ �����̸� �������
            if (DateTime.Now.ToString("yyyyMMdd") == date)
            {
                strSqlString.Append("                 FROM CWIPPLNDAY " + "\n");
                strSqlString.Append("                WHERE 1=1 " + "\n");
            }
            else// ������ �ƴϸ� ������ ������ ���̺��� ������.
            {
                strSqlString.Append("                 FROM CWIPPLNSNP@RPTTOMES " + "\n");
                strSqlString.Append("                WHERE SNAPSHOT_DAY = '" + date + "'" + "\n");
            }

            strSqlString.Append("                  AND FACTORY = '" + cdvFactory.Text + "' " + "\n");
            //strSqlString.Append("                  AND FACTORY  = '" + GlobalVariable.gsTestDefaultFactory + "' " + "\n");
            strSqlString.Append("                  AND PLAN_DAY BETWEEN '" + FindWeek.StartDay_ThisWeek + "' AND '" + FindWeek.EndDay_NextWeek + "' " + "\n");
            strSqlString.Append("                  AND IN_OUT_FLAG = 'IN'" + "\n");
            strSqlString.Append("                  AND CLASS = 'SLIS'" + "\n");
            strSqlString.Append("                GROUP BY MAT_ID " + "\n");
            strSqlString.Append("              ) W_PLAN " + "\n");
            strSqlString.Append("            , ( " + "\n");
            strSqlString.Append("               SELECT MAT_ID " + "\n");
            strSqlString.Append("                    , SUM(SHIP_MON) AS SHIP_MON " + "\n");
            strSqlString.Append("                    , SUM(SHIP_WEEK) AS SHIP_WEEK " + "\n");
            strSqlString.Append("                    , SUM(T_SHIP_WEEK) AS T_SHIP_WEEK " + "\n");
            strSqlString.Append("                    , SUM(SHP0) AS SHP0 " + "\n");
            strSqlString.Append("                    , SUM(SHP1) AS SHP1 " + "\n");
            strSqlString.Append("                    , SUM(SHP2) AS SHP2 " + "\n");
            strSqlString.Append("                    , SUM(SHIP_MON_FGS) AS SHIP_MON_FGS " + "\n");
            strSqlString.Append("                    , SUM(SHIP_DAY_FGS) AS SHIP_DAY_FGS " + "\n");
            strSqlString.Append("                 FROM ( " + "\n");

            if (cdvFactory.Text == GlobalVariable.gsTestDefaultFactory)
            {
                strSqlString.Append("                       SELECT MAT_ID " + "\n");
                strSqlString.Append("                            , 0 AS SHIP_MON" + "\n");
                strSqlString.Append("                            , SUM(CASE WHEN WORK_DATE BETWEEN '" + FindWeek.StartDay_ThisWeek + "' AND '" + date + "' THEN (S1_FAC_OUT_QTY_1+S2_FAC_OUT_QTY_1+S3_FAC_OUT_QTY_1) ELSE 0 END) SHIP_WEEK" + "\n");
                strSqlString.Append("                            , SUM(CASE WHEN WORK_DATE BETWEEN '" + FindWeek.StartDay_ThisWeek + "' AND '" + yesterday + "' THEN (S1_FAC_OUT_QTY_1+S2_FAC_OUT_QTY_1+S3_FAC_OUT_QTY_1) ELSE 0 END) T_SHIP_WEEK" + "\n");
                strSqlString.Append("                            , SUM(CASE WHEN WORK_DATE = '" + dayArry2[0] + "' THEN (S1_FAC_OUT_QTY_1+S2_FAC_OUT_QTY_1+S3_FAC_OUT_QTY_1) ELSE 0 END) SHP0" + "\n");
                strSqlString.Append("                            , SUM(CASE WHEN WORK_DATE = '" + dayArry2[1] + "' THEN (S1_FAC_OUT_QTY_1+S2_FAC_OUT_QTY_1+S3_FAC_OUT_QTY_1) ELSE 0 END) SHP1" + "\n");
                strSqlString.Append("                            , SUM(CASE WHEN WORK_DATE = '" + dayArry2[2] + "' THEN (S1_FAC_OUT_QTY_1+S2_FAC_OUT_QTY_1+S3_FAC_OUT_QTY_1) ELSE 0 END) SHP2" + "\n");
                strSqlString.Append("                            , 0 AS SHIP_MON_FGS, 0 AS SHIP_DAY_FGS" + "\n");
                strSqlString.Append("                         FROM RSUMFACMOV " + "\n");
                strSqlString.Append("                        WHERE 1=1 " + "\n");
                strSqlString.Append("                          AND WORK_DATE BETWEEN '" + FindWeek.StartDay_LastWeek + "' AND '" + date + "'" + "\n");
                strSqlString.Append("                          AND LOT_TYPE = 'W'" + "\n");
                strSqlString.Append("                          AND CM_KEY_1 = '" + GlobalVariable.gsTestDefaultFactory + "'" + "\n");
                strSqlString.Append("                          AND CM_KEY_2 = 'PROD'" + "\n");
                strSqlString.Append("                          AND FACTORY NOT IN ('RETURN') " + "\n");

                if (cdvLotType.Text != "ALL")
                {
                    strSqlString.Append("                          AND CM_KEY_3 LIKE '" + cdvLotType.Text + "'" + "\n");
                }

                strSqlString.Append("                        GROUP BY MAT_ID " + "\n");
                strSqlString.Append("                        UNION ALL " + "\n");
                strSqlString.Append("                       SELECT MAT_ID " + "\n");
                strSqlString.Append("                            , SUM(DECODE(FACTORY, '" + GlobalVariable.gsTestDefaultFactory + "', SHP_QTY_1, 0)) AS SHIP_MON  " + "\n");
                strSqlString.Append("                            , 0, 0, 0, 0, 0 " + "\n");
                strSqlString.Append("                            , SUM(DECODE(FACTORY, 'FGS', SHP_QTY_1, 0)) AS SHIP_MON_FGS " + "\n");
                strSqlString.Append("                            , SUM(CASE WHEN FACTORY = 'FGS' AND WORK_DATE = '" + date + "' THEN SHP_QTY_1 ELSE 0 END) AS SHIP_DAY_FGS " + "\n");
                strSqlString.Append("                         FROM VSUMWIPOUT " + "\n");
                strSqlString.Append("                        WHERE 1=1 " + "\n");
                strSqlString.Append("                          AND WORK_DATE BETWEEN '" + start_date + "' AND '" + date + "'" + "\n");
                strSqlString.Append("                          AND FACTORY IN ('" + GlobalVariable.gsTestDefaultFactory + "', 'FGS')" + "\n");
                strSqlString.Append("                          AND LOT_TYPE = 'W'" + "\n");
                strSqlString.Append("                          AND CM_KEY_2 = 'PROD'" + "\n");

                if (cdvLotType.Text != "ALL")
                {
                    strSqlString.Append("                          AND CM_KEY_3 LIKE '" + cdvLotType.Text + "'" + "\n");
                }

                strSqlString.Append("                        GROUP BY MAT_ID " + "\n");
            }
            else
            {
                //strSqlString.Append("                        UNION ALL " + "\n");
                strSqlString.Append("                       SELECT MAT_ID " + "\n");
                strSqlString.Append("                            , SUM(CASE WHEN WORK_DATE BETWEEN '" + start_date + "' AND '" + date + "' THEN (S1_FAC_OUT_QTY_1+S2_FAC_OUT_QTY_1+S3_FAC_OUT_QTY_1) ELSE 0 END) SHIP_MON" + "\n");
                strSqlString.Append("                            , SUM(CASE WHEN WORK_DATE BETWEEN '" + FindWeek.StartDay_ThisWeek + "' AND '" + date + "' THEN (S1_FAC_OUT_QTY_1+S2_FAC_OUT_QTY_1+S3_FAC_OUT_QTY_1) ELSE 0 END) SHIP_WEEK" + "\n");
                strSqlString.Append("                            , SUM(CASE WHEN WORK_DATE BETWEEN '" + FindWeek.StartDay_ThisWeek + "' AND '" + yesterday + "' THEN (S1_FAC_OUT_QTY_1+S2_FAC_OUT_QTY_1+S3_FAC_OUT_QTY_1) ELSE 0 END) T_SHIP_WEEK" + "\n");
                strSqlString.Append("                            , SUM(CASE WHEN WORK_DATE = '" + dayArry2[0] + "' THEN (S1_FAC_OUT_QTY_1+S2_FAC_OUT_QTY_1+S3_FAC_OUT_QTY_1) ELSE 0 END) SHP0" + "\n");
                strSqlString.Append("                            , SUM(CASE WHEN WORK_DATE = '" + dayArry2[1] + "' THEN (S1_FAC_OUT_QTY_1+S2_FAC_OUT_QTY_1+S3_FAC_OUT_QTY_1) ELSE 0 END) SHP1" + "\n");
                strSqlString.Append("                            , SUM(CASE WHEN WORK_DATE = '" + dayArry2[2] + "' THEN (S1_FAC_OUT_QTY_1+S2_FAC_OUT_QTY_1+S3_FAC_OUT_QTY_1) ELSE 0 END) SHP2" + "\n");
                strSqlString.Append("                            , 0 AS SHIP_MON_FGS, 0 AS SHIP_DAY_FGS" + "\n");
                strSqlString.Append("                         FROM RSUMFACMOV " + "\n");
                strSqlString.Append("                        WHERE 1=1 " + "\n");

                // 2010-04-02-������ : �ſ� 1��, 2�� ��� 3���� ������ �����ͷ� WORK_DATE ����Ѵ�.
                //if (date.Substring(6, 2).Equals("01") || date.Substring(6, 2).Equals("02"))
                if (Convert.ToInt32(FindWeek.StartDay_LastWeek) < Convert.ToInt32(Last_Month_Last_day))
                {
                    strSqlString.Append("                          AND WORK_DATE BETWEEN '" + FindWeek.StartDay_LastWeek + "' AND '" + date + "'" + "\n");
                }
                else
                {
                    strSqlString.Append("                          AND WORK_DATE BETWEEN '" + Last_Month_Last_day + "' AND '" + date + "'" + "\n");
                }

                strSqlString.Append("                          AND FACTORY = '" + GlobalVariable.gsAssyDefaultFactory + "'" + "\n");
                strSqlString.Append("                          AND CM_KEY_1 = 'HMKE1'" + "\n");

                if (cdvLotType.Text != "ALL")
                {
                    strSqlString.Append("                          AND CM_KEY_3 LIKE '" + cdvLotType.Text + "'" + "\n");
                }

                strSqlString.Append("                        GROUP BY MAT_ID " + "\n");
            }

            strSqlString.Append("                       ) " + "\n");
            strSqlString.Append("                 GROUP BY MAT_ID " + "\n");
            strSqlString.Append("               ) SHP " + "\n");
            strSqlString.Append("             , ( " + "\n");
            strSqlString.Append("                SELECT MAT_ID " + "\n");
            strSqlString.Append("                     , SUM(CASE WHEN WORK_DATE = '" + dayArry2[0] + "' AND OPER = 'T0000' THEN S1_OPER_IN_QTY_1 + S2_OPER_IN_QTY_1 + S3_OPER_IN_QTY_1 ELSE 0 END) RCV0" + "\n");
            strSqlString.Append("                     , SUM(CASE WHEN WORK_DATE = '" + dayArry2[1] + "' AND OPER = 'T0000' THEN S1_OPER_IN_QTY_1 + S2_OPER_IN_QTY_1 + S3_OPER_IN_QTY_1 ELSE 0 END) RCV1" + "\n");
            strSqlString.Append("                     , SUM(CASE WHEN WORK_DATE = '" + dayArry2[2] + "' AND OPER = 'T0000' THEN S1_OPER_IN_QTY_1 + S2_OPER_IN_QTY_1 + S3_OPER_IN_QTY_1 ELSE 0 END) RCV2" + "\n");
            strSqlString.Append("                     , SUM(CASE WHEN WORK_DATE = '" + dayArry2[0] + "' THEN (CASE WHEN T0100_CNT + T0960_CNT > 1 THEN (CASE WHEN OPER IN ('T0100') THEN S1_OPER_IN_QTY_1 + S2_OPER_IN_QTY_1 + S3_OPER_IN_QTY_1 ELSE 0 END)" + "\n");
            strSqlString.Append("                                                                                  WHEN T0500_CNT + T0960_CNT > 1 THEN (CASE WHEN OPER IN ('T0500') THEN S1_OPER_IN_QTY_1 + S2_OPER_IN_QTY_1 + S3_OPER_IN_QTY_1 ELSE 0 END)" + "\n");
            strSqlString.Append("                                                                                  WHEN OPER IN ('T0100', 'T0400', 'T0500', 'T0960', 'S0960') THEN S1_OPER_IN_QTY_1 + S2_OPER_IN_QTY_1 + S3_OPER_IN_QTY_1" + "\n");
            strSqlString.Append("                                                                                  ELSE 0 END)" + "\n");
            strSqlString.Append("                           END) T_IN0" + "\n");
            strSqlString.Append("                     , SUM(CASE WHEN WORK_DATE = '" + dayArry2[1] + "' THEN (CASE WHEN T0100_CNT + T0960_CNT > 1 THEN (CASE WHEN OPER IN ('T0100') THEN S1_OPER_IN_QTY_1 + S2_OPER_IN_QTY_1 + S3_OPER_IN_QTY_1 ELSE 0 END)" + "\n");
            strSqlString.Append("                                                                                  WHEN T0500_CNT + T0960_CNT > 1 THEN (CASE WHEN OPER IN ('T0500') THEN S1_OPER_IN_QTY_1 + S2_OPER_IN_QTY_1 + S3_OPER_IN_QTY_1 ELSE 0 END)" + "\n");
            strSqlString.Append("                                                                                  WHEN OPER IN ('T0100', 'T0400', 'T0500', 'T0960', 'S0960') THEN S1_OPER_IN_QTY_1 + S2_OPER_IN_QTY_1 + S3_OPER_IN_QTY_1" + "\n");
            strSqlString.Append("                                                                                  ELSE 0 END)" + "\n");
            strSqlString.Append("                           END) T_IN1" + "\n");
            strSqlString.Append("                     , SUM(CASE WHEN WORK_DATE = '" + dayArry2[2] + "' THEN (CASE WHEN T0100_CNT + T0960_CNT > 1 THEN (CASE WHEN OPER IN ('T0100') THEN S1_OPER_IN_QTY_1 + S2_OPER_IN_QTY_1 + S3_OPER_IN_QTY_1 ELSE 0 END)" + "\n");
            strSqlString.Append("                                                                                  WHEN T0500_CNT + T0960_CNT > 1 THEN (CASE WHEN OPER IN ('T0500') THEN S1_OPER_IN_QTY_1 + S2_OPER_IN_QTY_1 + S3_OPER_IN_QTY_1 ELSE 0 END)" + "\n");
            strSqlString.Append("                                                                                  WHEN OPER IN ('T0100', 'T0400', 'T0500', 'T0960', 'S0960') THEN S1_OPER_IN_QTY_1 + S2_OPER_IN_QTY_1 + S3_OPER_IN_QTY_1" + "\n");
            strSqlString.Append("                                                                                  ELSE 0 END)" + "\n");
            strSqlString.Append("                           END) T_IN2" + "\n");
            if (checkBox1.Checked)
            {
                strSqlString.Append("                     , SUM(CASE WHEN WORK_DATE = '" + dayArry2[0] + "' THEN (CASE WHEN T0100_CNT + T0960_CNT > 1 THEN (CASE WHEN OPER IN ('T0100') THEN S1_END_QTY_1 + S2_END_QTY_1 + S3_END_QTY_1 + S1_END_RWK_QTY_1 + S2_END_RWK_QTY_1 + S3_END_RWK_QTY_1 ELSE 0 END)" + "\n");
                strSqlString.Append("                                                                                  WHEN T0500_CNT + T0960_CNT > 1 THEN (CASE WHEN OPER IN ('T0500') THEN S1_END_QTY_1 + S2_END_QTY_1 + S3_END_QTY_1 + S1_END_RWK_QTY_1 + S2_END_RWK_QTY_1 + S3_END_RWK_QTY_1 ELSE 0 END)" + "\n");
                strSqlString.Append("                                                                                  WHEN OPER IN ('T0100', 'T0400', 'T0500', 'T0960', 'S0960') THEN S1_END_QTY_1 + S2_END_QTY_1 + S3_END_QTY_1 + S1_END_RWK_QTY_1 + S2_END_RWK_QTY_1 + S3_END_RWK_QTY_1" + "\n");
                strSqlString.Append("                                                                                  ELSE 0 END)" + "\n");
                strSqlString.Append("                           END) T_OUT0" + "\n");
                strSqlString.Append("                     , SUM(CASE WHEN WORK_DATE = '" + dayArry2[1] + "' THEN (CASE WHEN T0100_CNT + T0960_CNT > 1 THEN (CASE WHEN OPER IN ('T0100') THEN S1_END_QTY_1 + S2_END_QTY_1 + S3_END_QTY_1 + S1_END_RWK_QTY_1 + S2_END_RWK_QTY_1 + S3_END_RWK_QTY_1 ELSE 0 END)" + "\n");
                strSqlString.Append("                                                                                  WHEN T0500_CNT + T0960_CNT > 1 THEN (CASE WHEN OPER IN ('T0500') THEN S1_END_QTY_1 + S2_END_QTY_1 + S3_END_QTY_1 + S1_END_RWK_QTY_1 + S2_END_RWK_QTY_1 + S3_END_RWK_QTY_1 ELSE 0 END)" + "\n");
                strSqlString.Append("                                                                                  WHEN OPER IN ('T0100', 'T0400', 'T0500', 'T0960', 'S0960') THEN S1_END_QTY_1 + S2_END_QTY_1 + S3_END_QTY_1 + S1_END_RWK_QTY_1 + S2_END_RWK_QTY_1 + S3_END_RWK_QTY_1" + "\n");
                strSqlString.Append("                                                                                  ELSE 0 END)" + "\n");
                strSqlString.Append("                           END) T_OUT1" + "\n");
                strSqlString.Append("                     , SUM(CASE WHEN WORK_DATE = '" + dayArry2[2] + "' THEN (CASE WHEN T0100_CNT + T0960_CNT > 1 THEN (CASE WHEN OPER IN ('T0100') THEN S1_END_QTY_1 + S2_END_QTY_1 + S3_END_QTY_1 + S1_END_RWK_QTY_1 + S2_END_RWK_QTY_1 + S3_END_RWK_QTY_1 ELSE 0 END)" + "\n");
                strSqlString.Append("                                                                                  WHEN T0500_CNT + T0960_CNT > 1 THEN (CASE WHEN OPER IN ('T0500') THEN S1_END_QTY_1 + S2_END_QTY_1 + S3_END_QTY_1 + S1_END_RWK_QTY_1 + S2_END_RWK_QTY_1 + S3_END_RWK_QTY_1 ELSE 0 END)" + "\n");
                strSqlString.Append("                                                                                  WHEN OPER IN ('T0100', 'T0400', 'T0500', 'T0960', 'S0960') THEN S1_END_QTY_1 + S2_END_QTY_1 + S3_END_QTY_1 + S1_END_RWK_QTY_1 + S2_END_RWK_QTY_1 + S3_END_RWK_QTY_1" + "\n");
                strSqlString.Append("                                                                                  ELSE 0 END)" + "\n");
                strSqlString.Append("                           END) T_OUT2" + "\n");
            }
            else
            {
                strSqlString.Append("                     , SUM(CASE WHEN WORK_DATE = '" + dayArry2[0] + "' THEN (CASE WHEN T0100_CNT + T0960_CNT > 1 THEN (CASE WHEN OPER IN ('T0960') THEN S1_END_QTY_1 + S2_END_QTY_1 + S3_END_QTY_1 + S1_END_RWK_QTY_1 + S2_END_RWK_QTY_1 + S3_END_RWK_QTY_1 ELSE 0 END)" + "\n");
                strSqlString.Append("                                                                                  WHEN T0500_CNT + T0960_CNT > 1 THEN (CASE WHEN OPER IN ('T0960') THEN S1_END_QTY_1 + S2_END_QTY_1 + S3_END_QTY_1 + S1_END_RWK_QTY_1 + S2_END_RWK_QTY_1 + S3_END_RWK_QTY_1 ELSE 0 END)" + "\n");
                strSqlString.Append("                                                                                  WHEN OPER IN ('T0100', 'T0400', 'T0500', 'T0960', 'S0960') THEN S1_END_QTY_1 + S2_END_QTY_1 + S3_END_QTY_1 + S1_END_RWK_QTY_1 + S2_END_RWK_QTY_1 + S3_END_RWK_QTY_1" + "\n");
                strSqlString.Append("                                                                                  ELSE 0 END)" + "\n");
                strSqlString.Append("                           END) T_OUT0" + "\n");
                strSqlString.Append("                     , SUM(CASE WHEN WORK_DATE = '" + dayArry2[1] + "' THEN (CASE WHEN T0100_CNT + T0960_CNT > 1 THEN (CASE WHEN OPER IN ('T0960') THEN S1_END_QTY_1 + S2_END_QTY_1 + S3_END_QTY_1 + S1_END_RWK_QTY_1 + S2_END_RWK_QTY_1 + S3_END_RWK_QTY_1 ELSE 0 END)" + "\n");
                strSqlString.Append("                                                                                  WHEN T0500_CNT + T0960_CNT > 1 THEN (CASE WHEN OPER IN ('T0960') THEN S1_END_QTY_1 + S2_END_QTY_1 + S3_END_QTY_1 + S1_END_RWK_QTY_1 + S2_END_RWK_QTY_1 + S3_END_RWK_QTY_1 ELSE 0 END)" + "\n");
                strSqlString.Append("                                                                                  WHEN OPER IN ('T0100', 'T0400', 'T0500', 'T0960', 'S0960') THEN S1_END_QTY_1 + S2_END_QTY_1 + S3_END_QTY_1 + S1_END_RWK_QTY_1 + S2_END_RWK_QTY_1 + S3_END_RWK_QTY_1" + "\n");
                strSqlString.Append("                                                                                  ELSE 0 END)" + "\n");
                strSqlString.Append("                           END) T_OUT1" + "\n");
                strSqlString.Append("                     , SUM(CASE WHEN WORK_DATE = '" + dayArry2[2] + "' THEN (CASE WHEN T0100_CNT + T0960_CNT > 1 THEN (CASE WHEN OPER IN ('T0960') THEN S1_END_QTY_1 + S2_END_QTY_1 + S3_END_QTY_1 + S1_END_RWK_QTY_1 + S2_END_RWK_QTY_1 + S3_END_RWK_QTY_1 ELSE 0 END)" + "\n");
                strSqlString.Append("                                                                                  WHEN T0500_CNT + T0960_CNT > 1 THEN (CASE WHEN OPER IN ('T0960') THEN S1_END_QTY_1 + S2_END_QTY_1 + S3_END_QTY_1 + S1_END_RWK_QTY_1 + S2_END_RWK_QTY_1 + S3_END_RWK_QTY_1 ELSE 0 END)" + "\n");
                strSqlString.Append("                                                                                  WHEN OPER IN ('T0100', 'T0400', 'T0500', 'T0960', 'S0960') THEN S1_END_QTY_1 + S2_END_QTY_1 + S3_END_QTY_1 + S1_END_RWK_QTY_1 + S2_END_RWK_QTY_1 + S3_END_RWK_QTY_1" + "\n");
                strSqlString.Append("                                                                                  ELSE 0 END)" + "\n");
                strSqlString.Append("                           END) T_OUT2" + "\n");
            }
            
            strSqlString.Append("                     , SUM(CASE WHEN WORK_DATE BETWEEN '" + start_date + "' AND '" + date + "' THEN (CASE WHEN T0100_CNT + T0960_CNT > 1 THEN (CASE WHEN OPER IN ('T0960') THEN S1_END_QTY_1 + S2_END_QTY_1 + S3_END_QTY_1 + S1_END_RWK_QTY_1 + S2_END_RWK_QTY_1 + S3_END_RWK_QTY_1 ELSE 0 END)" + "\n");
            strSqlString.Append("                                                                                                          WHEN T0500_CNT + T0960_CNT > 1 THEN (CASE WHEN OPER IN ('T0960') THEN S1_END_QTY_1 + S2_END_QTY_1 + S3_END_QTY_1 + S1_END_RWK_QTY_1 + S2_END_RWK_QTY_1 + S3_END_RWK_QTY_1 ELSE 0 END)" + "\n");
            strSqlString.Append("                                                                                                          WHEN OPER IN ('T0100', 'T0400', 'T0500', 'T0960', 'S0960') THEN S1_END_QTY_1 + S2_END_QTY_1 + S3_END_QTY_1 + S1_END_RWK_QTY_1 + S2_END_RWK_QTY_1 + S3_END_RWK_QTY_1" + "\n");
            strSqlString.Append("                                                                                                          ELSE 0 END)" + "\n");
            strSqlString.Append("                           END) T_OUT_MONTH" + "\n");
            strSqlString.Append("                     , SUM(CASE WHEN WORK_DATE BETWEEN '" + FindWeek.StartDay_ThisWeek + "' AND '" + date + "' THEN (CASE WHEN T0100_CNT + T0960_CNT > 1 THEN (CASE WHEN OPER IN ('T0960') THEN S1_END_QTY_1 + S2_END_QTY_1 + S3_END_QTY_1 + S1_END_RWK_QTY_1 + S2_END_RWK_QTY_1 + S3_END_RWK_QTY_1 ELSE 0 END)" + "\n");
            strSqlString.Append("                                                                                                                          WHEN T0500_CNT + T0960_CNT > 1 THEN (CASE WHEN OPER IN ('T0960') THEN S1_END_QTY_1 + S2_END_QTY_1 + S3_END_QTY_1 + S1_END_RWK_QTY_1 + S2_END_RWK_QTY_1 + S3_END_RWK_QTY_1 ELSE 0 END)" + "\n");
            strSqlString.Append("                                                                                                                          WHEN OPER IN ('T0100', 'T0400', 'T0500', 'T0960', 'S0960') THEN S1_END_QTY_1 + S2_END_QTY_1 + S3_END_QTY_1 + S1_END_RWK_QTY_1 + S2_END_RWK_QTY_1 + S3_END_RWK_QTY_1" + "\n");
            strSqlString.Append("                                                                                                                          ELSE 0 END)" + "\n");
            strSqlString.Append("                           END) T_OUT_WEEK" + "\n"); 
            strSqlString.Append("                     , SUM(CASE WHEN WORK_DATE = '" + dayArry2[0] + "' AND OPER = 'T1200' THEN S1_END_QTY_1 + S2_END_QTY_1 + S3_END_QTY_1 + S1_END_RWK_QTY_1 + S2_END_RWK_QTY_1 + S3_END_RWK_QTY_1 ELSE 0 END) TR_OUT0" + "\n");
            strSqlString.Append("                     , SUM(CASE WHEN WORK_DATE = '" + dayArry2[1] + "' AND OPER = 'T1200' THEN S1_END_QTY_1 + S2_END_QTY_1 + S3_END_QTY_1 + S1_END_RWK_QTY_1 + S2_END_RWK_QTY_1 + S3_END_RWK_QTY_1 ELSE 0 END) TR_OUT1" + "\n");
            strSqlString.Append("                     , SUM(CASE WHEN WORK_DATE = '" + dayArry2[2] + "' AND OPER = 'T1200' THEN S1_END_QTY_1 + S2_END_QTY_1 + S3_END_QTY_1 + S1_END_RWK_QTY_1 + S2_END_RWK_QTY_1 + S3_END_RWK_QTY_1 ELSE 0 END) TR_OUT2" + "\n");
            strSqlString.Append("                  FROM (SELECT (SELECT COUNT(*) FROM MWIPFLWOPR@RPTTOMES FLW WHERE OPER = 'T0100' AND FLW.FLOW = A.FLOW) AS T0100_CNT" + "\n");
            strSqlString.Append("                             , (SELECT COUNT(*) FROM MWIPFLWOPR@RPTTOMES FLW WHERE OPER = 'T0500' AND FLW.FLOW = A.FLOW) AS T0500_CNT" + "\n");
            strSqlString.Append("                             , (SELECT COUNT(*) FROM MWIPFLWOPR@RPTTOMES FLW WHERE OPER = 'T0960' AND FLW.FLOW = A.FLOW) AS T0960_CNT" + "\n");
            strSqlString.Append("                             , A.*" + "\n");
            strSqlString.Append("                          FROM RSUMWIPMOV A" + "\n");
            strSqlString.Append("                         WHERE 1=1 " + "\n");            
            strSqlString.Append("                           AND A.FACTORY = '" + cdvFactory.Text + "' " + "\n");
            //strSqlString.Append("                   AND FACTORY = '" + GlobalVariable.gsTestDefaultFactory + "' " + "\n");
            strSqlString.Append("                           AND A.MAT_VER = 1 " + "\n");
            strSqlString.Append("                           AND A.OPER IN ('T0000','T0100','T0400', 'T0500', 'T0960', 'T1200', 'S0960') " + "\n");
            strSqlString.Append("                           AND A.LOT_TYPE = 'W' " + "\n");
            strSqlString.Append("                           AND A.CM_KEY_2 = 'PROD' " + "\n");
            
            if (cdvLotType.Text != "ALL")
            {
                strSqlString.Append("                           AND A.CM_KEY_3 LIKE '" + cdvLotType.Text + "'" + "\n");
            }

            if (Convert.ToInt32(FindWeek.StartDay_LastWeek) < Convert.ToInt32(Last_Month_Last_day))
            {
                strSqlString.Append("                           AND A.WORK_DATE BETWEEN '" + FindWeek.StartDay_LastWeek + "' AND '" + date + "'" + "\n");
            }
            else
            {
                strSqlString.Append("                           AND A.WORK_DATE BETWEEN '" + Last_Month_Last_day + "' AND '" + date + "'" + "\n");
            }
            strSqlString.Append("               ) " + "\n");
            //strSqlString.Append("                   AND WORK_DATE BETWEEN '" + dayArry2[0] + "' AND '" + dayArry2[2] + "'" + "\n");
            strSqlString.Append("               GROUP BY MAT_ID " + "\n");
            strSqlString.Append("               ) T_INOUT  " + "\n");
            strSqlString.Append("             , ( " + "\n");
            strSqlString.Append("                SELECT MAT_ID " + "\n");
            strSqlString.Append("                     , SUM(QTY_1) AS FGS " + "\n");

            if (DateTime.Now.ToString("yyyyMMdd") == cdvDate.SelectedValue())
            {
                strSqlString.Append("                  FROM RWIPLOTSTS " + "\n");
                strSqlString.Append("                 WHERE 1=1 " + "\n");
            }
            else
            {
                strSqlString.Append("                  FROM RWIPLOTSTS_BOH " + "\n");
                strSqlString.Append("                 WHERE CUTOFF_DT = '" + cdvDate.SelectedValue() + "22' " + "\n");
            }
                        
            strSqlString.Append("                   AND FACTORY = 'FGS' " + "\n");
            strSqlString.Append("                   AND MAT_VER = 1 " + "\n");
            strSqlString.Append("                   AND LOT_DEL_FLAG = ' ' " + "\n");
            
            if (cdvLotType.Text != "ALL")
            {
                strSqlString.Append("                   AND LOT_CMF_5 LIKE '" + cdvLotType.Text + "'" + "\n");
            }

            strSqlString.Append("                 GROUP BY MAT_ID " + "\n");
            strSqlString.Append("               ) FGS  " + "\n");
            strSqlString.Append("         WHERE 1 = 1 " + "\n");           
            strSqlString.Append("           AND MAT.MAT_ID = PLAN.MAT_ID(+) " + "\n");
            strSqlString.Append("           AND MAT.MAT_ID = W_PLAN.MAT_ID(+) " + "\n");
            strSqlString.Append("           AND MAT.MAT_ID = SHP.MAT_ID(+) " + "\n");            
            strSqlString.Append("           AND MAT.MAT_ID = T_INOUT.MAT_ID(+) " + "\n");
            strSqlString.Append("           AND MAT.MAT_ID = FGS.MAT_ID(+) " + "\n");
            strSqlString.Append("      GROUP BY MAT.MAT_GRP_1, MAT.MAT_GRP_2, MAT.MAT_GRP_3, MAT.MAT_GRP_4, MAT.MAT_GRP_5, MAT.MAT_GRP_6, MAT.MAT_GRP_7, MAT.MAT_GRP_8, MAT.MAT_CMF_10, MAT.MAT_ID, MAT.MAT_CMF_7, MAT.MAT_CMF_13" + "\n");
            strSqlString.Append("     ) A  " + "\n");
            strSqlString.Append("   , ( " + "\n");
            strSqlString.Append("      SELECT LOT.MAT_ID, MAT.MAT_GRP_3 " + "\n");
            strSqlString.Append("           , SUM(DECODE(OPER_GRP_1, 'HMK3T', DECODE(MAT.MAT_GRP_3,'COB',ROUND(QTY/TO_NUMBER(DECODE(MAT.MAT_CMF_13,' ',1,MAT.MAT_CMF_13)),0),QTY), 0)) V0 " + "\n");
            strSqlString.Append("           , SUM(DECODE(OPER_GRP_1, 'TEST', DECODE(LOT_STATUS, 'WAIT', DECODE(HOLD_FLAG, ' ', DECODE(MAT.MAT_GRP_3,'COB',ROUND(QTY/TO_NUMBER(DECODE(MAT.MAT_CMF_13,' ',1,MAT.MAT_CMF_13)),0),QTY),0),0), 0)) V1 " + "\n");
            strSqlString.Append("           , SUM(DECODE(OPER_GRP_1, 'TEST', DECODE(LOT_STATUS, 'PROC', DECODE(HOLD_FLAG, ' ', DECODE(MAT.MAT_GRP_3,'COB',ROUND(QTY/TO_NUMBER(DECODE(MAT.MAT_CMF_13,' ',1,MAT.MAT_CMF_13)),0),QTY),0),0), 0)) V2 " + "\n");
            strSqlString.Append("           , SUM(DECODE(OPER_GRP_1, 'TEST', DECODE(HOLD_FLAG, 'Y', DECODE(MAT.MAT_GRP_3,'COB',ROUND(QTY/TO_NUMBER(DECODE(MAT.MAT_CMF_13,' ',1,MAT.MAT_CMF_13)),0),QTY),0), 0)) V3 " + "\n");
            strSqlString.Append("           , SUM(DECODE(OPER_GRP_1, 'QA1', DECODE(MAT.MAT_GRP_3,'COB',ROUND(QTY/TO_NUMBER(DECODE(MAT.MAT_CMF_13,' ',1,MAT.MAT_CMF_13)),0),QTY), 0)) V4 " + "\n");
            strSqlString.Append("           , SUM(DECODE(OPER_GRP_1, 'CAS', DECODE(MAT.MAT_GRP_3,'COB',ROUND(QTY/TO_NUMBER(DECODE(MAT.MAT_CMF_13,' ',1,MAT.MAT_CMF_13)),0),QTY), 0)) V5 " + "\n");
            strSqlString.Append("           , SUM(DECODE(OPER_GRP_1, 'OS', DECODE(MAT.MAT_GRP_3,'COB',ROUND(QTY/TO_NUMBER(DECODE(MAT.MAT_CMF_13,' ',1,MAT.MAT_CMF_13)),0),QTY), 0)) V6 " + "\n");
            strSqlString.Append("           , SUM(DECODE(OPER_GRP_1, 'QA2', DECODE(MAT.MAT_GRP_3,'COB',ROUND(QTY/TO_NUMBER(DECODE(MAT.MAT_CMF_13,' ',1,MAT.MAT_CMF_13)),0),QTY), 0)) V7 " + "\n");
            strSqlString.Append("           , SUM(DECODE(OPER_GRP_1, 'BAKE', DECODE(MAT.MAT_GRP_3,'COB',ROUND(QTY/TO_NUMBER(DECODE(MAT.MAT_CMF_13,' ',1,MAT.MAT_CMF_13)),0),QTY), 0)) V8 " + "\n");
            strSqlString.Append("           , SUM(DECODE(OPER_GRP_1, 'V/I', DECODE(MAT.MAT_GRP_3,'COB',ROUND(QTY/TO_NUMBER(DECODE(MAT.MAT_CMF_13,' ',1,MAT.MAT_CMF_13)),0),QTY), 0)) V9 " + "\n");
            strSqlString.Append("           , SUM(DECODE(OPER_GRP_1, 'TnR', DECODE(MAT.MAT_GRP_3,'COB',ROUND(QTY/TO_NUMBER(DECODE(MAT.MAT_CMF_13,' ',1,MAT.MAT_CMF_13)),0),QTY), 0)) V10" + "\n");
            strSqlString.Append("           , SUM(DECODE(OPER_GRP_1, 'P/K', DECODE(MAT.MAT_GRP_3,'COB',ROUND(QTY/TO_NUMBER(DECODE(MAT.MAT_CMF_13,' ',1,MAT.MAT_CMF_13)),0),QTY), 0)) V11 " + "\n");
            strSqlString.Append("           , SUM(DECODE(OPER_GRP_1, 'HMK4T', DECODE(MAT.MAT_GRP_3,'COB',ROUND(QTY/TO_NUMBER(DECODE(MAT.MAT_CMF_13,' ',1,MAT.MAT_CMF_13)),0),QTY), 0)) V12 " + "\n");                
            strSqlString.Append("       FROM(  " + "\n");
            strSqlString.Append("             SELECT A.FACTORY, A.MAT_ID, B.OPER_GRP_1, B.OPER_GRP_7, B.OPER_CMF_3, SUM(A.QTY_1) QTY, HOLD_FLAG, LOT_STATUS " + "\n");

            if (DateTime.Now.ToString("yyyyMMdd") == cdvDate.SelectedValue())
            {
                strSqlString.Append("               FROM RWIPLOTSTS A " + "\n");
            }
            else
            {
                strSqlString.Append("               FROM RWIPLOTSTS_BOH A " + "\n");
            }

            strSqlString.Append("                  , MWIPOPRDEF B " + "\n");
            strSqlString.Append("              WHERE 1 = 1 " + "\n");
            strSqlString.Append("                AND A.FACTORY = '" + cdvFactory.Text + "' " + "\n");
            //strSqlString.Append("                AND A.FACTORY = '" + GlobalVariable.gsTestDefaultFactory + "' " + "\n");
            strSqlString.Append("                AND A.MAT_VER = 1 " + "\n");
            strSqlString.Append("                AND A.LOT_DEL_FLAG = ' ' " + "\n");
            strSqlString.Append("                AND A.FACTORY = B.FACTORY " + "\n");
            strSqlString.Append("                AND A.OPER = B.OPER " + "\n");
            
            if (cdvLotType.Text != "ALL")
            {
                strSqlString.Append("                AND A.LOT_CMF_5 LIKE '" + cdvLotType.Text + "'" + "\n");
            }


            if (DateTime.Now.ToString("yyyyMMdd") != cdvDate.SelectedValue())
            {
                strSqlString.Append("                AND A.CUTOFF_DT = '" + cdvDate.SelectedValue() + "22'     " + "\n");
            }

            strSqlString.Append("              GROUP BY A.MAT_ID, A.FACTORY, B.OPER_GRP_1, B.OPER_GRP_7, B.OPER_CMF_3, HOLD_FLAG, LOT_STATUS " + "\n");
            strSqlString.Append("           ) LOT " + "\n");
            strSqlString.Append("           , MWIPMATDEF MAT " + "\n");
            strSqlString.Append("      WHERE 1 = 1 " + "\n");
            strSqlString.Append("        AND LOT.FACTORY = MAT.FACTORY " + "\n");
            strSqlString.Append("        AND LOT.MAT_ID = MAT.MAT_ID " + "\n");
            strSqlString.Append("        AND MAT.DELETE_FLAG <> 'Y' " + "\n");
            strSqlString.Append("        AND MAT.MAT_GRP_2 <> '-' " + "\n");
            strSqlString.Append("      GROUP BY LOT.MAT_ID ,MAT.MAT_GRP_3  " + "\n");            
            strSqlString.Append("     ) B " + "\n");
            strSqlString.Append("     , MWIPMATDEF C " + "\n");
            strSqlString.Append(" WHERE 1 = 1 " + "\n");
            strSqlString.Append("   AND C.FACTORY = '" + cdvFactory.Text + "' " + "\n");
            //strSqlString.Append("   AND C.FACTORY IN ('" + GlobalVariable.gsTestDefaultFactory + "', 'HMKE1') " + "\n");
            strSqlString.Append("   AND A.MAT_ID = B.MAT_ID(+)" + "\n");
            strSqlString.Append("   AND A.MAT_ID = C.MAT_ID(+)" + "\n");
            strSqlString.Append("   AND C.DELETE_FLAG = ' ' " + "\n");
            strSqlString.Append("   AND C.MAT_GRP_3 <> 'COB' " + "\n");
            strSqlString.AppendFormat("           AND C.MAT_ID LIKE '" + txtProduct.Text.ToString().Trim() + "' " + "\n");
            //�� ��ȸ�� ���� SQL�� ����                        
            if (udcWIPCondition1.Text != "ALL" && udcWIPCondition1.Text != "")
                strSqlString.AppendFormat("   AND C.MAT_GRP_1 {0}" + "\n", udcWIPCondition1.SelectedValueToQueryString);

            if (udcWIPCondition2.Text != "ALL" && udcWIPCondition2.Text != "")
                strSqlString.AppendFormat("   AND C.MAT_GRP_2 {0} " + "\n", udcWIPCondition2.SelectedValueToQueryString);

            if (udcWIPCondition3.Text != "ALL" && udcWIPCondition3.Text != "")
                strSqlString.AppendFormat("   AND C.MAT_GRP_3 {0} " + "\n", udcWIPCondition3.SelectedValueToQueryString);

            if (udcWIPCondition4.Text != "ALL" && udcWIPCondition4.Text != "")
                strSqlString.AppendFormat("   AND C.MAT_GRP_4 {0} " + "\n", udcWIPCondition4.SelectedValueToQueryString);

            if (udcWIPCondition5.Text != "ALL" && udcWIPCondition5.Text != "")
                strSqlString.AppendFormat("   AND C.MAT_GRP_5 {0} " + "\n", udcWIPCondition5.SelectedValueToQueryString);

            if (udcWIPCondition6.Text != "ALL" && udcWIPCondition6.Text != "")
                strSqlString.AppendFormat("   AND C.MAT_GRP_6 {0} " + "\n", udcWIPCondition6.SelectedValueToQueryString);

            if (udcWIPCondition7.Text != "ALL" && udcWIPCondition7.Text != "")
                strSqlString.AppendFormat("   AND C.MAT_GRP_7 {0} " + "\n", udcWIPCondition7.SelectedValueToQueryString);

            if (udcWIPCondition8.Text != "ALL" && udcWIPCondition8.Text != "")
                strSqlString.AppendFormat("   AND C.MAT_GRP_8 {0} " + "\n", udcWIPCondition8.SelectedValueToQueryString);

            if (udcWIPCondition9.Text != "ALL" && udcWIPCondition9.Text != "")
                strSqlString.AppendFormat("   AND C.MAT_GRP_9 {0} " + "\n", udcWIPCondition9.SelectedValueToQueryString);

            //strSqlString.AppendFormat(" GROUP BY {0},G.MAT_CMF_13 " + "\n", QueryCond1);
            //strSqlString.Append(" GROUP BY C.MAT_GRP_1 , C.MAT_GRP_3 " + "\n");
            //1212
            strSqlString.Append(" GROUP BY " + QueryCond2 + "\n");



            strSqlString.Append("HAVING (" + "\n");
            strSqlString.Append("         NVL(SUM(A.MON_PLAN), 0)+ " + "\n");
            strSqlString.Append("         NVL(SUM(A.ORI_PLAN), 0)+ " + "\n");
            strSqlString.Append("         NVL(SUM(A.SHIP_MON), 0)+ " + "\n");
            strSqlString.Append("         NVL(SUM(A.SHP0), 0)+ NVL(SUM(A.SHP1), 0)+ NVL(SUM(A.SHP2), 0)+ " + "\n");
            strSqlString.Append("         NVL(SUM(A.RCV0), 0)+ NVL(SUM(A.RCV1), 0)+ NVL(SUM(A.RCV2), 0)+ " + "\n");
            strSqlString.Append("         NVL(SUM(A.T_IN0), 0)+ NVL(SUM(A.T_IN1), 0)+ NVL(SUM(A.T_IN2), 0)+ " + "\n");
            strSqlString.Append("         NVL(SUM(A.T_OUT0), 0)+ NVL(SUM(A.T_OUT1), 0)+ NVL(SUM(A.T_OUT2), 0)+ " + "\n");
            strSqlString.Append("         NVL(SUM(B.V0), 0)+ NVL(SUM(B.V1), 0)+ NVL(SUM(B.V2), 0)+ NVL(SUM(B.V3), 0)+ NVL(SUM(B.V4), 0)+ " + "\n");
            strSqlString.Append("         NVL(SUM(B.V5), 0)+ NVL(SUM(B.V6), 0)+ NVL(SUM(B.V7), 0)+ NVL(SUM(B.V8), 0)+ NVL(SUM(B.V9), 0)+ " + "\n");
            strSqlString.Append("         NVL(SUM(B.V10), 0)+ NVL(SUM(B.V11), 0)+ NVL(SUM(B.V12), 0)+ NVL(SUM(A.FGS), 0)+ NVL(SUM(A.WEEK_PLAN), 0)+ " + "\n");
            strSqlString.Append("         NVL(SUM(A.SHIP_WEEK), 0)+ NVL(SUM(A.PLAN1), 0)+ NVL(SUM(A.PLAN2), 0) " + "\n");
            strSqlString.Append("       ) <> 0" + "\n");

            //strSqlString.AppendFormat("  ORDER BY {0} " + "\n", QueryCond1);
            strSqlString.Append(" ORDER BY DECODE(C.MAT_GRP_1, 'SE', 1, 'IM', 2, 'FC', 3, 'AB', 4, 5), " + QueryCond3 + "\n");

            if (GlobalVariable.gsUserGroup == "ADMIN_GROUP" || GlobalVariable.gsUserGroup == "HANA_ADMIN_GROUP")
            {
                System.Windows.Forms.Clipboard.SetText(strSqlString.ToString());
            }

            return strSqlString.ToString();
        }

        #endregion


        #region EVENT ó��
        /// <summary>
        /// 6. View ��ư Action
        /// </summary>        ///         
        private void btnView_Click(object sender, EventArgs e)
        {                       
            DataTable dt = null;

            if (CheckField() == false) return;

            GridColumnInit();
            LabelTextChange();

            spdData_Sheet1.RowCount = 0;

            try
            {
                LoadingPopUp.LoadIngPopUpShow(this);
                
                dt = CmnFunction.oComm.GetFuncDataTable("DYNAMIC", MakeSqlString());
                                
                if (dt.Rows.Count == 0)
                {
                    dt.Dispose();
                    LoadingPopUp.LoadingPopUpHidden();
                    CmnFunction.ShowMsgBox(RptMessages.GetMessage("STD010", GlobalVariable.gcLanguage));
                    return;
                }

                //�׷��� ���� 1������ SubTotal�� ����ϱ� ���ؼ� ù��° ���� ������
                int sub = ((udcTableForm)(this.btnSort.BindingForm)).GetCheckedValue(2); 

                int[] rowType = spdData.RPT_DataBindingWithSubTotal(dt, 0, sub+1, 11, null, null, btnSort);                
                //����Ÿ���̺�, ��Ż ������ ���ѹ�, ���� ���� ������Ż � ������, ù������ ���° ������ TOTAL �������
                //spdData.Sheets[0].FrozenColumnCount = 3;
                //spdData.RPT_AutoFit(false);
                //spdData.DataSource = dt;
                //Total�κ� ������
                spdData.RPT_FillDataSelectiveCells("Total", 0, 11, 0, 1, true, Align.Center, VerticalAlign.Center);

                //spdData.RPT_AutoFit(false);
                
                if (ckbRev.Checked == false)
                {
                    spdData.RPT_SetPerSubTotalAndGrandTotal(1, 15, 11, 16);
                }
                else
                {
                    spdData.RPT_SetPerSubTotalAndGrandTotal(1, 15, 12, 16);
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
                
        // ���� ������ �������� ��������
        private string MakeSqlString2(string year, string date)
        {
            StringBuilder sqlString = new StringBuilder();

            sqlString.Append("SELECT MIN(SYS_DATE-1) " + "\n");
            sqlString.Append("  FROM MWIPCALDEF " + "\n");
            sqlString.Append(" WHERE 1=1" + "\n");
            sqlString.Append("   AND CALENDAR_ID='SE'" + "\n");
            sqlString.Append("   AND PLAN_YEAR='" + year + "'\n");
            sqlString.Append("   AND PLAN_WEEK=(" + "\n");
            sqlString.Append("                  SELECT PLAN_WEEK " + "\n");
            sqlString.Append("                    FROM MWIPCALDEF " + "\n");
            sqlString.Append("                   WHERE 1=1 " + "\n");
            sqlString.Append("                     AND CALENDAR_ID='SE' " + "\n");
            sqlString.Append("                     AND SYS_DATE=TO_CHAR(TO_DATE('" + date + "','YYYYMMDD'),'YYYYMMDD')" + "\n");
            sqlString.Append("                 )" + "\n");

            return sqlString.ToString();
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
                Condition.AppendFormat("��������: {0}     today: {1}      workday: {2}     ǥ��������: {3} " + "\n", cdvDate.Text, lblToday.Text.ToString(), lblLastDay.Text.ToString(), lblJindo.Text.ToString());
                //Condition.AppendFormat("today: {0}    workday: {1}     ǥ��������: {2} " + lblToday.Text.ToString() , lblLastDay.Text.ToString(), lblJindo.Text.ToString());
                //Condition.Append(" ||   ");
                //Condition.AppendFormat("���Ͻ�������: {0}    MEMO:   {1}     ������: {2}     ������: {3}    �ܿ��ϼ�: " + lblRemain2.Text.ToString(), lblYesterday.Text.ToString(), lblMemo.Text.ToString(), lblMagam2.Text.ToString(), lblJindo2.Text.ToString());

                ExcelHelper.Instance.subMakeExcel(spdData, this.lblTitle.Text, Condition.ToString(), null, true);
                //spdData.ExportExcel();            
            }
        }

        /// <summary>
        /// Factory ����
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cdvFactory_ValueSelectedItemChanged(object sender, Miracom.UI.MCCodeViewSelChanged_EventArgs e)
        {
            this.SetFactory(cdvFactory.txtValue);
            //cdvLotType.sFactory = cdvFactory.txtValue;
        }

        /// <summary>
        /// 7. ��� Lebel ǥ��
        /// </summary>
        private void LabelTextChange()
        {
            int remain = 0;

            FindWeek = CmnFunction.GetWeekInfo(cdvDate.SelectedValue(), "SE");
            
            //string getStartDate = cdvDate.Value.ToString("yyyyMM") + "01";
            DateTime getStartDate = Convert.ToDateTime(cdvDate.Value.ToString("yyyy-MM") + "-01");

            string getEndDate = getStartDate.AddMonths(1).AddDays(-1).ToString("yyyyMMdd");
            //string getDate = cdvDate.Value.ToString("yyyy-MM-dd");

            string strDate = cdvDate.Value.ToString("yyyyMMdd");
            
            // ASSY ������ LSI=�ش�� ���� - 2�� ����,MEMORY �ش�� ���� �������� ������ �����.
            //string magam1 = dt.Rows[0][0].ToString().Substring(8, 2);
            //string magam2 = dt.Rows[0][1].ToString().Substring(8, 2);
            string selectday = strDate.Substring(6, 2);
            string lastday = getEndDate.Substring(6, 2);

            double jindo = (Convert.ToDouble(selectday)) / Convert.ToDouble(lastday) * 100;

            //double jindo = (Convert.ToDouble(selectday)-1) / Convert.ToDouble(magam1) * 100;
            //double jindo2 = (Convert.ToDouble(selectday)-1) / Convert.ToDouble(magam2) * 100

            // ������ �Ҽ��� 1°�ڸ� ���� ǥ�� (2009.08.17 ������)
            jindoPer = Math.Round(Convert.ToDecimal(jindo),1);

            //int jindoPer = Convert.ToInt32(jindo);
            //int jindoPer2 = Convert.ToInt32(jindo2);
            
            //������ȸ�� ��� ��ȸ������ REALTIME
            //if (DateTime.Now.ToString("yyyyMMdd").Equals(strDate))
            //{
            //    dayArry[0] = cdvDate.Value.AddDays(-3).ToString("MM.dd");
            //    dayArry[1] = cdvDate.Value.AddDays(-2).ToString("MM.dd");
            //    dayArry[2] = cdvDate.Value.AddDays(-1).ToString("MM.dd");

            //    dayArry2[0] = cdvDate.Value.AddDays(-3).ToString("yyyyMMdd");
            //    dayArry2[1] = cdvDate.Value.AddDays(-2).ToString("yyyyMMdd");
            //    dayArry2[2] = cdvDate.Value.AddDays(-1).ToString("yyyyMMdd");
                
            //}
            //else
            //{
                dayArry[0] = cdvDate.Value.AddDays(-2).ToString("MM.dd");
                dayArry[1] = cdvDate.Value.AddDays(-1).ToString("MM.dd");
                dayArry[2] = cdvDate.Value.ToString("MM.dd");

                dayArry2[0] = cdvDate.Value.AddDays(-2).ToString("yyyyMMdd");
                dayArry2[1] = cdvDate.Value.AddDays(-1).ToString("yyyyMMdd");
                dayArry2[2] = cdvDate.Value.ToString("yyyyMMdd");
            //}

            lblToday.Text = selectday + " day";
            lblLastDay.Text = lastday + " day";

            // ������ȸ�� ��� �ܿ��Ͽ� ���� ������.
            if (DateTime.Now.ToString("yyyyMMdd").Equals(strDate))
            {
                remain = (Convert.ToInt32(lastday) - Convert.ToInt32(selectday) + 1);
            }
            else
            {
                remain = (Convert.ToInt32(lastday) - Convert.ToInt32(selectday));
            }
            lblRemain.Text = remain.ToString() + " day"; 
            lblJindo.Text = jindoPer.ToString() + "%";

        }
        #endregion

        private void cdvFactory_ValueButtonPress(object sender, EventArgs e)
        {
            string strQuery = string.Empty;

            strQuery += "SELECT FACTORY AS Code, FAC_DESC AS Data" + "\n";
            strQuery += "  FROM MWIPFACDEF " + "\n";
            strQuery += " WHERE FACTORY IN ('" + GlobalVariable.gsTestDefaultFactory + "', 'HMKE1') " + "\n";
            strQuery += "   AND FAC_GRP_5 = 'Y' " + "\n";
            strQuery += " ORDER BY FAC_GRP_4 DESC" + "\n";

            cdvFactory.sDynamicQuery = strQuery;
        }
    }       
}
