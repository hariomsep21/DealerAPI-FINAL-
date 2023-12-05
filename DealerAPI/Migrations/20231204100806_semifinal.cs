using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DealerAPI.Migrations
{
    /// <inheritdoc />
    public partial class semifinal : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Cars_Userstbl_UserInfoId",
                table: "Cars");

            migrationBuilder.AlterColumn<int>(
                name: "Status",
                table: "procDetails",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "UserInfoId",
                table: "Cars",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddForeignKey(
                name: "FK_Cars_Userstbl_UserInfoId",
                table: "Cars",
                column: "UserInfoId",
                principalTable: "Userstbl",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Cars_Userstbl_UserInfoId",
                table: "Cars");

            migrationBuilder.AlterColumn<int>(
                name: "Status",
                table: "procDetails",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<int>(
                name: "UserInfoId",
                table: "Cars",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Cars_Userstbl_UserInfoId",
                table: "Cars",
                column: "UserInfoId",
                principalTable: "Userstbl",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
