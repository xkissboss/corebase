using common.Model;
using common.Mysql.Utils;
using Dapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using static common.DapperExtend.SqlBuilder;

namespace common.Mysql.DAL.Impl
{
    /// <summary>
    /// 数据库操作的基本实现
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <typeparam name="PK"></typeparam>
    public abstract class BaseImpl<T, PK> where T : BaseModel
    {
        protected IDbConn _conn;


        public virtual IDbConn DbConn()
        {
            return this._conn;
        }


        /// <summary>
        /// 表名称
        /// </summary>
        /// <returns></returns>
        public abstract string TableName();

        /// <summary>
        /// 主键
        /// </summary>
        public virtual string PrivateKey
        {
            get
            {
                return "id";
            }
        }
        public abstract string[] Fields();

        string ColunmNames
        {
            get
            {
                return string.Join(",", Fields());
            }
        }

        string ColunmNamesParams
        {
            get
            {
                return "@" + string.Join(",@", Fields());
            }
        }


        public int DeleteById(PK pk)
        {
            string sql = string.Format("delete from {0} where {1} = @pk", TableName(), PrivateKey);
            return DbConn().GetConn().Execute(sql, new { pk = pk });
        }

        public long Insert(T t, bool getId = false, params string[] fields)
        {
            string cn = fields.Length > 0 ? string.Join(",", fields) : ColunmNames;
            string cnp = fields.Length > 0 ? $"@{string.Join(",@", fields)}" : ColunmNamesParams;

            DbConn().GetConn().Execute($"insert into {TableName()} ({cn}) values ({cnp})", t);
            if (!getId) return 0;
            return DbConn().GetConn().Query<Int64>("Select LAST_INSERT_ID() id").FirstOrDefault();
        }

        public T FindById(PK pk, params string[] fields)
        {
            string fd = fields.Length > 0 ? string.Join(",", fields) : "*";
            string sql = string.Format("select {1} from {0} where {2} = @pk", TableName(), fd, PrivateKey);
            return DbConn().GetConn().QueryFirst<T>(sql, new { pk = pk });
        }

        public List<T> FindByIds(List<PK> pkList, params string[] fields)
        {
            if (pkList == null || pkList.Count < 1) return null;
            string fd = fields.Length > 0 ? string.Join(",", fields) : "*";
            string sql = string.Format("select {1} from {0} where {2} in @pks", TableName(), fd, PrivateKey);
            return DbConn().GetConn().Query<T>(sql, new { pks = pkList.ToArray() }).ToList();
        }

        public int UpdateById(PK pk, Dictionary<string, object> data)
        {
            if (data == null || data.Count() < 1) return 0;

            StringBuilder sb = new StringBuilder();
            foreach (string key in data.Keys)
            {
                sb.AppendFormat("{0}=@{0}, ", key);
            }
            string sql = $"update {TableName()} set {sb.ToString().TrimEnd(',', ' ')} where {PrivateKey} = @pk";
            data.Add("pk", pk);
            return DbConn().GetConn().Execute(sql, data);
        }

        public int UpdateByWhere(string sql, Dictionary<string, object> data)
        {
            if (data == null || data.Count() < 1) return 0;
            return DbConn().GetConn().Execute(sql, data);
        }

        public int DeleteByWhere(Template template)
        {
            if (template == null || string.IsNullOrEmpty(template.RawSql)) return 0;
            return DbConn().GetConn().Execute(template.RawSql, template.Parameters);
        }

        public int UpdateByWhere(Template template)
        {
            if (template == null || string.IsNullOrEmpty(template.RawSql)) return 0;
            return DbConn().GetConn().Execute(template.RawSql, template.Parameters);
        }

        public T FindByWhere(Template template)
        {
            if (template == null || string.IsNullOrEmpty(template.RawSql)) return null;
            return DbConn().GetConn().QueryFirst<T>(template.RawSql, template.Parameters);
        }

        public List<T> FindListByWhere(Template template)
        {
            if (template == null || string.IsNullOrEmpty(template.RawSql)) return null;

            return DbConn().GetConn().Query<T>(template.RawSql, template.Parameters).ToList();
        }

        public List<T> FindListByWhere<TFirst, TSecond>(Template template, Func<TFirst, TSecond, T> func, string splitOn = "Id")
        {
            if (template == null || string.IsNullOrEmpty(template.RawSql)) return null;
            return DbConn().GetConn().Query<TFirst, TSecond, T>(template.RawSql, func,
                 template.Parameters, splitOn: splitOn).ToList();
        }

        public List<T> FindListByWhere<TFirst, TSecond, TThird>(Template template, Func<TFirst, TSecond, TThird, T> func, string splitOn = "Id")
        {
            if (template == null || string.IsNullOrEmpty(template.RawSql)) return null;
            return DbConn().GetConn().Query<TFirst, TSecond, TThird, T>(template.RawSql, func,
                 template.Parameters, splitOn: splitOn).ToList();
        }


        public List<T> FindListByWhere<TFirst, TSecond, TThird, TFourth>(Template template, Func<TFirst, TSecond, TThird, TFourth, T> func, string splitOn = "Id")
        {
            if (template == null || string.IsNullOrEmpty(template.RawSql)) return null;
            return DbConn().GetConn().Query<TFirst, TSecond, TThird, TFourth, T>(template.RawSql, func,
                 template.Parameters, splitOn: splitOn).ToList();
        }

        public List<T> FindListByWhere<TFirst, TSecond, TThird, TFourth, TFifth>(Template template, Func<TFirst, TSecond, TThird, TFourth, TFifth, T> func, string splitOn = "Id")
        {
            if (template == null || string.IsNullOrEmpty(template.RawSql)) return null;
            return DbConn().GetConn().Query<TFirst, TSecond, TThird, TFourth, TFifth, T>(template.RawSql, func,
                 template.Parameters, splitOn: splitOn).ToList();
        }

    }
}
