using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace quiz_app_api.Data.Migrations
{
    /// <inheritdoc />
    public partial class UserSeeder : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "UserEntities",
                columns: new[] { "Id", "AccountType", "ApiKey", "Login", "Name", "Password", "Status", "Surname" },
                values: new object[,]
                {
                    { 1, 0, "admin111", "admin", "Admin", 111, 0, "Admin" },
                    { 2, 1, "Imię222", "imięnazwisko", "Imię", 222, 0, "Nazwisko" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "UserEntities",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "UserEntities",
                keyColumn: "Id",
                keyValue: 2);
        }
    }
}
