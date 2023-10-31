using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace WebVisit.Models.DomainModels.MySQL;

public partial class DbnIpplusContext : DbContext
{
    public DbnIpplusContext()
    {
    }

    public DbnIpplusContext(DbContextOptions<DbnIpplusContext> options)
        : base(options)
    {
    }

    public virtual DbSet<TUserAdd> TUserAdds { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        => optionsBuilder.UseMySql("name=ConnectionStrings:DBMySQLWebVisitDevContext", Microsoft.EntityFrameworkCore.ServerVersion.Parse("10.4.22-mariadb"));

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

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
