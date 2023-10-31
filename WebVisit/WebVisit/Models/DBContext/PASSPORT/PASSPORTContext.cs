using Microsoft.EntityFrameworkCore;

namespace WebVisit.Models.DomainModels.Passport
{
    public class PASSPORTContext: BasePASSPORTContext
    {
        public PASSPORTContext(DbContextOptions<PASSPORTContext> options) : base(options)
        {
            Console.WriteLine("init PASSPORTContext");
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            Console.WriteLine("OnModelCreating");
        }
    }
}