using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DealerAPI.Migrations
{
    /// <inheritdoc />
    public partial class stockAddress : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "UnregisterAddresses");

            migrationBuilder.AddColumn<string>(
                name: "AddressType",
                table: "RegisterAddresses",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AddressType",
                table: "RegisterAddresses");

            migrationBuilder.CreateTable(
                name: "UnregisterAddresses",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UId = table.Column<int>(type: "int", nullable: false),
                    Address = table.Column<string>(type: "nvarchar(max)", nullable: false)
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
                name: "IX_UnregisterAddresses_UId",
                table: "UnregisterAddresses",
                column: "UId");
        }
    }
}
