﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Pagos.Infrastructure;

namespace Pagos.Infrastructure.Migrations
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

            modelBuilder.Entity("Pagos.Domain.Constancia", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<decimal>("Amount")
                        .HasColumnType("decimal(18,2)");

                    b.Property<long>("Cargo_Id");

                    b.Property<long?>("PagoId");

                    b.HasKey("Id");

                    b.HasIndex("PagoId");

                    b.ToTable("Constancia","dbo");
                });

            modelBuilder.Entity("Pagos.Domain.Pago", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<decimal>("Amount_Currency")
                        .HasColumnType("decimal(18,2)");

                    b.Property<decimal>("Amount_Legal")
                        .HasColumnType("decimal(18,2)");

                    b.Property<int>("Currency");

                    b.Property<long>("User_Id");

                    b.HasKey("Id");

                    b.ToTable("Pago","dbo");
                });

            modelBuilder.Entity("Pagos.Domain.Constancia", b =>
                {
                    b.HasOne("Pagos.Domain.Pago")
                        .WithMany("Cargos_Id")
                        .HasForeignKey("PagoId");
                });
#pragma warning restore 612, 618
        }
    }
}