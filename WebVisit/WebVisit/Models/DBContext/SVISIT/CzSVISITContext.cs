using Microsoft.EntityFrameworkCore;

namespace WebVisit.Models
{
    public class CzSVISITContext : BaseSVISITContext {
        public CzSVISITContext(DbContextOptions<CzSVISITContext> options): base(options) {
            Console.WriteLine("init CzSVISITContext");
        }
    }
}