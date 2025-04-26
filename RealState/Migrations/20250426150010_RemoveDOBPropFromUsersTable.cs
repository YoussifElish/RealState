using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RealState.Migrations
{
    /// <inheritdoc />
    public partial class RemoveDOBPropFromUsersTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DOB",
                table: "AspNetUsers");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "7292e4ca-33b6-4b21-a314-7f72262391fd",
                column: "PasswordHash",
                value: "AQAAAAIAAYagAAAAELsr5HYY+j5KiVWue9+5x/RL7cBK6SN3Z2PecC8Xe4m+vNPZjTB5B6Az3+E5JRl7QA==");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateOnly>(
                name: "DOB",
                table: "AspNetUsers",
                type: "date",
                nullable: false,
                defaultValue: new DateOnly(1, 1, 1));

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "7292e4ca-33b6-4b21-a314-7f72262391fd",
                columns: new[] { "DOB", "PasswordHash" },
                values: new object[] { new DateOnly(1, 1, 1), "AQAAAAIAAYagAAAAEAqRWojPdVv2pRdSCS5xR+4HangUMPkymNjqjdlbOTAX1PZjB2Q83TB/rtSu+M0JBQ==" });
        }
    }
}
