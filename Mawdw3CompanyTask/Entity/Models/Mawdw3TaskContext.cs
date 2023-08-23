using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace Entity.Models;

public partial class Mawdw3TaskContext : DbContext
{
    public Mawdw3TaskContext()
    {
    }

    public Mawdw3TaskContext(DbContextOptions<Mawdw3TaskContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Grade> Grades { get; set; }

    public virtual DbSet<Student> Students { get; set; }
    public virtual DbSet<User> Users { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=localhost;Database=Mawdw3Task;Trusted_Connection=True;Encrypt=false");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Grade>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Grades__3213E83F5081435B");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Grade1).HasColumnName("grade");
            entity.Property(e => e.Name)
                .HasMaxLength(45)
                .IsUnicode(false)
                .HasColumnName("name");
        });

        modelBuilder.Entity<Student>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Students__3213E83F91AF3A98");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.FirstName)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("firstName");
            entity.Property(e => e.GradeId).HasColumnName("gradeID");
            entity.Property(e => e.LastName)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("lastName");

            //entity.HasOne(d => d.Grade).WithMany(p => p.Students)
            //    .HasForeignKey(d => d.GradeId)
            //    .HasConstraintName("FK__Students__gradeI__2C3393D0");
        });
        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Students__3213E83F91AF3A98");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Name)
                .HasMaxLength(45)
                .IsUnicode(false)
                .HasColumnName("name");
            entity.Property(e => e.Email)
                .HasMaxLength(45)
                .IsUnicode(false)
                .HasColumnName("email");
            entity.Property(e => e.password)
                .HasMaxLength(45)
                .IsUnicode(false)
                .HasColumnName("password");

        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
