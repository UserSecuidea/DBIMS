using Microsoft.EntityFrameworkCore;

namespace WebVisit.Models
{
    public class S1ACCESSContext: BaseS1ACCESSContext
    {
        public S1ACCESSContext(DbContextOptions<S1ACCESSContext> options) : base(options)
        {
            Console.WriteLine("init S1ACCESSContext");
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            Console.WriteLine("OnModelCreating");
        }
    }
}