using System;
using System.Collections.Generic;
using System.Text;

namespace MyAPI.Model
{
    #region << 版 本 注 释 >>
    //----------------------------------------------------------------
    // Copyright © 于国庆
    // 版权所有。
    //
    // 文件名：Class1
    // 文件功能描述：
    //
    //CLR版本：4.0.30319.42000
    //
    // 创建者：于国庆
    // QQ: 714024961
    // 时间：2021/8/2 11:14:53
    //
    // 修改人：
    // 时间：
    // 修改说明：
    //
    // 版本：V1.0.0
    //----------------------------------------------------------------
    #endregion

    public class TouristRoutePicture
    {
        /// <summary>
        /// ID
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// URL地址
        /// </summary>
        public string Url { get; set; }
        /// <summary>
        /// 旅游路线外键
        /// </summary>
        public Guid TouristRouteId { get; set; }
        /// <summary>
        /// 旅游路线
        /// </summary>
        public TouristRoute TouristRoute { get; set; }
    }
}
