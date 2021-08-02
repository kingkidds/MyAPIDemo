using System;
using System.Collections.Generic;
using System.Text;

namespace MyAPI.Common.Expression
{
    #region << 版 本 注 释 >>
    //----------------------------------------------------------------
    // Copyright © 于国庆
    // 版权所有。
    //
    // 文件名：PageList
    // 文件功能描述：
    //
    //CLR版本：4.0.30319.42000
    //
    // 创建者：于国庆
    // QQ: 714024961
    // 时间：2021/8/2 15:12:48
    //
    // 修改人：
    // 时间：
    // 修改说明：
    //
    // 版本：V1.0.0
    //----------------------------------------------------------------
    #endregion

    public class PageList<T>
    {
        public PageList(int total, int pageSize, int pageIndex, List<T> items)
        {
            this.total = total;
            this.pageSize = pageSize;
            this.pageIndex = pageIndex;
            this.items = items;
            this.totalPage = this.total % this.pageSize == 0 ? this.total / this.pageSize : this.total / this.pageSize + 1;
        }

        private int total;
        public int Total
        {
            get { return this.total; }
        }

        private List<T> items;
        public List<T> Items
        {
            get { return this.items; }
        }

        private int pageSize;
        public int PageSize
        {
            get { return this.pageSize; }
        }

        private int pageIndex;
        public int PageIndex
        {
            get { return this.pageIndex; }
        }

        private int totalPage;
        public int TotalPage
        {
            get { return this.totalPage; }
        }

        public bool HasPrev
        {
            get { return this.pageIndex > 1; }
        }

        public bool HasNext
        {
            get { return this.pageIndex < this.totalPage; }
        }
    }
}
