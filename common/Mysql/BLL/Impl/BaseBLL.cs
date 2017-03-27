using common.Model;
using common.Mysql.DAL;
using System;
using System.Collections.Generic;
using System.Text;

namespace common.Mysql.BLL.Impl
{
    public abstract class BaseBLL<T, PK> where T : BaseModel
    {
        public abstract IBaseDAL<T, PK> DacOper();

        public virtual bool DeleteById(PK pk)
        {
            return DacOper().DeleteById(pk) > 0;
        }

        public virtual long Insert(T t, bool getId = false, params string[] fields)
        {
            return DacOper().Insert(t, getId, fields);
        }

        public virtual T FindById(PK pk, params string[] fields)
        {
            return DacOper().FindById(pk, fields);
        }

        public virtual List<T> FindByIds(List<PK> pkList, params string[] fields)
        {
            return DacOper().FindByIds(pkList, fields);
        }

        public virtual bool UpdateById(PK pk, Dictionary<string, object> data)
        {
            return DacOper().UpdateById(pk, data) > 0;
        }
    }
}
