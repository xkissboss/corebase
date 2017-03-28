using common.Mysql.DAL;
using db.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace db.DAL
{
    public interface IStudentDAL : IBaseDAL<Student, Int64>
    {
    }
}
