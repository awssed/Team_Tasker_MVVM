using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using TeamTasker.Models;

namespace TeamTasker.EntityModels;

public partial class TeamTaskerContext : DbContext
{
    public DbSet<Developer> Developers { get; set; }
    public DbSet<Project> Projects { get; set; }
    public TeamTaskerContext()
    {
    }

    public TeamTaskerContext(DbContextOptions<TeamTaskerContext> options)
        : base(options)
    {
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=DESKTOP-RECMBIL;Database=TeamTasker;TrustServerCertificate=True;integrated security=True;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
