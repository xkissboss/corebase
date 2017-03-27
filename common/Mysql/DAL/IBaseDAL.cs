using common.Model;
using System;
using System.Collections.Generic;
using System.Text;
using static common.DapperExtend.SqlBuilder;

namespace common.Mysql.DAL
{
    /// <summary>
    /// 数据库操作的基本接口
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <typeparam name="PK"></typeparam>
    public interface IBaseDAL<T, PK> where T : BaseModel
        //where PK : Int64 // 字符串 也可是int
    {
        /// <summary>
        /// 根据主键删除
        /// </summary>
        /// <param name="pk"></param>
        /// <returns></returns>
        int DeleteById(PK pk);

        /// <summary>
        /// 插入
        /// </summary>
        /// <param name="t">实体</param>
        /// <param name="getId">是否获取（自增主键），不获取默认返回0</param>
        /// <param name="fields">插入字段，如果没有则获取表的所有字段</param>
        /// <returns></returns>
        long Insert(T t, bool getId = false, params string[] fields);

        /// <summary>
        /// 根据主键获取
        /// </summary>
        /// <param name="pk">主键</param>
        /// <param name="fields">获取的字段，没传则获取全部字段</param>
        /// <returns></returns>
        T FindById(PK pk, params string[] fields);

        /// <summary>
        /// 根据主键列表获取
        /// </summary>
        /// <param name="pkList">主键列表</param>
        /// <param name="fields">获取的字段，没传则获取全部字段</param>
        /// <returns></returns>
        List<T> FindByIds(List<PK> pkList, params string[] fields);

        /// <summary>
        /// 根据主键更新
        /// </summary>
        /// <param name="pk">主键</param>
        /// <param name="data">更新字段</param>
        /// <returns></returns>
        int UpdateById(PK pk, Dictionary<string, object> data);

        /// <summary>
        /// 根据sql进行更新
        /// </summary>
        /// <param name="sql">更新语句，如update xx set a = @a where b=@b</param>
        /// <param name="data">更新字段及where参数</param>
        /// <returns></returns>
        int UpdateByWhere(string sql, Dictionary<string, object> data);

        /// <summary>
        /// 根据模版删除
        /// </summary>
        /// <param name="template"></param>
        /// <returns></returns>
        int DeleteByWhere(Template template);

        /// <summary>
        /// 根据模版更新
        /// </summary>
        /// <param name="template"></param>
        /// <returns></returns>
        int UpdateByWhere(Template template);

        /// <summary>
        /// 根据模版查找
        /// </summary>
        /// <param name="template"></param>
        /// <returns></returns>
        T FindByWhere(Template template);

        /// <summary>
        /// 根据模版查找列表
        /// </summary>
        /// <param name="template"></param>
        /// <returns></returns>
        List<T> FindListByWhere(Template template);

        /// <summary>
        /// 关联查询
        /// </summary>
        /// <typeparam name="TFirst">实体1</typeparam>
        /// <typeparam name="TSecond">实体2</typeparam>
        /// <param name="template">模版</param>
        /// <param name="func">回调函数</param>
        /// <param name="splitOn">分隔符,逗号分隔的，用来划分查询中的字段是属于哪个表，也就是查询结构映射到哪个实体</param>
        /// <returns></returns>
        List<T> FindListByWhere<TFirst, TSecond>(Template template, Func<TFirst, TSecond, T> func, string splitOn = "Id");

        /// <summary>
        /// 关联查询
        /// </summary>
        /// <typeparam name="TFirst">实体1</typeparam>
        /// <typeparam name="TSecond">实体2</typeparam>
        /// <typeparam name="TThird">实体3</typeparam>
        /// <param name="template">模版</param>
        /// <param name="func">回调函数</param>
        /// <param name="splitOn">分隔符,逗号分隔的，用来划分查询中的字段是属于哪个表，也就是查询结构映射到哪个实体</param>
        /// <returns></returns>
        List<T> FindListByWhere<TFirst, TSecond, TThird>(Template template, Func<TFirst, TSecond, TThird, T> func, string splitOn = "Id");

        /// <summary>
        /// 关联查询
        /// </summary>
        /// <typeparam name="TFirst">实体1</typeparam>
        /// <typeparam name="TSecond">实体2</typeparam>
        /// <typeparam name="TThird">实体3</typeparam>
        /// <typeparam name="TFourth">实体4</typeparam>
        /// <param name="template">模版</param>
        /// <param name="func">回调函数</param>
        /// <param name="splitOn">分隔符,逗号分隔的，用来划分查询中的字段是属于哪个表，也就是查询结构映射到哪个实体</param>
        /// <returns></returns>
        List<T> FindListByWhere<TFirst, TSecond, TThird, TFourth>(Template template, Func<TFirst, TSecond, TThird, TFourth, T> func, string splitOn = "Id");

        /// <summary>
        /// 关联查询
        /// </summary>
        /// <typeparam name="TFirst">实体1</typeparam>
        /// <typeparam name="TSecond">实体2</typeparam>
        /// <typeparam name="TThird">实体3</typeparam>
        /// <typeparam name="TFourth">实体4</typeparam>
        /// <typeparam name="TFifth">实体5</typeparam>
        /// <param name="template">模版</param>
        /// <param name="func">回调函数</param>
        /// <param name="splitOn">分隔符,逗号分隔的，用来划分查询中的字段是属于哪个表，也就是查询结构映射到哪个实体</param>
        /// <returns></returns>
        List<T> FindListByWhere<TFirst, TSecond, TThird, TFourth, TFifth>(Template template, Func<TFirst, TSecond, TThird, TFourth, TFifth, T> func, string splitOn = "Id");

        //Page<T> FindByPage(Template template, t);
    }
}
