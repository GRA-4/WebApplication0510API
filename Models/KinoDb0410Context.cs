using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace WebApplicationKinoAPI0510.Models;

public partial class KinoDb0410Context : DbContext
{
    public KinoDb0410Context()
    {
    }

    public KinoDb0410Context(DbContextOptions<KinoDb0410Context> options)
        : base(options)
    {
    }

    public virtual DbSet<Comment> Comments { get; set; }

    public virtual DbSet<FaveList> FaveLists { get; set; }

    public virtual DbSet<Genre> Genres { get; set; }

    public virtual DbSet<GenreTitle> GenreTitles { get; set; }

    public virtual DbSet<Role> Roles { get; set; }

    public virtual DbSet<Title> Titles { get; set; }

    public virtual DbSet<User> Users { get; set; }

    public virtual DbSet<Vote> Votes { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=DESKTOP-T5P3GVP;Database=KinoDb0410;Trusted_Connection=True;TrustServerCertificate=True;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Comment>(entity =>
        {
            entity.ToTable("Comment");

            entity.Property(e => e.Date).HasColumnType("datetime");

            entity.HasOne(d => d.Title).WithMany(p => p.Comments)
                .HasForeignKey(d => d.TitleId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("FK_Comment_Title");

            entity.HasOne(d => d.User).WithMany(p => p.Comments)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("FK_Comment_User");
        });

        modelBuilder.Entity<FaveList>(entity =>
        {
            entity.ToTable("FaveList");

            entity.HasOne(d => d.Title).WithMany(p => p.FaveLists)
                .HasForeignKey(d => d.TitleId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("FK_FaveList_Title");

            entity.HasOne(d => d.User).WithMany(p => p.FaveLists)
                .HasForeignKey(d => d.UserId)
                .HasConstraintName("FK_FaveList_User");
        });

        modelBuilder.Entity<Genre>(entity =>
        {
            entity.ToTable("Genre");
        });

        modelBuilder.Entity<GenreTitle>(entity =>
        {
            entity.HasKey(e => new { e.GenreId, e.TitleId });

            entity.ToTable("Genre_Title");

            entity.Property(e => e.GenreId).ValueGeneratedOnAdd();
        });

        modelBuilder.Entity<Role>(entity =>
        {
            entity.ToTable("Role");
        });

        modelBuilder.Entity<Title>(entity =>
        {
            entity.ToTable("Title");

            entity.Property(e => e.TitleAdditionalName).HasColumnName("TItleAdditionalName");
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.ToTable("User");

            entity.HasOne(d => d.Role).WithMany(p => p.Users)
                .HasForeignKey(d => d.RoleId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("FK_User_Role");
        });

        modelBuilder.Entity<Vote>(entity =>
        {
            entity
                .HasNoKey()
                .ToTable("Vote");

            entity.HasOne(d => d.Title).WithMany()
                .HasForeignKey(d => d.TitleId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("FK_Vote_Title");

            entity.HasOne(d => d.User).WithMany()
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("FK_Vote_User");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
