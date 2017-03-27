using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace common.Mysql.Utils
{
    public class MySqlDbConnection : IDbConn, IDisposable
    {

        public void Dispose()
        {
            if (_conn != null)
            {
                _conn.Close();
                _conn.Dispose();
            }
        }

        private MySqlConnection _conn;

        public IDbConnection GetConn()
        {
            if (_conn == null)
            {
                _conn = DbUtil.GetOpenConnection();
            }
            return _conn;
        }
    }
}
