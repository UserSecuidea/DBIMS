using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
// using System.ComponentModel.DataAnnotations.Schema;
// using System.Data.Entity.ModelConfiguration.Configuration;

namespace WebVisit.Models
{
    internal class ConfigureMealSchedule : IEntityTypeConfiguration<MealSchedule>
    {
        public void Configure(EntityTypeBuilder<MealSchedule> entity)
        {
            entity.Property(b => b.IsDelete).HasDefaultValue("N");
            entity.Property(b => b.InsertDate).HasDefaultValueSql("getdate()");
            
            // seed initial data
            entity.HasData(new { MealScheduleID = 1, Term = 0, Location = "2000", MealGB = 1, StartTime ="0530", EndTime ="0900", UpdateIP="", InsertSabun="1"});
            entity.HasData(new { MealScheduleID = 2, Term = 0, Location = "2000", MealGB = 2, StartTime ="1130", EndTime ="1400", UpdateIP="", InsertSabun="1"});
            entity.HasData(new { MealScheduleID = 3, Term = 0, Location = "2000", MealGB = 3, StartTime ="1630", EndTime ="1900", UpdateIP="", InsertSabun="1"});
            entity.HasData(new { MealScheduleID = 4, Term = 0, Location = "2000", MealGB = 4, StartTime ="2130", EndTime ="2330", UpdateIP="", InsertSabun="1"});
            entity.HasData(new { MealScheduleID = 5, Term = 1, Location = "2000", MealGB = 1, StartTime ="0530", EndTime ="0900", UpdateIP="", InsertSabun="1"});
            entity.HasData(new { MealScheduleID = 6, Term = 1, Location = "2000", MealGB = 2, StartTime ="1130", EndTime ="1400", UpdateIP="", InsertSabun="1"});
            entity.HasData(new { MealScheduleID = 7, Term = 1, Location = "2000", MealGB = 3, StartTime ="1630", EndTime ="1900", UpdateIP="", InsertSabun="1"});
            entity.HasData(new { MealScheduleID = 8, Term = 1, Location = "2000", MealGB = 4, StartTime ="2130", EndTime ="2330", UpdateIP="", InsertSabun="1"});
            
        }
    }
}