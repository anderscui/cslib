/// from: http://www.koders.com/csharp/fid40FA882F87466F7C8A97C27DE80236DF265BD345.aspx?s=mdef%3Adataset
/// 

using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SQLite;
using System.Linq;
using System.Text;

namespace Andersc.CodeLib.Common.Data
{
    /// <summary>
    /// Helper class that makes it easier to work with the provider.
    /// </summary>
    public sealed class SQLiteHelper
    {
        // this class provides only static methods
        private SQLiteHelper()
        {
        }

        #region ExecuteNonQuery

        // TODO: Cannot use this directly: conn not open; closed; 
        /// <summary>
        /// Executes a single command against a SQLite database.  The <see cref="IDbConnection"/> is assumed to be
        /// open when the method is called and remains open after the method completes.
        /// </summary>
        /// <param name="connection"><see cref="IDbConnection"/> object to use</param>
        /// <param name="commandText">SQL command to be executed</param>
        /// <param name="commandParameters">Array of <see cref="IDataParameter"/> objects to use with the command.</param>
        /// <returns></returns>
        public static int ExecuteNonQuery(
            IDbConnection connection,
            string commandText,
            params IDataParameter[] commandParameters)
        {
            //create a command and prepare it for execution
            SQLiteCommand cmd = new SQLiteCommand();
            cmd.Connection = (SQLiteConnection)connection;
            cmd.CommandText = commandText;
            System.Diagnostics.Debug.WriteLine(commandText);
            cmd.CommandType = CommandType.Text;

            if (commandParameters != null)
                foreach (IDataParameter p in commandParameters)
                {
                    // TODO: think about datetime operations.
                    //if (p.DbType == DbType.DateTime)
                    //{
                    //    p.DbType = DbType.String;
                    //    p.Value = ((DateTime)p.Value).ToString("yyyy-MM-dd HH:mm:ss");
                    //}
                    cmd.Parameters.Add(p);
                }

            int result = cmd.ExecuteNonQuery();
            cmd.Parameters.Clear();

            return result;
        }

        /// <summary>
        /// Executes a single command against a SQLite database.  A new <see cref="IDbConnection"/> is created
        /// using the <see cref="IDbConnection.ConnectionString"/> given.
        /// </summary>
        /// <param name="connectionString"><see cref="IDbConnection.ConnectionString"/> to use</param>
        /// <param name="commandText">SQL command to be executed</param>
        /// <param name="parms">Array of <see cref="IDataParameter"/> objects to use with the command.</param>
        /// <returns></returns>
        public static int ExecuteNonQuery(
            string connectionString,
            string commandText,
            params IDataParameter[] parms)
        {
            //create & open a SqlConnection, and dispose of it after we are done.
            using (SQLiteConnection cn = new SQLiteConnection(connectionString))
            {
                cn.Open();

                //call the overload that takes a connection in place of the connection string
                return ExecuteNonQuery(cn, commandText, parms);
            }
        }
        #endregion

        #region ExecuteDataSet

        /// <summary>
        /// Executes a single SQL command and returns the first row of the resultset.  A new IDbConnection object
        /// is created, opened, and closed during this method.
        /// </summary>
        /// <param name="connectionString">Settings to be used for the connection</param>
        /// <param name="commandText">Command to execute</param>
        /// <param name="parms">Parameters to use for the command</param>
        /// <returns>DataRow containing the first row of the resultset</returns>
        public static DataRow ExecuteDataRow(string connectionString, string commandText, params IDataParameter[] parms)
        {
            DataSet ds = ExecuteDataset(connectionString, commandText, parms);
            if (ds == null) return null;
            if (ds.Tables.Count == 0) return null;
            if (ds.Tables[0].Rows.Count == 0) return null;
            return ds.Tables[0].Rows[0];
        }

        /// <summary>
        /// Executes a single SQL command and returns the resultset in a <see cref="DataSet"/>.  
        /// A new IDbConnection object is created, opened, and closed during this method.
        /// </summary>
        /// <param name="connectionString">Settings to be used for the connection</param>
        /// <param name="commandText">Command to execute</param>
        /// <returns><see cref="DataSet"/> containing the resultset</returns>
        public static DataSet ExecuteDataset(string connectionString, string commandText)
        {
            //pass through the call providing null for the set of SqlParameters
            return ExecuteDataset(connectionString, commandText, (IDataParameter[])null);
        }

        /// <summary>
        /// Executes a single SQL command and returns the resultset in a <see cref="DataSet"/>.  
        /// A new IDbConnection object is created, opened, and closed during this method.
        /// </summary>
        /// <param name="connectionString">Settings to be used for the connection</param>
        /// <param name="commandText">Command to execute</param>
        /// <param name="commandParameters">Parameters to use for the command</param>
        /// <returns><see cref="DataSet"/> containing the resultset</returns>
        public static DataSet ExecuteDataset(string connectionString, string commandText, params IDataParameter[] commandParameters)
        {
            //create & open a SqlConnection, and dispose of it after we are done.
            using (SQLiteConnection cn = new SQLiteConnection(connectionString))
            {
                cn.Open();

                //call the overload that takes a connection in place of the connection string
                return ExecuteDataset(cn, commandText, commandParameters);
            }
        }

