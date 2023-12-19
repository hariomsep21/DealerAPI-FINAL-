using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DealerAPI.Migrations
{
    /// <inheritdoc />
    public partial class Stocks : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "RegisterAddresses",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Address = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Userid = table.Column<int>(type: "int", nullable: false),
                    IdU = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RegisterAddresses", x => x.Id);
                    table.ForeignKey(
                        name: "FK_RegisterAddresses_Userstbl_IdU",
                        column: x => x.IdU,
                        principalTable: "Userstbl",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "UnregisterAddresses",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Address = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    UId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UnregisterAddresses", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UnregisterAddresses_Userstbl_UId",
                        column: x => x.UId,
                        principalTable: "Userstbl",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_RegisterAddresses_IdU",
                table: "RegisterAddresses",
                column: "IdU");

            migrationBuilder.CreateIndex(
                name: "IX_UnregisterAddresses_UId",
                table: "UnregisterAddresses",
                column: "UId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "RegisterAddresses");

            migrationBuilder.DropTable(
                name: "UnregisterAddresses");
        }
    }
}
