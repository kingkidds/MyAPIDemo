using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Text;
using System.Threading.Tasks;

namespace MyAPI.IRepository
{
    #region << 版 本 注 释 >>
    //----------------------------------------------------------------
    // Copyright © 于国庆
    // 版权所有。
    //
    // 文件名：IRepository
    // 文件功能描述：
    //
    //CLR版本：4.0.30319.42000
    //
    // 创建者：于国庆
    // QQ: 714024961
    // 时间：2021/8/2 15:16:48
    //
    // 修改人：
    // 时间：
    // 修改说明：
    //
    // 版本：V1.0.0
    //----------------------------------------------------------------
    #endregion

    public interface IRepository<T> : IReadRepository<T>, IDisposable where T : class
    {
        int SaveChanges();


        #region 插入数据
        bool Insert(T entity, bool isSaveChange = true);
        Task<bool> InsertAsync(T entity, bool isSaveChange = true);
        bool Insert(List<T> entitys, bool isSaveChange = true);
        Task<bool> InsertAsync(List<T> entitys, bool isSaveChange = true);
        #endregion


        #region 修改数据
        bool Update(T entity, bool isSaveChange = true, List<string> updatePropertyList = null);
        Task<bool> UpdateAsync(T entity, bool isSaveChange = true, List<string> updatePropertyList = null);
        bool Update(List<T> entitys, bool isSaveChange = true);
        Task<bool> UpdateAsync(List<T> entitys, bool isSaveChange = true);
        #endregion

        #region 删除(删除之前需要查询)
        bool Delete(T entity, bool isSaveChange = true);
        bool Delete(List<T> entitys, bool isSaveChange = true);
        Task<bool> DeleteAsync(T entity, bool isSaveChange = true);
        Task<bool> DeleteAsync(List<T> entitys, bool isSaveChange = true);
        #endregion


        #region 执行Sql语句
        void BulkInsert<T>(List<T> entities);
        int ExecuteSql(string sql);
        Task<int> ExecuteSqlAsync(string sql);
        int ExecuteSql(string sql, List<DbParameter> spList);
        Task<int> ExecuteSqlAsync(string sql, List<DbParameter> spList);
        DataTable GetDataTableWithSql(string sql);

        DataTable GetDataTableWithSql(string sql, List<DbParameter> spList);



        #endregion


    }
}
