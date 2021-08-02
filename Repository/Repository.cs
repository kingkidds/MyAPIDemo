using MyAPI.IRepository;
using MyAPI.DataBase;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using System.Linq;
using MyAPI.Common.Expression;
using MyAPI.Common.Extension;
using System.Data.Common;
using System.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;

namespace MyAPI.Repository
{
    #region << 版 本 注 释 >>
    //----------------------------------------------------------------
    // Copyright © 于国庆
    // 版权所有。
    //
    // 文件名：Repository
    // 文件功能描述：
    //
    //CLR版本：4.0.30319.42000
    //
    // 创建者：于国庆
    // QQ: 714024961
    // 时间：2021/8/2 15:18:21
    //
    // 修改人：
    // 时间：
    // 修改说明：
    //
    // 版本：V1.0.0
    //----------------------------------------------------------------
    #endregion

    public abstract class BaseRepository<T> : IRepository<T> where T : class
    {
        private readonly DbSet<T> _dbSet;
        public AppDBContext _dbContext { get; } = null;

        /// <summary>
        /// 连接字符串
        /// </summary>
        protected string _connectionString { get; set; }

        public BaseRepository(AppDBContext context)
        {
            _dbContext = context;
            _dbSet = _dbContext.Set<T>();
        }
        public DatabaseFacade Database => _dbContext.Database;
        public IQueryable<T> Entities => _dbSet.AsQueryable().AsNoTracking();
        public int SaveChanges()
        {
            return _dbContext.SaveChanges();
        }
        public async Task<int> SaveChangesAsync()
        {
            return await _dbContext.SaveChangesAsync();
        }




        #region 01 Count 查询记录数

        public int Count(Expression<Func<T, bool>> predicate = null)
        {
            if (predicate == null)
            {
                predicate = c => true;
            }
            return _dbSet.Count(predicate);
        }

        public async Task<int> CountAsync(Expression<Func<T, bool>> predicate = null)
        {
            if (predicate == null)
            {
                predicate = c => true;
            }
            return await _dbSet.CountAsync(predicate);
        }

        #endregion

        #region 02 Exists 判断记录是否存在
        public bool Any(Expression<Func<T, bool>> whereLambd)
        {
            return _dbSet.Where(whereLambd).Any();
        }

        public bool Exists(Expression<Func<T, bool>> predicate)
        {
            return _dbSet.Any(predicate);
        }
        #endregion

        #region 03 Get 获取单个实体
        public async Task<T> GetAsync(Expression<Func<T, bool>> predicate, bool isNoTracking = true)
        {
            var data = isNoTracking ? _dbSet.Where(predicate).AsNoTracking() : _dbSet.Where(predicate);
            return await data.FirstOrDefaultAsync();
        }

        public async Task<T> GetAsync(object id)
        {
            if (id == null)
            {
                return default(T);
            }
            return await _dbSet.FindAsync(id);
        }

        public T Get(Expression<Func<T, bool>> predicate, bool isNoTracking = true)
        {
            return GetEx<T>(predicate, null, null, isNoTracking);
        }
        public T Get(Expression<Func<T, bool>> predicate, Func<DbSort<T>, DbSort<T>> orderby, bool isNoTracking = true)
        {
            return GetEx<T>(predicate, null, orderby, isNoTracking);
        }

        #endregion

        #region 04 GetEx 获取单条记录

        public TResult GetEx<TResult>(Expression<Func<T, bool>> predicate, Expression<Func<T, TResult>> selector, bool isNoTracking = true)
        {
            return GetEx(predicate, selector, null, isNoTracking);
        }

        public TResult GetEx<TResult>(Expression<Func<T, bool>> predicate, Expression<Func<T, TResult>> selector, Func<DbSort<T>, DbSort<T>> orderby, bool isNoTracking = true)
        {

            var data = isNoTracking ? _dbSet.Where(predicate).AsNoTracking() : _dbSet.Where(predicate);

            if (!orderby.IsNull())
            {
                data = data.OrderByBatch(orderby(new DbSort<T>()));
            }

            if (!selector.IsNull())
            {
                return data.Select(selector).FirstOrDefault();
            }

            return data.Cast<TResult>().FirstOrDefault();
        }

        #endregion

        #region 05 ToList 查询实体列表

        public List<T> ToList(Expression<Func<T, bool>> predicate, bool isNoTracking = true)
        {

            return ToListEx<T>(predicate, null, 0, null, isNoTracking);
        }

        public List<T> ToList(Expression<Func<T, bool>> predicate, int top, bool isNoTracking = true)
        {
            return ToListEx<T>(predicate, null, top, null, isNoTracking);
        }

        public List<T> ToList(Expression<Func<T, bool>> predicate, int top, Func<DbSort<T>, DbSort<T>> orderby, bool isNoTracking = true)
        {
            return ToListEx<T>(predicate, null, top, orderby, isNoTracking);
        }

