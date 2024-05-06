using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DVDVault.Infra.Migrations
{
    /// <inheritdoc />
    public partial class DirectorIsActiveColumn : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsActive",
                table: "Directors",
                type: "boolean",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsActive",
                table: "Directors");
        }
    }
}
