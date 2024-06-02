using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace TestWebApi.Models;

public partial class MonitorDataBaseContext : DbContext
{
    public MonitorDataBaseContext()
    {
    }

    public MonitorDataBaseContext(DbContextOptions<MonitorDataBaseContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Config> Configs { get; set; }

    public virtual DbSet<User> Users { get; set; }

    public virtual DbSet<UserRecord> UserRecords { get; set; }

    public virtual DbSet<Video> Videos { get; set; }

    public virtual DbSet<VideoRecord> VideoRecords { get; set; }

    public virtual DbSet<VideoType> VideoTypes { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=localhost,1433;Database=MonitorDataBase;User Id=sa;Password=Bohan233/;TrustServerCertificate=True;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Config>(entity =>
        {
            entity.HasKey(e => e.Key);
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.Uid);

            entity.HasIndex(e => e.MostRecentUserRecordId, "IX_Users_MostRecentUserRecordId")
                .IsUnique()
                .HasFilter("([MostRecentUserRecordId] IS NOT NULL)");

            entity.Property(e => e.Uid).ValueGeneratedNever();

            entity.HasOne(d => d.MostRecentUserRecord).WithOne(p => p.User).HasForeignKey<User>(d => d.MostRecentUserRecordId);
        });

        modelBuilder.Entity<UserRecord>(entity =>
        {
            entity.HasKey(e => e.UrId);

            entity.HasIndex(e => e.Uid, "IX_UserRecords_Uid");

            entity.HasOne(d => d.UidNavigation).WithMany(p => p.UserRecords).HasForeignKey(d => d.Uid);
        });

        modelBuilder.Entity<Video>(entity =>
        {
            entity.HasKey(e => e.BvId);

            entity.HasIndex(e => e.AuthorUid, "IX_Videos_AuthorUid");

            entity.HasIndex(e => e.MostRecentVideoRecordId, "IX_Videos_MostRecentVideoRecordId")
                .IsUnique()
                .HasFilter("([MostRecentVideoRecordId] IS NOT NULL)");

            entity.HasIndex(e => e.TypeId, "IX_Videos_TypeId");

            entity.HasIndex(e => e.UserUid, "IX_Videos_UserUid");

            entity.HasOne(d => d.AuthorU).WithMany(p => p.VideoAuthorUs)
                .HasForeignKey(d => d.AuthorUid)
                .OnDelete(DeleteBehavior.ClientSetNull);

            entity.HasOne(d => d.MostRecentVideoRecord).WithOne(p => p.Video).HasForeignKey<Video>(d => d.MostRecentVideoRecordId);

            entity.HasOne(d => d.Type).WithMany(p => p.Videos).HasForeignKey(d => d.TypeId);

            entity.HasOne(d => d.UserU).WithMany(p => p.VideoUserUs).HasForeignKey(d => d.UserUid);
        });

        modelBuilder.Entity<VideoRecord>(entity =>
        {
            entity.HasKey(e => e.VrId);

            entity.HasIndex(e => e.BvId, "IX_VideoRecords_BvId");

            entity.HasOne(d => d.Bv).WithMany(p => p.VideoRecords).HasForeignKey(d => d.BvId);
        });

        modelBuilder.Entity<VideoType>(entity =>
        {
            entity.HasKey(e => e.TypeId);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
