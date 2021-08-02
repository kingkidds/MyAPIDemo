using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace MyAPI.Common.Expression
{
    #region << 版 本 注 释 >>
    //----------------------------------------------------------------
    // Copyright © 于国庆
    // 版权所有。
    //
    // 文件名：DbSort
    // 文件功能描述：
    //
    //CLR版本：4.0.30319.42000
    //
    // 创建者：于国庆
    // QQ: 714024961
    // 时间：2021/8/2 15:11:44
    //
    // 修改人：
    // 时间：
    // 修改说明：
    //
    // 版本：V1.0.0
    //----------------------------------------------------------------
    #endregion

    public class DbSort<T>
    {
        private string orderby;

        public DbSort(string str = null)
        {
            this.orderby = str;
        }

        /// <summary>
        /// 排序(正序)
        /// </summary>
        /// <typeparam name="TField">排序字段</typeparam>
        /// <param name="expression">排序表达式</param>
        /// <returns></returns>
        public DbSort<T> Asc<TField>(Expression<Func<T, TField>> expression)
        {
            this.orderby += "," + (expression.Body as MemberExpression).Member.Name;
            return this;
        }

        /// <summary>
        /// 排序(反序)
        /// </summary>
        /// <typeparam name="TField">排序字段</typeparam>
        /// <param name="expression">排序表达式</param>
        /// <returns></returns>
        public DbSort<T> Desc<TField>(Expression<Func<T, TField>> expression)
        {
            //this.orderby += "," + (expression.Body as MemberExpression).Member.Name + " DESC";
            this.orderby += ",-" + (expression.Body as MemberExpression).Member.Name;
            return this;
        }

        public static implicit operator string(DbSort<T> value)
        {
            return value == null ? null : value.orderby.TrimStart(',');
        }
    }
}
