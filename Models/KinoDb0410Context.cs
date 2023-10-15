using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using WebApplicationKinoAPI0510.Models;

namespace WebApplicationKinoAPI0510;

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

    public virtual DbSet<Role> Roles { get; set; }

    public virtual DbSet<Title> Titles { get; set; }

    public virtual DbSet<User> Users { get; set; }

    public virtual DbSet<Vote> Votes { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder
            .UseLazyLoadingProxies()        // подключение lazy loading
            .UseSqlServer("Server=DESKTOP-T5P3GVP;Database=KinoDb0410;Trusted_Connection=True;TrustServerCertificate=True;");


    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Comment>(entity =>
        {
            entity.ToTable("Comment");

            entity.Property(e => e.Date)
                .HasDefaultValueSql("('01.01.2001')")
                .HasColumnType("datetime");
            entity.Property(e => e.TextContent).HasDefaultValueSql("('-')");

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
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("FK_FaveList_User");
        });

        modelBuilder.Entity<Genre>(entity =>
        {
            entity.ToTable("Genre");

            entity.Property(e => e.GenreName).HasDefaultValueSql("('-')");

            entity.HasMany(d => d.Titles).WithMany(p => p.Genres)
                .UsingEntity<Dictionary<string, object>>(
                    "GenreTitle",
                    r => r.HasOne<Title>().WithMany()
                        .HasForeignKey("TitleId")
                        .HasConstraintName("FK_Genre_Title_Title"),
                    l => l.HasOne<Genre>().WithMany()
                        .HasForeignKey("GenreId")
                        .HasConstraintName("FK_Genre_Title_Genre"),
                    j =>
                    {
                        j.HasKey("GenreId", "TitleId").HasName("PK__Genre_Ti__74D25DE6D3F00BC4");
                        j.ToTable("Genre_Title");
                    });
        });

        modelBuilder.Entity<Role>(entity =>
        {
            entity.ToTable("Role");

            entity.Property(e => e.RoleName).HasDefaultValueSql("('-')");
        });

        modelBuilder.Entity<Title>(entity =>
        {
            entity.ToTable("Title");

            entity.Property(e => e.Date).HasDefaultValueSql("((2020))");
            entity.Property(e => e.Description).HasDefaultValueSql("('-')");
            entity.Property(e => e.ImageUrl).HasDefaultValueSql("('-')");
            entity.Property(e => e.TitleAdditionalName)
                .HasDefaultValueSql("('-')")
                .HasColumnName("TItleAdditionalName");
            entity.Property(e => e.TitleName).HasDefaultValueSql("('-')");
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.ToTable("User");

            entity.Property(e => e.Email).HasDefaultValueSql("('-')");
            entity.Property(e => e.ImageUrl).HasDefaultValueSql("('https://i.pinimg.com/1200x/4f/9b/61/4f9b611e72d038a4e8176709de0913d4.jpg')");
            entity.Property(e => e.Password).HasDefaultValueSql("('-')");
            entity.Property(e => e.UserName).HasDefaultValueSql("('-')");

            entity.HasOne(d => d.Role).WithMany(p => p.Users)
                .HasForeignKey(d => d.RoleId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("FK_User_Role");
        });

        modelBuilder.Entity<Vote>(entity =>
        {
            entity.HasKey(e => new { e.UserId, e.TitleId });

            entity.ToTable("Vote");

            entity.Property(e => e.Rating).HasDefaultValueSql("((5))");

            entity.HasOne(d => d.Title).WithMany(p => p.Votes)
                .HasForeignKey(d => d.TitleId)
                .HasConstraintName("FK_Vote_Title");

            entity.HasOne(d => d.User).WithMany(p => p.Votes)
                .HasForeignKey(d => d.UserId)
                .HasConstraintName("FK_Vote_User");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
