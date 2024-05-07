using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DVDVault.Infra.Migrations
{
    /// <inheritdoc />
    public partial class AddColumnCreatedAtAndDeletedAt : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DeletedAt1",
                table: "DVDs");

            migrationBuilder.AlterColumn<DateTime>(
                name: "DeletedAt",
                table: "DVDs",
                type: "timestamp",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "timestamp");

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedAt",
                table: "DVDs",
                type: "timestamp",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CreatedAt",
                table: "DVDs");

            migrationBuilder.AlterColumn<DateTime>(
                name: "DeletedAt",
                table: "DVDs",
                type: "timestamp",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                oldClrType: typeof(DateTime),
                oldType: "timestamp",
                oldNullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "DeletedAt1",
                table: "DVDs",
                type: "timestamp with time zone",
                nullable: true);
        }
    }
}
