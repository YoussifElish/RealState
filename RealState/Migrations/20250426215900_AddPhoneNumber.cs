using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RealState.Migrations
{
    /// <inheritdoc />
    public partial class AddPhoneNumber : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ContactNumber",
                table: "propertForSells",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "ContactNumber",
                table: "propertForRents",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "7292e4ca-33b6-4b21-a314-7f72262391fd",
                column: "PasswordHash",
                value: "AQAAAAIAAYagAAAAEKmjjDlpuiO4wMqr621knEFbBLYs2aE5B4W0rTL58FE9ovPNoh63/evPn+eI6epeCg==");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ContactNumber",
                table: "propertForSells");

            migrationBuilder.DropColumn(
                name: "ContactNumber",
                table: "propertForRents");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "7292e4ca-33b6-4b21-a314-7f72262391fd",
                column: "PasswordHash",
                value: "AQAAAAIAAYagAAAAEOwUfz8/MXoKJR/qQ1zZ0LM5MrLp66b71FpATEFcUH9U3znFu3iB4dVfHybGTkUdbg==");
        }
    }
}
