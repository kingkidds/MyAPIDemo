using MyAPI.Model;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MyAPI.IService
{
    #region << 版 本 注 释 >>
    //----------------------------------------------------------------
    // Copyright © 于国庆
    // 版权所有。
    //
    // 文件名：ITouristRouteService
    // 文件功能描述：
    //
    //CLR版本：4.0.30319.42000
    //
    // 创建者：于国庆
    // QQ: 714024961
    // 时间：2021/8/2 16:48:18
    //
    // 修改人：
    // 时间：
    // 修改说明：
    //
    // 版本：V1.0.0
    //----------------------------------------------------------------
    #endregion

    public interface ITouristRouteService
    {
        List<TouristRoute> GetAllTouristRoutesAsync();
    }
}
