using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Text;

namespace common.Mysql.Utils
{
    public class DbUtil
    {

        /// <summary>
        /// 数据库连接字符串
        /// </summary>
        public static readonly string MySqlConn =
                 "server=127.0.0.1;database=blog;uid=root;pwd=root;charset='utf8'";

        public static MySqlConnection GetOpenConnection()
        {
            MySqlConnection conn = new MySqlConnection(MySqlConn);
            return conn;
        }

    }
}
