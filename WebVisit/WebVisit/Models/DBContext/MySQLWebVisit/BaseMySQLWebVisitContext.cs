using Microsoft.EntityFrameworkCore;

namespace WebVisit.Models.DomainModels.MySQL
{
    public partial class BaseMySQLWebVisitContext : DbContext
    {
        public BaseMySQLWebVisitContext()
        {
        }

        public BaseMySQLWebVisitContext(DbContextOptions options) : base(options)
        {
            Console.WriteLine("init BaseMySQLWebVisitContext");
        }

        public DbSet<TUserAdd> TUserAdds { get; set; }

        // protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        //     => optionsBuilder.UseMySql("name=ConnectionStrings:CzMySQLWebVisitContext", Microsoft.EntityFrameworkCore.ServerVersion.Parse("10.4.22-mariadb"));

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder
                .UseCollation("utf8mb4_general_ci")
                .HasCharSet("utf8mb4");

            modelBuilder.Entity<TUserAdd>(entity =>
            {
                entity.HasKey(e => e.Seq).HasName("PRIMARY");

                entity.Property(e => e.CellPhone).HasDefaultValueSql("''");
                entity.Property(e => e.ErrorFlag).HasDefaultValueSql("'0'");
                entity.Property(e => e.ExpireDate).HasDefaultValueSql("'0'");
                entity.Property(e => e.Rank).HasDefaultValueSql("''");
                entity.Property(e => e.TelNum).HasDefaultValueSql("''");
            });
            // OnModelCreatingPartial(modelBuilder);
        }
        // partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }

}