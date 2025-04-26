using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace RealState.Migrations
{
    /// <inheritdoc />
    public partial class AddLaunchTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "46235578-03e2-43cf-9344-6a0c7b20925d");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "aa6f1471-6662-4a6e-97b9-d27745585bda");

            migrationBuilder.AddColumn<int>(
                name: "LaunchId",
                table: "propertForSells",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "launches",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Location = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_launches", x => x.Id);
                });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "7292e4ca-33b6-4b21-a314-7f72262391fd",
                column: "PasswordHash",
                value: "AQAAAAIAAYagAAAAEMoJEWYgGypL2NWv9RlvZ+d4UqsQak19DUVErGy5OoHD4gXa4J3nN7n0jifuAEJ8wg==");

            migrationBuilder.CreateIndex(
                name: "IX_propertForSells_LaunchId",
                table: "propertForSells",
                column: "LaunchId");

            migrationBuilder.AddForeignKey(
                name: "FK_propertForSells_launches_LaunchId",
                table: "propertForSells",
                column: "LaunchId",
                principalTable: "launches",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_propertForSells_launches_LaunchId",
                table: "propertForSells");

            migrationBuilder.DropTable(
                name: "launches");

            migrationBuilder.DropIndex(
                name: "IX_propertForSells_LaunchId",
                table: "propertForSells");

            migrationBuilder.DropColumn(
                name: "LaunchId",
                table: "propertForSells");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "IsDefault", "IsDeleted", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "46235578-03e2-43cf-9344-6a0c7b20925d", "d7aaa1d4-a150-4044-b84c-2c59417140f7", true, false, "Member", "MEMBER" },
                    { "aa6f1471-6662-4a6e-97b9-d27745585bda", "8c841d4d-5a09-42a9-b6ff-9d7f362f5477", false, false, "Admin", "ADMIN" }
                });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "7292e4ca-33b6-4b21-a314-7f72262391fd",
                column: "PasswordHash",
                value: "AQAAAAIAAYagAAAAELsr5HYY+j5KiVWue9+5x/RL7cBK6SN3Z2PecC8Xe4m+vNPZjTB5B6Az3+E5JRl7QA==");
        }
    }
}
