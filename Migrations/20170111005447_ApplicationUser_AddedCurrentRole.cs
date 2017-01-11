using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

namespace spaceyyz_asp.Migrations
{
    public partial class ApplicationUser_AddedCurrentRole : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "CurrentRole",
                table: "AspNetUsers",
                maxLength: 30,
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CurrentRole",
                table: "AspNetUsers");
        }
    }
}
