using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
// using System.ComponentModel.DataAnnotations.Schema;
// using System.Data.Entity.ModelConfiguration.Configuration;

namespace WebVisit.Models
{
    internal class ConfigureLevel : IEntityTypeConfiguration<Level>
    {
        public void Configure(EntityTypeBuilder<Level> entity)
        {
            entity.Property(b => b.IsDelete).HasDefaultValue("N");
            entity.Property(b => b.InsertDate).HasDefaultValueSql("getdate()");

            // seed initial data
            entity.HasData(new { LevelID = 1, LevelName = "마스터관리자", InsertSabun="1"});
            entity.HasData(new { LevelID = 2, LevelName = "일반관리자", InsertSabun="1"});
            entity.HasData(new { LevelID = 3, LevelName = "임직원(일반)", InsertSabun="1"});
            entity.HasData(new { LevelID = 4, LevelName = "임직원(인사)", InsertSabun="1"});
            entity.HasData(new { LevelID = 5, LevelName = "임직원(ESH)", InsertSabun="1"});
            entity.HasData(new { LevelID = 6, LevelName = "임직원(영양사)", InsertSabun="1"});
            entity.HasData(new { LevelID = 7, LevelName = "비상주", InsertSabun="1"});
            entity.HasData(new { LevelID = 8, LevelName = "보안실", InsertSabun="1"});
            entity.HasData(new { LevelID = 9, LevelName = "비상주업체", InsertSabun="1"});
            //2023.09.19 구매 레벨 추가 
            entity.HasData(new { LevelID = 10, LevelName = "임직원(구매)", InsertSabun="1"});
        }
    }
}