﻿// <auto-generated />
using Lib.DataManagement;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace Lib.Migrations
{
    [DbContext(typeof(MonitorDbContext))]
    [Migration("20240528125855_Initial")]
    partial class Initial
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.5")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("Model.User", b =>
                {
                    b.Property<long>("Uid")
                        .HasColumnType("bigint");

                    b.Property<string>("FaceUrl")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<long>("MostRecentUserRecordId")
                        .HasColumnType("bigint");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Uid");

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

                    b.Property<long>("MostRecentVideoRecordId")
                        .HasColumnType("bigint");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("TypeId")
                        .HasColumnType("int");

                    b.Property<long>("UploadTimeStamp")
                        .HasColumnType("bigint");

                    b.HasKey("BvId");

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
                        .HasColumnType("nvarchar(max)");

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

                    b.Property<int>("Views")
                        .HasColumnType("int");

                    b.HasKey("VrId");

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
#pragma warning restore 612, 618
        }
    }
}