using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;

namespace Store_App.DataDB
{
    class DBUtils
    {
        public static SqlConnection GetDBConnection()
        {
            string datasource = @"IP_SECRET";
            string database = "DB_SECRET";
            string username = "USERNAME_SECRET";
            string password = "PASS_SECRET";
            return DBSQLServerUtils.GetDBConnection(datasource, database, username, password);
        }
    }
}