        public List<T> ToList(Expression<Func<T, bool>> predicate, Func<DbSort<T>, DbSort<T>> orderby, bool isNoTracking = true)
        {
            return ToListEx<T>(predicate, null, 0, orderby, isNoTracking);
        }

        #endregion

        #region 06 ToListEx 查询记录列表

        public List<TResult> ToListEx<TResult>(Expression<Func<T, bool>> predicate, Expression<Func<T, TResult>> selector, bool isNoTracking = true)
        {
            return ToListEx(predicate, selector, 0, null, isNoTracking);
        }

        public List<TResult> ToListEx<TResult>(Expression<Func<T, bool>> predicate, Expression<Func<T, TResult>> selector, int top, bool isNoTracking = true)
        {
            return ToListEx(predicate, selector, top, null, isNoTracking);
        }

        public List<TResult> ToListEx<TResult>(Expression<Func<T, bool>> predicate, Expression<Func<T, TResult>> selector, Func<DbSort<T>, DbSort<T>> orderby, bool isNoTracking = true)
        {
            return ToListEx(predicate, selector, 0, orderby, isNoTracking);
        }

        public List<TResult> ToListEx<TResult>(Expression<Func<T, bool>> predicate, Expression<Func<T, TResult>> selector, int top, Func<DbSort<T>, DbSort<T>> orderby, bool isNoTracking = true)
        {

            var data = isNoTracking ? _dbSet.Where(predicate).AsNoTracking() : _dbSet.Where(predicate);

            if (!orderby.IsNull())
            {
                data = data.OrderByBatch(orderby(new DbSort<T>()));
            }
            if (top > 0)
            {
                data = data.Take(top);
            }
            if (!selector.IsNull())
            {
                return data.Select(selector).ToList();
            }

            return data.Cast<TResult>().ToList();
            // data.OfType<TResult>().ToList();
        }


        public IQueryable<T> Load(Expression<Func<T, bool>> predicate, bool isNoTracking = true)
        {
            if (predicate == null)
            {
                predicate = c => true;
            }
            return isNoTracking ? _dbSet.Where(predicate).AsNoTracking() : _dbSet.Where(predicate);
        }

        public async Task<IQueryable<T>> LoadAsync(Expression<Func<T, bool>> predicate, bool isNoTracking = true)
        {
            if (predicate == null)
            {
                predicate = c => true;
            }
            return await Task.Run(() => isNoTracking ? _dbSet.Where(predicate).AsNoTracking() : _dbSet.Where(predicate));
        }

        #endregion

        #region  07 PageList 查询分页数据
        public PageList<T> PageList(Expression<Func<T, bool>> predicate, Func<DbSort<T>, DbSort<T>> orderby, int pageIndex, int pageSize, bool isNoTracking = true)
        {
            return PageListEx<T>(predicate, null, orderby, pageIndex, pageSize, isNoTracking);
        }
        #endregion

        #region 08 PageListEx 查询分页记录

        public PageList<TResult> PageListEx<TResult>(Expression<Func<T, bool>> predicate, Expression<Func<T, TResult>> selector, Func<DbSort<T>, DbSort<T>> orderby, int pageIndex, int pageSize, bool isNoTracking = true)
        {
            return getPageList(predicate, selector, orderby, pageIndex, pageSize, isNoTracking);

        }


        private PageList<TResult> getPageList<TResult>(
            Expression<Func<T, bool>> predicate,
            Expression<Func<T, TResult>> selector,
           Func<DbSort<T>, DbSort<T>> orderby,
            int pageIndex,
            int pageSize,
            bool isNoTracking = true
            )
        {
            var data = isNoTracking ? _dbSet.Where(predicate).AsNoTracking() : _dbSet.Where(predicate);

            if (!orderby.IsNull())
            {
                data = data.OrderByBatch(orderby(new DbSort<T>()));
            }

            if (!selector.IsNull())
            {
                var pageData = data.Skip((pageIndex - 1) * pageSize).Take(pageSize);
                var result = pageData.Select(selector);
                return new PageList<TResult>(
                        data.Count(),
                        pageSize,
                        pageIndex,
                        result.ToList()
                        );
            }
            else
            {
                var pageData = data.Skip((pageIndex - 1) * pageSize).Take(pageSize);
                var result = pageData.Cast<TResult>();
                return new PageList<TResult>(
                        data.Count(),
                        pageSize,
                        pageIndex,
                        result.ToList()
                        );
            }
        }
        #endregion

        #region  09 插入数据

        public bool Insert(T entity, bool isSaveChange = true)
        {
            _dbSet.Add(entity);
            if (isSaveChange)
            {
                return SaveChanges() > 0;
            }
            return false;
        }

        public async Task<bool> InsertAsync(T entity, bool isSaveChange = true)
        {
            _dbSet.Add(entity);
            if (isSaveChange)
            {
                return await SaveChangesAsync() > 0;
            }
            return false;
        }

        public bool Insert(List<T> entitys, bool isSaveChange = true)
        {
            _dbSet.AddRange(entitys);
            if (isSaveChange)
            {
                return SaveChanges() > 0;
            }
            return false;
        }