        /// <summary>
        /// Executes a single SQL command and returns the resultset in a <see cref="DataSet"/>.  
        /// The state of the <see cref="IDbConnection"/> object remains unchanged after execution
        /// of this method.
        /// </summary>
        /// <param name="connection"><see cref="IDbConnection"/> object to use</param>
        /// <param name="commandText">Command to execute</param>
        /// <returns><see cref="DataSet"/> containing the resultset</returns>
        public static DataSet ExecuteDataset(IDbConnection connection, string commandText)
        {
            //pass through the call providing null for the set of SqlParameters
            return ExecuteDataset(connection, commandText, (IDataParameter[])null);
        }

        /// <summary>
        /// Executes a single SQL command and returns the resultset in a <see cref="DataSet"/>.  
        /// The state of the <see cref="IDbConnection"/> object remains unchanged after execution
        /// of this method.
        /// </summary>
        /// <param name="connection"><see cref="IDbConnection"/> object to use</param>
        /// <param name="commandText">Command to execute</param>
        /// <param name="commandParameters">Parameters to use for the command</param>
        /// <returns><see cref="DataSet"/> containing the resultset</returns>
        public static DataSet ExecuteDataset(IDbConnection connection, string commandText, params IDataParameter[] commandParameters)
        {
            //create a command and prepare it for execution
            SQLiteCommand cmd = new SQLiteCommand();
            cmd.Connection = (SQLiteConnection)connection;
            cmd.CommandText = commandText;
            System.Diagnostics.Debug.WriteLine(commandText);
            cmd.CommandType = CommandType.Text;

            if (commandParameters != null)
                foreach (IDataParameter p in commandParameters)
                {
                    //if (p.DbType == DbType.DateTime) {
                    //    p.DbType = DbType.String;						
                    //    p.Value = ((DateTime)p.Value).ToString("yyyy-MM-dd HH:mm:ss");
                    //}
                    cmd.Parameters.Add(p);
                }

            //create the DataAdapter & DataSet
            SQLiteDataAdapter da = new SQLiteDataAdapter(cmd);
            DataSet ds = new DataSet();

            //fill the DataSet using default values for DataTable names, etc.
            //da.FillSchema(ds, SchemaType.Source);
            da.Fill(ds);
            if (ds.Tables.Count == 0)
            {
                da.FillSchema(ds, SchemaType.Source);
            }

            // detach the IDataParameters from the command object, so they can be used again.			
            cmd.Parameters.Clear();

            //return the dataset
            return ds;
        }

        /// <summary>
        /// Updates the given table with data from the given <see cref="DataSet" />
        /// </summary>
        /// <param name="connectionString">Settings to use for the update</param>
        /// <param name="commandText">Command text to use for the update</param>
        /// <param name="ds"><see cref="DataSet"/> containing the new data to use in the update</param>
        /// <param name="tablename">Tablename in the dataset to update</param>
        public static void UpdateDataSet(string connectionString, string commandText, DataSet ds, string tablename)
        {
            SQLiteConnection cn = new SQLiteConnection(connectionString);
            cn.Open();
            SQLiteDataAdapter da = new SQLiteDataAdapter(commandText, cn);
            da.Update(ds, tablename);
            cn.Close();
        }

        #endregion

        private static string TableQueryFormat = "select * from {0}";
        public static DataSet DatasetOfTable(string connString, string tableName)
        {
            return ExecuteDataset(connString, TableQueryFormat.FormatWith(tableName));
        }

        #region ExecuteDataReader

        /// <summary>
        /// Executes a single command against a SQLite database, possibly inside an existing transaction.
        /// </summary>
        /// <param name="connection"><see cref="IDbConnection"/> object to use for the command</param>
        /// <param name="transaction"><see cref="SQLiteTransaction"/> object to use for the command</param>
        /// <param name="commandText">Command text to use</param>
        /// <param name="commandParameters">Array of <see cref="IDataParameter"/> objects to use with the command</param>
        /// <param name="ExternalConn">True if the connection should be preserved, false if not</param>
        /// <returns><see cref="IDataReader"/> object ready to read the results of the command</returns>
        private static IDataReader ExecuteReader(IDbConnection connection, SQLiteTransaction transaction, string commandText, IDataParameter[] commandParameters, bool ExternalConn)
        {
            //create a command and prepare it for execution
            SQLiteCommand cmd = new SQLiteCommand();
            cmd.Connection = (SQLiteConnection)connection;
            cmd.Transaction = transaction;
            cmd.CommandText = commandText;
            System.Diagnostics.Debug.WriteLine(commandText);
            cmd.CommandType = CommandType.Text;

            if (commandParameters != null)
                foreach (IDataParameter p in commandParameters)
                {
                    //if (p.DbType == DbType.DateTime) 
                    //{
                    //    p.DbType = DbType.String;						
                    //    p.Value = ((DateTime)p.Value).ToString("yyyy-MM-dd HH:mm:ss");
                    //}
                    cmd.Parameters.Add(p);
                }

            //create a reader
            IDataReader dr;

            // call ExecuteReader with the appropriate CommandBehavior
            if (ExternalConn)
            {
                dr = cmd.ExecuteReader();
            }
            else
            {
                dr = cmd.ExecuteReader(CommandBehavior.CloseConnection);
            }

            // detach the SqlParameters from the command object, so they can be used again.
            cmd.Parameters.Clear();

            return dr;
        }

