using System;
using System.Collections.Generic;
using System.Text;

namespace MyAPI.Enum.Model
{
    #region << 版 本 注 释 >>
    //----------------------------------------------------------------
    // Copyright © 于国庆
    // 版权所有。
    //
    // 文件名：TripType
    // 文件功能描述：
    //
    //CLR版本：4.0.30319.42000
    //
    // 创建者：于国庆
    // QQ: 714024961
    // 时间：2021/8/2 11:59:53
    //
    // 修改人：
    // 时间：
    // 修改说明：
    //
    // 版本：V1.0.0
    //----------------------------------------------------------------
    #endregion

    /// <summary>
    /// 类型
    /// </summary>
    public enum TripType
    {
        
        /// <summary>
        /// 酒店+景点
        /// </summary>
        HotelAndAttractions,
        /// <summary>
        /// 跟团游
        /// </summary>
        Group,
        /// <summary>
        /// 私家团
        /// </summary>
        PrivateGroup,
        /// <summary>
        /// 自由行
        /// </summary>
        BackPackTour,
        /// <summary>
        /// 半自助游
        /// </summary>
        SemiBackPackTour
    }
}
