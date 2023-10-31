using Microsoft.EntityFrameworkCore;

namespace WebVisit.Models
{
    public class DBS1ACCESSDevContext : BaseS1ACCESSContext
    {
        public DBS1ACCESSDevContext(DbContextOptions<DBS1ACCESSDevContext> options) : base(options)
        {
            Console.WriteLine("init DBS1ACCESSDevContext");
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            Console.WriteLine("OnModelCreating");
        }
    }
}