using Microsoft.EntityFrameworkCore;

namespace WebVisit.Models
{
    public class DBS1ACCESSContext: BaseS1ACCESSContext
    {
        public DBS1ACCESSContext(DbContextOptions<DBS1ACCESSContext> options) : base(options)
        {
            Console.WriteLine("init DBS1ACCESSContext");
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            Console.WriteLine("OnModelCreating");
        }
    }
}