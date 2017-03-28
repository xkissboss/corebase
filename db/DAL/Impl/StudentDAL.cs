using common.Mysql.DAL.Impl;
using common.Mysql.Utils;
using db.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace db.DAL.Impl
{
    public class StudentDAL : BaseImpl<Student, Int64>, IStudentDAL
    {

        public StudentDAL(IDbConn conn)
        {
            this._conn = conn;
        }

        public override string[] Fields()
        {
            return Student.Fields;
        }

        public override string TableName()
        {
            return Student.TableName;
        }

        public override string PrivateKey
        {
            get
            {
                return "sid";
            }
        }
    }
}
