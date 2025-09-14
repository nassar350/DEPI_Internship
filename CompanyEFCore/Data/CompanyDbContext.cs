using System;
using System.Collections.Generic;
using CompanyEFCore.Models;
using Microsoft.EntityFrameworkCore;

namespace CompanyEFCore.Data;

public partial class CompanyDbContext : DbContext
{
    public CompanyDbContext()
    {
    }

    public CompanyDbContext(DbContextOptions<CompanyDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Department> Departments { get; set; }

    public virtual DbSet<Dependent> Dependents { get; set; }

    public virtual DbSet<Employee> Employees { get; set; }

    public virtual DbSet<Management> Managements { get; set; }

    public virtual DbSet<Project> Projects { get; set; }

    public virtual DbSet<WorkHour> WorkHours { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=localhost\\SQLEXPRESS;Database=Company;Trusted_Connection=True;TrustServerCertificate=true;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Department>(entity =>
        {
            entity.HasKey(e => e.Dnum).HasName("PK__Departme__24BE6B2FBDCE1F45");

            entity.ToTable("Department");

            entity.Property(e => e.Dnum).HasColumnName("DNum");
            entity.Property(e => e.Dname)
                .HasMaxLength(255)
                .HasColumnName("DName");
            entity.Property(e => e.Location).HasMaxLength(300);
        });

        modelBuilder.Entity<Dependent>(entity =>
        {
            entity.HasKey(e => e.Dname).HasName("PK__Dependen__48779E0A5673C768");

            entity.ToTable("Dependent");

            entity.Property(e => e.Dname)
                .HasMaxLength(255)
                .HasColumnName("DName");
            entity.Property(e => e.Essn).HasColumnName("ESSN");
            entity.Property(e => e.Gender)
                .HasMaxLength(1)
                .IsUnicode(false)
                .IsFixedLength();

            entity.HasOne(d => d.EssnNavigation).WithMany(p => p.Dependents)
                .HasForeignKey(d => d.Essn)
                .HasConstraintName("FK__Dependent__ESSN__534D60F1");
        });

        modelBuilder.Entity<Employee>(entity =>
        {
            entity.HasKey(e => e.Ssn).HasName("PK__Employee__CA1E8E3D4B23EE0A");

            entity.ToTable("Employee");

            entity.Property(e => e.Ssn).HasColumnName("SSN");
            entity.Property(e => e.Dnum).HasColumnName("DNum");
            entity.Property(e => e.Fname).HasMaxLength(255);
            entity.Property(e => e.Gender)
                .HasMaxLength(1)
                .IsUnicode(false)
                .IsFixedLength();
            entity.Property(e => e.Lname).HasMaxLength(255);
            entity.Property(e => e.Mssn).HasColumnName("MSSN");

            entity.HasOne(d => d.DnumNavigation).WithMany(p => p.Employees)
                .HasForeignKey(d => d.Dnum)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Employee__DNum__5CD6CB2B");

            entity.HasOne(d => d.MssnNavigation).WithMany(p => p.InverseMssnNavigation)
                .HasForeignKey(d => d.Mssn)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Employee__MSSN__4AB81AF0");
        });

        modelBuilder.Entity<Management>(entity =>
        {
            entity.HasKey(e => new { e.Essn, e.Dnum }).HasName("PK__Manageme__C145795EAA569786");

            entity.ToTable("Management");

            entity.Property(e => e.Essn).HasColumnName("ESSN");
            entity.Property(e => e.Dnum).HasColumnName("DNum");
            entity.Property(e => e.HireDate).HasColumnName("Hire_date");

            entity.HasOne(d => d.DnumNavigation).WithMany(p => p.Managements)
                .HasForeignKey(d => d.Dnum)
                .HasConstraintName("FK__Management__DNum__5BE2A6F2");

            entity.HasOne(d => d.EssnNavigation).WithMany(p => p.Managements)
                .HasForeignKey(d => d.Essn)
                .HasConstraintName("FK__Management__ESSN__5AEE82B9");
        });

        modelBuilder.Entity<Project>(entity =>
        {
            entity.HasKey(e => e.Pnumber).HasName("PK__Project__DDE0878D678F531D");

            entity.ToTable("Project");

            entity.Property(e => e.Pnumber)
                .ValueGeneratedNever()
                .HasColumnName("PNumber");
            entity.Property(e => e.City).HasMaxLength(100);
            entity.Property(e => e.Dnum).HasColumnName("DNum");
            entity.Property(e => e.Pname)
                .HasMaxLength(255)
                .HasColumnName("PName");

            entity.HasOne(d => d.DnumNavigation).WithMany(p => p.Projects)
                .HasForeignKey(d => d.Dnum)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Project__DNum__4F7CD00D");
        });

        modelBuilder.Entity<WorkHour>(entity =>
        {
            entity.HasKey(e => new { e.Essn, e.Pnumber }).HasName("PK__Work_Hou__EED097940C2D2DE6");

            entity.ToTable("Work_Hours");

            entity.Property(e => e.Essn).HasColumnName("ESSN");
            entity.Property(e => e.Pnumber).HasColumnName("PNumber");
            entity.Property(e => e.WorkingHours).HasColumnName("Working_hours");

            entity.HasOne(d => d.EssnNavigation).WithMany(p => p.WorkHours)
                .HasForeignKey(d => d.Essn)
                .HasConstraintName("FK__Work_Hours__ESSN__571DF1D5");

            entity.HasOne(d => d.PnumberNavigation).WithMany(p => p.WorkHours)
                .HasForeignKey(d => d.Pnumber)
                .HasConstraintName("FK__Work_Hour__PNumb__5812160E");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
