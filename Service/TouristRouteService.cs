using MyAPI.IRepository;
using MyAPI.IService;
using MyAPI.Model;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MyAPI.Service
{
    #region << 版 本 注 释 >>
    //----------------------------------------------------------------
    // Copyright © 于国庆
    // 版权所有。
    //
    // 文件名：TouristRouteService
    // 文件功能描述：
    //
    //CLR版本：4.0.30319.42000
    //
    // 创建者：于国庆
    // QQ: 714024961
    // 时间：2021/8/2 16:50:03
    //
    // 修改人：
    // 时间：
    // 修改说明：
    //
    // 版本：V1.0.0
    //----------------------------------------------------------------
    #endregion

    public class TouristRouteService : ITouristRouteService
    {
        private readonly ITouristRouteRepository _touristRouteRepository;

        public TouristRouteService(ITouristRouteRepository touristRouteRepository)
        {
            this._touristRouteRepository = touristRouteRepository;
        }
        public List<TouristRoute> GetAllTouristRoutesAsync()
        {
            return _touristRouteRepository.ToList(c=>true);
        } 
    }
}
