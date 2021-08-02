using Microsoft.EntityFrameworkCore;
using MyAPI.Model;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Text;

namespace MyAPI.DataBase
{
    #region << 版 本 注 释 >>
    //----------------------------------------------------------------
    // Copyright © 于国庆
    // 版权所有。
    //
    // 文件名：AppDBContext
    // 文件功能描述：
    //
    //CLR版本：4.0.30319.42000
    //
    // 创建者：于国庆
    // QQ: 714024961
    // 时间：2021/8/2 13:50:01
    //
    // 修改人：
    // 时间：
    // 修改说明：
    //
    // 版本：V1.0.0
    //----------------------------------------------------------------
    #endregion

    public class AppDBContext : DbContext
    {
        public AppDBContext(DbContextOptions<AppDBContext> options) : base(options)
        {

        }
        #region DbSet<Model>
        public DbSet<TouristRoute> TouristRoutes { get; set; }
        public DbSet<TouristRoutePicture> TouristRoutePictures { get; set; }
        #endregion
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            #region FluentAPI
            modelBuilder.Entity<TouristRoute>(entity =>
            {
                entity.HasKey(e => e.Id)
                    .HasName("PRIMARY");

                entity.ToTable("TouristRoute");

                entity.HasComment("旅游路线表");

                entity.Property(e => e.Id)
                    .HasColumnName("Id")
                    .HasColumnType("char(36)")
                    .HasComment("ID")
                    .ValueGeneratedNever();

                entity.Property(e => e.Title)
                    .IsRequired()
                    .HasColumnName("Title")
                    .HasColumnType("varchar(100)")
                    .HasComment("路线名称")
                    .HasMaxLength(100)
                    .HasCharSet("utf8mb4")
                    .UseCollation("utf8mb4_general_ci");


                entity.Property(e => e.Description)
                    .HasColumnName("Description")
                    .HasColumnType("varchar(1500)")
                    .HasComment("路线简介")
                    .HasMaxLength(1500)
                    .HasCharSet("utf8mb4")
                    .UseCollation("utf8mb4_general_ci");

                entity.Property(e => e.OriginalPrice)
                    .HasColumnName("OriginalPrice")
                    .HasColumnType("decimal(18,2)")
                    .HasComment("原始价格");

                entity.Property(e => e.DiscountPresent)
                    .HasColumnName("DiscountPresent")
                    .HasColumnType("double")
                    .HasComment("折扣率");

                entity.Property(e => e.CreateTime)
                    .HasColumnName("CreateTime")
                    .HasColumnType("datetime(6)")
                    .HasComment("创建日期");

                entity.Property(e => e.UpdateTime)
                    .HasColumnName("UpdateTime")
                    .HasColumnType("datetime(6)")
                    .HasComment("修改日期");

                entity.Property(e => e.Features)
                    .HasColumnName("Features")
                    .HasColumnType("longtext")
                    .HasComment("特色")
                    .HasCharSet("utf8mb4")
                    .UseCollation("utf8mb4_general_ci");

                entity.Property(e => e.Fees)
                    .HasColumnName("Fees")
                    .HasColumnType("longtext")
                    .HasComment("费用")
                    .HasCharSet("utf8mb4")
                    .UseCollation("utf8mb4_general_ci");

                entity.Property(e => e.Notes)
                    .HasColumnName("Notes")
                    .HasColumnType("longtext")
                    .HasComment("备注")
                    .HasCharSet("utf8mb4")
                    .UseCollation("utf8mb4_general_ci");

            });

            modelBuilder.Entity<TouristRoutePicture>(entity =>
            {
                entity.HasKey(e => e.Id)
                .HasName("PRIMARY");
                
                entity.ToTable("TouristRoutePicture");

                entity.HasComment("旅游路线图片表");

                entity.HasOne(p => p.TouristRoute)
                      .WithMany(r => r.TouristRoutePictures)
                      .HasForeignKey(p => p.TouristRouteId);

                entity.Property(e => e.Id)
                    .HasColumnName("Id")
                    .HasColumnType("int")
                    .ValueGeneratedOnAdd()
                    .HasComment("旅游路线图片ID");
                    

                entity.Property(e => e.Url)
                    .HasColumnName("Url")
                    .HasColumnType("varchar(100)")
                    .HasMaxLength(100)
                    .HasComment("URL地址")
                    .HasCharSet("utf8mb4")
                    .UseCollation("utf8mb4_general_ci");

                entity.Property(e => e.TouristRouteId)
                    .HasComment("旅游路线外键");
            });
            #endregion
            var path = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);

            var touristRouteJsonData = File.ReadAllText(path + @"touristRoutesMockData.json");
            var touristRoutes = JsonConvert.DeserializeObject(touristRouteJsonData);

            var touristRoutePictureJsonData = File.ReadAllText(path + @"touristRoutePicturesMockData.json");
            var touristRoutePictures = JsonConvert.DeserializeObject(touristRoutePictureJsonData);
            base.OnModelCreating(modelBuilder);
        }
    }
}
