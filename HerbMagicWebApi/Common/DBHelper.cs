using Dapper;
using HerbMagicWebApi.Controllers;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Reflection;
using System.Web;

namespace HerbMagicWebApi.Common
{

    /// <summary>
    /// ADO.NET 新增 刪除 修改之法 並且增加SP方法
    /// </summary>
    public class DBHelper
    {
        public static DataSet Search(string connect, string qurey)
        {
            using (SqlConnection connection = new SqlConnection(connect))
            {
                DataSet tempDataSet = new DataSet();

                try
                {
                    connection.Open();
                    SqlDataAdapter custAdapter = new SqlDataAdapter(qurey, connection);
                    custAdapter.Fill(tempDataSet, "tempTable");

                }
                catch (Exception ex)
                {
                }
                return tempDataSet;
            }

        }
        /// <summary>
        /// 新增
        /// </summary>
        /// <param name="connect">連線</param>
        /// <param name="qurey">新增指令</param>
        /// <returns></returns>
        public static int Insert(string connect, string qurey)
        {
            return InUpDe(connect, qurey);

        }
        /// <summary>
        /// 更新
        /// </summary>
        /// <param name="connect">連線</param>
        /// <param name="qurey">更新指令</param>
        /// <returns></returns>
        public static int Update(string connect, string qurey)
        {
            return InUpDe(connect, qurey);

        }
        /// <summary>
        /// 刪除
        /// </summary>
        /// <param name="connect">連線</param>
        /// <param name="qurey">刪除指令</param>
        /// <returns></returns>
        public static int Delete(string connect, string qurey)
        {
            return InUpDe(connect, qurey);

        }
        private static int InUpDe(string connect, string qurey)
        {
            return Convert.ToInt32(MainSp(connect, qurey, new List<SqlParameter>(), CommandType.Text, false));

            //using (SqlConnection connection = new SqlConnection(connect))
            //{
            //    int r = 0;
            //    try
            //    {
            //        using (SqlCommand command = new SqlCommand(qurey, connection))
            //        {
            //            command.CommandType = CommandType.Text;

            //            connection.Open();
            //            r = command.ExecuteNonQuery();
            //            connection.Close();

            //        }
            //    }
            //    catch (Exception ex)
            //    {
            //    }
            //    return r;
            //}
        }
        /// <summary>
        /// 資料轉換
        /// </summary>
        /// <typeparam name="T">給予不同回答型態</typeparam>
        /// <param name="pRow">資料row</param>
        /// <param name="v">欄位名稱</param>
        /// <returns></returns>

        public static T GetValue<T>(DataRow pRow, string v)
        {
            var q = default(T);
            try
            {
                q = pRow[v] == DBNull.Value ? default(T) : (T)pRow[v];
            }
            catch (Exception ex)
            {

            }
            return q;
        }
        /// <summary>
        /// Store Procedure 執行 不回傳
        /// </summary>
        /// <param name="connect">連線</param>
        /// <param name="spName">SP 名稱</param>
        /// <param name="lsp">SP 參數設定</param>
        public static void ExecSpNoneReturn(string connect, string spName, List<SqlParameter> lsp)
        {
            MainSp(connect, spName, lsp, CommandType.StoredProcedure, false);
        }
        /// <summary>
        /// Store Procedure 執行 回傳影響數據的量
        /// </summary>
        /// <param name="connect">連線</param>
        /// <param name="spName">SP 名稱</param>
        /// <param name="lsp">SP 參數設定</param>
        public static int ExecSp(string connect, string spName, List<SqlParameter> lsp)
        {
            return Convert.ToInt32(MainSp(connect, spName, lsp, CommandType.StoredProcedure, true));
        }
        /// <summary>
        /// Store Procedure 執行 取得回傳DATA
        /// </summary>
        /// <param name="connect">連線</param>
        /// <param name="spName">SP 名稱</param>
        /// <param name="lsp">SP 參數設定</param>
        public static object ExecSpGetOutput(string connect, string spName, List<SqlParameter> lsp)
        {
            SqlParameter sp = new SqlParameter();
            sp.ParameterName = "@OutputData";
            sp.SqlDbType = SqlDbType.VarChar;
            sp.Size = 250;
            lsp.Add(sp);
            return MainSp(connect, spName, lsp, CommandType.StoredProcedure, true);

        }
        /// <summary>
        /// Store Procedure 執行 回傳值
        /// </summary>
        /// <param name="connect">連線</param>
        /// <param name="spName">SP 名稱</param>
        /// <param name="lsp">SP 參數設定</param>
        public static object ExecSpGetReturn(string connect, string spName, List<SqlParameter> lsp)
        {
            SqlParameter sp = new SqlParameter();
            sp.ParameterName = "@RETURN_VALUE";
            sp.SqlDbType = SqlDbType.VarChar;
            sp.Size = 250;
            lsp.Add(sp);
            return MainSp(connect, spName, lsp, CommandType.StoredProcedure, true);

        }

