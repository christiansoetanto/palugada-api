﻿// <auto-generated />
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;
using palugada_api;

#nullable disable

namespace palugada_api.Migrations
{
    [DbContext(typeof(PalugadaDbContext))]
    [Migration("20211209131214_AddOrder")]
    partial class AddOrder
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.0")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("palugada_api.Entities.Menu", b =>
                {
                    b.Property<int>("MenuId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("MenuId"));

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<int>("Price")
                        .HasColumnType("integer");

                    b.Property<int>("UserId")
                        .HasColumnType("integer");

                    b.HasKey("MenuId");

                    b.HasIndex("UserId");

                    b.ToTable("Menu");
                });

            modelBuilder.Entity("palugada_api.Entities.OrderDetail", b =>
                {
                    b.Property<int>("OrderDetailId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("OrderDetailId"));

                    b.Property<int>("Amount")
                        .HasColumnType("integer");

                    b.Property<int>("MenuId")
                        .HasColumnType("integer");

                    b.Property<int>("OrderHeaderId")
                        .HasColumnType("integer");

                    b.HasKey("OrderDetailId");

                    b.HasIndex("MenuId");

                    b.HasIndex("OrderHeaderId");

                    b.ToTable("OrderDetail");
                });

            modelBuilder.Entity("palugada_api.Entities.OrderHeader", b =>
                {
                    b.Property<int>("OrderHeaderId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("OrderHeaderId"));

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<int>("UserId")
                        .HasColumnType("integer");

                    b.HasKey("OrderHeaderId");

                    b.HasIndex("UserId");

                    b.ToTable("OrderHeader");
                });

            modelBuilder.Entity("palugada_api.Entities.User", b =>
                {
                    b.Property<int>("UserId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("UserId"));

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Username")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("UserId");

                    b.ToTable("User");
                });

            modelBuilder.Entity("palugada_api.Entities.Menu", b =>
                {
                    b.HasOne("palugada_api.Entities.User", "User")
                        .WithMany("Menu")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("User");
                });

            modelBuilder.Entity("palugada_api.Entities.OrderDetail", b =>
                {
                    b.HasOne("palugada_api.Entities.Menu", "Menu")
                        .WithMany()
                        .HasForeignKey("MenuId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("palugada_api.Entities.OrderHeader", "OrderHeader")
                        .WithMany("OrderDetail")
                        .HasForeignKey("OrderHeaderId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Menu");

                    b.Navigation("OrderHeader");
                });

            modelBuilder.Entity("palugada_api.Entities.OrderHeader", b =>
                {
                    b.HasOne("palugada_api.Entities.User", "User")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("User");
                });

            modelBuilder.Entity("palugada_api.Entities.OrderHeader", b =>
                {
                    b.Navigation("OrderDetail");
                });

            modelBuilder.Entity("palugada_api.Entities.User", b =>
                {
                    b.Navigation("Menu");
                });
#pragma warning restore 612, 618
        }
    }
}