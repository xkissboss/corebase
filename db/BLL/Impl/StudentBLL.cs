using common.Mysql.BLL.Impl;
using db.Model;
using System;
using System.Collections.Generic;
using System.Text;
using common.Mysql.DAL;
using db.DAL;

namespace db.BLL.Impl
{
    public class StudentBLL : BaseBLL<Student, Int64>, IStudentBLL
    {

        private IStudentDAL _studentDAL;
        public StudentBLL(IStudentDAL studentDAL)
        {
            _studentDAL = studentDAL;
        }

        public override IBaseDAL<Student, Int64> DacOper()
        {
            return _studentDAL;
        }
    }
}