        public async Task<bool> InsertAsync(List<T> entitys, bool isSaveChange = true)
        {
            _dbSet.AddRange(entitys);
            if (isSaveChange)
            {
                return await SaveChangesAsync() > 0;
            }
            return false;
        }

        #endregion

        #region 10 修改数据

        public bool Update(T entity, bool isSaveChange = true, List<string> updatePropertyList = null)
        {
            if (entity == null)
            {
                return false;
            }
            _dbSet.Attach(entity);
            var entry = _dbContext.Entry(entity);
            if (updatePropertyList == null)
            {
                entry.State = EntityState.Modified;//全字段更新
            }
            else
            {

                updatePropertyList.ForEach(c =>
                {
                    entry.Property(c).IsModified = true; //部分字段更新的写法
                });


            }
            if (isSaveChange)
            {
                return SaveChanges() > 0;
            }
            return false;
        }

        public async Task<bool> UpdateAsync(T entity, bool isSaveChange = true, List<string> updatePropertyList = null)
        {
            if (entity == null)
            {
                return false;
            }
            _dbSet.Attach(entity);
            var entry = _dbContext.Entry<T>(entity);
            if (updatePropertyList == null)
            {
                entry.State = EntityState.Modified;//全字段更新
            }
            else
            {
                updatePropertyList.ForEach(c =>
                {
                    entry.Property(c).IsModified = true; //部分字段更新的写法
                });

            }
            if (isSaveChange)
            {
                return await SaveChangesAsync() > 0;
            }
            return false;
        }

        public bool Update(List<T> entitys, bool isSaveChange = true)
        {
            if (entitys == null || entitys.Count == 0)
            {
                return false;
            }
            entitys.ForEach(c =>
            {
                Update(c, false);
            });
            if (isSaveChange)
            {
                return SaveChanges() > 0;
            }
            return false;
        }

        public async Task<bool> UpdateAsync(List<T> entitys, bool isSaveChange = true)
        {
            if (entitys == null || entitys.Count == 0)
            {
                return false;
            }
            entitys.ForEach(c =>
            {
                _dbSet.Attach(c);
                _dbContext.Entry<T>(c).State = EntityState.Modified;
            });
            if (isSaveChange)
            {
                return await SaveChangesAsync() > 0;
            }
            return false;
        }

        #endregion

        #region 11 删除(删除之前需要查询)
        public bool Delete(T entity, bool isSaveChange = true)
        {
            _dbSet.Attach(entity);
            _dbSet.Remove(entity);
            return isSaveChange ? SaveChanges() > 0 : false;
        }

        public bool Delete(List<T> entitys, bool isSaveChange = true)
        {
            entitys.ForEach(entity =>
            {
                _dbSet.Attach(entity);
                _dbSet.Remove(entity);
            });
            return isSaveChange ? SaveChanges() > 0 : false;
        }

        public async Task<bool> DeleteAsync(T entity, bool isSaveChange = true)
        {
            _dbSet.Attach(entity);
            _dbSet.Remove(entity);
            return isSaveChange ? await SaveChangesAsync() > 0 : false;
        }

        public async Task<bool> DeleteAsync(List<T> entitys, bool isSaveChange = true)
        {
            entitys.ForEach(entity =>
            {
                _dbSet.Attach(entity);
                _dbSet.Remove(entity);
            });
            return isSaveChange ? await SaveChangesAsync() > 0 : false;
        }

        #endregion

        #region 12 执行SQL
        public virtual void BulkInsert<T1>(List<T1> entities)
        {
        }

        public int ExecuteSql(string sql)
        {
            return _dbContext.Database.ExecuteSqlRaw(sql);
        }

        public Task<int> ExecuteSqlAsync(string sql)
        {
            return _dbContext.Database.ExecuteSqlRawAsync(sql);
        }

        public int ExecuteSql(string sql, List<DbParameter> spList)
        {
            return _dbContext.Database.ExecuteSqlRaw(sql, spList.ToArray());
        }

        public Task<int> ExecuteSqlAsync(string sql, List<DbParameter> spList)
        {
            return _dbContext.Database.ExecuteSqlRawAsync(sql, spList.ToArray());
        }

        public virtual DataTable GetDataTableWithSql(string sql)
        {
            throw new NotImplementedException();
        }

        public virtual DataTable GetDataTableWithSql(string sql, List<DbParameter> spList)
        {
            throw new NotImplementedException();
        }

        #endregion

        #region 13 Query 未实现

        public IEnumerable<TResult> Query<TResult>(string sql, object parms, bool? readOnly = null)
        {
            throw new NotImplementedException();
        }

        public TResult ExecuteScalar<TResult>(string sql, object parms, bool? readOnly = null)
        {
            throw new NotImplementedException();
        }

        #endregion

        #region Dispose

        private bool disposed = false;

        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    if (this._dbContext != null)
                        this._dbContext.Dispose();

                }
            }

            this.disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        ~BaseRepository()
        {
            Dispose(false);
        }
        #endregion
    }
}
