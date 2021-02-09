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
//       - 2008-08-20 : Created by John Seo
//
//
//   Copyright(C) 1998-2005 MIRACOM,INC.
//   All rights reserved.
//
//-----------------------------------------------------------------------------

using System;
using System.Data;
using System.IO;
using System.Windows.Forms;
using System.Configuration;
using Miracom.SmartWeb.FWX;


namespace Miracom.SmartWeb.UI
{
    public sealed class CmnSpdFunction
    {
        public static CmnSpdFunction oSpd = new CmnSpdFunction();

        public CmnSpdFunction()
        {
        }

        //// ��Ÿ���� �����Ҵ°��� �������� �ϰ� ������ false �ָ� ��..
        //private bool isPreCellsType = true;

        // �̰� ���ϴ� blank �� ����.
        private const double RoundValue = 0.0009;
        private const double RoundValueMinus = -0.0009;
        //private const double RoundValue2 = 0.009;
        //private const double RoundValue2Minus = -0.009;
        //private const double RoundValue2Per = 0.0009;
        //private const double RoundValue2PerMinus = -0.0009;
        //private const double RoundValue2Double = 0.04;
        //private const double RoundValue2DoubleMinus = -0.04;

        /// <summary>
        ///  �κ� �հ踦 �������.. DataSource �� ���� �����ϸ� �ȉ�. 
        /// </summary>
        /// <param name="spdCtl"> Spread ��Ʈ��</param>
        /// <param name="dt">���� ����Ÿ ���̺�</param>
        /// <param name="startSubTotalColumnPosition">�κ��հ踦 ������ �÷�. 0���� ����</param>
        /// <param name="SubTotalColumnSize">�κ��հ踦 ����� �÷� ������. 0���� ����</param>
        /// <param name="StartValueColumnPos">���� ����� ����Ÿ�� ����ִ� �÷�. 0���� ����</param>
        /// <returns>Row Ÿ�� : �ο쵥��Ÿ�� 1, �հ�� 2, ������ SubTotal �� +1 ��, ù �κ��հ� ���� 3 ����..</returns>
        public static int[] DataBindingWithSubTotal(FarPoint.Win.Spread.FpSpread spdCtl, DataTable dt, int startSubTotalColumnPosition, int SubTotalColumnSize, int StartValueColumnPos)
        {
            return DataBindingWithSubTotal(spdCtl, dt, startSubTotalColumnPosition, SubTotalColumnSize, StartValueColumnPos, null, null);
            
        }


