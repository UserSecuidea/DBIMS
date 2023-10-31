using Microsoft.EntityFrameworkCore;

namespace WebVisit.Models.DomainModels.MySQL
{
    public class CzMySQLWebVisitContext: BaseMySQLWebVisitContext
    {
        public CzMySQLWebVisitContext(){}

        public CzMySQLWebVisitContext(DbContextOptions<CzMySQLWebVisitContext> options) : base(options)
        {
            Console.WriteLine("init CzMySQLWebVisitContext");
        }
        // protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder){
        //     optionsBuilder.UseMySql("name=ConnectionStrings:CzMySQLWebVisitContext", Microsoft.EntityFrameworkCore.ServerVersion.Parse("10.4.22-mariadb"));
        // }
    }
}