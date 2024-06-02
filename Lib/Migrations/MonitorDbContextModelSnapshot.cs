﻿// <auto-generated />
using System;
using Lib.DataManagement;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace Lib.Migrations
{
    [DbContext(typeof(MonitorDbContext))]
    partial class MonitorDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.5")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("Model.RunTimeVar.Config", b =>
                {
                    b.Property<string>("Key")
                        .HasColumnType("nvarchar(450)");

                    b.Property<long>("LastTimeUpdate")
                        .HasColumnType("bigint");

                    b.Property<string>("Value")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Key");

                    b.ToTable("Configs");

                    b.HasData(
                        new
                        {
                            Key = "SESSDATA",
                            LastTimeUpdate = 0L,
                            Value = ""
                        },
                        new
                        {
                            Key = "buvid3",
                            LastTimeUpdate = 0L,
                            Value = "AFE917A3-F157-AB06-AD81-F5E97E8B5D9A60062infoc"
                        },
                        new
                        {
                            Key = "buvid4",
                            LastTimeUpdate = 0L,
                            Value = "DAC8A899-9006-0900-5D29-5D64C5BB0F8824741-023013104-NLgo%2F2RgzmTINRwjdsii8w%3D%3D"
                        });
                });

            modelBuilder.Entity("Model.User", b =>
                {
                    b.Property<long>("Uid")
                        .HasColumnType("bigint");

                    b.Property<string>("FaceUrl")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<long?>("MostRecentUserRecordId")
                        .HasColumnType("bigint");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Uid");

                    b.HasIndex("MostRecentUserRecordId")
                        .IsUnique()
                        .HasFilter("[MostRecentUserRecordId] IS NOT NULL");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("Model.UserRecord", b =>
                {
                    b.Property<long>("UrId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<long>("UrId"));

                    b.Property<int>("ArchiveNum")
                        .HasColumnType("int");

                    b.Property<int>("FollowerNum")
                        .HasColumnType("int");

                    b.Property<int>("FollowingNum")
                        .HasColumnType("int");

                    b.Property<int>("LikeNum")
                        .HasColumnType("int");

                    b.Property<long>("TimeStamp")
                        .HasColumnType("bigint");

                    b.Property<long>("Uid")
                        .HasColumnType("bigint");

                    b.HasKey("UrId");

                    b.HasIndex("Uid");

                    b.ToTable("UserRecords");
                });

            modelBuilder.Entity("Model.Video", b =>
                {
                    b.Property<string>("BvId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<long>("AuthorUid")
                        .HasColumnType("bigint");

                    b.Property<string>("CoverUrl")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<long?>("MostRecentVideoRecordId")
                        .HasColumnType("bigint");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("TypeId")
                        .HasColumnType("int");

                    b.Property<long>("UploadTimeStamp")
                        .HasColumnType("bigint");

                    b.Property<long?>("UserUid")
                        .HasColumnType("bigint");

                    b.HasKey("BvId");

                    b.HasIndex("AuthorUid");

                    b.HasIndex("MostRecentVideoRecordId")
                        .IsUnique()
                        .HasFilter("[MostRecentVideoRecordId] IS NOT NULL");

                    b.HasIndex("TypeId");

                    b.HasIndex("UserUid");

                    b.ToTable("Videos");
                });

            modelBuilder.Entity("Model.VideoRecord", b =>
                {
                    b.Property<long>("VrId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<long>("VrId"));

                    b.Property<string>("BvId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.Property<int>("Coins")
                        .HasColumnType("int");

                    b.Property<int>("Comments")
                        .HasColumnType("int");

                    b.Property<int>("Danmaku")
                        .HasColumnType("int");

                    b.Property<int>("Dislikes")
                        .HasColumnType("int");

                    b.Property<int>("Favorites")
                        .HasColumnType("int");

                    b.Property<int>("Likes")
                        .HasColumnType("int");

                    b.Property<int>("Shares")
                        .HasColumnType("int");

                    b.Property<long>("TimeStamp")
                        .HasColumnType("bigint");

                    b.Property<int>("ViewingNum")
                        .HasColumnType("int");

                    b.Property<int>("Views")
                        .HasColumnType("int");

                    b.HasKey("VrId");

                    b.HasIndex("BvId");

                    b.ToTable("VideoRecords");
                });

            modelBuilder.Entity("Model.VideoType", b =>
                {
                    b.Property<int>("TypeId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("TypeId"));

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("TypeId");

                    b.ToTable("VideoTypes");
                });

            modelBuilder.Entity("Model.User", b =>
                {
                    b.HasOne("Model.UserRecord", "MostRecentUserRecord")
                        .WithOne()
                        .HasForeignKey("Model.User", "MostRecentUserRecordId")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.Navigation("MostRecentUserRecord");
                });

            modelBuilder.Entity("Model.UserRecord", b =>
                {
                    b.HasOne("Model.User", "User")
                        .WithMany("UserRecords")
                        .HasForeignKey("Uid")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("User");
                });

            modelBuilder.Entity("Model.Video", b =>
                {
                    b.HasOne("Model.User", "Author")
                        .WithMany()
                        .HasForeignKey("AuthorUid")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("Model.VideoRecord", "MostRecentVideoRecord")
                        .WithOne()
                        .HasForeignKey("Model.Video", "MostRecentVideoRecordId")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.HasOne("Model.VideoType", "Type")
                        .WithMany()
                        .HasForeignKey("TypeId")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.HasOne("Model.User", null)
                        .WithMany("Videos")
                        .HasForeignKey("UserUid");

                    b.Navigation("Author");

                    b.Navigation("MostRecentVideoRecord");

                    b.Navigation("Type");
                });

            modelBuilder.Entity("Model.VideoRecord", b =>
                {
                    b.HasOne("Model.Video", "Video")
                        .WithMany("VideoRecords")
                        .HasForeignKey("BvId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Video");
                });

            modelBuilder.Entity("Model.User", b =>
                {
                    b.Navigation("UserRecords");

                    b.Navigation("Videos");
                });

            modelBuilder.Entity("Model.Video", b =>
                {
                    b.Navigation("VideoRecords");
                });
#pragma warning restore 612, 618
        }
    }
}