        /// <summary>
        /// Executes a single command against a SQLite database.
        /// </summary>
        /// <param name="connectionString">Settings to use for this command</param>
        /// <param name="commandText">Command text to use</param>
        /// <returns><see cref="IDataReader"/> object ready to read the results of the command</returns>
        public static IDataReader ExecuteReader(
            string connectionString,
            string commandText)
        {
            //pass through the call providing null for the set of SqlParameters
            return ExecuteReader(
                connectionString,
                commandText,
                (IDataParameter[])null);
        }

        /// <summary>
        /// Executes a single command against a SQLite database.
        /// </summary>
        /// <param name="connectionString">Settings to use for this command</param>
        /// <param name="commandText">Command text to use</param>
        /// <param name="commandParameters">Array of <see cref="IDataParameter"/> objects to use with the command</param>
        /// <returns><see cref="IDataReader"/> object ready to read the results of the command</returns>
        public static IDataReader ExecuteReader(
            string connectionString,
            string commandText,
            params IDataParameter[] commandParameters)
        {
            //create & open a SqlConnection
            SQLiteConnection cn = new SQLiteConnection(connectionString);
            cn.Open();

            try
            {
                //call the private overload that takes an internally owned connection in place of the connection string
                return ExecuteReader(
                    cn, null, commandText, commandParameters, false);
            }
            catch
            {
                //if we fail to return the SqlDatReader, we need to close the connection ourselves
                cn.Close();
                throw;
            }
        }
        #endregion

        #region ExecuteScalar

        /// <summary>
        /// Execute a single command against a SQLite database.
        /// </summary>
        /// <param name="connectionString">Settings to use for the update</param>
        /// <param name="commandText">Command text to use for the update</param>
        /// <returns>The first column of the first row in the result set, or a null reference if the result set is empty.</returns>
        public static object ExecuteScalar(
            string connectionString,
            string commandText)
        {
            //pass through the call providing null for the set of IDataParameters
            return ExecuteScalar(
                connectionString,
                commandText,
                (IDataParameter[])null);
        }

        /// <summary>
        /// Execute a single command against a SQLite database.
        /// </summary>
        /// <param name="connectionString">Settings to use for the command</param>
        /// <param name="commandText">Command text to use for the command</param>
        /// <param name="commandParameters">Parameters to use for the command</param>
        /// <returns>The first column of the first row in the result set, or a null reference if the result set is empty.</returns>
        public static object ExecuteScalar(
            string connectionString,
            string commandText,
            params IDataParameter[] commandParameters)
        {
            //create & open a SqlConnection, and dispose of it after we are done.
            using (SQLiteConnection cn = new SQLiteConnection(connectionString))
            {
                cn.Open();

                //call the overload that takes a connection in place of the connection string
                return ExecuteScalar(cn, commandText, commandParameters);
            }
        }

        /// <summary>
        /// Execute a single command against a SQLite database.
        /// </summary>
        /// <param name="connection"><see cref="IDbConnection"/> object to use</param>
        /// <param name="commandText">Command text to use for the command</param>
        /// <returns>The first column of the first row in the result set, or a null reference if the result set is empty.</returns>
        public static object ExecuteScalar(IDbConnection connection, string commandText)
        {
            //pass through the call providing null for the set of IDataParameters
            return ExecuteScalar(connection, commandText, (IDataParameter[])null);
        }

        /// <summary>
        /// Execute a single command against a SQLite database.
        /// </summary>
        /// <param name="connection"><see cref="IDbConnection"/> object to use</param>
        /// <param name="commandText">Command text to use for the command</param>
        /// <param name="commandParameters">Parameters to use for the command</param>
        /// <returns>The first column of the first row in the result set, or a null reference if the result set is empty.</returns>
        public static object ExecuteScalar(IDbConnection connection, string commandText, params IDataParameter[] commandParameters)
        {
            //create a command and prepare it for execution
            SQLiteCommand cmd = new SQLiteCommand();
            cmd.Connection = (SQLiteConnection)connection;
            cmd.CommandText = commandText;
            System.Diagnostics.Debug.WriteLine(commandText);
            cmd.CommandType = CommandType.Text;

            if (commandParameters != null)
                foreach (IDataParameter p in commandParameters)
                {
                    cmd.Parameters.Add(p);
                }

            //execute the command & return the results
            object retval = cmd.ExecuteScalar();

            // detach the SqlParameters from the command object, so they can be used again.
            cmd.Parameters.Clear();
            return retval;
        }

        #endregion


    }
}
