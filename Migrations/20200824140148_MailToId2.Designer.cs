﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using yyytours;

namespace yyytours.Migrations
{
    [DbContext(typeof(yyyWebProjContext))]
    [Migration("20200824140148_MailToId2")]
    partial class MailToId2
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "3.1.5")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("yyytours.Models.Place", b =>
                {
                    b.Property<string>("ID")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Country")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ImageUrl")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("ID");

                    b.ToTable("Place");
                });

            modelBuilder.Entity("yyytours.Models.Trip", b =>
                {
                    b.Property<string>("ID")
                        .HasColumnType("nvarchar(450)");

                    b.Property<int>("Capacity")
                        .HasColumnType("int");

                    b.Property<DateTime>("Date")
                        .HasColumnType("datetime2");

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("DisplayName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("GuideId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("PlaceId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<int>("Price")
                        .HasColumnType("int");

                    b.Property<int>("TimeInHours")
                        .HasColumnType("int");

                    b.HasKey("ID");

                    b.HasIndex("GuideId");

                    b.HasIndex("PlaceId");

                    b.ToTable("Trip");
                });

            modelBuilder.Entity("yyytours.Models.User", b =>
                {
                    b.Property<string>("Email")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("FullName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Phone")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Type")
                        .HasColumnType("int");

                    b.HasKey("Email");

                    b.ToTable("User");
                });

            modelBuilder.Entity("yyytours.Models.Trip", b =>
                {
                    b.HasOne("yyytours.Models.User", "Guide")
                        .WithMany()
                        .HasForeignKey("GuideId");

                    b.HasOne("yyytours.Models.Place", "Place")
                        .WithMany()
                        .HasForeignKey("PlaceId");
                });
#pragma warning restore 612, 618
        }
    }
}