        /// <summary>
        ///  �κ� �հ踦 �������.. DataSource �� ���� �����ϸ� �ȉ�. 
        /// </summary>
        /// <param name="spdCtl"> Spread ��Ʈ��</param>
        /// <param name="dt">���� ����Ÿ ���̺�</param>
        /// <param name="startSubTotalColumnPosition">�κ��հ踦 ������ �÷�. 0���� ����</param>
        /// <param name="SubTotalColumnSize">�κ��հ踦 ����� �÷� ������. 0���� ����</param>
        /// <param name="StartValueColumnPos">���� ����� ����Ÿ�� ����ִ� �÷�. 0���� ����</param>
        /// <param name="Formula">���� ����Ÿ�� ���� ����</param>
        /// <param name="SumFormula">�հ�� �κ��հ迡�� ���� ����.</param>
        /// <returns>Row Ÿ�� : �ο쵥��Ÿ�� 1, �հ�� 2, ������ SubTotal �� +1 ��, ù �κ��հ� ���� 3 ����..</returns>
        public static int[] DataBindingWithSubTotal(FarPoint.Win.Spread.FpSpread spdCtl, DataTable dt, int startSubTotalColumnPosition, int SubTotalColumnSize, int StartValueColumnPos, string[,] Formula, string[,] SumFormula)
        {
            if (dt == null)
                return null;

            if (dt.Rows.Count == 0)
            {
                spdCtl.DataSource = dt;
                return null;
            }

            // ���ġ�� ��� ���̺�.. 
            DataTable desc = new DataTable();
            // �ο쵥��Ÿ ������ ��� �÷�..
            int iColumnType = dt.Columns.Count;
            // ��Ż���� ������ �迭
            double[] Total = new double[dt.Columns.Count - StartValueColumnPos];
            // ���� ��Ż���� ������ �迭
            double[,] SubTotalValue = new double[SubTotalColumnSize, dt.Columns.Count - StartValueColumnPos];
            // ������Ż�� ������ �Ǵ� ��
            string[] SubTotal = new string[SubTotalColumnSize];
            // ������Ż�� �߰����� �����Ҷ� ������ ���ذ���..
            string[] Base = new string[StartValueColumnPos];
            DataRow rows = null;
            // �κ� �հ�� ����Ÿ row
            DataRow[] subTotalRows = new DataRow[SubTotalColumnSize];
            // �κ��հ踦 ���� ����� �κ��հ谡 �ٲ�� ������ �ϴ��� �κ��հ踦 �ٲٱ�����.
            bool isSuperChanged;
            // ����Ÿ Ÿ���� �����س��� ���
            string[] DataTypes = new string[dt.Columns.Count];
            // DataTable �� Row�� { �Ϲ�, �հ�, �κ��հ� } �� ������ �迭
            int[] rowType;
            // �ӽ� ����  RoundValue ������ ���� ������ ���Ͽ�
            double temp = 0;
            
            // �÷� Ÿ�� ����
            for (int i = 0; i < dt.Columns.Count; i++)
            {
                desc.Columns.Add(dt.Columns[i].ColumnName, dt.Columns[i].DataType);
                DataTypes[i] = dt.Columns[i].DataType.Name.ToUpper();
            }
            
            // �հ����� �ο� ����Ÿ���� �����ϱ� ���� �÷���..
            // �ο쵥��Ÿ�� 1
            // �հ�� 2
            // ������ SubTotal �� +1 ��
            desc.Columns.Add("iDataRowType", typeof(int));

            // ��Ż��
            DataRow TotalRow = desc.NewRow();

            // ������Ż ���񱳸� ���� �� �̸� ����. 
            if (dt.Rows.Count > 0)
            {
                for (int i = 0; i < SubTotalColumnSize; i++)
                {
                    SubTotal[i] = (dt.Rows[0][startSubTotalColumnPosition + i] == null ? null : dt.Rows[0][startSubTotalColumnPosition + i].ToString());
                }
                // base ����
                for (int i = 0; i < Base.Length; i++)
                {
                    Base[i] = (dt.Rows[0][i] == null ? null : dt.Rows[0][i].ToString());
                }
            }

            // ��Ż �߰���.. 
            desc.Rows.Add(TotalRow);

            // ��ü row �� ��ȸ��.. 
            for (int row = 0; row < dt.Rows.Count; row++)
            {
                rows = desc.NewRow();

                isSuperChanged = false;

                // ��ü �÷��� ��ȸ��.. 
                for (int column = 0; column < dt.Columns.Count; column++)
                {
                    // �κ��հ� ��� ������ ���鵵 �κ��հ踦 ���� ���Ͽ� �˻�. ( �߰��κк��� �κ��հ踦 ���� ���׸� ���� ���Ͽ� )
                    if (column < startSubTotalColumnPosition)
                    {
                        if (Base[column] != (dt.Rows[row][column] == null ? null : ((string)dt.Rows[row][column])))
                        {
                            isSuperChanged = true;
                            Base[column] = (dt.Rows[row][column] == null ? null : dt.Rows[row][column].ToString());
                        }
                    }
                    // �κ��հ踦 ���� ���Ͽ� �����Ͱ� ������ ��
                    else if (column >= startSubTotalColumnPosition && column < (startSubTotalColumnPosition + SubTotalColumnSize))
                    {
                        // �ڱ��ڽ��� Ʋ���ų� ������ �κ��հ谡 ����Ȱ��  ���������� ������ ����. 
                        if (SubTotal[column - startSubTotalColumnPosition] != (dt.Rows[row][column] == null ? null : ((string)dt.Rows[row][column])) || isSuperChanged)
                        {
                            subTotalRows[column - startSubTotalColumnPosition] = desc.NewRow();

                            // ���� ������Ż row �� �����ϰ� �ٽ� ���� ����.. �ϱ� ���Ͽ� 0 ���� �ʱ�ȭ
                            for (int subValue = 0; subValue < (dt.Columns.Count - StartValueColumnPos); subValue++)
                            {
                                // ������Ż�� �����ΰ�츸 ������..  
                                if (DataTypes[StartValueColumnPos + subValue] == "INT" || DataTypes[StartValueColumnPos + subValue] == "INT32" || DataTypes[StartValueColumnPos + subValue] == "DOUBLE" || DataTypes[StartValueColumnPos + subValue] == "DECIMAL") 
                                    subTotalRows[column - startSubTotalColumnPosition][StartValueColumnPos + subValue] = SubTotalValue[column - startSubTotalColumnPosition, subValue];

                                SubTotalValue[column - startSubTotalColumnPosition, subValue] = 0;
                            }

                            // �κ��հ� ���� ������ ü��. Merge �� ���ؼ�. 
                            if (row != 0)
                            {
                                for (int i = 0; i < column + 1; i++)
                                {
                                    subTotalRows[column - startSubTotalColumnPosition][i] = dt.Rows[row - 1][i];
                                }
                            }
                            subTotalRows[column - startSubTotalColumnPosition][column + 1] = SubTotal[column - startSubTotalColumnPosition] + " Sub Total";

                            SubTotal[column - startSubTotalColumnPosition] = (dt.Rows[row][column] == null ? null : dt.Rows[row][column].ToString());

                            isSuperChanged = true;
                        }

                    }

                    // StartValueColumnPos �̻��� �÷��鸸 �����س���.. 
                    // ��Ż�� ������Ż ���� �����..
                    if (column >= StartValueColumnPos)
                    {
                        // ���� Ÿ���� ������Ż�� �����
                        if (DataTypes[column] == "INT" || DataTypes[column] == "INT32" || DataTypes[column] == "DOUBLE" || DataTypes[column] == "DECIMAL")
                        {
                            // ��Ż�� ���..
                            if (dt.Rows[row][column] != null && dt.Rows[row][column] != DBNull.Value)
                            {
                                Total[column - StartValueColumnPos] += Convert.ToDouble(dt.Rows[row][column]);

                                //���� ��Ż�� ������. 
                                for (int sub = 0; sub < SubTotal.Length; sub++)
                                {
                                    SubTotalValue[sub, column - StartValueColumnPos] += Convert.ToDouble(dt.Rows[row][column]);
                                }
                            }
                        }
                    }


                    // �Ϲ� low data ����
                    // Double Ÿ���� �ӵ��� ���ؼ� 0.009 ������ ������ ������ null ������ ������.
                    if (dt.Rows[row][column] != null && dt.Rows[row][column] != DBNull.Value)
                        if (DataTypes[column] == "DOUBLE")
                        {
                            temp = Convert.ToDouble(dt.Rows[row][column]);
                            if (temp < RoundValue && temp > RoundValueMinus)
                                rows[column] = 0;//DBNull.Value;
                            else
                                rows[column] = dt.Rows[row][column];
                        }
                        else
                            rows[column] = dt.Rows[row][column];
                    else
                        rows[column] = dt.Rows[row][column];
                }
                
                // �κ��հ迭 �߰�.. 
                // �������� �־�߸�.. ��.. 
                for (int sub = subTotalRows.Length - 1; sub >= 0; sub--)
                {
                    if (subTotalRows[sub] != null)
                    {
                        // ����Ÿ Ÿ���� �Է���.. 
                        // �ο쵥��Ÿ�� 1
                        // �հ�� 2
                        // ������ SubTotal �� +1 ��, ù �κ��հ� ���� 3 ����..
                        subTotalRows[sub][iColumnType] = sub + 3;
                        desc.Rows.Add(subTotalRows[sub]);
                        subTotalRows[sub] = null;
                    }
                }
                
                // ����Ÿ Ÿ���� �Է���.. 
                // �ο쵥��Ÿ�� 1
                // �հ�� 2
                // ������ SubTotal �� +1 ��
                rows[iColumnType] = 1;

                desc.Rows.Add(rows);
            }

            // ��Ż���� ä��.. 
            TotalRow[0] = "Total";
            // ����Ÿ Ÿ���� �Է���.. 
            // �ο쵥��Ÿ�� 1
            // �հ�� 2
            TotalRow[iColumnType] = 2;
            for (int i = StartValueColumnPos; i < dt.Columns.Count; i++)
            {
                // ���� Ÿ���� ������Ż�� �����
                if (DataTypes[i] == "INT" || DataTypes[i] == "INT32" || DataTypes[i] == "DOUBLE" || DataTypes[i] == "DECIMAL")
                    TotalRow[i] = Total[i - StartValueColumnPos];
            }

            // �������� �����ִ� �κ��հ� �ڿ������� ä��.. 
            for (int subValue = SubTotal.Length - 1; subValue >= 0; subValue--)
            {
                DataRow subTotalRow = desc.NewRow();

                for (int col = StartValueColumnPos; col < dt.Columns.Count; col++)
                {
                    if (DataTypes[col] == "INT" || DataTypes[col] == "INT32" || DataTypes[col] == "DOUBLE" || DataTypes[col] == "DECIMAL")
                        subTotalRow[col] = SubTotalValue[subValue, col - StartValueColumnPos];
                }

                // �κ��հ� ���� ������ ü��. 
                for (int i = 0; i < (subValue + 1 + startSubTotalColumnPosition); i++)
                {
                    subTotalRow[i] = dt.Rows[dt.Rows.Count - 1][i];
                }

                // ����Ÿ Ÿ���� �Է���.. 
                // �ο쵥��Ÿ�� 1
                // �հ�� 2
                // ������ SubTotal �� +1 ��, ù �κ��հ� ���� 3 ����..
                subTotalRow[iColumnType] = subValue + 3;
                subTotalRow[startSubTotalColumnPosition + subValue + 1] = SubTotal[subValue] + " Sub Total";
                desc.Rows.Add(subTotalRow);
            }

            //
            // �� ���ϴ� FarPoint ���� => �ٸ� �׸��忡���� ��������..
            //

            // �÷��� �̸� �ʱ�ȭ ���� ���ϵ��� ����. 
            //isPreCellsType = false;

            int[] merge = new int[StartValueColumnPos];

            //// Merge ������ �����.. 
            //for (int i = 0; i < merge.Length; i++)
            //{
            //    if (spdCtl.ActiveSheet.Columns[i].MergePolicy == FarPoint.Win.Spread.Model.MergePolicy.Always)
            //        merge[i] = 1;
            //    else if (spdCtl.ActiveSheet.Columns[i].MergePolicy == FarPoint.Win.Spread.Model.MergePolicy.None)
            //        merge[i] = 2;
            //    else
            //        merge[i] = 3;
            //    spdCtl.ActiveSheet.Columns[i].MergePolicy = FarPoint.Win.Spread.Model.MergePolicy.None;
            //}
            
            // �׸��忡 ����Ÿ ���̺� ����
            spdCtl.DataSource = desc;
            
            // ����Ÿ�� ���� ��� ������ �ʱ�ȭ ���� ����. 
            if (spdCtl.ActiveSheet.RowCount == 0)
                return null;
            
            // ������ �ʱ�ȭ..
            spdCtl.ActiveSheet.FrozenRowCount = 1;
            // iDataRowType Ÿ���� ���� 
            spdCtl.ActiveSheet.ColumnCount = desc.Columns.Count - 1;

            // ��������.. 
            // fix �� �÷� ��ŭ �ݺ�
            for (int i = 0; i < StartValueColumnPos; i++)
                spdCtl.ActiveSheet.Columns[i].BackColor = System.Drawing.Color.FromArgb(((System.Byte)(255)), ((System.Byte)(255)), ((System.Byte)(218))); ;
            // ��Ż �÷�.
            spdCtl.ActiveSheet.Rows[0].BackColor = System.Drawing.Color.FromArgb(((System.Byte)(255)), ((System.Byte)(233)), ((System.Byte)(204)));

            // �հ�κ� ����.. 
            spdCtl.ActiveSheet.Cells[0, 0].ColumnSpan = SubTotalColumnSize+1;
            spdCtl.ActiveSheet.Cells[0, 0].VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            spdCtl.ActiveSheet.Cells[0, 0].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Left;

            // �հ� Formula ����
            if (SumFormula != null)
            {
                for (int cel = 0; cel < SumFormula.Length / 2; cel++)
                {
                    spdCtl.ActiveSheet.Cells[0, Convert.ToInt32(SumFormula[cel, 0])].Formula = SumFormula[cel, 1];
                }
            }

            // Row Type�� ������ �Լ�..
            rowType = new int[desc.Rows.Count];
            rowType[0] = 2;	// ù��°�� ������ �հ���.

            // Formula ����( �κ��հ� + Low ����Ÿ )
            // �κ��հ� �÷�
            for (int i = 1; i < desc.Rows.Count; i++)
            {
                rowType[i] = ((int)desc.Rows[i][iColumnType]);

                // �ο쵥��Ÿ�� �հ�� �����ϰ� ��ĥ��.. 
                if (rowType[i] > 2)
                {
                    for (int j = ((int)startSubTotalColumnPosition + (int)desc.Rows[i][iColumnType] - 2); j < spdCtl.ActiveSheet.ColumnCount; j++)
                    {
                        spdCtl.ActiveSheet.Cells[i, j].BackColor = System.Drawing.Color.FromArgb(((System.Byte)(222)), ((System.Byte)(236)), ((System.Byte)(242)));

                    }
                    // �κ� �հ�κ� ����..                      
                    spdCtl.ActiveSheet.Cells[i, ((int)startSubTotalColumnPosition + (int)desc.Rows[i][iColumnType] - 2)].ColumnSpan = StartValueColumnPos - ((int)startSubTotalColumnPosition + (int)desc.Rows[i][iColumnType] - 2);                    
                    spdCtl.ActiveSheet.Cells[i, ((int)startSubTotalColumnPosition + (int)desc.Rows[i][iColumnType] - 2)].VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
                    spdCtl.ActiveSheet.Cells[i, ((int)startSubTotalColumnPosition + (int)desc.Rows[i][iColumnType] - 2)].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Left;
                     
                    // �κ� �հ� Formula ����
                    if (SumFormula != null)
                    {
                        for (int cel = 0; cel < SumFormula.Length / 2; cel++)
                        {
                            spdCtl.ActiveSheet.Cells[i, Convert.ToInt32(SumFormula[cel, 0])].Formula = SumFormula[cel, 1];
                        }
                    }
                }
                else
                {
                    // Low Data Formula ����
                    if (Formula != null)
                    {
                        for (int cel = 0; cel < Formula.Length / 2; cel++)
                        {
                            spdCtl.ActiveSheet.Cells[i, Convert.ToInt32(Formula[cel, 0])].Formula = Formula[cel, 1];
                        }
                    }
                }
            }

            // 0 �� blank �� �ٲ�..
            //spdCtl.SetCellsType();
             
            //isPreCellsType = true;

            // Merge �� �ٽ� ��.. 
            for (int i = 0; i < merge.Length; i++)
            {
                spdCtl.ActiveSheet.Columns[i].MergePolicy = FarPoint.Win.Spread.Model.MergePolicy.Always;
                
                //if (merge[i] == 1)
                //    spdCtl.ActiveSheet.Columns[i].MergePolicy = FarPoint.Win.Spread.Model.MergePolicy.Always;
                //else if (merge[i] == 2)
                //    spdCtl.ActiveSheet.Columns[i].MergePolicy = FarPoint.Win.Spread.Model.MergePolicy.None;
                //else
                //    spdCtl.ActiveSheet.Columns[i].MergePolicy = FarPoint.Win.Spread.Model.MergePolicy.Restricted;
            }

            // �ο쵥��Ÿ�� 1
            // �հ�� 2
            // ������ SubTotal �� +1 ��, ù �κ��հ� ���� 3 ����..
            return rowType;
        }

