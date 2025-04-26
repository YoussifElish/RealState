using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RealState.Migrations
{
    /// <inheritdoc />
    public partial class UpdateUserTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ResetPasswordCode",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "ResetPasswordCodeExpiration",
                table: "AspNetUsers",
                type: "datetime2",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "7292e4ca-33b6-4b21-a314-7f72262391fd",
                columns: new[] { "PasswordHash", "ResetPasswordCode", "ResetPasswordCodeExpiration" },
                values: new object[] { "AQAAAAIAAYagAAAAEEWsY8suZXHx2Qlaz8+NAIHdqUlmpyrEU5POooZdRXyXEuOgkcH652yVxbvmMzJCHQ==", null, null });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ResetPasswordCode",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "ResetPasswordCodeExpiration",
                table: "AspNetUsers");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "7292e4ca-33b6-4b21-a314-7f72262391fd",
                column: "PasswordHash",
                value: "AQAAAAIAAYagAAAAEMoJEWYgGypL2NWv9RlvZ+d4UqsQak19DUVErGy5OoHD4gXa4J3nN7n0jifuAEJ8wg==");
        }
    }
}
