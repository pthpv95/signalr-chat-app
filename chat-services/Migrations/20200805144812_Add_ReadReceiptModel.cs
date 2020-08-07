using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace realtime_app.Migrations
{
    public partial class Add_ReadReceiptModel : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ReadReceipts",
                columns: table => new
                {
                    MessageId = table.Column<Guid>(nullable: false),
                    SeenerId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ReadReceipts", x => new { x.MessageId, x.SeenerId });
                    table.ForeignKey(
                        name: "FK_ReadReceipts_Messages_MessageId",
                        column: x => x.MessageId,
                        principalTable: "Messages",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ReadReceipts");
        }
    }
}
