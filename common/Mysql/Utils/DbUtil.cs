using common.CSRedis;
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
        private static string _connectonString;


        public static string ConnectionString
        {
            get
            {
                if (string.IsNullOrEmpty(_connectonString))
                    _connectonString = RedisHelper.Get("connection_mysql");
                return _connectonString;
            }
        }

        public static MySqlConnection GetOpenConnection()
        {
            MySqlConnection conn = new MySqlConnection(ConnectionString);
            return conn;
        }

    }
}
