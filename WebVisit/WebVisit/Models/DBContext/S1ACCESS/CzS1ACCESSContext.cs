using Microsoft.EntityFrameworkCore;

namespace WebVisit.Models
{
    public class CzS1ACCESSContext: BaseS1ACCESSContext
    {
        public CzS1ACCESSContext(DbContextOptions<CzS1ACCESSContext> options) : base(options)
        {
            Console.WriteLine("init CzS1ACCESSContext");
        }
        // protected override void OnModelCreating(ModelBuilder modelBuilder)
        // {
        //     Console.WriteLine("OnModelCreating");
        // }
    }
}