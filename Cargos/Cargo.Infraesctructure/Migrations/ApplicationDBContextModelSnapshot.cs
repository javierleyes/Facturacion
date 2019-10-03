﻿// <auto-generated />
using System;
using Cargos.Infraesctructure;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace Cargos.Infrastructure.Migrations
{
    [DbContext(typeof(ApplicationDBContext))]
    partial class ApplicationDBContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.2.6-servicing-10079")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("Cargos.Domain.Cargo", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<decimal>("Amount")
                        .HasColumnType("decimal(18,2)");

                    b.Property<decimal>("Balance")
                        .HasColumnType("decimal(18,2)");

                    b.Property<long>("EventoId");

                    b.Property<long?>("FacturaId");

                    b.Property<int>("State");

                    b.Property<int>("Type");

                    b.Property<long>("User_Id");

                    b.HasKey("Id");

                    b.HasIndex("EventoId");

                    b.HasIndex("FacturaId");

                    b.ToTable("Cargo","dbo");
                });

            modelBuilder.Entity("Cargos.Domain.Evento", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<decimal>("Amount")
                        .HasColumnType("decimal(18,2)");

                    b.Property<int>("Currency");

                    b.Property<DateTime>("Date");

                    b.Property<int>("Type");

                    b.Property<long>("User_Id");

                    b.HasKey("Id");

                    b.ToTable("Evento","dbo");
                });

            modelBuilder.Entity("Cargos.Domain.Factura", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("Month");

                    b.Property<long>("User_Id");

                    b.Property<int>("Year");

                    b.HasKey("Id");

                    b.ToTable("Factura","dbo");
                });

            modelBuilder.Entity("Cargos.Domain.Cargo", b =>
                {
                    b.HasOne("Cargos.Domain.Evento", "Evento")
                        .WithMany()
                        .HasForeignKey("EventoId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("Cargos.Domain.Factura")
                        .WithMany("Cargos")
                        .HasForeignKey("FacturaId");
                });
#pragma warning restore 612, 618
        }
    }
}