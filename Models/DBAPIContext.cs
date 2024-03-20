using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace StudyApp.Models
{
    public partial class DBAPIContext : DbContext
    {
        public DBAPIContext()
        {
        }

        public DBAPIContext(DbContextOptions<DBAPIContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Categoria> Categoria { get; set; } = null!;
        public virtual DbSet<Course> Courses { get; set; } = null!;
        public virtual DbSet<User> Users { get; set; } = null!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Categoria>(entity =>
            {
                entity.HasKey(e => e.IdCategoria)
                    .HasName("PK__CATEGORI__A3C02A10AA357E77");

                entity.ToTable("CATEGORIA");

                entity.Property(e => e.Descripcion)
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Course>(entity =>
            {
                entity.HasKey(e => e.IdCourse)
                    .HasName("PK__COURSES__E0B50B812BF7766D");

                entity.ToTable("COURSES");

                entity.Property(e => e.Descripcion)
                    .HasMaxLength(80)
                    .IsUnicode(false);

                entity.Property(e => e.Duracion)
                    .HasMaxLength(30)
                    .IsUnicode(false)
                    .HasColumnName("duracion");

                entity.Property(e => e.Nombre)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Precio).HasColumnType("decimal(10, 2)");

                entity.HasOne(d => d.oCategoria)
                    .WithMany(p => p.Courses)
                    .HasForeignKey(d => d.IdCategoria)
                    .HasConstraintName("FK_IDCATEGORIA");
            });

            modelBuilder.Entity<User>(entity =>
            {
                entity.HasKey(e => e.IdUser)
                    .HasName("PK__USERS__3717C98277FAEED9");

                entity.ToTable("USERS");

                entity.Property(e => e.IdUser).HasColumnName("idUser");

                entity.Property(e => e.Apellido)
                    .HasMaxLength(30)
                    .IsUnicode(false)
                    .HasColumnName("apellido");

                entity.Property(e => e.Email)
                    .HasMaxLength(80)
                    .IsUnicode(false)
                    .HasColumnName("email");

                entity.Property(e => e.Nombre)
                    .HasMaxLength(30)
                    .IsUnicode(false)
                    .HasColumnName("nombre");

                entity.Property(e => e.Pass)
                    .HasMaxLength(25)
                    .IsUnicode(false)
                    .HasColumnName("pass");

                entity.Property(e => e.Rol)
                    .HasMaxLength(15)
                    .IsUnicode(false)
                    .HasColumnName("rol");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
