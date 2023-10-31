using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
// using System.ComponentModel.DataAnnotations.Schema;
// using System.Data.Entity.ModelConfiguration.Configuration;

namespace WebVisit.Models
{
    internal class ConfigureMealTerm : IEntityTypeConfiguration<MealTerm>
    {
        public void Configure(EntityTypeBuilder<MealTerm> entity)
        {
            entity.Property(b => b.IsDelete).HasDefaultValue("N");
            entity.Property(b => b.InsertDate).HasDefaultValueSql("getdate()");
            
            // seed initial data
            entity.HasData(new { MealTermID = 1, Term = 0, StartDate ="0401", EndDate ="0831", UpdateIP="", InsertSabun="1"});
            entity.HasData(new { MealTermID = 2, Term = 1, StartDate ="0901", EndDate ="0331", UpdateIP="", InsertSabun="1"});
            
        }
    }
}