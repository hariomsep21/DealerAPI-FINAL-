using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DealerAPI.Migrations
{
    /// <inheritdoc />
    public partial class updatedsecond : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Userstbl_Statetbl_StateId",
                table: "Userstbl");

            migrationBuilder.DropForeignKey(
                name: "FK_Userstbl_Statustbl_StatusId",
                table: "Userstbl");

            migrationBuilder.DropForeignKey(
                name: "FK_Userstbl_userPhonestbl_PhnId",
                table: "Userstbl");

            migrationBuilder.DropTable(
                name: "LastUsetbl");

            migrationBuilder.DropTable(
                name: "Statustbl");

            migrationBuilder.DropIndex(
                name: "IX_Userstbl_PhnId",
                table: "Userstbl");

            migrationBuilder.DropIndex(
                name: "IX_Userstbl_StateId",
                table: "Userstbl");

            migrationBuilder.DropColumn(
                name: "PhnId",
                table: "Userstbl");

            migrationBuilder.DropColumn(
                name: "StateId",
                table: "Userstbl");

            migrationBuilder.RenameColumn(
                name: "StatusId",
                table: "Userstbl",
                newName: "SId");

            migrationBuilder.RenameIndex(
                name: "IX_Userstbl_StatusId",
                table: "Userstbl",
                newName: "IX_Userstbl_SId");

            migrationBuilder.AlterColumn<string>(
                name: "UserName",
                table: "Userstbl",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(15)",
                oldMaxLength: 15);

            migrationBuilder.AlterColumn<string>(
                name: "UserEmail",
                table: "Userstbl",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<int>(
                name: "OTP",
                table: "Userstbl",
                type: "int",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(4)",
                oldMaxLength: 4,
                oldNullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "OTPExpiry",
                table: "Userstbl",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "Phone",
                table: "Userstbl",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "RefreshToken",
                table: "Userstbl",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "TokenCreated",
                table: "Userstbl",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "TokenExpires",
                table: "Userstbl",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Userstbl_Statetbl_SId",
                table: "Userstbl",
                column: "SId",
                principalTable: "Statetbl",
                principalColumn: "StateId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Userstbl_Statetbl_SId",
                table: "Userstbl");

            migrationBuilder.DropColumn(
                name: "OTPExpiry",
                table: "Userstbl");

            migrationBuilder.DropColumn(
                name: "Phone",
                table: "Userstbl");

            migrationBuilder.DropColumn(
                name: "RefreshToken",
                table: "Userstbl");

            migrationBuilder.DropColumn(
                name: "TokenCreated",
                table: "Userstbl");

            migrationBuilder.DropColumn(
                name: "TokenExpires",
                table: "Userstbl");

            migrationBuilder.RenameColumn(
                name: "SId",
                table: "Userstbl",
                newName: "StatusId");

            migrationBuilder.RenameIndex(
                name: "IX_Userstbl_SId",
                table: "Userstbl",
                newName: "IX_Userstbl_StatusId");

            migrationBuilder.AlterColumn<string>(
                name: "UserName",
                table: "Userstbl",
                type: "nvarchar(15)",
                maxLength: 15,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "UserEmail",
                table: "Userstbl",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "OTP",
                table: "Userstbl",
                type: "nvarchar(4)",
                maxLength: 4,
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddColumn<int>(
                name: "PhnId",
                table: "Userstbl",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "StateId",
                table: "Userstbl",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "LastUsetbl",
                columns: table => new
                {
                    ActiveId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserValue = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LastUsetbl", x => x.ActiveId);
                });

            migrationBuilder.CreateTable(
                name: "Statustbl",
                columns: table => new
                {
                    StatusId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    StatusName = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Statustbl", x => x.StatusId);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Userstbl_PhnId",
                table: "Userstbl",
                column: "PhnId");

            migrationBuilder.CreateIndex(
                name: "IX_Userstbl_StateId",
                table: "Userstbl",
                column: "StateId");

            migrationBuilder.AddForeignKey(
                name: "FK_Userstbl_Statetbl_StateId",
                table: "Userstbl",
                column: "StateId",
                principalTable: "Statetbl",
                principalColumn: "StateId",
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
    }
}