        /// <summary>
        ///  �߰� : 2007.08.09. By ������
        ///  2�� �̻��� �κ� �հ踦 �������.. DataSource �� ���� �����ϸ� �ȉ�. 
        /// </summary>
        /// <param name="spdCtl"> Spread ��Ʈ��</param>
        /// <param name="dt">���� ����Ÿ ���̺�</param>
        /// <param name="startSubTotalColumnPosition">�κ��հ踦 ������ �÷�..</param>
        /// <param name="SubTotalColumnSize">�κ��հ踦 ����� �÷� ������</param>
        /// <param name="StartValueColumnPos">���� ����� ����Ÿ�� ����ִ� �÷�</param>
        /// <returns>Row Ÿ�� : �ο쵥��Ÿ�� 1, �հ�� 2, ������ SubTotal �� +1 ��, ù �κ��հ� ���� 3 ����..</returns>
        public static int[] DataBindingWithSubTotalAndDivideRows(FarPoint.Win.Spread.FpSpread spdCtl, DataTable dt, int startSubTotalColumnPosition, int SubTotalColumnSize, int StartValueColumnPos,
            int basisColumnSize, int divideRowsSize, int repeatColumnSize)
        {
            ////TIMES
            //int T1 = Common.clsUtility.getJustTime();

            if (dt == null)
                return null;

            if (dt.Rows.Count == 0)
            {
                spdCtl.DataSource = dt;
                return null;
            }

            // ���ġ�� ��� ���̺�.. 
            DataTable desc = new DataTable();
            // �ο쵥��Ÿ ������ ��� �÷�..
            int iColumnType = dt.Columns.Count;
            // ��Ż���� ������ �迭
            double[] Total = new double[dt.Columns.Count - StartValueColumnPos];
            // ���� ��Ż���� ������ �迭
            double[,] SubTotalValue = new double[SubTotalColumnSize, dt.Columns.Count - StartValueColumnPos];
            // ������Ż�� ������ �Ǵ� ��
            string[] SubTotal = new string[SubTotalColumnSize];
            // ������Ż�� �߰����� �����Ҷ� ������ ���ذ���..
            string[] Base = new string[StartValueColumnPos];
            DataRow rows = null;
            // �κ� �հ�� ����Ÿ row
            DataRow[] subTotalRows = new DataRow[SubTotalColumnSize];
            // �κ��հ踦 ���� ����� �κ��հ谡 �ٲ�� ������ �ϴ��� �κ��հ踦 �ٲٱ�����.
            bool isSuperChanged;
            // ����Ÿ Ÿ���� �����س��� ���
            string[] DataTypes = new string[dt.Columns.Count];
            // DataTable �� Row�� { �Ϲ�, �հ�, �κ��հ� } �� ������ �迭
            int[] rowType;

            // �÷� Ÿ�� ����
            for (int i = 0; i < dt.Columns.Count; i++)
            {
                desc.Columns.Add(dt.Columns[i].ColumnName, dt.Columns[i].DataType);
                DataTypes[i] = dt.Columns[i].DataType.Name.ToUpper();
            }

            // �հ����� �ο� ����Ÿ���� �����ϱ� ���� �÷���..
            // �ο쵥��Ÿ�� 1
            // �հ�� 2
            // ������ SubTotal �� +1 ��
            desc.Columns.Add("iDataRowType", typeof(int));

            // ��Ż��
            DataRow TotalRow = desc.NewRow();

            // ������Ż ���񱳸� ���� �� �̸� ����. 
            if (dt.Rows.Count > 0)
            {
                for (int i = 0; i < SubTotalColumnSize; i++)
                {
                    SubTotal[i] = (dt.Rows[0][startSubTotalColumnPosition + i] == null ? null : dt.Rows[0][startSubTotalColumnPosition + i].ToString());
                }
                // base ����
                for (int i = 0; i < Base.Length; i++)
                {
                    Base[i] = (dt.Rows[0][i] == null ? null : dt.Rows[0][i].ToString());
                }
            }

            // ��Ż �߰���.. 
            desc.Rows.Add(TotalRow);

            // ��ü row �� ��ȸ��.. 
            for (int row = 0; row < dt.Rows.Count; row++)
            {
                rows = desc.NewRow();

                isSuperChanged = false;

                // ��ü �÷��� ��ȸ��.. 
                for (int column = 0; column < dt.Columns.Count; column++)
                {
                    // �κ��հ� ��� ������ ���鵵 �κ��հ踦 ���� ���Ͽ� �˻�. ( �߰��κк��� �κ��հ踦 ���� ���׸� ���� ���Ͽ� )
                    if (column < startSubTotalColumnPosition)
                    {
                        if (Base[column] != (dt.Rows[row][column] == null ? null : ((string)dt.Rows[row][column])))
                        {
                            isSuperChanged = true;
                            Base[column] = (dt.Rows[row][column] == null ? null : dt.Rows[row][column].ToString());
                        }
                    }
                    // �κ��հ踦 ���� ���Ͽ� �����Ͱ� ������ ��
                    else if (column >= startSubTotalColumnPosition && column < (startSubTotalColumnPosition + SubTotalColumnSize))
                    {
                        // �ڱ��ڽ��� Ʋ���ų� ������ �κ��հ谡 ����Ȱ��  ���������� ������ ����. 
                        if (SubTotal[column - startSubTotalColumnPosition] != (dt.Rows[row][column] == null ? null : ((string)dt.Rows[row][column])) || isSuperChanged)
                        {
                            subTotalRows[column - startSubTotalColumnPosition] = desc.NewRow();

                            // ���� ������Ż row �� �����ϰ� �ٽ� ���� ����.. �ϱ� ���Ͽ� 0 ���� �ʱ�ȭ
                            for (int subValue = 0; subValue < (dt.Columns.Count - StartValueColumnPos); subValue++)
                            {
                                // ������Ż�� �����ΰ�츸 ������.. 
                                if (DataTypes[StartValueColumnPos + subValue] == "INT" || DataTypes[StartValueColumnPos + subValue] == "INT32" || DataTypes[StartValueColumnPos + subValue] == "DOUBLE" || DataTypes[StartValueColumnPos + subValue] == "DECIMAL")
                                    subTotalRows[column - startSubTotalColumnPosition][StartValueColumnPos + subValue] = SubTotalValue[column - startSubTotalColumnPosition, subValue];

                                SubTotalValue[column - startSubTotalColumnPosition, subValue] = 0;
                            }

                            // �κ��հ� ���� ������ ü��. Merge �� ���ؼ�. 
                            if (row != 0)
                            {
                                for (int i = 0; i < column + 1; i++)
                                {
                                    subTotalRows[column - startSubTotalColumnPosition][i] = dt.Rows[row - 1][i];
                                }
                            }
                            subTotalRows[column - startSubTotalColumnPosition][column + 1] = SubTotal[column - startSubTotalColumnPosition] + " Sub Total";

                            SubTotal[column - startSubTotalColumnPosition] = (dt.Rows[row][column] == null ? null : dt.Rows[row][column].ToString());

                            isSuperChanged = true;
                        }
                    }

                    // StartValueColumnPos �̻��� �÷��鸸 �����س���.. 
                    // ��Ż�� ������Ż ���� �����..
                    if (column >= StartValueColumnPos)
                    {
                        // ���� Ÿ���� ������Ż�� �����
                        if (DataTypes[column] == "INT" || DataTypes[column] == "INT32" || DataTypes[column] == "DOUBLE" || DataTypes[column] == "DECIMAL")
                        {
                            // ��Ż�� ���..
                            if (dt.Rows[row][column] != null && dt.Rows[row][column] != DBNull.Value)
                            {
                                Total[column - StartValueColumnPos] += Convert.ToDouble(dt.Rows[row][column]);

                                //���� ��Ż�� ������. 
                                for (int sub = 0; sub < SubTotal.Length; sub++)
                                {
                                    SubTotalValue[sub, column - StartValueColumnPos] += Convert.ToDouble(dt.Rows[row][column]);
                                }
                            }
                        }
                    }

                    // �Ϲ� low data ����
                    // Double Ÿ���� �ӵ��� ���ؼ� 0.009 ������ ������ ������ null ������ ������.
                    if (dt.Rows[row][column] != null && dt.Rows[row][column] != DBNull.Value)
                        if (DataTypes[column] == "DOUBLE")
                            if (Convert.ToDouble(dt.Rows[row][column]) < RoundValue)
                                rows[column] = 0;//DBNull.Value;
                            else
                                rows[column] = dt.Rows[row][column];
                        else
                            rows[column] = dt.Rows[row][column];
                    else
                        rows[column] = dt.Rows[row][column];

                }

                // �κ��հ迭 �߰�.. 
                // �������� �־�߸�.. ��.. 
                for (int sub = subTotalRows.Length - 1; sub >= 0; sub--)
                {
                    if (subTotalRows[sub] != null)
                    {
                        // ����Ÿ Ÿ���� �Է���.. 
                        // �ο쵥��Ÿ�� 1
                        // �հ�� 2
                        // ������ SubTotal �� +1 ��, ù �κ��հ� ���� 3 ����..
                        subTotalRows[sub][iColumnType] = sub + 3;
                        desc.Rows.Add(subTotalRows[sub]);
                        subTotalRows[sub] = null;
                    }
                }

                // ����Ÿ Ÿ���� �Է���.. 
                // �ο쵥��Ÿ�� 1
                // �հ�� 2
                // ������ SubTotal �� +1 ��
                rows[iColumnType] = 1;

                desc.Rows.Add(rows);
            }

            // ��Ż���� ä��.. 
            TotalRow[0] = "Total";
            // ����Ÿ Ÿ���� �Է���.. 
            // �ο쵥��Ÿ�� 1
            // �հ�� 2
            TotalRow[iColumnType] = 2;
            for (int i = StartValueColumnPos; i < dt.Columns.Count; i++)
            {
                // ���� Ÿ���� ������Ż�� �����
                if (DataTypes[i] == "INT" || DataTypes[i] == "INT32" || DataTypes[i] == "DOUBLE" || DataTypes[i] == "DECIMAL")
                    TotalRow[i] = Total[i - StartValueColumnPos];
            }

            // �������� �����ִ� �κ��հ� �ڿ������� ä��.. 
            for (int subValue = SubTotal.Length - 1; subValue >= 0; subValue--)
            {
                DataRow subTotalRow = desc.NewRow();

                for (int col = StartValueColumnPos; col < dt.Columns.Count; col++)
                {
                    if (DataTypes[col] == "INT" || DataTypes[col] == "INT32" || DataTypes[col] == "DOUBLE" || DataTypes[col] == "DECIMAL")
                        subTotalRow[col] = SubTotalValue[subValue, col - StartValueColumnPos];
                }

                // �κ��հ� ���� ������ ü��. 
                for (int i = 0; i < (subValue + 1 + startSubTotalColumnPosition); i++)
                {
                    subTotalRow[i] = dt.Rows[dt.Rows.Count - 1][i];
                }

                // ����Ÿ Ÿ���� �Է���.. 
                // �ο쵥��Ÿ�� 1
                // �հ�� 2
                // ������ SubTotal �� +1 ��, ù �κ��հ� ���� 3 ����..
                subTotalRow[iColumnType] = subValue + 3;
                subTotalRow[startSubTotalColumnPosition + subValue + 1] = SubTotal[subValue] + " Sub Total";
                desc.Rows.Add(subTotalRow);
            }

            //
            // �� ���ϴ� FarPoint ���� => �ٸ� �׸��忡���� ��������..
            //

            // �÷��� �̸� �ʱ�ȭ ���� ���ϵ��� ����. 
            //isPreCellsType = false;

            int[] merge = new int[StartValueColumnPos];

            // Merge ������ �����.. 
            for (int i = 0; i < merge.Length; i++)
            {
                if (spdCtl.ActiveSheet.Columns[i].MergePolicy == FarPoint.Win.Spread.Model.MergePolicy.Always)
                    merge[i] = 1;
                else if (spdCtl.ActiveSheet.Columns[i].MergePolicy == FarPoint.Win.Spread.Model.MergePolicy.None)
                    merge[i] = 2;
                else
                    merge[i] = 3;
                spdCtl.ActiveSheet.Columns[i].MergePolicy = FarPoint.Win.Spread.Model.MergePolicy.None;
            }

            // �׸��忡 ����Ÿ ���̺� ����
            spdCtl.DataSource = desc;
            
            // ����Ÿ�� ���� ��� ������ �ʱ�ȭ ���� ����. 
            if (spdCtl.ActiveSheet.RowCount == 0)
                return null;

            // ������ �ʱ�ȭ..
            spdCtl.ActiveSheet.FrozenRowCount = 1;
            // iDataRowType Ÿ���� ���� 
            spdCtl.ActiveSheet.ColumnCount = desc.Columns.Count - 1;

            Spd_DivideRows(spdCtl, basisColumnSize, divideRowsSize, repeatColumnSize);

            // ��������.. 
            // fix �� �÷� ��ŭ �ݺ�
            for (int i = 0; i < StartValueColumnPos; i++)
                spdCtl.ActiveSheet.Columns[i].BackColor = System.Drawing.Color.FromArgb(((System.Byte)(255)), ((System.Byte)(255)), ((System.Byte)(218))); ;
            // ��Ż �÷�.
            for (int k = 0; k < divideRowsSize; k++)
                spdCtl.ActiveSheet.Rows[0 + k].BackColor = System.Drawing.Color.FromArgb(((System.Byte)(255)), ((System.Byte)(233)), ((System.Byte)(204)));
            
            // �հ�κ� ����.. 
            spdCtl.ActiveSheet.Cells[0, 0].ColumnSpan = SubTotalColumnSize+1;
            spdCtl.ActiveSheet.Cells[0, 0].VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            spdCtl.ActiveSheet.Cells[0, 0].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Left;
            spdCtl.ActiveSheet.Cells[0, 0].RowSpan = divideRowsSize;

            // Row Type�� ������ �Լ�..
            rowType = new int[desc.Rows.Count];
            rowType[0] = 2;	// ù��°�� ������ �հ���.

            ///////////////////////////////////////////////////////////////////////////////////
            // Formula ����( �κ��հ� + Low ����Ÿ )
            // �κ��հ� �÷�
            for (int i = 1; i < desc.Rows.Count; i++)
            {
                rowType[i] = ((int)desc.Rows[i][iColumnType]);

                // �ο쵥��Ÿ�� �հ�� �����ϰ� ��ĥ��.. 
                if (rowType[i] > 2)
                {
                    for (int j = ((int)startSubTotalColumnPosition + (int)desc.Rows[i][iColumnType] - 2); j < spdCtl.ActiveSheet.ColumnCount; j++)
                    {
                        for (int k = 0; k < divideRowsSize; k++)
                            spdCtl.ActiveSheet.Cells[i * divideRowsSize + k, j].BackColor = System.Drawing.Color.FromArgb(((System.Byte)(222)), ((System.Byte)(236)), ((System.Byte)(242)));

                    }
                    // �κ� �հ�κ� ����.. 
                    spdCtl.ActiveSheet.Cells[i * divideRowsSize, ((int)startSubTotalColumnPosition + (int)desc.Rows[i][iColumnType] - 2)].ColumnSpan = StartValueColumnPos - ((int)startSubTotalColumnPosition + (int)desc.Rows[i][iColumnType] - 2);
                    spdCtl.ActiveSheet.Cells[i * divideRowsSize, ((int)startSubTotalColumnPosition + (int)desc.Rows[i][iColumnType] - 2)].VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
                    spdCtl.ActiveSheet.Cells[i * divideRowsSize, ((int)startSubTotalColumnPosition + (int)desc.Rows[i][iColumnType] - 2)].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Left;
                    spdCtl.ActiveSheet.Cells[i * divideRowsSize, ((int)startSubTotalColumnPosition + (int)desc.Rows[i][iColumnType] - 2)].RowSpan = divideRowsSize;
                }
            }
            ///////////////////////////////////////////////////////////

            // 0 �� blank �� �ٲ�..
            //spdCtl.SetCellsType();

            //isPreCellsType = true;

            // Merge �� �ٽ� ��.. 
            for (int i = 0; i < merge.Length; i++)
            {
                if (merge[i] == 1)
                    spdCtl.ActiveSheet.Columns[i].MergePolicy = FarPoint.Win.Spread.Model.MergePolicy.Always;
                else if (merge[i] == 2)
                    spdCtl.ActiveSheet.Columns[i].MergePolicy = FarPoint.Win.Spread.Model.MergePolicy.None;
                else
                    spdCtl.ActiveSheet.Columns[i].MergePolicy = FarPoint.Win.Spread.Model.MergePolicy.Restricted;
            }

            // �ο쵥��Ÿ�� 1
            // �հ�� 2
            // ������ SubTotal �� +1 ��, ù �κ��հ� ���� 3 ����..
            return rowType;
        }


