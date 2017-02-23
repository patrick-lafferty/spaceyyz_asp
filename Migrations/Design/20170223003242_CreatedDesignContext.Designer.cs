using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using SpaceYYZ.Data;

namespace spaceyyz_asp.Migrations.Design
{
    [DbContext(typeof(DesignContext))]
    [Migration("20170223003242_CreatedDesignContext")]
    partial class CreatedDesignContext
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
            modelBuilder
                .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn)
                .HasAnnotation("ProductVersion", "1.1.0-rtm-22752");

            modelBuilder.Entity("SpaceYYZ.Models.Engine", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(50);

                    b.Property<int>("SeaLevelEngineId");

                    b.Property<int>("VacuumEngineId");

                    b.HasKey("ID");

                    b.HasIndex("SeaLevelEngineId");

                    b.HasIndex("VacuumEngineId");

                    b.ToTable("Engines");
                });

            modelBuilder.Entity("SpaceYYZ.Models.Performance", b =>
                {
                    b.Property<int>("EngineId")
                        .ValueGeneratedOnAdd();

                    b.Property<float>("Isp");

                    b.Property<float>("Thrust");

                    b.HasKey("EngineId");

                    b.ToTable("Performance");
                });

            modelBuilder.Entity("SpaceYYZ.Models.Engine", b =>
                {
                    b.HasOne("SpaceYYZ.Models.Performance", "SeaLevel")
                        .WithMany()
                        .HasForeignKey("SeaLevelEngineId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("SpaceYYZ.Models.Performance", "Vacuum")
                        .WithMany()
                        .HasForeignKey("VacuumEngineId")
                        .OnDelete(DeleteBehavior.Cascade);
                });
        }
    }
}
