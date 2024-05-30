using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Model;

namespace Lib.DataManagement;

public class MonitorDbContext : DbContext
{
    public DbSet<Video>       Videos       { get; set; } = null!;
    public DbSet<VideoRecord> VideoRecords { get; set; } = null!;
    public DbSet<User>        Users        { get; set; } = null!;
    public DbSet<UserRecord>  UserRecords  { get; set; } = null!;
    public DbSet<VideoType>   VideoTypes   { get; set; } = null!;

    public MonitorDbContext(DbContextOptions<MonitorDbContext> options) : base(options)
    {
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        var config = new ConfigurationBuilder()
                    .AddJsonFile("appsettings.json")
                    .Build();
        
        optionsBuilder.UseSqlServer(config.GetConnectionString("DefaultConnection"));
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        // config User - UserRecord one-to-many relationship
        modelBuilder.Entity<User>()
                    .HasMany(u => u.UserRecords)
                    .WithOne(ur => ur.User)
                    .HasForeignKey(ur => ur.Uid)
                    .OnDelete(DeleteBehavior.Cascade);

        // config User - UserRecord one-to-one relationship
        modelBuilder.Entity<User>()
                    .HasOne(u => u.MostRecentUserRecord)
                    .WithOne()
                    .HasForeignKey<User>(u => u.MostRecentUserRecordId)
                    .IsRequired(false)
                    .OnDelete(DeleteBehavior.Restrict);

        // config User - Video one-to-many relationship
        modelBuilder.Entity<User>()
                    .HasMany(u => u.Videos)
                    .WithOne(v => v.Author)
                    .HasForeignKey(v => v.AuthorUid)
                    .OnDelete(DeleteBehavior.Restrict);

        // config User - Video one-to-one relationship
        modelBuilder.Entity<Video>()
                    .HasOne(v => v.Author)
                    .WithMany()
                    .HasForeignKey(v => v.AuthorUid)
                    .OnDelete(DeleteBehavior.Restrict);

        // config Video and VideoRecord one-to-many relationship
        modelBuilder.Entity<Video>()
                    .HasMany(v => v.VideoRecords)
                    .WithOne(vr => vr.Video)
                    .HasForeignKey(vr => vr.BvId)
                    .OnDelete(DeleteBehavior.Cascade);

        // config Video and VideoRecord one-to-one relationship
        modelBuilder.Entity<Video>()
                    .HasOne(v => v.MostRecentVideoRecord)
                    .WithOne()
                    .HasForeignKey<Video>(v => v.MostRecentVideoRecordId)
                    .IsRequired(false)
                    .OnDelete(DeleteBehavior.Restrict);

        // config Video and VideoType one-to-many relationship
        modelBuilder.Entity<Video>()
                    .HasOne(v => v.Type)
                    .WithMany()
                    .HasForeignKey(v => v.TypeId)
                    .OnDelete(DeleteBehavior.Restrict);
    }
}


/// <summary>
/// 模型构建器扩展方法
/// 用于简化模型构建器的配置
/// 暂时先不用
/// </summary>
public static class ModelBuilderExtsions
{
    public static void HasOneToOne<TPrincipal, TDependent>(this ModelBuilder modelBuilder,
                                                           Expression<Func<TPrincipal, TDependent>>
                                                               navigationExpression,
                                                           Expression<Func<TDependent, TPrincipal>>
                                                               inverseNavigationExpression,
                                                           Expression<Func<TDependent, object>> foreignKeyExpression,
                                                           bool required = false,
                                                           DeleteBehavior deleteBehavior = DeleteBehavior.Restrict)
        where TPrincipal : class where TDependent : class
    {
        modelBuilder.Entity<TPrincipal>()
                    .HasOne(navigationExpression)
                    .WithOne(inverseNavigationExpression)
                    .HasForeignKey<TDependent>(foreignKeyExpression)
                    .IsRequired(required)
                    .OnDelete(deleteBehavior);
    }

    public static void HasOneToMany<TPrincipal, TDependent>(this ModelBuilder modelBuilder,
                                                            Expression<Func<TPrincipal, IEnumerable<TDependent>>>
                                                                navigationExpression,
                                                            Expression<Func<TDependent, TPrincipal>>
                                                                inverseNavigationExpression,
                                                            Expression<Func<TDependent, object>> foreignKeyExpression,
                                                            DeleteBehavior deleteBehavior = DeleteBehavior.Cascade)
        where TPrincipal : class where TDependent : class
    {
        modelBuilder.Entity<TPrincipal>()
                    .HasMany(navigationExpression)
                    .WithOne(inverseNavigationExpression)
                    .HasForeignKey(foreignKeyExpression)
                    .OnDelete(deleteBehavior);
    }

    public static ModelBuilder ConfigUserEntity(this ModelBuilder modelBuilder)
    {
        modelBuilder.HasOneToMany<User, UserRecord>(
            u => u.UserRecords, 
            ur => ur.User, 
            ur => ur.Uid);
        modelBuilder.HasOneToOne<User, UserRecord>(u => u.MostRecentUserRecord, ur => ur.User, ur => ur.Uid);
        modelBuilder.HasOneToMany<User, Video>(u => u.Videos, v => v.Author, v => v.AuthorUid);
        return modelBuilder;
    }
    
    public static ModelBuilder ConfigVideoEntity(this ModelBuilder modelBuilder)
    {
        modelBuilder.HasOneToMany<Video, VideoRecord>(v => v.VideoRecords, vr => vr.Video, vr => vr.BvId);
        modelBuilder.HasOneToOne<Video, VideoRecord>(v => v.MostRecentVideoRecord, vr => vr.Video, vr => vr.BvId);
        // modelBuilder.HasOneToOne<Video, VideoType>(v => v.Type, vt => vt.Videos, vt => vt.Id);
        return modelBuilder;
    }
    
    public static ModelBuilder ConfigVideoRecordEntity(this ModelBuilder modelBuilder)
    {
        modelBuilder.HasOneToOne<VideoRecord, Video>(vr => vr.Video, v => v.MostRecentVideoRecord, v => v.MostRecentVideoRecordId);
        return modelBuilder;
    }
    
    public static ModelBuilder ConfigUserRecordEntity(this ModelBuilder modelBuilder)
    {
        modelBuilder.HasOneToOne<UserRecord, User>(ur => ur.User, u => u.MostRecentUserRecord, u => u.MostRecentUserRecordId);
        return modelBuilder;
    }
    
    // public static ModelBuilder ConfigVideoTypeEntity(this ModelBuilder modelBuilder)
    // {
    //     modelBuilder.HasOneToMany<VideoType, Video>(vt => vt.Videos, v => v.Type, v => v.TypeId);
    //     return modelBuilder;
    // }
    
    
}