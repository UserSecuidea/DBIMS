using Microsoft.EntityFrameworkCore;

namespace WebVisit.Models.DomainModels.LPR
{
    public class DBLPRDevContext: BaseLPRContext
    {
        public DBLPRDevContext(DbContextOptions<DBLPRDevContext> options) : base(options)
        {
            Console.WriteLine("init DBLPRDevContext");
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            Console.WriteLine("OnModelCreating");
        }
    }
}