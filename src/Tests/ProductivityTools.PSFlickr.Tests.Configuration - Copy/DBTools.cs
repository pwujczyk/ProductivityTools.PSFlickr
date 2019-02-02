using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PSFlickr.Tests.Configuration
{
    public class DBTools
    {
        private string DatabaseName
        {
            get
            {
                return new TestConfig().DatabaseName; 
            }
        }
        private string ConnectionStringServer
        {
            get
            {
                return ConnectionStringLightPT.ConnectionStringLight.GetSqlDataSourceConnectionString(new TestConfig().DataSource);
            }
        }

        public void DropDatabase()
        {
            string databaseName = DatabaseName;
            using (SqlConnection con = new SqlConnection(ConnectionStringServer))
            {
                con.Open();
                String sqlCommandText = $@"
                ALTER DATABASE {databaseName} SET SINGLE_USER WITH ROLLBACK IMMEDIATE;
                DROP DATABASE [{databaseName}]";
                SqlCommand sqlCommand = new SqlCommand(sqlCommandText, con);
                sqlCommand.ExecuteNonQuery();
            }
        }

        public void CheckDataBaseExistance()
        {
            string databaseName = DatabaseName;
            using (SqlConnection con = new SqlConnection(ConnectionStringServer))
            {
                con.Open();
                String sqlCommandText = $@"SELECT * FROM sys.databases where name='{databaseName}'";
                SqlCommand sqlCommand = new SqlCommand(sqlCommandText, con);
                SqlDataReader reader = sqlCommand.ExecuteReader();
                if (reader.Read())
                {
                    var dbDatabaseName = reader["name"];
                    var createdDate = reader["create_date"];
                }
            }
        }
    }
}
