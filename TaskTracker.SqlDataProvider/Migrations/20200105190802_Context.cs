using Microsoft.EntityFrameworkCore.Migrations;

namespace TaskTracker.WebUI.Migrations
{
    public partial class Context : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ContextID",
                table: "Item",
                nullable: false,
                defaultValue: 1);

            migrationBuilder.CreateTable(
                name: "Context",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Context", x => x.ID);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Item_ContextID",
                table: "Item",
                column: "ContextID");

            migrationBuilder.InsertData("Context", new[] { "ID", "Name" }, new object[] { 1, "Inbox" });

            migrationBuilder.AddForeignKey(
                name: "FK_Item_Context_ContextID",
                table: "Item",
                column: "ContextID",
                principalTable: "Context",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Item_Context_ContextID",
                table: "Item");

            migrationBuilder.DropTable(
                name: "Context");

            migrationBuilder.DropIndex(
                name: "IX_Item_ContextID",
                table: "Item");

            migrationBuilder.DropColumn(
                name: "ContextID",
                table: "Item");
        }
    }
}
