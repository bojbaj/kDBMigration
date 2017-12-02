using Dapper;
using Dapper.Contrib.Extensions;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.OleDb;
using System.Data.SqlClient;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using ADOX;

namespace SQLSVN
{
    public class dbFactory
    {
        private const string TABLE_NAME = "tSQLSVN_Version";
        private readonly string connectionString = string.Empty;
        private readonly IDbConnection connection;
        private IDbTransaction transaction = null;
        public dbFactory() : this("DefaultConnection") { }
        public dbFactory(string connName)
        {
            connectionString = ConfigurationManager.ConnectionStrings[connName].ConnectionString;
            connection = new SqlConnection(connectionString);
        }
        public dbFactory(IDbConnection _connection)
        {
            connection = _connection;
        }
        public int Execute(string sql)
        {
            return connection.Execute(sql, transaction: transaction);
        }

        public T Get<T>(object Id) where T : class
        {
            return connection.Get<T>(Id, transaction: transaction);
        }
        public IEnumerable<T> GetAll<T>() where T : class
        {
            return connection.GetAll<T>(transaction: transaction);
        }
        public long Insert<T>(T model) where T : class
        {
            return connection.Insert(model, transaction: transaction);

        }
        public bool Update<T>(T model) where T : class
        {
            return connection.Update(model, transaction: transaction);
        }
        public IDbTransaction beginTrans()
        {
            if (transaction == null)
                transaction = connection.BeginTransaction();

            return transaction;
        }
        public void commitTrans()
        {
            transaction.Commit();
            transaction = null;
        }
        public void rollbackTrans()
        {
            transaction.Rollback();
            transaction = null;
        }
        public void connOpen()
        {
            if (connection.State != ConnectionState.Open)
                connection.Open();
        }
        public void connClose()
        {
            if (connection.State != ConnectionState.Closed)
                connection.Close();
        }
        private string initialDb()
        {
            string sql = string.Empty;
            sql += "IF NOT EXISTS ( SELECT * FROM sysobjects WHERE name = '{0}' and xtype = 'U' )\n";
            sql += "BEGIN\n";
            sql += "CREATE TABLE [dbo].{0}(Code uniqueidentifier NOT NULL, verNumber INT NOT NULL)\n";
            sql += "END\n";
            sql = string.Format(sql, TABLE_NAME);
            return sql;
        }
        public void initialDb(Guid projectCode)
        {
            string sql = string.Empty;            
            sql += initialDb();
            sql += "IF NOT EXISTS ( SELECT * FROM {0} WHERE Code = '{1}' )\n";
            sql += "BEGIN\n";
            sql += "INSERT INTO {0}(Code, verNumber) VALUES('{1}', 0)\n";
            sql += "END\n";
            sql = string.Format(sql, TABLE_NAME, projectCode);
            Execute(sql);
        }

        public bool exportFile(List<string> sqlScripts, string filePath)
        {
            bool result = false;
            if (System.IO.File.Exists(filePath))
                System.IO.File.Delete(filePath);

            string createTableScript = string.Empty;
            createTableScript += "CREATE TABLE [tScript](\n";
            createTableScript += "[scriptId] INT NOT NULL,\n";
            createTableScript += "[sql] MEMO NOT NULL,\n";
            createTableScript += "[tProjectCode] guid NOT NULL\n";
            createTableScript += ");\n";
            Catalog cat = new Catalog();
            try
            {
                using (OleDbConnection con = dbFactoryStatic.connectionMaker(filePath))
                {
                    cat.Create(con.ConnectionString);
                    con.Open();
                    OleDbCommand cmd = new OleDbCommand(createTableScript, con);
                    cmd.ExecuteNonQuery();
                    foreach (string query in sqlScripts)
                    {
                        cmd.CommandText = query;
                        cmd.ExecuteNonQuery();
                    }
                    con.Close();
                    result = true;
                }
            }
            catch (Exception ex)
            {
                result = false;
            }
            if (cat.ActiveConnection != null)
                cat.ActiveConnection.Close();
            cat = null;
            return result;
        }
    }
    public static class dbFactoryStatic
    {
        public static SqlConnection connectionMaker(string Server, byte AuthType, string UserId, string Password, string DbName)
        {
            return new SqlConnection(string.Format("Data Source={0};Initial Catalog={1};user id={2};password={3};Integrated security={4};Pooling=True;MultipleActiveResultSets=true;", Server, DbName, UserId, Password, (AuthType == 1)));
        }
        public static OleDbConnection connectionMaker(string filePath)
        {
            return new OleDbConnection(string.Format(@"Provider=Microsoft.Jet.OLEDB.4.0;Data Source={0}", filePath));
        }
        public static string escapeQuery(string query)
        {
            return query.Replace("'", "''");
        }
    }
}
