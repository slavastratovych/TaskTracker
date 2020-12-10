using Microsoft.EntityFrameworkCore.Migrations;

namespace TaskTracker.WebUI.Migrations
{
    public partial class AddUserProperty : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "UserID",
                table: "Context",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Context_UserID",
                table: "Context",
                column: "UserID");

            migrationBuilder.AddForeignKey(
                name: "FK_Context_AspNetUsers_UserID",
                table: "Context",
                column: "UserID",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Context_AspNetUsers_UserID",
                table: "Context");

            migrationBuilder.DropIndex(
                name: "IX_Context_UserID",
                table: "Context");

            migrationBuilder.DropColumn(
                name: "UserID",
                table: "Context");
        }
    }
}
