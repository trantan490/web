using System;
using System.Data;
using System.Text;

using Miracom.Query;
using Miracom.SmartWeb.FWX;
using System.Collections;

namespace Miracom.SmartWeb.RO
{
    public partial class StandardFunction
	{

        // CheckNumeric()
        //       - Check Numeric Data
        // Return Value
        //       - boolean : True / False
        // Arguments
        //       - string
        public static bool CheckNumeric(object val)
        {
            double result;
            object obj;

            try
            {
                if (val == null) return false;

                if (val is Char)
                {
                    obj = Convert.ToString(val);
                }
                else
                {
                    obj = val;
                }

                result = Convert.ToDouble(obj);
                return true;
            }

            catch
            {
                return false;
            }
        }

        public static String ToDate(string str)
        {
            try
            {
                string sTime = str.Trim();
                DateTime DTime;

                int year;
                int month;
                int day;
                int hour;
                int minute;
                int second;

                year = 0;
                month = 0;
                day = 0;
                hour = 0;
                minute = 0;
                second = 0;

                if (str.Trim() == "") return "";

                if (CheckNumeric(sTime) == true)
                {
                    if (sTime.Length >= 8)
                    {
                        year = Convert.ToInt32(sTime.Substring(0, 4));
                        month = Convert.ToInt32(sTime.Substring(4, 2));
                        day = Convert.ToInt32(sTime.Substring(6, 2));
                    }
                    if (sTime.Length >= 14)
                    {
                        hour = Convert.ToInt32(sTime.Substring(8, 2));
                        minute = Convert.ToInt32(sTime.Substring(10, 2));
                        second = Convert.ToInt32(sTime.Substring(12, 2));
                    }

                    DTime = new DateTime(year, month, day, hour, minute, second);
                }
                else
                {
                    DTime = DateTime.Now;
                }

                if (sTime.Length >= 14)
                {
                    return DTime.ToLocalTime().ToString();
                }
                else if (sTime.Length == 8)
                {
                    return DTime.ToShortDateString();
                }
                else
                {
                    return DateTime.Now.ToLocalTime().ToString();
                }
            }
            catch (Exception ex)
            {
                return DateTime.Now.ToString();
            }
        }