        /// <summary>
        /// 核心SP方法
        /// </summary>
        /// <param name="connect">連線</param>
        /// <param name="query">名稱</param>
        /// <param name="lsp">SP參數</param>
        /// <param name="commandType">內容參數</param>
        /// <param name="needReturn">是否回傳</param>
        /// <returns></returns>
        private static object MainSp(string connect, string query, List<SqlParameter> lsp, CommandType commandType, bool needReturn = true)
        {
            using (SqlConnection connection = new SqlConnection(connect))
            {
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.CommandType = commandType;
                    try
                    {
                        connection.Open();
                        switch (command.CommandType)
                        {
                            case CommandType.StoredProcedure:
                                foreach (var sp in lsp)
                                {
                                    command.Parameters.Add(sp.ParameterName, sp.SqlDbType, sp.Size);
                                    command.Parameters[sp.ParameterName].Value = sp.Value;
                                }

                                SqlParameter retValParam;
                                if (needReturn)
                                {
                                    if (lsp.Any(x => x.ParameterName.ToUpper() == "@RETURN_VALUE"))
                                    {
                                        retValParam = command.Parameters.Add("@RETURN_VALUE", SqlDbType.VarChar, 250);
                                        retValParam.Direction = ParameterDirection.ReturnValue;

                                        command.ExecuteNonQuery();
                                        return retValParam.Value;
                                    }
                                    else if (lsp.Any(x => x.ParameterName.ToUpper() == "@OUTPUTDATA"))
                                    {
                                        retValParam = command.Parameters.Add("@OutputData", SqlDbType.VarChar, 250);
                                        retValParam.Direction = ParameterDirection.Output;

                                        return retValParam.Value;
                                    }
                                    else
                                    {
                                        return command.ExecuteScalar();
                                    }
                                }
                                else
                                {

                                    return command.ExecuteNonQuery();
                                }
                            case CommandType.Text:
                                return command.ExecuteNonQuery();

                            default:
                                return new object();
                        }
                    }
                    catch (Exception ex)
                    {
                        throw ex.GetBaseException();
                    }
                    finally
                    {
                        connection.Close();
                    }
                }
            }

        }

    }


    public class DapperHelper
    {
        public static IEnumerable<T> Search<T>(string connect, string query)
        {
           
            using (SqlConnection conn = new SqlConnection(connect))
            {
                try {

                    return conn.Query<T>(query);
                }
                catch (Exception ex) {
                    return default(IEnumerable<T>);

                }
                }

        }
        public static object InsertSQL<T>(string connect, string Table, T objects)
        {
            var query = "INSERT INTO " + Table + " ( ";
            var data = string.Empty;
            var Target = "SeqNo";
            foreach (PropertyInfo pInfo in typeof(T).GetProperties())
            {
                if (pInfo.Name != Target)
                {
                    query += pInfo.Name + " , ";
                    if (pInfo.PropertyType == typeof(DateTime))
                    {
                        data += $" N'{((DateTime)pInfo.GetValue(objects, null)).ToString("yyyy/MM/dd HH:mm:ss")}' , ";
                    }
                    else
                    {

                        data += $" N'{ pInfo.GetValue(objects, null)}' ,";
                    }
                }
            }

            query = query.Substring(0, query.Length - 2);
            query += " ) OUTPUT INSERTED." + Target + " VALUES ( " + data;
            query = query.Substring(0, query.Length - 2) + " )";

            return ExecuteSQL(connect, query);
        }
        public static object InsertSQLNormal<T>(string connect, string Table, T objects)
        {
            var query = "INSERT INTO " + Table + " ( ";
            var data = string.Empty;
            foreach (PropertyInfo pInfo in typeof(T).GetProperties())
            {
                query += pInfo.Name + " , ";
                    if (pInfo.PropertyType == typeof(DateTime))
                    {
                        data += $" N'{((DateTime)pInfo.GetValue(objects, null)).ToString("yyyy/MM/dd HH:mm:ss")}' , ";
                    }
                    else
                    {

                        data += $" N'{ pInfo.GetValue(objects, null)}' ,";
                    }
                
            }

            query = query.Substring(0, query.Length - 2);
            query += " ) "+" VALUES ( " + data;
            query = query.Substring(0, query.Length - 2) + " )";

            return ExecuteSQL(connect, query);
        }

        public static object UpdateSQL<T>(string connect, string Table, T objects)
        {
            var query = "UPDATE  " + Table + " SET ";
            var seqno = Int32.MinValue;
            foreach (PropertyInfo pInfo in typeof(T).GetProperties())
            {
                if (pInfo.Name != "SeqNo")
                {
                    query += pInfo.Name +" = ";
                    if (pInfo.PropertyType == typeof(DateTime))
                    {
                        query += $" N'{((DateTime)pInfo.GetValue(objects, null)).ToString("yyyy/MM/dd HH:mm:ss")}' , ";
                    }
                    else
                    {

                        query += $" N'{ pInfo.GetValue(objects, null)}' ,";
                    }

                }
                else {
                    seqno= (int)pInfo.GetValue(objects, null);
            }
            }
            query = query.Substring(0, query.Length - 2);
            query += " WHERE SeqNo=" + seqno;

            return ExecuteSQL(connect, query);
        }
        public static object DeleteSQL(string connect, string Table,int id)
        {
            var query = "DELETE " + Table + " Where SeqNo="+ id;
            return ExecuteSQL(connect, query);
        }

        public static object ExecuteSQL(string connect, string query)
        {
            using (SqlConnection conn = new SqlConnection(connect))
            {
                return conn.ExecuteScalar(query);
            }
        }


        public static int SearchBySP(string connect, string query, List<SqlParameter> lsp)
        {
            using (SqlConnection conn = new SqlConnection(connect))
            {
                DynamicParameters parameters = new DynamicParameters();
                foreach (var sp in lsp)
                {
                    parameters.Add(sp.ParameterName, sp.Value, sp.DbType, ParameterDirection.Input);

                }

                return conn.Execute(query, parameters, commandType: CommandType.StoredProcedure);
            }
        }
    }
}