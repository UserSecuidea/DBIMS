using Microsoft.EntityFrameworkCore;

namespace WebVisit.Models.DomainModels.LPR
{
    public class BaseLPRContext : DbContext
    {
        public BaseLPRContext(DbContextOptions options) : base(options)
        {
            Console.WriteLine("init BaseLPRContext");
        }
        public DateTime Convert<T>(T a, string prm2) => throw new InvalidOperationException();
        // VMemberIntegration
        public DbSet<VMemberIntegration> VMemberIntegrations { get; set; } = null!;
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<VMemberIntegration>(entity =>
            {
                entity.ToView("vMemberIntegrations");
            });

        }        
    }
}