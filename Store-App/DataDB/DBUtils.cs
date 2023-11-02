using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;

namespace Tutorial.SqlConn
{
    class DBUtils
    {
        public static SqlConnection GetDBConnection()
        {
            string datasource = @"DESKTOP-ET5DFTO\SQLEXPRESS";
            string database = "store-app-db";
            string username = "store-admin";
            string password = "brand-new-store-2023";
            return DBSQLServerUtils.GetDBConnection(datasource, database, username, password);
        }
    }
}