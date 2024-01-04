using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DealerAPI.Migrations
{
    /// <inheritdoc />
    public partial class newproperty : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "Active",
                table: "Userstbl",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "Rejected",
                table: "Userstbl",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Active",
                table: "Userstbl");

            migrationBuilder.DropColumn(
                name: "Rejected",
                table: "Userstbl");
        }
    }
}
