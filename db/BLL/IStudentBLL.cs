using common.Mysql.BLL;
using db.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace db.BLL
{
    public interface IStudentBLL : IBaseBLL<Student, Int64>
    {
    }
}
