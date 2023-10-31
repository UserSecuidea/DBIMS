using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace WebVisit.Models.DomainModels.LPR;

public partial class InnoparkingContext : DbContext
{
    public InnoparkingContext()
    {
    }

    public InnoparkingContext(DbContextOptions<InnoparkingContext> options)
        : base(options)
    {
    }

    public virtual DbSet<VMemberIntegration> VMemberIntegrations { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        => optionsBuilder.UseSqlServer("Name=ConnectionStrings:DBLPRDevContext");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<VMemberIntegration>(entity =>
        {
            entity.ToView("vMemberIntegrations");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
