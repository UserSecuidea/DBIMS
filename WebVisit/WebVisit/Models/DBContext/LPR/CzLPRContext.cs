using Microsoft.EntityFrameworkCore;

namespace WebVisit.Models.DomainModels.LPR
{
    public class CzLPRContext: BaseLPRContext
    {
        public CzLPRContext(DbContextOptions<CzLPRContext> options) : base(options)
        {
            Console.WriteLine("init CzLPRContext");
        }
        // protected override void OnModelCreating(ModelBuilder modelBuilder)
        // {
        //     Console.WriteLine("OnModelCreating");
        // }
    }
}