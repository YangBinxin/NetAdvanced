using Microsoft.Extensions.Configuration;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;

namespace DbCommon
{
    public class DbHelper
    {
        //加载appsetting.json
        static IConfiguration configuration = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json").Build();

        static string conner = configuration["DBSetting:ConnectString"]; //ConnectionStrings配置文件读取
        private static SqlConnection con = new SqlConnection(conner);

        #region 基础方法

        /// <summary>
        /// 执行增删改操作
        /// </summary>
        /// <param name="sql"></param>
        /// <returns></returns>
        public static int ExexuteCommand(string sql)
        {
            try
            {
                con.Open();
                SqlCommand cmd = new SqlCommand(sql, con);
                return cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                con.Close();
            }
        }

        /// <summary>
        /// 执行增删改操作
        /// </summary>
        /// <param name="sql"></param>
        /// <returns></returns>
        public static int ExexuteCommand(string sql, params SqlParameter[] para)
        {
            try
            {
                con.Open();
                SqlCommand cmd = new SqlCommand(sql, con);
                //将参数添加到参数集合中
                cmd.Parameters.AddRange(para);
                return cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                con.Close();
            }
        }

        /// <summary>
        /// 返回dataReader的查询方法
        /// </summary>
        /// <param name="sql"></param>
        /// <returns></returns>
        public static SqlDataReader GetReader(string sql)
        {
            try
            {
                con.Open();
                SqlCommand cmd = new SqlCommand(sql, con);
                SqlDataReader reader = cmd.ExecuteReader(CommandBehavior.CloseConnection);
                return reader;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// 返回dataReader的查询方法
        /// </summary>
        /// <param name="sql"></param>
        /// <returns></returns>
        public static SqlDataReader GetReader(string sql, params SqlParameter[] para)
        {
            try
            {
                //if (con.State == ConnectionState.Closed)
                //{
                con.Open();
                //}
                SqlCommand cmd = new SqlCommand(sql, con);
                cmd.Parameters.AddRange(para);
                SqlDataReader reader = cmd.ExecuteReader(CommandBehavior.CloseConnection);
                return reader;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// 返回dataTable的查询方法
        /// </summary>
        /// <param name="sql"></param>
        /// <returns></returns>
        public static DataTable GetDateTable(string sql)
        {
            try
            {
                con.Open();
                SqlDataAdapter myAdapter = new SqlDataAdapter(sql, con);
                DataSet ds = new DataSet();
                myAdapter.Fill(ds);
                return ds.Tables[0];
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                con.Close();
            }
        }

        /// <summary>
        /// 返回dataTable的查询方法
        /// </summary>
        /// <param name="sql"></param>
        /// <returns></returns>
        public static DataTable GetDateTable(string sql, params SqlParameter[] para)
        {
            try
            {
                con.Open();
                //SqlDataAdapter myAdapter = new SqlDataAdapter(sql, con);
                SqlCommand cmd = new SqlCommand(sql, con);
                cmd.Parameters.AddRange(para);
                SqlDataAdapter myAdapter = new SqlDataAdapter(cmd);
                DataSet ds = new DataSet();
                myAdapter.Fill(ds);
                return ds.Tables[0];
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                con.Close();
            }
        }

        /// <summary>
        /// 返回单值的查询方法
        /// </summary>
        /// <param name="sql"></param>
        /// <returns></returns>
        public static object GetScalar(string sql)
        {
            try
            {
                con.Open();
                SqlCommand cmd = new SqlCommand(sql, con);
                return cmd.ExecuteScalar();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                con.Close();
            }
        }
        /// <summary>
        /// 返回单值的查询方法（有参数的查询语句）
        /// </summary>
        /// <param name="sql"></param>
        /// <returns></returns>
        public static object GetScalar(string sql, params SqlParameter[] para)
        {
            try
            {
                con.Open();
                SqlCommand cmd = new SqlCommand(sql, con);
                cmd.Parameters.AddRange(para);
                return cmd.ExecuteScalar();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                con.Close();
            }
        }

        #endregion

        #region 存储过程调用方法
        /// <summary>
        /// 调用执行增删改的有参数的存储过程
        /// </summary>
        /// <param name="name"></param>
        /// <param name="values"></param>
        /// <returns></returns>
        public static int ExectueCommandStoredProcedure(string name, params SqlParameter[] values)
        {
            //SqlConnection conn = new SqlConnection(connection);

            try
            {
                //connection.Open();
                SqlCommand comm = new SqlCommand()
                {
                    Connection = con,
                    CommandType = CommandType.StoredProcedure,
                    CommandText = name
                };
                comm.Parameters.AddRange(values);
                return comm.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                con.Close();
            }
        }
        /// <summary>
        /// 调用无参的存储过程的方法
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public static int ExectueCommandStoredProcedure(string name)
        {

            try
            {
                //connection.Open();
                SqlCommand comm = new SqlCommand();
                comm.Connection = con;
                comm.CommandType = CommandType.StoredProcedure;
                comm.CommandText = name;
                //comm.Parameters.AddRange(values);
                return comm.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                con.Close();
            }
        }
        /// <summary>
        /// 返回DataTable型的存储过程的调用方法
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public static DataSet GetTableByStoredProcedure(string name)
        {

            //SqlConnection conn = new SqlConnection(connection.ConnectionString);
            try
            {
                if (con.State == ConnectionState.Closed)
                {
                    con.Open();
                }
                DataSet ds = new DataSet();
                SqlCommand comm = new SqlCommand();
                comm.Connection = con;
                comm.CommandType = CommandType.StoredProcedure;
                comm.CommandText = name;
                SqlDataAdapter da = new SqlDataAdapter(comm);
                ds.Clear();
                da.Fill(ds);
                return ds;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                con.Close();
            }
        }
        /// <summary>
        /// 返回DataTable型的存储过程的调用方法(含参)
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public static DataSet GetTableByStoredProcedure(string name, params SqlParameter[] valuse)
        {
            //SqlConnection conn = new SqlConnection(connection.ConnectionString);
            try
            {
                if (con.State == ConnectionState.Closed)
                {
                    con.Open();

                }
                //conn.Open();
                DataSet ds = new DataSet();
                SqlCommand comm = new SqlCommand();
                comm.Connection = con;
                comm.CommandType = CommandType.StoredProcedure;
                comm.CommandText = name;
                comm.Parameters.AddRange(valuse);
                SqlDataAdapter da = new SqlDataAdapter(comm);
                da.Fill(ds);
                return ds;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                con.Close();
            }
        }
        /// <summary>
        /// 返回reader型的无参的调用存储过程的方法
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public static SqlDataReader GetReaderByStoredProcedure(string name)
        {
            try
            {

                SqlCommand comm = new SqlCommand();
                comm.Connection = con;
                comm.CommandType = CommandType.StoredProcedure;
                comm.CommandText = name;


                SqlDataReader reader = comm.ExecuteReader(CommandBehavior.CloseConnection);
                return reader;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// <summary>
        /// 返回reader型的（含参）的调用存储过程的方法
        /// </summary>
        /// <param name="name"></param>
        /// <param name="values"></param>
        /// <returns></returns>
        public static SqlDataReader GetReaderByStoredProcedure(string name, params SqlParameter[] values)
        {
            try
            {
                con.Open();
                SqlCommand comm = new SqlCommand();
                comm.Connection = con;
                comm.CommandType = CommandType.StoredProcedure;
                comm.CommandText = name;
                comm.Parameters.AddRange(values);


                SqlDataReader reader = comm.ExecuteReader(CommandBehavior.CloseConnection);
                return reader;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// <summary>
        /// 返回单值类型（无参）
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public static Object GetScalarByStoredProcedure(string name)
        {
            try
            {
                con.Open();
                SqlCommand comm = new SqlCommand();
                comm.Connection = con;
                comm.CommandType = CommandType.StoredProcedure;
                comm.CommandText = name;

                return comm.ExecuteScalar();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                con.Close();
            }
        }
        /// <summary>
        /// 返回单值类型（含参）
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public static Object GetScalarByStoredProcedure(string name, params SqlParameter[] values)
        {
            try
            {
                con.Open();
                SqlCommand comm = new SqlCommand();
                comm.Connection = con;
                comm.CommandType = CommandType.StoredProcedure;
                comm.CommandText = name;
                comm.Parameters.AddRange(values);

                return comm.ExecuteScalar();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                con.Close();
            }
        }
        #endregion

        #region 执行事务

        /// <summary>
        /// 执行多条SQL语句，实现数据库事务。
        /// </summary>
        /// <param name="SQLStringList">多条SQL语句</param>       
        /// <returns>影响的记录数</returns>
        public static int ExecuteSqlTran(List<String> SQLStringList)
        {
            using (SqlConnection conn = new SqlConnection(conner))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = conn;
                SqlTransaction tx = conn.BeginTransaction();
                cmd.Transaction = tx;
                try
                {
                    int count = 0;
                    for (int n = 0; n < SQLStringList.Count; n++)
                    {
                        string strsql = SQLStringList[n];
                        if (strsql.Trim().Length > 1)
                        {
                            cmd.CommandText = strsql;
                            count += cmd.ExecuteNonQuery();
                        }
                    }
                    tx.Commit();
                    return count;
                }
                catch
                {
                    tx.Rollback();
                    return 0;
                }
            }
        }

        /// <summary>
        /// 执行多条SQL语句，实现数据库事务。
        /// </summary>
        /// <param name="SQLStringList">SQL语句的哈希表（key为sql语句，value是该语句的SqlParameter[]）</param>
        public static void ExecuteSqlTran(Hashtable SQLStringList)
        {
            using (SqlConnection conn = new SqlConnection(conner))
            {
                conn.Open();
                using (SqlTransaction trans = conn.BeginTransaction())
                {
                    SqlCommand cmd = new SqlCommand();
                    try
                    {
                        //循环
                        foreach (DictionaryEntry myDE in SQLStringList)
                        {
                            string cmdText = myDE.Key.ToString();
                            SqlParameter[] cmdParms = (SqlParameter[])myDE.Value;
                            PrepareCommand(cmd, conn, trans, cmdText, cmdParms);
                            int val = cmd.ExecuteNonQuery();
                            cmd.Parameters.Clear();
                        }
                        trans.Commit();
                    }
                    catch
                    {
                        trans.Rollback();
                        throw;
                    }
                }
            }
        }

        /// <summary>
        /// 执行多条SQL语句，实现数据库事务。
        /// </summary>
        /// <param name="SQLStringList">SQL语句的哈希表（key为sql语句，value是该语句的SqlParameter[]）</param>
        public static void ExecuteSqlTranWithIndentity(Hashtable SQLStringList)
        {
            using (SqlConnection conn = new SqlConnection(conner))
            {
                conn.Open();
                using (SqlTransaction trans = conn.BeginTransaction())
                {
                    SqlCommand cmd = new SqlCommand();
                    try
                    {
                        int indentity = 0;
                        //循环
                        foreach (DictionaryEntry myDE in SQLStringList)
                        {
                            string cmdText = myDE.Key.ToString();
                            SqlParameter[] cmdParms = (SqlParameter[])myDE.Value;
                            foreach (SqlParameter q in cmdParms)
                            {
                                if (q.Direction == ParameterDirection.InputOutput)
                                {
                                    q.Value = indentity;
                                }
                            }
                            PrepareCommand(cmd, conn, trans, cmdText, cmdParms);
                            int val = cmd.ExecuteNonQuery();
                            foreach (SqlParameter q in cmdParms)
                            {
                                if (q.Direction == ParameterDirection.Output)
                                {
                                    indentity = Convert.ToInt32(q.Value);
                                }
                            }
                            cmd.Parameters.Clear();
                        }
                        trans.Commit();
                    }
                    catch
                    {
                        trans.Rollback();
                        throw;
                    }
                }
            }
        }

        private static void PrepareCommand(SqlCommand cmd, SqlConnection conn, SqlTransaction trans, string cmdText, SqlParameter[] cmdParms)
        {
            if (conn.State != ConnectionState.Open)
                conn.Open();
            cmd.Connection = conn;
            cmd.CommandText = cmdText;
            if (trans != null)
                cmd.Transaction = trans;
            cmd.CommandType = CommandType.Text;//cmdType;
            if (cmdParms != null)
            {
                foreach (SqlParameter parameter in cmdParms)
                {
                    if ((parameter.Direction == ParameterDirection.InputOutput || parameter.Direction == ParameterDirection.Input) &&
                        (parameter.Value == null))
                    {
                        parameter.Value = DBNull.Value;
                    }
                    cmd.Parameters.Add(parameter);
                }
            }
        }


        #endregion
    }
}
