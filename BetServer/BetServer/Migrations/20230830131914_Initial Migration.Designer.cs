﻿// <auto-generated />
using System;
using BetServer.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace BetServer.Migrations
{
    [DbContext(typeof(DemoDBContext))]
    [Migration("20230830131914_Initial Migration")]
    partial class InitialMigration
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.10")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("BetServer.Models.Bet", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("AwayTeamName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<float>("AwayTeamOdds")
                        .HasColumnType("real");

                    b.Property<bool>("BetStatus")
                        .HasColumnType("bit");

                    b.Property<string>("BetTeamName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<float>("BetTeamOdds")
                        .HasColumnType("real");

                    b.Property<string>("DateTime")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("EventId")
                        .HasColumnType("int");

                    b.Property<string>("HomeTeamName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<float>("HomeTeamOdds")
                        .HasColumnType("real");

                    b.Property<int>("Money")
                        .HasColumnType("int");

                    b.Property<string>("Sports")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("UserId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("Bets");
                });

            modelBuilder.Entity("BetServer.Models.Event", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("AwayTeamMoney")
                        .HasColumnType("int");

                    b.Property<string>("AwayTeamName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<float>("AwayTeamOdds")
                        .HasColumnType("real");

                    b.Property<float>("AwayTeamPoint")
                        .HasColumnType("real");

                    b.Property<int?>("AwayTeamScore")
                        .HasColumnType("int");

                    b.Property<bool>("CloseEvent")
                        .HasColumnType("bit");

                    b.Property<string>("DateTime")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("HomeTeamMoney")
                        .HasColumnType("int");

                    b.Property<string>("HomeTeamName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<float>("HomeTeamOdds")
                        .HasColumnType("real");

                    b.Property<float>("HomeTeamPoint")
                        .HasColumnType("real");

                    b.Property<int?>("HomeTeamScore")
                        .HasColumnType("int");

                    b.Property<string>("LoseTeamName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool?>("Point")
                        .HasColumnType("bit");

                    b.Property<string>("Sports")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("WinTeamName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Events");
                });

            modelBuilder.Entity("BetServer.Models.Sport", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Sports");
                });

            modelBuilder.Entity("BetServer.Models.User", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int?>("Money")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Profit")
                        .HasColumnType("int");

                    b.Property<string>("Role")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("Name")
                        .IsUnique();

                    b.ToTable("Users");
                });

            modelBuilder.Entity("BetServer.Models.Bet", b =>
                {
                    b.HasOne("BetServer.Models.User", null)
                        .WithMany("Bets")
                        .HasForeignKey("UserId");
                });

            modelBuilder.Entity("BetServer.Models.User", b =>
                {
                    b.Navigation("Bets");
                });
#pragma warning restore 612, 618
        }
    }
}