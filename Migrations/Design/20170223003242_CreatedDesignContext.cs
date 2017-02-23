using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Metadata;

namespace spaceyyz_asp.Migrations.Design
{
    public partial class CreatedDesignContext : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Performance",
                columns: table => new
                {
                    EngineId = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    Isp = table.Column<float>(nullable: false),
                    Thrust = table.Column<float>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Performance", x => x.EngineId);
                });

            migrationBuilder.CreateTable(
                name: "Engines",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    Name = table.Column<string>(maxLength: 50, nullable: false),
                    SeaLevelEngineId = table.Column<int>(nullable: false),
                    VacuumEngineId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Engines", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Engines_Performance_SeaLevelEngineId",
                        column: x => x.SeaLevelEngineId,
                        principalTable: "Performance",
                        principalColumn: "EngineId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Engines_Performance_VacuumEngineId",
                        column: x => x.VacuumEngineId,
                        principalTable: "Performance",
                        principalColumn: "EngineId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Engines_SeaLevelEngineId",
                table: "Engines",
                column: "SeaLevelEngineId");

            migrationBuilder.CreateIndex(
                name: "IX_Engines_VacuumEngineId",
                table: "Engines",
                column: "VacuumEngineId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Engines");

            migrationBuilder.DropTable(
                name: "Performance");
        }
    }
}