        /// <summary>
        /// ������ �����ٷ� ��Ȱ�ϴ� �޼ҵ�,, ������ ���� ���õ�
        /// </summary>
        /// <param name="basisColumnSize">��Ȱ���� ���� ���� �÷���</param>
        /// <param name="divideRowsSize">��Ȱ�� Row ��</param>
        /// <param name="repeatColumnSize">���ٿ� �� Column �� - ���������� �ݺ��Ǿ����</param>        
        public static void Spd_DivideRows(FarPoint.Win.Spread.FpSpread spdCtl, int basisColumnSize, int divideRowsSize, int repeatColumnSize)
        {

            // �ڵ� Sort �� ����
            for (int i = 0; i < spdCtl.ActiveSheet.ColumnCount; i++)
                spdCtl.ActiveSheet.Columns.Get(i).AllowAutoSort = false;

            DataTable dt = new DataTable();
            DataTable org = (DataTable)spdCtl.DataSource;
            //			this.DataSource = null;

            // Ÿ�Կ� �´� �÷��� ����
            for (int i = 0; i < (basisColumnSize + repeatColumnSize); i++)
            {
                dt.Columns.Add(org.Columns[i].ColumnName, org.Columns[i].DataType);
            }

            // ���̺��� ���������� �ݺ�
            for (int currentOrgRow = 0; currentOrgRow < org.Rows.Count; currentOrgRow++)
            {
                // �������� ���̺��� column ��(��Ȯ���� basisColumnSize �� ���ؾ���)  .. row ���� currentOrgRow
                int currentOrgColum = 0;

                // ��Ȱ�� Row ����ŭ ����
                for (int j = 0; j < divideRowsSize; j++)
                {
                    DataRow row = dt.NewRow();

                    // �⺻ �÷��� ����
                    for (int k = 0; k < basisColumnSize; k++)
                    {
                        row[k] = org.Rows[currentOrgRow][k];
                    }

                    // ������ �� ����
                    for (int m = 0; m < repeatColumnSize; m++, currentOrgColum++)
                    {
                        row[basisColumnSize + m] = org.Rows[currentOrgRow][basisColumnSize + currentOrgColum];
                    }

                    // row ���
                    dt.Rows.Add(row);
                }

            }
 
            // ������ ����
            spdCtl.DataSource = dt;

            // row fix
            spdCtl.ActiveSheet.FrozenRowCount *= divideRowsSize;

            //if (isPreCellsType)
            //{
            //    Spd_SetCellsType();
            //}

        }

