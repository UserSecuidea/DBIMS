using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
// using System.ComponentModel.DataAnnotations.Schema;
// using System.Data.Entity.ModelConfiguration.Configuration;

namespace WebVisit.Models
{
    internal class ConfigurePerson : IEntityTypeConfiguration<Person>
    {
        public void Configure(EntityTypeBuilder<Person> entity)
        {
            //IsForeigner PersonStatusID Gender
            entity.Property(b => b.Gender).HasDefaultValue(0); // [CommonCode] 성별: 남성(0)/여성(1)
            entity.Property(b => b.PersonStatusID).HasDefaultValue(0); // [CommonCode] 재직상태: 재직(0)/휴직(1)/퇴직(2)
            entity.Property(b => b.IsForeigner).HasDefaultValue(1); // [CommonCode] 내/외국인여부: 내국인(1)/외국인(2)

            //IsAllowSMS
            entity.Property(b => b.IsAllowSMS).HasDefaultValue("N");
            entity.Property(b => b.IsDelete).HasDefaultValue("N");
            entity.Property(b => b.InsertDate).HasDefaultValueSql("getdate()");

            // seed initial data            
            entity.HasData(
                new { PersonID = 1, Sabun = "master", CompanyID=0, LevelCodeID = 1, Password ="123456".SHA256Encrypt(), Name = "마스터관리자", InsertSabun="1", OrgNameKor="마스터", OrgNameEng="master"}
            );
        }
    }
}