using Microsoft.EntityFrameworkCore;

namespace WebVisit.Models.DomainModels.LPR
{
    public class DBLPRContext: BaseLPRContext
    {
        public DBLPRContext(DbContextOptions<DBLPRContext> options) : base(options)
        {
            Console.WriteLine("init DBLPRContext");
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            Console.WriteLine("OnModelCreating");
        }
    }
}