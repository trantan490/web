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
    public partial class PRD010204 : Miracom.SmartWeb.UI.Controls.udcCUSReport002
    {
        string[] dayArry = new string[3];
        string[] dayArry2 = new string[3];
        decimal jindoPer;
        GlobalVariable.FindWeek FindWeek = new GlobalVariable.FindWeek();

        /// <summary>
        /// Ŭ  ��  ��: PRD010204<br/>
        /// Ŭ�������: ASSY ���� ���� ��Ȳ<br/>
        /// ��  ��  ��: ������<br/>
        /// �����ۼ���: 2009-10-06<br/>
        /// ��  ����: ASSY ���� ���� ��Ȳ(���¼� ��û)<br/>
        /// ����  ����: <br/> 
        /// 2010-02-03-������ : ������, A/O ������ Return ������ A/O ������ ������ (���¼� ��û)
        /// 2010-03-31-������ : GATE �����׷� WB�� ���� �߰� ��.(���¼� ��û)
        /// 2011-02-10-������ : S/P ����� ���� SAW -> D/A ����� ���� �ǵ��� ���� (���¼� ��û)
        /// 2011-02-10-������ : ����, ���� ���� ��ȸ�� 3DAY ������ ���� ���� �� 3DAY�� ���� �� (���¼� ��û)
        /// 2011-02-11-������ : SAW 3DAY ���� ǥ�� �߰� (���¼� ��û)
        /// 2011-03-04-������ : �Ｚ ��ǰ�� ���ؼ� MAJOR CODE �߰� ǥ�� ��û (���¼� ��û)
        /// 2011-03-07-������ : MCP ��ǰ�� ��� ����� 1st, Middle, Merge �����͸� ǥ�� (���¼� ��û)
        ///                     �ű� VIEW ���̺� (VWIPMATDEF) ������̳� �� �� ���������� ����ü MCP ��ǰ�� TYPE �������� �Է½ÿ��� MWIPMATDEF�� �����Ͽ� ��� �ʿ�. 
        /// 2011-03-23-������ : MCP ��ǰ�� ��� ������ (WF,SW -> 1st,Merge) , (DA ~ WB -> SD2 : A0402,A0602  SD3: A0403,A0603  SD4: A0404,A0604....) �����͸� ǥ�� (���¼� ��û)
        /// 2011-03-28-������ : DDP, QDP ��ǰ�� ��쿡�� MCP ������ �����ϰ� ���� (���¼� ��û)
        /// 2011-08-01-������ : BGN PKG �� ��� COB �� �����ϰ� WAFER �������� ǥ�� ��. (���¼� ��û)
        /// 2011-09-15-������ : BGN PKG ��� �κе� WAFER �������� ǥ��, SST �������� A1760 ���� ���� ���� ��. (���¼� ��û)
        /// 2011-10-15-��ο� : ����ȹ Rev�߰�. (���¼� ��û)
        /// 2011-11-02-��ο� : ��ü �� Other�� ���� ��ü���� ��� �ɰ��� ���� �� ��(���¼� ��û)
        /// 2011-11-02-��ο� : ���� ���̺а� ���� �����ǥ ������ �ʰ� �ϱ�(���¼� ��û)
        /// 2011-11-02-��ο� : �޼��� (LIGIC: �������/�����ǥ)--> ������ ����(LIGIC: �������/����ȹRev) (���¼� ��û)
        /// 2011-11-02-��ο� : �����Ȳ���� Stock ����� Part �� ��ǰ�׷�(1~10) Family ������ '-' ���� �� �ִ� ���� ���ܽ��� ��� (���¼� ��û)
        /// 2011-11-09-��ο� : ���� ���� �ڵ尡 �ƴ϶� ��Ī���� ǥ��, TAT���� �� 3���� ����,��ȸ ��¥�� �����̸� ���� TAT�� ���� ��¥�� ��ȸ��¥ TAT (���¼� ��û)
        /// 2011-11-21-������ : �� ��ȹ ��ǰ �ߺ� ���� ����
        /// 2011-12-26-������ : MWIPCALDEF �� �۳�,���� ������ ���� ��ġ�� ���� �߻����� SYS_YEAR -> PLAN_YEAR ���� ����
        /// 2012-06-12-������ : PKG ������ PKG2 �������� ���� (���¼� ��û)
        /// 2012-10-19-��ο� : Pkg code : ��JRT�� �� ���ؼ� ����(���¼� K ��û) (���� 3Stack ��ǰ������ DA1-WB1-DA2-WB2�� �Ǿ� ����)
        /// 2013-01-31-������ : Pkg code : ��JWM�� �� ���ؼ� ����(�輺�� ��û) (���� 3Stack ��ǰ������ DA1-WB1-DA2-WB2�� �Ǿ� ����)
        /// 2013-06-04-��ο� : �׷��� ������ ����ں� ���� : Type1, Lead count, Pkg Code ���� �߰�(���¼�K)
        ///                     comp. ������ ���� Logic �ݿ� 
        ///                     Comp. ���� : Auto  Loss ó���ϴ� PKG �� ������� ��.  
        ///                     Wafer �� Saw ���� (1st Chip ����)/2
        ///                     ��� ��Ȳ : Wafer ~ Saw ���� (A0000~A0395) �� ����� 1/2 �Ѵ�.
        ///                     ���� Comp. ���� : �Ｚ 'JRT'/'JWM'/'JZH' PKG Code �� ������ �ִ� ��ǰ
        /// 2013-06-07-������ : SE ��ǰ �� ��ȹ REV ���� ���� OMS �����Ϳ��� �� ��ȹ UPLOAD �����ͷ� ���� �� (������ ��û)
        /// 2013-06-11-��ο� : 1st Part no �� SEKS% �� ���۵Ǵ� Stack(MCP) ��ǰ�� ���, 
        ///                     Wafer �԰� /SAW ���� : 2�� Part �� ��������, 
        ///                     ����� ��� 2�� Part  �� Merge Part �� ���ؼ��� ǥ��
        ///                     Stack ��ǰ�� ��� 1��, middle , Middle1, Merge � ���� ��� �� ����
        /// 2013-07-03-��ο� : * ������� ������ȹ �� ���� �߰�,  �����Ȳ ��ġ ����, AO ���� ��ġ ����
        ///                     * Plan : ERP ��ȹ  -> ���� ���� ���� (���� �����) 
        /// 2013-07-10-��ο� : HX ������ 06�� ���� �� �ܴ� 22�� (���¼�K)
        /// 2013-07-17-������ : �Ｚ �޸� MCP ��ǰ�� Merge ��Ʈ �������� ���� �����Ѵ�. (���¼� ��û)
        /// 2013-07-31-��ο� : HX ������ 22�� �������� ���� (���¼�K)
        /// 2013-07-31-��ο� : DETAIL ���� ��� Ȱ��ȭ �� Part no �� ��ȸ�� �� �ִ� ��� �߰�
        /// 2013-08-09-������ : EMMC Comp ��ǰ�� ��� ǥ�ý� A0250 ������ Comp ���� ���� �ǵ��� ����..A0250 ������ �����׷�� D/A �̳� ���ܷ� (���¼� ��û)
        /// 2013-08-26-������ : ��ȹ ���̺� ���� CWIPPLNWEK_N -> RWIPPLNWEK        
        /// 2013-08-30-������ : DA/WB FINAL ��� ��ȸ �ǵ��� �߰� -> MCP ���� 1st, Middle % ���� (WB_BEFORE) & MCP ���� Merge ����, �̱�Ĩ ���� (WB_AFTER)
        /// 2014-03-26-������ : �ְ� ������ ��ȸ ���� ���� ������ ��������..
        /// 2014-04-16-������ : �Ҽ��� ���� ����
        /// 2014-04-21-������ : ��ǰ�԰� �κ� �� ���� �������� ���� VSUMWIPOUT ���̺� ��� (���¼� ��û)
        /// 2014-07-15-������ : GROUP ���� ��ü �߰� �� (�躸�� ��û)
        /// 2014-08-07-������ : 6���� ���� �̷� �÷� �߰� (��Ǽ�D ��û)
        /// 2014-09-02-������ : HX ��ǰ ��� -> Operation �� A0015�� �ִ� ��ǰ�� ��� [(A0000~A0015) / Auto loss + A0016 ���� ����� ��] (���¼�K ��û)
        ///                                     Operation �� A0395�� �ִ� ��ǰ�� ��� [(A0000~A0395) / Auto loss + A0396 ���� ����� ��]
        ///                                     Operation �� A0015, A0395 �Ѵ� �ִ� ��ǰ [(A0000~A0015) / Auto loss] + [(A0016~A035) / (Auto loss/2)] + A0396 ���� ����� ��
        /// 2015-06-15-������ : DA/WB 9�� ���� ���� ���� �ǵ��� ���� (���¼�K ��û)
        /// 2015-09-14-������ : ��� ���� ���� - '-', '1st', 'Middle%', 'Merge' �� ���� (���¼�K ��û)
        /// 2015-09-21-������ : ���� �� �ϵ� �ڵ� �Ǿ� �ִ°��� ���������� ���� (���¼�K ��û)
        /// 2018-01-03-������ : SubTotal, GrandTotal ��հ� ���ϱ� Function ����
        ///                     Order by �κ� �׷� ������ ���� ��. �������� ���� �Ǿ� �־���.
        /// 2018-01-22-������ : SubTotal, GrandTotal ����� ���ϱ� Function ����
        /// 2018-06-11-������ : PO �̹��� HOLD CODE ���� - H71, H54 (�輺������ ��û)
        /// </summary>
        public PRD010204()
        {
            InitializeComponent();                                   
            SortInit();
            cdvDate.Value = DateTime.Now;
            GridColumnInit();            
            this.SetFactory(GlobalVariable.gsAssyDefaultFactory);
            cdvFactory.Text = GlobalVariable.gsAssyDefaultFactory;
            this.cdvFactory.sFactory = GlobalVariable.gsAssyDefaultFactory;
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

            String ss = DateTime.Now.ToString("MM-dd");
            FindWeek = CmnFunction.GetWeekInfo(cdvDate.SelectedValue(), "OTD");


            try
            {
                if (ckbKpcs.Checked == false)
                {
                    spdData.RPT_AddBasicColumn("Customer", 0, 0, Visibles.True, Frozen.True, Align.Center, Merge.True, Formatter.String, 70);
                    spdData.RPT_AddBasicColumn("Major Code", 0, 1, Visibles.True, Frozen.True, Align.Center, Merge.True, Formatter.String, 70);
                    spdData.RPT_AddBasicColumn("Package", 0, 2, Visibles.True, Frozen.True, Align.Center, Merge.True, Formatter.String, 70);
                    spdData.RPT_AddBasicColumn("Type1", 0, 3, Visibles.False, Frozen.True, Align.Center, Merge.True, Formatter.String, 50);
                    spdData.RPT_AddBasicColumn("Type2", 0, 4, Visibles.False, Frozen.True, Align.Center, Merge.True, Formatter.String, 50);
                    spdData.RPT_AddBasicColumn("LD Count", 0, 5, Visibles.False, Frozen.True, Align.Center, Merge.True, Formatter.String, 50);
                    spdData.RPT_AddBasicColumn("PKG Code", 0, 6, Visibles.False, Frozen.True, Align.Center, Merge.True, Formatter.String, 50);
                    spdData.RPT_AddBasicColumn("Density", 0, 7, Visibles.False, Frozen.True, Align.Center, Merge.True, Formatter.String, 50);
                    spdData.RPT_AddBasicColumn("Generation", 0, 8, Visibles.False, Frozen.True, Align.Center, Merge.True, Formatter.String, 50);
                    spdData.RPT_AddBasicColumn("Pin Type", 0, 9, Visibles.False, Frozen.True, Align.Left, Merge.True, Formatter.String, 120);
                    spdData.RPT_AddBasicColumn("Product", 0, 10, Visibles.False, Frozen.True, Align.Left, Merge.True, Formatter.String, 120);                    

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

                    spdData.RPT_AddBasicColumn(FindWeek.ThisWeek.Substring(0, 4) + "." + FindWeek.ThisWeek.Substring(4, 2) + " ����", 0, 18, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.String, 70);
                    spdData.RPT_AddBasicColumn("Plan", 1, 18, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 70);
                    spdData.RPT_AddBasicColumn("cumulative total", 1, 19, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 70);
                    spdData.RPT_AddBasicColumn("actual", 2, 19, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 90);
                    spdData.RPT_AddBasicColumn("residual quantity", 1, 20, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 90);
                    spdData.RPT_MerageHeaderColumnSpan(0, 18, 3);
                    spdData.RPT_MerageHeaderRowSpan(1, 18, 2);
                    spdData.RPT_MerageHeaderRowSpan(1, 20, 2);
                  
                    if (DateTime.Now.ToString("yyyyMMdd") == cdvDate.SelectedValue())
                    {
                        spdData.RPT_AddBasicColumn("�����Ȳ (" + DateTime.Now.ToString("yy.MM.dd HH:mm") + " ����)", 0, 21, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.String, 70);
                    }
                    else
                    {
                        spdData.RPT_AddBasicColumn("�����Ȳ (" + cdvDate.Value.ToString("yy.MM.dd") + " 22:00 ����)", 0, 21, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.String, 70);
                    }

                    spdData.RPT_AddBasicColumn("HMKA3A", 1, 21, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 70);
                    spdData.RPT_AddBasicColumn("FINISH", 1, 22, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 70);
                    spdData.RPT_AddBasicColumn("MOLD", 1, 23, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 70);
                    spdData.RPT_AddBasicColumn("WB", 1, 24, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 70);
                    spdData.RPT_AddBasicColumn("DA", 1, 25, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 70);
                    spdData.RPT_AddBasicColumn("SAW", 1, 26, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 70);
                    spdData.RPT_AddBasicColumn("STOCK", 1, 27, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 70);
                    spdData.RPT_AddBasicColumn("TTL", 1, 28, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 70);
                    spdData.RPT_MerageHeaderColumnSpan(0, 21, 8);

                    spdData.RPT_AddBasicColumn("a daily goal", 0, 29, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 70);

                    spdData.RPT_AddBasicColumn("Performance by Major Operation", 0, 30, Visibles.True, Frozen.False, Align.Center, Merge.False, Formatter.String, 70);
                    spdData.RPT_AddBasicColumn("3day A/O Qty", 1, 30, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.String, 70);
                    spdData.RPT_AddBasicColumn(dayArry[0], 2, 30, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 70);
                    spdData.RPT_AddBasicColumn(dayArry[1], 2, 31, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 70);
                    spdData.RPT_AddBasicColumn(dayArry[2], 2, 32, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 70);
                    spdData.RPT_MerageHeaderColumnSpan(1, 30, 3);


                    spdData.RPT_AddBasicColumn("WF", 1, 33, Visibles.True, Frozen.False, Align.Center, Merge.False, Formatter.Number, 70);
                    spdData.RPT_AddBasicColumn(dayArry[0], 2, 33, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 70);
                    spdData.RPT_AddBasicColumn(dayArry[1], 2, 34, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 70);
                    spdData.RPT_AddBasicColumn(dayArry[2], 2, 35, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 70);
                    spdData.RPT_MerageHeaderColumnSpan(1, 33, 3);

                    spdData.RPT_AddBasicColumn("SAW", 1, 36, Visibles.True, Frozen.False, Align.Center, Merge.False, Formatter.String, 70);
                    spdData.RPT_AddBasicColumn(dayArry[0], 2, 36, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 70);
                    spdData.RPT_AddBasicColumn(dayArry[1], 2, 37, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 70);
                    spdData.RPT_AddBasicColumn(dayArry[2], 2, 38, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 70);
                    spdData.RPT_MerageHeaderColumnSpan(1, 36, 3);

                    spdData.RPT_AddBasicColumn("D/A", 1, 39, Visibles.True, Frozen.False, Align.Center, Merge.False, Formatter.String, 70);
                    spdData.RPT_AddBasicColumn(dayArry[0], 2, 39, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 70);
                    spdData.RPT_AddBasicColumn(dayArry[1], 2, 40, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 70);
                    spdData.RPT_AddBasicColumn(dayArry[2], 2, 41, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 70);
                    spdData.RPT_MerageHeaderColumnSpan(1, 39, 3);

                    spdData.RPT_AddBasicColumn("W/B", 1, 42, Visibles.True, Frozen.False, Align.Center, Merge.False, Formatter.String, 70);
                    spdData.RPT_AddBasicColumn(dayArry[0], 2, 42, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 70);
                    spdData.RPT_AddBasicColumn(dayArry[1], 2, 43, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 70);
                    spdData.RPT_AddBasicColumn(dayArry[2], 2, 44, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 70);
                    spdData.RPT_MerageHeaderColumnSpan(1, 42, 3);

                    spdData.RPT_AddBasicColumn("MOLD", 1, 45, Visibles.True, Frozen.False, Align.Center, Merge.False, Formatter.String, 70);
                    spdData.RPT_AddBasicColumn(dayArry[0], 2, 45, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 70);
                    spdData.RPT_AddBasicColumn(dayArry[1], 2, 46, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 70);
                    spdData.RPT_AddBasicColumn(dayArry[2], 2, 47, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 70);
                    spdData.RPT_MerageHeaderColumnSpan(1, 45, 3);

                    spdData.RPT_AddBasicColumn("T/F,SST", 1, 48, Visibles.True, Frozen.False, Align.Center, Merge.False, Formatter.String, 70);
                    spdData.RPT_AddBasicColumn(dayArry[0], 2, 48, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 70);
                    spdData.RPT_AddBasicColumn(dayArry[1], 2, 49, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 70);
                    spdData.RPT_AddBasicColumn(dayArry[2], 2, 50, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 70);
                    spdData.RPT_MerageHeaderColumnSpan(1, 48, 3);
                    spdData.RPT_MerageHeaderColumnSpan(0, 30, 21);

                    spdData.RPT_AddBasicColumn("TAT(HR)", 0, 51, Visibles.True, Frozen.False, Align.Center, Merge.False, Formatter.String, 70);

                    if (DateTime.Now.ToString("yyyyMMdd") == cdvDate.SelectedValue())
                    {
                        spdData.RPT_AddBasicColumn(dayArry[1], 2, 51, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Double2, 70);
                    }
                    else
                    {
                        spdData.RPT_AddBasicColumn(dayArry[2], 2, 51, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Double2, 70);
                    }
                    spdData.RPT_MerageHeaderRowSpan(0, 51, 2);

                    spdData.RPT_AddBasicColumn("6 months&#10;production history", 0, 52, Visibles.True, Frozen.False, Align.Center, Merge.False, Formatter.String, 70);

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
                    spdData.RPT_MerageHeaderRowSpan(0, 14, 3);
                    spdData.RPT_MerageHeaderRowSpan(0, 29, 3);
                    spdData.RPT_MerageHeaderRowSpan(0, 52, 3);
                    
                    spdData.RPT_MerageHeaderRowSpan(1, 21, 2);
                    spdData.RPT_MerageHeaderRowSpan(1, 22, 2);
                    spdData.RPT_MerageHeaderRowSpan(1, 23, 2);
                    spdData.RPT_MerageHeaderRowSpan(1, 24, 2);
                    spdData.RPT_MerageHeaderRowSpan(1, 25, 2);
                    spdData.RPT_MerageHeaderRowSpan(1, 26, 2);
                    spdData.RPT_MerageHeaderRowSpan(1, 27, 2);
                    spdData.RPT_MerageHeaderRowSpan(1, 28, 2);
                    spdData.RPT_ColumnConfigFromTable(btnSort);
                }
                else
                {
                    spdData.RPT_AddBasicColumn("Customer", 0, 0, Visibles.True, Frozen.True, Align.Center, Merge.True, Formatter.String, 70);
                    spdData.RPT_AddBasicColumn("Major Code", 0, 1, Visibles.True, Frozen.True, Align.Center, Merge.True, Formatter.String, 70);
                    spdData.RPT_AddBasicColumn("Package", 0, 2, Visibles.True, Frozen.True, Align.Center, Merge.True, Formatter.String, 70);
                    spdData.RPT_AddBasicColumn("Type1", 0, 3, Visibles.False, Frozen.True, Align.Center, Merge.True, Formatter.String, 50);
                    spdData.RPT_AddBasicColumn("Type2", 0, 4, Visibles.False, Frozen.True, Align.Center, Merge.True, Formatter.String, 50);
                    spdData.RPT_AddBasicColumn("LD Count", 0, 5, Visibles.False, Frozen.True, Align.Center, Merge.True, Formatter.String, 50);
                    spdData.RPT_AddBasicColumn("PKG Code", 0, 6, Visibles.False, Frozen.True, Align.Center, Merge.True, Formatter.String, 50);
                    spdData.RPT_AddBasicColumn("Density", 0, 7, Visibles.False, Frozen.True, Align.Center, Merge.True, Formatter.String, 50);
                    spdData.RPT_AddBasicColumn("Generation", 0, 8, Visibles.False, Frozen.True, Align.Center, Merge.True, Formatter.String, 50);
                    spdData.RPT_AddBasicColumn("Pin Type", 0, 9, Visibles.False, Frozen.True, Align.Left, Merge.True, Formatter.String, 120);
                    spdData.RPT_AddBasicColumn("Product", 0, 10, Visibles.False, Frozen.True, Align.Left, Merge.True, Formatter.String, 120);                    

                    if (ckbRev.Checked == false)
                    {
                        spdData.RPT_AddBasicColumn("Monthly plan", 0, 11, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Double1, 70);
                        spdData.RPT_AddBasicColumn("Monthly Plan Rev", 0, 12, Visibles.False, Frozen.False, Align.Right, Merge.False, Formatter.Double1, 70);
                        spdData.RPT_AddBasicColumn("Monthly plan difference", 0, 13, Visibles.False, Frozen.False, Align.Right, Merge.False, Formatter.Double1, 70);
                    }
                    else
                    {
                        spdData.RPT_AddBasicColumn("Monthly plan", 0, 11, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Double1, 70);
                        spdData.RPT_AddBasicColumn("Monthly Plan Rev", 0, 12, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Double1, 70);
                        spdData.RPT_AddBasicColumn("Monthly plan difference", 0, 13, Visibles.False, Frozen.False, Align.Right, Merge.False, Formatter.Double1, 70);
                    }

                    spdData.RPT_AddBasicColumn("goal", 0, 14, Visibles.False, Frozen.False, Align.Right, Merge.False, Formatter.Number, 70);

                    spdData.RPT_AddBasicColumn("Production Status", 0, 15, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.String, 70);
                    spdData.RPT_AddBasicColumn("cumulative total", 1, 15, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.String, 70);
                    spdData.RPT_AddBasicColumn("actual", 2, 15, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Double1, 70);
                    spdData.RPT_AddBasicColumn("Progress rate (%)", 2, 16, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Double1, 90);
                    spdData.RPT_AddBasicColumn("Difference", 2, 17, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Double1, 70);
                    spdData.RPT_MerageHeaderColumnSpan(1, 15, 3);
                    spdData.RPT_MerageHeaderColumnSpan(0, 15, 3);

                    spdData.RPT_AddBasicColumn(FindWeek.ThisWeek.Substring(0, 4) + "." + FindWeek.ThisWeek.Substring(4, 2) + " ����", 0, 18, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.String, 70);
                    spdData.RPT_AddBasicColumn("Plan", 1, 18, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Double1, 70);
                    spdData.RPT_AddBasicColumn("cumulative total", 1, 19, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Double1, 70);
                    spdData.RPT_AddBasicColumn("actual", 2, 19, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Double1, 90);
                    spdData.RPT_AddBasicColumn("residual quantity", 1, 20, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Double1, 90);
                    spdData.RPT_MerageHeaderColumnSpan(0, 18, 3);
                    spdData.RPT_MerageHeaderRowSpan(1, 18, 2);
                    spdData.RPT_MerageHeaderRowSpan(1, 20, 2);
                    
                    if (DateTime.Now.ToString("yyyyMMdd") == cdvDate.SelectedValue())
                    {
                        spdData.RPT_AddBasicColumn("�����Ȳ (" + DateTime.Now.ToString("yy.MM.dd HH:mm") + " ����)", 0, 21, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.String, 70);
                    }
                    else
                    {
                        spdData.RPT_AddBasicColumn("�����Ȳ (" + cdvDate.Value.ToString("yy.MM.dd") + " 22:00 ����)", 0, 21, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.String, 70);
                    }

                    spdData.RPT_AddBasicColumn("HMKA3A", 1, 21, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Double1, 70);
                    spdData.RPT_AddBasicColumn("FINISH", 1, 22, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Double1, 70);
                    spdData.RPT_AddBasicColumn("MOLD", 1, 23, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Double1, 70);
                    spdData.RPT_AddBasicColumn("WB", 1, 24, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Double1, 70);
                    spdData.RPT_AddBasicColumn("DA", 1, 25, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Double1, 70);
                    spdData.RPT_AddBasicColumn("SAW", 1, 26, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Double1, 70);
                    spdData.RPT_AddBasicColumn("STOCK", 1, 27, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Double1, 70);
                    spdData.RPT_AddBasicColumn("TTL", 1, 28, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Double1, 70);
                    spdData.RPT_MerageHeaderColumnSpan(0, 21, 8);

                    spdData.RPT_AddBasicColumn("a daily goal", 0, 29, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Double1, 70);

                    spdData.RPT_AddBasicColumn("Performance by Major Operation", 0, 30, Visibles.True, Frozen.False, Align.Center, Merge.False, Formatter.String, 70);

                    spdData.RPT_AddBasicColumn("3day A/O Qty", 1, 30, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.String, 70);
                    spdData.RPT_AddBasicColumn(dayArry[0], 2, 30, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Double1, 70);
                    spdData.RPT_AddBasicColumn(dayArry[1], 2, 31, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Double1, 70);
                    spdData.RPT_AddBasicColumn(dayArry[2], 2, 32, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Double1, 70);
                    spdData.RPT_MerageHeaderColumnSpan(1, 30, 3);


                    spdData.RPT_AddBasicColumn("WF", 1, 33, Visibles.True, Frozen.False, Align.Center, Merge.False, Formatter.Double1, 70);
                    spdData.RPT_AddBasicColumn(dayArry[0], 2, 33, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Double1, 70);
                    spdData.RPT_AddBasicColumn(dayArry[1], 2, 34, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Double1, 70);
                    spdData.RPT_AddBasicColumn(dayArry[2], 2, 35, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Double1, 70);
                    spdData.RPT_MerageHeaderColumnSpan(1, 33, 3);

                    spdData.RPT_AddBasicColumn("SAW", 1, 36, Visibles.True, Frozen.False, Align.Center, Merge.False, Formatter.String, 70);
                    spdData.RPT_AddBasicColumn(dayArry[0], 2, 36, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Double1, 70);
                    spdData.RPT_AddBasicColumn(dayArry[1], 2, 37, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Double1, 70);
                    spdData.RPT_AddBasicColumn(dayArry[2], 2, 38, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Double1, 70);
                    spdData.RPT_MerageHeaderColumnSpan(1, 36, 3);

                    spdData.RPT_AddBasicColumn("D/A", 1, 39, Visibles.True, Frozen.False, Align.Center, Merge.False, Formatter.String, 70);
                    spdData.RPT_AddBasicColumn(dayArry[0], 2, 39, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Double1, 70);
                    spdData.RPT_AddBasicColumn(dayArry[1], 2, 40, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Double1, 70);
                    spdData.RPT_AddBasicColumn(dayArry[2], 2, 41, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Double1, 70);
                    spdData.RPT_MerageHeaderColumnSpan(1, 39, 3);

                    spdData.RPT_AddBasicColumn("W/B", 1, 42, Visibles.True, Frozen.False, Align.Center, Merge.False, Formatter.String, 70);
                    spdData.RPT_AddBasicColumn(dayArry[0], 2, 42, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Double1, 70);
                    spdData.RPT_AddBasicColumn(dayArry[1], 2, 43, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Double1, 70);
                    spdData.RPT_AddBasicColumn(dayArry[2], 2, 44, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Double1, 70);
                    spdData.RPT_MerageHeaderColumnSpan(1, 42, 3);

                    spdData.RPT_AddBasicColumn("MOLD", 1, 45, Visibles.True, Frozen.False, Align.Center, Merge.False, Formatter.String, 70);
                    spdData.RPT_AddBasicColumn(dayArry[0], 2, 45, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Double1, 70);
                    spdData.RPT_AddBasicColumn(dayArry[1], 2, 46, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Double1, 70);
                    spdData.RPT_AddBasicColumn(dayArry[2], 2, 47, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Double1, 70);
                    spdData.RPT_MerageHeaderColumnSpan(1, 45, 3);

                    spdData.RPT_AddBasicColumn("T/F,SST", 1, 48, Visibles.True, Frozen.False, Align.Center, Merge.False, Formatter.String, 70);
                    spdData.RPT_AddBasicColumn(dayArry[0], 2, 48, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Double1, 70);
                    spdData.RPT_AddBasicColumn(dayArry[1], 2, 49, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Double1, 70);
                    spdData.RPT_AddBasicColumn(dayArry[2], 2, 50, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Double1, 70);
                    spdData.RPT_MerageHeaderColumnSpan(1, 48, 3);
                    spdData.RPT_MerageHeaderColumnSpan(0, 30, 21);

                    spdData.RPT_AddBasicColumn("TAT(HR)", 0, 51, Visibles.True, Frozen.False, Align.Center, Merge.False, Formatter.String, 70);

                    if (DateTime.Now.ToString("yyyyMMdd") == cdvDate.SelectedValue())
                    {
                        spdData.RPT_AddBasicColumn(dayArry[1], 2, 51, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Double2, 70);
                    }
                    else
                    {
                        spdData.RPT_AddBasicColumn(dayArry[2], 2, 51, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Double2, 70);
                    }
                    spdData.RPT_MerageHeaderRowSpan(0, 51, 2);

                    spdData.RPT_AddBasicColumn("6 months&#10;production history", 0, 52, Visibles.True, Frozen.False, Align.Center, Merge.False, Formatter.String, 70);

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
                    spdData.RPT_MerageHeaderRowSpan(0, 14, 3);
                    spdData.RPT_MerageHeaderRowSpan(0, 29, 3);
                    spdData.RPT_MerageHeaderRowSpan(0, 52, 3);

                    spdData.RPT_MerageHeaderRowSpan(1, 21, 2);
                    spdData.RPT_MerageHeaderRowSpan(1, 22, 2);
                    spdData.RPT_MerageHeaderRowSpan(1, 23, 2);
                    spdData.RPT_MerageHeaderRowSpan(1, 24, 2);
                    spdData.RPT_MerageHeaderRowSpan(1, 25, 2);
                    spdData.RPT_MerageHeaderRowSpan(1, 26, 2);
                    spdData.RPT_MerageHeaderRowSpan(1, 27, 2);
                    spdData.RPT_MerageHeaderRowSpan(1, 28, 2);
                    spdData.RPT_ColumnConfigFromTable(btnSort);

                }

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
            ((udcTableForm)(this.btnSort.BindingForm)).AddRow("CUSTOMER", "(SELECT DATA_1 FROM MGCMTBLDAT@RPTTOMES WHERE FACTORY = '" + GlobalVariable.gsAssyDefaultFactory + "' AND TABLE_NAME = 'H_CUSTOMER' AND KEY_1 = A.CUSTOMER) AS CUSTOMER", "A.MAT_GRP_1", "A.MAT_GRP_1 AS CUSTOMER", "DECODE(A.CUSTOMER, 'SE', 1, 'HX', 2, 'IM', 3, 'FC', 4, 'IG', 5, 6),CUSTOMER", true);
            ((udcTableForm)(this.btnSort.BindingForm)).AddRow("MAJOR CODE", "A.MAJOR", "A.MAT_GRP_9", "A.MAT_GRP_9 AS MAJOR", "A.MAJOR", true);
            ((udcTableForm)(this.btnSort.BindingForm)).AddRow("PACKAGE", "A.PKG", "A.MAT_GRP_10", "A.MAT_GRP_10 AS PKG", "A.PKG", true);
            ((udcTableForm)(this.btnSort.BindingForm)).AddRow("TYPE1", "A.TYPE1", "A.MAT_GRP_4", "A.MAT_GRP_4 AS TYPE1", "A.TYPE1", false);
            ((udcTableForm)(this.btnSort.BindingForm)).AddRow("TYPE2", "A.TYPE2", "A.MAT_GRP_5", "A.MAT_GRP_5 AS TYPE2", "A.TYPE2", false);
            ((udcTableForm)(this.btnSort.BindingForm)).AddRow("LD COUNT", "A.LD_COUNT", "A.MAT_GRP_6", "A.MAT_GRP_6 AS LD_COUNT", "A.LD_COUNT", false);
            ((udcTableForm)(this.btnSort.BindingForm)).AddRow("PKG CODE", "A.PKG_CODE", "A.MAT_CMF_11", "A.MAT_CMF_11 AS PKG_CODE", "A.PKG_CODE", false);
            ((udcTableForm)(this.btnSort.BindingForm)).AddRow("DENSITY", "A.DENSITY", "A.MAT_GRP_7", "A.MAT_GRP_7 AS DENSITY", "A.DENSITY", false);
            ((udcTableForm)(this.btnSort.BindingForm)).AddRow("GENERATION", "A.GENERATION", "A.MAT_GRP_8", "A.MAT_GRP_8 AS GENERATION", "A.GENERATION", false);
            ((udcTableForm)(this.btnSort.BindingForm)).AddRow("PIN TYPE", "A.PIN_TYPE", "A.MAT_CMF_10", "A.MAT_CMF_10 AS PIN_TYPE", "A.PIN_TYPE", false);
            ((udcTableForm)(this.btnSort.BindingForm)).AddRow("PRODUCT", "A.MAT_ID", "A.MAT_ID", "A.MAT_ID", "A.MAT_ID", false);            
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
            string QueryCond4;
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
            QueryCond4 = tableForm.SelectedValue4ToQueryContainNull;

            //int remain = Convert.ToInt32(lblLastDay.Text.Substring(0,2)) - Convert.ToInt32(lblToday.Text.Substring(0,2)) + 1;

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

            if (ckbKpcs.Checked == false)
            {
                strSqlString.Append("SELECT " + QueryCond1 + "\n");
                
                if (ckbRev.Checked == false)
                {
                    strSqlString.Append("     , A.MON_PLAN AS \"����ȹ\"" + "\n");
                    strSqlString.Append("     , 0 AS \"����ȹRev\"" + "\n");
                    strSqlString.Append("     , 0 AS \"����ȹ ����\"" + "\n");
                }
                else
                {
                    strSqlString.Append("     , A.ORI_PLAN AS \"����ȹ\"" + "\n");
                    strSqlString.Append("     , A.MON_PLAN AS \"����ȹRev\"" + "\n");
                    strSqlString.Append("     , A.MON_PLAN -A.ORI_PLAN AS \"����ȹ ����\"" + "\n");
                }

                strSqlString.Append("     , A.TARGET_MON, A.ASSY_MON" + "\n");
                strSqlString.Append("     , DECODE(A.MON_PLAN, 0, 0, ROUND((A.ASSY_MON/A.MON_PLAN)*100, 1)) JINDO, A.DEF, WEEK_PLAN, ASSY_WEEK, WEEK_PLAN-ASSY_WEEK AS \"�ְ�ȹ ����\"" + "\n");

                if (ckbFinal.Checked == true)
                {
                    strSqlString.Append("     , A.HMK3A, A.FINISH, A.MOLD, A.WB_AFTER, A.DA + A.WB_BEFORE, A.SAW, A.STOCK, A.HMK3A+A.FINISH+A.MOLD+A.WB_AFTER+A.WB_BEFORE+A.DA+A.SAW+A.STOCK AS TTL, A.TARGET_DAY AS \"�ϸ�ǥ\", A.AO0, A.AO1, A.AO2" + "\n");                    
                }
                else
                {
                    strSqlString.Append("     , A.HMK3A, A.FINISH, A.MOLD, A.WB, A.DA, A.SAW, A.STOCK, A.HMK3A+A.FINISH+A.MOLD+A.WB+A.DA+A.SAW+A.STOCK AS TTL, A.TARGET_DAY AS \"�ϸ�ǥ\", A.AO0, A.AO1, A.AO2 " + "\n");
                }

                strSqlString.Append("     , A.RCV0, A.RCV1, A.RCV2, A.SW0, A.SW1, A.SW2, A.DA0, A.DA1, A.DA2, A.WB0, A.WB1, A.WB2, A.MD0, A.MD1, A.MD2, A.TF0, A.TF1, A.TF2 " + "\n");

                if (DateTime.Now.ToString("yyyyMMdd") == cdvDate.SelectedValue())
                {
                    strSqlString.Append("     , ROUND(B.TAT1*24,2)" + "\n");
                
                }
                else
                {
                    strSqlString.Append("     , ROUND(B.TAT2*24,2)" + "\n");
                
                }

                strSqlString.Append("     , CASE WHEN HIST_AO > 0 THEN '��' END AS HIST_AO" + "\n");
                
            }
            else
            {
                strSqlString.Append("SELECT " + QueryCond1 + "\n");
                if (ckbRev.Checked == false)
                {
                    strSqlString.Append("     , ROUND(A.MON_PLAN/1000,1) AS \"����ȹ\"" + "\n");
                    strSqlString.Append("     , 0 AS \"����ȹ\"" + "\n");
                    strSqlString.Append("     , 0 AS \"����ȹ\"" + "\n");
                }
                else
                {
                    strSqlString.Append("     , ROUND(A.ORI_PLAN/1000,1) AS \"����ȹ\"" + "\n");
                    strSqlString.Append("     , ROUND(A.MON_PLAN/1000,1) AS \"����ȹRev\"" + "\n");
                    strSqlString.Append("     , ROUND((A.MON_PLAN -A.ORI_PLAN)/1000,1) AS \"����ȹ ����\"" + "\n");
                }

                strSqlString.Append("     , ROUND(A.TARGET_MON/1000,1), ROUND(A.ASSY_MON/1000,1)" + "\n");

                strSqlString.Append("     , DECODE(A.MON_PLAN, 0, 0, ROUND((A.ASSY_MON/A.MON_PLAN)*100, 1)) JINDO, ROUND(A.DEF/1000,1), ROUND(WEEK_PLAN/1000,1), ROUND(ASSY_WEEK/1000,1),ROUND(WEEK_PLAN/1000,1)-ROUND(ASSY_WEEK/1000,1)" + "\n");

                if (ckbFinal.Checked == true)
                {
                    strSqlString.Append("     , ROUND(A.HMK3A/1000,1) AS HMK3A, ROUND(A.FINISH/1000,1) AS FINISH, ROUND(A.MOLD/1000,1) AS MOLD, ROUND(A.WB_AFTER/1000,1) AS WB, ROUND((A.DA+A.WB_BEFORE)/1000,1) AS DA, ROUND(A.SAW/1000,1) AS SAW, ROUND(A.STOCK/1000,1) AS STOCK, ROUND((A.HMK3A+A.FINISH+A.MOLD+A.WB_AFTER+A.WB_BEFORE+A.DA+A.SAW+A.STOCK)/1000,1) AS TTL, ROUND(A.TARGET_DAY/1000,1) AS \"�ϸ�ǥ\", ROUND(A.AO0/1000,1), ROUND(A.AO1/1000,1), ROUND(A.AO2/1000,1)" + "\n");      
                }
                else
                {
                    strSqlString.Append("     , ROUND(A.HMK3A/1000,1) AS HMK3A, ROUND(A.FINISH/1000,1) AS FINISH, ROUND(A.MOLD/1000,1) AS MOLD, ROUND(A.WB/1000,1) AS WB, ROUND(A.DA/1000,1) AS DA, ROUND(A.SAW/1000,1) AS SAW, ROUND(A.STOCK/1000,1) AS STOCK, ROUND((A.HMK3A+A.FINISH+A.MOLD+A.WB+A.DA+A.SAW+A.STOCK)/1000,1) AS TTL, ROUND(A.TARGET_DAY/1000,1) AS \"�ϸ�ǥ\", ROUND(A.AO0/1000,1), ROUND(A.AO1/1000,1), ROUND(A.AO2/1000,1)" + "\n");
                }
               
                strSqlString.Append("     , ROUND(A.RCV0/1000,1), ROUND(A.RCV1/1000,1), ROUND(A.RCV2/1000,1), ROUND(A.SW0/1000,1), ROUND(A.SW1/1000,1), ROUND(A.SW2/1000,1), ROUND(A.DA0/1000,1), ROUND(A.DA1/1000,1), ROUND(A.DA2/1000,1)" + "\n");
                strSqlString.Append("     , ROUND(A.WB0/1000,1), ROUND(A.WB1/1000,1), ROUND(A.WB2/1000,1), ROUND(A.MD0/1000,1), ROUND(A.MD1/1000,1), ROUND(A.MD2/1000,1)" + "\n");
                strSqlString.Append("     , ROUND(A.TF0/1000,1), ROUND(A.TF1/1000,1), ROUND(A.TF2/1000,1)" + "\n");

                if (DateTime.Now.ToString("yyyyMMdd") == cdvDate.SelectedValue())
                {
                    strSqlString.Append("    , ROUND(B.TAT1*24,0)" + "\n");
                }
                else
                {
                    strSqlString.Append("    , ROUND(B.TAT2*24,0)" + "\n");
                }

                strSqlString.Append("     , CASE WHEN HIST_AO > 0 THEN '��' END AS HIST_AO" + "\n");
            }

            strSqlString.Append("  FROM ( " + "\n");
           
            strSqlString.Append("        SELECT " + QueryCond3 + "\n");
         
            strSqlString.Append("             , SUM(NVL(A.MON_PLAN,0)) AS MON_PLAN " + "\n");
            strSqlString.Append("             , SUM(NVL(A.ORI_PLAN,0)) AS ORI_PLAN " + "\n");
            strSqlString.Append("             , SUM(NVL(A.WEEK_PLAN,0)) AS WEEK_PLAN " + "\n");
            strSqlString.Append("             , SUM(NVL(A.ASSY_WEEK,0)) AS ASSY_WEEK " + "\n");
            strSqlString.Append("             , ROUND(((SUM(NVL(A.MON_PLAN,0)) * " + jindoPer + ") / 100),0) AS TARGET_MON " + "\n");
            strSqlString.Append("             , SUM(NVL(A.ASSY_MON,0)) AS ASSY_MON " + "\n");            
            strSqlString.Append("             , SUM(NVL(A.ASSY_MON,0)) - ROUND(((SUM(NVL(A.MON_PLAN,0)) * " + jindoPer + ") / 100),1) AS DEF   " + "\n");
            strSqlString.Append("             , SUM(NVL(A.AO0,0)) AS AO0 " + "\n");
            strSqlString.Append("             , SUM(NVL(A.AO1,0)) AS AO1 " + "\n");
            strSqlString.Append("             , SUM(NVL(A.AO2,0)) AS AO2 " + "\n");

            if (lblRemain.Text != "0 day")  // �ܿ����� 0 ���� �ƴҶ�
            {
                strSqlString.Append("             , ROUND(((SUM(NVL(A.MON_PLAN,0)) - SUM(NVL(A.ASSY_MON,0))) /" + Convert.ToInt32(lblRemain.Text.Substring(0, 2)) + "), 1) AS TARGET_DAY " + "\n");
            }
            else  // �ܿ����� 0 �� �ϴ�
            {
                strSqlString.Append("             , 0 AS TARGET_DAY " + "\n");
            }

            strSqlString.Append("             , SUM(NVL(A.RCV0,0)) AS RCV0" + "\n");
            strSqlString.Append("             , SUM(NVL(A.RCV1,0)) AS RCV1" + "\n");
            strSqlString.Append("             , SUM(NVL(A.RCV2,0)) AS RCV2" + "\n");
            strSqlString.Append("             , ROUND(SUM(NVL((CASE WHEN A.MAT_CMF_11 IN (SELECT DISTINCT(MAT_CMF_11) FROM MWIPMATDEF WHERE FACTORY = '" + GlobalVariable.gsAssyDefaultFactory + "' AND MAT_GRP_5 = '1st' AND MAT_ID LIKE 'SEKS%') THEN DECODE(A.MAT_GRP_5, '2nd', D.SW0 / COMP_CNT, 0)" + "\n");
            strSqlString.Append("                                   WHEN A.MAT_GRP_3 IN ('COB', 'BGN') THEN D.SW0 / NET_DIE" + "\n");
            strSqlString.Append("                                   WHEN MAT_GRP_4 NOT IN ('-','FD','FU') AND MAT_GRP_5 <> '-' THEN CASE WHEN A.MAT_GRP_5 IN ('1st','Merge') OR MAT_GRP_5 LIKE 'Middle%' THEN D.SW0 / DECODE(A.MAT_GRP_1,'HX',1, COMP_CNT) ELSE 0 END " + "\n");
            strSqlString.Append("                                   ELSE D.SW0 / DECODE(A.MAT_GRP_1,'HX',1, COMP_CNT)" + "\n");
            strSqlString.Append("                              END),0)" + "\n");
            strSqlString.Append("                    ),0) AS SW0" + "\n");
            strSqlString.Append("             , ROUND(SUM(NVL((CASE WHEN A.MAT_CMF_11 IN (SELECT DISTINCT(MAT_CMF_11) FROM MWIPMATDEF WHERE FACTORY = '" + GlobalVariable.gsAssyDefaultFactory + "' AND MAT_GRP_5 = '1st' AND MAT_ID LIKE 'SEKS%') THEN DECODE(A.MAT_GRP_5, '2nd', D.SW1 / COMP_CNT, 0)" + "\n");
            strSqlString.Append("                                   WHEN A.MAT_GRP_3 IN ('COB', 'BGN') THEN D.SW1 / NET_DIE" + "\n");
            strSqlString.Append("                                   WHEN MAT_GRP_4 NOT IN ('-','FD','FU') AND MAT_GRP_5 <> '-' THEN CASE WHEN A.MAT_GRP_5 IN ('1st','Merge') OR MAT_GRP_5 LIKE 'Middle%' THEN D.SW1 / DECODE(A.MAT_GRP_1,'HX',1,COMP_CNT) ELSE 0 END " + "\n");
            strSqlString.Append("                                   ELSE D.SW1 / DECODE(A.MAT_GRP_1,'HX',1,COMP_CNT)" + "\n");
            strSqlString.Append("                              END),0)" + "\n");
            strSqlString.Append("                    ),0) AS SW1" + "\n");
            strSqlString.Append("             , ROUND(SUM(NVL((CASE WHEN A.MAT_CMF_11 IN (SELECT DISTINCT(MAT_CMF_11) FROM MWIPMATDEF WHERE FACTORY = '" + GlobalVariable.gsAssyDefaultFactory + "' AND MAT_GRP_5 = '1st' AND MAT_ID LIKE 'SEKS%') THEN DECODE(A.MAT_GRP_5, '2nd', D.SW2 / COMP_CNT, 0)" + "\n");
            strSqlString.Append("                                   WHEN A.MAT_GRP_3 IN ('COB', 'BGN') THEN D.SW2 / NET_DIE" + "\n");
            strSqlString.Append("                                   WHEN MAT_GRP_4 NOT IN ('-','FD','FU') AND MAT_GRP_5 <> '-' THEN CASE WHEN A.MAT_GRP_5 IN ('1st','Merge') OR MAT_GRP_5 LIKE 'Middle%' THEN D.SW2 / DECODE(A.MAT_GRP_1,'HX',1,COMP_CNT) ELSE 0 END " + "\n");
            strSqlString.Append("                                   ELSE D.SW2 / DECODE(A.MAT_GRP_1,'HX',1,COMP_CNT)" + "\n");
            strSqlString.Append("                              END),0)" + "\n");
            strSqlString.Append("                    ),0) AS SW2" + "\n");
            strSqlString.Append("             , SUM(NVL((CASE WHEN A.MAT_GRP_1 = 'SE' AND A.MAT_GRP_9 = 'MEMORY' AND A.MAT_GRP_5 <> '-' THEN (CASE WHEN A.MAT_GRP_5 = 'Merge' THEN NVL(D.DA02,0) + NVL(D.DA03,0) + NVL(D.DA04,0) + NVL(D.DA05,0) + NVL(D.DA06,0) + NVL(D.DA07,0) + NVL(D.DA08,0) + NVL(D.DA09,0) ELSE 0 END)" + "\n");
            strSqlString.Append("                             WHEN MAT_GRP_4 NOT IN ('-','FD','FU') AND MAT_GRP_5 <> '-' THEN DECODE(SUBSTR(MAT_GRP_4,-1), '2', D.DA02, '3', D.DA03, '4', D.DA04, '5', D.DA05, '6', D.DA06, '7', D.DA07, '8', D.DA08, '9', D.DA09, 0)" + "\n");
            strSqlString.Append("                             ELSE D.DA0" + "\n");
            strSqlString.Append("                        END),0)" + "\n");
            strSqlString.Append("                   ) AS DA0" + "\n");            
            strSqlString.Append("             , SUM(NVL((CASE WHEN A.MAT_GRP_1 = 'SE' AND A.MAT_GRP_9 = 'MEMORY' AND A.MAT_GRP_5 <> '-' THEN (CASE WHEN A.MAT_GRP_5 = 'Merge' THEN NVL(D.DA12,0) + NVL(D.DA13,0) + NVL(D.DA14,0) + NVL(D.DA15,0) + NVL(D.DA16,0) + NVL(D.DA17,0) + NVL(D.DA18,0) + NVL(D.DA19,0) ELSE 0 END)" + "\n");
            strSqlString.Append("                             WHEN MAT_GRP_4 NOT IN ('-','FD','FU') AND MAT_GRP_5 <> '-' THEN DECODE(SUBSTR(MAT_GRP_4,-1), '2', D.DA12, '3', D.DA13, '4', D.DA14, '5', D.DA15, '6', D.DA16, '7', D.DA17, '8', D.DA18, '9', D.DA19, 0)" + "\n");
            strSqlString.Append("                             ELSE D.DA1" + "\n");
            strSqlString.Append("                        END),0)" + "\n");
            strSqlString.Append("                   ) AS DA1" + "\n");            
            strSqlString.Append("             , SUM(NVL((CASE WHEN A.MAT_GRP_1 = 'SE' AND A.MAT_GRP_9 = 'MEMORY' AND A.MAT_GRP_5 <> '-' THEN (CASE WHEN A.MAT_GRP_5 = 'Merge' THEN NVL(D.DA22,0) + NVL(D.DA23,0) + NVL(D.DA24,0) + NVL(D.DA25,0) + NVL(D.DA26,0) + NVL(D.DA27,0) + NVL(D.DA28,0) + NVL(D.DA29,0) ELSE 0 END)" + "\n");
            strSqlString.Append("                             WHEN MAT_GRP_4 NOT IN ('-','FD','FU') AND MAT_GRP_5 <> '-' THEN DECODE(SUBSTR(MAT_GRP_4,-1), '2', D.DA22, '3', D.DA23, '4', D.DA24, '5', D.DA25, '6', D.DA26, '7', D.DA27, '8', D.DA28, '9', D.DA29, 0)" + "\n");
            strSqlString.Append("                             ELSE D.DA2" + "\n");
            strSqlString.Append("                        END),0)" + "\n");
            strSqlString.Append("                   ) AS DA2" + "\n");            
            strSqlString.Append("             , SUM(NVL((CASE WHEN A.MAT_GRP_1 = 'SE' AND A.MAT_GRP_9 = 'MEMORY' AND A.MAT_GRP_5 <> '-' THEN (CASE WHEN A.MAT_GRP_5 = 'Merge' THEN NVL(D.WB02,0) + NVL(D.WB03,0) + NVL(D.WB04,0) + NVL(D.WB05,0) + NVL(D.WB06,0) + NVL(D.WB07,0) + NVL(D.WB08,0) + NVL(D.WB09,0) ELSE 0 END)" + "\n");
            strSqlString.Append("                             WHEN MAT_GRP_4 NOT IN ('-','FD','FU') AND MAT_GRP_5 <> '-' THEN DECODE(SUBSTR(MAT_GRP_4,-1), '2', D.WB02, '3', D.WB03, '4', D.WB04, '5', D.WB05, '6', D.WB06, '7', D.WB07, '8', D.WB08, '9', D.WB09, 0)" + "\n");
            strSqlString.Append("                             ELSE D.WB0" + "\n");
            strSqlString.Append("                        END),0)" + "\n");
            strSqlString.Append("                   ) AS WB0" + "\n");            
            strSqlString.Append("             , SUM(NVL((CASE WHEN A.MAT_GRP_1 = 'SE' AND A.MAT_GRP_9 = 'MEMORY' AND A.MAT_GRP_5 <> '-' THEN (CASE WHEN A.MAT_GRP_5 = 'Merge' THEN NVL(D.WB12,0) + NVL(D.WB13,0) + NVL(D.WB14,0) + NVL(D.WB15,0) + NVL(D.WB16,0) + NVL(D.WB17,0) + NVL(D.WB18,0) + NVL(D.WB19,0) ELSE 0 END)" + "\n");
            strSqlString.Append("                             WHEN MAT_GRP_4 NOT IN ('-','FD','FU') AND MAT_GRP_5 <> '-' THEN DECODE(SUBSTR(MAT_GRP_4,-1), '2', D.WB12, '3', D.WB13, '4', D.WB14, '5', D.WB15, '6', D.WB16, '7', D.WB17, '8', D.WB18, '9', D.WB19, 0)" + "\n");
            strSqlString.Append("                             ELSE D.WB1" + "\n");
            strSqlString.Append("                        END),0)" + "\n");
            strSqlString.Append("                   ) AS WB1" + "\n");            
            strSqlString.Append("             , SUM(NVL((CASE WHEN A.MAT_GRP_1 = 'SE' AND A.MAT_GRP_9 = 'MEMORY' AND A.MAT_GRP_5 <> '-' THEN (CASE WHEN A.MAT_GRP_5 = 'Merge' THEN NVL(D.WB22,0) + NVL(D.WB23,0) + NVL(D.WB24,0) + NVL(D.WB25,0) + NVL(D.WB26,0) + NVL(D.WB27,0) + NVL(D.WB28,0) + NVL(D.WB29,0) ELSE 0 END)" + "\n");
            strSqlString.Append("                             WHEN MAT_GRP_4 NOT IN ('-','FD','FU') AND MAT_GRP_5 <> '-' THEN DECODE(SUBSTR(MAT_GRP_4,-1), '2', D.WB22, '3', D.WB23, '4', D.WB24, '5', D.WB25, '6', D.WB26, '7', D.WB27, '8', D.WB28, '9', D.WB29, 0)" + "\n");
            strSqlString.Append("                             ELSE D.WB2" + "\n");
            strSqlString.Append("                        END),0)" + "\n");
            strSqlString.Append("                   ) AS WB2" + "\n");
            strSqlString.Append("             , SUM(NVL(D.MD0,0)) AS MD0, SUM(NVL(D.MD1,0)) AS MD1, SUM(NVL(D.MD2,0)) AS MD2 " + "\n");
            strSqlString.Append("             , SUM(NVL(D.TF0,0)) AS TF0, SUM(NVL(D.TF1,0)) AS TF1, SUM(NVL(D.TF2,0)) AS TF2 " + "\n");
            strSqlString.Append("             , SUM(NVL(F.V0,0)) AS STOCK" + "\n");
            strSqlString.Append("             , SUM(NVL(F.V1,0)+NVL(F.V2,0)) AS SAW" + "\n");
            strSqlString.Append("             , SUM(NVL(F.V3,0)+NVL(F.V4,0)+NVL(F.V17,0)) AS DA" + "\n");            

            // 20130-08-30-������ : DA/WB FINAL ��� ��ȸ �ǵ��� �߰� -> MCP ���� 1st, Middle % ���� (WB_BEFORE) & MCP ���� Merge ����, �̱�Ĩ ���� (WB_AFTER)
            if (ckbFinal.Checked == true)
            {
                strSqlString.Append("             , SUM(NVL((CASE WHEN MAT_GRP_4 NOT IN ('-','FD','FU') AND MAT_GRP_5 <> '-' THEN CASE WHEN A.MAT_GRP_5 = '1st' OR MAT_GRP_5 LIKE 'Middle%' THEN NVL(F.V5+F.V16,0) ELSE 0 END" + "\n");
                strSqlString.Append("                             ELSE 0" + "\n");
                strSqlString.Append("                        END),0)" + "\n");
                strSqlString.Append("                    ) AS WB_BEFORE" + "\n");
                strSqlString.Append("             , SUM(NVL((CASE WHEN MAT_GRP_4 NOT IN ('-','FD','FU') AND MAT_GRP_5 <> '-' THEN CASE WHEN A.MAT_GRP_5 = 'Merge' THEN NVL(F.V5+F.V16,0) ELSE 0 END" + "\n");
                strSqlString.Append("                             ELSE NVL(F.V5+F.V16,0)" + "\n");
                strSqlString.Append("                        END),0)" + "\n");
                strSqlString.Append("                    ) AS WB_AFTER" + "\n");
            }
            else
            {
                strSqlString.Append("             , SUM(NVL(F.V5+F.V16,0)) AS WB" + "\n");
            }

            strSqlString.Append("             , SUM(NVL(F.V6,0)+NVL(F.V7,0)) AS MOLD" + "\n");
            strSqlString.Append("             , SUM(NVL(F.V8+F.V9+F.V10+F.V11+F.V12+F.V13+F.V14,0)) AS FINISH" + "\n");
            strSqlString.Append("             , SUM(NVL(F.V15,0)) AS HMK3A" + "\n");
            strSqlString.Append("             , SUM(NVL(F.V0+F.V1+F.V2+F.V3+F.V4+F.V5+F.V6+F.V7+F.V8+F.V9+F.V10+F.V11+F.V12+F.V13+F.V14+F.V15+F.V16+F.V17,0)) AS TTL" + "\n");          
            strSqlString.Append("             , SUM(HIST_AO) AS HIST_AO " + "\n");
            strSqlString.Append("          FROM ( " + "\n");
            strSqlString.Append("                SELECT MAT.MAT_GRP_1, MAT.MAT_GRP_2, MAT.MAT_GRP_3, MAT.MAT_GRP_4, MAT.MAT_GRP_5, MAT.MAT_GRP_6, MAT.MAT_GRP_7, MAT.MAT_GRP_8 " + "\n");
            strSqlString.Append("                     , DECODE(MAT.MAT_GRP_1,'SE',MAT.MAT_GRP_9,' ') AS MAT_GRP_9, MAT.MAT_GRP_10, MAT.MAT_CMF_10, MAT.MAT_ID, MAT.MAT_CMF_7, MAT.NET_DIE, MAT.MAT_CMF_11, MAT.COMP_CNT  " + "\n");

            // 2011-08-03-������ : BGN�� �� ��ȹ�� WF ����� �Է� �ϱ⿡ NET DIE �� ���� �ʿ䰡 ����.(���¼� ��û)
            if (ckbRev.Checked == false)
            {
                strSqlString.Append("                     , CASE WHEN MAT.MAT_GRP_3 IN ('COB') THEN ROUND(SUM(PLAN.PLAN_QTY_ASSY / MAT.NET_DIE),0)" + "\n");
                strSqlString.Append("                            ELSE SUM(PLAN.PLAN_QTY_ASSY)" + "\n");
                strSqlString.Append("                       END MON_PLAN" + "\n");
                strSqlString.Append("                     , 0 AS ORI_PLAN" + "\n");
            }
            else
            {
                strSqlString.Append("                     , CASE WHEN MAT.MAT_GRP_3 IN ('COB') THEN ROUND(SUM(PLAN.RESV_FIELD1 / MAT.NET_DIE),0)" + "\n");
                strSqlString.Append("                            ELSE SUM(PLAN.RESV_FIELD1)" + "\n");
                strSqlString.Append("                       END MON_PLAN" + "\n");
                strSqlString.Append("                     , CASE WHEN MAT.MAT_GRP_3 IN ('COB') THEN ROUND(SUM(PLAN.PLAN_QTY_ASSY / MAT.NET_DIE),0)" + "\n");
                strSqlString.Append("                            ELSE SUM(PLAN.PLAN_QTY_ASSY)" + "\n");
                strSqlString.Append("                       END ORI_PLAN" + "\n");
            }

            strSqlString.Append("                     , CASE WHEN MAT.MAT_GRP_3 IN ('COB') THEN ROUND(SUM(WEEK_PLAN.WEEK_PLAN / MAT.NET_DIE),0)" + "\n");
            strSqlString.Append("                            ELSE SUM(WEEK_PLAN.WEEK_PLAN)" + "\n");
            strSqlString.Append("                            END WEEK_PLAN" + "\n");

            strSqlString.Append("                     , CASE WHEN MAT.MAT_GRP_3 IN ('COB', 'BGN') THEN ROUND(SUM(WEEK_AO.WEEK_AO / MAT.NET_DIE),0)" + "\n");
            strSqlString.Append("                            ELSE SUM(WEEK_AO.WEEK_AO)" + "\n");
            strSqlString.Append("                            END ASSY_WEEK" + "\n");
            strSqlString.Append("                     , CASE WHEN MAT.MAT_GRP_3 IN ('COB', 'BGN') THEN ROUND(SUM(MON_AO.ASSY_MON / MAT.NET_DIE),0)" + "\n");
            strSqlString.Append("                            ELSE SUM(MON_AO.ASSY_MON)" + "\n");
            strSqlString.Append("                       END ASSY_MON" + "\n");
            strSqlString.Append("                     , CASE WHEN MAT.MAT_GRP_3 IN ('COB', 'BGN') THEN ROUND(SUM(MON_AO.AO0 / MAT.NET_DIE),0)" + "\n");
            strSqlString.Append("                            ELSE SUM(MON_AO.AO0)" + "\n");
            strSqlString.Append("                       END AO0" + "\n");
            strSqlString.Append("                     , CASE WHEN MAT.MAT_GRP_3 IN ('COB', 'BGN') THEN ROUND(SUM(MON_AO.AO1 / MAT.NET_DIE),0)" + "\n");
            strSqlString.Append("                            ELSE SUM(MON_AO.AO1)" + "\n");
            strSqlString.Append("                       END AO1" + "\n");
            strSqlString.Append("                     , CASE WHEN MAT.MAT_GRP_3 IN ('COB', 'BGN') THEN ROUND(SUM(MON_AO.AO2 / MAT.NET_DIE),0)" + "\n");
            strSqlString.Append("                            ELSE SUM(MON_AO.AO2)" + "\n");
            strSqlString.Append("                       END AO2" + "\n");

            strSqlString.Append("                     , ROUND(CASE WHEN MAT.MAT_GRP_3 IN ('COB', 'BGN') THEN ROUND(SUM(DAY_RCV.RCV0 / MAT.NET_DIE),0)" + "\n");
            strSqlString.Append("                                  WHEN MAT.MAT_CMF_11 IN (SELECT DISTINCT(MAT_CMF_11) FROM MWIPMATDEF WHERE FACTORY = '" + GlobalVariable.gsAssyDefaultFactory + "' AND MAT_GRP_5 = '1st' AND MAT_ID LIKE 'SEKS%') THEN DECODE(MAT.MAT_GRP_5, '2nd', SUM(DAY_RCV.RCV0 / MAT.COMP_CNT), 0)" + "\n");
            strSqlString.Append("                                  WHEN MAT.MAT_GRP_4 NOT IN ('-','FD','FU') AND MAT.MAT_GRP_5 <> '-' THEN CASE WHEN MAT.MAT_GRP_5 IN ('1st','Merge') OR MAT.MAT_GRP_5 LIKE 'Middle%' THEN SUM(DAY_RCV.RCV0 / MAT.COMP_CNT) ELSE 0 END " + "\n");
            strSqlString.Append("                                  ELSE SUM(DAY_RCV.RCV0 / MAT.COMP_CNT)" + "\n");
            strSqlString.Append("                             END, 0) AS RCV0" + "\n");
            strSqlString.Append("                     , ROUND(CASE WHEN MAT.MAT_GRP_3 IN ('COB', 'BGN') THEN ROUND(SUM(DAY_RCV.RCV1 / MAT.NET_DIE),0)" + "\n");
            strSqlString.Append("                                  WHEN MAT.MAT_CMF_11 IN (SELECT DISTINCT(MAT_CMF_11) FROM MWIPMATDEF WHERE FACTORY = '" + GlobalVariable.gsAssyDefaultFactory + "' AND MAT_GRP_5 = '1st' AND MAT_ID LIKE 'SEKS%') THEN DECODE(MAT.MAT_GRP_5, '2nd', SUM(DAY_RCV.RCV1 / MAT.COMP_CNT), 0)" + "\n");
            strSqlString.Append("                                  WHEN MAT.MAT_GRP_4 NOT IN ('-','FD','FU') AND MAT.MAT_GRP_5 <> '-' THEN CASE WHEN MAT.MAT_GRP_5 IN ('1st','Merge') OR MAT.MAT_GRP_5 LIKE 'Middle%' THEN SUM(DAY_RCV.RCV1 / MAT.COMP_CNT) ELSE 0 END " + "\n");
            strSqlString.Append("                                  ELSE SUM(DAY_RCV.RCV1 / MAT.COMP_CNT)" + "\n");
            strSqlString.Append("                             END, 0) AS RCV1" + "\n");
            strSqlString.Append("                     , ROUND(CASE WHEN MAT.MAT_GRP_3 IN ('COB', 'BGN') THEN ROUND(SUM(DAY_RCV.RCV2 / MAT.NET_DIE),0)" + "\n");
            strSqlString.Append("                                  WHEN MAT.MAT_CMF_11 IN (SELECT DISTINCT(MAT_CMF_11) FROM MWIPMATDEF WHERE FACTORY = '" + GlobalVariable.gsAssyDefaultFactory + "' AND MAT_GRP_5 = '1st' AND MAT_ID LIKE 'SEKS%') THEN DECODE(MAT.MAT_GRP_5, '2nd', SUM(DAY_RCV.RCV2 / MAT.COMP_CNT), 0)" + "\n");
            strSqlString.Append("                                  WHEN MAT.MAT_GRP_4 NOT IN ('-','FD','FU') AND MAT.MAT_GRP_5 <> '-' THEN CASE WHEN MAT.MAT_GRP_5 IN ('1st','Merge') OR MAT.MAT_GRP_5 LIKE 'Middle%' THEN SUM(DAY_RCV.RCV2 / MAT.COMP_CNT) ELSE 0 END " + "\n");
            strSqlString.Append("                                  ELSE SUM(DAY_RCV.RCV2 / MAT.COMP_CNT)" + "\n");
            strSqlString.Append("                             END, 0) AS RCV2" + "\n");
            strSqlString.Append("                     , SUM(HIST_AO.HIST_AO) AS HIST_AO " + "\n");
            strSqlString.Append("                  FROM VWIPMATDEF MAT " + "\n");

            //�� ��ȹ SLIS ��ǰ�� MP��ȹ�� �����ϰ�            
            strSqlString.Append("                     , ( " + "\n");
            strSqlString.Append("                        SELECT FACTORY,MAT_ID,PLAN_QTY_ASSY,PLAN_MONTH, RESV_FIELD1 " + "\n");
            strSqlString.Append("                          FROM ( " + "\n");
            strSqlString.Append("                                SELECT FACTORY, MAT_ID, SUM(PLAN_QTY_ASSY) AS PLAN_QTY_ASSY, PLAN_MONTH, SUM(RESV_FIELD1) AS RESV_FIELD1  " + "\n");
            strSqlString.Append("                                  FROM ( " + "\n");
            strSqlString.Append("                                        SELECT FACTORY, MAT_ID, SUM(PLAN_QTY_ASSY) AS PLAN_QTY_ASSY, PLAN_MONTH, SUM(TO_NUMBER(DECODE(RESV_FIELD1,' ',0,RESV_FIELD1))) AS RESV_FIELD1 " + "\n"); 
            strSqlString.Append("                                          FROM CWIPPLNMON " + "\n");
            strSqlString.Append("                                         WHERE 1=1 " + "\n");
            strSqlString.Append("                                           AND FACTORY = '" + GlobalVariable.gsAssyDefaultFactory + "' " + "\n");
            strSqlString.Append("                                         GROUP BY FACTORY, MAT_ID, PLAN_MONTH " + "\n");

            // 2013-06-07-������ : SE ��ǰ �� ��ȹ REV ���� ���� OMS �����Ϳ��� �� ��ȹ UPLOAD �����ͷ� ���� �� (������ ��û)
            //strSqlString.Append("                                         UNION ALL " + "\n");
            //strSqlString.Append("                                        SELECT A.FACTORY, A.MAT_ID, 0 AS PLAN_QTY_ASSY , '" + month + "' AS PLAN_MONTH, SUM(A.PLAN_QTY) AS RESV_FIELD1 " + "\n");
            //strSqlString.Append("                                          FROM ( " + "\n");

            //// ����ȹ �����̸� �������
            //if (DateTime.Now.ToString("yyyyMMdd") == cdvDate.SelectedValue())
            //{                
            //    strSqlString.Append("                                                SELECT FACTORY, MAT_ID, SUM(NVL(PLAN_QTY, 0)) AS PLAN_QTY " + "\n");
            //    strSqlString.Append("                                                  FROM CWIPPLNDAY " + "\n");
            //    strSqlString.Append("                                                 WHERE 1=1 " + "\n");
            //    strSqlString.Append("                                                   AND FACTORY = '" + GlobalVariable.gsAssyDefaultFactory + "' " + "\n");
            //    strSqlString.Append("                                                   AND PLAN_DAY BETWEEN '" + start_date + "' AND '" + last_day + "'\n");
            //    strSqlString.Append("                                                   AND IN_OUT_FLAG = 'OUT' " + "\n");
            //    strSqlString.Append("                                                   AND CLASS = 'ASSY' " + "\n");
            //    strSqlString.Append("                                                 GROUP BY FACTORY, MAT_ID " + "\n");
            //}
            //else// ������ �ƴϸ� ������ ������ ���̺��� ������.
            //{
            //    strSqlString.Append("                                                SELECT FACTORY, MAT_ID, SUM(NVL(PLAN_QTY, 0)) AS PLAN_QTY " + "\n");
            //    strSqlString.Append("                                                  FROM CWIPPLNSNP@RPTTOMES " + "\n");
            //    strSqlString.Append("                                                 WHERE 1=1 " + "\n");
            //    strSqlString.Append("                                                   AND FACTORY = '" + GlobalVariable.gsAssyDefaultFactory + "' " + "\n");
            //    strSqlString.Append("                                                   AND SNAPSHOT_DAY = '" + cdvDate.SelectedValue() + "'" + "\n");
            //    strSqlString.Append("                                                   AND PLAN_DAY BETWEEN '" + start_date + "' AND '" + last_day + "'\n");
            //    strSqlString.Append("                                                   AND IN_OUT_FLAG = 'OUT' " + "\n");
            //    strSqlString.Append("                                                   AND CLASS = 'ASSY' " + "\n");
            //    strSqlString.Append("                                                 GROUP BY FACTORY, MAT_ID " + "\n");
            //}

            //strSqlString.Append("                                                 UNION ALL " + "\n");
            //strSqlString.Append("                                                SELECT CM_KEY_1 AS FACTORY, MAT_ID " + "\n");
            //strSqlString.Append("                                                     , SUM(S1_FAC_OUT_QTY_1 + S2_FAC_OUT_QTY_1 + S3_FAC_OUT_QTY_1 + S4_FAC_OUT_QTY_1) AS PLAN_QTY " + "\n");
            //strSqlString.Append("                                                  FROM RSUMFACMOV " + "\n");
            //strSqlString.Append("                                                 WHERE 1=1 " + "\n");
            //strSqlString.Append("                                                   AND CM_KEY_1 = '" + GlobalVariable.gsAssyDefaultFactory + "' " + "\n");
            //strSqlString.Append("                                                   AND CM_KEY_2 = 'PROD' " + "\n");
            //strSqlString.Append("                                                   AND CM_KEY_3 LIKE 'P%' " + "\n");
            //strSqlString.Append("                                                   AND WORK_DATE BETWEEN '" + start_date + "' AND '" + Lastweek_lastday + "'\n");
            //strSqlString.Append("                                                 GROUP BY CM_KEY_1, MAT_ID " + "\n");            
            //strSqlString.Append("                                               ) A" + "\n");
            //strSqlString.Append("                                             , MWIPMATDEF B " + "\n");
            //strSqlString.Append("                                         WHERE 1=1  " + "\n");
            //strSqlString.Append("                                           AND A.FACTORY = B.FACTORY " + "\n");
            //strSqlString.Append("                                           AND A.MAT_ID = B.MAT_ID " + "\n");
            //strSqlString.Append("                                           AND B.MAT_GRP_1 = 'SE' " + "\n");
            //strSqlString.Append("                                           AND B.MAT_GRP_9 = 'S-LSI' " + "\n");
            //strSqlString.Append("                                         GROUP BY A.FACTORY, A.MAT_ID " + "\n");            
            strSqlString.Append("                                       ) " + "\n");
            strSqlString.Append("                                 GROUP BY FACTORY, MAT_ID,PLAN_MONTH " + "\n");            
            strSqlString.Append("                               ) " + "\n");
            strSqlString.Append("                       ) PLAN " + "\n");
            strSqlString.Append("                     , ( " + "\n");
            strSqlString.Append("                        SELECT FACTORY,MAT_ID, SUM(WW_QTY) AS WEEK_PLAN " + "\n");
            strSqlString.Append("                          FROM RWIPPLNWEK " + "\n");
            strSqlString.Append("                         WHERE FACTORY = '" + GlobalVariable.gsAssyDefaultFactory + "' " + "\n");
            strSqlString.Append("                           AND PLAN_WEEK = '" + FindWeek.ThisWeek + "'\n");
            strSqlString.Append("                           AND GUBUN = '3' " + "\n");
            strSqlString.Append("                        GROUP BY FACTORY,MAT_ID " + "\n");
            strSqlString.Append("                       ) WEEK_PLAN " + "\n");
            strSqlString.Append("                     , ( " + "\n");
            strSqlString.Append("                        SELECT MAT_ID " + "\n");
            strSqlString.Append("                             , SUM(AO0) AS AO0 " + "\n");
            strSqlString.Append("                             , SUM(AO1) AS AO1 " + "\n");
            strSqlString.Append("                             , SUM(AO2) AS AO2 " + "\n");
            strSqlString.Append("                             , SUM(ASSY_MON) AS ASSY_MON " + "\n");
            strSqlString.Append("                          FROM (" + "\n");
            // 2010-02-03-������ ��A/O ��, Return �����ϰ� ���� A/O ǥ�÷� ���Ͽ� �ش� ���� ���� ��. RSUMFACMOV (���¼� �븮 ��û)
            strSqlString.Append("                                SELECT MAT_ID " + "\n");            
            strSqlString.Append("                                     , SUM(DECODE(WORK_DATE,'" + dayArry2[0] + "', NVL(S1_FAC_OUT_QTY_1 + S2_FAC_OUT_QTY_1 + S3_FAC_OUT_QTY_1 + S4_FAC_OUT_QTY_1, 0), 0)) AS AO0 " + "\n");
            strSqlString.Append("                                     , SUM(DECODE(WORK_DATE,'" + dayArry2[1] + "', NVL(S1_FAC_OUT_QTY_1 + S2_FAC_OUT_QTY_1 + S3_FAC_OUT_QTY_1 + S4_FAC_OUT_QTY_1, 0), 0)) AS AO1" + "\n");
            strSqlString.Append("                                     , SUM(DECODE(WORK_DATE,'" + dayArry2[2] + "', NVL(S1_FAC_OUT_QTY_1 + S2_FAC_OUT_QTY_1 + S3_FAC_OUT_QTY_1 + S4_FAC_OUT_QTY_1, 0), 0)) AS AO2" + "\n");
            strSqlString.Append("                                     , 0 AS ASSY_MON  " + "\n");
            strSqlString.Append("                                  FROM RSUMFACMOV " + "\n");
            strSqlString.Append("                                 WHERE 1=1 " + "\n");
            strSqlString.Append("                                   AND WORK_DATE BETWEEN '" + dayArry2[0] + "' AND '" + dayArry2[2] + "'" + "\n");                
            strSqlString.Append("                                   AND LOT_TYPE = 'W' " + "\n");
            strSqlString.Append("                                   AND CM_KEY_1 = '" + GlobalVariable.gsAssyDefaultFactory + "' " + "\n");
            strSqlString.Append("                                   AND CM_KEY_2 = 'PROD' " + "\n");

            //2011-03-15-�����: Lot Type���� P% or E% �� �˻��� �� �ֵ��� (�蹮�� ����� ��û ��)
            if (txtSearchProduct.Text.Trim() != "%" && txtSearchProduct.Text.Trim() != "")
            {
                strSqlString.AppendFormat("                                   AND CM_KEY_3 LIKE '{0}'" + "\n", txtSearchProduct.Text);
            }

            strSqlString.Append("                                   AND FACTORY NOT IN ('RETURN') " + "\n");
            strSqlString.Append("                                 GROUP BY MAT_ID " + "\n");
            strSqlString.Append("                                 UNION ALL " + "\n");
            strSqlString.Append("                                SELECT MAT_ID " + "\n");
            strSqlString.Append("                                     , 0, 0, 0 " + "\n");
            strSqlString.Append("                                     , SUM(SHP_QTY_1) AS ASSY_MON " + "\n");
            strSqlString.Append("                                  FROM VSUMWIPOUT " + "\n");
            strSqlString.Append("                                 WHERE 1=1 " + "\n");
            strSqlString.Append("                                   AND WORK_DATE BETWEEN '" + start_date + "' AND '" + date + "'" + "\n");
            strSqlString.Append("                                   AND FACTORY = '" + GlobalVariable.gsAssyDefaultFactory + "' " + "\n");            
            strSqlString.Append("                                   AND LOT_TYPE = 'W' " + "\n");
            strSqlString.Append("                                   AND CM_KEY_2 = 'PROD' " + "\n");
            

            if (txtSearchProduct.Text.Trim() != "%" && txtSearchProduct.Text.Trim() != "")
            {
                strSqlString.AppendFormat("                                   AND CM_KEY_3 LIKE '{0}'" + "\n", txtSearchProduct.Text);
            }

            
            
            strSqlString.Append("                                 GROUP BY MAT_ID " + "\n");
            strSqlString.Append("                               ) " + "\n");
            strSqlString.Append("                         GROUP BY MAT_ID " + "\n");
            strSqlString.Append("                       ) MON_AO " + "\n");            
            strSqlString.Append("                     , ( " + "\n");
            strSqlString.Append("                        SELECT MAT_ID " + "\n");
            strSqlString.Append("                             , SUM(NVL(S1_FAC_OUT_QTY_1 + S2_FAC_OUT_QTY_1 + S3_FAC_OUT_QTY_1 + S4_FAC_OUT_QTY_1, 0)) AS WEEK_AO  " + "\n");
            strSqlString.Append("                          FROM RSUMFACMOV " + "\n");
            strSqlString.Append("                         WHERE 1=1 " + "\n");
            strSqlString.Append("                           AND WORK_DATE BETWEEN '" + FindWeek.StartDay_ThisWeek + "' AND '" + date + "'" + "\n");
            strSqlString.Append("                           AND LOT_TYPE = 'W' " + "\n");
            strSqlString.Append("                           AND CM_KEY_1 = '" + GlobalVariable.gsAssyDefaultFactory + "' " + "\n");
            strSqlString.Append("                           AND CM_KEY_2 = 'PROD' " + "\n");

            if (txtSearchProduct.Text.Trim() != "%" && txtSearchProduct.Text.Trim() != "")
            {
                strSqlString.AppendFormat("                           AND CM_KEY_3 LIKE '{0}'" + "\n", txtSearchProduct.Text);
            }

            strSqlString.Append("                           AND FACTORY NOT IN ('RETURN') " + "\n");
            strSqlString.Append("                         GROUP BY MAT_ID " + "\n");
            strSqlString.Append("                       ) WEEK_AO " + "\n");
            strSqlString.Append("                     , ( " + "\n");
            strSqlString.Append("                        SELECT MAT_ID " + "\n");
            strSqlString.Append("                             , SUM(DECODE(WORK_DATE,'" + dayArry2[0] + "', NVL(RCV_QTY_1,0), 0)) AS RCV0" + "\n");
            strSqlString.Append("                             , SUM(DECODE(WORK_DATE,'" + dayArry2[1] + "', NVL(RCV_QTY_1,0), 0)) AS RCV1" + "\n");
            strSqlString.Append("                             , SUM(DECODE(WORK_DATE,'" + dayArry2[2] + "', NVL(RCV_QTY_1,0), 0)) AS RCV2" + "\n");
            strSqlString.Append("                          FROM VSUMWIPRCV " + "\n");
            strSqlString.Append("                         WHERE 1=1 " + "\n");
            strSqlString.Append("                           AND WORK_DATE BETWEEN '" + dayArry2[0] + "' AND '" + dayArry2[2] + "'" + "\n");
            strSqlString.Append("                           AND LOT_TYPE = 'W' " + "\n");
            strSqlString.Append("                           AND FACTORY = '" + GlobalVariable.gsAssyDefaultFactory + "' " + "\n");
            strSqlString.Append("                           AND CM_KEY_2 = 'PROD' " + "\n");

            //2011-03-15-�����: Lot Type���� P% or E% �� �˻��� �� �ֵ��� (�蹮�� ����� ��û ��)
            if (txtSearchProduct.Text.Trim() != "%" && txtSearchProduct.Text.Trim() != "") 
            {
                strSqlString.AppendFormat("                           AND CM_KEY_3 LIKE '{0}'" + "\n", txtSearchProduct.Text); 
            }
                        
            strSqlString.Append("                         GROUP BY MAT_ID " + "\n");            
            strSqlString.Append("                       ) DAY_RCV  " + "\n");
            strSqlString.Append("                     , ( " + "\n");
            strSqlString.Append("                        SELECT MAT_ID " + "\n");
            strSqlString.Append("                             , SUM(NVL(S1_FAC_OUT_QTY_1 + S2_FAC_OUT_QTY_1 + S3_FAC_OUT_QTY_1 + S4_FAC_OUT_QTY_1, 0)) AS HIST_AO  " + "\n");
            strSqlString.Append("                          FROM RSUMFACMOV " + "\n");
            strSqlString.Append("                         WHERE 1=1 " + "\n");
            strSqlString.Append("                           AND WORK_DATE BETWEEN TO_CHAR(ADD_MONTHS('" + date + "', -6), 'YYYYMMDD') AND '" + date + "'" + "\n");
            strSqlString.Append("                           AND LOT_TYPE = 'W' " + "\n");
            strSqlString.Append("                           AND CM_KEY_1 = '" + GlobalVariable.gsAssyDefaultFactory + "' " + "\n");
            strSqlString.Append("                           AND CM_KEY_2 = 'PROD' " + "\n");

            if (txtSearchProduct.Text.Trim() != "%" && txtSearchProduct.Text.Trim() != "")
            {
                strSqlString.AppendFormat("                           AND CM_KEY_3 LIKE '{0}'" + "\n", txtSearchProduct.Text);
            }

            strSqlString.Append("                           AND FACTORY NOT IN ('RETURN') " + "\n");
            strSqlString.Append("                         GROUP BY MAT_ID " + "\n");
            strSqlString.Append("                       ) HIST_AO " + "\n");
            strSqlString.Append("                 WHERE 1 = 1 " + "\n");
            strSqlString.Append("                   AND MAT.FACTORY =PLAN.FACTORY(+) " + "\n");
            strSqlString.Append("                   AND MAT.FACTORY =WEEK_PLAN.FACTORY(+) " + "\n");
            strSqlString.Append("                   AND MAT.MAT_ID = PLAN.MAT_ID(+) " + "\n");            
            strSqlString.Append("                   AND MAT.MAT_ID = WEEK_PLAN.MAT_ID(+) " + "\n");
            strSqlString.Append("                   AND MAT.MAT_ID = MON_AO.MAT_ID(+) " + "\n");
            strSqlString.Append("                   AND MAT.MAT_ID = WEEK_AO.MAT_ID(+) " + "\n");
            strSqlString.Append("                   AND MAT.MAT_ID = DAY_RCV.MAT_ID(+) " + "\n");
            strSqlString.Append("                   AND MAT.MAT_ID = HIST_AO.MAT_ID(+) " + "\n");
            strSqlString.Append("                   AND MAT.FACTORY = '" + GlobalVariable.gsAssyDefaultFactory + "' " + "\n");
            strSqlString.Append("                   AND PLAN.PLAN_MONTH(+) = '" + month + "' " + "\n");
            strSqlString.Append("                   AND MAT.MAT_TYPE= 'FG' " + "\n");
            strSqlString.Append("                   AND MAT.DELETE_FLAG <> 'Y' " + "\n");
            strSqlString.Append("                 GROUP BY MAT.MAT_GRP_1, MAT.MAT_GRP_2, MAT.MAT_GRP_3, MAT.MAT_GRP_4, MAT.MAT_GRP_5, MAT.MAT_GRP_6, MAT.MAT_GRP_7, MAT.MAT_GRP_8, MAT.MAT_GRP_9, MAT.MAT_GRP_10, MAT.MAT_CMF_10, MAT.MAT_ID, MAT.MAT_CMF_7, MAT.NET_DIE, MAT.MAT_CMF_11, MAT.COMP_CNT" + "\n");            
            strSqlString.Append("               ) A  " + "\n");
            strSqlString.Append("             , ( " + "\n");
            strSqlString.Append("                SELECT MAT_ID " + "\n");
            strSqlString.Append("                     , SUM(DECODE(OPER, 'A0200', DECODE(WORK_DATE,'" + dayArry2[0] + "', QTY,0))) AS SW0" + "\n");
            strSqlString.Append("                     , SUM(DECODE(OPER, 'A0200', DECODE(WORK_DATE,'" + dayArry2[1] + "', QTY,0))) AS SW1" + "\n");
            strSqlString.Append("                     , SUM(DECODE(OPER, 'A0200', DECODE(WORK_DATE,'" + dayArry2[2] + "', QTY,0))) AS SW2" + "\n");
            strSqlString.Append("                     , SUM(DECODE(OPER_GRP_7, 'D/A', DECODE(WORK_DATE,'" + dayArry2[0] + "', QTY,0))) AS DA0" + "\n");
            strSqlString.Append("                     , SUM(DECODE(OPER_GRP_7, 'D/A', DECODE(WORK_DATE,'" + dayArry2[1] + "', QTY,0))) AS DA1" + "\n");
            strSqlString.Append("                     , SUM(DECODE(OPER_GRP_7, 'D/A', DECODE(WORK_DATE,'" + dayArry2[2] + "', QTY,0))) AS DA2" + "\n");
            strSqlString.Append("                     , SUM(DECODE(OPER_GRP_7, 'W/B', DECODE(WORK_DATE,'" + dayArry2[0] + "', QTY,0))) AS WB0" + "\n");
            strSqlString.Append("                     , SUM(DECODE(OPER_GRP_7, 'W/B', DECODE(WORK_DATE,'" + dayArry2[1] + "', QTY,0))) AS WB1" + "\n");
            strSqlString.Append("                     , SUM(DECODE(OPER_GRP_7, 'W/B', DECODE(WORK_DATE,'" + dayArry2[2] + "', QTY,0))) AS WB2" + "\n");
            strSqlString.Append("                     , SUM(DECODE(OPER_GRP_7, 'MOLD', DECODE(WORK_DATE,'" + dayArry2[0] + "', QTY,0))) AS MD0" + "\n");
            strSqlString.Append("                     , SUM(DECODE(OPER_GRP_7, 'MOLD', DECODE(WORK_DATE,'" + dayArry2[1] + "', QTY,0))) AS MD1" + "\n");
            strSqlString.Append("                     , SUM(DECODE(OPER_GRP_7, 'MOLD', DECODE(WORK_DATE,'" + dayArry2[2] + "', QTY,0))) AS MD2" + "\n");
            strSqlString.Append("                     , SUM(DECODE(OPER_GRP_7, 'SIG', DECODE(WORK_DATE,'" + dayArry2[0] + "', QTY,0))) AS TF0" + "\n");
            strSqlString.Append("                     , SUM(DECODE(OPER_GRP_7, 'SIG', DECODE(WORK_DATE,'" + dayArry2[1] + "', QTY,0))) AS TF1" + "\n");
            strSqlString.Append("                     , SUM(DECODE(OPER_GRP_7, 'SIG', DECODE(WORK_DATE,'" + dayArry2[2] + "', QTY,0))) AS TF2" + "\n");
            strSqlString.Append("                     , SUM(DECODE(OPER, 'A0402', DECODE(WORK_DATE,'" + dayArry2[0] + "', QTY,0))) AS DA02" + "\n");
            strSqlString.Append("                     , SUM(DECODE(OPER, 'A0402', DECODE(WORK_DATE,'" + dayArry2[1] + "', QTY,0))) AS DA12" + "\n");
            strSqlString.Append("                     , SUM(DECODE(OPER, 'A0402', DECODE(WORK_DATE,'" + dayArry2[2] + "', QTY,0))) AS DA22" + "\n");
            strSqlString.Append("                     , SUM(DECODE(OPER, 'A0403', DECODE(WORK_DATE,'" + dayArry2[0] + "', QTY,0))) AS DA03" + "\n");
            strSqlString.Append("                     , SUM(DECODE(OPER, 'A0403', DECODE(WORK_DATE,'" + dayArry2[1] + "', QTY,0))) AS DA13" + "\n");
            strSqlString.Append("                     , SUM(DECODE(OPER, 'A0403', DECODE(WORK_DATE,'" + dayArry2[2] + "', QTY,0))) AS DA23" + "\n");
            strSqlString.Append("                     , SUM(DECODE(OPER, 'A0404', DECODE(WORK_DATE,'" + dayArry2[0] + "', QTY,0))) AS DA04" + "\n");
            strSqlString.Append("                     , SUM(DECODE(OPER, 'A0404', DECODE(WORK_DATE,'" + dayArry2[1] + "', QTY,0))) AS DA14" + "\n");
            strSqlString.Append("                     , SUM(DECODE(OPER, 'A0404', DECODE(WORK_DATE,'" + dayArry2[2] + "', QTY,0))) AS DA24" + "\n");
            strSqlString.Append("                     , SUM(DECODE(OPER, 'A0405', DECODE(WORK_DATE,'" + dayArry2[0] + "', QTY,0))) AS DA05" + "\n");
            strSqlString.Append("                     , SUM(DECODE(OPER, 'A0405', DECODE(WORK_DATE,'" + dayArry2[1] + "', QTY,0))) AS DA15" + "\n");
            strSqlString.Append("                     , SUM(DECODE(OPER, 'A0405', DECODE(WORK_DATE,'" + dayArry2[2] + "', QTY,0))) AS DA25" + "\n");
            strSqlString.Append("                     , SUM(DECODE(OPER, 'A0406', DECODE(WORK_DATE,'" + dayArry2[0] + "', QTY,0))) AS DA06" + "\n");
            strSqlString.Append("                     , SUM(DECODE(OPER, 'A0406', DECODE(WORK_DATE,'" + dayArry2[1] + "', QTY,0))) AS DA16" + "\n");
            strSqlString.Append("                     , SUM(DECODE(OPER, 'A0406', DECODE(WORK_DATE,'" + dayArry2[2] + "', QTY,0))) AS DA26" + "\n");
            strSqlString.Append("                     , SUM(DECODE(OPER, 'A0407', DECODE(WORK_DATE,'" + dayArry2[0] + "', QTY,0))) AS DA07" + "\n");
            strSqlString.Append("                     , SUM(DECODE(OPER, 'A0407', DECODE(WORK_DATE,'" + dayArry2[1] + "', QTY,0))) AS DA17" + "\n");
            strSqlString.Append("                     , SUM(DECODE(OPER, 'A0407', DECODE(WORK_DATE,'" + dayArry2[2] + "', QTY,0))) AS DA27" + "\n");
            strSqlString.Append("                     , SUM(DECODE(OPER, 'A0408', DECODE(WORK_DATE,'" + dayArry2[0] + "', QTY,0))) AS DA08" + "\n");
            strSqlString.Append("                     , SUM(DECODE(OPER, 'A0408', DECODE(WORK_DATE,'" + dayArry2[1] + "', QTY,0))) AS DA18" + "\n");
            strSqlString.Append("                     , SUM(DECODE(OPER, 'A0408', DECODE(WORK_DATE,'" + dayArry2[2] + "', QTY,0))) AS DA28" + "\n");
            strSqlString.Append("                     , SUM(DECODE(OPER, 'A0409', DECODE(WORK_DATE,'" + dayArry2[0] + "', QTY,0))) AS DA09" + "\n");
            strSqlString.Append("                     , SUM(DECODE(OPER, 'A0409', DECODE(WORK_DATE,'" + dayArry2[1] + "', QTY,0))) AS DA19" + "\n");
            strSqlString.Append("                     , SUM(DECODE(OPER, 'A0409', DECODE(WORK_DATE,'" + dayArry2[2] + "', QTY,0))) AS DA29" + "\n");
            strSqlString.Append("                     , SUM(DECODE(OPER, 'A0602', DECODE(WORK_DATE,'" + dayArry2[0] + "', QTY,0))) AS WB02" + "\n");
            strSqlString.Append("                     , SUM(DECODE(OPER, 'A0602', DECODE(WORK_DATE,'" + dayArry2[1] + "', QTY,0))) AS WB12" + "\n");
            strSqlString.Append("                     , SUM(DECODE(OPER, 'A0602', DECODE(WORK_DATE,'" + dayArry2[2] + "', QTY,0))) AS WB22" + "\n");
            strSqlString.Append("                     , SUM(DECODE(OPER, 'A0603', DECODE(WORK_DATE,'" + dayArry2[0] + "', QTY,0))) AS WB03" + "\n");
            strSqlString.Append("                     , SUM(DECODE(OPER, 'A0603', DECODE(WORK_DATE,'" + dayArry2[1] + "', QTY,0))) AS WB13" + "\n");
            strSqlString.Append("                     , SUM(DECODE(OPER, 'A0603', DECODE(WORK_DATE,'" + dayArry2[2] + "', QTY,0))) AS WB23" + "\n");
            strSqlString.Append("                     , SUM(DECODE(OPER, 'A0604', DECODE(WORK_DATE,'" + dayArry2[0] + "', QTY,0))) AS WB04" + "\n");
            strSqlString.Append("                     , SUM(DECODE(OPER, 'A0604', DECODE(WORK_DATE,'" + dayArry2[1] + "', QTY,0))) AS WB14" + "\n");
            strSqlString.Append("                     , SUM(DECODE(OPER, 'A0604', DECODE(WORK_DATE,'" + dayArry2[2] + "', QTY,0))) AS WB24" + "\n");
            strSqlString.Append("                     , SUM(DECODE(OPER, 'A0605', DECODE(WORK_DATE,'" + dayArry2[0] + "', QTY,0))) AS WB05" + "\n");
            strSqlString.Append("                     , SUM(DECODE(OPER, 'A0605', DECODE(WORK_DATE,'" + dayArry2[1] + "', QTY,0))) AS WB15" + "\n");
            strSqlString.Append("                     , SUM(DECODE(OPER, 'A0605', DECODE(WORK_DATE,'" + dayArry2[2] + "', QTY,0))) AS WB25 " + "\n");
            strSqlString.Append("                     , SUM(DECODE(OPER, 'A0606', DECODE(WORK_DATE,'" + dayArry2[0] + "', QTY,0))) AS WB06" + "\n");
            strSqlString.Append("                     , SUM(DECODE(OPER, 'A0606', DECODE(WORK_DATE,'" + dayArry2[1] + "', QTY,0))) AS WB16" + "\n");
            strSqlString.Append("                     , SUM(DECODE(OPER, 'A0606', DECODE(WORK_DATE,'" + dayArry2[2] + "', QTY,0))) AS WB26 " + "\n");
            strSqlString.Append("                     , SUM(DECODE(OPER, 'A0607', DECODE(WORK_DATE,'" + dayArry2[0] + "', QTY,0))) AS WB07" + "\n");
            strSqlString.Append("                     , SUM(DECODE(OPER, 'A0607', DECODE(WORK_DATE,'" + dayArry2[1] + "', QTY,0))) AS WB17" + "\n");
            strSqlString.Append("                     , SUM(DECODE(OPER, 'A0607', DECODE(WORK_DATE,'" + dayArry2[2] + "', QTY,0))) AS WB27 " + "\n");
            strSqlString.Append("                     , SUM(DECODE(OPER, 'A0608', DECODE(WORK_DATE,'" + dayArry2[0] + "', QTY,0))) AS WB08" + "\n");
            strSqlString.Append("                     , SUM(DECODE(OPER, 'A0608', DECODE(WORK_DATE,'" + dayArry2[1] + "', QTY,0))) AS WB18" + "\n");
            strSqlString.Append("                     , SUM(DECODE(OPER, 'A0608', DECODE(WORK_DATE,'" + dayArry2[2] + "', QTY,0))) AS WB28 " + "\n");
            strSqlString.Append("                     , SUM(DECODE(OPER, 'A0609', DECODE(WORK_DATE,'" + dayArry2[0] + "', QTY,0))) AS WB09" + "\n");
            strSqlString.Append("                     , SUM(DECODE(OPER, 'A0609', DECODE(WORK_DATE,'" + dayArry2[1] + "', QTY,0))) AS WB19" + "\n");
            strSqlString.Append("                     , SUM(DECODE(OPER, 'A0609', DECODE(WORK_DATE,'" + dayArry2[2] + "', QTY,0))) AS WB29 " + "\n");
            strSqlString.Append("                  FROM ( " + "\n");
            strSqlString.Append("                        SELECT A.MAT_ID,A.WORK_DATE,B.OPER,B.OPER_GRP_7 " + "\n");
            strSqlString.Append("                             , SUM(A.S1_END_QTY_1 + A.S2_END_QTY_1 + A.S3_END_QTY_1 + A.S1_END_RWK_QTY_1 + A.S2_END_RWK_QTY_1 + A.S3_END_RWK_QTY_1 ) AS QTY " + "\n");
            strSqlString.Append("                          FROM RSUMWIPMOV A " + "\n");
            strSqlString.Append("                             , MWIPOPRDEF B " + "\n");
            strSqlString.Append("                         WHERE 1=1 " + "\n");
            strSqlString.Append("                           AND A.FACTORY = B.FACTORY " + "\n");
            strSqlString.Append("                           AND A.OPER = B.OPER " + "\n");
            strSqlString.Append("                           AND A.FACTORY = '" + GlobalVariable.gsAssyDefaultFactory + "' " + "\n");
            strSqlString.Append("                           AND B.OPER_GRP_7 IN ('SAW','D/A','W/B','MOLD','SIG','V/I') " + "\n");
            strSqlString.Append("                           AND A.MAT_VER = 1 " + "\n");

            //2011-03-15-�����: Lot Type���� P% or E% �� �˻��� �� �ֵ��� (�蹮�� ����� ��û ��)
            if (txtSearchProduct.Text.Trim() != "%" && txtSearchProduct.Text.Trim() != "") 
            {
                strSqlString.AppendFormat("                           AND A.CM_KEY_3 LIKE '{0}'" + "\n", txtSearchProduct.Text);
            }

            strSqlString.Append("                           AND A.WORK_DATE BETWEEN '" + dayArry2[0] + "' AND '" + dayArry2[2] + "'" + "\n");
            strSqlString.Append("                           AND A.LOT_TYPE = 'W'" + "\n");
            strSqlString.Append("                           AND A.OPER <> 'A1760'" + "\n");   
            strSqlString.Append("                         GROUP BY A.MAT_ID, A.WORK_DATE,B.OPER,B.OPER_GRP_7 " + "\n");            
            strSqlString.Append("                       ) " + "\n");
            strSqlString.Append("                 GROUP BY MAT_ID " + "\n");            
            strSqlString.Append("               ) D  " + "\n");
            strSqlString.Append("             , ( " + "\n");
            strSqlString.Append("                SELECT MAT_ID " + "\n");
            strSqlString.Append("                     , ROUND(SUM(DECODE(OPER_GRP_1, 'HMK2A', QTY, 0)), 0) AS V0 " + "\n");
            strSqlString.Append("                     , ROUND(SUM(DECODE(OPER_GRP_1, 'B/G', QTY, 0)), 0) AS V1 " + "\n");
            strSqlString.Append("                     , ROUND(SUM(DECODE(OPER_GRP_1, 'SAW', QTY, 0)), 0) AS V2 " + "\n");
            strSqlString.Append("                     , ROUND(SUM(DECODE(OPER_GRP_1, 'S/P', QTY, 0)), 0) AS V3 " + "\n");
            strSqlString.Append("                     , ROUND(SUM(DECODE(OPER_GRP_1, 'D/A', QTY, 0)), 0) AS V4 " + "\n");
            strSqlString.Append("                     , ROUND(SUM(DECODE(OPER_GRP_1, 'W/B', QTY, 0)), 0) AS V5 " + "\n");
            strSqlString.Append("                     , ROUND(SUM(DECODE(OPER_GRP_1, 'MOLD', QTY, 0)), 0) AS V6 " + "\n");
            strSqlString.Append("                     , ROUND(SUM(DECODE(OPER_GRP_1, 'CURE', QTY, 0)), 0) AS V7 " + "\n");
            strSqlString.Append("                     , ROUND(SUM(DECODE(OPER_GRP_1, 'M/K', QTY, 0)), 0) AS V8 " + "\n");
            strSqlString.Append("                     , ROUND(SUM(DECODE(OPER_GRP_1, 'TRIM', QTY, 0)), 0) AS V9 " + "\n");
            strSqlString.Append("                     , ROUND(SUM(DECODE(OPER_GRP_1, 'TIN', QTY, 0)), 0) AS V10 " + "\n");
            strSqlString.Append("                     , ROUND(SUM(DECODE(OPER_GRP_1, 'S/B/A', QTY, 0)), 0) AS V11 " + "\n");
            strSqlString.Append("                     , ROUND(SUM(DECODE(OPER_GRP_1, 'SIG', QTY, 0)), 0) AS V12 " + "\n");
            strSqlString.Append("                     , ROUND(SUM(DECODE(OPER_GRP_1, 'AVI', QTY, 0)), 0) AS V13 " + "\n");
            strSqlString.Append("                     , ROUND(SUM(DECODE(OPER_GRP_1, 'V/I', QTY, 0)), 0) AS V14 " + "\n");
            strSqlString.Append("                     , ROUND(SUM(DECODE(OPER_GRP_1, 'HMK3A', QTY, 0)), 0) AS V15 " + "\n");
            strSqlString.Append("                     , ROUND(SUM(DECODE(OPER_GRP_1, 'GATE', QTY, 0)), 0) AS V16 " + "\n");
            strSqlString.Append("                     , ROUND(SUM(DECODE(OPER_GRP_1, 'SMT', QTY, 0)), 0) AS V17 " + "\n");
            strSqlString.Append("                  FROM (  " + "\n");
            strSqlString.Append("                        SELECT MAT_ID, OPER, OPER_GRP_1 " + "\n");
            //strSqlString.Append("                             , CASE WHEN OPER <= 'A0395' THEN QTY_1 / NVL(COMP_CNT,1) ELSE QTY_1 END AS QTY " + "\n");
            // 2014-09-02-������ : HX ��ǰ ��� ���� ���� A0015, A0395 Ÿ�� ��ǰ�� ��� �ش� �������� COMP ����
            strSqlString.Append("                             , CASE WHEN MAT_GRP_1 = 'HX' AND HX_COMP_MIN IS NOT NULL" + "\n");
            strSqlString.Append("                                         THEN (CASE WHEN HX_COMP_MIN <> HX_COMP_MAX AND OPER > HX_COMP_MIN AND OPER <= HX_COMP_MAX THEN QTY_1 / NVL(COMP_CNT / 2,1)" + "\n");
            strSqlString.Append("                                                    WHEN OPER <= HX_COMP_MAX THEN QTY_1 / NVL(COMP_CNT,1)" + "\n");
            strSqlString.Append("                                                    ELSE QTY_1 END)" + "\n");
            strSqlString.Append("                                    WHEN OPER <= 'A0395' THEN QTY_1 / NVL(COMP_CNT,1) " + "\n");
            strSqlString.Append("                                    ELSE QTY_1 " + "\n");
            strSqlString.Append("                               END QTY " + "\n");
            strSqlString.Append("                          FROM ( " + "\n");
            strSqlString.Append("                                SELECT A.MAT_ID, B.OPER, C.MAT_GRP_1  " + "\n");
            strSqlString.Append("                                     , CASE WHEN C.MAT_GRP_3 IN ('FCBGA', 'FCFBGA') AND B.OPER = 'A0800' THEN 'D/A' ELSE B.OPER_GRP_1 END OPER_GRP_1  " + "\n");
            strSqlString.Append("                                     , CASE WHEN MAT_GRP_3 IN ('COB', 'BGN') THEN QTY_1/NET_DIE ELSE QTY_1 END AS QTY_1" + "\n");
            strSqlString.Append("                                     , COMP_CNT  " + "\n");
            strSqlString.Append("                                     , HX_COMP_MIN, HX_COMP_MAX " + "\n");

            if (DateTime.Now.ToString("yyyyMMdd") != cdvDate.SelectedValue())
            {
                strSqlString.Append("                                  FROM RWIPLOTSTS_BOH A " + "\n");
                strSqlString.Append("                                     , MWIPOPRDEF B " + "\n");
                strSqlString.Append("                                     , VWIPMATDEF C " + "\n");                
                strSqlString.Append("                                 WHERE A.CUTOFF_DT = '" + cdvDate.SelectedValue() + "22' " + "\n");
            }                   
            else if (DateTime.Now.ToString("yyyyMMdd") == cdvDate.SelectedValue())
            {
                strSqlString.Append("                                  FROM RWIPLOTSTS A  " + "\n");
                strSqlString.Append("                                     , MWIPOPRDEF B  " + "\n");
                strSqlString.Append("                                     , VWIPMATDEF C " + "\n");
                strSqlString.Append("                                 WHERE 1 = 1 " + "\n");
            }

            strSqlString.Append("                                   AND A.FACTORY = B.FACTORY " + "\n");
            strSqlString.Append("                                   AND A.FACTORY = C.FACTORY " + "\n");
            strSqlString.Append("                                   AND A.OPER = B.OPER " + "\n");
            strSqlString.Append("                                   AND A.MAT_ID = C.MAT_ID " + "\n");            
            strSqlString.Append("                                   AND A.FACTORY = '" + GlobalVariable.gsAssyDefaultFactory + "'  " + "\n");
            strSqlString.Append("                                   AND A.LOT_TYPE = 'W' " + "\n");
            strSqlString.Append("                                   AND A.LOT_DEL_FLAG = ' ' " + "\n");
            strSqlString.Append("                                   AND C.DELETE_FLAG = ' ' " + "\n");            
            strSqlString.Append("                                   AND C.MAT_GRP_2 <> '-' " + "\n");
            strSqlString.Append("                                   AND A.HOLD_CODE NOT IN ('H71','H54') " + "\n");
            // 2015-09-14-������ : ������� '-', '1st', 'Middle%', 'Merge' �� ���� (���¼�K ��û)
            strSqlString.Append("                                   AND REGEXP_LIKE(C.MAT_GRP_5, 'Middle*|Merge|1st|-') " + "\n");

            //2011-03-15-�����: Lot Type���� P% or E% �� �˻��� �� �ֵ��� (�蹮�� ����� ��û ��)
            if (txtSearchProduct.Text.Trim() != "%" && txtSearchProduct.Text.Trim() != "")
            {
                strSqlString.AppendFormat("                                   AND A.LOT_CMF_5 LIKE '{0}'" + "\n", txtSearchProduct.Text);
            }

            strSqlString.Append("                               ) " + "\n");            
            strSqlString.Append("                       )  " + "\n");                        
            strSqlString.Append("                 GROUP BY MAT_ID " + "\n");
            strSqlString.Append("               ) F " + "\n"); 
            strSqlString.Append("         WHERE 1 = 1 " + "\n");
            strSqlString.Append("           AND A.MAT_ID NOT IN (SELECT MAT_ID FROM MWIPMATDEF WHERE FIRST_FLOW = 'A-BANK' AND DELETE_FLAG = ' ')" + "\n");
            strSqlString.Append("           AND A.MAT_ID = D.MAT_ID(+)" + "\n");            
            strSqlString.Append("           AND A.MAT_ID = F.MAT_ID(+)" + "\n");
            strSqlString.Append("           AND A.MAT_ID LIKE '" + txtProduct.Text.ToString().Trim() + "' " + "\n");

            //�� ��ȸ�� ���� SQL�� ����                        
            if (udcWIPCondition1.Text != "ALL" && udcWIPCondition1.Text != "")
                strSqlString.AppendFormat("           AND A.MAT_GRP_1 {0}" + "\n", udcWIPCondition1.SelectedValueToQueryString);

            if (udcWIPCondition2.Text != "ALL" && udcWIPCondition2.Text != "")
                strSqlString.AppendFormat("           AND A.MAT_GRP_2 {0} " + "\n", udcWIPCondition2.SelectedValueToQueryString);

            if (udcWIPCondition3.Text != "ALL" && udcWIPCondition3.Text != "")
                strSqlString.AppendFormat("           AND A.MAT_GRP_3 {0} " + "\n", udcWIPCondition3.SelectedValueToQueryString);

            if (udcWIPCondition4.Text != "ALL" && udcWIPCondition4.Text != "")
                strSqlString.AppendFormat("           AND A.MAT_GRP_4 {0} " + "\n", udcWIPCondition4.SelectedValueToQueryString);

            if (udcWIPCondition5.Text != "ALL" && udcWIPCondition5.Text != "")
                strSqlString.AppendFormat("           AND A.MAT_GRP_5 {0} " + "\n", udcWIPCondition5.SelectedValueToQueryString);

            if (udcWIPCondition6.Text != "ALL" && udcWIPCondition6.Text != "")
                strSqlString.AppendFormat("           AND A.MAT_GRP_6 {0} " + "\n", udcWIPCondition6.SelectedValueToQueryString);

            if (udcWIPCondition7.Text != "ALL" && udcWIPCondition7.Text != "")
                strSqlString.AppendFormat("           AND A.MAT_GRP_7 {0} " + "\n", udcWIPCondition7.SelectedValueToQueryString);

            if (udcWIPCondition8.Text != "ALL" && udcWIPCondition8.Text != "")
                strSqlString.AppendFormat("           AND A.MAT_GRP_8 {0} " + "\n", udcWIPCondition8.SelectedValueToQueryString);

            if (udcWIPCondition9.Text != "ALL" && udcWIPCondition9.Text != "")
                strSqlString.AppendFormat("           AND A.MAT_GRP_9 {0} " + "\n", udcWIPCondition9.SelectedValueToQueryString);

            strSqlString.Append("         GROUP BY " + QueryCond2 + "\n");
            strSqlString.Append("        HAVING (" + "\n");
            strSqlString.Append("                NVL(SUM(A.MON_PLAN), 0)+ " + "\n");
            strSqlString.Append("                NVL(SUM(A.ORI_PLAN), 0)+ " + "\n");
            strSqlString.Append("                NVL(SUM(A.ASSY_MON), 0)+ " + "\n");
            strSqlString.Append("                NVL(SUM(A.WEEK_PLAN), 0)+ " + "\n");
            strSqlString.Append("                NVL(SUM(A.AO0), 0)+ NVL(SUM(A.AO1), 0)+ NVL(SUM(A.AO2), 0)+ " + "\n");
            strSqlString.Append("                NVL(SUM(A.RCV0), 0)+ NVL(SUM(A.RCV1), 0)+ NVL(SUM(A.RCV2), 0)+ " + "\n");
            strSqlString.Append("                NVL(SUM(D.SW0), 0)+ NVL(SUM(D.SW1), 0)+ NVL(SUM(D.SW2), 0)+ " + "\n");
            strSqlString.Append("                NVL(SUM(D.DA0), 0)+ NVL(SUM(D.DA1), 0)+ NVL(SUM(D.DA2), 0)+ " + "\n");
            strSqlString.Append("                NVL(SUM(D.WB0), 0)+ NVL(SUM(D.WB1), 0)+ NVL(SUM(D.WB2), 0)+ " + "\n");
            strSqlString.Append("                NVL(SUM(D.MD0), 0)+ NVL(SUM(D.MD1), 0)+ NVL(SUM(D.MD2), 0)+  " + "\n");
            strSqlString.Append("                NVL(SUM(D.TF0), 0)+ NVL(SUM(D.TF0), 0)+ NVL(SUM(D.TF0), 0)+ " + "\n");            
            strSqlString.Append("                NVL(SUM(F.V0+F.V1+F.V2+F.V3+F.V4+F.V5+F.V6+F.V7+F.V8+F.V9+F.V10+F.V11+F.V12+F.V13+F.V14+F.V15+F.V16+F.V17), 0)  " + "\n");
            strSqlString.Append("               ) <> 0" + "\n");
            strSqlString.Append("       ) A " + "\n");            
            strSqlString.Append("     , ( " + "\n");
            strSqlString.Append("        SELECT MAT_GRP_1 AS CUSTOMER" + "\n");
            strSqlString.Append("             , MAT_GRP_10 AS PKG " + "\n");
            strSqlString.Append("             , SUM(DECODE(SHIP_DATE,'" + dayArry2[0] + "', TAT, 0)) AS TAT0 " + "\n");
            strSqlString.Append("             , SUM(DECODE(SHIP_DATE,'" + dayArry2[1] + "', TAT, 0)) AS TAT1 " + "\n");
            strSqlString.Append("             , SUM(DECODE(SHIP_DATE,'" + dayArry2[2] + "', TAT, 0)) AS TAT2 " + "\n");
            strSqlString.Append("          FROM ( " + "\n");
            strSqlString.Append("                SELECT MAT.MAT_GRP_1,MAT.MAT_GRP_10,TAT.SHIP_DATE,SUM(TAT.TOTAL_TAT_QTY),SUM(SHIP.SHIP_QTY),ROUND(DECODE(SUM(SHIP.SHIP_QTY),0,0,(SUM(TAT.TOTAL_TAT_QTY)/SUM(SHIP.SHIP_QTY))),4) AS TAT " + "\n");            
            strSqlString.Append("                  FROM ( " + "\n");
            strSqlString.Append("                        SELECT FACTORY,MAT_ID,SHIP_DATE " + "\n");
            strSqlString.Append("                             , ROUND(SUM(TOTAL_TAT_QTY),4) AS TOTAL_TAT_QTY " + "\n");
            strSqlString.Append("                          FROM CSUMTATMAT@RPTTOMES " + "\n");
            strSqlString.Append("                         WHERE 1=1 " + "\n");
            strSqlString.Append("                           AND FACTORY = '" + GlobalVariable.gsAssyDefaultFactory + "' " + "\n");
            strSqlString.Append("                           AND OPER <> 'A0000' " + "\n");
            strSqlString.Append("                           AND SHIP_DATE BETWEEN '" + dayArry2[0] + "' AND '" + dayArry2[2] + "'" + "\n");
            strSqlString.Append("                         GROUP BY FACTORY,MAT_ID,SHIP_DATE " + "\n");
            strSqlString.Append("                       ) TAT " + "\n");
            strSqlString.Append("                     , ( " + "\n");
            strSqlString.Append("                        SELECT FACTORY,MAT_ID,SHIP_DATE,SHIP_QTY " + "\n");
            strSqlString.Append("                          FROM CSUMTATMAT@RPTTOMES " + "\n");
            strSqlString.Append("                         WHERE 1=1 " + "\n");
            strSqlString.Append("                           AND FACTORY = '" + GlobalVariable.gsAssyDefaultFactory + "' " + "\n");
            strSqlString.Append("                           AND OPER = 'AZ010' " + "\n");
            strSqlString.Append("                           AND SHIP_DATE BETWEEN '" + dayArry2[0] + "' AND '" + dayArry2[2] + "'" + "\n");
            strSqlString.Append("                       ) SHIP " + "\n");
            strSqlString.Append("                     , MWIPMATDEF MAT " + "\n"); 
            strSqlString.Append("                 WHERE 1=1 " + "\n");
            strSqlString.Append("                   AND TAT.FACTORY = SHIP.FACTORY " + "\n");
            strSqlString.Append("                   AND TAT.MAT_ID NOT IN (SELECT MAT_ID FROM MWIPMATDEF WHERE FIRST_FLOW = 'A-BANK' AND DELETE_FLAG = ' ') " + "\n");
            strSqlString.Append("                   AND TAT.MAT_ID = SHIP.MAT_ID " + "\n");
            strSqlString.Append("                   AND TAT.SHIP_DATE = SHIP.SHIP_DATE " + "\n");
            strSqlString.Append("                   AND TAT.MAT_ID = MAT.MAT_ID " + "\n");
            strSqlString.Append("                   AND MAT.MAT_TYPE = 'FG' " + "\n");
            strSqlString.Append("                   AND MAT.DELETE_FLAG <> 'Y' " + "\n");
            strSqlString.Append("                   AND MAT.FACTORY = '" + GlobalVariable.gsAssyDefaultFactory + "' " + "\n");
            strSqlString.Append("                 GROUP BY MAT_GRP_1,MAT_GRP_10,TAT.SHIP_DATE" + "\n");            
            strSqlString.Append("               ) " + "\n");
            strSqlString.Append("         GROUP BY MAT_GRP_1, MAT_GRP_10 " + "\n");          
            strSqlString.Append("       ) B  " + "\n");
            strSqlString.Append(" WHERE 1=1 " + "\n");
            strSqlString.Append("   AND A.CUSTOMER = B.CUSTOMER(+) " + "\n");
            strSqlString.Append("   AND A.PKG = B.PKG(+) " + "\n");
            strSqlString.Append("   AND A.PKG <> '-' " + "\n");
            strSqlString.Append("   AND NVL(ORI_PLAN,0) + NVL(MON_PLAN,0) + NVL(TARGET_MON,0) + NVL(ASSY_MON,0) + NVL(WEEK_PLAN,0) + NVL(DEF,0) + NVL(AO0,0) + NVL(AO1,0) + NVL(AO2,0) + NVL(A.RCV0,0) + NVL(A.RCV1,0) + NVL(A.RCV2,0) + NVL(A.SW0,0) " + "\n");
            strSqlString.Append("     + NVL(A.SW1,0) + NVL(A.SW2,0) + NVL(A.DA0,0) + NVL(A.DA1,0) + NVL(A.DA2,0) + NVL(A.WB0,0) + NVL(A.WB1,0) + NVL(A.WB2,0) + NVL(A.MD0,0) + NVL(A.MD1,0) + NVL(A.MD2,0) + NVL(A.TF0,0) " + "\n");
            strSqlString.Append("     + NVL(A.TF1,0) + NVL(A.TF2,0) + NVL(TTL,0) <> 0 " + "\n");
            strSqlString.Append(" ORDER BY " + QueryCond4 + "\n");
            
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
                int nGroupCount = ((udcTableForm)(this.btnSort.BindingForm)).GetSelectedCount();

                int[] rowType = spdData.RPT_DataBindingWithSubTotal(dt, 0, sub+1, 11, null, null, btnSort);                
                //����Ÿ���̺�, ��Ż ������ ���ѹ�, ���� ���� ������Ż � ������, ù������ ���° ������ TOTAL �������
                //spdData.Sheets[0].FrozenColumnCount = 3;
                //spdData.RPT_AutoFit(false);
                //spdData.DataSource = dt;
                //Total�κ� ������
                spdData.RPT_FillDataSelectiveCells("Total", 0, 11, 0, 1, true, Align.Center, VerticalAlign.Center);

                spdData.RPT_AutoFit(false);

                if (ckbRev.Checked == false)
                {
                    spdData.RPT_SetPerSubTotalAndGrandTotal(1, 15, 11, 16);
                }
                else
                {
                    spdData.RPT_SetPerSubTotalAndGrandTotal(1, 15, 12, 16);
                }
                
                spdData.RPT_SetAvgSubTotalAndGrandTotal(2, 51, nGroupCount, false);              
       
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
                Condition.AppendFormat("��������: {0}     today: {1}      workday: {2}     remain: {3}      ǥ��������: {4} " + "\n", cdvDate.Text, lblToday.Text.ToString(), lblLastDay.Text.ToString(), lblRemain.Text.ToString(), lblJindo.Text.ToString());
                //Condition.AppendFormat("today: {0}    workday: {1}     ǥ��������: {2} " + lblToday.Text.ToString() , lblLastDay.Text.ToString(), lblJindo.Text.ToString());
                Condition.Append("        ���� : PKG (pcs) , COB (��) ");
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

            // 2011-02-10-������ : ����, ���� ��ȸ �����ϰ� ���� ���� 3DAY �� ���� (���¼� ��û)
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

            // ���� ��ȸ�� ��� �ܿ��Ͽ� ���� ������.
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
    }       
}
