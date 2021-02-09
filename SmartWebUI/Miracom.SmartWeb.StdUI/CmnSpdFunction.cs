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

        //// 셀타입을 지정할는것을 수동으로 하고 싶을때 false 주면 됨..
        //private bool isPreCellsType = true;

        // 이값 이하는 blank 로 만듬.
        private const double RoundValue = 0.0009;
        private const double RoundValueMinus = -0.0009;
        //private const double RoundValue2 = 0.009;
        //private const double RoundValue2Minus = -0.009;
        //private const double RoundValue2Per = 0.0009;
        //private const double RoundValue2PerMinus = -0.0009;
        //private const double RoundValue2Double = 0.04;
        //private const double RoundValue2DoubleMinus = -0.04;

        /// <summary>
        ///  부분 합계를 만들어줌.. DataSource 를 먼저 셋팅하면 안됌. 
        /// </summary>
        /// <param name="spdCtl"> Spread 컨트롤</param>
        /// <param name="dt">원본 데이타 테이블</param>
        /// <param name="startSubTotalColumnPosition">부분합계를 시작할 컬럼. 0부터 시작</param>
        /// <param name="SubTotalColumnSize">부분합계를 계산할 컬럼 사이즈. 0부터 시작</param>
        /// <param name="StartValueColumnPos">실제 계산할 데이타가 들어있는 컬럼. 0부터 시작</param>
        /// <returns>Row 타입 : 로우데이타는 1, 합계는 2, 각각의 SubTotal 은 +1 씩, 첫 부분합계 값은 3 부터..</returns>
        public static int[] DataBindingWithSubTotal(FarPoint.Win.Spread.FpSpread spdCtl, DataTable dt, int startSubTotalColumnPosition, int SubTotalColumnSize, int StartValueColumnPos)
        {
            return DataBindingWithSubTotal(spdCtl, dt, startSubTotalColumnPosition, SubTotalColumnSize, StartValueColumnPos, null, null);
            
        }


        /// <summary>
        ///  부분 합계를 만들어줌.. DataSource 를 먼저 셋팅하면 안됌. 
        /// </summary>
        /// <param name="spdCtl"> Spread 컨트롤</param>
        /// <param name="dt">원본 데이타 테이블</param>
        /// <param name="startSubTotalColumnPosition">부분합계를 시작할 컬럼. 0부터 시작</param>
        /// <param name="SubTotalColumnSize">부분합계를 계산할 컬럼 사이즈. 0부터 시작</param>
        /// <param name="StartValueColumnPos">실제 계산할 데이타가 들어있는 컬럼. 0부터 시작</param>
        /// <param name="Formula">실제 데이타에 넣을 공식</param>
        /// <param name="SumFormula">합계와 부분합계에만 넣을 공식.</param>
        /// <returns>Row 타입 : 로우데이타는 1, 합계는 2, 각각의 SubTotal 은 +1 씩, 첫 부분합계 값은 3 부터..</returns>
        public static int[] DataBindingWithSubTotal(FarPoint.Win.Spread.FpSpread spdCtl, DataTable dt, int startSubTotalColumnPosition, int SubTotalColumnSize, int StartValueColumnPos, string[,] Formula, string[,] SumFormula)
        {
            if (dt == null)
                return null;

            if (dt.Rows.Count == 0)
            {
                spdCtl.DataSource = dt;
                return null;
            }

            // 결과치가 담길 테이블.. 
            DataTable desc = new DataTable();
            // 로우데이타 구분이 담길 컬럼..
            int iColumnType = dt.Columns.Count;
            // 토탈값을 유지할 배열
            double[] Total = new double[dt.Columns.Count - StartValueColumnPos];
            // 서브 토탈값을 유지할 배열
            double[,] SubTotalValue = new double[SubTotalColumnSize, dt.Columns.Count - StartValueColumnPos];
            // 서브토탈의 기준이 되는 값
            string[] SubTotal = new string[SubTotalColumnSize];
            // 서브토탈이 중간부터 시작할때 상위의 기준값들..
            string[] Base = new string[StartValueColumnPos];
            DataRow rows = null;
            // 부분 합계용 데이타 row
            DataRow[] subTotalRows = new DataRow[SubTotalColumnSize];
            // 부분합계를 낼때 상단의 부분합계가 바뀌면 무조건 하단의 부분합계를 바꾸기위해.
            bool isSuperChanged;
            // 데이타 타입을 저장해놓을 장소
            string[] DataTypes = new string[dt.Columns.Count];
            // DataTable 을 Row별 { 일반, 합계, 부분합계 } 을 리턴할 배열
            int[] rowType;
            // 임시 변수  RoundValue 이하의 값은 버리기 위하여
            double temp = 0;
            
            // 컬럼 타입 생성
            for (int i = 0; i < dt.Columns.Count; i++)
            {
                desc.Columns.Add(dt.Columns[i].ColumnName, dt.Columns[i].DataType);
                DataTypes[i] = dt.Columns[i].DataType.Name.ToUpper();
            }
            
            // 합계인지 로우 데이타인지 구분하기 위한 플래그..
            // 로우데이타는 1
            // 합계는 2
            // 각각의 SubTotal 은 +1 씩
            desc.Columns.Add("iDataRowType", typeof(int));

            // 토탈값
            DataRow TotalRow = desc.NewRow();

            // 서브토탈 값비교를 위한 값 미리 셋팅. 
            if (dt.Rows.Count > 0)
            {
                for (int i = 0; i < SubTotalColumnSize; i++)
                {
                    SubTotal[i] = (dt.Rows[0][startSubTotalColumnPosition + i] == null ? null : dt.Rows[0][startSubTotalColumnPosition + i].ToString());
                }
                // base 설정
                for (int i = 0; i < Base.Length; i++)
                {
                    Base[i] = (dt.Rows[0][i] == null ? null : dt.Rows[0][i].ToString());
                }
            }

            // 토탈 추가함.. 
            desc.Rows.Add(TotalRow);

            // 전체 row 을 순회함.. 
            for (int row = 0; row < dt.Rows.Count; row++)
            {
                rows = desc.NewRow();

                isSuperChanged = false;

                // 전체 컬럼을 순회함.. 
                for (int column = 0; column < dt.Columns.Count; column++)
                {
                    // 부분합계 계산 이전의 값들도 부분합계를 내기 위하여 검사. ( 중간부분부터 부분합계를 낼때 버그를 막기 위하여 )
                    if (column < startSubTotalColumnPosition)
                    {
                        if (Base[column] != (dt.Rows[row][column] == null ? null : ((string)dt.Rows[row][column])))
                        {
                            isSuperChanged = true;
                            Base[column] = (dt.Rows[row][column] == null ? null : dt.Rows[row][column].ToString());
                        }
                    }
                    // 부분합계를 내기 위하여 이전것과 같은지 비교
                    else if (column >= startSubTotalColumnPosition && column < (startSubTotalColumnPosition + SubTotalColumnSize))
                    {
                        // 자기자신이 틀리거나 상위의 부분합계가 변경된경우  하위에것은 무조건 변경. 
                        if (SubTotal[column - startSubTotalColumnPosition] != (dt.Rows[row][column] == null ? null : ((string)dt.Rows[row][column])) || isSuperChanged)
                        {
                            subTotalRows[column - startSubTotalColumnPosition] = desc.NewRow();

                            // 값을 서브토탈 row 에 복사하고 다시 누적 시작.. 하기 위하여 0 으로 초기화
                            for (int subValue = 0; subValue < (dt.Columns.Count - StartValueColumnPos); subValue++)
                            {
                                // 서브토탈이 숫자인경우만 더해줌..  
                                if (DataTypes[StartValueColumnPos + subValue] == "INT" || DataTypes[StartValueColumnPos + subValue] == "INT32" || DataTypes[StartValueColumnPos + subValue] == "DOUBLE" || DataTypes[StartValueColumnPos + subValue] == "DECIMAL") 
                                    subTotalRows[column - startSubTotalColumnPosition][StartValueColumnPos + subValue] = SubTotalValue[column - startSubTotalColumnPosition, subValue];

                                SubTotalValue[column - startSubTotalColumnPosition, subValue] = 0;
                            }

                            // 부분합계 이전 값들을 체움. Merge 를 위해서. 
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

                    // StartValueColumnPos 이상의 컬럼들만 누적해나감.. 
                    // 토탈및 서브토탈 값을 계산함..
                    if (column >= StartValueColumnPos)
                    {
                        // 숫자 타입인 서브토탈을 계산함
                        if (DataTypes[column] == "INT" || DataTypes[column] == "INT32" || DataTypes[column] == "DOUBLE" || DataTypes[column] == "DECIMAL")
                        {
                            // 토탈값 계산..
                            if (dt.Rows[row][column] != null && dt.Rows[row][column] != DBNull.Value)
                            {
                                Total[column - StartValueColumnPos] += Convert.ToDouble(dt.Rows[row][column]);

                                //서브 토탈을 누적함. 
                                for (int sub = 0; sub < SubTotal.Length; sub++)
                                {
                                    SubTotalValue[sub, column - StartValueColumnPos] += Convert.ToDouble(dt.Rows[row][column]);
                                }
                            }
                        }
                    }


                    // 일반 low data 복사
                    // Double 타입은 속도를 위해서 0.009 이하의 값들은 버리고 null 값으로 셋팅함.
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
                
                // 부분합계열 추가.. 
                // 역순으로 넣어야만.. 됨.. 
                for (int sub = subTotalRows.Length - 1; sub >= 0; sub--)
                {
                    if (subTotalRows[sub] != null)
                    {
                        // 데이타 타입을 입력함.. 
                        // 로우데이타는 1
                        // 합계는 2
                        // 각각의 SubTotal 은 +1 씩, 첫 부분합계 값은 3 부터..
                        subTotalRows[sub][iColumnType] = sub + 3;
                        desc.Rows.Add(subTotalRows[sub]);
                        subTotalRows[sub] = null;
                    }
                }
                
                // 데이타 타입을 입력함.. 
                // 로우데이타는 1
                // 합계는 2
                // 각각의 SubTotal 은 +1 씩
                rows[iColumnType] = 1;

                desc.Rows.Add(rows);
            }

            // 토탈값을 채움.. 
            TotalRow[0] = "Total";
            // 데이타 타입을 입력함.. 
            // 로우데이타는 1
            // 합계는 2
            TotalRow[iColumnType] = 2;
            for (int i = StartValueColumnPos; i < dt.Columns.Count; i++)
            {
                // 숫자 타입인 서브토탈을 계산함
                if (DataTypes[i] == "INT" || DataTypes[i] == "INT32" || DataTypes[i] == "DOUBLE" || DataTypes[i] == "DECIMAL")
                    TotalRow[i] = Total[i - StartValueColumnPos];
            }

            // 마지막에 남아있는 부분합계 뒤에서부터 채움.. 
            for (int subValue = SubTotal.Length - 1; subValue >= 0; subValue--)
            {
                DataRow subTotalRow = desc.NewRow();

                for (int col = StartValueColumnPos; col < dt.Columns.Count; col++)
                {
                    if (DataTypes[col] == "INT" || DataTypes[col] == "INT32" || DataTypes[col] == "DOUBLE" || DataTypes[col] == "DECIMAL")
                        subTotalRow[col] = SubTotalValue[subValue, col - StartValueColumnPos];
                }

                // 부분합계 이전 값들을 체움. 
                for (int i = 0; i < (subValue + 1 + startSubTotalColumnPosition); i++)
                {
                    subTotalRow[i] = dt.Rows[dt.Rows.Count - 1][i];
                }

                // 데이타 타입을 입력함.. 
                // 로우데이타는 1
                // 합계는 2
                // 각각의 SubTotal 은 +1 씩, 첫 부분합계 값은 3 부터..
                subTotalRow[iColumnType] = subValue + 3;
                subTotalRow[startSubTotalColumnPosition + subValue + 1] = SubTotal[subValue] + " Sub Total";
                desc.Rows.Add(subTotalRow);
            }

            //
            // 이 이하는 FarPoint 전용 => 다른 그리드에서는 지워야함..
            //

            // 컬럼을 미리 초기화 하지 못하도록 막음. 
            //isPreCellsType = false;

            int[] merge = new int[StartValueColumnPos];

            //// Merge 정보를 기록함.. 
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
            
            // 그리드에 데이타 테이블 적용
            spdCtl.DataSource = desc;
            
            // 데이타가 없는 경우 나머지 초기화 하지 않음. 
            if (spdCtl.ActiveSheet.RowCount == 0)
                return null;
            
            // 나머지 초기화..
            spdCtl.ActiveSheet.FrozenRowCount = 1;
            // iDataRowType 타입은 제거 
            spdCtl.ActiveSheet.ColumnCount = desc.Columns.Count - 1;

            // 색상지정.. 
            // fix 된 컬럼 만큼 반복
            for (int i = 0; i < StartValueColumnPos; i++)
                spdCtl.ActiveSheet.Columns[i].BackColor = System.Drawing.Color.FromArgb(((System.Byte)(255)), ((System.Byte)(255)), ((System.Byte)(218))); ;
            // 토탈 컬러.
            spdCtl.ActiveSheet.Rows[0].BackColor = System.Drawing.Color.FromArgb(((System.Byte)(255)), ((System.Byte)(233)), ((System.Byte)(204)));

            // 합계부분 머지.. 
            spdCtl.ActiveSheet.Cells[0, 0].ColumnSpan = SubTotalColumnSize+1;
            spdCtl.ActiveSheet.Cells[0, 0].VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            spdCtl.ActiveSheet.Cells[0, 0].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Left;

            // 합계 Formula 적용
            if (SumFormula != null)
            {
                for (int cel = 0; cel < SumFormula.Length / 2; cel++)
                {
                    spdCtl.ActiveSheet.Cells[0, Convert.ToInt32(SumFormula[cel, 0])].Formula = SumFormula[cel, 1];
                }
            }

            // Row Type을 리턴할 함수..
            rowType = new int[desc.Rows.Count];
            rowType[0] = 2;	// 첫번째는 무조건 합계임.

            // Formula 적용( 부분합계 + Low 데이타 )
            // 부분합계 컬러
            for (int i = 1; i < desc.Rows.Count; i++)
            {
                rowType[i] = ((int)desc.Rows[i][iColumnType]);

                // 로우데이타와 합계는 제외하고 색칠함.. 
                if (rowType[i] > 2)
                {
                    for (int j = ((int)startSubTotalColumnPosition + (int)desc.Rows[i][iColumnType] - 2); j < spdCtl.ActiveSheet.ColumnCount; j++)
                    {
                        spdCtl.ActiveSheet.Cells[i, j].BackColor = System.Drawing.Color.FromArgb(((System.Byte)(222)), ((System.Byte)(236)), ((System.Byte)(242)));

                    }
                    // 부분 합계부분 머지..                      
                    spdCtl.ActiveSheet.Cells[i, ((int)startSubTotalColumnPosition + (int)desc.Rows[i][iColumnType] - 2)].ColumnSpan = StartValueColumnPos - ((int)startSubTotalColumnPosition + (int)desc.Rows[i][iColumnType] - 2);                    
                    spdCtl.ActiveSheet.Cells[i, ((int)startSubTotalColumnPosition + (int)desc.Rows[i][iColumnType] - 2)].VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
                    spdCtl.ActiveSheet.Cells[i, ((int)startSubTotalColumnPosition + (int)desc.Rows[i][iColumnType] - 2)].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Left;
                     
                    // 부분 합계 Formula 적용
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
                    // Low Data Formula 적용
                    if (Formula != null)
                    {
                        for (int cel = 0; cel < Formula.Length / 2; cel++)
                        {
                            spdCtl.ActiveSheet.Cells[i, Convert.ToInt32(Formula[cel, 0])].Formula = Formula[cel, 1];
                        }
                    }
                }
            }

            // 0 을 blank 로 바꿈..
            //spdCtl.SetCellsType();
             
            //isPreCellsType = true;

            // Merge 를 다시 켬.. 
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

            // 로우데이타는 1
            // 합계는 2
            // 각각의 SubTotal 은 +1 씩, 첫 부분합계 값은 3 부터..
            return rowType;
        }

        /// <summary>
        ///  추가 : 2007.08.09. By 서영국
        ///  2열 이상의 부분 합계를 만들어줌.. DataSource 를 먼저 셋팅하면 안됌. 
        /// </summary>
        /// <param name="spdCtl"> Spread 컨트롤</param>
        /// <param name="dt">원본 데이타 테이블</param>
        /// <param name="startSubTotalColumnPosition">부분합계를 시작할 컬럼..</param>
        /// <param name="SubTotalColumnSize">부분합계를 계산할 컬럼 사이즈</param>
        /// <param name="StartValueColumnPos">실제 계산할 데이타가 들어있는 컬럼</param>
        /// <returns>Row 타입 : 로우데이타는 1, 합계는 2, 각각의 SubTotal 은 +1 씩, 첫 부분합계 값은 3 부터..</returns>
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

            // 결과치가 담길 테이블.. 
            DataTable desc = new DataTable();
            // 로우데이타 구분이 담길 컬럼..
            int iColumnType = dt.Columns.Count;
            // 토탈값을 유지할 배열
            double[] Total = new double[dt.Columns.Count - StartValueColumnPos];
            // 서브 토탈값을 유지할 배열
            double[,] SubTotalValue = new double[SubTotalColumnSize, dt.Columns.Count - StartValueColumnPos];
            // 서브토탈의 기준이 되는 값
            string[] SubTotal = new string[SubTotalColumnSize];
            // 서브토탈이 중간부터 시작할때 상위의 기준값들..
            string[] Base = new string[StartValueColumnPos];
            DataRow rows = null;
            // 부분 합계용 데이타 row
            DataRow[] subTotalRows = new DataRow[SubTotalColumnSize];
            // 부분합계를 낼때 상단의 부분합계가 바뀌면 무조건 하단의 부분합계를 바꾸기위해.
            bool isSuperChanged;
            // 데이타 타입을 저장해놓을 장소
            string[] DataTypes = new string[dt.Columns.Count];
            // DataTable 을 Row별 { 일반, 합계, 부분합계 } 을 리턴할 배열
            int[] rowType;

            // 컬럼 타입 생성
            for (int i = 0; i < dt.Columns.Count; i++)
            {
                desc.Columns.Add(dt.Columns[i].ColumnName, dt.Columns[i].DataType);
                DataTypes[i] = dt.Columns[i].DataType.Name.ToUpper();
            }

            // 합계인지 로우 데이타인지 구분하기 위한 플래그..
            // 로우데이타는 1
            // 합계는 2
            // 각각의 SubTotal 은 +1 씩
            desc.Columns.Add("iDataRowType", typeof(int));

            // 토탈값
            DataRow TotalRow = desc.NewRow();

            // 서브토탈 값비교를 위한 값 미리 셋팅. 
            if (dt.Rows.Count > 0)
            {
                for (int i = 0; i < SubTotalColumnSize; i++)
                {
                    SubTotal[i] = (dt.Rows[0][startSubTotalColumnPosition + i] == null ? null : dt.Rows[0][startSubTotalColumnPosition + i].ToString());
                }
                // base 설정
                for (int i = 0; i < Base.Length; i++)
                {
                    Base[i] = (dt.Rows[0][i] == null ? null : dt.Rows[0][i].ToString());
                }
            }

            // 토탈 추가함.. 
            desc.Rows.Add(TotalRow);

            // 전체 row 을 순회함.. 
            for (int row = 0; row < dt.Rows.Count; row++)
            {
                rows = desc.NewRow();

                isSuperChanged = false;

                // 전체 컬럼을 순회함.. 
                for (int column = 0; column < dt.Columns.Count; column++)
                {
                    // 부분합계 계산 이전의 값들도 부분합계를 내기 위하여 검사. ( 중간부분부터 부분합계를 낼때 버그를 막기 위하여 )
                    if (column < startSubTotalColumnPosition)
                    {
                        if (Base[column] != (dt.Rows[row][column] == null ? null : ((string)dt.Rows[row][column])))
                        {
                            isSuperChanged = true;
                            Base[column] = (dt.Rows[row][column] == null ? null : dt.Rows[row][column].ToString());
                        }
                    }
                    // 부분합계를 내기 위하여 이전것과 같은지 비교
                    else if (column >= startSubTotalColumnPosition && column < (startSubTotalColumnPosition + SubTotalColumnSize))
                    {
                        // 자기자신이 틀리거나 상위의 부분합계가 변경된경우  하위에것은 무조건 변경. 
                        if (SubTotal[column - startSubTotalColumnPosition] != (dt.Rows[row][column] == null ? null : ((string)dt.Rows[row][column])) || isSuperChanged)
                        {
                            subTotalRows[column - startSubTotalColumnPosition] = desc.NewRow();

                            // 값을 서브토탈 row 에 복사하고 다시 누적 시작.. 하기 위하여 0 으로 초기화
                            for (int subValue = 0; subValue < (dt.Columns.Count - StartValueColumnPos); subValue++)
                            {
                                // 서브토탈이 숫자인경우만 더해줌.. 
                                if (DataTypes[StartValueColumnPos + subValue] == "INT" || DataTypes[StartValueColumnPos + subValue] == "INT32" || DataTypes[StartValueColumnPos + subValue] == "DOUBLE" || DataTypes[StartValueColumnPos + subValue] == "DECIMAL")
                                    subTotalRows[column - startSubTotalColumnPosition][StartValueColumnPos + subValue] = SubTotalValue[column - startSubTotalColumnPosition, subValue];

                                SubTotalValue[column - startSubTotalColumnPosition, subValue] = 0;
                            }

                            // 부분합계 이전 값들을 체움. Merge 를 위해서. 
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

                    // StartValueColumnPos 이상의 컬럼들만 누적해나감.. 
                    // 토탈및 서브토탈 값을 계산함..
                    if (column >= StartValueColumnPos)
                    {
                        // 숫자 타입인 서브토탈을 계산함
                        if (DataTypes[column] == "INT" || DataTypes[column] == "INT32" || DataTypes[column] == "DOUBLE" || DataTypes[column] == "DECIMAL")
                        {
                            // 토탈값 계산..
                            if (dt.Rows[row][column] != null && dt.Rows[row][column] != DBNull.Value)
                            {
                                Total[column - StartValueColumnPos] += Convert.ToDouble(dt.Rows[row][column]);

                                //서브 토탈을 누적함. 
                                for (int sub = 0; sub < SubTotal.Length; sub++)
                                {
                                    SubTotalValue[sub, column - StartValueColumnPos] += Convert.ToDouble(dt.Rows[row][column]);
                                }
                            }
                        }
                    }

                    // 일반 low data 복사
                    // Double 타입은 속도를 위해서 0.009 이하의 값들은 버리고 null 값으로 셋팅함.
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

                // 부분합계열 추가.. 
                // 역순으로 넣어야만.. 됨.. 
                for (int sub = subTotalRows.Length - 1; sub >= 0; sub--)
                {
                    if (subTotalRows[sub] != null)
                    {
                        // 데이타 타입을 입력함.. 
                        // 로우데이타는 1
                        // 합계는 2
                        // 각각의 SubTotal 은 +1 씩, 첫 부분합계 값은 3 부터..
                        subTotalRows[sub][iColumnType] = sub + 3;
                        desc.Rows.Add(subTotalRows[sub]);
                        subTotalRows[sub] = null;
                    }
                }

                // 데이타 타입을 입력함.. 
                // 로우데이타는 1
                // 합계는 2
                // 각각의 SubTotal 은 +1 씩
                rows[iColumnType] = 1;

                desc.Rows.Add(rows);
            }

            // 토탈값을 채움.. 
            TotalRow[0] = "Total";
            // 데이타 타입을 입력함.. 
            // 로우데이타는 1
            // 합계는 2
            TotalRow[iColumnType] = 2;
            for (int i = StartValueColumnPos; i < dt.Columns.Count; i++)
            {
                // 숫자 타입인 서브토탈을 계산함
                if (DataTypes[i] == "INT" || DataTypes[i] == "INT32" || DataTypes[i] == "DOUBLE" || DataTypes[i] == "DECIMAL")
                    TotalRow[i] = Total[i - StartValueColumnPos];
            }

            // 마지막에 남아있는 부분합계 뒤에서부터 채움.. 
            for (int subValue = SubTotal.Length - 1; subValue >= 0; subValue--)
            {
                DataRow subTotalRow = desc.NewRow();

                for (int col = StartValueColumnPos; col < dt.Columns.Count; col++)
                {
                    if (DataTypes[col] == "INT" || DataTypes[col] == "INT32" || DataTypes[col] == "DOUBLE" || DataTypes[col] == "DECIMAL")
                        subTotalRow[col] = SubTotalValue[subValue, col - StartValueColumnPos];
                }

                // 부분합계 이전 값들을 체움. 
                for (int i = 0; i < (subValue + 1 + startSubTotalColumnPosition); i++)
                {
                    subTotalRow[i] = dt.Rows[dt.Rows.Count - 1][i];
                }

                // 데이타 타입을 입력함.. 
                // 로우데이타는 1
                // 합계는 2
                // 각각의 SubTotal 은 +1 씩, 첫 부분합계 값은 3 부터..
                subTotalRow[iColumnType] = subValue + 3;
                subTotalRow[startSubTotalColumnPosition + subValue + 1] = SubTotal[subValue] + " Sub Total";
                desc.Rows.Add(subTotalRow);
            }

            //
            // 이 이하는 FarPoint 전용 => 다른 그리드에서는 지워야함..
            //

            // 컬럼을 미리 초기화 하지 못하도록 막음. 
            //isPreCellsType = false;

            int[] merge = new int[StartValueColumnPos];

            // Merge 정보를 기록함.. 
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

            // 그리드에 데이타 테이블 적용
            spdCtl.DataSource = desc;
            
            // 데이타가 없는 경우 나머지 초기화 하지 않음. 
            if (spdCtl.ActiveSheet.RowCount == 0)
                return null;

            // 나머지 초기화..
            spdCtl.ActiveSheet.FrozenRowCount = 1;
            // iDataRowType 타입은 제거 
            spdCtl.ActiveSheet.ColumnCount = desc.Columns.Count - 1;

            Spd_DivideRows(spdCtl, basisColumnSize, divideRowsSize, repeatColumnSize);

            // 색상지정.. 
            // fix 된 컬럼 만큼 반복
            for (int i = 0; i < StartValueColumnPos; i++)
                spdCtl.ActiveSheet.Columns[i].BackColor = System.Drawing.Color.FromArgb(((System.Byte)(255)), ((System.Byte)(255)), ((System.Byte)(218))); ;
            // 토탈 컬러.
            for (int k = 0; k < divideRowsSize; k++)
                spdCtl.ActiveSheet.Rows[0 + k].BackColor = System.Drawing.Color.FromArgb(((System.Byte)(255)), ((System.Byte)(233)), ((System.Byte)(204)));
            
            // 합계부분 머지.. 
            spdCtl.ActiveSheet.Cells[0, 0].ColumnSpan = SubTotalColumnSize+1;
            spdCtl.ActiveSheet.Cells[0, 0].VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            spdCtl.ActiveSheet.Cells[0, 0].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Left;
            spdCtl.ActiveSheet.Cells[0, 0].RowSpan = divideRowsSize;

            // Row Type을 리턴할 함수..
            rowType = new int[desc.Rows.Count];
            rowType[0] = 2;	// 첫번째는 무조건 합계임.

            ///////////////////////////////////////////////////////////////////////////////////
            // Formula 적용( 부분합계 + Low 데이타 )
            // 부분합계 컬러
            for (int i = 1; i < desc.Rows.Count; i++)
            {
                rowType[i] = ((int)desc.Rows[i][iColumnType]);

                // 로우데이타와 합계는 제외하고 색칠함.. 
                if (rowType[i] > 2)
                {
                    for (int j = ((int)startSubTotalColumnPosition + (int)desc.Rows[i][iColumnType] - 2); j < spdCtl.ActiveSheet.ColumnCount; j++)
                    {
                        for (int k = 0; k < divideRowsSize; k++)
                            spdCtl.ActiveSheet.Cells[i * divideRowsSize + k, j].BackColor = System.Drawing.Color.FromArgb(((System.Byte)(222)), ((System.Byte)(236)), ((System.Byte)(242)));

                    }
                    // 부분 합계부분 머지.. 
                    spdCtl.ActiveSheet.Cells[i * divideRowsSize, ((int)startSubTotalColumnPosition + (int)desc.Rows[i][iColumnType] - 2)].ColumnSpan = StartValueColumnPos - ((int)startSubTotalColumnPosition + (int)desc.Rows[i][iColumnType] - 2);
                    spdCtl.ActiveSheet.Cells[i * divideRowsSize, ((int)startSubTotalColumnPosition + (int)desc.Rows[i][iColumnType] - 2)].VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
                    spdCtl.ActiveSheet.Cells[i * divideRowsSize, ((int)startSubTotalColumnPosition + (int)desc.Rows[i][iColumnType] - 2)].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Left;
                    spdCtl.ActiveSheet.Cells[i * divideRowsSize, ((int)startSubTotalColumnPosition + (int)desc.Rows[i][iColumnType] - 2)].RowSpan = divideRowsSize;
                }
            }
            ///////////////////////////////////////////////////////////

            // 0 을 blank 로 바꿈..
            //spdCtl.SetCellsType();

            //isPreCellsType = true;

            // Merge 를 다시 켬.. 
            for (int i = 0; i < merge.Length; i++)
            {
                if (merge[i] == 1)
                    spdCtl.ActiveSheet.Columns[i].MergePolicy = FarPoint.Win.Spread.Model.MergePolicy.Always;
                else if (merge[i] == 2)
                    spdCtl.ActiveSheet.Columns[i].MergePolicy = FarPoint.Win.Spread.Model.MergePolicy.None;
                else
                    spdCtl.ActiveSheet.Columns[i].MergePolicy = FarPoint.Win.Spread.Model.MergePolicy.Restricted;
            }

            // 로우데이타는 1
            // 합계는 2
            // 각각의 SubTotal 은 +1 씩, 첫 부분합계 값은 3 부터..
            return rowType;
        }


        /// <summary>
        /// 한줄을 여러줄로 분활하는 메소드,, 나머지 줄은 무시됨
        /// </summary>
        /// <param name="basisColumnSize">분활되지 않을 기준 컬럼들</param>
        /// <param name="divideRowsSize">분활한 Row 수</param>
        /// <param name="repeatColumnSize">한줄에 들어갈 Column 수 - 연속적으로 반복되어야함</param>        
        public static void Spd_DivideRows(FarPoint.Win.Spread.FpSpread spdCtl, int basisColumnSize, int divideRowsSize, int repeatColumnSize)
        {

            // 자동 Sort 를 막음
            for (int i = 0; i < spdCtl.ActiveSheet.ColumnCount; i++)
                spdCtl.ActiveSheet.Columns.Get(i).AllowAutoSort = false;

            DataTable dt = new DataTable();
            DataTable org = (DataTable)spdCtl.DataSource;
            //			this.DataSource = null;

            // 타입에 맞는 컬럼을 생성
            for (int i = 0; i < (basisColumnSize + repeatColumnSize); i++)
            {
                dt.Columns.Add(org.Columns[i].ColumnName, org.Columns[i].DataType);
            }

            // 테이블이 끝날때까지 반복
            for (int currentOrgRow = 0; currentOrgRow < org.Rows.Count; currentOrgRow++)
            {
                // 오리지널 테이블의 column 값(정확히는 basisColumnSize 를 더해야함)  .. row 값은 currentOrgRow
                int currentOrgColum = 0;

                // 분활될 Row 수만큼 생성
                for (int j = 0; j < divideRowsSize; j++)
                {
                    DataRow row = dt.NewRow();

                    // 기본 컬럼값 셋팅
                    for (int k = 0; k < basisColumnSize; k++)
                    {
                        row[k] = org.Rows[currentOrgRow][k];
                    }

                    // 나머지 값 세팅
                    for (int m = 0; m < repeatColumnSize; m++, currentOrgColum++)
                    {
                        row[basisColumnSize + m] = org.Rows[currentOrgRow][basisColumnSize + currentOrgColum];
                    }

                    // row 등록
                    dt.Rows.Add(row);
                }

            }
 
            // 마지막 리턴
            spdCtl.DataSource = dt;

            // row fix
            spdCtl.ActiveSheet.FrozenRowCount *= divideRowsSize;

            //if (isPreCellsType)
            //{
            //    Spd_SetCellsType();
            //}

        }

        // 특정 범위를 Merge 타입으로 지정하는것.
        public static void Spd_SetMerge(FarPoint.Win.Spread.FpSpread spdCtl, int ColumnPos, int ColumnSize)
        {
            spdCtl.ActiveSheet.Columns[ColumnPos].MergePolicy = FarPoint.Win.Spread.Model.MergePolicy.Always;
            for (int i = ColumnPos + 1; i < ColumnSize; i++)
                spdCtl.ActiveSheet.Columns[i].MergePolicy = FarPoint.Win.Spread.Model.MergePolicy.Restricted;
        }

        // Spread내역 Excel 출력
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
                    //MsgHelper.Instance.Show("ExcelExport", "파일을 저장했습니다.");
                }
                catch
                {
                    //MsgHelper.Instance.Show(MsgType.ErrMsg, "ExcelExport", "파일 저장에 실패했습니다.");
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

        //// 컬럼 타입이 숫자 형식인 경우 0 이면 빈 공백이 나오도록 처리함..  
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
        //        throw new Exception(" RPT_SetBlankFromZero : 컬럼 타입이 잘못되었습니다 ");
        //    }
        //}

         
    }
}
