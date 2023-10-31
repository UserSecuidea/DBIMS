using Microsoft.EntityFrameworkCore;

namespace WebVisit.Models.DomainModels.Passport
{
    public class DBPASSPORTContext: BasePASSPORTContext
    {
        public DBPASSPORTContext(DbContextOptions<DBPASSPORTContext> options) : base(options)
        {
            Console.WriteLine("init DBPASSPORTContext");
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            Console.WriteLine("OnModelCreating");
        }
    }
}