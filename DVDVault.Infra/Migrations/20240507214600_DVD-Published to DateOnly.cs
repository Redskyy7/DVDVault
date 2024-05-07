using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DVDVault.Infra.Migrations
{
    /// <inheritdoc />
    public partial class DVDPublishedtoDateOnly : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateOnly>(
                name: "Published",
                table: "DVDs",
                type: "date",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "timestamp");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "Published",
                table: "DVDs",
                type: "timestamp",
                nullable: false,
                oldClrType: typeof(DateOnly),
                oldType: "date");
        }
    }
}
