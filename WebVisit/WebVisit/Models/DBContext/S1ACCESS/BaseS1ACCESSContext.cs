using Microsoft.EntityFrameworkCore;

namespace WebVisit.Models
{
    public class BaseS1ACCESSContext : DbContext
    {
        public BaseS1ACCESSContext(DbContextOptions options) : base(options)
        {
            Console.WriteLine("init BaseS1ACCESSContext");
        }
    }
}