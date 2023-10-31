using Microsoft.EntityFrameworkCore;

namespace WebVisit.Models.DomainModels.MySQL
{
    public class DBMySQLWebVisitContext: BaseMySQLWebVisitContext
    {
        public DBMySQLWebVisitContext() { }
        public DBMySQLWebVisitContext(DbContextOptions<DBMySQLWebVisitContext> options) : base(options)
        {
            Console.WriteLine("init DBMySQLWebVisitContext");
        }
        // protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder){
        //     optionsBuilder.UseMySql("name=ConnectionStrings:DBMySQLWebVisitContext", Microsoft.EntityFrameworkCore.ServerVersion.Parse("10.4.22-mariadb"));
        // }
    }
}