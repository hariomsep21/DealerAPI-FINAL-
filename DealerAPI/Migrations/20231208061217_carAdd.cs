using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DealerAPI.Migrations
{
    /// <inheritdoc />
    public partial class carAdd : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Cars_Userstbl_UserInfoId",
                table: "Cars");

            migrationBuilder.DropForeignKey(
                name: "FK_Userstbl_Statustbl_StatusId",
                table: "Userstbl");

            migrationBuilder.DropForeignKey(
                name: "FK_Userstbl_userPhonestbl_PhnId",
                table: "Userstbl");

            migrationBuilder.DropIndex(
                name: "IX_Cars_UserInfoId",
                table: "Cars");

            migrationBuilder.DropColumn(
                name: "UserInfoId",
                table: "Cars");

            migrationBuilder.AlterColumn<int>(
                name: "StatusId",
                table: "Userstbl",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<int>(
                name: "PhnId",
                table: "Userstbl",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<string>(
                name: "OTP",
                table: "Userstbl",
                type: "nvarchar(4)",
                maxLength: 4,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(4)",
                oldMaxLength: 4);

            migrationBuilder.AddColumn<int>(
                name: "UserId",
                table: "Cars",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Cars_UserId",
                table: "Cars",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Cars_Userstbl_UserId",
                table: "Cars",
                column: "UserId",
                principalTable: "Userstbl",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Userstbl_Statustbl_StatusId",
                table: "Userstbl",
                column: "StatusId",
                principalTable: "Statustbl",
                principalColumn: "StatusId");

            migrationBuilder.AddForeignKey(
                name: "FK_Userstbl_userPhonestbl_PhnId",
                table: "Userstbl",
                column: "PhnId",
                principalTable: "userPhonestbl",
                principalColumn: "PhoneId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Cars_Userstbl_UserId",
                table: "Cars");

            migrationBuilder.DropForeignKey(
                name: "FK_Userstbl_Statustbl_StatusId",
                table: "Userstbl");

            migrationBuilder.DropForeignKey(
                name: "FK_Userstbl_userPhonestbl_PhnId",
                table: "Userstbl");

            migrationBuilder.DropIndex(
                name: "IX_Cars_UserId",
                table: "Cars");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "Cars");

            migrationBuilder.AlterColumn<int>(
                name: "StatusId",
                table: "Userstbl",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "PhnId",
                table: "Userstbl",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "OTP",
                table: "Userstbl",
                type: "nvarchar(4)",
                maxLength: 4,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(4)",
                oldMaxLength: 4,
                oldNullable: true);

            migrationBuilder.AddColumn<int>(
                name: "UserInfoId",
                table: "Cars",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Cars_UserInfoId",
                table: "Cars",
                column: "UserInfoId");

            migrationBuilder.AddForeignKey(
                name: "FK_Cars_Userstbl_UserInfoId",
                table: "Cars",
                column: "UserInfoId",
                principalTable: "Userstbl",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Userstbl_Statustbl_StatusId",
                table: "Userstbl",
                column: "StatusId",
                principalTable: "Statustbl",
                principalColumn: "StatusId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Userstbl_userPhonestbl_PhnId",
                table: "Userstbl",
                column: "PhnId",
                principalTable: "userPhonestbl",
                principalColumn: "PhoneId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
