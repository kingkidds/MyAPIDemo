using MyAPI.Common.Expression;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace MyAPI.IRepository
{
    #region << 版 本 注 释 >>
    //----------------------------------------------------------------
    // Copyright © 于国庆
    // 版权所有。
    //
    // 文件名：Interface1
    // 文件功能描述：
    //
    //CLR版本：4.0.30319.42000
    //
    // 创建者：于国庆
    // QQ: 714024961
    // 时间：2021/8/2 15:05:04
    //
    // 修改人：
    // 时间：
    // 修改说明：
    //
    // 版本：V1.0.0
    //----------------------------------------------------------------
    #endregion

    public interface IReadRepository<T>:IDisposable
    {
        #region 01 Count 查询记录数

        /// <summary>
        /// 查询记录数
        /// </summary>
        /// <param name="predicate">查询条件</param>
        /// <returns></returns>
        int Count(Expression<Func<T, bool>> predicate);
        Task<int> CountAsync(Expression<Func<T, bool>> predicate = null);

        #endregion

        #region 02 Exists 判断记录是否存在

        /// <summary>
        /// 查询记录是否存在
        /// </summary>
        /// <param name="predicate">查询条件</param>
        /// <returns></returns>
        bool Exists(Expression<Func<T, bool>> predicate);

        #endregion

        #region 03 Get 获取单个实体

        Task<T> GetAsync(Expression<Func<T, bool>> predicate, bool isNoTracking = true);

        Task<T> GetAsync(object id);
        /// <summary>
        /// 查询个实体
        /// </summary>
        /// <param name="predicate">查询条件</param>
        /// <returns></returns>
        T Get(Expression<Func<T, bool>> predicate, bool isNoTracking = true);

        /// <summary>
        /// 查询单个实体
        /// </summary>
        /// <param name="predicate">查询条件</param>
        /// <param name="orderby">排序 例：o=>o.Desc(u=>u.CreateTime)</param>
        /// <returns></returns>
        T Get(Expression<Func<T, bool>> predicate, Func<DbSort<T>, DbSort<T>> orderby, bool isNoTracking = true);

        #endregion

        #region 04 GetEx 获取单条记录

        /// <summary>
        /// 查询单条记录
        /// </summary>
        /// <typeparam name="TResult">查询结果类型</typeparam>
        /// <param name="predicate">查询条件</param>
        /// <param name="selector">查询哪些字段（例一: u=>u.UserId 例二: u=>new{ u.UserId, u.UserName}）</param>
        /// <returns></returns>
        TResult GetEx<TResult>(
            Expression<Func<T, bool>> predicate,
            Expression<Func<T, TResult>> selector, bool isNoTracking = true
            );


        /// <summary>
        /// 查询单条记录
        /// </summary>
        /// <typeparam name="TResult">查询结果类型</typeparam>
        /// <param name="predicate">查询条件</param>
        /// <param name="selector">查询哪些字段（例一: u=>u.UserId 例二: u=>new{ u.UserId, u.UserName}）</param>
        /// <param name="orderby">排序 例：o=>o.Desc(u=>u.CreateTime)</param>
        /// <returns></returns>
        TResult GetEx<TResult>(
            Expression<Func<T, bool>> predicate,
            Expression<Func<T, TResult>> selector,
            Func<DbSort<T>, DbSort<T>> orderby,
            bool isNoTracking = true
            );



        #endregion

        #region 05 ToList 查询实体列表


        IQueryable<T> Load(Expression<Func<T, bool>> predicate, bool isNoTracking = true);
        Task<IQueryable<T>> LoadAsync(Expression<Func<T, bool>> predicate, bool isNoTracking = true);


        /// <summary>
        /// 查询实体列表
        /// </summary>
        /// <param name="predicate">查询条件</param>
        /// <param name="dbLock">数据库锁</param>
        /// <returns></returns>
        List<T> ToList(
            Expression<Func<T, bool>> predicate, bool isNoTracking = true
            );

        /// <summary>
        /// 查询实体列表
        /// </summary>
        /// <param name="predicate">查询条件</param>
        /// <param name="top">最多返回几条记录</param>
        /// <param name="dbLock">数据库锁</param>
        /// <returns></returns>
        List<T> ToList(
            Expression<Func<T, bool>> predicate,
            int top, bool isNoTracking = true
            );

        /// <summary>
        /// 查询实体列表
        /// </summary>
        /// <param name="predicate">查询条件</param>
        /// <param name="top">最多返回几条记录</param>
        /// <param name="orderby">排序 例：o=>o.Desc(u=>u.CreateTime)</param>
        /// <param name="dbLock">数据库锁</param>
        /// <returns></returns>
        List<T> ToList(
            Expression<Func<T, bool>> predicate,
            int top,
            Func<DbSort<T>, DbSort<T>> orderby, bool isNoTracking = true
            );

        /// <summary>
        /// 查询实体列表
        /// </summary>
        /// <param name="predicate">查询条件</param>
        /// <param name="orderby">排序 例：o=>o.Desc(u=>u.CreateTime)</param>
        /// <param name="dbLock">数据库锁</param>
        /// <returns></returns>
        List<T> ToList(
            Expression<Func<T, bool>> predicate,
            Func<DbSort<T>, DbSort<T>> orderby, bool isNoTracking = true
            );

        #endregion

        #region 06 ToListEx 查询记录列表

        /// <summary>
        /// 查询记录列表
        /// </summary>
        /// <param name="predicate">查询条件</param>
        /// <param name="selector">查询哪些字段（例一: u=>u.UserId 例二: u=>new{ u.UserId, u.UserName}）</param>
        List<TResult> ToListEx<TResult>(
            Expression<Func<T, bool>> predicate,
            Expression<Func<T, TResult>> selector,
            bool isNoTracking = true
            );

        /// <summary>
        /// 查询记录列表
        /// </summary>
        /// <param name="predicate">查询条件</param>
        /// <param name="selector">查询哪些字段（例一: u=>u.UserId 例二: u=>new{ u.UserId, u.UserName}）</param>
        /// <param name="top">最多返回几条记录（0表示不限制）</param>
        List<TResult> ToListEx<TResult>(
            Expression<Func<T, bool>> predicate,
            Expression<Func<T, TResult>> selector,
            int top,
            bool isNoTracking = true
            );

        /// <summary>
        /// 查询记录列表
        /// </summary>
        /// <param name="predicate">查询条件</param>
        /// <param name="selector">查询哪些字段（例一: u=>u.UserId 例二: u=>new{ u.UserId, u.UserName}）</param>
        /// <param name="orderby">排序 例：o=>o.Desc(u=>u.CreateTime)</param>
        List<TResult> ToListEx<TResult>(
            Expression<Func<T, bool>> predicate,
            Expression<Func<T, TResult>> selector,
            Func<DbSort<T>, DbSort<T>> orderby,
            bool isNoTracking = true
            );

        /// <summary>
        /// 查询记录列表
        /// </summary>
        /// <param name="predicate">查询条件</param>
        /// <param name="selector">查询哪些字段（例一: u=>u.UserId 例二: u=>new{ u.UserId, u.UserName}）</param>
        /// <param name="top">最多返回几条记录（0表示不限制）</param>
        /// <param name="orderby">排序 例：o=>o.Desc(u=>u.CreateTime)</param>
        List<TResult> ToListEx<TResult>(
            Expression<Func<T, bool>> predicate,
            Expression<Func<T, TResult>> selector,
            int top,
            Func<DbSort<T>, DbSort<T>> orderby,
            bool isNoTracking = true
            );




        #endregion

        #region 07 PageList 查询分页数据

        /// <summary>
        /// 查询分页数据
        /// </summary>
        /// <param name="predicate">查询条件</param>
        /// <param name="orderby">排序 例：o=>o.Desc(u=>u.CreateTime)</param>
        /// <param name="pageIndex">查询第几页(1为首页)</param>
        /// <param name="pageSize">每页记录数</param>
        /// <returns></returns>
        PageList<T> PageList(
            Expression<Func<T, bool>> predicate,
            Func<DbSort<T>, DbSort<T>> orderby,
            int pageIndex,
            int pageSize,
            bool isNoTracking = true
            );




        #endregion

        #region 08 PageListEx 查询分页记录

        /// <summary>
        /// 查询分页记录
        /// </summary>
        /// <typeparam name="TResult">返回类型</typeparam>
        /// <param name="predicate">查询条件</param>
        /// <param name="selector">查询哪些字段（例一: u=>u.UserId 例二: u=>new{ u.UserId, u.UserName}）</param>
        /// <param name="orderby">排序 例：o=>o.Desc(u=>u.CreateTime)</param>
        /// <param name="pageIndex">查询第几页(1为首页)</param>
        /// <param name="pageSize">每页记录数</param>
        /// <returns></returns>
        PageList<TResult> PageListEx<TResult>(
            Expression<Func<T, bool>> predicate,
            Expression<Func<T, TResult>> selector,
            Func<DbSort<T>, DbSort<T>> orderby,
            int pageIndex,
            int pageSize,
            bool isNoTracking = true);



        #endregion

        #region 09 SQL 查询

        /// <summary>
        /// 执行 SQL 语句查询
        /// </summary>
        /// <typeparam name="TResult">返回对象类型</typeparam>
        /// <param name="sql">要执行的SQL语句</param>
        /// <param name="parms">SQL参数，例： new {name=="test1",mobile="138888888"}</param>
        /// <returns>查询结果对象枚举</returns>
        IEnumerable<TResult> Query<TResult>(string sql, object parms, bool? readOnly = null);


        /// <summary>
        /// 执行 SQL 语句查询单个对象
        /// </summary>
        /// <typeparam name="TResult">返回对象类型</typeparam>
        /// <param name="sql">要执行的SQL语句</param>
        /// <param name="parms">SQL参数，例： new {name=="test1",mobile="138888888"}</param>
        /// <returns>查询结果对象</returns>
        TResult ExecuteScalar<TResult>(string sql, object parms, bool? readOnly = null);

        #endregion
    }
}
