using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
// using System.ComponentModel.DataAnnotations.Schema;
// using System.Data.Entity.ModelConfiguration.Configuration;

namespace WebVisit.Models
{
    internal class ConfigureHoliday : IEntityTypeConfiguration<Holiday>
    {
        public void Configure(EntityTypeBuilder<Holiday> entity)
        {
            entity.Property(b => b.IsManual).HasDefaultValue("N");
            entity.Property(b => b.IsDelete).HasDefaultValue("N");
            entity.Property(b => b.InsertDate).HasDefaultValueSql("getdate()");

            // seed initial data
            entity.HasData( new { HolidayID = 1, HolidayDate = "2023-01-01", HolidayName = "설날", InsertSabun="1"});
            entity.HasData( new { HolidayID = 2, HolidayDate = "2023-03-01", HolidayName = "삼일절", InsertSabun="1"});
            entity.HasData( new { HolidayID = 3, HolidayDate = "2023-05-05", HolidayName = "어린이날", InsertSabun="1"});
        }
    }
}