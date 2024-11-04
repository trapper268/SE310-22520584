using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace ThucHanhWebMVC.Models;

public partial class MasterContext : DbContext
{
    public MasterContext()
    {
    }

    public MasterContext(DbContextOptions<MasterContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Trangtrai> Trangtrais { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Data Source=LAPTOP-HJSRG3SJ\\SQLEXPRESS;Integrated Security=True;Connect Timeout=30;Encrypt=False;Trust Server Certificate=False;Application Intent=ReadWrite;Multi Subnet Failover=False");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Trangtrai>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__TRANGTRA__3214EC279AFD666A");

            entity.ToTable("TRANGTRAI");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.Loai)
                .HasMaxLength(40)
                .HasColumnName("LOAI");
            entity.Property(e => e.Sua).HasColumnName("SUA");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
