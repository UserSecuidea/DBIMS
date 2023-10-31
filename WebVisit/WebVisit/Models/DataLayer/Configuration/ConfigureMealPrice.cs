using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
// using System.ComponentModel.DataAnnotations.Schema;
// using System.Data.Entity.ModelConfiguration.Configuration;

namespace WebVisit.Models
{
    internal class ConfigureMealPrice : IEntityTypeConfiguration<MealPrice>
    {
        public void Configure(EntityTypeBuilder<MealPrice> entity)
        {
            entity.Property(b => b.IsDelete).HasDefaultValue("N");
            entity.Property(b => b.InsertDate).HasDefaultValueSql("getdate()");
            
            // seed initial data
            entity.HasData(new { MealPriceID = 1, Location = "2000", Price =3500, InsertSabun="1"});
            entity.HasData(new { MealPriceID = 2, Location = "3000", Price =3500, InsertSabun="1"});
            entity.HasData(new { MealPriceID = 3, Location = "6000", Price =4000, InsertSabun="1"});
            
        }
    }
}