        // Ư�� ������ Merge Ÿ������ �����ϴ°�.
        public static void Spd_SetMerge(FarPoint.Win.Spread.FpSpread spdCtl, int ColumnPos, int ColumnSize)
        {
            spdCtl.ActiveSheet.Columns[ColumnPos].MergePolicy = FarPoint.Win.Spread.Model.MergePolicy.Always;
            for (int i = ColumnPos + 1; i < ColumnSize; i++)
                spdCtl.ActiveSheet.Columns[i].MergePolicy = FarPoint.Win.Spread.Model.MergePolicy.Restricted;
        }

        // Spread���� Excel ���
        public static void SaveToExcel(FarPoint.Win.Spread.FpSpread fpSpread)
        {
            SaveFileDialog saveFileDialog = new System.Windows.Forms.SaveFileDialog();

            saveFileDialog.Filter = "Excel Files (*.xls)|*.xls";
            saveFileDialog.FilterIndex = 1;
            saveFileDialog.RestoreDirectory = true;

            DialogResult result = saveFileDialog.ShowDialog();

            if (result == DialogResult.OK)
            {
                foreach (FarPoint.Win.Spread.SheetView currentSheet in fpSpread.Sheets)
                {
                    currentSheet.Protect = false;
                }

                string fileName = saveFileDialog.FileName;

                try
                {
                    fpSpread.SaveExcel(fileName, FarPoint.Win.Spread.Model.IncludeHeaders.BothCustomOnly);
                    //MsgHelper.Instance.Show("ExcelExport", "������ �����߽��ϴ�.");
                }
                catch
                {
                    //MsgHelper.Instance.Show(MsgType.ErrMsg, "ExcelExport", "���� ���忡 �����߽��ϴ�.");
                }
            }

            for (int i = 0; i < fpSpread.ActiveSheet.Columns.Count; i++)
            {
                fpSpread.ActiveSheet.Columns[i].Locked = true;
            }

        }


