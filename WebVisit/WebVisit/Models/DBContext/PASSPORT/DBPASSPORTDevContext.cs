using Microsoft.EntityFrameworkCore;

namespace WebVisit.Models.DomainModels.Passport
{
    public class DBPASSPORTDevContext: BasePASSPORTContext
    {
        public DBPASSPORTDevContext(DbContextOptions<DBPASSPORTDevContext> options) : base(options)
        {
            Console.WriteLine("init DBPASSPORTDevContext");
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            Console.WriteLine("OnModelCreating");
        }
    }
}