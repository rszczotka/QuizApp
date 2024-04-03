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
                    Status = table.Column<int>(type: "int", nullable: false),
                    StartTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    EndTime = table.Column<DateTime>(type: "datetime2", nullable: false)
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
                    { 1, 25, 2, "[\"magnezu\",\"witaminy B\",\"\\u017Celaza\",\"witaminy D\"]", "Anemię powoduje niedobór:" },
                    { 2, 25, 2, "[\"w Sudetach\",\"w Tatrach\",\"w Bieszczadach\",\"w G\\u00F3rach Sto\\u0142owych\"]", "W których górach znajduje się Jezioro Solińskie?" },
                    { 3, 25, 0, "[\"Stefan \\u017Beromski\",\"Adam Mickiewicz\",\"W\\u0142adys\\u0142aw Reymont\",\"Henryk Sienkiewicz\"]", "Kto jest autorem \"Syzyfowych prac\"?" },
                    { 4, 25, 3, "[\"Lublinie\",\"Warszawie\",\"Wroc\\u0142awiu\",\"Poznaniu\"]", "Uniwersytet im. Adama Mickiewicza mieści się w:" },
                    { 5, 25, 0, "[\"euro\",\"forint\",\"szyling\",\"marka austriacka\"]", "Jaka waluta obowiązuje w Austrii?" },
                    { 6, 25, 3, "[\"Ocean Arktyczny\",\"Ocean Atlantycki\",\"Ocean Indyjski\",\"Ocean Spokojny\"]", "Który ocean jest największy?" },
                    { 7, 25, 1, "[\"Jowisz \",\"Mars\",\"Pluton\",\"Wenus\"]", "„Czerwoną planetą” nazywamy …" },
                    { 8, 25, 1, "[\"fosfor\",\"\\u017Celazo\",\"srebro\",\"fluor\"]", "Który pierwiastek chemiczny ma symbol FE?" },
                    { 9, 25, 3, "[\"Jan Sebastian Bach\",\"Franz Schubert\",\"Wolfgang Amadeusz Mozart\",\"Ludwig van Beethoven\"]", "Kto jest autorem muzyki wykorzystywanej w hymnie Unii Europejskiej?" },
                    { 10, 25, 3, "[\"100 000 gr.\",\"1 000 000 gr.\",\"1000 gr.\",\"10 000 gr.\"]", "Jedna złotówka to 100 groszy, a sto złotych to...?" },
                    { 11, 25, 0, "[\"1791 r.\",\"1830 r .\",\"1795 r.\",\"1745 r.\"]", "W którym roku uchwalono Konstytucję 3 Maja?" },
                    { 12, 25, 0, "[\"amper\",\"wat\",\"om\",\"wolt\"]", "Co jest jednostką natężenia prądu elektrycznego?" },
                    { 13, 25, 1, "[\"neutrofile\",\"leukocyty\",\"erytrocyty\",\"trombocyty\"]", "Białe krwinki to...?" },
                    { 14, 25, 0, "[\"Miko\\u0142aj Rej\",\"Juliusz S\\u0142owacki\",\"Jan Zamoyski\",\"Jan Kochanowski\"]", "Kto wypowiedział słynne słowa: ,,Polacy nie gęsi, iż swój język mają”?" },
                    { 15, 25, 1, "[\"Przyroda jest najwa\\u017Cniejsza\",\"B\\u00F3g jest w centrum\",\"Teologia jest najwa\\u017Cniejsza\",\"Cz\\u0142owiek jest w centrum\"]", "Teocentryzm to pogląd , który mówi, że:" },
                    { 16, 25, 0, "[\"\\u015Awi\\u0119toszek\",\"Wiele ha\\u0142asu o nic\",\"Romeo i Julia\",\"Makbet\"]", "Którego z utworów nie napisał William Szekspir?" },
                    { 17, 25, 1, "[\"Hiperbola\",\"Anafora\",\"Epifora\",\"Oksymoron\"]", "Jak nazywa się środek stylistyczny, który polega na powtórzeniu tego samego wyrazu na początku sąsiadujących ze sobą wersetów?" },
                    { 18, 25, 0, "[\"To miesi\\u0105c pogodowych niespodzianek\",\"Jest bardzo ciep\\u0142o\",\"Jest bardzo mokro\",\"Jest bardzo zimno\"]", "Co oznacza przysłowie: W marcu jak w garncu?" },
                    { 19, 25, 0, "[\"W teatrze\",\"W bibliotece\",\"W operze\",\"W kawiarni\"]", "Gdzie Stanisław Wokulski zobaczył pannę Izabelę Łęcką po raz pierwszy?" },
                    { 20, 25, 0, "[\"Ferdydurke\",\"Szewcy\",\"Tango\",\"Granica\"]", "Termin ,,upupić kogoś” pochodzi z książki pt.?" },
                    { 21, 25, 2, "[\"Henryka Sienkiewicza\",\"Stefana \\u017Beromskiego\",\"Boles\\u0142awa Prusa\",\"Zofii Na\\u0142kowskiej\"]", "Aleksander Głowacki to pseudonim artystyczny?:" },
                    { 22, 25, 2, "[\"Dziady\",\"Sen nocy letniej\",\"Konrad Wallenrod\",\"Kordian\"]", "Który z poniższych utworów nie jest dramatem?" },
                    { 23, 25, 3, "[\"Pozytywizm\",\"XX-lecie mi\\u0119dzywojenne\",\"Romantyzm\",\"M\\u0142oda Polska\"]", "Jak nazywa się epoka literacka, która trwała od 1890 do 1918 roku?" },
                    { 24, 25, 3, "[\"z 18\",\"z 12\",\"z 14\",\"z 16\"]", "Z ilu krajów związkowych (landów)składa się Republika Federalna Niemiec?" },
                    { 25, 25, 2, "[\"witaminy D\",\"witaminy K\",\"witaminy C\",\"witaminy A\"]", "Niedobór której witaminy powoduje szkorbut, obniża odporność organizmu i osłabia naczynia krwionośne?" },
                    { 26, 25, 3, "[\"\\u015Blinianki\",\"dwunastnica\",\"trzustka\",\"grasica\"]", "Częścią układu pokarmowego człowieka nie jest:" },
                    { 27, 25, 2, "[\"diploidalno\\u015B\\u0107\",\"dwupienno\\u015B\\u0107\",\"jednopienno\\u015B\\u0107\",\"monofagia\"]", "Występowanie na tej samej roślinie męskich i żeńskich narządów rozrodczych to:" },
                    { 28, 25, 3, "[\"nietoperzach\",\"glonach\",\"porostach\",\"grzybach\"]", "Mykologia to nauka o:" },
                    { 29, 25, 2, "[\"glikogen\",\"chityna\",\"celuloza\",\"kolagen\"]", "Cukier złożony występujący powszechnie w roślinach to" },
                    { 30, 25, 3, "[\"woda i w\\u0119glowodany\",\"tlen i woda\",\"dwutlenek w\\u0119gla i woda\",\"tlen i w\\u0119glowodany\"]", "Co powstaje w wyniku fotosyntezy?" },
                    { 31, 25, 1, "[\"ryby, gady i ptaki\",\"ssaki i ptaki\",\"wszystkie kr\\u0119gowce\",\"tylko ssaki\"]", "Które zwierzęta są stałocieplne?" },
                    { 32, 25, 2, "[\"bakterie\",\"pierwotniaki\",\"wirusy\",\"paso\\u017Cyty\"]", "Różyczka to choroba zakaźna wywoływana przez:" },
                    { 33, 25, 0, "[\"gru\\u017Alic\\u0119\",\"wszystkie wymienione\",\"osp\\u0119 wietrzn\\u0105\",\"w\\u015Bcieklizn\\u0119\"]", "Do chorób wywoływanych przez bakterie zaliczamy:" },
                    { 34, 25, 1, "[\"gruczo\\u0142 uk\\u0142adu hormonalnego\",\"doros\\u0142a posta\\u0107 owada\",\"samozapylanie u ro\\u015Blin okrytonasiennych\",\"inaczej kora wzrokowa - cz\\u0119\\u015B\\u0107 m\\u00F3zgu odpowiedzialna za widzenie\"]", "Co to jest imago?" },
                    { 35, 25, 2, "[\"adaptacji oka do widzenia przy zmiennej ilo\\u015Bci \\u015Bwiat\\u0142a\",\"neutralizowaniu wirus\\u00F3w i bakterii chorobotw\\u00F3rczych przez uk\\u0142ad odporno\\u015Bciowy organizmu\",\"podziale j\\u0105dra kom\\u00F3rkowego\",\"kopiowanie materia\\u0142u genetycznego\"]", "Mitoza polega na:" },
                    { 36, 25, 1, "[\"sta\\u0142y\",\"plazma\",\"gazowy\",\"ciek\\u0142y\"]", "Jaki stan skupienia materii przeważa we Wszechświecie?" },
                    { 37, 25, 3, "[\"4\",\"5\",\"2\",\"3\"]", "Którą planetą w kolejności od Słońca jesteśmy?" },
                    { 38, 25, 1, "[\"Neptun\",\"Saturn\",\"Jowisz\",\"Mars\"]", "Która planeta ma wokół siebie charakterystyczne pierścienie?" },
                    { 39, 25, 3, "[\"Si\\u0142a od\\u015Brodkowa\",\"Si\\u0142a oporu\",\"Si\\u0142a ci\\u0119\\u017Cko\\u015Bci\",\"Si\\u0142a tarcia\"]", "Jaka siła powoduje ruch samochodu na rondzie?" },
                    { 40, 25, 1, "[\"Podczas ci\\u0105gni\\u0119cia sanek\",\"Podczas przenoszenia krzes\\u0142a z pokoju do kuchni\",\"Podczas hamowania\",\"Podczas podnoszenia ci\\u0119\\u017Car\\u00F3w\"]", "W którym przypadku nie wykonujemy pracy w sensie fizycznym?" },
                    { 41, 25, 1, "[\"Konfuzja\",\"Dyfuzja\",\"Konkluzja\",\"Parowanie\"]", "Jakie zjawisko odpowiada za rozchodzenie się zapachu po mieszkaniu?" },
                    { 42, 25, 2, "[\"czyste \\u017Celazo\",\"tylko tlenki \\u017Celaza\",\"tlenki i wodorotlenki \\u017Celaza\",\"tylko wodorotlenki \\u017Celaza\"]", "Głównymi składnikami rdzy są:" },
                    { 43, 25, 0, "[\"Ottawa\",\"Kair\",\"Toronto\",\"Dublin\"]", "Które miasto jest stolicą Kanady?" },
                    { 44, 25, 2, "[\"Pa\",\"Sn\",\"Po\",\"Cu\"]", "Który pierwiastek odkryła Maria Skłodowska Curie?" },
                    { 45, 25, 3, "[\"HNO3\",\"H3PO4\",\"H2SO4\",\"HCl\"]", "Jaki kwas mamy w żołądku?" },
                    { 46, 25, 0, "[\"Finlandia\",\"Hiszpania\",\"Szwecja\",\"Dania\"]", "Który kraj nie jest monarchią?" },
                    { 47, 25, 0, "[\"Nil\",\"Missisipi\",\"Jangcy\",\"Amazonka\"]", "Która z tych rzek jest najdłuższa na świecie?" },
                    { 48, 25, 1, "[\"Ameryka P\\u00F3\\u0142nocna\",\"Azja\",\"Afryka\",\"Australia z Oceani\\u0105\"]", "Który kontynent jest największy?" },
                    { 49, 25, 0, "[\"Sekwana\",\"Tag\",\"Tamiza\",\"Loara\"]", "Nad którą rzeką leży Paryż?" },
                    { 50, 25, 1, "[\"Finlandia\",\"Szwajcaria\",\"Austria\",\"Malta\"]", "Który kraj nie należy do Unii Europejskiej?" }
                });

            migrationBuilder.InsertData(
                table: "SystemStatusEntities",
                columns: new[] { "Id", "Status", "UpdatedAt" },
                values: new object[] { 1, 0, new DateTime(2024, 4, 3, 19, 20, 33, 83, DateTimeKind.Utc).AddTicks(6608) });

            migrationBuilder.InsertData(
                table: "UserEntities",
                columns: new[] { "Id", "AccountType", "EndTime", "Login", "Name", "Password", "StartTime", "Status", "Surname" },
                values: new object[,]
                {
                    { 1, 1, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "admin.admin", "admin", "0", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 0, "admin" },
                    { 2, 0, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "kamil.zdun", "Kamil", "111", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 0, "Zdun" },
                    { 3, 0, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "michał.zdun", "Michał", "222", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 0, "Zdunowski" },
                    { 4, 0, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "wojtek.zduński", "Wojtek", "333", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 0, "Zduński" }
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
