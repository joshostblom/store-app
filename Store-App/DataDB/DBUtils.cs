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
            string datasource = @"209.50.10.62,49170";
            string database = "master";
            string username = "store-admin";
            string password = "brand-new-store-2023";
            return DBSQLServerUtils.GetDBConnection(datasource, database, username, password);
        }
    }
}