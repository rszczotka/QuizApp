using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace quiz_app_api.Data.Migrations
{
    /// <inheritdoc />
    public partial class Init : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "QuestionEntities",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Text = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Options = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CorrectAnswer = table.Column<int>(type: "int", nullable: false),
                    AvailableTime = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_QuestionEntities", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "SystemStatusEntities",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Status = table.Column<int>(type: "int", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SystemStatusEntities", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "UserEntities",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AccountType = table.Column<int>(type: "int", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Surname = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Login = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Password = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Status = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserEntities", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "UserAnswerEntities",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    QuestionId = table.Column<int>(type: "int", nullable: false),
                    ChosenOption = table.Column<int>(type: "int", nullable: false),
                    StartTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    EndTime = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserAnswerEntities", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UserAnswerEntities_QuestionEntities_QuestionId",
                        column: x => x.QuestionId,
                        principalTable: "QuestionEntities",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UserAnswerEntities_UserEntities_UserId",
                        column: x => x.UserId,
                        principalTable: "UserEntities",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "QuestionEntities",
                columns: new[] { "Id", "AvailableTime", "CorrectAnswer", "Options", "Text" },
                values: new object[,]
                {
                    { 1, 25, 0, "[\"\\u017Celaza\",\"witaminy D\",\"witaminy B\",\"magnezu\"]", "Anemię powoduje niedobór:" },
                    { 2, 25, 1, "[\"w Tatrach\",\"w Bieszczadach\",\"w Sudetach\",\"w G\\u00F3rach Sto\\u0142owych\"]", "W których górach znajduje się Jezioro Solińskie?" },
                    { 3, 25, 1, "[\"Henryk Sienkiewicz\",\"Stefan \\u017Beromski\",\"Adam Mickiewicz\",\"W\\u0142adys\\u0142aw Reymont\"]", "Kto jest autorem \"Syzyfowych prac\"?" },
                    { 4, 25, 3, "[\"Lublinie\",\"Warszawie\",\"Wroc\\u0142awiu\",\"Poznaniu\"]", "Uniwersytet im. Adama Mickiewicza mieści się w:" },
                    { 5, 25, 2, "[\"forint\",\"szyling\",\"euro\",\"marka austriacka\"]", "Jaka waluta obowiązuje w Austrii?" },
                    { 6, 25, 3, "[\"Ocean Indyjski\",\"Ocean Arktyczny\",\"Ocean Atlantycki\",\"Ocean Spokojny\"]", "Który ocean jest największy?" }
                });

            migrationBuilder.InsertData(
                table: "SystemStatusEntities",
                columns: new[] { "Id", "Status", "UpdatedAt" },
                values: new object[] { 1, 0, new DateTime(2024, 3, 27, 15, 14, 53, 670, DateTimeKind.Utc).AddTicks(2287) });

            migrationBuilder.InsertData(
                table: "UserEntities",
                columns: new[] { "Id", "AccountType", "Login", "Name", "Password", "Status", "Surname" },
                values: new object[,]
                {
                    { 1, 1, "admin.admin", "admin", "0", 0, "admin" },
                    { 2, 0, "kamil.zdun", "Kamil", "111", 0, "Zdun" },
                    { 3, 0, "michał.zdun", "Michał", "222", 0, "Zdunowski" },
                    { 4, 0, "wojtek.zduński", "Wojtek", "333", 0, "Zduński" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_UserAnswerEntities_QuestionId",
                table: "UserAnswerEntities",
                column: "QuestionId");

            migrationBuilder.CreateIndex(
                name: "IX_UserAnswerEntities_UserId",
                table: "UserAnswerEntities",
                column: "UserId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "SystemStatusEntities");

            migrationBuilder.DropTable(
                name: "UserAnswerEntities");

            migrationBuilder.DropTable(
                name: "QuestionEntities");

            migrationBuilder.DropTable(
                name: "UserEntities");
        }
    }
}
