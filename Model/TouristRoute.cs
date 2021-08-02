using MyAPI.Enum.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
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
    // 时间：2021/8/2 11:50:03
    //
    // 修改人：
    // 时间：
    // 修改说明：
    //
    // 版本：V1.0.0
    //----------------------------------------------------------------
    #endregion

    /// <summary>
    /// 旅游路线
    /// </summary>
    public class TouristRoute
    {
        /// <summary>
        /// ID
        /// </summary>
        public Guid Id { get; set; }
        /// <summary>
        /// 标题
        /// </summary>
        public string Title { get; set; }
        /// <summary>
        /// 简介
        /// </summary>
        public string Description { get; set; }
        /// <summary>
        /// 价格
        /// </summary>
        public decimal OriginalPrice { get; set; }
        /// <summary>
        /// 折扣率
        /// </summary>
        [Range(0.0,1.0)]
        public double? DiscountPresent { get; set; }
        /// <summary>
        /// 创建日期
        /// </summary>
        public DateTime CreateTime { get; set; }
        /// <summary>
        /// 修改日期
        /// </summary>
        public DateTime? UpdateTime { get; set; }
        /// <summary>
        /// 出发日期
        /// </summary>
        public DateTime? DepartureTime { get; set; }
        /// <summary>
        /// 特色
        /// </summary>
        public string Features { get; set; }
        /// <summary>
        /// 费用
        /// </summary>
        public string Fees { get; set; }
        /// <summary>
        /// 备注
        /// </summary>
        public string Notes { get; set; }
        /// <summary>
        /// 图片
        /// </summary>
        public ICollection<TouristRoutePicture> TouristRoutePictures { get; set; }
        = new List<TouristRoutePicture>();
        /// <summary>
        /// 评分
        /// </summary>
        public double? Rating { get; set; }
        /// <summary>
        /// 旅行天数
        /// </summary>
        public TravelDays TravelDays { get; set; }
        /// <summary>
        /// 类型
        /// </summary>
        public TripType TripType { get; set; }
        /// <summary>
        /// 出发点
        /// </summary>
        public DepartureCity? DepartureCity { get; set; }
    }
}
