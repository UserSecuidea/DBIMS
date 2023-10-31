using Microsoft.EntityFrameworkCore;

namespace WebVisit.Models.DomainModels.Passport
{
    public class CzPASSPORTContext: BasePASSPORTContext
    {
        public CzPASSPORTContext(DbContextOptions<CzPASSPORTContext> options) : base(options)
        {
            Console.WriteLine("init CzPASSPORTContext");
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            Console.WriteLine("OnModelCreating");
        }
    }
}