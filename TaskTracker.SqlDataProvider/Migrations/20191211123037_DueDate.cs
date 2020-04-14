using Microsoft.EntityFrameworkCore.Migrations;
using System;

namespace TaskTracker.WebUI.Migrations
{
    public partial class DueDate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "DueDate",
                table: "Item",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DueDate",
                table: "Item");
        }
    }
}
