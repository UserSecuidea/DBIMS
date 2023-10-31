using Microsoft.EntityFrameworkCore;

namespace WebVisit.Models.DomainModels.MySQL
{
    public class DBMySQLWebVisitDevContext: BaseMySQLWebVisitContext
    {
        public DBMySQLWebVisitDevContext(){}
        public DBMySQLWebVisitDevContext(DbContextOptions<DBMySQLWebVisitDevContext> options) : base(options)
        {
            Console.WriteLine("init DBMySQLWebVisitDevContext");
        }
        // protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder){
        //     optionsBuilder.UseMySql("name=ConnectionStrings:DBMySQLWebVisitDevContext", Microsoft.EntityFrameworkCore.ServerVersion.Parse("10.4.22-mariadb"));
        // }
    }
}