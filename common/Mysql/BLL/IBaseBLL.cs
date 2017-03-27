using common.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace common.Mysql.BLL
{
    public interface IBaseBLL<T, PK> where T : BaseModel
    {
        bool DeleteById(PK pk);

        long Insert(T t, bool getId = false, params string[] fields);

        T FindById(PK pk, params string[] fields);

        List<T> FindByIds(List<PK> pkList, params string[] fields);

        bool UpdateById(PK pk, Dictionary<string, object> data);
    }
}