		/// <summary>
		/// 현재의 시스템의 날짜/시간(YYYYMMDDHHMISS)
		/// </summary>
		/// <returns>string, Otherwise Exception pass the throw </returns>
		public string GetSysDate()
		{
			string sysDateTime = "";
			DataTable adoDataTable = new DataTable();

			try
			{
                StdGlobalVariable.DBQuery.InitQueryComponent(StdGlobalVariable.QueryPath + @"\" + StdGlobalVariable.MP_COMMON_XML_FILE);

                adoDataTable = StdGlobalVariable.DBQuery.GetFuncDataTable("SEL_SYSDATE", null, null);
				// Check Data Not Found
				if (adoDataTable.Rows.Count < 1)
				{
					sysDateTime = "";
				}
				else
				{
					sysDateTime = adoDataTable.Rows[0][0].ToString();
				}
				return sysDateTime;
			}
			catch (Exception ex)
			{
				throw ex;
			}
			finally
			{
				adoDataTable.Dispose();
			}
		}

        public static void InitErrorMessage()
        {
            StdGlobalVariable.gsResult = null;
            StdGlobalVariable.gsErrorCode = null;
            StdGlobalVariable.gsErrorMessage = null;
        }

        public static string GetErrorMessage()
        {
            
            try
            {
                StringBuilder sbErrorMessage = new StringBuilder();
                if (StdGlobalVariable.gsResult == null)
                {
                    sbErrorMessage.Append(' ');
                }
                else
                {
                    sbErrorMessage.Append(StdGlobalVariable.gsResult.Substring(0,1));
                }

                if (StdGlobalVariable.gsErrorCode == null)
                {
                    sbErrorMessage.Append(' ',10);
                }
                else if (StdGlobalVariable.gsErrorCode.Length > 10)
                {
                    sbErrorMessage.Append(StdGlobalVariable.gsErrorCode.Substring(0, 10));
                }
                else
                {
                    sbErrorMessage.Append(StdGlobalVariable.gsErrorCode);
                    sbErrorMessage.Append(' ', 10 - StdGlobalVariable.gsErrorCode.Length);
                }

                if (StdGlobalVariable.gsErrorMessage == null)
                {
                    sbErrorMessage.Append(' ',200);
                }
                else if (StdGlobalVariable.gsErrorMessage.Length > 200)
                {
                    sbErrorMessage.Append(StdGlobalVariable.gsErrorMessage.Substring(0,200));
                }
                else
                {
                    sbErrorMessage.Append(StdGlobalVariable.gsErrorMessage);
                    sbErrorMessage.Append(' ', 200 - StdGlobalVariable.gsErrorMessage.Length);
                }

                return sbErrorMessage.ToString();

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

		/// <summary>
		/// 공장의 사용자 그룹별 함수리스트
		/// </summary>
		/// <param name="Factory">공장코드</param>
		/// <param name="GroupID">사용자 그룹코드</param>
		/// <returns>DataTable, Otherwise Exception pass the throw </returns>
        public DataTable GetFunctionList(ArrayList QueryCond)
		{
			DataTable adoDataTable = null;
            string[] CondList = null;

            CondList = (String[])QueryCond.ToArray(typeof(string));

            //if (QueryCond != null)
            //{
            //    CondList = FwxCmnFunction.UnPackCondition(QueryCond);
            //}
            //else
            //{
            //    CondList = null;
            //}

            string Factory = CondList[0];
            string GroupID = CondList[1];

			try
			{
                
                StdGlobalVariable.DBQuery.InitQueryComponent(StdGlobalVariable.QueryPath + @"\" + StdGlobalVariable.MP_COMMON_XML_FILE);

				if (Factory == null || Factory.TrimEnd().Length <= 0)
				{
					throw new ArgumentOutOfRangeException("Invalid Query 1st arguments null or zero length.");
				}

				if (GroupID == null || GroupID.TrimEnd().Length <= 0)
				{
					throw new ArgumentOutOfRangeException("Invalid Query 2nd arguments null or zero length.");
				}

                adoDataTable = StdGlobalVariable.DBQuery.GetFuncDataTable("GET_FUNCTION_LIST", null, CondList);

                return adoDataTable;
			}
			catch (Exception ex)
			{
				throw ex;
			}
			finally
			{
				if (adoDataTable != null) adoDataTable.Dispose();
			}
		}

        /// <summary>
        /// 공장의 Material 리스트
        /// </summary>
        /// <param name="Factory">공장코드</param>
        /// <param name="GroupID">Material ID 필터</param>
        /// <returns>DataTable, Otherwise Exception pass the throw </returns>
        public DataTable ViewMaterialList(ArrayList QueryCond)
        {
            DataTable adoDataTable = null;
            string DynamicQuery = "";
            string[] CondList = null;

            CondList = (String[])QueryCond.ToArray(typeof(string));
            string Factory = CondList[0];
            string MatID = CondList[1];
            try
            {
                StdGlobalVariable.DBQuery.InitQueryComponent(StdGlobalVariable.QueryPath + @"\" + StdGlobalVariable.MP_COMMON_XML_FILE);

                if (Factory == null || Factory.TrimEnd().TrimEnd().Length <= 0)
                {                    
                }
                else
                {
                    DynamicQuery = "WHERE FACTORY = '" + Factory + "'";
                }

                if (MatID == null || MatID.TrimEnd().Length <= 0)
                {
                }
                else
                {
                    if (Factory == null || Factory.TrimEnd().Length <= 0)
                    {
                        DynamicQuery = "WHERE MAT_ID LIKE '" + MatID + "%'";
                    }
                    else
                    {
                        DynamicQuery += " AND MAT_ID LIKE '" + MatID + "%'";
                    }
                }

                adoDataTable = StdGlobalVariable.DBQuery.GetFuncDataTable("VIEW_MATERIAL_LIST", new string[] { DynamicQuery }, null);

                return adoDataTable;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                if (adoDataTable != null) adoDataTable.Dispose();
            }
        }

        /// <summary>
        /// Material 에 해당하는 Version 리스트
        /// </summary>
        /// <param name="Factory">공장코드</param>
        /// <param name="GroupID">Material ID</param>
        /// <returns>DataTable, Otherwise Exception pass the throw </returns>
        public DataTable ViewMaterialVerList(ArrayList TempList)
        {
            DataTable adoDataTable = null;
            string DynamicQuery = "";
            string[] CondList = null;

            CondList = (String[])TempList.ToArray(typeof(string));
            string Factory = CondList[0];
            string MatID = CondList[1];
            try
            {
                StdGlobalVariable.DBQuery.InitQueryComponent(StdGlobalVariable.QueryPath + @"\" + StdGlobalVariable.MP_COMMON_XML_FILE);

                if (Factory == null || Factory.TrimEnd().Length <= 0)
                {
                    throw new ArgumentOutOfRangeException("Invalid Query 1st arguments null or zero length.");
                }
                else
                {
                    DynamicQuery = "WHERE FACTORY = '" + Factory + "'";
                }

                if (MatID == null || MatID.TrimEnd().Length <= 0)
                {
                    throw new ArgumentOutOfRangeException("Invalid Query 1st arguments null or zero length.");
                }
                else
                {
                    DynamicQuery += " AND MAT_ID = '" + MatID + "'";
                }

                adoDataTable = StdGlobalVariable.DBQuery.GetFuncDataTable("VIEW_MATERIAL_VER_LIST", new string[] { DynamicQuery }, null);

                return adoDataTable;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                if (adoDataTable != null) adoDataTable.Dispose();
            }
        }

        public DataTable ViewMaxMaterialVer(ArrayList TempList)
        {
            DataTable adoDataTable = new DataTable();
            string[] CondList = null;

            CondList = (String[])TempList.ToArray(typeof(string));
            string Factory = CondList[0];
            string Mat_ID = CondList[1];
            try
            {
                StdGlobalVariable.DBQuery.InitQueryComponent(StdGlobalVariable.QueryPath + @"\" + StdGlobalVariable.MP_COMMON_XML_FILE);

                if (Factory == null || Factory.TrimEnd().Length <= 0)
                {
                    throw new ArgumentOutOfRangeException("Invalid Query 1st arguments null or zero length.");
                }

                if (Mat_ID == null || Mat_ID.TrimEnd().Length <= 0)
                {
                    throw new ArgumentOutOfRangeException("Invalid Query 2st arguments null or zero length.");
                }

                adoDataTable = StdGlobalVariable.DBQuery.GetFuncDataTable("VIEW_MAX_MATERIAL_VER", null, CondList);

                return adoDataTable;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                if (adoDataTable != null) adoDataTable.Dispose();
            }
        }

        /// <summary>
        /// 공장의 Flow 리스트
        /// </summary>
        /// <param name="Factory">공장코드</param>
        /// <param name="GroupID">Material ID</param>
        /// <returns>DataTable, Otherwise Exception pass the throw </returns>
        public DataTable ViewFlowList(ArrayList TempList)
        {
            DataTable adoDataTable = null;
            string DynamicQuery = "";
            string[] CondList = null;

            CondList = (String[])TempList.ToArray(typeof(string));
            string Factory = CondList[0];
            string MatID = CondList[1];
            string MatVer = CondList[2];
            try
            {
                StdGlobalVariable.DBQuery.InitQueryComponent(StdGlobalVariable.QueryPath + @"\" + StdGlobalVariable.MP_COMMON_XML_FILE);

                if (Factory == null || Factory.TrimEnd().Length <= 0)
                {
                    throw new ArgumentOutOfRangeException("Invalid Query 1st arguments null or zero length.");
                }
                else
                {
                    if (MatID == null || MatID.TrimEnd().Length <= 0)
                    {
                        DynamicQuery = "SELECT * FROM MWIPFLWDEF WHERE FACTORY = '" + Factory + "'";
                    }
                    else
                    {
                        DynamicQuery = "SELECT * FROM MWIPMATFLW WHERE FACTORY = '" + Factory + "'";
                        DynamicQuery += " AND MAT_ID = '" + MatID + "'";
                        DynamicQuery += " AND MAT_VER = '" + MatVer + "'";
                    }
                    
                }

                adoDataTable = StdGlobalVariable.DBQuery.GetFuncDataTable("DYNAMIC", new string[] { DynamicQuery }, null);

                return adoDataTable;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                if (adoDataTable != null) adoDataTable.Dispose();
            }
        }

        /// <summary>
        /// 공장의 Oper 리스트
        /// </summary>
        /// <param name="Factory">공장코드</param>
        /// <param name="GroupID">Oper</param>
        /// <returns>DataTable, Otherwise Exception pass the throw </returns>
        public DataTable ViewOperList(ArrayList QueryCond)
        {
            DataTable adoDataTable = null;
            string DynamicQuery = "";
            string[] CondList = null;

            CondList = (String[])QueryCond.ToArray(typeof(string));
            string Factory = CondList[0];
            string Flow = CondList[1];
            try
            {
                StdGlobalVariable.DBQuery.InitQueryComponent(StdGlobalVariable.QueryPath + @"\" + StdGlobalVariable.MP_COMMON_XML_FILE);

                if (Factory == null || Factory.TrimEnd().Length <= 0)
                {
                    throw new ArgumentOutOfRangeException("Invalid Query 1st arguments null or zero length.");
                }
                else
                {
                    if (Flow == null || Flow.TrimEnd().Length <= 0)
                    {
                        DynamicQuery = "SELECT * FROM MWIPOPRDEF WHERE FACTORY = '" + Factory + "'";
                        DynamicQuery += " ORDER BY OPER";
                    }
                    else
                    {
                        DynamicQuery = "SELECT * FROM MWIPFLWOPR WHERE FACTORY = '" + Factory + "'";
                        DynamicQuery += " AND FLOW = '" + Flow + "'";
                        DynamicQuery += " ORDER BY SEQ_NUM";
                    }

                }

                adoDataTable = StdGlobalVariable.DBQuery.GetFuncDataTable("DYNAMIC", new string[] { DynamicQuery }, null);

                return adoDataTable;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                if (adoDataTable != null) adoDataTable.Dispose();
            }
        }

        public DataTable ViewFactoryList(ArrayList QueryCond)
        {
            DataTable adoDataTable = null;

            try
            {
                StdGlobalVariable.DBQuery.InitQueryComponent(StdGlobalVariable.QueryPath + @"\" + StdGlobalVariable.MP_COMMON_XML_FILE);

                adoDataTable = StdGlobalVariable.DBQuery.GetFuncDataTable("VIEW_FACTORY_LIST", null, null);

                return adoDataTable;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                if (adoDataTable != null) adoDataTable.Dispose();
            }
        }

        public DataTable ViewGCMTableList(ArrayList QueryCond)
        {
            DataTable adoDataTable = null;
            string[] CondList = null;

            CondList = (String[])QueryCond.ToArray(typeof(string));
            string Factory = CondList[0];
            string Table_Name = CondList[1];
            try
            {
                StdGlobalVariable.DBQuery.InitQueryComponent(StdGlobalVariable.QueryPath + @"\" + StdGlobalVariable.MP_COMMON_XML_FILE);

                if (Factory == null || Factory.TrimEnd().Length <= 0)
                {
                    throw new ArgumentOutOfRangeException("Invalid Query 1st arguments null or zero length.");
                }

                if (Table_Name == null || Table_Name.TrimEnd().Length <= 0)
                {
                    throw new ArgumentOutOfRangeException("Invalid Query 2st arguments null or zero length.");
                }

                adoDataTable = StdGlobalVariable.DBQuery.GetFuncDataTable("VIEW_GCMDATA_LIST", null, CondList);

                return adoDataTable;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                if (adoDataTable != null) adoDataTable.Dispose();
            }
        }

        public DataTable ViewResourceList(ArrayList TempList)
        {
            DataTable adoDataTable = null;
            string[] CondList = null;

            CondList = (String[])TempList.ToArray(typeof(string));
            string Factory = CondList[0];
            try
            {
                StdGlobalVariable.DBQuery.InitQueryComponent(StdGlobalVariable.QueryPath + @"\" + StdGlobalVariable.MP_COMMON_XML_FILE);

                if (Factory == null || Factory.TrimEnd().Length <= 0)
                {
                    throw new ArgumentOutOfRangeException("Invalid Query 1st arguments null or zero length.");
                }

                adoDataTable = StdGlobalVariable.DBQuery.GetFuncDataTable("VIEW_RESOURCE_LIST", null, CondList);

                return adoDataTable;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                if (adoDataTable != null) adoDataTable.Dispose();
            }
        }

        public DataTable ViewMATGroupList(ArrayList TempList)
        {
            DataTable adoDataTable = null;
            string[] CondList = null;

            CondList = (String[])TempList.ToArray(typeof(string));
            string Factory = CondList[0];
            string Item_Name = CondList[1];
            try
            {
                StdGlobalVariable.DBQuery.InitQueryComponent(StdGlobalVariable.QueryPath + @"\" + StdGlobalVariable.MP_COMMON_XML_FILE);

                if (Factory == null || Factory.TrimEnd().Length <= 0)
                {
                    throw new ArgumentOutOfRangeException("Invalid Query 1st arguments null or zero length.");
                }

                if (Item_Name == null || Item_Name.TrimEnd().Length <= 0)
                {
                    throw new ArgumentOutOfRangeException("Invalid Query 2st arguments null or zero length.");
                }

                adoDataTable = StdGlobalVariable.DBQuery.GetFuncDataTable("VIEW_MATGROUP_LIST", null, CondList);

                // 하나의 Row의 Column을 Row단위로 나눔
                DataTable ReAdoDataTable = new DataTable();
                string Prt = "";
                int i = 0;

                for (i = 0; i < 2; i++)
                {
                    DataColumn MatGrpCol = new DataColumn();

                    MatGrpCol.ReadOnly = false;
                    MatGrpCol.Unique = false;

                    if (i == 0)
                    {
                        MatGrpCol.ColumnName = "Prompt";
                        MatGrpCol.DataType = System.Type.GetType("System.String");
                    }
                    else if (i == 1)
                    {
                        MatGrpCol.ColumnName = "Group";
                        MatGrpCol.DataType = System.Type.GetType("System.String");
                    }
                    ReAdoDataTable.Columns.Add(MatGrpCol);
                }

                if (adoDataTable.Rows.Count > 0)
                {
                    for (i = 2; i < 22; i++) //Column:PRT_1(2) ~ Column:PRT_20(21)
                    {
                        DataRow MatGrpRow = null;
                        MatGrpRow = ReAdoDataTable.NewRow();
                        Prt = Convert.ToString(adoDataTable.Rows[0].ItemArray[i]);
                        if (Prt != " " && Prt != null)
                        {
                            MatGrpRow[0] = adoDataTable.Rows[0].ItemArray[i];
                            MatGrpRow[1] = i - 1;
                            ReAdoDataTable.Rows.Add(MatGrpRow);
                        }
                    }
                }

                return ReAdoDataTable;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                if (adoDataTable != null) adoDataTable.Dispose();
            }
        }
	}
}