        //public static void Spd_SetCellsType()
        //{
           
        //    int size = 0;
        //    if (_cellType.Count > this.ActiveSheet.Columns.Count)
        //        size = this.ActiveSheet.Columns.Count;
        //    else
        //        size = _cellType.Count;

        //    for (int i = 0; i < size; i++)
        //    {
        //        this.ActiveSheet.Columns[i].CellType = (FarPoint.Win.Spread.CellType.ICellType)_cellType[i];
        //        if (_cellType[i] != _text && _cellType[i] != _number)
        //            Spd_SetBlankFromZero(i);
        //    }
        //}

        //// �÷� Ÿ���� ���� ������ ��� 0 �̸� �� ������ �������� ó����..  
        //public static void Spd_SetBlankFromZero(int columnPos)
        //{
        //    double tempRound = RoundValue2;
        //    double tempRoundMinus = RoundValue2Minus;
        //    double temp = 0;

        //    if (_cellType[columnPos] == _percentDouble2)
        //    {
        //        tempRound = RoundValue2Per;
        //        tempRoundMinus = RoundValue2PerMinus;
        //    }
        //    else if (_cellType[columnPos] == _double)
        //    {
        //        tempRound = RoundValue2Double;
        //        tempRoundMinus = RoundValue2DoubleMinus;
        //    }

        //    try
        //    {
        //        for (int i = 0; i < this.ActiveSheet.Rows.Count; i++)
        //        {
        //            if (this.ActiveSheet.Cells[i, columnPos].Value == null)
        //                continue;

        //            temp = Convert.ToDouble(this.ActiveSheet.Cells[i, columnPos].Value);
        //            if (temp < tempRound && temp > tempRoundMinus)
        //            {

        //                this.ActiveSheet.Cells[i, columnPos].Formula = "";
        //                this.ActiveSheet.Cells[i, columnPos].Text = "";
        //            }
        //        }
        //    }
        //    catch (Exception)
        //    {
        //        throw new Exception(" RPT_SetBlankFromZero : �÷� Ÿ���� �߸��Ǿ����ϴ� ");
        //    }
        //}

         
    }
}
