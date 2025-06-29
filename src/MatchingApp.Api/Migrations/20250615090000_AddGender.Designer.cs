// <auto-generated />
using System;
using MatchingApp.Api.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace MatchingApp.Api.Migrations
{
    [DbContext(typeof(AppDbContext))]
    [Migration("20250615090000_AddGender")]
    partial class AddGender
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.0")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("MatchingApp.Api.Models.Client", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<DateTime>("BirthDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("BirthLocation")
                        .HasColumnType("nvarchar(max)");

                    b.Property<TimeSpan>("BirthTime")
                        .HasColumnType("time");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)");

                    b.Property<string>("Gender")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PasswordHash")
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)");

                    b.Property<string>("PreferredGender")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("Email")
                        .IsUnique();

                    b.ToTable("Clients");
                });

            modelBuilder.Entity("MatchingApp.Api.Models.Match", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("ClientAId")
                        .HasColumnType("int");

                    b.Property<int>("ClientBId")
                        .HasColumnType("int");

                    b.Property<double>("Score")
                        .HasColumnType("float");

                    b.HasKey("Id");

                    b.HasIndex("ClientAId", "ClientBId")
                        .IsUnique();

                    b.ToTable("Matches");
                });

            modelBuilder.Entity("MatchingApp.Api.Models.NatalChart", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Ascendant")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("ClientId")
                        .HasColumnType("int");

                    b.Property<string>("JupiterSign")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("MarsSign")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("MercurySign")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("MoonSign")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("NeptuneSign")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PlutoSign")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("SaturnSign")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("SunSign")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UranusSign")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("VenusSign")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("ClientId")
                        .IsUnique();

                    b.ToTable("NatalCharts");
                });

            modelBuilder.Entity("MatchingApp.Api.Models.NatalChart", b =>
                {
                    b.HasOne("MatchingApp.Api.Models.Client", "Client")
                        .WithOne("NatalChart")
                        .HasForeignKey("MatchingApp.Api.Models.NatalChart", "ClientId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Client");
                });

            modelBuilder.Entity("MatchingApp.Api.Models.Client", b =>
                {
                    b.Navigation("NatalChart");
                });
#pragma warning restore 612, 618
        }
    }
}
