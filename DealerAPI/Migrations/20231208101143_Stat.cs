using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DealerAPI.Migrations
{
    /// <inheritdoc />
    public partial class Stat : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Userstbl_Statustbl_StatusId",
                table: "Userstbl");

            migrationBuilder.AlterColumn<int>(
                name: "StatusId",
                table: "Userstbl",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Userstbl_Statustbl_StatusId",
                table: "Userstbl",
                column: "StatusId",
                principalTable: "Statustbl",
                principalColumn: "StatusId",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Userstbl_Statustbl_StatusId",
                table: "Userstbl");

            migrationBuilder.AlterColumn<int>(
                name: "StatusId",
                table: "Userstbl",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddForeignKey(
                name: "FK_Userstbl_Statustbl_StatusId",
                table: "Userstbl",
                column: "StatusId",
                principalTable: "Statustbl",
                principalColumn: "StatusId");
        }
    }
}
