using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RealState.Migrations
{
    /// <inheritdoc />
    public partial class EditUploadedFiles : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "uploadedFiles",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "7292e4ca-33b6-4b21-a314-7f72262391fd",
                column: "PasswordHash",
                value: "AQAAAAIAAYagAAAAEOwUfz8/MXoKJR/qQ1zZ0LM5MrLp66b71FpATEFcUH9U3znFu3iB4dVfHybGTkUdbg==");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Description",
                table: "uploadedFiles");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "7292e4ca-33b6-4b21-a314-7f72262391fd",
                column: "PasswordHash",
                value: "AQAAAAIAAYagAAAAEEWsY8suZXHx2Qlaz8+NAIHdqUlmpyrEU5POooZdRXyXEuOgkcH652yVxbvmMzJCHQ==");
        }
    }
}
