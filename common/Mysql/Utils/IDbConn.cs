using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace common.Mysql.Utils
{
    public interface IDbConn
    {
        IDbConnection GetConn();
    }
}
