using Microsoft.EntityFrameworkCore.Migrations;

namespace DowntimeAlerter.DataAccess.Migrations
{
    public partial class UserInitialModel : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    UserName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Password = table.Column<string>(type: "nvarchar(300)", maxLength: 300, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "Site",
                columns: new[] { "Id", "Email", "IntervalTime", "Name", "Url" },
                values: new object[] { 1, null, 40L, "Google", "https://google.com" });

            migrationBuilder.InsertData(
                table: "Site",
                columns: new[] { "Id", "Email", "IntervalTime", "Name", "Url" },
                values: new object[] { 2, null, 30L, "Down Site Example", "https://example.org/impolite" });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "Name", "Password", "UserName" },
                values: new object[] { 1, "Onur ARSLAN", "61EDC5202B80B64056B78436F6385B9C", "onurarslan" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.DeleteData(
                table: "Site",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Site",
                keyColumn: "Id",
                keyValue: 2);
        }
    }
}
